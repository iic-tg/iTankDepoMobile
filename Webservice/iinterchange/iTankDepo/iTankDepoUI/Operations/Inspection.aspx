<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inspection.aspx.vb" Inherits="Operations_Inspection" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr class="ctab" style="width: 100%; height: 8px;">
            <td align="left" valign="middle">
                <span id="spnEstHeader" class="ctabh"><span id="spnEstNo">Operations >> Inspection</span><span
                    class='olbl' style="vertical-align: middle"></span></span>
            </td>
            <td align="right">
                <nv:Navigation ID="navEstimate" runat="server" />
            </td>
        </tr>
    </table>
    <!-- UIG Fix -->
    <div class="tabdisplayGatePass" id="divCleaning" style="overflow-y: auto; overflow-x: auto; height: 100%">
        <table border="0" cellpadding="1" cellspacing="1" class="tblstd" style="width: 100%;">
            <tr>
                <td>
                    <Inp:iLabel ID="lblEquipmentNo" runat="server" CssClass="lbl">
                   Equipment No 
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtEquipmentNo" runat="server" CssClass="txtd" TabIndex="1" HelpText=""
                        TextMode="SingleLine" iCase="Upper" ReadOnly="True">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="True" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblEquipmentType" runat="server" CssClass="lbl">
               Type
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpEqpType" runat="server" CssClass="lkpd" DataKey="EQPMNT_TYP_CD"
                        DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="2" TableName="3" HelpText=""
                        ClientFilterFunction="" ReadOnly="true" AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="Right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                            LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Equipment Type Required" Type="String" Validate="True"
                            ValidationGroup="divHeader" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblCertificateNo" runat="server" CssClass="lbl">
                  Cleaning Certificate No
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLabel ID="lblCrtfct_No" runat="server" CssClass="blbl">
                    </Inp:iLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lblCustomer" runat="server" CssClass="lbl">
                   Customer 
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtCustomer" runat="server" CssClass="txtd" TabIndex="3" HelpText=""
                        TextMode="SingleLine" iCase="Upper" ReadOnly="True">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="True" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblPreviousCargo" runat="server" CssClass="lbl">
                   Previous Cargo
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpPreviousCargo" runat="server" CssClass="lkpd" DataKey="PRDCT_CD"
                        DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="4" TableName="46" HelpText="321,V_CLEANING_PRDCT_CD"
                        ClientFilterFunction="" AllowSecondaryColumnSearch="true" SecondaryColumnName="PRDCT_DSCRPTN_VC"
                        ReadOnly="true">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="PRDCT_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="PRDCT_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="PRDCT_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="Right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Previous Cargo. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="PreviousCargo Required" Type="String" Validate="True"
                            ValidationGroup="divCleaning" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                   
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblChemicalName" runat="server" CssClass="lbl">
                 Chemical Name
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtChemicalName" runat="server" CssClass="txtd" TabIndex="5" HelpText=""
                        TextMode="SingleLine" iCase="Upper" ReadOnly="True">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lblGateInDate" runat="server" CssClass="lbl">
                   In Date
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iDate ID="datGateInDate" TabIndex="6" runat="server" HelpText="" CssClass="datd"
                        MaxLength="11" iCase="Upper" ReadOnly="true">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup=""
                            Operator="LessThanEqual" LkpErrorMessage="Invalid In Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CustomValidationFunction="" CompareValidation="True"
                            ReqErrorMessage="In Date Required" CmpErrorMessage="In date Should not be greater than Current Date."
                            RangeValidation="False" CustomValidation="false" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblCleaningDate" runat="server" CssClass="lbl">
                  Original Cleaning Date
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel2" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iDate ID="datOriginalCleaningDate" TabIndex="7" runat="server" HelpText="322"
                        CssClass="dat" MaxLength="11" iCase="Upper" DataFormatString="{0:dd-MMM-yyyy}"
                        OnClientTextChange="fnSetLastCleaningDate">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup="divCleaning"
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Original Cleaning Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidateCleaningDate"
                            CompareValidation="True" ReqErrorMessage="Original Cleaning Date Required" CmpErrorMessage="Original Cleaning date cannot be greater than Current Date."
                            RangeValidation="False" CustomValidation="True" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                 <%--   <Inp:iLabel ID="lblOriginalEstimationDate" runat="server" CssClass="lbl">
                  Original Inspection Date  
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel6" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel>--%>
                          <Inp:iLabel ID="ILabel3" runat="server" CssClass="lbl">
                 Latest Cleaning Date
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel5" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel>
                </td>
                <td>
                    <%--<Inp:iDate ID="datOriginalInspectedDate" TabIndex="8" runat="server" HelpText="323"
                        CssClass="dat" MaxLength="11" iCase="Upper" DataFormatString="{0:dd-MMM-yyyy}">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup="divCleaning"
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Original Inspection Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidateOriginalInspectedDate"
                            CompareValidation="true" ReqErrorMessage="Original Inspection Date Required"
                            CmpErrorMessage="Original Inspection Date cannot be greater than Current Date."
                            RangeValidation="False" CustomValidation="true" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>--%>
                                 <Inp:iDate ID="datLastCleaningDate" TabIndex="9" runat="server" HelpText="421" CssClass="dat"
                        MaxLength="11" iCase="Upper" DataFormatString="{0:dd-MMM-yyyy}">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup="divCleaning"
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Latest Cleaning Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidateLastestCleaningDate"
                            CompareValidation="true" ReqErrorMessage="Latest Cleaning Date Required" CmpErrorMessage="Latest Cleaning date  cannot be greater than Current Date."
                            RangeValidation="False" CustomValidation="false" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
            </tr>
            <tr>
                <td>
              <%--      <Inp:iLabel ID="ILabel3" runat="server" CssClass="lbl">
                 Latest Cleaning Date
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel5" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel>--%>


                        <Inp:iLabel ID="lblOriginalEstimationDate" runat="server" CssClass="lbl">
                  Original Inspection Date  
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel6" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel>
                </td>
                <td>
       <%--             <Inp:iDate ID="datLastCleaningDate" TabIndex="9" runat="server" HelpText="421" CssClass="dat"
                        MaxLength="11" iCase="Upper" DataFormatString="{0:dd-MMM-yyyy}">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup="divCleaning"
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Latest Cleaning Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidateLastestCleaningDate"
                            CompareValidation="true" ReqErrorMessage="Latest Cleaning Date Required" CmpErrorMessage="Latest Cleaning date  cannot be greater than Current Date."
                            RangeValidation="False" CustomValidation="false" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>--%>

                       <Inp:iDate ID="datOriginalInspectedDate" TabIndex="8" runat="server" HelpText="323" OnClientTextChange="fnSetLastInspectionDate"
                        CssClass="dat" MaxLength="11" iCase="Upper" DataFormatString="{0:dd-MMM-yyyy}">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup="divCleaning"
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Original Inspection Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidateOriginalInspectedDate"
                            CompareValidation="true" ReqErrorMessage="Original Inspection Date Required"
                            CmpErrorMessage="Original Inspection Date cannot be greater than Current Date."
                            RangeValidation="False" CustomValidation="true" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblLastInspectedDate" runat="server" CssClass="lbl">
                 Latest Inspection Date
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel4" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iDate ID="datLastInspectedDate" runat="server" HelpText="324" CssClass="dat"
                        MaxLength="11" iCase="Upper" TabIndex="10" ReadOnly="False">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup="divCleaning"
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Latest Inspection Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="Latest Inspection Date Required"
                            CmpErrorMessage="Latest Inspection Date cannot be greater than Current Date."
                            RangeValidation="False" CustomValidation="true" CustomValidationFunction="ValidateLastInspectedDate" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblStatus" runat="server" CssClass="lbl">
                   Status
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpStatus" runat="server" CssClass="lkpd" DataKey="EQPMNT_STTS_CD"
                        DoSearch="True" iCase="None" MaxLength="10" TableName="12" HelpText="325,V_CLEANING_EQPMNT_STTS_CD"
                        ClientFilterFunction="" AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_STTS_DSCRPTN_VC"
                        ReadOnly="true">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="EQPMNT_STTS_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="EQPMNT_STTS_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_STTS_DSCRPTN_VC"
                                Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Equipment Status. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Equipment Status  Required" Type="String" Validate="True"
                            ValidationGroup="divHeader" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lblCleanedFor" runat="server" CssClass="lbl">
                    Cleaned For
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtCleanedFor" runat="server" CssClass="txt" TabIndex="11" HelpText="326,CLEANING_CLND_FR_VCR"
                        TextMode="SingleLine" iCase="None" ReadOnly="False">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblLocOfCleaning" runat="server" CssClass="lbl">
                    Location of Cleaning
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtLocOfCleaning" runat="server" CssClass="txt" TabIndex="12" HelpText="327,CLEANING_LCTN_OF_CLNG"
                        TextMode="SingleLine" iCase="None" ReadOnly="False">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lkpCleaningStatus1" runat="server" CssClass="lbl">
                Cleaning Status I
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpCleaningStatus" runat="server" CssClass="lkp" DataKey="ENM_CD"
                        DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="13" TableName="51" HelpText="328,CLEANING_EQPMNT_CLNNG_STTS_1_CD"
                        ClientFilterFunction="" AllowSecondaryColumnSearch="false" SecondaryColumnName="ENM_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Cleaning Status I" />
                            <Inp:LookupColumn ColumnName="ENM_DSCRPTN_VC" ControlToBind="" Hidden="true" ColumnCaption="Description" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="Right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                            LkpErrorMessage="Invalid Cleaning Status I. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Cleaning Status Required" Type="String" Validate="True"
                            ValidationGroup="divCleaning" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lblEqpmntCleaningStatus2" runat="server" CssClass="lbl">
              Cleaning Status II
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpEqpmntCleaningStatus2" runat="server" CssClass="lkp" DataKey="ENM_CD"
                        DoSearch="True" iCase="Upper" TabIndex="14" TableName="53" HelpText="329" MaxLength="9"
                        ClientFilterFunction="" SecondaryColumnName="ENM_DSCRPTN_VC" AllowSecondaryColumnSearch="false">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Cleaning Status II" />
                            <Inp:LookupColumn ColumnName="ENM_DSCRPTN_VC" ControlToBind="" Hidden="true" ColumnCaption="Description" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="left" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                            LkpErrorMessage="Invalid Cleaning Status II. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Cleaning Status Required" Type="String" Validate="True"
                            ValidationGroup="divCleaning" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblEqpmntCondition" runat="server" CssClass="lbl">
                 Condition
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpEqpmntCondition" runat="server" CssClass="lkp" DataKey="ENM_CD"
                        DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="15" TableName="52" HelpText="330,ENUM_ENM_CD"
                        ClientFilterFunction="" AllowSecondaryColumnSearch="false" SecondaryColumnName="ENM_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Condition" />
                            <Inp:LookupColumn ColumnName="ENM_DSCRPTN_VC" ControlToBind="" Hidden="true" ColumnCaption="Description" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="Right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                            LkpErrorMessage="Invalid Equipment Condition. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Cleaning Status Required" Type="String" Validate="True"
                            ValidationGroup="divCleaning" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblValveFittingCondition" runat="server" CssClass="lbl">
                 Valve and Fitting Condition
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpValveFittingCondition" runat="server" CssClass="lkp" DataKey="ENM_CD"
                        DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="16" TableName="52" HelpText="331,ENUM_ENM_CD"
                        ClientFilterFunction="" AllowSecondaryColumnSearch="false" SecondaryColumnName="ENM_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Valve and Fitting Condition" />
                            <Inp:LookupColumn ColumnName="ENM_DSCRPTN_VC" ControlToBind="" Hidden="true" ColumnCaption="Description" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="Right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                            LkpErrorMessage="Invalid Valve and Fitting Condition. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Valve and fitting Condition Required" Type="String"
                            Validate="True" ValidationGroup="divCleaning" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lblManLidSealNo" runat="server" CssClass="lbl">
                    Man Lid Seal No
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtManLidSealNo" runat="server" CssClass="txt" TabIndex="17" HelpText="333,CLEANING_MN_LID_SL_NO"
                        TextMode="SingleLine" iCase="None" ReadOnly="False" MaxLength="20">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblTopDischargSealNo" runat="server" CssClass="lbl">
                   Top Discharge/Airline Seal No
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtTopSealNo" runat="server" CssClass="txt" TabIndex="18" HelpText="334,CLEANING_TP_SL_NO"
                        TextMode="SingleLine" iCase="None" ReadOnly="False" MaxLength="20">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblOutletSealNo" runat="server" CssClass="lbl">
                   Bottom Outlet Seal No
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtOutletSealNo" runat="server" CssClass="txt" TabIndex="19" HelpText="335,CLEANING_BTTM_SL_NO"
                        TextMode="SingleLine" iCase="None" ReadOnly="False" MaxLength="20">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<Inp:iLabel ID="lblReferenceNo" runat="server" CssClass="lbl">
                Customer Ref No
                    </Inp:iLabel>--%>

                    <Inp:iLabel ID="lblReferenceNo" runat="server" CssClass="lbl">
                JTS EIR No
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtCustReferenceNo" runat="server" CssClass="txt" TabIndex="20"
                        HelpText="336,CLEANING_CSTMR_RFRNC_NO" TextMode="SingleLine" iCase="None" ReadOnly="true">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblCleaningReference" runat="server" CssClass="lbl">
                   Cleaning Ref No
                    </Inp:iLabel>
                 <%--   <Inp:iLabel ID="ILabel10" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel>--%>
                </td>
                <td>
                    <Inp:iTextBox ID="txtCleaningReferenceNo" runat="server" CssClass="txt" TabIndex="21"
                        HelpText="337,CLEANING_CLNNG_RFRNC_NO" TextMode="SingleLine" iCase="None"
                        ReadOnly="False">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false" 
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" CustomValidation="False" RegexValidation="False" RangeValidation="False"
                            ReqErrorMessage="Cleaning Reference Number Required" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblCleaningRate" runat="server" CssClass="lbl">
                  Cleaning Rate
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel7" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel>
                </td>
                <td>
                  <div id="divCurrencylkp" style="float:left;">
                    <Inp:iTextBox ID="txtCleaningRate" runat="server" CssClass="ntxto" TabIndex="22"
                        HelpText="458,CLEANING_CLNNG_RT" TextMode="SingleLine" iCase="Numeric" ReadOnly="False"
                        OnClientTextChange="FormatTwoDecimal">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Operator="GreaterThan"
                            ReqErrorMessage="Cleaning Rate Required" Type="Double" CustomValidationFunction=""
                            CustomValidation="false" CsvErrorMessage="Exist" Validate="True" RegErrorMessage="Invalid Cleaning Rate. Range must be from 0 to 999999999.99"
                            RegexValidation="true" RegularExpression="^\d{1,9}(\.\d{1,2})?$" CmpErrorMessage="Cleaning Rate must be greater than 0"
                            CompareValidation="false" ValueToCompare="0.00" />
                    </Inp:iTextBox>
                    </div>
                    <div id="divCurrency" style="float:left;">
                    <Inp:iLabel ID="ILabel8" runat="server" CssClass="lbl">
                (
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblCurrency" runat="server" CssClass="blbl">
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel9" runat="server" CssClass="lbl">
                   )
                    </Inp:iLabel>
                    </div>
                </td>
                <td>
                    <%--<Inp:iLabel ID="ILabel8" runat="server" CssClass="lbl">
                (
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblCurrency" runat="server" CssClass="blbl">
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel9" runat="server" CssClass="lbl">
                   )
                    </Inp:iLabel>--%>
                </td>
            </tr>
            <br />
            <tr>
                <td>
                    <Inp:iLabel ID="lblRemarks" runat="server" CssClass="lbl">
                  Remarks
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtRemarks" runat="server" CssClass="txt" TabIndex="23" HelpText="338"
                        TextMode="MultiLine" iCase="None" ReadOnly="False" Width="127px" MaxLength="500">
                        <Validator CustomValidateEmptyText="true" Operator="Equal" Type="String" Validate="true"
                            ReqErrorMessage="Remarks Required" ValidationGroup="divCleaning" LookupValidation="False"
                            CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="false"
                            RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblInvoiceTo" runat="server" CssClass="lbl">
                                                        Invoice To
                    </Inp:iLabel>
                </td>
                <td>
                    <asp:RadioButton ID="rbtnCustomer" runat="server" GroupName="GrpResponsibility" Text="Customer"  on />
                    <asp:RadioButton ID="rbtnParty" runat="server" GroupName="GrpResponsibility" Text="Party" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td id="divInvoicingParty">
                    <Inp:iLabel ID="lblInvoiceParty" runat="server" CssClass="lbl">
                                    Invoicing To
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel1" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel>
                </td>
                <td id="divInvoicingParty1">
                    <Inp:iLookup ID="lkpInvcngTo" runat="server" CssClass="lkp" DataKey="INVCNG_PRTY_CD"
                        DoSearch="True" iCase="Upper" TabIndex="24" TableName="71" HelpText="332,INVOICING_PARTY_INVCNG_PRTY_CD"
                        ClientFilterFunction="" AllowSecondaryColumnSearch="true" SecondaryColumnName="INVCNG_PRTY_NM"
                        OnClientTextChange="">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="INVCNG_PRTY_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="INVCNG_PRTY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnName="INVCNG_PRTY_NM" ControlToBind="" Hidden="False" ColumnCaption="Name" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="280px" VerticalAlign="Top"
                            HorizontalAlign="Right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="True" IsRequired="False" LkpErrorMessage="Invalid Invoicing To Party. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Invoicing Party Required" Type="String" Validate="True"
                            ValidationGroup="divCleaning" CustomValidation="true" CustomValidationFunction="ValidateInvoicingParty" />
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
                    <Inp:iLabel ID="lblCertofCleanliness" runat="server" CssClass="lbl">
                                   Additional Cleaning
                    </Inp:iLabel>
                </td>
                <td>
                    <asp:CheckBox ID="chkAdditionalCleaningBit" runat="server" CssClass="chk" TabIndex="25" />
                </td>
                <td>
                </td>
                <td>
                  <div id="divAddtionalCleaninglbl" style="display:none;">
                 <Inp:iLabel ID="ILabel10" runat="server" CssClass="lbl">
                                                        Additional Cleaning Procedure
                    </Inp:iLabel>
                    </div>
                </td>
                <td>
                <div id="divAddtionalCleaningLkp" style="display:none;">
                    <Inp:iLookup ID="lkpAddtnCleaning" runat="server" CssClass="lkp" DataKey="ENM_CD"
                        DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="26" TableName="86" HelpText="330,ENUM_ENM_CD"
                        ClientFilterFunction="" AllowSecondaryColumnSearch="false" SecondaryColumnName="ENM_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="ENM_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="Right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                            LkpErrorMessage="Invalid Additional Cleaning Status. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Additional Cleaning Status Required" Type="String"
                            Validate="True" ValidationGroup="divCleaning" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </div>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td colspan="2">
                </td>
            </tr>
        </table><br />
        <table align="center">
            <tr>
                <td>
                    <div id="divGenerateCleaningCertificate">
                        <a href="#" id="hypGenerateCleaningCertificate" class="btn btn-small btn-success"
                            style="font-weight: bold" onclick="PrintCleaningCertificate();" runat="server"><i
                                class="icon-print"></i>&nbsp;Print Cleaning Certificate</a>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="divSubmit">
        <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
            onClientPrint="null" />
    </div>
    <div id="divLockMessage" style="width: 100%; margin: 0px auto; text-align: center;
        font-weight: normal; font-weight: bold; height: 20px; background-color: #CDFFCC;
        border: 1px solid #07760D;">
    </div>
    <asp:HiddenField ID="hdnDepotID" runat="server" />
    <asp:HiddenField ID="hdnCustomerId" runat="server" />
    <asp:HiddenField ID="hdnGI_TRNSCTN_NO" runat="server" />
    <asp:HiddenField ID="hdnGateInDate" runat="server" />
    <asp:HiddenField ID="hdnCleaningID" runat="server" />
    <asp:HiddenField ID="hdnBillingFlag" runat="server" />
    <asp:HiddenField ID="hdnStr20EirNo" runat="server" />
    <asp:HiddenField ID="hdnCurrencyCode" runat="server" />
    <asp:HiddenField ID="hdnEditDate" runat="server" />
    <asp:HiddenField ID="hdnStatusId" runat="server" />
    <asp:HiddenField ID="hdnLockUserName" runat="server" />
    <asp:HiddenField ID="hdnIpError" runat="server" />
    <asp:HiddenField ID="hdnLockActivityName" runat="server" />
    <asp:HiddenField ID="hdnAddtnlClnngFlg" runat="server" />
    <asp:HiddenField ID="hdnSlabRateFlag" runat="server" />
    </form>
</body>
</html>
