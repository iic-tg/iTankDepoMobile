var _itemno;

//This method is used to change style while mouse over
function toggleStyle(obj, c) {
    obj.className = c;
}

//This method is used navigate first record from list grid
function firstRecord() {
    var sLocation = document.location.href;
    var sMode = getQueryStringValue(sLocation, "mode");
    hideMessage();
    if (sMode == MODE_VIEW) {
        var sURL = sLocation.split("?")[0];
        var sQstr = sLocation.split("?")[1];
        var b = new buildquerystring(sQstr);
        b.additem("itemno", "0");

        sQstr = b.getquerystring();
        sURL = sURL + "?" + sQstr;
        document.location.href = sURL;
    }
    else {
        if (pdfs("plFrame" + pdf("CurrentDesk")).listRowCount == 0) {

            showWarningMessage("No Records Found");
            return;
        }
        if (ppsc().giItemNo == 0) {
            showWarningMessage("You have already reached the first record.");
            return;
        }
        else {
            _itemno = 0;
            navigateRecord(_itemno, sMode, "first");
        }
    }
}


//This method is used navigate previous record from list grid
function prevRecord() {
    var sLocation = document.location.href;
    var sMode = getQueryStringValue(sLocation, "mode");
    hideMessage();

    if (sMode == MODE_VIEW) {
        var sURL = sLocation.split("?")[0];
        var sQstr = sLocation.split("?")[1];
        var b = new buildquerystring(sQstr);
        var itemno = b.getValue("itemno");
        if (itemno > 0) {
            itemno = parseInt(itemno) - 1;
        }
        b.additem("itemno", itemno);
        sQstr = b.getquerystring();
        sURL = sURL + "?" + sQstr;
        document.location.href = sURL;
    }
    else {
        if (pdfs("plFrame" + pdf("CurrentDesk")).listRowCount == 0) {
            showWarningMessage("No Records Found");
            return;
        }
        _itemno = ppsc().giItemNo;
        ppsc().gsUIMode = "Navigation";

        if (_itemno == 0 || typeof (_itemno) == "undefined") {
            showWarningMessage("You have already reached the first record.");
            return;
        }
        else {
            _itemno = parseInt(_itemno) - 1;
        }
        navigateRecord(_itemno, sMode, "prev");
    }
}

//This method is used navigate next record from list grid
function nextRecord() {
    var sLocation = document.location.href;
    var sMode = getQueryStringValue(sLocation, "mode");
    if (ppsc().gSubmitNext != "1") {
        hideMessage();
    }

    if (sMode == MODE_VIEW) {
        var sURL = sLocation.split("?")[0];
        var sQstr = sLocation.split("?")[1];
        var b = new buildquerystring(sQstr);
        var itemno = b.getValue("itemno");
        itemno = parseInt(itemno) + 1;
        b.additem("itemno", itemno);
        sQstr = b.getquerystring();
        sURL = sURL + "?" + sQstr;
        document.location.href = sURL;
    }
    else {
        if (pdfs("plFrame" + pdf("CurrentDesk")).listRowCount == 0) {
            showWarningMessage("No Records Found");
            return;
        }
        _itemno = ppsc().giItemNo;
        ppsc().gsUIMode = "Navigation";

        if (_itemno == pdfs("plFrame" + pdf("CurrentDesk")).listRowCount || typeof (_itemno) == "undefined") {
            showWarningMessage("You have already reached the last record.");
            return;
        }
        else {
            _itemno = parseInt(_itemno) + 1;
        }
        navigateRecord(_itemno, sMode, "next");
    }
}

//This method is used navigate last record from list grid
function lastRecord() {
    var sLocation = document.location.href;
    var sMode = getQueryStringValue(sLocation, "mode");
    hideMessage();

    if (sMode == MODE_VIEW) {
        var sURL = sLocation.split("?")[0];
        var sQstr = sLocation.split("?")[1];
        var b = new buildquerystring(sQstr);
        b.additem("itemno", "0");
        sQstr = b.getquerystring();
        sURL = sURL + "?" + sQstr;
        document.location.href = sURL;
    }
    else {
        if (pdfs("plFrame" + pdf("CurrentDesk")).listRowCount == 0) {
            showWarningMessage("No Records Found");
            return;
        }
        _itemno = pdfs("plFrame" + pdf("CurrentDesk")).listRowCount - 1;
        if (ppsc().giItemNo == _itemno) {
            showWarningMessage("You have already reached the last record.");
            return;
        }
        else {
            ppsc().gsUIMode = "Navigation";
            navigateRecord(_itemno, sMode, "last");
        }
    }
}

//This method is used navigate the corresponding record using itemno
function navigateRecord(_itemno, sMode, nMode) {
    var sURL = document.location.href;
    ppsc().gsUIMode = "Navigation";
    var pMode = ppsc().gsListMode;
    var iActivityID = getQueryStringValue(sURL, "activityid");
    var sMenuData = ppsc().Activities[iActivityID].split(";")[1].split("url=")[1];
    var sPageURL = getQueryStringValue(sMenuData, "apu");
    var sPageTitle = getQueryStringValue(sMenuData, "pagetitle");
    var sPageType = getQueryStringValue(sMenuData, "pagetype");
    var sTableName = getQueryStringValue(sMenuData, "tablename");
    var sActivityName = getQueryStringValue(sMenuData, "activityname");
    if (ppsc().gsPageType == "CustomMaster") {
        pdfs("clFrame" + pdf("CurrentDesk")).loadPage(sPageURL, iActivityID, sPageTitle, sPageType, sTableName, "edit", _itemno);
    }
    else if (ppsc().gsPageType == "Transaction") {
        viewEstimate(_itemno, iActivityID, sMenuData, sPageURL, sPageType, sMode, nMode)
    }
    else {
        if (typeof (pdfs("plFrame" + pdf("CurrentDesk")).el("iTabSelection_ITab2").value) != "undefined") {
            if (pdfs("plFrame" + pdf("CurrentDesk")).el("iTabSelection_ITab2").value == "TabPage1" && pdfs("plFrame" + pdf("CurrentDesk")).el("divTab").style.display != "none") {
                pMode = "new";
            }
        }

        pdfs("plFrame" + pdf("CurrentDesk")).loadPage(sPageURL, iActivityID, sPageTitle, sPageType, sTableName, pMode, _itemno);
    }
    ppsc().giItemNo = _itemno;
    ppsc().gsUIMode = "";
}


//This method is used to open estimate page to approve estimate
function backToListPage() {
    var backToList = "&backToList=true";
    if (pdfs("plFrame" + pdf("CurrentDesk")).location.href.indexOf(backToList) > -1) {
        ppsc().onMenuClick(pdfs("plFrame" + pdf("CurrentDesk")).location.href, true);
    

    }
    else {
        psc().gsURL + backToList;
        ppsc().onMenuClick(pdfs("plFrame" + pdf("CurrentDesk")).location.href + backToList, true);
    }
    //UIG Fix
    pel("trListPane" + pdf("CurrentDesk")).style.display = "block";
    if (chrome) {
        var tdplWidth = pel("trListPane" + pdf("CurrentDesk")).offsetWidth;
        pel("tdPl" + pdf("CurrentDesk")).style.width = (pel("trListPane" + pdf("CurrentDesk")).offsetWidth) + "px";
        // el("tdPl" + CurrentDesk).style.width = $(window).width() + "px";
    }

    hideHelpStrip();
}

function toggleHelpTip() {
    if (ppsc().gblnHelpTip) {
        ppsc().gblnHelpTip = false
        hideHelpStrip();
    }
    else {
        ppsc().gblnHelpTip = true;
        showHelpStrip();
    }
    toggleHelpiconColor();
}

function toggleHelpiconColor() {
    if (ppsc().gblnHelpTip) {
        el("lnkHelp").style.color = "Green";
    }
    else {
        el("lnkHelp").style.color = "black";
    }
}

function toggleFavourite() {
    var sURL = document.location.href;
    var sPageTitle = getQueryStringValue(sURL, "activityname");
    //  var sPageTitle = getPageTitle();  
    if (el("iconFav").className == "icon-star")
        ppsc().toggleFavouritesList(true, sPageTitle)
    else
        ppsc().toggleFavouritesList(false, sPageTitle)
    toggleFavouriteColor();
}

function toggleFavouriteColor() {
    if (ppsc().isFavouriteActivity()) {
        el("iconFav").className = "icon-star";
    }
    else {

        el("iconFav").className = "icon-star-empty";

    }
}