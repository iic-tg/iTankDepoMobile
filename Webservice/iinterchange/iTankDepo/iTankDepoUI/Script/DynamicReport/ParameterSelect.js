
var HasChanges = false;

function RunReport(ReportName) {
    //document.body.click();
    if (typeof (ifgParameter) == "object") {
        if (ifgParameter.Submit() == false) {
            return false;
        }
    }

    if (typeof (ifgParameterList) == "object") {
        if (ifgParameterList.Submit() == false) {
            return false;
        }
    }
    
    var result = ValidateReport(ReportName);

    if (result == false)
        return false;

    var ObjReport = new ReportPrint();
    ObjReport.ReportId = getQStr("activityid");
    ObjReport.ReportPath = getQStr("apu");
    ObjReport.ReportName = ReportName;
    ObjReport.KeyName = getQStr("tablename");
    ObjReport.OpenReportDialog();

}

function ValidateReport(ReportName) {
    var rpCallback = new Callback();
    rpCallback.add("ReportId", getQStr("activityid"));
    rpCallback.add("ReportName", ReportName);

    rpCallback.invoke("DynamicReportParameter.aspx", "validateReportParameters");
    if (rpCallback.getCallbackStatus()) {
        if (rpCallback.getReturnValue('status') == "true") {
            return true;
        }
        else {
            showErrorMessage(rpCallback.getReturnValue('statuserror'));           
            return false;
         }

    }
    rpCallback = null;
}

var _printWindow;
function ReportPrint() {
    var _pl = new Array();
    var ReportId = '';
    var ReportPath = '';
    var ReportName = '';
    var KeyName = '';

    this.add = function (name, value) {
        value = value.toString();
        if (value != '')
            _pl[name] = encodeURIComponent(value);
        return this;
    }

    this.OpenReportDialog = function () {
        var _url = 'DynamicReport/DynamicReportDialog.aspx?fm=1&reportpath=' + this.ReportPath + "&reportname=" + this.ReportName + "&" + "KeyName=" + this.KeyName + "&";
        _url += '&rel=0&rptid=' + this.ReportId;
        for (var p in _pl) {
            _url += '&';
            _url += p;
            _url += '=';
            _url += _pl[p];
        }
        var _lft = 0;
        var _top = 0;

        _properties = "resizable=yes,width=1000,height=700,left=" + _lft + ",top=" + _top + "";

        showPopupWindow(this.ReportName, _url, '960px', '700px', "", "", _properties);
    }
}

function ReportParameterLoadPage() {
    el('lblSelectedParameter').style.display = 'none';
}

function getselList(_cntrl) {
    var result = "";
    for (var i = 0; i < el(_cntrl).length; i++) {
        if (el(_cntrl).options[i].selected) {
            result += "," + el(_cntrl).options[i].value;
        }
    }
    return result.substring(1);
}

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

function ShowParam(_param, _paramtype, _paramname, rIndex, obj) {
    document.body.click();
    //  Page_ClientValidate();  
    if (!Page_IsValid) {
        showErrorMessage("Report Title Required.");
        return false;
    }

    var valiu = obj.checked;
    el('lblSelectedParameter').innerHTML = "Filter On : " + _paramname;
    switch (_paramtype) {
        case "master":
            {
                el('masterdiv').style.display = 'block';
                el('Paramdiv').style.display = 'none';
                el('divRecordNotFound').style.display = 'none';
                el('lblSelectedParameter').style.display = 'block';
                el('divRecordNotFound').style.display = 'none';

                var oCols = ifgParameterList.Rows(rIndex).GetClientColumns();
                var vrifgParameter = new ClientiFlexGrid('ifgParameter');
                vrifgParameter.parameters.add("paramselected", _param);
                if (typeof (obj.checked) != "undefined") {
                    vrifgParameter.parameters.add("Select", obj.checked);
                }
                vrifgParameter.DataBind();
                break;
            }
        case "dropdown":
            {
                ShowParamName(_paramname);
                el('Paramdiv').style.display = 'block';
                el('ddlParameter').param = _param;
                el('masterdiv').style.display = 'none';
                ShowParamBox('DropdownDiv');
                var rpCallback = new Callback();
                rpCallback.add("param", _param);
                rpCallback.invoke("DynamicReportParameter.aspx", "bindDropDown");

                if (rpCallback.getCallbackStatus()) {
                    var aDropdownData = rpCallback.getReturnValue("dropdownData");

                    var anOption;
                    el("ddlParameter").length = 0; //reset action to
                    anOption = document.createElement("OPTION")
                    anOption.text = "SELECT";
                    anOption.value = "SELECT";
                    el("ddlParameter").options.add(anOption);

                    var aDropdownDataCode = getQueryStringValue(aDropdownData, "Code").split("|");
                    var aDropdownDataID = getQueryStringValue(aDropdownData, "ID").split("|");

                    for (i = 0; i < aDropdownDataCode.length; i++) {
                        anOption = document.createElement("OPTION");
                        if ($.trim(aDropdownDataCode[i]) != "") {
                            anOption.text = aDropdownDataCode[i];
                            anOption.value = aDropdownDataID[i];
                            el("ddlParameter").options.add(anOption);
                        }
                    }
                }
                rpCallback = null;
                el('ddlParameter').focus();

                var cols = ifgParameterList.Rows(rIndex).Columns();
                if (cols[4] != null && cols[4] != "") {
                    el('ddlParameter').value = cols[4];
                }

                break;
            }
        case "date":
            {
                ShowParamName(_paramname);
                el('masterdiv').style.display = 'none';
                el('Paramdiv').style.display = 'block';
                el('lblSelectedParameter').style.display = 'block';
                el('divRecordNotFound').style.display = 'none';
                el('datParam').param = _param;
                ShowParamBox('Datdiv');
                if (_paramname.indexOf("From") != -1) {
                    SetHelpText(el('datParam'), 304);
                }
                else if (_paramname.indexOf("To") != -1) {
                    SetHelpText(el('datParam'), 304);
                }

                //                if (_paramname=="To Date")
                //                {
                //                    el('datParam').value = GetClientDate();
                //                    el('datParam').focus();
                //                    break;
                //                }
                //var rIndex = iifgParameterList.VirtualCurrentRowIndex();               
                var cols = ifgParameterList.Rows(rIndex).Columns();
                if (cols[4] != null) {
                    el('datParam').value = cols[4].replace(" 23:59:59", "");
                    el('datParam').focus();
                }
                break;
            }
        case "integer":
            {
                ShowParamName(_paramname);
                el('masterdiv').style.display = 'none';
                el('Paramdiv').style.display = 'block';
                el('lblSelectedParameter').style.display = 'block';
                el('divRecordNotFound').style.display = 'none';
                el('txtintParam').param = _param;
                ShowParamBox('Intdiv');

                //var rIndex = ifgParameterList.VirtualCurrentRowIndex();
                var cols = ifgParameterList.Rows(rIndex).Columns()

                el('txtintParam').value = cols[4];
                el('txtintParam').focus();
                break;
            }
        case "string":
            {
                ShowParamName(_paramname);
                el('masterdiv').style.display = 'none';
                el('Paramdiv').style.display = 'block';
                el('lblSelectedParameter').style.display = 'block';
                el('divRecordNotFound').style.display = 'none';
                el('txtstrParam').param = _param;
                ShowParamBox('Strdiv');

                //var rIndex = ifgParameterList.VirtualCurrentRowIndex();
                var cols = ifgParameterList.Rows(rIndex).Columns()
                if (cols[4] != null) {
                    el('txtstrParam').value = cols[4];
                    el('txtstrParam').focus();
                }
                break;
            }
    }
    reSizePane();
}

function Param_Reload(param) {

}

function ShowParamName(_paramname) {
    el('lblParameterEnter').innerHTML = _paramname + ' :';
}

function ShowParamBox(_paramboxid) {
    el('Datdiv').style.display = 'none';
    el('Strdiv').style.display = 'none';
    el('Intdiv').style.display = 'none';
    el('DropdownDiv').style.display = 'none';
    el(_paramboxid).style.display = 'block';
}

function HideParamBox() {
    el('masterdiv').style.display = 'none';
    el('Paramdiv').style.display = 'none';
    el('lblSelectedParameter').style.display = 'none';
    el('divRecordNotFound').style.display = 'none';
}
// added for dropdown
function SetDdlParam() {
    SetParamValue(el('ddlParameter').param, el('ddlParameter').value);
    hideMessage();
}
function SetParamValue(_parmName, _parmValue) {
    var rpCallback = new Callback();
    rpCallback.add("param", _parmName);
    rpCallback.add("paramvalue", _parmValue);

    rpCallback.invoke("DynamicReportParameter.aspx", "SetSimpleParam");

    if (rpCallback.getCallbackStatus()) {

    }
    rpCallback = null;
}

function SetSingleParam() {
    SetParamValue(el(event.srcElement.id).param, el(event.srcElement.id).value);
}

function SetDateParam() {
    SetParamValue(el('datParam').param, el('datParam').value);
}

function ResetParams() {
    //       if(!Page_IsValid)
    //        {
    //            return false;
    //        }
    ResetValidators();
    document.body.click();
    var rpCallback = new Callback();

    rpCallback.invoke("DynamicReportParameter.aspx", "ResetParams");

    if (rpCallback.getCallbackStatus()) {
        //While Click the Reset Button, clear the selected column value
        el('lblSelectedParameter').innerHTML = "";

        var vrifgParameter = new ClientiFlexGrid('ifgParameterList');
        vrifgParameter.DataBind();
    }
    HideParamBox();
    rpCallback = null;
}

function SaveParams() {
    if (el("txtReportTitle").value == "") {
        showErrorMessage("Report Title Required.");
        return false;
    }

    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }

    var _qs = new buildquerystring(location.href.substring(location.href.indexOf("?") + 1));
    document.body.click();

    if (el('rhid').value == "") {
        var rpCallback = new Callback();
        rpCallback.add("reporttitle", el("txtReportTitle").value);
        rpCallback.add("activityid", _qs.getValue("activityid"));
        rpCallback.add("savemode", "new");
        rpCallback.invoke("DynamicReportParameter.aspx", "SaveParam");
        if (rpCallback.getCallbackStatus()) {
            el('rhid').value = rpCallback.getReturnValue('rhid');
            showInfoMessage("Filter Inserted Sucessfully");

        } else {
            showErrorMessage(rpCallback.getReturnValue('Message'));
        }
        rpCallback = null;
    }
    else {
        var rpCallback = new Callback();
        rpCallback.add("reporttitle", el("txtReportTitle").value);
        rpCallback.add("activityid", _qs.getValue("activityid"));
        rpCallback.add("savemode", "edit");
        rpCallback.add("rhid", el('rhid').value);
        rpCallback.invoke("DynamicReportParameter.aspx", "SaveParam");
        if (rpCallback.getCallbackStatus()) {
            showInfoMessage("Filter Updated Sucessfully");

        } else {
            showErrorMessage(rpCallback.getReturnValue('Message'));
        }
        rpCallback = null;
    }


    return true;
}


function ResetValidators() {
    if (!Page_IsValid) {
        Page_BlockValidate = true;
        Page_ClientValidate();
        ValidationSummaryOnSubmit();
        Page_BlockValidate = false;
    }
    else {
        Page_BlockValidate = false;
    }
}

function SetHelpText(obj, ht) {
    if (obj != null) {
        obj.hT = ht;
    }
}

function GetReportTitle(mode, reportHeader) {
    if (mode == MODE_EDIT) {
        setPageMode(MODE_EDIT);
        el("txtReportTitle").value = reportHeader;
        setReadOnly("txtReportTitle", true);
        $("#spnHeader").text(getQStr("pagetitle"));
    }
    else if (mode == MODE_NEW) {
        setPageMode(MODE_NEW);
        clearTextValues("txtReportTitle");
        setReadOnly("txtReportTitle", false);
        $("#spnHeader").text(reportHeader);
    }    
}

//This event is fired after the grid bind
function onAfterCallBack(param) {
    var norecordsfound = param["norecordsfound"];
    if (norecordsfound == "True") {
        el('masterdiv').style.display = 'none';
        el('Paramdiv').style.display = 'none';
        el('lblSelectedParameter').style.display = 'block';
        el('divRecordNotFound').style.display = 'block';
    }
}

function SetMultiParamValue() {
    var rpCallback = new Callback();

    rpCallback.invoke("DynamicReportParameter.aspx", "SetMultiParamValue");

    if (rpCallback.getCallbackStatus()) {
        var error = rpCallback.getReturnValue("error");
        if (error != "") {
            showErrorMessage(error);
            return false;
        }
        else
            return true;
    }
    rpCallback = null;
}

//This method is to validate the duplicate code entered
function validateReportTitle(oSrc, args) {

    var oCallback = new Callback();
    oCallback.add("activityid", getQStr("activityid"));
    oCallback.add("reporttitle", args.Value);

    oCallback.invoke("DynamicReportParameter.aspx", "validateReportTitle");
    if (oCallback.getCallbackStatus()) {
        if (oCallback.getReturnValue("pkValid") == "true") {
            args.IsValid = true;
            oSrc.errormessage = "";
        }
        else {
            args.IsValid = false;
            oSrc.errormessage = oCallback.getReturnValue("Message");
        }
    }
    else {
        showErrorMessage(oCallback.getCallbackError());
    }
    oCallback = null;
    oCols = null;
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
        if ($(window.parent).height() < 768) {
            
            el("tabReport").style.height = $(window.parent).height() - 373 + "px";
        }
        else {
           
            el("tabReport").style.height = $(window.parent).height() - 478 + "px";
        }
    }

}

