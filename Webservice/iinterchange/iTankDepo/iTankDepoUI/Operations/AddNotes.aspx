<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddNotes.aspx.vb" Inherits="Operations_AddNotes" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="overflow:auto">
    <div align="center">
        <div id="divNotes" runat="server">
            <table class="tblstd" align="center">
                <tr>
                    <td>
                        <label id="lblNotes1" runat="server" class="lbl">
                            Notes 1</label>
                    </td>
                    <td colspan="3">
                        <Inp:iTextBox ID="txtNotes1" runat="server" CssClass="txt" TabIndex="15" HelpText="134,GATEIN_NT_1_VC"
                            TextMode="SingleLine" iCase="Upper" Width="350px">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage=""
                                CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False"
                                RangeValidation="False" />
                        </Inp:iTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label id="lblNotes2" runat="server" class="lbl">
                            Notes 2</label>
                    </td>
                    <td colspan="3">
                        <Inp:iTextBox ID="txtNotes2" runat="server" CssClass="txt" TabIndex="16" HelpText="135,GATEIN_NT_2_VC"
                            TextMode="SingleLine" iCase="Upper" Width="350px">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage=""
                                CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False"
                                RangeValidation="False" />
                        </Inp:iTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label id="lblNotes3" runat="server" class="lbl">
                            Notes 3</label>
                    </td>
                    <td colspan="3">
                        <Inp:iTextBox ID="txtNotes3" runat="server" CssClass="txt" TabIndex="17" HelpText="136,GATEIN_NT_3_VC"
                            TextMode="SingleLine" iCase="Upper" Width="350px">
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                ValidationGroup="gateInmoreInfo" LookupValidation="False" CsvErrorMessage=""
                                CustomValidationFunction="" IsRequired="False" CustomValidation="False" RegexValidation="False"
                                RangeValidation="False" />
                        </Inp:iTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table class="button" align="center" style="width: 80px">
                            <tr>
                                <td align="center">
                                    <ul>
                                        <li><a href="#" data-corner="8px" id="btnSubmit" class="btncorner" runat="server"
                                            onclick="submitPage(); return false;">Submit</a></li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divDescription" runat="server" style="height: 100px; width: 300px">
            <table class="tblstd" align="center">
                <tr>
                    <td>
                        <label id="lblDescription" runat="server" class="nlbl" style="font-size: small; font-weight: bolder;">
                        </label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hdnID" runat="server" />   
    <asp:HiddenField ID="HiddenField1" runat="server" />
    </form>
</body>
<script type="text/javascript">
    function submitPage() {
        var oCallback = new Callback();
        oCallback.add("Notes1", el("txtNotes1").value);
        oCallback.add("Notes2", el("txtNotes2").value);
        oCallback.add("Notes3", el("txtNotes3").value);
        oCallback.add("GateInId", el("hdnID").value);
        oCallback.invoke("Addnotes.aspx", "submit");
        pdfs('wfFrame').HasNoteChanges = true;
        ppsc().closeModalWindow();
        oCallback = null;
        return true;
    }
</script>
</html>
