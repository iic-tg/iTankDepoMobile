var HasChanges = false;
var vrGridIds = new Array('tabAgentRates;ifgChargeDetail', 'tabAgentRates;ifgStorageDetail');
var _RowValidationFails = false;
var tabID = "0";
var pTab = "0";

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
function initPage(sMode) {
    reSizePane();
    el('iconFav').style.display = "none";
    $('.btncorner').corner();
    //el("ITab1").Select(el("ITab1").TabPages[0]);
    setFocus();
    resetValidators();
    if (sMode == MODE_NEW) {
        clearTextValues("txtAgentCode");
        clearTextValues("txtAgentName");
        clearTextValues("txtContactPersonName");
        clearTextValues("txtContactAddress");
        clearTextValues("txtBillingAddress");
        clearTextValues("txtZipCode");
        clearTextValues("txtPhoneNo");
        clearTextValues("txtFax");
        clearTextValues("txtEmailforInvoicing");
        clearLookupValues("lkpAgentCurrency");
        clearTextValues("txtHndlng_Tx_Rt");
        clearTextValues("txtSrvc_Tx_Rt");
        clearTextValues("txtStorage_Tx_Rt");
        clearTextValues("txtLbr_rt_pr_hr");

        setReadOnly("txtAgentCode", false);

        el("chkActiveBit").checked = true;
        setPageID("0");
        setPageMode("new");
        bindChargeDetail();
        setDefaultValuesonLoad();
        hideMessage();
    }
    else {
        showSubmitButton(true);
        resetValidatorByGroup("divGeneralInfo");
        resetValidatorByGroup("tabAgent");
        resetValidatorByGroup("divAgentRates");
        setReadOnly("txtAgentCode", true);
        setPageMode("edit");
        bindChargeDetail();
    }
    setText(el('lblDepotCuurency'), "(" + el('hdnDepotCurrency').value + ")");

}

function submitPage() {
    if (Page_ClientValidate('divGeneralInfo')) {
        if (Page_ClientValidate('divAgentRates')) {
            var count = ifgChargeDetail.Rows().Count;
            if (count == 0) {
                var _cols = ifgChargeDetail.Rows(0).GetClientColumns();
                ifgChargeDetail.Rows(0).SetColumnValuesByIndex(5, "0.00");
                ifgChargeDetail.Rows(0).SetColumnValuesByIndex(6, true);
                if (_cols[0] == "" || _cols[0] == null) {
                    el("ITab1").Select(el("ITab1").TabPages[1]);
                    showErrorMessage("Agent Rate is a must for atleast an Equipment Code and Type.");
                    return false;
                }
            }
            el("ITab1").Select(el("ITab1").TabPages[1]);
            if (!Page_ClientValidate('divLaborRate')) {
                el("ITab1").Select(el("ITab1").TabPages[1]);
                return false;
            }
            GetLookupChanges();
            if (!Page_IsValid) {
                return false;
            }

            if (getPageChanges()) {
                var sMode = getPageMode();
                if (ifgChargeDetail.Submit(true) == false || _RowValidationFails) {
                    return false;
                }
                if (typeof (ifgStorageDetail) != 'undefined' && typeof (ifgStorageDetail) != 'unknown') {
                    if (ifgStorageDetail.Submit(true) == false || _RowValidationFails) {
                        return false;
                    }
                }

                if (sMode == MODE_NEW) {
                    createAgent();
                }
                else if (sMode == MODE_EDIT) {
                    updateAgent();
                }
            }
            else {
                showInfoMessage('No Changes to Save');
                setFocus();
            }
        }
        else {
            el("ITab1").Select(el("ITab1").TabPages[1]);
        }
    }
    else {
        el("ITab1").Select(el("ITab1").TabPages[1]);
        if (pTab == "" || pTab == null) {
            pTab = 0;
        }
        //el("ITab1").Select(el("ITab1").TabPages[pTab]);
    }
    return true;
}

function bindChargeDetail() {
    var ifgChargeDetail = new ClientiFlexGrid("ifgChargeDetail");
    ifgChargeDetail.parameters.add("WFDATA", el("WFDATA").value);
    ifgChargeDetail.parameters.add("AgentID", getPageID());
    ifgChargeDetail.DataBind();
    $('.btncorner').corner();
}

function createAgent() {
    var oCallback = new Callback();
    oCallback.add("bv_strAGNT_CD", el("txtAgentCode").value);
    oCallback.add("bv_strAGNT_NAM", el("txtAgentName").value);
    oCallback.add("bv_i64AGNT_CRRNCY_ID", el("lkpAgentCurrency").SelectedValues[0]);
    oCallback.add("bv_strCNTCT_PRSN_NAM", el("txtContactPersonName").value);
    oCallback.add("bv_strCNTCT_ADDRSS", el("txtContactAddress").value);
    oCallback.add("bv_strBLLNG_ADDRSS", el("txtBillingAddress").value);
    oCallback.add("bv_strZP_CD", el("txtZipCode").value);
    oCallback.add("bv_strPHN_NO", el("txtPhoneNo").value);
    oCallback.add("bv_strFX_NO", el("txtFax").value);
    oCallback.add("bv_strINVCNG_EML_ID", el("txtEmailforInvoicing").value);
    oCallback.add("bv_strServiceTax", el("txtSrvc_Tx_Rt").value);
    oCallback.add("bv_strStorageTax", el("txtStorage_Tx_Rt").value);
    oCallback.add("bv_strHandlingTax", el("txtHndlng_Tx_Rt").value);
    oCallback.add("bv_decLBR_RT_PR_HR_NC", el("txtLbr_rt_pr_hr").value);

    oCallback.add("bv_blnACTV_BT", el("chkActiveBit").checked);


    oCallback.add("wfData", el("WFDATA").value);

    oCallback.invoke("Agent.aspx", "CreateAgent");

    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setPageID(oCallback.getReturnValue("ID"));
        bindChargeDetail();
        resetHasChanges("ifgChargeDetail");
        setReadOnly("txtAgentCode", true);
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}


function updateAgent() {
    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("bv_strAGNT_CD", el("txtAgentCode").value);
    oCallback.add("bv_strAGNT_NAM", el("txtAgentName").value);
    oCallback.add("bv_i64AGNT_CRRNCY_ID", el("lkpAgentCurrency").SelectedValues[0]);
    oCallback.add("bv_strCNTCT_PRSN_NAM", el("txtContactPersonName").value);
    oCallback.add("bv_strCNTCT_ADDRSS", el("txtContactAddress").value);
    oCallback.add("bv_strBLLNG_ADDRSS", el("txtBillingAddress").value);
    oCallback.add("bv_strZP_CD", el("txtZipCode").value);
    oCallback.add("bv_strPHN_NO", el("txtPhoneNo").value);
    oCallback.add("bv_strFX_NO", el("txtFax").value);
    oCallback.add("bv_strINVCNG_EML_ID", el("txtEmailforInvoicing").value);
    oCallback.add("bv_decLBR_RT_PR_HR_NC", el("txtLbr_rt_pr_hr").value);
    oCallback.add("bv_strServiceTax", el("txtSrvc_Tx_Rt").value);
    oCallback.add("bv_strStorageTax", el("txtStorage_Tx_Rt").value);
    oCallback.add("bv_strHandlingTax", el("txtHndlng_Tx_Rt").value);

    oCallback.add("bv_blnACTV_BT", el("chkActiveBit").checked);
    oCallback.add("wfData", el("WFDATA").value);


    oCallback.invoke("Agent.aspx", "UpdateAgent");

    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindChargeDetail();
        resetHasChanges("ifgChargeDetail");
        setReadOnly("txtAgentCode", true);
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}


function setFocus() {
    var sMode = getPageMode();
    if (sMode == MODE_NEW) {
        setFocusToField("txtAgentCode");
    }
    else if (sMode == MODE_EDIT) {
        setFocusToField("txtAgentName");
    }
}

function validateAgentCode(oSrc, args) {
    var oCallback = new Callback();
    var valid;
    oCallback.add("Code", el("txtAgentCode").value);

    oCallback.invoke("Agent.aspx", "ValidateCode");

    if (oCallback.getCallbackStatus()) {
        valid = oCallback.getReturnValue("valid");
        if (valid == "true") {
            oSrc.errormessage = "";
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = valid;
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgChargeDetail.ClientRowState();
    if (ifgChargeDetail.Rows().Count == "0") {
        ifgChargeDetail.Rows(0).SetColumnValuesByIndex(3, "0.00");
        ifgChargeDetail.Rows(0).SetColumnValuesByIndex(4, "0.00");
        ifgChargeDetail.Rows(0).SetColumnValuesByIndex(5, "0.00");
        ifgChargeDetail.Rows(0).SetColumnValuesByIndex(6, true);
    }
    else {
        ifgChargeDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(3, "0.00");
        ifgChargeDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(4, "0.00");
        ifgChargeDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(5, "0.00");
        ifgChargeDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(6, true);
    }
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgChargeDetail.Rows(iCurrentIndex).SetReadOnlyColumn(1, false);
        ifgChargeDetail.Rows(iCurrentIndex).SetReadOnlyColumn(2, false);
    }

}

function setDefaultValuesonLoad(iCurrentIndex) {
    var sRowState = ifgChargeDetail.ClientRowState();
    if (ifgChargeDetail.Rows().Count == "0") {
        ifgChargeDetail.Rows(0).SetColumnValuesByIndex(3, "0.00");
        ifgChargeDetail.Rows(0).SetColumnValuesByIndex(4, "0.00");
        ifgChargeDetail.Rows(0).SetColumnValuesByIndex(5, "0.00");
        ifgChargeDetail.Rows(0).SetColumnValuesByIndex(6, true);
    }
    resetHasChanges("ifgChargeDetail");
}

function resetGridChanges() {
    resetHasChanges("ifgChargeDetail");
}

function parentOnBeforeCallBack(mode, param) {
    if (mode == "Update" || mode == "Insert" || mode == "Delete") {
        param.add("mode", getPageMode());
        param.add("AgentID", getPageID());
    }
}

function childOnBeforeCallBack(mode, param) {
    if (mode == "Update" || mode == "Insert" || mode == "Delete") {
        param.add("AgentChargeDetailID", el('hdnAgentChargeDetailID').value);
        param.add("AgentID", getPageID());
        param.add("mode", getPageMode());
    }
}

function childOnAfterCallBack(param, mode) {
    if (mode == "Add" || mode == "Edit" || mode == "Delete") {
        if (typeof (param["Error"]) != 'undefined') {
            showErrorMessage(param["Error"]);
        }
        if (typeof (param["Delete"]) != 'undefined') {
            showWarningMessage(param["Delete"]);
        }
    }
}

function parentOnAfterCallBack(param, mode) {
    if (mode == "Add" || mode == "Edit" || mode == "Delete") {
        if (typeof (param["Error"]) != 'undefined') {
            showErrorMessage(param["Error"]);
        }
        if (typeof (param["Delete"]) != 'undefined') {
            showWarningMessage(param["Delete"]);
        }
    }
}

function onClientExpand(rIndex, param) {
    var _cols = ifgChargeDetail.Rows(rIndex).Columns();
    if (_cols[0] != null) {
        param.add("AgentChargeDetailID", _cols[0]);
        param.add("AgentID", getPageID());
        el('hdnAgentChargeDetailID').value = _cols[0];
    }
}

function childOnBeforeRowCreated() {
    var i;
    var _vRIndex;
    var Rcount = parseInt(ifgStorageDetail.Rows().Count);
    if (Rcount > 0) {
        i = Rcount - 1;
        var cols = ifgStorageDetail.Rows(i).GetClientColumns();
        if (cols[0] == 9999) {
            showErrorMessage("New Upto slab value can only be added, if the previous Upto slab value is less than 9999.");
            return false;
        }
        else {
            return true;
        }
    }
}

function onBeforeSelect(obj) {
    if (!Page_IsValid) {
        return false;
    }
    if (obj == 1) {
        if (Page_ClientValidate('divGeneralInfo', false)) {
            el("ITab1").Select(el("ITab1").TabPages[0]);
            return false;
        }
    }
    pTab = obj;
    tabID = obj;
}

function onAfterSelect(obj) {

    tabID = obj;
    pTab = obj;
    return false;
}

function formatRate(obj) {
    var Amount = new Number;
    if (obj.value != "") {
        Amount = parseFloat(obj.value);
        obj.value = Amount.toFixed(2);
    }
    else {
        obj.value = "";
    }
}

function validateUpToDays(oSrc, args) {
    var strValue = args.Value;
    if (strValue.indexOf('.') >= 0) {
        args.IsValid = false;
        oSrc.errormessage = "Upto Days must be a whole number";
    }
    else {
        var rIndex, _cols, FromDys, ToDys, count;
        rIndex = ifgStorageDetail.CurrentRowIndex();
        _cols = ifgStorageDetail.Rows(rIndex).GetClientColumns();
        count = parseInt(ifgStorageDetail.Rows().Count);
        ToDys = _cols[0];
        if (count > 0) {
            _cols = ifgStorageDetail.Rows(parseInt(rIndex) - 1).GetClientColumns();
            FromDys = _cols[0];
            if (Number(ToDys) <= Number(FromDys)) {
                oSrc.errormessage = "Upto Days Value should be greater than the Previous Upto Days Value";
                args.IsValid = false;
                return false;
            }
            else {
                if (rIndex < count) {
                    if (ifgStorageDetail.rC > parseInt(rIndex) + 1) {
                        _cols = ifgStorageDetail.Rows(parseInt(rIndex) + 1).GetClientColumns();
                        FromDys = _cols[0];
                        if (Number(ToDys) >= Number(FromDys)) {
                            oSrc.errormessage = "Upto Days Value should be Less than the Next Upto Days Value";
                            args.IsValid = false;
                            return false;
                        }
                    }
                }
                else {
                    args.IsValid = true;
                    return true;
                }
            }
        }
    }
}


function onClientAgent(oSrc, args) {
    var sInput = el('txtBillingAddress').value;
    if ((sInput.indexOf("'") != -1) || (sInput.indexOf('"') != -1)) {
        showErrorMessage("Fail");
        setFocusToField('txtBillingAddress');
        return false;
    }
}

function validateAddress(oSrc, args) {
    var sInput = el('txtBillingAddress').value;
    if ((sInput.indexOf("'") != -1) || (sInput.indexOf('"') != -1)) {
        showErrorMessage("Fail");
        setFocusToField('txtBillingAddress');
        return false;
    }
}

$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window.parent).height() < 768) {
        el("tabAgent").style.height = $(window.parent).height() - 360 + "px";
    }
    else {
        el("tabAgent").style.height = $(window.parent).height() - 590 + "px";
    }

}

function formatLaborRate(oSrc, args) {
    var Amount = new Number;
    if (isNaN(parseFloat(el("txtLbr_rt_pr_hr").value))) {
        el("txtLbr_rt_pr_hr").value = 0;
    }
    Amount = parseFloat(el("txtLbr_rt_pr_hr").value);
    el("txtLbr_rt_pr_hr").value = Amount.toFixed(2);
    oSrc.errormessage = "";
    args.IsValid = true;
}

function formatServiceRate() {
    var Amount = new Number;
    if (isNaN(parseFloat(el("txtSrvc_Tx_Rt").value))) {
        el("txtSrvc_Tx_Rt").value = 0;
    }
    Amount = parseFloat(el("txtSrvc_Tx_Rt").value);
    el("txtSrvc_Tx_Rt").value = Amount.toFixed(2);
}
function formatStorageRate() {
    var Amount = new Number;
    if (isNaN(parseFloat(el("txtStorage_Tx_Rt").value))) {
        el("txtStorage_Tx_Rt").value = 0;
    }
    Amount = parseFloat(el("txtStorage_Tx_Rt").value);
    el("txtStorage_Tx_Rt").value = Amount.toFixed(2);
}
function formatHandlingRate() {
    var Amount = new Number;
    if (isNaN(parseFloat(el("txtHndlng_Tx_Rt").value))) {
        el("txtHndlng_Tx_Rt").value = 0;
    }
    Amount = parseFloat(el("txtHndlng_Tx_Rt").value);
    el("txtHndlng_Tx_Rt").value = Amount.toFixed(2);
}