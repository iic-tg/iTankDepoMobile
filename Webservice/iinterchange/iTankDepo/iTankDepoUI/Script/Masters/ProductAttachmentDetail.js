var HasPhotoUploadChanges = false;


function initPage() {
    CheckFlash();
    getErrorSummary();  
}

function deletePhoto(ProductAttachmentDetailId, ProductId) {
    var blnDelete = "";
    var oCallback = new Callback();
    oCallback.add("EquipmentInformationDetailId", ProductAttachmentDetailId);
    oCallback.add("EquipmentInformationId", ProductId);
    oCallback.invoke("ProductAttachmentDetail.aspx", "deletePhoto");
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
    ppsc().closeModalWindow();

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
    oCallback.invoke("ProductAttachmentDetail.aspx", "getErrorSummary");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("errorSummary") != '' && oCallback.getReturnValue("errorSummary") != null) {//Chrome Fix
            showErrorMessage(oCallback.getReturnValue("errorSummary"));
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return;
}