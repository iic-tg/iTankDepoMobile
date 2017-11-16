<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DynamicReportParameter.aspx.vb"
    Inherits="DynamicReport_DynamicReportParameter" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>

<body onload="ReportParameterLoadPage();">
    <form id="form1" runat="server" autocomplete="off">
    <div style="height: 440px">
        <div>
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh"></span>
                    </td>
                    <td align="right">
                        <nv:Navigation ID="navDynamicReports" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tabdisplayActivityConfigure" id="tabReport" style="height: 375px">
            <center>
                <table style="width: 80%; height: 10%">
                    <tr>
                        <td style="width: 20%">
                            <Inp:iLabel ID="lblRepotTitle" CssClass="lbl" runat="server" Font-Bold="true">Report Title</Inp:iLabel>
                        </td>
                        <td style="width: 75%" align="left">
                            <Inp:iTextBox ID="txtReportTitle" runat="server" CssClass="txt" MaxLength="20" TabIndex="1"
                                iCase="Upper" HelpText="" Width="300px">
                                <Validator Type="String" Operator="Equal" CustomValidateEmptyText="True" IsRequired="True"
                                    ReqErrorMessage="Report Title Required." Validate="True" CustomValidation="True"
                                    CustomValidationFunction="validateReportTitle"></Validator>
                            </Inp:iTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <Inp:iLabel ID="lblNoParameterRequired" Visible="false" CssClass="lbl" runat="server"
                                Font-Bold="true">Click 'Run Report' to view.</Inp:iLabel>
                        </td>
                    </tr>
                </table>
            </center>
            <table style="height: 90%; width: 100%">
                <tr>
                    <td>
                        <Inp:iLabel ID="lblReportColumns" runat="server" Font-Bold="true" Font-Underline="true"
                            CssClass="lbl">Report Filters</Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iLabel ID="lblSelectedParameter" runat="server" Font-Underline="true" Font-Bold="True"
                            CssClass="lbl"></Inp:iLabel>
                    </td>
                </tr>
                <tr>
                    <td style="height: 100%">
                        <div id="divParamGrid" runat="server" style="height: 100%">
                            <iFg:iFlexGrid ID="ifgParameterList" runat="server" AllowAdd="False" AllowDelete="False"
                                AllowFilter="False" AllowStaticHeader="True" AutoGenerateColumns="False" CaptionAlign="NotSet"
                                CssClass="tblstd" GridLines="Both" HeaderRows="1" HorizontalAlign="NotSet" OnAfterCallBack="Param_Reload"
                                OnAfterClientRowCreated="" OnBeforeCallBack="" OnBeforeClientRowCreated="" OnClientCollapse=""
                                OnClientExpand="" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                ShowEmptyPager="False" StaticHeaderHeight="250px" Type="Normal" ValidationGroup=""
                                CellPadding="2" UseCachedDataSource="True" Width="200px" DataKeyNames="parameter"
                                UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                                AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                                SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                                ClearButtonCssClass="btn btn-small btn-info" ClearButtonIconClass="icon-eraser"
                                ShowFooter="false">
                                <Columns>
                                    <iFg:CheckBoxField DataField="Checked" HeaderText="All" HelpText="" SortExpression=""
                                        AllowSearch="False" HeaderTitle="" HeaderImageUrl="../Images/flrsel.gif">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle HorizontalAlign="center" />
                                    </iFg:CheckBoxField>
                                    <iFg:HyperLinkField HeaderText="Filters" HeaderTitle="Filters" IsEditable="False"
                                        MaxLength="0" ReadOnly="True" SortAscUrl="" SortDescUrl="" Text="" HeaderStyle-Wrap="false">
                                        <ItemStyle Width="150px" Wrap="true" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </iFg:HyperLinkField>
                                    <iFg:TextboxField DataField="type" Visible="False" HeaderTitle="">
                                        <TextBox HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField DataField="parameter" Visible="False" HeaderTitle="">
                                        <TextBox HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField DataField="parametervalue" Visible="False" HeaderTitle="">
                                        <TextBox HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                        </TextBox>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </iFg:TextboxField>
                                </Columns>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle CssClass="ghdr" />
                                <RowStyle CssClass="gitem" />
                                <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                <SelectedRowStyle CssClass="gsitem" />
                            </iFg:iFlexGrid>
                        </div>
                    </td>
                    <td style="height: 100%" valign="top">
                        <div id="divRecordNotFound" runat="server" style="font-style: italic; font-family: Arial;
                            font-size: 8pt; display: none; height: 275px; width: 400px; vertical-align: bottom;
                            text-align: left;" align="left">
                            <Inp:iLabel ID="lblListRowCount" runat="server" Visible="true">No Records Found</Inp:iLabel>
                        </div>
                        <div id="masterdiv" style="display: block;">
                            <iFg:iFlexGrid ID="ifgParameter" runat="server" AllowAdd="False" AllowDelete="False"
                                AllowEdit="True" AllowStaticHeader="True" CaptionAlign="NotSet" CssClass="tblstd"
                                GridLines="Both" HeaderRows="1" HorizontalAlign="NotSet" OnAfterCallBack="onAfterCallBack"
                                OnAfterClientRowCreated="" OnBeforeCallBack="" OnBeforeClientRowCreated="" OnClientCollapse=""
                                OnClientExpand="" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                ShowEmptyPager="False" StaticHeaderHeight="275px" Type="Normal" ValidationGroup=""
                                AllowPaging="True" Width="750px" AllowSearch="True" UseCachedDataSource="True"
                                DataKeyNames="ID" AllowSorting="True" AutoSearch="True" ShowPageSizer="False"
                                AllowRefresh="True" UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                                AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                                SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                                ClearButtonCssClass="btn btn-small btn-info" ClearButtonIconClass="icon-eraser">
                                <PagerStyle CssClass="gpage" />
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                                <SelectedRowStyle CssClass="gsitem" />
                                <AlternatingRowStyle CssClass="gaitem" />
                                <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                    IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            </iFg:iFlexGrid>
                        </div>
                        <div id="Paramdiv" style="display: none; width: 400px;">
                            <br />
                            <div style="float: left; padding-left: 10px;">
                                <Inp:iLabel ID="lblParameterEnter" runat="server" Font-Bold="False" CssClass="lbl"></Inp:iLabel>&nbsp;
                            </div>
                            <div style="display: none; float: left" id="Datdiv">
                                <Inp:iDate ID="datParam" runat="server" CssClass="dat" OnClientTextChange="SetDateParam">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid Date. Click on the calendar icon"
                                        Validate="True" ReqErrorMessage="Date Required" />
                                    <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iDate>
                            </div>
                            <div style="display: none;" id="Strdiv">
                                <Inp:iTextBox ID="txtstrParam" runat="server" CssClass="txt" OnClientTextChange="SetSingleParam">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                </Inp:iTextBox>
                            </div>
                            <div style="display: none;" id="Intdiv">
                                <Inp:iTextBox ID="txtintParam" runat="server" CssClass="txt" iCase="Numeric" OnClientTextChange="SetSingleParam">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" />
                                </Inp:iTextBox>
                            </div>
                             <div style="display: none" id="DropdownDiv">
                            <select id="ddlParameter" class="sddl" onchange="SetDdlParam();" style="width: 140px; padding-right: auto" param="">
                            </select>
                        </div>
                            <br />
                            &nbsp;<div class="HSParent">
                                <div class="HelpStrip" id="divHelpStrip">
                                </div>
                            </div>
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 10px">
        </div>
        <div align="center">
            <table align="center">
                <tr>
                    <td>
                        <ul style="margin: 0px;">
                            <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnReset" style="text-decoration: none;
                                color: White; font-weight: bold;" class="icon-eraser" runat="server" onclick="ResetParams();return false;">
                                Reset</a></li>
                        </ul>
                    </td>
                    <td>
                        <ul style="margin: 0px;">
                            <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnRunReport"
                                style="text-decoration: none; color: White; font-weight: bold;" class="icon-print"
                                runat="server">Run Report</a></li>
                        </ul>
                    </td>
                    <td>
                        <ul style="margin: 0px;">
                            <li class="btn btn-small btn-success"><a href="#" data-corner="8px" id="btnSubmit"
                                style="text-decoration: none; color: White; font-weight: bold;" class="icon-save"
                                runat="server">Save</a></li>
                        </ul>
                    </td>
                </tr>
            </table>
        </div>
        <div style="display: none">
            <sp:SubmitPane ID="PageSubmitPane" runat="server" TabIndex="14" onClientSubmit="SaveParams()" />
        </div>
    </div>
    <input type="hidden" runat="server" id="rhid" />
    </form>
</body>
</html>
