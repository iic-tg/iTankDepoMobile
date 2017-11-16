
Partial Class Admin_Audit
    Inherits Pagebase
#Region "Parameters"
    Public dsAudit As AuditDataSet
    Public dtAudit As DataTable
 
#End Region

#Region ""
    Private Const AUDIT As String = "AUDIT"
#End Region

#Region "Page_PreRender"

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            'Dim objCommon As New CommonData
            'If objCommon.GetMultiLocationSupportConfig().ToLower = "false" Then
            '    navAudit.Visible = False
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Admin/Audit.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgAudit_ClientBind"
    Protected Sub ifgAudit_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgAudit.ClientBind
        Try
            Dim objAudit As New Audit
            ifgAudit.UseCachedDataSource = True
            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            dsAudit = objAudit.pub_GetAuditDetails(intDPT_ID)
            dtAudit = dsAudit.Tables(AuditData._V_AUDIT)
            'For Each drAudit As DataRow In dtAudit.Rows
            '    drAudit.Item(AuditData.OLD_VL) = drAudit.Item(AuditData.OLD_VL).ToString.Replace(",", "<br />")
            '    drAudit.Item(AuditData.NEW_VL) = drAudit.Item(AuditData.NEW_VL).ToString.Replace(",", "<br />")
            'Next
            e.DataSource = dtAudit
            CacheData(AUDIT, dsAudit)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
End Class
