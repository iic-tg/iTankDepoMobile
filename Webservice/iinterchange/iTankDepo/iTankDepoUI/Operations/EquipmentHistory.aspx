<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EquipmentHistory.aspx.vb"
    Inherits="Operations_EquipmentHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Header1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="height: 100%;">
    <div>
        <div style="width: 100%">
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh">Operations >> Equipment History</span>
                    </td>
                     <td align="right">
                    <nv:Navigation ID="navCleaningInspection" runat="server" />
                </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divSearch" style="overflow-y: auto;overflow-x: auto; height: auto">
    <!-- UIG Fix -->
        <fieldset class="lbl" id="fldSearch">
            <legend class="blbl">Search</legend>
            <table border="0" cellpadding="4" align="Left" cellspacing="4" class="tblstd" align="center">
                <tr>
                    <td valign="middle">
                        <label id="lblEquipment" runat="server" class="lbl">
                            Equipment No
                        </label>
                        <label id="lblReqEquipment" runat="server" class="lblReq">
                            *
                        </label>
                    </td>
                    <td valign="middle">
                        <Inp:iTextBox ID="txtEquipment" runat="server" CssClass="txt" MaxLength="11" TabIndex="1"
                            HelpText="252,EQUIPMENT_INFORMATION_EQPMNT_NO" TextMode="SingleLine" iCase="Upper"
                            ToolTip="Equipment No">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                                ValidationGroup="divSearch" LookupValidation="False" IsRequired="true" ReqErrorMessage="Equipment No Required" />
                        </Inp:iTextBox>
                    </td>
                    <td valign="middle">
                        <ul style="margin: 0px;">
                            <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnSearch" style="text-decoration: none;
                                color: White; font-weight: bold" class="icon-search" runat="server" onclick="onSearchClick(); return false;"
                                tabindex="2">&nbsp;Search</a></li>
                        </ul>
                    </td>
                    <td colspan="5">
                        <div id="divPrint" style="display: none;">
                            <ul style="margin: 0px;">
                                <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="A1" style="text-decoration: none;
                                    color: White; font-weight: bold" class="icon-print" runat="server" onclick="printEquipmentHistory(); return false;"
                                    tabindex="2">&nbsp;View Report</a></li>
                            </ul>
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
   
    <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
        font-family: Arial; font-size: 8pt; display: none; width: 80%;" align="center">
        <div>
            No Records Found.</div>
    </div>
    <div id="divEquipHistory">
        <fieldset class="lbl" id="fldEquipmentHistory">
            <legend class="blbl">Equipment History</legend>
            <br />
            <table style="width:100%">
                <tr>
                    <td valign="top">
                        <label id="lblType1" runat="server" class="lbl" visible="True">
                            Type :
                        </label>
                        <label id="lblType" runat="server" class="lbl" style="font-weight: bold">
                        </label>
                    </td>
                    <td valign="top">
                        <label id="lblCode1" runat="server" class="lbl" visible="True">
                            Code :
                        </label>
                        <label id="lblCode" runat="server" class="lbl" style="font-weight: bold">
                        </label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td valign="top" colspan="4">
                        <iFg:iFlexGrid ID="ifgEquipmentHistory" runat="server" AllowStaticHeader="True" DataKeyNames="TRCKNG_ID"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
                            ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            AddRowsonCurrentPage="True" ShowPageSizer="False" OnAfterCallBack="ifgEquipmentHistoryOnAfterCB"
                            OnBeforeCallBack="" AllowDelete="False" AllowAdd="False">
                            <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:TextboxField CharacterLimit="0" DataField="CSTMR_CD" HeaderText="Customer" HeaderTitle="Customer"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression="" HtmlEncode="true">
                                    <TextBox ID="txtCustomer" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="INVCNG_PRTY_CD" HeaderText="Invoicing Party"
                                    HeaderTitle="Invoicing Party" SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression=""
                                    HtmlEncode="true">
                                    <TextBox ID="TextBox1" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <%--GWS--%>
                                <iFg:TextboxField CharacterLimit="0" DataField="AGNT_CD" HeaderText="Agent"
                                    HeaderTitle="Agent" SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression=""
                                    HtmlEncode="true">
                                    <TextBox ID="TextBox8" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                </iFg:TextboxField>

                                <iFg:TextboxField CharacterLimit="0" DataField="PRDCT_DSCRPTN_VC" HeaderText="Previous Cargo"
                                    HeaderTitle="Previous Cargo" SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression=""
                                    HtmlEncode="true">
                                    <TextBox ID="TextBox2" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="ACTVTY_NAM" HeaderText="Activity Name"
                                    HeaderTitle="Activity Name" SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression=""
                                    HtmlEncode="true">
                                    <TextBox ID="TextBox6" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="40px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="EQPMNT_STTS_CD" HeaderText="Activity"
                                    HeaderTitle="Activity" SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression=""
                                    HtmlEncode="true">
                                    <TextBox ID="txtEquipmentStatus" HelpText="" iCase="Upper" OnClientTextChange=""
                                        ValidationGroup="" CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="40px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                             

                                <iFg:DateField DataField="ACTVTY_DT" HeaderText="Activity Date" HeaderTitle="Activity Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    SortExpression="">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" CssClass="txt" Width="80px" ReadOnly="True">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="70px" HorizontalAlign="Left" VerticalAlign="NotSet" />
                                </iFg:DateField>
                                   <%--gws--%>
                                <iFg:TextboxField CharacterLimit="0" DataField="STTS_CD" HeaderText="Equipment Status"
                                    HeaderTitle="Equipment Status" SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression=""
                                    HtmlEncode="true">
                                    <TextBox ID="TextBox9" HelpText="" iCase="Upper" OnClientTextChange=""
                                        ValidationGroup="" CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="40px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="EIR_NO" HeaderText="EIR No" HeaderTitle="EIR No"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression="" HtmlEncode="true">
                                    <TextBox ID="TextBox4" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="RF_NO" HeaderText="Reference No"
                                    HeaderTitle="Reference No" SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression=""
                                    HtmlEncode="true">
                                    <TextBox ID="TextBox5" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="INVC_GNRTN" HeaderText="Billed" HeaderTitle="Billed"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression="" HtmlEncode="true">
                                    <TextBox ID="txtInvoice" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                                    SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression="" HtmlEncode="true">
                                    <TextBox ID="TextBox7" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField CharacterLimit="0" DataField="CRTD_BY" HeaderText="Modified By"
                                    HeaderTitle="Modified By" SortAscUrl="" SortDescUrl="" ReadOnly="true" SortExpression=""
                                    HtmlEncode="true">
                                    <TextBox ID="TextBox3" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup=""
                                        CssClass="txt">
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:DateField DataField="CRTD_DT" HeaderText="Modified Date" HeaderTitle="Modified Date"
                                    SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"
                                    SortExpression="">
                                    <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="" CssClass="txt" Width="80px" ReadOnly="True">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="False" IsRequired="false" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="false" RegularExpression="" ReqErrorMessage="" Type="String"
                                            Validate="false" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="70px" HorizontalAlign="Left" VerticalAlign="NotSet" />
                                </iFg:DateField>
                                <iFg:ImageField HeaderTitle="Delete" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/trash.png"
                                    HeaderText="Delete" HeaderImageUrl="">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="16px" Wrap="True" HorizontalAlign="Center" />
                                </iFg:ImageField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
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
    <div id="divSubmit">
        <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="openDeleteReason()" />
        <asp:HiddenField ID="hdnTrackingID" runat="server" />
        <asp:HiddenField ID="hdnActivityName" runat="server" />
    </div>
    </form>
</body>
</html>
