<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangeOfStatus.aspx.vb"
    Inherits="Operations_ChangeOfStatus" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Operations >> Change of Status</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navChangeOfStatus" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Fix -->
    <div id="tabChangeofStatus" class="tabdisplayGatePass" style="overflow-y: auto; overflow-x: auto;height:auto">
        <fieldset class="lbl" id="fldSearch">
            <legend class="blbl">Search</legend>
            <table border="0" cellpadding="2" align="left" cellspacing="2" class="tblstd" align="center">
                <tr>
                    <td>
                        <label id="lblfromActivity" runat="server" class="lbl">
                            Current Status</label>
                        <Inp:iLabel ID="ILabel2" runat="server" CssClass="lblReq">
                   *
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iLookup ID="lkpSearchStatus" runat="server" CssClass="lkp" DataKey="EQPMNT_STTS_CD"
                            OnClientTextChange="OnChangeStatus" DoSearch="True" iCase="Upper" MaxLength="10"
                            TabIndex="1" TableName="50" HelpText="420,EQUIPMENT_STATUS_EQPMNT_STTS_CD" ClientFilterFunction=""
                            AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_STTS_DSCRPTN_VC" >
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="EQPMNT_STTS_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="EQPMNT_STTS_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnName="EQPMNT_STTS_DSCRPTN_VC" ControlToBind="" Hidden="False"
                                    ColumnCaption="Description" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                LkpErrorMessage="Invalid Equipment Status. Click on the list for valid values"
                                Operator="Equal" ReqErrorMessage="Equipment Status Required" Type="String" Validate="True"
                                ValidationGroup="tabChangeofStatus" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </td>
                    <td>
                    </td>
                    <td>
                        <label id="lblCustomer" runat="server" class="lbl">
                            Customer</label>
                    </td>
                    <td>
                        <Inp:iLookup ID="lkpCustomer" runat="server" CssClass="lkp" DataKey="CSTMR_CD" DoSearch="True"
                            iCase="Upper" MaxLength="10" TabIndex="2" TableName="49" HelpText="313,CUSTOMER_CSTMR_CD"
                            ClientFilterFunction="" ReadOnly="false" AllowSecondaryColumnSearch="true" SecondaryColumnName="CSTMR_NAM">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="CSTMR_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                LkpErrorMessage="Invalid Customer. Click on the list for valid values" Operator="Equal"
                                ReqErrorMessage="Customer Required" Type="String" Validate="True" ValidationGroup="tabChangeofStatus" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </td>
                    <td>
                    </td>
                    <td>
                        <Inp:iLabel ID="ILabel3" runat="server" CssClass="lbl">
                (or)
                        </Inp:iLabel>
                    </td>
                    <td>
                        <label id="lblEquipmentNo" runat="server" class="lbl">
                            Equipment No</label>
                        <Inp:iLabel ID="ILabel1" runat="server" CssClass="lblReq">
                   *
                        </Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iTextBox ID="txtEquipmentNo" runat="server" CssClass="txt" ToolTip="Equipment Number"
                            HelpText="312,ACTIVITY_STATUS_EQPMNT_NO" iCase="Upper" TabIndex="3">
                            <Validator IsRequired="false" ReqErrorMessage="Equipment Number Required" Validate="True"
                                ValidationGroup="tabChangeofStatus" />
                        </Inp:iTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <ul style="margin: 0px;">
                            <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnSearch" style="text-decoration: none;
                                color: White; font-weight: bold" class="icon-search" runat="server" onclick="fetch();return false;"
                                tabindex="4">&nbsp;Search</a></li>
                        </ul>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </fieldset>
        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
            font-family: Arial; font-size: 8pt; display: none; width: 80%; font-weight: bold;"
            align="center">
            <div>
                There are no information available in iTankdepo for the selected Status or Equipment
                to perform the Change of Status action.</div>
        </div>
        <div id="divStatusDetail">
            <fieldset class="lbl" id="fldStatus">
                <legend class="blbl">To Status Update</legend>
                <table border="0" cellpadding="2" cellspacing="2" class="tblstd">
                    <tr>
                        <td>
                            <label id="Label1" runat="server" class="lbl">
                                To Status</label>
                        </td>
                        <td>
                            <Inp:iLookup ID="lkpStatus" runat="server" CssClass="lkp" DataKey="EQPMNT_STTS_CD"
                                DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="4" TableName="12" HelpText="314,EQUIPMENT_STATUS_EQPMNT_STTS_CD"
                                ClientFilterFunction="applyFilter" AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_STTS_DSCRPTN_VC">
                                <LookupColumns>
                                    <Inp:LookupColumn ColumnName="EQPMNT_STTS_ID" Hidden="True" />
                                    <Inp:LookupColumn ColumnName="EQPMNT_STTS_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                                    <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_STTS_DSCRPTN_VC"
                                        Hidden="False" />
                                </LookupColumns>
                                <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                    IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                                <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                                <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                                    LkpErrorMessage="Invalid To Status. Click on the list for valid values" Operator="Equal"
                                    ReqErrorMessage="To Status Required" Type="String" Validate="True" ValidationGroup="divEquipmentDetail" />
                                <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                    IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            </Inp:iLookup>
                        </td>
                        <td>
                        </td>
                        <td>
                            <label id="Label2" runat="server" class="lbl">
                                To Status Date</label>
                        </td>
                        <td>
                            <Inp:iDate ID="dateStatus" TabIndex="5" runat="server" HelpText="315" CssClass="dat"
                                MaxLength="11" iCase="Upper">
                                <Validator CustomValidateEmptyText="False" IsRequired="False" Type="Date" ValidationGroup="divEquipmentDetail"
                                    Operator="LessThanEqual" LkpErrorMessage="Invalid To Status Date. Click on the calendar icon for valid values"
                                    Validate="True" CsvErrorMessage="" CustomValidationFunction="" CompareValidation="false"
                                    CmpErrorMessage="" />
                                <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                    OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            </Inp:iDate>
                        </td>
                        <td>
                        </td>
                        <td>
                            <label id="lblRemarks" runat="server" class="lbl">
                                Remarks</label>
                        </td>
                        <td>
                            <Inp:iTextBox ID="txtRemarks" runat="server" CssClass="txt" ToolTip="Remarks" HelpText="418,ACTIVITY_STATUS_RMRKS_VC"
                                iCase="None" TabIndex="6">
                                <Validator IsRequired="false" ReqErrorMessage="" Validate="True" ValidationGroup="divEquipmentDetail" />
                            </Inp:iTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <label id="lblYardLocation" runat="server" class="lbl">
                                Yard Loc</label>
                        </td>
                        <td>
                            <Inp:iTextBox ID="txtYardLocation" runat="server" CssClass="txt" ToolTip="Yard Location"
                                HelpText="419,ACTIVITY_STATUS_YRD_LCTN" iCase="None" TabIndex="7">
                                <Validator IsRequired="false" ReqErrorMessage="" Validate="True" ValidationGroup="divEquipmentDetail" />
                            </Inp:iTextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div id="divEquipmentDetail" style="margin: 1px; vertical-align: middle;">
            <fieldset class="lbl" id="fldEquipmentDetail">
                <legend style="font-weight: bold;">Equipment Detail</legend>
                <table border="0" cellpadding="2" cellspacing="2" class="" style="width: 100%;">
                    <tr>
                        <td colspan="5">
                            <iFg:iFlexGrid ID="ifgEquipmentDetail" runat="server" AllowStaticHeader="True" DataKeyNames="ACTVTY_STTS_ID"
                                Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="170px" Type="Normal"
                                ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                                EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                                PageSize="10" AddRowsonCurrentPage="False" AllowPaging="True" AllowAdd="false"
                                AllowDelete="False" OnAfterCallBack="ifgEquipmentDetailOnAfterCB" AllowRefresh="True"
                                AllowSearch="True" AutoSearch="True" OnBeforeCallBack="" ClearButtonCssClass="btn btn-small btn-success"
                                DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonCssClass="btn btn-small btn-info"
                                SearchButtonCssClass="btn btn-small btn-info" SearchCancelButtonCssClass="btn btn-small btn-danger"
                                UseIcons="True">
                                <PagerStyle CssClass="gpage" />
                                <RowStyle CssClass="gitem" />
                                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                <Columns>
                                    <iFg:BoundField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                                        IsEditable="False" SortAscUrl="" SortDescUrl="">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="10%" Wrap="True" Font-Bold="false" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="EQPMNT_TYP_CD" HeaderText="Type" HeaderTitle="Type" IsEditable="False"
                                        SortAscUrl="" SortDescUrl="">   
                                        <HeaderStyle Width="40px" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="5%" Wrap="True" Font-Bold="false" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="CSTMR_CD" HeaderText="Customer" HeaderTitle="Customer"
                                        IsEditable="False" SortAscUrl="" SortDescUrl="">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="10%" Wrap="True" Font-Bold="false" />
                                    </iFg:BoundField>
                                    <iFg:DateField DataField="GTN_DT" HeaderText="In Date" HeaderTitle="In Date" SortAscUrl=""
                                        SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ReadOnly="true">
                                        <iDate HelpText="316,ACTIVITY_STATUS_GTN_DT" iCase="Upper" LeftPosition="0" OnClientTextChange=""
                                            TopPosition="0" ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                                LkpErrorMessage="Invalid Gate In Date. Click on the calendar icon for valid values"
                                                ReqErrorMessage="Event Date Required" Validate="True" RangeValidation="false"
                                                CompareValidation="false" ValidationGroup="divEquipmentDetail" />
                                        </iDate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="10%" Wrap="True" HorizontalAlign="Left" Font-Bold="false" />
                                    </iFg:DateField>
                                    <iFg:BoundField DataField="PRDCT_DSCRPTN_VC" HeaderText="Previous Cargo" HeaderTitle="Previous Cargo"
                                        IsEditable="False" SortAscUrl="" SortDescUrl="" HeaderStyle-CssClass="hide">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="5%" Wrap="True" Font-Bold="false" />
                                    </iFg:BoundField>
                                    <iFg:BoundField DataField="EQPMNT_STTS_CD" HeaderText="Current Status" HeaderTitle="Current Status"
                                        IsEditable="false">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="10%" Wrap="true" Font-Bold="false" />
                                    </iFg:BoundField>
                                    <iFg:DateField DataField="ACTVTY_DT" HeaderText="Current Status Date" HeaderTitle="Current Status Date"
                                        SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                        ReadOnly="true">
                                        <iDate HelpText="317,ACTIVITY_STATUS_ACTVTY_DT" iCase="Upper" LeftPosition="0" OnClientTextChange=""
                                            TopPosition="0" ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                                LkpErrorMessage="Invalid Current Status Date. Click on the calendar icon for valid values"
                                                ValidationGroup="divEquipmentDetail" ReqErrorMessage="Event Date Required" Validate="True"
                                                RangeValidation="false" CompareValidation="false" />
                                        </iDate>
                                        <HeaderStyle Width="10%" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="10%" Wrap="True" HorizontalAlign="Left" Font-Bold="false" />
                                    </iFg:DateField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="YRD_LCTN" HeaderText="Yard Loc" HeaderTitle="Yard Loc"
                                        SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                        <TextBox CssClass="txt" HelpText="318,ACTIVITY_STATUS_YRD_LCTN" iCase="None" OnClientTextChange=""
                                            ValidationGroup="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                ValidationGroup="divEquipmentDetail" RegexValidation="false" RegularExpression=""
                                                RegErrorMessage="" />
                                        </TextBox>
                                        <HeaderStyle Width="10%" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="10%" Wrap="True" Font-Bold="false" />
                                    </iFg:TextboxField>
                                    <iFg:TextboxField CharacterLimit="0" DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                                        SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px">
                                        <TextBox CssClass="txt" HelpText="418,ACTIVITY_STATUS_RMRKS_VC" iCase="None" OnClientTextChange=""
                                            ValidationGroup="">
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                                ValidationGroup="divEquipmentDetail" RegexValidation="false" RegularExpression=""
                                                RegErrorMessage="" />
                                        </TextBox>
                                        <HeaderStyle Width="10%" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="10%" Wrap="True" Font-Bold="false" />
                                    </iFg:TextboxField>
                                    <iFg:LookupField DataField="NEW_EQPMNT_STTS_CD" ForeignDataField="EQPMNT_STTS_ID"
                                        HeaderText="To Status *" HeaderTitle="To Status" PrimaryDataField="EQPMNT_STTS_ID"
                                        SortAscUrl="" SortDescUrl="">
                                        <Lookup DataKey="EQPMNT_STTS_CD" DependentChildControls="" HelpText="311,EQUIPMENT_STATUS_EQPMNT_STTS_CD"
                                            iCase="Upper" OnClientTextChange="" ValidationGroup="divEquipmentDetail" MaxLength="15"
                                            TableName="12" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="applyFilter"
                                            AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_STTS_DSCRPTN_VC">
                                            <LookupColumns>
                                                <Inp:LookupColumn ColumnName="EQPMNT_STTS_ID" Hidden="True" />
                                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_STTS_CD" Hidden="False" />
                                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_STTS_DSCRPTN_VC"
                                                    Hidden="False" />
                                            </LookupColumns>
                                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="true"
                                                LkpErrorMessage="Invalid To Status. Click on the list for valid values" ValidationGroup="divEquipmentDetail"
                                                ReqErrorMessage="To Status Required." Validate="True" />
                                        </Lookup>
                                        <HeaderStyle Width="10%" Wrap="True" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="10%" Wrap="True" HorizontalAlign="Left" Font-Bold="false" />
                                    </iFg:LookupField>
                                    <iFg:DateField DataField="NEW_ACTVTY_DT" HeaderText="To Status Date *" HeaderTitle="To Status Date"
                                        SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false">
                                        <iDate HelpText="319" iCase="Upper" OnClientTextChange="" TopPosition="0" ValidationGroup=""
                                            MaxLength="11" CssClass="txt" Width="110px" LeftPosition="-70">
                                            <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                                ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="true"
                                                LkpErrorMessage="Invalid To Status Date. Click on the calendar icon for valid values"
                                                ValidationGroup="divEquipmentDetail" ReqErrorMessage="To Status Date Required"
                                                Validate="True" RangeValidation="false" CompareValidation="true" CustomValidation="true"
                                                CustomValidationFunction="validateStatusDate" />
                                        </iDate>
                                        <HeaderStyle Width="10%" HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="10%" Wrap="True" Font-Bold="false" />
                                    </iFg:DateField>
                                    <iFg:CheckBoxField DataField="CHECKED" HeaderText="">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
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
                
            </fieldset>
        </div>
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
                    onClientPrint="null" />
    <asp:HiddenField ID="hdnStatusCode" runat="server" />
    <asp:HiddenField ID="hdnCurrentDate" runat="server" />
    </form>
</body>
</html>
