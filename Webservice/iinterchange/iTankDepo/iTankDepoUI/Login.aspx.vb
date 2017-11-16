Imports System.Data
Imports System.ServiceModel

'<ServiceContract()> _
Public Class Login
    Inherits Framebase

#Region "Page_Load1"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim objConfig As New Config
            'Login Page Version
            Dim strVersion As String
            strVersion = Config.pub_GetAppConfigValue("Version")
            Dim strTitle As String
            strTitle = Config.pub_GetAppConfigValue("Title")
            If strTitle <> Nothing Then
                Title = strTitle
            End If
            lblVersionNo.Text = strVersion
            txtPassword.Attributes.Add("onkeypress", "checkCapsLock(event);")
            'test from dev solution
            If GetQueryString("lo") IsNot Nothing Then
                'errcontent.Style.Add("display", "block")
                'errLabel.InnerText = "You have been successfully logged out."
            End If
            licensePane.Visible = False
            If GetQueryString("se") IsNot Nothing AndAlso GetQueryString("se") = 1 Then
                errcontent.Style.Add("display", "block")
                errLabel.InnerText = "Session Expired. Please re-login."
            End If
            If GetQueryString("se") IsNot Nothing AndAlso GetQueryString("se") = 4 Then
                licensePane.Visible = True
                spnError.InnerText = "Invalid License"
                spnErrorMessage.InnerText = "Please Contact System Administrator."
                loginpane.Visible = False
            End If
        End If
    End Sub
#End Region

#Region "btnSubmit_ServerClick"
    ''' <summary>
    ''' This event raised when login button clicked. This method authenticates user to login the application.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSubmit_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.ServerClick
        Try

            '        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myJsFn", "Myfunction(); ", True)

            'Dim strpass As String = "<script type='text/javascript'>Myfunction();</script>"

          

            Dim dsUser As New UserDataSet
            Dim strUserName As String = UCase(txtUserName.Text)
            Dim strPassword As String = String.Empty
            Dim objUser As New User

            Dim strPassword1 As String = txtPassword.Text
            Dim strpass As String = hdnPd.Value

            strPassword = objUser.pub_EncryptPassword(txtPassword.Text)

            'strPassword = objUser.pub_EncryptPassword(hdnPd.Value)

           
            dsUser = objUser.pub_GetUserDetailByUserName(strUserName)

            If Not Request.QueryString("Estimation_No") Is Nothing Then
                Try
                    If txtUserName.Text = ConfigurationManager.AppSettings("PhotoUserName") And txtPassword.Text = ConfigurationManager.AppSettings("PhotoPassword") Then
                        'Response.Redirect(String.Concat("PhotoViewer.aspx?Estimation_No=", Request.QueryString("Estimation_No").ToString))
                        Server.Transfer(String.Concat("PhotoViewer.aspx?Estimation_No=", Request.QueryString("Estimation_No").ToString))
                    Else
                        errLabel.InnerText = "Incorrect User Name or Password."
                        errLabel.Visible = True
                        errcontent.Style.Add("display", "block")
                    End If
                Catch ex As Exception
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
                    errLabel.InnerText = "There seems to be an issue with connectivity. Please contact the system administrator, if the problem presists."
                    errLabel.Visible = True
                    errcontent.Style.Add("display", "block")
                End Try

            End If

            'iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat("Rows Count ", dsUser.Tables(UserData._V_USER_DETAIL).Rows.Count))

            If dsUser.Tables(UserData._V_USER_DETAIL).Rows.Count = 0 Then
                errLabel.InnerText = "Incorrect User Name or Password."
                errLabel.Visible = True
                errcontent.Style.Add("display", "block")
                Exit Sub
            End If
            Dim drUser As DataRow = dsUser.Tables(UserData._V_USER_DETAIL).Rows(0)

            'iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat("Data Row Password ", drUser(UserData.PSSWRD)))

            If strPassword <> drUser(UserData.PSSWRD) Then
                errLabel.InnerText = "Incorrect User Name or Password."
                errLabel.Visible = True
                errcontent.Style.Add("display", "block")
                Exit Sub
            End If

            If Not CBool(drUser(UserData.ACTV_BT)) Then
                errLabel.InnerText = "Your account has been de-activated. Please contact administrator."
                errLabel.Visible = True
                errcontent.Style.Add("display", "block")
                Exit Sub
            End If

            'Create forms authentication ticket
            pvt_CreateTicket(strUserName)

            'Store client details
            pvt_CreateUserLog()

            Dim iCache As New iNCache
            Dim strSessionID As String = Session.SessionID
            Dim objCommonData As New CommonData

            If GetNCache(strSessionID) Is Nothing Then
                SetNCache(strSessionID, iCache) 'New Session created here after log out
            Else
                'Dim dsUserShare As New UserDataSet
                'dsUserShare = CType(RetrieveData("UserData"), UserDataSet)
                'If Not dsUserShare Is Nothing Then
                '    If Not strUserName = objCommonData.GetCurrentUserName() Then
                '        Response.Redirect("Default.aspx?share='true'")
                '        Exit Sub
                '    End If
                'End If
            End If
            CacheData("UserData", dsUser)
            Dim strTheme As String = Nothing
            If Not IsDBNull(drUser.Item(UserData.THM_NAM)) AndAlso drUser.Item(UserData.THM_NAM).ToString() <> "" Then
                strTheme = drUser.Item(UserData.THM_NAM)
            End If
            pub_CacheData("theme", strTheme)
            Dim objCommon As New CommonUI
            'If Not pvt_SessionValidation() Then
            '    licensePane.Visible = True
            '    spnError.InnerText = "Maximum Session limit Reached"
            '    spnErrorMessage.InnerText = "Maximum limit has been exceed with the number of sessions. Please contact System administrator or try again later."
            '    loginpane.Visible = False
            '    Exit Sub
            'End If

            If objCommon.ValidateLicense(CInt(objCommonData.GetDepotID())) Then
                Response.Redirect("Home.aspx?ts=" & DateTime.Now.ToFileTime())
            Else
                licensePane.Visible = True
                spnError.InnerText = "Invalid License"
                spnErrorMessage.InnerText = "Please Contact System Administrator."
                loginpane.Visible = False
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            errLabel.InnerText = "There seems to be an issue with connectivity. Please contact the system administrator, if the problem presists."
            errLabel.Visible = True
            errcontent.Style.Add("display", "block")
        End Try
    End Sub
#End Region




#Region "pvt_CreateTicket"
    ''' <summary>
    ''' This method used to create forms authentication ticket
    ''' </summary>
    ''' <param name="bv_strUserName"></param>
    ''' <remarks></remarks>
    Private Sub pvt_CreateTicket(ByVal bv_strUserName As String)
        Try
            Dim authTicket As New FormsAuthenticationTicket(bv_strUserName, True, 90)
            ' Encrypt the ticket.
            Dim encryptedTicket As String = FormsAuthentication.Encrypt(authTicket)
            ' Create a cookie and add the encrypted ticket to the cookie as data.
            Dim authCookie As New HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)

            ' Add the cookie to the outgoing cookies collection. 
            Response.Cookies.Add(authCookie)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateUserLog"
    ''' <summary>
    ''' This method used to store client access details in USER_LOG table.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_CreateUserLog()
        Try
            Dim strIpAddress As String = Request.ServerVariables("REMOTE_ADDR")
            Dim strSessionID As String = Session.SessionID
            Dim strUserAgent As String = Request.UserAgent
            Dim strBrowser As String = String.Concat(Request.Browser.Browser, " ", Request.Browser.MajorVersion, ".", Request.Browser.MinorVersion)
            Dim strScreenSize As String = hdnScreenSize.Value
            Dim objUser As New User
            Dim objcommon As New CommonData
            objUser.pub_CreateUserLog(txtUserName.Text, objcommon.GetCurrentDate, Nothing, strIpAddress, strSessionID, strUserAgent, strBrowser, strScreenSize)
            'objUser.pub_CreateUserLog(txtUserName.Text, strIpAddress, strSessionID, strUserAgent, strBrowser, strScreenSize)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/MD5.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Settings.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Login.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "pvt_SessionValidation"
    Private Function pvt_SessionValidation() As Boolean
        Try
            Dim arr_SessionIDs As ArrayList = CType(Application("arr_SessionIDs"), ArrayList)
            If Not arr_SessionIDs.Contains(Session.SessionID.ToString()) Then
                Dim objCommon As New CommonData()
                Dim intMaxSessions As String
                Dim blnKeyExist As Boolean = False
                intMaxSessions = objCommon.GetConfigSetting("006", blnKeyExist)
                If blnKeyExist Then
                    If arr_SessionIDs.Count + 1 > CInt(intMaxSessions) Then
                        Return False
                    End If
                End If
                arr_SessionIDs.Add(Session.SessionID.ToString())
                Application("arr_SessionIDs") = arr_SessionIDs
            End If
            Return True
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

End Class
