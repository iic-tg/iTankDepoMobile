<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExchangeRate.aspx.vb" Inherits="Masters_ExchangeRate" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="height: 550px">
    <div>
        <div>
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh">Exchange Rate</span>
                    </td>
                    <td align="right">
                        <nv:Navigation ID="navExRate" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tabdisplayGatePass" id="tabExchangeRate" style="overflow-y: auto; overflow-x: auto;
            height: auto">
            <div class="topspace">
            </div>
            <iFg:iFlexGrid ID="ifgExchangeRate" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="10" AllowSearch="True"
                AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="300px" ShowEmptyPager="True"
                Width="100%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True"
                RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
                EnableViewState="False" AddButtonText="Add Exchange Rate" DeleteButtonText="Delete"
                GridLines="Both" HorizontalAlign="NotSet" PageSizerFormat="EXCHNG_RT_ID" Scrollbars="None"
                Type="Normal" ValidationGroup="tabExchangeRate" OnAfterCallBack="onAfterCB" OnBeforeCallBack=""
                OnAfterClientRowCreated="setDefaultValues" Mode="Insert" DataKeyNames="EXCHNG_RT_ID"
                UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser">
                <Columns>
                    <iFg:LookupField DataField="FRM_CRRNCY_CD" ForeignDataField="FRM_CRRNCY_ID" HeaderText="From Currency *"
                        HeaderTitle="From Currency" PrimaryDataField="CRRNCY_ID" SortAscUrl="" SortDescUrl=""
                        SortExpression="FRM_CRRNCY_CD">
                        <Lookup DataKey="CRRNCY_CD" DependentChildControls="" HelpText="18,CURRENCY_CRRNCY_CD"
                            iCase="Upper" OnClientTextChange="" TableName="2" ValidationGroup="" ID="lkpCurrencyFrom"
                            CssClass="lkp" DoSearch="True" Width="120px" ClientFilterFunction="applyDepoFilter"
                            AllowSecondaryColumnSearch="true" SecondaryColumnName="CRRNCY_DSCRPTN_VC">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnCaption="CRRNCY_ID" ColumnName="CRRNCY_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="CRRNCY_CD" Hidden="False" />
                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="CRRNCY_DSCRPTN_VC" Hidden="False" />
                            </LookupColumns>
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid From Currency. Click on the List for Valid Values"
                                ReqErrorMessage="From Currency Required" Validate="True" IsRequired="True" CustomValidation="True"
                                CustomValidationFunction="fnvalidateCurrencies" />
                        </Lookup>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="120px" />
                    </iFg:LookupField>
                    <iFg:LookupField DataField="TO_CRRNCY_CD" ForeignDataField="TO_CRRNCY_ID" HeaderText="To Currency *"
                        HeaderTitle="To Currency" PrimaryDataField="CRRNCY_ID" SortAscUrl="" SortDescUrl=""
                        SortExpression="TO_CRRNCY_CD">
                        <Lookup DataKey="CRRNCY_CD" DependentChildControls="" HelpText="19,CURRENCY_CRRNCY_CD"
                            iCase="Upper" OnClientTextChange="" TableName="2" ValidationGroup="" ID="lkpCurrencyTo"
                            CssClass="lkp" DoSearch="True" Width="95px" ClientFilterFunction="applyDepoFilter"
                            AllowSecondaryColumnSearch="true" SecondaryColumnName="CRRNCY_DSCRPTN_VC">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnCaption="CRRNCY_ID" ColumnName="CRRNCY_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="CRRNCY_CD" Hidden="False" />
                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="CRRNCY_DSCRPTN_VC" Hidden="False" />
                            </LookupColumns>
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                LkpErrorMessage="Invalid To Currency. Click on the List for Valid Values" ReqErrorMessage="To Currency Required"
                                Validate="True" CustomValidation="True" CustomValidationFunction="fnvalidateCurrencies" />
                        </Lookup>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="120px" />
                    </iFg:LookupField>
                    <iFg:TextboxField CharacterLimit="0" DataField="EXCHNG_RT_PR_UNT_NC" HeaderText="Exchange Rate *"
                        HeaderTitle="Exchange Rate" SortAscUrl="" SortDescUrl="" SortExpression="EXCHNG_RT_PR_UNT_NC">
                        <TextBox ID="txtExchangeRate" HelpText="20" iCase="Numeric" OnClientTextChange="formatExchageRate"
                            ValidationGroup="" CssClass="ntxto" MaxLength="17">
                            <Validator CustomValidateEmptyText="False" IsRequired="True" Operator="Equal" RegErrorMessage="Invalid Exchange Rate. Exchange Rate can be upto 12 digits and 4 decimal points."
                                RegexValidation="True" RegularExpression="^(\+)?\d{1,12}(\.\d{1,4})?$" ReqErrorMessage="Exchange Rate Required"
                                Type="Double" Validate="True" />
                        </TextBox>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="120px" HorizontalAlign="Right" />
                    </iFg:TextboxField>
                    <iFg:DateField DataField="WTH_EFFCT_FRM_DT" HeaderText="Effective Date *" HeaderTitle="Effective Date"
                        SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                        SortExpression="WTH_EFFCT_FRM_DT">
                        <iDate HelpText="21" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                            ValidationGroup="" CssClass="txt" Width="80px" MaxLength="11">
                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="True"
                                LkpErrorMessage="Invalid Effective Date. Click on the calendar icon for valid values"
                                CustomValidationFunction="" ReqErrorMessage="Effective Date Required" Validate="True"
                                CompareValidation="True" CustomValidation="true" CmpErrorMessage="Effective Date Should Not be Greater Than Current Date" />
                        </iDate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                    </iFg:DateField>
                    <iFg:CheckBoxField DataField="ACTV_BT" HeaderText="Active" HeaderTitle="Active" HelpText=""
                        SortAscUrl="" SortDescUrl="" SortExpression="">
                        <ItemStyle Width="80px" />
                        <HeaderStyle HorizontalAlign="Left" />
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
                    <iFg:ActionButton ID="aBtExport" Text="Export" ValidateRowSelection="False" OnClientClick="exportToExcel();"
                        IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                </ActionButtons>
            </iFg:iFlexGrid>
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
        onClientPrint="null" />
    <asp:HiddenField ID="hdnPageTitle" runat="server" />
    </form>
</body>
<script runat="server">
    Protected Function GetCurrentDate() As String
        Return DateTime.Now.ToString("dd-MMM-yyyy")
    End Function
</script>
</html>
