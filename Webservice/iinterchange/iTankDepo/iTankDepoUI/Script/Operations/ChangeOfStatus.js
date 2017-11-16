var HasChanges = false;
var vrGridIds = new Array('ITab1_0;ifgEquipmentDetail');
var _RowValidationFails = false;
var HasNoteChanges = false;
var Description = "";



//This function is used to intialize the page.
function initPage(sMode) {

    el("lstBack").style.display = "none";
    el("lstFirst").style.display = "none";
    el("lstPrev").style.display = "none";
    el("lstNext").style.display = "none";
    el("lstLast").style.display = "none";
    resetValidators();
    el("divEquipmentDetail").style.display = "none";
    el("divStatusDetail").style.display = "none";
    reSizePane();
}

function fetch() {
    GetLookupChanges();
    if (el('lkpSearchStatus').SelectedValues[0] == '' && el('lkpCustomer').value != '' && el('txtEquipmentNo').value == '') {
        showErrorMessage('Current Status or Equipment No is required to perform Change of Status Operation');
        setFocusToField("lkpSearchStatus");
    }
    else if (el('lkpSearchStatus').SelectedValues[0] == '' && el('txtEquipmentNo').value == '') {
        showErrorMessage('Enter Current Status');
        setFocusToField("lkpSearchStatus");
    }
    else {
        // Converted all .iinerText to Settext and GetText method - UIG Fix in chrome for the Issue No :57
        if (getText(el('btnSearch')) == "Reset") {
            clearTextValues("lkpSearchStatus");
            el('lkpSearchStatus').SelectedValues[0] = "";
            clearTextValues("txtEquipmentNo");
            clearLookupValues("lkpCustomer");
            clearLookupValues("lkpStatus");
            clearTextValues("dateStatus");
            clearTextValues("txtRemarks");
            clearTextValues("txtYardLocation");
            setReadOnly("lkpCustomer", false);
            setReadOnly("lkpSearchStatus", false);
            setReadOnly("txtEquipmentNo", false);
            if (ifgEquipmentDetail.Submit() == false || _RowValidationFails) {
                return false;
            }
            el("divEquipmentDetail").style.display = "none";
            el("divStatusDetail").style.display = "none";
            el("divRecordNotFound").style.display = "none";
            resetValidators();
            setText(el('btnSearch'), "Search");
            //el('btnSearch').innerText = " Search";
            ResetLockedRecords();
        }
        else if (el('lkpSearchStatus').SelectedValues[0] != "" || el('lkpCustomer').SelectedValues[0] != "" || el('txtEquipmentNo').value) {
            clearLookupValues("lkpStatus");
            clearTextValues("dateStatus");
            clearTextValues("txtRemarks");
            clearTextValues("txtYardLocation");
            bindEquipmentDetail("edit");
            var rowCount = ifgEquipmentDetail.Rows().Count;

            if (rowCount <= 0) {
                //el('btnSearch').innerText = " Reset";
                setText(el('btnSearch'), "Reset");
            }
            else if (rowCount > 0) {
                var cCols = ifgEquipmentDetail.Rows(0).Columns();
                el('lkpSearchStatus').SelectedValues[0] = cCols[12];
                el('lkpSearchStatus').value = cCols[13];
            }
            setFocusToField("lkpStatus");
            setReadOnly("lkpCustomer", true);
            setReadOnly("lkpSearchStatus", true);
            setReadOnly("txtEquipmentNo", true);
            HasChanges = false;
        }
    }
    reSizePane();
}

//This function is used to submit the page to the server.
function submitPage() {
    if (el("divEquipmentDetail").style.display == 'none') {
        showErrorMessage('Current Status or Equipment No is required to perform Change of Status Operation');
        setFocusToField("lkpSearchStatus");
        return false;
    }
    GetLookupChanges();
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (getPageChanges() || HasNoteChanges) {
        if (ifgEquipmentDetail.Submit() == false || _RowValidationFails) {
            return false;
        }
        return updateChangeOfStatus();
    }
    else {
        showInfoMessage('No Changes to Save');
    }
    return true;
}

//This function is used to Update the changes to the server. 
function updateChangeOfStatus() {
    var oCallback = new Callback();
    oCallback.add("WFData", getWFDATA());
    oCallback.invoke("ChangeOfStatus.aspx", "updateChangeOfStatus");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage(oCallback.getReturnValue("Message"));
        HasChanges = false;
        resetHasChanges("ifgEquipmentDetail");
        bindEquipmentDetail("edit");
        var rowCount = ifgEquipmentDetail.Rows().Count;
        if (rowCount == 0) {
            //el('btnSearch').innerText = "Search";
            setText(el('btnSearch'), "Search");
            setReadOnly('lkpSearchStatus', false);
            clearLookupValues("lkpSearchStatus");
            clearLookupValues("lkpCustomer");
            setReadOnly('lkpCustomer', false);
            setReadOnly('txtEquipmentNo', false);
            clearTextValues("txtEquipmentNo");
            initPage("new");
        }

    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    return true;
}

//This function is used to bind the Equipment Detail Grid.
function bindEquipmentDetail(mode) {
    var objGrid = new ClientiFlexGrid("ifgEquipmentDetail");
    objGrid.parameters.add("StatusID", el('lkpSearchStatus').SelectedValues[0]);
    objGrid.parameters.add("CustomerID", el('lkpCustomer').SelectedValues[0]);
    objGrid.parameters.add("EquipmentNo", el('txtEquipmentNo').value);
    objGrid.parameters.add("Remarks", "");
    objGrid.parameters.add("YardLocation", "");
    objGrid.parameters.add("StatusDate", "");
    objGrid.parameters.add("ToStatus", "");
    objGrid.parameters.add("selectAll", "");
    objGrid.parameters.add("ToStatusID", "");
    objGrid.parameters.add("checkselect", "");
    objGrid.parameters.add("Mode", mode);
    objGrid.DataBind();
}

function ifgEquipmentDetailOnAfterCB(param) {
    var norecordsfound = param["norecordsfound"];
    if (typeof (param["validateEquipment"]) != 'undefined') {
        showErrorMessage("Previous Activity Date is less than the Current Status Date for the specified Equipment (" + param["validateEquipment"] + ")");
        return false;
    }
    if (typeof (param["filter"]) != 'undefined') {
        if (param["filter"] == "INS") {
            el('hdnStatusCode').value = "CLN";
        } else {
            el('hdnStatusCode').value = param["filter"];
        }
    }
    if (norecordsfound == "True") {
        el("divEquipmentDetail").style.display = "none";
        el("divStatusDetail").style.display = "none";
        el("divRecordNotFound").style.display = "block";
        // el('btnSearch').innerText = " Search";
        setText(el('btnSearch'), "Search");
    }
    else {

        if (typeof (param["LockWarningMessage"]) != 'undefined' && param["LockWarningMessage"] != "") {
            showWarningMessage(param["LockWarningMessage"]);
        }
        el("divEquipmentDetail").style.display = "block";
        el("divStatusDetail").style.display = "block";
        el("divRecordNotFound").style.display = "none";
        //el('btnSearch').innerText = " Reset";
        setText(el('btnSearch'), "Reset");
        var current_date = el('hdnCurrentDate').value;
        if (el('dateStatus').value == "") {
            el('dateStatus').value = current_date;
        }
    }

}

function SelectAll(obj) {
    var icount;
    var seleactAll = obj.checked;
    if (obj.checked) {
        if (el('lkpStatus').value != '') {
            if (el('dateStatus').value == '') {
                showErrorMessage('To Status Date Required');
                setFocusToField("dateStatus");
                return false;
            }

            if (DateCompare(el("dateStatus").value, el('hdnCurrentDate').value)) {
                showErrorMessage('Status Date should not be greater than current date');
                setFocusToField("dateStatus");
                return false;
            }

            var objGrid = new ClientiFlexGrid("ifgEquipmentDetail");
            objGrid.parameters.add("Mode", getPageMode());
            objGrid.parameters.add("StatusID", el('lkpSearchStatus').SelectedValues[0]);
            objGrid.parameters.add("CustomerID", el('lkpCustomer').SelectedValues[0]);
            objGrid.parameters.add("EquipmentNo", el('txtEquipmentNo').value);
            objGrid.parameters.add("ToStatusID", el('lkpStatus').SelectedValues[0]);
            objGrid.parameters.add("ToStatus", el('lkpStatus').SelectedValues[1]);
            objGrid.parameters.add("selectAll", el('chbSelectAll').checked);
            objGrid.parameters.add("Remarks", el('txtRemarks').value);
            objGrid.parameters.add("YardLocation", el('txtYardLocation').value);
            objGrid.parameters.add("StatusDate", el('dateStatus').value);
            objGrid.parameters.add("checkselect", "true");
            objGrid.DataBind();
            HasChanges = true;
            return true;
        }
        else {
            showErrorMessage('To Status Required');
            setFocusToField("lkpStatus");
            return false;
        }
    }
    else {
        var objGrid = new ClientiFlexGrid("ifgEquipmentDetail");
        objGrid.parameters.add("Mode", getPageMode());
        objGrid.parameters.add("StatusID", el('lkpSearchStatus').SelectedValues[0]);
        objGrid.parameters.add("CustomerID", el('lkpCustomer').SelectedValues[0]);
        objGrid.parameters.add("EquipmentNo", el('txtEquipmentNo').value);
        objGrid.parameters.add("ToStatusID", el('lkpStatus').SelectedValues[0]);
        objGrid.parameters.add("ToStatus", el('lkpStatus').SelectedValues[1]);
        objGrid.parameters.add("selectAll", el('chbSelectAll').checked);
        objGrid.parameters.add("Remarks", el('txtRemarks').value);
        objGrid.parameters.add("YardLocation", el('txtYardLocation').value);
        objGrid.parameters.add("StatusDate", el('dateStatus').value);
        objGrid.parameters.add("checkselect", "false");
        objGrid.DataBind();
    }

}

function applyFilter() {
    var rIndex = ifgEquipmentDetail.VirtualCurrentRowIndex();
    var cols = ifgEquipmentDetail.Rows(rIndex).Columns();
    if (el('hdnStatusCode').value != "") {
        return " EQPMNT_STTS_CD IN (" + el('hdnStatusCode').value + ")"
    }

}

function updateStatus(obj, equipmentNo) {

    var rIndex = ifgEquipmentDetail.CurrentRowIndex();
    var cols = ifgEquipmentDetail.Rows(rIndex).GetClientColumns();
    if (obj.checked) {
        if (el('lkpStatus').value != '' || cols[9] != '') {

            if (el('dateStatus').value == '' && (cols[11] == '' || cols[11] == null)) {
                showErrorMessage('To Status Date Required');
                setFocusToField("dateStatus");
                return false;
            }
            if (DateCompare(el("dateStatus").value, el('hdnCurrentDate').value)) {
                showErrorMessage('Status Date should not be greater than current date');
                setFocusToField("dateStatus");
                return false;
            }
            if (el("txtYardLocation").value != "") {
                ifgEquipmentDetail.Rows(rIndex).SetColumnValuesByIndex(7, el("txtYardLocation").value);
            }
            if (el("txtRemarks").value != "") {
                ifgEquipmentDetail.Rows(rIndex).SetColumnValuesByIndex(8, el("txtRemarks").value);
            }
            if (el("lkpStatus").SelectedValues[1] != "") {
                ifgEquipmentDetail.Rows(rIndex).SetColumnValuesByIndex(9, new Array(el("lkpStatus").SelectedValues[1], el("lkpStatus").SelectedValues[0]));
                // ifgEquipmentDetail.Rows(rIndex).SetFocusInColumn(9);
            }

            else {
                ifgEquipmentDetail.Rows(rIndex).SetFocusInColumn(9);
                //Locking
                var sresult = COSlockData(obj, equipmentNo)
                if (sresult != true) {
                    obj.checked = false;
                    showErrorMessage(sresult);
                }
                //  
                return true;
            }
            //UIG Fix
            if (cols[11] != "" && cols[11] != null) {
                if ((DateCompareEqual(cols[11], cols[6])) == false) {
                    showErrorMessage("To Status Date should be greater than or equal to Current Status Date (" + cols[6] + ")");
                    ifgEquipmentDetail.Rows(rIndex).SetColumnValuesByIndex(11, false);
                    return false;
                }
            }
            else if (el('dateStatus').value != "" && el('dateStatus').value != null) {
                if ((DateCompareEqual(el('dateStatus').value, cols[6])) == false) {
                    showErrorMessage("To Status Date should be greater than or equal to Current Status Date (" + cols[6] + ")");
                    ifgEquipmentDetail.Rows(rIndex).SetColumnValuesByIndex(11, false);
                    return false;
                }
            }
            if (cols[11] == '' || cols[11] == null) {
                ifgEquipmentDetail.Rows(rIndex).SetColumnValuesByIndex(10, el("dateStatus").value);
            }
        }
        else {
            showErrorMessage('To Status Required');
            setFocusToField("lkpStatus");
            return false;
        }
    }

    else {
        resetValidators();
    }

    //Locking
    var sresult = COSlockData(obj, equipmentNo)
    if (sresult != true) {
        obj.checked = false;
        showErrorMessage(sresult);
    }
    //  
}

function validateStatusDate(oSrc, args) {
    var rIndex = ifgEquipmentDetail.CurrentRowIndex();
    var cols = ifgEquipmentDetail.Rows(rIndex).GetClientColumns();
    if (DateCompareEqual(args.Value, cols[6])) {
        args.IsValid = true;
        return;
    }
    else {
        args.IsValid = false;
        oSrc.errormessage = "To Status Date should be greater than or equal to Current Status Date (" + cols[6] + ")";
        return;
    }
}

function OnChangeStatus(obj) {
    if (obj.value != '') {
        el('hdnStatusCode').value = obj.value;
    }
}

// Lock Implementation 
function COSlockData(obj, strEquipmentNo) {
    var oCallback = new Callback();
    oCallback.add("CheckBit", obj.checked);
    oCallback.add("EquipmentNo", strEquipmentNo);
    oCallback.add("EquipmentStatus", el('lkpSearchStatus').value);
    oCallback.add("LockBit", "");
    oCallback.invoke("ChangeOfStatus.aspx", "COSlockData");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("ErrorMessage") != "" && oCallback.getReturnValue("ErrorMessage") != "") {//Chrome Fix
            obj.checked = false;
            var errorMessage = oCallback.getReturnValue("ErrorMessage");

            if (oCallback.getReturnValue("Activity") != "") {
                errorMessage = errorMessage + " <B> (Activity : " + oCallback.getReturnValue("Activity") + ")</B>";
            }

            //            showErrorMessage(errorMessage);
            return errorMessage;

        } else {

            return true;
        }

    }
    else {
        return true;
    }
    oCallback = null;
}

function ResetLockedRecords() {
    var oCallback = new Callback();
    oCallback.invoke("ChangeOfStatus.aspx", "ResetLockedRecords");
    if (oCallback.getCallbackStatus())
    { }

}

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
        if ($(window.parent)) {
            if ($(window.parent).height() < 680) {
                el("tabChangeofStatus").style.height = $(window.parent).height() - 230 + "px";
                if (el("ifgEquipmentDetail") != null) {
                    if (!chrome) {
                        el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 409 + "px");
                    }
                    else {
                        el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 429 + "px");
                    }
                }
            }
            else if ($(window.parent).height() < 768) {
                el("tabChangeofStatus").style.height = $(window.parent).height() - 325 + "px";
                if (el("ifgEquipmentDetail") != null) {
                    el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 550 + "px");;
                }
            }
            else {
                el("tabChangeofStatus").style.height = $(window.parent).height() - 239 + "px";
                if (el("ifgEquipmentDetail") != null) {
                    if (!chrome) {
                        el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 415 + "px");
                    }
                    else {
                        el("ifgEquipmentDetail").SetStaticHeaderHeight($(window.parent).height() - 440 + "px");
                    }
                }
            }
        }
    }
}

