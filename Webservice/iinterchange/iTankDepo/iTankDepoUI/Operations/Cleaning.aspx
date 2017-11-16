<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Cleaning.aspx.vb" Inherits="Operations_Cleaning" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>   

    <style type="text/css">
    
    #tblLayout {
    border-collapse: collapse;
    }

    #tblLayout > tr > td {
    padding-top: .5em;
    padding-bottom: .5em;
   }
    
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr class="ctab" style="width: 100%; height: 8px;">
            <td align="left" valign="middle">
                <span id="spnEstHeader" class="ctabh"><span id="spnEstNo">Operations >> Cleaning</span><span
                    class='olbl' style="vertical-align: middle"></span></span>
            </td>
            <td align="right">
                <nv:Navigation ID="navEstimate" runat="server" />
            </td>
        </tr>
    </table>
    <!-- UIG Fix -->
    <div class="tabdisplayGatePass" id="divCleaning" style="overflow-y: auto; overflow-x: auto;
        height: auto"><br />
        <table id="tblLayout" border="0" cellpadding="5" cellspacing="1" class="tblstd" style="width: 100%;">

          

            <tr>
                <td>
                    <Inp:iLabel ID="lblEquipmentNo" runat="server" CssClass="lbl">
                   Equipment No 
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtEquipmentNo" runat="server" CssClass="txtd" TabIndex="1" HelpText=""
                        TextMode="SingleLine" iCase="Upper" ReadOnly="True">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="True" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblEquipmentType" runat="server" CssClass="lbl">
               Type
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpEqpType" runat="server" CssClass="lkpd" DataKey="EQPMNT_TYP_CD"
                        DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="2" TableName="3" HelpText=""
                        ClientFilterFunction="" ReadOnly="true" AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="Right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="false"
                            LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Equipment Type Required" Type="String" Validate="True"
                            ValidationGroup="divHeader" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                      <Inp:iLabel ID="lblCleaningReference" runat="server" CssClass="lbl">
                   Cleaning Ref No
                    </Inp:iLabel>
                   <%-- <Inp:iLabel ID="ILabel10" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel></td>--%>
                <td>
                   <Inp:iTextBox ID="txtCleaningReferenceNo" runat="server" CssClass="txt" TabIndex="3"
                        HelpText="337,CLEANING_CLNNG_RFRNC_NO" TextMode="SingleLine" iCase="Numeric"
                        ReadOnly="False">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="true" 
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="false" CustomValidation="False" RegexValidation="False" RangeValidation="False"
                            ReqErrorMessage="Cleaning Reference Number Required" />
                    </Inp:iTextBox></td>
            </tr>       

            <tr>
                <td>
                    <Inp:iLabel ID="lblCustomer" runat="server" CssClass="lbl">
                   Customer 
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtCustomer" runat="server" CssClass="txtd" TabIndex="4" HelpText=""
                        TextMode="SingleLine" iCase="Upper" ReadOnly="True">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divHeader" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="True" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblPreviousCargo" runat="server" CssClass="lbl">
                   Previous Cargo
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iLookup ID="lkpPreviousCargo" runat="server" CssClass="lkpd" DataKey="PRDCT_CD"
                        DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="5" TableName="46" HelpText="321,V_CLEANING_PRDCT_CD"
                        ClientFilterFunction="" AllowSecondaryColumnSearch="true" SecondaryColumnName="PRDCT_DSCRPTN_VC"
                        ReadOnly="true">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="PRDCT_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="PRDCT_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="PRDCT_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top"
                            HorizontalAlign="Right" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Previous Cargo. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="PreviousCargo Required" Type="String" Validate="True"
                            ValidationGroup="divCleaning" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>                   
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblChemicalName" runat="server" CssClass="lbl">
                 Chemical Name
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtChemicalName" runat="server" CssClass="txtd" TabIndex="6" HelpText=""
                        TextMode="SingleLine" iCase="Upper" ReadOnly="True">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                            ValidationGroup="divCleaning" LookupValidation="False" CsvErrorMessage="" CustomValidationFunction=""
                            IsRequired="False" CustomValidation="False" RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
            </tr>           

            <tr>
                <td>
                    <Inp:iLabel ID="lblGateInDate" runat="server" CssClass="lbl">
                   In Date
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iDate ID="datGateInDate" TabIndex="7" runat="server" HelpText="" CssClass="datd"
                        MaxLength="11" iCase="Upper" ReadOnly="true">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup=""
                            Operator="LessThanEqual" LkpErrorMessage="Invalid In Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CustomValidationFunction="" CompareValidation="True"
                            ReqErrorMessage="In Date Required" CmpErrorMessage="In date Should not be greater than Current Date."
                            RangeValidation="False" CustomValidation="false" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <Inp:iLabel ID="lblCleaningDate" runat="server" CssClass="lbl">
                  Original Cleaning Date
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel2" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iDate ID="datOriginalCleaningDate" TabIndex="8" runat="server" HelpText="322"
                        CssClass="dat" MaxLength="11" iCase="Upper" DataFormatString="{0:dd-MMM-yyyy}"
                        OnClientTextChange="fnSetLastCleaningDate">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup="divCleaning"
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Original Cleaning Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidateCleaningDate"
                            CompareValidation="True" ReqErrorMessage="Original Cleaning Date Required" CmpErrorMessage="Original Cleaning date cannot be greater than Current Date."
                            RangeValidation="False" CustomValidation="True" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>     <Inp:iLabel ID="ILabel3" runat="server" CssClass="lbl">
                 Latest Cleaning Date
                    </Inp:iLabel>
                    <Inp:iLabel ID="ILabel5" runat="server" CssClass="lblReq">
                   *
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iDate ID="datLastCleaningDate" TabIndex="9" runat="server" HelpText="421" CssClass="dat"
                        MaxLength="11" iCase="Upper" DataFormatString="{0:dd-MMM-yyyy}">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Type="Date" ValidationGroup="divCleaning"
                            Operator="LessThanEqual" LkpErrorMessage="Invalid Latest Cleaning Date. Click on the calendar icon for valid values"
                            Validate="True" CsvErrorMessage="" CustomValidationFunction="ValidateLatestCleaningDate"
                            CompareValidation="true" ReqErrorMessage="Latest Cleaning Date Required" CmpErrorMessage="Latest Cleaning date  cannot be greater than Current Date."
                            RangeValidation="False" CustomValidation="true" />
                        <DateIcon CssClass="dimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iDate></td>
            </tr>                   
          
            <tr>
                <td>
                    <Inp:iLabel ID="lblCertofCleanliness" runat="server" CssClass="lbl">
                                   Additional Cleaning
                    </Inp:iLabel>
                </td>
                <td>
                    <asp:CheckBox ID="chkAdditionalCleaningBit" runat="server" CssClass="chk" TabIndex="10" Enabled="false" />
                </td>
                <td>
                </td>
                <td>
                    <div id="divCleaningRatelbl">
                        <Inp:iLabel ID="lblCleaningRate" runat="server" CssClass="lbl">
                  Cleaning Rate
                        </Inp:iLabel>
                        <Inp:iLabel ID="ILabel7" runat="server" CssClass="lblReq">
                   *
                        </Inp:iLabel></div>
                </td>
                <td>
                    <div id="divCleaningRatelkp">
                        <Inp:iTextBox ID="txtCleaningRate" runat="server" CssClass="ntxto" TabIndex="11"
                            HelpText="458,CLEANING_CLNNG_RT" TextMode="SingleLine" iCase="Numeric" 
                            OnClientTextChange="FormatTwoDecimal">
                            <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="GreaterThan"
                                ReqErrorMessage="Cleaning Rate Required" Type="Double" CustomValidationFunction=""
                                CustomValidation="false" CsvErrorMessage="Exist" Validate="True" RegErrorMessage="Invalid Cleaning Rate. Range must be from 0 to 999999999.99"
                                RegexValidation="true" RegularExpression="^\d{1,9}(\.\d{1,2})?$" CmpErrorMessage="Cleaning Rate must be greater than 0"
                                CompareValidation="false" ValueToCompare="0.00" />
                        </Inp:iTextBox>
                        <Inp:iLabel ID="ILabel8" runat="server" CssClass="lbl">
                (
                        </Inp:iLabel>
                        <Inp:iLabel ID="lblCurrency" runat="server" CssClass="blbl">
                        </Inp:iLabel>
                        <Inp:iLabel ID="ILabel9" runat="server" CssClass="lbl">
                   )
                        </Inp:iLabel>
                    </div>
                </td>
                <td>
                </td>
                <td><Inp:iLabel ID="lblStatus" runat="server" CssClass="lbl">
                   Status
                    </Inp:iLabel>
                </td>
                <td>
                <Inp:iLookup ID="lkpStatus" runat="server" CssClass="lkpd" DataKey="EQPMNT_STTS_CD" TabIndex="12"
                        DoSearch="True" iCase="None" MaxLength="10" TableName="12" HelpText="325,V_CLEANING_EQPMNT_STTS_CD"
                        ClientFilterFunction="" AllowSecondaryColumnSearch="true" SecondaryColumnName="EQPMNT_STTS_DSCRPTN_VC"
                        ReadOnly="true">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="EQPMNT_STTS_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="EQPMNT_STTS_CD" ControlToBind="" Hidden="False" ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_STTS_DSCRPTN_VC"
                                Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="250px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Equipment Status. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Equipment Status  Required" Type="String" Validate="True"
                            ValidationGroup="divHeader" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                </td>
                <td colspan="2">
                </td>
            </tr>         

            <tr>
                <td>
                    <Inp:iLabel ID="lblRemarks" runat="server" CssClass="lbl">
                  Remarks
                    </Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtRemarks" runat="server" CssClass="txt" TabIndex="13" HelpText="338"
                        TextMode="MultiLine" iCase="None" ReadOnly="False" Width="127px" MaxLength="500">
                        <Validator CustomValidateEmptyText="true" Operator="Equal" Type="String" Validate="true"
                            ReqErrorMessage="Remarks Required" ValidationGroup="divCleaning" LookupValidation="False"
                            CsvErrorMessage="" CustomValidationFunction="" IsRequired="False" CustomValidation="false"
                            RegexValidation="False" RangeValidation="False" />
                    </Inp:iTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td>
                  
                </td>
                <td>
                    &nbsp;
                </td>
                <td>

                </td>
                <td>
                    
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
            <td colspan="11" style="text-align:center;">
            <div id="divGenerateCleaningCertificate">
                <a href="#" id="A1" class="btn btn-small btn-success"
                        style="font-weight: bold;" onclick="PrintCleaningInstructionReport();return false;" runat="server"><i
                            class="icon-print"></i>&nbsp;Print Cleaning Instruction</a>
             </div>
            </td>
            </tr>
        
        </table>
        <table align="center">
            <tr>
                <td>
                    <a href="#" id="hypGenerateCleaningCertificate" class="btn btn-small btn-success"
                        style="font-weight: bold; display:none;" onclick="PrintCleaningCertificate();" runat="server"><i
                            class="icon-print"></i>&nbsp;Print Cleaning Instruction</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="divSubmit">
        <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage();"
            onClientPrint="null" />
    </div>
    <div id="divLockMessage" style="width: 100%; margin: 0px auto; text-align: center;
        font-weight: normal; font-weight: bold; height: 20px; background-color: #CDFFCC;
        border: 1px solid #07760D;">
    </div>
    <asp:HiddenField ID="hdnDepotID" runat="server" />
    <asp:HiddenField ID="hdnCustomerId" runat="server" />
    <asp:HiddenField ID="hdnGI_TRNSCTN_NO" runat="server" />
    <asp:HiddenField ID="hdnGateInDate" runat="server" />
    <asp:HiddenField ID="hdnCleaningID" runat="server" />
    <asp:HiddenField ID="hdnBillingFlag" runat="server" />
    <asp:HiddenField ID="hdnStr20EirNo" runat="server" />
    <asp:HiddenField ID="hdnCurrencyCode" runat="server" />
    <asp:HiddenField ID="hdnEditDate" runat="server" />
    <asp:HiddenField ID="hdnStatusId" runat="server" />
    <asp:HiddenField ID="hdnLockUserName" runat="server" />
    <asp:HiddenField ID="hdnIpError" runat="server" />
    <asp:HiddenField ID="hdnLockActivityName" runat="server" />
     <asp:HiddenField ID="hdnAddtnlClnngFlg" runat="server" />
     <asp:HiddenField ID="hdnSlabRateFlg" runat="server" />
    </form>
</body>
</html>
