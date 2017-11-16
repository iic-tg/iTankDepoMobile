var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgEquipmentDetail');
var _RowValidationFails = false;
var GATE_OUT_Outward = "Gate In-Outward Pass"
var HasMoreInfoChanges = false;


//This function is used to intialize the page.
function initPage(sMode) {
   
    // $("#spnHeader").text(getPageTitle());
    //Fix for BreadCrums
    var sPageTitle = getPageTitle();
    $("#spnHeader").text("Operations >>" + sPageTitle);
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    showSubmitPrintButton(true);
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
}
function ifgEquipmentDetailOnAfterPendingTabSelected() {
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


function bindEquipmentDetail(mode) {
    var objGrid = new ClientiFlexGrid("ifgEquipmentDetail");
    objGrid.parameters.add("Mode", mode);
    objGrid.parameters.add("WFData", getWFDATA());
    objGrid.DataBind();
}
function ValidateGateOutDate(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    var oCallback = new Callback();
    var _rowI = ifgEquipmentDetail.rowIndex;

    if (getConfigSetting('059') == "True") {
        if (cols[12] == "") {
            oSrc.errormessage = "Out Date Required";
            args.IsValid = false;
            return;
        }
        oCallback.add("EventDate", cols[12]);
    }
    else {
        if (cols[10] == "") {
            oSrc.errormessage = "Out Date Required";
            args.IsValid = false;
            return;
        }
        oCallback.add("EventDate", cols[10]);
    }
  
    oCallback.add("EquipmentNo", cols[2]);
    oCallback.invoke("GateOut.aspx", "ValidatePreviousActivityDate");

    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("Error") != "" && oCallback.getReturnValue("Error") != null) {//added and changed for UIG Fix
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

function CheckGrade(oSrc, args) {
    var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).GetClientColumns();
    var _rowI = ifgEquipmentDetail.rowIndex;
    if (cols[18] == "") {
        oSrc.errormessage = "Grade Required";
        args.IsValid = false;
        return;
    }    
}


//This function is used to submit the page to the server.
function submitPage() {

    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges() || HasMoreInfoChanges) {
        if (ifgEquipmentDetail.Submit() == false || _RowValidationFails) {
            return false;
        }
        return updateGateOut();
    }
    else {
        showInfoMessage('No Changes to Save');
        return false;
    }
    return true;
}


//This function is used to Update the changes to the server. 
function updateGateOut() {
    var oCallback = new Callback();
    oCallback.add("WFData", getWFDATA());
    oCallback.add("Mode", el("hdnMode").value);
    oCallback.invoke("GateOut.aspx", "UpdateGateOut");
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
        return false;
    }
    oCallback = null;
    return true;
}

function submitPrintPage() {
    printGateOut();

}

function openMoreInfo(_gtnBin, _EqpNo) {
    if ((_EqpNo != "") || (_gtnBin == "")) {
        var cols = ifgEquipmentDetail.Rows(ifgEquipmentDetail.CurrentRowIndex()).Columns();
        if (!_gtnBin) {
            if (cols.length == 1) {
                if (ifgEquipmentDetail.Submit(true) == false) {
                    return false;
                }
            } 
        }
        else {
            showModalWindow("Gate In-More information", "Operations/MoreInfo.aspx?GateInId=" + _gtnBin + "&GateOutEquipmentNo=" + _EqpNo, "780px", "600px", "100px", "", "");
            return true;
            psc().hideLoader();
        }
    }
}


//This function used to Hide the Detail Grid when it has no Rows to display.
function ifgEquipmentDetailOnAfterCB(param) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        el("divEquipmentDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
       
    }
    else {
        el("divEquipmentDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
       
    }

}

//This function is used to set the mode of the page for Equipment Detail Grid
function ifgEquipmentDetailOnBeforeCB(mode, param) {
    if (getPageTitle() == GATE_OUT_Outward)
        param.add("PageMode", "Outward");
    else
        param.add("PageMode", "New");
}

function printGateOut() {
    var oDocPrint = new DocumentPrint();
    if (getConfigSetting('055') == "True") {
        oDocPrint.KeyName = "GATE OUT";
        oDocPrint.Type = "document";
        oDocPrint.Title = "Gate Out";
        oDocPrint.DocumentId = "42";
        oDocPrint.ReportPath = "../Documents/Report/GateOutDocumentGWC.rdlc";
        oDocPrint.openReportDialog();
    }
    else {
        oDocPrint.KeyName = "GATE OUT";
        oDocPrint.Type = "document";
        oDocPrint.Title = "Gate Out";
        oDocPrint.DocumentId = "16";
        oDocPrint.ReportPath = "../Documents/Report/GateOutDocument.rdlc";
        oDocPrint.openReportDialog();
    }
}


// Lock Implementation 
function GOlockData(obj, strEquipmentNo) {
    var oCallback = new Callback();
    oCallback.add("CheckBit", obj.checked);
    oCallback.add("EquipmentNo", strEquipmentNo);
    oCallback.add("LockBit", "");
    oCallback.invoke("GateOut.aspx", "GOlockData");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ErrorMessage") != "" && oCallback.getReturnValue("ErrorMessage") != null) {//added and changed for UIG Fix
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
            el("tabGateOut").style.height = $(window.parent).height() - 232 + "px";
            if (el("ifgEquipmentDetail") != null) {
                el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 306 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            //        el("tabGateOut").style.height = "440px"
            el("tabGateOut").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgEquipmentDetail") != null) {

                //            el("ifgEquipmentDetail").SetStaticHeaderHeight(360 + "px");
                el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }
        }
        else {
            el("tabGateOut").style.height = $(window.parent).height() - 231 + "px";
            if (el("ifgEquipmentDetail") != null) {
                el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 302 + "px");
            }
        }
    }

}


//Added for Attchement
function showPhotoUpload(rowIndex, GateOutId, GateInID) {
    var cols = ifgEquipmentDetail.Rows(rowIndex).Columns();
    if (ifgEquipmentDetail.Submit() == false) {
        return false;
    }
    var subName = "GateOut";
    //Check Whethr Gate in has Attachment
    if (el("iTabSelection_ITab1").value == "Pending") {
        var oCallback = new Callback();
        oCallback.add("GateInId", GateInID);
        oCallback.invoke("GateOut.aspx", "ValidateGateINAttachment");
        if (oCallback.getReturnValue("Message") == "Yes") {
            GateOutId = GateInID;
            subName = "GateIn";
        }
    }
    showModalWindow("Photo Upload - " + cols[5] + "", "Operations/PhotoUpload.aspx?RepairEstimateId=" + GateOutId + "&PageName=" + "GateOut" + "&SubName=" + subName, "850px", "500px", "150px", "", "", "", "wfs().imageDisplayRepairCompletion(" + GateOutId + ")");
    psc().hideLoader();
   
}

function imageDisplayRepairCompletion(GateOutId) {
    var objGrid = new ClientiFlexGrid("ifgEquipmentDetail");
    if (el("iTabSelection_ITab1").value == "Pending") {
        sMode = "new";
    } else {
        sMode = "edit";
    }
    objGrid.parameters.add("AttchMode", "ReBind");
    objGrid.parameters.add("Mode", sMode);
    objGrid.parameters.add("RepairEstimateId", GateOutId);
    objGrid.parameters.add("WFDATA", el("WFDATA").value);
    objGrid.DataBind();
}