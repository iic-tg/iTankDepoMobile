//var HasChanges = false;
//var _RowValidationFails = false;

//function setFocus() {
//    setFocusToField("txtTo");
//}

//function SubmitBulkDetails() {
//    Page_ClientValidate();
//    if (!Page_IsValid) {
//        return false;
//    }
//    submitEmailInfo();
////    psc().closeModalWindow();
//    return true;
//}

//function submitEmailInfo() {
//    var oCallback = new Callback();
//    oCallback.add("CustomerID", el("hdnCustomer").value);
//    oCallback.add("FromEmail", el("txtFrom").value);
//    oCallback.add("ToEmail", el("txtTo").value);
//    oCallback.add("BCCEmail", el("txtBCC").value);
//    oCallback.add("Subject", el("txtSubject").value);
//    oCallback.add("EmailBody", el("txtBody").value);
//    oCallback.invoke("SendEmail.aspx", "SaveDetails");
//    if (oCallback.getCallbackStatus()) {
//        showInfoMessage(oCallback.getReturnValue("Message"));
//        HasChanges = false;
//    }
//    else {
//        showErrorMessage(oCallback.getCallbackError());
//        return false;
//    }
////    psc().closeModalWindow();
//    oCallback = null;
//    return true
//}
//function CloseDetails() {
//    psc().closeModalWindow();
//}