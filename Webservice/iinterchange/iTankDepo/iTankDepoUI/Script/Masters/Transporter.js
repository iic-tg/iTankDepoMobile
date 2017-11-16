var HasChanges = false;
var vrGridIds = new Array('tabTransporter;ifgRouteDetail');
var _RowValidationFails = false;

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}

function initPage(sMode) {
    
    if (sMode == MODE_NEW) {
        setFocusToField("txtTransporterCode");
        clearTextValues("txtTransporterCode");
        clearTextValues("txtTransporterDescription");
        clearTextValues("txtContactPerson");
        clearTextValues("txtContactAddress");
        clearTextValues("txtZipCode");
        clearTextValues("txtPhoneNo");
        clearTextValues("txtFax");
        clearTextValues("txtEmailID");
        setPageID("0");
        el("chkActive").checked = true;
        //        el("chkDefault").checked = false;
        setPageMode("new");
        resetValidators();
        setReadOnly('txtTransporterCode', false);
    }
    else {
        setReadOnly('txtTransporterCode', true);
        setPageMode("edit");
    }
    bindRouteDetail(sMode);
    reSizePane();
}

function submitPage(sMode) {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        var sMode = getPageMode();
        if (ifgRouteDetail.Submit(true) == false || _RowValidationFails) {
            return false;
        }
        if (sMode == MODE_NEW) {
            createTransporter();
        }
        else if (sMode == MODE_EDIT) {
            updateTransporter();
        }
    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

function createTransporter() {
    var oCallback = new Callback();
    oCallback.add("TransporterCode", el('txtTransporterCode').value);
    oCallback.add("TransporterDescription", el('txtTransporterDescription').value);
    oCallback.add("ContactPerson", el('txtContactPerson').value);
    oCallback.add("ContactAddress", el('txtContactAddress').value);
    oCallback.add("ZipCode", el('txtZipCode').value);
    oCallback.add("PhoneNo", el('txtPhoneNo').value);
    oCallback.add("Fax", el('txtFax').value);
    oCallback.add("Email", el('txtEmailID').value);
    oCallback.add("ActiveBit", el("chkActive").checked);
    //  oCallback.add("DefaultBit", el("chkDefault").checked);
    oCallback.invoke("Transporter.aspx", "CreateTransporter");
    if (oCallback.getCallbackStatus()) {
        setPageMode("edit");
        setPageID(oCallback.getReturnValue("TransporterId"));
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        resetHasChanges("ifgRouteDetail");
        setReadOnly("txtTransporterCode", true);
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function updateTransporter() {
    var oCallback = new Callback();
    oCallback.add("TransporterID", getPageID());
    oCallback.add("TransporterCode", el('txtTransporterCode').value);
    oCallback.add("TransporterDescription", el('txtTransporterDescription').value);
    oCallback.add("ContactPerson", el('txtContactPerson').value);
    oCallback.add("ContactAddress", el('txtContactAddress').value);
    oCallback.add("ZipCode", el('txtZipCode').value);
    oCallback.add("PhoneNo", el('txtPhoneNo').value);
    oCallback.add("Fax", el('txtFax').value);
    oCallback.add("Email", el('txtEmailID').value);
    oCallback.add("ActiveBit", el("chkActive").checked);
    //  oCallback.add("DefaultBit", el("chkDefault").checked);
    oCallback.invoke("Transporter.aspx", "UpdateTransporter");
    if (oCallback.getCallbackStatus()) {
        setPageMode("edit");
        setPageID(oCallback.getReturnValue("TransporterId"));
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        resetHasChanges("ifgRouteDetail");
        bindRouteDetail('ReBind');
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function bindRouteDetail(mode) {
    var ifgRouteDetail = new ClientiFlexGrid("ifgRouteDetail");
    ifgRouteDetail.parameters.add("WFDATA", el("WFDATA").value);
    ifgRouteDetail.parameters.add("TransporterID", getPageID());
    ifgRouteDetail.parameters.add("Mode", mode);
    ifgRouteDetail.DataBind();
    $('.btncorner').corner();
}

function onAfterCallBackRouteDetail(param, mode) {
    if (typeof (param["Error"]) != 'undefined') {
        showErrorMessage(param["Error"]);
    }
}

function validateTransporterCode(oSrc, args) {
    var oCallback = new Callback();
    var valid;
    oCallback.add("Code", el("txtTransporterCode").value);
    oCallback.invoke("Transporter.aspx", "ValidateCode");
    if (oCallback.getCallbackStatus()) {
        valid = oCallback.getReturnValue("valid");
        if (valid == "true") {
            oSrc.errormessage = "";
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = "Route Code Already Exist.";
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

//function checkDefault(obj) {
//    if (obj.checked) {
//        var oCallback = new Callback();
//        oCallback.invoke("Transporter.aspx", "CheckDefault");
//        if (oCallback.getCallbackStatus()) {
//            valid = oCallback.getReturnValue("valid");
//            if (valid != "true" && oCallback.getReturnValue("Code") != el('txtTransporterCode').value) {
//                obj.checked = false;
//                showInfoMessage("Already Default Setting Enabled for Transporter Code: " + oCallback.getReturnValue("Code"));
//            }
//        }
//        else {
//            showErrorMessage(oCallback.getCallbackError());
//        }
//        oCallback = null;
//        HasChanges = true;
//    }
//}

function validateRouteCode(oSrc, args) {
    var rIndex = ifgRouteDetail.VirtualCurrentRowIndex();
    var cols = ifgRouteDetail.Rows(rIndex).GetClientColumns();
    var i;
    var sPort = cols[0];
    var icount = ifgRouteDetail.Rows().Count
    var colscheck;

    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgRouteDetail.Rows(i).GetClientColumns();
                if (sPort == colscheck[0]) {
                    args.IsValid = false;
                    oSrc.errormessage = "Route Code already exists";
                    return;
                }
            }
        }
    }
    //    var rowState = ifgRouteDetail.ClientRowState();
    //    var oCallback = new Callback();
    //    oCallback.add("Code", sPort);
    //    oCallback.add("GridIndex", rIndex);   
    //    oCallback.add("RowState", rowState);
    //    oCallback.add("TransporterCode", el('txtTransporterCode').value);
    //    oCallback.add("WFDATA", el("WFDATA").value);
    //    oCallback.invoke("Transporter.aspx", "ValidateRouteCode");
    //    if (oCallback.getCallbackStatus()) {
    //        var valid = oCallback.getReturnValue("pkValid");
    //        if (valid == "true" || valid == "") {
    //            args.IsValid = true;
    //        }
    //        else {
    //            oSrc.errormessage = valid;
    //            args.IsValid = false;
    //        }
    //    }
    //    else {
    //        showErrorMessage(oCallback.getCallbackError());
    //    }
    //    oCallback = null;
    //    oCols = null;
}

function ValidatePickupLocation(oSrc, args) {
    var rIndex = ifgRouteDetail.CurrentRowIndex();
    var cColumns = ifgRouteDetail.Rows(rIndex).GetClientColumns();
    if (cColumns[2] != "" && cColumns[2] == cColumns[6]) {
        oSrc.errormessage = "Pickup Location and Drop Off Location cannot be same";
        args.IsValid = false;
        return;
    }
}
function ValidateDropOffLocation(oSrc, args) {
    var rIndex = ifgRouteDetail.CurrentRowIndex();
    var cColumns = ifgRouteDetail.Rows(rIndex).GetClientColumns();
    if (cColumns[2] == "") {
        oSrc.errormessage = "Pick Up Location Required";
        args.IsValid = false;
        return;
    }
    if (cColumns[2] != "" && cColumns[2] == cColumns[6]) {
        oSrc.errormessage = "Pickup Location and Drop Off Location cannot be same";
        args.IsValid = false;
        return;
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

function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgRouteDetail.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgRouteDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(4, "0.00");
        ifgRouteDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(5, "0.00");
        ifgRouteDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(6, "0.00");
        ifgRouteDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(7, "0.00");
    }
}

function onOpenCopyRoute() {
    showModalWindow("Copy Route", "Masters/TransporterCopyRoute.aspx?TransporterCode=" + el('txtTransporterCode').value, "500px", "270px", "100px", "", "", "", "");
    psc().hideLoader();
}

$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 768) {
            el("tabTransporter").style.height = $(window.parent).height() - 380 + "px";
            if (el("ifgRouteDetail") != null) {
                el("ifgRouteDetail").SetStaticHeaderHeight($(window.parent).height() - 541 + "px");
            }
        }
        else {
            el("tabTransporter").style.height = $(window.parent).height() - 477 + "px";
            if (el("ifgRouteDetail") != null) {
                el("ifgRouteDetail").SetStaticHeaderHeight($(window.parent).height() - 638 + "px");
            }
        }
    }

}