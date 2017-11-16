var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgRentalOtherCharge');
function initPage(sMode) {
    bindOtherChargeGrid();
    calculateAdditionalRate();
    if (getQueryStringValue(document.location.href, "OffhireDate") != "") {
        el('btnSave').style.display = "none";
    } else {
        el('btnSave').style.display = "block";
    }
}
function bindOtherChargeGrid() {
    var ifgRentalOtherCharge = new ClientiFlexGrid("ifgRentalOtherCharge");
    ifgRentalOtherCharge.parameters.add("EquipmentNo", el("hdnEquipmentNo").value);
    ifgRentalOtherCharge.parameters.add("CustomerID", el("hdnCustomerId").value);
    ifgRentalOtherCharge.parameters.add("ContractNo", el("hdnContractNo").value);
    ifgRentalOtherCharge.parameters.add("RentalID", el("hdnRentalId").value);
    ifgRentalOtherCharge.parameters.add("EqpmntID", el("hdnEqpmntID").value);
    ifgRentalOtherCharge.parameters.add("Offhiredate", getQueryStringValue(document.location.href, "OffhireDate"));
    ifgRentalOtherCharge.DataBind();
}

function addOtherCharge() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (ifgRentalOtherCharge.Submit() == false)
        return false;

    updateOtherCharge();
    var rIndex = pdfs("wfFrame" + pdf("CurrentDesk")).ifgRentalEntry.CurrentRowIndex();
    pdfs("wfFrame" + pdf("CurrentDesk")).ifgRentalEntry.Rows(rIndex).SetColumnValuesByIndex(6, "Add/Edit");
    pdfs("wfFrame" + pdf("CurrentDesk")).HasChargeHeadChanges = true;
    ppsc().closeModalWindow();
    return true;
}
function updateOtherCharge() {
    ifgRentalOtherCharge.Submit(true);
    var oCallback = new Callback();
    oCallback.add("RentalID", el("hdnRentalId").value);
    oCallback.invoke("RentalOtherCharge.aspx", "GetOtherCharge");
    if (oCallback.getCallbackStatus()) {
        ppsc().closeModalWindow();

        return true;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}
function onBeforeCallback(mode, param) {
    if (mode = "Insert") {
        param.add("RentalID", el('hdnRentalId').value);
        param.add("EqpmntID", el('hdnEqpmntID').value);
    }
}
function FilterAdditionalCharge() {
    return " OPRTN_TYP_ID = '88' AND DFLT_BT='FALSE'";
}
function checkDuplicateOtherCharge(oSrc, args) {
    var rIndex = ifgRentalOtherCharge.VirtualCurrentRowIndex();
    var cols = ifgRentalOtherCharge.Rows(rIndex).GetClientColumns();
    var i;
    var sPort = cols[0];
    var icount = ifgRentalOtherCharge.Rows().Count
    var colscheck;

    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgRentalOtherCharge.Rows(i).GetClientColumns();
                if (sPort == colscheck[0]) {
                    args.IsValid = false;
                    oSrc.errormessage = "This Code already exists";
                    return;
                }
            }
        }
    }
}
function onClose() {
    ppsc().closeModalWindow();
}
// Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :57
function calculateAdditionalRate() {
    var Rate = new Number;
    var rowCount = ifgRentalOtherCharge.Rows().Count;
    if (rowCount > 0) {
        for (i = 0; i < rowCount; i++) {
            cCols = ifgRentalOtherCharge.Rows(i).GetClientColumns();
            if (cCols[2] != '') {
                Rate = parseFloat(Rate) + parseFloat(cCols[2]);
            }
        }
    }
    // el('lblAdditionalRateDetail').innerText = parseFloat(Rate).toFixed(2);
    setText(el('lblAdditionalRateDetail'), parseFloat(Rate).toFixed(2));

}

function onAfterCallAdditionalRate(param, mode) {
    if (mode == "Add" || mode == "Edit" || mode == "Delete") {
        if (typeof (param["CheckDefault"]) != "undefined") {
            showErrorMessage(param["CheckDefault"]);
        }
        else {
            // el('lblAdditionalRateDetail').innerText = parseFloat(param["Rate"]).toFixed(2);
            setText(el('lblAdditionalRateDetail'), parseFloat(param["Rate"]).toFixed(2));
        }
        if (typeof (param["Delete"]) != "undefined") {
            showWarningMessage(param["Delete"]);
        }
    }
}

function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgRentalOtherCharge.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgRentalOtherCharge.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    resetHasChanges("ifgRentalOtherCharge");
    return true;

}

function onClientChargeClick(obj) {
    var oCallback = new Callback();
    oCallback.add("AdditionalChargeRateId", obj.SelectedValues[0]);
    oCallback.invoke("RentalOtherCharge.aspx", "getChargeRate");
    if (oCallback.getCallbackStatus()) {
        ifgRentalOtherCharge.Rows(ifgRentalOtherCharge.rowIndex).SetColumnValuesByIndex(1, parseFloat(oCallback.getReturnValue("ChargeRate")).toFixed(2));
        return true;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
}