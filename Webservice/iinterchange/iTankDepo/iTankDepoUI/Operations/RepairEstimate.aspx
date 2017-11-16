<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RepairEstimate.aspx.vb"
    Inherits="Operations_RepairEstimate" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .txtd
        {
        }
        .ublbl
        {
        }
        .style1
        {
        }
        .style4
        {
        }
        .style5
        {
        }
        
        .hide
        {
            display: none;
        }
        .show
        {
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <div>
            <div style="width: 100%">
                <table id="hdrEstimate" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr class="ctab" style="width: 100%; height: 8px;">
                        <td align="left" valign="middle">
                            <span id="spnEstHeader" class="ctabh"><span id="spnEstNo">Estimate</span><span class='olbl'
                                style="vertical-align: middle"> (<span id="spnPageTitle"> </span>)</span></span>
                        </td>
                        <td align="right">
                            <nv:Navigation ID="navEstimate" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <!-- UIG Fix -->
        <div class="tabdisplayGatePass" id="tabEstimate" style="overflow-y: auto; overflow-x: auto;
            height: auto">
            <table border="0" cellpadding="1" cellspacing="1" class="tblstd" style="width: 100%">
                <tr>
                    <td>
                        <div id="divHeader" style="width: 100%">
                            <table border="0" cellpadding="1" cellspacing="1" class="tblstd" style="width: 100%">
                                <tr>
                                    <td>
                                    </td>
                                  <%--  <td>
                                        <Inp:iLabel ID="lblEquipmentNo" runat="server" CssClass="lbl">
                    Equipment No
                                        </Inp:iLabel>
                                    </td>
                                    <td>
                                        <a id="hypEquipmentNo" runat="server" href="#" onclick="showEquipmentDetail();">
                                        </a>
                                    </td>--%>
                                       <td>
                                               <a id="hypEquipmentNo" runat="server" href="#" onclick="showEquipmentDetail();">Equipment No</a>
                                        </td>
                                        <td>
                                       
                                            <Inp:iTextBox ID="txtEquipmentNo" runat="server" CssClass="txtd" HelpText="" TextMode="SingleLine"
                                                iCase="None" ReadOnly="True">
                                                <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                    ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                                    IsRequired="True" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                            </Inp:iTextBox>
                                        </td>
                                    <td>
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblCustomer" runat="server" CssClass="lbl">
                   Customer 
                                        </Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iTextBox ID="txtCustomer" runat="server" CssClass="txtd" HelpText="" TextMode="SingleLine"
                                            TabIndex="1" iCase="Upper" ReadOnly="True">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                                IsRequired="True" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                        </Inp:iTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblgateInDate" runat="server" CssClass="lbl">
                   In Date
                                        </Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iDate ID="datGateInDate" runat="server" HelpText="" CssClass="datd" MaxLength="11"
                                            TabIndex="2" iCase="Upper" ReadOnly="true">
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
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblLaborRate" runat="server" CssClass="lbl">
                   Labor Rate
                                        </Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iTextBox ID="txtLaborRate" runat="server" CssClass="ntxtd" HelpText="" TextMode="SingleLine"
                                            TabIndex="3" iCase="Numeric" ReadOnly="True" MaxLength="11">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                                RegErrorMessage="Invalid Labor Rate. Labor Rate can be upto 8 digits and 2 decimal points"
                                                IsRequired="False" CustomValidation="False" RegexValidation="True" RangeValidation="False"
                                                RegularExpression="[0-9]{0,8}(\.[0-9]{1,2})?$" />
                                        </Inp:iTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblEstimateDate" runat="server" CssClass="lbl">
                   Estimate Date 
                                        </Inp:iLabel>
                                        <Inp:iLabel ID="ReqDate" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <Inp:iDate ID="datEstimationDate" TabIndex="4" runat="server" HelpText="346" CssClass="dat"
                                            iCase="None" MaxLength="11">
                                            <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup="divHeader"
                                                Operator="LessThanEqual" LkpErrorMessage="Invalid Estimate Date. Click on the calendar icon for valid values"
                                                Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidatePreviousActivity"
                                                CompareValidation="True" ReqErrorMessage="Estimate Date Required" CmpErrorMessage="Estimate date Should not be greater than Current Date."
                                                RangeValidation="False" CustomValidation="True" />
                                            <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        </Inp:iDate>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblInputOrginalEstimateDate" CssClass="lbl" runat="server">
                                        Org. Est. Date
                                        </Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iDate ID="datOrginalEstimateDate" TabIndex="5" runat="server" HelpText="" CssClass="datd"
                                            MaxLength="11" iCase="Upper" ReadOnly="true">
                                            <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="divHeader"
                                                Operator="LessThanEqual" LkpErrorMessage="" Validate="True" CsvErrorMessage=""
                                                CustomValidationFunction="" CompareValidation="false" CmpErrorMessage="" />
                                            <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        </Inp:iDate>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblStatus" runat="server" CssClass="lbl">
                   Status                                    
                                        </Inp:iLabel>
                                        <Inp:iLabel ID="ILabel1" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                        &nbsp;
                                    </td>
                    </td>
                    <td>
                        <Inp:iLookup ID="lkpStatus" runat="server" CssClass="lkp" DataKey="EQPMNT_STTS_CD"
                            DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="7" TableName="54" HelpText="347,EQUIPMENT_STATUS_EQPMNT_STTS_CD"
                            ClientFilterFunction="applyWorkFlowStatusFilter" ReadOnly="false">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="EQPMNT_STTS_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="EQPMNT_STTS_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_STTS_DSCRPTN_VC"
                                    Hidden="False" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                                HorizontalAlign="Right" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Equipment Status. Click on the list for valid values"
                                Operator="Equal" ReqErrorMessage="Equipment Status  Required" Type="String" Validate="True"
                                ValidationGroup="divHeader" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                        <Inp:iLookup ID="lkpEquipStatusGWS" runat="server" CssClass="lkp" DataKey="STTS_CD"
                            DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="6" TableName="100" HelpText="347,EQUIPMENT_STATUS_EQPMNT_STTS_CD"
                            ClientFilterFunction="applyWorkFlowStatusFilterGWS" ReadOnly="false">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="STTS_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="STTS_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="STTS_DSCRPTN_VC" Hidden="False" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                                HorizontalAlign="Right" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Equipment Status. Click on the list for valid values"
                                Operator="Equal" ReqErrorMessage="Equipment Status  Required" Type="String" Validate="True"
                                ValidationGroup="divHeader" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblCertofCleanliness" runat="server" CssClass="lbl">
                                    Cert. of Cleanliness
                        </Inp:iLabel>
                        <Inp:iLabel ID="lblConsignee" runat="server" CssClass="lbl">
                                    Consignee
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtConsignee" runat="server" CssClass="txt" TabIndex="8" TextMode="SingleLine"
                            iCase="None" ReadOnly="False" HelpText="596">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                IsRequired="false" CustomValidation="False" RangeValidation="False" RegexValidation="true"
                                RegularExpression="^[a-zA-Z0-9]+$" RegErrorMessage="Only alphabets and numbers allowed."
                                ReqErrorMessage="" />
                        </Inp:iTextBox>
                        <asp:CheckBox ID="chkCertofCleanlinessBit" runat="server" CssClass="chk" TabIndex="9"
                            Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="vertical-align: middle;">
                        <Inp:iLabel ID="lblLastTestType" runat="server" CssClass="lbl">
                                    Last Test Type
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iLookup ID="lkpLastTestType" runat="server" CssClass="lkp" DataKey="ENM_CD"
                            OnClientTextChange="bindTestType" DoSearch="True" iCase="Upper" TabIndex="10"
                            TableName="45" HelpText="353,ENUM_ENM_CD" ClientFilterFunction="" ReadOnly="false">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                                LkpErrorMessage="Invalid Last Test Type. Click on the list for valid values"
                                Operator="Equal" Type="String" Validate="True" ValidationGroup="divHeader" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblNextTestType" runat="server" CssClass="lbl">
                                    Next Test Type
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtNextTest" runat="server" CssClass="txtd" TabIndex="11" HelpText=""
                            TextMode="SingleLine" iCase="Upper" ReadOnly="true">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                IsRequired="false" CustomValidation="False" RegexValidation="False" RangeValidation="False"
                                ReqErrorMessage="" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblLastTestDate" runat="server" CssClass="lbl">
                                    Last Test Date
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iDate ID="datLastTestDate" TabIndex="12" runat="server" HelpText="352" CssClass="dat"
                            iCase="Upper" ReadOnly="false" OnClientTextChange="calculateNextTestDate" MaxLength="11">
                            <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="divHeader"
                                Operator="LessThanEqual" LkpErrorMessage="Invalid Last Test Date. Click on the calendar icon for valid values"
                                Validate="True" CsvErrorMessage="" CustomValidationFunction="" CompareValidation="false"
                                CmpErrorMessage="" />
                            <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iDate>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblNextTestDate" runat="server" CssClass="lbl">
                                    Next Test Date
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iDate ID="datNextTestDate" TabIndex="13" runat="server" HelpText="" CssClass="datd"
                            MaxLength="11" iCase="Upper" ReadOnly="true">
                            <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="tabInputRedelivery"
                                Operator="LessThanEqual" LkpErrorMessage="Invalid Next Test Date. Click on the calendar icon for valid values"
                                Validate="True" CsvErrorMessage="" CustomValidationFunction="" CompareValidation="false"
                                CmpErrorMessage="" />
                            <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iDate>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblLastSurveyor" runat="server" CssClass="lbl"> Last Surveyor      
                        </Inp:iLabel>
                        <Inp:iLabel ID="lblYardLocation" runat="server" CssClass="lbl"> Yard Location
                        </Inp:iLabel>
                    </td>
                    <td class="style4">
                        <Inp:iTextBox ID="txtLastSurveyor" runat="server" CssClass="txt" TabIndex="14" HelpText="354,EQUIPMENT_INFORMATION_LST_SRVYR_NM"
                            TextMode="SingleLine" iCase="Upper" ReadOnly="false">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                IsRequired="false" CustomValidation="False" RegexValidation="False" RangeValidation="False"
                                ReqErrorMessage="Last Surveyor Name Required" />
                        </Inp:iTextBox>
                        <Inp:iTextBox ID="txtYardLocation" runat="server" CssClass="txtd" TabIndex="14" HelpText="354,EQUIPMENT_INFORMATION_LST_SRVYR_NM"
                            TextMode="SingleLine" iCase="Upper" ReadOnly="true">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                IsRequired="false" CustomValidation="False" RegexValidation="False" RangeValidation="False"
                                ReqErrorMessage="Yard Location Required" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblValidityYear" runat="server" CssClass="lbl">
                                    Validity Period for Test 
                        </Inp:iLabel>
                        <Inp:iLabel ID="lblUnit" runat="server" CssClass="lbl">
                                    Unit
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtValidityYear" runat="server" CssClass="ntxtd" HelpText="" TextMode="SingleLine"
                            TabIndex="16" iCase="Upper" ReadOnly="true">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                IsRequired="True" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                        </Inp:iTextBox>
                        <Inp:iLookup ID="lkpUnit" runat="server" CssClass="lkp" DataKey="UNT_CD" OnClientTextChange=""
                            DoSearch="True" iCase="Upper" TabIndex="16" TableName="15" HelpText="101" ClientFilterFunction=""
                            ReadOnly="false">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="UNT_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="UNT_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnName="UNT_DSCRPTN_VC" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                                LkpErrorMessage="Invalid Unit. Click on the list for valid values" Operator="Equal"
                                Type="String" Validate="True" ValidationGroup="divHeader" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </td>
                    <td class="style4">
                    </td>
                    <td class="style4">
                        <Inp:iLabel ID="lblRepairType" runat="server" CssClass="lbl">
                                    Repair Type
                        </Inp:iLabel>
                        <Inp:iLabel ID="lblMeasure" runat="server" CssClass="lbl">
                                    Measure
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iLookup ID="lkpRepairType" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                            iCase="Upper" TabIndex="18" TableName="55" HelpText="355,ENUM_ENM_CD" ClientFilterFunction=""
                            ReadOnly="false">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                                LkpErrorMessage="Invalid Repair Type. Click on the list of valid values" Operator="Equal"
                                Type="String" Validate="True" ValidationGroup="divHeader" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                        <Inp:iLookup ID="lkpMeasure" runat="server" CssClass="lkp" DataKey="MSR_CD" OnClientTextChange=""
                            DoSearch="True" iCase="Upper" TabIndex="18" TableName="13" HelpText="100" ClientFilterFunction=""
                            ReadOnly="false">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="MSR_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="MSR_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnName="MSR_DSCRPTN_VC" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                                LkpErrorMessage="Invalid Unit. Click on the list for valid values" Operator="Equal"
                                Type="String" Validate="True" ValidationGroup="divHeader" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblRemarks" runat="server" CssClass="lbl">
                                                      Remarks
                        </Inp:iLabel>
                    </td>
                    <td class="style4">
                        <Inp:iTextBox ID="txtRemarks" runat="server" CssClass="txt" TabIndex="20" HelpText="461"
                            Width="110px" TextMode="MultiLine" ToolTip="" iCase="None" ReadOnly="false" MaxLength="500">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                                ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                RegErrorMessage="" IsRequired="False" CustomValidation="False" RegexValidation="false"
                                RangeValidation="False" RegularExpression="" />
                        </Inp:iTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblInvoicingParty" runat="server" CssClass="lbl">
                                                        Invoicing Party
                        </Inp:iLabel>
                        <Inp:iLabel ID="lblPrevONHLocation" runat="server" CssClass="lbl">
                                                       Previous On Hire Location
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iLookup ID="lkpInvoiceparty" runat="server" CssClass="lkp" DataKey="INVCNG_PRTY_CD"
                            OnClientTextChange="" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="22"
                            TableName="48" HelpText="367,INVOICING_PARTY_INVCNG_PRTY_CD" ClientFilterFunction="">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="INVCNG_PRTY_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="INVCNG_PRTY_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnName="INVCNG_PRTY_NM" ControlToBind="" Hidden="False" ColumnCaption="Name" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="350px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                ReqErrorMessage="Invoicing Party Required" LkpErrorMessage="Invalid Invoicing Party. Click on the list for valid values"
                                Operator="Equal" Type="String" Validate="True" ValidationGroup="divResponsibility" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                        <Inp:iLookup ID="lkpPrevONHLocation" runat="server" CssClass="lkp" DataKey="PRT_CD"
                            OnClientTextChange="" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="21"
                            TableName="28" HelpText="102,PORT_PRT_CD" ClientFilterFunction="">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="PRT_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="PRT_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnName="PRT_DSCRPTN_VC" ControlToBind="" Hidden="False" ColumnCaption="Name" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="350px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                ReqErrorMessage="Port Required" LkpErrorMessage="Invalid Port Values. Click on the list for valid values"
                                Operator="Equal" Type="String" Validate="True" ValidationGroup="divResponsibility" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblApprovalref" runat="server" CssClass="lbl" Text=" Cust. App Ref">
                                                      
                        </Inp:iLabel>
                        <Inp:iLabel ID="lblApprovRefReq" runat="server" CssClass="lblReq" ToolTip="*">
  *</Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtApprovalRef" runat="server" CssClass="txt" HelpText="462,REPAIR_ESTIMATE_OWNR_APPRVL_RF"
                            iCase="None" OnClientTextChange="" ReadOnly="false" TabIndex="27" TextMode="SingleLine"
                            ToolTip="Customer Approval Ref">
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" CustomValidation="False"
                                CustomValidationFunction="" IsRequired="true" LookupValidation="False" Operator="Equal"
                                RangeValidation="False" RegErrorMessage="" RegexValidation="false" RegularExpression=""
                                ReqErrorMessage="Customer Approval Reference Required" Type="String" Validate="True"
                                ValidationGroup="divApproval" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <Inp:iLabel ID="lblApprovalDate" runat="server" CssClass="lbl">
                                                        Approval Date
                                    </Inp:iLabel>
                                    <%--<Inp:iLabel ID="ILabel2" runat="server" ToolTip="*" CssClass="lblReq">
                                                    *</Inp:iLabel>--%>
                                    &nbsp;
                                    <Inp:iLabel ID="lblApprovalDateSym" runat="server" ToolTip="*" CssClass="lblReq">
                                                    *</Inp:iLabel>
                                </td>
                    </td>
                </tr>
            </table>
            <td>
                <Inp:iDate ID="datApprovalDate" TabIndex="28" runat="server" HelpText="" CssClass="dat"
                    iCase="Upper" ReadOnly="false">
                    <Validator CustomValidateEmptyText="False" IsRequired="true" Type="Date" ValidationGroup="divApproval"
                        CustomValidation="true" Operator="LessThanEqual" LkpErrorMessage="Invalid Approval Date. Click on the calendar icon for valid values"
                        Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidatePreviousActivity"
                        ReqErrorMessage="Approval Date is Required" CompareValidation="true" CmpErrorMessage="Approval Date should not be Greater than Current Date" />
                    <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                </Inp:iDate>
            </td>
            <td>
            </td>
            <td>
                <div id="divlblApprovalAmt">
                    <Inp:iLabel ID="lblApprovalAmount" runat="server" CssClass="lbl">
                                                 Approved Amount
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblPartyRef" runat="server" CssClass="lbl">
                                                       Party App Ref
                    </Inp:iLabel>
                </div>
            </td>
            <td>
                <Inp:iTextBox ID="txtPartyRef" runat="server" CssClass="txt" TabIndex="28" HelpText="543,REPAIR_ESTIMATE_OWNR_APPRVL_RF"
                    TextMode="SingleLine" iCase="None" ReadOnly="false" ToolTip="Party Approval Ref"
                    OnClientTextChange="">
                    <Validator CustomValidateEmptyText="true" Operator="Equal" Type="String" Validate="True"
                        ReqErrorMessage="Party Approval Reference Required" ValidationGroup="divApproval"
                        LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="validatePartyRefence"
                        RegErrorMessage="" IsRequired="false" CustomValidation="true" RegexValidation="false"
                        RangeValidation="False" RegularExpression="" />
                </Inp:iTextBox>
                <Inp:iTextBox ID="txtApprovalAmount" runat="server" CssClass="ntxtd" TabIndex="30"
                    HelpText="543,REPAIR_ESTIMATE_OWNR_APPRVL_RF" TextMode="SingleLine" iCase="None"
                    ReadOnly="True" ToolTip="Party Approval Ref" OnClientTextChange="">
                    <Validator CustomValidateEmptyText="true" Operator="Equal" Type="String" Validate="True"
                        ReqErrorMessage="Party Approval Reference Required" ValidationGroup="divApproval"
                        LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="validatePartyRefence"
                        RegErrorMessage="" IsRequired="false" CustomValidation="true" RegexValidation="false"
                        RangeValidation="False" RegularExpression="" />
                </Inp:iTextBox>
            </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblPrevONHDate" runat="server" CssClass="lbl">
                                                      Previous On Hire Date
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iDate ID="datPreviousONH" TabIndex="26" runat="server" HelpText="103" CssClass="dat"
                        MaxLength="11" iCase="None">
                        <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="divHeader"
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Estimate Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CustomValidationFunction="" CompareValidation="True"
                            ReqErrorMessage="Previous On Hire Date Required" CmpErrorMessage="Previous On Hire date Should not be greater than Current Date."
                            RangeValidation="False" CustomValidation="False" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
                <td>
                    <%--  <Inp:iLabel ID="lblSurveyorName" runat="server" CssClass="lbl">
                                                        Surveyor Name

                                        </Inp:iLabel>
                                        <Inp:iLabel ID="ILabel2" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>--%>
                </td>
                <td>
                    <%--  <Inp:iTextBox ID="txtSurveyorName" runat="server" CssClass="txt" TabIndex="18" HelpText="440,REPAIR_ESTIMATE_SRVYR_NM"
                                            TextMode="SingleLine" iCase="Upper" ReadOnly="false" ToolTip="Surveyor Name"
                                            OnClientTextChange="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                ValidationGroup="divSurvey" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                                RegErrorMessage="" IsRequired="true" CustomValidation="False" RegexValidation="false"
                                                ReqErrorMessage="Surveyor Name Required" RangeValidation="False" RegularExpression="" />
                                        </Inp:iTextBox>--%>
                    <Inp:iLabel ID="lblAgent" runat="server" CssClass="lbl">Agent                                                                                         </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtAgentName" runat="server" CssClass="txtd" TabIndex="23" HelpText="461"
                        Width="128px" ToolTip="" iCase="None" ReadOnly="True" MaxLength="500">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            RegErrorMessage="" IsRequired="False" CustomValidation="False" RegexValidation="false"
                            RangeValidation="False" RegularExpression="" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblBillTo" runat="server" CssClass="lbl">Bill To</Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpBillTo" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                        iCase="Upper" MaxLength="10" TabIndex="24" TableName="95" HelpText="609" ClientFilterFunction=""
                        OnClientTextChange="ShowConfirmationMessage" ReadOnly="false">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="ENM_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="ENM_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="350px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="true" CustomValidation="true"
                            CustomValidationFunction="validateBillTo" ReqErrorMessage="Bill To Required"
                            LkpErrorMessage="Invalid Bill To. Click on the list for valid values" Operator="Equal"
                            Type="String" Validate="True" ValidationGroup="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblTaxRate" runat="server" CssClass="lbl">
                                                        Tax Rate %
                    </Inp:iLabel>
                </td>
                <td>
                    <div id="divtxtApprovalAmt">
                        <Inp:iTextBox ID="txtTaxRate" runat="server" CssClass="txt" TabIndex="25" HelpText="440,REPAIR_ESTIMATE_SRVYR_NM"
                            TextMode="SingleLine" iCase="Numeric" ReadOnly="false" ToolTip="Tax Rate" OnClientTextChange="formatTaxRate">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                ValidationGroup="" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                RegErrorMessage="Invalid Tax Rate. Tax Rate should Be from 0 to 100.00 with two decimal places"
                                IsRequired="false" CustomValidation="False" RegexValidation="True" RangeValidation="False"
                                RegularExpression="^(100([\.][0]{1,})?$|[0-9]{1,2}([\.][0-9]{1,2})?)$" ReqErrorMessage="" />
                        </Inp:iTextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="7">
<%--                    <div id="divSurvey">
                        <table>
                            <tr>
                                <td>
                                        <Inp:iLabel ID="lblSurveyDate" runat="server" CssClass="lbl">
                                                        Survey Completion Date
                                                        </Inp:iLabel>
                                                        <Inp:iLabel ID="ILabel3" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                </td>
                                <td>
                                     <Inp:iDate ID="datSurveyDate" TabIndex="19" runat="server" HelpText="441" CssClass="dat"
                                                            iCase="Upper" ReadOnly="false" MaxLength="11">
                                                            <Validator CustomValidateEmptyText="False" IsRequired="true" Type="Date" ValidationGroup="divSurvey"
                                                                CustomValidation="true" Operator="LessThanEqual" LkpErrorMessage="Invalid Survey Date. Click on the calendar icon for valid values"
                                                                Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidatePreviousActivity"
                                                                ReqErrorMessage="Survey Date Required" CompareValidation="True" CmpErrorMessage="Survey Date should not be Greater than Current Date" />
                                                            <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                        </Inp:iDate>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <div id="divSurvey">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <Inp:iLabel ID="lblSurveyorName" runat="server" CssClass="lbl">
                                                        Surveyor Name

                                                        </Inp:iLabel>
                                                        <Inp:iLabel ID="ILabel4" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                                    </td>
                                                    <td>
                                                        <Inp:iTextBox ID="txtSurveyorName" runat="server" CssClass="txt" TabIndex="18" HelpText="440,REPAIR_ESTIMATE_SRVYR_NM"
                                                            TextMode="SingleLine" iCase="Upper" ReadOnly="false" ToolTip="Surveyor Name"
                                                            OnClientTextChange="">
                                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                                ValidationGroup="divSurvey" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                                                RegErrorMessage="" IsRequired="true" CustomValidation="False" RegexValidation="false"
                                                                ReqErrorMessage="Surveyor Name Required" RangeValidation="False" RegularExpression="" />
                                                        </Inp:iTextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <Inp:iLabel ID="lblSurveyDate" runat="server" CssClass="lbl">
                                                        Survey Completion Date
                                                        </Inp:iLabel>
                                                        <Inp:iLabel ID="ILabel5" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                                    </td>
                                                    <td>
                                                        <Inp:iDate ID="datSurveyDate" TabIndex="19" runat="server" HelpText="441" CssClass="dat"
                                                            iCase="Upper" ReadOnly="false" MaxLength="11">
                                                            <Validator CustomValidateEmptyText="False" IsRequired="true" Type="Date" ValidationGroup="divSurvey"
                                                                CustomValidation="true" Operator="LessThanEqual" LkpErrorMessage="Invalid Survey Date. Click on the calendar icon for valid values"
                                                                Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidatePreviousActivity"
                                                                ReqErrorMessage="Survey Date Required" CompareValidation="True" CmpErrorMessage="Survey Date should not be Greater than Current Date" />
                                                            <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                        </Inp:iDate>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                </td>
                <td colspan="4" rowspan="4" style="vertical-align: top;">
                    <div>
                        <fieldset class="lbl" id="fldCostSummary">
                            <legend class="blbl">Estimate Cost Summary</legend>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 10%;">
                                        <Inp:iLabel ID="lblOrginalCost" runat="server" CssClass="lbl">
                                                        Original
                                        </Inp:iLabel>
                                    </td>
                                    <td style="width: 1%;">
                                        :
                                    </td>
                                    <td style="width: 30%;" align="right">
                                        <Inp:iLabel ID="hypOrginalCost" runat="server" CssClass="lbl">0.00</Inp:iLabel>
                                    </td>
                                    <td style="width: 2%;">
                                        <Inp:iLabel ID="lblOrgCurrencyCode" runat="server" CssClass="lbl" Font-Bold="true"
                                            ForeColor="Blue">()</Inp:iLabel>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 30%;" align="right">
                                        (<Inp:iLabel ID="hypOrgDepotCost" runat="server" CssClass="lbl">0.00</Inp:iLabel>
                                    </td>
                                    <td style="width: 27%;">
                                        <Inp:iLabel ID="lblOrgDepotCurrency" runat="server" CssClass="lbl" Font-Bold="true"
                                            ForeColor="Blue">()</Inp:iLabel>
                                        )
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <Inp:iLabel ID="lblTotalCost" runat="server" CssClass="lbl">
                                                        Revised
                                        </Inp:iLabel>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <Inp:iLabel ID="hypTotalCost" runat="server" CssClass="lbl">0.00</Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblCurrencyCode" runat="server" CssClass="lbl" Font-Bold="true" ForeColor="Blue">()</Inp:iLabel>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        (<Inp:iLabel ID="hypDepotCost" runat="server" CssClass="lbl">0.00</Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblDepotCurrency" runat="server" CssClass="lbl" Font-Bold="true"
                                            ForeColor="Blue">()</Inp:iLabel>
                                        )
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <div id="divAppCostSummary" style="display: none;">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 9%;">
                                                        <Inp:iLabel ID="lblInputApprovalAmount" runat="server" CssClass="lbl">Approved
                                                        </Inp:iLabel>
                                                    </td>
                                                    <td style="width: 2%;">
                                                        :
                                                    </td>
                                                    <td style="width: 30%;" align="right">
                                                        <Inp:iLabel ID="hypAppCost" runat="server" CssClass="lbl">0.00</Inp:iLabel>
                                                    </td>
                                                    <td>
                                                        <Inp:iLabel ID="lblAppCurrency" runat="server" CssClass="lbl" Font-Bold="true" ForeColor="Blue">()</Inp:iLabel>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 30%;" align="right">
                                                        (<Inp:iLabel ID="hypAppDepCost" runat="server" CssClass="lbl">0.00</Inp:iLabel>
                                                    </td>
                                                    <td style="width: 27%;">
                                                        <Inp:iLabel ID="lblAppDepCurrency" runat="server" CssClass="lbl" Font-Bold="true"
                                                            ForeColor="Blue">()</Inp:iLabel>
                                                        )
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
             <%--<tr   id="divSurvey">
                                <td >
                                
                                    </td>
                                    <td>
                                    </td>
                                    
                                   
                                    <td  >
                                        <Inp:iLabel ID="lblSurveyorName" runat="server" CssClass="lbl">
                                                        Surveyor Name

                                        </Inp:iLabel>
                                        <Inp:iLabel ID="ILabel2" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iTextBox ID="txtSurveyorName" runat="server" CssClass="txt" TabIndex="18" HelpText="440,REPAIR_ESTIMATE_SRVYR_NM"
                                            TextMode="SingleLine" iCase="Upper" ReadOnly="false" ToolTip="Surveyor Name"
                                            OnClientTextChange="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                ValidationGroup="divSurvey" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                                RegErrorMessage="" IsRequired="true" CustomValidation="False" RegexValidation="false"
                                                ReqErrorMessage="Surveyor Name Required" RangeValidation="False" RegularExpression="" />
                                        </Inp:iTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <Inp:iLabel ID="lblSurveyDate" runat="server" CssClass="lbl">
                                                        Survey Completion Date
                                        </Inp:iLabel>
                                        <Inp:iLabel ID="ILabel3" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                    </td>
                                    <td>
                                        <Inp:iDate ID="datSurveyDate" TabIndex="19" runat="server" HelpText="441" CssClass="dat"
                                            iCase="Upper" ReadOnly="false" MaxLength="11">
                                            <Validator CustomValidateEmptyText="False" IsRequired="true" Type="Date" ValidationGroup="divSurvey"
                                                CustomValidation="true" Operator="LessThanEqual" LkpErrorMessage="Invalid Survey Date. Click on the calendar icon for valid values"
                                                Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidatePreviousActivity"
                                                ReqErrorMessage="Survey Date Required" CompareValidation="True" CmpErrorMessage="Survey Date should not be Greater than Current Date" />
                                            <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        </Inp:iDate>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>--%>
            <tr>
                <td>
                </td>
                <td colspan="2" id="divPhotoUpload">
                    <a id="hypPhotoUpload" href="#" onclick="showPhotoUpload();">Attach Files</a>
                    <br />
                    <Inp:iLabel ID="lblLineDetailHeader" runat="server" CssClass="ublbl">Line Detail</Inp:iLabel>
                </td>
                <td id="divlineDetail">
                  <Inp:iLabel ID="ILabel2" runat="server" CssClass="ublbl">Line Detail</Inp:iLabel>
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
            <tr id="divTariff">
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblTariffGroupCode" runat="server" CssClass="lbl">
                                        Tariff Group
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblCustomerTariff" runat="server" CssClass="lbl">
                                        Tariff Code
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpTariffGroup" runat="server" CssClass="lkp" DataKey="TRFF_GRP_CD"
                        OnClientTextChange="onClearTariffCode" DoSearch="True" iCase="Upper" TabIndex="31"
                        TableName="57" HelpText="356,TARIFF_GROUP_TRFF_GRP_CD" ClientFilterFunction=""
                        ReadOnly="false">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="TRFF_GRP_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="TRFF_GRP_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="TRFF_GRP_DESCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                            HorizontalAlign="Left" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                            LkpErrorMessage="Invalid Tariff Group Code. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Tariff Group Code Required" Type="String" Validate="True"
                            ValidationGroup="divHeader" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                    <%--  <Inp:iLookup ID="lkpCustomerTariff" runat="server" CssClass="lkp" DataKey="TRFF_CD_DTL_CD"
                                            DoSearch="True" iCase="Upper" TabIndex="31" TableName="98" HelpText="357,TARIFF_CODE_TRFF_CD_CD"
                                            ClientFilterFunction="filterCustomerTariffCode" ReadOnly="false">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="TRFF_CD_DTL_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnName="TRFF_CD_DTL_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="TRFF_CD_DTL_DSC" Hidden="False" />
                                                <Inp:LookupColumn ColumnName="DTL_TYP" ColumnCaption="Tariff Type" Hidden="False" />
                                            </LookupColumns>
                                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                                                HorizontalAlign="Left" />
                                            <Validator CsvErrorMessage="" CustomValidation="true" CustomValidationFunction="fnLoadTariff"
                                                CustomValidateEmptyText="False" IsRequired="false" LkpErrorMessage="Invalid Tariff Code. Click on the list for valid values"
                                                Operator="Equal" ReqErrorMessage="Tariff Code Required" Type="String" Validate="True"
                                                ValidationGroup="divHeader" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        </Inp:iLookup>--%>
                    <Inp:iLookup ID="lkpCustomerTariff" runat="server" CssClass="lkp" DataKey="TRFF_CD_DTL_CD"
                        DoSearch="True" iCase="Upper" TabIndex="31" TableName="98" HelpText="357,TARIFF_CODE_TRFF_CD_CD"
                        AutoSearch="false" ClientFilterFunction="filterCustomerTariffCode" ReadOnly="false"
                        OnClientTextChange="CustomerTarifftTextChange">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="TRFF_CD_DTL_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="TRFF_CD_DTL_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="TRFF_CD_DTL_DSC" Hidden="False" />
                            <Inp:LookupColumn ColumnName="DTL_TYP" ColumnCaption="Tariff Type" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                            HorizontalAlign="Left" />
                        <Validator CsvErrorMessage="" CustomValidation="false" CustomValidationFunction=""
                            CustomValidateEmptyText="False" IsRequired="false" LkpErrorMessage="Invalid Tariff Code. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Tariff Code Required" Type="String" Validate="True"
                            ValidationGroup="divHeader" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lbltariffCode" runat="server" CssClass="lbl">
                                        Tariff
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpTariffCode" runat="server" CssClass="lkp" DataKey="TRFF_CD_CD"
                        DoSearch="True" iCase="Upper" TabIndex="33" TableName="44" HelpText="357,TARIFF_CODE_TRFF_CD_CD"
                        ClientFilterFunction="filterTariffCode" ReadOnly="false">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="TRFF_CD_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="TRFF_CD_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="TRFF_CD_DESCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                            HorizontalAlign="Right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                            LkpErrorMessage="Invalid Tariff Code. Click on the list for valid values" Operator="Equal"
                            ReqErrorMessage="Tariff Code Required" Type="String" Validate="True" ValidationGroup="divHeader" />
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
                        <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="hypBindLineDetail"
                            style="text-decoration: none; color: White; font-weight: bold" class="icon-search"
                            runat="server" onclick="tariffBindLineDetail(); return false;" tabindex="34">&nbsp;Fetch</a></li>
                    </ul>
                </td>
            </tr>
            </table>
        </div>
        </td> </tr>
        <tr>
            <td>
                <br />
                <Inp:iLabel ID="ILabel3" runat="server" ToolTip="*" CssClass="lblReq">Note: Row highlighted in Yellow colour indicates there is a modification in Man Hour or Material Cost from Tariff.  </Inp:iLabel>
                <br />
                <div id="divLineDetail" class="tabLineDetail">
                    <iFg:iFlexGrid ID="ifgLineDetail" runat="server" AllowStaticHeader="True" DataKeyNames="RPR_ESTMT_DTL_ID"
                        Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        Scrollbars="Auto" ShowEmptyPager="False" StaticHeaderHeight="100px" Type="Normal"
                        ValidationGroup="divLineDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                        EnableViewState="False" OnAfterClientRowCreated="setDefaultValues" PageSize="10000"
                        AddRowsonCurrentPage="False" ShowPageSizer="False" OnAfterCallBack="ifgLineDetailOnAfterCB"
                        OnBeforeCallBack="" AutoSearch="True" AddButtonCssClass="btn btn-small btn-success"
                        DeleteButtonCssClass="btn btn-small btn-danger" ShowRecordCount="False" UseIcons="True"
                        UseActivitySpecificDatasource="True">
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <ActionButtons>
                            <iFg:ActionButton ID="btnMassApplyInput" OnClientClick="onClickMassApplyInput" Text="Mass Apply Input"
                                CSSClass="btn btn-small btn-success" IconClass="icon-save" ValidateRowSelection="False" />
                        </ActionButtons>
                        <Columns>
                            <iFg:LookupField DataField="ITM_CD" ForeignDataField="ITM_ID" HeaderText="Item *"
                                HeaderTitle="Item" PrimaryDataField="ITM_ID" SortAscUrl="" SortDescUrl="">
                                <Lookup DataKey="ITM_CD" DependentChildControls="" HelpText="610" iCase="Upper" OnClientTextChange=""
                                    ValidationGroup="divLineDetail" TableName="39" CssClass="lkp" DoSearch="True"
                                    Width="70px" ClientFilterFunction="">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="ITM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="ITM_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="ITM_DSCRPTN_VC" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Item  Required"
                                        LkpErrorMessage="Invalid Item. Click on the list for valid values." />
                                </Lookup>
                                <HeaderStyle CssClass="" Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:LookupField DataField="SB_ITM_CD" ForeignDataField="SB_ITM_ID" HeaderText="Sub Item"
                                HeaderTitle="Sub Item" PrimaryDataField="SB_ITM_ID" SortAscUrl="" SortDescUrl="">
                                <Lookup DataKey="SB_ITM_CD" DependentChildControls="" HelpText="597" iCase="Upper"
                                    OnClientTextChange="" ValidationGroup="divLineDetail" TableName="43" CssClass="lkp"
                                    DoSearch="True" Width="70px" ClientFilterFunction="applySubItemFilter">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="SB_ITM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="SB_ITM_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="SB_ITM_DSCRPTN_VC" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="False"
                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Sub Item  Required"
                                        LkpErrorMessage="Invalid Sub Item. Click on the list for valid values." />
                                </Lookup>
                                <HeaderStyle CssClass="" Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:LookupField DataField="DMG_CD" ForeignDataField="DMG_ID" HeaderText="Dam *"
                                HeaderTitle="Damage" PrimaryDataField="DMG_ID" SortAscUrl="" SortDescUrl="">
                                <Lookup DataKey="DMG_CD" DependentChildControls="" HelpText="360,DAMAGE_DMG_CD" iCase="Upper"
                                    OnClientTextChange="" ValidationGroup="divLineDetail" TableName="23" CssClass="lkp"
                                    DoSearch="True" Width="60px" ClientFilterFunction="applyDepoFilter">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="DMG_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="DMG_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="DMG_DSCRPTN_VC" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Damage Code Required"
                                        LkpErrorMessage="Invalid Damage. Click on the list for valid values." />
                                </Lookup>
                                <HeaderStyle CssClass="" Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:LookupField DataField="RPR_CD" ForeignDataField="RPR_ID" HeaderText="Rpr *"
                                HeaderTitle="Repair" PrimaryDataField="RPR_ID" SortAscUrl="" SortDescUrl="">
                                <Lookup DataKey="RPR_CD" DependentChildControls="" HelpText="361,REPAIR_RPR_CD" iCase="Upper"
                                    OnClientTextChange="" ValidationGroup="divLineDetail" MaxLength="15" TableName="24"
                                    CssClass="lkp" DoSearch="True" Width="60px" ClientFilterFunction="applyDepoFilter">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="RPR_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="RPR_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="RPR_DSCRPTN_VC" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Repair Code Required"
                                        LkpErrorMessage="Invalid Repair. Click on the list for valid values." />
                                </Lookup>
                                <HeaderStyle CssClass="" Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:LookupField DataField="MTRL_CD" ForeignDataField="MTRL_ID" HeaderText="Material *"
                                HeaderTitle="Material *" PrimaryDataField="MTRL_ID" SortAscUrl="" SortDescUrl="">
                                <Lookup DataKey="MTRL_CD" DependentChildControls="" HelpText="611" iCase="Upper"
                                    OnClientTextChange="" ValidationGroup="divLineDetail" TableName="14" CssClass="lkp"
                                    DoSearch="True" Width="70px" ClientFilterFunction="">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="MTRL_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="MTRL_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="MTRL_DSCRPTN_VC" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Material Required"
                                        LkpErrorMessage="Invalid Material. Click on the list for valid values." />
                                </Lookup>
                                <HeaderStyle CssClass="" Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:TextboxField CharacterLimit="0" DataField="LBR_HRS" HeaderText="Man Hour *"
                                HeaderTitle="Man Hours" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                <TextBox CssClass="ntxto" HelpText="362,REPAIR_ESTIMATE_DETAIL_LBR_HRS" iCase="Numeric"
                                    OnClientTextChange="calculateLHCfromHours" ValidationGroup="divLineDetail">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                        IsRequired="true" RegErrorMessage="Invalid Man Hour. Range must be from 0.01 to 9999999.99"
                                        ReqErrorMessage="Man Hour Required." RegexValidation="True" LookupValidation="False"
                                        RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$" />
                                </TextBox>
                                <HeaderStyle CssClass="" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:TextboxField CharacterLimit="0" DataField="DMG_RPR_DSCRPTN" HeaderText="Dmg/Rpr Dmnsn/Remarks "
                                HeaderTitle="Description of the Damage" SortAscUrl="" SortDescUrl="">
                                <TextBox CssClass="txt" HelpText="363,REPAIR_ESTIMATE_DETAIL_DMG_RPR_DSCRPTN" iCase="None"
                                    OnClientTextChange="" ValidationGroup="divLineDetail">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                        RegexValidation="False" LookupValidation="False" />
                                </TextBox>
                                <HeaderStyle CssClass="" HorizontalAlign="Left" />
                                <ItemStyle Width="200px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:TextboxField CharacterLimit="0" DataField="LBR_RT" HeaderText="MHR" HeaderTitle="Man Hour Rate"
                                SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" DataFormatString="{0:F2}"
                                HtmlEncode="False" ReadOnly="true">
                                <TextBox CssClass="ntxto" HelpText="364,REPAIR_ESTIMATE_DETAIL_LBR_RT" iCase="Numeric"
                                    OnClientTextChange="calculateLHCfromHours" ValidationGroup="divLineDetail" ToolTip="Man Hour Rate">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                        RegErrorMessage="Invalid Man Hour Rate.Range must be from 0.01 to 9999999.99"
                                        RegexValidation="True" LookupValidation="False" RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$" />
                                </TextBox>
                                <HeaderStyle CssClass="" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:BoundField DataField="LBR_HR_CST_NC" HeaderText="MHC" HeaderTitle="Man Hour Cost"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                <HeaderStyle CssClass="" HorizontalAlign="Left" />
                                <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:BoundField>
                            <iFg:TextboxField CharacterLimit="0" DataField="MTRL_CST_NC" HeaderText="MC" HeaderTitle="Material Cost"
                                SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" DataFormatString="{0:F2}"
                                HtmlEncode="False">
                                <TextBox CssClass="ntxto" HelpText="365,REPAIR_ESTIMATE_DETAIL_MTRL_CST_NC" iCase="Numeric"
                                    OnClientTextChange="calculateLHCfromHours" ValidationGroup="divLineDetail">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                        RegErrorMessage="Invalid Material Cost. Range must be from 0.01 to 9999999.99"
                                        RegexValidation="True" LookupValidation="False" RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$" />
                                </TextBox>
                                <HeaderStyle CssClass="" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:BoundField DataField="TTL_CST_NC" HeaderText="TC" HeaderTitle="Total Cost" IsEditable="False"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:BoundField>
                            <iFg:LookupField DataField="TX_RSPNSBLTY_ID" ForeignDataField="TX_RSPNSBLTY_ID" HeaderText="Tax *"
                                HeaderTitle="Tax *" PrimaryDataField="ENM_ID" SortAscUrl="" SortDescUrl="">
                                <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="366_ENUM_ENM_CD" iCase="Upper"
                                    OnClientTextChange="" ValidationGroup="divLineDetail" TableName="21" CssClass="lkp"
                                    DoSearch="True" Width="120px" ClientFilterFunction="" OnLookupClick="" MaxLength="1"
                                    ID="Lookup2" runat="server">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="ENM_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="ENM_DSCRPTN_VC" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px"
                                        HorizontalAlign="Right" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Tax Type Required"
                                        LkpErrorMessage="Invalid Tax Responsibility. Click on the list for valid values."
                                        CustomValidationFunction="" CustomValidation="false" />
                                </Lookup>
                                <HeaderStyle Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="60px" Wrap="True" />
                            </iFg:LookupField>
                            <%--  <iFg:LookupField DataField="RSPNSBLTY_CD" ForeignDataField="RSPNSBLTY_ID" HeaderText="Res *"
                                        HeaderTitle="Responsibility" PrimaryDataField="ENM_ID" SortAscUrl="" SortDescUrl="">
                                        <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="366_ENUM_ENM_CD" iCase="Upper"
                                            OnClientTextChange="" ValidationGroup="divLineDetail" TableName="56" CssClass="lkp"
                                            DoSearch="True" Width="120px" ClientFilterFunction="" OnLookupClick="" MaxLength="1"
                                            ID="lkpResponsiblityJTS" runat="server">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="ENM_CD" Hidden="False" />
                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="ENM_DSCRPTN_VC" Hidden="False" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px"
                                                HorizontalAlign="Right" />
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Responsibility Party Required"
                                                LkpErrorMessage="Invalid Responsibility Party. Click on the list for valid values."
                                                CustomValidationFunction="" />
                                        </Lookup>
                                        <HeaderStyle Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="60px" Wrap="True" />
                                    </iFg:LookupField>--%>
                            <iFg:LookupField DataField="RSPNSBLTY_CD" ForeignDataField="RSPNSBLTY_ID" HeaderText="Res *"
                                HeaderTitle="Responsibility" PrimaryDataField="RSPNSBLTY_ID" SortAscUrl="" SortDescUrl="">
                                <Lookup DataKey="RSPNSBLTY_CD" DependentChildControls="" HelpText="366" iCase="Upper"
                                    OnClientTextChange="" ValidationGroup="divLineDetail" TableName="101" CssClass="lkp"
                                    DoSearch="True" Width="120px" ClientFilterFunction="bindResponsibilityLookup"
                                    OnLookupClick="" ID="lkpResponsiblityGWS" runat="server">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="RSPNSBLTY_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="RSPNSBLTY_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="RSPNSBLTY_DSCRPTN_VC" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px"
                                        HorizontalAlign="Right" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Responsibility Party Required"
                                        LkpErrorMessage="Invalid Responsibility Party. Click on the list for valid values."
                                        CustomValidationFunction="" />
                                </Lookup>
                                <HeaderStyle Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="60px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:TextboxField CharacterLimit="0" DataField="RMRKS" HeaderText="Remarks" HeaderTitle="Remarks"
                                SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" DataFormatString="{0:F2}"
                                HtmlEncode="False" ReadOnly="false">
                                <TextBox CssClass="txt" HelpText="232" iCase="None" OnClientTextChange="" ValidationGroup="divLineDetail"
                                    MaxLength="255" ToolTip="Remarks">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                        RegErrorMessage="" RegexValidation="False" LookupValidation="False" RegularExpression="" />
                                </TextBox>
                                <HeaderStyle CssClass="" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:CheckBoxField DataField="CHK_BT" HeaderText="Mass Select" HeaderImageUrl=""
                                HeaderTitle="Mass Select" HelpText="" SortAscUrl="" SortDescUrl="">
                                <HeaderStyle CssClass="" Width="5%" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="5%" Wrap="True" HorizontalAlign="Left" />
                            </iFg:CheckBoxField>
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
        <tr>
            <td colspan="12">
                &nbsp;
            </td>
        </tr>
        </table>
        <table>
            <tr>
                <td>
                    <div id="divLineSummary">
                        <table border="0" cellpadding="0" cellspacing="0" class="tblstd">
                            <tr>
                                <td>
                                    <Inp:iLabel ID="ILabel24" runat="server" CssClass="ublbl">Summary</Inp:iLabel>
                                </td>
                                <td rowspan="6" valign="top">
                                    <iFg:iFlexGrid ID="ifgSummaryDetail" runat="server" AllowStaticHeader="False" DataKeyNames="SMMRY_ID"
                                        Width="400px" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                        Scrollbars="None" ShowEmptyPager="False" Type="Normal" ValidationGroup="" UseCachedDataSource="True"
                                        AutoGenerateColumns="False" EnableViewState="False" OnBeforeClientRowCreated=""
                                        OnAfterClientRowCreated="" PageSize="25" AddRowsonCurrentPage="False" ShowPageSizer="False"
                                        AllowPaging="False" AllowAdd="False" AllowDelete="False" ShowFooter="False" OnAfterCallBack="ifgSummaryDetailGWSOnAfterCallBack"
                                        AllowEdit="False" AllowExport="True" AllowFilter="False">
                                        <PagerStyle CssClass="gpage" />
                                        <RowStyle CssClass="gitem" />
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                        <Columns>
                                            <iFg:BoundField DataField="MN_HR_SMMRY" HeaderText="MH" HeaderTitle="" IsEditable="False"
                                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:BoundField>
                                            <iFg:BoundField DataField="LBR_RT_SMMRY" HeaderText="MHR" HeaderTitle="" IsEditable="False"
                                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False" Visible="false">
                                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:BoundField>
                                            <iFg:BoundField DataField="MN_HR_RT_SMMRY" HeaderText="MHC" HeaderTitle="" IsEditable="False"
                                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:BoundField>
                                            <iFg:BoundField DataField="MTRL_CST_SMMRY" HeaderText="MC" HeaderTitle="" IsEditable="False"
                                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:BoundField>
                                            <iFg:BoundField DataField="TTL_CST_SMMRY" HeaderText="TC" HeaderTitle="" IsEditable="False"
                                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:BoundField>
                                        </Columns>
                                        <SelectedRowStyle CssClass="gsitem" />
                                        <AlternatingRowStyle CssClass="gaitem" />
                                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                    </iFg:iFlexGrid>
                                </td>
                                <td rowspan="6" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td class="pgHdr">
                                    Customer
                                </td>
                            </tr>
                            <tr>
                                <td class="pgHdr">
                                    Invoicing Party
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divLineSummaryGWS">
                        <table border="0" cellpadding="0" cellspacing="0" class="tblstd">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="6" valign="top">
                                    <iFg:iFlexGrid ID="ifgSummaryGWS" runat="server" AllowStaticHeader="False" DataKeyNames=""
                                        Width="400px" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                        Scrollbars="None" ShowEmptyPager="False" Type="Normal" ValidationGroup="" UseCachedDataSource="True"
                                        AutoGenerateColumns="False" EnableViewState="False" OnBeforeClientRowCreated=""
                                        OnAfterClientRowCreated="" PageSize="25" AddRowsonCurrentPage="False" ShowPageSizer="False"
                                        AllowPaging="False" AllowAdd="False" AllowDelete="False" ShowFooter="False" OnAfterCallBack="ifgSummaryDetailGWSOnAfterCallBack"
                                        AllowEdit="False" AllowExport="True" AllowFilter="False">
                                        <PagerStyle CssClass="gpage" />
                                        <RowStyle CssClass="gitem" />
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                        <Columns>
                                            <iFg:BoundField DataField="RSPNSBLTY_CD" HeaderText="Responsibility" HeaderTitle="Responsibility"
                                                IsEditable="False" SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:BoundField>
                                            <iFg:BoundField DataField="MN_HR_SMMRY" HeaderText="MH" HeaderTitle="" IsEditable="False"
                                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:BoundField>
                                            <iFg:BoundField DataField="LBR_RT_SMMRY" HeaderText="MHR" HeaderTitle="" IsEditable="False"
                                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False" Visible="false">
                                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:BoundField>
                                            <iFg:BoundField DataField="MN_HR_RT_SMMRY" HeaderText="MHC" HeaderTitle="" IsEditable="False"
                                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:BoundField>
                                            <iFg:BoundField DataField="MTRL_CST_SMMRY" HeaderText="MC" HeaderTitle="" IsEditable="False"
                                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:BoundField>
                                            <iFg:BoundField DataField="TTL_CST_SMMRY" HeaderText="TC" HeaderTitle="" IsEditable="False"
                                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:F2}" HtmlEncode="False">
                                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:BoundField>
                                        </Columns>
                                        <SelectedRowStyle CssClass="gsitem" />
                                        <AlternatingRowStyle CssClass="gaitem" />
                                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                    </iFg:iFlexGrid>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td align="right">
                    <div id="divCostSummary">
                        <table>
                            <tr>
                                <td class="ghdr">
                                    Total Estimated Amount
                                </td>
                                <td>
                                    <Inp:iLabel ID="lblTotEstimatedAmount" DataFormatString="{0:F2}" runat="server" CssClass="lbl">
                  
                                    </Inp:iLabel>
                                </td>
                            </tr>
                            <tr>
                                <td class="ghdr">
                                    Total Material Cost
                                </td>
                                <td>
                                    <Inp:iLabel ID="lblTotMaterialCost" DataFormatString="{0:F2}" runat="server" CssClass="lbl">
                                    </Inp:iLabel>
                                </td>
                </td>
            </tr>
            <tr>
                <td class="ghdr">
                    Total Labor Cost
                </td>
                <td>
                    <Inp:iLabel ID="lblTotLabrCost" DataFormatString="{0:F2}" runat="server" CssClass="lbl">
                    </Inp:iLabel>
                </td>
            </tr>
            <tr>
                <td class="ghdr" id="lblServiceTax" runat="server">
                    Service Tax (<Inp:iLabel ID="lblServTaxCurr" runat="server" CssClass="lbl"> 
                    </Inp:iLabel>)
                </td>
                <td id="lblsrvTax" runat="server">
                    <Inp:iLabel ID="lblSrvcTax" DataFormatString="{0:F2}" runat="server" CssClass="lbl">
                    </Inp:iLabel>
                </td>
            </tr>
            <tr>
                <td class="ghdr" id="lblTotEstAmt" runat="server">
                    Total Estimate Amount
                    <br />
                    (Including Service Tax)
                </td>
                <td>
                    <Inp:iLabel ID="lblTotalEstimatedAmount" DataFormatString="{0:F2}" runat="server"
                        CssClass="lbl">
                    </Inp:iLabel>
                </td>
            </tr>
        </table>
    </div>
    </td>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    </table>
    <div>
        <table border="0" cellpadding="1" cellspacing="1" class="tblstd">
            <tr>
                <td>
                    <a id="hypTankRepairWorkOrder" href="#" onclick="generateWorkOrder();">Repair Work Order</a>
                </td>
                <td>
                </td>
                <td>
                    <a id="hypTankRepairEstimate" href="#" onclick="generateRepairEstimate();">Repair Estimate</a>
                </td>
                <td>
                </td>
                <td>
                    <div id="divLeaktest" runat="server">
                        <a id="hypLeaktest" href="#" onclick="showLeakTest();">Leak Test</a>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </div>
    <div id="divSubmit">
        <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
            onClientPrint="null" />
    </div>
    <div id="divLockMessage" style="width: 100%; margin: 0px auto; font-weight: bold;
        display: none; text-align: center; height: 20px; background-color: #CDFFCC; border: 1px solid #07760D;">
    </div>
    </div>
    <asp:HiddenField ID="hdnExchangeRate" runat="server" />
    <asp:HiddenField ID="hdnEirNo" runat="server" />
    <asp:HiddenField ID="hdnLesseeId" runat="server" />
    <asp:HiddenField ID="hdnEqpCDID" runat="server" />
    <asp:HiddenField ID="hdnEqpTypID" runat="server" />
    <asp:HiddenField ID="hdnCustomerId" runat="server" />
    <asp:HiddenField ID="hdnEstimateID" runat="server" />
    <asp:HiddenField ID="hdnAgentId" runat="server" />
    <asp:HiddenField ID="hdnEstimationTotal" runat="server" />
    <asp:HiddenField ID="hdnStatusCD" runat="server" />
    <asp:HiddenField ID="hdnEquipmentStatusId" runat="server" />
    <asp:HiddenField ID="hdnRprEstimationNo" runat="server" />
    <asp:HiddenField ID="hdnGateInBin" runat="server" />
    <asp:HiddenField ID="hdnRevisionNo" runat="server" />
    <asp:HiddenField ID="hdnTariffCodeID" runat="server" />
    <asp:HiddenField ID="hdnToCurrencyID" runat="server" />
    <asp:HiddenField ID="hdnToCurrencyCD" runat="server" />
    <asp:HiddenField ID="hdnDepotCurrencyCD" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField ID="hdnUserRole" runat="server" />
    <asp:HiddenField ID="hdnRepairEstimate" runat="server" />
    <asp:HiddenField ID="hdnLaborRate" runat="server" />
    <asp:HiddenField ID="hdnTestTypeId" runat="server" />
    <asp:HiddenField ID="hdnPageName" runat="server" />
    <asp:HiddenField ID="hdnLockUserName" runat="server" />
    <asp:HiddenField ID="hdnIpError" runat="server" />
    <asp:HiddenField ID="hdnLockActivityName" runat="server" />
    <asp:HiddenField ID="hdnGITransaction" runat="server" />
    <asp:HiddenField ID="hdnBillTo" runat="server" />
    </form>
</body>
</html>
