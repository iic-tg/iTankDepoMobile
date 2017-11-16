//Java Script

var sendMail = false;
//Declarations

var HasChanges = false;

function initPage() {
    if (chrome || Firefox) {
        showDiv("divPrint");
    }
    else {
        hideDiv("divPrint");
    }
}

//This method used to show email popup
function createEmail() {
    var popupURL = document.location.href
    var popupTitle = getQueryStringValue(popupURL, "title");
    popupTitle = popupTitle.replace("Report", "Email")
    var _url = popupURL + "&type=email&ref=true";
    showModalWindow(popupTitle, _url, '600px', '600px', '80px', '80px');
}

function PrintDoc() {
    var popupURL = document.location.href
    var popupTitle = getQueryStringValue(popupURL, "title");
    popupTitle = popupTitle.replace("Report", "Print")
    var _url = popupURL + "&type=print&ref=true";
    showModalWindow(popupTitle, _url, '600px', '600px', '180px', '180px');
         
    
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
    oCallback.add("EmailBody", el("txtBody").innerHTML)
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
function dEmail() {

    if (el("ReportViewerFrame").src.indexOf('dMode=email') == -1 && el("ReportViewerFrame").contentDocument.readyState == 'complete' || (el("ReportViewerFrame").src.indexOf('dMode=cancel') == -1)) {
        el("ReportViewerFrame").src = el("ReportViewerFrame").src + '&dMode=email';
        fnLoadReportContent();
    }
    el("divEmail").style.display = "none";
    //	el("divPrint").style.display = "none";
}

function dEmailCancel() {
    if (pel("ReportViewerFrame").src.indexOf('dMode=email') != -1) {
        pel("ReportViewerFrame").src = pel("ReportViewerFrame").src.replace("&dMode=email", "");

        pel("lblMsg").style.display = "none";
        pel("divEmail").style.display = "block";
        //pel("divPrint").style.display = "block";
        hideMessage();
    }
}

function fnLoadReportContent() {
    if (document.attachEvent)
        el('ReportViewerFrame').attachEvent("onreadystatechange", CheckReportLoader);
    else
        el('ReportViewerFrame').onload = CheckReportLoader;
    //    if(el("ReportViewerFrame").src.indexOf('dMode=email') !=-1)
    //        el('pnlEmail').attachEvent("onreadystatechange", CheckSendMailLoader);
}

function CheckSendMailLoader() {
    if (el('pnlEmail').readyState == "complete") {
        el("pndLoader").style.display = "none";
    }

    if (el('pnlEmail').readyState == "loading") {
        el("pndLoader").style.display = "block";
    }
}

function CheckReportLoader() {
    if (el('ReportViewerFrame').readyState == "complete") {
        el("pndLoader").style.display = "none";
        hideLayer();
    }

    if (el('ReportViewerFrame').readyState == "loading") {
        el("pndLoader").style.display = "block";
        showLayer("Loading Mail...");
    }
}

function ClearDocumentLoading() {
    el("pndLoader").style.display = "none";
}

function ShowDocumentLoading() {
    el("pndLoader").style.display = "block";
}

function hideLayer() {
    var shadow;
    var question;
    if (el("shadow") == null) shadow = pel("shadow");
    else shadow = el("shadow");
    if (el("question") == null) question = pel("question");
    else question = el("question");
    shadow.style.display = "none";
    question.style.display = "none";
    shadow = null;
    question = null;
    if (document.detachEvent) {
        el("imgloading").detachEvent("onkeypress", disableKey);
        el("imgloading").detachEvent("onkeydown", disableKey);
    }
    else {
        el("imgloading").onkeypress = null;
        el("imgloading").onkeydown = null;
        el("imgloading").onkeyup = null;
    }
}

function ShowErrMessage(_msg) {
    var messageDetails = new Object();
    messageDetails.message = _msg;
    messageDetails.title = "Error";

    if (window.showModalDialog) {
        window.showModalDialog("../Error.html", messageDetails, "dialogWidth:466px;dialogHeight:200px;status:no;help: No;");
    }
    else {
        window.alert(_msg);
    }
}
function mToolOver(_id) {
    if (el(_id).className == 'Reportsitem')
        el(_id).className = 'ReportsOitem';
}

function mToolOut(_id) {
    if (el(_id).className == 'ReportsOitem')
        el(_id).className = 'Reportsitem';
}

function dPrint() {
    if (fm("ReportViewerFrame").contentDocument.readyState == 'complete')
        fm("ReportViewerFrame").document.Script.Print();
}

function HideEDI() {
    //el('imgbtnEDI').style.display = "none";
}
function HidePrint() {
    //	el('imgbtnPrint').style.display = "none";
}


function showReportLoading(title) {
    if (el('ReportViewerFrame') != null) {
        showLayer(title);
        if (document.attachEvent)
            el('ReportViewerFrame').attachEvent("onreadystatechange", fnCheckReportLoading);
        else
            el('ReportViewerFrame').onload = fnCheckReportLoading;
    }
}

function fnCheckReportLoading() {
    if (el('ReportViewerFrame').readyState == 'complete') {
        hideLayer();
    }
    else if (el('ReportViewerFrame').readyState == "loading") {
        showLayer("Loading..");
    }
}

function showDefaultReportLoading() {
    document.attachEvent("onreadystatechange", fnCheckReportLoading);
}

function fnCheckDefaultReportLoading() {
    if (document.readyState == 'complete') {
        hideLayer();
    }
    else {
        showLayer("Loading..");
    }
}

function fnShowInfoMessageBox(_msg) {
    var messageDetails = new Object();
    messageDetails.message = _msg;
    messageDetails.title = "Information";
    var ScreenX = window.screenLeft + 296;
    var ScreenY = window.screenTop + 249;

    if (window.showModalDialog) {
        window.showModalDialog("../Message.html", messageDetails, "dialogWidth:366px;dialogHeight:150px;status:no;help: No;dialogLeft:" + ScreenX + ";dialogTop:" + ScreenY + ";");
    }
    else {
        window.alert(_msg);
    }
}


function showLayer(title) {
    setLayerPosition();
    setModalWindowTitle(title);
    var shadow;
    var question;
    if (el("shadow") == null) shadow = pel("shadow");
    else shadow = el("shadow");

    if (el("question") == null) question = pel("question");
    else question = el("question");

    shadow.style.display = "block";
    question.style.display = "block";
    el("imgloading").focus();
    if (document.attachEvent) {
        el("imgloading").attachEvent("onkeypress", disableKey);
        el("imgloading").attachEvent("onkeydown", disableKey);
    }
    else {
        el("imgloading").onkeypress = disableKey;
        el("imgloading").onkeydown = disableKey;
        el("imgloading").onkeyup = disableKey;
    }
    el("imgloading").onclick = null;
    el("imgloading").onclick = new Function("return false;");
    shadow = null;
    question = null;
}

function setModalWindowTitle(title) {
    if (typeof (title) == "undefined") title = "Loading..";
    el("tdmodalwindowtitle").innerHTML = title;
}

function setLayerPosition() {
    var shadow;
    var question;

    if (el("shadow") == null) shadow = pel("shadow");
    else shadow = el("shadow");

    if (el("question") == null) question = pel("question");
    else question = el("question");


    var bws = getBrowserHeight();
    shadow.style.width = bws.width + "px";
    shadow.style.height = bws.height + "px";

    question.style.left = parseInt((bws.width - 130) / 2);
    question.style.top = parseInt((bws.height - 150) / 2);

    shadow = null;
    question = null;
}



function getBrowserHeight() {
    var intH = 0;
    var intW = 0;

    if (typeof window.innerWidth == 'number') {
        intH = window.innerHeight;
        intW = window.innerWidth;
    }
    else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        intH = document.documentElement.clientHeight;
        intW = document.documentElement.clientWidth;
    }
    else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        intH = document.body.clientHeight;
        intW = document.body.clientWidth;
    }

    return {
        width: parseInt(intW),
        height: parseInt(intH)
    };
}

function disableKey() {
    if (event.keyCode != 13 && event.keyCode != 32) {
        event.returnValue = false;
    }
    if (event.keyCode == 27) {
        HideLayer();
    }
}

function setrvheight() {
    var viewer = el("pnlDocView");
    var reportDiv;
    var i = 0;
    for (var i = 0; i < document.all.length - 1; i++) {
        // Get an individual node
        if (document.all[i].id.indexOf("oReportDiv") > 0) {
            reportDiv = document.all[i];
            break;
        }
    }
    if (reportDiv != '') {
        reportDiv.style.height = document.body.offsetHeight - 40;
    }
}
function cancelEmail() {
    closeModalWindow();
    hideMessage();

}