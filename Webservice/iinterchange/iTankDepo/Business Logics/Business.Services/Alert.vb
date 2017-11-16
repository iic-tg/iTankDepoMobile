Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Xml
Imports System.IO
Imports System.Xml.Xsl
Imports System.Text
Imports iInterchange.iTankDepo.Business.Common

<ServiceContract()> _
Public Class Alert


#Region "Declaration...."
    Private Const PARENT_ELEMENT As String = "AlertGroup"
    Private Const TRANSACTIONID As String = "TransactionId"
    Private Const CHILD_ELEMENT As String = "DATA"
#End Region

#Region "pub_CreateAlert() TABLE NAME:Alert"

    <OperationContract()> _
    Public Function pub_CreateAlert(ByVal bv_intActivityID As String, ByVal bv_strRefValue As String, _
                               ByVal bv_intOrganisationID As String, ByVal bv_lngUSERID As Integer, _
                               ByVal bv_strAlertSubject As String, ByVal bv_strAlertTo As String, _
                               ByVal bv_strAlertCC As String, ByVal bv_dtAlertDueDate As DateTime, _
                               ByVal bv_strUserName As String, ByVal bv_strDBKey As String) As Long
        Try
            Dim objAlerts As New Alerts
            Dim dsAlertData As AlertDataSet
            Dim strAlertPendingQuery As String = String.Empty
            Dim strSubmitRefField As String = String.Empty
            Dim strTemplateFilePath As String = String.Empty
            Dim strSubject As String = String.Empty
            Dim intTemplateID As Integer
            Dim dsAlertRecord As DataSet
            Dim swAlertData As New StringWriter
            Dim datCreatedDate As DateTime = DateTime.Now()

            'Get Alert Setting by ActivityID
            dsAlertData = objAlerts.GetAlertSettingByActivityID(CInt(bv_intActivityID))

            strAlertPendingQuery = dsAlertData.Tables(AlertData._ALERTSETTING).Rows(0)(AlertData.ALRT_QRY_VCR).ToString
            strSubmitRefField = dsAlertData.Tables(AlertData._ALERTSETTING).Rows(0)(AlertData.ALRT_RF_FLD_VCR).ToString
            intTemplateID = CInt(dsAlertData.Tables(AlertData._ALERTSETTING).Rows(0)(AlertData.ALRT_TMPLT_ID))
            dsAlertData = objAlerts.GetTemplatePathByTemplateID(intTemplateID)
            strTemplateFilePath = dsAlertData.Tables(AlertData._ALERTTEMPLATE).Rows(0)(AlertData.ALRT_FL_PTH).ToString

            dsAlertRecord = objAlerts.GetAlertRecordByTransactionID(strAlertPendingQuery, strSubmitRefField, bv_strRefValue, "", bv_strDBKey)
            If dsAlertRecord.Tables("AlertTable").Rows.Count > 0 Then

                If bv_intActivityID = "145" Then
                    bv_strAlertSubject = String.Concat(dsAlertRecord.Tables("AlertTable").Rows(0).Item("CSTMR_CD"), "-", dsAlertRecord.Tables("AlertTable").Rows(0).Item("ACTVTY_NAM"), " ", "Invoice Rejected : ", bv_strRefValue)
                    
                End If
                swAlertData = ProcessTemplateFile(dsAlertRecord, CInt(bv_intActivityID), "", "", "", strTemplateFilePath, bv_strRefValue)

                pub_CreateAlert = objAlerts.CreatePHAlert(bv_intActivityID, bv_strRefValue, _
                                                          bv_intOrganisationID, bv_dtAlertDueDate, bv_strAlertSubject, _
                                                          swAlertData.ToString, bv_strAlertTo, _
                                                          bv_strAlertCC, False, bv_strUserName, _
                                                          datCreatedDate)
               
                pub_CreateAlert = objAlerts.CreatePQAlert(CInt(bv_intActivityID), bv_strRefValue, _
                                                          bv_intOrganisationID, bv_dtAlertDueDate, bv_strAlertSubject, _
                                                          swAlertData.ToString, bv_strAlertTo, _
                                                          bv_strAlertCC, bv_strUserName, datCreatedDate)
            Else
                Exit Function
            End If
        Catch ex As Exception
            Throw ex
        End Try


    End Function
#End Region

#Region "ProcessTemplateFile "
    Private Function ProcessTemplateFile(ByVal bv_dsAlertRecord As DataSet, ByVal bv_intActivtyId As Integer, _
                    ByVal bv_strTo As String, ByVal bv_strCC As String, ByVal bv_strSubject As String, _
                    ByVal bv_strTemplateFilePath As String, ByVal bv_strDBKey As String) As StringWriter
        Try
            Dim objXMLDoc As XmlDataDocument
            bv_dsAlertRecord.DataSetName = "AlertData"
            bv_dsAlertRecord.EnforceConstraints = False
            objXMLDoc = New XmlDataDocument(bv_dsAlertRecord)
            Dim xdoc As New XmlDocument
            Dim objAlert As New Alerts
            Dim objXSLTrans As New Xsl.XslCompiledTransform
            Dim swFileWriter As New System.IO.StringWriter
            Dim XSLargsList As New Xsl.XsltArgumentList
            Dim blnSend As Boolean = False
            Dim intTemplateId As Integer = 0
            Dim strTemplateFilePath As String = String.Empty
            Dim xeCols As XmlAttribute
            Dim xeNodeElement As XmlElement = Nothing
            Dim xeDataElement As XmlElement = Nothing
            Dim xeChildDataElement As XmlElement
            'Dim strDomainPath As String = AppDomain.CurrentDomain.BaseDirectory.ToString
            Dim datColumn As DateTime

            'Get Template
            Dim intTransactionID As Integer = 1
            strTemplateFilePath = String.Concat(Config.pub_GetAppConfigValue("TemplatesFolder").ToString(), bv_strTemplateFilePath)

            objXSLTrans.Load(strTemplateFilePath)

            xeNodeElement = objXMLDoc.CreateElement(PARENT_ELEMENT)

            For Each drRow As DataRow In bv_dsAlertRecord.Tables("AlertTable").Rows
                xeDataElement = objXMLDoc.CreateElement(CHILD_ELEMENT)
                For Each dcCol As DataColumn In bv_dsAlertRecord.Tables("AlertTable").Columns
                    xeCols = objXMLDoc.CreateAttribute(dcCol.ColumnName)
                    If Not IsDBNull(drRow.Item(dcCol.ColumnName)) Then

                        If dcCol.DataType Is GetType(Date) Then
                            datColumn = CDate(drRow.Item(dcCol.ColumnName).ToString)
                            xeCols.InnerText = datColumn.ToString("dd-MMM-yyyy").ToUpper()
                        Else
                            xeCols.InnerText = drRow.Item(dcCol.ColumnName).ToString
                        End If
                    Else
                        xeCols.InnerText = ""
                    End If

                    xeDataElement.Attributes.Append(xeCols)
                    xeChildDataElement = objXMLDoc.CreateElement(dcCol.ColumnName)
                    If Not IsDBNull(drRow.Item(dcCol.ColumnName)) Then

                        If dcCol.DataType Is GetType(Date) Then
                            datColumn = CDate(drRow.Item(dcCol.ColumnName).ToString)
                            xeChildDataElement.InnerText = datColumn.ToString("dd-MMM-yyyy").ToUpper()
                        Else
                            xeChildDataElement.InnerText = drRow.Item(dcCol.ColumnName).ToString
                        End If

                    Else
                        xeChildDataElement.InnerText = ""
                    End If

                    xeDataElement.AppendChild(xeChildDataElement)
                Next
                xeNodeElement.AppendChild(xeDataElement)
            Next

            objXMLDoc.DocumentElement.AppendChild(xeNodeElement)

            'Adding parameter to XSLT
            XSLargsList.AddParam("CurrentDate", "", DateTime.Now.ToString("dd-MMM-yyyy").ToUpper())
            XSLargsList.AddParam("invoiceno", "", bv_strDBKey.ToUpper())

            objXSLTrans.Transform(objXMLDoc, XSLargsList, swFileWriter)

            Return swFileWriter
        Catch ex As Exception
            Throw ex
        Finally
            'objXMLDoc = Nothing
        End Try

    End Function
#End Region

#Region "SendAlert"
    Public Sub pub_SendAlert()
        Try
            Dim dsAlert As New AlertDataSet
            Dim objAlert As New Alerts()
            Dim objEmail As New Email

            'Get Alert data from PQAlert
            dsAlert = objAlert.GetPQAlerts()

            If dsAlert.Tables(AlertData._PQALERT) IsNot Nothing Then
                For Each drAlertData As DataRow In dsAlert.Tables(AlertData._PQALERT).Rows
                    Try
                        Dim i64AlertID As Int64 = CLng(drAlertData.Item(AlertData.ALRT_BIN).ToString)
                        Dim blnMailStatus As Boolean = False
                        Dim dtAlertDueDate As DateTime
                        Dim strCC As String = ""
                        Dim strTo As String = ""
                        Dim strSubject As String = ""
                        Dim strBody As String = ""

                        If Not IsDBNull(drAlertData.Item(AlertData.ALRT_DE_DT)) Then
                            dtAlertDueDate = CDate(drAlertData.Item(AlertData.ALRT_DE_DT).ToString)
                        End If
                        If Not IsDBNull(drAlertData.Item(AlertData.ALRT_CC)) Then
                            strCC = drAlertData.Item(AlertData.ALRT_CC).ToString
                        End If
                        If Not IsDBNull(drAlertData.Item(AlertData.ALRT_TO)) Then
                            strTo = drAlertData.Item(AlertData.ALRT_TO).ToString
                        End If
                        If Not IsDBNull(drAlertData.Item(AlertData.ALRT_SBJCT_VCR)) Then
                            strSubject = drAlertData.Item(AlertData.ALRT_SBJCT_VCR).ToString
                        End If
                        If Not IsDBNull(drAlertData.Item(AlertData.ALRT_CNTNT_XML)) Then
                            strBody = drAlertData.Item(AlertData.ALRT_CNTNT_XML).ToString
                            If CLng(drAlertData.Item(AlertData.ACTVTY_ID).ToString) = 145 Then
                                strBody = strBody.Replace("&lt;", "<")
                                strBody = strBody.Replace("&gt;", "/>")
                            End If
                            End If

                        blnMailStatus = pub_Send_Alert(strTo, strCC, _
                                            strSubject, strBody)
                        If blnMailStatus Then
                            objAlert.DeletePQAlert(i64AlertID)
                            objAlert.UpdatePHAlertStatus(i64AlertID)
                        End If
                    Catch ex As Exception
                        Throw ex
                    End Try
                Next
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
            Throw ex
        End Try
    End Sub
#End Region

#Region "Sending Email using chilkat component with CC"
    ''' <summary>
    ''' Name     : pub_Send_Email
    ''' Purpose  : pub_Send_Email function is used to Send Email 
    ''' </summary>
    ''' <param name="bv_strToMailIDs"></param>
    ''' <param name="bv_strCCEmailIDs"></param>
    ''' <param name="bv_strSubject"></param>
    ''' <param name="bv_strBody"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pub_Send_Alert(ByVal bv_strToMailIDs As String, _
                                   ByVal bv_strCCEmailIDs As String, _
                                   ByVal bv_strSubject As String, _
                                   ByVal bv_strBody As String) As Boolean
        Dim objChilkat As New Chilkat.MailMan
        Dim objEmail As New Chilkat.Email

        Try
            objChilkat.UnlockComponent("UiinterchMAILQ_FmPBEfzL8SkF")
            objChilkat.SmtpHost = Config.pub_GetAppConfigValue("SmtpMailServer")

            'Adding From EmailID
            objEmail.From = Config.pub_GetAppConfigValue("FromMailID")

            If Config.pub_GetAppConfigValue("SmtpAuthEnabled") = "true" Then
                objChilkat.SmtpUsername = Config.pub_GetAppConfigValue("SmtpUserName")
                objChilkat.SmtpPassword = Config.pub_GetAppConfigValue("SmtpPassword")
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
            Dim strCCEmailIds As String()
            'Dim macTo As New System.Net.Mail.MailAddressCollection
            strCCEmailIds = bv_strCCEmailIDs.Split(CChar(","))

            If strCCEmailIds.Length = 0 Then
                strCCEmailIds(0) = bv_strCCEmailIDs
            End If

            For Each strCCEmail As String In strCCEmailIds
                objEmail.AddCC(strCCEmail, strCCEmail)
            Next

            'Email Subject
            objEmail.Subject = bv_strSubject

            'Adding Foot note
            Dim sbrFootNote As New Text.StringBuilder
            sbrFootNote.Append("<DIV style=""font-family:Arial;font-size:10pt"">")
            sbrFootNote.Append("<DIV>")
            sbrFootNote.Append(bv_strBody)
            sbrFootNote.Append("</DIV>")
            objEmail.SetHtmlBody(sbrFootNote.ToString())
            'Dim thread As Threading.Thread
            Try
                'Dim job As New Threading.ThreadStart(AddressOf AsyncSend)
                'thread = New Threading.Thread(job)
                'thread.Start()
                'Return True
                Dim blnSendMail As Boolean = False

                blnSendMail = objChilkat.SendEmail(objEmail)
                If Not blnSendMail Then
                    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, objChilkat.LastErrorText)
                End If
                Return blnSendMail
            Catch ex As Exception
                Throw ex
                iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
            Finally
                objChilkat = Nothing
            End Try
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
            Throw ex
        End Try
    End Function

#End Region

End Class
