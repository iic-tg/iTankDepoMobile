﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PhotoUpload.aspx.vb" Inherits="Operations_PhotoUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server" style="overflow: auto">
    <table style="width: 100%;">
        <tr>
        </tr>
        <tr>
            <td style="width: 70%; float: left; vertical-align: top;">
                <div id="divFile" style="width: 100%; vertical-align: top; float: left;">
                    <Inp:iLabel ID="lblHeader1" runat="server" CssClass="lbl">
              Attach Files
                    </Inp:iLabel>
                    <br />
                    <br />
                    <input type="file" id="filename" runat="server" contenteditable="false" multiple="true" />
                    <br />
                    <div style="display: none;">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" />
                    </div>
                </div>
                <div id="divFlash" style="width: 90%; vertical-align: top; float: left; margin: 0px;">
                    <fieldset class="lbl" id="fldCostSummary" style="width: 550px; height: 360px;">
                        <legend class="blbl">Select / Attach</legend>
                        <table>
                            <tr>
                                <td>
                                    <fUpload:FlashUpload ID="flashUpload" runat="server" UploadPage="PhotoUpload.aspx" 
                                        OnUploadComplete="onUploadComplete();" FileTypeDescription="Images" FileTypes="*.gif; *.png; *.jpg; *.jpeg; *.txt; *.pdf; *.doc;*.docx; *.xls; *.xlsx" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>                
            </td>
            <td style="width: 27%; float: right; vertical-align: top;">
                <div id="divMessage" runat="server" style="display: none; float: right;">
                    <asp:HyperLink ID="hlBrowserCompatible" runat="server" Target="_blank" Visible="true"
                        NavigateUrl="https://get.adobe.com/flashplayer/otherversions/">Download Flash Player</asp:HyperLink>
                </div>
                <br />
                <div id="divAttach" style="display: block;">
                    <fieldset class="lbl" id="Fieldset1" style="width: 94%; height: 348px;">
                        <legend class="blbl">Attached Files List</legend>
                        <div id="divNoImageUpload" style="display: none; vertical-align: middle; text-align: center;
                            font-weight: bold;">
                            No Files Attached.
                        </div>
                        <div id="lstUpload" style="overflow-y: auto; height: 300px; float: left;">
                            <div class="MultiFile-list" id="filename_wrap_list">
                                <div class="MultiFile-wrap" id="filename_wrap">
                                    <div id="photoList" class="MultiFile-label" runat="server" style="float: left; width: 100%;
                                        font-family: Arial; font-size: 12px;">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </td>
        </tr>     
          <tr>
            <td>
               <asp:Label ID="lblHelp" runat="server" Font-Bold ="true"   >
                  File Formats(gif, png, jpg, jpeg, txt, pdf, doc, docx, xls, xlsx) only will be allowed.
               </asp:Label> 
            </td>
        </tr>     
    </table>
    <table class="button" align="center" style="width: 120px; vertical-align: top;">
        <tr>
            <td align="center">
                <ul>
                    <li class="btn btn-small btn-success"><a href="#" data-corner="8px" id="A1" style="text-decoration: none;
                        color: White; font-weight: bold" class="icon-remove" runat="server" onclick="onClosePhotoUpload();return false;">
                        &nbsp;Close</a></li>
                </ul>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnTempPath" runat="server" />
    <asp:HiddenField ID="hdnTempFileName" runat="server" />
    <asp:HiddenField ID="hdnRepairEstimteId" runat="server" />
    <asp:HiddenField ID="hdnPageName" runat="server" />
    <asp:HiddenField ID="hdnInvalidFileName" runat="server" /> 
    </form>
</body>
</html>
