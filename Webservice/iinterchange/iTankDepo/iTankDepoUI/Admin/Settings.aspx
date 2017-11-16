<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Settings.aspx.vb" Inherits="Admin_Settings" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" >
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Settings</span>
                </td>
               <%-- <td align="right">
                  <nv:Navigation ID="navSettings" runat="server" />
                </td>--%>
            </tr>
        </table>
    </div>
    <div class="tabdisplayGatePass" id="tabSettings" style="overflow-y: auto;overflow-x: auto; height: auto">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width:100%:">
            <tr>
                <td>
                    <table border="0" height="50px" cellpadding="2" align="Left" cellspacing="2" class="tblstd"
                        align="center">
                        <tr>
                            <td>
                                <label id="lblDepotCode" runat="server" class="lbl">
                                    * Depot</label>
                            </td>
                            <td>
                                <Inp:iLookup ID="lkpDepotCode" runat="server" CssClass="lkp" DataKey="DPT_CD" DoSearch="True"
                                    iCase="Upper" MaxLength="10" TabIndex="1" TableName="29" HelpText="" ClientFilterFunction="filterHQDepot">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="DPT_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnName="DPT_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                        <Inp:LookupColumn ColumnCaption="Name" ColumnName="DPT_NAM" Hidden="False" />
                                    </LookupColumns>
                                    <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                        IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                    <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Depot. Click on the list for valid values"
                                        Operator="Equal" ReqErrorMessage="Depot Required" Type="String" Validate="True"
                                        ValidationGroup="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                </Inp:iLookup>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="3">
                                <span align="center"><a href="#" id="lnkFetch" onclick="onFetchClick();return false;">
                                    Fetch</a></span> <span align="center"><a href="#" id="lnkReset" onclick="onResetClick();return false;">
                                        Reset</a></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div style="margin-top: -5px; width: 100%; height: 20px;">
                    </div>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width:100%;">
            <tr style="height: 100%;">
                <td style="height: 100%; width: 100%">
                    <div id="divDetail" style="margin: 1px; width: 100%; height: 100%; vertical-align: middle">
                        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                            font-family: Arial; font-size: 8pt; display: none; width: 100%; height: 80%;"
                            align="center">
                            <div>
                                No Records Found.</div>
                        </div>
                         <!-- UIG Issue Fix (IE)- Issue No:4 - Page alignment is not proper  -->
                     <%--   <div id="divConfigurationTemplate" style="vertical-align: middle; width: 100%; height: 400px;
                            overflow: auto; display: none;" class="tabdisplayGatePass">--%>
                            <div class="tabdisplayGatePass" id="divConfigurationTemplate">
                            <div class="topspace">
                            </div>
                            <iFg:iFlexGrid ID="ifgConfigurationTemplate" runat="server" AllowStaticHeader="True"
                                DataKeyNames="CNFG_TMPLT_ID" Width="100%" CaptionAlign="NotSet" GridLines="Both"
                                HeaderRows="1" HorizontalAlign="Justify" PageSizerFormat="" 
                                RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="220px" Type="Normal"
                                ValidationGroup="divDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                                EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                                PageSize="10" AddRowsonCurrentPage="False" AllowAdd="False" AllowDelete="False"
                                ShowFooter="True" OnAfterCallBack="ifgConfigurationTemplateOnAfterCallBack" AllowPaging="True">
                                <PagerStyle CssClass="gpage" />
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <Columns>
                                    <iFg:BoundField DataField="KY_NAM" HeaderText="Code" HeaderTitle="Name" IsEditable="False"
                                        ReadOnly="True" SortAscUrl="" SortDescUrl="">
                                        <ItemStyle Width="30px" Wrap="False" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="KY_DSCRPTION" HeaderText="Description" HeaderTitle="Description"
                                        IsEditable="False" ReadOnly="True" SortAscUrl="" SortDescUrl="">
                                        <ItemStyle Width="600px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="KY_VL" HeaderText="Value" HeaderTitle="Value"
                                        SortAscUrl="" SortDescUrl="">
                                        <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                            MaxLength="500">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                RegexValidation="False" LookupValidation="False" IsRequired="False" RegularExpression="^[a-zA-Z0-9]*$"
                                                RegErrorMessage="Only Alphabets And Numbers are Allowed" />
                                        </TextBox>
                                        <ItemStyle Width="250px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:BoundField DataField="CNFG_TYP" HeaderText="Type" HeaderTitle="Type" IsEditable="False"
                                        ReadOnly="True" SortAscUrl="" SortDescUrl="">
                                        <ItemStyle Width="50px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:CheckBoxField DataField="ENBLD_BT" HeaderText="Enabled" HeaderTitle="Select"
                                        HelpText="" SortAscUrl="" SortDescUrl="" Visible="True">
                                        <ItemStyle Width="30px" Wrap="True" />
                                    </iFg:CheckBoxField>
                                </Columns>
                                <FooterStyle CssClass="gftr" HorizontalAlign="Left" Width ="100%" />
                                <SelectedRowStyle CssClass="gsitem" />
                                <AlternatingRowStyle CssClass="gaitem" />
                                <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                    IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                            </iFg:iFlexGrid>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div width="100%">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="center">
            <tr>
                <td>
                    <div class="button" style="width: 70px" runat="server">
                        <ul>
                            <li class="btncorner" data-corner="8px">
                                <button id="btnSubmit" class="btn" onmouseover="this.className='sbtn'" onmouseout="this.className='btn'"
                                    onclick="submitPage();return false;">
                                    Submit</button></li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
