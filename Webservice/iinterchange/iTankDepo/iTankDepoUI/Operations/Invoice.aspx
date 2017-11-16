<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Invoice.aspx.vb" Inherits="Billing_Invoice" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="overflow:auto">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Repair Invoice</span>
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
    </div>
    <div class="" id="tabInvoice">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd">
            <tr>
                <td>
                    <table border="0" height="50px" cellpadding="2" align="left" cellspacing="2" class="tblstd">
                        <tr>
                            <td>
                                <label id="lblCustomer" runat="server" class="lbl">
                                    * Customer</label>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpCustomer" runat="server" CssClass="lkp" DataKey="CSTMR_CD" DoSearch="True"
                                    iCase="Upper" TabIndex="1" TableName="9" HelpText="87,CUSTOMER_CSTMR_CD" Width="100px"
                                    ClientFilterFunction="applyDepoFilter">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                        IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        LkpErrorMessage="Invalid Customer Code. Click on the list for valid values" ReqErrorMessage="Customer Required."
                                        Validate="True" ValidationGroup="tabInvoice" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                            <td>
                                <label id="lblCustCurrency" runat="server" class="lbl">
                                    Customer Currency</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtCustCurrency" runat="server" CssClass="txt" TabIndex="-1" HelpText=""
                                    TextMode="SingleLine" iCase="Upper" ReadOnly="True">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                        LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False"
                                        CustomValidation="False" RegexValidation="False" SetFocusOnError="False" />
                                </Inp:iTextBox>
                            </td>
                            <td>
                                <label id="lblConvToCurrency" runat="server" class="lbl">
                                    Convert To Currency</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtConvToCurrency" runat="server" CssClass="txt" TabIndex="-1"
                                    HelpText="" TextMode="SingleLine" iCase="Upper" ReadOnly="True">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                        LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False"
                                        CustomValidation="False" RegexValidation="False" SetFocusOnError="False" />
                                </Inp:iTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <Inp:iLabel ID="lblExportFormat" runat="server" CssClass="lbl" ToolTip="Export Format" Visible="false" >
                                    Export Format
                                </Inp:iLabel>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpINVFormat" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                                    iCase="None" MaxLength="50" TabIndex="1" TableName="32" HelpText="205,ENUM_ENM_CD"
                                    ClientFilterFunction="" ToolTip="Invoice Format" OnClientTextChange="" Width="100px" Visible="false">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="lkpINVFormat" Hidden="False"
                                            ColumnCaption="Invoice Format" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                        IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="200px" VerticalAlign="Top"
                                        HorizontalAlign="Right" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                        LkpErrorMessage="Invalid Invoice Format. Click on the list for valid values"
                                        Operator="NotEqual" ReqErrorMessage="Invoice Format Required." Type="String"
                                        Validate="true" ValidationGroup="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <span><a href="#" id="lnkFetch" onclick="onFetchClick();return false;">Fetch</a></span>
                                <span><a href="#" id="lnkReset" onclick="onResetClick();return false;">Reset</a></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblExchangeRate" runat="server" class="lbl">
                                    Exchange Rate</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtExchangeRate" runat="server" CssClass="txt" TabIndex="-1" HelpText=""
                                    TextMode="SingleLine" iCase="Upper" ReadOnly="True">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                        LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False"
                                        CustomValidation="False" RegexValidation="False" SetFocusOnError="False" />
                                </Inp:iTextBox>
                            </td>
                            <td>
                                <label id="lblFromDate" runat="server" class="lbl">
                                    From Date</label>
                            </td>
                            <td>
                                <Inp:iDate ID="datFromDate" TabIndex="-1" runat="server" HelpText="" ToolTip="" CssClass="txt"
                                    ReadOnly="true">
                                    <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" Type="String"
                                        ValidationGroup="" LkpErrorMessage="" ReqErrorMessage="" Validate="false" CsvErrorMessage=""
                                        CustomValidation="True" CustomValidationFunction="" />
                                    <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="true"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iDate>
                            </td>
                            <td>
                                <label id="lblToDate" runat="server" class="lbl">
                                    To Date</label>
                            </td>
                            <td colspan="4">
                                <Inp:iDate ID="datToDate" TabIndex="-1" runat="server" HelpText="" ToolTip="" CssClass="txt">
                                    <Validator CustomValidateEmptyText="True" IsRequired="True" Operator="GreaterThan"
                                        Type="Date" ValidationGroup="" LkpErrorMessage="Invalid Date. Click on the calendar icon"
                                        ReqErrorMessage="To Date Required." Validate="True" CsvErrorMessage="" CustomValidationFunction="valdiateToDate"
                                        CmpErrorMessage="" ValueToCompare="" ControlToCompare="" CustomValidation="True" />
                                    <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="true"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iDate>
                            </td>
                        </tr>
                        <tr style="height: 30px; vertical-align: bottom;">
                            <td colspan="9">
                                <label id="lblExportLink" runat="server" class="lbl" visible="false" >
                                    Please <a id="lnkExport" class="blnk" runat="server" href="#" target="_blank">click
                                        here </a>to download the Invoice.
                                </label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 40px">
                <td>
                    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();" />
                </td>
            </tr>
        </table>
        <label id="lblInvoiceDetails" runat="server" class="lbl">
            <strong><u>Invoice Details</u></strong></label>
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd">
            <tr style="width: 100%; height: 2%;">
                <td>
                </td>
            </tr>
            <tr style="width: 100%; height: 100%;">
                <td>
                    <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                        font-family: Arial; font-size: 8pt; display: none; width: 100%;">
                        <Inp:iLabel ID="lblListRowCount" runat="server" Visible="true">No Records Found</Inp:iLabel>
                    </div>
                    <div id="divDetail" style="vertical-align: middle">
                        <iFg:iFlexGrid ID="ifgInvoice" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CaptionAlign="Left" CellPadding="2" CssClass="tblstd" DataKeyNames="INVC_BIN"
                            GridLines="Both" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Type="Normal" ValidationGroup="" PageSize="15" StaticHeaderHeight="300px" ShowEmptyPager="True"
                            Width="970px" AllowAdd="False" AllowDelete="False" UseCachedDataSource="True"
                            AllowSearch="True" AllowSorting="True" AllowStaticHeader="True" AutoSearch="True"
                            HeaderRows="1" EnableViewState="False" SortAscImageUrl="~/Images/down.gif" HorizontalAlign="NotSet"
                            OnAfterCallBack="onAfterCallBack" Scrollbars="None">
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <PagerStyle CssClass="gpage" Height="18px" Font-Names="Arial" HorizontalAlign="Center" />
                            <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <ActionButtons>
                                <iFg:ActionButton ID="btnViewInvoice" OnClientClick="viewInvoice" Text="View Invoice"
                                    ValidateRowSelection="true" Visible="True" />
                            </ActionButtons>
                            <Columns>
                                <iFg:TextboxField DataField="CSTMR_CD" HeaderText="Customer" HeaderTitle="Customer"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression="CSTMR_CD" HeaderStyle-Wrap="false">
                                    <TextBox HelpText="" iCase="Upper" MaxLength="10" OnClientTextChange="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            ValidationGroup="" RegexValidation="False" RegErrorMessage="" RegularExpression=""
                                            CustomValidation="False" CustomValidationFunction="" />
                                    </TextBox>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Width="120px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="INVC_NO" HeaderText="Invoice No" HeaderTitle="Invoice No"
                                    SortAscUrl="" SortDescUrl="" SortExpression="INVC_NO" HeaderStyle-Wrap="false">
                                    <TextBox HelpText="" iCase="Upper" CssClass="txt" ReadOnly="true">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false"
                                            ValidationGroup="" RegularExpression="" RegexValidation="True" RegErrorMessage=""
                                            CustomValidation="True" CustomValidationFunction="" IsRequired="true" ReqErrorMessage="" />
                                    </TextBox>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Width="80px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:DateField DataField="FRM_BLLNG_DT" HeaderText="Period From" HeaderTitle="Period From"
                                    SortAscUrl="" SortDescUrl="" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"
                                    ItemStyle-Wrap="false" ReadOnly="true" SortExpression="FRM_BLLNG_DT" HeaderStyle-Wrap="false">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" MaxLength="11"
                                        TopPosition="0" ValidationGroup="">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                    </iDate>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:DateField>
                                <iFg:DateField DataField="TO_BLLNG_DT" HeaderText="Period To" HeaderTitle="Period To"
                                    SortAscUrl="" SortDescUrl="" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"
                                    ItemStyle-Wrap="false" ReadOnly="true" SortExpression="TO_BLLNG_DT" HeaderStyle-Wrap="false">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" MaxLength="11"
                                        TopPosition="0" ValidationGroup="">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                    </iDate>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:DateField>
                                <iFg:TextboxField DataField="BLLNG_FLG" HeaderText="Version" HeaderTitle="Version"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression="BLLNG_FLG">
                                    <TextBox HelpText="" iCase="None" OnClientTextChange="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false"
                                            ValidationGroup="" RegexValidation="False" RegErrorMessage="" RegularExpression=""
                                            CustomValidation="False" CustomValidationFunction="" />
                                    </TextBox>
                                    <ItemStyle Width="50px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="CRTD_BY" HeaderText="Prepared By" HeaderTitle="Prepared By"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression="CRTD_BY" HeaderStyle-Wrap="false">
                                    <TextBox HelpText="" iCase="Upper" OnClientTextChange="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false"
                                            ValidationGroup="" RegexValidation="False" RegErrorMessage="" RegularExpression=""
                                            CustomValidation="False" CustomValidationFunction="" />
                                    </TextBox>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Width="120px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:DateField DataField="CRTD_DT" HeaderText="Prepared Date" HeaderTitle="Prepared Date"
                                    SortAscUrl="" SortDescUrl="" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"
                                    ItemStyle-Wrap="false" ReadOnly="true" SortExpression="CRTD_DT" HeaderStyle-Wrap="false">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" MaxLength="11"
                                        ValidationGroup="">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                    </iDate>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:DateField>
                                <iFg:HyperLinkField HeaderText="PDF" HeaderTitle="" IsEditable="False" MaxLength="0"
                                    ReadOnly="True" SortAscUrl="" SortDescUrl="" Text="PDF" HeaderStyle-Wrap="false">
                                    <ItemStyle Width="80px" Wrap="true" />
                                </iFg:HyperLinkField>
                                 <iFg:HyperLinkField HeaderText="Excel" HeaderTitle="" IsEditable="False" MaxLength="0"
                                    ReadOnly="True" SortAscUrl="" SortDescUrl="" Text="Excel" HeaderStyle-Wrap="false">
                                    <ItemStyle Width="80px" Wrap="true" />
                                </iFg:HyperLinkField>
                                 <iFg:HyperLinkField HeaderText="Word" HeaderTitle="" IsEditable="False" MaxLength="0"
                                    ReadOnly="True" SortAscUrl="" SortDescUrl="" Text="Word" HeaderStyle-Wrap="false">
                                    <ItemStyle Width="80px" Wrap="true" />
                                </iFg:HyperLinkField>
                            </Columns>
                        </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnInvcNo" runat="server" />
    <asp:HiddenField ID="hdnSysDate" runat="server" />
    <asp:HiddenField ID="hdn032KeyValue" runat="server" />
    <a id="hiddenLinkViewInvoice" href="#" style="display: none" target="_blank" />
    </form>
</body>
</html>
