<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CleaningInspection.aspx.vb"
    Inherits="Operations_CleaningInspection" %>

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
                    <span id="spnHeader" class="ctabh">Operations &gt;&gt; Cleaning Instruction</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navCleaningInstruction" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
        font-family: Arial; font-size: 8pt; display: none; width: 80%;" align="center">
        There are no matching Equipment available in iTankDepo for Cleaning Instruction
        Generation.
    </div>
    <!-- UIG Fix -->
    <div class="tabdisplayGatePass" id="divCleaningInstruction">
        <div class="topspace">
        </div>
        <iFg:iFlexGrid ID="ifgCleaningInstruction" runat="server" AllowStaticHeader="True"
            DataKeyNames="ACTVTY_STTS_ID" Width="100%" CaptionAlign="NotSet" GridLines="Both"
            HeaderRows="1" HorizontalAlign="Justify" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
            Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
            ValidationGroup="divCleaningInstruction" UseCachedDataSource="True" AutoGenerateColumns="False"
            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="True" OnAfterCallBack=""
            AllowDelete="False" AllowRefresh="True" AllowSearch="True" AutoSearch="True"
            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
            SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
            ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser"
            AllowAdd="False">
            <Columns>
                <iFg:LookupField DataField="CSTMR_CD" ForeignDataField="CSTMR_ID" HeaderText="Customer"
                    HeaderTitle="Customer" PrimaryDataField="CSTMR_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="" ReadOnly="True" IsEditable="False">
                    <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="" iCase="Upper" OnClientTextChange=""
                        TableName="9" ValidationGroup="divCleaningInstruction" ID="lkpCustomer" CssClass="lkp"
                        DoSearch="True" Width="150px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                        SecondaryColumnName="CSTMR_NAM">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="CSTMR_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Customer Name" ColumnName="CSTMR_NAM" Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="280px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Customer. Click on the List for Valid Values."
                            ReqErrorMessage="Customer Required" Validate="True" IsRequired="false" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="100px" />
                </iFg:LookupField>
                <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment Number"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False" ReadOnly="True"
                    IsEditable="False">
                    <TextBox ID="txtEquipmentNo" HelpText="" OnClientTextChange="" ValidationGroup="divCleaningInstruction"
                        CssClass="txt" MaxLength="11" iCase="Upper">
                        <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="Equipment No Required"
                            CustomValidationFunction="validateEquipmentno" CustomValidation="True" Type="String"
                            Validate="false" CsvErrorMessage="This Equipment No already Exist." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type"
                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="" ReadOnly="True" IsEditable="False">
                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="263" iCase="Upper"
                        OnClientTextChange="" TableName="3" ValidationGroup="divCleaningInstruction"
                        ID="lkpEquipmentCode" CssClass="lkp" DoSearch="True" Width="120px" ClientFilterFunction=""
                        MaxLength="3" AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="EQPMNT_TYP_ID" Hidden="true" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="150px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Equipment Type. Click on the List for Valid Values."
                            ReqErrorMessage="Equipment Type Required" Validate="false" IsRequired="True"
                            CustomValidation="false" CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="90px" Wrap="True" />
                </iFg:LookupField>
                <iFg:LookupField DataField="PRDCT_DSCRPTN_VC" ForeignDataField="PRDCT_ID" HeaderText="Previous Cargo"
                    HeaderTitle="Previous Cargo" PrimaryDataField="PRDCT_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="" ReadOnly="True" IsEditable="False">
                    <Lookup DataKey="PRDCT_DSCRPTN_VC" DependentChildControls="" HelpText="" iCase="Upper"
                        OnClientTextChange="" TableName="46" ValidationGroup="divCleaningInstruction"
                        ID="lkpPreviousCargo" CssClass="lkp" DoSearch="True" Width="120px" ClientFilterFunction=""
                        AllowSecondaryColumnSearch="true" SecondaryColumnName="PRDCT_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="PRDCT_ID" Hidden="true" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="PRDCT_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="PRDCT_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Previous Cargo. Click on the List for Valid Values."
                            ReqErrorMessage="" Validate="false" IsRequired="False" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="100px" />
                </iFg:LookupField>
                <iFg:DateField DataField="GTN_DT" HeaderText="In Date" HeaderTitle="In Date" SortAscUrl=""
                    SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ReadOnly="true">
                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                            LkpErrorMessage="Invalid Gate In Date. Click on the calendar icon for valid values"
                            ReqErrorMessage="Event Date Required" Validate="false" RangeValidation="false"
                            CompareValidation="false" ValidationGroup="divCleaningInstruction" />
                    </iDate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" Font-Bold="false" />
                </iFg:DateField>
                <iFg:LookupField DataField="EQPMNT_STTS_CD" ForeignDataField="EQPMNT_STTS_ID" HeaderText="Current Status"
                    HeaderTitle="Current Status" PrimaryDataField="EQPMNT_STTS_ID" SortAscUrl=""
                    SortDescUrl="" ReadOnly="True" IsEditable="False">
                    <Lookup DataKey="EQPMNT_STTS_CD" DependentChildControls="" HelpText="" iCase="Upper"
                        OnClientTextChange="" ValidationGroup="divCleaningInstruction" MaxLength="15"
                        TableName="12" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction=""
                        AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_STTS_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="EQPMNT_STTS_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_STTS_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_STTS_DSCRPTN_VC"
                                Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="true"
                            LkpErrorMessage="Invalid To Status. Click on the list for valid values" ValidationGroup="divCleaningInstruction"
                            ReqErrorMessage="To Status Required." Validate="false" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" Font-Bold="false" />
                </iFg:LookupField>
                <iFg:DateField DataField="ACTVTY_DT" HeaderText="Current Status Date" HeaderTitle="Current Status Date"
                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                    ReadOnly="true">
                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                            LkpErrorMessage="Invalid Current Status Date. Click on the calendar icon for valid values"
                            ValidationGroup="divCleaningInstruction" ReqErrorMessage="Current Status Required"
                            Validate="false" RangeValidation="false" CompareValidation="false" />
                    </iDate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" Font-Bold="false" />
                </iFg:DateField>
                <iFg:CheckBoxField DataField="CHECKED" HeaderText="Select" HeaderImageUrl="../Images/flrsel.gif"
                    HeaderTitle="Select" HelpText="" SortAscUrl="" SortDescUrl="" ReadOnly="false">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="25px" Wrap="True" HorizontalAlign="Left" />
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
            <ActionButtons>
                <iFg:ActionButton ID="act_PrintCleaningInstruction" Text="Generate Cleaning Instruction"
                    ValidateRowSelection="False" OnClientClick="GenerateCleaningInstructionReport();"
                    IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
            </ActionButtons>
        </iFg:iFlexGrid>
    </div>
   <%--<asp:HiddenField ID="hdnCleaningInspectionNo" runat="server" />--%>
     <%-- <asp:HiddenField ID="hdnGI_TRNSCTN_NO" runat="server" />
    <asp:HiddenField ID="hdnDepotId" runat="server" />
    <asp:HiddenField ID="hdnEquipmentNo" runat="server" />--%>
    </form>
</body>
</html>
