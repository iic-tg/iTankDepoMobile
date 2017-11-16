<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TariffGroup.aspx.vb" Inherits="Masters_TariffGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr class="ctab" style="width: 100%; height: 8px;">
            <td align="left" valign="middle">
                <span id="spnEstHeader" class="ctabh"><span id="spnEstNo">Tariff Group </span><span class='olbl' style="vertical-align: middle"></span></span>
            </td>
            <td align="right">
                <nv:Navigation ID="navEstimate" runat="server" />
            </td>
        </tr>
    </table>
     <!-- UIG Fix -->
    <div class="tabdisplayGatePass" id="tabEstimate" style="overflow-y: auto; overflow-x: auto;height:auto">
        <table border="0" cellpadding="1" cellspacing="1" class="tblstd" align="left">
            <tr>
                <td>
                    <div id="divHeader">
                        <table border="0" cellpadding="1" cellspacing="1" class="tblstd" align="Left">
                            <tr>
                                <td>
                                    <Inp:iLabel ID="lblTRFF_GRP_CD" runat="server" ToolTip="TariffGroup Code" CssClass="lbl">
 Code</Inp:iLabel>
                                    <Inp:iLabel ID="lblTariffGroupCode" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtTRFF_GRP_CD" runat="server" TabIndex="1" CssClass="txt" ToolTip="Tariff Group Code" HelpText="249,TARIFF_GROUP_TRFF_GRP_CD" Height="" TextMode="SingleLine" iCase="Upper" Width="" MaxLength="9">
                                        <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="True" ControlToCompare="" CsvErrorMessage="" CustomValidationFunction="ValidateTariffGroupCode" IsRequired="True" ReqErrorMessage="Tariff Group Code Required" ValidationGroup="tabTariffGroup" Validate="True" RegErrorMessage="Only Alphabets and Numbers are allowed" RegexValidation="True" RegularExpression="^[a-zA-Z0-9]+$"></Validator>
                                    </Inp:iTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <Inp:iLabel ID="lblTRFF_GRP_DSCRPTN" runat="server" ToolTip="TariffGroup Description" CssClass="lbl">
Description</Inp:iLabel>
                                    <Inp:iLabel ID="lblTRFF_GRP_DSCRPTNReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                                </td>
                                <td>
                                    <Inp:iTextBox ID="txtTRFF_GRP_DSCRPTN" runat="server" TabIndex="2" CssClass="txt" ToolTip="Tariff Group Code" HelpText="250,TARIFF_GROUP_TRFF_GRP_DESCRPTN_VC" Height="" TextMode="SingleLine" Width="" MaxLength="6">
                                        <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="True" ControlToCompare="" CsvErrorMessage="" CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Tariff Group Name Required" ValidationGroup="tabTariffGroup" Validate="True" RegErrorMessage="" RegexValidation="false" RegularExpression=""></Validator>
                                    </Inp:iTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td align="left" valign="top">
                                    <label id="lblActive" runat="server" class="lbl">
                                        Active</label>
                                </td>
                                <td valign="top">
                                    <asp:CheckBox ID="chkActive" runat="server" Width="97px" TabIndex="3" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="14">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="14">
                                    <Inp:iLabel ID="ILabel23" runat="server" CssClass="ublbl">Tariff Code Detail</Inp:iLabel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="14">
                                    <iFg:iFlexGrid ID="ifgTariffGroupDetail" runat="server" AllowStaticHeader="True" DataKeyNames="TRFF_GRP_DTL_ID" Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" Scrollbars="Auto" ShowEmptyPager="False" StaticHeaderHeight="145px" Type="Normal" ValidationGroup="divTariffGroup" UseCachedDataSource="True" AutoGenerateColumns="False" EnableViewState="False" OnAfterClientRowCreated="" ShowPageSizer="False" OnAfterCallBack="OnAfterCallBack" OnBeforeCallBack="OnBeforeCallBack" UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="btn btn-small btn-info" ClearButtonIconClass="icon-eraser" AllowSearch="True" AutoSearch="True" ShowRecordCount="False">
                                        <RowStyle CssClass="gitem" />
                                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                        <Columns>
                                            <iFg:LookupField DataField="TRFF_CD_CD" ForeignDataField="TRFF_CD_ID" HeaderText="Tariff Code *" HeaderTitle="" PrimaryDataField="TRFF_CD_ID" SortAscUrl="" SortDescUrl="">
                                                <Lookup ID="lkpTest" DataKey="TRFF_CD_CD" DependentChildControls="" HelpText="251,TARIFF_CODE_TRFF_CD_CD" iCase="Upper" OnClientTextChange="" TabIndex="4" ValidationGroup="divTariffGroup" MaxLength="15" TableName="44" CssClass="lkp" DoSearch="True" Width="120px" ClientFilterFunction="" AllowSecondaryColumnSearch="True" SecondaryColumnName="TRFF_CD_DESCRPTN_VC">
                                                    <LookupColumns>
                                                        <Inp:LookupColumn ColumnName="TRFF_CD_ID" Hidden="True" />
                                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="TRFF_CD_CD" Hidden="False" />
                                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="TRFF_CD_DESCRPTN_VC" Hidden="False" ControlToBind="1" />
                                                        <Inp:LookupColumn ColumnCaption="Remarks" ColumnName="RMRKS_VC" Hidden="True" ControlToBind="2" />
                                                    </LookupColumns>
                                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="140" />
                                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                                    <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" HorizontalAlign="Left" />
                                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True" Validate="True" ValidationGroup="divTariffGroup" ReqErrorMessage=" Tariff Code Required" LkpErrorMessage="Invalid Tariff Code. Click on the List for valid values" />
                                                </Lookup>
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                                <ItemStyle Width="150px" Wrap="True" HorizontalAlign="left" />
                                            </iFg:LookupField>
                                            <iFg:TextboxField DataField="TRFF_CD_DESCRPTN_VC" HeaderText="Description" HeaderTitle="Description" SortAscUrl="" SortDescUrl="">
                                                <TextBox ID="txtTariffDescription" CssClass="txt" HelpText="" OnClientTextChange="" ValidationGroup="" ReadOnly="true">
                                                    <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="Equal" ReqErrorMessage=" " Type="Double" />
                                                </TextBox>
                                                <ItemStyle Width="180px" Wrap="true" />
                                            </iFg:TextboxField>
                                            <iFg:TextboxField DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks" SortAscUrl="" SortDescUrl="">
                                                <TextBox ID="txtRemarks" CssClass="txt" HelpText="" OnClientTextChange="" ValidationGroup="" ReadOnly="true">
                                                    <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="Equal" ReqErrorMessage=" " Type="Double" />
                                                </TextBox>
                                                <ItemStyle Width="230px" Wrap="true" />
                                            </iFg:TextboxField>
                                            <iFg:CheckBoxField DataField="ACTV_BT" HeaderText="Active" HeaderTitle="Active" HelpText="" SortAscUrl="" SortDescUrl="" SortExpression="">
                                                <ItemStyle Width="25px" />
                                            </iFg:CheckBoxField>
                                        </Columns>
                                        <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                                        <SelectedRowStyle CssClass="gsitem" />
                                        <AlternatingRowStyle CssClass="gaitem" />
                                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                    </iFg:iFlexGrid>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
   
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();" onClientPrint="null" />
    <br />
    </form>
</body>
</html>
