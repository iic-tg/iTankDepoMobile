var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgLineDetail');
var _RowValidationFails = false;
var _repairEstimateId = "";
var _repairEstimateDetailId = "";

function initPage(sMode) {
    _repairEstimateDetailId = getQueryStringValue(document.location.href, "RepairEstimateDetailId");
    _repairEstimateId = getQueryStringValue(document.location.href, "RepairEstimateId");
    setFocusToField('lkpParty');   
}

function onApplyMassApplyInput() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }  
    var oCallback = new Callback();
    oCallback.add("PartyId", el('lkpParty').SelectedValues[0]);
    oCallback.add("PartyCode", el('lkpParty').value);
    oCallback.add("RepairEstimateDetailId", _repairEstimateDetailId);
    oCallback.add("RepairEstimateId", _repairEstimateId);  
    oCallback.invoke("RepairEstimateMassApplyInput.aspx", "massApplyInput")
    if (oCallback.getCallbackStatus()) {
        pdfs("wfFrame" + pdf("CurrentDesk")).bindLineDetail('ReBind', '', '');
        pdfs("wfFrame" + pdf("CurrentDesk")).bindSummaryDetail('ReBind', '', '');
        pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = true;       
        ppsc().closeModalWindow();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return true;
}

function onCancelMassApplyInput() {
    ppsc().closeModalWindow();
}