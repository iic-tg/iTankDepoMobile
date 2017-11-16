<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Attachment.ascx.vb" Inherits="UserControls_Attachment" %>
<div id="frmDiv" style="height:145px">
    <table style="height: 100%; width: 600px;">
        <tr>
            <td style="height: 5%; width: 100%;">           
                <a id="aAttachFile" runat="server" class="lbl" name="aAttachFile" onclick="attachFile();return false;"
                    href="#">Attach File</a>
            </td>
        </tr>
        <tr>
            <td style="height: 5%; width: 100%;">
                <span id="divErrorMessage" class="lbl"></span>
            </td>
        </tr>
        <tr>
            <td style="height: 85%; width: 100%;">
                <div id="AttachedLinks" style="height: 100%; width: 100%; overflow: auto;">
                </div>
            </td>
        </tr>
    </table>
</div>
<input type="hidden" name="MAX_FILE_SIZE" value="10000000">
<iframe name="fmAttachFile" frameborder="0" src="~/UserControls/AttachFile.aspx"
    id="fmAttachFile" marginheight="0" marginwidth="0" width="0px" height="0px" scrolling="no"
    runat="server"></iframe>
