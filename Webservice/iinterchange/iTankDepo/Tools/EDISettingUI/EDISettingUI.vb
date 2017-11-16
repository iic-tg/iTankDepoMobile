Imports System.IO
Imports System.Text
Imports System.IO.Compression
Imports System.Configuration
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Business.EDI
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Business.Common
Imports iInterchange.iTankDepo.Business.Services

Public Class EDISettingUI

#Region "Declaration"
    Dim intDPT_ID As Integer
    Dim strDPT_CD As String
    Dim strWfdata As String
    Dim arr_FileList As New ArrayList
    Dim strGenerationMode As String = "SERVICE"
    Dim blnDiagnostic As Boolean = False
    Dim objEDISetting As New EDISetting
#End Region

#Region "Start_Click"
    Private Sub Start_Click(sender As System.Object, e As System.EventArgs) Handles Start.Click
        Try
            Dim dsEDISetting As New EDISettingDataSet
            Dim dtDepotId As New DataTable
            Dim creationDateTime As DateTime
            blnDiagnostic = Config.pub_GetAppConfigValue("diagnostic")
            pvt_Diagnostic("Elapsed start")
            '   dsEDISetting = objEDISetting.pvt_GetCustomerEmailFormat()

            Dim dtToday As Date = Now
            Dim mi As String = dtToday.Minute
            If mi < 10 Then
                mi = String.Concat("0", mi)
            End If
            Dim strcrnttime As String = String.Concat(dtToday.Hour, ":", mi)

            If dsEDISetting.Tables(EDISettingData._V_EDI_SETTING_UI).Rows.Count > 0 Then
                For Each drCustomerEmailFormat As DataRow In dsEDISetting.Tables(EDISettingData._V_EDI_SETTING_UI).Rows()
                    Dim strgnrtTime As String = drCustomerEmailFormat.Item(EDISettingData.GNRTN_TM)




                    If strcrnttime.Replace(":", "") >= strgnrtTime.Replace(":", "") Then
                        Dim strFromMailId As String = ConfigurationManager.AppSettings("FromMailId")
                        intDPT_ID = drCustomerEmailFormat.Item(EDISettingData.DPT_ID)
                        strDPT_CD = drCustomerEmailFormat.Item(EDISettingData.DPT_CD)
                        strWfdata = String.Concat(EDISettingData.DPT_ID, "=", intDPT_ID, "&", EDISettingData.DPT_CD, "=", strDPT_CD)
                        If CStr(drCustomerEmailFormat.Item(EDISettingData.EDI_FRMT_CD)).Trim() = "CODECO" Then
                            pvt_GenerateEDICodeco(True, True, True, strWfdata, drCustomerEmailFormat.Item(EDISettingData.CSTMR_CD))
                        ElseIf CStr(drCustomerEmailFormat.Item(EDISettingData.EDI_FRMT_CD)).Trim() = "ANSII" Then
                            pvt_GenerateEDI(True, True, True, strWfdata, drCustomerEmailFormat.Item(EDISettingData.CSTMR_CD))
                        End If
                        Dim strDestinationPath As String = String.Concat(Config.pub_GetAppConfigValue("OutputFolder"), drCustomerEmailFormat.Item(EDISettingData.CSTMR_CD), "\", "Temp", "\")
                        Dim strfiles() As String = Directory.GetFileSystemEntries(strDestinationPath)
                        Dim objDirectotyInfo As New DirectoryInfo(strDestinationPath)
                        arr_FileList.Clear()

                        For fileCount = 0 To strfiles.Length - 1
                            Dim f As New FileInfo(strfiles(fileCount))
                            creationDateTime = f.CreationTime
                            If (Not strfiles(fileCount).EndsWith(".rcv")) Then
                                arr_FileList.Add(strfiles(fileCount))
                            End If
                        Next
                        If Not arr_FileList.Count = 0 Then
                            pvt_SendEmail(strFromMailId, drCustomerEmailFormat, strDestinationPath, arr_FileList)
                        End If

                        Dim dtLastRun As DateTime
                        Dim dtNextRunTime As String = String.Empty
                        Dim intGenerationFormat As Integer = 0
                        Dim strGenerationTime As String = String.Empty

                        intGenerationFormat = CInt(drCustomerEmailFormat.Item(EDISettingData.GNRTN_FRMT))
                        strGenerationTime = CStr(drCustomerEmailFormat.Item(EDISettingData.GNRTN_TM))

                        'If intGenerationFormat = 24 Then
                        '    dtLastRun = DateTime.Now.AddHours(CDbl((strGenerationTime)))
                        '    dtLastRun.ToString("HH:mm:ss")
                        '    dtNextRunTime = CStr(dtLastRun.ToString("HH:mm"))
                        'ElseIf intGenerationFormat = 25 Then
                        '    If DateTime.Now <= CDate(strGenerationTime) Then
                        '        dtLastRun = DateTime.Now.AddDays(1)
                        '    End If
                        '    dtLastRun.ToString("HH:mm:ss")
                        '    dtNextRunTime = strGenerationTime
                        'End If

                        objEDISetting.pvt_UpdateNextRunTime(drCustomerEmailFormat.Item(EDISettingData.CSTMR_ID), _
                                                            Now, intDPT_ID)

                    End If
                Next
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GenerateEDICodeco"
    Private Sub pvt_GenerateEDICodeco(ByVal bv_blnGateIn As Boolean, ByVal bv_blnGateOut As Boolean, ByVal bv_blnRepairEstimate As Boolean, ByVal strWfdata As String, ByVal bv_Customer_cd As String)
        Dim strDepotCode As String = ""
        Dim dsEDiDataSet As New EDIDataSet
        Try
            Dim objEDI As New EDI
            If bv_blnGateIn = True OrElse bv_blnGateOut = True Then
                dsEDiDataSet.Tables(EDIData._EDI_FILE_IDENTIFIER).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_FILE_IDENTIFIER).Merge(objEDI.pub_GetEdiFileIdentifierByDpt_Id(strWfdata).Tables(EDIData._EDI_FILE_IDENTIFIER))
                dsEDiDataSet.Tables(EDIData._EDI_VERSION).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_VERSION).Merge(objEDI.pub_GetEdiVersionByDpt_Id(strWfdata).Tables(EDIData._EDI_VERSION))
                dsEDiDataSet.Tables(EDIData._EDI_MOVEMENT).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_MOVEMENT).Merge(objEDI.pub_GetEdiMovementByDpt_Id(strWfdata).Tables(EDIData._EDI_MOVEMENT))
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Merge(objEDI.pub_GetEdiSegmentHeaderByDpt_Id(strWfdata).Tables(EDIData._EDI_SEGMENT_HEADER))
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Merge(objEDI.pub_GetEdiSegmentDetailByDpt_Id(strWfdata).Tables(EDIData._EDI_SEGMENT_DETAIL))
            End If

            'Generate Gatein EDI file
            If bv_blnGateIn = True Then
                dsEDiDataSet.Tables(EDIData._GATEIN_RET).Clear()
                dsEDiDataSet.Tables(EDIData._GATEIN_RET).Merge(objEDI.pub_GetGateinRetByGI_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEIN_RET))
                objEDI.pub_WriteCodeco(dsEDiDataSet, "GATEIN", bv_Customer_cd, strGenerationMode)
            End If
            'Generate Westim & Westimdt EDI file
            If bv_blnRepairEstimate = True Then
                objEDI.pub_WriteWestimFile(strWfdata, bv_Customer_cd, strGenerationMode)
            End If

            'Generate Gateout EDI file
            If bv_blnGateOut = True Then
                dsEDiDataSet.Tables(EDIData._GATEOUT_RET).Clear()
                dsEDiDataSet.Tables(EDIData._GATEOUT_RET).Merge(objEDI.pub_GetGateoutRetByGO_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEOUT_RET))
                objEDI.pub_WriteCodeco(dsEDiDataSet, "GATEOUT", bv_Customer_cd, strGenerationMode)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_GenerateEDI"
    Private Sub pvt_GenerateEDI(ByVal bv_blnGateIn As Boolean, ByVal bv_blnGateOut As Boolean, ByVal bv_blnRepairEstimate As Boolean, ByVal strWfdata As String, ByVal bv_Customer_cd As String)

        Try
            Dim strDepotCode As String = ""
            Dim objEDI As New EDI

            'Generate Gatein EDI file
            If bv_blnGateIn = True Then
                objEDI.pub_WriteGatinFile(strWfdata, bv_Customer_cd, strGenerationMode)
            End If

            'Generate Westim & Westimdt EDI file
            If bv_blnRepairEstimate = True Then
                objEDI.pub_WriteWestimFile(strWfdata, bv_Customer_cd, strGenerationMode)
            End If

            'Generate Gateout EDI file
            If bv_blnGateOut = True Then
                objEDI.pub_WriteGateoutFile(strWfdata, bv_Customer_cd, strGenerationMode)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_SendEmail"
    Private Sub pvt_SendEmail(ByVal bv_strFromMailId As String, ByRef br_drprndingjob As DataRow, ByVal strDestinationPath As String,
                                                Optional ByVal attachemnt As ArrayList = Nothing)
        Try
            Dim blnStatus As Boolean
            Dim objDirectotyInfo As New DirectoryInfo(strDestinationPath)
            Dim strBodyHTML As String = String.Empty
            'strBodyHTML = pvt_GenerateBody(br_dtActivities)
            blnStatus = pub_Send_EmailWithAttachment(bv_strFromMailId, br_drprndingjob.Item("TO_EML_ID"), "", "Bulk Email", strBodyHTML, strDestinationPath, arr_FileList)

            For Each objFileinfo As FileInfo In objDirectotyInfo.GetFiles("*.*")
                objFileinfo.Delete()
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
    
#Region "pvt_SendEmail"
    Public Function pub_Send_EmailWithAttachment(ByVal pvt_strFromMailID As String, _
                                                ByVal bv_strToMailIDs As String, _
                                                ByVal bv_strCCEmailIDs As String, _
                                                ByVal bv_strSubject As String, _
                                                ByVal bv_strBody As String, _
                                                ByVal bv_strAttachmentPath As String,
                                                Optional ByVal attachemnt As ArrayList = Nothing) As Boolean

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
            Dim strCCEmailIds As String()
            strCCEmailIds = bv_strCCEmailIDs.Split(CChar(","))

            If strCCEmailIds.Length = 0 Then
                strCCEmailIds(0) = bv_strCCEmailIDs
            End If

            For Each strCCEmail As String In strCCEmailIds
                objEmail.AddCC(strCCEmail, strCCEmail)
            Next

            objEmail.Subject = "EDI"

            'Adding Foot note
            Dim sbrFootNote As New StringBuilder
            sbrFootNote.Append("<DIV style=""font-family:verdana;font-size:8pt"">")
            sbrFootNote.Append("<DIV>")
            sbrFootNote.Append(bv_strBody)
            sbrFootNote.Append("</DIV>")
            objEmail.SetHtmlBody(sbrFootNote.ToString())

            If Not attachemnt Is Nothing Then
                Dim iCnt As Integer = attachemnt.Count - 1
                For i = 0 To iCnt
                    If FileExists(attachemnt(i)) Then _
                      objEmail.AddFileAttachment(attachemnt(i))
                Next
            End If

            Status = objChilkat.SendEmail(objEmail)
            If Not Status Then
                iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, objChilkat.LastErrorText)
            End If
            Return Status
        Catch ex As Exception
            Throw ex
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
            Throw ex
        End Try
    End Function
#End Region

#Region "pvt_GenerateBody"
    Private Function pvt_GenerateBody(ByRef br_DtReports As DataTable) As String
        Try
            Dim sbBody As New StringBuilder
            Return sbBody.ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Diagnostic"
    Private Sub pvt_Diagnostic(ByVal bv_Message As String)
        If CBool(blnDiagnostic) Then
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, bv_Message)
        End If
    End Sub
#End Region

End Class