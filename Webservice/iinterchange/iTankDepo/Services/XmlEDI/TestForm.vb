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
Imports System.Net


Public Class TestForm

    Private Sub start_Click(sender As System.Object, e As System.EventArgs) Handles start.Click
        Try
            Dim strPath As String = Config.pub_GetAppConfigValue("FTPToLocal")
            Dim strfiles() As String = Directory.GetFileSystemEntries(strPath)
            For fileCount = 0 To strfiles.Length - 1
                Dim fileInf As New FileInfo(strfiles(fileCount))
                Dim arrNma As Array = fileInf.ToString.Split("\")
                Dim strFilename As String = arrNma(6)
                Dim m_xmld As New XmlDocument
                Dim m_nodelist As XmlNodeList
                m_xmld.Load(fileInf.ToString)
                m_nodelist = m_xmld.GetElementsByTagName("Move")
                Dim strInvoiceNo As String = m_xmld.GetElementsByTagName("Invoice")(0).ChildNodes.Item(0).InnerText
                Dim strHeaderResponse As String = m_xmld.GetElementsByTagName("HeaderResponse")(0).ChildNodes.Item(0).InnerText
                
                pvt_UpdateHeaderResponse(strFilename, strInvoiceNo, strHeaderResponse)
                'Loop through the nodes
                For i = 0 To m_nodelist.Count - 1
                    'Get the firstName Element Value
                    Dim strMoveNo As String = m_xmld.GetElementsByTagName("TankonMove")(i).ChildNodes.Item(0).InnerText.ToString
                    Dim strSupportUrl As String = m_xmld.GetElementsByTagName("ChargeforTank")(i).ChildNodes.Item(6).InnerText.ToString
                    Dim strLineResponse As String = m_xmld.GetElementsByTagName("ChargeforTank")(i).ChildNodes.Item(7).InnerText.ToString
                    pvt_UpdateLineStatus(strInvoiceNo, strMoveNo, strSupportUrl, strLineResponse)
                Next
                pvt_fnMoveXmlFile(strPath, strFilename)
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message, " Inner Exception : ", ex.InnerException))
        End Try


    End Sub

    Private Sub pvt_UpdateLineStatus(ByVal bv_strInvoiceNo As String, ByVal bv_strMoveNo As String, ByVal bv_strSupportUrl As String, ByVal bv_strLineResponse As String)
        Try
            Dim objViewXmlEdi As New ViewXmlEdi
            objViewXmlEdi.pub_UpdateLineResponse(bv_strInvoiceNo, bv_strMoveNo, bv_strSupportUrl, bv_strLineResponse)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message, " Inner Exception : ", ex.InnerException))
        End Try

    End Sub

    Private Sub pvt_UpdateHeaderResponse(ByVal bv_strIFileName As String, ByVal bv_strInvoiceNo As String, ByVal bv_strHeaderResponse As String)
        Try
            Dim objViewXmlEdi As New ViewXmlEdi
            Dim strStatus As String = String.Empty
            If bv_strHeaderResponse = "Invoice Rejected. No Header Received" Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Error on Line(s)" Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Non-Numeric Data Received in Numeric Field" Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Vendor Reference is blank" Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Number Of Invoice Lines Does Not Match Header" Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Total Of Invoice Lines Does Not Match Header" Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Invoice Date is Invalid" Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Invalid Currency Code" Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Office Code is Invalid" Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Invalid URL" Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Duplicate Invoice Received" Then
                strStatus = "Rejected"
            Else
                strStatus = "Proccessed"
            End If
            objViewXmlEdi.pub_UpdateHeaderResponse(bv_strIFileName, bv_strInvoiceNo, bv_strHeaderResponse, strStatus)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message, " Inner Exception : ", ex.InnerException))
        End Try

    End Sub

    

    Private Sub pvt_fnMoveXmlFile(ByVal bv_strFilePath As String, ByVal bv_strFileName As String)
        Try
            Dim ExistingPath As String = String.Concat(bv_strFilePath, bv_strFileName)
            Dim NewPath As String = Config.pub_GetAppConfigValue("Archieved") & bv_strFileName
            If File.Exists(ExistingPath) Then
                File.Move(ExistingPath, NewPath)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message))
        End Try
    End Sub







    
End Class










