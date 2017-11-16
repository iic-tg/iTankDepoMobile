var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgRoute');



function initPage(sMode) {

    bindGrid();
    reSizePane();
    setDefaultValues();
    var sPageTitle = getPageTitle();
    el("hdnPageTitle").value = sPageTitle;
    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    el('iconFav').style.display = "none";
}

    function bindGrid() {
        var sPageTitle = getPageTitle();
        var ifgRoute = new ClientiFlexGrid("ifgRoute");
        ifgRoute.parameters.add("WFDATA", el("WFDATA").value);
        ifgRoute.DataBind();
        //Fix for BreadCrums
        // $("#spnHeader").text(sPageTitle);
        $("#spnHeader").text("Master >>Transport >>" + sPageTitle);
    }
    function submitPage() {
        Page_ClientValidate();
        if (!Page_IsValid) {
            return false;
        }
        //To submit the changes to server
        if (ifgRoute.Submit(true) == false) {
            return false;
        }
        else {
            if (getPageChanges()) {
                updateRoute();
            }
            else {
                showInfoMessage('No Changes to Save');
            }
        }
        return true;
    }

    //This method used to update/insert entered row to database
    function updateRoute() {
        var oCallback = new Callback();
        oCallback.add("WFDATA", el("WFDATA").value);
        oCallback.invoke("RouteDetail.aspx", "UpdateRoute");
        if (oCallback.getCallbackStatus()) {
            showInfoMessage(oCallback.getReturnValue("Message"));
            HasChanges = false;
            bindGrid();
            resetHasChanges("ifgRoute");
        }
        else {
            showErrorMessage(oCallback.getCallbackError());
        }
        oCallback = null;

    }
    function onAfterCB(param) {
        if (typeof (param["Delete"]) != 'undefined') {
            showWarningMessage(param["Delete"]);
        }
        else if (typeof (param["Update"]) != 'undefined' && param["Update"] != '') {
            showWarningMessage(param["Update"]);
        }
        else {
            hideMessage();
        }
    }
    function validateCode(oSrc, args) {
        var iRowIndex = ifgRoute.rowIndex;
        var oCols = ifgRoute.Rows(iRowIndex).GetClientColumns();
        var sCols = ifgRoute.Rows(iRowIndex).Columns();
        var sCode = oCols[0];

        var rowState = ifgRoute.ClientRowState();
        if (rowState != 'Added') {
            if (sCols[1] == sCode) {
                return false;
            }
        }

        var oCallback = new Callback();
        oCallback.add("Code", sCode);
        oCallback.add("GridIndex", ifgRoute.VirtualCurrentRowIndex());
        //Newly added code is available in existing data only in database.
        oCallback.add("RowState", rowState);
        oCallback.add("WFDATA", el("WFDATA").value);
        oCallback.invoke("RouteDetail.aspx", "ValidateCode");
        if (oCallback.getCallbackStatus()) {
            if (oCallback.getReturnValue("pkValid") == "true") {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }
        else {
            showErrorMessage(oCallback.getCallbackError());
        }
        oCallback = null;
        oCols = null;
    }
    function setDefaultValues(iCurrentIndex) {
        var sRowState = ifgRoute.ClientRowState();
        if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
            ifgRoute.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
        }
        if (ifgRoute.Rows().Count == "0")
            ifgRoute.Rows(0).SetColumnValuesByIndex(4, true);
        else if (ISNullorEmpty(iCurrentIndex) == false)
            ifgRoute.Rows(iCurrentIndex).SetColumnValuesByIndex(4, true);
        resetHasChanges("ifgRoute");
        return true;
    }
    function ValidatePickupLocation(oSrc, args) {
        var rIndex = ifgRoute.CurrentRowIndex();
        var cColumns = ifgRoute.Rows(rIndex).GetClientColumns();
        if ((cColumns[2] != "" && cColumns[2] != null) && (cColumns[2]==cColumns[4])) {
            oSrc.errormessage = "Pick Up Location and Drop Off Location cannot be same";
            args.IsValid = false;
            return;
        }
    }
    function ValidateDropOffLocation(oSrc, args) {
        var rIndex = ifgRoute.CurrentRowIndex();
        var cColumns = ifgRoute.Rows(rIndex).GetClientColumns();
        if (cColumns[4] == "" || cColumns[4] == null) {
            oSrc.errormessage = "Pick Up Location Required";
            args.IsValid = false;
            return;
        }
        if ((cColumns[2] != "" && cColumns[2] != null ) && (cColumns[2] == cColumns[4])) {
            oSrc.errormessage = "Pick Up Location and Drop Off Location cannot be same";
            args.IsValid = false;
            return;
        }
    }
//    function ValidateActivityLocation(oSrc, args) {
//        var rIndex = ifgRoute.CurrentRowIndex();
//        var cColumns = ifgRoute.Rows(rIndex).GetClientColumns();
//        if (cColumns[4] != "" && cColumns[6]=="") {
//            oSrc.errormessage = "Activity Location Required";
//            args.IsValid = false;
//            return;
//        }
//    }

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
                el("tabRoute").style.height = $(window.parent).height() - 240 + "px";
                if (el("ifgRoute") != null) {
                    el("ifgRoute").SetStaticHeaderHeight($(window.parent).height() - 295 + "px");
                }
            }
            else if ($(window.parent).height() < 768) {
                el("tabRoute").style.height = $(window.parent).height() - 350 + "px";
                if (el("ifgRoute") != null) {
                    el("ifgRoute").SetStaticHeaderHeight($(window.parent).height() - 450 + "px");
                }
            }
            else {
                el("tabRoute").style.height = $(window.parent).height() - 254 + "px";
                if (el("ifgRoute") != null) {
                    el("ifgRoute").SetStaticHeaderHeight($(window.parent).height() - 309 + "px");
                }
            }
        }

    }
