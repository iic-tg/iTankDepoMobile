<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FinanceIntegrationMaster.aspx.vb"
    Inherits="Sage_Integration_FinanceIntegrationMaster" %>

    <%@ Register Assembly="iInterchange.iTankDepo.Business.Common" Namespace="iInterchange.iTankDepo.Business.Common"
    TagPrefix="iExp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
   
    
            
             <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Admin >> Finance Integration</span>
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
                    <div id="divLineDetail" style="display: none;">
                        <iFg:iFlexGrid ID="ifgFinanceIntegration" runat="server" AllowStaticHeader="True"
                            DataKeyNames="FNC_INTGRTN_ID" Width="100%" CaptionAlign="NotSet" GridLines="Both"
                            HeaderRows="1" HorizontalAlign="Justify" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
                            ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="1000" AddRowsonCurrentPage="False" AllowPaging="True" AllowAdd="true"
                            AllowDelete="true" OnAfterCallBack="chekDuplicates" AllowRefresh="True" AllowEdit="true"
                            AllowExport="false" AllowSearch="True" AutoSearch="True" UseIcons="True" SearchButtonCssClass="btn btn-small btn-info"
                            DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonCssClass="btn btn-small btn-danger" ClearButtonCssClass="btn btn-small btn-success"
                            SearchButtonIconClass="icon-search" AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" RefreshButtonIconClass="icon-refresh" SearchCancelButtonIconClass="icon-remove">
                            <Columns>
                                <iFg:LookupField DataField="INVC_TYP_CD" ForeignDataField="INVC_TYP_ID" HeaderText="Invoice Type *"
                                    HeaderTitle="Invoice Type *" PrimaryDataField="ENM_ID" SortAscUrl="" SortDescUrl="">
                                    <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="442" iCase="Upper" OnClientTextChange=""
                                        ValidationGroup="divLineDetail" TableName="63" CssClass="lkp" DoSearch="True"
                                        ClientFilterFunction="" OnLookupClick="" MaxLength="1">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Invoice Type" ColumnName="ENM_CD" Hidden="False" />
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="150px"
                                            HorizontalAlign="Right" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            Validate="True" ValidationGroup="" ReqErrorMessage="Invoice Type Required." LkpErrorMessage="Invoice Type. Click on the list for valid values."
                                            CustomValidation="false" CustomValidationFunction="" />
                                    </Lookup>
                                    <HeaderStyle Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="80px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Equipment Type *"
                                    HeaderTitle="Equipment Type *" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl=""
                                    SortDescUrl="">
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                        OnClientTextChange="" ValidationGroup="divLineDetail" TableName="87" CssClass="lkp"
                                        DoSearch="True" ClientFilterFunction="" OnLookupClick="" MaxLength="1">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Type" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                                Hidden="False" />
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="7" VerticalAlign="NotSet" Width="300px"
                                            HorizontalAlign="Right" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            Validate="True" ValidationGroup="divLineDetail" ReqErrorMessage="Equipment Type Required"
                                            LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values."
                                            CustomValidation="false" CustomValidationFunction="" />
                                    </Lookup>
                                    <HeaderStyle Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="80px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:TextboxField CharacterLimit="0" DataField="CTGRY_CD" HeaderText="Category" HeaderTitle="Category"
                                    SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="txt" HelpText="" iCase="none" OnClientTextChange="" ValidationGroup="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            RegexValidation="false" RegularExpression="" RegErrorMessage="" IsRequired="false"
                                            ReqErrorMessage="Category Required." />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="ITEM_CD" HeaderText="Item ID *" HeaderTitle="Item ID *"
                                    SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            RegexValidation="false" RegularExpression="" RegErrorMessage="" IsRequired="true"
                                            ReqErrorMessage="Item ID Required." />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="ITEM_DSCRPTN_VC" HeaderText="Item Description *"
                                    HeaderTitle="Item Description *" SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                            RegexValidation="false" RegularExpression="" RegErrorMessage="" IsRequired="true"
                                            ReqErrorMessage="Item Description Required." />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:TextboxField>
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
