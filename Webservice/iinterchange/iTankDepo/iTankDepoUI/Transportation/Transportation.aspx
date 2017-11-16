<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Transportation.aspx.vb"
    Inherits="Transportation_Transportation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tblTransportation" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Transportation <span id="spnRequestNo">#</span><span
                        class='olbl' style="vertical-align: middle"></span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navTransportation" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Fix -->
    <div id="tabTransportation" style="overflow-y: auto;overflow-x: auto; height: auto">
        <table border="0" cellpadding="2" align="left" cellspacing="2" class="tblstd" style="width: 100%;">
            <tr>
                <td>
                    <Inp:iLabel ID="lblCustomer" runat="server" CssClass="lbl">
                    Customer
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblReqCustomer" runat="server" ToolTip="*" CssClass="lblReq">
                        *
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpCustomer" runat="server" CssClass="lkp" DataKey="CSTMR_CD" DoSearch="True"
                        iCase="Upper" TabIndex="1" TableName="9" HelpText="487,CUSTOMER_CSTMR_CD" OnClientTextChange="onCustomerChangeTripRate"
                        ClientFilterFunction="onApplyCustomer" Width="90px">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />                             
                            <Inp:LookupColumn ColumnCaption="Check Digit" ColumnName="CHK_DGT_VLDTN_BT" Hidden="true" />
                            <Inp:LookupColumn ColumnCaption="Bit" ColumnName="XML_BT" Hidden="true" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                            HorizontalAlign="Left" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                            LkpErrorMessage="Invalid Customer. Click on the list for valid values" ReqErrorMessage="Customer Required."
                            Validate="True" ValidationGroup="headTransportation" CustomValidation="false"
                            CustomValidationFunction="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td style="width: 100px">
                    <Inp:iLabel ID="lblRequestDate" runat="server" CssClass="lbl">
                    Request Date
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblReqRequestDate" runat="server" ToolTip="*" CssClass="lblReq">
                        *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iDate ID="datRequestDate" TabIndex="2" runat="server" HelpText="488" CssClass="dat"
                        iCase="Upper" ReadOnly="false" MaxLength="11" Width="90px">
                        <Validator CustomValidateEmptyText="False" IsRequired="true" Type="Date" ValidationGroup="headTransportation"
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Request Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="Request Date Required."
                            CmpErrorMessage="Request date Should not be greater than Current Date." RangeValidation="False"
                            CustomValidation="false" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
                <td>
                    <Inp:iLabel ID="lblRoute" runat="server" CssClass="lbl">
                    Route
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblReqRoute" runat="server" ToolTip="*" CssClass="lblReq">
                        *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpRoute" runat="server" CssClass="lkp" DataKey="RT_DSCRPTN_VC"
                        DoSearch="True" iCase="Upper" TabIndex="3" TableName="68" HelpText="489,ROUTE_RT_CD"
                        OnClientTextChange="onChangeTripRate" ClientFilterFunction="" ReadOnly="false"
                        Width="90px">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="RT_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="RT_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Route Description" ColumnName="RT_DSCRPTN_VC" ControlToBind="lkpRoute"
                                Hidden="false" />
                            <Inp:LookupColumn ColumnCaption="Pick Up Location" ColumnName="PCK_UP_LCTN_CD" Hidden="false"
                                ControlToBind="lkpPickupLocation" />
                            <Inp:LookupColumn ColumnCaption="Drop Off Location" ColumnName="DRP_OFF_LCTN_CD"
                                Hidden="false" ControlToBind="lkpDropLocation" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="600px" VerticalAlign="Top"
                            HorizontalAlign="right" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                            LkpErrorMessage="Invalid Route. Click on the list for valid values" ReqErrorMessage="Route Required."
                            Validate="True" ValidationGroup="headTransportation" CustomValidation="false"
                            CustomValidationFunction="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    <Inp:iLabel ID="lblTransporter" runat="server" CssClass="lbl">
                   Transporter
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblTransporterReq" runat="server" ToolTip="*" CssClass="lblReq">
                        *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpTransporter" runat="server" CssClass="lkp" DataKey="TRNSPRTR_CD"
                        DoSearch="True" iCase="Upper" TabIndex="4" TableName="79" HelpText="539,TRANSPORTER_TRNSPRTR_CD"
                        OnClientTextChange="onChangeTransporter" ClientFilterFunction="loadTransporter"
                        ReadOnly="false" Width="90px">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="TRNSPRTR_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="TRNSPRTR_CD" ControlToBind="lkpTransporter"
                                Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="TRNSPRTR_DSCRPTN" Hidden="false" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                            HorizontalAlign="right" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                            LkpErrorMessage="Invalid Transporter. Click on the list for valid values" ReqErrorMessage="Transporter Required."
                            Validate="True" ValidationGroup="headTransportation" CustomValidation="false"
                            CustomValidationFunction="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lblPickUpLocation" runat="server" CssClass="lbl">
                    Pick Up Location
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpPickupLocation" runat="server" CssClass="lkpd" DataKey="PCK_UP_LCTN_CD"
                        ToolTip="Pick up Location" DoSearch="True" iCase="Upper" TableName="65" HelpText=""
                        ClientFilterFunction="" ReadOnly="true" Width="90px">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="LCTN_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="LCTN_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="LCTN_DSCRPTN_VC" ColumnName="LCTN_DSCRPTN_VC" Hidden="False"
                                ControlToBind="" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="200px" VerticalAlign="Top" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                            LkpErrorMessage="Invalid Pickup Location Code. Click on the list for valid values"
                            ReqErrorMessage="Pickup Location Code Required." Validate="True" ValidationGroup=""
                            CustomValidation="false" CustomValidationFunction="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    <Inp:iLabel ID="lblDropOffLocation" runat="server" CssClass="lbl">
                    Drop Off Location
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpDropLocation" runat="server" CssClass="lkpd" DataKey="DRP_OFF_LCTN_CD"
                        ToolTip="Drop Off Location" DoSearch="True" iCase="Upper" TableName="65" HelpText=""
                        ClientFilterFunction="" ReadOnly="true" Width="90px">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="LCTN_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="LCTN_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="LCTN_DSCRPTN_VC" ColumnName="LCTN_DSCRPTN_VC" Hidden="False"
                                ControlToBind="" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="200px" VerticalAlign="Top" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                            LkpErrorMessage="Invalid Drop Off Location. Click on the list for valid values"
                            ReqErrorMessage="Drop Off Location Code Required." Validate="True" ValidationGroup=""
                            CustomValidation="false" CustomValidationFunction="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    <Inp:iLabel ID="lblActivityLocation" runat="server" CssClass="lbl">
                  Activity Location
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpActivityLocation" runat="server" CssClass="lkp" DataKey="LCTN_CD"
                        DoSearch="True" iCase="Upper" TabIndex="5" TableName="65" HelpText="540,LOCATION_LCTN_CD"
                        OnClientTextChange="" ClientFilterFunction="" ReadOnly="false" Width="90px">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="LCTN_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="LCTN_CD" ControlToBind="lkpActivityLocation"
                                Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="LCTN_DSCRPTN_VC" Hidden="false" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                            HorizontalAlign="right" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="false"
                            LkpErrorMessage="Invalid Activity Location. Click on the list for valid values"
                            ReqErrorMessage="Activity Location Required." Validate="True" ValidationGroup="headTransportation"
                            CustomValidation="false" CustomValidationFunction="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    <Inp:iLabel ID="lblActivity" runat="server" CssClass="lbl">
                    Activity
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblStatusRequired" runat="server" ToolTip="*" CssClass="lblReq">
                        *                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpActivity" runat="server" CssClass="lkp" DataKey="ENM_CD" ToolTip="Activity"
                        DoSearch="True" iCase="Upper" TableName="80" HelpText="173" ClientFilterFunction=""
                        OnClientTextChange="onStateChange" ReadOnly="false" Width="90px" TabIndex="6">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="ENM_CD" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="200px" VerticalAlign="Top"
                            HorizontalAlign="right" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                            LkpErrorMessage="Invalid Activity. Click on the list for valid values" ReqErrorMessage="Activity Required."
                            Validate="True" ValidationGroup="" CustomValidation="false" CustomValidationFunction="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lblStatus" runat="server" CssClass="lbl">
                   Status
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpStatus" runat="server" CssClass="lkpd" DataKey="ENM_CD" ToolTip="Status"
                        DoSearch="True" iCase="None" TableName="69" HelpText="490" ClientFilterFunction=""
                        AllowSecondaryColumnSearch="false" SecondaryColumnName="" ReadOnly="true" Width="90px">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="280px" VerticalAlign="Top"
                            HorizontalAlign="right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                            LkpErrorMessage="Invalid Status. Click on the list for valid values" Operator="Equal"
                            ReqErrorMessage="Status Required" Type="String" ValidationGroup="" Validate="True" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    <Inp:iLabel ID="lblTripRate" runat="server" CssClass="lbl">
                    Trip Rate
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtTripRate" runat="server" CssClass="ntxt" HelpText="519,TRANSPORTATION_TRP_RT_NC"
                        TextMode="SingleLine" iCase="Numeric" ToolTip="Full Trip Rate" ReadOnly="false"
                        OnClientTextChange="onTripRateChange" Width="90px" TabIndex="7">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegexValidation="true"
                            RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$" RegErrorMessage="Invalid Trip Rate. Range must be from 0.01 to 9999999.99"
                            Type="Double" Validate="True" ValidationGroup="headTransportation" LookupValidation="False"
                            CsvErrorMessage="" CustomValidationFunction="" IsRequired="false" ReqErrorMessage=""
                            CustomValidation="false" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <Inp:iLabel ID="lblRemarks" runat="server" CssClass="lbl">
                    Remarks
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtRemarks" runat="server" CssClass="txt" TabIndex="8" HelpText="494"
                        MaxLength="500" TextMode="MultiLine" iCase="None" ToolTip="Remarks" ReadOnly="false">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="" RegularExpression=""
                            Type="String" Validate="True" ValidationGroup="headTransportation" LookupValidation="False"
                            CsvErrorMessage="" CustomValidationFunction="" IsRequired="false" ReqErrorMessage=""
                            CustomValidation="false" RegexValidation="false" />
                    </Inp:iTextBox>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lTotalnoofTrips" runat="server" CssClass="lbl">
                 Total No of Trips
                    </Inp:iLabel>
                    </td>
                    <td>
                    <Inp:iLabel ID="lblTotalnoofTrips" runat="server" Font-Bold="true" CssClass="lbl">                
                    0.00
                    </Inp:iLabel>
                    </td>
                
                <td >
                    <Inp:iLabel ID="lTotalnoofEquipment" runat="server" CssClass="lbl">     
                No of Equipment
                    </Inp:iLabel>
                   
                     </td>
                    <td>
                    <Inp:iLabel ID="lblTotalnoofEquipment" runat="server" Font-Bold="true" CssClass="lbl">                            
                    0.00
                    </Inp:iLabel>
                    </td>
               
                <td>
                    <Inp:iLabel ID="lTotalAmount" runat="server" CssClass="lbl">     
                Total Amount         
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLabel ID="lblTotlAmount" runat="server" Font-Bold="true" CssClass="lbl"> 
                    0.00
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblDepotCurrency" runat="server" Font-Bold="true" CssClass="lbl">                   
                    </Inp:iLabel>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <Inp:iLabel ID="lblHeader2" runat="server" CssClass="lbl" Font-Bold="true" Font-Underline="true">
                Transportation Request Details
                    </Inp:iLabel>
                </td>
            </tr>
            </table>
                    <table border="0" cellpadding="2" style="width:100%" cellspacing="2" class="tblstd" >

            <tr>
                <td >
                    <iFg:iFlexGrid ID="ifgTransportation" runat="server" AllowStaticHeader="True" DataKeyNames="TRNSPRTTN_DTL_ID"
                        Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="130px" Type="Normal"
                        ValidationGroup="divTransportationDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                        EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="onAfterRowSetDefualtValues"
                        AllowAdd="true" PageSize="1000" AddRowsonCurrentPage="False" AllowPaging="false"
                        OnAfterCallBack="onAfterCallBakTransportation" AllowDelete="true" AllowRefresh="false"
                        AllowSearch="false" AutoSearch="True" UseIcons="true" SearchButtonIconClass="icon-search"
                        SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                        AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                        DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                        RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                        ClearButtonCssClass="icon-eraser" OnBeforeCallBack="onBeforeCallBackTransportation"
                        TabIndex="-1" UseActivitySpecificDatasource="True">
                        <PagerStyle CssClass="gpage" />
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="gsitem" />
                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                        <ActionButtons>
                            <iFg:ActionButton ID="btnMassApplyInput" OnClientClick="onClickMassApplyInput" Text="Mass Apply Input"
                                CSSClass="btn btn-small btn-success" IconClass="icon-save" ValidateRowSelection="False"
                                Visible="True" />
                        </ActionButtons>
                        <Columns>
                            <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No *" HeaderTitle="Equipment Number"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="491,TRANSPORTATION_DETAIL_EQPMNT_NO"
                                    OnClientTextChange="" iCase="Upper" ToolTip="Equipment Number" ValidationGroup="divTransportationDetail">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="true"
                                        ValidationGroup="divTransportationDetail" CustomValidationFunction="validateEquipmentno"
                                        IsRequired="true" ReqErrorMessage="Equipment Number Required" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="90px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type *"
                                HeaderTitle="Equipment Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                HeaderStyle-Width="50px" ReadOnly="false">
                                <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="495,EQUIPMENT_TYPE_EQPMNT_TYP_CD"
                                    iCase="Upper" OnClientTextChange="" ValidationGroup="divTransportationDetail"
                                    MaxLength="10" TableName="3" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction=""
                                    AllowSecondaryColumnSearch="false" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
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
                                        OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="NotSet" Width="300px"
                                        HorizontalAlign="Left" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                                        ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divTransportationDetail" />
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="60px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:TextboxField DataField="CSTMR_RF_NO" HeaderText="Customer Ref No *" HeaderTitle="Customer Reference No"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="493,TRANSPORTATION_DETAIL_CSTMR_RF_NO"
                                    iCase="None"   OnClientTextChange="" ToolTip="Customer Reference Number" ValidationGroup="divTransportationDetail" MaxLength ="15">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="true"
                                        CustomValidationFunction="ValidateCustomerRefNo" IsRequired="false"  ReqErrorMessage="Customer Reference Number Required" CustomValidateEmptyText ="true"  />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="120px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:LookupField DataField="PRDCT_DSCRPTN_VC" ForeignDataField="PRDCT_ID" HeaderText="Cargo"
                                HeaderTitle="Cargo" PrimaryDataField="PRDCT_ID" SortAscUrl="" SortDescUrl=""
                                AllowSearch="true">
                                <Lookup DataKey="PRDCT_DSCRPTN_VC" DependentChildControls="" HelpText="499,PRODUCT_PRDCT_DSCRPTN_VC"
                                    iCase="None" OnClientTextChange="" TableName="46" ValidationGroup="divTransportationDetail"
                                    ID="lklPreviousCargo" ToolTip="Previous Cargo" CssClass="lkp" DoSearch="True"
                                    Width="150px" ClientFilterFunction="" AutoSearch="true">
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
                                    <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="NotSet" Width="300px"
                                        HorizontalAlign="Left" />
                                    <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Cargo. Click on the List for Valid Values"
                                        ReqErrorMessage="" Validate="True" IsRequired="false" CustomValidation="false"
                                        ValidationGroup="divTransportationDetail" CustomValidationFunction="" />
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="100px" />
                            </iFg:LookupField>
                            <iFg:HyperLinkField HeaderText="Addl. Rate" HeaderTitle="Additional Rate" SortAscUrl=""
                                SortDescUrl="" Text="" IsEditable="False" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </iFg:HyperLinkField>
                            <iFg:DateField DataField="JB_STRT_DT" HeaderText="Job Start Date" HeaderTitle="Job Start Date"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                ReadOnly="false">
                                <iDate HelpText="496" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                    ToolTip="Job Start Date" ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                    <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                        ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator CustomValidateEmptyText="false" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                        LkpErrorMessage="Invalid Job Start Date. Click on the calendar icon for valid values"
                                        CustomValidation="true" CustomValidationFunction="compareRequestDate" ValidationGroup="divTransportationDetail"
                                        ReqErrorMessage="Job Start Date Required" Validate="True" RangeValidation="false"
                                        CompareValidation="true" />
                                </iDate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="90px" Wrap="True" HorizontalAlign="Left" Font-Bold="false" />
                            </iFg:DateField>
                            <iFg:DateField DataField="JB_END_DT" HeaderText="Job End Date" HeaderTitle="Job End Date"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                ReadOnly="false">
                                <iDate HelpText="497" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                    ToolTip="Job End Date" ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                    <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                        ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                        LkpErrorMessage="Invalid Job End Date. Click on the calendar icon for valid values"
                                        CustomValidation="true" ValidationGroup="divTransportationDetail" ReqErrorMessage="Job End Date Required"
                                        CustomValidationFunction="compareJobStartDate" Validate="True" RangeValidation="false"
                                        CompareValidation="true" />
                                </iDate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="90px" Wrap="True" HorizontalAlign="Left" Font-Bold="false" />
                            </iFg:DateField>
                            <iFg:TextboxField DataField="BLLNG_FLG" HeaderText="Billed" HeaderTitle="Billed"
                                SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange=""
                                    ToolTip="Billed" ValidationGroup="divTransportationDetail">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="false"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="40px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:LookupField DataField="EMPTY_SNGL_CD" ForeignDataField="EMPTY_SNGL_ID" HeaderText="Empty Single"
                                HeaderTitle="Empty Single" PrimaryDataField="ENM_ID" SortAscUrl="" SortDescUrl=""
                                ReadOnly="false">
                                <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="520,ENUM_ENM_CD" iCase="Upper"
                                    OnClientTextChange="onClientCheckRequired" ValidationGroup="divTransportationDetail"
                                    TableName="76" CssClass="lkp" DoSearch="True" ToolTip="Empty Single" Width="120px"
                                    ClientFilterFunction="" OnLookupClick="">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Empty Single" ColumnName="ENM_CD" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="200px"
                                        HorizontalAlign="Right" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="FALSE"
                                        Validate="True" ValidationGroup="" ReqErrorMessage="Empty Single Required" LkpErrorMessage="Invalid Empty Single. Click on the list for valid values."
                                        CustomValidation="true" CustomValidationFunction="checkRequired" />
                                </Lookup>
                                <HeaderStyle Width="60px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="60px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:CheckBoxField DataField="CHK_BT" HeaderText="Mass Select" HeaderImageUrl=""
                                HeaderTitle="Select" HelpText="" SortAscUrl="" SortDescUrl="">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="25px" Wrap="True" HorizontalAlign="Left" />
                            </iFg:CheckBoxField>
                        </Columns>
                    </iFg:iFlexGrid>
                </td>
            </tr>
        </table>
    </div>
    <table style="margin: 0px auto; width: 100%">
        <tr>
            <td valign="top" >
                <div id="divPrintJobOrder">
                    <ul style="float: left; vertical-align: top; ">
                        <li class="btn btn-small btn-success"><a href="#" data-corner="8px" id="hypPrintJobRequest"
                            style="text-decoration: none; color: White; font-weight: bold; float: left; vertical-align: middle;"
                            class="icon-print" runat="server" onclick="printTransportationJobOrder(); return false;"
                            tabindex="22">&nbsp;<span style="font-family: Arial; line-height: 10px;">Print Job Request</span></a></li>
                    </ul>
                </div>
            </td>
            <td align="center" valign="middle" style="padding-top: 9px;">
                <div id="divSubmit">
                    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" align="center"
                        onClientPrint="null" />
                </div>
                <div id="divLockMessage" style="width: 100%; display: none; margin: 0px auto; text-align: left;
                    font-weight: bold; height: 30px; background-color: #CDFFCC; border: 1px solid #07760D;">
                </div>
            </td>
            <td align="left" valign="top">
                <div id="divCancelRequest">
                    <ul style="float: left; vertical-align: top;">
                        <li class="btn btn-small btn-danger"><a href="#" data-corner="8px" id="hypCancel"
                            class="icon-remove" style="text-decoration: none; color: White; font-weight: bold;
                            float: left; vertical-align: middle;" runat="server" onclick="onRequestCancel(); return false;"
                            tabindex="22">&nbsp;<span style="font-family: Arial; line-height: 10px;">Cancel</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnRouteId" runat="server" />
    <asp:HiddenField ID="hdnTransportationId" runat="server" />
    <asp:HiddenField ID="hdnchkdgtvalue" runat="server" />
    <asp:HiddenField ID="hdnRequestNo" runat="server" />
    <asp:HiddenField ID="hdnTripRate" runat="server" />
    <asp:HiddenField ID="hdnTransportationDetailId" runat="server" />
    <asp:HiddenField ID="hdnEquipmentStateId" runat="server" />
    <asp:HiddenField ID="hdnEquipmentStateCode" runat="server" />
    <asp:HiddenField ID="hdnDepotCurrency" runat="server" />
    <asp:HiddenField ID="hdnCustomerCurrency" runat="server" />
    <asp:HiddenField ID="hdnExchangeRate" runat="server" />
    <asp:HiddenField ID="hdnCurrentDate" runat="server" />
    <asp:HiddenField ID="hdnLockUserName" runat="server" />
    <asp:HiddenField ID="hdnIpError" runat="server" />
    <asp:HiddenField ID="hdnLockActivityName" runat="server" />
    <asp:HiddenField ID="hdnXml" runat="server" />
    </form>
</body>
</html>
