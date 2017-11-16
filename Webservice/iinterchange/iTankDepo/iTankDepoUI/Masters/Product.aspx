<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Product.aspx.vb" Inherits="Masters_Product" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="overflow:auto">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 20px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Product</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navProduct" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Fix -->
    <div class="tabdisplayGatePass" id="tabProduct" style="overflow-y: auto; overflow-x: auto;height:auto">
        <table border="0" cellpadding="1" cellspacing="1" class="tblstd" style="width:100%">
   <%-- <div class="" id="tabProduct" style="height: 350px;">
        <table border="0" cellpadding="1" cellspacing="1" class="tblstd">--%>
            <tr>
                <td>
                </td>
                <td>
                    <label id="lblPRDCT_CD" runat="server" class="lbl">
                        Product Code
                    </label>
                     <label id="Label2" runat="server" class="lbl">
                      
                    </label>
                    <Inp:iLabel ID="lblPRDCT_CDReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtPRDCT_CD" runat="server" CssClass="txt" TabIndex="1" HelpText="269,PRODUCT_PRDCT_CD"
                        TextMode="SingleLine" iCase="Upper" ToolTip="Product Code">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets and Numbers are Allowed"
                           RegularExpression="^[a-zA-Z0-9]+$" Type="String" Validate="True"
                            ValidationGroup="tabCustomer" LookupValidation="False" CsvErrorMessage="This Code Already Exists"
                            CustomValidationFunction="fnValidateProductCode" IsRequired="true" ReqErrorMessage="Product Code Required"
                            CustomValidation="true" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <label id="lblPRDCT_DSCRPTN_VC" runat="server" class="lbl">
                        Product Name
                    </label>
                    <Inp:iLabel ID="lblPRDCT_DSCRPTN_VCReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtPRDCT_DSCRPTN_VC" runat="server" CssClass="txt" TabIndex="2"
                        HelpText="270,PRODUCT_PRDCT_DSCRPTN_VC" TextMode="SingleLine" iCase="None" ToolTip="Product Name">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabCustomer" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="true" ReqErrorMessage="Product Name Required" CustomValidation="false"
                            RegexValidation="false" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <label id="lblCHMCL_NM" runat="server" class="lbl">
                        Chemical Name
                    </label> <Inp:iLabel ID="lblCHMCL_NMReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtCHMCL_NM" runat="server" CssClass="txt" TabIndex="3" HelpText="271,PRODUCT_CHMCL_NM"
                        TextMode="SingleLine" iCase="None" ToolTip="Chemical Name">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabCustomer" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="true" ReqErrorMessage="Chemical Name Required" CustomValidation="false"
                            RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <label id="lblIMO_CLSS" runat="server" class="lbl">
                        IMO Class
                    </label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtIMO_CLSS" runat="server" CssClass="txt" TabIndex="4" HelpText="272,PRODUCT_IMO_CLSS"
                        TextMode="SingleLine" iCase="None" ToolTip="IMO Class">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabCustomer" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" ReqErrorMessage="IMO Class Required" CustomValidation="false"
                            RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <label id="lblUN_NO" runat="server" class="lbl">
                        UN #
                    </label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtUN_NO" runat="server" CssClass="txt" TabIndex="5" HelpText="273,PRODUCT_UN_NO"
                        TextMode="SingleLine" iCase="None" ToolTip="UN #">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabCustomer" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" ReqErrorMessage="UN No Required" CustomValidation="false"
                            RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <label id="lblCLSSFCTN_ID" runat="server" class="lbl">
                        Classification
                    </label>
                </td>
                <td>
                    <Inp:iLookup ID="lkpCLSSFCTN_ID" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                        iCase="None" TabIndex="6" TableName="40" HelpText="274,ENUM_ENM_CD" ClientFilterFunction=""
                        ToolTip="Classification" OnClientTextChange="" Width="100px" Visible="true">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="lkpCLSSFCTN_ID" Hidden="False"
                                ColumnCaption="Classification" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                            LkpErrorMessage="Invalid Classification. Click on the list for valid values"
                            Operator="NotEqual" ReqErrorMessage="Classification Required" Type="String"
                            Validate="true" ValidationGroup="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <label id="lblGRP_CLSSFCTN_ID" runat="server" class="lbl">
                        Group Classification
                    </label>
                </td>
                <td>
                    <Inp:iLookup ID="lkpGRP_CLSSFCTN_ID" runat="server" CssClass="lkp" DataKey="ENM_CD"
                        DoSearch="True" iCase="None" TabIndex="7" TableName="41" HelpText="275,ENUM_ENM_CD"
                        ClientFilterFunction="" ToolTip="Group Classification" OnClientTextChange=""
                        Width="100px" Visible="true">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="lkpGRP_CLSSFCTN_ID" Hidden="False"
                                ColumnCaption="Group Classification" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                            LkpErrorMessage="Invalid Group Classification. Click on the list for valid values"
                            Operator="NotEqual" ReqErrorMessage="Group Classification Required" Type="String"
                            Validate="true" ValidationGroup="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <label id="inpCLNNG_TTL_AMNT_NC" runat="server" class="lbl">
                        Cleaning Rate
                    </label>
                </td>
                <td>
                    <Inp:iLabel ID="lblCLNNG_TTL_AMNT_NC" runat="server" CssClass="lbl" Font-Bold="True"
                        Font-Underline="false"></Inp:iLabel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <label id="lblRMRKS_VC" runat="server" class="lbl">
                        Remarks
                    </label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtRMRKS_VC" runat="server" CssClass="txt" TabIndex="8" HelpText="276"
                        TextMode="MultiLine" iCase="None" ToolTip="Remarks" MaxLength="255">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabCustomer" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" ReqErrorMessage="Remarks Required" CustomValidation="false"
                            RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <label id="Label3" runat="server" class="lbl">
                        Cleaning Method
                    </label> 
                </td>
                <td>
              <Inp:iLookup ID="lkpType" runat="server" CssClass="lkp" DataKey="CLNNG_MTHD_TYP_CD"
                        DoSearch="True" iCase="None" TabIndex="10" TableName="81" HelpText=""
                        ClientFilterFunction="" ToolTip="Cleaning Method" OnClientTextChange=""
                        Width="100px" Visible="true">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="CLNNG_MTHD_TYP_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="CLNNG_MTHD_TYP_CD" ControlToBind="lkpType" Hidden="False"
                                ColumnCaption="Type" />
                                <Inp:LookupColumn ColumnName="CLNNG_MTHD_TYP_DSCRPTN_VC" Hidden="False" ColumnCaption="Description" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                            LkpErrorMessage="Invalid Cleaning Method. Click on the list for valid values"
                            Operator="NotEqual" ReqErrorMessage="Cleaning Method Required" Type="String"
                            Validate="true" ValidationGroup="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                  <label id="lblCLNBL_BT" runat="server" class="lbl">
                        Cleanable
                    </label>
                </td>
                <td>
                       <asp:CheckBox ID="chkCLNBL_BT" runat="server" Text="" TabIndex="9" ToolTip="Cleanable"
                        CssClass="chk" />
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                 <label id="lblACTV_BT" runat="server" class="lbl">
                        Active
                    </label> 
                </td>
                <td>
                       <asp:CheckBox ID="chkACTV_BT" runat="server" TabIndex="10" Text="" ToolTip="Active"
                        CssClass="chk" />
                </td>
                <td>             
                </td>
            </tr>
            <tr>
                <td>
            
                </td>
                <td>
                      
                </td>
                <td>               
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                   
                </td>
                <td>
                  
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                   
                </td>
                <td>
                   
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                 <a id="hypFilesAttachment" href="#" onclick="showFilesAttachment();" runat="server"  >Attach Files</a> 
                </td>
                <td>  
                      
                </td>
                <td>             
                </td>
            </tr>
            <tr>
                <td colspan="16">
                </td>
            </tr>
            <tr>
                <td colspan="16">
                    <table style="width: 300px;">
                        <tr>
                            <td>
                                <label id="lblCleaningType" runat="server" class="lbl">
                                    <strong><u>Default Cleaning Rate</u></strong>
                                </label>
                            </td>
                            <td>
                                <label id="Label1" runat="server" class="lbl">
                                    <strong><u>Customer Specific Cleaning Rate</u></strong>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divifgCleaningType" style="margin: 1px; width: 360px;">
                                    <iFg:iFlexGrid ID="ifgCleaningType" runat="server" AllowStaticHeader="true" DataKeyNames="PRDCT_CLNNG_RT_ID"
                                        Width="300px" Height="" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1"
                                        AllowPaging="false" HorizontalAlign="NotSet" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                        Scrollbars="None" ShowEmptyPager="true" StaticHeaderHeight="150px" Type="Normal"
                                        ValidationGroup="divCleaningType" UseCachedDataSource="True" AutoGenerateColumns="False"
                                        EnableViewState="False" OnAfterClientRowCreated="setDefaultValues" PageSize="15"
                                        CssClass="tblstd" ShowPageSizer="False" OnAfterCallBack="OnAfterCallBack" OnBeforeCallBack="OnBeforeCallBack"
                                        UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-primary"
                                        AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                        DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                        RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btnmcorner btn-small btn-primary"
                                        SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser">
                                        <RowStyle CssClass="gitem" />
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                        <Columns>
                                            <iFg:LookupField DataField="CLNNG_TYP_CD" HeaderText="Cleaning Type" PrimaryDataField="CLNNG_TYP_ID"
                                                ForeignDataField="CLNNG_TYP_ID" HeaderTitle="Cleaning Type" SortAscUrl="" SortDescUrl=""
                                                SortExpression="">
                                                <Lookup DataKey="CLNNG_TYP_CD" DependentChildControls="" HelpText="277,CLEANING_TYPE_CLNNG_TYP_CD" iCase="Upper"
                                                    OnClientTextChange="" TableName="42" ValidationGroup="" ID="lkpCLNG_TYP" CssClass="lkp"
                                                    DoSearch="True" Width="100px" ClientFilterFunction="applyFilter" AllowSecondaryColumnSearch="true" 
                                                    SecondaryColumnName="CLNNG_TYP_DSCRPTN_VC">
                                                    <LookupColumns>
                                                        <Inp:LookupColumn ColumnCaption="CLNNG_TYP_ID" ColumnName="CLNNG_TYP_ID" Hidden="True" />
                                                        <Inp:LookupColumn ColumnName="CLNNG_TYP_CD" Hidden="False" ColumnCaption="Code" />
                                                        <Inp:LookupColumn ColumnName="CLNNG_TYP_DSCRPTN_VC" Hidden="False" ColumnCaption="Description" />
                                                        <Inp:LookupColumn ColumnName="DFLT_BT" ColumnCaption="Default" Hidden="true" />
                                                    </LookupColumns>
                                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="Top"   Width="300px" HorizontalAlign="Left"   />
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Cleaning Type. Click on the List for Valid Values"
                                                        ReqErrorMessage="Cleaning Type Required" Validate="True" IsRequired="True" CustomValidation="true"
                                                        CustomValidationFunction="checkDuplicate" />
                                                </Lookup>
                                                <ItemStyle Width="120px" Wrap="True" />
                                            </iFg:LookupField>
                                            <iFg:TextboxField DataField="AMNT_NC" HeaderText="Rate" HeaderTitle="Rate" SortAscUrl=""
                                                SortDescUrl="">
                                                <TextBox ID="txtAMNT_NC" CssClass="txt" HelpText="278" MaxLength="10"
                                                    iCase="Numeric" OnClientTextChange="formatCleaningType" ValidationGroup="" Width="80px">
                                                    <Validator CustomValidateEmptyText="False" Validate="true" IsRequired="false" Operator="Equal"
                                                        ReqErrorMessage="Rate Required." Type="Double" RegexValidation="True" RegularExpression="^\d{1,7}(\.\d{1,2})?$"
                                                        RegErrorMessage="Invalid Cleaning Rate. Cleaning Rate must be upto 7 digits and 2 decimal points" />
                                                </TextBox>
                                                <ItemStyle Width="100px" Wrap="true" HorizontalAlign="Right" />
                                            </iFg:TextboxField>
                                        </Columns>
                                        <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                                        <SelectedRowStyle CssClass="gsitem" />
                                        <AlternatingRowStyle CssClass="gaitem" />
                                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                    </iFg:iFlexGrid>
                                </div>
                            </td>
                            <td>
                                <div id="divifgProductCustomer" style="margin: 1px; width: 480px;">
                                    <iFg:iFlexGrid ID="ifgProductCustomer" runat="server" AllowStaticHeader="True" DataKeyNames="PRDCT_CSTMR_ID"
                                        Width="450px" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="NotSet"
                                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                        Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="150px" Type="Normal"
                                        ValidationGroup="divProductCustomer" UseCachedDataSource="True" AutoGenerateColumns="False"
                                        EnableViewState="False" OnAfterClientRowCreated="" PageSize="15" CssClass="tblstd"
                                        ShowPageSizer="False" OnAfterCallBack="OnifgProductCustomerAfterCallBack" OnBeforeCallBack="OnifgProductCustomerBeforeCallBack"
                                        Mode="Insert" UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-primary"
                                        AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                        DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                        RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btnmcorner btn-small btn-primary"
                                        SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser">
                                        <RowStyle CssClass="gitem" />
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                        <Columns>
                                            <iFg:LookupField DataField="CSTMR_CD" HeaderText="Customer" PrimaryDataField="CSTMR_ID"
                                                ForeignDataField="CSTMR_ID" HeaderTitle="Customer" SortAscUrl="" SortDescUrl=""
                                                SortExpression="">
                                                <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="87,CUSTOMER_CSTMR_CD" iCase="Upper" OnClientTextChange=""
                                                    TableName="9" ValidationGroup="divProductCustomer" CssClass="lkp" DoSearch="True"
                                                    Width="200px" ClientFilterFunction="" AllowSecondaryColumnSearch ="true" SecondaryColumnName ="CSTMR_NAM">
                                                    <LookupColumns>
                                                        <Inp:LookupColumn ColumnName="CSTMR_ID" ColumnCaption="CSTMR_ID" Hidden="True" />
                                                        <Inp:LookupColumn ColumnName="CSTMR_CD" ColumnCaption="Code" Hidden="False" />
                                                        <Inp:LookupColumn ColumnName="CSTMR_NAM" ColumnCaption="Name" Hidden="False" />
                                                    </LookupColumns>
                                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="NotSet" Width="300px" />
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Customer. Click on the List for Valid Values"
                                                        ReqErrorMessage="Customer Required" Validate="True" IsRequired="True" CustomValidation="true"
                                                        ValidationGroup="divProductCustomer" CustomValidationFunction="checkDuplicateProductCustomer" />
                                                </Lookup>
                                                <ItemStyle Width="200px" Wrap="True" />
                                            </iFg:LookupField>
                                            <iFg:HyperLinkField HeaderText="Cleaning Rate" HeaderTitle="Cleaning Rate" SortAscUrl=""
                                                SortDescUrl="" Text="Add" IsEditable="False" ReadOnly="True" >
                                            </iFg:HyperLinkField>
                                            <iFg:TextboxField DataField="TTL_AMNT_NC" HeaderText="Total" HeaderTitle="Total"
                                                SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                                <TextBox CssClass="txt" HelpText="" iCase="Numeric" OnClientTextChange="" ValidationGroup="divProductCustomer"
                                                    Width="80px">
                                                    <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" ReqErrorMessage="Amount Required"
                                                        Type="Double" />
                                                </TextBox>
                                                <ItemStyle Width="150px" Wrap="true" HorizontalAlign="Right" />
                                            </iFg:TextboxField>
                                        </Columns>
                                        <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                                        <SelectedRowStyle CssClass="gsitem" />
                                        <AlternatingRowStyle CssClass="gaitem" />
                                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                    </iFg:iFlexGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" align="center" onClientPrint="null"/>
    </form>
</body>
</html>
