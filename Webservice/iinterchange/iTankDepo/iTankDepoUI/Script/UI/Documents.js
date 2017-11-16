//Declarations
var sendMail = false;
var HasChanges = false;

//This method used to show email popup
function createEmail() {
    var popupURL = document.location.href
    var popupTitle = getQueryStringValue(popupURL, "title");
    popupTitle = popupTitle.replace("Report", "Email")
    var _url = popupURL.replace("&type=document", "&type=email") + '&ref=true';
    showModalWindow(popupTitle, _url, '600px', '400px', '80px', '80px');  
}

////This method used to send email
function sendEmail() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    var oCallback = new Callback();
    oCallback.add("EmailTo", el("txtTo").value)
    oCallback.add("EmailSubject", el("txtSubject").value)
    oCallback.add("EmailBody", el("txtBody").value)
    oCallback.add("AttachFile", el("lnkAttachment").innerText)
    oCallback.add("TransactionNo", el("hdnTransactionNo").value)
    oCallback.add("TemplateID", el("hdnTemplateId").value)
    oCallback.add("WFDATA", getCWfData(document.location.href));
    oCallback.invoke("ReportViewer.aspx", "CreateAlert");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage("Email Sent Successfully.");
        ppsc().gsStayMessage = true;
        ppsc().closeModalWindow();       
    }
    
    oCallback = null;
}

////This method used to send email
function sendCustomHiddenEmail(sWfData) {
    var oCallback = new Callback();
    oCallback.add("EmailTo", el("txtTo").value)
    oCallback.add("EmailSubject", el("txtSubject").value)
    oCallback.add("EmailBody", el("txtBody").value)
    oCallback.add("AttachFile", el("lnkAttachment").innerText)
    oCallback.add("TransactionNo", el("hdnTransactionNo").value)
    oCallback.add("TemplateID", el("hdnTemplateId").value)
    oCallback.add("WFDATA", sWfData)
    oCallback.invoke("ReportViewer.aspx", "CreateAlert");
    if (oCallback.getCallbackStatus()) {
        return true;
    }
    else {
        return false;
    }
    oCallback = null;
}

function getCWfData(wfdata) {
    wfdata = decodeURIComponent(wfdata);
    var q = new buildquerystring(wfdata);
//    q.additem("TO_ORGNZTN_ID", getQueryStringValue(wfdata, "CRRNT_ORGNZTN_ID"));
//    q.additem("TO_ORGNZTN_TYP_CD", getQueryStringValue(wfdata, "CRRNT_ORGNZTN_TYP_CD"));
//    q.additem("FRM_ORGNZTN_ID", getQueryStringValue(wfdata,"CRRNT_ORGNZTN_ID"));
//    q.additem("FRM_ORGNZTN_TYP_CD", getQueryStringValue(wfdata,"CRRNT_ORGNZTN_TYP_CD"));
    var cwfdata = q.getquerystring();
    return cwfdata;
}

////This method used to send email
function sendHiddenEmail() {
    var oCallback = new Callback();
    oCallback.add("EmailTo", el("txtTo").value)
    oCallback.add("EmailSubject", el("txtSubject").value)
    oCallback.add("EmailBody", el("txtBody").value)
    oCallback.add("AttachFile", el("lnkAttachment").innerText)
    oCallback.add("TransactionNo", el("hdnTransactionNo").value)
    oCallback.add("TemplateID", el("hdnTemplateId").value)
    oCallback.add("WFDATA", document.location.href)
    oCallback.invoke("ReportViewer.aspx", "CreateAlert");
    if (oCallback.getCallbackStatus()) {
        return true;
    }
    else {
        return false;
    }
    oCallback = null;
}

//This method used to close email popup
function cancelEmail() {
  closeModalWindow();
  hideMessage();          
}

//This method is called from the page for email/report/export functionality
function DocumentPrint() {
    var aParameters = new Array();
    var DocumentId = '';
    var Title = '';
    var Type = '';
    this.add = function (name, value) {
        value = value.toString();
        if (value != '')
            aParameters[name] = encodeURIComponent(value);
        return this;
    }

    this.openReportDialog = function () {

        var sWfdata = "";
        if (el("WFDATA"))
            el("WFDATA").value;
        var ReportPath = getQueryStringValue(sWfdata, "REPORTPATH");
        var sURL = ReportPath + "Documents/DocumentDialog.aspx?fm=1&";
        sURL += "dcmnt_id=" + this.DocumentId;
        sURL += "&type=" + this.Type;
        sURL += "&title=" + this.Title;
        sURL += '&wfdata=' + encodeURIComponent(sWfdata);

        for (var p in aParameters) {
            sURL += '&';
            sURL += p;
            sURL += '=';
            sURL += aParameters[p];
        }

        if (this.Type == "document") {
            showPopupWindow(this.Title, sURL + "&rel=0", "1000", "600", 50, 200, "");
        }
        if (this.Type == "report") {
            showPopupWindow(this.Title, sURL + "&rel=1", "950", "600", 50, 200, "");
        }
        else if (this.Type == "email") {
            sURL += '&direct=1';
            showModalWindow(this.Title, sURL, '700px', '400px', '', '');
        }
        else if (this.Type == "noframe") {
            sURL += '&direct=1';
            showHiddenWindow(this.Title, sURL, '700px', '400px', '210px', '200px');
        }
        else if (this.Type == "export") {
            sURL += '&direct=1';
            showModalWindow(this.Title, sURL, '400px', '150px', '', '');          
        }
        else if (this.Type == "CreatePdf") {
            sURL += '&direct=1';
            showHiddenWindow(this.Title, sURL, '700px', '400px', '210px', '200px');
        }
        else if (this.Type == "word") { //Added for export in word document
            sURL += '&direct=1';         
            showHiddenWindow(this.Title, sURL, '400px', '150px', '', '');
        }
        else if (this.Type == "All") { // Print All Document Type
            sURL += '&direct=1';            
            showHiddenWindow(this.Title, sURL, '400px', '150px', '', '');
        }
    }
}

//This method used to change the template
function changeTemplate(objListBox, _docname, _othparams) {
    var d = new Date();
    var sURL = "DocumentViewer.aspx?tmplt_id=" + objListBox.value + "&dcmnt_nam=" + objListBox.options[objListBox.selectedIndex].text + "&ts=" + d.getTime();
    el("fmReport").src = sURL + _othparams;
}