var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgViewXmlEDI');

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}

function initPage(sMode) {
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
}

function filterActivity() {
    var sqry;
    sqry = " ENM_ID IN (80,83)"
    return sqry;
}

function clearValues() {
    resetViewXml();
    clearLookupValues("lkpInvoiceType");
    clearLookupValues("lkpServicePartner");
    clearLookupValues("lkpStatus");
    clearTextValues("datPeriodFrom");
    clearTextValues("datPeriodTo");
}

function resetViewXml() {
    resetValidators();
    setText(el('btnSubmit'), "Fetch");
    el("divViewXml").style.display = "none";
    el("divRecordNotFound").style.display = "none";
    el("divHeader").style.display = "none";
    setReadOnly('lkpInvoiceType', false);
    setReadOnly('lkpServicePartner', false);
    setReadOnly('lkpStatus', false);
    setReadOnly('datPeriodFrom', false);
    setReadOnly('datPeriodTo', false);
    setText(el('lblInvoiceTypeName'), "");
}

function fetchXml() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }

    if (getText(el('btnSubmit')) == "Fetch") {
        setText(el('btnSubmit'), "Reset");
        if (el('lkpInvoiceType').value != '') {

            bindGrid(el('lkpServicePartner').SelectedValues[0], el('datPeriodFrom').value, el('datPeriodTo').value, el('lkpInvoiceType').SelectedValues[1], el('lkpStatus').SelectedValues[1]);
            var rowCount = ifgViewXmlEDI.Rows().Count;
            if (rowCount > 0) {
                el("divViewXml").style.display = "block";
                el("divRecordNotFound").style.display = "none";
                el("divHeader").style.display = "block";
                setText(el('lblInvoiceTypeName'), el('lkpInvoiceType').value);
            }
            else {
                el("divRecordNotFound").style.display = "block";
                el("divViewXml").style.display = "none";
                el("divHeader").style.display = "none";
                setText(el('lblInvoiceTypeName'), "");

            }
        }

        setReadOnly('lkpInvoiceType', true);
        setReadOnly('lkpServicePartner', true);
        setReadOnly('datPeriodFrom', true);
        setReadOnly('datPeriodTo', true);
        setReadOnly('lkpStatus', true);
    }
    else {
        resetViewXml();
    }

    reSizePane();
}

function bindGrid(CustomerId, PeriodFrom, PeriodTo, ActivityName, Status) {
    var objGrid = new ClientiFlexGrid("ifgViewXmlEDI");
    objGrid.parameters.add("CustomerId", CustomerId);
    objGrid.parameters.add("PeriodFrom", PeriodFrom);
    objGrid.parameters.add("PeriodTo", PeriodTo);
    objGrid.parameters.add("ActivityName", ActivityName);
    objGrid.parameters.add("Status", Status);
    objGrid.DataBind();
}

$(window.parent).resize(function () {
    reSizePane();
});
function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 768) {
            el("tabViewXml").style.height = $(window.parent).height() - 100 + "px";
            if (el("ifgViewXmlEDI") != null) {
                if (!chrome) {
                    el("ifgViewXmlEDI").SetStaticHeaderHeight($(window.parent).height() - 387 + "px");
                }
                else {
                    el("ifgViewXmlEDI").SetStaticHeaderHeight($(window.parent).height() - 390 + "px");
                }
            }
        }
        else {
            el("tabViewXml").style.height = $(window.parent).height() - 100 + "px";
            if (el("ifgViewXmlEDI") != null) {
                if (!chrome) {
                    el("ifgViewXmlEDI").SetStaticHeaderHeight($(window.parent).height() - 385 + "px");
                }
                else {
                    el("ifgViewXmlEDI").SetStaticHeaderHeight($(window.parent).height() - 400 + "px");
                }
            }
        }
    }

}

function validateTodate(oSrc, args) {
    var fromdate;
    fromdate = el('datPeriodFrom').value;

    if (el('datPeriodTo').value == fromdate) {
        if (!DateCompare(el('datPeriodTo').value, fromdate)) {
            showErrorMessage("Period To Date should be greater or equal to Period From Date(" + fromdate + ")");
            args.IsValid = false;
            return false;
        }
    }
}

function openXmlDetail(InvoiceEdiId) {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    } else {
        showModalWindow("Line Response", "Billing/ViewXmlDetail.aspx?EDIId=" + InvoiceEdiId, "600px", "400px", "0px", "", "");
        HasChanges = true;
        psc().hideLoader();
    }
}

function openXmlDocument(invoiceFileName, InvcType) {
    var filename = "";
    filename = invoiceFileName;
    el("hiddenLinkViewInvoice").href = "../download.ashx?FL_NM=" + filename + "&INVC_TYPE=" + InvcType;
    el("hiddenLinkViewInvoice").click();
}

function OpenErrorPopup(TId, rUrl) {
    var MxLength = 300;
    if (Page_IsValid) {
        var prms;
        var d = new Date();
        prms = rUrl + "?mL=" + MxLength + "&tid=" + TId + "&vl=" + TId + "&readonly=" + true + "&tms=" + d.getDate();
        showModalWindow("More Info", prms, "390px", "335px", "", "", "", "More Info");
    }
}

function filterCustomer() {
    var sqry;
    sqry = " XML_BT = 1"
    return sqry;
}

function onClientChangeInvoiceTypeClearValues() {
    clearLookupValues("lkpServicePartner");
    clearTextValues("datPeriodFrom");
    clearTextValues("datPeriodTo");
    clearTextValues("lkpStatus");
}