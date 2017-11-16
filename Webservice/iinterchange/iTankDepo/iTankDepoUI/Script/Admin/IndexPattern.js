var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgIndexPattern');
var _RowValidationFails = false;
if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}

function initPage(sMode) {
    var sPageTitle = getPageTitle();
    $("#spnHeader").text("Admin >>Index Pattern" + sPageTitle);
    reSizePane();
    setReadOnly('txtIndexPattern', true);
    setReadOnly('txtSplitChar', false);
    if (sMode == "new") {
        setPageMode("new");
        setPage_Values();
         el("chkActivebit").checked = true;
        el('btnFetch').innerText = "Fetch"
        setReadOnly('chkActivebit', true);
        fnsetReadOnly(false);
    }
    else {
        setReadOnly("lkpScreenName", true);
        if (el("hdnActiveBit").value == "True") {
            setPageMode("edit");
            setReadOnly('chkActivebit', false);
            bindIndexPatternGrid("edit");
            showDiv("divIndexPatternDetail");
            // hideDiv("hlnkFetch");
            el("btnFetch").style.display = "none";
            el("tdFetch").style.display = "none";
           // tdFetch
            if (el("hdnEditBit").value == "True") {
                fnsetReadOnly(true);
            }
            else {
                fnsetReadOnly(false);
            }
        }
        else {
            setPageMode("new");
            setPage_Values();
            el('btnFetch').innerText = "Fetch"
            el("chkActivebit").checked = true;
            setReadOnly('chkActivebit', true);
            fnsetReadOnly(false);
            showErrorMessage("This record not in active, please create new index pattern");
        }
    }
}

function bindIndexPatternDetail(mode) {
    var objGrid = new ClientiFlexGrid("ifgIndexPattern");
    objGrid.parameters.add("Mode", mode);
  //  objGrid.parameters.add("WFData", getWFDATA());
    objIndexPattern.parameters.add("Activebit", el("chkActivebit").checked);
    objIndexPattern.parameters.add("pageMode", getPageMode());
    objGrid.DataBind();
}

function submitPage() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges()) {
        var sMode = getPageMode();
        var txtIndexPattern = el("txtIndexPattern").value;
        if (ifgIndexPattern.Submit(true) == false || _RowValidationFails) {
            return false;
        }
        ifgIndexPattern.Submit();
        if (sMode == MODE_NEW) {
            if (txtIndexPattern.indexOf(el("txtSplitChar").value) == -1) {
                IndexPatternCreation();
            }
            //  ConfirmClick(sMode);
            IndexPatternCreation();
            CreateIndexPattern();
           
          
        }
        else if (sMode == MODE_EDIT) {
            if (txtIndexPattern.indexOf(el("txtSplitChar").value) == -1) {
                IndexPatternCreation();
            }
            //ConfirmClick(sMode);
            IndexPatternCreation();
            UpdateIndexPattern();
        }
    }
    else {
        showInfoMessage("No Changes to Save");
    }
    return true;
}

function CreateIndexPattern() {
    var IndexPatternCallback = new Callback();
    IndexPatternCallback.add("ScreenNameId", el("lkpScreenName").SelectedValues[0]);
    IndexPatternCallback.add("ScreenName", el("lkpScreenName").SelectedValues[1]);
    IndexPatternCallback.add("TableName", el("lkpScreenName").SelectedValues[2]);
     IndexPatternCallback.add("ResetBasisId", el("lkpResetBasis").SelectedValues[0]);
    IndexPatternCallback.add("IndexBasisId", el("lkpIndexBasis").SelectedValues[0]);
    //IndexPatternCallback.add("SequenceNoStart", el("txtSequenceNoStart").value);
    IndexPatternCallback.add("NoOfDigits", el("txtNoOfDigits").value);
    IndexPatternCallback.add("ResetBasisId", "");
    IndexPatternCallback.add("IndexBasisId", "");
    IndexPatternCallback.add("SequenceNoStart", "");
    IndexPatternCallback.add("NoOfDigits", "");
    IndexPatternCallback.add("SplitChar", el("txtSplitChar").value);
    IndexPatternCallback.add("Active", el("chkActivebit").checked)
    IndexPatternCallback.add("IndexPatternActual", el("txtIndexPattern").value);
    IndexPatternCallback.add("IndexPattern", el("hdnIndexPattern").value);
    IndexPatternCallback.invoke("IndexPattern.aspx", "CreateIndexPattern");
    if (IndexPatternCallback.getCallbackStatus()) {
        if (IndexPatternCallback.getReturnValue("Message") != "") {
            showInfoMessage(IndexPatternCallback.getReturnValue("Message"));
            setPageID(IndexPatternCallback.getReturnValue("ID"));
            setPageMode("edit");
            bindIndexPatternGrid("edit");
            showDiv("divIndexPatternDetail");
            setReadOnly('chkActivebit', false);
            fnsetReadOnly(true);
           setReadOnly('txtSplitChar', false);
            // hideDiv("hlnkFetch");
            el("btnFetch").style.display = "none";
            el("tdFetch").style.display = "none";
        } else {
            showErrorMessage(IndexPatternCallback.getCallbackError());
            return false;
        }
    }
    else {
        showErrorMessage(IndexPatternCallback.getCallbackError());
        return false;
    }
    IndexPatternCallback = null;
    HasChanges = false;
    resetHasChanges("ifgIndexPattern");
}

function UpdateIndexPattern() {
    var IndexPatternCallback = new Callback();
    IndexPatternCallback.add("ID", getPageID());
    IndexPatternCallback.add("ScreenNameId", el("lkpScreenName").SelectedValues[0]);
    IndexPatternCallback.add("ScreenName", el("lkpScreenName").SelectedValues[1]);
    IndexPatternCallback.add("TableName", el("lkpScreenName").SelectedValues[2]);
    IndexPatternCallback.add("ResetBasisId", el("lkpResetBasis").SelectedValues[0]);
    IndexPatternCallback.add("IndexBasisId", el("lkpIndexBasis").SelectedValues[0]);
   // IndexPatternCallback.add("SequenceNoStart", el("txtSequenceNoStart").value);
     IndexPatternCallback.add("NoOfDigits", el("txtNoOfDigits").value);
     IndexPatternCallback.add("ResetBasisId", "");
     IndexPatternCallback.add("IndexBasisId", "");
     IndexPatternCallback.add("SequenceNoStart", "");
     IndexPatternCallback.add("NoOfDigits", "");
    IndexPatternCallback.add("SplitChar", el("txtSplitChar").value);
    IndexPatternCallback.add("Active", el("chkActivebit").checked)
    IndexPatternCallback.add("IndexPatternActual", el("txtIndexPattern").value);
    IndexPatternCallback.add("IndexPattern", el("hdnIndexPattern").value);
    IndexPatternCallback.invoke("IndexPattern.aspx", "UpdateIndexPattern");
    if (IndexPatternCallback.getCallbackStatus()) {
        if (IndexPatternCallback.getReturnValue("Message") != "") {
            showInfoMessage(IndexPatternCallback.getReturnValue("Message"));
            setPageID(IndexPatternCallback.getReturnValue("ID"));
            setPageMode("edit");
            bindIndexPatternGrid("edit");
            showDiv("divIndexPatternDetail");
            if (el("chkActivebit").checked == false) {
                setReadOnly('chkActivebit', true);
                setReadOnly('txtSplitChar', true);
               // showSubmitButton(false);
            }
            else {
                setReadOnly('chkActivebit', false);
                setReadOnly('txtSplitChar', false);
                showSubmitButton(true);
            }
            //fnsetReadOnly(true);
        } else {
            showErrorMessage(IndexPatternCallback.getCallbackError());
            return false;
        }
    }
    else {
        showErrorMessage(IndexPatternCallback.getCallbackError());
        return false;
    }
    IndexPatternCallback = null;
    HasChanges = false;
    resetHasChanges("ifgIndexPattern");
}

function ApplyFilterIndex() {
    return "SCRN_ID=" + el("lkpScreenName").SelectedValues[0];
}

function ApplyParameterFilter() {
    return "DLFT_BT = 1 OR SCRN_ID=" + el("lkpScreenName").SelectedValues[0];
}

function SetIndexPatternField() {
    var index = ifgIndexPattern.rowIndex;
    var columns = ifgIndexPattern.Rows(index).GetClientColumns();
    var selectedParam = columns[1];
    // 4 - Sequence and 5 - depot 6-Customer
    if (selectedParam == "4" || selectedParam == "5" || selectedParam == "6") {
        ifgIndexPattern.Rows(index).SetReadOnlyColumn(1, true);

    }
    else {
        ifgIndexPattern.Rows(index).SetReadOnlyColumn(1, false);
    }

}

function ValidateDefaultValue(oSrc, args) {
    var index = ifgIndexPattern.rowIndex;
    var columns = ifgIndexPattern.Rows(index).GetClientColumns();
    var selectedParam = columns[1];
    if (selectedParam == "1") {
        var defaultvalue = columns[2];
        if (defaultvalue == "") {
            oSrc.errormessage = "Pre-Defined Value Required";
            args.IsValid = false;
        }
    }

    if (selectedParam == "2") {
        var month = columns[2];
        if (month != "") {
            if (isNaN(month)) {
                if (month != "MM" && month != "MMM") {
                    oSrc.errormessage = "Month Should Be In MM or MMM Format";
                    args.IsValid = false;
                }
            }
            else {
                oSrc.errormessage = "Month Should Be In MM or MMM Format";
                args.IsValid = false;
            }
        }
        else {
            oSrc.errormessage = "Month Format Required";
            args.IsValid = false;
        }
    }
    //Year Validate
    if (selectedParam == "3") {
        var year = columns[2];
        if (year != "") {
            if (isNaN(year)) {
                if (year != "YY" && year != "YYYY") {
                    oSrc.errormessage = "Year Should Be In YY or YYYY Format";
                    args.IsValid = false;
                }
            }
            else {
                oSrc.errormessage = "Year Should Be In YY or YYYY Format";
                args.IsValid = false;
            }
        }
        else {
            oSrc.errormessage = "Year Format Required";
            args.IsValid = false;
        }
    }
}

function ValidateScreen(oSrc, args) {
    var IndexPatternCallback = new Callback();
    IndexPatternCallback.add("ScreenNameId", el("lkpScreenName").SelectedValues[0]);
    IndexPatternCallback.invoke("IndexPattern.aspx", "ValidateScreen");
    if (IndexPatternCallback.getCallbackStatus()) {
        if (IndexPatternCallback.getReturnValue("Message") != "") {
            if (IndexPatternCallback.getReturnValue("Message") != "0") {
                oSrc.errormessage = "Already " + el("lkpScreenName").SelectedValues[01] + " Pattern Created";
                args.IsValid = false;
            }
        } else {
            showErrorMessage(IndexPatternCallback.getCallbackError());
            return false;
        }
        IndexPatternCallback = null;
    }
}

function filterScreen() {
    var qry = "ACTVTY_ID NOT IN (SELECT SCRN_ID FROM INDEX_PATTERN WHERE ACTV_BT <> 0)";
    return qry;
}

function ValidateParameter(oSrc, args) {
    var rIndex = ifgIndexPattern.VirtualCurrentRowIndex();
    var cols = ifgIndexPattern.Rows(rIndex).GetClientColumns();
    ifgIndexPattern.Rows(rIndex).SetColumnValuesByIndex(1, "");
    var i;
    var paramId = cols[1];
    var icount = ifgIndexPattern.Rows().Count
    var colscheck;
    if (icount > 0) {
        for (i = 0; i < icount; i++) {
            if (rIndex != i) {
                colscheck = ifgIndexPattern.Rows(i).GetClientColumns();
                if (colscheck[1] != "1") {
                    if (paramId == colscheck[1]) {
                        args.IsValid = false;
                        oSrc.errormessage = "This parameter already exists";
                        return false;
                    }
                }
            }
        }
    }
}

function setPage_Values() {
    clearLookupValues("lkpScreenName");
    //clearTextValues("txtSequenceNoStart");
    clearTextValues("txtNoOfDigits");
    clearTextValues("txtIndexPattern");
    clearTextValues("txtSplitChar");
   clearLookupValues("lkpResetBasis");
    clearLookupValues("lkpIndexBasis");
    el("lkpScreenName").SelectedValues[0] = "";
    el("lkpResetBasis").SelectedValues[0] = "";
    el("lkpIndexBasis").SelectedValues[0] = "";
    resetHasChanges("ifgIndexPattern");
    //  showDiv("hlnkFetch");
    el('btnFetch').style.display = "block";
    el('tdFetch').style.display = "block"; 
    el('btnFetch').innerText = "Fetch"
    hideDiv("divIndexPatternDetail");
    bindIndexPatternGrid("new");
    //fnsetReadOnly(false);
    setFocusToField("lkpScreenName");
}

function bindIndexPatternGrid(mode) {
    var objIndexPattern = new ClientiFlexGrid("ifgIndexPattern");
    objIndexPattern.parameters.add("mode", mode);
    objIndexPattern.parameters.add("Activebit", el("chkActivebit").checked);
    objIndexPattern.parameters.add("pageMode", getPageMode());
    objIndexPattern.DataBind();
}

////function onBeforeCallBackIfgIndexPattern(mode,param) {
////    param.add("splitChar", el("txtSplitChar").value);
////}

function onAfterCallBackIfgIndexPattern(param) {
    IndexPatternCreation();
   }

function IndexPatternCreation() {
    var rIndex;
    var indexPatternActual = "";
    var indexPattern = "";
    var rCount = ifgIndexPattern.Rows().Count;
    var splitChar = el("txtSplitChar").value;
    document.body.click();
    if (rCount > 0) {
        for (rIndex = 0; rIndex < rCount; rIndex++) {
            var cols = ifgIndexPattern.Rows(rIndex).GetClientColumns();
            var paramId = cols[1];
         //   1 - Pre-Defined Values , 2 - Month , 3 - Year 
            if (paramId == "1" || paramId == "2" || paramId == "3" ) {
                indexPatternActual += cols[2];
                indexPattern += cols[2];
            }
            else {
                indexPatternActual += cols[0];
                if (cols[0] == "Sequence No") {
                    indexPattern += "MAXNO";
                }
                else if (cols[0] == "Depot Code") {
                    indexPattern += "DPTCD";
                }
                else {
                    indexPattern += "CSTMR_CD";
                }
                //indexPattern += ifgIndexPattern.Rows(rIndex).Columns()[7];
                //cols[5];
                        }
           
            if (splitChar == "") {
                indexPattern += "$";
            }
            else {
                indexPattern += splitChar;
                indexPatternActual += splitChar;
            }
        }
        if (splitChar != "") {
            indexPatternActual = indexPatternActual.substring(0, indexPatternActual.length - 1);
        }
        indexPattern = indexPattern.substring(0, indexPattern.length - 1);
        el("txtIndexPattern").value = indexPatternActual;
        el("hdnIndexPattern").value = indexPattern;
    }
}
function fnsetReadOnly(blnValue) {
    setReadOnly('lkpScreenName', blnValue);
    //setReadOnly('txtSequenceNoStart', blnValue);
    setReadOnly('txtNoOfDigits', blnValue);
    setReadOnly('lkpResetBasis', blnValue);
    setReadOnly('lkpIndexBasis', blnValue);
}



function FetchData() {
    if (el('btnFetch').innerText == "Fetch") {
        var sMode = getPageMode();
        Page_ClientValidate();
        if (!Page_IsValid) {
            return false;
        }
        bindIndexPatternGrid("edit");
        showDiv("divIndexPatternDetail");
        el('btnFetch').innerText = "Reset";
        setReadOnly("lkpScreenName", true);
    } else {
        //setReadOnly('txtSplitChar', false);
        setReadOnly("lkpScreenName", false);
        setPage_Values();
    }
    if ($(window.parent).height() < 680) {
        if (!chrome) {
            el("divIndexPatternDetail").style.height = $(window.parent).height() - 430 + "px";
        }
        else {
            el("divIndexPatternDetail").style.height = $(window.parent).height() - 440 + "px";
        }
    }
}


function reSizePane() {
    if ($(window) != null) {
        if ($(window.parent).height() < 680) {
            if (!chrome) {
                el("divIndexPatternDetail").style.height = $(window.parent).height() - 423 + "px";
            }
            else {
                el("divIndexPatternDetail").style.height = $(window.parent).height() - 430 + "px";
            }
            if (el("ifgIndexPattern") != null) {
                if (!chrome) {
                    el("ifgIndexPattern").SetStaticHeaderHeight($(window.parent).height() - 479 + "px");
                }
                else {
                    el("ifgIndexPattern").SetStaticHeaderHeight($(window.parent).height() - 499 + "px");
                }
            }
        }
        else if ($(window.parent).height() < 768) {
         //   showDiv("divIndexPatternDetail");
            el("divIndexPatternDetail").style.height = $(window.parent).height() - 580 + "px";
           // hideDiv("divIndexPatternDetail");
            if (el("ifgIndexPattern") != null) {
                el("ifgIndexPattern").SetStaticHeaderHeight($(window.parent).height() - 680 + "px");
            }

        }
        else {
            //showDiv("divIndexPatternDetail");
            el("divIndexPatternDetail").style.height = $(window.parent).height() - 532 + "px";
            //hideDiv("divIndexPatternDetail");
            if (el("ifgIndexPattern") != null) {
                el("ifgIndexPattern").SetStaticHeaderHeight($(window.parent).height() - 584 + "px");
            }

        }
    }

}