var HasChanges = false;
var recordLockChanges = false;
var strActvity;

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}


function initPage(sMode) {
    reSizePane();
    //$("#spnHeader").text(getPageTitle());
    if (sMode == "new") {
        setFocusToField("datOriginalCleaningDate");
       // setReadOnly("txtCleaningRate", false);
        setReadOnly("datLastCleaningDate", false);
        setReadOnly("datLastInspectedDate", false);
        setReadOnly("lkpInvcngTo", false);
        setReadOnly("txtManLidSealNo", false);
        setReadOnly("txtTopSealNo", false);
        setReadOnly("txtOutletSealNo", false);
//        setReadOnly("txtCustReferenceNo", false);
        setReadOnly("txtCleaningReferenceNo", false);
        el('chkAdditionalCleaningBit').disabled = false;
        clearTextValues("lkpInvcngTo");
        el("rbtnCustomer").checked = true
        el('lkpInvcngTo').SelectedValues[0] = "";
        el('divInvoicingParty').style.display = "none";
        el('divInvoicingParty1').style.display = "none";
        setPageMode("new");
        resetValidators();
        //        if (el('chkAdditionalCleaningBit').checked) {
        //            setReadOnly("txtRemarks", true);
        //            setReadOnly("datOriginalCleaningDate", true);
        //            setReadOnly("datOriginalInspectedDate", true);
        //    }
        //        else {
        //            setReadOnly("txtRemarks", false);
        //            setReadOnly("datOriginalCleaningDate", false);
        //            setReadOnly("datOriginalInspectedDate", false);
        //        }

        if (el("hdnAddtnlClnngFlg").value == "True") {
            setReadOnly("datOriginalInspectedDate", true);
        }

        if (el("hdnSlabRateFlag").value == "True") {
            setReadOnly("txtCleaningRate", true);
            el('txtCleaningRate').className = "txtd";
        }

        hideDiv("divGenerateCleaningCertificate");
        setFocusToField("datOriginalInspectedDate");
    }
    if (sMode == "edit") {
        el('chkAdditionalCleaningBit').disabled = true;
        setReadOnly("datOriginalCleaningDate", true);
        setReadOnly("txtRemarks", false);
        if (el("hdnEditDate").value == "C") {
            setFocusToField("datOriginalInspectedDate");
            setReadOnly("datOriginalInspectedDate", false);
        }
        else {
            setFocusToField("datLastCleaningDate");
            setReadOnly("datOriginalInspectedDate", true);
        }
        if (el("hdnSlabRateFlag").value == "True") {
            setReadOnly("txtCleaningRate", true);
            el('txtCleaningRate').className = "txtd";
        }
        //        if (el('hdnStatusId').value == "5") {
        //            el('chkAdditionalCleaningBit').disabled = false;
        //        }
        //        else {
        //            el('chkAdditionalCleaningBit').disabled = true;
        //        }
        if (el("hdnBillingFlag").value == "B") {
            //            el('chkAdditionalCleaningBit').disabled = true;
            setFocusToField("datLastCleaningDate");
            setReadOnly("datOriginalInspectedDate", true);
            setReadOnly("txtCleaningRate", true);
            setReadOnly("lkpInvcngTo", true);
            //            setReadOnly("txtCustReferenceNo", true);
            setReadOnly("txtCleaningReferenceNo", true);
            el("rbtnParty").disabled = true
            el("rbtnCustomer").disabled = true
            setPageMode("edit");
            resetValidators();
        }
        else if (el("hdnBillingFlag").value == "U") {
            if (el("hdnSlabRateFlag").value != "True") {
                setReadOnly("txtCleaningRate", false);
            }
            setReadOnly("lkpInvcngTo", false);
            setReadOnly("txtManLidSealNo", false);
            setReadOnly("txtTopSealNo", false);
            setReadOnly("txtOutletSealNo", false);
            //            setReadOnly("txtCustReferenceNo", false);
            setReadOnly("txtCleaningReferenceNo", false);
        }
        showInvoiceParty();
        showDiv("divGenerateCleaningCertificate");

        if (el("hdnStatusId").value != "6") {
            setReadOnly("datLastInspectedDate", true);
            strActvity = "next";
        }
        else {
            setReadOnly("datLastInspectedDate", false);
            strActvity = "current";
        }
    }



    recordLockError(el('txtEquipmentNo').value, el('hdnLockUserName').value, el('hdnIpError').value, el('hdnLockActivityName').value);
    setFocusToField("datOriginalInspectedDate");
    setReadOnly("datOriginalCleaningDate", true);
    setReadOnly("datLastCleaningDate", true);
    setReadOnly("txtCustReferenceNo", true);
    
}

function ValidateInvoicingParty(oSrc, args) {
    if (el("rbtnParty").checked == true && el('lkpInvcngTo').SelectedValues[0] == "") {
        oSrc.errormessage = "Invoicing To Party Required";
        args.IsValid = false;
        return;
    } else {
        args.IsValid = true;
        return;
    }
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

        if (customerEDIFlag() == true) {

            if (el("txtCleaningReferenceNo").value == "") {
                showErrorMessage("Cleaning reference no required. Since Customer have EDI generation Option.");
                setFocusToField("txtCleaningReferenceNo");
                return false;
            }
            else {

                if (!check_NumericValidation(el("txtCleaningReferenceNo").value)) {
                    showErrorMessage("Only Numbers are allowed");
                    return false;
                }
            }
        }

        if (el("rbtnParty").checked == true && el('lkpInvcngTo').SelectedValues[0] == "") {
            showErrorMessage("Invoicing To Party Required");
            return false;
        }
        if (sMode == "new") {
            return CreateCleaning();
        }
        else if (sMode == "edit") {
            if (getPageChanges()) {
                CheckInvoiceParty();
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
function CheckInvoiceParty() {
    if (el("rbtnCustomer").checked == true) {
        clearLookupValues("lkpInvcngTo");
        el('divInvoicingParty').style.display = "none";
        el('divInvoicingParty1').style.display = "none";
    }
    else if (el("rbtnParty").checked == true) {
        el('divInvoicingParty').style.display = "block";
        el('divInvoicingParty1').style.display = "block";
    }
}
function GetCurrencyCode() {
    var oCallback = new Callback();
    oCallback.add("InvoicingPartyId", el('lkpInvcngTo').SelectedValues[0]);
    oCallback.invoke("Inspection.aspx", "UpdateCurrencyCode");
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

    if (el("chkAdditionalCleaningBit").checked == true && el("lkpAddtnCleaning").value == "") {
        showErrorMessage("Additional Cleaning Status Required");
        setFocusToField("lkpAddtnCleaning");
        return false;
    }

    var oCallback = new Callback();
    //    oCallback.add("CleaningID", getPageID());
    oCallback.add("CleaningID", el("hdnCleaningID").value);
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    //oCallback.add("CretNo", el("lblCrtfct_No").innerText);
    oCallback.add("CretNo", getText(el('lblCrtfct_No')));
    oCallback.add("ChemicalName", el("txtChemicalName").value);
    oCallback.add("CleaningRate", el("txtCleaningRate").value);
    oCallback.add("OriginalCleaningDate", el("datOriginalCleaningDate").value);
    oCallback.add("LastCleaningDate", el("datLastCleaningDate").value);
    oCallback.add("OriginalInspectedDate", el("datOriginalInspectedDate").value);
    oCallback.add("LastInspectedDate", el("datLastInspectedDate").value);
    oCallback.add("EquipmentStatus", el("lkpStatus").SelectedValues[0]);
    oCallback.add("EquipmentStatusCD", el("lkpStatus").value);
    oCallback.add("CleanedFor", el("txtCleanedFor").value);
    oCallback.add("LocOfCleaning", el("txtLocOfCleaning").value);
    oCallback.add("EquipmentCleaningStatus1", el("lkpCleaningStatus").SelectedValues[0]);
    oCallback.add("EquipmentCleaningStatus1CD", el("lkpCleaningStatus").value);
    oCallback.add("EquipmentCleaningStatus2", el("lkpEqpmntCleaningStatus2").SelectedValues[0]);
    oCallback.add("EquipmentCleaningStatus2CD", el("lkpEqpmntCleaningStatus2").value);
    oCallback.add("EquipmentCondition", el("lkpEqpmntCondition").SelectedValues[0]);
    oCallback.add("EquipmentConditionCD", el("lkpEqpmntCondition").value);
    oCallback.add("ValveandFittingCondition", el("lkpValveFittingCondition").SelectedValues[0]);
    oCallback.add("ValveandFittingConditionCD", el("lkpValveFittingCondition").value);
    oCallback.add("InvoicingTo", el("lkpInvcngTo").SelectedValues[0]);
    oCallback.add("InvoicingToCD", el("lkpInvcngTo").value);
    oCallback.add("ManLidSealNo", el("txtManLidSealNo").value);
    oCallback.add("TopSealNo", el("txtTopSealNo").value);
    oCallback.add("BottomSealNo", el("txtOutletSealNo").value);
    oCallback.add("CustomerReferenceNo", el("txtCustReferenceNo").value);
    oCallback.add("CleaningReferenceNo", el("txtCleaningReferenceNo").value);
    oCallback.add("Remarks", el("txtRemarks").value);
    oCallback.add("CustomerId", el("hdnCustomerId").value);
    oCallback.add("GateInDate", el("datGateInDate").value);
    oCallback.add("GI_TRNSCTN_NO", el("hdnGI_TRNSCTN_NO").value);
    oCallback.add("ActivityId", getQueryStringValue(document.location.href, "activityid"));
    oCallback.add("blnAdditionalCleaningBit", el("chkAdditionalCleaningBit").checked);
    oCallback.add("AdditionalCleaningStatus", el("lkpAddtnCleaning").SelectedValues[1]);

    oCallback.invoke("Inspection.aspx", "CreateCleaning");
    if (oCallback.getCallbackStatus()) {
        setReadOnly("txtRemarks", false);
        el('chkAdditionalCleaningBit').disabled = false;
        //        el('chkAdditionalCleaningBit').checked = false;

        //  CleaningCertificate
        el('lblCrtfct_No').value = oCallback.getReturnValue("CleaningCertificate");
        if (el('chkAdditionalCleaningBit').checked == true) {
            hideDiv("divSubmit");
        }
        else {
            showDiv("divSubmit");
        }
        setPageMode(MODE_EDIT);
        setReadOnly("datOriginalCleaningDate", true);
        setReadOnly("datOriginalInspectedDate", true);
        setPageID(oCallback.getReturnValue("ID"));
        if (oCallback.getReturnValue("ActivitySubmit") == "" || oCallback.getReturnValue("ActivitySubmit") == null) {
//            GenerateCleaningCertificate();
            showInfoMessage(oCallback.getReturnValue("Message"));
            HasChanges = false;
            el("chkAdditionalCleaningBit").disabled = true;
        }
        else {
            activitySubmitMessage(oCallback.getReturnValue("ActivitySubmit"));
        }
        if (oCallback.getReturnValue("Status") != "" && oCallback.getReturnValue("Status") != null) {
            if (oCallback.getReturnValue("Status") != "6") {
                strActvity = "next";
            }
            else {
                strActvity = "current";
            }
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
    //    oCallback.add("CleaningID", getPageID());
    oCallback.add("CleaningID", el("hdnCleaningID").value);
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    //oCallback.add("CretNo", el("lblCrtfct_No").innerText);
    oCallback.add("CretNo", getText(el('lblCrtfct_No')));
    oCallback.add("ChemicalName", el("txtChemicalName").value);
    oCallback.add("CleaningRate", el("txtCleaningRate").value);
    oCallback.add("OriginalCleaningDate", el("datOriginalCleaningDate").value);
    oCallback.add("LastCleaningDate", el("datLastCleaningDate").value);
    oCallback.add("OriginalInspectedDate", el("datOriginalInspectedDate").value);
    oCallback.add("LastInspectedDate", el("datLastInspectedDate").value);
    oCallback.add("EquipmentStatus", el("lkpStatus").SelectedValues[0]);
    oCallback.add("EquipmentStatusCD", el("lkpStatus").value);
    oCallback.add("CleanedFor", el("txtCleanedFor").value);
    oCallback.add("LocOfCleaning", el("txtLocOfCleaning").value);
    oCallback.add("EquipmentCleaningStatus1", el("lkpCleaningStatus").SelectedValues[0]);
    oCallback.add("EquipmentCleaningStatus1CD", el("lkpCleaningStatus").value);
    oCallback.add("EquipmentCleaningStatus2", el("lkpEqpmntCleaningStatus2").SelectedValues[0]);
    oCallback.add("EquipmentCleaningStatus2CD", el("lkpEqpmntCleaningStatus2").value);
    oCallback.add("EquipmentCondition", el("lkpEqpmntCondition").SelectedValues[0]);
    oCallback.add("EquipmentConditionCD", el("lkpEqpmntCondition").value);
    oCallback.add("ValveandFittingCondition", el("lkpValveFittingCondition").SelectedValues[0]);
    oCallback.add("ValveandFittingConditionCD", el("lkpValveFittingCondition").value);
    oCallback.add("InvoicingTo", el("lkpInvcngTo").SelectedValues[0]);
    oCallback.add("InvoicingToCD", el("lkpInvcngTo").value);
    oCallback.add("ManLidSealNo", el("txtManLidSealNo").value);
    oCallback.add("TopSealNo", el("txtTopSealNo").value);
    oCallback.add("BottomSealNo", el("txtOutletSealNo").value);
    oCallback.add("CustomerReferenceNo", el("txtCustReferenceNo").value);
    oCallback.add("CleaningReferenceNo", el("txtCleaningReferenceNo").value);
    oCallback.add("Remarks", el("txtRemarks").value);
    oCallback.add("CustomerId", el("hdnCustomerId").value);
    oCallback.add("GateInDate", el("datGateInDate").value);
    oCallback.add("GI_TRNSCTN_NO", el("hdnGI_TRNSCTN_NO").value);
    oCallback.add("ActivityId", getQueryStringValue(document.location.href, "activityid"));
    oCallback.add("blnAdditionalCleaningBit", el("chkAdditionalCleaningBit").checked);
    oCallback.add("NextAcitivity", strActvity);

    oCallback.invoke("Inspection.aspx", "UpdateCleaning");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ActivitySubmit") == "" || oCallback.getReturnValue("ActivitySubmit") == null) {
            setPageID(oCallback.getReturnValue("ID"));
            showInfoMessage(oCallback.getReturnValue("Message"));
            el("chkAdditionalCleaningBit").disabled = true;
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
    if (obj.checked) {
        el('divInvoicingParty').style.display = "block";
        el('divInvoicingParty1').style.display = "block";
    }
}

function showInvoiceParty() {
    if (el("lkpInvcngTo").value != "") {
        el("rbtnParty").checked = true
        el('divInvoicingParty').style.display = "block";
        el('divInvoicingParty1').style.display = "block";
    }
    else {
        el("rbtnCustomer").checked = true
        el('divInvoicingParty').style.display = "none";
        el('divInvoicingParty1').style.display = "none";
    }
}

function selectCustomer(obj) {
    if (obj.checked) {
        clearLookupValues("lkpInvcngTo");
        el('divInvoicingParty').style.display = "none";
        el('divInvoicingParty1').style.display = "none";
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
    oCallback.invoke("Inspection.aspx", "ValidatePreviousActivityDate");

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

//function ValidateLastInspectedDate(oSrc, args) {
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

//    var oCallback = new Callback();
//    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
//    oCallback.add("EventDate", el("datLastInspectedDate").value);
//    oCallback.add("OriginalDate", el("datOriginalInspectedDate").value);
//    oCallback.invoke("Inspection.aspx", "ValidateLastActivityDate");

//    if (oCallback.getCallbackStatus()) {
//        if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {
//            args.IsValid = false;
//            oSrc.errormessage = oCallback.getReturnValue("Error");
//        }
//        else {
//            args.IsValid = true;
//        }
//    }
//    else {
//        showErrorMessage(oCallback.getCallbackError());
//    }
//    oCallback = null;
//}

function ValidateLastInspectedDate(oSrc, args) {

    if (el("datLastInspectedDate").value == "" || el("datLastInspectedDate").value == null) {
        oSrc.errormessage = "Latest Cleaning Date Required";
        args.IsValid = false;
        return;
    }

    var oCallback = new Callback();
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("OriginalDate", el("datOriginalInspectedDate").value);
    oCallback.add("EventDate", el("datLastInspectedDate").value);
    oCallback.invoke("Inspection.aspx", "ValidateLastActivityDate");

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
    if (el("datOriginalInspectedDate").value == "" || el("datOriginalInspectedDate").value == null) {
        oSrc.errormessage = "Original Inspection Date Required";
        args.IsValid = false;
        return;
    }

    var oCallback = new Callback();
    oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
    oCallback.add("EventDate", el("datOriginalInspectedDate").value);
    oCallback.add("CleaningDate", el("datLastCleaningDate").value);
    oCallback.invoke("Inspection.aspx", "ValidateOriginalInspectedDate");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error")!=null) {
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
    if (getText(el('lblCrtfct_No')) == "" || getText(el('lblCrtfct_No')) == null)
   {
        var oCallback = new Callback();
        oCallback.add("ID", getPageID());
        oCallback.add("EquipmentNo", el("txtEquipmentNo").value);
        oCallback.add("GI_TRNSCTN_NO", el("hdnGI_TRNSCTN_NO").value);
        oCallback.add("OriginalCleaningDate", el("txtEquipmentNo").value);
        oCallback.invoke("Inspection.aspx", "CleaningCertificate");
        if (oCallback.getCallbackStatus()) {
            var CertNo;
            CertNo = oCallback.getReturnValue("CertNo")
            //el("lblCrtfct_No").innerText = CertNo;
            setText(el('lblCrtfct_No'), CertNo);
        }
        oCallback = null;

        PrintCleaningCertificate();
        return true;
    }
}

function PrintCleaningCertificate() {
   // if (el("lblCrtfct_No").innerText != "") {
//    if (getText(el('lblCrtfct_No')) != "" && getText(el('lblCrtfct_No')) != null) {
        var oDocPrint = new DocumentPrint();
        oDocPrint.KeyName = "Cleaning";
        oDocPrint.Type = "document";
        oDocPrint.Title = "Cleaning";
        oDocPrint.DocumentId = "18";
        oDocPrint.add("CleaningID", getPageID());
        oDocPrint.add("DepotID", el("hdnDepotID").value);
        oDocPrint.add("EquipmentNo", el("txtEquipmentNo").value);
       // oDocPrint.add("CleanginCertificateNo", el("lblCrtfct_No").innerText);
        oDocPrint.add("CleanginCertificateNo", getText(el('lblCrtfct_No')))
        oDocPrint.add("GI_TRNSCTN_NO", el("hdnGI_TRNSCTN_NO").value);
        oDocPrint.ReportPath = "../Documents/Report/CleaningCertificate.rdlc";
        oDocPrint.openReportDialog();
//    }
//    else {
//        showErrorMessage("Record must be submitted before generating Cleaning Certificate");
//        return false;
//    }
}

function fnSetLastCleaningDate() {
    var cleaningdate = el("datOriginalCleaningDate").value;
    el("datLastCleaningDate").value = cleaningdate;
}

//Check Last Inspected Date
function fnSetLastInspectedDate() {
    var inpsectiondate = el("datOriginalInspectedDate").value;
    el("datLastInspectedDate").value = inpsectiondate;
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
        if ($(window.parent).height() < 768) {
            el("divCleaning").style.height = $(window.parent).height() - 243 + "px";
        }
        else {
            el("divCleaning").style.height = $(window.parent).height() - 240 + "px";
        }
    }

}

// Customer Cleaning ref No based on - EDI Bit (Customer Master)
function customerEDIFlag() {
 var oCallback = new Callback();
 oCallback.add("CustomerCode", el("txtCustomer").value);
 oCallback.add("DepotID", el("hdnDepotID").value);
 oCallback.invoke("Inspection.aspx", "CustomerEDIValidation");

 if (oCallback.getCallbackStatus()) {
     return true;
 }
 else {
     return false;
 }

}

// Show/Hide Addtional Cleaning Procedure
function fnAdditionalCleaningCheck() {
    if (el("chkAdditionalCleaningBit").checked == true) {
        showDiv("divAddtionalCleaninglbl");
        showDiv("divAddtionalCleaningLkp");
    }
    else {
        hideDiv("divAddtionalCleaninglbl");
        hideDiv("divAddtionalCleaningLkp");
    }
}


//Set Latest Inspection Date
function fnSetLastInspectionDate() {
    var cleaningdate = el("datOriginalInspectedDate").value;
    el("datLastInspectedDate").value = cleaningdate;
}


//Cleaning Ref No Validation

function IsValidNumeric(strVal) {

    var expr = /^\d+$/;
    return expr.test(strVal);

}

function check_NumericValidation(strVal) {

    if (!IsValidNumeric(strVal)) {     
        return false;
    }
    else {
        return true;
    }
}
  

