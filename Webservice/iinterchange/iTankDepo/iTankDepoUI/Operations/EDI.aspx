<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EDI.aspx.vb" Inherits="Operations_EDI" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../Script/UI/jquery.js" type="text/javascript" ></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="overflow:auto">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">EDI</span>
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
    </div>
    <div id="tabEDI" class="tabdisplay" style="height: 200px">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="center" style="margin-top: 35px">
            <tr>
                <td>
                    <label id="lblEdiFormat" runat="server" class="lbl">
                        EDI Format</label>
                </td>
                <td>
                    <Inp:iLookup ID="lkpEDIFormat" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True" iCase="Upper" MaxLength="15" TabIndex="1" TableName="30" HelpText="194,ENUM_ENM_CD" ClientFilterFunction="" ToolTip="EDI Format" OnClientTextChange="" Width="120px">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="lkpEDIFormat" Hidden="False" ColumnCaption="EDI Format" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="200px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid EDI Format. Click on the list for valid values" Operator="NotEqual" ReqErrorMessage="EDI Format Required." Type="String" Validate="true" ValidationGroup="tabEDI" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblCustomer" runat="server" class="lbl">
                        * Customer</label>
                </td>
                <td>
                    <Inp:iLookup ID="lkpCustomer" runat="server" CssClass="lkp" DataKey="CSTMR_CD" DoSearch="True" iCase="Upper" TabIndex="2" TableName="9" HelpText="87,CUSTOMER_CSTMR_CD" Width="120px" ClientFilterFunction="applyDepoFilter">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="CSTMR_CD" Hidden="False" />
                            <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle" IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="200px" VerticalAlign="Top" />
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True" LkpErrorMessage="Invalid Customer Code. Click on the list for valid values" ReqErrorMessage="Customer Required." Validate="True" ValidationGroup="tabEDI" CustomValidation ="true" CustomValidationFunction="Check031KeyExist" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblGateIn" runat="server" class="lbl">
                        GateIn</label>
                </td>
                <td>
                    <asp:CheckBox ID="chkGateIn" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblRepairEstimate" runat="server" class="lbl">
                        Estimate</label>
                </td>
                <td>
                    <asp:CheckBox ID="chkRepairEstimate" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblGateOut" runat="server" class="lbl">
                        GateOut</label>
                </td>
                <td>
                    <asp:CheckBox ID="chkGateOut" runat="server" />
                </td>
            </tr>
                <tr>
                <td> <label id="lblRepairComplete" runat="server" class="lbl">
                                   RepairComplete</label>
                </td>
                <td>
                    <asp:CheckBox ID="chkRepairComplete" runat="server" />                    
                </td>
                </tr> 
        </table>
    </div>
    <div style="height: 22px">
    </div>
    <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();" />
    </form>
</body>
</html>
