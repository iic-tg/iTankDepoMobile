<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewXmlDetail.aspx.vb" Inherits="Billing_ViewXmlDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div class="" id="tabViewXml" style="overflow-y: auto; overflow-x: auto; height: auto">
        <table style="width: 100%;">
            <tr>
                <td>
                    <Inp:iLabel ID="lblHeaderDetail" runat="server" CssClass="blbl" Font-Underline="True">Header Details
                    </Inp:iLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divViewXmlDetail" style="display: block;">
                        <iFg:iFlexGrid ID="ifgViewEdi" runat="server" AllowStaticHeader="True" DataKeyNames="INVC_EDI_HSTRY_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="Auto" ShowEmptyPager="false"  StaticHeaderHeight="40px" Type="Normal"
                            ValidationGroup="divXml" UseCachedDataSource="True" AutoGenerateColumns="False" ShowFooter="false"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="false"  OnAfterCallBack=""
                            AllowDelete="False" AllowRefresh="false" AllowSearch="false" AutoSearch="True"
                            UseIcons="false" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
                            AllowAdd="False" UseActivitySpecificDatasource="True">
                            <%--<PagerStyle CssClass="gpage" />--%>
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="CSTMR_CD" HeaderText="Customer" HeaderTitle="Customer"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="30px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="ACTVTY_NAM" HeaderText="Activity" HeaderTitle="Activity"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="30px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="INVC_NO" HeaderText="Invoice No" HeaderTitle="Invoice No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="30px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="SNT_FL_NAM" HeaderText="Sent File Name" HeaderTitle="Sent File Name"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:BoundField>
                            </Columns>
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                        </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lblEdiDetail" runat="server" CssClass="blbl" Font-Underline="True">Line Details
                    </Inp:iLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divViewXml" style="display: block;Width:100%">
                        <iFg:iFlexGrid ID="ifgViewXmlEDI" runat="server" AllowStaticHeader="True" DataKeyNames="INVC_EDI_HSTRY_DTL_ID"
                             CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify" Width="100%"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="200px" Type="Normal"
                            ValidationGroup="divViewXml" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="10" AddRowsonCurrentPage="False" AllowPaging="True" OnAfterCallBack="fnGridOnafterCB"
                            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
                            AllowAdd="False" UseActivitySpecificDatasource="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="10px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" Width ="20px"/>
                                </iFg:BoundField>
                                <iFg:BoundField DataField="LN_RSPNS_VC" HeaderText="Line Response" HeaderTitle="Line Response"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="180px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" Width ="200px" />
                                </iFg:BoundField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                        </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnEDIId" runat="server" />
    </form>
</body>
</html>
