//This method is used the submit the form for catching file upload values
function uploadedSubmit() {
    document.forms['frmUpload'].submit();
    el("divifgGrid").style.display = "none";
    el("divlnk").style.display = "none";
}

//This method is called when Submit
function submitUpload() {
    el("divRecordNotFound").style.display = "none";
    if (el("hdnFileName").value != '') {
        var tablename = getQStr("tablename");
        if (tablename.toUpperCase() == "TARIFF" || tablename.toUpperCase() == "TARIFF_CODE_DETAIL") {
            showConfirmMessage("Do you really want to upload this File? This will Delete all the existing data from Database and Insert Data from the File.",
                        "mfs().yesClickTariff();", "", "");
        }
        else {
            hideDiv("btnSubmit");
            showLayer("Loading..");
            setTimeout(insertUploadTable, 0);
        }
    }
    else {
        showErrorMessage('Please Upload a file');
        el("divifgGrid").style.display = "none";
        el("divlnk").style.display = "none";
    }
}

function yesClickTariff() {
    hideDiv("btnSubmit");
    showLayer("Loading..");
    setTimeout(insertUploadTable, 0);
}

//This method is used to call insert method for  File Upload
function insertUploadTable() {
    var SchemaID = getQStr("SchemaID");
    var oCallback = new Callback();
    oCallback.add("SchemaID", SchemaID);
    oCallback.add("DepotId", el("hdnDpt_ID").value);
    oCallback.add("filename", el("hdnFileName").value);
    oCallback.add("USERNAME", getQStr("USERNAME"));
    oCallback.add("TableName", getQStr("tablename"));
    oCallback.add("Size", getQStr("Size"));
    oCallback.add("Type", getQStr("Type"));
    oCallback.add("Cstmr_id", getQStr("Cstmr_id"));
    oCallback.add("TariffId", getQStr("TariffId"));

    oCallback.invoke("Upload.aspx", "InsertFileUpload");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue('Message') != '' && oCallback.getReturnValue('Message') != null) {
            showInfoMessage(oCallback.getReturnValue('Message'));
            if (typeof (pdfs('wfFrame' + pdf('CurrentDesk')).bindGrid()) != "undefined") {
                pdfs('wfFrame' + pdf('CurrentDesk')).bindGrid();
            }
        }
        else {
            showErrorMessage(oCallback.getReturnValue('Error'));
        }

        if (oCallback.getReturnValue('Upload') == 'yes') {
            bindGrid(oCallback.getReturnValue('IdCol'), "Valid", "No");
            el("hdnIdColumn").value = oCallback.getReturnValue('IdCol');
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    //UIG fix
    if (oCallback.getReturnValue('Error') == 'No')
        pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = true;
    oCallback = null;
    el("hdnFileName").value = ''
    hideLayer();
    showDiv("btnSubmit");
   
}

//This Method is used to bind the upload status grid
function bindGrid(IdCol, DisplayType, SessionChk) {
    el("divifgGrid").style.display = "block";
    var tablename = getQStr("tablename");
    var ifgUploadStatus = new ClientiFlexGrid("ifgUploadStatus");
    if (tablename == "TARIFF") {
        el("divlnk").style.display = "block";
        ifgUploadStatus.parameters.add("DisplayType", DisplayType);
        ifgUploadStatus.parameters.add("SessionChk", SessionChk);
    }
    else {
        el("divlnk").style.display = "none";
    }
    ifgUploadStatus.parameters.add("TableName", el("hdnUploadTblName").value);
    ifgUploadStatus.parameters.add("IdColumn", IdCol);
    ifgUploadStatus.DataBind();
}
function closeUpload() {
   // ifgUploadStatus.hasChanges;
    ppsc().closeModalWindow();
//    pdfs('wfFrame').bindGrid();       
}
//This Method is used to download the template
function DownloadTemplate(lnk) {
    var tablename = getQStr("tablename");
    var ActivityId = getQStr("SchemaID");
    if (tablename == "TARIFF") {
        var Size = getQStr("Size");
        var Type = getQStr("Type");
        var dwnld = "Tariff_Data";
        lnk.href = "Templates/" + dwnld + ".xls";
    }
    else if (tablename == "INWARD_PASS") {
        lnk.href = "Templates/INPUT_REDELIVERY.xls";
    }
    else if (tablename == "OUTWARD_PASS") {
        lnk.href = "Templates/RESERVE_BOOKING.xls";
    }
    else if (tablename == "CUSTOMER_TARIFF" || tablename == "TARIFF_CODE_DETAIL") {
        lnk.href = "Templates/CUSTOMER_TARIFF.xls";
    }
    else {
        if (ActivityId == "81" || ActivityId == "82") {
            if (ActivityId == "81") {
                lnk.href = "Templates/PRE_ADVICE.xls";
            }
            else if (ActivityId == "82") {
                lnk.href = "Templates/Pre_Advice_iTankDepo.xls";
            }
        }
        else {
            lnk.href = "Templates/" + tablename + ".xls";
        }
    }
}

//Hide the Div
function HideData() {
    el("divifgGrid").style.display = "none";
    el("divlnk").style.display = "none";
}

//This method used to Display Valid/Invalid data
function ShowData(lnk) {
    el("divRecordNotFound").style.display = "none";
    bindGrid(el("hdnIdColumn").value, lnk.name, "Yes");
}
//el("imgLoading") has been commented to fix the IE Upload issue : not able to upload . Issue No:18
function showLayer(title) {
    setLayerPosition();
    setModalWindowTitle(title);
    var shadow;
    var question;
    if (el("shadow") == null) shadow = el("shadow");
    else shadow = el("shadow");

    if (el("question") == null) question = el("question");
    else question = el("question");

    shadow.style.display = "block";
    question.style.display = "block";
    el("imgLoading").focus();
    if (document.addEventListener) {
        el("imgLoading").addEventListener("onkeypress", disableKey);
        el("imgLoading").addEventListener("onkeydown", disableKey);
    }
    else {
        el("imgLoading").attachEvent("onkeypress", disableKey);
        el("imgLoading").attachEvent("onkeydown", disableKey);
    }
//    el("imgLoading").attachEvent("onkeypress", disableKey);
//    el("imgLoading").attachEvent("onkeydown", disableKey);
    el("imgLoading").onclick = null;
    el("imgLoading").onclick = new Function("return false;");
    shadow = null;
    question = null;
}

function hideLayer() {
    var shadow;
    var question;
    if (el("shadow") == null) shadow = el("shadow");
    else shadow = el("shadow");
    if (el("question") == null) question = el("question");
    else question = el("question");
    shadow.style.display = "none";
    question.style.display = "none";
    shadow = null;
    question = null;
    if (document.addEventListener) {
        el("imgLoading").removeEventListener("onkeypress", disableKey);
        el("imgLoading").removeEventListener("onkeydown", disableKey);
    }
    else {
        el("imgLoading").detachEvent("onkeypress", disableKey);
        el("imgLoading").detachEvent("onkeydown", disableKey);
    }
}

function disableKey() {
    if (event.keyCode != 13 && event.keyCode != 32) {
        event.returnValue = false;
    }
    if (event.keyCode == 27) {
        HideLayer();
    }
}


function setModalWindowTitle(title) {
    if (typeof (title) == "undefined") title = "Loading..";
    el("tdmodalwindowtitle").innerHTML = title;
}

function setLayerPosition() {
    var shadow;
    var question;

    if (el("shadow") == null) shadow = el("shadow");
    else shadow = el("shadow");

    if (el("question") == null) question = el("question");
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

//This event is fired after the grid bind
function onAfterCallBack(param) {
    var norecordsfound = '';
    if (param["norecordsfound"] != undefined) {
        norecordsfound = param["norecordsfound"];
    }

    if (norecordsfound == "True") {
        el("divifgGrid").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divifgGrid").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
    var lblData = '';
    if (param["lblData"] != undefined) {
        lblData = param["lblData"];
    }
    if (lblData != '') {
        setText(el('lblData'), lblData);
    }
    else {
        setText(el('lblData'), '');
    }
}