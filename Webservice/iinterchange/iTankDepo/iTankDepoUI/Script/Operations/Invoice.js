var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgInvoice');
var PgHandlingStorageInvoice = "Handling/Storage Invoice";
var PgRepairInvoice = "Repair Invoice";
var resMsg = "";
var formatType = "";
var exportType = "";

//Initialize page while loading the page based on page mode

function initPage(sMode) {   
  
    hideDiv("btnSubmit");
    $("#spnHeader").text(getPageTitle());
    hideDiv("lnkReset");
    if (getPageTitle() == PgHandlingStorageInvoice) {
        bindInvoice("HS");
    }
    else if (getPageTitle() == PgRepairInvoice) {
        bindInvoice("RP");
    }
    resetValidatorByGroup("tabInvoice");
    setFocusToField("lkpCustomer");
    setPageMode("edit");
    clearTextValues("lkpCustomer");
    clearTextValues("txtCustCurrency");
    clearTextValues("txtConvToCurrency");
    clearTextValues("txtExchangeRate");
    clearTextValues("datFromDate");
    clearTextValues("datToDate");
    setReadOnly('datToDate', true);
    $('.btncorner').corner();
}


//This function is used to fetch the data from Database for the corresponding Customer ID.
function onFetchClick() {
    Page_ClientValidate();
    if (!Page_IsValid)
        return false; 
    {
    }
    var oCallback = new Callback();
    oCallback.add("Customer_Id", el("lkpCustomer").SelectedValues[0]);

    if (getPageTitle() == PgHandlingStorageInvoice) {
        oCallback.add("Invc_Type", "HS");
    }
    else if (getPageTitle() == PgRepairInvoice) {
        oCallback.add("Invc_Type", "RP");
    }
    oCallback.invoke("Invoice.aspx", "GetInvoiceDetails");
    if (oCallback.getCallbackStatus()) {
      //  hideDiv("lblExportLink");

        if (oCallback.getReturnValue("Message") != '' && oCallback.getReturnValue("Message") != null) {//added and changed for UIG Fix
            showErrorMessage(oCallback.getReturnValue("Message"));
        }
        else {
            setReadOnly('lkpCustomer', true);
            el('txtCustCurrency').value = oCallback.getReturnValue("CustCurrency");
            el('txtConvToCurrency').value = oCallback.getReturnValue("ConvToCurrency");
            var Amount = new Number;
            Amount = parseFloat(oCallback.getReturnValue("ExchangeRate"));
            el("txtExchangeRate").value = Amount.toFixed(2);
            //el('txtExchangeRate').value = oCallback.getReturnValue("ExchangeRate");
            el('datFromDate').value = oCallback.getReturnValue("FromDate");
            var dt = oCallback.getReturnValue("ToDate");         
            if (oCallback.getReturnValue("ToDate") == '') {             
            setReadOnly('datToDate', false); }
            else {
                setReadOnly('datToDate', true);
                el('datToDate').value = oCallback.getReturnValue("ToDate");
            }
            if (oCallback.getReturnValue("btnType") != "billed") {              
                setText(el('btnSubmit'), oCallback.getReturnValue("btnType"));              
                showDiv("btnSubmit");
            }
            else {
                hideDiv("btnSubmit");
            }

            //setPageID(oCallback.getReturnValue("PageID"));
            //showDiv("btnSubmit");        
            showDiv("lnkReset");
            hideDiv("lnkFetch");
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

//This function is used to Reset the data.
function onResetClick() {
    setReadOnly('lkpCustomer', false);
  //  hideDiv("lblExportLink");
    setFocusToField("lkpCustomer");
    clearTextValues("lkpCustomer");
    clearTextValues("txtCustCurrency");
    clearTextValues("txtConvToCurrency");
    clearTextValues("txtExchangeRate");
    clearTextValues("datFromDate");
    clearTextValues("datToDate");
    setPageID("");
    el('hdnInvcNo').value = '';
    //resetValidators(true);    
    //hideDiv("divDetail");
    showDiv("lnkFetch");
    hideDiv("lnkReset");
    hideDiv("btnSubmit");
    setReadOnly('datToDate', true);
   // clearTextValues('lkpINVFormat');
    hideMessage();
}

//This function is used to bind the Invoice Grid.
function bindInvoice(Invc_Type) {
    var objGrid = new ClientiFlexGrid("ifgInvoice");
    objGrid.parameters.add("Invc_Type", Invc_Type);
    objGrid.DataBind();
}

//This method used to submit the changes to database
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    //To submit the changes to server
    if (ifgInvoice.Submit(true) == false) {
        return false;
    }
    else {
        if (getPageChanges() || getText(el('btnSubmit')) == "Verified") {
            CreateInvoice();            
        }
        else {
            showInfoMessage('No Changes to Save');
        }

        return true;
    }
}

function CreateInvoice() {
    resMsg = "";
    var pageTitle = getPageTitle();
    var InvoiceFileType = "";
    var PageType = "";
    var oCallback = new Callback();
    oCallback.add("Customer_Id", el("lkpCustomer").SelectedValues[0]);
    oCallback.add("datFromDate", el("datFromDate").value);
    oCallback.add("datToDate", el("datToDate").value);
    oCallback.add("wfData", el("WFDATA").value);
    oCallback.add("InvcBin", getPageID());
    oCallback.add("InvcNo", el("hdnInvcNo").value);
    oCallback.add("btnType", getText(el('btnSubmit')));
    if (pageTitle == PgHandlingStorageInvoice) {
        oCallback.add("Invc_Type", "HS");
        PageType = "HS";
    }
    else if (pageTitle == PgRepairInvoice) {
        oCallback.add("Invc_Type", "RP");
        PageType = "RP";
    }

    oCallback.invoke("Invoice.aspx", "CreateInvoice");

    if (oCallback.getCallbackStatus()) {
        var ChargeMsg = "";
        if (oCallback.getReturnValue("ChargeMsg") != '' && oCallback.getReturnValue("ChargeMsg") != null) {//added and changed for UIG Fix
            ChargeMsg = oCallback.getReturnValue("ChargeMsg") + "  ";
        }
        if (oCallback.getReturnValue("Message") != '' && oCallback.getReturnValue("Message") != null) {//added and changed for UIG Fix
            showErrorMessage(oCallback.getReturnValue("Message"));
            //hideDiv("btnSubmit");
        }
        else {
            if (getText(el('btnSubmit')) != "Verified") {
                setText(el('btnSubmit'), "Verified");
                InvoiceFileType = "Draft";
            }
            else if (getText(el('btnSubmit')) == "Verified") {
                hideDiv("btnSubmit");
                InvoiceFileType = "";
            }

            setPageID(oCallback.getReturnValue("ID"));
            el('hdnInvcNo').value = (oCallback.getReturnValue("Invc_No"));
            HasChanges = false;
            if (pageTitle == PgHandlingStorageInvoice) {
                bindInvoice(PageType);
                InvoiceFileType = "HS_" + InvoiceFileType;
                CreateInvoiceFile(InvoiceFileType, oCallback.getReturnValue("Invc_No"));
            }
            else if (pageTitle == PgRepairInvoice) {
                bindInvoice(PageType);
                InvoiceFileType = "RP_" + InvoiceFileType;
                CreateInvoiceFile(InvoiceFileType, oCallback.getReturnValue("Invc_No"));
            }
            if (InvoiceFileType == "Draft")
                showInfoMessage(ChargeMsg +"Draft of Invoice " + oCallback.getReturnValue("Invc_No") + " Generated Sucessfully.");
            else
                showInfoMessage(ChargeMsg + "Invoice " + oCallback.getReturnValue("Invc_No") + " Generated Sucessfully.");
            resetHasChanges("ifgInvoice");
            setReadOnly("lkpCustomer", true);
            setFocusToField("lkpCustomer");
            var filename = "";
            var InvcFileType = "";
            if (InvoiceFileType == "HS_Draft" || InvoiceFileType == "RP_Draft") {
                //  filename = InvoiceFileType + "_" + oCallback.getReturnValue("Invc_No") + ".pdf";
                filename = InvoiceFileType + "_" + oCallback.getReturnValue("Invc_No") + "." +  exportType;
                InvcFileType = "Draft";
            }
            else if (InvoiceFileType == "HS_" || InvoiceFileType == "RP_") {
                filename = InvoiceFileType + oCallback.getReturnValue("Invc_No") + "."+ exportType;
                InvcFileType = "Final";
            }
            //el("lnkExport").href = "../download.ashx?FL_NM=" + filename + "&INVC_TYPE=" + InvcFileType;            
        }      
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function initHiddenFrame() {    
    setTimeout(ShowLink, 500);
}

function ShowLink() {
   // showDiv("lblExportLink");
}

//This method is used to Create Invoice File
function CreateInvoiceFile(InvcType, InvcNo) {  
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "InvoiceData";
    oDocPrint.Title = "Invoice";

//    formatType = el('lkpINVFormat').value;
//    if (formatType != "") {
//        formatType = formatType.split("."); //Split the value
//        formatType = formatType[1]; //taken only extension for checking
//        exportType = formatType.replace(")", "");
//    }
//    if (exportType == "pdf") {
//        oDocPrint.Type = "CreatePdf"; //use Createpddf as type
//    }
//    else if (exportType == "xls") {
//        oDocPrint.Type = "export"; //use Export as type
//    }
//    else if (exportType == "doc") {
//        oDocPrint.Type = "word"; //use Export as type
//    }
//    else {
//        oDocPrint.Type = "CreatePdf"; //use Createpddf as type
    //    }
    oDocPrint.Type = "All";
    var HSInvoiceDraft = "HSInvoiceDraft.rdlc"
    var HSInvoiceFinal = "HSInvoice.rdlc"
    if (getConfigSetting('014') != "") {
        HSInvoiceFinal = getConfigSetting('014')
    }

    if (getConfigSetting('013') != "") {
        HSInvoiceDraft = getConfigSetting('013')
    }

    if (InvcType == "HS_Draft") {
        oDocPrint.DocumentId = "1";
        oDocPrint.ReportPath = "../Documents/Report/" + HSInvoiceDraft;
    }
    else if (InvcType == "HS_") {
        oDocPrint.DocumentId = "1";
        oDocPrint.ReportPath = "../Documents/Report/" + HSInvoiceFinal;
    }
    else if (InvcType == "RP_Draft") {
        oDocPrint.DocumentId = "2";
        oDocPrint.ReportPath = "../Documents/Report/RPInvoiceDraft.rdlc";
    }
    else if (InvcType == "RP_") {
        oDocPrint.DocumentId = "2";
        oDocPrint.ReportPath = "../Documents/Report/RPInvoice.rdlc";
    }
    oDocPrint.add("INVC_TYP", InvcType);
    oDocPrint.add("INVC_NO", InvcNo);
    oDocPrint.openReportDialog();
}


//This method is used to view the Invoice
function viewInvoice() {   
    var rIndex = ifgInvoice.rowIndex;
    var cols = ifgInvoice.Rows(rIndex).GetClientColumns();
    var srccols = ifgInvoice.Rows(rIndex).Columns();
    var filename = "";
    var InvcType = srccols[16];
    if (exportType == "") {
        exportType = "pdf";
    }
    if (InvcType == "Draft") {
        filename = srccols[4] + "_Draft_" + srccols[1] + "." + exportType;
    }
    else if (InvcType == "Final") {
        filename = srccols[4] + "_" + srccols[1] + "." + exportType;
    }
    el("hiddenLinkViewInvoice").href = "../download.ashx?FL_NM=" + filename + "&INVC_TYPE=" + InvcType;
    el("hiddenLinkViewInvoice").click();
    return false;
}

//This event is fired after the invoice grid bind
function onAfterCallBack(param) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        el("divDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}

function valdiateToDate(oSrc, args) {
    var Todate = args.Value;    
    currentdate = el('hdnSysDate').value;
    var validateTodate = DateCompare(Todate, currentdate);
    if (validateTodate) {
        oSrc.errormessage = "To Date must not be greater than Current Date.";
        args.IsValid = false;
    }
}

function openInvoice(InvType) {
    var exportType = "";
    var rIndex = ifgInvoice.rowIndex;
    var cols = ifgInvoice.Rows(rIndex).GetClientColumns();
    var srccols = ifgInvoice.Rows(rIndex).Columns();
    var filename = "";
    var InvcType = srccols[16];
    if (InvType == "PDF") {
        exportType = "pdf";
    }
    else if (InvType == "XLS") {
        exportType = "xls";
    }
    else if (InvType == "DOC") {
        exportType = "doc";
    }
    if (InvcType == "Draft") {
        filename = srccols[4] + "_Draft_" + srccols[1] + "." + exportType;
    }
    else if (InvcType == "Final") {
        filename = srccols[4] + "_" + srccols[1] + "." + exportType;
    }
    el("hiddenLinkViewInvoice").href = "../download.ashx?FL_NM=" + filename + "&INVC_TYPE=" + InvcType;
    el("hiddenLinkViewInvoice").click();
    return false;
}