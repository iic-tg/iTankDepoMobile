Option Strict On
Imports System.Data

Partial Class ReportDialog
    Inherits Pagebase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim strReportPath As String
                strReportPath = GetQueryString("reportpath")
                
                LoadReport(strReportPath)

            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

    Private Sub LoadReport(ByVal bv_strReportPath As String)
        Try
            Dim sbrLoadReport As New StringBuilder
            sbrLoadReport.Append("document.getElementById('ReportViewerFrame').src='ReportViewer.aspx?")
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
                    sbrQueryString.Append(GetQueryString(strTemp))
                End If
            Next
            Return sbrQueryString.ToString()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex.Message)
            Return Nothing
        End Try
    End Function

    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Reports/ReportDialog.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

End Class
