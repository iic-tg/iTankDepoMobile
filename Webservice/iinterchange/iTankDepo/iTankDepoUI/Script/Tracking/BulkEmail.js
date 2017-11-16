var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgBulkEmail');
var _RowValidationFails = false;


if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
function initPage(sMode) {

    //GWS
    if (getConfigSetting('060') == "True") {
        hideDiv("divCleaning");
        hideDiv("divCleaingCheck");
    }
    else {
        showDiv("divCleaning");
        showDiv("divCleaingCheck");
    }
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    hideDiv("divBulkMail");
    hideDiv("btnSubmit");
    resetValidatorByGroup("divBulkMail");
    setFocusToField("lkpCustomer");
    setPageMode("edit");
    set_ClearValues();
    $('.btncorner').corner();
    checkAllActivity(true);
}

function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges() || HasChanges) {
        if (ifgBulkEmail.Submit() == false || _RowValidationFails) {
            return false;
        }
        return GetSelectedValues();
    }
    else {
        showInfoMessage('No Changes to Save');
        return false;
    }
    return true;
}

function ifgBulkMailOnAfterCB(param) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        el("divBulkMail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divBulkMail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}
// Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :57
function onSearchClick() {
    //if (el('hypSearch').innerText == " Search") {
    if (getText(el('hypSearch')) == "Search") {
        GetLookupChanges();
        Page_ClientValidate();
        if (!Page_IsValid) {
            hideDiv("divBulkMail");
            hideDiv("btnSubmit");
            return false;
        }
        set_ReadOnly_values(true);
        disableActivity(true);
        return getSearchDetails();
    }
    else {

        set_ClearValues();
        //el('hypSearch').innerText = " Search"
        setText(el('hypSearch'), "Search");
        set_ReadOnly_values(false);
        //set_tabIndex_values();
        disableActivity(false);
        setFocusToField("lkpCustomer");
        bindTracking("Reset");
        hideDiv("divBulkMail");
        hideDiv("btnSubmit");
        hideDiv("divRecordNotFound");
        resetValidators();
    }

}
function set_tabIndex_values() {
    el('lkpCustomer').tabIndex = 1;
    el('txtEquipmentNo').tabIndex = 2;
    el('datInDateFrom').tabIndex = 3;
    el('datInDateTo').tabIndex = 4;
    el('datEventDateFrom').tabIndex = 5;
    el('datEventDateTo').tabIndex = 6;
    el('lkpEmail').tabIndex = 7;

}

function set_ClearValues() {
    clearTextValues("lkpCustomer");
    clearTextValues("txtEquipmentNo");
    clearTextValues("datInDateFrom");
    clearTextValues("datInDateTo");
    checkAllActivity(true);
    clearTextValues("datEventDateFrom");
    clearTextValues("datEventDateTo");
    clearTextValues("lkpEmail");
    clearLookupValues("lkpCustomer");
    clearLookupValues("lkpEmail");
}

function set_ReadOnly_values(blnStatus) {
    setReadOnly("lkpCustomer", blnStatus);
    setReadOnly("txtEquipmentNo", blnStatus);
    setReadOnly("datInDateFrom", blnStatus);
    setReadOnly("datInDateTo", blnStatus);
    setReadOnly("datEventDateFrom", blnStatus);
    setReadOnly("datEventDateTo", blnStatus);
    setReadOnly("lkpEmail", blnStatus);
}

function bindTracking(pMode) {
    var objGrid = new ClientiFlexGrid("ifgBulkEmail");
    objGrid.parameters.add("btnType", pMode);
    objGrid.DataBind();
    reSizePane();
}

function openActivityReport(referenceNo, activityNam, date, eqstatus, report, ID, depotID, str_020EIRNo) {
    var oDocPrint = new DocumentPrint();
    oDocPrint.Type = "document";
    oDocPrint.add("wfData", el("WFDATA").value);
    if (activityNam == "Repair Estimate" || activityNam == "Repair Approval" || activityNam == "Repair Completion") {
                oDocPrint.KeyName = "Repair Estimate";
        oDocPrint.Type = "document";
        oDocPrint.Title = "Repair Estimate";
        if (getConfigSetting('071') == "True") {
            oDocPrint.DocumentId = "47";
            oDocPrint.ReportPath = "../Documents/Report/RepairEstimateAcacia.rdlc";
        }
        else if (getConfigSetting('060') == "True") {
            oDocPrint.DocumentId = "45";
            oDocPrint.ReportPath = "../Documents/Report/RepairEstimateGWC.rdlc";
        }
        else {
            oDocPrint.DocumentId = "20";
            oDocPrint.ReportPath = "../Documents/Report/RepairEstimate.rdlc";
        }
        oDocPrint.add("EstimateID", ID);
    }
    if (activityNam == "Cleaning") {
        oDocPrint.KeyName = "Cleaning";
        oDocPrint.Title = "Cleaning";
        oDocPrint.DocumentId = "18";
        oDocPrint.ReportPath = "../Documents/Report/Cleaning.rdlc";
        oDocPrint.add("CleaningID", ID);
        oDocPrint.add("DepotID", depotID);
        oDocPrint.add("CretNo", str_020EIRNo);
        oDocPrint.add("GI_TRNSCTN_NO", referenceNo);
    }
    oDocPrint.openReportDialog();
}

function openWorkOrderReport(referenceNo, activityNam, date, eqstatus, report, estimateID) {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "Repair Work Order";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Repair Work Order";
    if (getConfigSetting('060') == "True") {
        oDocPrint.DocumentId = "43";
        oDocPrint.ReportPath = "../Documents/Report/RepairWorkOrderGWC.rdlc";
    } else {
        oDocPrint.DocumentId = "19";
        oDocPrint.ReportPath = "../Documents/Report/RepairWorkOrder.rdlc";
    }
    oDocPrint.add("EstimateID", estimateID);
    oDocPrint.add("wfData", el("WFDATA").value);
    oDocPrint.openReportDialog();
}

function openBulkEmailDetail(GI_Trans_No, ActivityName, EqpNo, ActivityNo) {
    if (ifgBulkEmail.Submit(true) == false) {
        return false;
    }
    showModalWindow("Bulk Email Detail/Equipment No-" + EqpNo, "Tracking/BulkEmailDetail.aspx?GI_TRANS_NO=" + GI_Trans_No + "&EquipmentNo=" + EqpNo + "&ActivityName=" + ActivityName + "&ActivityNo=" + ActivityNo, "900px", "250px", "100px", "", "");
    return true;
    psc().hideLoader();
}

//This function is used to Update the changes to the server. 
function GetSelectedValues() {
    var oCallback = new Callback();
    oCallback.add("WFData", getWFDATA());
    oCallback.invoke("BulkEmail.aspx", "GetSelectedValues");
    if (oCallback.getCallbackStatus()) {
        var CustomerID = oCallback.getReturnValue("CustomerID")
        showModalWindow("Send Email", "Tracking/SendEmail.aspx?CustomerID=" + CustomerID + "&MailMode=Send", "600px", "400px", "100px", "", "");
        return true;
        psc().hideLoader();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}
// Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :57
function validateInDateFrom(oSrc, args) {
    if ((el("datInDateFrom").value == "") && (el("datInDateTo").value != "") || (el("datInDateFrom").value == null) && (el("datInDateTo").value != null)) {
        args.IsValid = false;
        oSrc.errormessage = "In Date From Required";
        return false;
    } else {
        args.IsValid = true;
    }
    if ((el("datInDateFrom").value != "") && (el("datInDateTo").value != "") && (el("datInDateFrom").value != null) && (el("datInDateTo").value != null)) {
        var oCallback = new Callback();
        oCallback.add("EventDateFrom", el("datInDateFrom").value);
        oCallback.add("EventDateTo", el("datInDateTo").value);
        oCallback.invoke("BulkEmail.aspx", "ValidateInDate");
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
}

function validateInDateTo(oSrc, args) {
    if ((el("datInDateTo").value == "") && (el("datInDateFrom").value != "") || (el("datInDateTo").value == null) && (el("datInDateFrom").value != null)) {
        args.IsValid = false;
        oSrc.errormessage = "In Date To Required";
        return false;
    }
    else if ((el("datInDateTo").value != "") && (el("datInDateFrom").value == "") && (el("datInDateTo").value != null) && (el("datInDateFrom").value == null)) {
        //        args.IsValid = false;
        oSrc.errormessage = "In Date From Required";
        setFocusToField("datInDateFrom");
        return false;
    }
    else {
        args.IsValid = true;
    }
    if ((el("datInDateFrom").value != "") && (el("datInDateTo").value != "") && (el("datInDateFrom").value != null) && (el("datInDateTo").value != null)) {
        var oCallback = new Callback();
        oCallback.add("EventDateFrom", el("datInDateFrom").value);
        oCallback.add("EventDateTo", el("datInDateTo").value);
        oCallback.invoke("BulkEmail.aspx", "ValidateInDateTo");
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
}

function validateActivityDateFrom(oSrc, args) {
    if ((el("datEventDateFrom").value == "") && (el("datEventDateTo").value != "") || (el("datEventDateFrom").value == null) && (el("datEventDateTo").value != null)) {
        args.IsValid = false;
        oSrc.errormessage = "Activity Date From Required";
        return false;
    }
    else {

        args.IsValid = true;
    }
    if ((el("datEventDateFrom").value != "") && (el("datEventDateTo").value != "") && (el("datEventDateFrom").value != null) && (el("datEventDateTo").value != null)) {
        var oCallback = new Callback();
        oCallback.add("EventDateFrom", el("datEventDateFrom").value);
        oCallback.add("EventDateTo", el("datEventDateTo").value);
        oCallback.invoke("BulkEmail.aspx", "ValidateDate");
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
}

function validateActivityDateTo(oSrc, args) {

    if ((el("datEventDateTo").value == "") && (el("datEventDateFrom").value != "") || (el("datEventDateTo").value == null) && (el("datEventDateFrom").value != null)) {
        args.IsValid = false;
        oSrc.errormessage = "Activity Date To Required";
        return false;
    }
    else if ((el("datEventDateTo").value != "") && (el("datEventDateFrom").value == "") || (el("datEventDateTo").value != null) && (el("datEventDateFrom").value == null)) {
        //        args.IsValid = false;
        oSrc.errormessage = "Activity Date From Required";
        setFocusToField("datEventDateFrom");
        //        return false;
    }
    else {
        args.IsValid = true;
    }
    if ((el("datEventDateFrom").value != "") && (el("datEventDateTo").value != "") && (el("datEventDateFrom").value != null) && (el("datEventDateTo").value != null)) {
        var oCallback = new Callback();
        oCallback.add("EventDateFrom", el("datEventDateFrom").value);
        oCallback.add("EventDateTo", el("datEventDateTo").value);
        oCallback.invoke("BulkEmail.aspx", "ValidateDateTo");
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
}

function setFocus() {
    setFocusToField("txtTo");
    setReadOnly("txtBCC", true);
}

function SubmitBulkDetails() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    submitEmailInfo();
    return true;
}

function submitEmailInfo() {
    pdfs("wfFrame" + pdf("CurrentDesk")).el('hdnMailMode').value = getQueryStringValue(document.location.href, "MailMode");
    var oCallback = new Callback();
    oCallback.add("CustomerID", el("hdnCustomer").value);
    oCallback.add("FromEmail", el("txtFrom").value);
    oCallback.add("ToEmail", el("txtTo").value);
    oCallback.add("CCEmail", el("txtCC").value);
    oCallback.add("BCCEmail", el("txtBCC").value);
    oCallback.add("Subject", el("txtSubject").value);
    //oCallback.add("EmailBody", el("txtBody").innerText);
    oCallback.add("EmailBody", getText(el('txtBody')));
    oCallback.add("MailMode", pdfs("wfFrame" + pdf("CurrentDesk")).el('hdnMailMode').value);
    oCallback.invoke("SendEmail.aspx", "SaveDetails");
    if (oCallback.getCallbackStatus()) {
        ppsc().closeModalWindow();
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        pdfs("wfFrame" + pdf("CurrentDesk")).getSearchDetails();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true
}
function CloseDetails() {
    psc().closeModalWindow();
}

function getSearchDetails() {
    if (checkActivity() == true) {
        var strActivityID = "";
        if (el("lkpCustomer").SelectedValues[0] != "" && el("lkpCustomer").SelectedValues[0] != null) {
            var oCallback = new Callback();
            oCallback.add("Customer", el("lkpCustomer").SelectedValues[0]);
            oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
            oCallback.add("InDateFrom", el("datInDateFrom").value);
            oCallback.add("InDateTo", el("datInDateTo").value);
            if (el("chkCleaning").checked) {
                strActivityID = "74";
            }
            if (el("chkRepairEstimate").checked) {
                if (strActivityID != "" && strActivityID != null) {
                    strActivityID = strActivityID + ",";
                }
                strActivityID = strActivityID + "73";
            }
            if (el("chkRepairApproval").checked) {
                if (strActivityID != "" && strActivityID != null) {
                    strActivityID = strActivityID + ",";
                }
                strActivityID = strActivityID + "72";
            }
            if (el("chkRepairCompletion").checked) {
                if (strActivityID != "" && strActivityID != null) {
                    strActivityID = strActivityID + ",";
                }
                strActivityID = strActivityID + "118";
            }
            oCallback.add("ActivityId", strActivityID);
            oCallback.add("EventDateFrom", el("datEventDateFrom").value);
            oCallback.add("EventDateTo", el("datEventDateTo").value);
            oCallback.add("Email", el("lkpEmail").SelectedValues[0]);
            oCallback.invoke("BulkEmail.aspx", "GetSearchDetails");
            if (oCallback.getCallbackStatus()) {
                if (oCallback.getReturnValue("RowCount") != "0") {
                    showDiv("divBulkMail");
                    showDiv("btnSubmit");
                    bindTracking("Search");
                    //el('hypSearch').innerText = " Reset"
                    setText(el('hypSearch'), "Reset");
                    hideDiv("divRecordNotFound");
                }
                else {
                    hideDiv("divBulkMail");
                    hideDiv("btnSubmit");
                    //el('hypSearch').innerText = " Reset"
                    setText(el('hypSearch'), "Reset");
                    showDiv("divRecordNotFound");
                }
            }
            else {
                showErrorMessage(oCallback.getCallbackError());
            }
            oCallback = null;
        }
        else {
            set_ReadOnly_values(false);
            disableActivity(false);
            showErrorMessage('Please select Coustomer');
            setFocusToField("lkpCustomer");
            return false;
        }
    }
    else {
        set_ReadOnly_values(false);
        disableActivity(false);
        showErrorMessage('Please select atleast one Activity Type');
        return false;
    }

}

function checkActivity() {
    if (el("chkCleaning").checked == false && el("chkRepairEstimate").checked == false && el("chkRepairApproval").checked == false && el("chkRepairCompletion").checked == false) {
        return false;
    }
    else {
        return true;
    }
}

function checkAllActivity(blnStatus) {
    el("chkCleaning").checked = blnStatus;
    el("chkRepairEstimate").checked = blnStatus;
    el("chkRepairApproval").checked = blnStatus;
    el("chkRepairCompletion").checked = blnStatus;
}

function disableActivity(blnStatus) {
    el("chkCleaning").disabled = blnStatus;
    el("chkRepairEstimate").disabled = blnStatus;
    el("chkRepairApproval").disabled = blnStatus;
    el("chkRepairCompletion").disabled = blnStatus;
}

function bulkmailResend(_rowIndex, _CustomerId, _ActivityName, _gateinTransactionNo, _EquipmentNo, _LastSendDate) {
    var noOfTimeSent = new Number;
    if (_CustomerId != "" && _CustomerId != null) {
        var cols = ifgBulkEmail.Rows(ifgBulkEmail.CurrentRowIndex()).Columns();
        var cCols = ifgBulkEmail.Rows(_rowIndex).GetClientColumns();
        if (cCols[9] == "" && cCols[9] == null) {
            noOfTimeSent = 0;
        }
        else {
            noOfTimeSent = cCols[9];
        }

        if (!_CustomerId) {
            if (cols.length == 1) {
                if (ifgBulkEmail.Submit(true) == false) {
                    return false;
                }
            }
        }
        else {
            if (noOfTimeSent >= 1) {
                var oCallback = new Callback();
                oCallback.add("GateinTransactionNo", _gateinTransactionNo);
                oCallback.add("EquipmentNo", _EquipmentNo);
                oCallback.add("ActivityName", _ActivityName);
                oCallback.invoke("BulkEmail.aspx", "GetBulkEmailResend");
                if (oCallback.getCallbackStatus()) {
                    if (oCallback.getReturnValue("AttachmentPath") != "" || oCallback.getReturnValue("AttachmentPath") != null) {
                        //el('hdnMailMode').value = "Resend";
                        showModalWindow("Send Email", "Tracking/SendEmail.aspx?CustomerID=" + _CustomerId + "&ActivityName=" + _ActivityName + "&MailMode=Resend", "600px", "350px", "100px", "", "");
                    }
                }
                else {
                    showErrorMessage(oCallback.getCallbackError());
                }
            }

            return true;
            psc().hideLoader();
        }

    }
}

try{
$(window.parent).resize(function () {
    reSizePane();
});
} catch (e) { }

function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 670) {
            if (!chrome) {
                try{
                    el("divBulkEmail").style.height = $(window.parent).height() - 228 + "px";
                } catch (e) { }
            }
            else {
                el("divBulkEmail").style.height = $(window.parent).height() - 235 + "px";
            }
            if (el("ifgBulkEmail") != null) {
                if (!chrome) {
                    try{
                        el("ifgBulkEmail").SetStaticHeaderHeight($(window.parent).height() - 447 + "px");
                    } catch (e) { }
                }
                else {
                    el("ifgBulkEmail").SetStaticHeaderHeight($(window.parent).height() - 473 + "px");
                }
            }
        }
        else if ($(window.parent).height() < 768) {
            el("divBulkEmail").style.height = $(window.parent).height() - 235 + "px";
            if (el("ifgBulkEmail") != null) {
                el("ifgBulkEmail").SetStaticHeaderHeight($(window.parent).height() - 500 + "px");
            }
        }
        else {
            if (!chrome) {
                try{
                    el("divBulkEmail").style.height = $(window.parent).height() - 233 + "px";
                } catch (e) { }
            }
            else {
                el("divBulkEmail").style.height = $(window.parent).height() - 244 + "px";
            }
            if (el("ifgBulkEmail") != null) {
                if (!chrome) {
                    try{
                        el("ifgBulkEmail").SetStaticHeaderHeight($(window.parent).height() - 456 + "px");
                    } catch (e) { }
                }
                else {
                    el("ifgBulkEmail").SetStaticHeaderHeight($(window.parent).height() - 482 + "px");
                }
            }
        }

    }

}