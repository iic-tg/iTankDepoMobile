<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportDialog.aspx.vb" Inherits="ReportDialog" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>Document Print Dialog</title>

</head>
<body class="backgr" onload="initPage();">
    <form id="form1" runat="server">
    <div id="shadow" class="opaqueLayer">
        </div>
        <div id="question" class="questionLayer">
            <table id="tblmodalwindow" style="width: 150px; height: 30px">
                <tr>
                    <td id="tdinfomodalwindowtitleimage" style="width: 40px;">
                        <input id="imgloading" type="image" class="loadingimg" src="../Images/ajloader.gif" />
                    </td>
                    <td id="tdmodalwindowtitle" class="lmodalwindowtitle" style="width: 110px;">
                    </td>
                </tr>
            </table>
    </div>
 <div id="divPrint" title="Print" runat="server" align="center" style="position: absolute;
        margin-left: 745px; width: 26px; height: 23px; vertical-align: bottom; margin-top: 5px;
        padding-top: 2px">
        <img id="imgPrint" class="lbl" onclick="PrintDoc();" src="../Images/print.gif" onmouseover="document.getElementById('divPrint').className='overf';"
            onmouseout="document.getElementById('divPrint').className='overo';"" />
    </div>
    <div id="divEmail" title="Email" runat="server" align="center" style="position: absolute;
        margin-left: 695px; width: 26px; height: 23px; vertical-align: bottom; margin-top: 3px;
        padding-top: 2px">
      <img id="imgEmail" class="lbl" onclick="createEmail();" src="../Images/email.gif" onmouseover="document.getElementById('divEmail').className='overf';"
            onmouseout="document.getElementById('divEmail').className='overo';"" />
    </div>
    <table class="rTblStd" border="0" cellpadding="0" cellspacing="0">
            <tr style="display: block">
            <td align="left" class="MenuHeader" style="text-align: left" colspan="3">
                <Inp:iLabel ID="lblReportHeader" runat="server"></Inp:iLabel>
                <div id="pndLoader" style="margin-left: 920px;
                    margin-top: 10px; left: 0px; display: none;">                  
                </div>
            </td>
        </tr>
      
        <tr valign="top" style="display: none" id="trTemplateBind" runat="server">
            <td>
            </td>
            <td style="width: 800px">
                <div id="divTop1" runat="server" visible="false" style="overflow: auto; height: 80px;
                    width: 700px; vertical-align: top" onclick="return false" onmousedown="return false"
                    onmousemove="return false" onmouseup="return false" ondblclick="return false">
                    <asp:ListBox ID="lbTemplates1" runat="server" CssClass="lbx" Width="200px" Height="67px">
                    </asp:ListBox>
                <input type="button" id="btnSetDefault" onclick="CreatePrintDocument();" class="btn"
                        runat="server" value="Set as default" style="width: 100px" />
                    <div id="imgbtnPrint" onmouseout="mToolOut('imgbtnPrint');" onclick="dPrint();" onmouseover="mToolOver('imgbtnPrint');"
                        align="center" class="Reportsitem">
                        <img onmouseover="mToolOver('imgbtnPrint');" src="../Images/rPrint.png" style="margin-top: 3px" />
                        Print
                    </div>
                    <div id="imgbtnEmail" onmouseout="mToolOut('imgbtnEmail');" onclick="createEmail();" onmouseover="mToolOver('imgbtnEmail');"
                        align="center" class="Reportsitem">
                        <img onmouseover="mToolOver('imgbtnEmail');" style="margin-top: 7px;" src="../Images/email.png" />
                        Email
                    </div>
                </div>
            </td>
            <td>
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
    <div>
        <div id="lblMsg" align="center" style="font-weight: bold; display: none; color: Red" 
            class="lbl">
            Email has been sent</div>
        <iframe width="100%" name="ReportViewerFrame" height="750px" frameborder="0" 
             id="ReportViewerFrame" src="about:blank" marginwidth="0" marginheight="0" scrolling="auto">
        </iframe>
    </div>
    <script type="text/javascript">
       // fnCheckDefaultReportLoading();
    </script>
    </form>
</body>
</html>
