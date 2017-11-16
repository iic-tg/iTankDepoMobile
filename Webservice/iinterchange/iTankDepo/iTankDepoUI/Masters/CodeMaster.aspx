<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CodeMaster.aspx.vb" Inherits="Masters_CodeMaster" %>

<%@ Register Assembly="iInterchange.iTankDepo.Business.Common" Namespace="iInterchange.iTankDepo.Business.Common"
    TagPrefix="iExp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="height: 560px">
    <!-- UIG Fix -->
    <div>
        <div id="divhdr" runat="server">
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh"></span>
                    </td>
                    <td align="right">
                        <nv:Navigation ID="navCodeMaster" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tabdisplayGatePass" id="tabCodeMaster">
            <div class="topspace">
            </div>
            <iFg:iFlexGrid ID="ifgCodeMaster" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                CellPadding="2" CssClass="tblstd" PageSize="25" AutoSearch="true" StaticHeaderHeight="500px"
                ShowEmptyPager="True" Width="100%" AllowEdit="True" AllowAdd="True" UseCachedDataSource="True"
                AllowStaticHeader="True" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                HeaderRows="1" EnableViewState="False" SortAscImageUrl="" SortDescImageUrl=""
                AddButtonText="Add" DeleteButtonText="Delete" GridLines="Both" HorizontalAlign="NotSet"
                PageSizerFormat="" Scrollbars="None" Type="Normal" ValidationGroup="tabCodeMaster"
                DataKeyNames="ID" OnAfterCallBack="onAfterCB" OnBeforeCallBack="" OnAfterClientRowCreated="setDefaultValues"
                AddRowsonCurrentPage="False" AllowExport="False" AllowPaging="True" AllowSorting="True"
                AllowSearch="True" AllowRefresh="True" UseIcons="true" SearchButtonIconClass="icon-search"
                SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                ClearButtonCssClass="btn btn-small btn-info" ClearButtonIconClass="icon-eraser" UseActivitySpecificDatasource="True">
                <Columns>
                    <iFg:TextboxField DataField="Code" HeaderText="Code" HeaderTitle="Code" SortExpression="Code"
                        SortAscUrl="" SortDescUrl="">
                        <TextBox HelpText="14" iCase="Upper" OnClientTextChange="" ValidationGroup="" CssClass="txt"
                            MaxLength="10">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                                CustomValidation="true" CsvErrorMessage="This Code Already Exists" CustomValidationFunction="validateCode"
                                IsRequired="true" ReqErrorMessage="Code Required" RegularExpression="^[a-zA-Z0-9]+$"
                                RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                        </TextBox>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" />
                    </iFg:TextboxField>
                    <iFg:TextboxField DataField="Description" HeaderText="Description" SortExpression="Description"
                        HeaderTitle="Description" SortAscUrl="" SortDescUrl="">
                        <TextBox HelpText="15" iCase="None" OnClientTextChange="" ValidationGroup="" CssClass="txt"
                            MaxLength="255">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                                IsRequired="true" ReqErrorMessage="Description Required" RegexValidation="true"
                                RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are allowed." />
                        </TextBox>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="780px" />
                    </iFg:TextboxField>
                    <iFg:CheckBoxField DataField="Active" HeaderText="Active" HeaderTitle="Active" HelpText=""
                        SortAscUrl="" SortDescUrl="" SortExpression="">
                        <ItemStyle Width="25px" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </iFg:CheckBoxField>
                </Columns>
                <RowStyle CssClass="gitem" />
                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                <PagerStyle CssClass="gpage" Height="18px" Font-Names="Verdana" HorizontalAlign="Center" />
                <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                <SelectedRowStyle CssClass="gsitem" />
                <AlternatingRowStyle CssClass="gaitem" />
                <ActionButtons>
                    <iFg:ActionButton ID="act_Upload" Text="Upload" ValidateRowSelection="False" OnClientClick="openUpload();"
                        IconClass="icon-upload-alt" CSSClass="btn btn-small btn-info" />
                    <iFg:ActionButton ID="aBtExport" Text="Export" ValidateRowSelection="False" OnClientClick="exportToExcel();"
                        IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                </ActionButtons>
            </iFg:iFlexGrid>
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();" onClientPrint="null" />
    <asp:HiddenField ID="hdnPageTitle" runat="server" />
    </form>
</body>
</html>
