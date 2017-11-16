function addCustomerCleaningRate() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (ifgProductCleaningType.Submit() == false)
        return false;

    updateCustomerCleaningRate();
    pdfs("wfFrame" + pdf("CurrentDesk")).HasChargeHeadChanges = true;
    ppsc().closeModalWindow();
    return true;
}

function initPage() {
    bindCleaningType();
}

function onBeforeCallback(mode, param) {
    if (mode = "Insert") {
        param.add("ProductCustomerID", el('hdnProductCustomerID').value);
    }
}

function bindCleaningType() {
    var ifgProductCleaningType = new ClientiFlexGrid("ifgProductCleaningType");
    ifgProductCleaningType.parameters.add("ProductID", el('hdnProductID').value);
    ifgProductCleaningType.parameters.add("ProductCustomerID", el('hdnProductCustomerID').value);
    ifgProductCleaningType.parameters.add("CustomerID", el('hdnCustomerID').value);
    ifgProductCleaningType.DataBind();
}

function checkDuplicateCleaningType(oSrc, args) {
    var rIndex = ifgProductCleaningType.VirtualCurrentRowIndex();
    var cols = ifgProductCleaningType.Rows(rIndex).GetClientColumns();
    var i;
    var sPort = cols[0];
    var icount = ifgProductCleaningType.Rows().Count
    var colscheck;

    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgProductCleaningType.Rows(i).GetClientColumns();
                if (sPort == colscheck[0]) {
                    args.IsValid = false;
                    oSrc.errormessage = "This Code already exists";
                    return;
                }
            }
        }
    }
}

function formatCleaningType(obj) {
    var Amount = new Number;
    Amount = parseFloat(obj.value);
    obj.value = Amount.toFixed(2);
}

function updateCustomerCleaningRate() {
    ifgProductCleaningType.Submit(true);
    var oCallback = new Callback();
    oCallback.add("CustomerID", el("hdnCustomerID").value);
    oCallback.invoke("CleaningTypePopup.aspx", "GetSumTotalAmount");
    if (oCallback.getCallbackStatus()) {
        var rIndex = pdfs("wfFrame" + pdf("CurrentDesk")).ifgProductCustomer.CurrentRowIndex();
        el('hdnProductCleaningRate').value = oCallback.getReturnValue("ProductCleaningRate");
        ppsc().closeModalWindow();
        if (el('hdnProductCleaningRate').value != "" && el('hdnProductCleaningRate').value != null) {//Chrome Fix
            var productCleaningAmount = new Number;
            productCleaningAmount = parseFloat(el('hdnProductCleaningRate').value);
            pdfs("wfFrame" + pdf("CurrentDesk")).ifgProductCustomer.Rows(rIndex).SetColumnValuesByIndex(2, productCleaningAmount.toFixed(2));
        }
        return true;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}