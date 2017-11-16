Imports System.ServiceProcess
Imports iInterchange.iTankDepo.Business.Billing
Imports iInterchange.iTankDepo.Business.Common
Imports iInterchange.iTankDepo.Business.Admin
Imports iInterchange.iTankDepo.Business.Services
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.Data
Imports System.Configuration
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Web
Imports System.Xml

Public Class FTPXml
#Region "StartStop"

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
    End Sub
#End Region

#Region "ReadXml"

    Private Sub tmrFtp_Elapsed(sender As System.Object, e As System.Timers.ElapsedEventArgs) Handles tmrFtp.Elapsed
        Try
            SyncLock tmrFtp
                pvt_readXml()
            End SyncLock
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, String.Concat(ex, "Inner Exception : ", ex.InnerException))
        End Try
    End Sub

    Private Sub pvt_readXml()
        Try
            Dim strPath As String = Config.pub_GetAppConfigValue("FTPToLocal")
            Dim strfiles() As String = Directory.GetFileSystemEntries(strPath)
            For fileCount = 0 To strfiles.Length - 1
                Dim fileInf As New FileInfo(strfiles(fileCount))
                'Dim arrNma As Array = fileInf.ToString.Split("\")
                Dim strFilename As String = fileInf.Name
                Dim m_xmld As New XmlDocument
                Dim m_nodelist As XmlNodeList
                m_xmld.Load(fileInf.ToString)
                m_nodelist = m_xmld.GetElementsByTagName("Move")
                Dim strInvoiceNo As String = m_xmld.GetElementsByTagName("Invoice")(0).ChildNodes.Item(0).InnerText.ToString
                Dim strActivity As String = m_xmld.GetElementsByTagName("Invoice")(0).ChildNodes.Item(4).InnerText.ToString
                Dim strHeaderResponse As String = m_xmld.GetElementsByTagName("HeaderResponse")(0).ChildNodes.Item(0).InnerText.ToString
                pvt_UpdateHeaderResponse(strFilename, strInvoiceNo, strHeaderResponse, strActivity)
                'Loop through the nodes
                For i = 0 To m_nodelist.Count - 1
                    'Get the firstName Element Value
                    Dim strMoveNo As String = m_xmld.GetElementsByTagName("TankonMove")(i).ChildNodes.Item(0).InnerText.ToString
                    Dim strSupportUrl As String = m_xmld.GetElementsByTagName("ChargeforTank")(i).ChildNodes.Item(6).InnerText.ToString
                    Dim strLineResponse As String = m_xmld.GetElementsByTagName("ChargeforTank")(i).ChildNodes.Item(7).InnerText.ToString
                    pvt_UpdateLineStatus(strInvoiceNo, strMoveNo, strSupportUrl, strLineResponse)
                Next
                Dim strStatus As String = String.Empty
                For i = 0 To m_nodelist.Count - 1
                    Dim strLineResponse As String = m_xmld.GetElementsByTagName("ChargeforTank")(i).ChildNodes.Item(7).InnerText.ToString
                    If strLineResponse = "Invoice Rejected. No Lines Received." Then
                        strStatus = "Rejected"
                        pvt_UpdateStatus(strInvoiceNo, strStatus)
                        Exit For
                    ElseIf strLineResponse = "Invoice Rejected. Line Valid." Then
                        strStatus = "Rejected"
                        pvt_UpdateStatus(strInvoiceNo, strStatus)
                        Exit For
                    ElseIf strLineResponse = "Invoice Rejected. Tank/Move combination is invalid." Then
                        strStatus = "Rejected"
                        pvt_UpdateStatus(strInvoiceNo, strStatus)
                        Exit For
                    ElseIf strLineResponse = "Invoice Rejected. Non-Numeric Data Received in Numeric Field." Then
                        strStatus = "Rejected"
                        pvt_UpdateStatus(strInvoiceNo, strStatus)
                        Exit For
                    ElseIf strLineResponse = "Invoice Rejected. Invoice Amount is Zero." Then
                        strStatus = "Rejected"
                        pvt_UpdateStatus(strInvoiceNo, strStatus)
                        Exit For
                    ElseIf strLineResponse = "Invoice Rejected. Service Date is Invalid." Then
                        strStatus = "Rejected"
                        pvt_UpdateStatus(strInvoiceNo, strStatus)
                        Exit For
                    ElseIf strLineResponse = "Invoice Rejected. Charge Code is Invalid." Then
                        strStatus = "Rejected"
                        pvt_UpdateStatus(strInvoiceNo, strStatus)
                        Exit For
                    ElseIf strLineResponse = "Invoice Rejected. Vendor Type is Invalid." Then
                        strStatus = "Rejected"
                        pvt_UpdateStatus(strInvoiceNo, strStatus)
                        Exit For
                        'ElseIf strLineResponse = "Invoice Processed." Then
                        '    strStatus = "Processed"
                        '    pvt_UpdateStatus(strInvoiceNo, strStatus)
                        'Exit For
                    ElseIf strLineResponse.Contains("Invoice Received.") Then
                        strStatus = "Received"
                        pvt_UpdateStatus(strInvoiceNo, strStatus)
                        'Exit For
                    ElseIf strLineResponse = "Invoice Rejected Manually. Please verify with STC." Then
                        strStatus = "Rejected"
                        pvt_UpdateStatus(strInvoiceNo, strStatus)
                        'Exit For
                    End If
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

    Private Sub pvt_UpdateHeaderResponse(ByVal bv_strIFileName As String, ByVal bv_strInvoiceNo As String, ByVal bv_strHeaderResponse As String, ByVal bv_strActivity As String)
        Try
            Dim objViewXmlEdi As New ViewXmlEdi
            Dim strStatus As String = String.Empty
            If bv_strHeaderResponse = "Invoice Rejected. No Header Received." Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Error on Line(s)." Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Non-Numeric Data Received in Numeric Field." Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Vendor Reference is blank." Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Number Of Invoice Lines Does Not Match Header." Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Total Of Invoice Lines Does Not Match Header." Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Invoice Date is Invalid." Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Invalid Currency Code." Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Office Code is Invalid." Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Invalid URL." Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Rejected. Duplicate Invoice Received." Then
                strStatus = "Rejected"
            ElseIf bv_strHeaderResponse = "Invoice Processed." Then
                strStatus = "Proccessed"
            ElseIf bv_strHeaderResponse.Contains("Invoice Received.") Then
                strStatus = "Received"
            ElseIf bv_strHeaderResponse = "Invoice Rejected Manually. Please verify with STC." Then
                strStatus = "Rejected"
            End If
            objViewXmlEdi.pub_UpdateHeaderResponse(bv_strIFileName, bv_strInvoiceNo, bv_strHeaderResponse, strStatus)
            If strStatus = "Rejected" Then
                Dim intDepotId As Integer = Config.pub_GetAppConfigValue("Depot")
                Dim strToMail As String = pub_getDepotDetails(intDepotId)
                Dim objAlert As New Alert
                Dim objCommonConfig As New ConfigSetting()
                Dim str_042EqType As String = objCommonConfig.pub_GetConfigSingleValue("42", 1)
                Dim bln_042EqType_Key As Boolean = objCommonConfig.IsKeyExists
                If bln_042EqType_Key And str_042EqType = "True" Then
                    objAlert.pub_CreateAlert("145", bv_strInvoiceNo, "", 0, "", strToMail, "", Nothing, "Alert Service", "")
                End If
            End If
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

    Private Sub pvt_UpdateStatus(ByVal bv_strInvoiceno As String, ByVal bv_strStatus As String)
        Try
            Dim objViewXmlEdi As New ViewXmlEdi
            objViewXmlEdi.pub_UpdateStatus(bv_strInvoiceno, bv_strStatus)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message))
        End Try

    End Sub

    Public Function pub_getDepotDetails(ByVal bv_intDepotId As Integer) As String
        Try
            Dim dtDepot As New DataTable
            Dim strToEmail As String = String.Empty
            Dim objViewXmlEdi As New ViewXmlEdi
            dtDepot = objViewXmlEdi.GetDepotDetails(bv_intDepotId)
            If dtDepot.Rows.Count > 0 Then
                strToEmail = dtDepot.Rows(0).Item(DepotData.EML_ID)
            End If
            Return strToEmail
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, String.Concat(ex.Message, " Inner Exception : ", ex.InnerException))
        End Try

    End Function
#End Region

End Class
