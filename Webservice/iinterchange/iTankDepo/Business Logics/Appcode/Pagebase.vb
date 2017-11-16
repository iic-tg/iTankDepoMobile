Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Net.Mail
Imports iInterchange.Framework.Common
Imports iInterchange.Framework.UI
Imports System.Web
Imports iInterchange.WebControls.v4.Data
Imports iInterchange.WebControls.v4.Navigation
Imports iInterchange.Framework
Imports System.Configuration
Imports System.Text
Imports System.Web.UI.HtmlControls

''' <summary>
''' This class is base class for all the workspace pages.
''' </summary>
''' <remarks></remarks>
Public Class Pagebase
    Inherits iPageBase

#Region "Declarations"
    Private sbrClientScript As New StringBuilder
    Private arlGridIDs As ArrayList
#End Region

#Region "Page_Load"
    ''' <summary>
    ''' This event will be fired on every page load for workspace pages alone
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsCallback Or Page.Request.QueryString("callbacktype") <> "fnGetData" Or pub_FramePage() Then
            Else
                pvt_CheckAccess()
                pvt_ClearSession()
            End If
            If Not Page.Request.Url.ToString.IndexOf("ForgotPassword.aspx") > 0 Then
                pvt_SessionCheck()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_ClearLockData"
    ''' <summary>
    ''' This method is used to clear the locked data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_ClearLockData()
        Try
            Dim objCommonData As New CommonData()
            Dim strSessionID As String = GetSessionID()
            objCommonData.ClearLockData(strSessionID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CheckAccess"
    ''' <summary>
    ''' This method used for code protection
    ''' </summary>
    ''' <remarks>Presently disabled</remarks>
    Private Shared Sub pvt_CheckAccess()
        'If Not HttpContext.Current.Request.ServerVariables("SERVER_NAME").ToLower = "180.179.104.17" Then
        '    HttpContext.Current.Response.Status = "403 Access Forbidden"
        '    HttpContext.Current.Session.Abandon()
        '    HttpContext.Current.Response.End()
        'End If
    End Sub
#End Region

#Region "pub_SetGridChanges"
    ''' <summary>
    ''' This method used set the grid changes for every tab
    ''' </summary>
    ''' <param name="bv_objFlexGrid">Denotes Grid object</param>
    ''' <param name="br_objTabPage">Denotes Tab page object</param>
    ''' <remarks></remarks>
    Public Sub pub_SetGridChanges(ByRef bv_objFlexGrid As iFlexGrid, ByRef br_objTabPage As TabPage)
        If arlGridIDs Is Nothing Then
            arlGridIDs = New ArrayList
        End If
        arlGridIDs.Add(String.Concat(br_objTabPage.ClientID, ";", bv_objFlexGrid.ClientID))
    End Sub

    ''' <summary>
    ''' This method used set the grid changes for every tab
    ''' </summary>
    ''' <param name="bv_objFlexGrid">Denotes Grid object</param>
    ''' <param name="br_strTabPageClientID">Denotes Tab page name</param>
    ''' <remarks></remarks>
    Public Sub pub_SetGridChanges(ByRef bv_objFlexGrid As iFlexGrid, ByRef br_strTabPageClientID As String)
        If arlGridIDs Is Nothing Then
            arlGridIDs = New ArrayList
        End If
        arlGridIDs.Add(String.Concat(br_strTabPageClientID, ";", bv_objFlexGrid.ClientID))
    End Sub
#End Region

#Region "pvt_SessionCheck"
    ''' <summary>
    ''' This method used to check the session on every request
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_SessionCheck()
        If RetrieveData("UserData") Is Nothing Then
            HttpContext.Current.Response.AddHeader("sessiontimedout", "true")
            CommonWeb.pub_SessionExpired()
        End If
    End Sub
#End Region

#Region "pvt_ClearSession"
    ''' <summary>
    ''' This method used to clear session variables
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub pvt_ClearSession()
        Dim dsUser As DataSet
        Dim strTableName As String
        Dim strFuncVals As String
        Dim strItemNo As String
        Dim strPageMode As String
        Dim strPageTitle As String
        Dim intListColCount As Integer
        Dim dtListData As DataTable
        Dim dtDbListData As DataTable
        Dim dsReport As DataSet
        Dim iCache As iNCache

        Dim strActivityId As String = Request.QueryString("activityid")

        dsUser = RetrieveData("UserData")
        intListColCount = CInt(RetrieveData("listcolcount" + strActivityId))
        strTableName = CStr(RetrieveData("tablename" + strActivityId))
        strPageTitle = CStr(RetrieveData("pagetitle" + strActivityId))
        strFuncVals = CStr(RetrieveData("funcvals" + strActivityId))
        strItemNo = CStr(RetrieveData("itemno" + strActivityId))
        strPageMode = CStr(RetrieveData("pagemode" + strActivityId))
        dtListData = RetrieveData("listdata" + strActivityId)
        dtDbListData = RetrieveData("dblistdata" + strActivityId)
        '   iCache = GetNCache(Session.SessionID)
        Dim blnCreateRight As Boolean = RetrieveData("createright" + strActivityId)
        Dim strReportPages As String = RetrieveData("REPORT_PAGES" + strActivityId)
        dsReport = RetrieveData("REPORT_DATASET" + strActivityId)


        'Session.RemoveAll()

        'iCache.Clear()

        'SetNCache(Session.SessionID, iCache)

        CacheData("createright" + strActivityId, blnCreateRight)
        CacheData("UserData", dsUser)
        CacheData("listcolcount" + strActivityId, intListColCount)
        CacheData("tablename" + strActivityId, strTableName)
        CacheData("pagetitle" + strActivityId, strPageTitle)
        CacheData("itemno" + strActivityId, strItemNo)
        CacheData("pagemode" + strActivityId, strPageMode)
        CacheData("funcvals" + strActivityId, strFuncVals)
        CacheData("listdata" + strActivityId, dtListData)
        CacheData("dblistdata" + strActivityId, dtDbListData)
        CacheData("REPORT_PAGES" + strActivityId, strReportPages)
        CacheData("REPORT_DATASET" + strActivityId, dsReport)
    End Sub
#End Region

#Region "Page_LoadComplete"
    ''' <summary>
    ''' Page load complete event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            If Page.IsCallback = False Then
                sbrClientScript.Append(vbCrLf)
                sbrClientScript.Append(CommonWeb.RenderCommonScripts)
                ClientScript.RegisterStartupScript(GetType(System.String), "Pagebase", sbrClientScript.ToString)
                ClientScript.RegisterStartupScript(GetType(System.String), "roundedcorner", "$('.btncorner').corner();", True)
                If Page.Request.Url.ToString().ToUpper().IndexOf("DEFAULT.ASPX") = -1 AndAlso _
                      Page.Request.Url.ToString().ToUpper().IndexOf("LOGIN.ASPX") = -1 AndAlso _
                    Page.Request.Url.ToString().ToUpper().IndexOf("LOGOUT.ASPX") = -1 AndAlso _
                    Page.Request.Url.ToString().ToUpper().IndexOf("PHOTOUPLOAD.ASPX") = -1 AndAlso _
                    Page.Request.Url.ToString().ToUpper().IndexOf("FORGOTPASSWORD.ASPX") = -1 AndAlso _
                      Page.Request.Url.ToString().ToUpper().IndexOf("ALERTS.ASPX") = -1 Then
                    ClientScript.RegisterStartupScript(GetType(System.String), "checkpagelocation", "checkPageLocation();", True)
                    ClientScript.RegisterStartupScript(GetType(System.String), "loadInputControls", "loadInputControls();", True)
                End If

                Dim metaDescription As New HtmlMeta()
                metaDescription.HttpEquiv = "X-UA-Compatible"
                metaDescription.Content = "IE=8"
                Page.Header.Controls.AddAt(0, metaDescription)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "ConvertDBNulltoString"
    ''' <summary>
    ''' This method is used to convert null value to string
    ''' </summary>
    ''' <param name="bv_objDBValue">Denotes database value</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertDBNulltoString(ByVal bv_objDBValue As Object) As String
        If IsDBNull(bv_objDBValue) = True Then
            Return ""
        End If
        Return CStr(bv_objDBValue)
    End Function
#End Region

#Region "GetClientIPAddress"
    ''' <summary>
    ''' This method is used to get client ip adress for user tracking purpose
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetClientIPAddress() As String
        Return Request.UserHostAddress.ToString()
    End Function
#End Region

#Region "GetSessionID"
    ''' <summary>
    ''' This method used to get the current browsers session id
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetSessionID() As String
        Return Session.SessionID
    End Function
#End Region

#Region "GenerateLockMessage"
    ''' <summary>
    ''' This method used to build locking message
    ''' </summary>
    ''' <param name="bv_strUserName">Denotes User Name</param>
    ''' <param name="bv_strIPAddress">Denotes IP Address</param>
    ''' <param name="bv_strSessionID">Denotes Session ID</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GenerateLockMessage(ByVal bv_strUserName As String, ByVal bv_strIPAddress As String, ByVal bv_strSessionID As String) As String
        Dim objCommon As New CommonData()
        Dim strCurrentIPAddress As String = GetClientIPAddress()
        Dim strCurrentUserName As String = objCommon.GetCurrentUserName()
        Dim strCurrentSessionID As String = GetSessionID()
        Dim strMsg As String = ""

        If bv_strSessionID <> strCurrentSessionID And bv_strIPAddress = strCurrentIPAddress Then
            strMsg = String.Concat("This record is locked for editing by ", "<b>", bv_strUserName, "</b>", " in different session.")
        ElseIf bv_strIPAddress <> strCurrentIPAddress Then
            strMsg = String.Concat("This record is locked for editing by ", "<b>", bv_strUserName, "</b>", " in different place.")
        Else
            strMsg = String.Concat("This record is locked for editing by ", "<b>", bv_strUserName, "</b>", ".")
        End If
        Return strMsg
    End Function
#End Region

    '#Region "Sending Email using chillkat component with single file attachment"

    ' 'Chilkat Email Method Start
    'Public Sub pub_Send_Email(ByVal bv_strEmailIds As String, ByVal bv_strSubject As String, _
    '                            ByVal bv_strBody As String, ByVal bv_strAttachmentPath As String)

    '    Dim objChilkat As New Chilkat.MailMan
    '    Try
    '        Dim Status As Boolean
    '        Dim strStartTLS As String = ""
    '        objChilkat.UnlockComponent("UiinterchMAILQ_FmPBEfzL8SkF")
    '        objChilkat.SmtpHost = pvt_GetSmtpHost()
    '        objChilkat.SmtpPort = ConfigurationManager.AppSettings("SmtpPortNo")
    '        Dim email As New Chilkat.Email
    '        strStartTLS = ConfigurationManager.AppSettings("StartTLS")

    '        If strStartTLS = True Then
    '            objChilkat.StartTLS = True
    '        End If
    '        email.From = ConfigurationManager.AppSettings("FromEmail")
    '        If ConfigurationManager.AppSettings("SmtpAuthEnabled") = "true" Then
    '            objChilkat.SmtpUsername = ConfigurationManager.AppSettings("SmtpUserName")
    '            objChilkat.SmtpPassword = ConfigurationManager.AppSettings("SmtpPassword")
    '        End If
    '        Dim strEmailIds As String()

    '        Dim macTo As New System.Net.Mail.MailAddressCollection
    '        strEmailIds = bv_strEmailIds.Split(CChar(","))

    '        If strEmailIds.Length = 0 Then
    '            strEmailIds(0) = bv_strEmailIds
    '            email.AddTo(bv_strEmailIds, bv_strEmailIds)
    '        End If

    '        For Each strEmail As String In strEmailIds
    '            email.AddTo(strEmail, strEmail)
    '        Next

    '        email.Subject = bv_strSubject

    '        'Adding Foot note
    '        Dim sbrFootNote As New Text.StringBuilder
    '        sbrFootNote.Append("<DIV style=""font-family:Arial;font-size:10pt"">")
    '        sbrFootNote.Append("<DIV>")
    '        sbrFootNote.Append(bv_strBody)
    '        sbrFootNote.Append("</DIV>")
    '        sbrFootNote.Append("<BR/>")
    '        sbrFootNote.Append("<BR/>")
    '        sbrFootNote.Append("<HR/>")
    '        sbrFootNote.Append("<DIV>")
    '        sbrFootNote.Append("This message was sent to you by <B>")
    '        sbrFootNote.Append(ConfigurationManager.AppSettings("appnam"))
    '        sbrFootNote.Append("</B>.  ")
    '        sbrFootNote.Append("Please do not respond to this message as it is automatically generated and ")
    '        sbrFootNote.Append("is for information purposes only.")
    '        sbrFootNote.Append("</DIV>")
    '        sbrFootNote.Append("</DIV>")
    '        email.SetHtmlBody(sbrFootNote.ToString())

    '        email.AddFileAttachment(bv_strAttachmentPath)

    '        Status = objChilkat.SendEmail(email)

    '        If Status = False Then
    '            iErrorHandler.pub_WriteErrorLog("Chilkat", _
    '                    objChilkat.LastErrorText, "Mail was not sent .objChilkat.SendEmail method returns FALSE")
    '        End If
    '    Catch ex As Exception
    '        iErrorHandler.pub_WriteErrorLog("pub_Send_Email", _
    '                                objChilkat.LastErrorText, ex.Message)
    '    End Try
    'End Sub
    ''Chilkat Email Method End

    '    'SMTP Email Method start
    '    Public Function pub_Send_Email(ByVal bv_strToAddress As String, ByVal bv_strSubject As String, ByVal strBody As String, ByVal bv_strFileName As String) As Boolean
    '        Dim strSMTPMailServer As String = ConfigurationManager.AppSettings("SmtpMailServer")
    '        Dim strSmtpAuthEnabled As String = ConfigurationManager.AppSettings("SmtpAuthEnabled")
    '        Dim strFromEmailID As String = ConfigurationManager.AppSettings("FromEmail")
    '        Dim portNumber As Integer
    '        If ConfigurationManager.AppSettings("SmtpPortNo") IsNot Nothing Then
    '            portNumber = CInt(ConfigurationManager.AppSettings("SmtpPortNo"))
    '        End If
    '        Dim strToEmailIds As String()
    '        Dim password As String = String.Empty
    '        Try

    '            If strSmtpAuthEnabled = "true" Then
    '                password = ConfigurationManager.AppSettings("SmtpPassword")
    '            End If

    '            Dim enableSSL As Boolean = False

    '            Dim mail As MailMessage = New MailMessage

    '            strToEmailIds = bv_strToAddress.Split(CChar(","))

    '            If strToEmailIds.Length = 0 Then
    '                strToEmailIds(0) = bv_strToAddress
    '            End If

    '            For Each strToEmail As String In strToEmailIds
    '                mail.To.Add(strToEmail)
    '            Next

    '            mail.From = New MailAddress(strFromEmailID)
    '            mail.Subject = bv_strSubject
    '            mail.Body = strBody
    '            mail.IsBodyHtml = True
    '            If bv_strFileName <> Nothing Then
    '                mail.Attachments.Add(New Attachment(bv_strFileName))
    '            End If

    '            Dim smtp As SmtpClient = New SmtpClient(strSMTPMailServer, portNumber)
    '            smtp.Credentials = New NetworkCredential(strFromEmailID, password)
    '            smtp.EnableSsl = enableSSL
    '            smtp.Send(mail)

    '            Return True
    '        Catch ex As Exception
    '            Throw ex
    '            Return False
    '        Finally
    '        End Try
    '    End Function
    '    'SMTP Email Method End
    '#End Region
#Region "Pub_Send_Email"

    'SMTP Email Method start
    Public Function pub_Send_Email(ByVal bv_strToAddress As String, ByVal bv_strSubject As String, ByVal strBody As String, ByVal bv_strFileName As String) As Boolean
        Dim strSMTPMailServer As String = ConfigurationManager.AppSettings("SmtpMailServer")
        Dim strSmtpAuthEnabled As String = ConfigurationManager.AppSettings("SmtpAuthEnabled")
        Dim strFromEmailID As String = ConfigurationManager.AppSettings("FromEmail")
        Dim portNumber As Integer
        If ConfigurationManager.AppSettings("SmtpPortNo") IsNot Nothing Then
            portNumber = CInt(ConfigurationManager.AppSettings("SmtpPortNo"))
        End If
        Dim strToEmailIds As String()
        Dim password As String = String.Empty
        Try

            If strSmtpAuthEnabled = "true" Then
                password = ConfigurationManager.AppSettings("SmtpPassword")

            End If

            Dim enableSSL As Boolean = False
            Dim mail As MailMessage = New MailMessage

            strToEmailIds = bv_strToAddress.Split(CChar(","))

            If strToEmailIds.Length = 0 Then
                strToEmailIds(0) = bv_strToAddress
            End If

            For Each strToEmail As String In strToEmailIds
                mail.To.Add(strToEmail)
            Next

            mail.From = New MailAddress(strFromEmailID)
            mail.Subject = bv_strSubject
            mail.Body = strBody
            mail.IsBodyHtml = True
            If bv_strFileName <> Nothing Then
                mail.Attachments.Add(New Attachment(bv_strFileName))
            End If

            Dim smtp As SmtpClient = New SmtpClient(strSMTPMailServer, portNumber)
            smtp.Credentials = New NetworkCredential(strFromEmailID, password)
            smtp.EnableSsl = enableSSL
            smtp.Send(mail)

            Return True
        Catch ex As Exception
            Throw ex
            Return False
        Finally
        End Try
    End Function
    'SMTP Email Method End
#End Region

#Region "Getting SMTP Host"

    Private Function pvt_GetSmtpHost() As String
        Return ConfigurationManager.AppSettings("SmtpMailServer")
    End Function

#End Region

End Class


