<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddDynamicReport.aspx.vb"
    Inherits="DynamicReports_AddDynamicReport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Report</title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="overflow: auto">
    <div>
        <div>
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh">Add Report</span>
                    </td>
                    <td align="right">
                        <nv:Navigation ID="navAddDynamicReports" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tabdisplayActivityConfigure" id="tabAddReport" style="height: 300px;">
            <br />
            <center>
                <table border="0" cellpadding="1" cellspacing="1" class="tblstd">
                    <tr>
                        <td>
                        </td>
                        <td align="left" style="height: 23px; width: 100px;">
                            <label id="lblReportName" runat="server" class="lbl">
                                Report Name
                            </label>
                            <Inp:iLabel ID="lblReportNameReq" runat="server" ToolTip="*" CssClass="lblReq">
                        *</Inp:iLabel>
                        </td>
                        <td align="left">
                            <Inp:iTextBox ID="txtReportName" runat="server" CssClass="txt" TabIndex="1" HelpText="459,ACTIVITY_ACTVTY_NAM"
                                MaxLength="50" TextMode="SingleLine" iCase="None" ToolTip="Report Name">
                                <Validator CustomValidateEmptyText="False" CsvErrorMessage="" CustomValidationFunction="validateAddReportName"
                                    CustomValidation="true" IsRequired="True" Operator="Equal" ReqErrorMessage="Report Name Required"
                                    Type="String" Validate="True" ValidationGroup="" RegexValidation="True" RegularExpression="^[a-zA-Z0-9 ]+$"
                                    RegErrorMessage="Only Alphabets and Numbers are allowed" />
                            </Inp:iTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left" style="height: 23px">
                            <label id="lblReportTitle" runat="server" class="lbl">
                                Report Title
                            </label>
                            <Inp:iLabel ID="lnlReportTitleReq" runat="server" ToolTip="*" CssClass="lblReq">
                        *</Inp:iLabel>
                        </td>
                        <td align="left">
                            <Inp:iTextBox ID="txtReportTitle" runat="server" CssClass="txt" HelpText="460,ACTIVITY_PG_TTL"
                                iCase="None" MaxLength="50" OnClientTextChange="" ValidationGroup="" ToolTip="Report Title"
                                TabIndex="2">
                                <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" RegexValidation="True"
                                    RegularExpression="^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$" RegErrorMessage="Only Alphabets And Numbers are Allowed"
                                    Validate="True" IsRequired="true" ReqErrorMessage="Report Title Required" />
                            </Inp:iTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left" style="height: 23px">
                            <label id="lblProcess" runat="server" class="lbl">
                                Process
                            </label>
                            <Inp:iLabel ID="lblProcessReq" runat="server" ToolTip="*" CssClass="lblReq">
                        *</Inp:iLabel>
                        </td>
                        <td align="left">
                            <div id="divtxtProcess" style="display: none">
                                <Inp:iTextBox ID="txtProcess" runat="server" CssClass="txt" HelpText="" iCase="None"
                                    MaxLength="50" OnClientTextChange="" ValidationGroup="" ToolTip="Process" TabIndex="3">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" RegexValidation="True"
                                        RegularExpression="^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$" RegErrorMessage="Only Alphabets And Numbers are Allowed"
                                        Validate="True" IsRequired="false" ReqErrorMessage="Process Required" CsvErrorMessage="Process Name Already Exists"
                                        CustomValidation="True" CustomValidationFunction="validateProcess" />
                                </Inp:iTextBox>
                            </div>
                            <div id="divlkpProcess">
                                <Inp:iLookup ID="lkpProcess" runat="server" CssClass="lkp" DataKey="PRCSS_NAM" DoSearch="True"
                                    iCase="Upper" TableName="77" TabIndex="3" HelpText="" ToolTip="Process" MaxLength="20">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="false"
                                        CustomValidation="True" CustomValidationFunction="valProcessbyRptName" Validate="True"
                                        ReqErrorMessage="Process Required" LkpErrorMessage="Invalid Type. Click on the list for valid values"
                                        ValidationGroup="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" Width="" OnImgClick="" />
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="PRCSS_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="PRCSS_NAM" Hidden="False" />
                                    </LookupColumns>
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </div>
                        </td>
                        <td style="width: 90px">
                            <span style="display: none;"><a id="lnkAddProcess" href="#" onclick="onAddProcessClick();return false;">
                                Add Process</a></span> <span style="display: none;"><a id="lnkCancel" href="#" onclick="onCancelClick();return false;">
                                    Cancel</a></span>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" style="height: 23px">
                            <label id="lblActive" runat="server" class="lbl">
                                Active
                            </label>
                        </td>
                        <td align="left" style="width: 150px">
                            <asp:CheckBox ID="chkActiveBit" runat="server" Checked="true" TabIndex="4" ToolTip="Active" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" style="height: 23px">
                            &nbsp;
                        </td>
                        <td align="left">
                            <a id="aFetchParameters" href="#" onclick="onParametersClick();return false;">Fetch
                                Parameters</a>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="1" cellspacing="1" class="tblstd">
                    <tr>
                        <td>
                            <div id="divifgParameters">
                                <iFg:iFlexGrid ID="ifgParameters" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="5" AllowSearch="True"
                                    AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="145px" ShowEmptyPager="True"
                                    Width="99%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True"
                                    RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
                                    EnableViewState="False" AddButtonText="" DeleteButtonText="Delete" GridLines="Both"
                                    HorizontalAlign="NotSet" PageSizerFormat="" Scrollbars="None" Type="Normal" OnBeforeCallBack=""
                                    Mode="Insert" DataKeyNames="PRMTR_ID" UseIcons="true" SearchButtonIconClass="icon-search"
                                    SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                                    AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                                    DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                                    RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                                    ClearButtonCssClass="icon-eraser" AllowAdd="False" AllowDelete="False">
                                    <Columns>
                                        <iFg:TextboxField CharacterLimit="0" DataField="PRMTR_NAM" HeaderText="Parameter Name"
                                            HeaderTitle="Parameter Name" SortAscUrl="" SortDescUrl="" SortExpression="PRMTR_NAM">
                                            <TextBox CssClass="txt" HelpText="" iCase="None" MaxLength="50" OnClientTextChange=""
                                                ValidationGroup="" ReadOnly="true">
                                                <Validator CustomValidateEmptyText="False" CsvErrorMessage="" CustomValidationFunction=""
                                                    CustomValidation="false" IsRequired="false" Operator="Equal" ReqErrorMessage=""
                                                    Type="String" Validate="false" ValidationGroup="" RegexValidation="false" RegularExpression="^[a-zA-Z0-9 ]+$"
                                                    RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                            </TextBox>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </iFg:TextboxField>
                                        <iFg:TextboxField CharacterLimit="0" DataField="PRMTR_TYP" HeaderText="Parameter Type"
                                            HeaderTitle="Type" SortAscUrl="" SortDescUrl="" SortExpression="PRMTR_TYP">
                                            <TextBox CssClass="txt" HelpText="" iCase="None" MaxLength="50" OnClientTextChange=""
                                                ValidationGroup="" ReadOnly="true">
                                                <Validator CustomValidateEmptyText="False" CsvErrorMessage="" CustomValidationFunction=""
                                                    CustomValidation="false" IsRequired="false" Operator="Equal" ReqErrorMessage=""
                                                    Type="String" Validate="false" ValidationGroup="" RegexValidation="false" RegularExpression="^[a-zA-Z0-9 ]+$"
                                                    RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                            </TextBox>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </iFg:TextboxField>
                                        <iFg:CheckBoxField DataField="PRMTR_OPT" HeaderText="Mandatory" HeaderTitle="Mandatory"
                                            HelpText="" SortAscUrl="" SortDescUrl="" SortExpression="">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="50px" />
                                        </iFg:CheckBoxField>
                                        <iFg:CheckBoxField DataField="PRMTR_DRPDWN" HeaderText="Dropdown" HeaderTitle="Dropdown"
                                            HelpText="" SortAscUrl="" SortDescUrl="" SortExpression="">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="50px" />
                                        </iFg:CheckBoxField>
                                        <iFg:LookupField DataField="PRMTR_DPNDNT" ForeignDataField="PRMTR_ID" HeaderText="Dependent"
                                            HeaderTitle="Dependent" PrimaryDataField="PRMTR_ID" SortAscUrl="" SortDescUrl=""
                                            HeaderStyle-Width="50px" ReadOnly="false">
                                            <Lookup DataKey="PRMTR_NAM" DependentChildControls="" HelpText="" iCase="none" OnClientTextChange=""
                                                ValidationGroup="" TableName="78" CssClass="lkp" DoSearch="True" Width="80px"
                                                ClientFilterFunction="applyParameterFilter" AllowSecondaryColumnSearch="false"
                                                SecondaryColumnName="">
                                                <LookupColumns>
                                                    <Inp:LookupColumn ColumnName="PRMTR_ID" Hidden="True" />
                                                    <Inp:LookupColumn ColumnCaption="Parameter Name" ColumnName="PRMTR_NAM" Hidden="False" />
                                                </LookupColumns>
                                                <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                    IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                    OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                                <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="NotSet" Width="350px" />
                                                <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="false"
                                                    CustomValidationFunction="" CustomValidation="false" LkpErrorMessage="Invalid Dependent. Click on the list for valid values"
                                                    ReqErrorMessage="Charge Required" Validate="false" ValidationGroup="" />
                                            </Lookup>
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="120px" Wrap="True" />
                                        </iFg:LookupField>
                                    </Columns>
                                    <RowStyle CssClass="gitem" />
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <PagerStyle CssClass="gpage" HorizontalAlign="Center" />
                                    <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                                    <SelectedRowStyle CssClass="gsitem" />
                                    <AlternatingRowStyle CssClass="gaitem" />
                                </iFg:iFlexGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
        onClientPrint="null" />
    <asp:HiddenField ID="hdnAddProcess" runat="server" />
    <asp:HiddenField ID="hdnFetchParameters" runat="server" />
    </form>
</body>
</html>
