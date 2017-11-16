var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgEquipmentInformation');
var _RowValidationFails = false;
var EqpmntTypeID = "";
var pageName = "";
var _gateinId = "";
var _eqpmntNo = "";
var _attachment = "";
var _gateinNo = "";


if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}
//This function is used to intialize the page.
function initPage(sMode) {
   
    if (sMode == "GateIn") {
        _attachment = getQueryStringValue(document.location.href, "Attachment");
        _gateinId = getQueryStringValue(document.location.href, "GateInId");
        _eqpmntNo = getQueryStringValue(document.location.href, "EquipmentNo");
        _gateinNo = getQueryStringValue(document.location.href, "GateinTransactionNo");
        if (_eqpmntNo == "" || _eqpmntNo==null) {//added and changed for UIG Fix
            sMode = "edit";
           
        } else {
            sMode = "GateIn";
            bindEquipmentInformation(sMode);
            ifgEquipmentInformation.hasChanges = getQueryStringValue(document.location.href, "HasChanges");
            el("ITab1_0").style.display = "none";
        }
        //  setDefaultGatein();
        
    } else {
        el("lstBack").style.display = "none";
        el("lstFirst").style.display = "none";
        el("lstPrev").style.display = "none";
        el("lstNext").style.display = "none";
        el("lstLast").style.display = "none";
        showSubmitPrintButton(false);
        bindEquipmentInformation(sMode);
    }
    pageName = sMode;
    reSizePane();
   
}

function onSubmit() {
    pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = ifgEquipmentInformation.hasChanges;
    submitPage();
}

function setDefaultGatein() {
    // ifgEquipmentInformation.Rows(0).SetColumnValuesByIndex(16, true);
    ifgEquipmentInformation.Rows(0).SetColumnValuesByIndex(17, true);
 }

//This function is used to submit the page to the server.
function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        if (ifgEquipmentInformation.Submit() == false || _RowValidationFails) {
            return false;
        }
        return UpdateEquipmentInformation();
    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

//This function is used to Update the changes to the server. 
function UpdateEquipmentInformation() {
    ifgEquipmentInformation.Submit();
    var oCallback = new Callback();
    oCallback.invoke("EquipmentInformation.aspx", "UpdateEquipmentInformation");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        var id = getQueryString();
        if (id != "" && id != null) {
            bindEquipmentInformation("GateIn");
        }
        else {
            bindEquipmentInformation("rebind");
        }

        resetHasChanges("ifgEquipmentInformation");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

//This function is used to bind the Equipment Detail Grid.
function bindEquipmentInformation(mode) {
    var objGrid = new ClientiFlexGrid("ifgEquipmentInformation");
    objGrid.parameters.add("EquipmentNo", el("hdnEquipmentNo").value);
    objGrid.parameters.add("GateinTransactionNo", _gateinNo);
    objGrid.parameters.add("Mode", mode);
    if (_attachment == "TRUE") {
        objGrid.parameters.add("Attachment", "TRUE");
    }
    else {
        objGrid.parameters.add("Attachment", "FALSE");
    }

    objGrid.DataBind();
}

//This function is to Validate the Equipment No for Duplicates
function validateEquipmentno(oSrc, args) {
    var cols = ifgEquipmentInformation.Rows(ifgEquipmentInformation.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgEquipmentInformation.rowIndex;
    var sEquipmentno = args.Value
    var checkDigit;
    var sContNo;
    var strContinue = "";
    var msg = checkContainerNo(sEquipmentno);

    if (msg == "") {
        if (getConfigSetting('ContainerCheckDigit').toUpperCase() == 'TRUE') {
            if (sEquipmentno.length == 10) {
                sEquipmentno = sEquipmentno + getCheckSum(sEquipmentno.substr(0, 10));
                ifgEquipmentInformation.Rows(_rowI).SetColumnValuesByIndex(0, sEquipmentno);
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

    var rowState = ifgEquipmentInformation.ClientRowState();
    var oCallback = new Callback();

    oCallback.add("EquipmentId", sEquipmentno);
    oCallback.add("GridIndex", ifgEquipmentInformation.VirtualCurrentRowIndex());
    oCallback.add("RowState", rowState);
    oCallback.invoke("EquipmentInformation.aspx", "ValidateEquipment");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("bNotExists") == "true") {
            args.IsValid = true;
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = "This Equipment No already Exist";
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

//This function is used to Set the Default Values in the Grid.
function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgEquipmentInformation.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
        ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(1, false);
        ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(7, false);
        ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(3, false);
        ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(4, false);
        ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(6, false);
       
        if (getConfigSetting('053') != "True") {
//            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(5, false);
//            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(7, false);
//            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(8, false);
//            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(9, true);
            //            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(10, true);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(6, false);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(11, true);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(8, false);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(9, false);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(10, true);
//            cell1 = Row.cells[16];
        }
        if (getConfigSetting('053') == "True") {
//                    ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(11, false);
//                    ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(12, false);
            //                    ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(13, false);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(14, false);
          //  ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(12, false);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(13, false);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetColumnValuesByIndex(2, getConfigSetting('DefaultEquipmentCode'));
        }
        var id = getQueryString();
        if (id != "") {

//            ifgEquipmentInformation.Rows(iCurrentIndex).SetColumnValuesByIndex(16, true);
            //            ifgEquipmentInformation.Rows(iCurrentIndex).SetColumnValuesByIndex(17, true);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetColumnValuesByIndex(18, true);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetColumnValuesByIndex(17, true);
         
        }
        else {
//            ifgEquipmentInformation.Rows(iCurrentIndex).SetColumnValuesByIndex(16, true);
            //            ifgEquipmentInformation.Rows(iCurrentIndex).SetColumnValuesByIndex(17, false);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetColumnValuesByIndex(17, true);
            ifgEquipmentInformation.Rows(iCurrentIndex).SetColumnValuesByIndex(18, false);

        }

        ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(18, false);
        //  ifgEquipmentInformation.Rows(iCurrentIndex).SetReadOnlyColumn(17, false);
        ifgEquipmentInformation.Rows(iCurrentIndex).SetColumnValuesByIndex(1, getConfigSetting('DefautEqType'));
        var rIndex = new Number;
        rIndex = parseInt(iCurrentIndex) + parseInt(1);
        var table = document.getElementById('ifgEquipmentInformation');
        var Row = table.rows[rIndex];
        var cell1;
        if (getConfigSetting('053') == "True") {
            cell1 = Row.cells[12];
        }
        else {
            cell1 = Row.cells[13];
        }
//       cell1 = Row.cells[16];
   // cell1 = Row.cells[15];
        cell1.innerHTML = "<img src='../Images/noattachment.png' align='right'/>";
    }
    return true;
}

//This function used to Hide the Detail Grid when it has no Rows to display.
function ifgEquipmentInformationOnAfterCB(param, mode) {

    if (typeof (param["Error"]) != 'undefined') {
        showErrorMessage(param["Error"]);
        return false;
    }
    if (typeof (param["Success"]) != 'undefined') {
        showWarningMessage(param["Success"]);
        return false;
    }

    if (typeof (param["norecordsfound"]) != 'undefined') {
        var norecordsfound = param["norecordsfound"];
        if (norecordsfound == "True") {
            el("divEquipmentInformation").style.display = "none";
            el("divRecordNotFound").style.display = "block";
        }
    }
}

function ifgEquipmentInformationOnBeforeCB(mode, param) {
    resetHasChanges("ifgEquipmentInformation");
    if (mode == "Delete") {
        var _cols = ifgEquipmentInformation.Rows(ifgEquipmentInformation.rowIndex).Columns();
        param.add("EquipmentID", _cols[0]);
        param.add("EquipmentNo", _cols[1]);
    }
}

function printEquipmentInformation() {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "";
    oDocPrint.Type = "document";
    oDocPrint.Title = "";
    oDocPrint.DocumentId = "";
    oDocPrint.ReportPath = "";
    oDocPrint.openReportDialog();
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

function makeReadOnly() {
    var _index = ifgEquipmentInformation.Rows().Count;
    for (var i = 0; i < _index; i++) {
        ifgEquipmentInformation.Rows(i).SetReadOnlyColumn(0, true);
    }
}

function bindTestType() {
    var testType = "";
    var rIndex = ifgEquipmentInformation.CurrentRowIndex();
    var cColumns = ifgEquipmentInformation.Rows(rIndex).GetClientColumns();
    var _arr = new Array();
    if (cColumns[11].toUpperCase() == "PNEUMATIC") {
        _arr[0] = "Hydro";
        _arr[1] = "Hydro";
    }
    else if (cColumns[11].toUpperCase() == "HYDRO") {
        _arr[0] = "Pneumatic";
        _arr[1] = "Pneumatic";
    }
    else if (cColumns[9] == "") {
        _arr[0] = "";
        _arr[1] = "";
    }
    ifgEquipmentInformation.Rows(rIndex).SetColumnValuesByIndex(11, _arr[0]);
}

function calculateNextTestDate() {
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
    var rIndex = ifgEquipmentInformation.CurrentRowIndex();
    var cColumns = ifgEquipmentInformation.Rows(rIndex).GetClientColumns();
    if (cColumns[10] != "") {
        var _arr = cColumns[10].split('-');
        var dt = _arr[1] + ',' + _arr[0] + ',' + _arr[2];
        var newDate = new Date(dt);
        newDate.setMonth(newDate.getMonth() + 30);
        var nxtTestDate = newDate.getDate() + "-" + months[newDate.getMonth()] + "-" + newDate.getFullYear();//Changed by Sakthivel on 15-OCT-2014 for Date Loaded incorrectly
        ifgEquipmentInformation.Rows(rIndex).SetColumnValuesByIndex(10, nxtTestDate);
    }
    else {
        ifgEquipmentInformation.Rows(rIndex).SetColumnValuesByIndex(9, "");
    }
}

function openEquipmentInformation(equipmentInformationId, equipmentNo, pageMode) {
    if (pageMode == "GateIn") {
        showInfoMessage("Attachment can not be done from Gate In");
        return false;
    }
    if (_gateinId != "") {
        equipmentNo = _eqpmntNo;
        equipmentInformationId = 0;
    }
    var obj = true;
    el("hdnEquipmentNo").value = equipmentNo;
    showModalWindow("Equipment Information", "Operations/EquipmentInformationDetail.aspx?EquipmentInformationId=" + equipmentInformationId + "&PageName=" + pageName + "&GateInId=" + _gateinId + "&EquipmentNo=" + equipmentNo, "880px", "500px", "50px", "", "", "", "wfs().setAttachmentDisplay(" + obj + ")");
    psc().hideLoader();
}

function setAttachmentDisplay(obj) {
    var getMode = getQueryString();
    var attachment = "";
//    if (getMode == "") {
//        attachment = "FALSE";
//    }
//    else {
//        attachment = "TRUE";
//    }
    if (obj == true) {
        var objGrid = new ClientiFlexGrid("ifgEquipmentInformation");
        objGrid.parameters.add("EquipmentNo", el("hdnEquipmentNo").value);
        objGrid.parameters.add("Attachment", "TRUE");
        objGrid.DataBind();
    }
}

function getQueryString() {
    var id = getQueryStringValue(document.location.href, "GateInId");
    return id;
}
$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent)){
        if (_eqpmntNo == "" || _eqpmntNo == null) {
            if ($(window.parent).height() < 680) {
                el("tabEquipmentInformation").style.height = $(window.parent).height() - 235 + "px";
                if (el("ifgEquipmentInformation") != null) {
                    el("ifgEquipmentInformation").SetStaticHeaderHeight($(window.parent).height() - 292 + "px");
                }
            }
            else if ($(window.parent).height() < 768) {
                el("tabEquipmentInformation").style.height = $(window.parent).height() - 350 + "px";
                if (el("ifgEquipmentInformation") != null) {
                    el("ifgEquipmentInformation").SetStaticHeaderHeight($(window.parent).height() - 460 + "px");
                }
            }
            else {
                el("tabEquipmentInformation").style.height = $(window.parent).height() - 239 + "px";
                if (el("ifgEquipmentInformation") != null) {
                    el("ifgEquipmentInformation").SetStaticHeaderHeight($(window.parent).height() - 300 + "px");
                }
            }
        }
    }
    }
}

//Set Equipment Code
function setEquipmentCode(oSrc, args) {
    if (getConfigSetting('053') == "True") {
        var cols = ifgEquipmentInformation.Rows(ifgEquipmentInformation.CurrentRowIndex()).GetClientColumns();

        if (cols[2] == "" || cols[2] == undefined) {
            ifgEquipmentInformation.Rows(ifgEquipmentInformation.CurrentRowIndex()).SetColumnValuesByIndex(2, getConfigSetting('DefaultEquipmentCode'));
        }
    }
    return false;

}