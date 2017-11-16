<%@ Page Language="VB" AutoEventWireup="false" CodeFile="User.aspx.vb" Inherits="Admin_User" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" style="overflow:auto">
    <div id="divhdr" runat="server">
        <table id="ITab1_0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="ctab" style="width: 100%; height: 30px;">
                <td align="left">
                    <span id="spnHeader" class="ctabh">User</span>
                </td>
                <td align="right">
                    <nv:navigation id="navEstimate" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- UIG Issue Fix (IE)- Issue No:2 -  Grid footer not visible and Submit Button not visible  -->
    <div class="" id="tabUser" runat="server" align="center" style="overflow-y: auto;overflow-x: auto; height: auto">
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd">
            <tr>
                <td>
                    <label id="Label1" runat="server" class="lbl">
                        *</label>
                </td>
                <td align="left">
                    <label id="lblUserName" runat="server" class="lbl">
                        User Name</label>
                </td>
                <td>
                    <inp:itextbox id="txtUserName" runat="server" onkeydown="placeCursor('txtPassword','txtUserName');" cssclass="txt" tabindex="1" icase="Upper" helptext="5,USER_DETAIL_USR_NAM">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Operator="Equal" RegErrorMessage="Only alphabets and numbers are Allowed"
                            RegexValidation="True" RegularExpression="^[a-zA-Z0-9]+$" ReqErrorMessage="User Name Required"
                            Type="String" Validate="True" ValidationGroup="tabUser" LookupValidation="False"
                            CsvErrorMessage="This User Name already exists" CustomValidation="True" CustomValidationFunction="validateUserName" />
                    </inp:itextbox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td rowspan="4">
                    <div id="divPhoto" style="height: 130px">
                        <img width="120" src="../Images/noimage.jpg" id="imgCompanyLogo" runat="server" title="Click to change photo" onclick="BrowseImage();" style="border: solid 2px #e5e0ec" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <label id="Label2" runat="server" class="lbl">
                        *</label>
                </td>
                <td valign="top" align="left">
                    <label id="lblPassword" runat="server" class="lbl">
                        Password</label>
                </td>
                <td nowrap="nowrap" style="vertical-align:top;">
                    <inp:itextbox id="txtPassword" runat="server" cssclass="txt" tabindex="2" textmode="Password" helptext="6,USER_DETAIL_PSSWRD">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" ReqErrorMessage="Password Required"
                            Type="String" Validate="True" ValidationGroup="tabPwd" LookupValidation="False"
                            RegErrorMessage="New Password must be at least 7 characters with at least one capital letter, one small letter, one numeric and one symbol"
                            RegexValidation="True" RegularExpression="^(?=.{7,20}$)(?=.*[\d])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%\-\^&amp;*()_<+=\|{}])[\w\d!@#$%^\-\&amp;*()_<+=\|{}].*"
                            IsRequired="True" />
                    </inp:itextbox>
                    <a id="lnkEditPassword" runat="server" href="#" style="display: none; width: 45px;" onclick="onEditClick(); return false;">Edit</a>
                </td>
                <td nowrap="nowrap">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" >
                    <label id="Label3" runat="server" class="lbl">
                        *</label>
                </td>
                <td align="left" valign="top" >
                    <label id="lblConfirmPassword" runat="server" class="lbl" width="126px">
                        Confirm Password</label>
                </td>
                <td style="vertical-align:top;">
                    <inp:itextbox id="txtConfirmPassword" runat="server" cssclass="txt" tabindex="3" textmode="Password" helptext="7,USER_DETAIL_PSSWRD">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Operator="Equal" ReqErrorMessage="Confirm Password Required"
                            Type="String" Validate="True" ValidationGroup="tabPwd" CmpErrorMessage="Passwords does not match"
                            CompareValidation="True" ControlToCompare="txtPassword" LookupValidation="False" />
                    </inp:itextbox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <label id="Label4" runat="server" class="lbl">
                        *</label>
                </td>
                <td align="left" valign="top">
                    <label id="lblFirstName" runat="server" class="lbl">
                        First Name</label>
                </td>
                <td style="vertical-align:top;">
                    <inp:itextbox id="txtFirstName" runat="server" cssclass="txt" tabindex="4" helptext="8,USER_DETAIL_FRST_NAM">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Operator="Equal" RegErrorMessage="Only Alphabets are Allowed"
                            RegexValidation="True" RegularExpression="^[a-zA-Z]+$" ReqErrorMessage="First Name Required"
                            Type="String" Validate="True" ValidationGroup="tabUser" LookupValidation="False" />
                    </inp:itextbox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <label id="Label5" runat="server" class="lbl">
                        *</label>
                </td>
                <td align="left" valign ="top" >
                    <label id="lblLastName" runat="server" class="lbl">
                        Last Name</label>
                </td>
                <td style="vertical-align:top;">
                    <inp:itextbox id="txtLastName" runat="server" cssclass="txt" tabindex="5" helptext="9,USER_DETAIL_LST_NAM">
                        <Validator CustomValidateEmptyText="False" IsRequired="True" Operator="Equal" RegErrorMessage="Only Alphabets are Allowed"
                            RegexValidation="True" RegularExpression="^[a-zA-Z]+$" ReqErrorMessage="Last Name Required"
                            Type="String" Validate="True" ValidationGroup="tabUser" LookupValidation="False" />
                    </inp:itextbox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="5"></td>
            </tr>
                       
            <tr>
                <td valign="top">
                    <label id="Label8" runat="server" class="lbl">
                        *</label>
                </td>
                <td align="left" valign="top">
                    <label id="lblEmailId" runat="server" class="lbl">
                        Email ID</label>
                </td>
                <td style="vertical-align:top;">
                    <inp:itextbox id="txtEmailId" runat="server" cssclass="txt" tabindex="6" helptext="10,USER_DETAIL_EML_ID" textmode="SingleLine" icase="None">
                        <Validator CustomValidateEmptyText="False" Operator="Equal" RegErrorMessage="Invalid Email format"
                            
                            RegularExpression="\w+([&amp;-+.']\w+)*@\w+([&amp;-.]\w+)*\.\w+([&amp;-.]\w+)*" Type="String"
                            Validate="True" ValidationGroup="tabUser" LookupValidation="False" CsvErrorMessage=""
                            CustomValidationFunction="validateEmailID" RegexValidation="True" IsRequired="True"
                            ReqErrorMessage="Email ID Required" />
                    </inp:itextbox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                  <%--  <label id="lblTheme" runat="server" class="lbl">
                        Theme</label>--%>
                </td>
            </tr>
            <tr>
                <td colspan="5"></td>
            </tr>

            <tr>
                <td valign ="top">
                    <label id="lblDepotField" runat="server" class="lbl">
                        *</label>
                </td>
                <td align="left" valign="top" >
                    <label id="lblDepoCode" runat="server" class="lbl">
                        Depot Code</label>
                </td>
                <td style="vertical-align:top;">
                    <inp:ilookup id="lkpDepotCode" runat="server" cssclass="lkp" datakey="DPT_CD" dosearch="True" icase="Upper" tabindex="7" 
                    tablename="29" helptext="613,USER_DETAIL_DPT_ID"  >
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="DPT_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="DPT_CD" ControlToBind="lkpDepotCode" Hidden="False" />
                            <Inp:LookupColumn ColumnName="DPT_NAM" Hidden="False" />
                        </LookupColumns>
                        <LookupGrid CurrentPageIndex="0" PageSize="5" Width="300px" VerticalAlign="Top" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Depot Code. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Depot Code Required" Type="String" Validate="True"
                            ValidationGroup="tabUser" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </inp:ilookup>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>  <label id="lblTheme" runat="server" class="lbl">
                        Theme</label></td>
                <%--<td>
                    <div id="Div1" class="RadioTheme">
                        <input type="radio" id="Radio1" name="radio" runat="server" /><label>Blue</label>
                        <input type="radio" id="Radio2" name="radio" runat="server" /><label>Matrix</label>
                        <input type="radio" id="Radio3" name="radio" runat="server" /><label>Classic</label>
                    </div>
                </td>--%>
            </tr>
            <tr>
                <td colspan="5"></td>
            </tr>
            <tr>
                <td valign ="top">
                    <label id="Label7" runat="server" class="lbl">
                        *</label>
                </td>
                <td align="left" valign="top" >
                    <label id="lblRole" runat="server" class="lbl">
                        Role</label>
                </td>
                <td style="vertical-align:top;">
                    <inp:ilookup id="lkpRole" runat="server" cssclass="lkp" datakey="RL_CD" dosearch="True" icase="Upper" tabindex="7" 
                    tablename="1" helptext="11,ROLE_RL_CD" clientfilterfunction="applyDepoFilter">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="RL_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="RL_CD" ControlToBind="lkpRole" Hidden="False" />
                            <Inp:LookupColumn ColumnName="RL_DSCRPTN_VC" Hidden="False" />
                        </LookupColumns>
                        <LookupGrid CurrentPageIndex="0" PageSize="5" Width="300px" VerticalAlign="Top" />
                        <InfoIcon CssClass="mimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="False"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="True" LkpErrorMessage="Invalid Role. Click on the list for valid values"
                            Operator="Equal" ReqErrorMessage="Role Required" Type="String" Validate="True"
                            ValidationGroup="tabUser" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </inp:ilookup>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <div id="radio" class="RadioTheme">
                        <input type="radio" id="rboBlue" name="radio" runat="server" /><label>Blue</label>
                        <input type="radio" id="rboMatrix" name="radio" runat="server" /><label>Matrix</label>
                        <input type="radio" id="rboClassic" name="radio" runat="server" /><label>Classic</label>
                    </div>
                </td>
            </tr>
          
            <tr>
                <td colspan="5"></td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" valign="top" >
                    <label id="lblActive" runat="server" class="lbl">
                        Active</label>
                </td>
                <td align="left" style="vertical-align:top;">
                    <asp:CheckBox ID="chkActive" runat="server" Width="97px" TabIndex="9" />
                </td>
            </tr>
        </table>
    </div>
    <div class="tblstd"  align="center" >
        <sp:submitpane id="PageSubmitPane" runat="server" tabindex="14" onclientsubmit="submitPage();" onClientPrint="null"  />
    </div>
    <iframe id="fmPhotoUpload" frameborder="0" height="0px" width="0px" name="fmPhotoUpload" src="PhotoUpload.aspx"></iframe>
    <input id="hdnLogoPath" name="hdnLogoPath" type="hidden" runat="server" />
    </form>
</body>
</html>
