<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MiscellaneousInvoice.aspx.vb" Inherits="Operations_MiscellaneousInvoice" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="overflow:auto">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Billing >> Miscellaneous Invoice Entry</span>
                </td>
                <td align="right">
                    <nv:navigation id="navMiscellaneousInvoice" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="tabMiscInvoice" class="tabdisplayGatePass" style="overflow-y: auto;overflow-x: auto; height: auto">
        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic; font-family: Arial; font-size: 8pt; display: none; width: 80%; height: 80%;" align="center">
            <div>
                No Records Found.</div>
        </div>
        <!-- UIG Fix -->
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width:100%">
            <tr style="width: 100%; height: 100%;">
                <td>
                    <div id="divMiscInvoice" style="margin: 1px; width: 100%; vertical-align: middle;">
                        <ifg:iflexgrid id="ifgMiscellaneousInvoiceDetail" runat="server" allowstaticheader="True" Width="100%" datakeynames="MSCLLNS_INVC_ID" 
                        captionalign="NotSet" gridlines="Both" headerrows="1" horizontalalign="Justify" pagesizerformat="" 
                        recordcountformat="Page {CPI} of {TPC}|{TRC} records available" scrollbars="None" showemptypager="True" staticheaderheight="300px" 
                        type="Normal" validationgroup="divMiscInvoice" usecacheddatasource="True" autogeneratecolumns="False" enableviewstate="False" onbeforeclientrowcreated="" 
                        onafterclientrowcreated="setDefaultValues" pagesize="25" addrowsoncurrentpage="False" allowpaging="True" onaftercallback="ifgMiscellaneousInvoiceDetailOnAfterCB"
                         allowdelete="true" allowrefresh="True" allowsearch="True" autosearch="True" useicons="true" searchbuttoniconclass="icon-search" 
                         searchbuttoncssclass="btn btn-small btn-info" addbuttoniconclass="icon-plus" addbuttoncssclass="btn btn-small btn-success" deletebuttoniconclass="icon-trash" 
                         deletebuttoncssclass="btn btn-small btn-danger" refreshbuttoniconclass="icon-refresh" refreshbuttoncssclass="btn btn-small btn-info" 
                         searchcancelbuttoniconclass="icon-remove" clearbuttoncssclass="icon-eraser" onbeforecallback="ifgMiscellaneousInvoiceDetailOnBeforeCB">
                            <PagerStyle CssClass="gpage" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:TextboxField DataField="NO_OF_EQPMNT_NO" HeaderText="Equipment No / No. of Equipment *"
                                    HeaderTitle="Equipment No / No. of Equipment" SortAscUrl="" SortDescUrl="" AllowSearch="true">
                                    <TextBox CssClass="txt" HelpText="339,MISCELLANEOUS_INVOICE_NO_OF_EQPMNT_NO" iCase="Upper"
                                        MaxLength="11" OnClientTextChange="" ValidationGroup="divMiscDetail">
                                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="false"
                                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No / No of Equipment Required."
                                            RegularExpression="^[a-zA-Z0-9]+$" RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                                    </TextBox>
                                    <HeaderStyle  CssClass="" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="250px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type *"
                                    HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""
                                    HeaderStyle-Width="50px" AllowSearch="true">
                                    <Lookup DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="340,EQUIPMENT_TYPE_EQPMNT_TYP_CD"
                                        iCase="Upper" OnClientTextChange="" ValidationGroup="divMiscDetail" MaxLength="10"
                                        TableName="3" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction="applyDepoFilter"
                                        AllowSecondaryColumnSearch="True" SecondaryColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                        AutoSearch="true">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Type" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                                Hidden="False" />
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="300px" />
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                            LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                                            ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divMiscDetail" />
                                    </Lookup>
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle  Width="200px" Wrap="True" />
                                </iFg:LookupField>
                                <iFg:LookupField DataField="INVCNG_TO_CD" ForeignDataField="INVCNG_TO_ID" HeaderText="Invoice To *"
                                    HeaderTitle="Invoice To *" PrimaryDataField="SRVC_PRTNR_ACTL_ID" SortAscUrl=""
                                    SortDescUrl="" SortExpression="">
                                    <Lookup DataKey="SRVC_PRTNR_CD" DependentChildControls="" HelpText="341,CUSTOMER_CSTMR_CD"
                                        iCase="Upper" TableName="59" ValidationGroup="divMiscDetail" ID="lkpInvoicingTo"
                                        CssClass="lkp" DoSearch="True" Width="150px" ClientFilterFunction="filterOnServicePartnerType"
                                        OnClientTextChange="" AllowSecondaryColumnSearch="true" SecondaryColumnName ="SRVC_PRTNR_NAM">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="SRVC_PRTNR_ACTL_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="SRVC_PRTNR_CD" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Description" ColumnName="SRVC_PRTNR_NAM" Hidden="False" />
                                            <Inp:LookupColumn ColumnCaption="Type" ColumnName="SRVC_PRTNR_TYP_CD" Hidden="False"
                                                ControlToBind="3" />                                         
                                            <Inp:LookupColumn ColumnCaption="DPT_ID" ColumnName="DPT_ID" Hidden="true" />
                                             <Inp:LookupColumn ColumnCaption="CRRNCY_ID" ColumnName="CRRNCY_ID" Hidden="true"   />
                                            <Inp:LookupColumn ColumnCaption="CRRNCY_CD" ColumnName="CRRNCY_CD" Hidden="true"  />
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="280px"
                                            HorizontalAlign="Center" />
                                        <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Invoice To. Click on the List for valid values"
                                            ReqErrorMessage="Invoice To Required" Validate="True" IsRequired="True" CustomValidation="false"
                                            CustomValidationFunction="" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="200px" />
                                </iFg:LookupField>
                                <iFg:TextboxField CharacterLimit="0" DataField="SRVC_PRTNR_TYP_CD" HeaderText="Customer/Invoicing Party"
                                    HeaderTitle="Customer/Invoicing Party" SortAscUrl="" SortDescUrl="" IsEditable="False"
                                    ReadOnly="True">
                                    <TextBox HelpText="" iCase="Upper" MaxLength="9" ValidationGroup="" OnClientTextChange=""
                                        ReadOnly="true" CssClass="txt" Width="80px">
                                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="Double" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="200px" Wrap="True" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:LookupField DataField="MIS_TYP" ForeignDataField="" HeaderText="Invoice Type *"
                                    HeaderTitle="Invoice Type *" PrimaryDataField="" SortAscUrl=""
                                    SortDescUrl="" SortExpression="">
                                    <Lookup DataKey="ENM_CD" DependentChildControls="" HelpText=""
                                        iCase="Upper" TableName="88" ValidationGroup="divMiscDetail" ID="Lookup1"
                                        CssClass="lkp" DoSearch="True" Width="150px" ClientFilterFunction=""
                                        OnClientTextChange="" AllowSecondaryColumnSearch="true" SecondaryColumnName ="">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="ENM_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="ENM_CD" Hidden="False" />                                            
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="280px"
                                            HorizontalAlign="Center" />
                                        <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Invoice Type Option. Click on the List for valid values"
                                            ReqErrorMessage="Invoice Type Required" Validate="True" IsRequired="true" CustomValidation="false" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="200px" />
                                </iFg:LookupField>
                                <iFg:LookupField DataField="MIS_CTGRY" ForeignDataField="" HeaderText="Category *"
                                    HeaderTitle="Category *" PrimaryDataField="" SortAscUrl=""
                                    SortDescUrl="" SortExpression="">
                                    <Lookup DataKey="ITEM_CD" DependentChildControls="" HelpText=""
                                        iCase="Upper" TableName="89" ValidationGroup="divMiscDetail" ID="Lookup2"
                                        CssClass="lkp" DoSearch="True" Width="150px" ClientFilterFunction="fnCategoryFilter"
                                        OnClientTextChange="" AllowSecondaryColumnSearch="true" SecondaryColumnName ="">
                                        <LookupColumns>
                                            <Inp:LookupColumn ColumnCaption="ID" ColumnName="FNC_INTGRTN_ID" Hidden="True" />
                                            <Inp:LookupColumn ColumnCaption="Type" ColumnName="INVC_TYP_CD" Hidden="false" /> 
                                            <Inp:LookupColumn ColumnCaption="Code" ColumnName="ITEM_CD" Hidden="false" />
                                            <Inp:LookupColumn ColumnCaption="DESCRIPTION" ColumnName="ITEM_DSCRPTN_VC" Hidden="False" />                                            
                                        </LookupColumns>
                                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <LookupGrid CurrentPageIndex="0" PageSize="10" VerticalAlign="NotSet" Width="380px"
                                            HorizontalAlign="Center" />
                                        <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" LkpErrorMessage="Invalid Catagory Option. Click on the List for valid values"
                                            ReqErrorMessage="Catagory Required" Validate="True" IsRequired="False" CustomValidation="True" CustomValidationFunction="ValidateCategory" />
                                    </Lookup>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="200px" />
                                </iFg:LookupField>
                                <iFg:DateField DataField="ACTVTY_DT" HeaderText="Activity Date *" HeaderTitle="Activity Date"
                                    SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}"
                                    HtmlEncode="false" AllowSearch="true">
                                    <iDate HelpText="343" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                        ValidationGroup="divMiscDetail" MaxLength="11" CssClass="txt" Width="120px">
                                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                        <Validator CustomValidateEmptyText="True" Operator="LessThanEqual" Type="Date" IsRequired="True"
                                            LkpErrorMessage="Invalid Activity Date. Click on the calendar icon for valid values"
                                            ReqErrorMessage="Activity Date Required" Validate="True" CompareValidation="True"
                                            CustomValidation="false" CmpErrorMessage="Activity Date cannot be greater than current Date."
                                            CustomValidationFunction="" />
                                    </iDate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="200px" Wrap="True" />
                                </iFg:DateField>                               
                                <iFg:TextboxField DataField="CHRG_DSCRPTN" HeaderText="Charge Description" HeaderTitle="Charge Description"
                                    SortAscUrl="" SortDescUrl="" HtmlEncode="False" AllowSearch="true">
                                    <TextBox HelpText="344,MISCELLANEOUS_INVOICE_CHRG_DSCRPTN" OnClientTextChange=""
                                        ValidationGroup="divMiscDetail" CssClass="txt">
                                        <Validator CustomValidateEmptyText="false" IsRequired="False" Operator="Equal" RegErrorMessage=""
                                            RegexValidation="False" RegularExpression="" ReqErrorMessage="" CustomValidationFunction=""
                                            CustomValidation="false" Type="String" Validate="true" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="400px" HorizontalAlign="Left" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="AMNT_NC" CharacterLimit="0" HeaderText="Amount *" HeaderTitle="Amount"
                                    SortAscUrl="" SortDescUrl="">
                                    <TextBox CssClass="ntxto" HelpText="345,MISCELLANEOUS_INVOICE_AMNT_NC" iCase="None" OnClientTextChange="formatAmount"
                                        ValidationGroup="divMiscDetail" MaxLength="11">
                                        <Validator CustomValidateEmptyText="False" IsRequired="True" Operator="GreaterThan"
                                            ReqErrorMessage="Amount Required" Type="Double" CustomValidationFunction="" CustomValidation="false"
                                            CsvErrorMessage="Exist" Validate="True" RegErrorMessage="Invalid Amount. Range must be from 1 to 9999999.99"
                                            RegexValidation="false" RegularExpression="^(\+|-)?\d{1,7}(\.\d{1,2})?$" CmpErrorMessage="Amount must be greater than 0"
                                            CompareValidation="true" ValueToCompare="0.00" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="200px" Wrap="true" HorizontalAlign="Right" />
                                </iFg:TextboxField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                        </ifg:iflexgrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <sp:submitpane id="PageSubmitPane" runat="server" onclientsubmit="submitPage()" align="center" onclientprint="null" />
    <asp:HiddenField ID="hdnEquipmentNo" runat="server" />
    </form>
</body>
</html>
