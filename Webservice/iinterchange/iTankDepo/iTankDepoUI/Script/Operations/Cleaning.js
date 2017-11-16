var HasChanges = false;
var recordLockChanges = false;
var CertNo;
var blnPrint = false;

if (window.$) {
    $(document).ready(function () {

        reSizePane();
    });
}



function initPage(sMode) {
    reSizePane();
    //$("#spnHeader").text(getPageTitle());
    if (sMode == "new") {
        setFocusToField("datOriginalCleaningDate");
        setReadOnly("txtCleaningRate", false);
        setReadOnly("datLastCleaningDate", false);

        setReadOnly("txtCleaningReferenceNo", false);
        el('chkAdditionalCleaningBit').disabled = true;
        setPageMode("new");
        resetValidators();
       
        blnPrint = false;
        //        hideDiv("divGenerateCleaningCertificate");
        if (el("hdnAddtnlClnngFlg").value == "True") {
            setReadOnly("datOriginalCleaningDate", true);
        }

    }
    if (sMode == "edit") {
        setReadOnly("datOriginalCleaningDate", true);
        setReadOnly("txtRemarks", false);
        if (el("hdnEditDate").value == "C") {
        }
        else {
            setFocusToField("datLastCleaningDate");
        }

        if (el("hdnBillingFlag").value == "B") {
           
            setFocusToField("datLastCleaningDate");
            setReadOnly("txtCleaningRate", true);
            //            setReadOnly("lkpInvcngTo", true);
            setReadOnly("txtCleaningReferenceNo", true);
            setPageMode("edit");
            resetValidators();
        }
        else if (el("hdnBillingFlag").value == "U") {
            setReadOnly("txtCleaningRate", false);
            setReadOnly("txtCleaningReferenceNo", false);
        }
        showInvoiceParty();
        blnPrint = false;
//        showDiv("divGenerateCleaningCertificate");
    }
    recordLockError(el('txtEquipmentNo').value, el('hdnLockUserName').value, el('hdnIpError').value, el('hdnLockActivityName').value);
    hideDiv("divCleaningRatelbl");
    hideDiv("divCleaningRatelkp");
}

function setHasChange() {
    HasChanges = true;
}

function submitPage() {
    if (recordLockChanges == false) {
        if (validateRemarks() == true) {
            GetLookupChanges();
            var sMode = getPageMode();
            Page_ClientValidate();
            if (!Page_IsValid) {
                return false;
            }

            if (sMode == "new") {
                return CreateCleaning();
            }
            else if (sMode == "edit") {
                if (getPageChanges()) {
                    //                CheckInvoiceParty();
                    return UpdateCleaning();
                }
                else {
                    showInfoMessage('No Changes to Save');
                }
            }

            return true;
        }
        else {
            setFocusToField("txtRemarks");
            showErrorMessage("Remarks Required");
            return false;
        }
    }
    else {
        recordLockMessageEdit(el("txtEquipmentNo").value, el("hdnLockUserName").value, el('hdnIpError').value, el('hdnLockActivityName').value);
        return false;
    }
}

function GetCurrencyCode() {
    var oCallback = new Callback();
    //    oCallback.add("InvoicingPartyId", el('lkpInvcngTo').SelectedValues[0]);
    oCallback.invoke("Cleaning.aspx", "UpdateCurrencyCode");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("CurrencyCode") != "" && oCallback.getReturnValue("CurrencyCode") != null) {
            var CurrencyCode = oCallback.getReturnValue("CurrencyCode");
            //el("lblCurrency").innerText = CurrencyCode;
            setText(el('lblCurrency'), CurrencyCode);
        }
        else {
            args.IsValid = true;
        }
    } else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}
function CreateCleaning() {
    var oCallback = new Callback();
    oCallback.add("CleaningID", getPageID());
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("ChemicalName", el("txtChemicalName").value);
    oCallback.add("CleaningRate", el("txtCleaningRate").value);
    oCallback.add("OriginalCleaningDate", el("datOriginalCleaningDate").value);
    oCallback.add("LastCleaningDate", el("datLastCleaningDate").value);
    oCallback.add("EquipmentStatus", el("lkpStatus").SelectedValues[0]);
    oCallback.add("EquipmentStatusCD", el("lkpStatus").value);
    oCallback.add("CleaningReferenceNo", el("txtCleaningReferenceNo").value);
    oCallback.add("Remarks", el("txtRemarks").value);
    oCallback.add("CustomerId", el("hdnCustomerId").value);
    oCallback.add("GateInDate", el("datGateInDate").value);
    oCallback.add("GI_TRNSCTN_NO", el("hdnGI_TRNSCTN_NO").value);
    oCallback.add("ActivityId", getQueryStringValue(document.location.href, "activityid"));
    oCallback.add("blnAdditionalCleaningBit", el("chkAdditionalCleaningBit").checked);
    oCallback.add("SlabRateFlag", el("hdnSlabRateFlg").value);

    oCallback.invoke("Cleaning.aspx", "CreateCleaning");
    if (oCallback.getCallbackStatus()) {
        setReadOnly("txtRemarks", false);
        el('chkAdditionalCleaningBit').checked = false;
        setPageMode(MODE_EDIT);
        setReadOnly("datOriginalCleaningDate", true);
        setPageID(oCallback.getReturnValue("ID"));
        if (oCallback.getReturnValue("ActivitySubmit") == "" || oCallback.getReturnValue("ActivitySubmit") == null) {
            blnPrint = true;
//            GenerateCleaningCertificate();
            showInfoMessage(oCallback.getReturnValue("Message"));
            HasChanges = false;
        }
        else {
            activitySubmitMessage(oCallback.getReturnValue("ActivitySubmit"));
        }
        return true;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
}

function UpdateCleaning() {
    var oCallback = new Callback();
    oCallback.add("CleaningID", getPageID());
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("ChemicalName", el("txtChemicalName").value);
    oCallback.add("CleaningRate", el("txtCleaningRate").value);
    oCallback.add("OriginalCleaningDate", el("datOriginalCleaningDate").value);
    oCallback.add("LastCleaningDate", el("datLastCleaningDate").value);
    oCallback.add("EquipmentStatus", el("lkpStatus").SelectedValues[0]);
    oCallback.add("EquipmentStatusCD", el("lkpStatus").value);
    oCallback.add("CleaningReferenceNo", el("txtCleaningReferenceNo").value);
    oCallback.add("Remarks", el("txtRemarks").value);
    oCallback.add("CustomerId", el("hdnCustomerId").value);
    oCallback.add("GateInDate", el("datGateInDate").value);
    oCallback.add("GI_TRNSCTN_NO", el("hdnGI_TRNSCTN_NO").value);
    oCallback.add("ActivityId", getQueryStringValue(document.location.href, "activityid"));
    oCallback.add("blnAdditionalCleaningBit", el("chkAdditionalCleaningBit").checked);
  //  oCallback.add("SlabRateFlag", el("hdnSlabRateFlg").checked);

    oCallback.invoke("Cleaning.aspx", "UpdateCleaning");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ActivitySubmit") == "" || oCallback.getReturnValue("ActivitySubmit") == null) {
            setPageID(oCallback.getReturnValue("ID"));
            showInfoMessage(oCallback.getReturnValue("Message"));
        }
        else {
            activitySubmitMessage(oCallback.getReturnValue("ActivitySubmit"));
        }
        HasChanges = false;
        return true;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
}


function selectInvoicingParty(obj) {
    //    if (obj.checked) {
    //        el('divInvoicingParty').style.display = "block";
    //        el('divInvoicingParty1').style.display = "block";
    //    }
}

function showInvoiceParty() {
    //    if (el("lkpInvcngTo").value != "") {
    ////        el("rbtnParty").checked = true
    //        el('divInvoicingParty').style.display = "block";
    //        el('divInvoicingParty1').style.display = "block";
    //    }
    //    else {
    //        el("rbtnCustomer").checked = true
    //        el('divInvoicingParty').style.display = "none";
    //        el('divInvoicingParty1').style.display = "none";
    //    }
}

function selectCustomer(obj) {
    if (obj.checked) {
        //        clearLookupValues("lkpInvcngTo");
        //        el('divInvoicingParty').style.display = "none";
        //        el('divInvoicingParty1').style.display = "none";
        //el("lblCurrency").innerText = el("hdnCurrencyCode").value;
        setText(el('lblCurrency'), el("hdnCurrencyCode").value);
    }
}

function ValidateCleaningDate(oSrc, args) {

    if (el("datOriginalCleaningDate").value == "" || el("datOriginalCleaningDate").value == null) {
        oSrc.errormessage = "Original Cleaning Date Required";
        args.IsValid = false;
        return;
    }

    var oCallback = new Callback();
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("EventDate", el("datOriginalCleaningDate").value);
    oCallback.invoke("Cleaning.aspx", "ValidatePreviousActivityDate");

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


//datLastCleaningDate

function ValidateLatestCleaningDate(oSrc, args) {

    if (el("datLastCleaningDate").value == "" || el("datLastCleaningDate").value == null) {
        oSrc.errormessage = "Latest Cleaning Date Required";
        args.IsValid = false;
        return;
    }

    var oCallback = new Callback();
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("OriginalDate", el("datOriginalCleaningDate").value);
    oCallback.add("EventDate", el("datLastCleaningDate").value);
    oCallback.invoke("Cleaning.aspx", "ValidateLastActivityDate");

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

function ValidateLastInspectedDate(oSrc, args) {
    //    if (el("datOriginalInspectedDate").value == "" || el("datOriginalInspectedDate").value == null) {
    //        oSrc.errormessage = "Original Inspected Date Required";
    //        args.IsValid = false;
    //        return;
    //    }
    //    if (el("datLastInspectedDate").value == "" || el("datLastInspectedDate").value == null) {
    //        oSrc.errormessage = "Latest Inspection Date Required";
    //        args.IsValid = false;
    //        return;
    //    }

    var oCallback = new Callback();
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    //    oCallback.add("EventDate", el("datLastInspectedDate").value);
    //    oCallback.add("OriginalDate", el("datOriginalInspectedDate").value);
    oCallback.invoke("Cleaning.aspx", "ValidateLastActivityDate");

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

function ValidateOriginalInspectedDate(oSrc, args) {

    if (el("datOriginalCleaningDate").value == "" || el("datOriginalCleaningDate").value == null) {
        oSrc.errormessage = "Oringal Cleaning Date Required";
        args.IsValid = false;
        return;
    }
    //    if (el("datOriginalInspectedDate").value == "" || el("datOriginalInspectedDate").value == null) {
    //        oSrc.errormessage = "Original Inspection Date Required";
    //        args.IsValid = false;
    //        return;
    //    }

    var oCallback = new Callback();
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    //    oCallback.add("EventDate", el("datOriginalInspectedDate").value);
    oCallback.add("CleaningDate", el("datOriginalCleaningDate").value);
    oCallback.invoke("Cleaning.aspx", "ValidateOriginalInspectedDate");

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

function GenerateCleaningCertificate() {
    // if (el("lblCrtfct_No").innerText == "") {
    //    if (getText(el('lblCrtfct_No')) == "" || getText(el('lblCrtfct_No')) == null)
    //   {


    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("GI_TRNSCTN_NO", el("hdnGI_TRNSCTN_NO").value);
    oCallback.add("OriginalCleaningDate", el("txtEquipmentNo").value);
    oCallback.invoke("Cleaning.aspx", "CleaningCertificate");
    if (oCallback.getCallbackStatus()) {

        CertNo = oCallback.getReturnValue("CertNo")
        //el("lblCrtfct_No").innerText = CertNo;
        //            setText(el('lblCrtfct_No'), CertNo);
    }
    oCallback = null;

    PrintCleaningCertificate();
    return true;
    //    }

}

function PrintCleaningCertificate() {
    // if (el("lblCrtfct_No").innerText != "") {
    //    if (getText(el('lblCrtfct_No')) != "" && getText(el('lblCrtfct_No')) != null) {

    if (blnPrint == true) {
        var oDocPrint = new DocumentPrint();
        oDocPrint.KeyName = "Cleaning";
        oDocPrint.Type = "document";
        oDocPrint.Title = "Cleaning";
        oDocPrint.DocumentId = "18";
        oDocPrint.add("CleaningID", getPageID());
        oDocPrint.add("DepotID", el("hdnDepotID").value);
        oDocPrint.add("EquipmentNo", el("txtEquipmentNo").value);
        // oDocPrint.add("CleanginCertificateNo", el("lblCrtfct_No").innerText);
        oDocPrint.add("CleanginCertificateNo", CertNo);
        oDocPrint.add("GI_TRNSCTN_NO", el("hdnGI_TRNSCTN_NO").value);
        oDocPrint.ReportPath = "../Documents/Report/CleaningCertificate.rdlc";
        oDocPrint.openReportDialog();
        //    }
        //    else {
        //        showErrorMessage("Record must be submitted before generating Cleaning Certificate");
        //        return false;
        //    }
    }
    else {
        showErrorMessage("Record must be submitted before generating Cleaning Certificate");
    }
}

function fnSetLastCleaningDate() {
    var cleaningdate = el("datOriginalCleaningDate").value;
    el("datLastCleaningDate").value = cleaningdate;
}

//Check Last Inspected Date
function fnSetLastInspectedDate() {
    //    var inpsectiondate = el("datOriginalInspectedDate").value;
    //    el("datLastInspectedDate").value = inpsectiondate;
}


//Conditional Validation for Remarks
function validateRemarks() {
    if (el('chkAdditionalCleaningBit').checked) {
        if (el("txtRemarks").value == "" || el("txtRemarks").value == null) {
            return false
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }
}
$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window) != null) {

        //hide the Green line
        hideDiv("divLockMessage");
        if ($(window.parent).height() < 670) {
            //el("tabCleaning").style.height = $(window.parent).height() - 240 + "px";
            if (el("ifgCleaning") != null) {
                el("ifgCleaning").SetStaticHeaderHeight($(window.parent).height() - 310 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            //        el("tabGateOut").style.height = "440px"
            //el("tabCleaning").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgCleaning") != null) {

                //            el("ifgEquipmentDetail").SetStaticHeaderHeight(360 + "px");
                el("ifgCleaning").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }
        }
        else {
           // el("tabCleaning").style.height = $(window.parent).height() - 245 + "px";
            if (el("ifgCleaning") != null) {
                el("ifgCleaning").SetStaticHeaderHeight($(window.parent).height() - 316 + "px");
            }
        }
        if ($(window.parent).height() < 768) {
            el("divCleaning").style.height = $(window.parent).height() - 243 + "px";
        }
        else {
            el("divCleaning").style.height = $(window.parent).height() - 240 + "px";
        }
    }

}


function PrintCleaningInstructionReport() {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "Cleaning Instruction";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Cleaning Instruction";
    oDocPrint.DocumentId = "30";
    oDocPrint.ReportPath = "../Documents/Report/CleaningInstruction.rdlc";
    oDocPrint.openReportDialog();
}

