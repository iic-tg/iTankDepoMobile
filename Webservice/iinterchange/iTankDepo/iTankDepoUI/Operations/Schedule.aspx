<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Schedule.aspx.vb" Inherits="Operations_Schedule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;
            margin: 0px;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Operations >> Schedule</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navChangeOfStatus" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="tabScheduling" style="height:auto">
        <div id="divScheduleSearch">
            <fieldset class="lbl" id="fldSearch">
                <legend class="blbl">Search</legend>
                <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width: 100%;">
                    <tr>
                        <td>
                            <Inp:iLabel ID="lblActivity" runat="server" CssClass="lbl">
                   Activity
                            </Inp:iLabel>
                            <Inp:iLabel ID="ILabel2" runat="server" CssClass="lblReq">
                   *
                            </Inp:iLabel>
                        </td>
                        <td>
                            <Inp:iLookup ID="lkpSchedulingType" runat="server" CssClass="lkp" DataKey="ENM_CD" AutoSearch="true"
                                DoSearch="true"  TabIndex="1"  iCase="Upper" HelpText="549" ReadOnly="false" TableName="82">
                                <LookupColumns>
                                    <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                    <Inp:LookupColumn ColumnName="ENM_CD" Hidden="False" ColumnCaption="Code" />                                   
                                  
                                </LookupColumns>
                                <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                    IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="true" ReqErrorMessage="Activity Required."
                                    LkpErrorMessage="Invalid Activity. Click on the list for valid values" LookupValidation="true"
                                    Operator="Equal" Type="String" Validate="True" ValidationGroup="divSchedule" />
                                <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                    IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            </Inp:iLookup>
                        </td>
                        <td>
                        </td>
                        <td>
                            <Inp:iLabel ID="lblSchedule" runat="server" CssClass="lbl">
                   Schedule
                            </Inp:iLabel>
                             <Inp:iLabel ID="ILabel1" runat="server" CssClass="lblReq">
                   *
                            </Inp:iLabel>
                        </td>
                        <td>
                            <Inp:iLookup ID="lkpSchedule" runat="server" CssClass="lkp" DataKey="ENM_CD" OnClientTextChange=""
                                DoSearch="True" iCase="Upper" TabIndex="2" TableName="83" HelpText="550" ClientFilterFunction=""
                                ReadOnly="false">
                                <LookupColumns>
                                    <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                    <Inp:LookupColumn ColumnName="ENM_CD" Hidden="False" ColumnCaption="Code" />
                                </LookupColumns>
                                <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                    IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="TRUE" ReqErrorMessage="Schedule Required."
                                    LkpErrorMessage="Invalid Schedule. Click on the list for valid values"
                                    Operator="Equal" Type="String" Validate="True" ValidationGroup="divSchedule" />
                                <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                    IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            </Inp:iLookup>
                        </td>
                        <td>
                        </td>
                        <td>
                            <ul style="margin: 0px;">
                                <li class="btn btn-small btn-info" style="width: 100px; height: 14px;"><a href="#"
                                    data-corner="8px" id="btnSearch" style="text-decoration: none; color: White;
                                    font-weight: bold;" class="icon-search" runat="server" onclick="fetch();return false;"
                                    tabindex="4">&nbsp;Fetch</a></li>
                            </ul>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div id="divScheduleDetail">
            <fieldset class="lbl" id="fldScheduleDetail">
                <legend class="blbl">To Schedule Update</legend>
                <table border="0" cellpadding="2" cellspacing="2" class="tblstd">
                    <tr>
                        <td>
                            <Inp:iLabel ID="lblScheduleDate" runat="server" CssClass="lbl">
                   Schedule Date
                            </Inp:iLabel>
                        </td>
                        <td>
                            <Inp:iDate ID="datScheduleDate" TabIndex="3" runat="server" HelpText="551" CssClass="dat"
                                iCase="Upper" ReadOnly="false" OnClientTextChange="" MaxLength="11">
                                <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="divSchedule"
                                    Operator="GreaterThanEqual"  LkpErrorMessage="Invalid Schedule Date. Click on the calendar icon for valid values"
                                    Validate="True" CsvErrorMessage="" CustomValidationFunction="" CompareValidation="true"
                                    CmpErrorMessage="Schedule date Should be greater than or equal to Current Date."  />
                                <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            </Inp:iDate>
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
                            <ul>
                                <li class="btn btn-small btn-success"><a href="#" data-corner="8px" id="hypPrintSchedule"
                                    style="text-decoration: none; color: White; font-weight: bold; float: left; vertical-align: middle;"
                                    class="icon-print" runat="server" onclick="printRepairSchedule();return false;" tabindex="5">&nbsp;<span style="font-family: Arial;
                                        line-height: 10px;">Print Schedule</span></a></li>
                            </ul>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div id="tabSchedulingGrid">
            <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                font-family: Arial; font-size: 8pt; display: none; width: 100%; height: 100%;">
                <div>
                    No Records Found.</div>
            </div>
            <div id="divScheduleGrid" style="width: 100%; height: 100%;">
                <fieldset class="lbl" id="fldScheduleGrid">
                    <legend class="blbl">Activity Detail</legend>
                    <iFg:iFlexGrid ID="ifgSchedule" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="ID" CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="25"
                        StaticHeaderHeight="220px" UseAccessibleHeader="True" ShowEmptyPager="true" Width="100%"
                        AllowAdd="False" AllowDelete="False" AllowEdit="true" UseCachedDataSource="True"
                        AllowSearch="True" AllowSorting="True" AllowStaticHeader="True" AutoSearch="True"
                        RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
                        EnableViewState="True" AllowRefresh="True" SortAscImageUrl="" SortDescImageUrl=""
                        AddButtonText="Add" OnAfterCallBack="ifgScheduleOnAfterCB" UseIcons="true" SearchButtonIconClass="icon-search"
                        SearchButtonCssClass="btn btn-small btn-info" AddButtonIconClass="icon-plus"
                        AddButtonCssClass="btn btn-small btn-success" DeleteButtonIconClass="icon-trash"
                        DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonIconClass="icon-refresh"
                        RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonIconClass="icon-remove"
                        ClearButtonCssClass="btn btn-small btn-success" ClearButtonIconClass="icon-eraser" ValidationGroup="divSchedule"
                        SearchCancelButtonCssClass="btn btn-small btn-danger">
                        <RowStyle CssClass="gitem"  Width="100px" Wrap="True" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />                      
                        <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <PagerStyle CssClass="gpage" Height="16px" Font-Names="Arial" HorizontalAlign="Center" />
                        <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                        <SelectedRowStyle CssClass="gsitem" />
                        <AlternatingRowStyle CssClass="gaitem" />
                    </iFg:iFlexGrid>
                </fieldset>
               
            </div>
        </div>
    </div>
    <div id="divSubmit">
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
                    onClientPrint="null" />
                </div>
    <asp:HiddenField ID="hdnCurrentDate" runat ="server" />
    </form>
</body>
</html>
