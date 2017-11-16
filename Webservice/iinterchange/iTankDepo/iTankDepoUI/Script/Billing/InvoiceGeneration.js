var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgHandlingStorage', 'ITab1_0;ifgHeating', 'ITab1_0;ifgCleaning', 'ITab1_0;ifgRepair', 'ITab1_0;ifgMiscellaneous', 'ITab1_0;ifgTransportation', 'ITab1_0;ifgCreditNote');
var _RowValidationFails = false;
var strInvoiceNo = "";
var strInvoiceFileName = "";
var intDepotID;


function initPage(sMode) {
   
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    if (getConfigSetting('070') != "True") {
        el("lblDepotCode").style.display = "none";
        el("lblDepotCodeReq").style.display = "none";
        el("divLkpDepot").style.display = "none";
    }
    else {
          if(el("hdnIsHeadQuarters").value=="false") {
             setReadOnly('lkpDepotCode', true);
          }
    }
}

function fetchInvoice() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    // Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :57
    if (getText(el('btnSubmit')) == "Fetch") {
        setText(el('btnSubmit'), "Reset");
//        el('btnSubmit').innerText = "Reset"
        if (el('lkpInvoiceType').value != '') {
            if (el('lkpInvoiceType').SelectedValues[0] == "78") {//Handling and Storage Type
                //  bindGrid("ifgHandlingStorage", el('lkpServicePartner').SelectedValues[0], el('lblFromDate').innerText, el('datPeriodTo').value, el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                bindGrid("ifgHandlingStorage", el('lkpServicePartner').SelectedValues[0], getText(el('lblFromDate')), el('datPeriodTo').value, el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                var rowCount = ifgHandlingStorage.Rows().Count;
                if (rowCount > 0) {
                    el("divHandlingStorage").style.display = "block";
                    el("divHeader").style.display = "block";
                    el("divFooter").style.display = "block";
                    setText(el('lblInvoiceTypeName'), el('lkpInvoiceType').value);
                    //el("lblInvoiceTypeName").innerText = el('lkpInvoiceType').value;
                    el("divRecordNotFound").style.display = "none";
                }
                else {
                    el("divRecordNotFound").style.display = "block";
                    el("divHandlingStorage").style.display = "none";
                    el("divHeader").style.display = "none";
                    el("divFooter").style.display = "none";
                    setText(el('lblInvoiceTypeName'), "");
                    // el("lblInvoiceTypeName").innerText = "";
                }
            }
            else if (el('lkpInvoiceType').SelectedValues[0] == "79") {//Heating 
                bindGrid("ifgHeating", el('lkpServicePartner').SelectedValues[0], el('datPeriodFrom').value, el('datPeriodTo').value, el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                var rowCount = ifgHeating.Rows().Count;
                if (rowCount > 0) {
                    el("divHeating").style.display = "block";
                    el("divHeader").style.display = "block";
                    el("divFooter").style.display = "block";
                    setText(el('lblInvoiceTypeName'), el('lkpInvoiceType').value);
                    //  el("lblInvoiceTypeName").innerText = el('lkpInvoiceType').value;
                    el("divRecordNotFound").style.display = "none";
                }
                else {
                    el("divRecordNotFound").style.display = "block";
                    el("divHeader").style.display = "none";
                    el("divFooter").style.display = "none";
                    setText(el('lblInvoiceTypeName'), "");
                    // el("lblInvoiceTypeName").innerText = "";
                    el("divHeating").style.display = "none";
                }
            }
            else if (el('lkpInvoiceType').SelectedValues[0] == "80") {//Cleaning
                if (getConfigSetting('073') != "True") {
                    bindGrid("ifgCleaning", el('lkpServicePartner').SelectedValues[0], el('datPeriodFrom').value, el('datPeriodTo').value, el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                    var rowCount = ifgCleaning.Rows().Count;
                    if (rowCount > 0) {
                        el("divCleaning").style.display = "block";
                        el("divHeader").style.display = "block";
                        el("divFooter").style.display = "block";
                        setText(el('lblInvoiceTypeName'), el('lkpInvoiceType').value);
                        //el("lblInvoiceTypeName").innerText = el('lkpInvoiceType').value;
                        el("divRecordNotFound").style.display = "none";
                    }
                    else {
                        el("divRecordNotFound").style.display = "block";
                        el("divCleaning").style.display = "none";
                        el("divHeader").style.display = "none";
                        el("divFooter").style.display = "none";
                        setText(el('lblInvoiceTypeName'), "");
                        //el("lblInvoiceTypeName").innerText = "";
                    }
                }
                else {
                    bindGrid("ifgCleaning", el('lkpServicePartner').SelectedValues[0], getText(el('lblFromDate')), getText(el('lblToDate')), el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                    var rowCount = ifgCleaning.Rows().Count;
                    if (rowCount > 0) {
                        el("divCleaning").style.display = "block";
                        el("divHeader").style.display = "block";
                        el("divFooter").style.display = "block";
                        setText(el('lblInvoiceTypeName'), el('lkpInvoiceType').value);
                        //el("lblInvoiceTypeName").innerText = el('lkpInvoiceType').value;
                        el("divRecordNotFound").style.display = "none";
                    }
                    else {
                        el("divRecordNotFound").style.display = "block";
                        el("divCleaning").style.display = "none";
                        el("divHeader").style.display = "none";
                        el("divFooter").style.display = "none";
                        setText(el('lblInvoiceTypeName'), "");
                        //el("lblInvoiceTypeName").innerText = "";
                    }
                }
            }
            else if (el('lkpInvoiceType').SelectedValues[0] == "81") {//Repair
                bindGrid("ifgRepair", el('lkpServicePartner').SelectedValues[0], el('datPeriodFrom').value, el('datPeriodTo').value, el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                var rowCount = ifgRepair.Rows().Count;
                if (rowCount > 0) {
                    el("divRepair").style.display = "block";
                    el("divRecordNotFound").style.display = "none";
                    el("divHeader").style.display = "block";
                    el("divFooter").style.display = "block";
                    setText(el('lblInvoiceTypeName'), el('lkpInvoiceType').value);
                    //el("lblInvoiceTypeName").innerText = el('lkpInvoiceType').value;
                }
                else {
                    el("divRecordNotFound").style.display = "block";
                    el("divRepair").style.display = "none";
                    el("divHeader").style.display = "none";
                    el("divFooter").style.display = "none";
                    setText(el('lblInvoiceTypeName'), "");
                    //  el("lblInvoiceTypeName").innerText = "";
                }
            }
            else if (el('lkpInvoiceType').SelectedValues[0] == "82") {//Miscellaneous
                bindGrid("ifgMiscellaneous", el('lkpServicePartner').SelectedValues[0], el('datPeriodFrom').value, el('datPeriodTo').value, el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                var rowCount = ifgMiscellaneous.Rows().Count;
                if (rowCount > 0) {
                    el("divMiscellaneous").style.display = "block";
                    el("divRecordNotFound").style.display = "none";
                    el("divHeader").style.display = "block";
                    el("divFooter").style.display = "block";
                    setText(el('lblInvoiceTypeName'), el('lkpInvoiceType').value);
                    // el("lblInvoiceTypeName").innerText = el('lkpInvoiceType').value;
                }
                else {
                    el("divRecordNotFound").style.display = "block";
                    el("divMiscellaneous").style.display = "none";
                    el("divHeader").style.display = "none";
                    el("divFooter").style.display = "none";
                    setText(el('lblInvoiceTypeName'), "");
                    //  el("lblInvoiceTypeName").innerText = "";
                }
            }
            else if (el('lkpInvoiceType').SelectedValues[0] == "140") {//Credit Note
                bindGrid("ifgCreditNote", el('lkpServicePartner').SelectedValues[0], el('datPeriodFrom').value, el('datPeriodTo').value, el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                var rowCount = ifgCreditNote.Rows().Count;
                if (rowCount > 0) {
                    el("divCreditNote").style.display = "block";
                    el("divRecordNotFound").style.display = "none";
                    el("divHeader").style.display = "block";
                    el("divFooter").style.display = "block";
                    setText(el('lblInvoiceTypeName'), el('lkpInvoiceType').value);
                    // el("lblInvoiceTypeName").innerText = el('lkpInvoiceType').value;
                }
                else {
                    el("divRecordNotFound").style.display = "block";
                    el("divCreditNote").style.display = "none";
                    el("divHeader").style.display = "none";
                    el("divFooter").style.display = "none";
                    setText(el('lblInvoiceTypeName'), "");
                    //  el("lblInvoiceTypeName").innerText = "";
                }
            }
            else if (el('lkpInvoiceType').SelectedValues[0] == "83") {//Transportation
                bindGrid("ifgTransportation", el('lkpServicePartner').SelectedValues[0], el('datPeriodFrom').value, el('datPeriodTo').value, el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                var rowCount = ifgTransportation.Rows().Count;
                if (rowCount > 0) {
                    el("divTransportation").style.display = "block";
                    el("divRecordNotFound").style.display = "none";
                    el("divHeader").style.display = "block";
                    el("divFooter").style.display = "block";
                    setText(el('lblInvoiceTypeName'), el('lkpInvoiceType').value);
                    // el("lblInvoiceTypeName").innerText = el('lkpInvoiceType').value;
                }
                else {
                    el("divRecordNotFound").style.display = "block";
                    el("divTransportation").style.display = "none";
                    el("divHeader").style.display = "none";
                    el("divFooter").style.display = "none";
                    setText(el('lblInvoiceTypeName'), "");
                    // el("lblInvoiceTypeName").innerText = "";
                }
            }

            //GWS
            else if (el('lkpInvoiceType').SelectedValues[0] == "151") {//Inspection
                bindGrid("ifgInspection", el('lkpServicePartner').SelectedValues[0], el('datPeriodFrom').value, el('datPeriodTo').value, el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                var rowCount = ifgInspection.Rows().Count;
                if (rowCount > 0) {
                    el("divInspection").style.display = "block";
                    el("divHeader").style.display = "block";
                    el("divFooter").style.display = "block";
                    setText(el('lblInvoiceTypeName'), el('lkpInvoiceType').value);
                    //el("lblInvoiceTypeName").innerText = el('lkpInvoiceType').value;
                    el("divRecordNotFound").style.display = "none";
                }
                else {
                    el("divRecordNotFound").style.display = "block";
                    el("divInspection").style.display = "none";
                    el("divHeader").style.display = "none";
                    el("divFooter").style.display = "none";
                    setText(el('lblInvoiceTypeName'), "");
                    //el("lblInvoiceTypeName").innerText = "";
                }
            }

            else if (el('lkpInvoiceType').SelectedValues[0] == "84") {//Rental
                // bindGrid("ifgRental", el('lkpServicePartner').SelectedValues[0], el('lblFromDate').innerText, el('datPeriodTo').value, el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                bindGrid("ifgRental", el('lkpServicePartner').SelectedValues[0], getText(el('lblFromDate')), el('datPeriodTo').value, el('lkpServicePartner').SelectedValues[3], el('lkpInvoiceType').SelectedValues[0]);
                var rowCount = ifgRental.Rows().Count;
                if (rowCount > 0) {
                    el("divRental").style.display = "block";
                    el("divHeader").style.display = "block";
                    el("divFooter").style.display = "block";
                    setText(el('lblInvoiceTypeName'), el('lkpInvoiceType').value);
                    //el("lblInvoiceTypeName").innerText = el('lkpInvoiceType').value;

                    // el("divRecordNotFound").style.display = "none";
                }
                else {
                    el("divRecordNotFound").style.display = "block";
                    el("divRental").style.display = "none";
                    el("divHeader").style.display = "none";
                    el("divFooter").style.display = "none";
                    setText(el('lblInvoiceTypeName'), "");
                    //  el("lblInvoiceTypeName").innerText = "";
                }
            }
        }
        setReadOnly('lkpDepotCode', true);
        setReadOnly('lkpInvoiceType', true);
        setReadOnly('lkpServicePartner', true);
        setReadOnly('datPeriodFrom', true);
        setReadOnly('datPeriodTo', true);
    }
    else {
        resetInvoice();
    }
    strInvoiceNo = "";
    strInvoiceFileName = "";
    reSizePane();
}

function bindGrid(GridName, CustomerId, PeriodFrom, PeriodTo, CustomerType, invoiceTypeID) {
    var objGrid = new ClientiFlexGrid(GridName);
    objGrid.parameters.add("CustomerId", CustomerId);
    objGrid.parameters.add("PeriodFrom", PeriodFrom);
    objGrid.parameters.add("PeriodTo", PeriodTo);
    objGrid.parameters.add("CustomerType", CustomerType);
    objGrid.parameters.add("InvoiceTypeID", invoiceTypeID);
    objGrid.parameters.add("DepotID", el('lkpDepotCode').SelectedValues[0]);
    objGrid.DataBind();
}

function filterInvoiceType() {
    if (el('lkpInvoiceType').value != "" && el('lkpInvoiceType').value != null) {
        if (getConfigSetting('067') != "True") {
            if ((el('lkpInvoiceType').value.toUpperCase() == "HANDLING & STORAGE") || (el('lkpInvoiceType').value.toUpperCase() == "HEATING")) {
                return "SRVC_PRTNR_TYP_CD='CUSTOMER'";
            }
            if (el('lkpInvoiceType').value.toUpperCase() == "CLEANING" || el('lkpInvoiceType').value.toUpperCase() == "REPAIR" || el('lkpInvoiceType').value.toUpperCase() == "MISCELLANEOUS" || el('lkpInvoiceType').value.toUpperCase() == "CREDIT NOTE") {
                return "SRVC_PRTNR_TYP_CD IN ('CUSTOMER','PARTY')";
            }
        }
        if (el('lkpInvoiceType').value.toUpperCase() == "RENTAL") {
            return "SRVC_PRTNR_TYP_CD='CUSTOMER' AND RNTL_BT=1";
        }
        if ( el('lkpInvoiceType').value.toUpperCase() == "TRANSPORTATION") {
            return "SRVC_PRTNR_TYP_CD='CUSTOMER' AND TRNSPRTTN_BT=1";
        }       
    }
}

function resetInvoice() {
    resetValidators();
    setText(el('btnSubmit'), "Fetch");
   // el('btnSubmit').innerText = "Fetch";
    el("divHandlingStorage").style.display = "none";
    el("divRecordNotFound").style.display = "none";
    el("divRepair").style.display = "none";
    el("divCleaning").style.display = "none";
    el("divMiscellaneous").style.display = "none";
    el("divCreditNote").style.display = "none";
    el("divHeating").style.display = "none";
    el("divTransportation").style.display = "none";
    el("divRental").style.display = "none";
    setReadOnly('lkpInvoiceType', false);
    setReadOnly('lkpDepotCode', false);
    setReadOnly('lkpServicePartner', false);
    setReadOnly('datPeriodFrom', false);
    setReadOnly('datPeriodTo', false);
    el("divRepair").style.display = "none";
    el("divHeader").style.display = "none";
    el("divFooter").style.display = "none";
    setText(el('lblInvoiceTypeName'), "");
    //el("lblInvoiceTypeName").innerText = "";
    el("divInspection").style.display = "none";
}

function clearValues() {
    resetInvoice();
    clearLookupValues("lkpInvoiceType");
    clearLookupValues("lkpServicePartner");
    if (el("hdnIsHeadQuarters").value != "false") {
        clearLookupValues("lkpDepotCode");
    }
    else {
        setReadOnly('lkpDepotCode', true);
    }
    clearTextValues("datPeriodFrom");
    clearTextValues("datPeriodTo");
    el("divlblFromDate").style.display = "none";
    el("divdatFromDate").style.display = "block";
    setText(el('lblToDate'),"");
    setText(el('lblFromDate'), "");
    el("divLblToDate").style.display = "none";
    el("divDatToDate").style.display = "block";
   // el("lblFromDate").innerText = "";
}

function onClientChangeInvoiceTypeClearValues() {
    clearLookupValues("lkpServicePartner");
    clearTextValues("datPeriodFrom");
    clearTextValues("datPeriodTo");
    if ((el('lkpInvoiceType').SelectedValues[0] == "78") || (el('lkpInvoiceType').SelectedValues[0] == "84")) {
        el("divlblFromDate").style.display = "block";
        el("divdatFromDate").style.display = "none";
        el("divLblToDate").style.display = "none";
        el("divDatToDate").style.display = "block";
        setText(el('lblFromDate'), "");
    }
    else if (getConfigSetting('073') == "True" && (el('lkpInvoiceType').SelectedValues[0] == "80")) {
        el("divlblFromDate").style.display = "block";
        el("divdatFromDate").style.display = "none";
        el("divLblToDate").style.display = "block";
        el("divDatToDate").style.display = "none";
        setText(el('lblFromDate'), "");
        setText(el('lblToDate'), "");
    }
    else {
        el("divlblFromDate").style.display = "none";
        el("divdatFromDate").style.display = "block";
        el("divLblToDate").style.display = "none";
        el("divDatToDate").style.display = "block";
        setText(el('lblFromDate'), "");
        setText(el('lblToDate'), "");
      //  el("lblFromDate").innerText = "";
    }
}

function printInvoiceRegister() {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "PendingInvoice";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Pending Invoice";
    oDocPrint.DocumentId = "25";
    oDocPrint.ReportPath = "../Documents/Report/PendingInvoice.rdlc";
    oDocPrint.openReportDialog();
}

function printInvoice() {
    var oDocPrint = new DocumentPrint();
    if (el('lkpInvoiceType').SelectedValues[0] == "78") {//Handling and Storage
        oDocPrint.KeyName = "InvoiceGeneration";
        oDocPrint.Title = "Invoice Generation";
        if (getConfigSetting('067') == "True") {
            oDocPrint.DocumentId = "24";
            oDocPrint.add("CUSTOMER_TYPE", "AGENT");
            oDocPrint.add("TEMPLATEID", "48");
            oDocPrint.ReportPath = "../Documents/Report/HSReportGWS.rdlc";
        }
        else {
            oDocPrint.DocumentId = "24";
            oDocPrint.ReportPath = "../Documents/Report/HSReport.rdlc";
        }
      
    }
    else if (el('lkpInvoiceType').SelectedValues[0] == "79") {//Heating 
        oDocPrint.KeyName = "HeatingInvoice";
        oDocPrint.Title = "Heating Invoice";
        oDocPrint.DocumentId = "29";
        oDocPrint.ReportPath = "../Documents/Report/HeatingInvoice.rdlc";
    }
    else if (el('lkpInvoiceType').SelectedValues[0] == "80") {//Cleaning
        oDocPrint.KeyName = "CleaningInvoice";
        oDocPrint.Title = "Cleaning Invoice";
        oDocPrint.DocumentId = "27";
        oDocPrint.ReportPath = "../Documents/Report/CleaningInvoice.rdlc";
    }
    else if (el('lkpInvoiceType').SelectedValues[0] == "81") {//Repair
        oDocPrint.KeyName = "RepairInvoice";
        oDocPrint.Title = "Repair Invoice";

        if (getConfigSetting('067') == "True") {
            oDocPrint.DocumentId = "26";
            oDocPrint.add("CUSTOMER_TYPE", "AGENT");
            oDocPrint.add("TEMPLATEID", "50");
            oDocPrint.ReportPath = "../Documents/Report/RepairInvoiceGWS.rdlc";
        }
        else {
            oDocPrint.DocumentId = "26";
            oDocPrint.ReportPath = "../Documents/Report/RepairInvoice.rdlc";
        }

//        oDocPrint.DocumentId = "26";
//        oDocPrint.ReportPath = "../Documents/Report/RepairInvoice.rdlc";
    }
    else if (el('lkpInvoiceType').SelectedValues[0] == "82") {//Miscellaneous
        oDocPrint.KeyName = "MiscellaneousInvoice";
        oDocPrint.Title = "Miscellaneous Invoice";
        oDocPrint.DocumentId = "28";
        oDocPrint.ReportPath = "../Documents/Report/MiscellaneousInvoice.rdlc";
    }
    else if (el('lkpInvoiceType').SelectedValues[0] == "83") {//Transportation
        oDocPrint.KeyName = "TransportationInvoice";
        oDocPrint.Title = "Transportation Invoice";
        oDocPrint.DocumentId = "34";
        oDocPrint.ReportPath = "../Documents/Report/TransportationInvoice.rdlc";
    }
    else if (el('lkpInvoiceType').SelectedValues[0] == "84") {//Rental
        oDocPrint.KeyName = "RentalInvoice";
        oDocPrint.Title = "Rental Invoice";
        oDocPrint.DocumentId = "35";
        oDocPrint.ReportPath = "../Documents/Report/RentalInvoice.rdlc";
    }
    else if (el('lkpInvoiceType').SelectedValues[0] == "140") {//CreditNote
        oDocPrint.KeyName = "CreditNote";
        oDocPrint.Title = "Credit Note";
        oDocPrint.DocumentId = "40";
        oDocPrint.ReportPath = "../Documents/Report/CreditNote.rdlc";
    }
    else if (el('lkpInvoiceType').SelectedValues[0] == "151") {//Inspection Invoice
        oDocPrint.KeyName = "InspectionInvoice";
        oDocPrint.Title = "Inspection Invoice";
        oDocPrint.DocumentId = "44";
        oDocPrint.ReportPath = "../Documents/Report/InspectionInvoiceGWC.rdlc";
    }

    oDocPrint.Type = "All";
    oDocPrint.add("INVC_TYP", el('lkpInvoiceType').SelectedValues[1]);
    oDocPrint.add("INVC_NO", strInvoiceFileName);
    oDocPrint.add("BILLING_TYPE", "DRAFT");
    oDocPrint.openReportDialog();
    showInfoMessage('Draft ' + el('lkpInvoiceType').SelectedValues[1] + ' Invoice has been generated with Invoice No : ' + strInvoiceNo + ' and same can be viewed in View Invoice Page.');
}

function fnGridOnafterCB(param) {
    if (typeof (param["ExRate"]) != 'undefined') {
        setText(el('lblExrate'), param["ExRate"]);
        // el('lblExrate').innerText = param["ExRate"];
    }
    if (typeof (param["CustCurrency"]) != 'undefined') {
        setText(el('lblCustAmount'), param["CustAmount"]);
        setText(el('lblCustCurrency'), param["CustCurrency"]);
        //el('lblCustAmount').innerText = param["CustAmount"];
        //  el('lblCustCurrency').innerText = param["CustCurrency"];
        el('hdnCustomerCurrencyID').value = param["CustCurrencyID"];
    }
    if (typeof (param["DepotCurrency"]) != 'undefined') {
        setText(el('lblDepotAmount'), param["DepotAmount"]);
        setText(el('lblDepotCurrency'), param["DepotCurrency"]);
        // el('lblDepotAmount').innerText = param["DepotAmount"];
        // el('lblDepotCurrency').innerText = param["DepotCurrency"];
        el('hdnDepotCurrencyID').value = param["DepotCurrencyID"];
    }
    if ((el('hdnWarningMsg') != "" || el('hdnWarningMsg') != null || el('hdnWarningMsg') != undefined) && (typeof (param["WarningMsg"]) != 'undefined')) {
        if (param["WarningMsg"] == "Show") {
            showWarningMessage(el('hdnWarningMsg').value);
        }
    }
}

function fnCalcHeatingAmount(obj) {
    var iRowIndex = ifgHeating.rowIndex;
    var oCols = ifgHeating.Rows(iRowIndex).GetClientColumns();
    var Custamount = new Number;
    var Depotamount = new Number;
    var TotalDepotamount = new Number;
    var TotalCustamount = new Number;
    Custamount = parseFloat(oCols[10]);
    Depotamount = parseFloat(oCols[9]);
//     setText(TotalCustamount, parseFloat(el('lblCustAmount')));
//     setText(TotalDepotamount, parseFloat(el('lblDepotAmount')));
     TotalCustamount = parseFloat(getText(el('lblCustAmount')));
     TotalDepotamount = parseFloat(getText(el('lblDepotAmount')));
   // TotalCustamount = parseFloat(el('lblCustAmount').innerText);
   // TotalDepotamount = parseFloat(el('lblDepotAmount').innerText);
    if (obj.checked == true) {
        TotalCustamount = TotalCustamount + Custamount;
        TotalDepotamount = TotalDepotamount + Depotamount;
    }
    else {
        TotalCustamount = TotalCustamount - Custamount.toFixed(2);
        TotalDepotamount = TotalDepotamount - Depotamount.toFixed(2);
    }
    setText(el('lblCustAmount'), TotalCustamount.toFixed(2));
    setText(el('lblDepotAmount'), TotalDepotamount.toFixed(2));
   // el('lblCustAmount').innerText = TotalCustamount.toFixed(2);
    //el('lblDepotAmount').innerText = TotalDepotamount.toFixed(2);
}

function fnCalcMiscellaneousAmount(obj) {
    var iRowIndex = ifgMiscellaneous.rowIndex;
    var oCols = ifgMiscellaneous.Rows(iRowIndex).GetClientColumns();
    var Custamount = new Number;
    var Depotamount = new Number;
    var TotalDepotamount = new Number;
    var TotalCustamount = new Number;
    Custamount = parseFloat(oCols[6]);
    Depotamount = parseFloat(oCols[5]);
//    setText(TotalCustamount, parseFloat(el('lblCustAmount')));
    //    setText(TotalDepotamount, parseFloat(el('lblDepotAmount')));
    TotalCustamount = parseFloat(getText(el('lblCustAmount')));
    TotalDepotamount = parseFloat(getText(el('lblDepotAmount')));
   // TotalCustamount = parseFloat(el('lblCustAmount').innerText);
   // TotalDepotamount = parseFloat(el('lblDepotAmount').innerText);
    if (obj.checked == true) {
        TotalCustamount = TotalCustamount + Custamount;
        TotalDepotamount = TotalDepotamount + Depotamount;
    }
    else {
        TotalCustamount = TotalCustamount - Custamount.toFixed(2);
        TotalDepotamount = TotalDepotamount - Depotamount.toFixed(2);
    }
      setText(el('lblCustAmount'), TotalCustamount.toFixed(2));
        setText(el('lblDepotAmount'), TotalDepotamount.toFixed(2));
   // el('lblCustAmount').innerText = TotalCustamount.toFixed(2);
   // el('lblDepotAmount').innerText = TotalDepotamount.toFixed(2);
    }

    function fnCalcCreditNoteAmount(obj) {
        var iRowIndex = ifgCreditNote.rowIndex;
        var oCols = ifgCreditNote.Rows(iRowIndex).GetClientColumns();
        var Custamount = new Number;
        var Depotamount = new Number;
        var TotalDepotamount = new Number;
        var TotalCustamount = new Number;
        Custamount = parseFloat(oCols[6]);
        Depotamount = parseFloat(oCols[5]);
        //    setText(TotalCustamount, parseFloat(el('lblCustAmount')));
        //    setText(TotalDepotamount, parseFloat(el('lblDepotAmount')));
        TotalCustamount = parseFloat(getText(el('lblCustAmount')));
        TotalDepotamount = parseFloat(getText(el('lblDepotAmount')));
        // TotalCustamount = parseFloat(el('lblCustAmount').innerText);
        // TotalDepotamount = parseFloat(el('lblDepotAmount').innerText);
        if (obj.checked == true) {
            TotalCustamount = TotalCustamount + Custamount;
            TotalDepotamount = TotalDepotamount + Depotamount;
        }
        else {
            TotalCustamount = TotalCustamount - Custamount.toFixed(2);
            TotalDepotamount = TotalDepotamount - Depotamount.toFixed(2);
        }
        setText(el('lblCustAmount'), TotalCustamount.toFixed(2));
        setText(el('lblDepotAmount'), TotalDepotamount.toFixed(2));
        // el('lblCustAmount').innerText = TotalCustamount.toFixed(2);
        // el('lblDepotAmount').innerText = TotalDepotamount.toFixed(2);
    }

function fnCalcCleaningAmount(obj) {
    var iRowIndex = ifgCleaning.rowIndex;
    var oCols = ifgCleaning.Rows(iRowIndex).GetClientColumns();
    var Custamount = new Number;
    var Depotamount = new Number;
    var TotalDepotamount = new Number;
    var TotalCustamount = new Number;
    Custamount = parseFloat(oCols[9]);
    Depotamount = parseFloat(oCols[8]);
//     setText(TotalCustamount, parseFloat(el('lblCustAmount')));
//     setText(TotalDepotamount, parseFloat(el('lblDepotAmount')));
    TotalCustamount = parseFloat(getText(el('lblCustAmount')));
    TotalDepotamount = parseFloat(getText(el('lblDepotAmount')));
   // TotalCustamount = parseFloat(el('lblCustAmount').innerText);
   // TotalDepotamount = parseFloat(el('lblDepotAmount').innerText);
    if (obj.checked == true) {
        TotalCustamount = TotalCustamount + Custamount;
        TotalDepotamount = TotalDepotamount + Depotamount;
    }
    else {
        TotalCustamount = TotalCustamount - Custamount.toFixed(2);
        TotalDepotamount = TotalDepotamount - Depotamount.toFixed(2);
    }
       setText(el('lblCustAmount'), TotalCustamount.toFixed(2));
        setText(el('lblDepotAmount'), TotalDepotamount.toFixed(2));
   // el('lblCustAmount').innerText = TotalCustamount.toFixed(2);
   // el('lblDepotAmount').innerText = TotalDepotamount.toFixed(2);
}

function fnCalcRepairAmount(obj) {
    var iRowIndex = ifgRepair.rowIndex;
    var oCols = ifgRepair.Rows(iRowIndex).GetClientColumns();
    var Custamount = new Number;
    var Depotamount = new Number;
    var TotalDepotamount = new Number;
    var TotalCustamount = new Number;
    //Fix for repair amount summary calculation

    if (getConfigSetting('067') == "True") {
        Custamount = parseFloat(oCols[10]);
        Depotamount = parseFloat(oCols[9]);
    }
    else {
        Custamount = parseFloat(oCols[15]);
        Depotamount = parseFloat(oCols[14]);
    }
    document.getElementById(obj.id).checked = obj.checked;
//    setText(TotalCustamount, parseFloat(el('lblCustAmount')));
//    setText(TotalDepotamount, parseFloat(el('lblDepotAmount')));
    TotalCustamount = parseFloat(getText(el('lblCustAmount')));
    TotalDepotamount = parseFloat(getText(el('lblDepotAmount')));
//    TotalCustamount = parseFloat(el('lblCustAmount').innerText);
//    TotalDepotamount = parseFloat(el('lblDepotAmount').innerText);
    if (obj.checked == true) {
        TotalCustamount = TotalCustamount + Custamount;
        TotalDepotamount = TotalDepotamount + Depotamount;
    }
    else {
        TotalCustamount = TotalCustamount - Custamount.toFixed(2);
        TotalDepotamount = TotalDepotamount - Depotamount.toFixed(2);
    }
       setText(el('lblCustAmount'), TotalCustamount.toFixed(2));
        setText(el('lblDepotAmount'), TotalDepotamount.toFixed(2));
//    el('lblCustAmount').innerText = TotalCustamount.toFixed(2);
//    el('lblDepotAmount').innerText = TotalDepotamount.toFixed(2);
}

function fnSaveDraftInvoice() {
    var oCallback = new Callback();
    oCallback.add("InvoiceTypeID", el('lkpInvoiceType').SelectedValues[0]);
    oCallback.add("CustomerCurrency", el('hdnCustomerCurrencyID').value);
    oCallback.add("DepotCurrency", el('hdnDepotCurrencyID').value);

    oCallback.add("ExRate", el('lblExrate').innerText);
    if ((el('lkpInvoiceType').SelectedValues[0] == "78") || (el('lkpInvoiceType').SelectedValues[0] == "84")) {
        oCallback.add("FromDate", getText(el('lblFromDate')))
        oCallback.add("ToDate", el('datPeriodTo').value);
        //oCallback.add("FromDate", el('lblFromDate').innerText);
    } else if (getConfigSetting('073') == "True" && (el('lkpInvoiceType').SelectedValues[0] == "80")) {
        oCallback.add("FromDate", getText(el('lblFromDate')))
        oCallback.add("ToDate", getText(el('lblToDate')));
    }
    else {
        oCallback.add("FromDate", el('datPeriodFrom').value);
        oCallback.add("ToDate", el('datPeriodTo').value);
    }
  
    oCallback.add("CustAmount", parseFloat(getText(el('lblCustAmount'))));
    oCallback.add("DepotAmount",  parseFloat(getText(el('lblDepotAmount'))));
//    oCallback.add("CustAmount", el('lblCustAmount').innerText);
//    oCallback.add("DepotAmount", el('lblDepotAmount').innerText);
    oCallback.add("ServicePartnerID", el('lkpServicePartner').SelectedValues[0]);
    oCallback.add("CustomerType", el('lkpServicePartner').SelectedValues[3]);
    oCallback.add("InvoiceNo", strInvoiceNo);
    oCallback.add("DepotID", el('lkpDepotCode').SelectedValues[0]);
    oCallback.invoke("InvoiceGeneration.aspx", "DraftInvoice");
    if (oCallback.getCallbackStatus()) {
        strInvoiceNo = oCallback.getReturnValue("InvoiceNo");
        strInvoiceFileName = oCallback.getReturnValue("FileName");
        printInvoice();
        resetInvoice();
        clearValues();
    }
    else {
        if (oCallback.getReturnValue("Select") == "False") {

            if (oCallback.getReturnValue("ErrorMessage") != "" && oCallback.getReturnValue("ErrorMessage") != null) 
            {

                showErrorMessage(oCallback.getReturnValue("ErrorMessage"));
            } else 
            {

                showErrorMessage("Atleast one Equipment must be selected to Generate Draft.");
            }
            return false;


        }
    }
    oCallback = null;
    return true;
}

function checkDraftExists() {
    var oCallback = new Callback();
    oCallback.add("InvoiceTypeID", el('lkpInvoiceType').SelectedValues[0]);
    if ((el('lkpInvoiceType').SelectedValues[0] == "78") || (el('lkpInvoiceType').SelectedValues[0] == "84")) {
        //   oCallback.add("FromDate", el('lblFromDate').innerText);
        oCallback.add("FromDate", getText(el('lblFromDate')));
        oCallback.add("ToDate", el('datPeriodTo').value);
    } else if (getConfigSetting('073') == "True" && (el('lkpInvoiceType').SelectedValues[0] == "80")) {
        oCallback.add("FromDate", getText(el('lblFromDate')))
        oCallback.add("ToDate", getText(el('lblToDate')));
    }
    else {
        oCallback.add("FromDate", el('datPeriodFrom').value);
        oCallback.add("ToDate", el('datPeriodTo').value);
    }
   
    oCallback.add("ServicePartnerID", el('lkpServicePartner').SelectedValues[0]);
    oCallback.add("CustomerType", el('lkpServicePartner').SelectedValues[3]);
    oCallback.add("DepotID", el('lkpDepotCode').SelectedValues[0]);
    oCallback.invoke("InvoiceGeneration.aspx", "CheckDraftInvoice");
    if (oCallback.getCallbackStatus()) {
        fnSaveDraftInvoice();
        oCallback = null;
        return true;
    }
    else {
        var Message = "Draft Invoice (" + oCallback.getReturnValue("InvoiceNo") +") already exists for the specified Period, Party and Invoice Type. Are you sure you want to override the Draft. Click 'Yes' to Override the Draft or 'No' to Ignore the Draft Generation.";
        showConfirmMessage(Message, "wfs().yesClick();", "wfs().noClick();");
        strInvoiceNo = oCallback.getReturnValue("InvoiceNo");
        strInvoiceFileName = oCallback.getReturnValue("FileName");
        return true;
    }
}

function yesClick() {
    fnSaveDraftInvoice();
    return true;
}

function noClick() {
    return false;
}

function onClientTextChangeServicePartner() {
    if ((el('lkpInvoiceType').SelectedValues[0] == "78") || (el('lkpInvoiceType').SelectedValues[0] == "84")) {//Handling and Storage & Rental
        clearTextValues("datPeriodFrom");
        var oCallback = new Callback();
        oCallback.add("InvoiceTypeID", el('lkpInvoiceType').SelectedValues[0]);
        oCallback.add("ServicePartnerID", el('lkpServicePartner').SelectedValues[0]);
        oCallback.add("CustomerType", el('lkpServicePartner').SelectedValues[3]);
        oCallback.add("DepotID", el('lkpDepotCode').SelectedValues[0]);
        oCallback.invoke("InvoiceGeneration.aspx", "GetFromDate");
        if (oCallback.getCallbackStatus()) {
            if (oCallback.getReturnValue("FROMDATE") != "" && oCallback.getReturnValue("FROMDATE") != null) {
                // el('lblFromDate').innerText = oCallback.getReturnValue("FROMDATE").toUpperCase();
                setText(el('lblFromDate'), oCallback.getReturnValue("FROMDATE").toUpperCase());
            }
            else {
                setText(el('lblFromDate'), "");
                //el('lblFromDate').value = "";
            }
        }
        else {
            setReadOnly('datPeriodFrom', false);
        }
    }
    else if (getConfigSetting('073') == "True" && (el('lkpInvoiceType').SelectedValues[0] == "80")) {
        clearTextValues("datPeriodFrom");
        clearTextValues("datPeriodTo");
        clearTextValues("lblFromDate");
        clearTextValues("lblToDate");
        var oCallback = new Callback();
        oCallback.add("InvoiceTypeID", el('lkpInvoiceType').SelectedValues[0]);
        oCallback.add("ServicePartnerID", el('lkpServicePartner').SelectedValues[0]);
        oCallback.add("CustomerType", el('lkpServicePartner').SelectedValues[3]);
        oCallback.add("DepotID", el('lkpDepotCode').SelectedValues[0]);
        oCallback.invoke("InvoiceGeneration.aspx", "GetFromDateForCleaning");
        if (oCallback.getCallbackStatus()) {
            if (oCallback.getReturnValue("FROMDATE") != "" && oCallback.getReturnValue("FROMDATE") != null) {
                // el('lblFromDate').innerText = oCallback.getReturnValue("FROMDATE").toUpperCase();
                setText(el('lblFromDate'), oCallback.getReturnValue("FROMDATE").toUpperCase());
                setText(el('lblToDate'), oCallback.getReturnValue("TODATE").toUpperCase());
            }
            else {
                setText(el('lblFromDate'), "");
                setText(el('lblToDate'), "");
                //el('lblFromDate').value = "";
            }
            if (oCallback.getReturnValue("WarningMsg") != "" && oCallback.getReturnValue("WarningMsg") != null) {
                el('hdnWarningMsg').value = oCallback.getReturnValue("WarningMsg");
            }
            else {
                el('hdnWarningMsg').value = "";
            }
        }
        else {
            setReadOnly('datPeriodFrom', false);
            setReadOnly('datPeriodTo', false);
        }
    }
}

function validateTodate(oSrc, args) {
    var fromdate;
    if ((el('lkpInvoiceType').SelectedValues[0] == "78") || (el('lkpInvoiceType').SelectedValues[0] == "84")) {
        //fromdate = el('lblFromDate').innerText;
        fromdate = getText(el('lblFromDate'));
    }
    else {
        fromdate = el('datPeriodFrom').value;
    }
    if (el('datPeriodTo').value != fromdate) {
        if (!DateCompare(el('datPeriodTo').value, fromdate)) {
            showErrorMessage("Period To Date should be greater or equal to Period From Date(" + fromdate + ")");
            args.IsValid = false;
            return false;
        }
    }
}

function validateFromDate(oSrc, args) {
    if (el('lkpInvoiceType').SelectedValues[0] != "78" && el('lkpInvoiceType').SelectedValues[0] != "84" && el('datPeriodFrom').value == "") {
        if (el('lkpInvoiceType').SelectedValues[0] != "80" && getConfigSetting('073') != "True") {
            showErrorMessage("Period From Required.");
            args.IsValid = false;
            return false;
        }
    }
}

function validateToDate(oSrc, args) {
    if (el('lkpInvoiceType').SelectedValues[0] != "80" && getConfigSetting('073') != "True" && el('datPeriodTo').value == "") {
        showErrorMessage("Period To Required.");
        args.IsValid = false;
        return false;
    }
}

function fnCalcTransportationAmount(obj) {
    var iRowIndex = ifgTransportation.rowIndex;
    var oCols = ifgTransportation.Rows(iRowIndex).GetClientColumns();
    var Custamount = new Number;
    var Depotamount = new Number;
    var TotalDepotamount = new Number;
    var TotalCustamount = new Number;
    Custamount = parseFloat(oCols[18]);
    Depotamount = parseFloat(oCols[17]);
//    setText(TotalCustamount, parseFloat(el('lblCustAmount')));
    //    setText(TotalDepotamount, parseFloat(el('lblDepotAmount')));
    TotalCustamount = parseFloat(getText(el('lblCustAmount')));
    TotalDepotamount = parseFloat(getText(el('lblDepotAmount')));
//    TotalCustamount = parseFloat(el('lblCustAmount').innerText);
//    TotalDepotamount = parseFloat(el('lblDepotAmount').innerText);
    if (obj.checked == true) {
        TotalCustamount = TotalCustamount + Custamount;
        TotalDepotamount = TotalDepotamount + Depotamount;
    }
    else {
        TotalCustamount = TotalCustamount - Custamount;
        TotalDepotamount = TotalDepotamount - Depotamount;
    }
    setText(el('lblCustAmount'), TotalCustamount.toFixed(2));
    setText(el('lblDepotAmount'), TotalDepotamount.toFixed(2));
//    el('lblCustAmount').innerText = TotalCustamount.toFixed(2);
//    el('lblDepotAmount').innerText = TotalDepotamount.toFixed(2);
}

//Window Resize
if (window.$) {
    $(document).ready(function () {

        reSizePane();
    });
}

$(window.parent).resize(function () {
    reSizePane();
});

function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 768) {
            el("tabInvoice").style.height = $(window.parent).height() - 100 + "px";
            if (el("ifgHandlingStorage") != null) {
                if (!chrome) {
                    el("ifgHandlingStorage").SetStaticHeaderHeight($(window.parent).height() - 417 + "px");
                }
                else {
                    el("ifgHandlingStorage").SetStaticHeaderHeight($(window.parent).height() - 447 + "px");
                }
            }
            if (el("ifgRepair") != null) {
                el("ifgRepair").SetStaticHeaderHeight($(window.parent).height() - 407 + "px");
            }
            if (el("ifgCleaning") != null) {
                el("ifgCleaning").SetStaticHeaderHeight($(window.parent).height() - 407 + "px");
            }
            if (el("ifgMiscellaneous") != null) {
                el("ifgMiscellaneous").SetStaticHeaderHeight($(window.parent).height() - 407 + "px");
            }
            if (el("ifgRental") != null) {
                el("ifgRental").SetStaticHeaderHeight($(window.parent).height() - 407 + "px");
            }
            if (el("ifgTransportation") != null) {
                el("ifgTransportation").SetStaticHeaderHeight($(window.parent).height() - 407 + "px");
            }
            if (el("ifgHeating") != null) {
                el("ifgHeating").SetStaticHeaderHeight($(window.parent).height() - 407 + "px");
            }
            if (el("ifgCreditNote") != null) {
                el("ifgCreditNote").SetStaticHeaderHeight($(window.parent).height() - 407 + "px");
            }
            if (el("ifgInspection") != null) {
                el("ifgInspection").SetStaticHeaderHeight($(window.parent).height() - 407 + "px");
            }
           
        }
        else {
            el("tabInvoice").style.height = $(window.parent).height() - 100 + "px";
            if (el("ifgHandlingStorage") != null) {
                if (!chrome) {
                    el("ifgHandlingStorage").SetStaticHeaderHeight($(window.parent).height() - 423 + "px");
                }
                else {
                    el("ifgHandlingStorage").SetStaticHeaderHeight($(window.parent).height() - 448 + "px");
                }
            }
            if (el("ifgRepair") != null) {
                el("ifgRepair").SetStaticHeaderHeight($(window.parent).height() - 423 + "px");
            }
            if (el("ifgCleaning") != null) {
                el("ifgCleaning").SetStaticHeaderHeight($(window.parent).height() - 423 + "px");
            }
            if (el("ifgMiscellaneous") != null) {
                el("ifgMiscellaneous").SetStaticHeaderHeight($(window.parent).height() - 423 + "px");
            }
            if (el("ifgRental") != null) {
                if (!chrome) {
                    el("ifgRental").SetStaticHeaderHeight($(window.parent).height() - 423 + "px");
                }
                else {
                    el("ifgRental").SetStaticHeaderHeight($(window.parent).height() - 448 + "px");
                }
            }
            if (el("ifgTransportation") != null) {
                el("ifgTransportation").SetStaticHeaderHeight($(window.parent).height() - 423 + "px");
            }
            if (el("ifgHeating") != null) {
                el("ifgHeating").SetStaticHeaderHeight($(window.parent).height() - 423 + "px");
            }
            if (el("ifgCreditNote") != null) {
                el("ifgCreditNote").SetStaticHeaderHeight($(window.parent).height() - 423 + "px");
            }
            if (el("ifgInspection") != null) {
                el("ifgInspection").SetStaticHeaderHeight($(window.parent).height() - 423 + "px");
            }
        }
    }

}

////Header CheckBox - Cleaning

function fnInvoiceHerderCheckBox(InvcGridName, obj) {
      
    var objGrid = new ClientiFlexGrid(InvcGridName);
    objGrid.parameters.add("CheckState", obj.checked);
    objGrid.parameters.add("DepotID", el('lkpDepotCode').SelectedValues[0]);
    objGrid.DataBind();
    //Header CheckBox Maintain the State
    document.getElementById(obj.id).checked = obj.checked;
    return false;
}

//Inspection
function fnCalcInspectionAmount(obj) {
    var iRowIndex = ifgInspection.rowIndex;
    var oCols = ifgInspection.Rows(iRowIndex).GetClientColumns();
    var Custamount = new Number;
    var Depotamount = new Number;
    var TotalDepotamount = new Number;
    var TotalCustamount = new Number;
    Custamount = parseFloat(oCols[6]);
    Depotamount = parseFloat(oCols[7]);
    TotalCustamount = parseFloat(getText(el('lblCustAmount')));
    TotalDepotamount = parseFloat(getText(el('lblDepotAmount')));
    if (obj.checked == true) {
        TotalCustamount = TotalCustamount + Custamount;
        TotalDepotamount = TotalDepotamount + Depotamount;
    }
    else {
        TotalCustamount = TotalCustamount - Custamount;
        TotalDepotamount = TotalDepotamount - Depotamount;
    }
//    setText(el('lblCustAmount'), TotalCustamount.toFixed(2));
    //    setText(el('lblDepotAmount'), TotalDepotamount.toFixed(2));
    setText(el('lblCustAmount'), TotalDepotamount.toFixed(2));
    setText(el('lblDepotAmount'), TotalCustamount.toFixed(2));
}

function filterInvoiceGenerationType() {
    if (getConfigSetting('067') == "True") {
        var strFilter;
        strFilter = " ENM_ID IN (151,78,81)";
        return strFilter;
    }
    else {
        var strFilter;
        strFilter = " ENM_ID IN (78,79,80,81,82,83,84,140)";
        return strFilter;
    }
        
}

function onDepotCodeChangeClearValues() {
    clearLookupValues("lkpServicePartner");
    clearLookupValues("lkpInvoiceType");
    clearTextValues("datPeriodFrom");
    clearTextValues("datPeriodTo");
    el("lblFromDate").innerHTML = "";
    el("lblToDate").innerHTML = "";
    el("divlblFromDate").style.display = "none";
    el("divdatFromDate").style.display = "block";
    el("divLblToDate").style.display = "none";
    el("divDatToDate").style.display = "block";
}