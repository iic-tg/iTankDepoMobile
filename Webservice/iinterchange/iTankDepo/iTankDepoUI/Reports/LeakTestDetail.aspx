<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeakTestDetail.aspx.vb"
    Inherits="Reports_LeakTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >
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
                    <span id="spnHeader" class="ctabh">Reports >> Leak Test Detail</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navExRate" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="" id="tabInvoice" style="overflow-y: auto; overflow-x: auto; height: auto">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="center" style="width: 100%;">
            <tr>
                <td>
                    <fieldset class="lbl" id="fldSearch" style="width: 98%;">
                        <legend class="blbl">Search</legend>
                        <table border="0" height="40px" cellpadding="2" cellspacing="2" style="width: 100%;">
                            <tr>
                            <%--Multidepo--%>
                             <td>
                                    <label id="lblDepotCode" runat="server" class="lbl">
                                        Depot Code</label>
                                    <Inp:iLabel ID="lblDepotCodeReq" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>
                                </td>
                                  <td>
                                <div id="divLkpDepot">
                                    <Inp:iLookup ID="lkpDepotCode" runat="server" CssClass="lkp" DataKey="DPT_CD" DoSearch="True"
                                        OnClientTextChange="onDepotCodeChangeClearValues" iCase="Upper" TabIndex="1" ClientFilterFunction=""
                                        TableName="29" HelpText="442" Width="100px">
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
                                    <label id="lblCustomer" runat="server" class="lbl">
                                        Customer</label>
                                    <Inp:iLabel ID="CustomerCode" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iLookup ID="lkpCustomer" runat="server" CssClass="lkp" DataKey="CSTMR_CD" DoSearch="True"
                                        iCase="Upper" MaxLength="10" TabIndex="2" TableName="49" HelpText="313,CUSTOMER_CSTMR_CD"
                                        ClientFilterFunction="" ReadOnly="false" AllowSecondaryColumnSearch="true" SecondaryColumnName="CSTMR_NAM">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnName="CSTMR_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                            <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                                        </LookupColumns>
                                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                                            LkpErrorMessage="Invalid Customer. Click on the list for valid values" Operator="Equal"
                                            ReqErrorMessage="Customer Required" Type="String" Validate="True" ValidationGroup="tabChangeofStatus" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iLookup>
                                </td>
                                <td>
                                    <label id="lblInDateFrom" runat="server" class="lbl">
                                        In Date From</label>
                                    <%--<Inp:iLabel ID="ILabel1" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>--%>
                                    <Inp:iLabel ID="ILabel1" runat="server" ToolTip="*" CssClass="lbl"></Inp:iLabel>
                                </td>
                                <td>
                                    <div id="div2">
                                        <Inp:iDate ID="datInDateFrom" TabIndex="6" runat="server" HelpText="444" CssClass="dat"
                                            iCase="Upper" ReadOnly="false" Width="80px">
                                            <Validator CustomValidateEmptyText="True" IsRequired="False" Type="Date" ValidationGroup="tabInvoice"
                                                Operator="LessThanEqual" LkpErrorMessage="Invalid Period From. Click on the calendar icon for valid values"
                                                Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="In From Date Required."
                                                CmpErrorMessage="Period From date Should not be greater than Current Date." RangeValidation="False"
                                                CustomValidation="true" CustomValidationFunction="" />
                                            <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        </Inp:iDate>
                                    </div>
                                </td>
                                <td>
                                    <label id="lblInDateTo" runat="server" class="lbl">
                                        In Date To</label>
                                    <%--<Inp:iLabel ID="ILabel2" runat="server" ToolTip="*" CssClass="lbl"></Inp:iLabel>--%>
                                </td>
                                <td>
                                    <div id="divlblFromDate" style="display: none; width: 80px">
                                        <label id="lblFromDate" runat="server" class="blbl" style="width: 80px" />
                                    </div>
                                    <div id="divdatFromDate">
                                        <Inp:iDate ID="datInDateTo" TabIndex="6" runat="server" HelpText="444" CssClass="dat"
                                            iCase="Upper" ReadOnly="false" Width="80px">
                                            <Validator CustomValidateEmptyText="True" IsRequired="False" Type="Date" ValidationGroup="tabInvoice"
                                                Operator="LessThanEqual" LkpErrorMessage="Invalid Period From. Click on the calendar icon for valid values"
                                                Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="In To Date Required."
                                                CmpErrorMessage="Period From date Should not be greater than Current Date." RangeValidation="False"
                                                CustomValidation="true" CustomValidationFunction="validateInDateTo" />
                                            <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        </Inp:iDate></div>
                                </td>
                                <td>
                                    <label id="lblPeriodTo" runat="server" class="lbl">
                                        Test Date From</label>
                                    <%-- <Inp:iLabel ID="ILabel3" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>--%>
                                </td>
                                <td>
                                    <Inp:iDate ID="datTestDateFrom" TabIndex="7" runat="server" HelpText="445" CssClass="dat"
                                        iCase="Upper" ReadOnly="false" Width="80px" LeftPosition="-90">
                                        <Validator CustomValidateEmptyText="True" IsRequired="False" Type="Date" ValidationGroup="tabInvoice"
                                            Operator="LessThanEqual" LkpErrorMessage="Invalid Period To. Click on the calendar icon for valid values"
                                            Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="Test From Date Required."
                                            CmpErrorMessage="Period To date Should not be greater than Current Date." RangeValidation="False"
                                            CustomValidation="true" CustomValidationFunction="" />
                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iDate>
                                </td>
                                <td>
                                    <label id="Label1" runat="server" class="lbl">
                                        Test Date To</label>
                                    <%-- <Inp:iLabel ID="ILabel4" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>--%>
                                </td>
                                <td>
                                    <Inp:iDate ID="datTestDateTo" TabIndex="7" runat="server" HelpText="445" CssClass="dat"
                                        iCase="Upper" ReadOnly="false" Width="80px" LeftPosition="-90">
                                        <Validator CustomValidateEmptyText="True" IsRequired="False" Type="Date" ValidationGroup="tabInvoice"
                                            Operator="LessThanEqual" LkpErrorMessage="Invalid Period To. Click on the calendar icon for valid values"
                                            Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="Test To Date Required."
                                            CmpErrorMessage="Period To date Should not be greater than Current Date." RangeValidation="False"
                                            CustomValidation="true" CustomValidationFunction="validateTestDateTo" />
                                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    </Inp:iDate>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblEquipmentNo" runat="server" class="lbl">
                                        Equipment No</label>
                                    <Inp:iLabel ID="ILabel5" runat="server" CssClass="lblReq">
                   *
                                    </Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtEquipmentNo" runat="server" CssClass="txt" ToolTip="Equipment Number"
                                        HelpText="312,ACTIVITY_STATUS_EQPMNT_NO" iCase="Upper" TabIndex="3">
                                        <Validator IsRequired="False" ReqErrorMessage="Equipment Number Required" Validate="True"
                                            ValidationGroup="tabChangeofStatus" />
                                    </Inp:iTextBox>
                                </td>
                                <%--                                <td colspan="3">
                                </td>--%>
                                <td align="right">
                                    <ul style="margin: 0px;">
                                        <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnSubmit" style="text-decoration: none;
                                            color: White; font-weight: bold;" class="icon-search" runat="server" onclick="FetchLeakTest();return false;">
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
                    <div id="divRecordNotFound" style="display: none; font-weight: bold;" align="center">
                        There are no matching details for Leak test details for the specified Customer and
                        Period.
                    </div>
                </td>
            </tr>
        </table>
    </div>
 <%--   <div id="tabLeaktest" class="tabLeaktest" style="overflow-y: auto; overflow-x: auto;
        height: auto">--%>
           <div id="tabLeaktest" class="tabLeaktest" style="margin: 1px; width: 99.9%; vertical-align: middle;">
        <table border="0" cellpadding="2" cellspacing="2" class="" style="width: 100%">
            <tr>
                <td>
                    <div id="divLeaktest" style="display: none;">
                        <iFg:iFlexGrid ID="ifgLeakTest" runat="server" AllowStaticHeader="True" DataKeyNames="LK_TST_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="250px" Type="Normal"
                            ValidationGroup="divLeakTest" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            AllowAdd="false" PageSize="10" AddRowsonCurrentPage="False" AllowPaging="True"
                            OnAfterCallBack="" AllowDelete="false" AllowRefresh="True" AllowSearch="True"
                            AutoSearch="True" UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser" OnBeforeCallBack="">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No" SortExpression=""
                                    HeaderTitle="Equipment No" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                    <ItemStyle Width="40px" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="CSTMR_CD" HeaderText="Customer" SortExpression="" HeaderTitle="Customer Code"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                    <ItemStyle Width="40px" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="GTN_DT" HeaderText="In Date" SortExpression="" HeaderTitle="In Date"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                    <TextBox iCase="Upper">
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                    <ItemStyle Width="40px" />
                                </iFg:TextboxField>
                                <%--   <iFg:TextboxField DataField="CSTMR_CD" HeaderText="Customer" SortExpression="" HeaderTitle="Customer Code"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                    <ItemStyle Width="40px" />
                                </iFg:TextboxField>--%>
                                <iFg:TextboxField DataField="LT_TST_DT" HeaderText="Test Date" SortExpression=""
                                    HeaderTitle="Test Date" DataFormatString="{0:dd-MMM-yyyy}" SortAscUrl="" SortDescUrl=""
                                    ReadOnly="true">
                                    <TextBox iCase="Upper">
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                    <ItemStyle Width="40px" />
                                </iFg:TextboxField>
                                <iFg:CheckBoxField DataField="CHECKED" HeaderText="" HeaderTitle="Select" HelpText=""
                                    HeaderImageUrl="../Images/flrsel.gif" SortAscUrl="" SortDescUrl="" Visible="True">
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
    <div id="divGenerateDoc" style="display: block;">
        <table style="width: 100%;">
            <tr style="width: 100%;">
                <td class="btncorner" style="text-align: center; width: 17%;">
                    <a href="#" id="hlnkPrint" onclick="printDocument();return false;" class="btn btn-small btn-success"
                        style="font-weight: bold" runat="server"><i class="icon-save"></i>&nbsp;Generate
                        Document</a>
                </td>
            </tr>
        </table>
    </div>
      <asp:HiddenField ID="hdnHQID" runat="server" />
    </form>
</body>
</html>
