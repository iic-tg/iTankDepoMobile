<%@ WebHandler Language="VB" Class="Download" %>

Imports System
Imports System.Web
Imports System.IO

Public Class Download : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim strFileName As String
        Dim strFile As String = context.Request.QueryString("FL_NM")
        Dim strInvc_Type As String = context.Request.QueryString("INVC_TYPE")
        Dim strSkipErrorMessage As String = context.Request.QueryString("SKP_ERR")
        'Open a file for reading        
        
        If strInvc_Type <> "" Then
            strFileName = String.Concat(ConfigurationManager.AppSettings("UploadPhyPath").ToString(), strInvc_Type, "\", strFile)
            
        Else
            strFileName = String.Concat(ConfigurationManager.AppSettings("UploadPhyPath").ToString(), strFile)
        End If
            
        
        
        Dim strExtension As String = IO.Path.GetExtension(strFile).ToLower()
        'Get a StreamReader class that can be used to read the file
        If IO.File.Exists(strFileName) Then
            Dim objByte As Byte()
            objByte = File.ReadAllBytes(strFileName)

            'Now, read the entire file into a string
        
            'Set the text of the file to a Web control
           
            Dim strContentType As String = ""
            Select Case strExtension.ToLower()
                Case ".jpg", ".jpeg"
                    strContentType = "image/jpeg"
                Case ".png",
                    strContentType = "image/png"
                Case ".gif"
                    strContentType = "image/gif"
                Case "doc",".doc"
                    strContentType = "application/ms-word"
                Case "docx",".docx"
                    strContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                Case "xls",".xls"
                    strContentType = "application/vnd.ms-excel"
                Case "xlsx", ".xlsx"
                    strContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Case "pdf",".pdf"
                    strContentType = "application/pdf"
            End Select
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile)
            context.Response.ContentType = strContentType
            context.Response.BinaryWrite(objByte)
        Else
            If strSkipErrorMessage = "" Then
                context.Response.Write("Requested file is not available. Please contact System Administrator.")
                context.Response.ContentType = "text/plain"
            End If
        End If
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class