var vrGridIds = new Array('ITab1_0;ifgFinanceIntegration');


function initPage(sMode) {
    bindGrid();
    reSizePane();
    var rowCount = ifgFinanceIntegration.Rows().Count;
//    if (rowCount > 0) {
    el("divLineDetail").style.display = "block";
//        el("divRecordNotFound").style.display = "none";
//    }
//    else {
//        el("divViewInvoice").style.display = "none";
//        el("divRecordNotFound").style.display = "block";
//    }
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    el("lnkHelp").style.display = "none";
}

//Grid bind
function bindGrid() {
    var ifgGrid = new ClientiFlexGrid("ifgFinanceIntegration");
    ifgGrid.DataBind();
}

//Submit
function submitPage() {
    if (!Page_IsValid) {
        return false;
    }
    if (!Page_ClientValidate('divLineDetail', false)) {
        return false;
    }
    var oCallback = new Callback();
    //    oCallback.add("Code", el("txtPRDCT_CD").value);
    ifgFinanceIntegration.Submit(true);
    oCallback.invoke("FinanceIntegrationMaster.aspx", "UpdateFinanceIntegration");
   
    if (oCallback.getCallbackStatus()) {
        showInfoMessage("Finance Integration Updated Successfully.");
    }
    else {
        showErrorMessage("Please Contact System Administrator");
    }
}

//Avoid duplicate types
function chekDuplicates(param, mode) {
    if (typeof (param["Duplicate"]) != "undefined") {
        showErrorMessage(param["Duplicate"]);
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
            el("tabViewInvoice").style.height = $(window.parent).height() - 233 + "px";
            if (el("ifgFinanceIntegration") != null) {
                el("ifgFinanceIntegration").SetStaticHeaderHeight($(window.parent).height() - 289 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("tabViewInvoice").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgFinanceIntegration") != null) {
                el("ifgFinanceIntegration").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }

        }
        else {
            if (!chrome) {
                el("tabViewInvoice").style.height = $(window.parent).height() - 233 + "px";
            }
            else {
                el("tabViewInvoice").style.height = $(window.parent).height() - 243 + "px";
            }
            if (el("ifgFinanceIntegration") != null) {
                if (!chrome) {
                    el("ifgFinanceIntegration").SetStaticHeaderHeight($(window.parent).height() - 294 + "px");
                }
                else {
                    el("ifgFinanceIntegration").SetStaticHeaderHeight($(window.parent).height() - 304 + "px");
                }
            }

        }
    }


}