Imports System.IO
Imports System.Drawing.Imaging
Imports System.Collections.Generic
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Imports System.Data
Imports iInterchange.Framework.Common


Partial Class Document_DocumentViewer
    Inherits Pagebase

#Region "Declarations"
    Private rptDatasources As Data.DataSet
    Public rsclientprintparams As String
    Dim objCommon As New CommonData()
    Private objMemoryStream As IList(Of Stream)
#End Region

#Region "Page_Init"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            Dim sbrrsclientDocumentparams As New StringBuilder
            sbrrsclientDocumentparams.Append("""")
            sbrrsclientDocumentparams.Append(Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.IndexOf("?")))
            sbrrsclientDocumentparams.Append(""",""")
            sbrrsclientDocumentparams.Append(Request.Url.Query.Remove(0, 1))
            sbrrsclientDocumentparams.Append("&type=Document"",""")
            sbrrsclientDocumentparams.Append(GetQueryString("DCMNT_NAM"))
            sbrrsclientDocumentparams.Append("""")
            rsclientprintparams = sbrrsclientDocumentparams.ToString
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                            ex)
        End Try
    End Sub
#End Region

#Region "GetRSCabURL"
    Public Function GetRSCabURL() As String
        Return "../download.ashx?FL_NM=RSClientPrint.cab"
    End Function
#End Region

#Region "Page_Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False AndAlso GetQueryString("callbacktype") Is Nothing Then
                Dim dsDocument As New CommonUIDataSet
                pvt_SetChangesMade()
                rvDocuments.HyperlinkTarget = "_blank"
                Dim intTemplateId As Integer = CInt(GetQueryString("TMPLT_ID"))
                Dim objDocument As New CommonUI
                Dim objTransporter As New Transporter
                Dim strTemplateFileName As String = String.Empty
                Dim strDocumentTemplateFormat As String = String.Empty
                Dim strDocumentName As String = String.Empty
                Dim intCustomerID As Integer
                Dim strEquipmentNo As String = String.Empty
                Dim strReportType As String = GetQueryString("type")
                Dim strInvoicetype As String = String.Empty
                Dim strInvoiceNo As String = String.Empty
                Dim strBillingType As String = String.Empty
                Dim strPageName As String = String.Empty
                Dim dtCustomer As New DataTable
                Dim blnCleaning As Boolean = False
                Dim blnRepairEstimate As Boolean = False
                Dim dtRepairEstimate As New DataTable
                Dim strcleaningMail As String = String.Empty
                Dim strCustomer As String = String.Empty
                Dim dsTransporter As New DataSet
                ''Estimate File Format
                If GetQueryString("EQPMNT_NO") <> Nothing Then
                    strEquipmentNo = GetQueryString("EQPMNT_NO").ToString
                End If

                If GetQueryString("INVC_TYP") <> Nothing Then
                    strInvoicetype = GetQueryString("INVC_TYP").ToString
                End If
                If GetQueryString("INVC_NO") <> Nothing Then
                    strInvoiceNo = GetQueryString("INVC_NO").ToString
                End If
                If GetQueryString("BILLING_TYPE") <> Nothing Then
                    strBillingType = GetQueryString("BILLING_TYPE").ToString
                End If
                If GetQueryString("PageName") <> Nothing Then
                    strPageName = GetQueryString("PageName").ToString
                End If
                If intTemplateId > 0 Then
                    Dim objReportViewer As ReportViewerDataSource
                    Dim paramcoll As String
                    If strReportType.ToLower <> "report" Then
                        If (strReportType.ToLower = "email" Or strReportType.ToLower = "noframe") And GetQueryString("direct") Is Nothing Then
                            intTemplateId = RetrieveData("TMPLT_ID")
                        Else
                            CacheData("TMPLT_ID", intTemplateId)
                        End If
                        If intTemplateId = 0 Then
                            intTemplateId = CInt(GetQueryString("TMPLT_ID"))
                        End If
                        dsDocument = objDocument.pub_GetDocumentTemplateByTemplateID(intTemplateId)


                        hdnTemplateId.Value = intTemplateId

                        strDocumentName = GetQueryString("DCMNT_NAM")
                        hdnTransactionNo.Value = GetQueryString("TRNSCTN_NO")

                        strTemplateFileName = dsDocument.Tables(CommonUIData._DOCUMENT_TEMPLATE).Rows(0).Item(CommonUIData.TMPLT_FL_NAM).ToString
                        strDocumentTemplateFormat = dsDocument.Tables(CommonUIData._DOCUMENT_TEMPLATE).Rows(0).Item(CommonUIData.DCMNT_TMPLT_FRMT).ToString
                        paramcoll = GetParameterCollections()
                    End If
                    If strReportType.ToLower = "report" Then
                        strDocumentName = GetQueryString("re").ToString
                        strTemplateFileName = GetQueryString("re").ToString
                        Dim dtParmeters As DataTable
                        dtParmeters = CType(RetrieveData("reportparamlist"), DataTable)
                        'objReportViewer = New ReportViewerDataSource(strDocumentName, Nothing, dtParmeters)
                    Else
                        objReportViewer = New ReportViewerDataSource(strDocumentName, Server.UrlDecode(paramcoll), strPageName, strCustomer)
                    End If
                    rptDatasources = objReportViewer.pub_GetDatasource

                    CacheData(strDocumentName, rptDatasources)

                    If String.IsNullOrEmpty(GetQueryString("ref")) = False AndAlso String.IsNullOrEmpty(GetQueryString("type")) = True Then
                        CacheData(strDocumentName + "_psrc", 0)
                    End If

                    Dim objcommon As New CommonData()

                    ShowReport(strTemplateFileName, strReportType, strDocumentTemplateFormat)
                End If

                'For Portrait and landscape
                dsDocument = objDocument.pub_GetDocumentTemplateByTemplateID(intTemplateId)
                strDocumentTemplateFormat = dsDocument.Tables(CommonUIData._DOCUMENT_TEMPLATE).Rows(0).Item(CommonUIData.DCMNT_TMPLT_FRMT).ToString

                If strReportType.ToLower = "email" Or strReportType.ToLower = "noframe" Then
                    pnlDocView.Visible = False
                    pnlEmail.Visible = True
                    pnlExport.Visible = False
                    Dim bln072Key As Boolean = False
                    Dim str072Value As String = objCommon.GetConfigSetting("072", bln072Key)
                    strDocumentName = GetQueryString("DCMNT_NAM")
                    Dim dtEmail As New DataTable
                    Dim dtTransporterRequestEmail As New DataTable
                    Dim objCommonUI As New CommonUI
                    If strCustomer <> String.Empty Then
                        Dim strEmailID As String = String.Empty
                        dtEmail = objCommonUI.pub_GetCustomerDetail(CInt(strCustomer.Replace("'", ""))).Tables(CommonUIData._V_SERVICE_PARTNER)
                        If Not IsDBNull(dtEmail.Rows(0).Item(CommonUIData.RPRTNG_EML_ID)) AndAlso intTemplateId <> 22 Then
                            'For Transportation Request page alone if Config setting keyvalue 072 is true  load Email ID from TRANSPORTER master table other wise load asusual
                            If strDocumentName.ToString.ToUpper.Trim = "TRANSPORTATION JOB ORDER" And GetQueryString("dcmnt_id") = 32 And str072Value.ToUpper.Trim = "TRUE" Then
                                If Not rptDatasources Is Nothing And rptDatasources.Tables(TransportationData._V_TRANSPORTATION).Rows.Count <> 0 Then
                                    Dim depoID As Int64 = rptDatasources.Tables(TransportationData._V_TRANSPORTATION).Rows(0)(CommonUIData.DPT_ID)
                                    Dim transporterID As Int64 = rptDatasources.Tables(TransportationData._V_TRANSPORTATION).Rows(0)(CommonUIData.TRNSPRTR_ID)
                                    dsTransporter = objTransporter.pub_GetTransporterDetailByID(transporterID, depoID)
                                    dtTransporterRequestEmail = dsTransporter.Tables(TransporterData._TRANSPORTER)
                                    If dtTransporterRequestEmail.Rows.Count > 0 Then
                                        strEmailID = (dtTransporterRequestEmail.Rows(0).Item(TransporterData.EML_ID)).ToString
                                    End If
                                End If
                            Else
                                strEmailID = (dtEmail.Rows(0).Item(BulkEmailData.RPRTNG_EML_ID)).ToString
                            End If

                        ElseIf Not IsDBNull(dtEmail.Rows(0).Item(CommonUIData.INVCNG_EML_ID)) AndAlso intTemplateId <> 22 Then
                            strEmailID = (dtEmail.Rows(0).Item(BulkEmailData.INVCNG_EML_ID)).ToString
                        ElseIf Not IsDBNull(dtEmail.Rows(0).Item(CommonUIData.RPR_TCH_EML_ID)) AndAlso intTemplateId = 22 Then
                            strEmailID = (dtEmail.Rows(0).Item(BulkEmailData.RPR_TCH_EML_ID)).ToString
                        End If
                        txtTo.Text = strEmailID
                    End If
                    Dim objCommondata As New CommonData
                    Dim strUserName As String = objCommondata.GetCurrentUserName()
                    Dim sbrBody As New StringBuilder
                    Dim strEmlSgn As String = String.Concat(vbCrLf, vbCrLf, strUserName)
                    sbrBody.Append(strEmlSgn)
                    txtBody.Text = sbrBody.ToString()
                    ProcessCreateEmail(strDocumentName, strEquipmentNo, strDocumentTemplateFormat)
                ElseIf strReportType.ToLower = "export" Then
                    pnlDocView.Visible = False
                    pnlEmail.Visible = False
                    pnlExport.Visible = True
                    CreateExcelDocument(strDocumentName, GetQueryString("TRNSCTN_CODE"), GetQueryString("TRNSCTN_DSCRPTN"), strReportType, strInvoicetype, strInvoiceNo, strDocumentTemplateFormat)
                    'ProcessCreatePdf(strDocumentName, strInvoiceNo, strInvoicetype)
                ElseIf strReportType.ToLower = "createpdf" Then
                    pnlDocView.Visible = False
                    pnlEmail.Visible = False
                    pnlExport.Visible = False
                    ProcessCreatePdf(strDocumentName, strInvoiceNo, strInvoicetype)
                ElseIf strReportType.ToLower = "word" Then
                    'Document for Word
                    pnlDocView.Visible = False
                    pnlEmail.Visible = False
                    pnlExport.Visible = True
                    CreateExcelDocument(strDocumentName, GetQueryString("TRNSCTN_CODE"), GetQueryString("TRNSCTN_DSCRPTN"), strReportType, strInvoicetype, strInvoiceNo, strDocumentTemplateFormat)
                    ProcessCreatePdf(strDocumentName, strInvoiceNo, strInvoicetype)
                ElseIf strReportType.ToUpper = "ALL" Then
                    pnlDocView.Visible = False
                    pnlEmail.Visible = False
                    pnlExport.Visible = False
                    ProcessCreatePdf(strDocumentName, strInvoiceNo, strBillingType)
                    CreateExcelDocument(strDocumentName, GetQueryString("TRNSCTN_CODE"), GetQueryString("TRNSCTN_DSCRPTN"), "export", strBillingType, strInvoiceNo, strDocumentTemplateFormat)
                    CreateExcelDocument(strDocumentName, GetQueryString("TRNSCTN_CODE"), GetQueryString("TRNSCTN_DSCRPTN"), "word", strBillingType, strInvoiceNo, strDocumentTemplateFormat)
                Else
                    pnlDocView.Visible = True
                    pnlEmail.Visible = False
                    pnlExport.Visible = False
                End If

            End If
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                            String.Concat(ex, " - Inner Exception : ", ex.InnerException))
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        Try
            CommonWeb.pub_AttachDescMaxlength(txtBody, True)
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "ProcessCreateEmail"
    Private Sub ProcessCreateEmail(ByVal bv_strReportName As String, ByVal bv_strEquipmentNo As String, ByVal strDocumentTemplateFormat As String)
        Try
            'Export report as PDF
            Export(bv_strReportName, rvDocuments.LocalReport, "PDF", strDocumentTemplateFormat)

            'Create memory
            Dim dataToRead As Long
            Dim objStream As Stream = objMemoryStream(0)
            Dim buffer(10000) As Byte
            Dim length As Integer
            dataToRead = objStream.Length
            If dataToRead = 0 Then
                CacheData(bv_strReportName + "_psrc", 0)
            End If

            'Generate File Name
            Dim strFileName As String
            strFileName = String.Concat(GenerateFileName("UploadPhyPath"), ".pdf")
            'Create File
            Dim objFileStream As FileStream = File.OpenWrite(strFileName)
            While dataToRead > 0
                If Response.IsClientConnected Then
                    length = objStream.Read(buffer, 0, 10000)
                    objFileStream.Write(buffer, 0, length)
                    ReDim buffer(10000) ' Clear the buffer
                    dataToRead = dataToRead - length
                Else
                    'prevent infinite loop if user disconnects
                    dataToRead = -1
                End If
            End While
            objFileStream.Close()
            objStream.Close()

            hdnattachfile.Value = strFileName
            lnkAttachment.InnerText = Mid(strFileName, strFileName.LastIndexOf("\") + 2)
            lnkAttachment.Attributes.Add("href", String.Concat("../download.ashx?FL_NM=", lnkAttachment.InnerText))
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                            ex)
        End Try
    End Sub
#End Region

#Region "ProcessCreatePdf"
    Private Sub ProcessCreatePdf(ByVal bv_strReportName As String, ByVal bv_strInvoiceNo As String, ByVal bv_strBillingType As String)
        Try
            'Export report as PDF
            ExportInvoice(bv_strReportName, rvDocuments.LocalReport, "PDF", bv_strBillingType)

            'Create memory
            Dim dataToRead As Long
            Dim objStream As Stream = objMemoryStream(0)
            Dim buffer(10000) As Byte
            Dim length As Integer
            dataToRead = objStream.Length
            If dataToRead = 0 Then
                CacheData(bv_strReportName + "_psrc", 0)
            End If

            'Generate File Name
            Dim strFileName As String = String.Empty

            strFileName = String.Concat(GenerateInvoiceFileName("UploadPhyPath", bv_strInvoiceNo, bv_strBillingType), ".pdf")

            If File.Exists(strFileName) Then
                File.Delete(strFileName)
            End If

            'Create File
            Dim objFileStream As FileStream = File.OpenWrite(strFileName)
            While dataToRead > 0
                If Response.IsClientConnected Then
                    length = objStream.Read(buffer, 0, 10000)
                    objFileStream.Write(buffer, 0, length)
                    ReDim buffer(10000) ' Clear the buffer
                    dataToRead = dataToRead - length
                Else
                    'prevent infinite loop if user disconnects
                    dataToRead = -1
                End If
            End While
            objFileStream.Close()
            objStream.Close()

            'Delete the Draft Invoice after Final Invoice being Generated
            If Not bv_strBillingType.ToLower().Contains("draft") Then
                strFileName = strFileName.Replace(bv_strBillingType, String.Concat(bv_strBillingType, "Draft_")).Replace("Final", "Draft")
                If File.Exists(strFileName) Then
                    File.Delete(strFileName)
                End If
            End If

            'hdnattachfile.Value = strFileName
            'lnkAttachment.InnerText = Mid(strFileName, strFileName.LastIndexOf("\") + 2)
            'lnkAttachment.Attributes.Add("href", String.Concat("../download.ashx?FL_NM=", lnkAttachment.InnerText))
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                            ex)
        End Try
    End Sub
#End Region

#Region "CreateExcelDocument"
    Private Sub CreateExcelDocument(ByVal bv_strReportName As String, ByVal strTransactionCode As String, _
                                    ByVal strTransactionDesc As String, ByVal bv_strReportType As String, _
                                    ByVal bv_strBillingType As String, ByVal bv_strInvoiceNo As String, _
                                    ByVal bv_strDocumentTemplateFormat As String)
        Try
            'Export report as PDF
            If bv_strReportType = "export" Then
                Export(bv_strReportName, rvDocuments.LocalReport, "EXCEL", bv_strDocumentTemplateFormat)
            ElseIf bv_strReportType = "word" Then
                Export(bv_strReportName, rvDocuments.LocalReport, "WORD", bv_strDocumentTemplateFormat)
            End If

            'Create memory
            Dim dataToRead As Long
            Dim objStream As Stream = objMemoryStream(0)
            Dim buffer(10000) As Byte
            Dim length As Integer
            dataToRead = objStream.Length
            If dataToRead = 0 Then
                CacheData(bv_strReportName + "_psrc", 0)
            End If

            'Generate File Name
            Dim strFileName As String = String.Empty
            If bv_strReportType = "export" Then
                strFileName = String.Concat(GenerateInvoiceFileName("UploadPhyPath", bv_strInvoiceNo, bv_strBillingType), ".xls")
            ElseIf bv_strReportType = "word" Then
                strFileName = String.Concat(GenerateInvoiceFileName("UploadPhyPath", bv_strInvoiceNo, bv_strBillingType), ".doc")
            End If

            If File.Exists(strFileName) Then
                File.Delete(strFileName)
            End If

            'Create File
            Dim objFileStream As FileStream = File.OpenWrite(strFileName)
            While dataToRead > 0
                If Response.IsClientConnected Then
                    length = objStream.Read(buffer, 0, 10000)
                    objFileStream.Write(buffer, 0, length)
                    ReDim buffer(10000) ' Clear the buffer
                    dataToRead = dataToRead - length
                Else
                    'prevent infinite loop if user disconnects
                    dataToRead = -1
                End If
            End While
            objFileStream.Close()
            objStream.Close()

            'hdnattachfile.Value = strFileName
            'strFileName = Mid(strFileName, strFileName.LastIndexOf("\") + 2)
            'lblExport.Text = "The Tariff  " & "<B>" & strTransactionCode & "</B>" & " has been exported. "
            'lnkExport.Attributes.Add("href", String.Concat("../download.ashx?FL_NM=", strFileName))
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            'lblExportLink.Text = "Error occurred. Please try again."
        End Try
    End Sub
#End Region

#Region "GenerateInvoiceFileName"
    Private Function GenerateInvoiceFileName(ByVal bv_strFolderName As String, _
                                             ByVal bv_strInvoiceNo As String, _
                                             ByVal bv_strBillingType As String) As String
        Try
            Dim strFilename As String
            strFilename = CommonWeb.pub_GetConfigValue(bv_strFolderName)
            Dim objCommon As New CommonData

            If bv_strBillingType.ToUpper = "FINAL" Then
                strFilename = strFilename & "Invoice\Final\"
                strFilename = String.Concat(strFilename, bv_strInvoiceNo)
            ElseIf bv_strBillingType.ToUpper = "DRAFT" Then
                strFilename = strFilename & "Invoice\Draft\"
                strFilename = String.Concat(strFilename, bv_strInvoiceNo)
            End If

            Return strFilename
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GenerateFileName"
    Private Function GenerateFileName(ByVal bv_strFolderName As String) As String
        Try
            Dim strFilename As String
            strFilename = CommonWeb.pub_GetConfigValue(bv_strFolderName)
            strFilename = String.Concat(strFilename, GetQueryString("DCMNT_NAM").Replace(" ", ""), Now.ToFileTime)
            'strFilename = String.Concat(strFilename, "CMN", Now.ToFileTime)
            txtSubject.Text = GetQueryString("DCMNT_NAM")
            Return strFilename
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GenerateExportTariffFileName"
    Private Function GenerateExportTariffFileName(ByVal bv_strFolderName As String, ByVal strTariffDescription As String) As String
        Try
            Dim strFilename As String
            strFilename = CommonWeb.pub_GetConfigValue(bv_strFolderName)
            'strFilename = String.Concat(strFilename, GetQueryString("DCMNT_NAM").Replace(" ", ""))
            strFilename = String.Concat(strFilename, "Tariff_", strTariffDescription, "_", Now.ToString("ddMMyyyy"))
            Return strFilename
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateStream"
    Private Function CreateStream(ByVal name As String, _
                                 ByVal fileNameExtension As String, _
                                 ByVal encoding As Encoding, _
                                 ByVal mimeType As String, _
                                 ByVal willSeek As Boolean) As Stream
        Try
            Dim objStream As Stream = New FileStream((CommonWeb.pub_GetConfigValue("UploadPhyPath")) + _
                             name + "." + fileNameExtension, FileMode.Create)
            objMemoryStream.Add(objStream)
            Return objStream
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            Throw ex
        End Try
    End Function
#End Region

#Region "ShowReport"
    Private Sub ShowReport(ByVal bv_strReportName As String, ByVal bv_strReportType As String, ByVal bv_strDocumentTemplateFormat As String)
        Try
            Dim strLessee As String = ""
            Dim strRedel As String = ""
            Dim strDocumentName As String
            With rvDocuments
                .LocalReport.EnableExternalImages = True
                .ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
                .LocalReport.DataSources.Clear()
                Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource
                For Each ky As Data.DataTable In rptDatasources.Tables
                    .LocalReport.DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource(ky.TableName, ky))
                Next
                '.LocalReport.ReportPath = Server.MapPath(String.Concat("Report/", bv_strReportName, ".rdlc"))
                Dim strAppDomainAppPath As String = System.Web.HttpRuntime.AppDomainAppPath()
                'Dim strLogoURL As String = String.Concat("file:///", strAppDomainAppPath, ConfigurationManager.AppSettings("DocumentLogo").Replace("../", ""))
                Dim strLogoURL As String = String.Concat(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), HttpRuntime.AppDomainAppVirtualPath, ConfigurationManager.AppSettings("DocumentLogo").Replace("../", "/"))
                Dim strBulkEmailLocalurl As String = Config.pub_GetAppConfigValue("BulkEmailLocalURL")
                Dim strUserName As String = objCommon.GetCurrentUserName()
                Dim strDepotName As String = objCommon.GetDepotName()
                Dim strDepotLogo As String = objCommon.GetCustomerLogo()
                Dim strLogoLeaf As String = String.Concat("file:///", strAppDomainAppPath, ConfigurationManager.AppSettings("LeafPhoto").Replace("../", ""))
                Dim strLogoCouncil As String = String.Concat("file:///", strAppDomainAppPath, ConfigurationManager.AppSettings("CouncilPhoto").Replace("../", ""))
                Dim bln007Key As Boolean = False
                Dim bln024Key As Boolean = False
                Dim bln071Key As Boolean = False
                Dim bln025Key As Boolean = False
                Dim bln022Key As Boolean = False
                Dim str007Value As String = objCommon.GetConfigSetting("007", bln007Key)
                Dim str024Value As String = objCommon.GetConfigSetting("024", bln024Key)
                Dim str025Value As String = objCommon.GetConfigSetting("025", bln025Key)
                Dim str022Value As String = objCommon.GetConfigSetting("022", bln022Key)
                Dim strLogoPath As String
                ' RetrieveData(strDocumentName)
                If Not rptDatasources.Tables(CommonUIData._DEPOT) Is Nothing Then
                    If rptDatasources.Tables(CommonUIData._DEPOT).Rows.Count > 0 Then
                        strLogoPath = rptDatasources.Tables(CommonUIData._DEPOT).Rows(0).Item(CommonUIData.CMPNY_LG_PTH)
                    Else
                        Dim objDepots As New Depots
                        strLogoPath = objDepots.GetDEPOTByDPT_CD(objCommon.GetDepotCD()).Tables(DepotData._DEPOT).Rows(0).Item(DepotData.CMPNY_LG_PTH)
                    End If
                End If
                Dim str071Value As String = objCommon.GetConfigSetting("071", bln071Key)

                Dim strMultiLogoURL As String = String.Concat("file:///", Server.MapPath(Config.pub_GetAppConfigValue("UploadPhotoPath")), strLogoPath)
                Dim bln070Key As Boolean = False
                Dim str070Value As String = objCommon.GetConfigSetting("070", bln070Key)
                If str070Value Then
                    strLogoURL = strMultiLogoURL
                End If
                .LocalReport.ReportPath = Server.MapPath(bv_strReportName)
                If bv_strReportType = "Report" Then
                    .LocalReport.EnableExternalImages = True
                    Dim dtParmeters As DataTable
                    dtParmeters = CType(RetrieveData("reportparamlist"), DataTable)
                    If dtParmeters.Rows.Count > 0 Then
                        Dim iparamcount As Int32 = dtParmeters.Rows.Count
                        Dim i As Int32 = 0
                        Dim params(iparamcount - 1) As ReportParameter
                        For Each drparam As DataRow In dtParmeters.Rows
                            params(i) = New Microsoft.Reporting.WebForms.ReportParameter(drparam.Item("parametername").ToString.Replace(" ", ""), drparam.Item("parametervalue").ToString, True)
                            If Not drparam("prmtr_rl").ToString = "" And Not drparam("prmtr_rl").ToString = String.Empty And drparam("prmtr_rl").ToString = "<020>" Then
                                strLessee = drparam("prmtr_dsply_nam").ToString
                            End If
                            If Not drparam("prmtr_rl").ToString = "" And Not drparam("prmtr_rl").ToString = String.Empty And drparam("prmtr_rl").ToString = "<021>" Then
                                strRedel = drparam("prmtr_dsply_nam").ToString
                            End If
                            i = i + 1
                        Next
                        .LocalReport.SetParameters(params)
                        .LocalReport.Refresh()
                    End If
                ElseIf bv_strReportType = "document" Then
                    Dim prmtitle As New ReportParameter("title", CStr(GetQueryString("title")))
                    Dim logo As New ReportParameter("logo", strLogoURL)
                    Dim username As New ReportParameter("username", strUserName)
                    Dim depot As New ReportParameter("depot", strDepotName)
                    Dim logoleaf As New ReportParameter("logoleaf", strLogoLeaf)
                    Dim logocouncil As New ReportParameter("logocouncil", strLogoCouncil)
                    Dim bulkemaillocal As New ReportParameter("bulkemaillocalurl", strBulkEmailLocalurl)

                    .LocalReport.EnableExternalImages = True
                    .LocalReport.EnableHyperlinks = True

                    .LocalReport.SetParameters(New ReportParameter() {prmtitle})
                    .LocalReport.SetParameters(New ReportParameter() {logo})
                    .LocalReport.SetParameters(New ReportParameter() {username})
                    .LocalReport.SetParameters(New ReportParameter() {depot})
                    .LocalReport.SetParameters(New ReportParameter() {bulkemaillocal})
                    ''23609 Add schedule date parameter and value
                    If Not GetQueryString("dcmnt_id") Is Nothing Then
                        If CStr(GetQueryString("dcmnt_id")) = "36" OrElse CStr(GetQueryString("dcmnt_id")) = "37" OrElse CStr(GetQueryString("dcmnt_id")) = "38" OrElse CStr(GetQueryString("dcmnt_id")) = "39" Then
                            Dim scheduledate As New ReportParameter("scheduledate", GetQueryString("SCHEDULE_DATE").ToString)
                            .LocalReport.SetParameters(New ReportParameter() {scheduledate})
                        End If
                    End If
                    ''
                    If Not GetQueryString("dcmnt_id") Is Nothing Then
                        If CStr(GetQueryString("dcmnt_id")) = "47" Then
                            Dim acaciaSpecific As New ReportParameter("acaciaSpecific", str071Value)
                            .LocalReport.SetParameters(New ReportParameter() {acaciaSpecific})
                        End If
                    End If

                    If bln007Key = "True" Then
                        Dim EirNo As New ReportParameter("EirNo", str007Value)
                        .LocalReport.SetParameters(New ReportParameter() {EirNo})
                    Else
                        Dim EirNo As New ReportParameter("EirNo", "EIR No")
                        .LocalReport.SetParameters(New ReportParameter() {EirNo})
                    End If
                    If bln024Key = "True" Then
                        .LocalReport.SetParameters(New ReportParameter() {logoleaf})
                    End If
                    If bln025Key = "True" Then
                        .LocalReport.SetParameters(New ReportParameter() {logocouncil})
                    End If
                    'If bln022Key = "True" Then
                    If CStr(GetQueryString("dcmnt_id")) = "11" OrElse CStr(GetQueryString("dcmnt_id")) = "16" Then
                        If str022Value.ToString.ToUpper = "TRUE" AndAlso objCommon.GetDepotID = objCommon.GetHeadQuarterID Then
                            Dim Rental As New ReportParameter("Rental", False)
                            .LocalReport.SetParameters(New ReportParameter() {Rental})
                        Else
                            Dim Rental As New ReportParameter("Rental", True)
                            .LocalReport.SetParameters(New ReportParameter() {Rental})
                        End If

                    End If
                    'End If
                    'MultiDepo
                    If CStr(GetQueryString("dcmnt_id")) = "21" OrElse CStr(GetQueryString("dcmnt_id")) = "22" OrElse (CStr(GetQueryString("dcmnt_id")) = "23") OrElse _
                          (CStr(GetQueryString("dcmnt_id")) = "30") OrElse (CStr(GetQueryString("dcmnt_id")) = "31") OrElse (CStr(GetQueryString("dcmnt_id")) = "37") OrElse _
                          (CStr(GetQueryString("dcmnt_id")) = "38") OrElse (CStr(GetQueryString("dcmnt_id")) = "39") OrElse (CStr(GetQueryString("dcmnt_id")) = "36" OrElse _
                                                                                                                             CStr(GetQueryString("dcmnt_id")) = "33") Then
                        If str070Value Then
                            Dim MultiDepot As New ReportParameter("MultiDepot", True)
                            .LocalReport.SetParameters(New ReportParameter() {MultiDepot})
                        Else
                            Dim MultiDepot As New ReportParameter("MultiDepot", False)
                            .LocalReport.SetParameters(New ReportParameter() {MultiDepot})
                        End If
                    End If

                    .LocalReport.Refresh()
                ElseIf bv_strReportType = "CreatePdf" Then
                    .LocalReport.Refresh()
                Else
                    Dim prmtitle As New ReportParameter("title", CStr(GetQueryString("title")))
                    Dim logo As New ReportParameter("logo", strLogoURL)
                    Dim username As New ReportParameter("username", strUserName)
                    Dim depot As New ReportParameter("depot", strDepotName)
                    Dim bulkemaillocal As New ReportParameter("bulkemaillocalurl", strBulkEmailLocalurl)
                    Dim logocouncil As New ReportParameter("logocouncil", strLogoCouncil)
                    Dim logoleaf As New ReportParameter("logoleaf", strLogoLeaf)
                    If Not GetQueryString("dcmnt_id") Is Nothing Then
                        If CStr(GetQueryString("dcmnt_id")) = "36" OrElse CStr(GetQueryString("dcmnt_id")) = "37" OrElse CStr(GetQueryString("dcmnt_id")) = "38" Then
                            Dim scheduledate As New ReportParameter("scheduledate", GetQueryString("SCHEDULE_DATE").ToString)
                            .LocalReport.SetParameters(New ReportParameter() {scheduledate})
                        End If
                    End If

                    If Not GetQueryString("dcmnt_id") Is Nothing Then
                        If CStr(GetQueryString("dcmnt_id")) = "26" OrElse CStr(GetQueryString("dcmnt_id")) = "40" OrElse CStr(GetQueryString("dcmnt_id")) = "47" OrElse CStr(GetQueryString("dcmnt_id")) = "27" OrElse CStr(GetQueryString("dcmnt_id")) = "28" OrElse CStr(GetQueryString("dcmnt_id")) = "29" OrElse CStr(GetQueryString("dcmnt_id")) = "24" Then
                            Dim acaciaSpecific As New ReportParameter("acaciaSpecific", str071Value)
                            .LocalReport.SetParameters(New ReportParameter() {acaciaSpecific})
                        End If
                    End If

                    'Add Image Parameter
                    ' Dim strReportTiltle As String = CStr(GetQueryString("title"))
                    .LocalReport.EnableHyperlinks = True
                    .LocalReport.EnableExternalImages = True
                    .LocalReport.SetParameters(New ReportParameter() {prmtitle})
                    .LocalReport.SetParameters(New ReportParameter() {logo})
                    .LocalReport.SetParameters(New ReportParameter() {username})
                    .LocalReport.SetParameters(New ReportParameter() {depot})
                    .LocalReport.SetParameters(New ReportParameter() {bulkemaillocal})
                    If bln007Key = "True" Then
                        Dim EirNo As New ReportParameter("EirNo", str007Value)
                        .LocalReport.SetParameters(New ReportParameter() {EirNo})
                    Else
                        Dim EirNo As New ReportParameter("EirNo", "EIR No")
                        .LocalReport.SetParameters(New ReportParameter() {EirNo})
                    End If
                    If bln024Key = "True" Then
                        .LocalReport.SetParameters(New ReportParameter() {logoleaf})
                    End If
                    If bln025Key = "True" Then
                        .LocalReport.SetParameters(New ReportParameter() {logocouncil})
                    End If

                    If CStr(GetQueryString("dcmnt_id")) = "21" OrElse CStr(GetQueryString("dcmnt_id")) = "22" OrElse (CStr(GetQueryString("dcmnt_id")) = "23") OrElse _
                        (CStr(GetQueryString("dcmnt_id")) = "30") OrElse (CStr(GetQueryString("dcmnt_id")) = "31") OrElse (CStr(GetQueryString("dcmnt_id")) = "37") OrElse _
                        (CStr(GetQueryString("dcmnt_id")) = "38") OrElse (CStr(GetQueryString("dcmnt_id")) = "39") OrElse (CStr(GetQueryString("dcmnt_id")) = "36") Then
                        If str070Value Then
                            Dim MultiDepot As New ReportParameter("MultiDepot", True)
                            .LocalReport.SetParameters(New ReportParameter() {MultiDepot})
                        Else
                            Dim MultiDepot As New ReportParameter("MultiDepot", False)
                            .LocalReport.SetParameters(New ReportParameter() {MultiDepot})
                        End If
                    End If
                    .LocalReport.Refresh()
                End If
            End With
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "GetParameterCollections"
    Private Function GetParameterCollections() As String
        Try
            Dim strParamCollection As New StringBuilder
            Dim htbParameters As String = ""
            For Each strTemp As String In Request.QueryString.AllKeys
                If Not strTemp Is Nothing Then
                    If strParamCollection.ToString <> "" Then
                        strParamCollection.Append("&")
                    End If
                    If strTemp = "wfdata" Then
                        strParamCollection.Append(String.Concat(strTemp, "=", Server.HtmlDecode(GetQueryString((strTemp)))))
                    Else
                        strParamCollection.Append(String.Concat(strTemp, "=", GetQueryString((strTemp))))
                    End If
                End If
            Next
            Return strParamCollection.ToString
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            Throw ex
        End Try
    End Function
#End Region

#Region "Export"
    Private Sub Export(ByVal bv_strReportName As String, ByVal objLocalReport As LocalReport, ByVal bv_strFormat As String, ByVal bv_strDocumentTemplateFormat As String)
        Try
            Dim strTempSession As String = RetrieveData(bv_strReportName + "_psrc")
            If strTempSession = 0 OrElse strTempSession Is Nothing Then
                strTempSession = 1
            End If

            Dim strDeviceInfo As String = String.Empty

            If bv_strFormat = "PDF" And bv_strDocumentTemplateFormat = "Portrait" Then
                strDeviceInfo = _
                        "<DeviceInfo>" + _
                        "  <OutputFormat>PDF</OutputFormat>" + _
                        "  <PageWidth>8.5in</PageWidth>" + _
                        "  <PageHeight>11.5in</PageHeight>" + _
                        "  <MarginTop>0.2in</MarginTop>" + _
                        "  <MarginLeft>0.2in</MarginLeft>" + _
                        "  <MarginRight>0.2in</MarginRight>" + _
                        "  <MarginBottom>0.2in</MarginBottom>" + _
                        "</DeviceInfo>"
            ElseIf bv_strFormat = "PDF" And bv_strDocumentTemplateFormat = "Landscape" Then
                strDeviceInfo = _
                        "<DeviceInfo>" + _
                        "  <OutputFormat>PDF</OutputFormat>" + _
                        "  <PageWidth>11.5in</PageWidth>" + _
                        "  <PageHeight>8.5in</PageHeight>" + _
                        "  <MarginTop>0.5in</MarginTop>" + _
                        "  <MarginLeft>0.5in</MarginLeft>" + _
                        "  <MarginRight>0.5in</MarginRight>" + _
                        "  <MarginBottom>0.5in</MarginBottom>" + _
                        "</DeviceInfo>"
            ElseIf UCase(bv_strFormat) = "EXCEL" Then
                strDeviceInfo = "<DeviceInfo> <SimplePageHeaders>False</SimplePageHeaders></DeviceInfo>"
            ElseIf UCase(bv_strFormat) = "WORD" Then
                strDeviceInfo = "<DeviceInfo> <SimplePageHeaders>False</SimplePageHeaders></DeviceInfo>"
            Else
                strDeviceInfo = _
                          "<DeviceInfo>" + _
                          "  <OutputFormat>EMF</OutputFormat>" + _
                          "  <PageWidth>8.5in</PageWidth>" + _
                          "  <PageHeight>11in</PageHeight>" + _
                          "  <MarginTop>0.25in</MarginTop>" + _
                          "  <MarginLeft>0.25in</MarginLeft>" + _
                          "  <MarginRight>0.25in</MarginRight>" + _
                          "  <MarginBottom>0.25in</MarginBottom>" + _
                          "  <StartPage>" + strTempSession + "</StartPage>" + _
                          "  <EndPage>" + strTempSession + "</EndPage>" + _
                          "</DeviceInfo>"
            End If

            Dim warnings() As Microsoft.Reporting.WebForms.Warning = Nothing
            objMemoryStream = New List(Of Stream)()
            Dim bln007Key As Boolean = False
            Dim str007Value As String = objCommon.GetConfigSetting("007", bln007Key)
            Dim bln022Key As Boolean = False
            Dim str022Value As String = objCommon.GetConfigSetting("022", bln022Key)
            If bln007Key = "True" Then
                Dim EirNo As New ReportParameter("EirNo", str007Value)
                objLocalReport.SetParameters(New ReportParameter() {EirNo})
            End If
            If CStr(GetQueryString("dcmnt_id")) = "11" OrElse CStr(GetQueryString("dcmnt_id")) = "16" Then
                If bln022Key = "True" Then
                    Dim Rental As New ReportParameter("Rental", str022Value)
                    objLocalReport.SetParameters(New ReportParameter() {Rental})
                End If
            End If

            objLocalReport.Render(bv_strFormat, strDeviceInfo, AddressOf CreateStream, warnings)

            Dim objStream As Stream
            For Each objStream In objMemoryStream
                objStream.Position = 0
            Next

            CacheData(bv_strReportName + "_psrc", CInt(strTempSession) + 1)

        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "ExportInvoice"
    Private Sub ExportInvoice(ByVal bv_strReportName As String, ByVal objLocalReport As LocalReport, ByVal bv_strFormat As String, ByVal bv_strInvoiceType As String)
        Try
            Dim strTempSession As String = RetrieveData(bv_strReportName + "_psrc")
            If strTempSession = 0 OrElse strTempSession Is Nothing Then
                strTempSession = 1
            End If

            Dim strDeviceInfo As String = String.Empty

            If bv_strFormat = "PDF" Then
                strDeviceInfo = "<DeviceInfo>" + _
                        "  <OutputFormat>PDF</OutputFormat>" + _
                       "  <PageWidth>11in</PageWidth>" + _
                        "  <PageHeight>8.5in</PageHeight>" + _
                        "  <MarginTop>0.25in</MarginTop>" + _
                        "  <MarginLeft>0.25in</MarginLeft>" + _
                        "  <MarginRight>0.25in</MarginRight>" + _
                        "  <MarginBottom>0.25in</MarginBottom>" + _
                        "</DeviceInfo>"
            ElseIf UCase(bv_strFormat) = "EXCEL" Then
                strDeviceInfo = "<DeviceInfo> <SimplePageHeaders>False</SimplePageHeaders></DeviceInfo>"
            Else
                strDeviceInfo = _
                          "<DeviceInfo>" + _
                          "  <OutputFormat>EMF</OutputFormat>" + _
                          "  <PageWidth>11in</PageWidth>" + _
                        "  <PageHeight>8.5in</PageHeight>" + _
                        "  <MarginTop>0.25in</MarginTop>" + _
                        "  <MarginLeft>0.25in</MarginLeft>" + _
                        "  <MarginRight>0.25in</MarginRight>" + _
                        "  <MarginBottom>0.25in</MarginBottom>" + _
                          "  <StartPage>" + strTempSession + "</StartPage>" + _
                          "  <EndPage>" + strTempSession + "</EndPage>" + _
                          "</DeviceInfo>"
            End If

            Dim warnings() As Microsoft.Reporting.WebForms.Warning = Nothing
            objMemoryStream = New List(Of Stream)()

            objLocalReport.Render(bv_strFormat, strDeviceInfo, AddressOf CreateStream, warnings)

            Dim objStream As Stream
            For Each objStream In objMemoryStream
                objStream.Position = 0
            Next

            CacheData(bv_strReportName + "_psrc", CInt(strTempSession) + 1)

        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "CreateAlert"
                CreateAlert(e.GetCallbackValue("EmailTo"), e.GetCallbackValue("EmailSubject"), _
                         e.GetCallbackValue("EmailBody"), e.GetCallbackValue("AttachFile"), _
                         e.GetCallbackValue("TransactionNo"), e.GetCallbackValue("TemplateID"), _
                         e.GetCallbackValue("WFDATA"))
        End Select
    End Sub
#End Region

#Region "CreateAlert"
    Private Sub CreateAlert(ByVal bv_strSendTo As String, ByVal bv_strSubject As String, _
                            ByVal bv_strBody As String, ByVal bv_strAttachFile As String, _
                            ByVal bv_strTransactionNo As String, ByVal bv_strTemplateID As String, _
                            ByVal bv_strWFDATA As String)
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

            bv_strAttachFile = String.Concat(ConfigurationManager.AppSettings.Get("UploadPhyPath"), bv_strAttachFile)
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

#Region "rvDocuments_Drillthrough"
    Protected Sub rvDocuments_Drillthrough(ByVal sender As Object, ByVal e As Microsoft.Reporting.WebForms.DrillthroughEventArgs) Handles rvDocuments.Drillthrough
        Try
            Dim strFilter As String = Nothing
            Dim strTableName As String = String.Empty
            Dim intCount As Integer = 0
            Dim DrillThroughValues As ReportParameterInfoCollection = e.Report.GetParameters()
            Dim extraFilter As String = ""
            Dim drillReportDataSet As New Data.DataSet

            For Each d As ReportParameterInfo In DrillThroughValues
                If d.Name.ToString().Trim() <> "TableName" And d.Name.ToString().Trim() <> "fc" Then
                    Try
                        If d.Values(0) IsNot Nothing Then
                            If strFilter <> "" Then
                                strFilter = String.Concat(strFilter, " and ", d.Name.ToString().Trim(), "='", d.Values(0).ToString().Trim(), "'")
                            Else
                                strFilter = String.Concat(d.Name.ToString().Trim(), "='", d.Values(0).ToString().Trim(), "'")
                            End If
                        End If
                    Catch ex As Exception
                        iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
                    End Try

                Else
                    If d.Name.ToString().Trim() = "TableName" Then
                        strTableName = d.Values(0).ToString().Trim()
                    End If
                End If

                If d.Name.ToString().Trim() = "fc" Then
                    If d.Values(0) IsNot Nothing Then
                        If strFilter <> "" Then
                            extraFilter = d.Values(0).ToString().Trim()
                            strFilter = String.Concat(strFilter, " and ", extraFilter)
                        Else
                            strFilter = String.Concat(strFilter, extraFilter)
                        End If
                    End If
                End If
            Next


            If strTableName = "V_TARIFF_CONTROL_INDICATOR" Then
                Dim strPeriodFrom, strPeriodTo, strRepairCompanyID, strBaseCurrency, strRankBy, _
                    strOrganizationID, strOrganizationType, strReportType As String

                strOrganizationID = "0"
                strRepairCompanyID = "0"
                strPeriodFrom = ""
                strPeriodTo = ""
                strBaseCurrency = ""
                strRankBy = ""
                strOrganizationType = ""
                strReportType = ""

                For Each d As ReportParameterInfo In DrillThroughValues
                    If d.Name.ToString().Trim() = "PeriodTo" Then
                        strPeriodTo = d.Values(0).ToString().Trim()
                    ElseIf d.Name.ToString().Trim() = "PeriodFrom" Then
                        strPeriodFrom = d.Values(0).ToString().Trim()
                    ElseIf d.Name.ToString().Trim() = "RepairCompany" Then
                        strRepairCompanyID = d.Values(0).ToString().Trim()
                    ElseIf d.Name.ToString().Trim() = "BaseCurrency" Then
                        strBaseCurrency = d.Values(0).ToString().Trim()
                    ElseIf d.Name.ToString().Trim() = "RankBy" Then
                        strRankBy = d.Values(0).ToString().Trim()
                    ElseIf d.Name.ToString().Trim() = "Organization" Then
                        strOrganizationID = d.Values(0).ToString().Trim()
                    ElseIf d.Name.ToString().Trim() = "OrganizationType" Then
                        strOrganizationType = d.Values(0).ToString().Trim()
                    ElseIf d.Name.ToString().Trim() = "Organization" Then
                        strOrganizationID = d.Values(0).ToString().Trim()
                    ElseIf d.Name.ToString().Trim() = "ReportType" Then
                        strReportType = d.Values(0).ToString().Trim()
                    End If
                Next

                'Dim ObjEstimate As New Organization
                'Dim dsParameter As New OrganizationDataSet
                'drillReportDataSet = ObjEstimate.pub_GetTariffControlIndicator(strPeriodFrom, strPeriodTo, strRankBy, _
                '                     CInt(strRepairCompanyID), CInt(strOrganizationID), strOrganizationType, strReportType)

                CacheData(GetQueryString("DCMNT_NAM"), drillReportDataSet)

            ElseIf strTableName = "V_TARIFF_CONTROL_INDICATORSUB" Then
                drillReportDataSet = RetrieveData(GetQueryString("DCMNT_NAM"))
            End If

            Dim drillthroughlocalreport As LocalReport = DirectCast(e.Report, LocalReport)

            If strTableName <> "" Then
                If drillthroughlocalreport.ReportPath.Contains(e.ReportPath) Then
                    With rvDocuments
                        Dim dt As DataTable
                        dt = drillReportDataSet.Tables(strTableName)
                        Dim dv As DataView
                        dv = dt.DefaultView
                        If strTableName <> "V_TARIFF_CONTROL_INDICATOR" Then
                            dv.RowFilter = strFilter
                        End If
                        Dim drillReportDS As New ReportDataSource(strTableName, dv.ToTable)
                        drillthroughlocalreport.DataSources.Clear()
                        drillthroughlocalreport.DataSources.Add(drillReportDS)
                        drillthroughlocalreport.Refresh()
                    End With
                End If
            End If

        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/ModalWindow.js", MyBase.Page)
        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region



End Class