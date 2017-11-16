<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GateOut.aspx.vb" Inherits="Operations_GateOut" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="height: 100%;">
    <div>
        <div style="width: 100%">
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh">Operations >> Gate Out</span>
                    </td>
                    <td align="right">
                        <nv:Navigation ID="navGateOut" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="tabGateOut" class="tabdisplayGatePass" style="overflow: hidden; height: auto">
        <div>
            <Nav:iTab ID="ITab1" runat="server" ControlHeight="" ControlWidth="" EnableClose="False"
                SelectedTab="" TabHeight="" TabWidth="" TabsPerRow="15">
                <TabPages>
                    <Nav:TabPage ID="Pending" runat="server" Caption="Pending" doValidate="True" OnAfterSelect="ifgEquipmentDetailOnAfterPendingTabSelected();"
                        OnBeforeSelect="" TabPageClientId="divPending">
                    </Nav:TabPage>
                    <Nav:TabPage ID="MySubmits" runat="server" Caption="My Submits" doValidate="True"
                        OnAfterSelect="ifgEquipmentDetailOnAfterSubmitTabSelected();" OnBeforeSelect=""
                        TabPageClientId="divPending">
                    </Nav:TabPage>
                </TabPages>
            </Nav:iTab>
        </div>
        <!-- UIG Fix -->
        <div id="divPending" style="width: 100%;">
            <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width: 100%;">
                <tr style="width: 100%; height: 100%;">
                    <td>
                        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                            font-family: Arial; font-size: 8pt; display: none; width: 100%;" align="center">
                            <div>
                                No Records Found.</div>
                        </div>
                        <div id="divEquipmentDetail" style="margin: 1px;">
                            <iFg:iFlexGrid ID="ifgEquipmentDetail" runat="server" AllowStaticHeader="True" DataKeyNames="GTOT_ID"
                                Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
                                ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                                EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="setDefaultValues"
                                AddRowsonCurrentPage="False" ShowPageSizer="True" OnAfterCallBack="ifgEquipmentDetailOnAfterCB"
                                OnBeforeCallBack="" AllowSearch="True" AllowPaging="True" UseIcons="true" SearchButtonIconClass="icon-search"
                                SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                                AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                                DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                                RefreshButtonCssClass="btn btn-small btn-info" ShowFooter="true" SearchCancelButtonIconClass="icon-remove"
                                SearchCancelButtonCssClass="btn btn-small btn-danger" ClearButtonIconClass="icon-eraser"
                                ClearButtonCssClass="btn btn-small btn-success" AutoSearch="true" AllowRefresh="True"
                                PageSize="25" UseActivitySpecificDatasource="True">
                                <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <Columns>
                                    <iFg:LookupField DataField="CSTMR_CD" ForeignDataField="CSTMR_ID" HeaderText="Customer"
                                        HeaderTitle="Customer" PrimaryDataField="CSTMR_ID" SortAscUrl="" SortDescUrl=""
                                        AllowSearch="true" ReadOnly="true">
                                        <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="87,CUSTOMER_CSTMR_CD"
                                            iCase="Upper" OnClientTextChange="" ValidationGroup="divEquipmentDetail" MaxLength="15"
                                            TableName="9" CssClass="lkp" DoSearch="True" Width="120px" ClientFilterFunction=""
                                            AllowSecondaryColumnSearch="false" SecondaryColumnName="CSTMR_NAM" AutoSearch="true">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                                                <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                                                <Inp:LookupColumn ColumnCaption="Check Digit" ColumnName="CHK_DGT_VLDTN_BT" Hidden="false" />
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
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                        SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                                        <TextBox CausesValidation="True" CssClass="txt" HelpText="369,GATEOUT_EQPMNT_NO"
                                            iCase="Upper" MaxLength="11" OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                                            <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                                                CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No Required." />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="110px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type"
                                        HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                        HeaderStyle-Width="50px" AllowSearch="true" ReadOnly="true">
                                        <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="370,GATEOUT_EQPMNT_TYP_CD"
                                            iCase="Upper" OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10"
                                            TableName="3" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction=""
                                            AllowSecondaryColumnSearch="false" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                            AutoSearch="true">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Type" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False" ControlToBind="3" />
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
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="False"
                                                LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                                                ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divDetail" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:LookupField>
                                    <iFg:LookupField DataField="EQPMNT_CD_CD" ForeignDataField="EQPMNT_CD_ID" HeaderText="Code"
                                        HeaderTitle="Code" PrimaryDataField="EQPMNT_CD_ID" SortAscUrl="" SortDescUrl=""
                                        AllowSearch="true" ReadOnly="true">
                                        <Lookup DataKey="EQPMNT_CD_CD" DependentChildControls="" HelpText="371,GATEOUT_EQPMNT_CD_CD"
                                            iCase="Upper" OnClientTextChange="" ValidationGroup="divEquipmentDetail" MaxLength="10"
                                            TableName="7" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction=""
                                            AllowSecondaryColumnSearch="false" SecondaryColumnName="EQPMNT_CD_DSCRPTN_VC"
                                            AutoSearch="true">
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
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="False"
                                                LkpErrorMessage="Invalid Equipment Code. Click on the list for valid values"
                                                ReqErrorMessage="Equipment Code Required." Validate="True" CustomValidation="False"
                                                CustomValidationFunction="" CsvErrorMessage="" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="GTOT_CNDTN" HeaderText="Equipment Status"
                                        HeaderTitle="Equipment Status" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                                        AllowSearch="true" ReadOnly="true">
                                        <TextBox CssClass="txt" HelpText="373,GATEOUT_GTOT_CNDTN" iCase="None" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:LookupField DataField="EQPMNT_STTS_CD" ForeignDataField="EQPMNT_STTS_ID" HeaderText="Activity Status"
                                        HeaderTitle="Activity Status" PrimaryDataField="EQPMNT_STTS_ID" SortAscUrl=""
                                        SortDescUrl="" AllowSearch="true" ReadOnly="true">
                                        <Lookup DataKey="EQPMNT_STTS_CD" ID="lkpstatus" DependentChildControls="" HelpText="372,GATEOUT_EQPMNT_STTS_CD"
                                            iCase="Upper" OnClientTextChange="" ValidationGroup="divEquipmentDetail" MaxLength="15"
                                            TableName="54" CssClass="lkp" DoSearch="True" Width="100px" ClientFilterFunction=""
                                            AllowSecondaryColumnSearch="false" SecondaryColumnName="EQPMNT_STTS_DSCRPTN_VC"
                                            AutoSearch="true">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="EQPMNT_STTS_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_STTS_CD" Hidden="False" />
                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_STTS_DSCRPTN_VC"
                                                    Hidden="False" ControlToBind="" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                            <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" IsRequired="False"
                                                CustomValidation="False" CustomValidationFunction="" LkpErrorMessage="Invalid Equipment Status. Click on the list for valid values"
                                                ReqErrorMessage="Equipment Status Required." Validate="True" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="AUTH_NO" HeaderText="Booking Authorization No *"
                                        HeaderTitle="Booking Authorization No" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                                        AllowSearch="true" ReadOnly="false">
                                        <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                            MaxLength="14">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                                                RegularExpression="^[a-zA-Z0-9]+$" RegErrorMessage="Only alphabets and numbers allowed."
                                                RegexValidation="true" LookupValidation="False" ReqErrorMessage="Booking Authorization No Required"
                                                IsRequired="true" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="YRD_LCTN" HeaderText="Yard Loc" HeaderTitle="Yard Location"
                                        SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="373,GATEOUT_YRD_LCTN" iCase="None" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:DateField DataField="GTOT_DT" HeaderText="Out Date *" HeaderTitle="Out Date"
                                        SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false" AllowSearch="true">
                                        <iDate HelpText="374" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                            ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="True" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                                LkpErrorMessage="Invalid Out Date. Click on the calendar icon for valid values"
                                                ReqErrorMessage="Out Date Required" Validate="True" CompareValidation="True"
                                                CustomValidation="true" CmpErrorMessage="Out Date cannot be greater than current Date."
                                                CustomValidationFunction="ValidateGateOutDate" />
                                        </iDate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:DateField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="GTOT_TM" HeaderText="Time *" HeaderTitle="Time"
                                        SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="375,GATEOUT_GTOT_TM" iCase="Lower" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                IsRequired="true" ReqErrorMessage="Gate Out Time Required." RegexValidation="True"
                                                RegularExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]" RegErrorMessage="Invalid Time. Enter Time in hh:mm format and must be between 00:00 to 23:59" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="EIR_NO" HeaderText="EIR No" HeaderTitle="EIR No"
                                        SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="false">
                                        <TextBox CssClass="txt" HelpText="376,GATEOUT_EIR_NO" iCase="Upper" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="14">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                RegexValidation="True" LookupValidation="False" IsRequired="False" ReqErrorMessage="EIR No Required."
                                                RegularExpression="^[a-zA-Z0-9]*$" RegErrorMessage="Only Alphabets And Numbers are Allowed" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="VHCL_NO" HeaderText="Vehicle No"
                                        HeaderTitle="Vehicle No" SortAscUrl="" SortDescUrl="" AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="377,GATEOUT_VHCL_NO" iCase="None" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="SL_NO" HeaderText="Seal No" HeaderTitle="Seal No"
                                        SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                            MaxLength="100">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="TRNSPRTR_CD" HeaderText="Transporter"
                                        HeaderTitle="Transporter" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                                        AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="378,GATEOUT_TRNSPRTR_CD" iCase="None" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:LookupField DataField="GRD_CD" ForeignDataField="GRD_ID" HeaderText="Grade*"
                                        HeaderTitle="Grade" PrimaryDataField="GRD_ID" SortAscUrl="" SortDescUrl="" AllowSearch="true">
                                        <Lookup DataKey="GRD_CD" DependentChildControls="" HelpText="605,GATEOUT_EQPMNT_CD_CD"
                                            iCase="Upper" OnClientTextChange="" ValidationGroup="divEquipmentDetail" MaxLength="10"
                                            TableName="34" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction=""
                                            AllowSecondaryColumnSearch="false" SecondaryColumnName="GRD_DSCRPTN_VC" AutoSearch="true">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="GRD_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="GRD_CD" Hidden="False" />
                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="GRD_DSCRPTN_VC" Hidden="False" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="true"
                                                LkpErrorMessage="Invalid Grade. Click on the list for valid values" ReqErrorMessage="Grade Required."
                                                Validate="True" CustomValidation="True" CustomValidationFunction="CheckGrade"
                                                CsvErrorMessage="" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                                        SortAscUrl="" SortDescUrl="" SortExpression="RMRKS_VC" HtmlEncode="False" AllowSearch="true">
                                        <TextBox ID="txtRemarks" HelpText="430,GATEOUT_RMRKS_VC" OnClientTextChange="" ValidationGroup=""
                                            CssClass="txt">
                                            <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                                                RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                                                CustomValidation="false" Type="String" Validate="true" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    </iFg:TextboxField>
                                    <iFg:ImageField HeaderTitle="More Info" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/info.png"
                                        HeaderImageUrl="../Images/info.png">
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    </iFg:ImageField>
                                    <iFg:CheckBoxField DataField="CHECKED" HeaderText="" HeaderTitle="Select" HelpText=""
                                        HeaderImageUrl="../Images/flrsel.gif" SortAscUrl="" SortDescUrl="" Visible="True">
                                       <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    </iFg:CheckBoxField>
                                    <%--Added for Attchemnt--%>
                                    <iFg:ImageField HeaderTitle="Attach Files" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/attachment.png"
                                        HeaderText="Attach Files" HeaderImageUrl="">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="16px" Wrap="True" HorizontalAlign="Center" />
                                    </iFg:ImageField>
                                    <iFg:CheckBoxField DataField="RNTL_BT" HeaderText="Rental" HeaderTitle="Rental" HelpText=""
                                        SortAscUrl="" SortDescUrl="" HeaderImageUrl="" ReadOnly="true" IsEditable="false" ReadOnlyCssClass="disabled">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Center" />
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
            <asp:HiddenField ID="hdnchkdgtvalue" runat="server" />
            <asp:HiddenField ID="hdn005EqStatusID" runat="server" />
            <asp:HiddenField ID="hdn005EqStatusDesc" runat="server" />
            <asp:HiddenField ID="hdn009EqTypeID" runat="server" />
            <asp:HiddenField ID="hdn009EqTypeDesc" runat="server" />
            <asp:HiddenField ID="hdn010EqCodeID" runat="server" />
            <asp:HiddenField ID="hdn010EqCodeDesc" runat="server" />
            <asp:HiddenField ID="hdn006YardLoc" runat="server" />
            <asp:HiddenField ID="hdnMode" runat="server" />
            <asp:HiddenField ID="hdnCurrentDate" runat="server" />
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
        onclientprint="submitPrintPage();" />
    </form>
</body>
</html>
