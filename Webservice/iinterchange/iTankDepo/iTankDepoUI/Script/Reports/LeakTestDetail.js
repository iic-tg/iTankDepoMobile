var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgLeakTest');
var _RowValidationFails = false;

function initPage(sMode) {

    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    hideDiv("divGenerateDoc");
    reSizePane();
}
function FetchLeakTest() {
    //GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getText(el('btnSubmit')) == "Fetch") {
        // setText(el('btnSubmit'), "Reset");
        if (el('lkpCustomer').value != "" || el('txtEquipmentNo').value != "") {
            var oCallback = new Callback();
            oCallback.add("Customer", el('lkpCustomer').SelectedValues[0]);
            oCallback.add("InDateFrom", el('datInDateFrom').value);
            oCallback.add("InDateTo", el('datInDateTo').value);
            oCallback.add("TestDateFrom", el('datTestDateFrom').value);
            oCallback.add("TestDateTo", el('datTestDateTo').value);
            oCallback.add("EquipemntNo", el('txtEquipmentNo').value);
            oCallback.add("DepotID", el('lkpDepotCode').SelectedValues[0]);
            oCallback.invoke("LeakTestDetail.aspx", "FetchLeakTest");
            if (oCallback.getCallbackStatus()) {
                BindLeakTest();
            }
        }
        else {
            showErrorMessage('Customer or Equipment No is required');
          //  setFocusToField("lkpSearchStatus");
        } 

    }

    reSizePane();
}
function BindLeakTest() {
    var objifgLeakTest = new ClientiFlexGrid("ifgLeakTest");
    objifgLeakTest.parameters.add("Customer", el('lkpCustomer').SelectedValues[0]);
    objifgLeakTest.DataBind();
    var rowCount = ifgLeakTest.Rows().Count;
    if (rowCount > 0) {       
        showDiv("divLeaktest");
        showDiv("divGenerateDoc");
        hideDiv("divRecordNotFound");
    }
    else {       
        hideDiv("divLeaktest");
        hideDiv("divGenerateDoc");
        showDiv("divRecordNotFound");       
    }
}
function printDocument() {
    ifgLeakTest.Submit(true)
    var rowCount = ifgLeakTest.Rows().Count;
    if (rowCount > 0) {
        var oCallback = new Callback();
        oCallback.invoke("LeakTestDetail.aspx", "GetLeakTestDetail");
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
    }
    else {
        showErrorMessage("Please select at least one ");
        return false;
    }
    return true;
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
function clearValues() {    
    clearLookupValues("lkpCustomer");
    clearTextValues("datInDateFrom");
    clearTextValues("datInDateTo");
    clearTextValues("datTestDateFrom");
    clearTextValues("datTestDateTo");
    clearTextValues("txtEquipmentNo");
    if (el('hdnHQID').value > 0) {
        clearTextValues("lkpDepotCode");
    } 
    hideDiv("divRecordNotFound");
    hideDiv("divLeaktest");
    hideDiv("divGenerateDoc");
}
function validateInDateTo(oSrc, args) {
    var Infromdate;

    Infromdate = el('datInDateFrom').value;
    if (el('datInDateTo').value != Infromdate) {
        if (!DateCompare(el('datInDateTo').value, Infromdate)) {
            showErrorMessage("IN Date To should be greater or equal to IN Date From(" + Infromdate + ")");
            args.IsValid = false;
            return false;
        }
    }
}
function validateTestDateTo(oSrc, args) {
    var Testfromdate;
    Testfromdate = el('datTestDateFrom').value;
    if (el('datTestDateTo').value != Testfromdate) {
        if (!DateCompare(el('datTestDateTo').value, Testfromdate)) {
            showErrorMessage("Test To Date should be greater or equal to Test From Date(" + Testfromdate + ")");
            args.IsValid = false;
            return false;
        }
    }
}

//function reSizePane() {
//    if ($(window) != null) {
//        if ($(window.parent).height() < 768) {
//            el("tabChangeofStatus").style.height = $(window.parent).height() - 250 + "px";
//            if (el("ifgEquipmentDetail") != null) {
//                el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 450 + "px"); ;
//            }
//        }
//        else {
//            el("tabChangeofStatus").style.height = $(window.parent).height() - 280 + "px";
//            if (el("ifgEquipmentDetail") != null) {
//                el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 480 + "px");
//            }
//        }
//    }

//}
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
            if (el("ifgLeakTest") != null) {
                if (!chrome) {
                    el("ifgLeakTest").SetStaticHeaderHeight($(window.parent).height() - 396 + "px");
                }
                else {
                    el("ifgLeakTest").SetStaticHeaderHeight($(window.parent).height() - 403 + "px");
                }
            }
        }       
        else {           
            if (el("ifgLeakTest") != null) {
                if (!chrome) {
                    el("ifgLeakTest").SetStaticHeaderHeight($(window.parent).height() - 395 + "px");
                }
                else {
                    el("ifgLeakTest").SetStaticHeaderHeight($(window.parent).height() - 406 + "px");
                }
            }

        }
    }

}
