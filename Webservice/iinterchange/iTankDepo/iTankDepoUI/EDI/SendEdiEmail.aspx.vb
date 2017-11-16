Option Strict On

Imports System.Xml
Imports System.Xml.Xsl
Imports iInterchange.Framework.Common
Imports System.IO

Partial Class EDI_SendEdiEmail
    Inherits Pagebase
#Region "Declarations"
    Dim dsEdiEmail As New EDIDataSet
    Dim dtBulkEmailDetail As DataTable
    Private Const BULKEMAILDETAIL As String = "BULKEMAILDETAIL"
    Private Const EDI As String = "EDI"
    Private Const BULKEMAIL As String = "BULKEMAIL"
    Dim dsBulkEmail As New BulkEmailDataSet
    Dim dsBulkEmailDetail As New BulkEmailDataSet
    Dim dsCustomer As New CustomerDataSet
    Dim objISO As New Customers
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/EDI/EDI.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/EDI/EDIEmail.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack AndAlso Not Page.IsCallback Then
                If Not (Request.QueryString("CustomerID")) Is Nothing Then
                    Dim objCommon As New CommonData
                    Dim objBulkEmail As New BulkEmail
                    Dim objBEDIEmail As New EDI
                    Dim strcleaningMail As String = String.Empty
                    hdnEDI.Value = Request.QueryString("EDINo")
                    Dim bln035Key As Boolean = False
                    Dim str035Value As String = String.Empty
                    Dim strRepairEstimateMail As String = String.Empty
                    Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
                    Dim customerId As Integer = CInt(Request.QueryString("CustomerID"))
                    Dim dtCustomer As New DataTable
                    Dim dtRepairEstimate As New DataTable
                    Dim dtCleaning As New DataTable
                    Dim strFileName As String = Request.QueryString("FileName")
                    'Multilocation
                    If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                        intDPT_ID = CInt(objCommon.GetHeadQuarterID())
                    End If

                    hdnCustomer.Value = CStr(customerId)
                    dsEdiEmail = CType(RetrieveData(EDI), EDIDataSet)
                    Dim blnCleaning As Boolean = False
                    Dim blnRepairEstimate As Boolean = False
                    Dim blnResend As Boolean = False
                    Dim strActivitySubject As String = String.Empty
                    If Not (Request.QueryString("ActivityName")) Is Nothing Then
                        Dim strActivityName As String = Request.QueryString("ActivityName")
                        If strActivityName.Contains("Cleaning") Then
                            blnCleaning = True
                            strActivitySubject = strActivityName
                        ElseIf strActivityName.Contains("Repair Estimate") Or strActivityName.Contains("Repair Approval") Or strActivityName.Contains("Repair Completion") Then
                            blnRepairEstimate = True
                            strActivitySubject = "Repair"
                        End If
                        blnResend = True
                    End If
                    dtCustomer = objBulkEmail.pub_GetCustomerDetail(intDPT_ID, customerId).Tables(BulkEmailData._CUSTOMER)
                    If dtCustomer.Rows(0).Item(BulkEmailData.RPRTNG_EML_ID) Is DBNull.Value Then
                        txtTo.Text = ""
                    Else
                        txtTo.Text = CStr(dtCustomer.Rows(0).Item(BulkEmailData.RPRTNG_EML_ID))
                    End If

                    Dim strSubjectwithCustomer As String = Request.QueryString("CustomerCD")
                    If Request.QueryString("FileName").Contains("Gatein") Then
                        strActivitySubject = "Gatein"
                        'End If
                    ElseIf Request.QueryString("FileName").Contains("Gateout") Then
                        strActivitySubject = "Gateout"
                    Else
                        strActivitySubject = "Repair Estimate"
                    End If

                    txtSubject.Text = String.Concat(strActivitySubject, " EDI File -  ", strSubjectwithCustomer)
                    str035Value = objCommon.GetConfigSetting("035", bln035Key)
                    If bln035Key Then
                        txtBCC.Text = str035Value
                    End If
                    If blnResend Then
                        txtSubject.Text = String.Concat("FW:", Config.pub_GetAppConfigValue("BulkEmailSubject"), " - ", strActivitySubject)
                    End If
                    txtFrom.Text = Config.pub_GetAppConfigValue("BulkEmailFromMailID")
                    Dim objCommondata As New CommonData
                    Dim strUserName As String = objCommondata.GetCurrentUserName()
                    Dim sbrBody As New StringBuilder
                    Dim strBulkEMailDetail As String = String.Empty
                    Dim strEmlSgn As String = String.Concat(vbCrLf, vbCrLf, strUserName)
                    dsEdiEmail = objBEDIEmail.pub_GetEDI_EmailDetails(CLng(hdnEDI.Value))
                    If dsEdiEmail.Tables(EDIData._EDI_EMAIL_DETAIL).Rows.Count > 0 Then
                        With dsEdiEmail.Tables(EDIData._EDI_EMAIL_DETAIL).Rows(0)
                            'strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "-----------------------------------------------------------------------------------------------------")
                            'strEmlSgn = String.Concat(strEmlSgn, vbCrLf, vbCrLf, "From: ", .Item(EDIData.FRM_EML))
                            'If Not IsDBNull(.Item(EDIData.SNT_DT)) Then
                            '    strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "Sent: ", CDate(.Item(EDIData.SNT_DT)).ToString("dddd, d MMMM yyyy hh:mm tt"))
                            'Else
                            '    strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "Sent: ", "")
                            'End If
                            'strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "To: ", .Item(EDIData.TO_EML))
                            'strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "Bcc: ", .Item(EDIData.BCC_EML))
                            'strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "Subject: ", .Item(EDIData.SBJCT_VC))
                            'strEmlSgn = String.Concat(strEmlSgn, vbCrLf, vbCrLf, .Item(EDIData.BDY_VC))
                            '  strFileName = String.Concat(strFileName, ",", .Item(EDIData.CSTMR_CD))
                        End With
                        txtSubject.Text = String.Concat("FW:", txtSubject.Text)
                    End If
                    sbrBody.Append(strEmlSgn)
                    txtBody.Text = sbrBody.ToString
                    dsCustomer = objISO.getISOCODEbyCustomer(CLng(customerId))
                    Dim strCustomerCode As String = String.Empty
                    If dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD") Is DBNull.Value Then
                        strCustomerCode = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("CSTMR_CD"))
                    Else
                        strCustomerCode = CStr(dsCustomer.Tables(CustomerData._V_CUSTOMER).Rows(0).Item("ISO_CD"))
                    End If

                    'strFileName = String.Concat(strFileName, ",", Request.QueryString("CustomerCD"))
                    strFileName = String.Concat(strFileName, ",", strCustomerCode)
                    ProcessCreateEmail(strUserName, strFileName)
                End If
                pvt_SetChangesMade()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        Try
            '  CommonWeb.pub_AttachDescMaxlength(txtBody, True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "SaveDetails"
                    pvt_SaveDetails(e.GetCallbackValue("EDINo"), _
                                    e.GetCallbackValue("CustomerID"), _
                                    e.GetCallbackValue("FromEmail"), _
                                    e.GetCallbackValue("ToEmail"), _
                                    e.GetCallbackValue("BCCEmail"), _
                                    e.GetCallbackValue("Subject"), _
                                    e.GetCallbackValue("EmailBody"), _
                                    e.GetCallbackValue("MailMode"), _
                                    e.GetCallbackValue("Attachment"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_SaveDetails"

    Private Sub pvt_SaveDetails(ByVal EdiId As String, _
                                ByVal CustomerID As String, _
                               ByVal FromMailID As String, _
                               ByVal ToEmailID As String, _
                               ByVal BCCEmailID As String, _
                               ByVal Subject As String, _
                               ByVal EmailBody As String, _
                               ByVal bv_strMailMode As String, _
                               ByVal bv_strAttachment As String)
        Try
            Dim dsSendEmail As New BulkEmailDataSet
            Dim objBulkEmail As New EDI
            Dim objCommon As New CommonData
            Dim drEmailDetail As DataRow() = Nothing
            Dim dtEmail As New DataTable
            Dim dtCustomer As New DataTable
            Dim objEmail As New iEmailHandler
            Dim dtRepairEstimate As New DataTable
            Dim dtCleaning As New DataTable
            Dim dtDepot As New DataTable
            Dim giTransno As String = String.Empty
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            dsEdiEmail = CType(RetrieveData(EDI), EDIDataSet)
            Dim blnResend As Boolean = False
            Dim str_037KeyValue As String = String.Empty
            Dim bln_037KeyValue As Boolean = False

            dtEmail = dsEdiEmail.Tables(EDIData._EDI_EMAIL_DETAIL).Clone()
            Dim lngCreated As Long
            dtDepot = objBulkEmail.pub_GetDepot(intDPT_ID).Tables(EDIData._DEPOT)

            str_037KeyValue = objCommon.GetConfigSetting("037", bln_037KeyValue)
            If bln_037KeyValue = False Then
                str_037KeyValue = String.Empty
            End If

            lngCreated = objBulkEmail.pub_EDIEmailCreate(CLng(EdiId), CLng(CustomerID), _
                                                                   FromMailID, _
                                                                   ToEmailID, _
                                                                   BCCEmailID, _
                                                                   Subject, _
                                                                   EmailBody, _
                                                                   intDPT_ID, _
                                                                   objCommon.GetCurrentUserName(), _
                                                                   CDate(objCommon.GetCurrentDate()), _
                                                                   bv_strMailMode, _
                                                                   blnResend)
            ' send cstmr _ cd  bv_strAttachment
            Dim strFilename As String()
            strFilename = bv_strAttachment.Split(CChar(","))
            bv_strAttachment = String.Concat(ConfigurationManager.AppSettings.Get("OutputFolder"), objCommon.GetDepotCD(), "\", strFilename(1), "\", strFilename(0))
            objEmail.pub_Send_Email(ToEmailID, Subject, EmailBody, bv_strAttachment)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", "EDI Email Sent Successfully")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region



#Region "pvt_buildHTMLTable"
    Private Function pvt_buildHTMLTable(ByVal drBulkEMailDetail As DataRow) As String
        Dim sbrHtmlTable As New StringBuilder
        Try
            'Dim dtBulkEmailDetailResend As New DataTable
            'dtBulkEmailDetailResend = bv_dsBulkEmail.Tables(BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND).Copy()
            sbrHtmlTable.Append("<table border=""2""><tr><td style=""font-weight:bold;"">Equipment No</td><td style=""font-weight:bold;"">Activity</td><td style=""font-weight:bold;"">Reference No</td><td style=""font-weight:bold;"">Amount</td><td style=""font-weight:bold;"">Currency</td></tr>")
            ' For Each drBulkEmailDetailResend As DataRow In dtBulkEmailDetailResend.Rows
            sbrHtmlTable.Append("<tr>")
            sbrHtmlTable.Append("<td>")
            sbrHtmlTable.Append(drBulkEMailDetail.Item(BulkEmailData.EQPMNT_NO))
            sbrHtmlTable.Append("</td>")
            sbrHtmlTable.Append("<td>")
            sbrHtmlTable.Append(drBulkEMailDetail.Item(BulkEmailData.ACTVTY_NAM))
            sbrHtmlTable.Append("</td>")
            sbrHtmlTable.Append("<td>")
            sbrHtmlTable.Append(drBulkEMailDetail.Item(BulkEmailData.GI_RF_NO))
            sbrHtmlTable.Append("</td>")
            sbrHtmlTable.Append("<td>")
            sbrHtmlTable.Append(drBulkEMailDetail.Item(BulkEmailData.AMNT_NC))
            sbrHtmlTable.Append("</td>")
            sbrHtmlTable.Append("<td>")
            sbrHtmlTable.Append(drBulkEMailDetail.Item(BulkEmailData.CRRNCY_CD))
            sbrHtmlTable.Append("</td>")
            sbrHtmlTable.Append("</tr>")
            ' Next
            sbrHtmlTable.Append("</table>")
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                     MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
        Return sbrHtmlTable.ToString()
    End Function
#End Region


#Region "ProcessCreateEmail"
    Private Sub ProcessCreateEmail(ByVal bv_strReportName As String, ByVal bv_strFilename As String)
        Try
            Dim objCommon As New CommonData
            Dim buffer(10000) As Byte
            hdnattachfile.Value = bv_strFilename
            Dim strFile As String()
            Dim strDepotCode As String = objCommon.GetDepotCD()
            strFile = bv_strFilename.Split(CChar(","))
            lnkAttachment.InnerText = strFile(0)
            Dim strFilepath As String = String.Concat("EDI\OUTPUT\", strDepotCode, "\", strFile(1), "\", strFile(0))
            ' lnkAttachment.Attributes.Add("href", String.Concat("../download.ashx?FL_NM=", strFile(0)))
            lnkAttachment.Attributes.Add("href", String.Concat("../download.ashx?FL_NM=", strFilepath))

        Catch ex As Exception
            iInterchange.iTankDepo.Business.Common.iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                            ex)
        End Try
    End Sub
#End Region

End Class
