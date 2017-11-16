<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="script/Callback.js" type="text/javascript"></script>
    <script src="script/Common.js" type="text/javascript"></script>
    <script src="Script/UI/Settings.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowLogin(_var_session) {
            var dt1 = new Date()
            if (DisplayMode == "Fixed") {
                document.location.href = "Login.aspx?wmode=on&se=" + _var_session, "&scr=" + dt1.getTime();
                    setTimeout('CloseWindow();', 10000);
            }
            else {
                document.location.href = "Login.aspx?wmode=on&se=" + _var_session, "&scr=" + dt1.getTime();
            }
        }
        function ShowHome(_var_session) {
            var dt1 = new Date()
            if (DisplayMode == "Fixed") {
                strProperties = 'width=1050,height=710,top=0,left=0,toolbars=no,scrollbars=no,status=no,resizable=yes,minimize=yes,maximize=yes'
                HomeWindow = window.open("Home.aspx?wmode=on&se=" + _var_session, "scr" + dt1.getTime(), strProperties);

                if (HomeWindow) {
                    setTimeout('CloseWindow();', 10000);
                }
            }
            else {
                document.location.href = "Login.aspx?wmode=on&se=" + _var_session, "&scr=" + dt1.getTime();
            }
        }
        function CloseWindow() {
            window.opener = this;
            window.open('close.htm', '_self');
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div style="vertical-align: middle; height: 600px; width: 400px;">
            <table style="height: 100px; width: 100%;">
                <tr valign="middle">
                    <td>
                        <div class="productlogo">
                        </div>
                    </td>
                    <td class="hline">
                    </td>
                    <td height="100px" align="center" valign="middle">
                        <div id="divBrowserCapability" runat="server" style="display: none; width: 300px;">
                            <asp:HyperLink ID="hlBrowserCompatible" runat="server" Target="_blank" Visible="False"
                                NavigateUrl="http://www.microsoft.com/windows/ie/default.asp">Microsoft Internet Explorer 7.0 or Above Required Click here to Download.</asp:HyperLink>
                            <asp:HiddenField ID="hdnScreenSize" runat="server" />
                        </div>
                        <div id="downloadShortCut" runat="server" style="display: none; width: 300px;" >
                            <span class="ita">The session is already in use. Please open the application in New Session.For more information, Please contact administrator.</span>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </center>
    <script language="javascript" type="text/javascript">
        document.getElementById("hdnScreenSize").value = document.body.clientWidth + "x" + document.body.clientHeight;
        var myWidth = 0, myHeight = 0;
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
