var _debug = false;
//var _ogridver = "3.0.4363.23113//290820121527";
//var _gridver = "4.0.0.1//031020131716";
//Initialize Grid
function Init_Grid(_id, _aa, _ad, _ae, pI, pS, hR, rC, aS, sHH, aF, aR, aBt, dBt, sBt, cBt, scBt, pBt, rBt, sDv, aRCP, aBIC, dBIC, cBIC, sCBIC) {
    if (document.addEventListener)
        document.addEventListener("DOMContentLoaded", new Function("Init_GridRS('" + _id + "','" + _aa + "','" + _ad + "','" + _ae + "','" + pI + "','" + pS + "'," + null + ",'" + hR + "','" + rC + "','" + aS + "','" + sHH + "','" + aF + "'," + null + "," + null + "," + null + "," + null + ",'" + aR + "','" + aBt + "','" + dBt + "','" + sBt + "','" + cBt + "','" + scBt + "','" + pBt + "','" + rBt + "','" + sDv + "','" + aRCP + "','" + aBIC + "','" + dBIC + "','" + cBIC + "','" + sCBIC + "');"), false);
    else
        window.document.attachEvent("onreadystatechange", new Function("Init_GridRS('" + _id + "','" + _aa + "','" + _ad + "','" + _ae + "','" + pI + "','" + pS + "'," + null + ",'" + hR + "','" + rC + "','" + aS + "','" + sHH + "','" + aF + "'," + null + "," + null + "," + null + "," + null + ",'" + aR + "','" + aBt + "','" + dBt + "','" + sBt + "','" + cBt + "','" + scBt + "','" + pBt + "','" + rBt + "','" + sDv + "','" + aRCP + "','" + aBIC + "','" + dBIC + "','" + cBIC + "','" + sCBIC + "');"));
}
//ID,AllowAdd,AllowDelete,AllowEdit,PageIndex,PageSize,PanelElement,Headerrows,RowCount,AllowStatic,StaticHeaderHeight,AllowFilter,ParentRowIndex,AllowRefresh,Context,HasChanges,FirstPageIndex,AllowRefresh,AddBtnTxt,DelBtnTxt,SrhBtnTxt,ClrBtnTxt,SrhCanBtnTxt,PrintBtnTxt,RefBtnTxt,SetDefaultValuesonAdd,AddRowsOnCurrentPage,AddButtonIconClass,DeleteButtonIconClass,ClearButtonIconClass,SearchCancelButtonIconClass
function Init_GridRS(_id, _aa, _ad, _ae, pI, pS, pE, hR, rC, aS, sHH, aF, pRI, context, _hc, fPI, aR, aBt, dBt, sBt, cBt, scBt, pBt, rBt, sDv, aRCP, aBIC, dBIC, cBIC, sCBIC) {
    if (document.readyState == 'interactive' || document.readyState == 'complete') {
        //var fgObj = getIObject(_id);
        var fgObj = document.all[_id];
        if (fgObj == null)
            return;
        fgObj.editCtrl = "";
        fgObj.readOnly = false;
        fgObj.hdrRows = hR;
        fgObj.Grid_IsValid = true;
        fgObj.edtColumns = "";
        fgObj.edtValues = "";
        fgObj.oldValue = "";
        fgObj.newValue = "";
        fgObj.Search = false;
        if (_hc)
            fgObj.hasChanges = _hc;
        else
            fgObj.hasChanges = false;
        fgObj.hasRowChanges = false;
        fgObj.allowAdd = GetBoolFromString(_aa);
        fgObj.allowDelete = GetBoolFromString(_ad);
        fgObj.allowEdit = GetBoolFromString(_ae);
        fgObj.allowSearch = GetBoolFromString(aS);
        fgObj.allowRefresh = GetBoolFromString(aR);
        fgObj.aF = GetBoolFromString(aF);
        fgObj.sHH = sHH;
        fgObj.rC = rC;
        fgObj.rowIndex = "";
        if (rC == 0) {
            fgObj.mode = "Add";
            fgObj.rowState = "Added";
            if (fgObj.allowAdd)
                fgObj.rowIndex = 0;
            else
                fgObj.rowIndex = "";
        }
        else {
            fgObj.mode = "";
            fgObj.rowState = "";
        }
        fgObj.parentRowIndex = pRI;
        fgObj.u = unescape(_wcU);
        fgObj.exRow = 0;
        fgObj.ctrlType = "iGrid";
        fgObj.status = "True";
        fgObj.sC = "";
        fgObj.iFg_Sort = function (args) { return iFg_Sort(args, fgObj) };
        fgObj.gps = function (args) { return iFg_gps(args, fgObj) };
        fgObj.Insert = function () { return iFg_AddData(fgObj) };
        fgObj.Export = function () { return iFg_Export(fgObj) };
        fgObj.Print = function () { return iFg_Print(fgObj) };
        fgObj.Delete = function (event) { return iFg_DeleteData(fgObj, event) };
        fgObj.SearchClick = function () { return iFg_SearchClick(fgObj) };
        fgObj.RefreshClick = function () { return iFg_RefreshClick(fgObj) };
        fgObj.Submit = function (fireReQValidators, fireAllValidators) { return iFg_Submit(fgObj, fireReQValidators, fireAllValidators) };
        fgObj.Validate = function () { return iFg_Submit(fgObj) };
        fgObj.Expand = function (rIndex, cIndex) { return iFg_ExpandRow(fgObj, rIndex, cIndex) };
        fgObj.SetProperty = function (p, v) { return iFg_SetProperty(fgObj, p, v) };
        fgObj.DataBind = function () { iFg_DataBind(fgObj) };
        fgObj.OnError = function () { OnError(fgObj); }
        fgObj.PsChange = function (obj) { return iFg_OnPageSizeChange(obj, fgObj) }
        fgObj.SetWidth = function (val) { return iFg_SetWidth(fgObj, val) }
        fgObj.ActionClick = function (fn, bol) { return iFg_ActionClick(fgObj, fn, bol) };
        fgObj.pageIndex = pI;
        if (fPI)
            fgObj.fPI = fPI;
        else
            fgObj.fPI = 0;
        fgObj.pageSize = pS;
        fgObj.sortExpression = null;
        fgObj.sortDirection = null;
        fgObj.stateField = getIObject(fgObj.id + "__hidden");
        if (pE != null)
            fgObj.panelElement = getIObject(pE)
        else
            fgObj.panelElement = getIObject("__fg" + fgObj.id + "__div")
        if (fgObj.rC == fgObj.pageSize) {
            //fgObj.showAll = true;
        }
        else {
            fgObj.showAll = false;
        }
        fgObj.createPropertyString = iFg_createPropertyString;
        fgObj.setStateField = iFg_setStateValue;
        fgObj.getHiddenFieldContents = function (args) { return iFg_getHiddenFieldContents(args, fgObj) }; // iFg_getHiddenFieldContents;           
        if (document.addEventListener) {
            BindClickEvent(window.document, "fg_docClick('" + _id + "');");
        }
        else {
            document.attachEvent("onclick", new Function("fg_docClick('" + _id + "');"));
        }
        fgObj.stateField.value = iFg_createPropertyStringFromValues(fgObj.pageIndex, fgObj.sortDirection, fgObj.sortExpression, fgObj.dataKeys, rC).toString();
        fgObj.Rows = function (index) { return iFg_Rows(index, fgObj) }
        fgObj._Columns = new Array();
        fgObj._ColumnByNames = new Array()
        fgObj.CurrentRowIndex = function () { return iFg_GetRowIndex(fgObj) };
        fgObj.ClientRowState = function () { return iFg_ClientRowState(fgObj) };
        fgObj.VirtualCurrentRowIndex = function () { return iFg_VirtualRowIndex(fgObj) };
        if (fgObj.allowAdd || fgObj.allowSearch)
            fgObj.AddButton = getIObject(fgObj.id + "_adb");
        else
            fgObj.AddButton = null;
        if (fgObj.allowDelete || fgObj.allowSearch)
            fgObj.DeleteButton = getIObject(fgObj.id + "_dlb");
        else
            fgObj.DeleteButton = null;
        if (fgObj.allowSearch)
            fgObj.SearchButton = getIObject(fgObj.id + "_srb");
        else
            fgObj.SearchButton = null;
        if (fgObj.allowRefresh)
            fgObj.RefreshButton = getIObject(fgObj.id + "_rfb");
        else
            fgObj.RefreshButton = null;
        iFg_AddRowEvents(fgObj.id);
        fgObj.ShowProgress = function () { return iFg_ShowProgress(fgObj) };
        fgObj.HideProgress = function () { return iFg_HideProgress(fgObj) };
        fgObj.SetStaticHeaderHeight = function (_sHH) { iFg_SetSHH(fgObj, _sHH) };
        fgObj.Reset = function () { ifg_ResetGridstate(fgObj, false); };
        //Button texts
        //set width
        iFg_SetWidth(fgObj, getIObject(fgObj.id).style.width);
        if (aBt != null)
            fgObj.aBt = aBt;
        else if (context != null && context.aBt != null)
            fgObj.aBt = context.aBt;
        if (dBt != null)
            fgObj.dBt = dBt;
        else if (context != null && context.dBt != null)
            fgObj.dBt = context.dBt;
        if (sBt != null)
            fgObj.sBt = sBt;
        else if (context != null && context.sBt != null)
            fgObj.sBt = context.sBt;
        if (cBt != null)
            fgObj.cBt = cBt;
        else if (context != null && context.cBt != null)
            fgObj.cBt = context.cBt;
        if (scBt != null)
            fgObj.scBt = scBt;
        else if (context != null && context.scBt != null)
            fgObj.scBt = context.scBt;
        if (pBt != null)
            fgObj.pBt = pBt;
        else if (context != null && context.pBt != null)
            fgObj.pBt = context.pBt;
        if (rBt != null)
            fgObj.rBt = rBt;
        else if (context != null && context.rBt != null)
            fgObj.rBt = context.rBt;
        if (sDv != null)
            fgObj.sDv = GetBoolFromString(sDv);
        else if (context != null && context.sDv != null)
            fgObj.sDv = GetBoolFromString(context.sDv);
        if (aRCP != null)
            fgObj.aRCP = GetBoolFromString(aRCP);
        else if (context != null && context.aRCP != null)
            fgObj.aRCP = GetBoolFromString(context.aRCP);
        if (aBIC != null)
            fgObj.aBIC = aBIC;
        else if (context != null && context.aBIC != null)
            fgObj.aBIC = context.aBIC;
        if (dBIC != null)
            fgObj.dBIC = dBIC;
        else if (context != null && context.dBIC != null)
            fgObj.dBIC = context.dBIC;
        if (cBIC != null)
            fgObj.cBIC = cBIC;
        else if (context != null && context.cBIC != null)
            fgObj.cBIC = context.cBIC;
        if (sCBIC != null)
            fgObj.sCBIC = sCBIC;
        else if (context != null && context.sCBIC != null)
            fgObj.sCBIC = context.sCBIC;
        if (sHH != null && sHH != "") {
            iFg_CreateStaticHeader(fgObj);
        }
    }
}

function iFg_CreateStaticHeader(fgObj) {
    var table = document.createElement("TABLE");
    table.id = "trHdr" + fgObj.id;
    //_mergeAttributes(fgObj, table);
    //$(table).html(fgObj.rows[0].outerHTML);
    //fgObj.parentNode.parentNode.insertBefore(table, fgObj.parentNode)
    //fgObj.rows[0].style.visibility = "hidden";
    //table.style.position = "relative";
    //iFg_SetHeaderWidth($('#' + fgObj.id), $('#trHdr' + fgObj.id));
    //window.onresize = function () {
    //    waitForFinalEvent(function(){
    //    iFg_SetHeader(fgObj);
    //    });
    //};
}
var waitForFinalEvent = (function () {
    var timers = {};
    return function (callback, ms, uniqueId) {
        if (!uniqueId) {
            uniqueId = "Don't call this twice without a uniqueId";
        }
        if (timers[uniqueId]) {
            clearTimeout(timers[uniqueId]);
        }
        timers[uniqueId] = setTimeout(callback, ms);
    };
})();

function iFg_SetHeader(fgObj) {

    if (document.getElementById("trHdr" + fgObj.id)) {
        iFg_SetHeaderWidth($('#' + fgObj.id), $('#trHdr' + fgObj.id));
    }
}

function iFg_SetHeaderWidth(fgObj, table) {
    var sObjid = fgObj.attr('id');
    if (msie) {
        for (i = 0; i < fgObj.children()[0].children[0].cells.length - 1 ; i++) {
            $(table).children()[0].cells[i].style.width = "";
            if (fgObj.children()[0].children[0].cells[i].offsetWidth > 0) {
                if (checkListGrid(sObjid))
                    $(table).children()[0].cells[i].width = parseInt(fgObj.children()[0].children[0].cells[i].offsetWidth) - 5;
                else
                    $(table).children()[0].cells[i].width = parseInt(fgObj.children()[0].children[0].cells[i].offsetWidth) - 3;
            }
            else {
                if (fgObj.length > 1)
                    $(table).children()[0].cells[i].width = fgObj.children()[0].cells[i].style.width;
            }
        }
        if (fgObj.children()[0].children[1]) {
            for (i = 0; i < fgObj.children()[0].children[1].cells.length - 1 ; i++) {
                var iGridHeader = parseInt(fgObj.children()[0].children[1].cells[i].offsetWidth);
                var iTableHeader = parseInt($(table).children()[0].cells[i].offsetWidth);
                if (iTableHeader > iGridHeader) {
                    iTableHeader = iTableHeader - iGridHeader
                    $(table).children()[0].cells[i].width = parseInt($(table).children()[0].cells[i].width) - iTableHeader;
                }
                else if (iGridHeader > iTableHeader) {
                    iTableHeader = iGridHeader - iTableHeader
                    $(table).children()[0].cells[i].width = parseInt($(table).children()[0].cells[i].width) + iTableHeader;
                }
            }
        }
        if (fgObj.children()[0].children[0].offsetHeight > 0)
            fgObj.parent()[0].style.marginTop = "-" + parseInt(fgObj.children()[0].children[0].offsetHeight) + "px";
        else
            fgObj.parent()[0].style.marginTop = "-25px";
    }
    else {
        for (i = 0; i < fgObj.children()[0].children[0].cells.length - 1 ; i++) {
            $(table).children()[0].children[0].cells[i].style.width = "";
            if (fgObj.children()[0].children[0].cells[i].offsetWidth > 0) {
                if (checkListGrid(sObjid))
                    $(table).children()[0].children[0].cells[i].width = parseInt(fgObj.children()[0].children[0].cells[i].offsetWidth) - 5;
                else
                    $(table).children()[0].children[0].cells[i].width = parseInt(fgObj.children()[0].children[0].cells[i].offsetWidth) - 3;
            }
            else {
                if (fgObj.rows) {
                    if (fgObj.rows.length > 1)
                        $(table).children()[0].children[0].cells[i].width = fgObj.rows[1].cells[i].style.width;
                }
            }
        }
        // for adjustment
        if (fgObj.children()[0].children[1]) {
            for (i = 0; i < fgObj.children()[0].children[1].cells.length - 1 ; i++) {
                var iGridHeader = parseInt(fgObj.children()[0].children[1].cells[i].offsetWidth);
                var iTableHeader = parseInt($(table).children()[0].children[0].cells[i].offsetWidth);
                if (iTableHeader > iGridHeader) {
                    iTableHeader = iTableHeader - iGridHeader
                    $(table).children()[0].children[0].cells[i].width = parseInt($(table).children()[0].children[0].cells[i].width) - iTableHeader;
                }
                else if (iGridHeader > iTableHeader) {
                    iTableHeader = iGridHeader - iTableHeader
                    $(table).children()[0].children[0].cells[i].width = parseInt($(table).children()[0].children[0].cells[i].width) + iTableHeader;
                }
            }
        }
        if (fgObj.children()[0].children[0].offsetHeight > 0)
            fgObj.parent()[0].style.marginTop = "-" + parseInt(fgObj.children()[0].children[0].offsetHeight) + "px";
        else
            fgObj.parent()[0].style.marginTop = "-18px";
    }
}

function iFg_GetRowIndex(fgObj) {
    fgObj = document.all[fgObj.id];
    return fgObj.rowIndex;
}
function iFg_VirtualRowIndex(fgObj) {
    fgObj = document.all[fgObj.id];
    var virtualRI = iFg_GetVRIfromIndex(fgObj, fgObj.rowIndex);
    return virtualRI;
}
function iFg_GetVRIfromIndex(fgObj, index) {
    fgObj = document.all[fgObj.id];
    if (index == "")
        index = 0;
    return parseInt(index) + (parseInt(fgObj.pageIndex) * parseInt(fgObj.pageSize));
}
function iFg_ClientRowState(fgObj) {
    fgObj = document.all[fgObj.id];
    if (fgObj.mode == "Add")
        return "Added";
    else if (fgObj.mode == "Edit")
        return "Modified";
    else if (fgObj.mode == "Delete")
        return "Deleted";
    else
        return "";
}
function iFg_Rows(index, fgObj) {
    fgObj = document.all[fgObj.id];
    this.Count = fgObj.rC;
    //Accessing Server side Columns in Dataset by using ColumnIndex
    this.Columns = function () {
        iFg_GetColValues(fgObj, index);
        return fgObj._Columns;
    }
    // Accessing Client side columns in Grid by using ColumnIndex
    this.GetClientColumns = function () {
        iFg_GetColValuesinClient(fgObj, index);
        return fgObj._ColumnByNames;
    }
    //To set the Columns from Server side
    this.SetValues = function (values) {
        fgObj._Columns = values;
    }
    // Set the client Columns in Grid by using Index
    this.SetColumnValuesByIndex = function (colIndex, values) {
        iFg_SetColValuesinClient(fgObj, values, colIndex, index);
    }
    this.SetReadOnlyColumn = function (colIndex, bolValue) {
        iFg_SetReadOnlyCell(fgObj, bolValue, colIndex, index);
    }
    this.SetReadOnlyColumnClass = function (colIndex, cssClass) {
        iFg_SetReadOnlyCellClass(fgObj, cssClass, colIndex, index);
    }
    this.SetFocusInColumn = function (colIndex) {
        iFg_SetFocusInColumn(fgObj, colIndex, index);
    }
    return this;
}

function iFg_GetColValues(fgObj, index) {
    fgObj = document.all[fgObj.id];
    var mode = fgObj.mode;
    var pRowIndex = fgObj.rowIndex;
    rIndex = iFg_GetVRIfromIndex(fgObj, index);
    fgObj.mode = "GetRow";
    fgObj.rowIndex = rIndex;
    GCB(fgObj);
    iFg_SetRowIndex(fgObj, pRowIndex);
    fgObj.mode = mode;
}

function iFg_SetColValues(fgObj, values) {
    fgObj = document.all[fgObj.id];
    var colNames = values.split(ifg_colsplitter);
    var arrValues = new Array();
    var index = 0;
    for (var count = 0; count < colNames.length; count += 1) {
        var colValues = colNames[count].split(",");
        arrValues[index] = trimAll(colValues[1]);
        index += 1;
    }
    fgObj.Rows(fgObj.rowIndex).SetValues(arrValues);
}

function iFg_SetColValuesinClient(fgObj, value, cIndex, rIndex) {
    fgObj = document.all[fgObj.id];
    var rowIndex = parseInt(rIndex) + parseInt(fgObj.hdrRows);
    var row = fgObj.rows[rowIndex];
    if (row == null) {
        alert('There is no row at ' + rIndex + 'Position');
        return;
    }
    iFg_SetRowIndex(fgObj, rIndex);
    var cell = row.cells[cIndex]
    if (cell != null) {
        if (getIAttribute(cell, "cT") == "iLookup") {
            if (cell.children.length > 0 && cell.childNodes[0].type == "text") {
                if (typeof (value) == "string") {
                    cell.childNodes[0].oldValue = value;
                    cell.childNodes[0].value = value;
                }
                else if (typeof (value) == "object") {
                    cell.childNodes[0].oldValue = value[0];
                    cell.childNodes[0].value = value[0];
                    if (value.length == 2) {
                        cell.childNodes[0].SelectedValues = value[1];
                        cell.SelectedValue = value[1];
                        setIAttribute(cell, "pDV", value[1]);
                    }
                }
            }
            else {
                if (typeof (value) == "string") {
                    setText(cell, value);
                }
                else if (typeof (value) == "object") {
                    setText(cell, value[0]);
                    if (value.length == 2) {
                        cell.SelectedValue = value[1];
                        setIAttribute(cell, "pDV", value[1]);
                    }
                }
            }
        }
        else if (getIAttribute(cell, "cT") == "iCheckBox") {
            cell.childNodes[0].checked = GetBoolFromString(value);
        }
        else if (getIAttribute(cell, "cT") == "iHyperLink") {
            cell.childNodes[0].innerHTML = value;
            //cell.children[0].value = value;
        }
        else if (getIAttribute(cell, "cT") == "iTextBox" || getIAttribute(cell, "cT") == "iDate" || getIAttribute(cell, "cT") == "iContainer") {
            if (cell.children.length > 0 && cell.childNodes[0].type == "text") {
                cell.childNodes[0].oldValue = value;
                cell.childNodes[0].value = value;
                setText(cell.childNodes[0], value);
            }
            else {
                setText(cell, value);
            }
        }
        fgObj.hasRowChanges = true;
        fgObj.hasChanges = true;
    }
}

function iFg_GetColValuesinClient(fgObj, rIndex) {
    fgObj = document.all[fgObj.id];
    var rowIndex = parseInt(rIndex) + parseInt(fgObj.hdrRows);
    var colsArray = new Array();
    var row = fgObj.rows[rowIndex];
    if (row == null) {
        alert('There is no row at ' + rIndex + 'Position');
        return;
    }
    var length = row.cells.length;
    var index = 0;
    for (var count = 0; count < length; count++) {
        var cell = row.cells[count];
        if (getIAttribute(cell, "cT") == "iLookup") {
            if (cell.children.length == 0) {

                colsArray[colsArray.length] = trimAll(getText(cell));
            }
            else {
                GetSelValues(cell.childNodes[0]);
                colsArray[colsArray.length] = trimAll(cell.childNodes[0].value);
            }
            if (typeof (cell.SelectedValue) != "undefined") {
                colsArray[colsArray.length] = trimAll(cell.SelectedValue);
            }
            else {
                colsArray[colsArray.length] = trimAll(getIAttribute(cell, "pDV"));
            }
        }
        else if (getIAttribute(cell, "cT") == "iCheckBox") {
            colsArray[colsArray.length] = cell.childNodes[0].checked;
        }
        else if (getIAttribute(cell, "cT") == "iHyperLink") {
            if (cell.hasChildNodes())
                colsArray[colsArray.length] = trimAll(getText(cell.childNodes[0]));
            else
                colsArray[colsArray.length] = "";
        }
        else if (getIAttribute(cell, "cT") == "iTextBox" || getIAttribute(cell, "cT") == "iDate" || getIAttribute(cell, "cT") == "iContainer") {
            if ((getText(cell) != "*") && (getText(cell) != "")) {
                colsArray[colsArray.length] = trimAll(getText(cell));
            }
            else {
                if (cell.childNodes.length != 0) {
                    // if(cell.childNodes){
                    colsArray[colsArray.length] = trimAll(cell.childNodes[0].value);
                }
                else {
                    colsArray[colsArray.length] = "";
                }
            }
        }
    }
    fgObj._ColumnByNames = colsArray;
}

function iFg_SetReadOnlyCell(fgObj, bolValue, cIndex, rIndex) {
    fgObj = document.all[fgObj.id];
    var rowIndex = parseInt(rIndex) + parseInt(fgObj.hdrRows);
    var row = fgObj.rows[rowIndex];
    if (row == null) {
        alert('There is no row at ' + rIndex + 'Position');
        return;
    }
    var cell = row.cells[cIndex]
    if (cell != null) {
        if (bolValue) {
            setIAttribute(cell, "readOnly", "true");
            setIAttribute(cell, "class", "gRocell");
        }
        else {
            setIAttribute(cell, "readOnly", "false");
            setIAttribute(cell, "class", "");
        }
    }
}

function iFg_SetReadOnlyCellClass(fgObj, cssClass, cIndex, rIndex) {
    fgObj = document.all[fgObj.id];
    var rowIndex = parseInt(rIndex) + parseInt(fgObj.hdrRows);
    var row = fgObj.rows[rowIndex];
    if (row == null) {
        alert('There is no row at ' + rIndex + 'Position');
        return;
    }
    var cell = row.cells[cIndex]
    if (cell != null) {
        setIAttribute(cell, "class", cssClass);
    }
}

function iFg_SetFocusInColumn(fgObj, colIndex, rIndex) {
    fgObj = document.all[fgObj.id];
    var rowIndex = parseInt(rIndex) + parseInt(fgObj.hdrRows);
    var tdObj = fgObj.rows[rowIndex].cells[colIndex];
    TDC(tdObj);
}

function iFg_SetSHH(fgObj, _sHH) {
    fgObj = document.all[fgObj.id];
    fgObj.sHH = _sHH;
    var el = getIObject("__fg" + fgObj.id + "__divsh")
    // if (!parseInt(_sHH)<0) {
    el.style.height = _sHH;
    //  }    
    iFg_SetHeader(fgObj);
}

function iFg_SetWidth(fgObj, _wid) {
    if (_wid) {
        if (_wid.indexOf("px") != -1) {
            _wid = parseInt(_wid) + 20 + "px";
        }
    }
    fgObj = document.all[fgObj.id];
    getIObject("__fg" + fgObj.id + "__div").style.width = _wid;
    if (getIObject("__fg" + fgObj.id + "__divsh")) {
        getIObject("__fg" + fgObj.id + "__divsh").style.width = _wid;
    }
    //getIObject(fgObj.id + "__ftr").style.width = fgObj.clientWidth;
}
function iFg_DataBind(id, param) {
    var fgObj;
    if (getIObject(id) == null) {
        fgObj = new Object();
    }
    else {
        fgObj = getIObject(id);
        //fgObj = document.all[id];
    }
    fgObj.u = unescape(_wcU);
    fgObj.id = id;
    fgObj.ctrlType = 'iGrid';
    fgObj.mode = "ClientBind";
    fgObj.param = iFg_ArraytoString(param);
    GCB(fgObj);
}

function iFg_ArraytoString(param) {
    var arr = "";
    for (var value in param) {
        arr += value + ifg_lkpsplitter;
        arr += param[value] + ifg_colsplitter;
    }
    arr = arr.substring(0, arr.length - ifg_colsplitter.length);
    return arr;
}

function ClientiFlexGrid(id) {
    var paramArray = new Array();
    this.parameters = new function () {
        this.add = function (name, value) {
            paramArray[name] = value;
        }
        return this;
    };
    this.DataBind = function () {
        iFg_DataBind(id, paramArray);
    }
}
/*var _g;
function GCB1(fgObj)
{
_g = fgObj;
fgObj.ShowProgress();
setTimeout(GCB,10);
}*/
//Grid CallBack 
function GCB(fgObj, cbParam, et, pRS, async) {
    /*if (fgObj==null)
    {
    fgObj = _g;
    }*/
    if (iFg_OnBeforeCallback(fgObj) == false) {
        fgObj.status = "false";
        //fgObj.ShowProgress();
        return false;
    }
    hideHelpStrip();
    if (ppsc().gblnHelpTip) {
        showHelpStrip();
    }
    var pU = "";
    pU = pU + fgObj.u;
    pU = iFg_SetQueryString(pU, "v", fgObj.edtValues)
    pU = iFg_SetQueryString(pU, "cols", fgObj.edtColumns)
    pU = iFg_SetQueryString(pU, "rc", fgObj.rC)
    iFg_ShowEdtVal(fgObj);
    if (et != null && et != "") {
        var rI = parseInt(fgObj.pageIndex) * parseInt(fgObj.pageSize) + fgObj.exRowIndex - parseInt(fgObj.hdrRows);
        pU = iFg_SetQueryString(pU, "rI", rI)
    }
    else {
        pU = iFg_SetQueryString(pU, "aa", fgObj.allowAdd);
        pU = iFg_SetQueryString(pU, "ae", fgObj.allowEdit);
        pU = iFg_SetQueryString(pU, "ad", fgObj.allowDelete);
        var rI = parseInt(fgObj.rowIndex);
        pU = iFg_SetQueryString(pU, "rI", rI.toString())
    }
    if (fgObj.parentRowIndex != "" && fgObj.parentRowIndex != null)
        pU = iFg_SetQueryString(pU, "pRI", fgObj.parentRowIndex)
    if (pRS != null && pRS != "") {
        pU = iFg_SetQueryString(pU, "pRS", pRS);
    }
    pU = iFg_SetQueryString(pU, "cT", fgObj.ctrlType);
    if (fgObj.fPI != null || fgObj.fPI != undefined)
        pU = iFg_SetQueryString(pU, "fPI", fgObj.fPI);
    pU = iFg_SetQueryString(pU, "FgMode", fgObj.mode);
    pU = iFg_SetQueryString(pU, "ifgActivityId", getQStr("activityid"));
    pU = iFg_SetQueryString(pU, "pI", fgObj.pageIndex);
    //pU = iFg_SetQueryString(pU,"params",fgObj.param);
    pU = iFg_SetQueryString(pU, "pS", fgObj.pageSize);
    pU = iFg_SetQueryString(pU, "sHH", fgObj.sHH);
    pU = iFg_SetQueryString(pU, "sAll", fgObj.showAll);
    reQ = getXMLHttpRequest();
    if (fgObj.mode == "Export") {
        pU = iFg_SetQueryString(pU, "id", fgObj.id);
        pU = iFg_SetQueryString(pU, "Exptype", "xls");
        var iFg_frm = iFg_FrmObj();
        iFg_frm.src = pU;
        return;
    }
    var postData = "";
    if (pU.length > 2000) {
        postData = pU.substr(fgObj.u.length + 1) + "&";
        pU = fgObj.u;
    }
    if (typeof (fgObj.async) != "undefined" && fgObj.async == true) {
        fgObj.ShowProgress();
        reQ.open("POST", pU, true);
        fgObj.async = false;
    }
    else {
        reQ.open("POST", pU, false);
    }
    reQ.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    iFg_SetReqHdrs(reQ, "params", fgObj.param);
    if (et != undefined && et != null && et != "") {
        reQ.onreadystatechange = new Function("return GetResponse('" + et + "','" + fgObj.mode + "','" + fgObj.id + "');");
        postData = postData + "__CALLBACKID=" + et;
        postData = postData + "&__EVENTTARGET=" + et;
    }
    else {
        reQ.onreadystatechange = new Function("return GetResponse('" + fgObj.id + "','" + fgObj.mode + "');");
        postData = postData + "__CALLBACKID=" + fgObj.id;
        postData = postData + "&__EVENTTARGET=" + fgObj.id;
    }
    if (cbParam != null) {
        postData = postData + "&__CALLBACKPARAM=" + fgObj.getHiddenFieldContents(cbParam).toString();
    }
    //postData = postData + "&__VIEWSTATE=" + document.getElementById("__VIEWSTATE").value;
    reQ.send(postData);

}
function iFg_ShowEdtVal(fgObj) {
    if (_debug) {
        alert("Values:- " + fgObj.edtValues);
        alert("Columns:- " + fgObj.edtColumns);
    }
}
function iFg_SetQueryString(pU, name, value) {
    if (typeof (value) == "undefined")
        return pU;
    if (pU.indexOf("?") != -1) {
        pU = pU + "&" + name + "=" + escape(encodeURIComponent(value));
    }
    else {
        pU = pU + "?" + name + "=" + escape(encodeURIComponent(value));
    }
    return pU;
}
function iFg_SetReqHdrs(xmlreq, name, value) {
    if (typeof (value) == "undefined" || value == "")
        return;
    xmlreq.setRequestHeader(name, encodeURIComponent(value));
}
function GetResponse(_fgid, mode, parentfgid) {
    var fgObj = getIObject(_fgid);
    if (reQ.readyState == 4) {
        if (reQ.status != "200") {
            document.write(reQ.responseText);
        }
        var res = reQ.responseText;
        if (res.charAt(0) == "s") {
            res = res.substring(1, res.length);
            res = "0|" + res;
        }
        if (res == "0|Error" && mode == "Search" && reQ.getResponseHeader("errmsg") != null && reQ.getResponseHeader("errmsg") != "") {
            alert(reQ.getResponseHeader("errmsg"));
            fgObj.Search = true;
            fgObj.readOnly = true;
            fgObj.HideProgress();
            return;
        }
        //if (res != "Error" && res.charAt(0) == "0") {
        if (res != "Error") {
            if (reQ.getResponseHeader("iFg_rebind") != null && reQ.getResponseHeader("iFg_rebind") != "") {
                var RebindGrid = GetBoolFromString(reQ.getResponseHeader("iFg_rebind"));
            }
            if (reQ.getResponseHeader("iFg_evtCan") != null && reQ.getResponseHeader("iFg_evtCan") != "") {
                var evtCancel = GetBoolFromString(reQ.getResponseHeader("iFg_evtCan"));
            }
            if (mode == "ps" || mode == "Sort" || mode == "Delete" || mode == "Add" || mode == "Search" || mode == "navigatetolastpage") {
                iFg_BindData(res, document.all[_fgid]);
                if (evtCancel) {
                    fgObj = document.all[_fgid];
                    //fgObj.status = "false";
                    //fgObj.hasChanges = false;
                }
                else {
                    fgObj.status = "True";
                }
            }
            else if (mode == "Expand" || mode == "ClientBind" || RebindGrid) {
                if (RebindGrid) {
                    var obj = getIObject(fgObj.editCtrl);
                    if (obj != null) {
                        if (getIAttribute(obj.parentNode, "cT") == 'iLookup') {
                            iFg_HideLkpGrid(obj);
                        }
                    }
                }
                iFg_BindData(res, _fgid, parentfgid);
            }
            else if (mode == "GetRow") {
                iFg_SetColValues(fgObj, res.substring(2, res.length));
            }
            else if (mode == "Print") {
                iFg_PrintData(res.substring(2, res.length));
            }
            else {
                fgObj.status = res.split("|")[1];
                if (fgObj.status == "True") {
                    ifg_ResetGridstate(fgObj);
                    //fgObj.hasChanges = false;
                }
            }
            if (fgObj == null || RebindGrid)
                fgObj = document.all[_fgid];
            iFg_OnAfterCallback(fgObj, mode);
        }
        else if (res.charAt(0) == "e") {
            if (typeof (fgObj) != "undefined" && fgObj) {
                fgObj.status = "false";
                fgObj._Columns = new Array();
            }
            if (_debug == true) {
                iFgMsgBox(res.split(".")[0]);
            }
        }
        /*if (fgObj)
        fgObj.HideProgress();*/
        return;
    }
}

function iFg_OnBeforeCallback(fgObj) {
    if (typeof (getIAttribute(fgObj, "bC")) == "string" && getIAttribute(fgObj, "bC") != "" && fgObj.mode != 'Expand' && fgObj.mode != 'GetRow' && fgObj.mode != 'ClientBind' && fgObj.mode != 'Search' && fgObj.mode != 'navigatetolastpage') {
        var param = new iFg_Parameters();
        var _r = true;
        var mode = "";
        if (fgObj.mode == "Add")
            mode = 'Insert';
        else if (fgObj.mode == "Edit")
            mode = 'Update';
        else if (fgObj.mode == "Delete")
            mode = 'Delete';
        _r = eval(getIAttribute(fgObj, "bC") + "('" + mode + "',param)");
        fgObj.param = iFg_ArraytoString(param.getParam());
        return _r;
    }
    return true;
}

function iFg_OnAfterCallback(fgObj, mode) {
    var custErrMsg = "";
    if (reQ.getResponseHeader("CustomErrMsg") != null && reQ.getResponseHeader("CustomErrMsg") != "") {
        custErrMsg = reQ.getResponseHeader("CustomErrMsg");
    }
    var param = new Object();
    if (custErrMsg != "") {
        var values = custErrMsg.split(ifg_colsplitter);
        for (var count = 0; count < values.length; count += 1) {
            var strError = values[count].split(ifg_lkpsplitter);
            param[strError[0]] = strError[1];
        }
        if (getIAttribute(fgObj, "aC") != "") {
            eval(getIAttribute(fgObj, "aC") + "(param,mode)");
        }
    }
    else if (mode == "Search" || mode == "Refresh" || mode == "Sort" || mode == "Edit") {
        if (typeof (getIAttribute(fgObj, "aC")) != "undefined" && getIAttribute(fgObj, "aC") != "") {
            eval(getIAttribute(fgObj, "aC") + "(param,mode)");
        }
    }
}
function fg_docClick(fgId) {
    var fgObj = document.all[fgId];
    if (fgObj != null && fgObj.Search != false)
        iFg_HideFilterDiv(fgObj);
    if (fgObj != null && fgObj.Search == false) {
        if ((typeof (iCalendar) != "undefined" && iCalendar != null && iCalendar.isActive == true) || _calvis == 1) {
            _calvis = 0;
            return;
        }
        var prowIndex = fgObj.rowIndex;
        var eCtrlId = fgObj.editCtrl;
        if (eCtrlId != "") {
            var obj = getIObject(eCtrlId);
            if (obj != null) {
                if (obj.hc == true) {
                    iFg_txtChange(obj);
                }
                else if (getIAttribute(obj.parentNode, "validate") == 'true') {
                    ValidatorOnChange(obj.id);
                }
                if (obj.isvalid == false) {
                    return;
                }
            }
            if (obj != null && obj != "" && getIAttribute(obj.parentNode, "cT") == 'iLookup') {
                lB(obj.id);
                lkpBlur(obj);
            }
            if (fgObj.hasRowChanges || fgObj.rowState == "Added") {
                if (obj != null && obj != "" && getIAttribute(obj.parentNode, "cT") == 'iLookup') {
                    iFg_HideLkpGrid(obj);
                }
                GetRowValues(fgObj, prowIndex);
                SaveData(fgObj);
            }
            else {
                if (obj != null) {
                    ifg_ResetGridstate(fgObj, true);
                }
            }
        }
        else {
            ifg_ResetGridstate(fgObj, true);
        }
    }
    else if (fgObj != null && fgObj.Search == true) {
        if ((typeof (iCalendar) != "undefined" && iCalendar != null && iCalendar.isActive == true) || _calvis == 1) {
            _calvis = 0;
            return;
        }
        var eCtrlId = fgObj.editCtrl;
        if (eCtrlId != "") {
            var obj = getIObject(eCtrlId);
            if (obj != null && obj != "" && getIAttribute(obj.parentNode, "cT") == 'iLookup') {
                lB(obj.id);
            }
        }
    }
}

function iFgMsgBox(msg) {
    alert(msg);
}

//Retrieve Selected Values of Lookup Control
function GetSelValues(obj) {
    var tdObj = obj.parentNode;
    var colValues = getIAttribute(tdObj, "Columns").split("|");
    var columns = colValues[0].split(",");
    for (var count = 0; count < columns.length; count++) {
        if (getIAttribute(tdObj, "pDF") == columns[count]) {
            if (typeof (obj.SelectedValues) != "undefined") {
                if (typeof (obj.SelectedValues) == "string" && obj.SelectedValues != "") {
                    tdObj.SelectedValue = obj.SelectedValues;
                }
                else {
                    tdObj.SelectedValue = obj.SelectedValues[count];
                }
            }
            else if (typeof (getIAttribute(tdObj, "pDV")) != "undefined" && obj.value != "") {
                tdObj.SelectedValue = getIAttribute(tdObj, "pDV");
            }
            else {
                tdObj.SelectedValue = obj.value;
            }
        }
    }
}

var _l = true;
function TDC(tdObj, bol) {
    var fgObj = getGridObj(tdObj);
    var event = getEvent(event);
    if (fgObj.readOnly == false && fgObj.Search == false) {
        hideHelpStrip();
        if (ppsc().gblnHelpTip) {
            showHelpStrip();
        }
        if (fgObj.allowSearch == false) {
            iFg_RaiseRowClickEvent(event, tdObj);
            bol = false;
        }
        var eCtrlId = fgObj.editCtrl;
        var obj = null;
        var err = false;
        if (fgObj.Grid_IsValid != false) {
            //Get Row Values
            if (eCtrlId != "") {
                obj = getIObject(eCtrlId);
                if (typeof (_frmme) != "undefined" && _frmme != "" && _frmme == eCtrlId) {
                    _frmme = "";
                    return;
                }
                if (obj != null && getIAttribute(obj.parentNode, "cT") == 'iLookup' && obj.hasChanges == true && _l == true) {
                    _l = false;
                    ValidatorOnChange(obj.id)
                    if (obj.isvalid == false)
                        return;
                    lkp_onChange(obj);
                    obj.hasChanges = false;
                }
                _l = true;
                var prowIndex = parseInt(fgObj.rowIndex);
                var cRowIndex = parseInt(getIAttribute(tdObj.parentNode, "rIndex"));
                var cellIndex = tdObj.cellIndex;
                if (prowIndex != cRowIndex) {
                    if (fgObj.hasRowChanges || fgObj.rowState == "Added") {
                        GetRowValues(fgObj, prowIndex);
                        SaveData(fgObj);
                        if (fgObj.status == "True") {
                            fgObj = iFg_ResetGridObj(fgObj);
                            var rI = cRowIndex + parseInt(fgObj.hdrRows);
                            try {
                                var ntdObj = document.all[fgObj.id].rows[rI].cells[cellIndex];
                                GetControl(ntdObj);
                            } catch (e) { }
                        }
                        else {
                            return;
                        }
                    }
                    else {
                        GetControl(tdObj);
                    }
                }
                else {
                    if (fgObj.oldValue == fgObj.newValue) {
                        GetControl(tdObj);
                    }
                    else {
                        GetControl(tdObj);
                    }
                }
                //if (event.preventDefault) {
                //    event.preventDefault(true);
                //    event.stopPropagation();
                //}
            }
            else {
                var prowIndex = fgObj.rowIndex;
                var cRowIndex = parseInt(getIAttribute(tdObj.parentNode, "rIndex"));
                var cellIndex = tdObj.cellIndex;
                if (prowIndex != cRowIndex) {
                    if (fgObj.hasRowChanges) {
                        GetRowValues(fgObj, prowIndex);
                        SaveData(fgObj);
                        if (fgObj.status == "True") {
                            fgObj = iFg_ResetGridObj(fgObj);
                            var rI = cRowIndex + parseInt(fgObj.hdrRows);
                            var ntdObj = document.all[fgObj.id].rows[rI].cells[cellIndex];
                            GetControl(ntdObj);
                        }
                        else {
                            return;
                        }
                    }
                    else {
                        GetControl(tdObj);
                    }
                }
                else {
                    GetControl(tdObj);
                }
            }
            if (bol)
                iFg_RaiseRowClickEvent(event, tdObj);
        }
    }
    fgObj.status = "True";
}
function GetControl(tdObj) {
    var fgObj = getGridObj(tdObj);
    var eCtrlId = fgObj.editCtrl;
    if (eCtrlId != "") {
        var obj = getIObject(eCtrlId);
        if (obj != null) {
            if (getIAttribute(obj.parentNode, "validate") == 'true' && fgObj.mode != "Add")
                setIAttribute(obj.parentNode, "kd", obj.IsCheckedOnKd);
            if (getIAttribute(obj.parentNode, "cT") != 'iCheckBox' && getIAttribute(obj.parentNode, "cT") != 'iHyperLink') {
                if (getIAttribute(obj.parentNode, "cT") == 'iLookup') {
                    iFg_HideLkpGrid(obj);
                    GetSelValues(obj);
                }
                setText(obj.parentNode, iFg_SetValues(obj.value));
            }
            else if (getIAttribute(obj.parentNode, "cT") == 'iHyperLink' && typeof (getIAttribute(obj.parentNode, "ML")) == "undefined") {
                iFg_ResetHyperLinkCtrl(obj.parentNode);
            }
        }
    }
    if (getIAttribute(tdObj, "cT") == 'iCheckBox' && iFg_CheckIsEditable(tdObj)) {
        iwc_SetFocus(tdObj.childNodes[0].id);
        fgObj.editCtrl = tdObj.childNodes[0].id;
        fgObj.oldValue = tdObj.childNodes[0].checked;
        return;
    }
    if (typeof (getIAttribute(tdObj, "ML")) != "undefined") {
        return;
    }
    if (iFg_CheckIsEditable(tdObj)) {
        var val = iFg_GetValues(trimAll(getText(tdObj)));
        tdObj.innerHTML = GetEditControl(tdObj);
        var childObj = tdObj.childNodes[0];
        childObj.value = val;
        childObj.title = GetHelpMessage(tdObj);
        helpTip = $(document).tooltip({ track: true });
        if (!ppsc().gblnHelpTip) {
            hideHelpStrip();
        }
        fgObj.editCtrl = childObj.id;
        childObj.isvalid = true;
        init_edtCtrl(childObj);
        SetEditCtrlStyle(tdObj);
        if (getIAttribute(tdObj, "validate") == 'true')
            AddValidators(tdObj);
        //childObj.focus();
        if (getIAttribute(tdObj, "kd") != null && fgObj.mode != "Add")
            childObj.IsCheckedOnKd = getIAttribute(tdObj, "kd");
        iwc_SetFocus(childObj.id);
        fgObj.oldValue = childObj.value;
        if (getIAttribute(tdObj, "cT") == 'iLookup') {
            childObj.oldValue = childObj.value;
        }
        if (getIAttribute(tdObj, "cT") == 'iDate') {
            var img = getIObject(childObj.id + '_img');
            img.tdId = tdObj.id;
            img.childId = childObj.id;
            img.onclick = function () { return DateImgClick(this); };
        }
        else if (getIAttribute(tdObj, "cT") == 'iInputBox') {
            var img = getIObject(childObj.id + '_img');
            img.tdId = tdObj.id;
            img.childId = childObj.id;
            img.onclick = function () { return IpBImgClick(this); };
        }
        iwc_SetFocus(childObj.id);
    }
    var rIndex = getIAttribute(tdObj.parentNode, "rIndex");
    iFg_SetRowIndex(fgObj, rIndex);
}
function SetEditCtrlStyle(tdObj, search, srcWid) {
    if (getIAttribute(tdObj, "cT") == "iLookup" || getIAttribute(tdObj, "cT") == "iTextBox" || getIAttribute(tdObj, "cT") == "iDate") {
        var _lkp = tdObj.childNodes[0];
        var _img = 0;
        var _val = 0;
        var _lmt = 9;
        var _smt = 0;
        if (tdObj.children.length > 1 && (getIAttribute(tdObj, "cT") == "iLookup" || getIAttribute(tdObj, "cT") == "iDate")) {
            _img = tdObj.childNodes[1].clientWidth;
        }
        if (tdObj.children.length > 0) {
            if (tdObj.children.length == 3) {
                _val = tdObj.childNodes[2].clientWidth;
                _lmt = 9;
            }
            if (tdObj.children.length == 1) {
                _lmt = 3;
            }
            if (tdObj.children.length == 2 && getIAttribute(tdObj, "cT") == "iTextBox") {
                _lmt = 8;
            }
        }
        if (search) {
            _smt = tdObj.childNodes[tdObj.children.length - 1].clientWidth;
        }
        //_lkp.style.position ="relative";
        _lkp.style.top = 0;
        _lkp.style.left = 0;
        /*if (typeof(tdObj.style.width)!="undefined" && tdObj.style.width!="")
        _lkp.style.width = parseInt(tdObj.style.width) - parseInt(_img) - parseInt(_val) - _lmt - _smt;
        else if (typeof(srcWid)!="undefined" && srcWid!="")
        _lkp.style.width = parseInt(srcWid) - parseInt(_img) - _smt - 5;*/

        if (typeof (srcWid) != "undefined" && srcWid != "") {
            if ((parseInt(srcWid) - parseInt(_img) - _smt - 19) > 0) {
                _lkp.style.width = parseInt(srcWid) - parseInt(_img) - _smt - 19 + "px";
            }


        }

        else if (typeof (tdObj.style.width) != "undefined" && tdObj.style.width != "")
            if (getIAttribute(tdObj, "cT") == "iLookup" || getIAttribute(tdObj, "cT") == "iDate") {
                _lkp.style.width = "80%";
            }
            else if (getIAttribute(tdObj, "cT") == "iTextBox")
                _lkp.style.width = "91%";
        //  _lkp.style.width =parseInt(tdObj.style.width) - parseInt(_img) - parseInt(_val) - _lmt - _smt+"px";
    }
}
function ValidateCell(tdObj, cellIndex) {
    var fgObj = getGridObj(tdObj);
    var rowIndex = parseInt(getIAttribute(tdObj.parentNode, "rIndex")) + parseInt(fgObj.hdrRows);
    var ctdObj = fgObj.rows[rowIndex].cells[cellIndex];
    var ctrl = fgObj.id + '_CVali';
    if (typeof (Page_Validators) != "undefined") {
        for (var count = 0; count < Page_Validators.length; count++) {
            if (Page_Validators[count].id == ctrl) {
                var val = Page_Validators[count];
                setIAttribute(val, "controltovalidate", getIAttribute(ctdObj, "controltovalidate"));
                ValidatorValidate(val);
                if (val.isvalid == false) {
                    fgObj.Grid_IsValid = false;
                    return false;
                }
            }
        }
    }
    return true;
}

function iFg_CheckIsEditable(tdObj) {
    var fgObj = getGridObj(tdObj);
    if (getIAttribute(tdObj, "readOnly") != "false")
        return false;
    else if (getIAttribute(tdObj, "disabled") == true)
        return false;
    else if (fgObj.rowState == "Added" && fgObj.allowAdd == true)// && fgObj.allowEdit==true && typeof(tdObj.isEdit) != 'undefined' && tdObj.isEdit == 'false')
        return true;
    else if (fgObj.rowState != "Added" && fgObj.allowEdit == true && typeof (getIAttribute(tdObj, "isEdit")) != 'undefined' && getIAttribute(tdObj, "isEdit") == 'false')
        return false;
    else if (fgObj.allowEdit == false)
        return false;
    return true;
}

function iFg_ResetHyperLinkCtrl(tdObj) {
    var id = tdObj.childNodes[0].id;
    var value = "";
    if (getText(tdObj) != "")
        value = getText(tdObj);
    else
        value = tdObj.childNodes[0].value;
    var hypctrl = "<a id = '" + id + "' href = '" + tdObj.href + "' >" + value + "</a>";
    tdObj.innerHTML = hypctrl;
}

function GetRowValues(fgObj, rIndex) {
    if (rIndex !== "") {
        var trObj = fgObj.rows[parseInt(rIndex) + parseInt(fgObj.hdrRows)];
        var cellLen = trObj.cells.length;
        var tdObj = null;
        fgObj.edtColumns = "";
        fgObj.edtValues = "";
        for (var count = 0; count < cellLen; count++) {
            tdObj = trObj.cells[count];
            if (getIAttribute(tdObj, "cT") != 'Expand' && getIAttribute(tdObj, "cT") != 'iCheckBox' && getIAttribute(tdObj, "cT") != 'Image') {
                var _tc = getText(tdObj);
                if (_tc != "") {
                    if (_tc != "*") {
                        if (getIAttribute(tdObj, "cT") == 'iLookup') {
                            ExtractLkpCellValues(tdObj);
                        }
                        else {
                            ExtractCellValues(tdObj);
                        }
                    }
                    else {
                        if (getIAttribute(tdObj, "cT") == 'iLookup') {
                            ExtractLkpValues(tdObj);
                        }
                        else {
                            ExtractCellValues(tdObj);
                        }
                    }
                }
                else if (typeof (tdObj.childNodes[0]) != "undefined") {
                    if (getIAttribute(tdObj, "cT") == 'iLookup')
                        ExtractLkpValues(tdObj);
                    else
                        ExtractCellValues(tdObj);
                }
                else if (getText(tdObj) == "") {
                    if (getIAttribute(tdObj, "cT") == 'iLookup') {
                        ExtractLkpCellValues(tdObj);
                    }
                    else {
                        ExtractCellValues(tdObj);
                    }
                }
            }
            else if (getIAttribute(tdObj, "cT") != 'iCheckBox' && getIAttribute(tdObj, "cT") != 'Image') {
                ifg_SetColumnandValues(fgObj, "Expand", "Expand");
            }
            else if (getIAttribute(tdObj, "cT") != 'Image') {
                ExtractChkCellValues(tdObj);
            }
            else {
                ifg_SetColumnandValues(fgObj, "Image", "Image");
            }
        }
    }
}

function ExtractCellValues(tdObj) {
    var fgObj = getGridObj(tdObj);
    if (getIAttribute(tdObj, "cT") != 'iHyperLink') {
        if (typeof (tdObj.childNodes[0]) != "undefined") {
            if (typeof (tdObj.childNodes[0].value) != "undefined")
                ifg_SetColumnandValues(fgObj, tdObj.childNodes[0].value, getIAttribute(tdObj, "cN"));
            else
                ifg_SetColumnandValues(fgObj, getText(tdObj), getIAttribute(tdObj, "cN"));
        }
        else {
            ifg_SetColumnandValues(fgObj, getText(tdObj), getIAttribute(tdObj, "cN"));
        }
    }
    else {
        if (tdObj.hasChildNodes()) {
            if (tdObj.childNodes[0].tagName == "A") {
                if (typeof (tdObj.childNodes[0].v) != 'undefined' && tdObj.childNodes[0].v != null)
                    ifg_SetColumnandValues(fgObj, tdObj.childNodes[0].v, getIAttribute(tdObj, "cN"));
                else
                    ifg_SetColumnandValues(fgObj, getText(tdObj), getIAttribute(tdObj, "cN"));
            }
            else
                ifg_SetColumnandValues(fgObj, getText(tdObj) || tdObj.childNodes[0].value, getIAttribute(tdObj, "cN"));
        }
        else
            ifg_SetColumnandValues(fgObj, getText(tdObj), getIAttribute(tdObj, "cN"));
    }
}

function ExtractLkpCellValues(tdObj) {
    var fgObj = getGridObj(tdObj);
    if (typeof (tdObj.SelectedValue) == "undefined" || tdObj.SelectedValue == "") {
        if (typeof (getIAttribute(tdObj, "pDV")) != "undefined")
            tdObj.SelectedValue = getIAttribute(tdObj, "pDV");
        else
            tdObj.SelectedValue = "";
    }

    if (trimAll(getText(tdObj)) == "")
        tdObj.SelectedValue = "";

    if (getIAttribute(tdObj, "fDF") != getIAttribute(tdObj, "cN"))
        ifg_SetlkpColumnandValues(fgObj, tdObj.SelectedValue, getIAttribute(tdObj, "fDF"));
    if (typeof (getIAttribute(tdObj, "cN")) != "undefined") {
        ifg_SetColumnandValues(fgObj, getText(tdObj), getIAttribute(tdObj, "cN"));
    }
}

function ExtractLkpValues(tdObj) {
    var fgObj = getGridObj(tdObj);
    GetSelValues(tdObj.childNodes[0]);
    ifg_SetlkpColumnandValues(fgObj, tdObj.SelectedValue, getIAttribute(tdObj, "fDF"));
    if (typeof (getIAttribute(tdObj, "cN")) != "undefined") {
        ifg_SetColumnandValues(fgObj, tdObj.childNodes[0].value || tdObj.childNodes[0].textContent, getIAttribute(tdObj, "cN"));
    }
}

function ExtractChkCellValues(tdObj) {
    var fgObj = getGridObj(tdObj);
    ifg_SetChkColumnandValues(fgObj, tdObj.childNodes[0].checked, getIAttribute(tdObj, "cN"));
}

function ifg_SetlkpColumnandValues(fgObj, val, col) {
    if (col != "" && col != null) {
        if (typeof (val) == "undefined")
            val = "";
        fgObj.edtColumns += trimAll(col) + ifg_lkpsplitter;
        fgObj.edtValues += iFg_SetValues(trimAll(val)) + ifg_lkpsplitter;
    }
}

var ifg_lkpsplitter = "<?>";
var ifg_colsplitter = "<|>";

function ifg_SetColumnandValues(fgObj, val, col) {
    if (col != "" && col != null) {
        if (iFg_CheckValues(fgObj.edtColumns, col)) {
            fgObj.edtColumns += trimAll(col) + ifg_colsplitter;
            fgObj.edtValues += iFg_EncodeString(trimAll(val)) + ifg_colsplitter;
        }
    }
    else {
        fgObj.edtColumns += ifg_colsplitter;
        fgObj.edtValues += ifg_colsplitter;
    }
}
function iFg_EncodeString(_string) {
    if (_string != "" && _string != null) {
        _string = _string.replace(/[\&]/g, "%26");
        _string = _string.replace(/[\']/g, "%27");
        _string = _string.replace(/[\+]/g, "%2b");
        return trimAll(_string);
    }
    else {
        return "";
    }
}
function iFg_CheckValues(fStr, inStr) {
    var len = inStr.length;
    var startIndex = fStr.indexOf(inStr);
    if (startIndex != -1) {
        var str = fStr.substring(startIndex, len - 1);
        if (str != inStr)
            return true;
        else
            return false;
    }
    else
        return true;
}

function ifg_SetChkColumnandValues(fgObj, val, col) {
    if (col != "" && col != null) {
        fgObj.edtColumns += col + ifg_colsplitter;
        fgObj.edtValues += val + ifg_colsplitter;
    }
    else {
        fgObj.edtColumns += ifg_colsplitter;
        fgObj.edtValues += ifg_colsplitter;
    }
}

//Lookup Initialise
function init_lkpCtrl(obj) {
    var tdObj = obj.parentNode;
    if (getIObject(obj.id + '_img') != null)
        Init_LkpRS(obj.id, obj.id + '_img');
    else
        Init_LkpRS(obj.id, '');
    obj.SelectedValues = tdObj.SelectedValue;
}

//Lookup Initialise
function init_ipbCtrl(obj) {
    if (getIObject(obj.id + '_img') != null)
        Init_ipbRS(obj.id, obj.id + '_img');
    else
        Init_ipbRS(obj.id, '');

}

//Edit controls Click event
function txtClick(obj) {
    iFg_CancelBubble();
}

//Lookup OnBlur Event
function lkpBlur(obj) {
    var parentEle = obj.parentNode;
    var fgObj = getGridObj(parentEle);
    if (obj.isvalid == true) {
        //lBr();
        fgObj.newValue = obj.value;
        if (fgObj.oldValue != fgObj.newValue) {
            obj.hc = true;
            fgObj.hasChanges = true;
            fgObj.hasRowChanges = true;
            if (fgObj.rowState != "Added")
                fgObj.mode = "Edit";
        }
    }
}

//TextBox OnBlur event
function txtBlur(obj) {
    if (obj.isvalid == true && obj.parentNode != null) {
        //lBr();    
        var tdObj = obj.parentNode;
        var fgObj = getGridObj(tdObj);
        fgObj.newValue = obj.value;
        if (fgObj.oldValue != fgObj.newValue) {
            obj.hc = true;
            fgObj.hasChanges = true;
            fgObj.hasRowChanges = true;
            if (fgObj.rowState != "Added")
                fgObj.mode = "Edit";
            iFg_txtChange(obj);
        }
    }
}


function iFg_txtChange(obj) {
    if (obj == null) {
        var event = getEvent(this);
        obj = event.srcElement || event.target;
    }
    if (typeof (obj.hc) == "undefined" || obj.hc == false)
        return;
    var tdObj = obj.parentNode;
    var fgObj = getGridObj(tdObj);
    if (obj.hasChanges == true) {
        ValidatorOnChange(obj.id);
    }
    if (fgObj.Search == true)
        return;
    if (tdObj != null && typeof (getIAttribute(tdObj, "tC")) != "undefined")
        eval(getIAttribute(tdObj, "tC") + "(obj);");
    obj.hc = false;
    var event = getEvent(event);
    if (event.preventDefault) {
        event.preventDefault(true);
        event.stopPropagation();
    }
}

function ifg_chkMouseDown(obj) {
    var fgObj = getGridObj($(obj).parent().get(0));
    if (fgObj == null) {
        fgObj = getIObject(obj.id)
        if (!fgObj) {
            fgObj = getIObject(obj.id.substring(0, obj.id.indexOf("_ct")))
        }
    }

    if (!fgObj || fgObj.readOnly == true)
        return false;
    fgObj.oldValue = obj.checked;
    fgObj.hasChanges = true;
    fgObj.hasRowChanges = true;
    obj.isvalid = true;
    if (fgObj.rowState != "Added")
        fgObj.mode = "Edit";
    return true;
}

function iFg_Save(tdObj) {
    var fgObj = getGridObj(tdObj);
    var edtCtrl = fgObj.editCtrl;
    if (edtCtrl != "" && fgObj.Grid_IsValid != false) {
        var obj = getIObject(edtCtrl);
        if (obj == null)
            return true;
        var ptdObj = obj.parentNode;
        var val = true;
        var prowIndex = getIAttribute(ptdObj.parentNode, "rIndex");
        var crowIndex = getIAttribute(tdObj.parentNode, "rIndex");
        if ((crowIndex != prowIndex) && (fgObj.hasRowChanges || fgObj.rowState == "Added")) {
            GetRowValues(fgObj, prowIndex);
            SaveData(fgObj);
            val = false;
            if (fgObj.status == "True")
                val = true;
        }
        if (val == false)
            return false;
    }
    return true;
}

function chkChange(obj) {
    var tdObj = obj.parentNode;
    var fgObj = getGridObj(tdObj);
    var fgid = fgObj.id;
    if (fgObj.Grid_IsValid == false || fgObj.readOnly == true)
        return false;
    //tdObj.click();
    fg_docClick(fgid);
    var prowIndex = parseInt(fgObj.rowIndex);
    var cRowIndex = parseInt(getIAttribute(tdObj.parentNode, "rIndex"));
    if (prowIndex === cRowIndex) {
        TDC(tdObj);
    }
    var fgObj = getIObject(fgid);
    //obj = getIObject(fgObj.editCtrl);
    //var tdObj = obj.parentNode;
    var event = getEvent(this);
    //if (event.stopPropagation) {
    //    event.stopPropagation();
    //}
    if (fgObj.status == "True") {
        if (ifg_chkMouseDown(obj) == false)
            return false;
        iFg_SetRowIndex(fgObj, getIAttribute(tdObj.parentNode, "rIndex"));
        fgObj.newValue = obj.checked;
        if (fgObj.readOnly != false || getIAttribute(tdObj, "readOnly") != 'false') {
            if (obj.checked) {
                obj.checked = false;
            }
            else {
                obj.checked = true;
            }
        }
        //To be Verified.
        var event = getEvent(this);
        var _obj = event.srcElement || event.target;
        if (obj.id != _obj.id) {
            if (fgObj.oldValue == false)
                obj.checked = true;
            else
                obj.checked = false;
        } else if (obj.checked != _obj.checked) {
            obj.checked = _obj.checked;
        }
        return true;
    }
    else {
        return false;
    }
}

//Edit Control KeyDown Event
function txtKeyDown(obj, event) {
    if (!event)
        event = getEvent(event);
    var tdObj = obj.parentNode;
    if (tdObj == null)
        return;
    var fgObj = getGridObj(tdObj);
    if (event.keyCode == 9 || event.keyCode == 13) {
        if (fgObj.Search == true) {
            if (event.shiftKey != true) {
                MoveNextSearchCell(tdObj);
            }
            else {
                MovePreviousSearchCell(tdObj);
            }
            iFg_StopNavi(event);
            return;
        }
        if (getIAttribute(tdObj, "cT") == 'iLookup') {
            lkpBlur(obj);
            iFg_HideLkpGrid(obj);
        }
        else if (getIAttribute(tdObj, "cT") == 'iCheckBox') {
            //chkBlur(obj);
            chkChange(obj);
        }
        else if (getIAttribute(tdObj, "cT") == 'iDate') {
            txtBlur(obj);
            iFg_txtChange(obj);
            iFg_HideCal();
        }
        else {
            txtBlur(obj); iFg_txtChange(obj);
        }
        if (event.shiftKey == false) {
            MoveNext(tdObj);
        }
        else {
            MovePrevious(tdObj);
        }
        SetFocustoEditCtrl(document.all[fgObj.id]);
        iFg_StopNavi(event);
    }
    else if (event.keyCode == get_icHlpKey() && get_icHlpEnb()) {
        lHp(event);
    }
}

//Move to Next Field
function MoveNext(tdObj) {
    try {
        var fgObj = getGridObj(tdObj);
        var trObj = tdObj.parentNode;
        var nTdObj = null;
        var nThObj = null;
        var currCellIndex = tdObj.cellIndex;
        var currRowIndex = parseInt(getIAttribute(trObj, "rIndex")) + parseInt(fgObj.hdrRows);
        var cellLen = trObj.cells.length;
        var rowLen = fgObj.rows.length;
        if (currCellIndex != cellLen - 1) {
            currCellIndex += 1;
        }
        else {
            currCellIndex = 0;
            currRowIndex += 1;
        }
        if (currRowIndex <= rowLen - 1) {
            nTdObj = fgObj.rows[currRowIndex].cells[currCellIndex];
        }
        else if (currRowIndex == rowLen)// && fgObj.allowAdd == false)
        {
            nTdObj = null;
            GetRowValues(fgObj, fgObj.rowIndex);
            SaveData(fgObj);
            fgObj = iFg_ResetGridObj(fgObj);
            if (currCellIndex != cellLen - 1) {
                if (typeof (fgObj.lr) != "undefined") {
                    if (fgObj.lr == true) {
                        fgObj.lr = false;
                        return;
                    }
                }
                currRowIndex -= 1;
                nTdObj = fgObj.rows[currRowIndex].cells[currCellIndex];
                fgObj.lr = true;
            }
        }
        else {
            nTdObj = null;
            GetRowValues(fgObj, fgObj.rowIndex);
            SaveData(fgObj);
            fgObj = iFg_ResetGridObj(fgObj);
            if (fgObj.status != "false" && fgObj.allowAdd == true) {
                //fgObj.Insert();
                nTdObj = null;
                currCellIndex = 0;
                currRowIndex -= 1;
                if (currRowIndex - parseInt(fgObj.hdrRows) > parseInt(fgObj.pageSize) - 1) {
                    currRowIndex -= 1;
                }
                nTdObj = fgObj.rows[currRowIndex].cells[currCellIndex];
            }
            else// if(fgObj.allowAdd != true)        
            {
                if (fgObj.status == "false") {
                }
                else {
                    ifg_ResetGridstate(fgObj);
                    var tdObj = fgObj.rows[parseInt(fgObj.rows.length - 1)].cells[0];
                    if (!iFg_CheckIsEditable(tdObj))
                        MoveNext(tdObj);
                    else
                        TDC(tdObj);
                }
                SetFocustoEditCtrl(fgObj);
                iFg_StopNavi();
                fgObj.status = "True"
            }
        }
        if (nTdObj != null) {
            if (getIAttribute(nTdObj, "cT") == 'Expand' || (getIAttribute(nTdObj, "cT") == 'iHyperLink' && typeof (getIAttribute(nTdObj, "ML")) != 'undefined') || !iFg_CheckIsEditable(nTdObj)) {
                MoveNext(nTdObj);
            }
            else {
                TDC(nTdObj);
            }
        }
    } catch (e) { }
}

//Move to Previous field
function MovePrevious(tdObj) {
    var fgObj = getGridObj(tdObj);
    var trObj = tdObj.parentNode;
    var nTdObj = null;
    var cellLen = trObj.cells.length;
    var rowLen = fgObj.rows.length;

    var currCellIndex = tdObj.cellIndex;
    var currRowIndex = parseInt(getIAttribute(trObj, "rIndex")) + parseInt(fgObj.hdrRows);

    if (currCellIndex != 0) {
        currCellIndex -= 1;
    }
    else {
        currCellIndex = cellLen - 1;
        currRowIndex -= 1;
    }
    if (currRowIndex >= 0) {
        nTdObj = fgObj.rows[currRowIndex].cells[currCellIndex];
    }
    else {
        nTdObj = null;
    }
    if (nTdObj != null) {
        if (getIAttribute(nTdObj, "cT") == 'Expand' || (getIAttribute(nTdObj, "cT") == 'iHyperLink' && typeof (getIAttribute(nTdObj, "ML")) != 'undefined') || !iFg_CheckIsEditable(nTdObj)) {
            MovePrevious(nTdObj);
        }
        else {
            TDC(nTdObj);
        }
    }
}
function SetFocustoEditCtrl(gObj) {
    try {
        document.body.focus();
        if (gObj.editCtrl)
            getIObject(gObj.editCtrl).focus();
    } catch (e) { }
}
function iFg_StopNavi(event) {
    if (!event)
        event = getEvent(event);
    try {
        if ((event.keyCode == 9 || event.keyCode == 13)) {//event.type == "keydown" && 
            if (event.preventDefault) {
                event.preventDefault(true);
                event.stopPropagation();
            }
            else
                event.keyCode = 0;
            event.returnValue = false;
            event.cancelBubble = true;
        }
    } catch (ex) { event.returnValue = false; }
}

function iFg_sD(fgObj) {
    var eCtrlId = fgObj.editCtrl;
    if (eCtrlId != "") {
        var obj = getIObject(eCtrlId);
        if (obj != null && getIAttribute(obj.parentNode, "validate") == 'true') {
            ValidatorOnChange(obj.id);
            if (obj.isvalid == false) {
                return;
            }
        }
        if (obj != null && obj != "" && getIAttribute(obj.parentNode, "cT") == 'iLookup') {
            lB(obj.id);
        }
    }
    GetRowValues(fgObj, fgObj.rowIndex);
    SaveData(fgObj);
}
function iFg_Print(fgObj) {
    fgObj.mode = "Print";
    GCB(fgObj);
}
function iFg_PrintData(res) {
    if (res == null && res == "")
        return;
    var s = "<html><head><title>";
    s += "</title>";
    var i = 0;
    for (i = 0; i < document.styleSheets.length; i++) {
        s += document.styleSheets(i).owningElement.outerHTML;
    }
    s += "</title></head><body topmargin='0' leftmargin='0'><table width='100%' height='90%'><tr valign='top'><td  width='100%' height='100%'><div id='g'>"
    s += "</div><input type='button' id='h' style='display:none;' name='h' onclick='javascript:window.print();'/></td></tr></table></body></html>"
    var frmobj = iFg_FrmObj();
    frmobj.contentWindow.document.open();
    frmobj.contentWindow.document.write(s);
    frmobj.contentWindow.document.close();
    frmobj.contentWindow.document.body.focus();
    frmobj.contentWindow.document.getElementById("g").innerHTML = res; //alert(frmobj.contentWindow.document.body.innerHTML);
    frmobj.contentWindow.document.getElementById("h").onclick();
}
function iFg_Export(fgObj) {
    fgObj.mode = "Export";
    GCB(fgObj);
}
function iFg_FrmObj() {
    var fE = document.getElementById("iFgfrm");
    if (fE == null) {
        fE = document.createElement("IFRAME");
        fE.id = "iFgfrm"
        fE.scrolling = "no";
        fE.frameborder = "0";
        fE.width = "100";
        fE.height = "100";
        fE.style.display = "none";
        document.body.appendChild(fE);
    }
    return fE;
}
function iFg_AddData(fgObj) {
    fgObj = document.all[fgObj.id];
    if (fgObj.AddButton) {
        var val = trimAll(getText(fgObj.AddButton));
        if (val.indexOf("Clear") != -1) {
            iFg_ClearSearchRow(fgObj);
            return;
        }
    }
    iFg_HideLkpGrid(fgObj);
    if (fgObj.hasRowChanges)
        iFg_sD(fgObj);
    fgObj = getIObject(fgObj.id);
    var RSAfgObj = iFg_GetRSAfgObj(fgObj);
    if (RSAfgObj != null && RSAfgObj.mode == 'Add' && RSAfgObj.Grid_IsValid != false) {
        var pmode = RSAfgObj.mode;
        if (RSAfgObj.hasRowChanges)
            iFg_sD(RSAfgObj);
        else {
            SetFocustoEditCtrl(RSAfgObj);
            return false;
        }
        if (RSAfgObj.status == "false") {
            SetFocustoEditCtrl(RSAfgObj);
            return false;
        }
        RSAfgObj = getIObject(RSAfgObj.id);
        RSAfgObj.mode = pmode;
        fgObj = getIObject(fgObj.id);
    }
    else if (RSAfgObj != null && RSAfgObj.mode == 'Add' && RSAfgObj.Grid_IsValid == false) {
        SetFocustoEditCtrl(RSAfgObj);
    }
    if (fgObj.readOnly == false && fgObj.allowAdd == true) // && fgObj.mode != "Add")
    {
        iFg_CancelBubble();
        /*if(fgObj.rowIndex != "")   
        {
        var trObj = iFg_GRO(fgObj);
        GridMouseOut(trObj.id);             
        }
        */
        if (fgObj.Grid_IsValid != false) {
            if (iFg_bRC(fgObj) == false) {
                SetFocustoEditCtrl(fgObj);
                return false;
            }
            //fgObj = iFg_ResetGridObj(fgObj);
            if (fgObj.aRCP == false) {
                var pg = (fgObj.rC / fgObj.pageSize) - 1;
                if (fgObj.pageIndex < pg) {
                    fgObj.mode = "navigatetolastpage";
                    GCB(fgObj);
                }
                fgObj = iFg_ResetGridObj(fgObj);
                if (fgObj.status == "false")
                    return false;
            }
            fgObj.mode = "Add";
            var row = fgObj.rows[fgObj.rows.length - 1];
            iFg_SetRowIndex(fgObj, getIAttribute(row, "rIndex"));
            if (CheckRowValues(fgObj) == true) {
                var nRow = fgObj.insertRow(fgObj.rows.length);
                if (nRow.mergeAttributes)
                    nRow.mergeAttributes(row);
                else
                    _mergeAttributes(row, nRow);
                setIAttribute(nRow, "rIndex", parseInt(getIAttribute(row, "rIndex")) + 1);
                nRow.id = fgObj.id + "_" + getIAttribute(nRow, "rIndex");
                setIAttribute(nRow, "kd", null);
                for (var count = 0; count < row.cells.length; count++) {
                    var cell = row.cells[count];
                    var nCell = nRow.insertCell(count);
                    nCell.innerHTML = cell.innerHTML;
                    if (nCell.mergeAttributes)
                        nCell.mergeAttributes(cell);
                    else
                        _mergeAttributes(cell, nCell);
                    nCell.id = fgObj.id + "_" + getIAttribute(nCell, "cN") + "_" + getIAttribute(nRow, "rIndex");
                    iFg_SetNewCellState(nCell);
                    if (getIAttribute(nCell, "cT") == "iLookup" && fgObj.sDv == false) {
                        nCell.SelectedValue = "";
                        setIAttribute(nCell, "pDV", "");
                    }
                    if (getIAttribute(nCell, "cT") == 'iCheckBox') {
                        if (!iFg_CheckIsEditable(nCell)) {
                            nCell.childNodes[0].disabled = false;
                        }
                        nCell.childNodes[0].id = fgObj.id + "_chk_" + "_" + getIAttribute(nCell, "cN") + "_" + getIAttribute(nRow, "rIndex");
                        if (fgObj.sDv == false)
                            nCell.childNodes[0].checked = false;
                    }
                    else if (getIAttribute(nCell, "cT") == 'Expand') {
                        if (nCell.children.length > 0)
                            nCell.childNodes[0].id = fgObj.id + "_exp_" + getIAttribute(nRow, "rIndex");
                    }
                    else if (getIAttribute(nCell, "cT") == 'iHyperLink' && typeof (getIAttribute(nCell, "ML")) != 'undefined' && fgObj.sDv == false) {
                        nCell.childNodes[0].v = "";
                    }
                    if (getIAttribute(nCell, "cT") != 'iHyperLink' && getIAttribute(nCell, "cT") != 'Expand' && getIAttribute(nCell, "cT") != 'iCheckBox' && fgObj.sDv == false) {
                        setText(nCell, "");
                    }
                    if (getIAttribute(nCell, "readOnly") == "true" && getIAttribute(nCell, "cT") != 'iHyperLink' && getIAttribute(nCell, "cT") != 'Expand' && getIAttribute(nCell, "cT") != 'iCheckBox' && fgObj.sDv == false) {
                        setText(nCell, "");
                    }
                }
                fgObj.rowState = "Added";
                iFg_SetRowIndex(fgObj, getIAttribute(nRow, "rIndex"));
                iFg_aRC(fgObj, fgObj.rowIndex);
                var tdObj = fgObj.rows[fgObj.rows.length - 1].cells[0];
                if (!iFg_CheckIsEditable(tdObj))
                    MoveNext(tdObj);
                else
                    TDC(tdObj);
                SetFocustoEditCtrl(fgObj);
                iFg_StopNavi();
            }
        }
    }
    return true;
}

function iFg_SetNewCellState(nCell) {
    /*var fgObj = getGridObj(nCell);
    if (nCell.readOnly == "true" && (typeof (tdObj.isEdit) != 'undefined' && tdObj.isEdit == 'false'))
    nCell.readOnly = "true"
    else
    nCell.readOnly = "false"*/
}

function iFg_RaiseRowClickEvent(event, tdObj) {
    if (!event)
        event = getEvent(event);
    if (event) {
        //var tdObj = event.srcElement || event.target;
        var trObj = tdObj.parentNode;
        if (trObj != null && trObj.tagName == 'TR' && trObj.onclick != null)
            tdObj.parentNode.onclick();
        event.cancelBubble = true;
    }
    else {
        var trObj = tdObj.parentNode;
        if (trObj != null && trObj.tagName == 'TR' && trObj.onclick != null)
            tdObj.parentNode.onclick();
    }
}

function iFg_CancelBubble(event) {
    try {
        if (!event)
            event = getEvent(event);
        event.cancelBubble = true;
        if (event.stopPropagation)
            event.stopPropagation();
    }
    catch (ex) { }
}

function iFg_HideLkpGrid(obj) {
    if (obj == null)
        return;
    if (obj.type != "text") {
        /*if (typeof(obj.gId)=="string" && obj.gId!="")
        {
        var fgObj = getIObject(obj.gId);
        if (fgObj.Grid_IsValid == false)
        return;
        }*/
        if (obj.editCtrl != "") {
            var txt = getIObject(obj.editCtrl);
            if (txt != null)
                iFg_HideLkpGrid(txt);
        }
        return;
    }
    if (obj.lkpGrid != null) {
        var _frm = getIObject(obj.id + "_frm");
        obj.lkpGrid.style.visibility = "hidden";
        if (_frm != null) {
            _frm.style.display = "none";
        }
    }
}

function iFg_HideCal() {
    try {
        if (typeof (iCalendar) != "undefined")
            iCalendar.hide();
    } catch (e) { }
}

function CheckRowValues(fgObj) {
    var trObj = fgObj.rows[parseInt(fgObj.rowIndex) + parseInt(fgObj.hdrRows)];
    var tdObj = null;
    for (var count = 0; count < trObj.cells.length; count++) {
        tdObj = trObj.cells[count];
        if (trimAll(getText(tdObj)) == "") {
            if (tdObj.q == 't' && tdObj.readOnly != "true")
                return false;
        }
    }
    return true;
}

function hypclick(obj) {
    var bol = true;
    var tdObj = obj.parentNode;
    var trObj = tdObj.parentNode;
    var fgObj = getGridObj(tdObj);
    if (fgObj.readOnly != true && fgObj.Grid_IsValid != false) {
    }
    else {
        bol = false;
    }
    if (bol == true) {
        var status = iFg_Save(tdObj);
        if (status) {
            fgObj = iFg_ResetGridObj(fgObj);
            ifg_ResetGridstate(fgObj, true);
            iFg_SetRowIndex(fgObj, getIAttribute(trObj, "rIndex"));
            if (fgObj.readOnly == false) {
            }
            else {
                bol = false;
            }
        }
        else {
            bol = false;
        }
    }
    if (bol == true && typeof (getIAttribute(tdObj, "ML")) != "undefined" && getIAttribute(tdObj, "ML").toString() == "true") {
        ShowMultiLineBox();
        bol = false;
    }
    if (bol == false) {
        iFg_StopNavi();
        iFg_CancelBubble();
    }
    if (document.body.click)
        document.body.click();
    else
        DBC();
    var event = getEvent(event);
    if (event.preventDefault) {
        event.preventDefault(true);
        event.stopPropagation();
    }
    return bol;
}

function ifg_ResetGridstate(fgObj, bol) {
    if (bol == null) {
        if (fgObj.rowState == "Added")
            ChangeReadOnlyCell(fgObj);
        fgObj.mode = "";
        iFg_SetRowIndex(fgObj, "")
        fgObj.hasRowChanges = false;
        fgObj.edtColumns = "";
        fgObj.edtValues = "";
        fgObj.rowState = "";
    }
    if (fgObj.editCtrl != "") {
        var obj = getIObject(fgObj.editCtrl);
        if (obj != null) {
            var tdObj = obj.parentNode;
            if (getIAttribute(tdObj, "cT") != 'iCheckBox' && getIAttribute(tdObj, "cT") != 'iHyperLink') {
                if (obj != null && getIAttribute(tdObj, "cT") == 'iLookup') {
                    iFg_HideLkpGrid(obj);
                    GetSelValues(obj);
                }
                setText(tdObj, obj.value);
            }
            else if (getIAttribute(tdObj, "cT") == 'iHyperLink' && typeof (getIAttribute(tdObj, "ML")) == "undefined") {
                iFg_ResetHyperLinkCtrl(tdObj);
            }
        }
    }
}

function SaveData(fgObj) {
    var bol = false;
    if (fgObj.rowState == "Added" && fgObj.rowIndex == fgObj.rows.length - parseInt(fgObj.hdrRows) - 1) {
        ValidateAddedRow(fgObj, true);
        fgObj.mode = "Add";
        bol = true;
    }
    else if (fgObj.hasRowChanges == true) {
        ValidateAddedRow(fgObj, true);
        fgObj.mode = "Edit";
        //bol = true;
    }

    if (fgObj.hasRowChanges == true) {
        var obj = getIObject(fgObj.editCtrl);
        if (obj) {
            var tdobj = obj.parentNode;
            if (bol == true) {
                var s = obj.IsCheckedOnKd;
                TDC(tdobj);
                obj = getIObject(fgObj.editCtrl);
                if (obj != null) {
                    if (s != null && typeof (s) != "undefined")
                        obj.IsCheckedOnKd = s;
                }
                bol = false;
            }
            if (obj != null && getIAttribute(obj.parentNode, "validate") == 'true') {
                ValidatorOnChange(obj.id);
                if (obj.isvalid == false) {
                    fgObj.status = "false";
                    return;
                }
            }
        }
        fgObj.edtColumns = fgObj.edtColumns.substring(0, fgObj.edtColumns.length - ifg_colsplitter.length);
        fgObj.edtValues = fgObj.edtValues.substring(0, fgObj.edtValues.length - ifg_colsplitter.length);
        GCB(fgObj);
    }
}

function ChangeReadOnlyCell(fgObj) {

}

function setText(obj, value) {
    if (typeof (obj.textContent) != "undefined")
        obj.textContent = value;
    else
        obj.innerText = value;
}


function getTdText(obj) {
    if (typeof (obj.textContent) != "undefined")
        return obj.textContent;
    else
        return obj.innerText;
}

function ValidateAddedRow(fgObj, bol) {
    var rowIndex = parseInt(fgObj.rowIndex) + parseInt(fgObj.hdrRows);
    var trObj = fgObj.rows[rowIndex];
    var tdObj = null;
    var eCtrlIsActive = false;
    //fgObj.editCtrl="";
    if (trObj == null)
        return true;
    for (var count = 0; count < trObj.cells.length; count++) {
        tdObj = trObj.cells[count];
        if (typeof (getIAttribute(tdObj, "controltovalidate")) != 'undefined') {
            var editCtrl = getIObject(getIAttribute(tdObj, "controltovalidate"));
            if (editCtrl != null) {
                if (editCtrl.value == "")
                    eCtrlIsActive = true;
            }
        }
        var tdIt = "";
        tdIt = getTdText(tdObj);
        if (trimAll(tdIt) == "" || eCtrlIsActive) // tdObj.innerText == "*")|| (tdIt=="*"&&!$.browser.msie)
        {
            eCtrlIsActive = false;
            if (getIAttribute(tdObj, "q") == 't' && getIAttribute(tdObj, "readOnly") != 'true' || (getIAttribute(tdObj, "cu") == 't' && getIAttribute(tdObj, "validateemptytext") == 'true')) {
                TDC(tdObj);
                editCtrl = getIObject(fgObj.editCtrl);
                if (editCtrl != null)
                    editCtrl.isvalid = false;
                SetFocustoEditCtrl(fgObj);
                var ctrl = fgObj.id + '_CVali';
                if (typeof (Page_Validators) != "undefined") {
                    var val = Page_Validators[Page_Validators.length - 1];
                    if (getIAttribute(tdObj, "cu") == 't' && getIAttribute(tdObj, "q") == 'f') {
                        ValidatorValidate(val);
                        if (val.isvalid == false) {
                            fgObj.Grid_IsValid = false;
                            fgObj.hasRowChanges = false;
                            fgObj.status = "false";
                            return false;
                        }
                    }
                    else {
                        val.isvalid = false;
                        val.errormessage = getIAttribute(tdObj, "ReqErrMsg");
                        fgObj.Grid_IsValid = false;
                        Page_IsValid = false;
                        ValidatorUpdateDisplay(val);
                        fgObj.hasRowChanges = false;
                        fgObj.status = "false";
                        return false;
                    }
                }
            }
        }
    }
    return true;
}

function getGridObj(tdObj) {
    try {
        var ele = tdObj.parentNode.parentNode.parentNode;
        if (ele.tagName.toLowerCase() == "tbody")
            ele = ele.parentNode;
        return ele;
    }
    catch (e) {
        //  Console.log(e.message);
        return null;
    }
}

//HTML Input Controls while editing
function GetEditControl(obj) {
    var input;
    var fgObj = getGridObj(obj);
    var ctrlName = "";
    var value = ""//iFg_GetValues(trimAll(obj.innerText));    
    var type = getIAttribute(obj, "cT");
    switch (type) {
        case "iTextBox":
            ctrlName = GetEditCtrlName(fgObj, obj);
            fgObj.editCtrl = ctrlName;
            return GetTxtCtrl(obj, ctrlName, value)
            break;

        case "iLookup":
            ctrlName = GetEditCtrlName(fgObj, obj);
            fgObj.editCtrl = ctrlName;
            return GetLkpCtrl(obj, ctrlName, value);
            break;

        case "iCombo":
            ctrlName = GetEditCtrlName(fgObj, obj);
            fgObj.editCtrl = ctrlName;
            return GetCmbCtrl(obj, ctrlName, value);
            break;

        case "iDate":
            ctrlName = GetEditCtrlName(fgObj, obj);
            fgObj.editCtrl = ctrlName;
            return GetDatCtrl(obj, ctrlName, value);
            break;

        case "iContainer":
            ctrlName = GetEditCtrlName(fgObj, obj);
            fgObj.editCtrl = ctrlName;
            return GetCntrCtrl(obj, ctrlName, value);
            break;

        case "iHyperLink":
            ctrlName = obj.childNodes[0].id; //GetEditCtrlName(fgObj,obj);    
            fgObj.editCtrl = ctrlName;
            return GetHypCtrl(obj, ctrlName, value);
            break;

        case "iInputBox":
            ctrlName = GetEditCtrlName(fgObj, obj);
            fgObj.editCtrl = ctrlName;
            return GetIpBoxCtrl(obj, ctrlName, value);
            break;

    }
}

//Initialize EditBox controls 
function init_edtCtrl(EdtObj) {
    if (get_icFEvt() || get_icFCss() || get_icHlpEnb() != null)
        BindFocusEvent(EdtObj, "lFs();");
    if (get_icBEvt() || get_icBCss() || get_icHlpEnb() != null)
        BindBlurEvent(EdtObj, "lBr();")
    if (getIAttribute(EdtObj.parentNode, "cT") == 'iLookup') {
        init_lkpCtrl(EdtObj);
    }
    else if (getIAttribute(EdtObj.parentNode, "cT") == 'iInputBox') {
        init_ipbCtrl(EdtObj);
    }
}

//Edit Lookup Control
function GetLkpCtrl(tdObj, ctrlName, value) {
    //var value = trimAll(tdObj.innerText);      
    setIAttribute(tdObj, "controltovalidate", ctrlName);
    var fgObj = getGridObj(tdObj);
    var u = _wcU;
    input = "<input name='" + ctrlName + "' type='text' cT = '" + getIAttribute(tdObj, "cT") + "'";
    input += "maxlength = '" + getIAttribute(tdObj, "MaxLength") + "' id='" + ctrlName + "' value = '";
    input += value + "' GridClass='" + getIAttribute(tdObj, "GridClass") + "' dK='" + getIAttribute(tdObj, "dK") + "' tC = '";
    input += getIAttribute(tdObj, "tC") + "' moverClass='" + getIAttribute(tdObj, "moverClass") + "' moutClass='" + getIAttribute(tdObj, "moutClass");
    input += "' GridText='" + getIAttribute(tdObj, "GridText") + "' KeyCode='40' movercssText='" + getIAttribute(tdObj, "movercssText");
    input += "' tblName='" + getIAttribute(tdObj, "tableName") + "' doSearch='" + getIAttribute(tdObj, "dosearch") + "' cpIndex='";
    input += getIAttribute(tdObj, "cpIndex") + "' iCase = '" + getIAttribute(tdObj, "iCase") + "' moutcssText='" + getIAttribute(tdObj, "moutcssText");
    input += "' auS='" + getIAttribute(tdObj, "auS") + "' dataKey='" + getIAttribute(tdObj, "dataKey") + "' ControlToBind='";
    input += getIAttribute(tdObj, "ControlToBind") + "' DependentControl = '" + getIAttribute(tdObj, "DependentControl") + "' cT='";
    input += getIAttribute(tdObj, "cT") + "' class='" + getIAttribute(tdObj, "CssClass") + "'";  //+ "' gId='" + fgObj.id;
    if (typeof (getIAttribute(tdObj, "aSCS")) != "undefined" && getIAttribute(tdObj, "aSCS") != "") {
        input += " aSCS = '" + getIAttribute(tdObj, "aSCS") + "'";
    }
    if (typeof (getIAttribute(tdObj, "hA")) != "undefined" && getIAttribute(tdObj, "hA") != "") {
        input += " hA = '" + getIAttribute(tdObj, "hA") + "'";
    }
    if (typeof (getIAttribute(tdObj, "vA")) != "undefined" && getIAttribute(tdObj, "vA") != "") {
        input += " vA = '" + getIAttribute(tdObj, "vA") + "'";
    }
    input += "' DependentControl onclick='txtClick();' onkeydown='txtKeyDown(this);' onblur='lkpBlur(this);' onkeyup='lkp_onKeyup();'";
    input += iFg_AttachEvents(tdObj);
    input += "/>";
    if (getIAttribute(tdObj, "Visible") == 'true') {
        input += "<img src='" + getIAttribute(tdObj, "Src") + "' id= '" + ctrlName + "_img' language='javascript' name= '" + ctrlName + "_img' class='" + getIAttribute(tdObj, "ImgClass") + "'";
        if (typeof (getIAttribute(tdObj, "ialign")) != "undefined")
            input += "align = '" + getIAttribute(tdObj, "ialign") + "'";
        else
            input += "align = 'AbsMiddle'";
        input += "/>";
    }
    if (getIAttribute(tdObj, "validate") == 'true') {
        input += "<span controltovalidate='" + ctrlName + "' id= '" + fgObj.id + "_CVali' Name= '" + fgObj.id + "_CVali' validateemptytext='" + getIAttribute(tdObj, "validateemptytext") + "' style='color:Red;visibility:hidden;'";
        input += ">*</span>";
    }
    return input;
}

//Edit Multiline TextBox Control
function GetIpBoxCtrl(tdObj, ctrlName, value) {
    //var value = trimAll(tdObj.innerText);      
    setIAttribute(tdObj, "controltovalidate", ctrlName);
    var fgObj = getGridObj(tdObj);
    var u = fgObj.u;
    input = "<input type='text' name='" + ctrlName + "' id='" + ctrlName + "' u = '" + u + "' maxlength = '" + getIAttribute(tdObj, "MaxLength") + "' cT = '" + getIAttribute(tdObj, "cT") + "' onchange = 'iFg_txtChange(this);' class='" + getIAttribute(tdObj, "CssClass") + "' value = '" + value + "' onclick='txtClick();' onblur = 'txtBlur(this);' onkeydown='txtKeyDown(this);'";
    input += iFg_AttachEvents(tdObj);
    input += "/>";
    if (getIAttribute(tdObj, "Visible") == 'true')
        input += "<img src='" + getIAttribute(tdObj, "Src") + "' id= '" + ctrlName + "_img' language='javascript' name= '" + ctrlName + "_img' class='" + getIAttribute(tdObj, "ImgCssClass") + "'/>";
    if (getIAttribute(tdObj, "validate") == 'true') {
        input += "<span controltovalidate='" + ctrlName + "' id= '" + fgObj.id + "_CVali' Name= '" + fgObj.id + "_CVali' validateemptytext='" + getIAttribute(tdObj, "validateemptytext") + "' style='color:Red;visibility:hidden;'";
        input += ">*</span>";
    }
    return input;
}

//Edit Textbox Control
function GetTxtCtrl(tdObj, ctrlName, value) {
    if (typeof (getIAttribute(tdObj, "tM")) != "undefined" && getIAttribute(tdObj, "tM") == "ML")
        return GetMLTxtCtrl(tdObj, ctrlName);
    //var value = trimAll(tdObj.innerText);     
    setIAttribute(tdObj, "controltovalidate", ctrlName);
    var fgObj = getGridObj(tdObj);
    var u = getGridObj(tdObj).u;
    input = '<input type="text" name="' + ctrlName + '" id="' + ctrlName + '" u = "' + u + '" maxlength = "' + getIAttribute(tdObj, "MaxLength") + '" cT = "' + getIAttribute(tdObj, "cT") + '" onchange = "iFg_txtChange(this);" class="' + getIAttribute(tdObj, "CssClass") + '" value = "' + value + '" onclick="txtClick();" onblur = "txtBlur(this);" onkeydown="txtKeyDown(this);" iCase = "' + getIAttribute(tdObj, "iCase") + '"';
    input += iFg_AttachEvents(tdObj);
    input += '/>';
    if (getIAttribute(tdObj, "validate") == 'true') {
        input += '<span controltovalidate="' + ctrlName + '" id= "' + fgObj.id + '_CVali" Name= "' + fgObj.id + '_CVali" validateemptytext="' + getIAttribute(tdObj, "validateemptytext") + '" style="color:Red;visibility:hidden;"';
        input += '>*</span>';
    }
    return input;
}

//Bound Control
function GetBndCtrl(tdObj, ctrlName, value) {
    //var value = trimAll(tdObj.innerText);      
    setIAttribute(tdObj, "controltovalidate", ctrlName);
    var fgObj = getGridObj(tdObj);
    var u = getGridObj(tdObj).u;
    input = "<input type='text' name='" + ctrlName + "' id='" + ctrlName + "' u = '" + u + "'cT = '" + getIAttribute(tdObj, "cT") + "' onchange = 'iFg_txtChange(this);' class='" + getIAttribute(tdObj, "CssClass") + "' value = '" + value + "' onclick='txtClick();' onblur = 'txtBlur(this);' onkeydown='txtKeyDown(this);'";
    input += "/>";
    return input;
}

//Edit Date Control
function GetDatCtrl(tdObj, ctrlName, value) {
    //var value = trimAll(tdObj.innerText);      
    setIAttribute(tdObj, "controltovalidate", ctrlName);
    var fgObj = getGridObj(tdObj);
    var u = fgObj.u;
    input = "<input type='text' name='" + ctrlName + "' id='" + ctrlName + "' u = '" + u + "' maxlength = '" + getIAttribute(tdObj, "MaxLength") + "' cT = '" + getIAttribute(tdObj, "cT") + "' onchange = 'iFg_txtChange(this);' class='" + getIAttribute(tdObj, "CssClass") + "' value = '" + value + "' onclick='txtClick();' onkeydown='txtKeyDown(this);' onblur = 'txtBlur(this);'";
    input += iFg_AttachEvents(tdObj);
    input += "/>";
    if (getIAttribute(tdObj, "Visible") == 'true')
        input += "<img src='" + getIAttribute(tdObj, "Src") + "' id= '" + ctrlName + "_img' language='javascript' name= '" + ctrlName + "_img' class='" + getIAttribute(tdObj, "ImgCssClass") + "'/>";
    if (getIAttribute(tdObj, "validate") == 'true') {
        input += "<span controltovalidate='" + ctrlName + "' id= '" + fgObj.id + "_CVali' Name= '" + fgObj.id + "_CVali' validateemptytext='" + getIAttribute(tdObj, "validateemptytext") + "' style='color:Red;visibility:hidden;'";
        input += ">*</span>";
    }
    return input;
}

function DateImgClick(obj) {
    var ctrlName = obj.childId;
    var tdObj = getIObject(obj.tdId);
    var fgObj = getGridObj(tdObj);
    if (fgObj.Search == true)
        iFg_HideFilterDiv(fgObj);
    var _y, _x;
    var event = getEvent(event);
    if (event) {
        if (msie) { _y = event.clientY; _x = event.clientX }
        else { _y = event.pageY; _x = event.pageX }
    }
    GetDatePicker(getIObject(ctrlName), getIAttribute(tdObj, "Lp"), parseInt(getIAttribute(tdObj, "Tp")));
    //GetDatePicker(getIObject(ctrlName),_y+10, getIAttribute(tdObj, "Lp"), getIAttribute(tdObj, "Tp"))
    iFg_CancelBubble();
}

function IpBImgClick(obj) {
    var ctrlName = obj.childId;
    var tdObj = getIObject(obj.tdId);
    //GetDatePicker(getIObject(ctrlName),tdObj.Lp,tdObj.Tp);    
    iFg_CancelBubble();
}

//Edit Combo Control
function GetCmbCtrl(tdObj, ctrlName) {
    var value = trimAll(getText(tdObj));
    setIAttribute(tdObj, "controltovalidate", ctrlName);
    var fgObj = getGridObj(tdObj);
    var u = getGridObj(tdObj).u;
    input = "<input type='text' name='" + ctrlName + "' id='" + ctrlName + "' class='" + getIAttribute(tdObj, "CssClass") + "' u = '" + u + "' cT = '" + getIAttribute(tdObj, "cT") + "' value = '" + value + "' onclick='txtClick();' onkeydown='txtKeyDown(this);'/>";
    if (getIAttribute(tdObj, "validate") == 'true') {
        input += "<span controltovalidate='" + ctrlName + "' id= '" + fgObj.id + "_CVali' Name= '" + fgObj.id + "_CVali' validateemptytext='" + getIAttribute(tdObj, "validateemptytext") + "' style='color:Red;visibility:hidden;'";
        input += ">*</span>";
    }
    return input;
}

//Edit Container Control
function GetCntrCtrl(tdObj, ctrlName, value) {
    //var value = trimAll(tdObj.innerText);      
    setIAttribute(tdObj, "controltovalidate", ctrlName);
    var fgObj = getGridObj(tdObj);
    var u = fgObj.u;
    var lpCheckDigit = GetBoolFromString(getIAttribute(tdObj, "CheckDigit"));
    input = "<input type='text' name='" + ctrlName + "' id='" + ctrlName + "' u = '" + u + "' maxlength = '" + getIAttribute(tdObj, "MaxLength") + "' value = '" + value + "' class='" + getIAttribute(tdObj, "CssClass") + "' lpCheckDigit= '" + lpCheckDigit + "' cT = '" + getIAttribute(tdObj, "cT") + "' onchange = 'iFg_txtChange(this);' onclick='txtClick();' onkeydown='txtKeyDown(this);' onblur = 'txtBlur(this);'";
    input += iFg_AttachEvents(tdObj);
    input += "/>";
    if (getIAttribute(tdObj, "validate") == 'true') {
        input += "<span controltovalidate='" + ctrlName + "' id= '" + fgObj.id + "_CVali' Name= '" + fgObj.id + "_CVali' validateemptytext='" + getIAttribute(tdObj, "validateemptytext") + "' style='color:Red;visibility:hidden;'";
        input += ">*</span>";
    }
    return input;
}

//Edit Textbox Control
function GetHypCtrl(tdObj, ctrlName) {
    var value = trimAll(getText(tdObj));
    tdObj.href = tdObj.childNodes[0].href;
    var fgObj = getGridObj(tdObj);
    var u = getGridObj(tdObj).u;
    input = "<input type='text' name='" + ctrlName + "' id='" + ctrlName + "' cT = '" + getIAttribute(tdObj, "cT") + "' value = '" + value + "' onclick='txtClick();' onblur = 'txtBlur(this);' onkeydown='txtKeyDown(this);'";
    input += iFg_AttachEvents(tdObj);
    input += "/>";
    return input;
}

function iFg_OMLBox() {
    //window.show
}

function iFg_AttachEvents(tdObj) {
    var htmlInput = "";
    var iCase = getIAttribute(tdObj, "iCase");
    if (iCase == 'Upper') {
        htmlInput = "onkeypress = 'enableUpperCaseAlways(event);' onpaste = 'enableUpperCaseOnPaste(event);lPs(this);'";
        return htmlInput;
    }
    else if (iCase == 'Lower') {
        htmlInput = "onkeypress = 'enableLowerCaseAlways(event);' onpaste = 'enableLowerCaseOnPaste(event);lPs(this);'";
        return htmlInput;
    }
    else if (iCase == 'Numeric') {
        htmlInput = "onkeypress = 'enableNumericOnly(event);' onpaste = 'enableNumericOnPaste();lPs(this);'";
        return htmlInput;
    }
    else {
        htmlInput = "onpaste = 'lPs(this);'";
        return htmlInput;
    }
}
function lPs(obj) {
    obj.IsCheckedOnKd = true;
    obj.hasChanges = true;
}
//Naming EditBox controls
function GetEditCtrlName(fgObj, obj) {
    var type = getIAttribute(obj, "cT");
    var trObj = obj.parentNode;
    var typeName = "";
    switch (type) {
        case "iTextBox":
            typeName = '_txt';
            break;

        case "iLookup":
            typeName = '_lkp';
            break;

        case "iCombo":
            typeName = '_cmb';
            break;

        case "iDate":
            typeName = '_dat';
            break;

        case "iContainer":
            typeName = '_cntr'
            break;

        case "iHyperLink":
            typeName = '_hyp'
            break;
    }
    if (fgObj.Search != true)
        return fgObj.id + "_" + typeName + "_" + getIAttribute(trObj, "rIndex") + "_" + obj.cellIndex;
    else
        return fgObj.id + "_" + typeName + "_" + getIAttribute(trObj, "rIndex") + "_SR" + "_" + obj.cellIndex;
}

/* Validation Functions */
//Add Validators to page 
function AddValidators(tdObj) {
    var fgObj = getGridObj(tdObj);
    var ctrl = fgObj.id + '_CVali';
    var flag = true;
    if (typeof (Page_Validators) != "undefined") {
        for (var count = 0; count < Page_Validators.length; count++) {
            if (Page_Validators[count].id == ctrl) {
                flag = false;
                break;
            }
        }
        if (flag == false) {
            Page_Validators.splice(count, 1);
        }
        Page_Validators[Page_Validators.length] = getIObject(ctrl);
        if (typeof (getIAttribute(fgObj, "valGrp")) != "undefined")
            Page_Validators[Page_Validators.length - 1].validationGroup = getIAttribute(fgObj, "valGrp");
    }
    else {
        Page_Validators = new Array(getIObject(ctrl));
        if (typeof (getIAttribute(fgObj, "valGrp")) != "undefined")
            Page_Validators[Page_Validators.length - 1].validationGroup = getIAttribute(fgObj, "valGrp");
    }
    ValidatorDGonLoad(tdObj);
}

//Initialize Validate controls 
function ValidatorDGonLoad(tdObj) {
    if (typeof (Page_ValidationActive) == "undefined") {
        var Page_ValidationActive = false; //return;        
    }
    var val;
    val = Page_Validators[Page_Validators.length - 1];
    val.clientvalidationfunction = "CustomDGBoxValidation";

    val.evaluationfunction = function (val) { return CustomDGValidatorEvaluateIsValid(val, tdObj); };
    if (typeof (val.isvalid) == "string") {
        if (val.isvalid == "False") {
            val.isvalid = false;
            Page_IsValid = false;
        }
        else {
            val.isvalid = true;
        }
    } else {
        val.isvalid = true;
    }
    if (typeof (val.enabled) == "string") {
        val.enabled = (val.enabled != "False");
    }
    setIAttribute(val, "controltovalidate", getIAttribute(tdObj, "controltovalidate"));
    if (val.validationGroup == "" || typeof (val.validationGroup) == "undefined") {
        val.validationGroup = getIAttribute(tdObj, "valGrp");
    }
    val.focusOnError = "t";
    ValidatorHookupControlID(getIAttribute(val, "controltovalidate"), val);
    ValidatorHookupControlID(val.controlhookup, val);
    Page_ValidationActive = true;
}

//Datagrid Custom Evaluation function
function CustomDGValidatorEvaluateIsValid(val, tdObj) {
    var value = "";
    if (typeof (getIAttribute(val, "controltovalidate")) == "string") {
        value = ValidatorGetValue(getIAttribute(val, "controltovalidate"));
        if ((ValidatorTrim(value).length == 0) &&
            ((typeof (getIAttribute(val, "validateemptytext")) != "string") || (getIAttribute(val, "validateemptytext") != "true"))) {
            var fgObj = getGridObj(val);
            if (fgObj)
                fgObj.Grid_IsValid = true;
            return true;
        }
    }
    var args = { Value: value, IsValid: true };
    if (typeof (val.clientvalidationfunction) == "string") {
        eval(val.clientvalidationfunction + "(tdObj, args) ;");
    }
    return args.IsValid;
}

//Datagrid Custom Validation Function
function CustomDGBoxValidation(oSrc, args) {
    var fgObj = getGridObj(oSrc);
    var ctrl = getIObject(fgObj.id + '_CVali');
    if (oSrc == null)
        return;
    var _IsValid = true;
    if (getIAttribute(oSrc, "q") == "t") {
        _IsValid = RequiredFieldValidatorEvaluateIsValid(oSrc, args);
        fgObj.Grid_IsValid = _IsValid;
    }
    if (_IsValid == false) {
        ctrl.errormessage = getIAttribute(oSrc, "ReqErrMsg");
        args.IsValid = _IsValid;
        return;
    }
    if (getIAttribute(oSrc, "g") == "t") {
        _IsValid = RegularExpressionValidatorEvaluateIsValid(oSrc);
        fgObj.Grid_IsValid = _IsValid;
    }
    if (_IsValid == false) {
        ctrl.errormessage = getIAttribute(oSrc, "RegErrMsg");
        args.IsValid = _IsValid;
        return;
    }
    //if (oSrc.l=="t" && args.Value!="")
    if (getIAttribute(oSrc, "l") == "t" && (args.Value != "" || (typeof (getIAttribute(oSrc, "validateemptytext")) == "string" && getIAttribute(oSrc, "validateemptytext") == "true"))) {
        oSrc.errormessage = getIAttribute(oSrc, "LkpErrMsg");
        _IsValid = CustomLookupValidation(oSrc, args);
        fgObj.Grid_IsValid = _IsValid;
        if (_IsValid == false) {
            ctrl.errormessage = getIAttribute(oSrc, "LkpErrMsg");
            args.IsValid = _IsValid;
            return;
        }
    }
    if (getIAttribute(oSrc, "n") == "t") {
        _IsValid = RangeValidatorEvaluateIsValid(oSrc);
        fgObj.Grid_IsValid = _IsValid;
    }
    if (_IsValid == false) {
        ctrl.errormessage = getIAttribute(oSrc, "RngErrMsg");
        args.IsValid = _IsValid;
        return;
    }
    if (getIAttribute(oSrc, "cm") == "t") {
        if (getIAttribute(oSrc, "controltocompare") != "") {
            var rowIndex = parseInt(getIAttribute(oSrc.parentNode, "rIndex")) + parseInt(fgObj.hdrRows);
            var compareTo = getText(fgObj.rows[rowIndex].cells[getIAttribute(oSrc, "controltocompare")]);
            if (trimAll(compareTo) == "") {
                if (ValidateCell(oSrc, getIAttribute(oSrc, "controltocompare"))) {
                    _IsValid = DGCompareValidatorEvaluateIsValid(oSrc);
                    fgObj.Grid_IsValid = _IsValid;
                }
            }
            else {
                _IsValid = DGCompareValidatorEvaluateIsValid(oSrc);
                fgObj.Grid_IsValid = _IsValid;
            }
        }
        else {
            _IsValid = DGCompareValidatorEvaluateIsValid(oSrc);
            fgObj.Grid_IsValid = _IsValid;
        }
    }
    if (_IsValid == false) {
        ctrl.errormessage = getIAttribute(oSrc, "CmpErrMsg");
        args.IsValid = _IsValid;
        return;
    }
    if (getIAttribute(oSrc, "cu") == "t") {
        oSrc.clientvalidationfunction = getIAttribute(oSrc, "valfn");
        oSrc.errormessage = '';
        _IsValid = CustomValidatorEvaluateIsValid(oSrc);
        fgObj.Grid_IsValid = _IsValid;
        oSrc.clientvalidationfunction = "CustomDGBoxValidation";
    }
    if (_IsValid == false) {
        if (getIAttribute(oSrc, "CsvErrMsg") != '' || oSrc.errormessage != '') {
            if (oSrc.errormessage != "") {
                ctrl.errormessage = oSrc.errormessage;
            }
            else {
                ctrl.errormessage = getIAttribute(oSrc, "CsvErrMsg");
            }
        }
        args.IsValid = _IsValid;
        return;
    }
}

function DGCompareValidatorEvaluateIsValid(val) {
    var value = ValidatorGetValue(getIAttribute(val, "controltovalidate"));
    if (ValidatorTrim(value).length == 0)
        return true;
    var compareTo = "";
    if (getIAttribute(val, "controltocompare") != "") {
        var fgObj = getGridObj(val);
        var rowIndex = parseInt(getIAttribute(val.parentNode, "rIndex")) + parseInt(fgObj.hdrRows);
        compareTo = getText(fgObj.rows[rowIndex].cells[getIAttribute(val, "controltocompare")]);
    }
    else if (typeof (getIAttribute(val, "valuetocompare")) == "string") {
        compareTo = getIAttribute(val, "valuetocompare");
    }
    return ValidatorCompare(value, compareTo, getIAttribute(val, "operator"), val);
}

function iFg_gps(args, fgObj) {
    if (fgObj.hasRowChanges) {
        if (fgObj.Grid_IsValid == true) {
            GetRowValues(fgObj, fgObj.rowIndex);
            SaveData(fgObj);
            fgObj = iFg_ResetGridObj(fgObj);
            if (fgObj.status == "false")
                return false;
        }
        else {
            return;
        }
    }
    iFg_CancelBubble();
    if (fgObj.readOnly == false && fgObj.Search == false && fgObj.Grid_IsValid != false) // &&  fgObj.mode != "Add")
    {
        fgObj.mode = "ps";
        fgObj.async = true;
        GCB(fgObj, args)
    }
}

function iFg_Sort(args, fgObj) {
    if (fgObj.Search == true)
        return;
    if (fgObj.hasRowChanges) {
        if (fgObj.Grid_IsValid == true) {
            GetRowValues(fgObj, fgObj.rowIndex);
            SaveData(fgObj);
            fgObj = iFg_ResetGridObj(fgObj);
            if (fgObj.status == "false")
                return false;
        }
        else {
            return;
        }
    }
    iFg_CancelBubble();
    if (fgObj.readOnly == false && fgObj.Search == false && fgObj.Grid_IsValid != false) // &&  fgObj.mode != "Add")
    {
        fgObj.mode = "Sort";
        fgObj.async = true;
        GCB(fgObj, args)
    }
}
function iFg_OnPageSizeChange(obj, fgObj) {
    if (obj.value == "0" || fgObj.Search == true || fgObj.Grid_IsValid == false) {
        if (event)
            event.returnValue = false;
        return false;
    }
    fgObj.pageSize = obj.value;
    fgObj.ctrlType = 'iGrid';
    fgObj.mode = "ReBind";
    fgObj.showAll = false;
    if (fgObj.rC == obj.value) {
        fgObj.showAll = true;
    }
    fgObj.async = true;
    GCB(fgObj);
}
function iFg_getHiddenFieldContents(arg, fgObj) {
    fgObj = document.all[fgObj.id];
    return arg + "|" + fgObj.stateField.value;
}
function iFg_createPropertyStringFromValues(pageIndex, sortDirection, sortExpression, dataKeys, rowCount) {
    value = new Array(pageIndex, sortDirection, sortExpression, dataKeys, rowCount);
    return value.join("|");
}
function iFg_createPropertyString() {
    return iFg_createPropertyStringFromValues(this.pageIndex, this.sortDirection, this.sortExpression, this.dataKeys);
}
function iFg_setStateValue() {
    this.stateField.value = this.createPropertyString();
}
function iFg_BindData(result, context, parentfgId) {
    var parentObj = getIObject(parentfgId);
    valsArray = result.split("|");
    innerHtml = valsArray[5];
    if (typeof (context) == "string") {
        for (i = 6; i < valsArray.length - 24; i++) {
            innerHtml += "|" + valsArray[i];
        }
        var pE = "";
        pE = "__fg" + context + "__div";
        var pRI;
        if (typeof (context) == "string") {
            var _c = document.all[context];
            if (_c)
                pRI = _c.parentRowIndex
        }
        var id = context;
        context = getIObject(id);
        var _hc = false;
        if (context != null) {
            _hc = context.hasChanges;
        }
        el(pE).innerHTML = innerHtml; //document.all[pE].innerHTML = innerHtml;       
        if (context == null)
            context = getIObject(id);
        iFg_getCallbackResult(context, valsArray);
        if (parentObj)
            pRI = parseInt(parentObj.pageIndex) * parseInt(parentObj.pageSize) + parentObj.exRowIndex - parseInt(parentObj.hdrRows);
        var rC = valsArray[valsArray.length - 18];
        Init_GridRS(context.id, context.allowAdd, context.allowDelete, context.allowEdit, context.pageIndex, context.pageSize, pE, context.hdrRows, rC, context.allowSearch, context.sHH, context.aF, pRI, context, _hc, context.fPI, context.allowRefresh);
    }
    else {
        for (i = 6; i < valsArray.length - 24; i++) {
            innerHtml += "|" + valsArray[i];
        }
        var _hc = context.hasChanges;
        var _pRI = "";
        if (context.parentRowIndex)
            _pRI = context.parentRowIndex;
        context.panelElement.innerHTML = innerHtml;
        context = getIObject(context.id);
        iFg_getCallbackResult(context, valsArray);
        var rC = valsArray[valsArray.length - 18];
        Init_GridRS(context.id, context.allowAdd, context.allowDelete, context.allowEdit, context.pageIndex, context.pageSize, null, context.hdrRows, rC, context.allowSearch, context.sHH, context.aF, _pRI, context, _hc, context.fPI, context.allowRefresh);
    }
    if (document.all[context.id] && document.all[context.id].stateField) {
        document.all[context.id].stateField.value = iFg_createPropertyStringFromValues(valsArray[1], valsArray[2], valsArray[3], valsArray[4], context.rC).toString();
    }
}
function iFg_getCallbackResult(context, valsarray) {
    context.pageIndex = valsArray[1];
    context.allowAdd = valsArray[valsArray.length - 24];
    context.allowDelete = valsArray[valsArray.length - 23];
    context.allowEdit = valsArray[valsArray.length - 22];
    context.fPI = valsArray[valsArray.length - 21];
    context.pageSize = valsArray[valsArray.length - 20];
    context.hdrRows = valsArray[valsArray.length - 19];
    context.allowSearch = valsArray[valsArray.length - 17];
    context.sHH = valsArray[valsArray.length - 16];
    context.aF = valsArray[valsArray.length - 15];
    context.allowRefresh = valsArray[valsArray.length - 14];
    context.aBt = valsArray[valsArray.length - 13];
    context.dBt = valsArray[valsArray.length - 12];
    context.sBt = valsArray[valsArray.length - 11];
    context.cBt = valsArray[valsArray.length - 10];
    context.scBt = valsArray[valsArray.length - 9];
    context.pBt = valsArray[valsArray.length - 8];
    context.rBt = valsArray[valsArray.length - 7];
    context.sDv = valsArray[valsArray.length - 6];
    context.aRCP = valsArray[valsArray.length - 5];
    context.aBIC = valsArray[valsArray.length - 4];
    context.dBIC = valsArray[valsArray.length - 3];
    context.cBIC = valsArray[valsArray.length - 2];
    context.sCBIC = valsArray[valsArray.length - 1];

}
function iFg_DeleteData(fgObj, event) {
    fgObj = document.all[fgObj.id];
    if (fgObj.DeleteButton) {
        var val = trimAll(getText(fgObj.DeleteButton));
        if (val == "Cancel") {
            iFg_CancelSearch(fgObj);
            return;
        }
    }
    if (fgObj.readOnly == false) {
        iFg_CancelBubble(event);
        if (fgObj.editCtrl != "") {
            var obj = getIObject(fgObj.editCtrl);
            if (obj != null && obj != "" && getIAttribute(obj.parentNode, "cT") == 'iLookup') {
                iFg_HideLkpGrid(obj);
            }
            if (obj != null && typeof (obj.isvalid) != "undefined" && obj.isvalid == false) {
                ResetLookupValidator(fgObj.editCtrl, true);
                fgObj.Grid_IsValid = true;
            }
        }
        if (fgObj.Grid_IsValid != false) {
            if (fgObj.rowIndex.toString() != "") {
                fgObj.mode = "Delete";
                var pRS = fgObj.rowState;
                fgObj.rowState = "Deleted";
                fgObj.hasRowChanges = true;
                fgObj.hasChanges = true;
                GetRowValues(fgObj, fgObj.rowIndex);
                GCB(fgObj, null, null, pRS);
            }
            else {
                alert('Please select a row to Delete!');
            }
        }
    }
}

function iFg_PageIndex(fgObj) {
    fgObj = document.all[fgObj.id];
    var pI = fgObj.pageIndex;
    var pS = fgObj.pageSize;
    var rC = fgObj.rC;
    var rem = (parseInt(rC) + 1) % parseInt(pS);
    var lpI = parseInt((parseInt(rC) + 1) / parseInt(pS));
    if (rem == 0)
        fgObj.pageIndex = lpI - 1;
    else
        fgObj.pageIndex = 2;
}

function iFg_bRC(fgObj) {
    if (typeof (getIAttribute(fgObj, "bRC")) == "string" && getIAttribute(fgObj, "bRC") != "") {
        return eval(getIAttribute(fgObj, "bRC") + "()");
    }
}

function iFg_aRC(fgObj, rI) {
    if (typeof (getIAttribute(fgObj, "aRC")) == "string" && getIAttribute(fgObj, "aRC") != "") {
        return eval(getIAttribute(fgObj, "aRC") + "('" + rI + "')");
    }
}

function iFg_Parameters() {
    var paramArray = new Array();
    this.add = function (name, value) {
        paramArray[name] = value;
    }
    this.getParam = function () {
        return paramArray;
    }
}

function iFg_Submit(fgObj, fireReqValidators, fireAllValidators) {
    if (fgObj.hasChanges == true || fgObj.rowState == "Added")// && fgObj.status != 'false')
    {
        fg_docClick(fgObj.id);
        fgObj = document.all[fgObj.id];
        fgObj.hasRowChanges = false;
        if (fgObj.status == "false") {
            return false;
        }
    }
    if (fgObj.mode == 'Search') {
        return false;
    }
    if (fireReqValidators)
        iFg_fRV(fgObj);
    if (fireAllValidators)
        iFg_fAV(fgObj);
    if (fgObj.Grid_IsValid == false) {
        //fgObj.hasChanges = true;
        return false;
    }
    if (iFg_CheckIsNumeric(fgObj.exRowIndex)) {
        var childfgObj = getIObject(getIAttribute(fgObj.rows[fgObj.exRowIndex].cells[fgObj.exCellIndex], "gridId"));
        iFg_CheckHasChanges(fgObj, childfgObj);
        if (iFg_Submit(childfgObj))
            iFg_CheckHasChanges(fgObj, childfgObj);
        else
            return false;
    }
    return true;
}

function iFg_fRV(fgObj) {
    var hdrRows = parseInt(fgObj.hdrRows);
    var length = fgObj.rows.length - hdrRows;
    for (var count = hdrRows; count < fgObj.rows.length; count++) {
        fgObj.Grid_IsValid = true;
        fgObj.rowIndex = getIAttribute(fgObj.rows[count], "rIndex");
        ValidateAddedRow(fgObj);
        if (fgObj.Grid_IsValid == false)
            break;
    }
}

function iFg_fAV(fgObj) {
    var hdrRows = parseInt(fgObj.hdrRows);
    var rLen = fgObj.rows.length; //- hdrRows;    
    var cLen = fgObj.rows[hdrRows].cells.length;
    var obj = null;
    var tdObj = null;
    for (var rcount = hdrRows; rcount < rLen; rcount++) {
        for (var cCount = 0; cCount < cLen; cCount++) {
            tdObj = fgObj.rows[rcount].cells[cCount];
            if (getIAttribute(tdObj, "readOnly") != 'true' || getIAttribute(tdObj, "cT") != 'Expand') {
                TDC(tdObj);
                obj = getIObject(fgObj.editCtrl);
                if (obj != null && getIAttribute(tdObj, "validate") == 'true') {
                    ValidatorOnChange(obj.id);
                    if (obj.isvalid == false) {
                        //fgObj.status = false;
                        break;
                    }
                }
            }
        }
    }
}

function iFg_CheckHasChanges(fgObj, childfgObj) {
    if (fgObj.hasChanges != true) {
        if (childfgObj.hasChanges == true) {
            fgObj.hasChanges = true;
        }
    }
}

function iFg_CheckhasRowChanges(fgObj) {
    if (fgObj.hasRowChanges == true) {
        return true;
    }
    else if (iFg_CheckIsNumeric(fgObj.exRowIndex)) {
        var childfgObj = getIObject(getIAttribute(fgObj.rows[fgObj.exRowIndex].cells[fgObj.exCellIndex], "gridId"));
        if (childfgObj != null && childfgObj.hasRowChanges == true) {
            return true;
        }
        else {
            return iFg_CheckhasRowChanges(childfgObj)
        }
    }
    return false;
}

function iFg_GetRSAfgObj(fgObj, rI) {
    if (fgObj.rowState == 'Added') {
        return fgObj;
    }
    return null;
}

function iFg_GetExpandCtrl(fgObj, _Row, _Col) {
    try {
        if (fgObj.rows[eval(_Row)].cells[_Col].childNodes[0].tagName.toLowerCase() == "img")
            return fgObj.rows[eval(_Row)].cells[_Col].childNodes[0];
    } catch (e) {
        return null
    }
}

function iFg_CheckIsNumeric(rI) {
    if (typeof (rI) != "undefined" && rI >= 0 && rI.toString() != "")
        return true;
    return false;
}

function iFg_SetRowIndex(fgObj, rIndex, rstRI) {
    fgObj = document.all[fgObj.id]
    if (rstRI == null && fgObj.rowIndex.toString() != "") {
        iFg_SetRowIndex(fgObj, fgObj.rowIndex, true)
    }
    fgObj.rowIndex = rIndex;
    var rowIndex = iFg_GRI(fgObj);
    var trObj = iFg_GRO(fgObj);
    if (trObj != null) {
        if (rstRI || fgObj.rowIndex.toString() == "")
            GridMouseOut(trObj.id);
        else
            GridMouseOver(trObj.id);
    }
}

function GridMouseOver(_rowId) {
    if (getIObject(_rowId) && getIAttribute(getIObject(_rowId), "MoverClass") && getIAttribute(getIObject(_rowId), "MoverClass").toString() != "")
        getIObject(_rowId).className = getIAttribute(getIObject(_rowId), "MoverClass").toString();
}
function GridMouseOut(_rowId) {
    if (getIObject(_rowId) && getIAttribute(getIObject(_rowId), "MoutClass") && getIAttribute(getIObject(_rowId), "MoutClass").toString() != "")
        getIObject(_rowId).className = getIAttribute(getIObject(_rowId), "MoutClass").toString();
}

function iFg_AddRowEvents(_id) {
    var count;
    var fgObj = getIObject(_id);
    var obj = null;
    for (count = fgObj.hdrRows; count < fgObj.rows.length; count++) {
        obj = fgObj.rows[count];
        obj.tabIndex = -1;
        if (obj.addEventListener) {
            obj.addEventListener("onclick", new Function("iFg_GRC();"), false);
            obj.addEventListener("onfocus", new Function("GridMouseOver('" + obj.id + "');"), false);
            obj.addEventListener("onblur", new Function("GridMouseOut('" + obj.id + "');"), false);
        }
        else {
            obj.attachEvent('onclick', new Function("iFg_GRC();"));
            obj.onfocus = new Function("GridMouseOver('" + obj.id + "');");
            obj.onblur = new Function("GridMouseOut('" + obj.id + "');");
        }
    }
}
function iFg_GRC() {
    iFg_CancelBubble();
}

function iFg_GRO(fgObj) {
    var rI = iFg_GRI(fgObj);
    var trObj = null;
    if (rI != "") {
        trObj = fgObj.rows[rI];
    }
    return trObj;
}

function iFg_GRI(fgObj) {
    var rI = "";
    if (fgObj.rowIndex.toString() != "")
        rI = parseInt(fgObj.rowIndex) + parseInt(fgObj.hdrRows);
    return rI;
}
function ShowMultiLineBox() {
    var obj = event.srcElement;
    var tdObj = obj.parentNode;
    var fgObj = getGridObj(tdObj);
    var path = window.location.pathname;
    var currUrl = path.split("/");
    var currpath = currUrl[1] + "/MultiLineBox.aspx";
    var loc = window.location;
    var _url = loc.protocol + "//" + loc.host + "/" + currpath;
    var args = new Object();
    if (typeof (obj.v) == 'undefined') {
        obj.v = "";
    }
    args.value = obj.v;
    args.title = "Enter the Value";
    args.returnValue = "";
    _url += "?r=" + getIAttribute(tdObj, "q") + "&mL=" + getIAttribute(tdObj, "mlen") + "&val=" + obj.v;
    var _returnvalue = ShowModalPopupWindow(_url, "400px", "275px", args);
    if (typeof (_returnvalue) == "string" || (typeof (_returnvalue) == "boolean" && _returnvalue != false)) {
        obj.v = _returnvalue;
        fgObj.hasRowChanges = true;
        fgObj.hasChanges = true;
        fgObj.editCtrl = obj.id;
    }
}
function iFg_ShowProgress(fgObj) {
    var pb = getIObject("ifgpb");
    if (pb) {
        //pb.innerHTML = "Loading";
        var _h; //= 50;
        if (typeof (fgObj.sHH) != "undefined" && fgObj.sHH != "") {
            _h = parseInt(fgObj.sHH) / 2;
        }
        else {
            _h = fgObj.clientHeight / 2;
        }
        pb.style.top = fgObj.scrollTop + _h;
        pb.style.left = fgObj.scrollLeft + fgObj.clientWidth / 2;
        pb.style.display = "block";
    }
    else {
        iFg_CreateProgress();
        iFg_ShowProgress(fgObj);
    }
}
function iFg_HideProgress(fgObj) {
    var pb = getIObject("ifgpb");
    if (pb) {
        pb.style.display = "none";
    }
}
function iFg_UpdateProgress(fgObj) {

}
function iFg_CreateProgress() {
    var c = document.createElement("DIV");
    c.id = "ifgpb";
    c.style.position = "absolute";
    c.style.top = "0px";
    c.style.left = "0px";
    c.style.zIndex = 9999;
    c.style.display = "none";
    c.className = "pb";
    //c.innerHTML = "<iframe scrolling='no' width='100%' height='100%' class='pb'></iframe>";
    window.document.body.appendChild(c);
}

function iFg_DisablePS(fgObj) {
    var obj = getIObject(fgObj.id + "_dPS");
    if (obj != null) {
        if (obj.disabled) {
            obj.disabled = false;
        }
        else {
            obj.disabled = true;
        }
    }
    /*var expobj=getIObject(fgObj.id + "_prnt");
    if(expobj != null)
    {
    expobj.style.display="none";
    }
    var refobj=getIObject(fgObj.id + "_rfb");
    if(refobj != null)
    {
    refobj.style.display="none";
    }*/
}

function iFg_ResetGridObj(fgObj) {
    return getIObject(fgObj.id);
}

function iFg_GetValues(str) {
    str = str.replace('\"', '"');
    //str = str.replace("\\","\\\");
    return str.replace("\'", "'");
}
function iFg_SetValues(str) {
    str = str.replace('"', '\"');
    //str = str.replace("\","\\");
    return str.replace("'", "\'");
}
function iFg_SetProperty(fgObj, p, v) {
    try {
        switch (p) {
            case "display":
                fgObj.panelElement.style.display = v;
                break;
            case "visibility":
                fgObj.panelElement.style.visibility = v;
                break;
        }
    } catch (e) {
        alert("Invalid Property name or Value.\n Inner Exception: " + e.Message);
    }
}
//Refresh
function iFg_RefreshClick(fgObj) {
    if (fgObj.Grid_IsValid == false || fgObj.Search == true)
        return;
    fg_docClick(fgObj.id);
    if (fgObj.Grid_IsValid == false)
        return;
    fgObj.showAll = false;
    fgObj.mode = "Refresh";
    fgObj.param = "";
    GCB(fgObj);
}
function iFg_ActionClick(fgObj, fn, bol) {
    if (fgObj.Grid_IsValid == false || fgObj.Search == true)
        return;
    var ri = "";
    if (fgObj.rowIndex.toString() != "" || bol == 'False') {
        ri = fgObj.rowIndex;
    }
    else {
        alert("Please select a row to perform this operation.");
        return false;
    }
    fg_docClick(fgObj.id);
    if (fgObj.Grid_IsValid == false)
        return;
    fgObj = document.all[fgObj.id];
    if (ri.toString() != "" || bol == 'False') {
        iFg_SetRowIndex(fgObj, ri);
        eval(fn + "('" + ri + "')");
    }
    else {
        alert("Please select a row to perform this operation.");
        return false;
    }
}
// To get help message for grid input controls
function GetHelpMessage(tdObj) {
    if (typeof (getIAttribute(tdObj, "hT")) != "undefined") {
        var helpmsg = getIAttribute(tdObj, "hT");
        var helpids = (getIAttribute(tdObj, "hT")).split(",");
        var sHelpMessage;
        if (helpids.length >= 0) {
            sHelpMessage = getHelpMessage(helpids[0], false);
        }
        return sHelpMessage;
    }
}


