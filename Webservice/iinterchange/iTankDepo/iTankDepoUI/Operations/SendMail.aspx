<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SendMail.aspx.vb" Inherits="Operations_BulkMail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="overflow:auto">
    <div class="tabdisplay">
        <table id="tblEmail" width="100%">
            <tr>
                <td>
                    <label id="lblFrom" runat="server" class="lbl">
                        From</label>
                </td>
                <td>
                    :
                </td>
                <td>
                    <Inp:iTextBox ID="txtFrom" runat="server" CssClass="txt" TabIndex="1" HelpText="174,BULK_EMAIL_FRM_EML"
                        TextMode="SingleLine" iCase="None" ReadOnly="false" Width="300px" MaxLength="50">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            ValidationGroup="" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            RegularExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" IsRequired="true"
                            CustomValidation="False" RegexValidation="true" RangeValidation="False" RegErrorMessage="Enter Valid From ID"
                            ReqErrorMessage="Required From ID" />
                    </Inp:iTextBox>
                </td>
                <td align="left">
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
                    <Inp:iTextBox ID="txtTo" runat="server" CssClass="txt" TabIndex="2" HelpText="175,BULK_EMAIL_TO_EML"
                        TextMode="SingleLine" iCase="None" ReadOnly="false" Width="300px">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            ValidationGroup="" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            RegularExpression="[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]{1,}([,][a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]{1,})*"
                            IsRequired="true" CustomValidation="False" RegexValidation="true" RangeValidation="False"
                            ReqErrorMessage="To ID Required" RegErrorMessage="Enter Valid To ID" />
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
                    <Inp:iTextBox ID="txtSubject" runat="server" CssClass="txt" TabIndex="3" HelpText="176,BULK_EMAIL_SBJCT_VC"
                        TextMode="SingleLine" iCase="None" ReadOnly="false" Width="300px">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false"
                            ValidationGroup="" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            RegularExpression="" IsRequired="true" CustomValidation="False" RegexValidation="true"
                            RangeValidation="False" RegErrorMessage="" />
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
                    <Inp:iTextBox ID="txtBody" runat="server" CssClass="txt" TabIndex="4" HelpText="177,BULK_EMAIL_BDY_VC"
                        TextMode="MultiLine" iCase="None" ReadOnly="false" Width="300px" Height="70px">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false"
                            ValidationGroup="" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            RegularExpression="" IsRequired="true" CustomValidation="False" RegexValidation="true"
                            RangeValidation="False" RegErrorMessage="" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <%--  <td>
                </td>--%>
                <td colspan="2">
                </td>
                <td colspan="2">
                    <div class="button" style="width: 200px">
                        <ul>
                            <li><a data-corner="8px" class="btncorner" id="btnSend" href="#" onmouseover="this.className='sbtn'"
                                            onmouseout="this.className='btn'" onclick="sendEmail();return false;">
                                Send</a></li>
                            <li><a data-corner="8px" class="btncorner" id="btnCancel" href="#" onclick="cancelEmail();return false;">
                                Cancel</a></li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
