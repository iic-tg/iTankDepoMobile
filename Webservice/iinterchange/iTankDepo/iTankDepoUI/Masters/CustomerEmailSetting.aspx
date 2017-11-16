<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CustomerEmailSetting.aspx.vb" Inherits="Masters_CustomerEmailSetting" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server" style="overflow:auto">
    <div id="divEmailSetting" style="margin: 1px; width: 970px; height: 440px;">
        <table>
            <tr>
                <td>
                    <iFg:iFlexGrid ID="ifgEmailSetting" runat="server" AllowStaticHeader="True" DataKeyNames="CSTMR_EML_STTNG_BIN" Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Center" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available" Scrollbars="None" ShowEmptyPager="False" StaticHeaderHeight="150px" Type="Normal" ValidationGroup="divEmailSetting" UseCachedDataSource="True" AutoGenerateColumns="False" EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="" AddRowsonCurrentPage="False" ShowPageSizer="False" OnAfterCallBack="" OnBeforeCallBack="" AllowAdd="False" AllowDelete="False" AllowFilter="False" AutoSearch="True">
                        <Columns>
                            <iFg:LookupField ForeignDataField="RPRT_ID" PrimaryDataField="ENM_ID" DataField="RPRT_CD" HeaderText="Report Name" HeaderTitle="Report Name" SortAscUrl="" ReadOnly="true" SortDescUrl="">
                                <Lookup DataKey="ENM_CD" Width="100px" TableName="33" MaxLength="6" DependentChildControls="" HelpText="390,RPRT_ID" OnClientTextChange="" CssClass="lkp" DoSearch="True">
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator ValidationGroup="divEmailSetting" CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True" ReqErrorMessage="Report Type Required" Validate="True" LkpErrorMessage="Invalid Report Name. Click on the List for valid values" />
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Report Name" ColumnName="* Report Name" ControlToBind="" Hidden="False"></Inp:LookupColumn>
                                    </LookupColumns>
                                </Lookup>
                                <ItemStyle Width="90px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:LookupField ForeignDataField="PRDC_FLTR_ID" PrimaryDataField="ENM_ID" DataField="PRDC_FLTR_CD" HeaderText="Period Filter *" HeaderTitle="Period Filter" SortAscUrl="" ReadOnly="false" SortDescUrl="" HeaderStyle-HorizontalAlign="Center">
                                <Lookup DataKey="ENM_CD" Width="100px" TableName="35" MaxLength="6" DependentChildControls="" HelpText="391,CUSTOMER_EMAIL_SETTING_PRDC_FLTR_ID" iCase="Upper" OnClientTextChange="fnFilter" CssClass="lkp" DoSearch="True">
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="200px" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator ValidationGroup="divEmailSetting" CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True" ReqErrorMessage="Period Filter Required" Validate="True" LkpErrorMessage="Invalid Period Filter. Click on the List for valid values"/>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Period Filter" ColumnName="ENM_CD" ControlToBind="" Hidden="False"></Inp:LookupColumn>
                                    </LookupColumns>
                                </Lookup>
                                <ItemStyle Width="90px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:LookupField ForeignDataField="PRDC_DY_ID" PrimaryDataField="ENM_ID" DataField="PRDC_DY_CD" HeaderText="Day" HeaderTitle="Day" SortAscUrl="" ReadOnly="false" SortDescUrl="" HeaderStyle-HorizontalAlign="Center">
                                <Lookup DataKey="ENM_CD" Width="100px" TableName="36" MaxLength="6" DependentChildControls="" HelpText="392,CUSTOMER_EMAIL_SETTING_PRDC_DY_ID" iCase="Upper" OnClientTextChange="" CssClass="lkp" DoSearch="True">
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="200px" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator ValidationGroup="divEmailSetting" Operator="Equal" Type="String" IsRequired="FALSE" ReqErrorMessage="" Validate="True" CustomValidateEmptyText="True" CustomValidation="True" CustomValidationFunction="validateDay"  LkpErrorMessage="Invalid Day. Click on the List for valid values"/>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Day" ColumnName="ENM_CD" ControlToBind="" Hidden="False"></Inp:LookupColumn>
                                    </LookupColumns>
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="90px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:LookupField ForeignDataField="PRDC_DT_ID" PrimaryDataField="PRDC_DT_ID" DataField="PRDC_DT_CD" HeaderText="Date" HeaderTitle="Date" SortAscUrl="" ReadOnly="false" SortDescUrl="" HeaderStyle-HorizontalAlign="Center">
                                <Lookup DataKey="PRDC_DT_CD" Width="100px" TableName="37" MaxLength="6" DependentChildControls="" HelpText="393,CUSTOMER_EMAIL_SETTING_PRDC_DT_ID" iCase="Upper" OnClientTextChange="" CssClass="lkp" DoSearch="True">
                                    <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="NotSet" Width="200px" />
                                    <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator ValidationGroup="divEmailSetting" Operator="Equal" Type="String" IsRequired="false" ReqErrorMessage="" Validate="True" CustomValidateEmptyText="true" CustomValidation="True" CustomValidationFunction="validateEmailSettingDate"  LkpErrorMessage="Invalid Date. Click on the List for valid values"/>
                                    <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <LookupColumns>
                                        <Inp:LookupColumn ColumnName="PRDC_DT_ID" Hidden="True" />
                                        <Inp:LookupColumn ColumnCaption="Date" ColumnName="PRDC_DT_CD" ControlToBind="" Hidden="False"></Inp:LookupColumn>
                                    </LookupColumns>
                                </Lookup>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="70px" Wrap="True" />
                            </iFg:LookupField>
                            <iFg:TextboxField DataField="GNRTN_TM" HeaderText="Generation Time *" HeaderTitle="Generation Time" SortAscUrl="" SortDescUrl="" HeaderStyle-HorizontalAlign="Center">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="394,CUSTOMER_EMAIL_SETTING_GNRTN_TM" MaxLength="11" OnClientTextChange="" ValidationGroup="tabEmail">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true" ValidationGroup="divEmailSetting" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="true" ReqErrorMessage="Generation Time Required" CustomValidation="false" RegexValidation="True" RegErrorMessage="Invalid Generation Time. Enter in hh:mm format and must be between 00:00 to 23:59" RegularExpression="^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="70px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="TO_EML" HeaderText="To Email *" HeaderTitle="To Email" SortAscUrl="" SortDescUrl="" HeaderStyle-HorizontalAlign="Center">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="395,CUSTOMER_EMAIL_SETTING_TO_EML" MaxLength="11" OnClientTextChange="" ValidationGroup="tabEmail">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true" ValidationGroup="divEmailSetting" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="true" ReqErrorMessage="Email ID Required" CustomValidation="false" RegexValidation="true" RegErrorMessage="Invalid Email Format" RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="250px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="CC_EML" HeaderText="CC" HeaderTitle="CC" SortAscUrl="" SortDescUrl="" HeaderStyle-HorizontalAlign="Center">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="396,CUSTOMER_EMAIL_SETTING_CC_EML" MaxLength="11" OnClientTextChange="" ValidationGroup="tabEmail">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true" ValidationGroup="divEmailSetting" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" ReqErrorMessage="" CustomValidation="false" RegexValidation="true" RegErrorMessage="Invalid Email Format" RegularExpression="^[\W]*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4}[\W]*,{1}[\W]*)*([\w+\-.&%]+@[\w\-.]+\.[A-Za-z]{2,4})[\W]*$" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="140px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:TextboxField DataField="SBJCT_VCR" HeaderText="Subject *" HeaderTitle="Subject" SortAscUrl="" SortDescUrl="" HeaderStyle-HorizontalAlign="Center">
                                <TextBox CausesValidation="True" CssClass="txt" HelpText="397,CUSTOMER_EMAIL_SETTING_SBJCT_VCR" MaxLength="11" OnClientTextChange="" ValidationGroup="divEmailSetting">
                                    <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="True" CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Subject Required" />
                                </TextBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="100px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:CheckBoxField DataField="ACTV_BT" HeaderText="Active" HeaderTitle="Active" HelpText="" SortAscUrl="" SortDescUrl="" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </iFg:CheckBoxField>
                        </Columns>
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="gsitem" />
                        <AlternatingRowStyle CssClass="gaitem" />
                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="NotSet" IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                        <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <PagerStyle CssClass="gpage" Height="8px" Font-Names="Arial" HorizontalAlign="Center" />
                    </iFg:iFlexGrid>
                </td>
            </tr>
            <tr align="center">
                <td align="center">
                </td>
            </tr>
        </table>
        <table align="center">
            <tr>
                <td align="center">
                    <div class="button">
                        <ul>
                            <li class="btn btn-small btn-success"><a href="#" tabindex="5" data-corner="8px" id="btnSubmit" style="text-decoration: none; color: White; font-weight:bold" class="icon-save" runat="server" onclick="submitEmailInfo();return false;">&nbsp;Submit</a></li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnCustomerID" runat="server" />
    </form>
</body>
</html>
