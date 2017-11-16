var HasChanges = false;
var vrGridIds = new Array('tabCleaningMethod;ifgCleaningMethodDetail');
var _RowValidationFails = false;
if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
//Initialize page while loading the page based on page mode
function initPage(sMode) {
    reSizePane();
    if (sMode == MODE_NEW) {
        clearTextValues("txtType");
        clearTextValues("txtDescription");
        setPageID("0");
        el("chkActive").checked = true;
        setPageMode("new");
        resetValidators();
        setReadOnly('txtType', false);
        setFocusToField("txtType");
    }
    else {
        setFocusToField("txtDescription");
        setReadOnly('txtType', true);
        setPageMode("edit");
    }
    bindCleaningTypeDetail(sMode);
    el('iconFav').style.display = "none";
    $('.btncorner').corner();

}
//This method is used to bind cleaning Method detail grid
function bindCleaningTypeDetail(mode) {
    var ifgCleaningMethodDetail = new ClientiFlexGrid("ifgCleaningMethodDetail");
    ifgCleaningMethodDetail.parameters.add("WFDATA", el("WFDATA").value);
    ifgCleaningMethodDetail.parameters.add("CleaningMethodTypeID", getPageID());
    ifgCleaningMethodDetail.parameters.add("Mode", mode);
    ifgCleaningMethodDetail.DataBind();
    $('.btncorner').corner();
}
//This method is used for submit action
function submitPage(sMode) {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        var sMode = getPageMode();
        if (ifgCleaningMethodDetail.Submit() == false || _RowValidationFails) {
            return false;
        }
        if (sMode == MODE_NEW) {
            createCleaningMethod();
        }
        else if (sMode == MODE_EDIT) {
            updateCleaningMethod();
        }
    }
    else {
        showInfoMessage('No Changes to Save');
    }
}
//This method is used to create cleaning method
function createCleaningMethod() {
    var oCallback = new Callback();
    oCallback.add("Type", el("txtType").value);
    oCallback.add("Description", el("txtDescription").value);
    oCallback.add("ActiveBit", el("chkActive").checked);
    oCallback.invoke("CleaningMethod.aspx", "CreateCleaningMethod");

    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setPageID(oCallback.getReturnValue("ID"));
        bindCleaningTypeDetail(MODE_EDIT);
        resetHasChanges("ifgCleaningMethodDetail");
        setReadOnly("txtType", true);
        setFocusToField("txtDescription");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}
//This method is used to update cleaning method
function updateCleaningMethod() {
    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("Type", el("txtType").value);
    oCallback.add("Description", el("txtDescription").value);
    oCallback.add("ActiveBit", el("chkActive").checked);
    oCallback.add("wfData", el("WFDATA").value);
    oCallback.invoke("CleaningMethod.aspx", "UpdateCleaningMethod");

    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setPageID(oCallback.getReturnValue("ID"));
        bindCleaningTypeDetail(MODE_EDIT);
        resetHasChanges("ifgCleaningMethodDetail");
        setReadOnly("txtType", true);
        setFocusToField("txtDescription");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}
//custom validation to check Type duplicates
function validateCleaningMethodType(oSrc, args) {
    var oCallback = new Callback();
    oCallback.add("Type", el("txtType").value);
    oCallback.invoke("CleaningMethod.aspx", "ValidateType");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("Message") != "") {
            args.IsValid = false;
            oSrc.errormessage = oCallback.getReturnValue("Message");
            return;
        }
    }
    oCallback = null;
    return true;
}
//to check duplicate types
function OnAfterCallBack(param, mode) {
    if (typeof (param["Duplicate"]) != "undefined") {
        showErrorMessage(param["Duplicate"]);
    }
}
$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window.parent).height() < 680) {
        el("tabCleaningMethod").style.height = $(window.parent).height() - 373 + "px";
    }
    else if ($(window.parent).height() < 768) {
        el("tabCleaningMethod").style.height = $(window.parent).height() - 380 + "px";
    }
    else {
        el("tabCleaningMethod").style.height = $(window.parent).height() - 481 + "px";
        if (el("ifgCleaningMethodDetail") != null) {
            el("ifgCleaningMethodDetail").SetStaticHeaderHeight($(window.parent).height() - 561 + "px");
        }
    }

}
