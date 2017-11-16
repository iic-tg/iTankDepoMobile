<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TariffCode.aspx.vb" Inherits="Masters_TariffCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Tariff</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navTariffCode" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="tabdisplayGatePass" id="tabTariffCode">
        <div class="topspace">
        </div>
        <iFg:iFlexGrid ID="ifgTariffCode" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="10" AllowSearch="True"
            AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="300px" ShowEmptyPager="True"
            Width="100%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True"
            RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
            EnableViewState="False" AddButtonText="Add Tariff" DeleteButtonText="Delete"
            GridLines="Both" HorizontalAlign="NotSet" PageSizerFormat="" Scrollbars="None"
            Type="Normal" ValidationGroup="tabTariffCode" DataKeyNames="TRFF_CD_ID" OnAfterCallBack="onAfterCB"
            OnBeforeCallBack="" OnAfterClientRowCreated="setDefaultValues" Mode="Insert"
            AddRowsonCurrentPage="False" UseIcons="true" SearchButtonIconClass="icon-search"
            SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
            AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
            DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
            RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
            ClearButtonCssClass="icon-eraser">
            <Columns>
                <iFg:TextboxField DataField="CODE" HeaderText="Code *" HeaderTitle="Code" SortAscUrl=""
                    SortDescUrl="" SortExpression="CODE" HtmlEncode="False">
                    <TextBox ID="txtTariffCode" HelpText="224,TARIFF_CODE_TRFF_CD_CD" OnClientTextChange=""
                        ValidationGroup="tabTariffCode" CssClass="txt" iCase="Upper">
                        <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage="Only Alphabets and Numbers are allowed"
                            RegexValidation="True" RegularExpression="^[a-zA-Z0-9]+$" ReqErrorMessage="Tariff Code Required"
                            CustomValidationFunction="validateCode" CustomValidation="True" Type="String"
                            Validate="true" CsvErrorMessage="This Code already Exist." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="DESCRIPTION" HeaderText="Description *" HeaderTitle="Description"
                    SortAscUrl="" SortDescUrl="" SortExpression="DESCRIPTION" HtmlEncode="False">
                    <TextBox ID="txtTariffDescription" HelpText="225,TARIFF_CODE_TRFF_CD_DESCRPTN_VC"
                        OnClientTextChange="" ValidationGroup="tabTariffCode" CssClass="txt">
                        <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="Tariff Description Required"
                            CustomValidationFunction="" CustomValidation="false" Type="String" Validate="true" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:LookupField DataField="ITEM_CODE" ForeignDataField="ITM_ID" HeaderText="Item *"
                    HeaderTitle="Item" PrimaryDataField="ITM_ID" SortAscUrl="" SortDescUrl="" SortExpression="ITM_ID">
                    <Lookup DataKey="ITM_CD" DependentChildControls="" HelpText="226,ITEM_ITM_CD" iCase="Upper"
                        OnClientTextChange="" TableName="39" ValidationGroup="" ID="lkpItem" runat="server"
                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="True"
                        SecondaryColumnName="ITM_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="ITM_ID" Hidden="true" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="ITM_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="ITM_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10"  Width="250px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Item.Click on the List for Valid Values"
                            ReqErrorMessage="Item Required" Validate="True" IsRequired="True" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="65px" />
                </iFg:LookupField>
                <iFg:LookupField DataField="SUB_ITEM_CODE" ForeignDataField="SB_ITM_ID" HeaderText="Sub Item"
                    HeaderTitle="Sub Item" PrimaryDataField="SB_ITM_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="SB_ITM_ID">
                    <Lookup DataKey="SB_ITM_CD" DependentChildControls="" HelpText="227,SUB_ITEM_SB_ITM_CD"
                        iCase="Upper" OnClientTextChange="" TableName="43" ValidationGroup="" ID="lkpSubItem"
                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="SubItemFilter"
                        AllowSecondaryColumnSearch="True" SecondaryColumnName="SB_ITM_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="SB_ITM_ID" Hidden="true" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="SB_ITM_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="SB_ITM_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10"  Width="250px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Sub Item.Click on the List for Valid Values"
                            ReqErrorMessage="Sub Item Required" Validate="True" IsRequired="False" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="65px" />
                </iFg:LookupField>
                <iFg:LookupField DataField="DAMAGE_CODE" ForeignDataField="DMG_ID" HeaderText="Damage *"
                    HeaderTitle="Damage" PrimaryDataField="DMG_ID" SortAscUrl="" SortDescUrl="" SortExpression="DMG_ID">
                    <Lookup DataKey="DMG_CD" DependentChildControls="" HelpText="228,DAMAGE_DMG_CD" iCase="Upper"
                        OnClientTextChange="" TableName="23" ValidationGroup="" ID="lkpDamage" CssClass="lkp"
                        DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="True"
                        SecondaryColumnName="DMG_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="DMG_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="DMG_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="DMG_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10"  Width="250px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Damage.Click on the List for Valid Values"
                            ReqErrorMessage="Damage Required" Validate="True" IsRequired="True" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="65px"/>
                </iFg:LookupField>
                <iFg:LookupField DataField="REPAIR_CODE" ForeignDataField="RPR_ID" HeaderText="Repair *"
                    HeaderTitle="Repair" PrimaryDataField="RPR_ID" SortAscUrl="" SortDescUrl="" SortExpression="RPR_ID">
                    <Lookup DataKey="RPR_CD" DependentChildControls="" HelpText="229,REPAIR_RPR_CD" iCase="Upper"
                        OnClientTextChange="" TableName="24" ValidationGroup="" ID="lklRepair" CssClass="lkp"
                        DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="True"
                        SecondaryColumnName="RPR_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="RPR_ID" Hidden="true" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="RPR_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="RPR_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10"  Width="250px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Repair.Click on the List for Valid Values"
                            ReqErrorMessage="Repair Required" Validate="True" IsRequired="True" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="65px" />
                </iFg:LookupField>
                <iFg:TextboxField DataField="MAN_HOURS" HeaderText="Man Hours" HeaderTitle="Man Hours"
                    SortAscUrl="" SortDescUrl="" SortExpression="MAN_HOURS" HtmlEncode="False">
                    <TextBox ID="txtManHours" HelpText="230,TARIFF_CODE_MN_HR" OnClientTextChange="FormatTwoDecimal"
                        ValidationGroup="tabTariffCode" CssClass="ntxto" iCase="Numeric">
                        <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="GreaterThanEqual"
                            ReqErrorMessage="Man Hours Required" Type="Double" CustomValidationFunction=""
                            CustomValidation="false" CsvErrorMessage="Exist" Validate="True" RegErrorMessage="Invalid Man Hours. Range must be from 0.01 to 9999999999.99"
                            RegexValidation="true" RegularExpression="^\d{1,12}(\.\d{1,2})?$" CmpErrorMessage="Man Hours must be greater than 0"
                            CompareValidation="True" ValueToCompare="0.00" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="90px" HorizontalAlign="right" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="MATERIAL_COST" HeaderText="Material Cost" HeaderTitle="Material Cost"
                    SortAscUrl="" SortDescUrl="" SortExpression="MATERIAL_COST" HtmlEncode="False">
                    <TextBox ID="txtMaterialCost" HelpText="231,TARIFF_CODE_MTRL_CST" OnClientTextChange="FormatTwoDecimal"
                        ValidationGroup="tabTariffCode" CssClass="ntxto" iCase="Numeric">
                        <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="GreaterThanEqual"
                            ReqErrorMessage="Material Cost Required" Type="Double" CustomValidationFunction=""
                            CustomValidation="false" CsvErrorMessage="Exist" Validate="True" RegErrorMessage="Invalid Material Cost. Range must be from 0.01 to 9999999999.99"
                            RegexValidation="true" RegularExpression="^\d{1,12}(\.\d{1,2})?$" CmpErrorMessage="Material Cost must be greater than 0"
                            CompareValidation="True" ValueToCompare="0.00" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="90px" HorizontalAlign="right" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="REMARKS" HeaderText="Remarks" HeaderTitle="Remarks"
                    SortAscUrl="" SortDescUrl="" SortExpression="REMARKS" HtmlEncode="False">
                    <TextBox ID="txtRemarks" HelpText="232,TARIFF_CODE_RMRKS_VC" OnClientTextChange=""
                        ValidationGroup="tabTariffCode" CssClass="txt">
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="false" Type="String" Validate="true" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:CheckBoxField DataField="ACTIVE" HeaderText="Active" HeaderTitle="Active" HelpText=""
                    SortAscUrl="" SortDescUrl="">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="30px" />
                </iFg:CheckBoxField>
            </Columns>
            <RowStyle CssClass="gitem" />
            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                OffsetX="" OffsetY="" OnImgClick="" Width="" />
            <PagerStyle CssClass="gpage" HorizontalAlign="Center" />
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
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
        onClientPrint="null" />
    <asp:HiddenField ID="hdnPageTitle" runat="server" />
    </form>
</body>
</html>
