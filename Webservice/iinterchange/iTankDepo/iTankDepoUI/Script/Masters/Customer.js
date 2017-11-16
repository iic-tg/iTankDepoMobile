var HasChanges = false;
var vrGridIds = new Array('tabCustomerRates;ifgChargeDetail', 'tabCustomerRates;ifgStorageDetail', 'tabCustomerRates;ifgCustomerTransportation', 'tabCustomerRates;ifgCustomerRental');
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
    if (getConfigSetting('050') == "True") {
        hideDiv("divStdRate");
        document.getElementById("divStandardRates").style.height = "0px";
        hideDiv("tabRental");
        hideDiv("tabFtp");
        if (el('tabTransportation') != undefined)
            hideDiv("tabTransportation");
        if (el('divRental') != undefined)
            hideDiv("divRental");
        if (el('divFtp') != undefined)
            hideDiv("divFtp");
        if (el('divTransportation') != undefined)
            hideDiv("divTransportation");
        document.getElementById("divTransportation").style.height = "0px";
        document.getElementById("divRental").style.height = "0px";
        document.getElementById("divFtp").style.height = "0px";
    }
    else {
    }
    if (sMode == MODE_NEW) {
        clearTextValues("txtCustomerCode");
        clearTextValues("txtCustomerName");
        clearTextValues("txtContactPersonName");
        clearTextValues("txtContactAddress");
        clearTextValues("txtBillingAddress");
        clearTextValues("txtZipCode");
        clearTextValues("txtPhoneNo");
        clearTextValues("txtFax");
        clearTextValues("txtEmailforReporting");
        clearTextValues("txtEmailforInvoicing");
        clearTextValues("txtEmailforRepairTech");
        clearLookupValues("lkpBulkEmailFormat");
        clearLookupValues("lkpBillingType");
        clearLookupValues("lkpCustomerCurrency");
        clearTextValues("txtHydroAmount");
        clearTextValues("txtPneumaticAmount");
        clearTextValues("txtLaborRateperHour");
        clearTextValues("txtLeakTestRate");
        clearTextValues("txtSurveyRate");
        clearLookupValues("lkpPeriodicTestType");
        clearTextValues("txtMinHeatingRate");
        clearTextValues("txtMinHeatingPeriod");
        clearTextValues("txtHourlyCharge");
        clearTextValues("txtEdiCode");
        if (getConfigSetting('050') == "True") {
            clearTextValues("txtCustVatNo");
            clearTextValues("txtHndlng_Tx_Rt");
            clearTextValues("txtStorage_Tx_Rt");
            clearTextValues("txtSrvc_Tx_Rt");
            clearTextValues("txtLaborRate");
            clearLookupValues("lkpAgent");
        }

        // Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :57
        setText(el('lblValidityPeriodTest'), "0");
        //        el("lblValidityPeriodTest").innerText = "0";
        setReadOnly("txtCustomerCode", false);
        if (getConfigSetting('050') != "True") {
            if (getConfigSetting('AllowRental') == "True") {
                el("chkRentalBit").checked = true;
            }

            if (getConfigSetting('AllowTransportation') == "True") {
                el("chkTransportationBit").checked = true;
            }

            if (getConfigSetting('GenerateXml') == "True") {
                if (!el('chkXML').checked) {
                    el("ITab1").TabPages[5].disabled = true;
                }
                else {
                    el("ITab1").TabPages[5].disabled = false;
                }
            }
        }
        el("chkActiveBit").checked = true;
        el("chkCheckDigitValidationBit").checked = false;
        el("ITab1").TabPages[3].disabled = false;
        el("lkpBillingType").value = "DAILY";
        setPageID("0");
        setPageMode("new");
        bindChargeDetail();
        BindTransportstionRental();
        setDefaultValuesonLoad();
        setDefaultValuesTransportation();
        hideMessage();
        el('lkpBulkEmailFormat').SelectedValues[0] = "53";
        //Bulk Email Default Formate 53- REGULAR , 54- COMPILED
        el('lkpBulkEmailFormat').value = getConfigSetting('DefaultBulkEmailFormat');
    }
    else {
        setDefaultValuesTransportation();
        showSubmitButton(true);
        resetValidatorByGroup("divGeneralInfo");
        resetValidatorByGroup("tabCustomer");
        resetValidatorByGroup("divStandardRates");
        resetValidatorByGroup("divCustomerRates");
        setReadOnly("txtCustomerCode", true);
        setPageMode("edit");
        bindChargeDetail();
        BindTransportstionRental();
    }
    if (getConfigSetting('050') != "True") {
        if (getConfigSetting('AllowTransportation') == "True") {
            if (!el('chkTransportationBit').checked) {
                el("ITab1").TabPages[3].disabled = true;
            }
            else {
                el("ITab1").TabPages[3].disabled = false;
            }
        }
        if (getConfigSetting('AllowRental') == "True") {
            if (!el('chkRentalBit').checked) {
                el("ITab1").TabPages[4].disabled = true;
            }
            else {
                el("ITab1").TabPages[4].disabled = false;
            }
        }

        if (getConfigSetting('GenerateXml') == "True") {
            el("ITab1").TabPages[5].style.display = "block";
            if (!el('chkXML').checked) {
                el("ITab1").TabPages[5].disabled = true;
            }
            else {
                el("ITab1").TabPages[5].disabled = false;
            }

        }
        else {
            if (getConfigSetting('AllowRental') == "True") {
                el("ITab1").TabPages[5].style.display = "none";
            }
            else {
                el("ITab1").TabPages[4].style.display = "none";
                el("ITab1").TabPages[3].style.display = "none";
            }


        }
    }
    //  el('lblDepotCuurency').innerText = "(" + el('hdnDepotCurrency').value + ")";
    setText(el('lblDepotCuurency'), "(" + el('hdnDepotCurrency').value + ")");

    //Finance Integration 
    if (getConfigSetting('FinanceIntegration') == "True" || getConfigSetting('FinanceIntegration') == "true") {
        //        alert("ON :" + getConfigSetting('FinanceIntegration'));
        showDiv("divlblLedger");
        showDiv("divtxtLedger");
    }
    else {
        //        alert("OFF :" + getConfigSetting('FinanceIntegration'));
        hideDiv("divlblLedger");
        hideDiv("divtxtLedger");
    }
    setText(el('lblDepotCuurency'), "(" + el('hdnDepotCurrency').value + ")");

}
function submitPageGWS() {
    if (Page_ClientValidate('divGeneralInfo')) {
        if (Page_ClientValidate('divCustomerRates')) {
            var count = ifgChargeDetail.Rows().Count;
            if (count == 0) {
                var _cols = ifgChargeDetail.Rows(0).GetClientColumns();
                ifgChargeDetail.Rows(0).SetColumnValuesByIndex(5, "0.00");
                ifgChargeDetail.Rows(0).SetColumnValuesByIndex(6, true);
                if (_cols[0] == "" || _cols[0] == null) {
                    el("ITab1").Select(el("ITab1").TabPages[1]);
                    showErrorMessage("Customer Rate is a must for atleast an Equipment Code and Type.");
                    return false;
                }
            }
            el("ITab1").Select(el("ITab1").TabPages[1]);
            if (!Page_ClientValidate('divLbrRate')) {
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
                    createCustomerGWS();
                }
                else if (sMode == MODE_EDIT) {
                    updateCustomerGWS();
                }
            }
            else {
                showInfoMessage('No Changes to Save');
                setFocus();
            }
        }
    }
}

function updateCustomerGWS() {
    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("bv_strCSTMR_CD", el("txtCustomerCode").value);
    oCallback.add("bv_strCSTMR_NAM", el("txtCustomerName").value);
    oCallback.add("bv_i64CSTMR_CRRNCY_ID", el("lkpCustomerCurrency").SelectedValues[0]);
    oCallback.add("bv_i64BLLNG_TYP_ID", el("lkpBillingType").SelectedValues[0]);
    oCallback.add("bv_i64BLK_EML_FRMT_ID", el("lkpBulkEmailFormat").SelectedValues[0]);
    oCallback.add("bv_strCNTCT_PRSN_NAM", el("txtContactPersonName").value);
    oCallback.add("bv_strCNTCT_ADDRSS", el("txtContactAddress").value);
    oCallback.add("bv_strBLLNG_ADDRSS", el("txtBillingAddress").value);
    oCallback.add("bv_strZP_CD", el("txtZipCode").value);
    oCallback.add("bv_strPHN_NO", el("txtPhoneNo").value);
    oCallback.add("bv_strFX_NO", el("txtFax").value);
    oCallback.add("bv_strRPRTNG_EML_ID", el("txtEmailforReporting").value);
    oCallback.add("bv_strINVCNG_EML_ID", el("txtEmailforInvoicing").value);
    oCallback.add("bv_strRPR_TCH_EML_ID", el("txtEmailforRepairTech").value);
    oCallback.add("bv_strEdiCode", el("txtEdiCode").value);
    oCallback.add("bv_blnCHK_DGT_VLDTN_BT", el("chkCheckDigitValidationBit").checked);
    oCallback.add("bv_blnACTV_BT", el("chkActiveBit").checked);
    oCallback.add("wfData", el("WFDATA").value);
    oCallback.add("bv_strCustVatNo", el("txtCustVatNo").value);
    oCallback.add("bv_strAgent", el("lkpAgent").SelectedValues[0]);
    oCallback.add("bv_strStorageTax", el("txtStorage_Tx_Rt").value);
    oCallback.add("bv_strServiceTax", el("txtSrvc_Tx_Rt").value);
    oCallback.add("bv_strHandlingTax", el("txtHndlng_Tx_Rt").value);
    oCallback.add("bv_strLaborRate", el("txtLaborRate").value);
    oCallback.invoke("Customer.aspx", "UpdateCustomerGWS");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindChargeDetail();
        resetHasChanges("ifgChargeDetail");
        setReadOnly("txtCustomerCode", true);
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function submitPage() {
    if (getConfigSetting('050') == "True") {
        submitPageGWS();
        return true;
    }
    else {
        if (Page_ClientValidate('divGeneralInfo', false)) {
            if (Page_ClientValidate('divStandardRates', false)) {
                if (Page_ClientValidate('divCustomerRates', false)) {
                    if (Page_ClientValidate('divTransportation', false)) {
                        if (Page_ClientValidate('divRental', false)) {
                            if (Page_ClientValidate('divFtp', false)) {
                                var count = ifgChargeDetail.Rows().Count;
                                if (count == 0) {
                                    var _cols = ifgChargeDetail.Rows(0).GetClientColumns();
                                    if (_cols[0] == "" || _cols[0] == null) {
                                        el("ITab1").Select(el("ITab1").TabPages[2]);
                                        showErrorMessage("Customer Rate is a must for atleast an Equipment Code and Type.");
                                        return false;
                                    }
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
                                    if (typeof (ifgCustomerRental) != 'undefined' && typeof (ifgCustomerRental) != 'unknown') {
                                        if (ifgCustomerRental.Submit() == false || _RowValidationFails) {
                                            return false;
                                        }
                                    }
                                    if (typeof (ifgCustomerTransportation) != 'undefined' && typeof (ifgCustomerTransportation) != 'unknown') {
                                        if (ifgCustomerTransportation.Submit() == false || _RowValidationFails) {
                                            return false;
                                        }
                                    }
                                    if (getConfigSetting('FinanceIntegration') == "True" || getConfigSetting('FinanceIntegration') == "true") {
                                        if (el("txtLedgerId").value == "") {
                                            showErrorMessage("Ledger Id Required");
                                            setFocusToField("txtLedgerId");
                                            return false;
                                        }
                                    }
                                    if (sMode == MODE_NEW) {
                                        createCustomer();
                                    }
                                    else if (sMode == MODE_EDIT) {
                                        updateCustomer();
                                    }
                                }
                                else {
                                    showInfoMessage('No Changes to Save');
                                    setFocus();
                                }
                            }
                        }
                    }
                }
            }
        }
        else {
            //if (pTab == "" || pTab == null) {
            pTab = 0;
            //   }
            el("ITab1").Select(el("ITab1").TabPages[0]);
        }
    }
}
function createCustomerGWS() {
    var oCallback = new Callback();
    oCallback.add("bv_strCSTMR_CD", el("txtCustomerCode").value);
    oCallback.add("bv_strCSTMR_NAM", el("txtCustomerName").value);
    oCallback.add("bv_i64CSTMR_CRRNCY_ID", el("lkpCustomerCurrency").SelectedValues[0]);
    oCallback.add("bv_i64BLLNG_TYP_ID", el("lkpBillingType").SelectedValues[0]);
    oCallback.add("bv_i64BLK_EML_FRMT_ID", el("lkpBulkEmailFormat").SelectedValues[0]);
    oCallback.add("bv_strCNTCT_PRSN_NAM", el("txtContactPersonName").value);
    oCallback.add("bv_strCNTCT_ADDRSS", el("txtContactAddress").value);
    oCallback.add("bv_strBLLNG_ADDRSS", el("txtBillingAddress").value);
    oCallback.add("bv_strZP_CD", el("txtZipCode").value);
    oCallback.add("bv_strPHN_NO", el("txtPhoneNo").value);
    oCallback.add("bv_strFX_NO", el("txtFax").value);
    oCallback.add("bv_strRPRTNG_EML_ID", el("txtEmailforReporting").value);
    oCallback.add("bv_strINVCNG_EML_ID", el("txtEmailforInvoicing").value);
    oCallback.add("bv_strRPR_TCH_EML_ID", el("txtEmailforRepairTech").value);
    oCallback.add("bv_strEdiCode", el("txtEdiCode").value);
    oCallback.add("bv_blnCHK_DGT_VLDTN_BT", el("chkCheckDigitValidationBit").checked);
    oCallback.add("bv_blnACTV_BT", el("chkActiveBit").checked);
    oCallback.add("bv_strCustVatNo", el("txtCustVatNo").value);
    oCallback.add("bv_strAgent", el("lkpAgent").SelectedValues[0]);
    oCallback.add("bv_strStorageTax", el("txtStorage_Tx_Rt").value);
    oCallback.add("bv_strServiceTax", el("txtSrvc_Tx_Rt").value);
    oCallback.add("bv_strHandlingTax", el("txtHndlng_Tx_Rt").value);
    oCallback.add("bv_strLaborRate", el("txtLaborRate").value);
    oCallback.add("wfData", el("WFDATA").value);

    oCallback.invoke("Customer.aspx", "CreateCustomerGWS");

    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setPageID(oCallback.getReturnValue("ID"));
        bindChargeDetail();
        resetHasChanges("ifgChargeDetail");
        setReadOnly("txtCustomerCode", true);
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
}
function BindTransportstionRental() {
    if (getConfigSetting('AllowTransportation') == "True") {
        //showDiv("divTransportation");
        bindCustomerTransportation();
    }
    else {
        hideDiv("divTransportation");
    }
    if (getConfigSetting('AllowRental') == "True") {
        //        showDiv("divRental");
        bindCustomerRental();
    }
    else {
        hideDiv("divRental");
        hideDiv("tabRental");
    }
}

function bindChargeDetail() {
    var ifgChargeDetail = new ClientiFlexGrid("ifgChargeDetail");
    ifgChargeDetail.parameters.add("WFDATA", el("WFDATA").value);
    ifgChargeDetail.parameters.add("CustomerID", getPageID());
    ifgChargeDetail.DataBind();
    $('.btncorner').corner();
}

function bindCustomerTransportation() {
    var ifgCustomerTransportation = new ClientiFlexGrid("ifgCustomerTransportation");
    ifgCustomerTransportation.parameters.add("WFDATA", el("WFDATA").value);
    ifgCustomerTransportation.parameters.add("CustomerID", getPageID());
    ifgCustomerTransportation.DataBind();
    $('.btncorner').corner();
}

function bindCustomerRental() {
    var ifgCustomerRental = new ClientiFlexGrid("ifgCustomerRental");
    ifgCustomerRental.parameters.add("WFDATA", el("WFDATA").value);
    ifgCustomerRental.parameters.add("CustomerID", getPageID());
    ifgCustomerRental.DataBind();
    $('.btncorner').corner();
}

function createCustomer() {
    var oCallback = new Callback();
    oCallback.add("bv_strCSTMR_CD", el("txtCustomerCode").value);
    oCallback.add("bv_strCSTMR_NAM", el("txtCustomerName").value);
    oCallback.add("bv_i64CSTMR_CRRNCY_ID", el("lkpCustomerCurrency").SelectedValues[0]);
    oCallback.add("bv_i64BLLNG_TYP_ID", el("lkpBillingType").SelectedValues[0]);
    oCallback.add("bv_i64BLK_EML_FRMT_ID", el("lkpBulkEmailFormat").SelectedValues[0]);
    oCallback.add("bv_strCNTCT_PRSN_NAM", el("txtContactPersonName").value);
    oCallback.add("bv_strCNTCT_ADDRSS", el("txtContactAddress").value);
    oCallback.add("bv_strBLLNG_ADDRSS", el("txtBillingAddress").value);
    oCallback.add("bv_strZP_CD", el("txtZipCode").value);
    oCallback.add("bv_strPHN_NO", el("txtPhoneNo").value);
    oCallback.add("bv_strFX_NO", el("txtFax").value);
    oCallback.add("bv_strRPRTNG_EML_ID", el("txtEmailforReporting").value);
    oCallback.add("bv_strINVCNG_EML_ID", el("txtEmailforInvoicing").value);
    oCallback.add("bv_strRPR_TCH_EML_ID", el("txtEmailforRepairTech").value);
    oCallback.add("bv_decHYDR_AMNT_NC", el("txtHydroAmount").value);
    oCallback.add("bv_decPNMTC_AMNT_NC", el("txtPneumaticAmount").value);
    oCallback.add("bv_decLBR_RT_PR_HR_NC", el("txtLaborRateperHour").value);
    oCallback.add("bv_decLK_TST_RT_NC", el("txtLeakTestRate").value);
    oCallback.add("bv_decSRVY_ONHR_OFFHR_RT_NC", el("txtSurveyRate").value);
    oCallback.add("bv_i64PRDC_TST_TYP_ID", el("lkpPeriodicTestType").SelectedValues[0]);
    // oCallback.add("bv_decVLDTY_PRD_TST_YRS", el("lblValidityPeriodTest").innerText);
    oCallback.add("bv_decVLDTY_PRD_TST_YRS", getText(el('lblValidityPeriodTest')));
    oCallback.add("bv_decMIN_HTNG_RT_NC", el("txtMinHeatingRate").value);
    oCallback.add("bv_decMIN_HTNG_PRD_NC", el("txtMinHeatingPeriod").value);
    oCallback.add("bv_decHRLY_CHRG_NC", el("txtHourlyCharge").value);
    oCallback.add("bv_strEdiCode", el("txtEdiCode").value);
    if (getConfigSetting('050') != "True") {
        if (getConfigSetting('AllowTransportation') == "True") {
            oCallback.add("bv_blnTRNSPRTTN_BT", el("chkTransportationBit").checked);
        }
        else {
            oCallback.add("bv_blnTRNSPRTTN_BT", false);
        }
        if (getConfigSetting('AllowRental') == "True") {
            oCallback.add("bv_blnRNTL_BT", el("chkRentalBit").checked);
        }
        else {
            oCallback.add("bv_blnRNTL_BT", false);
        }
        if (getConfigSetting('GenerateXml') == "True") {
            if (el('chkXML').checked && ((el('txtFtpServer').value == "" || el('txtFtpUserName').value == "" || el('txtPassword').value == ""))) {
                showErrorMessage("FTP Server Details Required");
                el("ITab1").Select(el("ITab1").TabPages[5]);
                return false;
            }
            oCallback.add("bv_blnXML_BT", el("chkXML").checked);
            oCallback.add("bv_strServerUrl", el("txtFtpServer").value);
            oCallback.add("bv_strServerName", el("txtFtpUserName").value);
            oCallback.add("bv_strPassword", el("txtPassword").value);

        }
        else {
            oCallback.add("bv_blnXML_BT", false);

        }
    }
    oCallback.add("bv_blnCHK_DGT_VLDTN_BT", el("chkCheckDigitValidationBit").checked);
    oCallback.add("bv_blnACTV_BT", el("chkActiveBit").checked);

    //Finance Integration
    if (getConfigSetting('050') != "True") {
        if (getConfigSetting('FinanceIntegration') == "True" || getConfigSetting('FinanceIntegration') == "true") {
            oCallback.add("FinanceIntegrationBit", true);
            oCallback.add("bv_LedgerId", el("txtLedgerId").value);
        }
        else {
            oCallback.add("FinanceIntegrationBit", false);
            oCallback.add("bv_LedgerId", el("txtLedgerId").value);
        }
    }

    //Customer Master Changes
    if (el("chkShell").checked = "false") {
        el("chkShell").checked = false;
    }
    if (el("ChkSTube").checked = "false") {
        el("ChkSTube").checked = false;
    }
    oCallback.add("bv_blnShell", el("chkShell").checked);
    oCallback.add("bv_blnSTube", el("ChkSTube").checked);

    oCallback.add("wfData", el("WFDATA").value);

    oCallback.invoke("Customer.aspx", "CreateCustomer");

    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setPageID(oCallback.getReturnValue("ID"));
        bindChargeDetail();
        bindCustomerTransportation();
        bindCustomerRental();
        resetHasChanges("ifgChargeDetail");
        if (getConfigSetting('050') != "True") {
            resetHasChanges("ifgCustomerTransportation");
            resetHasChanges("ifgCustomerRental");
        }
        setReadOnly("txtCustomerCode", true);
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}


function updateCustomer() {
    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("bv_strCSTMR_CD", el("txtCustomerCode").value);
    oCallback.add("bv_strCSTMR_NAM", el("txtCustomerName").value);
    oCallback.add("bv_i64CSTMR_CRRNCY_ID", el("lkpCustomerCurrency").SelectedValues[0]);
    oCallback.add("bv_i64BLLNG_TYP_ID", el("lkpBillingType").SelectedValues[0]);
    oCallback.add("bv_i64BLK_EML_FRMT_ID", el("lkpBulkEmailFormat").SelectedValues[0]);
    oCallback.add("bv_strCNTCT_PRSN_NAM", el("txtContactPersonName").value);
    oCallback.add("bv_strCNTCT_ADDRSS", el("txtContactAddress").value);
    oCallback.add("bv_strBLLNG_ADDRSS", el("txtBillingAddress").value);
    oCallback.add("bv_strZP_CD", el("txtZipCode").value);
    oCallback.add("bv_strPHN_NO", el("txtPhoneNo").value);
    oCallback.add("bv_strFX_NO", el("txtFax").value);
    oCallback.add("bv_strRPRTNG_EML_ID", el("txtEmailforReporting").value);
    oCallback.add("bv_strINVCNG_EML_ID", el("txtEmailforInvoicing").value);
    oCallback.add("bv_strRPR_TCH_EML_ID", el("txtEmailforRepairTech").value);
    oCallback.add("bv_decHYDR_AMNT_NC", el("txtHydroAmount").value);
    oCallback.add("bv_decPNMTC_AMNT_NC", el("txtPneumaticAmount").value);
    oCallback.add("bv_decLBR_RT_PR_HR_NC", el("txtLaborRateperHour").value);
    oCallback.add("bv_decLK_TST_RT_NC", el("txtLeakTestRate").value);
    oCallback.add("bv_decSRVY_ONHR_OFFHR_RT_NC", el("txtSurveyRate").value);
    oCallback.add("bv_i64PRDC_TST_TYP_ID", el("lkpPeriodicTestType").SelectedValues[0]);
    oCallback.add("bv_decVLDTY_PRD_TST_YRS", getText(el('lblValidityPeriodTest')));
    //oCallback.add("bv_decVLDTY_PRD_TST_YRS", el("lblValidityPeriodTest").innerText);
    oCallback.add("bv_decMIN_HTNG_RT_NC", el("txtMinHeatingRate").value);
    oCallback.add("bv_decMIN_HTNG_PRD_NC", el("txtMinHeatingPeriod").value);
    oCallback.add("bv_decHRLY_CHRG_NC", el("txtHourlyCharge").value);
    oCallback.add("bv_strEdiCode", el("txtEdiCode").value);
    if (getConfigSetting('050') != "True") {
        if (getConfigSetting('AllowTransportation') == "True") {
            oCallback.add("bv_blnTRNSPRTTN_BT", el("chkTransportationBit").checked);
        }
        else {
            oCallback.add("bv_blnTRNSPRTTN_BT", false);
        }
        if (getConfigSetting('AllowRental') == "True") {
            oCallback.add("bv_blnRNTL_BT", el("chkRentalBit").checked);
        }
        else {
            oCallback.add("bv_blnRNTL_BT", false);
        }
        if (getConfigSetting('GenerateXml') == "True") {
            if (el('chkXML').checked && ((el('txtFtpServer').value == "" || el('txtFtpUserName').value == "" || el('txtPassword').value == ""))) {
                showErrorMessage("FTP Server Details Required");
                el("ITab1").Select(el("ITab1").TabPages[5]);
                return false;
            }
            oCallback.add("bv_blnXML_BT", el("chkXML").checked);
            oCallback.add("bv_strServerUrl", el("txtFtpServer").value);
            oCallback.add("bv_strServerName", el("txtFtpUserName").value);
            oCallback.add("bv_strPassword", el("txtPassword").value);

        }
        else {
            oCallback.add("bv_blnXML_BT", false);
        }
    }
    oCallback.add("bv_blnCHK_DGT_VLDTN_BT", el("chkCheckDigitValidationBit").checked);
    oCallback.add("bv_blnACTV_BT", el("chkActiveBit").checked);

    //Finance Integration
    if (getConfigSetting('050') != "True") {
        if (getConfigSetting('FinanceIntegration') == "True" || getConfigSetting('FinanceIntegration') == "true") {
            oCallback.add("FinanceIntegrationBit", true);
            oCallback.add("bv_LedgerId", el("txtLedgerId").value);
        }
        else {
            oCallback.add("FinanceIntegrationBit", false);
            oCallback.add("bv_LedgerId", el("txtLedgerId").value);
        }
    }
    //Customer Master Changes
    oCallback.add("bv_blnShell", el("chkShell").checked);
    oCallback.add("bv_blnSTube", el("ChkSTube").checked);

    oCallback.add("wfData", el("WFDATA").value);


    oCallback.invoke("Customer.aspx", "UpdateCustomer");

    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindChargeDetail();
        bindCustomerTransportation();
        bindCustomerRental();
        resetHasChanges("ifgChargeDetail");
        if (getConfigSetting('050') != "True") {
            resetHasChanges("ifgCustomerTransportation");
            resetHasChanges("ifgCustomerRental");
        }
        setReadOnly("txtCustomerCode", true);
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
        setFocusToField("txtCustomerCode");
    }
    else if (sMode == MODE_EDIT) {
        setFocusToField("txtCustomerName");
    }
}

function validateCustomerCode(oSrc, args) {
    var oCallback = new Callback();
    var valid;
    oCallback.add("Code", el("txtCustomerCode").value);

    oCallback.invoke("Customer.aspx", "ValidateCode");

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

function setDefaultValuesTransportation(iCurrentIndex) {
    if (typeof (ifgCustomerTransportation) != "undefined") {
        if (ifgCustomerTransportation.Rows().Count == "0") {
            ifgCustomerTransportation.Rows(0).SetColumnValuesByIndex(3, "0.00");
            ifgCustomerTransportation.Rows(0).SetColumnValuesByIndex(4, "0.00");
        }
        resetHasChanges("ifgCustomerTransportation");
    }
}

function resetGridChanges() {
    resetHasChanges("ifgChargeDetail");
}

function parentOnBeforeCallBack(mode, param) {
    if (mode == "Update" || mode == "Insert" || mode == "Delete") {
        param.add("mode", getPageMode());
        param.add("CustomerID", getPageID());
    }
}

function childOnBeforeCallBack(mode, param) {
    var rIndex, _cols;
    rIndex = ifgStorageDetail.CurrentRowIndex();
    _cols = ifgStorageDetail.Rows(rIndex).Columns();
    if (mode == "Update" || mode == "Insert" || mode == "Delete") {
        param.add("CustomerChargeDetailID", el('hdnCustomerChargeDetailID').value);
        param.add("CustomerID", getPageID());
        param.add("mode", getPageMode());
        if (mode == "Delete") {
            param.add("CleaningDetail", rIndex);
        }
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

function onClientExpandList(rIndex, param) {
    param.add("RowIndex", rIndex);
    rIndex = ifgChargeDetail.CurrentRowIndex();
    var _cols = ifgChargeDetail.Rows(rIndex).Columns();
    if (_cols[0] != null) {
        param.add("CustomerChargeDetailID", _cols[0]);
        param.add("CustomerID", getPageID());
        el('hdnCustomerChargeDetailID').value = _cols[0];
    }
}
function onClientExpand(rIndex, param) {
    param.add("RowIndex", rIndex);
    el('hdnChargeRowIndex').value = rIndex;
    rIndex = ifgChargeDetail.CurrentRowIndex();
    var _cols = ifgChargeDetail.Rows(rIndex).Columns();
    if (_cols[0] != null && _cols[1]) {
        param.add("CustomerChargeDetailID", _cols[0]);
        param.add("CustomerID", getPageID());
        el('hdnCustomerChargeDetailID').value = _cols[0];
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
    reSizePane();
    tabID = obj;
    pTab = obj;
    return false;
}

function onBeforeCustomerRentalCallBack(mode, param) {
    if (mode == "Add" || mode == "Edit" || mode == "Delete") {
        param.add("CustomerName", el('txtCustomerName').value);
    }
}

function onAfterCustomerRentalCallBack(param, mode) {
    if (typeof (param["Delete"]) != 'undefined') {
        showWarningMessage(param["Delete"]);
    }
    if (typeof (param["CheckDefault"]) != "undefined") {
        showWarningMessage(param["CheckDefault"]);
    }
}
// Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :57
function showValidityYear() {
    var periodicTestType = "";
    periodicTestType = el("lkpPeriodicTestType").SelectedValues[1];
    if (periodicTestType.toUpperCase() == "HYDRO") {
        setText(el('lblValidityPeriodTest'), "5");
        //  el('lblValidityPeriodTest').innerText = "5";
    }
    else if (periodicTestType.toUpperCase() == "PNEUMATIC") {
        setText(el('lblValidityPeriodTest'), "2.5");
        // el('lblValidityPeriodTest').innerText = "2.5";
    }
    else if (periodicTestType == "" || periodicTestType == null) {
        setText(el('lblValidityPeriodTest'), "0");
        // el('lblValidityPeriodTest').innerText = "0";
    }
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

function openEmailSetting() {
    //   Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    } else {
        showModalWindow("Email Setting", "Masters/CustomerEmailSetting.aspx?CustomerId=" + getPageID(), "990px", "250px", "0px", "", "");
        HasChanges = true;
        psc().hideLoader();
    }
}

//This function is to Validate the Equipment No for Duplicates
function validateRouteCode(oSrc, args) {
    var rIndex = ifgCustomerTransportation.VirtualCurrentRowIndex();
    var cols = ifgCustomerTransportation.Rows(rIndex).GetClientColumns();
    var i;
    var chargeCode = cols[0];
    var icount = ifgCustomerTransportation.Rows().Count
    var colscheck;

    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgCustomerTransportation.Rows(i).GetClientColumns();
                if (chargeCode == colscheck[0]) {
                    args.IsValid = false;
                    oSrc.errormessage = "This Code already exists";
                    return;
                }
            }
        }
    }
}

//This function is to Validate the Equipment No for Duplicates
function validateContractNo(oSrc, args) {
    var sCols = ifgCustomerRental.Rows(ifgCustomerRental.CurrentRowIndex()).Columns();
    var sCode = sCols[4];
    var cols = ifgCustomerRental.Rows(ifgCustomerRental.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgCustomerRental.rowIndex;
    var routeCode = args.Value
    var rowState = ifgCustomerRental.ClientRowState();
    if (rowState != 'Added') {
        if (sCode == cols[0]) {
            return false;
        }
    }
    var oCallback = new Callback();
    oCallback.add("ContractNo", routeCode);
    oCallback.add("GridIndex", ifgCustomerRental.VirtualCurrentRowIndex());
    oCallback.add("RowState", rowState);
    oCallback.add("CustomerID", getPageID());
    oCallback.invoke("Customer.aspx", "validateContractNo");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("bNotExists") == "true") {
            args.IsValid = true;
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "This Contract Reference Number already Exist";
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;

}

function setDefaultRental(iCurrentIndex) {
    var sRowState = ifgCustomerRental.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgCustomerRental.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
        ifgCustomerRental.Rows(iCurrentIndex).SetReadOnlyColumn(1, false);
        ifgCustomerRental.Rows(iCurrentIndex).SetReadOnlyColumn(2, false);
        ifgCustomerRental.Rows(iCurrentIndex).SetReadOnlyColumn(3, false);
        ifgCustomerRental.Rows(iCurrentIndex).SetReadOnlyColumn(4, false);
        ifgCustomerRental.Rows(iCurrentIndex).SetReadOnlyColumn(5, false);
        ifgCustomerRental.Rows(iCurrentIndex).SetReadOnlyColumn(6, false);
        ifgCustomerRental.Rows(iCurrentIndex).SetReadOnlyColumn(7, false);
        ifgCustomerRental.Rows(iCurrentIndex).SetReadOnlyColumn(8, false);
        ifgCustomerRental.Rows(iCurrentIndex).SetReadOnlyColumn(9, false);
    }
}

function setDefaultTransportation(iCurrentIndex) {
    var sRowState = ifgCustomerTransportation.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgCustomerTransportation.Rows(iCurrentIndex).SetColumnValuesByIndex(3, "0.00");
        ifgCustomerTransportation.Rows(iCurrentIndex).SetColumnValuesByIndex(4, "0.00");
    }

}


function validateStartDate(oSrc, args) {
    var cols = ifgCustomerRental.Rows(ifgCustomerRental.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgCustomerRental.rowIndex;
    var startDate = args.Value
    //UIG Fix
    if (cols[2] != '' && cols[2] != null && cols[2] != 'undefined') {
        if (DateCompareEqual(cols[2], startDate) == false) {
            args.IsValid = false;
            showErrorMessage("Contract Start Date should be less than Contract End Date (" + cols[2] + ")");
            return false;
        }
        else {
            args.IsValid = true;
        }
    }
    resetValidators();
}

function validateEndDate(oSrc, args) {
    var cols = ifgCustomerRental.Rows(ifgCustomerRental.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgCustomerRental.rowIndex;
    var endDate = args.Value
    //UIG Fix
    if (cols[1] != '' || cols[2] != null) {
        if (DateCompareEqual(endDate, cols[1]) == false) {
            args.IsValid = false;
            showErrorMessage("Contract End Date should be greater than Contract Start Date (" + cols[1] + ")");
            return false;
        }
        else {
            args.IsValid = true;
        }
    }
    resetValidators();
}

function onClientCustomer(oSrc, args) {
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

function onClientRouteChange(obj) {
    var oCallback = new Callback();
    oCallback.add("RouteId", obj.SelectedValues[0]);
    oCallback.invoke("Customer.aspx", "getTripRate");
    if (oCallback.getCallbackStatus()) {
        ifgCustomerTransportation.Rows(ifgCustomerTransportation.rowIndex).SetColumnValuesByIndex(3, parseFloat(oCallback.getReturnValue("EmptyTripRate")).toFixed(2));
        ifgCustomerTransportation.Rows(ifgCustomerTransportation.rowIndex).SetColumnValuesByIndex(4, parseFloat(oCallback.getReturnValue("FullTripRate")).toFixed(2));
        return true;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
}

function checkTransportation(obj) {
    if (obj.checked) {
        el("ITab1").TabPages[3].disabled = false;
    }
    else {
        if (!Page_IsValid) {
            el('chkTransportationBit').checked = true;
            return false;
        }
        el("ITab1").TabPages[3].disabled = true;
        el("ITab1").Select(el("ITab1").TabPages[0]);
    }
    HasChanges = true;
}

function checkRental(obj) {
    if (obj.checked) {
        el("ITab1").TabPages[4].disabled = false;
    }
    else {
        if (!Page_IsValid) {
            el('chkRentalBit').checked = true;
            return false;
        }
        el("ITab1").TabPages[4].disabled = true;
        el("ITab1").Select(el("ITab1").TabPages[0]);
    }
    HasChanges = true;
}
$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window.parent).height() < 768) {
        el("tabCustomer").style.height = $(window.parent).height() - 373 + "px";
    }
    else if ($(window.parent).height() < 799) {
        el("tabCustomer").style.height = $(window.parent).height() - 468 + "px";
    }
    else {
        el("tabCustomer").style.height = $(window.parent).height() - 483 + "px";
        el("divGeneralInfo").style.height = $(window.parent).height() - 612 + "px";
        if (el("divStandardRates") != null) {
            el("divStandardRates").style.height = $(window.parent).height() - 612 + "px";
        }
        el("divCustomerRates").style.height = $(window.parent).height() - 612 + "px";
        el("divTransportation").style.height = $(window.parent).height() - 612 + "px";
        el("divRental").style.height = $(window.parent).height() - 612 + "px";
        el("divFtp").style.height = $(window.parent).height() - 612 + "px";
        if (el("ifgChargeDetail") != null) {
            el("ifgChargeDetail").SetStaticHeaderHeight($(window.parent).height() - 650 + "px");
        }
        if (el("ifgCustomerTransportation") != null) {
            el("ifgCustomerTransportation").SetStaticHeaderHeight($(window.parent).height() - 650 + "px");
        }
        if (el("ifgCustomerRental") != null) {
            el("ifgCustomerRental").SetStaticHeaderHeight($(window.parent).height() - 650 + "px");
        }
    }

}

function checkFtp(obj) {
    if (obj.checked) {
        el("ITab1").TabPages[5].disabled = false;
    }
    else {
        if (!Page_IsValid) {
            el('chkXML').checked = true;
            return false;
        }
        el("ITab1").TabPages[5].disabled = true;
        el("ITab1").Select(el("ITab1").TabPages[0]);
    }
    HasChanges = true;
}


function formatHandlingRate() {
    var Amount = new Number;
    if (isNaN(parseFloat(el("txtHndlng_Tx_Rt").value))) {
        el("txtHndlng_Tx_Rt").value = 0;
    }
    Amount = parseFloat(el("txtHndlng_Tx_Rt").value);
    el("txtHndlng_Tx_Rt").value = Amount.toFixed(2);
}

function formatStorageRate() {
    var Amount = new Number;
    if (isNaN(parseFloat(el("txtStorage_Tx_Rt").value))) {
        el("txtStorage_Tx_Rt").value = 0;
    }
    Amount = parseFloat(el("txtStorage_Tx_Rt").value);
    el("txtStorage_Tx_Rt").value = Amount.toFixed(2);
}
function formatServiceRate() {
    var Amount = new Number;
    if (isNaN(parseFloat(el("txtSrvc_Tx_Rt").value))) {
        el("txtSrvc_Tx_Rt").value = 0;
    }
    Amount = parseFloat(el("txtSrvc_Tx_Rt").value);
    el("txtSrvc_Tx_Rt").value = Amount.toFixed(2);
}
function formatLaborRate(oSrc, args) {
    var Amount = new Number;
    if (isNaN(parseFloat(el("txtLaborRate").value))) {
        el("txtLaborRate").value = 0;
    }
    Amount = parseFloat(el("txtLaborRate").value);
    el("txtLaborRate").value = Amount.toFixed(2);
    oSrc.errormessage = "";
    args.IsValid = true;
}

//function validateFtpServer() {
//    if (el('chkXML').checked && el('txtFtpServer').value == "") {
//        showErrorMessage("FTP Server Required");
////        el("ITab1").Select(el("ITab1").TabPages[5]);
//        return false;
//        }
//    
//}



//Set Equipment Code
function setEquipmentCode(oSrc, args) {

    var cols = ifgChargeDetail.Rows(ifgChargeDetail.CurrentRowIndex()).GetClientColumns();
    var oCallback = new Callback();
    oCallback.add("TypeID", cols[1]);
    oCallback.invoke("Customer.aspx", "GetEquipmentCodebyTypeId");

    if (oCallback.getCallbackStatus()) {
        ifgChargeDetail.Rows(ifgChargeDetail.CurrentRowIndex()).SetColumnValuesByIndex(1, new Array(args.Value, oCallback.getReturnValue("ID")));
        ifgChargeDetail.Rows(ifgChargeDetail.CurrentRowIndex()).SetColumnValuesByIndex(2, oCallback.getReturnValue("Code"));
    }
    else {
        var oCallback = new Callback();
        oCallback.add("Type", args.Value);
        oCallback.invoke("Customer.aspx", "GetEquipmentCode");

        if (oCallback.getCallbackStatus()) {
            ifgChargeDetail.Rows(ifgChargeDetail.CurrentRowIndex()).SetColumnValuesByIndex(1, new Array(args.Value, oCallback.getReturnValue("ID")));
            ifgChargeDetail.Rows(ifgChargeDetail.CurrentRowIndex()).SetColumnValuesByIndex(2, oCallback.getReturnValue("Code"));
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Please Configure Equipment Type & Code";
        }
    }
    //    if (cols[1] != "" || cols[1] != 'undefined') {
    //        //        //        ifgChargeDetail.Rows(ifgChargeDetail.CurrentRowIndex()).SetColumnValuesByIndex(3, getConfigSetting('DefaultEquipmentCode'));



    //        var oCallback = new Callback();
    //        oCallback.add("Type", args.Value);
    //        oCallback.invoke("Customer.aspx", "GetEquipmentCode");

    //        if (oCallback.getCallbackStatus()) {
    //            ifgChargeDetail.Rows(ifgChargeDetail.CurrentRowIndex()).SetColumnValuesByIndex(1, new Array(args.Value, oCallback.getReturnValue("ID")));
    //            ifgChargeDetail.Rows(ifgChargeDetail.CurrentRowIndex()).SetColumnValuesByIndex(2, oCallback.getReturnValue("Code"));
    //        }
    //        else {
    //            args.IsValid = false;
    //            oSrc.errormessage = "Please Configure Equipment Type & Code";
    //        }
    //    }
    //    else {

    //        var oCallback = new Callback();
    //        oCallback.add("TypeID", cols[1]);
    //        oCallback.invoke("Customer.aspx", "GetEquipmentCodebyTypeId");

    //        if (oCallback.getCallbackStatus()) {
    //            ifgChargeDetail.Rows(ifgChargeDetail.CurrentRowIndex()).SetColumnValuesByIndex(1, new Array(args.Value, oCallback.getReturnValue("ID")));
    //            ifgChargeDetail.Rows(ifgChargeDetail.CurrentRowIndex()).SetColumnValuesByIndex(2, oCallback.getReturnValue("Code"));
    //        }
    //        else {
    //            args.IsValid = false;
    //            oSrc.errormessage = "Please Configure Equipment Type & Code";
    //        }
    //    }
}

function OnListCollapse() {

}

function validateUpToContainers(oSrc, args) {
    var strValue = args.Value;
    if (strValue.indexOf('.') >= 0) {
        args.IsValid = false;
        oSrc.errormessage = "Upto Containers must be a whole number";
    }
    else {
        var rIndex, _cols, FromCntrs, ToCntrs, count;
        rIndex = ifgStorageDetail.CurrentRowIndex();
        _cols = ifgStorageDetail.Rows(rIndex).GetClientColumns();
        count = parseInt(ifgStorageDetail.Rows().Count);
        ToCntrs = _cols[0];
        if (ToCntrs != "") {
            if (count > 0) {
                _cols = ifgStorageDetail.Rows(parseInt(rIndex) - 1).GetClientColumns();
                FromCntrs = _cols[0];
                if (Number(ToCntrs) <= Number(FromCntrs)) {
                    oSrc.errormessage = "Upto Containers Value should be greater than the Previous Upto Containers Value";
                    args.IsValid = false;
                    return false;
                }
                else {
                    if (rIndex < count) {
                        if (ifgStorageDetail.rC > parseInt(rIndex) + 1) {
                            _cols = ifgStorageDetail.Rows(parseInt(rIndex) + 1).GetClientColumns();
                            FromCntrs = _cols[0];
                            if (Number(ToCntrs) >= Number(FromCntrs)) {
                                oSrc.errormessage = "Upto Containers Value should be Less than the Next Upto Containers Value";
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
    var rIndex, _cols;
    rIndex = ifgStorageDetail.CurrentRowIndex();
    _cols = ifgStorageDetail.Rows(rIndex).GetClientColumns();
    if (_cols[1] != null) {
        if (_cols[0] == "" && _cols[1] != "") {
            args.IsValid = false;
            oSrc.errormessage = "UpTo Containers Required";
        }
    }
}

function setRequiredForCleaningRate(oSrc, args) {
    var rIndex, _cols;
    rIndex = ifgStorageDetail.CurrentRowIndex();
    _cols = ifgStorageDetail.Rows(rIndex).GetClientColumns();
    if (_cols[0] != null) {
        if (_cols[1] == "" && _cols[0] != "") {
            args.IsValid = false;
            oSrc.errormessage = "Cleaning Required";
        }
    }
}