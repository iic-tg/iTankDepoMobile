<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EdiSettings.aspx.vb" Inherits="EDI_EdiSettings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvEdiSettings" runat="server">
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">EDI >> Edi Settings</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navEdiSettings" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divEdiSettings" class="tabLeaktest" style="overflow-y: auto; overflow-x: auto;
        height: auto">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width: 100%">
            <tr>
                <td>
                    <iFg:iFlexGrid ID="ifgEdiSettings" runat="server" AllowStaticHeader="True" DataKeyNames="EDI_STTNGS_ID"
                        Width="100%" PageSize="10" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1"
                        HorizontalAlign="Justify" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="380px" Type="Normal"
                        ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                        EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                        AddRowsonCurrentPage="False" OnAfterCallBack="OnAfterCallBack" OnBeforeCallBack=""
                        AllowSearch="True" AllowPaging="True" UseIcons="True" SearchButtonCssClass="btn btn-small btn-info"
                        AddButtonCssClass="btn btn-small btn-success" DeleteButtonCssClass="btn btn-small btn-danger"
                        RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonCssClass="btn btn-small btn-danger"
                        ClearButtonCssClass="btn btn-small btn-success" AutoSearch="True" AllowRefresh="True"
                        Mode="Insert" UseActivitySpecificDatasource="True">
                        <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <Columns>
                            <iFg:LookupField DataField="CSTMR_CD" ForeignDataField="CSTMR_ID" HeaderText="Customer *"
                                HeaderTitle="Customer" PrimaryDataField="CSTMR_ID" SortAscUrl="" SortDescUrl=""
                                AllowSearch="true">
                                <Lookup DataKey="CSTMR_CD" DependentChildControls="" HelpText="87,CUSTOMER_CSTMR_CD" iCase="Upper" OnClientTextChange=""
                                    ValidationGroup="divEdiSettings" MaxLength="15" TableName="9" CssClass="lkp"
                                    DoSearch="True" Width="110px" ClientFilterFunction="" AllowSecondaryColumnSearch="true"
                                    SecondaryColumnName="CSTMR_NAM" AutoSearch="true">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                                        <Inp:LookupColumn ColumnCaption="Check Digit" ColumnName="CHK_DGT_VLDTN_BT" Hidden="false" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                        LkpErrorMessage="Invalid Customer. Click on the list for valid values" ReqErrorMessage="Customer Required" CustomValidation ="true" 
                                        CustomValidationFunction="validateCustomer" Validate="True" />
                                </Lookup>
                                <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:LookupField DataField="FLE_FRMT_CD" ForeignDataField="FLE_FRMT_ID" HeaderText="File Type *"
                                HeaderTitle="File Type" SortAscUrl="" SortDescUrl="" PrimaryDataField="ENM_ID">
                                <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText="555,EDI_FLE_FRMT_ID" iCase="Upper" OnClientTextChange=""
                                    TableName="30" ValidationGroup="divEdiSettings" CssClass="lkp" DoSearch="True"
                                    Width="120px" ClientFilterFunction="">
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnCaption="ENM_ID" ColumnName="ENM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="File Type" ColumnName="ENM_CD" Hidden="False" />
                                    </LookupColumns>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                        IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                        OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" LkpErrorMessage="Invalid File Type. Click on the List for Valid Values"
                                        ReqErrorMessage="File Type Required" Validate="True" IsRequired="True" CustomValidation="false"
                                        CustomValidationFunction="" />
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="120px" />
                            </iFg:LookupField>
                            <iFg:TextboxField DataField="TO_EML" HeaderText="To Mail *" HeaderTitle="To Mail" SortAscUrl=""
                                SortDescUrl="" HeaderStyle-Width="40px">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="556,EDI_SETTINGS_TO_EML" iCase="Lower" OnClientTextChange=""
                                    ValidationGroup="">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="true"
                                        RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$"
                                        IsRequired="true" ReqErrorMessage="Email is Required" RegexValidation="true"
                                        RegErrorMessage="Invalid Email Format" ValidationGroup="divEdiSettings" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="500px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="CC_EML" HeaderText="CC Mail *" HeaderTitle="CC Mail" SortAscUrl=""
                                SortDescUrl="" HeaderStyle-Width="40px">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="209_CUSTOMER_CC_EML" iCase="Lower" OnClientTextChange=""
                                    ValidationGroup="">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="true"
                                        RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$"
                                        IsRequired="true" ReqErrorMessage="Email is Required" RegexValidation="true"
                                        RegErrorMessage="Invalid Email Format" ValidationGroup="divEdiSettings" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="250px" Wrap="True" />
                            </iFg:TextboxField>

                            <iFg:TextboxField DataField="SBJCT_VCR" HeaderText="Subject *" HeaderTitle="Subject" SortAscUrl="" SortDescUrl="" HeaderStyle-HorizontalAlign="Center">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="397,CUSTOMER_EMAIL_SETTING_SBJCT_VCR" MaxLength="11" OnClientTextChange="" ValidationGroup="divEdiSettings">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="True" CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Subject Required" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" />
                            </iFg:TextboxField>

                           <%-- <iFg:DateField DataField="GNRTD_DT_TM" HeaderText="Genarated Date *" HeaderTitle="Genarated Date"
                                SortAscUrl="" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy HH:mm}" HtmlEncode="false">
                                <iDate HelpText="399" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                    ValidationGroup="" MaxLength="11" CssClass="txt" Width="80px">
                                    <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                        ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="True"
                                        CompareValidation="true" CmpErrorMessage="Test Date cannot be greater than current Date."
                                        LkpErrorMessage="Invalid Test Date. Click on the calendar icon for valid values"
                                        ReqErrorMessage="Genarated Date Required" Validate="True" CustomValidationFunction=""
                                        CustomValidation="true" />
                                </iDate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="90px" Wrap="True" HorizontalAlign="Left" />
                            </iFg:DateField>--%>

                              <iFg:TextboxField DataField="GNRTN_TM" HeaderText="Generation Time *" HeaderTitle="Generation Time" SortAscUrl="" SortDescUrl="" HeaderStyle-HorizontalAlign="Center">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="394,CUSTOMER_EMAIL_SETTING_GNRTN_TM" MaxLength="11" OnClientTextChange="" ValidationGroup="tabEmail">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true" ValidationGroup="divEmailSetting" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="true" ReqErrorMessage="Generation Time Required" CustomValidation="false" RegexValidation="true" RegErrorMessage="Invalid Generation Time. Enter in hh:mm format and must be between 00:00 to 23:59" RegularExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="70px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:CheckBoxField DataField="ACTV_BT" HeaderText="" HeaderTitle="Select" HelpText=""
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
    <div style="width: 100%;">
        <table style="width: 100%;">
            <tr style="width: 100%;">
                <%--   <td class="btncorner" style="text-align: center; width: 17%;">
                    <a href="#" id="hlnkPrint" onclick="printDocument();return false;" class="btn btn-small btn-success"
                        style="font-weight: bold" runat="server"><i class="icon-save"></i>&nbsp;Generate Document</a>
                </td>--%>
                <td></td>
                <td style="text-align: center; width: 66%;">
                    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();" />
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
