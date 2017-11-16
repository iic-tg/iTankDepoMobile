var HasChanges = false;
var _RowValidationFails = false;
var _transporterCode = "";

function initPage(sMode) {
    _transporterCode = getQueryStringValue(document.location.href, "TransporterCode");
    setFocusToField('lkpTransporter');
}

function onApplyRoute() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }  
    var oCallback = new Callback();
    oCallback.add("TransporterID", el('lkpTransporter').SelectedValues[0]);
    oCallback.add("chkCustomerRate", el('chkCustomerRate').checked);
    oCallback.invoke("TransporterCopyRoute.aspx", "applyRouteDetail")
    if (oCallback.getCallbackStatus()) {
        pdfs("wfFrame" + pdf("CurrentDesk")).bindRouteDetail('ReBindCopy');
        pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = true;
        showInfoMessage("Route Details applied to Transporter Code :" + _transporterCode);
        ppsc().closeModalWindow();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return true;
}

function filterCurrentCode(oSrc, args) {
    return " TRNSPRTR_CD <> '" + _transporterCode + "'";
}