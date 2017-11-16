Option Strict On
Partial Class Tracking_BulkEmailDetail
    Inherits Pagebase
#Region "Decalration"
    Dim dsBulkEmailDetail As New BulkEmailDataSet
    Dim dtBulkEmailDetail As DataTable
    Private Const BULKEMAILDETAIL As String = "BULKEMAILDETAIL"
    Private Const BULKEMAIL As String = "BULKEMAIL"
#End Region

#Region "PreRender"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Tracking/BulkEmailDetail.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "PageLoad"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
     
    End Sub
#End Region

#Region "ifgBulkEmailDetail_ClientBind"
    Protected Sub ifgBulkEmailDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgBulkEmailDetail.ClientBind
        Try
            If Not (e.Parameters("EquipmentNo") Is Nothing AndAlso e.Parameters("GI_TRANS_NO") Is Nothing AndAlso e.Parameters("ActivityName") Is Nothing AndAlso e.Parameters("ActivityNo") Is Nothing) Then
                Dim objbulkEmail As New BulkEmail
                Dim dtBulkEMailView As New DataTable

                dsBulkEmailDetail = CType(RetrieveData(BULKEMAIL), BulkEmailDataSet)
                dtBulkEMailView = objbulkEmail.getBulkEmailDetail(CStr(e.Parameters("EquipmentNo")), CStr(e.Parameters("GI_TRANS_NO")), CStr(e.Parameters("ActivityName")), CStr(e.Parameters("ActivityNo"))).Tables(BulkEmailData._BULK_EMAILDETAIL)
                e.DataSource = dtBulkEMailView
                dsBulkEmailDetail.Tables(BulkEmailData._BULK_EMAILDETAIL).Merge(dtBulkEMailView)
                CacheData(BULKEMAIL, dsBulkEmailDetail)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgBulkEmailDetail_RowDataBound"

    Protected Sub ifgBulkEmailDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgBulkEmailDetail.RowDataBound
        Try            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim imgView As Image
                imgView = CType(e.Row.Cells(4).Controls(0), Image)
                imgView.ToolTip = "View"
                imgView.Visible = True
                imgView.ImageUrl = "../Images/letter.png"
                imgView.Attributes.Add("onclick", String.Concat("openBulkEmailDetailView('", e.Row.RowIndex.ToString, "','", drv.Item(BulkEmailData.BLK_EML_ID), "');return false;"))

                imgView.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
