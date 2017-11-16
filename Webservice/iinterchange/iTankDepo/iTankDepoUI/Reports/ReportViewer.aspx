<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportViewer.aspx.vb" Inherits="Reports_ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html>
<head id="Head1" runat="server">
    <title>Document Viewer</title>
    <style type="text/css">
        .vertical-scroll {
    height:600px;
    width:600px;
    overflow-x:hidden;
    overflow-y:scroll;
    border:1px solid;
}
    </style>
    <script language="javascript" type="text/javascript">
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
        function PrintDocument() {
           
            printReport('PrintViewerFrame');
        }
        

    </script>
    <%--<script src="../Script/UI/jquery-1.2.6.min.js" type="text/javascript"></script>
    <script src="../Script/Reports/ReportDialog.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../Script/Common.js"></script>
    <script language="javascript" type="text/javascript" src="../Script/Callback.js"></script>
    <script language="javascript" type="text/javascript" src="../Script/Controls/WebUIValidation.js"></script>--%>
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0" style="overflow: auto" >
    <%--<body onload="setFocus();" style="background-color: White; margin: 0px" onresize="resizeElementHeight(el('DocViewer'));return false">--%>
    <form id="form1" runat="server" autocomplete="off" >
    <%--<div id="divPrint" title="Print" runat="server" align="center" style="position: absolute;
        margin-left: 650px; width: 26px; height: 23px; vertical-align: bottom; margin-top: 5px;
        padding-top: 2px">
        <img id="imgPrint" class="lbl" onclick="PrintDoc();" src="../Images/print.gif" onmouseover="document.getElementById('divPrint').className='overf';"
            onmouseout="document.getElementById('divPrint').className='overo';"" style="z-index:200;cursor:hand" />
    </div>--%>
    <asp:Panel ID="pnlDocView" runat="server" Width="100%" Height="620px">
        <rsweb:ReportViewer BackColor="#cad0e8" InternalBorderColor="#e4edf8" ID="DocViewer"
            runat="server" TabIndex="-1" ShowFindControls="True" AsyncRendering="False" Width="100%"
            Height="100%">
        </rsweb:ReportViewer>
        
    </asp:Panel>
      
    
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
                            RegErrorMessage="Invalid Email Id" RegexValidation="True" RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$"
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


  <div>
    <asp:Panel ID="pnlPrint" runat="server" CssClass="vertical-scroll">
    <table>
    <tr>
    <td align="left" style="background-color: #cad0e8;">
                    <Inp:iLabel ID="ILabel1" runat="server" ToolTip="Email" CssClass="blbl" Font-Size="Medium">Print</Inp:iLabel>
                </td>
                </tr>
    <tr>
    <td>
     <iframe width="100%" name="PrintViewerFrame" height="600px" Width="100%" frameborder="0" 
             id="PrintViewerFrame" src="<%=SomeValue %>" type="application/pdf" marginwidth="0" marginheight="0" scrolling="auto">
        </iframe>
        </td>
        
        
        <td  align="right">
                    
                                <a href="#" id="A3" data-corner="8px" style="text-decoration: none; color: white;
                                    font-weight: bold; height: 18px;" class="btn btn-small btn-info" runat="server"
                                    onclick="PrintDocument();return false;">&nbsp; Print</a>
                           
                </td>
                </tr>
                </table>
 </asp:Panel>
 </div>
    <input type="hidden" runat="server" id="hdnattachfile" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--    <object id="RSClientPrint" classid="CLSID:41861299-EAB2-4DCC-986C-802AE12AC499" codebase="<% = GetRSCabURL()%>"
        viewastext>
    </object>  --%>
    <script type="text/javascript" language="javascript">
        var p = 0;

        function setFocus() {
            SetFocusToField("txtTo");
        }

        function SendMail() {
            Page_ClientValidate();
            if (!Page_IsValid) {
                return;
            }
            parent.document.Script.ShowDocumentLoading();

            setTimeout(SendCallback, 0);
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
    oCallback.add("TemplateID", el("hdnTemplateId").value)
    oCallback.invoke("ReportViewer.aspx", "CreateAlert");
    if (oCallback.getCallbackStatus()) {
        ppsc().showInfoMessage("Email Sent Successfully.");
        gsStayMessage = true;
        closeModalWindow();              
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
    
   function printReport(frame_ID) {
    var iframe = document.getElementById(frame_ID);
    var iframe_contents = iframe.contentWindow.document.body.innerHTML

    $('#PrintViewerFrame').attr('src', iframe_contents);
        var frame = document.getElementById('PrintViewerFrame');
        if (!frame) {
            alert("Error: Can't find printing frame.");
            return;
        }
        frame = frame.contentWindow;
        frame.focus();
        frame.print();
        gsStayMessage = true;
        closeModalWindow(); 
} 

//$(document).ready(function() {
//    $('#PrintViewerFrame').ready(function() {
//    var iframe = document.getElementById("PrintViewerFrame");
//    var iframe_contents = iframe.contentWindow.document.body.innerHTML

//    $('#PrintViewerFrame').attr('src', iframe_contents);
//        var frame = document.getElementById('PrintViewerFrame');
//        if (!frame) {
//            alert("Error: Can't find printing frame.");
//            return;
//        }
//        frame = frame.contentWindow;
//        frame.focus();
//        frame.print();
////        gsStayMessage = true;
////        closeModalWindow(); 
//    });
//})
//function PrintPanel() {
//            var panel = document.getElementById("<%=pnlPrint.ClientID %>");
//            var printWindow = window.open('', '', 'height=400,width=800');
////            printWindow.document.write('<html><head><title>DIV Contents</title>');
////            printWindow.document.write('</head><body >');
//            printWindow.document.write(panel.innerHTML);
////            printWindow.document.write('</body></html>');
//            printWindow.document.close();
//            setTimeout(function () {
//                printWindow.print();
//            }, 500);
//            return false;
//        }
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
