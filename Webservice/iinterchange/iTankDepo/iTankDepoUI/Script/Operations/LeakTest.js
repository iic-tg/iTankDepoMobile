var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgLeakTest');
var _RowValidationFails = false;
var _PageMode;
var _EquipmentNo;
var _GateinTransaction;



function initPage(sMode) {
    if (sMode == 'Repair') {
        _EquipmentNo = getQueryStringValue(document.location.href, "EquipmentNo");
        _GateinTransaction = getQueryStringValue(document.location.href, "GateinTransaction");


        if (_EquipmentNo == '') {
            _PageMode = "edit";
        } else {
            _PageMode = "Repair";
            bindLeakTest(sMode, _GateinTransaction, _EquipmentNo);
        }
    }
    else {
        bindLeakTest(sMode, "", "");
        el("lstBack").style.display = "none";
        el("lstFirst").style.display = "none";
        el("lstPrev").style.display = "none";
        el("lstNext").style.display = "none";
        el("lstLast").style.display = "none";
        showSubmitPrintButton(false);
        _PageMode = "edit";
    }
    el('hdnPageName').value = sMode;
    reSizePane();
}

function onSubmit() {
    pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = ifgLeakTest.hasChanges;
    submitPage();
}

function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        if (ifgLeakTest.Submit() == false || _RowValidationFails) {
            return false;
        }
        return UpdateLeakTest();
    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

function printDocument() {
    ifgLeakTest.Submit(true)
    var oCallback = new Callback();
    oCallback.add("WFData", getWFDATA());
    oCallback.invoke("LeakTest.aspx", "UpdateGenerationDetail");
    if (oCallback.getCallbackStatus()) {
        HasChanges = false;
        resetHasChanges("ifgLeakTest");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;

    printLeakTest();
    bindLeakTest("edit");
    return true;
}

function UpdateLeakTest() {
    var oCallback = new Callback();
    oCallback.add("ActivityName", getQueryStringValue(document.location.href, "activityname"));
    oCallback.invoke("LeakTest.aspx", "UpdateLeakTest");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        resetHasChanges("ifgLeakTest");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    bindLeakTest(_PageMode, _GateinTransaction, _EquipmentNo);
    reSizePane();
    return true;
}

function bindLeakTest(mode, GateinTransaction, EquipmentNo) {
    var objGrid = new ClientiFlexGrid("ifgLeakTest");
    objGrid.parameters.add("Mode", mode);
    objGrid.parameters.add("GateinTransaction", GateinTransaction);
    objGrid.parameters.add("EquipmentNo", EquipmentNo);
    objGrid.DataBind();
}

//This function is to Validate the Equipment No for Duplicates
function validateEquipmentno(oSrc, args) {
    var cols = ifgLeakTest.Rows(ifgLeakTest.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgLeakTest.rowIndex;
    var sEquipmentno = args.Value
    var checkDigit;
    var sContNo;

    var rowState = ifgLeakTest.ClientRowState();
    var oCallback = new Callback();

    oCallback.add("EquipmentId", sEquipmentno);
    oCallback.add("GridIndex", ifgLeakTest.VirtualCurrentRowIndex());
    oCallback.add("RowState", rowState);
    oCallback.invoke("LeakTest.aspx", "ValidateEquipment");
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

function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgLeakTest.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgLeakTest.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
        ifgLeakTest.Rows(iCurrentIndex).SetColumnValuesByIndex(6, el("hdnCurrentDate").value);
        ifgLeakTest.Rows(iCurrentIndex).SetColumnValuesByIndex(12, "0");
        ifgLeakTest.Rows(iCurrentIndex).SetColumnValuesByIndex(15, true);
    }
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

function showRevisionHistory() {
    icount = ifgLeakTest.Rows().Count;
    if (icount >= 0) {
        var rowIndex = ifgLeakTest.CurrentRowIndex();
        var cols = ifgLeakTest.Rows(rowIndex).Columns();
        showModalWindow("Revision History", "Operations/LeakTestRevision.aspx?EquipmentNo=" + cols[1] + "&GateinTransaction=" + cols[2], "995px", "350px", "0px", "", "");
        HasChanges = true;
        psc().hideLoader();
    }
}

function printLeakTest() {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "LeakTest";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Leak Test Report";
    oDocPrint.DocumentId = "23";
    oDocPrint.ReportPath = "../Documents/Report/LeakTest.rdlc";
    oDocPrint.openReportDialog();
}

function OnAfterCallBack(param, mode) {
    if (mode == "Delete") {
        if (typeof (param["Delete"]) != 'undefined') {
            showWarningMessage(param["Delete"]);
            return;
        }
    }

}

function showRepairMessage() { //Message
    showErrorMessage('Revision Details can be viewed only from the Leak Test Entry Screen');
}

function validateStatusDate(oSrc, args) {
    var pageName = el('hdnPageName').value;
    var cmpDate = new Date;
    var errDate = "";
    var rIndex = ifgLeakTest.CurrentRowIndex();
    if (pageName == "Repair") {
        var cols = ifgLeakTest.Rows(rIndex).Columns();
        cmpDate = cols[29].toUpperCase();
        errDate = cmpDate;
    }
    else {
        var cols = ifgLeakTest.Rows(rIndex).GetClientColumns();
        cmpDate = cols[4];
        errDate = cmpDate;
    }
    if (DateCompareEqual(args.Value, cmpDate)) {
        args.IsValid = true;
        return;
    }
    else {
        args.IsValid = false;
        oSrc.errormessage = "Test Date should be greater than or equal to In Date (" + errDate + ")";
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
        if (_PageMode == "edit") {
             if ($(window.parent).height() < 680) {
                el("tabLeaktest").style.height = $(window.parent).height() - 244 + "px";
                if (el("ifgLeakTest") != null) {
                    el("ifgLeakTest").SetStaticHeaderHeight($(window.parent).height() - 294 + "px");
                }
            }
            else if($(window.parent).height() < 768) {
                el("tabLeaktest").style.height = $(window.parent).height() - 350 + "px";
                if (el("ifgLeakTest") != null) {
                    el("ifgLeakTest").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
                }
            }
            else {
                el("tabLeaktest").style.height = $(window.parent).height() - 254 + "px";
                if (el("ifgLeakTest") != null) {
                    el("ifgLeakTest").SetStaticHeaderHeight($(window.parent).height() - 304 + "px");
                }
            }
        }
    }

}
function EquipmentFilter() {
     var DPT_ID = getQueryStringValue(getWFDATA(), "DPT_ID");
      var strFilter;
         strFilter = " DPT_ID IN  (" + DPT_ID + ")";
    return strFilter;
}