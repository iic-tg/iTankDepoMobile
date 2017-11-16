Option Strict On
Imports System.Xml

Partial Class Dashboard
    Inherits Framebase
    Shared Container As System.Net.CookieContainer = Nothing
    Shared DashboardApi As New DashboardApi.DashboardApi
    Dim strReportList As String = ""
    Dim strSessionToken As String = ""
    Dim strToken As String = ""
    Dim username As String = ConfigurationManager.AppSettings("UserName")
    Dim password As String = ConfigurationManager.AppSettings("Password")
    Dim Companyshortname As String = ConfigurationManager.AppSettings("CompanyShortName")
    Dim ReportID As Integer = ConfigurationManager.AppSettings("ReportID")
    Dim CompanyID As Integer = ConfigurationManager.AppSettings("CompanyID")
    Dim strUsername As String = ""
    Dim strRoleId As String = ""
    '  Dim objUserRole As New UserDetail
    Dim objCommonData As New CommonData
    Dim objRoleRight As New RoleRight
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            strSessionToken = DashboardApi.ivnInitializeSession(Companyshortname, username, "password", 0, 1, 1, "en", "")
            Dim Doc As XmlDocument = New XmlDocument()
            Doc.LoadXml(strSessionToken)
            Dim XmlNodeList As XmlNodeList = Doc.SelectNodes("//ResponseMessage/Session")
            For Each Node As XmlNode In XmlNodeList
                strToken = Node.Attributes("Token").Value
            Next
            strReportList = DashboardApi.ivnGetReportDetails(strToken, CompanyID, "")
            DashboardApi.ivnSetUIElementStatus(strToken, CompanyID, 1, True)
            DashboardApi.ivnSetUIElementStatus(strToken, CompanyID, 2, False)
            'Max Tool
            DashboardApi.ivnSetUIElementStatus(strToken, CompanyID, 3, True)
            Dim strToolXML As New StringBuilder
            Dim ToolEnable As String = "0"
            strToolXML.Append("<?xml version='1.0' encoding='utf-8'?>")
            strToolXML.Append("<WidgetMenu>")
            strToolXML.Append("<widgetlayouts enable='0' />")
            strToolXML.Append("<viewastable enable='0' />")
            strToolXML.Append("<viewaschart enable='0' />")
            strToolXML.Append("<showcolumnlist enable='0' />")
            strToolXML.Append("<editmode enable='0' />")
            strToolXML.Append("<refresh enable='1' />")
            strToolXML.Append("<maximizewidget enable='1' />")
            strToolXML.Append("<export enable='1' pdf='1' png='1' jpeg='1' csv='1' rtf='1' xls='1' xlsx='1' />")
            strToolXML.Append("<edit enable='0' />")
            strToolXML.Append("<info enable='0' />")
            strToolXML.Append("<closewidget enable='0'/>")
            strToolXML.Append("</WidgetMenu>")
            DashboardApi.ivnSetWidgetMenus(strToken, CompanyID, ReportID, strToolXML.ToString)

            Dim dashURL As String = DashboardApi.ivnShowReport(strToken, CompanyID, ReportID, "iDepo", "")
            Dim strDash As String = dashURL.Substring(dashURL.IndexOf("src"), dashURL.IndexOf("frameborder"))
            ReportFrame.Attributes.Add("src", strDash.Replace("src=", "").Replace("framebor", "").Replace("'", "").Trim())
        End If
    End Sub


End Class

