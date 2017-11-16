<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Customer.aspx.vb" Inherits="Masters_Customer" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer</title>
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table id="Table1" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Customer</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navCustomer" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Fix -->
    <div class="tabdisplayGatePass" id="tabCustomer" style="overflow-y: auto; overflow-x: auto;
        height: auto">
        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
            <tr>
                <td>
                    <table border="0" width="100%" cellpadding="2" align="left" cellspacing="2" class="tblstd"
                        style="width: 100%;">
                        <tr>
                            <td>
                                <label id="lblCustomerCode" runat="server" class="lbl">
                                    Customer Code
                                </label>
                                <Inp:iLabel ID="CustomerCode" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtCustomerCode" runat="server" CssClass="txt" MaxLength="10" TabIndex="1"
                                    HelpText="280,CUSTOMER_CSTMR_CD" TextMode="SingleLine" iCase="Upper" ToolTip="Customer Code">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Customer Code allows only alphabets and numbers"
                                        RegularExpression="^[a-zA-Z0-9]+$" Type="String" Validate="True" ValidationGroup="divGeneralInfo"
                                        LookupValidation="False" CsvErrorMessage="This Code Already Exists" CustomValidationFunction="validateCustomerCode"
                                        IsRequired="true" ReqErrorMessage="Customer Code Required" CustomValidation="true"
                                        RegexValidation="true" />
                                </Inp:iTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <label id="lblCustomername" runat="server" class="lbl">
                                    Customer Name
                                </label>
                                <Inp:iLabel ID="CustomerName" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtCustomerName" runat="server" CssClass="txt" MaxLength="15" TabIndex="2"
                                    HelpText="281,CUSTOMER_CSTMR_NAM" TextMode="SingleLine" iCase="Upper" ToolTip="Customer Name">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                        ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                        CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Customer Name Required"
                                        CustomValidation="false" RegexValidation="false" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                                        RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" />
                                </Inp:iTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <Inp:iLabel ID="lblCustomerCurrency" runat="server" CssClass="lbl">
                                                Currency
                                </Inp:iLabel>
                                <Inp:iLabel ID="CustomerCurrency" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpCustomerCurrency" runat="server" CssClass="lkp" DataKey="CRRNCY_CD"
                                    DoSearch="True" iCase="Upper" MaxLength="3" TabIndex="3" TableName="2" HelpText="293,CURRENCY_CRRNCY_CD"
                                    ClientFilterFunction="" OnClientTextChange="" ToolTip="Customer Currency" AllowSecondaryColumnSearch="true"
                                    SecondaryColumnName="CRRNCY_DSCRPTN_VC">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="CRRNCY_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="CRRNCY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="CRRNCY_DSCRPTN_VC" Hidden="false" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                        IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="true" ReqErrorMessage="Customer's Currency Required"
                                        RegErrorMessage="Customer Currency Required" LkpErrorMessage="Invalid Customer Currency. Click on the list for valid values"
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
                                <Inp:iLabel ID="lblTransportationBit" runat="server" Text="Transportation" CssClass="lbl">
                                Transportation
                                </Inp:iLabel>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkTransportationBit" runat="server" Text="" CssClass="chk" ToolTip="Transportation Bit"
                                    TabIndex="4" />
                            </td>
                            <td>
                            </td>
                            <td>
                                <Inp:iLabel ID="lblRentalBit" runat="server" Text="Rental" CssClass="lbl">
                                    Rental
                                </Inp:iLabel>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkRentalBit" runat="server" Text="" CssClass="chk" ToolTip="Rental Bit"
                                    TabIndex="5" />
                            </td>
                            <td>
                            </td>
                            <%--  <td>
                            <Inp:iLabel ID="lblXmlBit" runat="server" Text="Rental" CssClass="lbl">
                                    EDI
                                </Inp:iLabel>
                            </td>
                            <td>
                            <asp:CheckBox ID="chkXML" runat="server" Text="" CssClass="chk" ToolTip="EDI Bit"
                                    TabIndex="5" />
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                <Inp:iLabel ID="lblXmlBit" runat="server" Text="EDI" CssClass="lbl">
                                    EDI
                                </Inp:iLabel>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkXML" runat="server" Text="" CssClass="chk" ToolTip="EDI Bit"
                                    TabIndex="6" />
                            </td>
                            <td>
                            </td>
                            <td>
                                <div id="divlblLedger">
                                    <Inp:iLabel ID="lblLedgerId" runat="server" ToolTip="*" CssClass="lbl">
                                               Ledger Id
                                    </Inp:iLabel>
                                    <Inp:iLabel ID="lblLedgerIdReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                </div>
                            </td>
                            <td>
                                <div id="divtxtLedger">
                                    <Inp:iTextBox ID="txtLedgerId" runat="server" CssClass="txt" TabIndex="7" HelpText=""
                                        MaxLength="4" TextMode="SingleLine" iCase="Upper" ToolTip="Enter the Ledger Id for Customer">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" CustomValidation="false"
                                            RegexValidation="true" RegErrorMessage="Only Alphanumeric Character Allowed"
                                            RegularExpression="^[0-9a-zA-Z]+$" />
                                    </Inp:iTextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="11">
                                <a id="hypEmailSetting" href="#" onclick="return openEmailSetting();">Email Setting</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="divCustomerDetail">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <%--<tr>--%>
                    <td>
                        <div class="tab">
                            <Nav:iTab ID="ITab1" runat="server" ControlHeight="" ControlWidth="" SelectedTab="tabGeneralInfo"
                                TabHeight="" TabWidth="" TabsPerRow="6" EnableClose="False">
                                <TabPages>
                                    <Nav:TabPage ID="tabGeneralInfo" runat="server" Caption="General Info" doValidate="true"
                                        OnAfterSelect="onAfterSelect('0');" OnBeforeSelect="onBeforeSelect('0');" TabPageClientId="divGeneralInfo">
                                    </Nav:TabPage>
                                    <Nav:TabPage ID="tabStandardRates" runat="server" Caption="Standard Rates" doValidate="true"
                                        OnAfterSelect="onAfterSelect('1');" OnBeforeSelect="onBeforeSelect('1');" TabPageClientId="divStandardRates">
                                    </Nav:TabPage>
                                    <Nav:TabPage ID="tabCustomerRates" runat="server" Caption="Customer Rates" doValidate="true"
                                        OnAfterSelect="onAfterSelect('2');" OnBeforeSelect="onBeforeSelect('2');" TabPageClientId="divCustomerRates">
                                    </Nav:TabPage>
                                    <Nav:TabPage ID="tabTransportation" runat="server" Caption="Transportation" doValidate="true"
                                        OnAfterSelect="onAfterSelect('3');" OnBeforeSelect="onBeforeSelect('3');" TabPageClientId="divTransportation">
                                    </Nav:TabPage>
                                    <Nav:TabPage ID="tabRental" runat="server" Caption="Rental" doValidate="true" OnAfterSelect="onAfterSelect('4');"
                                        OnBeforeSelect="onBeforeSelect('4');" TabPageClientId="divRental">
                                    </Nav:TabPage>
                                    <Nav:TabPage ID="tabFtp" runat="server" Caption="FTP Credentials" doValidate="true"
                                        OnAfterSelect="onAfterSelect('5');" OnBeforeSelect="onBeforeSelect('5');" TabPageClientId="divFtp">
                                    </Nav:TabPage>
                                </TabPages>
                            </Nav:iTab>
                        </div>
                        <%--<div id="divGeneralInfo" class="" style="height: 220px;">--%>
                        <div id="divGeneralInfo" style="height: 166px;">
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
                                            HelpText="282,CUSTOMER_CNTCT_PRSN_NAM" TextMode="SingleLine" iCase="Upper" ToolTip="Contact Person Name">
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
                                        <Inp:iTextBox ID="txtContactAddress" runat="server" CssClass="txt" TabIndex="8" HelpText=""
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
                                        <Inp:iTextBox ID="txtBillingAddress" runat="server" CssClass="txt" TabIndex="9" HelpText=""
                                            TextMode="MultiLine" iCase="None" ToolTip="Billing Address" MaxLength="1000"
                                            OnClientTextChange="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                                CustomValidationFunction="validateAddress" IsRequired="false" ReqErrorMessage="Billing Address Required"
                                                CustomValidation="false" RegexValidation="false" RegErrorMessage="Single & Double quotes are not Allowed"
                                                RegularExpression="[^'"]*" />
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
                                            HelpText="285,CUSTOMER_ZP_CD" TextMode="SingleLine" iCase="None" ToolTip="Zip Code">
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
                                            HelpText="286,CUSTOMER_PHN_NO" TextMode="SingleLine" iCase="None" ToolTip="Phone Number">
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
                                            HelpText="287,CUSTOMER_FX_NO" TextMode="SingleLine" iCase="None" ToolTip="Fax Number">
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
                                        <label id="lblEmailforReporting" runat="server" class="lbl">
                                            Email for Reporting</label>
                                    </td>
                                    <td>
                                        <Inp:iTextBox ID="txtEmailforReporting" runat="server" CssClass="txt" TabIndex="13"
                                            HelpText="288,CUSTOMER_RPRTNG_EML_ID" TextMode="MultiLine" iCase="None" ToolTip="Email for Reporting"
                                            MaxLength="1000">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                                CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Email for Reporting Required"
                                                CustomValidation="false" RegexValidation="true" RegErrorMessage="Invalid Email Format"
                                                RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$" />
                                        </Inp:iTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <label id="lblEmailforInvoicing" runat="server" class="lbl">
                                            Email for Invoicing</label>
                                    </td>
                                    <td>
                                        <Inp:iTextBox ID="txtEmailforInvoicing" runat="server" CssClass="txt" TabIndex="14"
                                            HelpText="289,CUSTOMER_INVCNG_EML_ID" TextMode="MultiLine" iCase="None" ToolTip="Email for Invoicing"
                                            MaxLength="1000">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                                CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Email for Invoicing Required"
                                                CustomValidation="false" RegexValidation="true" RegErrorMessage="Invalid Email Format"
                                                RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$" />
                                        </Inp:iTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <label id="lblEmailforRepairTech" runat="server" class="lbl">
                                            Email for Repair Tech</label>
                                    </td>
                                    <td>
                                        <Inp:iTextBox ID="txtEmailforRepairTech" runat="server" CssClass="txt" TabIndex="15"
                                            HelpText="290,CUSTOMER_RPR_TCH_EML_ID" TextMode="MultiLine" iCase="None" ToolTip="Email for Repair Tech"
                                            MaxLength="1000">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                                CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Email for Repair Tech Required"
                                                CustomValidation="false" RegexValidation="true" RegErrorMessage="Invalid Email Format"
                                                RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$" />
                                        </Inp:iTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label id="lblBulkEmailFormatID" runat="server" class="lbl">
                                            Bulk Email Format</label>
                                        <Inp:iLabel ID="ILabel1" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iLookup ID="lkpBulkEmailFormat" runat="server" CssClass="lkp" DataKey="ENM_CD"
                                            ToolTip="Bulk Email Format" DoSearch="True" iCase="None" MaxLength="10" TabIndex="16"
                                            TableName="47" HelpText="291,ENUM_ENM_CD" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                            SecondaryColumnName="">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Bulk Email Format" />
                                            </LookupColumns>
                                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                                                HorizontalAlign="Left" />
                                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="true" LkpErrorMessage="Invalid Bulk Email Format. Click on the list for valid values"
                                                Operator="Equal" ReqErrorMessage="Bulk Email Format Required" Type="String" ValidationGroup="divGeneralInfo"
                                                Validate="True" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        </Inp:iLookup>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <label id="lblBillingType" runat="server" class="lbl">
                                            Billing Type</label>
                                    </td>
                                    <td>
                                        <Inp:iLookup ID="lkpBillingType" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                                            iCase="Upper" MaxLength="10" TabIndex="17" TableName="8" HelpText="292,ENUM_ENM_CD"
                                            ClientFilterFunction="" AllowSecondaryColumnSearch="false" ToolTip="Billing Type"
                                            SecondaryColumnName="">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Billing Type" />
                                            </LookupColumns>
                                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top" />
                                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                                                LkpErrorMessage="Invalid Billing Type. Click on the list for valid values" Operator="Equal"
                                                ReqErrorMessage="" Type="String" ValidationGroup="divGeneralInfo" Validate="True" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        </Inp:iLookup>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblCheckDigitValidation" runat="server" CssClass="lbl" ToolTip="Check Digit Validation Bit">
                                    Check Digit Validation
                                        </Inp:iLabel>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkCheckDigitValidationBit" runat="server" Text="" CssClass="chk"
                                            ToolTip="Check Digit Validation Bit" TabIndex="18" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <Inp:iLabel ID="lblEDICode" runat="server" CssClass="lbl">
                                               EDI Code
                                        </Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iTextBox ID="txtEdiCode" runat="server" CssClass="txt" TabIndex="19" HelpText=""
                                            MaxLength="9" TextMode="SingleLine" iCase="Upper" ToolTip="EDI Code">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                                CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" CustomValidation="false"
                                                RegexValidation="true" RegErrorMessage="Only Alphanumeric Character Allowed"
                                                RegularExpression="^[0-9a-zA-Z]+$" />
                                        </Inp:iTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblCustVatNo" Enabled="True" runat="server" CssClass="lbl" Visible="True">
                                               Customer VAT No
                                        </Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iTextBox ID="txtCustVatNo" runat="server" CssClass="txt" TabIndex="20" HelpText=""
                                            MaxLength="9" TextMode="SingleLine" iCase="Upper" 
                                            ToolTip="Customer VAT No">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                ValidationGroup="divGeneralInfo" LookupValidation="False" CsvErrorMessage=""
                                                CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" CustomValidation="false"
                                                RegexValidation="true" RegErrorMessage="Only Alphanumeric Character Allowed"
                                                RegularExpression="^[0-9a-zA-Z]+$" />
                                        </Inp:iTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblAgent" runat="server" CssClass="lbl">Agent
                                        </Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iLookup ID="lkpAgent" runat="server" CssClass="lkp" DataKey="AGNT_CD" DoSearch="True"
                                            iCase="Upper" MaxLength="10" TabIndex="21" TableName="93" HelpText="588,AGENT_AGNT_CD"
                                            ClientFilterFunction="" AllowSecondaryColumnSearch="false" ToolTip="Agent" 
                                            SecondaryColumnName="">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="AGNT_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnName="AGNT_CD" ControlToBind="" Hidden="False" ColumnCaption="Agent Code" />
                                                <Inp:LookupColumn ColumnName="AGNT_NAM" ControlToBind="" Hidden="False" ColumnCaption="Agent Name" />
                                            </LookupColumns>
                                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top" />
                                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                                                LkpErrorMessage="Invalid Agent Code. Click on the list for valid values" Operator="Equal"
                                                ReqErrorMessage="" Type="String" ValidationGroup="divGeneralInfo" Validate="True" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        </Inp:iLookup>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divStandardRates" style="height: 166px;">
                            <div id="divStdRate">
                                <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;" class="tblstd">
                                    <tr>
                                        <%-- <td colspan="8">
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <strong><u>Rates </u></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <Inp:iLabel ID="lblPIRHyrdo" runat="server" CssClass="lbl">
                                               Per. Inspection Rate (Hydro)
                                            </Inp:iLabel>
                                        </td>
                                        <td>
                                            <Inp:iTextBox ID="txtHydroAmount" runat="server" CssClass="ntxt" MaxLength="10" TabIndex="21"
                                                OnClientTextChange="formatRate" HelpText="295,CUSTOMER_HYDR_AMNT_NC" TextMode="SingleLine"
                                                iCase="Numeric" ToolTip="Per. Inspection Rate Hydro Amount">
                                                <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" RegErrorMessage="Invalid Per. Inspection Rate (Hydro). Range must be from 0.00 to 9999999999.99"
                                                    RegularExpression="^\d{1,18}(\.\d{1,2})?$" Type="Double" Validate="True" ValidationGroup="divStandardRates"
                                                    LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="false"
                                                    ReqErrorMessage="Rate Required" CustomValidation="false" RegexValidation="false"
                                                    CompareValidation="true" ValueToCompare="9999999999.99" CmpErrorMessage="Invalid Per. Inspection Rate (Hydro). Range must be from 0.00 to 9999999999.99" />
                                            </Inp:iTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <Inp:iLabel ID="lblPIRPneumatic" runat="server" CssClass="lbl">
                                                Per. Inspection Rate (Pneumatic)
                                            </Inp:iLabel>
                                        </td>
                                        <td>
                                            <Inp:iTextBox ID="txtPneumaticAmount" runat="server" CssClass="ntxt" MaxLength="10"
                                                OnClientTextChange="formatRate" TabIndex="22" HelpText="296,CUSTOMER_PNMTC_AMNT_NC"
                                                TextMode="SingleLine" iCase="Numeric" ToolTip="Per. Inspection Rate Pneumatic Amount">
                                                <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" RegErrorMessage="Invalid Per. Inspection Rate (Pneumatic). Range must be from 0.00 to 9999999999.99"
                                                    RegularExpression="^\d{1,18}(\.\d{1,2})?$" Type="Double" Validate="True" ValidationGroup="divStandardRates"
                                                    LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="false"
                                                    ReqErrorMessage="" CustomValidation="false" RegexValidation="false" CompareValidation="true"
                                                    ValueToCompare="9999999999.99" CmpErrorMessage="Invalid Per. Inspection Rate (Pneumatic). Range must be from 0.00 to 9999999999.99" />
                                            </Inp:iTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <Inp:iLabel ID="lblLaborRateperHour" runat="server" CssClass="lbl">
                                               Labor Rate
                                            </Inp:iLabel>
                                        </td>
                                        <td>
                                            <Inp:iTextBox ID="txtLaborRateperHour" runat="server" CssClass="ntxt" MaxLength="10"
                                                OnClientTextChange="formatRate" TabIndex="23" HelpText="297,CUSTOMER_LBR_RT_PR_HR_NC"
                                                TextMode="SingleLine" iCase="Numeric" ToolTip="Labor Rate Per Hour">
                                                <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" RegErrorMessage="Invalid Labor Rate. Range must be from 0.00 to 9999999999.99"
                                                    RegularExpression="^\d{1,18}(\.\d{1,2})?$" Type="Double" Validate="True" ValidationGroup="divStandardRates"
                                                    LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="false"
                                                    ReqErrorMessage="" CustomValidation="false" RegexValidation="false" CompareValidation="true"
                                                    ValueToCompare="9999999999.99" CmpErrorMessage="Invalid Labor Rate. Range must be from 0.00 to 9999999999.99" />
                                            </Inp:iTextBox>
                                        </td>
                </tr>
                <tr>
                    <td>
                        <Inp:iLabel ID="lblLeakTestRate" runat="server" CssClass="lbl">
                                               Leak Test Rate
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtLeakTestRate" runat="server" CssClass="ntxt" MaxLength="10"
                            TabIndex="24" OnClientTextChange="formatRate" HelpText="298,CUSTOMER_LK_TST_RT_NC"
                            TextMode="SingleLine" iCase="Numeric" ToolTip="Leak Test Rate">
                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" RegErrorMessage="Invalid Leak Test Rate. Range must be from 0.00 to 9999999999.99"
                                RegularExpression="^\d{1,18}(\.\d{1,2})?$" Type="Double" Validate="True" ValidationGroup="divStandardRates"
                                LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="false"
                                ReqErrorMessage="" CustomValidation="false" RegexValidation="false" CompareValidation="true"
                                ValueToCompare="9999999999.99" CmpErrorMessage="Invalid Leak Test Rate. Range must be from 0.00 to 9999999999.99" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblPeriodicTestType" runat="server" CssClass="lbl">
                                              Periodic Test Type
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iLookup ID="lkpPeriodicTestType" runat="server" CssClass="lkp" DataKey="ENM_CD"
                            DoSearch="True" iCase="None" MaxLength="10" TabIndex="26" TableName="45" HelpText="300,ENUM_ENM_CD"
                            ClientFilterFunction="" OnClientTextChange="showValidityYear" ToolTip="Periodic Test Type"
                            AllowSecondaryColumnSearch="false" SecondaryColumnName="">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Periodic Test Type" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                                HorizontalAlign="Right" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                RegErrorMessage="Invalid Periodic Test Type" LkpErrorMessage="Invalid Periodic Test Type. Click on the list for valid values"
                                Operator="Equal" Type="String" Validate="True" ValidationGroup="divStandardRates" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblInpValidityPeriodTest" runat="server" CssClass="lbl">
                                             Validity Period of Test(in Years)
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblValidityPeriodTest" runat="server" CssClass="lbl">                                          
                        </Inp:iLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <Inp:iLabel ID="lblSurveyRate" runat="server" CssClass="lbl">
                                              Survey- OnHire/OffHire Rate
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtSurveyRate" runat="server" CssClass="ntxt" MaxLength="10" TabIndex="25"
                            OnClientTextChange="formatRate" HelpText="299,CUSTOMER_SRVY_ONHR_OFFHR_RT_NC"
                            TextMode="SingleLine" iCase="Numeric" ToolTip="Survey- OnHire/OffHire Rate">
                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" RegErrorMessage="Invalid Survey Rate. Range must be from 0.00 to 9999999999.99"
                                RegularExpression="^\d{1,18}(\.\d{1,2})?$" Type="Double" Validate="True" ValidationGroup="divStandardRates"
                                LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="false"
                                ReqErrorMessage="" CustomValidation="false" RegexValidation="false" CompareValidation="true"
                                ValueToCompare="9999999999.99" CmpErrorMessage="Invalid Survey Rate. Range must be from 0.00 to 9999999999.99" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblLeakTest" runat="server" Text="Leak Test" CssClass="lbl">
                                                     Leak Test:
                        </Inp:iLabel>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <Inp:iLabel ID="lblShell" runat="server" Text="Shell" CssClass="lbl">
                                                            Shell
                        </Inp:iLabel>
                        <asp:CheckBox ID="chkShell" runat="server" Text="" CssClass="chk" ToolTip="EDI Bit"
                            TabIndex="25" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    <td>
                        <Inp:iLabel ID="lblSTube" runat="server" Text="S/Tube" CssClass="lbl">
                                                            S/Tube
                        </Inp:iLabel>
                        <asp:CheckBox ID="ChkSTube" runat="server" Text="" CssClass="chk" ToolTip="EDI Bit"
                            TabIndex="25" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="8" class="style1">
                        <strong><u>Heating Tariff </u></strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <Inp:iLabel ID="lblMinHeatingRate" runat="server" CssClass="lbl">
                                                        Min. Heating Rate
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtMinHeatingRate" runat="server" CssClass="ntxt" MaxLength="10"
                            OnClientTextChange="formatRate" TabIndex="27" HelpText="301,CUSTOMER_MIN_HTNG_RT_NC"
                            TextMode="SingleLine" iCase="Numeric" ToolTip="Minimum Heating Rate">
                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" RegErrorMessage="Invalid Minimum Heating Rate. Range must be from 0.00 to 9999999999.99"
                                RegularExpression="^\d{1,18}(\.\d{1,2})?$" Type="Double" Validate="True" ValidationGroup="divStandardRates"
                                LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="false"
                                ReqErrorMessage="" CustomValidation="false" RegexValidation="false" CompareValidation="true"
                                ValueToCompare="9999999999.99" CmpErrorMessage="Invalid Minimum Heating Rate. Range must be from 0.00 to 9999999999.99" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblMinHeatingPeriod" runat="server" CssClass="lbl">
                                                        Min. Heating Period
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtMinHeatingPeriod" runat="server" CssClass="ntxt" MaxLength="10"
                            OnClientTextChange="formatRate" TabIndex="28" HelpText="302,CUSTOMER_MIN_HTNG_PRD_NC"
                            TextMode="SingleLine" iCase="Numeric" ToolTip="Minimum Heating Period">
                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" RegErrorMessage="Invalid Minimum Heating Period. Range must be from 0.00 to 9999999999.99"
                                RegularExpression="^\d{1,18}(\.\d{1,2})?$" Type="Double" Validate="True" ValidationGroup="divStandardRates"
                                LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="false"
                                ReqErrorMessage="" CustomValidation="false" RegexValidation="false" CompareValidation="true"
                                ValueToCompare="9999999999.99" CmpErrorMessage="Invalid Minimum Heating Period. Range must be from 0.00 to 9999999999.99" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblHourlyCharge" runat="server" CssClass="lbl">
                                                        Hourly Charge
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtHourlyCharge" runat="server" CssClass="ntxt" MaxLength="10"
                            TabIndex="29" OnClientTextChange="formatRate" HelpText="303,CUSTOMER_HRLY_CHRG_NC"
                            TextMode="SingleLine" iCase="Numeric" ToolTip="Hourly Charge">
                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" RegErrorMessage="Invalid Hourly Charge. Range must be from 0.00 to 9999999999.99"
                                RegularExpression="^\d{1,18}(\.\d{1,2})?$" Type="Double" Validate="True" ValidationGroup="divStandardRates"
                                LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="false"
                                ReqErrorMessage="" CustomValidation="false" RegexValidation="false" CompareValidation="true"
                                ValueToCompare="9999999999.99" CmpErrorMessage="Invalid Hourly Charge. Range must be from 0.00 to 9999999999.99" />
                        </Inp:iTextBox>
                    </td>
                </tr>
                <%--
                                        </td>
                                    </tr>--%>
            </table>
        </div>
    </div>
    <div id="divCustomerRates" class="" style="height: 150px;">
      <%--  <div id="divCstmrRate">--%>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <iFg:iFlexGrid ID="ifgChargeDetail" runat="server" AllowStaticHeader="True" DataKeyNames="CSTMR_CHRG_DTL_ID"
                            Width="800px" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="130px" Type="Normal"
                            ValidationGroup="divCustomerRates" UseCachedDataSource="True" AutoGenerateColumns="False"
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
                                <iFg:ExpandableField GridId="ifgList" HeaderTitle="" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="10px" />
                                </iFg:ExpandableField>
                                <%--   <iFg:LookupField DataField="EQPMNT_CD_CD" ForeignDataField="EQPMNT_CD_ID" HeaderText="Code *"
                                                        HeaderTitle="Equipment Code" PrimaryDataField="EQPMNT_CD_ID" SortAscUrl="" SortDescUrl="">
                                                        <Lookup DataKey="EQPMNT_CD_CD" DependentChildControls="" HelpText="304,EQUIPMENT_CODE_EQPMNT_CD_CD"
                                                            iCase="Upper" OnClientTextChange="" ValidationGroup="" MaxLength="1" TableName="7"
                                                            CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                                                            SecondaryColumnName="EQPMNT_CD_DSCRPTN_VC">
                                                            <LookupColumns>
                                                                <Inp:LookupColumn ColumnName="EQPMNT_CD_ID" Hidden="True" />
                                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False" />
                                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_CD_DSCRPTN_VC" Hidden="False" />
                                                            </LookupColumns>
                                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px" />
                                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                                LkpErrorMessage="Invalid Equipment Code. Click on the list for valid values"
                                                                ReqErrorMessage="Equipment Code Required" Validate="True" ValidationGroup="divCustomerRates" />
                                                        </Lookup>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <ItemStyle Width="80px" Wrap="True" />
                                                    </iFg:LookupField>--%>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type *"
                                    HeaderTitle="Equipment Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                    HeaderStyle-Width="50px">
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="305,EQUIPMENT_TYPE_EQPMNT_TYP_CD"
                                        iCase="Upper" OnClientTextChange="" ValidationGroup="" MaxLength="1" TableName="3"
                                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                        SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Type" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False" />
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
                                            CustomValidation="true" CustomValidationFunction="setEquipmentCode" ReqErrorMessage="Equipment Type Required"
                                            Validate="True" ValidationGroup="divCustomerRates" />
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
                                    <TextBox CssClass="ntxto" HelpText="306,CUSTOMER_CHARGE_DETAIL_HNDLNG_IN_CHRG_NC"
                                        iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="divCustomerRates">
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
                                    <TextBox CssClass="ntxto" HelpText="307,CUSTOMER_CHARGE_DETAIL_HNDLNG_OUT_CHRG_NC"
                                        iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="divCustomerRates">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" ReqErrorMessage="Handling Out Charges Required"
                                            Type="Double" CustomValidationFunction="" CustomValidation="false" CsvErrorMessage="Exist"
                                            ValidationGroup="divCustomerRates" Validate="True" RegErrorMessage="Invalid Handling Out Charge Amount. Range must be from 0.00 to 9999999999.99"
                                            RegexValidation="true" RegularExpression="^\d{1,10}(\.\d{1,2})?$" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="80px" Wrap="true" HorizontalAlign="Right" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="INSPCTN_CHRGS" CharacterLimit="0" HeaderText="Inspection Charges"
                                    HeaderTitle="Inspection Charges" SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="ntxto" HelpText="562,CUSTOMER_CHARGE_DETAIL_INSPCTN_CHRGS" iCase="Numeric"
                                        OnClientTextChange="formatRate" ValidationGroup="divCustomerRates">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" ReqErrorMessage="Inspection Charges"
                                            Type="Double" CustomValidationFunction="" CustomValidation="false" CsvErrorMessage="Exist"
                                            ValidationGroup="divCustomerRates" Validate="True" RegErrorMessage="Invalid Inspection Charges. Range must be from 0.00 to 9999999999.99"
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
                         <Ifg:iFlexGrid ID="ifgList" runat="server" AutoGenerateColumns="False" AllowStaticHeader="False"
                            CaptionAlign="NotSet" GridLines="Both" HeaderRows="0" HorizontalAlign="NotSet"
                            StaticHeaderHeight="180" Type="Mergeable" UseCachedDataSource="True" AllowAdd="False"
                            AllowDelete="False" ShowHeader="False" OnAfterCallBack="" Width="100%" OnClientCollapse="OnListCollapse"
                            OnClientExpand="onClientExpandList" CssClass="tblstd" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            ShowEmptyPager="False" ValidationGroup="">
                            <SelectedRowStyle CssClass="gsitem" />
                            <RowStyle CssClass="gitem" />
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                            <HeaderStyle CssClass="ghdr" />
                            <Columns>
                                <Ifg:ExpandableField GridId="ifgStorageDetail" HeaderText="Select" ItemStyle-Width="40px">
                                </Ifg:ExpandableField>
                                <Ifg:TextboxField DataField="ENM_CD" HeaderText="Charges" ReadOnly="True" Visible="false"
                                    DataFormatString="">
                                    <TextBox iCase="None" OnClientTextChange="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                    </TextBox>
                                </Ifg:TextboxField>
                                <Ifg:TextboxField DataField="ENM_CD" HeaderText="Charges" ReadOnly="True">
                                    <TextBox iCase="None" OnClientTextChange="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                    </TextBox>
                                </Ifg:TextboxField>
                            </Columns>
                            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Ifg:iFlexGrid>
                        <iFg:iFlexGrid ID="ifgStorageDetail" runat="server" AllowStaticHeader="True" DataKeyNames="CSTMR_STRG_DTL_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="80px" Type="Mergeable"
                            ValidationGroup="divCustomerRates" UseCachedDataSource="True" AutoGenerateColumns="False"
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
                                <iFg:TextboxField DataField="UP_TO_DYS" CharacterLimit="0" HeaderText="Up to Days"
                                    HeaderTitle="Up to Days" SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="txt" HelpText="308,CUSTOMER_STORAGE_DETAIL_UP_TO_DYS" iCase="Numeric"
                                        OnClientTextChange="" ValidationGroup="tabStorageDetail">
                                        <Validator CustomValidateEmptyText="False" IsRequired="true" Operator="Equal" ReqErrorMessage="Up to Days Required and It should be greater than previous Days."
                                            Type="String" CustomValidationFunction="validateUpToDays" CustomValidation="true"
                                            CsvErrorMessage="Exist" ValidationGroup="divCustomerRates" RegexValidation="true"
                                            RegularExpression="^[0-9]+$" RegErrorMessage="Invalid Up to Days. Up to Days must be a round value"
                                            Validate="True" />
                                    </TextBox>
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Right" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="STRG_CHRG_NC" CharacterLimit="0" HeaderText="Storage Charge"
                                    HeaderTitle="Storage Charge" SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="ntxto" HelpText="309,CUSTOMER_STORAGE_DETAIL_STRG_CHRG_NC" iCase="Numeric"
                                        MaxLength="9" OnClientTextChange="formatRate" ValidationGroup="tabStorageDetail">
                                        <Validator CustomValidateEmptyText="False" IsRequired="true" Operator="LessThanEqual"
                                            ReqErrorMessage="Storage Charge Required" Type="Double" Validate="true" RegErrorMessage="Invalid Storage Charge. Range must be from 0.00 to 9999999.99"
                                            CustomValidation="false" CustomValidationFunction="" CompareValidation="true"
                                            ValueToCompare="9999999.99" CmpErrorMessage="Invalid Storage Charge. Range must be from 0.00 to 9999999.99"
                                            RegexValidation="false" RegularExpression="^(\d{1,7})(.\d{2})?$" ValidationGroup="divCustomerRates" />
                                    </TextBox>
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Right" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="UP_TO_CNTNRS" CharacterLimit="0" HeaderText="Up to Containers"
                                    HeaderTitle="Up to Containers" SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="txt" HelpText="620" iCase="Numeric"
                                        OnClientTextChange="" ValidationGroup="tabStorageDetail" MaxLength="4">
                                        <Validator CustomValidateEmptyText="True" IsRequired="False" Operator="Equal" ReqErrorMessage="Up to Containers Required."
                                            Type="String" CustomValidationFunction="validateUpToContainers" CustomValidation="true"
                                            CsvErrorMessage="Exist" ValidationGroup="divCustomerRates" RegexValidation="true"
                                            RegularExpression="^[0-9]+$" RegErrorMessage="Invalid Up to Containers. Up to Containers must be a round value"
                                            Validate="True" />
                                    </TextBox>
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Right" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="CLNNG_RT" CharacterLimit="0" HeaderText="Cleaning Rate"
                                    HeaderTitle="Storage Charge" SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="ntxto" HelpText="621,CUSTOMER_CLEANING_DETAIL_CLNNG_RT" iCase="Numeric"
                                        MaxLength="9" OnClientTextChange="formatRate" ValidationGroup="tabStorageDetail">
                                        <Validator CustomValidateEmptyText="True" IsRequired="False" Operator="LessThanEqual"
                                            ReqErrorMessage="Cleaning Rate Required" Type="Double" Validate="true" RegErrorMessage="Invalid Cleaning Rate. Range must be from 0.00 to 9999999.99"
                                            CustomValidation="true" CustomValidationFunction="setRequiredForCleaningRate" CompareValidation="true"
                                            ValueToCompare="9999999.99" CmpErrorMessage="Invalid Cleaning Rate. Range must be from 0.00 to 9999999.99"
                                            RegexValidation="false" RegularExpression="^(\d{1,7})(.\d{2})?$" ValidationGroup="divCustomerRates" />
                                    </TextBox>
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Right" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="RMRKS_VC" CharacterLimit="0" HeaderText="Remarks" HeaderTitle="Remarks"
                                    SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="txt" HelpText="622,CUSTOMER_CLEANING_DETAIL_RMRKS_VC" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="divCustomerRates">
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
                            <tr style="height:30px">
                                <td colspan="2">
                                    <Inp:iLabel ID="lblTaxes" runat="server" CssClass="lbl" Font-Bold="True" Font-Overline="False"
                                        Font-Underline="True">Taxes</Inp:iLabel>
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
                                        OnClientTextChange="formatHandlingRate" iCase="Numeric" TabIndex="18" HelpText="563,CUSTOMER_HANDLNG_TX"
                                        TextMode="SingleLine" ToolTip="Handling Tax Rate">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            ValidationGroup="tabCustomer" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                            IsRequired="False" ReqErrorMessage="Handling Tax Rate Required" CustomValidation="false"
                                            RegErrorMessage="Invalid Handling Tax Rate. Rate can be upto 14 digits and 2 decimal points"
                                            RegexValidation="True" RegularExpression="^(\+)?\d{1,14}(\.\d{1,2})?$" />
                                    </Inp:iTextBox>
                                </td>
                            </tr>
                            <tr style="height:30px">
                                <td>
                                    <label id="lblStorageTaxRate" runat="server" class="lbl">
                                        Storage Tax Rate
                                    </label>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtStorage_Tx_Rt" runat="server" CssClass="ntxt" MaxLength="100"
                                        TabIndex="19" HelpText="564,CUSTOMER_STORG_TX" TextMode="SingleLine" iCase="Numeric"
                                        OnClientTextChange="formatStorageRate" ToolTip="Storage Tax Rate">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            ValidationGroup="tabCustomer" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
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
                                        OnClientTextChange="formatServiceRate" HelpText="565,CUSTOMER_SERVC_TX" TextMode="SingleLine"
                                        iCase="Numeric" ToolTip="Service Tax (%)">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            ValidationGroup="tabCustomer" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
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
                                    <div id="divLbrRate">
                                        <Inp:iTextBox ID="txtLaborRate" runat="server" CssClass="ntxt" MaxLength="100"
                                            OnClientTextChange="" TabIndex="21" HelpText="566,CUSTOMER_LBR_RT_PR_HR"
                                            TextMode="SingleLine" iCase="Numeric" ToolTip="Labor Rate">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                ValidationGroup="divLbrRate" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="formatLaborRate"
                                                IsRequired="True" ReqErrorMessage="Labor Rate Per Hour Required" CustomValidation="True"
                                                RegErrorMessage="Invalid Labor Rate Per Hour. Rate can be upto 14 digits and 2 decimal points"
                                                RegexValidation="True" RegularExpression="^(\+)?\d{1,14}(\.\d{1,2})?$" SetFocusOnError="False" />
                                        </Inp:iTextBox>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
       <%-- </div>--%>
    </div>
    <div id="divTransportation" class="" style="height: 150px; display: none;">
        <div class="topspace">
        </div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <iFg:iFlexGrid ID="ifgCustomerTransportation" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="15"
                        AllowSearch="false" AutoSearch="True" AllowRefresh="false" StaticHeaderHeight="130px"
                        ShowEmptyPager="True" Width="900px" UseCachedDataSource="True" AllowSorting="True"
                        AllowStaticHeader="True" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        HeaderRows="1" EnableViewState="False" DeleteButtonText="Delete Row" GridLines="Both"
                        HorizontalAlign="NotSet" PageSizerFormat="" Scrollbars="Auto" Type="Normal" ValidationGroup="divTransportation"
                        DataKeyNames="CSTMR_TRNSPRTTN_ID" OnAfterCallBack="" OnBeforeCallBack="" OnAfterClientRowCreated="setDefaultTransportation"
                        AddRowsonCurrentPage="False" UseIcons="true" SearchButtonIconClass="icon-search"
                        SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                        AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                        DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                        RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                        ClearButtonCssClass="icon-eraser">
                        <Columns>
                            <iFg:LookupField DataField="RT_CD" ForeignDataField="" HeaderText="Route*" HeaderTitle="Route"
                                PrimaryDataField="" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="50px" ReadOnly="false">
                                <Lookup DataKey="RT_CD" DependentChildControls="" HelpText="474,ROUTE_RT_CD" iCase="Upper"
                                    OnClientTextChange="" ValidationGroup="" MaxLength="10" TableName="68" CssClass="lkp"
                                    DoSearch="True" Width="120px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                    SecondaryColumnName="">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="RT_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="RT_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Route Description" ColumnName="RT_DSCRPTN_VC" Hidden="false" />
                                        <Inp:LookupColumn ColumnCaption="Pick Up Location" ColumnName="PCK_UP_LCTN_CD" Hidden="false"
                                            ControlToBind="1" />
                                        <Inp:LookupColumn ColumnCaption="Drop Off Location" ColumnName="DRP_OFF_LCTN_CD"
                                            Hidden="false" ControlToBind="2" />
                                        <Inp:LookupColumn ColumnCaption="Empty Trip Rate" ColumnName="EMPTY_TRP_RT_NC" Hidden="true" />
                                        <Inp:LookupColumn ColumnCaption="Full Trip Rate" ColumnName="FLL_TRP_RT_NC" Hidden="true" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="NotSet" Width="400px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        CustomValidation="true" CustomValidationFunction="validateRouteCode" LkpErrorMessage="Invalid Route. Click on the list for valid values"
                                        ReqErrorMessage="Route Required" Validate="True" ValidationGroup="divTransportation" />
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="120px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:BoundField DataField="PCK_UP_LCTN_CD" HeaderText="Pick Up Location" HeaderTitle="Pick Up Location"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="80px" Wrap="True" />
                            </iFg:BoundField>
                            <iFg:BoundField DataField="DRP_OFF_LCTN_CD" HeaderText="Drop Off Location" HeaderTitle="Drop Off Location"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="80px" Wrap="True" />
                            </iFg:BoundField>
                            <iFg:TextboxField DataField="EMPTY_TRP_RT_NC" HeaderText="Empty Trip Rate" HeaderTitle="Empty Trip Rate"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="475,CUSTOMER_TRANSPORTATION_EMPTY_TRP_RT_NC"
                                    iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        ValidationGroup="divTransportation" RegexValidation="true" RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" RegErrorMessage="Invalid Empty Trip Rate. Range must be from 0.01 to 9999999.99" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="FLL_TRP_RT_NC" HeaderText="Full Trip Rate" HeaderTitle="Full Trip Rate"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="476,CUSTOMER_TRANSPORTATION_FLL_TRP_RT_NC"
                                    iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        RegexValidation="true" RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$" CustomValidationFunction=""
                                        ValidationGroup="divTransportation" IsRequired="false" ReqErrorMessage="" RegErrorMessage="Invalid Full Trip Rate. Range must be from 0.01 to 9999999.99" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                        </Columns>
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <PagerStyle CssClass="gpage" HorizontalAlign="Center" />
                        <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                        <SelectedRowStyle CssClass="gsitem" />
                        <AlternatingRowStyle CssClass="gaitem" />
                    </iFg:iFlexGrid>
                </td>
            </tr>
        </table>
    </div>
    <div id="divRental" class="" style="height: 150px; display: none;">
        <div class="topspace">
        </div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <iFg:iFlexGrid ID="ifgCustomerRental" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="17"
                        AllowSearch="false" AutoSearch="True" AllowRefresh="false" StaticHeaderHeight="130px"
                        ShowEmptyPager="True" Width="98%" UseCachedDataSource="True" AllowSorting="True"
                        AllowStaticHeader="True" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        HeaderRows="1" EnableViewState="False" DeleteButtonText="Delete Row" GridLines="Both"
                        HorizontalAlign="NotSet" PageSizerFormat="" Scrollbars="Auto" Type="Normal" ValidationGroup="divRental"
                        DataKeyNames="CSTMR_RNTL_ID" OnAfterCallBack="onAfterCustomerRentalCallBack"
                        OnBeforeCallBack="onBeforeCustomerRentalCallBack" OnAfterClientRowCreated="setDefaultRental"
                        AddRowsonCurrentPage="False" UseIcons="true" SearchButtonIconClass="icon-search"
                        SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                        AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                        DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                        RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                        ClearButtonCssClass="icon-eraser">
                        <Columns>
                            <iFg:TextboxField DataField="CNTRCT_RFRNC_NO" HeaderText="Contract Ref No*" HeaderTitle="Contract Reference Number"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="477,CUSTOMER_RENTAL_CNTRCT_RFRNC_NO"
                                    iCase="Upper" OnClientTextChange="" ValidationGroup="">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="true"
                                        RegularExpression="^[a-zA-Z0-9-_/]+$" IsRequired="true" ReqErrorMessage="Contract Reference Number Required"
                                        RegexValidation="true" RegErrorMessage="Only Alphabets/Numbers and -/_ are allowed."
                                        CustomValidationFunction="validateContractNo" ValidationGroup="divRental" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                            </iFg:TextboxField>
                            <iFg:DateField DataField="CNTRCT_STRT_DT" HeaderText="Contract Start Date*" HeaderTitle="Contract Start Date"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                ReadOnly="false">
                                <iDate HelpText="478" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                    ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                    <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                        ImageAlign="TextTop" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="true"
                                        CustomValidation="true" LkpErrorMessage="Invalid Contract Start Date. Click on the calendar icon for valid values"
                                        CustomValidationFunction="validateStartDate" ReqErrorMessage="Contract Start Date Required"
                                        Validate="True" RangeValidation="false" CompareValidation="false" ValidationGroup="divRental" />
                                </iDate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="90px" Wrap="True" HorizontalAlign="Left" />
                            </iFg:DateField>
                            <iFg:DateField DataField="CNTRCT_END_DT" HeaderText="Contract End Date*" HeaderTitle="Contract End Date"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                ReadOnly="false">
                                <iDate HelpText="479" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                    ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                    <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                        ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="true"
                                        LkpErrorMessage="Invalid Contract End Date. Click on the calendar icon for valid values"
                                        CustomValidation="true" CustomValidationFunction="validateEndDate" ReqErrorMessage="Contract End Date Required"
                                        Validate="True" RangeValidation="false" CompareValidation="false" ValidationGroup="divRental" />
                                </iDate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="90px" Wrap="True" HorizontalAlign="Left" />
                            </iFg:DateField>
                            <iFg:TextboxField DataField="MN_TNR_DY" HeaderText="Minimum Tenure (Days)" HeaderTitle="Minimum Tenure (Days)"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="513" iCase="Numeric"
                                    OnClientTextChange="" ValidationGroup="" MaxLength="4">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        RegexValidation="true" RegularExpression="^\d{1,4}?$" IsRequired="false" ReqErrorMessage="Minmum Tenure (Days) Required"
                                        CustomValidationFunction="" RegErrorMessage="Invalid Minimum Tenure Days. Range must be from 0 to 9999"
                                        ValidationGroup="divRental" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="RNTL_PR_DY" HeaderText="Rental Per Day*" HeaderTitle="Rental Per Day"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="480,CUSTOMER_RENTAL_RNTL_PR_DY"
                                    iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        ReqErrorMessage="Rental Per Day Required" RegexValidation="true" RegularExpression="^\d{1,7}(\.\d{1,2})?$"
                                        RegErrorMessage="Invalid Rental Per Day. Range must be from 0.01 to 9999999.99"
                                        CustomValidationFunction="" ValidationGroup="divRental" IsRequired="true" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="HNDLNG_OT" HeaderText="Handling Out" HeaderTitle="Handling Out"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="481,CUSTOMER_RENTAL_HNDLNG_OT"
                                    iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        RegexValidation="true" RegularExpression="^\d{1,7}(\.\d{1,2})?$" CustomValidationFunction=""
                                        ValidationGroup="divRental" IsRequired="false" ReqErrorMessage="" RegErrorMessage="Invalid Handling Out. Range must be from 0.01 to 9999999.99" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="HNDLNG_IN" HeaderText="Handling In" HeaderTitle="Handling In"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="482,CUSTOMER_RENTAL_HNDLNG_IN"
                                    iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        RegexValidation="true" RegularExpression="^\d{1,7}(\.\d{1,2})?$" CustomValidationFunction=""
                                        ValidationGroup="divRental" IsRequired="false" ReqErrorMessage="" RegErrorMessage="Invalid Handling In. Range must be from 0.01 to 9999999.99" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="ON_HR_SRVY" HeaderText="On-Hire Survey" HeaderTitle="On-Hire Survey"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="483,CUSTOMER_RENTAL_ON_HR_SRVY"
                                    iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        ValidationGroup="divRental" RegexValidation="true" RegularExpression="^\d{1,7}(\.\d{1,2})?$"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" RegErrorMessage="Invalid On-Hire Survey. Range must be from 0.01 to 9999999.99" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="OFF_HR_SRVY" HeaderText="Off-Hire Survey" HeaderTitle="Off-Hire Survey"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="484,CUSTOMER_RENTAL_OFF_HR_SRVY"
                                    iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        ValidationGroup="divRental" RegexValidation="true" RegularExpression="^\d{1,7}(\.\d{1,2})?$"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" RegErrorMessage="Invalid Off-Hire Survey. Range must be from 0.01 to 9999999.99" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="485,CUSTOMER_RENTAL_RMRKS_VC"
                                    iCase="None" OnClientTextChange="" ValidationGroup="">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="false"
                                        ValidationGroup="divRental" RegexValidation="false" CustomValidationFunction=""
                                        IsRequired="false" ReqErrorMessage="" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" />
                            </iFg:TextboxField>
                        </Columns>
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <PagerStyle CssClass="gpage" HorizontalAlign="Center" />
                        <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                        <SelectedRowStyle CssClass="gsitem" />
                        <AlternatingRowStyle CssClass="gaitem" />
                    </iFg:iFlexGrid>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFtp" style="height: 166px;">
        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
            <tr>
                <td>
                    <Inp:iLabel ID="lblFtpServer" runat="server" CssClass="lbl">
                                                    FTP Server Url
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel3" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtFtpServer" runat="server" CssClass="txt" TabIndex="30" HelpText="552,FTP_SRVR_URL"
                        TextMode="SingleLine" iCase="Upper" ToolTip="FTP Server Url">
                        <Validator CustomValidateEmptyText="false" Operator="Equal" Type="String" Validate="true"
                            ValidationGroup="divFtp" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="validateFtpServer"
                            IsRequired="false" ReqErrorMessage="FTP Server Url Required" CustomValidation="false"
                            RegexValidation="false" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu|COM|ORG|NET|MIL|EDU)$" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblFtpUserName" runat="server" CssClass="lbl">
                                                    FTP User Name
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel5" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtFtpUserName" runat="server" CssClass="txt" TabIndex="31" HelpText="553,FTP_USR_NAM"
                        TextMode="SingleLine" iCase="None" ToolTip="Ftp User Name" MaxLength="1000">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            ValidationGroup="divFtp" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" ReqErrorMessage="FTP User Name Required" CustomValidation="false"
                            RegexValidation="false" RegErrorMessage="" RegularExpression="^[a-zA-Z0-9@+.!#$&quot;,:;=/\(\),\-\s]{1,255}$" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblFtpPassword" runat="server" CssClass="lbl">
                                                    FTP Password
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel2" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtPassword" runat="server" CssClass="txt" TabIndex="32" HelpText="554,FTP_PSSWRD"
                        TextMode="Password" iCase="None" ToolTip="FTP Password" MaxLength="1000">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            ValidationGroup="divFtp" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" ReqErrorMessage="FTP Password Required" CustomValidation="false"
                            RegexValidation="false" RegErrorMessage="" RegularExpression="^[a-zA-Z0-9@+.!#$&quot;,:;=/\(\),\-\s]{1,255}$" />
                    </Inp:iTextBox>
                </td>
            </tr>
        </table>
    </div>
    </td> </tr>
    <tr>
        <td>
            <%--   <iFg:LookupField DataField="EQPMNT_CD_CD" ForeignDataField="EQPMNT_CD_ID" HeaderText="Code *"
                                                        HeaderTitle="Equipment Code" PrimaryDataField="EQPMNT_CD_ID" SortAscUrl="" SortDescUrl="">
                                                        <Lookup DataKey="EQPMNT_CD_CD" DependentChildControls="" HelpText="304,EQUIPMENT_CODE_EQPMNT_CD_CD"
                                                            iCase="Upper" OnClientTextChange="" ValidationGroup="" MaxLength="1" TableName="7"
                                                            CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                                                            SecondaryColumnName="EQPMNT_CD_DSCRPTN_VC">
                                                            <LookupColumns>
                                                                <Inp:LookupColumn ColumnName="EQPMNT_CD_ID" Hidden="True" />
                                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False" />
                                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_CD_DSCRPTN_VC" Hidden="False" />
                                                            </LookupColumns>
                                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px" />
                                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                                LkpErrorMessage="Invalid Equipment Code. Click on the list for valid values"
                                                                ReqErrorMessage="Equipment Code Required" Validate="True" ValidationGroup="divCustomerRates" />
                                                        </Lookup>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <ItemStyle Width="80px" Wrap="True" />
                                                    </iFg:LookupField>--%>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <%--   <iFg:LookupField DataField="EQPMNT_CD_CD" ForeignDataField="EQPMNT_CD_ID" HeaderText="Code *"
                                                        HeaderTitle="Equipment Code" PrimaryDataField="EQPMNT_CD_ID" SortAscUrl="" SortDescUrl="">
                                                        <Lookup DataKey="EQPMNT_CD_CD" DependentChildControls="" HelpText="304,EQUIPMENT_CODE_EQPMNT_CD_CD"
                                                            iCase="Upper" OnClientTextChange="" ValidationGroup="" MaxLength="1" TableName="7"
                                                            CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                                                            SecondaryColumnName="EQPMNT_CD_DSCRPTN_VC">
                                                            <LookupColumns>
                                                                <Inp:LookupColumn ColumnName="EQPMNT_CD_ID" Hidden="True" />
                                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False" />
                                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_CD_DSCRPTN_VC" Hidden="False" />
                                                            </LookupColumns>
                                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px" />
                                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                                LkpErrorMessage="Invalid Equipment Code. Click on the list for valid values"
                                                                ReqErrorMessage="Equipment Code Required" Validate="True" ValidationGroup="divCustomerRates" />
                                                        </Lookup>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <ItemStyle Width="80px" Wrap="True" />
                                                    </iFg:LookupField>--%>
                </tr>
            </table>
            <%--   <iFg:LookupField DataField="EQPMNT_CD_CD" ForeignDataField="EQPMNT_CD_ID" HeaderText="Code *"
                                                        HeaderTitle="Equipment Code" PrimaryDataField="EQPMNT_CD_ID" SortAscUrl="" SortDescUrl="">
                                                        <Lookup DataKey="EQPMNT_CD_CD" DependentChildControls="" HelpText="304,EQUIPMENT_CODE_EQPMNT_CD_CD"
                                                            iCase="Upper" OnClientTextChange="" ValidationGroup="" MaxLength="1" TableName="7"
                                                            CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                                                            SecondaryColumnName="EQPMNT_CD_DSCRPTN_VC">
                                                            <LookupColumns>
                                                                <Inp:LookupColumn ColumnName="EQPMNT_CD_ID" Hidden="True" />
                                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False" />
                                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_CD_DSCRPTN_VC" Hidden="False" />
                                                            </LookupColumns>
                                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px" />
                                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                                LkpErrorMessage="Invalid Equipment Code. Click on the list for valid values"
                                                                ReqErrorMessage="Equipment Code Required" Validate="True" ValidationGroup="divCustomerRates" />
                                                        </Lookup>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <ItemStyle Width="80px" Wrap="True" />
                                                    </iFg:LookupField>--%>
        </td>
    </tr>
    </table>
   <%-- <div style="vertical-align: bottom;">
       
    </div>--%>
    </div> </div>
     <Inp:iLabel ID="lblNote" runat="server" Font-Bold="true" CssClass="lbl">Note: The above specified rates are in Depot Local Currency</Inp:iLabel>
        <Inp:iLabel ID="lblDepotCuurency" CssClass="lbl" runat="server" Font-Bold="true"></Inp:iLabel>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" align="center"
        onClientPrint="null" />
    <asp:HiddenField ID="hdnNoOfCustomers" runat="server" />
    <asp:HiddenField ID="hdnCustomerId" runat="server" />
    <asp:HiddenField ID="hdnCustomerCode" runat="server" />
    <asp:HiddenField ID="hdnCustomerChargeDetailID" runat="server" />
    <asp:HiddenField ID="hdnDepotCurrency" runat="server" />
    <asp:HiddenField ID="hdnChargeRowIndex" runat="server" />
    </form>
</body>
</html>
