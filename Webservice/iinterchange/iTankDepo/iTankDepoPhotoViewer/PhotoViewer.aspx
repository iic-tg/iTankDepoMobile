<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PhotoViewer.aspx.vb" Inherits="Operations_PhotoViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div id="divPhotos" runat="server">
    <table class="tblstd" cellspacing="0" cellpadding="0" align="center" border="0">
            <tr>
                <td valign="top">
                    <table class="tblstd" cellspacing="0" cellpadding="3" align="center" border="0">
                        <tr>
                            <td id="tdEstimationNo" class="headertext" style="height: 20px;width:100%" colspan="2" runat="server">
                                Estimantion No:
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
                                    <table border="2" cellpadding="2" width="500px">
                                        <tr>
                                            <td colspan="2" align="center">                                            
                                                <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"ATTCHMNT_PTH") %>'
                                                    Height="200px" Width="200px" AlternateText="No Image" BorderColor="ActiveBorder"
                                                    BorderStyle="Groove" />                                                
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td class="labeltext">
                                                            File Name
                                                        </td>
                                                        <td class="labeltext">
                                                            :
                                                        </td>
                                                        <td>
                                                          <a target="_blank" href='<%# DataBinder.Eval(Container.DataItem,"ATTCHMNT_PTH") %>'>  <%#DataBinder.Eval(Container.DataItem, "ACTL_FL_NM")%></a>
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
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnfirst" runat="server" Font-Bold="true" Text="<<" Height="25px"
                                                Width="25px" OnClick="btnfirst_Click" />
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
                                                Width="25px" OnClick="btnlast_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    </div>
     <div id="divnoRecords" runat="server" visible="false" align="center">
        <label class="errortext">
            <i>No Photos Found</i>
        </label>
    </div>
    </form>
</body>
</html>
