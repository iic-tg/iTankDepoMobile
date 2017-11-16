<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CustomerTariffCode.aspx.vb"
    Inherits="Masters_CustomerTariffCode" %>

<head id="Head1" runat="server">
    <title>Tariff</title>
</head>
<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 20px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Tariff</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navCustomerTariff" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Fix -->
    <div class="tabdisplayGatePass" id="tabProduct" style="overflow-y: auto; overflow-x: auto;
        height: auto">
        <table border="0" cellpadding="1" cellspacing="1" class="tblstd" style="width: 100%">
            <%-- <div class="" id="tabProduct" style="height: 350px;">
        <table border="0" cellpadding="1" cellspacing="1" class="tblstd">--%>
            <tr>
                <td>
                </td>
                <td style="width: 200px">
                    <label id="lblEQPMNT_TYP" runat="server" class="lbl">
                        Equipment Type
                    </label>
                    <Inp:iLabel ID="lblPEQPMNT_TYP1" runat="server" ToolTip="*" CssClass="lblReq">*</Inp:iLabel>
                </td>
                <td style="width: 300px">
                    <Inp:iLookup ID="lkpEqpType" runat="server" CssClass="lkp" DataKey="EQPMNT_TYP_CD"
                        ToolTip="Equipment Type" DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="1"
                        TableName="3" HelpText="24" ClientFilterFunction=""
                        ReadOnly="false">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_CD" Hidden="False" ColumnCaption="Type" />
                            <Inp:LookupColumn ColumnName="EQPMNT_CD_CD" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top"
                            HorizontalAlign="Left" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="TRUE" LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Equipment Type Required" Type="String" Validate="true"
                            ValidationGroup="" CustomValidationFunction="" CustomValidation="FALSE" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td></td>
                <td style="width: 200px">
                    <label id="lblTRRF_TYP" runat="server" class="lbl">
                        Tariff Type
                    </label>
                    <label id="lblTRRF_TYP1" runat="server" class="lblReq">
                        *
                    </label>
                </td>
                <td style="width: 300px">
                    <Inp:iLookup ID="lkpTRRF_TYP" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                        iCase="None" TabIndex="2" TableName="94" HelpText="607" ClientFilterFunction=""
                        ToolTip="Tariff Type" OnClientTextChange="DisableControls" onblur="DisableControls"
                        Width="100px" Visible="true">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="lkpTRRF_TYP" Hidden="False"
                                ColumnCaption="Tariff Type" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="TRUE" LkpErrorMessage="Invalid Tariff Type. Click on the list for valid values"
                            Operator="NotEqual" ReqErrorMessage="Tariff Type Required" Type="String" Validate="true" CustomValidation ="true" CustomValidationFunction ="DisableControls"
                            ValidationGroup="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td></td>
                <td style="width: 350px">
                    <div id="divLblCstmr">
                        <label id="lblCSTMR_NM" runat="server" class="lbl">
                            Customer Name/Agent Name
                        </label>
                        <label id="lblCSTMR_NM1" runat="server" class="lblReq">
                            *
                        </label>
                    </div>
                </td>
                <td style="width: 300px">
                    <div id="divLkpCstmr">
                        <Inp:iLookup ID="lkpCSTMR" runat="server" CssClass="lkp" DataKey="CSTMR_CD" DoSearch="True"
                            iCase="Upper" MaxLength="50" TabIndex="3" TableName="9" HelpText="487,CUSTOMER_CSTMR_CD"
                            ClientFilterFunction="" ToolTip="Customer Name" OnClientTextChange="">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="CSTMR_CD" Hidden="False" ColumnCaption="Customer Code" />
                                <Inp:LookupColumn ColumnName="CSTMR_NAM" Hidden="False" ColumnCaption="Customer Name" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="14" Width="300px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Customer Code. Click on the list for valid values"
                                Operator="Equal" ReqErrorMessage="Customer Required" Type="String" Validate="True"
                                ValidationGroup="" CustomValidationFunction="" CustomValidation="FALSE" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </div>
                    <div id="divLkpAgnt">
                        <Inp:iLookup ID="lkpAgent" runat="server" AllowSecondaryColumnSearch="false" ClientFilterFunction=""
                            CssClass="lkp" DataKey="AGNT_CD" DoSearch="True" HelpText="588,AGENT_AGNT_CD"
                            iCase="Upper" MaxLength="50" SecondaryColumnName="" TabIndex="4" TableName="93"
                            ToolTip="Agent">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="AGNT_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnCaption="Agent Code" ColumnName="AGNT_CD" ControlToBind="lkpAgent"
                                    Hidden="False" />
                                <Inp:LookupColumn ColumnCaption="Agent Name" ColumnName="AGNT_NAM" Hidden="False" />
                            </LookupColumns>
                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="Top" Width="300px" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False" LkpErrorMessage="Invalid Agent Code. Click on the list for valid values"
                                Operator="Equal" ReqErrorMessage="Agent Required" Type="String" Validate="True"
                                ValidationGroup="" CustomValidationFunction="" CustomValidation="FALSE" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </div>
                </td>
                <td></td>
                <td style="width: 200px">
                    <Inp:iLabel ID="lblActive" runat="server" CssClass="lbl" ToolTip="Active Bit">Active</Inp:iLabel>
                </td>
                <td style="width: 300px">
                    <asp:CheckBox ID="chkActiveBit" runat="server" Text="" CssClass="chk" ToolTip="Active Bit"
                        TabIndex="5" Checked="true"   />
                </td>
               <%-- <td>
                </td>
                <td>
                </td>--%>
            </tr>
            <tr>
            <td>&nbsp;</td>
            
            </tr>
            <tr>
                <td colspan="16">
                    <table style="width: 300px;">
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: small">
                          <b>(Note : Tariff Detail can be only uploaded after the Tariff Header is created.) </b>  
                                <div id="divifgTariffCode" style="margin: 1px; width: 1000px;">
                                    <iFg:iFlexGrid ID="ifgTariffType" runat="server" AllowStaticHeader="true" DataKeyNames="TRFF_CD_DTL_ID"
                                        Width="1745px" Height="" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1"
                                        AllowPaging="false" HorizontalAlign="NotSet" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                        Scrollbars="None" ShowEmptyPager="true" StaticHeaderHeight="150px" Type="Normal"
                                        ValidationGroup="divTariffType" UseCachedDataSource="True" AutoGenerateColumns="False"
                                        EnableViewState="False" PageSize="15" CssClass="tblstd" ShowPageSizer="False"
                                        UseIcons="true" SearchButtonIconClass="icon-search" AllowAdd="true" SearchButtonCssClass="btn btn-small btn-info"
                                        AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                        DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                        RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                                        SearchCancelButtonIconClass="icon-remove" 
                                        ClearButtonCssClass="icon-eraser" AllowRefresh="True"
                                        AllowSearch="True" FooterStyle-HorizontalAlign="Left" 
                                        EditRowStyle-HorizontalAlign="Left" 
                                        onafterclientrowcreated="setDefaultValues" AllowExport="True" 
                                        ExportButtonCssClass="btn btn-small btn-info" TabIndex="7">
                                        <RowStyle CssClass="gitem" />
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                        <Columns>
                                            <iFg:TextboxField CharacterLimit="0" DataField="TRFF_CD_DTL_CD" HeaderText="Code *"
                                                HeaderTitle="Code" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Left">
                                                <TextBox CssClass="txt" HelpText="224,TARIFF_CODE_TRFF_CD_CD" iCase="UPPER" OnClientTextChange=""
                                                    ValidationGroup="">
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                        IsRequired="true" RegErrorMessage="Only Alphabets and Numbers are allowed" ReqErrorMessage="Tariff Code is Required"
                                                        RegexValidation="True" CustomValidation="True" CustomValidationFunction="checkDuplicate"
                                                        LookupValidation="False" RegularExpression="^[a-zA-Z0-9]+$" />
                                                </TextBox>
                                                <HeaderStyle CssClass="" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                            </iFg:TextboxField>
                                            <iFg:TextboxField CharacterLimit="255" DataField="TRFF_CD_DTL_DSC" HeaderText="Description *"
                                                HeaderTitle="Description" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                                <TextBox CssClass="txt" HelpText="225,TARIFF_CODE_DETAIL_TRFF_CD_DTL_DSC" iCase="UPPER"
                                                    OnClientTextChange="" ValidationGroup="divLineDetail">
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                        IsRequired="true" RegErrorMessage="" ReqErrorMessage="Tariff Description is Required"
                                                        RegexValidation="False" LookupValidation="False" RegularExpression="" />
                                                </TextBox>
                                                <HeaderStyle CssClass="" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                            </iFg:TextboxField>
                                            <iFg:LookupField DataField="TRFF_CD_DTL_LCTN_CD" ForeignDataField="TRFF_CD_DTL_LCTN_ID"
                                                HeaderText="Item *" HeaderTitle="Item" PrimaryDataField="ITM_ID" SortAscUrl=""
                                                SortDescUrl="">
                                                <Lookup DataKey="ITM_CD" DependentChildControls="" HelpText="358,ITEM_ITM_CD" iCase="Upper"
                                                    OnClientTextChange="" ValidationGroup="divLineDetail" TableName="39" CssClass="lkp" MaxLength="4"
                                                    DoSearch="True" Width="70px" ClientFilterFunction="">
                                                    <LookupColumns>
                                                        <Inp:LookupColumn ColumnName="ITM_ID" Hidden="True" />
                                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="ITM_CD" Hidden="False" />
                                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="ITM_DSCRPTN_VC" Hidden="False" />
                                                    </LookupColumns>
                                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px" />
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Item  Required"
                                                        LkpErrorMessage="Invalid Item. Click on the list for valid values." />
                                                </Lookup>
                                                <HeaderStyle CssClass="" Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="100px" Wrap="True" />
                                            </iFg:LookupField>
                                            <iFg:LookupField DataField="TRFF_CD_DTL_CMPNT_CD" ForeignDataField="TRFF_CD_DTL_CMPNT_ID"
                                                HeaderText="Sub Item" HeaderTitle="Sub Item" PrimaryDataField="SB_ITM_ID" SortAscUrl=""
                                                SortDescUrl="">
                                                <Lookup DataKey="SB_ITM_CD" DependentChildControls="" HelpText="359,SUB_ITEM_SB_ITM_CD"
                                                    iCase="Upper" OnClientTextChange="" ValidationGroup="divLineDetail" TableName="43" MaxLength="3"
                                                    CssClass="lkp" DoSearch="True" Width="70px" ClientFilterFunction="applySubItemFilter">
                                                    <LookupColumns>
                                                        <Inp:LookupColumn ColumnName="SB_ITM_ID" Hidden="True" />
                                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="SB_ITM_CD" Hidden="False" />
                                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="SB_ITM_DSCRPTN_VC" Hidden="False" />
                                                    </LookupColumns>
                                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px" />
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="False"
                                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Sub Item  Required"
                                                        LkpErrorMessage="Invalid Sub Item. Click on the list for valid values." />
                                                </Lookup>
                                                <HeaderStyle CssClass="" Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="100px" Wrap="True" />
                                            </iFg:LookupField>
                                            <iFg:LookupField DataField="TRFF_CD_DTL_DMG_CD" ForeignDataField="TRFF_CD_DTL_DMG_ID"
                                                HeaderText="Damage *" HeaderTitle="Damage" PrimaryDataField="DMG_ID" SortAscUrl=""
                                                SortDescUrl="">
                                                <Lookup DataKey="DMG_CD" DependentChildControls="" HelpText="360,TRFF_CD_DTL_DMG_CD"
                                                    iCase="Upper" OnClientTextChange="" ValidationGroup="divLineDetail" TableName="23" MaxLength="2"
                                                    CssClass="lkp" DoSearch="True" Width="60px" ClientFilterFunction="applyDepoFilter">
                                                    <LookupColumns>
                                                        <Inp:LookupColumn ColumnName="DMG_ID" Hidden="True" />
                                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="DMG_CD" Hidden="False" />
                                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="DMG_DSCRPTN_VC" Hidden="False" />
                                                    </LookupColumns>
                                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px" />
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Damage Code Required"
                                                        LkpErrorMessage="Invalid Damage. Click on the list for valid values." />
                                                </Lookup>
                                                <HeaderStyle CssClass="" Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="100px" Wrap="True" />
                                            </iFg:LookupField>
                                            <iFg:LookupField DataField="TRFF_CD_DTL_RPR_CD" ForeignDataField="TRFF_CD_DTL_RPR_ID"
                                                HeaderText="Repair *" HeaderTitle="Repair" PrimaryDataField="RPR_ID" SortAscUrl=""
                                                SortDescUrl="">
                                                <Lookup DataKey="RPR_CD" DependentChildControls="" HelpText="361,REPAIR_RPR_CD" iCase="Upper"
                                                    OnClientTextChange="" ValidationGroup="divLineDetail" MaxLength="2" TableName="24"
                                                    CssClass="lkp" DoSearch="True" Width="60px" ClientFilterFunction="applyDepoFilter">
                                                    <LookupColumns>
                                                        <Inp:LookupColumn ColumnName="RPR_ID" Hidden="True" />
                                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="RPR_CD" Hidden="False" />
                                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="RPR_DSCRPTN_VC" Hidden="False" />
                                                    </LookupColumns>
                                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px" />
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Repair Code Required"
                                                        LkpErrorMessage="Invalid Repair. Click on the list for valid values." />
                                                </Lookup>
                                                <HeaderStyle CssClass="" Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="100px" Wrap="True" />
                                            </iFg:LookupField>
                                            <iFg:LookupField DataField="TRFF_CD_DTL_MTRL_CD" ForeignDataField="TRFF_CD_DTL_MTRL_ID"
                                                HeaderText="Material *" HeaderTitle="Material" PrimaryDataField="MTRL_ID" SortAscUrl=""
                                                SortDescUrl="">
                                                <Lookup DataKey="MTRL_CD" DependentChildControls="" HelpText="98,TARIFF_CODE_DETAIL_TRFF_CD_DTL_MTRL_ID"
                                                    iCase="Upper" OnClientTextChange="" ValidationGroup="divLineDetail" MaxLength="2"
                                                    TableName="14" CssClass="lkp" DoSearch="True" Width="60px" ClientFilterFunction="applyDepoFilter">
                                                    <LookupColumns>
                                                        <Inp:LookupColumn ColumnName="MTRL_ID" Hidden="True" />
                                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="MTRL_CD" Hidden="False" />
                                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="MTRL_DSCRPTN_VC" Hidden="False" />
                                                    </LookupColumns>
                                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px" />
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                                        Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Material Code Required"
                                                        LkpErrorMessage="Invalid Material. Click on the list for valid values." />
                                                </Lookup>
                                                <HeaderStyle CssClass="" Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="100px" Wrap="True" />
                                            </iFg:LookupField>
                                            <iFg:TextboxField CharacterLimit="0" DataField="TRFF_CD_DTL_MNHR" HeaderText="Man Hour "
                                                HeaderTitle="Man Hours" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                                <TextBox CssClass="ntxto" HelpText="362,TARIFF_CODE_DETAIL_TRFF_CD_DTL_MNHR" iCase="NUMERIC"
                                                    OnClientTextChange="formatManHours" ValidationGroup="divLineDetail">
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                        IsRequired="false" RegErrorMessage="Invalid Man Hour. Range must be from 0.01 to 9999999.99"
                                                        ReqErrorMessage="Man Hour Required." RegexValidation="True" LookupValidation="False"
                                                        RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$" />
                                                </TextBox>
                                                <HeaderStyle CssClass="" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:TextboxField>
                                            <iFg:TextboxField CharacterLimit="0" DataField="TRFF_CD_DTL_MTRL_CST" HeaderText="Material Cost"
                                                HeaderTitle="Material Cost" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                                                DataFormatString="{0:F2}" HtmlEncode="False">
                                                <TextBox CssClass="ntxto" HelpText="365,TARIFF_CODE_DETAIL_TRFF_CD_DTL_MTRL_CST"
                                                    iCase="NUMERIC" OnClientTextChange="formatManHours" ValidationGroup="divLineDetail">
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                        RegErrorMessage="Invalid Material Cost. Range must be from 0.01 to 9999999.99"
                                                        RegexValidation="True" LookupValidation="False" RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$" />
                                                </TextBox>
                                                <HeaderStyle CssClass="" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                                            </iFg:TextboxField>
                                            <iFg:TextboxField CharacterLimit="0" DataField="TRFF_CD_DTL_RMRKS" HeaderText="Remarks "
                                                HeaderTitle="REMARKS" SortAscUrl="" SortDescUrl="">
                                                <TextBox CssClass="txt" HelpText="404,TARIFF_CODE_DETAIL_TRFF_CD_DTL_RMRKS" iCase="None"
                                                    OnClientTextChange="" ValidationGroup="divLineDetail">
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                        RegexValidation="False" LookupValidation="False" />
                                                </TextBox>
                                                <HeaderStyle CssClass="" HorizontalAlign="Left" />
                                                <ItemStyle Width="200px" Wrap="True" />
                                            </iFg:TextboxField>
                                            <iFg:CheckBoxField DataField="TRFF_CD_DTL_ACTV_BT" HeaderText="Active" HeaderImageUrl=""
                                                HeaderTitle="Active" HelpText=""  SortAscUrl="" SortDescUrl="">
                                                <HeaderStyle CssClass="" Width="5%" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="5%" Wrap="True" HorizontalAlign="Left" />
                                            </iFg:CheckBoxField>
                                        </Columns>
                                        <EditRowStyle HorizontalAlign="Left" />
                                        <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                                        <SelectedRowStyle CssClass="gsitem" />
                                        <AlternatingRowStyle CssClass="gaitem" />
                                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                        <ActionButtons>
                                            <iFg:ActionButton ID="act_Upload" Text="Upload" ValidateRowSelection="False" OnClientClick="openUpload();"
                                                IconClass="icon-upload-alt" CSSClass="btn btn-small btn-info" />
                                            <%--<iFg:ActionButton ID="aBtExport" Text="Export" ValidateRowSelection="False" OnClientClick="exportToExcel();"
                                                IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />--%>
                                        </ActionButtons>
                                    </iFg:iFlexGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" align="center"
        onClientPrint="null" />
    <asp:HiddenField ID="hdnTariffId" runat="server" />
    </form>
</body>
</html>
