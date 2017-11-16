<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PaneHeader.ascx.vb" Inherits="UserControls_PaneHeader" %>
<table cellpadding="0" cellspacing="0" class="headerPane" style="width:100%">
    <tr>
        <td id="tdTitle" runat="server" style="height: 18px; width: 97%" valign="top">
            <table id="hdrPaneTitle" name="hdrPaneTitle" runat="server" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="blbl">
                        <asp:Label ID="lblTitle" runat="server" CssClass="blbl" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td align="right" style="height: 18px; width: 3%">

        <a id="dockListPane" href="#" class="res" title="Restore" onclick="fitWindow(this);return false;">
                                </a>

        </td>
    </tr>
</table>
