<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Supplier.aspx.vb" Inherits="Masters_Supplier" %>

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
                    <span id="spnHeader" class="ctabh">Master >> Supplier</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navCustomer" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="tabdisplayGatePass" id="tabSupplier" style="overflow-y: auto; overflow-x: auto;height:auto">
        <table border="0" cellpadding="2" cellspacing="2">
            <tr>
                <td>
                    <table border="0" height="50px" cellpadding="2" align="Left" cellspacing="2" class="tblstd"
                        align="center">
                        <tr>
                            <td>
                                <label id="lblCode" runat="server" class="lbl">
                                    Code
                                </label>
                                <Inp:iLabel ID="Code" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtCode" runat="server" CssClass="txt" TabIndex="1"
                                    HelpText="446,SUPPLIER_SPPLR_CD" TextMode="SingleLine" iCase="Upper" ToolTip="Code">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets and Numbers are allowed"
                                        RegularExpression="^[a-zA-Z0-9]+$" Type="String" Validate="True" ValidationGroup="tabSupplier"
                                        LookupValidation="False" CsvErrorMessage="This Code Already Exists" CustomValidationFunction="validateSupplierCode"
                                        IsRequired="true" ReqErrorMessage="Code Required" CustomValidation="True"
                                        RegexValidation="true" />
                                </Inp:iTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <Inp:iLabel ID="lblDescription" runat="server" ToolTip="Description" CssClass="lbl">
Description</Inp:iLabel>
                                <Inp:iLabel ID="Description" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                            </td>
                            <td>
                                <Inp:iTextBox ID="txtDescription" runat="server" TabIndex="2" CssClass="txt" ToolTip="Description"
                                    HelpText="447,SUPPLIER_SPPLR_DSCRPTN_VC" Height="" TextMode="SingleLine" Width="">
                                    <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="True"
                                        ControlToCompare="" CsvErrorMessage="" CustomValidationFunction="" IsRequired="True"
                                        ReqErrorMessage="Description Required" ValidationGroup="tabSupplier" Validate="True"
                                        RegErrorMessage="" RegexValidation="false" RegularExpression=""></Validator>
                                </Inp:iTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <label id="lblActive" runat="server" class="lbl">
                                    Active</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActive" runat="server" Width="192px" TabIndex="3" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="divSupplierDetail">
          <fieldset class="lbl" id="fldSupplier">
           <legend class="blbl">Contract Details</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr >
                    <td>
                        <div id="divContractDetails" class="tabdisplay" style="width: 100%;">
                            <iFg:iFlexGrid ID="ifgContractDetails" runat="server" AllowStaticHeader="True" DataKeyNames="SPPLR_CNTRCT_DTL_ID"
                                Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="180px" Type="Normal"
                                ValidationGroup="tabSupplier" UseCachedDataSource="True" AutoGenerateColumns="False"
                                EnableViewState="False" OnAfterClientRowCreated="" PageSize="10" AddRowsonCurrentPage="False"
                                ShowPageSizer="True" OnAfterCallBack="onAfterCB" OnBeforeCallBack="" AllowPaging="False"
                                UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                                AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btnmcorner btn-small btn-primary"
                                SearchCancelButtonIconClass="icon-remove" 
                                ClearButtonCssClass="icon-eraser" AllowSearch="False" AutoSearch="True" AllowSorting="True">
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                  <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <Columns>
                                    <iFg:TextboxField DataField="CNTRCT_RFRNC_NO" HeaderText="Contract Ref No *"
                                        HeaderTitle="Contract Ref No *" SortAscUrl="" SortDescUrl="" SortExpression="">
                                        <TextBox ID="txtContractRefNo" CssClass="txt" HelpText="448,SUPPLIER_CONTRACT_DETAIL_CNTRCT_RFRNC_NO" OnClientTextChange="" ValidationGroup="tabSupplier"
                                            ReadOnly="False" TabIndex ="4">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                RegexValidation="True" RegularExpression="^[a-zA-Z0-9-_/]+$" RegErrorMessage="Only Alphabets/Numbers and -/_ are allowed." IsRequired="true"
                                                ReqErrorMessage="Contract Reference No Required" CustomValidation ="true" CustomValidationFunction ="checkDuplicateContractRef" />
                                        </TextBox>
                                         <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="150px" Wrap="true" />
                                    </iFg:TextboxField>
                                    <iFg:DateField DataField="CNTRCT_STRT_DT" HeaderText="Contract Start Date *" HeaderTitle="Contract Start Date *"
                                        SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false" AllowSearch="true" SortExpression="">
                                        <iDate HelpText="449" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                            ValidationGroup="tabSupplier" MaxLength="11" CssClass="txt" Width="100px" TabIndex ="5">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="true"
                                                LkpErrorMessage="Invalid Contract Start Date. Click on the calendar icon for valid values"
                                                ReqErrorMessage="Contract Start Date Required" Validate="True" CompareValidation="True"
                                                CustomValidation="True" CmpErrorMessage="Invalid Contract Start Date cannot be greater than current Date."
                                                CustomValidationFunction="ValidateStartDate" />
                                        </iDate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="120px" Wrap="True" />
                                    </iFg:DateField>
                                    <iFg:DateField DataField="CNTRCT_END_DT" HeaderText="Contract End Date *" HeaderTitle="Contract End Date *"
                                        SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false" AllowSearch="true" SortExpression="">
                                        <iDate HelpText="450" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                            ValidationGroup="tabSupplier" MaxLength="11" CssClass="txt" Width="100px" TabIndex ="6">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="true"
                                                LkpErrorMessage="Invalid Contract End Date. Click on the calendar icon for valid values"
                                                ReqErrorMessage="Contract End Date Required" Validate="True" CompareValidation="True"
                                                CustomValidation="True" CmpErrorMessage="Invalid Contract End Date cannot be greater than Current Date."
                                                CustomValidationFunction="ValidateEndDate" />
                                        </iDate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="120px" Wrap="True" />
                                    </iFg:DateField>
                                    <iFg:TextboxField DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                                        SortAscUrl="" SortDescUrl="" SortExpression="">
                                        <TextBox ID="txtRemarks" CssClass="txt" HelpText="452,SUPPLIER_CONTRACT_DETAIL_RMRKS_VC" OnClientTextChange="" ValidationGroup="tabSupplier"
                                            ReadOnly="False" TabIndex ="8" MaxLength ="500">
                                            <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="Equal" ReqErrorMessage=" "
                                                Type="String" />
                                        </TextBox>
                                         <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="300px" Wrap="true" />
                                    </iFg:TextboxField>
                                <iFg:HyperLinkField HeaderText="Equipment Details" HeaderTitle="Equipment Details" SortAscUrl=""
                                                SortDescUrl="" Text="Add" IsEditable="False" ReadOnly="True" >
                                                  <ItemStyle Width="60px" Wrap="true" HorizontalAlign="Center"  />
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </iFg:HyperLinkField>
                                </Columns>
                                 <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                                <SelectedRowStyle CssClass="gsitem" />
                                <AlternatingRowStyle CssClass="gaitem" />
                                <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                    IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            </iFg:iFlexGrid>
                        </div>
                       
                    </td>
                </tr>
            </table>
               </fieldset>
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" align="center"
        onClientPrint="null" />
    <asp:HiddenField ID="hdnSupplierId" runat="server" />
    </form>
</body>
</html>
