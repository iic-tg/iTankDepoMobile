var HasChanges = false;
var _RowValidationFails = false;
var HasNoteChanges = false;
var Description = "";


if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}

//This function is used to intialize the page.
function initPage(sMode) {
    
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    el("divProduct").style.display = "none";
    el("divCustomer").style.display = "none";
    el("divRoute").style.display = "none";
    //    el("hypReset").style.visibility = "hidden";
    setFocusToField("lkpEnquiryType");
    resetValidators();
}
// Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :57
function Fetch() {
    //if (el('hypFetch').innerText == " Fetch") {
    if (getText(el('hypFetch')) == "Fetch") {
        Page_ClientValidate();
        if (!Page_IsValid) {
            return false;
        }

        if (el('lkpEnquiryType').value == "PRODUCT") {
            bindProductDetail("Fetch");
            setReadOnly("lkpEnquiryType", true);
            // el('hypFetch').innerText = " Reset"
            setText(el('hypFetch'), "Reset");
            el("hypFetch").style.visibility = "visible";
        }
        else if (el('lkpEnquiryType').value == "CUSTOMER") {
            bindCustomerDetail("Fetch");
            setReadOnly("lkpEnquiryType", true);
            //el('hypFetch').innerText = " Reset"
            setText(el('hypFetch'), "Reset");
            el("hypFetch").style.visibility = "visible";
        }
        else if (el('lkpEnquiryType').value == "ROUTE") {
            bindRouteDetail("Fetch");
            setReadOnly("lkpEnquiryType", true);
            //el('hypFetch').innerText = " Reset"
            setText(el('hypFetch'), "Reset");
            el("hypFetch").style.visibility = "visible";
        }
    }
    else {
        clearTextValues("lkpEnquiryType");
        //el('hypFetch').innerText = " Fetch"
        setText(el('hypFetch'), "Fetch");
        setReadOnly("lkpEnquiryType", false);
        bindCustomerDetail("Reset");
        bindProductDetail("Reset");
        bindRouteDetail("Reset");
        el("divProduct").style.display = "none";
        el("divCustomer").style.display = "none";
        el("divRoute").style.display = "none";
        resetValidators();
    }
    reSizePane();
}

function bindProductDetail(pMode) {
    var objGrid = new ClientiFlexGrid("ifgProduct");
    objGrid.parameters.add("EnquiryType", el('lkpEnquiryType').value);
    objGrid.parameters.add("btnType", pMode);
    objGrid.parameters.add("checkselect", "");
    objGrid.DataBind();
}
function bindCustomerDetail(cMode) {
    var objGrid = new ClientiFlexGrid("ifgCustomer");
    objGrid.parameters.add("EnquiryType", el('lkpEnquiryType').value);
    objGrid.parameters.add("btnType", cMode);
    objGrid.DataBind();

}
function bindRouteDetail(cMode) {
    var objGrid = new ClientiFlexGrid("ifgRoute");
    objGrid.parameters.add("EnquiryType", el('lkpEnquiryType').value);
    objGrid.parameters.add("btnType", cMode);
    objGrid.DataBind();

}
function ifgCustomerOnAfterCB(param) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        el("divCustomer").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divCustomer").style.display = "block";
        el("divProduct").style.display = "none";
        el("divRecordNotFound").style.display = "none";
    }
}

function ifgProductOnAfterCB(param) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        el("divProduct").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divProduct").style.display = "block";
        el("divCustomer").style.display = "none";
        el("divRecordNotFound").style.display = "none";
    }
}
function ProductSelectAll(obj) {
    var icount;
    var seleactAll = obj.checked;
    if (obj.checked) {

        var objGrid = new ClientiFlexGrid("ifgProduct");
        objGrid.parameters.add("checkselect", "true");
        objGrid.parameters.add("btnType", "Fetch");
        objGrid.parameters.add("EnquiryType", el('lkpEnquiryType').value);
        objGrid.DataBind();
        HasChanges = true;
    }
    else {

        var objGrid = new ClientiFlexGrid("ifgProduct");
        objGrid.parameters.add("btnType", "Fetch");
        objGrid.parameters.add("checkselect", "false");
        objGrid.parameters.add("EnquiryType", el('lkpEnquiryType').value);
        objGrid.DataBind();
    }
}
function CustomerSelectAll(obj) {
    var icount;
    if (obj.checked) {
        icount = ifgCustomer.Rows().Count;
        for (i = 0; i < icount; i++) {
            ifgCustomer.Rows(i).SetColumnValuesByIndex(2, true);
        }
    }
    else {
        icount = ifgCustomer.Rows().Count;
        for (i = 0; i < icount; i++) {
            ifgCustomer.Rows(i).SetColumnValuesByIndex(2, false);
            resetValidators();
        }
    }
}
function GenerateProductDocument() {
    var oCallback = new Callback();
    oCallback.invoke("Enquiry.aspx", "GenerateProductDocument");
    if (oCallback.getCallbackStatus()) {
        //            showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "EnquiryProcuct";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Product Enquiry";
    oDocPrint.DocumentId = "21";
    oDocPrint.ReportPath = "../Documents/Report/EnquiryProduct.rdlc";
    oDocPrint.openReportDialog();
}
function GenerateCustomerDocument(CustomerID) {
    var rIndex = ifgCustomer.CurrentRowIndex();
    if (rIndex == "") {
        showWarningMessage("Please select a row to perform this operation");
        return false;
    }
    var cColumns = ifgCustomer.Rows(rIndex).GetClientColumns();
    var sCols = ifgCustomer.Rows(rIndex).Columns();
    var oCallback = new Callback();
    oCallback.add("CustomerId", CustomerID);
    oCallback.invoke("Enquiry.aspx", "GenerateCustomerDocument");
    if (oCallback.getCallbackStatus()) {
        //            showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "EnquiryCustomer";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Customer Enquiry";
    oDocPrint.DocumentId = "22";
    oDocPrint.ReportPath = "../Documents/Report/EnquiryCustomer.rdlc";
    oDocPrint.openReportDialog();
}
function ifgRentalOnAfterCB(param) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        el("divRoute").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divRoute").style.display = "block";
        el("divCustomer").style.display = "none";
        el("divProduct").style.display = "none";
        el("divRecordNotFound").style.display = "none";
    }
}

function openRouteReport(RouteID) {
    var rIndex = ifgRoute.CurrentRowIndex();
    if (rIndex == "") {
        showWarningMessage("Please select a row to perform this operation");
        return false;
    }
    var cColumns = ifgRoute.Rows(rIndex).GetClientColumns();
    var sCols = ifgRoute.Rows(rIndex).Columns();
    var oCallback = new Callback();
    oCallback.add("RouteID", RouteID);
    oCallback.invoke("Enquiry.aspx", "GenerateRouteDocument");
    if (oCallback.getCallbackStatus()) {
        //            showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "RouteEnquiry";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Route Enquiry";
    oDocPrint.DocumentId = "33";
    oDocPrint.ReportPath = "../Documents/Report/Route Enquiry.rdlc";
    oDocPrint.openReportDialog();
}
$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window) != null) {
          if ($(window.parent).height() < 670) {
              el("divEnquiry").style.height = $(window.parent).height() - 189 + "px";
              
            if (el("ifgProduct") != null) {
                el("ifgProduct").SetStaticHeaderHeight($(window.parent).height() - 356 + "px");
            }
            if (el("ifgCustomer") != null) {
                el("ifgCustomer").SetStaticHeaderHeight($(window.parent).height() - 356 + "px");
            }
            if (el("ifgRoute") != null) {
                el("ifgRoute").SetStaticHeaderHeight($(window.parent).height() - 356 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("divEnquiry").style.height = $(window.parent).height() - 250 + "px";
            if (el("ifgProduct") != null) {
                el("ifgProduct").SetStaticHeaderHeight($(window.parent).height() - 420 + "px");
            }
            if (el("ifgCustomer") != null) {
                el("ifgCustomer").SetStaticHeaderHeight($(window.parent).height() - 420 + "px");
            }
            if (el("ifgRoute") != null) {
                el("ifgRoute").SetStaticHeaderHeight($(window.parent).height() - 420 + "px");
            }
        }
        else {
            el("divEnquiry").style.height = $(window.parent).height() - 260 + "px";
            if (el("ifgProduct") != null) {
                el("ifgProduct").SetStaticHeaderHeight($(window.parent).height() - 409 + "px");
            }
            if (el("ifgCustomer") != null) {
                el("ifgCustomer").SetStaticHeaderHeight($(window.parent).height() - 409 + "px");
            }
            if (el("ifgRoute") != null) {
                el("ifgRoute").SetStaticHeaderHeight($(window.parent).height() - 409 + "px");
            }

        }
    }

}

