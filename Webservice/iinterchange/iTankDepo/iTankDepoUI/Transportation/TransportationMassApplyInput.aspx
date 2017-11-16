<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TransportationMassApplyInput.aspx.vb"
    Inherits="Transportation_TransportationMassApplyInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server" style="overflow: auto">
    <div id="tabTransportationMassApplyInput">
        <table align="center" width="100%">
            <tr>
                <td colspan="8">
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lblEqType" runat="server" CssClass="lbl">
                    Type
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpEqpType" runat="server" CssClass="lkp" DataKey="EQPMNT_TYP_CD"
                        DoSearch="True" iCase="Upper" MaxLength="10" TableName="3" HelpText="495,EQUIPMENT_TYPE_EQPMNT_TYP_CD"
                        ToolTip="Equipment Type" ClientFilterFunction="" ReadOnly="false" AllowSecondaryColumnSearch="true"
                        SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC" TabIndex="1">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_CD"  Hidden="False" ColumnCaption="Type" />
                            <Inp:LookupColumn ColumnName="EQPMNT_CD_CD"  Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="Left" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                            LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Equipment Type Required" Type="String" Validate="True"
                            ValidationGroup="divHeader" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="ILabel1" runat="server" CssClass="lbl">
                    Customer Ref No
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtCustomerRefNo" runat="server" CssClass="txt" HelpText="493,TRANSPORTATION_DETAIL_CSTMR_RF_NO"
                        iCase="None" TextMode="SingleLine" ToolTip="Customer Reference Number" TabIndex="3">
                        <Validator Validate="true" ValidationGroup="" Type="String" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblStartDate" runat="server" CssClass="lbl">
                    Job Start Date
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iDate ID="datJobStartDate" TabIndex="4" runat="server" HelpText="496" MaxLength="11"
                        CssClass="dat" iCase="Upper" ReadOnly="false" ToolTip="Job Start Date">
                        <Validator CustomValidateEmptyText="False" IsRequired="false" Type="Date" ValidationGroup=""
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Job Start Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="Job Start Date Required."
                            CmpErrorMessage="Job Start date Should not be greater than Current Date." RangeValidation="False"
                            CustomValidation="true" CustomValidationFunction="compareRequestDate" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
            </tr>
            <tr>
                <td>
                    <Inp:iLabel ID="lblJobEndDate" runat="server" CssClass="lbl">
                    Job End Date
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iDate ID="datJobEndDate" TabIndex="4" runat="server" HelpText="497" MaxLength="11"
                        CssClass="dat" iCase="Upper" ReadOnly="false" ToolTip="Job End Date">
                        <Validator CustomValidateEmptyText="False" IsRequired="false" Type="Date" ValidationGroup=""
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Job End Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CompareValidation="True" ReqErrorMessage="Job End Date Required."
                            CmpErrorMessage="Job End date Should not be greater than Current Date." RangeValidation="False"
                            CustomValidation="true" CustomValidationFunction="compareJobStartDate" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblCargo" runat="server" CssClass="lbl">
                  Cargo
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpPreviousCargo" runat="server" CssClass="lkp" DataKey="PRDCT_DSCRPTN_VC"
                        DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="5" TableName="46" HelpText="499,PRODUCT_PRDCT_CD"
                        ClientFilterFunction="" AllowSecondaryColumnSearch="true" SecondaryColumnName="PRDCT_DSCRPTN_VC"
                        ReadOnly="false">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="PRDCT_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="PRDCT_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="PRDCT_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="Right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                            LkpErrorMessage="Invalid Previous Cargo. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Previous Cargo Required" Type="String" Validate="True"
                            ValidationGroup="divCleaning" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
               <%-- <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblRemarks" runat="server" CssClass="lbl">
                   Remarks
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtMassRemarks" runat="server" CssClass="txt" TabIndex="6" HelpText="494"
                        MaxLength="500" TextMode="MultiLine" iCase="None" ToolTip="Remarks" ReadOnly="false">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="" RegularExpression=""
                            Type="String" Validate="True" ValidationGroup="" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" CustomValidation="false"
                            RegexValidation="false" />
                    </Inp:iTextBox>
                </td>--%>
            </tr>
            <tr>
                <td colspan="8">
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <Inp:iLabel ID="lblRateDetails" runat="server" Font-Bold="true" Font-Underline="true"
                        CssClass="lbl">
                    Rate Details
                    </Inp:iLabel>
                </td>
            </tr>
            <tr>
                <td colspan="8" align="center">
                    <iFg:iFlexGrid ID="ifgMassApplyInput" runat="server" AllowStaticHeader="True" DataKeyNames="TRNSPRTTN_DTL_RT_ID"
                        Width="400px" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Left"
                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="120px" Type="Normal"
                        ValidationGroup="divMassInputApply" UseCachedDataSource="True" AutoGenerateColumns="False"
                        EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                        AllowAdd="true" PageSize="10" AddRowsonCurrentPage="False" AllowPaging="false"
                        OnAfterCallBack="onAfterCallTransportationMassInput" AllowDelete="true" AllowRefresh="false"
                        AllowSearch="false" AutoSearch="True" UseIcons="true" SearchButtonIconClass="icon-search"
                        SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                        AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                        DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                        RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                        ClearButtonCssClass="icon-eraser" OnBeforeCallBack="">
                        <PagerStyle CssClass="gpage" />
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="gsitem" />
                        <Columns>
                            <iFg:LookupField DataField="ADDTNL_CHRG_RT_CD" ForeignDataField="ADDTNL_CHRG_RT_ID"
                                HeaderText="Charge" HeaderTitle="Charge" PrimaryDataField="ADDTNL_CHRG_RT_ID"
                                SortAscUrl="" SortDescUrl="" HeaderStyle-Width="50px" ReadOnly="false">
                                <Lookup DataKey="ADDTNL_CHRG_RT_CD" DependentChildControls="" HelpText="500,ADDITIONAL_CHARGE_RATE_ADDTNL_CHRG_RT_CD"
                                    iCase="Upper" ValidationGroup="divMassInputApply" MaxLength="10" OnClientTextChange="onClientChargeClick"
                                    ToolTip="Charge Rate Code" TableName="75" CssClass="lkp" DoSearch="True" Width="80px"
                                    ClientFilterFunction="onClickCharge" AllowSecondaryColumnSearch="false" SecondaryColumnName="ADDTNL_CHRG_RT_DSCRPTN_VC">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="ADDTNL_CHRG_RT_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="ADDTNL_CHRG_RT_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="ADDTNL_CHRG_RT_DSCRPTN_VC"
                                            Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Rate" ColumnName="RT_NC" Hidden="false"  />
                                        <Inp:LookupColumn ColumnCaption="Default Bit" ColumnName="DFLT_BT" Hidden="true" />
                                        <Inp:LookupColumn ColumnCaption="Operation Type" ColumnName="OPRTN_TYP_ID" Hidden="true" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="NotSet" Width="500px"
                                        HorizontalAlign="Left" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="true"
                                        CustomValidation="true" CustomValidationFunction="validateChargeCode" LkpErrorMessage="Invalid Charge. Click on the list for valid values"
                                        ReqErrorMessage="Charge Required" Validate="True" ValidationGroup="divMassInputApply" />
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="200px" Wrap="True" HorizontalAlign="Left" />
                            </iFg:LookupField>
                            <iFg:TextboxField DataField="ADDTNL_CHRG_RT_NC" HeaderText="Rate" HeaderTitle="Rate"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxt" HelpText="498,ADDITIONAL_CHARGE_RATE_RT_NC"
                                    iCase="Numeric" OnClientTextChange="" ToolTip="Rate" ValidationGroup="divMassInputApply">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        RegexValidation="true" RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$" RegErrorMessage="Invalid Additional Charge Rate. Range must be from 0 to 9999999.99"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                        </Columns>
                    </iFg:iFlexGrid>
                </td>
            </tr>
        </table>
        <table align="center">
            <tr>
                <td align="right">
                    <a href="#" id="btnApply" onclick="onApplyMassApplyInput(); return false;" class="btn btn-small btn-success"
                        runat="server" style="font-weight: bold; vertical-align: middle"><i class="icon-save">
                        </i>&nbsp;Apply</a>
                </td>
                <td align="left">
                    <a href="#" id="btnCancel" onclick="onCancelMassApplyInput(); return false;" class="btn btn-small btn-danger"
                        runat="server" style="font-weight: bold; vertical-align: middle"><i class="icon-remove">
                        </i>&nbsp;Cancel</a>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnTransportationId" runat="server" />
    <asp:HiddenField ID="hdnTransportationDetailId" runat="server" />
    <asp:HiddenField ID="hdnRequestDate" runat="server" />
    <asp:HiddenField ID="hdnEmptyRate" runat="server" />
    <asp:HiddenField ID="hdnFullRate" runat="server" />
    </form>
</body>
</html>
