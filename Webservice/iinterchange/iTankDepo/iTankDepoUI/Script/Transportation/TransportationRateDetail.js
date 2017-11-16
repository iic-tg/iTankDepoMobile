var HasChanges = false;
var vrGridIds = new Array('tabTransportationRate;ifgTransportationDetailRate');
var _RowValidationFails = false;

function initPage(sMode) {
    el('hdnTransportationId').value = getQueryStringValue(document.location.href, "TransportationId");
    el('hdnTransportationDetailId').value = getQueryStringValue(document.location.href, "TransportationDetailId");
    bindTransportationDetailRate();
    calculateTripRate();
    if (getQueryStringValue(document.location.href, "BillFlag") == "Y") {
        el('btnSave').style.display = "none";
    } else {
        el('btnSave').style.display = "block";
    }
}

function bindTransportationDetailRate() {
    var ifgTransportationDetailRate = new ClientiFlexGrid("ifgTransportationDetailRate");
    ifgTransportationDetailRate.parameters.add("TransportationId", el('hdnTransportationId').value);
    ifgTransportationDetailRate.parameters.add("TransportationDetailId", el('hdnTransportationDetailId').value);
    ifgTransportationDetailRate.parameters.add("BillFlag", getQueryStringValue(document.location.href, "BillFlag"));
    ifgTransportationDetailRate.DataBind();
}

function calculateTripRate() {
    var tripRate = new Number;
    var rowCount = ifgTransportationDetailRate.Rows().Count;
    if (rowCount > 0) {
        for (i = 0; i < rowCount; i++) {
            cCols = ifgTransportationDetailRate.Rows(i).GetClientColumns();
            if (cCols[2] != '') {
                tripRate = parseFloat(tripRate) + parseFloat(cCols[2]);
            }
        }
    }
    el('lblTripRateDetail').innerText = parseFloat(tripRate).toFixed(2);
}

function onClickCharge() {
    return "OPRTN_TYP_ID='87' AND DFLT_BT <> 1";
}

function onClientChargeClick(obj) {
    var oCallback = new Callback();
    oCallback.add("AdditionalChargeRateId", obj.SelectedValues[0]);
    oCallback.invoke("TransportationRateDetail.aspx", "getChargeRate");
    if (oCallback.getCallbackStatus()) {
        ifgTransportationDetailRate.Rows(ifgTransportationDetailRate.rowIndex).SetColumnValuesByIndex(1, parseFloat(oCallback.getReturnValue("ChargeRate")).toFixed(2));
        return true;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
}

function onCloseTransportationDetail() {
    el('hdnCancel').value = "True";
    ppsc().closeModalWindow();
}

function onSaveTransportationDetail() {
    el('hdnCancel').value = "False";
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (ifgTransportationDetailRate.Submit() == false) {
        return false;
    }
    ifgTransportationDetailRate.Submit(true);
    var oCallback = new Callback();
    oCallback.add("TransportationDetailId", el('hdnTransportationDetailId').value);
    oCallback.invoke("TransportationRateDetail.aspx", "saveTransportationRate");
    if (oCallback.getCallbackStatus()) {
        var rIndex = pdfs("wfFrame" + pdf("CurrentDesk")).ifgTransportation.CurrentRowIndex();
        var exchangeRate = new Number;
        exchangeRate = pdfs("wfFrame" + pdf("CurrentDesk")).el('hdnExchangeRate').value;
        pdfs("wfFrame" + pdf("CurrentDesk")).el('lblTotlAmount').innerText = parseFloat(oCallback.getReturnValue("TotalRate")).toFixed(2);
        pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = true;
        ppsc().closeModalWindow();
        return true;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function onBeforeCallTransportationDetailRate(mode, param) {
    param.add("TransportationId", el('hdnTransportationId').value);
    param.add("TransportationDetailId", el('hdnTransportationDetailId').value);
}

function onAfterCallTransportationDetailRate(param, mode) {
    if (mode == "Add" || mode == "Edit" || mode == "Delete") {
        if (typeof (param["CheckDefault"]) != "undefined") {
            showErrorMessage(param["CheckDefault"]);
        }
        else {
            el('lblTripRateDetail').innerText = parseFloat(param["TripRate"]).toFixed(2);
        }
        if (typeof (param["Delete"]) != "undefined") {
            showWarningMessage(param["Delete"]);
        }
    }
}

function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgTransportationDetailRate.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgTransportationDetailRate.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    resetHasChanges("ifgTransportationDetailRate");
    return true;

}

function validateChargeCode(oSrc, args) {
    var rIndex = ifgTransportationDetailRate.VirtualCurrentRowIndex();
    var cols = ifgTransportationDetailRate.Rows(rIndex).GetClientColumns();
    var i;
    var chargeCode = cols[0];
    var icount = ifgTransportationDetailRate.Rows().Count
    var colscheck;

    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgTransportationDetailRate.Rows(i).GetClientColumns();
                if (chargeCode == colscheck[0]) {
                    args.IsValid = false;
                    oSrc.errormessage = "This Code already exists";
                    return;
                }
            }
        }
    }
}

//This function is used to format all the Rate Fields to fixed.
function formatDecimalRate(obj) {
    var totalCost = 0;
    var icount = 0;
    var rIndex = ifgTransportationDetailRate.CurrentRowIndex();
    if (trimAll(obj.value) != "") {
        var Amount = new Number;
        Amount = parseFloat(obj.value);
        obj.value = Amount.toFixed(2);
    }
}

