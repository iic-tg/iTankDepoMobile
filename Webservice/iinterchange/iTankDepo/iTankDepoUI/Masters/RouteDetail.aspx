<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RouteDetail.aspx.vb" Inherits="Masters_RouteDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style ="height :550px">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Route</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navRoute" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="tabdisplayGatePass" id="tabRoute" style="overflow-y: auto; overflow-x: auto;height:auto">
        <div class="topspace">
        </div>
        <iFg:iFlexGrid ID="ifgRoute" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="10" AllowSearch="True"
            AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="300px" ShowEmptyPager="True"
            Width="100%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True"
            RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
            EnableViewState="False" AddButtonText="Add Route" DeleteButtonText="Delete" GridLines="Both"
            HorizontalAlign="NotSet" PageSizerFormat="" Scrollbars="None" Type="Normal" ValidationGroup="tabRoute"
            DataKeyNames="RT_ID" OnAfterCallBack="onAfterCB" OnBeforeCallBack="" OnAfterClientRowCreated="setDefaultValues"
            AddRowsonCurrentPage="False" UseIcons="true" SearchButtonIconClass="icon-search"
            SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
            AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
            DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
            RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
            ClearButtonCssClass="icon-eraser">
            <Columns>
                <iFg:TextboxField DataField="RT_CD" HeaderText="Code *" HeaderTitle="Code" SortAscUrl=""
                    SortDescUrl="" SortExpression="RT_CD" HtmlEncode="False">
                    <TextBox ID="txtRoute" HelpText="422" OnClientTextChange="" ValidationGroup="tabRoute"
                        CssClass="txt" iCase="Upper" MaxLength="10">
                        <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage="Only Alphabets and Numbers are allowed"
                            RegexValidation="True" RegularExpression="^[a-zA-Z0-9]+$" ReqErrorMessage="Route Code Required"
                            CustomValidationFunction="validateCode" CustomValidation="True" Type="String"
                            Validate="true" CsvErrorMessage="This Route Code already exist." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="RT_DSCRPTN_VC" HeaderText="Description *" HeaderTitle="Description"
                    SortAscUrl="" SortDescUrl="" SortExpression="RT_DSCRPTN_VC" HtmlEncode="False">
                    <TextBox ID="txtRouteDescription" HelpText="423" OnClientTextChange="" ValidationGroup="tabRoute"
                        CssClass="txt" MaxLength="50">
                        <Validator CustomValidateEmptyText="false" IsRequired="True" Operator="Equal" RegErrorMessage=""
                            RegexValidation="False" RegularExpression="" ReqErrorMessage="Route Description Required"
                            CustomValidationFunction="" CustomValidation="false" Type="String" Validate="true" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </iFg:TextboxField>
                <iFg:LookupField ForeignDataField="PCK_UP_LCTN_ID" PrimaryDataField="LCTN_ID" DataField="PCK_UP_LCTN_CD"
                    HeaderText="Pick Up Location *" HeaderTitle="Pick Up Location" SortAscUrl="" ReadOnly="false"
                    SortDescUrl="" SortExpression="PCK_UP_LCTN_CD">
                    <Lookup DataKey="LCTN_CD" DependentChildControls="" HelpText="424" iCase="Upper"
                        OnClientTextChange="" TableName="65" ValidationGroup="" ID="lkpPickUpLocation"
                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                        SecondaryColumnName="LCTN_DSCRPTN_VC" MaxLength="10">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="LCTN_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="LCTN_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="LCTN_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Pick Up Location. Click on the List for Valid Values"
                            ReqErrorMessage="Pick Up Location Required" Validate="True" IsRequired="True"
                            CustomValidation="True" CustomValidationFunction="ValidatePickupLocation" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="110px" />
                </iFg:LookupField>
                <%--   <iFg:LookupField ForeignDataField="ACTVTY_ID" PrimaryDataField="ENM_ID" DataField="ACTVTY_CD"
                    HeaderText="Activity" HeaderTitle="Activity" SortAscUrl=""
                    ReadOnly="false" SortDescUrl="" SortExpression="ACTVTY_CD">
                    <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="426"
                        iCase="Upper" OnClientTextChange="" TableName="64" ValidationGroup="" ID="lkpActivity"
                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" MaxLength ="15">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Activity" ColumnName="ENM_CD" Hidden="False" />
                        </LookupColumns>
                      <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Activity. Click on the List for Valid Values"
                            ReqErrorMessage="Activity Required" Validate="True" IsRequired="False" CustomValidation="false"
                            CustomValidationFunction="" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="100px" />
                </iFg:LookupField>--%>
                <%--   <iFg:LookupField ForeignDataField="ACTVTY_LCTN_ID" PrimaryDataField="LCTN_ID" DataField="ACTVTY_LCTN_CD"
                    HeaderText="Activity Location" HeaderTitle="Activity Location" SortAscUrl=""
                    ReadOnly="false" SortDescUrl="" SortExpression="ACTVTY_LCTN_CD">
                    <Lookup DataKey="LCTN_CD" DependentChildControls="" HelpText="427"
                        iCase="Upper" OnClientTextChange="" TableName="65" ValidationGroup="" ID="lklActivityLocation"
                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                        SecondaryColumnName="LCTN_DSCRPTN_VC" MaxLength ="10">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="LCTN_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="LCTN_CD" Hidden="False" />
                             <Inp:LookupColumn ColumnCaption="Description" ColumnName="LCTN_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                      <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Activity Location. Click on the List for Valid Values"
                            ReqErrorMessage="Drop Off Location Required" Validate="True" IsRequired="False" CustomValidation="True"
                            CustomValidationFunction="ValidateActivityLocation" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="80px" />
                </iFg:LookupField>--%>
                <iFg:LookupField ForeignDataField="DRP_OFF_LCTN_ID" PrimaryDataField="LCTN_ID" DataField="DRP_OFF_LCTN_CD"
                    HeaderText="Drop Off Location *" HeaderTitle="Drop Off Location" SortAscUrl=""
                    ReadOnly="false" SortDescUrl="" SortExpression="DRP_OFF_LCTN_CD">
                    <Lookup DataKey="LCTN_CD" DependentChildControls="" HelpText="425" iCase="Upper"
                        OnClientTextChange="" TableName="65" ValidationGroup="" ID="lklDropOffLocation"
                        CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                        SecondaryColumnName="LCTN_DSCRPTN_VC" MaxLength="10">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="LCTN_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="LCTN_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="LCTN_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="250px"
                            HorizontalAlign="Center" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Drop Off Location. Click on the List for Valid Values"
                            ReqErrorMessage="Drop Off Location Required" Validate="True" IsRequired="True"
                            CustomValidation="True" CustomValidationFunction="ValidateDropOffLocation" />
                    </Lookup>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="110px" />
                </iFg:LookupField>
              <%--  <iFg:TextboxField DataField="EMPTY_TRP_RT_NC" HeaderText="Empty Trip Rate" HeaderTitle="Empty Trip Rate"
                    SortAscUrl="" SortDescUrl="" SortExpression="EMPTY_TRP_RT_NC" HtmlEncode="False">
                    <TextBox ID="txtEmptyTripRate" HelpText="428" OnClientTextChange="FormatTwoDecimal"
                        ValidationGroup="tabRoute" CssClass="ntxto" iCase="Numeric" MaxLength="10">
                        <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="GreaterThanEqual"
                            ReqErrorMessage="Empty Trip Rate Required" Type="Double" CustomValidationFunction=""
                            CustomValidation="false" CsvErrorMessage="" Validate="True" RegErrorMessage="Invalid Empty Trip Rate. Range must be from 0.00 to 9999999.99"
                            RegexValidation="true" RegularExpression="^\d{1,7}(\.\d{1,2})?$" CmpErrorMessage="Empty Trip Rate must be greater than 0"
                            CompareValidation="True" ValueToCompare="0.00" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="right" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="FLL_TRP_RT_NC" HeaderText="Full Trip Rate" HeaderTitle="Full Trip Rate"
                    SortAscUrl="" SortDescUrl="" SortExpression="FLL_TRP_RT_NC" HtmlEncode="False">
                    <TextBox ID="txtFullTripRate" HelpText="429" OnClientTextChange="FormatTwoDecimal"
                        ValidationGroup="tabRoute" CssClass="ntxto" iCase="Numeric" MaxLength="10">
                        <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="GreaterThanEqual"
                            ReqErrorMessage="Full Trip Rate Required" Type="Double" CustomValidationFunction=""
                            CustomValidation="false" CsvErrorMessage="" Validate="True" RegErrorMessage="Invalid Full Trip Rate. Range must be from 0.00 to 9999999.99"
                            RegexValidation="true" RegularExpression="^\d{1,7}(\.\d{1,2})?$" CmpErrorMessage="Full Trip Rate must be greater than 0"
                            CompareValidation="True" ValueToCompare="0.00" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="right" />
                </iFg:TextboxField>--%>
                <iFg:CheckBoxField DataField="ACTV_BT" HeaderText="Active" HeaderTitle="Active" HelpText=""
                    SortAscUrl="" SortDescUrl="">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="30px" HorizontalAlign="Left" />
                </iFg:CheckBoxField>
            </Columns>
            <RowStyle CssClass="gitem" />
            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                OffsetX="" OffsetY="" OnImgClick="" Width="" />
            <PagerStyle CssClass="gpage" Height="18px" Font-Names="Verdana" HorizontalAlign="Center" />
            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
            <SelectedRowStyle CssClass="gsitem" />
            <AlternatingRowStyle CssClass="gaitem" />
        </iFg:iFlexGrid>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
        onClientPrint="null" />
    <asp:HiddenField ID="hdnPageTitle" runat="server" />
    </form>
</body>
</html>
