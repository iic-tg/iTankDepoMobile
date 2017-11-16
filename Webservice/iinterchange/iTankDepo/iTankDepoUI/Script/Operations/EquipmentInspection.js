
var vrGridIds = new Array('ITab1_0;ifgEquipmentDetail');
var CustomerName = "";
var Description = "";
var StatusID = "";
var YardLocation = "";
var StrPrint;
var EqpmntTypeID = "";
var EqpmntTypeCD = "";
var EqpmntCodeID = "";
var EqpmntCodeCD = "";
var pageModeForExport = "pending";

function initPage(sMode) {

    var sPageTitle = getPageTitle();
    $("#spnHeader").text("Operations >> " + sPageTitle);
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    bindEquipmentDetail("new");
    reSizePane();
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount <= 0) {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}


function submitPage() {

    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount > 0) {
        if (getPageChanges()) {
            ifgEquipmentDetail.Submit();
            if (ifgEquipmentDetail.Submit() == false || _RowValidationFails) {
                return false;
            }
            ifgEquipmentDetail.Submit(true);
            return updateEquipmentInspection();

        }
        else {
            showInfoMessage('No Changes to Save');
            return false;
        }
    } else {
        showInfoMessage('There are no Records to perform Submit');
        return false;
    }

    return true;
}

function yesClick() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount > 0) {
        if (getPageChanges() || HasMoreInfoChanges) {
            if (ifgEquipmentDetail.Submit() == false || _RowValidationFails) {
                return false;
            }
            return updateEquipmentInspection();
        }
        else {
            showInfoMessage('No Changes to Save');
            return false;
        }
    } else {
        showInfoMessage('There are no Records to perform Submit');
        return false;
    }
}

function noClick() {
    return false;
}

//This function is used to Update the changes to the server. 
function updateEquipmentInspection() {
    var oCallback = new Callback();
    //  ifgEquipmentDetail.Submit(true);
    oCallback.add("WFData", getWFDATA());
    oCallback.add("Mode", el("hdnMode").value);
    oCallback.invoke("EquipmentInspection.aspx", "updateEquipmentInspection");

    if (oCallback.getCallbackStatus()) {
        if (el("iTabSelection_ITab1").value == "Pending") {
            sMode = "new";
        } else {
            sMode = "edit";
        }
        //  sMode = "edit";
        bindEquipmentDetail(sMode);
        if (oCallback.getReturnValue("LockRecord") != "" && oCallback.getReturnValue("LockRecord") != null) {//added and changed for UIG Fix
            showErrorMessage("The following equipment(s) are already submitted. " + oCallback.getReturnValue("LockRecord"));
        }
        if (oCallback.getReturnValue("LockRecordBit") != "true") {
            showInfoMessage(oCallback.getReturnValue("Message"));
        }
        HasChanges = false;
        resetHasChanges("ifgEquipmentDetail");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function bindEquipmentDetail(mode) {
    var objGrid = new ClientiFlexGrid("ifgEquipmentDetail");
    objGrid.parameters.add("Mode", mode);
    objGrid.parameters.add("WFData", getWFDATA());
    objGrid.DataBind();
}


function bindPending() {
    bindEquipmentDetail(MODE_NEW);
    el("hdnMode").value = MODE_NEW;
}


function ifgEquipmentDetailOnAfterSubmitTabSelected() {
    if (HasChanges == true) {
        if (checkGridHasChanges("ifgEquipmentDetail")) {
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
        bindEquipmentDetail(MODE_EDIT);
    }
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount <= 0) {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}

// Lock Implementation 
function GIlockData(obj, strEquipmentNo) {
    var oCallback = new Callback();
    oCallback.add("CheckBit", obj.checked);
    oCallback.add("EquipmentNo", strEquipmentNo);
    oCallback.add("LockBit", "");
    oCallback.invoke("EquipmentInspection.aspx", "GIlockData");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ErrorMessage") != "") {
            obj.checked = false;
            var errorMessage = oCallback.getReturnValue("ErrorMessage");

            if (oCallback.getReturnValue("Activity") != "") {
                errorMessage = errorMessage + " <B> (Activity : " + oCallback.getReturnValue("Activity") + ")</B>";
            }

            showErrorMessage(errorMessage);
        }
        return true;
    }
    else {
        return false;
    }
    oCallback = null;
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
        if ($(window.parent)) {
            if ($(window.parent).height() < 768) {
                el("tabGateIn").style.height = $(window.parent).height() - 350 + "px";
                if (el("ifgEquipmentDetail") != null) {
                    el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
                }
            }
            else {
                el("tabGateIn").style.height = $(window.parent).height() - 360 + "px";
                if (el("ifgEquipmentDetail") != null) {
                    el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 460 + "px");
                }
            }
        }
    }
}

function ifgEquipmentDetailOnBeforeCB(mode, param) {
    param.add("PageMode", "New");
    param.add("CustomerName", CustomerName);
    param.add("Description", Description);
    param.add("StatusID", StatusID);
    param.add("YardLocation", YardLocation);
    param.add("EqpmntTypeID", EqpmntTypeID);
    param.add("EqpmntTypeCD", EqpmntTypeCD);
    param.add("EqpmntCodeID", EqpmntCodeID);
    param.add("EqpmntCodeID", EqpmntCodeID);
}

function GIlockData(obj, strEquipmentNo) {
    var oCallback = new Callback();
    oCallback.add("CheckBit", obj.checked);
    oCallback.add("EquipmentNo", strEquipmentNo);
    oCallback.add("LockBit", "");
    oCallback.invoke("GateIn.aspx", "GIlockData");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ErrorMessage") != "") {
            obj.checked = false;
            var errorMessage = oCallback.getReturnValue("ErrorMessage");

            if (oCallback.getReturnValue("Activity") != "") {
                errorMessage = errorMessage + " <B> (Activity : " + oCallback.getReturnValue("Activity") + ")</B>";
            }

            showErrorMessage(errorMessage);
        }
        return true;
    }
    else {
        return false;
    }
    oCallback = null;
}

function ifgEquipmentDetailOnAfterSubmitTabSelected() {
    pageModeForExport = "mysubmits";
    if (HasChanges == true) {
        if (checkGridHasChanges("ifgEquipmentDetail")) {
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
        bindEquipmentDetail(MODE_EDIT);
    }
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount <= 0) {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}

function ifgEquipmentDetailOnAfterPendingTabSelected() {
    pageModeForExport = "pending";
    bindEquipmentDetail(MODE_NEW);
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount <= 0) {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}

function filterDefaultStatus() {
    var strFilter;
    strFilter = " WF_ACTIVITY_NAME = 'Equipment Inspection' AND DPT_ID=" + el('hdnDepotID').value;
    return strFilter;
}

function ValidateEquipmentStatus(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    if (cols[9] == "") {
        oSrc.errormessage = "Equipment Status Required";
        args.IsValid = false;
    }
}

function ValidateEquipmentCode(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    if (cols[5] == "") {
        oSrc.errormessage = "Equipment Code Required";
        args.IsValid = false;
    }
}

function ValidateInspectionDate(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    if (cols[12] == "" || cols[12] == null) {
        oSrc.errormessage = "In Date Required";
        args.IsValid = false;
        return;
    }

    //    var oCallback = new Callback();
    //    oCallback.add("EquipmentNo", cols[2]);
    //    oCallback.add("EventDate", cols[10]);
    //    oCallback.invoke("GateIn.aspx", "ValidatePreviousActivityDate");

    //    if (oCallback.getCallbackStatus()) {
    //        if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {//added and changed for UIG Fix
    //            args.IsValid = false;
    //            oSrc.errormessage = oCallback.getReturnValue("Error");
    //        }
    //        else {
    //            args.IsValid = true;
    //        }
    //    }
    //    else {
    //        showErrorMessage(oCallback.getCallbackError());
    //    }
    //    oCallback = null;
}

function ifgEquipmentDetailOnAfterCB(param, mode) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    CustomerName = "";
    Description = "";
    if (typeof (param["Delete"]) != 'undefined') {
        showWarningMessage(param["Delete"]);
        return;
    }
}

function ifgEquipmentDetailOnBeforeSubmitTabSelected() {
}

function validateBillTo(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    if (cols[8] == null || cols[8] == "") {
        if (cols[0] == 'undefined' || cols[0] == "") {
            args.IsValid = false;
            oSrc.errormessage = "Please select a customer to validate";
        }
        else {
            if (cols[11] == "AGENT") {
                args.IsValid = false;
                oSrc.errormessage = "Please select only Customer";
            }
        }
    }
    else {
        if (cols[0] == 'undefined' || cols[0] == "") {
            args.IsValid = false;
            oSrc.errormessage = "Please select a customer to validate";
        }
    }
}

function ValidateInspectedDate() {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();

}

function ValidatePreviousActivity(oSrc, args) {
    // var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
//    if (args.Value == "") {
//        oSrc.errormessage = el('hdnPageName').value + " Date Required";
//        args.IsValid = false;
//        return;
//    }

    var oCallback = new Callback();
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    oCallback.add("EquipmentNo", cols[2]);
    oCallback.add("EventDate", args.Value);
    oCallback.invoke("EquipmentInspection.aspx", "ValidatePreviousActivityDate");

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

//This function is used to export the content in grids of all the master pages.
function exportToExcelWF() {
    var fE = document.getElementById("iFgfrm");
    var t = new Date();
    fE.src = "EquipmentInspectionExport.aspx?ifgActivityId=" + getQStr("activityid") + "&pageModeForExport=" + pageModeForExport + "&Export=True&t1=" + t.getTime();
}