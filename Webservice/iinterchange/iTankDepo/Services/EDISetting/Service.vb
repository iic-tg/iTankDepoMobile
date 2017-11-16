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
Imports iInterchange.iTankDepo.DataAccess

Public Class Service

#Region "Declaration"
    Dim intDPT_ID As Integer
    Dim strDPT_CD As String
    Dim strWfdata As String
    Dim arr_FileList As New ArrayList
    Dim strGenerationMode As String = "SERVICE"
    Dim blnDiagnostic As Boolean = False
    Dim dtNextRnDate As DateTime
    Dim objEDISetting As New iInterchange.iTankDepo.Business.Services.EDISetting
    Dim dsCustomer As New CustomerDataSet
    Dim objISO As New Customers
#End Region

#Region "OnStart"
    Protected Overrides Sub OnStart(ByVal args() As String)

    End Sub
#End Region

#Region "OnStop"
    Protected Overrides Sub OnStop()

    End Sub
#End Region

#Region "tmrEDI_Elapsed"
    Private Sub tmrEDI_Elapsed(sender As Object, e As System.Timers.ElapsedEventArgs) Handles tmrEDI.Elapsed
        Try
            SyncLock tmrEDI
                Dim dsEDISetting As New EDIDataSet
                Dim dtDepotId As New DataTable
                Dim creationDateTime As DateTime
                blnDiagnostic = Config.pub_GetAppConfigValue("diagnostic")
                pvt_Diagnostic("Elapsed start")
                dsEDISetting = objEDISetting.pvt_GetCustomerEmailFormat()

                Dim dtToday As Date = Now
                Dim mi As String = dtToday.Minute
                Dim hh As String = dtToday.Hour
                If mi < 10 Then
                    mi = String.Concat("0", mi)
                End If
                If hh < 10 Then
                    hh = String.Concat("0", hh)
                End If
                Dim strcrnttime As String = String.Concat(hh, ":", mi)

                If dsEDISetting.Tables(EDIData._V_EDI_SETTINGS).Rows.Count > 0 Then
                    For Each drCustomerEmailFormat As DataRow In dsEDISetting.Tables(EDIData._V_EDI_SETTINGS).Rows()
                        Dim strgnrtTime As String = drCustomerEmailFormat.Item(EDIData.GNRTN_TM)
                        Dim strGNRTN_TM As String = drCustomerEmailFormat.Item(EDIData.GNRTN_TM)
                        dtNextRnDate = Now.Date & " " & strGNRTN_TM
                        If Now >= drCustomerEmailFormat.Item(EDIData.NXT_RN_DT_TM) Then
                            Dim strFromMailId As String = ConfigurationManager.AppSettings("FromMailId")

                            intDPT_ID = drCustomerEmailFormat.Item(EDIData.DPT_ID)
                            strDPT_CD = drCustomerEmailFormat.Item(EDIData.DPT_CD)
                            'intDPT_ID = 1
                            'strDPT_CD = "JTSDXB"
                            strWfdata = String.Concat(EDIData.DPT_ID, "=", intDPT_ID, "&", EDIData.DPT_CD, "=", strDPT_CD)
                            If CStr(drCustomerEmailFormat.Item(EDIData.FLE_FRMT_CD)).Trim() = "CODECO" Then
                                pvt_GenerateEDICodeco(True, True, True, strWfdata, drCustomerEmailFormat.Item(EDIData.CSTMR_CD), drCustomerEmailFormat.Item(EDIData.CSTMR_ID), drCustomerEmailFormat.Item(EDIData.CRTD_BY))
                            ElseIf CStr(drCustomerEmailFormat.Item(EDIData.FLE_FRMT_CD)).Trim() = "ANSII" Then
                                pvt_GenerateEDI(True, True, True, strWfdata, drCustomerEmailFormat.Item(EDIData.CSTMR_CD), drCustomerEmailFormat.Item(EDIData.CSTMR_ID), drCustomerEmailFormat.Item(EDIData.CRTD_BY))
                            End If
                            Dim strDestinationPath As String '= String.Concat(Config.pub_GetAppConfigValue("OutputFolder"), drCustomerEmailFormat.Item(EDIData.CSTMR_CD), "\", "Temp", "\")
                            Dim objCommon As New CommonUIs
                            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                                strDestinationPath = String.Concat(Config.pub_GetAppConfigValue("OutputFolder"), strDPT_CD, "\", drCustomerEmailFormat.Item(EDIData.CSTMR_CD), "\", "Temp", "\")
                            Else
                                strDestinationPath = String.Concat(Config.pub_GetAppConfigValue("OutputFolder"), drCustomerEmailFormat.Item(EDIData.CSTMR_CD), "\", "Temp", "\")
                            End If

                            Dim objDirectotyInfo As New DirectoryInfo(strDestinationPath)
                            If IO.Directory.Exists(strDestinationPath) Then
                                Dim strfiles() As String = Directory.GetFileSystemEntries(strDestinationPath)
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
                            Else
                                iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, "There is no file genaration for this")
                            End If
                            Dim strDate As DateTime = drCustomerEmailFormat.Item(EDIData.NXT_RN_DT_TM)

                            objEDISetting.pvt_UpdateNextRunTime(drCustomerEmailFormat.Item(EDIData.CSTMR_ID), _
                                                                strDate.AddDays(1), intDPT_ID)
                        End If
                    Next
                End If
            End SyncLock
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GenerateEDICodeco"
    Private Sub pvt_GenerateEDICodeco(ByVal bv_blnGateIn As Boolean, ByVal bv_blnGateOut As Boolean, ByVal bv_blnRepairEstimate As Boolean, ByVal strWfdata As String, ByVal bv_Customer_cd As String, _
                                      ByVal bv_Customer_ID As Int64, ByVal bv_User As String)
        Dim strDepotCode As String = ""
        Dim dsEDiDataSet As New EDIDataSet
        Dim bv_blnRepairComplete As Boolean = True
        Dim objEDI As New EDI
        Dim objCommon As New CommonUIs
        Dim bv_Customer_Orginal As String
        'for EDI code passed as Customer 
        Try
            bv_Customer_Orginal = bv_Customer_cd
            If bv_blnGateIn = True OrElse bv_blnGateOut = True Then
                dsEDiDataSet.Tables(EDIData._EDI_FILE_IDENTIFIER).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_VERSION).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_MOVEMENT).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Clear()
                dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Clear()
                If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                    dsEDiDataSet.Tables(EDIData._EDI_FILE_IDENTIFIER).Merge(objEDI.pub_GetEdiFileIdentifierByDpt_Id(String.Concat("DPT_ID=", objCommon.GetHeadQuarterID().ToString)).Tables(EDIData._EDI_FILE_IDENTIFIER))
                    dsEDiDataSet.Tables(EDIData._EDI_VERSION).Merge(objEDI.pub_GetEdiVersionByDpt_Id(String.Concat("DPT_ID=", objCommon.GetHeadQuarterID().ToString)).Tables(EDIData._EDI_VERSION))
                    dsEDiDataSet.Tables(EDIData._EDI_MOVEMENT).Merge(objEDI.pub_GetEdiMovementByDpt_Id(String.Concat("DPT_ID=", objCommon.GetHeadQuarterID().ToString)).Tables(EDIData._EDI_MOVEMENT))
                    dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Merge(objEDI.pub_GetEdiSegmentHeaderByDpt_Id(String.Concat("DPT_ID=", objCommon.GetHeadQuarterID().ToString)).Tables(EDIData._EDI_SEGMENT_HEADER))
                    dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Merge(objEDI.pub_GetEdiSegmentDetailByDpt_Id(String.Concat("DPT_ID=", objCommon.GetHeadQuarterID().ToString)).Tables(EDIData._EDI_SEGMENT_DETAIL))
                Else
                    dsEDiDataSet.Tables(EDIData._EDI_FILE_IDENTIFIER).Merge(objEDI.pub_GetEdiFileIdentifierByDpt_Id(strWfdata).Tables(EDIData._EDI_FILE_IDENTIFIER))
                    dsEDiDataSet.Tables(EDIData._EDI_VERSION).Merge(objEDI.pub_GetEdiVersionByDpt_Id(strWfdata).Tables(EDIData._EDI_VERSION))
                    dsEDiDataSet.Tables(EDIData._EDI_MOVEMENT).Merge(objEDI.pub_GetEdiMovementByDpt_Id(strWfdata).Tables(EDIData._EDI_MOVEMENT))
                    dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_HEADER).Merge(objEDI.pub_GetEdiSegmentHeaderByDpt_Id(strWfdata).Tables(EDIData._EDI_SEGMENT_HEADER))
                    dsEDiDataSet.Tables(EDIData._EDI_SEGMENT_DETAIL).Merge(objEDI.pub_GetEdiSegmentDetailByDpt_Id(strWfdata).Tables(EDIData._EDI_SEGMENT_DETAIL))
                End If
            End If

            dsCustomer = objISO.getISOCODEbyCustomer(CLng(bv_Customer_ID))
            If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                bv_Customer_cd = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
            Else
                bv_Customer_cd = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
            End If

            'Generate Gatein EDI file
            If bv_blnGateIn = True Then
                dsEDiDataSet.Tables(EDIData._GATEIN_RET).Clear()
                dsEDiDataSet.Tables(EDIData._GATEIN_RET).Merge(objEDI.pub_GetGateinRetByGI_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEIN_RET))
                Dim strResult As String = objEDI.pub_WriteCodeco(dsEDiDataSet, "GATEIN", bv_Customer_cd, strGenerationMode, strWfdata)
                If strResult <> "No Gate In Records found for EDI generation" Then
                    Dim strmsg As String() = strResult.Split(",")
                    Dim fileName As String = String.Concat("", ",", Path.GetFileName(strmsg(1)))
                    pvt_CreateEDI(bv_Customer_Orginal, "GATEIN", DateTime.Now, 0, fileName, "CODECO", bv_Customer_ID, bv_User, DateTime.Now, strWfdata)
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat("Gate In Codeco File is Genarated ", bv_Customer_cd))
                Else
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, strResult)
                End If
            End If
            'Generate Westim & Westimdt EDI file
            If bv_blnRepairEstimate = True Then
                Dim strResult As String = objEDI.pub_WriteWestimFile(strWfdata, bv_Customer_cd, strGenerationMode)
                If strResult <> "No Estimate Records found for EDI generation" Then
                    Dim strmsg As String() = strResult.Split(",")
                    Dim fileName As String = Path.GetFileName(strmsg(1))
                    Dim strfilename As String = String.Empty
                    For i = 1 To strmsg.Length - 1
                        strfilename = String.Concat(strfilename, ",", Path.GetFileName(strmsg(i)))
                    Next
                    pvt_CreateEDI(bv_Customer_Orginal, "ESTIMATE", DateTime.Now, 0, strfilename, "ANSII", bv_Customer_ID, bv_User, DateTime.Now, strWfdata)
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat("Estimate Codeco File is Genarated ", bv_Customer_cd))
                Else
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, strResult)
                End If
            End If

            '  If bv_blnRepairComplete = True Then
            If bv_blnRepairComplete = True Then
                dsEDiDataSet.Tables(EDIData._GATEIN_RET).Clear()
                dsEDiDataSet.Tables(EDIData._GATEIN_RET).Merge(objEDI.pub_GetRepairCompletenRetByGI_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEIN_RET))
                Dim strResult As String = objEDI.pub_WriteCodeco(dsEDiDataSet, "REPAIRCOMPLETE", bv_Customer_cd, strGenerationMode, strWfdata)
                If strResult <> "No Repair Complete Records found for EDI generation" Then
                    Dim strmsg As String() = strResult.Split(",")
                    Dim fileName As String = String.Concat("", ",", Path.GetFileName(strmsg(1)))
                    pvt_CreateEDI(bv_Customer_Orginal, "REPAIRCOMPLETE", DateTime.Now, 0, fileName, "CODECO", bv_Customer_ID, bv_User, DateTime.Now, strWfdata)
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat("Repair Complete Codeco File is Genarated ", bv_Customer_cd))
                Else
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, strResult)
                End If
            End If

            ' End If


            'Generate Gateout EDI file
            If bv_blnGateOut = True Then
                dsEDiDataSet.Tables(EDIData._GATEOUT_RET).Clear()
                dsEDiDataSet.Tables(EDIData._GATEOUT_RET).Merge(objEDI.pub_GetGateoutRetByGO_DPT_TRMDS(bv_Customer_cd, "U", strWfdata).Tables(EDIData._GATEOUT_RET))
                Dim strResult As String = objEDI.pub_WriteCodeco(dsEDiDataSet, "GATEOUT", bv_Customer_cd, strGenerationMode, strWfdata)
                If strResult <> "No Gate Out Records found for EDI generation" Then
                    Dim strmsg As String() = strResult.Split(",")
                    Dim fileName As String = String.Concat("", ",", Path.GetFileName(strmsg(1)))
                    pvt_CreateEDI(bv_Customer_Orginal, "GATEOUT", DateTime.Now, 0, fileName, "CODECO", bv_Customer_ID, bv_User, DateTime.Now, strWfdata)
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat("GATEOUT Codeco File is Genarated ", bv_Customer_cd))
                Else
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, strResult)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_GenerateEDI"
    Private Sub pvt_GenerateEDI(ByVal bv_blnGateIn As Boolean, ByVal bv_blnGateOut As Boolean, ByVal bv_blnRepairEstimate As Boolean, ByVal strWfdata As String, ByVal bv_Customer_cd As String, _
                                ByVal bv_Customer_ID As Int64, ByVal bv_User As String)

        Try
            Dim strDepotCode As String = ""
            Dim objEDI As New EDI
            Dim bv_Customer_Orginal As String = bv_Customer_cd

            dsCustomer = objISO.getISOCODEbyCustomer(CLng(bv_Customer_ID))
            If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                bv_Customer_cd = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
            Else
                bv_Customer_cd = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
            End If
            'Generate Gatein EDI file
            If bv_blnGateIn = True Then
                Dim strResult As String = objEDI.pub_WriteGatinFile(strWfdata, bv_Customer_cd, strGenerationMode, "AUTO")
                If strResult <> "No Gate In Records found for EDI generation" Then
                    Dim strmsg As String() = strResult.Split(",")
                    Dim fileName As String = String.Concat("", ",", Path.GetFileName(strmsg(1)))
                    pvt_CreateEDI(bv_Customer_Orginal, "GATEIN", DateTime.Now, 0, fileName, "ANSII", bv_Customer_ID, bv_User, DateTime.Now, strWfdata)
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat("Gate In Ansii File Genarated ", bv_Customer_cd))
                Else
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, strResult)
                End If
            End If

          

            'Generate Westim & Westimdt EDI file
            If bv_blnRepairEstimate = True Then
                Dim strResult As String = objEDI.pub_WriteWestimFile(strWfdata, bv_Customer_cd, strGenerationMode)
                If strResult <> "No Estimate Records found for EDI generation" Then
                    Dim strmsg As String() = strResult.Split(",")
                    Dim fileName As String = Path.GetFileName(strmsg(1))
                    Dim strfilename As String = String.Empty
                    For i = 1 To strmsg.Length - 1
                        strfilename = String.Concat(strfilename, ",", Path.GetFileName(strmsg(i)))
                    Next
                    pvt_CreateEDI(bv_Customer_Orginal, "ESTIMATE", DateTime.Now, 0, strfilename, "ANSII", bv_Customer_ID, bv_User, DateTime.Now, strWfdata)
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat("Estimate Ansii File Genarated ", bv_Customer_cd))
                Else
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, strResult)
                End If
            End If

            'Generate Gateout EDI file
            If bv_blnGateOut = True Then
                Dim strResult As String = objEDI.pub_WriteGateoutFile(strWfdata, bv_Customer_cd, strGenerationMode)
                If strResult <> "No Gate out Records found for EDI generation" Then
                    Dim strmsg As String() = strResult.Split(",")
                    Dim fileName As String = String.Concat("", ",", Path.GetFileName(strmsg(1)))
                    pvt_CreateEDI(bv_Customer_Orginal, "GATEOUT", DateTime.Now, 0, fileName, "ANSII", bv_Customer_ID, bv_User, DateTime.Now, strWfdata)
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat("Gate out Ansii File Genarated ", bv_Customer_cd))
                Else
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, strResult)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pvt_SendEmail"
    Private Sub pvt_SendEmail(ByVal bv_strFromMailId As String, ByRef br_drPendingEDI As DataRow, ByVal strDestinationPath As String,
                                                Optional ByVal attachemnt As ArrayList = Nothing)
        Try
            Dim blnStatus As Boolean
            Dim objDirectotyInfo As New DirectoryInfo(strDestinationPath)
            Dim strBodyHTML As String = String.Empty
            'strBodyHTML = pvt_GenerateBody(br_dtActivities)
            blnStatus = pub_Send_EmailWithAttachment(bv_strFromMailId, br_drPendingEDI.Item("TO_EML"), br_drPendingEDI.Item("CC_EML"), br_drPendingEDI.Item("SBJCT_VCR"), strBodyHTML, strDestinationPath, arr_FileList)

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

            If Config.pub_GetAppConfigValue("StartTLS") IsNot Nothing AndAlso Config.pub_GetAppConfigValue("StartTLS") = "True" Then
                objChilkat.StartTLS = Config.pub_GetAppConfigValue("StartTLS")
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

            objEmail.Subject = bv_strSubject

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
            Else
                iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, "Mail Sent Successfully")
            End If
            Return Status
        Catch ex As Exception
            Throw ex
        Finally
            objChilkat = Nothing
        End Try
    End Function
#End Region

#Region "pvt_CreateEDI"
    Protected Sub pvt_CreateEDI(ByVal bv_Customer_cd As String, _
        ByVal bv_strActivity As String, _
        ByVal bv_strGenarated As DateTime, _
        ByVal bv_strRsnBit As String, _
        ByVal bv_strAscFileName As String, _
        ByVal bv_strFileformat As String, _
        ByVal bv_lngCstmr_id As Int64, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_strCreatedDt As DateTime, _
        ByVal bv_strwfData As String)
        Dim objEDI As New EDI
        Try
            Dim lng As Long = objEDI.pub_CreateEDI(bv_Customer_cd, bv_strActivity, _
                                                       bv_strGenarated, bv_strRsnBit, _
                                                       bv_strAscFileName, bv_strFileformat, _
                                                       bv_strCreatedBy, _
                                                       bv_strCreatedDt, bv_lngCstmr_id, bv_strwfData)

        Catch ex As Exception
            '    pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub
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
