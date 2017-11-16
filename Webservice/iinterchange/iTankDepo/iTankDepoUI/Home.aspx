<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="Home" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>iDepo</title>
    <meta charset="utf-8">
    <link rel="shortcut icon" href="Images/icon.ico">

</head>
<body style="margin: 0px;" onload="onHomeLoad();" onbeforeunload="onBeforeLoad();"
    onunload="onUnload();">
    <form id="formHome" runat="server">
    <div>
        <div id="shadow" class="opaqueLayer">
        </div>
        <div id="confirmshadow" class="opaqueConfirmLayer">
        </div>
        <div id="question" class="questionLayer">
            <table id="tblmodalwindow">
                <tr>
                    <td id="tdinfomodalwindowtitleimage" style="width: 30px;">
                        <i id="imgloading" class="icon-spinner icon-spin icon-large"></i>
                    </td>
                    <td class="slbl">
                        <span id="tdmodalwindowtitle"></span>
                    </td>
                </tr>
            </table>
        </div>
        <center>
            <div id="layoutContainer" class="ui-layout-container" style="width: 100%">
                <div class="ui-layout-north">
                    <div>
                        <div>
                            <table align="center" class="headerMenu" width="100%;height:100%">
                                <tr valign="top">
                                    <td width="60%">
                                        <img src="" id="customerlogo" height="40" runat="server" align="left" />
                                    </td>
                                    <td width="40%" align="right">
                                        <table>
                                           <tr>
                                                <td>
                                                 <%--   <a href="#" id="aDashboard" class="dashboard" title="Dashboard" onclick="onMenuClick('Dashboard.aspx');"></a>--%>
                                                 <table>
                                                 <tr>
                                                 <td>
                                                   <asp:Label ID="lblWelcome1" CssClass="wlbl" runat="server" Text="Welcome "></asp:Label>
                                                    <asp:Label ID="lblWelcome2" CssClass="ylbl" runat="server" ForeColor="#FFEF97"></asp:Label>
                                                 </td>
                                                 <td>
                                                  <asp:Label ID="lblDepotCode2" CssClass="ylbl" runat="server" ForeColor="#FFEF97"></asp:Label>
                                                  <asp:Label ID="lblWelcomeTitle" runat="server" style="display:none"></asp:Label>
                                                 </td>
                                                 </tr>
                                                 </table>
                                                  
                                                   
                                                    <%--<br />--%>
                                                    <table cellspacing="1px" cellpadding="1px" border="0" class="tblstd">
                                                        <tr valign="top">
                                                            <td>
                                                                <div class="icolnk" title="Contact Support" onmouseover="toggleStyle(this,'icolnko');"
                                                                    onmouseout="toggleStyle(this,'icolnk');" style="cursor: pointer">
                                                                    <i class="icon-phone"></i>
                                                                </div>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <div class="icolnk" title="User Manual" onmouseover="toggleStyle(this,'icolnko');"
                                                                    onmouseout="toggleStyle(this,'icolnk');" style="cursor: pointer">
                                                                    <i class="icon-question"></i>
                                                                </div>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <div class="icolnk" onclick="ShowTab('Home.aspx','HomeTab');return false;" title="Home"
                                                                    onmouseover="toggleStyle(this,'icolnko');" onmouseout="toggleStyle(this,'icolnk');"
                                                                    style="cursor: pointer">
                                                                    <i class="icon-home"></i>
                                                                </div>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <div id="divProfile" class="icolnk" runat="server" title="Profile" onmouseover="toggleStyle(this,'icolnko');"
                                                                    onmouseout="toggleStyle(this,'icolnk');" style="cursor: pointer">
                                                                    <i class="icon-user"></i>
                                                                </div>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <div class="icolnk" onclick="onMenuClick('ChangePassword.aspx');" title="Change Password"
                                                                    onmouseover="toggleStyle(this,'icolnko');" onmouseout="toggleStyle(this,'icolnk');"
                                                                    style="cursor: pointer">
                                                                    <i class="icon-lock"></i>
                                                                </div>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <div class="icolnk" onclick="onMenuClick('Logout.aspx');" title="Logout" onmouseover="toggleStyle(this,'icolnko');"
                                                                    onmouseout="toggleStyle(this,'icolnk');" style="cursor: pointer">
                                                                    <i class="icon-signout"></i>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <div class="productlogo" id="productlogo">
                                                    </div>
                                                    <span style="font-size: small" id="spnIndicator"><i id="lblindicator" class="icon-circle">
                                                    </i></span>
                                                    <asp:Label ID="lblVersion" CssClass="ylbl" runat="server" Text="Version: 2.0"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="sicolnk" align="right" style="cursor: pointer; position: absolute; top: 85px;
                    left: 98%; margin-right: 5px; z-index: 99999; display: none" onmouseover="toggleStyle(this,'sicolnko');"
                    onmouseout="toggleStyle(this,'sicolnk');">
                    <i title="Restore" class="icon-resize-small" id="dockListPaneDepot" style="display: block"
                        onclick="fitScreen(this);return false;"></i>
                </div>
                <div id="divSideBar" class="ui-layout-west">
                    <div id="accordionRI">
                        <h5>
                            &nbsp;Recent Items</h5>
                        <div>
                            <ul id="ulRecentItems" style="padding-left: 2px"">
                            </ul>
                        </div>
                    </div>
                    <div id="accordionQL">
                        <h5>
                            &nbsp;Quick links</h5>
                        <div>
                            <ul id="ulQuickLinks" style="margin-left: 2px">
                            </ul>
                        </div>
                    </div>
                    <div id="accordionFav">
                        <h5>
                            &nbsp;Favorites</h5>
                        <div>
                            <ul id="ulFavourites" style="margin-left: 0px; padding-left:2px;">
                            </ul>
                        </div>
                    </div>
                    <div id="accordionMaster">
                        <h5>
                            &nbsp;Masters</h5>
                        <div>
                            <ul id="ulMasters" style="margin-left: 2px">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="ui-layout-center" id="divCenterBar">
                    <div class="ui-layout-south">
                        <table cellpadding="0" cellspacing="0" style="margin: 2px; width: 99.6%">
                            <tr>
                                <td>
                                    <div class="colortabs">
                                        <ul id="deskTabs">
                                            <li id="liHomeTab" class="current" style="margin-left: 1px" data-corner="6px"><a
                                                href="#" rel="Content" onclick="ShowTab('Home.aspx','HomeTab');return false;"
                                                title="Home"><span>Home</span></a> </li>
                                        </ul>
                                    </div>
                                    <div id="colortabsline">
                                        &nbsp;</div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divMainBar" class="ui-layout-center" style="overflow: auto; border: 1px solid #C0C0C0">
                        <div id="deskTabPages" style="width: 100%;overflow: auto;">
                            <div id="Home">
                                <table id="tblHomeTab" style="margin: 2px; width: 99.6%; overflow:scroll;" >
                                    <tr id="trDashboardPane" valign="top">
                                        <td id="tdDb" valign="top" style="width: 100%; height: 100%; display: block">
                                            <iframe id="dbFrame" width="100%" height="800" name="dbFrame" runat ="server" 
                                                tabindex="-1" scrolling="no" frameborder="0"></iframe>
                                                <%-- <iframe id="ReportFrame" runat="server" src="" frameborder="0" style="width: 100%;
            height: 800px"  scrolling ="no" ></iframe>--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="ui-layout-south">
                    <div>
                        <table cellpadding="0" cellspacing="0" style="margin: 2px; width: 99.6%">
                            <tr>
                                <td align="left" style="width: 25%" class="footerMenu">
                                    &nbsp;
                                    <asp:Label ID="lblLastLogin" runat="server" Text="Last Login:"></asp:Label>
                                    <asp:Label ID="lblLastLoginDt" runat="server"></asp:Label>
                                </td>
                                <td align="center" style="width: 50%" class="footerMenu">
                                    Depot Management Tool powered by <b><i><a style="font-size: 8pt" href="http://www.iinterchange.com"
                                        target="_blank">iInterchange Systems Pvt. Ltd.</a></i></b> Your Indian Technology
                                    Partner.
                                </td>
                                <td align="right" style="width: 20%" class="footerMenu">
                                    <span align="right">Local Time : <span id="spnTime"></span>&nbsp;&nbsp; </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </center>
        <div id="modalframe" name="modalframe" class="modallayout" style="display: none;
            margin-left: 5px;">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td style="width: 100%; height: 2%">
                        <div id="modalframetitle" class="modallayouttitle">
                        </div>
                    </td>
                    <td style="width: 100%; height: 2%">
                        <a id="closeimage" onclick="closeModalWindow();return false;" class="icolnk" onmouseover="toggleStyle(this,'icolnko');"
                            onmouseout="toggleStyle(this,'icolnk');" href="#" style="text-decoration: none;"
                            title="Close"><i class="icon-remove"></i></a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%; height: 90%">
                        <iframe id="fmModalFrame" src="" width="100%" height="95%" frameborder="0" scrolling="auto"
                            style="margin-top: 2px" name="fmModalFrame"></iframe>
                    </td>
                </tr>
            </table>
        </div>
        <div id="confirmframe" name="confirmframe" class="confirmlayout" style="left: 0px;
            width: 405px; top: 236px; height: auto;">
            <table id="tblconfirmmodalwindow" style="width: 100%; height: 100%">
                <tr>
                    <td colspan="2">
                        <div class="confirmwindowheader">
                            <table style="width: 100%; height: 100%" class="tblstd">
                                <tr>
                                    <td align="center" width="80%">
                                        <div style="padding: 2px; font-weight: bold" align="left">
                                            Confirm
                                        </div>
                                    </td>
                                    <td align="right" width="20%">
                                        <a id="imgConfirmationCancel" onclick="hideConfirmMessagePane('CANCEL');return false;"
                                            class="icolnk" onmouseover="toggleStyle(this,'icolnko');" onmouseout="toggleStyle(this,'icolnk');"
                                            href="#" style="text-decoration: none; margin-left: 130px" title="Close"><i class="icon-remove">
                                            </i></a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center" id="td4">
                        <div style="font-size: 25pt; color: #F3E2A9">
                            <i class="icon-warning-sign"></i>
                        </div>
                    </td>
                    <td valign="middle" align="left" id="spnConfirmMessage" class="confirmmodalwindowtitle">
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle">
                    </td>
                    <td align="center" class="confirmmodalwindowtitle" valign="middle">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" style="height: 26px" valign="middle">
                    </td>
                    <td align="center" class="confirmmodalwindowtitle" style="height: 26px" valign="middle">
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle">
                    </td>
                    <td align="center" class="confirmmodalwindowtitle" valign="middle">
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="button" align="right">
                        <table align="center">
                            <tr>
                                <td>
                                    <ul style="width: 150px">
                                        <li>
                                            <button id="btnYes" class="btn btn-small btn-success" onclick="yesClick();return false;"
                                                style="width: 80px; border: 0px;">
                                                <i class="icon-ok"></i>&nbsp;Yes</button>
                                        </li>
                                        <li>
                                            <button id="btnNo" class="btn btn-small btn-danger" onclick="noClick();return false;"
                                                style="width: 80px; border: 0px;">
                                                <i class="icon-remove"></i>&nbsp;No</button>
                                        </li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="pgRef">
            <iframe style="display: none" id="HomeFrame"></iframe>
        </div>
    </div>
    <asp:HiddenField ID="hdnFavourites" runat="server" />
    <asp:HiddenField ID="WFDATA" runat="server" />
    </form>
</body>
</html>
