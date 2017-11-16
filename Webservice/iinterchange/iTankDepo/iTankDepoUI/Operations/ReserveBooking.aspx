<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReserveBooking.aspx.vb"
    Inherits="Operations_ReserveBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Operations >> Reserve Booking</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navEquipmentInfo" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="" id="tabUser" runat="server" align="center" style="overflow-y: auto;
        overflow-x: auto; height: auto">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width: 100%">
            <tr>
                <td>
                    <label id="lblCustomer" runat="server" class="lbl">
                        Customer</label>
                    <Inp:iLabel ID="lblCustomerReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpCustomer" runat="server" CssClass="lkp" DataKey="CSTMR_CD" DoSearch="True"
                        iCase="Upper" TabIndex="1" TableName="9" HelpText="87" Width="100px" ToolTip="Enter or Select Customer"
                        ClientFilterFunction="">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                            LkpErrorMessage="Invalid Customer Code. Click on the list for valid values" ReqErrorMessage="Customer Required."
                            Validate="True" ValidationGroup="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    <div id="divFetch">
                        <ul style="margin: 0px;">
                            <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnSearch" style="text-decoration: none;
                                color: White; font-weight: bold" class="icon-search" runat="server" onclick="onFetchClick(); return false;"
                                tabindex="2">&nbsp;Fetch</a></li>
                        </ul>
                    </div>
                </td>
                <td>
                    <%-- <a href ="#" disabled="disabled" >Upload</a>--%>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblbookingAuthNo" runat="server" class="lbl">
                        Booking Authorization No</label>
                    <Inp:iLabel ID="lblAuthNoReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtbookingAuthNo" runat="server" CssClass="txt" TabIndex="3" HelpText="608"
                        ToolTip="Enter the Booking Authorization No " TextMode="SingleLine" iCase="Upper"
                        ReadOnly="false" MaxLength="14">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            LookupValidation="False" CsvErrorMessage="" IsRequired="true" ReqErrorMessage="Booking Authorization No Required"
                            CustomValidation="False" RegexValidation="true" RegularExpression="^[a-zA-Z0-9]+$"
                            RegErrorMessage="Only alphabets and numbers allowed." />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblDateOfAcceptance" runat="server" class="lbl">
                        Date of Acceptance</label>
                    <Inp:iLabel ID="lblDateOfAccptReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iDate ID="datDateOfAcceptance" TabIndex="4" runat="server" HelpText="27" CssClass="dat"
                        ToolTip="Enter or Select Date of Acceptance" iCase="Upper" ReadOnly="false">
                        <Validator CustomValidateEmptyText="True" IsRequired="true" Type="Date" ValidationGroup=""
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Date of Acceptance. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CompareValidation="true" ReqErrorMessage="Date of Acceptance Required."
                            CmpErrorMessage="Date of Acceptance should not be greater than Current Date."
                            RangeValidation="False" CustomValidation="false" CustomValidationFunction="" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
                <td>
                    <label id="lblConsignee" runat="server" class="lbl">
                        Consignee</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtConsignee" runat="server" CssClass="txt" TabIndex="5" HelpText="596"
                        ToolTip="Eneter the Consignee" TextMode="SingleLine" iCase="None" ReadOnly="false"
                        MaxLength="50">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="false"
                            ReqErrorMessage="Consignee Required" CustomValidation="False" RegexValidation="true"
                            RegularExpression="^[a-zA-Z0-9]+$" RegErrorMessage="Only alphabets and numbers allowed."
                            SetFocusOnError="False" />
                    </Inp:iTextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="height: 30px">
    </div>
    <div class="" id="divdisplayGatePass" style="height: auto">
        <%-- Total Count Grid--%>
        <div id="divGridArea">
            <%--Equipment Grid--%>
            <table width="100%">
                <tr>
                    <td style="width: 25%" valign="top">
                        <fieldset style="width: 100%">
                            <legend class="ghdr" style="text-align: left; border-style: none;">Total No. of Equipments</legend>
                            <br />
                            <iFg:iFlexGrid ID="ifgBookingQuantity" runat="server" AllowStaticHeader="True" DataKeyNames="EQPMNT_CNT_ID"
                                Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="Auto" ShowEmptyPager="false" StaticHeaderHeight="50px" Type="Normal"
                                ValidationGroup="divGridArea" UseCachedDataSource="True" AutoGenerateColumns="False" EnableViewState="False"
                                OnBeforeClientRowCreated="" OnAfterClientRowCreated="" PageSize="1" AddRowsonCurrentPage="False"
                                AllowPaging="false" AllowAdd="false" AllowDelete="false" OnAfterCallBack="" AllowRefresh="false"
                                AllowEdit="true" AllowExport="false" AllowSearch="false" AutoSearch="True" UseIcons="True"
                                SearchButtonCssClass="btn btn-small btn-info" DeleteButtonCssClass="btn btn-small btn-danger"
                                RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonCssClass="btn btn-small btn-danger"
                                ClearButtonCssClass="btn btn-small btn-success" SearchButtonIconClass="icon-search"
                                AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                DeleteButtonIconClass="icon-trash" RefreshButtonIconClass="icon-refresh" SearchCancelButtonIconClass="icon-remove"
                                ShowFooter="false" ShowPageSizer="false" ShowRecordCount="false">
                                <Columns>
                                    <iFg:TextboxField CharacterLimit="0" DataField="GATEIN_COUNT" HeaderText="Gated In"
                                        HeaderTitle="Gated In" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                        <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup="divGridArea"
                                            ReadOnly="true">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false"
                                                IsRequired="true" ReqErrorMessage="Equipment Type Required." RegularExpression="^[a-zA-Z0-9]+$"
                                                RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="AVAILABLE_COUNT" HeaderText="Available"
                                        HeaderTitle="Available" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                        <TextBox CssClass="txt" ID="txtAvailableCount" HelpText="" iCase="Upper" OnClientTextChange=""
                                            ValidationGroup="divGridArea" ReadOnly="true">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false"
                                                IsRequired="true" ReqErrorMessage="Equipment Type Required." RegularExpression="^[a-zA-Z0-9]+$"
                                                RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="BOOKED_COUNT" HeaderText="Booked Quantity"
                                        HeaderTitle="Booked Quantity" SortAscUrl="" SortDescUrl="" ReadOnly="false">
                                        <TextBox CssClass="txt" HelpText="612," iCase="Numeric" OnClientTextChange="" ValidationGroup="divGridArea"
                                            ReadOnly="false">
                                            <Validator CustomValidateEmptyText="True" Type="Integer" Validate="True" IsRequired="False"
                                                ReqErrorMessage="Booked Quantity Required." RegularExpression="^[a-zA-Z0-9]+$"
                                                CustomValidation="True" CustomValidationFunction="ValidateBookedQuantity" RegexValidation="false"
                                                RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>
                                </Columns>
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                <PagerStyle CssClass="gpage" Height="18px" HorizontalAlign="Center" />
                                <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                                <SelectedRowStyle CssClass="gsitem" />
                                <AlternatingRowStyle CssClass="gaitem" />
                                <ActionButtons>
                                    <iFg:ActionButton ID="acnRefresh" Text="Refresh" ValidateRowSelection="False" OnClientClick="bindGrid();"
                                        IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                                </ActionButtons>
                            </iFg:iFlexGrid>
                            <%-- <legend class="ghdr" style="text-align: center; border-style: none;">Total No. of Equipments</legend>
                            <br />
                            <table>
                                <tr style="height: 30px">
                                    <td>
                                        <label id="lblGatedIn" runat="server" class="lbl">
                                            Gated In</label>
                                    </td>
                                    <td>
                                        <Inp:iTextBox ID="txtGatedIn" runat="server" CssClass="txt" TabIndex="3" HelpText=""
                                            TextMode="SingleLine" iCase="Upper" ReadOnly="True" MaxLength="14">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                                                LookupValidation="False" CsvErrorMessage="" IsRequired="False" ReqErrorMessage=" "
                                                CustomValidation="False" RegexValidation="False" RegularExpression="^[a-zA-Z0-9]+$"
                                                RegErrorMessage="Only alphabets and numbers allowed." />
                                        </Inp:iTextBox>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <td>
                                        <label id="lblAvailable" runat="server" class="lbl">
                                            Available</label>
                                    </td>
                                    <td>
                                        <Inp:iTextBox ID="txtAvailable" runat="server" CssClass="txt" TabIndex="3" HelpText=""
                                            TextMode="SingleLine" iCase="Upper" ReadOnly="True" MaxLength="14">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                LookupValidation="False" CsvErrorMessage="" IsRequired="False" ReqErrorMessage="Booking Authorization No Required"
                                                CustomValidation="False" RegexValidation="False" RegularExpression="^[a-zA-Z0-9]+$"
                                                RegErrorMessage="Only alphabets and numbers allowed." />
                                        </Inp:iTextBox>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <td>
                                        <label id="lblBookingQuantity" runat="server" class="lbl">
                                            Booking Quantitity</label>
                                    </td>
                                    <td>
                                        <Inp:iTextBox ID="txtBookingQuantity" runat="server" CssClass="txt" TabIndex="3"
                                            HelpText="" TextMode="SingleLine" iCase="Upper" ReadOnly="True" MaxLength="14">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                LookupValidation="False" CsvErrorMessage="" IsRequired="False" ReqErrorMessage="Booking Authorization No Required"
                                                CustomValidation="False" RegexValidation="False" RegularExpression="^[a-zA-Z0-9]+$"
                                                RegErrorMessage="Only alphabets and numbers allowed." />
                                        </Inp:iTextBox>
                                    </td>
                                </tr>
                            </table>--%>
                        </fieldset>
                    </td>
                    <td style="width: 3%">
                    </td>
                    <td>
                        <iFg:iFlexGrid ID="ifgEquipmentDetails" runat="server" AllowStaticHeader="True" DataKeyNames="ACTVTY_STTS_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="150px" Type="Normal"
                            ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="setDefaultValues"
                            PageSize="5" AddRowsonCurrentPage="False" AllowPaging="True" AllowAdd="false"
                            AllowDelete="false" OnAfterCallBack="onAfterCallBack" AllowRefresh="true" AllowEdit="true"
                            AllowExport="false" AllowSearch="True" AutoSearch="True" UseIcons="True" SearchButtonCssClass="btn btn-small btn-info"
                            DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonCssClass="btn btn-small btn-danger" ClearButtonCssClass="btn btn-small btn-success"
                            SearchButtonIconClass="icon-search" AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" RefreshButtonIconClass="icon-refresh" SearchCancelButtonIconClass="icon-remove">
                            <Columns>
                                <iFg:TextboxField CharacterLimit="0" DataField="EQPMNT_NO" HeaderText="Equipment No"
                                    HeaderTitle="Equipment No" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                    <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        ReadOnly="true">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false"
                                            IsRequired="true" ReqErrorMessage="Equipment Type Required." RegularExpression="^[a-zA-Z0-9]+$"
                                            RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="60px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="EQPMNT_TYP_CD" HeaderText="Type"
                                    HeaderTitle="Type" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                    <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        ReadOnly="true">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false"
                                            IsRequired="true" ReqErrorMessage="Equipment Type Required." RegularExpression="^[a-zA-Z0-9]+$"
                                            RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="60px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="EQPMNT_CD_CD" HeaderText="Code" HeaderTitle="Code"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                    <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        ReadOnly="true">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false"
                                            IsRequired="true" ReqErrorMessage="Equipment Type Required." RegularExpression="^[a-zA-Z0-9]+$"
                                            RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="60px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="STTS_CD" HeaderText="Status" HeaderTitle="Status"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                    <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        ReadOnly="true">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="false"
                                            IsRequired="true" ReqErrorMessage="Equipment Type Required." RegularExpression="^[a-zA-Z0-9]+$"
                                            RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="60px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:CheckBoxField DataField="SLCT" HeaderText="Select" HeaderTitle="Select" HelpText=""
                                    SortAscUrl="" SortDescUrl="" HeaderImageUrl="" ReadOnly="false">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Center" />
                                </iFg:CheckBoxField>
                            </Columns>
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <PagerStyle CssClass="gpage" Height="18px" HorizontalAlign="Center" />
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <%--  <ActionButtons>
                <iFg:ActionButton ID="acnRefresh" Text="Refresh" ValidateRowSelection="False" OnClientClick="Equipment_bindGrid();"
                    IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
            </ActionButtons>--%>
                        </iFg:iFlexGrid>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();"
        onclientprint="null" />
    <asp:HiddenField ID="hdnPageTitle" runat="server" />
    <asp:HiddenField ID="hdnID" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField ID="hdnAuthNoEdit" runat="server" />
    </form>
</body>
</html>
