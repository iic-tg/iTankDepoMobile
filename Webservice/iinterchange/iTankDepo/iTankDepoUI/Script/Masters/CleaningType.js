var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgCleaningType');


//Initialize page while loading the page based on page mode
function initPage(sMode) {
    bindGrid();
    reSizePane();
    setDefaultValues();
    var sPageTitle = getPageTitle();
    el("hdnPageTitle").value = sPageTitle;
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    el('iconFav').style.display = "none";
}

//This method used to submit the changes to database
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    //To submit the changes to server
    if (ifgCleaningType.Submit(true) == false) {
        return false;
    }
    else {
        if (getPageChanges()) {
            updateCleaningType();
        }
        else {
            showInfoMessage('No Changes to Save');
        }
    }
    return true;
}

//This method used to update/insert entered row to database
function updateCleaningType() {
    var oCallback = new Callback();
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("CleaningType.aspx", "UpdateCleaningType");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindGrid();
        resetHasChanges("ifgCleaningType");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method is to validate the duplicate code entered
function validateCode(oSrc, args) {
    var iRowIndex = ifgCleaningType.rowIndex;
    var oCols = ifgCleaningType.Rows(iRowIndex).GetClientColumns();
    var sCode = oCols[0];

    var sCols = ifgCleaningType.Rows(iRowIndex).Columns();
    var sCode = oCols[0];
    var rowState = ifgCleaningType.ClientRowState();
    if (rowState != 'Added') {
        if (sCols[1] == sCode) {
            return false;
        }
    }

    var oCallback = new Callback();
    oCallback.add("Code", sCode);
    oCallback.add("GridIndex", ifgCleaningType.VirtualCurrentRowIndex());
    //Newly added code is available in existing data only in database.
    oCallback.add("RowState", rowState);
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("CleaningType.aspx", "ValidateCode");
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
    var ifgCleaningType = new ClientiFlexGrid("ifgCleaningType");
    ifgCleaningType.parameters.add("WFDATA", el("WFDATA").value);
    ifgCleaningType.DataBind();
    //Fix for BreadCrums
    // $("#spnHeader").text(sPageTitle);
    $("#spnHeader").text("Master >>Cleaning Process >>" + sPageTitle);
}


// Check "Active" check box in page load for first column
// as well as when a new row added.
function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgCleaningType.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgCleaningType.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    if (ifgCleaningType.Rows().Count == "0")
        ifgCleaningType.Rows(0).SetColumnValuesByIndex(3, true);
    else if (ISNullorEmpty(iCurrentIndex) == false)
        ifgCleaningType.Rows(iCurrentIndex).SetColumnValuesByIndex(3, true);
    resetHasChanges("ifgCleaningType");

    return true;

}

function resetGridChanges() {
    resetHasChanges("ifgCleaningType");
}

function openUpload() {
    if (checkRights()) {
        var sPageTitle = getPageTitle();
        var SchemaId = getQStr("activityid");
        var tablename = getQStr("tablename");
        showModalWindow(sPageTitle + " - Upload", "Upload.aspx?SchemaID=" + SchemaId + "&tablename=" + tablename + "&" + el("WFDATA").value, "750px", "500px", "", "", "");
    }
    else
        return false;
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
    if ($(window.parent).height() < 680) {
        el("tabCleaningType").style.height = $(window.parent).height() - 235 + "px";
        if (el("ifgCleaningType") != null) {
            el("ifgCleaningType").SetStaticHeaderHeight($(window.parent).height() - 288 + "px");
        }
    }
    else if ($(window.parent).height() < 768) {
        el("tabCleaningType").style.height = $(window.parent).height() - 350 + "px";
        if (el("ifgCleaningType") != null) {
            el("ifgCleaningType").SetStaticHeaderHeight($(window.parent).height() - 456 + "px");
        }
    }
    else {
        el("tabCleaningType").style.height = $(window.parent).height() - 254 + "px";
        if (el("ifgCleaningType") != null) {
            el("ifgCleaningType").SetStaticHeaderHeight($(window.parent).height() - 308 + "px");
        }
    }

}
