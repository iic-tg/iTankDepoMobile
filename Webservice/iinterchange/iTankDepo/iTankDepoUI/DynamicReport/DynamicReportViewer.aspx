<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DynamicReportViewer.aspx.vb" Inherits="DynamicReportViewer" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function SetTitle() {
            var _qs = new buildquerystring(location.href.substring(location.href.indexOf("?") + 1));
            document.title = unescape(_qs.getValue("title"));
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
        function fnSEM(_msg) {
            var messageDetails = new Object();
            messageDetails.message = _msg;
            messageDetails.title = "Error";

            if (window.showModalDialog)
                window.showModalDialog("Error.html", messageDetails, "dialogWidth:466px;dialogHeight:200px;status:no;help: No;");
            else
                window.alert(_msg);
        }
    </script>
</head>
<body leftmargin="0" topmargin="0" style="overflow: auto;">
    <form id="form1" runat="server" autocomplete="off">
    <div id="divDocView" style="width: 100%; height: 600px;" runat="server">
        <rsweb:ReportViewer BackColor="#cad0e8" InternalBorderColor="#e4edf8" ID="RptVwr"
            ProcessingMode="Remote" runat="server" TabIndex="-1" ShowFindControls="True"
            AsyncRendering="false" Width="100%" Height="100%">
        </rsweb:ReportViewer>
    </div>
    <table cellpadding="0" cellspacing="0" class="tblstd" width="100%">
        <tr>
            <td>
                <Inp:iLabel ID="ILabel1" runat="server" CssClass="lbl" Font-Bold="True" ForeColor="Red"></Inp:iLabel>
            </td>
        </tr>
    </table>
    <div>
        <asp:Panel ID="pnlEmail" runat="server" Height="98%" Width="100%">
            <table id="tblEmail">
                <tr>
                    <td colspan="4" align="left" style="background-color: #cad0e8;">
                        <Inp:iLabel ID="lblTitle" runat="server" ToolTip="Email" CssClass="blbl" Font-Size="Medium">Email</Inp:iLabel>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <Inp:iLabel ID="lblTo" runat="server" ToolTip="To" CssClass="lbl">To</Inp:iLabel>
                    </td>
                    <td class="style1">
                        :
                    </td>
                    <td class="style1">
                        <Inp:iTextBox ID="txtTo" runat="server" TabIndex="1" CssClass="txt" ToolTip="To"
                            HelpText="" Height="" Width="500px" MaxLength="100" TextMode="SingleLine">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                RegErrorMessage="Invalid Email Id" RegexValidation="True" RegularExpression="^([\w][\w\.-]+@([\w]+\.)+[a-zA-Z]{2,9}(\s*,\s*[\w][\w\.-]+@([\w]+\.)+[a-zA-Z]{2,9})*)$"
                                ReqErrorMessage="To Required" Validate="True" ValidationGroup="document" />
                        </Inp:iTextBox>
                    </td>
                    <td class="style1">
                    </td>
                    <%--<td>
                        <input id="btnSend" type="button" onclick="SendMail();" class="btn" value="Send" tabindex="4" />
                    </td>--%>
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
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <%--<td style="height: 24px">
                        <input id="btnCancel" class="btn" onclick="dEmailCancel();" tabindex="5" type="button" value="Cancel" />
                    </td>--%>
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
                        <%--             <div id="txtBody" runat="server" tabindex="3" contenteditable="true" indicateeditable="true"
                        style="background-color: white; overflow: auto; width: 500px; height: 350px;
                        word-wrap: break-word;" class="mltxt">
                    </div>--%>
                        <Inp:iTextBox ID="txtBody" runat="server" CssClass="txt" Height="300px" HelpText=""
                            iCase="None" MaxLength="4000" TabIndex="3" TextMode="MultiLine" ToolTip="Body"
                            Width="500px">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left">
                        <table align="center">
                            <tr>
                                <td>
                                    <a href="#" id="A1" data-corner="8px" style="text-decoration: none; color: white;
                                        font-weight: bold; height: 18px;" class="btn btn-small btn-info" runat="server"
                                        onclick="sendEmailDocument();return false;">&nbsp; Send</a>
                                </td>
                                <td>
                                    <a href="#" data-corner="8px" id="A2" style="text-decoration: none; color: White;
                                        font-weight: bold; height: 18px;" class="btn btn-small btn-info" runat="server"
                                        onclick="cancelEmail();return false;">Cancel</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:HiddenField ID="hdnTemplateId" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <%--<asp:Panel ID="pnlEmail" runat="server" Height="100%" Width="100%">
            <table id="tblEmail">
                <tr>
                    <td colspan="4" align="left" style="background-color: #cad0e8;">
                        <Inp:iLabel ID="lblTitle" runat="server" ToolTip="Email" CssClass="blbl" Font-Size="Medium">Email</Inp:iLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <Inp:iLabel ID="lblTo" runat="server" ToolTip="To" CssClass="lbl">To</Inp:iLabel>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtTo" runat="server" TabIndex="1" CssClass="txt" ToolTip="To" HelpText="" Height="" Width="820px" MaxLength="100" TextMode="SingleLine">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True" RegErrorMessage="Invalid Email Id" RegexValidation="True" RegularExpression="^([\w][\w\.-]+@([\w]+\.)+[a-zA-Z]{2,9}(\s*,\s*[\w][\w\.-]+@([\w]+\.)+[a-zA-Z]{2,9})*)$" ReqErrorMessage="To Required" Validate="True" ValidationGroup="document" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                        <input id="btnSend" type="button" onclick="SendMail();" class="btn" value="Send" tabindex="4" />
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
                        <Inp:iTextBox ID="txtSubject" runat="server" CssClass="txt" Height="" HelpText="" iCase="None" MaxLength="100" TabIndex="2" ToolTip="Subject" Width="820px">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                        </Inp:iTextBox>
                    </td>
                    <td style="height: 24px">
                        <input id="btnCancel" class="btn" onclick="dEmailCancel();" tabindex="5" type="button" value="Cancel" />
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
                        <Inp:iLabel ID="lblAttached" runat="server" CssClass="lbl" Font-Bold="True"></Inp:iLabel>
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
                        <div id="txtBody" runat="server" tabindex="3" contenteditable="true" indicateeditable="true" style="background-color: white; overflow: auto; width: 820px; height: 450px; word-wrap: break-word;" class="mltxt">
                        </div>
                        <%--<Inp:iTextBox ID="txtBody" runat="server" CssClass="txt" Height="450px" HelpText=""
                            iCase="None" MaxLength="1000" TabIndex="3" TextMode="MultiLine" ToolTip="Body"
                            Width="820px">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdnTemplateId" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>--%>
    </div>
    <%--<asp:Panel ID="pnlRecordNotFound" runat="server" Style="margin: 10px; font-style: italic;
        font-family: Arial; font-size: 12pt; width: 100%; height: 100%" Font-Bold="True"
        Font-Italic="True" HorizontalAlign="Center">
        <Inp:iLabel ID="lblListRowCount" runat="server" Visible="true">No Records Found</Inp:iLabel>
    </asp:Panel>--%>
    <input type="hidden" runat="server" id="hdnattachfile" />
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <object id="RSClientPrint" classid="CLSID:41861299-EAB2-4DCC-986C-802AE12AC499" codebase="<% = GetRSCabURL()%>"
        viewastext>
    </object>
    <script type="text/javascript" language="javascript">
     
        var p = 0;

        function setFocus() {
            SetFocusToField("txtTo");
        }


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
    oCallback.invoke("DynamicReportViewer.aspx", "SendMail");
    if (oCallback.getCallbackStatus()) {
        ppsc().showInfoMessage("Email Sent Successfully.");
        ppsc().gsStayMessage = true;
        ppsc().closeModalWindow();              
    }
    
    oCallback = null;
}	

        
 		function Print()
		{		   
		    var RSClientPrint = document.getElementById("RSClientPrint")
            if (typeof RSClientPrint.Print == "undefined")
            {
            alert("Unable to load client print control.");
            return;
            }
            
            RSClientPrint.MarginLeft =  <%= rsLeftMargin %>;
            RSClientPrint.MarginTop = <%= rsTopMargin %>;
            RSClientPrint.MarginRight = <%= rsRightMargin %>;
            RSClientPrint.MarginBottom = <%= rsBottomMargin %>;

            RSClientPrint.PageHeight = <%= rsPageHeight %>;
            RSClientPrint.PageWidth = <%= rsPageWidth %>;

            RSClientPrint.Culture = 1033;
            RSClientPrint.UICulture = 9;
            RSClientPrint.Print(<%= rsclientprintparams %>)
    	}
		
    function FindControl() {
        // printDOMTree(document.getElementById('ReportViewer1'));
        // Make sure browser supports this dom method 
        if(!document.getElementsByTagName ) return; 
        // Get the input nodes 
        var printNodeList = document.getElementsByTagName("a"); 
        // Make sure something was found 
        if(!printNodeList) return; 
        // Iterate through the list 
        for( var i = 0; i < printNodeList.length; i++ ) {
            // Get an individual node
            var printNode = printNodeList.item(i);
            
            //alert(printNode.type);
            if( printNode.type.toLowerCase() == "image" && printNode.alt == "Print") {
                //alert(printNode.name);
                printNode.attachEvent('onclick', Print);
                printNode.style.display="none";
            }
        }
        
//        var exportNodeList = document.getElementsByTagName("a"); 
//        // Make sure something was found 
//        if(!exportNodeList) return; 
//        // Iterate through the list 
//        for( var i = 0; i < exportNodeList.length; i++ ) {
//            // Get an individual node
//            var exportNode = exportNodeList.item(i);
//            
//            if(exportNode.innerText == "Export") {
//                //alert(printNode.name);
//                exportNode.attachEvent('onclick', exportClicked)
//            }
//        }
    }
      
    
    </script>
    <div class="vals" id="DIV1">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="document" />
    </div>
    <div class="HSParent" style="visibility: hidden">
        <div class="HelpStrip" id="divHelpStrip">
        </div>
    </div>
    </form>
</body>
</html>
