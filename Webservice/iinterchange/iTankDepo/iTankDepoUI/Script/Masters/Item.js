var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgSubItem');
var _RowValidationFails = false;

if (window.$) {
    $().ready(function () {

        reSizePane();
    });
}

function initPage(sMode) {
    reSizePane();
    if (sMode == MODE_NEW) {
        clearTextValues("txtItemCode");
        clearTextValues("txtItemDescription");
        var configValue = getConfigSetting('044') 
        if (configValue != undefined) {
            document.getElementById("txtItemDescription").title = configValue + " Description";
            document.getElementById("txtItemCode").title = configValue+ " Code";
        }
        setReadOnly("txtItemCode", false);
        el("chkActiveBit").checked = true;
        setPageID("0");
        setPageMode("new");
        setFocus();
        resetValidators();
        bindSubItem();
        setDefaultValues();
    }
    else {
        showSubmitButton(true);
        resetValidatorByGroup("tabItem");
        setReadOnly("txtItemCode", true);
        setPageMode("edit");
        setFocus();
        resetValidators();
        bindSubItem();
        //makeReadOnly();
    }
    el('iconFav').style.display = "none";
    $('.btncorner').corner();
}

function submitPage() {
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }

    if (getPageChanges()) {
        var sMode = getPageMode();
        if (ifgSubItem.Submit(true) == false || _RowValidationFails) {
            return false;
        }
        if (sMode == MODE_NEW) {
            createItem();
        }
        else if (sMode == MODE_EDIT) {
            updateItem();
        }
    }
    else {
        showInfoMessage('No Changes to Save');
        setFocus();
    }
    return true;
}

function bindSubItem() {
    var ifgSubItem = new ClientiFlexGrid("ifgSubItem");
    ifgSubItem.parameters.add("WFDATA", el("WFDATA").value);
    ifgSubItem.parameters.add("ItemID", getPageID());
    ifgSubItem.DataBind();
    $('.btncorner').corner();
}

function setFocus() {
    var sMode = getPageMode();
    if (sMode == MODE_NEW) {
        setFocusToField("txtItemCode");
    }
    else if (sMode == MODE_EDIT) {
        setFocusToField("txtItemDescription");
    }
}

function validateItemCode(oSrc, args) {
    var oCallback = new Callback();
    var valid;
    oCallback.add("Code", el("txtItemCode").value);

    oCallback.invoke("Item.aspx", "ValidateCode");

    if (oCallback.getCallbackStatus()) {
        valid = oCallback.getReturnValue("valid");
        if (valid == "true") {
            oSrc.errormessage = oCallback.getReturnValue("message");
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = oCallback.getReturnValue("message");
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function validateSubItemCode(oSrc, args) {
    var iRowIndex = ifgSubItem.CurrentRowIndex();
    var oCols = ifgSubItem.Rows(iRowIndex).GetClientColumns();
    var sCols = ifgSubItem.Rows(iRowIndex).Columns();
    var sCode = oCols[0];

    var rowState = ifgSubItem.ClientRowState();
    if (rowState != 'Added') {
        if (sCols[1] == sCode) {
            return false;
        }
    }
    var oCallback = new Callback();
    var valid;
    oCallback.add("SubItemCode", args.Value);
    oCallback.add("ItemID", getPageID());
    oCallback.add("TableName", getTableName());
    oCallback.invoke("Item.aspx", "ValidateSubItemCode");
    if (oCallback.getCallbackStatus()) {
        valid = oCallback.getReturnValue("valid");
        if (valid == "true") {
            oSrc.errormessage = oCallback.getReturnValue("message");
            args.IsValid = true;
        }
        else {
            oSrc.errormessage = oCallback.getReturnValue("message");
            args.IsValid = false;
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function createItem() {
    var oCallback = new Callback();
    oCallback.add("bv_strItemCode", el("txtItemCode").value);
    oCallback.add("bv_strItemDescription", el("txtItemDescription").value);
    oCallback.add("bv_blnActiveBit", el("chkActiveBit").checked);
    oCallback.add("PageMode", getPageMode());
    oCallback.add("wfData", el("WFDATA").value);

    oCallback.invoke("Item.aspx", "CreateItem");
    if (oCallback.getCallbackStatus()) {
        setPageMode(MODE_EDIT);
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        setPageID(oCallback.getReturnValue("ID"));
        bindSubItem();
        resetHasChanges("ifgSubItem");
        setReadOnly("txtItemCode", true);
        setFocus();

    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function updateItem() {
    var oCallback = new Callback();
    oCallback.add("ID", getPageID());
    oCallback.add("bv_strItemCode", el("txtItemCode").value);
    oCallback.add("bv_strItemDescription", el("txtItemDescription").value);
    oCallback.add("bv_blnActiveBit", el("chkActiveBit").checked);
    oCallback.add("PageMode", getPageMode());
    oCallback.add("wfData", el("WFDATA").value);

    oCallback.invoke("Item.aspx", "UpdateItem");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        bindSubItem();
        resetHasChanges("ifgSubItem");
        setReadOnly("txtItemCode", true);
        setFocus();
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
}

function resetGridChanges() {
    resetHasChanges("ifgSubItem");
}

function OnBeforeCallBack(mode, param) {
    if (mode == "Update" || mode == "Insert" || mode == "Delete") {
        param.add("mode", getPageMode());
        param.add("ItemID", getPageID());
    }   
}

function OnAfterCallBack(param, mode) {
    if (mode == "Delete") {
        if (typeof (param["Delete"]) != 'undefined') {
            showWarningMessage(param["Delete"]);
            return;
        }
    }
    if (mode == "Add" || mode == "Edit" || mode == "Delete") {
        if (typeof (param["Duplicate"]) != 'undefined') {
            showErrorMessage(param["Duplicate"]);
        }
    }   
}


//function makeReadOnly() {
//    var _index = ifgSubItem.Rows().Count;
//    for (var i = 0; i < _index; i++) {
//        ifgSubItem.Rows(i).SetReadOnlyColumn(0, true);
//    }
//}

function setDefaultValues(iCurrentIndex) {
    var sRowState = ifgSubItem.ClientRowState();
    if (sRowState == "Added" && ISNullorEmpty(iCurrentIndex) == false) {
        ifgSubItem.Rows(iCurrentIndex).SetReadOnlyColumn(0, false);
    }
    if (ifgSubItem.Rows().Count == "0")
        ifgSubItem.Rows(0).SetColumnValuesByIndex(2, true);
    else if (ISNullorEmpty(iCurrentIndex) == false)
        ifgSubItem.Rows(iCurrentIndex).SetColumnValuesByIndex(2, true);
    resetHasChanges("ifgSubItem");

    return true;
}


//$(window.parent).resize(function () {
//    reSizePane();
//});
function reSizePane() {
    if ($(window.parent).height() < 680) {
        el("tabItem").style.height = $(window.parent).height() - 366 + "px";
        if (el("ifgSubItem") != null) {
            el("ifgSubItem").SetStaticHeaderHeight($(window.parent).height() - 477 + "px");
        }
    }
        //This chage is for IE starts here
    else if ($(window.parent).height() < 710) {
        el("tabItem").style.height = $(window.parent).height() - 419 + "px";
        if (el("ifgSubItem") != null) {
            el("ifgSubItem").SetStaticHeaderHeight($(window.parent).height() - 559 + "px");
        }
    }
    else if (!chrome && $(window.parent).height() < 850) {
        el("tabItem").style.height = $(window.parent).height() - 493 + "px";
        if (el("ifgSubItem") != null) {
            el("ifgSubItem").SetStaticHeaderHeight($(window.parent).height() - 632 + "px");
        }
    }
        //Ends here
    else {
        el("tabItem").style.height = $(window.parent).height() - 476 + "px";
        if (el("ifgSubItem") != null) {
            el("ifgSubItem").SetStaticHeaderHeight($(window.parent).height() - 579 + "px");
        }
    }

}
