var HasPhotoUploadChanges = false;
var strInvalidFilename="";

function initPage() {
    CheckFlash();
    getErrorSummary();
}

function deletePhoto(attachmentId) {
    var blnDelete = "";
    var oCallback = new Callback();
    oCallback.add("AttachmentId", attachmentId);
    oCallback.add("RepairEstimateId", el('hdnRepairEstimteId').value);
    oCallback.add("PageName", el('hdnPageName').value);
    oCallback.invoke("PhotoUpload.aspx", "deletePhoto");

    if (oCallback.getCallbackStatus()) {      
        if (oCallback.getReturnValue("fileList"))
            el("photoList").innerHTML = "";
       pdfs("wfFrame" + pdf("CurrentDesk")).HasPhotoUploadChanges = true;
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
    ppsc().closeModalWindow();
}

function onUploadComplete() {
    pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = true;
    $('#btnUpload').click();
    if (strInvalidFilename != "") {
        pdfs("wfFrame" + pdf("CurrentDesk")).showErrorMessage("File Name with special charaters are not allowed.");
    }
}

function CheckFlash() {
    el('hdnPageName').value = getQueryStringValue(document.location.href, "PageName");
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
    oCallback.add("PageName", el('hdnPageName').value);
    oCallback.invoke("Photoupload.aspx", "getErrorSummary");
    if (oCallback.getCallbackStatus()==true) {
        if (oCallback.getReturnValue("errorSummary")!=null && oCallback.getReturnValue("errorSummary")!='') {
            showErrorMessage(oCallback.getReturnValue("errorSummary"));
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return;
}