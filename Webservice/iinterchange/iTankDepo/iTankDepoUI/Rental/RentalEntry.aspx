<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RentalEntry.aspx.vb" Inherits="Rental_RentalEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<%--Rental Entry--%>
<body>
    <form id="form1" runat="server" style="overflow: auto">
    <div>
        <table id="tblRental" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Rental >> Rental Entry</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navRental" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="tabdisplayGatePass" id="divRentalEntry" style="height: auto">
        <div>
            <Nav:iTab ID="ITab1" runat="server" ControlHeight="" ControlWidth="" EnableClose="False"
                SelectedTab="" TabHeight="" TabWidth="" TabsPerRow="15">
                <TabPages>
                    <Nav:TabPage ID="Pending" runat="server" Caption="Pending" doValidate="True" OnAfterSelect="ifgRentalOnAfterPendingTabSelected();"
                        OnBeforeSelect="" TabPageClientId="divPending">
                    </Nav:TabPage>
                    <Nav:TabPage ID="MySubmits" runat="server" Caption="My Submits" doValidate="True"
                        OnAfterSelect="ifgRentalOnAfterSubmitTabSelected();" OnBeforeSelect="" TabPageClientId="divPending">
                    </Nav:TabPage>
                </TabPages>
            </Nav:iTab>
        </div>
        <div id="divPending">
            <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width:100%;" >
                <tr>
                    <td>
                        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                            font-family: Arial; font-size: 8pt; display: none; width: 100%;" align="center">
                            <div>
                                No Records Found.</div>
                        </div>
                        <div id="divEquipmentDetail" style="margin: 1px; width: 100%;">
                            <iFg:iFlexGrid ID="ifgRentalEntry" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="25" AllowSearch="True"
                                AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="120px" ShowEmptyPager="True"
                                Width="100%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True"
                                RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
                                EnableViewState="False" AddButtonText="Add Row" DeleteButtonText="Delete Row"
                                GridLines="Both" HorizontalAlign="NotSet" PageSizerFormat="" Scrollbars="None"
                                Type="Normal" ValidationGroup="divRentalEntry" DataKeyNames="RNTL_ENTRY_ID" OnAfterCallBack=""
                                OnBeforeCallBack="" OnAfterClientRowCreated="" Mode="Insert" AddRowsonCurrentPage="True"
                                RowStyle-Wrap="True" UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                                AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                                SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                                ClearButtonIconClass="icon-eraser" ClearButtonCssClass="btn btn-small btn-success"
                                AllowAdd="False" AllowDelete="False">
                                <Columns>
                                    <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                        SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False" ReadOnly="true">
                                        <TextBox ID="txtEquipmentNo" HelpText="" OnClientTextChange="" ValidationGroup="divRentalEntry"
                                            CssClass="txt" MaxLength="11" iCase="Upper">
                                            <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                                                RegexValidation="False" RegularExpression="" ReqErrorMessage="Equipment No Required"
                                                CustomValidationFunction="" CustomValidation="True" Type="String" Validate="true"
                                                CsvErrorMessage="This Equipment No already Exist." />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    </iFg:TextboxField>
                                    <iFg:LookupField DataField="CSTMR_CD" ForeignDataField="CSTMR_ID" HeaderText="Customer *"
                                        HeaderTitle="Customer" PrimaryDataField="CSTMR_ID" SortAscUrl="" SortDescUrl=""
                                        SortExpression="" AllowSearch="True">
                                        <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="501,CUSTOMER_CSTMR_CD"
                                            AutoSearch="true" iCase="Upper" OnClientTextChange="CleraContractRef" TableName="73"
                                            ValidationGroup="divRentalEntry" ID="lkpCustomer" CssClass="lkp" DoSearch="True"
                                            Width="110px" ClientFilterFunction="" TabIndex="1">
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
                                            <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Customer.Click on the List for Valid Values"
                                                ReqErrorMessage="Customer Required" Validate="True" IsRequired="False" CustomValidation="True"
                                                CustomValidationFunction="ValidateCustomer" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" />
                                    </iFg:LookupField>
                                    <iFg:LookupField DataField="CNTRCT_RFRNC_NO" ForeignDataField="CNTRCT_RFRNC_NO" HeaderText="Contract Ref No *"
                                        HeaderTitle="Contract Ref No" PrimaryDataField="CNTRCT_RFRNC_NO" SortAscUrl=""
                                        AllowSearch="True" SortDescUrl="">
                                        <Lookup DataKey="CNTRCT_RFRNC_NO" DependentChildControls="" HelpText="502,RENTAL_ENTRY_CNTRCT_RFRNC_NO"
                                            iCase="None" OnClientTextChange="" TableName="74" ValidationGroup="" ID="lkpCNTRCT_RFRNC_NO"
                                            CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="ContractRefNoFilter"
                                            TabIndex="2">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnCaption="Contract Ref No" ColumnName="CNTRCT_RFRNC_NO" Hidden="false" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px"
                                                HorizontalAlign="Center" />
                                            <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Contract Ref No.Click on the List for Valid Values"
                                                ReqErrorMessage="Contract Ref No Required" Validate="True" IsRequired="False"
                                                CustomValidation="True" CustomValidationFunction="ValidateContractRefNo" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="80px" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField DataField="PO_RFRNC_NO" HeaderText="PO Ref No" HeaderTitle="PO Ref No"
                                        SortAscUrl="" SortDescUrl="" AllowSearch="True">
                                        <TextBox CausesValidation="True" CssClass="txt" HelpText="503,RENTAL_ENTRY_PO_RFRNC_NO"
                                            iCase="None" OnClientTextChange="" ValidationGroup="divRentalEntry" TabIndex="3">
                                            <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                                RegexValidation="false" CustomValidationFunction="" ValidationGroup="divRental"
                                                IsRequired="false" ReqErrorMessage="" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                    </iFg:TextboxField>
                                    <iFg:DateField DataField="ON_HR_DT" HeaderText="On-Hire Date" HeaderTitle="On-Hire Date"
                                        SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                        ReadOnly="false" AllowSearch="True">
                                        <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                            ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="true" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                                LkpErrorMessage="Invalid On-Hire Date. Click on the calendar icon for valid values"
                                                CustomValidation="true" CustomValidationFunction="ValidateOnHireDate" ReqErrorMessage="On-Hire Date Required"
                                                Validate="True" RangeValidation="false" CompareValidation="false" ValidationGroup="divRental" />
                                        </iDate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                    </iFg:DateField>
                                    <iFg:DateField DataField="OFF_HR_DT" HeaderText="Off-Hire Date" HeaderTitle="Off-Hire Date"
                                        SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                        ReadOnly="false" AllowSearch="True">
                                        <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                            ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="true" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                                CustomValidation="true" LkpErrorMessage="Invalid Off-Hire Date. Click on the calendar icon for valid values"
                                                CustomValidationFunction="compareOnHireDate" ReqErrorMessage="Off-Hire Date Required"
                                                Validate="True" RangeValidation="false" CompareValidation="true" ValidationGroup="divRental" />
                                        </iDate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                    </iFg:DateField>
                                    <iFg:HyperLinkField DataTextField="HYP_OTHERCHARGE" HeaderText="Addl. Rate" HeaderTitle="Addl. Rate"
                                        MaxLength="0" SortAscUrl="" SortDescUrl="" IsEditable="False" ReadOnly="true">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                    </iFg:HyperLinkField>
                                    <iFg:TextboxField DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                                        SortAscUrl="" SortDescUrl="" AllowSearch="True">
                                        <TextBox CausesValidation="True" CssClass="txt" HelpText="509,RENTAL_ENTRY_RMRKS_VC"
                                            iCase="None" OnClientTextChange="" ValidationGroup="" TabIndex="9">
                                            <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="false"
                                                ValidationGroup="divRental" RegexValidation="false" CustomValidationFunction=""
                                                IsRequired="false" ReqErrorMessage="" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="120px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:CheckBoxField DataField="CHECKED" HeaderText="Select" HeaderTitle="Select" HelpText=""
                                        HeaderImageUrl="" SortAscUrl="" SortDescUrl="" Visible="True">
                                        <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </iFg:CheckBoxField>
                                     <iFg:TextboxField DataField="RNTL_ENTRY_ID" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                        SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False" ReadOnly="true" Visible="True">
                                        <TextBox ID="TextBox1" HelpText="" OnClientTextChange="" ValidationGroup="divRentalEntry"
                                            CssClass="txt" MaxLength="11" iCase="Upper">
                                            <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                                                RegexValidation="False" RegularExpression="" ReqErrorMessage="Equipment No Required"
                                                CustomValidationFunction="" CustomValidation="False" Type="String" Validate="true"
                                                CsvErrorMessage="This Equipment No already Exist." />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    </iFg:TextboxField>
                                     <iFg:TextboxField DataField="EQPMNT_INFRMTN_ID" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                        SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False" ReadOnly="true" Visible="True">
                                        <TextBox ID="TextBox2" HelpText="" OnClientTextChange="" ValidationGroup="divRentalEntry"
                                            CssClass="txt" MaxLength="11" iCase="Upper">
                                            <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                                                RegexValidation="False" RegularExpression="" ReqErrorMessage="Equipment No Required"
                                                CustomValidationFunction="" CustomValidation="False" Type="String" Validate="false"
                                                CsvErrorMessage="This Equipment No already Exist." />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    </iFg:TextboxField>
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
                                    <iFg:ActionButton ID="actnBttnDelete" Text="Delete" ValidateRowSelection="False"
                                        OnClientClick="DeleteClick();" IconClass="icon-trash" CSSClass="btn btn-small btn-danger" />
                                </ActionButtons>
                            </iFg:iFlexGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();"
        onclientprint="null" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    </form>
</body>
</html>
