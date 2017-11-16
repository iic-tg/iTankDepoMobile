var HasChanges = false;
var _RowValidationFails = false;
var vrGridIds = new Array('ITab1_0;ifgBankDetail');

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
//Initialize page while loading the page based on page mode
function initPage(sMode) {
    var header = "Master >> Depot";
    var res;
    if (sMode == "new") {
        res = header.concat(" -  New");
    }
    else if (sMode == "edit") {
        res = header.concat(" - Edit");
    }
    if (getConfigSetting('070') != "True") {
        el("lblOrganizationType").style.display = "none";
        el("lblOrgTypReq").style.display = "none";
        el("divLkpOrganizationType").style.display = "none";
        el("lblReportingTo").style.display = "none";
        el("divLkpDepot").style.display = "none";
        el("lblReportToReq").style.display = "none";
        el('txtCntctPrsn').tabIndex = 6;
        el("lstBack").style.display = "none";
        el("lstFirst").style.display = "none";
        el("lstPrev").style.display = "none";
        el("lstNext").style.display = "none";
        el("lstLast").style.display = "none";
        el("iconFav").style.display = "none"; 
    }
    
    el("spnHeader").innerHTML = res;
  
  
    if (sMode == MODE_NEW) {
        clearTextValues("txtDpt_cd");
        clearTextValues("txtDpt_Nam");
        clearTextValues("txtCntctPrsn");
        clearTextValues("txtCntct_Addrss_Ln1");
        clearTextValues("txtCntct_Addrss_Ln2");
        clearTextValues("txtCntct_Addrss_Ln3");
        clearTextValues("txtEml_ID");
        clearTextValues("txtfx_no");
        clearTextValues("txtphn_no");
      //  clearTextValues("txtZp_Cd");
        clearTextValues("txtVAT_NO");
        clearLookupValues("lkpOrgizationType");
        clearLookupValues("lkpDepotCode");
        setReadOnly("lkpOrgizationType", false);
        setReadOnly("lkpDepotCode", false);
        el('imgCompanyLogo').src = "../Images/noimage.jpg";
       // el("divLogo").style.display = "none";
        setReadOnly("txtDpt_cd", false);
        setPageMode("new");
        setPageID("0");
        setFocus();
        bindGrid();
        resetValidators();
        setActionButtonFocus("txtDpt_Nam", "chkActive");
    }
    else {
        setReadOnly("txtDpt_cd", true);
        setPageMode("edit");
        setActionButtonFocus("txtDpt_cd", "chkActive");
        el("divLogo").style.display = "block";
        setFocus();
        bindGrid();
        resetValidators();
    }
    $('.btncorner').corner();
    reSizePane();

}

//This method get fired while taking action from submit pane
function submitPage() {
    ifgBankDetail.Submit();
    if (ifgBankDetail.Search == true) {
        return false;
    }
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        if (getConfigSetting('070') == "True") {
            if (el('lkpOrgizationType').SelectedValues[0] == '') {
                showErrorMessage('Organization Type Required');
                setFocusToField("lkpOrgizationType");
                return false;
            }
            else if (el('lkpDepotCode').SelectedValues[0] == '') {
                showErrorMessage('Reporting To Depot Required');
                setFocusToField("lkpDepotCode");
                return false;
            }
        }
        var sMode = getPageMode();
        if (ifgBankDetail.Submit(true) == false || _RowValidationFails) {
            return false;
        }
        if (sMode == MODE_NEW) {
            createDepot();
        }
        else if (sMode == MODE_EDIT) {
            updateDepot();
        }
    }
    else {
        showInfoMessage("No Changes to Save");
        setFocus();
    }
    return true;
}

//This method get fired while creating new record
function createDepot() {
    var oCallback = new Callback();
    oCallback.add("DepotCode", el("txtDpt_cd").value);
    oCallback.add("DepotName", el("txtDpt_Nam").value);
    oCallback.add("ContactPerson", el("txtCntctPrsn").value);
    oCallback.add("ContactAddressLine1", el("txtCntct_Addrss_Ln1").value);
    oCallback.add("ContactAddressLine2", el("txtCntct_Addrss_Ln2").value);
    oCallback.add("ContactAddressLine3", el("txtCntct_Addrss_Ln3").value);
    oCallback.add("EmailID", el("txtEml_ID").value);
    oCallback.add("Phone", el("txtphn_no").value);
    oCallback.add("Fax", el("txtfx_no").value);
    oCallback.add("VAT", el("txtVAT_NO").value);
    oCallback.add("OrganizationType", el("lkpOrgizationType").SelectedValues[0]);
    oCallback.add("ReportingTo", el("lkpDepotCode").SelectedValues[0]);
    var logoImage = el('imgCompanyLogo').src;
    if (logoImage.indexOf("/Images/noimage.jpg") != -1) {
        showErrorMessage("Please Select a Image!");
        return;
    }

    oCallback.add("LG_PTH", el('imgCompanyLogo').src);
    oCallback.add("wfData", el("WFDATA").value);
    oCallback.invoke("Depot.aspx", "InsertDepot");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("blnGrid")) {
            showErrorMessage("Atleast one Tariff Code must be entered");
            return;
        }
        if (oCallback.getReturnValue("DepotTotalCount")) {
            showErrorMessage("Maximum Number of Depots exceeded");
            return;
        }
        
        showInfoMessage(oCallback.getReturnValue("Message"));
        setPageMode(MODE_EDIT);
        setReadOnly("txtDpt_cd", true);
        setActionButtonFocus("txtDpt_Nam", "chkActiveBit");
        setPageID(oCallback.getReturnValue("ID"));
        bindGrid();
        HasChanges = false;
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

//This method get fired while changing new record
function updateDepot() {
    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("DepotCode", el("txtDpt_cd").value);
    oCallback.add("DepotName", el("txtDpt_Nam").value);
    oCallback.add("ContactPerson", el("txtCntctPrsn").value);
    oCallback.add("ContactAddressLine1", el("txtCntct_Addrss_Ln1").value);
    oCallback.add("ContactAddressLine2", el("txtCntct_Addrss_Ln2").value);
    oCallback.add("ContactAddressLine3", el("txtCntct_Addrss_Ln3").value);
    oCallback.add("EmailID", el("txtEml_ID").value);
    oCallback.add("Phone", el("txtphn_no").value);
    oCallback.add("Fax", el("txtfx_no").value);
    oCallback.add("VAT", el("txtVAT_NO").value);
    oCallback.add("OrganizationType", el("lkpOrgizationType").SelectedValues[0]);
    oCallback.add("ReportingTo", el("lkpDepotCode").SelectedValues[0]);
    var logoImage = el('imgCompanyLogo').src;
    if (logoImage.indexOf("/Images/noimage.jpg") != -1) {
        showErrorMessage("Please Select a Image!");
        return;
    }

    oCallback.add("LG_PTH", el('imgCompanyLogo').src);
    oCallback.add("wfData", el("WFDATA").value);

    oCallback.invoke("Depot.aspx", "UpdateDepot");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("checkBankDetail")) {
            showErrorMessage("Atleast one Bank detail must be Required");
            return;
        }
        bindGrid();
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

//This function is used to validate Depot Name
function ValidateDepotCode(oSrc, args) {
    var oCallback = new Callback();
    var validatedresult;
    oCallback.add("DepotCode", el("txtDpt_cd").value);
    oCallback.invoke("Depot.aspx", "ValidateDepotCode");
    if (oCallback.getCallbackStatus()) {
        validatedresult = oCallback.getReturnValue("pkValid");
        if (validatedresult == "true") {
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = "This Depot Code already exists"
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

//This method get fired while changing new record
function setFocus() {
    var sMode = getPageMode();
    if (sMode == MODE_NEW) {
        setFocusToField("txtDpt_cd");
    }
    else if (sMode == MODE_EDIT) {
        setFocusToField("txtDpt_Nam");
    }
}

function fnBrowseImage() {
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

function bindGrid() {
    var ifgBankDetail = new ClientiFlexGrid("ifgBankDetail");
    ifgBankDetail.parameters.add("WFDATA", el("WFDATA").value);
    ifgBankDetail.parameters.add("DepotID", getPageID());
    ifgBankDetail.DataBind();
    $('.btncorner').corner();

}

function OnBeforeCallBack(mode, param) {
    if (mode == "Update" || mode == "Insert" || mode == "Delete") {
        param.add("mode", getPageMode());
        param.add("ID", getPageID());
    }
}

function OnAfterCallBack(param, mode) {
    if (mode == "Add" || mode == "Edit" || mode == "Delete") {
        if (typeof (param["Duplicate"]) != 'undefined') {
            showErrorMessage(param["Duplicate"]);
            resetHasChanges("ifgTariffGroupDetail");
        }
    }
}

function checkDuplicate(oSrc, args) {
    var rIndex = ifgBankDetail.VirtualCurrentRowIndex();
    var cols = ifgBankDetail.Rows(rIndex).GetClientColumns();
    var i;
    var sPort = cols[0];
    var icount = ifgBankDetail.Rows().Count
    var colscheck;

    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgBankDetail.Rows(i).GetClientColumns();
                if (sPort == colscheck[0]) {
                    args.IsValid = false;
                    oSrc.errormessage = "This Code already exists";
                    return;
                }
            }
        }
    }
}

$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 680) {
            if (el("tabDepot") != null) {
                if (!chrome) {
                    el("tabDepot").style.height = $(window.parent).height() - 370 + "px";
                }
                else {
                    el("tabDepot").style.height = $(window.parent).height() - 367 + "px";
                }
                if (el("ifgBankDetail") != null) {
                    el("ifgBankDetail").SetStaticHeaderHeight($(window.parent).height() - 559 + "px");
                }
            }
        }
        else if ($(window.parent).height() < 768) {
            if (el("tabDepot") != null) {
                if (!chrome) {
                    el("tabDepot").style.height = $(window.parent).height() - 370 + "px";
                }
                else {
                    el("tabDepot").style.height = $(window.parent).height() - 376 + "px";
                }
                if (el("ifgBankDetail") != null) {
                    el("ifgBankDetail").SetStaticHeaderHeight($(window.parent).height() - 555 + "px");
                }
            }
        }
        else {
            if (el("tabDepot") != null) {
                if (!chrome) {
                    el("tabDepot").style.height = $(window.parent).height() - 465 + "px";
                }
                else {
                    el("tabDepot").style.height = $(window.parent).height() - 471 + "px";
                }
                if (el("ifgBankDetail") != null) {
                    el("ifgBankDetail").SetStaticHeaderHeight($(window.parent).height() - 656 + "px");
                }
            }
        }
    }

}

function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgBankDetail.ClientRowState();
    if (ifgBankDetail.Rows().Count == "0") {
        ifgBankDetail.Rows(0).SetColumnValuesByIndex(0, "");
        ifgBankDetail.Rows(0).SetColumnValuesByIndex(1, "");
        ifgBankDetail.Rows(0).SetColumnValuesByIndex(2, "");
        ifgBankDetail.Rows(0).SetColumnValuesByIndex(3, "");
        ifgBankDetail.Rows(0).SetColumnValuesByIndex(4, "");
        ifgBankDetail.Rows(0).SetColumnValuesByIndex(5, "");
        ifgBankDetail.Rows(0).SetColumnValuesByIndex(6, "");
    }
}

function validateOrganizationType(oSrc, args) {
    if (el('lkpOrgizationType').SelectedValues[0] == '' && getConfigSetting('070') == "True") {
        oSrc.errormessage = "Orgization Type Required";
        args.IsValid = false;
    }
    if (el("lkpOrgizationType").SelectedValues[0] == "153") {
        setReadOnly("lkpDepotCode", false);
        var oCallback = new Callback();
        oCallback.add("DepotCode", el("txtDpt_cd").value);
        oCallback.invoke("Depot.aspx", "GetHQDepotID");
        if (oCallback.getCallbackStatus()) {
            if (oCallback.getReturnValue("HQDepotCount") > 0) {
                args.IsValid = false;
                oSrc.errormessage = "Already a HQ is present";
            }
        }
    }
}

function filterReportingTo() {
    return "ORGNZTN_TYP_ID=153";
}

function filterOrganizationType() {
    return "ENM_ID=154";
}