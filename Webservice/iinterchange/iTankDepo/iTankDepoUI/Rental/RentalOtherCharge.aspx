<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RentalOtherCharge.aspx.vb"
    Inherits="Rental_RentalOtherCharge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server" style="overflow: auto">
    <div>
        <div id="divRentalOtherCharge" class="tabOtherCharge">
            <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="center">
                <tr>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td align="center">
                                    <Inp:iLabel ID="lblRateDetail" runat="server" CssClass="lbl">
                   Total Additional Charge
                                    </Inp:iLabel>
                                </td>
                                <td align="right">
                                    <Inp:iLabel ID="lblAdditionalRateDetail" runat="server" Font-Bold="true" CssClass="lbl">
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
                <tr style="width: 100%;">
                    <td colspan="5">
                        <iFg:iFlexGrid ID="ifgRentalOtherCharge" runat="server" AllowStaticHeader="True"
                            DataKeyNames="RNTL_OTHR_CHRG_ID" Width="400px" CaptionAlign="NotSet" GridLines="Both"
                            HeaderRows="1" HorizontalAlign="Justify" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="False" StaticHeaderHeight="150px" Type="Normal"
                            ValidationGroup="divRentalOtherCharge" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="setDefaultValues"
                            PageSize="10" AddRowsonCurrentPage="False" ShowPageSizer="False" AllowPaging="True"
                            ShowFooter="True" OnAfterCallBack="onAfterCallAdditionalRate" AllowRefresh="False"
                            AllowSearch="False" AutoSearch="False" OnBeforeCallBack="onBeforeCallback" RowStyle-HorizontalAlign="NotSet"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser" ShowRecordCount="False">
                            <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:LookupField DataField="ADDTNL_CHRG_RT_CD" ForeignDataField="ADDTNL_CHRG_RT_ID"
                                    HeaderText="Charge *" HeaderTitle="Charge" PrimaryDataField="ADDTNL_CHRG_RT_ID"
                                    SortAscUrl="" SortDescUrl="" SortExpression="">
                                    <Lookup DataKey="ADDTNL_CHRG_RT_CD" DependentChildControls="" HelpText="510,ADDITIONAL_CHARGE_RATE_ADDTNL_CHRG_RT_CD"
                                        iCase="Upper" OnClientTextChange="onClientChargeClick" TableName="75" ValidationGroup="divRentalEntry"
                                        ID="lkpCharge" CssClass="lkp" DoSearch="True" Width="110px" ClientFilterFunction="FilterAdditionalCharge"
                                        TabIndex="1">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="ADDTNL_CHRG_RT_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="ADDTNL_CHRG_RT_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="ADDTNL_CHRG_RT_DSCRPTN_VC"
                                                Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Rate" ColumnName="RT_NC" Hidden="false" />
                                            <Inp:LookupColumn ColumnCaption="Default Bit" ColumnName="DFLT_BT" Hidden="true" />
                                            <Inp:LookupColumn ColumnCaption="Operation Type" ColumnName="OPRTN_TYP_ID" Hidden="true" />
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="3" VerticalAlign="NotSet" Width="250px"
                                            HorizontalAlign="Center" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Charge. Click on the List for Valid Values"
                                            ReqErrorMessage="Charge Required" Validate="True" IsRequired="True" CustomValidation="True"
                                            CustomValidationFunction="checkDuplicateOtherCharge" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="120px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:TextboxField CharacterLimit="0" DataField="RT_NC" HeaderText="Rate" HeaderTitle="Rate"
                                    SortAscUrl="" SortDescUrl=""  AllowSearch="true" ReadOnly="false">
                                    <TextBox CssClass="ntxto" HelpText="521,ADDITIONAL_CHARGE_RATE_RT_NC" iCase="Numeric"
                                        OnClientTextChange="" ValidationGroup="divRentalOtherCharge">
                                        <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="GreaterThanEqual"
                                            ReqErrorMessage="Rate Required" Type="Double" CustomValidationFunction="" CustomValidation="false"
                                            CsvErrorMessage="" Validate="True" RegErrorMessage="Invalid Rate. Range must be from 0.00 to 9999999.99"
                                            RegexValidation="true" RegularExpression="^\d{1,7}(\.\d{1,2})?$" CmpErrorMessage="Rate must be greater than 0"
                                            CompareValidation="True" ValueToCompare="0.00" />
                                    </TextBox>
                                   <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Right"  />
                                </iFg:TextboxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                        </iFg:iFlexGrid>
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td align="right">
                        <a href="#" id="btnSave" onclick="addOtherCharge(); return false;" class="btn btn-small btn-success"
                            runat="server" style="font-weight: bold; vertical-align: middle"><i class="icon-save">
                            </i>&nbsp;Save</a>
                    </td>
                 <%--   <td align="left">
                        <a href="#" id="btnCancel" onclick="onClose(); return false;" class="btn btn-small btn-danger"
                            runat="server" style="font-weight: bold; vertical-align: middle"><i class="icon-remove">
                            </i>&nbsp;Cancel</a>
                    </td>--%>
                </tr>
            </table>
            <%--<table align="center">
            <tr>
                <td align="center">
                    <div class="button">
                          <ul>
                        <li class="btn btn-small btn-success"><a href="#" data-corner="8px" id="btnSubmit" style="text-decoration: none; color: White; font-weight: bold" class="icon-save" runat="server" onclick="addOtherCharge();return false;">&nbsp;Submit</a></li>
                    </ul>
                    </div>
                </td>
            </tr>
        </table>--%>
        </div>
    </div>
    <asp:HiddenField ID="hdnEquipmentNo" runat="server" />
    <asp:HiddenField ID="hdnContractNo" runat="server" />
    <asp:HiddenField ID="hdnCustomerId" runat="server" />
    <asp:HiddenField ID="hdnRentalId" runat="server" />
    <asp:HiddenField ID="hdnEqpmntID" runat="server" />
    </form>
</body>
</html>
