<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewEDI.aspx.vb" Inherits="Operations_ViewEDI" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>iDepo</title>
</head>
<body>
        <form id="form1" runat="server" autocomplete="off" style="height: 100%; overflow :auto ">
    <div>
             <div style ="width :995px">
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">View EDI</span>
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
                 </div> 
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd">
            <tr>
                <td style="height: 100%; width: 100%">
                    <div id="divDetail" style="margin: 1px; width: 100%; vertical-align: middle; height: 18px;">
                        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                            font-family: Arial; font-size: 8pt; display: none; width: 100%;" align="center">
                            <div>
                                No Records Found.</div>
                        </div>
                        <div id="divEDIDetail" style="margin: 1px; width: 980px; height: 400px; vertical-align: middle;">
                            <iFg:iFlexGrid ID="ifgEDIDetail" runat="server" AllowStaticHeader="True" DataKeyNames=""
                                Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="380px" Type="Normal"
                                ValidationGroup="divDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                                EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                                PageSize="25" AddRowsonCurrentPage="False" AllowAdd="False" AllowDelete="False" AutoSearch ="True"
                                ShowFooter="True" OnAfterCallBack="" AllowPaging="True" AllowSearch="True" AllowRefresh="True" HeaderStyle-VerticalAlign="NotSet"  AllowSorting="True">
                                <PagerStyle CssClass="gpage" />
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <Columns>
                                    <iFg:BoundField DataField="Customer" HeaderText="Customer" HeaderTitle="Customer"
                                        IsEditable="False" ReadOnly="True" SortAscUrl="" SortDescUrl="">
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="EDItype" HeaderText="EDI Format" HeaderTitle="EDI Format" IsEditable="False"
                                        ReadOnly="True" SortAscUrl="" SortDescUrl="">
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="GeneratedDate" HeaderText="Generated Date" HeaderTitle="Generated Date"
                                        IsEditable="False" ReadOnly="True" SortAscUrl="" SortDescUrl="">
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="GeneratedTime" HeaderText="Generated Time" HeaderTitle="Generated Time"
                                        IsEditable="False" ReadOnly="True" SortAscUrl="" SortDescUrl="">
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="Activity" HeaderText="Activity" HeaderTitle="Activity"
                                        IsEditable="False" ReadOnly="True" SortAscUrl="" SortDescUrl="">
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:HyperLinkField DataTextField="FileName" HeaderText ="File Name" NavigateUrl ="#" ReadOnly="true" >
                                     <ItemStyle Width="200px" Wrap="True" />
                                    </iFg:HyperLinkField>
                                   <%-- <iFg:BoundField DataField="FileName" HeaderText="File Name" HeaderTitle="FileName"
                                        IsEditable="False" ReadOnly="True" SortAscUrl="" SortDescUrl="">
                                        <ItemStyle Width="300px" Wrap="True" />
                                    </iFg:BoundField>--%>
                                   <%-- <iFg:BoundField DataField="" HeaderText="Download" HeaderTitle="Download" IsEditable="False"
                                        ReadOnly="True" SortAscUrl="" SortDescUrl="">
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:BoundField>--%>
                                </Columns>
                                <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                                <SelectedRowStyle CssClass="gsitem" />
                                <AlternatingRowStyle CssClass="gaitem" />
                                <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                    IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            </iFg:iFlexGrid>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
      <iframe id="fmDownloadFile" runat="server" frameborder="0" marginheight="0" marginwidth="0" name="fmDownloadFile" scrolling="no" width="0px" height="0px"></iframe>
</body>
</html>
