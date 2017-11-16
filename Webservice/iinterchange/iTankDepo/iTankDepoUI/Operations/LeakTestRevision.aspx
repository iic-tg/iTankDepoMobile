<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeakTestRevision.aspx.vb" Inherits="Operations_LeakTestRevision" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="initPage();">
    <form id="form1" runat="server" style="overflow:auto">
    <div id="divRevisionHistory" style="margin: 1px; width: 970px; height: 440px;">
        <table>
            <tr>
                <td>
                    <iFg:iFlexGrid ID="ifgRevisionHistory" runat="server" AllowStaticHeader="True" DataKeyNames="LK_TST_ID"
                        Width="970px" CaptionAlign="NotSet" GridLines="Both" HeaderRows="1" HorizontalAlign="Center"
                        PageSizerFormat="" RecordCountFormat="Page {CPI} of {TPC}|{TRC} records available"
                        Scrollbars="None" ShowEmptyPager="False" StaticHeaderHeight="250px" Type="Normal"
                        ValidationGroup="divRevisionHistory" UseCachedDataSource="True" AutoGenerateColumns="False"
                        EnableViewState="False" OnBeforeClientRowCreated="" OnAfterClientRowCreated=""
                        AddRowsonCurrentPage="False" ShowPageSizer="False" OnAfterCallBack="" OnBeforeCallBack=""
                        AllowAdd="False" AllowDelete="False" AllowFilter="False">
                        <Columns>
                             <iFg:BoundField DataField="RVSN_NO" HeaderText="Rev No" HeaderTitle="Revision No"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="50px" Wrap="True" />
                            </iFg:BoundField>
                                                         <iFg:DateField DataField="TST_DT" HeaderText="Test Date" HeaderTitle="Test Date" SortAscUrl=""
                                SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ReadOnly="true">
                                <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                    ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                    <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                        ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                        LkpErrorMessage="Invalid Gate In Date. Click on the calendar icon for valid values"
                                        ReqErrorMessage="Event Date Required" Validate="True" RangeValidation="false"
                                        CompareValidation="false" ValidationGroup="divEquipmentDetail" />
                                </iDate>
                               <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="70px" Wrap="True" />
                            </iFg:DateField>
                             <%-- <iFg:BoundField DataField="TST_DT" HeaderText="Test Date" HeaderTitle="Test Date" DataFormatString="{0:dd-MMM-yyyy}"  HtmlEncode="false" 
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" iCase ="Upper"  >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="80px" Wrap="True" />
                            </iFg:BoundField>--%>
                              <iFg:BoundField DataField="SHLL_TST" HeaderText="Shell Test" HeaderTitle="Shell Test"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Center"  />
                            </iFg:BoundField>
                              <iFg:BoundField DataField="STM_TB_TST" HeaderText="Steam Tube Test" HeaderTitle="Steam Tube Test"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Center"  />
                            </iFg:BoundField>
                              <iFg:BoundField DataField="RLF_VLV_SRL_1" HeaderText="Relief Valve Srl #1" HeaderTitle="Relief Valve Srl #1"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="60px" Wrap="True" />
                            </iFg:BoundField>
                              <iFg:BoundField DataField="RLF_VLV_SRL_2" HeaderText="Relief Valve Srl #2" HeaderTitle="Relief Valve Srl #2"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="60px" Wrap="True" />
                            </iFg:BoundField>
                              <iFg:BoundField DataField="PG_1" HeaderText="Pressure Gauge 1" HeaderTitle="Pressure Gauge 1"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="60px" Wrap="True" />
                            </iFg:BoundField>
                              <iFg:BoundField DataField="PG_2" HeaderText="Pressure Gauge 2" HeaderTitle="Pressure Gauge 2"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="60px" Wrap="True" />
                            </iFg:BoundField>
                              <iFg:BoundField DataField="LST_GNRTD_BY" HeaderText="Last Generated By" HeaderTitle="Last Generated By"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="70px" Wrap="True" />
                            </iFg:BoundField>
                             <iFg:DateField DataField="LST_GNRTD_DT" HeaderText="Last Generated Date" HeaderTitle="Last Generated Date" SortAscUrl=""
                                SortDescUrl="" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ReadOnly="true">
                                <iDate HelpText="" iCase="Upper" LeftPosition="0" OnClientTextChange="" TopPosition="0"
                                    ValidationGroup="" MaxLength="11" CssClass="txt" Width="110px">
                                    <DateIcon CssClass="dimg" Src="../Images/calendar.png" Cursor="" Height="" HSpace=""
                                        ImageAlign="AbsMiddle" IsVisible="True" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                                    <Validator CustomValidateEmptyText="False" Operator="LessThanEqual" Type="Date" IsRequired="false"
                                        LkpErrorMessage="Invalid Gate In Date. Click on the calendar icon for valid values"
                                        ReqErrorMessage="Event Date Required" Validate="True" RangeValidation="false"
                                        CompareValidation="false" ValidationGroup="divEquipmentDetail" />
                                </iDate>
                               <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="70px" Wrap="True" />
                            </iFg:DateField>
                             <%-- <iFg:BoundField DataField="LST_GNRTD_DT" HeaderText="Last Generated Date" HeaderTitle="Last Generated Date" DataFormatString="{0:dd-MMM-yyyy}"  HtmlEncode="false" 
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" iCase ="Upper"  >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="80px" Wrap="True" />
                            </iFg:BoundField>--%>
                              <iFg:BoundField DataField="LTST_RPRT_NO" HeaderText="Latest Report No" HeaderTitle="Latest Report No"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="80px" Wrap="True" />
                            </iFg:BoundField>                            
                              <iFg:BoundField DataField="RMRKS_VC" HeaderText="Remarks" HeaderTitle="Remarks"
                                IsEditable="False" SortAscUrl="" SortDescUrl="" ReadOnly="true" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="150px" Wrap="True" />
                            </iFg:BoundField>
                        </Columns>
                        <RowStyle CssClass="gitem" />
                        <HeaderStyle CssClass="ghdr" HorizontalAlign="Left" />
                        <FooterStyle CssClass="gftr" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="gsitem" />
                        <AlternatingRowStyle CssClass="gaitem" />
                        <SearchIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="NotSet"
                            IsVisible="True" HSpace="" CssClass="" OnImgClick=""></SearchIcon>
                        <SearchIcon CssClass="" Cursor="" Height="" HSpace="" ImageAlign="NotSet" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <PagerStyle CssClass="gpage" Height="8px" Font-Names="Arial" HorizontalAlign="Center" />
                    </iFg:iFlexGrid>
                </td>
            </tr>
        </table>
    </div>
   
    </form>
</body>
</html>
