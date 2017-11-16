Imports System.ServiceProcess
Imports iInterchange.iTankDepo.Business.Billing
Imports iInterchange.iTankDepo.Business.Common
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.Data
Imports System.Configuration
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Web
Imports System.Xml

Public Class TestForm

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        pvt_XmlAlertMail("0990000", "Rejected")
    End Sub



#Region "Processing  parameters and construct xslt template - Invoice Rejected"
    Private Function pub_ProcessParameteres(ByVal bv_strPwd As String, ByVal bv_strUsername As String, ByVal bv_strTemplateFolderPath As String) As StringWriter
        Try
            Dim objXSLTrans As New Xsl.XslCompiledTransform
            Dim objXMLDoc As New XmlDocument
            Dim swFileWriter As New System.IO.StringWriter()

            Dim strDomainPath As String = AppDomain.CurrentDomain.BaseDirectory.ToString
            Dim XSLargsList As New Xsl.XsltArgumentList
            'Dim strTemplateFolder As String


            'strTemplateFolder = String.Concat(strDomainPath, bv_strTemplateFolderPath, "ForgotPassword.xslt")
            XSLargsList.AddParam("password", "", bv_strPwd)
            XSLargsList.AddParam("username", "", bv_strUsername)
            objXSLTrans.Load(bv_strTemplateFolderPath)
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


#Region "pvt_SendPassword"
    Protected Sub pvt_XmlAlertMail(ByVal bv_strInvoiceno As String, ByVal bv_strStatus As String)
        Dim strTemplatefolderPth As String
        Try
            Dim strSubject As String
            Dim strMessageBody As StringWriter
            strSubject = "Stolt Edi"
            strTemplatefolderPth = Config.pub_GetAppConfigValue("TemplatesFolder")
            strMessageBody = pub_ProcessParameteres(bv_strInvoiceno, bv_strStatus, strTemplatefolderPth)
            pub_Send_Email("karthika.s@iinterchange.com", "surya.s@iinterchange.com", "", strSubject, strMessageBody.ToString())
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                    ex.Message)
        End Try
    End Sub
#End Region

    Public Function pub_Send_Email(ByVal pvt_strFromMailID As String, _
                                                  ByVal bv_strToMailIDs As String, _
                                                  ByVal bv_strBCCEmailIDs As String, _
                                                  ByVal bv_strSubject As String, _
                                                  ByVal bv_strBody As String) As Boolean

        Dim objChilkat As New Chilkat.MailMan
        Dim objEmail As New Chilkat.Email
        Dim Status As Boolean
        Try
            objChilkat.UnlockComponent("UiinterchMAILQ_FmPBEfzL8SkF")
            objChilkat.SmtpHost = Config.pub_GetAppConfigValue("SmtpMailServer")

            'Adding From Email ID
            If pvt_strFromMailID = "" Then
                objEmail.From = Config.pub_GetAppConfigValue("FromEmailID")
            Else
                'Adding From EmailID
                objEmail.From = pvt_strFromMailID
            End If

            If Config.pub_GetAppConfigValue("SmtpAuthEnabled") = "true" Then
                objChilkat.SmtpUsername = Config.pub_GetAppConfigValue("SmtpUserName")
                objChilkat.SmtpPassword = Config.pub_GetAppConfigValue("SmtpPassword")
            End If

            If Config.pub_GetAppConfigValue("SmtpPortNo") IsNot Nothing Then
                objChilkat.SmtpPort = Config.pub_GetAppConfigValue("SmtpPortNo")
            End If

            'Adding TO EmailIDs
            Dim strToEmailIds As String()

            Dim macTo As New System.Net.Mail.MailAddressCollection
            strToEmailIds = bv_strToMailIDs.Split(CChar(","))

            If strToEmailIds.Length = 0 Then
                strToEmailIds(0) = bv_strToMailIDs
            End If

            For Each strToEmail As String In strToEmailIds
                objEmail.AddTo(strToEmail, strToEmail)
            Next

            'Adding CC EmailIDs
            Dim strBCCEmailIds As String()
            strBCCEmailIds = bv_strBCCEmailIDs.Split(CChar(","))

            If strBCCEmailIds.Length = 0 Then
                strBCCEmailIds(0) = bv_strBCCEmailIDs
            End If

            For Each strBCCEmail As String In strBCCEmailIds
                objEmail.AddBcc(strBCCEmail, strBCCEmail)
            Next

            'Email Subject
            objEmail.Subject = bv_strSubject
            objEmail.SetHtmlBody(bv_strBody)
            Status = objChilkat.SendEmail(objEmail)
            If Not Status Then
                iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, objChilkat.LastErrorText)
            End If

            Return Status
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        Finally
            objChilkat = Nothing
        End Try

    End Function

End Class