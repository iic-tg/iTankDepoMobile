<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportParameter.aspx.vb" Inherits="ReportParameter" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="overflow: auto">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh"></span>
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
    </div>
    <div id="tabReportParameter" style="height:auto">
        <table style="height: 90%; width: 100%">
            <tr>
                <td style="height: 100%; width:20%" valign="top" rowspan="2">
                <!-- UIG Fix -->
                    <div id="divParamGrid" runat="server" style="height: 100%;">
                        <iFg:iFlexGrid ID="ifgParamList" runat="server" AllowAdd="False" AllowDelete="False" AllowFilter="False" AllowStaticHeader="true" AutoGenerateColumns="False" CaptionAlign="NotSet" CssClass="tblstd" GridLines="Both" HeaderRows="1" HorizontalAlign="NotSet" OnAfterClientRowCreated="" OnBeforeCallBack="" OnBeforeClientRowCreated="" OnClientCollapse="" OnClientExpand="" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" ShowEmptyPager="False" StaticHeaderHeight="500px" Type="Normal" ValidationGroup="" CellPadding="2" UseCachedDataSource="True" Width="200px" DataKeyNames="prmtr_id" Scrollbars="Auto" ShowFooter="False">
                            <Columns>
                                <iFg:CheckBoxField DataField="Checked" HelpText="" SortExpression="" AllowSearch="False" HeaderTitle="Select" HeaderText="Select">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </iFg:CheckBoxField>
                                <iFg:TextboxField DataField="prmtr_dsply_nam" HeaderText="Filters" SortExpression="" ReadOnly="true" IsEditable="False" HeaderTitle="">
                                    <TextBox HelpText="" iCase="None" OnClientTextChange="" CssClass="txt" ValidationGroup="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                    </TextBox>
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="prmtr_typ" Visible="False" HeaderTitle="">
                                    <TextBox HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                    </TextBox>
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="prmtr_val" Visible="False" HeaderTitle="">
                                    <TextBox HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                    </TextBox>
                                </iFg:TextboxField>
                            </Columns>
                            <FooterStyle HorizontalAlign="Center" />
                            <RowStyle CssClass="gitem"  />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                        </iFg:iFlexGrid>
                    </div>
                </td>
                <td style="height: 100%;" valign="top" rowspan="2">
                    <div id="masterdiv" style="display: none; width: 100%;">
                        <iFg:iFlexGrid ID="ifgParamValue" runat="server" AllowAdd="False"  
                            AutoGenerateColumns="False" AllowDelete="False" AllowEdit="True" 
                            AllowStaticHeader="True" CaptionAlign="NotSet" CssClass="tblstd" 
                            GridLines="Both" HeaderRows="1" HorizontalAlign="NotSet" 
                            OnAfterClientRowCreated="" OnBeforeCallBack="" OnBeforeClientRowCreated="" 
                            OnClientCollapse="" OnClientExpand="" PageSizerFormat="" 
                            RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" 
                            ShowEmptyPager="True" StaticHeaderHeight="370px" Type="Normal" 
                            ValidationGroup="" AllowPaging="True" Width="100%" AllowSearch="True" 
                            UseCachedDataSource="True" DataKeyNames="ID" AllowSorting="True" 
                            AutoSearch="True" PageSize="25" OnAfterCallBack="fnSetDisplay" 
                            AllowRefresh="True" UseIcons="true" SearchButtonIconClass="icon-search" 
                            SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus" 
                            AddButtonCssClass="btn btn-small btn-success" 
                            DeleteButtonIconClass="icon-trash" 
                            DeleteButtonCssClass="btn btn-small btn-danger" 
                            RefreshButtonIconClass="icon-refresh" 
                            RefreshButtonCssClass="btn btn-small btn-info" 
                            SearchCancelButtonIconClass="icon-remove" 
                            SearchCancelButtonCssClass="btn btn-small btn-danger" 
                            ClearButtonIconClass="icon-eraser" 
                            ClearButtonCssClass="btn btn-small btn-success" 
                            UseActivitySpecificDatasource="True">
                            <Columns>
                                <iFg:CheckBoxField DataField="Checked" HeaderText="Select" SortExpression="" AllowSearch="False" HeaderTitle="Select">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </iFg:CheckBoxField>
                                <iFg:TextboxField DataField="Code" HeaderText="Code" SortExpression="" IsEditable="False" HeaderTitle="" ReadOnly="true">
                                    <TextBox HelpText="" iCase="None" OnClientTextChange="" CssClass="txt" ValidationGroup="">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                    </TextBox>
                                    <ItemStyle Width="20%" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="Description" Visible="true" HeaderText="Description" IsEditable="False" HeaderTitle="Description">
                                    <TextBox HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="" ReadOnly="true">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                    </TextBox>
                                    <ItemStyle Width="50%" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </iFg:TextboxField>
                            </Columns>
                            <FooterStyle HorizontalAlign="left" CssClass="gftr" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <PagerStyle CssClass="gpage" HorizontalAlign="Center" />
                        </iFg:iFlexGrid>
                    </div>
                    <div id="Paramdiv" style="display: none; width: 100%;">
                        <br />
                        <div style="float: left; margin-left: 0px;">
                            <Inp:iLabel ID="lblParameterEnter" runat="server" CssClass="blbl" Font-Underline="true"></Inp:iLabel>
                        </div>
                        <br />
                        <div style="display: none; float: right; width: 100%; margin-left: 70px;" id="Datdiv">
                            <Inp:iDate ID="datParam" runat="server" CssClass="dat" OnClientTextChange="SetDateParam">
                                <Validator CustomValidateEmptyText="False" Operator="Equal" Type="Date" CustomValidation="False" CustomValidationFunction="valdiateToDate" Validate="True" ReqErrorMessage="Date Required" IsRequired="false" LkpErrorMessage="Invalid Date. Click on the calendar icon" />
                                <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            </Inp:iDate>
                        </div>
                        <div style="display: none; float: right; width: 100%; margin-left: 70px;" id="Strdiv">
                            <Inp:iTextBox ID="txtstrParam" runat="server" CssClass="txt" OnClientTextChange="SetSingleParam">
                                <Validator Validate="true" CustomValidation="false" CustomValidateEmptyText="True" Operator="Equal" Type="String" CustomValidationFunction="" CsvErrorMessage="Container" />
                            </Inp:iTextBox>
                        </div>
                        <div style="display: none; float: right; width: 100%; margin-left: 70px" id="Intdiv">
                            <Inp:iTextBox ID="txtintParam" runat="server" CssClass="txt" iCase="Numeric" OnClientTextChange="SetSingleParam">
                                <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                            </Inp:iTextBox>
                        </div>
                        <div style="display: none; float: right; width: 100%; margin-left: 70px" id="DropdownDiv">
                            <select id="ddlParameter" class="sddl" onchange="SetDdlParam();" style="width: 140px; padding-right: auto" param="">
                            </select>
                        </div>
                        <br />
                        <br />
                        <br />
                        
                    </div>
                    <div class="HSParent">
                            <div id="divHelpStrip">
                            </div>
                        </div>
                    <asp:ValidationSummary ID="ValidationSummary1" CssClass="rptvals" runat="server" />
                </td>
            </tr>
        </table>
        
    </div>
    <table class="button" align="center" style="width: 150px">
            <tr>
                <td align="center">
                    <ul style="margin: 0px;">
                        <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnRunReport" style="text-decoration: none; color: White; font-weight: bold;" class="icon-print" runat="server" onclick="RunReport_Click();return false;">&nbsp;Run Report</a></li>
                    </ul>
                </td>
            </tr>
        </table>
    <asp:HiddenField ID="hdnKeyName" runat="server" />
    <asp:HiddenField ID="hdnReportName" runat="server" />
    <asp:HiddenField ID="hdnReportPath" runat="server" />
    <asp:HiddenField ID="hdnReportID" runat="server" />
    <asp:HiddenField ID="hdnSysDate" runat="server" />
    </form>
</body>
</html>
