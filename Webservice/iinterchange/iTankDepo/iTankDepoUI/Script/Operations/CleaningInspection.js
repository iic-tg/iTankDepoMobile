var HasChanges = false;
//var vrGridIds = new Array('ITab1_0;ifgCleaningInstruction');
var _RowValidationFails = false;



//This function is used to intialize the page.
function initPage(sMode) {
    
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    //Fix for BreadCrums
   // var sPageTitle = getPageTitle();
    $("#spnHeader").text("Operations >>Cleaning >>Cleaning Instruction");
    bindGrid();
    reSizePane();
}

function GenerateCleaningInstructionReport() {
    var oCallback = new Callback();   
    oCallback.invoke("CleaningInspection.aspx", "GenerateInstructionNo");
    if (oCallback.getCallbackStatus()) {
        PrintCleaningInspectionNo();
        bindGrid();
    }
    else {
        if (oCallback.getReturnValue("SELECT") == "True") {
            showErrorMessage("Please Select Atleast One Equipment to Generate Cleaning Instruction.");
        }
    }
    oCallback = null;  
    return true;
}

function PrintCleaningInspectionNo() {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "Cleaning Instruction";
    oDocPrint.Type = "document";
    oDocPrint.Title = "Cleaning Instruction";
    oDocPrint.DocumentId = "30";
    oDocPrint.ReportPath = "../Documents/Report/CleaningInspection.rdlc";
    oDocPrint.openReportDialog();
}

function bindGrid() {
    var oCallback = new Callback();
    var ifgCleaningInstruction = new ClientiFlexGrid("ifgCleaningInstruction");
    ifgCleaningInstruction.DataBind();
   
}

function AfterCallBack(param, mode) {
    if (param["RowCount"] == "0") {
        el("divRecordNotFound").style.display = "block";
        el("divCleaningInstruction").style.display = "none";
    }
    else {
        el("divRecordNotFound").style.display = "none";
        el("divCleaningInstruction").style.display = "block";
    }
}


//Record Lock Implementation 
function CIlockData(obj, strEquipmentNo) {
    var oCallback = new Callback();
    oCallback.add("CheckBit", obj.checked);
    oCallback.add("EquipmentNo", strEquipmentNo);
    oCallback.add("LockBit", "");
    oCallback.invoke("CleaningInspection.aspx", "CIlockData");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ErrorMessage") != "" && oCallback.getReturnValue("ErrorMessage") != null) {
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
            el("divCleaningInstruction").style.height = $(window.parent).height() - 223 + "px";
            if (el("ifgCleaningInstruction") != null) {
                el("ifgCleaningInstruction").SetStaticHeaderHeight($(window.parent).height() - 274 + "px");
            }
        }
        else if ($(window.parent).height() < 768) {
            el("divCleaningInstruction").style.height = $(window.parent).height() - 350 + "px";
            if (el("ifgCleaningInstruction") != null) {
                el("ifgCleaningInstruction").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
            }
        }
        else {
            el("divCleaningInstruction").style.height = $(window.parent).height() - 213 + "px";
            if (el("ifgCleaningInstruction") != null) {
                el("ifgCleaningInstruction").SetStaticHeaderHeight($(window.parent).height() - 262 + "px");
            }
        }
    }

}
//Added for Script Error
function submitPage() {
    //ifgCleaningInstruction.Submit();
    //GenerateCleaningInstructionReport();
}