var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgEquipmentDetail');
var _RowValidationFails = false;
var Mode;
var HasMoreInfoChanges = false;
var CustomerName = "";
var Description = "";
var StatusID = "";
var YardLocation = "";
var StrPrint;
var EqpmntTypeID = "";
var EqpmntTypeCD = "";
var EqpmntCodeID = "";
var EqpmntCodeCD = "";
var gridSbmit = false;


function initPage(sMode) {
    //$("#spnHeader").text(getPageTitle());
    //Fix for BreadCrums

    var sPageTitle = getPageTitle();
    $("#spnHeader").text("Operations >>Gate In >>" + sPageTitle);
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    showSubmitPrintButton(true);
    bindEquipmentDetail(MODE_NEW);
    reSizePane();
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount <= 0) {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}

function submitPage() {
    var blnRentalBit;
    blnRentalBit = checkRentalBit();
    if (blnRentalBit == true) {
        showConfirmMessage("There are some equipment(s) selected for Off-Hire. Are you sure you want to Submit. Click 'Yes' to Submit or 'No' to ignore",
                                "wfs().yesClick();", "wfs().noClick();");
    }
    else {
        GetLookupChanges();
        Page_ClientValidate();
        if (!Page_IsValid) {
            return false;
        }
        icount = ifgEquipmentDetail.Rows().Count;
        if (icount > 0) {
            if (getPageChanges() || HasMoreInfoChanges) {
                // ifgEquipmentDetail.Submit();
                if (ifgEquipmentDetail.Submit() == false || _RowValidationFails) {
                    return false;
                }
                //    ifgEquipmentDetail.Submit(true);
                return updateGateIn();

            }
            else {
                showInfoMessage('No Changes to Save');
                return false;
            }
        } else {
            showInfoMessage('There are no Records to perform Submit');
            return false;
        }
    }
    return true;
}

function yesClick() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount > 0) {
        if (getPageChanges() || HasMoreInfoChanges) {
            //            if (ifgEquipmentDetail.Submit() == false || _RowValidationFails) {
            //                return false;
            //            }
            return updateGateIn();
        }
        else {
            showInfoMessage('No Changes to Save');
            return false;
        }
    } else {
        showInfoMessage('There are no Records to perform Submit');
        return false;
    }
}

function noClick() {
    return false;
}

function submitPrintPage() {
    var blnRentalBit;
    blnRentalBit = checkRentalBit();
    if (blnRentalBit == true) {
        showConfirmMessage("There are some equipment(s) selected for Off-Hire. Are you sure you want to Submit. Click 'Yes' to Submit or 'No' to ignore",
                                "wfs().yesPrintClick();", "wfs().noClick();");
    }
    else {
        printGateIn();
    }
}

function yesPrintClick() {
    yesClick();
    printGateIn();
}



//This function is used to Update the changes to the server. 
function updateGateIn() {
    var oCallback = new Callback();
    //  ifgEquipmentDetail.Submit(true);
    oCallback.add("WFData", getWFDATA());
    oCallback.add("Mode", el("hdnMode").value);
    oCallback.invoke("GateIn.aspx", "UpdateGateIn");

    if (oCallback.getCallbackStatus()) {
        if (el("iTabSelection_ITab1").value == "Pending") {
            sMode = "new";
        } else {
            sMode = "edit";
        }
        //  sMode = "edit";
        bindEquipmentDetail(sMode);
        if (oCallback.getReturnValue("LockRecord") != "" && oCallback.getReturnValue("LockRecord") != null) {//added and changed for UIG Fix
            showErrorMessage("The following equipment(s) are already submitted. " + oCallback.getReturnValue("LockRecord"));
        }
        if (oCallback.getReturnValue("LockRecordBit") != "true") {
            showInfoMessage(oCallback.getReturnValue("Message"));
        }
        HasChanges = false;
        resetHasChanges("ifgEquipmentDetail");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function bindEquipmentDetail(mode) {
    var objGrid = new ClientiFlexGrid("ifgEquipmentDetail");
    objGrid.parameters.add("Mode", mode);
    objGrid.parameters.add("WFData", getWFDATA());
    objGrid.DataBind();
}

function openMoreInfo(_gtnBin, _EqpNo) {
    if (ifgEquipmentDetail.Submit() == false) {
        return false;
    }
    if ((_EqpNo != "") || (_gtnBin == "")) {
        var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).Columns();
        if (!_gtnBin) {

            if (cols.length == 1) {
                if (ifgEquipmentDetail.Submit() == false) {
                    return false;
                }
            } else {
                showModalWindow("Gate In - More Information", "Operations/MoreInfo.aspx?GateInId=" + cols[0], "780px", "600px", "100px", "", "");
                psc().hideLoader();
            }
        }
        else {
            showModalWindow("Gate In - More Information", "Operations/MoreInfo.aspx?GateInId=" + _gtnBin + "&EquipmentNo=" + _EqpNo, "780px", "600px", "100px", "", "");
            return true;
            psc().hideLoader();
        }
    }
}

function openEquipmentInfo(_gtnBin, _EqpNo, _gateinNo) {
    if (_EqpNo == "") {
        var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).Columns();
        _EqpNo = cols[4];
    }
    if ((_EqpNo != "") || (_gtnBin == "")) {
        if (!_gtnBin) {

            if (cols.length == 1) {
                if (ifgEquipmentDetail.Submit(true) == false) {
                    return false;
                }
            } else {
                showModalWindow("Gate In - Equipment Information", "Operations/EquipmentInformation.aspx?GateInId=" + cols[0] + "&activityid=80" + "&GateinTransactionNo=" + _gateinNo, "950px", "250px", "100px", "", "");
                psc().hideLoader();
            }
        }
        else {
            showModalWindow("Gate In - Equipment Information", "Operations/EquipmentInformation.aspx?GateInId=" + _gtnBin + "&EquipmentNo=" + _EqpNo + "&activityid=80" + "&GateinTransactionNo=" + _gateinNo, "950px", "250px", "100px", "", "");
            psc().hideLoader();
        }
    }
}

function validateEquipmentno(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgEquipmentDetail.rowIndex;
    var sEquipmentno = args.Value
    var checkDigit;
    var sContNo;
    var strContinue = "";
    //  ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(18, "");
    var msg = checkContainerNo(sEquipmentno);
    //    ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(17, true);

    if (msg == "") {
        if (el("hdnchkdgtvalue").value == "True") {
            if (sEquipmentno.length == 10) {
                sEquipmentno = sEquipmentno + getCheckSum(sEquipmentno.substr(0, 10));
                ifgEquipmentDetail.Rows(_rowI).SetColumnValuesByIndex(1, sEquipmentno);
                strContinue = "N";
            } else {
                checkDigit = getCheckSum(sEquipmentno.substr(0, 10));

                if (sEquipmentno.length >= 10) {
                    if (checkDigit != sEquipmentno.substr(10)) {
                        args.IsValid = false;
                        oSrc.errormessage = "Check Digit is incorrect for the entered Equipment. Correct check digit is  " + checkDigit;
                        return;
                    }
                }
            }
        }
    } else {
        args.IsValid = false;
        oSrc.errormessage = msg;
        return;
    }

    if (strContinue == "") {
        validateEquipmentNo(oSrc, args);
        if (args.IsValid == false) {
            return false
        }
    }

    var rowState = ifgEquipmentDetail.ClientRowState();
    var oCallback = new Callback();

    oCallback.add("EquipmentId", sEquipmentno);
    oCallback.add("GridIndex", ifgEquipmentDetail.VirtualCurrentRowIndex());
    oCallback.add("RowState", rowState);
    oCallback.invoke("GateIn.aspx", "ValidateEquipment");
    if (oCallback.getCallbackStatus()) {
        //Newly added if Equipment no already available in some other depot
        if (oCallback.getReturnValue("EquipmentNoInAnotherDepot") == "false") {
            oSrc.errormessage = "This Equipment " + sEquipmentno + " already exists for Pre-Advice in some other Depot.";
            args.IsValid = false;
        }
        //Newly added if Equipment no already available in some other depot
        else if (oCallback.getReturnValue("StatusOfEquipment") == "false") {
            oSrc.errormessage = "This Equipment " + sEquipmentno + " already is in Active State in some other Depot.";
            args.IsValid = false;
        }
        else if (oCallback.getReturnValue("bNotExists") == "true") {
            args.IsValid = true;
        }
        else if (oCallback.getReturnValue("bRentalNotExists") == "false") {
            //         args.IsValid = false;
            var strCustomer = oCallback.getReturnValue("Customer");
            var strGateOut = oCallback.getReturnValue("GateOutNotExists");
            var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
            if (strCustomer != "") {
                // ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(17, false);
                ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(18, false);
            }

            else {
                //ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(17, true);
                ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(18, true);
            }
            if (strCustomer != cols[0] && cols[0] != "") {
                oSrc.errormessage = "This Equipment " + sEquipmentno + " already exists for Customer " + strCustomer + " in Rental";
                if (strGateOut == "False") {
                    // ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(17, false);
                    ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(18, false);
                }
                //ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(17, true);
                ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(18, true);
                args.IsValid = false;
            }
            else {
                if (strGateOut == "False") {
                    //                    ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(17, false);
                    ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(18, false);
                }
                else {
                    //ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(17, true);
                    ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(18, true);
                }
                //              ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(17, false);
            }
        }

        else {
            //alert(JSON.stringify(cols[0]));
            if (cols[0] != "" && cols[0] != null) {
                args.IsValid = false;
                var strCustomer = oCallback.getReturnValue("Customer");
                oSrc.errormessage = "Equipment " + sEquipmentno + " already exists for Customer " + strCustomer;
            }
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    //ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(18, "");
    oCallback = null;
}

function checkContainerNo(str) {
    var spmsg = "";
    var UnitIDA = str.substr(0, 4).toUpperCase();
    var UnitIDN = str.substr(4, 6);

    if ((UnitIDA.charCodeAt(0) < 65 || UnitIDA.charCodeAt(0) > 91) || (UnitIDA.charCodeAt(1) < 65 || UnitIDA.charCodeAt(1) > 91) || (UnitIDA.charCodeAt(2) < 65 || UnitIDA.charCodeAt(2) > 91) || (UnitIDA.charCodeAt(3) < 65 || UnitIDA.charCodeAt(3) > 91)) {//alpha check.
        spmsg = "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit<br>";
    }
    else if (isNaN(UnitIDN)) {//numeric check.
        spmsg = "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit<br>";
    }
    return spmsg;
}

function setDefaultValues(iCurrentIndex) {
    var sMode;
    if (el("iTabSelection_ITab1").value == "Pending") {
        sMode = "new";
    } else {
        sMode = "edit";
    }

    if (sMode == "edit") {
        ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(1, false);
    } else if (sMode == "new") {
        var sRowState = ifgEquipmentDetail.ClientRowState();
        if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
            ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
            ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(1, false);
            ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(2, false);
            //            ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(3, false);
            ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(4, false);
            ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(5, false);
            ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(6, false);
            ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(7, false);
            ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(20, true);
            if (getConfigSetting('051') != "True") {
                //                ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(4, true);
                ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(7, getConfigSetting('008'));
                ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(8, false);
                ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(9, false);
                ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(17, true);

                var rowcount = ifgEquipmentDetail.Rows().Count;
                var rIndex = new Number;
                rIndex = parseInt(iCurrentIndex) + parseInt(1);
                var table = document.getElementById('ifgEquipmentDetail');
                var Row = table.rows[rIndex];

                var cell1 = Row.cells[14];
                var cell2 = Row.cells[15];
                cell1.innerHTML = "<img src='../Images/info.png' align='right'/>";
                cell2.innerHTML = "<img src='../Images/einfo.png' align='right'/>";
            }
            else {
                var today = new Date();
                var h = today.getHours();
                var m = today.getMinutes();
                if (m < 10) {
                    m = "0" + m;
                }
                el("hdnCurrentTime").value = h + ":" + m;
                ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(4, "");
                ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(7, el("hdnCurrentTime").value);
                ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(9, true);
                ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(10, false);
                ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(11, "");
                ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(12, false);
                ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(12, "");
                ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(13, false);
                ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(13, "");
                ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(17, "");
                ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(21, false);
                var rIndex = new Number;
                rIndex = parseInt(iCurrentIndex) + parseInt(1);
                var table = document.getElementById('ifgEquipmentDetail');
                var Row = table.rows[rIndex];
                //                var cell1 = Row.cells[19];
                //                var cell2 = Row.cells[20];
                var cell1 = Row.cells[17];
                var cell2 = Row.cells[18];
                var cell3 = Row.cells[19];
                cell1.innerHTML = "<img src='../Images/info.png' align='right'/>";
                cell2.innerHTML = "<img src='../Images/einfo.png' align='right'/>";
            }
            ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(2, getConfigSetting('009'));
            ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(3, getConfigSetting('010'));
            ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(4, getConfigSetting('005'));
            ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(5, getYardLocation());
            ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(6, el("hdnCurrentDate").value);
            ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(9, "");
            ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(14, "");
            ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(15, "");
            //            ifgEquipmentDetail.Rows(iCurrentIndex).SetReadOnlyColumn(18, false);
            //            ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(18, "");
            //19- more info 20-equipinfo 21 - attachm 22-chked 23-rental
        }
    }
    resetHasChanges("ifgEquipmentDetail");
    return true;
}

//This function used to Hide the Detail Grid when it has no Rows to display.
function ifgEquipmentDetailOnAfterCB(param, mode) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    CustomerName = "";
    Description = "";
    if (typeof (param["Delete"]) != 'undefined') {
        showWarningMessage(param["Delete"]);
        return;
    }
}

//This function is used to set the mode of the page for Equipment Detail Grid
function ifgEquipmentDetailOnBeforeCB(mode, param) {
    //    param.add("PageMode", "New");
    //    param.add("CustomerName", CustomerName);
    //    param.add("Description", Description);
    //    param.add("StatusID", StatusID);
    //    param.add("YardLocation", YardLocation);
    //    param.add("EqpmntTypeID", EqpmntTypeID);
    //    param.add("EqpmntTypeCD", EqpmntTypeCD);
    //    param.add("EqpmntCodeID", EqpmntCodeID);
    //    param.add("EqpmntCodeID", EqpmntCodeID);
}

function printGateIn() {
    var oDocPrint = new DocumentPrint();
    if (getConfigSetting('055') == "True") {
        oDocPrint.KeyName = "GateIn";
        oDocPrint.Type = "document";
        oDocPrint.Title = "Gate In";
        oDocPrint.DocumentId = "41";
        oDocPrint.ReportPath = "../Documents/Report/GateInDocumentGWC.rdlc";
        oDocPrint.openReportDialog();
    }
    else {
        oDocPrint.KeyName = "GateIn";
        oDocPrint.Type = "document";
        oDocPrint.Title = "Gate In";
        oDocPrint.DocumentId = "11";
        oDocPrint.ReportPath = "../Documents/Report/GateInDocument.rdlc";
        oDocPrint.openReportDialog();
    }

}

function bindCustomerName(obj) {
    //  CustomerName = obj.SelectedValues[2];
    var chkdgtvalue = obj.SelectedValues[3];
    el("hdnchkdgtvalue").value = chkdgtvalue;
    ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(1, "");
    //ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(17, false);
    // ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(18, "");
    setAgentNameNValidate(obj)
}

function bindDescription(obj) {
    Description = obj.SelectedValues[2];
}

function getCheckSum(strNumber) {
    strNumber = strNumber.toUpperCase();
    var retVal = true;
    var total = parseInt("0")
    var product;
    if (strNumber.length == 10) {
        for (var i = 0; retVal = true, i < strNumber.length; i++) {
            var strCharCode = strNumber.charCodeAt(i);
            // Check For Alphabet
            if (strCharCode >= 65 && strCharCode <= 90) {
                product = a[strCharCode - 65]
            }
            else {
                // Check for Number
                if (strCharCode >= 48 && strCharCode <= 57) {
                    product = (strCharCode - 48)
                }
                //Not a Number or Alphabet
                else
                    retVal = false;
            }
            total = total + ((parseInt(product)) * Math.pow(2, i))
        }

        if (retVal) {
            var cs = (total % 11) % 10;
            return cs;
        }
    }
}

function ValidateEquipmentCode(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    if (cols[5] == "") {
        oSrc.errormessage = "Equipment Code Required";
        args.IsValid = false;
    }
}
function ValidateEquipmentStatus(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    if (cols[7] == "") {
        oSrc.errormessage = "Equipment Status Required";
        args.IsValid = false;
     
    }
}

function ValidateGateInDate(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    if (cols[10] == "" || cols[10] == null) {
        oSrc.errormessage = "In Date Required";
        args.IsValid = false;
        return;
    }

    var oCallback = new Callback();
    oCallback.add("EquipmentNo", cols[2]);
    oCallback.add("EventDate", cols[10]);
    oCallback.invoke("GateIn.aspx", "ValidatePreviousActivityDate");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {//added and changed for UIG Fix
            args.IsValid = false;
            oSrc.errormessage = oCallback.getReturnValue("Error");
        }
        else {
            args.IsValid = true;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function ValidateCargo(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    //    if (cols[12] == "") {
    if ((cols[12] == "" || cols[12] == null) && (cols[19] == true)) {
        oSrc.errormessage = "Previous Cargo Required";
        args.IsValid = false;
    }
}

function filterDefaultStatus() {
    var strFilter;
    if (getConfigSetting('051') != "True") {
        strFilter = " WF_ACTIVITY_NAME='Gate In' AND DPT_ID=" + el('hdnDepotID').value;
    }
    else {
        strFilter = " WF_ACTIVITY_NAME='GateIn-GWS' AND DPT_ID=" + el('hdnDepotID').value;
    }
    return strFilter;
}

function clearEquipmentNo() {
    ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(1, "");
}

function bindPending() {
    bindEquipmentDetail(MODE_NEW);
    el("hdnMode").value = MODE_NEW;
}

function ifgEquipmentDetailOnAfterSubmitTabSelected() {
    if (HasChanges == true) {
        if (checkGridHasChanges("ifgEquipmentDetail")) {
            setHasChanges();
            var confirm = false;
            if (psc().confirmresult == false) {
                confirm = confirmChanges("submit", "");
                return false;
            }
            if (confirm)
                return true;
        }
    }
    else {
        el("hdnMode").value = "edit";
        bindEquipmentDetail(MODE_EDIT);
    }
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount <= 0) {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}

function ifgEquipmentDetailOnAfterPendingTabSelected() {
    bindEquipmentDetail(MODE_NEW);
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount <= 0) {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}

function ifgEquipmentDetailOnBeforeSubmitTabSelected() {
    //    Page_ClientValidate();
    //    if (!Page_IsValid) {
    //        return false;
    //    }
}

function clearInDate() {
    //    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    //    if (cols[10] != "") {
    //        ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(6, "");      
    //    }
}

function validateRentalEntry(obj) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    var oCallback = new Callback();
    if (cols[2] != "") {
        oCallback.add("EquipmentNo", cols[2]);
        oCallback.invoke("Gatein.aspx", "validateRentalEntry");
        if (oCallback.getCallbackStatus()) {
            if (oCallback.getReturnValue("isRentalExist") == "False") {
                showErrorMessage("Equipment " + cols[2] + " cannot be marked for Rental Gate In, as there is no Rental Entry / Rental Gate Out created.");
                // ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(17, false);
                //for Attchement
                //ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(18, false);
                ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(23, false);
                obj.checked = false;
            }

        }
        else {
            showErrorMessage(oCallback.getCallbackError());
        }
        oCallback = null;
    }
    return;
}

function getSupplierDetails(obj) {
    var oCallback = new Callback();
    oCallback.add("EquipmentNo", obj.value);
    oCallback.invoke("Gatein.aspx", "getSupplierDetails");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("EquipmentTypeCode") != '' && oCallback.getReturnValue("EquipmentTypeId") != '' && oCallback.getReturnValue("EquipmentCode") != '' && oCallback.getReturnValue("EquipmentCodeId") != '') {
            ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(2, new Array(oCallback.getReturnValue("EquipmentTypeCode"), oCallback.getReturnValue("EquipmentTypeId")));
            ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(3, new Array(oCallback.getReturnValue("EquipmentCode"), oCallback.getReturnValue("EquipmentCodeId")));
            ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(2, true);
            ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(3, true);
        }
        else {
            // ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(17, false);
            // ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(18, false);
            ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(23, false);
        }

    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return;
}


function checkRentalBit() {
    if (gridSbmit == false) {
        gridSbmit = ifgEquipmentDetail.Submit(true);
    }
    var oCallback = new Callback();
    oCallback.invoke("Gatein.aspx", "checkRentalBit");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("RenalBit") == 'true') {
            return true;
        }
        // UIG fix for equipment not submitted
        else
            return false;
    }
    else {
        return false;
    }
    oCallback = null;
}

// Lock Implementation 
function GIlockData(obj, strEquipmentNo) {
    var oCallback = new Callback();
    oCallback.add("CheckBit", obj.checked);
    oCallback.add("EquipmentNo", strEquipmentNo);
    oCallback.add("LockBit", "");
    oCallback.invoke("GateIn.aspx", "GIlockData");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ErrorMessage") != "") {
            obj.checked = false;
            var errorMessage = oCallback.getReturnValue("ErrorMessage");

            if (oCallback.getReturnValue("Activity") != "") {
                errorMessage = errorMessage + " <B> (Activity : " + oCallback.getReturnValue("Activity") + ")</B>";
            }

            showErrorMessage(errorMessage);
        }
        return true;
    }
    else {
        return false;
    }
    oCallback = null;
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
        if ($(window.parent)) {
            if ($(window.parent).height() < 680) {
                el("tabGateIn").style.height = $(window.parent).height() - 233 + "px";
                if (el("ifgEquipmentDetail") != null) {
                    el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 303 + "px");
                }
            }
            else if ($(window.parent).height() < 768) {
                el("tabGateIn").style.height = $(window.parent).height() - 350 + "px";
                if (el("ifgEquipmentDetail") != null) {
                    el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
                }
            }
            else {
                el("tabGateIn").style.height = $(window.parent).height() - 231 + "px";
                if (el("ifgEquipmentDetail") != null) {
                    el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 302 + "px");
                }
            }
        }
    }
}

//Added for Attchement
function showPhotoUpload(rowIndex, GateInId, GateInPreAdvice) {
    var cols = ifgEquipmentDetail.Rows(rowIndex).Columns();
    if (ifgEquipmentDetail.Submit() == false) {
        return false;
    }
    var cMode = "edit";
    var SubName = "GateIn";
    //Check Whethr Gate in has Attachment
    if (el("iTabSelection_ITab1").value == "Pending") {
        if (GateInPreAdvice != "") {
            var oCallback = new Callback();
            oCallback.add("GateInPreAdvice", GateInPreAdvice);
            oCallback.invoke("GateIn.aspx", "ValidateGateINAttachment");
            if (oCallback.getReturnValue("Message") == "Yes") {
                GateInId = GateInPreAdvice;
                SubName = "Pre-Advice"
            }
        }
        cMode = "new";
    }
    //var oCallback = new Callback();
    //oCallback.add("CheckBit", false);
    //oCallback.add("EquipmentNo", cols[4]);
    //oCallback.add("LockBit", "");
    //oCallback.invoke("GateIn.aspx", "GIlockData");

    //showModalWindow("Photo Upload - " + cols[4] + "", "Operations/PhotoUpload.aspx?RepairEstimateId=" + GateInId + "&PageName=" + "GateIn", "850px", "500px", "150px", "", "", "", "wfs().imageDisplayRepairCompletion(" + GateInId + ",'" + cMode  + "')");
    showModalWindow("Photo Upload - " + cols[4] + "", "Operations/PhotoUpload.aspx?RepairEstimateId=" + GateInId + "&PageName=" + "GateIn" + "&SubName=" + SubName, "850px", "500px", "150px", "", "", "", "wfs().imageDisplayRepairCompletion(" + GateInId + ",'" + cMode + "')");
    psc().hideLoader();
}

function imageDisplayRepairCompletion(GateInId, cMode) {
    var objGrid = new ClientiFlexGrid("ifgEquipmentDetail");
    //    if (el("iTabSelection_ITab1").value == "Pending") {
    //        sMode = "new";
    //    } else {
    //        sMode = "edit";
    //    }
    sMode = cMode;
    // el("hdnMode").value = "edit";
    //   setPageMode("edit");
    //   setPageMode(sMode);
    objGrid.parameters.add("AttchMode", "ReBind");
    objGrid.parameters.add("Mode", sMode);
    objGrid.parameters.add("RepairEstimateId", GateInId);
    objGrid.parameters.add("WFDATA", el("WFDATA").value);
    objGrid.DataBind();
    if (cMode == "edit") {
        setPageMode(cMode);
        getPageMode(sMode);
    }
}


//Equipment Type & Code
//Set Equipment Code
function setEquipmentCode(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    var oCallback = new Callback();

    oCallback.add("Type", args.Value);
    oCallback.invoke("GateIn.aspx", "GetEquipmentCode");

    if (oCallback.getCallbackStatus()) {
        ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(2, new Array(args.Value, oCallback.getReturnValue("ID")));
        ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(3, oCallback.getReturnValue("Code"));
    }
    else {
        if (oCallback.getCallbackStatus()) {
            var oCallbackType = new Callback();
            oCallbackType.add("TypeID", cols[4]);
            oCallbackType.invoke("GateIn.aspx", "GetEquipmentCodebyTypeId");
            ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(2, new Array(args.Value, oCallback.getReturnValue("ID")));
            ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(3, oCallback.getReturnValue("Code"));
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Please Configure Equipment Type & Code";
        }
    }
}

function setAgentNameNValidate(obj) {
    if (getConfigSetting('051') == "True") {
        var oCallback = new Callback();
        oCallback.add("CustomerCode", obj.SelectedValues[1]);
        oCallback.add("RowIndex", ifgEquipmentDetail.CurrentRowIndex());
        oCallback.invoke("GateIn.aspx", "GetAgentName");
        if (oCallback.getCallbackStatus()) {
            var AgentName = oCallback.getReturnValue("AgentName");
            ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(9, AgentName);
            if (AgentName != "") {
                ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(10, "AGENT");
            }
            else {
                //                  ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(11, el("lkpBillTo").SelectedValues[0]);
                ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(10, "CUSTOMER");
                //  ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(10);
            }
            // ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetColumnValuesByIndex(18, "");
        }
    }
}

function validateBillTo(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    if (cols[13] == null || cols[13] == "") {
        if (cols[0] == 'undefined' || cols[0] == "") {
            args.IsValid = false;
            oSrc.errormessage = "Please select a customer to validate";
        }
        else {
            if (cols[14] == "AGENT") {
                args.IsValid = false;
                oSrc.errormessage = "Please select only Customer";
            }
        }
    }
    else {
        if (cols[0] == 'undefined' || cols[0] == "") {
            args.IsValid = false;
            oSrc.errormessage = "Please select a customer to validate";
        }
    }
}

function ValidateGrade(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    if ((cols[20] == "" || cols[20] == null) && (cols[23] == true)) {
        oSrc.errormessage = "Grade Required";
        args.IsValid = false;
    }
}


function getYardLocation() {
    var oCallback = new Callback();
    var strYardLocation;

    oCallback.invoke("GateIn.aspx", "GetYardLocation");
    if (oCallback.getCallbackStatus()) {
        strYardLocation = oCallback.getReturnValue("YardLocation");
    }
    return strYardLocation;
}