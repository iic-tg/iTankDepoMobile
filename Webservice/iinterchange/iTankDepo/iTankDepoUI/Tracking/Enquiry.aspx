<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Enquiry.aspx.vb" Inherits="Tracking_Enquiry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Header1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Tracking >> Enquiry</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navEnquiry" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divEnquiry" style="overflow-y: auto;overflow-x: auto; height: auto">
        <fieldset class="lbl" id="fldSearch">
            <legend class="blbl">Search</legend>
            <table border="0" cellpadding="2" cellspacing="2" class="tblstd">
                <tr>
                    <td>
                        <table border="0" cellpadding="2" align="left" cellspacing="2" class="tblstd" align="center">
                            <tr>
                                <td>
                                    <label id="lblEnquiryType" runat="server" class="lbl">
                                        Enquiry Type
                                    </label>
                                    <Inp:iLabel ID="ILabel6" runat="server" CssClass="lblReq">
                   *
                                    </Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iLookup ID="lkpEnquiryType" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                                        iCase="Upper" MaxLength="10" TabIndex="1" TableName="" HelpText="388,ENUM_ENM_CD"
                                        ClientFilterFunction="" AllowSecondaryColumnSearch="false" SecondaryColumnName="ENM_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Enquiry Type" />
                                            <Inp:LookupColumn ColumnName="ENM_DSCRPTN_VC" ControlToBind="" Hidden="True" ColumnCaption="Description" />
                                        </LookupColumns>
                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Enquiry Type. Click on the list for valid values"
                                            Operator="Equal" ReqErrorMessage="Enquiry Type Required" Type="String" Validate="True"
                                            ValidationGroup="divEnquiry" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iLookup>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <ul style="margin: 0px;">
                                        <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="hypFetch" style="text-decoration: none;
                                            color: White; font-weight: bold" class="icon-search" runat="server" onclick="Fetch(); return false;"
                                            tabindex="2">&nbsp;Fetch</a></li>
                                    </ul>
                                </td>
                                <td>
                                    <label id="Label1" runat="server" class="blbl">
                                        Note:
                                    </label>
                                    <label id="lblNote" runat="server" class="lbl">
                                        Select the Enquiry Type and click the Fetch Button
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
            font-family: Arial; font-size: 8pt; display: none; width: 80%;" align="center">
            <div>
                No Records Found.</div>
        </div>
        <div id="divProduct" runat="server" style="margin: 1px; width: 99.9%; vertical-align: middle;">
            <fieldset class="lbl" id="fldProduct">
                <legend class="blbl">Product</legend>
                <table border="0" cellpadding="2" cellspacing="2" class="">
                    <tr >
                        <td colspan="5">
                            <iFg:iFlexGrid ID="ifgProduct" runat="server" AllowStaticHeader="True" DataKeyNames="PRDCT_ID"
                                Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                                ValidationGroup="divProduct" UseCachedDataSource="True" AutoGenerateColumns="False"
                                EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                                PageSize="15" AddRowsonCurrentPage="False" ShowPageSizer="True" AllowPaging="True"
                                AllowAdd="false" AllowDelete="False" ShowFooter="True" OnAfterCallBack="ifgProductOnAfterCB"
                                AllowRefresh="True" AllowSearch="True" AutoSearch="True" OnBeforeCallBack=""
                                RowStyle-HorizontalAlign="NotSet" UseIcons="true" SearchButtonIconClass="icon-search"
                                SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                                AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                                DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                                RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                                SearchCancelButtonCssClass="btn btn-small btn-danger" ClearButtonIconClass="icon-eraser"
                                ClearButtonCssClass="btn btn-small btn-success" AllowSorting="True" UseActivitySpecificDatasource="True">
                                <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <Columns>
                                    <iFg:LookupField DataField="PRDCT_CD" ForeignDataField="PRDCT_ID" HeaderText="Code"
                                        HeaderTitle="Code" PrimaryDataField="PRDCT_ID" SortAscUrl="" SortDescUrl="" AllowSearch="true">
                                        <Lookup DataKey="PRDCT_CD" DependentChildControls="" HelpText="" iCase="Upper" OnClientTextChange=""
                                            TableName="46" ValidationGroup="divProduct" ID="lklPreviousCargo" CssClass="lkp"
                                            DoSearch="True" Width="150px" ClientFilterFunction="" AutoSearch="true" AllowSecondaryColumnSearch="true"
                                            SecondaryColumnName="PRDCT_DSCRPTN_VC" ReadOnly="true">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnCaption="ID" ColumnName="PRDCT_ID" Hidden="true" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="PRDCT_CD" Hidden="false" />
                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="PRDCT_DSCRPTN_VC" Hidden="false" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="200px" HorizontalAlign="Center" />
                                            <Validator CustomValidateEmptyText="false" Operator="Equal" Type="String" LkpErrorMessage="Invalid Previous Cargo. Click on the List for Valid Values"
                                                ReqErrorMessage="" Validate="True" IsRequired="false" CustomValidation="false"
                                                CustomValidationFunction="" />
                                        </Lookup>
                                        <HeaderStyle CssClass="ghdr" Width="80px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="110px" Wrap="True" Font-Bold="false" />
                                    </iFg:LookupField>
                                    <iFg:LookupField DataField="PRDCT_DSCRPTN_VC" ForeignDataField="PRDCT_ID" HeaderText="Name"
                                        HeaderTitle="Name" PrimaryDataField="PRDCT_ID" SortAscUrl="" SortDescUrl="" AllowSearch="true">
                                        <Lookup DataKey="PRDCT_DSCRPTN_VC" DependentChildControls="" HelpText="" iCase="Upper"
                                            OnClientTextChange="" TableName="46" ValidationGroup="divProduct" ID="lkpName"
                                            CssClass="lkp" DoSearch="True" Width="150px" ClientFilterFunction="" AutoSearch="true"
                                            AllowSecondaryColumnSearch="true" SecondaryColumnName="PRDCT_DSCRPTN_VC" ReadOnly="true">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnCaption="ID" ColumnName="PRDCT_ID" Hidden="true" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="PRDCT_CD" Hidden="false" />
                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="PRDCT_DSCRPTN_VC" Hidden="false" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="200px" HorizontalAlign="Center" />
                                            <Validator CustomValidateEmptyText="false" Operator="Equal" Type="String" LkpErrorMessage="Invalid Previous Cargo. Click on the List for Valid Values"
                                                ReqErrorMessage="" Validate="True" IsRequired="false" CustomValidation="false"
                                                CustomValidationFunction="" />
                                        </Lookup>
                                        <HeaderStyle CssClass="ghdr" Width="80px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="110px" Wrap="True" Font-Bold="false" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="CHMCL_NM" HeaderText="Chemical Name"
                                        HeaderTitle="Chemical Name" SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true"
                                        SortExpression="">
                                        <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                            MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle CssClass="ghdr" Width="80px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="120px" Wrap="True" Font-Bold="false" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="IMO_CLSS" HeaderText="IMO Class"
                                        HeaderTitle="IMO Class" SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true"
                                        SortExpression="">
                                        <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                            MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle CssClass="ghdr" Width="80px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="120px" Wrap="True" Font-Bold="false" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="UN_NO" HeaderText="UN #" HeaderTitle="UN #"
                                        SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true" SortExpression="">
                                        <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                            MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle CssClass="ghdr" Width="80px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="120px" Wrap="True" Font-Bold="false" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="CLNBL" HeaderText="Cleanable" HeaderTitle="Cleanable"
                                        SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true" SortExpression="">
                                        <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                            MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle CssClass="ghdr" Width="40px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="70px" Wrap="True" Font-Bold="false" HorizontalAlign="Center" />
                                    </iFg:TextboxField>
                                    <iFg:LookupField DataField="PRODUCT_CLASSIFICATION" ForeignDataField="PRODUCT_CLASSIFICATION"
                                        HeaderText="Classification" HeaderTitle="Classification" SortAscUrl="" SortDescUrl=""
                                        PrimaryDataField="ENM_ID" SortExpression="">
                                        <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="" iCase="Upper" OnClientTextChange=""
                                            TableName="40" ValidationGroup="divProduct" ID="lkpClassification" CssClass="lkp"
                                            DoSearch="True" Width="90px" ClientFilterFunction="" ReadOnly="true">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnCaption="ENM_ID" ColumnName="ENM_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Classification" ColumnName="ENM_CD" Hidden="False" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="200px" />
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage=""
                                                ReqErrorMessage="" Validate="True" IsRequired="False" CustomValidation="False"
                                                CustomValidationFunction="" />
                                        </Lookup>
                                        <HeaderStyle CssClass="ghdr" Width="80px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="120px" Wrap="True" Font-Bold="false" />
                                    </iFg:LookupField>
                                    <iFg:LookupField DataField="GROUP_CLASSIFICATION" ForeignDataField="GROUP_CLASSIFICATION"
                                        HeaderText="Group Classification" HeaderTitle="Group Classification" SortAscUrl=""
                                        SortDescUrl="" PrimaryDataField="ENM_ID" SortExpression="">
                                        <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="" iCase="Upper" OnClientTextChange=""
                                            TableName="41" ValidationGroup="divProduct" ID="lkpGroup" CssClass="lkp" DoSearch="True"
                                            Width="90px" ClientFilterFunction="" ReadOnly="true">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnCaption="ENM_ID" ColumnName="ENM_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Group Classification" ColumnName="ENM_CD" Hidden="False" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" HorizontalAlign="Right" VerticalAlign="NotSet"
                                                Width="200px" />
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage=""
                                                ReqErrorMessage="" Validate="True" IsRequired="False" CustomValidation="False"
                                                CustomValidationFunction="" />
                                        </Lookup>
                                        <HeaderStyle CssClass="ghdr" Width="80px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="120px" Wrap="True" Font-Bold="false" />
                                    </iFg:LookupField>
                                    <iFg:CheckBoxField DataField="CHECKED" HeaderText="Select">
                                       <HeaderStyle CssClass="ghdr" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="25px" HorizontalAlign="Left" /> 
                                    </iFg:CheckBoxField>
                                </Columns>
                                <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                                <SelectedRowStyle CssClass="gsitem" />
                                <AlternatingRowStyle CssClass="gaitem" />
                                <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                    IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                <ActionButtons>
                                    <iFg:ActionButton ID="actnBttnGenerateProduct" Text="Generate" ValidateRowSelection="False"
                                        OnClientClick="GenerateProductDocument();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                                </ActionButtons>
                            </iFg:iFlexGrid>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div id="divCustomer" runat="server" style="margin: 1px; width: 99.9%; vertical-align: middle;">
            <fieldset class="lbl" id="Fieldset1">
                <legend class="blbl">Customer Tariff</legend>
                <table border="0" cellpadding="2" cellspacing="2" class="" style="width: 100%;">
                    <tr style="width: 100%;">
                        <td colspan="5">
                            <iFg:iFlexGrid ID="ifgCustomer" runat="server" AllowStaticHeader="True" DataKeyNames="CSTMR_ID"
                                Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                                ValidationGroup="divProduct" UseCachedDataSource="True" AutoGenerateColumns="False"
                                EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                                PageSize="25" AddRowsonCurrentPage="False" ShowPageSizer="True" AllowPaging="True"
                                AllowAdd="false" AllowDelete="False" ShowFooter="True" OnAfterCallBack="ifgCustomerOnAfterCB"
                                AllowRefresh="True" AllowSearch="True" AutoSearch="True" OnBeforeCallBack=""
                                UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                                AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                                SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="btn btn-small btn-success"
                                SearchCancelButtonCssClass="btn btn-small btn-danger" UseActivitySpecificDatasource="True">
                                <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <Columns>
                                    <iFg:LookupField DataField="CSTMR_CD" ForeignDataField="CSTMR_ID" HeaderText="Customer"
                                        HeaderTitle="Customer" PrimaryDataField="CSTMR_ID" SortAscUrl="" SortDescUrl=""
                                        AllowSearch="true">
                                        <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="" iCase="Upper" ReadOnly="true"
                                            OnClientTextChange="clearEquipmentNo" ValidationGroup="divEquipmentDetail" MaxLength="15"
                                            TableName="9" CssClass="lkp" DoSearch="True" Width="450px" ClientFilterFunction=""
                                            AllowSecondaryColumnSearch="true" SecondaryColumnName="CSTMR_NAM" AutoSearch="true">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                                                <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px" />
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                LkpErrorMessage="Invalid Customer. Click on the list for valid values" ReqErrorMessage="Customer Required."
                                                Validate="True" />
                                        </Lookup>
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="200px" Wrap="True" Font-Bold="false" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="CSTMR_NAM" HeaderText="Name" HeaderTitle="Name"
                                        SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                                        <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                            MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle CssClass="ghdr" Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="380px" Wrap="True" Font-Bold="false" />
                                    </iFg:TextboxField>
                                    <iFg:HyperLinkField DataTextField="RPRT" Text="View" HeaderText="View" HeaderTitle="View"
                                        MaxLength="0" SortAscUrl="" SortDescUrl="" IsEditable="False" ReadOnly="True"
                                        AllowSearch="true">
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="90px" />
                                    </iFg:HyperLinkField>
                                </Columns>
                                <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                                <SelectedRowStyle CssClass="gsitem" />
                                <AlternatingRowStyle CssClass="gaitem" />
                                <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                    IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                <%-- <ActionButtons>
                                    <iFg:ActionButton ID="actnBtnGenerate" Text="Generate" ValidateRowSelection="False"
                                        OnClientClick="GenerateCustomerDocument();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                                </ActionButtons>--%>
                            </iFg:iFlexGrid>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div id="divRoute" runat="server" style="margin: 1px; width: 99.9%; vertical-align: middle;">
            <fieldset class="lbl" id="Fieldset2">
                <legend class="blbl">Route Tariff</legend>
                <table border="0" cellpadding="2" cellspacing="2" class="" style="width: 100%;">
                    <tr style="width: 100%;">
                        <td colspan="5">
                            <iFg:iFlexGrid ID="ifgRoute" runat="server" AllowStaticHeader="True" DataKeyNames="RT_ID"
                                Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                                ValidationGroup="divRoute" UseCachedDataSource="True" AutoGenerateColumns="False"
                                EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                                PageSize="25" AddRowsonCurrentPage="False" ShowPageSizer="True" AllowPaging="True"
                                AllowAdd="false" AllowDelete="False" ShowFooter="True" OnAfterCallBack="ifgRentalOnAfterCB"
                                AllowRefresh="True" AllowSearch="True" AutoSearch="True" OnBeforeCallBack=""
                                UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                                AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                                SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="btn btn-small btn-success"
                                SearchCancelButtonCssClass="btn btn-small btn-danger" UseActivitySpecificDatasource="True">
                                <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <Columns>
                                    <iFg:LookupField DataField="RT_CD" ForeignDataField="RT_ID" HeaderText="Route" HeaderTitle="Route"
                                        PrimaryDataField="RT_ID" SortAscUrl="" SortDescUrl="" AllowSearch="true">
                                        <Lookup DataKey="RT_CD" DependentChildControls="" HelpText="" iCase="Upper" ReadOnly="true"
                                            OnClientTextChange="" ValidationGroup="" MaxLength="15" TableName="68" CssClass="lkp"
                                            DoSearch="True" Width="450px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                                            SecondaryColumnName="RT_DSCRPTN_VC" AutoSearch="true">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="RT_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="RT_CD" Hidden="False" />
                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="RT_DSCRPTN_VC" Hidden="False" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px" />
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                LkpErrorMessage="Invalid Route. Click on the list for valid values" ReqErrorMessage="Route Required."
                                                Validate="True" />
                                        </Lookup>
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="200px" Wrap="True" Font-Bold="false" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="RT_DSCRPTN_VC" HeaderText="Description"
                                        HeaderTitle="Description" SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                                        <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                            MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle CssClass="ghdr" Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="450px" Wrap="True" Font-Bold="false" />
                                    </iFg:TextboxField>
                                    <iFg:HyperLinkField DataTextField="RPRT" Text="View" HeaderText="View" HeaderTitle="View"
                                        MaxLength="0" SortAscUrl="" SortDescUrl="" IsEditable="False" ReadOnly="True"
                                        AllowSearch="true">
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="90px" />
                                    </iFg:HyperLinkField>
                                </Columns>
                                <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                                <SelectedRowStyle CssClass="gsitem" />
                                <AlternatingRowStyle CssClass="gaitem" />
                                <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                    IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            </iFg:iFlexGrid>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
