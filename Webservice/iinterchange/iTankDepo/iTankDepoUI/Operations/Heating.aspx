<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Heating.aspx.vb" Inherits="Operations_Heating" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Operations >> Heating</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navEquipmentInfo" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divHeating">
        <div class="topspace">
        </div>
        <iFg:iFlexGrid ID="ifgHeating" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="10" AllowSearch="True"
            AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="300px" ShowEmptyPager="True"
            Width="100%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True"
            RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
            EnableViewState="False" AddButtonText="Add Row" DeleteButtonText="Delete Row"
            GridLines="Both" HorizontalAlign="NotSet" PageSizerFormat="" Scrollbars="None"
            Type="Normal" ValidationGroup="divHeating" DataKeyNames="HTNG_ID" OnAfterCallBack="ifgHeatingOnAfterCB"
            OnBeforeCallBack="ifgHeatingOnBeforeCB" OnAfterClientRowCreated="" Mode="Insert"
            AddRowsonCurrentPage="False" RowStyle-Wrap="True" UseIcons="true" SearchButtonIconClass="icon-search"
            SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
            AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
            DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
            RefreshButtonCssClass="btn btn-small btn-info" searchcancelbuttoniconclass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger" ClearButtonIconClass="icon-eraser" clearbuttoncssclass="btn btn-small btn-success" AllowAdd="False" AllowDelete="False">
            <Columns>
                <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False" ReadOnly="true">
                    <TextBox ID="txtEquipmentNo" HelpText="" OnClientTextChange="" ValidationGroup="divHeating"
                        CssClass="txt" MaxLength="11" iCase="Upper">
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="Equipment No Required"
                            CustomValidationFunction="" CustomValidation="True" Type="String" Validate="true"
                            CsvErrorMessage="This Equipment No already Exist." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type"
                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="" ReadOnly="true">
                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                        OnClientTextChange="" TableName="3" ValidationGroup="divHeating" ID="lkpEquipmentCode"
                        CssClass="lkp" DoSearch="True" Width="100px" ClientFilterFunction="">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="EQPMNT_TYP_ID" Hidden="true" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Equipment Type.Click on the List for Valid Values"
                            ReqErrorMessage="Equipment Type Required" Validate="True" IsRequired="False"
                            CustomValidation="false" CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="60px" Wrap="True" />
                </iFg:LookupField>
                <iFg:LookupField DataField="CSTMR_CD" ForeignDataField="CSTMR_ID" HeaderText="Customer"
                    HeaderTitle="Customer" PrimaryDataField="CSTMR_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="" ReadOnly="true">
                    <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="" iCase="Upper" OnClientTextChange=""
                        TableName="9" ValidationGroup="divHeating" ID="lkpCustomer" CssClass="lkp" DoSearch="True"
                        Width="110px" ClientFilterFunction="">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="CSTMR_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Customer Name" ColumnName="CSTMR_NAM" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Check Digit" ColumnName="CHK_DGT_VLDTN_BT" Hidden="false" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="280px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Customer.Click on the List for Valid Values"
                            ReqErrorMessage="Customer Required" Validate="True" IsRequired="False" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="60px" />
                </iFg:LookupField>
                <iFg:LookupField DataField="PRDCT_DSCRPTN_VC" ForeignDataField="PRDCT_ID" HeaderText="Previous Cargo"
                    HeaderTitle="Previous Cargo" PrimaryDataField="PRDCT_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="" ReadOnly="true">
                    <Lookup DataKey="PRDCT_DSCRPTN_VC" DependentChildControls="" HelpText="" iCase="Upper" OnClientTextChange=""
                        TableName="46" ValidationGroup="divHeating" ID="lklPreviousCargo" CssClass="lkp"
                        DoSearch="True" Width="100px" ClientFilterFunction="">
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
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Previous Cargo.Click on the List for Valid Values"
                            ReqErrorMessage="" Validate="True" IsRequired="False" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle  HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" />
                </iFg:LookupField>
                <iFg:DateField DataField="GTN_DT" HeaderText="In Date" HeaderTitle="Gate In Date"
                    SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                    HtmlEncode="false" AllowSearch="true" ReadOnly="true">
                    <iDate iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0" ValidationGroup=""
                        MaxLength="11" CssClass="txt" Width="150px">
                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <Validator CustomValidateEmptyText="True" Operator="LessThanEqual" Type="Date" IsRequired="false"
                            LkpErrorMessage="Invalid In Date. Click on the calendar icon for valid values"
                            ReqErrorMessage="In Date Required" Validate="True" CompareValidation="False"
                            CustomValidation="False" CmpErrorMessage="In Date cannot be greater than current Date."
                            CustomValidationFunction="" />
                    </iDate>
                    <HeaderStyle  HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="60px" Wrap="True" />
                </iFg:DateField>
                <iFg:DateField DataField="HTNG_STRT_DT" HeaderText="Start Date *" HeaderTitle="Heating Start Date"
                    SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                    HtmlEncode="false" AllowSearch="true">
                    <iDate HelpText="379" iCase="Upper" LeftPosition="0"
                        OnClientTextChange="ValidateTotalHeatingPeriod" TopPosition="0" ValidationGroup=""
                        MaxLength="11" CssClass="txt" Width="150px">
                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="20px" />
                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="True"
                            LkpErrorMessage="Invalid Heating Start Date. Click on the calendar icon for valid values"
                            ReqErrorMessage="Heating Start Date Required" Validate="True" CompareValidation="False"
                            CustomValidation="true" CmpErrorMessage="Heating Start Date cannot be greater than current Date."
                            CustomValidationFunction="ValidateHeatingStartDate"/>
                    </iDate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" />
                </iFg:DateField>
                <iFg:TextboxField CharacterLimit="0" DataField="HTNG_STRT_TM" HeaderText="Start Time *"
                    HeaderTitle="Heating Start Time" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="60px"
                    AllowSearch="true">
                    <TextBox CssClass="txt" HelpText="380,HEATING_CHARGE_HTNG_STRT_TM" iCase="lower"
                        OnClientTextChange="ValidateTotalHeatingPeriod" ValidationGroup="" MaxLength="5">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                            IsRequired="True" ReqErrorMessage="Heating Start Time Required" RegexValidation="True" CustomValidation="True" CustomValidationFunction ="ValidateHeatingStartTime"
                            RegularExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]" RegErrorMessage="Invalid Heating Start Time. Enter in hh:mm format and must be between 00:00 to 23:59" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign ="Left" ></HeaderStyle>
                    <ItemStyle Width="50px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:DateField DataField="HTNG_END_DT" HeaderText="End Date *" HeaderTitle="Heating End Date"
                    SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                    HtmlEncode="false" AllowSearch="true">
                    <iDate HelpText="381" iCase="Upper" LeftPosition="0" OnClientTextChange="ValidateTotalHeatingPeriod"
                        TopPosition="0" ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="True"
                            LkpErrorMessage="Invalid  Heating End Date. Click on the calendar icon for valid values"
                            ReqErrorMessage="Heating End Date Required" Validate="True" CompareValidation="False"
                            CustomValidation="true" CmpErrorMessage="Heating End Date cannot be greater than current Date."
                            CustomValidationFunction="ValidateHeatingEndDate" />
                    </iDate>
                    <HeaderStyle  HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" />
                </iFg:DateField>
                <iFg:TextboxField CharacterLimit="0" DataField="HTNG_END_TM" HeaderText="End Time *"
                    HeaderTitle="Heating End Time" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                    AllowSearch="true">
                    <TextBox CssClass="txt" HelpText="382,HEATING_CHARGE_HTNG_END_TM" iCase="lower" OnClientTextChange="ValidateTotalHeatingPeriod"
                        ValidationGroup="divHeating" MaxLength="5">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                            CustomValidation="True" CustomValidationFunction="ValidateHeatingEndTime" CsvErrorMessage="Heating End Time should be greater than Heating Start Time"
                            RegexValidation="True" RegularExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]" RegErrorMessage="Invalid Heating End Time. Enter in hh:mm format and must be between 00:00 to 23:59"
                            IsRequired="True" ReqErrorMessage="Heating End Time Required" />
                    </TextBox>
                    <HeaderStyle  HorizontalAlign ="Left" ></HeaderStyle>
                    <ItemStyle Width="50px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField CharacterLimit="0" DataField="HTNG_TMPRTR" HeaderText="Temp   (°C) *"
                    HeaderTitle="Heating Temperature (°C)" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                    AllowSearch="true">
                    <TextBox CssClass="txt" HelpText="383,HEATING_CHARGE_HTNG_TMPRTR" iCase="Upper" OnClientTextChange=""
                        ValidationGroup="divHeating" MaxLength="5">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                            RegexValidation="False" RegularExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]" RegErrorMessage="Invalid Temperature."
                            IsRequired="true" ReqErrorMessage="Heating Temperature Required" />
                    </TextBox>
                    <HeaderStyle  HorizontalAlign ="Left" ></HeaderStyle>
                    <ItemStyle Width="50px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField CharacterLimit="0" DataField="TTL_HTN_PRD" HeaderText="Total Period"
                    HeaderTitle="Total Heating Period" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                    AllowSearch="true" ReadOnly ="true" >
                    <TextBox CssClass="txt" HelpText="384,HEATING_CHARGE_TTL_HTN_PRD" iCase="Numeric" 
                        OnClientTextChange="calculateTotalRate" ValidationGroup="divHeating" MaxLength="5">
                        <Validator CustomValidateEmptyText="False" Operator="GreaterThanEqual"  Type="String" Validate="True"
                            IsRequired="True" ReqErrorMessage="Total Heating Period Required" 
                             RegErrorMessage="Invalid  Total Heating Period. Range must be from 0.10 to 999.99" RegexValidation="true" RegularExpression="^\d{1,3}(\.\d{1,2})?$"
                              CmpErrorMessage="Total Heating Period must be greater than 0.10" CompareValidation="True" ValueToCompare="0.10"
                            CustomValidation="True" CustomValidationFunction ="ValidateTotalHeatingPeriod" />
                    </TextBox>
                    <HeaderStyle  HorizontalAlign ="Left" ></HeaderStyle>
                    <ItemStyle Width="50px" Wrap="True" />
                </iFg:TextboxField>              
                <iFg:TextboxField CharacterLimit="0" DataField="MIN_HTNG_RT_NC" HeaderText="Min. Rate"
                    HeaderTitle="Min. Rate" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                    AllowSearch="true" ReadOnly ="true">
                    <TextBox CssClass="txt" HelpText="385,HEATING_CHARGE_MIN_HTNG_RT_NC" iCase="Lower"
                        OnClientTextChange="" ValidationGroup="divHeating" MaxLength="5">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                            RegexValidation="False" RegularExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]" RegErrorMessage="Invalid Event Time. Enter hh:mm Format"
                            IsRequired="true" ReqErrorMessage="Minimun Heating Rate Required" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign ="Left" ></HeaderStyle>
                    <ItemStyle Width="50px" Wrap="True" HorizontalAlign ="Right"  />
                </iFg:TextboxField>
                <iFg:TextboxField CharacterLimit="0" DataField="HRLY_CHRG_NC" HeaderText="Hourly Rate"
                    HeaderTitle="Hourly Rate" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                    AllowSearch="true" ReadOnly ="true" >
                    <TextBox CssClass="txt" HelpText="386,HEATING_CHARGE_HRLY_CHRG_NC" iCase="Lower"
                        OnClientTextChange="calculateTotalRate" ValidationGroup="divHeating" MaxLength="5">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                            IsRequired="true" ReqErrorMessage="Hourly Heating Charge Required" RegexValidation="false"
                            RegularExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]" RegErrorMessage="Invalid Event Time. Enter hh:mm Format"
                            CustomValidation="false" CustomValidationFunction="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign ="Left" ></HeaderStyle>
                    <ItemStyle Width="50px" Wrap="True" HorizontalAlign ="Right"  />
                </iFg:TextboxField>
                <iFg:TextboxField CharacterLimit="0" DataField="TTL_RT_NC" HeaderText="Total Rate"
                    HeaderTitle="Total Rate" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                    AllowSearch="true" ReadOnly="true">
                    <TextBox CssClass="txt" HelpText="387,HEATING_CHARGE_TTL_RT_NC" iCase="Lower" OnClientTextChange=""
                        ValidationGroup="divHeating" MaxLength="5">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                            RegexValidation="False" RegularExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]" RegErrorMessage="Invalid Event Time. Enter hh:mm Format" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" ></HeaderStyle>
                    <ItemStyle Width="50px" Wrap="True" HorizontalAlign ="Right"  />
                </iFg:TextboxField>
                <iFg:CheckBoxField DataField="CHECKED" HeaderText="" HeaderTitle="Select" HelpText=""
                    HeaderImageUrl="../Images/flrsel.gif" SortAscUrl="" SortDescUrl="" Visible="True">
                    <ItemStyle Width="15px" Wrap="True" />
                </iFg:CheckBoxField>
            </Columns>
            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <PagerStyle CssClass="gpage" Height="18px" Font-Names="Verdana" HorizontalAlign="Center" />
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
        </iFg:iFlexGrid>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();"
        onclientprint="null" />
    <asp:HiddenField ID="hdnPageTitle" runat="server" />
    </form>
</body>
</html>
