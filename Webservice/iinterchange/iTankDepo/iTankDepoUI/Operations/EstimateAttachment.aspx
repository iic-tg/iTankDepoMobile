<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EstimateAttachment.aspx.vb"
    Inherits="Operations_EstimateAttachment" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Notes</title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="overflow:auto">
    <div class="topspace">
    </div>
    <div id="divFilters">
        <fieldset id="fsViewAttachments" class="blbl">
            <legend>
                <Inp:iLabel ID="ILabel6" runat="server" ToolTip="" CssClass="lbl" Font-Bold="True"
                    >View Attachments</Inp:iLabel>
            </legend>
            <table style="width: 100%" border="0" cellpadding="1" cellspacing="1" class="tblstd">
                <tr>
                    <td>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <Inp:iLabel ID="ILabel3" runat="server" ToolTip="" CssClass="lbl" Font-Bold="False"
                                        Font-Underline="False">Attachment Type:</Inp:iLabel>
                                </td>
                                <td>
                                    <select name="AttachmentType" onchange="attachmentFilterOnChange()">
                                        <option value=""></option>
                                    </select>
                                </td>
                                <td>
                                    <Inp:iLabel ID="ILabel4" runat="server" ToolTip="" CssClass="lbl" Font-Bold="False"
                                        Font-Underline="False">Header/Line:</Inp:iLabel>
                                </td>
                                <td>
                                    <select name="HeaderLine" onchange="attachmentFilterOnChange()">
                                        <option value=""></option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divAttachment" style="height: 250px; overflow-x: hidden; overflow-y: auto;">
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="divAddAttachments">
        <fieldset id="fsAddAttachments" class="blbl">
            <legend>
                <Inp:iLabel ID="ILabel5" runat="server" ToolTip="" CssClass="lbl" Font-Bold="True"
                    >Add Attachments</Inp:iLabel>
            </legend>
            <table id="tblEstimateAttachment" style="width: 100%" border="0" cellpadding="1"
                cellspacing="1" class="tblstd">
                <tr id="trSubmit">
                    <td>
                        &nbsp &nbsp<Inp:iLabel ID="ILabel2" runat="server" ToolTip="" CssClass="lbl" Font-Bold="True"
                            Font-Underline="True">Description</Inp:iLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <Inp:iTextBox ID="txtDescription" runat="server" TabIndex="-1" CssClass="txt" Width="300px"
                            ToolTip="Post" HelpText="191" Height="" TextMode="MultiLine" iCase="None" MaxLength="255">
                            <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="false"
                                ControlToCompare="" CsvErrorMessage="" CustomValidationFunction="" IsRequired="True"
                                ReqErrorMessage="Description Required." ValidationGroup="vgAttachments" Validate="True"></Validator>
                        </Inp:iTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="1" cellspacing="1" class="tblstd">
                            <tr>
                                <td>
                                    <a id="aAttachFile" class="lbl" name="aAttachFile" onclick="AddEstimateFile();return false;"
                                        href="#">Add File</a>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <Inp:iLabel ID="lblReference" class="lbl" runat="server">Reference</Inp:iLabel>
                                </td>
                                <td>
                                    <select name="HeaderLine1">
                                        <option value=""></option>
                                    </select>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <div class="button">
                                        <ul>
                                            <li><a data-corner="8px" class="btncorner" href="#" onclick="AttachEstimateFiles();return false;">
                                                Attach</a></li>
                                        </ul>
                                    </div>
                                </td>
                                <td style="height: 25px">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <iframe name="fmAttachFile" frameborder="0" src="..\MnR\EstimateAttachFile.aspx"
        id="Iframe1" marginheight="0" marginwidth="0" width="0px" height="0px" scrolling="no"
        runat="server"></iframe>
    </form>
</body>
</html>
