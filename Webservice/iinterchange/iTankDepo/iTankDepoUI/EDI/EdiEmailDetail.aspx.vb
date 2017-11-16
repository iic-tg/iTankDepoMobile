
Partial Class EDI_EdiEmailDetail
    Inherits Pagebase
#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not (Page.IsPostBack AndAlso Page.IsCallback) Then
                If Not Request.QueryString("EdiId") Is Nothing Then
                    ' pvt_getEdiEmailDetail(Request.QueryString("EdiId"))

                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
#Region "Decalration"
    Dim dsEDIEmailDetail As New EDIDataSet
    Dim dtEDIEmailDetail As DataTable
    Private Const BULKEMAILDETAIL As String = "BULKEMAILDETAIL"
    Private Const BULKEMAIL As String = "BULKEMAIL"
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            '   CommonWeb.IncludeScript("Script/Tracking/BulkEmailDetail.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/EDI/EDIEmail.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_getEdiEmailDetail"
    Private Sub pvt_getEdiEmailDetail(ByVal bv_intEdiID As Int64)
        Try
            Dim objEdiEmail As New EDI
            Dim strBodyHistory As String = String.Empty
            Dim dtView As New DataTable
            dtView = objEdiEmail.pub_GetEDIEmailDetail(bv_intEdiID).Tables(EDIData._EDI_EMAIL_DETAIL)
            If dtView.Rows.Count > 0 Then
                strBodyHistory = pvt_generateHTML(dtView)
                ' divEdiEmailDetail.InnerHtml = strBodyHistory
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_generateHTML()"
    Private Function pvt_generateHTML(ByVal bv_dtView As DataTable) As String
        Dim sbrView As New StringBuilder
        Try
            Dim strBodyView As String()
            Dim strSubject As String() = Nothing
            sbrView.Append("<br />")
            sbrView.Append(String.Concat("<b>", "From: ", "</b>", bv_dtView.Rows(0).Item(EDIData.FRM_EML).ToString, "<br />"))
            sbrView.Append(String.Concat("<b>", "Sent: ", "</b>", CDate(bv_dtView.Rows(0).Item(EDIData.SNT_DT)).ToString("dddd, d MMMM yyyy hh:mm tt"), "<br />"))
            sbrView.Append(String.Concat("<b>", "To: ", "</b>", bv_dtView.Rows(0).Item(EDIData.TO_EML).ToString, "<br />"))
            sbrView.Append(String.Concat("<b>", "Bcc: ", "</b>", bv_dtView.Rows(0).Item(EDIData.BCC_EML).ToString, "<br />"))
            sbrView.Append(String.Concat("<b>", "Subject: ", "</b>", bv_dtView.Rows(0).Item(EDIData.SBJCT_VC).ToString))
            sbrView.Append("<br/><br/>")
            strBodyView = bv_dtView.Rows(0).Item(EDIData.BDY_VC).ToString.Split(vbCrLf)
            strSubject = bv_dtView.Rows(0).Item(EDIData.BDY_VC).ToString.Split("Subject: ")

            If strBodyView.Length = 0 Then
                strBodyView(0) = bv_dtView.Rows(0).Item(EDIData.BDY_VC).ToString
            End If

         
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
        Return sbrView.ToString
    End Function
#End Region

#Region "ifgEdiEmailDetail_ClientBind"
    Protected Sub ifgEdiEmailDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEdiEmailDetail.ClientBind
        Try
            If Not (e.Parameters("EDIId") Is Nothing) Then
                Dim objbulkEmail As New EDI
                Dim dtBulkEMailView As New DataTable

                '  dsBulkEmailDetail = CType(RetrieveData(EDI), EDIData)
                Dim objEdiEmail As New EDI
                Dim dtView As New DataTable
                dtView = objEdiEmail.pub_GetEDIEmailDetail(e.Parameters("EDIId")).Tables(EDIData._EDI_EMAIL_DETAIL)
                e.DataSource = dtView
                ' dsBulkEmailDetail.Tables(BulkEmailData._BULK_EMAILDETAIL).Merge(dtBulkEMailView)
                ' CacheData(BULKEMAIL, dsBulkEmailDetail)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEdiEmailDetail_RowDataBound"

    Protected Sub ifgEdiEmailDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEdiEmailDetail.RowDataBound
        Try            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            '    Dim imgView As Image
            '    imgView = CType(e.Row.Cells(4).Controls(0), Image)
            '    imgView.ToolTip = "View"
            '    imgView.Visible = True
            '    imgView.ImageUrl = "../Images/letter.png"
            '    imgView.Attributes.Add("onclick", String.Concat("openBulkEmailDetailView('", e.Row.RowIndex.ToString, "','", drv.Item(BulkEmailData.BLK_EML_ID), "');return false;"))

            '    imgView.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
            'End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
End Class
