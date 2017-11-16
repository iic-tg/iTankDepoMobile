var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgAdditionalCharge');


function initPage(sMode) {
    bindGrid();
    reSizePane();
    var sPageTitle = getPageTitle();
    el("hdnPageTitle").value = sPageTitle;
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    el('iconFav').style.display = "none";
}
function bindGrid() {
    var sPageTitle = getPageTitle();
    var ifgAdditionalCharge = new ClientiFlexGrid("ifgAdditionalCharge");
    ifgAdditionalCharge.parameters.add("WFDATA", el("WFDATA").value);
    ifgAdditionalCharge.DataBind();
    //Fix for BreadCrums
    //$("#spnHeader").text(sPageTitle);
    $("#spnHeader").text("Master >>Billing Process >>" + sPageTitle);
}
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    //To submit the changes to server
    if (ifgAdditionalCharge.Submit(true) == false) {
        return false;
    }
    else {
        if (getPageChanges()) {
            updateAdditionalCharge();
        }
        else {
            showInfoMessage('No Changes to Save');
        }
    }
    return true;
}
function updateAdditionalCharge() {
    var oCallback = new Callback();
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("AdditionalChargeRate.aspx", "UpdateAdditionalCharge");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindGrid();
        resetHasChanges("ifgAdditionalCharge");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}
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
function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgAdditionalCharge.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgAdditionalCharge.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
        ifgAdditionalCharge.Rows(iCurrentIndex).SetReadOnlyColumn(1, false);
    }
    if (ifgAdditionalCharge.Rows().Count == "0")
        ifgAdditionalCharge.Rows(0).SetColumnValuesByIndex(5, true);
    else if (ISNullorEmpty(iCurrentIndex) == false)
        ifgAdditionalCharge.Rows(iCurrentIndex).SetColumnValuesByIndex(5, true);
    resetHasChanges("ifgAdditionalCharge");
    return true;
}
function validateCharge(oSrc, args) {
    var iRowIndex = ifgAdditionalCharge.rowIndex;
    var oCols = ifgAdditionalCharge.Rows(iRowIndex).GetClientColumns();
    var sCols = ifgAdditionalCharge.Rows(iRowIndex).Columns();
    var sCode = oCols[2];
    var sOperation = oCols[0];
    if (sOperation == "") {
        args.IsValid = false;
        showErrorMessage("Operation Type Required");
    }
    var rowState = ifgAdditionalCharge.ClientRowState();
    if (rowState != 'Added') {
        if (sCols[3] == sCode) {
            return false;
        }
    }

    var oCallback = new Callback();
    oCallback.add("Code", sCode);
    oCallback.add("Operation", sOperation);
    oCallback.add("GridIndex", ifgAdditionalCharge.VirtualCurrentRowIndex());
    //Newly added code is available in existing data only in database.
    oCallback.add("RowState", rowState);
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("AdditionalChargeRate.aspx","ValidateCharge");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {//Chrome Fix
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
        if ($(window.parent).height() < 680) {
            el("tabAdditionalRate").style.height = $(window.parent).height() - 234 + "px";
            if (el("ifgAdditionalCharge") != null) {
                el("ifgAdditionalCharge").SetStaticHeaderHeight($(window.parent).height() - 288 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("tabAdditionalRate").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgAdditionalCharge") != null) {
                el("ifgAdditionalCharge").SetStaticHeaderHeight($(window.parent).height() - 454 + "px");
            }
        }
        else {
            el("tabAdditionalRate").style.height = $(window.parent).height() - 243 + "px";
            if (el("ifgAdditionalCharge") != null) {
                el("ifgAdditionalCharge").SetStaticHeaderHeight($(window.parent).height() - 297 + "px");
            }
        }
    }

}