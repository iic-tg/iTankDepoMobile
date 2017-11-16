Imports iInterchange.iTankDepo.Business.Services
Imports iInterchange.iTankDepo.Business.Common
Imports iInterchange.iTankDepo.Business.Operations
Imports iInterchange.iTankDepo.Business.Admin
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.Data
Imports Microsoft.Reporting.WinForms
Imports System.Configuration
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Web

Public Class Service

#Region "Declaration"
    Dim dsBulk_Email As New Bulk_EmailDataSet
    Dim objBulk_mail As New Bulk_Email
    Dim rptViewer As New LocalReport
    Private m_streams As IList(Of Stream)
    Dim arr_FileList As New ArrayList
    Private pvt_strDomainPath As String = AppDomain.CurrentDomain.BaseDirectory.ToString
    Dim blnDiagnostic As Boolean = Config.pub_GetAppConfigValue("diagnostic")
    Dim intDepotID As Integer = Config.pub_GetAppConfigValue("DepotID")
    Dim strReapirEstimate As String = Config.pub_GetAppConfigValue("RepariEstimate")
    Dim strReapirApproval As String = Config.pub_GetAppConfigValue("RepairApproval")
    Dim strRepairCompletion As String = Config.pub_GetAppConfigValue("RepairCompletion")
    Dim strEstimateRDLC As String = Config.pub_GetAppConfigValue("EstimateRDLC")
    Dim strWorkOrderRDLC As String = Config.pub_GetAppConfigValue("WorkOrderRDLC")
    Dim strCompiledEstimateRDLC As String = Config.pub_GetAppConfigValue("CompiledEstimateRDLC")
    Dim strCleaningRDLC As String = Config.pub_GetAppConfigValue("CleaningRDLC")
    Dim strReportFolder As String = Config.pub_GetAppConfigValue("ReportFolder")
    Dim strBulkEmailResendFolder As String = Config.pub_GetAppConfigValue("UploadBulkEmail")
    Dim strEquipId As String = String.Empty
    Dim bv_strStatus As String = String.Empty
    Dim objCommonConfig As New ConfigSetting()
    Dim dtDepot As New DataTable
#End Region

    'Protected Overrides Sub OnStart(ByVal args() As String)
    'Me.tmrBulkEmail.Interval = CDbl(Config.pub_GetAppConfigValue("BulkEmailTimerInterval"))
    ' Add code here to start your service. This method should set things
    ' in motion so your service can do its work.
    'End Sub

    'Protected Overrides Sub OnStop()
    ' Add code here to perform any tear-down necessary to stop your service.
    'End Sub

    'Private Sub tmrBulkEmail_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrBulkEmail.Elapsed
    '    Try
    '        SyncLock tmrBulkEmail
    '            pvt_Diagnostic("Timer Elapsed")
    '            pvt_SendBulkEMail()
    '        End SyncLock
    '    Catch ex As Exception
    '        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub btnStart_Click(sender As Object, e As System.EventArgs) Handles btnStart.Click
        pvt_SendBulkEMail()
    End Sub

#Region "pvt_SendBulkEMail"
    Private Sub pvt_SendBulkEMail()
        Try
            Dim objBulkEmail As New Bulk_Email
            Dim blnStatus As Boolean
            Dim strFileName As String = String.Empty

            dtDepot = objBulkEmail.pub_GetDepot(intDepotID).Tables(Bulk_EmailData._DEPOT)
            dsBulk_Email = objBulkEmail.pub_GetBulk_Email(intDepotID)
            Dim objDirectotyInfo As New DirectoryInfo(strReportFolder)
            'pvt_Diagnostic("Send_EmailWithAttachment - End : ")
            For Each objFileinfo As FileInfo In objDirectotyInfo.GetFiles("*.pdf")
                Try
                    objFileinfo.Delete()
                Catch ex As Exception
                    Continue For
                End Try
            Next
            For Each drPendingBulkEmail As DataRow In dsBulk_Email.Tables(Bulk_EmailData._V_BULK_EMAIL).Rows
                Dim sbrSNO As New StringBuilder
                arr_FileList.Clear()

                'pvt_Diagnostic("GetPendingActivititesForTheJob - Start")

                Dim dtBulkEmailDetail As New DataTable
                Dim dtBulkEMailGroup As New DataTable
                dtBulkEMailGroup = objBulkEmail.pub_GetBulkEmailDetailbyBulkEmailID_Group(drPendingBulkEmail.Item(Bulk_EmailData.BLK_EML_ID)).Tables(Bulk_EmailData._V_BULK_EMAIL_DETAIL_GROUP)
                dtBulkEmailDetail = objBulkEmail.pub_GetBulkEmailDetailbyBulkEmailID(drPendingBulkEmail.Item(Bulk_EmailData.BLK_EML_ID)).Tables(Bulk_EmailData._V_BULK_EMAIL_DETAIL)

                For Each drActivity As DataRow In dtBulkEMailGroup.Select(String.Concat(Bulk_EmailData.RSND_BT, " = 'False'"))
                    If drPendingBulkEmail.Item(Bulk_EmailData.BLK_EML_FRMT_ID) = 54 And drActivity.Item(Bulk_EmailData.ACTVTY_NAM) <> "Cleaning" Then
                        'pvt_Diagnostic("Activity Compiled And Cleaning - Start")
                        If drActivity.Item(Bulk_EmailData.ACTVTY_NO) <> Nothing Then
                            sbrSNO.Append(",")
                        End If
                        sbrSNO.Append(drActivity.Item(Bulk_EmailData.ACTVTY_NO))
                        'pvt_Diagnostic("Activity Compiled And Cleaning - End")
                    Else
                        If (drActivity.Item(Bulk_EmailData.ACTVTY_NAM) = "Repair Estimate" Or drActivity.Item(Bulk_EmailData.ACTVTY_NAM) = "Repair Approval" Or drActivity.Item(Bulk_EmailData.ACTVTY_NAM) = "Repair Completion") And drPendingBulkEmail.Item(Bulk_EmailData.BLK_EML_FRMT_ID) <> 54 Then
                            Dim objRepairEstimate As New RepairEstimate
                            Dim ds As New DataSet
                            Dim bv_Param As String = String.Concat("EstimateID=", drActivity.Item(Bulk_EmailData.ACTVTY_NO), "&USERNAME=", drPendingBulkEmail.Item(Bulk_EmailData.CRTD_BY), "&DPT_ID=", CInt(dsBulk_Email.Tables(Bulk_EmailData._V_BULK_EMAIL).Rows(0).Item(Bulk_EmailData.DPT_ID)))
                            ds = objRepairEstimate.RepairWorkOrder(bv_Param, Nothing)
                            'pvt_Diagnostic("Activity - ESTIMATE And APPROVAL - Start")
                            pvt_GenerateEstimateReportPDF(ds, (drActivity.Item(Bulk_EmailData.ACTVTY_NAM)).ToString, strFileName)
                            drActivity.Item(Bulk_EmailData.ATTCHMNT_PTH) = strFileName
                            For Each drPath As DataRow In dtBulkEmailDetail.Select(String.Concat(Bulk_EmailData.ACTVTY_NO, " = '", drActivity.Item(Bulk_EmailData.ACTVTY_NO), "'"))
                                drPath.Item(Bulk_EmailData.ATTCHMNT_PTH) = strFileName
                            Next
                            'pvt_Diagnostic("Activity - ESTIMATE And APPROVAL - End")
                        ElseIf drActivity.Item(Bulk_EmailData.ACTVTY_NAM) = "Cleaning" Then
                            pvt_Diagnostic("Activity - CLEANING : Start")
                            Dim objCleaning As New Cleaning
                            Dim dsCleaning As CleaningDataSet
                            Dim dtCleaning As DataTable
                            dtCleaning = objBulk_mail.GetCleaningDetails(drActivity.Item(Bulk_EmailData.ACTVTY_NO), CInt(dsBulk_Email.Tables(Bulk_EmailData._V_BULK_EMAIL).Rows(0).Item(Bulk_EmailData.DPT_ID))).Tables(Bulk_EmailData._V_CLEANING)
                            Dim bv_Param As String = String.Concat("CleaningID=", drActivity.Item(Bulk_EmailData.ACTVTY_NO), _
                                                                   "&CleanginCertificateNo=", dtCleaning.Rows(0).Item(Bulk_EmailData.CLNNG_CERT_NO).ToString, _
                                                                   "&DepotID=", CInt(dsBulk_Email.Tables(Bulk_EmailData._V_BULK_EMAIL).Rows(0).Item(Bulk_EmailData.DPT_ID)), _
                                                                   "&EquipmentNo=", CStr(drActivity.Item(Bulk_EmailData.EQPMNT_NO)), _
                                                                   "&GI_TRNSCTN_NO=", CStr(drActivity.Item(Bulk_EmailData.GI_TRNSCTN_NO)))
                            dsCleaning = objCleaning.pub_GenerateCleaningCertificate(bv_Param)
                            pvt_GenerateCleaningReportPDF(dsCleaning, drActivity.Item(Bulk_EmailData.ACTVTY_NAM), strFileName)
                            drActivity.Item(Bulk_EmailData.ATTCHMNT_PTH) = strFileName
                            For Each drPath As DataRow In dtBulkEmailDetail.Select(String.Concat(Bulk_EmailData.ACTVTY_NO, " = '", drActivity.Item(Bulk_EmailData.ACTVTY_NO), "'"))
                                drPath.Item(Bulk_EmailData.ATTCHMNT_PTH) = strFileName
                            Next
                            'pvt_Diagnostic("Activity - CLEANING : End")
                        End If
                    End If
                Next
                If (sbrSNO.Length > 1) Then
                    sbrSNO.Remove(0, 1)
                End If
                'pvt_Diagnostic("GetPendingActivititesForTheJob - End")
                If sbrSNO.ToString <> Nothing Then
                    pvt_GenerateCompiledEstimateReportPDF(sbrSNO.ToString(), drPendingBulkEmail.Item(Bulk_EmailData.CSTMR_ID), drPendingBulkEmail.Item(Bulk_EmailData.CRTD_BY), intDepotID)
                End If
                drPendingBulkEmail.Item(Bulk_EmailData.SNT_BT) = 1
                drPendingBulkEmail.Item(Bulk_EmailData.SNT_DT) = DateTime.Now
                blnStatus = pvt_SendBulkMail(drPendingBulkEmail, dtBulkEmailDetail)
                If blnStatus Then
                    objBulkEmail.pvt_UpdateSentStatus(drPendingBulkEmail.Item(Bulk_EmailData.BLK_EML_ID), drPendingBulkEmail.Item(Bulk_EmailData.DPT_ID))
                    For Each drBulkEmailDetail As DataRow In dtBulkEmailDetail.Select(String.Concat(Bulk_EmailData.ATTCHMNT_PTH, " IS NOT NULL"))
                        objBulkEmail.pvt_UpdateBulkEmailDetail(drBulkEmailDetail.Item(Bulk_EmailData.BLK_EML_DTL_ID), drBulkEmailDetail.Item(Bulk_EmailData.ATTCHMNT_PTH))
                    Next
                End If
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat(ex, "Inner Exception : ", ex.InnerException))
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_SendBulkMail"
    Private Function pvt_SendBulkMail(ByRef br_drprndingjob As DataRow, ByRef br_dtActivities As DataTable) As Boolean
        Try
            Dim blnStatus As Boolean
            Dim blnResend As Boolean = False

            Dim strBodyHTML As String
            'pvt_Diagnostic("HTML Body - Start : ")
            Dim emailBody As String = String.Empty
            If (Not br_drprndingjob.Item(Bulk_EmailData.BDY_VC) Is DBNull.Value) Then
                emailBody = br_drprndingjob.Item(Bulk_EmailData.BDY_VC)
            Else
                emailBody = String.Empty
            End If
            If br_dtActivities.Rows.Count > 0 Then
                If Not IsDBNull(br_dtActivities.Rows(0).Item(Bulk_EmailData.RSND_BT)) Then
                    blnResend = br_dtActivities.Rows(0).Item(Bulk_EmailData.RSND_BT)
                    If blnResend Then
                        arr_FileList.Add(String.Concat(strBulkEmailResendFolder, br_dtActivities.Rows(0).Item(Bulk_EmailData.ATTCHMNT_PTH)))
                    End If
                End If
            End If
            If blnResend Then
                strBodyHTML = pvt_generateHTML(br_dtActivities, emailBody)
            Else
                strBodyHTML = pvt_GenerateBody(br_dtActivities, emailBody, blnResend)
            End If

            'pvt_Diagnostic("HTML Body - End : ")
            'pvt_Diagnostic("Send_EmailWithAttachment - Start : ")
            blnStatus = pub_Send_EmailWithAttachment(br_drprndingjob.Item(Bulk_EmailData.FRM_EML).ToString, br_drprndingjob.Item(Bulk_EmailData.TO_EML).ToString, _
                                                     br_drprndingjob.Item(Bulk_EmailData.BCC_EML).ToString, br_drprndingjob.Item(Bulk_EmailData.SBJCT_VC).ToString, _
                                                     strBodyHTML, "", blnResend, arr_FileList)
            'pvt_Diagnostic("Send_EmailWithAttachment - End : ")
            'For Each objFileinfo As FileInfo In objDirectotyInfo.GetFiles("*.pdf")
            '    objFileinfo.Delete()
            'Next

            Return blnStatus
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "pvt_GenerateBody"
    Private Function pvt_GenerateBody(ByRef br_DtReports As DataTable, ByRef br_strBodyContent As String, _
                                      ByVal bv_blnResend As Boolean) As String
        Try
            Dim sbBody As New StringBuilder
            sbBody.Append("<br/><br/>")
            sbBody.Append("Bulk Email for the following Containers")
            sbBody.Append("<br/><br/>")
            If bv_blnResend Then
                sbBody.Append(br_strBodyContent)
                sbBody.Append("<br/><br/>")
            End If
            sbBody.Append("<table border=""1"" style=""font-size:10.0pt;font-family:""Arial"",""sans-serif"";"">")
            sbBody.Append("<tr>")
            sbBody.Append("<td>")
            sbBody.Append("Customer/Party")
            sbBody.Append("</td>")
            sbBody.Append("<td>")
            sbBody.Append("Type")
            sbBody.Append("</td>")
            sbBody.Append("<td>")
            sbBody.Append("Equipment No")
            sbBody.Append("</td>")
            sbBody.Append("<td>")
            sbBody.Append("Activity")
            sbBody.Append("</td>")
            sbBody.Append("<td>")
            sbBody.Append("Reference No")
            sbBody.Append("</td>")
            sbBody.Append("<td>")
            sbBody.Append("Amount")
            sbBody.Append("</td>")
            sbBody.Append("<td>")
            sbBody.Append("Currency")
            sbBody.Append("</td>")
            sbBody.Append("</tr>")

            For Each dr As DataRow In br_DtReports.Rows
                sbBody.Append("<tr>")
                sbBody.Append("<td>")
                sbBody.Append(dr.Item(Bulk_EmailData.SRVC_PRTNR_CD))
                sbBody.Append("</td>")
                sbBody.Append("<td>")
                sbBody.Append(dr.Item(Bulk_EmailData.SRVC_PRTNR_TYP_CD))
                sbBody.Append("</td>")
                sbBody.Append("<td>")
                sbBody.Append(dr.Item(Bulk_EmailData.EQPMNT_NO))
                sbBody.Append("</td>")
                sbBody.Append("<td>")
                sbBody.Append(dr.Item(Bulk_EmailData.ACTVTY_NAM))
                sbBody.Append("</td>")
                sbBody.Append("<td>")
                sbBody.Append(dr.Item(Bulk_EmailData.GI_RF_NO))
                sbBody.Append("</td>")
                sbBody.Append("<td>")
                sbBody.Append(dr.Item(Bulk_EmailData.AMNT_NC))
                sbBody.Append("</td>")
                sbBody.Append("<td>")
                sbBody.Append(dr.Item(Bulk_EmailData.CRRNCY_CD))
                sbBody.Append("</td>")
                sbBody.Append("</tr>")
            Next

            sbBody.Append("</table>")
            sbBody.Append("<br/>")
            If bv_blnResend = False Then
                sbBody.Append(br_strBodyContent)
            End If
            sbBody.Append("<br/><br/>")
            sbBody.Append("<div class=MsoNormal align=center style='text-align:center'><span lang=EN-GBstyle='font-size:10.0pt;font-family:""Arial"",""sans-serif"";mso-fareast-font-family:""Times New Roman"";mso-ansi-language:EN-GB'><hr size=2 width=""100%""align=center></span></div>")
            sbBody.Append("<br/><br/>")
            sbBody.Append("This is an automated message sent to you by <B>Joint Tank Services FZCO</B>. For any further clarification/information, please communicate with us on ops@jts.ae<mailto:ops@jts.ae>")
            Return sbBody.ToString()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Function
#End Region

#Region "pvt_generateHTML()"
    Private Function pvt_generateHTML(ByVal bv_dtView As DataTable, _
                                      ByVal bv_stremailBody As String) As String
        Dim sbrView As New StringBuilder
        Try
            Dim strBodyView As String()
            Dim strSubject As String() = Nothing
            sbrView.Append("<br/><br/>")
            strBodyView = bv_stremailBody.Split(vbCrLf)
            strSubject = bv_stremailBody.ToString.Split("Subject: ")
            If strBodyView.Length = 0 Then
                strBodyView(0) = bv_stremailBody.ToString
            End If

            If strSubject.Length = 1 Then
                sbrView.Append("<br/>")
                sbrView.Append(String.Concat("Bulk Email for the following Containers", "<br/><br/>"))
                sbrView.Append("<table border=""2""><tr><td>Customer/Party</td><td>Type</td><td>Equipment No</td><td>Activity</td><td>Reference No</td><td>Amount</td><td>Currency</td></tr>")
                For Each drBulkEMailDetail As DataRow In bv_dtView.Rows
                    sbrView.Append("<tr>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.SRVC_PRTNR_CD).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.SRVC_PRTNR_TYP_CD).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.EQPMNT_NO).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.ACTVTY_NAM).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.GI_RF_NO).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.AMNT_NC).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("<td>")
                    sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.CRRNCY_CD).ToString)
                    sbrView.Append("</td>")
                    sbrView.Append("</tr>")
                Next
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
                    sbrView.Append("<table border=""2""><tr><td>Customer/Party</td><td>Type</td><td>Equipment No</td><td>Activity</td><td>Reference No</td><td>Amount</td><td>Currency</td></tr>")
                    For Each drBulkEMailDetail As DataRow In bv_dtView.Rows
                        sbrView.Append("<tr>")
                        sbrView.Append("<td>")
                        sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.EQPMNT_NO).ToString)
                        sbrView.Append("</td>")
                        sbrView.Append("<td>")
                        sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.ACTVTY_NAM).ToString)
                        sbrView.Append("</td>")
                        sbrView.Append("<td>")
                        sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.GI_RF_NO).ToString)
                        sbrView.Append("</td>")
                        sbrView.Append("<td>")
                        sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.AMNT_NC).ToString)
                        sbrView.Append("</td>")
                        sbrView.Append("<td>")
                        sbrView.Append(drBulkEMailDetail.Item(BulkEmailData.CRRNCY_CD).ToString)
                        sbrView.Append("</td>")
                        sbrView.Append("</tr>")
                    Next
                    sbrView.Append("</table>")
                Else
                        sbrView.Append(strBodyemail)
                        sbrView.Append("<br/>")
                End If
            Next
            sbrView.Append("<br/><br/>")
            sbrView.Append("<div class=MsoNormal align=center style='text-align:center'><span lang=EN-GBstyle='font-size:10.0pt;font-family:""Arial"",""sans-serif"";mso-fareast-font-family:""Times New Roman"";mso-ansi-language:EN-GB'><hr size=2 width=""100%""align=center></span></div>")
            sbrView.Append("<br/><br/>")
            sbrView.Append("This is an automated message sent to you by <B>Joint Tank Services FZCO</B>. For any further clarification/information, please communicate with us on ops@jts.ae<mailto:ops@jts.ae>")
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex)
        End Try
        Return sbrView.ToString
    End Function
#End Region

#Region "Sending Email using chilkat component with attachments"

    Public Function pub_Send_EmailWithAttachment(ByVal pvt_strFromMailID As String, _
                                                  ByVal bv_strToMailIDs As String, _
                                                  ByVal bv_strBCCEmailIDs As String, _
                                                  ByVal bv_strSubject As String, _
                                                  ByVal bv_strBody As String, _
                                                  ByVal bv_strAttachmentPath As String, _
                                                  ByVal bv_blnResend As Boolean, _
                                                  Optional ByVal bulkattachemnt As ArrayList = Nothing) As Boolean

        Dim objChilkat As New Chilkat.MailMan
        Dim objEmail As New Chilkat.Email
        Dim Status As Boolean
        Try
            objChilkat.UnlockComponent("UiinterchMAILQ_FmPBEfzL8SkF")
            objChilkat.SmtpHost = Config.pub_GetAppConfigValue("SmtpMailServer")

            'Adding From Email ID
            If pvt_strFromMailID = "" Then
                objEmail.From = Config.pub_GetAppConfigValue("FromEmailID")
            Else
                'Adding From EmailID
                objEmail.From = pvt_strFromMailID
            End If

            If Config.pub_GetAppConfigValue("SmtpAuthEnabled") = "true" Then
                objChilkat.SmtpUsername = Config.pub_GetAppConfigValue("SmtpUserName")
                objChilkat.SmtpPassword = Config.pub_GetAppConfigValue("SmtpPassword")
            End If

            If Config.pub_GetAppConfigValue("SmtpPortNo") IsNot Nothing Then
                objChilkat.SmtpPort = Config.pub_GetAppConfigValue("SmtpPortNo")
            End If

            'Adding TO EmailIDs
            Dim strToEmailIds As String()

            Dim macTo As New System.Net.Mail.MailAddressCollection
            strToEmailIds = bv_strToMailIDs.Split(CChar(","))

            If strToEmailIds.Length = 0 Then
                strToEmailIds(0) = bv_strToMailIDs
            End If

            For Each strToEmail As String In strToEmailIds
                objEmail.AddTo(strToEmail, strToEmail)
            Next

            'Adding CC EmailIDs
            Dim strBCCEmailIds As String()
            strBCCEmailIds = bv_strBCCEmailIDs.Split(CChar(","))

            If strBCCEmailIds.Length = 0 Then
                strBCCEmailIds(0) = bv_strBCCEmailIDs
            End If

            For Each strBCCEmail As String In strBCCEmailIds
                objEmail.AddBcc(strBCCEmail, strBCCEmail)
            Next

            'Email Subject
            objEmail.Subject = bv_strSubject
            Dim strBody As String()
            strBody = bv_strBody.Split(vbCrLf)
            If strBody.Length = 0 Then
                strBody(0) = bv_strBody
            End If

            'Adding Foot note
            Dim sbrFootNote As New StringBuilder
            sbrFootNote.Append("<DIV style=""font-family:verdana;font-size:8pt"">")
            sbrFootNote.Append("<DIV>")
            For Each strBodyemail As String In strBody
                sbrFootNote.Append(strBodyemail)
                sbrFootNote.Append("<br/>")
            Next
            sbrFootNote.Append("</DIV>")
            objEmail.SetHtmlBody(sbrFootNote.ToString())

            If bv_blnResend = False Then
                If Not bulkattachemnt Is Nothing Then
                    Dim iCnt As Integer = bulkattachemnt.Count - 1
                    For i = 0 To iCnt
                        If FileExists(bulkattachemnt(i)) Then _
                          objEmail.AddFileAttachment(bulkattachemnt(i))
                    Next
                End If
            Else
                objEmail.AddFileAttachment(bulkattachemnt(0))
            End If

            Status = objChilkat.SendEmail(objEmail)
            If Not Status Then
                iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, objChilkat.LastErrorText)
            End If

            Return Status
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        Finally
            objChilkat = Nothing
        End Try

    End Function

#End Region

#Region "FileExists"
    Private Function FileExists(ByVal FileFullPath As String) As Boolean
        Try
            If Trim(FileFullPath) = "" Then Return False
            Dim f As New IO.FileInfo(FileFullPath)
            Return f.Exists
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "pvt_GenerateEstimateReportPDF"
    Private Sub pvt_GenerateEstimateReportPDF(ByVal bv_ds As DataSet, ByVal bv_strActivityName As String, ByRef br_strFileName As String)
        Try
            'pvt_Diagnostic(String.Concat("RepairWorder - End : ", bv_strActivityName))
            Dim strEstimateFileName As String = String.Empty
            Dim strWOrderFileName As String = String.Empty
            strEquipId = bv_ds.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows(0).Item(RepairEstimateData.EQPMNT_NO)
            bv_strStatus = bv_ds.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows(0).Item(RepairEstimateData.EQPMNT_STTS_CD)
            If bv_strStatus = strReapirEstimate Then
                'pvt_Diagnostic(String.Concat(bv_strStatus, " - Start : ", bv_strActivityName))
                strEstimateFileName = String.Concat("Estimate_RepairEstimate_", strEquipId, "_", DateTime.Now.ToFileTime())
                'strWOrderFileName = String.Concat("Estimate_RepairWorkOrder_", strEquipId, "_", DateTime.Now.ToFileTime())
                'pvt_Diagnostic(String.Concat(bv_strStatus, " - End : ", bv_strActivityName, strEstimateFileName, strWOrderFileName))
            ElseIf bv_strStatus = strReapirApproval Then
                'pvt_Diagnostic(String.Concat(bv_strStatus, " - Start : ", bv_strActivityName))
                strEstimateFileName = String.Concat("Approval_RepairEstimate_", strEquipId, "_", DateTime.Now.ToFileTime())
                'strWOrderFileName = String.Concat("Approval_RepairWorkOrder_", strEquipId, "_", DateTime.Now.ToFileTime())
                'pvt_Diagnostic(String.Concat(bv_strStatus, " - End : ", bv_strActivityName, strEstimateFileName, strWOrderFileName))
            ElseIf bv_strStatus = strRepairCompletion Then
                strEstimateFileName = String.Concat("Completion_RepairEstimate_", strEquipId, "_", DateTime.Now.ToFileTime())
            End If
            pvt_GenerateReport(strEstimateFileName, strEstimateRDLC, bv_ds, bv_strActivityName, "Regular")
            br_strFileName = String.Concat(strEstimateFileName, ".pdf")
            'pvt_Diagnostic(String.Concat("GenerateReport - End : ", bv_strActivityName))
            arr_FileList.Add(String.Concat(strReportFolder, strEstimateFileName, ".pdf"))
            'pvt_GenerateReport(strWOrderFileName, strWorkOrderRDLC, bv_ds, bv_strActivityName, "Regular")
            'arr_FileList.Add(String.Concat(strReportFolder, strWOrderFileName, ".pdf"))            
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat(ex, "Inner Exception : ", ex.InnerException))
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_GenerateCompiledEstimateReportPDF"
    Private Sub pvt_GenerateCompiledEstimateReportPDF(ByVal bv_strSNO As String, ByVal CustomerID As String, ByVal bv_strUserName As String, _
                                                      ByVal bv_intDepotID As Integer)
        Try
            pvt_Diagnostic("GenerateCompiledEstimateReportPDF - Start : ")
            Dim dsCompiledEstimate As New Bulk_EmailDataSet

            Dim dtCustomer As New DataTable
            Dim dtSummary As New DataTable
            Dim dtEstimate As New DataTable


            dtCustomer = objBulk_mail.getRepairWorkOrderCustomerDetails(CustomerID, bv_intDepotID).Tables(Bulk_EmailData._REPAIR_WORKORDER_CUSTOMER)
            dsCompiledEstimate.Merge(dtCustomer)
            dtEstimate = objBulk_mail.getRepairWorkOrderCompiledDetails(bv_strSNO).Tables(Bulk_EmailData._REPAIR_WORKORDER_COMPILEDESTIMATE)
            dsCompiledEstimate.Merge(dtEstimate)
            dtSummary = objBulk_mail.getRepairWorkOrderSummary(bv_strSNO).Tables(Bulk_EmailData._REPAIR_WORKORDER_SUMMARY)
            dsCompiledEstimate.Merge(dtSummary)
            'pvt_Diagnostic("GenerateCompiledEstimateReportPDF - End : ")
            Dim strEstimateFileName As String = String.Empty
            strEstimateFileName = String.Concat("RE_", dtCustomer.Rows(0).Item(Bulk_EmailData.CSTMR_NAM).ToString, "_", DateTime.Now.ToFileTime())
            pvt_GenerateReport(strEstimateFileName, strCompiledEstimateRDLC, dsCompiledEstimate, "Estimate", "Compiled")
            arr_FileList.Add(String.Concat(strReportFolder, strEstimateFileName, ".pdf"))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat(ex, "Inner Exception : ", ex.InnerException))
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_GenerateCleaningReportPDF"
    Private Sub pvt_GenerateCleaningReportPDF(ByVal bv_dsCleaning As DataSet, ByVal bv_strActivityName As String, _
                                               ByRef br_strFileName As String)
        Try
            'pvt_Diagnostic(String.Concat("GenerateCleaningReportPDF - End : ", bv_strActivityName))
            strEquipId = bv_dsCleaning.Tables(CleaningData._V_CLEANING).Rows(0).Item(CleaningData.EQPMNT_NO)
            Dim strCleaningCertificateFileName As String
            strCleaningCertificateFileName = String.Concat("Cleaning_Certificate_", strEquipId, "_", DateTime.Now.ToFileTime())
            br_strFileName = String.Concat(strCleaningCertificateFileName, ".pdf")
            pvt_GenerateReport(strCleaningCertificateFileName, strCleaningRDLC, bv_dsCleaning, bv_strActivityName, "Regular")
            arr_FileList.Add(String.Concat(strReportFolder, strCleaningCertificateFileName, ".pdf"))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat(ex, "Inner Exception : ", ex.InnerException))
            Throw ex
        End Try
    End Sub
#End Region

#Region "Report Methods"

#Region "Generate Report"
    Private Sub pvt_GenerateReport(ByVal strOutputFileName As String, _
                                   ByVal bv_strRDLCName As String, _
                                   ByRef br_dsReport As DataSet, _
                                   ByVal bv_strActivityName As String, _
                                   ByVal bv_strReportType As String)
        Try
            pvt_Diagnostic(String.Concat("Generate Report : ", bv_strActivityName))
            rptViewer = New LocalReport

            For Each dtInvoice As DataTable In br_dsReport.Tables
                rptViewer.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource(dtInvoice.TableName, dtInvoice))
            Next
            rptViewer.ReportPath = String.Concat(strReportFolder, bv_strRDLCName)
            SetLocalReportParameter(rptViewer)
            rptViewer.EnableHyperlinks = True
            pvt_GeneratePDFReport(strOutputFileName)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat(ex, "Inner Exception : ", ex.InnerException))
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_GeneratePDFReport"
    Private Sub pvt_GeneratePDFReport(ByVal bv_rptName As String)
        Try
            'pvt_Diagnostic("Generate PDF")
            Dim strDestFilePath As String = String.Concat(strBulkEmailResendFolder, bv_rptName, ".pdf")
            pvt_Export(bv_rptName, rptViewer, "PDF")
            Dim dataToRead As Long
            Dim iStream As Stream = m_streams(0)
            Dim buffer(10000) As Byte
            Dim length As Integer
            dataToRead = iStream.Length

            Dim strFileName As String = String.Concat(GenerateFileName(bv_rptName), ".pdf")
            Dim objFileinfo As New FileInfo(strFileName)
            If objFileinfo.Exists Then
                objFileinfo.Delete()
            End If
            Dim fsStream As FileStream = File.OpenWrite(String.Concat(strFileName))

            While dataToRead > 0
                length = iStream.Read(buffer, 0, 10000)
                fsStream.Write(buffer, 0, length)
                ReDim buffer(10000) ' Clear the buffer
                dataToRead = dataToRead - length
            End While
            fsStream.Close()
            If File.Exists(strDestFilePath) Then
                File.Delete(strDestFilePath)
            End If
            File.Copy(strFileName, strDestFilePath)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat(ex, "Inner Exception : ", ex.InnerException))
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_Export"
    Private Sub pvt_Export(ByVal bv_rptName As String, _
                            ByVal report As LocalReport, _
                            ByVal format As String)
        Try
            Dim s As String
            s = 1
            Dim deviceInfo As String
            If UCase(format) = "PDF" Then
                deviceInfo = "<DeviceInfo>" + _
                    "  <OutputFormat>PDF</OutputFormat>" + _
                    "  <PageWidth>8.5in</PageWidth>" + _
                    "  <PageHeight>11.5in</PageHeight>" + _
                    "  <MarginTop>0.25in</MarginTop>" + _
                    "  <MarginLeft>0.25in</MarginLeft>" + _
                    "  <MarginRight>0.25in</MarginRight>" + _
                    "  <MarginBottom>0.25in</MarginBottom>" + _
                    "</DeviceInfo>"
            ElseIf UCase(format) = "JPG" Then
                deviceInfo = "<DeviceInfo>" + _
                    "<OutputFormat>JPG</OutputFormat>" + _
                    "  <PageWidth>9.5in</PageWidth>" + _
                    "  <PageHeight>11in</PageHeight>" + _
                    "  <MarginTop>0.25in</MarginTop>" + _
                    "  <MarginLeft>0.25in</MarginLeft>" + _
                    "  <MarginRight>0.25in</MarginRight>" + _
                    "  <MarginBottom>0.25in</MarginBottom>" + _
                    "</DeviceInfo>"
            Else
                deviceInfo = "<DeviceInfo>" + _
                    "  <OutputFormat>EMF</OutputFormat>" + _
                    "  <PageWidth>8.5in</PageWidth>" + _
                    "  <PageHeight>11.5in</PageHeight>" + _
                    "  <MarginTop>0.25in</MarginTop>" + _
                    "  <MarginLeft>0.25in</MarginLeft>" + _
                    "  <MarginRight>0.25in</MarginRight>" + _
                    "  <MarginBottom>0.25in</MarginBottom>" + _
                    "  <StartPage>" + s + "</StartPage>" + _
                    "  <EndPage>" + s + "</EndPage>" + _
                    "</DeviceInfo>"
            End If

            Dim warnings() As Warning = Nothing
            'dispose stream
            If Not (m_streams Is Nothing) Then
                Dim mstream As Stream
                For Each mstream In m_streams
                    mstream.Close()
                Next
                m_streams = Nothing
            End If

            m_streams = New List(Of Stream)()
            report.Render(format, deviceInfo, AddressOf CreateStream, warnings)

            Dim stream As Stream = Nothing
            For Each stream In m_streams
                stream.Position = 0
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat(ex, "Inner Exception : ", ex.InnerException))
            Throw ex
        End Try
    End Sub
#End Region

#Region "GenerateFileName"
    Private Function GenerateFileName(ByVal bv_strRptName As String) As String
        Try
            Dim strFilename As String
            strFilename = String.Concat(strReportFolder, bv_strRptName)
            Return strFilename

        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "CreateStream"
    Private Function CreateStream(ByVal name As String, _
                                  ByVal fileNameExtension As String, _
                                  ByVal encoding As System.Text.Encoding, _
                                  ByVal mimeType As String, _
                                  ByVal willSeek As Boolean) As Stream
        Dim stream As Stream
        Try
            'stream = New FileStream((AppDomain.CurrentDomain.BaseDirectory.ToString + "RDLC\") + name + "." + fileNameExtension, FileMode.Create)
            stream = New FileStream(String.Concat(strReportFolder) + name + "." + fileNameExtension, FileMode.Create)
            m_streams.Add(stream)
            Return stream
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "pvt_Diagnostic"
    Private Sub pvt_Diagnostic(ByVal bv_Message As String)
        If CBool(blnDiagnostic) Then
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, bv_Message)
        End If
    End Sub
#End Region

#Region "SetLocalReportParameter"
    Private Sub SetLocalReportParameter(ByVal rptLocalReport As Microsoft.Reporting.WinForms.LocalReport)
        Dim rptParamCol As New List(Of Microsoft.Reporting.WinForms.ReportParameter)()
        Try
            Dim strLogoURL As String = String.Empty
            Dim str_007EIRNo As String = String.Empty
            Dim strLogoLeaf As String = String.Empty
            Dim strLogoCouncil As String = String.Empty
            Dim bln_007EIRNo_Key As Boolean = False
            If dtDepot.Rows.Count > 0 Then
                If Not IsDBNull(dtDepot.Rows(0).Item(Bulk_EmailData.CMPNY_LG_PTH)) Then
                    Dim strAppDomainAppPath As String = pvt_strDomainPath
                    strLogoURL = String.Concat("file:///", Config.pub_GetAppConfigValue("UploadPhotoPath").Replace("../", ""), Config.pub_GetAppConfigValue("JTSLogo").Replace("..\", ""))
                    strLogoLeaf = String.Concat("file:///", Config.pub_GetAppConfigValue("UploadPhotoPath").Replace("../", ""), Config.pub_GetAppConfigValue("LeafPhoto").Replace("..\", ""))
                    strLogoCouncil = String.Concat("file:///", Config.pub_GetAppConfigValue("UploadPhotoPath").Replace("../", ""), Config.pub_GetAppConfigValue("CouncilPhoto").Replace("..\", ""))
                End If
            End If

            Dim prmLogoURL As New ReportParameter
            Dim prmEirNo As New ReportParameter
            Dim prmUserName As New ReportParameter
            Dim prmPhotoURL As New ReportParameter
            Dim prmLogoLeaf As New ReportParameter
            Dim prmLogoCouncil As New ReportParameter

            Dim bln024Key As Boolean = False
            Dim bln025Key As Boolean = False
            Dim strUserName As String = String.Empty
            'Dim str024Value As String = objCommon.GetConfigSetting("024", bln024Key)
            'Dim str025Value As String = objCommon.GetConfigSetting("025", bln025Key)
            rptLocalReport.EnableExternalImages = True
            If Not strUserName Is Nothing Then
                strUserName = (dsBulk_Email.Tables(Bulk_EmailData._V_BULK_EMAIL).Rows(0).Item(Bulk_EmailData.CRTD_BY).ToString)
            Else
                strUserName = "Admin"
            End If
            If Not IsNothing(strLogoURL) Then
                prmLogoURL = New ReportParameter("logourl", strLogoURL)
            End If

            If Not IsNothing(strLogoLeaf) Then
                prmLogoLeaf = New ReportParameter("LogoLeaf", strLogoLeaf)
            End If

            If Not IsNothing(strLogoCouncil) Then
                prmLogoCouncil = New ReportParameter("LogoCouncil", strLogoCouncil)
            End If
            str_007EIRNo = objCommonConfig.pub_GetConfigSingleValue("007", intDepotID)
            bln_007EIRNo_Key = objCommonConfig.IsKeyExists

            If bln_007EIRNo_Key Then
                prmEirNo = New ReportParameter("EirNo", str_007EIRNo)
            End If
            prmUserName = New ReportParameter("username", strUserName)
            rptLocalReport.EnableHyperlinks = True
            prmPhotoURL = New ReportParameter("PhotoParameterUrl", Config.pub_GetAppConfigValue("PhotoViewerURl"))

            Dim rptPC As ReportParameterInfoCollection
            rptPC = rptLocalReport.GetParameters()
            rptLocalReport.SetParameters(New ReportParameter() {prmLogoURL, prmEirNo, prmUserName, prmPhotoURL, prmLogoLeaf, prmLogoCouncil})
            rptLocalReport.Refresh()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub

#End Region

#End Region
End Class