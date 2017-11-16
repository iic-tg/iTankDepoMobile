var action;
var MODE_NEW = "new";
var MODE_EDIT = "edit";
var MODE_VIEW = "view";
var RC = "RC";
var SURVEYOR = "SV";
var AO = "AO";
var RO = "RO";
var HO = "HO";
var EMERGENCY = "EMERGENCY";
var OFFHIRE = "OFF HIRE";
var previousScroll = 0;
var currentScroll;
var helpTip;
opera = /(opera)(?:.*version)?[ \/]([\w.]+)/i.test(navigator.userAgent);
msie = /(msie) ([\w.]+)/i.test(navigator.userAgent);
Firefox = /(Firefox)(?:.*? rv:([\w.]+))?/i.test(navigator.userAgent);
safari = ((/Safari/i.test(navigator.userAgent)) && !(/Chrome/i.test(navigator.userAgent)));
chrome = /Chrome/i.test(navigator.userAgent);

if (document.addEventListener) {
    document.addEventListener("onkeypress", preOnKeyPress, false);
    document.addEventListener("onkeydown", preOnKeyDown, false);
    document.addEventListener("onkeyup", preOnKeyUp, false);
}
else {
    document.attachEvent("onkeypress", preOnKeyPress);
    document.attachEvent("onkeydown", preOnKeyDown);
    document.attachEvent("onkeyup", preOnKeyUp);
}
//document.oncontextmenu = fContext;
window.history.go(+1)
var ctrl = false;
////This method is used to block any keys done it here
function preOnKeyDown(event) {
    if (!event)
        event = getEvent(event);
    disableCtrlKey(event);
    if (event.keyCode == 0)
        return false;

    //    if (event.keyCode == "9") {
    //        var o = event.srcElement;
    //        if (o.id == "lkpMaterialCode") {
    //            alert(el("lkpMaterialCode").tabIndex + "|" + el("txtLength").tabIndex + "|" + el("lkpUnitCode").tabIndex);
    //        }
    //    }
    switch (event.keyCode) {
        case 112: // F1
            {
                event.keyCode = 0;
                event.returnValue = false;
            }
        case 113: // F2
        case 114: // F3
        case 115: // F4
        case 116: // F5
        case 117: // F6
        case 121: // F10
        case 122, 27: // F11, Esc
            {
                if (action == "ModalWindow") {
                    ppsc().closeModalWindow();
                }
                else {
                    if (event.preventDefault) {
                        event.preventDefault(true);
                        event.stopPropagation();
                    }
                    else {
                        event.keyCode = 0;
                        event.returnValue = false;
                    }
                }
            }
    }

    if (event.altKey && event.keyCode == 37) {
        alert('The key you are trying to use is invalid in this context.');
        if (event.preventDefault) {
            event.preventDefault(true);
            event.stopPropagation();
        }
        else {
            event.keyCode = 0;
            event.returnValue = false;
            event.cancelBubble = true;
        }
        return false;
    }

    if (event.altKey && event.keyCode == 115 || event.ctrlKey && event.keyCode == 115) {
        alert('The key you are trying to use is invalid in this context.');
        if (event.preventDefault) {
            event.preventDefault(true);
            event.stopPropagation();
        }
        else {
            event.keyCode = 0;
            event.returnValue = false;
            event.cancelBubble = true;
        }
        return false;
    }

    //To block the Refresh, New Window, Zoom
    if (event.ctrlKey == true && (event.keyCode == 78 || event.keyCode == 82 || event.keyCode == 107 || event.keyCode == 109)) {
        return;
    }

    //Backspace is only allowed in textarea and input text and password
    if (event.keyCode == 8) {
        var obj = event.srcElement || event.target;
        if (obj.type == "text" || obj.type == "textarea" || obj.type == "password" || obj.tagName == "DIV") {
            if (obj.readOnly == true)
                return false;
            else
                return true;
        }
        return false;
    }

    if (event.keyCode == 13 && action == "ModalWindow") {
        if (event.preventDefault) {
            event.preventDefault(true);
            event.stopPropagation();
        }
        else {
            event.keyCode = 0;
            event.returnValue = false;
            event.cancelBubble = true;
        }
        return false;
    }

}

//This method is used for checking the page location to prevent improper access to page
function checkPageLocation() {
    if (window.top.location.href.toUpperCase().indexOf('POPUP.ASPX') == -1 &&
            window.top.location.href.toUpperCase().indexOf('HOME.ASPX') == -1)
        window.top.location.href = getBaseURL() + "alerts.aspx?se=1";
}

//This method is used get base URL
function getBaseURL() {
    var url = location.href;
    var pathname = location.pathname;
    var index1 = url.indexOf(pathname);
    var index2 = url.indexOf("/", index1 + 1);
    var baseLocalUrl = url.substr(0, index2);
    return baseLocalUrl + "/";
}

function preOnKeyUp(event) {
    if (!event)
        event = getEvent(event);
    if (event.keyCode == 17) {
        ctrl = false;
    }
    disableCtrlKey(event);
}

function preOnKeyPress(event) {
    if (!event)
        event = getEvent(event);
    disableCtrlKey(event);
}

function disableCtrlKey(event) {
    if (!event)
        event = getEvent(event);
    //list all CTRL + key combinations you want to disable
    var forbiddenKeys = new Array('+', '-', 'n');
    var key;
    var isCtrl;
    if (window.event) {
        key = window.event.keyCode;     //IE
        if (window.event.ctrlKey)
            isCtrl = true;
        else
            isCtrl = false;
    }
    else {
        key = event.which;     //firefox
        if (event.ctrlKey)
            isCtrl = true;
        else
            isCtrl = false;
    }
    //if ctrl is pressed check if other key is in forbidenKeys array
    if (isCtrl) {
        for (i = 0; i < forbiddenKeys.length; i++) {
            //case-insensitive comparation
            if (forbiddenKeys[i].toLowerCase() == String.fromCharCode(key).toLowerCase()) {
                return false;
            }
        }
    }
    return true;
}

//var obj = document.body;
//if (obj != null) {
//    if (obj.addEventListener) {
//        obj.addEventListener('DOMMouseScroll', mouseWheel, false);
//        obj.addEventListener("mousewheel", mouseWheel, false);
//    }
//    else obj.onmousewheel = mouseWheel;
//}

document.onmousedown = disableclick;
function disableclick(e) {
    e = e ? e : window.event;
    if (e.button == 4) {
        return false;
    }
}
function mouseWheel(e) {
    e = e ? e : window.event;
    if (e.ctrlKey) {
        if (e.preventDefault) e.preventDefault();
        else e.returnValue = false;
        return false;
    }
}

//For disabling right click
function fContext(event) {
    if (!event)
        event = getEvent(event);
    var obj = event.srcElement || event.target;
    if (obj.type == "text" || obj.type == "textarea") {
        obj.IsCheckedOnKd = true;
        return true;
    }
    else
        return false;
}

//This method is used to get element by id
function el(elementid) {
    if (document.getElementById) {
        return document.getElementById(elementid);
    } else if (window[elementid]) {
        return window[elementid];
    }
}

//This method is used to get parent element by id
function pel(elementid) {
    if (parent.document.getElementById) {
        return parent.document.getElementById(elementid);
    } else if (window[elementid]) {
        return window[elementid];
    }
}

//This method is used to get parent of parent element by id
function ppel(elementid) {
    if (parent.parent.document.getElementById) {
        return parent.parent.document.getElementById(elementid);
    } else if (window[elementid]) {
        return window[elementid];
    }
}


//This method is used to get parent workflow element from modal frame by id
function pwel(elementid) {
    if (pdf('wfFrame' + pdf('CurrentDesk'))) {
        return pdf('wfFrame' + pdf('CurrentDesk')).el(elementid);
    }
}

//This method is used to execute document script
function psc() {
    if (parent.document.Script)
        return parent.document.Script;
    else
        return window.parent;
}


//This method is used to execute parent of parent document script
function ppsc() {
    if (parent.document.Script)
        return parent.parent.document.Script;
    else
        return window.parent;
}

//This method is used to execute parent of parent of parent document script
function pppsc() {
    if (parent.document.Script)
        return parent.parent.parent.document.Script;
    else
        return window.parent;
}


//This method is used to get frame script by frame id
function dfs(sFrameID) {
    if (document.frames && !opera)
        return document.frames[sFrameID].document.Script;
    // return parent.document.frames[sFrameID];
    else
        return window.frames[sFrameID];
}

//This method is used to get workflow frame script
function wfs() {
    return dfs('wfFrame' + pdf('CurrentDesk'));
}

//This method is used to get workflow frame script
function lfs() {
    return dfs('plFrame' + pdf('CurrentDesk'));
}

//This method is used to get modal window frame script
function mfs() {
    return dfs('fmModalFrame');
}

function pmfs() {
    return pdfs('fmModalFrame');
}

//This method is used to get parent workflow frame script
function pwfs() {
    return pdfs('wfFrame' + pdf('CurrentDesk'));
}


//This method is used to get parent document frame script by frame id
function ppdf(sFrameID) {
    if (document.frames && !opera)
        return parent.parent.document.frames[sFrameID];
    else
        return window.parent.frames[sFrameID];
}


//This method is used to get parent document frame script by frame id
function pppdf(sFrameID) {
    if (document.frames && !opera)
        return parent.parent.parent.document.frames[sFrameID];
    else
        return window.parent.frames[sFrameID];
}

//This method is used to get parent document frame script by frame id
function pdf(sFrameID) {
    if (document.frames && !opera)
        return parent.document.frames[sFrameID];
    else
        return window.parent.frames[sFrameID];
}

//This method is used to get parent document frame script by frame id
function df(sFrameID) {
    if (document.frames && !opera)
        return document.frames[sFrameID];
    else
        return window.frames[sFrameID];
}

//This method is used to get parent document frame script by frame id
function pdfs(sFrameID) {
    if (document.frames && !opera)
        return parent.document.frames[sFrameID].document.Script;
    else
        return window.parent.frames[sFrameID];
}

//This method is used to get parent of parent document frame script by frame id
function ppdfs(sFrameID) {
    if (document.frames && !opera)
        return parent.parent.document.frames[sFrameID].document.Script;
    else
        return window.parent.frames[sFrameID];
}


//This method is used to get workflow frame document
function GetWFFrameDoc() {
    if (el("wfFrame" + pdf("CurrentDesk")))
        return el("wfFrame" + pdf("CurrentDesk")).contentWindow.document;
    else if (pel("wfFrame" + pdf("CurrentDesk")))
        return pel("wfFrame" + pdf("CurrentDesk")).contentWindow.document;
    else
        return null;
}

//This method is used to get pending frame document
function GetPLFrameDoc() {
    if (el("plFrame" + pdf("CurrentDesk")))
        return el("plFrame" + pdf("CurrentDesk")).contentWindow.document;
    else if (pel("plFrame" + pdf("CurrentDesk")))
        return pel("plFrame" + pdf("CurrentDesk")).contentWindow.document;
    else
        return null;
}

//This method is used to hide DIV element
function hideDiv(sElementID) {
    el(sElementID).style.display = "none";
}

//This method is used to show DIV element
function showDiv(sElementID) {
    if (el(sElementID)) {
        if (el(sElementID).tagName == "BUTTON" || el(sElementID).tagName == "I") {
            el(sElementID).style.display = "inline";
        }
        else {
            el(sElementID).style.display = "block";

        }
    }
}

//This method is used to get querystring value
function getQStr(sKey) {
    var sURL = document.location.href;
    if (sURL.indexOf("?") != -1)
        sURL = decodeURIComponent(sURL.split("?")[1]);
    var sQuery = sURL.toString();
    var sPairs = sQuery.split("&");
    for (var i = 0; i < sPairs.length; i++) {
        var iPos = sPairs[i].indexOf('=');
        if (iPos >= 0) {
            if (sPairs[i].substring(0, iPos).toLowerCase() == sKey.toLowerCase())
                return sPairs[i].substring(iPos + 1);
        }
    }
    return "";
}

//This method is used to get querystring value
function getQueryStringValue(sURL, sKey) {
    if (sURL.indexOf("?") != -1)
        sURL = decodeURIComponent(sURL.split("?")[1]);
    var sQuery = sURL.toString();
    var sPairs = sQuery.split("&");
    for (var i = 0; i < sPairs.length; i++) {
        var iPos = sPairs[i].indexOf('=');
        if (iPos >= 0) {
            if (sPairs[i].substring(0, iPos).toLowerCase() == sKey.toLowerCase())
                return sPairs[i].substring(iPos + 1);
        }
    }
    return "";
}


//This method used to get help strip message from helptip array in helptip.js file
function getHelpMessage(iHelpID, bReadOnly) {
    if (bReadOnly)
        return "";
    else
        return HelpMessages[iHelpID];
}

//This method used to show help strip
//function showHelpStrip(sHelpText, bReadOnly, oControl) {

//    if (helpTip)
//        helpTip.tooltip('close');
//    var sHelpMessage;
//    sHelpMessage = getHelpMessage(sHelpText, bReadOnly);
//    if (sHelpMessage != '') {
//        oControl.title = sHelpMessage;
//        helpTip = $(oControl).tooltip({
//            'content': sHelpMessage,
//            //'position': { my: 'center top', at: 'center bottom+10' },
//            'unique': true,
//            'disabled': true,
//            'track': true,
//            'open': function (e) {
//                if (!ppsc().gblnHelpTip) {
//                    setTimeout(function () {
//                        $(e.target).tooltip('close');
//                    }, 10);
//                }
//            }
//        }).on("focusin", function () {
//            $(this).tooltip("enable").tooltip("open");
//        }).on("focusout", function () {
//            $(this).tooltip("close").tooltip("disable");
//        });
//        helpTip.tooltip('open');        
//    }

//}

//This method used to show help strip
function showCustomHelpStrip(sHelpText, bReadOnly) {
    if (pel('helptip') != null) {
        var sHelpMessage;
        sHelpMessage = sHelpText;
        if (sHelpMessage != '') {
            pel('helptip').style.display = "block";
            pel('helptipcontent').innerHTML = sHelpMessage;
        }
    }
}

//This method used to hide help strip
function hideHelpStrip() {
    if (helpTip) {
        $(document).tooltip('close');
        $(document).tooltip({ disabled: true });
    }
}

//This method used to show help strip
function showHelpStrip() {
    //    loadInputControls();
    helpTip = $(document).tooltip({ disabled: false });
    helpTip = $(document).tooltip({ track: true });
}


//This method executes on focus event in each and every control
function Control_onFocus(sMode, iHelpTextID, oControl) {
    var bReadOnly = event.srcElement.readOnly;
    var iHelpTextIDs = iHelpTextID.split(",")
    iHelpTextID = iHelpTextIDs[0];
    var iMaxLength = iHelpTextIDs[1];

    if (bReadOnly == false && typeof (iMaxLength) != "undefined") {
        var maxLengthDetails = getMaximumLength(iMaxLength, bReadOnly);
        var datatype = (maxLengthDetails != "" && maxLengthDetails.split(",").length > 2) ? maxLengthDetails.split(",")[3].split(':')[1] : "";
        switch (datatype.toLowerCase()) {
            case 'datetime', 'date':
                oControl.maxLength = 11;
                break;
            case 'decimal':
            case 'numeric':
                oControl.maxLength = parseInt(maxLengthDetails.split(",")[4].split(':')[1]) + parseInt(maxLengthDetails.split(",")[5].split(':')[1]) + 1
                break;
            default:
                oControl.maxLength = (maxLengthDetails != "" && maxLengthDetails.split(",").length >= 2) ? maxLengthDetails.split(",")[2].split(':')[1] : "";
                break;
        }


    }
    //    if (sMode == 'show' && iHelpTextID != '') {
    //        if (parseInt(iHelpTextID)) {
    //            showHelpStrip(iHelpTextID, bReadOnly, oControl)
    //        }
    //        else {
    //            showCustomHelpStrip(iHelpTextID, bReadOnly)
    //        }

    //    }
    //    else
    //        hideHelpStrip()
    //    if (!(oControl.type.toUpperCase() == "TEXT" || oControl.tagName.toUpperCase() == "TEXTAREA" || oControl.type.toUpperCase() == "PASSWORD")) {
    //        if (helpTip)
    //            helpTip.tooltip('close');
    //    }
}

//This method is used to get Maximum length details
function getMaximumLength(iHelpText) {
    var columnlist = TableColumnList.toString().split(iHelpText);
    var position = columnlist[0].split(',').length - 1;
    return MaxLength[position];
}

//This method is used to show information message in status bar
function showInfoMessage(sMessage) {
    ppsc().showInfoMessagePane(sMessage);
}

//This method is used to show important
function showImportantMessage(sMessage) {
    ppsc().showImportantPane(sMessage);
}

//This method is used to show error message in status bar
function showErrorMessage(sMessage) {
    ppsc().showErrorMessagePane(sMessage);
}
function showErrorMessageWithReport(sMessage) {
    ppsc().showErrorMessagePaneWithReport(sMessage);
}

//This method is used to show confirm message in status bar
function showConfirmMessage(sMessage, yesMethod, noMethod, onAfterCancelMethod) {
    ppsc().showConfirmMessagePane(sMessage, yesMethod, noMethod, onAfterCancelMethod);
}


//This method is used to hide error/warning/information message in status bar
function hideMessage() {
    //if (ppsc().gsAction != "Submit" && !Page_IsValid) {
    //    if (typeof (ppsc().hideMessagePane) != "undefined") {
    //        ppsc().hideMessagePane();
    //    }
    //}
}


//This method is used to show warning message in status bar
function showWarningMessage(sMessage) {
    ppsc().showWarningMessagePane(sMessage);
}

//This method is used to show cutom modal window in all pages
function showModalWindow(title, url, vWidth, vHeight, vTop, vLeft, onAfterClose, modalWindowType, onBeforeClose) {
    if (typeof (onBeforeClose) == "undefined")
        onBeforeClose = "";
    if (typeof (onAfterClose) == "undefined")
        onAfterClose = "";
    if (typeof (modalWindowType) == "undefined")
        modalWindowType = title;
    ppsc().ShowModalWindow(title, url, vWidth, vHeight, vTop, vLeft, onAfterClose, modalWindowType, onBeforeClose);
}

//This method is used to show cutom modal window in all pages
function showHiddenWindow(title, url, vWidth, vHeight, vTop, vLeft, onAfterClose, modalWindowType, onBeforeClose) {
    if (typeof (onBeforeClose) == "undefined")
        onBeforeClose = "";
    if (typeof (onAfterClose) == "undefined")
        onAfterClose = "";
    if (typeof (modalWindowType) == "undefined")
        modalWindowType = title;
    ppsc().ShowHiddenWindow(title, url, vWidth, vHeight, vTop, vLeft, onAfterClose, modalWindowType, onBeforeClose);
}

//This method is used to show cutom modal window in all pages
function showPopupWindow(title, url, vWidth, vHeight, vTop, vLeft, _args) {
    var _parameters;
    _parameters = 'width=' + vWidth + ',height=' + vHeight;
    _parameters = _parameters + ',toolbars=no,scrollbars=no,status=no,location=no,resizable=no,minimize=no,maximise=no'

    vWidth = vWidth.replace('px', '');
    if (vWidth > 1000) {
        vWidth = 1000;
    }
    vHeight = vHeight.replace('px', '');
    var ScreenX = (1000 - vWidth) / 2;
    var ScreenY = (700 - vHeight) / 2;

    _parameters = _parameters + ',left=' + ScreenX + ',top=' + ScreenY;

    var rel = getQueryStringValue(url, "rel");
    var _url;

    if (rel == "1") {
        _url = "Popup.aspx?wt=" + parent.window.document.title + "&title=" + title + "&apu=" + url;
    }
    else if (rel == "0" || rel == "") {
        _url = "../Popup.aspx?wt=" + parent.window.document.title + "&title=" + title + "&apu=" + url;
    }

    var d = new Date();
    var _result;
    _url = _url + "&fm=true"
    _result = window.open(_url, d.getTime(), _parameters);
    return _result;
}


// show modal popup window
function ShowModalPopupWindow(_url, _popupWidth, _popupHeight, _messageDetails) {
    var _parameters;
    var ScreenX = window.screenLeft + 200;
    var ScreenY = window.screenTop + 100;

    _parameters = 'dialogWidth:' + _popupWidth + ';dialogHeight:' + _popupHeight;
    _parameters = _parameters + ';status:no;help: No;resizable: No'
    _parameters = _parameters + ';dialogLeft:' + ScreenX + ';dialogTop:' + ScreenY;

    if (window.showModalDialog) {
        _returnvaluepopup = window.showModalDialog(_url, _messageDetails, _parameters);
        return _returnvaluepopup;
    }
    else {
        _url = _url + "&fm=true"
        _returnvaluepopup = window.open(_url, _messageDetails, _parameters);
        return _returnvaluepopup;
    }
}

//This method is used to modal window return method call
function reloadModalWindow(bStatus) {
    ppsc().reloadModalWindow(bStatus);
}

//This method used to execute load data in workspace page
function loadData(sPageurl, sQryStr, sMode, sPageType, iItemno) {
    var _framedoc = GetWFFrameDoc();
    if (_framedoc == null)
        return;
    if (sPageType.toString().toLowerCase() == "callback") {
        if (_framedoc.Script)
            _framedoc.Script.loadPageData(_framedoc.URLUnencoded, sQryStr, sMode, iItemno);
        else
            getIFrameObj("wfFrame" + pdf("CurrentDesk")).loadPageData(decodeURIComponent(_framedoc.URL), sQryStr, sMode, iItemno);
        return true;
    }
    //    else {
    //        _framedoc.location.href = sPageurl + addTimeStamp(sQryStr);
    //    }
}

//This method used to execute empty data in workspace page
function emptyData(_vmode, _vurl, sPageType, iItemno) {
    try {
        var _framedoc = GetWFFrameDoc();

        var _url = _vurl;
        var _Qstr = _vurl.split("?")[1];
        var _vrurl = _vurl.split("?")[0];
        var sMode = getQueryStringValue(_vurl, "mode");
        var iActivityID = getQueryStringValue(_vurl, "activityid");

        if (sPageType.toString().toLowerCase() == "callback") {
            if (_framedoc.Script)
                _framedoc.Script.loadPageData(_framedoc.URLUnencoded, _Qstr, _vmode, iItemno);
            else
                getIFrameObj("wfFrame" + pdf("CurrentDesk")).loadPageData(decodeURIComponent(_framedoc.URL), _Qstr, _vmode, iItemno);
        }
        else {
            var d = new Date();
            pel("wfFrame" + pdf("CurrentDesk")).src = _vrurl + "?activityid=" + iActivityID + "&mode=" + _vmode + "&pagetype=" + sPageType + "&itemno=" + iItemno + "&ts=" + d.getTime();
        }
        if (parent.document.Script)
            parent.document.Script.focuscontroltype = "";
        else
            window.parent.focuscontroltype = "";
        $('.btncorner').corner();

    } catch (e) { }
}


function loadPageData(sPageurl, sQryStr, sMode, iItemno) {
    DBC();
    var _backToList = "";
    var _qryType = "";
    var _backToListAdd = "";
    _backToList = getQueryStringValue(psc().gsURL, "backToList");
    _qryType = getQueryStringValue(psc().gsURL, "qrytype");
    _backToListAdd = getQueryStringValue(sQryStr, "backToListAdd");
    if (_qryType == "newact" || _backToListAdd == "true") {
    //if (_qryType == "newact" || sMode == "new") { //Changed for backtolist Getdata load
        _backToList = "false";
    }
    
    if (_backToList != "true" && _backToList != "") {
        var oCallback = new Callback();
        oCallback.add("mode", sMode);
        // oCallback.add("backToList", _backToList);
        sPageurl = sPageurl + "&itemno=" + iItemno;
        oCallback.invoke(addTimeStamp(sPageurl), "fnGetData");
        if (oCallback.getCallbackStatus()) {
            //This function shall be written in Page Level
            if (typeof (GetLookupChanges) == "function")
                GetLookupChanges(true);

            eval(decodeURIComponent(oCallback.getReturnValue("Message")));
            (decodeURIComponent(oCallback.getReturnValue("Message")));
            if (typeof (setPageMode) != "undefined")
                setPageMode(sMode);
        }

        oCallback = null;
    }
    //This method used to call page intialization for all pages
    if (typeof (pwfs().initPage) != "undefined") {
        initPage(sMode);
    }
    if (ppsc().gblnHelpTip) {
        showHelpStrip();
    }
    $('.btncorner').corner();

}

//function height() {
//    if (document.all)
//        alert('Document offsetHeight = ' + document.body.offsetHeight);
//    else if (document.layers)
//        alert('Document height = ' + document.body.document.height);
//}

//This method is used to add time stamp in URL
function addTimeStamp(sURL) {
    if (sURL != false) {
        if (sURL.indexOf("?") != -1) {
            sURL = sURL + "&";
        }
        else {
            sURL = sURL + "?";
        }
        var d = new Date();
        sURL = sURL + "tm=" + d.getTime();
        sURL = sURL + "&Load=True";
        return sURL;
    }
}

//This method is used to clear itextbox ,idate controls 
function clearTextValues(sControlID) {
    var oControl = el(sControlID);
    oControl.value = "";
    oControl.IsCheckedOnKd = true;
}

//This method is used to clear All itextbox ,idate controls. All the Control's ID has to be passed as a array
function clearAllTextValues(sControlIDs) {
    sControlIDs.forEach(function (ID) {
        clearTextValues(ID);
    });
}

//This method is used to clear ilookup control
function clearLookupValues(sLookupID) {
    var aLookupValue = new Array();
    aLookupValue[0] = '';
    aLookupValue[1] = '';
    el(sLookupID).value = "";
    setLkpContrlValue(sLookupID, aLookupValue);
    el(sLookupID).IsCheckedOnKd = true;
}

//This method is used to clear All ilookup control, All the Control's ID has to be passed as a array
function clearAllTLookupValues(sControlIDs) {
    sControlIDs.forEach(function (ID) {
        clearLookupValues(ID);
    });
}

//This method is used to reset all the validators during on callback
function resetValidators(bol) {
    resetValidatorByGroup(null, bol)
}

//This method is used to reset all the validators during on callback by group
function resetValidatorByGroup(sValidationGroup, bol) {
    if (typeof (GetLookupChanges) == "function" && bol == null)
        GetLookupChanges(true);
    ResetValidators(sValidationGroup)
}

//This method is used to set lookup control value
function setLkpContrlValue(iLookupID, arrObj, index) {
    if (index == null)
        index = 1
    if (arrObj[index] != null)
        SetLookupValue(iLookupID, arrObj[index], arrObj);

    //el(iLookupID).SelectedValues = arrObj;
}

function setLookupTableName(lkpObj, tblName) {
    if (lkpObj && tblName) {
        setIAttribute(obj, "tblName", tblName);
    }    
}

//This method is used to set text control value
function setTxtControlValue(cntrlid, value) {
    el(cntrlid).value = value;
    el(cntrlid).IsCheckedOnKd = true;
}


//This method is used to make read only 
function setReadOnly(sControlID, bReadOnly) {
    var obj = el(sControlID);
    /*if (event != null && event.srcElement != null && event.srcElement.id == sControlID) {
    GetLookupChanges();
    }*/
    obj.readOnly = bReadOnly;
    if (bReadOnly) {
        obj.title = "";
        if (obj.tabIndex != "-1") {
            obj.setAttribute("oldtabIndex", obj.tabIndex);
            obj.tabIndex = -1;
        }

        switch (obj.className) {
            case "lkp":
                obj.className = "lkpd";
                break;
            case "txt":
                obj.className = "txtd";
                break;
            case "ntxt":
                obj.className = "ntxtd";
                break;
            case "dat":
                obj.className = "datd";
                break;
        }
    }
    else {
        if (ppsc().gblnHelpTip) {
            var helpid = (getIAttribute(obj, "hT")).split(",");
            var sHelpMessage;
            if (helpid.length >= 0) {
                sHelpMessage = getHelpMessage(helpid[0], obj.readOnly);
            }
            obj.title = sHelpMessage;
        }
        //else {
        //    obj.title = "";
        //}
        if (typeof (obj.oldtabIndex) != "undefined" && obj.oldtabIndex != "") {
            obj.tabIndex = obj.oldtabIndex;
            obj.setAttribute("oldtabIndex", "");
        }
        else if (obj.getAttribute("oldtabIndex") != null) {//Added for  UIG Tab Issue
            obj.tabIndex = obj.getAttribute("oldtabIndex");
            obj.setAttribute("oldtabIndex", "");
        }
        switch (obj.className) {
            case "lkpd":
                obj.className = "lkp";
                break;
            case "txtd":
                obj.className = "txt";
                break;
            case "ntxtd":
                obj.className = "ntxt";
                break;
            case "datd":
                obj.className = "dat";
                break;
        }
    }
}

//This method is used to set focus to action pane or submit pane control
function setActionButtonFocus(_PrevCur, _NextCur) {
    var vName1;
    if (Page_IsValid) {
        vName1 = "placeCursor('" + _PrevCur + "','" + _NextCur + "');"
        if (document.addEventListener)
            el('btnSubmit').addEventListener("onkeydown", new Function(vName1), false);
        else
            el('btnSubmit').attachEvent("onkeydown", new Function(vName1));
    }
}

//This method is used to set focus to custom focus field
function setCustomFocus(_PrevCur, _NextCur) {
    var vName1;
    vName1 = "placeCursor('" + _PrevCur + "','" + _NextCur + "');"
    if (document.addEventListener) {
        el(_NextCur).addEventListener("onkeypress", new Function(vName1), false);
        el(_NextCur).addEventListener("onkeydown", new Function(vName1), false);
    }
    else {
        el(_NextCur).attachEvent("onkeypress", new Function(vName1));
        el(_NextCur).attachEvent("onkeydown", new Function(vName1));
    }
}

//This method used to placing the cursor static in first control
function placeCursor(_before, _after) {
    if (Page_IsValid) {
        if (event.shiftKey == true && event.keyCode == 9) {
            event.keyCode = 0;
            event.returnValue = false;
            if (el(_after))
                try { el(_after).focus(); } catch (ex) { };
        }
        else if (event.shiftKey == false && event.keyCode == 9) {
            event.keyCode = 0;
            event.returnValue = false;
            if (el(_before))
                try { el(_before).focus(); } catch (ex) { };
        }
    }
}


//This method used to change the lookup table name dynamically
function setLkpTableName(lkpid, tblname) {
    el(lkpid).tblName = tblname;
}

//This method is used to get page changes
function getPageChanges() {
    if (getQueryStringValue(document.location.href, 'mode') == 'view') {
        return false;
    }

    if (typeof (HasChanges) != "undefined" && HasChanges || typeof (HasRCChanges) != "undefined" && HasRCChanges)
        return true;
    else if (typeof (vrGridIds) != "undefined") {
        for (var vrGridId in vrGridIds) {
            var _actgridid = vrGridIds[vrGridId].split(";")[1];

            if (checkGridHasChanges(_actgridid)) {
                return true;
            }
        }

        for (var vrGridId in vrGridIds) {
            var _actgridid = vrGridIds[vrGridId].split(";")[1];
            var _grid_obj = el(_actgridid);

            if (typeof (_grid_obj) != "undefined" && _grid_obj != null) {
                _grid_obj.Submit();
            }

            if (checkGridHasChanges(_actgridid)) {
                return true;
            }
        }

    }
    return false;
}

//This method is used to check grid has changes
function checkGridHasChanges(_grid_id) {
    _grid_obj = el(_grid_id);

    if (typeof (_grid_obj) == "undefined" || _grid_obj == null) {
        return false;
    }
    else {
        return _grid_obj.hasChanges;
    }
}

//This method is used to reset changes in grid
function resetHasChanges(_grid_id) {
    _grid_obj = el(_grid_id);

    if (typeof (_grid_obj) == "undefined" || _grid_obj == null) {

    }
    else {
        _grid_obj.hasChanges = false;
    }
}

//This method is used to set has changes method
function setHasChanges() {
    if (!HasChanges)
        HasChanges = true;
}

//This method is used to setfocus to the field
function setFocusToField(_fieldid) {
    try {
        el(_fieldid).focus();
        var event = getEvent(event);
        if (event) {
            event.returnValue = false;
            event.cancelBubble = true;
        }
        return false;
    }
    catch (e) { }
}

//This method is used to set focus in checkbox
function setCBHasChanges() {
    if (!HasChanges)
        HasChanges = true;
}

function getPageAttribute(oKey) {
    return pdfs("plFrame" + pdf("CurrentDesk")).getListRowData(oKey);
}

var a = new Array(10, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 34, 35, 36, 37, 38);
var HasChanges = false;

// This method is used for Equipment No CheckDigit and Format  Validations
function validateEquipmentNo(oSrc, args) {
    var str = trimAll(args.Value);
    if (str.length != 11) {
        oSrc.errormessage = "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit";
        args.IsValid = false;
        return;
    }
    var UnitIDA = str.substr(0, 4).toUpperCase();
    var UnitIDN = str.substr(4, 6);
    if (trimAll(UnitIDN).length != 6) {
        args.IsValid = false;
        return;
    }
    // var oParent = document.getElementById(oSrc.controltovalidate);
    var oParent = document.getElementById(getIAttribute(oSrc, "controltovalidate"));
    if (typeof (oParent.lpCheckDigit) == 'undefined' || ((oParent.lpCheckDigit == 'false' || oParent.lpCheckDigit == 'true') && str.length == 11))//if (typeof(oParent.lpCheckDigit)=='undefined' || oParent.lpCheckDigit=='false')
    {
        if (typeof (oParent.lpCheckDigit) == 'undefined')
            oParent.lpCheckDigit = "false";
        var UnitIDC = str.substr(10, 1);
    }

    //error message format.
    var msg = "";
    var spmsg = ""; // this is specific error message.
    //    msg = "<ul type=square><li>";
    //    msg += "4 Alpha characters - mandatory.</li>";
    //    msg += "<li>6 digit Number - mandatory.</li>";
    //    msg += "<li>1 check digit - optional.</li></ul>";

    if (str == "" || str.length < 10) {
        spmsg = "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit<br>";
        spmsg += msg;
        oSrc.errormessage = spmsg;
        args.IsValid = false;
        return;
    }
    else if ((UnitIDA.charCodeAt(0) < 65 || UnitIDA.charCodeAt(0) > 91) || (UnitIDA.charCodeAt(1) < 65 || UnitIDA.charCodeAt(1) > 91) || (UnitIDA.charCodeAt(2) < 65 || UnitIDA.charCodeAt(2) > 91) || (UnitIDA.charCodeAt(3) < 65 || UnitIDA.charCodeAt(3) > 91)) {//alpha check.
        spmsg = "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit<br>";
        spmsg += msg;
        oSrc.errormessage = spmsg;
        args.IsValid = false;
        return;
    }
    else if (isNaN(UnitIDN)) {//numeric check.
        spmsg = "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit<br>";
        spmsg += msg;
        //alert(spmsg);
        oSrc.errormessage = spmsg;
        args.IsValid = false;
        return;
    }
    else if ((isNaN(UnitIDC) || UnitIDC == "") && oParent.lpCheckDigit == 'false') {//Check Digit
        spmsg = "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit<br>";
        spmsg += msg;
        //alert(spmsg);
        oSrc.errormessage = spmsg;
        args.IsValid = false;
        return;
    }

    else if (oParent.lpCheckDigit == 'true') {
        oParent.value = str.substr(0, 4).toUpperCase() + str.substr(4, 6) + getCheckSum(str.substr(0, 10));
        args.IsValid = true;
        return;
    }
    else {
        //oParent.value=str.substr(0,4).toUpperCase() + str.substr(4,6) + getCheckSum(str.substr(0,10));
        args.IsValid = true;
        return;
    }
}

// Equipment No CheckDigit 
function getCheckSum(strNumber) {
    strNumber = strNumber.toUpperCase();
    var retVal = true;
    var total = parseInt("0")
    var product;
    if (strNumber.length == 10) {
        for (var i = 0; retVal = true, i < strNumber.length; i++) {
            var strCharCode = strNumber.charCodeAt(i);
            // Check For Alphabet
            if (strCharCode >= 65 && strCharCode <= 90) {
                product = a[strCharCode - 65]
            }
            else {
                // Check for Number
                if (strCharCode >= 48 && strCharCode <= 57) {
                    product = (strCharCode - 48)
                }
                //Not a Number or Alphabet
                else
                    retVal = false;
            }
            total = total + ((parseInt(product)) * Math.pow(2, i))
        }

        if (retVal) {
            var cs = (total % 11) % 10;
            return cs;
        }
    }
}

//on tab select event
function OBTabSel() {
    var bol = true;
    if (typeof (vrGridIds) != 'undefined')
        for (var vrGridId in vrGridIds) {
            var _actgridid = vrGridIds[vrGridId].split(";")[1];
            var _curtabid = vrGridIds[vrGridId].split(";")[0];
            if (typeof (ITab1.currentTab) != "undefined" && ITab1.currentTab == _curtabid) {
                var _grid_obj = el(_actgridid);
                if (typeof (_grid_obj) != "undefined" && _grid_obj != null) {
                    bol = _grid_obj.Validate();
                    if (bol == false)
                        break;
                    DBC();
                    /*if (_grid_obj.hasChanges==true)
                    _grid_obj.Submit();*/
                }
            }
        }
    return bol;
}


//For multiline textbox
function imposeMaxLength(Object, MaxLen) {
    var event = window.event || arguments.callee.caller.arguments[0];
    var _kc = event.keyCode ? event.keyCode : event.charCode ? event.charCode : event.which;
    HasChanges = true;
    if (document.selection) {
        var oRange = document.selection.createRange();
        return (Object.value.length - oRange.text.length <= MaxLen);
    }
    else {
        if (!checkSpecialKeys(event)) {
            if (Object.value.length > MaxLen - 1) {
                event.preventDefault(true);
            }
        }
    }
}

function checkSpecialKeys(e) {
    if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
        return false;
    else
        return true;
}
//For multiline Div
function imposeMaxLengthDiv(Object, MaxLen) {
    var event = window.event || arguments.callee.caller.arguments[0];
    var _kc = event.keyCode ? event.keyCode : event.charCode ? event.charCode : event.which;
    HasChanges = true;
    if (document.selection) {
        var oRange = document.selection.createRange();
        return (Object.value.length - oRange.text.length <= MaxLen);
    }
    else {
        if (!checkSpecialKeys(event)) {
            if (Object.value.length > MaxLen - 1) {
                event.preventDefault(true);
            }
        }
    }
}

//on paste check max length
function OnPasteCheckMaxLength(obj, len) {
    if (obj.value.length > 0) {
        var oRange;
        if (window.getSelection) {
            oRange = window.getSelection();
        }
        else if (document.selection) {
            oRange = document.selection.createRange();
        }
        if (oRange.text.length > 0) {
            len = len + oRange.text.length;
        }
        if (window.clipboardData)
            var sText = window.clipboardData.getData("Text").substr(0, len - obj.value.length);
        else if (event.clipboardData)
            var sText = event.clipboardData.getData("Text").substr(0, len - obj.value.length);
        oRange.text = sText;
        oRange.collapse(true);
        oRange.select();
    }
    else {
        if (window.clipboardData)
            obj.value = window.clipboardData.getData("Text").substr(0, len);
        else if (event.clipboardData)
            obj.value = event.clipboardData.getData("Text").substr(0, len);
    }
    var obj = event.srcElement || event.target;
    if($){
        $(obj).change();
    }
    //Commented for script error in ie.11 by govindarajan
    //var changeEvent;
    //if (obj.attributes["onchange"])
    //    changeEvent = obj.attributes["onchange"].value
    //else
    //    changeEvent= getIAttribute(obj, "onchange");
    //if (changeEvent != "undefined" && changeEvent != "")
    //    eval(changeEvent);
    HasChanges = true;
}



//Multiline More Info
function OpenMultiLinePopup(rUrl, TId, MxLength) {
    if (Page_IsValid) {
        var prms;
        var d = new Date();
        prms = rUrl + "?mL=" + MxLength + "&tid=" + TId + "&vl=" + encodeURIComponent(el(TId).value) + "&readonly=" + el(TId).readOnly + "&tms=" + d.getDate();
        showModalWindow("More Info", prms, "390px", "335px", "", "", "dfs('fmModalFrame').setMultiLineContent();", "More Info");
    }
}

function setMultiLineContent() {
    //var Tid = getQueryStringValue(document.location.href, "tid")
    // pdf('wfFrame' + pdf('CurrentDesk')).el(Tid).value = el("txtDescription").value;
    //pdf('wfFrame' + pdf('CurrentDesk')).el(Tid).focus();
    // pdf('wfFrame' + pdf('CurrentDesk')).el(Tid).IsCheckedOnKd = true;
    //pdfs('wfFrame' + pdf('CurrentDesk')).HasChanges = true; 
    var Tid = getQueryStringValue(document.location.href, "tid")
    pdfs('wfFrame' + pdf('CurrentDesk')).el(Tid).value = el("txtDescription").value;
    pdfs('wfFrame' + pdf('CurrentDesk')).el(Tid).focus();
    pdfs('wfFrame' + pdf('CurrentDesk')).el(Tid).IsCheckedOnKd = true;
    pdfs('wfFrame' + pdf('CurrentDesk')).HasChanges = true;
    HasChanges = true;
}

function formatDecimal(strValue, precision) {
    if (strValue != "" && !(isNaN(strValue))) {
        var intDecimalPlace = strValue.length - strValue.lastIndexOf(".")
        if (intDecimalPlace == 0 || strValue.lastIndexOf(".") == -1) {
            if (precision == 3)
                strValue = strValue + ".000"
            else
                strValue = strValue + ".00"
        }
        else if (intDecimalPlace == 2) {
            if (precision == 3)
                strValue = strValue + "00"
            else
                strValue = strValue + "0"
        }
        else if (intDecimalPlace == 3) {
            if (precision == 3)
                strValue = strValue + "0"
            else
                strValue = strValue
        }
        else if (intDecimalPlace == 4 && precision == 3) {
            strValue = strValue
        }
        if (strValue.lastIndexOf(".") == 0)
        { strValue = "0" + strValue }
        return strValue;
    }
}
function formatDecimalDouble(strValue, precision) {
    if (strValue != "" && !(isNaN(strValue))) {
        var intDecimalPlace = strValue.length - strValue.lastIndexOf(".")
        if (intDecimalPlace == 0 || strValue.lastIndexOf(".") == -1) {
            if (precision == 3)
                strValue = strValue + ".000"
            else
                strValue = strValue + ".00"
        }
        else if (intDecimalPlace == 2) {
            if (precision == 3)
                strValue = strValue + "00"
            else
                strValue = strValue + "0"
        }
        if (strValue.lastIndexOf(".") == 0)
        { strValue = "0" + strValue }
        return strValue;
    }
}

function getOffset(el) {
    var _x = 0;
    var _y = 0;
    while (el && !isNaN(el.offsetLeft) && !isNaN(el.offsetTop)) {
        _x += el.offsetLeft - el.scrollLeft;
        _y += el.offsetTop - el.scrollTop;
        el = el.offsetParent;
    }
    return { top: _y, left: _x };
}

function attachFile() {
    var fmAttachFile;
    fmAttachFile = document.frames["AttachmentPane_fmAttachFile"].document.getElementById("frmAttachFile");
    if (fmAttachFile == null) {
        showErrorMessage("There could be an issue with the last attached file. Due to security reasons, please relogin into the application.");
        return false;
    }
    fmAttachFile.filename.click();
    fmAttachFile.btnSubmit.click();
    HasChanges = true;
    getPageID();

    var objCallback = new Callback();
    objCallback.invoke("../UserControls/AttachFile.aspx", "attachFile")
    objCallback = null;
}

function removeAttachment(FilePath, AttachmentID) {

    var objCallback = new Callback();

    objCallback.add("FilePath", FilePath);
    objCallback.add("AttachmentID", AttachmentID);
    objCallback.invoke("../UserControls/AttachFile.aspx", "removeAttachment")

    if (objCallback.getCallbackStatus()) {
        var path, FileName, filesize, attid, type, attachmentlinks;

        path = objCallback.getReturnValue('TempPath');
        FileName = objCallback.getReturnValue('TempFileName');
        filesize = objCallback.getReturnValue('TempFileSize');
        attid = objCallback.getReturnValue('AttachmentId');
        type = objCallback.getReturnValue('Type');
        attachmentlinks = objCallback.getReturnValue('strAttachment');

        bindPaths(path, FileName, attid, type, attachmentlinks, filesize)
    }
    objCallback = null;

}


//Attachments
function bindPaths(paths, filenames, attids, type, attachedLinks, filesize) {
    if (type == 'remove') {
        if (el('hdnAttchIds').value != "") {
            el('hdnAttchIds').value = el('hdnAttchIds').value + "|";
        }
        el('hdnAttchIds').value = el('hdnAttchIds').value + attids;
        if (el('hdnAttchIds').value != "") {
            HasChanges = true;
        }
        el('hdnPaths').value = paths;
        el('hdnFileNames').value = filenames;
        el('hdnFileSize').value = filesize;

        showWarningMessage("File has been deleted.");
    }


    if (attachedLinks != "") {
        if (type == 'attach') {
            if (el('hdnPaths').value != "") {
                el('hdnPaths').value = el('hdnPaths').value + "|";
            }
            if (el('hdnFileNames').value != "") {
                el('hdnFileNames').value = el('hdnFileNames').value + "|";
            }
            if (el('hdnFileSize').value != "") {
                el('hdnFileSize').value = el('hdnFileSize').value + "|";
            }
            el('hdnPaths').value = el('hdnPaths').value + paths;
            el('hdnFileNames').value = el('hdnFileNames').value + filenames;
            el('hdnFileSize').value = el('hdnFileSize').value + filesize;
            HasChanges = true;
            if (paths != "")
                showInfoMessage("File uploaded successfully.");
        }
    }
    else {

        el('hdnPaths').value = "";
        el('hdnFileNames').value = "";
        el('hdnAttchIds').value = "";
        el('hdnFileSize').value = "";
    }
    var Attachment;
    var attLinks;
    Attachment = el("AttachedLinks");
    attachedLinks = attachedLinks.replace(/\&quot/g, "'");
    Attachment.innerHTML = attachedLinks;

}


var notAttached = true;


function bindAttachments(refno) {
    el("AttachedLinks").innerHTML = "";
    var objCallback = new Callback();
    var wfdata = getWFDATA();

    var activityid = getQueryStringValue(wfdata, "ACTVTY_ID");

    objCallback.add("ActivityID", activityid);
    objCallback.add("RefNo", refno);
    objCallback.invoke("../UserControls/AttachFile.aspx", "bindAttachments")

    if (objCallback.getCallbackStatus()) {
        var path, FileName, filesize, attid, type, attachmentlinks;

        path = objCallback.getReturnValue('TempPath');
        FileName = objCallback.getReturnValue('TempFileName');
        filesize = objCallback.getReturnValue('TempFileSize');
        attid = objCallback.getReturnValue('AttachmentId');
        type = objCallback.getReturnValue('Type');
        attachmentlinks = objCallback.getReturnValue('strAttachment');
        el('hdnPaths').value = "";
        el('hdnFileNames').value = "";
        bindPaths(path, FileName, attid, "bind", attachmentlinks, filesize);
    }
    objCallback = null;
}



function validateDesc(oSrc, args) {
    // if ((args.Value).length > parseInt(el(oSrc.controltovalidate).maxlength) + 1)
    if ((args.Value).length > (getIAttribute(el(oSrc.controltovalidate), "maxLength")) + 1)
        args.IsValid = false;
    else
        args.IsValid = true;
}

//To build (add keys or get values from query string)
function buildquerystring(q) {
    if (q.length > 1) this.q = q.substring(0, q.length);
    else this.q = null;
    this.keyValuePairs = new Array();

    if (q) {
        for (var i = 0; i < this.q.split('&').length; i++) {
            this.keyValuePairs[i] = this.q.split('&')[i];
        }
    }

    this.additem = function (keyname, value) {
        var keyexists = false;
        for (var j = 0; j < this.keyValuePairs.length; j++) {
            if (this.keyValuePairs[j].split('=')[0] == keyname) {
                this.keyValuePairs[j] = keyname + '=' + value;
                keyexists = true;
            }
        }
        if (!keyexists)
            this.keyValuePairs[this.keyValuePairs.length] = keyname + '=' + value;
    }

    this.getquerystring = function () {
        return this.keyValuePairs.join('&');
    }

    this.getValue = function (s) {
        for (var j = 0; j < this.keyValuePairs.length; j++) {
            if (this.keyValuePairs[j].split('=')[0] == s)
                return this.keyValuePairs[j].split('=')[1];
        }
        return false
    }
}


// datecompare - for custom comparevalidation purpose
function DateCompare(_strDate1, _strDate2) {
    var strDate;
    var strDateArray;
    var strDay;
    var strMonth;
    var strYear;

    strDateArray = _strDate1.split("-");

    strDay = strDateArray[0];
    strMonth = strDateArray[1];
    strYear = strDateArray[2];

    _strDate1 = new Date(strYear, GetMonth(strMonth), strDay);

    strDateArray = _strDate2.split("-");

    strDay = strDateArray[0];
    strMonth = strDateArray[1];
    strYear = strDateArray[2];

    _strDate2 = new Date(strYear, GetMonth(strMonth), strDay);

    if (_strDate1 > _strDate2) {
        return true;
    }
    else {
        return false;
    }
}

// get month - used locally
function GetMonth(strMonth) {
    var intMonth;
    switch (strMonth) {
        case "JAN": intMonth = 0; break;
        case "FEB": intMonth = 1; break;
        case "MAR": intMonth = 2; break;
        case "APR": intMonth = 3; break;
        case "MAY": intMonth = 4; break;
        case "JUN": intMonth = 5; break;
        case "JUL": intMonth = 6; break;
        case "AUG": intMonth = 7; break;
        case "SEP": intMonth = 8; break;
        case "OCT": intMonth = 9; break;
        case "NOV": intMonth = 10; break;
        case "DEC": intMonth = 11; break;
    }
    return intMonth;
}

function F1help() {

}

//This method is used to set Default Estimate Input Type
function setDefaultEstimateType(sLookupID) {
    var aLookupValue = new Array();
    aLookupValue[0] = '10';
    aLookupValue[1] = 'WE';
    el(sLookupID).value = "WE";
    setLkpContrlValue(sLookupID, aLookupValue);
}



function getCurrentDate() {
    var months = new Array(13);
    months[0] = "JAN";
    months[1] = "FEB";
    months[2] = "MAR";
    months[3] = "APR";
    months[4] = "MAY";
    months[5] = "JUN";
    months[6] = "JUL";
    months[7] = "AUG";
    months[8] = "SEP";
    months[9] = "OCT";
    months[10] = "NOV";
    months[11] = "DEC";
    var now = new Date();
    var monthnumber = now.getMonth();
    var monthname = months[monthnumber];
    var monthday = now.getDate();
    var year = now.getYear();
    if (year < 2000) { year = year + 1900; }
    var dateString = monthname +
                    ' ' +
                    monthday +
                    ', ' +
                    year;
    return dateString;
}

function getCurrentDateFormatted() {
    var months = new Array(13);
    months[0] = "JAN";
    months[1] = "FEB";
    months[2] = "MAR";
    months[3] = "APR";
    months[4] = "MAY";
    months[5] = "JUN";
    months[6] = "JUL";
    months[7] = "AUG";
    months[8] = "SEP";
    months[9] = "OCT";
    months[10] = "NOV";
    months[11] = "DEC";
    var now = new Date();
    var monthnumber = now.getMonth();
    var monthname = months[monthnumber];
    var monthday = now.getDate();
    var year = now.getYear();
    if (year < 2000) { year = year + 1900; }
    var dateString = monthday +
                    '-' +
                    monthname +
                    '-' +
                    year;
    return dateString;
}

function getClientTime() {
    var now = new Date();
    var hour = now.getHours();
    var minute = now.getMinutes();
    var second = now.getSeconds();
    var ap = "AM";
    if (hour > 11) { ap = "PM"; }
    if (hour > 12) { hour = hour - 12; }
    if (hour == 0) { hour = 12; }
    if (hour < 10) { hour = "0" + hour; }
    if (minute < 10) { minute = "0" + minute; }
    if (second < 10) { second = "0" + second; }
    var timeString = hour +
                    ':' +
                    minute +
                    ':' +
                    second +
                    " " +
                    ap;
    return timeString;
}
function confirmChanges(clicktype, clickmethod) {
    psc().clicktype = clicktype;
    psc().clickmethod = clickmethod;
    var confirmresult = false;
    var confirm = false;

    if (clicktype == "menu" || clicktype == "list" || clicktype == "submit" || bAllowAction == true) {
        if (typeof (pdfs("wfFrame" + pdf("CurrentDesk"))) == "object") {
            if (typeof (pdfs("wfFrame" + pdf("CurrentDesk")).getPageChanges) != "undefined") {
                if (pdfs("wfFrame" + pdf("CurrentDesk")).getPageChanges() == true && confirmresult == false) {
                    if ((pdfs("wfFrame" + pdf("CurrentDesk")).el("btnSubmit") != null && pdfs("wfFrame" + pdf("CurrentDesk")).el("btnSubmit").style.display == "none") || (pdfs("wfFrame" + pdf("CurrentDesk")).el("btnSubmit") != null && trimAll(getText(pdfs("wfFrame" + pdf("CurrentDesk")).el("btnSubmit"))).indexOf("Submit") == -1))
                        return confirm;
                    showConfirmMessage("Changes has been made in the Screen. Click 'Yes' to accept the changes or 'No' to ignore the changes",
                                "wfs().yesButtonClick();", "wfs().noButtonClick();");
                    confirm = true;
                }
            }
        }
    }
    return confirm;
}

//This method is called when YES is clicked on save changes popup
function yesButtonClick() {

    pdfs("wfFrame" + pdf("CurrentDesk")).Submit_Click();
    psc().gsUIMode = "Confirm";
    if (pdfs("wfFrame" + pdf("CurrentDesk")).HasNoError) {
        psc().confirmresult = true;
        eval(psc().clickmethod);
        confirmresult = false;
        //psc().confirmresult = false;
        // psc().hideConfirmMessagePane('YES');

    }
}

//This method is called when NO is clicked on save changes popup
function noButtonClick() {
    pdfs("wfFrame" + pdf("CurrentDesk")).HasChanges = false
    pdfs("wfFrame" + pdf("CurrentDesk")).HasRCChanges = false;
    if (typeof (pdfs("wfFrame" + pdf("CurrentDesk")).resetGridChanges) != "undefined") {
        pdfs("wfFrame" + pdf("CurrentDesk")).resetGridChanges();
    }
    psc().gsUIMode = "";
    psc().confirmresult = true;
    eval(psc().clickmethod);
    confirmresult = false;
    // psc().hideConfirmMessagePane('NO');
    // psc().confirmresult = false;

    //reset grid changes - added by saravanan
    if (typeof (vrGridIds) != "undefined") {
        for (var vrGridId in vrGridIds) {
            var _actgridid = vrGridIds[vrGridId].split(";")[1];
            resetHasChanges(_actgridid);
        }
    }
}

function showWorkingMessage() {
    psc().showLayer("Working..");
}

//This function is used to show loading message in workflow pane load.
function hideWorkingMessage() {
    psc().hideLayer();
}


//Get Custom WFDATA
function getWFDataKey(key) {
    var WFDATA = el('WFDATA').value;
    return getQueryStringValue(WFDATA, key)
}
//function to check the object is null or empty
function ISNullorEmpty(obj) {
    if (typeof (obj) == 'undefined' || obj == null) {
        return true;
    }
    return false;
}

function checkDataLength(obj) {
    //UIG fix for Description
    var len = getIAttribute(document.getElementById('txtDescription'), "mL")

    if (obj.value.length > 0) {
        if (window.clipboardData)
            obj.value = obj.value + window.clipboardData.getData("Text").substr(0, len - obj.value.length - 1);
        else if (event.clipboardData)
            obj.value = obj.value + event.clipboardData.getData("Text").substr(0, len - obj.value.length - 1);
    }
    else {
        if (window.clipboardData)
            obj.value = window.clipboardData.getData("Text").substr(0, len);
        else if (event.clipboardData)
            obj.value = event.clipboardData.getData("Text").substr(0, len);
    }
}

function setFocusMoreInfoTextBox() {
    setFocusToField("txtDescription");
}

function sessionExpired() {
    window.top.location.href = "Default.aspx?se=1";
}

function maliciousScript() {
    window.top.location.href = "Default.aspx?se=2";
}

function checkFile() {
    var re = new RegExp("^[a-zA-Z0-9-_. ]+$");
    var _filename = el("filename").value;
    if (_filename.length > 0) {
        _filename = _filename.substr(_filename.lastIndexOf("\\") + 1, _filename.length);
        if (_filename.length == 0) {
            showErrorMessage("Please Select a File to Upload.");
            return false;
        }
        var matches = re.exec(_filename);
        if (matches != null && matches.length > 0) {
            _filename = matches[0];
        }
        else {
            showErrorMessage("File Name with special characters are not allowed.");
            return false;
        }
        var _extnArr = new Array();
        _extnArr = _filename.split(".")
        if (_extnArr.length > 0) {
            if (_extnArr[0].length > 50) {
                showErrorMessage("Attachment File Name must be below 50 Characters.");
                return false;
            }
            var _filtypes = new Array("jpg", "png", "xls", "xlsx", "doc", "pdf", "docx");
            for (var i = 0; i < _filtypes.length; i++) {
                if (_filtypes[i] == _extnArr[_extnArr.length - 1].toLowerCase()) {
                    return true;
                }
            }
            showErrorMessage("File Must be in following formats: .jpg, .png, .xls, .xlsx, .doc, .pdf, .docx");
            return false;
        }
        else {
            showErrorMessage("Invalid File Name.");
            return false;
        }
    }
    else {
        showErrorMessage("Please Select a File to Upload.");
        return false;
    }
    return true;
}

function isValidDate(x) {
    var strDateArray;
    var strDay;
    var strMonth;
    var strYear;
    strDateArray = x.split("-");

    strDay = strDateArray[0];
    strMonth = strDateArray[1];
    strYear = strDateArray[2];

    x = new Date(strYear, GetMonth(strMonth), strDay);

    return (null != x) && !isNaN(x) && ("undefined" !== typeof x.getDate);
}

function currentURL() {
    return document.location.href;
}


//This method will be used to lock the other activities
function lockRecord(reffield, refno) {
    var sActivityName = getQueryStringValue(document.location.href, "activityname");
    var oCallback = new Callback();
    oCallback.add("reffield", reffield);
    oCallback.add("refno", refno);
    oCallback.add("actvty_nam", sActivityName);
    oCallback.invoke("../UserControls/ActionPane.aspx", "LockRecord");
    if (oCallback.getCallbackStatus()) {

        var lockedMsg = oCallback.getReturnValue("lockedMessage")
        if (lockedMsg != "") {
            showWarningMessage(lockedMsg);
            var smode = getQueryStringValue(document.location.href, "mode");
            if (smode != "view") {
                disableSubmitPane();
            }
        }
        else {
            hideMessage();
            var smode = getQueryStringValue(document.location.href, "mode");
            if (smode != "view") {
                enableSubmitPane();
            }
        }
    }
}

//This method will be used to reset locked records
function resetLockedRecord(root) {
    var oCallback = new Callback();
    var _url;
    if (typeof (root) != "undefined" && root == "root") {
        _url = "UserControls/ActionPane.aspx";
    }
    else {
        _url = "../UserControls/ActionPane.aspx";
    }
    oCallback.invoke(_url, "ResetLockedRecord");
}

//This method will be used to select all checkboxes inside grid
function selectALL(headerCheckbox, gridID, columnIndex) {
    var grid = el(gridID);
    var checkedStatus = headerCheckbox.checked;
    for (i = 0; i < grid.rows.length; i++) {
        var cell = grid.rows[i].cells[columnIndex];
        for (j = 0; j < cell.childNodes.length; j++) {
            //if childNode type is CheckBox                 
            if (cell.childNodes[j].type == "checkbox") {
                //if (cell.childNodes[j].checked == false) {
                //assign the status of the Select All checkbox to the cell checkbox within the grid
                cell.childNodes[j].click();
                //}
            }
        }
    }
    headerCheckbox.checked = checkedStatus;
}

//This method will be used to select all checkboxes inside grid
function unSelectALL(headerCheckbox, gridID, columnIndex) {
    var grid = el(gridID);
    var checkedStatus = headerCheckbox.checked;
    for (i = 0; i < grid.rows.length; i++) {
        var cell = grid.rows[i].cells[columnIndex];
        for (j = 0; j < cell.childNodes.length; j++) {
            //if childNode type is CheckBox                 
            if (cell.childNodes[j].type == "checkbox") {
                if (cell.childNodes[j].checked == true) {
                    //assign the status of the Select All checkbox to the cell checkbox within the grid
                    cell.childNodes[j].click();
                }
            }
        }
    }
    headerCheckbox.checked = checkedStatus;
}
//This method is used to get the Depot filter for the Lookup control.
function applyDepoFilter() {
    if (getConfigSetting('070') != "True") {
        var DPT_ID = getQueryStringValue(getWFDATA(), "DPT_ID");
        return "DPT_ID=" + DPT_ID;
    }
}

function FormatSlab() {
    var event = getEvent(this);
    if (event) {
        var obj = event.srcElement || event.target;
        if (obj.value != "" && !(isNaN(obj.value))) {
            if (obj.value > 0) {
                obj.value = (obj.value).replace(/^[0]+/g, "");
            }
        }
    }
}

//This method is used to manually trigger the click event for different browsers.
function DBC() {
    safari = /Safari/i.test(navigator.userAgent);
    chrome = /Chrome/i.test(navigator.userAgent);
    if (safari && !chrome) { //if (navigator.userAgent.indexOf("Safari") >= 0) {
        if (!event)
            return;
        try {
            var evt = document.createEvent('MouseEvents');
            evt.initMouseEvent('click', true, true, document.defaultView, 1, 0, 0, 0, 0, false, false, false, false, 0, document.body);
            document.body.dispatchEvent(evt);
        }
        catch (ex) { }
        if (event.preventDefault)
            event.preventDefault(true);
        return;
    }
    if (document.body.click) {
        document.body.click();
    }
    else {
        var evt = document.createEvent('MouseEvents');
        evt.initMouseEvent('click', true, true, document.defaultView, 1, 0, 0, 0, 0, false, false, false, false, 0, document.body);
        document.body.dispatchEvent(evt);
    }
}
//This method is used to get the iframe object.
function getIFrameObj(iFrame) {
    return window.parent.frames[iFrame];
}

//This method is used to get the element inside an iFrame.
function getIFrameElement(frame, element) {
    if (element) {
        try { return document.frames[frame].document.documentElement.document.getElementById(element); }
        catch (ex) { return window.frames[frame].document.all[element]; }
    }
    else {
        try { return document.frames[frame].document.documentElement.document.Script; }
        catch (ex) { return window.frames[frame]; }
    }
}

//This method is used to set the the text of label,button,td,span,div...etc.
function setText(obj, value) {
    if (typeof (obj.textContent) != "undefined")
        obj.textContent = trimAll(value);
    else
        obj.innerText = trimAll(value);
}

//This method is used to get the the text of label,button,td,span,div...etc.
function getText(obj) {
    if (typeof (obj.innerText) != "undefined")
        return trimAll(obj.innerText);
    else
        return trimAll(obj.textContent);
}

//This method is used to fire event manually
function fireEvent(element, eventType) {
    if (element.click)
        return $(element).click();
    if (document.createEvent) {
        // dispatch for firefox + others
        var evt = document.createEvent("HTMLEvents");
        evt.initEvent(eventType, true, true); // event type,bubbling,cancelable
        return !element.dispatchEvent(evt);
    } else {
        // dispatch for IE
        var evt = document.createEventObject();
        return element.fireEvent('on' + eventType, evt)
    }
}

//Event Check method for firefox
document.onkeypress = eventcheck;
//This method is used to check the key press event for Fire fox
function eventcheck(event) {
    if (!event)
        return;
    try {
        var obj = event.target;
        if (obj.mozIsTextField) {
            if (event.keyCode == 9 || event.keyCode == 13) {
                if (obj.id == gtGrdObj(event.target.parentNode).editCtrl) {
                    event.preventDefault(true);
                    event.stopPropagation();
                }
                else if (obj.value == "" && getIAttribute(event.target.nextSibling, "q") == "t") {
                    event.preventDefault(true);
                    event.stopPropagation();
                }
            }
        }
    }
    catch (ex) { }
}

//This function is used to get the Grid Object
function gtGrdObj(tdObj) { try { var ele = tdObj.parentNode.parentNode.parentNode; if (ele.tagName.toLowerCase() == "tbody") le = ele.parentNode; return ele; } catch (e) { return null; } }

//This function is used to export the content in grids of all the master pages.
function exportToExcel() {
    var fE = document.getElementById("iFgfrm");
    if (fE == null) {
        fE = document.createElement("IFRAME");
        fE.id = "iFgfrm"
        fE.scrolling = "no";
        fE.frameborder = "0";
        fE.width = "100";
        fE.height = "100";
        document.body.appendChild(fE);
        hideDiv("iFgfrm");
    }
    fE.src = currentURL() + "&ifgActivityId=" + getQStr("activityid") + "&Export=True";
    return;
}
//This function is used to get the Config value from the server using a call back
function getConfigSetting(keyName) {
    return ppsc().getConfigValue(keyName);
}

//This function is used to check the role rights of the page.
function checkRights() {

    var bCreateRights = GetBoolFromString(getWFDataKey("CRT_BT"));
    var bEditRights = GetBoolFromString(getWFDataKey("EDT_BT"));
    var sMode = getPageMode();
    var bAllowToSubmit = false;

    if (sMode == MODE_NEW && bCreateRights == true) {
        bAllowToSubmit = true;

    }
    else if (sMode == MODE_EDIT && bEditRights == true) {
        bAllowToSubmit = true;
    }
    else {
        showWarningMessage("You do not have sufficient rights to perform the selected operation.");
    }
    return bAllowToSubmit;

}

function FormatTwoDecimal() {
    if (event != null) {
        var obj = event.srcElement;
        if (obj.value != "" && !(isNaN(obj.value))) {
            //obj.value = parseFloat(obj.value);
            var intDecimalPlace = obj.value.length - obj.value.lastIndexOf(".")

            if (intDecimalPlace == 1 || intDecimalPlace == 0 || obj.value.lastIndexOf(".") == -1) {
                obj.value = obj.value + ".00"
            }
            else if (intDecimalPlace == 2) {
                obj.value = obj.value + "0"
            }
            else if (intDecimalPlace == 3) {
                obj.value = obj.value
            }

        }
    }
}

//To display fields in Four point decimal
function FormatFourDecimal() {
    if (event != null) {
        var obj = event.srcElement;
        if (obj.value != "" && !(isNaN(obj.value))) {
            var intDecimalPlace = obj.value.length - obj.value.lastIndexOf(".")

            if (intDecimalPlace == 1 || intDecimalPlace == 0 || obj.value.lastIndexOf(".") == -1) {
                obj.value = obj.value + ".0000"
            }
            else if (intDecimalPlace == 2) {
                obj.value = obj.value + "000"
            }
            else if (intDecimalPlace == 3) {
                obj.value = obj.value + "00"
            }
            else if (intDecimalPlace == 4) {
                obj.value = obj.value + "0"
            }
            else if (intDecimalPlace == 5) {
                obj.value = obj.value
            }
        }
    }
}

function trimAll(_str) {
    if (_str != null && typeof (_str) == "string") {
        return _str.replace(/^\s*|\s*$/g, "");
    }
    return null;
}

// datecompare - for custom comparevalidation purpose
function DateCompareEqual(_strDate1, _strDate2) {
    var strDate;
    var strDateArray;
    var strDay;
    var strMonth;
    var strYear;

    strDateArray = _strDate1.split("-");

    strDay = strDateArray[0];
    strMonth = strDateArray[1];
    strYear = strDateArray[2];

    _strDate1 = new Date(strYear, GetMonth(strMonth), strDay);

    strDateArray = _strDate2.split("-");

    strDay = strDateArray[0];
    strMonth = strDateArray[1];
    strYear = strDateArray[2];

    _strDate2 = new Date(strYear, GetMonth(strMonth), strDay);

    if (_strDate1 >= _strDate2) {
        return true;
    }
    else {
        return false;
    }
}

function FormatThreeDecimal() {
    if (event != null) {
        var obj = event.srcElement;
        if (obj.value != "" && !(isNaN(obj.value))) {
            var intDecimalPlace = obj.value.length - obj.value.lastIndexOf(".")

            if (intDecimalPlace == 1 || intDecimalPlace == 0 || obj.value.lastIndexOf(".") == -1) {
                obj.value = obj.value + ".000"
            }
            else if (intDecimalPlace == 2) {
                obj.value = obj.value + "00"
            }
            else if (intDecimalPlace == 3) {
                obj.value = obj.value + "0"
            }
            else if (intDecimalPlace == 4) {
                obj.value = obj.value
            }
        }
    }
}

// To assign title for all input controls
function loadInputControls() {
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (typeof (getIAttribute(inputs[i], "hT")) != "undefined") {
            var helpid = (getIAttribute(inputs[i], "hT")).split(",");
            var sHelpMessage;
            if (helpid.length >= 0) {
                sHelpMessage = getHelpMessage(helpid[0], inputs[i].readOnly);
            }
            inputs[i].title = sHelpMessage;
        }
    }
    var txtAreas = document.getElementsByTagName("textarea");
    for (var i = 0; i < txtAreas.length; i++) {
        if (typeof (getIAttribute(txtAreas[i], "hT")) != "undefined") {
            var helpid = (getIAttribute(txtAreas[i], "hT")).split(",");
            var sHelpMessage;
            if (helpid.length >= 0) {
                sHelpMessage = getHelpMessage(helpid[0], txtAreas[i].readOnly);
            }
            txtAreas[i].title = sHelpMessage;
        }
    }
    if (ppsc().gblnHelpTip) {
        showHelpStrip();
    }
}
function getIAttribute(obj, key) {
    try {
        if (!obj.hasAttribute) {
            if (obj.attributes[key].value == "null")
                return null;
            else
                return obj.attributes[key].value;
        }
        if (obj.hasAttribute(key)) {
            if (obj.getAttribute(key) == "null")
                return null;
            else
                return obj.getAttribute(key);
        }
        else
            return undefined;
    }
    catch (ex) { return; }
}

function lockData(obj, primaryRefNo, secondaryRefNo) {
    var pageName = "";
    var refNo = "";
    var arrPageName = new Array();
    var activityId = "";
    var activityName = "";
    activityId = getQueryStringValue(document.location.href, "activityid");
    activityName = getQueryStringValue(document.location.href, "activityname");
    if (activityId == "125") {
        refNo = secondaryRefNo;
    }
    else {
        refNo = primaryRefNo;
    }

    pageName = getQueryStringValue(document.location.href, "apu");
    arrPageName = pageName.split("/");
    if (arrPageName.length > 1) {
        var oCallback = new Callback();
        oCallback.add("checkBit", obj.checked);
        oCallback.add("EquipmentNo", refNo);
        oCallback.add("ActivityName", activityName);
        oCallback.invoke(arrPageName[1], "RecordLockData");
        if (oCallback.getCallbackStatus()) {
            if (oCallback.getReturnValue("IPError") == 'true') {
                obj.checked = false;
                showErrorMessage("This record (" + primaryRefNo + ") cannot be modified because it is already being used by " + '<b>' + oCallback.getReturnValue("UserName") + '</b>' + " user in another session. " + '<b>' + "(Activity Name : " + oCallback.getReturnValue("ActivityName") + '</b>' + ")");
            }
            else if (oCallback.getReturnValue("IPError") == 'false') {
                obj.checked = false;
                showErrorMessage("This record (" + primaryRefNo + ") cannot be modified because it is already being used by " + '<b>' + oCallback.getReturnValue("UserName") + '</b>' + " user in another place. " + '<b>' + "(Activity Name : " + oCallback.getReturnValue("ActivityName") + '</b>' + ")");
            }
            return true;
        }
        else {
            return false;
        }
        oCallback = null;
    }
}

//Record Locking
function recordLockError(referenceNo, userName, ipError, activityName) {
    if (userName != '' && referenceNo != '') {
        pdfs("wfFrame" + pdf("CurrentDesk")).recordLockChanges = true;
        pdfs("wfFrame" + pdf("CurrentDesk")).hideDiv("divSubmit");
        pdfs("wfFrame" + pdf("CurrentDesk")).showDiv("divLockMessage");
        if (ipError == 'true') {
            pdfs("wfFrame" + pdf("CurrentDesk")).el('divLockMessage').innerText = ("This record (" + referenceNo + ") cannot be modified because it is already being used by " + userName + " user  in another session. " + "(Activty Name : " + activityName + ")");
        }
        else {
            pdfs("wfFrame" + pdf("CurrentDesk")).el('divLockMessage').innerText = ("This record (" + referenceNo + ") cannot be modified because it is already being used by " + userName + " user  in another place. " + "(Activty Name : " + activityName + ")");
        }
        return false;
    }
    else {
        pdfs("wfFrame" + pdf("CurrentDesk")).recordLockChanges = false;
        pdfs("wfFrame" + pdf("CurrentDesk")).hideDiv("divLockMessage");
        pdfs("wfFrame" + pdf("CurrentDesk")).showDiv("divSubmit");
        return true;
    }
}

function recordLockMessageEdit(referenceNo, userName, ipError, activityName) {
    if (ipError == 'true') {
        showErrorMessage("This record (" + referenceNo + ") cannot be modified because it is already being used by " + '<b>' + userName + '</b>' + " user in another session. " + '<b>' + "(Activity Name : " + activityName + '</b>' + ")");
    }
    else {
        showErrorMessage("This record (" + referenceNo + ") cannot be modified because it is already being used by " + '<b>' + userName + '</b>' + " user in another place. " + '<b>' + "(Activity Name : " + activityName + '</b>' + ")");
    }
    return false;
}

function activitySubmitMessage(message) {
    showErrorMessage("The following equipment no(s) are already billed. " + message);
}

function checkListGrid(_id) {
    if ((_id.indexOf("List") != -1) || (_id.indexOf("Master") != -1))
        return true;
    else
        return false;
}
function checkGridHeader() {
    if (typeof (vrGridIds) != "undefined") {
        for (var vrGridId in vrGridIds) {
            var _actgridid = vrGridIds[vrGridId].split(";")[1];
            if (el(_actgridid)) {
                iFg_SetHeader(el(_actgridid));
            }
        }
    }
}