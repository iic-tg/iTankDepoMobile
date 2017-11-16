<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SendEdiEmail.aspx.vb" Inherits="EDI_SendEdiEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <div>
            <table id="tblEmail">
                <tr>
                    <td style="height: 24px">
                        <Inp:iLabel ID="lblFrom" runat="server" CssClass="lbl">From</Inp:iLabel>
                    </td>
                    <td style="height: 24px">
                        :
                    </td>
                    <td style="height: 24px">
                        <Inp:iTextBox ID="txtFrom" runat="server" CssClass="txt" Height="" HelpText="413,BULK_EMAIL_FRM_EML" iCase="None" MaxLength="100" TabIndex="1" ToolTip="From" Width="450px">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="true" ReqErrorMessage="From Required" Validate="true" RegexValidation="true"  RegularExpression="^([0-9a-zA-Z]([-.&%+\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"  RegErrorMessage ="Invalid From Email ID" ValidationGroup="document" />
                        </Inp:iTextBox>
                    </td>
                    <td style="height: 24px" align="left">
                    </td>
                </tr>
                <tr>
                    <td>
                        <Inp:iLabel ID="lblTo" runat="server" CssClass="lbl">To</Inp:iLabel>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtTo" runat="server" TabIndex="1" CssClass="txt" ToolTip="To" HelpText="414,BULK_EMAIL_TO_EML" Height="" Width="450px" MaxLength="100" TextMode="SingleLine">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True" RegErrorMessage="Invalid Email Id" RegexValidation="True" RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$" ReqErrorMessage="To Required" Validate="True" ValidationGroup="document" />
                        </Inp:iTextBox>
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td>
                        <Inp:iLabel ID="lblBCC" runat="server" CssClass="lbl">Bcc</Inp:iLabel>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtBCC" runat="server" TabIndex="1" CssClass="txt" ToolTip="BCC" HelpText="415,BULK_EMAIL_BCC_EML" Height="" Width="450px" MaxLength="100" TextMode="SingleLine" ReadOnly="true">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="False" RegErrorMessage="Invalid Email Id" RegexValidation="True" RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$" ReqErrorMessage="Bcc Required" Validate="True" ValidationGroup="document" />
                        </Inp:iTextBox>
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td>
                        <Inp:iLabel ID="lblSubject" runat="server" CssClass="lbl">Subject</Inp:iLabel>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtSubject" runat="server" TabIndex="1" CssClass="txt" ToolTip="Subject" HelpText="416,BULK_EMAIL_SBJCT_VC" Height="" Width="450px" MaxLength="100" TextMode="SingleLine">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True" Validate="True" ValidationGroup="document" ReqErrorMessage="Subject Required" />
                        </Inp:iTextBox>
                    </td>
                    <td align="left">
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
                        <Inp:iLabel ID="lblBody" runat="server" CssClass="lbl">
                                    Body
                        </Inp:iLabel>
                    </td>
                    <td valign="top">
                        :
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtBody" runat="server" CssClass="txt" Height="150px" HelpText="417" iCase="None" MaxLength="4000" TabIndex="1" TextMode="MultiLine" ToolTip="Body" Width="450px">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                        </Inp:iTextBox>
                    <%--  <div id="txtBody" runat="server" contenteditable="true" style="width:450px;height:150px;border:solid 1px #cdd4d5;overflow-y: scroll;"></div>--%>
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
                        &nbsp;</td>
                    <td>
                    </td>
                </tr>
            </table>          
            <table align="center">
                <tr>
                    <td>
                    </td>
                    <td colspan="3">
                        <a href="#" id="hypSendMail" class="btn btn-small btn-info" onclick="SubmitBulkDetails();return false;" runat="server" style="font-weight: bold"><i class="icon-print"></i>&nbsp;Send</a>
                    </td>
                    <td colspan="3">
                        <a href="#" id="hypClose" class="btn btn-small btn-info" onclick="CloseDetails();return false;" runat="server" style="font-weight: bold">&nbsp;Close</a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnCustomer" runat="server" />
          <asp:HiddenField ID="hdnEDI" runat="server" />
           <asp:HiddenField ID="hdnattachfile" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
