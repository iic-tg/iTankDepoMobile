<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TransporterCopyRoute.aspx.vb"
    Inherits="Masters_TransporterCopyRoute" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage()">
    <form id="form1" runat="server">
    <div>
        <div id="divCopyRoute" style="height: 120px;">
            <table class="tblstd" cellpadding="2" cellspacing="2" border="0">
            <tr>
                <td colspan="4"></td>
            </tr>
             <tr>
                <td colspan="4"></td>
            </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <label id="lblTransporterCode" runat="server" class="lbl">
                            Transporter Code
                        </label>
                        <Inp:iLabel ID="reqTransporterCode" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                    </td>
                    <td>
                        <Inp:iLookup ID="lkpTransporter" runat="server" CssClass="lkp" DataKey="TRNSPRTR_CD"
                            DoSearch="True" iCase="Upper" TabIndex="6" TableName="79" HelpText="522,TRANSPORTER_TRNSPRTR_CD"
                            ClientFilterFunction="filterCurrentCode" ToolTip="Transporter Code" OnClientTextChange=""
                            Width="100px" Visible="true">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="TRNSPRTR_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnName="TRNSPRTR_CD" ControlToBind="lkpTransporter" Hidden="False"
                                    ColumnCaption="Code" />
                                <Inp:LookupColumn ColumnName="TRNSPRTR_DSCRPTN" Hidden="False" ColumnCaption="Description" />
                            </LookupColumns>
                            <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                            <LookupGrid CurrentPageIndex="0" PageSize="5" Width="300px" VerticalAlign="Top" />
                            <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="true" LkpErrorMessage="Invalid Transporter Code. Click on the list for valid values"
                                Operator="NotEqual" ReqErrorMessage="Transporter Code Required" Type="String"
                                Validate="true" ValidationGroup="" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        </Inp:iLookup>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <label id="lblCopyCustomerRate" runat="server" class="lbl">
                            Copy Customer Rate
                        </label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkCustomerRate" runat="server" Text="" CssClass="chk" ToolTip="Enable for Customer Rate"
                            TabIndex="2" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <table align="center">
        <tr>
        <td>
           <div align="center">
            <ul style="float: left; vertical-align: top;">
                <li class="btn btn-small btn-success"><a href="#" data-corner="8px" id="hypApplyRoute"
                    onclick="onApplyRoute(); return false;" class="icon-save" style="text-decoration: none;
                    color: White; font-weight: bold; float: left; vertical-align: middle; height: 12px;
                    width: 50px;" runat="server" tabindex="22">&nbsp;<span style="font-family: Arial;
                        line-height: 10px; width: 200px;">Apply</span></a></li>
             </ul>
            </div>
        </td>
        </tr>
     
        </table>
    </div>
    </form>
</body>
</html>
