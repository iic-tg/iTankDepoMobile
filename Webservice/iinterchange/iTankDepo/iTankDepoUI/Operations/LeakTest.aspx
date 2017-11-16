<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeakTest.aspx.vb" Inherits="Operations_LeakTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage('Repair');">
    <form id="form1" runat="server">
    <div id="dvLeakTest" runat="server">
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Operations >> Leak Test</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navLeakTest" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Fix -->
    <div id="tabLeaktest" class="tabLeaktest">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width: 100%">
            <tr>
                <td>
                    <iFg:iFlexGrid ID="ifgLeakTest" runat="server" AllowStaticHeader="True" DataKeyNames="LK_TST_ID"
                        Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
                        ValidationGroup="divLeakTest" UseCachedDataSource="True" AutoGenerateColumns="False"
                        EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="setDefaultValues"
                        AllowAdd="true" PageSize="10" AddRowsonCurrentPage="False" AllowPaging="True"
                        OnAfterCallBack="OnAfterCallBack" AllowDelete="true" AllowRefresh="True" AllowSearch="True"
                        AutoSearch="True" UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                        AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                        DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                        RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                        SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser" OnBeforeCallBack="">
                        <PagerStyle CssClass="gpage" />
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <Columns>
                            <iFg:LookupField DataField="EQPMNT_NO" ForeignDataField="" HeaderText="Equipment No"
                                HeaderTitle="Equipment Number" PrimaryDataField="" SortAscUrl="" SortDescUrl=""
                                HeaderStyle-Width="50px" ReadOnly="false">
                                <Lookup DataKey="EQPMNT_NO" DependentChildControls="" HelpText="398,LEAK_TEST_EQPMNT_NO"
                                    iCase="Upper" OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10"
                                    TableName="60" CssClass="lkp" DoSearch="True" Width="100px" ClientFilterFunction="EquipmentFilter"
                                    AllowSecondaryColumnSearch="false" SecondaryColumnName="">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="EQPMNT_NO" ColumnCaption="Equipment No" Hidden="false" />
                                        <Inp:LookupColumn ColumnCaption="In Date" ColumnName="GTN_DT" Hidden="False" ControlToBind="2" />
                                        <Inp:LookupColumn ColumnCaption="Current Status" ColumnName="EQPMNT_STTS_CD" Hidden="True"
                                            ControlToBind="4" />
                                        <Inp:LookupColumn ColumnCaption="Type" ColumnName="EQPMNT_TYP_CD" Hidden="True" ControlToBind="1" />
                                        <Inp:LookupColumn ColumnCaption="Customer" ColumnName="CSTMR_CD" Hidden="False" ControlToBind="3" />
                                        <Inp:LookupColumn ColumnCaption="Depot" ColumnName="DPT_ID" Hidden="True" ControlToBind="" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="400px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        CustomValidation="true" CustomValidationFunction="validateEquipmentno" LkpErrorMessage="Invalid Equipment No. Click on the list for valid values"
                                        ReqErrorMessage="Equipment No Required" Validate="True" ValidationGroup="divLeakTest" />
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type"
                                HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                HeaderStyle-Width="50px" ReadOnly="true">
                                <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="" iCase="Upper"
                                    OnClientTextChange="" ValidationGroup="divDetail" MaxLength="10" TableName="3"
                                    CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                    SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                            Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                                        ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divLeakTest" />
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="50px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:DateField DataField="GTN_DT" HeaderText="In Date" HeaderTitle="" SortAscUrl=""
                                SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ReadOnly="true">
                                <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                    ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                    <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                        ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                        LkpErrorMessage="Invalid Gate In Date. Click on the calendar icon for valid values"
                                        ReqErrorMessage="Event Date Required" Validate="True" RangeValidation="false"
                                        CompareValidation="false" ValidationGroup="divLeakTest" />
                                </iDate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="90px" Wrap="True" HorizontalAlign="Left" />
                            </iFg:DateField>
                            <iFg:BoundField DataField="CSTMR_CD" HeaderText="Customer" HeaderTitle="Customer Code"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="50px" Wrap="True" />
                            </iFg:BoundField>
                            <iFg:BoundField DataField="EQPMNT_STTS_CD" HeaderText="Current Status" HeaderTitle="Current Status"
                                IsEditable="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="50px" Wrap="true" />
                            </iFg:BoundField>
                            <iFg:CheckBoxField DataField="SHLL_TST_BT" HeaderText="Shell Test" HeaderTitle="Shell Test"
                                HelpText="" SortAscUrl="" SortDescUrl="" HeaderImageUrl="">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="25px" Wrap="True" HorizontalAlign="Center" />
                            </iFg:CheckBoxField>
                            <iFg:DateField DataField="TST_DT" HeaderText="Test Date *" HeaderTitle="Test Date"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false">
                                <iDate HelpText="399" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                    ValidationGroup="" MaxLength="11" CssClass="txt" Width="80px">
                                    <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                        ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="True"
                                        CompareValidation="true" CmpErrorMessage="Test Date cannot be greater than current Date."
                                        LkpErrorMessage="Invalid Test Date. Click on the calendar icon for valid values"
                                        ReqErrorMessage="Test Date Required" Validate="True" CustomValidationFunction="validateStatusDate"
                                        CustomValidation="true" />
                                </iDate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="90px" Wrap="True" HorizontalAlign="Left" />
                            </iFg:DateField>
                            <iFg:CheckBoxField DataField="STM_TB_TST_BT" HeaderText="Steam Tube Test" HeaderTitle="Steam Tube Test"
                                HelpText="" SortAscUrl="" SortDescUrl="" HeaderImageUrl="">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="15px" Wrap="True" HorizontalAlign="Center" />
                            </iFg:CheckBoxField>
                            <iFg:TextboxField DataField="RLF_VLV_SRL_1" HeaderText="Relief Valve Srl #1" HeaderTitle="Relief Valve Srl #1"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="400,LEAK_TEST_RLF_VLV_SRL_1"
                                    iCase="Upper" OnClientTextChange="" ValidationGroup="divLeakTest">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="false"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="50px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="RLF_VLV_SRL_2" HeaderText="Relief Valve Srl #2" HeaderTitle="Relief Valve Srl #2"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="401,LEAK_TEST_RLF_VLV_SRL_2"
                                    iCase="Upper" OnClientTextChange="" ValidationGroup="divLeakTest">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="false"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="50px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="PG_1" HeaderText="Pressure Gauge 1" HeaderTitle="Pressure Gauge 1"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="402,LEAK_TEST_PG_1" iCase="Upper"
                                    OnClientTextChange="" ValidationGroup="divLeakTest">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="false"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="50px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="PG_2" HeaderText="Pressure Gauge 2" HeaderTitle="Pressure Gauge 2"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="403,LEAK_TEST_PG_2" iCase="Upper"
                                    OnClientTextChange="" ValidationGroup="divLeakTest">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="false"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="50px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:HyperLinkField HeaderText="Rev No" HeaderTitle="Rev No" SortAscUrl="" SortDescUrl=""
                                Text="" IsEditable="False" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                            </iFg:HyperLinkField>
                            <iFg:BoundField DataField="NO_OF_TMS_GNRTD" HeaderText="No. of Times Generated" HeaderTitle="Number of Times Generated"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Center" />
                            </iFg:BoundField>
                            <iFg:TextboxField DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                                SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="404,LEAK_TEST_RMRKS_VC"
                                    iCase="None" MaxLength="500" OnClientTextChange="" ValidationGroup="divLeakTest">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="false"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="20px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:CheckBoxField DataField="CHECKED" HeaderText="" HeaderTitle="Select" HelpText=""
                                HeaderImageUrl="../Images/flrsel.gif" SortAscUrl="" SortDescUrl="" Visible="True">
                                <ItemStyle Width="15px" Wrap="True" />
                            </iFg:CheckBoxField>
                        </Columns>
                        <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                        <SelectedRowStyle CssClass="gsitem" />
                        <AlternatingRowStyle CssClass="gaitem" />
                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                    </iFg:iFlexGrid>
                </td>
            </tr>
        </table>
    </div>
    <div align="center">
        <a href="#" id="btnSubmitLeakTest" onclick="onSubmit();return false;" class="btn btn-small btn-success"
            runat="server" style="font-weight: bold"><i class="icon-save"></i>&nbsp;Save</a>
    </div>
    <div style="width: 100%;">
        <table style="width: 100%;">
            <tr style="width: 100%;">
                <td class="btncorner" style="text-align: center; width: 17%;">
                    <a href="#" id="hlnkPrint" onclick="printDocument();return false;" class="btn btn-small btn-success"
                        style="font-weight: bold" runat="server"><i class="icon-save"></i>&nbsp;Generate
                        Document</a>
                </td>
                <td style="text-align: center; width: 66%;">
                    <sp:SubmitPane ID="PageSubmitPane" runat="server" onclientsubmit="submitPage();"
                        onclientprint="null" />
                </td>
                <td style="width: 17%;">
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnPrintBit" runat="server" />
    <asp:HiddenField ID="hdnPageName" runat="server" />
    <asp:HiddenField ID="hdnCurrentDate" runat="server" />
    </form>
</body>
</html>
