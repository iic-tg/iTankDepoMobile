// var _lkpver = "4.0.0.1//031020131716";
// var _olkpver = "3.0.4267.33577//0602121259";
var _frmme = "";
var _spliter = "<?>";
var bolSelect = true;
function Init_Lkp(aid, iid, sv) {
    if (sv)
        getIObject(aid).SelectedValues = sv
    else
        getIObject(aid).SelectedValues = new Array();
    if (document.addEventListener) {
        window.document.addEventListener("DOMContentLoaded", new Function("Init_LkpRS('" + aid + "','" + iid + "');"), false);
    }
    else
        window.document.attachEvent("onreadystatechange", new Function("Init_LkpRS('" + aid + "','" + iid + "');"));
}
function Init_LkpRS(aid, iid) {
    if (document.readyState == 'interactive' || document.readyState == 'complete') {
        var a = getIObject(aid);
        BindOnKeyDownEvent(a, "lkp_onKeydown();");
        setIAttribute(a, "autocomplete", "off");
        a.onkeyup = lkp_onKeyup;
        a.hasChanges = false;
        setIAttribute(a, "language", "javascript");
        setIAttribute(a, "cT", "iLookup");
        setIAttribute(a, "f", "Lookup");
        setIAttribute(a, "sc", "");
        a.SetSelectedValues = function (arrValues) { return lkp_sSV(a, arrValues); };
        BindOnChangeEvent(a, "lkp_onChange();");
        if (iid != "") {
            var img = getIObject(iid);
            img.lkp = a;
            img.onclick = function () { return lkp_imgClick(this); };
            if (msie) {
                img.style.height = "18px";
                img.className = "limg";
            }
            else {
                img.className = "limgc"
            }
            img.style.verticalAlign = "top";
        }
        if (getIAttribute(a, "f") == "Lookup") {
            cD(a);
        }
        BindClickEvent(window.document, "lB('" + aid + "');");
    }
}

function lkp_imgClick(obj) {
    var event = window.event || arguments.callee.caller.arguments[0];
    if (typeof (obj.lkpGrid) == "undefined" || obj.lkpGrid == null) {
        if (obj.lkp.readOnly == true)
            return;
        obj.lkp.focus();
        //obj.lkp.value = "";
        lCB(obj.lkp, "search", true);
        if (_frmme != obj.lkp.id) {
            tLG(getIObject(_frmme), false);
        }
        _frmme = obj.lkp.id;
    }
    var pObj = obj.parentElement || obj.parentNode;
    if (typeof (getIAttribute(pObj, "cT")) != "undefined") {
        if (getIAttribute(pObj, "cT") == 'iLookup') {
            if (event.stopPropagation) {
                event.stopPropagation();
            }
            event.returnValue = false;
            event.cancelBubble = true;
        }
    }
    /*event.returnValue=false;
    event.cancelBubble=true;*/
    return false;
}
function cD(a) {
    var c = getIObject(a.id + "_ilkpCntr");
    if (c == null) {
        var c = document.createElement("DIV");
        setIAttribute(a, "cId", a.id + "_ilkpCntr");
        setIAttribute(c, "id", a.id + "_ilkpCntr");
        c.style.position = "absolute";
        c.style.top = "0px";
        c.style.left = "0px";
        c.style.zIndex = 99999;
        c.style.display = "none";
        window.document.body.appendChild(c);
    }
    else {
        setIAttribute(a, "cId", a.id + "_ilkpCntr");
        c.style.position = "absolute";
        c.style.top = "0px";
        c.style.left = "0px";
        c.style.zIndex = 99999;
        c.style.display = "block";
    }
}
function fS(event) {
    if (event != null && typeof (event.srcElement.f) != "undefined")
        return false;
}
//On Keyup event of TextBox
function lkp_onKeyup(event) {
    if (!event)
        event = getEvent(event);
    var obj = event.srcElement || event.target;
    var keycode = parseInt(getIAttribute(obj, "KeyCode"));
    if (obj.readOnly == true)
        return false;
    if (event.keyCode == 13 || event.keyCode == 9) {
        if (getIAttribute(obj, "doSearch") == "true" && getIAttribute(obj, "f") == "Lookup") {
            tLG(obj, false);
            iwc_SetFocus(obj.id);
            if (event.stopPropagation) {
                //event.preventDefault(true);
                event.stopPropagation();
            }
            else
                event.keyCode = 0;
            event.returnValue = false;
            event.cancelBubble = true;
            //return false;
        }
        else {
            return false;
        }

    }
    if ((event.keyCode > 96 && event.keyCode < 112) || (event.keyCode > 64 && event.keyCode < 91) || (event.keyCode > 47 && event.keyCode < 91) || (event.keyCode == keycode || event.keyCode == 38 || event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 33 || event.keyCode == 34) && (event.keyCode != 27)) {
        //obj.range=null;
        sLC(obj);
        if (getIAttribute(obj, "doSearch") == "true" && (getIAttribute(obj, "f") == "Lookup" || getIAttribute(obj, "f") == "TypeAhead")) {
            if (event.keyCode != keycode && event.keyCode != 38 && event.keyCode != 33 && event.keyCode != 34 && (getIObject(obj.id + "_iData") && getIObject(obj.id + "_iData").style.display == "block")) {
                bolSelect = false;
                lCB(obj);
            }
            else if (event.keyCode == keycode && (getIObject(obj.id + "_iData") == null || getIObject(obj.id + "_iData").style.display == "none")) {
                bolSelect = false;
                lCB(obj, "search");
            }
            else if (event.keyCode == 8 && (getIObject(obj.id + "_iData") != null))
                lCB(obj);
            else if (event.keyCode == 33)
                onPageIndexChanged("down", obj.id, event.keyCode);
            else if (event.keyCode == 34)
                onPageIndexChanged("up", obj.id, event.keyCode);
            else
                sLg(event);
        }
    }
}
//On KeyDown event of TextBox
function lkp_onKeydown(event) {
    if (!event)
        event = getEvent(event);
    var _obj = event.srcElement || event.target;
    if (_obj.readOnly == true)
        return false;
    if (event.keyCode == 13 || event.keyCode == 9) {
        if (getIObject(_obj.id + "_iData") != null) {
            getIObject(_obj.id + "_iData").style.display = "none";
            getIObject(_obj.id + "_frm").style.display = "none";
        }
        if (_obj.hasChanges == true) {
            if (typeof (_obj.onchange) != 'undefined')
                _obj.onchange();
            _obj.hasChanges = false;
            if (event.stopPropagation) {
                event.stopPropagation();
            }
            return false;
        }
    }
    if (event.keyCode == 27) {
        lB(_obj.id);
        return;
    }
    if (event.keyCode == 8 && _obj.readOnly != true) {
        if (document.selection) {
            if (document.selection.type != "None")
                document.selection.clear();
        }
        else {
            _obj.cP = GetCaretPosition(_obj);
            _obj.value = _obj.value.substring(0, _obj.cP);
            return true;
        }
    }
    if (document.selection) {
        if (document.selection.type != "None") {
            //if (event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 17)
            //  document.selection.clear();
        }
    }
}
//Text change event of lookup control
function lkp_onChange(obj) {
    var event = getEvent(event);
    if (obj == null)
        obj = event.srcElement || event.target;
    if (obj.readOnly == true)
        return false;
    var vali = getIObject(obj.id + "_CVali")
    if (vali != null) {
        if (getIAttribute(vali, "l") == 'f') {
            //lCB(obj);
        }
    }
    if (typeof (AllValidatorsValid) != "undefined") {
        if (AllValidatorsValid(obj.Validators, obj) == true)
            try {
                rTc(obj, true);
            }
            catch (ex) { }
    }
    else {
        try {
            rTc(obj, true);
        }
        catch (ex) { }
    }
    if (obj.mozIsTextField) {
        event.preventDefault(true);
        event.stopPropagation();
    }
}

function clearChildValues(obj) {
    lkp_CBC(obj);
    if (obj.hasChanges == true) {
        if (typeof (getIAttribute(obj.parentNode, "cT")) != "undefined") {
            if (getIAttribute(obj.parentNode, "cT") == 'iLookup') {
                var tdObj = obj.parentNode;
                clearDependentcells(tdObj);
            }
        }
        else if (getIAttribute(obj, "cT") == 'iLookup') {
            if (typeof (getIAttribute(obj, "ChildControls")) != "undefined") {
                var controls = getIAttribute(obj, "ChildControls").split(",");
                for (var i = 0; i <= controls.length - 1; i++) {
                    if (controls[i] != "") {
                        var ctrl = getIObject(controls[i])
                        if (ctrl) {
                            ctrl.value = "";
                            ctrl.IsCheckedOnKd = true;
                            if (ctrl.SelectedValues != "") {
                                ctrl.SelectedValues = "";
                                clearChildValues(ctrl);
                            }
                        }
                    }
                }
            }
        }
    }
}

function lkp_CBC(_id) {
    var BindControl = getIAttribute(_id, "ControlToBind").split(',');
    for (var i = 0; i <= BindControl.length - 1; i++) {
        var cellIndex = parseInt(BindControl[i]);
        if (BindControl[i] != "") {
            if (typeof (getIAttribute(_id.parentNode, "cT")) != "undefined") {
                if (getIAttribute(_id.parentNode, "cT") == 'iLookup' && (_id.value == "" || _id.parentNode.children.length == 0)) {
                    var fgObj = getGridObj(_id.parentNode);
                    var rowIndex = parseInt(getIAttribute(_id.parentNode.parentNode, "rIndex")) + parseInt(getIAttribute(fgObj, "hdrRows"));
                    setText(fgObj.rows[rowIndex].cells[cellIndex], "");
                }
            }
            else if (getIAttribute(_id, "cT") == 'iLookup' && _id.value == "") {
                var _bind = getIObject(BindControl[i]);
                if (_frmme == BindControl[i]) {
                    tLG(_bind, false);
                }
                _bind.value = "";
                _bind.IsCheckedOnKd = true;
            }
        }
    }
}

function clearDependentcells(tdObj) {
    if (typeof (getIAttribute(tdObj, "ChildControls")) != "undefined") {
        var fgObj = getGridObj(tdObj);
        var rowIndex = parseInt(getIAttribute(tdObj.parentNode, "rIndex")) + parseInt(getIAttribute(fgObj, "hdrRows"));
        var cells = getIAttribute(tdObj, "ChildControls").split(",");
        for (var i = 0; i <= cells.length - 1; i++) {
            if (cells[i] != "") {
                var cTdObj = fgObj.rows[rowIndex].cells[cells[i]];
                setText(cTdObj, "");
                if (typeof (getIAttribute(cTdObj, "SelectedValue")) == "undefined" || getIAttribute(cTdObj, "SelectedValue") == "") {
                    setIAttribute(cTdObj, "SelectedValue", "");
                }
                clearDependentcells(cTdObj);
            }
        }
    }
}

function lkp_sSV(obj, arrSV) {
    if (obj != null) {
        var SelectedValues = new Array();
        for (var count = 0; count < arrSV.length; count++) {
            obj.SelectedValues[count] = arrSV[count];
        }
    }
}

var reQ;
function lCB(obj, mode, iLC) {
    if (obj.readOnly == true)
        return false;
    if (typeof (getIAttribute(obj.parentNode, "cT")) != "undefined") {
        if (getIAttribute(obj.parentNode, "cT") == 'iLookup')
            var gridObj = getGridObj(obj.parentNode);
    }
    else {
        var gridObj = null;
    }
    if (typeof (getIAttribute(obj, "FilterFunction")) != "undefined") {
        var fc = GetFilter(getIAttribute(obj, "FilterFunction"), obj);
    }
    else if (typeof (getIAttribute(obj.parentNode, "cT")) != "undefined" && getIAttribute(obj.parentNode, "cT") == 'iLookup' && typeof (getIAttribute(obj.parentNode, "FilterFunction")) != "undefined") {
        var fc = GetFilter(getIAttribute(obj.parentNode, "FilterFunction"), obj);
    }
    else {
        var fc = filtercondition(obj);
    }
    var pU = "";
    pU = pU + unescape(_wcU);
    pU = Lkp_SetQueryString(pU, "l", "true");
    if (getIAttribute(obj.parentNode, "cT") == 'iLookup')
        pU = Lkp_SetQueryString(pU, "c", getIAttribute(obj.parentNode, "columns"));
    else
        pU = Lkp_SetQueryString(pU, "c", getIAttribute(obj, "colName"));
    pU = Lkp_SetQueryString(pU, "fc", fc);
    pU = Lkp_SetQueryString(pU, "t", getIAttribute(obj, "tblName"));
    pU = Lkp_SetQueryString(pU, "d", getIAttribute(obj, "dataKey"));
    pU = Lkp_SetQueryString(pU, "as", getIAttribute(obj, "auS"));
    pU = Lkp_SetQueryString(pU, "cI", getIAttribute(obj.parentNode, "cI"));
    pU = Lkp_SetQueryString(pU, "i", obj.id);
    pU = Lkp_SetQueryString(pU, "activityid", getQStr("activityid"));
    pU = Lkp_SetQueryString(pU, "cT", getIAttribute(obj, "cT"));
    if (getIAttribute(obj, "f"))
        pU = Lkp_SetQueryString(pU, "f", getIAttribute(obj, "f"));
    if (mode != null && mode != "" && mode != "search") {
        pU = Lkp_SetQueryString(pU, "m", mode);
        if (mode != "pk")
            pU = Lkp_SetQueryString(pU, "p", getIAttribute(obj, "cpIndex"));
    }
    if ((mode == null || mode != "pk") && !iLC)
        setIAttribute(obj, "oldValue", getLkpControlValue(obj));
    reQ = getXMLHttpRequest();
    reQ.open("POST", pU, false);
    reQ.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    if (!iLC && ((mode == "down" || mode == "up") && getLkpControlValue(obj) != ""))
        setIAttribute(obj, "sc", getLkpControlValue(obj));
    else {
        if (mode == "pk" || mode == null) {
            setIAttribute(obj, "sc", getLkpControlValue(obj));
        }
    }
    if ((obj.value != "") && !iLC)
        reQ.setRequestHeader("v", encodeURIComponent(getLkpControlValue(obj)));
    else {
        if (mode == "down" || mode == "up") {
            reQ.setRequestHeader("v", encodeURIComponent(getIAttribute(obj, "sc")));
        }
    }
    reQ.onreadystatechange = new Function(" lLG('" + obj.id + "','" + mode + "');");
    var postData = "";
    if (getIAttribute(obj.parentNode, "cT") == 'iLookup') {
        postData = postData + "__CALLBACKID=" + gridObj.id;
        postData = postData + "&__EVENTTARGET=I" + gridObj.id;
        postData = postData + "&__CALLBACKPARAM=" + obj.id;
    }
    else {
        postData = postData + "__CALLBACKID=" + obj.id;
        postData = postData + "&__EVENTTARGET=I" + obj.id;
    }
    reQ.send(postData);
}
function GetFilter(clientFunction, obj) {
    if (clientFunction != undefined)
        return eval(clientFunction + "('" + obj.id + "')");
    else
        return "";
}
function lLG(_id, mode) {
    if (reQ.readyState == 4) {
        var res = reQ.responseText;
        if (res.charAt(0) == "s") {
            res = res.substring(1, res.length);
            res = "0|" + res;
        }
        if (res.substring(0, 2) == '0|') {
            var response = res.substring(2, reQ.responseText.toString().length);
        }
        else
            var response = res.toString();
        if (reQ.status != "200") {
            document.write(response);
            return;
        }
        var obj = getIObject(_id);
        if (response != "Error" && response != "False") {
            if (mode == "pk") {
                if (reQ.getResponseHeader("Selvalues") != null && reQ.getResponseHeader("Selvalues") != "") {
                    var selvalues = reQ.getResponseHeader("Selvalues");
                    obj.SelectedValues = selvalues.split(_spliter);
                }
                obj.isvalid = true;
                BindDependentControls(obj);
                clearChildValues(obj);
                return;
            }
            var _len = obj.value.length;
            if (getIAttribute(obj, "f") == "TypeAhead") {
                obj.value = response;
                lsT(obj, _len);
                return;
            }
            if (obj.value == getIAttribute(obj, "oldValue"))
                setIAttribute(obj, "oldValue", "");
            bD(obj, response, getIAttribute(obj, "cId"));
            lsT(obj, _len);
            if (mode != null && mode != "" && reQ.getResponseHeader("cpIndex") != null)
                setIAttribute(obj, "cpIndex", reQ.getResponseHeader("cpIndex"));
            if (reQ.getResponseHeader("sHead") != null)
                setIAttribute(obj, "sHead", reQ.getResponseHeader("sHead"));
            lkpAddEvents(_id);
            lSp(obj);
        }
        else if (response == "Error")
            alert(reQ.getResponseHeader("errmsg"));
        else if (response == "False") {
            if (mode == "pk") {
                obj.isvalid = false;
                return;
            }
            bD(obj, "No Records found", getIAttribute(obj, "cId"));
        }
    }
}
function lsP(l, lG, fE) {
    if (lG) {
        fE.style.width = lG.offsetWidth;
        fE.style.height = lG.offsetHeight;
        if (typeof (getIAttribute(l, "hA")) == 'undefined') {
            lG.style.left = oL(l) - l.offsetLeft + 3 + "px";
            fE.style.left = lG.style.left;
        }
        else {
            lG.style.left = oL(l) - l.offsetLeft + 5 - lG.clientWidth + l.clientWidth + "px";
            fE.style.left = lG.style.left;
        }
        if (typeof (getIAttribute(l, "vA")) == 'undefined') {
            lG.style.top = oT(l) + l.offsetHeight - 1 + "px"; //lG.style.top = $(l).offset().top + l.offsetHeight + "px"
            //if (!msie)
            //  lG.style.top = l.getBoundingClientRect().bottom +"px";		    
            //fE.style.top = lG.style.top;
        }
        else {
            lG.style.top = $(l).offset().top + l.offsetHeight - 2 - lG.clientHeight - l.clientHeight + "px" //lG.style.top = oT(l) + l.offsetHeight - 2 - lG.clientHeight - l.clientHeight + "px";
            //if (!msie)
            //lG.style.top = l.getBoundingClientRect().bottom + "px";             
            //fE.style.top = lG.style.top;
        }
    }
}

function lkp_Frm(_lkpDiv, id) {
    var fE;
    fE = document.createElement("IFRAME");
    fE.id = id + "_frm"
    fE.style.position = "absolute";
    fE.scrolling = "no";
    fE.frameborder = "0";
    fE.style.height = "0px";
    document.body.appendChild(fE);
}

function bD(obj, _data, id) {
    if (typeof (obj.lkpGrid) == "undefined" || obj.lkpGrid == null) {
        var d = getIObject(obj.id + "_iData");
        var fE = getIObject(obj.id + "_frm");
        if (d == null) {
            d = document.createElement("DIV");
            d.id = obj.id + "_iData";
            getIObject(id).style.display = "block";
            d.style.zIndex = 9999;
            d.style.position = "absolute";
            lkp_Frm(d, obj.id);
            fE = getIObject(obj.id + "_frm");
            getIObject(id).appendChild(d);
            fE.style.zIndex = d.style.zIndex - 1;
        }
        obj.lkpGrid = d;
    }
    else {
        getIObject(id).style.display = "block";
        var d = obj.lkpGrid;
        var fE = getIObject(obj.id + "_frm");
    }
    d.innerHTML = "";
    d.innerHTML = _data;
    if (getIObject(obj.id + "_lkpDiv") != null) { getIObject(obj.id + "_lkpDiv").style.height = "auto"; }
    fE.style.display = "block";
    d.style.display = "block";
    d.style.visibility = "visible";
    tLG(obj, true);
    if (_data == "No Records found") {
        d.className = getIAttribute(obj, "GridClass");
        d.style.width = '150px';
    }
    lsP(obj, d, fE);
    _frmme = obj.id;
    setIAttribute(obj, "rowEle", null);
    if (typeof (getIAttribute(obj, "rowEle")) == "undefined" || getIAttribute(obj, "rowEle") == null || getIAttribute(obj, "rowEle") == "")
        setIAttribute(obj, "rowEle", "lkrid_" + obj.id + "_1");
    var _lkpRow = getIObject(getIAttribute(obj, "rowEle"));
    if (_lkpRow != null) {
        onLkpMover(_lkpRow.id, obj.id);
        SetSelectedValues(obj, _lkpRow.id);
        BindDependentControls(obj, getIAttribute(obj, "rowEle"));
    }
}
function SetSelectedValues(obj, _lkpRow) {
    obj.SelectedValues = new Array();
    obj.SelectedValues = getIAttribute(getIObject(_lkpRow), "SelectedValues").split(_spliter);
    if ((!(getIAttribute(obj, "aSCS") == "True") || !(getIObject(_lkpRow).parentNode.parentNode.rows.length > 2)) || (obj.value == "") || bolSelect) {
        obj.value = ChangeCase(obj, obj.SelectedValues[getIAttribute(obj, "dK")]);
    }
    iwc_SetFocus(obj.id);
    obj.IsCheckedOnKd = true;
    if (getIAttribute(obj, "cT") == 'iLookup' && getIObject(obj.id + '_CVali') != null) {
        ResetLookupValidator(obj.id);
    }
    else if (getIAttribute(obj.parentNode, "cT") == 'iLookup' && obj.parentNode.validate == 'true') {
        ResetLookupValidator(obj.id);
        var gObj = getGridObj(obj.parentNode);
        if (gObj != null)
            gObj.Grid_IsValid = Grid_IsValid;
    }
    if (getIAttribute(obj, "oldValue") != obj.value) {
        obj.IsCheckedOnKd = true;
        sLC(obj);
    }
}
function rTc(obj, bol) {
    if (obj != null && obj.hasChanges) {
        lkp_RSV(obj);
        if (getIAttribute(obj, "cT") == 'iLookup' && getIObject(obj.id + '_CVali') != null) {
            if (ValidateLookupControl(obj, bol) == false)
                return false;
        }
        else if (getIAttribute(obj.parentNode, "cT") == 'iLookup' && getIAttribute(obj.parentNode, "validate") == 'true') {
            if (ValidateLookupControl(obj, bol) == false)
                return false;
        }
        clearChildValues(obj);
        //obj.hasChanges = false;
        var _pObj = obj.parentElement || obj.parentNode;
        if (typeof (getIAttribute(_pObj, "cT")) != "undefined") {
            if (getIAttribute(_pObj, "cT") == 'iLookup') {
                var gridObj = getGridObj(_pObj);
                if (gridObj.Search == true)
                    return;
            }
        }
        if (typeof (getIAttribute(obj, "tC")) != "undefined" && getIAttribute(obj, "tC") != "") {
            if (getIAttribute(obj, "oldValue") != obj.value) {
                if (getIAttribute(obj, "tC") != "undefined")
                    eval(getIAttribute(obj, "tC") + "(obj);");
                setIAttribute(obj, "oldValue", obj.value);
            }
        }
        //setSelectionRange(obj, 0, 0);
        //obj.IsCheckedOnKd = false;
    }
}

function lkp_RSV(obj) {
    if (obj != null && obj.value == "") {
        if (obj.SelectedValues)
            var SValues = obj.SelectedValues;
        if (SValues == null)
            var SValues = getIAttribute(obj.parentNode, "SelectedValue");
        if (SValues == null && obj.parentNode && typeof (getIAttribute(obj.parentNode, "pDV")) != "undefined")
            setIAttribute(obj.parentNode, "pDV", "");
        if (SValues) {
            for (var count = 0; count < SValues.length; count++) {
                SValues[count] = "";
            }
        }
    }
}

function GetLookupChanges(bol) {
    var elements = document.getElementsByTagName("INPUT")
    for (var count = 0; count < elements.length; count++) {
        var obj = elements[count];
        if (obj.type == 'text' && typeof (getIAttribute(obj, "cT")) != 'undefined' && getIAttribute(obj, "cT") == 'iLookup') {
            if (bol == null)
                bol = false
            if (obj.hasChanges == true && bol == false) {
                lB(obj.id);
                obj.hasChanges = false;
                //return true;
            }
            else {
                tLG(obj, false);
                obj.hasChanges = false;
                //return false;
            }
        }
    }
}

function ValidateLookupControl(obj, bol) {
    if (bol != true && (obj.isvalid != false && obj.islkpval != false)) {
        Page_BlockLookupValidate = true;
        SetValidatorsValid(obj);
    }
    ValidatorOnChange(obj.id);
    if (bol != true)
        Page_BlockLookupValidate = false;
    else
        obj.IsCheckedOnKd = false;
    return obj.isvalid;
}

function BindDependentControls(_id, _rowid) {
    var BindControl = getIAttribute(_id, "ControlToBind").split(',');
    for (var i = 0; i <= BindControl.length - 1; i++) {
        var cellIndex = parseInt(BindControl[i]);
        if (BindControl[i] != "") {
            if (typeof (getIAttribute(_id.parentNode, "cT")) != "undefined") {
                if (getIAttribute(_id.parentNode, "cT") == 'iLookup') {
                    var fgObj = getGridObj(_id.parentNode);
                    var rowIndex = parseInt(getIAttribute(_id.parentNode.parentNode, "rIndex")) + parseInt(fgObj.hdrRows);
                    setText(fgObj.rows[rowIndex].cells[cellIndex], ChangeCase(fgObj.rows[rowIndex].cells[cellIndex], _id.SelectedValues[i]));
                    fgObj.hasChanges = true;
                    fgObj.hasRowChanges = true;
                }
            }
            else if (getIAttribute(_id, "cT") == 'iLookup') {
                var _bind = getIObject(BindControl[i]);
                if (getIAttribute(_id, "aSCS") == "True") {
                    if (!(_id.id == _bind.id)) {
                        _bind.value = ChangeCase(getIObject(BindControl[i]), _id.SelectedValues[i]);
                        _bind.IsCheckedOnKd = true;
                    }
                }
                else {
                    _bind.value = ChangeCase(getIObject(BindControl[i]), _id.SelectedValues[i]);
                    _bind.IsCheckedOnKd = true;
                }

            }
        }
    }
}

function filtercondition(id) {
    var filtercondn = "";
    if (getIAttribute(id, "DependentControl") != "") {
        var strcontrols = getIAttribute(id, "DependentControl").split(':');
        var strlookups = strcontrols[0].split(',');
        var strcolName = strcontrols[1].split(',');
        var strcolIndex = strcontrols[2].split(',');
        var SelectedValues = "";
        for (var count = 0; count <= strlookups.length - 1; count++) {
            if (typeof (getIAttribute(id.parentNode, "cT")) != "undefined") {
                if (getIAttribute(id.parentNode, "cT") == 'iLookup') {
                    var fgObj = getGridObj(id.parentNode);
                    if (strlookups[count] != "") {
                        var rowIndex = parseInt(getIAttribute(id.parentNode.parentNode, "rIndex")) + parseInt(getIAttribute(fgObj, "hdrRows"));
                        var cellIndex = parseInt(strlookups[count]);
                        var tdObj = fgObj.rows(rowIndex).cells(cellIndex);
                        var tdVal = getText(tdObj);
                        if (tdVal != "") {
                            var value = tdVal;
                            var index = parseInt(strcolIndex[count]);
                            if (getIAttribute(tdObj, "cT") == 'iLookup') {
                                if (typeof (getIAttribute(tdObj, "SelectedValue")) != "undefined") {
                                    SelectedValues = getIAttribute(tdObj, "SelectedValue");
                                }
                                else if (typeof (getIAttribute(tdObj, "pDV")) != "undefined")
                                    SelectedValues = getIAttribute(tdObj, "pDV");
                                else
                                    SelectedValues = "";
                            }
                            else {
                                SelectedValues = tdVal;
                            }
                            SelectedValues = SelectedValues.replace('\'', '\'\'');
                            filtercondn += strcolName[count] + "='" + SelectedValues + "'";
                            // if(count != strlookups.length-1)
                            filtercondn += " and ";
                        }
                    }
                }
            }
            else if (getIAttribute(id, "cT") == 'iLookup') {
                if (strlookups[count] != "") {
                    if (getIObject(strlookups[count]).value != "")  //strlookups[count]
                    {
                        var value = getIObject(strlookups[count]).value;
                        var index = parseInt(strcolIndex[count]);
                        var SelectedValues = getIObject(strlookups[count]).SelectedValues[index];      //.split(',');                                            
                        if (typeof (SelectedValues) != "undefined") {
                            SelectedValues = SelectedValues.replace('\'', '\'\'');
                            filtercondn += strcolName[count] + "='" + SelectedValues + "'";
                            filtercondn += " and ";
                        }
                    }
                }
            }
        }
    }
    if (filtercondn != "" && trimAll(filtercondn).substring(filtercondn.length - 4, filtercondn.length - 1) == "and") {
        filtercondn = trimAll(filtercondn).substring(0, filtercondn.length - 4);
    }
    return filtercondn;
}

function lkpAddEvents(_id) {
    var count;
    var tbl = getIObject(_id + "_LkpTblRows");
    for (count = 1; count < tbl.rows.length; count++) {
        var trId = "lkrid_" + _id + "_" + count;
        var _txt = getIObject(_id);
        var obj = getIObject(trId);
        obj.ATOMICSELECTION = true;
        obj.tabIndex = -1;
        if (getIAttribute(_txt, "moutcssText"))
            obj.style.cssText = getIAttribute(_txt, "moutcssText").toString();
        if (getIAttribute(_txt, "moutClass") && getIAttribute(_txt, "moutClass").toString() != "" && count != 1) {
            //obj.className = getIAttribute(_txt,"moutClass").toString();
        }
        obj.onmouseover = new Function("return onLkpMover('" + trId + "','" + _id + "');");
        obj.onmouseout = new Function("return onLkpMout('" + trId + "','" + _id + "');");
        obj.onclick = new Function("return onLkpClick('" + _id + "','" + trId + "');");
    }
}

function lB(_id) {
    if (_frmme != "" && _frmme == _id) {
        _frmme = "";
        return;
    }
    var obj = getIObject(_id);
    if (obj != null) {
        tLG(obj, false);
    }
    //rTc(obj);
    lkp_onChange(obj);
    if (obj != null && obj.value != "" && obj.isvalid != true) {
        if (typeof (obj.Validators) != "undefined")
            ValidatorOnChange(_id);
    }
}
function tLG(obj, bol) {
    if (obj != null) {
        var lG = obj.lkpGrid;
        var _frm = getIObject(obj.id + "_frm");
        if (typeof (lG) != "undefined" && lG != null) {
            if (bol) {
                lG.style.display = "block";
                _frm.style.display = "none";
            }
            else {
                lG.style.display = "none";
                _frm.style.display = "none";
            }
        }
    }
}
function onLkpClick(_tid, _id) {
    var event = window.event || arguments.callee.caller.arguments[0];
    getIObject(_tid + "_iData").style.display = "block";
    var _frm = getIObject(_tid + "_frm");
    var _t = getIObject(_tid)
    _frmme = "";
    bolSelect = true;
    SetSelectedValues(_t, _id);
    getIObject(_tid + "_iData").style.display = "none";
    _frm.style.display = "none";
    iwc_SetFocus(_tid);
    _t.hasChanges = true;
    sLC(_t);
    clearChildValues(_t);
    BindDependentControls(_t, _id);
    event.cancelBubble = true;
}
var _oid;
function onLkpMover(_id, _parent) {
    var par = getIObject(_parent);
    if (par != null) {
        if (typeof (_oid) != "undefined")
            toogleCss(getIObject(_oid), getIAttribute(par, "moverClass").toString(), false);
        toogleCss(getIObject(_id).parentNode, getIAttribute(par, "moutClass").toString(), true);
        if (getIAttribute(par, "movercssText"))
            getIObject(_id).style.cssText = getIAttribute(par, "movercssText").toString();
        if (getIAttribute(par, "moverClass") && getIAttribute(par, "moverClass").toString() != "") {
            //toogleCss(getIObject(_id),getIAttribute(par,"moutClass").toString(),false);
            toogleCss(getIObject(_id), getIAttribute(par, "moverClass").toString(), true);
        }
        _oid = _id;
    }
}
function onLkpMout(_id, _parent) {
    var par = getIObject(_parent);
    if (par != null) {
        if (getIAttribute(par, "moutcssText"))
            getIObject(_id).style.cssText = getIAttribute(par, "moutcssText").toString();
        if (getIAttribute(par, "moutClass") && getIAttribute(par, "moutClass") != "") {
            //toogleCss(getIObject(_id),getIAttribute(par,"moverClass").toString(),false);
            toogleCss(getIObject(_id).parentNode, getIAttribute(par, "moutClass").toString(), true);
        }
    }
}
function sLg(event) {
    var obj = event.srcElement || event.target;
    var keycode = parseInt(getIAttribute(obj, "KeyCode"));
    if (event == null || obj == null || getIObject(obj.id + "_iData") == null || getIObject(obj.id + "_iData").style.display == "none")// && typeof(obj.rowEle)!="undefined")
        return;
    if (event.keyCode == keycode) {
        if (event.preventDefault)
            event.preventDefault(true);
        else
            event.keyCode = 0;
        event.returnValue = false;
        var _lkpRow = getIObject(getIAttribute(obj, "rowEle"));
        var _hdr = 0;
        if (getIAttribute(obj, "sHead") != null && getIAttribute(obj, "sHead") == "true")
            _hdr = 1;
        if (_lkpRow != null) {
            if (_lkpRow.parentNode.rows.length - 1 != _lkpRow.rowIndex) {
                setIAttribute(obj, "rowEle", _lkpRow.parentNode.rows[_lkpRow.rowIndex + 1].id);
                _lkpRow = _lkpRow.parentNode.rows[_lkpRow.rowIndex + 1];
                onLkpMover(_lkpRow.id, obj.id);
                sLC(obj);
                //obj.oldValue = obj.value;
                bolSelect = true;
                SetSelectedValues(obj, getIAttribute(obj, "rowEle"));
                BindDependentControls(obj, getIAttribute(obj, "rowEle"))
                if (_lkpRow.rowIndex != _hdr) {
                    onLkpMout(_lkpRow.parentNode.rows[_lkpRow.rowIndex - 1].id, obj.id);
                }
            }
            else {
                if (_lkpRow.rowIndex != 1) {
                    onLkpMout(_lkpRow.parentNode.rows[_lkpRow.parentNode.rows.length - 1].id, obj.id);
                }
                if (_hdr == 0 && _lkpRow.parentNode.rows[_hdr].id == "") {
                    _hdr = 1;
                }
                setIAttribute(obj, "rowEle", _lkpRow.parentNode.rows[_hdr].id);
                _lkpRow = _lkpRow.parentNode.rows[_hdr];
                onLkpMover(_lkpRow.id, obj.id);
                sLC(obj);
                //obj.oldValue = obj.value;
                SetSelectedValues(obj, getIAttribute(obj, "rowEle"));
                BindDependentControls(obj, getIAttribute(obj, "rowEle"))
            }
        }
        return;
    }
    else if (event.keyCode == 38) {
        if (event.preventDefault)
            event.preventDefault(true);
        else
            event.keyCode = 0;
        event.returnValue = false;
        var _lkpRow = getIObject(getIAttribute(obj, "rowEle"));
        var _hdr = 0;
        if (getIAttribute(obj, "sHead") != null && getIAttribute(obj, "sHead") == "true")
            _hdr = 1;
        if (_lkpRow != null) {
            if (_lkpRow.rowIndex != _hdr) {
                setIAttribute(obj, "rowEle", _lkpRow.parentNode.rows[_lkpRow.rowIndex - 1].id);
                _lkpRow = _lkpRow.parentNode.rows[_lkpRow.rowIndex - 1];
                onLkpMover(_lkpRow.id, obj.id);
                sLC(obj);
                //obj.oldValue = obj.value;
                SetSelectedValues(obj, getIAttribute(obj, "rowEle"));
                BindDependentControls(obj, getIAttribute(obj, "rowEle"))
                if (_lkpRow.rowIndex != _lkpRow.parentNode.rows.length - 1) {
                    onLkpMout(_lkpRow.parentNode.rows[_lkpRow.rowIndex + 1].id, obj.id);
                }
            }
            else {
                setIAttribute(obj, "rowEle", _lkpRow.parentNode.rows[_lkpRow.parentNode.rows.length - 1].id);
                _lkpRow = _lkpRow.parentNode.rows[_lkpRow.parentNode.rows.length - 1];
                sLC(obj);
                onLkpMover(_lkpRow.id, obj.id);
                //obj.oldValue = obj.value;
                SetSelectedValues(obj, getIAttribute(obj, "rowEle"));
                BindDependentControls(obj, getIAttribute(obj, "rowEle"))
                onLkpMout(_lkpRow.parentNode.rows[_hdr].id, obj.id);
            }
            return;
        }
    }
}
function sLC(obj) {
    obj.hasChanges = true;
    if (typeof (HasChanges) != "undefined")
        HasChanges = true;
}
function lSp(obj) {
    var fE = getIObject(obj.id + "_frm");
    lsP(obj, obj.lkpGrid, fE);
}
function lsT(obj, _start) {
    if (document.createRange)
        setSelectionRange(obj, _start, obj.value.length);
    else {
        var selectRnge = obj.createTextRange();
        obj.range = selectRnge;
        selectRnge.moveStart("character", _start);
        selectRnge.moveEnd("character", obj.value.length);
        selectRnge.select();
    }
}
function setSelectionRange(input, selectionStart, selectionEnd) {
    if (input.setSelectionRange) {
        input.focus();
        input.setSelectionRange(selectionStart, selectionEnd);
    }
    else if (input.createTextRange) {
        var range = input.createTextRange();
        range.collapse(true);
        range.moveEnd('character', selectionEnd);
        range.moveStart('character', selectionStart);
        range.select();
    }
}
function onPageIndexChanged(_mode, _id, _kc) {
    try {
        var _len = 0;
        var obj = getIObject(_id);
        if (obj.lkpGrid)
            _len = obj.lkpGrid.childNodes[0].childNodes[0].rows.length;
        if (_len < 2)
            return;
        if (_kc) {
            if (_kc == 34) {
                if (trimAll(getText(obj.lkpGrid.childNodes[0].childNodes[0].rows[1].cells[2])) == "")
                    return;
            }
            else if (_kc == 33) {
                if (trimAll(getText(obj.lkpGrid.childNodes[0].childNodes[0].rows[1].cells[0])) == "")
                    return;
            }
        }
        var s = true;
        if (obj.range) {
            if (obj.value.length > obj.range.text.length) {
                obj.value = obj.value.substr(0, obj.value.length - obj.range.text.length);
                s = false
            }
        }
        //if (s==true)
        //    obj.value = getIAttribute(obj, "sc");
        //if (opera)
        obj.value = "";
        setIAttribute(obj, "oldValue", "");
        setIAttribute(obj, "sc", "");
        lCB(obj, _mode);
        if (!event)
            var event = getEvent(event);
        if (event.stopPropagation) {
            event.stopPropagation();
            event.preventDefault(true);
        }
        event.returnValue = false;
        event.cancelBubble = true;
    } catch (e) { }
}
//Parameters -  Control Name, Column Name
function LookupValidate(oSrc, args) {
    var src = getIObject(getIAttribute(oSrc, "controltovalidate"));
    if (src.value.length > 0) {
        if (getIAttribute(src, "aSCS") == "True") {
            var lkpRow = getIObject(getIAttribute(src, "rowEle"))
            if (lkpRow && getIAttribute(lkpRow, "selectedValues").split(_spliter)[getIAttribute(src, "dK")] == src.value) {
                bolSelect = true;
                SetSelectedValues(src, getIAttribute(src, "rowEle"));
                BindDependentControls(src, getIAttribute(src, "rowEle"))
            }
        }
        lCB(src, "pk");
        return src.isvalid;
    }
}

function ChangeCase(tdObj, value) {
    value = unescape(value);
    if (getIAttribute(tdObj, "iCase") == 'Upper')
        return value.toUpperCase();
    else if (getIAttribute(tdObj, "iCase") == 'Lower')
        return value.toLowerCase();
    else
        return value;
}
function Lkp_SetQueryString(pU, name, value) {
    if (typeof (value) == "undefined")
        return pU;
    if (pU.indexOf("?") != -1) {
        pU = pU + "&" + name + "=" + encodeURIComponent(escape(value));
    }
    else {
        pU = pU + "?" + name + "=" + encodeURIComponent(escape(value));
    }
    return pU;
}
function SetLookupValue(lkpId, code, arrvalues) {
    var obj = getIObject(lkpId);
    if (obj != null) {
        if (code != "") {
            if (obj.value != code)
                setIAttribute(obj, "oldValue", code);
            // obj.oldValue = code;
            obj.value = code;
            obj.SelectedValues = arrvalues;
        }
        else {
            obj.value = "";
            setIAttribute(obj, "oldValue", "");
            // obj.oldValue = "";
            obj.SelectedValues = arrvalues;
        }
    }
}
function getLkpControlValue(control) {
    if (control.value === getIAttribute(control, "placeholder")) {
        return "";
    }
    else {
        return control.value
    }
}
