﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EquipmentUpdateReason.aspx.vb" Inherits="Operations_EquipmentUpdateReason" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="overflow:auto"> 
    <div>
     <br />
    <table align="center">
        <tr align="center">
            <td align="center">
                <Inp:iTextBox ID="txtReason" runat="server" CssClass="txt" Height="100px" HelpText="486" iCase="None" MaxLength="1000" TabIndex="0" TextMode="MultiLine" ToolTip="Reason" Width="400px">
                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="true" ReqErrorMessage="Update Reason Required" Validate="true" />
                </Inp:iTextBox>
            </td>
        </tr>
        <tr align="center" valign="middle">
            <td align="center" valign="middle">
                <a href="#" id="btnSave" onclick="updateEquipmentReason(); return false;" class="btn btn-small btn-success" runat="server" style="font-weight: bold; vertical-align: middle"><i class="icon-save"></i>&nbsp;Save</a>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
