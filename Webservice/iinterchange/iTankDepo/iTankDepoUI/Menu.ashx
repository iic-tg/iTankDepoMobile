<%@ WebHandler Language="VB" Class="menu" %>

Imports System
Imports System.Web
Imports System.IO

Public Class menu : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
                   
        'Open a file for reading
        Dim FILENAME As String = context.Server.MapPath(String.Concat(CommonWeb.pub_GetConfigValue("RoleRightMenuPath").ToString(), context.Request.QueryString("RL_CD"), "_menu.js"))

        'Get a StreamReader class that can be used to read the file
        Dim objStreamReader As StreamReader
        objStreamReader = File.OpenText(FILENAME)

        'Now, read the entire file into a string
        Dim contents As String = objStreamReader.ReadToEnd()

        objStreamReader.Close()

        context.Response.Write(contents)
        'Set the text of the file to a Web control
        context.Response.ContentType = "text/javascript"
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class