var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgPreAdvice');
var _RowValidationFails = false;


//Initialize page while loading the page based on page mode
function initPage(sMode) {

    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    bindGrid();
    setDefaultValues();
    reSizePane();
    var sPageTitle = getPageTitle();
    el("hdnPageTitle").value = sPageTitle;
}

//This method used to submit the changes to database
function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    ifgPreAdvice.Submit();
    //To submit the changes to server    
    if (getPageChanges()) {
        if (ifgPreAdvice.Submit() == false || _RowValidationFails) {
            return false;
        }
        return updatePreAdvice();
    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}
//This method used to update/insert entered row to database
function updatePreAdvice() {
    var oCallback = new Callback();
    oCallback.add("WFDATA", el("WFDATA").value);
    oCallback.invoke("PreAdvice.aspx", "UpdatePreAdvice");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindGrid();
        resetHasChanges("ifgPreAdvice");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

function bindGrid() {
    var sPageTitle = getPageTitle();
    var ifgPreAdvice = new ClientiFlexGrid("ifgPreAdvice");
    ifgPreAdvice.parameters.add("WFDATA", el("WFDATA").value);
    ifgPreAdvice.DataBind();
    //UIG Fix - Pending pane title
    //$("#spnHeader").text(sPageTitle);
    //Fix for BreadCrums
    $("#spnHeader").text("Operations >>Gate In >>" + sPageTitle);
}

//This function is to Validate the Equipment No for Duplicates
function validateEquipmentno(oSrc, args) {
    var cols = ifgPreAdvice.Rows(ifgPreAdvice.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgPreAdvice.rowIndex;
    var sEquipmentno = args.Value
    var checkDigit;
    var sContNo;
    var strContinue = "";
    var msg = checkContainerNo(sEquipmentno);

    if (msg == "") {
        if (el("hdnchkdgtvalue").value == "True") {
            if (sEquipmentno.length == 10) {
                sEquipmentno = sEquipmentno + getCheckSum(sEquipmentno.substr(0, 10));
                ifgPreAdvice.Rows(_rowI).SetColumnValuesByIndex(1, sEquipmentno);
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

    var rowState = ifgPreAdvice.ClientRowState();
    var oCallback = new Callback();

    oCallback.add("EquipmentId", sEquipmentno);
    oCallback.add("GridIndex", ifgPreAdvice.VirtualCurrentRowIndex());
    oCallback.add("RowState", rowState);
    oCallback.invoke("PreAdvice.aspx", "ValidateEquipment");
    if (oCallback.getCallbackStatus()) {
        //Newly added if Equipment no already available in some other depot
         if (oCallback.getReturnValue("EquipmentNoInAnotherDepot") == "false") {
            oSrc.errormessage = "This Equipment " + sEquipmentno + " already exists for Pre-Advice in some other Depot.";
            args.IsValid = false;
         }
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
            var strAllowRental = oCallback.getReturnValue("AllowRental");
            var cols = ifgPreAdvice.Rows(ifgPreAdvice.CurrentRowIndex()).GetClientColumns();
            if (strCustomer != cols[0] && cols[0] != "") {
                oSrc.errormessage = "This Equipment " + sEquipmentno + " already exists for Customer " + strCustomer + " in Rental";
                args.IsValid = false;
            }
            else if (strAllowRental == "False") {
                oSrc.errormessage = "This Equipment " + sEquipmentno + " cannot be submitted as Rental Gate Out not created " + strCustomer + " in Rental";
                args.IsValid = false;
            }
        }
        else {
            args.IsValid = false;
            var strCustomer = oCallback.getReturnValue("Customer");
            oSrc.errormessage = "Gate In has been already created for the Equipment " + sEquipmentno + " with Customer " + strCustomer + ",hence cannot create Pre-Advice.";
        }
        if (oCallback.getReturnValue("EquipmentTypeCode") != '' && oCallback.getReturnValue("EquipmentTypeId") != '') {
            ifgPreAdvice.Rows(ifgPreAdvice.CurrentRowIndex()).SetColumnValuesByIndex(2, new Array(oCallback.getReturnValue("EquipmentTypeCode"), oCallback.getReturnValue("EquipmentTypeId")));
            ifgPreAdvice.Rows(ifgPreAdvice.CurrentRowIndex()).SetReadOnlyColumn(2, true);
        }
       
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
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


function bindCustomerName(obj) {
    CustomerName = obj.SelectedValues[2];
    var chkdgtvalue = obj.SelectedValues[3];
    el("hdnchkdgtvalue").value = chkdgtvalue;
    ifgPreAdvice.Rows(ifgPreAdvice.CurrentRowIndex()).SetColumnValuesByIndex(1, "");
}

//This function used to Hide the Detail Grid when it has no Rows to display.
function ifgPreAdviceOnAfterCB(param) {
    if (typeof (param["Error"]) != 'undefined') {
        showErrorMessage(param["Error"]);
        return false;
    }
    if (typeof (param["Success"]) != 'undefined') {
        showWarningMessage(param["Success"]);
        return false;
    }
}

function ifgPreAdviceOnBeforeCB(mode, param) {
    if (mode == "Delete") {
        var _cols = ifgPreAdvice.Rows(ifgPreAdvice.rowIndex).Columns();
        param.add("PreAdviceID", _cols[0]);
        param.add("EquipmentNo", _cols[1]);
        param.add("GI_TransactionNo", _cols[17]);
    }
}

function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgPreAdvice.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgPreAdvice.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
        ifgPreAdvice.Rows(iCurrentIndex).SetReadOnlyColumn(1, false);
        ifgPreAdvice.Rows(iCurrentIndex).SetReadOnlyColumn(2, false);
        if (getConfigSetting('054') != "True") {
            ifgPreAdvice.Rows(iCurrentIndex).SetReadOnlyColumn(5, false);
            ifgPreAdvice.Rows(iCurrentIndex).SetReadOnlyColumn(4, false);
            ifgPreAdvice.Rows(iCurrentIndex).SetColumnValuesByIndex(5, el('hdnCurrentDate').value);
        }
        else if (getConfigSetting('054') == "True") {
            ifgPreAdvice.Rows(iCurrentIndex).SetColumnValuesByIndex(3, getConfigSetting('DefaultEquipmentCode'));
            ifgPreAdvice.Rows(iCurrentIndex).SetColumnValuesByIndex(8, el('hdnCurrentDate').value);
        }
        //  ifgEquipmentDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(2, getConfigSetting('DefaultEquipmentType'));
        ifgPreAdvice.Rows(iCurrentIndex).SetColumnValuesByIndex(2, getConfigSetting('013'));

        //ifgPreAdvice.Rows(iCurrentIndex).SetColumnValuesByIndex(2, getConfigSetting('DefautPreAdviceType'));
        //ifgPreAdvice.Rows(iCurrentIndex).SetColumnValuesByIndex(7, el('hdnCurrentDate').value);
       
    }
    return true;
}


function ValidatePreAdviceDate(oSrc, args) {
    var cols = ifgPreAdvice.Rows(ifgPreAdvice.CurrentRowIndex()).GetClientColumns();
    if (cols[8] == "") {
        oSrc.errormessage = "ETA Date Required";
        args.IsValid = false;
        return;
    }

    var eventdate = cols[12];
    var currentdate = el('hdnCurrentDate').value;
    //    var oCallback = new Callback();
    //    oCallback.add("EquipmentNo", cols[2]);
    //    oCallback.add("EventDate", cols[8]);
    //    oCallback.invoke("PreAdvice.aspx", "ValidatePreviousActivityDate");

    //    if (oCallback.getCallbackStatus()) {
    //        if (oCallback.getReturnValue("Error") != "") {
    //            args.IsValid = false;
    //            oSrc.errormessage = oCallback.getReturnValue("Error");
    //        }
    //        else {
    //            args.IsValid = true;
    //        }
    //    }
    //    else {
    //        showErrorMessage(oCallback.getCallbackError());
    //    }
    //    oCallback = null;

    if (DateCompareEqual(args.Value, currentdate)) {
        args.IsValid = true;
        return;
    }
    else {
        args.IsValid = false;
        oSrc.errormessage = "ETA Date should be greater than or equal to Current Date (" + currentdate + ")";
        return;
    }
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
        if ($(window.parent).height() < 680) {
            el("divPreAdvice").style.height = $(window.parent).height() - 238 + "px";
            if (el("ifgPreAdvice") != null) {
                el("ifgPreAdvice").SetStaticHeaderHeight($(window.parent).height() - 300 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("divPreAdvice").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgPreAdvice") != null) {
                el("ifgPreAdvice").SetStaticHeaderHeight($(window.parent).height() - 452 + "px");
            }
        }
        else {
            el("divPreAdvice").style.height = $(window.parent).height() - 243 + "px";
            if (el("ifgPreAdvice") != null) {
                el("ifgPreAdvice").SetStaticHeaderHeight($(window.parent).height() - 300 + "px");
            }
        }
    }
}

//Added for Attchement
function showPhotoUpload(rowIndex, PreAdviceId) {
    var cols = ifgPreAdvice.Rows(rowIndex).Columns();
    HasChanges = true;
    if (ifgPreAdvice.Submit() == false) {
        return false;
    }
    showModalWindow("Photo Upload - " + cols[1] + "", "Operations/PhotoUpload.aspx?RepairEstimateId=" + PreAdviceId + "&PageName=" + "Pre-Advice", "850px", "500px", "150px", "", "", "", "wfs().imageDisplayRepairCompletion(" + PreAdviceId + ")");
    psc().hideLoader();
}

function imageDisplayRepairCompletion(preAdviceId) {
    var objGrid = new ClientiFlexGrid("ifgPreAdvice");
    objGrid.parameters.add("Mode", "ReBind");
    objGrid.parameters.add("RepairEstimateId", preAdviceId);
    objGrid.parameters.add("WFDATA", el("WFDATA").value);
    objGrid.DataBind();
}

//Set Equipment Code
function setEquipmentCode(oSrc, args) {
    if (getConfigSetting('054') == "True") {
        var cols = ifgPreAdvice.Rows(ifgPreAdvice.CurrentRowIndex()).GetClientColumns();
        // ifgPreAdvice.Rows(ifgPreAdvice.CurrentRowIndex()).SetColumnValuesByIndex(3, cols[5]);
        var oCallback = new Callback();
        oCallback.add("Type", cols[3]);
        oCallback.invoke("PreAdvice.aspx", "GetEquipmentCode");
        if (oCallback.getCallbackStatus()) {
            ifgPreAdvice.Rows(ifgPreAdvice.CurrentRowIndex()).SetColumnValuesByIndex(3, oCallback.getReturnValue("Code"));
        }
    }
    else {
        //        ifgPreAdvice.Rows(ifgPreAdvice.CurrentRowIndex()).SetColumnValuesByIndex(3, "");
    }
    return false;
}

function openUpload() {
    if (checkRights()) {
        var sPageTitle = getPageTitle();
        var SchemaId = getQStr("activityid");
        var tablename = "PRE_ADVICE";
        if (getConfigSetting('054') == "True") {
            SchemaId = "81";
        }
        else {
            SchemaId = "82";
        }
        showModalWindow("Pre-Advice - Upload", "Upload.aspx?SchemaID=" + SchemaId + "&tablename=" + tablename + "&" + el("WFDATA").value, "1000px", "460px", "", "", "");
    }
    else
        return false;
}