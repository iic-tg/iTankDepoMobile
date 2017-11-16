var HasChanges = false;
var _RowValidationFails = false;
var HasNoteChanges = false;
var Description = "";



function initPage(sMode) {
    el("rboAnsii").checked = true;
    el("rboCodco").checked = false;
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    bindEDIDetail();
    reSizePane();
}

function submitPage() {
    if (checkRight()) {
   
      var GateIn = el("chkGateIn").checked;
    var RepairEstimate = el("chkRepairEstimate").checked;
    var RepairComplete = el("chkRepairComplete").checked; 
    var FileFormat;
    var GateOut = el("chkGateOut").checked;
   // if (getText(el('btnSubmit')) == "Genarate") {
        Page_ClientValidate();
        if (!Page_IsValid) {
            return false;
        }
        document.body.click();
        if ((GateIn == true || RepairEstimate == true || GateOut == true || RepairComplete == true)) {
            if (el("rboAnsii").checked) {
                 FileFormat = "ANSII";
            }
            else {
                 FileFormat = "CODECO";
            }
            var objCallback = new Callback();
            objCallback.add("GateIn", GateIn);
            objCallback.add("RepairEstimate", RepairEstimate);
            objCallback.add("GateOut", GateOut);
            objCallback.add("GateOut", GateOut);
            objCallback.add("RepairComplete", RepairComplete);
            objCallback.add("WfData", el("WFDATA").value);
            objCallback.add("CustomerID", el("lkpCustomer").SelectedValues[0])
            objCallback.add("Customer", el("lkpCustomer").SelectedValues[1]);
            objCallback.add("FileFormat", FileFormat);
            objCallback.invoke("EDI.aspx", "GenerateEDI");
            bindEDIDetail();
            Msg = objCallback.getReturnValue("Msg")
            showInfoMessage(Msg);
            objCallback = null;
            if (el('lkpCustomer').value) {
                        }
            
        }
        else {
            showErrorMessage("Select Atleast One Event.");
        }
}
}

function bindEDIDetail() {
    var objGrid = new ClientiFlexGrid("ifgEdi");
    objGrid.parameters.add("Customer", el('lkpCustomer').value);
    objGrid.parameters.add("GateIn", el("chkGateIn").checked);
    objGrid.parameters.add("GateOut", el("chkGateOut").checked);
    objGrid.parameters.add("RepairEstimate", el("chkRepairEstimate").checked);
    objGrid.parameters.add("RepairApproval", "false");
    objGrid.parameters.add("btnType", "");
    objGrid.DataBind();
    if (ifgEdi.Rows().Count == 0) {
           el("divEDI").style.display = "none";
    }
    else {
        el("divEDI").style.display = "block";
    }
}

function fnDownloadExcelFile() {
    var ifgEDIDetail = new ClientiFlexGrid("ifgEdi");
    var RowIndex = ifgEdi.CurrentRowIndex();
    var _col = ifgEdi.Rows(RowIndex).GetClientColumns();
    el('fmDownloadFile').src = "../EDI/EDI.aspx?Customer=" + _col[0] + "&Filename=" + _col[5] + "&CustomerID=" + _col[1];
//    var Msg = objCallback.getReturnValue("Msg")
//    showInfoMessage(Msg);
}

function openXmlDocument(invoiceFileName, InvcType) {
    var filename = "";
    var fileExtension = "xml";
    if (InvcType == "Received") {
        filename = invoiceFileName;
    }
    else {
        filename = invoiceFileName + "." + fileExtension;
    }

    el("hiddenLinkViewInvoice").href = "../download.ashx?FL_NM=" + filename + "&INVC_TYPE=" + InvcType;
    el("hiddenLinkViewInvoice").click();
}

function clearValues() {
    clearLookupValues("lkpCustomer");
      //   setFocusToField("lkpCustomer");
       el("chkGateIn").checked = false;
       el("chkRepairEstimate").checked = false;
     el("chkGateOut").checked = false;
   el("chkRepairComplete").checked = false;
    //    $('.btncorner').corner();
    //    resetValidators();
}

function showErrorfileMessage() {
 showErrorMessage("File not available. It is deleted or archived from the server.");
}

function fnEdimailSend(_rowIndex, _CustomerId, _ActivityName, _gateinTransactionNo, _EDINo, _LastSendDate,_customerCode) {
    showModalWindow("Send Email", "EDI/SendEdiEmail.aspx?CustomerID=" + _CustomerId + "&FileName=" + _gateinTransactionNo + "&EDINo=" + _EDINo +" &CustomerCD=" +_customerCode + "&MailMode=Send", "600px", "400px", "100px", "", ""); 
   // return true;
  //  psc().hideLoader()
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
    oCallback.add("EDINo", el("hdnEDI").value);
    oCallback.add("FromEmail", el("txtFrom").value);
    oCallback.add("ToEmail", el("txtTo").value);
    oCallback.add("BCCEmail", el("txtBCC").value);
    oCallback.add("Subject", el("txtSubject").value);
    //oCallback.add("EmailBody", el("txtBody").innerText);
    oCallback.add("EmailBody", getText(el('txtBody')));
    oCallback.add("MailMode", pdfs("wfFrame" + pdf("CurrentDesk")).el('hdnMailMode').value);
    oCallback.add("Attachment", el("hdnattachfile").value);
 //   oCallback.add("WFDATA", getCWfData(document.location.href));
    oCallback.invoke("SendEdiEmail.aspx", "SaveDetails");
    if (typeof (pdfs('wfFrame' + pdf('CurrentDesk')).bindEDIDetail()) != "undefined") {
        pdfs('wfFrame' + pdf('CurrentDesk')).bindEDIDetail();
    }
  //  ppsc().bindEDIDetail();
    if (oCallback.getCallbackStatus()) {
        ppsc().closeModalWindow();
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
     //   pdfs("wfFrame" + pdf("CurrentDesk")).getSearchDetails();
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

function openEDIEmailDetail(_ediEmailId) {
    showModalWindow("EDI Email Detail View", "EDI/EdiEmailDetail.aspx?EdiId=" + _ediEmailId, "550px", "400px", "100px", "", "");
}
function checkRight() {

    var bCreateRights = GetBoolFromString(getWFDataKey("CRT_BT"));
    var bEditRights = GetBoolFromString(getWFDataKey("EDT_BT"));
   // var sMode = getPageMode();
    var bAllowToSubmit = false;

    if (bCreateRights == true) {
        bAllowToSubmit = true;

    }
    else if (bEditRights == true) {
        bAllowToSubmit = true;
    }
    else {
        showWarningMessage("You do not have sufficient rights to perform the selected operation.");
    }
    return bAllowToSubmit;

}
//Window Resize
if (window.$) {
    $(document).ready(function () {

        reSizePane();
    });
}

try{
    $(window.parent).resize(function () {
        reSizePane();
    });
} catch (e) { }

function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 768) {
            if (chrome) {
                el("fldFormat").style.width = "80%";
            }
            el("tabEDI").style.height = $(window.parent).height() - 100 + "px";
            if (el("ifgEdi") != null) {
                el("ifgEdi").SetStaticHeaderHeight($(window.parent).height() - 370 + "px");
            }

        }
        else {
            el("tabEDI").style.height = $(window.parent).height() - 300 + "px";
            el("fldSearch").style.width = "730px";
            if (el("ifgEdi") != null) {
                el("ifgEdi").SetStaticHeaderHeight($(window.parent).height() - 377 + "px");
            }

        }
    }


}