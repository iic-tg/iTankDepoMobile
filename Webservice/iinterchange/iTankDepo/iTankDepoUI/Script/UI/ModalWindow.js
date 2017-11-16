
//Declarations
var gsTitle;
var gsModalWindowFunction;
var gsModalWindowFunctionBefore;
var gsPopupReturnValue;
var gsModalWindowStatus;
var gsmodalWindowType;
var gsStayMessage;
var gsStayMessageOnInit;
var gsYesMethod;
var gsNoMethod;
var gsCancelMethod;
var gsCancelModalWindow = false;
var info = "icon-info-sign";
var success = "icon-ok";
var error = "icon-remove";
var warning = "icon-warning-sign";
var errorToast;
var infoToast;
var warningToast;

var importantToast;

//This method is used to show information message
function showInfoMessagePane(sMessage) {
    if (infoToast)
        $(infoToast).remove();
    infoToast=$().toastmessage('showNoticeToast', sMessage);
    return false;
}

//This method is used to show important message
function showImportantPane(sMessage) {
    if (importantToast)
        $(importantToast).remove();
    importantToast = $().toastmessage('showImportantToast', sMessage);
    return false;
}

//This method is used to set popup function
function onAfterCloseModalWindow(sFunctionName) {
    gsModalWindowFunction = sFunctionName;
}

//This method is used to set popup function
function onBeforeCloseModalWindow(sFunctionName) {
    gsModalWindowFunctionBefore = sFunctionName;
}


//This method is used to set popup function
function clearModalWindowFunctions() {
    gsModalWindowFunction = "";
    gsModalWindowStatus = true;
    gsModalWindowFunctionBefore = "";
}

//This method is used to show warning message
function showWarningMessagePane(sMessage) {
   if (warningToast)
      $(warningToast).remove();
    warningToast= $().toastmessage('showWarningToast', sMessage);           
   // $().toastmessage('showWarningToast', sMessage);
    return false;
}

//This method is used to show error message
function showErrorMessagePane(sMessage) {
    if (errorToast)
        $(errorToast).remove();
    if (sMessage == "" || sMessage == null) {
        sMessage = "OOPS:System Error. Please contact administrator.";
//        sMessage += " <b><i><a style='font-size: 8pt' href='#' onclick='openReportError();return false;'>Report This Error</a></i></b>";
//        errorToast = $().toastmessage('showErrorToast', {
//            text: sMessage,
//            sticky: true,
//            position: 'bottom-right',
//            type: error,
//            close: function () { return; }
//        });
    }
//    else {
//       errorToast= $().toastmessage('showErrorToast', sMessage);        
    //    }
    errorToast = $().toastmessage('showErrorToast', sMessage); 
    return false;
}

//This method is used to show error message
function showErrorMessagePaneWithReport(sMessage) {
    if (errorToast)
        $(errorToast).remove();
    if (sMessage == "" || sMessage == null) {
        sMessage = "OOPS:System Error. Please contact administrator.";
        sMessage += " <b><i><a style='font-size: 8pt' href='#' onclick='openReportError();return false;'>Report This Error</a></i></b>";
        errorToast = $().toastmessage('showErrorToast', sMessage);   
    }
    else {
        sMessage += " <b><i><a style='font-size: 8pt' href='#' onclick='openReportError();return false;'>Report This Error</a></i></b>";
        errorToast = $().toastmessage('showErrorToast', sMessage);   
    }   
    return false;
}

//This method is used to report the error.
function openReportError() {
    showModalWindow("Report Error", "Admin/ReportError.aspx", "600px", "400px", "", "");
}

//This method is used to show confirmation window.
function showConfirmMessagePane(title, yesMethod, noMethod,cancelMethod) {
    gsYesMethod = yesMethod;
    gsNoMethod = noMethod;
    gsCancelMethod = cancelMethod;
    gsModalWindowFunction = "";
    focuscontroltype = "confirm";
    $('.btncorner').corner();
    $("#confirmframe").draggable({ containment: "window" });
    
    setConfirmWindowLayerPosition();

    if (el("spnConfirmMessage") == null)
        el("spnConfirmMessage").innerHTML = title;
    else
        pel("spnConfirmMessage").innerHTML = title;

    var confirmshadow;
    var popupconfirmcontent;

    if (el("confirmshadow") == null)
        confirmshadow = pel("confirmshadow");
    else
        confirmshadow = el("confirmshadow");

    if (el("confirmframe") == null)
        popupconfirmcontent = pel("confirmframe");
    else
        popupconfirmcontent = el("confirmframe");

    confirmshadow.style.display = "block";
    popupconfirmcontent.style.display = "block";

    el('btnNo').focus();
    if (document.attachEvent) {
        el("btnNo").attachEvent("onkeypress", disableKey);
        el("btnNo").attachEvent("onkeydown", disableKey);
    }
    else {
        el("btnNo").onkeypress = disableKey;
        el("btnNo").onkeydown = disableKey;
        el("btnNo").onkeyup = disableKey;
    }  
    confirmshadow = null;
    popupconfirmcontent = null;
    window.onresize = setConfirmWindowLayerPosition;
}

//This method is used to show confirmation window.
function showEquipmentWindow() {
    //Confirm changes
    var confirm = false;
    if (confirmresult == false) {
        confirm = confirmChanges("menu", "psc().showEquipmentWindow();");
    }
    if (confirm)
        return;

    action = "ModalWindow";

    $('.btncorner').corner();

    var popupequipmentcontent;
    if (el("equipmentframe") == null)
        popupequipmentcontent = pel("equipmentframe");
    else
        popupequipmentcontent = el("equipmentframe");

    popupequipmentcontent.style.display = "block";

    el('btnGo').focus();

    el("txtEquipmentNumber").value = "";
    setFocusToField("txtEquipmentNumber");

    popupequipmentcontent = null;
}

function closeEquipmentWindow() {
    if (typeof (el("equipmentframe")) != 'undefined' && el("equipmentframe")!= null) {
        var popupequipmentcontent;
        if (el("equipmentframe") == null)
            popupequipmentcontent = pel("equipmentframe");
        else
            popupequipmentcontent = el("equipmentframe");

        el("equipmentframe").style.display = "none";

    }
}

//This method is used to set confirmation window layer position.
function setConfirmWindowLayerPosition() {
    var confirmshadow;
    var popupconfirmcontent;

    if (el("confirmshadow") == null)
        confirmshadow = pel("confirmshadow");
    else
        confirmshadow = el("confirmshadow");

    if (el("confirmframe") == null)
        popupconfirmcontent = pel("confirmframe");
    else
        popupconfirmcontent = el("confirmframe");

    el("confirmframe").style.display = "none";

    var bws = getBrowserHeight();
    confirmshadow.style.width = bws.width + "px";  
    popupconfirmcontent.style.left = parseInt((bws.width - 310) / 2);
    popupconfirmcontent.style.top = parseInt((bws.height - 200) / 2);


    confirmshadow = null;
    popupconfirmcontent = null;
}


//This method is used to hide confirmation window.
function hideConfirmMessagePane(clickType) {
    var confirmshadow
    var popupconfirmcontent;

    if (el("confirmshadow") == null)
        confirmshadow = pel("confirmshadow");
    else
        confirmshadow = el("confirmshadow");

    if (el("confirmframe") == null)
        popupconfirmcontent = pel("confirmframe");
    else
        popupconfirmcontent = el("confirmframe");

    confirmshadow.style.display = "none";
    popupconfirmcontent.style.display = "none";

    confirmshadow = null;
    popupconfirmcontent = null;
    if (document.detachEvent) {
        el("btnNo").detachEvent("onkeypress", disableKey);
        el("btnNo").detachEvent("onkeydown", disableKey);
    }
    else {
        el("btnNo").onkeypress = null;
        el("btnNo").onkeydown = null;
        el("btnNo").onkeyup = null;
    } 
    hideLayer();
    if (typeof (gsCancelMethod) != "undefined" && gsCancelMethod != "" && clickType =="CANCEL") {
        eval(gsCancelMethod);
    }
}

//This method is used to raise on after no button click in confirmation window
function noClick() {
    gsUIMode = "Confirmation";
    hideConfirmMessagePane('NO');
    eval(gsNoMethod);
    gsUIMode ="";
}

//This method is used to raise on after yes button click in confirmation window
function yesClick() {
    gsUIMode = "Confirmation";
    hideConfirmMessagePane('YES');
    eval(gsYesMethod);
    gsUIMode = "";
}

//This method is used to hide the status bar message
function hideMessagePane() {   
    $('.toast-container').remove();
}

//This method is used to show the modal window
function ShowModalWindow(title, url, vWidth, vHeight, vTop, vLeft, onAfterClose, modalWindowType, onBeforeClose) {
    closeEquipmentWindow();
    clearModalWindowFunctions();
    gsmodalWindowType = modalWindowType;
    onAfterCloseModalWindow(onAfterClose);
    onBeforeCloseModalWindow(onBeforeClose);
    $('.btncorner').corner();

    gsTitle = title;
    $("#modalframe").draggable({ containment: "window" });

    loadingModalFrame("Loading " + title);

    action = "ModalWindow";

    var tsm = new Date();
    var popupcontent = el('modalframe')

    if (url.indexOf("?") == -1) {
        url = url + "?tsm=" + tsm.getTime();
    }
    else {
        url = url + "&tsm=" + tsm.getTime();

    }
    el('fmModalFrame').src = url;
    setText(el('modalframetitle'),title);

    popupcontent.style.width = vWidth;
    popupcontent.style.height = vHeight;
    popupcontent.style.top = vTop;
    popupcontent.style.left = vLeft;

    var oShadowFrame = el("shadow");
    var oBrowser = getBrowserHeight();

    oShadowFrame.style.width = oBrowser.width + "px";
    //oShadowFrame.style.height = oBrowser.height + "px";
    oShadowFrame.style.display = "block";

    if (Animate) {
        $('#modalframe').fadeIn("slow");
    }
    else {
        el("modalframe").style.display = "block"; 
    }

    if (vTop != "" && vLeft != "") {
        setWindowLayout1(popupcontent);
    }
    else {
        setWindowLayout(popupcontent);
    }

    oShadowFrame = null;
    popupcontent = null;
}


//This method is used to show the hidden modal window
function ShowHiddenWindow(title, url, vWidth, vHeight, vTop, vLeft, onAfterClose, modalWindowType, onBeforeClose) {
    clearModalWindowFunctions();
    gsmodalWindowType = modalWindowType;
    onAfterCloseModalWindow(onAfterClose);
    onBeforeCloseModalWindow(onBeforeClose);
    $('.btncorner').corner();

    gsTitle = title;
    
    loadingHiddenFrame("Loading " + title);

    action = "ModalWindow";

    var tsm = new Date();
    var popupcontent = el('modalframe')

    if (url.indexOf("?") == -1) {
        url = url + "?tsm=" + tsm.getTime();
    }
    else {
        url = url + "&tsm=" + tsm.getTime();

    }
    el('fmModalFrame').src = url;
    setText(el('modalframetitle'),title);

    popupcontent.style.width = vWidth;
    popupcontent.style.height = vHeight;
    popupcontent.style.top = vTop;
    popupcontent.style.left = vLeft;

    var oShadowFrame = el("shadow");
    var oBrowser = getBrowserHeight();

    oShadowFrame.style.width = oBrowser.width + "px";
    //oShadowFrame.style.height = oBrowser.height + "px";
    oShadowFrame.style.display = "block";   

    setWindowLayout(popupcontent);

    oShadowFrame = null;
    popupcontent = null;
}

//This method used to reload
function reloadModalWindow(bStatus) {
    gsModalWindowStatus = bStatus;
}

//This method is used to close the modal window
function closeModalWindow() {
    //invoke onbefore close
    if (typeof (gsModalWindowFunctionBefore) != "undefined" && gsModalWindowFunctionBefore != "" && gsCancelModalWindow == false) {
        eval(gsModalWindowFunctionBefore);
    }

    if (gsCancelModalWindow == false) {
        hideLayer();
        var shadow;
        var modalframe;
        if (el("shadow") == null) shadow = ppel("shadow");
        else shadow = el("shadow");
        if (el("modalframe") == null) modalframe = ppel("modalframe");
        else modalframe = el("modalframe");
        shadow.style.display = "none";
        modalframe.style.display = "none";
        shadow = null;
        modalframe = null;
        //el("fmModalFrame").src = "about:blank";
    }
    //invoke onafter close
    if (typeof (gsModalWindowFunction) != "undefined" && gsModalWindowFunction != "" && gsCancelModalWindow==false) {
        eval(gsModalWindowFunction);
    }
    if (typeof (gsStayMessage) == "undefined" || gsStayMessage=="") {
        hideMessagePane();
    }
    gsCancelModalWindow = false;
    //gsStayMessage = "";
}

//This method is used to set the modal window layout
function setWindowLayout(oPopupContent) {
    var iWidth = oPopupContent.style.width.split("px")[0];
    var iHeight = oPopupContent.style.height.split("px")[0];
    var iLeftPos = oPopupContent.style.left.split("px")[0];
    var iTopPos = oPopupContent.style.top.split("px")[0];

    var oBrowser = getBrowserHeight();

    iLeftPos = oBrowser.width - iWidth;
    iTopPos = oBrowser.height - iHeight;

    oPopupContent.style.left = iLeftPos / 2;
    if (iHeight > 380) {
        oPopupContent.style.top = iTopPos / 5;
    }
    else {
        oPopupContent.style.top = iTopPos / 2.5;
    }
}

//This method is used to set the modal window layout
function setWindowLayout1(oPopupContent) {
    var iWidth = oPopupContent.style.width.split("px")[0];
    var iHeight = oPopupContent.style.height.split("px")[0];
    var iLeftPos = oPopupContent.style.left.split("px")[0];
    var iTopPos = oPopupContent.style.top.split("px")[0];

    var oBrowser = getBrowserHeight();

    if (iLeftPos == "") {
        iLeftPos = 990 - iWidth;
    }

    if (iTopPos == "") {
        iTopPos = 700 - iHeight;
    }

    oPopupContent.style.left = iLeftPos;
    oPopupContent.style.top = iTopPos;
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

//This method is used to attach ready state change event while loading dashboard.
function loadingDashboard(title) {
    if (el('dbFrame') != null) {
        showLayer(title);
        if(document.attachEvent)
            el('dbFrame').attachEvent("onreadystatechange", attachDbLoader);
        else
            el('dbFrame').onload=attachDbLoader;
    }
}

//This method is used to detach ready state change event while loading dashboard.
function attachDbLoader() {
    if (el('dbFrame').readyState == 'complete') {
        if (typeof (dfs("dbFrame").initDashboard) != "undefined") {
            dfs('dbFrame').initDashboard();
        }
        hideLayer();
        if(document.detachEvent)
            el('dbFrame').detachEvent("onreadystatechange", attachDbLoader);
        else
            el('dbFrame').onload=null;
    }
}

//This method is used to attach ready state change event while loading status grid.
function loadingStatusGrid(title) {
    if (dfs("dbFrame").el('sgFrame') != null) {
        showLayer(title);
        if(document.attachEvent)
            dfs("dbFrame").el('sgFrame').attachEvent("onreadystatechange", attachStatusGridLoader);
        else
            dfs("dbFrame").el('sgFrame').onload=attachStatusGridLoader;
    }
}

//This method is used to detach ready state change event while loading status grid.
function attachStatusGridLoader() {
    if (pdf("dbFrame").el('sgFrame').readyState == 'complete') {
        if (typeof (pdf("dbFrame").dfs('sgFrame').document.Script.initStatusGrid) != "undefined") {
            pdf("dbFrame").dfs('sgFrame').document.Script.initStatusGrid();
        }
        hideLayer();
        if(document.detachEvent)
            pdf("dbFrame").el('sgFrame').detachEvent("onreadystatechange", attachStatusGridLoader);
        else
             pdf("dbFrame").el('sgFrame').onload=null;
    }
}

//This method is used to attach ready state change event while loading news grid.
function loadingNewsGrid(title) {
    if (dfs("dbFrame").el('newsFrame') != null) {
        showLayer(title);
        dfs("dbFrame").el('newsFrame').attachEvent("onreadystatechange", attachNewsGridLoader);
    }
}

//This method is used to detach ready state change event while loading news grid.
function attachNewsGridLoader() {
    if (pdf("dbFrame").el('newsFrame').readyState == 'complete') {       
        hideLayer();
        pdf("dbFrame").el('newsFrame').detachEvent("onreadystatechange", attachNewsGridLoader);
    }
}

//This method is used to attach ready state change event while loading dashboard list space.
function loadingDashboardListPane(title) {
    if (dfs("dbFrame").el('dlstFrame') != null) {
        showLayer(title);
        dfs("dbFrame").el('dlstFrame').attachEvent("onreadystatechange", attachDbListLoader);
    }
}

//This method is used to detach ready state change event while loading dashboard list space.
function attachDbListLoader() {
    if (pdf("dbFrame").el('dlstFrame').readyState == 'complete') {
        if (typeof (pdf("dbFrame").dfs('dlstFrame').document.Script.initDashboardListGrid) != "undefined") {
            pdf("dbFrame").dfs('dlstFrame').document.Script.initDashboardListGrid();
        }
        hideLayer();
        pdf("dbFrame").el('dlstFrame').detachEvent("onreadystatechange", attachDbListLoader);
    }
}

//This method is used to attach ready state change event while loading organization tree.
function loadingDashboardOrgTree(title) {
    if (pdf("dbFrame").el('orgFrame') != null) {
        showLayer(title);
        pdf("dbFrame").el('orgFrame').attachEvent("onreadystatechange", attachDbOrgTreeLoader);
    }
}

//This method is used to detach ready state change event while loading organization tree.
function attachDbOrgTreeLoader() {
    if (pdf("dbFrame").el("orgFrame").readyState == 'complete') {
        if (typeof (pdf("dbFrame").dfs("orgFrame").initOrganizationTree) != "undefined") {
            pdf("dbFrame").dfs("orgFrame").initOrganizationTree();
        }
        hideLayer();
        pdf("dbFrame").el("orgFrame").detachEvent("onreadystatechange", attachDbOrgTreeLoader);
    }
}

//This method is used to attach ready state change event while loading show all organization tree.
function loadingShowAllOrgTree(title) {
    if (pdf("dbFrame").df("orgFrame").el("orgshowallframe") != null) {
        showLayer(title);
        pdf("dbFrame").df("orgFrame").el("orgshowallframe").attachEvent("onreadystatechange", attachShowAllOrgTree);
    }
}

//This method is used to detach ready state change event while loading show all organization tree.
function attachShowAllOrgTree() {
    if (pdf("dbFrame").df("orgFrame").el("orgshowallframe").readyState == 'complete') {
        hideLayer();
        pdf("dbFrame").df("orgFrame").el("orgshowallframe").detachEvent("onreadystatechange", attachShowAllOrgTree);
    }
}

//This method is used to attach ready state change event while loading my pending organization tree.
function loadingMyPendingOrgTree(title) {
    if (pdf("dbFrame").df("orgFrame").el("orgmypendingframe") != null) {
        showLayer(title);
        pdf("dbFrame").df("orgFrame").el("orgmypendingframe").attachEvent("onreadystatechange", attachMyPendingOrgTree);
    }
}

//This method is used to detach ready state change event while loading my pending organization tree.
function attachMyPendingOrgTree() {
    if (pdf("dbFrame").df("orgFrame").el("orgmypendingframe").readyState == 'complete') {
        hideLayer();
        pdf("dbFrame").df("orgFrame").el("orgmypendingframe").detachEvent("onreadystatechange", attachMyPendingOrgTree);
    }
}

//This method is used to attach ready state change event while loading custom modal window.
function loadingModalFrame(title) {
    if (el('fmModalFrame') != null) {
        showLayer(title);
        if (document.attachEvent)
            el('fmModalFrame').attachEvent("onreadystatechange", attachModalFrameLoader);
        else
            el('fmModalFrame').onload = attachModalFrameLoader;
    }
}

//This method is used to attach ready state change event while loading custom modal window.
function loadingHiddenFrame(title) {
    if (el('fmModalFrame') != null) {
        showLayer(title);
        if (document.attachEvent)
            el('fmModalFrame').attachEvent("onreadystatechange", attachHiddenFrameLoader);
        else
            el('fmModalFrame').onload =attachHiddenFrameLoader;
    }
}

//This method is used to detach ready state change event while loading custom modal window.
function attachHiddenFrameLoader() {
    var rsStatus = (el('wfFrame' + CurrentDesk).readyState || el('wfFrame' + CurrentDesk).contentDocument.readyState || el('wfFrame' + CurrentDesk).contentWindow.readyState);
    if (rsStatus == 'complete') {
        hideLayer();
        if (typeof (dfs("wfFrame" + pdf("CurrentDesk")).initHiddenFrame) != "undefined") {
            dfs("wfFrame" + pdf("CurrentDesk")).initHiddenFrame(gsmodalWindowType);
        }
        if (document.detachEvent)
            el('fmModalFrame').detachEvent("onreadystatechange", attachHiddenFrameLoader);
        else
            el('fmModalFrame').onload = null;


    }
}

//This method is used to detach ready state change event while loading custom modal window.
function attachModalFrameLoader() {
    var rsStatus = (el('fmModalFrame').readyState || el('fmModalFrame').contentDocument.readyState || el('fmModalFrame').contentWindow.readyState);
    if (rsStatus == 'complete') {
        hideLoader();
        if (typeof (dfs("fmModalFrame").initPopup) != "undefined") {
            dfs("fmModalFrame").initPopup(gsmodalWindowType);
        }
        showDiv("modalframe");
        if (document.detachEvent)
            el('fmModalFrame').detachEvent("onreadystatechange", attachModalFrameLoader);
        else
            el('fmModalFrame').onload = null;
    }
}

//This method is used to attach ready state change event while loading list space.
function loadingListPane(title) {
    if (el('plFrame' + CurrentDesk) != null) {
        showLayer(title);
        if (document.attachEvent)
            el('plFrame' + CurrentDesk).attachEvent("onreadystatechange", attachPlLoader);
        else
            el('plFrame' + CurrentDesk).onload = attachPlLoader;
    }
}

//This method is used to detach ready state change event while loading list space.
function attachPlLoader() {
    var rsStatus = (el('plFrame' + CurrentDesk).readyState || el('plFrame' + CurrentDesk).contentDocument.readyState || el('plFrame' + CurrentDesk).contentWindow.readyState);
    if (rsStatus == 'complete') {
        hideLayer();
        if (lfs().showListGrid) {
            lfs().showListGrid();
        }
        if (document.detachEvent)
            el('plFrame' + CurrentDesk).detachEvent("onreadystatechange", attachPlLoader);
        else
            el('plFrame' + CurrentDesk).onload = null;
    }
}


//This method is used to attach ready state change event while loading custom list space.
function loadingCustomListPane() {
    if (el('clFrame' + CurrentDesk) != null) {
        if (document.attachEvent)
            el('clFrame' + CurrentDesk).attachEvent("onreadystatechange", attachClLoader);
        else
            el('clFrame' + CurrentDesk).onload = attachClLoader;
    }
}

//This method is used to detach ready state change event while loading custom list space.
function attachClLoader() {
    var rsStatus = (el('clFrame' + CurrentDesk).readyState || el('clFrame' + CurrentDesk).contentDocument.readyState || el('clFrame' + CurrentDesk).contentWindow.readyState);
    if (rsStatus == 'complete') {
        if (typeof (dfs("clFrame" + pdf("CurrentDesk")).initListPane) != "undefined") {
            dfs("clFrame" + pdf("CurrentDesk")).initListPane();
        }
        if (document.detachEvent)
            el('clFrame' + CurrentDesk).detachEvent("onreadystatechange", attachClLoader);
        else
            el('clFrame' + CurrentDesk).onload = null;
    }
}


//This method is used to attach ready state change event while loading report in workspace.
function loadingReportPane(title) {
    if (el('wfFrame' + CurrentDesk) != null) {
        showLayer(title);
        if (document.attachEvent)
            el('wfFrame' + CurrentDesk).attachEvent("onreadystatechange", attachReportPaneLoader);
        else
            el('wfFrame' + CurrentDesk).onload = attachReportPaneLoader;
    }
}

//This method is used to detach ready state change event while loading report in workspace.
function attachReportPaneLoader() {
    var rsStatus = (el('wfFrame' + CurrentDesk).readyState || el('wfFrame' + CurrentDesk).contentDocument.readyState || el('wfFrame' + CurrentDesk).contentWindow.readyState);
    if (rsStatus == 'complete' && gsStatus == "") {
        hideLayer();
        if (typeof (dfs("wfFrame" + pdf("CurrentDesk")).initReport) != "undefined") {
            dfs("wfFrame" + pdf("CurrentDesk")).initReport();
        }
        initGUI();
        if (document.detachEvent)
            el('wfFrame' + CurrentDesk).detachEvent("onreadystatechange", attachReportPaneLoader);
        else
            el('wfFrame' + CurrentDesk).onload = null;
    }
}


//This method is used to attach ready state change event while loading workspace.
function loadingWorkflowPane(title) {
    if (el('wfFrame' + CurrentDesk) != null) {
        showLayer(title);
        if (document.attachEvent)
            el('wfFrame' + CurrentDesk).attachEvent("onreadystatechange", attachWfLoader);
        else
            el('wfFrame' + CurrentDesk).onload = attachWfLoader;
    }
}

//This method is used to detach ready state change event while loading workspace.
function attachWfLoader() {
    var rsStatus = (el('wfFrame' + CurrentDesk).readyState || el('wfFrame' + CurrentDesk).contentDocument.readyState || el('wfFrame' + CurrentDesk).contentWindow.readyState);
    if (rsStatus == 'complete' && gsStatus == "") {
        hideLayer();
        setListAction();
        if (typeof (dfs("wfFrame" + pdf("CurrentDesk")).emptyData) != "undefined") {
            if (gsRequestType == "Callback") {
                dfs("wfFrame" + pdf("CurrentDesk")).emptyData(gsMode, gsWorkspaceURL, gsRequestType, "0");
            }
        }

        if (gsRequestType != "PostBack")
            initGUI();
        if (document.detachEvent)
            el('wfFrame' + CurrentDesk).detachEvent("onreadystatechange", attachWfLoader);
        else
            el('wfFrame' + CurrentDesk).onload = null;
    }
}

//This method is used to show layer while loading the page.
function showLayer(title) {
    setLayerPosition();
    setModalWindowTitle(title);
    var shadow;
    var question;
    if (el("shadow") == null) shadow = pel("shadow");
    else shadow = el("shadow");

    if (el("question") == null) question = pel("question");
    else question = el("question");

    shadow.style.display = "block";
    question.style.display = "block";
    el("imgloading").focus();
    if (document.attachEvent) {
        el("imgloading").attachEvent("onkeypress", disableKey);
        el("imgloading").attachEvent("onkeydown", disableKey);
    }
    else {
        el("imgloading").onkeypress = disableKey;
        el("imgloading").onkeydown = disableKey;
        el("imgloading").onkeyup = disableKey;
    }    
    el("imgloading").onclick = null;
    el("imgloading").onclick = new Function("return false;");
    shadow = null;
    question = null;
}

//This method is used to hide layer after loading the page.
function hideLayer() {
    var shadow;
    var question;
    if (el("shadow") == null) shadow = pel("shadow");
    else shadow = el("shadow");
    if (el("question") == null) question = pel("question");
    else question = el("question");
    shadow.style.display = "none";
    question.style.display = "none";
    shadow = null;
    question = null;    
    if (document.detachEvent) {
        pel("imgloading").detachEvent("onkeypress", disableKey);
        pel("imgloading").detachEvent("onkeydown", disableKey);
    }
    else {
        pel("imgloading").onkeypress = null;
        pel("imgloading").onkeydown = null;
        pel("imgloading").onkeyup = null;
    }    
}

//This method is used to hide loading message after loading the page.
function hideLoader() {
    var question;
    if (el("question") == null) question = pel("question");
    else question = el("question");
    question.style.display = "none";
    question = null;
    if (document.detachEvent) {
        pel("imgloading").detachEvent("onkeypress", disableKey);
        pel("imgloading").detachEvent("onkeydown", disableKey);
    }
    else {
        pel("imgloading").onkeypress = null;
        pel("imgloading").onkeydown = null;
        pel("imgloading").onkeyup = null;
    }
}


//This method is used to set the layer position for modal window
function setLayerPosition() {
    var shadow;
    var question;

    if (el("shadow") == null) shadow = pel("shadow");
    else shadow = el("shadow");

    if (el("question") == null) question = pel("question");
    else question = el("question");


    var bws = getBrowserHeight();
    shadow.style.width = bws.width + "px";
    shadow.style.height = bws.height + "px";

    question.style.left = parseInt((bws.width - 130) / 2);
    question.style.top = parseInt((bws.height-500) / 2);

    shadow = null;
    question = null;
}

//This method is used to set modal window title
function setModalWindowTitle(title) {
    if (typeof (title) == "undefined") title = "Loading..";
    el("tdmodalwindowtitle").innerHTML = title;
}

//This method used to disable key navigation in modal windows like "Information Message".
function disableKey(event) {
    if (!event)
        event = getEvent(event);
    if (event.keyCode != 13 && event.keyCode != 32) {
        event.returnValue = false;
        if (event.stopPropagation) {
            event.stopPropagation();
            event.preventDefault(true);
        }
    }
    if (event.keyCode == 27) {
        hideLayer();
    }
}

//This method used to attach ready state change event while loading email page from document print as well as from page
function loadingDocument(title) {
    showLayer(title);
    var sType = getQueryStringValue(document.location.href, "reporttype")
    if (sType == "Report") {
        el("reportpane").style.display = "block";
        el("divEmail").style.display = "block";
        if (document.attachEvent)
            dfs('fmModalFrame').dfs("fmReport").attachEvent("onreadystatechange", attachReportLoader);
        else
            dfs('fmModalFrame').dfs("fmReport").onload = attachReportLoader;
    }
    else if (sType == "Email") {
        el("reportpane").style.display = "none";
        el("divEmail").style.display = "none";
        if (document.attachEvent)
            dfs('fmModalFrame').dfs("fmReport").attachEvent("onreadystatechange", attachEmailLoader);
        else
            dfs('fmModalFrame').dfs("fmReport").onload = attachEmailLoader;
    }
}

//This method used to detach ready state change event while loading opening document for print
function attachReportLoader() {
    var rsStatus = (el('fmModalFrame').readyState || el('fmModalFrame').contentDocument.readyState || el('fmModalFrame').contentWindow.readyState);
    if (rsStatus == 'complete') {
        hideLayer();
        el("reportpane").style.display = "block";
        if (document.detachEvent)
            dfs('fmModalFrame').dfs("fmReport").detachEvent("onreadystatechange", attachReportLoader);
        else
            dfs('fmModalFrame').dfs("fmReport").onload = null;
    }
}

//This method used to detach ready state change event while loading email page from document print as well as from page
function attachEmailLoader() {
    var rsStatus = (el('fmModalFrame').readyState || el('fmModalFrame').contentDocument.readyState || el('fmModalFrame').contentWindow.readyState);
    if (rsStatus == 'complete') {
        hideLayer();
        createEmail();
        el("reportpane").style.display = "none";
        if (document.detachEvent)
            dfs('fmModalFrame').dfs("fmReport").detachEvent("onreadystatechange", attachEmailLoader);
        else
            dfs('fmModalFrame').dfs("fmReport").onload = null;
    }
}

//This method used to attach ready state change event while loading popup page
function loadingPopup(title) {
    if (el('wfFrame' + CurrentDesk) != null) {
        showLayer(title);
        if (document.attachEvent)
            el('wfFrame' + CurrentDesk).attachEvent("onreadystatechange", attachPopup);
        else
            el('wfFrame' + CurrentDesk).onload = attachPopup;
    }
}

//This method used to detach ready state change event while loading popup page
function attachPopup() {
    var rsStatus = (el('wfFrame' + CurrentDesk).readyState || el('wfFrame' + CurrentDesk).contentDocument.readyState || el('wfFrame' + CurrentDesk).contentWindow.readyState);
    if (rsStatus == 'complete') {
        if (document.Script) {
            if (typeof (document.Script.initPopupData) != "undefined")
                document.Script.initPopupData();
        }
        else {
            if (typeof (initPopupData) != "undefined")
                initPopupData();
        }
        hideLayer();
        if (document.detachEvent)
            el('wfFrame' + CurrentDesk).detachEvent("onreadystatechange", attachPopup);
        else
            el('wfFrame' + CurrentDesk).onload = null;
    }
}

//This method used to attach ready state change event while downloading file
function loadingDownloadFile(title) {
    if (df("wfFrame").el('fmDownloadFile') != null) {
        showLayer(title);
        if (document.attachEvent)
            df("wfFrame").el('fmDownloadFile').attachEvent("onreadystatechange", attachDownloadFile);
        else
            df("wfFrame").el('fmDownloadFile').onload = attachDownloadFile;
    }
}
//This method used to detach ready state change event while downloading file
function attachDownloadFile() {
    var rsStatus = (el('wfFrame' + CurrentDesk).el('fmDownloadFile').readyState || el('wfFrame' + CurrentDesk).el('fmDownloadFile').contentDocument.readyState || el('wfFrame' + CurrentDesk).el('fmDownloadFile').contentWindow.readyState);
    if (rsStatus == 'interactive') {
        hideLayer();
        if (document.detachEvent)
            df("wfFrame" + df("CurrentDesk")).el('fmDownloadFile').detachEvent("onreadystatechange", attachDownloadFile);
        else
            df("wfFrame" + df("CurrentDesk")).el('fmDownloadFile').onload = null;
    }
}

//This method used to attach ready state change event while downloading file
function loadingImportFile(title) {
    if (df("fmModalFrame").el('frameImportFile') != null) {
        showLayer(title);
        if (document.attachEvent)
            df("fmModalFrame").el('frameImportFile').attachEvent("onreadystatechange", attachImportFile);
        else
            df("fmModalFrame").el('frameImportFile').onload = attachImportFile;
    }
}
//This method used to detach ready state change event while downloading file
function attachImportFile() {
    var rsStatus = (el('fmModalFrame').el('frameImportFile').readyState || el('fmModalFrame').el('frameImportFile').contentDocument.readyState || el('fmModalFrame').el('frameImportFile').contentWindow.readyState);
    if (rsStatus == 'interactive') {
        hideLayer();
        if (document.detachEvent)
            df("fmModalFrame").el('frameImportFile').detachEvent("onreadystatechange", attachImportFile);
        else
            df("fmModalFrame").el('frameImportFile').onload = null;
        if (typeof (df("fmModalFrame").onAfterImport) != "undefined")
            df("fmModalFrame").onAfterImport();
    }
}

//This method used to attach ready state change event while import equipment file
function loadingEquipmentImportFile(title) {
    if (df("fmModalFrame").el('frameImportFile') != null) {
        if (document.attachEvent)
            df("fmModalFrame").el('frameImportFile').attachEvent("onreadystatechange", attachEquipmentImportFile);
        else
            df("fmModalFrame").el('frameImportFile').onload = attachEquipmentImportFile;
    }
}
//This method used to detach ready state change event while import equipment file
function attachEquipmentImportFile() {
    var rsStatus = (el('fmModalFrame').el('frameImportFile').readyState || el('fmModalFrame').el('frameImportFile').contentDocument.readyState || el('fmModalFrame').el('frameImportFile').contentWindow.readyState);
    if (rsStatus == 'interactive') {
        df("fmModalFrame").el('frameImportFile').detachEvent("onreadystatechange", attachEquipmentImportFile);
        if (typeof (df("fmModalFrame").onAfterImport) != "undefined")
            df("fmModalFrame").onAfterImport();
    }
}
