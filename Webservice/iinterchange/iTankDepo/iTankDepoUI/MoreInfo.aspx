<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MoreInfo.aspx.vb" Inherits="MoreInfo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>More Info</title>
    <script language="javascript" src="Script/Common.js" type="text/javascript"></script>
</head>
<body class="popuppage" onload="setFocusMoreInfoTextBox();">
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table style="width: 246px">
            <tr>
                <td style="height: 32px">
                    <Inp:iTextBox ID="txtDescription" onpaste="checkDataLength(this);return false;" runat="server"
                        CssClass="txt" TextMode="MultiLine" TabIndex="0" Height="240px" Width="375px">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div class="button" style="width: 100px">
                        <ul>
                            <li class="btncorner" data-corner="8px">
                                <button  id="btnClose" class="btn" onmouseover=""
                                    onmouseout="this.className='btn'" onclick="ppsc().closeModalWindow();return false;">
                                    Close</button></li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
