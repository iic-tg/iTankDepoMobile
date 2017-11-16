<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>iDepo</title>
</head>
<body onload="setFocus();" class="loginbody">
    <form id="form1" runat="server" onsubmit="encrptPassword();" autocomplete="off">
    <table id="tbllogin" align="center">
        <tr style="height: 97%" valign="middle" align="center">
            <td align="left">
                <div id="loginpane" class="loginpane" runat="server" align="center" style ="width :305px">
                    <table align="center" runat="server"  id="tbLogin" runat="server" class="tblstd" style="width: 100%">
                        <tr>
                            <td colspan="3" align="center">
                                <div class="ylbl" style="cursor: auto; width: 100%">
                                    <span style="width: 100%; font-size: small; font-style: italic">Sign In</span>
                                </div>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" CssClass="wlbl" runat="server" Text="User Name"></asp:Label>
                            </td>
                            <td valign="middle">
                                <Inp:iTextBox ID="txtUserName" runat="server" CssClass="txt" MaxLength="20" TabIndex="1"
                                    iCase="Upper" HelpText="" Width="210px">
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" CssClass="wlbl" runat="server" Text="Password"></asp:Label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtPassword" runat="server" CssClass="txt" MaxLength="20" TabIndex="2"
                                    HelpText="" TextMode="Password" Width="210px" autocomplete="off">
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td valign="middle" colspan="2" style="width: 130px">
                                            <a href="#" class="wlbl" onclick="openForgotPassword();" tabindex="-1">Forgot Password</a>
                                        </td>
                                        <td style="width: 170px" align="right">
                                            <button id="btnSubmit" runat="server" class="btn-corner btn-success" style="border: 0px">
                                                <i class="icon-signin"></i>&nbsp;Sign In
                                            </button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" style="height: 35px">
                                <table id="errcontent" runat="server" style="display: none">
                                    <tr>
                                        <td valign="middle">
                                            <img src="Images/mws.png" />
                                        </td>
                                        <td valign="top">
                                            <span id="errLabel" name="errLabel" class="ylbl" style="width: 250px" runat="server">
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table id="tblVersion" class="tblstd" style="width: 100%">
                                    <tr>
                                        <td style="width:60%;">
                                            &nbsp;
                                        </td>
                                        <td align="right" style="width:30%;">
                                            <asp:Label ID="lblVersion" CssClass="wlbl" runat="server" Text="Version : "></asp:Label>
                                        </td>
                                        <td style="width:10%;">
                                            <asp:Label ID="lblVersionNo" CssClass="wlbl" runat="server" Text="1.0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="licensePane" class="loginpane" runat="server">
                    <table id="tblLicense" runat="server" class="tblstd" style="width: 100%">
                        <tr>
                            <td colspan="3" align="center">
                                <div class="ylbl" style="cursor: auto; width: 100%">
                                    <span id="spnError" style="width: 100%; font-size: small; font-style: italic" runat="server">
                                        Invalid License</span>
                                </div>
                                <br />
                                <div class="wlbl" style="cursor: auto; width: 100%">
                                    <span id="spnErrorMessage" style="width: 100%; font-size: small;" runat="server">Please
                                        Contact System Administrator</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td valign="middle">
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
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td valign="middle" colspan="2" style="width: 130px">
                                        </td>
                                        <td style="width: 170px" align="right">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" style="height: 35px">
                                <table id="Table2" runat="server" style="display: none">
                                    <tr>
                                        <td valign="middle">
                                        </td>
                                        <td valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr style="height: 3%; width: 100%">
            <td class="footerMenu" align="center" valign="baseline" style="width: 100%;">
                Depot Management Tool powered by <b><i><a style="font-size: 8pt" href="http://www.iinterchange.com"
                    target="_blank">iInterchange Systems Pvt. Ltd</a></i></b>.
            </td>
        </tr>
    </table>

    <asp:HiddenField ID="hdnPd" runat="server" />
    <asp:HiddenField ID="hdnScreenSize" runat="server" />
    <script type="text/javascript">
        var myWidth = 0, myHeight = 0;
        if (typeof (window.innerWidth) == 'number') {
            //Non-IE
            myWidth = window.innerWidth;
            myHeight = window.innerHeight;
        } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
            //IE 6+ in 'standards compliant mode'
            myWidth = document.documentElement.clientWidth;
            myHeight = document.documentElement.clientHeight;
        } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
            //IE 4 compatible
            myWidth = document.body.clientWidth;
            myHeight = document.body.clientHeight;
        }

        document.getElementById("hdnScreenSize").value = myWidth + "x" + myHeight;


        


    </script>
    </form>
</body>
</html>
