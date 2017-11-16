<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Navigation.ascx.vb" Inherits="UserControls_Navigation" %>
<div id="divNav" style="width: 250px;" align="right" class="protected" runat="server">
    <div class="navigation" id="pnlNavigation" runat="server" height="100%" width="100%">
        <table cellspacing="1px" cellpadding="1px" border="0" class="tblstd" width="60%">
            <tr>
              <%--  <td>
                <div id="divDepotCode" runat="server" style="vertical-align:middle">
                    <Inp:iLabel ID="lblDepotCode" runat="server" CssClass="olbl" Font-Bold="true" Font-Names="Arial" Font-Size="8pt" ForeColor="#ffc000">Depot Code : 
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblUserDepotCode" runat="server" CssClass="olbl" Font-Bold="true" Font-Names="Arial" Font-Size="8pt" ForeColor="#ffc000"></Inp:iLabel>
                </div>
                </td>--%>
                <td>
                    <div id="lnkFavourites" onclick="toggleFavourite();return false;" class="sicolnk" onmouseover="toggleStyle(this,'sicolnko');"
                        onmouseout="toggleStyle(this,'sicolnk');"  style="text-decoration: none;  font-style:normal; color:#fbf4a0;"
                        title="Set as Favorites"><i id="iconFav" class="icon-star-empty"></i></div>
                </td>
                <td>
                   <div id="lnkHelp" onclick="toggleHelpTip();return false;" class="sicolnk" onmouseover="toggleStyle(this,'sicolnko');"
                        onmouseout="toggleStyle(this,'sicolnk');"  style="text-decoration: none;  font-style:normal;"
                        title="Help"><i class="icon-question-sign"></i></div>
                </td>
                <td>
                   <div id="lstBack" onclick="backToListPage();return false;" class="sicolnk" onmouseover="toggleStyle(this,'sicolnko');"
                        onmouseout="toggleStyle(this,'sicolnk');"  style="text-decoration: none;"
                        title="Back to List"><i class="icon-arrow-left"></i></div>
                </td>
                <td>
                   <div class="sicolnk" onmouseover="toggleStyle(this,'sicolnko');" onmouseout="toggleStyle(this,'sicolnk');"
                        id="lstFirst" onclick="firstRecord();return false;"  style="text-decoration: none;"
                        title="First"><i class="icon-fast-backward"></i></div>
                </td>
                <td>
                   <div id="lstPrev" onclick="prevRecord();return false;" class="sicolnk" onmouseover="toggleStyle(this,'sicolnko');"
                        onmouseout="toggleStyle(this,'sicolnk');"  style="text-decoration: none;"
                        title="Previous"><i class="icon-step-backward"></i></div>
                </td>
                <td>
                   <div id="lstNext" onclick="nextRecord();return false;" class="sicolnk" onmouseover="toggleStyle(this,'sicolnko');"
                        onmouseout="toggleStyle(this,'sicolnk');"  style="text-decoration: none;"
                        title="Next"><i class="icon-step-forward"></i></div>
                </td>
                <td>
                   <div id="lstLast" onclick="lastRecord();return false;" class="sicolnk" onmouseover="toggleStyle(this,'sicolnko');"
                        onmouseout="toggleStyle(this,'sicolnk');"  style="text-decoration: none;"
                        title="Last"><i class="icon-fast-forward"></i></div>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
</div>