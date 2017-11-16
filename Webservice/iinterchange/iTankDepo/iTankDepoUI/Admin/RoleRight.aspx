<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RoleRight.aspx.vb" Inherits="Admin_RoleRight" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script src="../Script/Controls/iTreeFunctions.js" type="text/javascript" language="javascript"></script>
    <script src="../Script/Common.js" type="text/javascript"></script>
    <script src="../Script/Admin/RoleRight.js" type="text/javascript" language="javascript"></script>
</head>
<body onload="hide();">
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">Admin>>Role Rights</span>
                </td>
                <td align="right">
                      <nv:Navigation ID="navEstimate" runat="server" />
                </td> 
            </tr>
        </table>
    </div>
    <div id="tabRoleRights" class="tabdisplayGatePass" style="height: 240px">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd" width="700px">
            <tr>
                <td>
                    <Inp:iLabel ID="lblRoleCodereq1" runat="server" ToolTip="*" CssClass="lbl">
                      *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iLabel ID="lblRoleCode" runat="server" ToolTip="Role Code" CssClass="lbl">
                      Role Code</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtRoleCode" runat="server" TabIndex="1" CssClass="txt" ToolTip="Role Code"
                        HelpText="16,ROLE_RL_CD" Height="" TextMode="SingleLine" iCase="Upper" Width="" MaxLength="10">
                        <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="true"
                            ControlToCompare="" CsvErrorMessage="Role Code Already Exists" CustomValidationFunction="validateRoleCode"
                            IsRequired="True" ReqErrorMessage="Role Code Required" ValidationGroup="tabRoleRights"
                            RegularExpression="^[a-zA-Z0-9]+$" RegexValidation="True" RegErrorMessage="Only Alphabets and Numbers are Allowed"
                            Validate="True"></Validator>
                    </Inp:iTextBox>
                </td>
                <td>
                    <Inp:iLabel ID="lblRoleDescriptionreq1" runat="server" ToolTip="*" CssClass="lbl">
                          *</Inp:iLabel>
                </td>
                <td>
                    <Inp:iLabel ID="lblRoleDescription" runat="server" ToolTip="Role Description" CssClass="lbl">
                          Role Description</Inp:iLabel>
                </td>
                <td>
                    <Inp:iTextBox ID="txtRoleDescription" runat="server" TabIndex="2" CssClass="txt"
                        ToolTip="Role Description" HelpText="17,ROLE_RL_DSCRPTN_VC" Height="" TextMode="SingleLine"
                        iCase="None" Width="">
                        <Validator Type="String" Operator="Equal" CustomValidateEmptyText="False" CustomValidation="false"
                            ControlToCompare="" CsvErrorMessage="" CustomValidationFunction="" IsRequired="True"
                            ReqErrorMessage="Role Description Required" ValidationGroup="tabRoleRights" Validate="True"
                            RegularExpression="^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$" RegexValidation="True"
                            RegErrorMessage="Only Alphabets/Numbers and [-_.'&\/[](),] are allowed"></Validator>
                    </Inp:iTextBox>
                </td>
                <td>
                </td>
                <td>
                    <Inp:iLabel ID="lblActiveBit" runat="server" ToolTip="Active" CssClass="lbl">
                              Active</Inp:iLabel>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActiveBit" runat="server" ToolTip="Active" TabIndex="3" Checked="true" />
                    </td>    
                    <%--Dashboard--%>
                     <td></td>
                      <td>
                    <Inp:iLabel ID="lblDashboardActiveBit" runat="server" ToolTip="DashboardActive" CssClass="lbl">
                              Show Dashboard</Inp:iLabel>
                </td>
                <td>
                    <asp:CheckBox ID="chkDashboardActiveBit" runat="server" ToolTip="DashboardActive" TabIndex="4" Checked="true" />
                </td>               
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" border="0" style="height: 180px">
                <tr>
                    <td valign="top" style="cursor: hand; width: 180px;">
                        <tvw:TreeView ID="ITreeView1" runat="server" />
                        <asp:PlaceHolder ID="plhTree" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="display: none;">
                            <sp:SubmitPane ID="PageSubmitPane" runat="server" TabIndex="19" onClientSubmit="submitPage();" onClientPrint="null"  />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <center>
            <div class="button" style="width: 150px" runat="server" align="center">
                <ul>
                    <li>
                   <button id="btnSubmit" class="btn btn-success" style="border: 0px;" runat="server">
  <i class="icon-save"></i> Submit</button>
                      
                    </li>
                </ul>
            </div>
        </center>
        <div style="display: none">
            <asp:Button ID="btnSubmitDummy" runat="server" Text="Submit" />
            <
        </div>
        <asp:HiddenField ID="hdnModify" runat="server" />
        <asp:HiddenField ID="hdnRoleId" runat="server" />
        <asp:HiddenField ID="hdnMode" runat="server" />
        <asp:HiddenField ID="hdnPageMode" runat="server" />
        <asp:HiddenField ID="hdnKeys" runat="server" />
        <asp:HiddenField ID="hdnID" runat="server" />
        <mnu:Menu ID="iMenuControl" runat="server" />
        <asp:HiddenField ID="hdnRoleCode" runat="server" />
        <asp:HiddenField ID="hdnDescription" runat="server" />
        <asp:HiddenField ID="hdnActive" runat="server" />
         <asp:HiddenField ID="hdnDashboardActive" runat="server" />
    </form>
    <script language="javascript">
        function getPageMode() {
            return el("hdnPageMode").value;
        }
    
    </script>
</body>
</html>
