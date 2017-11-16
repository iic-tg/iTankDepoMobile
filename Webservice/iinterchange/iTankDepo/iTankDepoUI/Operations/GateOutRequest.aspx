<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GateOutRequest.aspx.vb" Inherits="Operations_GateOutRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
       <form id="form1" runat="server">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Operations >> Gate Out Request</span>
                </td>
                <td align="right">
             <nv:Navigation ID="navCodeMaster" runat="server" />
                </td>
            </tr>
        </table>
    </div>
      <!-- UIG Fix -->
    <div class="" id="divdisplayGatePass" style="height:auto">
        <div class="topspace">
        </div>
        <iFg:iFlexGrid ID="ifgGateOutRequest" runat="server" AllowStaticHeader="True" DataKeyNames="ACTVTY_STTS_ID"
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
                <iFg:TextboxField CharacterLimit="0" DataField="CSTMR_CD" HeaderText="Customer" HeaderTitle="Customer" ReadOnly="true"
                    SortAscUrl="" SortDescUrl="">
                    <TextBox CssClass="txt" HelpText="" iCase="Upper" OnClientTextChange="" ValidationGroup="" ReadOnly="true"
                        MaxLength="3">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="True"
                            IsRequired="true" ReqErrorMessage="Equipment Type Required." RegularExpression="^[a-zA-Z0-9]+$"
                            RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are allowed" />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="60px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="EQPMNT_NO" HeaderText="Equipment No" HeaderTitle="Equipment No"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="" iCase="Upper" MaxLength="11"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="60px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="EQPMNT_TYP_CD" HeaderText="Type" HeaderTitle="Type"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="" iCase="Upper" MaxLength="11"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="60px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="EQPMNT_CD_CD" HeaderText="Code" HeaderTitle="Code" SortAscUrl=""
                    SortDescUrl="" AllowSearch="true" ReadOnly="true">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="" iCase="Upper" MaxLength="11"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="60px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="STTS_CD" HeaderText="Equipment Status" HeaderTitle="Equipment Status"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="" iCase="Upper" MaxLength="11"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="60px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="EQPMNT_STTS_CD" HeaderText="Activity Status" HeaderTitle="Activity Status"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="" iCase="Upper" MaxLength="11"
                        OnClientTextChange="" ValidationGroup="divEquipmentDetail">
                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="Equipment No Required." />
                    </TextBox>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="60px" Wrap="True" />
                </iFg:TextboxField>
                <iFg:TextboxField DataField="RQST_APPRVL_CNGNE" HeaderText="Consignee" HeaderTitle="Consignee"
                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="false">
                    <TextBox CausesValidation="True" CssClass="txt" HelpText="596" ToolTip="" iCase="None" MaxLength="11"
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
                <iFg:CheckBoxField DataField="SLCT" HeaderText="Select" HeaderTitle="Select" HelpText=""
                    SortAscUrl="" SortDescUrl="" HeaderImageUrl="" ReadOnly="false">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="30px" Wrap="True" HorizontalAlign="Center" />
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
                <iFg:ActionButton ID="acnRefresh" Text="Refresh" ValidateRowSelection="False" OnClientClick="bindGrid();"
                    IconClass="icon-refresh" CSSClass="btn btn-small btn-info" />
            </ActionButtons>
        </iFg:iFlexGrid>
        <br /><br />
         <sp:SubmitPane ID="PageSubmitPane"  runat="server" onclientsubmit="submitPage();"
        onclientprint="null" />
    </div>
   
    <asp:HiddenField ID="hdnPageTitle" runat="server" />
    <%--<asp:HiddenField ID="hdnchkdgtvalue" runat="server" />
    <asp:HiddenField ID="hdnCurrentDate" runat="server" />--%>
    </form>
</body>
</html>
