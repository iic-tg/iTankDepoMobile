<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CleaningMethod.aspx.vb"
    Inherits="Masters_CleaningMethod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr class="ctab" style="width: 100%; height: 20px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Master >> Cleaning Method</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navCleaningMethod" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="tabCleaningMethod" style="overflow-y: auto; overflow-x: auto;height:auto">
        <table border="0" class="tblstd" cellpadding="1" cellspacing="2">
            <tr>
                <td colspan="11">
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblCleaningMethodCode" runat="server" class="lbl">
                        Cleaning Method
                    </label>
                    <Inp:iLabel ID="reqCleaningMethodCode" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtType" runat="server" CssClass="txt" TabIndex="1" HelpText="544,CLEANING_METHOD_CLNNG_MTHD_TYP_CD"
                        TextMode="SingleLine" iCase="Upper" ToolTip="Cleaning Method">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets are Allowed"
                            RegularExpression="^[a-zA-Z]+$" Type="String" Validate="True" ValidationGroup=""
                            LookupValidation="False" CsvErrorMessage="This Cleaning Method Already Exists." CustomValidationFunction="validateCleaningMethodType"
                            IsRequired="true" ReqErrorMessage="Cleaning Method Required" CustomValidation="true" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblCleaningMethodDescription" runat="server" class="lbl">
                        Description
                    </label>
                    <Inp:iLabel ID="reqCleaningMethodDescription" runat="server" ToolTip="*" CssClass="lblReq">
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtDescription" runat="server" TabIndex="2" CssClass="txt" ToolTip="Description"
                        HelpText="545,CLEANING_METHOD_CLNNG_MTHD_TYP_DSCRPTN_VC" Height="" TextMode="SingleLine"
                        Width="" MaxLength="500">
                        <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="False"
                            ControlToCompare="" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False"
                            ReqErrorMessage="Description Required" ValidationGroup="tabSupplier" Validate="True"
                            RegErrorMessage="" RegexValidation="false" RegularExpression=""></Validator>
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblActive" runat="server" class="lbl">
                        Active</label>
                </td>
                <td>
                    <asp:CheckBox ID="chkActive" runat="server" Text="" CssClass="chk" ToolTip="Active"
                        TabIndex="9" />
                </td>
            </tr>
            <tr>
                <td colspan="11">
                </td>
            </tr>
            <tr>
                <td colspan="11">
                    <asp:Label ID="lblGridCleaningMethodDetail" Font-Underline="true" Font-Bold="true"
                        runat="server" CssClass="lbl">
                    Cleaning Method Details
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="11">
                    <iFg:iFlexGrid ID="ifgCleaningMethodDetail" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="1000"
                        AllowSearch="false" AutoSearch="True" OnAfterCallBack="OnAfterCallBack" AllowRefresh="false"
                        StaticHeaderHeight="200px" ShowEmptyPager="True" Width="550px" UseCachedDataSource="True"
                        AllowSorting="True" AllowStaticHeader="True" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        HeaderRows="1" EnableViewState="False" DeleteButtonText="Delete Row" GridLines="Both"
                        HorizontalAlign="NotSet" PageSizerFormat="" Scrollbars="Auto" Type="Normal" ValidationGroup=""
                        DataKeyNames="CLNNG_MTHD_DTL_ID" OnBeforeCallBack="" OnAfterClientRowCreated=""
                        AddRowsonCurrentPage="False" UseIcons="true" SearchButtonIconClass="icon-search"
                        SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                        AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                        DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                        RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                        ClearButtonCssClass="icon-eraser">
                        <Columns>
                            <iFg:LookupField DataField="CLNNG_TYP_DSCRPTN_VC" ForeignDataField="CLNNG_TYP_ID"
                                HeaderText="Cleaning Type*" HeaderTitle="Cleaning Type" PrimaryDataField="CLNNG_TYP_ID"
                                SortAscUrl="" SortDescUrl="" HeaderStyle-Width="50px" ReadOnly="false">
                                <Lookup DataKey="CLNNG_TYP_DSCRPTN_VC" DependentChildControls="" HelpText="546,CLNNG_TYP_CD"
                                    iCase="Upper" OnClientTextChange="" ValidationGroup="" MaxLength="10" TableName="42"
                                    CssClass="lkp" DoSearch="True" Width="200px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                    SecondaryColumnName="" TabIndex="3">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="CLNNG_TYP_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="CLNNG_TYP_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="CLNNG_TYP_DSCRPTN_VC" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="Top" Width="400px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        CustomValidation="true" CustomValidationFunction="" LkpErrorMessage="Invalid Cleaning Type. Click on the list for valid values"
                                        ReqErrorMessage="Cleaning Type Required" Validate="True" ValidationGroup="tabCleaningMethod" />
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="180px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:TextboxField CharacterLimit="0" DataField="CMMNTS_VC" HeaderText="Comments"
                                HeaderTitle="Comments" SortAscUrl="" SortDescUrl="">
                                <TextBox CssClass="txt" HelpText="547,CLEANING_METHOD_DETAIL_CMMNTS_VC" iCase="None"
                                    OnClientTextChange="" ValidationGroup="tabCleaningMethod" MaxLength="255" TabIndex="4"
                                    Width="500px">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                        RegexValidation="False" LookupValidation="False" IsRequired="True" ReqErrorMessage="Comments Required" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="350px" Wrap="True" />
                            </iFg:TextboxField>
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
                </td>
            </tr>
        </table>
    </div>
    <table class="tblstd" style="margin: 0px auto;">
        <tr>
            <td>
                <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" align="center"
                    onClientPrint="null" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
