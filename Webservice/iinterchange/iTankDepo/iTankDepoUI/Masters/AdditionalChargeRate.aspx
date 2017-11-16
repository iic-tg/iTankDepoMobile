<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdditionalChargeRate.aspx.vb"
    Inherits="Masters_AdditionalChargeRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="height: 550px">
    <div>
        <div>
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh">Additional Charge Rate</span>
                    </td>
                    <td align="right">
                        <nv:Navigation ID="navRoute" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <!-- UIG Issue Fix (IE) -->
        <div class="tabdisplayGatePass" id="tabAdditionalRate" style="overflow-y: auto; overflow-x: auto;height:auto">
            <div class="topspace">
            </div>
            <iFg:iFlexGrid ID="ifgAdditionalCharge" runat="server" AutoGenerateColumns="False"
                AllowPaging="True" CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="10"
                AllowSearch="True" AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="300px"
                ShowEmptyPager="True" Width="99.5%" UseCachedDataSource="True" AllowSorting="True"
                AllowStaticHeader="True" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                HeaderRows="1" EnableViewState="False" AddButtonText="Add Additional Charge Rate "
                DeleteButtonText="Delete" GridLines="Both" HorizontalAlign="NotSet" PageSizerFormat=""
                Scrollbars="None" Type="Normal" ValidationGroup="tabAdditionalRate" DataKeyNames="ADDTNL_CHRG_RT_ID"
                OnAfterCallBack="onAfterCB" OnBeforeCallBack="" OnAfterClientRowCreated="setDefaultValues"
                AddRowsonCurrentPage="False" UseIcons="true" SearchButtonIconClass="icon-search"
                SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                ClearButtonCssClass="icon-eraser">
                <Columns>
                    <iFg:LookupField ForeignDataField="OPRTN_TYP_ID" PrimaryDataField="ENM_ID" DataField="OPRTN_TYP_CD"
                        HeaderText="Operation Type *" HeaderTitle="Opertaion Type *" SortAscUrl="" ReadOnly="false"
                        SortDescUrl="" SortExpression="OPRTN_TYP_CD">
                        <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="431" iCase="Upper" OnClientTextChange=""
                            TableName="66" ValidationGroup="" ID="lklOperationType" CssClass="lkp" DoSearch="True"
                            Width="80px" ClientFilterFunction="" MaxLength="15">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnCaption="ID" ColumnName="ENM_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnCaption="Operation Type" ColumnName="ENM_CD" Hidden="False" />
                            </LookupColumns>
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px"
                                HorizontalAlign="Center" />
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Operation Type. Click on the List for Valid Values"
                                ReqErrorMessage="Operation Type Required" Validate="True" IsRequired="True" CustomValidation="true"
                                CustomValidationFunction="validateCharge" />
                        </Lookup>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="150px" />
                    </iFg:LookupField>
                    <iFg:TextboxField DataField="ADDTNL_CHRG_RT_CD" HeaderText="Charge *" HeaderTitle="Charge *"
                        SortAscUrl="" SortDescUrl="" SortExpression="ADDTNL_CHRG_RT_CD" HtmlEncode="False">
                        <TextBox ID="txtCharge" HelpText="432" OnClientTextChange="" ValidationGroup="tabAdditionalRate"
                            CssClass="txt" iCase="Upper" MaxLength="10">
                            <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage="Only Alphabets and Numbers are allowed"
                                RegexValidation="True" RegularExpression="^[a-zA-Z0-9]+$" ReqErrorMessage="Charge Required"
                                CustomValidationFunction="validateCharge" CustomValidation="True" Type="String"
                                Validate="true" CsvErrorMessage="" />
                        </TextBox>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </iFg:TextboxField>
                    <iFg:TextboxField DataField="ADDTNL_CHRG_RT_DSCRPTN_VC" HeaderText="Description *"
                        HeaderTitle="Description *" SortAscUrl="" SortDescUrl="" SortExpression="ADDTNL_CHRG_RT_DSCRPTN_VC"
                        HtmlEncode="False">
                        <TextBox ID="txtChargeDescription" HelpText="433" OnClientTextChange="" ValidationGroup="tabAdditionalRate"
                            CssClass="txt" MaxLength="50">
                            <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                                RegexValidation="False" RegularExpression="" ReqErrorMessage="Description Required"
                                CustomValidationFunction="" CustomValidation="false" Type="String" Validate="true" />
                        </TextBox>
                        <HeaderStyle Width="350px" HorizontalAlign="Left" />
                        <ItemStyle Width="350px" HorizontalAlign="Left" />
                    </iFg:TextboxField>
                    <iFg:TextboxField DataField="RT_NC" HeaderText="Rate" HeaderTitle="Rate" SortAscUrl=""
                        SortDescUrl="" SortExpression="RT_NC" HtmlEncode="False">
                        <TextBox ID="txtRate" HelpText="434" OnClientTextChange="FormatTwoDecimal" ValidationGroup="tabAdditionalRate"
                            CssClass="ntxto" iCase="Numeric" MaxLength="10">
                            <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="GreaterThanEqual"
                                ReqErrorMessage="Rate Required" Type="Double" CustomValidationFunction="" CustomValidation="false"
                                CsvErrorMessage="" Validate="True" RegErrorMessage="Invalid Rate. Range must be from 0.00 to 9999999.99"
                                RegexValidation="true" RegularExpression="^\d{1,7}(\.\d{1,2})?$" CmpErrorMessage="Rate must be greater than 0"
                                CompareValidation="True" ValueToCompare="0.00" />
                        </TextBox>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="110px" HorizontalAlign="right" />
                    </iFg:TextboxField>
                    <iFg:CheckBoxField DataField="DFLT_BT" HeaderText="Default" HeaderTitle="Default"
                        HelpText="" SortAscUrl="" SortDescUrl="">
                        <HeaderStyle Width="30px" HorizontalAlign="Left" />
                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                    </iFg:CheckBoxField>
                    <iFg:CheckBoxField DataField="ACTV_BT" HeaderText="Active" HeaderTitle="Active" HelpText=""
                        SortAscUrl="" SortDescUrl="">
                        <HeaderStyle Width="30px" HorizontalAlign="Left" />
                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                    </iFg:CheckBoxField>
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
        </div>
            <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
                onClientPrint="null" />
            <asp:HiddenField ID="hdnPageTitle" runat="server" />
    </form>
</body>
</html>
