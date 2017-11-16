<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tracking.aspx.vb" Inherits="Operations_Tracking" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tracking</title>
</head>
<body>
           <form id="form1" runat="server" autocomplete="off" style="height: 100%; overflow :auto ">
    <div>
        <div style ="width :995px">
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Tracking</span>
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
            </div> 
    </div>
    <div class="tabdisplay" id="tabTracking" style="height: 450px;">
        <table border="0" cellpadding="0" cellspacing="3" class="tblstd" style="width: 100%">
            <tr>
                <td>
                    <table border="0" cellpadding="5" align="left" cellspacing="0" class="tblstd" style="width: 100%">
                        <tr>
                            <td>
                                <label id="lblCustomer" runat="server" class="lbl">
                                    Customer</label>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpCustomer" runat="server" CssClass="lkp" DataKey="CSTMR_CD" DoSearch="True"
                                    iCase="Upper" MaxLength="255" TabIndex="1" TableName="9" HelpText="167,CUSTOMER_CSTMR_CD"
                                    ClientFilterFunction="applyDepoFilter" ToolTip="Customer Name" OnClientTextChange="">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="CSTMR_CD" ControlToBind="lkpCustomer" Hidden="False"
                                            ColumnCaption="Customer Code" />
                                        <Inp:LookupColumn ColumnName="CSTMR_NAM" ControlToBind="" Hidden="False" ColumnCaption="Customer Name" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                        IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="14" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                        LkpErrorMessage="Invalid Customer. Click on the list for valid values" Operator="NotEqual"
                                        ReqErrorMessage="" Type="String" Validate="true" ValidationGroup="tabTracking" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                            <td>
                                <label id="lblContainer" runat="server" class="lbl">
                                    Equipment No</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtContainerNo" runat="server" CssClass="txt" TabIndex="2" HelpText="168,TRACKING_EQPMNT_NO"
                                    TextMode="SingleLine" iCase="Upper" ReadOnly="false" Width="130px">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                        ValidationGroup="tabTracking" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                        IsRequired="false" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                            <td>
                                <label id="lblPickUpDate" runat="server" class="lbl">
                                    Pickup Date</label>
                            </td>
                            <td>
                                <Inp:iDate ID="datPickUpDate" TabIndex="3" runat="server" HelpText="169,TRACKING_TRNSMSSN_NO"
                                    CssClass="dat" MaxLength="15" iCase="Upper">
                                    <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="tabTracking"
                                        Operator="Equal" LkpErrorMessage="Invalid Pickup Date. Click on the calendar icon for valid values"
                                        Validate="True" CsvErrorMessage="" CustomValidationFunction="" CmpErrorMessage=""
                                        CompareValidation="False" />
                                    <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iDate>
                            </td>
                            <td>
                                <label id="lblReceivedDate" runat="server" class="lbl">
                                    Received Date</label>
                            </td>
                            <td>
                                <Inp:iDate ID="datReceivedDate" TabIndex="4" runat="server" HelpText="170,TRACKING_TRNSMSSN_NO"
                                    CssClass="dat" MaxLength="15" iCase="Upper">
                                    <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="tabTracking"
                                        Operator="Equal" LkpErrorMessage="Invalid Received Date. Click on the calendar icon for valid values"
                                        Validate="True" CsvErrorMessage="" CustomValidationFunction="" CmpErrorMessage=""
                                        CompareValidation="False" />
                                    <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iDate>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblTransmissionNo" runat="server" class="lbl">
                                    Transmission No</label>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtTransmissionNo" runat="server" CssClass="txt" TabIndex="5" HelpText="171,TRACKING_TRNSMSSN_NO"
                                    TextMode="SingleLine" iCase="Upper" ReadOnly="false" Width="130px">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                        ValidationGroup="tabTracking" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                                        IsRequired="false" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                                </Inp:iTextBox>
                            </td>
                            <td>
                                <label id="lblLessee" runat="server" class="lbl">
                                    Lessee Code</label>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpLessee" runat="server" CssClass="lkp" DataKey="LSS_CD" DoSearch="True"
                                    iCase="Upper" MaxLength="255" TabIndex="6" TableName="6" HelpText="172,LESSEE_LSS_CD"
                                    ClientFilterFunction="" ToolTip="Lessee Code" OnClientTextChange="">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="LSS_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="LSS_CD" ControlToBind="lkpLessee" Hidden="False" ColumnCaption="Code" />
                                        <Inp:LookupColumn ColumnName="LSS_DSCRPTN_VC" ControlToBind="" Hidden="False" ColumnCaption="Description" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                        IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                        LkpErrorMessage="Invalid Lessee Code. Click on the list for valid values" Operator="NotEqual"
                                        ReqErrorMessage="" Type="String" Validate="true" ValidationGroup="tabTracking" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                            <td>
                                <label id="lblActivity" runat="server" class="lbl">
                                    Activity</label>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpActivity" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                                    iCase="Upper" MaxLength="255" TabIndex="7" TableName="19" HelpText="173,ENUM_ENM_CD"
                                    ClientFilterFunction="" ToolTip="Activity" OnClientTextChange="">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="lkpActivity" Hidden="False"
                                            ColumnCaption="Activity" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                        IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                        LkpErrorMessage="Invalid Activity. Click on the list for valid values" Operator="NotEqual"
                                        ReqErrorMessage="" Type="String" Validate="true" ValidationGroup="tabTracking" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                            <td colspan="2" align="left">
                            <div class="button">
                                <button id="lnkSearch" class="btn" onmouseover="this.className='sbtn'" onmouseout="this.className='btn'"
                                    onclick="onSearchClick();return false;">
                                    Search</button>&nbsp;&nbsp;&nbsp;
                                <button id="lnkReset" class="btn" onmouseover="this.className='sbtn'" onmouseout="this.className='btn'"
                                    onclick="onResetClick();return false;">
                                    Reset</button>
                               </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <div style="margin-top: -5px; width: 100%; height: 20px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divMessage" runat="server" style="margin: 10px; font-style: italic; font-family: Arial;
                        font-size: 8pt; display: none; width: 100%;">
                        <Inp:iLabel ID="lblListRowCount" runat="server" Visible="true">No Records Found</Inp:iLabel>
                    </div>
                    <div id="divDetail" style="margin: 1px; width: 100%; height: 100%; vertical-align: middle">
                        <iFg:iFlexGrid ID="ifgTracking" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="12" AllowSearch="True"
                            AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="355px" ShowEmptyPager="True"
                            Width="100%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True"
                            RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
                            EnableViewState="False" AddButtonText="" DeleteButtonText="" GridLines="Both"
                            HorizontalAlign="NotSet" PageSizerFormat="None" Type="Normal" ValidationGroup="tabTracking"
                            OnAfterCallBack="" OnBeforeCallBack="" OnAfterClientRowCreated="" Mode="Insert"
                            DataKeyNames="TRCKNG_ID" Scrollbars="None">
                            <Columns>
                                <iFg:TextboxField CharacterLimit="0" DataField="TRNSMSSN_NO" HeaderText="Transmission No"
                                    HeaderTitle="Transmission No" SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression="TRNSMSSN_NO"
                                    HtmlEncode="true">
                                    <TextBox ID="txtTransmission" HelpText="59,CUSTOMER_RATE_STRG_CHRGS_PR_DY_NC" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="" CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <ItemStyle  Width="100px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="EQPMNT_NO" HeaderText="Equipment No"
                                    HeaderTitle="Container No" SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression="EQPMNT_NO"
                                    HtmlEncode="true">
                                    <TextBox ID="txtEquipment" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="True" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="LSS_CD" HeaderText="Lessee Code"
                                    HeaderTitle="Lessee Code" SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression="LSS_CD"
                                    HtmlEncode="true">
                                    <TextBox ID="txtLessee" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:HyperLinkField DataTextField="ACTVTY_NAM" Text="Activity Name" HeaderText="Activity Name"
                                    HeaderTitle="" MaxLength="0" SortAscUrl="" SortDescUrl="" IsEditable="False"
                                    ReadOnly="True" SortExpression="ACTVTY_NAM">
                                    <ItemStyle Width="150px" />
                                </iFg:HyperLinkField>
                                <iFg:TextboxField CharacterLimit="0" DataField="EQPMNT_STTS_CD" HeaderText="Status"
                                    HeaderTitle="Status" SortAscUrl="" SortDescUrl="" SortExpression="EQPMNT_STTS_CD"
                                    ReadOnly="true" HtmlEncode="true">
                                    <TextBox ID="txtEquipment_Status" HelpText="" iCase="Upper" OnClientTextChange=""
                                        ValidationGroup="" CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="PCKP_DT" HeaderText="Pickup Date"
                                    ReadOnly="true" HeaderTitle="" SortAscUrl="" SortDescUrl="" HtmlEncode="false"
                                    DataFormatString="{0:dd-MMM-yyyy}" SortExpression="PCKP_DT">
                                    <TextBox iCase="Upper" MaxLength="9" ValidationGroup="" OnClientTextChange="" ReadOnly="true"
                                        CssClass="txt" Width="100px">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="Date" />
                                    </TextBox>
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="RCVD_DT" HeaderText="Received Date"
                                    ReadOnly="true" HeaderTitle="" SortAscUrl="" SortDescUrl="" HtmlEncode="false"
                                    DataFormatString="{0:dd-MMM-yyyy}" SortExpression="RCVD_DT">
                                    <TextBox iCase="Upper" MaxLength="9" ValidationGroup="" OnClientTextChange="" ReadOnly="true"
                                        CssClass="txt" Width="100px">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="Date" />
                                    </TextBox>
                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:CheckBoxField DataField="BLK_MAIL_BT" HeaderText="Bulk Email" HeaderTitle="Bulk Email"
                                    HelpText="" SortAscUrl="" SortDescUrl="" SortExpression="">
                                    <ItemStyle Width="50px" />
                                </iFg:CheckBoxField>
                                <iFg:CheckBoxField DataField="RSND_BTN_BT" HeaderText="Resend" HeaderTitle="Resend"
                                    HelpText="" SortAscUrl="" SortDescUrl="" SortExpression="">
                                    <ItemStyle Width="40px" />
                                </iFg:CheckBoxField>
                            </Columns>
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="NotSet"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <PagerStyle CssClass="gpage" Height="8px" Font-Names="Arial" HorizontalAlign="Center" />
                        </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="btnSubmit" style="margin: 1px; width: 100%; height: 100%; vertical-align: middle">
                        <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <%-- <table id="tblSubmit" border="0" cellpadding="5" align="left" cellspacing="0" class="tblstd"
        align="center" style="width: 100%">
        <tr>
        <td>--%>
    <%-- </td></tr> 
       </table>--%>
    </form>
</body>
</html>
