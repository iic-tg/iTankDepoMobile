<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AuditRemarksEntry.aspx.vb" Inherits="Operations_AuditRemarksEntry" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>   
</head>
<body>
    <form id="form1" runat="server" style="overflow:auto">
    <br />
    <table align="center">
        <tr align="center">
            <td align="center">
                <Inp:iTextBox ID="txtRemarks" runat="server" CssClass="txt" Height="100px" HelpText="486" iCase="None" MaxLength="1000" TabIndex="0" TextMode="MultiLine" ToolTip="" Width="400px">
                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="true" ReqErrorMessage="Deletion Reason Required" Validate="true" />
                </Inp:iTextBox>
            </td>
        </tr>
        <tr align="center" valign="middle">
            <td align="center" valign="middle">
                <a href="#" id="btnSave" onclick="saveEquipmentHistory(); return false;" class="btn btn-small btn-success" runat="server" style="font-weight: bold; vertical-align: middle"><i class="icon-save"></i>&nbsp;Save</a>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnTrackingID" runat="server" />
    <asp:HiddenField ID="hdnActivityName" runat="server" />
    </form>
</body>
</html>