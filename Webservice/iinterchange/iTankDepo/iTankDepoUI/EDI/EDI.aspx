<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EDI.aspx.vb" Inherits="EDI_EDI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 67px;
        }
        .style2
        {
            width: 135px;
        }
        .style3
        {
            width: 105px;
        }
        .style4
        {
            width: 87px;
        }
        .style5
        {
            width: 58px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">EDI >> EDI</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navEquipmentInfo" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="" id="tabEDI">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="center" style="width: 100%;">
            <tr>
                <td>
                    <table border="0" height="40px" style="width: 100%;">
                        <tr>
                            <td style="width: 85px">
                                <label id="lblCustomer" runat="server" class="lbl">
                                    * Customer</label>
                            </td>
                            <td style="width: 150px">
                                <Inp:iLookup ID="lkpCustomer" runat="server" CssClass="lkp" DataKey="CSTMR_CD" DoSearch="True"
                                    OnClientTextChange="" iCase="Upper" TabIndex="1" TableName="9" HelpText="487"
                                    Width="100px">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                        IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="150px" VerticalAlign="Top" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        LkpErrorMessage="Invalid Customer. Click on the list for valid values" ReqErrorMessage="Customer Required."
                                        Validate="True" ValidationGroup="tabInvoice" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                            <td style="width: 200px">
                                <fieldset class="lbl" id="fldFormat" style="width: 100%;">
                                    <legend class="blbl">EDI Format</legend>
                                    <table border="0" height="40px" cellpadding="2" cellspacing="2" style="width: 100%;">
                                        <tr>
                                            <td style="width: 20%">
                                                <%--   <asp:RadioButton ID="rbtnAnsii" runat="server"  Text="Party" />--%>
                                                <input type="radio" id="rboAnsii" name="radio" runat="server" /><label>ANSII</label>
                                            </td>
                                            <td style="width: 20%">
                                                <input type="radio" id="rboCodco" name="radio" runat="server" /><label>CODECO</label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <fieldset class="lbl" id="fldSearch" style="width: 430px;">
                                    <legend class="blbl">* Activity</legend>
                                    <table border="0" height="40px" cellpadding="2" cellspacing="2" style="width: 78%;">
                                        <tr>
                                            <%-- <td style="width:3px">--%>
                                            <td class="style5">
                                                <label id="lblGateIn" runat="server" class="lbl">
                                                    Gate In</label>
                                            </td>
                                            <%--   <td style="width: 1px">--%>
                                            <td class="style1">
                                                <asp:CheckBox ID="chkGateIn" runat="server" />
                                            </td>
                                            <%--<td style="width: 6px">--%>
                                            <td class="style2">
                                                <label id="lblRepairEstimate" runat="server" class="lbl">
                                                    Repair Estimate</label>
                                            </td>
                                            <%--   <td style="width: 1px">--%>
                                            <td class="style3">
                                                <asp:CheckBox ID="chkRepairEstimate" runat="server" />
                                            </td>
                                            <td class="style2">
                                                <label id="lblRepairComplete" runat="server" class="lbl">
                                                    Repair Complete</label>
                                            </td>
                                            <%--   <td style="width: 1px">--%>
                                            <td class="style3">
                                                <asp:CheckBox ID="chkRepairComplete" runat="server" />
                                            </td>
                                            <%--<td style="width: 6px">--%>
                                            <td class="style4">
                                                <label id="lblGateOut" runat="server" class="lbl">
                                                    Gate Out</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkGateOut" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                </td>
                <td style="width: 100px">
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td style="width: 100px">
                </td>
                <td>
                    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
                        Visible="false" />
                </td>
                <td class="btncorner" style="text-align: center; width: 20%;">
                    <a href="#" id="hlnkGenarate" onclick="submitPage();return false;" class="btn btn-small btn-success"
                        style="font-weight: bold" runat="server"><i class="icon-save"></i>&nbsp;Generate</a>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td>
                    <div id="divEDI" style="width: 100%;">
                        <iFg:iFlexGrid ID="ifgEdi" runat="server" AllowStaticHeader="True" DataKeyNames="EDI_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="310px" Type="Normal"
                            ValidationGroup="divEDI" UseCachedDataSource="True" AutoGenerateColumns="False"
                            OnBeforeClientRowCreated="" OnAfterClientRowCreated="" PageSize="25" AddRowsonCurrentPage="False"
                            AllowPaging="True" OnAfterCallBack="" AllowDelete="False" AllowRefresh="True"
                            AllowSearch="True" AutoSearch="True" UseIcons="true" SearchButtonIconClass="icon-search"
                            SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                            AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                            DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                            RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                            SearchCancelButtonCssClass="btn btn-small btn-danger" ClearButtonCssClass="btn btn-small btn-success"
                            ClearButtonIconClass="icon-eraser" AllowAdd="False" AllowSorting="True">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <%-- <iFg:BoundField DataField="CSTMR_CD" HeaderText="Customer" HeaderTitle="Customer"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="80px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>--%>
                                <iFg:LookupField DataField="CSTMR_CD" ForeignDataField="CSTMR_ID" HeaderText="Customer"
                                    HeaderTitle="Customer" PrimaryDataField="CSTMR_ID" SortAscUrl="" SortDescUrl=""
                                    AllowSearch="true" ReadOnly="true">
                                    <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="87,CUSTOMER_CSTMR_CD"
                                        iCase="Upper" OnClientTextChange="" ValidationGroup="divEdiSettings" MaxLength="15"
                                        TableName="9" CssClass="lkp" DoSearch="True" Width="110px" ClientFilterFunction=""
                                        AllowSecondaryColumnSearch="true" SecondaryColumnName="CSTMR_NAM" AutoSearch="true">
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
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            LkpErrorMessage="Invalid Customer. Click on the list for valid values" ReqErrorMessage="Customer Required"
                                            CustomValidation="true" CustomValidationFunction="validateCustomer" Validate="True" />
                                    </Lookup>
                                    <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:BoundField DataField="EDI_ACTVTY_NAM" HeaderText="Activity" HeaderTitle="Activity"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="80px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="FLE_FRMT" HeaderText="EDI Format" HeaderTitle="EDI Format"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="80px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:DateField DataField="GNRTD_DT_TM" HeaderText="Generated Date and Time" HeaderTitle="Generated Date and Time"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy HH:mm}" HtmlEncode="false"
                                    ReadOnly="true">
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
                                <iFg:BoundField DataField="RSND_BIT" HeaderText="Resend" HeaderTitle="Resend" IsEditable="False"
                                    SortAscUrl="" SortDescUrl="" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:BoundField>
                                <iFg:HyperLinkField DataTextField="ANC_FL_NM" HeaderText="File Name" NavigateUrl="#"
                                    ReadOnly="true">
                                    <ItemStyle Width="200px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:HyperLinkField>
                                <%--  <iFg:HyperLinkField DataTextField="CDC_FL_NM" HeaderText="CODCO FileName" NavigateUrl="#"
                                    ReadOnly="true" Visible ="true" >
                                    <ItemStyle Width="200px" Wrap="True" />
                                </iFg:HyperLinkField>--%>
                                <iFg:BoundField DataField="LST_SNT_BY" HeaderText="Last Sent By" HeaderTitle="Last Sent By"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="80px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
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
                                <iFg:ImageField HeaderTitle="Send" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/BulkEmail.png"
                                    HeaderText="Send" HeaderImageUrl="">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Center" />
                                </iFg:ImageField>
                            </Columns>
                        </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnMailMode" runat="server" />
    </form>
    <iframe id="fmDownloadFile" runat="server" frameborder="0" marginheight="0" marginwidth="0"
        name="fmDownloadFile" scrolling="no" width="0px" height="0px"></iframe>
</body>
</html>
