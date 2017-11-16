<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DocumentViewer.aspx.vb"
    Inherits="Document_DocumentViewer" %>

<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
    <title>Document Viewer</title>
    <script language="javascript">
        function resizeElementHeight(element) {
            if (element == null)
                return;

            var height = 0;
            var body = window.document.body;
            if (window.innerHeight) {
                height = window.innerHeight;
            } else if (body.parentElement.clientHeight) {
                height = body.parentElement.clientHeight;
            } else if (body && body.clientHeight) {
                height = body.clientHeight;
            }
            element.style.height = ((height - element.offsetTop) + "px");
        }
        function PrintDoc() {

            printReport('rvDocuments');
        }
        function initPage() {
            if (chrome || Firefox) {
                showDiv("divPrint");
            }
            else {
                hideDiv("divPrint");
            }
        }
    </script>
</head>
<body onload="setFocus();initPage();" style="background-color: White; margin: 0px" onresize="resizeElementHeight(el('rvDocuments'));return false" >
  <%--  <form id="form1" runat="server" autocomplete="off" style="overflow: auto">--%>
    <form id="form1" runat="server" autocomplete="off" style="overflow: auto">
    <asp:ScriptManager ID="reportviewer" runat="server" AsyncPostBackTimeout="0" >
    </asp:ScriptManager>
    <div id="divPrint" title="Print" runat="server" align="center" style="position: absolute;
        margin-left: 650px; width: 26px; height: 23px; vertical-align: bottom; margin-top: 5px;
        padding-top: 2px">
        <img id="imgPrint" class="lbl" onclick="PrintDoc();" src="../Images/print.gif" onmouseover="document.getElementById('divPrint').className='overf';"
            onmouseout="document.getElementById('divPrint').className='overo';"" style="z-index:200;cursor:hand" />
    </div>
   <%-- <asp:Panel ID="pnlDocView" runat="server" Height="430px" Width="100%" ScrollBars="none">--%>
    <asp:Panel ID="pnlDocView" runat="server" Height="570px" Width="100%" ScrollBars="Auto">
        <rsweb:ReportViewer ID="rvDocuments" Height="100%" Width="100%" TabIndex="-1" BackColor="#ACC4E6"
            InternalBorderColor="228, 237, 248" runat="server" AsyncRendering="False" Font-Names="Arial"
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Arial"
            WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="E:\iInterchange\iTankDepo\iTankDepo\iTankDepoUI\Documents\Report\UserEstimate.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    </asp:Panel>
    <asp:Panel ID="pnlEmail" runat="server" Height="98%" Width="100%">
        <table id="tblEmail">
            <tr>
                <td>
                    <Inp:iLabel ID="lblTo" runat="server" ToolTip="To" CssClass="lbl">To</Inp:iLabel>
                </td>
                <td>
                    :
                </td>
                <td>
                    <Inp:iTextBox ID="txtTo" runat="server" TabIndex="1" CssClass="txt" ToolTip="To"
                        HelpText="" Height="" Width="500px" MaxLength="100" TextMode="SingleLine">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                            RegErrorMessage="Invalid Email Id" RegexValidation="True"  RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$"
                            ReqErrorMessage="To Required" Validate="True" ValidationGroup="document" />
                    </Inp:iTextBox>
                </td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td style="height: 24px">
                    <Inp:iLabel ID="lblSubject" runat="server" ToolTip="Subject" CssClass="lbl">Subject</Inp:iLabel>
                </td>
                <td style="height: 24px">
                    :
                </td>
                <td style="height: 24px">
                    <Inp:iTextBox ID="txtSubject" runat="server" CssClass="txt" Height="" HelpText=""
                        iCase="None" MaxLength="100" TabIndex="2" ToolTip="Subject" Width="500px">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="true"
                            ReqErrorMessage="Subject Required" Validate="true" ValidationGroup="document" />
                    </Inp:iTextBox>
                </td>
                <td style="height: 24px" align="left">
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lvlAttached1" runat="server" ToolTip="Attached" CssClass="lbl">
                                    Attached</Inp:iLabel>
                </td>
                <td>
                    :
                </td>
                <td>
                    <a id="lnkAttachment" class="blnk" runat="server" href="#" target="_blank"></a>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <Inp:iLabel ID="lblBody" runat="server" CssClass="lbl" ToolTip="Body">
                                    Body
                    </Inp:iLabel>
                </td>
                <td valign="top">
                    :
                </td>
                <td>
                    <%--    <div id="txtBody" runat="server" tabindex="3" contenteditable="true" indicateeditable="true"
                            style="background-color: white; overflow: auto; width: 500px; height: 350px;
                            word-wrap: break-word;" class="mltxt">
                            </div>--%>
                    <Inp:iTextBox ID="txtBody" runat="server" CssClass="txt" Height="150px" HelpText=""
                        iCase="None" MaxLength="4000" TabIndex="3" TextMode="MultiLine" ToolTip="Body"
                        Width="500px">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                    </Inp:iTextBox>
                    <asp:HiddenField ID="hdnTemplateId" runat="server" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table align="center">
                        <tr>
                            <td>
                                <a href="#" id="btnSend" data-corner="8px" style="text-decoration: none; color: white;
                                    font-weight: bold; height: 18px;" class="btn btn-small btn-info" runat="server"
                                    onclick="sendEmailDocument();return false;">&nbsp; Send</a>
                            </td>
                            <td>
                                <a href="#" data-corner="8px" id="btnCancel" style="text-decoration: none; color: White;
                                    font-weight: bold; height: 18px;" class="btn btn-small btn-info" runat="server"
                                    onclick="cancelEmail();return false;">Cancel</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlExport" runat="server" Height="100%" Width="100%">
        <br />
        <Inp:iLabel ID="lblExport" runat="server" CssClass="lbl"> </Inp:iLabel>
        <br />
        <br />
        <Inp:iLabel ID="lblExportLink" runat="server" CssClass="lbl">Please <a id="lnkExport" class="blnk"  runat="server" HREF="#" target="_blank" visible="false"  >  click here </a> to download the same. </Inp:iLabel>
    </asp:Panel>
    <script type="text/javascript" language="javascript">
        var p = 0;

        function setstart() {

            var els = document.getElementsByTagName('div');


            var i = els.length;


            while (i--)


                if (els[i].id.indexOf('ReportDiv') > 0)

                    els[i].style.overflow = 'visible';

        }



        function setFocus() {
            setFocusToField("txtTo");
            setstart();
        }

        function print()
		{		   
		    var RSClientPrint = document.getElementById("RSClientPrint")
            if (typeof RSClientPrint.Print == "undefined")
            {
            alert("Unable to load client print control.");
            return;
            }

            RSClientPrint.MarginLeft = 12.7;
            RSClientPrint.MarginTop = 12.7;
            RSClientPrint.MarginRight = 12.7;
            RSClientPrint.MarginBottom = 12.7;


            RSClientPrint.PageHeight = 279.4;
            RSClientPrint.PageWidth = 215.9;

            RSClientPrint.Culture = 1033;
            RSClientPrint.UICulture = 9;
            RSClientPrint.Print(<%= rsclientprintparams %>)
    	}	
        
        ////This method used to send email
function sendEmailDocument() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    var oCallback = new Callback();
    oCallback.add("EmailTo", el("txtTo").value)
    oCallback.add("EmailSubject", el("txtSubject").value)
    oCallback.add("EmailBody", el("txtBody").innerHTML)
    oCallback.add("AttachFile", el("lnkAttachment").innerText)
    oCallback.add("TransactionNo", el("hdnTransactionNo").value)
    oCallback.add("TemplateID", el("hdnTemplateId").value)
    oCallback.add("WFDATA", getCWfData(document.location.href));
    oCallback.invoke("DocumentViewer.aspx", "CreateAlert");
    if (oCallback.getCallbackStatus()) {
        ppsc().showInfoMessage("Email Sent Successfully.");    
            gsStayMessage = true; 
            closeModalWindow();         
    }
    
    oCallback = null;
}	
function printReport(report_ID) {
    var rv1 = $('#' + report_ID);
    var iDoc = rv1.parents('html');

    // Reading the report styles
    var styles = iDoc.find("head style[id$='ReportControl_styles']").html();
    if ((styles == undefined) || (styles == '')) {
        iDoc.find('head script').each(function () {
            var cnt = $(this).html();
            var p1 = cnt.indexOf('ReportStyles":"');
            if (p1 > 0) {
                p1 += 15;
                var p2 = cnt.indexOf('"', p1);
                styles = cnt.substr(p1, p2 - p1);
            }
        });
    }
    if (styles == '') { alert("Cannot generate styles, Displaying without styles.."); }
    styles = '<style type="text/css">' + styles + "</style>";

    // Reading the report html
    var table = rv1.find("div[id$='_oReportDiv']");
    if (table == undefined) {
        alert("Report source not found.");
        return;
    }

    // Generating a copy of the report in a new window
    var docType = '<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/loose.dtd">';
    var docCnt = styles + table.parent().html();
    var docHead = '<head><title></title><style>body{margin:2;padding:0;}</style></head>';
    var winAttr = "location=yes,statusbar=no,directories=no,menubar=no,titlebar=no,toolbar=no,dependent=no,width=720,height=650,resizable=yes,screenX=200,screenY=200,personalbar=no,scrollbars=yes"; ;
    var newWin = window.open("", "_blank", winAttr);
    writeDoc = newWin.document;
    writeDoc.open();
    writeDoc.write(docType + '<html>' + docHead + '<body onload="window.print();">' + docCnt + '</body></html>');
    writeDoc.close();

    // The print event will fire as soon as the window loads
    newWin.focus();
    // uncomment to autoclose the preview window when printing is confirmed or canceled.
    // newWin.close();
};
    </script>
    <div class="vals" id="DIV1">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="document" />
    </div>
    <div class="HSParent">
        <div class="HelpStrip" id="divHelpStrip">
        </div>
    </div>
    <input type="hidden" runat="server" id="hdnTransactionNo" />
    <input type="hidden" runat="server" id="hdnattachfile" />
    <%--<object id="RSClientPrint"  classid="CLSID:41861299-EAB2-4DCC-986C-802AE12AC499" codebase=<% GetRSCabURL()%>
        viewastext>
    </object>--%>
    <!-- <object id="RSClientPrint"  classid="CLSID:60677965-AB8B-464f-9B04-4BA871A2F17F" codebase=<% GetRSCabURL()%>
        viewastext>
    </object>-->
    </form>
</body>
</html>
