var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgTracking');
var _RowValidationFails = false;

function initPage(sMode) {
    hideDiv("btnSubmit");
    hideDiv("divMessage");
    resetValidatorByGroup("tabTracking");
    setFocusToField("lkpCustomer");
    setPageMode("edit");
    clearTextValues("lkpCustomer");
    clearTextValues("txtContainerNo");
    clearTextValues("datPickUpDate");
    clearTextValues("datReceivedDate");
    clearTextValues("txtTransmissionNo");
    clearTextValues("lkpLessee");
    clearTextValues("lkpActivity");
    $('.btncorner').corner();
}

//This function is used to Reset the data.
function onResetClick() {
    clearLookupValues("lkpCustomer");
    clearTextValues("txtContainerNo");
    clearTextValues("datPickUpDate");
    clearTextValues("datReceivedDate");
    clearTextValues("txtTransmissionNo");
    clearLookupValues("lkpLessee");
    clearLookupValues("lkpActivity");
    resetValidatorByGroup("tabTracking");
    hideDiv("btnSubmit");
    hideDiv("divDetail");
    hideDiv("divMessage");
    setFocusToField("lkpCustomer");
}

function onSearchClick() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        hideDiv("divDetail");
        hideDiv("btnSubmit");
        return false;
    }
    var oCallback = new Callback();
    oCallback.add("bv_strCstmr_ID", el("lkpCustomer").SelectedValues[0]);
    oCallback.add("bv_strContainer_No", el("txtContainerNo").value);
    oCallback.add("bv_datPickUpDate", el("datPickUpDate").value);
    oCallback.add("bv_datReceivedDate", el("datReceivedDate").value);
    oCallback.add("bv_strTransmission_No", el("txtTransmissionNo").value);
    oCallback.add("bv_strLss_ID", el("lkpLessee").SelectedValues[0]);
    oCallback.add("bv_strActvty_ID", el("lkpActivity").SelectedValues[1]);
    oCallback.add("wfData", el("WFDATA").value);
    oCallback.invoke("Tracking.aspx", "GetTrackingDetails");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("RowCount") != "0") {
            bindTracking();
            showDiv("divDetail");
            showDiv("btnSubmit");
            hideDiv("divMessage");
        }
        else {
            hideDiv("divDetail");
            hideDiv("btnSubmit");
            showDiv("divMessage");
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

//This function is used to bind the Tracking Grid.
function bindTracking() {
    var objGrid = new ClientiFlexGrid("ifgTracking");
    objGrid.DataBind();
}

function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }

    if (ifgTracking.Submit() == false) {
        return false;
    }

    var oCallback = new Callback();
    oCallback.invoke("Tracking.aspx", "GetBulkMailData");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("RowCount") != "0") {
            HasChanges = false;
            bindTracking();
            resetHasChanges('ifgTracking');
            openSendMail();
        }
        else {
            HasChanges = false;
            bindTracking();
            resetHasChanges('ifgTracking');
            clearLookupValues("lkpCustomer");
            clearTextValues("txtContainerNo");
            clearTextValues("datPickUpDate");
            clearTextValues("datReceivedDate");
            clearTextValues("txtTransmissionNo");
            clearLookupValues("lkpLessee");
            clearLookupValues("lkpActivity");
            resetValidatorByGroup("tabTracking");
            showInfoMessage('Resend Successfully');
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}


function openSendMail() {
    var SchemaId = getQStr("activityid");
    var tablename = getQStr("tablename");
    showModalWindow("Send Email", "Operations/SendMail.aspx?SchemaID=" + SchemaId + "&tablename=" + tablename + "&" + el("WFDATA").value, "650px", "430px", "", "", "")
}


function openActivityReport(referenceNo, activityNam, date, eqstatus) {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "GateIn";
    oDocPrint.Type = "document";
    if (activityNam == "Gate In") {
        oDocPrint.Title = "Gate In - Print";
        oDocPrint.DocumentId = "3";
        oDocPrint.ReportPath = "../Documents/Report/GateIn.rdlc";
    }
    else if (activityNam == "Repair Complete") {
        oDocPrint.Title = "Repair Complete - Print";
        oDocPrint.DocumentId = "6";
        oDocPrint.ReportPath = "../Documents/Report/GateIn.rdlc";
    }
    else if (activityNam == "Gate Out") {
        oDocPrint.Title = "Gate Out - Print";
        oDocPrint.DocumentId = "4";
        oDocPrint.ReportPath = "../Documents/Report/GateOut.rdlc";
    }

    else if (eqstatus == "F" || eqstatus == "G") {
        oDocPrint.Title = "Lessee Authorization";
        oDocPrint.DocumentId = "5";
        oDocPrint.ReportPath = "../Documents/Report/Estimate.rdlc";
    }
    else if (eqstatus == "J" || eqstatus == "K") {
        oDocPrint.Title = "Owner Approval";
        oDocPrint.DocumentId = "5";
        oDocPrint.ReportPath = "../Documents/Report/Estimate.rdlc";
    }
    else if (eqstatus == "H" || eqstatus == "L" || eqstatus == "M") {
        oDocPrint.Title = "Survey Completion";
        oDocPrint.DocumentId = "5";
        oDocPrint.ReportPath = "../Documents/Report/Estimate.rdlc";
    }
    else {
        oDocPrint.Title = "Repair Estimate";
        oDocPrint.DocumentId = "5";
        oDocPrint.ReportPath = "../Documents/Report/Estimate.rdlc";
    }
    oDocPrint.add("ReferenceNo", referenceNo);
    oDocPrint.add("EIRDate", date);
    oDocPrint.add("Status", eqstatus);
    oDocPrint.openReportDialog();
}
        
