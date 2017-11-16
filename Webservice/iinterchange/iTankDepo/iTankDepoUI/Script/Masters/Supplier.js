var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgContractDetails');
var _RowValidationFails = false;

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
function initPage(sMode) {
    if (sMode == MODE_NEW) {
        clearTextValues("txtCode");
        clearTextValues("txtDescription");
        setReadOnly("txtCode", false);
        el("chkActive").checked = true;
        setPageMode("new");
        setPageID("0");//Added by Sakthivel on 14-OCT-2014 for Inserting New mode
        bindContractDetail();
        resetValidators();
    }
    else {
        showSubmitButton(true);
        resetValidatorByGroup("tabCustomer");
        resetValidatorByGroup("divContractDetails");
        setReadOnly("txtCode", true);
        setPageMode("edit");
        bindContractDetail();
        resetValidators();
    }
    el('iconFav').style.display = "none";
    $('.btncorner').corner();
    setFocus();
    reSizePane();
}
function bindContractDetail() {
    var ifgContractDetails = new ClientiFlexGrid("ifgContractDetails");
    ifgContractDetails.parameters.add("WFDATA", el("WFDATA").value);
    ifgContractDetails.parameters.add("SupplierID", getPageID());
    ifgContractDetails.DataBind();
    $('.btncorner').corner();
}
function setFocus() {
    var sMode = getPageMode();
    if (sMode == MODE_NEW) {
        setFocusToField("txtCode");
    }
    else if (sMode == MODE_EDIT) {
        setFocusToField("txtDescription");
    }
}
function bindEquipmentDetail() {
    var ifgEquipmentDetails = new ClientiFlexGrid("ifgEquipmentDetails");
    ifgEquipmentDetails.parameters.add("WFDATA", el("WFDATA").value);
    ifgEquipmentDetails.parameters.add("SupplierID", getPageID());
    ifgEquipmentDetails.DataBind();
    $('.btncorner').corner();
}
function ValidateStartDate(oSrc, args) {
    var cols = ifgContractDetails.Rows(ifgContractDetails.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgContractDetails.rowIndex;
        if (cols[1] == "") {
            oSrc.errormessage = "Start Date Required";
            args.IsValid = false;
            return;
        }
        //UIG issue fix for Chrome not accepting ""
        if (cols[2] != "" && cols[2] != null) {
           
      
        var oCallback = new Callback();
        oCallback.add("StartDate", cols[1]);
        oCallback.add("EndDate", cols[2]);
        oCallback.invoke("Supplier.aspx", "ValidateStartDate");

        if (oCallback.getCallbackStatus()) {
            if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {
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

}
function ValidateEndDate(oSrc, args) {
    var cols = ifgContractDetails.Rows(ifgContractDetails.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgContractDetails.rowIndex;
    var rIndex = ifgContractDetails.CurrentRowIndex();
    if (cols[1] != "") {
        //        oSrc.errormessage = "Contract Start Date Required";
        //        ifgContractDetails.Rows(rIndex).SetFocusInColumn(1); 

        if (cols[2] != "") {
            var oCallback = new Callback();
            oCallback.add("StartDate", cols[1]);
            oCallback.add("EndDate", cols[2]);
            oCallback.invoke("Supplier.aspx", "ValidateEndDate");

            if (oCallback.getCallbackStatus()) {
                if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {
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
    }
}
function submitPage() {
        GetLookupChanges();
        Page_ClientValidate();
                if (!Page_IsValid) {
                    return false;
                }

                if (getPageChanges()) {
                    var sMode = getPageMode();
                    if (ifgContractDetails.Submit(true) == false || _RowValidationFails) {
                        return false;
                    }
                    if (sMode == MODE_NEW) {
                        createSupplier();
                    }
                    else if (sMode == MODE_EDIT) {
                        updateSupplier();
                    }
                }
                else {
                    showInfoMessage('No Changes to Save');
                    setFocus();
                }
    return true;
}

function createSupplier() {
    var oCallback = new Callback();
    oCallback.add("Code", el("txtCode").value);
    oCallback.add("Description", el("txtDescription").value);
    oCallback.add("Active", el("chkActive").checked);
    oCallback.add("wfData", el("WFDATA").value);
    oCallback.invoke("Supplier.aspx", "CreateSupplier");

    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setPageID(oCallback.getReturnValue("ID"));
        bindContractDetail();
//        bindEquipmentDetail();
        resetHasChanges("ifgContractDetails");
        resetHasChanges("ifgEquipmentDetails");
        setReadOnly("txtCode", true);
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}
function updateSupplier() {
    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("Code", el("txtCode").value);
    oCallback.add("Description", el("txtDescription").value);
    oCallback.add("Active", el("chkActive").checked);
    oCallback.add("wfData", el("WFDATA").value);
    oCallback.invoke("Supplier.aspx", "UpdateSupplier");

    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindContractDetail();
//        bindEquipmentDetail();
        resetHasChanges("ifgContractDetails");
        resetHasChanges("ifgEquipmentDetails");
        setReadOnly("txtCode", true);
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}
function showEquipmentDetails() {

    if (ifgContractDetails.Submit() == false) {
        return false;
    }
    else {
        var _rlngth = ifgContractDetails.Rows().Count;
        if ((ifgContractDetails.rowIndex) == "") {
            if ((parseInt(_rlngth) - 1) == (ifgContractDetails.rowIndex)) {
                onClickAddEdit();
            }
        }
        else {
            onClickAddEdit();
        }
    }
}
function onClickAddEdit() {
    var cols = ifgContractDetails.Rows(ifgContractDetails.rowIndex).Columns();
    if (cols.length == 1) {
        if (ifgContractDetails.Submit(true) == false) {
            return false;
        }
    } else {
        ifgContractDetails.Submit(true);
        showModalWindow("Equipment Details - " + cols[2], "Masters/SupplierEquipmentDetail.aspx?SupplierID=" + getPageID() + "&rowIndex=" + ifgContractDetails.rowIndex + "&ContractRefNo=" + cols[2] + "&SPPLR_CNTRCT_DTL_ID=" + cols[0] + "&ContractEndDate=" + cols[4], "550px", "350px", "100px", "", "");
        psc().hideLoader();
    }
}
function validateSupplierCode(oSrc, args) {
    var oCallback = new Callback();
    var valid;
    oCallback.add("Code", el("txtCode").value);

    oCallback.invoke("Supplier.aspx", "ValidateCode");

    if (oCallback.getCallbackStatus()) {
        valid = oCallback.getReturnValue("valid");
        if (valid == "true") {
            oSrc.errormessage = "";
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = valid;
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

function checkDuplicateContractRef(oSrc, args) {
    var rIndex = ifgContractDetails.VirtualCurrentRowIndex();
    var cols = ifgContractDetails.Rows(rIndex).GetClientColumns();
    var i;
    var sPort = cols[0];
    var icount = ifgContractDetails.Rows().Count
    var colscheck;

    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgContractDetails.Rows(i).GetClientColumns();
                if (sPort == colscheck[0]) {
                    args.IsValid = false;
                    oSrc.errormessage = "Contract Reference Number already exists";
                    return;
                }
            }
        }
    }
    var oCallback = new Callback();
    var valid;
    var rIndex = ifgContractDetails.VirtualCurrentRowIndex();
    var cols = ifgContractDetails.Rows(rIndex).GetClientColumns();
    var rowState = ifgContractDetails.ClientRowState();
    oCallback.add("ReferenceNo",cols[0]);
    oCallback.add("SupplierCode", el("txtCode").value);
    oCallback.add("RowState", rowState);
    oCallback.invoke("Supplier.aspx", "ValidateContractReferenceNo");
    if (oCallback.getCallbackStatus()) {
        valid = oCallback.getReturnValue("valid");
        if (valid == "true" || (valid=="" && valid== null)) {
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = valid;
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//function checkDuplicateContractRef(oSrc, args) {
//    var rIndex = ifgContractDetails.VirtualCurrentRowIndex();

//    var cols = ifgContractDetails.Rows(rIndex).GetClientColumns();
//    var i;
//    var sPort = cols[0];
//    var icount = ifgContractDetails.Rows().Count
//    var colscheck;
//    if (icount > 0) {
//        for (i = 0; i < icount; i++) {
//            if (rIndex != i) {
//                colscheck = ifgContractDetails.Rows(i).GetClientColumns();
//                if (sPort == colscheck[0]) {
//                    args.IsValid = false;
//                    oSrc.errormessage = "This Contract Reference No already exists";
//                    return;
//                }
//            }
//        }
//    }
//}

function onAfterCB(param) {
    if (typeof (param["Delete"]) != 'undefined') {
        showWarningMessage(param["Delete"]);
    }
    else if (typeof (param["Update"]) != 'undefined' && param["Update"] != '') {
        showWarningMessage(param["Update"]);
    }
    else {
        hideMessage();
    }
}
$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 768) {
            el("tabSupplier").style.height = $(window.parent).height() - 370 + "px";
            if (el("ifgContractDetails") != null) {
                el("ifgContractDetails").SetStaticHeaderHeight($(window.parent).height() - 482 + "px");
            }
        }
        else {
            el("tabSupplier").style.height = $(window.parent).height() - 481 + "px";
            if (el("ifgContractDetails") != null) {
                el("ifgContractDetails").SetStaticHeaderHeight($(window.parent).height() - 593 + "px");
            }
        }
    }

}