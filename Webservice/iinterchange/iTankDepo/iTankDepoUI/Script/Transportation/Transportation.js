var HasChanges = false;
var vrGridIds = new Array('tabTransportation;ifgTransportation');
var _RowValidationFails = false;
var cancelLogics = "";
var _reason = "";
var chkdgtvalue = "";
var recordLockChanges = false;

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
// Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :57
function initPage(sMode) {
    var blnLock;
    if (sMode == MODE_NEW) {
        setFocusToField("lkpCustomer");
        clearLookupValues("lkpCustomer");
        clearLookupValues("lkpRoute");
        clearLookupValues("lkpActivity");
        clearLookupValues("lkpActivityLocation");
        clearLookupValues("lkpTransporter");            
        clearTextValues("lkpPickupLocation");
        clearTextValues("lkpDropLocation");       
        clearTextValues("txtRemarks");
        el('txtTripRate').value = "0.00";
//        el('lblTotalnoofTrips').innerText = "0";
        el('lblTotalnoofEquipment').innerText = "0";
        //el('lblTotlAmount').innerText = "0.00";
        setText(el('lblTotalnoofTrips'), "0.00");
//        setText(el('lblTotalnoofEquipment'), "0.00");
        setText(el('lblTotlAmount'), "0.00");
        setReadOnly('lkpCustomer', false);
        setReadOnly('datRequestDate', false);
        setReadOnly('lkpRoute', false);
        setReadOnly('txtRemarks', false);
        setReadOnly('lkpActivity', false);
        setReadOnly('lkpTransporter', false);
        setReadOnly('lkpActivityLocation', false);
        setReadOnly('txtTripRate', false);
        setPageID("0");
        setPageMode("new");
        bindTransportation('new');
        resetValidators();
        onAfterRowSetDefualtValues();
        showSubmitButton(true);
        el('divCancelRequest').style.display = "none";
    }
    else {
        psc().hideListGrid();
        var arrRequestNo = new Array();
        setReadOnly('lkpCustomer', true);
        setReadOnly('datRequestDate', true);
        setReadOnly('lkpActivityLocation', true);
        setReadOnly('lkpActivity', true);
        if (el('lkpStatus').SelectedValues[0] == "91" || el('lkpStatus').SelectedValues[0] == "92") {
            //89- Open, 90- Billed, 91 - Closed, 92 - Cancelled,  95-In Prorgess
            showSubmitButton(false);
        }
        else {
            showSubmitButton(true);
        }

        if (el('lkpStatus').SelectedValues[0] == "90" || el('lkpStatus').SelectedValues[0] == "91" || el('lkpStatus').SelectedValues[0] == "92") {
            el('divCancelRequest').style.display = "none";
            setReadOnly('lkpRoute', true);
            setReadOnly('lkpTransporter', true);
            setReadOnly('txtTripRate', true);
        }
        else {
            el('divCancelRequest').style.display = "block";
            setReadOnly('lkpRoute', false);
            setReadOnly('lkpTransporter', false);
            setReadOnly('txtTripRate', false);
        }

        if (el('lkpStatus').SelectedValues[0] == "92") {
            el('divPrintJobOrder').style.display = "none";
        }
        else {
            el('divPrintJobOrder').style.display = "block";
        }
        bindTransportation('bind');
        //makeReadOnly();
        setPageMode("edit");
        //arrRequestNo = el("spnRequestNo").innerText.split("-");
        arrRequestNo = getText(el('spnRequestNo')).split("-");
        if (arrRequestNo.length > 1) {
            blnLock = recordLockError(arrRequestNo[1], el("hdnLockUserName").value, el('hdnIpError').value, el('hdnLockActivityName').value);
            if (!blnLock) {
                hideDiv('divCancelRequest');
            }
        }

    }
    //el('lblDepotCurrency').innerText = el('hdnDepotCurrency').value;
    setText(el('lblDepotCurrency'), el('hdnDepotCurrency').value);
    $('.btncorner').corner();
    reSizePane();
}

function submitPage(sMode) {
    if (!recordLockChanges) {
        if (cancelLogics == 'cancel') {
            requestCancelReason();
        }
        else {
            GetLookupChanges();
            Page_ClientValidate();
            if (!Page_IsValid) {
                return false;
            }

            if (getPageChanges()) {
                var sMode = getPageMode();
                if (ifgTransportation.Submit() == false || _RowValidationFails) {
                    return false;
                }
                if (sMode == MODE_NEW) {
                    createTransportation();
                }
                else if (sMode == MODE_EDIT) {
                    updateTransportation();
                }
            }
            else {
                showInfoMessage('No Changes to Save');
            }
        }
        return true;
    }
    else {
        var arrRequestNo = new Array();
        //arrRequestNo = el("spnRequestNo").innerText.split("-");
        arrRequestNo = getText(el('spnRequestNo')).split("-");
        recordLockMessageEdit(arrRequestNo[1], el("hdnLockUserName").value, el('hdnIpError').value, el('hdnLockActivityName').value);       
        return false;
    }
}

function createTransportation() {
    var oCallback = new Callback();
    oCallback.add("CustomerId", el('lkpCustomer').SelectedValues[0]);
    oCallback.add("RequestDate", el('datRequestDate').value);
    oCallback.add("RouteId", el('lkpRoute').SelectedValues[0]);
    oCallback.add("TransporterId", el('lkpTransporter').SelectedValues[0]);
    oCallback.add("ActivityLocationId", el('lkpActivityLocation').SelectedValues[0]);
    oCallback.add("ActivityId", el('lkpActivity').SelectedValues[0]);
    oCallback.add("ActivityCode", el('lkpActivity').value);
    oCallback.add("StatusId", el('lkpStatus').SelectedValues[0]);
    oCallback.add("TripRate", el('txtTripRate').value);
    oCallback.add("Remarks", el('txtRemarks').value);
    oCallback.add("NoOfTrip", el('lblTotalnoofTrips').innerText);
   // oCallback.add("NoOfTrip", getText(el('lblTotalnoofTrips')));
    oCallback.invoke("Transportation.aspx", "CreateTransportation");
    if (oCallback.getCallbackStatus()) {
        setPageMode("edit");
        if (typeof (oCallback.getReturnValue("StatusId")) != 'undefined') {
            el('lkpStatus').SelectedValues[0] = oCallback.getReturnValue("StatusId");
        }
        if (typeof (oCallback.getReturnValue("StatusCode")) != 'undefined') {
            el('lkpStatus').value = oCallback.getReturnValue("StatusCode");
        }
        if (typeof (oCallback.getReturnValue("TransportationId")) != 'undefined') {
            setPageID(oCallback.getReturnValue("TransportationId"));
        }
        el('hdnRequestNo').value = oCallback.getReturnValue("RequestNo");
        setRequestNo(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        toggleTransportationDiv(oCallback.getReturnValue("RequestNo"));
        el('divCancelRequest').style.display = "block";
        el('divPrintJobOrder').style.display = "block";
        bindTransportation('bind');
        resetHasChanges("ifgTransportation");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function updateTransportation() {
    var oCallback = new Callback();
    oCallback.add("TransportationId", getPageID());
    oCallback.add("CustomerId", el('lkpCustomer').SelectedValues[0]);
    oCallback.add("RouteId", el('lkpRoute').SelectedValues[0]);
    oCallback.add("TransporterId", el('lkpTransporter').SelectedValues[0]);
    oCallback.add("ActivityLocationId", el('lkpActivityLocation').SelectedValues[0]);
    oCallback.add("ActivityId", el('lkpActivity').SelectedValues[0]);
    oCallback.add("ActivityCode", el('lkpActivity').value);
    oCallback.add("RequestDate", el('datRequestDate').value);
    oCallback.add("StateId", el('lkpStatus').SelectedValues[0]);
    oCallback.add("TripRate", el('txtTripRate').value);
    oCallback.add("Remarks", el('txtRemarks').value);
    oCallback.add("NoOfTrip", el('lblTotalnoofTrips').innerText);
    //oCallback.add("NoOfTrip", getText(el('lblTotalnoofTrips')));
    oCallback.add("RequestNo", el('hdnRequestNo').value);
    oCallback.invoke("Transportation.aspx", "UpdateTransportation");
    if (oCallback.getCallbackStatus()) {
        setPageMode("edit");
        if (typeof (oCallback.getReturnValue("StatusId")) != 'undefined') {
            el('lkpStatus').SelectedValues[0] = oCallback.getReturnValue("StatusId");
        }
        if (typeof (oCallback.getReturnValue("StatusCode")) != 'undefined') {
            el('lkpStatus').value = oCallback.getReturnValue("StatusCode");
        }
        if (typeof (oCallback.getReturnValue("TransportationId")) != 'undefined') {
            setPageID(oCallback.getReturnValue("TransportationId"));
        }
        el('divPrintJobOrder').style.display = "block";
        setRequestNo(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindTransportation('bind');
        resetHasChanges("ifgTransportation");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function bindTransportation(mode) {
    var ifgTransportation = new ClientiFlexGrid("ifgTransportation");
    ifgTransportation.parameters.add("WFDATA", el("WFDATA").value);
    ifgTransportation.parameters.add("TransportationID", getPageID());
    ifgTransportation.parameters.add("CustomerId", el('lkpCustomer').SelectedValues[0]);
    ifgTransportation.parameters.add("RouteId", el('lkpRoute').SelectedValues[0]);
    ifgTransportation.parameters.add("TransportationDetailId", el('hdnTransportationDetailId').value);
    ifgTransportation.parameters.add("TransportationStatusId", el('lkpStatus').SelectedValues[0]);
    ifgTransportation.parameters.add("TripRate", el('txtTripRate').value);
    ifgTransportation.parameters.add("Mode", mode);
    ifgTransportation.DataBind();
    $('.btncorner').corner();
}

function onAfterRowSetDefualtValues(iCurrentIndex) {
    var sRowState = ifgTransportation.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgTransportation.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
        ifgTransportation.Rows(iCurrentIndex).SetReadOnlyColumn(1, false);
        ifgTransportation.Rows(iCurrentIndex).SetReadOnlyColumn(2, false);
        ifgTransportation.Rows(iCurrentIndex).SetReadOnlyColumn(3, false);
        ifgTransportation.Rows(iCurrentIndex).SetReadOnlyColumn(6, false);
        if (el('lkpActivity').value == "EMPTY") {
            ifgTransportation.Rows(iCurrentIndex).SetReadOnlyColumn(8, false);
        }
        else {
            ifgTransportation.Rows(iCurrentIndex).SetReadOnlyColumn(8, true);
        }
        ifgTransportation.Rows(iCurrentIndex).SetColumnValuesByIndex(1, getConfigSetting('DefautPreAdviceType'));
        ifgTransportation.Rows(iCurrentIndex).SetColumnValuesByIndex(5, el('hdnCurrentDate').value)
        if (el('lkpActivity').value == 'EMPTY') {
            ifgTransportation.Rows(iCurrentIndex).SetColumnValuesByIndex(8, new Array(getConfigSetting('DefaultTransPortationEmpty'), "109"));
        }
    }
    if (ifgTransportation.Rows().Count == "0") {
        if (el('lkpActivity').value != "EMPTY") {
            ifgTransportation.Rows(0).SetReadOnlyColumn(8, true);
        }
    }
}

function onBeforeCallBackTransportation(mode, param) {
    if (mode = "Insert" || mode == "Update") {
        param.add("CustomerId", el('lkpCustomer').SelectedValues[0]);
        param.add("RouteId", el('lkpRoute').SelectedValues[0]);
        param.add("TripRate", el('txtTripRate').value);
        param.add("EquipmentStateCode", el('lkpActivity').value);
    }
    param.add("TransportationId", getPageID());
    if (Page_ClientValidate('headTransportation', false)) {
        return true;
    }
    else {
        return false;
    }
    if (!Page_IsValid) {
        return false;
    }
}

function onAfterCallBakTransportation(param, mode) {
    if (typeof (param["Error"]) != 'undefined') {
        showErrorMessage(param["Error"]);
    }
    if (typeof (param["Delete"]) != 'undefined') {
        showWarningMessage(param["Delete"]);
    }
    if (typeof (param["NoofEquipment"]) != 'undefined') {
       // el('lblTotalnoofEquipment').innerText = parseInt(param["NoofEquipment"]);
        setText(el('lblTotalnoofEquipment'), param["NoofEquipment"]);
    }
    if (typeof (param["NoofTrips"]) != 'undefined') {
       // el('lblTotalnoofTrips').innerText = parseInt(param["NoofTrips"]);
        setText(el('lblTotalnoofTrips'), param["NoofTrips"]);
    }
    if (typeof (param["TotalTripsRate"]) != 'undefined') {
      //  el('lblTotlAmount').innerText = parseFloat(param["TotalTripsRate"]).toFixed(2);
        setText(el('lblTotlAmount'), parseFloat(param["TotalTripsRate"]).toFixed(2));
    }
    if (mode == "Delete" || mode == "Insert" || mode == "Edit") {
        if (ifgTransportation.Rows().Count == "1") {
            if (el('lkpActivity').value == 'EMPTY') {
                var cols = ifgTransportation.Rows(0).GetClientColumns();
                var _rowI = ifgTransportation.rowIndex;
                if (cols[10] == "") {
                    ifgTransportation.Rows(0).SetColumnValuesByIndex(8, new Array(getConfigSetting('DefaultTransPortationEmpty'), "109"));
                }
            }
        }
    }

}

function clearRouteDetails() {
    clearLookupValues('lkpRoute');
    el('lkpRoute').value = '';
    clearLookupValues('lkpPickupLocation');
    el('lkpPickupLocation').value = '';
    clearLookupValues('lkpDropLocation');
    el('lkpDropLocation').value = '';
    clearLookupValues('lkpActivity');
    el('lkpActivity').value = '';
    clearTextValues('txtRouteDescription');
    clearTextValues('txtActivityLocation');
}

function onClickMassApplyInput() {
    if (ifgTransportation.Submit(true) == false || _RowValidationFails) {
        return false;
    }
    if (el('datRequestDate').value == '') {
        showErrorMessage("Fill Rrequest date to proceed mass apply input");
        setFocusToField('datRequestDate');
    }
    else {
        var oCallback = new Callback();
        oCallback.invoke("Transportation.aspx", "checkSelectBit");
        if (oCallback.getCallbackStatus()) {
            if (oCallback.getReturnValue("blnSelect") == "True") {
                var cCols = ifgTransportation.Rows(ifgTransportation.CurrentRowIndex()).Columns();
                showModalWindow("Mass Apply Input", "Transportation/TransportationMassApplyInput.aspx?RequestDate=" + el("datRequestDate").value + "&TransportationId=" + getPageID() + "&TransportationDetailId=" + cCols[0] + " ", "800px", "330px", "140px", "", "");
                psc().hideLoader();
            }
            else {
                showErrorMessage("Please Select Atleast One Equipment");
            }
        }
        HasChanges = false;
        oCallback = null;
    }
}

function onClickRate(rowID) {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }

    if (ifgTransportation.Submit() == false) {
        return false;
    }
    else {
        var _rlngth = ifgTransportation.Rows().Count;
        if ((ifgTransportation.rowIndex) == "") {
            if ((parseInt(_rlngth) - 1) == (rowID)) {
                onClickAddEdit(rowID);
            }
        }
        else {
            onClickAddEdit(rowID);
        }
    }
}

function onClickAddEdit(rowID) {
    var cols1 = ifgTransportation.Rows(rowID).Columns();
    if ((cols1[2] == "") || (cols1[11] == "")) {
        if (ifgTransportation.Submit(true) == false) {
            return false;
        }
    } else {
        showModalWindow("Additional Charge Detail - " + cols1[2] + "", "Transportation/TransportationRateDetail.aspx?TransportationId=" + getPageID() + "&rowIndex=" + ifgTransportation.rowIndex + "&TransportationDetailId=" + cols1[0] + "&BillFlag=" + cols1[14], "550px", "330px", "100px", "", "");
        psc().hideLoader();
    }
}

function onSelectRoute() {
    return "RT_CD IN (SELECT RT_CD FROM V_ROUTE WHERE RT_ID IN (SELECT RT_ID FROM CUSTOMER_TRANSPORTATION WHERE CSTMR_ID='" + el('lkpCustomer').SelectedValues[0] + "'))";
}

function validateEquipmentno(oSrc, args) {
    var cols = ifgTransportation.Rows(ifgTransportation.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgTransportation.rowIndex;
    var sEquipmentno = args.Value;
    var checkDigit;
    var sContNo;
    var strContinue = "";
    var msg = checkContainerNo(sEquipmentno);

    if (msg == "") {
        if (el("hdnchkdgtvalue").value == "True") {
            if (sEquipmentno.length == 10) {
                sEquipmentno = sEquipmentno + getCheckSum(sEquipmentno.substr(0, 10));
                ifgTransportation.Rows(_rowI).SetColumnValuesByIndex(0, sEquipmentno);
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
    }
    else {
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
    var rowState = ifgTransportation.ClientRowState();
    var oCallback = new Callback();
    oCallback.add("EquipmentId", sEquipmentno);
    oCallback.add("GridIndex", ifgTransportation.VirtualCurrentRowIndex());
    oCallback.add("RowState", rowState);
    oCallback.add("RequestNo", el('hdnRequestNo').value);
    oCallback.invoke("Transportation.aspx", "ValidateEquipment");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("bNotExists") == "true") {
            args.IsValid = true;
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Equipment already exists for another/same Job.";
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return;
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
    else if (str.length < 10) {
        spmsg = "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit<br>";
    }
    return spmsg;
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

function onApplyCustomer(obj) {
    chkdgtvalue = el('lkpCustomer').SelectedValues[3];
    el("hdnchkdgtvalue").value = chkdgtvalue;
    return "TRNSPRTTN_BT = 'True'";
}

function compareRequestDate(oSrc, args) {
    var cols = ifgTransportation.Rows(ifgTransportation.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgTransportation.rowIndex;
    if (el("datRequestDate").value != '') {
        if (DateCompareEqual(args.Value, el("datRequestDate").value)) {
            if (cols[8] != '' && cols[8] != null) {
                if (DateCompareEqual(cols[8], args.Value)) {
                    args.IsValid = true;
                    return;
                }
                else {
                    args.IsValid = false;
                    oSrc.errormessage = "Job Start Date should be less than or equal to Job End Date (" + cols[8] + ")";
                    return;
                }
            }

        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Job Start Date should be greater than or equal to Request Date (" + el("datRequestDate").value + ")";
            return;
        }
    }
    else if (cols[8] != '') {
        if (DateCompareEqual(cols[8], args.Value)) {
            args.IsValid = true;
            return;
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Job Start Date should be less than or equal to Job End Date (" + cols[8] + ")";
            return;
        }
    }
}

function compareJobStartDate(oSrc, args) {
    var cols = ifgTransportation.Rows(ifgTransportation.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgTransportation.rowIndex;

    if (cols[7] != '') {
        if (DateCompareEqual(args.Value, cols[7])) {
            args.IsValid = true;
            if (el('lkpActivity').value == 'EMPTY') {
                ifgTransportation.Rows(_rowI).SetReadOnlyColumn(8, false);
            }
            return true;
        }
        else {
            //ifgTransportation.Rows(_rowI).SetColumnValuesByIndex(8, "");
            //  ifgTransportation.Rows(_rowI).SetReadOnlyColumn(8, true);
            args.IsValid = false;
            oSrc.errormessage = "Job End Date should be greater than or equal to Job Start Date (" + cols[7] + ")";
            return false;
        }
    }
}

function printTransportationJobOrder() {
    if (el('hdnRequestNo').value != '#') {
        var oDocPrint = new DocumentPrint();
        oDocPrint.KeyName = "TransportationJobOrder";
        oDocPrint.Type = "document";
        oDocPrint.Title = "Transportation Job Order";
        oDocPrint.DocumentId = "32";
        oDocPrint.add("TransportationId", getPageID());
        oDocPrint.add("wfData", el("WFDATA").value);
        oDocPrint.ReportPath = "../Documents/Report/TransportationJobOrder.rdlc";
        oDocPrint.openReportDialog();
    }
    else {
        showErrorMessage("Submit the Transportation Request before printing Job Order");
        return false;
    }
}

function setRequestNo(sMode) {
    el('spnRequestNo').innerHTML = " - <span class='ylbl'>" + el('hdnRequestNo').value + "</span>";
}

function onTripRateSelect(obj) {
    var cols = ifgTransportation.Rows(ifgTransportation.CurrentRowIndex()).Columns();
    if (ifgTransportation.Rows().Count > 0) {
        el('hdnTransportationDetailId').value = cols[0];
    }
    if (el('lkpCustomer').value != '' && el('lkpRoute').value != '') {
        el('hdnEquipmentStateId').value = obj.SelectedValues[0];
        el('hdnEquipmentStateCode').value = obj.value;
        bindTransportation("fetchCustomerRoute");
    }
    else if (el('lkpCustomer').value == '' && el('lkpRoute').value != '') {
    }
}

function onChangeTripRate(objControl, blnMode) {
    if (el('lkpRoute').value != '') {
        if (blnMode != "TRUE") {
            clearLookupValues("lkpTransporter");
            el('lkpTransporter').value = "";
        }
        var oCallback = new Callback();
        oCallback.add("CustomerId", el('lkpCustomer').SelectedValues[0]);
        oCallback.add("RouteId", el('lkpRoute').SelectedValues[0]);
        oCallback.add("TransporterId", el('lkpTransporter').SelectedValues[0]);
        oCallback.add("EquipmentStatusId", el('lkpActivity').SelectedValues[0]);
        oCallback.add("TransporterCheck", blnMode);
        oCallback.invoke("Transportation.aspx", "calculateTripDetail");
        if (oCallback.getCallbackStatus()) {
            if (el('lkpActivity').value != '') {
                if (el('lkpActivity').value == 'EMPTY') {
                    el('txtTripRate').value = oCallback.getReturnValue("EmptyTripRate");
                }
                else {
                    el('txtTripRate').value = oCallback.getReturnValue("FullTripRate");
                }
            }
             el('lblTotlAmount').innerText = parseFloat((parseFloat(el('lblTotalnoofTrips').innerText) * parseFloat(el('txtTripRate').value)) + parseFloat(oCallback.getReturnValue("TotalRate"))).toFixed(2);
           // setText(el('lblTotlAmount'), parseFloat((parseFloat(getText(el('lblTotalnoofTrips')) * parseFloat(el('txtTripRate').value)) + parseFloat(oCallback.getReturnValue("TotalRate"))).toFixed(2)));

            if (oCallback.getReturnValue("NoRecords") == "False") {
                showErrorMessage("Transporter Not Available for the selected Route.");
            }
        }
        else {
            showErrorMessage(oCallback.getCallbackError());
        }
        oCallback = null;
        return;
    }
}


function onChangeTransporter(objControl) {
    onChangeTripRate(objControl, 'TRUE');
}

//This function is used to format all the Rate Fields to fixed.
function formatDecimalRate(obj) {
    var totalCost = 0;
    var icount = 0;
    var rIndex = ifgTransportation.CurrentRowIndex();
    if (trimAll(obj.value) != "") {
        var Amount = new Number;
        Amount = parseFloat(obj.value);
        obj.value = Amount.toFixed(2);
    }
}

function formatTripRate() {
    var fullTripRate = new Number;
    fullTripRate = trimAll(el('txtTripRate').value);
    if (fullTripRate != "") {
        el('txtTripRate').value = parseFloat(fullTripRate).toFixed(2);
    }
}

function onRequestCancel() {
    if (checkRights()) {
        if (el('hdnRequestNo').value != '#') {
            showConfirmMessage("Are you sure you want to cancel this request. Click 'Yes' to cancel or 'No' to ignore",
                                "wfs().yesClick();", "wfs().noClick();");
        }
        else {
            showErrorMessage("Can not Cancel the Request Before Submit Job Order");
            return false;
        }
    }
}

function yesClick() {
    showModalWindow("Cancel Reason", "Transportation/TransportationReason.aspx", "560px", "200px", "100px", "", "");
    psc().hideLoader();
    return true;
}

function noClick() {
    return false;
}

function requestCancelReason() {
    var oCallback = new Callback();
    oCallback.add("TransportationId", getPageID());
    oCallback.add("RequestNo", el('hdnRequestNo').value);
    oCallback.add("Reason", _reason);
    oCallback.invoke("Transportation.aspx", "cancelRequest");
    if (oCallback.getCallbackStatus()) {
        ppsc().closeModalWindow();
        showInfoMessage("Transportation Request : " + el('hdnRequestNo').value + " Cancelled Successfully.");
        el('lkpStatus').SelectedValues[0] = "92";
        el('lkpStatus').value = 'CANCELLED';
        el('divCancelRequest').style.display = "none";
        el('divPrintJobOrder').style.display = "none";
        showSubmitButton(false);
        bindTransportation('bind');
        return true;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return true;
}

function onStateChange() {
    resetValidatorByGroup('divTransportationDetail');
    ifgTransportation.Submit(true);

    var count = ifgTransportation.Rows().Count;
    for (var i = 0; i < count; i++) {
        if (el('lkpActivity').value == 'EMPTY') {
            ifgTransportation.Rows(i).SetReadOnlyColumn(8, false);
            ifgTransportation.Rows(i).SetColumnValuesByIndex(8, new Array(getConfigSetting('DefaultTransPortationEmpty'), "109"));
        }
        else {
            ifgTransportation.Rows(i).SetReadOnlyColumn(8, true);
            ifgTransportation.Rows(i).SetColumnValuesByIndex(8, new Array("", ""));
        }
    }
    var oCallback = new Callback();
    oCallback.add("EqpState", el('lkpActivity').value);
    oCallback.invoke("Transportation.aspx", "stateChange");
    if (oCallback.getCallbackStatus()) {
        var emptyRate = new Number;
        var fullRate = new Number;
        if (oCallback.getReturnValue("EmptyTripRate") == '' || oCallback.getReturnValue("EmptyTripRate") == null) {
            emptyRate = 0;
        }
        else {
            emptyRate = oCallback.getReturnValue("EmptyTripRate");
        }

        if (oCallback.getReturnValue("FullTripRate") == '' || oCallback.getReturnValue("FullTripRate") == null) {
            fullRate = 0;
        }
        else {
            fullRate = oCallback.getReturnValue("FullTripRate");
        }
        if (el('lkpActivity').value == 'EMPTY') {
            el('txtTripRate').value = parseFloat(emptyRate).toFixed(2);
        }
        else {
            el('txtTripRate').value = parseFloat(fullRate).toFixed(2);
        }
        el('lblTotlAmount').innerText = parseFloat((parseFloat(el('lblTotalnoofTrips').innerText) * parseFloat(el('txtTripRate').value)) + parseFloat(oCallback.getReturnValue("TotalRate"))).toFixed(2);
      // setText(el('lblTotlAmount'), parseFloat((parseFloat(getText(el('lblTotalnoofTrips')) * parseFloat(el('txtTripRate').value)) + parseFloat(oCallback.getReturnValue("TotalRate"))).toFixed(2)));
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return;
}

function toggleTransportationDiv(requestNo) {
    if (requestNo != '') {
        setReadOnly('lkpCustomer', true);
        setReadOnly('datRequestDate', true);
        setReadOnly('lkpActivity', true);
        setReadOnly('lkpActivityLocation', true);
    }
}

function transportationCancelReason(cancelreason) {
    cancelLogics = cancelreason;
    var iFrame = psc().document.getElementsByTagName('iframe');
    var obj = null;
    for (var i = 0; i < iFrame.length; i++) {
        if (iFrame[i].id == "fmModalFrame") {
            obj = iFrame[i];
            break;
        }
    }
    if (obj) {
        _reason = obj.contentWindow.el('txtReason').value;
    }
    fireEvent(el('btnSubmit'), 'click');
}

function onTripRateChange(obj) {
    var oCallback = new Callback();
    oCallback.add("TripRate", el('txtTripRate').value);
    oCallback.invoke("Transportation.aspx", "onTripRateChange");
    if (oCallback.getCallbackStatus()) {
        el('lblTotlAmount').innerText = parseFloat((parseFloat(el('lblTotalnoofTrips').innerText) * parseFloat(el('txtTripRate').value)) + parseFloat(oCallback.getReturnValue("TotalRate"))).toFixed(2);
        //setText(el('lblTotlAmount'), parseFloat((parseFloat(getText(el('lblTotalnoofTrips')) * parseFloat(el('txtTripRate').value)) + parseFloat(oCallback.getReturnValue("TotalRate"))).toFixed(2)));
        el('txtTripRate').value = parseFloat(el('txtTripRate').value).toFixed(2);
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return;
}

function setCheckDigit(val) {
    el("hdnchkdgtvalue").value = val;
}

function loadTransporter() {
    return " TRNSPRTR_ID IN(SELECT TRNSPRTR_ID FROM TRANSPORTER_ROUTE_DETAIL WHERE RT_ID = " + el('lkpRoute').SelectedValues[0] + ")";
}

function checkRequired(oSrc, args) {
    var cols = ifgTransportation.Rows(ifgTransportation.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgTransportation.rowIndex;
    if (el('lkpActivity').value == 'EMPTY') {
        if (cols[10] == '' || cols[10] == null) {
            ifgTransportation.Rows(_rowI).SetReadOnlyColumn(8, false);
            args.IsValid = false;
            oSrc.errormessage = "Empty Single Required";
            return false;
        }
    }
}

function onClientCheckRequired(obj) {
    var cols = ifgTransportation.Rows(ifgTransportation.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgTransportation.rowIndex;
    if (el('lkpActivity').value == 'EMPTY') {
        if (cols[10] == '') {
            showErrorMessage("Empty Single Required");
            ifgTransportation.Rows(_rowI).SetFocusInColumn(8);
            return false;
        }
    }
}

function makeReadOnly() {
    var count = ifgTransportation.Rows().Count;
    for (var i = 0; i < count; i++) {
        if (el('lkpActivity').value != 'EMPTY') {
            ifgTransportation.Rows(i).SetReadOnlyColumn(8, true);
        }
    }
}
$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 768) {
            el("tabTransportation").style.height = $(window.parent).height() - 265 + "px";
            if (el("ifgTransportation") != null) {
                el("ifgTransportation").SetStaticHeaderHeight($(window.parent).height() - 435 + "px");
            }
        }
        else {
            el("tabTransportation").style.height = $(window.parent).height() - 250 + "px";
            if (el("ifgTransportation") != null) {
                el("ifgTransportation").SetStaticHeaderHeight($(window.parent).height() - 417 + "px");
            }
        }
    }

}


function ValidateCustomerRefNo(oSrc, args) {
    var re = new RegExp("^[0-9]+$");
    var cols = ifgTransportation.Rows(ifgTransportation.CurrentRowIndex()).GetClientColumns();
    if ((el("lkpCustomer").SelectedValues[3] == "True") && cols[3] == "") {
        oSrc.errormessage = "Customer Reference Number Required.";
//        ifgTransportation.Rows(ifgTransportation.CurrentRowIndex()).SetFocusInColumn(2);
        args.IsValid = false;
        return;
    }
//    else if ((el("lkpCustomer").SelectedValues[3] == "True") && (isNaN(cols[3]))) {
//        oSrc.errormessage = "Customer Reference No Value Should Be Numeric.";
//        args.IsValid = false;
//        return;

//    }
    else if ((el("lkpCustomer").SelectedValues[3] == "True") && (!(re.test(cols[3])))) {
        oSrc.errormessage = "Customer Reference No Value Should Be Numeric.";
        args.IsValid = false;
        return;

    }
   }