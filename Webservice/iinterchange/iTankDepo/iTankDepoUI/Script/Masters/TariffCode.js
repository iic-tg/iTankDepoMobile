var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgTariffCode');


//Initialize page while loading the page based on page mode
function initPage(sMode) {
    bindGrid();
    setDefaultValues();
    var sPageTitle = getPageTitle();
    el("hdnPageTitle").value = sPageTitle;
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    el('iconFav').style.display = "none";
    reSizePane();
}

//This method used to submit the changes to database
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    //To submit the changes to server
    if (ifgTariffCode.Submit(true) == false) {
        return false;
    }
    else {
        if (getPageChanges()) {
            updateTariffCode();
        }
        else {
            showInfoMessage('No Changes to Save');
        }
    }
    return true;
}

//This method used to update/insert entered row to database
function updateTariffCode() {
    var oCallback = new Callback();
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("TariffCode.aspx", "UpdateTariffCode");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindGrid();
        resetHasChanges("ifgTariffCode");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method is to validate the duplicate code entered
function validateCode(oSrc, args) {
    var iRowIndex = ifgTariffCode.rowIndex;
    var oCols = ifgTariffCode.Rows(iRowIndex).GetClientColumns();
    var sCols = ifgTariffCode.Rows(iRowIndex).Columns();
    var sCode = oCols[0];

    var rowState = ifgTariffCode.ClientRowState();
    if (rowState != 'Added') {
        if (sCols[1] == sCode) {
            return false;
        }
    }

    var oCallback = new Callback();
    oCallback.add("Code", sCode);
    oCallback.add("GridIndex", ifgTariffCode.VirtualCurrentRowIndex());
    //Newly added code is available in existing data only in database.
    oCallback.add("RowState", rowState);
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("TariffCode.aspx", "ValidateCode");
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
    oCols = null;
}

//This method is used to show warning message after a row is deleted
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

function bindGrid() {
    var sPageTitle = getPageTitle();
    var ifgTariffCode = new ClientiFlexGrid("ifgTariffCode");
    ifgTariffCode.parameters.add("WFDATA", el("WFDATA").value);
    ifgTariffCode.DataBind();
    //Fix for BreadCrums
    // $("#spnHeader").text(sPageTitle);
    $("#spnHeader").text("Master >>Repair Process >>" + sPageTitle);
}

// Check "Active" check box in page load for first column
// as well as when a new row added.
function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgTariffCode.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgTariffCode.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    if (ifgTariffCode.Rows().Count == "0")
        ifgTariffCode.Rows(0).SetColumnValuesByIndex(4, true);
    else if (ISNullorEmpty(iCurrentIndex) == false)
        ifgTariffCode.Rows(iCurrentIndex).SetColumnValuesByIndex(4, true);
    resetHasChanges("ifgTariffCode");
    return true;
    }

function resetGridChanges() {
    resetHasChanges("ifgTariffCode");
}

function openUpload() {
    if (checkRights()) {
        var sPageTitle = getPageTitle();
        var SchemaId = getQStr("activityid");
        var tablename = getQStr("tablename");
        showModalWindow(sPageTitle + " - Upload", "Upload.aspx?SchemaID=" + SchemaId + "&tablename=" + tablename + "&" + el("WFDATA").value, "700px", "460px", "", "", "");
    }
    else
        return false;
}

function SubItemFilter() {
    var iRowIndex = ifgTariffCode.rowIndex;
    var oCols = ifgTariffCode.Rows(iRowIndex).GetClientColumns();
    var str;
    str = "ITM_ID=" + oCols[3];
    return str;
}

function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgTariffCode.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgTariffCode.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    if (ifgTariffCode.Rows().Count == "0")
        ifgTariffCode.Rows(0).SetColumnValuesByIndex(9, true);
    else if (ISNullorEmpty(iCurrentIndex) == false)
        ifgTariffCode.Rows(iCurrentIndex).SetColumnValuesByIndex(9, true);
    resetHasChanges("ifgTariffCode");
    return true;
}

function formatManHours(obj) {
    var ManHours = new Number;
    ManHours = parseFloat(obj.value);
    obj.value = ManHours.toFixed(2);
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
            el("tabTariffCode").style.height = $(window.parent).height() - 249 + "px";
            if (el("ifgTariffCode") != null) {
                el("ifgTariffCode").SetStaticHeaderHeight($(window.parent).height() - 304 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("tabTariffCode").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgTariffCode") != null) {
                el("ifgTariffCode").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }

        }
        else {
            el("tabTariffCode").style.height = $(window.parent).height() - 254 + "px";
            if (el("ifgTariffCode") != null) {
                el("ifgTariffCode").SetStaticHeaderHeight($(window.parent).height() - 308 + "px");
            }

        }
    }


}