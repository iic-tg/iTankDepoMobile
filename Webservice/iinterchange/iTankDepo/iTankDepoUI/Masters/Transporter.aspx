<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Transporter.aspx.vb" Inherits="Masters_Transporter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr class="ctab" style="width: 100%; height: 20px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Transporter</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navTransporter" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Issue Fix (IE)- Issue No :17 Page alignment is not proper,Grid alignment is not proper , submit button not shown  -->
    <div id="tabTransporter" class="tabdisplayGatePass" style="overflow-y: auto; overflow-x: auto;height:auto">
        <table border="0" class="tblstd" cellpadding="1" cellspacing="2" style="width:100%" >
            <tr>
                <td colspan="11">
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblTransporterCode" runat="server" class="lbl">
                        Code
                    </label>
                    <Inp:iLabel ID="reqTransporterCode" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtTransporterCode" runat="server" CssClass="txt" TabIndex="1"
                        HelpText="522,TRANSPORTER_TRNSPRTR_CD" TextMode="SingleLine" iCase="Upper" ToolTip="Transporter Code">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets and Numbers are Allowed"
                            RegularExpression="^[a-zA-Z0-9]+$" Type="String" Validate="True" ValidationGroup=""
                            LookupValidation="False" CsvErrorMessage="This Code Already Exists" CustomValidationFunction="validateTransporterCode"
                            IsRequired="true" ReqErrorMessage="Transporter Code Required" CustomValidation="true"
                            RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblTransporterDescription" runat="server" class="lbl">
                        Description
                    </label>
                    <Inp:iLabel ID="reqTransporterDescription" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtTransporterDescription" runat="server" CssClass="txt" TabIndex="2"
                        HelpText="523,TRANSPORTER_TRNSPRTR_DSCRPTN" TextMode="SingleLine" iCase="Upper"
                        ToolTip="Transporter Description">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are Allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="true" ReqErrorMessage="Transporter Description Required" CustomValidation="false"
                            RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblContactPerson" runat="server" class="lbl">
                        Contact Person
                    </label>
                    <Inp:iLabel ID="reqContactPerson" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtContactPerson" runat="server" CssClass="txt" TabIndex="3" HelpText="524,TRANSPORTER_CNTCT_PRSN"
                        TextMode="SingleLine" iCase="Upper" ToolTip="Contact Person Name">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are allowed."
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="true" ReqErrorMessage="Contact Person Name Required" CustomValidation="false"
                            RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblContactAddress" runat="server" class="lbl">
                        Contact Address
                    </label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtContactAddress" runat="server" CssClass="txt" TabIndex="4" HelpText="525"
                        TextMode="MultiLine"  iCase="None" ToolTip="Contact Address" MaxLength="1000">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Single & Double quotes are not Allowed"
                            Type="String"  Validate="True" ValidationGroup="" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" CustomValidation="false"
                            RegexValidation="true"  RegularExpression="^["']"/>
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblZipCode" runat="server" class="lbl">
                        Zip Code
                    </label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtZipCode" runat="server" CssClass="txt" MaxLength="100" TabIndex="5"
                        HelpText="526,TRANSPORTER_ZP_CD" TextMode="SingleLine" iCase="None" ToolTip="Zip Code">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                            ValidationGroup="" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" ReqErrorMessage="" CustomValidation="false" RegErrorMessage="Invalid Zip Code [a-z, A-Z, 0-9,-,_,+,Space Characters are Allowed]"
                            RegexValidation="true" RegularExpression="^[a-zA-Z0-9-_+ ]+$" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblPhoneNo" runat="server" class="lbl">
                        Phone No
                    </label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtPhoneNo" runat="server" CssClass="txt" MaxLength="20" TabIndex="6"
                        HelpText="527,TRANSPORTER_PHN_NO" TextMode="SingleLine" iCase="Upper" ToolTip="Phone Number">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                            ValidationGroup="" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" ReqErrorMessage="" CustomValidation="false" RegexValidation="true"
                            RegErrorMessage="Invalid Phone Number [a-z, A-Z, 0-9,-,_,+,Space Characters are Allowed]"
                            RegularExpression="^[a-zA-Z0-9-_+ ]+$" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblFaxNo" runat="server" class="lbl">
                        Fax</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtFax" runat="server" CssClass="txt" MaxLength="100" TabIndex="7"
                        HelpText="528,TRANSPORTER_FX_NO" TextMode="SingleLine" iCase="Upper" ToolTip="Fax Number">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            ValidationGroup="" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" ReqErrorMessage="" CustomValidation="false" RegexValidation="true"
                            RegErrorMessage="Invalid Fax Number [a-z, A-Z, 0-9,-,_,+,Space Characters are Allowed]"
                            RegularExpression="^[a-zA-Z0-9-_+ ]+$" />
                    </Inp:iTextBox>
                </td>
                <td>
                    <label id="lblEmailID" runat="server" class="lbl">
                        Email ID</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtEmailID" runat="server" CssClass="txt" TabIndex="8" HelpText="529"
                        TextMode="MultiLine" iCase="None" ToolTip="Email for Transporter" MaxLength="1000">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                            ValidationGroup="" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" ReqErrorMessage="Email for Transporter" CustomValidation="false"
                            RegexValidation="true" RegErrorMessage="Invalid Email Format" RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblActive" runat="server" class="lbl">
                        Active</label>
                </td>
                <td>
                    <asp:CheckBox ID="chkActive" runat="server" Text="" CssClass="chk" ToolTip="Active"
                        TabIndex="9" />
                </td>
             <%--   <td>
                    <label id="lblDefault" runat="server" class="lbl">
                        Default</label>
                </td>
                <td>
                    <asp:CheckBox ID="chkDefault" runat="server" Text="" CssClass="chk" ToolTip="Default"
                        TabIndex="10" />
                </td>--%>
            </tr>
            <tr>
                <td colspan="11">
                </td>
            </tr>
            <tr>
                <td colspan="11">
                    <asp:Label ID="lblGridRoute" Font-Underline="true" Font-Bold="true" runat="server"
                        CssClass="lbl">
                    Route Details
                    </asp:Label>
                </td>
            </tr>
            </table>
            <table  >
            <tr>
                <td  >
                  <div id="divifgCleaningType" style="width: 100%; "  > 
                    <iFg:iFlexGrid ID="ifgRouteDetail" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        CaptionAlign="Left" CellPadding="2" CssClass="tblstd" PageSize="25" AllowSearch="false"
                        AutoSearch="True" AllowRefresh="false" StaticHeaderHeight="140px" ShowEmptyPager="True"
                        Width="100%" UseCachedDataSource="True" AllowSorting="True" AllowStaticHeader="True"
                        RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" HeaderRows="1"
                        EnableViewState="False" DeleteButtonText="Delete Row" GridLines="Both" HorizontalAlign="NotSet"
                        PageSizerFormat="" Scrollbars="Auto" Type="Normal" ValidationGroup="divRouteDetail"
                        DataKeyNames="TRNSPRTR_RT_DTL_ID" OnAfterCallBack="onAfterCallBackRouteDetail"
                        OnBeforeCallBack="" OnAfterClientRowCreated="setDefaultValues" AddRowsonCurrentPage="True"
                        UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                        AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                        DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                        RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                        SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser">
                        <Columns>
                            <iFg:LookupField DataField="RT_CD" ForeignDataField="RT_ID" HeaderText="Route*" HeaderTitle="Route"
                                PrimaryDataField="RT_ID" SortAscUrl="" SortDescUrl="" HeaderStyle-Width="50px" ReadOnly="false">
                                <Lookup DataKey="RT_CD" DependentChildControls="" HelpText="530,ROUTE_RT_CD" iCase="Upper"
                                    OnClientTextChange="" ValidationGroup="" MaxLength="10" TableName="68"
                                    CssClass="lkp" DoSearch="True" Width="120px" ClientFilterFunction="" AllowSecondaryColumnSearch="false"
                                    SecondaryColumnName="">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="RT_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="RT_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Route Description" ColumnName="RT_DSCRPTN_VC" Hidden="false" ControlToBind="1"  />
                                        <Inp:LookupColumn ColumnCaption="Pick Up Location" ColumnName="PCK_UP_LCTN_CD" Hidden="false"
                                            ControlToBind="2" />                                     
                                        <Inp:LookupColumn ColumnCaption="Drop Off Location" ColumnName="DRP_OFF_LCTN_CD"
                                            Hidden="false" ControlToBind="3" />                                    
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="70px" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="Top"  Width="400px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True" 
                                        CustomValidation="true" CustomValidationFunction="validateRouteCode" LkpErrorMessage="Invalid Route. Click on the list for valid values"
                                        ReqErrorMessage="Route Required" Validate="True" ValidationGroup="divRouteDetail" />
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="120px" Wrap="True" />
                            </iFg:LookupField>
                             <iFg:BoundField DataField="RT_DSCRPTN_VC" HeaderText="Description" HeaderTitle="Description"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="150px" Wrap="True" />
                            </iFg:BoundField>
                            <iFg:BoundField DataField="PCK_UP_LCTN_CD" HeaderText="Pick Up Location" HeaderTitle="Pick Up Location"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="120px" Wrap="True" />
                            </iFg:BoundField>
                            <iFg:BoundField DataField="DRP_OFF_LCTN_CD" HeaderText="Drop Off Location" HeaderTitle="Drop Off Location"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="120px" Wrap="True" />
                            </iFg:BoundField>
                                                      
                            <iFg:TextboxField DataField="EMPTY_TRP_SPPLR_RT_NC" HeaderText="Empty Trip Supplier Rate"
                                HeaderTitle="Empty Trip Supplier Rate" SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="535,TRANSPORTER_ROUTE_DETAIL_EMPTY_TRP_SPPLR_RT_NC"
                                    iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        ValidationGroup="divRouteDetail" RegexValidation="true" RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" RegErrorMessage="Invalid Empty Trip Rate. Range must be from 0.01 to 9999999.99" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="250px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="EMPTY_TRP_CSTMR_RT_NC" HeaderText="Empty Trip Customer Rate"
                                HeaderTitle="Empty Trip Customer Rate" SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="536,TRANSPORTER_ROUTE_DETAIL_EMPTY_TRP_CSTMR_RT_NC"
                                    iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        ValidationGroup="divRouteDetail" RegexValidation="true" RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" RegErrorMessage="Invalid Empty Trip Rate. Range must be from 0.01 to 9999999.99" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="250px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="FLL_TRP_SPPLR_RT_NC" HeaderText="Full Trip Supplier Rate"
                                HeaderTitle="Full Trip Supplier Rate" SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="537,TRANSPORTER_ROUTE_DETAIL_FLL_TRP_SPPLR_RT_NC"
                                    iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        ValidationGroup="divRouteDetail" RegexValidation="true" RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" RegErrorMessage="Invalid Full Trip Rate. Range must be from 0.01 to 9999999.99" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="200px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="FLL_TRP_CSTMR_RT_NC" HeaderText="Full Trip Customer Rate"
                                HeaderTitle="Full Trip Customer Rate" SortAscUrl="" SortDescUrl="">
                                <TextBox CausesValidation="True" CssClass="ntxto" HelpText="538,TRANSPORTER_ROUTE_DETAIL_FLL_TRP_CSTMR_RT_NC"
                                    iCase="Numeric" OnClientTextChange="formatRate" ValidationGroup="">
                                    <Validator Operator="Equal" Type="Double" Validate="true" CustomValidation="false"
                                        ValidationGroup="divRouteDetail" RegexValidation="true" RegularExpression="^[0-9]{0,7}(\.[0-9]{1,2})?$"
                                        CustomValidationFunction="" IsRequired="false" ReqErrorMessage="" RegErrorMessage="Invalid Full Trip Rate. Range must be from 0.01 to 9999999.99" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="200px" Wrap="True" HorizontalAlign="Right" />
                            </iFg:TextboxField>
                        </Columns>
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <PagerStyle CssClass="gpage" HorizontalAlign="Center" />
                        <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                        <SelectedRowStyle CssClass="gsitem" />
                        <AlternatingRowStyle CssClass="gaitem" />
                    </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
        </table>
  </div>
    <table class="tblstd" style="margin: 0px auto;">
        <tr>
            <td>
                <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" align="center"
                    onClientPrint="null" />
            </td>
            <td>
                <div id="divCopyRoute" style="vertical-align:top; margin-top: -20px; ">
                    <ul style="float: left; vertical-align: top; float:none;">
                        <li class="btn btn-small btn-success" style="vertical-align:top;" ><a href="#" data-corner="8px" id="hypCopyRoute"
                            class="icon-copy" style="text-decoration: none; color: White; font-weight: bold;
                            float: left; vertical-align: top; height:10px; border: 0px; border-bottom-style:0px; border-left-style:0px; border-right-style:0px;     " runat="server" onclick="onOpenCopyRoute(); return false;"
                            tabindex="22">&nbsp;<span style="font-family: Arial , Lucida Grande , Lucida, Geneva, Helvetica, sans-serif; line-height: 10px;">Copy Route</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
      
    </form>
    
</body>
</html>
