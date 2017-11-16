<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GateIn.aspx.vb" Inherits="Operations_GateIn" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
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
    <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Gate In</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navGateIn" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="tabGateIn" class="tabdisplayGatePass" style="height: auto; overflow: hidden;">
        <div>
            <Nav:iTab ID="ITab1" runat="server" ControlHeight="" ControlWidth="" EnableClose="False"
                SelectedTab="" TabHeight="" TabWidth="" TabsPerRow="15">
                <TabPages>
                    <Nav:TabPage ID="Pending" runat="server" Caption="Pending" doValidate="True" OnAfterSelect="ifgEquipmentDetailOnAfterPendingTabSelected();"
                        OnBeforeSelect="" TabPageClientId="divPending">
                    </Nav:TabPage>
                    <Nav:TabPage ID="MySubmits" runat="server" Caption="My Submits" doValidate="True"
                        OnAfterSelect="ifgEquipmentDetailOnAfterSubmitTabSelected();" OnBeforeSelect="ifgEquipmentDetailOnBeforeSubmitTabSelected();"
                        TabPageClientId="divPending">
                    </Nav:TabPage>
                </TabPages>
            </Nav:iTab>
        </div>
        <div id="divPending" style="width: 100%">
            <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width: 100%">
                <tr style="width: 100%; height: 100%;">
                    <td>
                        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                            font-family: Arial; font-size: 8pt; display: none; width: 100%;" align="center">
                            <div>
                                No Records Found.</div>
                        </div>
                        <%--                        UIG Fix--%>
                        <div id="divEquipmentDetail" style="margin: 1px; width: 100%; vertical-align: middle;
                            overflow-x: auto;">
                            <iFg:iFlexGrid ID="ifgEquipmentDetail" runat="server" AllowStaticHeader="True" DataKeyNames="GTN_ID"
                                Width="100%" PageSize="10" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1"
                                HorizontalAlign="Justify" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
                                ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                                EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="setDefaultValues"
                                AddRowsonCurrentPage="False" OnAfterCallBack="ifgEquipmentDetailOnAfterCB" OnBeforeCallBack="ifgEquipmentDetailOnBeforeCB"
                                AllowSearch="True" AllowPaging="True" UseIcons="True" SearchButtonCssClass="btn btn-small btn-info"
                                AddButtonCssClass="btn btn-small btn-success" DeleteButtonCssClass="btn btn-small btn-danger"
                                RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonCssClass="btn btn-small btn-danger"
                                ClearButtonCssClass="btn btn-small btn-success" AutoSearch="True" AllowRefresh="True"
                                Mode="Insert" UseActivitySpecificDatasource="True">
                                <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" />
                                <Columns>
                                    <iFg:LookupField DataField="CSTMR_CD" ForeignDataField="CSTMR_ID" HeaderText="Customer *"
                                        HeaderTitle="Customer" PrimaryDataField="CSTMR_ID" SortAscUrl="" SortDescUrl=""
                                        AllowSearch="true">
                                        <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="87,CUSTOMER_CSTMR_CD"
                                            ID="lkpCustCode" iCase="Upper" OnClientTextChange="bindCustomerName" ValidationGroup="divEquipmentDetail"
                                            ToolTip="Customer" MaxLength="15" TableName="9" CssClass="lkp" DoSearch="True"
                                            Width="5%" ClientFilterFunction="" AllowSecondaryColumnSearch="False" SecondaryColumnName="CSTMR_NAM"
                                            AutoSearch="true">
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
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="10%" />
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                LkpErrorMessage="Invalid Customer. Click on the list for valid values" ReqErrorMessage="Customer Required"
                                                Validate="True" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="10%" Wrap="True" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No *" HeaderTitle="Equipment No"
                                        SortAscUrl="" SortDescUrl="" AllowSearch="true">
                                        <TextBox CausesValidation="True" CssClass="txt" HelpText="84,GATEIN_EQPMNT_NO" iCase="Upper"
                                            MaxLength="11" OnClientTextChange="getSupplierDetails" ValidationGroup="divEquipmentDetail"
                                            ToolTip="Equipment No">
                                            <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="True"
                                                CustomValidationFunction="validateEquipmentno" IsRequired="True" ReqErrorMessage="Equipment No Required" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type *"
                                        HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                        HeaderStyle-Width="50px" AllowSearch="true">
                                        <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="85,EQUIPMENT_TYPE_EQPMNT_TYP_CD"
                                            ToolTip="Equipment Type" iCase="Upper" OnClientTextChange="" ValidationGroup="divDetail"
                                            MaxLength="10" ID="lkpEquipType" TableName="3" CssClass="lkp" DoSearch="True"
                                            Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="false" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                            AutoSearch="true">
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
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="20%" />
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                                                CustomValidation="true" CustomValidationFunction="setEquipmentCode" ReqErrorMessage="Equipment Type Required"
                                                Validate="True" ValidationGroup="divDetail" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:LookupField>
                                    <iFg:LookupField DataField="EQPMNT_CD_CD" ForeignDataField="EQPMNT_CD_ID" HeaderText="Code *"
                                        HeaderTitle="Code" PrimaryDataField="EQPMNT_CD_ID" SortAscUrl="" SortDescUrl=""
                                        ReadOnly="true" AllowSearch="true">
                                        <Lookup DataKey="EQPMNT_CD_CD" DependentChildControls="" HelpText="86,EQUIPMENT_CODE_EQPMNT_CD_CD"
                                            ToolTip="Equipment Code" iCase="Upper" OnClientTextChange="" ValidationGroup="divEquipmentDetail"
                                            MaxLength="10" TableName="7" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction=""
                                            AllowSecondaryColumnSearch="True" SecondaryColumnName="EQPMNT_CD_DSCRPTN_VC"
                                            ReadOnly="true" AutoSearch="true">
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
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" />
                                            <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" IsRequired="False"
                                                LkpErrorMessage="Invalid Equipment Code. Click on the list for valid values"
                                                ReqErrorMessage="Equipment Code Required" Validate="True" CustomValidation="True"
                                                CustomValidationFunction="ValidateEquipmentCode" CsvErrorMessage="" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:LookupField>
                                    <iFg:LookupField DataField="EQPMNT_STTS_CD" ForeignDataField="EQPMNT_STTS_ID" HeaderText="Status *"
                                        HeaderTitle="Status" PrimaryDataField="EQPMNT_STTS_ID" SortAscUrl="" SortDescUrl=""
                                        AllowSearch="true">
                                        <Lookup DataKey="EQPMNT_STTS_CD" ID="lkpstatus" DependentChildControls="" HelpText="89,EQUIPMENT_STATUS_EQPMNT_STTS_CD"
                                            iCase="Upper" OnClientTextChange="" ValidationGroup="divEquipmentDetail" MaxLength="15"
                                            ToolTip="Equipment Status" TableName="54" CssClass="lkp" DoSearch="True" Width="80px" ReadOnly="false"
                                            ClientFilterFunction="filterDefaultStatus" AllowSecondaryColumnSearch="true"
                                            SecondaryColumnName="EQPMNT_STTS_DSCRPTN_VC" AutoSearch="true">
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
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" />
                                            <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" IsRequired="False"
                                                CustomValidation="true" CustomValidationFunction="ValidateEquipmentStatus" LkpErrorMessage="Invalid Equipment Status. Click on the list for valid values"
                                                ReqErrorMessage="Equipment Status Required" Validate="True" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="YRD_LCTN" HeaderText="Yard Loc" HeaderTitle="Yard Location"
                                        SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="88,GATEIN_YRD_LCTN" iCase="None" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="5" ToolTip="Yard Location">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="80px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:DateField DataField="GTN_DT" HeaderText="In Date *" HeaderTitle="In Date" SortAscUrl=""
                                        HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false" AllowSearch="true">
                                        <iDate HelpText="90" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                            ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px" ToolTip="Gate In Date">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="True" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                                LkpErrorMessage="Invalid In Date. Click on the calendar icon for valid values"
                                                ReqErrorMessage="In Date Required" Validate="True" CompareValidation="True" CustomValidation="true"
                                                CmpErrorMessage="In Date cannot be greater than current Date." CustomValidationFunction="ValidateGateInDate" />
                                        </iDate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:DateField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="GTN_TM" HeaderText="Time *" HeaderTitle="Time"
                                        SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="91" iCase="Lower" ValidationGroup=""
                                            MaxLength="5" ToolTip="Gate In Time" OnClientTextChange="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="true"
                                                ReqErrorMessage="In Time Required" Validate="True" RegexValidation="True" RegularExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]"
                                                RegErrorMessage="Invalid In Time. Enter Time in hh:mm format and must be between 00:00 to 23:59" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="50px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <%----7--%>
                                    <iFg:LookupField DataField="PRDCT_DSCRPTN_VC" ForeignDataField="PRDCT_ID" HeaderText="Previous Cargo *"
                                        HeaderTitle="Previous Cargo" PrimaryDataField="PRDCT_ID" SortAscUrl="" SortDescUrl=""
                                        AllowSearch="true">
                                        <Lookup DataKey="PRDCT_DSCRPTN_VC" DependentChildControls="" HelpText="266,PRODUCT_PRDCT_DSCRPTN_VC"
                                            iCase="Upper" OnClientTextChange="" TableName="46" ValidationGroup="divEquipmentDetail"
                                            ToolTip="Previous Cargo" ID="lklPreviousCargo" CssClass="lkp" DoSearch="True"
                                            Width="150px" ClientFilterFunction="" AutoSearch="true" AllowSecondaryColumnSearch="true"
                                            SecondaryColumnName="PRDCT_DSCRPTN_VC">
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
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" HorizontalAlign="Center" />
                                            <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Previous Cargo. Click on the List for Valid Values"
                                                ReqErrorMessage="" Validate="True" IsRequired="false" CustomValidation="True"
                                                CustomValidationFunction="ValidateCargo" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="150px" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="EIR_NO" HeaderText="EIR NO" HeaderTitle="EIR NO"
                                        SortAscUrl="" SortDescUrl="" AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="93,GATEIN_EIR_NO" iCase="Upper" OnClientTextChange=""
                                            ToolTip="EIR No" ValidationGroup="" MaxLength="14" ReadOnly="False">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                RegexValidation="False" LookupValidation="False" IsRequired="False" ReqErrorMessage="EIR No Required"
                                                RegularExpression="^[a-zA-Z0-9]*$" RegErrorMessage="Only Alphabets And Numbers are Allowed" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="150px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="AGNT_CD" HeaderText="Agent" HeaderTitle="Agent"
                                        SortAscUrl="" SortDescUrl="" AllowSearch="true" IsEditable="false">
                                        <TextBox CssClass="txt" HelpText="590,GATEIN_AGNT_ID" iCase="Upper" OnClientTextChange=""
                                            ToolTip="Agent" ValidationGroup="" MaxLength="14" ID="txtAgntID" runat="server">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                RegexValidation="False" LookupValidation="False" IsRequired="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:LookupField DataField="BLL_CD" ForeignDataField="BLL_ID" HeaderText="Bill To"
                                        HeaderTitle="Bill To" PrimaryDataField="ENM_CD" SortAscUrl="" SortDescUrl=""
                                        IsEditable="false">
                                        <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="591,ENUM_ENM_CD" iCase="None"
                                            ID="lkpBillTo" OnClientTextChange="" ValidationGroup="divLineDetail" TableName="95"
                                            ReadOnly="false" ToolTip="Bill To" CssClass="lkp" DoSearch="True" Width="120px"
                                            ClientFilterFunction="" OnLookupClick="" MaxLength="1">
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
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="False"
                                                Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Bill To Party Required"
                                                CustomValidation="true" LkpErrorMessage="Invalid Bill To Party. Click on the list for valid values."
                                                CustomValidationFunction="validateBillTo" />
                                        </Lookup>
                                        <HeaderStyle Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="60px" Wrap="True" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="RDL_ATH" HeaderText="Redel Auth No"
                                        HeaderTitle="Redel Auth No" SortAscUrl="" SortDescUrl="" AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="592,GATEIN_RDL_ATH" iCase="Upper" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="14" ToolTip="Redel Auth No">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                RegexValidation="true" RegularExpression="^[a-zA-Z0-9]*$" RegErrorMessage="Only Alphabets And Numbers are Allowed"
                                                LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="CNSGNE" HeaderText="Consignee" HeaderTitle="Consignee"
                                        SortAscUrl="" SortDescUrl="" AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="593,GATEIN_CNSGNE" iCase="None" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="255" ToolTip="Consignee">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                RegexValidation="true" RegularExpression="^[a-zA-Z0-9]*$" RegErrorMessage="Only Alphabets And Numbers are Allowed"
                                                LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="VHCL_NO" HeaderText="Vehicle No"
                                        HeaderTitle="Vehicle No" SortAscUrl="" SortDescUrl="" AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="94,GATEIN_VHCL_NO" iCase="None" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="5" ToolTip="Vehicle No">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <%-- 14--%>
                                    <iFg:TextboxField CharacterLimit="0" DataField="TRNSPRTR_CD" HeaderText="Transporter"
                                        HeaderTitle="Transporter" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                                        AllowSearch="true">
                                        <TextBox CssClass="txt" HelpText="95,GATEIN_TRNSPRTR_CD" iCase="None" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="5" ToolTip="Transporter">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:CheckBoxField DataField="HTNG_BT" HeaderText="Heating" HeaderTitle="Heating"
                                        HelpText="" SortAscUrl="" SortDescUrl="">
                                        <ItemStyle Width="30px" />
                                    </iFg:CheckBoxField>
                                    <iFg:LookupField DataField="GRD_CD" ForeignDataField="GRD_ID" HeaderText="Grade *"
                                        HeaderTitle="Grade *" PrimaryDataField="GRD_ID" SortAscUrl="" SortDescUrl=""
                                        AllowSearch="true">
                                        <Lookup DataKey="GRD_CD" DependentChildControls="" HelpText="594,GATEIN_GRD_CD" iCase="Upper"
                                            OnClientTextChange="" TableName="34" ValidationGroup="divEquipmentDetail" ID="Lookup1"
                                            CssClass="lkp" ToolTip="Grade" DoSearch="True" Width="150px" ClientFilterFunction=""
                                            AutoSearch="true" AllowSecondaryColumnSearch="false" SecondaryColumnName="GRD_DSCRPTN_VC">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnCaption="ID" ColumnName="GRD_ID" Hidden="true" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="GRD_CD" Hidden="False" />
                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="GRD_DSCRPTN_VC" Hidden="False" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" HorizontalAlign="Center" />
                                            <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Grade Code. Click on the List for Valid Values"
                                                ReqErrorMessage="" Validate="True" IsRequired="False" CustomValidation="True"
                                                CustomValidationFunction="ValidateGrade" />
                                        </Lookup>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="150px" />
                                    </iFg:LookupField>
                                    <iFg:TextboxField DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                                        SortAscUrl="" SortDescUrl="" SortExpression="RMRKS_VC" HtmlEncode="False" AllowSearch="true">
                                        <TextBox ID="txtRemarks" HelpText="456,GATEIN_RMRKS_VC" OnClientTextChange="" ValidationGroup=""
                                            CssClass="txt" ToolTip="Remarks">
                                            <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                                                RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                                                CustomValidation="false" Type="String" Validate="False" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    </iFg:TextboxField>
                                    <iFg:ImageField HeaderTitle="More Info" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/info.png"
                                        HeaderImageUrl="../Images/info.png">
                                        <ItemStyle Width="16px" Wrap="True" />
                                    </iFg:ImageField>
                                    <iFg:ImageField HeaderTitle="Equipment Info" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/einfo.png"
                                        HeaderImageUrl="../Images/einfo.png">
                                        <ItemStyle Width="16px" Wrap="True" />
                                    </iFg:ImageField>
                                    <%--Added for Attachemnt--%>
                                    <iFg:ImageField HeaderTitle="Attach Files" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/attachment.png"
                                        HeaderText="Attach Files" HeaderImageUrl="">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="16px" Wrap="True" HorizontalAlign="Center" />
                                    </iFg:ImageField>
                                    <iFg:CheckBoxField DataField="CHECKED" HeaderText="" HeaderTitle="Select" HelpText=""
                                        HeaderImageUrl="../Images/flrsel.gif" SortAscUrl="" SortDescUrl="" Visible="True">
                                        <ItemStyle Width="15px" Wrap="True" />
                                    </iFg:CheckBoxField>
                                    <iFg:CheckBoxField DataField="RNTL_BT" HeaderText="Rental" HeaderTitle="Rental" HelpText=""
                                        HeaderImageUrl="" SortAscUrl="" SortDescUrl="" Visible="True">
                                        <ItemStyle Width="15px" Wrap="True" />
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
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();"
        onclientprint="submitPrintPage();" />
    <asp:HiddenField ID="hdnchkdgtvalue" runat="server" />
    <asp:HiddenField ID="hdn005EqStatusID" runat="server" />
    <asp:HiddenField ID="hdnDepotID" runat="server" />
    <asp:HiddenField ID="hdn005EqStatusDesc" runat="server" />
    <asp:HiddenField ID="hdn009EqTypeID" runat="server" />
    <asp:HiddenField ID="hdn009EqTypeDesc" runat="server" />
    <asp:HiddenField ID="hdn010EqCodeID" runat="server" />
    <asp:HiddenField ID="hdn010EqCodeDesc" runat="server" />
    <asp:HiddenField ID="hdn006YardLoc" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField ID="hdnCurrentDate" runat="server" />
    <asp:HiddenField ID="hdnRentalBit" runat="server" />
    <asp:HiddenField ID="hdnCurrentTime" runat="server" />
    </form>
</body>
</html>
