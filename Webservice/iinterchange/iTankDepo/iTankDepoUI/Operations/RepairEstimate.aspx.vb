Option Strict On

Imports Newtonsoft.Json

Partial Class Operations_RepairEstimate
    Inherits Pagebase
    Dim dsRepairEstimate As New RepairEstimateDataSet
    Dim dtRepairEstimate As DataTable
    Private Const REPAIR_ESTIMATE As String = "REPAIR_ESTIMATE"
    Private Const MODE_CREATE As String = "Creation"
    Private Const MODE_REVISE As String = "Revision"
    Private Const REPAIR_ESTIMATE_CREATION As String = "Repair Estimate"
    Private Const REPAIR_APPROVAL As String = "Repair Approval"
    Private Const SURVEY_COMPLETION As String = "Survey Completion"
    Private strMSGUPDATE As String = " Updated Successfully."
    Private strMSGINSERT As String = " Created Successfully."
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
    Dim strArrFilter As String() = {RepairEstimateData.RPR_ID, RepairEstimateData.DMG_ID, RepairEstimateData.ITM_ID, RepairEstimateData.SB_ITM_ID}
    Dim blnShowCheckbox As Boolean
    Private Const REPAIR_ESTIMATEMODE As String = "REPAIR_ESTIMATEMODE"
    Private Const PAGENAME As String = "PAGENAME-RE"
    Private Const PAGEMODE As String = "PAGEMODE-RE"
    Dim objCommonUI As New CommonUI
    Dim str_032KeyValue As String
    Dim bln_032EqType_Key As Boolean
    Dim objCommonConfig As New ConfigSetting()
    Dim bln_045KeyExist As Boolean
    Dim bln_044KeyExist As Boolean
    Dim str_044KeyValue As String
    Dim str_045KeyValue As String
    Dim str_057GWS As String
    Dim bln_057GWSActive_Key As Boolean
    Dim bv_strEquipmentNo As String
    Dim bv_strGateinTransaction As String

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                datEstimationDate.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy")
                datPreviousONH.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy")
                pvt_SetChangesMade()
                Dim objCommondata As New CommonData
                Dim str_061KeyValue As String
                Dim bln_061Key As Boolean
                Dim bln_063Key As Boolean
                Dim bln_065Key As Boolean
                Dim str_063KeyValue As String
                Dim str_065KeyValue As String
                str_061KeyValue = objCommondata.GetConfigSetting("061", bln_061Key)
                str_063KeyValue = objCommondata.GetConfigSetting("063", bln_063Key)
                str_065KeyValue = objCommondata.GetConfigSetting("065", bln_065Key)
                If bln_061Key OrElse bln_065Key Then

                    HideMenusGWS(str_061KeyValue, str_063KeyValue, str_065KeyValue)

                End If
                Dim strSessionId As String = objCommondata.GetSessionID()
                Dim strActivityName As String = String.Empty
                If Not pub_GetQueryString("activityname") Is Nothing Then
                    strActivityName = pub_GetQueryString("activityname")
                End If
                If strActivityName = "Repair Approval" Then
                    datApprovalDate.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy")
                ElseIf strActivityName = "Survey Completion" Then
                    datSurveyDate.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy")
                End If
                If strActivityName <> "Repair Approval" Then
                    'lblApprovalAmount.Visible = False
                    'txtApprovalAmount.Visible = False
                    lblPartyRef.Visible = False
                    lblApprovalref.Visible = False
                    txtPartyRef.Visible = False
                    txtApprovalRef.Visible = False
                    datApprovalDate.Visible = False
                    datApprovalDate.Validator.IsRequired = False
                    lblApprovRefReq.Visible = False
                ElseIf str_065KeyValue.ToLower = "true" Then
                    txtApprovalAmount.ReadOnly = True
                    lblPartyRef.Visible = False
                    lblApprovalAmount.Visible = True
                    lblApprovalDate.Visible = True
                    lblApprovalDateSym.Visible = True
                    txtApprovalAmount.Visible = True
                    datApprovalDate.Visible = True
                    datApprovalDate.Validator.IsRequired = True
                    txtApprovalRef.Visible = True
                    txtPartyRef.Visible = False
                    lblApprovalref.Visible = True
                    lblApprovRefReq.Visible = True
                    lblApprovalref.Text = "Approval Reference No"
                End If
                objCommondata.FlushLockDataByActivityName(RepairEstimateData.EQPMNT_NO, strSessionId, strActivityName)
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
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/RepairEstimate.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "CreateRepairEstimate"
                    pvt_CreateRepairEstimate(e.GetCallbackValue("CustomerId"), _
                                             e.GetCallbackValue("CustomerCd"), _
                                             e.GetCallbackValue("EstimationDate"), _
                                             e.GetCallbackValue("OrginalEstimateDate"), _
                                             e.GetCallbackValue("StatusID"), _
                                             e.GetCallbackValue("StatusCode"), _
                                             e.GetCallbackValue("EquipmentNo"), _
                                             e.GetCallbackValue("EIRNo"), _
                                             e.GetCallbackValue("LastTestDate"), _
                                             e.GetCallbackValue("LastTestTypeID"), _
                                             e.GetCallbackValue("LastTestTypeCode"), _
                                             e.GetCallbackValue("ValidityPeriodYear"), _
                                             e.GetCallbackValue("NextTestDate"), _
                                             e.GetCallbackValue("LastSurveyor"), _
                                             e.GetCallbackValue("NextTestTypeID"), _
                                             e.GetCallbackValue("NextTestTypeCode"), _
                                             e.GetCallbackValue("RepairTypeID"), _
                                             e.GetCallbackValue("RepairTypeCode"), _
                                             e.GetCallbackValue("blnCertofCleanlinessBit"), _
                                             e.GetCallbackValue("InvoicingPartyID"), _
                                             e.GetCallbackValue("InvoicingPartyCode"), _
                                             e.GetCallbackValue("LaborRate"), _
                                             e.GetCallbackValue("ApprovalDate"), _
                                             e.GetCallbackValue("ApprovalRef"), _
                                             e.GetCallbackValue("SurveyDate"), _
                                             e.GetCallbackValue("SurveyName"), _
                                             e.GetCallbackValue("WFData"), _
                                             e.GetCallbackValue("Mode"), _
                                             e.GetCallbackValue("EstimateID"), _
                                             e.GetCallbackValue("RevisionNo"), _
                                             e.GetCallbackValue("Remarks"), _
                                             e.GetCallbackValue("EstimationNo"), _
                                             e.GetCallbackValue("CustomerEstimatedCost"), _
                                             e.GetCallbackValue("CustomerApprovedCost"), _
                                             e.GetCallbackValue("PartyApprovalRef"), _
                                             CInt(e.GetCallbackValue("ActivityId")))
                Case "UpadteRepairEstimate"
                    pvt_UpdateRepairEstimate(e.GetCallbackValue("RepairEstimateId"), _
                                             e.GetCallbackValue("CustomerId"), _
                                             e.GetCallbackValue("CustomerCd"), _
                                             e.GetCallbackValue("EstimationDate"), _
                                             e.GetCallbackValue("OrginalEstimateDate"), _
                                             e.GetCallbackValue("StatusID"), _
                                             e.GetCallbackValue("StatusCode"), _
                                             e.GetCallbackValue("EquipmentNo"), _
                                             e.GetCallbackValue("EIRNo"), _
                                             e.GetCallbackValue("GateInTrnsactionNo"), _
                                             e.GetCallbackValue("LastTestDate"), _
                                             e.GetCallbackValue("LastTestTypeID"), _
                                             e.GetCallbackValue("LastTestTypeCode"), _
                                             e.GetCallbackValue("ValidityPeriodYear"), _
                                             e.GetCallbackValue("NextTestDate"), _
                                             e.GetCallbackValue("LastSurveyor"), _
                                             e.GetCallbackValue("NextTestTypeID"), _
                                             e.GetCallbackValue("NextTestTypeCode"), _
                                             e.GetCallbackValue("RepairTypeID"), _
                                             e.GetCallbackValue("RepairTypeCode"), _
                                             e.GetCallbackValue("blnCertofCleanlinessBit"), _
                                             e.GetCallbackValue("InvoicingPartyID"), _
                                             e.GetCallbackValue("InvoicingPartyCode"), _
                                             e.GetCallbackValue("LaborRate"), _
                                             e.GetCallbackValue("ApprovalAmount"), _
                                             e.GetCallbackValue("ApprovalDate"), _
                                             e.GetCallbackValue("ApprovalRef"), _
                                             e.GetCallbackValue("SurveyDate"), _
                                             e.GetCallbackValue("SurveyName"), _
                                             e.GetCallbackValue("WFData"), _
                                             e.GetCallbackValue("Mode"), _
                                             e.GetCallbackValue("EstimateID"), _
                                             e.GetCallbackValue("RevisionNo"), _
                                             e.GetCallbackValue("Remarks"), _
                                             e.GetCallbackValue("EstimationNo"), _
                                             e.GetCallbackValue("CustomerEstimatedCost"), _
                                             e.GetCallbackValue("CustomerApprovedCost"), _
                                             e.GetCallbackValue("PartyApprovalRef"), _
                                             CInt(e.GetCallbackValue("ActivityId")))
                Case "fnGetData"
                    pvt_GetData(e.GetCallbackValue("mode"))
                Case "ValidatePreviousActivityDate"
                    pvt_ValidatePreviousActivityDate(e.GetCallbackValue("EquipmentNo"), _
                                                     e.GetCallbackValue("EventDate"), _
                                                     e.GetCallbackValue("PageName"))
                Case "GetExchangeRate"
                    pvt_GetExchangeRate(e.GetCallbackValue("fromCurrencyId"), e.GetCallbackValue("toCurrencyId"))
                Case "checkSelectBit"
                    pvt_CheckSelectBit()
                Case "validateInvoicingParty"
                    pvt_validateInvoicingParty(e.GetCallbackValue("InvoicingParty"))
                    'Attachment
                Case "ValidateGateINAttachment"
                    pvt_ValidateGateINAttachment(e.GetCallbackValue("GITransaction"), e.GetCallbackValue("EquipmentNo"))
                Case "CreateRepairEstimateGWS"
                    pvt_CreateRepairEstimateGWS(e.GetCallbackValue("CustomerId"), _
                                            e.GetCallbackValue("CustomerCd"), _
                                            e.GetCallbackValue("EstimationDate"), _
                                            e.GetCallbackValue("OrginalEstimateDate"), _
                                            e.GetCallbackValue("StatusID"), _
                                            e.GetCallbackValue("StatusCode"), _
                                            e.GetCallbackValue("EquipmentNo"), _
                                            e.GetCallbackValue("EIRNo"), _
                                            e.GetCallbackValue("LaborRate"), _
                                            e.GetCallbackValue("ApprovalDate"), _
                                            e.GetCallbackValue("ApprovalRef"), _
                                            e.GetCallbackValue("SurveyDate"), _
                                            e.GetCallbackValue("SurveyName"), _
                                            e.GetCallbackValue("WFData"), _
                                            e.GetCallbackValue("Mode"), _
                                            e.GetCallbackValue("EstimateID"), _
                                            e.GetCallbackValue("RevisionNo"), _
                                            e.GetCallbackValue("Remarks"), _
                                            e.GetCallbackValue("EstimationNo"), _
                                            e.GetCallbackValue("CustomerEstimatedCost"), _
                                            e.GetCallbackValue("CustomerApprovedCost"), _
                                            e.GetCallbackValue("PartyApprovalRef"), _
                                            e.GetCallbackValue("PrevONHLocation"), _
                                            e.GetCallbackValue("PrevONHLocationCode"), _
                                            e.GetCallbackValue("PrevONHLocDate"), _
                                            e.GetCallbackValue("Measure"), _
                                            e.GetCallbackValue("Unit"), _
                                            e.GetCallbackValue("BillTo"), _
                                            e.GetCallbackValue("AgentName"), _
                                            e.GetCallbackValue("TaxRate"), _
                                            e.GetCallbackValue("Consignee"), _
                                            CInt(e.GetCallbackValue("ActivityId")), _
                                            CInt(e.GetCallbackValue("EquipmentStatusId")), _
                                            e.GetCallbackValue("MeasureCode"), _
                                            e.GetCallbackValue("UnitCode"))
                Case "UpdateRepairEstimateGWS"
                    pvt_UpdateRepairEstimateGWS(e.GetCallbackValue("RepairEstimateId"), _
                                            e.GetCallbackValue("CustomerId"), _
                                            e.GetCallbackValue("CustomerCd"), _
                                           e.GetCallbackValue("EstimationDate"), _
                                           e.GetCallbackValue("OrginalEstimateDate"), _
                                           e.GetCallbackValue("StatusID"), _
                                           e.GetCallbackValue("StatusCode"), _
                                          e.GetCallbackValue("EquipmentNo"), _
                                          e.GetCallbackValue("EIRNo"), _
                                          e.GetCallbackValue("LaborRate"), _
                                          e.GetCallbackValue("ApprovalDate"), _
                                          e.GetCallbackValue("ApprovalRef"), _
                                          e.GetCallbackValue("SurveyDate"), _
                                          e.GetCallbackValue("SurveyName"), _
                                          e.GetCallbackValue("WFData"), _
                                          e.GetCallbackValue("Mode"), _
                                          e.GetCallbackValue("EstimateID"), _
                                          e.GetCallbackValue("RevisionNo"), _
                                          e.GetCallbackValue("Remarks"), _
                                          e.GetCallbackValue("EstimationNo"), _
                                          e.GetCallbackValue("CustomerEstimatedCost"), _
                                          e.GetCallbackValue("CustomerApprovedCost"), _
                                          e.GetCallbackValue("PartyApprovalRef"), _
                                          e.GetCallbackValue("PrevONHLocation"), _
                                          e.GetCallbackValue("PrevONHLocationCode"), _
                                          e.GetCallbackValue("PrevONHLocDate"), _
                                          e.GetCallbackValue("Measure"), _
                                          e.GetCallbackValue("Unit"), _
                                          e.GetCallbackValue("BillTo"), _
                                          e.GetCallbackValue("AgentName"), _
                                          e.GetCallbackValue("TaxRate"), _
                                          e.GetCallbackValue("GateInTrnsactionNo"), _
                                          e.GetCallbackValue("Consignee"), _
                                          CInt(e.GetCallbackValue("ActivityId")), _
                                          CInt(e.GetCallbackValue("EquipmentStatusId")), _
                                          e.GetCallbackValue("MeasureCode"), _
                                          e.GetCallbackValue("UnitCode"))

                Case "fetchCustomerTariff"
                    pvt_GetCustomerTariff(e.GetCallbackValue("CustomerId"), _
                                          e.GetCallbackValue("AgentId"), _
                                          e.GetCallbackValue("BillTo"), _
                                          e.GetCallbackValue("EquipType"))
            End Select
        Catch ex As Exception
            'iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _pub_GetEquipmentInformationByEqpmntNo
            '                               MethodBase.GetCurrentMethod.Name, ex)

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetData"
    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim objCommon As New CommonData()

            Dim datGetDateTime As DateTime = CDate(objCommon.GetCurrentDate())
            Dim sbRepairEstimate As New StringBuilder
            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            str_032KeyValue = objCommonConfig.pub_GetConfigSingleValue("032", intDepotID)
            bln_032EqType_Key = objCommonConfig.IsKeyExists
            Dim str_061KeyValue As String
            Dim bln_061Key As Boolean
            bln_061Key = objCommonConfig.IsKeyExists
            str_061KeyValue = objCommonConfig.pub_GetConfigSingleValue("061", intDepotID)
            Dim str_063KeyValue As String
            Dim bln_063Key As Boolean
            bln_063Key = objCommonConfig.IsKeyExists
            str_063KeyValue = objCommonConfig.pub_GetConfigSingleValue("063", intDepotID)
            Dim str_065KeyValue As String
            Dim bln_065Key As Boolean
            Dim dsEqpStatus As New DataSet
            Dim blnShowEqStatus As Boolean
            bln_065Key = objCommonConfig.IsKeyExists
            str_065KeyValue = objCommonConfig.pub_GetConfigSingleValue("065", intDepotID)

            sbRepairEstimate.Append("showSubmitButton(true);")
            If Not PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_NO) Is Nothing Then

                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_STTS_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_STTS_ID) <> "" Then
                    ' sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, "", ""))
                Else
                    ' sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_STTS_ID), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_STTS_CD)))
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnStatusCD, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_STTS_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_CD) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtCustomer, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtCustomer, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCustomerId, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCustomerId, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.GTN_BIN) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnGateInBin, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnGateInBin, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.GTN_BIN)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnPageName, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnPageName, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME)))
                End If
                If bln_063Key Then
                    If str_063KeyValue.ToLower = "true" Then
                        If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.TX_RT_PRCNT) Is Nothing Then
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtTaxRate, "0.00"))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtTaxRate, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.TX_RT_PRCNT)))
                        End If
                    End If
                End If

                'If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_NO) Is Nothing Then
                '    sbRepairEstimate.Append("setText(el('hypEquipmentNo'),'');")
                'Else
                '    sbRepairEstimate.Append(String.Concat("setText(el('hypEquipmentNo'),'" + PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_NO) + "');"))
                'End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_NO) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtEquipmentNo, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtEquipmentNo, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_NO)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_TYP_ID) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEqpTypID, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEqpTypID, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_TYP_ID)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_CD_ID) Is Nothing Then

                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEqpCDID, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEqpCDID, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_CD_ID)))
                End If

                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_TYP_ID) Is Nothing Then
                    If bln_032EqType_Key Then
                        sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpRepairType, objCommon.GetEnumID("REPAIR TYPE", str_032KeyValue), UCase(str_032KeyValue)))
                    Else
                        sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpRepairType, "", ""))
                    End If

                Else
                    sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpRepairType, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_TYP_ID), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_TYP_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.INVCNG_PRTY_ID) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpInvoiceparty, "", ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpInvoiceparty, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.INVCNG_PRTY_ID), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.INVCNG_PRTY_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ESTMTN_TTL_NC) Is Nothing Then
                    sbRepairEstimate.Append("setText(el('hypTotalCost'),'0.00');")
                Else
                    sbRepairEstimate.Append(String.Concat("setText(el('hypTotalCost'),'" + PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ESTMTN_TTL_NC) + "');"))

                End If
                'End If





                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RMRKS_VC) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtRemarks, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtRemarks, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RMRKS_VC)))
                End If
                sbRepairEstimate.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_ESTMT_ID), "');"))
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.GI_TRNSCTN_NO) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEirNo, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEirNo, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.GI_TRNSCTN_NO)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_ESTMT_NO) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnRprEstimationNo, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnRprEstimationNo, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_ESTMT_NO)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_ESTMT_ID) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEstimateID, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEstimateID, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_ESTMT_ID)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RVSN_NO) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnRevisionNo, ""))
                Else
                    Dim intRevision As Integer
                    intRevision = CInt(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RVSN_NO))
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnRevisionNo, CStr(intRevision)))
                End If
                Dim intAgentID As Integer = 0
                Dim strAgentCD As String = ""
                Dim objRepairEstimate As New RepairEstimate
                Dim dtEqipmentInformation As New DataTable
                intDepotID = CInt(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.DPT_ID))
                If bln_061Key Then
                    If str_061KeyValue.ToLower = "true" Then
                        Dim objEstimate As New RepairEstimate

                        Dim dt As DataTable

                        If Not PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID) Is Nothing Then
                            dt = objEstimate.GetAgentIdByCustomer(CInt(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID)), intDepotID)
                            If dt.Rows.Count > 0 Then
                                intAgentID = CInt(dt.Rows(0).Item("AGNT_ID"))
                                strAgentCD = CStr(dt.Rows(0).Item("AGNT_NAM"))
                            End If
                        End If

                        Dim ActivityName As String = Server.UrlDecode(objCommon.HttpContextGetQueryString("activityname")).ToString

                        If bv_strMode = MODE_NEW AndAlso ActivityName.ToUpper() = "REPAIR ESTIMATE" AndAlso intAgentID <> 0 Then

                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtAgentName, strAgentCD))
                            sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAgentId, intAgentID.ToString))
                            'sbRepairEstimate.Append("setText(el('lkpBillTo'),'AGENT');")
                            sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpBillTo, 144, "AGENT"))
                            sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnBillTo, "AGENT"))
                            Dim strLaborRate As String = ""
                            strLaborRate = objRepairEstimate.pub_GetLaborRateperHourByAgentID(intDepotID, CLng(intAgentID))
                            If strLaborRate Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, ""))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, strLaborRate))
                            End If

                        ElseIf bv_strMode = MODE_NEW AndAlso ActivityName.ToUpper() = "REPAIR ESTIMATE" AndAlso intAgentID = 0 Then

                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtAgentName, strAgentCD))
                            ' sbRepairEstimate.Append("setText(el('lkpBillTo'),'CUSTOMER'); setReadOnly('lkpBillTo', true); ")
                            sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAgentId, "0"))
                            sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpBillTo, 145, "CUSTOMER"))

                            sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnBillTo, "CUSTOMER"))
                            Dim strLaborRate As String = ""
                            strLaborRate = objRepairEstimate.pub_GetLaborRateperHourByCustomerID(intDepotID, CLng(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID)))
                            If strLaborRate Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, ""))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, strLaborRate))
                            End If

                        ElseIf bv_strMode = MODE_EDIT AndAlso ActivityName.ToUpper() = "REPAIR ESTIMATE" Then

                            If PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID) <> Nothing AndAlso PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID).ToUpper() = "AGENT" Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtAgentName, strAgentCD))
                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAgentId, intAgentID.ToString))
                                'sbRepairEstimate.Append("setText(el('lkpBillTo'),'AGENT');")
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpBillTo, 144, "AGENT"))
                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnBillTo, "AGENT"))
                                Dim strLaborRate As String = ""
                                strLaborRate = objRepairEstimate.pub_GetLaborRateperHourByAgentID(intDepotID, CLng(intAgentID))
                                If strLaborRate Is Nothing Then
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, ""))
                                Else
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, strLaborRate))
                                End If
                            End If


                            If PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID) <> Nothing AndAlso PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID).ToUpper() = "CUSTOMER" Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtAgentName, strAgentCD))
                                ' sbRepairEstimate.Append("setText(el('lkpBillTo'),'CUSTOMER'); setReadOnly('lkpBillTo', true); ")
                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAgentId, "0"))
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpBillTo, 145, "CUSTOMER"))

                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnBillTo, "CUSTOMER"))
                                Dim strLaborRate As String = ""
                                strLaborRate = objRepairEstimate.pub_GetLaborRateperHourByCustomerID(intDepotID, CLng(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID)))
                                If strLaborRate Is Nothing Then
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, ""))
                                Else
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, strLaborRate))
                                End If
                            End If


                        ElseIf bv_strMode = MODE_NEW AndAlso ActivityName.ToUpper() = "REPAIR APPROVAL" Then

                            If PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID) <> Nothing AndAlso PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID).ToUpper() = "AGENT" Then
                                sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datApprovalDate, Now.ToString("dd-MMM-yyyy").ToUpper))
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtAgentName, strAgentCD))
                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAgentId, intAgentID.ToString))
                                'sbRepairEstimate.Append("setText(el('lkpBillTo'),'AGENT');")
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpBillTo, 144, "AGENT"))
                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnBillTo, "AGENT"))
                                Dim strLaborRate As String = ""
                                strLaborRate = objRepairEstimate.pub_GetLaborRateperHourByAgentID(intDepotID, CLng(intAgentID))
                                If strLaborRate Is Nothing Then
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, ""))
                                Else
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, strLaborRate))
                                End If
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtAgentName, PageSubmitPane.pub_GetPageAttribute("Agent")))

                            ElseIf PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID) <> Nothing AndAlso PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID).ToUpper() = "CUSTOMER" Then
                                sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datApprovalDate, Now.ToString("dd-MMM-yyyy").ToUpper))
                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAgentId, intAgentID.ToString))
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtAgentName, strAgentCD))
                                ' sbRepairEstimate.Append("setText(el('lkpBillTo'),'CUSTOMER'); setReadOnly('lkpBillTo', true); ")

                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpBillTo, 145, "CUSTOMER"))

                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnBillTo, "CUSTOMER"))
                                Dim strLaborRate As String = ""
                                strLaborRate = objRepairEstimate.pub_GetLaborRateperHourByCustomerID(intDepotID, CLng(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID)))
                                If strLaborRate Is Nothing Then
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, ""))
                                Else
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, strLaborRate))
                                End If

                            End If
                            If Not PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_APPRVL_AMNT_NC) Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtApprovalAmount, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_APPRVL_AMNT_NC)))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtApprovalAmount, "0.00"))
                            End If

                        ElseIf bv_strMode = MODE_EDIT AndAlso ActivityName.ToUpper() = "REPAIR APPROVAL" Then
                            If Not PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_APPRVL_AMNT_NC) Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtApprovalAmount, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_APPRVL_AMNT_NC)))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtApprovalAmount, "0.00"))
                            End If
                            If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.STTS_ID) <> Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpEquipStatusGWS, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.STTS_ID), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.STTS_CD)))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpEquipStatusGWS, "", ""))
                            End If

                            If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.OWNR_APPRVL_RF) <> Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtApprovalRef, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.OWNR_APPRVL_RF)))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtApprovalRef, ""))
                            End If

                            If PageSubmitPane.pub_GetPageAttribute("Approval Date") <> Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datApprovalDate, CDate(PageSubmitPane.pub_GetPageAttribute("Approval Date")).ToString("dd-MMM-yyyy").ToUpper))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datApprovalDate, Now.ToString("dd-MMM-yyyy").ToUpper))
                            End If

                            If PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID) <> Nothing AndAlso PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID).ToUpper() = "AGENT" Then

                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtAgentName, strAgentCD))
                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAgentId, intAgentID.ToString))
                                'sbRepairEstimate.Append("setText(el('lkpBillTo'),'AGENT');")
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpBillTo, 144, "AGENT"))
                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnBillTo, "AGENT"))
                                Dim strLaborRate As String = ""
                                strLaborRate = objRepairEstimate.pub_GetLaborRateperHourByAgentID(intDepotID, CLng(intAgentID))
                                If strLaborRate Is Nothing Then
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, ""))
                                Else
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, strLaborRate))
                                End If
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtAgentName, strAgentCD))
                                ' sbRepairEstimate.Append("setText(el('lkpBillTo'),'CUSTOMER'); setReadOnly('lkpBillTo', true); ")
                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAgentId, intAgentID.ToString))
                                'sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAgentId, "0"))
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpBillTo, 145, "CUSTOMER"))

                                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnBillTo, "CUSTOMER"))
                                Dim strLaborRate As String = ""
                                strLaborRate = objRepairEstimate.pub_GetLaborRateperHourByCustomerID(intDepotID, CLng(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID)))
                                If strLaborRate Is Nothing Then
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, ""))
                                Else
                                    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, strLaborRate))
                                End If

                            End If
                        End If

                        ' ''Bill to
                        'If PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID) <> Nothing AndAlso PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID).ToUpper() = "AGENT" Then

                        'End If
                        Dim intCstmrID As Integer = CInt(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID))
                        Dim intEquipType As Integer = CInt(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_TYP_ID))
                        Dim dtTariffCode As New DataTable
                        ' dtTariffCode = objEstimate.GetTariffCodeTable(intAgentID, intCstmrID, intEquipType, intDepotID)
                        If dtTariffCode.Rows.Count > 0 Then
                            sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnTariffCodeID, dtTariffCode.Rows(0).Item(0).ToString))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnTariffCodeID, ""))
                        End If

                        If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.UNT_CD) Is Nothing Then
                            sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpUnit, "", ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpUnit, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.UNT_ID), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.UNT_CD)))
                        End If
                        If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.MSR_CD) Is Nothing Then
                            sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpMeasure, "", ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpMeasure, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.MSR_ID), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.MSR_CD)))
                        End If
                        If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRV_ONH_LCTN_DT) Is Nothing Then
                            sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datPreviousONH, ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datPreviousONH, CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRV_ONH_LCTN_DT)).ToString("dd-MMM-yyyy").ToUpper))
                        End If
                        If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CNSGNE) Is Nothing Then
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtConsignee, ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtConsignee, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CNSGNE)))
                        End If
                        If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRT_CD) Is Nothing Then
                            sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpPrevONHLocation, "", ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpPrevONHLocation, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRV_ONH_LCTN), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRT_CD)))
                        End If
                        If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.YRD_LCTN) Is Nothing Then
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtYardLocation, ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtYardLocation, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.YRD_LCTN)))
                        End If
                        'If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_STTS_CD) Is Nothing Then
                        '    sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, "", ""))
                        'Else
                        '    sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, 1, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_STTS_CD)))
                        'End If
                        sbRepairEstimate.Append("hideDiv('divSurvey');hideDiv('divlineDetail');")
                    Else
                        Dim objEstimate As New RepairEstimate
                        Dim ActivityName As String = Server.UrlDecode(objCommon.HttpContextGetQueryString("activityname")).ToString
                        If ActivityName.ToUpper() = "REPAIR APPROVAL" Then
                            sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, "10", "AUR"))
                            If PageSubmitPane.pub_GetPageAttribute("Approval Date") <> Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datApprovalDate, CDate(PageSubmitPane.pub_GetPageAttribute("Approval Date")).ToString("dd-MMM-yyyy").ToUpper))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datApprovalDate, Now.ToString("dd-MMM-yyyy").ToUpper))
                            End If
                            If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRTY_APPRVL_RF) Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtPartyRef, ""))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtPartyRef, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRTY_APPRVL_RF).ToString))
                            End If
                            If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.OWNR_APPRVL_RF) <> Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtApprovalRef, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.OWNR_APPRVL_RF)))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtApprovalRef, ""))
                            End If
                        ElseIf ActivityName.ToUpper = "REPAIR ESTIMATE" Then
                            dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity(REPAIR_ESTIMATE_CREATION, True, intDepotID)
                            If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                                blnShowEqStatus = True
                            End If
                            If blnShowEqStatus Then
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID), dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS)))
                            End If
                        ElseIf PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME).ToString = SURVEY_COMPLETION Then
                            If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ACTVTY_DT) Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datSurveyDate, DateTime.Now.ToString("dd-MMM-yyyy").ToUpper))
                            Else
                                If CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ACTVTY_DT)) = sqlDbnull Then
                                    sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datSurveyDate, DateTime.Now.ToString("dd-MMM-yyyy").ToUpper))
                                Else
                                    sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datSurveyDate, CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ACTVTY_DT)).ToString("dd-MMM-yyyy").ToUpper))
                                End If
                            End If
                            If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.SRVYR_NM) Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtSurveyorName, ""))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtSurveyorName, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.SRVYR_NM).ToString))
                            End If
                            dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity(SURVEY_COMPLETION, True, intDepotID)
                            If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                                blnShowEqStatus = True
                            End If
                            If blnShowEqStatus Then
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID), dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS)))
                            End If
                        End If

                        Dim strLaborRate As String = ""
                        strLaborRate = objRepairEstimate.pub_GetLaborRateperHourByCustomerID(CInt(objCommon.GetHeadQuarterID()), CLng(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID)))
                        If strLaborRate Is Nothing Then
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, strLaborRate))
                        End If
                        If bv_strMode = MODE_EDIT Then

                            If Not PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CRT_OF_CLNLNSS_BT) Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetCheckboxValuesJSO(chkCertofCleanlinessBit, CBool(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CRT_OF_CLNLNSS_BT))))
                            End If
                        End If
                    End If
                    If Not PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME) Is Nothing Then
                        sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnRepairEstimate, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME).ToString))
                        sbRepairEstimate.Append("pMode='" & PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME).ToString & "' ; toggleApprovalSurveyDiv('" + bv_strMode + "'); ")
                        sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnRepairEstimate, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME).ToString))
                    Else
                        sbRepairEstimate.Append("pMode=''; toggleApprovalSurveyDiv('" + bv_strMode + "'); ")
                        sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnRepairEstimate, ""))
                    End If
                End If

                'If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.SRVYR_NM) Is Nothing Then
                '    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtSurveyorName, ""))
                'Else
                '    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtSurveyorName, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.SRVYR_NM).ToString))
                'End If
                'If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.OWNR_APPRVL_RF) Is Nothing Then
                '    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtApprovalRef, ""))
                'Else
                '    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtApprovalRef, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.OWNR_APPRVL_RF).ToString))
                'End If
                'If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRTY_APPRVL_RF) Is Nothing Then
                '    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtPartyRef, ""))
                'Else
                '    sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtPartyRef, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRTY_APPRVL_RF).ToString))
                'End If
                'FOR GETTING DATA FROM EQUIPMENT INFORMATION
                dsRepairEstimate = objRepairEstimate.pub_GetEquipmentInformationByEqpmntNo(intDepotID, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_NO).ToString)
                dtEqipmentInformation = dsRepairEstimate.Tables(RepairEstimateData._V_EQUIPMENT_INFORMATION).Copy()
                If dtEqipmentInformation.Rows.Count > 0 AndAlso bln_061Key Then
                    If str_061KeyValue.ToLower = "false" Then
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT).ToString = "" Then
                            sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datLastTestDate, ""))
                        Else
                            If CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT)) = sqlDbnull Then
                                sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datLastTestDate, ""))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datLastTestDate, CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_DT)).ToString("dd-MMM-yyyy").ToUpper))
                            End If
                        End If
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT).ToString = "" Then
                            sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datNextTestDate, ""))
                        Else
                            If CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT)) = sqlDbnull Then
                                sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datNextTestDate, ""))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datNextTestDate, CDate(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_DT)).ToString("dd-MMM-yyyy").ToUpper))
                            End If
                        End If
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_TYP_ID) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_TYP_ID).ToString = "" Then
                            sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpLastTestType, "", ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpLastTestType, dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_TYP_ID), dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_TST_TYP_CD)))
                        End If
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_SRVYR_NM) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_SRVYR_NM).ToString = "" Then
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLastSurveyor, ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtLastSurveyor, dtEqipmentInformation.Rows(0).Item(RepairEstimateData.LST_SRVYR_NM).ToString))
                        End If
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_TYP_ID) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_TYP_ID).ToString = "" Then
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtNextTest, ""))
                            sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnTestTypeId, ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtNextTest, dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_TYP_CD)))
                            sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnTestTypeId, CStr(dtEqipmentInformation.Rows(0).Item(RepairEstimateData.NXT_TST_TYP_ID))))
                        End If
                        If dtEqipmentInformation.Rows(0).Item(RepairEstimateData.VLDTY_PRD_TST_YRS) Is Nothing Or dtEqipmentInformation.Rows(0).Item(RepairEstimateData.VLDTY_PRD_TST_YRS).ToString = "" Then
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtValidityYear, ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtValidityYear, dtEqipmentInformation.Rows(0).Item(RepairEstimateData.VLDTY_PRD_TST_YRS)))
                        End If
                    End If
                End If
                If bln_061Key Then
                    If str_061KeyValue.ToLower = "false" Then
                        If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.INVCNG_PRTY_ID) Is Nothing Then
                            sbRepairEstimate.Append("bindResponsility('0');")
                        Else
                            sbRepairEstimate.Append("bindResponsility('" & PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.INVCNG_PRTY_ID).ToString & "');")
                        End If
                    End If
                End If


                Dim strActivityName As String = Server.UrlDecode(objCommon.HttpContextGetQueryString("activityname")).ToString

                If strActivityName.ToUpper() = "REPAIR ESTIMATE" Then
                    sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpEquipStatusGWS, 4, "D"))
                End If


                If (strActivityName = REPAIR_ESTIMATE_CREATION Or strActivityName = REPAIR_APPROVAL Or strActivityName = SURVEY_COMPLETION) AndAlso bv_strMode = MODE_NEW Then
                    sbRepairEstimate.Append("bindLineDetail('" + bv_strMode + "','','');bindSummaryDetail('" + bv_strMode + "');setEstimationNo('" + bv_strMode + "');")
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnMode, MODE_CREATE))
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnMode, MODE_REVISE))
                    ' rbtnCustomerBit.Enabled = False
                Else
                    sbRepairEstimate.Append("setRepairEstimationNo('" + bv_strMode + "');")
                End If

                If strActivityName = SURVEY_COMPLETION And bv_strMode = MODE_EDIT Then
                    sbRepairEstimate.Append("bindLineDetail('" + bv_strMode + "','','');bindSummaryDetail('" + bv_strMode + "');")
                End If
                If strActivityName = REPAIR_APPROVAL And bv_strMode = MODE_NEW Then
                    If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ESTMTN_TTL_NC) Is Nothing Then
                        sbRepairEstimate.Append("setText(el('hypAppCost'),'0.00');")
                    Else
                        sbRepairEstimate.Append(String.Concat("setText(el('hypAppCost'),'" + PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ESTMTN_TTL_NC) + "');"))
                    End If
                    If bln_065Key Then
                        If str_065KeyValue.ToLower = "true" Then
                            sbRepairEstimate.Append("setText(el('hypAppCost'),'0.00');")
                            If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.UNT_CD) Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpUnit, "", ""))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpUnit, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.UNT_ID), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.UNT_CD)))
                            End If
                            If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.MSR_CD) Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpMeasure, "", ""))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpMeasure, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.MSR_ID), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.MSR_CD)))
                            End If
                            'If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRV_ONH_LCTN_DT) Is Nothing Then
                            '    sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datPreviousONH, ""))
                            'Else
                            '    sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datPreviousONH, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRV_ONH_LCTN_DT)))
                            'End If
                            If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CNSGNE) Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtConsignee, ""))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtConsignee, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CNSGNE)))
                            End If
                            If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRT_CD) Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpPrevONHLocation, "", ""))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpPrevONHLocation, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRV_ONH_LCTN), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PRT_CD)))
                            End If
                            If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.YRD_LCTN) Is Nothing Then
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtYardLocation, ""))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetTextValuesJSO(txtYardLocation, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.YRD_LCTN)))
                            End If
                        End If
                    End If
                Else
                    If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.APPRVL_AMNT_NC) Is Nothing Then
                        sbRepairEstimate.Append("setText(el('hypAppCost'),'0.00');")
                    Else
                        sbRepairEstimate.Append(String.Concat("setText(el('hypAppCost'),'" + PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.APPRVL_AMNT_NC) + "');"))
                    End If
                End If

                If strActivityName = REPAIR_ESTIMATE_CREATION And bv_strMode = MODE_NEW Then
                    If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_ESTMT_DT) Is Nothing Then
                        sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datEstimationDate, Now.ToString("dd-MMM-yyyy").ToUpper))
                    Else
                        If CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_ESTMT_DT)) = sqlDbnull Then
                            sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datEstimationDate, ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datEstimationDate, CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_ESTMT_DT)).ToString("dd-MMM-yyyy").ToUpper))
                        End If
                    End If
                Else
                    If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_ESTMT_DT) Is Nothing Then
                        sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datEstimationDate, Now.ToString("dd-MMM-yyyy").ToUpper))
                    Else
                        If CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_ESTMT_DT)) = sqlDbnull Then
                            sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datEstimationDate, ""))
                        Else
                            sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datEstimationDate, CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.RPR_ESTMT_DT)).ToString("dd-MMM-yyyy").ToUpper))
                        End If
                    End If
                    If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ORGNL_ESTMTN_AMNT_NC) Is Nothing Then
                        sbRepairEstimate.Append("setText(el('hypOrginalCost'),'0.00');")
                    Else
                        sbRepairEstimate.Append(String.Concat("setText(el('hypOrginalCost'),'" + PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ORGNL_ESTMTN_AMNT_NC) + "');"))
                    End If
                End If

                CacheData(REPAIR_ESTIMATEMODE, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME).ToString)

                If Not PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME) Is Nothing Then
                    If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME).ToString = REPAIR_APPROVAL Then

                        'If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ACTVTY_DT) Is Nothing Then
                        '    sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datApprovalDate, DateTime.Now.ToString("dd-MMM-yyyy").ToUpper))
                        'Else
                        '    If CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ACTVTY_DT)) = sqlDbnull Then
                        '        sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datApprovalDate, DateTime.Now.ToString("dd-MMM-yyyy").ToUpper))
                        '    Else
                        '        sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datApprovalDate, CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ACTVTY_DT)).ToString("dd-MMM-yyyy").ToUpper))
                        '    End If
                        'End If
                        dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity(REPAIR_APPROVAL, True, intDepotID)
                    ElseIf PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME).ToString = REPAIR_ESTIMATE_CREATION Then
                        dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity(REPAIR_ESTIMATE_CREATION, True, intDepotID)

                        If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                            sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEquipmentStatusId, CStr(dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID))))
                        End If


                    ElseIf PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME).ToString = SURVEY_COMPLETION Then
                        'If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ACTVTY_DT) Is Nothing Then
                        '    sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datSurveyDate, DateTime.Now.ToString("dd-MMM-yyyy").ToUpper))
                        'Else
                        '    If CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ACTVTY_DT)) = sqlDbnull Then
                        '        sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datSurveyDate, DateTime.Now.ToString("dd-MMM-yyyy").ToUpper))
                        '    Else
                        '        sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datSurveyDate, CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ACTVTY_DT)).ToString("dd-MMM-yyyy").ToUpper))
                        '    End If
                        'End If
                        dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity(SURVEY_COMPLETION, True, intDepotID)
                    End If
                    If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                        blnShowEqStatus = True
                    End If
                    If blnShowEqStatus Then
                        'sbRepairEstimate.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID), dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS)))
                    End If
                End If
                'If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID) Is Nothing Then
                '    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnToCurrencyCD, ""))
                'Else
                Dim blnFlag As Boolean = False
                Dim dtCurrency As DataTable
                If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                    intDepotID = CInt(objCommon.GetHeadQuarterID())
                    'intDepotID = CInt(objCommon.GetDepotID())
                End If
                dtCurrency = objRepairEstimate.Pub_GetCurrencyExchangeRateByDptId(intDepotID, CLng(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.CSTMR_ID))).Tables(RepairEstimateData._V_REPAIR_ESTIMATE_REPORT)
                If dtCurrency.Rows.Count > 0 Then
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotCurrencyCD, dtCurrency.Rows(0).Item(RepairEstimateData.DPT_CRRNCY_CD).ToString))

                End If

                If PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID) <> Nothing AndAlso PageSubmitPane.pub_GetPageAttribute(GateinData.BLL_ID).ToUpper() = "AGENT" Then
                    Dim dtCurrcy As New DataTable
                    Dim dtExchange As New DataTable

                    dtCurrcy = objRepairEstimate.GetCurrByAgntId(intAgentID, intDepotID)
                    dtExchange = objRepairEstimate.Pub_GetAgentCurrencyExchangeRateByDptId(intDepotID, intAgentID)

                    'For currency Code
                    If dtCurrcy.Rows.Count > 0 Then
                        sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnToCurrencyCD, dtCurrcy.Rows(0).Item(1).ToString))
                    End If

                    'For Exchage Rate
                    If dtExchange.Rows.Count > 0 Then
                        sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnExchangeRate, dtExchange.Rows(0).Item(RepairEstimateData.EXCHNG_RT_PR_UNT_NC).ToString()))
                    End If

                Else
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnToCurrencyCD, dtCurrency.Rows(0).Item(RepairEstimateData.CSTMR_CRRNCY_CD).ToString))
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnExchangeRate, dtCurrency.Rows(0).Item(RepairEstimateData.EXCHNG_RT_PR_UNT_NC).ToString))
                End If

                'End If




                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ORGNL_ESTMN_DT) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datOrginalEstimateDate, ""))
                Else
                    If CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ORGNL_ESTMN_DT)) = sqlDbnull Then
                        sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datOrginalEstimateDate, ""))
                    Else
                        sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datOrginalEstimateDate, CDate(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.ORGNL_ESTMN_DT)).ToString("dd-MMM-yyyy").ToUpper))
                    End If
                End If
                Dim dtEquipmentDetail As New DataTable
                dtEquipmentDetail = objRepairEstimate.pub_GetGetActivityStatusByEqpmntNo(CommonWeb.iInt(objCommon.GetDepotID), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_NO).ToString, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.GI_TRNSCTN_NO).ToString).Tables(RepairEstimateData._V_ACTIVITY_STATUS)
                If dtEquipmentDetail.Rows.Count > 0 Then
                    If dtEquipmentDetail.Rows(0).Item(RepairEstimateData.GTN_DT) Is Nothing Then
                        sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datGateInDate, ""))
                    Else
                        sbRepairEstimate.Append(CommonWeb.GetTextDateValuesJSO(datGateInDate, CDate(dtEquipmentDetail.Rows(0).Item(RepairEstimateData.GTN_DT)).ToString("dd-MMM-yyyy").ToUpper))
                    End If
                    If bln_061Key Then
                        If str_061KeyValue.ToLower = "false" Then
                            If Not IsDBNull(dtEquipmentDetail.Rows(0).Item(RepairEstimateData.CERT_GNRTD_FLG)) Then
                                sbRepairEstimate.Append(CommonWeb.GetCheckboxValuesJSO(chkCertofCleanlinessBit, CBool(dtEquipmentDetail.Rows(0).Item(RepairEstimateData.CERT_GNRTD_FLG))))
                            Else
                                sbRepairEstimate.Append(CommonWeb.GetCheckboxValuesJSO(chkCertofCleanlinessBit, False))
                            End If
                        End If
                    End If

                End If
                dsRepairEstimate.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Merge(dtEquipmentDetail)
                CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
                Dim strUserName As String = String.Empty
                Dim strIpError As String = String.Empty
                Dim strActivityLockName As String = String.Empty
                strUserName = pvt_GetLockData(CStr(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_NO)), strIpError, strActivityLockName)
                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnLockUserName, strUserName))
                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnIpError, strIpError))
                sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnLockActivityName, strActivityLockName))

                'For Attachment
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.GI_TRNSCTN_NO) Is Nothing Then
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnGITransaction, ""))
                Else
                    sbRepairEstimate.Append(CommonWeb.GetHiddenTextValuesJSO(hdnGITransaction, PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.GI_TRNSCTN_NO)))
                End If
            End If

            Dim objRepairEstimates As New RepairEstimates
            Dim dtCleaning As DataTable
            dtCleaning = objRepairEstimates.GetAdditionalCleaningBit(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_NO), PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.GI_TRNSCTN_NO), CInt(objCommon.GetDepotID()))
            Dim addClngBtFromDb As String = "NOT AVAILABLE"
            Dim addClngFlgFromDb As String = "NOT AVAILABLE"
            If dtCleaning.Rows.Count > 0 Then
                addClngBtFromDb = CStr(dtCleaning.Rows(0).Item(ChangeOfStatusData.ADDTNL_CLNNG_BT))
                addClngFlgFromDb = CStr(dtCleaning.Rows(0).Item(CleaningData.ADDTNL_CLNNG_FLG))
            End If
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                          String.Concat("EQPMNT_NO : ", PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_NO), _
                                                        "    Page Name/Mode : ", PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.PAGENAME).ToString, "/", bv_strMode, _
                                          "  GI_TRNSCTN_NO : ", PageSubmitPane.pub_GetPageAttribute(CleaningData.GI_TRNSCTN_NO), _
                                          "  ADDTNL_CLNNG_BT : ", PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_BT), _
                                          "  ADDTNL_CLNNG_BT FROM DB : ", addClngBtFromDb, _
                                          "   ADDTNL_CLNNG_FLG : ", PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_FLG), _
                                          "   ADDTNL_CLNNG_FLG FROM DB : ", addClngFlgFromDb))
            pub_SetCallbackReturnValue("Message", sbRepairEstimate.ToString())
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetExchangeRate"
    Private Sub pvt_GetExchangeRate(ByVal bv_strFromCurrencyId As String, ByVal bv_strToCurrencyId As String)
        Try
            Dim objRepairEstimate As New RepairEstimate
            Dim objCommon As New CommonData()
            dsRepairEstimate = objRepairEstimate.pub_GetExchangeRateWithEffectivedate(CLng(bv_strFromCurrencyId), CLng(bv_strToCurrencyId), DateTime.Now(), CInt(objCommon.GetDepotID()))
            If dsRepairEstimate.Tables(RepairEstimateData._EXCHANGE_RATE).Rows.Count > 0 Then
                Dim strExchangeRate As String
                strExchangeRate = dsRepairEstimate.Tables(RepairEstimateData._EXCHANGE_RATE).Rows(0).Item(RepairEstimateData.EXCHNG_RT_PR_UNT_NC).ToString
                pub_SetCallbackStatus(True)
                pub_SetCallbackReturnValue("exchangeRate", strExchangeRate)
            Else
                pub_SetCallbackStatus(False)
            End If
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateRepairEstimate"
    Private Sub pvt_CreateRepairEstimate(ByVal bv_strCustomerID As String, _
                                         ByVal bv_strCustomerCode As String, _
                                         ByVal bv_strEstimateDate As String, _
                                         ByVal bv_strOrginalEstimateDate As String, _
                                         ByVal bv_strStatusID As String, _
                                         ByVal bv_strStatusCode As String, _
                                         ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_strEIRNo As String, _
                                         ByVal bv_strLastTestDate As String, _
                                         ByVal bv_strLastTestTypeID As String, _
                                         ByVal bv_strLastTestTypeCode As String, _
                                         ByVal bv_strValidityYear As String, _
                                         ByVal bv_strNextTestDate As String, _
                                         ByVal bv_strLastSurveyor As String, _
                                         ByVal bv_strNextTestTypeID As String, _
                                         ByVal bv_strNextTestTypeCode As String, _
                                         ByVal bv_strRepairTypeID As String, _
                                         ByVal bv_strRepairTypeCode As String, _
                                         ByVal bv_strCertOfCleanlinessBit As String, _
                                         ByVal bv_strInvoicingPartyID As String, _
                                         ByVal bv_strInvoicingPartyCode As String, _
                                         ByVal bv_strLaborRate As String, _
                                         ByVal bv_strApprovalDate As String, _
                                         ByVal bv_strApprovalRef As String, _
                                         ByVal bv_strSurveyDate As String, _
                                         ByVal bv_strSurveyName As String, _
                                         ByVal bv_strWFData As String, _
                                         ByVal bv_strMode As String, _
                                         ByVal bv_strEstimateId As String, _
                                         ByRef bv_strRevisionNo As String, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strEstimationNo As String, _
                                         ByVal bv_strCustomerEstimatedCost As String, _
                                         ByVal bv_strCustomerApprovedCost As String, _
                                         ByVal bv_strPartyApprovalRef As String, _
                                         ByVal bv_intActivityId As Integer)

        Try
            Dim objcommon As New CommonData
            Dim objCommonUI As New CommonUI
            Dim str_037KeyValue As String = String.Empty
            Dim bln_037KeyValue As Boolean = False
            Dim strActivitySubmit As String = String.Empty
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            Dim PageMode As String = RetrieveData(REPAIR_ESTIMATEMODE).ToString


            If Not dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("At least one line detail is mandatory for an Estimate.")
                Exit Sub
            End If
            Dim blnMode As Boolean = False
            Dim objRepairEstimate As New RepairEstimate
            Dim lngCreated As Long
            Dim bv_strEIRNumber As String = ""
            Dim datGateinDate As DateTime
            If dsRepairEstimate.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows.Count > 0 Then
                datGateinDate = CDate(dsRepairEstimate.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.GTN_DT))
            End If
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            str_032KeyValue = objCommonConfig.pub_GetConfigSingleValue("032", intDepotID)
            bln_032EqType_Key = objCommonConfig.IsKeyExists
            If bln_032EqType_Key And bv_strRepairTypeID = Nothing Then
                bv_strRepairTypeID = objcommon.GetEnumID("REPAIR TYPE", str_032KeyValue)
            End If
            'REPAIR FLOW SETTING: 037
            'CR-001 (AWE_TO_AWP)
            str_037KeyValue = objcommon.GetConfigSetting("037", bln_037KeyValue)
            If bln_037KeyValue = False Then
                str_037KeyValue = String.Empty
            End If

            'GWS
            Dim str_063GWS As String
            Dim bln_063GWSKey As Boolean
            str_057GWS = objCommonConfig.pub_GetConfigSingleValue("057", intDepotID)
            bln_057GWSActive_Key = objCommonConfig.IsKeyExists
            bln_063GWSKey = objCommonConfig.IsKeyExists
            If bln_063GWSKey Then
                str_063GWS = objCommonConfig.pub_GetConfigSingleValue("063", intDepotID)
            Else
                str_063GWS = "false"
            End If
            lngCreated = objRepairEstimate.pub_CreateRepairEstimate(CommonWeb.iLng(bv_strCustomerID),
                                                                    bv_strCustomerCode, _
                                                                    CommonWeb.iDat(bv_strEstimateDate), _
                                                                    CommonWeb.iDat(bv_strOrginalEstimateDate), _
                                                                    datGateinDate, _
                                                                    bv_strEquipmentNo, _
                                                                    CommonWeb.iLng(bv_strStatusID), _
                                                                    bv_strStatusCode, _
                                                                    bv_strEIRNo, _
                                                                    CommonWeb.iDat(bv_strLastTestDate), _
                                                                    CommonWeb.iLng(bv_strLastTestTypeID), _
                                                                    bv_strLastTestTypeCode, _
                                                                    bv_strValidityYear, _
                                                                    CommonWeb.iDat(bv_strNextTestDate), _
                                                                    bv_strLastSurveyor, _
                                                                    CommonWeb.iLng(bv_strNextTestTypeID), _
                                                                    bv_strNextTestTypeCode, _
                                                                    CommonWeb.iLng(bv_strRepairTypeID), _
                                                                    bv_strRepairTypeCode, _
                                                                    CBool(bv_strCertOfCleanlinessBit), _
                                                                    CommonWeb.iLng(bv_strInvoicingPartyID), _
                                                                    bv_strInvoicingPartyCode, _
                                                                    CommonWeb.iDec(bv_strLaborRate), _
                                                                    CommonWeb.iDat(bv_strApprovalDate), _
                                                                    bv_strApprovalRef, _
                                                                    CommonWeb.iDat(bv_strSurveyDate), _
                                                                    bv_strSurveyName, _
                                                                    CommonWeb.iInt(objcommon.GetDepotID()), _
                                                                    bv_strWFData, _
                                                                    bv_strMode, _
                                                                    bv_strEstimateId, _
                                                                    bv_strRevisionNo, _
                                                                    bv_strEstimationNo, _
                                                                    bv_strRemarks, _
                                                                    dsRepairEstimate, _
                                                                    bv_strEIRNumber, _
                                                                    PageMode, _
                                                                    bv_strApprovalRef, _
                                                                    CDec(bv_strCustomerEstimatedCost), _
                                                                    CDec(bv_strCustomerApprovedCost), _
                                                                    strModifiedby, _
                                                                    CDate(datModifiedDate), _
                                                                    strModifiedby, _
                                                                    CDate(datModifiedDate), _
                                                                    str_037KeyValue, _
                                                                    bv_strPartyApprovalRef, _
                                                                    strActivitySubmit, _
                                                                    bv_intActivityId, _
                                                                    str_057GWS, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    str_063GWS, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    "False", _
                                                                    Nothing, _
                                                                    Nothing)

            pub_SetCallbackReturnValue("EstimateId", lngCreated.ToString)
            pub_SetCallbackReturnValue("RepairEstimationNo", bv_strEIRNumber)
            pub_SetCallbackReturnValue("RevisionNo", bv_strRevisionNo)
            dsRepairEstimate.AcceptChanges()
            'Dim check As DataSet = dsRepairEstimate
            'Dim strserialize As String = JsonConvert.SerializeObject(check)
            'Response.Write(strserialize)

            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", String.Concat(PageMode, " : ", bv_strEIRNumber, strMSGINSERT))
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateRepairEstimate"
    Private Sub pvt_UpdateRepairEstimate(ByVal bv_strRepairEstimateId As String, _
                                         ByVal bv_strCustomerID As String, _
                                         ByVal bv_strCustomerCode As String, _
                                         ByVal bv_strEstimateDate As String, _
                                         ByVal bv_strOrginalEstimateDate As String, _
                                         ByVal bv_strStatusID As String, _
                                         ByVal bv_strStatusCode As String, _
                                         ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_strEIRNo As String, _
                                         ByVal bv_strGateInTransaction As String, _
                                         ByVal bv_strLastTestDate As String, _
                                         ByVal bv_strLastTestTypeID As String, _
                                         ByVal bv_strLastTestTypeCode As String, _
                                         ByVal bv_strValidityYear As String, _
                                         ByVal bv_strNextTestDate As String, _
                                         ByVal bv_strLastSurveyor As String, _
                                         ByVal bv_strNextTestTypeID As String, _
                                         ByVal bv_strNextTestTypeCode As String, _
                                         ByVal bv_strRepairTypeID As String, _
                                         ByVal bv_strRepairTypeCode As String, _
                                         ByVal bv_strCertOfCleanlinessBit As String, _
                                         ByVal bv_strInvoicingPartyID As String, _
                                         ByVal bv_strInvoicingPartyCode As String, _
                                         ByVal bv_strLaborRate As String, _
                                         ByVal bv_strApprovalAmount As String, _
                                         ByVal bv_strApprovalDate As String, _
                                         ByVal bv_strApprovalRef As String, _
                                         ByVal bv_strSurveyDate As String, _
                                         ByVal bv_strSurveyName As String, _
                                         ByVal bv_strWFData As String, _
                                         ByVal bv_strMode As String, _
                                         ByVal bv_strEstimateId As String, _
                                         ByRef bv_strRevisionNo As String, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strEstimationNo As String, _
                                         ByVal bv_strCustomerEstimatedCost As String, _
                                         ByVal bv_strCustomerApprovedCost As String, _
                                         ByVal bv_strPartyApprovalRef As String, _
                                         ByVal bv_intActivityId As Integer)
        Try
            Dim objcommon As New CommonData
            Dim str_037KeyValue As String = String.Empty
            Dim bln_037KeyValue As Boolean = False
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            Dim blnMode As Boolean = False
            Dim strActivitySubmit As String = String.Empty
            Dim objRepairEstimate As New RepairEstimate
            Dim PageMode As String = RetrieveData(REPAIR_ESTIMATEMODE).ToString
            If Not dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("At least one line detail is mandatory for an Estimate.")
                Exit Sub
            End If
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            str_057GWS = objCommonConfig.pub_GetConfigSingleValue("057", intDepotID)
            bln_057GWSActive_Key = objCommonConfig.IsKeyExists
            'REPAIR FLOW SETTING: 037
            'CR-001 (AWE_TO_AWP)
            str_037KeyValue = objcommon.GetConfigSetting("037", bln_037KeyValue)
            If bln_037KeyValue = False Then
                str_037KeyValue = String.Empty
            End If
            Dim lngCreated As Long
            lngCreated = objRepairEstimate.pub_ModifyRepairEstimate(CommonWeb.iLng(bv_strRepairEstimateId), _
                                                                    CommonWeb.iLng(bv_strCustomerID),
                                                                    bv_strCustomerCode, _
                                                                    CommonWeb.iDat(bv_strEstimateDate), _
                                                                    CommonWeb.iDat(bv_strOrginalEstimateDate), _
                                                                    bv_strEquipmentNo, _
                                                                    CommonWeb.iLng(bv_strStatusID), _
                                                                    bv_strStatusCode, _
                                                                    bv_strEIRNo, _
                                                                    bv_strGateInTransaction, _
                                                                    CommonWeb.iDat(bv_strLastTestDate), _
                                                                    CommonWeb.iLng(bv_strLastTestTypeID), _
                                                                    bv_strLastTestTypeCode, _
                                                                    bv_strValidityYear, _
                                                                    CommonWeb.iDat(bv_strNextTestDate), _
                                                                    bv_strLastSurveyor, _
                                                                    CommonWeb.iLng(bv_strNextTestTypeID), _
                                                                    bv_strNextTestTypeCode, _
                                                                    CommonWeb.iLng(bv_strRepairTypeID), _
                                                                    bv_strRepairTypeCode, _
                                                                    CBool(bv_strCertOfCleanlinessBit), _
                                                                    CommonWeb.iLng(bv_strInvoicingPartyID), _
                                                                    bv_strInvoicingPartyCode, _
                                                                    CommonWeb.iDec(bv_strLaborRate), _
                                                                    CommonWeb.iDec(bv_strApprovalAmount), _
                                                                    CommonWeb.iDat(bv_strApprovalDate), _
                                                                    bv_strApprovalRef, _
                                                                    bv_strPartyApprovalRef, _
                                                                    CommonWeb.iDat(bv_strSurveyDate), _
                                                                    bv_strSurveyName, _
                                                                    PageMode, _
                                                                    CommonWeb.iInt(objcommon.GetDepotID()), _
                                                                    bv_strWFData, _
                                                                    bv_strMode, _
                                                                    bv_strEstimateId, _
                                                                    bv_strRevisionNo, _
                                                                    bv_strRemarks, _
                                                                    bv_strEstimationNo, _
                                                                    CDec(bv_strCustomerEstimatedCost), _
                                                                    CDec(bv_strCustomerApprovedCost), _
                                                                    strModifiedby, _
                                                                    CDate(datModifiedDate), _
                                                                    str_037KeyValue, _
                                                                    dsRepairEstimate, _
                                                                    strActivitySubmit, _
                                                                    bv_intActivityId, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    "False", _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    str_057GWS)

            pub_SetCallbackReturnValue("EstimateId", bv_strRepairEstimateId)
            pub_SetCallbackReturnValue("RepairEstimationNo", bv_strEstimationNo)
            pub_SetCallbackReturnValue("RevisionNo", bv_strRevisionNo)
            dsRepairEstimate.AcceptChanges()
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", String.Concat(PageMode, " : ", bv_strEstimationNo, strMSGUPDATE))
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgLineDetail"

#Region "ifgLineDetail_ClientBind"
    Protected Sub ifgLineDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgLineDetail.ClientBind
        Try
            Dim objCommon As New CommonData()
            Dim drNew As DataRow
            Dim intDepotId As Integer
            Dim dtActivityStatus As New DataTable
            Dim strPageMode As String = RetrieveData(REPAIR_ESTIMATEMODE).ToString
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            dtActivityStatus = dsRepairEstimate.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Copy()
            Dim strPageName As String = e.Parameters("PageName").ToString()
            CacheData(REPAIR_ESTIMATEMODE, strPageName)
            CacheData(PAGENAME, strPageName)
            CacheData(PAGEMODE, e.Parameters("Mode").ToString)
            intDepotId = CInt(objCommon.GetDepotID())
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotId = CInt(objCommon.GetHeadQuarterID())
            End If
            Dim decManHourRate As Decimal = 0
            If Not e.Parameters("ManHourRate").ToString().Trim() Is Nothing AndAlso Not e.Parameters("ManHourRate").ToString().Trim() Is String.Empty Then
                decManHourRate = CDec(e.Parameters("ManHourRate"))
            End If
            Dim objEstimate As New RepairEstimate
            Dim objCommondata As New CommonData
            Dim str_061KeyValue As String
            Dim bln_061KeyExist As Boolean
            str_061KeyValue = objCommondata.GetConfigSetting("061", bln_061KeyExist)
            Dim str_065KeyValue As String
            Dim bln_065KeyExist As Boolean
            str_065KeyValue = objCommondata.GetConfigSetting("065", bln_065KeyExist)
            If bln_061KeyExist Then
                If str_061KeyValue.ToLower = "true" Then
                    ifgLineDetail.ActionButtons(0).Visible = False
                Else
                    ifgLineDetail.ActionButtons(0).Visible = True
                End If
            End If
            If Not e.Parameters("Mode") Is Nothing Then
                Select Case e.Parameters("Mode").ToString
                    Case MODE_EDIT
                        Dim approvAmt As Decimal
                        If Not e.Parameters("EstimateId") Is Nothing And Not e.Parameters("EstimateId").ToString = "" Then
                            LineDetailBind(e.Parameters("EstimateId").ToString, str_061KeyValue)
                        End If
                        If strPageMode = SURVEY_COMPLETION Then
                            btnMassApplyInput.Visible = False
                        End If
                        If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count = 0 Then
                            Dim drRepairEstimate As DataRow = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).NewRow()
                            drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = CommonWeb.GetNextIndex(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), RepairEstimateData.RPR_ESTMT_DTL_ID)
                            drRepairEstimate.Item(RepairEstimateData.LBR_HRS) = "0.00"
                            drRepairEstimate.Item(RepairEstimateData.LBR_RT) = decManHourRate
                            drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC) = "0.00"
                            drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC) = "0.00"
                            drRepairEstimate.Item(RepairEstimateData.TTL_CST_NC) = "0.00"
                            'drRepairEstimate.Item(RepairEstimateData.CHNG_BIT) = False
                            If bln_061KeyExist Then
                                If str_061KeyValue.ToLower <> "true" Then
                                    drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD) = "C"
                                    drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_ID) = 66
                                End If
                            End If
                            dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Add(drRepairEstimate)
                        Else
                            For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(RepairEstimateData.CHK_BT & "= 'True'")
                                Dim temp As Decimal
                                temp = CDec(drRepair.Item(RepairEstimateData.TTL_CST_NC))
                                approvAmt = approvAmt + temp
                            Next
                        End If
                        e.Parameters.Add("TotalApprovedAmount", approvAmt)
                    Case "FETCH"
                        dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
                        Dim drSelect As DataRow()
                        drSelect = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairEstimateData.ITM_ID, " is NULL AND ", RepairEstimateData.SB_ITM_ID, " is NULL AND ", RepairEstimateData.RPR_ID, " is NULL AND ", RepairEstimateData.DMG_ID, " is NULL"), "")
                        If drSelect.Length > 0 Then
                            dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Remove(drSelect(0))
                        End If
                        ' If Not e.Parameters("TariffId").ToString = "" Then
                        Dim dtTariff As New DataTable
                        If bln_061KeyExist Then
                            If str_061KeyValue.ToLower = "true" Then
                                dtTariff = objEstimate.GetTariffCodeTable(intDepotId, e.Parameters("TariffId")).Tables(RepairEstimateData._V_TARIFF_CODE_DETAIL)
                                If dtTariff.Rows.Count > 0 And bln_061KeyExist Then
                                    For Each dr As DataRow In dtTariff.Rows
                                        drNew = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).NewRow()
                                        drNew.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = CommonWeb.GetNextIndex(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), RepairEstimateData.RPR_ESTMT_DTL_ID)
                                        drNew.Item(RepairEstimateData.ITM_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_LCTN_ID)
                                        drNew.Item(RepairEstimateData.ITM_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_LCTN_CD)
                                        drNew.Item(RepairEstimateData.SB_ITM_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_CMPNT_ID)
                                        drNew.Item(RepairEstimateData.SB_ITM_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_CMPNT_CD)
                                        drNew.Item(RepairEstimateData.DMG_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_DMG_ID)
                                        drNew.Item(RepairEstimateData.DMG_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_DMG_CD)
                                        drNew.Item(RepairEstimateData.MTRL_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_ID)
                                        drNew.Item(RepairEstimateData.MTRL_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CD)
                                        drNew.Item(RepairEstimateData.RPR_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_RPR_ID)
                                        drNew.Item(RepairEstimateData.RPR_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_RPR_CD)
                                        If Not dr.Item(RepairEstimateData.TRFF_CD_DTL_MNHR) Is DBNull.Value Then
                                            drNew.Item(RepairEstimateData.LBR_HRS) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MNHR)
                                        Else
                                            drNew.Item(RepairEstimateData.LBR_HRS) = "0.00"
                                        End If
                                        drNew.Item(RepairEstimateData.CHNG_BIT) = False
                                        'drNew.Item(RepairEstimateData.RSPNSBLTY_CD) = "O"
                                        'drNew.Item(RepairEstimateData.RSPNSBLTY_ID) = 9

                                        drNew.Item(RepairEstimateData.TRFF_CD_DTL_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_ID)

                                        drNew.Item(RepairEstimateData.LBR_RT) = decManHourRate
                                        drNew.Item(RepairEstimateData.LBR_HR_CST_NC) = CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HRS)) * CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_RT))
                                        If Not dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CST) Is DBNull.Value Then
                                            drNew.Item(RepairEstimateData.MTRL_CST_NC) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CST)
                                        Else
                                            drNew.Item(RepairEstimateData.MTRL_CST_NC) = "0.00"
                                        End If

                                        drNew.Item(RepairEstimateData.TTL_CST_NC) = CommonWeb.iDec(dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CST)) + CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HR_CST_NC))
                                        drNew.Item(RepairEstimateData.DMG_RPR_DSCRPTN) = dr.Item(RepairEstimateData.TRFF_CD_DTL_RMRKS)
                                        dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Add(drNew)
                                    Next
                                End If
                            Else
                                dtTariff = objEstimate.pub_GetLineDeatilbyTariffCodeId(intDepotId, CStr(e.Parameters("TariffId")), CStr(e.Parameters("TariffGroupId"))).Tables(RepairEstimateData._V_TARIFF_CODE)
                                If dtTariff.Rows.Count > 0 Then
                                    For Each dr As DataRow In dtTariff.Rows
                                        drNew = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).NewRow()
                                        drNew.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = CommonWeb.GetNextIndex(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), RepairEstimateData.RPR_ESTMT_DTL_ID)
                                        drNew.Item(RepairEstimateData.ITM_ID) = dr.Item(RepairEstimateData.ITM_ID)
                                        drNew.Item(RepairEstimateData.ITM_CD) = dr.Item(RepairEstimateData.ITM_CD)
                                        drNew.Item(RepairEstimateData.SB_ITM_ID) = dr.Item(RepairEstimateData.SB_ITM_ID)
                                        drNew.Item(RepairEstimateData.SB_ITM_CD) = dr.Item(RepairEstimateData.SB_ITM_CD)
                                        drNew.Item(RepairEstimateData.DMG_ID) = dr.Item(RepairEstimateData.DMG_ID)
                                        drNew.Item(RepairEstimateData.DMG_CD) = dr.Item(RepairEstimateData.DMG_CD)
                                        drNew.Item(RepairEstimateData.RPR_ID) = dr.Item(RepairEstimateData.RPR_ID)
                                        drNew.Item(RepairEstimateData.RPR_CD) = dr.Item(RepairEstimateData.RPR_CD)
                                        drNew.Item(RepairEstimateData.LBR_HRS) = dr.Item(RepairEstimateData.MN_HR)
                                        drNew.Item(RepairEstimateData.LBR_RT) = decManHourRate
                                        drNew.Item(RepairEstimateData.LBR_HR_CST_NC) = CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HRS)) * CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_RT))
                                        drNew.Item(RepairEstimateData.MTRL_CST_NC) = dr.Item(RepairEstimateData.MTRL_CST)
                                        drNew.Item(RepairEstimateData.TTL_CST_NC) = CommonWeb.iDec(dr.Item(RepairEstimateData.MTRL_CST_NC)) + CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HR_CST_NC))
                                        drNew.Item(RepairEstimateData.DMG_RPR_DSCRPTN) = dr.Item(RepairEstimateData.RMRKS_VC)
                                        drNew.Item(RepairEstimateData.RSPNSBLTY_CD) = "C"
                                        drNew.Item(RepairEstimateData.RSPNSBLTY_ID) = 66
                                        dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Add(drNew)
                                    Next
                                End If
                            End If
                        End If
                        ' End If
                    Case MODE_NEW
                        dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
                        If strPageMode = SURVEY_COMPLETION Then
                            btnMassApplyInput.Visible = False
                            If Not e.Parameters("EstimateId") Is Nothing And Not e.Parameters("EstimateId").ToString = "" Then
                                LineDetailBind(e.Parameters("EstimateId").ToString, str_061KeyValue)
                            End If
                        ElseIf strPageMode = REPAIR_APPROVAL Then
                            If Not e.Parameters("EstimateId") Is Nothing And Not e.Parameters("EstimateId").ToString = "" Then
                                Dim approvAmt As Decimal
                                LineDetailBind(e.Parameters("EstimateId").ToString, str_061KeyValue)
                                If str_065KeyValue.ToLower = "true" Then
                                    For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(RepairEstimateData.CHK_BT & "= 'True'")
                                        Dim temp As Decimal
                                        temp = CDec(drRepair.Item(RepairEstimateData.TTL_CSTS_NC))
                                        approvAmt = approvAmt + temp
                                    Next
                                    e.Parameters.Add("TotalApprovedAmount", approvAmt)
                                Else
                                    For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
                                        drRepair(RepairEstimateData.CHK_BT) = "False"
                                    Next
                                End If

                                'For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
                                '    drRepair(RepairEstimateData.CHK_BT) = "True"
                                'Next
                            End If
                        ElseIf strPageMode = REPAIR_ESTIMATE_CREATION Then
                            dsRepairEstimate = New RepairEstimateDataSet
                            If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count = 0 Then
                                Dim drRepairEstimate As DataRow = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).NewRow()
                                drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = CommonWeb.GetNextIndex(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), RepairEstimateData.RPR_ESTMT_DTL_ID)
                                drRepairEstimate.Item(RepairEstimateData.LBR_HRS) = "0.00"
                                drRepairEstimate.Item(RepairEstimateData.LBR_RT) = decManHourRate
                                drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC) = "0.00"
                                drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC) = "0.00"
                                drRepairEstimate.Item(RepairEstimateData.TTL_CST_NC) = "0.00"
                                'drRepairEstimate.Item(RepairEstimateData.CHNG_BIT) = False
                                If bln_061KeyExist Then
                                    If str_061KeyValue.ToLower <> "true" Then
                                        drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD) = "C"
                                        drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_ID) = 66
                                    End If
                                End If
                                dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Add(drRepairEstimate)
                            End If
                        End If
                    Case "ReBind"
                        dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
                    Case "MassInput"
                        If bln_061KeyExist Then
                            If str_061KeyValue.ToLower <> "true" Then
                                'btnMassApplyInput.Visible = False
                                For Each dr As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
                                    dr.Item(RepairEstimateData.CHK_BT) = False
                                Next
                            End If
                        End If
                        If strPageMode = SURVEY_COMPLETION Then
                            btnMassApplyInput.Visible = False
                        End If
                    Case "Clear"
                        dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows().Clear()

                        'For Each dr As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
                        '    dr.Delete()
                        'Next

                        '  If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count = 0 Then
                        Dim drRepairEstimate As DataRow = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).NewRow()
                        drRepairEstimate.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = CommonWeb.GetNextIndex(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), RepairEstimateData.RPR_ESTMT_DTL_ID)
                        drRepairEstimate.Item(RepairEstimateData.LBR_HRS) = "0.00"
                        drRepairEstimate.Item(RepairEstimateData.LBR_RT) = decManHourRate
                        drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC) = "0.00"
                        drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC) = "0.00"
                        drRepairEstimate.Item(RepairEstimateData.TTL_CST_NC) = "0.00"
                        dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Add(drRepairEstimate)
                        ' End If
                End Select
            End If
            Dim decTotalCost As Decimal
            If Not IsDBNull(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", "")) Then
                decTotalCost = CDec(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
            Else
                decTotalCost = 0
            End If
            e.Parameters.Add("TotalCost", decTotalCost)
            e.DataSource = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL)
            dsRepairEstimate.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Merge(dtActivityStatus)
            CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#Region "LineDetailBind"

    Private Sub LineDetailBind(ByVal bv_strEstimateId As String, ByVal bv_str061KeyValue As String)
        Try
            Dim objEstimate As New RepairEstimate
            dsRepairEstimate = objEstimate.pub_GetRepairEstimateDetailByRepairEstimationId(CLng(bv_strEstimateId))
            If bv_str061KeyValue.ToLower = "true" Then

            Else
                For i = 0 To 1
                    Dim drRepairEstimate As DataRow = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).NewRow()
                    drRepairEstimate.Item(RepairEstimateData.SMMRY_ID) = i + 1
                    drRepairEstimate.Item(RepairEstimateData.MN_HR_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.LBR_RT_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.MN_HR_RT_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.MTRL_CST_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.TTL_CST_SMMRY) = 0.0
                    dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows.Add(drRepairEstimate)
                Next
                For Each drRepairEstimate As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
                    If CLng(drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_ID)) = 66 Then
                        drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD) = "C"
                    ElseIf CLng(drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_ID)) = 67 Then
                        drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD) = "I"
                    End If

                    pvt_CalculateSummaryDetail(drRepairEstimate.Item(RepairEstimateData.LBR_HRS).ToString, _
                                               drRepairEstimate.Item(RepairEstimateData.LBR_RT).ToString, _
                                               drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC).ToString, _
                                               drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC).ToString, _
                                               drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD).ToString, _
                                               dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL))
                Next
            End If

            CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#End Region

#Region "ifgLineDetail_RowDataBound"
    Protected Sub ifgLineDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgLineDetail.RowDataBound
        Try
            Dim objCommondata As New CommonData
            Dim str_061KeyValue As String
            Dim bln_061KeyExist As Boolean
            Dim bln_063Key As Boolean
            Dim str_063KeyValue As String
            Dim bln_065KeyExist As Boolean
            Dim str_065KeyValue As String
            str_061KeyValue = objCommondata.GetConfigSetting("061", bln_061KeyExist)
            str_063KeyValue = objCommondata.GetConfigSetting("063", bln_063Key)
            str_044KeyValue = objCommondata.GetConfigSetting("044", bln_044KeyExist)
            str_045KeyValue = objCommondata.GetConfigSetting("045", bln_045KeyExist)
            str_065KeyValue = objCommondata.GetConfigSetting("065", bln_065KeyExist)
            Dim strRepairEstimate As String = CType(RetrieveData(REPAIR_ESTIMATEMODE), String)
            If e.Row.RowType = DataControlRowType.Header Then
                If bln_044KeyExist Then
                    CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat(str_044KeyValue, " *")
                    CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ToolTip = str_044KeyValue
                End If
                If bln_045KeyExist Then
                    CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = str_045KeyValue
                    CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ToolTip = str_045KeyValue
                End If
                If strRepairEstimate = "Repair Estimate" AndAlso bln_061KeyExist Then
                    If str_061KeyValue.ToLower = "true" Then
                        objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 12, "true")
                        objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "show", 13, "true")
                        'objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 15, "false")
                        objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 14, "false")
                        If str_063KeyValue.ToLower <> "true" Then
                            ifgLineDetail.Columns.Item(11).Visible = False
                            'objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 11, "false")
                            Dim lkpTaxType As iLookup = CType(DirectCast(DirectCast(e.Row.Cells(11),  _
                           iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                            lkpTaxType.Validator.IsRequired = False
                        End If
                        'objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 11, "false")
                    Else
                        ifgLineDetail.Columns.Item(4).Visible = False
                        ifgLineDetail.Columns.Item(13).Visible = False
                        objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 10, "true")
                        ifgLineDetail.Columns.Item(14).Visible = True
                        If str_063KeyValue.ToLower <> "true" Then
                            ifgLineDetail.Columns.Item(11).Visible = False
                            'objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 11, "false")
                            Dim lkpTaxType As iLookup = CType(DirectCast(DirectCast(e.Row.Cells(11),  _
                           iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                            lkpTaxType.Validator.IsRequired = False
                        End If
                    End If
                ElseIf strRepairEstimate = "Repair Approval" AndAlso bln_065KeyExist Then
                    If str_061KeyValue.ToLower = "true" Then
                        CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = "Select"
                        objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "show", 14, "true")
                        ifgLineDetail.Columns.Item(13).Visible = True
                    Else

                    End If
                Else
                    objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 13, "true")
                End If

                If strRepairEstimate = "Repair Approval" Then
                    If str_063KeyValue.ToLower() = "true" Then
                        objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 11, "true")
                    Else
                        ifgLineDetail.Columns.Item(13).Visible = False
                        ifgLineDetail.Columns.Item(11).Visible = False
                        'objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 11, "false")
                    End If
                End If

                If strRepairEstimate = "Repair Estimate" AndAlso str_061KeyValue.ToLower = "true" Then
                    objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 14, "false")
                End If
            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                If bln_044KeyExist Then
                    Dim lkpItemCode As iLookup = CType(DirectCast(DirectCast(e.Row.Cells(0),  _
                             iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpItemCode.Validator.ReqErrorMessage = str_044KeyValue.Trim + " is Required"
                    lkpItemCode.Validator.LkpErrorMessage = "Invalid " + str_044KeyValue.Trim + ".Click on the List for Valid Values "
                    If str_044KeyValue.Trim = "Location" Then
                        lkpItemCode.HelpText = "599," + "ITEM_ITM_CD"
                    Else
                        lkpItemCode.HelpText = "358," + "ITEM_ITM_CD"
                    End If
                End If
                If bln_045KeyExist Then
                    Dim lkpSubItemCode As iLookup = CType(DirectCast(DirectCast(e.Row.Cells(3),  _
                            iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    'lkpSubItemCode.Validator.ReqErrorMessage = str_045KeyValue.Trim + " is Required"
                    lkpSubItemCode.Validator.LkpErrorMessage = "Invalid " + str_045KeyValue.Trim + ".Click on the List for Valid Values "
                    If str_045KeyValue.Trim = "Component" Then
                        CType(DirectCast(DirectCast(e.Row.Cells(1),  _
                            iFgFieldCell).ContainingField, LookupField).Lookup, iLookup).HelpText = "597," + "SUB_ITEM_SB_ITM_CD"
                    Else
                        CType(DirectCast(DirectCast(e.Row.Cells(1),  _
                            iFgFieldCell).ContainingField, LookupField).Lookup, iLookup).HelpText = "359," + "SUB_ITEM_SB_ITM_CD"
                    End If
                End If

                If strRepairEstimate = "Repair Estimate" AndAlso bln_061KeyExist Then
                    If str_061KeyValue.ToLower = "true" Then
                        ' objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 12, "false")
                        objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "show", 12, "true")
                        objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "show", 13, "true")
                        objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 14, "false")

                        If str_063KeyValue.ToLower <> "true" Then
                            'objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 11, "false")
                            ifgLineDetail.Columns.Item(11).Visible = False
                            Dim lkpTaxType As iLookup = CType(DirectCast(DirectCast(e.Row.Cells(11),  _
                           iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                            lkpTaxType.Validator.IsRequired = False
                        End If
                    Else
                        'objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 4, "false")
                        ifgLineDetail.Columns.Item(13).Visible = False
                        ifgLineDetail.Columns.Item(4).Visible = False
                        'objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 11, "false")
                        ifgLineDetail.Columns.Item(14).Visible = True
                        If str_063KeyValue.ToLower <> "true" Then
                            ifgLineDetail.Columns.Item(11).Visible = False
                            'objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 11, "false")
                            Dim lkpTaxType As iLookup = CType(DirectCast(DirectCast(e.Row.Cells(11),  _
                           iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                            lkpTaxType.Validator.IsRequired = False
                        End If
                    End If
                ElseIf strRepairEstimate = "Repair Approval" AndAlso bln_065KeyExist Then
                    If str_065KeyValue.ToLower = "true" Then
                        objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "show", 14, "true")
                        ifgLineDetail.Columns.Item(13).Visible = True
                    Else
                        ifgLineDetail.Columns.Item(4).Visible = False
                    End If
                Else
                    'objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 12, "false")
                    objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 13, "false")
                    objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "show", 14, "true")
                    ifgLineDetail.Columns.Item(13).Visible = False
                    Dim chkBox As iFgCheckBox
                    Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                    chkBox = CType(e.Row.Cells(14).Controls(0), iFgCheckBox)
                    chkBox.Attributes.Add("onclick", "showApprovalAmount(this," & e.Row.RowIndex & ")")
                    chkBox.ToolTip = "Check for Approval Amount"
                End If

                If strRepairEstimate = "Repair Approval" Then
                    If str_063KeyValue.ToLower() = "true" Then
                        objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 11, "true")
                        CType(DirectCast(DirectCast(e.Row.Cells(11), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup).Validator.Validate = True
                    Else
                        ifgLineDetail.Columns.Item(11).Visible = False
                        'objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 11, "false")
                        CType(DirectCast(DirectCast(e.Row.Cells(11), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup).Validator.Validate = False
                    End If
                End If

                If strRepairEstimate = "Repair Estimate" AndAlso str_061KeyValue.ToLower = "true" Then
                    objCommondata.SetGridVisibilitybyIndex(ifgLineDetail, "hide", 14, "false")
                End If

                'Highlight the Man Hour or Material Cost Change
                Dim strPageName As String = Nothing
                Dim strPageMode As String = Nothing
                If Not RetrieveData(PAGENAME) Is Nothing Then
                    strPageName = RetrieveData(PAGENAME).ToString()
                End If

                If Not RetrieveData(PAGEMODE) Is Nothing Then
                    strPageMode = RetrieveData(PAGEMODE).ToString()
                End If
                If (bln_061KeyExist OrElse bln_065KeyExist) AndAlso (str_061KeyValue.ToLower = "true" OrElse str_065KeyValue.ToLower = "true") Then
                    If Not strPageName Is Nothing AndAlso strPageName.ToUpper() = "REPAIR ESTIMATE" AndAlso Not strPageMode Is Nothing AndAlso strPageMode.ToUpper() = "NEW" Or strPageMode.ToUpper() = "FETCH" Or strPageMode.ToUpper() = "CLEAR" Then

                    Else
                        Dim drview As DataRowView = CType(e.Row.DataItem, Data.DataRowView)

                        If Not drview.Item(RepairEstimateData.TRFF_CD_DTL_ID) Is DBNull.Value AndAlso Not drview.Item(RepairEstimateData.CHNG_BIT) Is DBNull.Value AndAlso CBool(drview.Item(RepairEstimateData.CHNG_BIT)) = True Then

                        Else
                            e.Row.Style.Add("background-color", "#FFEA71")
                        End If
                    End If
                End If
            End If
            'Dim chk As iFgCheckBox
            'Dim rowIndex As Int32
            'rowIndex = e.Row.RowIndex
            'chk = CType(e.Row.Cells(11).Controls(0), iFgCheckBox)
            'chk.Attributes.Add("onclick", "updateApprovalAmount(this,'" + rowIndex.ToString + "');")
            ' CType(e.Row.Cells(11).Controls(0), iFgCheckBox).Checked = True
            'chk.Checked = True
            'If strRepairEstimate = REPAIR_APPROVAL Then
            '    ifgLineDetail.Columns.Item(11).Visible = True
            If strRepairEstimate = SURVEY_COMPLETION Then
                CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(11), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                ifgLineDetail.Columns.Item(4).Visible = False
                ifgLineDetail.Columns.Item(11).Visible = False
                ifgLineDetail.Columns.Item(14).Visible = False
                CType(e.Row.Cells(12), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                ifgLineDetail.AllowAdd = False
                ifgLineDetail.AllowDelete = False
                If e.Row.RowIndex > 1 Then
                    Dim lkpControlType As iLookup
                    lkpControlType = CType(DirectCast(DirectCast(e.Row.Cells(1), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpControlType.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                    Dim lkpSubItem As iLookup
                    lkpSubItem = CType(DirectCast(DirectCast(e.Row.Cells(2), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpSubItem.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                    Dim lkpDam As iLookup
                    lkpDam = CType(DirectCast(DirectCast(e.Row.Cells(3), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpDam.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                    Dim lkpRpr As iLookup
                    lkpRpr = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpRpr.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If

            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgLineDetail_RowInserting"
    Protected Sub ifgLineDetail_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgLineDetail.RowInserting
        Try
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            'If CommonWeb.pub_IsRowAlreadyExists(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), CType(e.Values, OrderedDictionary), strArrFilter, "New", RepairEstimateData.RPR_ESTMT_DTL_ID, CInt(e.Values(RepairEstimateData.RPR_ESTMT_DTL_ID))) Then
            '    e.Cancel = True
            '    e.OutputParamters.Add("Error", "Same Combination cannot be entered when there is already record existing with the given combination.")
            'End If
            e.Values(RepairEstimateData.RPR_ESTMT_DTL_ID) = CommonWeb.GetNextIndex(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), RepairEstimateData.RPR_ESTMT_DTL_ID)
            Dim str_061KeyValue As String
            Dim bln_061KeyExist As Boolean
            Dim objCommondata As New CommonData
            str_061KeyValue = objCommondata.GetConfigSetting("061", bln_061KeyExist)
            If bln_061KeyExist AndAlso str_061KeyValue.ToLower = "true" Then

            Else
                pvt_CalculateSummaryDetail(CommonWeb.pub_IsNullString(e.Values(RepairEstimateData.LBR_HRS)), _
                                     CommonWeb.pub_IsNullString(e.Values(RepairEstimateData.LBR_RT)), _
                                     CommonWeb.pub_IsNullString(e.Values(RepairEstimateData.LBR_HR_CST_NC)), _
                                     CommonWeb.pub_IsNullString(e.Values(RepairEstimateData.MTRL_CST_NC)), _
                                     CommonWeb.pub_IsNullString(e.Values(RepairEstimateData.RSPNSBLTY_CD)), _
                                     dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL))
            End If
            Dim dblApprovalAmount As Decimal

            Dim strPageMode As String = RetrieveData(REPAIR_ESTIMATEMODE).ToString

            If strPageMode.ToLower() = "repair estimate" Then
                If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                    For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows

                        If drRepair.RowState <> DataRowState.Deleted Then
                            Dim tmp As Decimal
                            tmp = CDec(drRepair.Item(RepairEstimateData.TTL_CST_NC))
                            dblApprovalAmount += tmp
                        End If
                    Next
                    e.OutputParamters.Add("ApprovalAmount", dblApprovalAmount)
                End If
            Else
                If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                    For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(RepairEstimateData.CHK_BT & "= 'True'")
                        Dim tmp As Decimal
                        tmp = CDec(drRepair.Item(RepairEstimateData.TTL_CST_NC))
                        dblApprovalAmount += tmp
                    Next
                    e.OutputParamters.Add("ApprovalAmount", dblApprovalAmount)
                End If
            End If


            e.OutputParamters.Add("Summary", "True")
            CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgLineDetail_RowUpdating"
    Protected Sub ifgLineDetail_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgLineDetail.RowUpdating
        Try
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            'If CommonWeb.pub_IsRowAlreadyExists(CType(ifgLineDetail.DataSource, DataTable), CType(e.NewValues, OrderedDictionary), strArrFilter, "New", RepairEstimateData.RPR_ESTMT_DTL_ID, CInt(e.OldValues(RepairEstimateData.RPR_ESTMT_DTL_ID))) Then
            '    e.Cancel = True
            '    e.RequiresRebind = True
            '    e.OutputParamters.Add("Error", "Same Combination cannot be entered when there is already record existing with the given combination.")
            'End If
            Dim str_061KeyValue As String
            Dim bln_061KeyExist As Boolean
            Dim objCommondata As New CommonData
            'Dim strRowCnt As Integer = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Select(String.Concat(" RSPNSBLTY_ID =", e.NewValues(RepairEstimateData.RSPNSBLTY_ID))).Length
            str_061KeyValue = objCommondata.GetConfigSetting("061", bln_061KeyExist)
            Dim str_065KeyValue As String
            Dim bln_065KeyExist As Boolean
            str_065KeyValue = objCommondata.GetConfigSetting("065", bln_065KeyExist)
            If bln_061KeyExist AndAlso str_061KeyValue.ToLower = "true" Then

            Else
                pvt_CalculateSummaryDetail(CommonWeb.pub_IsNullString(e.NewValues(RepairEstimateData.LBR_HRS)), _
                                      CommonWeb.pub_IsNullString(e.NewValues(RepairEstimateData.LBR_RT)), _
                                      CommonWeb.pub_IsNullString(e.NewValues(RepairEstimateData.LBR_HR_CST_NC)), _
                                      CommonWeb.pub_IsNullString(e.NewValues(RepairEstimateData.MTRL_CST_NC)), _
                                      CommonWeb.pub_IsNullString(e.NewValues(RepairEstimateData.RSPNSBLTY_CD)), _
                                      dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL))

            End If
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)


            CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
            Dim dblApprovalAmount As Decimal

            Dim strPageMode As String = RetrieveData(REPAIR_ESTIMATEMODE).ToString

            If strPageMode.ToLower() = "repair estimate" Then
                If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                    For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows

                        If drRepair.RowState <> DataRowState.Deleted Then
                            Dim tmp As Decimal
                            tmp = CDec(drRepair.Item(RepairEstimateData.TTL_CST_NC))
                            dblApprovalAmount += tmp
                        End If
                    Next
                    'dblApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                    e.OutputParamters.Add("ApprovalAmount", dblApprovalAmount)
                End If
            Else
                If str_065KeyValue.ToLower = "true" AndAlso bln_065KeyExist Then
                    If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                        For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(RepairEstimateData.CHK_BT & "= 'True'")
                            Dim tmp As Decimal
                            tmp = CDec(drRepair.Item(RepairEstimateData.TTL_CST_NC))
                            dblApprovalAmount += tmp
                        Next
                        'dblApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))

                    End If
                Else
                    dblApprovalAmount = CDec(CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", "")))
                End If
                e.OutputParamters.Add("ApprovalAmount", dblApprovalAmount)
            End If


        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgLineDetail_RowDeleting"
    Protected Sub ifgLineDetail_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgLineDetail.RowDeleting
        Try
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)

            Dim str_061KeyValue As String
            Dim bln_061KeyExist As Boolean
            Dim objCommondata As New CommonData
            str_061KeyValue = objCommondata.GetConfigSetting("061", bln_061KeyExist)
            If bln_061KeyExist AndAlso str_061KeyValue.ToLower = "true" Then
                ifgLineDetail.ActionButtons(0).Visible = False
            Else
                pvt_CalculateSummaryDetail(CStr(e.Values(RepairEstimateData.LBR_HRS)), _
                                   CStr(e.Values(RepairEstimateData.LBR_RT)), _
                                   CStr(e.Values(RepairEstimateData.LBR_HR_CST_NC)), _
                                   CStr(e.Values(RepairEstimateData.MTRL_CST_NC)), _
                                   "DEL", _
                                   dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL))
                pvt_BindEmptySummary(dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL))

                pvt_CalculateSummaryDetail(CStr((-CDbl(e.Values(RepairEstimateData.LBR_HRS)))), _
                                           CStr((-CDbl(e.Values(RepairEstimateData.LBR_RT)))), _
                                           CStr((-CDbl(e.Values(RepairEstimateData.LBR_HR_CST_NC)))), _
                                           CStr((-CDbl(e.Values(RepairEstimateData.MTRL_CST_NC)))), _
                                           CStr(e.Values(RepairEstimateData.RSPNSBLTY_CD)), _
                                           dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL))
            End If
            dtRepairEstimate = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Copy()
            dtRepairEstimate.AcceptChanges()
            If Not dtRepairEstimate.Rows.Count > 1 Then
                e.Cancel = True
                e.OutputParamters.Add("Error", "At least one line detail is mandatory for an Estimate.")
                Exit Sub
            End If
            Dim dblApprovalAmount As Decimal
            If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(RepairEstimateData.CHK_BT & "= 'True'")
                    Dim tmp As Decimal
                    tmp = CDec(drRepair.Item(RepairEstimateData.TTL_CST_NC))
                    dblApprovalAmount += tmp
                Next
                'dblApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                e.OutputParamters.Add("ApprovalAmount", dblApprovalAmount)
            End If
            CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
            e.OutputParamters.Add("Summary", "True")
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgSummaryDetail_ClientBind"
    Protected Sub ifgSummaryDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgSummaryDetail.ClientBind
        Try
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            Dim dblCustomerTotalEstimateAmount As Decimal
            Dim dblPartyTotalEstimateAmount As Decimal
            Dim dblTotalMaterialcost As Decimal = 0
            'Dim dblTotallabourCost As Decimal
            Dim sbRepairEstimate As New StringBuilder
            Dim strBaseCurrencyCode As String = ""
            If Not dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows.Count > 0 Then
                For i = 0 To 1
                    Dim drRepairEstimate As DataRow = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).NewRow()
                    drRepairEstimate.Item(RepairEstimateData.SMMRY_ID) = i + 1
                    drRepairEstimate.Item(RepairEstimateData.MN_HR_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.LBR_RT_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.MN_HR_RT_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.MTRL_CST_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.TTL_CST_SMMRY) = 0.0
                    dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows.Add(drRepairEstimate)
                Next
            Else
                If e.Parameters("Mode") Is Nothing Then
                    pvt_BindEmptySummary(dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL))
                Else
                    'If e.Parameters("Mode").ToString = "Delete" Then
                    pvt_BindEmptySummary(dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL))
                    For Each drRepairEstimate As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
                        If Not drRepairEstimate.RowState = DataRowState.Deleted Then
                            pvt_CalculateSummaryDetail(drRepairEstimate.Item(RepairEstimateData.LBR_HRS).ToString, _
                                                       drRepairEstimate.Item(RepairEstimateData.LBR_RT).ToString, _
                                                       drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC).ToString, _
                                                       drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC).ToString, _
                                                       drRepairEstimate.Item(RepairEstimateData.RSPNSBLTY_CD).ToString, _
                                                       dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL))
                        End If
                    Next
                    'End If
                End If
                dtRepairEstimate = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL)
                dblCustomerTotalEstimateAmount = CommonWeb.iDec(dtRepairEstimate.Rows(0).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + _
                                                 CommonWeb.iDec(dtRepairEstimate.Rows(0).Item(RepairEstimateData.MTRL_CST_SMMRY))


                dblPartyTotalEstimateAmount = CommonWeb.iDec(dtRepairEstimate.Rows(1).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + _
                                              CommonWeb.iDec(dtRepairEstimate.Rows(1).Item(RepairEstimateData.MTRL_CST_SMMRY))

                dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(0).Item(RepairEstimateData.TTL_CST_SMMRY) = dblCustomerTotalEstimateAmount
                dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(1).Item(RepairEstimateData.TTL_CST_SMMRY) = dblPartyTotalEstimateAmount
            End If
            Dim dblTotal As Double = 0
            dblTotal = dblCustomerTotalEstimateAmount + dblPartyTotalEstimateAmount
            e.Parameters.Add("TotalCost", dblTotal)
            e.OutputParameters.Add("SummaryDetail", sbRepairEstimate.ToString)
            e.DataSource = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL)
            CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_BindEmptySummary"
    Private Sub pvt_BindEmptySummary(ByRef br_dtSummary As DataTable)
        Try
            For Each drSummary As DataRow In br_dtSummary.Rows
                drSummary.Item(RepairEstimateData.MN_HR_SMMRY) = 0.0
                drSummary.Item(RepairEstimateData.LBR_RT_SMMRY) = 0.0
                drSummary.Item(RepairEstimateData.MN_HR_RT_SMMRY) = 0.0
                drSummary.Item(RepairEstimateData.MTRL_CST_SMMRY) = 0.0
                drSummary.Item(RepairEstimateData.TTL_CST_SMMRY) = 0.0
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CalculateSummaryDetail"
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
                    br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MN_HR_SMMRY) = CommonWeb.iDec(br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MN_HR_SMMRY)) + CommonWeb.iDec(bv_strLabourHour)
                    br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.LBR_RT_SMMRY) = CommonWeb.iDec(br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.LBR_RT_SMMRY)) + CommonWeb.iDec(bv_strLaborHourRate)
                    br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MN_HR_RT_SMMRY) = CommonWeb.iDec(br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + CommonWeb.iDec(bv_strLabourHourCost)
                    br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MTRL_CST_SMMRY) = CommonWeb.iDec(br_dtSummaryDetail.Rows(0).Item(RepairEstimateData.MTRL_CST_SMMRY)) + decMaterialCost
                Case "I"
                    br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MN_HR_SMMRY) = CommonWeb.iDec(br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MN_HR_SMMRY)) + CommonWeb.iDec(bv_strLabourHour)
                    br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.LBR_RT_SMMRY) = CommonWeb.iDec(br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.LBR_RT_SMMRY)) + CommonWeb.iDec(bv_strLaborHourRate)
                    br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MN_HR_RT_SMMRY) = CommonWeb.iDec(br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MN_HR_RT_SMMRY)) + CommonWeb.iDec(bv_strLabourHourCost)
                    br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MTRL_CST_SMMRY) = CommonWeb.iDec(br_dtSummaryDetail.Rows(1).Item(RepairEstimateData.MTRL_CST_SMMRY)) + decMaterialCost
            End Select
            Dim dblApprovalAmount As Double = 0
            If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                dblApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                CacheData("ApprovalAmount", dblApprovalAmount)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgLineDetail_RowInserted"
    Protected Sub ifgLineDetail_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgLineDetail.RowInserted
        Try
            Dim dblApprovalAmount As Double = 0
            Dim objCommondata As New CommonData
            Dim str_061KeyValue As String
            Dim bln_061KeyExist As Boolean
            str_061KeyValue = objCommondata.GetConfigSetting("061", bln_061KeyExist)
            Dim str_065KeyValue As String
            Dim bln_065KeyExist As Boolean
            str_065KeyValue = objCommondata.GetConfigSetting("065", bln_065KeyExist)
            If bln_061KeyExist Then
                If str_061KeyValue.ToLower = "true" Then
                    ifgLineDetail.ActionButtons(0).Visible = False
                End If
            End If
            If Not bln_065KeyExist Then
                If Not str_065KeyValue.ToLower = "true" Then
                    If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                        dblApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                        e.OutputParamters.Add("ApprovalAmount", dblApprovalAmount)
                    End If
                Else
                    For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(RepairEstimateData.CHK_BT & "= 'True'")
                        Dim tmp As Decimal
                        tmp = CDec(drRepair.Item(RepairEstimateData.TTL_CST_NC))
                        dblApprovalAmount += tmp
                    Next
                    e.OutputParamters.Add("ApprovalAmount", dblApprovalAmount)
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgLineDetail_RowUpdated"
    Protected Sub ifgLineDetail_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgLineDetail.RowUpdated
        Try
            Dim str_065KeyValue As String
            Dim bln_065KeyExist As Boolean
            Dim objCommondata As New CommonData
            str_065KeyValue = objCommondata.GetConfigSetting("065", bln_065KeyExist)
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            Dim dblApprovalAmount As Double = 0

            Dim strPageMode As String = RetrieveData(REPAIR_ESTIMATEMODE).ToString

            If strPageMode.ToLower() = "repair estimate" Then
                If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                    For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows
                        If drRepair.RowState <> DataRowState.Deleted Then
                            Dim tmp As Decimal
                            tmp = CDec(drRepair.Item(RepairEstimateData.TTL_CST_NC))
                            dblApprovalAmount += tmp
                        End If
                    Next
                    'dblApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                    e.OutputParamters.Add("ApprovalAmount", dblApprovalAmount)
                End If
            Else
                If bln_065KeyExist AndAlso str_065KeyValue.ToLower = "true" Then
                    If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                        For Each drRepair As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(RepairEstimateData.CHK_BT & "= 'True'")
                            Dim tmp As Decimal
                            tmp = CDec(drRepair.Item(RepairEstimateData.TTL_CST_NC))
                            dblApprovalAmount += tmp
                        Next
                        'dblApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))

                    End If
                Else
                    dblApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                End If
                e.OutputParamters.Add("ApprovalAmount", dblApprovalAmount)
            End If


            Dim objDatasetHelpers As New DatasetHelpers
            Dim dtSummaryDetail As New DataTable
            If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                dtSummaryDetail = objDatasetHelpers.SelectGroupByInto(String.Concat(RepairEstimateData._SUMMARY_DETAIL_GWS), dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), _
                                                                "RSPNSBLTY_CD,SUM(LBR_HRS) MN_HR_SMMRY, SUM(LBR_RT) LBR_RT_SMMRY,SUM(LBR_HR_CST_NC) MN_HR_RT_SMMRY,SUM(MTRL_CST_NC) MTRL_CST_SMMRY, SUM(TTL_CST_NC) TTL_CST_SMMRY", _
                                                                                                     "", "RSPNSBLTY_ID")
                If dtSummaryDetail.Rows.Count > 0 Then
                    dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Clear()
                    dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Merge(dtSummaryDetail)
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgLineDetail_RowDeleted"
    Protected Sub ifgLineDetail_RowDeleted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeletedEventArgs) Handles ifgLineDetail.RowDeleted
        Try
            Dim dblApprovalAmount As Double = 0
            Dim dblRepairApprovalAmount As Double = 0
            If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                If Not dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", "").ToString = "" Then
                    dblApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                End If
                e.OutputParamters.Add("ApprovalAmount", dblApprovalAmount)
            End If
            Dim dtRepairEstimate As New DataTable
            dtRepairEstimate = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Copy()
            dtRepairEstimate.AcceptChanges()
            'If Not dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", RepairEstimateData.CHK_BT + "= 'True'").ToString = "" Then
            '    dblRepairApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", RepairEstimateData.CHK_BT + "= 'True'"))
            'End If
            ' e.OutputParamters.Add("RepairApprovalAmount", dblRepairApprovalAmount)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(txtCustomer)
        CommonWeb.pub_AttachHasChanges(datGateInDate)
        CommonWeb.pub_AttachHasChanges(datEstimationDate)
        CommonWeb.pub_AttachHasChanges(lkpStatus)
        CommonWeb.pub_AttachHasChanges(datLastTestDate)
        CommonWeb.pub_AttachHasChanges(lkpLastTestType)
        CommonWeb.pub_AttachHasChanges(txtValidityYear)
        'CommonWeb.pub_AttachHasChanges(txtPartyRef)
        CommonWeb.pub_AttachHasChanges(datNextTestDate)
        CommonWeb.pub_AttachHasChanges(txtLastSurveyor)
        CommonWeb.pub_AttachHasChanges(lkpRepairType)
        CommonWeb.pub_AttachHasChanges(lkpInvoiceparty)
        CommonWeb.pub_AttachHasChanges(txtLaborRate)
        CommonWeb.pub_AttachDescMaxlength(txtRemarks)
        CommonWeb.pub_AttachHasChanges(chkCertofCleanlinessBit)
        CommonWeb.pub_AttachHasChanges(datApprovalDate)
        pub_SetGridChanges(ifgLineDetail, "tabEstimate")
    End Sub
#End Region

#Region "pvt_ValidatePreviousActivityDate"
    Private Sub pvt_ValidatePreviousActivityDate(ByVal bv_strEquipmentNo As String, _
                                                 ByVal bv_strEventDate As String, _
                                                 ByVal bv_strPageName As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objGateIn As New Gatein
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData
            Dim dsCommonUI As New CommonUIDataSet
            blnDateValid = objCommonUI.pub_GetEquipmentPreviousActivityDate(bv_strEquipmentNo, _
                                                                            CDate(bv_strEventDate), _
                                                                            dtPreviousDate, _
                                                                            bv_strPageName, _
                                                                            CInt(objCommon.GetDepotID()))
            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("" + bv_strPageName + "  Date should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), " )"))
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CheckSelectBit"
    Private Sub pvt_CheckSelectBit()
        Try
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            Dim intCount As Integer = 0
            Dim blnSelect As Boolean
            If Not dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute(String.Concat("COUNT(", RepairEstimateData.RPR_ESTMT_DTL_ID, ")"), String.Concat(RepairEstimateData.CHK_BT, "= 'True'")).ToString = "" Then
                intCount = CInt(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute(String.Concat("COUNT(", RepairEstimateData.RPR_ESTMT_DTL_ID, ")"), String.Concat(RepairEstimateData.CHK_BT, "= 'True'")))
            End If
            If intCount > 0 Then
                blnSelect = True
            Else
                blnSelect = False
            End If
            pub_SetCallbackReturnValue("blnSelect", CStr(blnSelect).ToUpper)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                         MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_validateInvoicingParty()"
    Private Sub pvt_validateInvoicingParty(ByVal bv_strInvoicingParty As String)
        Try
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            Dim drParty As DataRow() = Nothing
            Dim blnParty As Boolean
            Dim blnRes As Boolean
            drParty = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairEstimateData.RSPNSBLTY_ID, " = '67'"))
            If bv_strInvoicingParty = Nothing AndAlso drParty.Length > 0 Then
                blnParty = True
            ElseIf bv_strInvoicingParty <> Nothing AndAlso drParty.Length = 0 Then
                blnRes = True
            End If
            pub_SetCallbackReturnValue("LineRes", CStr(blnParty).ToUpper)
            pub_SetCallbackReturnValue("HeaderParty", CStr(blnRes).ToUpper)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                         MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetLockData"
    Private Function pvt_GetLockData(ByVal bv_strEquipmentNo As String, _
                                     ByRef br_strIpError As String, _
                                     ByRef br_strActivityName As String) As String
        Try
            Dim objCommonData As New CommonData
            Dim strErrorMessage As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim strIpAddress As String = GetClientIPAddress()
            Dim strCurrentIpAddress As String = String.Empty
            Dim strUserName As String = String.Empty
            If Not pub_GetQueryString("activityname") Is Nothing Then
                br_strActivityName = pub_GetQueryString("activityname")
            End If
            blnLockData = objCommonData.pub_GetLockData(False, bv_strEquipmentNo, strUserName, br_strActivityName, strIpAddress, True, RepairEstimateData.EQPMNT_NO)

            If blnLockData Then
                strCurrentIpAddress = GetClientIPAddress()
                If strCurrentIpAddress = strIpAddress Then
                    br_strIpError = "true"
                Else
                    br_strIpError = "false"
                End If
            End If
            Return strUserName
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
            Return String.Empty
        End Try
    End Function
#End Region

#Region "pvt_ValidateGateINAttachment"
    Private Sub pvt_ValidateGateINAttachment(ByVal bv_strGITransactionNO As String, ByVal bv_strEquipmentNO As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objGateIn As New RepairEstimate
            Dim dsGateOutAttchemnt As RepairEstimateDataSet
            dsGateOutAttchemnt = objGateIn.pub_GetAttchemntbyGateIN(bv_strGITransactionNO, "GateIn")
            If CInt(dsGateOutAttchemnt.Tables(RepairEstimateData._REPAIR_ESTIMATE).Rows(0).Item("COUNT_ATTACH")) > 0 Then
                pub_SetCallbackReturnValue("Message", "Yes")
                pub_SetCallbackReturnValue("GateInID", CStr(CInt(dsGateOutAttchemnt.Tables(RepairEstimateData._REPAIR_ESTIMATE).Rows(0).Item("COUNT_ATTACH"))))
            Else
                pub_SetCallbackReturnValue("Message", "No")
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub HideMenusGWS(ByVal str_061Value As String, ByVal str_063Value As String, ByVal str_065Value As String)
        Dim strActivityName As String = String.Empty
        If Not pub_GetQueryString("activityname") Is Nothing Then
            strActivityName = pub_GetQueryString("activityname")
        End If

        If str_061Value.ToLower = "true" Then
            Dim objRepairEstimate As New RepairEstimate
            lblLastTestDate.Visible = False
            lblLastTestType.Visible = False
            lblNextTestDate.Visible = False
            lblNextTestType.Visible = False
            lblInvoicingParty.Visible = False
            lkpRepairType.Visible = False
            txtNextTest.Visible = False
            lkpLastTestType.Visible = False
            datLastTestDate.Visible = False
            datNextTestDate.Visible = False
            lkpInvoiceparty.Visible = False
            lblValidityYear.Visible = False
            lblRepairType.Visible = False
            lblApprovalDate.Visible = False
            lblApprovalDateSym.Visible = False
            datApprovalDate.Visible = False
            txtValidityYear.Visible = False
            chkCertofCleanlinessBit.Visible = False
            lblCertofCleanliness.Visible = False
            lblLastSurveyor.Visible = False
            txtLastSurveyor.Visible = False
            lblTariffGroupCode.Visible = False
            lkpTariffGroup.Visible = False
            lbltariffCode.Visible = False
            lkpTariffCode.Visible = False
            txtAgentName.ReadOnly = True
            lkpStatus.Visible = False
            ifgSummaryDetail.Visible = False
            divLeaktest.Visible = False
            ifgLineDetail.ActionButtons.Item(0).Visible = False
            'lblSurveyorName.Visible = False
            'txtSurveyorName.Visible = False
            If str_063Value.ToLower <> "true" Then
                txtTaxRate.Visible = False
                lblTaxRate.Visible = False
                lblsrvTax.Visible = False
                lblServiceTax.Visible = False
                lblTotEstAmt.Visible = False
                lblTotalEstimatedAmount.Visible = False
            End If
        ElseIf str_061Value.ToLower = "false" Then
            ILabel3.Visible = False
            lkpInvoiceparty.Visible = True
            ifgLineDetail.ActionButtons.Item(0).Visible = True
            lkpBillTo.Visible = False
            lblBillTo.Visible = False
            lblAgent.Visible = False
            txtAgentName.Visible = False
            lblYardLocation.Visible = False
            txtYardLocation.Visible = False
            lblUnit.Visible = False
            lkpUnit.Visible = False
            lblMeasure.Visible = False
            lkpMeasure.Visible = False
            datPreviousONH.Visible = False
            lblPrevONHDate.Visible = False
            lkpPrevONHLocation.Visible = False
            lblPrevONHLocation.Visible = False
            lblApprovalref.Text = " Cust. App Ref"
            txtApprovalRef.Validator.ReqErrorMessage = "Customer Approval Reference Required"
            lblPrevONHDate.Visible = False
            lblConsignee.Visible = False
            txtConsignee.Visible = False
            lblApprovalAmount.Visible = False
            txtApprovalAmount.Visible = False
            lkpEquipStatusGWS.Visible = False
            'ILabel2.Visible = False
            If strActivityName <> "Repair Approval" Then
                lblApprovalDate.Visible = False
                lblApprovalDateSym.Visible = False
            End If
            lblCustomerTariff.Visible = False
            lkpCustomerTariff.Visible = False
            If str_063Value.ToLower <> "true" Then
                txtTaxRate.Visible = False
                lblTaxRate.Visible = False
                lblsrvTax.Visible = False
                lblServiceTax.Visible = False
                lblTotEstAmt.Visible = False
                lblTotalEstimatedAmount.Visible = False
            End If
        End If
    End Sub

    Protected Sub ifgSummaryDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgSummaryDetail.RowDataBound

    End Sub


#Region "pvt_CreateRepairEstimateGWS"
    Private Sub pvt_CreateRepairEstimateGWS(ByVal bv_strCustomerID As String, _
                                            ByVal bv_strCustomerCode As String, _
                                            ByVal bv_strEstimateDate As String, _
                                            ByVal bv_strOrginalEstimateDate As String, _
                                            ByVal bv_strStatusID As String, _
                                            ByVal bv_strStatusCode As String, _
                                            ByVal bv_strEquipmentNo As String, _
                                            ByVal bv_strEIRNo As String, _
                                            ByVal bv_strLaborRate As String, _
                                            ByVal bv_strApprovalDate As String, _
                                            ByVal bv_strApprovalRef As String, _
                                            ByVal bv_strSurveyDate As String, _
                                            ByVal bv_strSurveyName As String, _
                                            ByVal bv_strWFData As String, _
                                            ByVal bv_strMode As String, _
                                            ByVal bv_strEstimateId As String, _
                                            ByVal bv_strRevisionNo As String, _
                                            ByVal bv_strRemarks As String, _
                                            ByVal bv_strEstimationNo As String, _
                                            ByVal bv_strCustomerEstimatedCost As String, _
                                            ByVal bv_strCustomerApprovedCost As String, _
                                            ByVal bv_strPartyApprovalRef As String, _
                                            ByVal bv_intPrevONHLocation As String, _
                                            ByVal bv_intPrevONHLocationCode As String, _
                                            ByVal bv_datPrevONHLocDate As String, _
                                            ByVal bv_intMeasure As String, _
                                            ByVal bv_intUnit As String, _
                                            ByVal bv_strBillTo As String, _
                                            ByVal bv_strAgentName As String, _
                                            ByVal bv_strTaxRate As String, _
                                            ByVal bv_strConsignee As String, _
                                            ByVal bv_intActivityId As Integer, _
                                            ByVal bv_intEquipmentStatusId As Integer, _
                                            ByVal bv_strMeasureCode As String, _
                                            ByVal bv_strUnitCode As String)
        Try
            Dim objcommon As New CommonData
            Dim objCommonUI As New CommonUI
            Dim strActivitySubmit As String = String.Empty
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            Dim PageMode As String = RetrieveData(REPAIR_ESTIMATEMODE).ToString


            If Not dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("At least one line detail is mandatory for an Estimate.")
                Exit Sub
            End If

            If bv_strStatusCode.ToUpper() = "U" Then
                bv_intEquipmentStatusId = 21

            ElseIf bv_strStatusCode.ToUpper() = "J" Then
                bv_intEquipmentStatusId = 10
            ElseIf bv_strStatusCode.ToUpper() = "D" Then
                bv_intEquipmentStatusId = 9
            End If

            Dim blnMode As Boolean = False
            Dim objRepairEstimate As New RepairEstimate
            Dim lngCreated As Long
            Dim bv_strEIRNumber As String = ""
            Dim datGateinDate As DateTime
            If dsRepairEstimate.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows.Count > 0 Then
                datGateinDate = CDate(dsRepairEstimate.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Rows(0).Item(RepairEstimateData.GTN_DT))
            End If
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            str_032KeyValue = objCommonConfig.pub_GetConfigSingleValue("032", intDepotID)
            bln_032EqType_Key = objCommonConfig.IsKeyExists

            str_057GWS = objCommonConfig.pub_GetConfigSingleValue("057", intDepotID)
            bln_057GWSActive_Key = objCommonConfig.IsKeyExists
            Dim str_063GWS As String
            Dim bln_063GWSKey As Boolean
            str_057GWS = objCommonConfig.pub_GetConfigSingleValue("057", intDepotID)
            bln_057GWSActive_Key = objCommonConfig.IsKeyExists
            bln_063GWSKey = objCommonConfig.IsKeyExists
            If bln_063GWSKey Then
                str_063GWS = objCommonConfig.pub_GetConfigSingleValue("063", intDepotID)
            Else
                str_063GWS = "false"
            End If


            Dim str_067InvoiceGenerationGWSBit As String = Nothing
            str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", CInt(objcommon.GetDepotID()))


            lngCreated = objRepairEstimate.pub_CreateRepairEstimate(CommonWeb.iLng(bv_strCustomerID),
                                                                    bv_strCustomerCode, _
                                                                    CommonWeb.iDat(bv_strEstimateDate), _
                                                                    CommonWeb.iDat(bv_strOrginalEstimateDate), _
                                                                    datGateinDate, _
                                                                    bv_strEquipmentNo, _
                                                                    CommonWeb.iLng(bv_intEquipmentStatusId), _
                                                                    bv_strStatusCode, _
                                                                    bv_strEIRNo, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    CommonWeb.iDec(bv_strLaborRate), _
                                                                    CommonWeb.iDat(bv_strApprovalDate), _
                                                                    bv_strApprovalRef, _
                                                                    CommonWeb.iDat(bv_strSurveyDate), _
                                                                    bv_strSurveyName, _
                                                                    CommonWeb.iInt(objcommon.GetDepotID()), _
                                                                    bv_strWFData, _
                                                                    bv_strMode, _
                                                                    bv_strEstimateId, _
                                                                    bv_strRevisionNo, _
                                                                    bv_strEstimationNo, _
                                                                    bv_strRemarks, _
                                                                    dsRepairEstimate, _
                                                                    bv_strEIRNumber, _
                                                                    PageMode, _
                                                                    bv_strApprovalRef, _
                                                                    CDec(bv_strCustomerEstimatedCost), _
                                                                    CDec(bv_strCustomerApprovedCost), _
                                                                    strModifiedby, _
                                                                    CDate(datModifiedDate), _
                                                                    strModifiedby, _
                                                                    CDate(datModifiedDate), _
                                                                    "D", _
                                                                    CStr(bv_strPartyApprovalRef), _
                                                                    strActivitySubmit, _
                                                                    bv_intActivityId, _
                                                                    str_057GWS, _
                                                                    CommonWeb.CI32(bv_intPrevONHLocation), _
                                                                    CommonWeb.CI32(bv_intMeasure), _
                                                                    CommonWeb.CI32(bv_intUnit), _
                                                                    bv_strBillTo, _
                                                                    bv_strAgentName, _
                                                                    str_063GWS, _
                                                                    CommonWeb.iDat(bv_datPrevONHLocDate), _
                                                                    bv_strConsignee, _
                                                                    CDec(bv_strTaxRate), _
                                                                    CInt(bv_strStatusID), _
                                                                    bv_intPrevONHLocationCode, _
                                                                    str_067InvoiceGenerationGWSBit, _
                                                                    bv_strMeasureCode, _
                                                                    bv_strUnitCode)

            pub_SetCallbackReturnValue("EstimateId", lngCreated.ToString)
            pub_SetCallbackReturnValue("RepairEstimationNo", bv_strEIRNumber)
            pub_SetCallbackReturnValue("RevisionNo", bv_strRevisionNo)
            dsRepairEstimate.AcceptChanges()
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", String.Concat(PageMode, " : ", bv_strEIRNumber, strMSGINSERT))
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

    Protected Sub ifgSummaryGWS_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgSummaryGWS.ClientBind
        Try
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            Dim dblTotalManHRCost As Decimal
            Dim dblTotalEstimateAmount As Decimal
            Dim dblTotalMaterialcost As Decimal
            Dim dblLaborCost As Decimal = 0
            Dim dblServTax As Decimal
            Dim sbRepairEstimate As New StringBuilder
            Dim strPageName As String = e.Parameters("PageName").ToString()
            Dim strBaseCurrencyCode As String = ""
            Dim objDatasetHelpers As New DatasetHelpers
            Dim dtSummaryDetail As New DataTable
            If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then

                If strPageName.ToUpper() = "REPAIR APPROVAL" Then
                    dtSummaryDetail = objDatasetHelpers.SelectGroupByInto(String.Concat(RepairEstimateData._SUMMARY_DETAIL_GWS), dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), _
                                                         "RSPNSBLTY_CD,SUM(LBR_HRS) MN_HR_SMMRY, SUM(LBR_RT) LBR_RT_SMMRY,SUM(LBR_HR_CST_NC) MN_HR_RT_SMMRY,SUM(MTRL_CST_NC) MTRL_CST_SMMRY, SUM(TTL_CST_NC) TTL_CST_SMMRY", _
                                                                                              "CHK_BT='True'", "RSPNSBLTY_ID")


                Else
                    dtSummaryDetail = objDatasetHelpers.SelectGroupByInto(String.Concat(RepairEstimateData._SUMMARY_DETAIL_GWS), dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), _
                                                         "RSPNSBLTY_CD,SUM(LBR_HRS) MN_HR_SMMRY, SUM(LBR_RT) LBR_RT_SMMRY,SUM(LBR_HR_CST_NC) MN_HR_RT_SMMRY,SUM(MTRL_CST_NC) MTRL_CST_SMMRY, SUM(TTL_CST_NC) TTL_CST_SMMRY", _
                                                                                              "", "RSPNSBLTY_ID")
                End If

                If dtSummaryDetail.Rows.Count > 0 Then
                    dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Clear()
                    dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Merge(dtSummaryDetail)
                End If
            End If
            If dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Rows.Count > 0 AndAlso strPageName.ToUpper() <> "REPAIR APPROVAL" Then
                For Each drRepairEstimate As DataRow In dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Rows

                    dblTotalMaterialcost += CDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_SMMRY))
                    dblTotalManHRCost += CDec(drRepairEstimate.Item(RepairEstimateData.MN_HR_RT_SMMRY))
                    'dblLaborCost += CDec(drRepairEstimate.Item(RepairEstimateData.LBR_HRS))
                Next
                dtRepairEstimate = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS)
            End If
            Dim dblLabrTax As Decimal
            Dim dblMaterialTax As Decimal
            Dim dblLabrMatTax As Decimal
            Dim totalTaxAmt As Decimal
            If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                For Each drRepairEstimate As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows

                    If drRepairEstimate.RowState <> DataRowState.Deleted Then


                        Select Case drRepairEstimate.Item(RepairEstimateData.TX_RSPNSBLTY_ID).ToString
                            Case "L", "19"
                                dblLabrTax += CDec(drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC))
                            Case "M", "20"
                                dblMaterialTax += CDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC))
                            Case "B", "21"
                                dblLabrMatTax += CDec(drRepairEstimate.Item(RepairEstimateData.LBR_HR_CST_NC)) + CDec(drRepairEstimate.Item(RepairEstimateData.MTRL_CST_NC))
                            Case "N", "22"

                        End Select
                    End If
                Next
            End If
            dblTotalEstimateAmount = dblTotalMaterialcost + dblTotalManHRCost
            e.OutputParameters.Add("TotalMaterialCost", String.Concat(dblTotalMaterialcost.ToString))
            e.OutputParameters.Add("TotalLbrCost", String.Concat(dblTotalManHRCost.ToString))
            e.OutputParameters.Add("TotalEstmtdAmt", String.Concat(dblTotalEstimateAmount.ToString))
            'If (dblLabrMatTax + dblMaterialTax + dblLabrTax) <> 0 Then
            '    dblServTax = ((dblLabrMatTax + dblMaterialTax + dblLabrTax) * CDec(IIf(e.Parameters("TaxRate") Is "", 0.0, e.Parameters("TaxRate"))) / 100)
            'Else
            '    dblServTax = 0
            'End If
            e.OutputParameters.Add("ServiceTax", String.Concat(dblServTax.ToString))
            e.OutputParameters.Add("TotlEstCstIncTax", String.Concat(dblServTax + dblTotalEstimateAmount + dblTotalManHRCost + dblTotalMaterialcost))
            e.OutputParameters.Add("SummaryDetailGws", sbRepairEstimate.ToString)
            If Not e.Parameters("Mode") Is Nothing Then
                If e.Parameters("Mode").ToString = "Clear" Then
                    dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Rows.Clear()
                    dtSummaryDetail = objDatasetHelpers.SelectGroupByInto(String.Concat(RepairEstimateData._SUMMARY_DETAIL_GWS), dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), _
                                                                "RSPNSBLTY_CD,SUM(LBR_HRS) MN_HR_SMMRY, SUM(LBR_RT) LBR_RT_SMMRY,SUM(LBR_HR_CST_NC) MN_HR_RT_SMMRY,SUM(MTRL_CST_NC) MTRL_CST_SMMRY, SUM(TTL_CST_NC) TTL_CST_SMMRY", _
                                                                                                     "", "RSPNSBLTY_ID")
                    dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS).Merge(dtSummaryDetail)
                End If
            End If
            Dim dblApprovalAmount As Double = 0
            If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                dblApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                e.OutputParameters.Add("ApprovalAmount", dblApprovalAmount)
            End If

            If strPageName.ToUpper() = "REPAIR APPROVAL" Then

                If dtSummaryDetail.Rows.Count = 0 Then
                    Dim dr As DataRow = dtSummaryDetail.NewRow()
                    dr.Item(RepairEstimateData.MN_HR_SMMRY) = 0D
                    dr.Item(RepairEstimateData.LBR_RT_SMMRY) = 0D
                    dr.Item(RepairEstimateData.MN_HR_RT_SMMRY) = 0D
                    dr.Item(RepairEstimateData.MTRL_CST_SMMRY) = 0D
                    dr.Item(RepairEstimateData.TTL_CST_SMMRY) = 0D

                    dtSummaryDetail.Rows.Add(dr)
                End If
                e.DataSource = dtSummaryDetail
            Else
                e.DataSource = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL_GWS)
            End If


            CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub



    Private Sub pvt_UpdateRepairEstimateGWS(ByVal bv_strRepairEstimateId As String, _
                                            ByVal bv_strCustomerID As String, _
                                            ByVal bv_strCustomerCode As String, _
                                            ByVal bv_strEstimateDate As String, _
                                            ByVal bv_strOrginalEstimateDate As String, _
                                            ByVal bv_strStatusID As String, _
                                            ByVal bv_strStatusCode As String, _
                                            ByVal bv_strEquipmentNo As String, _
                                            ByVal bv_strEIRNo As String, _
                                            ByVal bv_strLaborRate As String, _
                                            ByVal bv_strApprovalDate As String, _
                                            ByVal bv_strApprovalRef As String, _
                                            ByVal bv_strSurveyDate As String, _
                                            ByVal bv_strSurveyName As String, _
                                            ByVal bv_strWFData As String, _
                                            ByVal bv_strMode As String, _
                                            ByVal bv_strEstimateId As String, _
                                            ByVal bv_strRevisionNo As String, _
                                            ByVal bv_strRemarks As String, _
                                            ByVal bv_strEstimationNo As String, _
                                            ByVal bv_strCustomerEstimatedCost As String, _
                                            ByVal bv_strCustomerApprovedCost As String, _
                                            ByVal bv_strPartyApprovalRef As String, _
                                            ByVal bv_intPrevONHLocation As String, _
                                            ByVal bv_intPrevONHLocationCode As String, _
                                            ByVal bv_datPrevONHDat As String, _
                                            ByVal bv_intMeasure As String, _
                                            ByVal bv_intUnit As String, _
                                            ByVal bv_strBillTo As String, _
                                            ByVal bv_strAgentName As String, _
                                            ByVal bv_strTaxRate As String, _
                                            ByVal bv_strGateinTransaction As String, _
                                            ByVal bv_strConsignee As String, _
                                            ByVal bv_intActivityId As Integer, _
                                            ByVal bv_intEquipmentStatusId As Integer, _
                                            ByVal bv_strMeasureCode As String, _
                                            ByVal bv_strUnitCode As String)
        Try
            Dim objcommon As New CommonData
            Dim str_037KeyValue As String = String.Empty
            Dim bln_037KeyValue As Boolean = False
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            Dim blnMode As Boolean = False
            Dim strActivitySubmit As String = String.Empty
            Dim objRepairEstimate As New RepairEstimate
            Dim PageMode As String = RetrieveData(REPAIR_ESTIMATEMODE).ToString
            If Not dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("At least one line detail is mandatory for an Estimate.")
                Exit Sub
            End If
            'REPAIR FLOW SETTING: 037
            'CR-001 (AWE_TO_AWP)
            str_037KeyValue = objcommon.GetConfigSetting("037", bln_037KeyValue)
            If bln_037KeyValue = False Then
                str_037KeyValue = String.Empty
            End If

            If bv_strStatusCode.ToUpper() = "U" Then
                bv_intEquipmentStatusId = 21

            ElseIf bv_strStatusCode.ToUpper() = "J" Then
                bv_intEquipmentStatusId = 10
            ElseIf bv_strStatusCode.ToUpper() = "D" Then
                bv_intEquipmentStatusId = 9
            End If

            Dim datPrevONHDat As DateTime = Nothing

            If bv_datPrevONHDat <> Nothing Then
                datPrevONHDat = CDate(bv_datPrevONHDat)
            End If



            Dim str_067InvoiceGenerationGWSBit As String = Nothing
            Dim objCommonConfig As New ConfigSetting()

            str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", CInt(objcommon.GetDepotID()))

            Dim bln_057KeyExist As Boolean
            Dim objCommondata As New CommonData
            str_057GWS = objCommondata.GetConfigSetting("061", bln_057KeyExist)
            Dim lngCreated As Long
            lngCreated = objRepairEstimate.pub_ModifyRepairEstimate(CLng(bv_strRepairEstimateId), _
                                                                    CommonWeb.iLng(bv_strCustomerID),
                                                                    bv_strCustomerCode, _
                                                                    CommonWeb.iDat(bv_strEstimateDate), _
                                                                    CommonWeb.iDat(bv_strOrginalEstimateDate), _
                                                                    bv_strEquipmentNo, _
                                                                    CommonWeb.iLng(bv_intEquipmentStatusId), _
                                                                    bv_strStatusCode, _
                                                                    bv_strEIRNo, _
                                                                    bv_strGateinTransaction, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    CommonWeb.iDec(bv_strLaborRate), _
                                                                    Nothing, _
                                                                    CommonWeb.iDat(bv_strApprovalDate), _
                                                                    bv_strApprovalRef, _
                                                                    bv_strPartyApprovalRef, _
                                                                    CommonWeb.iDat(bv_strSurveyDate), _
                                                                    bv_strSurveyName, _
                                                                    PageMode, _
                                                                    CommonWeb.iInt(objcommon.GetDepotID()), _
                                                                    bv_strWFData, _
                                                                    bv_strMode, _
                                                                    bv_strEstimateId, _
                                                                    bv_strRevisionNo, _
                                                                    bv_strRemarks, _
                                                                    bv_strEstimationNo, _
                                                                    CDec(bv_strCustomerEstimatedCost), _
                                                                    CDec(bv_strCustomerApprovedCost), _
                                                                    strModifiedby, _
                                                                    CDate(datModifiedDate), _
                                                                    str_037KeyValue, _
                                                                    dsRepairEstimate, _
                                                                    strActivitySubmit, _
                                                                    bv_intActivityId, _
                                                                    bv_intPrevONHLocation, _
                                                                    datPrevONHDat, _
                                                                    bv_intMeasure, _
                                                                    bv_intUnit, _
                                                                    bv_strBillTo, _
                                                                    bv_strAgentName, _
                                                                    bv_strConsignee, _
                                                                    bv_strTaxRate, _
                                                                    CLng(bv_strStatusID), _
                                                                    bv_intPrevONHLocationCode, _
                                                                    str_067InvoiceGenerationGWSBit, _
                                                                    bv_strMeasureCode, _
                                                                    bv_strUnitCode, _
                                                                    str_057GWS)

            pub_SetCallbackReturnValue("EstimateId", bv_strRepairEstimateId)
            pub_SetCallbackReturnValue("RepairEstimationNo", bv_strEstimationNo)
            pub_SetCallbackReturnValue("RevisionNo", bv_strRevisionNo)
            dsRepairEstimate.AcceptChanges()
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", String.Concat(PageMode, " : ", bv_strEstimationNo, strMSGUPDATE))
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub

    Private Sub pvt_GetCustomerTariff(ByVal intCustId As String, ByVal intAgntId As String, ByVal strBillTo As String, ByVal intEquipType As String)

        Try
            Dim intDepotID As Integer
            Dim objCommon As New CommonData
            intDepotID = CInt(objCommon.GetDepotID())
            Dim objRepairEstimate As New RepairEstimate
            Dim dtTariffCode As DataTable
            Dim dtCurrcy As New DataTable
            Dim lbrRate As String = ""
            Dim dtExchange As New DataTable
            Dim intAgentID As Int32 = 0


            Dim dt As New DataTable

            If strBillTo.ToUpper() = "AGENT" Then
                dt = objRepairEstimate.GetAgentIdByCustomer(CInt(intCustId), intDepotID)
                If dt.Rows.Count > 0 Then
                    intAgentID = CInt(dt.Rows(0).Item("AGNT_ID"))
                End If
            End If
            dtTariffCode = objRepairEstimate.GetTariffCodeTable(CInt(intAgentID), CInt(intCustId), CInt(intEquipType), intDepotID, strBillTo)

            If strBillTo.ToUpper = "AGENT" Then
                dtCurrcy = objRepairEstimate.GetCurrByAgntId(CInt(intAgentID), intDepotID)
                lbrRate = objRepairEstimate.pub_GetLaborRateperHourByAgentID(intDepotID, CInt(intAgentID))
                dtExchange = objRepairEstimate.Pub_GetAgentCurrencyExchangeRateByDptId(intDepotID, CInt(intAgentID))
                ''''dtExchange.Rows(0).Item(RepairEstimateData.EXCHNG_RT_PR_UNT_NC).ToString()
            ElseIf strBillTo.ToUpper = "CUSTOMER" Then
                dtCurrcy = objRepairEstimate.GetCurrByCstmrId(CInt(intCustId), intDepotID)
                lbrRate = objRepairEstimate.pub_GetLaborRateperHourByCustomerID(intDepotID, CInt(intCustId))
                dtExchange = objRepairEstimate.Pub_GetCurrencyExchangeRateByDptId(intDepotID, CLng(intCustId)).Tables(RepairEstimateData._V_REPAIR_ESTIMATE_REPORT)
            End If
            pub_SetCallbackReturnValue("LaborRate", lbrRate)
            pub_SetCallbackReturnValue("ServiceTax", dtCurrcy.Rows(0).Item(0).ToString)
            pub_SetCallbackReturnValue("CurrencyCode", dtCurrcy.Rows(0).Item(1).ToString)
            pub_SetCallbackReturnValue("TariffCodeId", dtTariffCode.Rows(0).Item(0).ToString)

            If dtExchange.Rows.Count > 0 Then
                If Not dtExchange.Rows(0).Item(RepairEstimateData.EXCHNG_RT_PR_UNT_NC) Is DBNull.Value Then
                    pub_SetCallbackReturnValue("Exchange", dtExchange.Rows(0).Item(RepairEstimateData.EXCHNG_RT_PR_UNT_NC).ToString())
                Else
                    pub_SetCallbackReturnValue("Exchange", CStr(0))
                End If
            Else
                pub_SetCallbackReturnValue("Exchange", CStr(0))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex)
        End Try


    End Sub

    Protected Sub lkpCustomerTariff_Search(sender As Object, e As iInterchange.WebControls.v4.Input.SearchEventArgs) Handles lkpCustomerTariff.Search
        Try

            Dim strFilterCondition As String() = Nothing
            Dim objTrariff As New TariffCode
            Dim dtTariff As New DataTable
            Dim dvTariff As New DataView
            Dim objCommon As New CommonData()
            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())


            strFilterCondition = e.FilterCondition.Split(CChar(","))

            If strFilterCondition.Length > 2 Then

                If strFilterCondition(0).ToUpper() = "CUSTOMER" Then

                    Dim dt As New DataTable

                    'Get Customer Tarriff
                    dt = objTrariff.GetTariffCodeByCustomerID(strFilterCondition(1), strFilterCondition(2), CStr(intDepotID))

                    'Get Standard Tariff

                    If dt.Rows.Count = 0 Then
                        dt = objTrariff.GetStandardTariffCode(strFilterCondition(2), CStr(intDepotID))
                    End If

                    'Add 
                    If dt.Rows.Count > 0 Then
                        dtTariff = dt
                    End If

                    If dt.Rows.Count > 0 Then
                        dtTariff = dt
                        dvTariff = dtTariff.DefaultView
                        dvTariff.RowFilter = ""

                        If CommonWeb.IsLookupValidation(Page) Then
                            If e.SearchValue <> "" Then
                                dvTariff.RowFilter = String.Concat(e.SearchColumn, " = '", e.SearchValue, "'")
                            End If
                        End If

                    End If

                End If

                If strFilterCondition(0).ToUpper() = "AGENT" Then

                    Dim dt As New DataTable

                    'Agent tariff
                    dt = objTrariff.GetTariffCodeByAgentID(strFilterCondition(1), strFilterCondition(3), CStr(intDepotID))


                    'Get Customer Tarriff

                    If dt.Rows.Count = 0 Then
                        dt = objTrariff.GetTariffCodeByCustomerID(strFilterCondition(2), strFilterCondition(3), CStr(intDepotID))
                    End If


                    'Get Standard Tariff

                    If dt.Rows.Count = 0 Then
                        dt = objTrariff.GetStandardTariffCode(strFilterCondition(3), CStr(intDepotID))
                    End If


                    If dt.Rows.Count > 0 Then
                        dtTariff = dt
                        dvTariff = dtTariff.DefaultView
                        dvTariff.RowFilter = ""

                        If CommonWeb.IsLookupValidation(Page) Then
                            If e.SearchValue <> "" Then
                                dvTariff.RowFilter = String.Concat(e.SearchColumn, " = '", e.SearchValue, "'")
                            End If
                        End If

                    End If


                End If
            End If

            lkpCustomerTariff.DataSource = dvTariff.ToTable
            lkpCustomerTariff.DataBind()

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
End Class