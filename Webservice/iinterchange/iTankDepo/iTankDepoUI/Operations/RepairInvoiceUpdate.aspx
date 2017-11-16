<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RepairInvoiceUpdate.aspx.vb"
    Inherits="Operations_RepairInvoiceUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Operations &gt;&gt; Repair Invoice Update</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navCleaningInstruction" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Fix -->
    <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
        font-family: Arial; font-size: 8pt; display: none; width: 80%;" align="center">
        No Records Found
    </div>
    <div class="tabdisplayGatePass" id="divRepairInvoiceUpdate" style="height: auto">
        <div class="topspace">
        </div>
        <iFg:iFlexGrid ID="ifgRepairInvoiceUpdate" runat="server" AllowStaticHeader="True"
            DataKeyNames="RPR_CHRG_ID" Width="100%" CaptionAlign="NotSet" GridLines="Both"
            HeaderRows="1" HorizontalAlign="Justify" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
            Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
            ValidationGroup="divRepairInvoiceUpdate" UseCachedDataSource="True" AutoGenerateColumns="False"
            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
            PageSize="24" AddRowsonCurrentPage="False" AllowPaging="True" OnAfterCallBack=""
            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
            AllowAdd="False">
            <Columns>
            <%--MultiDepo--%>
            <iFg:TextboxField DataField="DPT_CD" HeaderText="Depot Code" HeaderTitle="Depot Code"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False" ReadOnly="True"
                    IsEditable="False">
                    <TextBox ID="txtDepotCode" HelpText="" OnClientTextChange="" ValidationGroup="divRepairInvoiceUpdate"
                        CssClass="txt" MaxLength="11" iCase="Upper">
                        <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="Equipment No Required"
                            CustomValidationFunction="" CustomValidation="False" Type="String" Validate="false"
                            CsvErrorMessage="This Depot Code already Exist." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:LookupField DataField="CSTMR_CD" ForeignDataField="CSTMR_ID" HeaderText="Customer"
                    HeaderTitle="Customer" PrimaryDataField="CSTMR_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="" ReadOnly="True" IsEditable="False">
                    <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="" iCase="Upper" OnClientTextChange=""
                        TableName="9" ValidationGroup="divRepairInvoiceUpdate" ID="lkpCustomer" CssClass="lkp"
                        DoSearch="True" Width="120px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                        SecondaryColumnName="CSTMR_NAM">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="CSTMR_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Customer Name" ColumnName="CSTMR_NAM" Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="280px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Customer. Click on the List for Valid Values."
                            ReqErrorMessage="Customer Required" Validate="True" IsRequired="false" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="120px" />
                </iFg:LookupField>
                <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment Number"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False" ReadOnly="True"
                    IsEditable="False">
                    <TextBox ID="txtEquipmentNo" HelpText="" OnClientTextChange="" ValidationGroup="divRepairInvoiceUpdate"
                        CssClass="txt" MaxLength="11" iCase="Upper">
                        <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="Equipment No Required"
                            CustomValidationFunction="" CustomValidation="False" Type="String" Validate="false"
                            CsvErrorMessage="This Equipment No already Exist." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="ESTMT_NO" HeaderText="Estimate No" HeaderTitle="Estimate Number"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False" ReadOnly="True"
                    IsEditable="False">
                    <TextBox ID="txtEstimateNo" HelpText="" OnClientTextChange="" ValidationGroup="divRepairInvoiceUpdate"
                        CssClass="txt" iCase="Upper">
                        <Validator CustomValidateEmptyText="false" IsRequired="false" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="Estimate Number Required"
                            CustomValidationFunction="" CustomValidation="False" Type="String" Validate="false"
                            CsvErrorMessage="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:DateField DataField="RPR_CMPLTN_DT" HeaderText="Repair Completion Date" HeaderTitle="Repair Completion Date"
                    SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                    HtmlEncode="false" AllowSearch="true" ReadOnly="true">
                    <iDate HelpText="90" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <Validator CustomValidateEmptyText="True" Operator="LessThanEqual" Type="Date" IsRequired="false"
                            LkpErrorMessage="Invalid In Date. Click on the calendar icon for valid values"
                            ReqErrorMessage="In Date Required" Validate="True" CompareValidation="True" CustomValidation="true"
                            CmpErrorMessage="In Date cannot be greater than current Date." CustomValidationFunction="ValidateGateInDate" />
                    </iDate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="120px" Wrap="True" />
                </iFg:DateField>
                <iFg:TextboxField DataField="APPRVL_RF_NO" HeaderText="Approval Ref No *" HeaderTitle="Approval Ref No"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False" ReadOnly="False"
                    IsEditable="True">
                    <TextBox ID="txtReferenceNo" HelpText="514,REPAIR_ESTIMATE_OWNR_APPRVL_RF" OnClientTextChange=""
                        ValidationGroup="divRepairInvoiceUpdate" CssClass="txt" iCase="None">
                        <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="Customer Approval Ref No Required"
                            CustomValidationFunction="" CustomValidation="false" Type="String" Validate="True"
                            CsvErrorMessage="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="RPR_APPRVL_AMNT_NC" HeaderText="Amount" HeaderTitle="Amount"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False" ReadOnly="true"
                    IsEditable="True">
                    <TextBox ID="txtAmount" HelpText="515,REPAIR_CHARGE_RPR_APPRVL_AMNT_NC" ValidationGroup="divRepairInvoiceUpdate"
                        CssClass="ntxto" iCase="Numeric" OnClientTextChange="formatAmount">
                        <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="Amount Required"
                            CustomValidationFunction="" CustomValidation="false" Type="String" Validate="True"
                            CsvErrorMessage="This Equipment No already Exist." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </iFg:TextboxField>
                <iFg:LookupField DataField="INVCNG_PRTY_CD" ForeignDataField="INVCNG_PRTY_ID" HeaderText="Invoicing Party"
                    HeaderTitle="Invoicing Party" PrimaryDataField="INVCNG_PRTY_ID" SortAscUrl=""
                    SortDescUrl="" SortExpression="" ReadOnly="false" IsEditable="true">
                    <Lookup DataKey="INVCNG_PRTY_CD" DependentChildControls="" HelpText="516,INVOICING_PARTY_INVCNG_PRTY_CD"
                        iCase="Upper" OnClientTextChange="" TableName="48" ValidationGroup="divRepairInvoiceUpdate"
                        ID="lkpInvoicingParty" CssClass="lkp" DoSearch="True" Width="150px" ClientFilterFunction=""
                        AllowSecondaryColumnSearch="true" SecondaryColumnName="INVCNG_PRTY_NM">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="INVCNG_PRTY_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="INVCNG_PRTY_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Name" ColumnName="INVCNG_PRTY_NM" Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="280px"
                            HorizontalAlign="Right" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Invoicing Party. Click on the list for valid values."
                            ReqErrorMessage="" Validate="True" IsRequired="false" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="120px" />
                </iFg:LookupField>
                <iFg:CheckBoxField DataField="CHECKED" HeaderText="Select" HeaderImageUrl="" HeaderTitle="Select"
                    HelpText="" SortAscUrl="" SortDescUrl="" ReadOnly="false">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="25px" Wrap="True" HorizontalAlign="Left" />
                </iFg:CheckBoxField>
            </Columns>
            <RowStyle CssClass="gitem" />
            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                OffsetX="" OffsetY="" OnImgClick="" Width="" />
            <PagerStyle CssClass="gpage" Height="18px" HorizontalAlign="Center" />
            <FooterStyle CssClass="gftr" HorizontalAlign="Left" Width="100%" />
            <SelectedRowStyle CssClass="gsitem" />
            <AlternatingRowStyle CssClass="gaitem" />
        </iFg:iFlexGrid>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
        onClientPrint="null" />
    </form>
</body>
</html>
