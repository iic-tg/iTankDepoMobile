Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.Data.SqlTypes
Imports System.Security.Cryptography
Imports System.Configuration
Imports iInterchange.iTankDepo.Business.Common

<ServiceContract()> _
Public Class RepairEstimate

#Region "GET :pub_GetExchangeRateWithEffectivedate() "
    <OperationContract()> _
    Public Function pub_GetExchangeRateWithEffectivedate(ByVal bv_lngFromCurrencyId As Int64, ByVal bv_lngToCurrencyId As Int64, ByVal bv_datCurrentDateTime As DateTime, ByVal bv_intDepotId As Int32) As RepairEstimateDataSet

        Try
            Dim dsRepairEstimateDataSet As RepairEstimateDataSet
            Dim objRepairEstimates As New RepairEstimates
            dsRepairEstimateDataSet = objRepairEstimates.GetExchangeRateWithEffectiveDate(bv_lngFromCurrencyId, bv_lngToCurrencyId, bv_datCurrentDateTime, bv_intDepotId)
            Return dsRepairEstimateDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET : pub_GetRepairEstimateDetailByRepairEstimationId() "

    <OperationContract()> _
    Public Function pub_GetRepairEstimateDetailByRepairEstimationId(ByVal bv_blnRepairEstimationId As Int64) As RepairEstimateDataSet

        Try
            Dim dsRepairEstimateDataSet As RepairEstimateDataSet
            Dim objRepairEstimates As New RepairEstimates
            dsRepairEstimateDataSet = objRepairEstimates.GetRepairEstimateDetailByRepairEstimationId(bv_blnRepairEstimationId)
            Return dsRepairEstimateDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "INSERT : pub_CreateRepairEstimate() TABLE NAME:Repair_Estimate"
    <OperationContract()> _
    Public Function pub_CreateRepairEstimate(ByVal bv_i64CustomerId As Int64, _
                                             ByVal bv_strCustomerCode As String, _
                                             ByVal bv_datRepairEstimationDate As DateTime, _
                                             ByVal bv_datOrginalEstimationDate As DateTime, _
                                             ByVal bv_dateGateinDate As DateTime, _
                                                       ByVal bv_strEquipmentNo As String, _
                                             ByVal bv_intEquipmentStatusId As Int64, _
                                             ByVal bv_strStatusCode As String, _
                                             ByVal bv_strGateinTransaction As String, _
                                             ByVal bv_datLastTestDate As DateTime, _
                                             ByVal bv_i64LastTestTypeId As Int64, _
                                             ByVal bv_strLastTestTypeCode As String, _
                                             ByVal bv_strValidityYear As String, _
                                             ByVal bv_datNextTestDate As DateTime, _
                                             ByVal bv_strSurveyorName As String, _
                                             ByVal bv_i64NextTestTypeId As Int64, _
                                             ByVal bv_strNextTestTypeCode As String, _
                                             ByVal bv_i64RepairTypeId As Int64, _
                                             ByVal bv_strRepairTypeCode As String, _
                                             ByVal bv_blnCertofCleanBit As Boolean, _
                                             ByVal bv_i64InvoicingPartyId As Int64, _
                                             ByVal bv_strInvoicingPartyCode As String, _
                                             ByVal bv_decLaborRate As Decimal, _
                                             ByVal bv_datApprovlDate As DateTime, _
                                             ByVal bv_strApprovlRef As String, _
                                             ByVal bv_datSurveyDate As DateTime, _
                                             ByVal bv_strSurveyName As String, _
                                             ByVal bv_i32DepotId As Int32, _
                                             ByVal bv_strWfData As String, _
                                             ByVal bv_strMode As String, _
                                             ByVal bv_strEstimationId As String, _
                                             ByRef bv_strRevisionNo As String, _
                                             ByRef bv_strEstimationNo As String, _
                                             ByVal bv_strRemarks As String, _
                                             ByRef br_dsRepairEstimateDataset As RepairEstimateDataSet, _
                                             ByRef br_strEIRNo As String, _
                                             ByVal bv_strPageMode As String, _
                                             ByVal bv_strApprovalRef As String, _
                                             ByVal bv_decCustomerEstimatedCost As Decimal, _
                                             ByVal bv_decCustomerApprovedCost As Decimal, _
                                             ByVal bv_strCreatedBy As String, _
                                             ByVal bv_datCreatedDate As DateTime, _
                                             ByVal bv_strModifiedBy As String, _
                                             ByVal bv_datModifiedDate As DateTime, _
                                             ByVal bv_strEquipmentStatus As String, _
                                             ByVal bv_strPartyApprovalRef As String, _
                                             ByRef br_strActivitySubmit As String, _
                                             ByVal bv_intActivityId As Integer, _
                                             ByVal str_057GWSKey As String, _
                                             ByVal bv_intPrevONHLocation As Integer, _
                                             ByVal bv_intMeasure As Integer, _
                                             ByVal bv_intUnit As Integer, _
                                             ByVal bv_strBillTo As String, _
                                             ByVal bv_strAgentName As String, _
                                             ByVal bv_str063Key As String, _
                                             ByVal bv_datPrevONHLocDate As DateTime, _
                                             ByVal bv_strConsignee As String, _
                                             ByVal bv_strTaxRate As Decimal, _
                                             ByVal bv_i64StatusId As Int64, _
                                             ByVal bv_intPrevONHLocationCode As String, _
                                             ByVal str_067InvoiceGenerationGWSBit As String, _
                                             ByVal bv_strMeasureCode As String, _
                                             ByVal bv_strUnitCode As String) As Long
        Dim objTrans As New Transactions
        Try
            Dim objRepairEstimate As New RepairEstimates
            Dim objCommons As New CommonUIs
            Dim dtRepairEstimate As DataTable = br_dsRepairEstimateDataset.Tables(RepairEstimateData._SUMMARY_DETAIL)
            Dim lngCreated As Long
            Dim lngRETCreated As Long
            Dim strTransmissionNo As String = "W" & Format(Now, "yyyyMMddHHmmssffff")
            Dim decEstimateAmount As Decimal
            Dim decApprovalAmount As Decimal
            Dim decCusLaborRate As Decimal
            Dim decCusMaterialRate As Decimal
            Dim decCusTotal As Decimal
            Dim decInvLaborRate As Decimal
            Dim decInvMaterialRate As Decimal
            Dim decInvTotal As Decimal
            Dim decTotalServicetax As Decimal
            Dim decLabourCost As Decimal
            Dim decCleaningCost As Decimal
            Dim strRCStatus As String = Nothing
            Dim strEstimationType As String = Nothing
            Dim blnRestimatebit As Boolean = False
            Dim strEqTypeCode As String = ""
            Dim strEqpCodeCode As String = ""
            Dim RepairEstimateId As Long = 0
            Dim datGateInDate As DateTime
            Dim decEstimateOriginal As Decimal = 0
            Dim bv_blnEDI As Boolean = False
            Dim intRevisionNo As Integer = 0
            Dim blnActivitySubmit As Boolean = False
            Dim strTempstatus As String
            Dim dsCustomer As CustomerDataSet
            Dim dsDepot As DepotDataSet
            Dim objISO As New Customers
            Dim objDepot As New Depots
            Dim decApprovalRET As Decimal
            Dim dtRepairGateIn As New DataTable
            Dim decCusLabourCost As Decimal
            Dim decInvLabourCost As Decimal
            Dim dtSummaryDetailGWS As New DataTable

            'If br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE) Is Nothing OrElse br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows.Count = 0 Then
            '    blnActivitySubmit = objCommons.GetActivitySubmit(bv_intActivityId, br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0), True, objTrans)
            'Else
            '    blnActivitySubmit = objCommons.GetActivitySubmit(bv_intActivityId, br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows(0), True, objTrans)
            'End If

            'If blnActivitySubmit = False Then

            Dim br_decDLaborRate As Decimal
            Dim br_decDMaterialRate As Decimal
            Dim br_decDTotal As Decimal
            Dim br_decULaborRate As Decimal
            Dim br_decUMaterialRate As Decimal
            Dim br_decUTotal As Decimal
            Dim br_decSLaborRate As Decimal
            Dim br_decSMaterialRate As Decimal
            Dim br_decSTotal As Decimal
            Dim br_decXLaborRate As Decimal
            Dim br_decXMaterialRate As Decimal
            Dim br_decXTotal As Decimal

            pvt_CalculateTotalAmount(br_dsRepairEstimateDataset, decEstimateAmount, decCusLaborRate, decCusMaterialRate, decCusTotal, decInvLaborRate, decInvMaterialRate, decInvTotal, br_decDLaborRate, br_decDMaterialRate, br_decDTotal, br_decULaborRate, br_decUMaterialRate, br_decUTotal, br_decSLaborRate, br_decSMaterialRate, br_decSTotal, br_decXLaborRate, br_decXMaterialRate, br_decXTotal, decApprovalAmount)

            'Insert into REPAIR_ESTIMATE table
            'decCusLabourCost = decCusTotal - decCusMaterialRate
            'decInvLabourCost = decInvTotal - decInvMaterialRate
            decCusLabourCost = decCusLaborRate
            decInvLabourCost = decInvLaborRate

            Dim dtEstimateDate As DateTime
            Dim dtOrginalDate As DateTime
            Dim dtActivityDate As DateTime
            Dim strActivityName As String = ""
            Dim dtRepairEstimateTemp As New DataTable
            Dim dtAttachment As New DataTable
            dtAttachment = br_dsRepairEstimateDataset.Tables(RepairEstimateData._ATTACHMENT).Clone()
            If br_dsRepairEstimateDataset.Tables(RepairEstimateData._ATTACHMENT).Rows.Count = 0 Then
                If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                    dtAttachment = objRepairEstimate.GetAttachmentByRepairEstimateId(bv_i32DepotId, "Repair Estimate", CLng(bv_strEstimationId)).Tables(RepairEstimateData._ATTACHMENT)
                    br_dsRepairEstimateDataset.Tables(RepairEstimateData._ATTACHMENT).Merge(dtAttachment)
                End If
            End If
            If bv_strPageMode = "Repair Approval" Then
                strTempstatus = "F"
                decApprovalRET = decApprovalAmount
                Dim strEstimationAmount As String = ""
                dtEstimateDate = bv_datRepairEstimationDate
                dtOrginalDate = bv_datOrginalEstimationDate
                dtActivityDate = bv_datApprovlDate
                strActivityName = bv_strPageMode
                intRevisionNo = objRepairEstimate.GetRevisionNoByEqpmntNo(bv_strEquipmentNo, bv_strGateinTransaction, bv_strEstimationNo, "Repair Estimate") + 1
                decEstimateAmount = objRepairEstimate.GetEstimationCostByEqpmntNo(bv_strEquipmentNo, bv_strGateinTransaction, bv_strEstimationNo, "Repair Estimate", bv_i32DepotId)
                dtRepairEstimateTemp = objRepairEstimate.GetRepairEstimateByGateinTransactionNo(bv_strEquipmentNo, bv_strGateinTransaction, bv_strEstimationNo, "Repair Estimate", bv_i32DepotId).Tables(RepairEstimateData._REPAIR_ESTIMATE)
                If dtRepairEstimateTemp.Rows.Count > 0 Then
                    If Not IsDBNull(dtRepairEstimateTemp.Rows(0).Item(RepairEstimateData.ORGNL_ESTMTN_AMNT_NC)) Then
                        decEstimateOriginal = CDec(dtRepairEstimateTemp.Rows(0).Item(RepairEstimateData.ORGNL_ESTMTN_AMNT_NC))
                    End If
                End If
            ElseIf bv_strPageMode = "Repair Estimate" Then
                strTempstatus = "D"
                dtEstimateDate = bv_datRepairEstimationDate
                dtOrginalDate = bv_datRepairEstimationDate
                dtActivityDate = bv_datRepairEstimationDate
                strActivityName = bv_strPageMode
                intRevisionNo = 0
                decEstimateOriginal = decEstimateAmount
            ElseIf bv_strPageMode = "Survey Completion" Then
                strTempstatus = "F"
                dtEstimateDate = bv_datRepairEstimationDate
                dtOrginalDate = bv_datRepairEstimationDate
                dtActivityDate = bv_datSurveyDate
                strActivityName = bv_strPageMode
                dtRepairEstimateTemp = objRepairEstimate.GetRepairEstimateByGateinTransactionNo(bv_strEquipmentNo, bv_strGateinTransaction, bv_strEstimationNo, "Repair Estimate", bv_i32DepotId).Tables(RepairEstimateData._REPAIR_ESTIMATE)
                If dtRepairEstimateTemp.Rows.Count > 0 Then
                    If Not IsDBNull(dtRepairEstimateTemp.Rows(0).Item(RepairEstimateData.ORGNL_ESTMTN_AMNT_NC)) Then
                        decEstimateOriginal = CDec(dtRepairEstimateTemp.Rows(0).Item(RepairEstimateData.ORGNL_ESTMTN_AMNT_NC))
                    End If
                End If
                intRevisionNo = objRepairEstimate.GetRevisionNoByEqpmntNo(bv_strEquipmentNo, bv_strGateinTransaction, bv_strEstimationNo, "Repair Completion") + 1
            End If
            bv_strRevisionNo = CStr(intRevisionNo)
            Dim strEqupTypeId As String = String.Empty
            If br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows.Count > 0 Then
                strEqupTypeId = br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.EQPMNT_TYP_ID).ToString
                strEqTypeCode = br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.EQPMNT_TYP_CD).ToString
                strEqpCodeCode = br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.EQPMNT_CD_CD).ToString
                datGateInDate = CommonUIs.iDat(br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.GTN_DT))
            End If
            'From Index Pattern
            Dim objIndexPattern As New IndexPatterns
            '  bv_strEstimationNo = objIndexPattern.GetMaxReferenceNo(RepairEstimateData._REPAIR_ESTIMATE, dtEstimateDate, objTrans, Nothing, bv_i32DepotId)
            'bv_strEstimationNo = objIndexPattern.GetMaxReferenceNo(String.Concat(RepairEstimateData._ESTIMATE_NO, ",", "Repair Estimate"), dtEstimateDate, objTrans, Nothing, bv_i32DepotId)
            'Estimate no generated only for Estimate. Approval & Completion used same Estimate No

            If bv_strPageMode.ToUpper() = "REPAIR ESTIMATE" Then
                If str_057GWSKey = "True" Then
                    bv_strEstimationNo = objIndexPattern.GetMaxReferenceNo(String.Concat(RepairEstimateData._ESTIMATE_NO, ",", "Repair Estimate NO", ",", bv_i64CustomerId), dtEstimateDate, objTrans, Nothing, bv_i32DepotId)
                Else
                    bv_strEstimationNo = objIndexPattern.GetMaxReferenceNo(String.Concat(RepairEstimateData._ESTIMATE_NO, ",", "Repair Estimate", ",", bv_i64CustomerId), dtEstimateDate, objTrans, Nothing, bv_i32DepotId)
                End If

            End If
            Dim strBillCd As String = bv_strBillTo
            If CInt(bv_strBillTo) = 144 Then
                strBillCd = "AGENT"
            ElseIf CInt(bv_strBillTo) = 145 Then
                strBillCd = "CUSTOMER"
            End If

            lngCreated = objRepairEstimate.CreateRepairEstimate(bv_i64CustomerId, _
                                                                bv_strEstimationNo, _
                                                                dtEstimateDate, _
                                                                dtOrginalDate, _
                                                                dtActivityDate, _
                                                                strActivityName, _
                                                                bv_dateGateinDate, _
                                                                bv_strEquipmentNo, _
                                                                bv_i64StatusId, _
                                                                bv_datLastTestDate, _
                                                                bv_i64LastTestTypeId, _
                                                                bv_strValidityYear, _
                                                                bv_datNextTestDate, _
                                                                bv_strSurveyorName, _
                                                                bv_i64NextTestTypeId, _
                                                                bv_i64RepairTypeId, _
                                                                bv_blnCertofCleanBit, _
                                                                bv_i64InvoicingPartyId, _
                                                                bv_decLaborRate, _
                                                                br_strEIRNo, _
                                                                intRevisionNo, _
                                                                bv_i32DepotId, _
                                                                bv_strGateinTransaction, _
                                                                bv_strApprovalRef, _
                                                                bv_strPartyApprovalRef, _
                                                                bv_strSurveyName, _
                                                                decEstimateOriginal, _
                                                                decEstimateAmount, _
                                                                decApprovalAmount, _
                                                                bv_decCustomerEstimatedCost, _
                                                                bv_decCustomerApprovedCost, _
                                                                bv_strRemarks, _
                                                                bv_strCreatedBy, _
                                                                bv_datCreatedDate, _
                                                                bv_strModifiedBy, _
                                                                bv_datModifiedDate, _
                                                                bv_intActivityId, _
                                                                bv_intPrevONHLocation, _
                                                                bv_intMeasure, _
                                                                bv_intUnit, _
                                                                bv_strBillTo, _
                                                                bv_strAgentName, _
                                                                strBillCd, _
                                                                bv_datPrevONHLocDate, _
                                                                bv_strTaxRate, _
                                                                bv_strConsignee, _
                                                                CInt(bv_intEquipmentStatusId), _
                                                                objTrans)

            'Repair Approval - Repair Type CR  - it will update to repair estimate
            If bv_strPageMode = "Repair Approval" Then
                objRepairEstimate.UpadateRepairEstimate_fromApproval(bv_strEquipmentNo, bv_strGateinTransaction, bv_i64RepairTypeId, objTrans)
            End If

            br_strEIRNo = bv_strEstimationNo

            Dim blnEqInfo As Boolean = False
            blnEqInfo = objRepairEstimate.UpdateEquipmentInformation(bv_strEquipmentNo, _
                                                                     bv_datLastTestDate, _
                                                                     bv_datNextTestDate, _
                                                                     bv_i64LastTestTypeId, _
                                                                     bv_i64NextTestTypeId, _
                                                                     bv_strValidityYear, _
                                                                     bv_strSurveyorName, _
                                                                     bv_i32DepotId, _
                                                                     objTrans)

            Dim strlessor As String
            Dim basCurr As String
            dsCustomer = objISO.getISOCODEbyCustomer(CLng(bv_i64CustomerId))
            dsDepot = objDepot.GetBankDetailLocalCurrency(bv_i32DepotId, 44)

            basCurr = CStr(dsDepot.Tables(DepotData._V_BANK_DETAIL).Rows(0).Item("CRRNCY_CD"))
            If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
            Else
                strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
            End If

            ' If bv_blnEDI Then
            'TODO : Insert In to RET Table
            'bv_strStatusCode - D
            ' br_dsRepairEstimateDataset = objRepairEstimate.GetGateinDetails(bv_strEquipmentNo)

            'dtRepairGateIn = objRepairEstimate.GetGateinDetails(bv_strEquipmentNo).Tables("V_GATEIN_DETAIL")
            'Dim strUnit As String = dtRepairGateIn.Rows(0).Item(RepairEstimateData.UNT_CD).ToString
            'Dim strMeasure As String = dtRepairGateIn.Rows(0).Item(RepairEstimateData.MSR_CD).ToString
            'Dim strMaterial As String = dtRepairGateIn.Rows(0).Item(RepairEstimateData.MTRL_CD).ToString
            'Dim strWeight As String = dtRepairGateIn.Rows(0).Item(RepairEstimateData.GRSS_WGHT_NC).ToString
            Dim strRefe As String = "C"
            Dim strEquipmentDescription As String = objRepairEstimate.GetEquipmentDescription(strEqpCodeCode, objTrans)
            If bv_strPageMode = "Repair Approval" Then
                decEstimateAmount = decApprovalAmount
            End If

            '  br_strEIRNo.Substring(3, 13)
            'Dim intEIRLength As Int32 = br_strEIRNo.Length
            'Dim intEIRMaxLenghth As Int32 = intEIRLength - 3
            'Dim strEIRNo As String = br_strEIRNo.Substring(3, intEIRMaxLenghth)
            Dim intEIRlenghth As Int32
            Dim intLenghth As Int32
            Dim strTrimEirNumber As String
            If br_strEIRNo.Length > 11 Then
                intEIRlenghth = br_strEIRNo.Length - 11
                intLenghth = br_strEIRNo.Length
                strTrimEirNumber = br_strEIRNo.Substring(intEIRlenghth, 11)
            Else
                strTrimEirNumber = br_strEIRNo
            End If
            If strEquipmentDescription.Length > 30 Then
                strEquipmentDescription = strEquipmentDescription.Substring(0, 30)
            End If
            If strEquipmentDescription.Length > 30 Then
                strEquipmentDescription = strEquipmentDescription.Substring(0, 30)
            End If
            Dim strEquipType As String
            Dim objEquipType As New EquipmentTypes
            Dim dsEquipType As EquipmentTypeDataSet
            If objCommons.GetMultiLocationSupportConfig().ToLower = "true" Then
                dsEquipType = objEquipType.GetEquipmentGroupByEquipmentTypeId(strEqupTypeId, CInt(objCommons.GetHeadQuarterID()))
            Else
                dsEquipType = objEquipType.GetEquipmentGroupByEquipmentTypeId(strEqupTypeId, bv_i32DepotId)
            End If

            If dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                strEquipType = CStr(dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows(0).Item(EquipmentTypeData.EQPMNT_GRP_CD))
            Else
                strEquipType = String.Empty
            End If
            lngRETCreated = objRepairEstimate.CreateRepairEstimateRet(lngCreated, strTransmissionNo, _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       strTrimEirNumber, _
                                                                       "", _
                                                                       intRevisionNo, _
                                                                       bv_datRepairEstimationDate, _
                                                                       bv_strEquipmentNo, _
                                                                       strRefe, _
                                                                       strEquipType, _
                                                                       strEqpCodeCode, _
                                                                       strEquipmentDescription, _
                                                                       " ", _
                                                                       CStr(datGateInDate.ToString("yyyyMMdd")), _
                                                                       CStr(datGateInDate.ToString("hh:mm")), _
                                                                       bv_intPrevONHLocationCode, _
                                                                       bv_datPrevONHLocDate.ToString("yyyyMMdd"), _
                                                                       strTempstatus, _
                                                                       "", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       strlessor, _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                        " ", _
                                                                       CommonUIs.ParseWFDATA(bv_strWfData, "DPT_CD"), _
                                                                       " ", _
                                                                       "", _
                                                                       bv_strSurveyorName, _
                                                                       " ", _
                                                                       Nothing, _
                                                                       " ", _
                                                                       "", _
                                                                       " ", _
                                                                       " ", _
                                                                       basCurr, _
                                                                       bv_decLaborRate, _
                                                                       "", _
                                                                       Nothing, _
                                                                       "", _
                                                                       bv_strMeasureCode, _
                                                                       bv_strUnitCode, _
                                                                       " ", _
                                                                       br_decULaborRate, _
                                                                       br_decUMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       br_decUTotal, _
                                                                       decInvLaborRate, _
                                                                       decInvMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       decInvTotal, _
                                                                       decCusLabourCost, _
                                                                       decCusMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       decCusTotal, _
                                                                       br_decDLaborRate, _
                                                                       br_decDMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       br_decDTotal, _
                                                                       br_decSLaborRate, _
                                                                       br_decSMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       br_decSTotal, _
                                                                       br_decXLaborRate, _
                                                                       br_decXMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       br_decXTotal, _
                                                                       decEstimateAmount, _
                                                                       "", _
                                                                       strTrimEirNumber, _
                                                                       " ", _
                                                                       decApprovalRET, _
                                                                       bv_strPartyApprovalRef, _
                                                                       CStr(CStr(Format(bv_datApprovlDate, "yyyyMMdd"))), _
                                                                       CStr(CStr(Format(dtEstimateDate, "yyyyMMdd"))), _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       "1", _
                                                                       "U", _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       bv_strModifiedBy, _
                                                                       1, _
                                                                       Nothing, _
                                                                       decCleaningCost, _
                                                                       Nothing, _
                                                                       decLabourCost, _
                                                                       decTotalServicetax, _
                                                                       "", _
                                                                       "", _
                                                                       strRCStatus, _
                                                                       strEstimationType, _
                                                                       objTrans)

            ' End If
            'Insert Into Detail table
            Dim drSelect As DataRow()
            drSelect = br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairEstimateData.ITM_ID, " is NULL AND ", RepairEstimateData.SB_ITM_ID, " is NULL AND ", RepairEstimateData.RPR_ID, " is NULL AND ", RepairEstimateData.DMG_ID, " is NULL"), "")
            If drSelect.Length = 0 Then
                Dim lngWD_LINE As Int64 = 0
                Dim blnTrafiffValidation As Boolean = False
                Dim i64TariffDetailId As Int64 = Nothing
                For Each drRepairEstimate As DataRow In br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
                    blnTrafiffValidation = False
                    i64TariffDetailId = Nothing
                    If drRepairEstimate.RowState <> DataRowState.Deleted Then

                        If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then
                            Dim objCompletions As New RepairCompletions

                            If Not drRepairEstimate.Item(RepairEstimateData.TRFF_CD_DTL_ID) Is DBNull.Value Then

                                Dim dt As New DataTable
                                dt = objCompletions.ValidateTariff_ByDetailId(CLng(drRepairEstimate.Item(RepairEstimateData.TRFF_CD_DTL_ID)), _
                                                                              CDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS)), _
                                                                              CDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC)), objTrans)

                                i64TariffDetailId = CLng(drRepairEstimate.Item(RepairEstimateData.TRFF_CD_DTL_ID))
                                If dt.Rows.Count > 0 Then
                                    blnTrafiffValidation = True
                                End If
                            Else
                                blnTrafiffValidation = False
                            End If

                        End If

                        Dim lngDetail As Long
                        Dim strMatCd As String = Nothing
                        If Not drRepairEstimate.Item(RepairEstimateData.MTRL_CD) Is Nothing Then
                            strMatCd = drRepairEstimate.Item(RepairEstimateData.MTRL_CD).ToString
                        End If
                        If drRepairEstimate.RowState <> DataRowState.Deleted Then
                            lngDetail = objRepairEstimate.CreateRepairEstimateDetail(lngCreated, _
                                                                                     CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.ITM_ID)), _
                                                                                     CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.SB_ITM_ID)), _
                                                                                     CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.DMG_ID)), _
                                                                                     CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RPR_ID)), _
                                                                                     CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS)), _
                                                                                     drRepairEstimate.Item(RepairEstimateData.DMG_RPR_DSCRPTN).ToString, _
                                                                                     CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_RT)), _
                                                                                     CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC)), _
                                                                                     CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC)), _
                                                                                     CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.TTL_CST_NC)), _
                                                                                     CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_ID)), _
                                                                                     CommonUIs.iBool(drRepairEstimate.Item(RepairEstimateData.CHK_BT)), _
                                                                                     strMatCd, _
                                                                                     drRepairEstimate.Item(RepairEstimateData.TX_RSPNSBLTY_ID).ToString, _
                                                                                     drRepairEstimate.Item(RepairEstimateData.RMRKS).ToString, _
                                                                                     blnTrafiffValidation, _
                                                                                     i64TariffDetailId, _
                                                                                     objTrans)

                            drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = lngDetail
                            '  If bv_blnEDI Then
                            Dim strWDResponsibility As String
                            If bv_str063Key.ToLower = "true" Then
                                strWDResponsibility = drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD).ToString
                            Else
                                If drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD).ToString = "C" Then
                                    strWDResponsibility = "O"
                                Else
                                    strWDResponsibility = "U"
                                End If
                            End If
                            lngWD_LINE = lngWD_LINE + 1

                            'If br_strEIRNo.Length > 11 Then
                            '    intEIRlenghth = br_strEIRNo.Length - 11
                            '    intLenghth = br_strEIRNo.Length
                            '    strTrimEirNumber = br_strEIRNo.Substring(intEIRlenghth, 11)
                            'Else
                            '    strTrimEirNumber = br_strEIRNo
                            'End If
                            Dim check As Boolean = False
                            'If check Then

                            lngDetail = objRepairEstimate.CreateRepairEstimateDetailRet(lngDetail, _
                                                                                        strTransmissionNo, _
                                                                                        strTrimEirNumber, _
                                                                                       intRevisionNo, _
                                                                                        bv_datRepairEstimationDate, _
                                                                                        bv_strEquipmentNo, _
                                                                                        drRepairEstimate.Item(RepairEstimateData.LBR_RT).ToString, _
                                                                                        drRepairEstimate.Item(RepairEstimateData.RPR_CD).ToString, _
                                                                                        0, _
                                                                                        drRepairEstimate.Item(RepairEstimateData.DMG_CD).ToString, _
                                                                                        CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS)), _
                                                                                        CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC)), _
                                                                                        drRepairEstimate.Item(RepairEstimateData.DMG_RPR_DSCRPTN).ToString, _
                                                                                     "U", _
                                                                                        lngCreated, _
                                                                                        1, _
                                                                                        lngWD_LINE, _
                                                                                        drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD).ToString, _
                                                                                        drRepairEstimate.Item(RepairEstimateData.SB_ITM_CD).ToString, _
                                                                                         drRepairEstimate.Item(RepairEstimateData.ITM_CD).ToString, _
                                                                                         bv_strUnitCode, _
                                                                                         drRepairEstimate.Item(RepairEstimateData.MTRL_CD).ToString, _
                                                                                        objTrans)



                            'End If
                            'bv_strStatusCode "U"
                            'End If
                        End If
                    End If


                    'If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then

                    '    Dim objCompletion As New RepairCompletions
                    '    'Delete existing Estimate details
                    '    objCompletion.DeleteRepairEstimateDetailByEstimateId(lngCreated, objTrans)


                    'End If


                Next
            End If
            Dim strYardLocation As String = ""
            If br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows.Count > 0 Then
                If Not br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.YRD_LCTN) Is Nothing Then
                    If Not IsDBNull(br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.YRD_LCTN)) Then
                        strYardLocation = CStr(br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.YRD_LCTN))
                    End If
                Else
                    strYardLocation = ""
                End If
            Else
                strYardLocation = ""
            End If
            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
            Dim strEquipmentInfoRemarks As String = String.Empty
            strEquipmentInfoRemarks = objCommons.GetEquipmentInformation(bv_strEquipmentNo, _
                                                                           bv_i32DepotId, _
                                                                           objTrans)

            objCommons.CreateTracking(lngCreated, bv_i64CustomerId, _
                                             bv_strEquipmentNo, _
                                             bv_strPageMode, _
                                             bv_intEquipmentStatusId, _
                                             CStr(lngCreated), _
                                             dtActivityDate, _
                                             bv_strRemarks, _
                                             strYardLocation, _
                                             bv_strGateinTransaction, _
                                             bv_i64InvoicingPartyId, _
                                             br_strEIRNo, _
                                             bv_strModifiedBy, _
                                             Now, _
                                             bv_strModifiedBy, _
                                             Now, _
                                             Nothing, _
                                             Nothing, _
                                             Nothing, _
                                             bv_i32DepotId, _
                                             0, _
                                             Nothing, _
                                             strEquipmentInfoRemarks, _
                                             False, _
                                             objTrans)
            '  strEquipmentInfoRemarks, _
            'REPAIR FLOW SETTING: 037
            'CR-001 (AWE_TO_AWP)
            If str_057GWSKey.ToLower = "true" Then
                If (bv_strEquipmentStatus <> "" Or bv_strEquipmentStatus <> Nothing) And (bv_strPageMode = "Repair Estimate") Then
                    bv_intEquipmentStatusId = CLng(objRepairEstimate.GetEquipmentStatusDetail("AWP", bv_i32DepotId, objTrans))
                End If
                If (bv_strEquipmentStatus <> "" Or bv_strEquipmentStatus <> Nothing) And (bv_strPageMode = "Repair Approval") Then
                    bv_i64StatusId = CLng(objRepairEstimate.GetEquipmentStatusDetailFromStatus(bv_strStatusCode, bv_i32DepotId, objTrans))
                    'bv_i64StatusId = CLng(objRepairEstimate.GetEquipmentStatusDetailFromStatus(bv_strEquipmentStatus, bv_i32DepotId, objTrans))
                End If

            Else
                If (bv_strEquipmentStatus <> "" Or bv_strEquipmentStatus <> Nothing) And (bv_strPageMode = "Repair Estimate") AndAlso objCommons.GetMultiLocationSupportConfig().ToLower = "false" Then
                    bv_i64StatusId = CLng(objRepairEstimate.GetEquipmentStatusDetail(bv_strEquipmentStatus, bv_i32DepotId, objTrans))
                ElseIf (bv_strEquipmentStatus <> "" Or bv_strEquipmentStatus <> Nothing) And (bv_strPageMode = "Repair Estimate") Then
                    bv_i64StatusId = CLng(objRepairEstimate.GetEquipmentStatusDetail(bv_strEquipmentStatus, CInt(objCommons.GetHeadQuarterID()), objTrans))
                End If
            End If

            objRepairEstimate.UpdateActivityStatus(bv_intEquipmentStatusId, _
                                                   bv_strEquipmentNo, _
                                                   bv_strGateinTransaction, _
                                                   bv_strRemarks, _
                                                   bv_strPageMode, _
                                                   dtActivityDate, _
                                                   objTrans)

            Dim objCommonUIS As New CommonUIs
            Dim lngCreatedAttachment As Long
            If bv_strMode = "edit" Then
                Dim blnDeleteAttachment As Boolean = False
                For Each drAttachment As DataRow In br_dsRepairEstimateDataset.Tables(RepairEstimateData._ATTACHMENT).Rows
                    blnDeleteAttachment = objCommonUIS.DeleteAttachmentByActivityName(drAttachment.Item(RepairEstimateData.GI_TRNSCTN_NO).ToString, _
                                                                                      CLng(drAttachment.Item(RepairEstimateData.RPR_ESTMT_ID)), _
                                                                                      drAttachment.Item(RepairEstimateData.RPR_ESTMT_NO).ToString, _
                                                                                      bv_i32DepotId, _
                                                                                      objTrans)
                Next
                If br_dsRepairEstimateDataset.Tables(RepairEstimateData._ATTACHMENT).Rows.Count = 0 Then
                    For Each drAttachment As DataRow In br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows
                        blnDeleteAttachment = objCommonUIS.DeleteAttachmentByActivityName(drAttachment.Item(RepairEstimateData.GI_TRNSCTN_NO).ToString, _
                                                                                          CLng(drAttachment.Item(RepairEstimateData.RPR_ESTMT_ID)), _
                                                                                          drAttachment.Item(RepairEstimateData.RPR_ESTMT_NO).ToString, _
                                                                                          bv_i32DepotId, _
                                                                                          objTrans)
                    Next
                End If
            End If
            For Each drAttachment As DataRow In br_dsRepairEstimateDataset.Tables(RepairEstimateData._ATTACHMENT).Rows
                If drAttachment.RowState <> DataRowState.Deleted Then
                    lngCreatedAttachment = objCommonUIS.CreateAttachment(lngCreated,
                                                                         bv_strPageMode, _
                                                                         br_strEIRNo, _
                                                                         bv_strGateinTransaction, _
                                                                         CStr(drAttachment.Item(RepairEstimateData.ATTCHMNT_PTH)), _
                                                                         CStr(drAttachment.Item(RepairEstimateData.ACTL_FL_NM)),
                                                                         bv_strModifiedBy, _
                                                                         bv_datModifiedDate, _
                                                                         bv_i32DepotId,
                                                                         objTrans)
                End If
            Next
            'Else
            'If br_strActivitySubmit.Length > 0 Then
            '    br_strActivitySubmit = String.Concat(br_strActivitySubmit, ",")
            'End If
            'br_strActivitySubmit = String.Concat(br_strActivitySubmit, br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows(0).Item(RepairEstimateData.EQPMNT_NO))
            'End If
            objTrans.commit()
            Dim dtCleaning As DataTable
            dtCleaning = objRepairEstimate.GetAdditionalCleaningBit(bv_strEquipmentNo, bv_strGateinTransaction, bv_i32DepotId)
            If dtCleaning.Rows.Count > 0 Then
                iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, String.Concat("EQPMNT_NO : ", bv_strEquipmentNo, _
                                                                        "  GI_TRNSCTN_NO : ", bv_strGateinTransaction, _
                                                                       "ADDTNL_CLNNG_BT : ", dtCleaning.Rows(0).Item(ChangeOfStatusData.ADDTNL_CLNNG_BT), _
                                                                        "ADDTNL_CLNNG_FLG : ", dtCleaning.Rows(0).Item(CleaningData.ADDTNL_CLNNG_FLG)))
            End If
            Return lngCreated
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function

#End Region

#Region "UPDATE : pub_ModifyRepairEstimate() TABLE NAME:Repair_Estimate"

    <OperationContract()> _
    Public Function pub_ModifyRepairEstimate(ByVal bv_i64RepairEstimationId As Int64, _
                                             ByVal bv_i64CustomerId As Int64, _
                                             ByVal bv_strCustomerCode As String, _
                                             ByVal bv_datRepairEstimationDate As DateTime, _
                                             ByVal bv_datOrginalEstimationDate As DateTime, _
                                             ByVal bv_strEquipmentNo As String, _
                                             ByVal bv_i64EquipmentSttsId As Int64, _
                                             ByVal bv_strStatusCode As String, _
                                             ByVal bv_strRepairEstimateNo As String, _
                                             ByVal bv_strGateinTransaction As String, _
                                             ByVal bv_datLastTestDate As DateTime, _
                                             ByVal bv_i64LastTestTypeId As Int64, _
                                             ByVal bv_strLastTestTypeCode As String, _
                                             ByVal bv_strValidityYear As String, _
                                             ByVal bv_datNextTestDate As DateTime, _
                                             ByVal bv_strSurveyorName As String, _
                                             ByVal bv_i64NextTestTypeId As Int64, _
                                             ByVal bv_strNextTestTypeCode As String, _
                                             ByVal bv_i64RepairTypeId As Int64, _
                                             ByVal bv_strRepairTypeCode As String, _
                                             ByVal bv_blnCertofCleanBit As Boolean, _
                                             ByVal bv_i64InvoicingPartyId As Int64, _
                                             ByVal bv_strInvoicingPartyCode As String, _
                                             ByVal bv_decLaborRate As Decimal, _
                                             ByVal bv_decApprovalAmount As Decimal, _
                                             ByVal bv_datApprovalDate As DateTime, _
                                             ByVal bv_strApprovalRef As String, _
                                             ByVal bv_strPartyApprovalRef As String, _
                                             ByVal bv_datSurveyDate As DateTime, _
                                             ByVal bv_strSurveyName As String, _
                                             ByVal bv_strPageMode As String, _
                                             ByVal bv_i32DepotId As Int32, _
                                             ByVal bv_strWfData As String, _
                                             ByVal bv_strMode As String, _
                                             ByVal bv_strEstimationId As String, _
                                             ByRef bv_strRevisionNo As String, _
                                             ByVal bv_strRemarks As String, _
                                             ByVal bv_strEstimationNo As String, _
                                             ByVal bv_decCustomerEstimatedCost As Decimal, _
                                             ByVal bv_decCustomerApprovedCost As Decimal, _
                                             ByVal bv_strModifiedBy As String, _
                                             ByVal bv_datModifiedDate As DateTime, _
                                             ByVal bv_strEquipmentStatus As String, _
                                             ByRef br_dsRepairEstimateDataset As RepairEstimateDataSet, _
                                             ByRef br_strActivitySubmit As String, _
                                             ByVal bv_intActivityId As Integer, _
                                             ByVal bv_intPrevONHLocation As String, _
                                             ByVal bv_datPrevONHDat As DateTime, _
                                             ByVal bv_intMeasure As String, _
                                             ByVal bv_intUnit As String, _
                                             ByVal bv_strBillTo As String, _
                                             ByVal bv_strAgentName As String, _
                                             ByVal bv_strConsignee As String, _
                                             ByVal bv_strTaxRate As String, _
                                             ByVal bv_i64StatusId As Int64, _
                                             ByVal bv_intPrevONHLocationCode As String, _
                                             ByVal str_067InvoiceGenerationGWSBit As String, _
                                             ByVal bv_strMeasureCode As String, _
                                             ByVal bv_strUnitCode As String, _
                                             ByVal str_057GWSKey As String) As Long

        Dim objtrans As New Transactions
        Try
            Dim lngCreated As Long
            Dim lngRETCreated As Long

            'Dim decCustomerCost As Decimal
            'Dim decPartyCost As Decimal
            Dim dtRepairEstimate As DataTable = br_dsRepairEstimateDataset.Tables(RepairEstimateData._SUMMARY_DETAIL)
            Dim strRCStatus As String = Nothing
            Dim strEstimationType As String = Nothing
            Dim decEstimateAmount As Decimal
            Dim decCusLaborRate As Decimal
            Dim decCusMaterialRate As Decimal
            Dim decCusTotal As Decimal
            Dim decInvLaborRate As Decimal
            Dim decInvMaterialRate As Decimal
            Dim decInvTotal As Decimal
            Dim blnRestimatebit As Boolean = False
            Dim strEqTypeCode As String = ""
            Dim strEqpCodeCode As String = ""
            Dim datGateInDate As DateTime
            Dim objCommonUIS As New CommonUIs
            Dim decApprovalAmount As Decimal
            Dim strAttachmentExist As String = ""
            Dim intRevisionNo As Integer = 0
            Dim bv_blnEDI As Boolean = False
            Dim strTransmissionNo As String = "W" & Format(Now, "yyyyMMddHHmmssffff")
            Dim blnActivitySubmit As Boolean = False
            Dim strTempstatus As String = String.Empty
            Dim dsCustomer As CustomerDataSet
            Dim dsDepot As DepotDataSet
            Dim objISO As New Customers
            Dim objDepot As New Depots
            Dim decApprovalRET As Decimal
            Dim decCusLabourCost As Decimal
            Dim decInvLabourCost As Decimal
            'blnActivitySubmit = objCommonUIS.GetActivitySubmit(bv_intActivityId, br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows(0), True, objtrans)
            'If blnActivitySubmit = False Then

            'pvt_CalculateTotalAmount(br_dsRepairEstimateDataset, decEstimateAmount, decCusLaborRate, decCusMaterialRate, decCusTotal, decInvLaborRate, decInvMaterialRate, decInvTotal, decApprovalAmount)
            Dim br_decDLaborRate As Decimal
            Dim br_decDMaterialRate As Decimal
            Dim br_decDTotal As Decimal
            Dim br_decULaborRate As Decimal
            Dim br_decUMaterialRate As Decimal
            Dim br_decUTotal As Decimal
            Dim br_decSLaborRate As Decimal
            Dim br_decSMaterialRate As Decimal
            Dim br_decSTotal As Decimal
            Dim br_decXLaborRate As Decimal
            Dim br_decXMaterialRate As Decimal
            Dim br_decXTotal As Decimal

            'pvt_CalculateTotalAmount(br_dsRepairEstimateDataset, decEstimateAmount, decCusLaborRate, decCusMaterialRate, decCusTotal, decInvLaborRate, decInvMaterialRate, decInvTotal, decApprovalAmount, br_decDLaborRate, br_decDMaterialRate, br_decDTotal, br_decULaborRate, br_decUMaterialRate, br_decUTotal, br_decSLaborRate, br_decSMaterialRate, br_decSTotal, br_decXLaborRate, br_decXMaterialRate, br_decXTotal)
            pvt_CalculateTotalAmount(br_dsRepairEstimateDataset, decEstimateAmount, decCusLaborRate, decCusMaterialRate, decCusTotal, decInvLaborRate, decInvMaterialRate, decInvTotal, br_decDLaborRate, br_decDMaterialRate, br_decDTotal, br_decULaborRate, br_decUMaterialRate, br_decUTotal, br_decSLaborRate, br_decSMaterialRate, br_decSTotal, br_decXLaborRate, br_decXMaterialRate, br_decXTotal, decApprovalAmount)

            'for RET insertion Labour Cost
            decCusLabourCost = decCusLaborRate
            decInvLabourCost = decInvLaborRate

            Dim objRepairEstimate As New RepairEstimates
            Dim blnUpdated As Boolean
            Dim dtEstimateDate As DateTime
            Dim dtOrginalDate As DateTime
            Dim dtActivityDate As DateTime
            Dim strActivityName As String = ""
            If bv_strPageMode = "Repair Approval" Then
                strTempstatus = "F"
                decApprovalRET = decApprovalAmount
                dtEstimateDate = bv_datRepairEstimationDate
                dtOrginalDate = bv_datOrginalEstimationDate
                dtActivityDate = bv_datApprovalDate
                strActivityName = bv_strPageMode
                decEstimateAmount = objRepairEstimate.GetEstimationCostByEqpmntNo(bv_strEquipmentNo, bv_strGateinTransaction, bv_strEstimationNo, "Repair Estimate", bv_i32DepotId)
            ElseIf bv_strPageMode = "Repair Estimate" Then
                strTempstatus = "D"
                dtEstimateDate = bv_datRepairEstimationDate
                dtOrginalDate = bv_datRepairEstimationDate
                dtActivityDate = bv_datRepairEstimationDate
                strActivityName = bv_strPageMode
            ElseIf bv_strPageMode = "Survey Completion" Then
                strTempstatus = "F"
                dtEstimateDate = bv_datRepairEstimationDate
                dtOrginalDate = bv_datRepairEstimationDate
                dtActivityDate = bv_datSurveyDate
                strActivityName = bv_strPageMode
            End If
            Dim dtAttachment As New DataTable
            dtAttachment = br_dsRepairEstimateDataset.Tables(RepairEstimateData._ATTACHMENT).Clone()
            If br_dsRepairEstimateDataset.Tables(RepairEstimateData._ATTACHMENT).Rows.Count = 0 Then
                If bv_i64RepairEstimationId <> 0 Then
                    dtAttachment = objRepairEstimate.GetAttachmentByRepairEstimateId(bv_i32DepotId, "Repair Estimate", bv_i64RepairEstimationId).Tables(RepairEstimateData._ATTACHMENT)
                    br_dsRepairEstimateDataset.Tables(RepairEstimateData._ATTACHMENT).Merge(dtAttachment)
                End If
            End If
            ' If bv_strRevisionNo Is Nothing Then
            intRevisionNo = objRepairEstimate.GetRevisionNoByRepairEstimateId(bv_i64RepairEstimationId)
            intRevisionNo = intRevisionNo + 1
            'Else
            '    strRevisionNo = CStr(bv_strRevisionNo)
            'End If
            bv_strRevisionNo = CStr(intRevisionNo)
            Dim strEqupTypeId As String = String.Empty
            If br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows.Count > 0 Then
                strEqupTypeId = br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.EQPMNT_TYP_ID).ToString
                strEqTypeCode = br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.EQPMNT_TYP_CD).ToString
                strEqpCodeCode = br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.EQPMNT_CD_CD).ToString
                datGateInDate = CommonUIs.iDat(br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.GTN_DT))
            End If

            Dim strBillCd As String = bv_strBillTo
            If CInt(bv_strBillTo) = 144 Then
                strBillCd = "AGENT"
            ElseIf CInt(bv_strBillTo) = 145 Then
                strBillCd = "CUSTOMER"
            End If

            Dim strOnHireDate As String = Nothing

            If bv_datPrevONHDat <> Nothing Then
                strOnHireDate = bv_datPrevONHDat.ToString()
            End If

            blnUpdated = objRepairEstimate.UpdateRepairEstimate(bv_i64RepairEstimationId, _
                                                                intRevisionNo, _
                                                                bv_i64CustomerId, _
                                                                dtEstimateDate, _
                                                                dtOrginalDate, _
                                                                dtActivityDate, _
                                                                bv_i64EquipmentSttsId, _
                                                                bv_datLastTestDate, _
                                                                bv_i64LastTestTypeId, _
                                                                bv_strValidityYear, _
                                                                bv_datNextTestDate, _
                                                                bv_strSurveyorName, _
                                                                bv_i64NextTestTypeId, _
                                                                bv_i64RepairTypeId, _
                                                                bv_blnCertofCleanBit, _
                                                                bv_i64InvoicingPartyId, _
                                                                decEstimateAmount, _
                                                                decApprovalAmount, _
                                                                bv_datApprovalDate, _
                                                                bv_strApprovalRef, _
                                                                bv_strPartyApprovalRef, _
                                                                bv_datSurveyDate, _
                                                                bv_strSurveyName, _
                                                                bv_decCustomerEstimatedCost, _
                                                                bv_decCustomerApprovedCost, _
                                                                bv_strRemarks, _
                                                                bv_i32DepotId, _
                                                                bv_strModifiedBy, _
                                                                bv_datModifiedDate, _
                                                                bv_intPrevONHLocation, _
                                                                strOnHireDate, _
                                                                bv_intMeasure, _
                                                                bv_intUnit, _
                                                                bv_strBillTo, _
                                                                bv_strAgentName, _
                                                                bv_strTaxRate, _
                                                                bv_strConsignee, _
                                                                bv_i64StatusId, _
                                                                strBillCd, _
                                                                objtrans)

            'Repair Approval - Repair Type CR  - it will update to repair estimate
            If bv_strPageMode = "Repair Approval" Then
                objRepairEstimate.UpadateRepairEstimate_fromApproval(bv_strEquipmentNo, bv_strGateinTransaction, bv_i64RepairTypeId, objtrans)
            End If

            'Newly added for EStimate RET insertion SUR
            Dim strlessor As String
            Dim basCurr As String
            dsDepot = objDepot.GetBankDetailLocalCurrency(1, 44)
            If dsDepot.Tables(DepotData._V_BANK_DETAIL).Rows.Count > 0 Then
                basCurr = CStr(dsDepot.Tables(DepotData._V_BANK_DETAIL).Rows(0).Item("CRRNCY_CD"))
            Else
                basCurr = ""
            End If
            dsCustomer = objISO.getISOCODEbyCustomer(CLng(bv_i64CustomerId))
            If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
            Else
                strlessor = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
            End If
            ' If objRepairEstimate.GetRepairEstimateRET(bv_i64RepairEstimationId, objtrans) Then
            Dim strRef As String = "C"
            Dim strEquipmentDescription As String = objRepairEstimate.GetEquipmentDescription(strEqpCodeCode, objtrans)
            If bv_strPageMode = "Repair Approval" Then
                decEstimateAmount = decApprovalAmount
            End If

            Dim intEIRlenghth As Int32
            Dim intLenghth As Int32
            Dim strTrimEirNumber As String
            If bv_strEstimationNo.Length > 11 Then
                intEIRlenghth = bv_strEstimationNo.Length - 11
                intLenghth = bv_strEstimationNo.Length
                strTrimEirNumber = bv_strEstimationNo.Substring(intEIRlenghth, 11)
            Else
                strTrimEirNumber = bv_strEstimationNo
            End If
            'bv_strEstimationNo.Substring(3, 13)
            'objRepairEstimate.CreateRepairEstimateRet(bv_i64RepairEstimationId, strTransmissionNo, _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                     strTrimEirNumber, _
            '                                                      "", _
            '                                                      CInt(bv_strRevisionNo), _
            '                                                      bv_datRepairEstimationDate, _
            '                                                      bv_strEquipmentNo, _
            '                                                      strRef, _
            '                                                      strEqTypeCode, _
            '                                                      strEqpCodeCode, _
            '                                                      strEquipmentDescription, _
            '                                                      " ", _
            '                                                      CStr(datGateInDate.ToString("yyyyMMdd")), _
            '                                                      CStr(datGateInDate.ToString("hh:mm")), _
            '                                                     bv_intPrevONHLocationCode, _
            '                                                      bv_datPrevONHDat.ToString("yyyyMMdd"), _
            '                                                      strTempstatus, _
            '                                                      "", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      strlessor, _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      CommonUIs.ParseWFDATA(bv_strWfData, "DPT_CD"), _
            '                                                      " ", _
            '                                                      "", _
            '                                                      bv_strSurveyorName, _
            '                                                      " ", _
            '                                                      Nothing, _
            '                                                      " ", _
            '                                                      "", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      basCurr, _
            '                                                      bv_decLaborRate, _
            '                                                      "", _
            '                                                      Nothing, _
            '                                                      "", _
            '                                                      bv_strMeasureCode, _
            '                                                      bv_strUnitCode, _
            '                                                      " ", _
            '                                                      decInvLabourCost, _
            '                                                      decInvMaterialRate, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      decInvTotal, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      decCusLabourCost, _
            '                                                      decCusMaterialRate, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      decCusTotal, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      decEstimateAmount, _
            '                                                      "", _
            '                                                      strTrimEirNumber, _
            '                                                      " ", _
            '                                                      decApprovalAmount, _
            '                                                     bv_strPartyApprovalRef, _
            '                                                     CStr(Format(bv_datApprovalDate, "yyyyMMdd")), _
            '                                                       CStr(Format(dtEstimateDate, "yyyyMMdd")), _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                      " ", _
            '                                                     "1", _
            '                                                      "U", _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      Nothing, _
            '                                                      bv_strModifiedBy, _
            '                                                      1, _
            '                                                      Nothing, _
            '                                                      bv_decCustomerEstimatedCost, _
            '                                                      Nothing, _
            '                                                      bv_decCustomerEstimatedCost, _
            '                                                      bv_decCustomerEstimatedCost, _
            '                                                      "", _
            '                                                      "", _
            '                                                      strRCStatus, _
            '                                                      strEstimationType, _
            '                                                      objtrans)


            If strEquipmentDescription.Length > 30 Then
                strEquipmentDescription = strEquipmentDescription.Substring(0, 30)
            End If
            Dim strEquipType As String
            Dim objEquipType As New EquipmentTypes
            Dim dsEquipType As EquipmentTypeDataSet
            If objCommonUIS.GetMultiLocationSupportConfig().ToLower = "true" Then
                dsEquipType = objEquipType.GetEquipmentGroupByEquipmentTypeId(strEqupTypeId, CInt(objCommonUIS.GetHeadQuarterID()))
            Else
                dsEquipType = objEquipType.GetEquipmentGroupByEquipmentTypeId(strEqupTypeId, bv_i32DepotId)
            End If

            If dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                strEquipType = CStr(dsEquipType.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows(0).Item(EquipmentTypeData.EQPMNT_GRP_CD))
            Else
                strEquipType = String.Empty
            End If
            lngRETCreated = objRepairEstimate.CreateRepairEstimateRet(bv_i64RepairEstimationId, strTransmissionNo, _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       strTrimEirNumber, _
                                                                       "", _
                                                                       intRevisionNo, _
                                                                       bv_datRepairEstimationDate, _
                                                                       bv_strEquipmentNo, _
                                                                       strRef, _
                                                                       strEquipType, _
                                                                       strEqpCodeCode, _
                                                                       strEquipmentDescription, _
                                                                       " ", _
                                                                       CStr(datGateInDate.ToString("yyyyMMdd")), _
                                                                       CStr(datGateInDate.ToString("hh:mm")), _
                                                                       bv_intPrevONHLocationCode, _
                                                                       bv_datPrevONHDat.ToString("yyyyMMdd"), _
                                                                       strTempstatus, _
                                                                       "", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       strlessor, _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                        " ", _
                                                                       CommonUIs.ParseWFDATA(bv_strWfData, "DPT_CD"), _
                                                                       " ", _
                                                                       "", _
                                                                       bv_strSurveyorName, _
                                                                       " ", _
                                                                       Nothing, _
                                                                       " ", _
                                                                       "", _
                                                                       " ", _
                                                                       " ", _
                                                                       basCurr, _
                                                                       bv_decLaborRate, _
                                                                       "", _
                                                                       Nothing, _
                                                                       "", _
                                                                       bv_strMeasureCode, _
                                                                       bv_strUnitCode, _
                                                                       " ", _
                                                                       br_decULaborRate, _
                                                                       br_decUMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       br_decUTotal, _
                                                                       decInvLaborRate, _
                                                                       decInvMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       decInvTotal, _
                                                                       decCusLabourCost, _
                                                                       decCusMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       decCusTotal, _
                                                                       br_decDLaborRate, _
                                                                       br_decDMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       br_decDTotal, _
                                                                       br_decSLaborRate, _
                                                                       br_decSMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       br_decSTotal, _
                                                                       br_decXLaborRate, _
                                                                       br_decXMaterialRate, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       br_decXTotal, _
                                                                       decEstimateAmount, _
                                                                       "", _
                                                                       strTrimEirNumber, _
                                                                       " ", _
                                                                       decApprovalRET, _
                                                                       bv_strPartyApprovalRef, _
                                                                       CStr(CStr(Format(bv_datApprovalDate, "yyyyMMdd"))), _
                                                                       CStr(CStr(Format(dtEstimateDate, "yyyyMMdd"))), _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       " ", _
                                                                       "1", _
                                                                       "U", _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       Nothing, _
                                                                       bv_strModifiedBy, _
                                                                       1, _
                                                                       Nothing, _
                                                                       bv_decCustomerEstimatedCost, _
                                                                       Nothing, _
                                                                       bv_decCustomerEstimatedCost, _
                                                                       bv_decCustomerEstimatedCost, _
                                                                       "", _
                                                                       "", _
                                                                       strRCStatus, _
                                                                       strEstimationType, _
                                                                       objtrans)

            ' End If


            Dim blnEqInfo As Boolean = False
            blnEqInfo = objRepairEstimate.UpdateEquipmentInformation(bv_strEquipmentNo, _
                                                                     bv_datLastTestDate, _
                                                                     bv_datNextTestDate, _
                                                                     bv_i64LastTestTypeId, _
                                                                     bv_i64NextTestTypeId, _
                                                                     bv_strValidityYear, _
                                                                     bv_strSurveyorName, _
                                                                     bv_i32DepotId, _
                                                                     objtrans)

            Dim drSelect As DataRow()





            drSelect = br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairEstimateData.ITM_ID, " is NULL AND ", RepairEstimateData.SB_ITM_ID, " is NULL AND ", RepairEstimateData.RPR_ID, " is NULL AND ", RepairEstimateData.DMG_ID, " is NULL"), "")
            If drSelect.Length = 0 Then
                Dim RetDetailline As Long = 0
                For Each drRepairEstimate As DataRow In br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
                    Dim strMatCd As String = ""

                    Dim lngDetail As Long
                 
                    If drRepairEstimate.RowState = DataRowState.Added Then
                        If Not drRepairEstimate.Item(RepairEstimateData.MTRL_CD) Is Nothing Then
                            strMatCd = drRepairEstimate.Item(RepairEstimateData.MTRL_CD).ToString
                        End If
                        lngDetail = objRepairEstimate.CreateRepairEstimateDetail(bv_i64RepairEstimationId, _
                                                                                 CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.ITM_ID)), _
                                                                                 CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.SB_ITM_ID)), _
                                                                                 CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.DMG_ID)), _
                                                                                 CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RPR_ID)), _
                                                                                 CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS)), _
                                                                                 drRepairEstimate.Item(RepairEstimateData.DMG_RPR_DSCRPTN).ToString, _
                                                                                 CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_RT)), _
                                                                                 CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC)), _
                                                                                 CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC)), _
                                                                                 CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.TTL_CST_NC)), _
                                                                                 CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_ID)), _
                                                                                 CommonUIs.iBool(drRepairEstimate.Item(RepairEstimateData.CHK_BT)), _
                                                                                 strMatCd, _
                                                                                 drRepairEstimate.Item(RepairEstimateData.TX_RSPNSBLTY_ID).ToString, _
                                                                                 drRepairEstimate.Item(RepairEstimateData.RMRKS).ToString, _
                                                                                 False, _
                                                                                 Nothing, _
                                                                                 objtrans)

                    ElseIf drRepairEstimate.RowState = DataRowState.Modified Then

                        Dim checkCount As String
                        checkCount = objRepairEstimate.CheckLineDetailRecordByDetailId(CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID)), objtrans)
                        If Not checkCount = "0" Then
                            objRepairEstimate.UpdateRepairEstimateDetail(CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID)), _
                                                                         bv_i64RepairEstimationId, _
                                                                         CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.ITM_ID)), _
                                                                         CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.SB_ITM_ID)), _
                                                                         CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.DMG_ID)), _
                                                                         CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RPR_ID)), _
                                                                         CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS)), _
                                                                         drRepairEstimate.Item(RepairEstimateData.DMG_RPR_DSCRPTN).ToString, _
                                                                         CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_RT)), _
                                                                         CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC)), _
                                                                         CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC)), _
                                                                         CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.TTL_CST_NC)), _
                                                                         CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_ID)), _
                                                                         CommonUIs.iBool(drRepairEstimate.Item(RepairEstimateData.CHK_BT)), _
                                                                         objtrans)

                        Else

                            If Not drRepairEstimate.Item(RepairEstimateData.MTRL_CD) Is Nothing Then
                                strMatCd = drRepairEstimate.Item(RepairEstimateData.MTRL_CD).ToString
                            End If
                            lngDetail = objRepairEstimate.CreateRepairEstimateDetail(bv_i64RepairEstimationId, _
                                                                                     CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.ITM_ID)), _
                                                                                     CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.SB_ITM_ID)), _
                                                                                     CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.DMG_ID)), _
                                                                                     CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RPR_ID)), _
                                                                                     CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS)), _
                                                                                     drRepairEstimate.Item(RepairEstimateData.DMG_RPR_DSCRPTN).ToString, _
                                                                                     CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_RT)), _
                                                                                     CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC)), _
                                                                                     CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC)), _
                                                                                     CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.TTL_CST_NC)), _
                                                                                     CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_ID)), _
                                                                                     CommonUIs.iBool(drRepairEstimate.Item(RepairEstimateData.CHK_BT)), _
                                                                                     strMatCd, _
                                                                                     drRepairEstimate.Item(RepairEstimateData.TX_RSPNSBLTY_ID).ToString, _
                                                                                     drRepairEstimate.Item(RepairEstimateData.RMRKS).ToString, _
                                                                                     False, _
                                                                                     Nothing, _
                                                                                     objtrans)

                            drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = lngDetail

                        End If


                    ElseIf drRepairEstimate.RowState = DataRowState.Deleted Then
                        objRepairEstimate.DeleteRepairEstimateDetailRet(CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID, DataRowVersion.Original)), objtrans)
                        objRepairEstimate.DeleteRepairEstimateDetail(CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID, DataRowVersion.Original)), objtrans)
                    End If
                    If drRepairEstimate.RowState <> DataRowState.Deleted Then
                        RetDetailline = RetDetailline + 1
                        Dim strWDResponsibility As String
                        If drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD).ToString = "C" Then
                            strWDResponsibility = "O"
                        Else
                            strWDResponsibility = "U"
                        End If

                        If bv_strEstimationNo.Length > 11 Then
                            intEIRlenghth = bv_strEstimationNo.Length - 11
                            intLenghth = bv_strEstimationNo.Length
                            strTrimEirNumber = bv_strEstimationNo.Substring(intEIRlenghth, 11)
                        Else
                            strTrimEirNumber = bv_strEstimationNo
                        End If
                        lngDetail = objRepairEstimate.CreateRepairEstimateDetailRet(CLng(drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID)), _
                                                                                      strTransmissionNo, _
                                                                                      strTrimEirNumber, _
                                                                                      CInt(bv_strRevisionNo), _
                                                                                      bv_datRepairEstimationDate, _
                                                                                      bv_strEquipmentNo, _
                                                                                      drRepairEstimate.Item(RepairEstimateData.LBR_RT).ToString, _
                                                                                      drRepairEstimate.Item(RepairEstimateData.RPR_CD).ToString, _
                                                                                      0, _
                                                                                      drRepairEstimate.Item(RepairEstimateData.DMG_CD).ToString, _
                                                                                      CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS)), _
                                                                                      CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC)), _
                                                                                      drRepairEstimate.Item(RepairEstimateData.DMG_RPR_DSCRPTN).ToString, _
                                                                                      "U", _
                                                                                      bv_i64RepairEstimationId, _
                                                                                      1, _
                                                                                      RetDetailline, _
                                                                                      drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD).ToString, _
                                                                                      drRepairEstimate.Item(RepairEstimateData.SB_ITM_CD).ToString, _
                                                                                      drRepairEstimate.Item(RepairEstimateData.ITM_CD).ToString, _
                                                                                      bv_strUnitCode, _
                                                                                      drRepairEstimate.Item(RepairEstimateData.MTRL_CD).ToString, _
                                                                                      objtrans)
                    End If





                Next
            End If



            If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then
                Dim objCompletion As New RepairCompletions
                'Delete existing Estimate details
                objCompletion.DeleteRepairEstimateDetailByEstimateId(bv_i64RepairEstimationId, objtrans)
                Dim strMatCd As String = Nothing
                Dim blnTrafiffValidation As Boolean = False
                Dim i64TrariffDetailId As Int64 = Nothing
                For Each drRepairEstimate As DataRow In br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows

                    blnTrafiffValidation = False
                    i64TrariffDetailId = Nothing
                    If drRepairEstimate.RowState <> DataRowState.Deleted Then

                        If Not drRepairEstimate.Item(RepairEstimateData.MTRL_CD) Is DBNull.Value Then
                            strMatCd = drRepairEstimate.Item(RepairEstimateData.MTRL_CD).ToString
                        End If

                        Dim objCompletions As New RepairCompletions

                        If Not drRepairEstimate.Item(RepairEstimateData.TRFF_CD_DTL_ID) Is DBNull.Value Then

                            Dim dt As New DataTable
                            dt = objCompletions.ValidateTariff_ByDetailId(CLng(drRepairEstimate.Item(RepairEstimateData.TRFF_CD_DTL_ID)), _
                                                                          CDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS)), _
                                                                          CDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC)), objtrans)

                            i64TrariffDetailId = CLng(drRepairEstimate.Item(RepairEstimateData.TRFF_CD_DTL_ID))

                            If dt.Rows.Count > 0 Then
                                blnTrafiffValidation = True
                            End If
                        Else
                            blnTrafiffValidation = False
                            i64TrariffDetailId = Nothing
                        End If




                        'Create new Estimate Details
                        objRepairEstimate.CreateRepairEstimateDetail(bv_i64RepairEstimationId, _
                                                                                    CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.ITM_ID)), _
                                                                                    CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.SB_ITM_ID)), _
                                                                                    CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.DMG_ID)), _
                                                                                    CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RPR_ID)), _
                                                                                    CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS)), _
                                                                                    drRepairEstimate.Item(RepairEstimateData.DMG_RPR_DSCRPTN).ToString, _
                                                                                    CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_RT)), _
                                                                                    CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC)), _
                                                                                    CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC)), _
                                                                                    CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.TTL_CST_NC)), _
                                                                                    CommonUIs.iLng(drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_ID)), _
                                                                                    CommonUIs.iBool(drRepairEstimate.Item(RepairEstimateData.CHK_BT)), _
                                                                                    strMatCd, _
                                                                                    drRepairEstimate.Item(RepairEstimateData.TX_RSPNSBLTY_ID).ToString, _
                                                                                    drRepairEstimate.Item(RepairEstimateData.RMRKS).ToString, _
                                                                                    blnTrafiffValidation, _
                                                                                    i64TrariffDetailId, _
                                                                                    objtrans)

                    End If
                Next
            End If





            'For RET table Check
            'For Each drRepairEstimate As DataRow In br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
            '    br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).AcceptChanges()
            '    If objRepairEstimate.GetRepairEstimateRETDetail(CLng(drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID)), objtrans) Then
            '        objRepairEstimate.CreateRepairEstimateDetailRet(CLng(drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID)), _
            '                                                                        strTransmissionNo, _
            '                                                                        bv_strEstimationNo, _
            '                                                                        CInt(bv_strRevisionNo), _
            '                                                                        bv_datRepairEstimationDate, _
            '                                                                        bv_strEquipmentNo, _
            '                                                                        drRepairEstimate.Item(RepairEstimateData.LBR_RT).ToString, _
            '                                                                        drRepairEstimate.Item(RepairEstimateData.RPR_CD).ToString, _
            '                                                                        0, _
            '                                                                        drRepairEstimate.Item(RepairEstimateData.DMG_CD).ToString, _
            '                                                                        CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS)), _
            '                                                                        CommonUIs.iDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC)), _
            '                                                                        drRepairEstimate.Item(RepairEstimateData.DMG_RPR_DSCRPTN).ToString, _
            '                                                                        "U", _
            '                                                                        bv_i64RepairEstimationId, _
            '                                                                        1, _
            '                                                                        objtrans)
            '    End If

            'Next

            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
            Dim strEquipmentInfoRemarks As String = String.Empty
            strEquipmentInfoRemarks = objCommonUIS.GetEquipmentInformation(bv_strEquipmentNo, _
                                                                           bv_i32DepotId, _
                                                                           objtrans)
            'Update into Tracking Table
            'Dim dtTtracking As New DataTable
            'dtTtracking = objRepairEstimate.GetTracking(bv_strEquipmentNo, bv_strPageMode, bv_strGateinTransaction, bv_i32DepotId, objtrans)
            'If dtTtracking.Rows.Count > 0 Then
            objRepairEstimate.UpdateTracking(bv_strPageMode, _
                                             bv_strRemarks, _
                                             bv_strGateinTransaction, _
                                             CStr(bv_i64RepairEstimationId), _
                                             dtActivityDate, _
                                             bv_i64InvoicingPartyId, _
                                             bv_strModifiedBy, _
                                             Now, _
                                             bv_i32DepotId, _
                                             strEquipmentInfoRemarks, _
                                             objtrans)
            '  End If

            'REPAIR FLOW SETTING: 037
            'CR-001 (AWE_TO_AWP)
            If str_067InvoiceGenerationGWSBit.ToLower = "true" Then
                If (bv_strEquipmentStatus <> "" Or bv_strEquipmentStatus <> Nothing) And (bv_strPageMode = "Repair Estimate") Then
                    bv_i64EquipmentSttsId = CLng(objRepairEstimate.GetEquipmentStatusDetail("AWP", bv_i32DepotId, objtrans))
                End If
                If (bv_strEquipmentStatus <> "" Or bv_strEquipmentStatus <> Nothing) And (bv_strPageMode = "Repair Approval") Then
                    bv_i64StatusId = CLng(objRepairEstimate.GetEquipmentStatusDetailFromStatus(bv_strStatusCode, bv_i32DepotId, objtrans))
                    'bv_i64StatusId = CLng(objRepairEstimate.GetEquipmentStatusDetailFromStatus(bv_strEquipmentStatus, bv_i32DepotId, objTrans))
                End If

            Else
                If (bv_strEquipmentStatus <> "" Or bv_strEquipmentStatus <> Nothing) And (bv_strPageMode = "Repair Estimate") And objCommonUIS.GetMultiLocationSupportConfig().ToLower = "false" Then
                    bv_i64StatusId = CLng(objRepairEstimate.GetEquipmentStatusDetail(bv_strEquipmentStatus, bv_i32DepotId, objtrans))
                ElseIf (bv_strEquipmentStatus <> "" Or bv_strEquipmentStatus <> Nothing) And (bv_strPageMode = "Repair Estimate") Then
                    bv_i64StatusId = CLng(objRepairEstimate.GetEquipmentStatusDetail(bv_strEquipmentStatus, CInt(objCommonUIS.GetHeadQuarterID()), objtrans))
                End If
            End If

            objRepairEstimate.UpdateActivityStatus(bv_i64EquipmentSttsId, _
                                                   bv_strEquipmentNo, _
                                                   bv_strGateinTransaction, _
                                                   bv_strRemarks, _
                                                   bv_strPageMode, _
                                                   dtActivityDate, _
                                                   objtrans)


            Dim lngCreatedAttachment As Long

            objCommonUIS.DeleteAttachmentByActivityName(bv_strGateinTransaction, _
                                                                              bv_i64RepairEstimationId, _
                                                                              bv_strRepairEstimateNo, _
                                                                              bv_i32DepotId, _
                                                                              objtrans)

            For Each drAttachment As DataRow In br_dsRepairEstimateDataset.Tables(RepairEstimateData._ATTACHMENT).Rows
                If drAttachment.RowState <> DataRowState.Deleted Then
                    lngCreatedAttachment = objCommonUIS.CreateAttachment(bv_i64RepairEstimationId,
                                                                     bv_strPageMode, _
                                                                     bv_strRepairEstimateNo, _
                                                                     bv_strGateinTransaction, _
                                                                     CStr(drAttachment.Item(RepairEstimateData.ATTCHMNT_PTH)), _
                                                                     CStr(drAttachment.Item(RepairEstimateData.ACTL_FL_NM)),
                                                                     bv_strModifiedBy, _
                                                                     bv_datModifiedDate, _
                                                                     bv_i32DepotId,
                                                                     objtrans)
                End If

            Next
            'Else
            'If br_strActivitySubmit.Length > 0 Then
            '    br_strActivitySubmit = String.Concat(br_strActivitySubmit, ",")
            'End If
            'br_strActivitySubmit = String.Concat(br_strActivitySubmit, br_dsRepairEstimateDataset.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Rows(0).Item(RepairEstimateData.EQPMNT_NO))
            'End If
            objtrans.commit()
            Dim dtCleaning As DataTable
            dtCleaning = objRepairEstimate.GetAdditionalCleaningBit(bv_strEquipmentNo, bv_strGateinTransaction, bv_i32DepotId)
            If dtCleaning.Rows.Count > 0 Then
                iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, String.Concat("EQPMNT_NO : ", bv_strEquipmentNo, _
                                                                        "  GI_TRNSCTN_NO : ", bv_strGateinTransaction, _
                                                                       "ADDTNL_CLNNG_BT : ", dtCleaning.Rows(0).Item(ChangeOfStatusData.ADDTNL_CLNNG_BT), _
                                                                        "ADDTNL_CLNNG_FLG : ", dtCleaning.Rows(0).Item(CleaningData.ADDTNL_CLNNG_FLG)))
            End If
            Return lngCreated
        Catch ex As Exception
            objtrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objtrans = Nothing
        End Try
    End Function

#End Region

#Region "pvt_CalculateTotalAmount"
    Private Sub pvt_CalculateTotalAmount(ByVal bv_dsRepairEstimate As RepairEstimateDataSet, _
                                         ByRef br_decTotalEstimateAmount As Decimal, _
                                         ByRef br_decCusLaborRate As Decimal, _
                                         ByRef br_decCusMaterialRate As Decimal, _
                                         ByRef br_decCusTotal As Decimal, _
                                         ByRef br_decInvLaborRate As Decimal, _
                                         ByRef br_decInvMaterialRate As Decimal, _
                                         ByRef br_decInvTotal As Decimal, _
                                         ByRef br_decDLaborRate As Decimal, _
                                         ByRef br_decDMaterialRate As Decimal, _
                                         ByRef br_decDTotal As Decimal, _
                                         ByRef br_decULaborRate As Decimal, _
                                         ByRef br_decUMaterialRate As Decimal, _
                                         ByRef br_decUTotal As Decimal, _
                                         ByRef br_decSLaborRate As Decimal, _
                                         ByRef br_decSMaterialRate As Decimal, _
                                         ByRef br_decSTotal As Decimal, _
                                         ByRef br_decXLaborRate As Decimal, _
                                         ByRef br_decXMaterialRate As Decimal, _
                                         ByRef br_decXTotal As Decimal, _
                                         ByRef br_decRepairApprovalAmount As Decimal)
        Try
            If bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                br_decTotalEstimateAmount = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'O'")).ToString = "" Then
                    br_decCusLaborRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'O'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'O'")).ToString = "" Then
                    br_decCusMaterialRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'O'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'O'")).ToString = "" Then
                    br_decCusTotal = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'O'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'I'")).ToString = "" Then
                    br_decInvLaborRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'I'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'I'")).ToString = "" Then
                    br_decInvMaterialRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'I'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'I'")).ToString = "" Then
                    br_decInvTotal = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'I'")))
                End If
                'Approval Amount Calculation
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", "").ToString = "" Then
                    br_decRepairApprovalAmount = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                End If

                ''Depo - D
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, " NOT IN ('O','I','U','X','S')")).ToString = "" Then
                    br_decDLaborRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, " NOT IN ('O','I','U','X','S')")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, " NOT IN ('O','I','U','X','S')")).ToString = "" Then
                    br_decDMaterialRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, " NOT IN ('O','I','U','X','S')")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, " NOT IN ('O','I','U','X','S')")).ToString = "" Then
                    br_decDTotal = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, " NOT IN ('O','I','U','X','S')")))
                End If

                'User - U
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'U'")).ToString = "" Then
                    br_decULaborRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'U'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'U'")).ToString = "" Then
                    br_decUMaterialRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'U'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'U'")).ToString = "" Then
                    br_decUTotal = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'U'")))
                End If


                'Deletion  - X
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'X'")).ToString = "" Then
                    br_decXLaborRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'X'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'X'")).ToString = "" Then
                    br_decXMaterialRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'X'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'X'")).ToString = "" Then
                    br_decXTotal = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'X'")))
                End If

                'Surveyor S
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'S'")).ToString = "" Then
                    br_decSLaborRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(LBR_HR_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'S'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'S'")).ToString = "" Then
                    br_decSMaterialRate = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(MTRL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'S'")))
                End If
                If Not bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'S'")).ToString = "" Then
                    br_decSTotal = CDec(bv_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'S'")))
                End If





            End If

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Sub
#End Region

#Region "pvt_CalculateRepairChargesCompletion"
    Private Sub pvt_CalculateRepairChargesCompletion(ByVal bv_dsRepairEstimate As RepairEstimateDataSet, _
                                           ByRef br_dblTotalEstimateAmount As Decimal, _
                                           ByRef br_dblTotalCleaningcost As Decimal, _
                                           ByRef br_dblTotallabourCost As Decimal, _
                                           ByRef br_dblTotalMaterialCost As Decimal, _
                                           ByRef br_dblTotalServicetax As Decimal, _
                                           ByRef br_dblServicetax As Decimal, _
                                           ByRef br_dblTotalTotalEstimateWithtax As Decimal, _
                                            ByRef br_dblTaxRate As Decimal, _
                                              ByRef br_dblTaxRateAmount As Decimal, _
                                            ByVal bv_blnReCalulateSummary As Boolean)
        Try
            If bv_blnReCalulateSummary Then
                For i = 0 To 4
                    Dim drRepairEst As DataRow = bv_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).NewRow()
                    drRepairEst.Item(RepairEstimateData.SMMRY_ID) = i + 1
                    drRepairEst.Item(RepairEstimateData.MN_HR_SMMRY) = 0.0
                    drRepairEst.Item(RepairEstimateData.MN_HR_RT_SMMRY) = 0.0
                    drRepairEst.Item(RepairEstimateData.MH_CST_SMMRY) = 0.0
                    drRepairEst.Item(RepairEstimateData.MTRL_CST_SMMRY) = 0.0
                    drRepairEst.Item(RepairEstimateData.TTL_CST_SMMRY) = 0.0
                    bv_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows.Add(drRepairEst)
                Next

            End If

            Dim dtRepairEstimate As DataTable
            dtRepairEstimate = bv_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL)
            br_dblTotalEstimateAmount = CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(RepairEstimateData.MN_HR_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(RepairEstimateData.MH_CST_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(RepairEstimateData.MTRL_CST_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(RepairEstimateData.TTL_CST_SMMRY))
            br_dblTotalCleaningcost = CommonUIs.iDec(bv_dsRepairEstimate.Tables(RepairEstimateData._REPAIR_ESTIMATE_DETAIL).Compute("SUM([" & RepairEstimateData.LBR_HR_CST_NC & "])", RepairEstimateData.RPR_CD & "='CC'")) + _
                CommonUIs.iDec(bv_dsRepairEstimate.Tables(RepairEstimateData._REPAIR_ESTIMATE_DETAIL).Compute("SUM([" & RepairEstimateData.MTRL_CST_NC & "])", RepairEstimateData.RPR_CD & "='CC'"))
            br_dblTotallabourCost = CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(RepairEstimateData.MN_HR_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(RepairEstimateData.MH_CST_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(RepairEstimateData.MTRL_CST_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(RepairEstimateData.TTL_CST_SMMRY))


            br_dblTotalMaterialCost = CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(RepairEstimateData.MN_HR_SMMRY)) + _
                                CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + _
                                CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(RepairEstimateData.MH_CST_SMMRY)) + _
                                CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(RepairEstimateData.MTRL_CST_SMMRY)) + _
                                CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(RepairEstimateData.TTL_CST_SMMRY))

            br_dblTaxRateAmount = CommonUIs.iDec(dtRepairEstimate.Rows(3).Item(RepairEstimateData.MN_HR_SMMRY)) + _
                            CommonUIs.iDec(dtRepairEstimate.Rows(3).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + _
                            CommonUIs.iDec(dtRepairEstimate.Rows(3).Item(RepairEstimateData.MH_CST_SMMRY)) + _
                            CommonUIs.iDec(dtRepairEstimate.Rows(3).Item(RepairEstimateData.MTRL_CST_SMMRY)) + _
                            CommonUIs.iDec(dtRepairEstimate.Rows(3).Item(RepairEstimateData.TTL_CST_SMMRY))


            If br_dblTotalEstimateAmount > 0 Then
                br_dblTotalServicetax = br_dblTotalEstimateAmount * br_dblServicetax / 100
                br_dblTotalTotalEstimateWithtax = br_dblTotalEstimateAmount + br_dblTotalServicetax
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Sub

#End Region

#Region "pvt_DeleteRepairEstimateDetail() TABLE NAME:Repair_Estimate_Detail"
    Private Function pvt_DeleteRepairEstimateDetail(ByVal bv_i64RepairEstimationDetailId As Int64, ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim objRepairEstimateDetail As New RepairEstimates
            Dim blnDeleted As Boolean
            blnDeleted = objRepairEstimateDetail.DeleteRepairEstimateDetail(bv_i64RepairEstimationDetailId, br_objTrans)
            Return blnDeleted
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_CreateRepairEstimateRet() TABLE NAME:Repair_Estimate_Ret"
    Private Function pvt_CreateRepairEstimateRet(ByVal bv_strTRANSMISSION_NO As String, _
                                                 ByVal bv_strCOMPLETE As String, _
                                                 ByVal bv_strSENT_EIR As String, _
                                                 ByVal bv_strSENT_DATE As String, _
                                                 ByVal bv_strREC_EIR As String, _
                                                 ByVal bv_strREC_DATE As String, _
                                                 ByVal bv_strREC_ADDR As String, _
                                                 ByVal bv_strREC_TYPE As String, _
                                                 ByVal bv_strEXPORTED As String, _
                                                 ByVal bv_strEXPOR_DATE As String, _
                                                 ByVal bv_strIMPORTED As String, _
                                                 ByVal bv_strIMPOR_DATE As String, _
                                                 ByVal bv_strTRNSXN As String, _
                                                 ByVal bv_strPTY_RSPONS As String, _
                                                 ByVal bv_i32REVISION As Int32, _
                                                 ByVal bv_datESTIM_DATE As DateTime, _
                                                 ByVal bv_strUNIT_NBR As String, _
                                                 ByVal bv_strREFERENCE As String, _
                                                 ByVal bv_strEQUIP_TYPE As String, _
                                                 ByVal bv_strEQUIP_CODE As String, _
                                                 ByVal bv_strEQUIP_DESC As String, _
                                                 ByVal bv_strTERM_LOCA As String, _
                                                 ByVal bv_strTERM_DATE As String, _
                                                 ByVal bv_strTERM_TIME As String, _
                                                 ByVal bv_strLAST_OH_LOC As String, _
                                                 ByVal bv_strLAST_OH_DATE As String, _
                                                 ByVal bv_strCONDITION As String, _
                                                 ByVal bv_strMANU_DATE As String, _
                                                 ByVal bv_strCSC_REEXAM As String, _
                                                 ByVal bv_strLOAD As String, _
                                                 ByVal bv_strSENDER As String, _
                                                 ByVal bv_strATTENTION As String, _
                                                 ByVal bv_strLSR_OWNER As String, _
                                                 ByVal bv_strSEND_EDI_1 As String, _
                                                 ByVal bv_strSSL_LSE As String, _
                                                 ByVal bv_strSEND_EDI_2 As String, _
                                                 ByVal bv_strHAULIER As String, _
                                                 ByVal bv_strSEND_EDI_3 As String, _
                                                 ByVal bv_strDPT_TRM As String, _
                                                 ByVal bv_strSEND_EDI_4 As String, _
                                                 ByVal bv_strINSURER As String, _
                                                 ByVal bv_strSURVEYOR As String, _
                                                 ByVal bv_strOTHERS_1 As String, _
                                                 ByVal bv_dblTAX_RATE As Double, _
                                                 ByVal bv_strFILLER As String, _
                                                 ByVal bv_strNOTE_1 As String, _
                                                 ByVal bv_strNOTE_2 As String, _
                                                 ByVal bv_strNOTE_3 As String, _
                                                 ByVal bv_strBAS_CURR As String, _
                                                 ByVal bv_dblLABOR_RATE As Double, _
                                                 ByVal bv_strDPP_CURR As String, _
                                                 ByVal bv_dblDPP_AMT As Double, _
                                                 ByVal bv_strGrossWEIGHT As String, _
                                                 ByVal bv_strMEASURE As String, _
                                                 ByVal bv_strUNITS As String, _
                                                 ByVal bv_strMATERIAL As String, _
                                                 ByVal bv_dblU_LABOR As Double, _
                                                 ByVal bv_dblU_MATERIAL As Double, _
                                                 ByVal bv_dblU_HANDLING As Double, _
                                                 ByVal bv_dblU_TAX As Double, _
                                                 ByVal bv_dblU_TOTAL As Double, _
                                                 ByVal bv_dblI_LABOR As Double, _
                                                 ByVal bv_dblI_MATERIAL As Double, _
                                                 ByVal bv_dblI_HANDLING As Double, _
                                                 ByVal bv_dblI_TAX As Double, _
                                                 ByVal bv_dblI_TOTAL As Double, _
                                                 ByVal bv_dblO_LABOR As Double, _
                                                 ByVal bv_dblO_MATERIAL As Double, _
                                                 ByVal bv_dblO_HANDLING As Double, _
                                                 ByVal bv_dblO_TAX As Double, _
                                                 ByVal bv_dblO_TOTAL As Double, _
                                                 ByVal bv_dblD_LABOR As Double, _
                                                 ByVal bv_dblD_MATERIAL As Double, _
                                                 ByVal bv_dblD_HANDLING As Double, _
                                                 ByVal bv_dblD_TAX As Double, _
                                                 ByVal bv_dblD_TOTAL As Double, _
                                                 ByVal bv_dblS_LABOR As Double, _
                                                 ByVal bv_dblS_MATERIAL As Double, _
                                                 ByVal bv_dblS_HANDLING As Double, _
                                                 ByVal bv_dblS_TAX As Double, _
                                                 ByVal bv_dblS_TOTAL As Double, _
                                                 ByVal bv_dblX_LABOR As Double, _
                                                 ByVal bv_dblX_MATERIAL As Double, _
                                                 ByVal bv_dblX_HANDLING As Double, _
                                                 ByVal bv_dblX_TAX As Double, _
                                                 ByVal bv_dblX_TOTAL As Double, _
                                                 ByVal bv_dblEST_TOTAL As Double, _
                                                 ByVal bv_strADVICE As String, _
                                                 ByVal bv_strEIR_NUM As String, _
                                                 ByVal bv_strAUTH_NUM As String, _
                                                 ByVal bv_dblAUTH_AMT As Double, _
                                                 ByVal bv_strAUTH_PTY As String, _
                                                 ByVal bv_strAUTH_DATE As String, _
                                                 ByVal bv_strO_ESTIM_DATE As String, _
                                                 ByVal bv_strOTHERS_2 As String, _
                                                 ByVal bv_strSEND_EDI_5 As String, _
                                                 ByVal bv_strSEND_EDI_6 As String, _
                                                 ByVal bv_strSEND_EDI_7 As String, _
                                                 ByVal bv_strSEND_EDI_8 As String, _
                                                 ByVal bv_strNOTE_4 As String, _
                                                 ByVal bv_strNOTE_5 As String, _
                                                 ByVal bv_strWEIGHT_2 As String, _
                                                 ByVal bv_strMEASURE_2 As String, _
                                                 ByVal bv_strINVOICE_TYPE As String, _
                                                 ByVal bv_strODOMETER_HOURS As String, _
                                                 ByVal bv_strOUT_SVC_DATE As String, _
                                                 ByVal bv_strRET_SVC_DATE As String, _
                                                 ByVal bv_strOWN_INSP_DATE As String, _
                                                 ByVal bv_strMECHANIC_NAME As String, _
                                                 ByVal bv_strBILLEE_CODE As String, _
                                                 ByVal bv_strSUB_REPAIR_TYPE As String, _
                                                 ByVal bv_strOUT_SVC_TIME As String, _
                                                 ByVal bv_strRET_SVC_TIME As String, _
                                                 ByVal bv_strEXCHG_RATE As String, _
                                                 ByVal bv_strSTATUS As String, _
                                                 ByVal bv_datPICK_DATE As DateTime, _
                                                 ByVal bv_strESTSTATUS As String, _
                                                 ByVal bv_strErrors As String, _
                                                 ByVal bv_strMatchStatus As String, _
                                                 ByVal bv_strERRSTATUS As String, _
                                                 ByVal bv_strUSERNAME As String, _
                                                 ByVal bv_i32LIVE_STATUS As Int32, _
                                                 ByVal bv_dblEST_TOTAL_TAXED As Double, _
                                                 ByVal bv_dblCC_TOTAL As Double, _
                                                 ByVal bv_dblSRVC_TAX_RATE As Double, _
                                                 ByVal bv_dblTOTAL_LABOR_COST As Double, _
                                                 ByVal bv_dblTOTAL_SRVC_TAX As Double, _
                                                 ByVal bv_strEQUIP_SIZE As String, _
                                                 ByVal bv_strYARD_LOC As String, _
                                                 ByVal bv_strRCESTSTATUS As String, _
                                                 ByVal bv_strESTIMATE_TYPE As String, _
                                                 ByRef br_objTrans As Transactions) As Long
        Try
            Dim objRepair_Estimate_Ret As New RepairEstimates
            Dim lngCreated As Long
            lngCreated = objRepair_Estimate_Ret.CreateRepairEstimateRet(lngCreated, bv_strTRANSMISSION_NO, _
                                                                        bv_strCOMPLETE, _
                                                                        bv_strSENT_EIR, _
                                                                        bv_strSENT_DATE, _
                                                                        bv_strREC_EIR, _
                                                                        bv_strREC_DATE, _
                                                                        bv_strREC_ADDR, _
                                                                        bv_strREC_TYPE, _
                                                                        bv_strEXPORTED, _
                                                                        bv_strEXPOR_DATE, _
                                                                        bv_strIMPORTED, _
                                                                        bv_strIMPOR_DATE, _
                                                                        bv_strTRNSXN, _
                                                                        bv_strPTY_RSPONS, _
                                                                        bv_i32REVISION, _
                                                                        bv_datESTIM_DATE, _
                                                                        bv_strUNIT_NBR, _
                                                                        bv_strREFERENCE, _
                                                                        bv_strEQUIP_TYPE, _
                                                                        bv_strEQUIP_CODE, _
                                                                        bv_strEQUIP_DESC, _
                                                                        bv_strTERM_LOCA, _
                                                                        bv_strTERM_DATE, _
                                                                        bv_strTERM_TIME, _
                                                                        bv_strLAST_OH_LOC, _
                                                                        bv_strLAST_OH_DATE, _
                                                                        bv_strCONDITION, _
                                                                        bv_strMANU_DATE, _
                                                                        bv_strCSC_REEXAM, _
                                                                        bv_strLOAD, _
                                                                        bv_strSENDER, _
                                                                        bv_strATTENTION, _
                                                                        bv_strLSR_OWNER, _
                                                                        bv_strSEND_EDI_1, _
                                                                        bv_strSSL_LSE, _
                                                                        bv_strSEND_EDI_2, _
                                                                        bv_strHAULIER, _
                                                                        bv_strSEND_EDI_3, _
                                                                        bv_strDPT_TRM, _
                                                                        bv_strSEND_EDI_4, _
                                                                        bv_strINSURER, _
                                                                        bv_strSURVEYOR, _
                                                                        bv_strOTHERS_1, _
                                                                        bv_dblTAX_RATE, _
                                                                        bv_strFILLER, _
                                                                        bv_strNOTE_1, _
                                                                        bv_strNOTE_2, _
                                                                        bv_strNOTE_3, _
                                                                        bv_strBAS_CURR, _
                                                                        bv_dblLABOR_RATE, _
                                                                        bv_strDPP_CURR, _
                                                                        bv_dblDPP_AMT, _
                                                                        bv_strGrossWEIGHT, _
                                                                        bv_strMEASURE, _
                                                                        bv_strUNITS, _
                                                                        bv_strMATERIAL, _
                                                                        bv_dblU_LABOR, _
                                                                        bv_dblU_MATERIAL, _
                                                                        bv_dblU_HANDLING, _
                                                                        bv_dblU_TAX, _
                                                                        bv_dblU_TOTAL, _
                                                                        bv_dblI_LABOR, _
                                                                        bv_dblI_MATERIAL, _
                                                                        bv_dblI_HANDLING, _
                                                                        bv_dblI_TAX, _
                                                                        bv_dblI_TOTAL, _
                                                                        bv_dblO_LABOR, _
                                                                        bv_dblO_MATERIAL, _
                                                                        bv_dblO_HANDLING, _
                                                                        bv_dblO_TAX, _
                                                                        bv_dblO_TOTAL, _
                                                                        bv_dblD_LABOR, _
                                                                        bv_dblD_MATERIAL, _
                                                                        bv_dblD_HANDLING, _
                                                                        bv_dblD_TAX, _
                                                                        bv_dblD_TOTAL, _
                                                                        bv_dblS_LABOR, _
                                                                        bv_dblS_MATERIAL, _
                                                                        bv_dblS_HANDLING, _
                                                                        bv_dblS_TAX, _
                                                                        bv_dblS_TOTAL, _
                                                                        bv_dblX_LABOR, _
                                                                        bv_dblX_MATERIAL, _
                                                                        bv_dblX_HANDLING, _
                                                                        bv_dblX_TAX, _
                                                                        bv_dblX_TOTAL, _
                                                                        bv_dblEST_TOTAL, _
                                                                        bv_strADVICE, _
                                                                        bv_strEIR_NUM, _
                                                                        bv_strAUTH_NUM, _
                                                                        bv_dblAUTH_AMT, _
                                                                        bv_strAUTH_PTY, _
                                                                        bv_strAUTH_DATE, _
                                                                        bv_strO_ESTIM_DATE, _
                                                                        bv_strOTHERS_2, _
                                                                        bv_strSEND_EDI_5, _
                                                                        bv_strSEND_EDI_6, _
                                                                        bv_strSEND_EDI_7, _
                                                                        bv_strSEND_EDI_8, _
                                                                        bv_strNOTE_4, _
                                                                        bv_strNOTE_5, _
                                                                        bv_strWEIGHT_2, _
                                                                        bv_strMEASURE_2, _
                                                                        bv_strINVOICE_TYPE, _
                                                                        bv_strODOMETER_HOURS, _
                                                                        bv_strOUT_SVC_DATE, _
                                                                        bv_strRET_SVC_DATE, _
                                                                        bv_strOWN_INSP_DATE, _
                                                                        bv_strMECHANIC_NAME, _
                                                                        bv_strBILLEE_CODE, _
                                                                        bv_strSUB_REPAIR_TYPE, _
                                                                        bv_strOUT_SVC_TIME, _
                                                                        bv_strRET_SVC_TIME, _
                                                                        bv_strEXCHG_RATE, _
                                                                        bv_strSTATUS, _
                                                                        bv_datPICK_DATE, _
                                                                        bv_strESTSTATUS, _
                                                                        bv_strErrors, _
                                                                        bv_strMatchStatus, _
                                                                        bv_strERRSTATUS, _
                                                                        bv_strUSERNAME, _
                                                                        bv_i32LIVE_STATUS, _
                                                                        bv_dblEST_TOTAL_TAXED, _
                                                                        bv_dblCC_TOTAL, _
                                                                        bv_dblSRVC_TAX_RATE, _
                                                                        bv_dblTOTAL_LABOR_COST, _
                                                                        bv_dblTOTAL_SRVC_TAX, _
                                                                        bv_strEQUIP_SIZE, _
                                                                        bv_strYARD_LOC, _
                                                                        bv_strRCESTSTATUS, _
                                                                        bv_strESTIMATE_TYPE, _
                                                                        br_objTrans)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_CreateRepairEstimateDetailRet() TABLE NAME:Repair_Estimate_Detail_Ret"
    Private Function pvt_CreateRepairEstimateDetailRet(ByVal bv_i64RepairEstimateDetailId As Int64, _
                                                       ByVal bv_strTRANSMISSION_NO As String, _
                                                       ByVal bv_strTRNSXN As String, _
                                                       ByVal bv_i32REVISION As Int32, _
                                                       ByVal bv_datESTIM_DATE As DateTime, _
                                                       ByVal bv_strUNIT_NBR As String, _
                                                       ByVal bv_strLABOR_RATE As String, _
                                                       ByVal bv_strLINE As String, _
                                                       ByVal bv_strREPAIR As String, _
                                                       ByVal bv_strDAMAGE As String, _
                                                       ByVal bv_dblHOURS As Double, _
                                                       ByVal bv_dblMAT_COST As Double, _
                                                       ByVal bv_strDMGREP_DESC As String, _
                                                       ByVal bv_strSTATUS As String, _
                                                       ByVal bv_i64SNo As Int64, _
                                                       ByVal bv_i32LIVE_STATUS As Int32, _
                                                       ByVal bv_strWfData As String,
                                                       ByRef br_objTrans As Transactions) As Long
        Try
            Dim objRepair_Estimate_Detail_Ret As New RepairEstimates
            Dim lngCreated As Long
            Dim lngWD_LINE As Long = 0
            lngCreated = objRepair_Estimate_Detail_Ret.CreateRepairEstimateDetailRet(bv_i64RepairEstimateDetailId, bv_strTRANSMISSION_NO, bv_strTRNSXN, _
                                                                                     bv_i32REVISION, bv_datESTIM_DATE, _
                                                                                     bv_strUNIT_NBR,
                                                                                     bv_strLABOR_RATE, _
                                                                                     bv_strREPAIR, 0, bv_strDAMAGE, _
                                                                                     bv_dblHOURS,
                                                                                     bv_dblMAT_COST,
                                                                                     bv_strDMGREP_DESC, _
                                                                                     bv_strSTATUS, _
                                                                                     bv_i64SNo, bv_i32LIVE_STATUS, _
                                                                                     lngWD_LINE, _
                                                                                     "", "", "", _
                                                                                     Nothing, _
                                                                                     Nothing, _
                                                                                     br_objTrans)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "Get Owner Approval Amount TABLE NAME:INVENTORY"

    Public Function GetOwnerApprovalByEQPMNT_NO(ByVal bv_strEqpmntNo As String) As String
        Try
            Dim OwnerApprovalAmt As String
            Dim objRepairEstimates As New RepairEstimates
            OwnerApprovalAmt = objRepairEstimates.GetOwnerApprovalByEQPMNT_NO(bv_strEqpmntNo)
            Return OwnerApprovalAmt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GET : pub_GetEquipmentInformationByEqpmntNo() "

    <OperationContract()> _
    Public Function pub_GetEquipmentInformationByEqpmntNo(ByVal bv_i32DepotID As Int32, ByVal bv_strEqpmntNo As String) As RepairEstimateDataSet

        Try
            Dim dsRepairEstimateDataSet As RepairEstimateDataSet
            Dim objRepairEstimates As New RepairEstimates
            dsRepairEstimateDataSet = objRepairEstimates.GetEquipmentInformationByEqpmntNo(bv_i32DepotID, bv_strEqpmntNo)
            Return dsRepairEstimateDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET : pub_GetLaborRateperHourByCustomerID() "

    <OperationContract()> _
    Public Function pub_GetLaborRateperHourByCustomerID(ByVal bv_i32DepotID As Int32, ByVal bv_i64CustomerID As Int64) As String

        Try
            Dim objRepairEstimates As New RepairEstimates
            Dim strLaborRate As String
            strLaborRate = objRepairEstimates.LaborRateperHourByCustomerID(bv_i32DepotID, bv_i64CustomerID)
            Return strLaborRate
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "TransposeDataTable()"

    <OperationContract()> _
    Public Function TransposeDataTable(ByRef br_br_dtInputTable As DataTable, ByVal blnAcaciaSpecific As Boolean) As DataTable
        Try
            Dim dtOutputTable As New DataTable
            Dim dc As DataColumn
            Dim intAcaciaRowCnt As Integer = 10
            Dim totalItemCount As Integer = 30
            dc = New DataColumn
            dc.DataType = System.Type.GetType("System.String")
            dc.ColumnName = "COL1"
            dc.Caption = "COL1"
            dtOutputTable.Columns.Add(dc)

            dc = New DataColumn
            dc.DataType = System.Type.GetType("System.String")
            dc.ColumnName = "COL2"
            dc.Caption = "COL2"
            dtOutputTable.Columns.Add(dc)

            dc = New DataColumn
            dc.DataType = System.Type.GetType("System.String")
            dc.ColumnName = "COL3"
            dc.Caption = "COL3"
            dtOutputTable.Columns.Add(dc)
            Dim intRowCount As Integer = 0
            Dim intColumnCount As Integer = 0
            Dim intTotalRowCount As Integer = 0
            If blnAcaciaSpecific Then
                intAcaciaRowCnt = 10
                totalItemCount = 30
            Else
                intAcaciaRowCnt = 8
                totalItemCount = 24
            End If
            For Each drDamage As DataRow In br_br_dtInputTable.Rows
                If intTotalRowCount >= totalItemCount Then
                    Exit For
                Else
                    If intRowCount < intAcaciaRowCnt Then
                        Dim drow As DataRow = dtOutputTable.NewRow()
                        ' If row count > 7 stop adding additional rows
                        If intTotalRowCount < intAcaciaRowCnt Then
                            dtOutputTable.Rows.Add(drow)
                        End If
                        dtOutputTable.Rows(intRowCount)(intColumnCount) = String.Concat("(", drDamage.Item(4).ToString, ") ", drDamage.Item(5).ToString)
                        intRowCount = intRowCount + 1
                    Else
                        intRowCount = 0
                        intColumnCount = intColumnCount + 1
                        dtOutputTable.Rows(intRowCount)(intColumnCount) = String.Concat("(", drDamage.Item(4).ToString, ") ", drDamage.Item(5).ToString)
                        intRowCount = intRowCount + 1
                    End If
                End If
                intTotalRowCount = intTotalRowCount + 1
            Next
            Return dtOutputTable
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "Work order- RDLC Report"
    Public Function RepairWorkOrder(ByRef bv_param As String, ByRef br1_dsRepairEstimate As RepairEstimateDataSet, ByVal bv_blnAcaciaSpecifc As Boolean) As DataSet
        Try
            Dim objRepairEstimates As New RepairEstimates
            Dim dsEstimate As New DataSet
            Dim dt As DataTable
            Dim dc As DataColumn
            Dim dr As DataRow
            Dim dtItem As New DataTable
            Dim dtSubItem As New DataTable
            Dim dtWestim As New DataTable
            Dim dtRepairEstimateDetail As New DataTable
            Dim drRepairEstimateDetail() As DataRow
            Dim blnComma As Boolean = False
            Dim intDepotId As Int32
            'Added by Francis on 9-Apr-2013 
            Dim dtDamageCode As DataTable
            Dim dtRepairCode As DataTable
            Dim j As Integer
            Dim strItemCode As String
            Dim strItemDesc As String
            Dim strSubItemDesc As String
            Dim strSubItemCode As String
            Dim strDamage As String = ""
            Dim strRepair As String = ""
            Dim strRemarks As String = ""
            Dim strRspnsblty_cd As String = ""
            Dim decManHours As Decimal = 0
            Dim decMaterialCost As Decimal = 0
            Dim strRepairEstimateId As String
            Dim objCommon As New CommonUIs
            Dim strUserName As String
            Dim dtCustomer As New DataTable
            Dim dtRepairEstimate As New DataTable
            Dim i64CustomerId As Int64
            Dim strCreateEstimateUser As String = ""
            Dim dtActivityStatus As New DataTable
            Dim dtEquipmentInfo As New DataTable
            Dim br_dsRepairEstimate As New RepairEstimateDataSet
            Dim strScreenType As String = String.Empty
            Dim dtDepot As New DataTable
            Dim dtCustomerDetail As New DataTable
            Dim dtAgent As New DataTable
            Dim objDatasetHelpers As New DatasetHelpers
            Dim strGWS As String
            Dim dblTax As Decimal
            Dim strMultiLocation As String = objCommon.pub_GetParameter("MultiLocation", bv_param)
            Dim strHQID As String = objCommon.pub_GetParameter("HeadQuarterID", bv_param)


            br_dsRepairEstimate.Clear()
            strRepairEstimateId = objCommon.pub_GetParameter("EstimateID", bv_param)
            strGWS = objCommon.pub_GetParameter("GWS", bv_param)
            ' strScreenType = objCommon.pub_GetParameter("ScreenType", bv_param)
            If Not strRepairEstimateId = "" Then
                strUserName = objCommon.pub_GetParameter("USERNAME", bv_param)
                intDepotId = CInt(objCommon.pub_GetParameter("DPT_ID", bv_param))
                dtRepairEstimate = objRepairEstimates.GetRepairEstimateById(CLng(strRepairEstimateId))
                dtDepot = objRepairEstimates.GetDepotDetailsbyDepotId(intDepotId)
                If dtRepairEstimate.Rows(0).Item(RepairEstimateData.TX_RT_PRCNT) Is DBNull.Value Then
                    dblTax = 0
                Else
                    dblTax = CDec(dtRepairEstimate.Rows(0).Item(RepairEstimateData.TX_RT_PRCNT))
                End If


                If dtRepairEstimate.Rows.Count > 0 Then
                    dtActivityStatus = objRepairEstimates.GetActivityStatusByEqpmntNo(intDepotId, _
                                                                                      dtRepairEstimate.Rows(0).Item(RepairEstimateData.EQPMNT_NO).ToString, _
                                                                                      dtRepairEstimate.Rows(0).Item(RepairEstimateData.GI_TRNSCTN_NO).ToString).Tables(RepairEstimateData._V_ACTIVITY_STATUS)
                End If
                br_dsRepairEstimate.Merge(objRepairEstimates.GetRepairEstimateDetailByRepairEstimationId(CLng(strRepairEstimateId)))
                br_dsRepairEstimate.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Merge(dtActivityStatus)
                br_dsRepairEstimate.Tables(RepairEstimateData._DEPOT).Merge(dtDepot)
                dtRepairEstimate.Rows(0).Item(RepairEstimateData.MDFD_BY) = strUserName
                If dtRepairEstimate.Rows.Count > 0 Then
                    Dim strEncryptRepairEstimateNo As String = String.Empty
                    Dim strEncryptRepairEstimateId As String = String.Empty
                    Dim strEncryptKey As String = String.Empty
                    strEncryptRepairEstimateNo = EncryptData(dtRepairEstimate.Rows(0).Item(RepairEstimateData.RPR_ESTMT_NO).ToString)
                    strEncryptRepairEstimateId = EncryptData(dtRepairEstimate.Rows(0).Item(RepairEstimateData.RPR_ESTMT_ID).ToString)
                    strEncryptRepairEstimateNo = strEncryptRepairEstimateNo.Replace("+", "IIC")
                    strEncryptRepairEstimateId = strEncryptRepairEstimateId.Replace("+", "IIC")
                    strCreateEstimateUser = objRepairEstimates.GetEstimateUserByEqpmntNo(dtRepairEstimate.Rows(0).Item(RepairEstimateData.EQPMNT_NO).ToString, _
                                                                                         dtRepairEstimate.Rows(0).Item(RepairEstimateData.GI_TRNSCTN_NO).ToString, _
                                                                                         dtRepairEstimate.Rows(0).Item(RepairEstimateData.RPR_ESTMT_NO).ToString, _
                                                                                         "Repair Estimate", _
                                                                                         CInt(dtRepairEstimate.Rows(0).Item(RepairEstimateData.DPT_ID)))
                    dtRepairEstimate.Rows(0).Item(RepairEstimateData.ESTMT_CRTD_BY) = strCreateEstimateUser
                    dtRepairEstimate.Rows(0).Item(RepairEstimateData.ENCRYPT_RPR_ESTMT_NO) = strEncryptRepairEstimateNo
                    dtRepairEstimate.Rows(0).Item(RepairEstimateData.ENCRYPT_REPAIR_ESTIMATE_ID) = strEncryptRepairEstimateId
                    i64CustomerId = CLng(dtRepairEstimate.Rows(0).Item(RepairEstimateData.CSTMR_ID))
                End If
                dtCustomerDetail = objRepairEstimates.GetCustomerDetailsByCustomerId(i64CustomerId)
                br_dsRepairEstimate.Tables(RepairEstimateData._V_CUSTOMER).Merge(dtCustomerDetail)
                dtAgent = objRepairEstimates.GetAgentByCustomerId(i64CustomerId, intDepotId)
                br_dsRepairEstimate.Tables(RepairEstimateData._V_AGENT).Merge(dtAgent)
                dtEquipmentInfo = objRepairEstimates.GetEquipmentInformationByEqpmntNo(intDepotId, dtRepairEstimate.Rows(0).Item(RepairEstimateData.EQPMNT_NO).ToString).Tables(RepairEstimateData._V_EQUIPMENT_INFORMATION)
                br_dsRepairEstimate.Tables(RepairEstimateData._V_EQUIPMENT_INFORMATION).Merge(dtEquipmentInfo)
                If Not IsDBNull(dtEquipmentInfo.Rows(0).Item(RepairEstimateData.LST_TST_DT)) Then
                    dtRepairEstimate.Rows(0).Item(RepairEstimateData.LST_TST_DT) = dtEquipmentInfo.Rows(0).Item(RepairEstimateData.LST_TST_DT)
                Else
                    dtRepairEstimate.Rows(0).Item(RepairEstimateData.LST_TST_DT) = DBNull.Value
                End If
                If Not IsDBNull(dtEquipmentInfo.Rows(0).Item(RepairEstimateData.NXT_TST_DT)) Then
                    dtRepairEstimate.Rows(0).Item(RepairEstimateData.NXT_TST_DT) = dtEquipmentInfo.Rows(0).Item(RepairEstimateData.NXT_TST_DT)
                Else
                    dtRepairEstimate.Rows(0).Item(RepairEstimateData.NXT_TST_DT) = DBNull.Value
                End If
                br_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE).Merge(dtRepairEstimate)
                If strMultiLocation.ToLower = "true" Then
                    intDepotId = CInt(strHQID)
                End If
                dtCustomer = objRepairEstimates.GetRepairEstimateCustomerDetailsByCustomerId(intDepotId, i64CustomerId).Tables(RepairEstimateData._V_REPAIR_ESTIMATE_REPORT)
                br_dsRepairEstimate.Merge(dtCustomer)
                dtSubItem = objRepairEstimates.GetSubItem(intDepotId)

                dtDamageCode = objRepairEstimates.DamageCodeByActive(intDepotId)
                Dim dtTempDamage As New DataTable
                dtTempDamage = TransposeDataTable(dtDamageCode, bv_blnAcaciaSpecifc)
                dtTempDamage.TableName = RepairEstimateData._REPAIRWORDER_DAMAGE
                br_dsRepairEstimate.Merge(dtTempDamage)

                dtRepairCode = New DataTable
                dtRepairCode = objRepairEstimates.RepairCodeByActive(intDepotId)
                Dim dtTempRepairCode As New DataTable
                dtTempRepairCode = TransposeDataTable(dtRepairCode, bv_blnAcaciaSpecifc)
                dtTempRepairCode.TableName = RepairEstimateData._REPAIRWORDER_REPAIR
                br_dsRepairEstimate.Merge(dtTempRepairCode)

                dt = New DataTable
                dc = New DataColumn
                dc.DataType = System.Type.GetType("System.String")
                dc.ColumnName = RepairEstimateData._ITEM
                dc.Caption = RepairEstimateData._ITEM
                dt.Columns.Add(dc)

                dc = New DataColumn
                dc.DataType = System.Type.GetType("System.String")
                dc.ColumnName = RepairEstimateData.SUBITEM
                dc.Caption = RepairEstimateData.SUBITEM
                dt.Columns.Add(dc)

                dc = New DataColumn
                dc.DataType = System.Type.GetType("System.String")
                dc.ColumnName = RepairEstimateData.DAMAGE
                dc.Caption = RepairEstimateData.DAMAGE
                dt.Columns.Add(dc)

                dc = New DataColumn
                dc.DataType = System.Type.GetType("System.String")
                dc.ColumnName = RepairEstimateData.REPAIR
                dc.Caption = RepairEstimateData.REPAIR
                dt.Columns.Add(dc)

                dc = New DataColumn
                dc.DataType = System.Type.GetType("System.String")
                dc.ColumnName = RepairEstimateData.REMARKS
                dc.Caption = RepairEstimateData.REMARKS
                dt.Columns.Add(dc)

                dc = New DataColumn
                dc.DataType = System.Type.GetType("System.Decimal")
                dc.ColumnName = RepairEstimateData.MANHOURS
                dc.Caption = RepairEstimateData.MANHOURS
                dt.Columns.Add(dc)

                dc = New DataColumn
                dc.DataType = System.Type.GetType("System.Decimal")
                dc.ColumnName = RepairEstimateData.MATERIALCOST
                dc.Caption = RepairEstimateData.MATERIALCOST
                dt.Columns.Add(dc)

                'Invoice or Party
                dc = New DataColumn
                dc.DataType = System.Type.GetType("System.String")
                dc.ColumnName = RepairEstimateData.CUS_OR_INVC_PARTY
                dc.Caption = RepairEstimateData.CUS_OR_INVC_PARTY
                dt.Columns.Add(dc)

                ' For i = 0 To dtItem.Rows.Count - 1

                For j = 0 To dtSubItem.Rows.Count - 1
                    strSubItemDesc = dtSubItem.Rows(j).Item(RepairEstimateData.SB_ITM_DSCRPTN_VC).ToString
                    strItemCode = dtSubItem.Rows(j).Item(RepairEstimateData.ITM_CD).ToString
                    strSubItemCode = dtSubItem.Rows(j).Item(RepairEstimateData.SB_ITM_CD).ToString
                    dtItem = objRepairEstimates.GetItem(strItemCode, intDepotId)
                    If dtItem.Rows.Count > 0 Then
                        strItemDesc = dtItem.Rows(0).Item(RepairEstimateData.ITM_DSCRPTN_VC).ToString
                    Else
                        strItemDesc = String.Empty
                    End If
                    drRepairEstimateDetail = br_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairEstimateData.ITM_CD, " = '", strItemCode, "' AND ", RepairEstimateData.SB_ITM_CD, " = '", strSubItemCode, "'"))
                    dr = dt.NewRow()
                    If strItemDesc = "No repair" Then
                        dr(RepairEstimateData._ITEM) = ""
                    Else
                        dr(RepairEstimateData._ITEM) = strItemDesc
                    End If

                    dr(RepairEstimateData.SUBITEM) = j + 1 & ". " & strSubItemDesc
                    Dim i As Integer = 0
                    Dim blnRowAdded As Boolean = False
                    For Each drWD As DataRow In drRepairEstimateDetail

                        blnComma = True
                        If Not String.IsNullOrEmpty(drWD.Item(RepairEstimateData.DMG_CD).ToString) Then
                            strDamage = strDamage & drWD.Item(RepairEstimateData.DMG_CD).ToString.Trim
                        End If
                        If Not String.IsNullOrEmpty(drWD.Item(RepairEstimateData.RPR_CD).ToString) Then
                            strRepair = strRepair & drWD.Item(RepairEstimateData.RPR_CD).ToString.Trim
                        End If
                        If Not String.IsNullOrEmpty(drWD.Item(RepairEstimateData.DMG_RPR_DSCRPTN).ToString) Then
                            strRemarks = strRemarks & drWD.Item(RepairEstimateData.DMG_RPR_DSCRPTN).ToString.Trim
                        End If

                        'Invoice or Party
                        If Not String.IsNullOrEmpty(drWD.Item(RepairEstimateData.RSPNSBLTY_CD).ToString) Then
                            strRspnsblty_cd = strRspnsblty_cd & drWD.Item(RepairEstimateData.RSPNSBLTY_CD).ToString.Trim
                        End If


                        decManHours = decManHours + CommonUIs.iDec(drWD.Item(RepairEstimateData.LBR_HRS))
                        decMaterialCost = decMaterialCost + CommonUIs.iDec(drWD.Item(RepairEstimateData.MTRL_CST_NC))
                        i = i + 1
                        Dim iCount As Integer
                        iCount = drRepairEstimateDetail.Length
                        If bv_blnAcaciaSpecifc Then
                            dr = dt.NewRow()
                            dr(RepairEstimateData._ITEM) = strItemDesc
                            dr(RepairEstimateData.SUBITEM) = j + 1 & ". " & strSubItemDesc
                            dr(RepairEstimateData.DAMAGE) = strDamage
                            dr(RepairEstimateData.REPAIR) = strRepair
                            dr(RepairEstimateData.REMARKS) = strRemarks
                            dr(RepairEstimateData.MANHOURS) = CommonUIs.iDec(drWD.Item(RepairEstimateData.LBR_HRS))
                            dr(RepairEstimateData.MATERIALCOST) = CommonUIs.iDec(drWD.Item(RepairEstimateData.MTRL_CST_NC))

                            Dim drAc As DataRow() = dt.Select(String.Concat(RepairEstimateData._ITEM, " = '", strItemDesc, "' AND ", RepairEstimateData.SUBITEM, " = '", dr(RepairEstimateData.SUBITEM), "' AND ", RepairEstimateData.DAMAGE, " ='", strDamage, "' AND ", RepairEstimateData.REPAIR, " ='", strRepair, "' "))
                            If drAc.Length > 0 Then
                                Dim decMat As Decimal = 0
                                Dim decManHr As Decimal = 0
                                Dim strRemark As String = ""
                                For Each drTemp As DataRow In drAc
                                    strRspnsblty_cd = String.Concat(strRspnsblty_cd, drTemp.Item(RepairEstimateData.CUS_OR_INVC_PARTY).ToString)
                                    decManHr = decManHr + CommonUIs.iDec(drTemp.Item(RepairEstimateData.MANHOURS))
                                    decMat = decMat + CommonUIs.iDec(drTemp.Item(RepairEstimateData.MATERIALCOST))
                                    'strRemark = CStr(drTemp.Item(RepairEstimateData.REMARKS))
                                    If dr(RepairEstimateData.REMARKS).ToString.Trim = "" Then
                                    Else
                                        strRemark = String.Concat(drTemp.Item(RepairEstimateData.REMARKS), ",", dr(RepairEstimateData.REMARKS))
                                    End If
                                Next
                                decManHr = decManHr + CommonUIs.iDec(drWD.Item(RepairEstimateData.LBR_HRS))
                                decMat = decMat + CommonUIs.iDec(drWD.Item(RepairEstimateData.MTRL_CST_NC))
                                drAc(0)(RepairEstimateData.MANHOURS) = decManHr
                                drAc(0)(RepairEstimateData.MATERIALCOST) = decMat
                                If dr(RepairEstimateData.REMARKS).ToString.Trim = "" Then
                                Else
                                    drAc(0)(RepairEstimateData.REMARKS) = strRemark
                                End If
                            End If
                            'Invoice or Party
                            If strRspnsblty_cd <> String.Empty AndAlso strRspnsblty_cd.ToLower().Contains("i") AndAlso Not strRspnsblty_cd.ToLower().Contains("c") Then
                                strRspnsblty_cd = "I"
                            End If

                            If strRspnsblty_cd <> String.Empty AndAlso strRspnsblty_cd.ToLower().Contains("c") AndAlso Not strRspnsblty_cd.ToLower().Contains("i") Then
                                strRspnsblty_cd = "C"
                            End If

                            If strRspnsblty_cd <> String.Empty AndAlso strRspnsblty_cd.ToLower().Contains("c") AndAlso strRspnsblty_cd.ToLower().Contains("i") Then
                                strRspnsblty_cd = "C,I"
                            End If

                            dr(RepairEstimateData.CUS_OR_INVC_PARTY) = strRspnsblty_cd

                            If Not drAc.Length > 0 Then
                                dt.Rows.Add(dr)
                            End If
                            blnRowAdded = True
                            strDamage = ""
                            strRepair = ""
                            strRemarks = ""
                            decManHours = 0
                            decMaterialCost = 0
                            strRspnsblty_cd = ""
                        Else
                            If i = iCount Then
                            Else
                                strDamage = strDamage & ","
                                strRepair = strRepair & ","
                                strRspnsblty_cd = strRspnsblty_cd & ","

                                If strRemarks.Trim = "" Then
                                Else
                                    strRemarks = strRemarks & ","
                                End If

                            End If
                        End If
                    Next
                    If bv_blnAcaciaSpecifc Then
                        If Not blnRowAdded Then
                            dr = dt.NewRow()
                            dr(RepairEstimateData._ITEM) = strItemDesc
                            dr(RepairEstimateData.SUBITEM) = j + 1 & ". " & strSubItemDesc
                            dr(RepairEstimateData.DAMAGE) = strDamage
                            dr(RepairEstimateData.REPAIR) = strRepair
                            dr(RepairEstimateData.REMARKS) = strRemarks
                            dr(RepairEstimateData.MANHOURS) = decManHours
                            dr(RepairEstimateData.MATERIALCOST) = decMaterialCost

                            'Invoice or Party
                            If strRspnsblty_cd <> String.Empty AndAlso strRspnsblty_cd.ToLower().Contains("i") AndAlso Not strRspnsblty_cd.ToLower().Contains("c") Then
                                strRspnsblty_cd = "I"
                            End If

                            If strRspnsblty_cd <> String.Empty AndAlso strRspnsblty_cd.ToLower().Contains("c") AndAlso Not strRspnsblty_cd.ToLower().Contains("i") Then
                                strRspnsblty_cd = "C"
                            End If

                            If strRspnsblty_cd <> String.Empty AndAlso strRspnsblty_cd.ToLower().Contains("c") AndAlso strRspnsblty_cd.ToLower().Contains("i") Then
                                strRspnsblty_cd = "C,I"
                            End If

                            dr(RepairEstimateData.CUS_OR_INVC_PARTY) = strRspnsblty_cd

                            dt.Rows.Add(dr)
                            strDamage = ""
                            strRepair = ""
                            strRemarks = ""
                            decManHours = 0
                            decMaterialCost = 0
                            strRspnsblty_cd = ""
                        Else
                            dr = Nothing
                        End If
                        blnRowAdded = False
                    Else
                        dr(RepairEstimateData.DAMAGE) = strDamage
                        dr(RepairEstimateData.REPAIR) = strRepair
                        dr(RepairEstimateData.REMARKS) = strRemarks
                        dr(RepairEstimateData.MANHOURS) = decManHours
                        dr(RepairEstimateData.MATERIALCOST) = decMaterialCost

                        'Invoice or Party
                        If strRspnsblty_cd <> String.Empty AndAlso strRspnsblty_cd.ToLower().Contains("i") AndAlso Not strRspnsblty_cd.ToLower().Contains("c") Then
                            strRspnsblty_cd = "I"
                        End If

                        If strRspnsblty_cd <> String.Empty AndAlso strRspnsblty_cd.ToLower().Contains("c") AndAlso Not strRspnsblty_cd.ToLower().Contains("i") Then
                            strRspnsblty_cd = "C"
                        End If

                        If strRspnsblty_cd <> String.Empty AndAlso strRspnsblty_cd.ToLower().Contains("c") AndAlso strRspnsblty_cd.ToLower().Contains("i") Then
                            strRspnsblty_cd = "C,I"
                        End If

                        dr(RepairEstimateData.CUS_OR_INVC_PARTY) = strRspnsblty_cd

                        dt.Rows.Add(dr)
                        strDamage = ""
                        strRepair = ""
                        strRemarks = ""
                        decManHours = 0
                        decMaterialCost = 0
                        strRspnsblty_cd = ""
                    End If
                Next
                dt.TableName = RepairEstimateData._REPAIRWORDER_DETAIL
                br_dsRepairEstimate.Merge(dt)
                For i = 0 To 1
                    Dim drRepairEstimate As DataRow = br_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).NewRow()
                    drRepairEstimate.Item(RepairEstimateData.SMMRY_ID) = i + 1
                    drRepairEstimate.Item(RepairEstimateData.MN_HR_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.LBR_RT_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.MN_HR_RT_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.MTRL_CST_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.TTL_CST_SMMRY) = 0.0
                    'Added for GWS
                    'drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD)
                    br_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows.Add(drRepairEstimate)
                Next
                'Added for GWS Summary details
                Dim dtSummaryGWS As New DataTable
                Dim dblTotalMaterialcost As Decimal
                Dim dblTotalManHRCost As Decimal
                Dim dblTotalEstimateAmount As Decimal
                Dim dblServTax As Decimal
                Dim dblLaborCost As Decimal
                If br_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                    dtSummaryGWS = objDatasetHelpers.SelectGroupByInto(String.Concat(RepairEstimateData._SUMMARY_DETAIL_GWS), br_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), _
                                                                    "RSPNSBLTY_CD,SUM(LBR_HRS) MN_HR_SMMRY, SUM(LBR_RT) LBR_RT_SMMRY,SUM(LBR_HR_CST_NC) MN_HR_RT_SMMRY,SUM(MTRL_CST_NC) MTRL_CST_SMMRY, SUM(TTL_CST_NC) TTL_CST_SMMRY", _
                                                                                                         "", "RSPNSBLTY_ID")
                    If dtSummaryGWS.Rows.Count > 0 Then
                        br_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Clear()
                        br_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Merge(dtSummaryGWS)
                    End If
                End If

                If br_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Rows.Count > 0 Then
                    For Each drRepairEstimate As DataRow In br_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
                        dblTotalMaterialcost += CDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC))
                        dblTotalManHRCost += CDec(drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC))
                        dblLaborCost += CDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS))
                    Next
                    '   dtRepairEstimate = br_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS)
                End If
                dblTotalEstimateAmount = dblTotalMaterialcost + dblTotalManHRCost
                If (dblTotalEstimateAmount + dblTotalManHRCost + dblTotalMaterialcost) <> 0 Then
                    'dblServTax = ((dblTotalEstimateAmount + dblTotalManHRCost + dblTotalMaterialcost) * CDec(IIf(e.Parameters("TaxRate") Is "", 0.0, e.Parameters("TaxRate"))) / 100)
                    dblServTax = ((dblTotalEstimateAmount + dblTotalManHRCost + dblTotalMaterialcost) * CDec(dblTax) / 100)
                Else
                    dblServTax = 0
                End If
                Dim drRepairSummary As DataRow = br_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_CALCULATION_GWS).NewRow()
                drRepairSummary.Item(RepairEstimateData.TTL_MTRL_CST_SMMRY) = String.Concat(dblTotalMaterialcost.ToString)
                drRepairSummary.Item(RepairEstimateData.TTL_LBR_CST_SMMRY) = String.Concat(dblTotalManHRCost.ToString)
                drRepairSummary.Item(RepairEstimateData.TTL_EST_AMNT) = String.Concat(dblTotalEstimateAmount.ToString)
                drRepairSummary.Item(RepairEstimateData.TTL_LBR_RT_SMMR) = String.Concat(dblLaborCost.ToString)
                If strGWS.ToLower = "true" Then
                    drRepairSummary.Item(RepairEstimateData.TAX_FLG) = True
                Else
                    drRepairSummary.Item(RepairEstimateData.TAX_FLG) = False
                End If

                drRepairSummary.Item(RepairEstimateData.SRVC_TX_AMNT) = dblServTax
                drRepairSummary.Item(RepairEstimateData.TTL_EST_TX_AMNT) = String.Concat(dblServTax + dblTotalEstimateAmount + dblTotalManHRCost + dblTotalMaterialcost)
                br_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_CALCULATION_GWS).Rows.Add(drRepairSummary)



                'Above added for GWS
                For Each drRepairEstimate As DataRow In br_dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
                    pvt_CalculateSummaryDetail(drRepairEstimate.Item(RepairEstimateData.LBR_HRS).ToString, _
                                               drRepairEstimate.Item(RepairEstimateData.LBR_RT).ToString, _
                                               drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC).ToString, _
                                               drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC).ToString, _
                                               drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD).ToString, _
                                               br_dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL))
                Next
                Return br_dsRepairEstimate
            Else

            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    Private Sub pvt_CalculateSummaryDetail(ByVal bv_strLabourHour As String, _
                                         ByVal bv_strLaborHourRate As String, _
                                         ByVal bv_strLabourHourCost As String, _
                                         ByVal bv_strMaterialCost As String, _
                                         ByVal bv_strResponsibilityType As String, _
                                         ByRef br_dtSummaryDetail As DataTable)
        Try
            Dim decMaterialCost As Decimal = 0
            If bv_strMaterialCost <> "" AndAlso bv_strMaterialCost <> Nothing Then
                decMaterialCost = CDec(bv_strMaterialCost)
            End If
            Select Case bv_strResponsibilityType
                Case "C"
                    br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MN_HR_SMMRY) = iDec(br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MN_HR_SMMRY)) + iDec(bv_strLabourHour)
                    br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.LBR_RT_SMMRY) = iDec(br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.LBR_RT_SMMRY)) + iDec(bv_strLaborHourRate)
                    br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MN_HR_RT_SMMRY) = iDec(br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + iDec(bv_strLabourHourCost)
                    br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MTRL_CST_SMMRY) = iDec(br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MTRL_CST_SMMRY)) + decMaterialCost
                    br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.TTL_CST_SMMRY) = CDec(br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + CDec(br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MTRL_CST_SMMRY))
                Case "I"
                    br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MN_HR_SMMRY) = iDec(br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MN_HR_SMMRY)) + iDec(bv_strLabourHour)
                    br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.LBR_RT_SMMRY) = iDec(br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.LBR_RT_SMMRY)) + iDec(bv_strLaborHourRate)
                    br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MN_HR_RT_SMMRY) = iDec(br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + iDec(bv_strLabourHourCost)
                    br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MTRL_CST_SMMRY) = iDec(br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MTRL_CST_SMMRY)) + decMaterialCost
                    br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.TTL_CST_SMMRY) = CDec(br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + CDec(br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MTRL_CST_SMMRY))
            End Select

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Sub

    Public Shared Function iDec(ByVal bv_objObject As Object) As Decimal
        If IsDBNull(bv_objObject) Then
            Return Nothing
        Else
            Return CDec(bv_objObject)
        End If
    End Function

#End Region

#Region "GET :pub_GetLineDeatilbyTariffCodeId() "
    <OperationContract()> _
    Public Function pub_GetLineDeatilbyTariffCodeId(ByVal bv_intDepotId As Int32, _
                                                    ByVal bv_i64TariffId As String, _
                                                    ByVal bv_strTariffGroupId As String) As RepairEstimateDataSet

        Try
            Dim dsRepairEstimateDataSet As RepairEstimateDataSet
            Dim objRepairEstimates As New RepairEstimates
            dsRepairEstimateDataSet = objRepairEstimates.GetLineDeatilbyTariffCodeId(bv_intDepotId, bv_i64TariffId, bv_strTariffGroupId)
            Return dsRepairEstimateDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET :pub_GetGetActivityStatusByEqpmntNo()  Table Name: Activity_Status"
    <OperationContract()> _
    Public Function pub_GetGetActivityStatusByEqpmntNo(ByVal bv_intDepotId As Int32, _
                                                       ByVal bv_strEquipmentNo As String, _
                                                       ByVal bv_GateinTransaction As String) As RepairEstimateDataSet

        Try
            Dim dsRepairEstimateDataSet As RepairEstimateDataSet
            Dim objRepairEstimates As New RepairEstimates
            dsRepairEstimateDataSet = objRepairEstimates.GetActivityStatusByEqpmntNo(bv_intDepotId, bv_strEquipmentNo, bv_GateinTransaction)
            Return dsRepairEstimateDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET :Pub_GetCurrencyExchangeRateByDptId() "
    <OperationContract()> _
    Public Function Pub_GetCurrencyExchangeRateByDptId(ByVal bv_intDepotId As Int32, ByVal bv_i64CustomerId As Int64) As RepairEstimateDataSet
        Try
            Dim dsRepairEstimateDataSet As RepairEstimateDataSet
            Dim objRepairEstimates As New RepairEstimates
            dsRepairEstimateDataSet = objRepairEstimates.GetCurrencyExchangeRateByDptId(bv_intDepotId, bv_i64CustomerId)
            Return dsRepairEstimateDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function


    <OperationContract()> _
    Public Function Pub_GetAgentCurrencyExchangeRateByDptId(ByVal bv_intDepotId As Int32, ByVal bv_i64AgentId As Int64) As DataTable
        Try

            Dim objRepairEstimates As New RepairEstimates
            Return objRepairEstimates.GetAgentCurrencyExchangeRateByDptId(bv_intDepotId, bv_i64AgentId)

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "GET :Pub_GetAttachmentByRepairEstimateId() "
    <OperationContract()> _
    Public Function Pub_GetAttachmentByRepairEstimateId(ByVal bv_intDepotId As Int32, ByVal bv_strActivity As String, ByVal bv_i64RepairEstimateId As Int64) As RepairEstimateDataSet
        Try
            Dim dsRepairEstimateDataSet As New RepairEstimateDataSet
            Dim objRepairEstimates As New RepairEstimates
            dsRepairEstimateDataSet = objRepairEstimates.GetAttachmentByRepairEstimateId(bv_intDepotId, bv_strActivity, bv_i64RepairEstimateId)
            Return dsRepairEstimateDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET :Pub_GetAttachmentByRepairEstimateNo() "
    <OperationContract()> _
    Public Function Pub_GetAttachmentByRepairEstimateNo(ByVal bv_intDepotId As Int32, _
                                                        ByVal bv_strRepairEstimateNo As String, _
                                                        ByVal bv_lngEstimateID As Long, _
                                                        ByVal bv_strfilterUpload As String) As RepairEstimateDataSet
        Try
            Dim dsRepairEstimateDataSet As RepairEstimateDataSet
            Dim objRepairEstimates As New RepairEstimates
            Dim strOrderBy As String = String.Empty
            strOrderBy = pvt_GenerateAttachmentOrderByQuery(bv_strfilterUpload)
            dsRepairEstimateDataSet = objRepairEstimates.GetAttachmentByRepairEstimateNo(bv_intDepotId, bv_strRepairEstimateNo, bv_lngEstimateID, strOrderBy)
            Return dsRepairEstimateDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "EncryptData()"
    <OperationContract()> _
    Public Function EncryptData(Message As String) As String
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        Dim Results As Byte()
        Dim pvt_strKeyPhrase As String
        Try
            ' pvt_strKeyPhrase = IO.File.GetLastWriteTime(String.Concat(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings("KeyFileName"))).ToString("yyMMddHHHmmss")
            pvt_strKeyPhrase = "IIC"
            Dim UTF8 As New System.Text.UTF8Encoding()
            Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(pvt_strKeyPhrase))
            TDESAlgorithm.Key = TDESKey
            TDESAlgorithm.Mode = CipherMode.ECB
            TDESAlgorithm.Padding = PaddingMode.PKCS7
            Dim DataToEncrypt As Byte() = UTF8.GetBytes(Message)
            Dim Encryptor As ICryptoTransform = TDESAlgorithm.CreateEncryptor()
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return Convert.ToBase64String(Results)
    End Function
#End Region

#Region "GET :Pub_GetTariffDetails() "
    <OperationContract()> _
    Public Function Pub_GetTariffDetails(ByVal bv_strItemId As String, _
                                         ByVal bv_strSubItemId As String, _
                                         ByVal bv_strDamageID As String, _
                                         ByVal bv_strRepairId As String, _
                                         ByVal bv_i32DepotId As Int32) As RepairEstimateDataSet
        Try
            Dim dsRepairEstimateDataSet As RepairEstimateDataSet
            Dim objRepairEstimates As New RepairEstimates
            dsRepairEstimateDataSet = objRepairEstimates.GetTariffDetails(bv_strItemId, bv_strSubItemId, bv_strDamageID, bv_strRepairId, bv_i32DepotId)
            Return dsRepairEstimateDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET :Pub_GetAttachmentByActivityName()  Table Name: Attachment"
    <OperationContract()> _
    Public Function Pub_GetAttachmentByActivityName(ByVal bv_intDepotId As Int32, _
                                                    ByVal bv_strRepairEstimateNo As String, _
                                                    ByVal bv_strActivityName As String, _
                                                    ByVal bv_strfilterUpload As String) As RepairEstimateDataSet
        Try
            Dim dsRepairEstimateDataSet As RepairEstimateDataSet
            Dim objRepairEstimates As New RepairEstimates
            Dim strOrderBy As String = String.Empty
            strOrderBy = pvt_GenerateAttachmentOrderByQuery(bv_strfilterUpload)
            If bv_strActivityName.Contains("'") Then
                bv_strActivityName = String.Concat(RepairEstimateData.ACTVTY_NAM, " IN (", bv_strActivityName, ")")
            Else
                bv_strActivityName = String.Concat(RepairEstimateData.ACTVTY_NAM, " IN ('", bv_strActivityName, "')")
            End If

            dsRepairEstimateDataSet = objRepairEstimates.GetAttachmentByActivityName(bv_intDepotId, bv_strRepairEstimateNo, bv_strActivityName, strOrderBy)
            Return dsRepairEstimateDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_GenerateAttachmentOrderByQuery()"
    <OperationContract()> _
    Private Function pvt_GenerateAttachmentOrderByQuery(ByVal bv_strFilterUpload As String) As String
        Try
            Dim i As Integer = 0
            Dim strfilterUpload As String = bv_strFilterUpload
            Dim strQuery As String = " ORDER BY CASE  "
            Dim strFilter As String() = Nothing
            strFilter = strfilterUpload.Split(CChar(","))
            If strFilter.Length > 0 Then
                For Each Str As String In strFilter
                    i += 1
                    strQuery = String.Concat(strQuery, " WHEN PARSENAME(REPLACE(" & RepairEstimateData.ACTL_FL_NM & "  , '', '.'), 1) = '", Str, "' THEN ", i)
                Next
                strQuery = String.Concat(strQuery, " END ")
            End If
            Return strQuery
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

    'Attachment
#Region "pub_GetAttchemntbyGateIN"
    <OperationContract()> _
    Public Function pub_GetAttchemntbyGateIN(ByVal bv_strGiTransactionNo As String, ByVal bv_strActivityName As String) As RepairEstimateDataSet

        Try
            Dim dsGateoutDataset As RepairEstimateDataSet
            Dim objGateouts As New RepairEstimates
            dsGateoutDataset = objGateouts.pub_GetAttchemntbyGateIN(bv_strGiTransactionNo, bv_strActivityName)
            Return dsGateoutDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

    Public Function GetTariffCodeTable(ByVal intAgentID As Integer, _
                                    ByVal intCustmrID As Integer, _
                                    ByVal intEquipTypeID As Integer, _
                                    ByVal intDepotID As Integer, _
                                    ByVal strBillTo As String) As DataTable
        Dim objEstimates As New RepairEstimates
        Return objEstimates.GetTariffDetail(intAgentID, intCustmrID, intEquipTypeID, intDepotID, strBillTo)
    End Function

    Function GetTariffCodeTable(intDepotId As Integer, intTariffId As Object) As RepairEstimateDataSet
        Dim objEstimates As New RepairEstimates
        Return objEstimates.GetTariffDetail(intDepotId, intTariffId)
    End Function

    Function GetAgentIdByCustomer(intCustId As Integer, intDepotId As Integer) As DataTable
        Dim objEstimates As New RepairEstimates
        Return objEstimates.GetAgentIdByCustomer(intDepotId, intCustId)
    End Function

#Region "GET : pub_GetLaborRateperHourByAgentID() "

    <OperationContract()> _
    Public Function pub_GetLaborRateperHourByAgentID(ByVal bv_i32DepotID As Int32, ByVal bv_i64AgentID As Int64) As String

        Try
            Dim objRepairEstimates As New RepairEstimates
            Dim strLaborRate As String
            strLaborRate = objRepairEstimates.LaborRateperHourByAgentID(bv_i32DepotID, bv_i64AgentID)
            Return strLaborRate
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


    Function GetCurrByAgntId(intAgentId As Integer, intDepotID As Integer) As DataTable
        Try
            Dim objRepairEstimates As New RepairEstimates
            Dim dtCurrcty As DataTable
            dtCurrcty = objRepairEstimates.GetCurrByAgntId(intAgentId, intDepotID)
            Return dtCurrcty
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    Function GetCurrByCstmrId(intCustId As Integer, intDepotID As Integer) As DataTable
        Try
            Dim objRepairEstimates As New RepairEstimates
            Dim dtCurrcty As DataTable
            dtCurrcty = objRepairEstimates.GetCurrByCstmrId(intCustId, intDepotID)
            Return dtCurrcty
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

End Class
