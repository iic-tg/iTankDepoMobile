<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GateOutApproval.aspx.vb" Inherits="Operations_GateOutApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <div>
        <div style="width: 100%">
            <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr class="ctab" style="width: 100%; height: 30px;">
                    <td align="left">
                        <span id="spnHeader" class="ctabh">Operations >> Gate Out Approval</span>
                    </td>
                    <td align="right">
                    <nv:Navigation ID="navCodeMaster" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="tabGateOut" class="tabdisplayGatePass" style="overflow:hidden; height: auto">
        <div>
            <Nav:iTab ID="ITab1" runat="server" ControlHeight="" ControlWidth="" EnableClose="False"
                SelectedTab="" TabHeight="" TabWidth="" TabsPerRow="15">
                <TabPages>
                    <Nav:TabPage ID="Pending" runat="server" Caption="Pending" doValidate="True" OnAfterSelect="pendingTab();"
                        OnBeforeSelect="" TabPageClientId="divPending">
                    </Nav:TabPage>
                    <Nav:TabPage ID="MySubmits" runat="server" Caption="My Submits" doValidate="True"
                        OnAfterSelect="mySubmitTab();" OnBeforeSelect=""
                        TabPageClientId="divPending">
                    </Nav:TabPage>
                </TabPages>
            </Nav:iTab>
        </div>

       <%-- Pending--%>

       <div id="divPending" style="width: 100%;">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width: 100%;">
                <tr style="width: 100%; height: 100%;">
                    <td>
                        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                            font-family: Arial; font-size: 8pt; display: none; width: 100%;" align="center">
                            <div>
                                No Records Found.</div>
                        </div>
                         <%-- Pending Grid--%>
                        <iFg:iFlexGrid ID="ifgGateOutApprovalPending" runat="server" AllowStaticHeader="True" DataKeyNames="ACTVTY_STTS_ID"
            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
            Scrollbars="Auto" ShowEmptyPager="True" StaticHeaderHeight="300px" Type="Normal"
            ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated="setDefaultValues"
            PageSize="5" AddRowsonCurrentPage="False" AllowPaging="True" AllowAdd="false"
            AllowDelete="false" OnAfterCallBack="" AllowRefresh="false" AllowEdit="true"
            AllowExport="false" AllowSearch="True" AutoSearch="True" UseIcons="True" SearchButtonCssClass="btn btn-small btn-info"
            DeleteButtonCssClass="btn btn-small btn-danger" RefreshButtonCssClass="btn btn-small btn-info"
            SearchCancelButtonCssClass="btn btn-small btn-danger" ClearButtonCssClass="btn btn-small btn-success"
            SearchButtonIconClass="icon-search" AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
            DeleteButtonIconClass="icon-trash" RefreshButtonIconClass="icon-refresh" SearchCancelButtonIconClass="icon-remove">
            <Columns>
                <iFg:TextboxField CharacterLimit="0" DataField="CSTMR_CD" HeaderText="Customer" HeaderTitle="Customer"
                    SortAscUrl="" SortDescUrl="" ReadOnly="true" >
                    <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup="" ReadOnly="true"
                        MaxLength="3">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                            IsRequired="true" ReqErrorMessage="Equipment Type Required." RegularExpression="^[a-zA-Z0-9]+$"
                            RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="" iCase="Upper" MaxLength="11"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="EQPMNT_TYP_CD" HeaderText="Type" HeaderTitle="Type"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="" iCase="Upper" MaxLength="11"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="EQPMNT_CD_CD" HeaderText="Code" HeaderTitle="Code" SortAscUrl=""
                    SortDescUrl="" AllowSearch="true" ReadOnly="true">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="" iCase="Upper" MaxLength="11"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="STTS_CD" HeaderText="Equipment Status" HeaderTitle="Equipment Status"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="" iCase="Upper" MaxLength="11"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="EQPMNT_STTS_CD" HeaderText="Activity Status" HeaderTitle="Activity Status"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="" iCase="Upper" MaxLength="11"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="True" />
                </iFg:TextboxField>

                 <iFg:TextboxField DataField="BKNG_AUTH_NO" HeaderText="Booking Authorization No" HeaderTitle="Booking Authorization No"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="false">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="608" iCase="Upper" MaxLength="14"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"  RegularExpression="^[a-zA-Z0-9]+$" 
                                   RegErrorMessage="Only alphabets and numbers allowed." RegexValidation="true"
                            CustomValidationFunction="" IsRequired="false" ReqErrorMessage="Booking Authorization No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" />
                </iFg:TextboxField>

                <iFg:TextboxField DataField="RQST_APPRVL_CNGNE" HeaderText="Consignee" HeaderTitle="Consignee"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="false">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="596" iCase="None" MaxLength="11"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="false" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="False" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="RQST_APPRVL_RMKRS" HeaderText="Remarks" HeaderTitle="Remarks"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="false">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="232" MaxLength="255" iCase="Upper"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="false" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="False" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="GTAPPRVL_BY" HeaderText="Approval By" HeaderTitle="Approval By"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="" MaxLength="255" iCase="Upper"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="false" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="False" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:DateField DataField="GTAPPRVL_DT" HeaderText="Approval Date" HeaderTitle="Approval Date"
                    ReadOnly="true" SortAscUrl="" HeaderStyle-HorizontalAlign="Left" SortDescUrl=""
                    DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" AllowSearch="true">
                    <iDate HelpText="374" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                        ReadOnly="true" ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                        <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                            ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <Validator CustomValidateEmptyText="True" Operator="LessThanEqual" Type="Date" IsRequired="false"
                            LkpErrorMessage="Invalid Out Date. Click on the calendar icon for valid values"
                            ReqErrorMessage="Out Date Required" Validate="false" CompareValidation="True"
                            CustomValidation="true" CmpErrorMessage="Out Date cannot be greater than current Date."
                            CustomValidationFunction="ValidateGateOutDate" />
                    </iDate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="80px" Wrap="True" />
                </iFg:DateField>


                <iFg:CheckBoxField DataField="SLCT" HeaderText="Approve" HeaderTitle="Approve" HelpText=""
                    SortAscUrl="" SortDescUrl="" HeaderImageUrl="" ReadOnly="false">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="20px" Wrap="True" HorizontalAlign="Center" />
                </iFg:CheckBoxField>

                 <iFg:CheckBoxField DataField="CNCL" HeaderText="Cancel" HeaderTitle="Cancel" HelpText=""
                    SortAscUrl="" SortDescUrl="" HeaderImageUrl="" ReadOnly="false">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="20px" Wrap="True" HorizontalAlign="Center" />
                </iFg:CheckBoxField>

            </Columns>
            <RowStyle CssClass="gitem" />
            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                OffsetX="" OffsetY="" OnImgClick="" Width="" />
            <PagerStyle CssClass="gpage" Height="18px" HorizontalAlign="Center" />
            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
            <SelectedRowStyle CssClass="gsitem" />
            <AlternatingRowStyle CssClass="gaitem" />
            <ActionButtons>
                <iFg:ActionButton ID="acnRefresh" Text="Refresh" ValidateRowSelection="False" OnClientClick="RefreshGrid();"
                    IconClass="icon-refresh" CSSClass="btn btn-small btn-info" />
            </ActionButtons>
        </iFg:iFlexGrid>
                          </td>
                </tr>
            </table>

            <br /><br />
            
         <sp:SubmitPane ID="PageSubmitPane"  runat="server" onclientsubmit="submitPage();"
        onclientprint="null" />
       </div>

     
        </div>
    </form>
</body>
</html>
