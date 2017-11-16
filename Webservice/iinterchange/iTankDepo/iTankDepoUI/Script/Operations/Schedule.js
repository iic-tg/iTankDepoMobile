var HasChanges = false;
var _RowValidationFails = false;
var vrGridIds = new Array('ITab1_0;ifgSchedule');



//$(document).ready(function () {
//    parent.top.$('#wfFrameSchedule').height(750);
//});

//This function is used to intialize the page.
function initPage(sMode) {
   
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    resetValidators();
    el("divScheduleGrid").style.display = "none";
    el("divScheduleDetail").style.display = "none";
}

//This function is used to submit the page to the server.
function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        if (ifgSchedule.Submit() == false || _RowValidationFails) {
            return false;
        }
        return updateScheduleDate();
    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

//This function is used to Update the changes to the server.
function updateScheduleDate() {
    var oCallback = new Callback();
    oCallback.add("WFData", getWFDATA());
    oCallback.add("ActivityId", el('lkpSchedulingType').SelectedValues[0]);
    oCallback.invoke("Schedule.aspx", "updateScheduleDate");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        bindSchedule(el('lkpSchedulingType').SelectedValues[0], 'fetch');
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return true;
}


//This function is used to bind the respective details to the grid
function bindSchedule(activityId, mode) {
    var objGrid = new ClientiFlexGrid("ifgSchedule");
    objGrid.parameters.add("Mode", mode);
    objGrid.parameters.add("ActivityId", activityId);
    objGrid.parameters.add("ActivityName", el('lkpSchedulingType').SelectedValues[1]);
    objGrid.parameters.add("ScheduleId", el('lkpSchedule').SelectedValues[0]);
    objGrid.parameters.add("WFData", getWFDATA());
    objGrid.DataBind();

    reSizePane();
}

//This function is used to call after grid is bind.
function ifgScheduleOnAfterCB(param) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        el("divScheduleGrid").style.display = "none";
        el("divScheduleDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divScheduleGrid").style.display = "block";
        el("divScheduleDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }

    var gridValidation = param["GridValidation"];
    if (gridValidation == "True") {

        //        var rowCount = ifgSchedule.Rows().Count;
        //        var i = 0;
        //        var j = 0;
        //        if (rowCount > 0) {
        //            var colCount = ifgSchedule.Rows(0).GetClientColumns().length;
        //            for (i = 0; i < rowCount; i++) {

        //                for (j = 0; j < colCount; j++) {
        //                    //ifgSchedule.Rows(rIndex).SetColumnValuesByIndex(4, "");
        //                    ifgSchedule.Rows(i).SetReadOnlyColumn(j, true);
        //                }

        //            }

        //            hideDiv("divSubmit");
        //        }
        hideDiv("divSubmit");
    }
    else {
        showDiv("divSubmit");
    }


}

//This function used to fetch the detail based on the activity
function fetch() {
    GetLookupChanges();
    if (el('lkpSchedulingType').SelectedValues[0] != "") {
//        if (el('btnSearch').innerText == " Fetch") {
        if (getText(el('btnSearch')) == "Fetch") {
            el("divScheduleGrid").style.display = "block";
            el("divScheduleDetail").style.display = "block";
            setFocusToField("lkpSchedulingType");
            bindSchedule(el('lkpSchedulingType').SelectedValues[0], 'fetch');
            setReadOnly("lkpSchedulingType", true);
            setReadOnly("lkpSchedule", true);
            //el('btnSearch').innerText = " Reset";
            setText(el('btnSearch'), "Reset");
            el('datScheduleDate').value = el('hdnCurrentDate').value;           
        }
        //else if (el('btnSearch').innerText == " Reset") {
        else if (getText(el('btnSearch')) == "Reset") {
            el("divScheduleGrid").style.display = "none";
            el("divScheduleDetail").style.display = "none";
            clearLookupValues("lkpSchedulingType");
            el('lkpSchedulingType').SelectedValues[0] = "";
            resetValidators();
            setReadOnly("lkpSchedulingType", false);
            setReadOnly("lkpSchedule", false);
            el('lkpSchedule').SelectedValues[0] = "127";
            el('lkpSchedule').SelectedValues[1] = getConfigSetting('ScheduleDefaultType');
            el('lkpSchedule').value = getConfigSetting('ScheduleDefaultType');
            //el('btnSearch').innerText = " Fetch";
            setText(el('btnSearch'), "Fetch");
            el("divRecordNotFound").style.display = "none";           
        }
    }
    else {
        showErrorMessage('Activity Required');
        setFocusToField("lkpSchedulingType");
    }
    
}

//This function is used to update the date field based on the check box check and uncheck
function updateDate(obj) {
    var rIndex = ifgSchedule.CurrentRowIndex();
    var _cols = ifgSchedule.Rows(rIndex).GetClientColumns();
    if (obj.checked) {
        if (_cols[3] == "" || _cols[3]== null) {
            if (el('datScheduleDate').value != '') {
                ifgSchedule.Rows(rIndex).SetColumnValuesByIndex(3, el("datScheduleDate").value);
            }
            else {
                showErrorMessage('Enter Schedule Date');
                setFocusToField("datScheduleDate");
            }
        }
    }
    else {
        ifgSchedule.Rows(rIndex).SetColumnValuesByIndex(3, "");
    }
}

//This function is used to print the schedule
function printRepairSchedule() {
    if (el('datScheduleDate').value != "") {
        var documentId = "";
        var keyName = "";
        var title = "";
        var reportPath = "";

        if (el('lkpSchedulingType').SelectedValues[0] == "123") {
            documentId = "37";
            keyName = "CleaningSchedule";
            title = "Cleaning Schedule";
            reportPath = "../Documents/Report/CleaningSchedule.rdlc";
        }
        else if (el('lkpSchedulingType').SelectedValues[0] == "124") {
            documentId = "36";
            keyName = "RepairSchedule";
            title = "Repair Schedule";
            reportPath = "../Documents/Report/RepairSchedule.rdlc";
        }
        else if (el('lkpSchedulingType').SelectedValues[0] == "125") {
            documentId = "38";
            keyName = "SurveySchedule";
            title = "Survey Schedule";
            reportPath = "../Documents/Report/SurveySchedule.rdlc";
        }

        else if (el('lkpSchedulingType').SelectedValues[0] == "137") {
            documentId = "39";
            keyName = "InspectionSchedule";
            title = "Inspection Schedule";
            reportPath = "../Documents/Report/InspectionSchedule.rdlc";
        }

        var oDocPrint = new DocumentPrint();
        oDocPrint.KeyName = keyName;
        oDocPrint.Type = "document";
        oDocPrint.Title = title;
        oDocPrint.DocumentId = documentId;
        oDocPrint.add("SCHEDULE_DATE", el('datScheduleDate').value);
        oDocPrint.ReportPath = reportPath;
        oDocPrint.openReportDialog();
    }
    else {
        showErrorMessage('Schedule Date is Required.');
        setFocusToField("datScheduleDate");
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
        if ($(window.parent).height() < 788) {
            el("tabScheduling").style.height = $(window.parent).height() - 235 + "px";
            if (el("ifgSchedule") != null) {
                el("ifgSchedule").SetStaticHeaderHeight($(window.parent).height() - 433 + "px");
            }
        }
        else {
            el("tabScheduling").style.height = $(window.parent).height() - 242 + "px";
            if (el("ifgSchedule") != null) {
                el("ifgSchedule").SetStaticHeaderHeight($(window.parent).height() - 443 + "px");

            }
        }
    }

}

function CheckDate(oSrc, args) {
        var rIndex = ifgSchedule.CurrentRowIndex();
        var _cols = ifgSchedule.Rows(rIndex).GetClientColumns();
        var Current = el('hdnCurrentDate').value;
        if (DateCompare(el('hdnCurrentDate').value, _cols[3])) {
            oSrc.errormessage = 'Status Date should be greater than or equal to  current date';
            args.IsValid = false;
        }
        else {
            oSrc.errormessage = "";
            args.IsValid = true;
        }
}