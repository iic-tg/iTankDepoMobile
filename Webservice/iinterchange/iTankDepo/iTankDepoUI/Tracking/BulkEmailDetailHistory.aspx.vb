
Partial Class Tracking_BulkEmailDetailHistory
    Inherits Pagebase



#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not (Page.IsPostBack AndAlso Page.IsCallback) Then
                If Not Request.QueryString("BulkEmailId") Is Nothing Then
                    pvt_getBulkEmailDetail(Request.QueryString("EquipmentNo").ToString, _
                                           Request.QueryString("GateinTransactionNo").ToString, _
                                           Request.QueryString("ActivityName").ToString, _
                                           Request.QueryString("ActivityNo").ToString, _
                                           Request.QueryString("BulkEmailId").ToString)

                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region


#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            '   CommonWeb.IncludeScript("Script/Tracking/BulkEmailDetail.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Tracking/BulkEmailDetailHistory.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_getBulkEmailDetail"
    Private Sub pvt_getBulkEmailDetail(ByVal bv_strEquipmentNo As String, _
                                       ByVal bv_strGateinTransactionNo As String, _
                                       ByVal bv_strActivityName As String, _
                                       ByVal bv_strActivityNo As String, _
                                       ByVal bv_strBulkEmailId As String)
        Try
            Dim objBulkEmail As New BulkEmail
            Dim strBodyHistory As String = String.Empty
            Dim dtView As New DataTable
            dtView = objBulkEmail.pub_GetBulkEmailDetailView(bv_strEquipmentNo, bv_strGateinTransactionNo, bv_strActivityName, bv_strActivityNo, bv_strBulkEmailId).Tables(BulkEmailData._BULK_EMAIL_DETAIL_VIEW)
            If dtView.Rows.Count > 0 Then
                strBodyHistory = pvt_generateHTML(dtView)
                divBulkEmailDetailHistory.InnerHtml = strBodyHistory
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
        Dim objCommon As New CommonData
        Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
        Dim objCommonConfig As New ConfigSetting()
        Try
            Dim strBodyView As String()
            Dim strSubject As String() = Nothing
            sbrView.Append("<br />")
            sbrView.Append(String.Concat("<b>", "From: ", "</b>", bv_dtView.Rows(0).Item(BulkEmailData.FRM_EML).ToString, "<br />"))
            sbrView.Append(String.Concat("<b>", "Sent: ", "</b>", CDate(bv_dtView.Rows(0).Item(BulkEmailData.SNT_DT)).ToString("dddd, d MMMM yyyy hh:mm tt"), "<br />"))
            sbrView.Append(String.Concat("<b>", "To: ", "</b>", bv_dtView.Rows(0).Item(BulkEmailData.TO_EML).ToString, "<br />"))
            sbrView.Append(String.Concat("<b>", "Bcc: ", "</b>", bv_dtView.Rows(0).Item(BulkEmailData.BCC_EML).ToString, "<br />"))
            sbrView.Append(String.Concat("<b>", "Subject: ", "</b>", bv_dtView.Rows(0).Item(BulkEmailData.SBJCT_VC).ToString))
            sbrView.Append("<br/><br/>")
            strBodyView = bv_dtView.Rows(0).Item(BulkEmailData.BDY_VC).ToString.Split(vbCrLf)
            strSubject = bv_dtView.Rows(0).Item(BulkEmailData.BDY_VC).ToString.Split("Subject: ")

            If strBodyView.Length = 0 Then
                strBodyView(0) = bv_dtView.Rows(0).Item(BulkEmailData.BDY_VC).ToString
            End If

            If CBool(bv_dtView.Rows(0).Item(BulkEmailData.RSND_BT)) = False Then
                sbrView.Append("<br/>")
                sbrView.Append(String.Concat("Bulk Email for the following Containers", "<br/><br/>"))
                If objCommonConfig.pub_GetConfigSingleValue("060", intDepotID) Then
                    sbrView.Append("<table border=""2""><tr><td style=""font-weight:bold;"">Customer/Agent</td><td style=""font-weight:bold;"">Type</td><td style=""font-weight:bold;"">Equipment No</td><td style=""font-weight:bold;"">Activity</td><td style=""font-weight:bold;"">Reference No</td><td style=""font-weight:bold;"">Amount</td><td style=""font-weight:bold;"">Currency</td></tr>")
                Else
                    sbrView.Append("<table border=""2""><tr><td style=""font-weight:bold;"">Customer/Party</td><td style=""font-weight:bold;"">Type</td><td style=""font-weight:bold;"">Equipment No</td><td style=""font-weight:bold;"">Activity</td><td style=""font-weight:bold;"">Reference No</td><td style=""font-weight:bold;"">Amount</td><td style=""font-weight:bold;"">Currency</td></tr>")
                End If

                For Each drBulkEmailDetail As DataRow In bv_dtView.Rows
                    sbrView.Append("<tr>")
                    sbrView.Append("<td>")
                    If objCommonConfig.pub_GetConfigSingleValue("060", intDepotID) Then
                        sbrView.Append(drBulkEmailDetail.Item(BulkEmailData.CSTMR_CD).ToString)
                    Else
                        sbrView.Append(drBulkEmailDetail.Item(BulkEmailData.SRVC_PRTNR_CD).ToString)
                    End If

                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEmailDetail.Item(BulkEmailData.SRVC_PRTNR_TYP_CD).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEmailDetail.Item(BulkEmailData.EQPMNT_NO).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEmailDetail.Item(BulkEmailData.ACTVTY_NAM).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEmailDetail.Item(BulkEmailData.GI_RF_NO).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEmailDetail.Item(BulkEmailData.AMNT_NC).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEmailDetail.Item(BulkEmailData.CRRNCY_CD).ToString)
                    sbrView.Append("</td>")
                Next
                sbrView.Append("</tr>")
                sbrView.Append("</table>")
                sbrView.Append("<br/>")
            End If
          
            For Each strBodyemail As String In strBodyView
                If strBodyemail.Contains("Subject: FW:") Then
                    sbrView.Append(strBodyemail)
                    sbrView.Append("<br/>")
                ElseIf strBodyemail.Contains("Subject: ") Then
                    sbrView.Append(strBodyemail)
                    sbrView.Append("<br/><br/>")
                    sbrView.Append(String.Concat("Bulk Email for the following Containers", "<br/><br/>"))
                    sbrView.Append("<table border=""2""><tr><td style=""font-weight:bold;"">Equipment No</td><td style=""font-weight:bold;"">Activity</td><td style=""font-weight:bold;"">Reference No</td><td style=""font-weight:bold;"">Amount</td><td style=""font-weight:bold;"">Currency</td></tr>")
                    sbrView.Append("<tr>")
                    sbrView.Append("<td>")
                    sbrView.Append(bv_dtView.Rows(0).Item(BulkEmailData.EQPMNT_NO).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(bv_dtView.Rows(0).Item(BulkEmailData.ACTVTY_NAM).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(bv_dtView.Rows(0).Item(BulkEmailData.GI_RF_NO).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(bv_dtView.Rows(0).Item(BulkEmailData.AMNT_NC).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(bv_dtView.Rows(0).Item(BulkEmailData.CRRNCY_CD).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("</tr>")
                    sbrView.Append("</table>")
                Else
                    sbrView.Append(strBodyemail)
                    sbrView.Append("<br/>")
                End If
               
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
        Return sbrView.ToString
    End Function
#End Region

End Class
