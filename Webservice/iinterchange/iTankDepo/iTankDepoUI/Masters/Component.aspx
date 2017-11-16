<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Component.aspx.vb" Inherits="Masters_Component" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="height: 100%; overflow :auto ">
    <div>
           <div style ="width :995px">
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh">Component</span>
                    </td>
                    <td align="right">
                        <nv:Navigation ID="navComponent" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tabdisplay" id="tabComponent">
            <div class="topspace">
            </div>
            <iFg:iFlexGrid ID="ifgComponent" runat="server" AutoGenerateColumns="False" AllowPaging="True" CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="25" AllowSearch="True" AutoSearch="True" AllowRefresh="True" StaticHeaderHeight="450px" ShowEmptyPager="True" Width="99%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1" EnableViewState="False" AddButtonText="Add Component" DeleteButtonText="Delete" GridLines="Both" HorizontalAlign="NotSet" PageSizerFormat="" Scrollbars="None" Type="Normal" ValidationGroup="tabComponent" DataKeyNames="CMPNNT_ID" OnAfterCallBack="onAfterCB" OnBeforeCallBack="" OnAfterClientRowCreated="setDefaultValues" Mode="Insert" AddRowsonCurrentPage="False" UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser">
                <Columns>
                    <iFg:TextboxField DataField="CMPNNT_CD" HeaderText="Code" HeaderTitle="Code" SortExpression="CMPNNT_CD" SortAscUrl="" SortDescUrl="">
                        <TextBox HelpText="22,COMPONENT_CMPNNT_CD" iCase="Upper" OnClientTextChange="" ValidationGroup="" CssClass="txt" ID="txtComponentCode">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true" IsRequired="true" ReqErrorMessage="Component code Required" RegexValidation="True" CustomValidation="True" CustomValidationFunction="validateCode" RegErrorMessage="Only Alphabets is Allowed." RegularExpression="^[a-zA-Z]+$" CsvErrorMessage="This Code already Exist." />
                        </TextBox>
                        <ItemStyle Width="170px" />
                    </iFg:TextboxField>
                    <iFg:TextboxField DataField="CMPNNT_DSCRPTN_VC" HeaderText="Description" HeaderTitle="Description" SortExpression="CMPNNT_DSCRPTN_VC" SortAscUrl="" SortDescUrl="">
                        <ItemStyle Width="310px" />
                        <TextBox ID="txtDescription" HelpText="23,COMPONENT_CMPNNT_DSCRPTN_VC" iCase="None" OnClientTextChange="" ValidationGroup="" CssClass="txt">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True" RegexValidation="True" RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are allowed." IsRequired="true" ReqErrorMessage="Component Description Required." />
                        </TextBox>
                    </iFg:TextboxField>
                    <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Equipment Type" HeaderTitle="Equipment Type" PrimaryDataField="EQPMNT_TYP_ID" SortExpression="EQPMNT_TYP_CD" SortAscUrl="" SortDescUrl="">
                        <Lookup ID="lkpChargeHeadType" CssClass="lkp" DependentChildControls="" HelpText="24,EQUIPMENT_TYPE_EQPMNT_TYP_CD" iCase="Upper" DoSearch="True" OnClientTextChange="" TableName="3" DataKey="EQPMNT_TYP_CD" ValidationGroup="" ClientFilterFunction="applyDepoFilter" AllowSecondaryColumnSearch="false" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnCaption="EQPMNT_TYP_ID" ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC" Hidden="False" />
                            </LookupColumns>
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                            <Validator CustomValidateEmptyText="False" IsRequired="True" Operator="Equal" ReqErrorMessage="Equipment Type required." Type="String" Validate="True" LkpErrorMessage="Invalid Equipment Type.Click on the List for Valid Values." />
                        </Lookup>
                        <ItemStyle Width="170px" />
                    </iFg:LookupField>
                    <iFg:TextboxField DataField="ASSMBLY" HeaderText="ASSEMBLY" HeaderTitle="ASSEMBLY" SortExpression="ASSMBLY" SortAscUrl="" SortDescUrl="">
                        <TextBox HelpText="25,COMPONENT_ASSMBLY" iCase="Upper" OnClientTextChange="" ValidationGroup="" CssClass="txt" MaxLength="3" ID="txtAssemblyCode">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true" IsRequired="false" ReqErrorMessage="" RegexValidation="True" CustomValidation="True" CustomValidationFunction="" RegErrorMessage="Only Alphabets And Numbers are Allowed" RegularExpression="^[a-zA-Z0-9]*$" />
                        </TextBox>
                        <ItemStyle Width="170px" />
                    </iFg:TextboxField>
                    <iFg:CheckBoxField DataField="ACTV_BT" HeaderText="Active" HeaderTitle="Active" HelpText="" SortAscUrl="" SortDescUrl="">
                        <ItemStyle Width="80px" />
                    </iFg:CheckBoxField>
                </Columns>
                <RowStyle CssClass="gitem" />
                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                <PagerStyle CssClass="gpage" HorizontalAlign="Center" />
                <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                <SelectedRowStyle CssClass="gsitem" />
                <AlternatingRowStyle CssClass="gaitem" />
                <ActionButtons>
                    <iFg:ActionButton ID="act_Upload" Text="Upload" ValidateRowSelection="False" OnClientClick="openUpload();" IconClass="icon-upload-alt" CSSClass="btn btn-small btn-info" />
                    <iFg:ActionButton ID="aBtExport" Text="Export" ValidateRowSelection="False" OnClientClick="exportToExcel();" IconClass="icon-download-alt" CSSClass="btn btn-small btn-info" />
                </ActionButtons>
            </iFg:iFlexGrid>
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();" onClientPrint="null" />
    <asp:HiddenField ID="hdnPageTitle" runat="server" />
    </form>
</body>
</html>
