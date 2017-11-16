<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RepairCompletion.aspx.vb"
    Inherits="Operations_RepairCompletion" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .hide
        {
            display: none;
        }
        .show
       {
            display: block;
          width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="height: 100%;">
    <div>
        <div style="width: 100%">
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh">Operations >> Repair Completion</span>
                    </td>
                    <td align="right">
                        <nv:Navigation ID="navRepairCompletion" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
      <!-- UIG Fix -->
    <div id="tabRepairCompletion" class="tabdisplayGatePass" style="overflow: hidden;">
        <div>
            <Nav:iTab ID="ITab1" runat="server">
                <TabPages>
                    <Nav:TabPage ID="Pending" runat="server" Caption="Pending" TabPageClientId="divCommon"
                        OnAfterSelect="ifgEquipmentDetailOnAfterPendingTabSelected();" doValidate="true">
                    </Nav:TabPage>
                    <Nav:TabPage ID="Mysubmits" runat="server" Caption="My Submits" TabPageClientId="divCommon"
                        OnAfterSelect="ifgEquipmentDetailOnAfterSubmitTabSelected();" OnBeforeSelect="ifgEquipmentDetailOnBeforeSubmitTabSelected();"
                        doValidate="true">
                    </Nav:TabPage>
                </TabPages>
            </Nav:iTab>
        </div>
        <div id="divCommon">
            <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width:100%">
                <tr>
                    <td>
                        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                            font-family: Arial; font-size: 8pt; display: none; width: 100%;" align="center">
                            <div>
                                No Records Found.</div>
                        </div>
                        <div id="divEquipmentDetail" style="margin: 1px; width: 100%; height: 100%; vertical-align: middle;
                            ">
                            <iFg:iFlexGrid ID="ifgEquipmentDetail" runat="server" AllowStaticHeader="True" DataKeyNames="RPR_ESTMT_ID"
                                Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
                                ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                                EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                                PageSize="23" AddRowsonCurrentPage="False" AllowPaging="True"
                                AllowAdd="False" AllowDelete="False" OnAfterCallBack="ifgEquipmentDetailOnAfterCB"
                                AllowRefresh="True" AllowSearch="True" AutoSearch="True" UseIcons="True"
                                SearchButtonCssClass="btn btn-small btn-info"
                                DeleteButtonCssClass="btn btn-small btn-danger"
                                RefreshButtonCssClass="btn btn-small btn-info"
                                SearchCancelButtonCssClass="btn btn-small btn-danger"
                                ClearButtonCssClass="btn btn-small btn-success" Mode="Insert">
                                <PagerStyle CssClass="gpage" />
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <Columns>
                                    <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment Number"
                                        IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="EQPMNT_TYP_CD" HeaderText="Type" HeaderTitle="Equipment Type"
                                        IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="EQPMNT_CD_CD" HeaderText="Code" HeaderTitle="Equipment Code"
                                        IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="EQPMNT_STTS_CD" HeaderText="Status" HeaderTitle="Equipment Status"
                                        IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="True">
                                        <HeaderStyle Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                         <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="RPR_ESTMT_NO" HeaderText="Estimate No" HeaderTitle="Estimate Number"
                                        IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="150px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="CSTMR_CD" HeaderText="Customer" HeaderTitle="Customer"
                                        IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="RPR_TYP_CD" HeaderText="Repair Type" HeaderTitle="Repair Type"
                                        IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="True">
                                        <HeaderStyle Width="150px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="150px" Wrap="True" />
                                    </iFg:BoundField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="YRD_LCTN" HeaderText="Yard Loc" HeaderTitle="Yard Location"
                                        SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                        <TextBox CssClass="txt" HelpText="435,ACTIVITY_STATUS_YRD_LCTN" iCase="None" OnClientTextChange=""
                                            ValidationGroup="" MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                                RegexValidation="False" LookupValidation="False" />
                                        </TextBox>
                                        <HeaderStyle Width="150px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="150px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:BoundField DataField="ESTMTD_LBR_HRS" HeaderText="Estimated Man Hour" HeaderTitle="Estimated Man Hour"
                                        IsEditable="False" SortAscUrl="" SortDescUrl="">
                                        <HeaderStyle Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                                    </iFg:BoundField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="ACTL_MN_HR_NC" HeaderText="Actual Man Hour"
                                        HeaderTitle="Actual Man Hour" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                        <TextBox CssClass="ntxto" HelpText="436,REPAIR_ESTIMATE_LBR_RT_NC" iCase="Numeric"
                                            OnClientTextChange="formatDecimalRate" ValidationGroup="" MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" IsRequired="false" ReqErrorMessage="Estimated Man Hour is Required."
                                                Operator="Equal" Type="String" Validate="True" RegexValidation="True" LookupValidation="False" RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$" RegErrorMessage="Invalid Actual Man Hour. Range must be from 0.01 to 9999999.99" />
                                        </TextBox>
                                        <HeaderStyle Width="100px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Right" />
                                    </iFg:TextboxField>
                                    <iFg:DateField DataField="RPR_CMPLTN_DT" HeaderText="Completion Date *" HeaderTitle="Repair Completion Date"
                                        SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <iDate HelpText="437" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                            ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="True"
                                                LkpErrorMessage="Invalid Completion Date. Click on the calendar icon for valid values"
                                                ReqErrorMessage="Completion Date Required" Validate="True" RangeValidation="True" CustomValidation="true" CustomValidationFunction="ValidatePreviousActivityDate"    
                                                CompareValidation="true" />
                                        </iDate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="120px" Wrap="True" />
                                    </iFg:DateField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="RPR_CMPLTN_TM" HeaderText="Time *"
                                        HeaderTitle="Repair Completion Time" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <TextBox CssClass="txt" HelpText="438" iCase="Lower" OnClientTextChange="" ValidationGroup=""
                                            MaxLength="5">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                RegexValidation="True" RegularExpression="([01]?[0-9]|2[0-3]):[0-5][0-9]" RegErrorMessage="Invalid Completion Time. Enter in hh:mm format and must be between 00:00 to 23:59"
                                                IsRequired="True" ReqErrorMessage="Completion Time Required" />
                                        </TextBox>
                                        <HeaderStyle Width="60px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="60px" Wrap="True" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                                        SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center"
                                        Visible="true" ReadOnly="false">
                                        <TextBox CssClass="txt" HelpText="439,ACTIVITY_STATUS_RMRKS_VC" iCase="Lower" OnClientTextChange=""
                                            ValidationGroup="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                RegexValidation="false" RegularExpression="" RegErrorMessage="" IsRequired="false"
                                                ReqErrorMessage="" />
                                        </TextBox>
                                        <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="100px" Wrap="True" />
                                    </iFg:TextboxField>                                   
                                    <iFg:ImageField HeaderTitle="Attach Files" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/attachment.png"
                                        HeaderText="Attach Files" HeaderImageUrl="">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="16px" Wrap="True" HorizontalAlign="Center" />
                                    </iFg:ImageField>
                                    <iFg:CheckBoxField DataField="CHECKED" HeaderText="" HeaderTitle="Select" HelpText=""
                                        SortAscUrl="" SortDescUrl="" Visible="True" HeaderImageUrl="../Images/flrsel.gif">
                                        <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Center" />
                                    </iFg:CheckBoxField>
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
        </div>
    </div>

    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
        onclientprint="submitPrintPage();" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField ID="hdnPageName" runat="server" />
    <asp:HiddenField ID="hdnRevisionNo" runat="server" />
    <asp:HiddenField ID="hdnCurrentDate" runat="server" />
    </form>
</body>
</html>
