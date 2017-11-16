<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BulkEmail.aspx.vb" Inherits="Tracking_BulkEmail" %>

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
                    <span id="spnHeader" class="ctabh">Tracking >> Bulk Email</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navEquipmentInfo" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divBulkEmail" style="overflow-y: auto;overflow-x: auto; height: auto">
    <div id="divSearch" >
        <fieldset class="blbl" id="fldSearch">
            <legend>Search</legend>
            <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width: 100%;">
                <tr>
                    <td>
                        <table border="0" cellpadding="2" align="left" cellspacing="2" class="tblstd" style="width: 100%;" >
                            <tr>
                                <td>
                                    <label id="lblCustomer" runat="server" class="lbl" style="font-weight: normal">
                                        Customer</label>
                                    <Inp:iLabel ID="ILabel6" runat="server" CssClass="lblReq">
                   *
                                    </Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iLookup ID="lkpCustomer" runat="server" CssClass="lkp" DataKey="CSTMR_CD" DoSearch="True"
                                        iCase="Upper" MaxLength="10" TabIndex="1" TableName="49" HelpText="405,CUSTOMER_CSTMR_CD"
                                        ClientFilterFunction="" ReadOnly="false" AllowSecondaryColumnSearch="true" SecondaryColumnName="CSTMR_NAM">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnName="CSTMR_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="CSTMR_NAM" Hidden="False" />
                                        </LookupColumns>
                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Customer. Click on the list for valid values"
                                            Operator="Equal" ReqErrorMessage="Customer Required" Type="String" Validate="True"
                                            ValidationGroup="divSearch" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iLookup>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <label id="lblEquipmentNo" runat="server" class="lbl" style="font-weight: normal">
                                        Equipment No</label>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtEquipmentNo" runat="server" CssClass="txt" HelpText="406,TRACKING_EQPMNT_NO"
                                        iCase="Upper" TabIndex="2" ToolTip="Equipment Number">
                                        <Validator IsRequired="false" ReqErrorMessage="Equipment Number Required" Validate="True"
                                            ValidationGroup="divSearch" />
                                    </Inp:iTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <Inp:iLabel ID="lblInDateFrom" runat="server" CssClass="lbl" Style="font-weight: normal">
                In Date From
                                    </Inp:iLabel>
                                </td>
                                <td class="style5">
                                    <Inp:iDate ID="datInDateFrom" runat="server" HelpText="407" CssClass="dat" MaxLength="11"
                                        iCase="Upper" TabIndex="3" ReadOnly="False" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false">
                                        <Validator CustomValidateEmptyText="True" IsRequired="False" Type="Date" ValidationGroup="divSearch"
                                            Operator="LessThanEqual" LkpErrorMessage="Invalid In Date From. Click on the calendar icon for valid values"
                                            Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage=""
                                            CmpErrorMessage="In Date From, must be greater than or equal to In Date To" RangeValidation="False"
                                            CustomValidation="True" CustomValidationFunction="validateInDateFrom" ControlToCompare="" />
                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iDate>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <Inp:iLabel ID="lblInDateTo" runat="server" CssClass="lbl" Style="font-weight: normal">
                In Date To
                                    </Inp:iLabel>
                                </td>
                                <td class="style5">
                                    <Inp:iDate ID="datInDateTo" runat="server" HelpText="408" CssClass="dat" MaxLength="11"
                                        iCase="Upper" TabIndex="4" ReadOnly="False" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false">
                                        <Validator CustomValidateEmptyText="True" IsRequired="False" Type="Date" ValidationGroup="divSearch"
                                            Operator="LessThanEqual" LkpErrorMessage="Invalid In Date To. Click on the calendar icon for valid values"
                                            Validate="True" CsvErrorMessage="" CompareValidation="False" ReqErrorMessage=""
                                            CmpErrorMessage="" RangeValidation="False" CustomValidation="True" CustomValidationFunction="validateInDateTo" />
                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iDate>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <Inp:iLabel ID="lblEventDateFrom" runat="server" CssClass="lbl" Style="font-weight: normal">
               Activity Date From
                                    </Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iDate ID="datEventDateFrom" runat="server" HelpText="410" CssClass="dat" MaxLength="11"
                                        iCase="Upper" TabIndex="5" ReadOnly="False" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false">
                                        <Validator CustomValidateEmptyText="True" IsRequired="False" Type="Date" ValidationGroup="divSearch"
                                            Operator="LessThanEqual" LkpErrorMessage="Invalid Activity Date From. Click on the calendar icon for valid values"
                                            Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage=""
                                            CmpErrorMessage="Activity Date To, must be greater than or equal to Activity Date From"
                                            RangeValidation="False" CustomValidation="True" CustomValidationFunction="validateActivityDateFrom"
                                            ControlToCompare="" />
                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iDate>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <Inp:iLabel ID="lblEventDateTo" runat="server" CssClass="lbl" Style="font-weight: normal">
               Activity Date To
                                    </Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iDate ID="datEventDateTo" runat="server" HelpText="411" CssClass="dat" MaxLength="11"
                                        iCase="Upper" TabIndex="6" ReadOnly="False" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false">
                                        <Validator CustomValidateEmptyText="True" IsRequired="False" Type="Date" ValidationGroup="divSearch"
                                            Operator="LessThanEqual" LkpErrorMessage="Invalid Activity Date To. Click on the calendar icon for valid values"
                                            Validate="True" CsvErrorMessage="" CompareValidation="False" ReqErrorMessage=""
                                            CmpErrorMessage="" RangeValidation="False" CustomValidation="True" CustomValidationFunction="validateActivityDateTo" />
                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iDate>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <Inp:iLabel ID="lblEmail" runat="server" CssClass="lbl" Style="font-weight: normal">
               Status
                                    </Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iLookup ID="lkpEmail" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                                        iCase="None" MaxLength="10" TabIndex="7" TableName="62" HelpText="412,ENUM_ENM_CD"
                                        ClientFilterFunction="">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Status" />
                                        </LookupColumns>
                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="200px" VerticalAlign="Top"
                                            HorizontalAlign="Right" />
                                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                                            LkpErrorMessage="Invalid Email. Click on the list for valid values." Operator="Equal"
                                            ReqErrorMessage="Activity Required" Type="String" Validate="True" ValidationGroup="divSearch" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iLookup>
                                </td>
                                <td>
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="9">
                                    <div>
                                        <fieldset class="lbl" id="fldActivityType">
                                            <legend class="blbl">Activity Type</legend>
                                            <table style="width: 100%;">
                                                <tr>
                                                  
                                                    <td style="width: 15%;">
                                                        <Inp:iLabel ID="lblRepairEstimate" runat="server" CssClass="lbl" Style="font-weight: normal">
               Repair Estimate
                                                        </Inp:iLabel>
                                                    </td>
                                                    <td style="width: 5%;">
                                                        <asp:CheckBox ID="chkRepairEstimate" CssClass="chk" runat="server" TabIndex="8" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="width: 15%;">
                                                        <Inp:iLabel ID="lblRepairApproval" runat="server" CssClass="lbl" Style="font-weight: normal">
             Repair Approval
                                                        </Inp:iLabel>
                                                    </td>
                                                    <td style="width: 5%;">
                                                        <asp:CheckBox ID="chkRepairApproval" CssClass="chk" runat="server" TabIndex="9" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="width: 17%;">
                                                        <Inp:iLabel ID="lblRepairCompletion" runat="server" CssClass="lbl" Style="font-weight: normal">
             Repair Completion
                                                        </Inp:iLabel>
                                                    </td>
                                                    <td style="width: 3%;">
                                                        <asp:CheckBox ID="chkRepairCompletion" CssClass="chk" runat="server" TabIndex="10" />
                                                    </td>
                                                     <td>
                                                    </td>
                                                      <td style="width: 10%;">
                                                      <div id="divCleaning">
                                                        <Inp:iLabel ID="lblCleaning" runat="server" CssClass="lbl" Style="font-weight: normal">
               Cleaning 
                                                        </Inp:iLabel>
                                                        </div>
                                                    </td>
                                                    <td style="width: 5%;">
                                                    <div id="divCleaingCheck">
                                                        <asp:CheckBox ID="chkCleaning" CssClass="chk" runat="server" TabIndex="11" />
                                                        </div> 
                                                    </td>
                                                   
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                </td>
                                <td colspan="2">
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <ul style="margin: 0px;">
                                                    <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="hypSearch" style="text-decoration: none;
                                                        color: White; font-weight: bold" class="icon-search" runat="server" onclick="onSearchClick(); return false;"
                                                        tabindex="12">&nbsp;Search</a></li>
                                                </ul>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <%--   <table align="center">
            <tr>
                <td>
                    <ul style="margin: 0px;">
                        <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="hypSearch" style="text-decoration: none;
                            color: White; font-weight: bold" class="icon-search" runat="server" onclick="onSearchClick(); return false;"
                            tabindex="9">&nbsp;Search</a></li>
                    </ul>
                </td>
            </tr>
        </table>--%>
        </fieldset>
    </div>
    <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
        font-family: Arial; font-size: 8pt; display: none; width: 80%;" align="center">
        <div>
            No Records Found.</div>
    </div>
    <%--  <div id="divTest" runat="server" >
    
    </div>--%>
    <div id="divBulkMail" style="margin: 1px; width: 99.9%; vertical-align: middle;">
        <fieldset class="lbl" id="fldBulkMail">
            <legend class="blbl">Bulk Email</legend>
            <table border="0" cellpadding="2" cellspacing="2" class="" style="width: 100%;">
                <tr style="width: 100%;">
                    <td colspan="5">
                        <iFg:iFlexGrid ID="ifgBulkEmail" runat="server" AllowStaticHeader="True" DataKeyNames="TRCKNG_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="160px" Type="Normal"
                            ValidationGroup="divBulkMail" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="10" AddRowsonCurrentPage="False" AllowPaging="True" AllowAdd="false"
                            AllowDelete="False" ShowFooter="True" OnAfterCallBack="" AllowRefresh="True"
                            AllowSearch="True" AutoSearch="True" OnBeforeCallBack="" RowStyle-HorizontalAlign="NotSet"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="btn btn-small btn-success"
                            SearchCancelButtonCssClass="btn btn-small btn-danger" ClearButtonIconClass="icon-eraser">
                            <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:TextboxField CharacterLimit="0" DataField="EQPMNT_NO" HeaderText="Equipment No"
                                    HeaderTitle="Equipment No" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                                    AllowSearch="true" ReadOnly="true">
                                    <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="divBulkMail"
                                        MaxLength="5">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                            RegexValidation="False" LookupValidation="False" />
                                    </TextBox>
                                    <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="80px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:LookupField DataField="CSTMR_CD" ForeignDataField="CSTMR_ID" HeaderText="Customer "
                                    HeaderTitle="Customer" PrimaryDataField="CSTMR_ID" SortAscUrl="" SortDescUrl=""
                                    AllowSearch="False" ReadOnly="true">
                                    <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="" iCase="Upper" OnClientTextChange=""
                                        ValidationGroup="divBulkMail" MaxLength="15" TableName="9" CssClass="lkp" DoSearch="True"
                                        Width="120px" ClientFilterFunction="" AllowSecondaryColumnSearch="true" SecondaryColumnName="CSTMR_NAM"
                                        AutoSearch="True">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="False"
                                            LkpErrorMessage="Invalid Customer. Click on the list for valid values" ReqErrorMessage="Customer Required."
                                            Validate="True" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="70px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:DateField DataField="ACTVTY_DT" HeaderText="Activity Date" HeaderTitle="Activity Date"
                                    SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                                    HtmlEncode="false" AllowSearch="true" ReadOnly="true">
                                    <iDate HelpText="374,GATEOUT_GTOT_DT" iCase="Upper" LeftPosition="0" OnClientTextChange=""
                                        TopPosition="0" ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="True" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Activity Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Activity Date Required" Validate="True" CompareValidation="False"
                                            CustomValidation="False" CmpErrorMessage="Activity Date cannot be greater than current Date."
                                            CustomValidationFunction="" />
                                    </iDate>
                                    <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="80px" Wrap="True" />
                                </iFg:DateField>
                                <iFg:HyperLinkField DataTextField="ACTVTY_NAM" Text="Activity" HeaderText="Activity"
                                    HeaderTitle="" MaxLength="0" SortAscUrl="" SortDescUrl="" IsEditable="False"
                                    ReadOnly="True" SortExpression="ACTVTY_NAM" AllowSearch="true">
                                    <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="90px" />
                                </iFg:HyperLinkField>
                                <iFg:HyperLinkField DataTextField="RPRT" Text="Report" HeaderText="Report" HeaderTitle=""
                                    MaxLength="0" SortAscUrl="" SortDescUrl="" IsEditable="False" ReadOnly="True"
                                    SortExpression="RPRT" AllowSearch="true">
                                    <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="90px" />
                                </iFg:HyperLinkField>
                                <iFg:DateField DataField="GTN_DT" HeaderText="In Date" HeaderTitle="In Date" SortAscUrl=""
                                    HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                                    HtmlEncode="false" AllowSearch="true" ReadOnly="true">
                                    <iDate HelpText="374,GATEOUT_GTOT_DT" iCase="Upper" LeftPosition="0" OnClientTextChange=""
                                        TopPosition="0" ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="True" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid In Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="In Date Required" Validate="True" CompareValidation="False"
                                            CustomValidation="False" CmpErrorMessage="In Date cannot be greater than current Date."
                                            CustomValidationFunction="" />
                                    </iDate>
                                    <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="80px" Wrap="True" />
                                </iFg:DateField>
                                <iFg:TextboxField CharacterLimit="0" DataField="LST_SNT_BY" HeaderText="Last Sent By"
                                    HeaderTitle="Last Sent By" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                                    AllowSearch="true" ReadOnly="true">
                                    <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                        MaxLength="5">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                            RegexValidation="False" LookupValidation="False" />
                                    </TextBox>
                                    <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="80px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:DateField DataField="LST_SNT_DT" HeaderText="Last Sent Date" HeaderTitle="Last Sent Date"
                                    SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy hh:mm:ss tt}"
                                    HtmlEncode="false" AllowSearch="true" ReadOnly="true">
                                    <iDate HelpText="374,GATEOUT_GTOT_DT" iCase="Upper" LeftPosition="0" OnClientTextChange=""
                                        TopPosition="0" ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="True" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Last Sent Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Last Sent Date Required" Validate="True" CompareValidation="False"
                                            CustomValidation="False" CmpErrorMessage="Last Sent Date cannot be greater than current Date."
                                            CustomValidationFunction="" />
                                    </iDate>
                                    <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="130px" Wrap="True" />
                                </iFg:DateField>
                                <iFg:HyperLinkField DataTextField="TMS_SNT" HeaderText="No. of Times Sent" HeaderTitle=""
                                    MaxLength="0" SortAscUrl="" SortDescUrl="" IsEditable="False" ReadOnly="True"
                                    SortExpression="TMS_SNT">
                                    <HeaderStyle Width="40px" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                </iFg:HyperLinkField>
                                <iFg:CheckBoxField DataField="CHECKED" HeaderText="Bulk Email">
                                    <ItemStyle Width="25px" HorizontalAlign="Center" />
                                </iFg:CheckBoxField>
                                <iFg:ImageField HeaderTitle="Resend" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/BulkEmail.png"
                                    HeaderText="Resend" HeaderImageUrl="">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Center" />
                                </iFg:ImageField>
                                <iFg:TextboxField CharacterLimit="0" DataField="ERR_RMRKS" HeaderText="Error Remarks"
                                    HeaderTitle="Last Sent By" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                                    AllowSearch="true" ReadOnly="true">
                                    <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                        MaxLength="5">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                            RegexValidation="False" LookupValidation="False" />
                                    </TextBox>
                                    <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="80px" Wrap="True" />
                                </iFg:TextboxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                        </iFg:iFlexGrid>
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td>
                        <%--<sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();"
                            onclientprint="null" />--%>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnCustomer" runat="server" />
            <asp:HiddenField ID="hdnMailMode" runat="server" />
        </fieldset>
    </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();"
                            onclientprint="null" />
    </form>
</body>
</html>
