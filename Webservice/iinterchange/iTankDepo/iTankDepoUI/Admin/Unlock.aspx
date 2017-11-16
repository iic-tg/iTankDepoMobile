<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Unlock.aspx.vb" Inherits="Admin_Unlock" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../Script/Admin/Unlock.js" type="text/javascript" language="javascript"></script>
    <title></title>
</head>
<body>
<%--Record Locking Unlock--%>
    <form id="form1" runat="server" >
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Admin &gt;&gt; Unlock</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navCleaningInstruction" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divPending" class="tabdisplayGatePass">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" style="width:100%;">
            <tr style="width: 100%; height: 100%;">
                <td>
                    <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                        font-family: Arial; font-size: 8pt; display: none; width: 100%;" align="center">
                        <div>
                            No Records Found.</div>
                    </div>
                    <div id="divUnlock" style="margin: 1px; width: 100%; height: 440px; vertical-align: middle;
                        overflow-x: auto; width:100%;" >
                        <iFg:iFlexGrid ID="ifgUnlock" runat="server" AllowStaticHeader="True" DataKeyNames="RefNo"
                            Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                            PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                            Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="380px" Type="Normal"
                            ValidationGroup="divEquipmentDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                            EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                            AddRowsonCurrentPage="False" OnAfterCallBack="showHideRecords" OnBeforeCallBack=""
                            AllowSearch="True" AllowPaging="True" UseIcons="True" SearchButtonCssClass="btn btn-small btn-info"
                            AddButtonCssClass="btn btn-small btn-success" DeleteButtonCssClass="btn btn-small btn-danger"
                            RefreshButtonCssClass="btn btn-small btn-info" SearchCancelButtonCssClass="btn btn-small btn-danger"
                            ClearButtonCssClass="btn btn-small btn-success" AutoSearch="True" AllowRefresh="True"
                            Mode="Insert" UseActivitySpecificDatasource="True" AllowAdd="False" AllowDelete="False">
                            <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <RowStyle CssClass="gitem" />
                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                            <Columns>
                                <iFg:TextboxField DataField="ActivityName" HeaderText="Activity Name" HeaderTitle="Activity Name"
                                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                                    <TextBox CausesValidation="True" CssClass="txt" HelpText="Activity Name" iCase="Upper"
                                        MaxLength="50" OnClientTextChange="" ValidationGroup="">
                                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="RefNoField" HeaderText="Reference No Field" HeaderTitle="Reference No Field"
                                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                                    <TextBox CausesValidation="True" CssClass="txt" HelpText="Reference No" iCase="Upper"
                                        MaxLength="50" OnClientTextChange="" ValidationGroup="">
                                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="RefNo" HeaderText="Reference No" HeaderTitle="Reference No"
                                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                                    <TextBox CausesValidation="True" CssClass="txt" HelpText="Reference No" iCase="Upper"
                                        MaxLength="50" OnClientTextChange="" ValidationGroup="">
                                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="IPAddress" HeaderText="IP Address" HeaderTitle="IP Address"
                                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                                    <TextBox CausesValidation="True" CssClass="txt" HelpText="IP Address" iCase="Upper"
                                        MaxLength="50" OnClientTextChange="" ValidationGroup="">
                                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="LockedTime" HeaderText="Locked Time" HeaderTitle="Locked Time"
                                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                                    <TextBox CausesValidation="True" CssClass="txt" HelpText="Locked Time" iCase="Upper"
                                        MaxLength="50" OnClientTextChange="" ValidationGroup="">
                                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:TextboxField DataField="LockedBy" HeaderText="User Name" HeaderTitle="User Name"
                                    SortAscUrl="" SortDescUrl="" AllowSearch="true" ReadOnly="true">
                                    <TextBox CausesValidation="True" CssClass="txt" HelpText="User Name" iCase="Upper"
                                        MaxLength="50" OnClientTextChange="" ValidationGroup="">
                                        <Validator Operator="Equal" Type="String" Validate="true" CustomValidation="False"
                                            CustomValidationFunction="" IsRequired="True" ReqErrorMessage="" />
                                    </TextBox>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="100px" Wrap="True" />
                                </iFg:TextboxField>
                                <iFg:HyperLinkField DataTextField="Unlock" Text="Unlock" HeaderText="Unlock" HeaderTitle=""
                                    MaxLength="0" SortAscUrl="" SortDescUrl="" IsEditable="False" ReadOnly="True"
                                    SortExpression="Unlock" AllowSearch="true">
                                    <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="90px" />
                                </iFg:HyperLinkField>
                            </Columns>
                            <FooterStyle CssClass="gftr" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="gsitem" />
                            <AlternatingRowStyle CssClass="gaitem" />
                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                        </iFg:iFlexGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
