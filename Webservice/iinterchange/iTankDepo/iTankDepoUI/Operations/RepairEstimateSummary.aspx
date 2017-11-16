<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RepairEstimateSummary.aspx.vb"
    Inherits="Operations_RepairEstimateSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
        <form id="form1" runat="server" autocomplete="off" style="height: 100%; overflow :auto ">
    <div id="divRepairEstimateSummary" style="margin: 1px; width: 560px; height: 100px;">
    <br />
        <table border="0" cellpadding="0" cellspacing="0" class="tblstd" style="margin:0px auto;">
            <tr>
                <td>
                    <Inp:iLabel ID="ILabel24" runat="server" CssClass="ublbl">Summary</Inp:iLabel>
                </td>
                <td rowspan="6" valign="top">
                    <iFg:iFlexGrid ID="ifgSummaryDetail" runat="server" AllowStaticHeader="False" DataKeyNames="SMMRY_ID"
                        Width="400px" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        Scrollbars="None" ShowEmptyPager="False" Type="Normal" ValidationGroup="" UseCachedDataSource="True"
                        AutoGenerateColumns="False" EnableViewState="False" OnBeforeClientRowCreated=""
                        OnAfterClientRowCreated="" PageSize="25" AddRowsonCurrentPage="False" ShowPageSizer="False"
                        AllowPaging="False" AllowAdd="False" AllowDelete="False" ShowFooter="False" OnAfterCallBack="ifgSummaryDetailOnAfterCallBack"
                        AllowEdit="False" AllowExport="True" AllowFilter="False">
                        <PagerStyle CssClass="gpage" />
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <Columns>
                            <iFg:BoundField DataField="MN_HR_SMMRY" HeaderText="MH" HeaderTitle="" IsEditable="False"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:BoundField>
                            <iFg:BoundField DataField="LBR_RT_SMMRY" HeaderText="MHR" HeaderTitle="" IsEditable="False"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False" Visible="false">
                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:BoundField>
                            <iFg:BoundField DataField="MN_HR_RT_SMMRY" HeaderText="MHC" HeaderTitle="" IsEditable="False"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:BoundField>
                            <iFg:BoundField DataField="MTRL_CST_SMMRY" HeaderText="MC" HeaderTitle="" IsEditable="False"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:BoundField>
                            <iFg:BoundField DataField="TTL_CST_SMMRY" HeaderText="TC" HeaderTitle="" IsEditable="False"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:BoundField>
                        </Columns>
                        <SelectedRowStyle CssClass="gsitem" />
                        <AlternatingRowStyle CssClass="gaitem" />
                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                    </iFg:iFlexGrid>
                </td>
                <td rowspan="6" valign="top">
                </td>
            </tr>
            <tr>
                <td class="pgHdr">
                    Customer
                </td>
            </tr>
            <tr>
                <td class="pgHdr">
                    Invoicing Party
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
