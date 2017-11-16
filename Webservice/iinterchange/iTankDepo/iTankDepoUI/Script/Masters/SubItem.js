var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgSubItem');


//Initialize page while loading the page based on page mode
function initPage(sMode) {
    bindGrid();
    setDefaultValues();
    var sPageTitle = getPageTitle();
    el("hdnPageTitle").value = sPageTitle;
}

//This method used to submit the changes to database
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    //To submit the changes to server
    if (ifgSubItem.Submit(true) == false) {
        return false;
    }
    else {
        if (getPageChanges()) {
            updateSubItem();
        }
        else {
            showInfoMessage('No Changes to Save')
        }
    }
    return true;
}

//This method used to update/insert entered row to database
function updateSubItem() {
    var oCallback = new Callback();
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("SubItem.aspx", "UpdateSubItem");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindGrid();
        resetHasChanges("ifgSubItem");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method is to validate the duplicate code entered
function validateCode(oSrc, args) {
    var iRowIndex = ifgSubItem.rowIndex;
    var oCols = ifgSubItem.Rows(iRowIndex).GetClientColumns();
    var sCode = oCols[0];

    var sCols = ifgSubItem.Rows(iRowIndex).Columns();
    var sCode = oCols[0];
    var rowState = ifgSubItem.ClientRowState();
    if (rowState != 'Added') {
        if (sCols[1] == sCode) {
            return false;
        }
    }

    var oCallback = new Callback();
    oCallback.add("Code", sCode);
    oCallback.add("GridIndex", ifgSubItem.VirtualCurrentRowIndex());
    //Newly added code is available in existing data only in database.
    oCallback.add("RowState", rowState);
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("SubItem.aspx", "ValidateCode");
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
    var ifgSubItem = new ClientiFlexGrid("ifgSubItem");
    ifgSubItem.parameters.add("WFDATA", el("WFDATA").value);
    ifgSubItem.DataBind();
    $("#spnHeader").text(sPageTitle);
}


// Check "Active" check box in page load for first column
// as well as when a new row added.
function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgSubItem.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgSubItem.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    if (ifgSubItem.Rows().Count == "0")
        ifgSubItem.Rows(0).SetColumnValuesByIndex(4, true);
    else if (ISNullorEmpty(iCurrentIndex) == false)
        ifgSubItem.Rows(iCurrentIndex).SetColumnValuesByIndex(4, true);
    resetHasChanges("ifgSubItem");

    return true;

}

function resetGridChanges() {
    resetHasChanges("ifgSubItem");
}

function openUpload() {
    if (checkRights()) {
        var sPageTitle = getPageTitle();
        var SchemaId = getQStr("activityid");
        var tablename = getQStr("tablename");
        showModalWindow(sPageTitle + " - Upload", "Upload.aspx?SchemaID=" + SchemaId + "&tablename=" + tablename + "&" + el("WFDATA").value, "650px", "430px", "", "", "");
    }
    else
        return false;
}
