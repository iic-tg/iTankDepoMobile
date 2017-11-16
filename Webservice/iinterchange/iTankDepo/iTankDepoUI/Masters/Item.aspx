<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Item.aspx.vb" Inherits="Masters_Item" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 20px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh" runat="server">Item</span>
                </td>
                <td align="right">
                    <nv:Navigation ID="navItem" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Fix -->
    <div id="tabItem" style="height: 250px;">
        <table class="tblstd" cellpadding="1" cellspacing="1" style="margin: 0px auto;" align="left">
            <tr>
                <td>
                    <Inp:iLabel ID="lblItemCode" runat="server" CssClass="lbl">
                        Item Code
                    </Inp:iLabel>
                    <Inp:iLabel ID="lblItemCodeReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtItemCode" runat="server" CssClass="txt" iCase="Upper" HelpText="212,ITEM_ITM_CD" MaxLength="4">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Only Alphabets and Numbers are allowed"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" Type="String" Validate="True"
                            ValidationGroup="tabItem" LookupValidation="False" CsvErrorMessage="This Code Already Exists"
                            CustomValidationFunction="validateItemCode" IsRequired="true" ReqErrorMessage="Item Code Required"
                            CustomValidation="true" RegexValidation="true" />
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblItemDescription" runat="server" CssClass="lbl">
                        Item Description
                    </Inp:iLabel><Inp:iLabel ID="lblItemDescriptionReq" runat="server" ToolTip="*" CssClass="lblReq">
  *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtItemDescription" runat="server" CssClass="txt" iCase="Upper"
                        HelpText="213,ITEM_ITM_DSCRPTN_VC">
                        <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" IsRequired="True"
                            ReqErrorMessage="Item Description Required" Validate="true"></Validator>
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblActiveBit" runat="server" CssClass="lbl">
                        Active
                    </Inp:iLabel>
                </td>
                <td>
                    <asp:CheckBox ID="chkActiveBit" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="11">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="11">
                    <table style="margin: 0px auto; width: 100%">
                        <tr>
                            <td>
                                <label id="lblSubItemHeader" runat="server" class="lbl">
                                    <strong><u>Sub Item</u></strong>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iFg:iFlexGrid ID="ifgSubItem" runat="server" AllowStaticHeader="True" DataKeyNames="SB_ITM_ID"
                                    Width="100%" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Justify"
                                    PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                    Scrollbars="None" ShowEmptyPager="True" StaticHeaderHeight="200px" Type="Normal"
                                    ValidationGroup="divSubItem" UseCachedDataSource="True" AutoGenerateColumns="False"
                                    EnableViewState="False" OnAfterClientRowCreated="setDefaultValues" OnAfterCallBack="OnAfterCallBack"
                                    OnBeforeCallBack="OnBeforeCallBack" Mode="Insert" AllowPaging="false" AutoSearch="True"
                                    UseIcons="true" SearchButtonIconClass="icon-search" SearchButtonCssClass="btn btn-small btn-primary"
                                    AddButtonIconClass="icon-plus" AddButtonCssClass="btn btn-small btn-success"
                                    DeleteButtonIconClass="icon-trash" DeleteButtonCssClass="btn btn-small btn-danger"
                                    RefreshButtonIconClass="icon-refresh" RefreshButtonCssClass="btn btnmcorner btn-small btn-primary"
                                    SearchCancelButtonIconClass="icon-remove" ClearButtonCssClass="icon-eraser">
                                    <RowStyle CssClass="gitem" />
                                    <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                    <Columns>
                                        <iFg:TextboxField DataField="SB_ITM_CD" CharacterLimit="0" HeaderText="Code *" HeaderTitle="Code"
                                            SortAscUrl="" SortDescUrl="" >
                                            <TextBox CssClass="txt" iCase="Upper" OnClientTextChange="" ID="txtSubItemCode" runat="server" MaxLength="3"
                                                ValidationGroup="">
                                                <Validator CustomValidateEmptyText="False" IsRequired="True" Operator="Equal" ReqErrorMessage="Sub Item Code Required."
                                                    Type="String" CustomValidationFunction="validateSubItemCode" CustomValidation="true"
                                                    CsvErrorMessage="Exist" Validate="True" />
                                            </TextBox>
                                            <ItemStyle Width="200px" Wrap="true" />
                                        </iFg:TextboxField>
                                        <iFg:TextboxField DataField="SB_ITM_DSCRPTN_VC" CharacterLimit="0" HeaderText="Description *"
                                            HeaderTitle="Description" SortAscUrl="" SortDescUrl="">
                                            <TextBox CssClass="txt" iCase="Upper" OnClientTextChange="" ValidationGroup="" MaxLength="255">
                                                <Validator CustomValidateEmptyText="False" IsRequired="True" Operator="Equal" ReqErrorMessage="Sub Item Description Required."
                                                    Type="String" Validate="true" />
                                            </TextBox>
                                            <ItemStyle Width="300px" Wrap="true" />
                                        </iFg:TextboxField>
                                        <iFg:CheckBoxField DataField="ACTV_BT" HeaderText="Active" HeaderTitle="Active" HelpText=""
                                            SortAscUrl="" SortDescUrl="" SortExpression="">
                                            <ItemStyle Width="25px" />
                                        </iFg:CheckBoxField>
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
                </td>
            </tr>
        </table>
    </div>
    <div id="divSubmit">
        <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" onClientPrint="null" />
    </div>
    </form>
</body>
</html>
