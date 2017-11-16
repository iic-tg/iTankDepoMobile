var HasChanges = false;

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
//Initialize page while loading the page based on page mode
function initPage(sMode) {
    reSizePane();
    if (sMode == MODE_NEW) {
        clearTextValues("txtInvoicePartyCode");
        clearTextValues("txtInvoicePartyName");
        clearTextValues("txtContactPersonName");
        clearTextValues("txtContactJobTitle");
        clearTextValues("txtContactAddress");
        clearTextValues("txtBillingAddress");     
        clearTextValues("txtZipCode");
        clearTextValues("txtPhoneNo");
        clearTextValues("txtFaxNo");
        clearTextValues("txtRemarks");
        clearTextValues("lkpBaseCrrncy");
        //clearTextValues("lkpCnvrtToCrrncy");
        clearTextValues("txtReportingEmailID");
        clearTextValues("txtInvoicingEmailID");
        el("chkACTV_BT").checked = true;
        setPageID("0");    
        setReadOnly("txtInvoicePartyCode", false);
        setPageMode("new");
        setFocus();
        resetValidators();
        //setActionButtonFocus("txtInvoicePartyName", "chkActive");
    }
    else {
        setReadOnly("txtInvoicePartyCode", true);
        setPageMode("edit");
       // setActionButtonFocus("txtInvoicePartyCode", "chkActive");      
        setFocus();
        resetValidators();
    }
    el('iconFav').style.display = "none";
    $('.btncorner').corner();

    //Finance Integration 
    if (getConfigSetting('FinanceIntegration') == "True" || getConfigSetting('FinanceIntegration') == "true") {
        //        alert("ON :" + getConfigSetting('FinanceIntegration'));
        showDiv("divlblLedger");
        showDiv("divtxtLedger");
    }
    else {
        //        alert("OFF :" + getConfigSetting('FinanceIntegration'));
        hideDiv("divlblLedger");
        hideDiv("divtxtLedger");
    }

}

//This method get fired while taking action from submit pane
function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        var sMode = getPageMode();

        if (getConfigSetting('FinanceIntegration') == "True" || getConfigSetting('FinanceIntegration') == "true") {
            if (el("txtLedgerId").value == "") {
                showErrorMessage("Ledger Id Required");
                setFocusToField("txtLedgerId");
                return false;
            }
        }


        if (sMode == MODE_NEW) {
            createInvoiceParty();
        }
        else if (sMode == MODE_EDIT) {
            updatInvoiceParty();
        }
    }
    else {
        showInfoMessage("No Changes to Save");
        setFocus();
    }
    return true;
}

//This method get fired while creating new record
function createInvoiceParty() {
    var oCallback = new Callback();
    oCallback.add("InvoicePartyCode", el("txtInvoicePartyCode").value);
    oCallback.add("InvoicePartyName", el("txtInvoicePartyName").value);
    oCallback.add("ContactPersonName", el("txtContactPersonName").value);
    oCallback.add("ContactJobTitle", el("txtContactJobTitle").value);
    oCallback.add("ContactAddress", el("txtContactAddress").value);  
    oCallback.add("BillingAddress", el("txtBillingAddress").value);    
    oCallback.add("ZipCode", el("txtZipCode").value);
    oCallback.add("PhoneNo", el("txtPhoneNo").value);
    oCallback.add("FaxNo", el("txtFaxNo").value);
    oCallback.add("Remarks", el("txtRemarks").value);
    oCallback.add("BaseCurrency", el("lkpBaseCrrncy").SelectedValues[0]);
  //  oCallback.add("ConvertedCurrency", el("lkpCnvrtToCrrncy").SelectedValues[0]);
    oCallback.add("ReportingEmailID", el("txtReportingEmailID").value);
    oCallback.add("InvoicingEmailID", el("txtInvoicingEmailID").value);
    oCallback.add("Active", el("chkACTV_BT").checked);

    //Finance Integration
    if (getConfigSetting('FinanceIntegration') == "True" || getConfigSetting('FinanceIntegration') == "true") {
        oCallback.add("FinanceIntegrationBit", true);
        oCallback.add("bv_LedgerId", el("txtLedgerId").value);
    }
    else {
        oCallback.add("FinanceIntegrationBit", false);
        oCallback.add("bv_LedgerId", el("txtLedgerId").value);
    }



    oCallback.add("wfData", el("WFDATA").value);
    oCallback.invoke("InvoiceParty.aspx", "InsertInvoiceParty");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        setPageMode(MODE_EDIT);
        HasChanges = false;
        setPageID(oCallback.getReturnValue("ID"));        
        setReadOnly("txtInvoicePartyCode", true);
       // setActionButtonFocus("txtInvoicePartyName", "chkActiveBit");             
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method get fired while changing new record
function updatInvoiceParty() {
    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("InvoicePartyCode", el("txtInvoicePartyCode").value);
    oCallback.add("InvoicePartyName", el("txtInvoicePartyName").value);
    oCallback.add("ContactPersonName", el("txtContactPersonName").value);
    oCallback.add("ContactJobTitle", el("txtContactJobTitle").value);
    oCallback.add("ContactAddress", el("txtContactAddress").value);
    oCallback.add("BillingAddress", el("txtBillingAddress").value);   
    oCallback.add("ZipCode", el("txtZipCode").value);
    oCallback.add("PhoneNo", el("txtPhoneNo").value);
    oCallback.add("FaxNo", el("txtFaxNo").value);
    oCallback.add("Remarks", el("txtRemarks").value);
    oCallback.add("BaseCurrency", el("lkpBaseCrrncy").SelectedValues[0]);
    //oCallback.add("ConvertedCurrency", el("lkpCnvrtToCrrncy").SelectedValues[0]);
    oCallback.add("ReportingEmailID", el("txtReportingEmailID").value);
    oCallback.add("InvoicingEmailID", el("txtInvoicingEmailID").value);
    oCallback.add("Active", el("chkACTV_BT").checked);

    //Finance Integration
    if (getConfigSetting('FinanceIntegration') == "True" || getConfigSetting('FinanceIntegration') == "true") {
        oCallback.add("FinanceIntegrationBit", true);
        oCallback.add("bv_LedgerId", el("txtLedgerId").value);
    }
    else {
        oCallback.add("FinanceIntegrationBit", false);
        oCallback.add("bv_LedgerId", el("txtLedgerId").value);
    }

    oCallback.add("wfData", el("WFDATA").value);
    oCallback.invoke("InvoiceParty.aspx", "UpdateInvoiceParty");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}


function validateInvoicePartyCode(oSrc, args) {
    var oCallback = new Callback();
    var valid;
    oCallback.add("Code", el("txtInvoicePartyCode").value);
    oCallback.invoke("InvoiceParty.aspx", "ValidateCode");
    if (oCallback.getCallbackStatus()) {
        valid = oCallback.getReturnValue("valid");
        if (valid == "true") {
            oSrc.errormessage = "";
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = valid;
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}


//This method get fired while changing new record
function setFocus() {
    var sMode = getPageMode();
    if (sMode == MODE_NEW) {
        setFocusToField("txtInvoicePartyCode");
    }
    else if (sMode == MODE_EDIT) {
        setFocusToField("txtInvoicePartyName");
    }
}

function OnBeforeCallBack(mode, param) {
    if (mode == "Update" || mode == "Insert" || mode == "Delete") {
        param.add("mode", getPageMode());
        //param.add("InvoicePartyID", getPageID());
    }
}

$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 768) {
            el("tabInvoiceParty").style.height = $(window.parent).height() - 380 + "px";            
        }
        else {
            el("tabInvoiceParty").style.height = $(window.parent).height() - 485 + "px";            
        }
    }

}
