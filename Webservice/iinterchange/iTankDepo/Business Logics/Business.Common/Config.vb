Imports System.Xml
Imports System.Configuration
Imports System.Security.Cryptography

Public Class Config

    Public Shared Function pub_GetAppConfigValue(ByVal bv_strConfigName As String) As String
        Return ConfigurationManager.AppSettings(bv_strConfigName)
    End Function

#Region "pub_GetChildNodes"
    Public Shared Function pub_GetChildNodes(ByVal bv_strPath As String) As XmlNode
        Try
            Dim xmlDocTcp As New XmlDocument
            Dim strConfigFile As String = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Config.xml")
            xmlDocTcp.Load(strConfigFile)
            Dim rootTcp As XmlElement = xmlDocTcp.DocumentElement
            Dim xmlClientNodes As XmlNode = rootTcp.SelectSingleNode(bv_strPath)
            Return xmlClientNodes
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("StationUI", "pvt_GetClients", ex.Message)
        End Try
    End Function
#End Region

    Public Shared Function GetConfigValue(ByVal bv_strKeyName As String) As String
        Dim strKeyName As String = bv_strKeyName
        Try
            Dim strConfigFile As String = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Config.xml")
            Using reader As XmlReader = XmlReader.Create(strConfigFile)
                While reader.Read()
                    If reader.IsStartElement() Then
                        Dim attribute As String = reader(bv_strKeyName)
                        If attribute IsNot Nothing Then
                            Return attribute
                        End If
                        If reader.Value IsNot Nothing AndAlso reader.Value <> "" Then
                            Return reader.Value
                        End If
                    End If
                End While
            End Using
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("Config.GetConfigValue", strKeyName, ex.ToString())
        End Try
    End Function

    Public Shared Function GetConfigValueByPath(ByVal bv_strKeyName As String, ByVal bv_strXMLPath As String) As String
        Dim strKeyName As String = bv_strKeyName
        Try
            Dim strConfigFile As String = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Config.xml")
            Dim doc As XmlDocument = New XmlDocument()
            doc.Load(strConfigFile)
            Dim nodeList As XmlNodeList = doc.SelectNodes(bv_strXMLPath)
            Dim node As XmlNode
            For Each node In nodeList
                If node.Attributes(bv_strKeyName) IsNot Nothing Then
                    Return node.Attributes(bv_strKeyName).Value
                End If
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("Config.GetConfigValueByPath", strKeyName, ex.ToString())
        End Try
    End Function

    Public Shared Function SetConfigValue(ByVal bv_strKeyName As String, ByVal bv_strKeyValue As String, ByVal bv_strXMLPath As String) As String
        Dim strKeyName As String = bv_strKeyName
        Try
            Dim strConfigFile As String = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Config.xml")
            Dim doc As XmlDocument = New XmlDocument()
            doc.Load(strConfigFile)
            Dim nodeList As XmlNodeList = doc.SelectNodes(bv_strXMLPath)
            Dim node As XmlNode
            For Each node In nodeList
                node.Attributes(bv_strKeyName).Value = bv_strKeyValue
            Next
            doc.Save(strConfigFile)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("Config.SetConfigValue", strKeyName, ex.ToString())
        End Try
    End Function

    Public Shared Function EncryptData(Message As String) As String
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        Dim Results As Byte()
        Dim pvt_strKeyPhrase As String
        Try
            pvt_strKeyPhrase = "IIC"
            Dim UTF8 As New System.Text.UTF8Encoding()
            Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(pvt_strKeyPhrase))
            TDESAlgorithm.Key = TDESKey
            TDESAlgorithm.Mode = CipherMode.ECB
            TDESAlgorithm.Padding = PaddingMode.PKCS7
            Dim DataToEncrypt As Byte() = UTF8.GetBytes(Message)
            Dim Encryptor As ICryptoTransform = TDESAlgorithm.CreateEncryptor()
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("Config.EncryptData", Message, ex.ToString())
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return Convert.ToBase64String(Results)
    End Function

    Public Shared Function DecryptString(Message As String) As String
        Dim Results As Byte()
        Dim UTF8 As New System.Text.UTF8Encoding()
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        Dim pvt_strKeyPhrase As String
        Try
            pvt_strKeyPhrase = "IIC"
            Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(pvt_strKeyPhrase))
            TDESAlgorithm.Key = TDESKey
            TDESAlgorithm.Mode = CipherMode.ECB
            TDESAlgorithm.Padding = PaddingMode.PKCS7
            Dim DataToDecrypt As Byte() = Convert.FromBase64String(Message)
            Dim Decryptor As ICryptoTransform = TDESAlgorithm.CreateDecryptor()
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("Config.DecryptString", Message, ex.ToString())
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return UTF8.GetString(Results)
    End Function

End Class
