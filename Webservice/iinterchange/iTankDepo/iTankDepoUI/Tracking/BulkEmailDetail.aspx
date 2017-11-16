<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BulkEmailDetail.aspx.vb"
    Inherits="Tracking_BulkEmailDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server" style="overflow:auto">
    <div id="divBulkEmailDetail">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd">
            <tr style="width: 100%;">
                <td colspan="5">
                    <iFg:iFlexGrid ID="ifgBulkEmailDetail" runat="server" AllowStaticHeader="True" DataKeyNames="BLK_EML_ID"
                        Width="860px" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        Scrollbars="None" ShowEmptyPager="False" StaticHeaderHeight="150px" Type="Normal"
                        ValidationGroup="divBulkEmailDetail" UseCachedDataSource="True" AutoGenerateColumns="False"
                        EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                        PageSize="10" AddRowsonCurrentPage="False" ShowPageSizer="True" AllowPaging="True"
                        AllowAdd="false" AllowDelete="False" ShowFooter="True" OnAfterCallBack="" AllowRefresh="False"
                        AllowSearch="False" AutoSearch="False" OnBeforeCallBack="" RowStyle-HorizontalAlign="NotSet"
                        UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-info"
                        AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                        DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                        RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btn-small btn-info"
                        SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser">
                        <PagerStyle CssClass="gpage" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <Columns>
                            <iFg:TextboxField CharacterLimit="0" DataField="TO_EML" HeaderText="To Email" HeaderTitle="To Email"
                                SortAscUrl="" SortDescUrl="" HeaderStyle-Width="60px" AllowSearch="true" ReadOnly="true">
                                <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="divBulkEmailDetail"
                                    MaxLength="5">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                        RegexValidation="False" LookupValidation="False" />
                                </TextBox>
                                <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="60px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:TextboxField CharacterLimit="0" DataField="BCC_EML" HeaderText="BCC" HeaderTitle="BCC"
                                SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" AllowSearch="true" ReadOnly="true">
                                <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="divBulkEmailDetail"
                                    MaxLength="5">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                        RegexValidation="False" LookupValidation="False" />
                                </TextBox>
                                <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="60px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:TextboxField CharacterLimit="0" DataField="SBJCT_VC" HeaderText="Subject" HeaderTitle="Subject"
                                SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" AllowSearch="true" ReadOnly="true">
                                <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="divBulkEmailDetail"
                                    MaxLength="5">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                        RegexValidation="False" LookupValidation="False" />
                                </TextBox>
                                <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="80px" Wrap="True" />
                            </iFg:TextboxField>
                            <iFg:TextboxField CharacterLimit="0" DataField="SNT_DT" HeaderText="Sent Date & Time" HeaderTitle="Sent Date & Time"
                                SortAscUrl="" SortDescUrl="" HeaderStyle-Width="40px" AllowSearch="true" ReadOnly="true" DataFormatString="{0:dd-MMM-yyyy hh:mm:ss tt}" HtmlEncode="false" >
                                <TextBox CssClass="txt" HelpText="" iCase="None" OnClientTextChange="" ValidationGroup="divBulkEmailDetail"
                                    MaxLength="5">
                                    <Validator CustomValidateEmptyText="False" Operator="Equal" Type="String" Validate="False"
                                        RegexValidation="False" LookupValidation="False" />
                                </TextBox>
                                <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle Width="60px" Wrap="True" />
                            </iFg:TextboxField>
                             <iFg:ImageField HeaderTitle="View" SortAscUrl="" SortDescUrl="" DataImageUrlField="../Images/letter.png"
                                    HeaderText="View" HeaderImageUrl="">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Center" />
                                </iFg:ImageField>
                        </Columns>
                        <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="gsitem" />
                        <AlternatingRowStyle CssClass="gaitem" />
                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                    </iFg:iFlexGrid>
                </td>
            </tr>
        </table>
          <table align="center">
            <tr>
                <td align="center">
                    <div class="button">
                        <ul>
                            <li Class="btn btn-small btn-info"><a href="#" tabindex="5" id="btnSubmit" data-corner="8px" Class="icon-remove" onclick="CloseBulkEmailDetail();return false;" style="text-decoration: none; color: White; font-weight: bold">&nbsp;Close</a></li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnEquipmentNo" runat="server" />
    <asp:HiddenField ID="hdnGateinTransactionNo" runat="server" />
    </form>
</body>
</html>
