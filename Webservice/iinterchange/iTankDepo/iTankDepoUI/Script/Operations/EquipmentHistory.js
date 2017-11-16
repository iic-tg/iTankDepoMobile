var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgEquipmentHistory');
var _RowValidationFails = false;
var DeleteFlag = false;
var thisPage = "pdfs(" + "wfFrame + pdf(" + "CurrentDesk))"
var strtrackingId = "";
var stractvityName = "";
var streqStatusCode = "";
// Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :57

function initPage(sMode) {

    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    hideDiv("divEquipHistory");
    hideDiv("divRecordNotFound");
    clearTextValues("txtEquipment");
    setFocusToField("txtEquipment");
    setPageMode("edit");
    showSubmitPrintButton(false);
    showSubmitButton(false);
    $('.btncorner').corner();
}

function onSearchClick() {
    Page_ClientValidate();

    if (!Page_IsValid) {
        hideDiv("divEquipHistory");
        return false;
    }
    //UIG Fix  for the Chrome Issue No:57
    if (getText(el('btnSearch')) == "Search") {
        setReadOnly("txtEquipment", true);
        setText(el('btnSearch'), "Reset");
        bindEquipmentHistory("Search");
    }
    else if (getText(el('btnSearch')) == "Reset") {
        onResetClick();
        setReadOnly("txtEquipment", false);
        setText(el('btnSearch'), "Search");
        setFocusToField("txtEquipment");
        el("divPrint").style.display = "none";
    }
    reSizePane();
}

function bindEquipmentHistory(mode) {
    var objGrid = new ClientiFlexGrid("ifgEquipmentHistory");
    objGrid.parameters.add("EquipmentNo", el("txtEquipment").value);
    objGrid.parameters.add("Mode", mode);
    objGrid.DataBind();
}

function ifgEquipmentHistoryOnAfterCB(param) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "true") {
        showDiv("divRecordNotFound");
        hideDiv("divEquipHistory");
        setText(el('btnSearch'), "Reset");
        el("divPrint").style.display = "none";
        setReadOnly("txtEquipment", true);
        return false;
    }
    else {
        hideDiv("divRecordNotFound");
        showDiv("divEquipHistory");
        setText(el('lblType'), param["Type"]);
        setText(el('lblCode'), param["Code"]);
        // el('lblType').innerText = param["Type"];
        //        el('lblCode').innerText = param["Code"];
        el("divPrint").style.display = "block";
    }
}

function onResetClick() {
    hideDiv("divRecordNotFound");
    hideDiv("divEquipHistory");
    clearTextValues("txtEquipment");
    setText(el('lblType'), "");
    setText(el('lblCode'), "");
    //    el('lblType').innerText = "";
    //    el('lblCode').innerText = "";
    bindEquipmentHistory("Reset");
}


////Release -3 Changes
function fnAuditRemarksEntry(trackingId, actvityName, equipmentNo, gi_Trnsctn_No, eqStatusCode) {
    if (checkRights()) {
        strtrackingId = trackingId;
        stractvityName = actvityName;
        streqStatusCode = eqStatusCode;

        var oCallback = new Callback();
        oCallback.add("EquipmentNo", equipmentNo);
        oCallback.add("GI_TRNSCTN_NO", gi_Trnsctn_No);
        oCallback.invoke("EquipmentHistory.aspx", "Validate_Histroy_Delete");

        if (oCallback.getCallbackStatus()) {

            var retTrackingId = oCallback.getReturnValue("TrackingId");

            if (retTrackingId == trackingId) {

                showConfirmMessage("Are you sure you want to delete this activity. Click 'Yes' to Delete or 'No' to ignore",
                                "wfs().yesClick();", "wfs().noClick();");
            }
            else {
                showErrorMessage("Equipment is mapped to another customer (or) Equipment is already deleted  from this activity.");
            }

        }
        else {
            showErrorMessage("Equipment is mapped to another customer (or) Equipment is already deleted  from this activity.");
        }


    }
    else {
        return false;
    }
}

function yesClick() {
    el("hdnTrackingID").value = strtrackingId;
    el("hdnActivityName").value = stractvityName;

    if (streqStatusCode == "") {
        streqStatusCode = stractvityName;
    }

    showModalWindow("Activity (" + streqStatusCode + ") - Deletion Reason", "Operations/AuditRemarksEntry.aspx?TrackingId=" + strtrackingId + "&actvityName=" + stractvityName + "&" + el("WFDATA").value, "450px", "220px", "", "", "");
    return true;
}

function noClick() {
    return false;
}

function saveEquipmentHistory() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }

    var oCallback = new Callback();
    oCallback.add("TrackingID", el("hdnTrackingID").value);
    oCallback.add("ActivityName", el("hdnActivityName").value);
    oCallback.add("AuditRemarks", el("txtRemarks").value);
    oCallback.invoke("EquipmentHistory.aspx", "EquipmentDeleteActivity");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("validRemarks") == "true") {
            showErrorMessage("Please Enter Valid Info");
            return false;
        }
        if (oCallback.getReturnValue("lengthValidRemarks") == "true") {
            showErrorMessage("InValid Length");
            return false;
        }
        else {
            ppsc().closeModalWindow();
            showInfoMessage(oCallback.getReturnValue("Message"));
            HasChanges = false;
            resetHasChanges("ifgEquipmentHistory");

            pdfs("wfFrame" + pdf("CurrentDesk")).bindEquipmentHistory("Search");
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function printEquipmentHistory() {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "EquipmentHistory";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Equipment History";
    //GWS
    if (getConfigSetting('062') == "True") {
        oDocPrint.DocumentId = "46";
        oDocPrint.ReportPath = "../Documents/Report/EquipmentHistoryGWS.rdlc";
    }
    else {
        oDocPrint.DocumentId = "31";
        oDocPrint.ReportPath = "../Documents/Report/EquipmentHistory.rdlc";
    }
    oDocPrint.openReportDialog();
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
        if ($(window.parent) != undefined) {
            if ($(window.parent).height() < 680) {
                if (el("divSearch") != null) {
                    el("divSearch").style.height = $(window.parent).height() - 227 + "px";
                }
                if (el("ifgEquipmentHistory") != null) {
                    el("ifgEquipmentHistory").SetStaticHeaderHeight($(window.parent).height() - 373 + "px");
                }
            }
            else if ($(window.parent).height() < 768) {
                if (el("divSearch") != null) {
                    el("divSearch").style.height = $(window.parent).height() - 350 + "px";
                }
                if (el("ifgEquipmentHistory") != null) {
                    el("ifgEquipmentHistory").SetStaticHeaderHeight($(window.parent).height() - 550 + "px");
                }
            }
            else {
                if (el("divSearch") != null) {
                    el("divSearch").style.height = $(window.parent).height() - 210 + "px";
                }
                if (el("ifgEquipmentHistory") != null) {
                    el("ifgEquipmentHistory").SetStaticHeaderHeight($(window.parent).height() - 354 + "px");
                }
            }
        }
    }

}