
Partial Class Operations_Cleaning
    Inherits Pagebase
    Dim str_007EIRNo As String
    Dim bln_007EIRNo_Key As Boolean
    Dim str_020EIRNo As String
    Dim bln_020EIRNo_Key As Boolean
    Dim dsCleaning As New CleaningDataSet
    Dim objCommonUI As New CommonUI
    Private Const CLEANING As String = "CLEANING"
    Private strMSGINSERT As String = " Created Successfully."
    Private strMSGUPDATE As String = " Updated Successfully."
    Dim objCleaning As New Cleaning
    Private Const CLEANING_INSTRUCTION As String = "CLEANING_INSTRUCTION"

#Region "Page_Load"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCommonConfig As New ConfigSetting()
            Dim objCommonUI As New CommonUI
            Dim objCommondata As New CommonData
            Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
            str_007EIRNo = objCommonConfig.pub_GetConfigSingleValue("007", intDepotID)
            bln_007EIRNo_Key = objCommonConfig.IsKeyExists
            str_020EIRNo = objCommonConfig.pub_GetConfigSingleValue("020", intDepotID)
            hdnStr20EirNo.Value = str_020EIRNo
            'lblReferenceNo.Text = str_007EIRNo
            'rbtnCustomer.Checked = True
            'rbtnParty.Attributes.Add("onclick", "selectInvoicingParty(this)")
            'rbtnCustomer.Attributes.Add("onclick", "selectCustomer(this)")
            pvt_SetChangesMade()
            datOriginalCleaningDate.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
            datLastCleaningDate.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
            'datOriginalInspectedDate.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyy").ToUpper
            'datLastInspectedDate.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyy").ToString
            Dim strSessionId As String = objCommondata.GetSessionID()
            Dim strActivityName As String = String.Empty
            If Not pub_GetQueryString("activityname") Is Nothing Then
                strActivityName = pub_GetQueryString("activityname")
            End If
            objCommondata.FlushLockDataByActivityName(CleaningData.EQPMNT_NO, strSessionId, strActivityName)
        End If
    End Sub
#End Region

#Region "oncallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "fnGetData"
                    pvt_GetData(e.GetCallbackValue("mode"))
                Case "CreateCleaning"
                    pvt_CreateCleaning(e.GetCallbackValue("CleaningID"), _
                                       e.GetCallbackValue("EquipmentNo"), _
                                       e.GetCallbackValue("ChemicalName"), _
                                       e.GetCallbackValue("CleaningRate"), _
                                       e.GetCallbackValue("OriginalCleaningDate"), _
                                       e.GetCallbackValue("LastCleaningDate"), _
                                       e.GetCallbackValue("EquipmentStatus"), _
                                       e.GetCallbackValue("EquipmentStatusCD"), _
                                       e.GetCallbackValue("CleaningReferenceNo"), _
                                       e.GetCallbackValue("Remarks"), _
                                       e.GetCallbackValue("CustomerId"), _
                                       e.GetCallbackValue("GateInDate"), _
                                       e.GetCallbackValue("GI_TRNSCTN_NO"), _
                                       CInt(e.GetCallbackValue("ActivityId")), _
                                       CBool(e.GetCallbackValue("blnAdditionalCleaningBit")), _
                                       CBool(e.GetCallbackValue("SlabRateFlag")))
                Case "UpdateCleaning"
                    pvt_UpdateCleaning(e.GetCallbackValue("CleaningID"), _
                                       e.GetCallbackValue("EquipmentNo"), _
                                       e.GetCallbackValue("ChemicalName"), _
                                       e.GetCallbackValue("CleaningRate"), _
                                       e.GetCallbackValue("OriginalCleaningDate"), _
                                       e.GetCallbackValue("LastCleaningDate"), _
                                       e.GetCallbackValue("EquipmentStatus"), _
                                       e.GetCallbackValue("EquipmentStatusCD"), _
                                       e.GetCallbackValue("CleaningReferenceNo"), _
                                       e.GetCallbackValue("Remarks"), _
                                       e.GetCallbackValue("CustomerId"), _
                                       e.GetCallbackValue("GateInDate"), _
                                       e.GetCallbackValue("GI_TRNSCTN_NO"), _
                                       CInt(e.GetCallbackValue("ActivityId")), _
                                       CBool(e.GetCallbackValue("blnAdditionalCleaningBit")))
                Case "ValidatePreviousActivityDate"
                    pvt_ValidatePreviousActivityDate(e.GetCallbackValue("EquipmentNo"), _
                                                     e.GetCallbackValue("EventDate"))
                Case "ValidateOriginalInspectedDate"
                    pvt_ValidateOriginalInspectedDate(e.GetCallbackValue("EquipmentNo"), _
                                                     e.GetCallbackValue("EventDate"), _
                                                     e.GetCallbackValue("CleaningDate"))

                Case "ValidateLastActivityDate"
                    pvt_ValidateLastActivityDate(e.GetCallbackValue("EquipmentNo"), _
                                                     e.GetCallbackValue("EventDate"), _
                                                     e.GetCallbackValue("OriginalDate"))
                Case "CleaningCertificate"
                    pvt_PrintCleaningCertificate(e.GetCallbackValue("ID"), _
                                                 e.GetCallbackValue("EquipmentNo"), _
                                                 e.GetCallbackValue("GI_TRNSCTN_NO"))
                Case "ValidateLatestCleaningDate"
                    pvt_ValidateLatestCleaningDate(e.GetCallbackValue("EquipmentNo"), _
                                                     e.GetCallbackValue("OriginalCleaningDate"), _
                                                     e.GetCallbackValue("LatestCleaningDate"))
                Case "UpdateCurrencyCode"
                    pvt_UpdateCurrencyCode(e.GetCallbackValue(("InvoicingPartyId")))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateCurrencyCode"
    Private Sub pvt_UpdateCurrencyCode(ByVal bv_InvoicingPartyId As String)
        Try
            Dim objCommon As New CommonData
            Dim Party_id As Integer = CInt(bv_InvoicingPartyId)
            Dim strCleaningCertificateNo As String = String.Empty
            Dim intDepotID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim dtCleaning As DataTable
            dtCleaning = objCleaning.GerCurrencyCode(Party_id, intDepotID).Tables(CleaningData._INVOICING_PARTY)
            pub_SetCallbackReturnValue("CurrencyCode", dtCleaning.Rows(0).Item(CleaningData.CRRNCY_CD))
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_PrintCleaningCertificate"
    Private Sub pvt_PrintCleaningCertificate(ByVal bv_strCleaningId As String, _
                                             ByVal bv_strEquipmentNo As String, _
                                             ByVal bv_strGiTransactionNo As String)
        Try
            Dim objCommon As New CommonData
            Dim strCleaningCertificateNo As String = String.Empty
            Dim intDepotID As Integer = CommonWeb.iInt(objCommon.GetDepotID())

            objCleaning.UpdateCleaningCertificateNo(CommonWeb.iLng(bv_strCleaningId), bv_strEquipmentNo, intDepotID, bv_strGiTransactionNo, strCleaningCertificateNo)

            CacheData(CLEANING, dsCleaning)
            pub_SetCallbackReturnValue("CertNo", CStr(strCleaningCertificateNo))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateCleaning"
    Private Sub pvt_CreateCleaning(ByVal bv_strCleaningId As String, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByVal bv_strChemicalName As String, _
                                   ByVal bv_strCleaningRate As String, _
                                   ByVal bv_strOriginalCleaningDate As String, _
                                   ByVal bv_strLastCleaningDate As String, _
                                   ByVal bv_strEquipmentStatus As String, _
                                   ByVal bv_strEquipmentStatusCD As String, _
                                   ByVal bv_strCleaningReferenceNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_CustomerId As String, _
                                   ByVal bv_GateInDate As String, _
                                   ByVal bv_strGI_TRNSCTN_NO As String, _
                                   ByVal bv_intActivityId As Integer, _
                                   ByVal bv_blnAdditionalCleaningFlag As Boolean, _
                                   ByVal bv_blnSlabRateFlag As Boolean)
        Try
            Dim objCommon As New CommonData
            Dim lngCreated As Long

            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim strActivitySubmit As String = String.Empty

            dsCleaning = CType(RetrieveData(CLEANING), CleaningDataSet)
            lngCreated = objCleaning.pub_CreateCleaning_Clean(CLng(bv_strCleaningId), _
                                                        bv_strEquipmentNo, _
                                                        bv_strChemicalName, _
                                                        bv_strCleaningRate, _
                                                        CommonWeb.iDat(bv_strOriginalCleaningDate), _
                                                        CommonWeb.iDat(bv_strLastCleaningDate), _
                                                        CommonWeb.iLng(bv_strEquipmentStatus), _
                                                        bv_strEquipmentStatusCD, _
                                                        bv_strCleaningReferenceNo, _
                                                        bv_strRemarks, _
                                                        bv_CustomerId, _
                                                        CommonWeb.iDat(bv_GateInDate), _
                                                        bv_strGI_TRNSCTN_NO, _
                                                        intDPT_ID, _
                                                        objCommon.GetCurrentUserName(), _
                                                        CDate(objCommon.GetCurrentDate()), _
                                                        dsCleaning, _
                                                        strActivitySubmit, _
                                                        CInt(bv_intActivityId), _
                                                        bv_blnAdditionalCleaningFlag, _
                                                        bv_blnSlabRateFlag)
            Dim strSplitActivitySubmit() As String = Nothing
            If strActivitySubmit <> Nothing Then
                If strActivitySubmit.Length > 0 Then
                    pub_SetCallbackReturnValue("ActivitySubmit", strActivitySubmit)
                Else
                    pub_SetCallbackReturnValue("ActivitySubmit", "")
                End If
            End If
            pub_SetCallbackReturnValue("ID", CStr(lngCreated))
            pub_SetCallbackReturnValue("Message", String.Concat("Cleaning : ", " ", "Equipment ", bv_strEquipmentNo, strMSGINSERT))
            pub_SetCallbackStatus(True)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                            String.Concat("EQPMNT_NO : ", bv_strEquipmentNo, _
                                           "  GI_TRNSCTN_NO : ", bv_strGI_TRNSCTN_NO, _
                                           "  ADDTNL_CLNNG_BT : ", bv_blnAdditionalCleaningFlag))
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateCleaning"
    Private Sub pvt_UpdateCleaning(ByVal bv_strCleaningID As String, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByVal bv_strChemicalName As String, _
                                   ByVal bv_strCleaningRate As String, _
                                   ByVal bv_strOriginalCleaningDate As String, _
                                   ByVal bv_strLastCleaningDate As String, _
                                   ByVal bv_strEquipmentStatus As String, _
                                   ByVal bv_strEquipmentStatusCD As String, _
                                   ByVal bv_strCleaningReferenceNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_CustomerId As String, _
                                   ByVal bv_GateInDate As String, _
                                   ByVal bv_strGI_TRNSCTN_NO As String, _
                                   ByVal bv_intActivityId As Integer, _
                                   ByVal bv_blnAdditionalCleaningFlag As Boolean)
        Try
            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim strActivitySubmit As String = String.Empty

            Dim blnBill As Boolean = False
            blnBill = objCleaning.pub_GetCleaningChargeBilled(bv_strEquipmentNo, intDPT_ID, CLng(bv_strCleaningID), bv_strGI_TRNSCTN_NO, bv_strCleaningRate)
            If blnBill = False Then
                pub_SetCallbackError("Cleaning Invoice has been raised, Hence cannot change the cleaning rate.")
                pub_SetCallbackStatus(False)
                Exit Sub
            End If
            ''
            dsCleaning = CType(RetrieveData(CLEANING), CleaningDataSet)
            objCleaning.ModifyCleaning_Clean(CommonWeb.iLng(bv_strCleaningID), _
                                       bv_strEquipmentNo, _
                                       bv_strChemicalName, _
                                       bv_strCleaningRate, _
                                       CommonWeb.iDat(bv_strOriginalCleaningDate), _
                                       CommonWeb.iDat(bv_strLastCleaningDate), _
                                       CommonWeb.iLng(bv_strEquipmentStatus), _
                                       bv_strEquipmentStatusCD, _
                                       bv_strCleaningReferenceNo, _
                                       bv_strRemarks, _
                                       bv_CustomerId, _
                                       CommonWeb.iDat(bv_GateInDate), _
                                       bv_strGI_TRNSCTN_NO, _
                                       intDPT_ID, _
                                       objCommon.GetCurrentUserName(), _
                                       CDate(objCommon.GetCurrentDate()), _
                                       dsCleaning, _
                                       strActivitySubmit, _
                                       bv_intActivityId, _
                                       bv_blnAdditionalCleaningFlag)
            CacheData(CLEANING, dsCleaning)
            Dim strSplitActivitySubmit() As String = Nothing
            If strActivitySubmit <> Nothing Then
                If strActivitySubmit.Length > 0 Then
                    pub_SetCallbackReturnValue("ActivitySubmit", strActivitySubmit)
                Else
                    pub_SetCallbackReturnValue("ActivitySubmit", "")
                End If
            End If
            pub_SetCallbackReturnValue("ID", CStr(bv_strCleaningID))
            pub_SetCallbackReturnValue("Message", String.Concat("Cleaning : ", " ", "Equipment ", bv_strEquipmentNo, strMSGUPDATE))
            pub_SetCallbackStatus(True)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                        String.Concat("EQPMNT_NO : ", bv_strEquipmentNo, _
                                       "  GI_TRNSCTN_NO : ", bv_strGI_TRNSCTN_NO, _
                                       "  ADDTNL_CLNNG_BT : ", bv_blnAdditionalCleaningFlag))
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetData"
    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim objCommon As New CommonData()
            Dim datGetDateTime As DateTime = CDate(objCommon.GetCurrentDate())
            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            Dim objCleaning As New Cleaning
            Dim sbCleaning As New StringBuilder
            Dim dtCleaning As DataTable
            Dim dtEqpStatus As DataTable
            sbCleaning.Append("showSubmitButton(true);")
            If bv_strMode = MODE_NEW Then
                Dim dsEqpStatus As New DataSet
                Dim blnCustomerProductRate As Boolean
                Dim blnProductRate As Boolean
                Dim blnSlabRate As Boolean
                Dim bln_073KeyExists As Boolean
                Dim str_073Key As String
                dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Cleaning", True, intDepotID)
                dtEqpStatus = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY)
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.GI_TRNSCTN_NO) <> Nothing Then
                    dsCleaning = objCleaning.pub_GetActivityStatusDetails(PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_NO), PageSubmitPane.pub_GetPageAttribute(CleaningData.DPT_ID), 0, PageSubmitPane.pub_GetPageAttribute(CleaningData.GI_TRNSCTN_NO))
                Else
                    Exit Sub
                End If
                dtCleaning = dsCleaning.Tables(CleaningData._V_ACTIVITY_STATUS)
                sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtEquipmentNo, PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_NO)))
                'rbtnCustomer.Checked = True
                sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpEqpType, dtCleaning.Rows(0).Item(CleaningData.EQPMNT_TYP_ID), dtCleaning.Rows(0).Item(CleaningData.EQPMNT_TYP_CD)))
                sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCustomer, PageSubmitPane.pub_GetPageAttribute(CleaningData.CSTMR_CD)))
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.PRDCT_DSCRPTN_VC) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CleaningData.PRDCT_DSCRPTN_VC) <> "" Then
                    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpPreviousCargo, PageSubmitPane.pub_GetPageAttribute(CleaningData.PRDCT_ID), PageSubmitPane.pub_GetPageAttribute(CleaningData.PRDCT_DSCRPTN_VC)))
                Else
                    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpPreviousCargo, "", ""))
                End If
                If (dtCleaning.Rows(0).Item(CleaningData.CHMCL_NM)) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtChemicalName, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtChemicalName, (dtCleaning.Rows(0).Item(CleaningData.CHMCL_NM))))
                End If
                str_073Key = objCommon.GetConfigSetting("073", bln_073KeyExists)
                If bln_073KeyExists AndAlso str_073Key.ToLower = "true" Then
                    If PageSubmitPane.pub_GetPageAttribute(CleaningData.CSTMR_ID) Is Nothing Then
                        sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCustomerId, ""))
                    Else
                        blnSlabRate = objCleaning.pub_CheckSlabRateExists(PageSubmitPane.pub_GetPageAttribute(CleaningData.CSTMR_ID), PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_TYP_ID))
                        sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCustomerId, PageSubmitPane.pub_GetPageAttribute(CleaningData.CSTMR_ID)))
                    End If
                    If (dtCleaning.Rows(0).Item(CleaningData.CLNNG_RT)) Is Nothing Then
                        sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, ""))
                    Else
                        blnCustomerProductRate = dtCleaning.Rows(0).Item("CLNNG_CSTMR_PRDCT_RT_BT")
                        blnProductRate = dtCleaning.Rows(0).Item("CLNNG_PRDCT_RT_BT")
                        If blnProductRate = True AndAlso dtCleaning.Rows(0).Item(CleaningData.CLNNG_RT) = 0 AndAlso blnCustomerProductRate = False AndAlso blnSlabRate = False Then      'No rates are available
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, ""))
                            sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnSlabRateFlg, "False"))

                        ElseIf blnProductRate = True AndAlso blnCustomerProductRate = False AndAlso blnSlabRate = False Then  'Customer specific product rate available
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, (dtCleaning.Rows(0).Item(CleaningData.CLNNG_RT))))
                            sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnSlabRateFlg, "False"))

                        ElseIf blnProductRate = True AndAlso blnCustomerProductRate = True AndAlso blnSlabRate = False Then  'Customer specific product rate and Product Rate available
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, (dtCleaning.Rows(0).Item(CleaningData.CLNNG_RT))))
                            sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnSlabRateFlg, "False"))

                        ElseIf blnProductRate = True AndAlso blnCustomerProductRate = True AndAlso blnSlabRate = True Then  'Customer specific product rate, product rate and slab rate available 
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, (dtCleaning.Rows(0).Item(CleaningData.CLNNG_RT))))
                            sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnSlabRateFlg, "False"))

                        ElseIf blnProductRate = True AndAlso blnCustomerProductRate = False AndAlso blnSlabRate = True Then  'Customer specific product rate And Slab rate available
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, ""))
                            sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnSlabRateFlg, "True"))

                        ElseIf blnProductRate = False AndAlso blnCustomerProductRate = False AndAlso blnSlabRate = True Then  'Only Slab Rate available
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, ""))
                            sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnSlabRateFlg, "True"))
                        End If
                    End If
                Else
                    If PageSubmitPane.pub_GetPageAttribute(CleaningData.CSTMR_ID) Is Nothing Then
                        sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCustomerId, ""))
                    Else
                        sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCustomerId, PageSubmitPane.pub_GetPageAttribute(CleaningData.CSTMR_ID)))
                    End If
                    If (dtCleaning.Rows(0).Item(CleaningData.CLNNG_RT)) Is Nothing Then
                        sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, ""))
                    Else
                        sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, (dtCleaning.Rows(0).Item(CleaningData.CLNNG_RT))))
                    End If
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnSlabRateFlg, "False"))
                End If

                'If (dtCleaning.Rows(0).Item(CleaningData.GI_RF_NO)) Is Nothing Then
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCustReferenceNo, ""))
                'Else
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCustReferenceNo, (dtCleaning.Rows(0).Item(CleaningData.GI_RF_NO))))
                'End If
                sbCleaning.Append(CommonWeb.GetTextValuesJSO(datGateInDate, CDate(PageSubmitPane.pub_GetPageAttribute(CleaningData.GTN_DT)).ToString("dd-MMM-yyyy").ToUpper))
                If (dtEqpStatus.Rows(0).Item(CommonUIData.DEFAULT_STATUS)) <> "NULLABLE" And (dtEqpStatus.Rows(0).Item(CommonUIData.DEFAULT_STATUS)) <> "" Then
                    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, (dtEqpStatus.Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)), (dtEqpStatus.Rows(0).Item(CommonUIData.DEFAULT_STATUS))))
                Else
                    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, "", ""))
                End If
                
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.GI_TRNSCTN_NO) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnGI_TRNSCTN_NO, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnGI_TRNSCTN_NO, PageSubmitPane.pub_GetPageAttribute(CleaningData.GI_TRNSCTN_NO)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.DPT_ID) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotID, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotID, PageSubmitPane.pub_GetPageAttribute(CleaningData.DPT_ID)))
                End If

                If dtCleaning.Rows(0).Item(CommonUIData.CRRNCY_CD) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCurrency, ""))
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCurrencyCode, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCurrency, dtCleaning.Rows(0).Item(CommonUIData.CRRNCY_CD)))
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCurrencyCode, dtCleaning.Rows(0).Item(CommonUIData.CRRNCY_CD)))
                End If


                Dim objCommonConfig As New ConfigSetting()
                Dim str_014KeyValue As String
                Dim str_015KeyValue As String
                Dim str_016KeyValue As String
                Dim str_017KeyValue As String
                Dim str_018KeyValue As String
                Dim str_019KeyValue As String
                sbCleaning.Append(CommonWeb.GetTextDateValuesJSO(datOriginalCleaningDate, datGetDateTime.ToString("dd-MMM-yyyy").ToUpper))
                sbCleaning.Append(CommonWeb.GetTextDateValuesJSO(datLastCleaningDate, datGetDateTime.ToString("dd-MMM-yyyy").ToUpper))
                'sbCleaning.Append(CommonWeb.GetTextDateValuesJSO(datOriginalInspectedDate, datGetDateTime.ToString("dd-MMM-yyyy").ToUpper))
                'sbCleaning.Append(CommonWeb.GetTextDateValuesJSO(datLastInspectedDate, datGetDateTime.ToString("dd-MMM-yyyy").ToUpper))
                str_014KeyValue = objCommonConfig.pub_GetConfigSingleValue("014", intDepotID)
                'sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleanedFor, (str_014KeyValue)))
                str_015KeyValue = objCommonConfig.pub_GetConfigSingleValue("015", intDepotID)
                'sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtLocOfCleaning, (str_015KeyValue)))
                str_016KeyValue = objCommonConfig.pub_GetConfigSingleValue("016", intDepotID)
                'sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpEqpmntCondition, objCommon.GetEnumID("EQUIPMENTCONDTN", str_016KeyValue), UCase(str_016KeyValue)))
                str_017KeyValue = objCommonConfig.pub_GetConfigSingleValue("017", intDepotID)
                'sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpValveFittingCondition, objCommon.GetEnumID("EQUIPMENTCONDTN", str_017KeyValue), UCase(str_017KeyValue)))
                str_018KeyValue = objCommonConfig.pub_GetConfigSingleValue("018", intDepotID)
                'sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpCleaningStatus, objCommon.GetEnumID("EQUIPMENTSTATUS", str_018KeyValue), UCase(str_018KeyValue)))
                str_019KeyValue = objCommonConfig.pub_GetConfigSingleValue("019", intDepotID)
                'sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpEqpmntCleaningStatus2, objCommon.GetEnumID("EQUIPMENTSTATUS2", str_019KeyValue), UCase(str_019KeyValue)))
                '  sbCleaning.Append("setHasChange();")
                If Not (PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_BT) Is Nothing AndAlso PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_FLG) Is Nothing) Then
                    If CBool(PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_BT)) = False AndAlso CBool(PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_FLG)) = False Then
                        sbCleaning.Append(CommonWeb.GetCheckboxValuesJSO(chkAdditionalCleaningBit, False))
                        'sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCrtfct_No, ""))
                        sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtRemarks, ""))
                        'sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtOutletSealNo, ""))
                        'sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtManLidSealNo, ""))
                        'sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtTopSealNo, ""))
                        sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningReferenceNo, ""))
                    Else
                        'If PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_CERT_NO) Is Nothing Then
                        '    sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCrtfct_No, ""))
                        'Else
                        '    sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCrtfct_No, PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_CERT_NO)))

                        'End If
                        If PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_RMRKS_VC) Is Nothing Then
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtRemarks, ""))
                        Else
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtRemarks, PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_RMRKS_VC)))
                        End If
                        If Not PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_ID) Is Nothing Then
                            sbCleaning.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_ID), "');"))
                        End If
                        sbCleaning.Append(CommonWeb.GetCheckboxValuesJSO(chkAdditionalCleaningBit, PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_BT)))

                        If Not PageSubmitPane.pub_GetPageAttribute(CleaningData.ORGNL_CLNNG_DT) Is Nothing Then
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(datOriginalCleaningDate, CDate(PageSubmitPane.pub_GetPageAttribute(CleaningData.ORGNL_CLNNG_DT)).ToString("dd-MMM-yyyy").ToUpper))
                        End If

                        'sbCleaning.Append(CommonWeb.GetTextValuesJSO(datOriginalInspectedDate, CDate(PageSubmitPane.pub_GetPageAttribute(CleaningData.ORGNL_INSPCTD_DT)).ToString("dd-MMM-yyyy").ToUpper))
                        'If (PageSubmitPane.pub_GetPageAttribute(CleaningData.BTTM_SL_NO)) Is Nothing Then
                        '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtOutletSealNo, ""))
                        'Else
                        '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtOutletSealNo, PageSubmitPane.pub_GetPageAttribute(CleaningData.BTTM_SL_NO)))
                        'End If
                        'If (PageSubmitPane.pub_GetPageAttribute(CleaningData.MN_LID_SL_NO)) Is Nothing Then
                        '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtManLidSealNo, ""))
                        'Else
                        '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtManLidSealNo, PageSubmitPane.pub_GetPageAttribute(CleaningData.MN_LID_SL_NO)))
                        'End If
                        'If (PageSubmitPane.pub_GetPageAttribute(CleaningData.TP_SL_NO)) Is Nothing Then
                        '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtTopSealNo, ""))
                        'Else
                        '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtTopSealNo, PageSubmitPane.pub_GetPageAttribute(CleaningData.TP_SL_NO)))
                        'End If
                        If (PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_RFRNC_NO)) Is Nothing Then
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningReferenceNo, ""))
                        Else
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningReferenceNo, PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_RFRNC_NO)))
                        End If
                        If (PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_RT)) Is Nothing Then
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, ""))
                        Else
                            sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, CDec(PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_RT))))
                        End If

                        If (PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_FLG)) Is Nothing Then
                            sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAddtnlClnngFlg, ""))
                        Else
                            sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAddtnlClnngFlg, CBool(PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_FLG))))
                        End If


                    End If
                Else
                    'sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtTopSealNo, ""))
                    'sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtManLidSealNo, ""))
                    'sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtOutletSealNo, ""))
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningReferenceNo, ""))
                    sbCleaning.Append(CommonWeb.GetCheckboxValuesJSO(chkAdditionalCleaningBit, False))
                    'sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCrtfct_No, ""))
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtRemarks, ""))
                End If
            ElseIf bv_strMode = MODE_EDIT Then
                Dim dtGateIn As New DataTable
                Dim dtParty As DataTable
                dsCleaning = objCleaning.pub_GetActivityStatusDetails(PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_NO), PageSubmitPane.pub_GetPageAttribute(CleaningData.DPT_ID), _
                                                                      PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_ID), PageSubmitPane.pub_GetPageAttribute(CleaningData.GI_TRNSCTN_NO))
                dtCleaning = dsCleaning.Tables(CleaningData._V_ACTIVITY_STATUS)
                sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtEquipmentNo, PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_NO)))
                If (PageSubmitPane.pub_GetPageAttribute(CleaningData.CRRNT_EQPMNT_STTS_ID)) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnStatusId, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnStatusId, PageSubmitPane.pub_GetPageAttribute(CleaningData.CRRNT_EQPMNT_STTS_ID)))
                End If
                'If PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_CERT_NO) Is Nothing Then
                '    sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCrtfct_No, ""))
                'Else
                '    sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCrtfct_No, PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_CERT_NO)))
                'End If
                If dtCleaning.Rows(0).Item(CleaningData.EQPMNT_TYP_CD) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpEqpType, "", ""))
                Else
                    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpEqpType, dtCleaning.Rows(0).Item(CleaningData.EQPMNT_TYP_ID), dtCleaning.Rows(0).Item(CleaningData.EQPMNT_TYP_CD)))
                End If
                sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCustomer, PageSubmitPane.pub_GetPageAttribute(CleaningData.CSTMR_CD)))
                sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpPreviousCargo, PageSubmitPane.pub_GetPageAttribute(CleaningData.PRDCT_ID), PageSubmitPane.pub_GetPageAttribute(CleaningData.PRDCT_DSCRPTN_VC)))
                If (PageSubmitPane.pub_GetPageAttribute(CleaningData.CHMCL_NM)) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtChemicalName, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtChemicalName, PageSubmitPane.pub_GetPageAttribute(CleaningData.CHMCL_NM)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_RT)) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningRate, PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_RT)))
                End If
                sbCleaning.Append(CommonWeb.GetTextValuesJSO(datGateInDate, CDate(dtCleaning.Rows(0).Item(CleaningData.GTN_DT)).ToString("dd-MMM-yyyy").ToUpper))
                sbCleaning.Append(CommonWeb.GetTextValuesJSO(datOriginalCleaningDate, CDate(PageSubmitPane.pub_GetPageAttribute(CleaningData.ORGNL_CLNNG_DT)).ToString("dd-MMM-yyyy").ToUpper))
                sbCleaning.Append(CommonWeb.GetTextValuesJSO(datLastCleaningDate, CDate(PageSubmitPane.pub_GetPageAttribute(CleaningData.LST_CLNNG_DT)).ToString("dd-MMM-yyyy").ToUpper))
                'sbCleaning.Append(CommonWeb.GetTextValuesJSO(datOriginalInspectedDate, CDate(PageSubmitPane.pub_GetPageAttribute(CleaningData.ORGNL_INSPCTD_DT)).ToString("dd-MMM-yyyy").ToUpper))
                'sbCleaning.Append(CommonWeb.GetTextValuesJSO(datLastInspectedDate, CDate(PageSubmitPane.pub_GetPageAttribute(CleaningData.LST_INSPCTD_DT)).ToString("dd-MMM-yyyy").ToUpper))
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_STTS_CD) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_STTS_CD) <> "" Then
                    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_STTS_ID), PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_STTS_CD)))
                Else
                    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpStatus, "", ""))
                End If

                'If (PageSubmitPane.pub_GetPageAttribute(CleaningData.CLND_FR_VCR)) Is Nothing Then
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleanedFor, ""))
                'Else
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleanedFor, PageSubmitPane.pub_GetPageAttribute(CleaningData.CLND_FR_VCR)))
                'End If
                'If (PageSubmitPane.pub_GetPageAttribute(CleaningData.LCTN_OF_CLNG)) Is Nothing Then
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtLocOfCleaning, ""))
                'Else
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtLocOfCleaning, PageSubmitPane.pub_GetPageAttribute(CleaningData.LCTN_OF_CLNG)))
                'End If
                'If PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CLNNG_STTS_1_CD) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CLNNG_STTS_1_CD) <> "" Then
                '    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpCleaningStatus, PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CLNNG_STTS_1), PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CLNNG_STTS_1_CD)))
                'Else
                '    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpCleaningStatus, "", ""))
                'End If
                'If PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CLNNG_STTS_2_CD) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CLNNG_STTS_2_CD) <> "" Then
                '    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpEqpmntCleaningStatus2, PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CLNNG_STTS_2), PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CLNNG_STTS_2_CD)))
                'Else
                '    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpEqpmntCleaningStatus2, "", ""))
                'End If
                'If PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CNDTN_CD) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CNDTN_CD) <> "" Then
                '    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpEqpmntCondition, PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CNDTN_ID), PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_CNDTN_CD)))
                'Else
                '    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpEqpmntCondition, "", ""))
                'End If
                'If PageSubmitPane.pub_GetPageAttribute(CleaningData.VLV_FTTNG_CNDTN_CD) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CleaningData.VLV_FTTNG_CNDTN_CD) <> "" Then
                '    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpValveFittingCondition, PageSubmitPane.pub_GetPageAttribute(CleaningData.VLV_FTTNG_CNDTN), PageSubmitPane.pub_GetPageAttribute(CleaningData.VLV_FTTNG_CNDTN_CD)))
                'Else
                '    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpValveFittingCondition, "", ""))
                'End If
                'If PageSubmitPane.pub_GetPageAttribute(CleaningData.INVCNG_PRTY_CD) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CleaningData.INVCNG_PRTY_CD) <> "" Then
                '    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpInvcngTo, PageSubmitPane.pub_GetPageAttribute(CleaningData.INVCNG_PRTY_ID), PageSubmitPane.pub_GetPageAttribute(CleaningData.INVCNG_PRTY_CD)))
                'Else
                '    sbCleaning.Append(CommonWeb.GetLookupValuesJSO(lkpInvcngTo, "", ""))
                'End If
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.INVCNG_PRTY_CD) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CleaningData.INVCNG_PRTY_CD) <> "" Then
                    dtParty = objCleaning.GerCurrencyCode(PageSubmitPane.pub_GetPageAttribute(CleaningData.INVCNG_PRTY_ID), intDepotID).Tables(CleaningData._INVOICING_PARTY)
                    If dtParty.Rows(0).Item(CommonUIData.CRRNCY_CD) Is Nothing Then
                        sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCurrency, ""))
                    Else
                        sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCurrency, dtParty.Rows(0).Item(CommonUIData.CRRNCY_CD)))
                    End If
                Else
                    If (dtCleaning.Rows(0).Item(CleaningData.CRRNCY_CD)) Is Nothing Then
                        sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCurrency, ""))
                    Else
                        sbCleaning.Append(CommonWeb.GetLabelValuesJSO(lblCurrency, (dtCleaning.Rows(0).Item(CleaningData.CRRNCY_CD))))
                    End If
                End If
                If dtCleaning.Rows(0).Item(CleaningData.CRRNCY_CD) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCurrencyCode, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCurrencyCode, dtCleaning.Rows(0).Item(CleaningData.CRRNCY_CD)))
                End If
                'If (PageSubmitPane.pub_GetPageAttribute(CleaningData.BTTM_SL_NO)) Is Nothing Then
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtOutletSealNo, ""))
                'Else
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtOutletSealNo, PageSubmitPane.pub_GetPageAttribute(CleaningData.BTTM_SL_NO)))
                'End If
                'If (PageSubmitPane.pub_GetPageAttribute(CleaningData.MN_LID_SL_NO)) Is Nothing Then
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtManLidSealNo, ""))
                'Else
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtManLidSealNo, PageSubmitPane.pub_GetPageAttribute(CleaningData.MN_LID_SL_NO)))
                'End If
                'If (PageSubmitPane.pub_GetPageAttribute(CleaningData.TP_SL_NO)) Is Nothing Then
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtTopSealNo, ""))
                'Else
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtTopSealNo, PageSubmitPane.pub_GetPageAttribute(CleaningData.TP_SL_NO)))
                'End If
                'If (PageSubmitPane.pub_GetPageAttribute(CleaningData.CSTMR_RFRNC_NO)) Is Nothing Then
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCustReferenceNo, ""))
                'Else
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCustReferenceNo, PageSubmitPane.pub_GetPageAttribute(CleaningData.CSTMR_RFRNC_NO)))
                'End If
                'If (dtCleaning.Rows(0).Item(CleaningData.GI_RF_NO)) Is Nothing Then
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCustReferenceNo, ""))
                'Else
                '    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCustReferenceNo, (dtCleaning.Rows(0).Item(CleaningData.GI_RF_NO))))
                'End If
                If (PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_RFRNC_NO)) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningReferenceNo, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtCleaningReferenceNo, PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_RFRNC_NO)))
                End If
                If (PageSubmitPane.pub_GetPageAttribute(CleaningData.RMRKS_VC)) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtRemarks, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetTextValuesJSO(txtRemarks, PageSubmitPane.pub_GetPageAttribute(CleaningData.RMRKS_VC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.CSTMR_ID) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCustomerId, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCustomerId, PageSubmitPane.pub_GetPageAttribute(CleaningData.CSTMR_ID)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.GI_TRNSCTN_NO) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnGI_TRNSCTN_NO, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnGI_TRNSCTN_NO, PageSubmitPane.pub_GetPageAttribute(CleaningData.GI_TRNSCTN_NO)))
                End If
                If dtCleaning.Rows(0).Item(CleaningData.GTN_DT) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnGateInDate, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnGateInDate, dtCleaning.Rows(0).Item(CleaningData.GTN_DT)))
                End If
                If dtCleaning.Rows(0).Item(CleaningData.BLLNG_FLG) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnBillingFlag, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnBillingFlag, dtCleaning.Rows(0).Item(CleaningData.BLLNG_FLG)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_ID) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCleaningID, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCleaningID, PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_ID)))
                End If
                sbCleaning.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(CleaningData.CLNNG_ID), "');"))
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.DPT_ID) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotID, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotID, PageSubmitPane.pub_GetPageAttribute(CleaningData.DPT_ID)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.IS_CHNG_OF_STTS) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEditDate, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnEditDate, PageSubmitPane.pub_GetPageAttribute(CleaningData.IS_CHNG_OF_STTS)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_BT) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetCheckboxValuesJSO(chkAdditionalCleaningBit, False))
                Else
                    sbCleaning.Append(CommonWeb.GetCheckboxValuesJSO(chkAdditionalCleaningBit, PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_BT)))
                End If

                If (PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_FLG)) Is Nothing Then
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAddtnlClnngFlg, ""))
                Else
                    sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnAddtnlClnngFlg, CBool(PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_FLG))))
                End If

                CacheData(CLEANING, dsCleaning)
            End If
            Dim strUserName As String = String.Empty
            Dim strIpError As String = String.Empty
            Dim strActivityName As String = String.Empty
            strUserName = pvt_GetLockData(CStr(PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.EQPMNT_NO)), strIpError, strActivityName)
            sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnLockUserName, strUserName))
            sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnIpError, strIpError))
            sbCleaning.Append(CommonWeb.GetHiddenTextValuesJSO(hdnLockActivityName, strActivityName))

            'Print Cleaning Instruction
            If Not PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_NO).ToString() Is Nothing Then
                Dim dt As DataTable = objCleaning.pub_GetCleaningInsructionReportDetails(PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_NO), objCommon.GetDepotID())
                Dim dsCleaningInstruction As New CleaningInspectionDataSet

                dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Clear()

                For Each dr As DataRow In dt.Rows

                    Dim drClean As DataRow = dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).NewRow()

                    For Each dc As DataColumn In dt.Columns

                        If dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Columns.Contains(dc.ColumnName) Then
                            drClean(dc.ColumnName) = dr(dc.ColumnName)
                        End If


                    Next

                    dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Rows.Add(drClean)
                Next

                'dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Merge(dt)
                CacheData(CLEANING_INSTRUCTION, dsCleaningInstruction)

            End If

            CacheData(CLEANING, dsCleaning)
            pub_SetCallbackReturnValue("Message", sbCleaning.ToString())
            pub_SetCallbackStatus(True)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, _
                                            String.Concat("EQPMNT_NO : ", PageSubmitPane.pub_GetPageAttribute(CleaningData.EQPMNT_NO), _
                                            "  GI_TRNSCTN_NO : ", PageSubmitPane.pub_GetPageAttribute(CleaningData.GI_TRNSCTN_NO), _
                                            "  ADDTNL_CLNNG_BT : ", PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_BT), _
                                            "   ADDTNL_CLNNG_FLG : ", PageSubmitPane.pub_GetPageAttribute(CleaningData.ADDTNL_CLNNG_FLG)))
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "PreRender"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/Cleaning.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "SetChangesMade"
    ''' <summary>
    ''' This method is used to set haschanges to all fields in page
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_SetChangesMade()
        'CommonWeb.pub_AttachHasChanges(datOriginalInspectedDate)
        CommonWeb.pub_AttachHasChanges(lkpPreviousCargo)
        CommonWeb.pub_AttachHasChanges(datOriginalCleaningDate)
        CommonWeb.pub_AttachHasChanges(datLastCleaningDate)
        'CommonWeb.pub_AttachHasChanges(datLastInspectedDate)
        CommonWeb.pub_AttachHasChanges(lkpStatus)
        'CommonWeb.pub_AttachHasChanges(txtCleanedFor)
        'CommonWeb.pub_AttachHasChanges(txtLocOfCleaning)
        'CommonWeb.pub_AttachHasChanges(lkpCleaningStatus)
        'CommonWeb.pub_AttachHasChanges(lkpEqpmntCleaningStatus2)
        'CommonWeb.pub_AttachHasChanges(lkpEqpmntCondition)
        'CommonWeb.pub_AttachHasChanges(lkpValveFittingCondition)
        'CommonWeb.pub_AttachHasChanges(txtOutletSealNo)
        'CommonWeb.pub_AttachHasChanges(txtManLidSealNo)
        'CommonWeb.pub_AttachHasChanges(txtTopSealNo)
        'CommonWeb.pub_AttachHasChanges(txtCustReferenceNo)
        CommonWeb.pub_AttachHasChanges(txtCleaningReferenceNo)
        CommonWeb.pub_AttachHasChanges(chkAdditionalCleaningBit)
        CommonWeb.pub_AttachDescMaxlength(txtRemarks)
    End Sub

#End Region

#Region "pvt_ValidatePreviousActivityDate"
    Private Sub pvt_ValidatePreviousActivityDate(ByVal bv_strEquipmentNo As String, ByVal bv_strEventDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objGateIn As New Gatein
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData
            Dim dsCommonUI As New CommonUIDataSet
            blnDateValid = objCommonUI.pub_GetEquipmentPreviousActivityDate(bv_strEquipmentNo, _
                                                                          CDate(bv_strEventDate), _
                                                                          dtPreviousDate, _
                                                                          "Cleaning", _
                                                                          CInt(objCommon.GetDepotID()))
            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Original Cleaning Date Should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateOriginalInspectedDate"
    Private Sub pvt_ValidateOriginalInspectedDate(ByVal bv_strEquipmentNo As String, ByVal bv_strEventDate As String, ByVal bv_strCleaningDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim Originaldate As DateTime = (bv_strCleaningDate)
            'If Not CDate(bv_strEventDate) = Nothing Then
            '    datLastInspectedDate.Text = CDate(bv_strEventDate).ToString("dd-MMM-yyyy").ToUpper
            'End If

            'If bv_strCleaningDate Is Nothing Then
            '    pub_SetCallbackReturnValue("Error", String.Concat("Please select Cleaning Date "))
            '    pub_SetCallbackStatus(True)
            '    Exit Sub
            'End If
            If (Not (CDate(bv_strEventDate) >= CDate(bv_strCleaningDate))) Then
                blnDateValid = True
            End If

            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Original Inspection Date should be greater than or equal to Original Cleaning Date (", Originaldate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateLastActivityDate"
    Private Sub pvt_ValidateLastActivityDate(ByVal bv_strEquipmentNo As String, ByVal bv_strEventDate As String, ByVal bv_strOriginalDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objCommon As New CommonData
            If (Not (CDate(bv_strEventDate) >= CDate(bv_strOriginalDate))) Then
                blnDateValid = True
            End If
            Dim Originaldate As DateTime = (bv_strOriginalDate)
            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Latest Cleaning Date should be greater than or equal to Original Cleaning Date (", Originaldate.ToString("dd-MMM-yyyy"), ")"))
            End If

            blnDateValid = objCommonUI.pub_GetEquipmentPreviousActivityDate(bv_strEquipmentNo, _
                                                                        CDate(bv_strEventDate), _
                                                                        dtPreviousDate, _
                                                                        "Cleaning", _
                                                                        CInt(objCommon.GetDepotID()))
            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Latest Cleaning Date Should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), ")"))
            End If

            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateLatestCleaningDate"
    Private Sub pvt_ValidateLatestCleaningDate(ByVal bv_strEquipmentNo As String, ByVal bv_strOriginalCleaningDate As String, ByVal bv_strLatestCleaningDate As String)
        Try
            Dim blnDateValid As Boolean = False
            'If (bv_strOriginalDate Is Nothing) Then
            '    pub_SetCallbackReturnValue("Error", String.Concat("Please select Original Date "))
            '    pub_SetCallbackStatus(True)
            '    Exit Sub
            'End If
            Dim Originaldate As DateTime = bv_strOriginalCleaningDate
            If ((CDate(bv_strOriginalCleaningDate) > CDate(bv_strLatestCleaningDate))) Then
                blnDateValid = True
            End If
            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Latest Cleaning Date should be greater than or equal to Original Cleaning Date (", Originaldate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_GetLockData"
    Private Function pvt_GetLockData(ByVal bv_strEquipmentNo As String, ByRef br_strIpError As String, _
                                      ByRef br_strActivityName As String) As String
        Try
            Dim objCommonData As New CommonData
            Dim strErrorMessage As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim strIpAddress As String = GetClientIPAddress()
            Dim strCurrentIpAddress As String = String.Empty
            Dim strUserName As String = String.Empty
            If Not pub_GetQueryString("activityname") Is Nothing Then
                br_strActivityName = pub_GetQueryString("activityname")
            End If
            blnLockData = objCommonData.pub_GetLockData(False, bv_strEquipmentNo, strUserName, br_strActivityName, strIpAddress, True, CleaningData.EQPMNT_NO)

            If blnLockData Then
                strCurrentIpAddress = GetClientIPAddress()
                If strCurrentIpAddress = strIpAddress Then
                    br_strIpError = "true"
                Else
                    br_strIpError = "false"
                End If
            End If
            Return strUserName
        Catch ex As Exception
            Return String.Empty
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

End Class
