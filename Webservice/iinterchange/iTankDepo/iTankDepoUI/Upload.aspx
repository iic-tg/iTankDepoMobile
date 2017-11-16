<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Upload.aspx.vb" Inherits="U_Upload" %>

<!DOCTYPE HTML >
<html >
<head runat="server">
    <title>Upload</title>
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function filename_onclick() {

        }

        function file1_onclick() {

        }

        function file1_onclick() {

        }

// ]]>
    </script>
    <!-- [if lt IE 7]>
   <link href="Styles/Default/font-awesome-ie7.min.css" rel="stylesheet" />
<! [endif] -->
</head>
 <!-- UIG Fix -->
<body class ="metro">
    <form id="frmUpload" runat="server" style="overflow:auto">
    <div style="overflow: auto;">
        <div id="shadow" class="opaqueLayer">
        </div>
        <%--<div id="question" class="questionLayer">
            <table id="tblmodalwindow" style="width: 150px; height: 30px">
                <tr>
                    <td id="tdinfomodalwindowtitleimage" style="width: 40px;">
                    <!-- UIG Fix -->
                        <input id="imgLoading" type="image" class="loadingimg" src="Images/ajloader.gif" />
                    </td>
                    <td id="tdmodalwindowtitle" class="lmodalwindowtitle" style="width: 110px;">
                    </td>
                </tr>
            </table>
        </div>--%>
                            <div id="question" class="questionLayer">
        <div id="tblmodalwindow" style="margin-top: 1px;width:170px">
            <div id="divinfomodalwindowtitleimage" style="width: 40px; float: left">
                <i id="imgLoading" class="icon-spinner icon-spin icon-large"></i>
            </div>
            <div class="slbl">
                <span id="tdmodalwindowtitle"></span>
            </div>
                    </div>
 </div>

        <div style="overflow: auto">
            <table border="0" cellpadding="2" cellspacing="2" class="tblstd" align="center">
                <tr>
                    <td>
                        <fieldset>
                            <legend class="lbl" style="font-weight: bold">Upload</legend>
                            <table align="center" cellpadding="2" cellspacing="2" class="tblstd">
                                <tr>
                                    <td style="width: 75px">
                                        <label id="lblCustomerCode" runat="server" class="lbl">
                                            Select File</label>
                                    </td>
                                    <td>
                                        <input type="file" id="filename" contenteditable="false" runat="server" class="txt"
                                            style="width: 200px;" onchange="uploadedSubmit();" onclick="return filename_onclick()" />
                                    </td>
                                    <td>
                                        <a href="#" tabindex="4" id="btnSubmit" class="btn btn-small btn-info" onclick="submitUpload();return false;">
                                            <i class="icon-upload-alt"></i>Upload</a>
                                    </td>
                                    <td style="width: 25px">
                                    </td>
                                    <td align="left" >
                                        <a href="#" class="btn btn-small btn-info" onclick="DownloadTemplate(this)"
                                            style="margin-left: 15px; font-size: 12px; "><i class="icon-download-alt"></i>Download
                                            Template</a>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        &nbsp;
                    </td>
                    <td style="width: 75px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <div id="divlnk" runat="server">
                            <a href="#" style="margin-left: 10px; font-size: 12px" onclick="ShowData(this)" shape="rect"
                                name="Valid">Valid Data</a> <a href="#" style="margin-left: 10px; font-size: 12px"
                                    onclick="ShowData(this)" name="InValid">InValid Data</a> &nbsp;&nbsp;&nbsp;&nbsp;
                            <Inp:iLabel ID="lblData" runat="server" ToolTip="Data" CssClass="lbl" />
                        </div>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <div id="divRecordNotFound" runat="server" style="margin: 10px; font-style: italic;
                            font-family: Arial; font-size: 8pt; display: none; width: 100%;">
                            <Inp:iLabel ID="lblListRowCount" runat="server" Visible="true">No Records Found</Inp:iLabel>
                        </div>
                        <div id="divifgGrid" runat="server" style="overflow: auto">
                            <table align="center" style="width: 650px" border="0" cellpadding="2" cellspacing="2"
                                class="tblstd">
                                <tr>
                                    <td>
                                        <iFg:iFlexGrid ID="ifgUploadStatus" runat="server" AllowPaging="True" CaptionAlign="Left"
                                            CellPadding="2" CssClass="tblstd" GridLines="Both" PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                                            Type="Normal" ValidationGroup="" PageSize="10" StaticHeaderHeight="200px" ShowEmptyPager="True"
                                            Width="650px" AllowAdd="False" AllowDelete="False" AllowSearch="True" AllowStaticHeader="True"
                                            AutoSearch="True" HeaderRows="1" EnableViewState="False" SortAscImageUrl="~/Images/down.gif"
                                            HorizontalAlign="NotSet" OnAfterCallBack="onAfterCallBack" AllowEdit="False"
                                            Scrollbars="Auto" AllowRefresh="True" AutoGenerateColumns="False" AddRowsonCurrentPage="False"
                                            AddButtonCssClass="button success add" DeleteButtonIconClass="icon-remove" DeleteButtonCssClass="button danger delete"
                                            RefreshButtonCssClass="button info search" ClearButtonCssClass="button success add"
                                            ClearButtonIconClass="icon-battery-empty" SearchCancelButtonCssClass="button danger delete"
                                            RefreshButtonIconClass="icon-cycle" SearchCancelButtonIconClass="icon-cancel-2"
                                            SearchButtonCssClass="button info search"
                                            UseCachedDataSource="True" >
                                            <RowStyle CssClass="gitem" />
                                            <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                                            <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                                                OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                            <PagerStyle CssClass="gpage" Height="18px" Font-Names="Arial" HorizontalAlign="Center" />
                                            <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                                            <SelectedRowStyle CssClass="gsitem" />
                                            <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="NotSet"
                                                IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                                        </iFg:iFlexGrid>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table align="center">
                            <tr>
                                <td style="height:80px;">
                                    <a href="#" data-corner="8px" id="btnCancel" style="text-decoration: none; color: White;
                                    font-weight: bold; height :18px;" class="btn btn-small btn-info" runat="server" onclick="closeUpload();return false;">
                                    Close</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hdnFileName" runat="server" />
    <asp:HiddenField ID="hdnDpt_ID" runat="server" />
    <asp:HiddenField ID="hdnUploadTblName" runat="server" />
    <asp:HiddenField ID="hdnIdColumn" runat="server" />
    </form>
</body>
</html>
