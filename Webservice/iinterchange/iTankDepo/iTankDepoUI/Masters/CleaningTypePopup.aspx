<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CleaningTypePopup.aspx.vb"
    Inherits="Masters_CleaningTypePopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server" style="overflow: auto">
    <%-- <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 20px;">
                <td align="left">
                </td>
            </tr>
        </table>
    </div>--%>
    <div class="" id="tabProductCleaningType" style="height: 350px;">
        <table>
            <tr>
                <td>
                    <div id="divifgProductCleaningType" style="margin: 1px; width: 400px;">
                        <iFg:iFlexGrid ID="ifgProductCleaningType" runat="server" AllowStaticHeader="true"
                            DataKeyNames="PRDCT_CSTMR_RT_ID" Width="380px" Height="" CaptionAlign="NotSet"
                            GridLines="Both" HeaderRows="1" HorizontalAlign="NotSet" 
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="150px" Type="Normal"
                            ValidationGroup="divProductCleaningType" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnAfterClientRowCreated="" CssClass="tblstd" ShowPageSizer="False"
                            OnAfterCallBack="" OnBeforeCallBack="onBeforeCallback" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonCssClass="btn btn-small btn-danger" ClearButtonCssClass="icon-eraser"
                            UseIcons="True" PageSize="15">
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:LookupField DataField="CLNNG_TYP_CD" HeaderText="Cleaning Type *" PrimaryDataField="CLNNG_TYP_ID"
                                    ForeignDataField="CLNNG_TYP_ID" HeaderTitle="Cleaning Type" SortAscUrl="" SortDescUrl=""
                                    SortExpression="">
                                    <Lookup DataKey="CLNNG_TYP_CD" DependentChildControls="" HelpText="277,CLEANING_TYPE_CLNNG_TYP_CD"
                                        iCase="Upper" OnClientTextChange="" TableName="42" ValidationGroup="divProductCleaningType"
                                        ID="lkpCLNG_TYP" CssClass="lkp" DoSearch="True" Width="100px" ClientFilterFunction=""
                                        AllowSecondaryColumnSearch="false" SecondaryColumnName="CLNNG_TYP_DSCRPTN_VC">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="CLNNG_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnName="CLNNG_TYP_CD" Hidden="False" ColumnCaption="Code" />
                                            <Inp:LookupColumn ColumnName="CLNNG_TYP_DSCRPTN_VC" Hidden="False" ColumnCaption="Description" />
                                            <Inp:LookupColumn ColumnName="DFLT_BT" ColumnCaption="Default" Hidden="true" />
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                        <Validator CustomValidateEmptyText="true" Operator="Equal" Type="String" LkpErrorMessage="Invalid Cleaning Type. Click on the List for Valid Values"
                                            ReqErrorMessage="Cleaning Type Required" Validate="True" IsRequired="True" CustomValidation="true"
                                            ValidationGroup="divProductCleaningType" CustomValidationFunction="checkDuplicateCleaningType" />
                                    </Lookup>
                                    <ItemStyle Width="150px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:TextboxField DataField="AMNT_NC" HeaderText="Rate *" HeaderTitle="Rate" SortAscUrl=""
                                    SortDescUrl="">
                                    <TextBox ID="txtAMNT_NC" CssClass="ntxt" HelpText="159" iCase="Numeric" OnClientTextChange="formatCleaningType"
                                        ValidationGroup="divProductCleaningType" Width="80px" MaxLength="10">
                                        <Validator CustomValidateEmptyText="False" IsRequired="True" Validate="true" Operator="Equal"
                                            ReqErrorMessage="Rate Required" Type="Double" RegexValidation="True" RegularExpression="^\d{1,7}(\.\d{1,2})?$"
                                            RegErrorMessage="Invalid Cleaning Rate. Cleaning Rate must be upto 7 digits and 2 decimal points" />
                                    </TextBox>
                                    <ItemStyle Width="200px" Wrap="true" HorizontalAlign="Right" />
                                </iFg:TextboxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                        </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
        </table>
        <table class="button" align="center" style="width: 120px">
            <tr>
                <td align="center">
                    <ul>
                        <li class="btn btn-small btn-success"><a href="#" data-corner="8px" id="btnSubmit"
                            style="text-decoration: none; color: White; font-weight: bold" class="icon-save"
                            runat="server" onclick="addCustomerCleaningRate();return false;">&nbsp;Submit</a></li>
                    </ul>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnProductCleaningRate" runat="server" />
    <asp:HiddenField ID="hdnrowIndex" runat="server" />
    <asp:HiddenField ID="hdnProductID" runat="server" />
    <asp:HiddenField ID="hdnProductCustomerID" runat="server" />
    <asp:HiddenField ID="hdnCustomerID" runat="server" />
    </form>
</body>
</html>
