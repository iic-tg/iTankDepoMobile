<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewXmlEdi.aspx.vb" Inherits="Billing_ViewXmlEdi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Tracking >> View Invoice EDI</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navExRate" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!--UIG Fix -->
    <div class="" id="tabViewXml" style="overflow-y: auto; overflow-x: auto; height: auto">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="center" style="width: 100%;">
            <tr>
                <td>
                    <fieldset class="lbl" id="fldSearch" style="width: 98%;">
                        <legend class="blbl">Search</legend>
                        <table border="0" height="40px" cellpadding="2" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td>
                                    <label id="lblInvoiceType" runat="server" class="lbl">
                                        Activity</label>
                                    <Inp:iLabel ID="CustomerCode" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iLookup ID="lkpInvoiceType" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                                        OnClientTextChange="onClientChangeInvoiceTypeClearValues" iCase="Upper" TabIndex="1"
                                        TableName="63" HelpText="549" Width="100px" ClientFilterFunction="filterActivity">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Activity" ColumnName="ENM_CD" Hidden="False" />
                                        </LookupColumns>
                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            LkpErrorMessage="Invalid Activity. Click on the list for valid values" ReqErrorMessage="Activity Required."
                                            Validate="True" ValidationGroup="tabViewXml" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iLookup>
                                </td>
                                <td>
                                    <label id="lblInvoicingParty" runat="server" class="lbl">
                                        Customer</label>
                                    <Inp:iLabel ID="ILabel1" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iLookup ID="lkpServicePartner" runat="server" CssClass="lkp" DataKey="CSTMR_CD"
                                        DoSearch="True" iCase="Upper" TabIndex="2" TableName="9" HelpText="29" Width="60px"
                                        ClientFilterFunction="filterCustomer" OnClientTextChange="">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                                            <%--<Inp:LookupColumn ColumnCaption="Type" ColumnName="SRVC_PRTNR_TYP_CD" Hidden="False" />--%>
                                        </LookupColumns>
                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            CustomValidation="false" CustomValidationFunction="" LkpErrorMessage="Invalid Customer. Click on the list for valid values"
                                            ReqErrorMessage="Customer Required." Validate="True" ValidationGroup="tabViewXml" />
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
                                    <Inp:iDate ID="datPeriodFrom" TabIndex="3" runat="server" HelpText="444" CssClass="dat"
                                        iCase="Upper" ReadOnly="false" Width="80px">
                                        <Validator CustomValidateEmptyText="True" IsRequired="true" Type="Date" ValidationGroup="tabInvoice"
                                            Operator="LessThanEqual" LkpErrorMessage="Invalid Period From. Click on the calendar icon for valid values"
                                            Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="Period From Required."
                                            CmpErrorMessage="Period From date Should not be greater than Current Date." RangeValidation="False"
                                            CustomValidation="true" CustomValidationFunction="" />
                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iDate>
                                </td>
                                <td>
                                    <label id="lblPeriodTo" runat="server" class="lbl">
                                        Period To</label>
                                    <Inp:iLabel ID="ILabel3" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iDate ID="datPeriodTo" TabIndex="4" runat="server" HelpText="445" CssClass="dat"
                                        iCase="Upper" ReadOnly="false" Width="80px" LeftPosition="-90">
                                        <Validator CustomValidateEmptyText="False" IsRequired="true" Type="Date" ValidationGroup="tabInvoice"
                                            Operator="LessThanEqual" LkpErrorMessage="Invalid Period To. Click on the calendar icon for valid values"
                                            Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="Period To Required."
                                            CmpErrorMessage="Period To date Should not be greater than Current Date." RangeValidation="False"
                                            CustomValidation="true" CustomValidationFunction="validateTodate" />
                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iDate>
                                </td>
                                <td>
                                    <label id="lblStatus" runat="server" class="lbl">
                                        Status</label>
                                    <%--<Inp:iLabel ID="ILabel5" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>--%>
                                </td>
                                <td>
                                    <Inp:iLookup ID="lkpStatus" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                                        OnClientTextChange="" iCase="Upper" TabIndex="5" TableName="84" HelpText="490"
                                        Width="100px">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Status" ColumnName="ENM_CD" Hidden="False" />
                                        </LookupColumns>
                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="false"
                                            LkpErrorMessage="Invalid Status. Click on the list for valid values" ReqErrorMessage="Status Required."
                                            Validate="True" ValidationGroup="tabViewXml" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iLookup>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                </td>
                                <td align="right">
                                    <ul style="margin: 0px;">
                                        <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnSubmit" style="text-decoration: none;
                                            color: White; font-weight: bold;" class="icon-search" runat="server" onclick="fetchXml();return false;">
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
            <%--<tr>
                <td>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <div id="divRecordNotFound" style="display: none; font-weight: bold;" align="center">
                        There are no matching details for View Xml EDI for the specified Activity , Customer
                        and Period.
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divHeader" style="display: none; width: 100%">
                        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="left">
                            <tr>
                                <td>
                                    <Inp:iLabel ID="lblInvoiceTypeName" runat="server" CssClass="blbl" Font-Underline="True">
                                    </Inp:iLabel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td>
                    <div id="divViewXml" style="display: block;">
                        <iFg:iFlexGrid ID="ifgViewXmlEDI" runat="server" AllowStaticHeader="True" DataKeyNames="INVC_EDI_HSTRY_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                            ValidationGroup="divTransportation" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="10" AddRowsonCurrentPage="False" AllowPaging="True" OnAfterCallBack=""
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
                                <iFg:BoundField DataField="CSTMR_CD" HeaderText="Customer" HeaderTitle="Customer"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="20px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="ACTVTY_NAM" HeaderText="Activity" HeaderTitle="Activity"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="20px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="INVC_NO" HeaderText="Invoice No" HeaderTitle="Invoice No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="25px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:DateField DataField="GNRTD_DT" HeaderText="Generated Date" HeaderTitle="Generated Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss tt}" HtmlEncode="false"
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
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:HyperLinkField HeaderTitle="Sent File Name" MaxLength="0" SortAscUrl="" SortDescUrl=""
                                    IsEditable="False" IsRequired="True" AllowFilter="False" AllowSearch="False"
                                    HeaderText="Sent File Name" ReadOnly="True" DataTextField="SNT_FL_NAM">
                                    <ItemStyle Width="20px" Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle" />
                                </iFg:HyperLinkField>
                                <iFg:HyperLinkField HeaderTitle="Received File Name" MaxLength="0" SortAscUrl=""
                                    SortDescUrl="" IsEditable="False" IsRequired="True" AllowFilter="False" AllowSearch="False"
                                    HeaderText="Received File Name" ReadOnly="True" DataTextField="RCVD_FL_NAM">
                                    <ItemStyle Width="20px" Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle" />
                                </iFg:HyperLinkField>
                                <iFg:DateField DataField="SNT_DT" HeaderText="Sent Date" HeaderTitle="Sent Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss tt}" HtmlEncode="false"
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
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="STTS" HeaderText="Status" HeaderTitle="Status" IsEditable="False"
                                    SortAscUrl="" SortDescUrl="" iCase="Upper">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="20px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:HyperLinkField HeaderTitle="Remarks" MaxLength="0" SortAscUrl="" SortDescUrl=""
                                    IsEditable="False" IsRequired="True" AllowFilter="False" AllowSearch="False"
                                    HeaderText="Remarks" ReadOnly="True" DataTextField="RMRKS_VC">
                                    <ItemStyle Width="30px" Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </iFg:HyperLinkField>
                                <iFg:HyperLinkField HeaderTitle="Error" MaxLength="0" SortAscUrl="" SortDescUrl=""
                                    IsEditable="False" IsRequired="True" AllowFilter="False" AllowSearch="False"
                                    HeaderText="Error" ReadOnly="True" DataTextField="View Error">
                                    <ItemStyle Width="60px" Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                </iFg:HyperLinkField>
                                <%--<iFg:BoundField DataField="ERRR_DSCRPTN" HeaderText="Error" HeaderTitle="Error" IsEditable="False"
                                    SortAscUrl="" SortDescUrl="">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:BoundField>--%>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            <ActionButtons>
                                <iFg:ActionButton ID="ActionButton1" Text="Resend" ValidateRowSelection="False" OnClientClick=""
                                    IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" Visible="false" />
                            </ActionButtons>
                        </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <a id="hiddenLinkViewInvoice" href="#" style="display: none" target="_blank" />
    </form>
</body>
</html>
