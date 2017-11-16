<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EquipmentTypeCode.aspx.vb"
    Inherits="Masters_EquipmentTypeCode" %>

<%@ Register Assembly="iInterchange.iTankDepo.Business.Common" Namespace="iInterchange.iTankDepo.Business.Common"
    TagPrefix="iExp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Master >> Equipment >> Equipment Type and Code</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navExRate" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="" id="tabViewInvoice" style="height: auto">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width: 100%;">
            <tr>
                <td>
                    <%-- <div id="divRecordNotFound" style="display: none; font-weight: bold;" align="center">
                        There are no Draft or Final Invoice(s) generated to be viewed.
                    </div>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divViewInvoice" style="display: none;">
                        <iFg:iFlexGrid ID="ifgEquipmentTypeCode" runat="server" AllowStaticHeader="True"
                            DataKeyNames="EQPMNT_TYP_ID" Width="100%" CaptionAlign="NotSet" GridLines="Both"
                            HeaderRows="1" HorizontalAlign="Justify" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
                            ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="setDefaultValues"
                            PageSize="1000" AddRowsonCurrentPage="False" AllowPaging="True" AllowAdd="true"
                            AllowDelete="true" OnAfterCallBack="chekDuplicates" AllowRefresh="True" AllowEdit="true"
                            AllowExport="false" AllowSearch="True" AutoSearch="True" UseIcons="True" SearchButtonCssClass="btn btn-small btn-info"
                            DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonCssClass="btn btn-small btn-danger" ClearButtonCssClass="btn btn-small btn-success"
                            SearchButtonIconClass="icon-search" AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" RefreshButtonIconClass="icon-refresh" SearchCancelButtonIconClass="icon-remove">
                            <Columns>
                                <iFg:TextboxField CharacterLimit="0" DataField="EQPMNT_TYP_CD" HeaderText="Equipment Type *"
                                    HeaderTitle="Equipment Type *" SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="txt" HelpText="616" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        MaxLength="5">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            IsRequired="true" ReqErrorMessage="Equipment Type Required." RegularExpression="^[a-zA-Z0-9]+$"
                                            RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="40px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="EQPMNT_CD_CD" HeaderText="Equipment Code  *"
                                    HeaderTitle="Equipment Code *" SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="txt" HelpText="617" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        MaxLength="4">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            IsRequired="true" RegularExpression="^[a-zA-Z0-9]+$" RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed"
                                            ReqErrorMessage="Equipment Code Required." />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="40px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="EQPMNT_TYP_DSCRPTN_VC" HeaderText="Equipment Type Description *"
                                    HeaderTitle="Equipment Type Description *" SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="txt" HelpText="618" iCase="none" OnClientTextChange="" ValidationGroup=""
                                        MaxLength="255">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            RegexValidation="false" RegularExpression="" RegErrorMessage="" IsRequired="true"
                                            ReqErrorMessage="Equipment Type Description Required." />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="500px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="EQPMNT_GRP_CD" HeaderText="Equipment Group *"
                                    HeaderTitle="Equipment Group *" SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="txt" HelpText="619" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        MaxLength="3">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            IsRequired="True" ReqErrorMessage="Equipment Group Required." RegularExpression="^[a-zA-Z0-9]+$"
                                            RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="40px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:CheckBoxField DataField="ACTV_BT" HeaderText="Active" HeaderTitle="Active" HelpText=""
                                    SortAscUrl="" SortDescUrl="" SortExpression="">
                                    <ItemStyle Width="25px" />
                                    <HeaderStyle HorizontalAlign="Left" />
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
                </td>
            </tr>
        </table>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
        onclientprint="null" />
    <asp:HiddenField ID="hdnPageTitle" runat="server" />
    </form>
</body>
</html>
