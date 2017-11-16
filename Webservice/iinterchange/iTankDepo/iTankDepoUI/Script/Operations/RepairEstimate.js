var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgLineDetail');
var _RowValidationFails = false;
var LocationCode = "";
var REPAIR_ESTIMATE_CREATION = "Repair Estimate >> Creation";
var REPAIR_ESTIMATE_REVISION = "Repair Estimate >> Revision";
var RMode = "";
var DamageDescription = "";
var RepairDescription = "";
var gridClearMode = "";
var tempEstimateId;
var pMode = "";
var recordLockChanges = false;
var HasPhotoUploadChanges = false;
// Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :57
//This function is used to intialize the page.

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
function initPage(sMode) {

    var blnRecordLock;
    sMode = getPageMode();
    pMode = el('hdnPageName').value;
    if (sMode == "new" && pMode == "Repair Estimate") {
        //        el('hypOrginalCost').innerText = "0.00";
        //        el('hypOrgDepotCost').innerText = "0.00";
        //        el('hypTotalCost').innerText = "0.00";
        //        el('hypDepotCost').innerText = "0.00";
        //        el('hypAppCost').innerText = "0.00";
        //        el('hypAppDepCost').innerText = "0.00";
        setText(el('hypOrginalCost'), "0.00");
        setText(el('hypOrgDepotCost'), "0.00");
        setText(el('hypTotalCost'), "0.00");
        setText(el('hypDepotCost'), "0.00");
        setText(el('hypAppCost'), "0.00");
        setText(el('hypAppDepCost'), "0.00");
    }
    //    if (pMode == "Repair Estimate") {
    //        el('txtApprovalAmount').style.visibility = "hidden";
    //    }
    if (sMode == "edit") {
        bindLineDetail(sMode, 0, 0);
        bindSummaryDetail(sMode)
        resetValidators();
    }
    if (getConfigSetting('061') == "True" || getConfigSetting('065') == "True") {
        //        if (el('lkpBillTo').value == "CUSTOMER") {
        //            setReadOnly('lkpBillTo', true);
        //        }
        //        else {
        //            setReadOnly('lkpBillTo', false);
        //        }
        hideDiv('divLineSummary');
        if (getConfigSetting('063') == "True") {
            setText(el('txtTaxRate'), "0.00");
        }
    }
    else {
        setReadOnly('lkpStatus', true);
        hideDiv('divCostSummary');
        hideDiv('divLineSummaryGWS');
    }
    currencyEstimateCalculation();
    el("hdnMode").value = sMode;
    //blnRecordLock = recordLockError(el("hypEquipmentNo").innerText, el("hdnLockUserName").value, el('hdnIpError').value, el('hdnLockActivityName').value);
    //blnRecordLock = recordLockError(getText(el('hypEquipmentNo')), el("hdnLockUserName").value, el('hdnIpError').value, el('hdnLockActivityName').value);
    blnRecordLock = recordLockError(el("txtEquipmentNo").value, el("hdnLockUserName").value, el('hdnIpError').value, el('hdnLockActivityName').value);
    if (getConfigSetting('061') != "True" || getConfigSetting('065') != "True") {
        if (!blnRecordLock) {
            el('hypLeaktest').style.visibility = "hidden";
        }
        else {
            el('hypLeaktest').style.visibility = "visible";
        }
    }


    if (el('hdnPageName').value != 'Repair Approval') {
        hideDiv("divlblApprovalAmt");
        hideDiv("divtxtApprovalAmt");
    }
    else {
        showDiv("divlblApprovalAmt");
        showDiv("divtxtApprovalAmt");
    }

    if (el('hdnPageName').value == "Repair Approval" && getConfigSetting('065') == "True") {
        setReadOnly('lkpBillTo', true);

    }

    if (el('hdnPageName').value == "Repair Approval" && sMode == "edit" && getConfigSetting('065') == "True") {
        setReadOnly('lkpEquipStatusGWS', true);
    }


    if ((el('hdnPageName').value == "Repair Estimate" && el('hdnAgentId').value == "0") && (getConfigSetting('061') == "True" || getConfigSetting('065') == "True")) {
        setReadOnly('lkpBillTo', true);
    }

    if ((el('hdnPageName').value == "Repair Estimate" && el('hdnAgentId').value != "0") && (getConfigSetting('061') == "True" || getConfigSetting('065') == "True")) {
        setReadOnly('lkpBillTo', false);
    }

    if (sMode == "edit" && el('hdnPageName').value == "Repair Approval" && getConfigSetting('065') == "True") {
        setText(el('hypAppCost'), parseFloat(el('txtApprovalAmount').value).toFixed(2));
        setText(el('hypAppDepCost'), parseFloat(el('txtApprovalAmount').value * el('hdnExchangeRate').value).toFixed(2));

    }

    reSizePane();
}

function currencyEstimateCalculation() {
    var totalCost = new Number;
    var orginalCost = new Number;
    var appCost = new Number;
    var exchangeRate = new Number;
    //    el('lblCurrencyCode').innerText = el('hdnDepotCurrencyCD').value;
    //    el('lblOrgCurrencyCode').innerText = el('hdnDepotCurrencyCD').value;
    //    el('lblDepotCurrency').innerText = el('hdnToCurrencyCD').value;
    //    el('lblOrgDepotCurrency').innerText = el('hdnToCurrencyCD').value;
    //    el('lblAppCurrency').innerText = el('hdnDepotCurrencyCD').value;
    //    el('lblAppDepCurrency').innerText = el('hdnToCurrencyCD').value;
    setText(el('lblCurrencyCode'), el('hdnDepotCurrencyCD').value);
    setText(el('lblOrgCurrencyCode'), el('hdnDepotCurrencyCD').value);
    setText(el('lblDepotCurrency'), el('hdnToCurrencyCD').value);
    setText(el('lblOrgDepotCurrency'), el('hdnToCurrencyCD').value);
    setText(el('lblAppCurrency'), el('hdnDepotCurrencyCD').value);
    setText(el('lblAppDepCurrency'), el('hdnToCurrencyCD').value);

    // if (el('hypTotalCost').innerText == "") {
    if (getText(el('hypTotalCost')) == "" || getText(el('hypTotalCost')) == null) {
        totalCost = "0.00";
    }
    else {
        //totalCost = el('hypTotalCost').innerText;
        totalCost = getText(el('hypTotalCost'));
    }
    if (el('hdnExchangeRate').value == "" || el('hdnExchangeRate').value == null) {
        exchangeRate = 0;
    }
    else {
        exchangeRate = el('hdnExchangeRate').value;
    }
    if (el('hdnDepotCurrencyCD').value == el('hdnToCurrencyCD').value) {
        exchangeRate = 1;
    }
    //if (el('hypOrginalCost').innerText == "") {
    if (getText(el('hypOrginalCost')) == "" || getText(el('hypOrginalCost')) == null) {
        orginalCost = "0.00";
    }
    else {
        //orginalCost = el('hypOrginalCost').innerText;
        orginalCost = getText(el('hypOrginalCost'));
    }
    //if (el('hypAppCost').innerText == "") {
    if (getText(el('hypAppCost')) == "" || getText(el('hypAppCost')) == null) {
        appCost = "0.00";
    }
    else {
        //appCost = el('hypAppCost').innerText;
        appCost = getText(el('hypAppCost'));
    }

    setText(el('hypDepotCost'), parseFloat(totalCost * exchangeRate).toFixed(2));
    setText(el('hypOrgDepotCost'), parseFloat(orginalCost * exchangeRate).toFixed(2));
    setText(el('hypAppDepCost'), parseFloat(appCost * exchangeRate).toFixed(2));
}

//This function is used to submit the page to the server.
function submitPage() {
    if (recordLockChanges == false) {
        GetLookupChanges();
        if (getConfigSetting('061') != "True" || getConfigSetting('065') != "True") {
            blnValidate = validateInvoicingParty();
        }
        else {
            blnValidate = true;
        }
        if (blnValidate != false) {
            bindSummaryDetail("ReBind");
            tempEstimateId = el('hdnEstimateID').value;
            var blnValidate;
            if (el("btnSubmit").value == "Print Estimate") {
                //printEstimate();
                return true;
            }
            //GetLookupChanges();
            var sMode = getPageMode();
            if (pMode == 'Repair Estimate') {
                Page_ClientValidate('divHeader');
            }
            else if (pMode == 'Repair Approval') {
                Page_ClientValidate('divHeader');
                Page_ClientValidate('divApproval');
            }
            else if (pMode == 'Survey Completion') {
                Page_ClientValidate('divHeader');
                Page_ClientValidate('divSurvey');
            }

            if (!Page_IsValid) {
                return false;
            }
            var count = ifgLineDetail.Rows().Count;
            if (count >= 0) {
                var _cols = ifgLineDetail.Rows(0).GetClientColumns();
                if (_cols[0] == "") {
                    showErrorMessage("Please Enter Atleast One Line Detail.");
                    return false;
                }
            }
            if (getPageChanges() || HasPhotoUploadChanges) {
                if (ifgLineDetail.Submit(true) == false || _RowValidationFails) {
                    return false;
                }
                if (sMode == MODE_NEW) {
                    if (getConfigSetting('061') != "True") {
                        createRepairEstimate();
                    }
                    else {
                        createRepairEstimateGWS();
                    }
                }
                else if (sMode == MODE_EDIT) {
                    if (getConfigSetting('061') != "True") {
                        updateRepairEstimate();
                    }
                    else {
                        updateRepairEstimateGWS();
                    }
                }
            }
            else {
                showInfoMessage('No Changes to Save');
            }
        }
        return true;
    }
    else {
        //recordLockMessageEdit(el("hypEquipmentNo").innerText, el("hdnLockUserName").value, el('hdnIpError').value, el('hdnLockActivityName').value);
        //recordLockMessageEdit(getText(el('hypEquipmentNo')), el("hdnLockUserName").value, el('hdnIpError').value, el('hdnLockActivityName').value);
        recordLockMessageEdit(el("txtEquipmentNo").value, el("hdnLockUserName").value, el('hdnIpError').value, el('hdnLockActivityName').value);
        return false;
    }
    ifgLineDetail.Submit(true);
    return false;
}

function createRepairEstimateGWS() {
    var oCallback = new Callback();
    oCallback.add("CustomerId", el("hdnCustomerId").value);
    oCallback.add("CustomerCd", el("txtCustomer").value);
    oCallback.add("EstimationDate", el("datEstimationDate").value);
    oCallback.add("OrginalEstimateDate", el("datOrginalEstimateDate").value);
    oCallback.add("StatusID", el("lkpEquipStatusGWS").SelectedValues[0]);
    oCallback.add("StatusCode", el("lkpEquipStatusGWS").SelectedValues[1]);
    //oCallback.add("EquipmentNo", el("hypEquipmentNo").innerText);
    //oCallback.add("EquipmentNo", getText(el('hypEquipmentNo')));
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("EirNo", el("hdnEirNo").value);
    oCallback.add("LaborRate", el("txtLaborRate").value);
    var pMode = el('hdnPageName').value;
    if (pMode == 'Repair Estimate') {
        oCallback.add("ApprovalDate", "");
        oCallback.add("ApprovalRef", "");
    }
    else if (pMode == 'Repair Approval') {
        oCallback.add("ApprovalDate", el("datApprovalDate").value);
        oCallback.add("ApprovalRef", el("txtApprovalRef").value);
    }
    oCallback.add("Consignee", el("txtConsignee").value);
    oCallback.add("SurveyDate", "");
    oCallback.add("SurveyName", "");
    oCallback.add("WFData", getWFDATA());
    oCallback.add("Mode", el("hdnMode").value);
    oCallback.add("EstimateID", el("hdnEstimateID").value);
    oCallback.add("RevisionNo", el("hdnRevisionNo").value);
    oCallback.add("PrevONHLocation", el("lkpPrevONHLocation").SelectedValues[0]);
    oCallback.add("PrevONHLocationCode", el("lkpPrevONHLocation").SelectedValues[1]);
    oCallback.add("PrevONHLocDate", el("datPreviousONH").value);
    oCallback.add("Measure", el("lkpMeasure").SelectedValues[0]);
    oCallback.add("MeasureCode", el("lkpMeasure").SelectedValues[1]);
    oCallback.add("Remarks", el("txtRemarks").value);

    if (el("txtTaxRate") != undefined) {
        oCallback.add("TaxRate", el("txtTaxRate").value);
    }
    else {
        oCallback.add("TaxRate", "0.00");
    }
    oCallback.add("Unit", el("lkpUnit").SelectedValues[0]);
    oCallback.add("UnitCode", el("lkpUnit").SelectedValues[1]);
    if (el("lkpBillTo").SelectedValues[0] == "") {
        if (el("lkpBillTo").value = "AGENT")
            oCallback.add("BillTo", "144");
        else
            oCallback.add("BillTo", "145");
    }
    else {
        oCallback.add("BillTo", el("lkpBillTo").SelectedValues[0]);
    }
    oCallback.add("AgentName", el("hdnAgentId").value);
    oCallback.add("EstimationNo", el("hdnRprEstimationNo").value);
    //    oCallback.add("CustomerEstimatedCost", el('hypDepotCost').innerText);
    //    oCallback.add("CustomerApprovedCost", el('hypAppDepCost').innerText); 
    oCallback.add("CustomerEstimatedCost", getText(el('hypDepotCost')));
    oCallback.add("CustomerApprovedCost", el("txtApprovalAmount").value);
    oCallback.add("PartyApprovalRef", "");
    oCallback.add("ActivityId", getQueryStringValue(document.location.href, "activityid"));
    oCallback.add("EquipmentStatusId", el("hdnEquipmentStatusId").value);

    oCallback.invoke("RepairEstimate.aspx", "CreateRepairEstimateGWS");
    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        resetHasChanges("ifgLineDetail");
        if (oCallback.getReturnValue("EstimateId") != "")
            el("hdnEstimateID").value = oCallback.getReturnValue("EstimateId");
        if (oCallback.getReturnValue("RepairEstimationNo") != "")
            el("hdnRprEstimationNo").value = oCallback.getReturnValue("RepairEstimationNo");
        if (oCallback.getReturnValue("RevisionNo") != "")
            el("hdnRevisionNo").value = oCallback.getReturnValue("RevisionNo");
        setRepairEstimationNo(MODE_EDIT);
        setPageID(oCallback.getReturnValue("EstimateId"));
        bindLineDetail(MODE_EDIT, oCallback.getReturnValue("EstimateId"), ""); // Added For Release-3
        HasChanges = false;
        HasPhotoUploadChanges = false;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function createRepairEstimate() {
    var oCallback = new Callback();
    oCallback.add("CustomerId", el("hdnCustomerId").value);
    oCallback.add("CustomerCd", el("txtCustomer").value);
    oCallback.add("EstimationDate", el("datEstimationDate").value);
    oCallback.add("OrginalEstimateDate", el("datOrginalEstimateDate").value);
    oCallback.add("StatusID", el("lkpStatus").SelectedValues[0]);
    oCallback.add("StatusCode", el("lkpStatus").SelectedValues[1]);
    //oCallback.add("EquipmentNo", el("hypEquipmentNo").innerText);
    //oCallback.add("EquipmentNo", getText(el('hypEquipmentNo')));
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("EirNo", el("hdnEirNo").value);
    oCallback.add("LastTestDate", el("datLastTestDate").value);
    if (el("lkpLastTestType").value != '') {
        oCallback.add("LastTestTypeID", el("lkpLastTestType").SelectedValues[0]);
        oCallback.add("LastTestTypeCode", el("lkpLastTestType").SelectedValues[1]);
    }
    oCallback.add("ValidityPeriodYear", el("txtValidityYear").value);
    oCallback.add("NextTestDate", el("datNextTestDate").value);
    oCallback.add("LastSurveyor", el("txtLastSurveyor").value);
    oCallback.add("NextTestTypeID", el('hdnTestTypeId').value);
    oCallback.add("NextTestTypeCode", el('txtNextTest').value);
    if (el("lkpRepairType").value != '') {
        oCallback.add("RepairTypeID", el("lkpRepairType").SelectedValues[0]);
        oCallback.add("RepairTypeCode", el("lkpRepairType").SelectedValues[1]);
    }
    oCallback.add("blnCertofCleanlinessBit", el("chkCertofCleanlinessBit").checked);
    if (el("lkpInvoiceparty").value == "") {
        el("lkpInvoiceparty").SelectedValues[0] = "";
        el("lkpInvoiceparty").SelectedValues[1] = "";
    }
    oCallback.add("InvoicingPartyID", el("lkpInvoiceparty").SelectedValues[0]);
    oCallback.add("InvoicingPartyCode", el("lkpInvoiceparty").SelectedValues[1]);
    oCallback.add("LaborRate", el("txtLaborRate").value);
    if (pMode == 'Repair Approval') {
        oCallback.add("ApprovalDate", el("datApprovalDate").value);
        oCallback.add("ApprovalRef", el("txtApprovalRef").value);
        oCallback.add("PartyApprovalRef", el('txtPartyRef').value);
    }
    else if (pMode == 'Survey Completion') {
        oCallback.add("SurveyDate", el("datSurveyDate").value);
        oCallback.add("SurveyName", el("txtSurveyorName").value);
    }
    oCallback.add("WFData", getWFDATA());
    oCallback.add("Mode", el("hdnMode").value);
    oCallback.add("EstimateID", el("hdnEstimateID").value);
    oCallback.add("RevisionNo", el("hdnRevisionNo").value);
    oCallback.add("Remarks", el("txtRemarks").value);
    oCallback.add("EstimationNo", el("hdnRprEstimationNo").value);
    //    oCallback.add("CustomerEstimatedCost", el('hypDepotCost').innerText);
    //    oCallback.add("CustomerApprovedCost", el('hypAppDepCost').innerText);
    oCallback.add("CustomerEstimatedCost", getText(el('hypDepotCost')));
    oCallback.add("CustomerApprovedCost", getText(el('hypAppDepCost')));

    oCallback.add("ActivityId", getQueryStringValue(document.location.href, "activityid"));

    oCallback.invoke("RepairEstimate.aspx", "CreateRepairEstimate");
    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        resetHasChanges("ifgLineDetail");
        if (oCallback.getReturnValue("EstimateId") != "")
            el("hdnEstimateID").value = oCallback.getReturnValue("EstimateId");
        if (oCallback.getReturnValue("RepairEstimationNo") != "")
            el("hdnRprEstimationNo").value = oCallback.getReturnValue("RepairEstimationNo");
        if (oCallback.getReturnValue("RevisionNo") != "")
            el("hdnRevisionNo").value = oCallback.getReturnValue("RevisionNo");
        setRepairEstimationNo(MODE_EDIT);
        setPageID(oCallback.getReturnValue("EstimateId"));
        bindLineDetail("MassInput", "", ""); // Added For Release-3
        HasChanges = false;
        HasPhotoUploadChanges = false;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

//This function is used submit the changes to the Server                                 
function updateRepairEstimate() {
    var oCallback = new Callback();
    if (getPageID() == "" || getPageID() == "0") {
        oCallback.add("RepairEstimateId", el("hdnEstimateID").value);
    }
    else {
        oCallback.add("RepairEstimateId", getPageID());
    }
    oCallback.add("CustomerId", el("hdnCustomerId").value);
    oCallback.add("CustomerCd", el("txtCustomer").value);
    oCallback.add("GateInDate", el("datGateInDate").value);
    oCallback.add("EstimationDate", el("datEstimationDate").value);
    oCallback.add("OrginalEstimateDate", el("datOrginalEstimateDate").value);
    oCallback.add("StatusID", el("lkpStatus").SelectedValues[0]);
    oCallback.add("StatusCode", el("lkpStatus").SelectedValues[1]);
    //    oCallback.add("EquipmentNo", el("hypEquipmentNo").innerText);
    //oCallback.add("EquipmentNo", getText(el('hypEquipmentNo')));
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("EirNo", el("hdnRprEstimationNo").value);
    oCallback.add("GateInTrnsactionNo", el("hdnEirNo").value);
    oCallback.add("LastTestDate", el("datLastTestDate").value);
    if (el("lkpLastTestType").value != '') {
        oCallback.add("LastTestTypeID", el("lkpLastTestType").SelectedValues[0]);
        oCallback.add("LastTestTypeCode", el("lkpLastTestType").SelectedValues[1]);
    }
    oCallback.add("ValidityPeriodYear", el("txtValidityYear").value);
    oCallback.add("NextTestDate", el("datNextTestDate").value);
    oCallback.add("LastSurveyor", el("txtLastSurveyor").value);
    oCallback.add("NextTestTypeID", el('hdnTestTypeId').value);
    oCallback.add("NextTestTypeCode", el('txtNextTest').value);
    if (el("lkpRepairType").value != '') {
        oCallback.add("RepairTypeID", el("lkpRepairType").SelectedValues[0]);
        oCallback.add("RepairTypeCode", el("lkpRepairType").SelectedValues[1]);
    }
    oCallback.add("blnCertofCleanlinessBit", el("chkCertofCleanlinessBit").checked);
    if (el("lkpInvoiceparty").value == "") {
        el("lkpInvoiceparty").SelectedValues[0] = "";
        el("lkpInvoiceparty").SelectedValues[1] = "";
    }
    oCallback.add("InvoicingPartyID", el("lkpInvoiceparty").SelectedValues[0]);
    oCallback.add("InvoicingPartyCode", el("lkpInvoiceparty").SelectedValues[1]);
    oCallback.add("LaborRate", el("txtLaborRate").value);
    if (pMode == 'Repair Approval') {
        oCallback.add("ApprovalDate", el("datApprovalDate").value);
        oCallback.add("ApprovalRef", el("txtApprovalRef").value);
        oCallback.add("PartyApprovalRef", el('txtPartyRef').value);
    }
    else if (pMode == 'Survey Completion') {
        oCallback.add("SurveyDate", el("datSurveyDate").value);
        oCallback.add("SurveyName", el("txtSurveyorName").value);
    }
    oCallback.add("WFData", getWFDATA());
    oCallback.add("Mode", el("hdnMode").value);
    if (el('hdnEstimateID').value == "0") {
        el("hdnEstimateID").value = tempEstimateId;
    }
    oCallback.add("EstimateID", el("hdnEstimateID").value);
    oCallback.add("RevisionNo", el("hdnRevisionNo").value);
    oCallback.add("Remarks", el("txtRemarks").value);
    oCallback.add("EstimationNo", el("hdnRprEstimationNo").value);
    //    oCallback.add("CustomerEstimatedCost", el('hypDepotCost').innerText);
    //    oCallback.add("CustomerApprovedCost", el('hypAppDepCost').innerText);
    oCallback.add("CustomerEstimatedCost", getText(el('hypDepotCost')));
    oCallback.add("CustomerApprovedCost", getText(el('hypAppDepCost')));
    // oCallback.invoke("RepairEstimate.aspx", "UpadteRepairEstimate");
    oCallback.invoke("RepairEstimate.aspx", "UpadteRepairEstimate");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        resetHasChanges("ifgLineDetail");
        if (oCallback.getReturnValue("EstimateId") != "" && oCallback.getReturnValue("EstimateId") != null)
            el("hdnEstimateID").value = oCallback.getReturnValue("EstimateId");
        if (oCallback.getReturnValue("RepairEstimationNo") != "" && oCallback.getReturnValue("RepairEstimationNo") != null)
            el("hdnRprEstimationNo").value = oCallback.getReturnValue("RepairEstimationNo");
        if (oCallback.getReturnValue("RevisionNo") != "" && oCallback.getReturnValue("RevisionNo") != null)
            el("hdnRevisionNo").value = oCallback.getReturnValue("RevisionNo");
        setRepairEstimationNo(MODE_EDIT);
        setPageID(oCallback.getReturnValue("RepairEstimateId"));
        bindLineDetail("MassInput", "", ""); // Added For Release-3
        HasChanges = false;
        HasPhotoUploadChanges = false;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function updateRepairEstimateGWS() {
    var oCallback = new Callback();
    if (getPageID() == "" || getPageID() == "0") {
        oCallback.add("RepairEstimateId", el("hdnEstimateID").value);
    }
    else {
        oCallback.add("RepairEstimateId", getPageID());
    }
    oCallback.add("CustomerId", el("hdnCustomerId").value);
    oCallback.add("CustomerCd", el("txtCustomer").value);
    oCallback.add("GateInDate", el("datGateInDate").value);
    oCallback.add("EstimationDate", el("datEstimationDate").value);
    oCallback.add("OrginalEstimateDate", el("datOrginalEstimateDate").value);
    oCallback.add("StatusID", el("lkpEquipStatusGWS").SelectedValues[0]);
    oCallback.add("StatusCode", el("lkpEquipStatusGWS").SelectedValues[1]);
    //    oCallback.add("EquipmentNo", el("hypEquipmentNo").innerText);
    //oCallback.add("EquipmentNo", getText(el('hypEquipmentNo')));
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("EirNo", el("hdnRprEstimationNo").value);
    oCallback.add("GateInTrnsactionNo", el("hdnEirNo").value);
    oCallback.add("LaborRate", el("txtLaborRate").value);
    oCallback.add("GateInTrnsactionNo", el("hdnEirNo").value);
    //    oCallback.add("ApprovalDate", el("datApprovalDate").value);
    //    oCallback.add("ApprovalRef", el("txtApprovalRef").value);
    //    oCallback.add("SurveyDate", el("datSurveyDate").value);
    //    oCallback.add("SurveyName", el("txtSurveyorName").value);
    oCallback.add("WFData", getWFDATA());
    oCallback.add("Mode", el("hdnMode").value);
    if (el('hdnEstimateID').value == "0") {
        el("hdnEstimateID").value = tempEstimateId;
    }

    if (pMode == 'Repair Approval') {
        oCallback.add("ApprovalDate", el("datApprovalDate").value);
        oCallback.add("ApprovalRef", el("txtApprovalRef").value);
    }

    oCallback.add("Consignee", el("txtConsignee").value);
    oCallback.add("EstimateID", el("hdnEstimateID").value);
    oCallback.add("RevisionNo", el("hdnRevisionNo").value);
    oCallback.add("Remarks", el("txtRemarks").value);
    oCallback.add("EstimationNo", el("hdnRprEstimationNo").value);
    //    oCallback.add("CustomerEstimatedCost", el('hypDepotCost').innerText);
    //    oCallback.add("CustomerApprovedCost", el('hypAppDepCost').innerText);
    oCallback.add("CustomerEstimatedCost", getText(el('hypDepotCost')));
    oCallback.add("CustomerApprovedCost", el("txtApprovalAmount").value);
    // oCallback.add("PartyApprovalRef", el('txtPartyRef').value);
    // oCallback.invoke("RepairEstimate.aspx", "UpadteRepairEstimate");

    oCallback.add("PrevONHLocation", el("lkpPrevONHLocation").SelectedValues[0]);
    oCallback.add("PrevONHLocationCode", el("lkpPrevONHLocation").SelectedValues[1]);
    oCallback.add("PrevONHLocDate", el("datPreviousONH").value);
    oCallback.add("Measure", el("lkpMeasure").SelectedValues[0]);
    oCallback.add("MeasureCode", el("lkpMeasure").SelectedValues[1]);

    oCallback.add("Unit", el("lkpUnit").SelectedValues[0]);
    oCallback.add("UnitCode", el("lkpUnit").SelectedValues[1]);
    if (el("lkpBillTo").SelectedValues[0] == "") {
        if (el("lkpBillTo").value = "AGENT")
            oCallback.add("BillTo", "144");
        else
            oCallback.add("BillTo", "145");
    }
    else {
        oCallback.add("BillTo", el("lkpBillTo").SelectedValues[0]);
    }
    oCallback.add("AgentName", el("hdnAgentId").value);

    oCallback.add("ActivityId", getQueryStringValue(document.location.href, "activityid"));
    oCallback.add("EquipmentStatusId", el("hdnEquipmentStatusId").value);
    oCallback.invoke("RepairEstimate.aspx", "UpdateRepairEstimateGWS");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        resetHasChanges("ifgLineDetail");
        if (oCallback.getReturnValue("EstimateId") != "" && oCallback.getReturnValue("EstimateId") != null)
            el("hdnEstimateID").value = oCallback.getReturnValue("EstimateId");
        if (oCallback.getReturnValue("RepairEstimationNo") != "" && oCallback.getReturnValue("RepairEstimationNo") != null)
            el("hdnRprEstimationNo").value = oCallback.getReturnValue("RepairEstimationNo");
        if (oCallback.getReturnValue("RevisionNo") != "" && oCallback.getReturnValue("RevisionNo") != null)
            el("hdnRevisionNo").value = oCallback.getReturnValue("RevisionNo");
        setRepairEstimationNo(MODE_EDIT);
        setPageID(oCallback.getReturnValue("RepairEstimateId"));
        bindLineDetail(MODE_EDIT, el("hdnEstimateID").value, "");  // Added For Release-3
        HasChanges = false;
        HasPhotoUploadChanges = false;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}


function bindLineDetail(mode, tariffId) {
    var objGrid = new ClientiFlexGrid("ifgLineDetail");
    objGrid.parameters.add("Mode", mode);
    objGrid.parameters.add("TariffId", tariffId);

}

//This function is used Bind Line Detail Grid with the corresponding mode                                 
function bindLineDetail(mode, tariffId, tariffGroupId) {
    var objGrid = new ClientiFlexGrid("ifgLineDetail");
    objGrid.parameters.add("Mode", mode);
    objGrid.parameters.add("EstimateId", el("hdnEstimateID").value);
    objGrid.parameters.add("TariffId", tariffId);
    if (tariffGroupId == undefined) {
        tariffGroupId = "";
    }
    objGrid.parameters.add("TariffGroupId", tariffGroupId);
    objGrid.parameters.add("ManHourRate", el("txtLaborRate").value);
    objGrid.parameters.add("PageName", el('hdnPageName').value);
    objGrid.parameters.add("WFData", getWFDATA());
    objGrid.DataBind();
}

//This function is used Bind Summary Detail Grid with the corresponding mode                                 
function bindSummaryDetail(mode) {
    if (getConfigSetting('061') != "True") {
        var objGrid = new ClientiFlexGrid("ifgSummaryDetail");
        //    objGrid.parameters.add("ServiceTax", el('hdnTaxRate').value); 
        if (!!mode)
            objGrid.parameters.add("Mode", mode);
        objGrid.parameters.add("WFData", getWFDATA());
        objGrid.DataBind();
    }
    else {
        var objGrid = new ClientiFlexGrid("ifgSummaryGWS");
        if (!!mode)
            objGrid.parameters.add("Mode", mode);
        if (getConfigSetting('063') == "True") {
            objGrid.parameters.add("TaxRate", el('txtTaxRate').value);
        }
        objGrid.parameters.add("WFData", getWFDATA());
        objGrid.parameters.add("PageName", el('hdnPageName').value);
        objGrid.DataBind();
    }
}


//This function is used to set the location value after selecting the value in the location lookup.
function setLocationValue(rIndex) {
    if (LocationCode != "") {
        if (rIndex != ifgLineDetail.CurrentRowIndex())
            rIndex = ifgLineDetail.Rows().Count - 1;
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(1, LocationCode);
    }
}

//This function is used to set the default values of the line detail grid.
function setDefaultValues(rIndex) {
    if (getConfigSetting('061') == "True" || getConfigSetting('065') == "True") {
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(5, "0.00");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(9, "0.00");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(7, el("txtLaborRate").value);
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(10, "0.00");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(8, "0.00");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(1, "");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(3, "");
        // ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(11, "");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(17, "");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(22, "");
    }
    else {
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(4, "0.00");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(8, "0.00");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(6, el("txtLaborRate").value);
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(9, "0.00");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(7, "0.00");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(1, "");
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(3, "");
        //   ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(11, "");
        //  ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(17, "");
        // ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(22, "");
    }

    if (getConfigSetting('061') != "True") {
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(10, new Array("C", "66"));
    }
    if (el('hdnPageName').value != 'Repair Estimate' && getConfigSetting('061') == "True") {
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(13, true);
    }
}

//function ifgLineDetailOnBeforeCB(mode,param) {
//    if (mode = "Insert") {
//        param.add("ManHourRate", el("txtLaborRate").value);     
//    }
//}

//This function is used to set the labor Rate in the detail Grid.
function setDefaultValuesOnLoad() {
    ifgLineDetail.Rows(0).SetColumnValuesByIndex(4, "0.00");
    ifgLineDetail.Rows(0).SetColumnValuesByIndex(8, "0.00");
    ifgLineDetail.Rows(0).SetColumnValuesByIndex(6, el("txtLaborRate").value);
    resetHasChanges("ifgLineDetail");
}

//This function is used to bind and calculate the summary Detail Grid.
function ifgSummaryDetailOnAfterCallBack(param, mode) {
    if (param["SummaryDetail"] != null) {
        eval(param["SummaryDetail"]);
    }
}

//This function is used to format all the Rate Fields to fixed.
function formatDecimalRate(obj) {
    var totalCost = 0;
    var icount = 0;
    var rIndex = ifgLineDetail.CurrentRowIndex();
    if (trimAll(obj.value) != "") {
        var Amount = new Number;
        Amount = parseFloat(obj.value);
        obj.value = Amount.toFixed(2);
    }
}


//This function is used to calculate the LHC, fire on labour hours change
function calculateLHCfromHours(obj) {
    formatDecimalRate(obj);
    var totalCost = new Number;
    var materialCost = new Number;
    var manHour = new Number;
    var manHourRate = new Number;
    var lHc = new Number;
    var rIndex = ifgLineDetail.CurrentRowIndex();
    if (getConfigSetting('061') == "True" || getConfigSetting('065') == "True") {
        var cColumns = ifgLineDetail.Rows(rIndex).GetClientColumns();
        //Man Hour
        if (cColumns[10] == "" || cColumns[10] == "null") {
            manHour = 0.00;
        }
        else {
            manHour = cColumns[10];
        }
        //Hours
        if (cColumns[12] == "" || cColumns[12] == "null") {
            manHourRate = 0.00;
        }
        else {
            manHourRate = cColumns[12];
        }

        lHc = parseFloat(manHour * manHourRate).toFixed(2);
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(8, lHc);


        //Material Cost
        if (cColumns[14] == "" || cColumns[14] == "null") {
            materialCost = 0.00;
        }
        else {
            materialCost = cColumns[14];
        }
        lHc = parseFloat(parseFloat(lHc) + parseFloat(materialCost)).toFixed(2);

        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(10, lHc);

    }
    else {
        var cColumns = ifgLineDetail.Rows(rIndex).GetClientColumns();
        //Man Hour
        if (cColumns[8] == "" || cColumns[8] == "null") {
            manHour = 0.00;
        }
        else {
            manHour = cColumns[8];
        }
        //Hours
        if (cColumns[10] == "" || cColumns[10] == "null") {
            manHourRate = 0.00;
        }
        else {
            manHourRate = cColumns[10];
        }

        lHc = parseFloat(manHour * manHourRate).toFixed(2);
        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(7, lHc);


        //Material Cost
        if (cColumns[12] == "" || cColumns[12] == "null") {
            materialCost = 0.00;
        }
        else {
            materialCost = cColumns[12];
        }
        lHc = parseFloat(parseFloat(lHc) + parseFloat(materialCost)).toFixed(2);

        ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(9, lHc);

    }
    if (getConfigSetting('063') == "True") {
        showTotalChargeByTaxRateChange();
    }
}

//This function is used to calculate the LHC, fire on Rate change
function calculateLHCfromRate(obj) {
    var rIndex = ifgLineDetail.CurrentRowIndex();
    if (trimAll(obj.value) != "") {
        var Amount = new Number;
        Amount = parseFloat(obj.value);
        obj.value = Amount.toFixed(2);
    }
}

function ifgLineDetailOnAfterCB(param, mode) {
    bindSummaryDetail(mode);
    if (getConfigSetting('063') == "True") {
        showTotalChargeByTaxRateChangeHdr();
    }
    var pageMode = getPageMode();
    pMode = el('hdnPageName').value;
    if (param["Error"] != null && param["Error"] != "")
        showErrorMessage(param["Error"])
    if (typeof (param["ApprovalAmount"]) != "undefined") {
        var approvalAmount = new Number;
        var exchangeRate = new Number;
        approvalAmount = parseFloat(param["ApprovalAmount"]);
        if (el('hdnPageName').value == 'Repair Estimate' && pageMode == MODE_NEW) {
            setText(el('hypOrginalCost'), approvalAmount.toFixed(2));
        }
        if (el('hdnExchangeRate').value == "") {
            exchangeRate = 0;
        }
        else {
            exchangeRate = el('hdnExchangeRate').value;
        }
        if (el('hdnDepotCurrencyCD').value == el('hdnToCurrencyCD').value) {
            exchangeRate = 1;
        }
        //        if (pMode == 'Repair Approval') {
        if (typeof (param["ApprovalAmount"]) != "undefined") {
            var repairApprovalAmount = new Number;
            repairApprovalAmount = parseFloat(param["ApprovalAmount"]);
            setText(el('hypAppCost'), repairApprovalAmount.toFixed(2));
            setText(el('hypAppDepCost'), parseFloat(repairApprovalAmount * exchangeRate).toFixed(2));
            if (pMode == 'Repair Approval' && getConfigSetting('065') == "True") {
                el('txtApprovalAmount').value = parseFloat(repairApprovalAmount).toFixed(2);
            }
        }
        if (pMode != 'Repair Approval') {
            setText(el('hypTotalCost'), approvalAmount.toFixed(2));
            setText(el('hypDepotCost'), parseFloat(approvalAmount * exchangeRate).toFixed(2));
        }
        //        }

        if (el('hdnPageName').value == 'Repair Estimate' && pageMode == MODE_NEW) {
            setText(el('hypOrgDepotCost'), parseFloat(approvalAmount * exchangeRate).toFixed(2));
        }

    }
}

//Show Approval Amount Based on Tax Text Event
function showApprovalAmount(obj, rIndex) {
    if (getConfigSetting('065') == "True") {
        var TOTAL_AMOUNT;
        var PREV_AMOUNT;
        var colscheck;
        var rowIndex = ifgLineDetail.CurrentRowIndex();
        colscheck = ifgLineDetail.Rows(rowIndex).GetClientColumns();
        if (obj.checked) {
            if (isNaN(parseFloat(colscheck[15]))) {
                TOTAL_AMOUNT = 0;
            } else {
                TOTAL_AMOUNT = parseFloat(colscheck[15]);
            }
            if (el('txtApprovalAmount').value != "") {
                PREV_AMOUNT = el('txtApprovalAmount').value;
                TOTAL_AMOUNT = parseFloat(TOTAL_AMOUNT) + parseFloat(PREV_AMOUNT);
            }
            el("txtApprovalAmount").value = parseFloat(TOTAL_AMOUNT).toFixed(2);
        }
        else {
            if (isNaN(parseFloat(colscheck[15]))) {
                TOTAL_AMOUNT = 0;
            } else {
                TOTAL_AMOUNT = parseFloat(colscheck[15]);
            }
            if ((el("txtApprovalAmount").value != "") && (el("txtApprovalAmount").value != "0.00")) {
                PREV_AMOUNT = el("txtApprovalAmount").value;
                TOTAL_AMOUNT = parseFloat(PREV_AMOUNT) - parseFloat(TOTAL_AMOUNT);

            }
            el("txtApprovalAmount").value = parseFloat(TOTAL_AMOUNT).toFixed(2);
        }
        if (el('hdnExchangeRate').value == "") {
            exchangeRate = 0;
        }
        else {
            exchangeRate = el('hdnExchangeRate').value;
        }
        if (el('hdnDepotCurrencyCD').value == el('hdnToCurrencyCD').value) {
            exchangeRate = 1;
        }
        setText(el('hypAppCost'), TOTAL_AMOUNT.toFixed(2));
        setText(el('hypAppDepCost'), parseFloat(TOTAL_AMOUNT * exchangeRate).toFixed(2));
    }
}

//This function is used to bind the summary detail grid when the Tax Rate Text Box changed.
function onTaxRateChange(obj) {
    if (trimAll(obj.value) != "") {
        var Amount = new Number;
        Amount = parseFloat(obj.value);
        obj.value = Amount.toFixed(2);
    }
    bindSummaryDetail("ReBind");
    el('txtAppAmount').value = "";
}

//This function is used to apply Tariff filter for the tariff lookup
function applyTariffFilter() {
    if (trimAll(el("lkpEqpSize").value) == "") {
        setFocusToField("lkpEqpSize");
        showErrorMessage("Equipment Size Required.")
        return false;
    }
    var rIndex = ifgLineDetail.CurrentRowIndex();
    var cColumns = ifgLineDetail.Rows(rIndex).GetClientColumns();
    if (cColumns[2] == "") {
        setTimeout(function () { ifgLineDetail.Rows(ifgLineDetail.CurrentRowIndex()).SetFocusInColumn(1); }, 10);
        //showErrorMessage("Location Code Required.")
        //return;
    }
    var str = applyDepoFilter() + " AND EQPMNT_TYP = '" + el("txtEqpTyp").value + "' AND CSTMR_CD='" + trimAll(el("txtCustomer").value) + "' AND EQPMNT_SZ='" + trimAll(el("lkpEqpSize").value) + "' AND CMP_CD='" + cColumns[0] + "' AND RPR_CD ='" + cColumns[6] + "' AND DMG_CD='" + cColumns[4] + "'";
    return str;
}

//This function is used to set the Header title along with the Estimation and Revision no.
function setEstimationNo(sMode) {
    var strHeader;
    if (sMode == MODE_NEW) {
        strHeader = "Creation";
    }
    else {
        strHeader = "Revision";
    }
    el('spnEstNo').innerHTML = "Estimate/" + strHeader + " # - <span class='ylbl'>" + el('hdnRprEstimationNo').value + " / " + el('hdnRevisionNo').value + "</span>";
    el('spnPageTitle').innerHTML = getQueryStringValue(document.location.href, "activityname");
}

function setRepairEstimationNo(sMode) {
    var strRevisionNo;
    var strHeader;
    if (el('hdnRevisionNo').value == "") {
        strRevisionNo = "0";
    }
    else {
        strRevisionNo = el('hdnRevisionNo').value;
    }
    //var sMode = getPageMode();
    if (sMode == MODE_NEW) {
        strHeader = "Creation";
    }
    else {
        strHeader = "Revision";
    }
    el('spnEstNo').innerHTML = "Estimate/" + strHeader + " # - <span class='ylbl'>" + el('hdnRprEstimationNo').value + " / " + strRevisionNo + "</span>";
    el('spnPageTitle').innerHTML = getQueryStringValue(document.location.href, "activityname");
}

//This function is used to show/hide the survey div depending upon the page mode.
function toggleApprovalSurveyDiv(mode) {
    if (pMode == 'Repair Approval') {
        setFocusToField("datApprovalDate");
        var totalCost = new Number;
        var exchangeRate = new Number;
        showDiv('divApproval');
        showDiv('divAppCostSummary');
        hideDiv('divSurvey');
        hideDiv('divlineDetail');
        setReadOnly('datEstimationDate', true);
        if (getConfigSetting('065') != "True") {
            setReadOnly('lkpLastTestType', true);
            setReadOnly('datLastTestDate', true);
            setReadOnly('txtLastSurveyor', true);
        }
        //        setReadOnly('lkpRepairType', true);
        //        setReadOnly('chkCertofCleanlinessBit', true);
        //        el('lblAppCurrency').innerText = el('hdnDepotCurrencyCD').value;
        //        el('lblAppDepCurrency').innerText = el('hdnToCurrencyCD').value;
        setText(el('lblAppCurrency'), el('hdnDepotCurrencyCD').value);
        setText(el('lblAppDepCurrency'), el('hdnToCurrencyCD').value);

        //if (el('hypAppCost').innerText == "") {
        if (getText(el('hypAppCost')) == "") {
            totalCost = 0;
        }
        else {
            //totalCost = el('hypAppCost').innerText;
            totalCost = getText(el('hypAppCost'));
        }
        if (el('hdnExchangeRate').value == "") {
            exchangeRate = 0;
        }
        else {
            exchangeRate = el('hdnExchangeRate').value;
        }
        if (el('hdnDepotCurrencyCD').value == el('hdnToCurrencyCD').value) {
            exchangeRate = 1;
        }
        setText(el('hypDepotCost'), parseFloat(totalCost * exchangeRate).toFixed(2));
    }
    else if (pMode == 'Repair Estimate') {
        setFocusToField("datEstimationDate");
        setReadOnly('datEstimationDate', false);
        if (getConfigSetting('061') != "True") {
            //   hideDiv('divApproval');
            hideDiv('divAppCostSummary');
            hideDiv('divSurvey');
            hideDiv('divlineDetail');
            setReadOnly('chkCertofCleanlinessBit', true);
        }
    }
    else if (pMode == 'Survey Completion') {
        setReadOnly('datEstimationDate', true);
        setReadOnly('lkpLastTestType', true);
        setReadOnly('datLastTestDate', true);
        setReadOnly('txtLastSurveyor', true);
        setReadOnly('lkpRepairType', true);
        //   el('rbtnCustomerBit').disabled = true;
        //    el('rbtnPartyBit').disabled = true;
        setFocusToField("txtSurveyorName");
        showDiv('divSurvey');
        setReadOnly('datEstimationDate', true);
        //hideDiv('divApproval');

        hideDiv('divTariff');
        hideDiv('divPhotoUpload');
        showDiv('divAppCostSummary');
        setReadOnly('chkCertofCleanlinessBit', true);
        //if (el('hypAppCost').innerText == "") {
        if (getText(el('hypAppCost')) == "") {
            totalCost = 0;
        }
        else {
            //totalCost = el('hypAppCost').innerText;
            totalCost = getText(el('hypAppCost'));
        }
        if (el('hdnExchangeRate').value == "") {
            exchangeRate = 0;
        }
        else {
            exchangeRate = el('hdnExchangeRate').value;
        }
        if (el('hdnDepotCurrencyCD').value == el('hdnToCurrencyCD').value) {
            exchangeRate = 1;
        }
        setText(el('hypDepotCost'), parseFloat(totalCost * exchangeRate).toFixed(2));
    }
    setActivityTitle();
}
//This function is used to set the Activity Title.

function setActivityTitle() {
    //    el('spnPageTitle').innerHTML = getQueryStringValue(document.location.href, "activityname");
}

//This function is used to open the more info popup
function openMoreInfo(_dtlBin) {
    if (ifgLineDetail.Submit(true) == false) {
        return false;
    }
    showModalWindow("Description", "Operations/AddNotes.aspx?DetailBin=" + _dtlBin, "500px", "100px", "", "", "");
    psc().hideLoader();
}


function validateEstimationDate(oSrc, args) {
    if (args.Value != el("datGateInDate").value) {
        if (DateCompare(args.Value, el("datGateInDate").value)) {
            args.IsValid = true;
            return;
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Date should be greater than or equal to In Date";
            return;
        }
    }
}

function ValidatePreviousActivity(oSrc, args) {
    // var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    if (args.Value == "") {
        oSrc.errormessage = el('hdnPageName').value + " Date Required";
        args.IsValid = false;
        return;
    }

    var oCallback = new Callback();
    //oCallback.add("EquipmentNo", el('hypEquipmentNo').innerText);
    //oCallback.add("EquipmentNo", getText(el('hypEquipmentNo')));
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("EventDate", args.Value);
    oCallback.add("PageName", el('hdnPageName').value);
    oCallback.invoke("RepairEstimate.aspx", "ValidatePreviousActivityDate");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("Error") != "") {
            args.IsValid = false;
            oSrc.errormessage = oCallback.getReturnValue("Error");
        }
        else {
            args.IsValid = true;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}


function validateOwnerEstimationDate(oSrc, args) {
    if (args.Value != el("datEstimationDate").value) {
        if (DateCompare(args.Value, el("datEstimationDate").value)) {
            args.IsValid = true;
            return;
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Owner Estimate Date should be greater than or equal to Estimation Date";
            return;
        }
    }
}



//This function is used to validate the estimation date
function validateSurveyDate(oSrc, args) {
    if (args.Value != el("datEstimationDate").value) {
        if (DateCompare(args.Value, el("datEstimationDate").value)) {
            args.IsValid = true;
            return;
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Survey Date should be greater than or equal to Estimation Date";
            return;
        }
    }
}

//This function is used to Print the Estimate.
function printEstimate() {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "RepairEstimate";
    oDocPrint.Type = "document";
    if (RMode == 'LA') {
        oDocPrint.Title = "Repair Estimate - Lessee Approval";
    }
    else if (RMode == 'OA' || RMode == 'REA') {
        oDocPrint.Title = "Repair Estimate - Owner Authorization";
    }
    else if (RMode == 'SC') {
        oDocPrint.Title = "Survey Completion";
    }
    else {
        oDocPrint.Title = "Repair Estimate";
    }
    oDocPrint.DocumentId = "7";
    oDocPrint.ReportPath = "../Documents/Report/Estimate.rdlc";
    oDocPrint.add("EstimateID", el("hdnEstimateID").value);
    oDocPrint.openReportDialog();
}


//FOR CHECK OPTION BOX: INVOICE PARTY
function selectInvoicingParty(obj) {
    if (obj.checked) {
        el('divInvoicingParty').style.display = "block";
        HasChanges = true;
    }
}

//FOR CHECK OPTION BOX: CUSTOMER
function selectCustomer(obj) {
    if (obj.checked) {
        clearTextValues("lkpInvoiceparty");
        el('lkpInvoiceparty').SelectedValues[0] = "";
        el('divInvoicingParty').style.display = "none";
        HasChanges = true;
    }
}

//FOR FILTER FOR SUB ITEM WHILE SELECTING ITEM IN LINE DETAIL GRID
function applySubItemFilter() {
    var rIndex = ifgLineDetail.CurrentRowIndex();
    var cColumns = ifgLineDetail.Rows(rIndex).GetClientColumns();
    return "ITM_ID IN ('" + cColumns[1] + "')  AND ACTV_BT=1";
}

//FOR STATUS FILTER
function applyWorkFlowStatusFilter() {
    return " WF_ACTIVITY_NAME='Repair Estimate' GROUP BY EQPMNT_STTS_CD,EQPMNT_STTS_ID,EQPMNT_STTS_DSCRPTN_VC,EQUIPMENT_TYPE,EQUIPMENT_CODE,DPT_ID,ACTV_BT";
}

function bindResponsility(obj) {
    //    if (obj != '0') {
    //        el("rbtnPartyBit").checked = true;
    //        el('divInvoicingParty').style.display = "block";
    //    }
    //    else {
    //        el("rbtnCustomerBit").checked = true;
    //        clearTextValues("lkpInvoiceparty");
    //        el('lkpInvoiceparty').SelectedValues[0] = "";
    //        el('divInvoicingParty').style.display = "none";
    //    }
}

function calculateNextTestDate() {
    var months = new Array(12);
    months[0] = "JAN";
    months[1] = "FEB";
    months[2] = "MAR";
    months[3] = "APR";
    months[4] = "MAY";
    months[5] = "JUN";
    months[6] = "JUL";
    months[7] = "AUG";
    months[8] = "SEP";
    months[9] = "OCT";
    months[10] = "NOV";
    months[11] = "DEC";
    var LstTstDate = new Date();
    var result = "";
    if (el('datLastTestDate').value != "") {
        var _arr = el('datLastTestDate').value.split('-');
        var dt = _arr[1] + ',' + _arr[0] + ',' + _arr[2];
        var newDate = new Date(dt);
        newDate.setMonth(newDate.getMonth() + 29);
        var nxtTestDate = newDate.getDate() + "-" + months[newDate.getMonth()] + "-" + newDate.getFullYear(); //Changed by Sakthivel on 14-OCT-2014 for UIG Changes(Year returing invalid year)
        el('datNextTestDate').value = nxtTestDate;
    }
    else {
        el('datNextTestDate').value = "";
    }
    HasChanges = true;
}

function bindTestType() {
    var _arr = new Array();
    if (el('lkpLastTestType').value == "PNEUMATIC") {
        _arr[0] = "51";
        _arr[1] = "HYDRO";
        el('txtValidityYear').value = "2.5";
    }
    else if (el('lkpLastTestType').value == "HYDRO") {
        _arr[0] = "52";
        _arr[1] = "PNEUMATIC";
        el('txtValidityYear').value = "2.5";
    }
    else if (el('lkpLastTestType').value == "") {
        _arr[0] = "";
        _arr[1] = "";
        el('txtValidityYear').value = "";
    }
    el('hdnTestTypeId').value = _arr[0];
    el('txtNextTest').value = _arr[1];
}

function generateWorkOrder() {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "Repair Work Order";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Repair Work Order";
    //GWS
    if (getConfigSetting('058') == "True") {
        oDocPrint.DocumentId = "43";
        oDocPrint.ReportPath = "../Documents/Report/RepairWorkOrderGWC.rdlc";
    }
    else {
        oDocPrint.DocumentId = "19";
        oDocPrint.ReportPath = "../Documents/Report/RepairWorkOrder.rdlc";
    }
    if (el("hdnEstimateID").value == "0") {
        el("hdnEstimateID").value = tempEstimateId;
    }
    else if (el("hdnEstimateID").value == "") {
        showErrorMessage("Submit the Repair Estimate before printing Work Order");
        return false;
    }
    oDocPrint.add("EstimateID", el("hdnEstimateID").value);
    oDocPrint.add("wfData", el("WFDATA").value);
    oDocPrint.openReportDialog();
}

function generateRepairEstimate() {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "Repair Estimate";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Repair Estimate";
    if (getConfigSetting('071') == "True") {
        oDocPrint.DocumentId = "47";
        oDocPrint.ReportPath = "../Documents/Report/RepairEstimateAcacia.rdlc";
    }
    else if (getConfigSetting('058') == "True") {
        oDocPrint.DocumentId = "45";
        oDocPrint.ReportPath = "../Documents/Report/RepairEstimateGWC.rdlc";
    }
    else {
        oDocPrint.DocumentId = "20";
        oDocPrint.ReportPath = "../Documents/Report/RepairEstimate.rdlc";
    }
    if (el("hdnEstimateID").value == "0") {
        el("hdnEstimateID").value = tempEstimateId;
    }
    else if (el("hdnEstimateID").value == "") {
        showErrorMessage("Submit the Repair Estimate before printing Report");
        return false;
    }
    oDocPrint.add("EstimateID", el("hdnEstimateID").value);
    oDocPrint.add("wfData", el("WFDATA").value);
    oDocPrint.openReportDialog();
}


function filterTariffCode() {
    if (el('lkpTariffGroup').value != '') {
        return "TRFF_CD_ID IN (SELECT TRFF_CD_ID FROM TARIFF_GROUP_DETAIL WHERE TRFF_GRP_ID IN (SELECT TRFF_GRP_ID FROM TARIFF_GROUP WHERE TRFF_GRP_CD= '" + el('lkpTariffGroup').value + "') AND ACTV_BT=1)";
    }
}

function tariffBindLineDetail() {
    Page_ClientValidate('divHeader');
    if (getConfigSetting('061') != "True") {
        if (el('lkpTariffGroup').value == "" && el('lkpTariffCode').value == "") {
            showErrorMessage('Select Tariff Group Code (or) Tariff Code');
        }
        else {
            GetLookupChanges();
            Page_ClientValidate('divHeader');
            if (!Page_IsValid) {
                return false;
            }
            if (el('lkpTariffGroup').value == "") {
                clearLookupValues('lkpTariffGroup');
            }
            if (el('lkpTariffCode').value == "") {
                clearLookupValues('lkpTariffCode');
            }

            bindLineDetail("FETCH", el('lkpTariffCode').SelectedValues[0], el('lkpTariffGroup').SelectedValues[0]);
            ifgLineDetail.Submit(true);
            bindSummaryDetail("ReBind");
            HasChanges = true;
            calculateTotalSummary();
        }
    }
    else {
        if (el('lkpCustomerTariff').value == "" && el('lkpCustomerTariff').value == "") {
            showErrorMessage('Select Tariff Code');
        }
        else {
            GetLookupChanges();
            Page_ClientValidate('divHeader');
            if (!Page_IsValid) {
                return false;
            }
            if (el('lkpCustomerTariff').value == "") {
                clearLookupValues('lkpCustomerTariff');
            }
            if (el('lkpCustomerTariff').value == "") {
                clearLookupValues('lkpCustomerTariff');
            }

            bindLineDetail("FETCH", el('lkpCustomerTariff').SelectedValues[0], el('lkpCustomerTariff').SelectedValues[0]);
            ifgLineDetail.Submit(true);
            bindSummaryDetail("ReBind");
            HasChanges = true;
            calculateTotalSummary();
        }
    }
}

function calculateTotalSummary() {
    var pageMode = getPageMode();
    var rowCount = ifgLineDetail.Rows().Count;
    var cCols = "";
    var gridTotalAmount = new Number;
    var totalAmount = new Number;
    if (rowCount > 0) {
        for (i = 0; i < rowCount; i++) {
            cCols = ifgLineDetail.Rows(i).GetClientColumns();
            if (cCols[13] == "") {
                gridTotalAmount = 0;
            }
            else {
                gridTotalAmount = cCols[13];
            }
            totalAmount = parseFloat(totalAmount) + parseFloat(gridTotalAmount);
        }
        if (el('hdnExchangeRate').value == "") {
            exchangeRate = 0;
        }
        else {
            exchangeRate = el('hdnExchangeRate').value;
        }
        if (el('hdnDepotCurrencyCD').value == el('hdnToCurrencyCD').value) {
            exchangeRate = 1;
        }
        if (el('hdnPageName').value != 'Repair Approval') {
            setText(el('hypTotalCost'), parseFloat(totalAmount).toFixed(2));
            setText(el('hypDepotCost'), parseFloat(totalAmount * exchangeRate).toFixed(2));
        }
        else {
            setText(el('hypAppCost'), totalAmount.toFixed(2));
            setText(el('hypAppDepCost'), parseFloat(totalAmount * exchangeRate).toFixed(2));
        }
        if (el('hdnPageName').value == 'Repair Estimate') {  // && pageMode == MODE_NEW
            setText(el('hypOrginalCost'), parseFloat(totalAmount).toFixed(2));
            setText(el('hypOrgDepotCost'), parseFloat(totalAmount * exchangeRate).toFixed(2));
        }
    }

}

function showLeakTest() {
    // showModalWindow("Leak Test", "Operations/LeakTest.aspx?GateinTransaction=" + el("hdnEirNo").value + "&EquipmentNo=" + el("hypEquipmentNo").innerText, "950px", "250px", "100px", "", "");
    //showModalWindow("Leak Test", "Operations/LeakTest.aspx?GateinTransaction=" + el("hdnEirNo").value + "&EquipmentNo=" + getText(el('hypEquipmentNo')), "950px", "250px", "100px", "", "");
    showModalWindow("Leak Test", "Operations/LeakTest.aspx?GateinTransaction=" + el("hdnEirNo").value + "&EquipmentNo=" + el("txtEquipmentNo").value, "950px", "250px", "100px", "", "");
    psc().hideLoader();
}

function showEstimateSummary() {
    showModalWindow("Line Detail Summary", "Operations/RepairEstimateSummary.aspx?", "560px", "200px", "100px", "", "");
    psc().hideLoader();
}

function showEquipmentDetail() {
    //showModalWindow("Equipment Detail", "Operations/RepairEstimateEqDetail.aspx?EquipmentNo=" + el('hypEquipmentNo').innerText + "&GateinTransaction=" + el('hdnEirNo').value, "300px", "300px", "100px", "", "");
    //showModalWindow("Equipment Detail", "Operations/RepairEstimateEqDetail.aspx?EquipmentNo=" + getText(el('hypEquipmentNo')) + "&GateinTransaction=" + el('hdnEirNo').value, "300px", "300px", "100px", "", "");
    showModalWindow("Equipment Detail", "Operations/RepairEstimateEqDetail.aspx?EquipmentNo=" + el("txtEquipmentNo").value + "&GateinTransaction=" + el('hdnEirNo').value, "300px", "300px", "100px", "", "");
    psc().hideLoader();
}

function updateApprovalAmount(obj, rowIndex) {
    var totalAmount = new Number;
    var approvalAmount = new Number;
    var exchangeRate = new Number;
    var selAmount = new Number;
    //approvalAmount = parseFloat(el('hypAppCost').innerText);
    approvalAmount = getText(el('hypAppCost'));
    var rIndex = ifgLineDetail.VirtualCurrentRowIndex();
    var cColumns = ifgLineDetail.Rows(rowIndex).GetClientColumns();
    if (cColumns[14] == "") {
        selAmount = 0;
    }
    else {
        selAmount = cColumns[14];
    }
    totalAmount = parseFloat(selAmount);
    if (obj.checked) {
        totalAmount = parseFloat(approvalAmount) + parseFloat(totalAmount);
        setText(el('hypAppCost'), parseFloat(totalAmount).toFixed(2));
    }
    else {
        totalAmount = approvalAmount - totalAmount;
        setText(el('hypAppCost'), parseFloat(totalAmount).toFixed(2));
    }

    if (el('hdnExchangeRate').value == "") {
        exchangeRate = 0;
    }
    else {
        exchangeRate = el('hdnExchangeRate').value;
    }
    if (el('hdnDepotCurrencyCD').value == el('hdnToCurrencyCD').value) {
        exchangeRate = 1;
    }
    setText(el('hypAppDepCost'), parseFloat(totalAmount * exchangeRate).toFixed(2));
    ifgLineDetail.Submit(true);
}

function showPhotoUpload() {
    //Added for Attachment
    var EstimateID;
    var subName;
    EstimateID = el("hdnEstimateID").value;
    if (el("hdnEstimateID").value == "") {
        var oCallback = new Callback();
        oCallback.add("GITransaction", el("hdnGITransaction").value);
        //oCallback.add("EquipmentNo", el("hypEquipmentNo").innerText);
        oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
        //             oCallback.invoke("RepairEstimate.aspx", "ValidateGateINAttachment");
        //            if (oCallback.getReturnValue("Message") == "Yes") {
        //                EstimateID = oCallback.getReturnValue("GateInID");
        //            }

    }
    //Attachment
    if (el('hdnPageName').value == "Repair Estimate") {
        subName = "Repair Estimate";
    }
    else {

        if (el("hdnMode").value == "new") {
            subName = "Repair Estimate";
        }
        else {
            subName = "Repair Approval";
        }
    }
    //showModalWindow("Attach Files", "Operations/PhotoUpload.aspx?RepairEstimateId=" + el("hdnEstimateID").value + "&PageName=" + el('hdnPageName').value, "850px", "500px", "150px", "", "");
    showModalWindow("Attach Files", "Operations/PhotoUpload.aspx?RepairEstimateId=" + EstimateID + "&PageName=" + el('hdnPageName').value + "&SubName=" + subName, "850px", "500px", "150px", "", "");
    psc().hideLoader();
}

function onClearTariffCode() {
    clearLookupValues("lkpTariffCode");
    el('lkpTariffCode').SelectedValues[0] = "";
}

//Release - 3
function onClickMassApplyInput() {
    if (ifgLineDetail.Submit(true) == false || _RowValidationFails) {
        return false;
    }
    var oCallback = new Callback();
    oCallback.invoke("RepairEstimate.aspx", "checkSelectBit");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("blnSelect") == "TRUE") {
            var cCols = ifgLineDetail.Rows(ifgLineDetail.CurrentRowIndex()).Columns();
            //showModalWindow("Mass Apply Input - " + el('hypEquipmentNo').innerText, "Operations/RepairEstimateMassApplyInput.aspx?RepairEstimateDetailId=" + cCols[1] + "&RepairEstimateId=" + getPageID() + " ", "450px", "250px", "120px", "", "");
            //showModalWindow("Mass Apply Input - " + getText(el('hypEquipmentNo')), "Operations/RepairEstimateMassApplyInput.aspx?RepairEstimateDetailId=" + cCols[1] + "&RepairEstimateId=" + getPageID() + " ", "450px", "250px", "120px", "", "");
            showModalWindow("Mass Apply Input - " + el("txtEquipmentNo").value, "Operations/RepairEstimateMassApplyInput.aspx?RepairEstimateDetailId=" + cCols[4] + "&RepairEstimateId=" + getPageID() + " ", "450px", "250px", "120px", "", "");
            psc().hideLoader();
        }
        else {
            showErrorMessage("Please Select Atleast One Line Detail");
        }
    }
    HasChanges = false;
    oCallback = null;

}

function validateInvoicingParty() {
    var oCallback = new Callback();
    oCallback.add("InvoicingParty", el('lkpInvoiceparty').value);
    oCallback.invoke("RepairEstimate.aspx", "validateInvoicingParty");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("LineRes") == "TRUE") {
            showErrorMessage("Please select Invoicing Party, as one of the Repair Item “Res” is indicated as I");
            return false;
        }
        else if (oCallback.getReturnValue("HeaderParty") == "TRUE") {
            showErrorMessage("Please remove Invoicing Party, as no Line items has “Res” as “I”");
            return false;
        }
        else {
            return true;
        }
    }
    HasChanges = false;
    oCallback = null;
}

function validatePartyRefence(oSrc, args) {
    if (el('lkpInvoiceparty').value != "" && args.Value == "") {
        args.IsValid = false;
        oSrc.errormessage = "Party Approval Reference Required";
        return;
    }
    else {
        args.IsValid = true;
        return;
    }
}


function onTextInvoiceParty() {
    clearTextValues("txtPartyRef");
}

try {
    $(window.parent).resize(function () {
        reSizePane();
    });
} catch (e) { }

function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent) != undefined) {
            if ($(window.parent).height() < 680) {
                el("tabEstimate").style.height = $(window.parent).height() - 242 + "px";
            }
            else if ($(window.parent).height() < 768) {
                el("tabEstimate").style.height = $(window.parent).height() - 243 + "px";
            }
            else {
                el("tabEstimate").style.height = $(window.parent).height() - 234 + "px";
                if (el("ifgLineDetail") != null) {
                    el("ifgLineDetail").SetStaticHeaderHeight($(window.parent).height() - 655 + "px");
                }
            }
        }
    }
}
function filterCustomerTariffCode() {
    //    fnLoadTariff("nothing")
    //    if (el('hdnTariffCodeID').value != "") {
    //        return " TRFF_CD_ID = " + el('hdnTariffCodeID').value;
    //    }
    //    else {
    //        return " TRFF_CD_ID = 0"
    //    }

    if (el("lkpBillTo").value == "CUSTOMER") {
        return "CUSTOMER," + el("hdnCustomerId").value + "," + el("hdnEqpTypID").value;
    }

    if (el("lkpBillTo").value == "AGENT") {
        return "AGENT," + el("hdnAgentId").value + "," + el("hdnCustomerId").value + "," + el("hdnEqpTypID").value;
    }

    return false;

}

function ShowConfirmationMessage(obj) {
    if (el("hdnBillTo").value != obj.value) {
        showConfirmMessage("Bill To value is changed, which would result in changes of Tariff and Line Details. Do you want to continue?",
                     "wfs().yesClick();", "wfs().noClick();");
    }
}

function yesClick() {

    //    setText(el('hypOrginalCost'), "0.00");
    //    setText(el('hypOrgDepotCost'), "0.00");
    resetValidators();
    setText(el('hypTotalCost'), "0.00");
    setText(el('hypDepotCost'), "0.00");

    setText(el('lblTotEstimatedAmount'), "0.00");
    setText(el('lblTotMaterialCost'), "0.00");
    setText(el('lblTotLabrCost'), "0.00");



    ConfirmYesOnClick();
    el("hdnBillTo").value = el("lkpBillTo").value;
    return true;
}

function noClick() {
    return false;
}

function validateBillTo(oSrc, args) {
    if (el('txtAgentName').value == "") {
        if (el("lkpBillTo").SelectedValues[0] != "145") {
            args.IsValid = false;
            oSrc.errormessage = "Please select only Customer";
        }
    }

}
function ConfirmYesOnClick() {
    clearLookupValues("lkpCustomerTariff");
    el('lkpCustomerTariff').SelectedValues[0] = "";
    fnLoadTariff("clear");
}
function UpdateTotalCostGrid(objGrid) {
    var totEstAmt = 0;
    var totMatAmt = 0;
    var totLabrCost = 0;
    var servTax = 0;
    var totalAmount = 0;
    for (i = 0; i < objGrid.rows.length; i++) {
        var node = objGrid.rows[i].cells[3].childNodes[3]; //textbox
        if (node != undefined) //check only textbox, ignore empty one
            if (!isNaN(node.value) && node.value != "") //check for valid number
                totMatAmt += parseInt(node.value);
    }
    for (i = 0; i < objGrid.rows.length; i++) {
        var node = objGrid.rows[i].cells[4].childNodes[3]; //textbox
        if (node != undefined) //check only textbox, ignore empty one
            if (!isNaN(node.value) && node.value != "") //check for valid number
                totEstAmt += parseInt(node.value);
    }
    for (i = 0; i < objGrid.rows.length; i++) {
        var node = objGrid.rows[i].cells[2].childNodes[3]; //textbox
        if (node != undefined) //check only textbox, ignore empty one
            if (!isNaN(node.value) && node.value != "") //check for valid number
                totLabrCost += parseInt(node.value);
    }
    servTax = (el('txtTaxRate').value * totEstAmt) / 100;
    totalAmount = servTax + totEstAmt;
    setText(el('txtTotEstimatedAmount'), totEstAmt);
    setText(el('txtTotMaterialCost'), totEstAmt);
    setText(el('txtTotLaborCost'), totLabrCost);
    setText(el('txtServiceTax'), servTax);
    setText(el('txtTotalEstimatedAmount'), totalAmount);
}

function ifgSummaryDetailGWSOnAfterCallBack(param, mode) {
    if (getConfigSetting('061') == "True" || getConfigSetting('065') == "True") {
        var totEstCost = 0;
        var totMatCost = 0;
        var totLabrCost = 0;
        var icount = 0;

        //    if (typeof (ifgParamValue) == "object") {

        //        if (ifgSummaryGWS.Submit(true) == false) {
        //            return false;
        //        }
        //        else {

        icount = ifgSummaryGWS.Rows().Count;
        if (icount > 0) {
            for (i = 0; i < icount; i++) {
                cColumns = ifgSummaryGWS.Rows(i).GetClientColumns();
                totEstCost = parseFloat(totEstCost) + parseFloat(cColumns[4]);
                totMatCost = parseFloat(totMatCost) + parseFloat(cColumns[3]);
                totLabrCost = parseFloat(totLabrCost) + parseFloat(cColumns[2]);
            }
        }

        var exchangeRate = new Number;
        if (el('hdnExchangeRate').value == "") {
            exchangeRate = 0;
        }
        else {
            exchangeRate = el('hdnExchangeRate').value;
        }
        if (el('hdnDepotCurrencyCD').value == el('hdnToCurrencyCD').value) {
            exchangeRate = 1;
        }


        setText(el('lblTotEstimatedAmount'), parseFloat(totEstCost).toFixed(2));
        setText(el('lblTotMaterialCost'), parseFloat(totMatCost).toFixed(2));
        setText(el('lblTotLabrCost'), parseFloat(totLabrCost).toFixed(2));
        //    setText(el('hypDepotCost'), parseFloat(totEstCost * exchangeRate).toFixed(2));
        //    setText(el('hypDepotCost'), parseFloat(totEstCost * exchangeRate).toFixed(2));
        //        }
        //    }
    }
}

function formatCostRate(value) {
    var Amount = new Number;
    if (isNaN(parseFloat(value))) {
        value = 0;
    }
    return value.toFixed(2);
}

function formatTaxRate() {
    var Amount = new Number;
    if (isNaN(parseFloat(el("txtTaxRate").value))) {
        el("txtTaxRate").value = 0;
    }
    Amount = parseFloat(el("txtTaxRate").value);
    el("txtTaxRate").value = Amount.toFixed(2);
    showTotalChargeByTaxRateChangeHdr();
    bindSummaryDetail("ReBind");
}

function applyWorkFlowStatusFilterGWS() {
    var pMode = el('hdnPageName').value;
    if (pMode == 'Repair Estimate') {
        return " STTS_ID=4 ";
    }
    else if (pMode == 'Repair Approval') {
        return " STTS_ID IN (10,21) ";
    }
}

function fnLoadTariff(gridClearMode) {
    var oCallback = new Callback();
    oCallback.add("CustomerId", el("hdnCustomerId").value);
    oCallback.add("AgentId", el("hdnAgentId").value);
    if (el("lkpBillTo").SelectedValues[1] == "") {
        oCallback.add("BillTo", el("lkpBillTo").value);
    }
    else {
        oCallback.add("BillTo", el("lkpBillTo").SelectedValues[1]);
    }
    oCallback.add("EquipType", el("hdnEqpTypID").value);
    oCallback.invoke("RepairEstimate.aspx", "fetchCustomerTariff");
    if (oCallback.getReturnValue("TariffCodeId") != "")
        el("hdnTariffCodeID").value = oCallback.getReturnValue("TariffCodeId");
    if (getConfigSetting('063') == "True") {
        el("lblServTaxCurr").value = oCallback.getReturnValue("ServiceTax");
    }
    setText(el('txtLaborRate'), oCallback.getReturnValue("LaborRate"))
    el('hdnToCurrencyCD').value = oCallback.getReturnValue("CurrencyCode");
    setText(el('lblOrgDepotCurrency'), el('hdnToCurrencyCD').value)

    setText(el('lblDepotCurrency'), el('hdnToCurrencyCD').value)

    if (oCallback.getReturnValue("Exchange") != "" && oCallback.getReturnValue("Exchange") != null) {
        el('hdnExchangeRate').value = oCallback.getReturnValue("Exchange");
    }


    if (el('hdnExchangeRate').value == "" || el('hdnExchangeRate').value == null) {
        exchangeRate = 0;
    }
    else {
        exchangeRate = el('hdnExchangeRate').value;
    }

    if (el('hdnPageName').value != 'Repair Approval') {
        setText(el('hypOrgDepotCost'), parseFloat(getText(el('hypOrginalCost')) * exchangeRate).toFixed(2));
    }

    if (gridClearMode == "clear") {
        bindLineDetail("Clear", 0, 0);
        bindSummaryDetail("Clear");
        setDefaultValues(0);
    }
}


function showTotalChargeByTaxRateChangeHdr() {
    var colscheck;
    if (el('txtTaxRate').value != "") {
        var taxRate = el('txtTaxRate').value;
    }
    else {
        var taxRate = 0;
    }
    var rIndex = ifgLineDetail.CurrentRowIndex();

    //    el('txtAppAmount').value = "";
    icount = ifgLineDetail.Rows().Count;
    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            cColumns = ifgLineDetail.Rows(i).GetClientColumns();

            var obj = cColumns[16];

            var totalCost = 0;
            if (obj == "B") {
                if (cColumns[14] != "" && cColumns[13] != "") {
                    totalCost = parseFloat(parseFloat(cColumns[14]) + parseFloat(cColumns[13])) + parseFloat((parseFloat(cColumns[14]) + parseFloat(cColumns[13])) * taxRate / 100);
                    ifgLineDetail.Rows(i).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
                }
                else if (cColumns[14] != "" && cColumns[13] == "") {
                    totalCost = parseFloat(cColumns[14]) + (parseFloat(cColumns[13]) * taxRate / 100);
                    ifgLineDetail.Rows(i).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
                }
            }
            else if (obj == "L") {
                totalCost = parseFloat(parseFloat(cColumns[13]) + ((parseFloat(cColumns[13]) * taxRate / 100)) + parseFloat(cColumns[14]));
                ifgLineDetail.Rows(i).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
            }
            else if (obj == "M") {
                if (cColumns[14] != "") {
                    totalCost = parseFloat(parseFloat(cColumns[14]) + ((parseFloat(cColumns[14]) * taxRate / 100)) + parseFloat(cColumns[13]));
                    ifgLineDetail.Rows(i).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
                }
                else {
                    totalCost = parseFloat(cColumns[19]);
                    ifgLineDetail.Rows(i).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
                }
            }
            else if (obj == "N") {
                if (cColumns[15] != "") {
                    totalCost = parseFloat(parseFloat(cColumns[14]) + parseFloat(cColumns[13]));
                    ifgLineDetail.Rows(i).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
                }
            }

            //            if (el('txtAppAmount').value != "" && el('txtAppAmount').value != "0.00") {
            //                el('txtAppAmount').value = (parseFloat(parseFloat(el('txtAppAmount').value)) + parseFloat(totalCost)).toFixed(2);
            //            }
            //            else {
            //                el('txtAppAmount').value = parseFloat(totalCost).toFixed(2);
            //            }

        }
    }
}

function showTotalChargeByTaxRateChange() {
    var colscheck;
    if (el('txtTaxRate').value != "") {
        var taxRate = el('txtTaxRate').value;
    }
    else {
        var taxRate = 0;
    }
    var rIndex = ifgLineDetail.CurrentRowIndex();

    icount = ifgLineDetail.Rows().Count;
    cColumns = ifgLineDetail.Rows(rIndex).GetClientColumns();

    var obj = cColumns[16];

    var totalCost = 0;
    if (obj == "B") {
        if (cColumns[14] != "" && cColumns[13] != "") {
            totalCost = parseFloat(parseFloat(cColumns[14]) + parseFloat(cColumns[13])) + parseFloat((parseFloat(cColumns[14]) + parseFloat(cColumns[13])) * taxRate / 100);
            ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
        }
        else if (cColumns[14] != "" && cColumns[13] == "") {
            totalCost = parseFloat(cColumns[14]) + (parseFloat(cColumns[13]) * taxRate / 100);
            ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
        }
    }
    else if (obj == "L") {
        if (cColumns[13] != "") {
            totalCost = parseFloat(parseFloat(cColumns[13]) + ((parseFloat(cColumns[13]) * taxRate / 100)) + parseFloat(cColumns[14]));
            ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
        }
        else {
            totalCost = parseFloat(cColumns[13]);
            ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
        }
    }
    else if (obj == "M") {
        if (cColumns[14] != "") {
            totalCost = parseFloat(parseFloat(cColumns[14]) + ((parseFloat(cColumns[14]) * taxRate / 100)) + parseFloat(cColumns[13]));
            ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
        }
        else {
            totalCost = parseFloat(cColumns[14]);
            ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
        }
    }
    else if (obj == "N") {
        if (cColumns[15] != "") {
            totalCost = parseFloat(parseFloat(cColumns[14]) + parseFloat(cColumns[13]));
            ifgLineDetail.Rows(rIndex).SetColumnValuesByIndex(10, parseFloat(totalCost).toFixed(2));
        }
    }
}

function CustomerTarifftTextChange() {

    if (el("lkpBillTo").value == "AGENT") {

        if (el("lkpCustomerTariff").SelectedValues[3] == "AGENT") {
            return false;
        }
        else if (el("lkpCustomerTariff").SelectedValues[3] == "CUSTOMER") {

            showWarningMessage('Tariff does not exist for the Agent, Hence customer tariff is displayed');

        }
        else if (el("lkpCustomerTariff").SelectedValues[3] == "STANDARD") {
            showWarningMessage('Tariff does not exist for the Agent and Customer, Hence standard tariff is displayed');
        }
        else {
            showWarningMessage('Tariff does not exist.');
        }
    }

    if (el("lkpBillTo").value == "CUSTOMER") {

        if (el("lkpCustomerTariff").SelectedValues[3] == "CUSTOMER") {
            return false;
        }
        else if (el("lkpCustomerTariff").SelectedValues[3] == "STANDARD") {
            showWarningMessage('Tariff does not exist for the Customer, Hence standard tariff is displayed');
        }
        else {
            showWarningMessage('Tariff does not exist.');
        }
    }

    return false;
}

function bindResponsibilityLookup() {
    if (getConfigSetting('061') == "True" || getConfigSetting('065') == "True") {
        return "TBL_TYP='MASTER'";
    }
    else {
        return "TBL_TYP='ENUM'";
    }
}
