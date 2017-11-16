Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.Data.SqlTypes

#Region "RepairCompletion"
<ServiceContract()> _
Public Class RepairCompletion

#Region "GET :pub_GetActivityStatusEquipmentByDepotId() "
    <OperationContract()> _
    Public Function pub_GetActivityStatusEquipmentByDepotId(ByVal bv_intDepotId As Int32) As RepairCompletionDataSet

        Try
            Dim dsRepairCompletionDataSet As RepairCompletionDataSet
            Dim objRepairEstimates As New RepairCompletions
            dsRepairCompletionDataSet = objRepairEstimates.GetActivityStatusEquipmentByDepotId(bv_intDepotId)
            Return dsRepairCompletionDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET :GetRepairEstimatePendingByDepotId() "
    <OperationContract()> _
    Public Function pub_GetRepairEstimatePendingByDepotId(ByVal bv_intDepotId As Int32) As RepairCompletionDataSet

        Try
            Dim dsRepairCompletionDataSet As RepairCompletionDataSet
            Dim objRepairEstimates As New RepairCompletions
            dsRepairCompletionDataSet = objRepairEstimates.GetRepairEstimatePendingByDepotId(bv_intDepotId)
            Return dsRepairCompletionDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET :GetRepairEstimatePendingByDepotId() "
    <OperationContract()> _
    Public Function pub_GetRepairEstimatePendingJTSByDepotId(ByVal bv_intDepotId As Int32) As RepairCompletionDataSet

        Try
            Dim dsRepairCompletionDataSet As RepairCompletionDataSet
            Dim objRepairEstimates As New RepairCompletions
            dsRepairCompletionDataSet = objRepairEstimates.GetRepairEstimatePendingJTSByDepotId(bv_intDepotId)
            Return dsRepairCompletionDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET :pub_GetRepairEstimateMySubmitByDepotId() "
    <OperationContract()> _
    Public Function pub_GetRepairEstimateMySubmitByDepotId(ByVal bv_intDepotId As Int32) As RepairCompletionDataSet

        Try
            Dim dsRepairCompletionDataSet As RepairCompletionDataSet
            Dim objRepairEstimates As New RepairCompletions
            dsRepairCompletionDataSet = objRepairEstimates.GetRepairEstimateMySubmitByDepotId(bv_intDepotId)
            Return dsRepairCompletionDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET :pub_GetRepairEstimateMySubmitJTSByDepotId() "
    <OperationContract()> _
    Public Function pub_GetRepairEstimateMySubmitJTSByDepotId(ByVal bv_intDepotId As Int32) As RepairCompletionDataSet

        Try
            Dim dsRepairCompletionDataSet As RepairCompletionDataSet
            Dim objRepairEstimates As New RepairCompletions
            dsRepairCompletionDataSet = objRepairEstimates.GetRepairEstimateMySubmitJTSByDepotId(bv_intDepotId)
            Return dsRepairCompletionDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "UPDATE : pub_UpdateRepairCompletion"
    <OperationContract()> _
    Public Function pub_UpdateRepairCompletion(ByRef br_dsRepairEstimate As RepairCompletionDataSet, _
                                               ByVal bv_strWfData As String, _
                                               ByVal bv_strRevisionNo As String, _
                                               ByVal bv_datCreatedDate As DateTime, _
                                               ByVal bv_strMode As String, _
                                               ByVal bv_intDepotId As Integer, _
                                               ByVal bv_strUserName As String, _
                                               ByVal str_067InvoiceGenerationGWSBit As String) As Boolean
        Dim objtrans As New Transactions

        Dim decTotalServicetax As Decimal
        Dim decLabourCost As Decimal
        Dim decCleaningCost As Decimal
        Dim strRCStatus As String = Nothing
        Dim strEstimationType As String = Nothing
        Dim blnRestimatebit As Boolean = False
        Dim strEquipmentInfoRemarks As String = String.Empty

        Dim strTransmissionNo As String = "W" & Format(Now, "yyyyMMddHHmmssffff")
        Dim dtRepairEstimate As DataTable = br_dsRepairEstimate.Tables(RepairCompletionData._SUMMARY_DETAIL)
        Dim intEqpmnt_stts_id As Integer
        Try
            Dim objRepairCompletions As New RepairCompletions
            Dim lngCreated As Long
            Dim dtExchnage As New DataTable
            ' Dim dtRepairDetail As New DataTable
            Dim RepairEstimateId As Long
            Dim strExists As String = ""
            Dim lngRETCreated As Long
            Dim blnEDI As Boolean = False
            Dim intRevisionNo As Integer = 0
            Dim strOwnerApproval As String = String.Empty
            Dim strPartyApproval As String = String.Empty
            Dim strRemarks As String = String.Empty
            Dim decLaborRate As Decimal = 0
            Dim dsEqpStatus As CommonUIDataSet
            Dim objConfigs As New CommonUIs
            Dim decApprovalAmount As Decimal = 0
            Dim decPartyApprovalAmount As Decimal = 0
            Dim decCustomerApprovalAmount As Decimal = 0
            Dim decTotalAmount As Decimal = 0
            Dim decMaterialRate As Decimal = 0
            Dim decPartyLaborRate As Decimal = 0
            Dim decPartyMaterialRate As Decimal = 0



            dsEqpStatus = objConfigs.GetWorkFlowActivity("Repair Completion", True, bv_intDepotId)

            If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                intEqpmnt_stts_id = CInt(dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID))
            End If
            Dim dtAttachment As New DataTable
            dtAttachment = br_dsRepairEstimate.Tables(RepairCompletionData._ATTACHMENT).Clone()


            For Each drRepairCompletion As DataRow In br_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(RepairCompletionData.CHECKED & "='True'")
                If br_dsRepairEstimate.Tables(RepairCompletionData._ATTACHMENT).Rows.Count = 0 Then
                    If Not IsDBNull(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_ID)) Then
                        dtAttachment = objRepairCompletions.GetAttachmentByRepairEstimateId(bv_intDepotId, CLng(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_ID)), objtrans).Tables(RepairCompletionData._ATTACHMENT)
                        br_dsRepairEstimate.Tables(RepairCompletionData._ATTACHMENT).Merge(dtAttachment)
                    End If
                End If
                Dim decTotalCustomerEstimateAmount As Decimal = 0
                Dim decTotalPartyEstimateAmount As Decimal = 0
                Dim decTotalCustomerlabourCost As Decimal = 0
                Dim decTotalPartylabourCost As Decimal = 0
                Dim decTotalCustomerMaterialCost As Decimal = 0
                Dim decTotalPartyMaterialCost As Decimal = 0
                Dim i64ResCustomerId As Int64 = 0
                Dim i64ResPartyId As Int64 = 0
                Dim i64PartyId As Int64 = 0
                Dim dtGateIn As New DataTable
                Dim dtRepairCharge As DataTable
                Dim dblExchangeRate As Double = 0
                Dim dtActivityStatus As New DataTable
                Dim decPartyCurrecnyAmount As Decimal = 0
                Dim decCustomerCurrencyAmount As Decimal = 0
                Dim decExchangeRate As Decimal = 0
                Dim strApprovalRefNo As String = String.Empty

                If Not IsDBNull(drRepairCompletion.Item(RepairCompletionData.CSTMR_EXCHANGE_RATE_NC)) Then
                    decExchangeRate = CDec(drRepairCompletion.Item(RepairCompletionData.CSTMR_EXCHANGE_RATE_NC))
                End If
                dtRepairCharge = br_dsRepairEstimate.Tables(RepairCompletionData._REPAIR_CHARGE).Clone()
                dtRepairCharge = objRepairCompletions.GetRepairChargeByTransactionNo(CInt(CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.DPT_ID))), drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString, objtrans)
                dtActivityStatus = br_dsRepairEstimate.Tables(RepairCompletionData._V_ACTIVITY_STATUS).Clone()
                dtActivityStatus = objRepairCompletions.GetActivityStatusByEqpmntNo(bv_intDepotId, _
                                                                                    drRepairCompletion.Item(RepairCompletionData.EQPMNT_NO).ToString, _
                                                                                    drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString, _
                                                                                    objtrans)

                Dim dsRepairCompletion As New RepairCompletionDataSet
                Dim dtRepairEstimateDetail As New DataTable
                dtRepairEstimateDetail = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Clone()
                dtRepairEstimateDetail = objRepairCompletions.GetRepairEstimateDetailByRepairEstimationIdTrans(CLng(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_ID)), objtrans)
                '   If dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Merge(dtRepairEstimateDetail)
                '  End If
                pvt_CalculateRepairChargesCompletion(dsRepairCompletion, _
                                                     decTotalCustomerEstimateAmount, _
                                                     decTotalPartyEstimateAmount, _
                                                     decTotalCustomerlabourCost, _
                                                     decTotalPartylabourCost, _
                                                     decTotalCustomerMaterialCost, _
                                                     decTotalPartyMaterialCost, _
                                                     i64ResCustomerId, _
                                                     i64ResPartyId, _
                                                     True)
                If dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                    pvt_CalculateTotalAmount(dsRepairCompletion, decTotalAmount, decLaborRate, decMaterialRate, decCustomerApprovalAmount, decPartyLaborRate, decPartyMaterialRate, decPartyApprovalAmount, decApprovalAmount, decCustomerCurrencyAmount, _
                                             decExchangeRate)
                End If
                Dim dtExchangeRate As New DataTable
                If Not IsDBNull(drRepairCompletion.Item(RepairCompletionData.INVCNG_PRTY_ID)) Then
                    If CLng(drRepairCompletion.Item(RepairCompletionData.DPT_CRRNCY_ID)) = CLng(drRepairCompletion.Item(RepairCompletionData.INVCNG_PRTY_CRRNCY_ID)) Then
                        decPartyCurrecnyAmount = decPartyApprovalAmount
                    Else
                        If objConfigs.GetMultiLocationSupportConfig().ToLower = "true" Then
                            dtExchangeRate = objRepairCompletions.GetExchangeRateByCurrencyId(CInt(objConfigs.GetHeadQuarterID()), CLng(drRepairCompletion.Item(RepairCompletionData.DPT_CRRNCY_ID)), CLng(drRepairCompletion.Item(RepairCompletionData.INVCNG_PRTY_CRRNCY_ID)), objtrans)
                        Else
                            dtExchangeRate = objRepairCompletions.GetExchangeRateByCurrencyId(bv_intDepotId, CLng(drRepairCompletion.Item(RepairCompletionData.DPT_CRRNCY_ID)), CLng(drRepairCompletion.Item(RepairCompletionData.INVCNG_PRTY_CRRNCY_ID)), objtrans)
                        End If

                        If dtExchangeRate.Rows.Count > 0 Then
                            decPartyCurrecnyAmount = CDec(dtExchangeRate.Rows(0).Item(RepairCompletionData.EXCHNG_RT_PR_UNT_NC)) * decPartyApprovalAmount
                        End If
                    End If
                End If
                strExists = objRepairCompletions.CheckRepairEstimateByEstimateId(CommonUIs.iInt(drRepairCompletion.Item(RepairCompletionData.DPT_ID)), CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_ID)), "Repair Completion", objtrans)
                Dim dtRepairData As New DataTable
                dtRepairData = br_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Clone()
                dtRepairData = objRepairCompletions.GetRepairEstimateByEqpmntNo(drRepairCompletion.Item(RepairCompletionData.EQPMNT_NO).ToString, drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString, "Repair Approval", objtrans)

                If dtRepairData.Rows.Count > 0 Then
                    If Not IsDBNull(dtRepairData.Rows(0).Item(RepairCompletionData.OWNR_APPRVL_RF)) Then
                        strOwnerApproval = dtRepairData.Rows(0).Item(RepairCompletionData.OWNR_APPRVL_RF).ToString
                    End If
                    If Not IsDBNull(dtRepairData.Rows(0).Item(RepairCompletionData.PRTY_APPRVL_RF)) Then
                        strPartyApproval = dtRepairData.Rows(0).Item(RepairCompletionData.PRTY_APPRVL_RF).ToString
                    End If
                    If Not IsDBNull(dtRepairData.Rows(0).Item(RepairCompletionData.RMRKS_VC)) Then
                        strRemarks = dtRepairData.Rows(0).Item(RepairCompletionData.RMRKS_VC).ToString
                    End If
                    If Not IsDBNull(dtRepairData.Rows(0).Item(RepairCompletionData.LBR_RT_NC)) Then
                        decLaborRate = CDec(dtRepairData.Rows(0).Item(RepairCompletionData.LBR_RT_NC))
                    End If
                    If Not IsDBNull(dtRepairData.Rows(0).Item(RepairCompletionData.RVSN_NO)) Then
                        intRevisionNo = CInt(dtRepairData.Rows(0).Item(RepairCompletionData.RVSN_NO))
                    End If
                End If
                If strExists = "0" Then
                    Dim strInwardTime As String = CStr(drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM))
                    Dim strHours As String
                    Dim strMinutes As String
                    If strInwardTime.Length = 5 Then
                        strHours = strInwardTime.Substring(0, 2)
                        strMinutes = strInwardTime.Substring(3, 2)
                    Else
                        strHours = strInwardTime.Substring(0, 1)
                        strMinutes = strInwardTime.Substring(2, 2)
                    End If
                    Dim datOldDatetime As DateTime = CDate(drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT))
                    Dim datNewDatetime As New DateTime(datOldDatetime.Year, datOldDatetime.Month, datOldDatetime.Day, CInt(strHours), CInt(strMinutes), 0)

                    lngCreated = objRepairCompletions.CreateRepairCompletion(CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.CSTMR_ID)), _
                                                                           drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_NO).ToString, _
                                                                           CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_DT)), _
                                                                           CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.ORGNL_ESTMN_DT)), _
                                                                           datNewDatetime, _
                                                                           CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.GTN_DT)), _
                                                                           "Repair Completion", _
                                                                           drRepairCompletion.Item(RepairCompletionData.EQPMNT_NO).ToString, _
                                                                           intEqpmnt_stts_id, _
                                                                           CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.LST_TST_DT)), _
                                                                           CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.LST_TST_TYP_ID)), _
                                                                           drRepairCompletion.Item(RepairCompletionData.VLDTY_PRD_TST_YRS).ToString, _
                                                                           CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.NXT_TST_DT)), _
                                                                           drRepairCompletion.Item(RepairCompletionData.LST_SRVYR_NM).ToString, _
                                                                           CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.NXT_TST_TYP_ID)), _
                                                                           CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.RPR_TYP_ID)), _
                                                                           CommonUIs.iBool(drRepairCompletion.Item(RepairCompletionData.CRT_OF_CLNLNSS_BT)), _
                                                                           CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.INVCNG_PRTY_ID)), _
                                                                           decLaborRate, _
                                                                           drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString, _
                                                                           intRevisionNo, _
                                                                           CommonUIs.iInt(drRepairCompletion.Item(RepairCompletionData.DPT_ID)), _
                                                                           strOwnerApproval, _
                                                                           CommonUIs.iDec(drRepairCompletion.Item(RepairCompletionData.ESTMTN_TTL_NC)), _
                                                                           CommonUIs.iDec(drRepairCompletion.Item(RepairCompletionData.APPRVL_AMNT_NC)), _
                                                                           CommonUIs.iDec(drRepairCompletion.Item(RepairCompletionData.ORGNL_ESTMTN_AMNT_NC)), _
                                                                           CommonUIs.iDec(drRepairCompletion.Item(RepairCompletionData.CSTMR_ESTMTN_TTL_NC)), _
                                                                           CommonUIs.iDec(drRepairCompletion.Item(RepairCompletionData.CSTMR_APPRVL_AMNT_NC)), _
                                                                           CommonUIs.iDec(drRepairCompletion.Item(RepairCompletionData.ACTL_MN_HR_NC)), _
                                                                           bv_strUserName, _
                                                                           bv_datCreatedDate, _
                                                                           bv_strUserName, _
                                                                           bv_datCreatedDate, _
                                                                           strPartyApproval, _
                                                                           objtrans)
                    RepairEstimateId = lngCreated
                    Dim lngDetail As Long = 0

                    'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]

                    strEquipmentInfoRemarks = objConfigs.GetEquipmentInformation(drRepairCompletion.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                   bv_intDepotId, _
                                                                                   objtrans)
                    For Each drRepairDetail As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", CLng(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_ID))))
                        If drRepairDetail.RowState <> DataRowState.Deleted Then
                            lngDetail = objRepairCompletions.CreateRepairCompletionDetail(lngCreated, _
                                                                                          CommonUIs.iLng(drRepairDetail.Item(RepairCompletionData.ITM_ID)), _
                                                                                          CommonUIs.iLng(drRepairDetail.Item(RepairCompletionData.SB_ITM_ID)), _
                                                                                          CommonUIs.iLng(drRepairDetail.Item(RepairCompletionData.DMG_ID)), _
                                                                                          CommonUIs.iLng(drRepairDetail.Item(RepairCompletionData.RPR_ID)), _
                                                                                          CommonUIs.iDec(drRepairDetail.Item(RepairCompletionData.LBR_HRS)), _
                                                                                          drRepairDetail.Item(RepairCompletionData.DMG_RPR_DSCRPTN).ToString, _
                                                                                          CommonUIs.iDec(drRepairDetail.Item(RepairCompletionData.LBR_RT)), _
                                                                                          CommonUIs.iDec(drRepairDetail.Item(RepairCompletionData.LBR_HR_CST_NC)), _
                                                                                          CommonUIs.iDec(drRepairDetail.Item(RepairCompletionData.MTRL_CST_NC)), _
                                                                                          CommonUIs.iDec(drRepairDetail.Item(RepairCompletionData.TTL_CST_NC)), _
                                                                                          CommonUIs.iLng(drRepairDetail.Item(RepairCompletionData.RSPNSBLTY_ID)), _
                                                                                          objtrans)
                            drRepairDetail.Item(RepairCompletionData.RPR_ESTMT_DTL_ID) = lngDetail
                        End If
                    Next
                    'If blnEDI Then
                    Dim strEIRTime As String = CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_DT)).ToString("hh:mm")
                    drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_NO).ToString()
                    drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString()
                    'drRepairCompletion.Item(GateinData.EQPMNT_STTS_CD).ToString() C
                    Dim dsCustomer As New CustomerDataSet
                    Dim objISO As New Customers
                    Dim strlessor As String
                    dsCustomer = objISO.getISOCODEbyCustomer(CLng(drRepairCompletion.Item(RepairCompletionData.CSTMR_ID)))
                    If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                        strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
                    Else
                        strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
                    End If
                    Dim strEquipmentDescription As String = objRepairCompletions.GetEquipmentDescription(drRepairCompletion.Item(RepairCompletionData.EQPMNT_CD_CD).ToString, objtrans)
                    Dim intEIRlenghth As Int32
                    Dim intLenghth As Int32
                    Dim strTrimEirNumber As String
                    'For Index Pattern
                    If drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString().Length > 11 Then
                        intEIRlenghth = drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString().Length - 11
                        intLenghth = drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString().Length
                        strTrimEirNumber = drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString().Substring(intEIRlenghth, 11)
                    Else
                        strTrimEirNumber = drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString()
                    End If
                    If strEquipmentDescription.Length > 30 Then
                        strEquipmentDescription = strEquipmentDescription.Substring(0, 30)
                    End If
                    Dim strEquipType As String
                    Dim objEquipType As New EquipmentTypes
                    Dim dsEquipType As EquipmentTypeDataSet
                    If objConfigs.GetMultiLocationSupportConfig().ToLower = "true" Then
                        dsEquipType = objEquipType.GetEquipmentGroupByEquipmentTypeId(drRepairCompletion.Item(RepairCompletionData.EQPMNT_TYP_ID).ToString, CInt(objConfigs.GetHeadQuarterID()))
                    Else
                        dsEquipType = objEquipType.GetEquipmentGroupByEquipmentTypeId(drRepairCompletion.Item(RepairCompletionData.EQPMNT_TYP_ID).ToString, CInt(CommonUIs.ParseWFDATA(bv_strWfData, "DPT_ID")))
                    End If

                    If dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                        strEquipType = CStr(dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows(0).Item(EquipmentTypeData.EQPMNT_GRP_CD))
                    Else
                        strEquipType = String.Empty
                    End If
                    objRepairCompletions.CreateGateinRet(lngCreated, drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString(), Nothing, Nothing, CStr(DateTime.Now.ToString("yyyyMMdd")), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strTrimEirNumber, _
                                                         strTrimEirNumber, drRepairCompletion.Item(RepairCompletionData.EQPMNT_NO).ToString, strEquipType, _
                                            strEquipmentDescription, drRepairCompletion.Item(RepairCompletionData.EQPMNT_CD_CD).ToString, "C", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, CDate(drRepairCompletion.Item(RepairCompletionData.GTN_DT)), Nothing, _
                                            drRepairCompletion.Item(RepairCompletionData.CSTMR_CD).ToString, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing, Nothing, strlessor, _
                                            Nothing, Nothing, Nothing, Nothing, Nothing, CommonUIs.ParseWFDATA(bv_strWfData, "DPT_CD"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "U", Nothing, Nothing, Nothing, _
                                            CommonUIs.ParseWFDATA(bv_strWfData, "USERNAME"), 1, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, objtrans)

                    Dim decCustomerTotalRate As Decimal = 0
                    Dim decPartyTotalRate As Decimal = 0
                    ' drCustomer = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairCompletionData.RSPNSBLTY_ID, " = '66'"))
                    If Not IsDBNull(dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_ID, "= '66'"))) AndAlso dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_ID, "= '66'")).ToString <> "" Then
                        decCustomerTotalRate = CDec(dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_ID, "= '66'")))
                    End If
                    If Not IsDBNull(dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_ID, "= '67'"))) AndAlso dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_ID, "= '67'")).ToString <> "" Then
                        decPartyTotalRate = CDec(dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_ID, "= '67'")))
                    End If
                    '  drParty = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairCompletionData.RSPNSBLTY_ID, " = '67'"))
                    If decCustomerTotalRate > 0 Then
                        If Not IsDBNull(drRepairCompletion.Item(RepairCompletionData.OWNR_APPRVL_RF)) Then
                            strApprovalRefNo = CStr(drRepairCompletion.Item(RepairCompletionData.OWNR_APPRVL_RF))
                        End If
                        objRepairCompletions.CreateRepairCharge(CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.CSTMR_ID)), _
                                                                drRepairCompletion.Item(RepairCompletionData.EQPMNT_NO).ToString, _
                                                                drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString, _
                                                                lngCreated, _
                                                                CommonUIs.iLng(dtActivityStatus.Rows(0).Item(RepairCompletionData.EQPMNT_TYP_ID)), _
                                                                drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_NO).ToString, _
                                                                CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.ACTVTY_DT)), _
                                                                CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT)).Date, _
                                                                decTotalCustomerMaterialCost, _
                                                                decTotalCustomerlabourCost, _
                                                                decCustomerApprovalAmount, _
                                                                decCustomerApprovalAmount, _
                                                                "U", _
                                                                True, _
                                                                bv_intDepotId, _
                                                                0, _
                                                                drRepairCompletion.Item(RepairCompletionData.YRD_LCTN).ToString, _
                                                                decCustomerCurrencyAmount, _
                                                                decCustomerCurrencyAmount, _
                                                                Nothing, _
                                                                i64ResCustomerId, _
                                                                strApprovalRefNo, _
                                                                Nothing, _
                                                                objtrans)
                    End If

                    If (Not IsDBNull(drRepairCompletion.Item(RepairCompletionData.INVCNG_PRTY_ID))) AndAlso decPartyTotalRate > 0 Then
                        If Not IsDBNull(drRepairCompletion.Item(RepairCompletionData.PRTY_APPRVL_RF)) Then
                            strApprovalRefNo = CStr(drRepairCompletion.Item(RepairCompletionData.PRTY_APPRVL_RF))
                        End If

                        objRepairCompletions.CreateRepairCharge(CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.CSTMR_ID)), _
                                                                drRepairCompletion.Item(RepairCompletionData.EQPMNT_NO).ToString, _
                                                                drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString, _
                                                                lngCreated, _
                                                                CommonUIs.iLng(dtActivityStatus.Rows(0).Item(RepairCompletionData.EQPMNT_TYP_ID)), _
                                                                drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_NO).ToString, _
                                                                CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.ACTVTY_DT)), _
                                                                CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT)).Date, _
                                                                decTotalPartyMaterialCost, _
                                                                decTotalPartylabourCost, _
                                                                decPartyApprovalAmount, _
                                                                decPartyApprovalAmount, _
                                                                "U", _
                                                                True, _
                                                                bv_intDepotId, _
                                                                CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.INVCNG_PRTY_ID)), _
                                                                drRepairCompletion.Item(RepairCompletionData.YRD_LCTN).ToString, _
                                                                decPartyCurrecnyAmount, _
                                                                decPartyCurrecnyAmount, _
                                                                Nothing, _
                                                                i64ResPartyId, _
                                                                strApprovalRefNo, _
                                                                Nothing, _
                                                                objtrans)
                    End If


                    'For GWS Repair Charge
                    If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then
                        decTotalCustomerMaterialCost = 0
                        decTotalCustomerlabourCost = 0
                        decCustomerApprovalAmount = 0
                        decCustomerCurrencyAmount = 0
                        Dim AgentId As String = Nothing

                        If Not drRepairCompletion.Item(GateinData.BLL_CD) Is DBNull.Value AndAlso drRepairCompletion.Item(GateinData.BLL_CD).ToString().ToUpper() = "AGENT" Then
                            AgentId = drRepairCompletion.Item(GateinData.AGNT_ID).ToString()
                        End If

                        If dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Select("CHK_BT='True'").Length > 0 Then

                            decTotalCustomerMaterialCost = CDec(dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("SUM(MTRL_CST_NC)", "CHK_BT='True'"))
                            decTotalCustomerlabourCost = CDec(dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("SUM(LBR_HR_CST_NC)", "CHK_BT='True'"))
                            decCustomerApprovalAmount = decTotalCustomerMaterialCost + decTotalCustomerlabourCost

                        Else
                            decTotalCustomerMaterialCost = 0
                            decTotalCustomerlabourCost = 0
                            decCustomerApprovalAmount = 0
                            decCustomerCurrencyAmount = 0
                        End If


                        objRepairCompletions.CreateRepairCharge(CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.CSTMR_ID)), _
                                                                                     drRepairCompletion.Item(RepairCompletionData.EQPMNT_NO).ToString, _
                                                                                     drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString, _
                                                                                     lngCreated, _
                                                                                     CommonUIs.iLng(dtActivityStatus.Rows(0).Item(RepairCompletionData.EQPMNT_TYP_ID)), _
                                                                                     drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_NO).ToString, _
                                                                                     CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.ACTVTY_DT)), _
                                                                                     CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT)).Date, _
                                                                                     decTotalCustomerMaterialCost, _
                                                                                     decTotalCustomerlabourCost, _
                                                                                     decCustomerApprovalAmount, _
                                                                                     decCustomerApprovalAmount, _
                                                                                     "U", _
                                                                                     True, _
                                                                                     bv_intDepotId, _
                                                                                     0, _
                                                                                     drRepairCompletion.Item(RepairCompletionData.YRD_LCTN).ToString, _
                                                                                     decCustomerCurrencyAmount, _
                                                                                     decCustomerCurrencyAmount, _
                                                                                     Nothing, _
                                                                                     i64ResCustomerId, _
                                                                                     strApprovalRefNo, _
                                                                                     AgentId, _
                                                                                     objtrans)


                    End If

                    objConfigs.CreateTracking(lngCreated, _
                                             CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.CSTMR_ID)), _
                                             drRepairCompletion.Item(RepairCompletionData.EQPMNT_NO).ToString, _
                                             "Repair Completion", _
                                             intEqpmnt_stts_id, _
                                             CStr(lngCreated), _
                                             CDate(CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT)).ToString("yyyy-MM-dd 00:00:00.000")), _
                                             drRepairCompletion.Item(RepairCompletionData.RMRKS_VC).ToString, _
                                             drRepairCompletion.Item(RepairCompletionData.YRD_LCTN).ToString, _
                                             drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString, _
                                             CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.INVCNG_PRTY_ID)), _
                                             drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_NO).ToString, _
                                             bv_strUserName, _
                                             Now, _
                                             bv_strUserName, _
                                             Now, _
                                             Nothing, _
                                             Nothing, _
                                             Nothing, _
                                             CommonUIs.iInt(drRepairCompletion.Item(RepairCompletionData.DPT_ID)), _
                                             0, _
                                             Nothing, _
                                             strEquipmentInfoRemarks, _
                                             False, _
                                             objtrans)
                Else

                    'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]

                    strEquipmentInfoRemarks = objConfigs.GetEquipmentInformation(drRepairCompletion.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                   bv_intDepotId, _
                                                                                   objtrans)
                    Dim strInwardTime As String = CStr(drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM))
                    Dim strHours As String
                    Dim strMinutes As String
                    If strInwardTime.Length = 5 Then
                        strHours = strInwardTime.Substring(0, 2)
                        strMinutes = strInwardTime.Substring(3, 2)
                    Else
                        strHours = strInwardTime.Substring(0, 1)
                        strMinutes = strInwardTime.Substring(2, 2)
                    End If
                    Dim datOldDatetime As DateTime = CDate(drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT))
                    Dim datNewDatetime As New DateTime(datOldDatetime.Year, datOldDatetime.Month, datOldDatetime.Day, CInt(strHours), CInt(strMinutes), 0)

                    objRepairCompletions.UpdateRepairCompletion(CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_ID)), _
                                                              CommonUIs.iInt(drRepairCompletion.Item(RepairCompletionData.RVSN_NO)) + 1, _
                                                              datNewDatetime, _
                                                              CommonUIs.iDec(drRepairCompletion.Item(RepairCompletionData.ACTL_MN_HR_NC)), _
                                                              CommonUIs.iInt(drRepairCompletion.Item(RepairCompletionData.DPT_ID)), _
                                                              bv_strUserName, _
                                                              bv_datCreatedDate, _
                                                              objtrans)
                    RepairEstimateId = CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_ID))
                    For Each drRepairCharge As DataRow In dtRepairCharge.Rows
                        objRepairCompletions.UpdateRepairCharge(CommonUIs.iLng(drRepairCharge.Item(RepairCompletionData.RPR_CHRG_ID)), _
                                                                CommonUIs.iDat(drRepairCharge.Item(RepairCompletionData.RPR_CMPLTN_DT)), _
                                                                drRepairCompletion.Item(RepairCompletionData.YRD_LCTN).ToString, _
                                                                bv_intDepotId, _
                                                                objtrans)
                    Next
                    objRepairCompletions.UpdateTracking("Repair Completion", _
                                                    CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_ID)), _
                                                    CDate(CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT)).ToString("yyyy-MM-dd 00:00:00.000")), _
                                                    drRepairCompletion.Item(RepairCompletionData.RMRKS_VC).ToString(), _
                                                    CStr(drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO)), _
                                                    CommonUIs.iLng(drRepairCompletion.Item(RepairCompletionData.INVCNG_PRTY_ID)), _
                                                    drRepairCompletion.Item(RepairCompletionData.YRD_LCTN).ToString(), _
                                                    bv_strUserName, _
                                                    Now, _
                                                    bv_intDepotId, _
                                                    strEquipmentInfoRemarks, _
                                                    objtrans)

                End If



                objRepairCompletions.UpdateActivityStatus(intEqpmnt_stts_id, _
                                                          drRepairCompletion.Item(RepairCompletionData.EQPMNT_NO).ToString, _
                                                          drRepairCompletion.Item(RepairCompletionData.YRD_LCTN).ToString, _
                                                          drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString, _
                                                          "Repair Completion", _
                                                          CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT)), _
                                                          CommonUIs.iDat(drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT)), _
                                                          drRepairCompletion.Item(RepairCompletionData.RMRKS_VC).ToString, _
                                                          objtrans)
                Dim objCommonUIS As New CommonUIs
                Dim lngCreatedAttachment As Long
                If bv_strMode = "edit" OrElse bv_strMode = "ReBind" Then
                    Dim blnDeleteAttachment As Boolean = False
                    For Each drAttachment As DataRow In br_dsRepairEstimate.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", CLng(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_ID))))
                        'For Checking  - No need for delete Approval details in attchemnt - SS
                        'CLng(drAttachment.Item(CommonUIData.RPR_ESTMT_ID)) - RepairEstimateId
                        If drAttachment.RowState <> DataRowState.Deleted Then
                            blnDeleteAttachment = objCommonUIS.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO).ToString, _
                                                                                              RepairEstimateId, _
                                                                                              drAttachment.Item(CommonUIData.RPR_ESTMT_NO).ToString, _
                                                                                              bv_intDepotId, _
                                                                                              objtrans)
                        Else
                            blnDeleteAttachment = objCommonUIS.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO, DataRowVersion.Original).ToString, _
                                                                                              RepairEstimateId, _
                                                                                             drAttachment.Item(CommonUIData.RPR_ESTMT_NO, DataRowVersion.Original).ToString, _
                                                                                             bv_intDepotId, _
                                                                                             objtrans)
                        End If
                    Next
                    If br_dsRepairEstimate.Tables(CommonUIData._ATTACHMENT).Rows.Count = 0 Then
                        For Each drAttachment As DataRow In br_dsRepairEstimate.Tables(CommonUIData._V_REPAIR_ESTIMATE).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", CLng(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_ID))))
                            If drAttachment.RowState <> DataRowState.Deleted Then
                                '  'CLng(drAttachment.Item(CommonUIData.RPR_ESTMT_ID)) - RepairEstimateId
                                blnDeleteAttachment = objCommonUIS.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO).ToString, _
                                                                                                  RepairEstimateId, _
                                                                                                  drAttachment.Item(CommonUIData.RPR_ESTMT_NO).ToString, _
                                                                                                  bv_intDepotId, _
                                                                                                  objtrans)
                            End If
                        Next
                    End If
                    For Each drAttachment As DataRow In br_dsRepairEstimate.Tables(CommonUIData._ATTACHMENT).Rows
                        If drAttachment.RowState = DataRowState.Deleted Then
                            'blnDeleteAttachment = objCommonUIS.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO, DataRowVersion.Original).ToString, _
                            '                                                                  CLng(drAttachment.Item(CommonUIData.RPR_ESTMT_ID, DataRowVersion.Original)), _
                            '                                                                  drAttachment.Item(CommonUIData.RPR_ESTMT_NO, DataRowVersion.Original).ToString, _
                            '                                                                  bv_intDepotId, _
                            '                                                                  objtrans)
                            ' issue Fix : insted of completion repair estimate id, approval estimate id is passed
                            blnDeleteAttachment = objCommonUIS.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO, DataRowVersion.Original).ToString, _
                                                                                             RepairEstimateId, _
                                                                                             drAttachment.Item(CommonUIData.RPR_ESTMT_NO, DataRowVersion.Original).ToString, _
                                                                                             bv_intDepotId, _
                                                                                             objtrans)
                        End If
                    Next
                End If
                For Each drAttachment As DataRow In br_dsRepairEstimate.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", CLng(drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_ID))))
                    If drAttachment.RowState <> DataRowState.Deleted Then
                        lngCreatedAttachment = objCommonUIS.CreateAttachment(RepairEstimateId,
                                                                             "Repair Completion", _
                                                                             drRepairCompletion.Item(RepairCompletionData.RPR_ESTMT_NO).ToString, _
                                                                             drRepairCompletion.Item(RepairCompletionData.GI_TRNSCTN_NO).ToString, _
                                                                             CStr(drAttachment.Item(CommonUIData.ATTCHMNT_PTH)), _
                                                                             CStr(drAttachment.Item(CommonUIData.ACTL_FL_NM)),
                                                                             bv_strUserName, _
                                                                             bv_datCreatedDate, _
                                                                             bv_intDepotId,
                                                                             objtrans)
                    End If
                Next

            Next
            objtrans.commit()
            Return True
        Catch ex As Exception
            objtrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objtrans = Nothing
        End Try
    End Function
#End Region

#Region "pvt_CalculateRepairChargesCompletion"
    Private Sub pvt_CalculateRepairChargesCompletion(ByVal bv_dsRepairEstimate As RepairCompletionDataSet, _
                                                     ByRef br_decTotalCustomerEstimateAmount As Decimal, _
                                                     ByRef br_decTotalPartyEstimateAmount As Decimal, _
                                                     ByRef br_decTotalCustomerLabourCost As Decimal, _
                                                     ByRef br_decTotalPartyLabourCost As Decimal, _
                                                     ByRef br_decTotalCustomerMaterialCost As Decimal, _
                                                     ByRef br_decTotalPartyMaterialCost As Decimal, _
                                                     ByRef br_i64ResponsibilityCustomerId As Int64, _
                                                     ByRef br_i64ResponsibilityPartyId As Int64, _
                                                     ByVal bv_blnReCalulateSummary As Boolean)
        Try
            If bv_blnReCalulateSummary Then
                If bv_dsRepairEstimate.Tables(RepairCompletionData._SUMMARY_DETAIL).Rows.Count > 0 Then
                    bv_dsRepairEstimate.Tables(RepairCompletionData._SUMMARY_DETAIL).Rows.Clear()
                End If
                For i = 0 To 1
                    Dim drRepairEst As DataRow = bv_dsRepairEstimate.Tables(RepairCompletionData._SUMMARY_DETAIL).NewRow()
                    drRepairEst.Item(RepairCompletionData.SMMRY_ID) = i + 1
                    drRepairEst.Item(RepairCompletionData.MN_HR_SMMRY) = 0.0
                    drRepairEst.Item(RepairCompletionData.LBR_RT_SMMRY) = 0.0
                    drRepairEst.Item(RepairCompletionData.MN_HR_RT_SMMRY) = 0.0
                    drRepairEst.Item(RepairCompletionData.MTRL_CST_SMMRY) = 0.0
                    drRepairEst.Item(RepairCompletionData.RSPNSBLTY_ID) = DBNull.Value
                    bv_dsRepairEstimate.Tables(RepairCompletionData._SUMMARY_DETAIL).Rows.Add(drRepairEst)
                Next

                For Each drRepair As DataRow In bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Rows
                    pvt_CalculateSummaryDetail(drRepair.Item(RepairCompletionData.LBR_HRS).ToString, _
                                               drRepair.Item(RepairCompletionData.LBR_RT).ToString, _
                                               drRepair.Item(RepairCompletionData.LBR_HR_CST_NC).ToString, _
                                               drRepair.Item(RepairCompletionData.MTRL_CST_NC).ToString, _
                                               CLng(drRepair.Item(RepairCompletionData.RSPNSBLTY_ID)), _
                                               drRepair.Item(RepairCompletionData.RSPNSBLTY_CD).ToString, _
                                               bv_dsRepairEstimate.Tables(RepairCompletionData._SUMMARY_DETAIL))
                Next
            End If

            Dim dtRepairEstimate As DataTable
            dtRepairEstimate = bv_dsRepairEstimate.Tables(RepairCompletionData._SUMMARY_DETAIL)
            br_decTotalCustomerEstimateAmount = CommonUIs.iDec(dtRepairEstimate.Rows(0).Item(RepairCompletionData.TTL_CST_SMMRY))
            br_decTotalPartyEstimateAmount = CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(RepairCompletionData.TTL_CST_SMMRY))
            br_decTotalCustomerLabourCost = CommonUIs.iDec(dtRepairEstimate.Rows(0).Item(RepairCompletionData.MN_HR_RT_SMMRY))
            br_decTotalPartyLabourCost = CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(RepairCompletionData.MN_HR_RT_SMMRY))
            br_decTotalCustomerMaterialCost = CommonUIs.iDec(dtRepairEstimate.Rows(0).Item(RepairCompletionData.MTRL_CST_SMMRY))
            br_decTotalPartyMaterialCost = CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(RepairCompletionData.MTRL_CST_SMMRY))
            If Not IsDBNull(dtRepairEstimate.Rows(0).Item(RepairCompletionData.RSPNSBLTY_ID)) Then
                br_i64ResponsibilityCustomerId = CLng(dtRepairEstimate.Rows(0).Item(RepairCompletionData.RSPNSBLTY_ID))
            End If
            If Not IsDBNull(dtRepairEstimate.Rows(1).Item(RepairCompletionData.RSPNSBLTY_ID)) Then
                br_i64ResponsibilityPartyId = CLng(dtRepairEstimate.Rows(1).Item(RepairCompletionData.RSPNSBLTY_ID))
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Sub

#End Region

#Region "pvt_CalculateSummaryDetail"
    Private Sub pvt_CalculateSummaryDetail(ByVal bv_strLabourHour As String, _
                                           ByVal bv_strLaborHourRate As String, _
                                           ByVal bv_strLabourHourCost As String, _
                                           ByVal bv_strMaterialCost As String, _
                                           ByVal bv_i64ResponsibilityId As Int64, _
                                           ByVal bv_strResponsibilityType As String, _
                                           ByRef br_dtSummaryDetail As DataTable)
        Try
            Select Case bv_strResponsibilityType
                Case "C"
                    br_dtSummaryDetail.Rows(0).Item(RepairCompletionData.MN_HR_SMMRY) = CommonUIs.iDec(br_dtSummaryDetail.Rows(0).Item(RepairCompletionData.MN_HR_SMMRY)) + CommonUIs.iDec(bv_strLabourHour)
                    br_dtSummaryDetail.Rows(0).Item(RepairCompletionData.LBR_RT_SMMRY) = CommonUIs.iDec(br_dtSummaryDetail.Rows(0).Item(RepairCompletionData.LBR_RT_SMMRY)) + CommonUIs.iDec(bv_strLaborHourRate)
                    br_dtSummaryDetail.Rows(0).Item(RepairCompletionData.MN_HR_RT_SMMRY) = CommonUIs.iDec(br_dtSummaryDetail.Rows(0).Item(RepairCompletionData.MN_HR_RT_SMMRY)) + CommonUIs.iDec(bv_strLabourHourCost)
                    br_dtSummaryDetail.Rows(0).Item(RepairCompletionData.MTRL_CST_SMMRY) = CommonUIs.iDec(br_dtSummaryDetail.Rows(0).Item(RepairCompletionData.MTRL_CST_SMMRY)) + CommonUIs.iDec(bv_strMaterialCost)
                    br_dtSummaryDetail.Rows(0).Item(RepairCompletionData.RSPNSBLTY_ID) = bv_i64ResponsibilityId
                Case "I"
                    br_dtSummaryDetail.Rows(1).Item(RepairCompletionData.MN_HR_SMMRY) = CommonUIs.iDec(br_dtSummaryDetail.Rows(1).Item(RepairCompletionData.MN_HR_SMMRY)) + CommonUIs.iDec(bv_strLabourHour)
                    br_dtSummaryDetail.Rows(1).Item(RepairCompletionData.LBR_RT_SMMRY) = CommonUIs.iDec(br_dtSummaryDetail.Rows(1).Item(RepairCompletionData.LBR_RT_SMMRY)) + CommonUIs.iDec(bv_strLaborHourRate)
                    br_dtSummaryDetail.Rows(1).Item(RepairCompletionData.MN_HR_RT_SMMRY) = CommonUIs.iDec(br_dtSummaryDetail.Rows(1).Item(RepairCompletionData.MN_HR_RT_SMMRY)) + CommonUIs.iDec(bv_strLabourHourCost)
                    br_dtSummaryDetail.Rows(1).Item(RepairCompletionData.MTRL_CST_SMMRY) = CommonUIs.iDec(br_dtSummaryDetail.Rows(1).Item(RepairCompletionData.MTRL_CST_SMMRY)) + CommonUIs.iDec(bv_strMaterialCost)
                    br_dtSummaryDetail.Rows(1).Item(RepairCompletionData.RSPNSBLTY_ID) = bv_i64ResponsibilityId
            End Select
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Sub
#End Region

#Region "pvt_CalculateTotalAmount"
    Private Sub pvt_CalculateTotalAmount(ByVal bv_dsRepairEstimate As RepairCompletionDataSet, _
                                         ByRef br_decTotalEstimateAmount As Decimal, _
                                         ByRef br_decCusLaborRate As Decimal, _
                                         ByRef br_decCusMaterialRate As Decimal, _
                                         ByRef br_decCusTotal As Decimal, _
                                         ByRef br_decInvLaborRate As Decimal, _
                                         ByRef br_decInvMaterialRate As Decimal, _
                                         ByRef br_decInvTotal As Decimal, _
                                         ByRef br_decRepairApprovalAmount As Decimal, _
                                         ByRef br_decCustomerCurrencyAmount As Decimal, _
                                         ByVal bv_decExchangeRate As Decimal)
        Try
            If bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                br_decTotalEstimateAmount = CDec(bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                If Not bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_RT)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'C'")).ToString = "" Then
                    br_decCusLaborRate = CDec(bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_RT)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'C'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'C'")).ToString = "" Then
                    br_decCusMaterialRate = CDec(bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'C'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'C'")).ToString = "" Then
                    br_decCusTotal = CDec(bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'C'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_RT)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'I'")).ToString = "" Then
                    br_decInvLaborRate = CDec(bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_RT)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'I'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'I'")).ToString = "" Then
                    br_decInvMaterialRate = CDec(bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'I'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'I'")).ToString = "" Then
                    br_decInvTotal = CDec(bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairCompletionData.RSPNSBLTY_CD, "= 'I'")))
                End If
                'Approval Amount Calculation
                If Not bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", "").ToString = "" Then
                    br_decRepairApprovalAmount = CDec(bv_dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                End If

                br_decCustomerCurrencyAmount = br_decCusTotal * bv_decExchangeRate
            End If

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Sub
#End Region

End Class
#End Region
