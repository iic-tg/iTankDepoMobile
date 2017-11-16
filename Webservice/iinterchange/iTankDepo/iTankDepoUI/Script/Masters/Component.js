var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgComponent');


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
}

//This method used to submit the changes to database
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    //To submit the changes to server
    if (ifgComponent.Submit(true) == false) {
        return false;
    }
    else {
        if (getPageChanges()) {
            updateComponent();
        }
        else {
            showInfoMessage('No Changes to Save')
        }
    }
    return true;
}

//This method used to update/insert entered row to database
function updateComponent() {
    var oCallback = new Callback();
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("Component.aspx", "UpdateComponent");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindGrid();
        resetHasChanges("ifgComponent");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method is to validate the duplicate code entered
function validateCode(oSrc, args) {
    var iRowIndex = ifgComponent.rowIndex;
    var oCols = ifgComponent.Rows(iRowIndex).GetClientColumns();
    var sCode = oCols[0];

    var sCols = ifgComponent.Rows(iRowIndex).Columns();
    var sCode = oCols[0];
    var rowState = ifgComponent.ClientRowState();
    if (rowState != 'Added') {
        if (sCols[1] == sCode) {
            return false;
        }
    }

    var oCallback = new Callback();
    oCallback.add("Code", sCode);
    oCallback.add("GridIndex", ifgComponent.VirtualCurrentRowIndex());
    //Newly added code is available in existing data only in database.
    oCallback.add("RowState", rowState);
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("Component.aspx", "ValidateCode");
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
    var ifgComponent = new ClientiFlexGrid("ifgComponent");    
    ifgComponent.parameters.add("WFDATA", el("WFDATA").value);
    ifgComponent.DataBind();
    $("#spnHeader").text(sPageTitle);
}


// Check "Active" check box in page load for first column
// as well as when a new row added.
function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgComponent.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgComponent.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    if (ifgComponent.Rows().Count == "0")
        ifgComponent.Rows(0).SetColumnValuesByIndex(4, true);
    else if (ISNullorEmpty(iCurrentIndex) == false)
        ifgComponent.Rows(iCurrentIndex).SetColumnValuesByIndex(4, true);
    resetHasChanges("ifgComponent");

    return true;

}

function resetGridChanges() {
    resetHasChanges("ifgComponent");
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
