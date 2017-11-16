<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PhotoUpload.aspx.vb" Inherits="Admin_PhotoUpload" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
 
    <script src="../Script/Common.js" type="text/javascript"></script>
    <script language="javascript">
        function frmPhotoUploadSubmit() {

            // Simply, submit the form
            document.frmPhotoUpload.submit();
          //  var path = document.getElementById("hdnLogoPath").value;
              //   parent.document.Script.fnSetImageUrl(path);
        }
    </script>

</head>
<body>
    <form id="frmPhotoUpload" runat="server" method="post" action="PhotoUpload.aspx" style="overflow:auto">
    <div>
        <Inp:iLabel ID="lblMsg" CssClass="lbl" runat="server"></Inp:iLabel>
        <input type="file" runat="server" id="imagebrowse" name="imagebrowse" style="visibility: hidden;" onchange="frmPhotoUploadSubmit()" />
        <input type="button" runat="server" id="btnSubmit" name="btnSubmit" 
            style="visibility: hidden;" />
        <input id="hdnLogoPath" name="hdnLogoPath" type="hidden" runat="server" />
        <br />
    </div>
    </form>
</body>
</html>
