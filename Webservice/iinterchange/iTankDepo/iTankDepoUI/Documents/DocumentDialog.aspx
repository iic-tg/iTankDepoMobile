<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DocumentDialog.aspx.vb"
    Inherits="DocumentDialog" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
    <title>Document Print Dialog</title>
 
</head>
<body class="backgr">
    <form id="form1" runat="server" >
    <div id="shadow" class="opaqueLayer">
    </div>
    <div id="question" class="questionLayer">
       <%-- <table id="tblmodalwindow" style="width: 150px; height: 30px">--%>
        <table id="tblmodalwindow" style="width: 180px">
            <tr>
                <td id="tdinfomodalwindowtitleimage" style="width: 40px;">
                    <input id="imgloading" type="image" class="loadingimg" src="../Images/ajloader.gif" />
                </td>
                <td id="tdmodalwindowtitle" class="lmodalwindowtitle" style="width: 110px;">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div id="divEmail" title="Email" runat="server" align="center" style="position: absolute;
            margin-left: 695px; width: 26px; height: 23px; vertical-align: bottom; margin-top: 3px;
            display: block; padding-top: 2px">
            <img id="imgEmail" alt="Email" class="lbl" onclick="createEmail();" src="../Images/email.gif"
                onmouseover="el('divEmail').className='overf';" onmouseout="el('divEmail').className='overo';" />
        </div>
         <div id="divTemplates" title="Templates" runat="server" align="center" style="position: absolute;
            margin-left: 770px; vertical-align: bottom; margin-top: 6px; padding-top: 2px">
            <asp:DropDownList ID="ddlTemplates" runat="server" CssClass="lbl" Width="165px">
                
            </asp:DropDownList>
        </div>
        <table class="rTblStd" border="0" cellpadding="0" cellspacing="0">
            <tr style="display: none">
                <td align="left" class="MenuHeader" style="text-align: left" colspan="3">
                    <Inp:iLabel ID="lblReportHeader" runat="server"></Inp:iLabel>
                    <div id="pndLoader" style="position: absolute; visibility: visible; margin-left: 920px;
                        margin-top: 10px; left: 0px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnDefaultTemplateId" runat="server" />
                </td>
                <td colspan="2" style="text-align: center">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="2">
                </td>
            </tr>
        </table>
        </div>
      <%--  <div id="reportpane" style="display: block; height: 550px;" >--%>
           <%-- <iframe width="100%" name="fmReport" height="100%" frameborder="0" marginheight="0"--%>
             <iframe width="100%" name="fmReport" height="700px" frameborder="0" marginheight="0"
                id="fmReport" src="about:blank" marginwidth="0" style="margin-top: 0px"
                scrolling="auto"></iframe>
       <%-- </div>--%>
        <div id="lblMsg" align="center" style="font-weight: bold; display: none; color: Red"
            class="lbl">
            Email has been sent</div>
    </div>
  
    </form>
</body>
</html>
