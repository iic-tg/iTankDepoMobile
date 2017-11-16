Imports iInterchange.iTankDepo.Business.Services
Imports iInterchange.iTankDepo.Business.Common
Imports iInterchange.iTankDepo.Business.Admin
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.Data
Imports System.Configuration
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Web
Public Class DARService

#Region "Declaration"
    Dim dsDAR As New DARDataSet
    Dim objDAR As New DAR
    Dim rptDARLocalReport As New LocalReport
    Private m_streams As IList(Of Stream)
    Dim i64DepotID As Integer = ConfigurationSettings.AppSettings("Depot")
    Private strLogoURL As String = ConfigurationSettings.AppSettings("LogoURL")
    Private strstatusRDLCName As String = ConfigurationSettings.AppSettings("statusRDLCName")
    Dim strFormat As String = UCase(ConfigurationManager.AppSettings("Format"))
    Dim ReportPath As String = ConfigurationSettings.AppSettings("ReportPath")
    Dim strFromMailID As String = ConfigurationSettings.AppSettings("FromMailID")
    Dim strDiagnostic As String = Config.pub_GetAppConfigValue("diagnostic")
    Dim dsEmailSetting As New DARDataSet
    Dim i32PRDC_FLTR_ID As Int32
    Dim i32PRDC_DT As Int32
    Dim i32PRDC_DY As Int32
    Dim strGNRTN_TM As String
    Dim dtNxtRun As Date
    Dim dtNxtRunUpdate As Date
    Dim intRprtId As Integer
    Dim blnStart As Boolean = True
#End Region

#Region "btnStart_Click"

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        tmrDAR.Start()
    End Sub

#End Region

#Region "tmrDAR_Elapsed"

    Private Sub tmrDAR_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrDAR.Elapsed
        Try

            If blnStart = True Then
                blnStart = False
                SyncLock tmrDAR
                    Dim dtNextRnDate As DateTime
                    Dim blnGnrtnday As Boolean = "False"
                    pvt_Diagnostic("Elapsed start")
                    pvt_Diagnostic("Boolean Flag: " & blnStart)
                    Dim dtScheduleTime As New DataTable
                    dtScheduleTime.Clear()
                    pvt_Diagnostic("Get Schedule Time Query Started..")
                    dtScheduleTime = objDAR.pub_GetScheduleTime(i64DepotID).Tables(DARData._V_EMAIL_SETTING)
                    pvt_Diagnostic("Get Schedule Time Query End..")
                    dsEmailSetting.Merge(dtScheduleTime)
                    If dtScheduleTime.Rows.Count > 0 Then
                        pvt_Diagnostic("Get Email Settings Started..")
                        dsEmailSetting = objDAR.pub_GetEmailSetting(i64DepotID)
                        pvt_Diagnostic("Get Email Settings End..")
                        For Each drScheduleTime As DataRow In dtScheduleTime.Rows
                            For Each drEmailService As DataRow In dsEmailSetting.Tables(DARData._V_CUSTOMER_EMAIL_SETTING).Select(DARData.CSTMR_ID & "=" & drScheduleTime.Item(DARData.CSTMR_ID))

                                i32PRDC_FLTR_ID = drEmailService.Item("PRDC_FLTR_ID")
                                If Not IsDBNull(drEmailService.Item("PRDC_DT_ID")) Then
                                    i32PRDC_DT = drEmailService.Item("PRDC_DT_ID")
                                End If
                                If Not IsDBNull(drEmailService.Item("PRDC_DY_ID")) Then
                                    i32PRDC_DY = drEmailService.Item("PRDC_DY_ID")
                                End If

                                dtNxtRun = drEmailService.Item("NXT_Rn_DT_TM")
                                strGNRTN_TM = drEmailService.Item("GNRTN_TM").ToString
                                intRprtId = drEmailService.Item("RPRT_ID").ToString

                                If i32PRDC_FLTR_ID = 33 Then
                                    dtNextRnDate = Now.Date & " " & strGNRTN_TM
                                    blnGnrtnday = "True"
                                ElseIf i32PRDC_FLTR_ID = 34 Then
                                    If dtNxtRun.Date = Now.Date Then
                                        dtNextRnDate = Now.Date & " " & strGNRTN_TM
                                        blnGnrtnday = "True"
                                    ElseIf dtNxtRun.Date < Now.Date And DateDiff("d", dtNxtRun.Date, Now.Date) > i32PRDC_DT Then
                                        dtNextRnDate = Now.Date & " " & strGNRTN_TM
                                        blnGnrtnday = "True"
                                    End If

                                ElseIf i32PRDC_FLTR_ID = 35 Then
                                    If i32PRDC_DY = 37 And Today.DayOfWeek = 0 Then
                                        dtNextRnDate = Now.Date & " " & strGNRTN_TM
                                        blnGnrtnday = "True"
                                    ElseIf i32PRDC_DY = 38 And Today.DayOfWeek = 1 Then
                                        dtNextRnDate = Now.Date & " " & strGNRTN_TM
                                        blnGnrtnday = "True"
                                    ElseIf i32PRDC_DY = 39 And Today.DayOfWeek = 2 Then
                                        dtNextRnDate = Now.Date & " " & strGNRTN_TM
                                        blnGnrtnday = "True"
                                    ElseIf i32PRDC_DY = 40 And Today.DayOfWeek = 3 Then
                                        dtNextRnDate = Now.Date & " " & strGNRTN_TM
                                        blnGnrtnday = "True"
                                    ElseIf i32PRDC_DY = 41 And Today.DayOfWeek = 4 Then
                                        dtNextRnDate = Now & " " & strGNRTN_TM
                                        blnGnrtnday = "True"
                                    ElseIf i32PRDC_DY = 42 And Today.DayOfWeek = 5 Then
                                        dtNextRnDate = Now.Date & " " & strGNRTN_TM
                                        blnGnrtnday = "True"
                                    ElseIf i32PRDC_DY = 43 And Today.DayOfWeek = 6 Then
                                        dtNextRnDate = Now.Date & " " & strGNRTN_TM
                                        blnGnrtnday = "True"
                                    End If
                                Else
                                    If i32PRDC_DT = Today.Day Then
                                        dtNextRnDate = Now.Date & " " & strGNRTN_TM
                                        blnGnrtnday = "True"
                                    End If
                                End If
                                If Now >= dtNextRnDate And blnGnrtnday = "True" Then
                                    pvt_Diagnostic("Elapsed start")
                                    Select Case intRprtId
                                        Case 31
                                            pvt_GetDAR(drEmailService)
                                            'Case 32
                                            '    pvt_GetInventory(drEmailService)
                                            'Case 44
                                            '    pvt_GetGatein(drEmailService)
                                            'Case 45
                                            '    pvt_GetGateout(drEmailService)
                                    End Select
                                End If
                                blnGnrtnday = "False"
                            Next
                        Next
                    End If
                End SyncLock
                pvt_Diagnostic("Email tmrDAR_Elapsed End..")
                blnStart = True
            End If
        Catch ex As Exception
            blnStart = True
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region
#Region "pvt_GetDAR"
    Private Sub pvt_GetDAR(ByRef br_drCustomerEmailSetting As DataRow)
        Try
            Dim i64CustomerID As Integer
            Dim cstmr_email_id As Integer
            Dim objCommonUI As New CommonUI
            Dim dsCommon As New CommonUIDataSet
            i64CustomerID = br_drCustomerEmailSetting.Item(DARData.CSTMR_ID)
            cstmr_email_id = br_drCustomerEmailSetting.Item(DARData.CSTMR_EML_STTNG_BIN)
            dsCommon.Tables(CommonUIData._V_DAR_ACTIVITY_STATUS).Rows.Clear()
            dsCommon.Tables(CommonUIData._V_DAR_CUSTOMER_SUMMARY).Rows.Clear()
            dsCommon.Tables(CommonUIData._V_DAR_TYPE_SUMMARY).Rows.Clear()

            Dim strParam As String = String.Concat("Customer='", i64CustomerID, "'&Next Test Type='51','52'&Out Date From=", Now.AddDays(-2).ToString("dd-MMM-yyyy"), "&Out Date To=", Now.ToString("dd-MMM-yyyy"))

            dsCommon = objCommonUI.GetStatusReport(strParam, i64DepotID, _
                                               "Inventory")
            pvt_GenerateReport(strstatusRDLCName, dsCommon, _
                               br_drCustomerEmailSetting, "Landscape")
            dtNxtRunUpdate = pub_getNextRunDateTime(i32PRDC_FLTR_ID, strGNRTN_TM, i32PRDC_DT)
            pvt_Diagnostic(String.Concat("Customer Mail Date Update Started (Next Run Date Time).. Customer ID: ", i64CustomerID.ToString, "Depot ID: ", i64DepotID.ToString, "Report ID: ", intRprtId.ToString, "Next Run Update Time: ", dtNxtRunUpdate.ToString, "Mail ID ", cstmr_email_id.ToString))
            objDAR.pub_UpdateCustomerEmailSetting(i64DepotID, i64CustomerID, intRprtId, dtNxtRunUpdate, cstmr_email_id)
            pvt_Diagnostic("Customer Mail Date Update End (Next Run Date Time)..")
        Catch ex As Exception
            blnStart = True
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "pub_getNextRunDateTime()"

    Private Function pub_getNextRunDateTime(ByVal intPRDC_FLTR_ID As Integer, ByVal strGNRTN_TM As String, ByVal intPRDC_DT_ID As Integer) As DateTime
        Try
            Dim today As Date = Date.Today
            pvt_Diagnostic("GET: Next Run Date Time Function Started..")
            If i32PRDC_FLTR_ID = 33 Then
                dtNxtRunUpdate = today.AddDays(1)
                dtNxtRunUpdate = dtNxtRunUpdate.AddHours(dtNxtRun.Hour)
                dtNxtRunUpdate = dtNxtRunUpdate.AddMinutes(dtNxtRun.Minute)
                dtNxtRunUpdate = dtNxtRunUpdate.AddSeconds(dtNxtRun.Second)
            ElseIf i32PRDC_FLTR_ID = 34 Then
                dtNxtRunUpdate = today.AddDays(i32PRDC_DT)
                dtNxtRunUpdate = dtNxtRunUpdate.AddHours(dtNxtRun.Hour)
                dtNxtRunUpdate = dtNxtRunUpdate.AddMinutes(dtNxtRun.Minute)
                dtNxtRunUpdate = dtNxtRunUpdate.AddSeconds(dtNxtRun.Second)
            ElseIf i32PRDC_FLTR_ID = 35 Then
                Dim dayDiff As Integer = today.DayOfWeek - i32PRDC_DT
                Dim NxtRnDat As Date = today.AddDays(-dayDiff)
                dtNxtRunUpdate = today.AddDays(7)
                dtNxtRunUpdate = dtNxtRunUpdate.AddHours(dtNxtRun.Hour)
                dtNxtRunUpdate = dtNxtRunUpdate.AddMinutes(dtNxtRun.Minute)
                dtNxtRunUpdate = dtNxtRunUpdate.AddSeconds(dtNxtRun.Second)
            Else
                If today.Day >= i32PRDC_DT AndAlso DateTime.Now > CDate(New DateTime(Now.Year, Now.Month, i32PRDC_DT) & " " & strGNRTN_TM) Then
                    If i32PRDC_DT > 28 Then
                        dtNxtRunUpdate = CDate(New DateTime(Now.Year, Now.Month, i32PRDC_DT) & " " & strGNRTN_TM).AddMonths(1).AddDays(-1)
                    Else
                        dtNxtRunUpdate = CDate(New DateTime(Now.Year, Now.Month + 1, i32PRDC_DT) & " " & strGNRTN_TM)
                    End If
                Else
                    If i32PRDC_DT > 28 Then
                        dtNxtRunUpdate = CDate(New DateTime(Now.Year, Now.Month, 1) & " " & strGNRTN_TM).AddMonths(1).AddDays(-1)
                    Else
                        dtNxtRunUpdate = CDate(New DateTime(Now.Year, Now.Month, i32PRDC_DT) & " " & strGNRTN_TM)
                    End If
                End If
            End If
            pvt_Diagnostic("GET: Next Run Date Time Function End.. Next Run Update: " & dtNxtRunUpdate)
            Return dtNxtRunUpdate

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
            blnStart = True
        End Try
    End Function
#End Region

#Region "Generate Report"

    Private Sub pvt_GenerateReport(ByVal bv_strRDLCName As String, ByRef br_dsReport As DataSet, ByRef br_drCustomerEmailSetting As DataRow, ByVal bv_reportFormat As String)
        Try
            rptDARLocalReport = New LocalReport
            For Each dtReport As DataTable In br_dsReport.Tables
                rptDARLocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource(dtReport.TableName, dtReport))
            Next

            rptDARLocalReport.ReportPath = String.Concat(ReportPath, bv_strRDLCName, ".rdlc")

            'rptDARLocalReport.ReportPath = String.Concat(AppDomain.CurrentDomain.BaseDirectory.ToString, "RDLC\", bv_strRDLCName)
            pvt_Diagnostic("Excel File Generated Started..")
            SetLocalReportParameter(rptDARLocalReport)
            If strFormat = "EXCEL" Then
                pvt_Export(bv_strRDLCName, rptDARLocalReport, "EXCEL", bv_reportFormat)
            ElseIf strFormat = "PDF" Then
                pvt_Export(bv_strRDLCName, rptDARLocalReport, "PDF", bv_reportFormat)
            End If

            Dim dataToRead As Long
            Dim iStream As Stream = m_streams(0)
            Dim buffer(10000) As Byte
            Dim length As Integer
            dataToRead = iStream.Length
            Dim strFileName As String = String.Empty
            If strFormat = "EXCEL" Then
                strFileName = GenerateFileName(String.Concat(bv_strRDLCName, "_", Now.ToString("ddMMyyyy"), Now.ToString("hhmmss"), ".xls"))
            ElseIf strFormat = "PDF" Then
                strFileName = GenerateFileName(String.Concat(bv_strRDLCName, "_", Now.ToString("ddMMyyyy"), Now.ToString("hhmmss"), ".pdf"))
            End If
            Dim filename As String = Path.GetFileName(strFileName)
            Dim fsStream As FileStream = File.OpenWrite(strFileName)

            While dataToRead > 0
                length = iStream.Read(buffer, 0, 10000)
                fsStream.Write(buffer, 0, length)
                ReDim buffer(10000) ' Clear the buffer
                dataToRead = dataToRead - length
            End While
            fsStream.Close()
            'To activate the Mail Service
            Dim strToMailID As String
            Dim strSubject As String
            Dim strCCMailId As String
            Dim strBCCMailId As String
            strToMailID = br_drCustomerEmailSetting.Item(DARData.TO_EML)
            If Not br_drCustomerEmailSetting.Item(DARData.CC_EML) Is DBNull.Value Then
                strCCMailId = br_drCustomerEmailSetting.Item(DARData.CC_EML)
            Else
                strCCMailId = String.Empty
            End If

            strBCCMailId = String.Empty
            If Not br_drCustomerEmailSetting.Item(DARData.SBJCT_VCR) Is DBNull.Value Then
                strSubject = br_drCustomerEmailSetting.Item(DARData.SBJCT_VCR)
            Else
                strSubject = String.Empty
            End If
            Dim blnStatus As Boolean = False
            blnStatus = pvt_SendMail(filename, strToMailID, strCCMailId, strBCCMailId, strSubject)
            If blnStatus = True Then
                pub_writeSentMailLog(br_drCustomerEmailSetting.Item(DARData.CSTMR_CD))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message, " Inner Exception : ", ex.InnerException))
            blnStart = True
        End Try
    End Sub

#End Region

#Region "pvt_Export"
    Private Sub pvt_Export(ByVal bv_rptName As String, _
                            ByVal objReport As LocalReport, _
                            ByVal format As String, ByVal reportFormat As String)
        Try
            Dim deviceInfo As String = String.Empty
            If reportFormat = "Portrait" Then

                If UCase(format) = "PDF" Then
                    deviceInfo = "<DeviceInfo>" + _
                                   "  <OutputFormat>PDF</OutputFormat>" + _
                                   "  <PageWidth>9in</PageWidth>" + _
                                   "  <PageHeight>11in</PageHeight>" + _
                                   "  <MarginTop>0.25in</MarginTop>" + _
                                   "  <MarginLeft>0.25in</MarginLeft>" + _
                                   "  <MarginRight>0.25in</MarginRight>" + _
                                   "  <MarginBottom>0.25in</MarginBottom>" + _
                                   "</DeviceInfo>"

                ElseIf UCase(format) = "EXCEL" Then
                    deviceInfo = "<DeviceInfo> <SimplePageHeaders>False</SimplePageHeaders></DeviceInfo>"

                End If



            ElseIf reportFormat = "Landscape" Then
                If UCase(format) = "PDF" Then
                    deviceInfo = "<DeviceInfo>" + _
                                 "  <OutputFormat>PDF</OutputFormat>" + _
                                 "  <PageWidth>11.5in</PageWidth>" + _
                                 "  <PageHeight>8.5in</PageHeight>" + _
                                 "  <MarginTop>0.25in</MarginTop>" + _
                                 "  <MarginLeft>0.25in</MarginLeft>" + _
                                 "  <MarginRight>0.25in</MarginRight>" + _
                                 "  <MarginBottom>0.25in</MarginBottom>" + _
                                 "</DeviceInfo>"
                ElseIf UCase(format) = "EXCEL" Then
                    deviceInfo = "<DeviceInfo> <SimplePageHeaders>False</SimplePageHeaders></DeviceInfo>"
                End If
            End If

            Dim warnings() As Microsoft.Reporting.WinForms.Warning = Nothing
            'dispose stream
            If Not (m_streams Is Nothing) Then
                Dim mstream As Stream
                For Each mstream In m_streams
                    mstream.Close()
                Next
                m_streams = Nothing
            End If

            m_streams = New List(Of Stream)()
            objReport.Render(format, deviceInfo, AddressOf CreateStream, warnings)

            Dim stream As Stream = Nothing
            For Each stream In m_streams
                stream.Position = 0
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message, " Inner Exception : ", ex.InnerException))
            blnStart = True
        End Try
    End Sub

#End Region

#Region "CreateStream"
    Private Function CreateStream(ByVal name As String, _
                                  ByVal fileNameExtension As String, _
                                  ByVal encoding As System.Text.Encoding, _
                                  ByVal mimeType As String, _
                                  ByVal willSeek As Boolean) As Stream
        Dim stream As Stream
        Try
            Dim strFileName As String = String.Concat(Config.pub_GetAppConfigValue("FilePath"), name, ".pdf")

            stream = New FileStream(strFileName, FileMode.Create)
            m_streams.Add(stream)

            Return stream
            stream.Close()
        Catch ex As Exception
            blnStart = True
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Function
#End Region

#Region "GenerateFileName"

    Private Function GenerateFileName(ByVal bv_strRptName As String) As String
        Try
            Dim strFilename As String
            Dim strDestinationPath As String = Config.pub_GetAppConfigValue("ReportFolder")
            strFilename = String.Concat(strDestinationPath, bv_strRptName)
            Return strFilename
        Catch ex As Exception
            blnStart = True
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function

#End Region

#Region "pvt_SendMail"

    Private Function pvt_SendMail(ByVal strFileName As String, ByVal bv_strToMailID As String, ByVal bv_strCCMailID As String, ByVal bv_strBCCMailID As String, ByVal bv_strMailSubject As String) As Boolean
        Try
            'Dim objZipFile As New ZipFile
            Dim strDestinationPath As String = Config.pub_GetAppConfigValue("ReportFolder")
            Dim sbBody As New StringBuilder
            sbBody.Append("Dear User, " & vbCrLf)
            sbBody.Append("<br/><br/>")
            If strFileName.Contains("StatusReport") Then
                'sbBody.Append("Please find the attached document activity wise daily report." & vbCrLf)
                sbBody.Append("<br/><br/>")
                sbBody.Append("Please find the Status Report for ")
                sbBody.Append(DateTime.Today.ToString("dd-MMM-yyyy"))
                sbBody.Append(" enclosed here.")
                sbBody.Append("<br/><br/>")
                sbBody.Append("Kindly feel free to contact us if you need any clarification.  ")
                sbBody.Append("<br/><br/>")
                sbBody.Append("<br/><br/>")
                sbBody.Append("Warm Regards,")
                sbBody.Append("<br/><br/>")
                sbBody.Append("Joint Tank Services.")
                sbBody.Append("<div class=MsoNormal align=center style='text-align:center'><span lang=EN-GBstyle='font-size:10.0pt;font-family:""Arial"",""sans-serif"";mso-fareast-font-family:""Times New Roman"";mso-ansi-language:EN-GB'><hr size=2 width=""100%""align=center></span></div>")
                sbBody.Append("<br/><br/>")
                sbBody.Append("This is an automated message sent to you by <B>Joint Tank Services FZCO</B>. For any further clarification/information, please communicate with us on ops@jts.ae<mailto:ops@jts.ae>")
            ElseIf strFileName.Contains("Inventory") Then
                sbBody.Append("Please find the attached document daily report for the current inventory." & vbCrLf)
            End If
            'sbBody.Append("<br/><br/>")
            'sbBody.Append("<div class=MsoNormal align=center style='text-align:center'><span lang=EN-GBstyle='font-size:10.0pt;font-family:""Arial"",""sans-serif"";mso-fareast-font-family:""Times New Roman"";mso-ansi-language:EN-GB'><hr size=2 width=""100%""align=center></span></div>")
            'sbBody.Append("<br/><br/>")
            'sbBody.Append("This message was sent to you for information purposes only . Please do not respond to this message as it is automatically generated.")
            Dim strBody As String = sbBody.ToString
            Dim blnEmailStatus As Boolean = False
            blnEmailStatus = pub_Send_EmailWithAttachment(bv_strToMailID, strBody, bv_strMailSubject, bv_strCCMailID, bv_strBCCMailID, String.Concat(strDestinationPath, strFileName))
            pvt_Diagnostic("Mail Details.. TO Mail: " & bv_strToMailID)
            Return blnEmailStatus

        Catch ex As Exception
            blnStart = True
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message, " Inner Exception : ", ex.InnerException))
        End Try
    End Function

#End Region

#Region "Sending Email using chilkat component with attachments"

    Public Function pub_Send_EmailWithAttachment(ByVal bv_strToMailID As String, ByVal bv_strBody As String, ByVal bv_strSubject As String, _
                                              ByVal bv_strCCMAilID As String, ByVal bv_strBCCMailId As String, ByVal bv_strAttachmentPath As String) As Boolean


        Dim objChilkat As New Chilkat.MailMan
        Dim objEmail As New Chilkat.Email
        Dim Status As Boolean
        Try
            pvt_Diagnostic("Send Email With Attachment Started..")
            objChilkat.UnlockComponent("UiinterchMAILQ_FmPBEfzL8SkF")
            objChilkat.SmtpHost = Config.pub_GetAppConfigValue("SmtpMailServer")
            If Config.pub_GetAppConfigValue("SmtpAuthEnabled") = "true" Then
                objChilkat.SmtpUsername = Config.pub_GetAppConfigValue("SmtpUserName")
                objChilkat.SmtpPassword = Config.pub_GetAppConfigValue("SmtpPassword")
            End If
            If Config.pub_GetAppConfigValue("SmtpPortNo") IsNot Nothing Then
                objChilkat.SmtpPort = Config.pub_GetAppConfigValue("SmtpPortNo")
            End If
            objEmail.From = strFromMailID
            Dim strToEmailIds As String()
            Dim macTo As New System.Net.Mail.MailAddressCollection
            strToEmailIds = bv_strToMailID.Split(CChar(","))

            If strToEmailIds.Length = 0 Then
                strToEmailIds(0) = bv_strToMailID
            End If
            For Each strToEmail As String In strToEmailIds
                objEmail.AddTo(strToEmail, strToEmail)
            Next
            'Adding CC EmailIDs
            Dim strCCEmailIds As String()

            strCCEmailIds = bv_strCCMAilID.Split(CChar(","))

            If strCCEmailIds.Length = 0 Then
                strCCEmailIds(0) = bv_strCCMAilID
            End If
            For Each strCCEmail As String In strCCEmailIds
                objEmail.AddCC(strCCEmail, strCCEmail)
            Next
            'Adding BCC EmailIDs
            Dim strBCCEmailIds As String()

            strBCCEmailIds = bv_strBCCMailId.Split(CChar(","))

            If strBCCEmailIds.Length = 0 Then
                strBCCEmailIds(0) = bv_strBCCMailId
            End If
            For Each strBCCEmail As String In strBCCEmailIds
                objEmail.AddBcc(strBCCEmail, strBCCEmail)
            Next
            objEmail.Subject = bv_strSubject
            'Adding Foot note
            Dim sbrFootNote As New StringBuilder
            sbrFootNote.Append("<DIV style=""font-family:verdana;font-size:8pt"">")
            sbrFootNote.Append("<DIV>")
            sbrFootNote.Append(bv_strBody)
            sbrFootNote.Append("</DIV>")
            objEmail.SetHtmlBody(sbrFootNote.ToString())

            'Adding Email Attachments
            objEmail.AddFileAttachment(bv_strAttachmentPath)
            Try

                Status = objChilkat.SendEmail(objEmail)
                If Not Status Then
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, "Email Error:", objChilkat.LastErrorText)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                objChilkat = Nothing

            End Try
            Return Status
            pvt_Diagnostic("Send Email With Attachment End..")
        Catch ex As Exception
            blnStart = True
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message, " Inner Exception : ", ex.InnerException))
        End Try

    End Function

#End Region

#Region "SetLocalReportParameter"
    Dim rptParamCol As New List(Of Microsoft.Reporting.WinForms.ReportParameter)()
    Private Sub SetLocalReportParameter(ByVal rptDARLocalReport As Microsoft.Reporting.WinForms.LocalReport)
        Try
            Dim objDAR As New DAR
            'Dim id As String = dsDAR.Tables(DARData._V_DEPOT).Rows(0).Item(DARData.DPT_ID)
            dsDAR = objDAR.pub_Get_Depot(i64DepotID)
            If dsDAR.Tables(DARData._V_DEPOT).Rows.Count > 0 Then
                strLogoURL = String.Concat("file:///", ConfigurationManager.AppSettings("UploadPhotoPath").Replace("../", ""), dsDAR.Tables(DARData._V_DEPOT).Rows(0).Item(DARData.CMPNY_LG_PTH))
            End If
            Dim prmLogoURL As New ReportParameter("logourl", strLogoURL)
            Dim rptPC As ReportParameterInfoCollection
            rptDARLocalReport.EnableExternalImages = True
            rptPC = rptDARLocalReport.GetParameters()

            rptDARLocalReport.SetParameters(New ReportParameter() {prmLogoURL})
        Catch ex As Exception
            blnStart = True
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message, " Inner Exception : ", ex.InnerException))
        End Try
    End Sub

#End Region

#Region "pvt_Diagnostic"
    Private Sub pvt_Diagnostic(ByVal bv_Message As String)
        If CBool(strDiagnostic) Then
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, bv_Message)
        End If
    End Sub
#End Region

#Region "SentMailLog"
    Public Shared Sub pub_writeSentMailLog(ByVal bv_strCustomerName As String)
        Try
            Dim strLogFilePath As String = String.Empty
            If Config.pub_GetAppConfigValue("SentMailLogPath") IsNot Nothing Then
                strLogFilePath = Config.pub_GetAppConfigValue("SentMailLogPath")
            End If

            strLogFilePath = String.Concat(strLogFilePath, "SentMailLog", DateTime.Now.ToString("ddMMyyyy"), ".txt")

            Using w As StreamWriter = File.AppendText(strLogFilePath)
                Log(bv_strCustomerName, w)
                w.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Shared Sub Log(ByVal bv_strCustomerName As String, ByVal w As TextWriter)
        Try
            w.Write(ControlChars.CrLf)
            w.WriteLine("Customer Name: {0} | Date: {1} |Time: {2}", _
                    bv_strCustomerName, DateTime.Today.ToString("dd-MMM-yyyy"), System.DateTime.Now.ToString("hh:mm:ss"))
            w.Flush()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
#End Region
End Class
