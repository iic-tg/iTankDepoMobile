<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportError.aspx.vb" Inherits="Admin_ReportError" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="overflow:auto">
    <div>
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
                        HelpText="" Height="" Width="450px" MaxLength="100" TextMode="SingleLine">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                            RegErrorMessage="Invalid Email Id" RegexValidation="True" RegularExpression="[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]{1,}([,][a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]{1,})*"
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
                        iCase="None" MaxLength="100" TabIndex="2" ToolTip="Subject" Width="450px">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="true"
                            ReqErrorMessage="Subject Required" Validate="true" ValidationGroup="document" />
                    </Inp:iTextBox>
                </td>
                <td style="height: 24px" align="left">
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lblBody" runat="server" CssClass="lbl" ToolTip="Body">
                                    Body
                    </Inp:iLabel>
                </td>
                <td>
                    :
                </td>
                <td>
                   
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
                    <Inp:iTextBox ID="txtBody" runat="server" CssClass="txt" Height="150px" HelpText=""
                        iCase="None" MaxLength="255" TabIndex="3" TextMode="MultiLine" ToolTip="Body"
                        Width="450px">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                    </Inp:iTextBox>                    
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <div class="button" style="width: 200px">
                        <ul>
                            <li><a data-corner="8px" class="btncorner btn btn-success" id="btnSend" href="#" onclick="sendEmail();return false;" style=" text-decoration:none">
                                <i class="icon-envelope"></i>Send</a></li>                            
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript" language="javascript">
 ////This method used to send email
    function sendEmail() {
    Page_ClientValidate();
    if (!Page_IsValid) {
        return false;
    }
    var oCallback = new Callback();
    oCallback.add("EmailTo", el("txtTo").value)
    oCallback.add("EmailSubject", el("txtSubject").value)
    oCallback.add("EmailBody", el("txtBody").value)    
    oCallback.invoke("ReportError.aspx", "ReportError");
    if (oCallback.getCallbackStatus()) {
        showInfoMessage("Error Reported Successfully.");
        ppsc().gsStayMessage = true;
        ppsc().closeModalWindow();              
    }
    
    oCallback = null;
}	

    </script>
</html>
