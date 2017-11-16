var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgCleaningType', 'ITab1_0;ifgProductCustomer');
var _RowValidationFails = false;


if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
function initPage(sMode) {
    reSizePane();
    if (sMode == MODE_NEW) {
        //clearTextValues("txtPRDCT_CD");
        clearTextValues("txtPRDCT_DSCRPTN_VC");
        clearTextValues("txtCHMCL_NM");
        clearTextValues("txtIMO_CLSS");
        clearTextValues("txtUN_NO");
        clearTextValues("lblCLNNG_TTL_AMNT_NC");
        clearTextValues("lkpCLSSFCTN_ID");
        clearTextValues("lkpType");
        clearTextValues("lkpGRP_CLSSFCTN_ID");
        clearTextValues("txtRMRKS_VC");
        el("lblCLNNG_TTL_AMNT_NC").innerText = "0.00";
        el('hypFilesAttachment').innerText = "Attach Files" + "(0)";
        setReadOnly("txtPRDCT_CD", false);
        el("chkCLNBL_BT").checked = true;
        el("chkACTV_BT").checked = true;
        setPageID("0");
        setPageMode("new");
        setFocus();
        resetValidators();
        bindCleaningType();
        bindProductCustomer();
        if (getConfigSetting('ProductSerialNo') == "TRUE") {
            setReadOnly("txtPRDCT_CD", true);
        }
    }
    else {
        showSubmitButton(true);
        resetValidatorByGroup("tabProduct");
        setReadOnly("txtPRDCT_CD", true);
        setPageMode("edit");
        setFocus();
        resetValidators();
        bindCleaningType();
        bindProductCustomer();
    }

    el('iconFav').style.display = "none";
    $('.btncorner').corner();
}

function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }

    if (getPageChanges()) {
        var sMode = getPageMode();
        if (ifgCleaningType.Submit(true) == false || _RowValidationFails) {
            return false;
        }
        if (sMode == MODE_NEW) {
            createProduct();
        }
        else if (sMode == MODE_EDIT) {
            updateProduct();
        }
    }
    else {
        showInfoMessage('No Changes to Save');
        setFocus();
    }
    return true;
}

function bindCleaningType() {
    var ifgCleaningType = new ClientiFlexGrid("ifgCleaningType");
    ifgCleaningType.parameters.add("WFDATA", el("WFDATA").value);
    ifgCleaningType.parameters.add("ProductID", getPageID());
    ifgCleaningType.DataBind();
    $('.btncorner').corner();
}

function bindProductCustomer() {
    var ifgProductCustomer = new ClientiFlexGrid("ifgProductCustomer");
    ifgProductCustomer.parameters.add("WFDATA", el("WFDATA").value);
    ifgProductCustomer.parameters.add("ProductID", getPageID());
    ifgProductCustomer.DataBind();
    $('.btncorner').corner();
}

function setFocus() {
    var sMode = getPageMode();
    if (sMode == MODE_NEW) {
        setFocusToField("txtPRDCT_CD");
    }
    else if (sMode == MODE_EDIT) {
        setFocusToField("txtPRDCT_DSCRPTN_VC");
    }
}

function fnValidateProductCode(oSrc, args) {
    var oCallback = new Callback();
    var valid;
    oCallback.add("Code", el("txtPRDCT_CD").value);

    oCallback.invoke("Product.aspx", "ValidateCode");

    if (oCallback.getCallbackStatus()) {
        valid = oCallback.getReturnValue("valid");
        if (valid == "true") {
            oSrc.errormessage = "";
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = "Product Code already in use.";
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function checkDuplicate(oSrc, args) {
    var rIndex = ifgCleaningType.VirtualCurrentRowIndex();
    var cols = ifgCleaningType.Rows(rIndex).GetClientColumns();
    var i;
    var sPort = cols[0];
    var icount = ifgCleaningType.Rows().Count
    var colscheck;

    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgCleaningType.Rows(i).GetClientColumns();
                if (sPort == colscheck[0]) {
                    args.IsValid = false;
                    oSrc.errormessage = "This Code already exists";
                    return;
                }
            }
        }
    }
}

function createProduct() {
    var oCallback = new Callback();
    oCallback.add("bv_strPRDCT_CD", el("txtPRDCT_CD").value);
    oCallback.add("bv_strPRDCT_DSCRPTN_VC", el("txtPRDCT_DSCRPTN_VC").value);
    oCallback.add("bv_strCHMCL_NM", el("txtCHMCL_NM").value);
    oCallback.add("bv_strIMO_CLSS", el("txtIMO_CLSS").value);
    oCallback.add("bv_strUN_NO", el("txtUN_NO").value);
    oCallback.add("bv_i64CLSSFCTN_ID", el("lkpCLSSFCTN_ID").SelectedValues[0]);
    oCallback.add("bv_i64GRP_CLSSFCTN_ID", el("lkpGRP_CLSSFCTN_ID").SelectedValues[0]);
    oCallback.add("bv_strRMRKS_VC", el("txtRMRKS_VC").value);
    oCallback.add("bv_dblCLNNG_TTL_AMNT_NC", el('lblCLNNG_TTL_AMNT_NC').innerText);
    oCallback.add("bv_blnCLNBL_BT", el("chkCLNBL_BT").checked);
    oCallback.add("bv_blnACTV_BT", el("chkACTV_BT").checked);
    oCallback.add("TypeID", el("lkpType").SelectedValues[0]);
    oCallback.add("PageMode", getPageMode());
    oCallback.add("wfData", el("WFDATA").value);

    oCallback.invoke("Product.aspx", "CreateProduct");
    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setPageID(oCallback.getReturnValue("ID"));
        bindCleaningType();
        bindProductCustomer();
        resetHasChanges("ifgCleaningType");
        resetHasChanges("ifgProductCustomer");
        setReadOnly("txtPRDCT_CD", true);
        setFocus();
        el('lblCLNNG_TTL_AMNT_NC').innerText = parseFloat(oCallback.getReturnValue("CleaningRate")).toFixed(2);
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function updateProduct() {
    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("bv_strPRDCT_CD", el("txtPRDCT_CD").value);
    oCallback.add("bv_strPRDCT_DSCRPTN_VC", el("txtPRDCT_DSCRPTN_VC").value);
    oCallback.add("bv_strCHMCL_NM", el("txtCHMCL_NM").value);
    oCallback.add("bv_strIMO_CLSS", el("txtIMO_CLSS").value);
    oCallback.add("bv_strUN_NO", el("txtUN_NO").value);
    oCallback.add("bv_i64CLSSFCTN_ID", el("lkpCLSSFCTN_ID").SelectedValues[0]);
    oCallback.add("bv_i64GRP_CLSSFCTN_ID", el("lkpGRP_CLSSFCTN_ID").SelectedValues[0]);
    oCallback.add("bv_strRMRKS_VC", el("txtRMRKS_VC").value);
    oCallback.add("bv_dblCLNNG_TTL_AMNT_NC", el('lblCLNNG_TTL_AMNT_NC').innerText);
    oCallback.add("bv_blnCLNBL_BT", el("chkCLNBL_BT").checked);
    oCallback.add("bv_blnACTV_BT", el("chkACTV_BT").checked);
    oCallback.add("TypeID", el("lkpType").SelectedValues[0]);
    oCallback.add("PageMode", getPageMode());
    oCallback.add("wfData", el("WFDATA").value);

    oCallback.invoke("Product.aspx", "UpdateProduct");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        setPageMode(MODE_EDIT);
        HasChanges = false;
        bindCleaningType();
        bindProductCustomer();
        resetHasChanges("ifgCleaningType");
        resetHasChanges("ifgProductCustomer");
        setReadOnly("txtPRDCT_CD", true);
        setFocus();
        el('lblCLNNG_TTL_AMNT_NC').innerText = parseFloat(oCallback.getReturnValue("CleaningRate")).toFixed(2);
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

// Check "Active" check box in page load for first column
// as well as when a new row added.
function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgCleaningType.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgCleaningType.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    resetHasChanges("ifgCleaningType");
    return true;

}

function resetGridChanges() {
    resetHasChanges("ifgCleaningType");
    resetHasChanges("ifgProductCustomer");
}

function OnBeforeCallBack(mode, param) {
    if (mode == "Update" || mode == "Insert" || mode == "Delete") {
        param.add("mode", getPageMode());
        param.add("ProductID", getPageID());
    }
}

function OnAfterCallBack(param, mode) {
    if (typeof (param["CleaningRate"]) != "undefined") {
        var cleaningAmount = new Number;
        cleaningAmount = parseFloat(param["CleaningRate"]);
        el('lblCLNNG_TTL_AMNT_NC').innerText = cleaningAmount.toFixed(2);
    }
    if (typeof (param["ProductCleaningRate"]) != "undefined") {
        el('hdnProductCleaningRate').value = param["ProductCleaningRate"];
    }
    if (mode == "Add" || mode == "Edit" || mode == "Delete") {
        if (typeof (param["Duplicate"]) != "undefined") {
            showErrorMessage(param["Duplicate"]);
        }
        if (typeof (param["CheckDefault"]) != "undefined") {
            showErrorMessage(param["CheckDefault"]);
        }
        if (typeof (param["Delete"]) != "undefined") {
            showWarningMessage(param["Delete"]);
        }
    }
}

function formatCleaningType(obj) {
    var Amount = new Number;
    if (obj.value != "") {
        Amount = parseFloat(obj.value);
        obj.value = Amount.toFixed(2);
    }
    else {
        obj.value = "";
    }

}

function applyFilter() {
    return "DFLT_BT=0";
}

function showCleaningType() {

    if (ifgProductCustomer.Submit() == false) {
        return false;
    }
    else {
        var _rlngth = ifgProductCustomer.Rows().Count;
        if ((ifgProductCustomer.rowIndex) == "") {
            if ((parseInt(_rlngth) - 1) == (ifgProductCustomer.rowIndex)) {
                onClickAddEdit();
            }
        }
        else {
            onClickAddEdit();
        }
    }
}

function onClickAddEdit() {
    var cols = ifgProductCustomer.Rows(ifgProductCustomer.rowIndex).Columns();
    if (cols.length == 1) {
        if (ifgProductCustomer.Submit(true) == false) {
            return false;
        }
    } else {
        ifgProductCustomer.Submit(true);
        showModalWindow("Cleaning Type - " + cols[5], "Masters/CleaningTypePopup.aspx?PRDCT_ID=" + getPageID() + "&rowIndex=" + ifgProductCustomer.rowIndex + "&PRDCT_CSTMR_ID=" + cols[0] + "&CSTMR_ID=" + cols[2], "450px", "400px", "", "", "", "", "");
        psc().hideLoader();
    }
}

function checkDuplicateProductCustomer(oSrc, args) {
    var rIndex = ifgProductCustomer.VirtualCurrentRowIndex();

    var cols = ifgProductCustomer.Rows(rIndex).GetClientColumns();
    var i;
    var sPort = cols[0];
    var icount = ifgProductCustomer.Rows().Count
    var colscheck;
    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgProductCustomer.Rows(i).GetClientColumns();
                if (sPort == colscheck[0]) {
                    args.IsValid = false;
                    oSrc.errormessage = "This Code already exists";
                    return;
                }
            }
        }
    }
}

function OnifgProductCustomerBeforeCallBack(mode, param) {
    if (mode == "Update" || mode == "Insert" || mode == "Delete") {
    }
}

function OnifgProductCustomerAfterCallBack(param, mode) {
    if (mode == "Add" || mode == "Edit" || mode == "Delete") {
        if (typeof (param["Delete"]) != "undefined") {
            showWarningMessage(param["Delete"]);
        }
    }
}

function showFilesAttachment() {
    showModalWindow("Product", "Masters/ProductAttachmentDetail.aspx?ProductId=" + getPageID(), "850px", "550px", "150px", "", "", "","wfs().onCloseAttachmentCount('" + getPageID() + "')");
    psc().hideLoader();
}

function onCloseAttachmentCount(productId) {
    var oCallback = new Callback();
    oCallback.add("ProductId", productId);
    oCallback.invoke("Product.aspx", "AttachmentCount");
    if (oCallback.getCallbackStatus()) {        
        el('hypFilesAttachment').innerText = "Attach Files" + "(" + oCallback.getReturnValue("AttachmentCount") + ")";
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window.parent).height() < 768) {
        el("tabProduct").style.height = $(window.parent).height() - 369 + "px";
        if (el("ifgProductCustomer") != null) {
            el("ifgProductCustomer").SetStaticHeaderHeight($(window.parent).height() - 526 + "px");
            el("ifgCleaningType").SetStaticHeaderHeight($(window.parent).height() - 526 + "px");
        }
    }
    else {
        el("tabProduct").style.height = $(window.parent).height() - 474 + "px";
        if (el("ifgProductCustomer") != null) {
            el("ifgProductCustomer").SetStaticHeaderHeight($(window.parent).height() - 638 + "px");
            el("ifgCleaningType").SetStaticHeaderHeight($(window.parent).height() - 631 + "px");
        }
    }

}