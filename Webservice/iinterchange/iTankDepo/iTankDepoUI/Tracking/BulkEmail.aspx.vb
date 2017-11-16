Option Strict On

Imports System.Xml
Imports System.Xml.Xsl


Partial Class Tracking_BulkEmail
    Inherits Pagebase
#Region "Declarations"
    Dim dsBulkEmail As New BulkEmailDataSet
    Private Const BULKEMAIL As String = "BULKEMAIL"
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            'divTest.InnerHtml = "<html><body style=""font-family:verdana;font-size:8pt;""><table cellpadding=""1"" cellspacing=""1""><tr><td colspan=""2"">Bulk Email for the following Containers</td></tr><tr><td /></tr><tr><td colspan=""2""><table cellpadding=""0"" cellspacing=""0"" border=""1""><tr style=""font-weight:bold""><td>Equipment No</td></tr></table></td></tr><tr><td /></tr></table></body></html>"
            '  divTest.InnerHtml = pvt_GenerateXSLT(dsBulkEmail).ToString
            pvt_SetChangesMade()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Tracking/BulkEmail.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "GetSearchDetails"
                    pvt_GetSearchDetails(e.GetCallbackValue("Customer"), _
                                         e.GetCallbackValue("EquipmentNo"), _
                                         e.GetCallbackValue("InDateFrom"), _
                                         e.GetCallbackValue("InDateTo"), _
                                         e.GetCallbackValue("ActivityId"), _
                                         e.GetCallbackValue("EventDateFrom"), _
                                         e.GetCallbackValue("EventDateTo"), _
                                         e.GetCallbackValue("Email"))
                Case "GetSelectedValues"
                    pvt_GetSelectedValues(e.GetCallbackValue("WFData"))
                Case "ValidateDate"
                    pvt_ValidateDate(e.GetCallbackValue("EventDateFrom"), _
                                     e.GetCallbackValue("EventDateTo"))
                Case "ValidateDateTo"
                    pvt_ValidateDateTo(e.GetCallbackValue("EventDateFrom"), _
                                     e.GetCallbackValue("EventDateTo"))
                Case "ValidateInDate"
                    pvt_ValidateInDate(e.GetCallbackValue("EventDateFrom"), _
                                     e.GetCallbackValue("EventDateTo"))
                Case "ValidateInDateTo"
                    pvt_ValidateInDateTo(e.GetCallbackValue("EventDateFrom"), _
                                     e.GetCallbackValue("EventDateTo"))
                Case "GetBulkEmailResend"
                    pvt_GetBulkEmailResend(e.GetCallbackValue("GateinTransactionNo"), _
                                           e.GetCallbackValue("EquipmentNo"), _
                                           e.GetCallbackValue("ActivityName"))
            End Select

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetBulkEmailResend"
    Private Sub pvt_GetBulkEmailResend(ByVal bv_strGateinTransactionNo As String, _
                                       ByVal bv_strEquipmentNo As String, _
                                       ByVal bv_strActivityName As String)

        Try
            Dim dtBulkEmailDetail As New DataTable
            Dim objBulkEmail As New BulkEmail
            dsBulkEmail = CType(RetrieveData(BULKEMAIL), BulkEmailDataSet)
            dsBulkEmail.Tables(BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND).Clear()
            dtBulkEmailDetail = objBulkEmail.pub_GetBulkEmailDetailReSendbyTransactionNo(bv_strGateinTransactionNo, bv_strEquipmentNo, bv_strActivityName).Tables(BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND)
            dsBulkEmail.Tables(BulkEmailData._V_BULK_EMAIL_DETAIL_RESEND).Merge(dtBulkEmailDetail)
            CacheData(BULKEMAIL, dsBulkEmail)
            pub_SetCallbackReturnValue("AttachmentPath", dtBulkEmailDetail.Rows(0).Item(BulkEmailData.ATTCHMNT_PTH).ToString())
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateInDate"
    Private Sub pvt_ValidateInDate(ByVal bv_strEventFromDate As String, ByVal bv_strEventToDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim blnPreviousDateValid As Boolean = False
            Dim objCommonUI As New CommonUI
            Dim dtPreviousDate As DateTime = Nothing
            Dim blnCompareValid As Boolean = False
            Dim objCommon As New CommonData

            If ((CDate(bv_strEventToDate) < CDate(bv_strEventFromDate))) Then
                blnDateValid = True
            End If
            Dim ActivityDate As DateTime = CDate(bv_strEventToDate)

            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("In Date From Should be lesser than or equal to In Date To (", ActivityDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackReturnValue("Error", "")
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateDateTo"
    Private Sub pvt_ValidateDateTo(ByVal bv_strEventFromDate As String, ByVal bv_strEventToDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim blnPreviousDateValid As Boolean = False
            Dim objCommonUI As New CommonUI
            Dim dtPreviousDate As DateTime = Nothing
            Dim blnCompareValid As Boolean = False
            Dim objCommon As New CommonData

            If ((CDate(bv_strEventToDate) < CDate(bv_strEventFromDate))) Then
                blnDateValid = True
            End If
            Dim ActivityDate As DateTime = CDate(bv_strEventFromDate)

            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Activity Date To Should be greater than or equal to Activity Date From (", ActivityDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackReturnValue("Error", "")
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateInDateTo"
    Private Sub pvt_ValidateInDateTo(ByVal bv_strEventFromDate As String, ByVal bv_strEventToDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim blnPreviousDateValid As Boolean = False
            Dim objCommonUI As New CommonUI
            Dim dtPreviousDate As DateTime = Nothing
            Dim blnCompareValid As Boolean = False
            Dim objCommon As New CommonData

            If ((CDate(bv_strEventToDate) < CDate(bv_strEventFromDate))) Then
                blnDateValid = True
            End If
            Dim ActivityDate As DateTime = CDate(bv_strEventFromDate)

            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("In Date To  Should be greater than or equal to In Date From (", ActivityDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackReturnValue("Error", "")
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateDate"
    Private Sub pvt_ValidateDate(ByVal bv_strEventFromDate As String, ByVal bv_strEventToDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim blnPreviousDateValid As Boolean = False
            Dim objCommonUI As New CommonUI
            Dim dtPreviousDate As DateTime = Nothing
            Dim blnCompareValid As Boolean = False
            Dim objCommon As New CommonData

            If ((CDate(bv_strEventToDate) < CDate(bv_strEventFromDate))) Then
                blnDateValid = True
            End If
            Dim ActivityDate As DateTime = CDate(bv_strEventToDate)

            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Activity From Date Should be lesser than or equal to Activity To Date(", ActivityDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            'Chrome Fix - for null accepts
            pub_SetCallbackReturnValue("Error", "")
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_GetSelectedValues"
    Private Sub pvt_GetSelectedValues(ByVal bv_strWfData As String)
        Try
            dsBulkEmail = CType(RetrieveData(BULKEMAIL), BulkEmailDataSet)

            Dim drBulkEmail As DataRow()
            drBulkEmail = dsBulkEmail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Select(HeatingData.CHECKED & "='True'")
            If Not drBulkEmail.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please select at least one Equipment to send Bulk Email.")
                Exit Sub
            End If

            hdnCustomer.Value = CStr(dsBulkEmail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Rows(0).Item(BulkEmailData.CSTMR_ID))
            pub_SetCallbackReturnValue("CustomerID", CStr(dsBulkEmail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Rows(0).Item(BulkEmailData.CSTMR_ID)))
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetSearchDetails"
    Private Sub pvt_GetSearchDetails(ByVal bv_strCstmr_Id As String, _
                                      ByVal bv_strEquipmentNo As String, _
                                      ByVal bv_strInDateFrom As String, _
                                      ByVal bv_strInDateTo As String, _
                                      ByVal bv_strActivityId As String, _
                                      ByVal bv_strEventDateFrom As String, _
                                      ByVal bv_strEventDateTo As String, _
                                      ByVal bv_strEmail As String)
        Try
            Dim objBulkEmail As New BulkEmail
            dsBulkEmail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Clear()
            hdnCustomer.Value = (bv_strCstmr_Id)
            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            dsBulkEmail = objBulkEmail.GetTrackingDetails(bv_strCstmr_Id, _
                                                          bv_strEquipmentNo, _
                                                          bv_strInDateFrom, _
                                                          bv_strInDateTo, _
                                                          bv_strActivityId, _
                                                          bv_strEventDateFrom, _
                                                          bv_strEventDateTo, _
                                                          bv_strEmail, _
                                                          intDPT_ID)

            Dim dtTracking As New DataTable
            dtTracking = dsBulkEmail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING)
            Dim intRowCount As Integer = dsBulkEmail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Rows.Count

            CacheData(BULKEMAIL, dsBulkEmail)
            pub_SetCallbackReturnValue("RowCount", CStr(intRowCount))
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgBulkEmail_ClientBind"
    Protected Sub ifgBulkEmail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgBulkEmail.ClientBind
        Try
            If e.Parameters("btnType").ToString = "Search" Then
                dsBulkEmail = CType(RetrieveData(BULKEMAIL), BulkEmailDataSet)
                e.DataSource = dsBulkEmail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING)
                CacheData(BULKEMAIL, dsBulkEmail)
            Else
                dsBulkEmail.Tables(BulkEmailData._V_BULKEMAIL_TRACKING).Rows.Clear()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgBulkEmail_RowDataBound"
    Protected Sub ifgBulkEmail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgBulkEmail.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                If (drv.Item(BulkEmailData.ACTVTY_NAM).ToString.Trim = "Repair Estimate") Or (drv.Item(BulkEmailData.ACTVTY_NAM).ToString.Trim = "Repair Approval") Then
                    drv.Item(BulkEmailData.RPRT) = "Repair Workorder"
                Else
                    drv.Item(BulkEmailData.RPRT) = " "
                End If
                Dim str_020EIRNo As String
                Dim objCommonConfig As New ConfigSetting()
                str_020EIRNo = objCommonConfig.pub_GetConfigSingleValue("020", CInt(drv.Item(BulkEmailData.DPT_ID)))
                Dim Activityhyplnk As HyperLink
                Activityhyplnk = CType(e.Row.Cells(3).Controls(0), HyperLink)
                Activityhyplnk.Attributes.Add("onclick", String.Concat("openActivityReport('", drv.Item(BulkEmailData.GI_TRNSCTN_NO).ToString(), "','", drv.Item(BulkEmailData.ACTVTY_NAM).ToString(), "','", drv.Item(BulkEmailData.GTN_DT).ToString(), "','", drv.Item(BulkEmailData.EQPMNT_STTS_CD).ToString(), "','", drv.Item(BulkEmailData.RPRT).ToString(), "','", drv.Item(BulkEmailData.ACTVTY_NO).ToString(), "','", drv.Item(BulkEmailData.DPT_ID).ToString(), "','", str_020EIRNo, "');"))
                Activityhyplnk.NavigateUrl = "#"
                Dim Reporthyplnk As HyperLink
                Reporthyplnk = CType(e.Row.Cells(4).Controls(0), HyperLink)
                Reporthyplnk.Attributes.Add("onclick", String.Concat("openWorkOrderReport('", drv.Item(BulkEmailData.GI_TRNSCTN_NO).ToString(), "','", drv.Item(BulkEmailData.ACTVTY_NAM).ToString(), "','", drv.Item(BulkEmailData.GTN_DT).ToString(), "','", drv.Item(BulkEmailData.EQPMNT_STTS_CD).ToString(), "','", drv.Item(BulkEmailData.RPRT).ToString(), "','", drv.Item(BulkEmailData.ACTVTY_NO).ToString(), "');"))
                Reporthyplnk.NavigateUrl = "#"
                If CInt(drv.Item(BulkEmailData.TMS_SNT)) > 0 Then
                    Dim BulkMailhyplnk As HyperLink
                    BulkMailhyplnk = CType(e.Row.Cells(8).Controls(0), HyperLink)
                    BulkMailhyplnk.Attributes.Add("onclick", String.Concat("openBulkEmailDetail('", drv.Item(BulkEmailData.GI_TRNSCTN_NO).ToString(), "','", drv.Item(BulkEmailData.ACTVTY_NAM).ToString(), "','", drv.Item(BulkEmailData.EQPMNT_NO).ToString(), "','", drv.Item(BulkEmailData.ACTVTY_NO), "'    );return false;"))
                    BulkMailhyplnk.NavigateUrl = "#"
                End If
                Dim imgResendLink As Image
                imgResendLink = CType(e.Row.Cells(10).Controls(0), Image)
                imgResendLink.ToolTip = "Resend"
                imgResendLink.Visible = True
                If Not IsDBNull(drv.Item(BulkEmailData.TMS_SNT)) Then
                    If CInt(drv.Item(BulkEmailData.TMS_SNT)) > 0 Then
                        imgResendLink.ImageUrl = "../Images/BulkEMail.png"
                    Else
                        imgResendLink.ImageUrl = "../Images/BulkEMail_Disable.png"
                    End If
                End If

                imgResendLink.Attributes.Add("onclick", String.Concat("bulkmailResend('", e.Row.RowIndex.ToString, "','", drv.Item(BulkEmailData.CSTMR_ID), "','", drv.Item(BulkEmailData.ACTVTY_NAM), "','", drv.Item(BulkEmailData.GI_TRNSCTN_NO), "','", drv.Item(BulkEmailData.EQPMNT_NO), "','", drv.Item(BulkEmailData.LST_SNT_DT), "'    );return false;"))

                imgResendLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub
#End Region

#Region "pvt_SetChangesMade"

    Private Sub pvt_SetChangesMade()
        Try
            CommonWeb.pub_AttachHasChanges(lkpCustomer)
            CommonWeb.pub_AttachHasChanges(txtEquipmentNo)
            CommonWeb.pub_AttachHasChanges(datInDateFrom)
            CommonWeb.pub_AttachHasChanges(datInDateTo)
            CommonWeb.pub_AttachHasChanges(datEventDateFrom)
            CommonWeb.pub_AttachHasChanges(datEventDateTo)
            CommonWeb.pub_AttachHasChanges(chkCleaning)
            CommonWeb.pub_AttachHasChanges(chkRepairEstimate)
            CommonWeb.pub_AttachHasChanges(chkRepairApproval)
            CommonWeb.pub_AttachHasChanges(lkpEmail)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub

#End Region

    Private Function pvt_GenerateXSLT(ByVal bv_dsBulkEmailDetail As BulkEmailDataSet) As String
        Dim strinnerHTML As String = ""
        Dim objMemoryStream As New System.IO.MemoryStream
        Dim objhtmlMemoryStream As New System.IO.MemoryStream
        Try

            bv_dsBulkEmailDetail.WriteXml(objMemoryStream, XmlWriteMode.DiffGram)

            Dim doc As New XmlDocument()

            objMemoryStream.Position = 0
            doc.Load(objMemoryStream)

            Dim objXmlWriterSetting As New XmlWriterSettings
            objXmlWriterSetting.ConformanceLevel = ConformanceLevel.Fragment

            Dim objwriter As XmlWriter = XmlWriter.Create(objhtmlMemoryStream, objXmlWriterSetting)
            Dim transform As New XslCompiledTransform()

            transform.Load(Server.MapPath("../Templates/XSLTFile.xslt"))

            transform.Transform(doc.CreateNavigator(), Nothing, objwriter)
            objwriter.Close()

            objhtmlMemoryStream.Flush()
            objhtmlMemoryStream.Position = 0
            Dim strStream As New IO.StreamReader(objhtmlMemoryStream)

            strinnerHTML = strStream.ReadToEnd()

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        Finally
            objMemoryStream = Nothing
            objhtmlMemoryStream = Nothing
        End Try
        Return strinnerHTML
    End Function

End Class
