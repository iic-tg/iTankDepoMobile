<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MoreInfo.aspx.vb" Inherits="Operations_MoreInfo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server" style="overflow: auto">
    <div>
        <table border="0" cellpadding="1" align="Left" cellspacing="1" class="tblstd" align="center">
            <tr>
                <td>
                    <label id="lblEquipmentId" runat="server" class="lbl">
                        Equipment No</label>
                </td>
                <td>
                    <div>
                        <Inp:iTextBox ID="txtEquipmentNo" runat="server" CssClass="txtd" TabIndex="-1" HelpText="26,GATEIN_EQPMNT_NO" TextMode="SingleLine" iCase="Upper" ReadOnly="True">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="True" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                        </Inp:iTextBox>
                    </div>
                </td>
                <td>
                    <label id="lblDateofMfg" runat="server" class="lbl">
                        Manuf. Date</label>
                </td>
                <td>
                    <Inp:iDate ID="txtDateofManf" TabIndex="1" runat="server" HelpText="457" CssClass="dat" MaxLength="11" iCase="Upper">
                        <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="gateInmoreInfo" Operator="LessThanEqual" LkpErrorMessage="Invalid  Manuf. Date. Click on the calendar icon for valid values" Validate="True" CsvErrorMessage="" CustomValidationFunction="" CmpErrorMessage="Manuf. Date cannot be greater than current Date." CompareValidation="True" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
                <td>
                    <label id="lblacep" runat="server" class="lbl">
                        ACEP</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtAcep" runat="server" CssClass="txt" TabIndex="2" HelpText="97,GATEIN_DETAIL_ACEP_CD" TextMode="SingleLine" iCase="Upper">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="True" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblMaterialCode" runat="server" class="lbl">
                        Material</label>
                </td>
                <td>
                    <div>
                        <Inp:iLookup ID="lkpMaterialCode" runat="server" CssClass="lkp" DataKey="MTRL_CD" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="3" TableName="14" HelpText="98,MATERIAL_MTRL_CD" ClientFilterFunction="applyDepoFilter" align="left" AllowSecondaryColumnSearch="true" SecondaryColumnName="MTRL_DSCRPTN_VC">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="MTRL_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="MTRL_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="MTRL_DSCRPTN_VC" Hidden="False" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Material. Click on the list for valid values" Operator="Equal" ReqErrorMessage="" Type="String" Validate="True" ValidationGroup="" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </div>
                </td>
                <td>
                    <label id="lblWeight" runat="server" class="lbl">
                        Gross Weight (Kgs)</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtGrossWeight" runat="server" CssClass="ntxto" TabIndex="4" HelpText="256,GATEIN_DETAIL_GRSS_WGHT_NC" TextMode="SingleLine" iCase="Numeric" OnClientTextChange="FormatThreeDecimal">
                        <Validator CustomValidateEmptyText="False" Operator="GreaterThan" Validate="True" Type="Double" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="True" RangeValidation="False" RegErrorMessage="Invalid Gross Weight. Range must be from 0.001 to 9999999.999" RegularExpression="^\d{1,7}(\.\d{1,3})?$" CompareValidation="true" ValueToCompare="0" CmpErrorMessage="Gross Weight must be greater than 0" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="Label1" runat="server" class="lbl">
                        Tare Weight (Kgs)</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtTareWeight" runat="server" CssClass="ntxto" TabIndex="5" HelpText="255,GATEIN_DETAIL_TR_WGHT_NC" TextMode="SingleLine" iCase="Numeric" OnClientTextChange="FormatThreeDecimal">
                        <Validator CustomValidateEmptyText="False" Operator="GreaterThan" Validate="True" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" Type="Double" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="True" RangeValidation="False" RegErrorMessage="Invalid Tare Weight. Range must be from 0.001 to 9999999.999" RegularExpression="^\d{1,7}(\.\d{1,3})?$" CompareValidation="true" ValueToCompare="0" CmpErrorMessage="Tare Weight must be greater than 0" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblMeeasure" runat="server" class="lbl">
                        Measure</label>
                </td>
                <td>
                    <div>
                        <Inp:iLookup ID="lkpMeasure" runat="server" CssClass="lkp" DataKey="MSR_CD" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="6" TableName="13" HelpText="100,MEASURE_MSR_CD" ClientFilterFunction="applyDepoFilter" align="left" AllowSecondaryColumnSearch="true" SecondaryColumnName="MSR_DSCRPTN_VC">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="MSR_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="MSR_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="MSR_DSCRPTN_VC" Hidden="False" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Measure Code. Click on the list for valid values" Operator="Equal" ReqErrorMessage="" Type="String" Validate="True" ValidationGroup="" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </div>
                </td>
                <td>
                    <label id="lblunits" runat="server" class="lbl">
                        Units</label>
                </td>
                <td>
                    <Inp:iLookup ID="lkpUnits" runat="server" CssClass="lkp" DataKey="UNT_CD" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="7" TableName="15" HelpText="101,UNIT_UNT_CD" ClientFilterFunction="applyDepoFilter" AllowSecondaryColumnSearch="true" SecondaryColumnName="UNT_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="UNT_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="UNT_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="UNT_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Units. Click on the list for valid values" Operator="Equal" ReqErrorMessage="" Type="String" Validate="True" ValidationGroup="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="tblstd">
                        <tr>
                            <td>
                                <label id="lblOnhireLoc" runat="server" class="lbl">
                                    Previous On-Hire Location</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtOnhireLoc" runat="server" CssClass="txt" TabIndex="8" HelpText="102,GATEIN_DETAIL_LST_OH_LOC" TextMode="SingleLine" iCase="Upper">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblOnhireDate" runat="server" class="lbl">
                                    Previous On-Hire Date</label>
                            </td>
                            <td>
                                <Inp:iDate ID="datOnhireDate" TabIndex="9" runat="server" HelpText="103" CssClass="dat" MaxLength="11" iCase="Upper">
                                    <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="gateInmoreInfo" Operator="Equal" LkpErrorMessage="Invalid Previous On-Hire Date. Click on the calendar icon for valid values" Validate="True" CsvErrorMessage="" CustomValidationFunction="" CmpErrorMessage="" CompareValidation="False" />
                                    <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iDate>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblTruckerCode" runat="server" class="lbl">
                                    Trucker Code</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtTruckerCode" runat="server" CssClass="txt" TabIndex="10" HelpText="104,GATEIN_DETAIL_TRCKR_CD" TextMode="SingleLine" iCase="Upper">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblLoadStatus" runat="server" class="lbl">
                                    Load Status</label>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpLoadStatus" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="11" TableName="16" HelpText="105,GATEIN_DETAIL_LDD_STTS_CD" ClientFilterFunction="" AllowSecondaryColumnSearch="false" SecondaryColumnName="ENM_DSCRPTN_VC">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                        <Inp:LookupColumn ColumnName="ENM_DSCRPTN_VC" ControlToBind="" Hidden="False" ColumnCaption="Description" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Load Status. Click on the list for valid values" Operator="Equal" ReqErrorMessage="" Type="String" Validate="True" ValidationGroup="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="3">
                    <fieldset>
                        <legend class="lbl">Chassis License</legend>
                        <table class="tblstd" align="center">
                            <tr>
                                <td>
                                    <label id="lblCountry" runat="server" class="lbl">
                                        Country</label>
                                </td>
                                <td>
                                    <Inp:iLookup ID="lkpCountry" runat="server" CssClass="lkp" DataKey="CNTRY_CD" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="12" TableName="17" HelpText="106,COUNTRY_CNTRY_CD" ClientFilterFunction="applyDepoFilter" AllowSecondaryColumnSearch="true" SecondaryColumnName="CNTRY_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="CNTRY_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnName="CNTRY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="CNTRY_DSCRPTN_VC" Hidden="False" />
                                        </LookupColumns>
                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Country. Click on the list for valid values" Operator="Equal" ReqErrorMessage="" Type="String" Validate="True" ValidationGroup="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iLookup>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblState" runat="server" class="lbl">
                                        State</label>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtState" runat="server" CssClass="txt" TabIndex="13" HelpText="107,GATEIN_DETAIL_LIC_STT" TextMode="SingleLine" iCase="Upper">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                    </Inp:iTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblNumber" runat="server" class="lbl">
                                        Number</label>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtNumber" runat="server" CssClass="txt" TabIndex="14" HelpText="108,GATEIN_DETAIL_LIC_REG" TextMode="SingleLine" iCase="Upper">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                    </Inp:iTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblExpiry" runat="server" class="lbl">
                                        Expiry</label>
                                </td>
                                <td>
                                    <Inp:iDate ID="txtExpiry" runat="server" HelpText="109" TabIndex="15" CssClass="dat" MaxLength="11" iCase="Upper">
                                        <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="gateInmoreInfo" Operator="Equal" LkpErrorMessage="Invalid Chassis License Expiry Date. Click on the calendar icon for valid values" Validate="True" CsvErrorMessage="" CustomValidationFunction="" CmpErrorMessage="" CompareValidation="False" />
                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iDate>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table class="tblstd">
                        <tr>
                            <td>
                                <label id="lblNotes1" runat="server" class="lbl">
                                    Notes 1</label>
                            </td>
                            <td colspan="3">
                                <Inp:iTextBox ID="txtNotes1" runat="server" CssClass="txt" TabIndex="16" HelpText="110,GATEIN_DETAIL_NT_1_VC" TextMode="SingleLine" iCase="Upper" Width="300px">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblNotes2" runat="server" class="lbl">
                                    Notes 2</label>
                            </td>
                            <td colspan="3">
                                <Inp:iTextBox ID="txtNotes2" runat="server" CssClass="txt" TabIndex="17" HelpText="111,GATEIN_DETAIL_NT_1_VC" TextMode="SingleLine" iCase="Upper" Width="300px">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblNotes3" runat="server" class="lbl">
                                    Notes 3</label>
                            </td>
                            <td colspan="3">
                                <Inp:iTextBox ID="txtNotes3" runat="server" CssClass="txt" TabIndex="18" HelpText="112,GATEIN_DETAIL_NT_1_VC" TextMode="SingleLine" iCase="Upper" Width="300px">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="Label2" runat="server" class="lbl">
                                    Notes 4</label>
                            </td>
                            <td colspan="3">
                                <Inp:iTextBox ID="txtNotes4" runat="server" CssClass="txt" TabIndex="18" HelpText="557,GATEIN_DETAIL_NT_1_VC" TextMode="SingleLine" iCase="Upper" Width="300px">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="Label3" runat="server" class="lbl">
                                    Notes 5</label>
                            </td>
                            <td colspan="3">
                                <Inp:iTextBox ID="txtNotes5" runat="server" CssClass="txt" TabIndex="18" HelpText="558,GATEIN_DETAIL_NT_1_VC" TextMode="SingleLine" iCase="Upper" Width="300px">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="5">
                    <table class="tblstd">
                        <tr>
                            <td>
                                <label id="lblParty1" runat="server" class="lbl">
                                    Party 1</label>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpParty1" runat="server" CssClass="lkp" DataKey="INVCNG_PRTY_CD" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="19" TableName="48" HelpText="113,INVOICING_PARTY_INVCNG_PRTY_CD" ClientFilterFunction="applyDepoFilter" AllowSecondaryColumnSearch="true" SecondaryColumnName="INVCNG_PRTY_NM">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="INVCNG_PRTY_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="INVCNG_PRTY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                        <Inp:LookupColumn ColumnCaption="Party Name" ColumnName="INVCNG_PRTY_NM" Hidden="False" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Party Code. Click on the list for valid values" Operator="Equal" ReqErrorMessage="" Type="String" Validate="True" ValidationGroup="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                                <Inp:iLookup ID="lkpPartyMaster1" runat="server" CssClass="lkp" DataKey="PRTY_CD" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="19" TableName="18" HelpText="113,INVOICING_PARTY_INVCNG_PRTY_CD" ClientFilterFunction="applyDepoFilter" AllowSecondaryColumnSearch="true" SecondaryColumnName="INVCNG_PRTY_NM">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="PRTY_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="PRTY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                        <Inp:LookupColumn ColumnCaption="Party Name" ColumnName="PRTY_DSCRPTN_VC" Hidden="False" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Party Code. Click on the list for valid values" Operator="Equal" ReqErrorMessage="" Type="String" Validate="True" ValidationGroup="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                            <td>
                                <label id="lblNumber1" runat="server" class="lbl">
                                    Number</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtNumber1" runat="server" CssClass="txt" TabIndex="20" HelpText="114,GATEIN_DETAIL_SL_NMBR_1" TextMode="SingleLine" iCase="Upper">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblParty2" runat="server" class="lbl">
                                    Party 2</label>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpParty2" runat="server" CssClass="lkp" DataKey="INVCNG_PRTY_CD" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="21" TableName="48" HelpText="115,INVOICING_PARTY_INVCNG_PRTY_CD" ClientFilterFunction="applyDepoFilter" AllowSecondaryColumnSearch="true" SecondaryColumnName="INVCNG_PRTY_NM">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="INVCNG_PRTY_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="INVCNG_PRTY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                        <Inp:LookupColumn ColumnCaption="Party Name" ColumnName="INVCNG_PRTY_NM" Hidden="False" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Party Code. Click on the list for valid values" Operator="Equal" ReqErrorMessage="" Type="String" Validate="True" ValidationGroup="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                                <Inp:iLookup ID="lkpPartyMaster2" runat="server" CssClass="lkp" DataKey="PRTY_CD" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="21" TableName="18" HelpText="115,INVOICING_PARTY_INVCNG_PRTY_CD" ClientFilterFunction="applyDepoFilter" AllowSecondaryColumnSearch="true" SecondaryColumnName="INVCNG_PRTY_NM">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="PRTY_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="PRTY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                        <Inp:LookupColumn ColumnCaption="Party Name" ColumnName="PRTY_DSCRPTN_VC" Hidden="False" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Party Code. Click on the list for valid values" Operator="Equal" ReqErrorMessage="" Type="String" Validate="True" ValidationGroup="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                            <td>
                                <label id="lblNumber2" runat="server" class="lbl">
                                    Number</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtNumber2" runat="server" CssClass="txt" TabIndex="22" HelpText="116,GATEIN_DETAIL_SL_NMBR_2" TextMode="SingleLine" iCase="Upper">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblParty3" runat="server" class="lbl">
                                    Party 3</label>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpParty3" runat="server" CssClass="lkp" DataKey="INVCNG_PRTY_CD" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="23" TableName="48" HelpText="117,INVOICING_PARTY_INVCNG_PRTY_CD" ClientFilterFunction="applyDepoFilter" AllowSecondaryColumnSearch="true" SecondaryColumnName="INVCNG_PRTY_NM">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="INVCNG_PRTY_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="INVCNG_PRTY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                        <Inp:LookupColumn ColumnCaption="Party Name" ColumnName="INVCNG_PRTY_NM" Hidden="False" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Party Code. Click on the list for valid values" Operator="Equal" ReqErrorMessage="" Type="String" Validate="True" ValidationGroup="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                                <Inp:iLookup ID="lkpPartyMaster3" runat="server" CssClass="lkp" DataKey="PRTY_CD" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="23" TableName="18" HelpText="117,INVOICING_PARTY_INVCNG_PRTY_CD" ClientFilterFunction="applyDepoFilter" AllowSecondaryColumnSearch="true" SecondaryColumnName="INVCNG_PRTY_NM">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="PRTY_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="PRTY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                        <Inp:LookupColumn ColumnCaption="Party Name" ColumnName="PRTY_DSCRPTN_VC" Hidden="False" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Party Code. Click on the list for valid values" Operator="Equal" ReqErrorMessage="" Type="String" Validate="True" ValidationGroup="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                            <td>
                                <label id="lblNumber3" runat="server" class="lbl">
                                    Number</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtNumber3" runat="server" CssClass="txt" TabIndex="24" HelpText="118,GATEIN_DETAIL_SL_NMBR_3" TextMode="SingleLine" iCase="Upper">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblPortFuncCode" runat="server" class="lbl">
                        Port Function Code</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtPortFuncCode" runat="server" CssClass="txt" TabIndex="25" HelpText="119,GATEIN_DETAIL_PRT_FNC_CD" TextMode="SingleLine" iCase="Upper">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lbl" runat="server" class="lbl">
                        Port Name</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtPortName" runat="server" CssClass="txt" TabIndex="26" HelpText="120,GATEIN_DETAIL_PRT_NM" TextMode="SingleLine" iCase="Upper">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblVesselName" runat="server" class="lbl">
                        Vessel Name</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtVesselName" runat="server" CssClass="txt" TabIndex="29" HelpText="123,GATEIN_DETAIL_VSSL_NM" TextMode="SingleLine" iCase="Upper">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblPortNumber" runat="server" class="lbl">
                        Port Number</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtPortnumber" runat="server" CssClass="txt" TabIndex="27" HelpText="121,GATEIN_DETAIL_PRT_NO" TextMode="SingleLine" iCase="Upper">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblvoyageNumber" runat="server" class="lbl">
                        Voyage Number</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtVoyageNumber" runat="server" CssClass="txt" TabIndex="30" HelpText="124,GATEIN_DETAIL_VYG_NO" TextMode="SingleLine" iCase="Upper">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblPortLocCode" runat="server" class="lbl">
                        Port Loc Code</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtPortLocCode" runat="server" CssClass="txt" TabIndex="28" HelpText="122,GATEIN_DETAIL_PRT_LC_CD" TextMode="SingleLine" iCase="Upper">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblVesselIdCode" runat="server" class="lbl">
                        Vessel Id Code</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtVesselIdCode" runat="server" CssClass="txt" TabIndex="31" HelpText="125,GATEIN_DETAIL_VSSL_CD" TextMode="SingleLine" iCase="Upper">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="tblstd" align="left">
                        <tr>
                            <td>
                                <label id="lblShipper" runat="server" class="lbl">
                                    Shipper</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtShipper" runat="server" CssClass="txt" TabIndex="32" HelpText="126,GATEIN_DETAIL_SHPPR_NAM" TextMode="SingleLine" iCase="Upper">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblRailId" runat="server" class="lbl">
                                    Rail Id</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtRailId" runat="server" CssClass="txt" TabIndex="33" HelpText="127,GATEIN_DETAIL_RL_ID_VC" TextMode="SingleLine" iCase="Upper">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblRailRamp" runat="server" class="lbl">
                                    Rail Ramp Loc</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtRailRamp" runat="server" CssClass="txt" TabIndex="34" HelpText="128,GATEIN_DETAIL_RL_RMP_LOC" TextMode="SingleLine" iCase="Upper">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="2">
                    <fieldset>
                        <legend class="lbl">Haz Material</legend>
                        <table class="tblstd" align="center">
                            <tr>
                                <td>
                                    <label id="lblhazcode" runat="server" class="lbl">
                                        Code</label>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txthazCode" runat="server" CssClass="txt" TabIndex="35" HelpText="129,GATEIN_DETAIL_HAZ_MTL_CD" TextMode="SingleLine" iCase="Upper">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                    </Inp:iTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblhazDesc" runat="server" class="lbl">
                                        Desc</label>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txthazDesc" runat="server" CssClass="txt" TabIndex="36" HelpText="130,GATEIN_DETAIL_HAZ_MATL_DSC" TextMode="SingleLine" iCase="Upper">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False" ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                    </Inp:iTextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
                <td colspan="2">
                    <div id="dvButton" runat="server">
                        <table class="button" align="center">
                            <tr>
                                <td align="center">
                                    <ul>
                                        <li class="btn btn-small btn-success"><a href="#" data-corner="8px" id="btnSubmit" style="text-decoration: none; color: White; font-weight: bold" class="icon-save" runat="server" onclick="submitPage();return false;">&nbsp;Submit</a></li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div style="display: none">
            <sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();" onclientprint="null" />
        </div>
    </div>
    <asp:HiddenField ID="hdnID" runat="server" />
    </form>
</body>
</html>
