Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.IO
Imports System.Web.Script.Serialization

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ForgotPassword
    Inherits System.Web.Services.WebService

    <WebMethod(enableSession:=True)> _
     <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function ValidateEmailID(ByVal ForgotCredentials As ForgotCredentials) As ForgotPasswordModel



        Dim objUserDetail As New User
        Dim bolValidPss As Boolean
        Dim bolValidUsr As Boolean
        Dim forgotPss As New ForgotPassword_C
        Dim genRanPss As String
        Dim validateStatus As New ForgotPasswordModel
        Dim exception As New exceptionModel
        Dim sendEmail As New sendEmail
        'Dim forgotPss As New ForgotPassword_C
        'Dim objUserDetail As New User
        Dim blnUpdated As Boolean
        Dim dsjUserDetailData As New UserData
        Dim strEncrypPwd As String
        'Dim bolValidPss As Boolean
        'Dim genRanPss As String
        Dim strTemplatefolderPth As String

        Try
            bolValidUsr = objUserDetail.pub_ValidatePKUser(ForgotCredentials.bv_strUserName)
            bolValidPss = objUserDetail.pub_ValidateEmailID(Server.UrlDecode(ForgotCredentials.bv_strEmailId))
            'Dim bolValidUser As Boolean
            'bolValidUser = objUser.pub_ValidatePKUser(bv_strCode)
            If bolValidPss = False And bolValidUsr = False Then



                If objUserDetail.pub_ValidateUserIDAndEmailID(ForgotCredentials.bv_strUserName, ForgotCredentials.bv_strEmailId) Then
                    'bolValidPss = objUserDetail.pub_ValidateEmailID(Server.UrlDecode(bv_strEmailId))

                    genRanPss = forgotPss.GenHashPassword(ForgotCredentials.bv_strEmailId)
                    'Dim bolValidUser As Boolean
                    'bolValidUser = objUser.pub_ValidatePKUser(bv_strCode)

                    strEncrypPwd = objUserDetail.pub_EncryptPassword(genRanPss)
                    blnUpdated = objUserDetail.pub_ModifyUserPassword(ForgotCredentials.bv_strUserName, strEncrypPwd)

                    If blnUpdated Then
                        Dim strSubject As String
                        Dim strMessageBody As StringWriter
                        strSubject = "iDepo - Password Reset"
                        strTemplatefolderPth = ConfigurationManager.AppSettings("TemplatesFolder")
                        strMessageBody = forgotPss.pub_ProcessParameteres(Server.UrlDecode(ForgotCredentials.bv_strEmailId), strSubject, genRanPss, ForgotCredentials.bv_strUserName, strTemplatefolderPth)
                        'pub_SetCallbackReturnValue("UserEmailValid", "true")
                        'pub_SetCallbackStatus(True)
                        Dim objEmailhandler As New iInterchange.Framework.Common.iEmailHandler
                        objEmailhandler.pub_Send_Email(Server.UrlDecode(ForgotCredentials.bv_strEmailId), strSubject, strMessageBody.ToString())
                        validateStatus.validateEmailStatus = "Your password has been reset and sent to your email address " + ForgotCredentials.bv_strEmailId + ""
                        validateStatus.statusText = HttpContext.Current.Response.StatusDescription
                        validateStatus.stauscode = HttpContext.Current.Response.StatusCode

                        Return validateStatus

                        Exit Function


                    End If
                Else

                    validateStatus.validateEmailStatus = "Email ID doesn't match with this User Name"

                    Return validateStatus

                    Exit Function


                End If


            Else
                validateStatus.validateEmailStatus = "Invalid Username or Email Id"
                Return validateStatus
                Exit Function



            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                   ex.Message)

            validateStatus.validateEmailStatus = ex.Message
            validateStatus.statusText = HttpContext.Current.Response.StatusDescription
            validateStatus.stauscode = HttpContext.Current.Response.StatusCode
            Return validateStatus

            Exit Function



        End Try
        'Return New JavaScriptSerializer().Serialize(validateStatus)

        'Exit Function
        'Return "Hello World"
    End Function


    '<WebMethod(enableSession:=True)> _
    ' <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    'Public Function SendPassword(ByVal bv_strEmailId As String, ByVal bv_strUserName As String) As String

    '    Dim sendEmail As New sendEmail
    '    Dim forgotPss As New ForgotPassword_C
    '    Dim objUserDetail As New User
    '    Dim blnUpdated As Boolean
    '    Dim dsjUserDetailData As New UserData
    '    Dim strEncrypPwd As String
    '    Dim bolValidPss As Boolean
    '    Dim genRanPss As String
    '    Dim strTemplatefolderPth As String
    '    Try
    '        If objUserDetail.pub_ValidateUserIDAndEmailID(bv_strUserName, bv_strEmailId) Then
    '            bolValidPss = objUserDetail.pub_ValidateEmailID(Server.UrlDecode(bv_strEmailId))

    '            genRanPss = forgotPss.GenHashPassword(bv_strEmailId)
    '            'Dim bolValidUser As Boolean
    '            'bolValidUser = objUser.pub_ValidatePKUser(bv_strCode)

    '            strEncrypPwd = objUserDetail.pub_EncryptPassword(genRanPss)
    '            blnUpdated = objUserDetail.pub_ModifyUserPassword(bv_strUserName, strEncrypPwd)

    '            If blnUpdated Then
    '                Dim strSubject As String
    '                Dim strMessageBody As StringWriter
    '                strSubject = "iDepo - Password Reset"
    '                strTemplatefolderPth = ConfigurationManager.AppSettings("TemplatesFolder")
    '                strMessageBody = forgotPss.pub_ProcessParameteres(Server.UrlDecode(bv_strEmailId), strSubject, genRanPss, bv_strUserName, strTemplatefolderPth)
    '                'pub_SetCallbackReturnValue("UserEmailValid", "true")
    '                'pub_SetCallbackStatus(True)
    '                Dim objEmailhandler As New iInterchange.Framework.Common.iEmailHandler
    '                objEmailhandler.pub_Send_Email(Server.UrlDecode(bv_strEmailId), strSubject, strMessageBody.ToString())
    '                sendEmail.emailStatus = "Your password has been reset and sent to your email address " + bv_strEmailId + ""
    '                sendEmail.statusText = HttpContext.Current.Response.StatusDescription
    '                sendEmail.stauscode = HttpContext.Current.Response.StatusCode

    '                Return New JavaScriptSerializer().Serialize(sendEmail)

    '                Exit Function


    '            End If
    '        Else

    '            sendEmail.emailStatus = "Email ID doesn't match with this User Name"

    '            'Return New JavaScriptSerializer().Serialize(sendEmail)

    '            'Exit Function
    '            'pub_SetCallbackReturnValue("UserEmailValid", "false")
    '            'pub_SetCallbackStatus(True)
    '        End If
    '    Catch ex As Exception
    '        'pub_SetCallbackStatus(False)
    '        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
    '                                ex.Message)
    '        sendEmail.emailStatus = ex.Message
    '        sendEmail.statusText = HttpContext.Current.Response.StatusDescription
    '        sendEmail.stauscode = HttpContext.Current.Response.StatusCode
    '        'Return New JavaScriptSerializer().Serialize(sendEmail)

    '        'Exit Function
    '    End Try
    '    Return New JavaScriptSerializer().Serialize(sendEmail)

    '    'Exit Function
    'End Function

End Class