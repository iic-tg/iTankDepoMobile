<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Widgets.aspx.vb" Inherits="Widgets_Widgets" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function toggleStyle(obj, c) {
            obj.className = c;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="WidgetList" runat="server">
            <HeaderTemplate>
                <div class="widgets">
                    <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div id="div<%#Eval("WDGT_NAM")%>" class="widgetdisplay">
                        <table id="tblWidgets<%#Eval("WDGT_NAM")%>" align="center" cellpadding="0" cellspacing="0"
                            style="width: 100%;">
                            <tr class="ctabh" style="width: 100%; height: 20px;">
                                <td align="left">
                                    <span id="spnWidgets<%#Eval("WDGT_NAM")%>">&nbsp;&nbsp;<%#Eval("WDGT_TTL")%></span>
                                </td>
                                <td align="right">
                                   <%-- <a class="refresh" onmouseover="toggleStyle(this,'refresho');" onmouseout="toggleStyle(this,'refresh');"
                                        href="#" title="Refresh" onclick="document.getElementById('frmWidgets<%#Eval("WDGT_NAM")%>').src = document.getElementById('frmWidgets<%#Eval("WDGT_NAM")%>').src+'?scr=<%Format(Now, "yyyyMMddHHmmssffff") %>';">
                                    </a>--%>
                                    <div class="sicolnk"  title="Refresh" style=" margin-right:2px;"
                                            onmouseover="toggleStyle(this,'sicolnko');" onmouseout="toggleStyle(this,'sicolnk');"
                                            onclick="document.getElementById('frmWidgets<%#Eval("WDGT_NAM")%>').src = document.getElementById('frmWidgets<%#Eval("WDGT_NAM")%>').src+'?scr=<%Format(Now, "yyyyMMddHHmmssffff") %>';">
                                   <i class="icon-refresh"></i>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <iframe frameborder='0' id='frmWidgets<%#Eval("WDGT_NAM")%>' name='frmWidgest<%#Eval("WDGT_NAM")%>'
                                        width='450px' height='260px' src="<%#Eval("WDGT_URL")%>?scr=<%Format(Now, "yyyyMMddHHmmssffff") %>">
                                    </iframe>
                                </td>
                            </tr>
                        </table>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul> </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
