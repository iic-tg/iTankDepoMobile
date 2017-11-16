var HasChanges = false;
var _RowValidationFails = false;
var vrGridIds = new Array('ITab1_0;ifgTariffGroupDetail');

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
//Initialize page while loading the page based on page mode
function initPage(sMode) {
    reSizePane();
    if (sMode == MODE_NEW) {
        clearTextValues("txtTRFF_GRP_CD");
        clearTextValues("txtTRFF_GRP_DSCRPTN");
        setReadOnly("txtTRFF_GRP_CD", false);
        setPageMode("new");
        setPageID("0");
        setFocus();
        bindGrid(sMode);
        resetValidators();
        setActionButtonFocus("txtTRFF_GRP_CD", "txtTRFF_GRP_DSCRPTN");
        el("chkActive").checked = true;
    }
    else {
        setReadOnly("txtTRFF_GRP_CD", true);
        setPageMode("edit");
        setActionButtonFocus("txtTRFF_GRP_CD", "txtTRFF_GRP_DSCRPTN");
        setFocus();
        bindGrid(sMode);
        resetValidators();
    }
    el('iconFav').style.display = "none";
    $('.btncorner').corner();
}
//This method get fired while taking action from submit pane
function submitPage() {
    GetLookupChanges();
    //ifgTariffGroupDetail.Submit(true);
    ifgTariffGroupDetail.Submit();
    if (ifgTariffGroupDetail.Search == true) {
        return false;
    }
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    
    if (getPageChanges()) {
        var sMode = getPageMode();
        if (ifgTariffGroupDetail.Submit(true) == false || _RowValidationFails) {
            return false;
        }
        if (sMode == MODE_NEW) {
            createTariffGroup(sMode);
        }
        else if (sMode == MODE_EDIT) {
            updateTariffGroup(sMode);
        }
    }
    else {
        showInfoMessage("No Changes to Save");
        setFocus();
    }
    return true;
}

function ValidateTariffGroupCode(oSrc, args) {
    var oCallback = new Callback();
    var validatedresult;
    oCallback.add("TariffGroupCode", el("txtTRFF_GRP_CD").value);
    oCallback.invoke("TariffGroup.aspx", "ValidateTariffGroup");
    if (oCallback.getCallbackStatus()) {
        validatedresult = oCallback.getReturnValue("pkValid");
        if (validatedresult == "true") {
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = "This Tariff Code already exists"
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

//This method get fired while creating new record
function createTariffGroup(sMode) {
    var oCallback = new Callback();
    oCallback.add("TariffGroupCode", el("txtTRFF_GRP_CD").value);
    oCallback.add("TariffGroupDescription", el("txtTRFF_GRP_DSCRPTN").value);
    oCallback.add("ActiveBit", el("chkActive").checked);
    oCallback.add("wfData", el("WFDATA").value);
    oCallback.invoke("TariffGroup.aspx", "InsertTariffGroup");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("checkTariffDetail")) {
            showErrorMessage("Atleast one Tariff Code must be entered");
            return;
        }
        showInfoMessage(oCallback.getReturnValue("Message"));
        setReadOnly("txtTRFF_GRP_CD", true);
        setPageID(oCallback.getReturnValue("ID"));
        setPageMode(MODE_EDIT);
        sMode = MODE_EDIT
        bindGrid(sMode);
        resetHasChanges("ifgTariffGroupDetail");
        HasChanges = false;
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method get fired while changing new record
function updateTariffGroup(sMode) {
    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("TariffGroupCode", el("txtTRFF_GRP_CD").value);
    oCallback.add("TariffGroupDescription", el("txtTRFF_GRP_DSCRPTN").value);
    oCallback.add("ActiveBit", el("chkActive").checked);
    oCallback.add("wfData", el("WFDATA").value);

    oCallback.invoke("TariffGroup.aspx", "UpdateTariffGroup");
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("checkTariffDetail")) {
            showErrorMessage("Atleast one Tariff Code should be selected");
            return;
        }
        sMode == MODE_EDIT
        bindGrid(sMode);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

//This method get fired while changing new record
function setFocus() {
    var sMode = getPageMode();
    if (sMode == MODE_NEW) {
        setFocusToField("txtTRFF_GRP_CD");
    }
    else if (sMode == MODE_EDIT) {
        setFocusToField("txtTRFF_GRP_DSCRPTN");
    }
}

function bindGrid(sMode) {
    var ifgTariffGroupDetail = new ClientiFlexGrid("ifgTariffGroupDetail");
    ifgTariffGroupDetail.parameters.add("MODE", sMode);
    ifgTariffGroupDetail.parameters.add("ID", getPageID());
    ifgTariffGroupDetail.parameters.add("WFDATA", el("WFDATA").value);
    ifgTariffGroupDetail.DataBind();
    $('.btncorner').corner();
}

function resetGridChanges() {
    resetHasChanges("ifgTariffGroupDetail");
}

function OnBeforeCallBack(mode, param) {
    if (mode == "Update" || mode == "Insert" || mode == "Delete") {
        param.add("mode", getPageMode());
        param.add("ID", getPageID());
    }
}

function OnAfterCallBack(param, mode) {
    //    if(typeof (param["Add"]) != 'undefined' && param["Add"] != ''){
    if (typeof (param["Duplicate"]) != 'undefined' && param["Duplicate"] != '') {
    if (mode == "Add" || mode == "Edit") {
        showErrorMessage(param["Duplicate"]);
        resetHasChanges("ifgTariffGroupDetail");
        return false;
    }
 }
    if (typeof (param["Delete"]) != 'undefined') {
        showWarningMessage(param["Delete"]);
        return;
    }
}
$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window.parent).height() < 680) {
        el("tabEstimate").style.height = $(window.parent).height() - 383 + "px";
        if (el("ifgTariffGroupDetail") != null) {
            el("ifgTariffGroupDetail").SetStaticHeaderHeight($(window.parent).height() - 476 + "px");
        }
    }
    else if ($(window.parent).height() < 768) {
        el("tabEstimate").style.height = $(window.parent).height() - 380 + "px";
    }
    else {
        el("tabEstimate").style.height = $(window.parent).height() - 473 + "px";
        if (el("ifgTariffGroupDetail") != null) {
            el("ifgTariffGroupDetail").SetStaticHeaderHeight($(window.parent).height() - 564 + "px");
        }
    }

}