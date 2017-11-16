<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RepairEstimateMassApplyInput.aspx.vb"
    Inherits="Operations_RepairEstimateMassApplyInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server">
    <div style="height: 120px;">
        <table>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
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
                    <Inp:iLabel ID="lblParty" runat="server" CssClass="lbl">
                    Responsible Party
                    </Inp:iLabel>
                     <Inp:iLabel ID="Reqparty" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpParty" runat="server" CssClass="lkp" DataKey="ENM_CD" DoSearch="True"
                        iCase="Upper" MaxLength="10" TableName="56" HelpText="541,ENUM_ENM_CD" ToolTip="Responsible Party"
                        ClientFilterFunction="" ReadOnly="false" AllowSecondaryColumnSearch="false" SecondaryColumnName=""
                        TabIndex="1">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="ENM_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="ENM_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="ENM_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="Left" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="true"
                            LkpErrorMessage="Invalid Responsible Party. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Responsible Party Required" Type="String" Validate="True"
                            ValidationGroup="" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
            </tr>
        </table>
    </div>
    <table align="center">
        <tr>
            <td align="right">
                <a href="#" id="btnApply" onclick="onApplyMassApplyInput(); return false;" class="btn btn-small btn-success"
                    runat="server" style="font-weight: bold; vertical-align: middle"><i class="icon-save">
                    </i>&nbsp;Apply</a>
            </td>
            <td align="left">
                <a href="#" id="btnCancel" onclick="onCancelMassApplyInput(); return false;" class="btn btn-small btn-danger"
                    runat="server" style="font-weight: bold; vertical-align: middle"><i class="icon-remove">
                    </i>&nbsp;Close</a>
            </td>
        </tr>
    </table>
    </form>
</body>

</html>
