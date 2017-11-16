<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InvoiceParty.aspx.vb" Inherits="Masters_InvoiceParty" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" >
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 20px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Invoicing Party</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navEstimate" runat="server" />
                </td>
            </tr>
        </table>
    </div>
     <!-- UIG Fix -->
    <div class="tabdisplayGatePass" id="tabInvoiceParty" style="overflow-y: auto; overflow-x: auto;height:auto">
        <table border="0" cellpadding="0" cellspacing="0" class="tblstd" style="width: 100%;">
            <tr>
                <td>
                </td>
                <td style="height: 30px">
                    <Inp:iLabel ID="lblInvoicePartyCode" runat="server" class="lbl">
                        Code
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblInvoicePartyCodeReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtInvoicePartyCode" runat="server" CssClass="txt" MaxLength="9"
                        TabIndex="1" HelpText="233,INVOICING_PARTY_INVCNG_PRTY_CD" TextMode="SingleLine"
                        iCase="Upper" ToolTip="Invoicing Party Code">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage="This Code Already Exists"
                            CustomValidationFunction="validateInvoicePartyCode" IsRequired="true" ReqErrorMessage="Invoicing Party Code Required"
                            CustomValidation="true" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblInvoicePartyName" runat="server" class="lbl">
                       Name
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblInvoicePartyNameReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtInvoicePartyName" runat="server" CssClass="txt" MaxLength="10"
                        TabIndex="2" HelpText="234,INVOICING_PARTY_INVCNG_PRTY_NM" TextMode="SingleLine"
                        iCase="Upper" ToolTip="Invoicing Party Name">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="true" ReqErrorMessage="Invoicing Party Name Required"
                            CustomValidation="false" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblContactPersonName" runat="server" class="lbl">
                        Contact Person
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtContactPersonName" runat="server" CssClass="txt" MaxLength="10"
                        TabIndex="3" HelpText="235,INVOICING_PARTY_CNTCT_PRSN_NM" TextMode="SingleLine"
                        iCase="Upper" ToolTip="Contact Person Name">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Contact Person Name Required"
                            CustomValidation="false" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="height: 30px">
                    <Inp:iLabel ID="lblContactJobTitle" runat="server" class="lbl">
                       Job Title
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtContactJobTitle" runat="server" CssClass="txt" MaxLength="10"
                        TabIndex="4" HelpText="236,INVOICING_PARTY_CNTCT_JB_TTL" TextMode="SingleLine"
                        iCase="None" ToolTip="Contact Job Title">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Contact Job Title Required"
                            CustomValidation="false" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblBaseCurrency" runat="server" class="lbl">
                      Currency
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblBaseCurrencyReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpBaseCrrncy" runat="server" CssClass="lkp" DataKey="CRRNCY_CD"
                        DoSearch="True" iCase="Upper" MaxLength="3" TabIndex="5" TableName="2" HelpText="245,CURRENCY_CRRNCY_CD"
                        ClientFilterFunction="" ToolTip="Base Currency" AllowSecondaryColumnSearch="true" 
                        SecondaryColumnName="CRRNCY_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="CRRNCY_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="CRRNCY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="CRRNCY_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True"
                            LkpErrorMessage="Invalid Currency. Click on the list for valid values" Operator="Equal"
                            ReqErrorMessage="Currency Required" Type="String" Validate="True" CustomValidationFunction=""
                            ValidationGroup="tabInvoiceParty" CustomValidation="false" />
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
                <td>
                  
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="height: 30px">
                    <Inp:iLabel ID="lblContactAddress" runat="server" class="lbl">
                        Contact Address
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtContactAddress" runat="server" CssClass="txt" MaxLength="500"
                        TabIndex="7" HelpText="237" TextMode="MultiLine" iCase="None"
                        ToolTip="Contact Address">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Single & Double quotes are not Allowed"
                            RegularExpression="^[a-zA-Z0-9@+.!#$&quot;,:;=/\(\),\-\s]{1,255}$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Contact Address Required"
                            CustomValidation="false" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="inpBillingAddress" runat="server" class="lbl">
                        Billing Address
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtBillingAddress" runat="server" CssClass="txt" MaxLength="500"
                        TabIndex="8" HelpText="238" TextMode="MultiLine" iCase="None"
                        ToolTip="Billing Address">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Single & Double quotes are not Allowed"
                            RegularExpression="^[a-zA-Z0-9@+.!#$&quot;,:;=/\(\),\-\s]{1,255}$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Billing Address Required"
                            CustomValidation="false" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblRemarks" runat="server" class="lbl">
                      Remarks
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtRemarks" runat="server" CssClass="txt" TabIndex="9" MaxLength="500"
                        HelpText="244" TextMode="MultiLine" iCase="None" ToolTip="Remarks">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Remarks Required"
                            CustomValidation="false" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="height: 30px">
                    <Inp:iLabel ID="lblPhoneNo" runat="server" class="lbl">
                       Phone No
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtPhoneNo" runat="server" CssClass="txt" MaxLength="10" TabIndex="10"
                        HelpText="242,INVOICING_PARTY_PHN_NO" TextMode="SingleLine" iCase="None" ToolTip="Phone No">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Phone No Required"
                            CustomValidation="false" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblFaxNo" runat="server" class="lbl">
                       Fax No
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtFaxNo" runat="server" CssClass="txt" MaxLength="10" TabIndex="11"
                        HelpText="243,INVOICING_PARTY_FX_NO" TextMode="SingleLine" iCase="None" ToolTip="Fax No">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Fax No Required"
                            CustomValidation="false" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblZipCode" runat="server" class="lbl">
                      Zip Code
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtZipCode" runat="server" CssClass="txt" MaxLength="10" TabIndex="12"
                        HelpText="241,INVOICING_PARTY_ZP_CD" TextMode="SingleLine" iCase="None" ToolTip="Zip Code">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Zip Code Required"
                            CustomValidation="false" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="height: 30px">
                    <Inp:iLabel ID="lblReportingEmailID" runat="server" class="lbl">
                      Email for Reporting 
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtReportingEmailID" runat="server" CssClass="txt" TabIndex="13" MaxLength="1000"
                        HelpText="247" TextMode="MultiLine" iCase="None"
                        ToolTip="Reporting Email ID">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Invalid Email Format"
                            RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Reporting Email ID Required"
                            CustomValidation="false" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblInvoicingEmailID" runat="server" class="lbl">
                     Email for Invoicing 
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtInvoicingEmailID" runat="server" CssClass="txt" MaxLength="1000"
                        TabIndex="14" HelpText="248" TextMode="MultiLine"
                        iCase="None" ToolTip="Invoicing Email ID">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Invalid Email Format"
                            RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$" Type="String" Validate="True"
                            ValidationGroup="tabInvoiceParty" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Invoicing Email ID Required"
                            CustomValidation="false" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblActive" runat="server" class="lbl">
                      Active
                    </Inp:iLabel>
                </td>
                <td>
                    <asp:CheckBox ID="chkACTV_BT" runat="server" Text="" ToolTip="Active" CssClass="chk" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
            <td></td>
                    <td style="height: 30px">
                            <div id="divlblLedger">
                                <Inp:iLabel ID="lblLedgerId" runat="server" ToolTip="*" CssClass="lbl">
                                               Ledger Id
                                </Inp:iLabel>
                                  <Inp:iLabel ID="lblLedgerIdReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel></div>
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
                <td colspan="8">
                </td>
            </tr>
        </table>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" align="center"
        onClientPrint="null" />
    </form>
</body>
</html>
