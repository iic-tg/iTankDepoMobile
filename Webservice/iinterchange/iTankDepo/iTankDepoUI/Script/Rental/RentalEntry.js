var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgRentalEntry');
var _RowValidationFails = false;


//Init Page
function initPage(sMode) {
    
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    bindRentalGrid(MODE_NEW);
    checkNoRecordFound();
    reSizePane();
}

function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        if (ifgRentalEntry.Submit() == false || _RowValidationFails) {
            return false;
        }
        updateRental();
        checkNoRecordFound();
    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

//This method used to update/insert entered row to database
function updateRental() {
    var activityName = "";
    activityName = getQueryStringValue(document.location.href, "activityname");
    var oCallback = new Callback();
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.add("ActivityName", activityName);
    oCallback.invoke("RentalEntry.aspx", "UpdateRental");
    if (oCallback.getCallbackStatus()) {
        if (el("iTabSelection_ITab1").value == "Pending") {
            sMode = "new";
        } else {
            sMode = "edit";
        }
        bindRentalGrid(sMode);
        if (oCallback.getReturnValue("RentalExistEquipmentNo") != '') {
            showErrorMessage("The following equipment(s) are already submitted by another user " + oCallback.getReturnValue("RentalExistEquipmentNo"));
        }
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;

        resetHasChanges("ifgRentalEntry");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

//BindRental
function bindRentalGrid(mode) {
    var objGrid = new ClientiFlexGrid("ifgRentalEntry");
    objGrid.parameters.add("ActivityName", getQueryStringValue(document.location.href, "activityname"));
    objGrid.parameters.add("Mode", mode);
    objGrid.DataBind();
}

function ContractRefNoFilter() {
    var iRowIndex = ifgRentalEntry.rowIndex;
    if (iRowIndex != "") {
        var oCols = ifgRentalEntry.Rows(iRowIndex).GetClientColumns();
        var str;
        if (isNaN(oCols[2])) {
            str = "CSTMR_CD='" + oCols[1] + "'";
        }
        else {
            str = "CSTMR_ID=" + oCols[2];
        }
        return str;
    }
}

function formatRate(obj) {
    var Amount = new Number;
    if (obj.value != "") {
        Amount = parseFloat(obj.value);
        obj.value = Amount.toFixed(2);
    }
    else {
        obj.value = "";
    }
}

function openOtherChargeDetail() {
    if (ifgRentalEntry.CurrentRowIndex() != "") {
        var iRowIndex = ifgRentalEntry.CurrentRowIndex();
        var cols = ifgRentalEntry.Rows(iRowIndex).GetClientColumns();
        if (cols.length == 1) {
            if (ifgRentalEntry.Submit(true) == false) {
                return false;
            }
        }
        else {
            showModalWindow("Additional Charge Detail - " + cols[0], "Rental/RentalOtherCharge.aspx?EquipmentNo=" + cols[0] + "&CustomerId=" + cols[1] + "&EqpmntID=" + cols[12] + "&ContractNo=" + cols[3] + "&RentalID=" + cols[11] + "&OffhireDate=" + cols[7], "550px", "350px", "100px", "", "");
            return true;
            psc().hideLoader();
        }
    }
}

function ValidateCustomer(oSrc, args) {
    var rIndex = ifgRentalEntry.CurrentRowIndex();
    var cColumns = ifgRentalEntry.Rows(rIndex).GetClientColumns();
    if (cColumns[1] == "") {
        oSrc.errormessage = "Customer Name Required";
        args.IsValid = false;
    }
}

function ValidateContractRefNo(oSrc, args) {
    var rIndex = ifgRentalEntry.CurrentRowIndex();
    var cColumns = ifgRentalEntry.Rows(rIndex).GetClientColumns();
    if (cColumns[3] == "") {
        oSrc.errormessage = "Contract Reference No Required";
        args.IsValid = false;
    }
}

function ValidateRentalPerDay(oSrc, args) {
    var rIndex = ifgRentalEntry.CurrentRowIndex();
    var cColumns = ifgRentalEntry.Rows(rIndex).GetClientColumns();
    if (cColumns[6] == "") {
        oSrc.errormessage = "Rental Per Day Required";
        args.IsValid = false;
    }
}

function ifgRentalOnAfterPendingTabSelected() {
    bindRentalGrid(MODE_NEW);
    checkNoRecordFound();
}

function ifgRentalOnAfterSubmitTabSelected() {
    if (HasChanges == true) {
        if (checkGridHasChanges("ifgRentalEntry")) {
            setHasChanges();
            var confirm = false;
            if (psc().confirmresult == false) {
                confirm = confirmChanges("submit", "");
                return false;
            }
            if (confirm)
                return true;
        }
    }
    else {
        el("hdnMode").value = "edit";
        bindRentalGrid(MODE_EDIT);
    }
    checkNoRecordFound();
}

function ValidateCustomer(oSrc, args) {
    var cols = ifgRentalEntry.Rows(ifgRentalEntry.CurrentRowIndex()).GetClientColumns();
    if (cols[1] == "") {
        oSrc.errormessage = "Customer Required";
        args.IsValid = false;
    }
}

function ValidateContractRefNo(oSrc, args) {
    var cols = ifgRentalEntry.Rows(ifgRentalEntry.CurrentRowIndex()).GetClientColumns();
    if (cols[3] == "") {
        oSrc.errormessage = "Contract Reference Number Required";
        args.IsValid = false;
    }
}

function CleraContractRef() {
    var cols = ifgRentalEntry.Rows(ifgRentalEntry.CurrentRowIndex()).GetClientColumns();
    var rIndex = ifgRentalEntry.CurrentRowIndex();
    ifgRentalEntry.Rows(rIndex).SetColumnValuesByIndex(2, "");
}

function DeleteClick() {
    ifgRentalEntry.Submit();
    var oCallback = new Callback();
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.add("ActivityName", getQueryStringValue(document.location.href, "activityname"));  
    oCallback.invoke("RentalEntry.aspx", "DeleteRental");
    if (oCallback.getCallbackStatus()) {
        var confirmresult = false;
        var confirm = false;
        showConfirmMessage("Are you sure you want to delete this activity. Click 'Yes' to Delete or 'No' to ignore",
     "wfs().yesClick();", "wfs().noClick();");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function yesClick() {
    var oCallback = new Callback();
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("RentalEntry.aspx", "DeleteRentalEquipment");
    if (oCallback.getCallbackStatus()) {
        if (el("iTabSelection_ITab1").value == "Pending") {
            sMode = "new";
        } else {
            sMode = "edit";
        }
        bindRentalGrid(sMode);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;

        resetHasChanges("ifgRentalEntry");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function noClick() {
    return false;
}

function compareOnHireDate(oSrc, args) {
    var cols = ifgRentalEntry.Rows(ifgRentalEntry.CurrentRowIndex()).GetClientColumns();
    if (cols[6] != '') {
        var col = ifgRentalEntry.Rows(ifgRentalEntry.CurrentRowIndex()).Columns();
        var oCallback = new Callback();
        oCallback.add("GateInDate", col[32]);
        oCallback.add("ActivityDate", args.Value);
        oCallback.add("Activity", "OFFHIRE");
        oCallback.invoke("RentalEntry.aspx", "ValidateOnHireOffHireDate");
        if (args.Value == "") {
            args.IsValid = false;
            oSrc.errormessage = "Off-Hire Date Required";
            return false;
        }
        else if (oCallback.getCallbackStatus()) {
            args.IsValid = true;
            return true;
        }
        else if (oCallback.getReturnValue("GateInError") != "" && oCallback.getReturnValue("GateInError") != null) {
            args.IsValid = false;
            oSrc.errormessage = "Off-Hire Date should be greater than or equal to Gate In Date (" + oCallback.getReturnValue("GateInDate") + ")";
            return false;
        }
        oCallback = null;
    }
}

function ValidateOnHireDate(oSrc, args) {
    var cols = ifgRentalEntry.Rows(ifgRentalEntry.CurrentRowIndex()).Columns();
    var oCallback = new Callback();
    oCallback.add("RentalID", cols[0]);
    oCallback.add("EquipmentNo", cols[1]);
    oCallback.add("GateOutDate", cols[30]);
    oCallback.add("ContractStartDate", cols[31]);
    oCallback.add("ActivityDate", args.Value);
    oCallback.add("Activity", "ONHIRE");
    oCallback.invoke("RentalEntry.aspx", "ValidateOnHireOffHireDate");
    if (args.Value == "") {
        args.IsValid = false;
        oSrc.errormessage = "On-Hire Date Required";
        return false;
    }
    else if (oCallback.getCallbackStatus()) {
        args.IsValid = true;
        return true;
    }
    else if (oCallback.getReturnValue("GateError") != "" && oCallback.getReturnValue("GateError") != null) {
        args.IsValid = false;
        oSrc.errormessage = "On-Hire Date should be lesser than or equal to Gate Out Date (" + oCallback.getReturnValue("GateOutDate") + ")";
        return false;
    }
    else if (oCallback.getReturnValue("GateInDateError") != "") {
        args.IsValid = false;
        oSrc.errormessage = "On-Hire Date should be greater than or equal to previous Off-Gire Date (" + oCallback.getReturnValue("GateInDate") + ")";
        return false;
    }
    else if (oCallback.getReturnValue("ContractDateError") != "") {
        args.IsValid = false;
        oSrc.errormessage = "On-Hire Date should be greater than or equal to Contract Start Date (" + oCallback.getReturnValue("CotnractStartDate") + ")";
        return false;
    }
    oCallback = null;
}

function checkNoRecordFound() {
    icount = ifgRentalEntry.Rows().Count;
    if (icount <= 0) {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
        showSubmitButton(false);
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
        showSubmitButton(true);
    }
}

function ifgRentalEntryOnBeforeCB(mode, param) {
    if (mode == "Delete") {
        param.add("ActivityName", getQueryStringValue(document.location.href, "activityname"));       
    }
}

//Window Resize
if (window.$) {
    $(document).ready(function () {

        reSizePane();
    });
}

$(window.parent).resize(function () {
    reSizePane();
});


function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 768) {
            el("divRentalEntry").style.height = $(window.parent).height() - 235 + "px";
            if (el("ifgRentalEntry") != null) {
                el("ifgRentalEntry").SetStaticHeaderHeight($(window.parent).height() - 316 + "px");
            }

        }
        else {
            el("divRentalEntry").style.height = $(window.parent).height() - 250 + "px";
            if (el("ifgRentalEntry") != null) {
                el("ifgRentalEntry").SetStaticHeaderHeight($(window.parent).height() - 335 + "px");
            }

        }
    }

}