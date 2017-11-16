var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgEquipmentUpdate');
var _RowValidationFails = false;
var recordLockChanges = false;
// Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :58
//This function is used to intialize the page.

function initPage(sMode) {
    reSizePane();
    hideDiv("divEquipmentUpdate");
    hideDiv("divRecordNotFound");
    setFocusToField("txtEquipmentNo");
    setPageMode("edit");
    clearTextValues("txtEquipmentNo");
    clearValues();
    showSubmitPrintButton(false);
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    $('.btncorner').corner();
}

function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        if (ifgEquipmentUpdate.Submit() == false || _RowValidationFails) {
            return false;
        }

        //Release 3

        if (el('lkpCustomer').value != "") {

            if (el('txtNewEqpNo').value == "") {
                showErrorMessage("Equipment No Required");
                setFocusToField("txtNewEqpNo");
                return false;
            }

            if (el('datEffectiveFrom').value != "") {

                return onClickEquipment();
            }
            else {
                showErrorMessage("Effective From Date Required");
                setFocusToField("datEffectiveFrom");
                return false;
            }
        }
        else {
            return onClickEquipment();
        }

    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

function onClickEquipment() {
    if (checkRights()) {
        var oCallback = new Callback();
        oCallback.add("NewEquipmentNo", el('txtNewEqpNo').value);
        oCallback.add("NewCustomerCd", el('lkpCustomer').value);
        oCallback.add("NewTypeCd", el('lkpEqpType').value);
        oCallback.add("NewCodeCd", el('txtNewEqpCode').value);
        oCallback.add("oldEquipmentNo", el('txtEquipmentNo').value);
        oCallback.add("oldCustomerId", pdfs("wfFrame" + pdf("CurrentDesk")).el('hdnCustomerId').value);
        oCallback.add("NewCustomerId", el('lkpCustomer').SelectedValues[0]);
        oCallback.invoke("EquipmentUpdate.aspx", "checkHasChanges");
        if (oCallback.getCallbackStatus()) {

            if (oCallback.getReturnValue("Message") == "True") {
                showInfoMessage('No Changes to Save');
            }
            else {

                if (oCallback.getReturnValue("Message") == "Show Confirmation Message") {
                    showConfirmMessage("Same Customer already exists for this Equipment. Click 'Yes' to update or 'No' to ignore",
                                "wfs().yesClick();", "wfs().noClick();");
                }
                else {
                    showConfirmMessage("Are you sure you want to update this activity. Click 'Yes' to update or 'No' to ignore",
                                "wfs().yesClick();", "wfs().noClick();");
                }
            }

        }
        else {
            showErrorMessage(oCallback.getCallbackError());
        }
    }
}

function yesClick() {
    if (!recordLockChanges) {
        showModalWindow("Update Reason", "Operations/EquipmentUpdateReason.aspx", "560px", "200px", "100px", "", "");
        psc().hideLoader();
        return true;
    }
    else {
        recordLockMessageEdit(el("txtEquipmentNo").value, el("hdnLockUserName").value, el('hdnIpError').value, el('hdnLockActivityName').value);
        return false;
    }
}

function noClick() {
    return false;
}

function UpdateEquipment(Reason) {
    var oCallback = new Callback();
    oCallback.add("NewEquipmentNo", pdfs("wfFrame" + pdf("CurrentDesk")).el('txtNewEqpNo').value);
    oCallback.add("NewCustomerId", pdfs("wfFrame" + pdf("CurrentDesk")).el('lkpCustomer').SelectedValues[0]);
    oCallback.add("NewCustomerCd", pdfs("wfFrame" + pdf("CurrentDesk")).el('lkpCustomer').value);
    oCallback.add("NewTypeId", pdfs("wfFrame" + pdf("CurrentDesk")).el('lkpEqpType').SelectedValues[0]);
    oCallback.add("NewTypeCd", pdfs("wfFrame" + pdf("CurrentDesk")).el('lkpEqpType').value);
    oCallback.add("NewCodeId", pdfs("wfFrame" + pdf("CurrentDesk")).el('lkpEqpType').SelectedValues[0]);
    oCallback.add("NewCodeCd", pdfs("wfFrame" + pdf("CurrentDesk")).el('txtNewEqpCode').value);
    oCallback.add("oldEquipmentNo", pdfs("wfFrame" + pdf("CurrentDesk")).el('txtEquipmentNo').value);
    oCallback.add("oldTypeId", pdfs("wfFrame" + pdf("CurrentDesk")).el('hdnTypeId').value);
    oCallback.add("oldTypeCd", pdfs("wfFrame" + pdf("CurrentDesk")).el('txtEqType').value);
    oCallback.add("oldCodeId", pdfs("wfFrame" + pdf("CurrentDesk")).el('hdnCodeId').value);
    oCallback.add("oldCodeCd", pdfs("wfFrame" + pdf("CurrentDesk")).el('txtEqCode').value);
    oCallback.add("oldCustomerId", pdfs("wfFrame" + pdf("CurrentDesk")).el('hdnCustomerId').value);
    oCallback.add("oldCustomerCd", pdfs("wfFrame" + pdf("CurrentDesk")).el('txtCustomer').value);
    oCallback.add("BillingBit", pdfs("wfFrame" + pdf("CurrentDesk")).el('hdnBillingBit').value);
    oCallback.add("EffectiveFromDate", pdfs("wfFrame" + pdf("CurrentDesk")).el('datEffectiveFrom').value); //Release 3 - Equipment Update
    oCallback.add("Reason", Reason);

    oCallback.invoke("EquipmentUpdate.aspx", "UpdateEquipmentUpdate");
    if (oCallback.getCallbackStatus()) {
        ppsc().closeModalWindow();
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        if (Reason != '') {
            pdfs("wfFrame" + pdf("CurrentDesk")).el('txtNewEqpNo').value = "";
            pdfs("wfFrame" + pdf("CurrentDesk")).el('lkpCustomer').SelectedValues[0] = "";
            pdfs("wfFrame" + pdf("CurrentDesk")).el('lkpCustomer').value = "";
            pdfs("wfFrame" + pdf("CurrentDesk")).el('lkpEqpType').SelectedValues[0] = "";
            pdfs("wfFrame" + pdf("CurrentDesk")).el('lkpEqpType').value = "";
            pdfs("wfFrame" + pdf("CurrentDesk")).el('lkpEqpType').SelectedValues[0] = "";
            pdfs("wfFrame" + pdf("CurrentDesk")).el('txtNewEqpCode').value = "";
            pdfs("wfFrame" + pdf("CurrentDesk")).bindEquipmentUpdate("Search");
        }
        else {
            clearValues();
        }
        resetHasChanges("ifgEquipmentUpdate");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return true;
}

function onSearchClick() {
    if (el('txtEquipmentNo').value != "" && el('txtEquipmentNo').value != null) {
        resetValidators();
        el('txtNewEqpNo').value = "";
        if (!Page_IsValid) {
            hideDiv("divEquipmentUpdate");
            hideDiv("divStatusDetails");
            return false;
        }
        //UIG Fix  for the Chrome Issue No:58
        if (getText(el('btnSearch')) == "Search") {
            bindEquipmentUpdate("Search");
            reSizePane();
            lockDataUpdate(el('txtEquipmentNo').value);
            setReadOnly("txtEquipmentNo", true);
            setText(el('btnSearch'), "Reset");
        }
        else if (getText(el('btnSearch')) == "Reset") {
            onResetClick();
            setReadOnly("txtEquipmentNo", false);
            setText(el('btnSearch'), "Search");
            setFocusToField("txtEquipmentNo");
        }

        clearTextValues("datEffectiveFrom");
    }
    else {
        showErrorMessage("Equipment Number Required");
    }
}

function bindEquipmentUpdate(mode) {
    var activtyName = getQueryStringValue(document.location.href, "activityname");
    var objGrid = new ClientiFlexGrid("ifgEquipmentUpdate");
    objGrid.parameters.add("EquipmentNo", el("txtEquipmentNo").value);
    objGrid.parameters.add("Mode", mode);
    objGrid.parameters.add("ActivtyName", activtyName);
    objGrid.DataBind();
}

function onResetClick() {
    hideDiv("divRecordNotFound");
    hideDiv("divEquipmentUpdate");
    hideDiv("divStatusDetails");
    hideDiv("divBillingBit");
    hideDiv("divEqpInformation");
    hideDiv("divSubmit");
    bindEquipmentUpdate("Reset");
    clearTextValues("txtEquipmentNo");
    resetValidators('divEquipmentUpdate');
    hideDiv("divLockMessage");
    clearValues();
}

function clearValues() {
    clearTextValues("txtNewEqpNo");
    clearLookupValues("lkpCustomer");
    clearLookupValues("lkpEqpType");
    clearTextValues("txtNewEqpCode");
    clearTextValues("datEffectiveFrom");
}


function ifgEquipmentUpdateOnAfterCB(param) {
    if (typeof (param["norecordsfound"]) != 'undefined') {
        var norecordsfound = param["norecordsfound"];
    }
    if (typeof (param["preadvice"]) != 'undefined') {
        var preadvice = param["preadvice"];
    }

    if (norecordsfound == "true") {
        showDiv("divRecordNotFound");
        hideDiv("divEqpInformation");
        hideDiv("divEquipmentUpdate");
        hideDiv("divStatusDetails");
        //        el('btnSearch').innerText = " Reset"
        setText(el('btnSearch'), "Reset");
        setReadOnly("txtEquipmentNo", true);
        return false;
    }
    else if (preadvice == "true") {
        showDiv("divEqpInformation");
        hideDiv("divEquipmentUpdate");
        hideDiv("divStatusDetails");
        hideDiv("divRecordNotFound");
    }
    else {
        showDiv("divEquipmentUpdate");
        showDiv("divStatusDetails");
        if (param["BillingBit"] == 'True') {
            el('hdnBillingBit').value = param["BillingBit"];
            showDiv("divBillingBit");
            hideDiv("divStatusDetails");
            hideDiv("divEquipmentUpdate");
            hideDiv("divSubmit");
        }
        else {
            el('hdnBillingBit').value = 'False';
            hideDiv("divBillingBit");
            showDiv("divStatusDetails");
            showDiv("divEquipmentUpdate");
        }
        if (typeof (param["Status"]) != 'undefined') {
            el('txtCurrentStatus').value = param["Status"];
        }
        if (typeof (param["ActivityDate"]) != 'undefined') {
            // el('datCurrentActivityDate').innerText = param["ActivityDate"];
            // setText(el('datCurrentActivityDate'), param["ActivityDate"]);
            el('datCurrentActivityDate').value = param["ActivityDate"];
        }
        if (typeof (param["Customer"]) != 'undefined') {
            el('txtCustomer').value = param["Customer"];
        }
        if (typeof (param["Type"]) != 'undefined') {
            el('txtEqType').value = param["Type"];
        }
        if (typeof (param["Code"]) != 'undefined') {
            el('txtEqCode').value = param["Code"];
        }
        if (typeof (param["TypeId"]) != 'undefined') {
            el('hdnTypeId').value = param["TypeId"];
        }
        if (typeof (param["CodeId"]) != 'undefined') {
            el('hdnCodeId').value = param["CodeId"];
        }
        if (typeof (param["CustomerId"]) != 'undefined') {
            el('hdnCustomerId').value = param["CustomerId"];
        }
        if (el('txtCurrentStatus').value == "") {
            showDiv("divEqpInformation");
            hideDiv("divStatusDetails");
            hideDiv("divEquipmentUpdate");
        }
        else if (el('txtCurrentStatus').value != "" && param["BillingBit"] == 'True' && recordLockChanges == true) {
            hideDiv("divSubmit");
        }
        else if (el('txtCurrentStatus').value != "" && param["BillingBit"] == 'False' && recordLockChanges == true) { //Added for record locking
            hideDiv("divSubmit");
        }
        else {
            showDiv("divSubmit");
        }
    }
}

function validateEquipmentno(oSrc, args) {
    var sEquipmentno = args.Value
    var oldEquipmentNo = el('txtEquipmentNo').value;
    var checkDigit;
    var sContNo;
    var strContinue = "";
    var msg = checkContainerNo(sEquipmentno);

    if (msg == "") {
        if (getConfigSetting('ContainerCheckDigit') == 'TRUE') {
            if (sEquipmentno.length == 10) {
                sEquipmentno = sEquipmentno + getCheckSum(sEquipmentno.substr(0, 10));
                el('txtNewEqpNo').value = sEquipmentno;
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
    if (oldEquipmentNo != sEquipmentno) {
        var oCallback = new Callback();
        oCallback.add("EquipmentNo", sEquipmentno);
        oCallback.invoke("EquipmentUpdate.aspx", "ValidateEquipment");
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
            else {
                args.IsValid = false;
                oSrc.errormessage = "Equipment already exists in Supplier/Equipment Information.";
            }
        }
        else {
            showErrorMessage(oCallback.getCallbackError());
        }
        oCallback = null;

    }
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

function calculateNextTestDate() {
    var rIndex = ifgEquipmentUpdate.CurrentRowIndex();
    var cColumns = ifgEquipmentUpdate.Rows(rIndex).GetClientColumns();
    var months = new Array(12);
    months[0] = "JAN";
    months[1] = "FEB";
    months[2] = "MAR";
    months[3] = "APR";
    months[4] = "MAY";
    months[5] = "JUN";
    months[6] = "JUL";
    months[7] = "AUG";
    months[8] = "SEP";
    months[9] = "OCT";
    months[10] = "NOV";
    months[11] = "DEC";
    var LstTstDate = new Date();
    var result = "";
    if (cColumns[4] != "") {
        var _arr = cColumns[4].split('-');
        var dt = _arr[1] + ',' + _arr[0] + ',' + _arr[2];
        var newDate = new Date(dt);
        newDate.setMonth(newDate.getMonth() + 29);
        var nxtTestDate = newDate.getDate() + "-" + months[newDate.getMonth()] + "-" + newDate.getFullYear();
        ifgEquipmentUpdate.Rows(rIndex).SetColumnValuesByIndex(6, nxtTestDate);
    }
    else {
        ifgEquipmentUpdate.Rows(rIndex).SetColumnValuesByIndex(6, "");
    }
}

function applyCustomer() {
    return "CSTMR_CD != '" + el('txtCustomer').value + "'";
}

function bindTestType() {
    var rIndex = ifgEquipmentUpdate.CurrentRowIndex();
    var cColumns = ifgEquipmentUpdate.Rows(rIndex).GetClientColumns();
    var _arr = new Array();
    if (cColumns[5] == "PNEUMATIC") {
        _arr[0] = "HYDRO";
        _arr[1] = "51";
        ifgEquipmentUpdate.Rows(rIndex).SetColumnValuesByIndex(5, "5");
    }
    else if (cColumns[5] == "HYDRO") {
        _arr[0] = "PNEUMATIC";
        _arr[1] = "52";
        ifgEquipmentUpdate.Rows(rIndex).SetColumnValuesByIndex(5, "2.5");
    }
    else if (cColumns[5] == "") {
        _arr[0] = "";
        _arr[1] = "";
        ifgEquipmentUpdate.Rows(rIndex).SetColumnValuesByIndex(5, "");
    }
    ifgEquipmentUpdate.Rows(rIndex).SetColumnValuesByIndex(7, _arr);
}


function updateEquipmentReason() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    UpdateEquipment(el('txtReason').value);
}

function applyEquipmentType() {
    //    return "EQPMNT_TYP_CD !='" + el('txtEqType').value + "'"; 
    return " not (EQPMNT_TYP_CD ='" + el('txtEqType').value + "' and EQPMNT_CD_CD ='" + el('txtEqCode').value + "')";
}

function applyEquipmentCode() {
    return "EQPMNT_CD_CD != '" + el('txtEqCode').value + "'"
}


//Release -3 Effective Date Validation

function validateEffectiveDate(oSrc, args) {

    if (args.Value != "") {

        var effective_dt = args.Value;
        var eqpmnt_no = el('txtEquipmentNo').value;
        var old_Equipment = el('txtEquipmentNo').value;
        var oCallback = new Callback();
        oCallback.add("OldEquipmentNo", old_Equipment);
        oCallback.add("EquipmentNo", eqpmnt_no);
        oCallback.add("EffectiveDate", effective_dt);


        oCallback.invoke("EquipmentUpdate.aspx", "validateEffective_Date");

        if (oCallback.getCallbackStatus()) {
            args.IsValid = true;
        }
        else {
            showErrorMessage(oCallback.getReturnValue("Message"));
            args.IsValid = false;
        }
    }
    else {


        if (el("lkpCustomer").value != "") {
            showErrorMessage("Effective From Date Required");
            args.IsValid = false;
        }


    }
}

//Record Locking
function lockDataUpdate(equipmentNo) {
    var oCallback = new Callback();
    oCallback.add("EquipmentNo", equipmentNo);
    oCallback.add("ActivityName", getQueryStringValue(document.location.href, "activityname"));
    oCallback.invoke("EquipmentUpdate.aspx", "RecordLockData");
    if (oCallback.getCallbackStatus()) {
        el("hdnLockUserName").value = oCallback.getReturnValue("UserName");
        el('hdnIpError').value = oCallback.getReturnValue("IPError");
        el('hdnLockActivityName').value = oCallback.getReturnValue("ActivityName");
        recordLockError(equipmentNo, oCallback.getReturnValue("UserName"), oCallback.getReturnValue("IPError"), oCallback.getReturnValue("ActivityName"));
    }
    else {
        el("hdnLockUserName").value = "";
        el('hdnIpError').value = "";
        el('hdnLockActivityName').value = "";
        showErrorMessage(oCallback.getCallbackError());
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
        if ($(window.parent)!= undefined) {
            if ($(window.parent).height() < 680) {
                if (el("tabEquipmentUpdate") != null) {
                    el("tabEquipmentUpdate").style.height = $(window.parent).height() - 253 + "px";
                }
                //                el("tabEquipmentUpdate").style.height = "980px";
                if (el("ifgEquipmentUpdate") != null) {
                    el("ifgEquipmentUpdate").SetStaticHeaderHeight($(window.parent).height() - 535 + "px");
                }
            }
            else if ($(window.parent).height() < 768) {
                if (el("tabEquipmentUpdate") != null) {
                    el("tabEquipmentUpdate").style.height = $(window.parent).height() - 350 + "px";
                }
                if (el("ifgEquipmentUpdate") != null) {
                    el("ifgEquipmentUpdate").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
                }
            }
            else {
                if (el("tabEquipmentUpdate") != null) {
                    el("tabEquipmentUpdate").style.height = $(window.parent).height() - 254 + "px";
                }
                if (el("ifgEquipmentUpdate") != null) {
                    el("ifgEquipmentUpdate").SetStaticHeaderHeight($(window.parent).height() - 540 + "px");
                }
            }
        }
    }
}


//Type & Code Merge
function setEquipmentCode(oSrc, args) {
//    DBC();
    var TypeId = el('lkpEqpType').SelectedValues[0];
    var TypeCode = el('lkpEqpType').SelectedValues[1];
    var textVal = el('lkpEqpType').value;

    var oldCode = el('txtEqCode').value;

    if (TypeCode != textVal) {



        var oCallback = new Callback();
        oCallback.add("Type", args.Value);
        oCallback.add("OldCode", oldCode);
        oCallback.invoke("EquipmentUpdate.aspx", "GetEquipmentCode");

        if (oCallback.getCallbackStatus()) {

            el("txtNewEqpCode").value = oCallback.getReturnValue("Code");
            el('lkpEqpType').SelectedValues[0] = oCallback.getReturnValue("ID");
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Please Configure Equipment Type & Code";
        }

     
    }
    else {
        var oCallback = new Callback();
        oCallback.add("TypeID", TypeId);
        oCallback.invoke("EquipmentUpdate.aspx", "GetEquipmentCodebyTypeId");

        if (oCallback.getCallbackStatus()) {
            el("txtNewEqpCode").value = oCallback.getReturnValue("Code");
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "Please Configure Equipment Type & Code";
        }
      
    }



//    alert("ID : " + TypeId + " Code : " + TypeCode + " Value : " + tex);
}