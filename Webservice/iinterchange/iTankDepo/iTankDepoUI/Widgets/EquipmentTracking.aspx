<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EquipmentTracking.aspx.vb"
    Inherits="Widgets_EquipmentTracking" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="3" class="tblstd" style="width: 100%">
            <tr>
                <td>
                    <label id="lblType" runat="server" class="lbl">
                        Type</label>
                </td>
                <td>
                    <Inp:iLookup ID="lkpType" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                        iCase="None" MaxLength="255" TabIndex="1" TableName="31" HelpText="" ClientFilterFunction=""
                        ToolTip="Type" OnClientTextChange="">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="lkpType" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnName="ENM_DSCRPTN_VC" Hidden="True" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="6" Width="150px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false" Operator="NotEqual"
                            ReqErrorMessage="" Type="String" Validate="False" ValidationGroup="tabTracking" LookupValidation="False" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    <label id="lblValue" runat="server" class="lbl">
                        Value</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtValue" runat="server" CssClass="txt" TabIndex="2" HelpText=""
                        TextMode="SingleLine" iCase="Upper" ReadOnly="false" Width="130px">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="tabTracking" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <a href="#" class="btn btn-tiny btn-primary" tabindex="3" id="lnkSearch" onclick="searchEquipment();return false;"><i class="icon-search"></i> Search</a>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" class="tblstd" style="width: 100%">
            <tr>
                <td>
                    <div id="divMessage" runat="server" style="margin: 10px; font-style: italic; font-family: Arial;
                        font-size: 8pt; display: none; width: 100%;">
                        <Inp:iLabel ID="lblListRowCount" runat="server" Visible="true">No Records Found</Inp:iLabel>
                    </div>
                    <div id="divDetail" style="margin: 1px; width: 100%; height: 100%; vertical-align: middle">
                        <iFg:iFlexGrid ID="ifgTracking" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="5" AllowSearch="False"
                            AutoSearch="True" AllowRefresh="False" StaticHeaderHeight="200px" ShowEmptyPager="True"
                            Width="100%" UseCachedDataSource="True" AllowSorting="False" AllowStaticHeader="True"
                            RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
                            EnableViewState="False" AddButtonText="" DeleteButtonText="" GridLines="Both"
                            HorizontalAlign="NotSet" PageSizerFormat="None" Type="Normal" ValidationGroup="tabTracking"
                            OnAfterCallBack="ifgTrackingOnAfterCB" OnBeforeCallBack="" OnAfterClientRowCreated="" Mode="Insert"
                            DataKeyNames="TRCKNG_ID" Scrollbars="None" AllowAdd="False" 
                            AllowDelete="False" ShowPageSizer="False" ShowRecordCount="False">
                            <Columns>
                                <iFg:HyperLinkField DataTextField="EQPMNT_NO" Text="Equipment No" HeaderText="Equipment No"
                                    HeaderTitle="" MaxLength="0" SortAscUrl="" SortDescUrl="" IsEditable="False"
                                    ReadOnly="True">
                                    <ItemStyle Width="30px" />
                                </iFg:HyperLinkField>
                                <iFg:BoundField DataField="CSTMR_CD" HeaderText="Customer" HeaderTitle="Customer"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="50px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:HyperLinkField DataTextField="ACTVTY_NAM" Text="Last Activity " HeaderText="Last Activity "
                                    HeaderTitle="" MaxLength="0" SortAscUrl="" SortDescUrl="" IsEditable="False"
                                    ReadOnly="True" SortExpression="ACTVTY_NAM">
                                    <ItemStyle Width="80px" />
                                </iFg:HyperLinkField>
                                <iFg:DateField DataField="EIR_DT" HeaderText="Last Activity Date" HeaderTitle="" SortAscUrl=""
                                    HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                                    HtmlEncode="false" ReadOnly="True">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px" ReadOnly="True">                                        
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />                                        

                                        <Validator CustomValidateEmptyText="False" Operator="NotEqual" Type="Date" IsRequired="False"
                                            Validate="False" CompareValidation="False" LookupValidation="False" />                                        
                                    </iDate>
                                    <ItemStyle Width="60px" Wrap="False" />
                                </iFg:DateField>                               
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
        </table>
    </div>
    </form>
</body>
</html>
