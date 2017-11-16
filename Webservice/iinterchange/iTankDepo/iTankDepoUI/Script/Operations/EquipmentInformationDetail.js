var HasPhotoUploadChanges = false;
var _gateinId = "";
var _eqpmntNo = "";

function initPage() {
    CheckFlash();
    getErrorSummary();
    _gateinId = getQueryStringValue(document.location.href, "GateInId");
    _eqpmntNo = getQueryStringValue(document.location.href, "EquipmentNo");
}

function deletePhoto(EquipmentInformationDetailId, EquipmentInformationId) {
    var blnDelete = "";
    var oCallback = new Callback();
    oCallback.add("EquipmentInformationDetailId", EquipmentInformationDetailId);
    oCallback.add("EquipmentInformationId", EquipmentInformationId);
    oCallback.invoke("EquipmentInformationDetail.aspx", "deletePhoto");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("fileList"))
            el("photoList").innerHTML = "";
        pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = true;
        el("photoList").innerHTML = oCallback.getReturnValue("fileList");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return;
}

$().ready(function () {
    $('#filename').change(function () {
        $('#btnUpload').click();
        pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = true;
    });
});

function onClosePhotoUpload() {
    if (_gateinId != "") {
        showModalWindow("Gate In - Equipment Information", "Operations/EquipmentInformation.aspx?GateInId=" + _gateinId + "&EquipmentNo=" + _eqpmntNo + "&activityid=80" + "&HasChanges=" + pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges + "&Attachment=TRUE", "950px", "250px", "100px", "", "");
        return true;
        ppsc().closeModalWindow();
    }
    else {

        ppsc().closeModalWindow();
    } 
}

function onUploadComplete() {
    pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = true;
    HasPhotoUploadChanges = true;
    $('#btnUpload').click();   
}

function CheckFlash() { 
    var hasFlash = false;
    if (swfobject.getFlashPlayerVersion().major > 9) {
        hasFlash = true;
        document.getElementById("divFile").style.display = "none";
        document.getElementById("divMessage").style.display = "none";
    }
    else {
        document.getElementById("divFlash").style.display = "none";
        document.getElementById("divMessage").style.display = "block";
    }
}

function displayError(invalidFileName) {
    strInvalidFilename = invalidFileName;
}

function getErrorSummary() {
    var oCallback = new Callback();
    oCallback.invoke("EquipmentInformationDetail.aspx", "getErrorSummary");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("errorSummary") != '' && oCallback.getReturnValue("errorSummary") != null) {
            showErrorMessage(oCallback.getReturnValue("errorSummary"));
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return;
}