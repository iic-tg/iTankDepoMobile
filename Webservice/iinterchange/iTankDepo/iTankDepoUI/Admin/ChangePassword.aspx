<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb"
    Inherits="Admin_ChangePassword" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>        
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="overflow:auto">
    <table cellpadding="0" cellspacing="0" style="vertical-align:middle;width:100%">        
        <tr>
            <td>
            <div id="tabChangePwd" >
        <table border="0" cellpadding="2" cellspacing="2" align="center" class="tblstd" width="300px">
            <tr>
                <td style="width: 190px; height: 22px; vertical-align:top;">
                    <label id="lblUserName" runat="server" cssclass="lbl">
                        Old Password</label>
                </td>
                <td style="height: 22px">
                    <Inp:iTextBox ID="txtOldPassword" runat="server" CssClass="txt" MaxLength="20" TabIndex="1"
                        TextMode="Password" HelpText="12">
                        <Validator ControlToCompare="" CustomValidateEmptyText="False" IsRequired="True"
                            LookupValidation="False" Operator="Equal" ReqErrorMessage="Old Password Required"
                            Type="String" Validate="True" ValidationGroup="tabUserChangePwd" CsvErrorMessage="Invalid Old Password"
                            CustomValidation="True" CustomValidationFunction="validatePassword" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 190px; height: 22px; vertical-align:top;">
                    <label id="ILabel1" runat="server" cssclass="lbl">
                        New Password</label>
                </td>
                <td style="height: 22px">
                    <Inp:iTextBox ID="txtNewPassword" runat="server" CssClass="txt" MaxLength="20" TabIndex="2"
                        TextMode="Password" HelpText="2">
                        <Validator ControlToCompare="txtOldPassword" CustomValidateEmptyText="False" IsRequired="True"
                            LookupValidation="False" Operator="NotEqual" ReqErrorMessage="New Password Required"
                            Type="String" Validate="True" ValidationGroup="tabUserChangePwd" RegErrorMessage="New Password must be atleast 7 chars with atleast one numeric, one capital letter and one symbol."
                            RegexValidation="True" 
                            RegularExpression="^(?=.{7,20}$)(?=.*[\d])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%\-\^&amp;*()_<+=\|{}])[\w\d!@#$%^\-\&amp;*()_<+=\|{}].*" 
                            CmpErrorMessage="The New password cannot be same as Old password." 
                            CompareValidation="True" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 190px; vertical-align:top;">
                    <label id="ILabel2" runat="server" cssclass="lbl">
                        Confirm Password</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtConfirmPassword" runat="server" CssClass="txt" MaxLength="20"
                        TabIndex="3" TextMode="Password" HelpText="3">
                        <Validator CmpErrorMessage="Confirm Password does not match,&lt;br&gt; please re-enter your passwords."
                            CompareValidation="True" ControlToCompare="txtNewPassword" CustomValidateEmptyText="True"
                            Operator="Equal" ReqErrorMessage="Confirm Password Required" Type="String" Validate="True"
                            ValidationGroup="tabUserChangePwd" IsRequired="True" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
            </td>
        </tr>
            <tr>
                <td style="height: 17px" >
                    <table cellpadding="0" cellspacing="0" style="widht:100%" align="center">
                        <tr>
                            <td>
                                <div class="button" style="width: 130px" runat="server">
                                    <ul>
                                        <li class="btncorner" data-corner="6px">
                                            <button tabindex="4" id="btnSubmit" class="btn btn-small btn-success" style="border: 0px;" onclick="changePassword();return false;">
                                                <i class="icon-save"></i>
                                                Submit</button>
                                        </li>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>           

    </table>    
    <asp:HiddenField ID="hdnNpd" runat="server" />
    <asp:HiddenField ID="hdnOpd" runat="server" />
    </form>
</body>
<script type="text/javascript">
    $('.btncorner').corner();
    checkPageLocation();
    initPage();
</script>
</html>
