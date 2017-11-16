<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EquipmentInformation.aspx.vb"
    Inherits="Operations_EquipmentInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--   <link href="../Styles/Classic/Styles.css" rel="stylesheet" type="text/css" />--%>
</head>
<body onload="initPage('GateIn');">
    <form id="form1" runat="server" autocomplete="off" style="height: 550px">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Operations >> Equipment Information</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navEquipmentInfo" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="tabEquipmentInformation" class="tabdisplayGatePass" style="height: 100%;">
        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
            font-family: Arial; font-size: 8pt; display: none; width: 80%;" align="center">
            <div>
                No Records Found.</div>
        </div>
        <table style="width: 100%;" border="0" cellpadding="2" cellspacing="2" class="tblstd">
            <tr style="width: 100%;">
                <td>
                    <!-- UIG Fix -->
                    <div id="divEquipmentInformation" style="margin: 1px; width: 100%; vertical-align: middle;">
                        <iFg:iFlexGrid ID="ifgEquipmentInformation" runat="server" AllowStaticHeader="True"
                            DataKeyNames="EQPMNT_INFRMTN_ID" Width="100%" CaptionAlign="NotSet" GridLines="Both"
                            HeaderRows="1" HorizontalAlign="Justify" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
                            ValidationGroup="divEquipmentInformation" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="setDefaultValues"
                            PageSize="20" AddRowsonCurrentPage="False" AllowPaging="true" OnAfterCallBack="ifgEquipmentInformationOnAfterCB"
                            AllowDelete="true" AllowRefresh="True" AllowSearch="True" AutoSearch="True" UseIcons="true"
                            SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser" OnBeforeCallBack="ifgEquipmentInformationOnBeforeCB"
                            UseActivitySpecificDatasource="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No *" HeaderTitle="Equipment No"
                                    SortAscUrl="" SortDescUrl="">
                                    <TextBox CausesValidation="True" CssClass="txt" HelpText="252,EQUIPMENT_INFORMATION_EQPMNT_NO"
                                        iCase="Upper" MaxLength="11" OnClientTextChange="" ValidationGroup="divEquipmentInformation">
                                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="True"
                                            CustomValidationFunction="validateEquipmentno" IsRequired="True" ReqErrorMessage="Equipment No Required" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Wrap="True" Width="100px" />
                                </iFg:TextboxField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type *"
                                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl="">
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="253,EQUIPMENT_TYPE_EQPMNT_TYP_CD"
                                        iCase="Upper" OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10"
                                        TableName="99" CssClass="lkp" DoSearch="True" Width="40px" ClientFilterFunction=""
                                        AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
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
                                            OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                                            ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divEquipmentInformation"
                                            CustomValidation="true" CustomValidationFunction="setEquipmentCode" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Wrap="True" Width="90px" />
                                </iFg:LookupField>
                                <%--GWS--%>
                                <iFg:LookupField DataField="EQPMNT_CD_CD" ForeignDataField="EQPMNT_CD_ID" HeaderText="Code"
                                    HeaderTitle="Code" PrimaryDataField="EQPMNT_CD_ID" SortAscUrl="" SortDescUrl=""
                                    SortExpression="" ReadOnly="true" AllowSearch="true">
                                    <Lookup DataKey="EQPMNT_CD_CD" DependentChildControls="" HelpText="263" TabIndex="3"
                                        iCase="Upper" OnClientTextChange="" TableName="3" ValidationGroup="divPreAdvice"
                                        ID="Lookup1" CssClass="lkp" DoSearch="True" Width="60px" ClientFilterFunction=""
                                        MaxLength="3" AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
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
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px"
                                            HorizontalAlign="Center" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Equipment Type. Click on the List for Valid Values."
                                            ReqErrorMessage="Equipment Type Required" Validate="True" IsRequired="True" CustomValidation="false"
                                            CustomValidationFunction="setEquipmentCode" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Wrap="True" Width="90px" />
                                </iFg:LookupField>
                                <iFg:DateField DataField="MNFCTR_DT" HeaderText="Manuf. Date" HeaderTitle="" SortAscUrl=""
                                    HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                                    HtmlEncode="false">
                                    <iDate HelpText="254" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="100px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="80px" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid  Manuf. Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Manuf. Date Required" Validate="True" CompareValidation="true"
                                            CmpErrorMessage="Manuf. Date cannot be greater than current Date." />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:TextboxField DataField="TR_WGHT_NC" HeaderText="Tare Weight (Kgs)" HeaderTitle="Tare Weight (Kgs)"
                                    SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                    <TextBox CssClass="ntxto" HelpText="255,EQUIPMENT_INFORMATION_TR_WGHT_NC" iCase="Numeric"
                                        OnClientTextChange="FormatThreeDecimal" ValidationGroup="" MaxLength="5">
                                        <Validator CustomValidateEmptyText="False" Operator="GreaterThan" Type="Double" Validate="True"
                                            RegexValidation="True" RegularExpression="^\d{1,7}(\.\d{1,3})?$" RegErrorMessage="Invalid Tare Weight. Range must be from 0.001 to 9999999.999"
                                            LookupValidation="False" CompareValidation="true" ValueToCompare="0" CmpErrorMessage="Tare Weight must be greater than 0" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="GRSS_WGHT_NC" HeaderText="Gross Weight (Kgs)" HeaderTitle="Gross Weight (Kgs)"
                                    SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                    <TextBox CssClass="ntxto" HelpText="256,EQUIPMENT_INFORMATION_GRSS_WGHT_NC" iCase="Numeric"
                                        OnClientTextChange="FormatThreeDecimal" ValidationGroup="" MaxLength="5">
                                        <Validator CustomValidateEmptyText="False" Operator="GreaterThan" Type="Double" Validate="True"
                                            RegexValidation="True" RegularExpression="^\d{1,7}(\.\d{1,3})?$" RegErrorMessage="Invalid Gross Weight. Range must be from 0.001 to 9999999.999"
                                            LookupValidation="False" CompareValidation="true" ValueToCompare="0" CmpErrorMessage="Gross Weight must be greater than 0" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="CPCTY_NC" HeaderText="Capacity (Litres)" HeaderTitle="Capacity (Litres)"
                                    SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                    <TextBox CssClass="ntxto" HelpText="257,EQUIPMENT_INFORMATION_CPCTY_NC" iCase="Numeric"
                                        OnClientTextChange="FormatThreeDecimal" ValidationGroup="" MaxLength="5">
                                        <Validator CustomValidateEmptyText="False" Operator="GreaterThan" Type="Double" Validate="True"
                                            RegexValidation="True" RegularExpression="^\d{1,7}(\.\d{1,3})?$" RegErrorMessage="Invalid Capacity. Range must be from 0.001 to 9999999.999"
                                            LookupValidation="False" CompareValidation="true" ValueToCompare="0" CmpErrorMessage="Capacity must be greater than 0" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="LST_SRVYR_NM" HeaderText="Last Surveyor" HeaderTitle="Last Surveyor"
                                    SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                    <TextBox CssClass="txt" HelpText="258,EQUIPMENT_INFORMATION_LST_SRVYR_NM" iCase="None"
                                        OnClientTextChange="" ValidationGroup="" MaxLength="5">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                            RegexValidation="False" LookupValidation="False" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:DateField DataField="LST_TST_DT" HeaderText="Last Test Date" HeaderTitle="Last Test Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false">
                                    <iDate HelpText="259" iCase="Upper" LeftPosition="0" OnClientTextChange="calculateNextTestDate"
                                        TopPosition="0" ValidationGroup="" MaxLength="11" CssClass="txt" Width="80px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid  Last Test Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Last Test Date Required" Validate="True" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:LookupField HeaderTitle="Last Test Type" SortAscUrl="" SortDescUrl="" DataField="LST_TST_TYP_CD"
                                    ForeignDataField="LST_TST_TYP_ID" HeaderText="Last Test Type" PrimaryDataField="ENM_ID">
                                    <Lookup DependentChildControls="" HelpText="260,ENUM_ENM_CD" iCase="None" OnClientTextChange="bindTestType"
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
                                            CustomValidationFunction="" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="90px" Wrap="true" />
                                </iFg:LookupField>
                                <iFg:DateField DataField="NXT_TST_DT" HeaderText="Next Test Date" HeaderTitle="Next Test Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="261,EQUIPMENT_INFORMATION_NXT_TST_DT" iCase="Upper" LeftPosition="0"
                                        OnClientTextChange="" TopPosition="0" ValidationGroup="" MaxLength="11" CssClass="txt"
                                        Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="GreaterThanEqual" Type="Date"
                                            IsRequired="false" LkpErrorMessage="Invalid  Next Test Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Next Test Date Required" Validate="True" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:LookupField HeaderTitle="Next Test Type" SortAscUrl="" SortDescUrl="" DataField="NXT_TST_TYP_CD"
                                    ForeignDataField="NXT_TST_TYP_ID" HeaderText="Next Test Type" PrimaryDataField="ENM_ID"
                                    ReadOnly="true">
                                    <Lookup DependentChildControls="" HelpText="260,ENUM_ENM_CD" iCase="None" OnClientTextChange=""
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
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="true" />
                                </iFg:LookupField>
                                <%--   Added for GWS--%>
                                <iFg:LookupField HeaderTitle="Prev ONH Loc" SortAscUrl="" SortDescUrl="" DataField="PRVS_ONHR_LCTN_CD"
                                    ForeignDataField="PRVS_ONHR_LCTN_ID" HeaderText="Prev ONH Loc" PrimaryDataField="PRT_ID">
                                    <Lookup DependentChildControls="" HelpText="262" iCase="None" ValidationGroup=""
                                        CssClass="lkp" DataKey="PRT_CD" DoSearch="True" TableName="28" AllowSecondaryColumnSearch="false"
                                        SecondaryColumnName="PRT_CD">
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px" />
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="PRT_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="PRT_CD" ControlToBind="" Hidden="False"></Inp:LookupColumn>
                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="PRT_DSCRPTN_VC" ControlToBind=""
                                                Hidden="False"></Inp:LookupColumn>
                                        </LookupColumns>
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Previous On Hire Location. Click on the List for Valid Values"
                                            ReqErrorMessage="Previous On Hire Location Required" Validate="True" IsRequired="false"
                                            CustomValidation="false" CustomValidationFunction="" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="90px" Wrap="true" />
                                </iFg:LookupField>
                                <iFg:DateField DataField="PRVS_ONHR_DT" HeaderText="Prev ONH Date" HeaderTitle=""
                                    SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                                    HtmlEncode="false">
                                    <iDate HelpText="254" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="100px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="80px" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid  Manuf. Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Prev.Onh Date Required" Validate="True" CompareValidation="true"
                                            CmpErrorMessage="Prev.Onh Date cannot be greater than current Date." />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:TextboxField DataField="CSC_VLDTY" HeaderText="CSC Validity" HeaderTitle="CSC Validity"
                                    SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="txt" HelpText="265" iCase="None" MaxLength="5">
                                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                                            CustomValidation="false" Type="String" Validate="true" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:TextboxField>
                                <%--   Added for GWS--%>
                                <iFg:TextboxField DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                                    SortAscUrl="" SortDescUrl="" SortExpression="">
                                    <TextBox ID="txtRemarks" HelpText="268" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt" MaxLength="500">
                                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                                            CustomValidation="false" Type="String" Validate="true" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                </iFg:TextboxField>
                                <iFg:ImageField HeaderTitle="Upload" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/attachment.png"
                                    HeaderImageUrl="../Images/attachment.png">
                                    <%--   <ItemStyle  Wrap="True" HorizontalAlign ="Center" />--%>
                                    <ItemStyle Width="20px" Wrap="True" HorizontalAlign="Center" />
                                </iFg:ImageField>
                                <iFg:CheckBoxField DataField="ACTV_BT" HeaderText="Active" HeaderTitle="Active" HelpText=""
                                    SortAscUrl="" SortDescUrl="" HeaderImageUrl="">
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Width="20px" Wrap="True" HorizontalAlign="Center" />
                                </iFg:CheckBoxField>
                                <iFg:CheckBoxField DataField="RNTL_BT" HeaderText="Rental" HeaderTitle="Rental" HelpText=""
                                    SortAscUrl="" SortDescUrl="" HeaderImageUrl="">
                                    <HeaderStyle HorizontalAlign="Left" Width="9%"></HeaderStyle>
                                    <ItemStyle Wrap="True" HorizontalAlign="Center" />
                                </iFg:CheckBoxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                        </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
        </table>
        <div align="center">
            <a href="#" id="btnSubmitEq" onclick="onSubmit();return false;" class="btn btn-small btn-success"
                runat="server" style="font-weight: bold"><i class="icon-save"></i>&nbsp;Save</a>
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();"
        onclientprint="null" />
    <asp:HiddenField ID="hdnEquipmentNo" runat="server" />
    </form>
</body>
</html>
