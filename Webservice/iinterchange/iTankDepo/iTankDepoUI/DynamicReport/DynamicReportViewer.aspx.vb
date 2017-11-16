Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Data
Imports System.Configuration
Imports System.Math
Partial Class DynamicReportViewer
    Inherits PageBase
    Private dtParameterList As DataTable
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'pnlDocView.Visible = False

            Dim strCustomer As String = String.Empty
            pnlEmail.Visible = False
            If Page.IsPostBack = False Then
                Dim objCommondata As New CommonData
                Dim strReportName As String = GetQueryString("reportname")
                Dim strUserName As String = ""
                Dim strActivityId As String = GetQueryString("rptid")
                Dim ReportSource As New ReportViewerDataSource


                rsPageHeight = GetQueryString("h")
                If String.IsNullOrEmpty(rsPageHeight) = True OrElse rsPageHeight = "undefined" Then
                    rsPageHeight = "279.4"
                End If
                rsPageWidth = GetQueryString("w")
                If String.IsNullOrEmpty(rsPageWidth) = True OrElse rsPageWidth = "undefined" Then
                    rsPageWidth = "215.9"
                End If
                rsLeftMargin = GetQueryString("l")
                If String.IsNullOrEmpty(rsLeftMargin) = True OrElse rsLeftMargin = "undefined" Then
                    rsLeftMargin = "12.7"
                End If
                rsRightMargin = GetQueryString("r")
                If String.IsNullOrEmpty(rsRightMargin) = True OrElse rsRightMargin = "undefined" Then
                    rsRightMargin = "12.7"
                End If
                rsTopMargin = GetQueryString("t")
                If String.IsNullOrEmpty(rsTopMargin) = True OrElse rsTopMargin = "undefined" Then
                    rsTopMargin = "12.7"
                End If
                rsBottomMargin = GetQueryString("b")
                If String.IsNullOrEmpty(rsBottomMargin) = True OrElse rsBottomMargin = "undefined" Then
                    rsBottomMargin = "12.7"
                End If
                'DirectCast(DirectCast(DocViewer, Microsoft.Reporting.WebForms.ReportViewer).Controls(1), System.Web.UI.Control).Controls(7).Visible = False

                If Not Request.QueryString("reportname") Is Nothing Then

                    If ConfigurationManager.AppSettings("ReportServerCredential").ToUpper = "TRUE" Then
                        Dim rc As Microsoft.Reporting.WebForms.IReportServerCredentials = New DynamicReportCustomReportCredentials(ConfigurationManager.AppSettings("ReportServerUID"), ConfigurationManager.AppSettings("ReportServerPWD"), ConfigurationManager.AppSettings("ReportServerDomain"))
                        RptVwr.ServerReport.ReportServerCredentials = rc
                    End If
                    RptVwr.ServerReport.ReportServerUrl = pvt_Getreportserver()
                    RptVwr.ServerReport.ReportPath = String.Concat(ConfigurationManager.AppSettings("ReportFolderpath"), Request.QueryString("reportname"))
                    RptVwr.ShowParameterPrompts = False
                    RptVwr.ShowPrintButton = False
                    dtParameterList = CType(RetrieveData("reportparamlist" + strActivityId), DataTable)
                    Dim dsUserDetail As UserDataSet = CType(RetrieveData("UserData"), UserDataSet)

                    strUserName = objCommondata.GetCurrentUserName()
                End If
                Dim dsParameterMasters As Data.DataSet = CType(RetrieveData("reportds" + strActivityId), DataSet)

                Dim blnReportParmChk As Boolean = True
                If Not dsParameterMasters Is Nothing AndAlso dtParameterList.Rows.Count > 0 Then

                    Dim reportParamArray() As Microsoft.Reporting.WebForms.ReportParameter = ReportSource.GetDynamicReportParameters(dtParameterList, CType(RetrieveData("reportds" + strActivityId), DataSet), strUserName, strActivityId)

                    For Each reportParam As Microsoft.Reporting.WebForms.ReportParameter In reportParamArray
                        If reportParam Is Nothing Then
                            blnReportParmChk = False
                        End If

                    Next

                    If blnReportParmChk = True Then
                        RptVwr.ServerReport.SetParameters(reportParamArray)
                    End If

                End If

                'RptVwr.DataBind()
                Dim dmodevalue As String = Request.QueryString("type")
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
                    divDocView.Visible = False
                    pnlEmail.Visible = True
                    txtBody.Text = ""
                    strUserName = objCommondata.GetCurrentUserName()
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
                    divDocView.Visible = True
                    ClientScript.RegisterStartupScript(GetType(System.String), "ShowReportContent", "ShowReportContent();", True)
                    pnlEmail.Visible = False
                End If
            End If



        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Sub
    Private Function pvt_Getreportserver() As System.Uri
        Try
            Dim strUri As New System.UriBuilder(ConfigurationManager.AppSettings("ReportServerPath"))
            Return strUri.Uri

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Function

    Protected Sub RptVwr_ReportError(ByVal sender As Object, ByVal e As Microsoft.Reporting.WebForms.ReportErrorEventArgs) Handles RptVwr.ReportError
        Try
            e.Handled = True

            If e.Exception.GetType Is GetType(MissingParameterException) Then

                ILabel1.Text = CustomizeError(e.Exception.Message)
                'pnlDocView.Visible = False
                'ClientScript.RegisterStartupScript(GetType(System.String), "HideReportContent", "HideReportContent();", True)
                pnlEmail.Visible = False
            Else

            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Sub

    Private Function CustomizeError(ByVal bv_errmsg As String) As String
        Try
            Dim regpattern As New Regex("'\b\w+\b'")
            Dim paramname As String
            Dim dr() As DataRow
            If regpattern.IsMatch(bv_errmsg) Then
                paramname = regpattern.Match(bv_errmsg).ToString
                dr = dtParameterList.Select(String.Concat("parameter=", paramname))
                paramname = dr(0).Item("parametername").ToString
                Return regpattern.Replace(bv_errmsg, paramname)
            Else
                Return bv_errmsg
            End If
            dr = Nothing
            paramname = Nothing
            regpattern = Nothing

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Function

#Region "Page_PreRender"

    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/DynamicReport/DynamicReportDialog.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Callback.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Controls/WebUIValidation.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region


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
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub


    Private Sub ProcessCreateEmail(ByVal bv_rptName As String)
        Try
            Dim s As String = Session(bv_rptName + "_psrc")
            If s = 0 OrElse s Is Nothing Then
                s = 1
            End If
            Dim deviceInfo As String = String.Empty
            Dim results As Byte()
            Dim extension As String
            Dim strFormat As String
            Dim warning As Microsoft.Reporting.WebForms.Warning() = Nothing
            Dim strReportPath As String = String.Empty
            'Convert Millimeter to INCH
            pvt_fnConvertMM_to_INCH(rsPageWidth, rsPageHeight, rsTopMargin, rsBottomMargin, rsLeftMargin, rsRightMargin)

            
            extension = "pdf"
            strFormat = "PDF"
            deviceInfo = "<DeviceInfo>" + _
                "  <OutputFormat>PDF</OutputFormat>" + _
                "  <PageWidth>10.5in</PageWidth>" + _
                "  <PageHeight>11in</PageHeight>" + _
                "  <MarginTop>0.25in</MarginTop>" + _
                "  <MarginLeft>0.25in</MarginLeft>" + _
                "  <MarginRight>0.25in</MarginRight>" + _
                "  <MarginBottom>0.25in</MarginBottom>" + _
                "</DeviceInfo>"

            results = RptVwr.ServerReport.Render(strFormat, deviceInfo, "", "", "." & extension, Nothing, warning)
            Dim oFileStream As System.IO.FileStream
            strReportPath = String.Concat(GenerateFileName(), ".", extension)
            oFileStream = New System.IO.FileStream(strReportPath, System.IO.FileMode.OpenOrCreate)
            oFileStream.Write(results, 0, results.Length)
            oFileStream.Close()
            results = Nothing

            Session(bv_rptName + "_psrc") = s + 1


            hdnattachfile.Value = strReportPath
            lnkAttachment.InnerText = Mid(strReportPath, strReportPath.LastIndexOf("\") + 2)
            txtSubject.Text = bv_rptName
            lnkAttachment.Attributes.Add("href", String.Concat("../download.ashx?FL_NM=", lnkAttachment.InnerText))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
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
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
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
           New FileStream(String.Concat(ConfigurationManager.AppSettings("UploadDocPath"), _
            name, "_", Now.ToString("ddMMyyhhmmss"), ".", fileNameExtension), FileMode.Create)
            m_streams.Add(stream)
            Return stream
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function GetParameterCollections() As String
        Try
            Dim strParamCollection As New StringBuilder
            Dim htbParameters As String = ""
            Dim dtParameters As DataTable = pub_RetrieveData("parameters")
            For Each drparam As DataRow In dtParameters.Rows
                If strParamCollection.ToString <> "" Then
                    strParamCollection.Append("&")
                End If
                strParamCollection.Append(String.Concat(drparam.Item(CommonUIData.PRMTR_NAM), "=", drparam.Item("prmtr_val")))
            Next
            Return strParamCollection.ToString
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
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
                deviceInfo = "<DeviceInfo>" + _
                   "  <OutputFormat>PDF</OutputFormat>" + _
                   "  <PageWidth>10.5in</PageWidth>" + _
                   "  <PageHeight>11in</PageHeight>" + _
                   "  <MarginTop>0.25in</MarginTop>" + _
                   "  <MarginLeft>0.25in</MarginLeft>" + _
                   "  <MarginRight>0.25in</MarginRight>" + _
                   "  <MarginBottom>0.25in</MarginBottom>" + _
                   "</DeviceInfo>"
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
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub

#Region "SendMail"
    Private Sub SendMail(ByVal bv_strSendTo As String, ByVal bv_strSubject As String, _
                            ByVal bv_strBody As String, ByVal bv_strAttachFile As String)
        Try
            Dim objCommonUI As New CommonUI
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
            pub_Send_Email(bv_strSendTo, bv_strSubject, sbrFootNote.ToString(), bv_strAttachFile)
            'Dim wfdata As String = Server.UrlDecode(bv_strWFDATA)
            'strReferenceNo = objCommonUI.pub_CreateAlert(bv_strTransactionNo, bv_strTemplateID, bv_strSendTo, bv_strSubject, bv_strBody, bv_strAttachFile, bv_strWFDATA)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "SendMail"
                SendMail(e.GetCallbackValue("EmailTo"), e.GetCallbackValue("EmailSubject"), _
                         e.GetCallbackValue("EmailBody"), e.GetCallbackValue("AttachFile"))
        End Select
    End Sub

    Protected Sub DocViewer_Drillthrough(ByVal sender As Object, ByVal e As Microsoft.Reporting.WebForms.DrillthroughEventArgs) Handles RptVwr.Drillthrough
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
        rsPageWidth = Round(str_rsPageWidth * 0.0393700787, 1)
        rsPageHeight = Round(str_rsPageHeight * 0.0393700787, 1)
        rsTopMargin = Round(str_rsTopMargin * 0.0393700787, 1)
        rsBottomMargin = Round(str_rsBottomMargin * 0.0393700787, 1)
        rsLeftMargin = Round(str_rsLeftMargin * 0.0393700787, 1)
        rsRightMargin = Round(str_rsRightMargin * 0.0393700787, 1)
    End Sub

End Class

Public Class DynamicReportCustomReportCredentials
    Implements Microsoft.Reporting.WebForms.IReportServerCredentials

    Dim _UserName As String
    Dim _PassWord As String
    Dim _DomainName As String
    Sub New(ByVal UserName As String, ByVal Password As String, ByVal DomainName As String)
        _UserName = UserName
        _PassWord = Password
        _DomainName = DomainName
    End Sub

    Public Function GetFormsCredentials(ByRef authCookie As System.Net.Cookie, ByRef userName As String, ByRef password As String, ByRef authority As String) As Boolean Implements Microsoft.Reporting.WebForms.IReportServerCredentials.GetFormsCredentials
        Return False
    End Function

    Public ReadOnly Property ImpersonationUser() As System.Security.Principal.WindowsIdentity Implements Microsoft.Reporting.WebForms.IReportServerCredentials.ImpersonationUser
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property NetworkCredentials() As System.Net.ICredentials Implements Microsoft.Reporting.WebForms.IReportServerCredentials.NetworkCredentials
        Get
            Return New System.Net.NetworkCredential(_UserName, _PassWord)
        End Get
    End Property

End Class

