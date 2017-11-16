var HasChanges = false;
var draftJson;

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
//Initialize page while loading the page based on page mode
function initPage(sMode) {
    reSizePane();
    if (sMode == MODE_NEW) {
        clearTextValues("txtUserName");
        clearTextValues("txtPassword");
        clearTextValues("txtConfirmPassword");
        clearTextValues("txtFirstName");
        clearTextValues("txtLastName");
        clearTextValues("txtEmailId");
        clearTextValues("lkpRole");
       // if (getConfigSetting('070') == "True") {
            clearTextValues("lkpDepotCode");
       // }
        setReadOnly("txtUserName", false);
        setReadOnly("txtPassword", false);
        setReadOnly("txtConfirmPassword", false);
        setPageMode("new");
        el('imgCompanyLogo').src = "../Images/noimage.jpg";
        setFocus();
        resetValidators();
        setActionButtonFocus("txtUserName", "chkActive");
        el("lnkEditPassword").innerHTML = "ADD";
        el("chkActive").checked = true;
        el("rboBlue").checked = true;
        hideDiv("lnkEditPassword");
    }
    else {
        clearTextValues("txtPassword");
        clearTextValues("txtConfirmPassword");
        resetValidatorByGroup("tabPwd");
        setReadOnly("txtUserName", true);
        setReadOnly("txtPassword", true);
        setReadOnly("txtConfirmPassword", true);
        el("lnkEditPassword").innerHTML = "Edit";
        setPageMode("edit");
        showDiv("lnkEditPassword");
        setActionButtonFocus("txtFirstName", "chkActive");
        setFocus();
        resetValidators();
    }
    el('iconFav').style.display = 'none';
    $('.btncorner').corner();


}

//This method get fired while taking action from submit pane
function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        var sMode = getPageMode();
        if (sMode == MODE_NEW) {
            createUser();
        }
        else if (sMode == MODE_EDIT) {
            updateUser();
        }
    }
    else {
        showInfoMessage("No Changes to Save");
        setFocus();
    }
    return true;
}

//This method get fired while creating new record
function createUser() {
    var oCallback = new Callback();
    oCallback.add("UserName", el("txtUserName").value);
    oCallback.add("Password", b64_md5(el("txtPassword").value));
    oCallback.add("FirstName", el("txtFirstName").value);
    oCallback.add("LastName", el("txtLastName").value);
    oCallback.add("EmailID", el("txtEmailId").value);
    oCallback.add("RoleID", el("lkpRole").SelectedValues[0]);
        oCallback.add("DepotID", el("lkpDepotCode").SelectedValues[0]);
      oCallback.add("ActiveBit", el("chkActive").checked);
    oCallback.add("wfData", el("WFDATA").value);
    var logoImage = el('imgCompanyLogo').src;
    if (logoImage.indexOf("/Images/noimage.jpg") != -1) {
        logoImage = '';
    }
    oCallback.add("PHT_PTH", logoImage);
    oCallback.add("THM_NAM", getThemeValue());
    oCallback.invoke("User.aspx", "InsertUser");
    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        clearTextValues("txtPassword");
        clearTextValues("txtConfirmPassword");
        setReadOnly("txtUserName", true);
        setReadOnly("txtPassword", true);
        setReadOnly("txtConfirmPassword", true);
        el("lnkEditPassword").innerHTML = "Edit";
        showDiv("lnkEditPassword");
        setActionButtonFocus("txtFirstName", "chkActiveBit");
        setPageID(oCallback.getReturnValue("ID"));
        el("txtUserName", true);
        HasChanges = false;
        setFocus();
        showInfoMessage(oCallback.getReturnValue("Message"));
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method get fired while changing new record
function updateUser() {
    var popup = "";    
    popup = getQueryStringValue(document.location.href, "popup");
    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("UserName", el("txtUserName").value);
    if (el("txtPassword").value != "")
        oCallback.add("Password", b64_md5(el("txtPassword").value));
    else
        oCallback.add("Password", el("txtPassword").value);
    oCallback.add("FirstName", el("txtFirstName").value);
    oCallback.add("LastName", el("txtLastName").value);
    oCallback.add("EmailID", el("txtEmailId").value);
    oCallback.add("RoleID", el("lkpRole").SelectedValues[0]);
        oCallback.add("DepotID", el("lkpDepotCode").SelectedValues[0]);
     oCallback.add("ActiveBit", el("chkActive").checked);
    oCallback.add("wfData", el("WFDATA").value);
    oCallback.add("popup", popup);
    var logoImage = el('imgCompanyLogo').src;
    if (logoImage.indexOf("/Images/noimage.jpg") != -1) {
        logoImage = '';
    }
    oCallback.add("PHT_PTH", logoImage);
    oCallback.add("THM_NAM", getThemeValue()); //fnGetThemeValue());
    oCallback.invoke("User.aspx", "UpdateUser");
    if (oCallback.getCallbackStatus()) {
        setReadOnly("txtPassword", true);
        setReadOnly("txtConfirmPassword", true);
        clearTextValues("txtPassword");
        clearTextValues("txtConfirmPassword");
        el("lnkEditPassword").innerHTML = "Edit";
        HasChanges = false;
        setFocus();
        if (popup != "" && popup == "yes") {
            psc().ChangeStyle(oCallback.getReturnValue("ThemeName"));   //Change the Styles after change the theme
            psc().closeModalWindow();
        }
        showInfoMessage(oCallback.getReturnValue("Message"));
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

//This function is used to validate user name
function validateUserName(oSrc, args) {
    var oCallback = new Callback();
    var validatedresult;
    oCallback.add("UserName", el("txtUserName").value);
     oCallback.add("DepotID", 0);
    oCallback.invoke("User.aspx", "ValidateUserName");
    if (oCallback.getCallbackStatus()) {
        validatedresult = oCallback.getReturnValue("pkValid");
        if (validatedresult == "true") {
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = "This User Name already exists"
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

//This method get fired on click of edit button
function onEditClick() {
    var lnkEdit = el("lnkEditPassword");
    var txtPassword = el("txtPassword");
    var txtConfirmPassword = el("txtConfirmPassword");
    var sMode = getPageMode();
    resetValidatorByGroup("tabPwd");
    if (sMode == MODE_EDIT) {
        if (lnkEdit.innerHTML == "Edit") {
            lnkEdit.innerHTML = "Cancel";
            setReadOnly("txtPassword", false);
            setFocusToField("txtPassword");
            setReadOnly("txtConfirmPassword", false);            
        }
        else {
            lnkEdit.innerHTML = "Edit";            
            clearTextValues("txtPassword");
            clearTextValues("txtConfirmPassword");            
            setReadOnly("txtPassword", true);
            setReadOnly("txtConfirmPassword", true);
            setFocusToField("txtFirstName");
            hideMessage();
        }
    }
}

//This method get fired while changing new record
function setFocus() {
    var sMode = getPageMode();
    if (sMode == MODE_NEW) {
        setFocusToField("txtUserName");
    }
    else if (sMode == MODE_EDIT) {
        setFocusToField("txtFirstName");
    }
}
function BrowseImage() {
    var fmUpload;
    fmUpload = fmPhotoUpload.document.frmPhotoUpload;

    if (fmUpload == null) {
        showErrorMessage("There could be an issue with the last attached file. Due to security reasons, please relogin into the application.");
        return false;
    }
    fmUpload.imagebrowse.click();
    fmUpload.btnSubmit.click();
    HasChanges = true;
}
function SetImageUrl(path) {
    el('imgCompanyLogo').src = path;
}
function getThemeValue() {
    var rboBlue = el("rboBlue").checked;
    var rboMatrix = el("rboMatrix").checked;
    var rboClassic = el("rboClassic").checked;
    var ThemeValue = '';
    if ((rboBlue == true && rboMatrix == false && rboClassic == false))
        ThemeValue = 'Blue';
    else if ((rboBlue == false && rboMatrix == true && rboClassic == false))
        ThemeValue = 'Matrix';
    else if ((rboBlue == false && rboMatrix == false && rboClassic == true))
        ThemeValue = 'Classic';
    return ThemeValue;
}
$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 768) {
            el("tabUser").style.height = $(window.parent).height() - 360 + "px";

        }
        else {
            el("tabUser").style.height = $(window.parent).height() - 463 + "px";
           
        }
    }

}
