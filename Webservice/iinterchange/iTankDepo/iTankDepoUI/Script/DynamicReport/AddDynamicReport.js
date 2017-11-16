var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgParameters');
var Process = '';
var smodenam='';
function initPage(sMode) {

    smodenam = sMode;
    el('divifgParameters').style.display = 'none';
    el("hdnFetchParameters").value = "false";
    $('.btncorner').corner();
    if (sMode == MODE_NEW) {
        clearTextValues("txtProcess");
        clearTextValues("lkpProcess");
        clearTextValues("txtReportName");
        clearTextValues("txtReportTitle");
        setReadOnly("txtReportName", false);
        setPageID("0");
        el("txtReportName").focus();
        }
    else {
        clearTextValues("txtProcess");
        setReadOnly("txtReportName", true);
        el("txtReportTitle").focus();
        Process = el('lkpProcess').SelectedValues[0];
        }
  
    hideDiv("lnkCancel");
    el('hdnAddProcess').value = "false";
    setPageMode(sMode);
    reSizePane();
}

//This method used to submit the changes to database
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    //To submit the changes to server
    if (typeof (ifgParameters) == "object") {
        if (ifgParameters.Submit() == false) {
            return false;
        }
    }
        if (getPageChanges()) {
            if (getPageMode() == MODE_NEW) {
                createReport();
            }
            else {
                updateReport();
            }
        }
        else {
            showInfoMessage('No Changes to Save');
        }
    return true;
}


//This method used to update/insert entered row to database
function createReport() {
    var oCallback = new Callback();
    oCallback.add("ReportName", el('txtReportName').value);
//    if (el('hdnAddProcess').value=="false")
//        oCallback.add("ProcessName", el('lkpProcess').SelectedValues[0]);
//    else
    //        oCallback.add("ProcessName", el('txtProcess').value);
    //since always id will be passed
    el('hdnAddProcess').value = "false";
  
        oCallback.add("ProcessName", el('lkpProcess').SelectedValues[0]);
        
 

    oCallback.add("AddProcessFlag", el('hdnAddProcess').value);
    oCallback.add("ReportTitle", el('txtReportTitle').value);
    oCallback.add("Active", el('chkActiveBit').checked);
    oCallback.invoke("AddDynamicReport.aspx", "createReport");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        setPageID(oCallback.getReturnValue("ID"));
        setPageMode(MODE_EDIT);
        HasChanges = false;
        setReadOnly("txtReportName", true);
        el("txtReportTitle").focus();
        if (el('hdnAddProcess').value == "true")        
            SetLookupValue('lkpProcess', el('txtProcess').value, new Array(oCallback.getReturnValue("ProcessID"), el('txtProcess').value))
        el('hdnAddProcess').value == "false"
        Process = el('lkpProcess').SelectedValues[0];
        showDiv("lnkAddProcess");
        hideDiv("lnkCancel");
        showDiv("divlkpProcess");
        hideDiv("divtxtProcess");
        resetHasChanges("ifgParameters");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method used to update/insert entered row to database
function updateReport() {
    var orderFlag = "true";
    var oCallback = new Callback();
    oCallback.add("ActivityId", getPageID());
    el('hdnAddProcess').value = "false";

    oCallback.add("ProcessName", el('lkpProcess').SelectedValues[0]);


    oCallback.add("ReportName", el('txtReportName').value);
    oCallback.add("ReportTitle", el('txtReportTitle').value);
    oCallback.add("AddProcessFlag", el('hdnAddProcess').value);
    oCallback.add("Active", el('chkActiveBit').checked);
    oCallback.add("FetchParameter", el('hdnFetchParameters').value);    
    if (Process == el('lkpProcess').SelectedValues[0] && el('hdnAddProcess').value == "false") {
        orderFlag = "false";
    }
    oCallback.add("OrderFlag", orderFlag);
    oCallback.invoke("AddDynamicReport.aspx", "updateReport");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));        
        HasChanges = false;
        resetHasChanges("ifgParameters");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method is to bind the grid details
function bindParameterGrid() {
    var objAddReport = new ClientiFlexGrid("ifgParameters");
    objAddReport.parameters.add("ReportID", getPageID());
    objAddReport.parameters.add("ReportName", el('txtReportName').value);
    objAddReport.DataBind();
}

//This method is to validate the duplicate code entered
function validateAddReportName(oSrc, args) {
 
    var oCallback = new Callback();
    oCallback.add("ReportName", args.Value);

    //changes for add process flag
    if (smodenam == MODE_NEW) {
        oCallback.add("AddProcessFlag", "true");
    }
    else { oCallback.add("AddProcessFlag", "false"); }


    oCallback.add("AddProcessFlag", el('hdnAddProcess').value)
    oCallback.add("ProcessId", el('lkpProcess').SelectedValues[0]);

    oCallback.invoke("AddDynamicReport.aspx", "validateAddReportName");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("pkValid") == "true") {
            args.IsValid = true;
            oSrc.errormessage = "";
        }
        else {            
            args.IsValid = false;
            oSrc.errormessage = oCallback.getReturnValue("Message");
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}


//This method is to validate the duplicate Process entered
function validateProcess(oSrc, args) {
    if (el('hdnAddProcess').value == "false") {
        args.IsValid = true;
        return;
    }
    var objCallback = new Callback();

    objCallback.add("PRCSS_NAM", args.Value);

    objCallback.invoke("AddDynamicReport.aspx", "validateProcess")

    if (objCallback.getCallbackStatus()) {
        var Validateresult;
        Validateresult = objCallback.getReturnValue("pkvalid");

        if (Validateresult == "true") {
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = "Process Name Already Exists.";
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(objCallback.getCallbackError());
    }
    objCallback = null;
}

//This method is to validate the duplicate Process entered
function valProcessbyRptName(oSrc, args) {

    if (el('hdnAddProcess').value == "true") {
        args.IsValid = true;
        return;
    }
    if (getPageMode() == MODE_EDIT) {
        if (Process == el('lkpProcess').SelectedValues[0]) {
            args.IsValid = true;
            return;
        }        
    }
    var objCallback = new Callback();

    objCallback.add("PRCSS_ID", el('lkpProcess').SelectedValues[0]);
    objCallback.add("ACTVTY_NAM", el('txtReportName').value);
    
    objCallback.invoke("AddDynamicReport.aspx", "valProcessbyRptName")

    if (objCallback.getCallbackStatus()) {
        var Validateresult;
        Validateresult = objCallback.getReturnValue("pkvalid");

        if (Validateresult == "true") {
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = "Report Name Already Exists for " + args.Value +" process.";
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(objCallback.getCallbackError());
    }
    objCallback = null;
}

//This method is used to show warning message after a row is deleted
function onAfterCB(param) {
    if (typeof (param["Delete"]) != 'undefined') {
        showWarningMessage(param["Delete"]);
    }
    else {
        hideMessage();
    }
}


function onAddProcessClick() {
    showDiv("lnkCancel");
    hideDiv("lnkAddProcess");
    showDiv("divtxtProcess");
    hideDiv("divlkpProcess");
    clearTextValues("txtProcess");
    el('hdnAddProcess').value = "true";
}

function onCancelClick() {
    showDiv("lnkAddProcess");
    hideDiv("lnkCancel");
    showDiv("divlkpProcess");
    hideDiv("divtxtProcess");
    el('hdnAddProcess').value = "false";
}


function applyParameterFilter() {
    return " RPRT_ID=" + getPageID();
}

function onParametersClick() {

    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }

    if (el('hdnAddProcess').value=="false" && el('lkpProcess').value=="" )
    {
    showErrorMessage('Please Select Process.');
    return false;
    }
    el('divifgParameters').style.display = 'block';
    el("hdnFetchParameters").value = "true";
    if (getPageMode() == MODE_NEW) {
        showConfirmMessage("Do you really want to get the parameters? This will submit the report details.",
                        "wfs().yesParameterClick();", "", "");
    }
    else {
        bindParameterGrid();
    }
}

function yesParameterClick() {    
    if (getPageMode() == MODE_NEW) {
        createReport();
        bindParameterGrid();
    }
}

function onParameterClick(rIndex, obj) {
    var chkdropdown = obj.checked;
    if (typeof (obj.checked) != "undefined") {
        if (obj.checked == true) {
            ifgParameters.Rows(rIndex).SetReadOnlyColumn(4, false);          
        }
        else {
            ifgParameters.Rows(rIndex).SetColumnValuesByIndex(4, "");
            ifgParameters.Rows(rIndex).SetReadOnlyColumn(4, true);
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
        if ($(window.parent).height() < 768) {
            el("tabAddReport").style.height = $(window.parent).height() - 373 + "px";            

        }
        else {
            el("tabAddReport").style.height = $(window.parent).height() - 478 + "px";          

        }
    }


}
