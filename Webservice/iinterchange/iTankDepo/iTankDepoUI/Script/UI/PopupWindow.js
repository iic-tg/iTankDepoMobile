var popupContentURL,popupQstr,popupMode;
function loadContent() {
    var popupURL = document.location.href;
    var popupTitle = getQueryStringValue(popupURL, "title");
    popupContentURL = popupURL.split("&apu=")[1]
    popupQstr = popupContentURL.split("?")[1];
    popupMode = getQueryStringValue(popupContentURL, "mode");

    loadingPopup(popupTitle);

    el("wfFrame" + pdf("CurrentDesk")).src = popupContentURL;

}

//This method used to execute load data in workspace page
function initPopupData() {
    var _framedoc = el("wfFrame").contentWindow.document;
    if (_framedoc == null)
        return;    
    var popupURL = _framedoc.URL;
    var type = getQueryStringValue(popupURL, "type");
    var itemno = getQueryStringValue(popupURL, "itemno");

    if (type != "report") {
        ppsc().gsURL = popupURL;
        if (_framedoc.Script)
            _framedoc.Script.loadPageData(popupURL, popupQstr, popupMode, itemno);
        else
            getIFrameObj("wfFrame").loadPageData(decodeURIComponent(popupURL), popupQstr, popupMode, itemno);
    }
}