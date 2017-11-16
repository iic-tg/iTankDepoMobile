<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BulkEmailDetailHistory.aspx.vb" Inherits="Tracking_BulkEmailDetailHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server">
    <div id="divBulkEmailDetailHistory" runat="server" style="overflow:scroll;height:300px;"  >
    
    </div>
    <div>
         <table align="center">
            <tr>
                <td align="center">
                    <div class="button">
                        <ul>
                            <li Class="btn btn-small btn-info"><a href="#" tabindex="5" id="btnSubmit" data-corner="8px" Class="icon-backward" onclick="CloseBulkEmailDetailView();return false;" style="text-decoration: none; color: White; font-weight: bold">&nbsp;Back</a></li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
