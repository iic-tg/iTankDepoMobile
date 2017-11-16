var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgEquipmentDetail');
var _RowValidationFails = false;
var HasNoteChanges = false;

//This function is used to intialize the page.
function initPage(sMode) {
    $("#spnHeader").text(getPageTitle());
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    showSubmitPrintButton(false);
    bindEquipmentDetail(MODE_NEW);
    reSizePane();
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount <= 0) {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
    el('hdnPageName').value = 'Repair Completion';
}

//This function is used to submit the page to the server.
function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges() || HasNoteChanges) {
        if (ifgEquipmentDetail.Submit() == false || _RowValidationFails) {
            return false;
        }
        return UpdateRepairCompletion();
    }
    else {
        showInfoMessage('Please Select Atleast One Equipment.');
    }
    return true;
}

function submitPrintPage() {
    printRepairCompletion();
}

//This function is used to Update the changes to the server. 
function UpdateRepairCompletion() {
    var oCallback = new Callback();
    oCallback.add("WFData", getWFDATA());
    oCallback.add("PageTitle", getPageTitle());
    oCallback.add("RevisionNo", el("hdnRevisionNo").value);
    oCallback.add("Mode", el("hdnMode").value);
    oCallback.invoke("RepairCompletion.aspx", "UpdateRepairCompletion");
    if (oCallback.getCallbackStatus()) {
        if (el("iTabSelection_ITab1").value == "Pending") {
            sMode = "new";
        } else {
            sMode = "edit";
        }
        bindEquipmentDetail(sMode);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        resetHasChanges("ifgEquipmentDetail");
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return true;
}

//This function is used to set the default values of the line detail grid.
function setDefaultValues(rIndex) {
    ifgEquipmentDetail.Rows(rIndex).SetColumnValuesByIndex(11, "0.00");
    ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).SetReadOnlyColumn(5, false);
}

//This function is used to bind the Equipment Detail Grid.
function bindEquipmentDetail(mode) {
    var objGrid = new ClientiFlexGrid("ifgEquipmentDetail");
    objGrid.parameters.add("WFData", getWFDATA());
    objGrid.parameters.add("Mode", mode);
    objGrid.DataBind();
}

function formatDecimalRate(obj) {
    var totalCost = 0;
    var icount = 0;
    var rIndex = ifgEquipmentDetail.CurrentRowIndex();
    if (trimAll(obj.value) != "") {
        var Amount = new Number;
        Amount = parseFloat(obj.value);
        obj.value = Amount.toFixed(2);
    }
}


function ifgEquipmentDetailOnAfterCB(param) {
    if (typeof (param["Mode"]) != 'undefined') {
        el("hdnMode").value = param["Mode"];
    }
}

function printRepairCompletion() {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "RepairCompletion";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Repair Completion";
    oDocPrint.DocumentId = "12";
    oDocPrint.ReportPath = "../Documents/Report/RepairCompletion.rdlc";
    oDocPrint.openReportDialog();
}

function ifgEquipmentDetailOnAfterSubmitTabSelected() {
    if (HasChanges == true) {
        if (checkGridHasChanges("ifgEquipmentDetail")) {
            setHasChanges();
            var confirm = false;
            if (psc().confirmresult == false) {
                confirm = confirmChanges("submit", "");
                return false;
            }
            if (confirm)
                return true;
        }
    }
    else {
        el("hdnMode").value = "edit";
        bindEquipmentDetail(MODE_EDIT);
    }
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount <= 0) {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}

function ifgEquipmentDetailOnAfterPendingTabSelected() {
    el("hdnMode").value = "new";
    bindEquipmentDetail(MODE_NEW);
    icount = ifgEquipmentDetail.Rows().Count;
    if (icount <= 0) {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
}

function ifgEquipmentDetailOnBeforeSubmitTabSelected() {
    if (!Page_IsValid) {
        return false;
    }
}

function showPhotoUpload(rowIndex, RepairEstimateId) {
    var cols = ifgEquipmentDetail.Rows(rowIndex).Columns();
    var subName;
    if (ifgEquipmentDetail.Submit() == false) {
        return false;
    }
    //Attachment
    if (el("hdnMode").value == "new") {
        subName = "Repair Approval";
    }
    else {
        subName = "Repair Completion";
    }
    showModalWindow("Photo Upload - " + cols[4] + "", "Operations/PhotoUpload.aspx?RepairEstimateId=" + RepairEstimateId + "&PageName=" + el('hdnPageName').value + "&SubName=" + subName, "850px", "500px", "150px", "", "", "", "wfs().imageDisplayRepairCompletion(" + RepairEstimateId + ")");
    psc().hideLoader();
}

function ValidatePreviousActivityDate(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    if (cols[10] == "") {
        oSrc.errormessage = "Repair Completion Date Required";
        args.IsValid = false;
        return;
    }

    var oCallback = new Callback();
    oCallback.add("EquipmentNo", cols[0]);
    oCallback.add("EventDate", cols[10]);
    oCallback.invoke("RepairCompletion.aspx", "ValidatePreviousActivityDate");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("Error")) {
            args.IsValid = false;
            oSrc.errormessage = oCallback.getReturnValue("Error");
        }
        else {
            args.IsValid = true;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}


function imageDisplayRepairCompletion(repairEstimateId) {
    var objGrid = new ClientiFlexGrid("ifgEquipmentDetail");
    objGrid.parameters.add("Mode", "ReBind");
    objGrid.parameters.add("RepairEstimateId", repairEstimateId);
    objGrid.DataBind();
}

//Lock Implementation  
function RClockData(obj, strEquipmentNo) {
    var oCallback = new Callback();
    oCallback.add("CheckBit", obj.checked);
    oCallback.add("EquipmentNo", strEquipmentNo);
    oCallback.add("LockBit", "");
    oCallback.invoke("RepairCompletion.aspx", "RClockData");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ErrorMessage") != "") {
            obj.checked = false;
            var errorMessage = oCallback.getReturnValue("ErrorMessage");

            if (oCallback.getReturnValue("Activity") != "") {
                errorMessage = errorMessage + " <B> (Activity : " + oCallback.getReturnValue("Activity") + ")</B>";
            }

            showErrorMessage(errorMessage);
        }
        return true;
    }
    else {
        return false;
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
        if ($(window.parent).height() < 680) {
            el("tabRepairCompletion").style.height = $(window.parent).height() - 236 + "px";
            if (el("ifgEquipmentDetail") != null) {
                el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 305 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("tabRepairCompletion").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgEquipmentDetail") != null) {
                el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }
        }
        else {
            el("tabRepairCompletion").style.height = $(window.parent).height() - 231 + "px";
            if (el("ifgEquipmentDetail") != null) {
                el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 302 + "px");
            }
        }
    }

}