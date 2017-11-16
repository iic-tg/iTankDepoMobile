var HasChanges = false;
var vrGridIds = new Array('tabTransportationMassApplyInput;ifgMassApplyInput');
var _RowValidationFails = false;

function initPage(sMode) {
    el('hdnTransportationId').value = getQueryStringValue(document.location.href, "TransportationId");
    el('hdnTransportationDetailId').value = getQueryStringValue(document.location.href, "TransportationDetailId");
    el('hdnRequestDate').value = getQueryStringValue(document.location.href, "RequestDate");
    setFocusToField('lkpEqpType');
    bindTransportationMassApplyInput();
}

function bindTransportationMassApplyInput() {
    var ifgMassApplyInput = new ClientiFlexGrid("ifgMassApplyInput");
    ifgMassApplyInput.parameters.add("TransportationId", el('hdnTransportationId').value);
    ifgMassApplyInput.parameters.add("TransportationDetailId", el('hdnTransportationDetailId').value);
    ifgMassApplyInput.DataBind();
}

function onAfterCallTransportationMassInput(param, mode) {
    if (mode == "Add" || mode == "Edit" || mode == "Delete") {
        if (typeof (param["CheckDefault"]) != "undefined") {
            showErrorMessage(param["CheckDefault"]);
        }
        if (typeof (param["Delete"]) != "undefined") {
            showWarningMessage(param["Delete"]);
        }
    }
}

function onClickCharge() {
    return "OPRTN_TYP_ID='87'";
}

function onCancelMassApplyInput() {
    ppsc().closeModalWindow();
}

function onApplyMassApplyInput() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (ifgMassApplyInput.Submit() == false) {
        return false;
    }
    ifgMassApplyInput.Submit(true);
    var oCallback = new Callback();
    oCallback.add("EquipmentTypeId", el('lkpEqpType').SelectedValues[0]);
    oCallback.add("EquipmentTypeCode", el('lkpEqpType').value);
    oCallback.add("CustomerRefNo", el('txtCustomerRefNo').value);
    oCallback.add("JobStartDate", el('datJobStartDate').value);
    oCallback.add("JobEndDate", el('datJobEndDate').value);
    oCallback.add("PreviousCargoId", el('lkpPreviousCargo').SelectedValues[0]);
    oCallback.add("PreviousCargoCode", el('lkpPreviousCargo').value);    
    oCallback.invoke("TransportationMassApplyInput.aspx", "massApplyInput")
    if (oCallback.getCallbackStatus()) {
        pdfs("wfFrame" + pdf("CurrentDesk")).bindTransportation('ReBind');
        pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = true;
        showErrorMessage("Applied");
        ppsc().closeModalWindow();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return true;
}

function validateChargeCode(oSrc, args) {
    var rIndex = ifgMassApplyInput.VirtualCurrentRowIndex();
    var cols = ifgMassApplyInput.Rows(rIndex).GetClientColumns();
    var i;
    var chargeCode = cols[0];
    var icount = ifgMassApplyInput.Rows().Count
    var colscheck;

    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgMassApplyInput.Rows(i).GetClientColumns();
                if (chargeCode == colscheck[0]) {
                    args.IsValid = false;
                    oSrc.errormessage = "This Code already exists";
                    return;
                }
            }
        }
    }
}

function compareRequestDate(oSrc, args) {
    if (el("hdnRequestDate").value != '') {
        if (DateCompareEqual(args.Value, el("hdnRequestDate").value)) {
            if (el('datJobEndDate').value != '') {
                if (DateCompareEqual(el('datJobEndDate').value, args.Value)) {
                    args.IsValid = true;
                    return;
                }
                else {
                    args.IsValid = false;
                    oSrc.errormessage = "Job Start Date should be less than or equal to Job End Date (" + el('datJobEndDate').value + ")";
                    return;
                }
            }

        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Job Start Date should be greater than or equal to Request Date (" + el("hdnRequestDate").value + ")";
            return;
        }
    }
    else if (el('datJobEndDate').value != '') {
        if (DateCompareEqual(el('datJobEndDate').value, args.Value)) {
            args.IsValid = true;
            return;
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Job Start Date should be less than or equal to Job End Date (" + el('datJobEndDate').value + ")";
            return;
        }
    }
}

function compareJobStartDate(oSrc, args) {
    if (el('datJobStartDate').value != '') {
        if (DateCompareEqual(args.Value, el('datJobStartDate').value)) {
            args.IsValid = true;
            return;
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Job End Date should be greater than or equal to Job Start Date (" + el('datJobStartDate').value + ")";
            return;
        }
    }
    else if (el("hdnRequestDate").value != '') {
        if (DateCompareEqual(args.Value, el("hdnRequestDate").value)) {
            args.IsValid = true;
            return;
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Job End Date should be greater than or equal to Request Date (" + el("hdnRequestDate").value + ")";
            return;
        }
    }
}

function onStateClick(obj) {
    if (el('lkpEqState').value == "EMPTY") {
        el('txtTripRate').value = el('hdnEmptyRate').value;
    }
    else if (el('lkpEqState').value == "FULL") {
        el('txtTripRate').value = el('hdnFullRate').value;
    }
    else {
        el('txtTripRate').value = '';
    }
}

function formatRate(obj) {
    var Amount = new Number;
    obj = el('txtTripRate').value;
    if (obj.value != "") {
        Amount = parseFloat(obj);
        el('txtTripRate').value = Amount.toFixed(2);
    }
    else {
        obj.value = "";
    }
}

function onClientChargeClick(obj) {
    var oCallback = new Callback();
    oCallback.add("AdditionalChargeRateId", obj.SelectedValues[0]);
    oCallback.invoke("TransportationMassApplyInput.aspx", "getChargeRate");
    if (oCallback.getCallbackStatus()) {
        ifgMassApplyInput.Rows(ifgMassApplyInput.rowIndex).SetColumnValuesByIndex(1, parseFloat(oCallback.getReturnValue("ChargeRate")).toFixed(2));
        return true;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
}