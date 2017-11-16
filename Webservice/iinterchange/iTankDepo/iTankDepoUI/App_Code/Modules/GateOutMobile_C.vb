Imports Microsoft.VisualBasic
Imports System.IO

Public Class GateOutMobile_C
    Inherits Framebase

    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"
    Dim dsGateOutData As New GateOutDataSet
    Dim str_008EIRTime As String
    Dim bln_008EIRTime_Key As Boolean
    Dim bln_005EqStatus_Key As Boolean
    Dim str_005EqStatus As String
    Dim strMode As String
    Dim bln_022RentalBit_Key As Boolean
    Dim str_022RentalBit As String
    Dim str_055 As String
    Dim bln_055 As Boolean

    Dim objCommon As New CommonData
    Dim gateInMobile As New GateinMobile_C


    Public Function Update(ByVal YardLocation As String, ByVal EquipmentNo As String, ByVal OutDate As String, ByVal Time As String, ByVal EIRNo As String,
                           ByVal VehicalNo As String, ByVal TransPorter As String, ByVal Rental As String, ByVal UserName As String, ByVal Mode As String,
                           ByVal RepairEstimateId As String, ByVal Remarks As String,
                           ByVal IfAttchment As String, ByVal hfc As ArrayOfFileParams) As Boolean




        Try


            gateInMobile.DepotID(UserName)
            Dim strFilterName As String = ""
            Dim strFilterValue As String = ""
            Dim objCommonUI As New CommonUI()
            Dim dtGateOut As DataTable
            Dim dtRental As New DataTable
            Dim objGateOut As New GateOut()
            Dim objCommon As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim str_059Gateout As String = String.Empty
            Dim strGateOutApprovalProcess As String = Nothing
            Dim GOTList As New GOTList


            str_059Gateout = objCommonConfig.pub_GetConfigSingleValue("059", intDPT_ID)
            strGateOutApprovalProcess = objCommonConfig.pub_GetConfigSingleValue("066", intDPT_ID)


            'hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
            '

            Dim strCurrentSessionId As String = objCommon.GetSessionID()
            'objCommon.FlushLockDataByActivity(strCurrentSessionId, "Gate Out")

            ' hdnMode.Value = e.Parameters("Mode").ToString()
            Select Case Mode
                Case strNew
                    'Dim blnShowEqStatus As Boolean = False
                    Dim dsEqpStatus As New DataSet
                    dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Gate Out", True, intDPT_ID)

                    If str_059Gateout.Trim.ToUpper = "TRUE" Then
                        dsGateOutData = objGateOut.pub_GetGateOutDetailsGWS(intDPT_ID)
                    Else
                        dsGateOutData = objGateOut.pub_GetGateOutDetails(intDPT_ID)
                    End If



                    If strGateOutApprovalProcess <> Nothing AndAlso CBool(strGateOutApprovalProcess) = True Then

                        dsGateOutData = New GateOutDataSet
                        dsGateOutData = objGateOut.pub_GetGateOutDetailsGWSWithApproval(intDPT_ID)

                    End If



                    dtGateOut = dsGateOutData.Tables(GateOutData._V_GATEOUT)
                    str_008EIRTime = objCommonConfig.pub_GetConfigSingleValue("008", intDPT_ID)
                    bln_008EIRTime_Key = objCommonConfig.IsKeyExists
                    Dim str_006YardLocation As String
                    str_006YardLocation = objCommon.GetYardLocation()
                    'If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                    '    blnShowEqStatus = True
                    'End If
                    For Each drGateOut As DataRow In dtGateOut.Rows
                        drGateOut.Item(GateOutData.GTOT_ID) = CommonWeb.GetNextIndex(dsGateOutData.Tables(GateOutData._V_GATEOUT), GateOutData.GTOT_ID)
                        'If blnShowEqStatus Then
                        '    drGateOut.Item(GateOutData.EQPMNT_STTS_CD) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.PENDING_STATUS)
                        '    drGateOut.Item(GateOutData.EQPMNT_STTS_ID) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)
                        'End If
                        If bln_008EIRTime_Key Then
                            drGateOut.Item(GateOutData.GTOT_TM) = str_008EIRTime
                        End If
                        If str_059Gateout.Trim.ToUpper = "TRUE" Then
                            drGateOut.Item(GateOutData.GTOT_TM) = DateTime.Now.ToString("H:mm").ToUpper
                        End If
                        drGateOut.Item(GateOutData.GTOT_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                        drGateOut.Item(GateOutData.YRD_LCTN) = str_006YardLocation
                    Next
                    'For Each drGateOut As DataRow In dtGateOut.Rows



                Case strEdit
                    dsGateOutData = objGateOut.pub_GetGateOutMySubmitDetails(intDPT_ID)
            End Select

            Dim dr1() As DataRow = dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(GateinData.EQPMNT_NO, "='", EquipmentNo, "'"))
            If dr1.Length > 0 Then


                dr1(0).Item("GTOT_DT") = OutDate
                dr1(0).Item("GTOT_TM") = Time
                dr1(0).Item("EIR_NO") = EIRNo
                dr1(0).Item("VHCL_NO") = VehicalNo
                dr1(0).Item("TRNSPRTR_CD") = TransPorter
                dr1(0).Item("RNTL_BT") = Rental
                dr1(0).Item("CHECKED") = "TRUE"
                dr1(0).Item("RMRKS_VC") = Remarks
                dr1(0).Item("YRD_LCTN") = YardLocation





            End If


            If IfAttchment = "True" Then
                Dim strSize As String = ConfigurationManager.AppSettings("UploadPhotoSize")
                Dim strPhotoLength As String = ConfigurationManager.AppSettings("UploadPhotoFileLength")


                Dim strModifiedBy As String
                Dim strVirtualPath As String = ""
                Dim uploadFolder As String = AppDomain.CurrentDomain.BaseDirectory.ToString
                Dim drAttachment As DataRow
                Dim strFilename As String = ""
                Dim strExtension As String = ""
                Dim strClientFileName As String = ""
                'intDepotId = objCommonData.GetDepotID()
                strModifiedBy = objCommon.GetCurrentUserName()
                'Dim strRepairEstimateId As String = RetrieveData("RepairEstimateId").ToString
                Dim strRepairEstimateId As String = RepairEstimateId
                Dim lngMaxSize As Long = CLng(strSize)
                lngMaxSize = lngMaxSize / 1024000


                For i As Integer = 0 To hfc.ArrayOfFileParams.Count - 1
                    Dim hpf As FileParams = hfc.ArrayOfFileParams(i)
                    If hpf.ContentLength > 0 Then
                        Dim lngFileSize As Long
                        Dim sbPath As New StringBuilder
                        strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
                        ' strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
                        strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                        lngFileSize = hpf.ContentLength

                        drAttachment = dsGateOutData.Tables(RepairEstimateData._ATTACHMENT).NewRow()
                        drAttachment(RepairEstimateData.ATTCHMNT_ID) = CommonWeb.GetNextIndex(dsGateOutData.Tables(RepairEstimateData._ATTACHMENT), RepairEstimateData.ATTCHMNT_ID)
                        If Not IsDBNull(strRepairEstimateId) AndAlso strRepairEstimateId <> String.Empty AndAlso strRepairEstimateId <> " " Then
                            drAttachment(RepairEstimateData.RPR_ESTMT_ID) = strRepairEstimateId
                        End If

                        Dim myMatch As Match = System.Text.RegularExpressions.Regex.Match(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), "^[a-zA-Z0-9 _.-]*$")
                        If myMatch.Success Then
                            strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName).Replace("+", ""), Now.ToFileTime)
                            strFilename = String.Concat(strFilename, strExtension)
                            strVirtualPath = String.Concat(Config.pub_GetAppConfigValue("UploadAttachPath"), strFilename)
                            strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                            lngFileSize = hpf.ContentLength
                            If strClientFileName.Length < strPhotoLength Then
                                If Not System.IO.Directory.Exists(String.Concat(uploadFolder, "\UpLoad\Attachment")) Then
                                    System.IO.Directory.CreateDirectory(String.Concat(uploadFolder, "\UpLoad\Attachment"))
                                End If
                                File.WriteAllBytes(strVirtualPath, Convert.FromBase64String(hpf.base64imageString))
                                'hpf.SaveAs(strVirtualPath)
                                drAttachment(RepairEstimateData.ATTCHMNT_PTH) = strFilename
                                drAttachment(RepairEstimateData.ACTL_FL_NM) = strClientFileName
                                drAttachment(RepairEstimateData.MDFD_BY) = strModifiedBy
                                drAttachment(RepairEstimateData.MDFD_DT) = CDate(Now)
                                drAttachment(RepairEstimateData.DPT_ID) = objCommon.GetDepotID()
                                drAttachment(RepairEstimateData.ERR_FLG) = False
                            Else
                                drAttachment(RepairEstimateData.ERR_FLG) = True
                                drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("Attachment File Name (", strClientFileName, " must be below ", strPhotoLength, "Characters.")
                            End If
                        Else
                            drAttachment(RepairEstimateData.ERR_FLG) = True
                            drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("File Name (", strClientFileName, ") with special characters are not allowed. ")
                        End If
                        dsGateOutData.Tables(RepairEstimateData._ATTACHMENT).Rows.Add(drAttachment)

                        'If bv_strPageName = "GateIn" Then
                        '    'CacheData(GATE_IN, dsGateInData)
                        'End If

                    End If
                Next

                Dim lngGateinId As Long = 0
                Dim intFilesCount As Integer = 0
                'Dim dsGateInData As GateinDataSet = CType(RetrieveData(GATE_IN), GateinDataSet)
                Dim drGateIn1 As DataRow() = Nothing

                lngGateinId = CLng(RepairEstimateId)
                drGateIn1 = dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(GateOutData.GTOT_ID, " = ", lngGateinId))
                If drGateIn1.Length > 0 Then
                    intFilesCount = CInt(dsGateOutData.Tables(GateOutData._ATTACHMENT).Compute(String.Concat("COUNT(", RepairCompletionData.RPR_ESTMT_ID, ")"), String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = '", lngGateinId, "'")))
                    drGateIn1(0).Item(GateOutData.COUNT_ATTACH) = intFilesCount
                End If
                CacheData("AttachmentClear", intFilesCount)

                ' dsGateInData = GateinMobile.PreAdvice


            Else
                CacheData("AttachmentClear", Nothing)

            End If





            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            Dim intHQID As Integer = CInt(objCommon.GetDepotID())

            Dim drAGateOut As DataRow()
            drAGateOut = dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(GateOutData.CHECKED & "='True'")
            dtGateOut = dsGateOutData.Tables(GateOutData._V_GATEOUT).Clone()

            Dim str_067InvoiceGenerationGWSBit As String = Nothing
            str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", intDepotID)

            'If Not drAGateOut.Length > 0 Then
            '    pub_SetCallbackStatus(False)
            '    pub_SetCallbackError("Please Select Atleast One Equipment.")
            '    Exit Function
            'End If


            'For Each dr As DataRow In dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(RepairCompletionData.CHECKED, "= 'True'"))
            '    pvt_GOlockData("FALSE", dr.Item(GateOutData.EQPMNT_NO).ToString, "FALSE")
            'Next

            Dim bv_strWfData As String = objCommon.GenerateWFData(18)
            Dim strEqpmntNO As String = String.Empty
            'Dim blnAllowRental As Boolean
            RetrieveData("AttachmentClear")

            'GWS 
            str_055 = objCommonConfig.pub_GetConfigSingleValue("055", intDPT_ID)
            bln_055 = objCommonConfig.IsKeyExists

            Dim result As Boolean = objGateOut.pub_UpdateGateOut(dsGateOutData, _
                                         bv_strWfData, _
                                         False, _
                                         objCommon.GetCurrentUserName(), _
                                         CDate(objCommon.GetCurrentDate()), _
                                         Mode, _
                                         intDPT_ID, _
                                         CStr(RetrieveData("AttachmentClear")), str_055, str_067InvoiceGenerationGWSBit)


            Return result


        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)

        End Try
    End Function

End Class
