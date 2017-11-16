<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dashboard.aspx.vb" Inherits="Dashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="Script/UI/jquery-1.9.1.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
        $(window).load(function () {
            $("#divLoading").fadeOut("slow");
           // alert("windowouter" + $(window.parent).outerHeight());
            if (screen.width < 1920) {
                reSizePane();
            }
        });
    

        $(window.parent).resize(function () {

            if ($(window.parent).outerHeight() > 800) {
                $("#ReportFrame").height("800");
            }
            else {
               $("#ReportFrame").height("470");

            }
          
        });

        function reSizePane() {
            if ($(window.parent).outerHeight() > 800) {
                $("#ReportFrame").height("800");
            }
            else {
              $("#ReportFrame").height("470");

            }

        }
    </script>
</head>
<body style="overflow: hidden">
  <form id="form1" runat="server">
    
    <div id="divContainer" style="width: 100%; height: 100%; overflow: hidden;" 
        runat="server" >
             <%-- <iframe id="ReportFrame" runat="server" src="" frameborder="0" style="width: 100%;
            height: 800px"  scrolling ="no" ></iframe>--%>
             <div id='divLoading' align="center">
                        <div class="loading" align="center" id="divImageLoader">
                            <img src="images/RTO_Loader.GIF" alt="Loading..." />Please Wait..
                        </div>
                    </div>

             <iframe id="ReportFrame" runat="server" src="" frameborder="0" style="width: 100%;
            height: 800px"  scrolling ="auto" ></iframe>
    </div>
    </form>
</body>
</html>

