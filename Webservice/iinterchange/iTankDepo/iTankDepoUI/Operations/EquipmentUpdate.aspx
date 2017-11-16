<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EquipmentUpdate.aspx.vb"
    Inherits="Operations_EquipmentUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="width: 100%">
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh">Operations >> Equipment Update</span>
                    </td>
                    <td align="right">
                        <nv:Navigation ID="navEquipmentUpdate" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="tabEquipmentUpdate" class="tabDisplay" style="overflow-y: auto;overflow-x: auto; height: auto">
        <div id="divSearch" style="width:966px;">
            <fieldset class="lbl" id="fldSearch">
                <legend class="blbl">Search</legend>
                <table class="tblstd" border="0" cellpadding="2" cellspacing="2" style="width:500px;"> 
                    <tr>
                        <td>
                            <Inp:iLabel ID="lblEquipmentNo" runat="server" CssClass="lbl">
                                        Equipment No
                            </Inp:iLabel>
                            <Inp:iLabel ID="ReqEquipmentNo" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <Inp:iTextBox ID="txtEquipmentNo" runat="server" CssClass="txt" ToolTip="Equipment Number"
                                HelpText="463,EQUIPMENT_INFORMATION_EQPMNT_NO" iCase="Upper" TabIndex="0">
                                <Validator IsRequired="true" ReqErrorMessage="Equipment Number Required" Validate="True"
                                    ValidationGroup="" />
                            </Inp:iTextBox>
                        </td>
                        <td>
                        </td>
                        <td valign="middle">
                            <ul style="margin: 0px;">
                                <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnSearch" style="text-decoration: none;
                                    color: White; font-weight: bold" class="icon-search" runat="server" onclick="onSearchClick(); return false;"
                                    tabindex="2">&nbsp;Search</a></li>
                            </ul>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <table class="tblstd" border="0" cellpadding="2" cellspacing="2">
            <tr>
                <td colspan="4" style="vertical-align: top;">
                    <div id="divStatusDetails" style="display: none;">
                        <table class="tblstd" style="width: 966px;">
                            <tr>
                                <td style="vertical-align: top;">
                                    <fieldset class="lbl" id="flsCurrentDetails">
                                        <legend class="blbl">Current Details</legend>
                                        <table class="tblstd" style="height: 117px;">
                                            <tr>
                                                <td>
                                                    <Inp:iLabel ID="lblCurrentStatus" runat="server" CssClass="lbl">
                                                Status
                                                    </Inp:iLabel>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <Inp:iTextBox ID="txtCurrentStatus" runat="server" CssClass="txtd" ToolTip="Current Status"
                                                        HelpText="" iCase="Upper" TabIndex="0" ReadOnly="true">
                                                        <Validator IsRequired="false" ReqErrorMessage="" Validate="True" ValidationGroup="" />
                                                    </Inp:iTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <Inp:iLabel ID="lblCurrentActivityDate" runat="server" CssClass="lbl">
                                                Activity Date
                                                    </Inp:iLabel>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <Inp:iDate ID="datCurrentActivityDate" TabIndex="0" runat="server" HelpText="" CssClass="datd"
                                                        iCase="Upper" ReadOnly="true">
                                                        <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup=""
                                                            Operator="LessThanEqual" LkpErrorMessage="Invalid  Date. Click on the calendar icon"
                                                            Validate="True" CsvErrorMessage="" CustomValidationFunction="" CompareValidation="false"
                                                            CmpErrorMessage="" />
                                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    </Inp:iDate>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <Inp:iLabel ID="lblCustomer" runat="server" CssClass="lbl">
                                               Customer
                                                    </Inp:iLabel>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <Inp:iTextBox ID="txtCustomer" runat="server" CssClass="txtd" ToolTip="Customer"
                                                        HelpText="" iCase="Upper" TabIndex="0" ReadOnly="true">
                                                        <Validator IsRequired="false" ReqErrorMessage="" Validate="True" ValidationGroup="" />
                                                    </Inp:iTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <Inp:iLabel ID="lblEqType" runat="server" CssClass="lbl">
                                               Type
                                                    </Inp:iLabel>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <Inp:iTextBox ID="txtEqType" runat="server" CssClass="txtd" ToolTip="Equipment Type"
                                                        HelpText="" iCase="Upper" TabIndex="0" ReadOnly="true">
                                                        <Validator IsRequired="false" ReqErrorMessage="" Validate="True" ValidationGroup="" />
                                                    </Inp:iTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <Inp:iLabel ID="lblEqCode" runat="server" CssClass="lbl">
                                               Code
                                                    </Inp:iLabel>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <Inp:iTextBox ID="txtEqCode" runat="server" CssClass="txtd" ToolTip="Equipment Code"
                                                        HelpText="" iCase="Upper" TabIndex="0" ReadOnly="true">
                                                        <Validator IsRequired="false" ReqErrorMessage="" Validate="True" ValidationGroup="" />
                                                    </Inp:iTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td style="vertical-align: top;">
                                    <fieldset class="lbl" id="flsNewDetails">
                                        <legend class="blbl">New Details</legend>
                                        <table class="tblstd" style="height: 117px;">
                                            <tr>
                                                <td>
                                                    <Inp:iLabel ID="lblNewEqpNo" runat="server" CssClass="lbl">
                                            Equipment No
                                                    </Inp:iLabel>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <Inp:iTextBox ID="txtNewEqpNo" runat="server" CssClass="txt" ToolTip="Equipment Number"
                                                        HelpText="464,EQUIPMENT_INFORMATION_EQPMNT_NO" iCase="Upper" TabIndex="1">
                                                        <Validator IsRequired="false" ReqErrorMessage="Equipment Number Required" CustomValidation="true"
                                                            Validate="True" CustomValidationFunction="validateEquipmentno" ValidationGroup="divEquipmentUpdate" />
                                                    </Inp:iTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <Inp:iLabel ID="lblNewCustomer" runat="server" CssClass="lbl">
                                            Customer
                                                    </Inp:iLabel>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <Inp:iLookup ID="lkpCustomer" runat="server" CssClass="lkp" DataKey="CSTMR_CD" DoSearch="True"
                                                        iCase="Upper" TabIndex="2" TableName="9" HelpText="465,CUSTOMER_CSTMR_CD" ToolTip="Customer"
                                                        ClientFilterFunction="applyCustomer">
                                                        <LookupColumns>
                                                            <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                                                            <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                                                        </LookupColumns>
                                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                                                            HorizontalAlign="Left" />
                                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="false"
                                                            LkpErrorMessage="Invalid Customer Code. Click on the list for valid values" ReqErrorMessage="Customer Required."
                                                            Validate="True" ValidationGroup="" />
                                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    </Inp:iLookup>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <Inp:iLabel ID="lblNewType" runat="server" CssClass="lbl">
                                                Type
                                                    </Inp:iLabel>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <Inp:iLookup ID="lkpEqpType" runat="server" CssClass="lkp" DataKey="EQPMNT_TYP_CD"
                                                        ToolTip="Equipment Type" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="3"
                                                        TableName="3" HelpText="466,EQUIPMENT_TYPE_EQPMNT_TYP_CD" ClientFilterFunction="applyEquipmentType"
                                                        ReadOnly="false">
                                                        <LookupColumns>
                                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_CD" ControlToBind="" Hidden="False" ColumnCaption="Type" />
                                                            <Inp:LookupColumn ColumnName="EQPMNT_CD_CD"  Hidden="False" ColumnCaption="Code" ControlToBind="txtNewEqpCode" />
                                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                                                Hidden="False" />
                                                        </LookupColumns>
                                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                                                            HorizontalAlign="Left" />
                                                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                                            LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                                                            Operator="Equal" ReqErrorMessage="Equipment Type Required" Type="String" Validate="True"
                                                            ValidationGroup="" CustomValidation="true" CustomValidationFunction="setEquipmentCode" />
                                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    </Inp:iLookup>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <Inp:iLabel ID="lblNewCode" runat="server" CssClass="lbl">
                                            Code
                                                    </Inp:iLabel>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <%--<Inp:iLookup ID="lkpEqpCode" runat="server" CssClass="lkp" DataKey="EQPMNT_CD_CD"
                                                        ToolTip="Equipment Code" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="4"
                                                        TableName="7" HelpText="467,EQUIPMENT_CODE_EQPMNT_CD_CD" ClientFilterFunction="applyEquipmentCode"
                                                        ReadOnly="true">
                                                        <LookupColumns>
                                                            <Inp:LookupColumn ColumnName="EQPMNT_CD_ID" Hidden="True" />
                                                            <Inp:LookupColumn ColumnName="EQPMNT_CD_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_CD_DSCRPTN_VC" Hidden="False" />
                                                        </LookupColumns>
                                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                                                            HorizontalAlign="left" />
                                                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                                            LkpErrorMessage="Invalid Equipment Code. Click on the list for valid values"
                                                            Operator="Equal" ReqErrorMessage="Equipment Code Required" Type="String" Validate="True"
                                                            ValidationGroup="" />
                                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    </Inp:iLookup>--%>

                                                     <Inp:iTextBox ID="txtNewEqpCode" runat="server" CssClass="txtd" ToolTip="Equipment Code"
                                                        HelpText="" iCase="Upper" TabIndex="0" ReadOnly="true">
                                                        <Validator IsRequired="false" ReqErrorMessage="" Validate="True" ValidationGroup="" />
                                                    </Inp:iTextBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <Inp:iLabel ID="ILabel1" runat="server" CssClass="lbl">Effective From</Inp:iLabel>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>                                                   
                                                    <Inp:iDate ID="datEffectiveFrom" TabIndex="5" runat="server" HelpText="" CssClass="dat"
                                                        ToolTip="Effective From Date" iCase="Upper" MaxLength="11">
                                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Type="Date" ValidationGroup="divValidation"
                                                            Operator="GreaterThan" LkpErrorMessage="Invalid Date. Click on the calendar icon for valid values"
                                                            Validate="True" CompareValidation="false" ReqErrorMessage="Effective From Date Required"
                                                            CustomValidation="true" CustomValidationFunction="validateEffectiveDate" CmpErrorMessage="Effective From Date should be greater then current Date." />
                                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    </Inp:iDate>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                        font-family: Arial; font-size: 8pt; display: none; width: 100%;" align="center">
                        <div>
                            There is no information available in iTankdepo for the selected Equipment Number
                            to perform the Equipment Update action.</div>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div id="divBillingBit" runat="server" style="margin: 10px; font-style: italic; font-family: Arial;
                        font-size: 8pt; display: none; width: 100%;" align="center">                      
                        <div>
                            (Repair / Cleaning / Heating / Survey) Activity is in progress, Equipment cannot
                            be updated
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div id="divEqpInformation" runat="server" style="margin: 10px; font-style: italic;
                        font-family: Arial; font-size: 8pt; display: none; width: 100%;" align="center">
                        <div>
                            Equipment can not be updated, since the Equipment is available in Equipment Information
                            or Pre-Advice.</div>
                    </div>
                </td>
            </tr>
            </table>
             <table class="tblstd" border="0" cellpadding="2" cellspacing="2" style="width:100%" >
            <tr>
                <td colspan="4">
                    <div id="divEquipmentUpdate" style="width:100%;">
                        <fieldset class="lbl" id="fldEquipmentUpdate">
                            <legend class="blbl">Additional Update</legend>
                            <iFg:iFlexGrid ID="ifgEquipmentUpdate" runat="server" AllowStaticHeader="True" DataKeyNames="EQPMNT_INFRMTN_ID"
                                Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="130px" Type="Normal"
                                ValidationGroup="divEquipmentUpdate" UseCachedDataSource="True" AutoGenerateColumns="False"
                                EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                                AddRowsonCurrentPage="True" ShowPageSizer="False" OnAfterCallBack="ifgEquipmentUpdateOnAfterCB"
                                OnBeforeCallBack="" AllowDelete="False" AllowAdd="False">
                                <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <Columns>
                                    <iFg:LookupField DataField="PRDCT_DSCRPTN_VC" ForeignDataField="PRDCT_ID" HeaderText="Previous Cargo"
                                        HeaderTitle="Previous Cargo" PrimaryDataField="PRDCT_ID" SortAscUrl="" SortDescUrl=""
                                        AllowSearch="true">
                                        <Lookup DataKey="PRDCT_DSCRPTN_VC" DependentChildControls="" HelpText="468,PRODUCT_PRDCT_CD"
                                            iCase="Upper" OnClientTextChange="" TableName="46" ValidationGroup="" ID="lklPreviousCargo"
                                            CssClass="lkp" DoSearch="True" Width="150px" ClientFilterFunction="" AutoSearch="true">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnCaption="ID" ColumnName="PRDCT_ID" Hidden="true" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="PRDCT_CD" Hidden="False" />
                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="PRDCT_DSCRPTN_VC" Hidden="False" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px"
                                                HorizontalAlign="Center" />
                                            <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Previous Cargo. Click on the List for Valid Values"
                                                Validate="True" IsRequired="true" CustomValidation="false" ReqErrorMessage="Previous Cargo Required"
                                                CustomValidationFunction="" />
                                        </Lookup>
                                        <HeaderStyle cssClass="ghdr" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="GI_RF_NO" HeaderText="JTS EIR No"
                                        HeaderTitle="JTS EIR No" SortAscUrl="" SortDescUrl="" ReadOnly="false" SortExpression=""
                                        HtmlEncode="true">
                                        <TextBox HelpText="469,ACTIVITY_STATUS_GI_RF_NO" iCase="None" OnClientTextChange=""
                                            ValidationGroup="" CssClass="txt">
                                            <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                                RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                                ValidationGroup="divEquipmentUpdate" Validate="false" />
                                        </TextBox>
                                        <HeaderStyle cssClass="" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="LST_SRVYR_NM" HeaderText="Last Surveyor"
                                        HeaderTitle="Last Surveyor" SortAscUrl="" SortDescUrl="" ReadOnly="false" SortExpression=""
                                        HtmlEncode="true">
                                        <TextBox HelpText="470,EQUIPMENT_INFORMATION_LST_SRVYR_NM" iCase="None" OnClientTextChange=""
                                            ValidationGroup="" CssClass="txt">
                                            <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                                RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                                ValidationGroup="divEquipmentUpdate" Validate="false" />
                                        </TextBox>
                                        <HeaderStyle cssClass="" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    </iFg:TextboxField>
                                    <iFg:DateField DataField="LST_TST_DT" HeaderText="Last Test Date" HeaderTitle="Last Test Date"
                                        SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false">
                                        <iDate HelpText="471" iCase="Upper" LeftPosition="0" OnClientTextChange="calculateNextTestDate"
                                            TopPosition="0" ValidationGroup="" MaxLength="11" CssClass="txt" Width="80px">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                                LkpErrorMessage="Invalid  Last Test Date. Click on the calendar icon for valid values"
                                                ValidationGroup="divEquipmentUpdate" ReqErrorMessage="Last Test Date Required"
                                                Validate="True" />
                                        </iDate>
                                        <HeaderStyle cssClass="ghdr" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                    </iFg:DateField>
                                    <iFg:LookupField HeaderTitle="Last Test Type" SortAscUrl="" SortDescUrl="" DataField="LST_TST_TYP_CD"
                                        ForeignDataField="LST_TST_TYP_ID" HeaderText="Last Test Type" PrimaryDataField="ENM_ID">
                                        <Lookup DependentChildControls="" HelpText="472,ENUM_ENM_CD" iCase="Upper" OnClientTextChange="bindTestType"
                                            ValidationGroup="" CssClass="lkp" DataKey="ENM_CD" DoSearch="True" TableName="45"
                                            AllowSecondaryColumnSearch="false" SecondaryColumnName="ENM_CD">
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px" />
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Last Type" ColumnName="ENM_CD" ControlToBind=""
                                                    Hidden="False"></Inp:LookupColumn>
                                            </LookupColumns>
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Test Type of the Equipment. Click on the List for Valid Values"
                                                ReqErrorMessage="Test Type Required" Validate="True" IsRequired="false" CustomValidation="false"
                                                ValidationGroup="divEquipmentUpdate" CustomValidationFunction="" />
                                        </Lookup>
                                        <HeaderStyle cssClass="" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="90px" Wrap="true" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField DataField="VLDTY_PRD_TST_YRS" HeaderText="Validity Period of Test(in Years)"
                                        HeaderTitle="Validity Period of Test(in Years)" SortAscUrl="" SortDescUrl=""
                                        HeaderStyle-Width="40px" ReadOnly="true">
                                        <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                ValidationGroup="divEquipmentUpdate" RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle cssClass="" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:DateField DataField="NXT_TST_DT" HeaderText="Next Test Date" HeaderTitle="Next Test Date"
                                        SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                        ReadOnly="true">
                                        <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                            ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="False" Operator="GreaterThanEqual" Type="Date"
                                                ValidationGroup="" IsRequired="false" LkpErrorMessage="Invalid  Next Test Date. Click on the calendar icon for valid values"
                                                ReqErrorMessage="Next Test Date Required" Validate="True" />
                                        </iDate>
                                        <HeaderStyle  HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                    </iFg:DateField>
                                    <iFg:LookupField HeaderTitle="Next Test Type" SortAscUrl="" SortDescUrl="" DataField="NXT_TST_TYP_CD"
                                        ForeignDataField="NXT_TST_TYP_ID" HeaderText="Next Test Type" PrimaryDataField="ENM_ID"
                                        ReadOnly="true">
                                        <Lookup DependentChildControls="" HelpText="" iCase="Upper" OnClientTextChange=""
                                            ValidationGroup="" CssClass="lkp" DataKey="ENM_CD" DoSearch="True" TableName="45"
                                            AllowSecondaryColumnSearch="true" SecondaryColumnName="ENM_CD">
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" />
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Next Type" ColumnName="ENM_CD" ControlToBind=""
                                                    Hidden="False"></Inp:LookupColumn>
                                            </LookupColumns>
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" ValidationGroup="" />
                                        </Lookup>
                                        <HeaderStyle cssClass="ghdr" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="true" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField DataField="INSTRCTNS_VC" HeaderText="Remarks " HeaderTitle="Remarks"
                                        SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                        <TextBox CssClass="txt" HelpText="473,ACTIVITY_STATUS_INSTRCTNS_VC" iCase="None"
                                            OnClientTextChange="" ValidationGroup="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                ValidationGroup="divEquipmentUpdate" RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle Width ="10%" CssClass="ghdr" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="10%"  Wrap="True" />
                                    </iFg:TextboxField>
                                </Columns>
                            </iFg:iFlexGrid>
                        </fieldset>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="divSubmit" style="display: none;">
        <sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();"
            onclientprint="null" />
    </div>
    <div id="divLockMessage" style="width: 100%; display: none; margin: 0px auto; text-align: center;
        font-weight: bold; height: 20px; background-color: #CDFFCC; border: 1px solid #07760D;">
    </div>
    <asp:HiddenField ID="hdnDepot" runat="server" />
    <asp:HiddenField ID="hdnCustomerId" runat="server" />
    <asp:HiddenField ID="hdnTypeId" runat="server" />
    <asp:HiddenField ID="hdnCodeId" runat="server" />
    <asp:HiddenField ID="hdnBillingBit" runat="server" />
    <asp:HiddenField ID="hdnPageName" runat="server" />
    <asp:HiddenField ID="hdnLockUserName" runat="server" />
    <asp:HiddenField ID="hdnIpError" runat="server" />
    <asp:HiddenField ID="hdnLockActivityName" runat="server" />
    </form>
</body>
</html>
