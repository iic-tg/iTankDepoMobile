var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgViewInvoice');
var _RowValidationFails = false;
var strInvoiceBin = "";
var strRemarks = "";
var strInvoiceId = "";
var strInvoiceNo = "";
var intDepotID = "";

function initPage(sMode) {
    strRemarks = "";
    bindGrid();
    reSizePane();
    var rowCount = ifgViewInvoice.Rows().Count;
    if (rowCount > 0) {
        el("divViewInvoice").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
    else {
        el("divViewInvoice").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    el("lnkHelp").style.display = "none";
}

function bindGrid() {
    var objGrid = new ClientiFlexGrid("ifgViewInvoice");
    objGrid.DataBind();
}

function fnSaveFinalInvoice() {
    var oCallback = new Callback();
    oCallback.add("InvoiceBin", strInvoiceBin);
    oCallback.add("DepotID", intDepotID);
    oCallback.invoke("ViewInvoice.aspx", "FinalInvoice");
    if (oCallback.getCallbackStatus()) {
        strInvoiceBin = "";
        printInvoice(oCallback.getReturnValue("DraftInvoiceNo"), oCallback.getReturnValue("InvoiceFileName"), oCallback.getReturnValue("InvoiceTypeID"));
        showInfoMessage("Draft Invoice " + oCallback.getReturnValue("DraftInvoiceNo") + " has been finalized with Finalized Invoice No " + oCallback.getReturnValue("FinalInvoiceNo") + " and can be viewed by clicking the corresponding icons.");
        bindGrid();
    }
    else {
        if (oCallback.getReturnValue("NoRecords") == "True") {
            showErrorMessage("There are no Equipment(s) available to Finalize the Invoice. Equipment(s) might have been deleted or Customer has been changed after Draft Generation. Please regenerate Draft to Finalize the Invoice.");
        }

        if (oCallback.getReturnValue("Error_Msg") != "") {
            if (oCallback.getReturnValue("Error_Msg") != null) {
                showErrorMessage(oCallback.getReturnValue("Error_Msg"));
            }
        }

        return false;
    }
    oCallback = null;
    return true;
}

function confirmFinalizingInvoice(invoiceBin,DepotID) {
    showConfirmMessage("Are you sure you want to finalize this Draft. Click 'Yes' to finalize the Draft or 'No' to Ignore the finalize Generation.",
                                "wfs().yesClick();", "wfs().noClick();");
    strInvoiceBin = invoiceBin;
    intDepotID = DepotID;
    return true;
}

function yesClick() {
    fnSaveFinalInvoice();
    return true;
}

function noClick() {
    return false;
}

function downloadInvoice(exportType, InvcType, invoiceFileName) {
    var filename = "";
    var fileExtension;
    if (exportType == "PDF") {
        fileExtension = "pdf";
    }
    else if (exportType == "EXCEL") {
        fileExtension = "xls";
    }
    else if (exportType == "WORD") {
        fileExtension = "doc";
    }
    if (InvcType == "DRAFT") {
        filename = invoiceFileName + "." + fileExtension;
    }
    else if (InvcType == "FINAL") {
        filename = invoiceFileName + "." + fileExtension;
    }
    el("hiddenLinkViewInvoice").href = "../download.ashx?FL_NM=" + filename + "&INVC_TYPE=" + InvcType;
    el("hiddenLinkViewInvoice").click();
}

function printInvoice(strInvoiceNo, strInvoiceFileName, strInvoiceTypeID) {
    var oDocPrint = new DocumentPrint();
    if (strInvoiceTypeID == "78") {//Handling and Storage
        oDocPrint.KeyName = "InvoiceGeneration";
        oDocPrint.Title = "Invoice Generation";

        if (getConfigSetting('067') == "True") {
            oDocPrint.DocumentId = "24";
            oDocPrint.add("CUSTOMER_TYPE", "AGENT");
            oDocPrint.add("TEMPLATEID", "48");
            oDocPrint.ReportPath = "../Documents/Report/HSReportGWS.rdlc";
        }
        else {
            oDocPrint.DocumentId = "24";
            oDocPrint.ReportPath = "../Documents/Report/HSReport.rdlc";
        }

    }
    else if (strInvoiceTypeID == "79") {//Heating 
        oDocPrint.KeyName = "HeatingInvoice";
        oDocPrint.Title = "Heating Invoice";
        oDocPrint.DocumentId = "29";
        oDocPrint.ReportPath = "../Documents/Report/HeatingInvoice.rdlc";
    }
    else if (strInvoiceTypeID == "80") {//Cleaning
        oDocPrint.KeyName = "CleaningInvoice";
        oDocPrint.Title = "Cleaning Invoice";
        oDocPrint.DocumentId = "27";
        oDocPrint.ReportPath = "../Documents/Report/CleaningInvoice.rdlc";
    }
    else if (strInvoiceTypeID == "81") {//Repair
        oDocPrint.KeyName = "RepairInvoice";
        oDocPrint.Title = "Repair Invoice";

        if (getConfigSetting('067') == "True") {
            oDocPrint.DocumentId = "26";
            oDocPrint.add("CUSTOMER_TYPE", "AGENT");
            oDocPrint.add("TEMPLATEID", "50");
            oDocPrint.ReportPath = "../Documents/Report/RepairInvoiceGWS.rdlc";
        }
        else {
            oDocPrint.DocumentId = "26";
            oDocPrint.ReportPath = "../Documents/Report/RepairInvoice.rdlc";
        }

        //        oDocPrint.DocumentId = "26";
        //        oDocPrint.ReportPath = "../Documents/Report/RepairInvoice.rdlc";
    }
    else if (strInvoiceTypeID == "82") {//Miscellaneous
        oDocPrint.KeyName = "MiscellaneousInvoice";
        oDocPrint.Title = "Miscellaneous Invoice";
        oDocPrint.DocumentId = "28";
        oDocPrint.ReportPath = "../Documents/Report/MiscellaneousInvoice.rdlc";
    }
    else if (strInvoiceTypeID == "140") {//Credit Note
        oDocPrint.KeyName = "CreditNote";
        oDocPrint.Title = "Credit Note";
        oDocPrint.DocumentId = "40";
        oDocPrint.ReportPath = "../Documents/Report/CreditNote.rdlc";
    }
    else if (strInvoiceTypeID == "83") {//Transportation
        oDocPrint.KeyName = "TransportationInvoice";
        oDocPrint.Title = "Transportation Invoice";
        oDocPrint.DocumentId = "34";
        oDocPrint.ReportPath = "../Documents/Report/TransportationInvoice.rdlc";
    }
    else if (strInvoiceTypeID == "84") {//Rental
        oDocPrint.KeyName = "RentalInvoice";
        oDocPrint.Title = "Rental Invoice";
        oDocPrint.DocumentId = "35";
        oDocPrint.ReportPath = "../Documents/Report/RentalInvoice.rdlc";
    }
    else if (strInvoiceTypeID == "151") {//Inspection Invoice
        oDocPrint.KeyName = "InspectionInvoice";
        oDocPrint.Title = "Inspection Invoice";
        oDocPrint.DocumentId = "44";
        oDocPrint.ReportPath = "../Documents/Report/InspectionInvoiceGWC.rdlc";
    }
    oDocPrint.Type = "All";
    oDocPrint.add("INVC_NO", strInvoiceFileName);
    oDocPrint.add("BILLING_TYPE", "FINAL");
    oDocPrint.add("PageName", "ViewInvoice");
    oDocPrint.openReportDialog();
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
            if (el("ifgViewInvoice") != null) {
                el("ifgViewInvoice").SetStaticHeaderHeight($(window.parent).height() - 264 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            // el("tabViewInvoice").style.height = $(window.parent).height() - 250 + "px";
            if (el("ifgViewInvoice") != null) {
                el("ifgViewInvoice").SetStaticHeaderHeight($(window.parent).height() - 350 + "px");
            }

        }
        else {
            //  el("tabViewInvoice").style.height = $(window.parent).height() - 260 + "px";
            if (el("ifgViewInvoice") != null) {
                el("ifgViewInvoice").SetStaticHeaderHeight($(window.parent).height() - 273 + "px");
                
            }

        }
    }

}



//Invoice Cancel

function confirmCancelInvoice(InvoiceId, InvoiceNo, DepotID) {

    strInvoiceId = InvoiceId;
    strInvoiceNo = InvoiceNo;
    intDepotID = DepotID;
    showConfirmMessage("Are you sure you want to Cancel this Invoice. Click 'Yes' to Cancel or 'No' to ignore",
                                "wfs().yesClickCancel('" + InvoiceNo + "');", "wfs().noClickCancel();");

    return false;
}

//Store Remarks
function saveInvoiceRemarks() {

    if (el("txtRemarks").value == "") {
        showErrorMessage("Remarks Required.");
        return false;
    }
    strRemarks = el("txtRemarks").value;
    ppsc().closeModalWindow();

    //Invoice Cancel Operations

    var oCallback = new Callback();
    oCallback.add("InvoiceBin", el("hdnInvoiceId").value);
    oCallback.add("InvoiceNo", el("hdnInvoiceNo").value);
    oCallback.add("Remarks", el("txtRemarks").value);
    oCallback.add("DepotID", el("hdnDepotID").value);
    oCallback.invoke("ViewInvoice.aspx", "InvoiceCancel");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(" InvoiceNo : " + el("hdnInvoiceNo").value + " Cancelled Successfully.");
        //         bindGrid();
        pdfs("wfFrame" + pdf("CurrentDesk")).bindGrid();
    }
    else {
        showErrorMessage(oCallback.getReturnValue("Error_Msg"));
    }

    return false;
}


//Pop up

function yesClickCancel(InvoiceNo) {


    showModalWindow("Invoice Cancel - " + InvoiceNo, "Billing/InvoiceRemarksEntry.aspx?InvoiceId=" + strInvoiceId + "&InvoiceNo=" + strInvoiceNo + "&DepotID=" + intDepotID, "450px", "220px", "", "", "");

    return true;
}

function noClickCancel() {
    return false;
}
