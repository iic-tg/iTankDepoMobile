Option Strict On

Imports System.Xml
Imports System.Xml.Xsl

Partial Class Tracking_SendEmail
    Inherits Pagebase

#Region "Declarations"
    Dim dsBulkEmailDetail As New BulkEmailDataSet
    Dim dtBulkEmailDetail As DataTable
    Private Const BULKEMAILDETAIL As String = "BULKEMAILDETAIL"
    Private Const BULKEMAIL As String = "BULKEMAIL"
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Tracking/BulkEmail.js", MyBase.Page)
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
                    '   divHTML.InnerHtml = pvt_GenerateXSLT(dsBulkEmailDetail)
                    Dim objCommon As New CommonData
                    Dim objBulkEmail As New BulkEmail
                    Dim strcleaningMail As String = String.Empty
                    Dim bln035Key As Boolean = False
                    Dim bln074Key As Boolean = False
                    Dim str035Value As String = String.Empty
                    Dim str074Value As String = String.Empty
                    Dim strRepairEstimateMail As String = String.Empty
                    Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
                   
                    Dim customerId As Integer = CInt(Request.QueryString("CustomerID"))
                    Dim dtCustomer As New DataTable
                    Dim dtRepairEstimate As New DataTable
                    Dim dtCleaning As New DataTable
                    If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                        intDPT_ID = CInt(objCommon.GetHeadQuarterID())
                    End If
                    hdnCustomer.Value = CStr(customerId)
                    dsBulkEmailDetail = CType(RetrieveData(BULKEMAIL), BulkEmailDataSet)
                    Dim blnCleaning As Boolean = False
                    Dim blnRepairEstimate As Boolean = False
                    Dim blnResend As Boolean = False

                    For Each dr As DataRow In dsBulkEmailDetail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Select(BulkEmailData.CHECKED & "='True'")
                        If dr.Item(BulkEmailData.ACTVTY_NAM).ToString.Contains("Cleaning") Then
                            blnCleaning = True
                        End If
                        If dr.Item(BulkEmailData.ACTVTY_NAM).ToString.Contains("Repair Estimate") Or dr.Item(BulkEmailData.ACTVTY_NAM).ToString.Contains("Repair Approval") Or dr.Item(BulkEmailData.ACTVTY_NAM).ToString.Contains("Repair Completion") Then
                            blnRepairEstimate = True
                        End If
                        If blnCleaning = True AndAlso blnRepairEstimate = True Then
                            Exit For
                        End If
                    Next
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
                    'Dim dsBulkEmailCustomer As New BulkEmailDataSet
                    'dsBulkEmailCustomer.Tables(BulkEmailData._CUSTOMER).Rows.Clear()
                    dtCustomer = objBulkEmail.pub_GetCustomerDetail(intDPT_ID, customerId).Tables(BulkEmailData._CUSTOMER)
                    'dsBulkEmailDetail.Tables(BulkEmailData._CUSTOMER).Clear()
                    'dsBulkEmailDetail.Merge(dsBulkEmailCustomer.Tables(BulkEmailData._CUSTOMER))
                    If Not (dtCustomer.Rows(0).Item(BulkEmailData.RPRTNG_EML_ID)) Is DBNull.Value Then
                        strcleaningMail = (dtCustomer.Rows(0).Item(BulkEmailData.RPRTNG_EML_ID)).ToString
                    End If
                    If Not (dtCustomer.Rows(0).Item(BulkEmailData.RPR_TCH_EML_ID)) Is DBNull.Value Then
                        strRepairEstimateMail = (dtCustomer.Rows(0).Item(BulkEmailData.RPR_TCH_EML_ID)).ToString
                    End If
                    If blnRepairEstimate = False And blnCleaning = True Then
                        txtTo.Text = strcleaningMail
                        txtSubject.Text = String.Concat(Config.pub_GetAppConfigValue("BulkEmailSubject"), " - Cleaning")
                    ElseIf blnCleaning = True And blnRepairEstimate = True Then
                        txtTo.Text = String.Concat(strcleaningMail, ", ", strRepairEstimateMail)
                        txtSubject.Text = String.Concat(Config.pub_GetAppConfigValue("BulkEmailSubject"), " - Cleaning, Repair")
                    ElseIf blnRepairEstimate = True And blnCleaning = False Then
                        txtTo.Text = strRepairEstimateMail
                        txtSubject.Text = String.Concat(Config.pub_GetAppConfigValue("BulkEmailSubject"), " - Repair")
                    Else
                        txtTo.Text = String.Empty
                    End If
                    If txtSubject.Text.Contains("Repair") Then
                        str035Value = objCommon.GetConfigSetting("035", bln035Key)
                        If bln035Key Then
                            txtBCC.Text = str035Value
                        End If
                    End If
                    'If txtSubject.Text.Contains("Repair") Then
                    str074Value = objCommon.GetConfigSetting("074", bln074Key)
                    If bln074Key Then
                        txtCC.Text = str074Value
                    End If
                    'End If
                    If blnResend Then
                        txtSubject.Text = String.Concat("FW:", Config.pub_GetAppConfigValue("BulkEmailSubject"), " - ", strActivitySubject)
                    End If
                    txtFrom.Text = Config.pub_GetAppConfigValue("BulkEmailFromMailID")
                    'txtSubject.Text = Config.pub_GetAppConfigValue("BulkEmailSubject")
                    'txtFrom.Text = FromMailId
                    Dim objCommondata As New CommonData
                    Dim strUserName As String = objCommondata.GetCurrentUserName()
                    Dim sbrBody As New StringBuilder
                    Dim strBulkEMailDetail As String = String.Empty
                    Dim strEmlSgn As String = String.Concat(vbCrLf, vbCrLf, strUserName)
                    If blnResend AndAlso dsBulkEmailDetail.Tables(BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND).Rows.Count > 0 Then
                        With dsBulkEmailDetail.Tables(BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND).Rows(0)
                            strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "-----------------------------------------------------------------------------------------------------")
                            strEmlSgn = String.Concat(strEmlSgn, vbCrLf, vbCrLf, "From: ", .Item(BulkEmailData.FRM_EML))
                            If Not IsDBNull(.Item(BulkEmailData.SNT_DT)) Then
                                strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "Sent: ", CDate(.Item(BulkEmailData.SNT_DT)).ToString("dddd, d MMMM yyyy hh:mm tt"))
                            Else
                                strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "Sent: ", "")
                            End If
                            strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "To: ", .Item(BulkEmailData.TO_EML))
                            strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "CC: ", .Item(BulkEmailData.CC_EML))
                            strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "Bcc: ", .Item(BulkEmailData.BCC_EML))
                            strEmlSgn = String.Concat(strEmlSgn, vbCrLf, "Subject: ", .Item(BulkEmailData.SBJCT_VC))
                            '  strBulkEMailDetail = pvt_buildHTMLTable(dsBulkEmailDetail.Tables(BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND).Rows(0))
                            '   strEmlSgn = String.Concat(strEmlSgn, "<br/>", "<br/>", strBulkEMailDetail)
                            strEmlSgn = String.Concat(strEmlSgn, vbCrLf, vbCrLf, .Item(BulkEmailData.BDY_VC))
                        End With
                    End If
                    sbrBody.Append(strEmlSgn)
                    'sbrBody.Append("</b>")
                    txtBody.Text = sbrBody.ToString
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
                    pvt_SaveDetails(e.GetCallbackValue("CustomerID"), _
                                    e.GetCallbackValue("FromEmail"), _
                                    e.GetCallbackValue("ToEmail"), _
                                    e.GetCallbackValue("CCEmail"), _
                                    e.GetCallbackValue("BCCEmail"), _
                                    e.GetCallbackValue("Subject"), _
                                    e.GetCallbackValue("EmailBody"), _
                                    e.GetCallbackValue("MailMode"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_SaveDetails"

    Private Sub pvt_SaveDetails(ByVal CustomerID As String, _
                               ByVal FromMailID As String, _
                               ByVal ToEmailID As String, _
                                ByVal CCEmailID As String, _
                               ByVal BCCEmailID As String, _
                               ByVal Subject As String, _
                               ByVal EmailBody As String, _
                               ByVal bv_strMailMode As String)
        Try
            Dim dsSendEmail As New BulkEmailDataSet
            Dim objBulkEmail As New BulkEmail
            Dim objCommon As New CommonData
            Dim drEmail As DataRow
            Dim drEmailDetail As DataRow() = Nothing
            Dim dtEmail As New DataTable
            Dim dtCustomer As New DataTable
            Dim dtRepairEstimate As New DataTable
            Dim dtCleaning As New DataTable
            Dim dtDepot As New DataTable
            Dim giTransno As String = String.Empty
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            dsBulkEmailDetail = CType(RetrieveData(BULKEMAIL), BulkEmailDataSet)
            Dim drSendemail As DataRow()
            Dim blnResend As Boolean = False
            Dim str_037KeyValue As String = String.Empty
            Dim bln_037KeyValue As Boolean = False
            Dim str_060KeyValue As String = String.Empty
            Dim bln_060KeyValue As Boolean = False

            If Not bv_strMailMode = "Resend" Then
                drSendemail = dsBulkEmailDetail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Select(BulkEmailData.CHECKED & "='True'")
                If Not drSendemail.Length > 0 Then
                    pub_SetCallbackStatus(False)
                    pub_SetCallbackError("Please Select Atleast One Equipment.")
                    Exit Sub
                End If
            End If
            dtEmail = dsBulkEmailDetail.Tables(BulkEmailData._BULK_EMAIL_DETAIL).Clone()
            Dim lngCreated As Long
            dtDepot = objBulkEmail.pub_GetDepot(intDPT_ID).Tables(BulkEmailData._DEPOT)

            If bv_strMailMode = "Resend" Then
                blnResend = True
                drEmailDetail = dsBulkEmailDetail.Tables(BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND).Select(String.Concat(BulkEmailData.BLK_EML_ID, " IS NOT NULL"))

            Else
                blnResend = False
                drEmailDetail = dsBulkEmailDetail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Select(BulkEmailData.CHECKED & "='True'")
            End If

            For Each dr As DataRow In drEmailDetail
                drEmail = dtEmail.NewRow()
                drEmail.Item(BulkEmailData.BLK_EML_DTL_ID) = CommonWeb.GetNextIndex(dtEmail, BulkEmailData.BLK_EML_DTL_ID)
                drEmail.Item(BulkEmailData.BLK_EML_ID) = lngCreated
                drEmail.Item(BulkEmailData.EQPMNT_NO) = dr.Item(BulkEmailData.EQPMNT_NO)

                If Not dr.Item(BulkEmailData.EQPMNT_STTS_ID) Is DBNull.Value AndAlso dr.Item(BulkEmailData.EQPMNT_STTS_ID).ToString() = "19" Then
                    drEmail.Item(BulkEmailData.EQPMNT_STTS_ID) = 9
                    drEmail.Item(BulkEmailData.EQPMNT_STTS_CD) = "AAR"
                Else
                    drEmail.Item(BulkEmailData.EQPMNT_STTS_ID) = dr.Item(BulkEmailData.EQPMNT_STTS_ID)
                    drEmail.Item(BulkEmailData.EQPMNT_STTS_CD) = dr.Item(BulkEmailData.EQPMNT_STTS_CD)
                End If
                'drEmail.Item(BulkEmailData.EQPMNT_STTS_ID) = dr.Item(BulkEmailData.EQPMNT_STTS_ID)
                'drEmail.Item(BulkEmailData.EQPMNT_STTS_CD) = dr.Item(BulkEmailData.EQPMNT_STTS_CD)
                drEmail.Item(BulkEmailData.ACTVTY_NAM) = dr.Item(BulkEmailData.ACTVTY_NAM)
                drEmail.Item(BulkEmailData.GI_TRNSCTN_NO) = dr.Item(BulkEmailData.GI_TRNSCTN_NO)
                giTransno = dr.Item(BulkEmailData.GI_TRNSCTN_NO).ToString
                drEmail.Item(BulkEmailData.CHECKED) = dr.Item(BulkEmailData.CHECKED)
                If dr.Item(BulkEmailData.ACTVTY_NAM).ToString = "Cleaning" Then
                    dtCleaning = objBulkEmail.GetCleaningDetails(intDPT_ID, CInt(dr.Item(BulkEmailData.ACTVTY_NO))).Tables(BulkEmailData._CLEANING)
                    For Each drCleaning As DataRow In dtCleaning.Select(String.Concat(BulkEmailData.GI_TRNSCTN_NO, "='", _
                                                                                     dr.Item(BulkEmailData.GI_TRNSCTN_NO), "'"))
                        drEmail.Item(BulkEmailData.ACTVTY_NO) = drCleaning.Item(BulkEmailData.CLNNG_ID)
                        drEmail.Item(BulkEmailData.CRRNCY_ID) = DBNull.Value
                        drEmail.Item(BulkEmailData.AMNT_NC) = DBNull.Value
                        Exit For
                    Next
                Else

                    dtRepairEstimate = objBulkEmail.pub_GetRepairEstimate(intDPT_ID, CInt(dr.Item(BulkEmailData.ACTVTY_NO)), giTransno).Tables(BulkEmailData._REPAIR_ESTIMATE)
                    drEmail.Item(BulkEmailData.CRRNCY_ID) = dtDepot.Rows(0).Item(BulkEmailData.CRRNCY_ID)
                    For Each drRepairEstimateDetail As DataRow In dtRepairEstimate.Select(String.Concat(BulkEmailData.GI_TRNSCTN_NO, "='", _
                                                                                     dr.Item(BulkEmailData.GI_TRNSCTN_NO), "'", " AND ", BulkEmailData.ACTVTY_NM, "='", dr.Item(BulkEmailData.ACTVTY_NAM), "'"))
                        drEmail.Item(BulkEmailData.AMNT_NC) = CommonUIs.iDbl(drRepairEstimateDetail.Item(BulkEmailData.ESTMTN_TTL_NC))
                        drEmail.Item(BulkEmailData.ACTVTY_NO) = drRepairEstimateDetail.Item(BulkEmailData.RPR_ESTMT_ID)
                        Exit For
                    Next
                End If
                dtEmail.Rows.Add(drEmail)
            Next

            dsBulkEmailDetail.Tables(BulkEmailData._BULK_EMAIL_DETAIL).Clear()
            dsBulkEmailDetail.Tables(BulkEmailData._BULK_EMAIL_DETAIL).Merge(dtEmail)
            Dim strAttachmentPath As String = String.Empty
            'REPAIR FLOW SETTING: 037
            'CR-001 (AWE_TO_AWP)
            str_037KeyValue = objCommon.GetConfigSetting("037", bln_037KeyValue)
            If bln_037KeyValue = False Then
                str_037KeyValue = String.Empty
            End If
            str_060KeyValue = objCommon.GetConfigSetting("060", bln_060KeyValue)
            If bln_060KeyValue = False Then
                str_060KeyValue = String.Empty
            End If



            lngCreated = objBulkEmail.pub_BulkEmailCreateBulkEmail(CLng(CustomerID), _
                                                                   FromMailID, _
                                                                   ToEmailID, _
                                                                   CCEmailID, _
                                                                   BCCEmailID, _
                                                                   Subject, _
                                                                   EmailBody, _
                                                                   intDPT_ID, _
                                                                   objCommon.GetCurrentUserName(), _
                                                                   CDate(objCommon.GetCurrentDate()), _
                                                                   bv_strMailMode, _
                                                                   blnResend, _
                                                                   str_037KeyValue, _
                                                                   str_060KeyValue, _
                                                                   dsBulkEmailDetail)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", "Bulk Email Sent Successfully")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GenerateXSLT"
    ''' <summary>
    ''' This method is used generate XSLT by passing dataset
    ''' </summary>
    ''' <param name="bv_dsAttachment">Denotes Estimate dataset</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function pvt_GenerateXSLT(ByVal bv_dsBulkEmailDetail As BulkEmailDataSet) As String
        Dim strinnerHTML As String = ""
        Dim objMemoryStream As New System.IO.MemoryStream
        Dim objhtmlMemoryStream As New System.IO.MemoryStream
        Try
            bv_dsBulkEmailDetail.DataSetName = "BulkEmailDataSet"
            bv_dsBulkEmailDetail.Tables(BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND).TableName = "V_BULK_EMAIL_DETAIL_RESEND"

            'bv_dsBulkEmailDetail.Tables.Remove(BulkEmailData._V_BULKEMAIL_TRACKING)
            'bv_dsBulkEmailDetail.Tables.Remove(BulkEmailData._BULK_EMAIL_DETAIL)
            'bv_dsBulkEmailDetail.Tables.Remove(BulkEmailData._CUSTOMER)
            'bv_dsBulkEmailDetail.Tables.Remove(BulkEmailData._BULK_EMAIL)
            'bv_dsBulkEmailDetail.Tables.Remove(BulkEmailData._REPAIR_ESTIMATE)
            'bv_dsBulkEmailDetail.Tables.Remove(BulkEmailData._DEPOT)
            'bv_dsBulkEmailDetail.Tables.Remove(BulkEmailData._CLEANING)

            bv_dsBulkEmailDetail.WriteXml(objMemoryStream, XmlWriteMode.DiffGram)
            Dim doc As New XmlDocument()

            objMemoryStream.Position = 0
            doc.Load(objMemoryStream)

            Dim objXmlWriterSetting As New XmlWriterSettings
            objXmlWriterSetting.ConformanceLevel = ConformanceLevel.Fragment

            Dim objwriter As XmlWriter = XmlWriter.Create(objhtmlMemoryStream, objXmlWriterSetting)
            Dim transform As New XslCompiledTransform()

            transform.Load(Server.MapPath("../Templates/BulkEmailResend.xslt"))

            transform.Transform(doc.CreateNavigator(), Nothing, objwriter)
            objwriter.Close()

            objhtmlMemoryStream.Flush()
            objhtmlMemoryStream.Position = 0
            Dim strStream As New IO.StreamReader(objhtmlMemoryStream)

            strinnerHTML = strStream.ReadToEnd()

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex.Message)
        Finally
            objMemoryStream = Nothing
            objhtmlMemoryStream = Nothing
        End Try
        Return strinnerHTML
    End Function
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

End Class
