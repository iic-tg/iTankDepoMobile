var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgMiscellaneousInvoiceDetail');
var _RowValidationFails = false;



//This function is used to intialize the page.
function initPage(sMode) {
    
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    showSubmitPrintButton(false);
    bindMiscellaneousInvoiceDetail(sMode);
    setDefaultValues();
    reSizePane();
}

//This function is used to submit the page to the server.
function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        if (ifgMiscellaneousInvoiceDetail.Submit() == false || _RowValidationFails) {
            return false;
        }
        return UpdateMiscInvoice();
    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

//This function is used to Update the changes to the server. 
function UpdateMiscInvoice() {
    var oCallback = new Callback();
    oCallback.add("ActivityId", getQueryStringValue(document.location.href, "activityid"));
    oCallback.invoke("MiscellaneousInvoice.aspx", "UpdateMiscellaneousInvoice");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ActivitySubmit") == "" || oCallback.getReturnValue("ActivitySubmit") == null) {
            showInfoMessage(oCallback.getReturnValue("Message"));
        }
        else {
            activitySubmitMessage(oCallback.getReturnValue("ActivitySubmit"));
        }
        HasChanges = false;
        resetHasChanges("ifgMiscellaneousInvoiceDetail");
        bindMiscellaneousInvoiceDetail("edit");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    return true;
}

//This function is used to Set the Default Values in the Grid.
function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgMiscellaneousInvoiceDetail.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgMiscellaneousInvoiceDetail.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
        ifgMiscellaneousInvoiceDetail.Rows(iCurrentIndex).SetReadOnlyColumn(1, false);
        ifgMiscellaneousInvoiceDetail.Rows(iCurrentIndex).SetReadOnlyColumn(2, false);
        ifgMiscellaneousInvoiceDetail.Rows(iCurrentIndex).SetReadOnlyColumn(3, true);
        ifgMiscellaneousInvoiceDetail.Rows(iCurrentIndex).SetReadOnlyColumn(4, false);
        ifgMiscellaneousInvoiceDetail.Rows(iCurrentIndex).SetReadOnlyColumn(5, false);
        ifgMiscellaneousInvoiceDetail.Rows(iCurrentIndex).SetReadOnlyColumn(6, false);
        ifgMiscellaneousInvoiceDetail.Rows(iCurrentIndex).SetReadOnlyColumn(7, false);
        ifgMiscellaneousInvoiceDetail.Rows(iCurrentIndex).SetColumnValuesByIndex(1, getConfigSetting('DefautEqType'));
    }
    return true;
}

//This function used to Hide the Detail Grid when it has no Rows to display.
function ifgMiscellaneousInvoiceDetailOnAfterCB(param) {
    if (typeof (param["Error"]) != 'undefined') {
        showErrorMessage(param["Error"]);
        return false;
    }
    if (typeof (param["Success"]) != 'undefined') {
        showWarningMessage(param["Success"]);
        return false;
    }
}

function ifgMiscellaneousInvoiceDetailOnBeforeCB(mode, param) {
    if (mode == "Delete") {
        var _cols = ifgMiscellaneousInvoiceDetail.Rows(ifgMiscellaneousInvoiceDetail.rowIndex).Columns();       
        param.add("NoofEquipment", _cols[1]);
    }
}

function makeReadOnly() {
    var _index = ifgMiscellaneousInvoiceDetail.Rows().Count;
    for (var i = 0; i < _index; i++) {
        ifgMiscellaneousInvoiceDetail.Rows(i).SetReadOnlyColumn(0, true);
    }
}

function bindMiscellaneousInvoiceDetail(mode) {
    var objGrid = new ClientiFlexGrid("ifgMiscellaneousInvoiceDetail");
    objGrid.parameters.add("Mode", mode);
    objGrid.DataBind();
}

function formatAmount(obj) {
    var Amount = new Number;
    if (obj.value != "") {
        Amount = parseFloat(obj.value);
        obj.value = Amount.toFixed(2);
    }
    else {
        obj.value = "";
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
            el("tabMiscInvoice").style.height = $(window.parent).height() - 253 + "px";
            if (el("ifgMiscellaneousInvoiceDetail") != null) {
                el("ifgMiscellaneousInvoiceDetail").SetStaticHeaderHeight($(window.parent).height() - 310 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("tabMiscInvoice").style.height = $(window.parent).height() - 320 + "px";
            if (el("ifgMiscellaneousInvoiceDetail") != null) {
                el("ifgMiscellaneousInvoiceDetail").SetStaticHeaderHeight($(window.parent).height() - 427 + "px");
            }

        }
        else {
            el("tabMiscInvoice").style.height = $(window.parent).height() - 254 + "px";
            if (el("ifgMiscellaneousInvoiceDetail") != null) {
                el("ifgMiscellaneousInvoiceDetail").SetStaticHeaderHeight($(window.parent).height() - 313 + "px");
            }

        }
    }

}


// Category Filter
function fnCategoryFilter() {

    var rIndex = ifgMiscellaneousInvoiceDetail.CurrentRowIndex();
    var cColumns = ifgMiscellaneousInvoiceDetail.Rows(rIndex).GetClientColumns();
    
    if (cColumns[6] == "MISCELLANEOUS") {
        return "INVC_TYP_CD ='" + cColumns[6] + "'";
    }

}

function ValidateCategory(oSrc, args) {
    if (getConfigSetting('043') == "True") {
        var cols = ifgMiscellaneousInvoiceDetail.Rows(ifgMiscellaneousInvoiceDetail.CurrentRowIndex()).GetClientColumns();
        if (cols[8] == "" || cols[8] == null) {
            oSrc.errormessage = "Category Required";
            args.IsValid = false;
        }
    }
}

function filterOnServicePartnerType() {
    if (getConfigSetting('067') == "True") {
        return "SRVC_PRTNR_TYP_CD IN ('CUSTOMER','AGENT')";
    }
    else {
        return "SRVC_PRTNR_TYP_CD IN ('CUSTOMER','PARTY')";
    }
    
}