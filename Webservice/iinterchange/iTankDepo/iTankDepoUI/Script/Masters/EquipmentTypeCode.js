var vrGridIds = new Array('ITab1_0;ifgEquipmentTypeCode');
var errMsg = "";
var errStatus = false;

function initPage(sMode) {
    bindGrid();
    reSizePane();
    var rowCount = ifgEquipmentTypeCode.Rows().Count;
    //    if (rowCount > 0) {
    el("divViewInvoice").style.display = "block";
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
    el("lnkHelp").style.display = "block";
}

//Grid bind
function bindGrid() {
    var ifgGrid = new ClientiFlexGrid("ifgEquipmentTypeCode");
    ifgGrid.DataBind();
    return false;
}

//Submit
function submitPage() {
    DBC();

    if (errStatus == true) {
        showErrorMessage(errMsg);
        return false;
    }

    var oCallback = new Callback();
    //    oCallback.add("Code", el("txtPRDCT_CD").value);
    oCallback.invoke("EquipmentTypeCode.aspx", "UpdateEquipmentTypeCode");
    ifgEquipmentTypeCode.Submit();
    if (oCallback.getCallbackStatus()) {
        showInfoMessage("Equipment Type & Code Updated Successfully.");
        bindGrid();
    }
    else {
        showInfoMessage(oCallback.getReturnValue("Message"));
    }
}

//Avoid duplicate types
function chekDuplicates(param, mode) {
    errStatus = false;
    if (typeof (param["Error"]) != "undefined") {
        showErrorMessage(param["Error"]);
        errMsg = param["Error"];
        errStatus = true;
    }

    if (typeof (param["Warning"]) != "undefined") {
        showWarningMessage(param["Warning"]);
    }
    return false;

}

//Default Values

function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgEquipmentTypeCode.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgEquipmentTypeCode.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    resetHasChanges("ifgEquipmentTypeCode");

    return true;

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
            if (el("ifgEquipmentTypeCode") != null) {
                el("ifgEquipmentTypeCode").SetStaticHeaderHeight($(window.parent).height() - 293 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("tabViewInvoice").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgEquipmentTypeCode") != null) {
                el("ifgEquipmentTypeCode").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }
        }
        else {
            el("tabViewInvoice").style.height = $(window.parent).height() - 242 + "px";
            if (el("ifgEquipmentTypeCode") != null) {
                el("ifgEquipmentTypeCode").SetStaticHeaderHeight($(window.parent).height() - 304 + "px");
            }

        }
    }

}