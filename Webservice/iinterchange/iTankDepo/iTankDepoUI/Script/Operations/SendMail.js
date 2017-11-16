function sendEmail() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        CreateBulkMail();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    return true;
}

//This method used to close email popup
function cancelEmail() {
    ppsc().closeModalWindow();
}

function CreateBulkMail() {
    var oCallback = new Callback();
    oCallback.add("bv_strFrom", el("txtFrom").value);
    oCallback.add("bv_strTo", el("txtTo").value);
    oCallback.add("bv_strSubject", el("txtSubject").value);
    oCallback.add("bv_strBody", el("txtBody").value);
    oCallback.invoke("SendMail.aspx", "CreateBulkMail");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
    }
     else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

