Imports System.IO
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.Collections.Generic
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports System.Data
Imports System.Math
Imports iInterchange.Framework.Common

Partial Class Reports_ReportViewer
    Inherits Pagebase

    Private rptDatasources As Data.DataSet
    Public rsclientprintparams As String
    Public rsPageHeight As String
    Public rsPageWidth As String
    Public rsLeftMargin As String
    Public rsRightMargin As String
    Public rsTopMargin As String
    Public rsBottomMargin As String
    Private m_streams As IList(Of Stream)
    Dim count As Integer = 0
    Private rptName As String
    Private strDBKey As String
    Private strLogoURL As String = ConfigurationSettings.AppSettings("LogoURL")

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            Dim sbrrsclientprintparams As New StringBuilder
            sbrrsclientprintparams.Append("""")
            sbrrsclientprintparams.Append(Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.IndexOf("?")))
            sbrrsclientprintparams.Append(""",""")
            sbrrsclientprintparams.Append(Request.Url.Query.Remove(0, 1))
            sbrrsclientprintparams.Append("&type=print"",""")
            sbrrsclientprintparams.Append(GetQueryString("reportpath"))
            sbrrsclientprintparams.Append("""")
            rsclientprintparams = sbrrsclientprintparams.ToString
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub
    Protected Property SomeValue() As String
        Get
            Return m_SomeValue
        End Get
        Set(value As String)
            m_SomeValue = Value
        End Set
    End Property
    Private m_SomeValue As String

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'CommonWeb.pub_AttachDescMaxlength(txtBody)
            If Not Page.IsPostBack Then
                Dim strCustomer As String = String.Empty
                pvt_SetChangesMade()
                Dim strReportName As String = GetQueryString("reportname")
                Dim strReportpath As String = GetQueryString("reportpath")
                Dim strReportsId As String = GetQueryString("ReportId")
                rsPageHeight = GetQueryString("h")
                If String.IsNullOrEmpty(rsPageHeight) = True OrElse rsPageHeight = "undefined" Then
                    rsPageHeight = "279.4"
                End If
                rsPageWidth = GetQueryString("w")
                If String.IsNullOrEmpty(rsPageWidth) = True OrElse rsPageWidth = "undefined" Then
                    'rsPageWidth = "215.9"
                    rsPageWidth = "225.9"
                End If
                rsLeftMargin = GetQueryString("l")
                If String.IsNullOrEmpty(rsLeftMargin) = True OrElse rsLeftMargin = "undefined" Then
                    rsLeftMargin = "7.7"
                End If
                rsRightMargin = GetQueryString("r")
                If String.IsNullOrEmpty(rsRightMargin) = True OrElse rsRightMargin = "undefined" Then
                    rsRightMargin = "7.7"
                End If
                rsTopMargin = GetQueryString("t")
                If String.IsNullOrEmpty(rsTopMargin) = True OrElse rsTopMargin = "undefined" Then
                    rsTopMargin = "7.7"
                End If
                rsBottomMargin = GetQueryString("b")
                If String.IsNullOrEmpty(rsBottomMargin) = True OrElse rsBottomMargin = "undefined" Then
                    rsBottomMargin = "7.7"
                    '  rsBottomMargin = "12.7"
                End If
                'DirectCast(DirectCast(DocViewer, Microsoft.Reporting.WebForms.ReportViewer).Controls(1), System.Web.UI.Control).Controls(7).Visible = False

                If strReportName <> "" Then
                    Dim paramcoll As String
                    Dim objReportViewer As ReportViewerDataSource
                    Dim objCommonData As New CommonData
                    Dim intDepotID As Integer = CommonWeb.iInt(objCommonData.GetDepotID())
                    paramcoll = GetParameterCollections(strReportsId)

                    objReportViewer = New ReportViewerDataSource(strReportName, paramcoll, True, intDepotID, strReportsId)

                    Dim strCustomerSplit As String()
                    Dim strCustomerSel As String = String.Empty
                    Dim ObjCommon As New CommonUI
                    strCustomerSel = ObjCommon.pub_GetParameter("Customer", paramcoll)
                    strCustomerSplit = strCustomerSel.Split(",")

                    If strCustomerSplit.length = 1 Then
                        strCustomer = strCustomerSplit(0)
                    End If

                    rptDatasources = objReportViewer.pub_GetDatasource

                    pub_CacheData(strReportName, rptDatasources)

                    ShowReport(strReportpath, strCustomer)
                End If

                Dim dmodevalue As String = Request.QueryString("type")
                'Dim dmodevalue As String = "aaa"

                If Not dmodevalue Is Nothing Then
                    If dmodevalue.Length > 0 Then
                        If dmodevalue.IndexOf(",") > 0 Then
                            dmodevalue = dmodevalue.Remove(dmodevalue.IndexOf(","), dmodevalue.Length - dmodevalue.IndexOf(","))
                        End If
                    End If
                Else
                    dmodevalue = Request.QueryString("type")
                End If

                If Not Request.QueryString("type") Is Nothing And dmodevalue = "email" Then
                    Dim dtEmail As New DataTable
                    Dim objCommonUI As New CommonUI
                    If strCustomer <> String.Empty Then
                        Dim strEmailID As String = String.Empty
                        dtEmail = objCommonUI.pub_GetCustomerDetail(CInt(strCustomer.Replace("'", ""))).Tables(CommonUIData._V_SERVICE_PARTNER)
                        If Not IsDBNull(dtEmail.Rows(0).Item(CommonUIData.RPRTNG_EML_ID)) Then
                            strEmailID = (dtEmail.Rows(0).Item(BulkEmailData.RPRTNG_EML_ID)).ToString
                        ElseIf Not IsDBNull(dtEmail.Rows(0).Item(CommonUIData.INVCNG_EML_ID)) Then
                            strEmailID = (dtEmail.Rows(0).Item(BulkEmailData.INVCNG_EML_ID)).ToString
                        ElseIf Not IsDBNull(dtEmail.Rows(0).Item(CommonUIData.RPR_TCH_EML_ID)) Then
                            strEmailID = (dtEmail.Rows(0).Item(BulkEmailData.RPR_TCH_EML_ID)).ToString
                        End If
                        txtTo.Text = strEmailID
                    End If
                    pnlDocView.Visible = False
                    pnlPrint.Visible = False
                    pnlEmail.Visible = True
                    txtBody.Text = ""
                    Dim objCommondata As New CommonData
                    Dim strUserName As String = objCommondata.GetCurrentUserName()
                    Dim sbrBody As New StringBuilder
                    Dim strEmlSgn As String = String.Concat(vbCrLf, vbCrLf, strUserName)
                    'sbrBody.Append("<br/>")
                    'sbrBody.Append("<br/>")
                    'sbrBody.Append("<b>")
                    sbrBody.Append(strEmlSgn)
                    'sbrBody.Append("</b>")
                    txtBody.Text = sbrBody.ToString()
                    ProcessCreateEmail(strReportName)
                    Exit Sub
                Else
                    pnlDocView.Visible = True
                    pnlEmail.Visible = False
                    pnlPrint.Visible = False
                End If
                If Not Request.QueryString("type") Is Nothing And dmodevalue = "print" Then
                    pnlPrint.Visible = True
                    pnlDocView.Visible = False
                    pnlEmail.Visible = False

                    Dim warnings As Warning()
                    Dim streamids As String()
                    Dim mimeType As String
                    Dim encoding As String
                    Dim filenameExtension As String

                    Dim bytes As Byte() = DocViewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, filenameExtension, streamids, _
                     warnings)

                    Dim filename As String = Guid.NewGuid().ToString() & ".pdf"

                    Dim Path As String = Server.MapPath("~/Upload/")

                    Using fs As New FileStream(Path & filename, FileMode.Create)
                        fs.Write(bytes, 0, bytes.Length)
                    End Using
                    SomeValue = "/iTankDepoui/Upload/" + HttpUtility.HtmlDecode(filename.ToString)
                    ' ClientScript.RegisterClientScriptBlock(GetType(String), "redirect6", " $().ready(function () {PrintDocument();});", True)
                    Exit Sub
                End If
                If Not Request.QueryString("type") Is Nothing Then
                    Export(strReportName, DocViewer.LocalReport, "Image")
                    Dim dataToRead As Long
                    Dim iStream As Stream = m_streams(0)
                    Dim buffer(10000) As Byte
                    Dim length As Integer
                    dataToRead = iStream.Length
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-Disposition", "attachment; filename=jk")
                    If dataToRead = 0 Then
                        Session(strReportName + "_psrc") = 0
                    End If
                    While dataToRead > 0
                        If Response.IsClientConnected Then
                            length = iStream.Read(buffer, 0, 10000)
                            Response.OutputStream.Write(buffer, 0, length)
                            Response.Flush()
                            ReDim buffer(10000) ' Clear the buffer
                            dataToRead = dataToRead - length
                        Else
                            'prevent infinite loop if user disconnects
                            dataToRead = -1
                        End If
                    End While
                    Response.End()
                End If
            End If
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        Try
            CommonWeb.pub_AttachDescMaxlength(txtBody, True)
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub ProcessCreateEmail(ByVal bv_rptName As String)
        Try
            Export(bv_rptName, DocViewer.LocalReport, "PDF")
            Dim dataToRead As Long
            Dim iStream As Stream = m_streams(0)
            Dim buffer(10000) As Byte
            Dim length As Integer
            dataToRead = iStream.Length
            If dataToRead = 0 Then
                Session(bv_rptName + "_psrc") = 0
            End If
            Dim strFileName As String
            strFileName = String.Concat(GenerateFileName(), ".pdf")
            Dim fsStream As FileStream = File.OpenWrite(strFileName)
            While dataToRead > 0
                If Response.IsClientConnected Then
                    length = iStream.Read(buffer, 0, 10000)
                    fsStream.Write(buffer, 0, length)
                    ReDim buffer(10000) ' Clear the buffer
                    dataToRead = dataToRead - length
                Else
                    'prevent infinite loop if user disconnects
                    dataToRead = -1
                End If
            End While
            fsStream.Close()
            hdnattachfile.Value = strFileName
            lnkAttachment.InnerText = Mid(strFileName, strFileName.LastIndexOf("\") + 2)
            txtSubject.Text = bv_rptName
            lnkAttachment.Attributes.Add("href", String.Concat("../download.ashx?FL_NM=", lnkAttachment.InnerText))
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try


    End Sub

    Private Function GenerateFileName() As String
        Try
            Dim strFilename As String

            strFilename = ConfigurationManager.AppSettings("PrintDocuments")
            strFilename = String.Concat(strFilename, GetQueryString("reportname").Replace(" ", ""))
            strFilename = String.Concat(strFilename, Now.ToString("ddMMyyhhmmss"))

            Return strFilename
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex.Message)
            Return Nothing
        End Try

    End Function

    Private Function CreateStream(ByVal name As String, _
         ByVal fileNameExtension As String, _
         ByVal encoding As Encoding, ByVal mimeType As String, _
         ByVal willSeek As Boolean) As Stream
        Try
            Dim stream As Stream = _
           New FileStream(String.Concat(Server.MapPath(ConfigurationManager.AppSettings("UploadDocPath")), _
            name, "_", Now.ToString("ddMMyyhhmmss"), ".", fileNameExtension), FileMode.Create)
            m_streams.Add(stream)
            Return stream
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub ShowReport(ByVal bv_strReportpath As String, ByRef bv_strCustomer As String)
        Try
            Dim objCommon As New CommonData()
            With DocViewer
                .LocalReport.EnableExternalImages = True
                .ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
                .LocalReport.DataSources.Clear()

                For Each ky As Data.DataTable In rptDatasources.Tables
                    .LocalReport.DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource(ky.TableName, ky))
                Next
                .LocalReport.ReportPath = bv_strReportpath
                Dim dtFields As DataTable = pub_RetrieveData("parameters")
                Dim strFields As String = ""
                Dim strFromDate As String = ""
                Dim strToDate As String = ""
                If Not dtFields Is Nothing Then
                    Dim drPeriodFrom() As DataRow = dtFields.Select(String.Concat(CommonUIData.PRMTR_NAM, "='period from' AND PRMTR_VAL IS NOT NULL"))
                    Dim drInFrom() As DataRow = Nothing
                    Dim drInTo() As DataRow = Nothing
                    If drPeriodFrom.Length > 0 Then
                        strFromDate = drPeriodFrom(0).Item("prmtr_val")
                    End If
                    Dim drPeriodTo() As DataRow = dtFields.Select(String.Concat(CommonUIData.PRMTR_NAM, "='period to' AND PRMTR_VAL IS NOT NULL"))
                    If drPeriodTo.Length > 0 Then
                        strToDate = drPeriodTo(0).Item("prmtr_val")
                    End If
                    If drPeriodFrom.Length = 0 AndAlso drPeriodTo.Length = 0 Then
                        drInFrom = dtFields.Select(String.Concat(CommonUIData.PRMTR_NAM, "='in date from' AND PRMTR_VAL IS NOT NULL"))
                        If drInFrom.Length > 0 Then
                            strFromDate = drInFrom(0).Item("prmtr_val")
                        End If
                        drInTo = dtFields.Select(String.Concat(CommonUIData.PRMTR_NAM, "='in date to' AND PRMTR_VAL IS NOT NULL"))
                        If drInTo.Length > 0 Then
                            strToDate = drInTo(0).Item("prmtr_val")
                        End If
                    End If
                    If drPeriodFrom.Length = 0 AndAlso drPeriodTo.Length = 0 AndAlso drInFrom.Length = 0 AndAlso drInTo.Length = 0 Then
                        Dim drEstimateFrom() As DataRow = dtFields.Select(String.Concat(CommonUIData.PRMTR_NAM, "='estimate date from' AND PRMTR_VAL IS NOT NULL"))
                        If drEstimateFrom.Length > 0 Then
                            strFromDate = drEstimateFrom(0).Item("prmtr_val")
                        End If
                        Dim drEstimateTo() As DataRow = dtFields.Select(String.Concat(CommonUIData.PRMTR_NAM, "='estimate date to' AND PRMTR_VAL IS NOT NULL"))
                        If drEstimateTo.Length > 0 Then
                            strToDate = drEstimateTo(0).Item("prmtr_val")
                        End If
                    End If
                    Dim drCustomer() As DataRow
                    drCustomer = dtFields.Select(String.Concat(CommonUIData.PRMTR_NAM, "='Customer' AND PRMTR_VAL IS NOT NULL"))
                    If drCustomer.Length > 0 AndAlso Not (CStr(drCustomer(0).Item("prmtr_val")).Contains(CChar(","))) Then
                        bv_strCustomer = CStr(drCustomer(0).Item("prmtr_val"))
                    Else
                        drCustomer = dtFields.Select(String.Concat(CommonUIData.PRMTR_NAM, "='Invoicing To' AND PRMTR_VAL IS NOT NULL"))
                        If drCustomer.Length > 0 AndAlso Not (CStr(drCustomer(0).Item("prmtr_val")).Contains(CChar(","))) Then
                            bv_strCustomer = CStr(drCustomer(0).Item("prmtr_val"))
                        End If
                    End If
                    If strFromDate <> "" AndAlso strToDate = "" Then
                        strToDate = Now.ToString("dd-MMM-yyyy").ToUpper
                    End If
                End If
                Dim objCommondata As New CommonData
                Dim strUserName As String = objCommondata.GetCurrentUserName()
                'Dim prmlogo As New ReportParameter("logo", String.Concat(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), HttpRuntime.AppDomainAppVirtualPath, "/upload/photo/", objCommondata.GetCustomerLogo()))
                Dim prmUserName As New ReportParameter("username", strUserName)
                Dim prmFromDate As New ReportParameter("fromdate", strFromDate)
                Dim prmToDate As New ReportParameter("todate", strToDate)
                If (strFromDate <> "" And strToDate <> "") Then
                    .LocalReport.SetParameters(New ReportParameter() {prmUserName, prmFromDate, prmToDate})
                ElseIf (strFromDate = "" And strToDate <> "") Then
                    .LocalReport.SetParameters(New ReportParameter() {prmUserName, prmToDate})
                Else
                    .LocalReport.SetParameters(New ReportParameter() {prmUserName})
                End If
                Dim bln022Key As Boolean = False
                Dim str022Value As String = objCommon.GetConfigSetting("022", bln022Key)
                Dim bln007Key As Boolean = False
                Dim str007Value As String = objCommon.GetConfigSetting("007", bln007Key)
                Dim strReportName As String = GetQueryString("reportname")
                If GetQueryString("ReportId") <> 162 Then

                    If strReportName = "Revenue - Invoice Register" OrElse strReportName = "Gate Moves" Then
                        If str022Value.ToString.ToUpper = "TRUE" Then
                            Dim Rental As New ReportParameter("Rental", True)
                            .LocalReport.SetParameters(New ReportParameter() {Rental})
                        Else
                            Dim Rental As New ReportParameter("Rental", False)
                            .LocalReport.SetParameters(New ReportParameter() {Rental})
                        End If
                    End If
                    If strReportName = "Status" OrElse strReportName = "Inventory" OrElse strReportName = "Gate Moves" Then
                        If bln007Key Then
                            Dim rpEirNo As New ReportParameter("EirNo", str007Value)
                            .LocalReport.SetParameters(New ReportParameter() {rpEirNo})
                        End If
                    End If
                End If

                'Added for MultiDepot
                Dim bln070Key As Boolean = False
                Dim str070Value As String = objCommon.GetConfigSetting("070", bln070Key)
                If str070Value Then
                    Dim MultiDepot As New ReportParameter("MultiDepot", True)
                    .LocalReport.SetParameters(New ReportParameter() {MultiDepot})
                Else
                    Dim MultiDepot As New ReportParameter("MultiDepot", False)
                    .LocalReport.SetParameters(New ReportParameter() {MultiDepot})
                End If

                .LocalReport.Refresh()
            End With
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

    Private Function GetParameterCollections(ByVal bv_strReportsId As String) As String
        Try
            Dim strParamCollection As New StringBuilder
            Dim htbParameters As String = ""
            Dim dtParameters As DataTable = pub_RetrieveData("parameters" + bv_strReportsId)
            For Each drparam As DataRow In dtParameters.Rows
                If strParamCollection.ToString <> "" Then
                    strParamCollection.Append("&")
                End If
                strParamCollection.Append(String.Concat(drparam.Item(CommonUIData.PRMTR_NAM), "=", drparam.Item("prmtr_val")))
            Next
            Return strParamCollection.ToString
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub Export(ByVal bv_rptName As String, ByVal report As LocalReport, ByVal format As String)
        Try
            Dim s As String = Session(bv_rptName + "_psrc")
            If s = 0 OrElse s Is Nothing Then
                s = 1
            End If
            Dim deviceInfo As String = String.Empty

            'Convert Millimeter to INCH
            pvt_fnConvertMM_to_INCH(rsPageWidth, rsPageHeight, rsTopMargin, rsBottomMargin, rsLeftMargin, rsRightMargin)

            If format = "PDF" Then

                deviceInfo = _
                                      "<DeviceInfo>" + _
                                      "  <OutputFormat>PDF</OutputFormat>" + _
                                      "  <PageWidth>11.5in</PageWidth>" + _
                                      "  <PageHeight>8.5in</PageHeight>" + _
                                      "</DeviceInfo>"
            ElseIf format = "EXCEL" Then
                deviceInfo = "<DeviceInfo> <SimplePageHeaders>False</SimplePageHeaders></DeviceInfo>"
            Else
                deviceInfo = _
                          "<DeviceInfo>" + _
                          "  <OutputFormat>EMF</OutputFormat>" + _
                          "  <PageWidth>" + rsPageWidth + "in</PageWidth>" + _
                          "  <PageHeight>" + rsPageHeight + "in</PageHeight>" + _
                          "  <MarginTop>" + rsTopMargin + "in</MarginTop>" + _
                          "  <MarginLeft>" + rsLeftMargin + "in</MarginLeft>" + _
                          "  <MarginRight>" + rsRightMargin + "in</MarginRight>" + _
                          "  <MarginBottom>" + rsBottomMargin + "in</MarginBottom>" + _
                          "  <StartPage>" + s + "</StartPage>" + _
                          "  <EndPage>" + s + "</EndPage>" + _
                          "</DeviceInfo>"
            End If

            Dim warnings() As Microsoft.Reporting.WebForms.Warning = Nothing
            m_streams = New List(Of Stream)()
            report.Render(format, deviceInfo, AddressOf CreateStream, warnings)
            Session(bv_rptName + "_psrc") = s + 1

            Dim stream As Stream
            For Each stream In m_streams
                stream.Position = 0
            Next

        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub

    Protected Sub DocViewer_Drillthrough(ByVal sender As Object, ByVal e As Microsoft.Reporting.WebForms.DrillthroughEventArgs) Handles DocViewer.Drillthrough
        'Try
        '    Dim strRefField As String = Nothing
        '    Dim strRefFieldValue As String = Nothing
        '    Dim hsRefParam As New Hashtable
        '    Dim strFilter As String = Nothing
        '    Dim strTableName As String = String.Empty
        '    Dim intCount As Integer = 0
        '    Dim drillReportDataSet As Data.DataSet = pub_RetrieveData(GetQueryString("reportname"))
        '    Dim DrillThroughValues As ReportParameterInfoCollection = e.Report.GetParameters()

        '    For Each d As ReportParameterInfo In DrillThroughValues
        '        If d.Name.ToString().Trim() <> "TableName" Then
        '            hsRefParam.Add(d.Name.ToString().Trim(), d.Values(0).ToString().Trim())
        '        Else
        '            strTableName = d.Values(0).ToString().Trim()
        '        End If
        '    Next

        '    Dim drillthroughlocalreport As LocalReport = DirectCast(e.Report, LocalReport)

        '    If drillthroughlocalreport.ReportPath.Contains(e.ReportPath) Then
        '        With DocViewer
        '            Dim dt As DataTable
        '            dt = drillReportDataSet.Tables(strTableName)
        '            Dim dv As DataView
        '            dv = dt.DefaultView

        '            For intCount = 0 To hsRefParam.Count - 1
        '                strRefField = hsRefParam.Keys(intCount).ToString
        '                strRefFieldValue = hsRefParam.Values(intCount).ToString
        '                If strFilter <> "" Then
        '                    strFilter = String.Concat(strFilter, " and ", strRefField, "='", strRefFieldValue, "'")
        '                Else
        '                    strFilter = String.Concat(strRefField, "='", strRefFieldValue, "'")
        '                End If
        '            Next

        '            dv.RowFilter = strFilter

        '            Dim drillReportDS As New ReportDataSource(strTableName, dv.ToTable)
        '            drillthroughlocalreport.DataSources.Clear()
        '            drillthroughlocalreport.DataSources.Add(drillReportDS)
        '            drillthroughlocalreport.Refresh()

        '        End With
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Public Function GetRSCabURL() As String
        Return ConfigurationManager.AppSettings("RSCabURL")
    End Function

    Private Sub pvt_fnConvertMM_to_INCH(ByVal str_rsPageWidth As String, ByVal str_rsPageHeight As String, ByVal str_rsTopMargin As String, ByVal str_rsBottomMargin As String, ByVal str_rsLeftMargin As String, ByVal str_rsRightMargin As String)
        rsPageWidth = Round(str_rsPageWidth * 0.0293700787, 1)
        rsPageHeight = Round(str_rsPageHeight * 0.0393700787, 1)
        rsTopMargin = Round(str_rsTopMargin * 0.0393700787, 1)
        rsBottomMargin = Round(str_rsBottomMargin * 0.0393700787, 1)
        rsLeftMargin = Round(str_rsLeftMargin * 0.0393700787, 1)
        rsRightMargin = Round(str_rsRightMargin * 0.0393700787, 1)
    End Sub

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Reports/ReportDialog.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/ModalWindow.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "CreateAlert"
                CreateAlert(e.GetCallbackValue("EmailTo"), e.GetCallbackValue("EmailSubject"), _
                         e.GetCallbackValue("EmailBody"), e.GetCallbackValue("AttachFile"), _
                         e.GetCallbackValue("TemplateID"))
        End Select
    End Sub
#End Region

#Region "CreateAlert"
    Private Sub CreateAlert(ByVal bv_strSendTo As String, ByVal bv_strSubject As String, _
                            ByVal bv_strBody As String, ByVal bv_strAttachFile As String, _
                             ByVal bv_strTemplateID As String)
        Try
            Dim objCommonUI As New CommonUI
            Dim objEmail As New iEmailHandler
            'Dim strReferenceNo As String
            Dim objCommon As New CommonData

            Dim strBody As String()
            strBody = bv_strBody.Split(vbCrLf)
            If strBody.Length = 0 Then
                strBody(0) = bv_strBody
            End If

            'Adding Foot note
            Dim sbrFootNote As New StringBuilder
            sbrFootNote.Append("<DIV style=""font-family:verdana;font-size:8pt"">")
            sbrFootNote.Append("<DIV>")
            For Each strBodyemail As String In strBody
                sbrFootNote.Append("<br/>")
                sbrFootNote.Append(strBodyemail)
                sbrFootNote.Append("<br/>")
            Next
            sbrFootNote.Append("</DIV>")


            bv_strAttachFile = String.Concat(ConfigurationManager.AppSettings.Get("PrintDocuments"), bv_strAttachFile)
            'objEmail.pub_Send_Email(bv_strSendTo, bv_strSubject, sbrFootNote.ToString(), bv_strAttachFile)
            pub_Send_Email(bv_strSendTo, bv_strSubject, sbrFootNote.ToString(), bv_strAttachFile)
            'Dim wfdata As String = Server.UrlDecode(bv_strWFDATA)
            'strReferenceNo = objCommonUI.pub_CreateAlert(bv_strTransactionNo, bv_strTemplateID, bv_strSendTo, bv_strSubject, bv_strBody, bv_strAttachFile, bv_strWFDATA)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

    '#Region "Sending Email using chilkat component with single file attachment"

    '    Public Sub pub_Send_ReportEmail(ByVal bv_strEmailIds As String, ByVal bv_strSubject As String, _
    '                                ByVal bv_strBody As String, ByVal bv_strAttachmentPath As String)

    '        Dim objChilkat As New Chilkat.MailMan
    '        Try
    '            Dim Status As Boolean
    '            objChilkat.UnlockComponent("UiinterchMAILQ_FmPBEfzL8SkF")
    '            objChilkat.SmtpHost = ConfigurationManager.AppSettings("SmtpMailServer")
    '            Dim email As New Chilkat.Email
    '            email.From = ConfigurationManager.AppSettings("FromEmail")
    '            If ConfigurationManager.AppSettings("SmtpAuthEnabled") = "true" Then
    '                objChilkat.SmtpUsername = ConfigurationManager.AppSettings("SmtpUserName")
    '                objChilkat.SmtpPassword = ConfigurationManager.AppSettings("SmtpPassword")
    '            End If
    '            objChilkat.SmtpPort = ConfigurationManager.AppSettings("SmtpPortNo")
    '            Dim strEmailIds As String()

    '            Dim macTo As New System.Net.Mail.MailAddressCollection
    '            strEmailIds = bv_strEmailIds.Split(CChar(","))

    '            If strEmailIds.Length = 0 Then
    '                strEmailIds(0) = bv_strEmailIds
    '                email.AddTo(bv_strEmailIds, bv_strEmailIds)
    '            End If

    '            For Each strEmail As String In strEmailIds
    '                email.AddTo(strEmail, strEmail)
    '            Next

    '            email.Subject = bv_strSubject

    '            'Adding Foot note
    '            Dim sbrFootNote As New Text.StringBuilder
    '            sbrFootNote.Append("<DIV style=""font-family:Arial;font-size:10pt"">")
    '            sbrFootNote.Append("<DIV>")
    '            sbrFootNote.Append(bv_strBody)
    '            sbrFootNote.Append("</DIV>")
    '            sbrFootNote.Append("<BR/>")
    '            sbrFootNote.Append("<BR/>")
    '            sbrFootNote.Append("<HR/>")
    '            sbrFootNote.Append("<DIV>")
    '            sbrFootNote.Append("This message was sent to you by <B>")
    '            sbrFootNote.Append(ConfigurationManager.AppSettings("appnam"))
    '            sbrFootNote.Append("</B>.  ")
    '            sbrFootNote.Append("Please do not respond to this message as it is automatically generated and ")
    '            sbrFootNote.Append("is for information purposes only.")
    '            sbrFootNote.Append("</DIV>")
    '            sbrFootNote.Append("</DIV>")


    '            email.SetHtmlBody(sbrFootNote.ToString())

    '            email.AddFileAttachment(bv_strAttachmentPath)

    '            Status = objChilkat.SendEmail(email)

    '            If Status = False Then
    '                iErrorHandler.pub_WriteErrorLog("Chilkat", _
    '                        objChilkat.LastErrorText, "Mail was not sent .objChilkat.SendEmail method returns FALSE")
    '            End If
    '        Catch ex As Exception
    '            iErrorHandler.pub_WriteErrorLog("pub_Send_Email", _
    '                                    objChilkat.LastErrorText, ex.Message)
    '        End Try
    '    End Sub

    '#End Region

End Class