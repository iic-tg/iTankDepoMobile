var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgExchangeRate');


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
    if (ifgExchangeRate.Submit(true) == false) {
        return false;
    }
    else {
        if (getPageChanges()) {
            updateExchangeRate();
        }
        else {
            showInfoMessage('No Changes to Save')
        }
    }
    return true;
}

//This method used to update/insert entered row to database
function updateExchangeRate() {
    var oCallback = new Callback();
    oCallback.add("TableName", getTableName());
    oCallback.invoke("ExchangeRate.aspx", "UpdateExchangeRate");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindGrid();
        resetHasChanges("ifgExchangeRate");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method is used to show warning message after a row is deleted
function onAfterCB(param) {
    if (typeof (param["Delete"]) != 'undefined') {
        showWarningMessage(param["Delete"]);
    }
    else if (typeof (param["Update"]) != 'undefined' && param["Update"] != '') {
        showWarningMessage(param["Update"]);
    }
    else if (typeof (param["Duplicate"]) != 'undefined' && param["Update"] != '') {
        showWarningMessage(param["Duplicate"]);
    }
    else {
        hideMessage();
    }
}


function bindGrid() {
    var sPageTitle = getPageTitle();
    var ifgExchangeRate = new ClientiFlexGrid("ifgExchangeRate");
    ifgExchangeRate.parameters.add("TableName", getTableName());
    ifgExchangeRate.parameters.add("PageTitle", sPageTitle);
    ifgExchangeRate.parameters.add("WFDATA", el("WFDATA").value);
    ifgExchangeRate.DataBind();
    //Fix for BreadCrums
    //$("#spnHeader").text(sPageTitle);
    $("#spnHeader").text("Master >>Billing Process >>" + sPageTitle);
}


// Check "Active" check box in page load for first column
// as well as when a new row added.
function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgExchangeRate.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgExchangeRate.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    if (ifgExchangeRate.Rows().Count == "0")
        ifgExchangeRate.Rows(0).SetColumnValuesByIndex(4, true);
    else if (ISNullorEmpty(iCurrentIndex) == false)
        ifgExchangeRate.Rows(iCurrentIndex).SetColumnValuesByIndex(4, true);
    resetHasChanges("ifgExchangeRate");

    return true;

}

function resetGridChanges() {
    resetHasChanges("ifgExchangeRate");
}

function fnvalidateCurrencies(oSrc, args) {
    var intCurrentRowIndex = ifgExchangeRate.CurrentRowIndex();
    var cols = ifgExchangeRate.Rows(intCurrentRowIndex).GetClientColumns();
    if (cols[1] == cols[3]) {
        args.IsValid = false;
        oSrc.errormessage = "From Currency and To Currency cannot be same.";
    }
    else {
        args.IsValid = true;
        oSrc.errormessage = "";
    }

}

//This method is used to format the exchange rate to four decimal points.
function formatExchageRate(obj) {
    var Amount = new Number;
    Amount = parseFloat(obj.value);
    obj.value = Amount.toFixed(4);

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
        if ($(window.parent).height() < 670) {
            el("tabExchangeRate").style.height = $(window.parent).height() - 234 + "px";
            if (el("ifgExchangeRate") != null) {
                el("ifgExchangeRate").SetStaticHeaderHeight($(window.parent).height() - 288 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("tabExchangeRate").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgExchangeRate") != null) {
                el("ifgExchangeRate").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }
        }
        else {
            el("tabExchangeRate").style.height = $(window.parent).height() - 238 + "px";
            if (el("ifgExchangeRate") != null) {
                el("ifgExchangeRate").SetStaticHeaderHeight($(window.parent).height() - 292 + "px");
            }
        }
    }

}