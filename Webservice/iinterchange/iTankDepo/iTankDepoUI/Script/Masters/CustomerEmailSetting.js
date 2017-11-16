var HasChanges = false;

function initPage() {  
    bindCustomerEmailSettings();
}

function submitEmailInfo() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (ifgEmailSetting.Submit() == false) {
        return false;
    }
    submitCustomerEmailsetting();
    ppsc().closeModalWindow();
    return true;
}

function submitCustomerEmailsetting() {
    var oCallback = new Callback();
    oCallback.invoke("CustomerEmailSetting.aspx", "updateCustomerEmailSetting");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
        return false;
    }
    oCallback = null;
    ppsc().closeModalWindow();
    return true;
}

function bindCustomerEmailSettings() {
    var objEmailSetting = new ClientiFlexGrid("ifgEmailSetting");
    objEmailSetting.parameters.add("CustomerID", el('hdnCustomerID').value);
    objEmailSetting.DataBind();
}

function fnActiveCheck(obj) {
    var iRowIndex = ifgEmailSetting.CurrentRowIndex();
    var _Cols = ifgEmailSetting.Rows(iRowIndex).GetClientColumns();
    if (_Cols[2] == "" || _Cols[3] == "" ||_Cols[2] == null || _Cols[3] ==null ) {
        obj.checked = false;
        return false;
    }
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
}

function closeEmailInfo() {
    ppsc().closeModalWindow();
}

function fnFilter() {
    var iRowIndex = ifgEmailSetting.CurrentRowIndex();
    var _Cols = ifgEmailSetting.Rows(iRowIndex).GetClientColumns();
    _Cols[4] = " ";
    _Cols[6] = " ";
    ifgEmailSetting.Rows(iRowIndex).SetColumnValuesByIndex(2, "")
    ifgEmailSetting.Rows(iRowIndex).SetColumnValuesByIndex(3, "")
    if (_Cols[3] == "33") {
        ifgEmailSetting.Rows(iRowIndex).SetReadOnlyColumn(2, true);
        ifgEmailSetting.Rows(iRowIndex).SetReadOnlyColumn(3, true);
    }
    else if (_Cols[3] == "34") {
        ifgEmailSetting.Rows(iRowIndex).SetReadOnlyColumn(2, true);
        ifgEmailSetting.Rows(iRowIndex).SetReadOnlyColumn(3, false);
    }
    else if (_Cols[3] == "35") {
        ifgEmailSetting.Rows(iRowIndex).SetReadOnlyColumn(2, false);
        ifgEmailSetting.Rows(iRowIndex).SetReadOnlyColumn(3, true);
    }
    else if (_Cols[3] == "36") {
        ifgEmailSetting.Rows(iRowIndex).SetReadOnlyColumn(2, true);
        ifgEmailSetting.Rows(iRowIndex).SetReadOnlyColumn(3, false);
    }
}

function validateDay(oSrc, args) {
    var _rowI = ifgEmailSetting.rowIndex;
    var _col = ifgEmailSetting.Rows(_rowI).GetClientColumns();
    if (_col[3] == "35") {
        if (_col[4] == "" || _col[4] == null) {
            oSrc.errormessage = "Day Required";
            args.IsValid = false;
        }
    }
}

function validateEmailSettingDate(oSrc, args) {   
    var _rowI = ifgEmailSetting.rowIndex;
    var _col = ifgEmailSetting.Rows(_rowI).GetClientColumns();
    if (_col[3] == "36" || _col[3] == "34") {
        if (_col[6] == "" || _col[6] == null) {
            oSrc.errormessage = "Date Required";
            args.IsValid = false;
        }
    }
}