﻿Imports System.ServiceProcess
Imports iInterchange.iTankDepo.Business.Billing
Imports iInterchange.iTankDepo.Business.Common
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.Data
Imports System.Configuration
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Public Class Forward

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Dim objViewXmlEdi As New ViewXmlEdi
        'Dim dtCustomer As New DataTable
        'dtCustomer = objViewXmlEdi.GetCustomerFtpCredentials()
        'For Each drCustomer As DataRow In dtCustomer.Rows
        '    Dim strFtpHost As String = drCustomer.Item(CustomerData.FTP_SRVR_URL)
        '    Dim strFtpUserName As String = drCustomer.Item(CustomerData.FTP_USR_NAM)
        '    Dim strFtpPassword As String = drCustomer.Item(CustomerData.FTP_PSSWRD)
        '    UploadXmlFile(strFtpHost, strFtpUserName, strFtpPassword)
        'Next
        UploadXmlFile("ftp://10.1.193.157/Upload/", "ftpuser", "ftp123")
    End Sub

    Public Sub UploadXmlFile(ByVal bv_strUploadPath As String, ByVal bv_strFTPUser As String, ByVal bv_strFTPPass As String)
        Dim strPath As String = Config.pub_GetAppConfigValue("XmlSource")
        Try
            Dim strfiles() As String = Directory.GetFileSystemEntries(strPath)
            For fileCount = 0 To strfiles.Length - 1
                Dim fileInf As New FileInfo(strfiles(fileCount))
                Dim arrNma As Array = fileInf.ToString.Split("\")
                Dim strFilename As String = arrNma(8)
                GenearateXmlFile(bv_strUploadPath, strFilename, fileInf, strPath, bv_strFTPUser, bv_strFTPPass)
                'Dim FtpWebRequest As System.Net.FtpWebRequest = CType(System.Net.FtpWebRequest.Create(New Uri(String.Concat(bv_strUploadPath, strFilename))), System.Net.FtpWebRequest)
                'FtpWebRequest.Credentials = New Net.NetworkCredential(bv_strFTPUser, bv_strFTPPass)
                'FtpWebRequest.KeepAlive = True
                'FtpWebRequest.Method = Net.WebRequestMethods.Ftp.UploadFile
                'FtpWebRequest.UseBinary = True
                'FtpWebRequest.ContentLength = fileInf.Length
                'Dim contentLen As Integer
                'contentLen = fileInf.Length
                'Dim buffLength As Integer = contentLen
                'Dim buff(buffLength) As Byte
                'Dim fs As IO.FileStream = fileInf.OpenRead()
                'Dim strm As IO.Stream = FtpWebRequest.GetRequestStream()
                'Dim fileContent As Byte() = New Byte(fileInf.Length - 1) {}
                'fs.Read(fileContent, 0, Convert.ToInt32(fileInf.Length))
                'strm.Write(fileContent, 0, fileContent.Length)
                'fs.Close()
                'strm.Close()
                'strm.Dispose()
                'fs.Dispose()
                'pvt_fnMoveXmlFile(strPath, strFilename)
                ''strFile.SetValue("", 0)
                'strFile = strFilename.Split(".")
                'pvt_UpdateStatus(strFile(0), "Success", "")
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message, " Inner Exception : ", ex.InnerException))

        End Try

    End Sub

    Public Sub UploadPdfFile(ByVal bv_strUploadPath As String, ByVal bv_strFTPUser As String, ByVal bv_strFTPPass As String)
        Dim strPath As String = Config.pub_GetAppConfigValue("PDFSource")
        Try
            Dim dtPdf As New DataTable
            Dim objViewXmlEdi As New ViewXmlEdi
            'dtPdf = objViewXmlEdi.GetPdfDetails()

            For Each dr As DataRow In dtPdf.Rows
                Dim strFilename As String = String.Concat(strPath, dr.Item(ViewXmlEdiData.INVC_FL_NAM))
                If File.Exists(strFilename) Then
                    Dim FtpWebRequest As System.Net.FtpWebRequest = CType(System.Net.FtpWebRequest.Create(New Uri(String.Concat(bv_strUploadPath, dr.Item(ViewXmlEdiData.INVC_FL_NAM)))), System.Net.FtpWebRequest)
                    FtpWebRequest.Credentials = New Net.NetworkCredential(bv_strFTPUser, bv_strFTPPass)
                    FtpWebRequest.KeepAlive = True
                    FtpWebRequest.Method = Net.WebRequestMethods.Ftp.UploadFile
                    FtpWebRequest.UseBinary = True
                    Dim fileInf As New FileInfo(strFilename)
                    FtpWebRequest.ContentLength = fileInf.Length
                    Dim contentLen As Integer
                    contentLen = fileInf.Length
                    Dim buffLength As Integer = contentLen
                    Dim buff(buffLength) As Byte
                    Dim fs As IO.FileStream = fileInf.OpenRead()
                    Dim strm As IO.Stream = FtpWebRequest.GetRequestStream()
                    Dim fileContent As Byte() = New Byte(fileInf.Length - 1) {}
                    fs.Read(fileContent, 0, Convert.ToInt32(fileInf.Length))
                    strm.Write(fileContent, 0, fileContent.Length)
                    fs.Close()
                    strm.Close()
                    strm.Dispose()
                    fs.Dispose()
                    pvt_UpdatePDFStatus(dr.Item(ViewXmlEdiData.INVC_FL_NAM))
                End If
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message, " Inner Exception : ", ex.InnerException))

        End Try

    End Sub

    Private Sub pvt_fnMoveXmlFile(ByVal bv_strFilePath As String, ByVal bv_strFileName As String)
        Try
            Dim ExistingPath As String = String.Concat(bv_strFilePath, bv_strFileName)
            Dim NewPath As String = Config.pub_GetAppConfigValue("Processsed") & bv_strFileName
            If File.Exists(ExistingPath) Then
                File.Move(ExistingPath, NewPath)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message))
        End Try
    End Sub
    Private Sub pvt_UpdateStatus(ByVal bv_strFileName As String, ByVal bv_strStatus As String, ByVal bv_strError As String, ByVal bv_datSentdate As DateTime)
        Try
            Dim objViewXmlEdi As New ViewXmlEdi
            objViewXmlEdi.pub_UpdateXmlStatus(bv_strFileName, bv_strStatus, bv_strError, bv_datSentdate)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message))
        End Try
    End Sub

    Private Sub pvt_UpdatePDFStatus(ByVal bv_strFileName As String)
        Try
            Dim objViewXmlEdi As New ViewXmlEdi
            objViewXmlEdi.pub_UpdatePDFStatus(bv_strFileName)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message))
        End Try
    End Sub

    Private Sub GenearateXmlFile(ByVal bv_strUploadPath As String, ByVal bv_strFilename As String, ByVal bv_fileInf As FileInfo, ByVal strPath As String, ByVal bv_strFTPUser As String, ByVal bv_strFTPPass As String)
        Dim arrFileName As Array = bv_strFilename.Split(".")
        Try
            Dim FtpWebRequest As System.Net.FtpWebRequest = CType(System.Net.FtpWebRequest.Create(New Uri(String.Concat(bv_strUploadPath, bv_strFilename))), System.Net.FtpWebRequest)
            FtpWebRequest.Credentials = New Net.NetworkCredential(bv_strFTPUser, bv_strFTPPass)
            FtpWebRequest.KeepAlive = True
            FtpWebRequest.Method = Net.WebRequestMethods.Ftp.UploadFile
            FtpWebRequest.UseBinary = True
            FtpWebRequest.ContentLength = bv_fileInf.Length
            Dim contentLen As Integer
            contentLen = bv_fileInf.Length
            Dim buffLength As Integer = contentLen
            Dim buff(buffLength) As Byte
            Dim fs As IO.FileStream = bv_fileInf.OpenRead()
            Dim strm As IO.Stream = FtpWebRequest.GetRequestStream()
            Dim fileContent As Byte() = New Byte(bv_fileInf.Length - 1) {}
            fs.Read(fileContent, 0, Convert.ToInt32(bv_fileInf.Length))
            strm.Write(fileContent, 0, fileContent.Length)
            fs.Close()
            strm.Close()
            strm.Dispose()
            fs.Dispose()
            pvt_fnMoveXmlFile(strPath, bv_strFilename)
            'strFile.SetValue("", 0)
            arrFileName = bv_strFilename.Split(".")
            pvt_UpdateStatus(arrFileName(0), "Success", "", Now)
        Catch ex As Exception
            pvt_UpdateStatus(arrFileName(0), "Failure", ex.Message, Nothing)
        End Try

    End Sub
End Class