<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CustomerEDISetting.aspx.vb"
    Inherits="Masters_EDIGenerationInfo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="overflow:auto">
    <div style="align: center">
        <table border="0" class="tblstd" align="center">
            <tr>
                <td>
                    <label id="lblEDIemailid" runat="server" class="lbl">
                        * To Email ID</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtEDIemailid" runat="server" CssClass="txt" TabIndex="1" HelpText=""
                        MaxLength="500" TextMode="MultiLine" iCase="none" ReadOnly="False" Width="150px"
                        Height="40px">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            ValidationGroup="tabCustomer" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="true" ReqErrorMessage="Email ID Required" CustomValidation="false"
                            RegexValidation="true" RegErrorMessage="Invalid Email Format" RegularExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblGenerationFormat" runat="server" class="lbl">
                        * Generation Format</label>
                </td>
                <td>
                    <Inp:iLookup ID="lkpGenerationFormat" runat="server" CssClass="lkp" DataKey="ENM_CD"
                        DoSearch="True" iCase="Upper" MaxLength="15" TabIndex="2" TableName="31" HelpText="194,ENUM_ENM_CD"
                        ClientFilterFunction="" ToolTip="EDI Format" OnClientTextChange="" Width="130px">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="lkpGenerationFormat" Hidden="False"
                                ColumnCaption="EDI Format" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="200px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Generation Format. Click on the list for valid values"
                            Operator="NotEqual" ReqErrorMessage="EDI Generation Format Required." Type="String"
                            Validate="true" ValidationGroup="tabEDI" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblGenerationTime" runat="server" class="lbl">
                        * Generation Time</label>
                </td>
                <td>
                    <Inp:iTextBox ID="txtGenerationTime" runat="server" CssClass="txt" TabIndex="3" HelpText=""
                        MaxLength="5" TextMode="SingleLine" iCase="Upper" ReadOnly="False" Width="130px">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true"
                            ValidationGroup="tabCustomer" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="true" ReqErrorMessage="Genetarion Time Required" CustomValidation="false"
                            RegexValidation="true" RegErrorMessage="Invalid Generation Time.Period cannot be greater than 24 hours"
                            RegularExpression="(?:^([2][0-3]|[01]?[0-9]):)?([0-5][0-9])" />
                         
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblEDIFormat" runat="server" class="lbl">
                        * EDI Format</label>
                </td>
                <td>
                    <Inp:iLookup ID="lkpEDIFormat" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                        iCase="Upper" MaxLength="15" TabIndex="4" TableName="30" HelpText="194,ENUM_ENM_CD"
                        ClientFilterFunction="" ToolTip="EDI Format" OnClientTextChange="" Width="130px">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="lkpEDIFormat" Hidden="False"
                                ColumnCaption="EDI Format" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="200px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid EDI Format. Click on the list for valid values"
                            Operator="NotEqual" ReqErrorMessage="EDI Format Required." Type="String" Validate="true"
                            ValidationGroup="tabEDI" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
            </tr>
            <tr align="center">
                <td align="center">
                </td>
                <td align="center">
                    <div class="button">
                        <ul>
                            <li><a href="#" tabindex="5" id="btnSubmit" class="btncorner" data-corner="8px" onmouseover="this.className='sbtn'"
                                onmouseout="this.className='btn'" onclick="submitPage();return false;">Submit</a></li>
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
