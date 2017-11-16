<%@ Page Language="VB" AutoEventWireup="true" CodeFile="PhotoViewer.aspx.vb" Inherits="Operations_PhotoViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divPhotos" runat="server">
        <table class="tblstd" cellspacing="0" cellpadding="0" align="left" border="0">
            <tr>
                <td valign="top">
                    <table class="tblstd" cellspacing="0" cellpadding="3" align="center" border="0" width="100%">
                        <tr>
                            <td id="tdEstimationNo" class="headertext" style="height: 20px; width: 100%" colspan="2"
                                runat="server">
                                Estimantion No:
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100%;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">
                                            <asp:Label ID="lblFilter" runat="server" Text="Activity Name: "></asp:Label>
                                        </td>
                                        <td >
                                            <asp:DropDownList ID="ddlActivityName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlActivityName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataList ID="dlPhotos" runat="server">
                                    <AlternatingItemStyle BackColor="White" />
                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                    <ItemTemplate>
                                        <table border="2" cellpadding="2" width="800px">
                                            <tr>
                                                <td colspan="2" align="center" width="250px">
                                                    <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"IMG_ATTCHMNT_PTH") %>'
                                                        Height="200px" Width="250px" AlternateText="No Image" BorderColor="ActiveBorder"
                                                        BorderStyle="Groove" />
                                                </td>
                                                <td width="550px">
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="labeltext" style="width: 20%;">
                                                                File Name
                                                            </td>
                                                            <td class="labeltext" style="width: 1%;">
                                                                :
                                                            </td>
                                                            <td style="width: 79%;">
                                                                <a target="_blank" href='<%# DataBinder.Eval(Container.DataItem,"ATTCHMNT_PTH") %>'>
                                                                    <%#DataBinder.Eval(Container.DataItem, "ACTL_FL_NM")%></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="labeltext">
                                                                Activity Name
                                                            </td>
                                                            <td class="labeltext">
                                                                :
                                                            </td>
                                                            <td class="normalblacktext">
                                                                <%#DataBinder.Eval(Container.DataItem, "ACTVTY_NAM")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr colspan="2">
                            <td align="center">
                                <div id="divFileNavigation" runat="server" >
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnfirst" runat="server" Font-Bold="true" Text="<<" Height="25px"
                                                    Width="25px" OnClick="btnfirst_Click" Visible="true" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnprevious" runat="server" Font-Bold="true" Text="<" Height="25px"
                                                    Width="25px" OnClick="btnprevious_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnnext" runat="server" Font-Bold="true" Text=">" Height="25px" Width="25px"
                                                    OnClick="btnnext_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnlast" runat="server" Font-Bold="true" Text=">>" Height="25px"
                                                    Width="25px" OnClick="btnlast_Click" Visible="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="float: right; vertical-align: top; margin-top: -30px;">
                                    <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divnoRecords" runat="server" visible="false" align="center" class="errortext"
        style="font-style: italic;">
        No Files Found
        <%--<label class="errortext">--%>
        <%--<i>No Photos Found</i>--%>
        <%--</label>--%>
    </div>
    </form>
</body>
</html>
