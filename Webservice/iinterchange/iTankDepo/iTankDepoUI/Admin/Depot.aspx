<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Depot.aspx.vb" Inherits="Admin_Depot" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Depot</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navEstimate" runat="server" />
                </td>
            </tr>
        </table>
        <!-- UIG Fix -->
        <div id="tabDepot" class="tabdisplayGatePass" style="overflow-y: auto; overflow-x: auto;
            height: auto">
            <table border="0" cellpadding="2" cellspacing="2" align="left" class="tblstd" style="width: 99.5%;">
                <tr>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblDpt_cd" runat="server" ToolTip="Depot Code" CssClass="lbl">
  Depot Code</Inp:iLabel>
                        <Inp:iLabel ID="lblbrnch_cdreq1" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtDpt_cd" runat="server" TabIndex="1" CssClass="txt" ToolTip="Depot Code"
                            HelpText="70,DEPOT_DPT_CD" Height="" TextMode="SingleLine" iCase="Upper" Width=""
                            MaxLength="9">
                            <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="True"
                                ControlToCompare="" CsvErrorMessage="This Depot Code already exists" CustomValidationFunction="ValidateDepotCode"
                                IsRequired="True" ReqErrorMessage="Depot Code Required" ValidationGroup="tabDepot"
                                Validate="True" RegErrorMessage="Only Alphabets and Numbers are allowed" RegexValidation="True"
                                RegularExpression="^[a-zA-Z0-9]+$"></Validator>
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblDptNam" runat="server" ToolTip="Depot Name" CssClass="lbl"> Depot Name</Inp:iLabel>
                        <Inp:iLabel ID="DptNam" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtDpt_Nam" runat="server" TabIndex="2" CssClass="txt" ToolTip="Depot Name"
                            HelpText="71,DEPOT_DPT_NAM" Height="" TextMode="SingleLine" iCase="None" Width=""
                            MaxLength="50">
                            <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="false"
                                ControlToCompare="" CsvErrorMessage="" CustomValidationFunction="" IsRequired="True"
                                ReqErrorMessage="Depot Name  Required" ValidationGroup="tabDepot" Validate="True"
                                RegexValidation="true" RegErrorMessage="Only Alphabets/Numbers and [-_+.:'&\/[](),] are allowed"
                                RegularExpression="^[a-zA-Z0-9-_+.:'&,\\\/\[\]\(\) ]+$"></Validator>
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblCntctPrsn" runat="server" ToolTip="Contact Person" CssClass="lbl">   
  Contact Person</Inp:iLabel><Inp:iLabel ID="CntctPersn" runat="server" ToolTip="*"
      CssClass="lblReq">
  *</Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtCntctPrsn" runat="server" CssClass="txt" MaxLength="20" TabIndex="3"
                            HelpText="73,DEPOT_CNTCT_PRSN_NAM" TextMode="SingleLine" iCase="None" 
                            ToolTip="Contact Person">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                ValidationGroup="tabDepot" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                IsRequired="True" ReqErrorMessage="Contact Person Required" CustomValidation="False"
                                RegexValidation="false" RegErrorMessage="" RegularExpression="" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblLogo" runat="server" ToolTip="Company Logo" CssClass="lbl">
  Logo : </Inp:iLabel>
                        <Inp:iLabel ID="lblLogoReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td class="style1">
                        <Inp:iLabel ID="lblOrganizationType" runat="server" ToolTip="Organization Type" CssClass="lbl">   
  Organization Type</Inp:iLabel><Inp:iLabel ID="lblOrgTypReq" runat="server" ToolTip="*"
      CssClass="lblReq">
  *</Inp:iLabel>
                    </td>
                    <td class="style1">
                      <div id="divLkpOrganizationType">
                        <Inp:iLookup ID="lkpOrgizationType" runat="server" CssClass="lkp" DataKey="ENM_CD"
                            DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="4" TableName="102" HelpText="614,ENUM_ENM_CD"
                            ClientFilterFunction="filterOrganizationType" OnClientTextChange="" ReadOnly="false">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="ENM_CD" Hidden="False" />
                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="ENM_DSCRPTN_VC" Hidden="False" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="350px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" CustomValidation="True"
                                CustomValidationFunction="validateOrganizationType" ReqErrorMessage="Organization Type Required" LkpErrorMessage="Invalid Organization Type. Click on the list for valid values"
                                Operator="Equal" Type="String" Validate="True" ValidationGroup="" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                        </div>
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                        <Inp:iLabel ID="lblReportingTo" runat="server" ToolTip="Reporting To" CssClass="lbl">   
  Reporting To</Inp:iLabel><Inp:iLabel ID="lblReportToReq" runat="server" ToolTip="*"
      CssClass="lblReq">
  *</Inp:iLabel>
                    </td>
                    <td class="style1">
                    <div id="divLkpDepot">
                        <Inp:iLookup ID="lkpDepotCode" runat="server" CssClass="lkp" DataKey="DPT_CD" DoSearch="True"
                            iCase="Upper" MaxLength="10" TabIndex="5" TableName="29" HelpText="615,DEPOT_DPT_CD" 
                            ClientFilterFunction="filterReportingTo" AllowSecondaryColumnSearch="False">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="DPT_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="DPT_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnCaption="Name" ColumnName="DPT_NAM" Hidden="False" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Depot. Click on the list for valid values"
                                Operator="Equal" ReqErrorMessage="Reporting To Depot Required" Type="String" Validate="True"
                                ValidationGroup="" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </div>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td class="style1">
                        <Inp:iLabel ID="lblCntct_Addrss" runat="server" ToolTip="Address1" CssClass="lbl">
 Address 1</Inp:iLabel>
                        <Inp:iLabel ID="ILabel2" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                    </td>
                    <td class="style1">
                        <Inp:iTextBox ID="txtCntct_Addrss_Ln1" runat="server" CssClass="txt" MaxLength="50"
                            TabIndex="6" HelpText="74,DEPOT_ADDRSS_LN1_VC" TextMode="SingleLine" iCase="None"
                            ToolTip="Contact Address">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                ValidationGroup="tabDepot" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                IsRequired="True" ReqErrorMessage="Contact Address 1 Required" CustomValidation="False"
                                RegexValidation="false" RegErrorMessage="" RegularExpression="" />
                        </Inp:iTextBox>
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                        <Inp:iLabel ID="lblphn_no" runat="server" ToolTip="Phone No" CssClass="lbl">
  Phone No</Inp:iLabel>
                    </td>
                    <td class="style1">
                        <Inp:iTextBox ID="txtphn_no" runat="server" TabIndex="7" CssClass="txt" ToolTip="Phone No"
                            HelpText="81,DEPOT_PHN_NO" Height="" TextMode="SingleLine" iCase="None" Width=""
                            MaxLength="15">
                            <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="false"
                                ControlToCompare="" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False"
                                ReqErrorMessage="Phone No Required" ValidationGroup="tabDepot" Validate="True"
                                RegErrorMessage="Invalid Phone No" RegexValidation="True" RegularExpression="[0-9 ]*[\]*[0-9 ,_,+,-]*$">
                            </Validator>
                        </Inp:iTextBox>
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                        <Inp:iLabel ID="lblfx_no" runat="server" ToolTip="Fax" CssClass="lbl">
  Fax</Inp:iLabel>
                    </td>
                    <td class="style1">
                        <Inp:iTextBox ID="txtfx_no" runat="server" TabIndex="8" CssClass="txt" ToolTip="Fax"
                            HelpText="82,DEPOT_FX_NO" Height="" TextMode="SingleLine" iCase="None" Width=""
                            MaxLength="15">
                            <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="false"
                                ControlToCompare="" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False"
                                ReqErrorMessage="Fax Required" ValidationGroup="tabDepot" Validate="True" RegErrorMessage="Invalid Fax"
                                RegexValidation="true" RegularExpression="[0-9 ]*[\]*[0-9 ,_,+,-]*$"></Validator>
                        </Inp:iTextBox>
                    </td>
                    <td class="style1">
                    </td>
                    <td rowspan="5" colspan="2" align="right" valign="top">
                        <div id="divLogo">
                            <img width="100" src="../Images/noimage.jpg" id="imgCompanyLogo" runat="server" title="Click to change logo"
                                onclick="fnBrowseImage();" style="border: solid 2px #e5e0ec" alt="" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="Address2" runat="server" ToolTip="Address2" CssClass="lbl">
 Address 2</Inp:iLabel>
                        <Inp:iLabel ID="ILabel3" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtCntct_Addrss_Ln2" runat="server" CssClass="txt" MaxLength="50"
                            TabIndex="9" HelpText="75,DEPOT_ADDRSS_LN1_VC" TextMode="SingleLine" iCase="None"
                            ToolTip="Contact Address">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                ValidationGroup="tabDepot" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                IsRequired="True" ReqErrorMessage="Contact Address 2 Required" CustomValidation="False"
                                RegexValidation="false" RegErrorMessage="" RegularExpression="" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblEmailID" runat="server" ToolTip="Email ID" CssClass="lbl">
  Email ID</Inp:iLabel>
                        <Inp:iLabel ID="ILabel1" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtEml_ID" runat="server" TabIndex="10" CssClass="txt" ToolTip="Email ID"
                            HelpText="80,DEPOT_EML_ID" Height="" TextMode="SingleLine" iCase="None" Width=""
                            MaxLength="10">
                            <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="false"
                                ControlToCompare="" CsvErrorMessage="" CustomValidationFunction="" IsRequired="True"
                                ReqErrorMessage="Email ID Required" ValidationGroup="tabDepot" Validate="True"
                                RegexValidation="true" RegErrorMessage="Invalid Email Format" RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$">
                            </Validator>
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblVAT_No" runat="server" ToolTip="VAT No" CssClass="lbl">
  VAT No</Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtVAT_NO" runat="server" TabIndex="11" CssClass="txt" ToolTip="VAT No"
                            HelpText="83,DEPOT_VT_NO" Height="" TextMode="SingleLine" iCase="Upper" Width=""
                            MaxLength="50">
                            <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="false"
                                ControlToCompare="" CsvErrorMessage="" CustomValidationFunction="" IsRequired="false"
                                ReqErrorMessage="VAT No Required" ValidationGroup="tabDepot" Validate="True"
                                RegexValidation="true" RegErrorMessage="Only Alphabets/Numbers and [-_+.:'&\/[](),] are allowed"
                                RegularExpression="^[a-zA-Z0-9-_+.:'&,\\\/\[\]\(\) ]+$"></Validator>
                        </Inp:iTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="Address3" runat="server" ToolTip="Address3" CssClass="lbl">
 Address 3</Inp:iLabel>
                        <Inp:iLabel ID="ILabel4" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtCntct_Addrss_Ln3" runat="server" CssClass="txt" MaxLength="50"
                            TabIndex="12" HelpText="76,DEPOT_ADDRSS_LN1_VC" TextMode="SingleLine" iCase="None"
                            ToolTip="Contact Address">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                ValidationGroup="tabDepot" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                IsRequired="True" ReqErrorMessage="Contact Address 3 Required" CustomValidation="False"
                                RegexValidation="false" RegErrorMessage="" RegularExpression="" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <!-- UIG Fix -->
            <div id="divBankDetail" class='tabdisplayGatePass' style="width: 99.5%;">
                <iFg:iFlexGrid ID="ifgBankDetail" runat="server" AllowStaticHeader="True" DataKeyNames="BNK_DTL_BIN"
                    Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Center"
                    PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                    Scrollbars="None" ShowEmptyPager="False" StaticHeaderHeight="140px" Type="Normal"
                    ValidationGroup="divBankDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                    EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="setDefaultValues"
                    ShowPageSizer="False" OnAfterCallBack="OnAfterCallBack" OnBeforeCallBack="OnBeforeCallBack"
                    AllowFilter="False" UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-primary"
                    AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                    DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                    RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btnmcorner btn-small btn-primary"
                    SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser" ShowRecordCount="False">
                    <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <RowStyle CssClass="gitem" />
                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                    <Columns>
                        <iFg:LookupField DataField="BNK_TYP_CD" ForeignDataField="BNK_TYP_ID" HeaderText="Bank Type *"
                            HeaderTitle="Bank Type" SortAscUrl="" SortDescUrl="" PrimaryDataField="ENM_ID"
                            SortExpression="BNK_TYP_CD">
                            <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="217,ENUM_ENM_CD" iCase="Upper"
                                OnClientTextChange="" TableName="38" ValidationGroup="divBankDetail" ID="lkpBNK_TYP_CD"
                                CssClass="lkp" DoSearch="True" Width="90px" ClientFilterFunction="">
                                <LookupColumns>
                                    <Inp:LookupColumn ColumnCaption="ENM_ID" ColumnName="ENM_ID" Hidden="True" />
                                    <Inp:LookupColumn ColumnCaption="Bank Type" ColumnName="ENM_CD" Hidden="False" />
                                </LookupColumns>
                                <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                    IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Bank Type. Click on the List for Valid Values"
                                    ReqErrorMessage="Bank Type Required" Validate="True" IsRequired="True" CustomValidation="True"
                                    CustomValidationFunction="checkDuplicate" />
                            </Lookup>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="90px" />
                        </iFg:LookupField>
                        <iFg:TextboxField DataField="BNK_NM" HeaderText="Bank Name *" HeaderTitle="Bank Name"
                            SortAscUrl="" SortDescUrl="" SortExpression="BNK_NM" HtmlEncode="False">
                            <TextBox ID="txtBankName" HelpText="218,BANK_DETAIL_BNK_NM" OnClientTextChange=""
                                ValidationGroup="divBankDetail" CssClass="txt">
                                <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                                    RegexValidation="False" RegularExpression="" ReqErrorMessage="Bank Name Required"
                                    CustomValidationFunction="" CustomValidation="false" Type="String" Validate="true" />
                            </TextBox>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </iFg:TextboxField>
                        <iFg:TextboxField DataField="BNK_ADDRSS" HeaderText="Bank Address *" HeaderTitle="Bank Address"
                            SortAscUrl="" SortDescUrl="" SortExpression="BNK_ADDRSS" HtmlEncode="False">
                            <TextBox ID="txtBankAddress" HelpText="219,BANK_DETAIL_BNK_ADDRSS" OnClientTextChange=""
                                ValidationGroup="" CssClass="txt">
                                <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                                    RegexValidation="False" RegularExpression="" ReqErrorMessage="Bank Address Required"
                                    CustomValidationFunction="" CustomValidation="false" Type="String" Validate="true" />
                            </TextBox>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                        </iFg:TextboxField>
                        <iFg:TextboxField DataField="ACCNT_NO" HeaderText="Account No *" HeaderTitle="Account No"
                            SortAscUrl="" SortDescUrl="" SortExpression="ACCNT_NO" HtmlEncode="False">
                            <TextBox ID="txtAccountNo" HelpText="220,BANK_DETAIL_ACCNT_NO" OnClientTextChange=""
                                ValidationGroup="divBankDetail" CssClass="txt">
                                <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                                    RegexValidation="False" RegularExpression="" ReqErrorMessage="Account No Required"
                                    CustomValidationFunction="" CustomValidation="false" Type="String" Validate="true" />
                            </TextBox>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="90px" HorizontalAlign="Left" />
                        </iFg:TextboxField>
                        <iFg:TextboxField DataField="IBAN_NO" HeaderText="IBAN" HeaderTitle="IBAN" SortAscUrl=""
                            SortDescUrl="" SortExpression="IBAN_NO" HtmlEncode="False">
                            <TextBox ID="txtIBANNo" HelpText="221,BANK_DETAIL_IBAN_NO" OnClientTextChange=""
                                ValidationGroup="divBankDetail" CssClass="txt">
                                <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                                    RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                                    CustomValidation="false" Type="String" Validate="true" />
                            </TextBox>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="90px" HorizontalAlign="Left" />
                        </iFg:TextboxField>
                        <iFg:TextboxField DataField="SWIFT_CD" HeaderText="SWIFT Code" HeaderTitle="SWIFT Code"
                            SortAscUrl="" SortDescUrl="" SortExpression="SWIFT_CD" HtmlEncode="False">
                            <TextBox ID="txtSWIFTCode" HelpText="222,BANK_DETAIL_SWIFT_CD" OnClientTextChange=""
                                ValidationGroup="divBankDetail" CssClass="txt">
                                <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                                    RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                                    CustomValidation="false" Type="String" Validate="true" />
                            </TextBox>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="90px" HorizontalAlign="Left" />
                        </iFg:TextboxField>
                        <iFg:LookupField DataField="CRRNCY_CD" ForeignDataField="CRRNCY_ID" HeaderText="Currency *"
                            HeaderTitle="Currency" PrimaryDataField="CRRNCY_ID" SortAscUrl="" SortDescUrl=""
                            SortExpression="CRRNCY_CD">
                            <Lookup DataKey="CRRNCY_CD" DependentChildControls="" HelpText="223,BANK_DETAIL_CRRNCY_ID"
                                iCase="Upper" OnClientTextChange="" TableName="2" ValidationGroup="divBankDetail"
                                ID="lkpCurrency" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction=""
                                AllowSecondaryColumnSearch="true" SecondaryColumnName="CRRNCY_DSCRPTN_VC" MaxLength="3">
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
                                <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px"
                                    HorizontalAlign="Right" />
                                <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Currency. Click on the List for Valid Values"
                                    ReqErrorMessage="Currency Required" Validate="True" IsRequired="True" CustomValidation="false"
                                    CustomValidationFunction="" />
                            </Lookup>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="80px" />
                        </iFg:LookupField>
                    </Columns>
                    <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                    <SelectedRowStyle CssClass="gsitem" />
                    <AlternatingRowStyle CssClass="gaitem" />
                    <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                        IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                </iFg:iFlexGrid>
            </div>
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
        onClientPrint="null" />
     <iframe id="fmPhotoUpload" frameborder="0" height="0px" width="0px" name="fmPhotoUpload" src="PhotoUpload.aspx"></iframe>
    <input id="hdnLogoPath" name="hdnLogoPath" type="hidden" runat="server" />
    <input id="hdnDepotID" name="DepotID" type="hidden" runat="server" />
    </form>
</body>
</html>
