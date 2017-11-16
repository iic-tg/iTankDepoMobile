<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OperationsTurnOver.aspx.vb"
    Inherits="Widgets_OperationsTurnOver" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="setOperationTrendGraph();">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="2" cellspacing="2" class="tblstd">
            <tr>
                <td>
                    <label id="lblCustomercode" runat="server" class="lbl">
                        Customer</label>
                </td>
                <td>
                    <Inp:iLookup ID="lkpCustomerCode" runat="server" CssClass="lkp" DataKey="CSTMR_CD"
                        DoSearch="True" iCase="Upper" MaxLength="10" TabIndex="1" TableName="9" HelpText=""
                        ClientFilterFunction="applyDepoFilter" Width="70px">
                        <LookupColumns>
                            <Inp:LookupColumn ColumnName="CSTMR_ID" Hidden="True" />
                            <Inp:LookupColumn ColumnName="CSTMR_CD" ControlToBind="lkpCustomerCode" Hidden="False"
                                ColumnCaption="Code" />
                            <Inp:LookupColumn ColumnCaption="Name" ColumnName="CSTMR_NAM" Hidden="False" />
                        </LookupColumns>
                        <InfoIcon OffsetX="" OffsetY="" Width="" Height="" Cursor="" ImageAlign="AbsMiddle"
                            IsVisible="False" HSpace="" CssClass="mimg" OnImgClick=""></InfoIcon>
                        <LookupGrid CurrentPageIndex="0" PageSize="10" Width="300px" VerticalAlign="Top" />
                        <Validator CsvErrorMessage="" CustomValidateEmptyText="False" IsRequired="False"
                            Operator="Equal" Type="String" Validate="False" ValidationGroup="" LookupValidation="False" />
                        <AddNewIcon CssClass="aimg" Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle"
                            IsVisible="False" OffsetX="" OffsetY="" OnImgClick="" Width="" />
                        <LookupIcon Cursor="" Height="" HSpace="" ImageAlign="AbsMiddle" IsVisible="True"
                            OffsetX="" OffsetY="" OnImgClick="" Width="" />
                    </Inp:iLookup>
                </td>
                <td>
                    <a href="#" tabindex="2" id="lnkSearch" onclick="setOperationTrendGraph();return false;">
                        Set</a>
                </td>
                <td align="right">
                    <div id="divGraphLegend" style="display: none" align="right">
                        <table border="0" cellpadding="0" cellspacing="2" class="tblstd" align="right">
                            <tr>
                                <td>
                                    <div style="height: 10px; width: 10px; background-color: #4bb2c5">
                                    </div>
                                </td>
                                <td>
                                    <span>Gate In</span>
                                </td>
                                <td>
                                    <div style="height: 10px; width: 10px; background-color: #c5b47f">
                                    </div>
                                </td>
                                <td>
                                    <span>Repair Completion</span>
                                </td>
                                <td>
                                    <div style="height: 10px; width: 10px; background-color: #EAA228">
                                    </div>
                                </td>
                                <td>
                                    <span>Gate Out</span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMessage" runat="server" style="margin: 10px; font-style: italic; font-family: Arial;
        font-size: 8pt; display: none; width: 100%;">
        <Inp:iLabel ID="lblListRowCount" runat="server" Visible="true">No Records Found</Inp:iLabel>
    </div>
    <div id="divOperationTrend" style="margin-top: 3px; margin-left: 5px; width: 430px;
        height: 250px;">
    </div>
    <div style="display: none">
        <sp:SubmitPane ID="PageSubmitPane" runat="server" onClientSubmit="submitPage()" />
    </div>
    </form>
    <script class="include" type="text/javascript" src="../Script/UI/jqplot/excanvas.min.js"></script>
    <script class="include" type="text/javascript" src="../Script/UI/jqplot/jquery.jqplot.min.js"></script>
    <script type="text/javascript" src="../Script/UI/jqplot/shCore.min.js"></script>
    <script type="text/javascript" src="../Script/UI/jqplot/shBrushJScript.min.js"></script>
    <script type="text/javascript" src="../Script/UI/jqplot/shBrushXml.min.js"></script>
    <script class="include" type="text/javascript" src="../Script/UI/jqplot/jqplot.CanvasAxisLabelRenderer.min.js"></script>
    <script class="include" type="text/javascript" src="../Script/UI/jqplot/jqplot.DateAxisRenderer.min.js"></script>
    <script class="include" type="text/javascript" src="../Script/UI/jqplot/jqplot.canvasTextRenderer.min.js"></script>
    <script class="include" type="text/javascript" src="../Script/UI/jqplot/jqplot.enhancedLegendRenderer.min.js"></script>
    <script class="include" type="text/javascript" src="../Script/UI/jqplot/jqplot.cursor.min.js"></script>
    <script class="include" type="text/javascript" src="../Script/UI/jqplot/jqplot.highlighter.min.js"></script>
</body>
</html>
