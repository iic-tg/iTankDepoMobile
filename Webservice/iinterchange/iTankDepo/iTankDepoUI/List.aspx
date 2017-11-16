<%@ Page Language="VB" AutoEventWireup="false" CodeFile="List.aspx.vb" Inherits="List" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>iTankDepo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="tab" id="divTab" runat="server">
        <Nav:iTab ID="ITab2" runat="server" ControlHeight="" ControlWidth="" SelectedTab="TabPage1"
            TabHeight="" TabWidth="" TabsPerRow="5">
            <TabPages>
                <Nav:TabPage ID="TabPending" runat="server" Caption="Pending" doValidate="false" OnAfterSelect="bindListGrid(null,null,null,null);"
                    OnBeforeSelect="" TabPageClientId="tabListGrid">
                </Nav:TabPage>
                <Nav:TabPage ID="TabMySubmit" runat="server" Caption="My Submits" doValidate="false"
                    OnAfterSelect="bindMySubmitListGrid(null,null,null,null);" OnBeforeSelect=""
                    TabPageClientId="tabListGrid">
                </Nav:TabPage>
            </TabPages>
        </Nav:iTab>
    </div>
    <div id="tabListGrid">
       <div id="divRecordNotFound1" runat="server" style="margin: 10px; font-style: italic; font-family: Arial; font-size: 8pt; display: none; width: 100%; height: 100%;">
                    <div>
                        No Records Found. Please <a href="#" onclick="onAddClick(); return false;">click here</a> to create new record.</div>
                </div>

                  <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic; font-family: Arial; font-size: 8pt; display: none; width: 100%; height: 100%;">
                    <div>
                        No Records Found.</div>
                </div>
        <!-- UIG Fix for TFS ID :25483-->
        <div id="divListGrid" runat="server" style="margin: 1px; width: 100%;height: 100%; ">
            <iFg:iFlexGrid ID="ifgList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataKeyNames="rowindex" CaptionAlign="Left" CellPadding="2" CssClass="tblstd"
                PageSize="20" StaticHeaderHeight="380px" UseAccessibleHeader="True" ShowEmptyPager="true"
                Width="100%" AllowAdd="False" AllowDelete="False" AllowEdit="False" UseCachedDataSource="True"
                AllowSearch="True" AllowSorting="True" AllowStaticHeader="True" AutoSearch="True"
                RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
                EnableViewState="True" AllowRefresh="False" SortAscImageUrl="" SortDescImageUrl=""
                AddButtonText="Add" OnAfterCallBack="onAfterListBind" UseIcons="true" SearchButtonIconClass="icon-search"
                SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser" SearchCancelButtonCssClass="btn btn-small btn-danger">
                <RowStyle CssClass="gitem" />
                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                <PagerStyle CssClass="gpage" Height="16px" Font-Names="Arial" HorizontalAlign="Center" />
                <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                <SelectedRowStyle CssClass="gsitem" />
                <AlternatingRowStyle CssClass="gaitem" />
            </iFg:iFlexGrid>
        </div>
    </div>
    <asp:HiddenField ID="hdnActivityId" runat="server" />
    <asp:HiddenField ID="hdnRoleRights" runat="server" />
    <asp:HiddenField ID="hdnListRowData" runat="server" />
    </form>
</body>
</html>
