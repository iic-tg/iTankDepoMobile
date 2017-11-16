Option Strict On
Imports System.IO
Imports System.Xml

Partial Class ForgotPassword
    Inherits Pagebase

#Region "pvt_ValidateUserName()"
    Private Sub pvt_ValidateUserName(ByVal bv_strCode As String)
        Try
            Dim objUser As New User
            Dim bolValid As Boolean
            bolValid = objUser.pub_ValidatePKUser(bv_strCode)
            If bolValid Then
                pub_SetCallbackReturnValue("pkValid", "true")
            Else
                pub_SetCallbackReturnValue("pkValid", "false")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateEmailId"
    Private Sub pvt_ValidateEmailId(ByVal bv_strEmailId As String)
        Try
            Dim objUser As New User
            Dim bolValid As Boolean
            bolValid = objUser.pub_ValidateEmailID(Server.UrlDecode(bv_strEmailId))
            If bolValid = False Then
                pub_SetCallbackReturnValue("pkValid", "true")
            Else
                pub_SetCallbackReturnValue("pkValid", "false")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_SendPassword"
    Protected Sub pvt_SendPassword(ByVal bv_strPwd As String, ByVal bv_strHashPwd As String, ByVal bv_strEmailId As String, ByVal bv_strUserName As String)
        Dim objUserDetail As New User
        Dim blnUpdated As Boolean
        Dim dsjUserDetailData As New UserData
        Dim strEncrypPwd As String
        Dim strTemplatefolderPth As String
        Try
            If objUserDetail.pub_ValidateUserIDAndEmailID(bv_strUserName, bv_strEmailId) Then
                strEncrypPwd = objUserDetail.pub_EncryptPassword(bv_strHashPwd)
                blnUpdated = objUserDetail.pub_ModifyUserPassword(bv_strUserName, strEncrypPwd)

                If blnUpdated Then
                    Dim strSubject As String
                    Dim strMessageBody As StringWriter
                    strSubject = "iDepo - Password Reset"
                    strTemplatefolderPth = ConfigurationManager.AppSettings("TemplatesFolder")
                    strMessageBody = pub_ProcessParameteres(Server.UrlDecode(bv_strEmailId), strSubject, bv_strPwd, bv_strUserName, strTemplatefolderPth)
                    pub_SetCallbackReturnValue("UserEmailValid", "true")
                    pub_SetCallbackStatus(True)
                    Dim objEmailhandler As New iInterchange.Framework.Common.iEmailHandler
                    objEmailhandler.pub_Send_Email(Server.UrlDecode(bv_strEmailId), strSubject, strMessageBody.ToString())

                End If
            Else
                pub_SetCallbackReturnValue("UserEmailValid", "false")
                pub_SetCallbackStatus(True)
            End If
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                    ex.Message)
        End Try
    End Sub
#End Region

#Region "Processing  parameters and construct xslt template - Forget Password Page"
    Private Function pub_ProcessParameteres(ByVal bv_strEmailIds As String, ByVal bv_strSubject As String, _
                                       ByVal bv_strPwd As String, ByVal bv_strUsername As String, ByVal bv_strTemplateFolderPath As String) As StringWriter
        Try
            Dim objXSLTrans As New Xsl.XslCompiledTransform
            Dim objXMLDoc As New XmlDocument
            Dim swFileWriter As New System.IO.StringWriter()

            Dim strDomainPath As String = AppDomain.CurrentDomain.BaseDirectory.ToString
            Dim XSLargsList As New Xsl.XsltArgumentList
            Dim strTemplateFolder As String


            strTemplateFolder = String.Concat(bv_strTemplateFolderPath, "ForgotPassword.xslt")
            XSLargsList.AddParam("password", "", bv_strPwd)
            XSLargsList.AddParam("username", "", bv_strUsername)
            objXSLTrans.Load(strTemplateFolder)
            objXSLTrans.Transform(objXMLDoc, XSLargsList, swFileWriter)

            objXMLDoc = Nothing
            objXSLTrans = Nothing

            Return swFileWriter
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                   ex.Message)
        End Try
       
    End Function
#End Region

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "ValidateUserName"
                pvt_ValidateUserName(e.GetCallbackValue("UserName"))
            Case "ValidateEmailId"
                pvt_ValidateEmailId(e.GetCallbackValue("EmailId"))
            Case "SendPassword"
                pvt_SendPassword(e.GetCallbackValue("RandPwd"), e.GetCallbackValue("HashPwd"), _
                    e.GetCallbackValue("EmailId"), e.GetCallbackValue("UserName"))
        End Select
    End Sub

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Admin/ForgetPwd.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/MD5.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.corner.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.ui.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region
End Class

