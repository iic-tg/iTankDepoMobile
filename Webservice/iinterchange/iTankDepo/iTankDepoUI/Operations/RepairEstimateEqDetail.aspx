<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RepairEstimateEqDetail.aspx.vb"
    Inherits="Operations_RepairEstimateEqDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="height: 100%; overflow: auto">
    <div id="dicRepairEstimateEqDetail" style="margin: 1px; height: 50%;">
        <br />
        <table border="0" cellpadding="0" cellspacing="0" class="tblstd" style="margin: 0px auto;">
            <tr>
                <td class="pgHdr">
                    Type
                </td>
                <td>
                    <Inp:iTextBox ID="txtType" runat="server" CssClass="txtd" HelpText="" TextMode="SingleLine"
                        iCase="Numeric" ReadOnly="True" MaxLength="11">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            RegErrorMessage="" IsRequired="False" CustomValidation="False" RegexValidation="false"
                            RangeValidation="False" RegularExpression="" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td class="pgHdr">
                    Code
                </td>
                <td>
                    <Inp:iTextBox ID="txtCode" runat="server" CssClass="txtd" HelpText="" TextMode="SingleLine"
                        iCase="Numeric" ReadOnly="True" MaxLength="11">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            RegErrorMessage="" IsRequired="False" CustomValidation="False" RegexValidation="false"
                            RangeValidation="False" RegularExpression="" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td class="pgHdr">
                    <Inp:iLabel ID="lblprevCargo" runat="server">
                Previous Cargo
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtPreviousCargo" runat="server" CssClass="txtd" HelpText="" TextMode="SingleLine"
                        iCase="Numeric" ReadOnly="True" MaxLength="11">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            RegErrorMessage="" IsRequired="False" CustomValidation="False" RegexValidation="false"
                            RangeValidation="False" RegularExpression="" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td class="pgHdr">
                    Tare Weight (Kgs)
                </td>
                <td>
                    <Inp:iTextBox ID="txtTareWeight" runat="server" CssClass="ntxtd" TabIndex="5" HelpText="349,EQUIPMENT_INFORMATION_TR_WGHT_NC"
                        TextMode="SingleLine" iCase="Numeric" ReadOnly="true" ToolTip="Tare Weight (Kgs)"
                        OnClientTextChange="FormatThreeDecimal">
                        <Validator CustomValidateEmptyText="False" Operator="GreaterThan" Type="Double" Validate="false"
                            ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            RegErrorMessage="Invalid Tare Weight. Range must be from 0.001 to 9999999.999"
                            IsRequired="False" CustomValidation="False" RegexValidation="false" RangeValidation="False"
                            RegularExpression="^\d{1,7}(\.\d{1,3})?$" CompareValidation="false" ValueToCompare="0"
                            CmpErrorMessage="Tare Weight must be greater than 0" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td class="pgHdr">
                    Gross Weight (Kgs)
                </td>
                <td>
                    <Inp:iTextBox ID="txtGrossWeight" runat="server" CssClass="ntxtd" TabIndex="6" HelpText="350,EQUIPMENT_INFORMATION_GRSS_WGHT_NC"
                        TextMode="SingleLine" iCase="Numeric" ReadOnly="true" ToolTip="Gross Weight (Kgs)"
                        OnClientTextChange="FormatThreeDecimal">
                        <Validator CustomValidateEmptyText="False" Operator="GreaterThan" Type="Double" Validate="True"
                            ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            RegErrorMessage="Invalid Gross Weight. Range must be from 0.001 to 9999999.999"
                            IsRequired="False" CustomValidation="False" RegexValidation="false" RangeValidation="False"
                            RegularExpression="^\d{1,7}(\.\d{1,3})?$" CompareValidation="false" ValueToCompare="0"
                            CmpErrorMessage="Gross Weight must be greater than 0" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td class="pgHdr">
                 <Inp:iLabel ID="lblCapacity" runat="server" Text="Capacity (Litres)">
                
                    </Inp:iLabel>
                  
                </td>
                <td>
                    <Inp:iTextBox ID="txtCapacity" runat="server" CssClass="ntxtd" TabIndex="7" HelpText="351,EQUIPMENT_INFORMATION_CPCTY_NC"
                        TextMode="SingleLine" iCase="Numeric" ReadOnly="true" OnClientTextChange="FormatThreeDecimal">
                        <Validator CustomValidateEmptyText="False" Operator="GreaterThan" Type="Double" Validate="True"
                            ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" CustomValidation="False" RegexValidation="false" RangeValidation="False"
                            RegularExpression="^\d{1,7}(\.\d{1,3})?$" RegErrorMessage="Invalid Capacity. Range must be from 0.001 to 9999999.999"
                            CompareValidation="false" ValueToCompare="0" CmpErrorMessage="Capacity must be greater than 0" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td class="pgHdr">
                    Manuf. Date
                </td>
                <td>
                    <Inp:iTextBox ID="txtMfgDate" runat="server" CssClass="txtd" TabIndex="7" HelpText=""
                        TextMode="SingleLine" iCase="Upper" ReadOnly="true" OnClientTextChange="">
                        <Validator CustomValidateEmptyText="False" Operator="GreaterThan" Type="Date" Validate="True"
                            ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" CustomValidation="False" RegexValidation="false" RangeValidation="False"
                            RegularExpression="" RegErrorMessage="" CompareValidation="false" ValueToCompare="0"
                            CmpErrorMessage="" />
                    </Inp:iTextBox>
                </td>
            </tr>
            <tr>
                <td class="pgHdr">
                  <Inp:iLabel ID="lblLastSurveyor" runat="server">
                Last Surveyor
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtLastSurveyor" runat="server" CssClass="txtd" TabIndex="7" HelpText=""
                        TextMode="SingleLine" iCase="Upper" ReadOnly="true" OnClientTextChange="">
                        <Validator CustomValidateEmptyText="False" Operator="GreaterThan" Type="Date" Validate="True"
                            ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" CustomValidation="False" RegexValidation="false" RangeValidation="False"
                            RegularExpression="" RegErrorMessage="" CompareValidation="false" ValueToCompare="0"
                            CmpErrorMessage="" />
                    </Inp:iTextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
