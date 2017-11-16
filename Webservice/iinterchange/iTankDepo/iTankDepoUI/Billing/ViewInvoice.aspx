<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewInvoice.aspx.vb" Inherits="Billing_ViewInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server" autocomplete="off" style="overflow: auto">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Billing >> View Invoice</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navExRate" runat="server" />
                </td>
            </tr>
        </table>
    </div>


    <div class="" id="tabViewInvoice" style="overflow-y: auto;overflow-x: auto; height: auto">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width: 100%;">
            <tr>
                <td>
                    <div id="divRecordNotFound" style="display: none; font-weight: bold;" align="center">
                        There are no Draft or Final Invoice(s) generated to be viewed.
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divViewInvoice" style="display: none;">
                        <iFg:iFlexGrid ID="ifgViewInvoice" runat="server" AllowStaticHeader="True" DataKeyNames="INVC_BIN"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
                            ValidationGroup="divViewInvoice" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            PageSize="25" AddRowsonCurrentPage="False" AllowPaging="True" OnAfterCallBack=""
                            AllowDelete="False" AllowRefresh="false" AllowSearch="True" AutoSearch="True"
                            UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                            DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                            SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="btn btn-small btn-info"
                            ClearButtonIconClass="icon-eraser" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            AllowAdd="False">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:BoundField DataField="DPT_CD" HeaderText="Depot Code" HeaderTitle="Depot Code"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="40px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="INVC_NM" HeaderText="Invoice Type" HeaderTitle="Invoice Type"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="40px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </iFg:BoundField>
                                <iFg:LookupField DataField="SRVC_PRTNR_CD" ForeignDataField="SRVC_PRTNR_ID" HeaderText="Customer / Invoicing Party"
                                    HeaderTitle="Customer / Invoicing Party" PrimaryDataField="SRVC_PRTNR_ACTL_ID"
                                    SortAscUrl="" SortDescUrl="" SortExpression="" IsEditable="False" ReadOnly="True">
                                    <Lookup DataKey="SRVC_PRTNR_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                        TableName="59" ValidationGroup="divMiscDetail" ID="lkpInvoicingTo" CssClass="lkp"
                                        DoSearch="True" Width="150px" ClientFilterFunction="" OnClientTextChange="">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="SRVC_PRTNR_ACTL_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="SRVC_PRTNR_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="SRVC_PRTNR_NAM" Hidden="False" />
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="280px"
                                            HorizontalAlign="Center" />
                                        <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Invoicing Party / Customer. Click on the List for valid values"
                                            ReqErrorMessage="Invoicing Party / Customer Required" Validate="False" IsRequired="True"
                                            CustomValidation="false" CustomValidationFunction="" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                    <ItemStyle Width="70px" />
                                </iFg:LookupField>
                                <iFg:BoundField DataField="INVC_NO" HeaderText="Invoice No" HeaderTitle="Invoice No"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="50px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%"/>
                                </iFg:BoundField>
                                <iFg:DateField DataField="FRM_BLLNG_DT" HeaderText="From Billing Date" HeaderTitle="From Billing Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid From Billing Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="From Billing Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="divViewInvoice" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:DateField DataField="TO_BLLNG_DT" HeaderText="To Billing Date" HeaderTitle="To Billing Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid To Billing Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="To Billing Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="divViewInvoice" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:BoundField DataField="BLLNG_FLG" HeaderText="Billing Type" HeaderTitle="Billing Type"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="60px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </iFg:BoundField>
                                <iFg:BoundField DataField="MDFD_BY" HeaderText="Generated By" HeaderTitle="Generated By"
                                    IsEditable="False" SortAscUrl="" SortDescUrl="">
                                    <ItemStyle Width="70px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </iFg:BoundField>
                                <iFg:DateField DataField="MDFD_DT" HeaderText="Generated Date" HeaderTitle="Generated Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    ReadOnly="true">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                            LkpErrorMessage="Invalid Generated Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Generated Date Required" Validate="True" RangeValidation="false"
                                            CompareValidation="false" ValidationGroup="divViewInvoice" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    <ItemStyle Width="80px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:DateField>
                                <iFg:ImageField HeaderTitle="PDF" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/pdf.png"
                                    HeaderText="PDF" HeaderImageUrl="">
                                    <HeaderStyle HorizontalAlign="Left" Width="5%"></HeaderStyle>
                                    <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Center" />
                                </iFg:ImageField>
                                <iFg:ImageField HeaderTitle="XLS" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/excel.png"
                                    HeaderText="XLS" HeaderImageUrl="">
                                    <HeaderStyle HorizontalAlign="Left"  Width="5%"></HeaderStyle>
                                    <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Center" />
                                </iFg:ImageField>
                                <iFg:ImageField HeaderTitle="DOC" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/word.png"
                                    HeaderText="DOC" HeaderImageUrl="">
                                    <HeaderStyle HorizontalAlign="Left"  Width="5%"></HeaderStyle>
                                    <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Center" />
                                </iFg:ImageField>
                                <iFg:ImageField HeaderTitle="Finalize" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/final.png"
                                    HeaderText="Finalize" HeaderImageUrl="">
                                    <HeaderStyle HorizontalAlign="Left"  Width="5%"></HeaderStyle>
                                    <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Center" />
                                </iFg:ImageField>
                                 <iFg:ImageField HeaderTitle="Cancel" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/Cancel.png"
                                    HeaderText="Cancel" HeaderImageUrl="">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Wrap="True" HorizontalAlign="Center" />
                                </iFg:ImageField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                <ActionButtons>
                               <iFg:ActionButton ID="acnRefresh" Text="Refresh" ValidateRowSelection="False"
                                    OnClientClick="bindGrid();" IconClass="icon-refresh" CSSClass="btn btn-small btn-info" /> 
                                </ActionButtons>
                        </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <a id="hiddenLinkViewInvoice" href="#" style="display: none" target="_blank" />
    <asp:HiddenField ID="hdnRemarks" runat="server" />
    </form>
</body>
</html>
