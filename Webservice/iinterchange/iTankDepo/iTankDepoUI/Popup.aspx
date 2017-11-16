<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Popup.aspx.vb" Inherits="Common_Popup" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>iTankDepo</title>
    <script language="javascript" type="text/javascript" src="Script/UI/Home.js"></script>
    <script language="javascript" type="text/javascript" src="Script/Callback.js"></script>
    <script language="javascript" src="Script/UI/Settings.js" type="text/javascript"></script>
    <script language="javascript" src="Script/UI/ModalWindow.js" type="text/javascript"></script>
    <script language="javascript" src="Script/UI/PopupWindow.js" type="text/javascript"></script>
    <script language="javascript" src="Script/Common.js" type="text/javascript"></script>
    <script language="javascript" src="Script/UI/ModalWindow.js" type="text/javascript"></script>
    <script src="Script/UI/jquery.js" type="text/javascript"></script>
    <script src="Script/UI/jquery.ui.js" type="text/javascript"></script>    
    <script src="Script/UI/jquery.corner.js" type="text/javascript"></script>
</head>
<body onload="loadContent();" style="margin=0px">
    <form id="form1" runat="server">
    <div id="shadow" class="opaqueLayerPopup">
    </div>
    <div id="question" class="questionLayer">
        <table id="tblmodalwindow" style="width: 150px; height: 30px">
            <tr>
                <td id="tdinfomodalwindowtitleimage" style="width: 40px;">
                  <i id="imgloading" class="icon-spinner icon-spin icon-large"></i>
                </td>
                <td id="tdmodalwindowtitle" class="loadingTitle" style="width: 110px;">
                </td>
            </tr>
        </table>
    </div>
    <table style="height: 100%;" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="0">
        <tr id="trPane" valign="top">
            <td id="tdWf" style ="height:100%">
                <iframe id="wfFrame" width="100%" height="700px" name="wfFrame" src="about:blank"
                    scrolling="no" tabindex="-1" frameborder="0"></iframe>
            </td>
        </tr>       
    </table>
    
    <div id="modalframe" name="modalframe" class="modallayout" style="display: none;
        margin-left: 5px;">
        <table style="width: 100%; height: 100%">
            <tr>
                <td style="width: 100%; height: 2%">
                    <div id="modalframetitle" class="modallayouttitle">
                    </div>
                </td>
                <td style="width: 100%; height: 2%">                   
                    <div class="frameclose">
                        <img src="Styles/Default/Images/close_white.png" id="closeimage" class="frameclose" style="border: 0px; cursor: hand" onmouseover="this.className='framecloseover'" onmouseout="this.className='frameclose'" onclick="closeModalWindow();" alt="Close" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%; height: 90%">
                    <iframe id="fmModalFrame" src="" width="100%" height="100%" frameborder="0" scrolling="auto"
                        style="margin-top: 2px"></iframe>
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
                        <div class="confirmwindowcloseimg" style="padding: 2px">
                            <img src="Images/close.png" id="imgConfirmationCancel" onclick="hideConfirmMessagePane('CANCEL');return false;" /></div>
                        <div style="padding: 2px; text-align: center;">
                            Confirm
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="center" id="td4">
                    <img src="Images/mw.png" />
                </td>
                <td valign="middle" align="center" id="spnConfirmMessage" class="confirmmodalwindowtitle">
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
                    <ul style="width: 60px">
                          <li class="btncorner" data-corner="8px"><button id="btnYes" name="btnYes" class="btn" onmouseover="this.className='sbtn'" onmouseout="this.className='btn'" href="#" onclick="yesClick();return true;">
                            Yes</button></li>
                        <li class="btncorner" data-corner="8px"><button id="btnNo" name="btnNo" class="btn" onmouseover="this.className='sbtn'" onmouseout="this.className='btn'" href="#" onclick="noClick();return false;">
                            No</button></li>
                    </ul>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
