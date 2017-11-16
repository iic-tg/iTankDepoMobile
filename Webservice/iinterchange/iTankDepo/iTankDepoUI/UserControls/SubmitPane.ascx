<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SubmitPane.ascx.vb" Inherits="UserControls_SubmitPane" %>
<center>
    <div class="button" style="width: 300px" runat="server" id="pnlSubmitButton">
        <table>
            <tr>
                <td class="btncorner" data-corner="6px">
                    <button tabindex="<%= TabIndex %>" id="btnSubmitPrint" class="btn btn-small btn-success" style="border: 0px; display: none; font-weight:bold" onclick="SubmitAndPrint_Click();return false;">
                        <i class="icon-print"></i>&nbsp;Submit & Print</button>
                </td>
                <td class="btncorner" data-corner="6px">
                    <button tabindex="<%= TabIndex %>" id="btnSubmit" class="btn btn-small btn-success" style="border: 0px; font-weight:bold" onclick="Submit_Click();return false;">
                        <i class="icon-save"></i>&nbsp;Submit</button>
                </td>
            </tr>
        </table>
        <%--<ul style="list-style: none">
            <li class="btncorner" data-corner="6px">
                <button tabindex="<%= TabIndex %>" id="btnSubmitPrint" class="btn btn-small btn-success" style="border: 0px; display: none;" onclick="SubmitAndPrint_Click();return false;">
                    <i class="icon-print"></i>Submit & Print</button>
            </li>
            <li class="btncorner" data-corner="6px">
                <button tabindex="<%= TabIndex %>" id="btnSubmit" class="btn btn-small btn-success" style="border: 0px;" onclick="Submit_Click();return false;">
                    <i class="icon-save"></i>Submit</button>
            </li>
        </ul>--%>
    </div>
</center>
<asp:HiddenField ID="hdnID" runat="server" />
<asp:HiddenField ID="hdnPageMode" runat="server" />
<asp:HiddenField ID="hdnItemNo" runat="server" />
<asp:HiddenField ID="hdnTableName" runat="server" />
<input type="hidden" id="WFDATA" name="WFDATA" />
<script language="javascript" type="text/javascript">
    var HasNoError=true;
    var SubmitAction="";
    var ListAction="";
    var PageMode="";
    var bAllowAction=true;
    function setSubmitPaneWidth(width)
    {
        el("PageSubmitPane_pnlSubmitButton").style.width=width;    
    }

    function Submit_Click() {       
        SubmitAction = getText(el("btnSubmit"));
        psc().gsStatus = "Working..";
        if (!checkRights())
        {
            HasNoError = false;
            psc().gsStatus = "";
            return false;
        }

        addAttData();
        ppsc().gsAction="Submit";
        
        HasNoError = <%= onClientSubmit %>;  

        psc().gsStatus = "";

        if(HasNoError==true)
        {
            refreshListPane();
        }

        ppsc().gsAction = "";

        return HasNoError;
    }
    function SubmitAndPrint_Click() {       
        SubmitAction = getText(el("btnSubmitPrint"));
        psc().gsStatus = "Working..";
        if (!checkRights())
        {
            HasNoError = false;
            psc().gsStatus = "";
            return false;
        }

        addAttData();
        ppsc().gsAction="Submit";
        
        HasNoError = <%= onClientSubmit %>;  

        psc().gsStatus = "";

        if(HasNoError==true)
        {
            refreshListPane();
            <% If onClientPrint <> "" Then%>
            HasNoError = <%= onClientPrint %>;
             <% End If%>
        }

        ppsc().gsAction = "";

        return HasNoError;
    }
   
    function checkRights()
    {
    
        var bCreateRights = GetBoolFromString(getWFDataKey("CRT_BT"));
        var bEditRights = GetBoolFromString(getWFDataKey("EDT_BT"));
        var sMode = getPageMode();
        var bAllowToSubmit =false;
        
        if(sMode == MODE_NEW && bCreateRights == true)
        {
            bAllowToSubmit =true;
           
        }
        else if(sMode == MODE_EDIT && bEditRights == true)
        {
            bAllowToSubmit =true;
        }
        else
        {
            showWarningMessage("You do not have sufficient rights to perform the selected operation.");
        }
        return bAllowToSubmit;
    
    }

    function addAttData() {
   
        var wfdata = el('WFDATA').value;
        var objqstr = new buildquerystring(wfdata);
        objqstr.additem('ACTIVITYID', getQueryStringValue(document.location.href, "activityid"));
        if (el('hdnPaths')) {
            objqstr.additem('ATTPATH', el('hdnPaths').value);
            objqstr.additem('ATTFILENAME', el('hdnFileNames').value);
            objqstr.additem('ATTID', el('hdnAttchIds').value);
            objqstr.additem('ATTSIZE', el('hdnFileSize').value);
        }

        //Adding client date & time
        var today = getCurrentDate();
        objqstr.additem('ACTN_DT', today);

        el('WFDATA').value = objqstr.getquerystring();
    }

    function setClientDate() {
   
        var wfdata = el('WFDATA').value;
        var objqstr = new buildquerystring(wfdata);   
        //Adding client date & time
        var today = getCurrentDate();
        objqstr.additem('ACTN_DT', today);

        el('WFDATA').value = objqstr.getquerystring();
    }

    function refreshListPane(_ActivityId)
    {
        if(HasNoError==true)
        {
            if(ListAction=="List"){
                ppsc().refreshListPane(_ActivityId);   
            }
            else if(ListAction=="CustomList"){
                ppsc().refreshCustomListPane(_ActivityId);
            }
        }
    }

    function getPageID() {
        return el("PageSubmitPane_hdnID").value;
    }
        
    function getPageMode() {
        return el("PageSubmitPane_hdnPageMode").value;
    }
    
    function getTableName() {
        return el("PageSubmitPane_hdnTableName").value;
    }
    
    function getPageTitle()
    {
        var sURL = document.location.href;
        var sPageTitle = decodeURIComponent(getQueryStringValue(sURL,"pagetitle"));
        return sPageTitle;
    }

    function setButtonCaption(sSubmitAndGotoDashboard,sSubmit,sSubmitAndNext)
    {
        
        setText(el("btnSubmit"),sSubmit);
        
    }

    function setPageID(_id) {
        el("PageSubmitPane_hdnID").value = _id;
    }

    function setPageMode(_mode) {
      el("PageSubmitPane_hdnPageMode").value = _mode;
    }

    function setPageMode(_mode) {
      el("PageSubmitPane_hdnPageMode").value = _mode;
    }

    function getWFDATA()
    {
        var WFDATA = el('WFDATA').value;
        return WFDATA;
    }
    
    function showSubmitButton(bVisible)
    {
        if(bVisible)
            showDiv("btnSubmit");
        else
            hideDiv("btnSubmit");
    }
     function showSubmitPrintButton(bVisible)
    {
        if(bVisible)
            showDiv("btnSubmitPrint");
        else
            hideDiv("btnSubmitPrint");
    }
</script>
