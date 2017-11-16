<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PreAdvice.aspx.vb" Inherits="Operations_PreAdvice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<style type="text/css">
        .hide
        {
            display: none;
        }
        .show
       {
            display: block;
          width: 100px;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Operations >> Pre-Advice</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navEquipmentInfo" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Fix -->
    <div class="tabdisplayGatePass" id="divPreAdvice" style="overflow-y: auto; overflow-x: auto;
        height: auto">
        <div class="topspace">
        </div>
        <iFg:iFlexGrid ID="ifgPreAdvice" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="10" AllowSearch="True"
            AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="300px" ShowEmptyPager="True"
            Width="100%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True"
            RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
            EnableViewState="False" AddButtonText="Add Row" DeleteButtonText="Delete Row"
            GridLines="Both" HorizontalAlign="NotSet" PageSizerFormat="" Scrollbars="None"
            Type="Normal" ValidationGroup="divPreAdvice" DataKeyNames="PR_ADVC_ID" OnAfterCallBack="ifgPreAdviceOnAfterCB"
            OnBeforeCallBack="ifgPreAdviceOnBeforeCB" OnAfterClientRowCreated="setDefaultValues"
            Mode="Insert" AddRowsonCurrentPage="False" RowStyle-Wrap="True" UseIcons="true"
            SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
            SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser">
            <Columns>
                <iFg:LookupField DataField="CSTMR_CD" ForeignDataField="CSTMR_ID" HeaderText="Customer *"
                    HeaderTitle="Customer" PrimaryDataField="CSTMR_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="">
                    <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="265,CUSTOMER_CSTMR_CD"
                        TabIndex="1" iCase="Upper" OnClientTextChange="bindCustomerName" TableName="9"
                        ValidationGroup="divPreAdvice" ID="lkpCustomer" CssClass="lkp" DoSearch="True"
                        Width="150px" ClientFilterFunction="" AllowSecondaryColumnSearch="False" SecondaryColumnName="CSTMR_NAM">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="CSTMR_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Customer Name" ColumnName="CSTMR_NAM" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Check Digit" ColumnName="CHK_DGT_VLDTN_BT" Hidden="false" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="280px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Customer. Click on the List for Valid Values."
                            ReqErrorMessage="Customer Required" Validate="True" IsRequired="True" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="90px" />
                </iFg:LookupField>
                <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No *" HeaderTitle="Equipment No"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False">
                    <TextBox ID="txtEquipmentNo" HelpText="262,PRE_ADVICE_EQPMNT_NO" OnClientTextChange=""
                        ValidationGroup="divPreAdvice" CssClass="txt" MaxLength="11" iCase="Upper" TabIndex="2">
                        <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="Equipment No Required"
                            CustomValidationFunction="validateEquipmentno" CustomValidation="True" Type="String"
                            Validate="true" CsvErrorMessage="This Equipment No already Exist." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type *"
                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="">
                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="263" TabIndex="3"
                        iCase="Upper" OnClientTextChange="" TableName="3" ValidationGroup="divPreAdvice"
                        ID="lkpEquipmentCode" CssClass="lkp" DoSearch="True" Width="120px" ClientFilterFunction=""
                        MaxLength="3" AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="EQPMNT_TYP_ID" Hidden="true" />
                            <Inp:LookupColumn ColumnCaption="Type" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False"  />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Equipment Type. Click on the List for Valid Values."
                            ReqErrorMessage="Equipment Type Required" Validate="True" IsRequired="True" CustomValidation="true"
                            CustomValidationFunction="setEquipmentCode" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="90px" Wrap="True" />
                </iFg:LookupField>
                <%--gws--%>
                <iFg:LookupField DataField="EQPMNT_CD_CD" ForeignDataField="EQPMNT_CD_ID" HeaderText="Code *"
                    HeaderTitle="Code" PrimaryDataField="EQPMNT_CD_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="" ReadOnly="true" AllowSearch="true">
                    <Lookup DataKey="EQPMNT_CD_CD" DependentChildControls="" HelpText="263" TabIndex="3"
                        iCase="Upper" OnClientTextChange="" TableName="3" ValidationGroup="divPreAdvice"
                        ID="Lookup1" CssClass="lkp" DoSearch="True" Width="90px" ClientFilterFunction=""
                        MaxLength="3" AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="EQPMNT_CD_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_CD_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Equipment Type. Click on the List for Valid Values."
                            ReqErrorMessage="Equipment Type Required" Validate="True" IsRequired="True" CustomValidation="false"
                            CustomValidationFunction="setEquipmentCode" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="50px" Wrap="True" />
                </iFg:LookupField>
                <iFg:LookupField DataField="PRDCT_DSCRPTN_VC" ForeignDataField="PRDCT_ID" HeaderText="Previous Cargo"
                    HeaderTitle="Previous Cargo" PrimaryDataField="PRDCT_ID" SortAscUrl="" SortDescUrl=""
                    SortExpression="">
                    <Lookup DataKey="PRDCT_DSCRPTN_VC" DependentChildControls="" HelpText="266,PRODUCT_PRDCT_DSCRPTN_VC"
                        TabIndex="4" iCase="Upper" OnClientTextChange="" TableName="46" ValidationGroup="divPreAdvice"
                        ID="lklPreviousCargo" CssClass="lkp" DoSearch="True" Width="120px" ClientFilterFunction=""
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
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Previous Cargo. Click on the List for Valid Values."
                            ReqErrorMessage="" Validate="True" IsRequired="False" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="90px" />
                </iFg:LookupField>
                <iFg:TextboxField DataField="CLNNG_RFRNC_NO" HeaderText="Cleaning Ref No" HeaderTitle="Cleaning Reference No"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False">
                    <TextBox ID="txtCleaningReference" HelpText="267,PRE_ADVICE_CLNNG_RFRNC_NO" OnClientTextChange=""
                        TabIndex="5" ValidationGroup="divPreAdvice" CssClass="txt" MaxLength="16">
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="false" Type="String" Validate="true" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <%--GWS--%>
                <iFg:TextboxField DataField="AUTH_NO" HeaderText="Redelivery Auth No" HeaderTitle="Redelivery Auth No"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False">
                    <TextBox ID="TextBox1" HelpText="595" OnClientTextChange="" TabIndex="6" ValidationGroup="divPreAdvice"
                        CssClass="txt" MaxLength="14">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            RegexValidation="True" LookupValidation="False" IsRequired="False" ReqErrorMessage="Redel Auth No Required."
                            RegularExpression="^[a-zA-Z0-9]*$" RegErrorMessage="Only Alphabets And Numbers are Allowed" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="CNSGN_NM" HeaderText="Consignee" HeaderTitle="Consignee"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False">
                    <TextBox ID="TextBox2" HelpText="596" OnClientTextChange="" TabIndex="7" ValidationGroup="divPreAdvice"
                        CssClass="txt" MaxLength="255">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            RegexValidation="true" RegularExpression="^[a-zA-Z0-9'&]+$" LookupValidation="False"
                            IsRequired="False" ReqErrorMessage="" RegErrorMessage="Only Alphabets/Numbers and '& are allowed." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <%--<iFg:DateField DataField="ENTRD_DT" HeaderText="ETA Date *" HeaderTitle="ETA Date"
                    SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                    HtmlEncode="false" AllowSearch="true">
                    <iDate HelpText="389" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px" TabIndex="8">
                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <Validator CustomValidateEmptyText="True" Operator="LessThanEqual" Type="Date" IsRequired="True"
                            LkpErrorMessage="Invalid ETA Date. Click on the calendar icon for valid values"
                            ReqErrorMessage="ETA Date Required" Validate="True" CompareValidation="false"
                            CustomValidation="true" CmpErrorMessage="ETA Date cannot be greater than current Date."
                            CustomValidationFunction="ValidatePreAdviceDate" />
                    </iDate>
                    <HeaderStyle Width="120px" HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="120px" Wrap="True" />
                </iFg:DateField>--%>
                <iFg:DateField DataField="ENTRD_DT" HeaderText="ETA Date *" HeaderTitle="ETA Date"
                    SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                    HtmlEncode="false" AllowSearch="true">
                    <iDate HelpText="90" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <Validator CustomValidateEmptyText="True" Operator="LessThanEqual" Type="Date" IsRequired="false"
                            LkpErrorMessage="Invalid ETA Date. Click on the calendar icon for valid values"
                            ReqErrorMessage="ETA Date Required" Validate="True" CompareValidation="True"
                            CustomValidation="true" CmpErrorMessage="ETA Date cannot be greater than current Date."
                            CustomValidationFunction="ValidatePreAdviceDate" />
                    </iDate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="100px" Wrap="True" />
                </iFg:DateField>
                <iFg:TextboxField DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                    SortAscUrl="" SortDescUrl="" SortExpression="" HtmlEncode="False">
                    <TextBox ID="txtRemarks" HelpText="268" OnClientTextChange="" TabIndex="9" ValidationGroup="divPreAdvice"
                        CssClass="txt" MaxLength="256">
                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                            CustomValidation="false" Type="String" Validate="true" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <%--Added for Attchemnt--%>
                <iFg:ImageField HeaderTitle="Attach Files" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/attachment.png"
                    HeaderText="Attach Files" HeaderImageUrl="">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <%--   <ItemStyle  Wrap="True" HorizontalAlign="Center" />--%>
                    <%--   <ItemStyle Width="120px" HorizontalAlign="Left" />--%>
                    <ItemStyle Width="16px" Wrap="True" HorizontalAlign="Center" />
                </iFg:ImageField>
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
                    <iFg:ActionButton ID="act_Upload" Text="Upload" ValidateRowSelection="False" OnClientClick="openUpload();"
                        IconClass="icon-upload-alt" CSSClass="btn btn-small btn-info" />
                </ActionButtons>
        </iFg:iFlexGrid>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();"
        onclientprint="null" />
    <asp:HiddenField ID="hdnPageTitle" runat="server" />
    <asp:HiddenField ID="hdnchkdgtvalue" runat="server" />
    <asp:HiddenField ID="hdnCurrentDate" runat="server" />
    </form>
</body>
</html>
