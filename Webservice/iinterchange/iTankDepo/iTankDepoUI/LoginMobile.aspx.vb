Imports System.Data
Imports System.ServiceModel
Imports System.Web.Script.Serialization
Imports System.Security.Cryptography

Partial Class LoginMobile
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
            Dim quetsr As String = Request.QueryString("Password")
        End If
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



#Region "LoginReturn"

    Public Function LoginReturn(ByVal Username As String, ByVal Password As String) As Message
        Try
            Dim dsUser As New UserDataSet
            Dim strUserName As String = UCase(Username)
            Dim strPassword As String = String.Empty
            Dim objUser As New User

            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim javaScript As String = "<script type='text/javascript'>encrptPasswordForMobile();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

            strPassword = objUser.pub_EncryptPassword(Password)

            dsUser = objUser.pub_GetUserDetailByUserName(strUserName)

            Dim hasher As MD5 = MD5.Create()

            Dim dbytes As Byte() =
             hasher.ComputeHash(Encoding.UTF8.GetBytes(Password))

            ' sb to create string from bytes
            Dim sBuilder As New StringBuilder()

            ' convert byte data to hex string
            For n As Integer = 0 To dbytes.Length - 1
                sBuilder.Append(dbytes(n).ToString("X2"))
            Next n

            Dim dd As String = sBuilder.ToString()

           


            Dim Rsmsg As New Message

            If dsUser.Tables(UserData._V_USER_DETAIL).Rows.Count = 0 Then

                Rsmsg.ResponseText = "Incorrect User Name or Password."


                Return Rsmsg

                Exit Function
            End If

            Dim drUser As DataRow = dsUser.Tables(UserData._V_USER_DETAIL).Rows(0)

            If strPassword <> drUser(UserData.PSSWRD) Then

                Rsmsg.ResponseText = "Incorrect User Name or Password."
                Return Rsmsg

                Exit Function
            End If

            If Not CBool(drUser(UserData.ACTV_BT)) Then

                Rsmsg.ResponseText = "Your account has been de-activated. Please contact administrator."
                Return Rsmsg

                Exit Function
            End If

            pvt_CreateTicket(strUserName)

            pvt_CreateUserLog(strUserName)

            Dim iCache As New iNCache
            Dim strSessionID As String = Session.SessionID
            Dim objCommonData As New CommonData

            If GetNCache(strSessionID) Is Nothing Then
                SetNCache(strSessionID, iCache) 'New Session created here after log out
            Else

            End If
            CacheData("UserData", dsUser)


            Dim objCommon As New CommonUI

            If objCommon.ValidateLicense(CInt(objCommonData.GetDepotID())) Then
                Rsmsg.ResponseText = "Success"
                Rsmsg.statusText = HttpContext.Current.Response.StatusDescription
                Rsmsg.stauscode = HttpContext.Current.Response.StatusCode

                Return Rsmsg
                Exit Function
            Else

                Rsmsg.ResponseText = "Invalid License. Please Contact System Administrator."

                Return Rsmsg
                Exit Function

            End If
        Catch ex As Exception
            Dim Rsmsg As New Message
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)

            Rsmsg.ResponseText = ex.Message
            Rsmsg.statusText = HttpContext.Current.Response.StatusDescription
            Rsmsg.stauscode = HttpContext.Current.Response.StatusCode


            Return Rsmsg
            Exit Function
        End Try
    End Function
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
    Private Sub pvt_CreateUserLog(ByVal bv_strUserName As String)
        Try
            Dim strIpAddress As String = Request.ServerVariables("REMOTE_ADDR")
            Dim strSessionID As String = Session.SessionID
            Dim strUserAgent As String = Request.UserAgent
            Dim strBrowser As String = String.Concat(Request.Browser.Browser, " ", Request.Browser.MajorVersion, ".", Request.Browser.MinorVersion)
            Dim strScreenSize As String = ""
            Dim objUser As New User
            Dim objcommon As New CommonData
            objUser.pub_CreateUserLog(bv_strUserName, objcommon.GetCurrentDate, Nothing, strIpAddress, strSessionID, strUserAgent, strBrowser, strScreenSize)
            'objUser.pub_CreateUserLog(txtUserName.Text, strIpAddress, strSessionID, strUserAgent, strBrowser, strScreenSize)
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
