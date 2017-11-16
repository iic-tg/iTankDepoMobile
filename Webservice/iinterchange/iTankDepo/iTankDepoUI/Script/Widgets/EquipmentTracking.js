
//This function is used to fetch the data from Database for the corresponding Auth No.
function searchEquipment() {
    bindifgTracking();
    
}
//This function is used to bind the Equipment Detail Grid.
function bindifgTracking() {
    var objGrid = new ClientiFlexGrid("ifgTracking");
    objGrid.parameters.add("Type", el("lkpType").SelectedValues[2]);
    objGrid.parameters.add("Value", el("txtValue").value);
    objGrid.DataBind();
}

//This function used to Hide the Detail Grid when it has no Rows to display.
function ifgTrackingOnAfterCB(param) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        showDiv("divMessage");
        hideDiv("divDetail");
    }
    else {
       showDiv("divDetail");
       hideDiv("divMessage");
    }
}

function openActivityReport(referenceNo, activityNam, date, eqstatus) {
    var oDocPrint = new DocumentPrint();
    oDocPrint.KeyName = "GateIn";
    oDocPrint.Type = "document";
    if (activityNam == "Gate In") {
        oDocPrint.Title = "Gate In - Print";
        oDocPrint.DocumentId = "3";
        oDocPrint.ReportPath = "../Documents/Report/GateIn.rdlc";
    }
    else if (activityNam == "Repair Complete") {
        oDocPrint.Title = "Repair Complete - Print";
        oDocPrint.DocumentId = "6";
        oDocPrint.ReportPath = "../Documents/Report/GateIn.rdlc";
    }
    else if (activityNam == "Gate Out") {
        oDocPrint.Title = "Gate Out - Print";
        oDocPrint.DocumentId = "4";
        oDocPrint.ReportPath = "../Documents/Report/GateOut.rdlc";
    }

    else if (eqstatus == "F" || eqstatus == "G") {
        oDocPrint.Title = "Lessee Authorization";
        oDocPrint.DocumentId = "5";
        oDocPrint.ReportPath = "../Documents/Report/Estimate.rdlc";
    }
    else if (eqstatus == "J" || eqstatus == "K") {
        oDocPrint.Title = "Owner Approval";
        oDocPrint.DocumentId = "5";
        oDocPrint.ReportPath = "../Documents/Report/Estimate.rdlc";
    }
    else if (eqstatus == "H" || eqstatus == "L" || eqstatus == "M") {
        oDocPrint.Title = "Survey Completion";
        oDocPrint.DocumentId = "5";
        oDocPrint.ReportPath = "../Documents/Report/Estimate.rdlc";
    }
    else {
        oDocPrint.Title = "Repair Estimate";
        oDocPrint.DocumentId = "5";
        oDocPrint.ReportPath = "../Documents/Report/Estimate.rdlc";
    }
    oDocPrint.add("ReferenceNo", referenceNo);
    oDocPrint.add("EIRDate", date);
    oDocPrint.add("Status", eqstatus);
    oDocPrint.openReportDialog();
}

function openContainerHistoryReport(customerId,containerNo) {
    psc().showModalWindow("Container History", "Reports/ContainerHistory.aspx?CSTMR_ID=" + customerId + "&EQPMNT_NO=" + containerNo, "500px", "400px", "", "", "", "", "");   
}