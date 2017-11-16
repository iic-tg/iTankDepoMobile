<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CleaningType.aspx.vb" Inherits="Masters_CleaningType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server"  autocomplete="off" style ="height :550px">
    <div>
        <div>
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh">CleaningType</span>
                    </td>
                    <td align="right">
                        <nv:Navigation ID="navCleaningType" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tabdisplayGatePass" id="tabCleaningType" style="overflow-y: auto; overflow-x: auto;height:auto">
            <div class="topspace">
            </div>
            <iFg:iFlexGrid ID="ifgCleaningType" runat="server" AutoGenerateColumns="False" AllowPaging="True" CaptionAlign="Left" 
            CellPadding="2" CssClass="tblstd" PageSize="25" AllowSearch="True" AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="300px" 
            ShowEmptyPager="True" Width="100%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True" 
            RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1" EnableViewState="False" AddButtonText="Add Exchange Rate" 
            DeleteButtonText="Delete" GridLines="Both" HorizontalAlign="NotSet" PageSizerFormat="CLNNG_TYP_ID" Scrollbars="None" Type="Normal" 
            ValidationGroup="tabCleaningType" OnAfterCallBack="onAfterCB" OnBeforeCallBack="" OnAfterClientRowCreated="setDefaultValues" Mode="Insert"
            DataKeyNames="CLNNG_TYP_ID" UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info" 
            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger" 
            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser">
                <Columns>
                    <iFg:TextboxField DataField="Code" HeaderText="Code *" HeaderTitle="Code" SortAscUrl="" SortDescUrl="" SortExpression="Code">
                        <TextBox ID="txtCleaningTypeCode" HelpText="216,CLEANING_TYPE_CLNNG_TYP_CD" OnClientTextChange="" ValidationGroup="" iCase="Upper" CssClass="txt">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true" IsRequired="true" ReqErrorMessage="Code Required" RegexValidation="false" CustomValidation="True" CustomValidationFunction="validateCode" RegErrorMessage="Only Alphabets is Allowed." RegularExpression="^[a-zA-Z]+$" CsvErrorMessage="This Code already Exist." />
                        </TextBox>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                    </iFg:TextboxField>
                    <iFg:TextboxField DataField="Description" HeaderText="Description *" HeaderTitle="Description" SortAscUrl="" SortDescUrl="" SortExpression="Description">
                        <TextBox ID="txtCleaningType" HelpText="320,CLEANING_TYPE_CLNNG_TYP_DSCRPTN_VC" iCase="None" OnClientTextChange="" ValidationGroup="" CssClass="txt">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true" IsRequired="true" ReqErrorMessage="Description Required" RegexValidation="true" RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are allowed." />
                        </TextBox>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="220px" HorizontalAlign="Left" />
                    </iFg:TextboxField>
                    <iFg:CheckBoxField DataField="Default" HeaderText="Default" HeaderTitle="Default" HelpText="" SortAscUrl="" SortDescUrl="" SortExpression="">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="25px" HorizontalAlign="Left" />
                    </iFg:CheckBoxField>
                    <iFg:CheckBoxField DataField="Active" HeaderText="Active" HeaderTitle="Active" HelpText="" SortAscUrl="" SortDescUrl="" SortExpression="">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="25px" />
                    </iFg:CheckBoxField>
                </Columns>
                <RowStyle CssClass="gitem" />
                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                <PagerStyle CssClass="gpage" HorizontalAlign="Center" />
                <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                <SelectedRowStyle CssClass="gsitem" />
                <AlternatingRowStyle CssClass="gaitem" />
                <ActionButtons>
                    <iFg:ActionButton ID="act_Upload" Text="Upload" ValidateRowSelection="False" OnClientClick="openUpload();" IconClass="icon-upload-alt" CSSClass="btn btn-small btn-info" />
                    <iFg:ActionButton ID="aBtExport" Text="Export" ValidateRowSelection="False" OnClientClick="exportToExcel();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                </ActionButtons>
            </iFg:iFlexGrid>
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();" onClientPrint="null" />
    <asp:HiddenField ID="hdnPageTitle" runat="server" />
    </form>
</body>
</html>
