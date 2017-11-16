Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.IO
Imports System.Web.Services
Imports System.Web

Public Class ForgotPassword_C

    Dim Server As System.Web.HttpServerUtility

    Public Function pub_ProcessParameteres(ByVal bv_strEmailIds As String, ByVal bv_strSubject As String, _
                                       ByVal bv_strPwd As String, ByVal bv_strUsername As String, ByVal bv_strTemplateFolderPath As String) As StringWriter
        Try
            Dim objXSLTrans As New Xsl.XslCompiledTransform
            Dim objXMLDoc As New XmlDocument
            Dim swFileWriter As New System.IO.StringWriter()

            Dim strDomainPath As String = AppDomain.CurrentDomain.BaseDirectory.ToString
            Dim XSLargsList As New Xsl.XsltArgumentList
            Dim strTemplateFolder As String


            strTemplateFolder = String.Concat(strDomainPath, bv_strTemplateFolderPath, "ForgotPassword.xslt")
            XSLargsList.AddParam("password", "", bv_strPwd)
            XSLargsList.AddParam("username", "", bv_strUsername)
            objXSLTrans.Load(strTemplateFolder)
            objXSLTrans.Transform(objXMLDoc, XSLargsList, swFileWriter)

            objXMLDoc = Nothing
            objXSLTrans = Nothing

            Return swFileWriter
        Catch ex As exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                   ex.Message)
        End Try

    End Function

    Public Function GenHashPassword(ByVal bv_strEmailId As String) As String
        Dim genRanPss As String
        Dim objUser As New User
        Dim sb As New StringBuilder

        Try

            'Dim bolValidPss As Boolean

            'bolValidPss = objUser.pub_ValidateEmailID(Server.UrlDecode(bv_strEmailId))
            'Dim bolValidUser As Boolean
            'bolValidUser = objUser.pub_ValidatePKUser(bv_strCode)
            'If bolValidPss = False Then

            Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            Dim r As New Random

            For i As Integer = 1 To 8
                Dim idx As Integer = r.Next(0, 35)
                sb.Append(s.Substring(idx, 1))
            Next



            'Else

            'genRanPss = "This is invalid Email Id"


            'End If
        Catch ex As exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                   ex.Message)
        End Try


        genRanPss = objUser.pub_EncryptPassword(sb.ToString)

        Return genRanPss

    End Function




End Class
