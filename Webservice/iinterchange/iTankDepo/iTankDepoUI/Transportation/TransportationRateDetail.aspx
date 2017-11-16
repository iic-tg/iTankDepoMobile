<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TransportationRateDetail.aspx.vb"
    Inherits="Transportation_TransportationRateDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server" style="overflow: auto">
    <div id="tabTransportationRate">
        <table align="center">
            <tr>
                <td colspan="3">
                    <table>
                        <tr>
                            <td align="center">
                                <Inp:iLabel ID="lblRateDetail" runat="server" CssClass="lbl">
                   Total Additional Charge:
                                </Inp:iLabel>
                            </td>
                            <td align="right">
                                <Inp:iLabel ID="lblTripRateDetail" runat="server" Font-Bold="true" CssClass="lbl">
                   0.00
                                </Inp:iLabel>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <Inp:iLabel ID="lblRateDetails" runat="server" CssClass="lbl" Font-Bold="true" Font-Underline="true">
                    Rate Details
                    </Inp:iLabel>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <iFg:iFlexGrid ID="ifgTransportationDetailRate" runat="server" AllowStaticHeader="True"
                        DataKeyNames="TRNSPRTTN_DTL_RT_ID" Width="400px" CaptionAlign="NotSet" GridLines="Both"
                        HeaderRows="1" HorizontalAlign="Justify" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="120px" Type="Normal"
                        ValidationGroup="divTripRateDetails" UseCachedDataSource="True" AutoGenerateColumns="False"
                        EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="setDefaultValues"
                        AllowAdd="true" PageSize="1000" AddRowsonCurrentPage="False" AllowPaging="false"
                        OnAfterCallBack="onAfterCallTransportationDetailRate" AllowDelete="true" AllowRefresh="false"
                        AllowSearch="false" AutoSearch="True" UseIcons="true" SearchButtonIconClass="icon-search"
                        SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                        AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                        DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                        RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                        ClearButtonCssClass="icon-eraser" OnBeforeCallBack="onBeforeCallTransportationDetailRate">
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
                                    iCase="Upper" OnClientTextChange="onClientChargeClick" ValidationGroup="divMassInputApply" TableName="75"
                                    CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="onClickCharge"
                                    AllowSecondaryColumnSearch="false" SecondaryColumnName="ADDTNL_CHRG_RT_DSCRPTN_VC">
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
                                    <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="NotSet" Width="350px"   />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="true"
                                        CustomValidationFunction="validateChargeCode" CustomValidation="true" LkpErrorMessage="Invalid Charge. Click on the list for valid values"
                                        ReqErrorMessage="Charge Required" Validate="True" ValidationGroup="divTripRateDetails" />
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="120px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:TextboxField DataField="ADDTNL_CHRG_RT_NC" HeaderText="Rate" HeaderTitle="Rate"
                                SortAscUrl="" SortDescUrl="" >
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="498,ADDITIONAL_CHARGE_RATE_RT_NC" 
                                    iCase="Numeric" OnClientTextChange="formatDecimalRate" ToolTip="Additional Rate" ValidationGroup="divTripRateDetails">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false" RegexValidation="true"
                                     RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$"  RegErrorMessage="Invalid Additional Charge Rate. Range must be from 0.01 to 9999999.99" 
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right"  />
                            </iFg:TextboxField>
                        </Columns>
                    </iFg:iFlexGrid>
                </td>
            </tr>
            <tr>
               <td colspan="3">&nbsp;</td>
            </tr>         
           
        </table>
        <table align="center" >
            <tr>
                 <td align="right">
                    <a href="#" id="btnSave" onclick="onSaveTransportationDetail(); return false;" class="btn btn-small btn-success"
                        runat="server" style="font-weight: bold; vertical-align: middle"><i class="icon-save">
                        </i>&nbsp;Save</a>
                </td>
                <td align="left">
                   <%-- <a href="#" id="btnCancel" onclick="onCloseTransportationDetail(); return false;"
                        class="btn btn-small btn-danger" runat="server" style="font-weight: bold; vertical-align: middle">
                        <i class="icon-remove"></i>&nbsp;Cancel</a>--%>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnTransportationId" runat="server" />
    <asp:HiddenField ID="hdnTransportationDetailId" runat="server" />
    <asp:HiddenField ID="hdnCancel" runat="server" />
    </form>
</body>
</html>
