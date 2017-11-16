Imports Microsoft.VisualBasic
Imports System.Web.Script.Serialization
Imports System.Globalization
Imports System.IO

Public Class GateinMobile_C
    Inherits Framebase




    Dim dsGateInData As New GateinDataSet
    Dim dsGateInAttchmentData As New GateinDataSet
    Dim dtGateinData As DataTable
    Private Const GATE_IN As String = "GATE_IN"
    Private Const GATE_IN_DOCUMENT As String = "GATE_IN_DOCUMENT"
    Private strMSGUPDATE As String = "Gate In Creation : Equipment(s) Updated Successfully."
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"
    Dim bln_011KeyExist As Boolean
    Dim bln_020KeyExist As Boolean
    Dim str_020KeyValue As String
    Dim bln_005EqStatus_Key As Boolean
    'Dim bln_006YardLocation_Key As Boolean
    Dim bln_007EIRNo_Key As Boolean
    Dim bln_008EIRTime_Key As Boolean
    Dim bln_009EqType_Key As Boolean
    'Dim bln_010EqCode_Key As Boolean

    Dim str_005EqStatus As String
    Dim str_006YardLocation As String
    Dim str_007EIRNo As String
    Dim str_008EIRTime As String
    Dim str_009EqType As String
    'Dim str_010EqCode As String
    Private Const GateInMode As String = "GateInMode"
    Dim strMode As String
    Dim objCommonConfig As New ConfigSetting()
    Dim bln_022RentalBit_Key As Boolean
    Dim str_022RentalBit As String
    Dim hdnCurrentDate As DateTime



    Public Function DepotID(ByVal UserName As String) As Integer

        Dim dsUser As New UserDataSet
        Dim objUser As New User
        dsUser = objUser.pub_GetUserDetailByUserName(UserName)
        Dim strSessionID As String = Session.SessionID
        Dim objCommonData As New CommonData
        Dim iCache As New iNCache

        If GetNCache(strSessionID) Is Nothing Then
            SetNCache(strSessionID, iCache) 'New Session created here after log out
        Else

        End If
        CacheData("UserData", dsUser)

    End Function

    Public Function PreAdvice(ByVal UserName As String, ByVal Mode As String) As GateinDataSet


        DepotID(UserName)

        Dim objGateIn As New Gatein
        Dim objCommon As New CommonData
        Dim dsGateInData As New GateinDataSet
        Dim intDepotID As Integer = CInt(objCommon.GetDepotID())

        Try
            '    If Session.Count > 0 Then

            Select Case Mode
                Case strNew


                    Dim strCurrentSessionId As String = objCommon.GetSessionID()




                    Dim dtPreaAdvice As DataTable

                    Dim dsEqpStatus As New DataSet


                    Dim str_006YardLocation As String

                    dsGateInData = objGateIn.GetGateInPreAdviceDetail(1)
                    dtPreaAdvice = dsGateInData.Tables(GateinData._V_GATEIN)
                    Dim objCommonUI As New CommonUI()

                    Dim dsEqpmntTyp As New DataSet
                    'Dim dsEqpmntCode As New DataSet
                    Dim dtEquipmentType As New DataTable
                    Dim objCommonConfig As New ConfigSetting()
                    Dim blnShowEqStatus As Boolean = False



                    str_006YardLocation = objCommon.GetYardLocation()


                    str_008EIRTime = objCommonConfig.pub_GetConfigSingleValue("008", intDepotID)
                    bln_008EIRTime_Key = objCommonConfig.IsKeyExists

                    str_009EqType = objCommonConfig.pub_GetConfigSingleValue("009", intDepotID)
                    bln_009EqType_Key = objCommonConfig.IsKeyExists

                    'str_010EqCode = objCommonConfig.pub_GetConfigSingleValue("010", intDepotID)
                    'bln_010EqCode_Key = objCommonConfig.IsKeyExists




                    Dim bln_051GwsBit As Boolean
                    Dim str_051GWSKey As String
                    str_051GWSKey = objCommonConfig.pub_GetConfigSingleValue("051", intDepotID)
                    bln_051GwsBit = objCommonConfig.IsKeyExists
                    If bln_051GwsBit Then
                        If str_051GWSKey.ToLower = "true" Then
                            dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("GateIn-GWS", True, intDepotID)
                            dsEqpmntTyp = objCommonUI.pub_GetEquipmentType(str_009EqType, intDepotID)
                            dtEquipmentType = objCommonUI.GetAllEquipmentCode(intDepotID)
                        Else

                            ''''' Commented Since this error 

                            ''''' "Message": "Session state can only be used when enableSessionState is set to true, either in a configuration file or in the Page directive. Please also make sure that System.Web.SessionStateModule or a custom session state module is included in the <configuration>\\<system.web>\\<httpModules> section in the application configuration."
                            ''''''''''



                            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                                dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Gate In", True, CInt(objCommon.GetHeadQuarterID()))
                                dsEqpmntTyp = objCommonUI.pub_GetEquipmentType(str_009EqType, CInt(objCommon.GetHeadQuarterID()))
                                'ifgEquipmentDetail.Columns.Item(4).IsEditable = True
                                dtEquipmentType = objCommonUI.GetAllEquipmentCode(CInt(objCommon.GetHeadQuarterID()))
                            Else
                                dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Gate In", True, intDepotID)
                                dsEqpmntTyp = objCommonUI.pub_GetEquipmentType(str_009EqType, intDepotID)
                                'ifgEquipmentDetail.Columns.Item(4).IsEditable = False
                                dtEquipmentType = objCommonUI.GetAllEquipmentCode(intDepotID)
                            End If

                            ''''''''

                        End If
                    End If
                    If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                        blnShowEqStatus = True
                    End If
                    For Each drPreAdvice As DataRow In dtPreaAdvice.Rows
                        drPreAdvice.Item(GateinData.GTN_ID) = CommonWeb.GetNextIndex(dsGateInData.Tables(GateinData._V_GATEIN), GateinData.GTN_ID)


                        drPreAdvice.Item(GateinData.YRD_LCTN) = str_006YardLocation

                        If bln_008EIRTime_Key Then
                            drPreAdvice.Item(GateinData.GTN_TM) = str_008EIRTime
                        End If
                        If bln_051GwsBit Then
                            If str_051GWSKey.ToLower = "true" Then
                                drPreAdvice.Item(GateinData.GTN_TM) = DateTime.Now.ToString("H:mm").ToUpper
                            End If
                        End If

                        If blnShowEqStatus Then
                            drPreAdvice.Item(GateinData.EQPMNT_STTS_CD) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS)
                            drPreAdvice.Item(GateinData.EQPMNT_STTS_ID) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)
                        End If
                        'Type & Code Merge
                        'If bln_010EqCode_Key Then
                        '    If Not str_010EqCode = "" Then
                        '        If dsEqpmntCode.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then  ' Type & code Merge
                        '            'drPreAdvice.Item(GateinData.EQPMNT_CD_ID) = dsEqpmntCode.Tables(CommonUIData._EQUIPMENT_CODE).Rows(0).Item(CommonUIData.EQPMNT_CD_ID).ToString
                        '            drPreAdvice.Item(GateinData.EQPMNT_CD_CD) = str_010EqCode
                        '        End If
                        '    End If
                        'End If

                        If Not dtEquipmentType Is Nothing AndAlso dtEquipmentType.Rows.Count > 0 Then

                            If dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drPreAdvice.Item(GateinData.EQPMNT_TYP_ID))).Length > 0 Then
                                Dim dr() As DataRow = dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drPreAdvice.Item(GateinData.EQPMNT_TYP_ID)))

                                drPreAdvice.Item(GateinData.EQPMNT_CD_ID) = dr(0).Item(GateinData.EQPMNT_TYP_ID)
                                drPreAdvice.Item(GateinData.EQPMNT_CD_CD) = dr(0).Item(GateinData.EQPMNT_CD_CD)
                            End If
                        End If
                        drPreAdvice.Item(GateinData.PR_ADVC_BT) = True
                        drPreAdvice.Item(GateinData.GTN_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                        hdnCurrentDate = DateTime.Now.ToString("dd-MMM-yyyy")
                    Next

                    If dsGateInData.Tables(GateinData._V_GATEIN).Rows.Count = 0 Then
                        Dim drGateIn As DataRow = dsGateInData.Tables(GateinData._V_GATEIN).NewRow()
                        drGateIn.Item(GateinData.YRD_LCTN) = str_006YardLocation
                        If bln_008EIRTime_Key Or bln_009EqType_Key Then
                            If blnShowEqStatus Then
                                drGateIn.Item(GateinData.EQPMNT_STTS_CD) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS)
                                drGateIn.Item(GateinData.EQPMNT_STTS_ID) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)
                                drGateIn.Item(GateinData.GTN_ID) = CommonWeb.GetNextIndex(dsGateInData.Tables(GateinData._V_GATEIN), GateinData.GTN_ID)
                                drGateIn.Item(GateinData.CHECKED) = True
                            End If

                            If bln_008EIRTime_Key Then
                                drGateIn.Item(GateinData.GTN_TM) = str_008EIRTime
                            End If
                            If bln_051GwsBit Then
                                If str_051GWSKey.ToLower = "true" Then
                                    drGateIn.Item(GateinData.GTN_TM) = DateTime.Now.ToString("H:mm").ToUpper
                                End If
                            End If
                            If Not str_009EqType = "" Then
                                If dsEqpmntTyp.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                                    drGateIn.Item(GateinData.EQPMNT_TYP_ID) = dsEqpmntTyp.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                                    drGateIn.Item(GateinData.EQPMNT_TYP_CD) = str_009EqType
                                End If
                            End If
                            'Type & Code Merge
                            'If Not str_010EqCode = "" Then
                            '    dsEqpmntCode = objCommonUI.pub_GetEquipmentCode(str_010EqCode, intDepotID)
                            '    If dsEqpmntCode.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then ' Type & code Merge
                            '        'drGateIn.Item(GateinData.EQPMNT_CD_ID) = dsEqpmntCode.Tables(CommonUIData._EQUIPMENT_CODE).Rows(0).Item(CommonUIData.EQPMNT_CD_ID).ToString
                            '        drGateIn.Item(GateinData.EQPMNT_CD_CD) = str_010EqCode
                            '    End If
                            'End If

                            If Not dtEquipmentType Is Nothing AndAlso dtEquipmentType.Rows.Count > 0 Then

                                If dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drGateIn.Item(GateinData.EQPMNT_TYP_ID))).Length > 0 Then
                                    Dim dr() As DataRow = dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drGateIn.Item(GateinData.EQPMNT_TYP_ID)))

                                    drGateIn.Item(GateinData.EQPMNT_CD_ID) = dr(0).Item(GateinData.EQPMNT_TYP_ID)
                                    drGateIn.Item(GateinData.EQPMNT_CD_CD) = dr(0).Item(GateinData.EQPMNT_CD_CD)
                                End If
                            End If
                            drGateIn.Item(GateinData.PR_ADVC_BT) = False
                            drGateIn.Item(GateinData.GTN_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                            '  drGateIn.Item(GateinData.GTN_TM) = DateTime.Now.ToString("H:mm")
                            hdnCurrentDate = DateTime.Now.ToString("dd-MMM-yyyy")
                            dsGateInData.Tables(GateinData._V_GATEIN).Rows.Add(drGateIn)
                        End If
                    End If

                Case strEdit

                    dsGateInData = objGateIn.pub_GetGateIn(intDepotID)
            End Select





            CacheData(GATE_IN, dsGateInData)




            Dim RVDataSet As GateinDataSet = dsGateInData



            Return RVDataSet

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)


        End Try



    End Function


    Public Function Update(ByVal GTN_ID As String, ByVal CSTMR_ID As String, ByVal CSTMR_CD As String, ByVal EQPMNT_NO As String, ByVal EQPMNT_TYP_ID As String,
                           ByVal EQPMNT_TYP_CD As String, ByVal EQPMNT_CD_ID As String, ByVal EQPMNT_CD_CD As String, ByVal YRD_LCTN As String,
                           ByVal GTN_DT As String, ByVal GTN_TM As String, ByVal PRDCT_ID As String, ByVal PRDCT_CD As String, ByVal EIR_NO As String, ByVal VHCL_NO As String,
                           ByVal TRNSPRTR_CD As String, ByVal HTNG_BT As String, ByVal RMRKS_VC As String, ByVal CHECKED As String, ByVal PRDCT_DSCRPTN_VC As String,
                           ByVal RNTL_BT As String, ByVal hfc As ArrayOfFileParams, ByVal RepairEstimateId As String,
                           ByVal IfAttchment As String, ByVal UserName As String, ByVal Mode As String, ByVal EIMNFCTR_DT As String, ByVal EITR_WGHT_NC As String,
                                        ByVal EIGRSS_WGHT_NC As String, ByVal EICPCTY_NC As String, ByVal EILST_SRVYR_NM As String, ByVal EILST_TST_DT As String,
                                        ByVal EILST_TST_TYP_ID As String, ByVal EINXT_TST_DT As String, ByVal EINXT_TST_TYP_ID As String, ByVal EIRMRKS_VC As String,
                                        ByVal EIACTV_BT As String, ByVal EIRNTL_BT As String, ByVal EIAttachment As String, ByVal EIHasChanges As String, ByVal PageName As String, ByVal GateinTransactionNo As String) As String


        dsGateInData = PreAdvice(UserName, Mode)

        'Dim ds As DataSet
        'ds = CType(RetrieveData(GATE_IN), GateinDataSet)

        Dim objCommonConfig As New ConfigSetting()
        Dim objCommon As New CommonUI
        Dim objCommonData As New CommonData
        Dim objRepairEstimate As New RepairEstimate
        Dim drgateIn As DataRow
        Dim dtGateInDocument As DataTable
        dtGateInDocument = dsGateInData.Tables(GateinData._V_GATEIN).Clone()
        Dim blnAllowRental As Boolean = False



        Try



            Dim dr1() As DataRow = dsGateInData.Tables(GateinData._V_GATEIN).Select(String.Concat(GateinData.EQPMNT_NO, "='", EQPMNT_NO, "'"))
            If dr1.Length > 0 Then
                dr1(0).Item(GateinData.GTN_ID) = GTN_ID

                dr1(0).Item(GateinData.CSTMR_ID) = CSTMR_ID
                dr1(0).Item(GateinData.CSTMR_CD) = CSTMR_CD
                dr1(0).Item(GateinData.EQPMNT_NO) = EQPMNT_NO
                dr1(0).Item(GateinData.EQPMNT_TYP_ID) = EQPMNT_TYP_ID
                dr1(0).Item(GateinData.EQPMNT_TYP_CD) = EQPMNT_TYP_CD
                dr1(0).Item(GateinData.EQPMNT_CD_ID) = EQPMNT_CD_ID
                dr1(0).Item(GateinData.EQPMNT_CD_CD) = EQPMNT_CD_CD
                dr1(0).Item(GateinData.EQPMNT_STTS_ID) = 1
                dr1(0).Item(GateinData.EQPMNT_STTS_CD) = "IND"
                dr1(0).Item(GateinData.YRD_LCTN) = YRD_LCTN
                dr1(0).Item(GateinData.GTN_DT) = GTN_DT
                dr1(0).Item(GateinData.GTN_TM) = GTN_TM
                dr1(0).Item(GateinData.PRDCT_ID) = PRDCT_ID
                dr1(0).Item(GateinData.PRDCT_CD) = PRDCT_CD
                dr1(0).Item(GateinData.EIR_NO) = EIR_NO
                dr1(0).Item(GateinData.VHCL_NO) = VHCL_NO
                dr1(0).Item(GateinData.TRNSPRTR_CD) = TRNSPRTR_CD
                dr1(0).Item(GateinData.HTNG_BT) = HTNG_BT
                dr1(0).Item(GateinData.RMRKS_VC) = RMRKS_VC

                dr1(0).Item(GateinData.CHECKED) = CHECKED

                dr1(0).Item(GateinData.PRDCT_DSCRPTN_VC) = PRDCT_DSCRPTN_VC
                dr1(0).Item(GateinData.RNTL_BT) = RNTL_BT



            Else
                'lngGateInbin = CommonWeb.GetNextIndex(dsGateInData.Tables(GateinData._V_GATEIN), GateinData.GTN_ID)

                drgateIn = dsGateInData.Tables(GateinData._V_GATEIN).NewRow()

                drgateIn.Item(GateinData.GTN_ID) = CommonWeb.GetNextIndex(dsGateInData.Tables(GateinData._V_GATEIN), GateinData.GTN_ID)
                drgateIn.Item(GateinData.GTN_ID) = GTN_ID

                drgateIn.Item(GateinData.CSTMR_ID) = CSTMR_ID
                drgateIn.Item(GateinData.CSTMR_CD) = CSTMR_CD
                drgateIn.Item(GateinData.EQPMNT_NO) = EQPMNT_NO
                drgateIn.Item(GateinData.EQPMNT_TYP_ID) = EQPMNT_TYP_ID
                drgateIn.Item(GateinData.EQPMNT_TYP_CD) = EQPMNT_TYP_CD
                drgateIn.Item(GateinData.EQPMNT_CD_ID) = EQPMNT_CD_ID
                drgateIn.Item(GateinData.EQPMNT_CD_CD) = EQPMNT_CD_CD
                drgateIn.Item(GateinData.EQPMNT_STTS_ID) = 1
                drgateIn.Item(GateinData.EQPMNT_STTS_CD) = "IND"
                drgateIn.Item(GateinData.YRD_LCTN) = YRD_LCTN
                drgateIn.Item(GateinData.GTN_DT) = GTN_DT
                drgateIn.Item(GateinData.GTN_TM) = GTN_TM
                drgateIn.Item(GateinData.PRDCT_ID) = PRDCT_ID
                drgateIn.Item(GateinData.PRDCT_CD) = PRDCT_CD
                drgateIn.Item(GateinData.EIR_NO) = EIR_NO
                drgateIn.Item(GateinData.VHCL_NO) = VHCL_NO
                drgateIn.Item(GateinData.TRNSPRTR_CD) = TRNSPRTR_CD
                drgateIn.Item(GateinData.HTNG_BT) = HTNG_BT
                drgateIn.Item(GateinData.RMRKS_VC) = RMRKS_VC

                drgateIn.Item(GateinData.CHECKED) = CHECKED

                drgateIn.Item(GateinData.PRDCT_DSCRPTN_VC) = PRDCT_DSCRPTN_VC
                drgateIn.Item(GateinData.RNTL_BT) = RNTL_BT


                dsGateInData.Tables(GateinData._V_GATEIN).Rows.Add(drgateIn)
            End If



            If IfAttchment = "True" And Mode = "new" Then


                dsGateInData = Attachment(dsGateInData, hfc, RepairEstimateId)


                Dim lngGateinId As Long = 0
                Dim intFilesCount As Integer = 0
                'Dim dsGateInData As GateinDataSet = CType(RetrieveData(GATE_IN), GateinDataSet)
                Dim drGateIn1 As DataRow() = Nothing

                lngGateinId = CLng(RepairEstimateId)
                drGateIn1 = dsGateInData.Tables(GateinData._V_GATEIN).Select(String.Concat(GateinData.GTN_ID, " = ", lngGateinId))
                If drGateIn1.Length > 0 Then
                    intFilesCount = CInt(dsGateInData.Tables(GateinData._ATTACHMENT).Compute(String.Concat("COUNT(", RepairCompletionData.RPR_ESTMT_ID, ")"), String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = '", lngGateinId, "'")))
                    drGateIn1(0).Item(GateinData.COUNT_ATTACH) = intFilesCount
                End If
                CacheData("AttachmentClear", intFilesCount)

                ' dsGateInData = GateinMobile.PreAdvice


            Else
                CacheData("AttachmentClear", Nothing)

            End If


            If IfAttchment = "True" And Mode = "edit" Then


                dsGateInData = Attachment(dsGateInData, hfc, RepairEstimateId)


                Dim lngGateinId As Long = 0
                Dim intFilesCount As Integer = 0
                'Dim dsGateInData As GateinDataSet = CType(RetrieveData(GATE_IN), GateinDataSet)
                Dim drGateIn1 As DataRow() = Nothing

                lngGateinId = CLng(RepairEstimateId)
                drGateIn1 = dsGateInData.Tables(GateinData._V_GATEIN).Select(String.Concat(GateinData.GTN_ID, " = ", lngGateinId))
                If drGateIn1.Length > 0 Then
                    intFilesCount = CInt(dsGateInData.Tables(GateinData._ATTACHMENT).Compute(String.Concat("COUNT(", RepairCompletionData.RPR_ESTMT_ID, ")"), String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = '", lngGateinId, "'")))
                    drGateIn1(0).Item(GateinData.COUNT_ATTACH) = intFilesCount
                End If
                CacheData("AttachmentClear", intFilesCount)

                ' dsGateInData = GateinMobile.PreAdvice


            Else
                CacheData("AttachmentClear", Nothing)

            End If


            'Dim bv_strWfData As String = "USERID=1&SYSTM_DT=16-NOV-2016 02:43:30 PM&USERNAME=ADMIN&RL_CD=ADMIN&RL_ID=1&DPT_ID=1&DPT_CD=DEPOT!&MSTR_ID_CSV=&QCK_LNK_ID_CSV=&CRT_BT=False&EDT_BT=True&VW_BT=True&ACTIVITYID=82&ACTN_DT=NOV 16, 2016"

            Dim bv_strWfData As String = objCommonData.GenerateWFData(82)
            'Dim objCommonData As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommonData.GetDepotID())
            Dim str_051GWSBit As String
            Dim bln_051GWSBit_Key As Boolean
            str_051GWSBit = objCommonConfig.pub_GetConfigSingleValue("051", intDPT_ID)
            bln_051GWSBit_Key = objCommonConfig.IsKeyExists
            Dim str_067InvoiceGenerationGWSBit As String = Nothing
            Dim str_068GWSBit As String
            Dim bln_068GWSBit_Key As Boolean
            bln_068GWSBit_Key = objCommonConfig.IsKeyExists
            str_068GWSBit = objCommonConfig.pub_GetConfigSingleValue("068", intDPT_ID)
            If bln_068GWSBit_Key = False Then
                str_068GWSBit = "false"
            End If
            Dim objgateIn As New Gatein
            Dim strRemarks As String = String.Empty
            Dim strLockingRecords As String = String.Empty



            Dim result As Boolean = objgateIn.pub_UpdateGateIn(dsGateInData, bv_strWfData, _
                                       CBool(ConfigurationManager.AppSettings("GenerateEDI")), _
                                       objCommonData.GetCurrentUserName(), CDate(objCommonData.GetCurrentDate()), _
                                       Mode, intDPT_ID, strRemarks, _
                                       CStr(RetrieveData("AttachmentClear")), strLockingRecords, str_051GWSBit, str_068GWSBit, str_067InvoiceGenerationGWSBit)
            'CacheData("AttachmentClear", Nothing)
            Dim ReturnResult As String = ""

            If result Then
                Dim EIInfo As New EquipmentInfoMobile

                If EIHasChanges = "True" Then

                    Dim EIResult As EIResult = EIInfo.UpdateEquipmentInfo(EQPMNT_NO, EQPMNT_TYP_CD, EIMNFCTR_DT, EITR_WGHT_NC, EIGRSS_WGHT_NC, EICPCTY_NC, EILST_SRVYR_NM, EILST_TST_DT,
                            EILST_TST_TYP_ID, EINXT_TST_DT, EINXT_TST_TYP_ID, EIRMRKS_VC,
                            EIACTV_BT, EIRNTL_BT, PageName, GateinTransactionNo,
                                        EIAttachment, UserName)

                    If EIResult.Status = "EIUpdated" AndAlso result Then

                        ReturnResult = "Success"

                    ElseIf Not EIResult.Status = "EIUpdated" AndAlso result Then


                        ReturnResult = "EINotUpdated"



                    End If

                Else
                    ReturnResult = "Success"
                End If


            Else

                ReturnResult = "GateIn Not Updated"

            End If






            Return ReturnResult

        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)

        End Try












    End Function

    Public Function Attachment(ByVal dsGateInData As GateinDataSet, ByVal hfc As ArrayOfFileParams, ByVal RepairEstimateId As String) As GateinDataSet




        Dim objCommon As New CommonUI
        Dim objCommonData As New CommonData
        Dim objRepairEstimate As New RepairEstimate
        Dim strSize As String = ConfigurationManager.AppSettings("UploadPhotoSize")
        Dim strPhotoLength As String = ConfigurationManager.AppSettings("UploadPhotoFileLength")

        Dim intDepotId As Integer
        Dim strModifiedBy As String
        Dim strVirtualPath As String = ""
        Dim uploadFolder As String = AppDomain.CurrentDomain.BaseDirectory.ToString
        Dim drAttachment As DataRow
        Dim strFilename As String = ""
        Dim strExtension As String = ""
        Dim strClientFileName As String = ""
        Dim actualfleName As String = ""
        intDepotId = objCommonData.GetDepotID()
        strModifiedBy = objCommonData.GetCurrentUserName()
        'Dim strRepairEstimateId As String = RetrieveData("RepairEstimateId").ToString
        Dim strRepairEstimateId As String = RepairEstimateId
        Dim lngMaxSize As Long = CLng(strSize)
        lngMaxSize = lngMaxSize / 1024000


        For i As Integer = 0 To hfc.ArrayOfFileParams.Count - 1
            Dim hpf As FileParams = hfc.ArrayOfFileParams(i)
            drAttachment = dsGateInData.Tables(RepairEstimateData._ATTACHMENT).NewRow()

            Dim lngFileSize As Long
            Dim sbPath As New StringBuilder
            strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
			'actualfleName="FFFFFFFFFFFF.jpg"
            
			
            ' strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
            strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
            lngFileSize = hpf.ContentLength


            drAttachment(RepairEstimateData.ATTCHMNT_ID) = CommonWeb.GetNextIndex(dsGateInData.Tables(RepairEstimateData._ATTACHMENT), RepairEstimateData.ATTCHMNT_ID)
            If Not IsDBNull(strRepairEstimateId) AndAlso strRepairEstimateId <> String.Empty AndAlso strRepairEstimateId <> " " Then
                drAttachment(RepairEstimateData.RPR_ESTMT_ID) = strRepairEstimateId
            End If
            If hpf.ContentLength > 0 Then
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
                        drAttachment(RepairEstimateData.DPT_ID) = intDepotId
                        drAttachment(RepairEstimateData.ERR_FLG) = False
                    Else
                        drAttachment(RepairEstimateData.ERR_FLG) = True
                        drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("Attachment File Name (", strClientFileName, " must be below ", strPhotoLength, "Characters.")
                    End If
                Else
                    drAttachment(RepairEstimateData.ERR_FLG) = True
                    drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("File Name (", strClientFileName, ") with special characters are not allowed. ")
                End If


                'If bv_strPageName = "GateIn" Then
                '    'CacheData(GATE_IN, dsGateInData)
                'End If
            Else
			     actualfleName = System.IO.Path.GetFileName(hpf.base64imageString)
                drAttachment(RepairEstimateData.ATTCHMNT_PTH) = actualfleName
                drAttachment(RepairEstimateData.ACTL_FL_NM) = strClientFileName
                drAttachment(RepairEstimateData.MDFD_BY) = strModifiedBy
                drAttachment(RepairEstimateData.MDFD_DT) = CDate(Now)
                drAttachment(RepairEstimateData.DPT_ID) = intDepotId
                drAttachment(RepairEstimateData.ERR_FLG) = False
            End If
            dsGateInData.Tables(RepairEstimateData._ATTACHMENT).Rows.Add(drAttachment)
        Next


        Return dsGateInData

    End Function


    Public Function AttachmentEdit(ByVal dsGateInData As GateinDataSet, ByVal hfc As ArrayOfFileParams, ByVal RepairEstimateId As String) As GateinDataSet




        Dim objCommon As New CommonUI
        Dim objCommonData As New CommonData
        Dim objRepairEstimate As New RepairEstimate
        Dim strSize As String = ConfigurationManager.AppSettings("UploadPhotoSize")
        Dim strPhotoLength As String = ConfigurationManager.AppSettings("UploadPhotoFileLength")

        Dim intDepotId As Integer
        Dim strModifiedBy As String
        Dim strVirtualPath As String = ""
        Dim uploadFolder As String = AppDomain.CurrentDomain.BaseDirectory.ToString
        Dim drAttachment As DataRow
        Dim strFilename As String = ""
        Dim strExtension As String = ""
        Dim strClientFileName As String = ""
        Dim stractualImage As String = ""

        intDepotId = objCommonData.GetDepotID()
        strModifiedBy = objCommonData.GetCurrentUserName()
        'Dim strRepairEstimateId As String = RetrieveData("RepairEstimateId").ToString
        Dim strRepairEstimateId As String = RepairEstimateId
        Dim lngMaxSize As Long = CLng(strSize)
        lngMaxSize = lngMaxSize / 1024000


        For i As Integer = 0 To hfc.ArrayOfFileParams.Count - 1
            Dim hpf As FileParams = hfc.ArrayOfFileParams(i)

            Dim lngFileSize As Long
            Dim sbPath As New StringBuilder
            strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
            stractualImage = System.IO.Path.GetFileName(hpf.base64imageString)

            ' strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
            strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
            lngFileSize = hpf.ContentLength

            drAttachment = dsGateInData.Tables(RepairEstimateData._ATTACHMENT).NewRow()
            'strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName).Replace("+", ""), Now.ToFileTime)
            strFilename = System.IO.Path.GetFileName(hpf.FileName)
            drAttachment(RepairEstimateData.ATTCHMNT_ID) = CommonWeb.GetNextIndex(dsGateInData.Tables(RepairEstimateData._ATTACHMENT), RepairEstimateData.ATTCHMNT_ID)
            If Not IsDBNull(strRepairEstimateId) AndAlso strRepairEstimateId <> String.Empty AndAlso strRepairEstimateId <> " " Then
                drAttachment(RepairEstimateData.RPR_ESTMT_ID) = strRepairEstimateId
            End If
            If hpf.ContentLength > 0 Then
                'Dim myMatch As Match = System.Text.RegularExpressions.Regex.Match(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), "^[a-zA-Z0-9 _.-]*$")
                'If myMatch.Success Then

                strFilename = String.Concat(strFilename, strExtension)
                strVirtualPath = String.Concat(Config.pub_GetAppConfigValue("UploadAttachPath"), strFilename)
                strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                lngFileSize = hpf.ContentLength
                If strClientFileName.Length < strPhotoLength Then
                    If Not System.IO.Directory.Exists(String.Concat(uploadFolder, "\UpLoad\Attachment")) Then
                        System.IO.Directory.CreateDirectory(String.Concat(uploadFolder, "\UpLoad\Attachment"))
                    End If
                    'File.WriteAllBytes(strVirtualPath, Convert.FromBase64String(hpf.base64imageString))
                    'hpf.SaveAs(strVirtualPath)
                    drAttachment(RepairEstimateData.ATTCHMNT_PTH) = strFilename
                    drAttachment(RepairEstimateData.ACTL_FL_NM) = strClientFileName
                    drAttachment(RepairEstimateData.MDFD_BY) = strModifiedBy
                    drAttachment(RepairEstimateData.MDFD_DT) = CDate(Now)
                    drAttachment(RepairEstimateData.DPT_ID) = intDepotId
                    drAttachment(RepairEstimateData.ERR_FLG) = False
                Else
                    drAttachment(RepairEstimateData.ERR_FLG) = True
                    drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("Attachment File Name (", strClientFileName, " must be below ", strPhotoLength, "Characters.")
                End If
                'Else
                '    drAttachment(RepairEstimateData.ERR_FLG) = True
                '    drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("File Name (", strClientFileName, ") with special characters are not allowed. ")
                'End If
            Else
                drAttachment(RepairEstimateData.ATTCHMNT_PTH) = stractualImage
                drAttachment(RepairEstimateData.ACTL_FL_NM) = strClientFileName
                drAttachment(RepairEstimateData.MDFD_BY) = strModifiedBy
                drAttachment(RepairEstimateData.MDFD_DT) = CDate(Now)
                drAttachment(RepairEstimateData.DPT_ID) = intDepotId
                drAttachment(RepairEstimateData.ERR_FLG) = False
            End If
            dsGateInData.Tables(RepairEstimateData._ATTACHMENT).Rows.Add(drAttachment)

            'If bv_strPageName = "GateIn" Then
            '    'CacheData(GATE_IN, dsGateInData)
            'End If

        Next


        Return dsGateInData

    End Function





End Class


