// JScript File


// called when the submit button clicked
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        ppsc().gsUIMode = "RoleRights";
        ppsc().loadingWorkflowPane("Submitting Role Rights..");
        el('btnSubmitDummy').click();
    } else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

//This methods is to validate the duplicate code entered
function validateRoleCode(oSrc, args) {
    var oCallback = new Callback();
    oCallback.add("RL_CD", el("txtRoleCode").value);
    if (el("hdnPageMode").value == MODE_NEW) {
        oCallback.add("RL_ID", 0);
    }
    else {
        oCallback.add("RL_ID", el("hdnRoleId").value);
    }
    oCallback.invoke("RoleRight.aspx", "validateRoleCode");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("pkValid") == "true") {
            args.IsValid = true;
        }
        else {
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function initPage(sMode) {
    var header = "Admin >> Role Rights";
    var res;
    if (sMode == "new") {
        res = header.concat(" -  New");
    }
    else if (sMode == "edit") {
        res = header.concat(" - Edit");
    }
    el("spnHeader").innerHTML = res;
    if (sMode == MODE_NEW) {       
        setReadOnly('txtRoleCode', false);
        setFocusToField("txtRoleCode");
    }
    else {
        setReadOnly('txtRoleCode', true);       
        setFocusToField("txtRoleDescription");
    }
    reSizePane();
}

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}

try{
$(window.parent).resize(function () {
    reSizePane();
});
} catch (e) { }

function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent) != undefined) {
            if ($(window.parent).height() < 670) {
                el("tabRoleRights").style.height = $(window.parent).height() - 360 + "px";
                el("divTree").style.height = $(window.parent).height() - 413 + "px";
            }
            else if ($(window.parent).height() < 768) {
                el("tabRoleRights").style.height = $(window.parent).height() - 243 + "px";
            }
            else {
                el("tabRoleRights").style.height = $(window.parent).height() - 460 + "px";
                
                el("divTree").style.height = $(window.parent).height() - 519 + "px";
                
            }
        }
    }
}
