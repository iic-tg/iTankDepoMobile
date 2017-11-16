Imports System.Web
Imports System
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Configuration
Imports System.Text

Public Class iErrorHandler
    Public Const XML_ERRORLOG As String = "errorlog"
    Public Const XML_ERRORLOG_DOCTYPE As String = "errorlog"
    Public Const XML_ERRORLOG_DTD As String = "errorlog.dtd"
    Public Const XML_EXCEPTION As String = "exception"
    Public Const XML_EXCEPTION_TIME As String = "time"
    Public Const XML_EXCEPTION_DESCRIPTION As String = "description"
    Public Const XML_EXCEPTION_METHOD As String = "method"
    Public Const XML_EXCEPTION_CLASSNAME As String = "classname"

    Public Shared Sub pub_WriteErrorLog(ByVal bv_strClassName As String, ByVal bv_strFunctionName As String, ByVal bv_strMessage As String)
        'Try
        Dim strUserName As String = ""
        Dim strIpAddress As String = ""
        Dim strFileNamePrefix As String = ""

        Dim strLogFilePath As String
        If Config.pub_GetAppConfigValue("LogFilePath") IsNot Nothing Then
            strLogFilePath = Config.pub_GetAppConfigValue("LogFilePath")

        Else
            strLogFilePath = AppDomain.CurrentDomain.BaseDirectory.ToString & "LogFiles\"
        End If

        If Config.pub_GetAppConfigValue("LogFileName") IsNot Nothing Then
            strFileNamePrefix = Config.pub_GetAppConfigValue("LogFileName")
        End If

        If bv_strMessage.Contains("Thread was being aborted") Then
            Exit Sub
        End If

        If Config.pub_GetAppConfigValue("LogException") IsNot Nothing AndAlso Config.pub_GetAppConfigValue("LogException").ToLower() = "true" Then
            If bv_strMessage.Contains("chilkat") Then
                strLogFilePath = String.Concat(strLogFilePath, strFileNamePrefix, "chilkat", DateTime.Now.ToString("ddMMyyyy"), ".txt")
            Else
                strLogFilePath = String.Concat(strLogFilePath, strFileNamePrefix, DateTime.Now.ToString("ddMMyyyy"), ".txt")
            End If
            Using w As StreamWriter = File.AppendText(strLogFilePath)
                Log(bv_strClassName, bv_strFunctionName, bv_strMessage, w, strIpAddress, strUserName)
                ' Close the writer and underlying file.
                w.Close()
            End Using
        End If

        If Config.pub_GetAppConfigValue("errorrss") IsNot Nothing AndAlso Config.pub_GetAppConfigValue("errorrss").ToLower() = "true" Then
            pvt_RssFeedError(bv_strClassName, bv_strFunctionName, bv_strMessage, strIpAddress, strUserName)
        End If

    End Sub

    Public Shared Sub pub_WriteApplicationLog(ByVal bv_strClassName As String, ByVal bv_strFunctionName As String, ByVal bv_strMessage As String)
        'Try
        Dim strUserName As String = ""
        Dim strIpAddress As String = ""
        Dim strFileNamePrefix As String = ""

        Dim strLogFilePath As String
        If Config.pub_GetAppConfigValue("LogFilePath") IsNot Nothing Then
            strLogFilePath = Config.pub_GetAppConfigValue("LogFilePath")

        Else
            strLogFilePath = AppDomain.CurrentDomain.BaseDirectory.ToString & "LogFiles\"
        End If

        If Config.pub_GetAppConfigValue("ApplicationLogFileName") IsNot Nothing Then
            strFileNamePrefix = Config.pub_GetAppConfigValue("ApplicationLogFileName")
        End If

        If bv_strMessage.Contains("Thread was being aborted") Then
            Exit Sub
        End If

        If Config.pub_GetAppConfigValue("ApplicationLog") IsNot Nothing AndAlso Config.pub_GetAppConfigValue("ApplicationLog").ToLower() = "true" Then
            strLogFilePath = String.Concat(strLogFilePath, strFileNamePrefix, "AppLog", DateTime.Now.ToString("ddMMyyyy"), ".txt")
            Using w As StreamWriter = File.AppendText(strLogFilePath)
                Log(bv_strClassName, bv_strFunctionName, bv_strMessage, w, strIpAddress, strUserName)
                ' Close the writer and underlying file.
                w.Close()
            End Using
        End If
    End Sub
    Public Shared Sub pub_WriteErrorLog(ByVal bv_strClassName As String, ByVal bv_strFunctionName As String, ByVal ex As Exception)
        'Try
        Dim strUserName As String = ""
        Dim strIpAddress As String = ""

        Dim strFileNamePrefix As String = ""

        Dim strLogFilePath As String
        If Config.pub_GetAppConfigValue("LogFilePath") IsNot Nothing Then
            strLogFilePath = Config.pub_GetAppConfigValue("LogFilePath")

        Else
            strLogFilePath = AppDomain.CurrentDomain.BaseDirectory.ToString & "LogFiles\"
        End If

        If Config.pub_GetAppConfigValue("LogFileName") IsNot Nothing Then
            strFileNamePrefix = Config.pub_GetAppConfigValue("LogFileName")
        End If


        If ex.Message.Contains("Thread was being aborted") Then
            Exit Sub
        End If

        If Config.pub_GetAppConfigValue("LogException") IsNot Nothing AndAlso Config.pub_GetAppConfigValue("LogException").ToLower() = "true" Then
            If ex.Message.Contains("chilkat") Then
                strLogFilePath = String.Concat(strLogFilePath, strFileNamePrefix, "chilkat", DateTime.Now.ToString("ddMMyyyy"), ".txt")
            Else
                strLogFilePath = String.Concat(strLogFilePath, strFileNamePrefix, DateTime.Now.ToString("ddMMyyyy"), ".txt")
            End If
            Using w As StreamWriter = File.AppendText(strLogFilePath)
                Log(bv_strClassName, bv_strFunctionName, GetErrorMessage(ex), w, strIpAddress, strUserName)
                ' Close the writer and underlying file.
                w.Close()
            End Using
        End If

        If Config.pub_GetAppConfigValue("errorrss") IsNot Nothing AndAlso Config.pub_GetAppConfigValue("errorrss").ToLower() = "true" Then
            If ex.InnerException Is Nothing Then
                pvt_RssFeedError(bv_strClassName, bv_strFunctionName, ex.Message, strIpAddress, strUserName)
            Else
                pvt_RssFeedError(bv_strClassName, bv_strFunctionName, ex.Message & " " & ex.InnerException.ToString, strIpAddress, strUserName)
            End If
        End If

    End Sub

    Private Shared Sub WriteException(ByVal strClassName As String, ByVal bv_strFunctionName As String, ByVal strMessage As String, ByVal xmlWriter As XmlWriter)
        xmlWriter.WriteStartElement(XML_EXCEPTION)
        xmlWriter.WriteElementString(XML_EXCEPTION_TIME, System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
        xmlWriter.WriteElementString(XML_EXCEPTION_DESCRIPTION, strMessage)
        If Not (bv_strFunctionName Is Nothing) Then
            xmlWriter.WriteElementString(XML_EXCEPTION_METHOD, bv_strFunctionName)
        End If
        If Not (strClassName Is Nothing) Then
            xmlWriter.WriteElementString(XML_EXCEPTION_CLASSNAME, strClassName)
        End If
        xmlWriter.WriteEndElement()
    End Sub

    Public Shared Sub SaveExeptionToLog(ByVal strClassName As String, ByVal bv_strFunctionName As String, ByVal strMessage As String)
        Dim strOriginal As String = Nothing
        Dim ApplicationPath As String = AppDomain.CurrentDomain.BaseDirectory.ToString & "LogFiles\"

        Dim objStream As New IO.FileStream(String.Concat(ApplicationPath, DateTime.Now.ToString("ddMMyyyy"), ".xml"), FileMode.Append)

        Dim xmlWriter As XmlTextWriter = New System.Xml.XmlTextWriter(objStream, System.Text.Encoding.UTF8)
        xmlWriter.WriteStartDocument()
        xmlWriter.WriteDocType(XML_ERRORLOG_DOCTYPE, Nothing, XML_ERRORLOG_DTD, Nothing)
        xmlWriter.WriteStartElement(XML_ERRORLOG)
        'xmlWriter.WriteRaw(strOriginal)
        WriteException(strClassName, bv_strFunctionName, strMessage, xmlWriter)
        xmlWriter.WriteEndElement()
        xmlWriter.Close()
    End Sub

    Public Shared Sub Log(ByVal bv_strClassName As String, ByVal bv_strFunctionName As String, ByVal bv_strErrorMessage As String, ByVal w As TextWriter, ByVal bv_strIpAddress As String, ByVal bv_strUserId As String)
        w.Write(ControlChars.CrLf)
        w.WriteLine("{0} | {1} | {2} | Class Name: {3} | Function Name:  {4} | Message : {5} ", _
                DateTime.Now, bv_strIpAddress, bv_strUserId, bv_strClassName, bv_strFunctionName, bv_strErrorMessage)
        w.Flush()
    End Sub
    Public Shared Function GetErrorMessage(ByVal bv_Exception As Exception) As String
        If Not Config.pub_GetAppConfigValue("LogInnerException") Is Nothing AndAlso Config.pub_GetAppConfigValue("LogInnerException") = "true" Then
            If Not bv_Exception.InnerException Is Nothing Then
                Return bv_Exception.InnerException.ToString
            End If
        End If
        Return bv_Exception.Message
    End Function

    Public Shared Sub Log(ByVal bv_strClassName As String, ByVal bv_strFunctionName As String, ByVal bv_strMessage As String, ByVal w As TextWriter)
        w.Write(ControlChars.CrLf)
        w.WriteLine("{0} :", DateTime.Now)
        w.WriteLine("{0}  {1}   {2} ", bv_strClassName, bv_strFunctionName, bv_strMessage)
        w.Flush()
    End Sub

    Private Shared Sub pvt_RssFeedError(ByVal bv_strClassName As String, _
                               ByVal bv_strFunctionName As String, _
                               ByVal bv_strMessage As String, _
                               ByVal bv_strIpAddress As String, _
                               ByVal bv_strUserId As String)
        Try
            Dim strXmlRssFeedFile As String
            strXmlRssFeedFile = String.Concat(AppDomain.CurrentDomain.BaseDirectory.ToString, "error.xml")
            Dim xmldocRssFeed As New System.Xml.XmlDocument()
            xmldocRssFeed.Load(strXmlRssFeedFile)
            Dim channelNode As System.Xml.XmlNode = xmldocRssFeed.DocumentElement.FirstChild
            Dim itemNode As System.Xml.XmlNode = xmldocRssFeed.CreateNode(System.Xml.XmlNodeType.Element, "item", "")
            Dim titleNode As System.Xml.XmlNode = xmldocRssFeed.CreateNode(System.Xml.XmlNodeType.Element, "title", "")
            titleNode.InnerText = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt")
            Dim descriptionNode As System.Xml.XmlNode = xmldocRssFeed.CreateNode(System.Xml.XmlNodeType.Element, "description", "")
            Dim sbrErrorMessage As New Text.StringBuilder()
            sbrErrorMessage.Append("<table style='font-size: 10pt;font-family: Arial;color: #000000;'> <tr> <td align='left' valign='top' style='width:200px'><b>Class Name :</b></b></td> <td align='left' valign='top'>" & bv_strClassName & "</td></tr>")
            sbrErrorMessage.Append("<tr> <td align='left' valign='top' style='width:200px'><b>Function Name :</b></td> <td align='left' valign='top'>" & bv_strFunctionName & "</td></tr>")
            sbrErrorMessage.Append("<tr> <td align='left' valign='top' style='width:200px'><b>Error Message :</b></td> <td style='font-size: 10pt;font-family: Arial;color: #ff0000;'><b> <i>" & bv_strMessage & "</i></b></td></tr>")
            sbrErrorMessage.Append("<tr> <td align='left' valign='top' style='width:200px'><b>IP Address :</b></td> <td align='left' valign='top'>" & bv_strIpAddress & "</td></tr>")
            sbrErrorMessage.Append("<tr> <td align='left' valign='top' style='width:200px'><b>User ID :</b></td> <td align='left' valign='top'>" & bv_strUserId & "</td></tr></table> ")
            descriptionNode.InnerText = sbrErrorMessage.ToString()
            Dim linkNode As System.Xml.XmlNode = xmldocRssFeed.CreateNode(System.Xml.XmlNodeType.Element, "link", "")
            Dim urlstr As String = ""
            linkNode.InnerText = urlstr
            itemNode.AppendChild(titleNode)
            itemNode.AppendChild(descriptionNode)
            itemNode.AppendChild(linkNode)
            channelNode.AppendChild(itemNode)
            xmldocRssFeed.Save(strXmlRssFeedFile)
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub SaveQueryToLog(ByVal bv_strClassName As String, ByVal bv_strFunctionName As String, ByVal bv_strMessage As String)
        Try
            Dim strApplicationPath As String = AppDomain.CurrentDomain.BaseDirectory.ToString
            Dim strOriginal As String = Nothing
            Dim xmlReader As XmlTextReader = New XmlTextReader(String.Concat(Config.pub_GetAppConfigValue("LogDBQueryFilePath"), DateTime.Now.ToString("ddMMyyyy"), ".xml"))
            Try
                xmlReader.WhitespaceHandling = WhitespaceHandling.None
                xmlReader.MoveToContent()
                strOriginal = xmlReader.ReadInnerXml
                xmlReader.Close()
            Catch ex As Exception
                xmlReader.Close()
            End Try
            Dim xmlWriter As XmlTextWriter = New System.Xml.XmlTextWriter(String.Concat(Config.pub_GetAppConfigValue("LogDBQueryFilePath"), DateTime.Now.ToString("ddMMyyyy"), ".xml"), System.Text.Encoding.UTF8)
            Try
                xmlWriter.WriteStartDocument()
                xmlWriter.WriteStartElement(XML_ERRORLOG)
                xmlWriter.WriteRaw(strOriginal)
                WriteException(bv_strClassName, bv_strFunctionName, bv_strMessage, xmlWriter)
                xmlWriter.WriteEndElement()
                xmlWriter.Close()
            Catch ex As Exception
                xmlWriter.Close()
            End Try
        Catch ex As Exception
        End Try
    End Sub
End Class

