
var activityIdList = new Array();
var listRowCount;
var listMode;
//This function is used to set the list grid incase of any records available. 
function onAfterListBind(param) {
    var norecordsfound = param["norecordsfound"];
    if (!param["listrowcount"])
        listRowCount = ifgList.Rows().Count;
    else
        listRowCount = param["listrowcount"];
    if (listRowCount > 0) {
        listRowCount = listRowCount;
    }

//    if (norecordsfound == "True") {
//        if (mysubmit == "False") {
//            el("divListGrid").style.display = "none";
//            el("divRecordNotFound").style.display = "block";
//        }
//        else {
//            el("divListGrid").style.display = "none";
//            el("divRecordNotFound").style.display = "block"
//            el("divRecordNotFound").innerHTML = "No Records Found."
//        }
//    }

    if (norecordsfound == "True") {
        el("divListGrid").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divListGrid").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }


    var iActivityID = parseInt(getQueryStringValue(psc().gsURL, "activityid"));

    if (CheckvalueinArray(iActivityID)) {
        pel("dockListPane" + pdf("CurrentDesk")).style.display = "none";
    }
    else {
        pel("dockListPane" + pdf("CurrentDesk")).style.display = "block";
    }
}
//This function is used to set the list grid incase of any records available -- Invoke at Page_Load. 
function onAfterListBindPageLoad(listrowcount, norecordsfound) {
    listRowCount = listrowcount;
    if (listRowCount > 0) {
        listRowCount = listRowCount;
    }
    if (norecordsfound == "True") {
        el("divListGrid").style.display = "none";
        el("divRecordNotFound").style.display = "block";
    }
    else {
        el("divListGrid").style.display = "block";
        el("divRecordNotFound").style.display = "none";
    }
    var iActivityID = parseInt(getQueryStringValue(psc().gsURL, "activityid"));
    //Added for Traffic
   // if (iActivityID == 154) {
    if (((getQueryStringValue(psc().gsURL, "listpanetitle").indexOf("Master") > -1) || (getQueryStringValue(psc().gsURL, "listpanetitle").indexOf("Admin") > -1)) && (listrowcount == 0)) {
        el("divRecordNotFound1").style.display = "block";
       el("divRecordNotFound").style.display = "none";
    }
   
    if (CheckvalueinArray(iActivityID)) {
        pel("dockListPane" + pdf("CurrentDesk")).style.display = "none";
    }
    else {
        pel("dockListPane" + pdf("CurrentDesk")).style.display = "block";
    }
    //UIG Fix  : BacktoList not loading
    var backToListFalse = "&backToList=false";
    psc().gsURL = psc().gsURL + backToListFalse;
}

var oListGrid;

//List grid bind action
function bindListGrid(iActivityID, refresh, filterName, filterValue) {
    var TabID = el("iTabSelection_ITab2").value;
    if (TabID == "TabPending") {
        oListGrid = new ClientiFlexGrid("ifgList");
        if (!iActivityID) {
            oListGrid.parameters.add("activityid", getQStr("activityid"));
        }
        else {
            oListGrid.parameters.add("activityid", iActivityID);
        }
        oListGrid.parameters.add("mode", "Pending");
        oListGrid.parameters.add("qrytype", getQStr("qrytype"));
        oListGrid.parameters.add("FLTR_NAM", filterName);
        oListGrid.parameters.add("FLTR_VAL", filterValue);
        if (refresh != null)
            oListGrid.parameters.add("refresh", "true");
        oListGrid.DataBind();
    }
    else {
        bindMySubmitListGrid(iActivityID, refresh, filterName, filterValue);
    }
}

function onRefreshClick() {
    bindListGrid("", true, "", "");
}
function bindMySubmitListGrid(iActivityID, refresh, filterName, filterValue) {
    oListGrid = new ClientiFlexGrid("ifgList");
    oListGrid.parameters.add("activityid", getQStr("activityid"));
    oListGrid.parameters.add("mode", "MySubmits");
    oListGrid.parameters.add("qrytype", getQStr("qrytype"));
    if (filterName) {
        oListGrid.parameters.add("FLTR_NAM", filterName);
        oListGrid.parameters.add("FLTR_VAL", filterValue);
    }
    if (refresh != null)
        oListGrid.parameters.add("refresh", "true");
    oListGrid.DataBind();
}

function onAddClick(rowindex) {

    //confirm changes
    var confirm = false;
    if (psc().confirmresult == false) {
        confirm = confirmChanges("list", "pdfs('plFrame'+pdf('CurrentDesk')).onAddClick('" + rowindex + "');");

    }

    if (confirm)
        return;

    //showWorkingMessage("Loading " + sActivityName + "..");

    var vURL = psc().gsURL;
    var iActivityID = getQueryStringValue(vURL, "activityid");
    var sMenuData = Activities[iActivityID].split(";")[1].split("url=")[1];
    var sPageType = getQueryStringValue(sMenuData, "pagetype");
    var sActivityName = getQueryStringValue(sMenuData, "activityname");
    psc().displayWorkflowPane("new");
    if (psc().gsUIMode != "Confirm") {
        psc().hideMessagePane();
    }
    var sPageURL = getQueryStringValue(sMenuData, "apu");
    psc().gsMode = "new";
    var dCurrentDate = new Date();
    var sQryStr;
    sQryStr = '?activityid=' + iActivityID;
    sQryStr += '&mode=new';
    sQryStr += '&pagetype=' + sPageType;
    sQryStr += '&tmstp=' + dCurrentDate.getTime();
    sQryStr += "&backToListAdd=true";    //Added for chrome
    sPageURL = sPageURL + sQryStr;

    DBC();
    psc().loadEmptyPage('new', sPageURL);
    pel("dockListPane" + pdf("CurrentDesk")).className = "icon-resize-full";
    pel("dockListPane" + pdf("CurrentDesk")).title = "Maximize";

    var vURL = psc().gsURL;
    var iActivityID = getQueryStringValue(vURL, "activityid");

    if (iActivityID == 102) {
        psc().hideListGrid(); 
    }
}

//This function used to parse the workspace data
function loadWorkspace(iActivityID, sMode, iItemNo, sItemCode) {
    var sMenuData = Activities[iActivityID].split(";")[1].split("url=")[1];
    var sPageURL = getQueryStringValue(sMenuData, "apu");
    var sPageTitle = getQueryStringValue(sMenuData, "pagetitle");
    var sPageType = getQueryStringValue(sMenuData, "pagetype");
    var sTableName = getQueryStringValue(sMenuData, "tablename");
    var sActivityName = getQueryStringValue(sMenuData, "activityname"); //Record Locking
    psc().giItemNo = iItemNo;
    psc().hideMessagePane();
    loadPage(sPageURL, iActivityID, sPageTitle, sPageType, sTableName, sMode, iItemNo)
}

//This function used to load the data in workspace
function loadPage(sPageURL, iActivityID, sPageTitle, sPageType, sTableName, sMode, iItemno) {
    //changes for record locking
    var backToList = "&backToList=true";
    var backToListFalse = "&backToList=false";
    if (pdfs("plFrame" + pdf("CurrentDesk")).location.href.indexOf(backToList) != -1) {
        pdfs("plFrame" + pdf("CurrentDesk")).location.href.replace(backToList, backToListFalse);
    }
        if (psc().gsURL.indexOf(backToList) > 0) {
            psc().gsURL = psc().gsURL.replace(backToList, backToListFalse);
        }
        else {
            psc().gsURL = psc().gsURL + backToListFalse;
        }
    //completed

    //confirm changes
    var confirm = false;
    //    if (pdfs("wfFrame" + pdf("CurrentDesk")).recordLockChanges == true) {
    //        psc().confirmresult = true;
    //    }
    // if (pdfs("wfFrame" + pdf("CurrentDesk")).recordLockChanges == false) {
    if (psc().confirmresult == false) {
        confirm = confirmChanges("list", "pdfs('plFrame'+pdf('CurrentDesk')).loadPage('" + sPageURL + "','" + iActivityID + "','"
                    + sPageTitle + "','" + sPageType + "','" + sTableName + "','" + sMode + "','" + iItemno + "');");
    }
    // }

    if (confirm) {
        return;
    }

    var bDialogResult = false;
    var sUIMode = psc().gsUIMode;
    if (typeof (ifgList) != "undefined" && sUIMode != "Navigation" && sUIMode != "Confirm") {
        iItemno = parseInt(ifgList.VirtualCurrentRowIndex());
    }
    psc().giItemNo = iItemno;
    listMode = sMode;
    psc().gsListMode = sMode;
    psc().displayWorkflowPane("edit");

    if (!bDialogResult) {
        var dCurrentDate = new Date();
        var sQryStr;
        sQryStr = '?activityid=' + iActivityID;

        sQryStr += '&mode=' + sMode;

        sQryStr += '&itemno=' + iItemno;
        sQryStr += '&tablename=' + sTableName;
        sQryStr += '&pagetype=' + sPageType;
        sQryStr += '&pagetitle=' + sPageTitle;
        sQryStr += '&tmstp=' + dCurrentDate.getTime();

        psc().gsMode = sMode;

        if (sPageType != "PostBack") {
            psc().loadActivityPane(sPageURL, sQryStr, sMode, sPageType, iItemno);
        }
        else {
            psc().gsWorkspaceURL = sPageURL + sQryStr;
            psc().loadingWorkflowPane("Loading " + sPageTitle + "..");
            psc().loadWorkspace();
        }
    }
    pel("dockListPane" + pdf("CurrentDesk")).className = "icon-resize-full";
    pel("dockListPane" + pdf("CurrentDesk")).title = "Maximize";
    if (CheckvalueinArray(iActivityID)) {
        psc().hideListGrid();
    }
    else {
        showListGrid();
    }
}

function showListGrid() {
    if (pel("trListPane" + pdf("CurrentDesk")) != null) {
        var bws = getBrowserHeight();
        if (bws.height < 250) {
            //psc().setListGridHeight(document.documentElement.clientHeight - 55);
            psc().setListGridHeight(($(window).height()) - 60);
        }
        else if ($(window).height() < 600) {
            psc().setListGridHeight(($(window).height()) - 52);
        }
        else {
            //psc().setListGridHeight(document.documentElement.clientHeight - 55);
            psc().setListGridHeight(($(window).height()) - 61);
        }
        if (el("divTab").style.display == "block") {
            //psc().setListGridHeight(document.documentElement.clientHeight - 75);
            psc().setListGridHeight(($(window).height()) - 75);
        }
    }

}

function CheckvalueinArray(a) {
    for (var i = 0; i < activityIdList.length; i++) {
        if (a == activityIdList[i]) {
            return true;
        }
    }
    return false;
}

//This method is used to get browser height
function getBrowserHeight() {
    var iHeight = 0;
    var iWidth = 0;

    if (typeof window.innerWidth == 'number') {
        iHeight = window.innerHeight;
        iWidth = window.innerWidth;
    }
    else if (document.body && (document.body.scrollWidth || document.body.scrollHeight)) {
        iHeight = document.body.scrollHeight - 3;
        iWidth = document.body.scrollWidth - 3;
    }

    return { width: parseInt(iWidth), height: parseInt(iHeight) };
}