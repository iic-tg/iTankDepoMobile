<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IndexPattern.aspx.vb" Inherits="Admin_IndexPattern" %>

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
                    <span id="spnHeader" class="ctabh">Index Pattern</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navGateIn" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <table width="100%">
        <tr>
            <td>
                <div id="tabIndexPattern">
                    <table border="0" cellpadding="2" align="left" cellspacing="2" class="tblstd" align="center">
                        <tr>
                            <td>
                                <label id="lblScreenName" runat="server" class="lbl">
                                    Screen Name</label>
                                <Inp:iLabel ID="ILabel2" runat="server" CssClass="lblReq">
                   *
                                </Inp:iLabel>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpScreenName" runat="server" CssClass="lkp" DataKey="ACTVTY_NAM"
                                    DoSearch="True" iCase="Upper" TabIndex="1" TableName="91" HelpText="559" ClientFilterFunction="filterScreen">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnCaption="ID" ColumnName="ACTVTY_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Screen Name" ColumnName="ACTVTY_NAM" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Table Name" ColumnName="TBL_NAM" Hidden="True" />
                                    </LookupColumns>
                                    <LookupGrid CurrentPageIndex="0" PageSize="5" Width="300px" VerticalAlign="Top" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Screen. Click on the list for valid values"
                                        Operator="Equal" ReqErrorMessage="Screen Name Required" Type="String" Validate="True"
                                        ValidationGroup="tabIndexPattern" CustomValidation="true" CustomValidationFunction="ValidateScreen" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblNoOfDigits" runat="server" Text="No of Digits" CssClass="required"></asp:Label>
                            </td>
                            <td>
                                <div class="row">
                                    <Inp:iTextBox runat="server" CssClass="ntxt" iCase="Numeric" Text="5" ID="txtNoOfDigits"
                                        MaxLength="1" TabIndex="3" ToolTip="Enter No Of Digits" HelpText="540">
                                        <Validator IsRequired="true" Validate="true" Operator="Equal" ReqErrorMessage="No Of Digits Required"
                                            Type="Integer" RegexValidation="true" RegularExpression="^[1-6]*$" RegErrorMessage="No Of Digits Will Be 1 To 6"
                                            CustomValidation="true" />
                                    </Inp:iTextBox>
                                </div>
                            </td>
                            <td>
                            </td>
                            <td>
                                <label id="lblIndexPattern" runat="server" class="lbl">
                                    Index Pattern</label>
                            </td>
                            <td>
                                <Inp:iTextBox runat="server" CssClass="txt" iCase="None" ID="txtIndexPattern" HelpText=""
                                    TabIndex="4" ToolTip="Enter Index Pattern" Width="255px">
                                    <Validator IsRequired="false" Operator="Equal" Type="String" />
                                </Inp:iTextBox>
                            </td>
                           <td>
                            </td>
                   <td>
                                <label id="lblActive" runat="server" class="lbl">
                                    Active</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActivebit" runat="server" TabIndex="8" />
                            </td>
                        </tr>
                        <tr>
                               <td>
                                <asp:Label ID="lblResetBasis" runat="server" Text="Reset Basis" CssClass="required"></asp:Label>
                            </td>
                            <td>
                            <Inp:iLookup runat="server" DataKey="ENM_CD" DependentChildControls="" iCase="Upper"
                                    TableName="96" ToolTip="Choose Reset Basis" ValidationGroup="" ID="lkpResetBasis"
                                    CssClass="lkp" TabIndex="5" DoSearch="True" AllowSecondaryColumnSearch="false"
                                    SecondaryColumnName="" ClientFilterFunction="" HelpText="542">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnCaption="Id" ColumnName="ENM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Reset Basis" ColumnName="ENM_CD" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px"
                                        HeaderStyle-HorizontalAlign="Left" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="false"
                                        LkpErrorMessage="Invalid Reset Basis. Click on the List for Valid Values" ReqErrorMessage="Reset Basis Required"
                                        Validate="True" CustomValidation="false" CustomValidationFunction="" />
                                </Inp:iLookup>
                            </td>
                            <td>
                            </td>
                            <td>
                                <label id="lblSplitChar" runat="server" class="lbl">
                                    Split Character</label>
                            </td>
                            <td>
                                <Inp:iTextBox runat="server" CssClass="txt" iCase="None" ID="txtSplitChar" TabIndex="6"
                                    MaxLength="1" ToolTip="Enter Split Char" HelpText="560">
                                    <Validator Validate="true" Operator="Equal" Type="String" RegErrorMessage="Split Char should be /  or - or _"
                                        RegexValidation="true" RegularExpression="^[/_-]$" />
                                </Inp:iTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblIndexBasis" runat="server" Text="Index Basis"></asp:Label>
                            </td>
                            <td>
                                <Inp:iLookup runat="server" DataKey="PRMTR_NM" DependentChildControls="" iCase="Upper"
                                    TableName="97" ToolTip="Choose Index Basis" ValidationGroup="" ID="lkpIndexBasis"
                                    CssClass="lkp" TabIndex="7" DoSearch="True" AllowSecondaryColumnSearch="false"
                                    SecondaryColumnName="" ClientFilterFunction="ApplyFilterIndex" HelpText="544">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnCaption="PRMTR_ID" ColumnName="PRMTR_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Index Basis" ColumnName="PRMTR_NM" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px"
                                        HeaderStyle-HorizontalAlign="Left" />
                                </Inp:iLookup>
                            </td>
                            <td>
                            </td>
                            <td id="tdFetch">
                                <ul style="margin: 0px;">
                                    <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnFetch" style="text-decoration: none;
                                        color: White; font-weight: bold" class="icon-search" runat="server" onclick="FetchData();return false;"
                                        tabindex="4">&nbsp;Fetch</a></li>
                                </ul>
                            </td>
                            <td>
                            </td>
                            
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divIndexPatternDetail" style="height: 150px; width: 900px">
                    <fieldset class="lbl" id="fldSearch">
                        <legend class="blbl">Index Pattern Details</legend>
                        <table border="0" cellpadding="2" cellspacing="2" class="" style="width: 100%;">
                            <tr>
                                <td>
                                    <iFg:iFlexGrid ID="ifgIndexPattern" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="1000" AllowSearch="false"
                                        AutoSearch="True" AllowRefresh="false" StaticHeaderHeight="130px" ShowEmptyPager="True"
                                        Width="100%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True"
                                        RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
                                        EnableViewState="False" DeleteButtonText="Delete Row" GridLines="Both" HorizontalAlign="NotSet"
                                        PageSizerFormat="" Scrollbars="Auto" Type="Normal" ValidationGroup="divIndexPatternDetail"
                                        DataKeyNames="INDX_PTTRN_DTL_ID" OnAfterCallBack="onAfterCallBackIfgIndexPattern"
                                        OnBeforeCallBack="" OnAfterClientRowCreated="" AddRowsonCurrentPage="False" UseIcons="true"
                                        SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                                        AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                        DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                        RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                                        SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser" UseActivitySpecificDatasource="True">
                                        <PagerStyle CssClass="gpage" />
                                        <RowStyle CssClass="gitem" />
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                        <Columns>
                                            <iFg:LookupField DataField="PRMTR_NM" ForeignDataField="PRMTR_ID" HeaderText="Parameter*"
                                                HeaderTitle="Parameter" PrimaryDataField="PRMTR_ID" SortAscUrl="" SortDescUrl=""
                                                HeaderStyle-Width="50px" ReadOnly="false">
                                                <Lookup DataKey="PRMTR_NM" DependentChildControls="" HelpText="560" iCase="None"
                                                    OnClientTextChange="SetIndexPatternField" ValidationGroup="" MaxLength="10" TableName="90"
                                                    CssClass="lkp" DoSearch="True" AutoSearch="true" Width="120px" ClientFilterFunction=""
                                                    AllowSecondaryColumnSearch="false" SecondaryColumnName="">
                                                    <LookupColumns>
                                                        <Inp:LookupColumn ColumnCaption="ID" ColumnName="PRMTR_ID" Hidden="true" />
                                                        <Inp:LookupColumn ColumnCaption="Parameter Name" ColumnName="PRMTR_NM" Hidden="False" />
                                                        <Inp:LookupColumn ColumnCaption="Field Name" ColumnName="FLD_NM" Hidden="true" />
                                                    </LookupColumns>
                                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                                    <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="NotSet" Width="400px" />
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                        CustomValidation="True" CustomValidationFunction="ValidateParameter" LkpErrorMessage="Invalid Parameter Name. Click on the list for valid values"
                                                        ReqErrorMessage="Paramenter Required" Validate="True" ValidationGroup="tabIndexPattern" />
                                                </Lookup>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="120px" Wrap="True" />
                                            </iFg:LookupField>
                                            <iFg:TextboxField DataField="DFLT_VL" HeaderText="Default Value" HeaderTitle="Default Value"
                                                SortAscUrl="" SortDescUrl="" SortExpression="">
                                                <TextBox ID="txtDefaultValue" iCase="Upper" runat="server" ValidationGroup="" CssClass="txt"
                                                    HelpText="561" MaxLength="6">
                                                    <Validator CustomValidateEmptyText="true" Operator="Equal" Validate="true" Type="String"
                                                        CustomValidation="true" CustomValidationFunction="ValidateDefaultValue" RegularExpression="^[a-zA-Z0-9/-]+$"
                                                        RegexValidation="true" RegErrorMessage="Default Value allows only alphabets and numbers" />
                                                </TextBox>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="120px" />
                                            </iFg:TextboxField>
                                        </Columns>
                                        <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                                        <SelectedRowStyle CssClass="gsitem" />
                                        <AlternatingRowStyle CssClass="gaitem" />
                                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                    </iFg:iFlexGrid>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <sp:SubmitPane ID="PageSubmitPane" runat="server" tabindex="14" onclientsubmit="submitPage();"
                    onClientPrint="null" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnIndexPattern" runat="server" />
    <asp:HiddenField ID="hdnEditBit" runat="server" />
    <asp:HiddenField ID="hdnActiveBit" runat="server" />
    </form>
</body>
</html>
