var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgConfigurationTemplate');
var _RowValidationFails = false;
//This function is used to intialize the page.


function initPage(sMode) {
   
    bindConfigurationTemplate();
    setFocusToField("lkpDepotCode");
    hideDiv("lnkReset");
    hideDiv("divDetail");
}

function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        if (ifgConfigurationTemplate.Submit(true) == false || _RowValidationFails) {
            return false;
        }
        return createConfigurationTemplate();
    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

function createConfigurationTemplate() {
    var oCallback = new Callback();
    oCallback.add("Depot", el("lkpDepotCode").SelectedValues[0]);        
    oCallback.invoke("Settings.aspx", "CreateConfigurationTemplate");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        resetHasChanges("ifgConfigurationTemplate");       
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function bindConfigurationTemplate(mode) {
    var objGrid = new ClientiFlexGrid("ifgConfigurationTemplate");
    if (mode)
        objGrid.parameters.add("DepotID", el("lkpDepotCode").SelectedValues[0]);
    objGrid.DataBind();
}

function ifgConfigurationTemplateOnAfterCallBack(param, mode) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        el("divConfigurationTemplate").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divConfigurationTemplate").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}

function refresh() {
    hideDiv("divDetail");
    showDiv("lnkFetch");
    hideDiv("lnkReset");    
}

function onResetClick() {
    clearLookupValues("lkpDepotCode");
    resetHasChanges("ifgConfigurationTemplate");
    setReadOnly('lkpDepotCode', false);
    hideDiv("divDetail");   
    showDiv("lnkFetch");
    hideDiv("lnkReset");    
    hideMessage();
}

function onFetchClick() {
    if (el("lkpDepotCode").value == "") {
        showErrorMessage("Depot Required.")
        setFocusToField("lkpDepotCode");
        return;
    }
    bindConfigurationTemplate("Bind");
    setReadOnly('lkpDepotCode', true);
    showDiv("divDetail");
    showDiv("lnkReset");
    hideDiv("lnkFetch");
    reSizePane();
}
$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 670) {
            el("tabSettings").style.height = $(window.parent).height() - 254 + "px";
            if (el("ifgConfigurationTemplate") != null) {
                el("ifgConfigurationTemplate").SetStaticHeaderHeight($(window.parent).height() - 376 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("tabSettings").style.height = $(window.parent).height() - 330 + "px";
            if (el("ifgConfigurationTemplate") != null) {
                el("ifgConfigurationTemplate").SetStaticHeaderHeight($(window.parent).height() - 480 + "px");
            }

        }
        else {
            el("tabSettings").style.height = $(window.parent).height() - 250 + "px";
            if (el("ifgConfigurationTemplate") != null) {
                el("ifgConfigurationTemplate").SetStaticHeaderHeight($(window.parent).height() - 378 + "px");
            }

        }
    }

}

function filterHQDepot() {
    return " ORGNZTN_TYP_ID=153 ";
}