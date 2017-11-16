var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgRepairInvoiceUpdate');
var _RowValidationFails = false;


//This function is used to intialize the page.
function initPage(sMode) {

    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    bindRepairInvoiceUpdateGrid();
    reSizePane();
}


function bindRepairInvoiceUpdateGrid() {
    var ifgRepairInvoiceUpdate = new ClientiFlexGrid("ifgRepairInvoiceUpdate");
    ifgRepairInvoiceUpdate.DataBind();
}

function submitPage() {
    var oCallback = new Callback();
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        if (ifgRepairInvoiceUpdate.Submit() == false || _RowValidationFails) {
            return false;
        }
        oCallback.invoke("RepairInvoiceUpdate.aspx", "ValidateCheckBox");
        if (oCallback.getCallbackStatus()) {
            ifgRepairInvoiceUpdate.Submit();
            return onClickUpdate();
        }
        else {
            showErrorMessage(oCallback.getCallbackError());
        }
        oCallback = null;

    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

function onClickUpdate() {

    if (checkRights()) {

        var oCallback = new Callback();
        oCallback.add("ActivityId", getQueryStringValue(document.location.href, "activityid"));
        oCallback.invoke("RepairInvoiceUpdate.aspx", "ActivitySubmit");
        if (oCallback.getCallbackStatus()) {
            if (oCallback.getReturnValue("ActivitySubmit") == "True") {
                showConfirmMessage("Are you sure you want to update this activity. Click 'Yes' to update or 'No' to ignore",
                                "wfs().yesClick();", "wfs().noClick();");
            }
            else {
                showErrorMessage(oCallback.getReturnValue("ActivitySubmit"));
                return false;
            }
        }
    }
}

function yesClick() {
    showModalWindow("Repair Invoice Update Reason", "Operations/RepairInvoiceUpdateReason.aspx?activityname=" + getQueryStringValue(document.location.href, "activityname"), "560px", "200px", "100px", "", "");
    psc().hideLoader();
    return true;
}

function noClick() {
    return false;
}

function updateReason() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    updateRepairCharge(el('txtReason').value);
}


//This method used to update/insert entered row to database
function updateRepairCharge(Reason) {
    var activityName = "";  
    activityName = getQueryStringValue(document.location.href, "activityname");
    var oCallback = new Callback();
    oCallback.add("Reason", Reason);
    oCallback.add("ActivityName", activityName);
    oCallback.invoke("RepairInvoiceUpdate.aspx", "UpdateRepairCharge");
    if (oCallback.getCallbackStatus()) {
        ppsc().closeModalWindow();
        if (oCallback.getReturnValue("ActivitySubmit") == "" || oCallback.getReturnValue("ActivitySubmit") == null) {
            showInfoMessage(oCallback.getReturnValue("Message"));
        }
        else {
            activitySubmitMessage(oCallback.getReturnValue("ActivitySubmit"));
        }
        HasChanges = false;
        pdfs("wfFrame" + pdf("CurrentDesk")).bindRepairInvoiceUpdateGrid();
        resetHasChanges("ifgRepairInvoiceUpdate");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

function formatAmount(obj) {
    var Amount = new Number;
    Amount = parseFloat(obj.value);
    obj.value = Amount.toFixed(2);
}

function activitySubmit() { //Activity Submit
    var oCallback = new Callback();
    oCallback.add("ActivityId", getQueryStringValue(document.location.href, "activityid"));
    oCallback.invoke("RepairInvoiceUpdate.aspx", "ActivitySubmit");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ActivitySubmit") == "True") {
            return true;
        }
        else {
            showErrorMessage(oCallback.getReturnValue("ActivitySubmit"));
            return false;
        }
    }
    oCallback = null;
    return true;
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
      //  if ($(window.parent).style != undefined) {
            if ($(window.parent).height() < 680) {
                el("divRepairInvoiceUpdate").style.height = $(window.parent).height() - 237 + "px";
                if (el("ifgRepairInvoiceUpdate") != null) {
                    el("ifgRepairInvoiceUpdate").SetStaticHeaderHeight($(window.parent).height() - 295 + "px");
                }
            }
            else if ($(window.parent).height() < 768) {
                el("divRepairInvoiceUpdate").style.height = $(window.parent).height() - 350 + "px";
                if (el("ifgRepairInvoiceUpdate") != null) {
                    el("ifgRepairInvoiceUpdate").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
                }
            }
            else {
                el("divRepairInvoiceUpdate").style.height = $(window.parent).height() - 241 + "px";
                if (el("ifgRepairInvoiceUpdate") != null) {
                    el("ifgRepairInvoiceUpdate").SetStaticHeaderHeight($(window.parent).height() - 290 + "px");
                }
            }
       // }
    }
}