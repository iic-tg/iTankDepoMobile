<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agent.aspx.vb" Inherits="Masters_Agent" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Agent</title>
    <style type="text/css">
        .style1
        {
            width: 555px;
            height: 130px;
        }
        .style2
        {
            width: 30%;
            height: 130px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table id="Table1" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Agent</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navAgent" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Fix -->
    <div class="tabdisplayGatePass" id="tabAgent" style="overflow-y: auto; overflow-x: auto;
        height: auto">
        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
            <tr>
                <td>
                    <table border="0" width="100%" cellpadding="2" align="left" cellspacing="2" class="tblstd"
                        style="width: 100%;">
                        <tr>
                            <td>
                                <label id="lblAgentCode" runat="server" class="lbl">
                                    Agent Code
                                </label>
                                <Inp:iLabel ID="AgentCode" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtAgentCode" runat="server" CssClass="txt" MaxLength="10" TabIndex="1"
                                    HelpText="567,AGENT_AGNT_CD" TextMode="SingleLine" iCase="Upper" ToolTip="Agent Code">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Agent Code allows only alphabets and numbers"
                                        RegularExpression="^[a-zA-Z0-9]+$" Type="String" Validate="True" ValidationGroup="divGeneralInfo"
                                        LookupValidation="False" CsvErrorMessage="This Code Already Exists" CustomValidationFunction="validateAgentCode"
                                        IsRequired="true" ReqErrorMessage="Agent Code Required" CustomValidation="true"
                                        RegexValidation="true" />
                                </Inp:iTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <label id="lblAgentname" runat="server" class="lbl">
                                    Agent Name
                                </label>
                                <Inp:iLabel ID="AgentName" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtAgentName" runat="server" CssClass="txt" MaxLength="15" TabIndex="2"
                                    HelpText="568,AGENT_AGNT_NAM" TextMode="SingleLine" iCase="Upper" ToolTip="Agent Name">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                        ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                        CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Agent Name Required"
                                        CustomValidation="false" RegexValidation="false" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                                        RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" />
                                </Inp:iTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <Inp:iLabel ID="lblAgentCurrency" runat="server" CssClass="lbl">
                                                Currency
                                </Inp:iLabel>
                                <Inp:iLabel ID="AgentCurrency" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpAgentCurrency" runat="server" CssClass="lkp" DataKey="CRRNCY_CD"
                                    DoSearch="True" iCase="Upper" MaxLength="3" TabIndex="3" TableName="2" HelpText="589,CURRENCY_CRRNCY_CD"
                                    ClientFilterFunction="" OnClientTextChange="" ToolTip="Agent Currency" AllowSecondaryColumnSearch="true"
                                    SecondaryColumnName="CRRNCY_DSCRPTN_VC">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="CRRNCY_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="CRRNCY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="CRRNCY_DSCRPTN_VC" Hidden="false" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                        IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="true" ReqErrorMessage="Agent's Currency Required"
                                        RegErrorMessage="Agent Currency Required" LkpErrorMessage="Invalid Agent Currency. Click on the list for valid values"
                                        Operator="Equal" Type="String" Validate="true" ValidationGroup="divGeneralInfo"
                                        CustomValidation="false" CustomValidationFunction="" />
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
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <Inp:iLabel ID="lblActive" runat="server" CssClass="lbl" ToolTip="Active Bit">
                                    Active
                                </Inp:iLabel>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActiveBit" runat="server" Text="" CssClass="chk" ToolTip="Active Bit"
                                    TabIndex="6" />
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="tab">
                        <Nav:iTab ID="ITab1" runat="server" ControlHeight="" ControlWidth="" SelectedTab="tabGeneralInfo"
                            TabHeight="" TabWidth="" TabsPerRow="6" EnableClose="False">
                            <TabPages>
                                <Nav:TabPage ID="tabGeneralInfo" runat="server" Caption="General Info" doValidate="true"
                                    OnAfterSelect="onAfterSelect('0');" OnBeforeSelect="onBeforeSelect('0');" TabPageClientId="divGeneralInfo">
                                </Nav:TabPage>
                                <Nav:TabPage ID="tabAgentRates" runat="server" Caption="Agent Rates" doValidate="true"
                                    OnAfterSelect="onAfterSelect('1');" OnBeforeSelect="onBeforeSelect('1');" TabPageClientId="divAgentRates">
                                </Nav:TabPage>
                            </TabPages>
                        </Nav:iTab>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divGeneralInfo" style="height: 150px;">
                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td>
                                    <Inp:iLabel ID="lblContactPersonName" runat="server" CssClass="lbl">
                                                    Contact Person
                                    </Inp:iLabel>
                                    <Inp:iLabel ID="ContactPersonName" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtContactPersonName" runat="server" CssClass="txt" TabIndex="7"
                                        HelpText="569,AGENT_AGNT_CNTCT_PRSN_NAM" TextMode="SingleLine" iCase="Upper"
                                        ToolTip="Contact Person Name">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                            CustomValidationFunction="" IsRequired="true" ReqErrorMessage="Contact Person Name Required"
                                            CustomValidation="false" RegexValidation="true" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" />
                                    </Inp:iTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <Inp:iLabel ID="lblContactAddress" runat="server" CssClass="lbl">
                                                    Contact Address
                                    </Inp:iLabel>
                                    <Inp:iLabel ID="ContactAddress" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtContactAddress" runat="server" CssClass="txt" TabIndex="8" HelpText="570,AGENT_AGNT_CNTCT_ADDRSS"
                                        TextMode="MultiLine" iCase="None" ToolTip="Contact Address" MaxLength="1000">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                            CustomValidationFunction="" IsRequired="true" ReqErrorMessage="Contact Address Required"
                                            CustomValidation="false" RegexValidation="false" RegErrorMessage="Single & Double quotes are not Allowed"
                                            RegularExpression="^[a-zA-Z0-9@+.!#$&quot;,:;=/\(\),\-\s]{1,255}$" />
                                    </Inp:iTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <Inp:iLabel ID="lblBillingAddress" runat="server" CssClass="lbl">
                                                    Billing Address
                                    </Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtBillingAddress" runat="server" CssClass="txt" TabIndex="9" HelpText="571,AGENT_AGNT_BLLNG_ADDRSS"
                                        TextMode="MultiLine" iCase="None" ToolTip="Billing Address" MaxLength="1000"
                                        OnClientTextChange="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                            CustomValidationFunction="validateAddress" IsRequired="false" ReqErrorMessage="Billing Address Required"
                                            CustomValidation="false" RegexValidation="false" RegErrorMessage="Single & Double quotes are not Allowed"
                                            RegularExpression="[^']*\&quot;" />
                                    </Inp:iTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblZipCode" runat="server" class="lbl">
                                        Zip Code
                                    </label>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtZipCode" runat="server" CssClass="txt" MaxLength="100" TabIndex="10"
                                        HelpText="572,AGENT_AGNT_ZP_CD" TextMode="SingleLine" iCase="None" ToolTip="Zip Code">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Zip Code Required"
                                            CustomValidation="false" RegErrorMessage="Invalid Zip Code [a-z, A-Z, 0-9,-,_,+,Space Characters are Allowed]"
                                            RegexValidation="true" RegularExpression="^[a-zA-Z0-9-_+ ]+$" />
                                    </Inp:iTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <label id="lblPhoneNo" runat="server" class="lbl">
                                        Phone No
                                    </label>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtPhoneNo" runat="server" CssClass="txt" MaxLength="20" TabIndex="11"
                                        HelpText="573,AGENT_AGNT_PHN_NO" TextMode="SingleLine" iCase="Upper" ToolTip="Phone Number">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Phone Number Required"
                                            CustomValidation="false" RegexValidation="true" RegErrorMessage="Invalid Phone Number [a-z, A-Z, 0-9,-,_,+,Space Characters are Allowed]"
                                            RegularExpression="^[a-zA-Z0-9-_+ ]+$" />
                                    </Inp:iTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <label id="lblFaxNo" runat="server" class="lbl">
                                        Fax</label>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtFax" runat="server" CssClass="txt" MaxLength="100" TabIndex="12"
                                        HelpText="574,AGENT_AGNT_FX_NO" TextMode="SingleLine" iCase="Upper" ToolTip="Fax Number">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                                            ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Fax Required"
                                            CustomValidation="false" RegexValidation="true" RegErrorMessage="Invalid Fax Number [a-z, A-Z, 0-9,-,_,+,Space Characters are Allowed]"
                                            RegularExpression="^[a-zA-Z0-9-_+ ]+$" />
                                    </Inp:iTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblEmailforInvoicing" runat="server" class="lbl">
                                        Email for Invoicing</label>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtEmailforInvoicing" runat="server" CssClass="txt" TabIndex="14"
                                        HelpText="575,AGENT_AGNT_INVCNG_EML_ID" TextMode="MultiLine" iCase="None" ToolTip="Email for Invoicing"
                                        MaxLength="1000">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Email for Invoicing Required"
                                            CustomValidation="false" RegexValidation="true" RegErrorMessage="Invalid Email Format"
                                            RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$" />
                                    </Inp:iTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divAgentRates">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <iFg:iFlexGrid ID="ifgChargeDetail" runat="server" AllowStaticHeader="True" DataKeyNames="AGNT_CHRG_DTL_ID"
                                        Width="800px" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                        Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="130px" Type="Normal"
                                        ValidationGroup="divAgentRates" UseCachedDataSource="True" AutoGenerateColumns="False"
                                        OnClientExpand="onClientExpand" EnableViewState="False" OnAfterClientRowCreated="setDefaultValues"
                                        OnAfterCallBack="parentOnAfterCallBack" OnBeforeCallBack="parentOnBeforeCallBack"
                                        Mode="Insert" AllowPaging="false" AutoSearch="True" UseIcons="True" SearchButtonIconClass="icon-search"
                                        SearchButtonCssClass="btn btn-small btn-primary" AddButtonIconClass="icon-plus"
                                        AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                                        DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                                        RefreshButtonCssClass="btn btnmcorner btn-small btn-primary" SearchCancelButtonIconClass="icon-remove"
                                        ClearButtonCssClass="icon-eraser">
                                        <RowStyle CssClass="gitem" />
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                        <Columns>
                                            <iFg:ExpandableField GridId="ifgStorageDetail" HeaderTitle="" SortAscUrl="" SortDescUrl="">
                                                <ItemStyle Width="10px" />
                                            </iFg:ExpandableField>
                                            <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type *"
                                                HeaderTitle="Equipment Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                                HeaderStyle-Width="50px">
                                                <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="582,EQUIPMENT_TYPE_EQPMNT_TYP_CD"
                                                    iCase="Upper" OnClientTextChange="" ValidationGroup="divAgentRates" MaxLength="1"
                                                    TableName="3" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction=""
                                                    AllowSecondaryColumnSearch="false" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                                                    <LookupColumns>
                                                        <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                                        <Inp:LookupColumn ColumnCaption="Type" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False" ControlToBind="2" />
                                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                                            Hidden="False" />
                                                    </LookupColumns>
                                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                        LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                                                        CustomValidation="false" CustomValidationFunction="" ReqErrorMessage="Equipment Type Required"
                                                        Validate="True" ValidationGroup="divAgentRates" />
                                                </Lookup>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="80px" Wrap="True" />
                                            </iFg:LookupField>
                                            <iFg:TextboxField DataField="EQPMNT_CD_CD" HeaderText="Code" HeaderTitle="Code" SortAscUrl=""
                                                ReadOnly="true" SortDescUrl="">
                                                <TextBox CssClass="txt" HelpText="" OnClientTextChange="" ValidationGroup="" ReadOnly="true">
                                                    <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="Equal" ReqErrorMessage=" "
                                                        Type="Double" />
                                                </TextBox>
                                                <ItemStyle Width="80px" Wrap="true" />
                                            </iFg:TextboxField>
                                            <iFg:TextboxField DataField="HNDLNG_IN_CHRG_NC" CharacterLimit="0" HeaderText="Handling In Charges"
                                                HeaderTitle="Handling In Charges" SortAscUrl="" SortDescUrl="">
                                                <TextBox CssClass="ntxto" HelpText="581,AGENT_CHARGE_DETAIL_HNDLNG_IN_CHRG_NC" iCase="Numeric"
                                                    OnClientTextChange="formatRate" ValidationGroup="divAgentRates">
                                                    <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" ReqErrorMessage="Handling In Charges Required"
                                                        Type="Double" CustomValidationFunction="" CustomValidation="false" CsvErrorMessage="Exist"
                                                        Validate="True" RegErrorMessage="Invalid Handling In Charge. Range must be from 0.00 to 9999999999.99"
                                                        RegexValidation="true" RegularExpression="^\d{1,10}(\.\d{1,2})?$" />
                                                </TextBox>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="80px" Wrap="true" HorizontalAlign="Right" />
                                            </iFg:TextboxField>
                                            <iFg:TextboxField DataField="HNDLNG_OUT_CHRG_NC" CharacterLimit="0" HeaderText="Handling Out Charges"
                                                HeaderTitle="Handling Out Charges" SortAscUrl="" SortDescUrl="">
                                                <TextBox CssClass="ntxto" HelpText="583,AGENT_CHARGE_DETAIL_HNDLNG_OUT_CHRG_NC" iCase="Numeric"
                                                    OnClientTextChange="formatRate" ValidationGroup="divAgentRates">
                                                    <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" ReqErrorMessage="Handling Out Charges Required"
                                                        Type="Double" CustomValidationFunction="" CustomValidation="false" CsvErrorMessage="Exist"
                                                        ValidationGroup="divAgentRates" Validate="True" RegErrorMessage="Invalid Handling Out Charge Amount. Range must be from 0.00 to 9999999999.99"
                                                        RegexValidation="true" RegularExpression="^\d{1,10}(\.\d{1,2})?$" />
                                                </TextBox>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="80px" Wrap="true" HorizontalAlign="Right" />
                                            </iFg:TextboxField>
                                            <iFg:TextboxField DataField="INSPCTN_CHRGS" CharacterLimit="0" HeaderText="Inspection Charges"
                                                HeaderTitle="Inspection Charges" SortAscUrl="" SortDescUrl="">
                                                <TextBox CssClass="ntxto" HelpText="584,AGENT_CHARGE_DETAIL_INSPCTN_CHRGS" iCase="Numeric"
                                                    OnClientTextChange="formatRate" ValidationGroup="divAgentRates">
                                                    <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" ReqErrorMessage="Inspection Charges"
                                                        Type="Double" CustomValidationFunction="" CustomValidation="false" CsvErrorMessage="Exist"
                                                        ValidationGroup="divAgentRates" Validate="True" RegErrorMessage="Invalid Inspection Charges. Range must be from 0.00 to 9999999999.99"
                                                        RegexValidation="true" RegularExpression="^\d{1,10}(\.\d{1,2})?$" />
                                                </TextBox>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="80px" Wrap="true" HorizontalAlign="Right" />
                                            </iFg:TextboxField>
                                            <iFg:CheckBoxField DataField="ACTV_BT" HeaderText="Active" HeaderTitle="Active" HelpText="Active"
                                                SortAscUrl="" SortDescUrl="" SortExpression="">
                                                <ItemStyle Width="25px" />
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </iFg:CheckBoxField>
                                        </Columns>
                                        <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                                        <SelectedRowStyle CssClass="gsitem" />
                                        <AlternatingRowStyle CssClass="gaitem" />
                                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                    </iFg:iFlexGrid>
                                    <iFg:iFlexGrid ID="ifgStorageDetail" runat="server" AllowStaticHeader="True" DataKeyNames="AGNT_STRG_DTL_ID"
                                        Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                        Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="80px" Type="Mergeable"
                                        ValidationGroup="divAgentRates" UseCachedDataSource="True" AutoGenerateColumns="False"
                                        EnableViewState="False" OnAfterClientRowCreated="childOnBeforeRowCreated" OnAfterCallBack="childOnAfterCallBack"
                                        OnBeforeCallBack="childOnBeforeCallBack" Mode="Insert" AllowPaging="false" AutoSearch="True"
                                        UseIcons="True" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-primary"
                                        AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                        DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                        RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btnmcorner btn-small btn-primary"
                                        SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser">
                                        <RowStyle CssClass="gitem" />
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                        <Columns>
                                            <iFg:TextboxField DataField="AGNT_UP_TO_DYS" CharacterLimit="0" HeaderText="Up to Days"
                                                HeaderTitle="Up to Days" SortAscUrl="" SortDescUrl="">
                                                <TextBox CssClass="txt" HelpText="585,AGENT_STORAGE_DETAIL_AGNT_UP_TO_DYS" iCase="Numeric"
                                                    OnClientTextChange="" ValidationGroup="divAgentRates">
                                                    <Validator CustomValidateEmptyText="False" IsRequired="true" Operator="Equal" ReqErrorMessage="Up to Days Required and It should be greater than previous Days."
                                                        Type="String" CustomValidationFunction="validateUpToDays" CustomValidation="true"
                                                        CsvErrorMessage="Exist" ValidationGroup="divAgentRates" RegexValidation="true"
                                                        RegularExpression="^[0-9]+$" RegErrorMessage="Invalid Up to Days. Up to Days must be a round value"
                                                        Validate="True" />
                                                </TextBox>
                                                <HeaderStyle Width="150px" />
                                                <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Right" />
                                            </iFg:TextboxField>
                                            <iFg:TextboxField DataField="AGNT_STRG_CHRG_NC" CharacterLimit="0" HeaderText="Storage Charge"
                                                HeaderTitle="Storage Charge" SortAscUrl="" SortDescUrl="">
                                                <TextBox CssClass="ntxto" HelpText="586,AGENT_STORAGE_DETAIL_AGNT_STRG_CHRG_NC" iCase="Numeric"
                                                    MaxLength="9" OnClientTextChange="formatRate" ValidationGroup="divAgentRates">
                                                    <Validator CustomValidateEmptyText="False" IsRequired="true" Operator="LessThanEqual"
                                                        ReqErrorMessage="Storage Charge Required" Type="Double" Validate="true" RegErrorMessage="Invalid Storage Charge. Range must be from 0.00 to 9999999.99"
                                                        CustomValidation="false" CustomValidationFunction="" CompareValidation="true"
                                                        ValueToCompare="9999999.99" CmpErrorMessage="Invalid Storage Charge. Range must be from 0.00 to 9999999.99"
                                                        RegexValidation="false" RegularExpression="^(\d{1,7})(.\d{2})?$" ValidationGroup="divAgentRates" />
                                                </TextBox>
                                                <HeaderStyle Width="150px" />
                                                <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Right" />
                                            </iFg:TextboxField>
                                            <iFg:TextboxField DataField="AGNT_RMRKS_VC" CharacterLimit="0" HeaderText="Remarks"
                                                HeaderTitle="Remarks" SortAscUrl="" SortDescUrl="">
                                                <TextBox CssClass="txt" HelpText="587,AGENT_STORAGE_DETAIL_AGNT_RMRKS_VC" iCase="Upper"
                                                    OnClientTextChange="" ValidationGroup="divAgentRates">
                                                    <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" ReqErrorMessage="Remarks Required"
                                                        Type="String" Validate="true" />
                                                </TextBox>
                                                <HeaderStyle Width="200px" />
                                                <ItemStyle Width="150px" Wrap="true" />
                                            </iFg:TextboxField>
                                        </Columns>
                                        <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                                        <SelectedRowStyle CssClass="gsitem" />
                                        <AlternatingRowStyle CssClass="gaitem" />
                                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                    </iFg:iFlexGrid>
                                </td>
                                <td style="width: 30%" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="1" class="tblstd">
                                        <tr>
                                            <td colspan="2">
                                                <label id="Label1" runat="server" class="lbl">
                                                    <strong><u>Taxes</u></strong></label>
                                            </td>
                                        </tr>
                                        <tr style="height:30px">
                                            <td>
                                                <label id="lblHandlingTaxRate" runat="server" class="lbl">
                                                    Handling Tax Rate
                                                </label>
                                            </td>
                                            <td>
                                                <Inp:iTextBox ID="txtHndlng_Tx_Rt" runat="server" CssClass="ntxt" MaxLength="100"
                                                    OnClientTextChange="formatHandlingRate" iCase="Numeric" TabIndex="18" HelpText="577,AGENT_AGNT_HANDLNG_TX"
                                                    TextMode="SingleLine" ToolTip="Handling Tax Rate">
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                        ValidationGroup="divAgentRates" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                                        IsRequired="False" ReqErrorMessage="Handling Tax Rate Required" CustomValidation="false"
                                                        RegErrorMessage="Invalid Handling Tax Rate. Rate can be upto 14 digits and 2 decimal points"
                                                        RegexValidation="True" RegularExpression="^(\+)?\d{1,14}(\.\d{1,2})?$" />
                                                </Inp:iTextBox>
                                            </td>
                                        </tr>
                                       
                                        <tr  style="height:30px">
                                            <td>
                                                <label id="lblStorageTaxRate" runat="server" class="lbl">
                                                    Storage Tax Rate
                                                </label>
                                            </td>
                                            <td>
                                                <Inp:iTextBox ID="txtStorage_Tx_Rt" runat="server" CssClass="ntxt" MaxLength="100"
                                                    TabIndex="19" HelpText="578,AGENT_AGNT_STORG_TX" TextMode="SingleLine" iCase="Numeric"
                                                    OnClientTextChange="formatStorageRate" ToolTip="Storage Tax Rate">
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                        ValidationGroup="divAgentRates" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                                        IsRequired="False" ReqErrorMessage="Storage Tax Rate Required" CustomValidation="false"
                                                        RegErrorMessage="Invalid Storage Tax Rate. Rate can be upto 14 digits and 2 decimal points"
                                                        RegexValidation="True" RegularExpression="^(\+)?\d{1,14}(\.\d{1,2})?$" />
                                                </Inp:iTextBox>
                                            </td>
                                        </tr>
                                        <tr style="height:30px">
                                            <td>
                                                <label id="lblServiceTaxRate" runat="server" class="lbl">
                                                    Service Tax
                                                </label>
                                            </td>
                                            <td>
                                                <Inp:iTextBox ID="txtSrvc_Tx_Rt" runat="server" CssClass="ntxt" MaxLength="100" TabIndex="20"
                                                    OnClientTextChange="formatServiceRate" HelpText="579,AGENT_AGNT_SERVC_TX" TextMode="SingleLine"
                                                    iCase="Numeric" ToolTip="Service Tax (%)">
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                        ValidationGroup="divAgentRates" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                                        IsRequired="False" ReqErrorMessage="Service Tax Percentage Required" CustomValidation="false"
                                                        RegErrorMessage="Invalid Service Tax Percentage. Percentage can be upto 2 digits and 2 decimal points"
                                                        RegexValidation="True" RegularExpression="^\d{1,2}(\.\d{1,2})?$" />
                                                </Inp:iTextBox>
                                            </td>
                                        </tr>
                                        <tr style="height:30px">
                                            <td>
                                                <label id="lblLaborRate" runat="server" class="lbl">
                                                    Labor Rate Per Hour
                                                </label>
                                                <Inp:iLabel ID="lblLbrRtReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                            </td>
                                            <td>
                                                <div id="divLaborRate">
                                                    <Inp:iTextBox ID="txtLbr_rt_pr_hr" runat="server" CssClass="ntxt" MaxLength="100"
                                                        OnClientTextChange="" TabIndex="21" HelpText="580,AGENT_AGNT_LBR_RT_PR_HR" TextMode="SingleLine"
                                                        iCase="Numeric" ToolTip="Labor Rate">
                                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                            ValidationGroup="divLaborRate" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="formatLaborRate"
                                                            IsRequired="true" ReqErrorMessage="Labor Rate Per Hour Required" CustomValidation="True"
                                                            RegErrorMessage="Invalid Labor Rate Per Hour. Rate can be upto 14 digits and 2 decimal points"
                                                            RegexValidation="True" RegularExpression="^(\+)?\d{1,14}(\.\d{1,2})?$" />
                                                    </Inp:iTextBox>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="vertical-align: bottom;">
        <Inp:iLabel ID="lblNote" runat="server" Font-Bold="true" CssClass="lbl">Note: The above specified rates are in Depot Local Currency</Inp:iLabel>
        <Inp:iLabel ID="lblDepotCuurency" CssClass="lbl" runat="server" Font-Bold="true"></Inp:iLabel>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" align="center"
        onClientPrint="null" />
    <asp:HiddenField ID="hdnNoOfAgents" runat="server" />
    <asp:HiddenField ID="hdnAgentId" runat="server" />
    <asp:HiddenField ID="hdnAgentCode" runat="server" />
    <asp:HiddenField ID="hdnAgentChargeDetailID" runat="server" />
    <asp:HiddenField ID="hdnDepotCurrency" runat="server" />
    </form>
</body>
</html>
