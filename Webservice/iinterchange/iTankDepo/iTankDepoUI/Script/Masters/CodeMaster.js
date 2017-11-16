var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgCodeMaster');


//Initialize page while loading the page based on page mode
function initPage(sMode) {
    var sPageTitle = getPageTitle();
    bindGrid();
    setDefaultValues();
    el("hdnPageTitle").value = sPageTitle;
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    el('iconFav').style.display = 'none';
}

//This method used to submit the changes to database
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    //To submit the changes to server
    if (ifgCodeMaster.Submit(true) == false) {
        return false;
    }
    else {
        if (getPageChanges()) {
            updateCodeMaster();
        }
        else {
            showInfoMessage('No Changes to Save')
        }
    }
    return true;
}

//This method used to update/insert entered row to database
function updateCodeMaster() {
    var oCallback = new Callback();
    oCallback.add("TableName", getTableName());
    oCallback.add("WFDATA", el("WFDATA").value);

    oCallback.invoke("CodeMaster.aspx", "UpdateCodeMaster");

    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindGrid();
        resetHasChanges("ifgCodeMaster");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method is to validate the duplicate code entered
function validateCode(oSrc, args) {
    var iRowIndex = ifgCodeMaster.CurrentRowIndex();
    var oCols = ifgCodeMaster.Rows(iRowIndex).GetClientColumns();
    var sCols = ifgCodeMaster.Rows(iRowIndex).Columns();
    var sCode = oCols[0];

    var rowState = ifgCodeMaster.ClientRowState();
    if (rowState != 'Added') {
        if (sCols[1] == sCode) {
            return false;
        }
    }

    var oCallback = new Callback();
    oCallback.add("Code", sCode);
    oCallback.add("TableName", getTableName());
    oCallback.add("ActivityId", getQStr("activityid"));
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.add("GridIndex", ifgCodeMaster.VirtualCurrentRowIndex());

    //Newly added code is available in existing data only in database.
    oCallback.invoke("CodeMaster.aspx", "ValidateCode");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("bNotExists") == "true") {
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
    var slistpanetitle=""
    var sPageTitle = getPageTitle();
    var ifgCodeMaster = new ClientiFlexGrid("ifgCodeMaster");
    ifgCodeMaster.parameters.add("TableName", getTableName());
    ifgCodeMaster.parameters.add("PageTitle", sPageTitle);
    ifgCodeMaster.parameters.add("WFDATA", el("WFDATA").value);
    ifgCodeMaster.DataBind();
    //Fix for BreadCrums
   slistpanetitle= getlistpanetitle(sPageTitle);
   $("#spnHeader").text("Master >>" + slistpanetitle);
   // $("#spnHeader").text(getQueryStringValue(document.location.href, "listpanetitle"));
}


// Check "Active" check box in page load for first column
// as well as when a new row added.
function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgCodeMaster.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgCodeMaster.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    if (ifgCodeMaster.Rows().Count == "0")
        ifgCodeMaster.Rows(0).SetColumnValuesByIndex(2, true);
    else if (ISNullorEmpty(iCurrentIndex) == false)
        ifgCodeMaster.Rows(iCurrentIndex).SetColumnValuesByIndex(2, true);
    resetHasChanges("ifgCodeMaster");

    return true;

}

function resetGridChanges() {
    resetHasChanges("ifgCodeMaster");
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
//UIG fix for Bread crums not coming properly
function getlistpanetitle(sPageTitle) {
    var slistpanetitle;
    sPageTitle = trimAll(sPageTitle);
    if (sPageTitle == "Equipment Code" || sPageTitle == "Equipment Type" || sPageTitle == "Equipment Status" || sPageTitle == "Grade" || sPageTitle == "Port" || sPageTitle == "Party" || sPageTitle == "Activity Status" || sPageTitle == "Country")
        slistpanetitle = "Equipment >> " + sPageTitle;
    else if (sPageTitle == "Repair" || sPageTitle == "Damage" || sPageTitle == "Material" ||  sPageTitle == "Tariff" || sPageTitle == "Responsibility" || sPageTitle == "Measure" || sPageTitle == "Unit" )
        slistpanetitle = "Repair Process >> " + sPageTitle;
    else if (sPageTitle == "Location")
        slistpanetitle = "Transport >> " + sPageTitle;
    else if (sPageTitle == "Currency")
        slistpanetitle = "Billing Process >> " + sPageTitle;
    return slistpanetitle;
}