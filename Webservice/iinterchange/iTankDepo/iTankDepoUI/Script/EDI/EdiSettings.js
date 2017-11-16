var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgEdiSettings');
if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
function initPage(sMode) {
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    bindEDISettingsDetail();
    reSizePane();
}

function bindEDISettingsDetail() {
    var objGrid = new ClientiFlexGrid("ifgEdiSettings");
    objGrid.DataBind();
}

function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    ifgEdiSettings.Submit();
    if (getPageChanges()) {
        if (ifgEdiSettings.Submit() == false) {
            return false;
        }
        return UpdateEdiSettings();
    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

function UpdateEdiSettings() {
    var oCallback = new Callback();
  //  oCallback.add("ActivityName", getQueryStringValue(document.location.href, "activityname"));
    oCallback.invoke("EdiSettings.aspx", "UpdateEdiSettings");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        resetHasChanges("ifgEdiSettings");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    bindEDISettingsDetail();
    return true;
}


function OnAfterCallBack(param, mode) {
    if (mode == "Delete") {
        if (typeof (param["Delete"]) != 'undefined') {
            showWarningMessage(param["Delete"]);
            return;
        }
    }

}

function validateCustomer(oSrc, args) {

    var cols = ifgEdiSettings.Rows(ifgEdiSettings.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgEdiSettings.rowIndex;
    var sCustomer = args.Value;

    var oCallback = new Callback();
    var valid;
    var oCallback = new Callback();
 //   oCallback.add("FileType", sCustomer);
    oCallback.add("Customer", cols[0]);
    oCallback.add("CustomerID", cols[1]);
    oCallback.invoke("EdiSettings.aspx", "ValidateCustomer");
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

function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent) != undefined) {
            if ($(window.parent).height() < 680) {
                el("divEdiSettings").style.height = $(window.parent).height() - 249 + "px";
                //                el("tabEquipmentUpdate").style.height = "980px";
                if (el("ifgEdiSettings") != null) {
                    el("ifgEdiSettings").SetStaticHeaderHeight($(window.parent).height() - 304 + "px");
                }
            }
            else if ($(window.parent).height() < 768) {
                el("divEdiSettings").style.height = $(window.parent).height() - 350 + "px";
                if (el("ifgEdiSettings") != null) {
                    el("ifgEdiSettings").SetStaticHeaderHeight($(window.parent).height() - 454 + "px");
                }
            }
            else {
                el("divEdiSettings").style.height = $(window.parent).height() - 254 + "px";
                if (el("ifgEdiSettings") != null) {
                    el("ifgEdiSettings").SetStaticHeaderHeight($(window.parent).height() - 313 + "px");
                }
            }
        }
    }
}
