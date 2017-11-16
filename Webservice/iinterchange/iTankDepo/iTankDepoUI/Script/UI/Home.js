
//Declarations
var activityIdList = new Array();
var browserinvalid = false;
var _fl = false;
var _as = false;
var _wfa = false;
var gsPageType;
var gsPrevPageType;
var gsWorkspaceURL;
var gsMode;
var gsURL;
var giActivityID;
var gsListPaneURL;
var gsType;
var giItemNo;
var gsAction;
var giListRowCount;
var gsUIMode;
var gsListMode;
var gsRequestType;
var gsWorkingMessage;
var gSubmitNext;
var gNoRecordsFound;
var confirmresult = false;
var clicktype = "";
var gsStatus = "";
var gblnHelpTip = false;
var sidePanel;
var OldTheme = "";
var tablist = new Array();
var CurrentDesk = "";
var LastDesk = "HomeTab";
var removedTabId = "";
var previousTab = "";
var confirmCall;
var CloseStatus = true;
var recentItem = new Array();
var recentItemObj = function (activityID, pageTitle) {
    this.ActivityID = activityID;
    this.PageTitle = pageTitle;
    return this;
};
var minWidth = 1100, minHeight = 768;
var myLayout;

function reloadIFrame() {
    var dtTimeStamp = new Date();
    document.all.HomeFrame.src = "pgref.aspx?ts=" + dtTimeStamp.getTime();
}
var strFavActivityIds = "";
//This method is used for showing warning message on before unload the home page.
function onBeforeLoad() {
    if (!browserinvalid && !_as) {
        window.event.returnValue = "The changes you made will be lost if not saved."
        _as = true;
    }
}

function onHomeLoad() {
    var dtTimeStamp = new Date();
    document.all.HomeFrame.src = "pgref.aspx?ts=" + dtTimeStamp.getTime();
    window.setInterval("reloadIFrame();", 60000);
    document.title = getText(lblWelcome2) + " - Logged on to " + getText(lblWelcomeTitle);
  
    fitMenu();
    fitCenter();
    startTime();
}
if (window.$) {
    $().ready(function () {
        myLayout = $('#layoutContainer').layout({
            north__closable: false
            , north__resizable: false
            , south__closable: false
            , south__resizable: false
            , west__size: 200
            , west__resizable: false
            , west__maxSize: 250
            , west__spacing_closed: 20
            , west__togglerLength_closed: 160
            , west__togglerAlign_closed: "top"
            , west__togglerContent_closed: "N<BR>A<BR>V<BR>I<BR>G<BR>A<BR>T<BR>I<BR>O<BR>N"
            , west__togglerTip_closed: "Open & Pin Menu"
            , west__sliderTip: "Slide Open Menu"
            , west__slideTrigger_open: "mouseover"
            , center__size: "auto"
            , center__maskContents: true
            , onopen_end: function () { fitLayout(); }
            , onclose: fitLayout
        });
        $("#accordionRI").accordion({ active: false, autoHeight: false, collapsible: true, navigation: true });
        $("#accordionQL").accordion({ active: false, autoHeight: false, collapsible: true, navigation: true });
        $("#accordionFav").accordion({ active: false, autoHeight: false, collapsible: true, navigation: true });
        $("#accordionMaster").accordion({ active: false, autoHeight: false, collapsible: true, navigation: true });
        $(".ui-accordion .ui-accordion-content").css("overflow", "hidden")
        $('#layoutContainer').height($(window).height());
        //strFavActivityIds = el("hdnFavourites").value;
        loadFavouritesLinks();
        /*$("#ulWorkflow").sortable();*/
        unFoldNavigationPane();
    });
}


//This method is used for showing logout popup on unload the home page.
function onUnload() {
    _as = true;
    if (_fl == false) {
        window.location.href = "Logout.aspx?lo=false&onb4uld=true";
    }
}

// This method is used for showing session expired incase it is expired unfortunately.
function sessionExpired() {
    window.location.href = "alerts.aspx?se=1";
}

//This method is used to redirect page to login
function onLogout() {
    browserinvalid = true;
    _fl = true;
    if (DisplayMode == "Fixed") {
        window.location.href = "Logout.aspx?lo=true";
    }
    else {
        var dt1 = new Date();
        window.location.href = "Logout.aspx?dm=n&lo=true&d=" + dt1.getTime();
    }
}


//This method is used to raise menu click event from MenuSrc.js
function onMenuClick(sURL, bLP) {
    var tabLimit = '';
    var dTimeStamp = new Date();
    var sActivityName = getQueryStringValue(sURL, "activityname")
    var sTableName = getQueryStringValue(sURL, "tablename");
    var sActivityID = getQueryStringValue(sURL, "activityid");
    var sPageTitle = getQueryStringValue(sURL, "pagetitle");
    var sType = getQueryStringValue(sURL, "type");
    var sMode = getQueryStringValue(sURL, "mode");

    if (sURL.indexOf("Home.aspx") == -1 && sURL.indexOf("Logout.aspx") == -1 && sURL.indexOf("ChangePassword.aspx") == -1 && bLP != true) {
        if (getConfigSetting('TabLimit') != "") {
            tabLimit = getConfigSetting('TabLimit');
        }
        else {
            tabLimit = 5;
        }
        //        if (tablist.length == tabLimit) {
        //            showWarningMessagePane("Maximum Tab limit of (<B> " + tabLimit + "</B>) was reached.");
        //            return;
        //        }

        if (getQueryStringValue(sURL, "pagetype") == "Report") {
            CurrentDesk = getQueryStringValue(sURL, "listpanetitle").replace(/>/gi, "");
            CurrentDesk = CurrentDesk.replace(/&/gi, "");
            CurrentDesk = CurrentDesk.replace(/-/gi, "");
            CurrentDesk = CurrentDesk.replace("/", "")
            CurrentDesk = CurrentDesk.replace(/ /gi, "");
            //fnCreateTab(CurrentDesk, getQueryStringValue(sURL, "listpanetitle"), sURL, sType);
        }
        else {
            CurrentDesk = sActivityName.replace(/>/gi, "");
            CurrentDesk = CurrentDesk.replace(/&/gi, "");
            CurrentDesk = CurrentDesk.replace(/-/gi, "");
            CurrentDesk = CurrentDesk.replace("/", "")
            CurrentDesk = CurrentDesk.replace(/ /gi, "");
            // fnCreateTab(CurrentDesk, sActivityName, sURL, sType);
        }
        if (CurrentDesk in fnCheckvalueinArray(tablist)) {
            ShowTab(sURL, CurrentDesk, sType);
            return;
        }
        else {
            if (tablist.length == tabLimit) {
                showWarningMessagePane("Kindly Close any of the Tab,the Maximum Tab limit of (<B> " + tabLimit + "</B>) was reached.");
                return;
            }
        }
        fnCreateTab(CurrentDesk, sActivityName, sURL, sType);
    }
    closeEquipmentWindow();
    gsURL = sURL;
    gSubmitNext = getQueryStringValue(sURL, "SubmitNext");
    //Reset Modal Window Functions
    clearModalWindowFunctions();

    if (gSubmitNext != "1" && gsUIMode != "Confirm")
        closeModalWindow();

    hideHelpStrip();


    //Load Home Page
    if (sURL.indexOf("Home.aspx") >= 0) {
        //hideMessage();
        if (gsUIMode != "Confirm") {
            showInfoMessagePane("Please select the Count to view the list of Equipment awaiting for your action.");
        }
        loadHomePage();
        return;
    }

    //Logout Event
    if (sURL.indexOf("Logout.aspx") >= 0) {
        if (tablist.length > 0) {
            var blnStatus = CheckHasChanges();
            if (blnStatus) {
                showConfirmMessage(" All the changes you have made will be lost. Do you want to continue?",
                     "ConfirmYesClick();", "ConfirmNoClick();");
                return;
            }
        }
        onLogout();
        return;
    }
    if (sURL.indexOf("ChangePassword.aspx") >= 0) {
        openChangePassword();
        return;
    }

    //Get Relavant Fields
    var sType = getQueryStringValue(sURL, "type");
    var sWorkspaceURL = getQueryStringValue(sURL, "apu");
    var iActivityID = getQueryStringValue(sURL, "activityid");
    var sActivityName = getQueryStringValue(sURL, "activityname");

    gsRequestType = getQueryStringValue(sURL, "pagetype");
    gsType = sType;
    giActivityID = iActivityID;
    gsUIMode = "";

    if (sType == "CodeMaster") {
        gsMode = "edit";
        gsWorkspaceURL = sWorkspaceURL;
        //        var sListPaneTitle = getQueryStringValue(sURL, "listpanetitle");
        //        setListPaneTitle(sListPaneTitle);
        loadCodeMasterPage();
        loadingWorkflowPane("Loading " + sActivityName + "..");
    }
    else if (sType == "GridMaster") {
        gsMode = "edit";
        var sFilterName = "";
        var sFilterValue = "";
        var sActivityName = getQueryStringValue(sURL, "activityname");
        if (getQueryStringValue(sURL, "FLTR_NAM") != "undefined")
            sFilterName = getQueryStringValue(sURL, "FLTR_NAM");
        if (getQueryStringValue(sURL, "FLTR_VAL") != "undefined")
            sFilterValue = getQueryStringValue(sURL, "FLTR_VAL");
        loadGridMasterPage(sWorkspaceURL, sFilterName, sFilterValue, sActivityName);
        loadingWorkflowPane("Loading " + sActivityName + "..");
    }
    else if (sType == "Master") {
        LoadListFrame();
        giItemNo = 0
        var sListPaneTitle = getQueryStringValue(sURL, "listpanetitle");
        setListPaneTitle(sListPaneTitle);
        loadMasterPage(sWorkspaceURL);
        loadingWorkflowPane("Loading " + sActivityName + "..");
        var sFilterName = "";
        var sFilterValue = "";
        if (getQueryStringValue(sURL, "FLTR_NAM") != "undefined")
            sFilterName = getQueryStringValue(sURL, "FLTR_NAM");
        if (getQueryStringValue(sURL, "FLTR_VAL") != "undefined")
            sFilterValue = getQueryStringValue(sURL, "FLTR_VAL");
        //        dfs("plFrame").bindListGrid(iActivityID, null, sFilterName, sFilterValue);
        gsMode = "new";
        if (gsRequestType == "PostBack") {
            gsPageType = "Master";
            // hideDiv("tdDb");
            //hideDiv("tdCl"+CurrentDesk);
            showDiv("tdPl" + CurrentDesk);
            initInterface(gsPageType);
        }
    }
    else if (sType == "CustomMaster") {
        giItemNo = 0
        var sListPaneTitle = getQueryStringValue(sURL, "listpanetitle");
        setCustomListPaneTitle(sListPaneTitle);
        gsMode = "new";
        loadingCustomListPane();
        loadCustomListPage(sURL);
        loadMasterPage(sWorkspaceURL);
        loadingWorkflowPane("Loading " + sActivityName + "..");
    }
    else if (sType == "Transaction") {
        giItemNo = 0
        gsMode = getQueryStringValue(sURL, "mode");
        loadTransactionPage(sWorkspaceURL);
        loadingWorkflowPane("Loading " + sActivityName + "..");
    }
    else if (sType == "Report") {
        giItemNo = 0
        loadReportPage(sURL, sWorkspaceURL);
        loadingReportPane("Loading " + sActivityName + "..");
    }
    else if (sType == "Popup") {
        giItemNo = 0
        var sPageTitle = getQueryStringValue(sURL, "pagetitle");
    }
    confirmresult = false;
}
// Check changes on Logout
function CheckHasChanges() {
    for (var i = 0; i < tablist.length; i++) {
        confirmresult = false;
        CurrentDesk = tablist[i].id;
        var confirm = confirmChanges("menu", '');
        if (confirm) {
            return true;
        }
    }
}
function ConfirmYesClick() {
    onLogout();
    return;
}
function ConfirmNoClick() {
    return false;
}
//This method called from modalwindow while loading work space
function initGUI() {
    initInterface(gsType);
}

//This method is used load code master page
function loadCodeMasterPage() {
    var dTimeStamp = new Date();
    var sTableName = getQueryStringValue(gsURL, "tablename");
    var sActivityID = getQueryStringValue(gsURL, "activityid");
    var sPageTitle = getQueryStringValue(gsURL, "pagetitle");
    var sType = getQueryStringValue(gsURL, "type");
    var sMode = getQueryStringValue(gsURL, "mode");
    gsWorkspaceURL = gsWorkspaceURL + "?activityid=" + sActivityID + "&tablename=" + sTableName + "&mode=" + sMode + "&pagetitle=" + sPageTitle + "&pagetype=" + sType + "&ts=" + dTimeStamp.getTime();
    loadWorkspace();
}

//This method is used load grid master page
function loadGridMasterPage(sWorkspaceURL, sFilterName, sFilterValue, sActivityName) {
    var dTimeStamp = new Date();
    var sActivityID = getQueryStringValue(gsURL, "activityid");
    var sTableName = getQueryStringValue(gsURL, "tablename");
    var sPageTitle = getQueryStringValue(gsURL, "pagetitle");
    var sType = getQueryStringValue(gsURL, "type");
    var sMode = getQueryStringValue(gsURL, "mode");
    var sQrytype = getQueryStringValue(gsURL, "qrytype");
    gsWorkspaceURL = sWorkspaceURL + "?activityid=" + sActivityID + "&tablename=" + sTableName + "&mode=" + sMode + "&pagetitle=" + sPageTitle + "&pagetype=" + sType + "&qrytype=" + sQrytype + "&FLTR_NAM=" + sFilterName + "&FLTR_VAL=" + sFilterValue + "&apu=" + sWorkspaceURL + "&activityname=" + sActivityName + "&ts=" + dTimeStamp.getTime();
    //setTimeout(loadWorkspace, 1);
    loadWorkspace();
}

//This method is used load master page
function loadMasterPage(sWorkspaceURL) {
    var dTimeStamp = new Date();
    var sActivityID = getQueryStringValue(gsURL, "activityid");
    var sActivityName = getQueryStringValue(gsURL, "activityname");
    var sQrytype = getQueryStringValue(gsURL, "qrytype");
    gsWorkspaceURL = sWorkspaceURL + "?activityid=" + sActivityID + "&mode=new&activityname=" + sActivityName + "&qrytype=" + sQrytype + "&ts=" + dTimeStamp.getTime();
    //setTimeout(loadWorkspace, 1);
    loadWorkspace();
}

//This method is used load transaction page
function loadTransactionPage(sWorkspaceURL) {
    var dTimeStamp = new Date();
    gsWorkspaceURL = gsURL + "&SubmitNext=" + gSubmitNext + "&ts=" + dTimeStamp.getTime();
    //setTimeout(loadWorkspace, 1);
    loadWorkspace();
}
//This method is used load Report page
function loadReportPage(sURL, sWorkspaceURL) {
    var dTimeStamp = new Date();
    gsWorkspaceURL = sURL + "&ts=" + dTimeStamp.getTime();
    //setTimeout(loadWorkspace, 1);
    loadWorkspace();
}

//This method is used load custom list page
function loadCustomListPage(sListPaneURL) {
    gsListPaneURL = sListPaneURL;
    //setTimeout(loadListspace, 1);
    loadListspace();
}

//This method is used load work space
function loadWorkspace() {
    el("wfFrame" + CurrentDesk).src = gsWorkspaceURL;
}

function LoadListFrame() {
    el("plFrame" + CurrentDesk).src = gsURL;

}
//This method is used load custom list space
function loadListspace() {
    el("clFrame" + CurrentDesk).src = gsListPaneURL;
}

//This method is used set list pane title
function setListPaneTitle(sTitle) {
    setText(el("PlPaneHdr_lblTitle" + CurrentDesk), sTitle);
}

//This method is used set custom list pane title
function setCustomListPaneTitle(sTitle) {
    setText(el("clFramehdr_lblTitle" + CurrentDesk), sTitle);
}

//This method is used to initialize workspace
function initInterface(sPageType) {
    gsPageType = sPageType;

    //Show message when Submit & Next button clicked
    if (sPageType != "Dashboard" && gsPrevPageType != "CodeMaster") {
        if (gSubmitNext != "1" && gsUIMode != "RoleRights" && gsUIMode != "Confirm" && gsStayMessage == "")
            hideMessagePane();
    }

    switch (sPageType) {
        case "CodeMaster":case "GridMaster":
            if (($(window).height()) < 770) {
                wfHeight = 500;
            }
            else {
                wfHeight = ($(window).height()) - 165;
            }
            el("tdWf" + CurrentDesk).style.height = wfHeight + "px";
            el("wfFrame" + CurrentDesk).height = wfHeight + "px";
            // el("trWorkflowPane" + CurrentDesk).style.height = wfHeight + "px";
            showDiv("tdWf" + CurrentDesk);
            if ($(window).height() < 670) {
                var iHeight = ($(window).height()) - 187;
            }
            else {
                var iHeight = ($(window).height()) - 183;
            }
            setMasterGridHeight(iHeight);
            getIFrameObj("wfFrame" + pdf("CurrentDesk")).checkGridHeader();
            break;
        case "Master":
            //hideDiv("tdDb");
            //hideDiv("tdCl" + CurrentDesk);
            showDiv("tdPl" + CurrentDesk);
            displayWorkflowPane("new");
            el("dockListPane" + CurrentDesk).className = "icon-resize-full";
            fitWindow(el("dockListPane" + CurrentDesk));
            el("dockListPane" + CurrentDesk).className = "icon-resize-small";
            break;
        case "CustomMaster":
            showDiv("tdCl" + CurrentDesk);
            displayWorkflowPane("new");
            el("dockCustomListPane" + CurrentDesk).className = "icon-resize-full";
            fitWindow(el("dockCustomListPane" + CurrentDesk));
            el("dockCustomListPane" + CurrentDesk).className = "icon-resize-small";
            break;
        case "Transaction":
            el("tdWf" + CurrentDesk).style.height = (document.documentElement.clientHeight) - 150 + "px";
            el("wfFrame" + CurrentDesk).height = (document.documentElement.clientHeight) - 150 + "px";
            showDiv("tdWf" + CurrentDesk);
            break;
        case "Report":
            //if (($(window).height()) < 780) {
            //    wfHeight = 560;
            //}
            //else {
            //    wfHeight = ($(window).height()) - 160;
            //}
            if (($(window).height()) < 770) {
                wfHeight = 500;
            }
            else {
                wfHeight = ($(window).height()) - 165;
            }
            //alert(wfHeight);
            el("tdWf" + CurrentDesk).style.height = wfHeight + "px";
            el("wfFrame" + CurrentDesk).height = wfHeight + "px";
            showDiv("tdWf" + CurrentDesk);
            break;
        case "Dashboard":
            showDiv("tdDb");
            dfs("dbFrame").showDefaultDashboard();
            break;
        case "Tools":
            showDiv("tdDb");
            break;
        default:
    }
    if (CurrentDesk != "" && CurrentDesk != "HomeTab") {
        if (dfs("wfFrame" + CurrentDesk).toggleHelpiconColor) {
            dfs("wfFrame" + CurrentDesk).toggleHelpiconColor();
            dfs("wfFrame" + CurrentDesk).toggleFavouriteColor();
        };
    }
    loadNavigationPane();
    gsPrevPageType = sPageType;
}

//This method is used for show home page
function loadHomePage() {
    resetLockedRecord('root');
    el("productlogo").focus();
    initInterface("Dashboard");
}

//This method is used for show tools page
function loadToolsPage() {
    el("productlogo").focus();
    initInterface("GridMaster");
}

//This method is used for add button click event fired from list.js, Bypass to common.js method.
function loadEmptyPage(_mode, _url) {
    var vURL = gsURL;
    var iItemNo = "";
    var iActivityID = getQueryStringValue(vURL, "activityid");
    var sMenuData = Activities[iActivityID].split(";")[1].split("url=")[1];
    var sPageType = getQueryStringValue(sMenuData, "pagetype");
    var sActivityName = getQueryStringValue(sMenuData, "activityname");
    if (sPageType == "PostBack") {
        loadingWorkflowPane("Loading " + sActivityName + "..");
    }
    emptyData(_mode, _url, sPageType, iItemNo);
}

//This method is used for add button click event fired from list.js, Bypass to common.js method.
function initEmptyPage(_mode) {
    initPage(_mode);
}

//This method is used to display workspace from list.js.
function loadActivityPane(sPageURL, sQryStr, sMode, sPageType, iItemNo) {
    loadData(sPageURL, sQryStr, sMode, sPageType, iItemNo);
}

//This method is used to resize elements while resize the window.
function resizeWindow() {
    if (gsPageType == "Master") {
        el("tdWf" + CurrentDesk).style.height = "600px" //document.body.clientHeight - 150 + "px";
        if (el("tdPl" + CurrentDesk).style.height == "100%") {
            el("plFrame" + CurrentDesk).height = (document.documentElement.clientHeight) - 175 + "px";
            var iHeight = document.body.clientHeight - 185;
            setListGridHeight(iHeight);
        }
    }
}

//This method is used to adjust  height of list pane(like maximize, restore).
function fitWindow(img) {
    if (!img)
        return;
    if (img.className == "icon-resize-full") {
        img.className = "icon-resize-small";
        img.title = "Restore";
        if (gsPageType == "Master") {
            displayListPane();
            //            var iHeight = document.body.clientHeight - 300;
            //            setListGridHeight(iHeight);
        }
        else if (gsPageType == "CustomMaster") {
            displayCustomListPane();
            var iHeight = document.body.clientHeight - 230;
            setCustomListGridHeight(iHeight);
        }

        return;
    }
    if (img.className == "icon-resize-small") {
        img.className = "icon-resize-full";
        img.title = "Maximize";
        displayWorkflowPane();
    }
}

//This method is used to adjust  height of list pane(like maximize, restore).
function fitScreen(img) {
    if (!img)
        return;
    if (img.className == "icon-resize-full") {
        img.className = "icon-resize-small";
        img.title = "Maximize";
        $(".ui-layout-container").width("1018")
    }
    else {
        img.className = "icon-resize-full";
        img.title = "Restore";
        $(".ui-layout-container").width("100%");
    }
}

//This method is used to display workflow pane.
function displayWorkflowPane(mode) {
    var iLstFrmHeight, iWfFrmHeight, iGridHeight;
    if (($(window).height()) < 768) {
        // iWfFrmHeight = 440;
        iWfFrmHeight = $(window).height() - 300;
        iGridHeight = 600;
        iLstFrmHeight = ($(window).height()) - iWfFrmHeight - 195;
        if (iLstFrmHeight < 0) {
            iLstFrmHeight = 100;
        }
    }
    else {
        iWfFrmHeight = getConfigValue("WorkFlowFrameHeight");
        if (!iWfFrmHeight)
        //iWfFrmHeight = 440;
            iWfFrmHeight = $(window).height() - 400;
        iGridHeight = iLstFrmHeight;
        iLstFrmHeight = ($(window).height()) - iWfFrmHeight - 215;
    }

    showDiv("tdWf" + CurrentDesk);
    el("trWorkflowPane" + CurrentDesk).style.height = iWfFrmHeight + "px";
    el("wfFrame" + CurrentDesk).height = iWfFrmHeight + "px";
    if (gsPageType == "Master") {
        if (!CheckActivityinArray(giActivityID)) {
            showDiv("tdPl" + CurrentDesk);
            el("trListPane" + CurrentDesk).style.height = iLstFrmHeight + "px";
            el("tdPl" + CurrentDesk).style.height = iLstFrmHeight + "px";
            el("plFrame" + CurrentDesk).height = iLstFrmHeight + "px";
            setTimeout(setTListGridHeight, 1000);
        }
        else {
            hideDiv("tdPl" + CurrentDesk);
            el("trListPane" + CurrentDesk).style.height = 0 + "px";
        }
    }
    else if (gsPageType == "CustomMaster") {
        //hideDiv("tdPl"+CurrentDesk);
        showDiv("tdCl" + CurrentDesk);
        el("tdWf" + CurrentDesk).style.height = "420px";
        el("wfFrame" + CurrentDesk).height = "420";
        el("tdCl" + CurrentDesk).style.height = "160px";
        el("clFrame" + CurrentDesk).height = "120px";
        setCustomListGridHeight("80");
    }
}

//This method is used to display custom list pane.
function displayCustomListPane(sMode) {
    var iLstFrmHeight;
    if (document.documentElement.clientHeight < 768 || document.documentElement.clientWidth < 1024) {
        iLstFrmHeight = 600;
    }
    else {
        iLstFrmHeight = document.documentElement.clientHeight - 150;
    }
    el("tdCl" + CurrentDesk).style.height = iLstFrmHeight + "px";
    el("tdCl" + CurrentDesk).style.height = "600px" //(document.documentElement.clientHeight) - 150 + "px";
    el("tdWf" + CurrentDesk).style.height = "0%";
    hideDiv("tdWf" + CurrentDesk);
    showDiv("tdCl" + CurrentDesk);
    el("dockCustomListPane" + CurrentDesk).className = "icon-resize-small";
}

//This method is used to display list pane.
function displayListPane() {
    var iLstFrmHeight;
    if (($(window).height()) < 600) {
        iLstFrmHeight = 402;
    }
    else if (($(window).height()) < 650) {
        iLstFrmHeight = 440;
    }
    else {
        iLstFrmHeight = ($(window).height()) - 180;
    }
    el("trWorkflowPane" + CurrentDesk).style.height = "0%";
    el("tdWf" + CurrentDesk).style.height = "0%";
    el("trListPane" + CurrentDesk).style.height = iLstFrmHeight + "px";
    el("tdPl" + CurrentDesk).style.height = iLstFrmHeight + "px";
    el("plFrame" + CurrentDesk).height = iLstFrmHeight + "px";
    hideDiv("tdWf" + CurrentDesk);
    //hideDiv("tdCl"+CurrentDesk);
    showDiv("tdPl" + CurrentDesk);
    el("dockListPane" + CurrentDesk).className = "icon-resize-small";
    setTimeout(setTListGridHeight, 1000);
}

//This method is used to adjust list grid height.
function setTListGridHeight() {
    if (lfs().showListGrid) {
        lfs().showListGrid();
    }
}

//This method is used to adjust list grid height.
function setListGridHeight(_h) {
    if (_h < 0)
        return;
    var _d = el('plFrame' + CurrentDesk).contentWindow.document;
    if (_d) {
        if (_d.Script) {
            if (_h != "" && typeof (_d.Script.ifgList) != "undefined" && _d.Script.ifgList) {
                if (typeof (_d.Script.ifgList.SetStaticHeaderHeight) != "undefined") {
                    _d.Script.ifgList.SetStaticHeaderHeight(parseInt(_h) + "px");
                }
            }
        }
        else {
            _d = el('plFrame' + CurrentDesk).contentWindow;
            if (_h != "" && typeof (_d.ifgList) != "undefined" && _d.ifgList) {
                if (typeof (_d.ifgList.SetStaticHeaderHeight) != "undefined") {
                    _d.ifgList.SetStaticHeaderHeight(parseInt(_h) + "px");
                }
            }
        }

    }
}
//This method is used to adjust Master grid height.
function setMasterGridHeight(_h) {
    if (_h < 0)
        return;
    var _d = el('wfFrame' + CurrentDesk).contentWindow.document;
    if (_d) {
        if (_d.Script) {
            if (_h != "" && typeof (_d.Script.ifgCodeMaster) != "undefined" && _d.Script.ifgCodeMaster) {
                _d.Script.ifgCodeMaster.SetStaticHeaderHeight((parseInt(_h) - 100) + "px");
            }
        }
        else {
            _d = el('wfFrame' + CurrentDesk).contentWindow;
            if (_h != "" && typeof (_d.ifgCodeMaster) != "undefined" && _d.ifgCodeMaster) {
                _d.ifgCodeMaster.SetStaticHeaderHeight((parseInt(_h) - 100) + "px");
            }
        }

    }
}


//This method is used to adjust list grid height.
function setCustomListGridHeight(_h) {
    if (_h < 0)
        return;
    var _d = el('clFrame' + CurrentDesk).contentWindow.document;
    if (_d) {
        if (_d.Script) {
            if (_h != "" && typeof (_d.Script.ifgList) != "undefined" && _d.Script.ifgList) {
                _d.Script.ifgList.SetStaticHeaderHeight(parseInt(_h) + "px");
            }
        }
        else {
            _d = el('plFrame' + CurrentDesk).contentWindow;
            if (_h != "" && typeof (_d.ifgList) != "undefined" && _d.ifgList) {
                _d.ifgList.SetStaticHeaderHeight(parseInt(_h) + "px");
            }
        }
    }
}

//This method is used to refresh list grid.
function refreshListPane(_iActivityID) {
    dfs("plFrame" + CurrentDesk).bindListGrid(_iActivityID, true, "", "");
}

//This method is used to refresh list grid.
function refreshCustomListPane(_iActivityID) {
    dfs("clFrame" + CurrentDesk).bindListGrid(_iActivityID, true, "", "");
}

//This method is used to identify type of list page in workspace.
function setListAction() {
    if (gsType == "CustomMaster")
        dfs("wfFrame" + CurrentDesk).ListAction = "CustomList";
    else if (gsType == "Master")
        dfs("wfFrame" + CurrentDesk).ListAction = "List";
    else
        dfs("wfFrame" + CurrentDesk).ListAction = "";
}

//This method used to download the IE shortcut based on browser
function downloadShortcut(_path) {
    //Confirm changes
    var confirm = false;
    if (confirmresult == false) {
        confirm = confirmChanges("menu", "psc().downloadShortcut('" + _path + "');");
    }
    if (confirm)
        return;

    el("hiddenLinkShortcut").setAttribute("href", _path);
    el("hiddenLinkShortcut").click();
}
//This method is used to open the Change password page.
function openChangePassword() {
    showModalWindow("Change Password", "Admin/ChangePassword.aspx", "400px", "250px", "", "");
}

//This is used to re-align the menu on re-sizing the window
window.onresize = function () {
    //  var bws = getBrowserHeight();
    //    if (document.documentElement.clientHeight < 725 || document.documentElement.clientWidth < 1055) {
    //        return false;
    //    }
    //    else {
    fitMenu();
    fitCenter();
    fitListFrame();
    //}
};
//This is used to re-align the menu on re-sizing the window
function fitMenu() {
    if (el("menu0") != null) {
        el("menu0").style.left = ((window.innerWidth || document.body.clientWidth) / 2) - 457 + "px";
        el("menu0").style.top = $(document).scrollTop() + 36 + "px";
    }
}
// This is used to re-align the height on re-sizing the window
function fitCenter() {
    if (el("layoutContainer") != null) {
        var iTblHeight;
        var iFrmHeight;
        $('#layoutContainer').height($(window).height());
        $('#divCenterBar').height($(window).height() - 125);
        $('#divMainBar').height($(window).height() - 141);
        iFrmHeight = document.documentElement.clientHeight - 125;
        _ih = iFrmHeight;
        setTimeout(fitLayout, 700);
    }
}
function fitLayout() {
    el("divCenterBar").style.height = _ih + "px";
}
// This is used to re-align the height of Workflow and listpane on re-sizing the window
function fitListFrame() {
    var iTblHeight, iFrmHeight, iLstFrmHeight, iWfFrmHeight;
    if (($(window).height()) < 768) {
        iTblHeight = 764;
        iFrmHeight = $(window).height()-200;
        iWfFrmHeight = 360;
        iLstFrmHeight = ($(window).height()) - iWfFrmHeight - 165;
        if (iLstFrmHeight < 0) {
            iLstFrmHeight = 100;
        }
    }
    else {
        iTblHeight = $(window).height();
        iFrmHeight = iTblHeight - 290;
        iWfFrmHeight = getConfigValue("WorkFlowFrameHeight");
        if (!iWfFrmHeight)
            iWfFrmHeight = 635;
        iLstFrmHeight = iTblHeight - iWfFrmHeight - 165;
    }
    if (el("trWorkflowPane" + CurrentDesk) != null && el("trListPane" + CurrentDesk) != null) {
        if (el("dockListPane" + CurrentDesk).title == "Restore") {
            el("trWorkflowPane" + CurrentDesk).style.height = "0%";
            el("trListPane" + CurrentDesk).style.height = iFrmHeight + "px";
            el("tdPl" + CurrentDesk).style.height = iFrmHeight + "px";
            if (chrome) {
                el("tdPl" + CurrentDesk).style.width = $(window).width() + "px";
            }
            el("plFrame" + CurrentDesk).height = iFrmHeight + "px";
            var iHeight;
            if (($(window).height()) < 768) {
                iHeight = document.body.clientHeight - 300;
            }
            else {
                iHeight = document.body.clientHeight - 360;
            }
            setListGridHeight(iHeight);
        }        
        else {
            if (el("dockListPane" + CurrentDesk).style.display != "none") {
                el("trWorkflowPane" + CurrentDesk).style.height = 400 + "px";
                el("trListPane" + CurrentDesk).style.height = iLstFrmHeight + "px";
                el("tdPl" + CurrentDesk).style.height = iLstFrmHeight + "px";
                if (chrome) {
                    el("tdPl" + CurrentDesk).style.width = $(window).width() + "px";
                }
            }
            if (el("dockListPane" + CurrentDesk).style.display == "none") {                
                el("trWorkflowPane" + CurrentDesk).style.height = $(window).height() - 162 + "px";
                el("tdWf" + CurrentDesk).style.height = $(window).height() - 162 + "px";
                el("wfFrame" + CurrentDesk).style.height = $(window).height() - 162 + "px";
                
            }

        }
    }
    else if (el("trListPane" + CurrentDesk) == null && el("trWorkflowPane" + CurrentDesk) != null) {
        // iWfFrmHeight = parseInt(iWfFrmHeight) + 579;
        iWfFrmHeight = $(window).height() - 165;
        el("trWorkflowPane" + CurrentDesk).style.height = iWfFrmHeight + "px";
        el("tdWf" + CurrentDesk).style.height = iWfFrmHeight + "px";
        el("wfFrame" + CurrentDesk).height = iWfFrmHeight + "px";
        var iHeight = document.body.clientHeight - 190;
        //var iHeight = document.body.clientHeight - 300;
        setMasterGridHeight(iHeight);

    }
    if (el("dockListPane" + CurrentDesk) && el("dockListPane" + CurrentDesk).style.display != "none") {
        if (el("dockListPane" + CurrentDesk).className == "icon-resize-full")
            el("dockListPane" + CurrentDesk).className = "icon-resize-small";
        else
            el("dockListPane" + CurrentDesk).className = "icon-resize-full";
        fitWindow(el("dockListPane" + CurrentDesk))
    }

}

//This function is used to get the Config value from the server using a call back
function getConfigValue(keyName) {
    var KeyValue = '';
    if (ConfigClass[keyName]) {
        if (ConfigClass[keyName] != '' && ConfigClass[keyName] != "undefined") {
            KeyValue = ConfigClass[keyName];
        }
    }
    else {
        var oCallback = new Callback();
        oCallback.add("KeyName", keyName);
        oCallback.invoke("Home.aspx", "GetKeyValue");
        if (oCallback.getCallbackStatus()) {
            KeyValue = oCallback.getReturnValue("KeyValue");
        }
    }
    return KeyValue;
}

var weekday = { 0: "Sunday", 1: "Monday", 2: "Tuesday", 3: "Wednesday", 4: "Thursday", 5: "Friday", 6: "Saturday" };
var months = { 0: "January", 1: "February", 2: "March", 3: "April", 4: "May", 5: "June", 6: "July", 7: "August", 8: "September", 9: "October", 10: "November", 11: "December" };

function startTime() {
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    m = checkTime(m);
    s = checkTime(s);
    el('spnTime').innerHTML = h + ":" + m + ":" + s;
    el('spnTime').title = weekday[today.getDay()] + "," + months[today.getMonth()] + " " + today.getDate() + "," + today.getFullYear();
    t = setTimeout(function () { startTime() }, 500);
}

function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

function ViewUserProfile(UserID) {
    showModalWindow("User Profile", "Admin/User.aspx?activityid=1&mode=edit&popup=yes&userid=" + UserID, "580", "340", "", "", "")
}
function ChangeStyle(ThemeName) {

    if (OldTheme != ThemeName) {
        _as = true;
        _fl = true;
        var _mUrl = location.href;
        var dat = new Date();

        _mUrl = _mUrl.substring(0, _mUrl.indexOf("ts"));

        _mUrl = _mUrl + "ts=" + dat.getTime();
        document.location.href = _mUrl;
        fnSetThemeName(ThemeName);
    }
}

function fnSetThemeName(ThemeName) {
    OldTheme = ThemeName;
}

function fnCreateTab(id, title, url, type) {
    previousTab = id;
    var newTab, newTablist, tablistcontent;
    var newList, tabPage;
    var swidth = screen.width - 50;
    var sheight = screen.height - 308;
    newList = "li" + id;
    removedTabId = "";
    if (id in fnCheckvalueinArray(tablist)) {
        ShowTab(url, id, type);
        return;
    }

    newTablist = document.createElement("li")
    newTablist.id = newList;
    newTablist.innerHTML = "<a href='#' rel='" + id + "' onclick='ShowTab(\"" + url + "\",\"" + id + "\",\"" + type + "\");' > <span>" + title + " &nbsp;<i id='ico" + id + "' class='icon-remove-sign' title='close' onclick='CloseTab(\"" + id + "\");return false;' /></span></a>";

    tabListContent = el("deskTabs");
    tabListContent.appendChild(newTablist);

    var vcCallback = new Callback();
    vcCallback.add("TableName", id);
    vcCallback.add("Title", title);
    vcCallback.add("Url", url);
    vcCallback.add("Type", type);
    vcCallback.add("TabPage", tabPage);

    vcCallback.invoke("Home.aspx", "CreateTab");

    if (vcCallback.getCallbackStatus()) {
        newTab = document.createElement("div")
        newTab.id = id;
        newTab.innerHTML = unescape(vcCallback.getReturnValue("TabPage"));
    }
    else {
    }
    vcCallback = null;

    var tabpages;
    tabpages = el("deskTabPages");
    tabpages.appendChild(newTab);
    previousTab = newTab.id;
    ShowTab(url, newTab.id, type);
    fnAddvaluetoArray(tablist, id, url, type);
    $("#deskTabs").sortable({
        items: "li:not(#liHomeTab)"
    });

    fitCenter();
}

function showCloseButton(obj) {
    el("ico" + obj.rel).style.position = "absolute";
    showDiv("ico" + obj.rel);
}

function hideCloseButton(obj) {
    el("ico" + obj.rel).style.position = "relative";
    hideDiv("ico" + obj.rel);
}
function fnAddvaluetoArray(arrayName, value, _url, type) {
    var nextElement = arrayName.length;
    var tab = { "id": value, "url": _url, "type": type }
    arrayName[nextElement] = tab;
}

function fnCheckvalueinArray(a) {
    var Arr = {};
    for (var i = 0; i < a.length; i++) {
        Arr[a[i].id] = '';
    }
    return Arr;
}

function CheckActivityinArray(a) {
    for (var i = 0; i < activityIdList.length; i++) {
        if (a == activityIdList[i]) {
            return true;
        }
    }
    return false;
}

function ShowTab(url, tabid, type) {
    if (tabid != removedTabId) {
        CurrentDesk = tabid;
        if (LastDesk != "") {
            el("li" + LastDesk).className = "";
            hideDiv("tbl" + LastDesk);
        }
        // showDiv("tbl" + CurrentDesk); 
        el("tbl" + CurrentDesk).style.display = "table"; //chrome v50 issue table not in full width
        el("li" + CurrentDesk).className = "current";
        if (CloseStatus == false) {
            if (tabid == previousTab) {
                LastDesk = previousTab;
            }
        }
        else {
            giActivityID = getQueryStringValue(url, "activityid");
            previousTab = LastDesk;
            LastDesk = CurrentDesk;
        }

        loadNavigationPane();
    }
    gsURL = url;
}

function CloseTab(tabid, confirmCall) {
    var confirm = false;
    var activityName = "";
    CurrentDesk = tabid;
    if (!confirmCall)
        confirmresult = false;
    if (confirmresult == false) {
        confirm = confirmChanges("menu", "psc().CloseTab('" + tabid + "',true);");
    }
    if (confirm) {
        CloseStatus = false;
        return;
    }
    hideMessagePane();
    CloseStatus = true;
    el("li" + tabid).style.display = "none";
    removedTabId = tabid;
    for (var i = 0; i < tablist.length; i++) {
        if (tablist[i].id == tabid) {
            createRecentItem(getQueryStringValue(tablist[i].url, "activityid"), getQueryStringValue(tablist[i].url, "pagetitle"));
            activityName = getQueryStringValue(tablist[i].url, "activityname");
            if (tabid != LastDesk && tabid != previousTab) {
                if (LastDesk != previousTab) {
                    ShowTab('', LastDesk, '');
                }
            }
            else if (tablist.length > 0 && tablist.length > i + 1) {
                ShowTab(tablist[i + 1].url, tablist[i + 1].id, tablist[i + 1].type);
            }
            else if (tablist.length > 1 && tablist.length <= i + 1) {
                ShowTab(tablist[i - 1].url, tablist[i - 1].id, tablist[i - 1].type);

            }
            else {
                ShowTab("Home.aspx", "HomeTab", "normal");
            }
            tablist.splice(i, 1);
        }
    }
    try {
        var tabpages, tabListContent;
        tabpages = el("deskTabPages");
        var removeTabPage = el(tabid)
        try {
            tabpages.removeChild(removeTabPage);
            //Unlock Record Locking
            removeRecordsClosingTab(activityName)
        }
        catch (e) { }
        tabListContent = el("deskTabs");
        var removeTab = el("li" + tabid);
        tabListContent.removeChild(removeTab);
        return false;
        // return;
    }
    catch (e) { return false; }
}


//This function is used to toggle the Css Style
function toggleStyle(obj, c) {
    obj.className = c;
}

//This function is used to load the navigation 
function loadNavigationPane() {
    if (pdf("CurrentDesk") != '' && pdf("CurrentDesk") != 'HomeTab') {
        if (dfs("wfFrame" + pdf("CurrentDesk"))) {
            if (dfs("wfFrame" + pdf("CurrentDesk")).getWFDATA) {
                var strQuickLinkIDs = dfs("wfFrame" + pdf("CurrentDesk")).getWFDataKey("QCK_LNK_ID_CSV");
                var strMasterIDs = dfs("wfFrame" + pdf("CurrentDesk")).getWFDataKey("MSTR_ID_CSV");
                loadRelatedMasters(strMasterIDs);
                loadQuickLinks(strQuickLinkIDs);
            }
        }
    }
}
function unFoldNavigationPane() {
    if ($(".ui-layout-toggler")) {
        $(".ui-layout-toggler").click();
    }
}

function loadRelatedMasters(strMasterIds) {
    el("ulMasters").innerHTML = "";
    if (strMasterIds != "") {
        var _Ids = strMasterIds.split(",");
        for (var icount = 0; icount < _Ids.length; icount++) {
            var sMenuData = Activities[_Ids[icount]].split(";")[1].slice(4);
            var Master_List = "";
            //            Master_List = Master_List.concat("<li><a href='#' onclick='onMenuClick(&quot;", sMenuData, "&quot;);return false;'>", getQueryStringValue(sMenuData, "pagetitle"), "</a></li></br>");
            Master_List = Master_List.concat("<li><a style='WHITE-SPACE: nowrap' href='#' onclick='onMenuClick(&quot;", sMenuData, "&quot;);return false;'>", getQueryStringValue(sMenuData, "activityname"), "</a></li></br>");
            $('#ulMasters').append(Master_List);
        }
        el("accordionMaster").children[1].style.height = _Ids.length * 30 + "px";
    }
}

function loadQuickLinks(strQuicklinksIds) {
    el("ulQuickLinks").innerHTML = "";
    if (strQuicklinksIds != "") {
        var _Ids = strQuicklinksIds.split(",");
        for (var icount = 0; icount < _Ids.length; icount++) {
            var sMenuData = Activities[_Ids[icount]].split(";")[1].slice(4);
            var Quick_Link_List = "";
            //            Quick_Link_List = Quick_Link_List.concat("<li><a href='#' onclick='onMenuClick(&quot;", sMenuData, "&quot;);return false;'>", getQueryStringValue(sMenuData, "pagetitle"), "</a></li></br>");
            Quick_Link_List = Quick_Link_List.concat("<li><a style='WHITE-SPACE: nowrap' href='#' onclick='onMenuClick(&quot;", sMenuData, "&quot;);return false;'>", getQueryStringValue(sMenuData, "activityname"), "</a></li></br>");

            $('#ulQuickLinks').append(Quick_Link_List);
        }
        el("accordionQL").children[1].style.height = _Ids.length * 25 + "px";
    }
}
//PageTile argument has been added to fix the UIG issue ( Favourite button is not working) - changes
function toggleFavouritesList(blnRemove, pageTitle) {
    if (blnRemove) {
        if (strFavActivityIds != "") {
            var _Ids = strFavActivityIds.split(",");
            var Fav_Link_List = "";
            for (var icount = 0; icount < _Ids.length; icount++) {
                if (_Ids[icount] != giActivityID) {
                    if (icount != _Ids.length - 1) {
                        Fav_Link_List = Fav_Link_List.concat(_Ids[icount], ",")
                    }
                    else {
                        Fav_Link_List = Fav_Link_List.concat(_Ids[icount])
                    }

                }
            }
            strFavActivityIds = Fav_Link_List;
        }
    }
    else {
        if (strFavActivityIds != "") {
            strFavActivityIds = strFavActivityIds.concat(",", giActivityID)
        }
        else {
            strFavActivityIds = strFavActivityIds.concat(giActivityID)

        }
    }
    var oCallback = new Callback();
    oCallback.add("FavActivityIds", strFavActivityIds);
    oCallback.add("State", blnRemove);
    oCallback.invoke("Home.aspx", "SetFavourites");
    if (oCallback.getCallbackStatus()) {
        if (blnRemove) {
            showInfoMessagePane("" + pageTitle + " has been unmarked as Favorites.");
        }
        else {
            showInfoMessagePane("" + pageTitle + " has been marked as Favorites.");
        }
    }
    loadFavouritesLinks();
}

function loadFavouritesLinks() {
    el("ulFavourites").innerHTML = "";
    if (strFavActivityIds != "") {
        var _Ids = strFavActivityIds.split(",");
        for (var icount = 0; icount < _Ids.length; icount++) {
            if (_Ids[icount] != "") {
                var sMenuData = Activities[_Ids[icount]].split(";")[1].slice(4);
                var Fav_Link_List = "";
                //                Fav_Link_List = Fav_Link_List.concat("<li><a href='#' onclick='onMenuClick(&quot;", sMenuData, "&quot;);return false;'>", getQueryStringValue(sMenuData, "pagetitle"), "</a></li></br>");
                Fav_Link_List = Fav_Link_List.concat("<li><a style='WHITE-SPACE: nowrap' href='#' onclick='onMenuClick(&quot;", sMenuData, "&quot;);return false;'>", getQueryStringValue(sMenuData, "activityname"), "</a></li></br>");

                $('#ulFavourites').append(Fav_Link_List);
            }
            el("accordionFav").children[1].style.height = _Ids.length * 35 + "px";
        }
    }
}

function isFavouriteActivity() {
    if (strFavActivityIds != "") {
        var _Ids = strFavActivityIds.split(",");
        for (var icount = 0; icount < _Ids.length; icount++) {
            if (_Ids[icount] == giActivityID) {
                return true;
            }
        }
    }
    return false;
}

function createRecentItem(activityID, pageTitle) {
    var objRecentItem = new recentItemObj(activityID, pageTitle);
    if (recentItem.length > 5) {
        recentItem.splice(0, 1);
    }
    for (var i = 0; i < recentItem.length; i++) {
        if (recentItem[i].ActivityID == activityID) {
            recentItem.splice(i, 1);
        }
    }
    recentItem.push(objRecentItem);
    el("ulRecentItems").innerHTML = "";
    for (var icount = 0; icount < recentItem.length; icount++) {
        $('#ulRecentItems').append("<li><a href='#' onclick='openRecentItem(&quot;" + recentItem[icount].ActivityID + "&quot;);return false;'>" + recentItem[icount].PageTitle + "</a></li></br>");
    }
    el("accordionRI").children[1].style.height = "140px"
}

function removeRecentItem(activityID, itemNo) {
    for (var i = 0; i < recentItem.length; i++) {
        if (recentItem[i]) {
            if (recentItem[i].ActivityID == activityID) {
                recentItem.pop(itemCode);
            }
        }
    }

}
var _RitemNo;
function openRecentItem(activityID) {
    for (var i = 0; i < recentItem.length; i++) {
        if (recentItem[i].ActivityID == activityID) {
            _RitemNo = i;
            break;
        }
    }
    var sMenuData = Activities[recentItem[_RitemNo].ActivityID].split(";")[1].slice(4);
    onMenuClick(sMenuData);
}

function isOnline() {
    var status = navigator.onLine ? 'online' : 'offline',
     indicator = document.getElementById('lblindicator'),
        current = indicator.title;
    if (current != status) {
        indicator.title = status;
    };
    if (current == 'offline') {
        if (el("spnIndicator").style.color == "red")
            el("spnIndicator").style.color = "white";
        else
            el("spnIndicator").style.color = "red";
    } else {
        el("spnIndicator").style.color = "#83a94c";
    }
    setTimeout(isOnline, 1000);
}
if (window.$) { $(isOnline); }

if (window.$) {
    $(isOnline);
    $(checkIsMaintenance);
}

function checkIsMaintenance() {
    var oCallback = new Callback();
    oCallback.invoke("Home.aspx", "CheckIsMaintenance");
    if (oCallback.getCallbackStatus()) {
        showImportantMessage(oCallback.getReturnValue("SystemMessage"));
    }
    setTimeout(checkIsMaintenance, 120000);
}

function removeRecordsClosingTab(sActivity) {
    var oCallback = new Callback();
    oCallback.add("TabID", sActivity);
    oCallback.invoke("Home.aspx", "RemoveLockedRecordsClosingTab");
    if (oCallback.getCallbackStatus()) {

    }
}
// This function is to set hide list frame and display workflow frame
function hideListGrid() {
    var bws = getBrowserHeight();
    var wfheight;
    if (bws.height > 768) {
        // wfheight = 865;
        wfheight = $(window).height() - 170;
    }
    else {
        wfheight = $(window).height() - 180;
    }
    pel("trListPane" + pdf("CurrentDesk")).style.display = "none";
    pel("tdPl" + pdf("CurrentDesk")).style.display = "none";
    pel("trWorkflowPane" + pdf("CurrentDesk")).style.height = wfheight + "px";
    pel("tdWf" + pdf("CurrentDesk")).style.height = wfheight + "px";
    pel("wfFrame" + pdf("CurrentDesk")).style.height = wfheight + "px";

}
