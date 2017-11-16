var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgHeating');


function initPage(sMode) {
    
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    bindGrid();
    reSizePane();
    var sPageTitle = getPageTitle();
    el("hdnPageTitle").value = sPageTitle;
}

function bindGrid() {
    var objGrid = new ClientiFlexGrid("ifgHeating");
    objGrid.parameters.add("WFDATA", el("WFDATA").value);
    objGrid.DataBind();
}

//This function used to Hide the Detail Grid when it has no Rows to display.
function ifgHeatingOnAfterCB(param) {
    if (typeof (param["Error"]) != 'undefined') {
        showErrorMessage(param["Error"]);
        return false;
    }
    if (typeof (param["Success"]) != 'undefined') {
        showInfoMessage(param["Success"]);
        return false;
    }
}

function ifgHeatingOnBeforeCB(mode, param) {
    if (mode == "Delete") {
        var _cols = ifgHeating.Rows(ifgHeating.rowIndex).Columns();
        param.add("PreAdviceID", _cols[0]);
        param.add("EquipmentNo", _cols[1]);
        param.add("GI_TransactionNo", _cols[17]);
    }
}

function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }

    //To submit the changes to server    
    if (getPageChanges()) {
        if (ifgHeating.Submit() == false) {
            return false;
        }
        return updateHeating();
    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

// This method used to update/insert entered row to database
function updateHeating() {
    var oCallback = new Callback();
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.add("ActivityName", getQueryStringValue(document.location.href, "activityname"));
    oCallback.add("ActivityId", getQueryStringValue(document.location.href, "activityid"));
    oCallback.invoke("Heating.aspx", "UpdateHeating");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ActivitySubmit") == "" || oCallback.getReturnValue("ActivitySubmit") == null) {
            showInfoMessage(oCallback.getReturnValue("Message"));
            HasChanges = false;
        }
        else {
            activitySubmitMessage(oCallback.getReturnValue("ActivitySubmit"));
        }
        bindGrid();
        resetHasChanges("ifgHeating");

    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function calculateTotalRate(oSrc, args) {
    var rIndex = ifgHeating.CurrentRowIndex();
    var cColumns = ifgHeating.Rows(rIndex).GetClientColumns();
    var sCols = ifgHeating.Rows(rIndex).Columns();
    if (cColumns[13] == "") {
        oSrc.errormessage = "Total Heating Period Required";
        return;
    }
    else {
        var oCallback = new Callback();
        oCallback.add("MinRate", cColumns[15]);
        oCallback.add("TotalPeriod", cColumns[13]);
        oCallback.add("MinHeatingPeriod", sCols[25]);
        oCallback.add("HourlyRate", cColumns[16]);
        oCallback.invoke("Heating.aspx", "CalculateTotalRate");
        if (oCallback.getCallbackStatus()) {
            var TotalRate = oCallback.getReturnValue("TotalRate");
            TotalRate = parseFloat(TotalRate).toFixed(2);
            ifgHeating.Rows(rIndex).SetColumnValuesByIndex(13, TotalRate);
        }
        oCallback = null;
    }
}

function ValidateTotalHeatingPeriod(oSrc, args) {
    var cols = ifgHeating.Rows(ifgHeating.CurrentRowIndex()).GetClientColumns();
    var rIndex = ifgHeating.CurrentRowIndex();
    if (cols[8] == "" || cols[9] == "" || cols[10] == "" || cols[11] == "" || cols[8] == null || cols[9] == null || cols[10] == null || cols[11] == null) {
        ifgHeating.Rows(rIndex).SetColumnValuesByIndex(10, "");
    }
    else if ((cols[8] != "" && cols[9] != "" && cols[10] != "" && cols[11] != "") || (cols[8] != null && cols[9] != null && cols[10] != null && cols[11] != null)) {
        var oCallback = new Callback();
        oCallback.add("HeatingStartDate", cols[8]);
        oCallback.add("HeatingStartTime", cols[9]);
        oCallback.add("HeatingEndDate", cols[10]);
        oCallback.add("HeatingEndTime", cols[11]);
        oCallback.invoke("Heating.aspx", "CalculateHeatingPeriod");
        if (oCallback.getCallbackStatus()) {
            var TotalHeatingPeriod = oCallback.getReturnValue("TotalHeatingPeriod");
            TotalHeatingPeriod = parseFloat(TotalHeatingPeriod).toFixed(2);
            ifgHeating.Rows(rIndex).SetColumnValuesByIndex(10, TotalHeatingPeriod);
        }
        oCallback = null;
    }
    var rIndex = ifgHeating.CurrentRowIndex();
    var cColumns = ifgHeating.Rows(rIndex).GetClientColumns();
    if (cColumns[13] != "") {
        var rIndex = ifgHeating.CurrentRowIndex();
        var cColumns = ifgHeating.Rows(rIndex).GetClientColumns();
        var sCols = ifgHeating.Rows(rIndex).Columns();
        if (cColumns[13] == "") {
            oSrc.errormessage = "Total Heating Period Required";
            args.IsValid = false;
            return;
        }
        else {
            var oCallback = new Callback();
            oCallback.add("MinRate", cColumns[14]);
            oCallback.add("TotalPeriod", cColumns[13]);
            oCallback.add("MinHeatingPeriod", sCols[25]);
            oCallback.add("HourlyRate", cColumns[15]);
            oCallback.invoke("Heating.aspx", "CalculateTotalRate");
            if (oCallback.getCallbackStatus()) {
                var TotalRate = oCallback.getReturnValue("TotalRate");
                TotalRate = parseFloat(TotalRate).toFixed(2);
                ifgHeating.Rows(rIndex).SetColumnValuesByIndex(13, TotalRate);
            }
            oCallback = null;
        }
    }
}

function ClearEndTime(oSrc, args) {
    var cols = ifgHeating.Rows(ifgHeating.CurrentRowIndex()).GetClientColumns();
    var rIndex = ifgHeating.CurrentRowIndex();
    ifgHeating.Rows(rIndex).SetColumnValuesByIndex(8, "");
    ifgHeating.Rows(rIndex).SetColumnValuesByIndex(14, "");
}

function ClearTotalHeatingPeriod(oSrc, args) {
    var cols = ifgHeating.Rows(ifgHeating.CurrentRowIndex()).GetClientColumns();
    var rIndex = ifgHeating.CurrentRowIndex();
    ifgHeating.Rows(rIndex).SetColumnValuesByIndex(10, "");
}

function ValidateHeatingStartDate(oSrc, args) {
    var cols = ifgHeating.Rows(ifgHeating.CurrentRowIndex()).GetClientColumns();
    var rIndex = ifgHeating.CurrentRowIndex();
    var oCallback = new Callback();
    oCallback.add("EquipmentNo", cols[0]);
    oCallback.add("EventDate", cols[8]);
    if (cols[10] == null)
        oCallback.add("EndDate", "");
    else
        oCallback.add("EndDate", cols[10]);

    oCallback.invoke("Heating.aspx", "ValidatePreviousActivityDate");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {
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

function ValidateHeatingEndDate(oSrc, args) {
    var cols = ifgHeating.Rows(ifgHeating.CurrentRowIndex()).GetClientColumns();
    var rIndex = ifgHeating.CurrentRowIndex();

    var oCallback = new Callback();
    oCallback.add("EquipmentNo", cols[0]);
    oCallback.add("EventDate", cols[10]);
    oCallback.add("HeatingStartDate", cols[8]);
    oCallback.invoke("Heating.aspx", "ValidateHeatingEndDate");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {
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

function ValidateHeatingEndTime(oSrc, args) {
    var cols = ifgHeating.Rows(ifgHeating.CurrentRowIndex()).GetClientColumns();
    var rIndex = ifgHeating.CurrentRowIndex();
    if (cols[8] != "" && cols[9] != "" && cols[10] != "" && cols[11] != "") {
        if (cols[8] == cols[10]) {
            var oCallback = new Callback();
            oCallback.add("StartTime", cols[9]);
            oCallback.add("EndTime", cols[11]);
            oCallback.invoke("Heating.aspx", "ValidateHeatingEndTime");
            if (oCallback.getCallbackStatus()) {
                if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {
                    args.IsValid = false;
                    oSrc.errormessage = oCallback.getReturnValue("Error");
                }
                else {
                    args.IsValid = true;
                }
            }

        }
    }
}

function ValidateHeatingStartTime(oSrc, args) {
    var cols = ifgHeating.Rows(ifgHeating.CurrentRowIndex()).GetClientColumns();
    var rIndex = ifgHeating.CurrentRowIndex();
    if (cols[8] != "" && cols[9] != "" && cols[10] != "" && cols[11] != "") {
        if (cols[8] == cols[10]) {
            var oCallback = new Callback();
            oCallback.add("StartTime", cols[9]);
            oCallback.add("EndTime", cols[11]);
            oCallback.invoke("Heating.aspx", "ValidateHeatingStartTime");
            if (oCallback.getCallbackStatus()) {
                if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {
                    args.IsValid = false;
                    oSrc.errormessage = oCallback.getReturnValue("Error");
                }
                else {
                    args.IsValid = true;
                }
            }

        }
    }
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
            el("divHeating").style.height = $(window.parent).height() - 238 + "px";
            if (el("ifgHeating") != null) {
                el("ifgHeating").SetStaticHeaderHeight($(window.parent).height() - 290 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("divHeating").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgHeating") != null) {
                el("ifgHeating").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }
        }
        else {
            el("divHeating").style.height = $(window.parent).height() - 243 + "px";
            if (el("ifgHeating") != null) {
                el("ifgHeating").SetStaticHeaderHeight($(window.parent).height() - 295 + "px");
            }
        }
    }

}


