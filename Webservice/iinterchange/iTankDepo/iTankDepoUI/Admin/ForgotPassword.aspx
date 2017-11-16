<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForgotPassword.aspx.vb"
    Inherits="ForgotPassword" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
</head>
<body onload="setFocus();">
    <form id="form2" runat="server" autocomplete="off" style="overflow:auto">
    <table border="0" cellpadding="2" cellspacing="2" class="tblstd" width="300px" align="center">
        <tr>
            <td>
                <label id="Label1" runat="server" class="lbl">
                    *</label>
            </td>
            <td>
                <label id="ILabel1" class="lbl" runat="server">
                    User Name</label>
            </td>
            <td>
                <Inp:iTextBox ID="txtUserID" runat="server" CssClass="txt" MaxLength="20" TabIndex="1"
                    iCase="Upper">
                    <Validator CsvErrorMessage="User Name does not exists" CustomValidateEmptyText="True"
                        CustomValidation="True" CustomValidationFunction="validateUserName" IsRequired="False"
                        LookupValidation="False" Operator="Equal" RegErrorMessage="Only Alphabets And Numbers are Allowed"
                        RegularExpression="^[a-zA-Z0-9]+$" ReqErrorMessage="" Type="String"
                        Validate="True" ValidationGroup="tabUser" />
                </Inp:iTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <label id="Label2" runat="server" class="lbl">
                    *</label>
            </td>
            <td>
                <label id="lblEmailId" class="lbl" runat="server">
                    Email ID</label>
            </td>
            <td>
                <Inp:iTextBox ID="txtEmailId" runat="server" CssClass="txt" MaxLength="50" TabIndex="2">
                    <Validator CsvErrorMessage="Email ID does not exists" CustomValidateEmptyText="True"
                        CustomValidation="True" CustomValidationFunction="validateEmailId" IsRequired="False"
                        LookupValidation="False" Operator="Equal" RegErrorMessage="Email ID Invalid"
                        RegexValidation="False" RegularExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                        ReqErrorMessage="Email ID Required" Type="String" Validate="True" ValidationGroup="tabUser" />
                </Inp:iTextBox>
            </td>
        </tr>
    </table>
    <table class="tblstd" align="center">
        <tr>
            <td>
            </td>
            <td>
                <div class="button">
                    <ul>
                        <li class="btncorner" data-corner="8px">
                            <input type="button" tabindex="3" id="btnSubmit" onclick="sendPassword();" class="btn"
                                onmouseover="this.className='sbtn'" onmouseout="this.className='btn'" value="Submit" />
                        </li>
                        <li class="btncorner" data-corner="8px">
                            <input type="button" tabindex="4" id="btnClose" onclick="window.close();" class="btn"
                                onmouseover="this.className='sbtn'" onmouseout="this.className='btn'" value="Close" />
                        </li>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    <table class="tblstd" width="100px" align="left">
        <tr id="trStatusBarPane" valign="bottom">
            <td colspan="2" id="tdStatusBarPane" valign="top" style="height: 30px; width: 100px"
                align="left">
                <table style="width: 200px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 50%" valign="top">
                            <div id="message" class="message">
                                <b><span id="messagetitle" class="messagetitle">ERROR:</span></b><br />
                                <div id="messagecontent" class="messagecontent">
                                    error
                                </div>
                            </div>
                        </td>
                        <td style="width: 50%" valign="top">
                            <div id="helptip" class="helptip">
                                <b><span id="helptiptitle" class="helptiptitle">HELP TIP:</span></b><br />
                                <div id="helptipcontent" class="helptipcontent">
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnPwd" runat="server" />
    <asp:HiddenField ID="hdnHashPwd" runat="server" />
    </form>
</body>
</html>
