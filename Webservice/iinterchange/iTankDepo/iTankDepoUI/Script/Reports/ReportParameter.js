var HasChanges = false;



function RunReport_Click() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    if (typeof (ifgParamValue) == "object") {
        if (ifgParamValue.Submit() == false) {
            return false;
        }
    }

    var isparamselected = SetMultiParamValue();

    if (isparamselected) {
        var ObjReport = new ReportPrint();
        ObjReport.ReportId = getQStr("activityid");
        ObjReport.ReportPath = getQStr("apu");
        ObjReport.ReportName = getQStr("activityname");
        ObjReport.KeyName = getQStr("tablename");
        ObjReport.OpenReportDialog();
    }
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
        var _url = 'Reports/ReportDialog.aspx?fm=1&reportpath=' + this.ReportPath + "&reportname=" + this.ReportName + "&" + "KeyName=" + this.KeyName + "&" + "ReportId=" + this.ReportId + "&";
        if (this.ReportName == "Container History") {
            _url += '&rel=0&rptid=' + this.ReportId;
        }
        else {
            _url += '&rel=1&rptid=' + this.ReportId;
        }
        for (var p in _pl) {
            _url += '&';
            _url += p;
            _url += '=';
            _url += _pl[p];
        }
        var _lft = 0; //(1000 - 1000) / 2;
        var _top = 0; //(700 - 690) / 2;

        _properties = "resizable=yes,width=1000,height=700,left=" + _lft + ",top=" + _top + "";

        //_properties = "width=" + window.screen.availWidth - 50 + ",height" + window.screen.availHeight - 50 + ",left=0,top=0";
        //_printWindow = window.open(_url, null, _properties);
        showPopupWindow(this.ReportName, _url, '960px', '650px', "", "", _properties);
    }
}

function ShowParam(_paramname, _paramtype, rIndex) {
    document.body.click();
    if (!Page_IsValid) {
        return false;
    }

    switch (_paramtype) {
        case "multivalue":
            {
                el('masterdiv').style.display = 'block';
                el('Paramdiv').style.display = 'none';

                var vrdgParameter = new ClientiFlexGrid('ifgParamValue');
                vrdgParameter.parameters.add("paramselected", _paramname);
                vrdgParameter.DataBind();
                break;
            }
            //dropdown
        case "dropdown":
            {
                ShowParamName(_paramname);
                el('Paramdiv').style.display = 'block';
                el('ddlParameter').param = _paramname;
                el('masterdiv').style.display = 'none';
                ShowParamBox('DropdownDiv');
                var rpCallback = new Callback();
                rpCallback.add("param", _paramname);
                rpCallback.invoke("ReportParameter.aspx", "BindDropDown");

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

                var cols = ifgParamList.Rows(rIndex).Columns();
                if (cols[7] != null && cols[7] != "") {
                    el('ddlParameter').value = cols[12];
                }

                break;
            }
        case "date":
            {
                ShowParamName(_paramname);
                el('masterdiv').style.display = 'none';
                el('Paramdiv').style.display = 'block';
                el('datParam').param = _paramname;

                ShowParamBox('Datdiv');

                if (_paramname.indexOf("From") != -1) {
                    // SetHelpText(el('datParam'), 182);
                }
                else if (_paramname.indexOf("To") != -1) {
                    // SetHelpText(el('datParam'), 183);
                }

                //var cols = ifgParamList.Rows(rIndex).Columns();

                var datCallback = new Callback();
                datCallback.add("paramname", _paramname);

                datCallback.invoke("ReportParameter.aspx", "GetParamValue");

                if (datCallback.getCallbackStatus()) {
                    var date = datCallback.getReturnValue("Value");
                    el('datParam').value = date;
                    el('datParam').focus();
                }
                datCallback = null;

//                if (cols[7] != null) {
//                    el('datParam').value = cols[12];
//                    el('datParam').focus();
//                }
                break;
            }
        case "integer":
            {
                ShowParamName(_paramname);
                el('masterdiv').style.display = 'none';
                el('Paramdiv').style.display = 'block';
                el('txtintParam').param = _paramname;
                ShowParamBox('Intdiv');

                var datCallback = new Callback();
                datCallback.add("paramname", _paramname);

                datCallback.invoke("ReportParameter.aspx", "GetParamValue");

                if (datCallback.getCallbackStatus()) {
                    var strValue = datCallback.getReturnValue("Value");
                    el('txtintParam').value = strValue;
                    el('txtintParam').focus();
                }
                datCallback = null;

//                var scols = ifgParamList.Rows(rIndex).Columns()

//                el('txtintParam').value = scols[12];
//                el('txtintParam').focus();
                break;
            }
        case "string":
            {
                el('masterdiv').style.display = 'none';
                ShowParamName(_paramname);
                el('masterdiv').style.display = 'none';
                el('Paramdiv').style.display = 'block';
                el('txtstrParam').param = _paramname;
                ShowParamBox('Strdiv');

                var datCallback = new Callback();
                datCallback.add("paramname", _paramname);

                datCallback.invoke("ReportParameter.aspx", "GetParamValue");

                if (datCallback.getCallbackStatus()) {
                    var strValue = datCallback.getReturnValue("Value");
                    el('txtstrParam').value = strValue;
                    el('txtstrParam').focus();
                }
                datCallback = null;

//                var cols = ifgParamList.Rows(rIndex).Columns()

//                if (cols[10] != null) {
//                    el('txtstrParam').value = cols[12];
//                    el('txtstrParam').focus();
//                }
                break;
            }
    }
    reSizePane();
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
}

function SetHelpText(obj, ht) {
    if (obj != null) {
        obj.hT = ht;
    }
}

function getHelpMessage(_id, IsReadOnly) {
    if (IsReadOnly)
        return "";
    else
        return HelpMessages[_id];
    getHelpContext();
}

function SetDateParam() {
    SetParamValue(el('datParam').param, el('datParam').value);
}
function SetSingleParam() {
    SetParamValue(el("txtstrParam").param, el("txtstrParam").value);
}
// added for dropdown
function SetDdlParam() {
    SetParamValue(el('ddlParameter').param, el('ddlParameter').value);
    hideMessage();
}
function SetParamValue(_paramName, _paramValue) {
    var rpCallback = new Callback();
    rpCallback.add("paramname", _paramName);
    rpCallback.add("paramvalue", _paramValue);

    rpCallback.invoke("ReportParameter.aspx", "SetSimpleParam");

    if (rpCallback.getCallbackStatus()) {

    }
    rpCallback = null;
}

function SetMultiParamValue() {
    var rpCallback = new Callback();
    rpCallback.add("ReportName", el('hdnReportName').value);
    rpCallback.invoke("ReportParameter.aspx", "SetMultiParamValue");

    if (rpCallback.getCallbackStatus()) {
        var error = rpCallback.getReturnValue("error");
        if (error != "" && error != null) {
            showErrorMessage(error);
            return false;
        }
        else
            return true;
    }
    rpCallback = null;
}

function fnSetDisplay(param, mode) {
    if (mode == "clientbind") {
        var rowcount = param["count"];
        if (rowcount > 0) {
            el("masterdiv").style.display = "block";
        }
        else {
            el("masterdiv").style.display = "none";
            el("Paramdiv").style.display = "none";
            ResetValidators();
        }
    }
}

function valdiateToDate(oSrc, args) {
    var Todate = args.Value;
    currentdate = el('hdnSysDate').value;
    var validateTodate = DateCompare(Todate, currentdate);
    if (validateTodate) {
        oSrc.errormessage = " Date must not be greater than Current Date.";
        args.IsValid = false;
    }
}


function GetReportTitle() {
    $("#spnHeader").text(getQStr("listpanetitle"));
}

function onTextInvoiceParty() {
    clearTextValues("txtPartyRef");
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
            el("tabReportParameter").style.height = $(window.parent).height() - 233 + "px";
            if (el("ifgParamList") != null) {
                el("ifgParamList").SetStaticHeaderHeight($(window.parent).height() - 223 + "px");
            }
            if (el("ifgParamValue") != null) {
                el("ifgParamValue").SetStaticHeaderHeight($(window.parent).height() - 296 + "px");
            }
        }
        else {
            el("tabReportParameter").style.height = $(window.parent).height() - 231 + "px";
            if (el("ifgParamList") != null) {
                el("ifgParamList").SetStaticHeaderHeight($(window.parent).height() - 287 + "px");
            }
            if (el("ifgParamValue") != null) {
                el("ifgParamValue").SetStaticHeaderHeight($(window.parent).height() - 317 + "px");
            }
        }
    }

}