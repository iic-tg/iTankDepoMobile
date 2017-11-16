
Partial Class Admin_Unlock
    Inherits Pagebase

#Region "Page_OnCallback"
    ''' <summary>
    ''' This method is used to initialise call back methods
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "UnlockRecord"
                    pvt_UnlockRecord(e.GetCallbackValue("ReferenceNoField"), _
                                     e.GetCallbackValue("ReferenceNo"), _
                                     e.GetCallbackValue("Activity"))
            End Select
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim objLockDataCollection As New LockDataCollection
                Dim dtLockdata As DataTable
                dtLockdata = objLockDataCollection.Values
                ifgUnlock.DataSource = dtLockdata
                ifgUnlock.DataBind()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgUnlock_ClientBind"
    Protected Sub ifgUnlock_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgUnlock.ClientBind
        Try
            Dim dtLockdata As DataTable
            Dim objLockDataCollection As New LockDataCollection
            dtLockdata = objLockDataCollection.Values
            e.DataSource = dtLockdata
            If dtLockdata.Rows.Count > 0 Then
                e.OutputParameters.Add("norecordsfound", "False")
            Else
                e.OutputParameters.Add("norecordsfound", "True")
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "ifgUnlock_RowDataBound"
    Protected Sub ifgUnlock_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgUnlock.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim Reporthyplnk As HyperLink
                Reporthyplnk = CType(e.Row.Cells(6).Controls(0), HyperLink)
                Reporthyplnk.Attributes.Add("onclick", String.Concat("unlockRecord('", drv.Item("RefNoField").ToString(), "','", drv.Item("RefNo").ToString(), "','", drv.Item("ActivityName").ToString(), "');"))
                Reporthyplnk.NavigateUrl = "#"
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                   MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UnlockRecord()"
    Private Sub pvt_UnlockRecord(ByVal bv_strReferenceNoField As String, _
                                 ByVal bv_strReferenceNo As String, _
                                 ByVal bv_strActivity As String)
        Try
            Dim objCommonData As New CommonData
            objCommonData.FlushLockDataByActivityNameandRefNo(bv_strReferenceNoField, bv_strReferenceNo, bv_strActivity)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Admin/Unlock.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
