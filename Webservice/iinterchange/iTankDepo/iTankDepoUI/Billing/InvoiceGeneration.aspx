<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InvoiceGeneration.aspx.vb"
    Inherits="Billing_InvoiceGeneration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" >
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Billing >> Invoice Generation</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navExRate" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!--UIG Fix -->
    <div class="" id="tabInvoice" style="overflow-y: auto;overflow-x: auto; height: auto">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="center" style="width: 100%;">
            <tr>
                <td>
                    <fieldset class="lbl" id="fldSearch" style="width: 98%;">
                        <legend class="blbl">Search</legend>
                        <table border="0" height="40px" cellpadding="2" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td>
                                    <label id="lblDepotCode" runat="server" class="lbl">
                                        Depot Code</label>
                                    <Inp:iLabel ID="lblDepotCodeReq" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>
                                </td>
                                <td>
                                <div id="divLkpDepot">
                                    <Inp:iLookup ID="lkpDepotCode" runat="server" CssClass="lkp" DataKey="DPT_CD" DoSearch="True"
                                        OnClientTextChange="onDepotCodeChangeClearValues" iCase="Upper" TabIndex="1" ClientFilterFunction=""
                                        TableName="29" HelpText="442" Width="70px">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="DPT_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Depot Code" ColumnName="DPT_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Depot Name" ColumnName="DPT_NAM" Hidden="False" />
                                        </LookupColumns>
                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            LkpErrorMessage="Invalid Depot Code. Click on the list for valid values" ReqErrorMessage="Depot Code Required."
                                            Validate="True" ValidationGroup="tabInvoice" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iLookup>
                                    </div>
                                </td>
                            
                                <td>
                                    <label id="lblInvoiceType" runat="server" class="lbl">
                                        Invoice Type</label>
                                    <Inp:iLabel ID="CustomerCode" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>
                                </td>
                                <td>                                
                                    <Inp:iLookup ID="lkpInvoiceType" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                                        OnClientTextChange="onClientChangeInvoiceTypeClearValues" iCase="Upper" TabIndex="1" ClientFilterFunction="filterInvoiceGenerationType"
                                        TableName="63" HelpText="442" Width="70px">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Invoice Type" ColumnName="ENM_CD" Hidden="False" />
                                        </LookupColumns>
                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            LkpErrorMessage="Invalid Invoice Type. Click on the list for valid values" ReqErrorMessage="Invoice Type Required."
                                            Validate="True" ValidationGroup="tabInvoice" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iLookup>
                                </td>
                                <td>
                                    <label id="lblInvoicingParty" runat="server" class="lbl">
                                        Invoice To</label>
                                    <Inp:iLabel ID="ILabel1" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iLookup ID="lkpServicePartner" runat="server" CssClass="lkp" DataKey="SRVC_PRTNR_CD"
                                        DoSearch="True" iCase="Upper" TabIndex="2" TableName="59" HelpText="443" Width="60px"
                                        ClientFilterFunction="filterInvoiceType" OnClientTextChange="onClientTextChangeServicePartner">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="SRVC_PRTNR_ACTL_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="SRVC_PRTNR_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Name" ColumnName="SRVC_PRTNR_NAM" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Type" ColumnName="SRVC_PRTNR_TYP_CD" Hidden="False" />
                                        </LookupColumns>
                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            CustomValidation="false" CustomValidationFunction="" LkpErrorMessage="Invalid Invoice To. Click on the list for valid values"
                                            ReqErrorMessage="Invoice To Required." Validate="True" ValidationGroup="tabInvoice" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iLookup>
                                </td>
                                <td>
                                    <label id="lblPeriodFrom" runat="server" class="lbl">
                                        Period From</label>
                                    <Inp:iLabel ID="ILabel2" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>
                                </td>
                                <td>
                                    <div id="divlblFromDate" style="display: none; width: 80px">
                                        <label id="lblFromDate" runat="server" class="blbl" style="width: 150px" />
                                    </div>
                                    <div id="divdatFromDate">
                                        <Inp:iDate ID="datPeriodFrom" TabIndex="6" runat="server" HelpText="444" CssClass="dat"
                                            iCase="Upper" ReadOnly="false" Width="80px">
                                            <Validator CustomValidateEmptyText="True" IsRequired="False" Type="Date" ValidationGroup="tabInvoice"
                                                Operator="LessThanEqual" LkpErrorMessage="Invalid Period From. Click on the calendar icon for valid values"
                                                Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="Period From Required."
                                                CmpErrorMessage="Period From date Should not be greater than Current Date." RangeValidation="False"
                                                CustomValidation="true" CustomValidationFunction="validateFromDate" />
                                            <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        </Inp:iDate>
                                    </div>
                                </td>
                                <td>
                                    <label id="lblPeriodTo" runat="server" class="lbl">
                                        Period To</label>
                                    <Inp:iLabel ID="ILabel3" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>
                                </td>
                                <td>
                                  <div id="divLblToDate">
                                    <label id="lblToDate" runat="server" class="blbl" style="width: 150px" />
                                  </div>
                                   <div id="divDatToDate">
                                    <Inp:iDate ID="datPeriodTo" TabIndex="7" runat="server" HelpText="445" CssClass="dat"
                                        iCase="Upper" ReadOnly="false" Width="80px" LeftPosition="-90">
                                        <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="tabInvoice"
                                            Operator="LessThanEqual" LkpErrorMessage="Invalid Period To. Click on the calendar icon for valid values"
                                            Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="Period To Required."
                                            CmpErrorMessage="Period To date Should not be greater than Current Date." RangeValidation="False"
                                            CustomValidation="True" CustomValidationFunction="validateTodate" />
                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iDate>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                                <td colspan="3">
                                </td>
                                <td align="right">
                                    <ul style="margin: 0px;">
                                        <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnSubmit" style="text-decoration: none;
                                            color: White; font-weight: bold;" class="icon-search" runat="server" onclick="fetchInvoice();return false;">
                                            Fetch</a></li>
                                    </ul>
                                </td>
                                <td colspan="4" align="left">
                                    <ul style="margin: 0px;">
                                        <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnClear" style="text-decoration: none;
                                            color: White; font-weight: bold;" class="icon-eraser" runat="server" onclick="clearValues();return false;">
                                            Clear</a></li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divRecordNotFound" style="display: none; font-weight: bold;" align="center">
                        There are no matching details for Draft Invoice Generation for the specified Invoice
                        Type, Customer and Period.
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divHeader" style="display: none;width: 100%">
                        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="left">
                            <tr>
                                <td>
                                    <Inp:iLabel ID="lblInvoiceTypeName" runat="server" CssClass="blbl" Font-Underline="True">
                                    </Inp:iLabel>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="right">
                            <tr>
                                <td>
                                    <Inp:iLabel ID="ILabel19" runat="server" CssClass="blbl">Exchange Rate :  
                                    </Inp:iLabel>
                                    <Inp:iLabel ID="lblExrate" runat="server" CssClass="blbl"></Inp:iLabel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table  style="width: 100%;">
            <tr>
                <td>
                    <div id="divHandlingStorage" style="display: none;">
                        <iFg:iFlexGrid ID="ifgHandlingStorage" runat="server" AllowStaticHeader="True" DataKeyNames="HNDLNG_STRG_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="200px" Type="Normal"
                            ValidationGroup="divHandlingStorage" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="True" OnAfterCallBack="fnGridOnafterCB"
                            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser" PageIndex="1"
                            AllowAdd="False" UseActivitySpecificDatasource="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="80px" Wrap="True" />
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type "
                                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                    HeaderStyle-Width="50px" ReadOnly="true">
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10" TableName="3"
                                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                        SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
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
                                            ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divHandlingStorage" />
                                    </Lookup>
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="30px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:BoundField DataField="RFRNC_EIR_NO_1" HeaderText="Reference No" HeaderTitle="Reference No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="50px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="PRDCT_DSCRPTN_VC" HeaderText="Previous Cargo" HeaderTitle="Previous Cargo"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="150px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:DateField DataField="GTN_DT" HeaderText="In Date" HeaderTitle="In Date" SortAscUrl=""
                                    SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid In Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Event Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="divHandlingStorage" />
                                    </iDate>
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="60px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:DateField DataField="FROM_BILLING_DATE" HeaderText="From Date" HeaderTitle="From Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid From Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="" Validate="True" RangeValidation="false" CompareValidation="false"
                                            ValidationGroup="divHandlingStorage" />
                                    </iDate>
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="60px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:DateField DataField="TO_BILLING_DATE" HeaderText="To Date" HeaderTitle="To Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid To Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="" Validate="True" RangeValidation="false" CompareValidation="false"
                                            ValidationGroup="divHandlingStorage" />
                                    </iDate>
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="60px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="STORAGE_DAYS" HeaderText="STR Days" HeaderTitle="Storage Days"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="FR_DYS" HeaderText="Free Days" HeaderTitle="Free Days"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="CHARGEABLE_DAYS" HeaderText="Chrgbl Days" HeaderTitle="Chargeable Days"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="HNDIN_CHRG" HeaderText="HNDIN" HeaderTitle="Handling IN in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="HNDOUT_CHRG" HeaderText="HNDOUT" HeaderTitle="Handling OUT in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="STRG_CHRG" HeaderText="STR" HeaderTitle="Storage Charge in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="DPT_TTL_NC" HeaderText="Amount" HeaderTitle="Amount in Depot Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="60px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="TTL_CSTS_NC" HeaderText="Amount" HeaderTitle="Amount in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <ItemStyle Width="60px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            <ActionButtons>
                                <iFg:ActionButton ID="act_Print" Text="Generate Draft" ValidateRowSelection="False"
                                    OnClientClick="checkDraftExists();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />                               
                            </ActionButtons>
                        </iFg:iFlexGrid>
                    </div>
                    <div id="divRepair" style="display: none;">
                        <iFg:iFlexGrid ID="ifgRepair" runat="server" AllowStaticHeader="True" DataKeyNames="RPR_CHRG_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                            ValidationGroup="divRepair" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="false" OnAfterCallBack="fnGridOnafterCB"
                            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
                            AllowAdd="False" UseActivitySpecificDatasource="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="80px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type "
                                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                    HeaderStyle-Width="50px" ReadOnly="true">
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10" TableName="3"
                                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                        SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
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
                                            ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divHandlingStorage" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="30px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:DateField DataField="GTN_DT" HeaderText="In Date" HeaderTitle="In Date" SortAscUrl=""
                                    SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid In Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="In Date Required" Validate="True" RangeValidation="false" CompareValidation="false"
                                            ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="ESTMT_NO" HeaderText="Estimate No" HeaderTitle="Estimate No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="APPRVL_RFRNC_NO" HeaderText="Approval Ref No" HeaderTitle="Approval Reference No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:DateField DataField="RPR_APPRVL_DT" HeaderText="Approval Date" HeaderTitle="Approval Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Approval Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Approval Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="LEAK_TEST" HeaderText="Leak Test" HeaderTitle="Leak Test"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                 <iFg:BoundField DataField="ADDTNL_CLNNG_CHRG_NC" HeaderText="Addl Cleaning" HeaderTitle="Additional Cleaning"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="PTI" HeaderText="PTI" HeaderTitle="PTI" IsEditable="False"
                                    SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="SURVEY" HeaderText="Survey" HeaderTitle="Survey" IsEditable="False"
                                    SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                 <iFg:BoundField DataField="MTRL_CST_NC" HeaderText="Material Cost" HeaderTitle="Material Cost" IsEditable="False"
                                    SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                 <iFg:BoundField DataField="LBR_CST_NC" HeaderText="Man Hour Cost" HeaderTitle="Man Hour Cost" IsEditable="False"
                                    SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="REPAIRS" HeaderText="Repairs" HeaderTitle="Repairs" IsEditable="False"
                                    SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="DPT_TTL_NC" HeaderText="Amount" HeaderTitle="Amount in Depot Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="TTL_CSTS_NC" HeaderText="Amount" HeaderTitle="Amount in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:CheckBoxField DataField="CHECKED" HeaderText="Select"
                                    HeaderTitle="Select" HelpText="" SortAscUrl="" SortDescUrl="" ReadOnly="false">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="25px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:CheckBoxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            <ActionButtons>
                                <iFg:ActionButton ID="act_PrintRepairInvoice" Text="Generate Draft" ValidateRowSelection="False"
                                    OnClientClick="checkDraftExists();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                                <%--  <iFg:ActionButton ID="act_PrintRepairRegister" Text="Generate Report" ValidateRowSelection="False"
                                    OnClientClick="printInvoiceRegister();" IconClass="icon-print" CSSClass="btn btn-small btn-info" />--%>
                            </ActionButtons>
                        </iFg:iFlexGrid>
                    </div>
                    <div id="divCleaning" style="display: none;" >
                        <iFg:iFlexGrid ID="ifgCleaning" runat="server" AllowStaticHeader="True" DataKeyNames="CLNNG_CHRG_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                            ValidationGroup="divRepair" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="false" OnAfterCallBack="fnGridOnafterCB"
                            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
                            AllowAdd="False" UseActivitySpecificDatasource="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="100px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type "
                                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                    HeaderStyle-Width="50px" ReadOnly="true">
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10" TableName="3"
                                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                        SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
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
                                            ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divHandlingStorage" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:BoundField DataField="RFRNC_NO" HeaderText="Reference No" HeaderTitle="Reference No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:DateField DataField="GTN_DT" HeaderText="In Date" HeaderTitle="In Date" SortAscUrl=""
                                    SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid In Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="In Date Required" Validate="True" RangeValidation="false" CompareValidation="false"
                                            ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="PRDCT_DSCRPTN_VC" HeaderText="Previous Cargo" HeaderTitle="Previous Cargo"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:DateField DataField="ORGNL_CLNNG_DT" HeaderText="Cleaning Date" HeaderTitle="Cleaning Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Cleaning Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Cleaning Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:DateField DataField="ORGNL_INSPCTD_DT" HeaderText="Inspection Date" HeaderTitle="Inspection Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Inspection Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Inspection Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="DPT_TTL_NC" HeaderText="Amount" HeaderTitle="Amount in Depot Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="TTL_CSTS_NC" HeaderText="Amount" HeaderTitle="Amount in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:CheckBoxField DataField="CHECKED" HeaderText="Select"
                                    HeaderTitle="Select" HelpText="" SortAscUrl="" SortDescUrl="" ReadOnly="false">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:CheckBoxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            <ActionButtons>
                                <iFg:ActionButton ID="act_PrintCleaningInvoice" Text="Generate Draft" ValidateRowSelection="False"
                                    OnClientClick="checkDraftExists();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                                <%-- <iFg:ActionButton ID="act_PrintCleaningRegister" Text="Generate Report" ValidateRowSelection="False"
                                    OnClientClick="printInvoiceRegister();" IconClass="icon-print" CSSClass="btn btn-small btn-info" />--%>
                            </ActionButtons>
                        </iFg:iFlexGrid>
                    </div>
                    <div id="divMiscellaneous" style="display: none;">
                        <iFg:iFlexGrid ID="ifgMiscellaneous" runat="server" AllowStaticHeader="True" DataKeyNames="MSCLLNS_INVC_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                            ValidationGroup="divRepair" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="false" OnAfterCallBack="fnGridOnafterCB"
                            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
                            AllowAdd="False" UseActivitySpecificDatasource="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No/ No. of Equipment"
                                    HeaderTitle="Equipment No/ No. of Equipment" IsEditable="False" SortAscUrl=""
                                    SortDescUrl="">
                                    <ItemStyle Width="130px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type "
                                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                    HeaderStyle-Width="50px" ReadOnly="true">
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10" TableName="3"
                                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                        SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
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
                                            ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divHandlingStorage" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="60px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:DateField DataField="ACTVTY_DT" HeaderText="Activity Date" HeaderTitle="Activity Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Activity Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Activity Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="CHRG_DSCRPTN" HeaderText="Charge Description" HeaderTitle="Charge Description"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="DPT_TTL_NC" HeaderText="Amount" HeaderTitle="Amount in Depot Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="AMNT_NC" HeaderText="Amount" HeaderTitle="Amount in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:CheckBoxField DataField="CHECKED" HeaderText="Select"
                                    HeaderTitle="Select" HelpText="" SortAscUrl="" SortDescUrl="" ReadOnly="false">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="25px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:CheckBoxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            <ActionButtons>
                                <iFg:ActionButton ID="act_PrintMiscellaneousInvoice" Text="Generate Draft" ValidateRowSelection="False"
                                    OnClientClick="checkDraftExists();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                            </ActionButtons>
                        </iFg:iFlexGrid>
                    </div>

                     <div id="divCreditNote" style="display: none;">
                        <iFg:iFlexGrid ID="ifgCreditNote" runat="server" AllowStaticHeader="True" DataKeyNames="MSCLLNS_INVC_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                            ValidationGroup="divRepair" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="false" OnAfterCallBack="fnGridOnafterCB"
                            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
                            AllowAdd="False" UseActivitySpecificDatasource="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No/ No. of Equipment"
                                    HeaderTitle="Equipment No/ No. of Equipment" IsEditable="False" SortAscUrl=""
                                    SortDescUrl="">
                                    <ItemStyle Width="130px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type "
                                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                    HeaderStyle-Width="50px" ReadOnly="true">
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10" TableName="3"
                                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                        SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
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
                                            ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divHandlingStorage" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="60px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:DateField DataField="ACTVTY_DT" HeaderText="Activity Date" HeaderTitle="Activity Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Activity Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Activity Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="CHRG_DSCRPTN" HeaderText="Charge Description" HeaderTitle="Charge Description"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="DPT_TTL_NC" HeaderText="Amount" HeaderTitle="Amount in Depot Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="AMNT_NC" HeaderText="Amount" HeaderTitle="Amount in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:CheckBoxField DataField="CHECKED" HeaderText="Select"
                                    HeaderTitle="Select" HelpText="" SortAscUrl="" SortDescUrl="" ReadOnly="false">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="25px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:CheckBoxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            <ActionButtons>
                                <iFg:ActionButton ID="ActionButton2" Text="Generate Draft" ValidateRowSelection="False"
                                    OnClientClick="checkDraftExists();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                            </ActionButtons>
                        </iFg:iFlexGrid>
                    </div>

                    <div id="divHeating" style="display: none;">
                        <iFg:iFlexGrid ID="ifgHeating" runat="server" AllowStaticHeader="True" DataKeyNames="HTNG_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                            ValidationGroup="divHeating" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="false" OnAfterCallBack="fnGridOnafterCB"
                            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
                            AllowAdd="False" UseActivitySpecificDatasource="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="80px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type "
                                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                    HeaderStyle-Width="50px" ReadOnly="true">
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10" TableName="3"
                                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                        SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
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
                                            ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divHandlingStorage" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="30px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:BoundField DataField="PRDCT_DSCRPTN_VC" HeaderText="Previous Cargo" HeaderTitle="Previous Cargo"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="70px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="HTNG_TMPRTR" HeaderText="Temperature (°C)" HeaderTitle="Heating Temperature (°C)"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:DateField DataField="GTN_DT" HeaderText="In Date" HeaderTitle="In Date" SortAscUrl=""
                                    SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy }" HtmlEncode="false" ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid In Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="In Date Required" Validate="True" RangeValidation="false" CompareValidation="false"
                                            ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:DateField DataField="HTNG_STRT_DT_TM" HeaderText="Start Date Time" HeaderTitle="Start Date Time"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy HH:mm}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Heating Start Date Time Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Heating Start Date Time Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:DateField DataField="HTNG_END_DT_TM" HeaderText="End Date Time" HeaderTitle="End Date Time"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy HH:mm}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Heating End Date Time Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Heating End Date Time Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="TTL_HTN_PRD" HeaderText="Total Period" HeaderTitle="Total Period"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="60px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="DPT_TTL_NC" HeaderText="Amount" HeaderTitle="Amount in Depot Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="TTL_RT_NC" HeaderText="Amount" HeaderTitle="Amount in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:CheckBoxField DataField="CHECKED" HeaderText="Select"
                                    HeaderTitle="Select" HelpText="" SortAscUrl="" SortDescUrl="" ReadOnly="False">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="25px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:CheckBoxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            <ActionButtons>
                                <iFg:ActionButton ID="act_PrintHeatingInvoice" Text="Generate Draft" ValidateRowSelection="False"
                                    OnClientClick="checkDraftExists();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                            </ActionButtons>
                        </iFg:iFlexGrid>
                    </div>
                    <div id="divTransportation" style="display: none;">
                        <iFg:iFlexGrid ID="ifgTransportation" runat="server" AllowStaticHeader="True" DataKeyNames="TRNSPRTTN_CHRG_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                            ValidationGroup="divTransportation" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="false" OnAfterCallBack="fnGridOnafterCB"
                            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
                            AllowAdd="False" UseActivitySpecificDatasource="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="30px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type"
                                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                    HeaderStyle-Width="60px" ReadOnly="true" AllowSearch="true"  >
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10" TableName="3"
                                        CssClass="lkp" DoSearch="True" Width="60px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                                        SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC" AutoSearch="true"  >
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
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
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                                            ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divTransportation" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left" Width="60px"></HeaderStyle>
                                    <ItemStyle Width="60px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:LookupField DataField="EQPMNT_STT_CD" ForeignDataField="EQPMNT_STT_ID" HeaderText="Activity"
                                    HeaderTitle="Activity" PrimaryDataField="ENM_ID" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="60px"
                                    ReadOnly="true">
                                    <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="" iCase="Upper" OnClientTextChange=""
                                        ValidationGroup="divDetail" MaxLength="10" TableName="70" CssClass="lkp" DoSearch="True"
                                        Width="60px" ClientFilterFunction="" AllowSecondaryColumnSearch="false">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="ENM_CD" Hidden="False" />
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="20px" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            LkpErrorMessage="Invalid Activity. Click on the list for valid values"
                                            ReqErrorMessage="Activity Required" Validate="True" ValidationGroup="divTransportation" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left" Width="60px"></HeaderStyle>
                                    <ItemStyle Width="60px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:BoundField DataField="RQST_NO" HeaderText="Request No" HeaderTitle="Request No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="CSTMR_RF_NO" HeaderText="Customer Ref. No" HeaderTitle="Customer Ref. No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:LookupField DataField="RT_CD" ForeignDataField="RT_ID" HeaderText="Route"
                                    HeaderTitle="Route" PrimaryDataField="RT_ID" SortAscUrl="" SortDescUrl=""
                                    AllowSearch="true" IsEditable="False" ReadOnly="True" HeaderStyle-Width="60px" >
                                    <Lookup DataKey="RT_CD" DependentChildControls="" HelpText="" iCase="Upper" OnClientTextChange=""
                                        TableName="68" ValidationGroup="divTransportation" ID="lkpRoute" CssClass="lkp"
                                        DoSearch="True" Width="60px" ClientFilterFunction="" AutoSearch="true">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="RT_ID" Hidden="true" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="RT_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="RT_DSCRPTN_VC" Hidden="False" />
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="20px" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px"
                                            HorizontalAlign="Center" />
                                        <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Route Details. Click on the List for Valid Values"
                                            ReqErrorMessage="" Validate="True" IsRequired="false" CustomValidation="false"
                                            ValidationGroup="divTransportation" CustomValidationFunction="" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left" Width="60px"></HeaderStyle>
                                    <ItemStyle Width="60px" Wrap="true"  />
                                </iFg:LookupField>
                                <iFg:DateField DataField="EVNT_DT" HeaderText="Job End Date" HeaderTitle="Job End Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Job End Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Job End Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="60px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="TRP_RT_NC" HeaderText="Trip Amount" HeaderTitle="Trip Amount"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="WGHTMNT_AMNT_NC" HeaderText="Weighment" HeaderTitle="Weighment"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="TKN_AMNT_NC" HeaderText="Token" HeaderTitle="Token" IsEditable="False"
                                    SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="RCHRGBL_CST_AMNT_NC" HeaderText="Rechargable Cost" HeaderTitle="Rechargable Cost"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="FNNC_CHRG_AMNT_NC" HeaderText="Finance Charges" HeaderTitle="Finance Charges"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="DTNTN_AMNT_NC" HeaderText="Detention" HeaderTitle="Detention"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="OTHR_CHRG_AMNT_NC" HeaderText="Other Charges" HeaderTitle="Other Charges"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="DPT_AMNT" HeaderText="Amount" HeaderTitle="Amount in Depot Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="CSTMR_AMNT" HeaderText="Amount" HeaderTitle="Amount in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:CheckBoxField DataField="CHECKED" HeaderText="Select" HeaderTitle="Select"
                                    HelpText="" SortAscUrl="" SortDescUrl="" ReadOnly="False">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="25px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:CheckBoxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            <ActionButtons>
                                <iFg:ActionButton ID="ActionButton1" Text="Generate Draft" ValidateRowSelection="False"
                                    OnClientClick="checkDraftExists();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                            </ActionButtons>
                        </iFg:iFlexGrid>
                    </div>
                    <div id="divRental" style="display: none;">
                        <iFg:iFlexGrid ID="ifgRental" runat="server" AllowStaticHeader="True" DataKeyNames="RNTL_CHRG_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                            ValidationGroup="divRental" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="True" OnAfterCallBack="fnGridOnafterCB"
                            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
                            AllowAdd="False" UseActivitySpecificDatasource="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="80px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type "
                                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                    HeaderStyle-Width="70px" ReadOnly="true" AllowSearch="true"  >
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="divRental" MaxLength="10" TableName="3" AutoSearch="true"  
                                        CssClass="lkp" DoSearch="True" Width="70px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                        SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                                Hidden="False" />
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="20px" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                                            ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divRental" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left" Width="70px"></HeaderStyle>
                                    <ItemStyle Width="70px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:BoundField DataField="RFRNC_EIR_NO_1" HeaderText="Contract Ref No" HeaderTitle="Contract Ref No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="RFRNC_EIR_NO_2" HeaderText="PO Ref No" HeaderTitle="PO Ref No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" />
                                </iFg:BoundField>                              
                                <iFg:DateField DataField="FRM_BLLNG_DT" HeaderText="On-Hire Date" HeaderTitle="On-Hire Date" SortAscUrl=""
                                    SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid On-Hire Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Event Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="divRental" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:DateField DataField="TO_BLLNG_DT" HeaderText="Off-Hire Date" HeaderTitle="Off-Hire Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Off-Hire Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="" Validate="True" RangeValidation="false" CompareValidation="false"
                                            ValidationGroup="divRental" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                 <iFg:DateField DataField="FROM_BILLING_DATE" HeaderText="Rental From" HeaderTitle="Rental From"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Rental From. Click on the calendar icon for valid values"
                                            ReqErrorMessage="" Validate="True" RangeValidation="false" CompareValidation="false"
                                            ValidationGroup="divRental" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:DateField DataField="TO_BILLING_DATE" HeaderText="Rental To" HeaderTitle="Rental To"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Rental To. Click on the calendar icon for valid values"
                                            ReqErrorMessage="" Validate="True" RangeValidation="false" CompareValidation="false"
                                            ValidationGroup="divRental" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="RNTL_PR_DY_NC" HeaderText="Rental Per Day" HeaderTitle="Rental Per Day"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="RNTL_DYS" HeaderText="Rental Days" HeaderTitle="Rental Days"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="RNTL_CHRG_NC" HeaderText="Rental Charge" HeaderTitle="Rental Charge"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="" HtmlEncode="false" DataFormatString="{0:f2}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="ON_HR_SRVY_NC" HeaderText="On-Hire Survey" HeaderTitle="On-Hire Survey in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="40px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="HNDLNG_OT_NC" HeaderText="On-Hire HNDOUT" HeaderTitle="On-Hire HNDOUT in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="40px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                 <iFg:BoundField DataField="OFF_HR_SRVY_NC" HeaderText="Off-Hire Survey" HeaderTitle="Off-Hire Survey in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="40px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="HNDLNG_IN_NC" HeaderText="Off-Hire HNDIN" HeaderTitle="Off-Hire HNDIN in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="40px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="OTHR_CHRG_NC" HeaderText="Other Charges" HeaderTitle="Other Charges in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="40px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>                               
                                <iFg:BoundField DataField="TTL_CSTS_NC" HeaderText="Amount" HeaderTitle="Amount in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="40px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                 <iFg:BoundField DataField="DPT_TTL_NC" HeaderText="Amount" HeaderTitle="Amount in Depot Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="" HtmlEncode="false" DataFormatString="{0:f2}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="40px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            <ActionButtons>
                                <iFg:ActionButton ID="act_Rental" Text="Generate Draft" ValidateRowSelection="False"
                                    OnClientClick="checkDraftExists();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />                               
                            </ActionButtons>
                        </iFg:iFlexGrid>
                    </div>

                    <%--GWS--%>
                    <div id="divInspection" style="display: none;">
                        <iFg:iFlexGrid ID="ifgInspection" runat="server" AllowStaticHeader="True" DataKeyNames="INSPCTN_CHRG_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                            ValidationGroup="divRepair" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="false" OnAfterCallBack="fnGridOnafterCB"
                            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
                            AllowAdd="False" UseActivitySpecificDatasource="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="100px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type "
                                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                    HeaderStyle-Width="50px" ReadOnly="true">
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10" TableName="3"
                                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                        SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
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
                                            ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divHandlingStorage" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:BoundField DataField="EIR_NO" HeaderText="EIR No" HeaderTitle="EIR No" IsEditable="False"
                                    SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:DateField DataField="GTN_DT" HeaderText="In Date" HeaderTitle="In Date" SortAscUrl=""
                                    SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid In Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="In Date Required" Validate="True" RangeValidation="false" CompareValidation="false"
                                            ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:DateField DataField="INSPCTD_DT" HeaderText="Inspection Date" HeaderTitle="Inspection Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Cleaning Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Cleaning Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="DPT_TTL_NC" HeaderText="Amount" HeaderTitle="Amount in Depot Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="TTL_CSTS_NC" HeaderText="Amount" HeaderTitle="Amount in Customer Currency"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                                </iFg:BoundField>
                                <iFg:CheckBoxField DataField="CHECKED" HeaderText="Select" HeaderTitle="Select" HelpText=""
                                    SortAscUrl="" SortDescUrl="" ReadOnly="false">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:CheckBoxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            <ActionButtons>
                                <iFg:ActionButton ID="ActionButton3" Text="Generate Draft" ValidateRowSelection="False"
                                    OnClientClick="checkDraftExists();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                            </ActionButtons>
                        </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="center" style="width: 100%;">
            <tr>
                <td>
                    <div id="divFooter" style="display: none;">
                        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="right">
                            <tr>
                                <td align="right">
                                    <Inp:iLabel ID="ILabel4" runat="server" CssClass="blbl">TOTAL AMOUNT :  
                                    </Inp:iLabel>
                                    <Inp:iLabel ID="lblDepotAmount" runat="server" CssClass="blbl"></Inp:iLabel>
                                    <Inp:iLabel ID="lblDepotCurrency" runat="server" CssClass="blbl"></Inp:iLabel>
                                </td>
                                <td align="center">
                                    <Inp:iLabel ID="ILabel22" runat="server" CssClass="blbl"> |           
                                    </Inp:iLabel>
                                </td>
                                <td align="right">
                                    <Inp:iLabel ID="lblCustAmount" runat="server" CssClass="blbl"></Inp:iLabel>
                                    <Inp:iLabel ID="lblCustCurrency" runat="server" CssClass="blbl"></Inp:iLabel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnCustomerCurrencyID" runat="server" />
    <asp:HiddenField ID="hdnDepotCurrencyID" runat="server" />
    <asp:HiddenField ID="hdnExRate" runat="server" />
    <asp:HiddenField ID="hdnIsHeadQuarters" runat="server" />
    <asp:HiddenField ID="hdnWarningMsg" runat="server" />
    </form>
</body>
</html>
