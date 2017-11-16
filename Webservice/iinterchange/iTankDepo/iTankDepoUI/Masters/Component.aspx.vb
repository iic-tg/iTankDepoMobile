
Partial Class Masters_Component
    Inherits Pagebase
#Region "Declarations"
    Private strMSGUPDATE As String = "Component Code(s) Updated Successfully."
    Private strMSGDUPLICATE As String = "This combination already exists."
    Private Const COMPONENT As String = "COMPONENT"
#End Region

#Region "Parameters"
    Public dsComponent As ComponentDataSet
    Public dtComponent As DataTable
    Private pvt_strTableKey As String
    Private pvt_strTableName As String
#End Region

#Region "Page_Load"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strpagetitle As String = Request.QueryString("pagetitle")
            ifgComponent.DeleteButtonText = "Delete"
            ifgComponent.RefreshButtonText = "Refresh"
            ifgComponent.AddButtonText = "Add Component"
            ifgComponent.ActionButtons.Item(0).Text = "Upload"
            ifgComponent.ActionButtons.Item(1).Text = "Export"

            If Not Request.QueryString("Export") Is Nothing Then
                If Request.QueryString("Export") = "True" Then
                    Dim objCommon As New CommonData
                    Dim objstream As New IO.StringWriter
                    objstream = objCommon.ExportToExcel(CType(ifgComponent.DataSource, DataTable), "Component", "CMPNNT_CD,CMPNNT_DSCRPTN_VC,EQPMNT_TYP_CD,ASSMBLY,ACTV_BT", "CMPNNT_ID", True)
                    Response.ContentType = "application/vnd.Excel"
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Component" + ".xls")
                    Response.Write(objstream.ToString())
                    Response.End()
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "ValidateCode"
                pvt_ValidateComponentCode(e.GetCallbackValue("Code"), _
                               e.GetCallbackValue("GridIndex"), e.GetCallbackValue("RowState"), e.GetCallbackValue("WFDATA"))
            Case "UpdateComponent"
                pvt_UpdateComponent(e.GetCallbackValue("WFDATA"))
        End Select
    End Sub
#End Region

#Region "pvt_ValidateComponentCode"
    Private Sub pvt_ValidateComponentCode(ByVal bv_strCode As String, ByVal bv_intGridRowIndex As Integer, ByVal bv_strRowState As String, ByVal bv_strWFDATA As String)
        Try
            Dim blnValid As Boolean
            Dim strExistCode As String = String.Empty
            dsComponent = CType(RetrieveData(COMPONENT), ComponentDataSet)
            Dim dtComponent As DataTable = dsComponent.Tables(ComponentData._COMPONENT).Copy
            dtComponent.AcceptChanges()

            'Checking whether the entered code is available in grid
            Dim intResultIndex() As System.Data.DataRow = dtComponent.Select("CMPNNT_CD" & "='" & bv_strCode & "'")

            If intResultIndex.Length > 0 Then
                blnValid = False
            Else
                blnValid = True
            End If

            'Checking whether the entered code is available in database
            If blnValid = True Then
                If bv_strRowState = "Added" Then
                    Dim objComponent As New Component
                    blnValid = objComponent.pub_ValidateComponent(bv_strCode, bv_strWFDATA)
                End If
            End If

            If blnValid = True Then
                pub_SetCallbackReturnValue("pkValid", "true")
            Else
                pub_SetCallbackReturnValue("pkValid", "false")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgComponent_ClientBind"
    Protected Sub ifgComponent_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgComponent.ClientBind
        Try
            Dim obj As New Component
            Dim strWfData As String = CStr(e.Parameters("WFDATA"))
            ifgComponent.UseCachedDataSource = True
            dsComponent = obj.pub_ComponentGetComponentByDepotID(strWfData)
            dtComponent = dsComponent.Tables(ComponentData._COMPONENT)
            e.DataSource = dtComponent
            CacheData(COMPONENT, dsComponent)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgComponent_RowInserting"
    Protected Sub ifgComponent_RowInserting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgComponent.RowInserting
        Try
            dsComponent = CType(RetrieveData(COMPONENT), ComponentDataSet)
            dtComponent = dsComponent.Tables(ComponentData._COMPONENT)
            e.Values(ComponentData.CMPNNT_ID) = CommonWeb.GetNextIndex(dtComponent, ComponentData.CMPNNT_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgComponent_RowDeleting"
    Protected Sub ifgComponent_RowDeleting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgComponent.RowDeleting
        Try
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgComponent.PageSize * ifgComponent.PageIndex + e.RowIndex
            If CType(ifgComponent.DataSource, DataTable).Select(String.Concat(ComponentData.CMPNNT_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.Cancel = True
                e.OutputParamters("Delete") = "Component cannot be deleted"
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateComponent"
    Private Sub pvt_UpdateComponent(ByVal bv_strWFData As String)
        Try
            Dim objComponent As New Component
            Dim objCommondata As New CommonData
            dtComponent = CType(ifgComponent.DataSource, DataTable)
            objComponent.pub_UpdateComponent(CType(dtComponent.DataSet, ComponentDataSet), objCommondata.GetCurrentUserName(), CDate(objCommondata.GetCurrentDate()), bv_strWFData)
            dtComponent.AcceptChanges()
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgComponent_RowDataBound"
    Protected Sub ifgComponent_RowDataBound(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgComponent.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As System.Data.DataRowView
                    drv = CType(e.Row.DataItem, Data.DataRowView)
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Masters/Component.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class
