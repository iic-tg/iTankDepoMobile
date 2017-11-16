<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SupplierEquipmentDetail.aspx.vb"
    Inherits="Masters_SupplierEquipmentDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server" style="overflow:auto">
    <div>
        <div id="divRentalOtherCharge" class="tabOtherCharge">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="center" >
          <tr>
                <td colspan="3">
        <div id="divEquipmentDetail" class="tabEquipmentDetails">
            <iFg:iFlexGrid ID="ifgEquipmentDetails" runat="server" AllowStaticHeader="True" DataKeyNames="SPPLR_EQPMNT_DTL_ID"
                Width="400px" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                Scrollbars="None" ShowEmptyPager="False" StaticHeaderHeight="200px" Type="Normal"
                ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                EnableViewState="False" OnBeforeClientRowCreated="ValidateEndDate" OnAfterClientRowCreated="setDefaultValues"
                AddRowsonCurrentPage="False" ShowPageSizer="False" OnAfterCallBack="onAfterCB"
                OnBeforeCallBack="onBeforeCallback" AllowSearch="False" AllowPaging="False"
                UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                SearchCancelButtonIconClass="icon-remove" SearchCancelButtonCssClass="btn btn-small btn-danger"
                ClearButtonIconClass="icon-eraser" ClearButtonCssClass="btn btn-small btn-success" AllowAdd="True" AllowSorting="False" AutoSearch="False" ShowRecordCount="False" PageSize="1000">
                <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center"/>
                <RowStyle CssClass="gitem" />
                <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                <Columns>
                   
                    <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No *" HeaderTitle="Equipment No"
                        SortAscUrl="" SortDescUrl="" AllowSearch="true" SortExpression ="">
                        <TextBox CausesValidation="True" CssClass="txt" HelpText="453" iCase="Upper"
                            MaxLength="11" ValidationGroup="divEquipmentDetail" TabIndex ="1" OnClientTextChange ="">
                            <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="True" CustomValidateEmptyText="true" 
                                CustomValidationFunction="validateEquipmentno" IsRequired="True" ReqErrorMessage="Equipment No Required" />
                        </TextBox>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle Width="100px" Wrap="True" />
                    </iFg:TextboxField>
                    <iFg:LookupField DataField="EQPMNT_TYP_CD" ForeignDataField="EQPMNT_TYP_ID" HeaderText="Type *"
                        HeaderTitle="Type" PrimaryDataField="EQPMNT_TYP_ID" SortAscUrl="" SortDescUrl=""  
                        HeaderStyle-Width="50px" AllowSearch="true" SortExpression="">
                        <Lookup ID="lkpType" DataKey="EQPMNT_TYP_CD" DependentChildControls="" HelpText="454"
                            iCase="Upper" OnClientTextChange="" ValidationGroup="divEquipmentDetail" MaxLength="5"
                            TableName="3" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction=""
                            AllowSecondaryColumnSearch="true"  SecondaryColumnName="EQPMNT_CD_CD"
                            AutoSearch="true" TabIndex ="2">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="EQPMNT_TYP_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnCaption="Type" ColumnName="EQPMNT_TYP_CD" Hidden="False" />
                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False"  ControlToBind="2" />
                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_TYP_DSCRPTN_VC"
                                    Hidden="False" />
                            </LookupColumns>
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="NotSet" Width="250px" />
                            <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" IsRequired="True"
                                LkpErrorMessage="Invalid Equipment Type. Click on the list for valid values"
                                ReqErrorMessage="Equipment Type Required" Validate="True" ValidationGroup="divEquipmentDetail" />
                        </Lookup>
                        <HeaderStyle></HeaderStyle>
                        <ItemStyle Width="100px" Wrap="True" />
                    </iFg:LookupField>

                    <iFg:TextboxField DataField="EQPMNT_CD_CD" HeaderText="Code" HeaderTitle="Code" SortAscUrl=""
                        SortDescUrl="">
                        <TextBox CssClass="txt" HelpText="" OnClientTextChange="" ValidationGroup=""
                            ReadOnly="true">
                            <Validator CustomValidateEmptyText="False" IsRequired="False" Operator="Equal" ReqErrorMessage=" "
                                Type="Double" />
                        </TextBox>
                        <ItemStyle Width="80px" Wrap="true" />
                    </iFg:TextboxField>


                  
               <%--     <iFg:LookupField DataField="EQPMNT_CD_CD" ForeignDataField="EQPMNT_CD_ID" HeaderText="Code *"
                        HeaderTitle="Code" PrimaryDataField="EQPMNT_CD_ID" SortAscUrl="" SortDescUrl="" ReadOnly="true"
                        AllowSearch="true" SortExpression ="">
                        <Lookup DataKey="EQPMNT_CD_CD" DependentChildControls="" HelpText="455"
                            iCase="Upper" OnClientTextChange="" ValidationGroup="divEquipmentDetail" MaxLength="4"
                            TableName="7" CssClass="lkp" DoSearch="True" Width="80px" ClientFilterFunction=""
                            AllowSecondaryColumnSearch="true"  SecondaryColumnName="EQPMNT_CD_DSCRPTN_VC" ReadOnly="true"
                            AutoSearch="true" TabIndex ="3">
                            <LookupColumns>
                                <Inp:LookupColumn ColumnName="EQPMNT_CD_ID" Hidden="True" />
                                <Inp:LookupColumn ColumnCaption="Code" ColumnName="EQPMNT_CD_CD" Hidden="False" />
                                <Inp:LookupColumn ColumnCaption="Description" ColumnName="EQPMNT_CD_DSCRPTN_VC" Hidden="False" />
                            </LookupColumns>
                            <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                                IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                            <LookupGrid CurrentPageIndex="0" PageSize="5" VerticalAlign="NotSet" HorizontalAlign ="Right"  Width="250px" />
                            <Validator CustomValidateEmptyText="True" Operator="Equal" Type="String" IsRequired="true" 
                                LkpErrorMessage="Invalid Equipment Code. Click on the list for valid values"
                                ReqErrorMessage="Equipment Code Required" Validate="True" CustomValidation="False"
                                CustomValidationFunction="" CsvErrorMessage=""  ValidationGroup="divEquipmentDetail"/>
                        </Lookup>
                        <HeaderStyle></HeaderStyle>
                        <ItemStyle Width="100px" Wrap="True" HorizontalAlign ="Left" />
                    </iFg:LookupField>--%>
                </Columns>
                <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                <SelectedRowStyle CssClass="gsitem" />
                <AlternatingRowStyle CssClass="gaitem" />
                <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                    IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
            </iFg:iFlexGrid>
         
        </div>
        <table >
        <tr>
        <td>
      <Inp:iLabel ID="lblMessage" runat="server" CssClass="blbl">
                    </Inp:iLabel>
        </td>
        </tr></table>
           <div id="HypButton">
<%--                   <table class="button" align="center" style="width: 220px">
            <tr>
                <td>
                    <ul>
                        <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnSubmit"
                            style="text-decoration: none; color: White; font-weight: bold"
                            runat="server" onclick="addEquipmentDetails();return false;">&nbsp;Submit</a></li>
                    </ul>
                  <ul>
                        <li class="btn btn-small btn-info"><a href="#" data-corner="8px" id="btnClose"
                            style="text-decoration: none; color: White; font-weight: bold"
                            runat="server" onclick="Close();return false;">&nbsp;Close</a></li>
                    </ul>
                </td>
            </tr>
        </table>--%>
         <table align="center" >
            <tr>
                 <td align="right">
                    <a href="#" id="btnSubmit" onclick="addEquipmentDetails()" class="btn btn-small btn-success"
                        runat="server" style="font-weight: bold; vertical-align: middle"><i class="icon-save">
                        </i>&nbsp;Save</a>
                </td>
            <%--    <td align="left">
                    <a href="#" id="btnClose" onclick="Close(); return false;"
                        class="btn btn-small btn-danger" runat="server" style="font-weight: bold; vertical-align: middle">
                        <i class="icon-remove"></i>&nbsp;Cancel</a>
                </td>--%>
            </tr>
        </table>
           </div>

        <asp:HiddenField ID="hdnSupplierId" runat="server" />
        <asp:HiddenField ID="hdnSupplierContractId" runat="server" />
        <asp:HiddenField ID="hdnSupplierEquipmentDetailId" runat="server" />
        <asp:HiddenField ID="hdnContractRefNo" runat="server" />
        <asp:HiddenField ID="hdnEndDate" runat="server" />
        </td>
        </tr>
        </table> 
    </div>

    </form>
</body>
</html>
