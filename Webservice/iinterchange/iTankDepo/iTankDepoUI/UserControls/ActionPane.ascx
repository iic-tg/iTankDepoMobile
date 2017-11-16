<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ActionPane.ascx.vb" Inherits="UserControls_ActionPane" %>

<table align="center" class="actionpane" id="actionpane" style="width: 100%;" cellspacing="1"
    cellpadding="0">
    <tr>
        <td class="actiontd"  >
          <span id="spnAction" class="slbl" onclick="showActionInfo('',this);" style="cursor:hand">
                Action</span>
        </td>
        <td class="actiontd">
            <select id="ddlActions" class="sddl" onchange="changeAction()" style="width:150px">
            </select>
        </td>
        <td class="actiontd" >
            <div class="actionto">
                <span id="spnActionTo" class="slbl">Action To</span>
            </div>
        </td>
        <td class="actiontd" >
            <div class="actionto">
                <select id="ddlActionTo" class="sddl" style="width:150px">
                </select>
            </div>
        </td>
        <td class="actiontd" >
            <div class="actiondate">
                <span id="spnActionDate" class="slbl">Action Date</span>
            </div>
        </td>
        <td class="actiontd">
            <div class="actiondate" id="divdatActionDate">
                <Inp:iDate ID="datActionDate" TabIndex="1" runat="server" HelpText="" ToolTip="Action Date"
                    Width="80px" CssClass="dat" MaxLength="11" TopPosition="-200" LeftPosition="-100"
                    iCase="Upper">
                    <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup="actionpane"
                        Operator="LessThanEqual" LkpErrorMessage="Invalid Action Date. Click on the calendar icon for valid values"
                        ReqErrorMessage="Action Date Required." Validate="True" CsvErrorMessage="" CustomValidation="True"
                        CompareValidation="true" CmpErrorMessage="Action date should be less than or equal to current date."
                        CustomValidationFunction="" />
                    <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                </Inp:iDate></div>
        </td>
        <td class="actiontd" >
            <div class="reason">
                <span id="spnReason" class="slbl">Reason</span>
            </div>
        </td>
        <td class="actiontd">
            <div class="reason">
                <select id="ddlReason" class="sddl" onchange="changeReason(this);" style="width:150px">
                </select>
            </div>
        </td>
        <td class="actionbutton" align="right" >
            <div class="button" align="right">
                <ul>
                    <li>
                        <button href="#" tabindex="<%= TabIndex %>" data-corner="8px" id="btnSubmitAndGotoDashboard"
                            class="btncorner" onmouseover="this.className='sbtn'" onmouseout="this.className='btn'"
                            onclick="SubmitAndGotoDashboard_Click();return false;">
                            Submit & Go to Dashboard</button></li>
                    <li>
                        <button href="#" tabindex="<%= TabIndex %>" data-corner="8px" id="btnSubmit" class="btncorner"
                            onmouseover="this.className='sbtn'" onmouseout="this.className='btn'" onclick="Submit_Click();return false;">
                            Submit</button></li>
                    <li>
                        <button href="#" tabindex="<%= TabIndex %>" data-corner="8px" id="btnSubmitAndNext"
                            class="btncorner" onmouseover="this.className='sbtn'" onmouseout="this.className='btn'"
                            onclick="SubmitAndNext_Click();return false;">
                            Submit & New</button></li>
                </ul>
            </div>
        </td>
    </tr>
</table>
<input type="hidden" id="WFDATA" name="WFDATA" />
<asp:HiddenField ID="hdnID" runat="server" />
<asp:HiddenField ID="hdnPageMode" runat="server" />
<asp:HiddenField ID="hdnDefaultAction" runat="server" />
<asp:HiddenField ID="hdnDecisionReasons" runat="server" />
<asp:HiddenField ID="hdnActions" runat="server" />
<asp:HiddenField ID="hdnItemNo" runat="server" />
<asp:HiddenField ID="hdnActionDate" runat="server" />
<script src="../Script/UI/ActionPane.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var HasNoError=true;
    var SubmitAction="";
    var ListAction="";
    var bAllowAction=false;
    var bBulkAction=false;
    var bSubmitting=false;

    function SubmitAndGotoDashboard_Click() {
        SubmitAction = "SubmitAndGotoDashboard"
        bSubmitting = true;

        //update wfdata
        addAttData();

        //check rolerights 
        if (!checkRights())
        return false;
   
        //validate workflow data
        bAllowAction = validateData();

        if(bAllowAction)
        {
            HasNoError = <%= onClientAction %>; 
            
            if(HasNoError)
            {
                if(typeof(pdfs("dbFrame").dfs("dlstFrame").bindDashboardListGrid)!="undefined")
                {
                    pdfs("dbFrame").dfs("dlstFrame").bindDashboardListGrid();
                }
                if(typeof(pdfs("dbFrame").refreshDashboard)!="undefined")
                {
                    pdfs("dbFrame").refreshDashboard(false);
                }
                if(typeof(ppsc().loadHomePage)!="undefined")
                {
                ppsc().loadHomePage(); 
                }
            }
        }else{
            HasNoError = false;
        }
        bSubmitting=false;
        return false;

    }

    function GotoDashboard_Click() {
            SubmitAction = "GotoDashboard"
            hideMessage();
            if(typeof(pdfs("dbFrame").dfs("dlstFrame").bindDashboardListGrid)!="undefined")
            {
                pdfs("dbFrame").dfs("dlstFrame").bindDashboardListGrid();
            }
            if(typeof(ppsc().loadHomePage)!="undefined")
            {
                ppsc().loadHomePage(); 
            }
                
            return false;
    }

    function Submit_Click() {
        SubmitAction = "Submit"
        bSubmitting = true;
        //update wfdata
        addAttData();

        //check rolerights 
        if (!checkRights())
        return false;
   
        //validate workflow data
        bAllowAction = validateData();

        if(bAllowAction)
        {
            PageMode = getPageMode();
        
            HasNoError = <%= onClientAction %>;  

            if(bDBMode==false && (HasNoError || typeof(HasNoError)=="undefined") )
            {
                if(PageMode==MODE_EDIT)
                {
                    if(typeof(pdfs("dbFrame").dfs("dlstFrame").bindDashboardListGrid)!="undefined")
                    {
                        pdfs("dbFrame").dfs("dlstFrame").bindDashboardListGrid();
                    }

                    if(typeof(pdfs("dbFrame").refreshDashboard)!="undefined")
                    {
                        pdfs("dbFrame").refreshDashboard(false);
                    }
                }             
                disableActionPane();
            }
        }else{
            HasNoError = false;
        }
        bSubmitting=false;
        return false;
    }

    function SubmitAndNext_Click() {
        SubmitAction = "SubmitAndNext"        
        
        //update wfdata
        addAttData();

        //check rolerights 
        if (!checkRights())
        return false;
   
        //validate workflow data
        bAllowAction = validateData();

        if(bAllowAction)
        {

            PageMode = getPageMode();
          
            HasNoError = <%= onClientAction %>;

            HasChanges = false;

            if(HasNoError==true && bDBMode==false){      
                if(PageMode == MODE_NEW)
                {
                    var bqstr = new buildquerystring(document.location.href);
                    bqstr.additem("SubmitNext","1");
                    var _surl = bqstr.getquerystring();
                    ppsc().onMenuClick(_surl);
                }
                else if(PageMode == MODE_EDIT)
                {
                    if(typeof(pdfs("dbFrame").dfs("dlstFrame").bindDashboardListGrid)!="undefined")
                    {
                        pdfs("dbFrame").dfs("dlstFrame").bindDashboardListGrid();
                    }
                    nextRecord();
                }
            }
            ppsc().gsAction = "";
        }else{
            HasNoError = false;
        }
        
        return false;
    }

    function Next_Click() {
        SubmitAction = "Next"        
        PageMode = getPageMode();
        hideMessage();
      
        if(PageMode == MODE_NEW)
        {
            var bqstr = new buildquerystring(document.location.href);
            bqstr.additem("SubmitNext","1");
            var _surl = bqstr.getquerystring();
            ppsc().onMenuClick(_surl);
        }
        else if(PageMode == MODE_EDIT)
        {
            if(typeof(pdfs("dbFrame").dfs("dlstFrame").bindDashboardListGrid)!="undefined")
            {
                pdfs("dbFrame").dfs("dlstFrame").bindDashboardListGrid();
            }
            nextRecord();
        }
           
        ppsc().gsAction = "";      
        
        return false;
    }

    function checkRights()
    {
        var bCreateRights = getWFDataKey("CRT_BT");
        var bEditRights = getWFDataKey("EDT_BT");
        var sMode = getPageMode();
        var bAllowToSubmit =false;
       
        if(sMode == MODE_NEW && bCreateRights == true)
        {
            bAllowToSubmit = true;
           
        }
        else if(sMode == MODE_EDIT && bEditRights == true)
        {
            bAllowToSubmit = true;
        }
        else
        {
            HasChanges=false;
            showWarningMessage("You do not have sufficient rights to perform the selected operation.");
        }
        return bAllowToSubmit;
    
    }

</script>
