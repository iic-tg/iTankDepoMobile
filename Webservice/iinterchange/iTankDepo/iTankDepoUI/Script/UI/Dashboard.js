var activities = {GATE_IN : "29",REPAIR_ESTIMATE : "39",LESSEE_APPROVAL : "41",OWNER_APPROVAL : "43",REPAIR_COMPLETION : "50",SURVEY_COMPLETION : "49",GATE_OUT : "56"}
//This function is used to initialize the dashboard
function initDashboard() {
    //bindPendingActionGrids();
    // $("#divaccordion").accordion();   
}

//This function is used to show the default dashboard on clicking home link
function showDefaultDashboard() {
    bindPendingActionGrids();
    $("#divaccordion").accordion();
}

//This Function is used to navigate to the corresponding activity
function navigateToPendingActivity(activityID,filterName,filterId) {
    ppsc().onMenuClick(ppsc().Activities[activities[activityID]].split(";")[1].slice(4) + "&FLTR_NAM=" +filterName + "&FLTR_VAL=" + filterId);
}

//This function is used to bind the pending Action Grid
function bindPendingActionGrids() {
    var objGrid = new ClientiFlexGrid("ifgPendingActions");
        objGrid.DataBind();
    }
