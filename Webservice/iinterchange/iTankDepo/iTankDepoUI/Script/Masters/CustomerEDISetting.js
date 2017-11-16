var HasChanges = false;
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    submitEDIInfo();
    return true;
}

function submitEDIInfo() {
    var oCallback = new Callback();
    oCallback.add("ToEDIEmail", el("txtEDIemailid").value);
    oCallback.add("GenerationFormatID", el("lkpGenerationFormat").SelectedValues[0]);
    oCallback.add("GenerationFormatCode", el("lkpGenerationFormat").value);
    oCallback.add("GenerationTime", el("txtGenerationTime").value);
    oCallback.add("EDIFormatID", el("lkpEDIFormat").SelectedValues[0]);
    oCallback.add("EDIFormatCode", el("lkpEDIFormat").value);
    oCallback.add("HiddenID", el("hdnCustomerID").value);
    oCallback.invoke("CustomerEDISetting.aspx", "submit");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("blnPeriodic")) {
            showErrorMessage("Generation Time format should be in Hours (0 to 23)");
            return;
        }
        if (oCallback.getReturnValue("blnSpecific")) {
            showErrorMessage("Generation Time format should be Hours:Minutes(HH:MM)");
            return;
        }
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
    }
    else {
        showErrorMessage(oCallback.getReturnValue("Message"));
    }
    ppsc().closeModalWindow();
    oCallback = null;
    return true;
}