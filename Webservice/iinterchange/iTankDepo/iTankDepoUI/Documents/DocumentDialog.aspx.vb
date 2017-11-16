Option Strict On
Imports System.Data

Partial Class DocumentDialog
    Inherits Framebase

    Dim dsDocument As CommonUIDataSet
    Dim intDocumentId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim objDocument As New CommonUI
                Dim strDocumentName As String
                Dim intTemplateID As Integer
                Dim blnSendMail As Boolean

                If GetQueryString("type").ToString.ToLower = "report" Then
                    strDocumentName = GetQueryString("re").ToString
                    intTemplateID = CInt(GetQueryString("RPRT_ID"))
                    ddlTemplates.Visible = False
                Else
                    ddlTemplates.Visible = True
                    intDocumentId = CInt(GetQueryString("DCMNT_ID"))

                    dsDocument = New CommonUIDataSet

                    dsDocument = objDocument.pub_GetDocumentByDocumentID(intDocumentId)
                    With dsDocument.Tables(CommonUIData._DOCUMENT_TEMPLATE).Rows(0)
                        strDocumentName = CStr(.Item(CommonUIData.DCMNT_NAM))
                        'intTemplateID = CInt(.Item(CommonUIData.DCMNT_TMPLT_ID))

                        If GetQueryString("CUSTOMER_TYPE") <> Nothing AndAlso GetQueryString("CUSTOMER_TYPE").ToUpper() = "AGENT" Then
                            intTemplateID = CInt(GetQueryString("TEMPLATEID"))
                        Else
                            intTemplateID = CInt(.Item(CommonUIData.DCMNT_TMPLT_ID))
                        End If


                        blnSendMail = CBool(.Item(CommonUIData.SND_ML_BT))
                    End With

                    ddlTemplates.DataSource = dsDocument.Tables(CommonUIData._DOCUMENT_TEMPLATE)
                    ddlTemplates.DataTextField = CommonUIData.TMPLT_NAM
                    ddlTemplates.DataValueField = CommonUIData.DCMNT_TMPLT_ID

                    ddlTemplates.DataBind()
                    ddlTemplates.Attributes.Add("onchange", String.Concat("changeTemplate(this,'", _
                                                                    dsDocument.Tables(CommonUIData._DOCUMENT_TEMPLATE).Rows(0).Item(CommonUIData.TMPLT_NAM), _
                                                                    "','", AttachParameterCollections(), "');"))

                    ddlTemplates.SelectedIndex = 0
                End If
                lblReportHeader.Text = strDocumentName & " Report"
                LoadDefaultReport(intTemplateID, strDocumentName)
                If blnSendMail Then
                    divEmail.Visible = True
                Else
                    divEmail.Visible = False
                End If

                If GetQueryString("type") = "email" Then
                    divEmail.Visible = False
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                            ex.Message)
        End Try
    End Sub

    Private Sub LoadDefaultReport(ByVal bv_TemplateID As Integer, ByVal bv_strDocumentName As String)
        Try
            Dim sbrLoadReport As New StringBuilder
            sbrLoadReport.Append("el('fmReport').src='DocumentViewer.aspx?TMPLT_ID=")
            sbrLoadReport.Append(bv_TemplateID)
            sbrLoadReport.Append("&DCMNT_NAM=")
            sbrLoadReport.Append(bv_strDocumentName)
            sbrLoadReport.Append(AttachParameterCollections())
            sbrLoadReport.Append("';")
            ClientScript.RegisterStartupScript(GetType(String), "reportload", sbrLoadReport.ToString, True)
            sbrLoadReport = Nothing
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

    Private Function AttachParameterCollections() As String
        Try
            Dim sbrQueryString As New StringBuilder
            For Each strTemp As String In Request.QueryString.AllKeys
                If Not strTemp Is Nothing Then
                    sbrQueryString.Append("&")
                    sbrQueryString.Append(strTemp)
                    sbrQueryString.Append("=")
                    If strTemp <> "wfdata" Then
                        sbrQueryString.Append(GetQueryString(strTemp))
                    Else
                        sbrQueryString.Append(Server.UrlEncode(GetQueryString(strTemp)))
                    End If
                End If
            Next
            Return sbrQueryString.ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

End Class
