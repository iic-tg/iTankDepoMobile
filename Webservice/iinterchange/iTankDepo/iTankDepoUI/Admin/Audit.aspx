<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Audit.aspx.vb" Inherits="Admin_Audit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Admin >> Audit Log</span>
                </td>
             <%--    <td align="right">
                  <nv:Navigation ID="navAudit" runat="server" />
                </td>--%>
            </tr>
        </table>
    </div>
    <div class="tabdisplayGatePass" id="tabAudit" style="width: 100%; height: 100%;">
        <div class="topspace">
        </div>
        <!-- UIG Issue Fix (IE)- Issue No:3 Page alignment is not proper  -->
        <iFg:iFlexGrid ID="ifgAudit" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="25" AllowSearch="True"
            AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="300px" ShowEmptyPager="True"
            Width="100%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True"
            RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
            EnableViewState="False" AddButtonText="" DeleteButtonText="" GridLines="Both"
            HorizontalAlign="NotSet" PageSizerFormat="" Scrollbars="None" Type="Normal" ValidationGroup="tabAudit"
            DataKeyNames="RFRNC_NO" OnAfterCallBack="" OnBeforeCallBack="" OnAfterClientRowCreated=""
            AddRowsonCurrentPage="False" UseIcons="true" SearchButtonIconClass="icon-search"
            SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
            AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
            DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
            RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
             ClearButtonIconClass="icon-eraser" AllowDelete="False" AllowAdd="False" AllowEdit="False"
            SearchCancelButtonCssClass="btn btn-small btn-danger"  ClearButtonCssClass="btn btn-small btn-success">
            <Columns>
                <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No / Reference Code" HeaderTitle="Equipment No / Reference Code"
                    SortAscUrl="" SortDescUrl="" SortExpression="EQPMNT_NO" HtmlEncode="False">
                    <TextBox ID="txtEquipment No" HelpText="" OnClientTextChange="" ValidationGroup="tabAudit"
                        CssClass="txt" iCase="Upper" ReadOnly ="true" >
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="False" Type="String" Validate="true" CsvErrorMessage="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="ACTVTY_NAM" HeaderText="Activity Name" HeaderTitle="Activity Name"
                    SortAscUrl="" SortDescUrl="" SortExpression="ACTVTY_NAM" HtmlEncode="False">
                    <TextBox ID="txtAvtivity" HelpText="" OnClientTextChange="" ValidationGroup="tabAudit"
                        CssClass="txt" iCase="None" ReadOnly ="true" >
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="False" Type="String" Validate="true" CsvErrorMessage="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                 <iFg:TextboxField DataField="RFRNC_NO" HeaderText="Reference No" HeaderTitle="Reference No"
                    SortAscUrl="" SortDescUrl="" SortExpression="RFRNC_NO" HtmlEncode="False">
                    <TextBox ID="txtRefNo" HelpText="" OnClientTextChange="" ValidationGroup="tabAudit"
                        CssClass="txt" iCase="Upper" ReadOnly ="true" >
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="False" Type="String" Validate="true" CsvErrorMessage="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="ACTN_VC" HeaderText="Action" HeaderTitle="Action" SortAscUrl=""
                    SortDescUrl="" SortExpression="ACTN_VC" HtmlEncode="False">
                    <TextBox ID="txtAction" HelpText="" OnClientTextChange="" ValidationGroup="tabAudit"
                        CssClass="txt" iCase="None" ReadOnly ="true" >
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="False" Type="String" Validate="true" CsvErrorMessage="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="CNCLD_BY" HeaderText="Action By" HeaderTitle="Action By"
                    SortAscUrl="" SortDescUrl="" SortExpression="CNCLD_BY" HtmlEncode="False">
                    <TextBox ID="txtActionBy" HelpText="" OnClientTextChange="" ValidationGroup="tabAudit"
                        CssClass="txt" iCase="Upper" ReadOnly ="true" >
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="False" Type="String" Validate="true" CsvErrorMessage="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:DateField DataField="CNCLD_DT" HeaderText="Action Date and Time" HeaderTitle="Action Date and Time" SortAscUrl="" SortDescUrl=""  DataFormatString="{0:dd-MMM-yyyy hh:mm:ss:tt}"
                    HtmlEncode="false" AllowSearch="true" SortExpression="CNCLD_DT">
                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px" ReadOnly ="true" >
                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                            LkpErrorMessage="Invalid Action Date. Click on the calendar icon for valid values"
                            ReqErrorMessage="Action Date Required" Validate="True" CompareValidation="False" CustomValidation="False"
                            CmpErrorMessage="Action Date cannot be greater than current Date." CustomValidationFunction="" />
                    </iDate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="100px" Wrap="True" />
                </iFg:DateField>
                <iFg:TextboxField DataField="ADT_RMRKS" HeaderText="Reason" HeaderTitle="Reason"
                    SortAscUrl="" SortDescUrl="" SortExpression="ADT_RMRKS" HtmlEncode="False">
                    <TextBox ID="txtRemarks" OnClientTextChange="" ValidationGroup="tabAudit"
                        CssClass="txt" iCase="None" ReadOnly ="true" HelpText ="512,AUDIT_LOG_RSN_VC">
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="False" Type="String" Validate="true" CsvErrorMessage="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" Wrap="true"  />
                </iFg:TextboxField>
                <%-- <iFg:TextboxField DataField="HDR_NM" HeaderText="Header Name" HeaderTitle="Header Name"
                    SortAscUrl="" SortDescUrl="" SortExpression="HDR_NM" HtmlEncode="False">
                    <TextBox HelpText="" OnClientTextChange="" ValidationGroup="tabAudit"
                        CssClass="txt" iCase="None" ReadOnly ="true"  >
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="False" Type="String" Validate="true" CsvErrorMessage="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" Wrap="true"  />
                </iFg:TextboxField>--%>
                 <iFg:TextboxField DataField="OLD_VL" HeaderText="Old Value" HeaderTitle="Old Value"
                    SortAscUrl="" SortDescUrl="" SortExpression="OLD_VL" HtmlEncode="False">
                    <TextBox HelpText="" OnClientTextChange="" ValidationGroup="tabAudit"
                        CssClass="txt" iCase="None" ReadOnly ="true"  >
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="False" Type="String" Validate="true" CsvErrorMessage="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" Wrap="true"  />
                </iFg:TextboxField>
                 <iFg:TextboxField DataField="NEW_VL" HeaderText="New Value" HeaderTitle="New Value"
                    SortAscUrl="" SortDescUrl="" SortExpression="NEW_VL" HtmlEncode="False">
                    <TextBox  HelpText="" OnClientTextChange="" ValidationGroup="tabAudit"
                        CssClass="txt" iCase="None" ReadOnly ="true"  >
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="False" Type="String" Validate="true" CsvErrorMessage="" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" Wrap="true"  />
                </iFg:TextboxField>
            </Columns>
            <RowStyle CssClass="gitem" />
            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                OffsetX="" OffsetY="" OnImgClick="" Width="" />
            <PagerStyle CssClass="gpage" HorizontalAlign="Center" />
            <FooterStyle CssClass="gftr" HorizontalAlign="Left" Width ="100%" />
            <SelectedRowStyle CssClass="gsitem" />
            <AlternatingRowStyle CssClass="gaitem" />
        </iFg:iFlexGrid>
    </div>
    </form>
</body>
</html>
