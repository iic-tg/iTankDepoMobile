
Partial Class Masters_CleaningType
    Inherits Pagebase
#Region "Declarations"
    Private strMSGUPDATE As String = "Cleaning Type Code(s) Updated Successfully."
    Private strMSGDUPLICATE As String = "This combination already exists."
    Private Const CleaningType As String = "Cleaning_Type"
#End Region

#Region "Parameters"
    Public dsCleaningType As CleaningTypeDataSet
    Public dtCleaningType As DataTable
    Private pvt_strTableKey As String
    Private pvt_strTableName As String
#End Region

#Region "Page_Load"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strpagetitle As String = Request.QueryString("pagetitle")
            ifgCleaningType.DeleteButtonText = "Delete"
            ifgCleaningType.RefreshButtonText = "Refresh"
            ifgCleaningType.AddButtonText = "Add Cleaning Type"
            ifgCleaningType.ActionButtons.Item(0).Text = "Upload"
            ifgCleaningType.ActionButtons.Item(1).Text = "Export"

            If Not Request.QueryString("Export") Is Nothing Then
                If Request.QueryString("Export") = "True" Then
                    Dim objCommon As New CommonData
                    Dim objstream As New IO.StringWriter
                    objstream = objCommon.ExportToExcel(CType(ifgCleaningType.DataSource, DataTable), "Cleaning Type", "Code,Description,Default,Active", "CLNNG_TYP_ID", False)
                    Response.ContentType = "application/vnd.Excel"
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Cleaning Type" + ".xls")
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
                pvt_ValidateCleaningTypeCode(e.GetCallbackValue("Code"), _
                               e.GetCallbackValue("GridIndex"), e.GetCallbackValue("RowState"), e.GetCallbackValue("WFDATA"))
            Case "UpdateCleaningType"
                pvt_UpdateCleaningType(e.GetCallbackValue("WFDATA"))
        End Select
    End Sub
#End Region

#Region "pvt_ValidateCleaningTypeCode"
    Private Sub pvt_ValidateCleaningTypeCode(ByVal bv_strCode As String, ByVal bv_intGridRowIndex As Integer, ByVal bv_strRowState As String, ByVal bv_strWFDATA As String)
        Try
            Dim blnValid As Boolean
            Dim strExistCode As String = String.Empty
            dsCleaningType = CType(RetrieveData(CleaningType), CleaningTypeDataSet)
            Dim dtCleaningType As DataTable = dsCleaningType.Tables(CleaningTypeData._CLEANING_TYPE).Copy
            dtCleaningType.AcceptChanges()

            'Checking whether the entered code is available in grid
            Dim intResultIndex() As System.Data.DataRow = dtCleaningType.Select("Code" & "='" & bv_strCode & "'")

            If intResultIndex.Length > 0 Then
                blnValid = False
            Else
                blnValid = True
            End If

            'Checking whether the entered code is available in database
            If blnValid = True Then
                If bv_strRowState = "Added" Then
                    Dim objCleaningType As New CleaningType
                    blnValid = objCleaningType.pub_ValidateCleaningType(bv_strCode, bv_strWFDATA)
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

#Region "ifgCleaningType_ClientBind"
    Protected Sub ifgCleaningType_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgCleaningType.ClientBind
        Try
            Dim obj As New CleaningType
            Dim objCommondata As New CommonData
            Dim strWfData As String = CStr(e.Parameters("WFDATA"))
            ifgCleaningType.UseCachedDataSource = True
            If objCommondata.GetMultiLocationSupportConfig().ToLower = "true" Then
                strWfData = String.Concat("DPT_ID=", objCommondata.GetHeadQuarterID().ToString)
            End If
            dsCleaningType = obj.pub_CleaningTypeGetCleaningTypeByDepotID(strWfData)
            obj.pub_GetColumnAliasName(dsCleaningType)
            dtCleaningType = dsCleaningType.Tables(CleaningTypeData._CLEANING_TYPE)
            e.DataSource = dtCleaningType
            CacheData(CleaningType, dsCleaningType)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCleaningType_RowInserting"
    Protected Sub ifgCleaningType_RowInserting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgCleaningType.RowInserting
        Try
            dsCleaningType = CType(RetrieveData(CleaningType), CleaningTypeDataSet)
            dtCleaningType = dsCleaningType.Tables(CleaningTypeData._CLEANING_TYPE)
            e.Values(CleaningTypeData.CLNNG_TYP_ID) = CommonWeb.GetNextIndex(dtCleaningType, CleaningTypeData.CLNNG_TYP_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCleaningType_RowDeleting"
    Protected Sub ifgCleaningType_RowDeleting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgCleaningType.RowDeleting
        Try
            dsCleaningType = CType(RetrieveData(CleaningType), CleaningTypeDataSet)
            Dim dtCleaning As Data.DataTable = dsCleaningType.Tables(CleaningTypeData._CLEANING_TYPE).Copy
            If CType(ifgCleaningType.DataSource, DataTable).Select(String.Concat(CleaningTypeData.CLNNG_TYP_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then
                e.Cancel = True
                e.OutputParamters("Delete") = String.Concat("Cleaning Type : Code ", dtCleaning.Select(String.Concat(CleaningTypeData.CLNNG_TYP_ID, "=", e.Keys(0)))(0).Item("Code").ToString, " cannot be deleted")
                Exit Sub
            Else
                e.OutputParamters("Delete") = String.Concat("Cleaning Type : Code ", dtCleaning.Select(String.Concat(CleaningTypeData.CLNNG_TYP_ID, "=", e.Keys(0)))(0).Item("Code").ToString, " has been be deleted. Click submit to save changes.")
                Exit Sub
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateCleaningType"
    Private Sub pvt_UpdateCleaningType(ByVal bv_strWFData As String)
        Try
            Dim objCleaningType As New CleaningType
            Dim objCommondata As New CommonData
            dtCleaningType = CType(ifgCleaningType.DataSource, DataTable)
            If objCommondata.GetMultiLocationSupportConfig().ToLower = "true" Then
                bv_strWFData = String.Concat("DPT_ID=", objCommondata.GetHeadQuarterID().ToString)
            End If
            objCleaningType.pub_UpdateCleaningType(CType(dtCleaningType.DataSet, CleaningTypeDataSet), objCommondata.GetCurrentUserName(), CDate(objCommondata.GetCurrentDate()), bv_strWFData)
            dtCleaningType.AcceptChanges()
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCleaningType_RowDataBound"
    Protected Sub ifgCleaningType_RowDataBound(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgCleaningType.RowDataBound
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
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/CleaningType.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class