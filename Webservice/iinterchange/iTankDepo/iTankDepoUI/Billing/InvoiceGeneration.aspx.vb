
Partial Class Billing_InvoiceGeneration
    Inherits Pagebase

#Region "Declarations.."
    Dim dsInvoiceGeneration As New InvoiceGenerationDataSet
    Dim dtInvoiceGeneration As DataTable
    Private Const INVOICE_GENERATION As String = "INVOICE_GENERATION"
    Dim strDepotCurrency As String = String.Empty
    Dim strCustomerCurrency As String = String.Empty
    Dim i32DepotCurrencyID As Integer
    Dim i32CustomerCurrencyID As Integer
#End Region

#Region "Page_Load1"

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim objCommondata As New CommonData
            If Not Page.IsPostBack Then
                datPeriodFrom.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy")
                datPeriodTo.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy")
                If objCommondata.GetMultiLocationSupportConfig.ToLower = "false" Then
                    lkpDepotCode.Validator.IsRequired = False
                Else
                    If objCommondata.GetOrganizationTypeCD = "BO" Then
                        hdnIsHeadQuarters.Value = "false"
                        lkpDepotCode.Text = objCommondata.GetDepotCD()
                        lkpDepotCode.ReadOnly = True
                        lkpDepotCode.Enabled = False
                    End If
                End If

            End If
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
                Case "DraftInvoice"
                    pvt_fnDraftInvoice(e.GetCallbackValue("InvoiceTypeID"), _
                                       e.GetCallbackValue("CustomerCurrency"), _
                                       e.GetCallbackValue("DepotCurrency"), _
                                       e.GetCallbackValue("ExRate"), _
                                       e.GetCallbackValue("FromDate"), _
                                       e.GetCallbackValue("ToDate"), _
                                       e.GetCallbackValue("ServicePartnerID"), _
                                       e.GetCallbackValue("CustomerType"), _
                                       e.GetCallbackValue("InvoiceNo"), _
                                       e.GetCallbackValue("DepotID"))
                Case "CheckDraftInvoice"
                    pvt_CheckDraftExists(e.GetCallbackValue("InvoiceTypeID"), _
                                         e.GetCallbackValue("FromDate"), _
                                         e.GetCallbackValue("ToDate"), _
                                         e.GetCallbackValue("ServicePartnerID"), _
                                         e.GetCallbackValue("CustomerType"), _
                                         e.GetCallbackValue("DepotID"))
                Case "GetFromDate"
                    pvt_GetFromDate(e.GetCallbackValue("InvoiceTypeID"), _
                                    e.GetCallbackValue("ServicePartnerID"), _
                                    e.GetCallbackValue("CustomerType"), _
                                    e.GetCallbackValue("DepotID"))
                Case "GetFromDateForCleaning"
                    pvt_GetFromDateForCleaning(e.GetCallbackValue("InvoiceTypeID"), _
                                   e.GetCallbackValue("ServicePartnerID"), _
                                   e.GetCallbackValue("CustomerType"), _
                                   e.GetCallbackValue("DepotID"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetFromDate"
    Private Sub pvt_GetFromDate(ByVal bv_i32InvoiceTypeID As Int32, _
                                ByVal bv_i32ServicePartnerID As Int32, _
                                ByVal bv_strCustomerType As String, _
                                ByVal bv_i32DepotID As Int32)
        Try
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim objCommon As New CommonData
            ' Dim i32DepotID As Int32
            If objCommon.GetMultiLocationSupportConfig.ToLower = "false" Then
                bv_i32DepotID = objCommon.GetDepotID()
            Else
                If objCommon.GetOrganizationTypeCD = "BO" Then
                    bv_i32DepotID = objCommon.GetDepotID()
                End If
            End If
            Dim dtInvoiceHistory As New DataTable
            Dim i64CustomerID As Int64 = 0
            Dim i64InvoicingPartyID As Int64 = 0
            Dim strInvoiceType As String = String.Empty

            If bv_strCustomerType = "CUSTOMER" Then
                i64CustomerID = bv_i32ServicePartnerID
            ElseIf bv_strCustomerType = "PARTY" Then
                i64InvoicingPartyID = bv_i32ServicePartnerID
            ElseIf bv_strCustomerType.ToUpper = "AGENT" Then
                i64InvoicingPartyID = bv_i32ServicePartnerID
            End If

            'Get the Invoice Type for the corresponding Invoice type ID
            pvtGetInvoiceType(bv_i32InvoiceTypeID, strInvoiceType)

            dtInvoiceHistory = objInvoiceGeneration.pub_GetVInvoiceHistoryByInvoiceTypeCustomerBillingFlag(bv_i32DepotID, "FINAL", _
                                                                                                           strInvoiceType, i64CustomerID, _
                                                                                                           i64InvoicingPartyID).Tables(InvoiceGenerationData._INVOICE_HISTORY)

            If dtInvoiceHistory.Rows.Count > 0 AndAlso Not IsDBNull(dtInvoiceHistory.Rows(0).Item(InvoiceGenerationData.TO_BLLNG_DT)) Then
                pub_SetCallbackReturnValue("FROMDATE", CDate(dtInvoiceHistory.Rows(0).Item(InvoiceGenerationData.TO_BLLNG_DT)).AddDays(1).ToString("dd-MMM-yyy"))
            Else
                Dim strFromDate As String = String.Empty
                If bv_i32InvoiceTypeID = 84 And bv_strCustomerType.ToUpper() = "CUSTOMER" Then
                    objInvoiceGeneration.pub_GetRentalOnHireDateByCustomer(bv_i32DepotID, i64CustomerID, strFromDate)
                Else
                    If bv_strCustomerType.ToUpper() = "AGENT" Then
                        objInvoiceGeneration.GetActivityStatusByAgent(bv_i32DepotID, i64InvoicingPartyID, strFromDate)
                    Else
                        objInvoiceGeneration.pub_GetActivityStatusByCustomer(bv_i32DepotID, i64CustomerID, strFromDate, "GateIn", " ORDER BY GTN_DT ASC ")
                    End If

                End If
                pub_SetCallbackReturnValue("FROMDATE", strFromDate)
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_fnDraftInvoice"
    Private Sub pvt_fnDraftInvoice(ByVal bv_i32InvoiceTypeID As Int32, _
                                   ByVal bv_i64CustomerCurrencyID As Int64, _
                                   ByVal bv_i64DepotCurrencyID As Int64, _
                                   ByVal bv_decExchangeRate As Decimal, _
                                   ByVal bv_datFromDate As Date, _
                                   ByVal bv_datToDate As Date, _
                                   ByVal bv_i32ServicePartnerID As Int32, _
                                   ByVal bv_strCustomerType As String, _
                                   ByVal bv_strInvoiceNo As String, _
                                   ByVal i32DepotID As Int32)
        Try
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim objCommon As New CommonData
            dsInvoiceGeneration = CType(RetrieveData(INVOICE_GENERATION), InvoiceGenerationDataSet)
            Dim strTable1PrimaryID As String = String.Empty
            Dim strTable2PrimaryID As String = String.Empty
            Dim strInvoiceNo As String = String.Empty
            Dim strInvoiceFileName As String = String.Empty
            Dim i64CustomerID As Int64 = 0
            Dim i64InvoicingPartyID As Int64 = 0
            Dim i64AgentID As Int64 = 0
            Dim strInvoiceType As String = String.Empty
            'Dim i32DepotID As Int32 = CInt(objCommon.GetDepotID())
            Dim blnGenerateInvoiceNo As Boolean = False
            Dim dblTotalCustomerAmount As Double = 0.0
            Dim dblTotalDepotAmount As Double = 0.0
            Dim intCountEquipment As Integer = 0
            Dim strErrorMessage As String = ""
            If objCommon.GetMultiLocationSupportConfig.ToLower = "false" Then
                i32DepotID = objCommon.GetDepotID()
            Else
                If objCommon.GetOrganizationTypeCD = "BO" Then
                    i32DepotID = objCommon.GetDepotID()
                End If
            End If
            If bv_strCustomerType = "CUSTOMER" Then
                i64CustomerID = bv_i32ServicePartnerID
            ElseIf bv_strCustomerType = "PARTY" Then
                i64InvoicingPartyID = bv_i32ServicePartnerID
            ElseIf bv_strCustomerType.ToUpper = "AGENT" Then
                i64InvoicingPartyID = bv_i32ServicePartnerID
            End If

            If bv_strInvoiceNo <> String.Empty Then
                strInvoiceNo = bv_strInvoiceNo
                blnGenerateInvoiceNo = False
            Else
                blnGenerateInvoiceNo = True
            End If
            'Get the Invoice Type for the corresponding Invoice type ID
            pvtGetInvoiceType(bv_i32InvoiceTypeID, strInvoiceType)
            Select Case bv_i32InvoiceTypeID
                Case 78 'Handling & Storage                  
                    ''22266- Start
                    objInvoiceGeneration.pub_validateFinalizedInvoice(i32DepotID, bv_i32ServicePartnerID, strInvoiceType, bv_datFromDate, strErrorMessage)

                    If strErrorMessage <> "" Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackReturnValue("ErrorMessage", "Invoice already finalized for the selected period and invoice party,plese reset and select again")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    '' 22266- End
                    For Each drSelect As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HANDLING_CHARGE).Rows
                        If Not IsDBNull(drSelect.Item(InvoiceGenerationData.HNDLNG_CHRG_ID)) Then
                            If strTable1PrimaryID <> String.Empty Then
                                strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(InvoiceGenerationData.HNDLNG_CHRG_ID))
                            Else
                                strTable1PrimaryID = drSelect.Item(InvoiceGenerationData.HNDLNG_CHRG_ID)
                            End If
                        End If
                    Next
                    For Each drSelect As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_STORAGE_CHARGE).Rows
                        If Not IsDBNull(drSelect.Item(InvoiceGenerationData.STRG_CHRG_ID)) Then
                            If strTable2PrimaryID <> String.Empty Then
                                strTable2PrimaryID = String.Concat(strTable2PrimaryID, ",", drSelect.Item(InvoiceGenerationData.STRG_CHRG_ID))
                            Else
                                strTable2PrimaryID = drSelect.Item(InvoiceGenerationData.STRG_CHRG_ID)
                            End If
                        End If
                    Next
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                        dblTotalDepotAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                        dblTotalCustomerAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                        intCountEquipment = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                    End If
                Case 79 'Heating
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Select(String.Concat(InvoiceGenerationData.CHECKED, "='", True, "'")).Length = 0 Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._HEATING_INVOICE).Rows.Clear()
                    For Each drSelect As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Rows
                        If Not IsDBNull(drSelect.Item(InvoiceGenerationData.CHECKED)) AndAlso CBool(drSelect.Item(InvoiceGenerationData.CHECKED)) Then
                            dsInvoiceGeneration.Tables(InvoiceGenerationData._HEATING_INVOICE).ImportRow(drSelect)
                            If strTable1PrimaryID <> String.Empty Then
                                strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(InvoiceGenerationData.HTNG_ID))
                            Else
                                strTable1PrimaryID = drSelect.Item(InvoiceGenerationData.HTNG_ID)
                            End If
                        End If
                    Next
                    ''22309 - Start
                    objInvoiceGeneration.pub_validateFinalizedInvoiceByEquipmentTransactionID(i32DepotID, strTable1PrimaryID, strInvoiceType, strErrorMessage)
                    If strErrorMessage <> "" Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackReturnValue("ErrorMessage", "one of the selected equipment(s) heating invoice is already finalized")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    ''22309 - End
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                        dblTotalDepotAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.TTL_RT_NC & ")", String.Empty)) Then
                        dblTotalCustomerAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.TTL_RT_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._HEATING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                        intCountEquipment = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._HEATING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                    End If
                Case 80 'CLEANING
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Select(String.Concat(InvoiceGenerationData.CHECKED, "='", True, "'")).Length = 0 Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._CLEANING_INVOICE).Rows.Clear()
                    For Each drSelect As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Rows
                        If Not IsDBNull(drSelect.Item(InvoiceGenerationData.CHECKED)) AndAlso CBool(drSelect.Item(InvoiceGenerationData.CHECKED)) Then
                            dsInvoiceGeneration.Tables(InvoiceGenerationData._CLEANING_INVOICE).ImportRow(drSelect)
                            If strTable1PrimaryID <> String.Empty Then
                                strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(InvoiceGenerationData.CLNNG_CHRG_ID))
                            Else
                                strTable1PrimaryID = drSelect.Item(InvoiceGenerationData.CLNNG_CHRG_ID)
                            End If
                        End If
                    Next

                    ''Start 22294
                    objInvoiceGeneration.pub_validateFinalizedInvoiceByEquipmentTransactionID(i32DepotID, strTable1PrimaryID, strInvoiceType, strErrorMessage)
                    If strErrorMessage <> "" Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackReturnValue("ErrorMessage", "one of the selected equipment(s) cleaning invoice is already finalized")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If

                    ''End 22294
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                        dblTotalDepotAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                        dblTotalCustomerAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._CLEANING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                        intCountEquipment = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._CLEANING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                    End If
                Case 81 'REPAIR
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Select(String.Concat(InvoiceGenerationData.CHECKED, "='", True, "'")).Length = 0 Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._REPAIR_INVOICE).Rows.Clear()
                    For Each drSelect As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Rows
                        If Not IsDBNull(drSelect.Item(InvoiceGenerationData.CHECKED)) AndAlso CBool(drSelect.Item(InvoiceGenerationData.CHECKED)) Then
                            dsInvoiceGeneration.Tables(InvoiceGenerationData._REPAIR_INVOICE).ImportRow(drSelect)
                            If strTable1PrimaryID <> String.Empty Then
                                strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(InvoiceGenerationData.RPR_CHRG_ID))
                            Else
                                strTable1PrimaryID = drSelect.Item(InvoiceGenerationData.RPR_CHRG_ID)
                            End If
                        End If
                    Next

                    ''22370 - Start
                    objInvoiceGeneration.pub_validateFinalizedInvoiceByEquipmentTransactionID(i32DepotID, strTable1PrimaryID, strInvoiceType, strErrorMessage)
                    If strErrorMessage <> "" Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackReturnValue("ErrorMessage", "one of the selected equipment(s) repair invoice is already finalized")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    ''22309 - End
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                        dblTotalDepotAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                        dblTotalCustomerAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._REPAIR_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                        intCountEquipment = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._REPAIR_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                    End If
                Case 82 'MISCELLANEOUS
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Select(String.Concat(InvoiceGenerationData.CHECKED, "='", True, "'")).Length = 0 Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Rows.Clear()
                    For Each drSelect As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows
                        If Not IsDBNull(drSelect.Item(InvoiceGenerationData.CHECKED)) AndAlso CBool(drSelect.Item(InvoiceGenerationData.CHECKED)) Then
                            dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).ImportRow(drSelect)
                            If strTable1PrimaryID <> String.Empty Then
                                strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(InvoiceGenerationData.MSCLLNS_INVC_ID))
                            Else
                                strTable1PrimaryID = drSelect.Item(InvoiceGenerationData.MSCLLNS_INVC_ID)
                            End If
                        End If
                    Next

                    ''22312 - Start
                    objInvoiceGeneration.pub_validateFinalizedInvoiceByEquipmentTransactionID(i32DepotID, strTable1PrimaryID, strInvoiceType, strErrorMessage)
                    If strErrorMessage <> "" Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackReturnValue("ErrorMessage", "one of the selected equipment(s) miscellaneous invoice is already finalized")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    ''22309 - End
                    ''Activity Submit Validation

                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                        dblTotalDepotAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty)) Then
                        dblTotalCustomerAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                        intCountEquipment = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                    End If

                Case 140 'Credit Note
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Select(String.Concat(InvoiceGenerationData.CHECKED, "='", True, "'")).Length = 0 Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._CREDIT_NOTE).Rows.Clear()
                    For Each drSelect As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows
                        If Not IsDBNull(drSelect.Item(InvoiceGenerationData.CHECKED)) AndAlso CBool(drSelect.Item(InvoiceGenerationData.CHECKED)) Then
                            dsInvoiceGeneration.Tables(InvoiceGenerationData._CREDIT_NOTE).ImportRow(drSelect)
                            If strTable1PrimaryID <> String.Empty Then
                                strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(InvoiceGenerationData.MSCLLNS_INVC_ID))
                            Else
                                strTable1PrimaryID = drSelect.Item(InvoiceGenerationData.MSCLLNS_INVC_ID)
                            End If
                        End If
                    Next

                    ''22312 - Start
                    objInvoiceGeneration.pub_validateFinalizedInvoiceByEquipmentTransactionID(i32DepotID, strTable1PrimaryID, strInvoiceType, strErrorMessage)
                    If strErrorMessage <> "" Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackReturnValue("ErrorMessage", "one of the selected equipment(s) miscellaneous invoice is already finalized")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    ''22309 - End
                    ''Activity Submit Validation

                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                        dblTotalDepotAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty)) Then
                        dblTotalCustomerAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                        intCountEquipment = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                    End If
                Case 83 'Transportation
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Select(String.Concat(InvoiceGenerationData.CHECKED, "='", True, "'")).Length = 0 Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._TRANSPORTATION_INVOICE).Rows.Clear()
                    Dim intFullTrips As Integer = 0
                    Dim intEmptySingleTrips As Integer = 0
                    Dim intEmptyNonSingleTrips As Integer = 0
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(InvoiceGenerationData.CHECKED, "= True AND ", CommonUIData.EQPMNT_STT_ID, " IN(94,121,122)"))) Then
                        intFullTrips = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(InvoiceGenerationData.CHECKED, "= True AND ", CommonUIData.EQPMNT_STT_ID, " IN(94,121,122)")))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(InvoiceGenerationData.CHECKED, "= True AND ", CommonUIData.EQPMNT_STT_ID, "=93 AND EMPTY_SNGL_ID=108"))) Then
                        intEmptySingleTrips = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(InvoiceGenerationData.CHECKED, "= True AND ", CommonUIData.EQPMNT_STT_ID, " IN (93,120) AND EMPTY_SNGL_ID=108")))
                    End If
                    If Not IsDBNull(Math.Ceiling(CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(InvoiceGenerationData.CHECKED, "= True AND ", CommonUIData.EQPMNT_STT_ID, " IN(93,120) AND (EMPTY_SNGL_ID=109 OR EMPTY_SNGL_ID IS NULL)"))) / 2)) Then
                        intEmptyNonSingleTrips = CInt(Math.Ceiling(CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(InvoiceGenerationData.CHECKED, "= True AND ", CommonUIData.EQPMNT_STT_ID, " IN(93,120) AND (EMPTY_SNGL_ID=109 OR EMPTY_SNGL_ID IS NULL)"))) / 2))
                    End If
                    For Each drSelect As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Rows
                        If Not IsDBNull(drSelect.Item(InvoiceGenerationData.CHECKED)) AndAlso CBool(drSelect.Item(InvoiceGenerationData.CHECKED)) Then
                            drSelect.Item(CommonUIData.NO_OF_TRIPS) = intFullTrips + intEmptySingleTrips + intEmptyNonSingleTrips
                            dsInvoiceGeneration.Tables(InvoiceGenerationData._TRANSPORTATION_INVOICE).ImportRow(drSelect)
                            If strTable1PrimaryID <> String.Empty Then
                                strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(InvoiceGenerationData.TRNSPRTTN_CHRG_ID))
                            Else
                                strTable1PrimaryID = drSelect.Item(InvoiceGenerationData.TRNSPRTTN_CHRG_ID)
                            End If
                        End If
                    Next
                    ''22368 - Start
                    objInvoiceGeneration.pub_validateFinalizedInvoiceByEquipmentTransactionID(i32DepotID, strTable1PrimaryID, strInvoiceType, strErrorMessage)
                    If strErrorMessage <> "" Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackReturnValue("ErrorMessage", "one of the selected equipment(s) transportation invoice is already finalized")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    ''22309 - End
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.DPT_AMNT & ")", String.Empty)) Then
                        dblTotalDepotAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.DPT_AMNT & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.CSTMR_AMNT & ")", String.Empty)) Then
                        dblTotalCustomerAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.CSTMR_AMNT & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                        intCountEquipment = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                    End If
                Case 84 'Rental
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._RENTAL_INVOICE).Rows.Clear()
                    For Each drSelect As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_RENTAL_CHARGE).Rows
                        If Not IsDBNull(drSelect.Item(InvoiceGenerationData.RNTL_CHRG_ID)) Then
                            drSelect.Item(InvoiceGenerationData.BLLNG_FLG) = "D"
                            dsInvoiceGeneration.Tables(InvoiceGenerationData._RENTAL_INVOICE).ImportRow(drSelect)
                            If strTable1PrimaryID <> String.Empty Then
                                strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(InvoiceGenerationData.RNTL_CHRG_ID))
                            Else
                                strTable1PrimaryID = drSelect.Item(InvoiceGenerationData.RNTL_CHRG_ID)
                            End If
                        End If
                    Next

                    ''22266- Start
                    objInvoiceGeneration.pub_validateFinalizedInvoice(i32DepotID, bv_i32ServicePartnerID, strInvoiceType, bv_datFromDate, strErrorMessage)

                    If strErrorMessage <> "" Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackReturnValue("ErrorMessage", "Invoice already finalized for the selected period and invoice party,plese reset and select again")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    '' 22266- End

                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                        dblTotalDepotAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                        dblTotalCustomerAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._RENTAL_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                        intCountEquipment = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._RENTAL_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                    End If
                    'GWS
                Case 151 'Inspection
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Select(String.Concat(InvoiceGenerationData.CHECKED, "='", True, "'")).Length = 0 Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                    '  If Not (dsInvoiceGeneration.Tables(InvoiceGenerationData._INSPECTION_INVOICE) Is Nothing) Then
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._INSPECTION_INVOICE).Rows.Clear()
                    '  End If
                    For Each drSelect As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Rows
                        If Not IsDBNull(drSelect.Item(InvoiceGenerationData.CHECKED)) AndAlso CBool(drSelect.Item(InvoiceGenerationData.CHECKED)) Then
                            dsInvoiceGeneration.Tables(InvoiceGenerationData._INSPECTION_INVOICE).ImportRow(drSelect)
                            If strTable1PrimaryID <> String.Empty Then
                                strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(InvoiceGenerationData.INSPCTN_CHRG_ID))
                            Else
                                strTable1PrimaryID = drSelect.Item(InvoiceGenerationData.INSPCTN_CHRG_ID)
                            End If
                        End If
                    Next

                    ''Start 22294
                    objInvoiceGeneration.pub_validateFinalizedInvoiceByEquipmentTransactionID(i32DepotID, strTable1PrimaryID, strInvoiceType, strErrorMessage)
                    If strErrorMessage <> "" Then
                        pub_SetCallbackReturnValue("Select", "False")
                        pub_SetCallbackReturnValue("ErrorMessage", "one of the selected equipment(s) cleaning invoice is already finalized")
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If

                    ''End 22294
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._INSPECTION_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                        dblTotalDepotAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._INSPECTION_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._INSPECTION_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                        dblTotalCustomerAmount = CDbl(dsInvoiceGeneration.Tables(InvoiceGenerationData._INSPECTION_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                    End If
                    If Not IsDBNull(dsInvoiceGeneration.Tables(InvoiceGenerationData._INSPECTION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                        intCountEquipment = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._INSPECTION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                    End If
            End Select

            'Get the Invoice Type for the corresponding Invoice type ID
            pvtGetInvoiceType(bv_i32InvoiceTypeID, strInvoiceType)

            If strTable1PrimaryID <> String.Empty OrElse strTable2PrimaryID <> String.Empty Then
                objInvoiceGeneration.pub_UpdateMiscInvoiceNoBillingFlag(strTable1PrimaryID, _
                                                                        strTable2PrimaryID, "D", _
                                                                        strInvoiceNo, _
                                                                        bv_i64CustomerCurrencyID, _
                                                                        bv_i64DepotCurrencyID, _
                                                                        bv_decExchangeRate, _
                                                                        bv_datFromDate, _
                                                                        bv_datToDate, _
                                                                        dblTotalCustomerAmount, _
                                                                        dblTotalDepotAmount, _
                                                                        i64CustomerID, _
                                                                        i64InvoicingPartyID, _
                                                                        i32DepotID, _
                                                                        objCommon.GetCurrentUserName(), _
                                                                        blnGenerateInvoiceNo, _
                                                                        bv_i32InvoiceTypeID, _
                                                                        strInvoiceType, _
                                                                        strInvoiceFileName, _
                                                                        intCountEquipment, _
                                                                        dsInvoiceGeneration)
            End If

            If strInvoiceNo <> Nothing Then
                If dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Rows.Count > 0 Then
                    For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Rows
                        dr.Item(InvoiceGenerationData.DRFT_INVC_NO) = strInvoiceNo
                    Next
                End If
                If dsInvoiceGeneration.Tables(InvoiceGenerationData._INSPECTION_INVOICE).Rows.Count > 0 Then
                    For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._INSPECTION_INVOICE).Rows
                        dr.Item(InvoiceGenerationData.DRFT_INVC_NO) = strInvoiceNo
                    Next
                End If

                If dsInvoiceGeneration.Tables(InvoiceGenerationData._REPAIR_INVOICE).Rows.Count > 0 Then
                    For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._REPAIR_INVOICE).Rows
                        dr.Item(InvoiceGenerationData.DRFT_INVC_NO) = strInvoiceNo
                    Next
                End If

            End If

            CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            pub_SetCallbackReturnValue("InvoiceNo", strInvoiceNo)
            pub_SetCallbackReturnValue("FileName", strInvoiceFileName)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CheckDraftExists"
    Private Sub pvt_CheckDraftExists(ByVal bv_i32InvoiceTypeID As Int32, _
                                     ByVal bv_datFromDate As Date, _
                                     ByVal bv_datToDate As Date, _
                                     ByVal bv_i32ServicePartnerID As Int32, _
                                     ByVal bv_strCustomerType As String, _
                                     ByVal bv_i32DepotID As Int32)
        Try
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim objCommon As New CommonData
            dsInvoiceGeneration = CType(RetrieveData(INVOICE_GENERATION), InvoiceGenerationDataSet)
            Dim strPrimaryID As String = String.Empty
            Dim strInvoiceNo As String = String.Empty
            Dim i64CustomerID As Int64 = 0
            Dim i64InvoicingPartyID As Int64 = 0
            Dim strInvoiceType As String = String.Empty
            'Dim i32DepotID As Int32 = CInt(objCommon.GetDepotID())
            If objCommon.GetMultiLocationSupportConfig.ToLower = "false" Then
                bv_i32DepotID = objCommon.GetDepotID()
            Else
                If objCommon.GetDepotID() <> objCommon.GetHeadQuarterID Then
                    bv_i32DepotID = objCommon.GetDepotID()
                End If
            End If

            If bv_strCustomerType = "CUSTOMER" Then
                i64CustomerID = bv_i32ServicePartnerID
            ElseIf bv_strCustomerType = "PARTY" Then
                i64InvoicingPartyID = bv_i32ServicePartnerID
                'GWS
            ElseIf bv_strCustomerType = "AGENT" Then
                i64InvoicingPartyID = bv_i32ServicePartnerID
            End If

            Dim dtInvoiceHistory As New DataTable

            pvtGetInvoiceType(bv_i32InvoiceTypeID, strInvoiceType)

            dtInvoiceHistory = objInvoiceGeneration.pub_GetInvoice_HistoryByFromToBillingDate(i64CustomerID, "DRAFT", _
                                                                                              bv_i32DepotID, bv_datFromDate, _
                                                                                              bv_datToDate, i64InvoicingPartyID, _
                                                                                              strInvoiceType).Tables(InvoiceGenerationData._INVOICE_HISTORY)
            If dtInvoiceHistory.Rows.Count > 0 Then
                For Each dr As DataRow In dtInvoiceHistory.Rows
                    If strInvoiceNo <> String.Empty Then
                        strInvoiceNo = String.Concat(strInvoiceNo, ",", CStr(dr.Item(InvoiceGenerationData.INVC_NO)))
                    Else
                        strInvoiceNo = CStr(dr.Item(InvoiceGenerationData.INVC_NO))
                    End If
                Next
                pub_SetCallbackReturnValue("InvoiceNo", strInvoiceNo)
                pub_SetCallbackReturnValue("FileName", CStr(dtInvoiceHistory.Rows(0).Item(InvoiceGenerationData.FL_NM)))
                pub_SetCallbackStatus(False)
            Else
                pub_SetCallbackStatus(True)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvtGetInvoiceType"
    Private Sub pvtGetInvoiceType(ByVal bv_i32InvoiceTypeID As Integer, _
                                  ByRef bv_strInvoiceType As String)
        Try
            Select Case bv_i32InvoiceTypeID
                Case 78 'Handling & Storage
                    bv_strInvoiceType = "HS"
                Case 79 'HEATING
                    bv_strInvoiceType = "HT"
                Case 80 'CLEANING
                    bv_strInvoiceType = "CI"
                Case 81 'REPAIR
                    bv_strInvoiceType = "RP"
                Case 82 'MISCELLANEOUS
                    bv_strInvoiceType = "MI"
                Case 83 'Transportation
                    bv_strInvoiceType = "TP"
                Case 84 'Rental
                    bv_strInvoiceType = "RT"
                Case 140 'Credit Note
                    bv_strInvoiceType = "CN"
                Case 151 'Inspection
                    bv_strInvoiceType = "IN"
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(lkpInvoiceType)
        CommonWeb.pub_AttachHasChanges(lkpServicePartner)
        CommonWeb.pub_AttachHasChanges(datPeriodFrom)
        CommonWeb.pub_AttachHasChanges(datPeriodTo)
        pub_SetGridChanges(ifgHandlingStorage, "ITab1_0")
        pub_SetGridChanges(ifgRepair, "ITab1_0")
        pub_SetGridChanges(ifgCleaning, "ITab1_0")
        pub_SetGridChanges(ifgMiscellaneous, "ITab1_0")
        pub_SetGridChanges(ifgCreditNote, "ITab1_0")
        pub_SetGridChanges(ifgHeating, "ITab1_0")
        pub_SetGridChanges(ifgTransportation, "ITab1_0")
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Billing/InvoiceGeneration.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgHandlingStorage_ClientBind"
    Protected Sub ifgHandlingStorage_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgHandlingStorage.ClientBind
        Try
            Dim objCommon As New CommonData
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim intDepotId As Integer = objCommon.GetDepotID()
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" AndAlso objCommon.GetOrganizationTypeCD = "HQ" Then
                intDepotId = e.Parameters("DepotID")
            End If

            If Not e.Parameters("CustomerId") Is Nothing Then
                Dim dtHandling As New DataTable
                Dim dtStorage As New DataTable
                Dim dtCustomer As New DataTable
                Dim dtBankDetail As New DataTable
                Dim dtForeignBankDetail As New DataTable
                Dim objCommonUI As New CommonUI
                Dim dsDepot As DataSet
                Dim fromDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodFrom"))
                Dim toDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodTo"))
                Dim strCustomerType As String = e.Parameters("CustomerType")
                Dim i64CustomerID As Long = CommonUIs.iLng(e.Parameters("CustomerId"))
                Dim i32InvoiceTypeID As Long = CommonUIs.iInt(e.Parameters("InvoiceTypeID"))
                Dim decExchangeRate As Decimal
                Dim dtExchangeRate As New DataTable

                Dim str_067InvoiceGenerationGWSBit As String = Nothing
                Dim objCommonConfig As New ConfigSetting()

                str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", intDepotId)


                If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then

                    If strCustomerType.ToUpper() = "AGENT" Then
                        dtHandling = objInvoiceGeneration.pub_Get_GWSHandlingChargeByAgentId(i64CustomerID, fromDate, toDate, intDepotId).Tables(InvoiceGenerationData._V_HANDLING_CHARGE)
                        dtStorage = objInvoiceGeneration.pub_Get_GWSStorageChargeByAgentId(i64CustomerID, fromDate, toDate, intDepotId).Tables(InvoiceGenerationData._V_STORAGE_CHARGE)
                    Else
                        'To fill Datatable from Handling, Storage Respectively
                        dtHandling = objInvoiceGeneration.Pub_Get_GWSHandlingChargeByCustomerId(i64CustomerID, fromDate, toDate, intDepotId).Tables(InvoiceGenerationData._V_HANDLING_CHARGE)
                        dtStorage = objInvoiceGeneration.pub_Get_GWSStorageChargeByCustomerId(i64CustomerID, fromDate, toDate, intDepotId).Tables(InvoiceGenerationData._V_STORAGE_CHARGE)
                    End If

                Else
                    'To fill Datatable from Handling, Storage Respectively
                    dtHandling = objInvoiceGeneration.pub_GetHandlingChargeByCustomerId(i64CustomerID, fromDate, toDate, intDepotId).Tables(InvoiceGenerationData._V_HANDLING_CHARGE)
                    dtStorage = objInvoiceGeneration.pub_GetStorageChargeByCustomerId(i64CustomerID, fromDate, toDate, intDepotId).Tables(InvoiceGenerationData._V_STORAGE_CHARGE)
                End If


                'To fill Customer Details
                If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID, dtCustomer)
                    If objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId) Is Nothing Then
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = DBNull.Value
                    Else
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId)
                    End If

                Else
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, intDepotId, dtCustomer)
                End If

                'To Fill Depot Details
                dsDepot = objCommonUI.pub_GetDepoDetail(intDepotId)
                'TO Fill Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)
                'TO Fill Foreign Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotIdForForeignCurrency(intDepotId, dtForeignBankDetail)
                'TO get Exchange Rate
                If dtCustomer.Rows.Count > 0 Then
                    If Not IsDBNull(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC)) Then
                        decExchangeRate = CDec(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC))
                    Else
                        decExchangeRate = pvt_GetExchangeRate(dtCustomer, dtBankDetail)
                    End If
                Else
                    decExchangeRate = 0
                End If
                'Data Table Merged with DataSet
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HANDLING_CHARGE).Merge(dtHandling)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_STORAGE_CHARGE).Merge(dtStorage)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Merge(dtCustomer)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Merge(dtBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._FOREIGN_BANK_DETAIL).Merge(dtForeignBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._DEPOT).Merge(dsDepot.Tables(InvoiceGenerationData._DEPOT))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dtCustomer.Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))
                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dtCustomer.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))


                objInvoiceGeneration.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE), _
                                                                         decExchangeRate, fromDate, toDate, 0, 0, i64CustomerID, CType(dsInvoiceGeneration, DataSet), _
                                                                         intDepotId, str_067InvoiceGenerationGWSBit)

                dsInvoiceGeneration = CType(dsInvoiceGeneration, InvoiceGenerationDataSet)

                Dim decTotalCustomerAmount As Decimal
                Dim decTotalDepotAmount As Decimal
                For Each drHSInvoice As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Rows
                    decTotalCustomerAmount = decTotalCustomerAmount + CDec(drHSInvoice.Item(InvoiceGenerationData.TTL_CSTS_NC))
                    decTotalDepotAmount = decTotalDepotAmount + CDec(drHSInvoice.Item(InvoiceGenerationData.DPT_TTL_NC))
                Next

                If dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Rows.Count > 0 Then
                    Dim dtInvoiceTable As New DataTable
                    dtInvoiceTable = dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Copy
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Rows.Clear()
                    dtInvoiceTable = dtInvoiceTable.Select(String.Empty, String.Concat(InvoiceGenerationData.GTN_DT, " ASC")).CopyToDataTable
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE).Merge(dtInvoiceTable)
                End If

                e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._HANDLING_STORAGE_INVOICE)
                e.OutputParameters.Add("Activity", "HANDLING")
                e.OutputParameters.Add("ExRate", CDec(decExchangeRate).ToString("0.0000"))
                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustAmount", decTotalCustomerAmount.ToString("0.00"))
                e.OutputParameters.Add("DepotAmount", decTotalDepotAmount.ToString("0.00"))
                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgHandlingStorage_RowDataBound"
    Protected Sub ifgHandlingStorage_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgHandlingStorage.RowDataBound
        Try

            Dim str_067InvoiceGenerationGWSBit As String = Nothing
            Dim objCommonConfig As New ConfigSetting()
            Dim objCommon As New CommonData
            Dim intDepotId As Integer = objCommon.GetDepotID()

            str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", intDepotId)


            If e.Row.RowType = DataControlRowType.Header Then
                If strDepotCurrency = String.Empty OrElse strCustomerCurrency = String.Empty Then
                    dsInvoiceGeneration = CType(RetrieveData(INVOICE_GENERATION), InvoiceGenerationDataSet)
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows.Count > 0 Then
                        strDepotCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows(0).Item(CommonUIData.CRRNCY_CD))
                    End If
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows.Count > 0 Then
                        strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(CommonUIData.CSTMR_CRRNCY_CD))
                    End If
                End If
                CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("HNDIN in ", strDepotCurrency)
                CType(e.Row.Cells(11), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("HNDOUT in ", strDepotCurrency)
                CType(e.Row.Cells(12), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("STR in ", strDepotCurrency)
                CType(e.Row.Cells(13), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strDepotCurrency)
                CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strCustomerCurrency)



                If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then
                    CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")

                Else
                    CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "table-cell")

                End If

            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then
                    CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")

                Else
                    CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "table-cell")

                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgRepair_ClientBind"

    Protected Sub ifgRepair_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgRepair.ClientBind
        Try
            Dim objCommon As New CommonData
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim intDepotId As Integer = objCommon.GetDepotID()

            Dim strFilter As String = String.Empty
            If Not ifgRepair.DataSource Is Nothing AndAlso CType(ifgRepair.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgRepair.DataSource, DataView).RowFilter
            End If
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" AndAlso objCommon.GetOrganizationTypeCD = "HQ" Then
                intDepotId = CommonUIs.iInt(e.Parameters("DepotID"))
            End If
            If Not e.Parameters("CustomerId") Is Nothing Then
                Dim dtRepairCharge As New DataTable
                Dim dtStorage As New DataTable
                Dim dtCustomer As New DataTable
                Dim dtBankDetail As New DataTable
                Dim dtForeignBankDetail As New DataTable
                Dim objCommonUI As New CommonUI
                Dim dsDepot As DataSet
                Dim fromDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodFrom"))
                Dim toDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodTo"))
                Dim strCustomerType As String = e.Parameters("CustomerType")
                Dim i64CustomerID As Long = CommonUIs.iLng(e.Parameters("CustomerId"))
                Dim i32InvoiceTypeID As Long = CommonUIs.iInt(e.Parameters("InvoiceTypeID"))
                Dim decExchangeRate As Decimal
                Dim dtExchangeRate As New DataTable
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                Dim str_067InvoiceGenerationGWSBit As String = Nothing
                Dim objCommonConfig As New ConfigSetting()

                str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", intDepotId)


                If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then

                    If strCustomerType.ToUpper() = "AGENT" Then
                        'To fill Datatable Repair charge details
                        dtRepairCharge = objInvoiceGeneration.pub_GetRepairChargeGWS_ByAgentId(i64CustomerID, fromDate, _
                                                                                              toDate, intDepotId).Tables(InvoiceGenerationData._V_REPAIR_CHARGE)
                    Else
                        'To fill Datatable Repair charge details
                        dtRepairCharge = objInvoiceGeneration.pub_GetRepairChargeGWS_ByCustomerId(i64CustomerID, fromDate, _
                                                                                              toDate, intDepotId).Tables(InvoiceGenerationData._V_REPAIR_CHARGE)
                    End If
                Else
                    'To fill Datatable Repair charge details
                    dtRepairCharge = objInvoiceGeneration.pub_GetRepairChargeByCustomerId(i64CustomerID, fromDate, _
                                                                                          toDate, intDepotId).Tables(InvoiceGenerationData._V_REPAIR_CHARGE)
                End If


                'To fill Customer Details
                If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                    ' objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID(), dtCustomer)
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID, dtCustomer)
                    If objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId) Is Nothing Then
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = DBNull.Value
                    Else
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId)
                    End If
                Else
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetDepotID(), dtCustomer)
                End If
                'To Fill Depot Details
                dsDepot = objCommonUI.pub_GetDepoDetail(intDepotId)
                'TO Fill Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)
                'TO Fill Foreign Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotIdForForeignCurrency(intDepotId, dtForeignBankDetail)
                'TO get Exchange Rate
                If dtCustomer.Rows.Count > 0 Then
                    If Not IsDBNull(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC)) Then
                        decExchangeRate = CDec(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC))
                    Else
                        decExchangeRate = pvt_GetExchangeRate(dtCustomer, dtBankDetail)
                    End If
                Else
                    decExchangeRate = 0
                End If
                objInvoiceGeneration.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtRepairCharge, _
                                                                         decExchangeRate, fromDate, toDate, decCustomerAmount, _
                                                                         decDepotAmount)

                'Data Table Merged with DataSet
                Dim dtValidRepiarInvoice As New DataTable
                dtValidRepiarInvoice = dtRepairCharge.Clone
                If dtRepairCharge.Select(String.Concat(InvoiceGenerationData.RPR_APPRVL_AMNT_NC, " IS NOT NULL AND ", InvoiceGenerationData.RPR_APPRVL_AMNT_NC, " >0 AND ", InvoiceGenerationData.RPR_APPRVL_AMNT_NC, " >0.00")).Length > 0 Then
                    dtValidRepiarInvoice = dtRepairCharge.Select(String.Concat(InvoiceGenerationData.RPR_APPRVL_AMNT_NC, " IS NOT NULL AND ", InvoiceGenerationData.RPR_APPRVL_AMNT_NC, " >0 AND ", InvoiceGenerationData.RPR_APPRVL_AMNT_NC, " > 0.00")).CopyToDataTable
                End If
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Merge(dtValidRepiarInvoice)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Merge(dtCustomer)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Merge(dtBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._FOREIGN_BANK_DETAIL).Merge(dtForeignBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._DEPOT).Merge(dsDepot.Tables(InvoiceGenerationData._DEPOT))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dtCustomer.Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))
                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dtCustomer.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                Dim drPendingInvoice As DataRow
                For Each drRepair As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Rows
                    'To fill the dataset for pending invoice
                    drPendingInvoice = dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).NewRow()
                    For Each dc As DataColumn In dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).Columns
                        If dtRepairCharge.Columns.Contains(dc.ColumnName) AndAlso Not IsDBNull(drRepair.Item(dc.ColumnName)) Then
                            drPendingInvoice.Item(dc.ColumnName) = drRepair.Item(dc.ColumnName)
                        End If
                    Next
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).Rows.Add(drPendingInvoice)
                Next

                If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Rows.Count > 0 Then
                    Dim dtInvoiceTable As New DataTable
                    dtInvoiceTable = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Copy
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Rows.Clear()
                    dtInvoiceTable = dtInvoiceTable.Select(String.Empty, String.Concat(InvoiceGenerationData.GTN_DT, " ASC")).CopyToDataTable
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Merge(dtInvoiceTable)
                End If

                ' e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE)

                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = False
                Next

                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Copy())
                e.DataSource = dv

                '51063 - Fix
                decCustomerAmount = CDec("0.00")
                decDepotAmount = CDec("0.00")


                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Merge(dt)

                e.OutputParameters.Add("Activity", "REPAIR")
                e.OutputParameters.Add("ExRate", CDec(decExchangeRate).ToString("0.0000"))
                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If

            'Repair Heder Operation
            If Not e.Parameters("CheckState") Is Nothing Then
                dsInvoiceGeneration = RetrieveData(INVOICE_GENERATION)
                Dim blnCheckState As Boolean = e.Parameters("CheckState")

                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = blnCheckState
                Next
                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Copy())
                dv.RowFilter = strFilter
                e.DataSource = dv

                '51063 - Fix
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                Dim dtAmount As New DataTable
                dtAmount = dv.ToTable().Copy()
                If e.Parameters("CheckState") = True Then
                    decCustomerAmount = CDec(dtAmount.Compute("SUM(TTL_CSTS_NC)", "NOT TTL_CSTS_NC IS NULL"))
                    decDepotAmount = CDec(dtAmount.Compute("SUM(DPT_TTL_NC)", "NOT DPT_TTL_NC IS NULL"))
                Else
                    decCustomerAmount = CDec("0.00")
                    decDepotAmount = CDec("0.00")
                End If

                Dim dtBankDetail As New DataTable
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)

                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))

                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)

                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))


                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Merge(dt)
                CacheData("ifgRepair_Check", e.Parameters("CheckState"))
                'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE)
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgRepair_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgRepair.RowDataBound
        Try

            Dim str_067InvoiceGenerationGWSBit As String = Nothing
            Dim objCommonConfig As New ConfigSetting()
            Dim objCommon As New CommonData
            Dim intDepotId As Integer = objCommon.GetDepotID()
            str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", intDepotId)



            If e.Row.RowType = DataControlRowType.Header Then
                If strDepotCurrency = String.Empty OrElse strCustomerCurrency = String.Empty Then
                    dsInvoiceGeneration = CType(RetrieveData(INVOICE_GENERATION), InvoiceGenerationDataSet)
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows.Count > 0 Then
                        strDepotCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows(0).Item(CommonUIData.CRRNCY_CD))
                    End If
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows.Count > 0 Then
                        strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(CommonUIData.CSTMR_CRRNCY_CD))
                    End If
                End If
                CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Leak Test in ", strDepotCurrency)
                CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Addl Cleaning in ", strDepotCurrency)
                CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("PTI in ", strDepotCurrency)
                CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Survey in ", strDepotCurrency)

                CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Material Cost in ", strDepotCurrency)
                CType(e.Row.Cells(11), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Man Hour Cost in ", strDepotCurrency)
                CType(e.Row.Cells(12), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Repairs in ", strDepotCurrency)


                'CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Repairs in ", strDepotCurrency)
                CType(e.Row.Cells(13), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strDepotCurrency)
                CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strCustomerCurrency)


                'Header Check Box
                Dim ifgCheck As New CheckBox
                ifgCheck.EnableViewState = True
                'ifgCheck.Checked = True
                Dim blnCheck As Boolean = False
                If Not RetrieveData("ifgRepair_Check") Is Nothing Then
                    blnCheck = CBool(RetrieveData("ifgRepair_Check"))
                End If
                ifgCheck.Checked = False

                ifgCheck.Attributes.Add("onclick", "fnInvoiceHerderCheckBox('ifgRepair',this);return false;")
                e.Row.Cells(15).Controls.Add(ifgCheck)

                If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then
                    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    CType(e.Row.Cells(12), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                Else
                    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                    CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                    CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                    CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                    CType(e.Row.Cells(12), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                End If

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                Dim chk As iFgCheckBox
                chk = CType(e.Row.Cells(15).Controls(0), iFgCheckBox)
                chk.Attributes.Add("onclick", "fnCalcRepairAmount(this);")

                If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then
                    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    CType(e.Row.Cells(12), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                Else
                    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                    CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                    CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                    CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                    CType(e.Row.Cells(12), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgRepair_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgRepair.RowUpdated
        Try
            CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgRepair_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgRepair.RowUpdating
        Try
            Dim strFilter As String = String.Empty
            If Not ifgRepair.DataSource Is Nothing AndAlso CType(ifgRepair.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgRepair.DataSource, DataView).RowFilter
            End If


            Dim dv As DataView = ifgRepair.DataSource
            dv.RowFilter = strFilter

            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Rows.Clear()
            Dim dt As DataTable = dv.ToTable()
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Merge(dt)

            Dim str_067InvoiceGenerationGWSBit As String = Nothing
            Dim objCommonConfig As New ConfigSetting()
            Dim objCommon As New CommonData
            Dim intDepotId As Integer = objCommon.GetDepotID()
            str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", intDepotId)

            If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then
                e.OldValues(InvoiceGenerationData.CHECKED) = e.NewValues(11)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Rows(e.RowIndex).Item(InvoiceGenerationData.CHECKED) = e.NewValues(11)


                Dim dv12 As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Copy())
                ifgRepair.DataSource = dv12

            Else
                e.OldValues(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_REPAIR_CHARGE).Rows(e.RowIndex).Item(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgCleaning  Events"

    Protected Sub ifgCleaning_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgCleaning.ClientBind
        Try
            Dim objCommon As New CommonData
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim intDepotId As Integer = objCommon.GetDepotID()
            Dim strFilter As String = String.Empty
            Dim bln_073_KeyExists As Boolean
            Dim str_073_KeyValue As String
            str_073_KeyValue = objCommon.GetConfigSetting("073", bln_073_KeyExists)
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" AndAlso objCommon.GetOrganizationTypeCD = "HQ" Then
                intDepotId = e.Parameters("DepotID")
            Else
                intDepotId = objCommon.GetDepotID()
            End If
            If Not ifgCleaning.DataSource Is Nothing AndAlso CType(ifgCleaning.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgCleaning.DataSource, DataView).RowFilter
            End If
            If e.Parameters("PeriodFrom") = "" AndAlso e.Parameters("PeriodTo") = "" Then
                If Not ifgCleaning.DataSource Is Nothing Then
                    Dim dtCLeaning As DataTable = CType(ifgCleaning.DataSource, DataView).ToTable
                    If Not dtCLeaning Is Nothing Then
                        dtCLeaning.Clear()
                    End If
                    Dim dvClean As New DataView(dtCLeaning)
                    e.DataSource = dvClean
                End If
                e.OutputParameters.Add("WarningMsg", "Hide")
                Exit Sub
            End If
            If Not e.Parameters("CustomerId") Is Nothing Then
                Dim dtCleaningCharge As New DataTable
                Dim dtStorage As New DataTable
                Dim dtCustomer As New DataTable
                Dim dtBankDetail As New DataTable
                Dim dtForeignBankDetail As New DataTable
                Dim objCommonUI As New CommonUI
                Dim dsDepot As DataSet
                Dim fromDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodFrom"))
                Dim toDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodTo"))
                Dim strCustomerType As String = e.Parameters("CustomerType")
                Dim i64CustomerID As Long = CommonUIs.iLng(e.Parameters("CustomerId"))
                Dim i32InvoiceTypeID As Long = CommonUIs.iInt(e.Parameters("InvoiceTypeID"))
                Dim decExchangeRate As Decimal
                Dim dtExchangeRate As New DataTable
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                'To fill Datatable Cleaning charge details
                dtCleaningCharge = objInvoiceGeneration.pub_GetCleaningChargeByCustomerId(i64CustomerID, fromDate, _
                                                                                          toDate, intDepotId, strCustomerType, RetrieveData("BackDated").ToString).Tables(InvoiceGenerationData._V_CLEANING_CHARGE)
                'To fill Customer Details based on multi location config
                If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                    ' objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID(), dtCustomer)
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID, dtCustomer)
                    If objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId) Is Nothing Then
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = DBNull.Value
                    Else
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId)
                    End If
                Else
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, intDepotId, dtCustomer)
                End If

                'To Fill Depot Details
                dsDepot = objCommonUI.pub_GetDepoDetail(intDepotId)
                'TO Fill Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)
                'TO Fill Foreign Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotIdForForeignCurrency(intDepotId, dtForeignBankDetail)
                'TO get Exchange Rate
                If dtCustomer.Rows.Count > 0 Then
                    If Not IsDBNull(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC)) Then
                        decExchangeRate = CDec(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC))
                    Else
                        decExchangeRate = pvt_GetExchangeRate(dtCustomer, dtBankDetail)
                    End If
                Else
                    decExchangeRate = 0
                End If

                objInvoiceGeneration.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtCleaningCharge, _
                                                                     decExchangeRate, fromDate, toDate, decCustomerAmount, _
                                                                     decDepotAmount)



                'Data Table Merged with DataSet
                Dim dtValidCleaningInvoice As New DataTable
                dtValidCleaningInvoice = dtCleaningCharge.Clone
                If dtCleaningCharge.Select(String.Concat(InvoiceGenerationData.TTL_CSTS_NC, " IS NOT NULL AND ", InvoiceGenerationData.TTL_CSTS_NC, " >0 AND ", InvoiceGenerationData.TTL_CSTS_NC, " >0.00")).Length > 0 Then
                    dtValidCleaningInvoice = dtCleaningCharge.Select(String.Concat(InvoiceGenerationData.TTL_CSTS_NC, " IS NOT NULL AND ", InvoiceGenerationData.TTL_CSTS_NC, " >0 AND ", InvoiceGenerationData.TTL_CSTS_NC, " >0.00")).CopyToDataTable
                End If
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Merge(dtValidCleaningInvoice)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Merge(dtCustomer)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Merge(dtBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._FOREIGN_BANK_DETAIL).Merge(dtForeignBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._DEPOT).Merge(dsDepot.Tables(InvoiceGenerationData._DEPOT))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dtCustomer.Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))
                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dtCustomer.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                Dim drPendingInvoice As DataRow
                For Each drCleaning As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Rows
                    'To fill the dataset for pending invoice
                    drPendingInvoice = dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).NewRow()
                    For Each dc As DataColumn In dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).Columns
                        If dtCleaningCharge.Columns.Contains(dc.ColumnName) AndAlso Not IsDBNull(drCleaning.Item(dc.ColumnName)) Then
                            drPendingInvoice.Item(dc.ColumnName) = drCleaning.Item(dc.ColumnName)
                        End If
                    Next
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).Rows.Add(drPendingInvoice)
                Next

                If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Rows.Count > 0 Then
                    Dim dtInvoiceTable As New DataTable
                    dtInvoiceTable = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Copy
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Rows.Clear()
                    dtInvoiceTable = dtInvoiceTable.Select(String.Empty, String.Concat(InvoiceGenerationData.GTN_DT, " ASC")).CopyToDataTable
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Merge(dtInvoiceTable)
                End If

                If Not str_073_KeyValue.ToLower = "true" Then
                    'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE)
                    For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Rows
                        dr.Item(InvoiceGenerationData.CHECKED) = False
                    Next
                    '51063 - Fix
                    decCustomerAmount = CDec("0.00")
                    decDepotAmount = CDec("0.00")
                Else
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Rows.Count > 0 Then
                        e.OutputParameters.Add("WarningMsg", "Hide")
                    Else                        e.OutputParameters.Add("WarningMsg", "Show")
                    End If

                End If

                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Copy())
                e.DataSource = dv

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Merge(dt)

                e.OutputParameters.Add("Activity", "CLEANING")
                e.OutputParameters.Add("ExRate", CDec(decExchangeRate).ToString("0.0000"))
                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
                'CacheData("ifgCleaning_Check", "clientbind")
            End If

            'Cleaning Heder Operation
            If Not e.Parameters("CheckState") Is Nothing AndAlso Not str_073_KeyValue.ToLower = "true" Then
                dsInvoiceGeneration = RetrieveData(INVOICE_GENERATION)
                Dim blnCheckState As Boolean = e.Parameters("CheckState")

                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = blnCheckState
                Next
                'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE)
                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Copy())
                dv.RowFilter = strFilter
                e.DataSource = dv

                '51063 - Fix
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                Dim dtAmount As New DataTable
                dtAmount = dv.ToTable().Copy()
                If e.Parameters("CheckState") = True Then
                    decCustomerAmount = CDec(dtAmount.Compute("SUM(TTL_CSTS_NC)", "NOT TTL_CSTS_NC IS NULL"))
                    decDepotAmount = CDec(dtAmount.Compute("SUM(DPT_TTL_NC)", "NOT DPT_TTL_NC IS NULL"))
                Else
                    decCustomerAmount = CDec("0.00")
                    decDepotAmount = CDec("0.00")
                End If

                Dim dtBankDetail As New DataTable
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)

                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))

                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)

                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Merge(dt)
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
                CacheData("ifgCleaning_Check", e.Parameters("CheckState"))
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgCleaning_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgCleaning.RowDataBound
        Try
            Dim objCommon As New CommonData
            Dim bln_073KeyExist As New Boolean
            If e.Row.RowType = DataControlRowType.Header Then
                If strDepotCurrency = String.Empty OrElse strCustomerCurrency = String.Empty Then
                    dsInvoiceGeneration = CType(RetrieveData(INVOICE_GENERATION), InvoiceGenerationDataSet)
                    If dsInvoiceGeneration Is Nothing Then
                        Exit Sub
                    End If
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows.Count > 0 Then
                        strDepotCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows(0).Item(CommonUIData.CRRNCY_CD))
                    End If
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows.Count > 0 Then
                        strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(CommonUIData.CSTMR_CRRNCY_CD))
                    End If
                End If
                CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strDepotCurrency)
                CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strCustomerCurrency)

                'Header Check Box
                Dim ifgCheck As New CheckBox
                ifgCheck.EnableViewState = True
                Dim blnCheck As Boolean = False
                If Not RetrieveData("ifgCleaning_Check") Is Nothing Then
                    blnCheck = CBool(RetrieveData("ifgCleaning_Check"))
                End If
                ifgCheck.Checked = False

                ifgCheck.Attributes.Add("onclick", "fnInvoiceHerderCheckBox('ifgCleaning',this);return false;")
                e.Row.Cells(9).Controls.Add(ifgCheck)
                If objCommon.GetConfigSetting("073", bln_073KeyExist).ToLower = "true" Then
                    e.Row.Cells(9).Visible = False
                End If
            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                Dim chk As iFgCheckBox
                chk = CType(e.Row.Cells(9).Controls(0), iFgCheckBox)
                chk.Attributes.Add("onclick", "fnCalcCleaningAmount(this);")
                If objCommon.GetConfigSetting("073", bln_073KeyExist).ToLower = "true" Then
                    e.Row.Cells(9).Visible = False
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgCleaning_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgCleaning.RowUpdated
        Try
            CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgCleaning_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgCleaning.RowUpdating
        Try
            Dim strFilter As String = String.Empty
            If Not ifgCleaning.DataSource Is Nothing AndAlso CType(ifgCleaning.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgCleaning.DataSource, DataView).RowFilter
            End If


            Dim dv As DataView = ifgCleaning.DataSource
            dv.RowFilter = strFilter

            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Rows.Clear()
            Dim dt As DataTable = dv.ToTable()
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Merge(dt)

            e.OldValues(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Rows(e.RowIndex).Item(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub


#End Region

#Region "ifgMiscellaneous_ClientBind"

    Protected Sub ifgMiscellaneous_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgMiscellaneous.ClientBind
        Try
            Dim objCommon As New CommonData
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim intDepotId As Integer = objCommon.GetDepotID()

            Dim strFilter As String = String.Empty

            If Not ifgMiscellaneous.DataSource Is Nothing AndAlso CType(ifgMiscellaneous.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgMiscellaneous.DataSource, DataView).RowFilter
            End If
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" AndAlso objCommon.GetOrganizationTypeCD = "HQ" Then
                '    intDepotId = CommonUIs.iInt(e.Parameters("DepotID"))
                'End If
                intDepotId = e.Parameters("DepotID")
            Else
                intDepotId = objCommon.GetDepotID()
            End If
            If Not e.Parameters("CustomerId") Is Nothing Then
                Dim dtMiscellaneousInvoice As New DataTable
                Dim dtStorage As New DataTable
                Dim dtCustomer As New DataTable
                Dim dtBankDetail As New DataTable
                Dim dtForeignBankDetail As New DataTable
                Dim objCommonUI As New CommonUI
                Dim dsDepot As DataSet
                Dim fromDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodFrom"))
                Dim toDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodTo"))
                Dim strCustomerType As String = e.Parameters("CustomerType")
                Dim i64CustomerID As Long = CommonUIs.iLng(e.Parameters("CustomerId"))
                Dim i32InvoiceTypeID As Long = CommonUIs.iInt(e.Parameters("InvoiceTypeID"))
                Dim decExchangeRate As Decimal
                Dim dtExchangeRate As New DataTable
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                'To fill Datatable Miscellaneous charge details
                dtMiscellaneousInvoice = objInvoiceGeneration.pub_GetMiscellaneousInvoiceeByInvoicingPartyID(i64CustomerID, fromDate, _
                                                                                                             toDate, intDepotId).Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE)
                'To fill Customer Details
                If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                    'objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID(), dtCustomer)
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID, dtCustomer)
                    If objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId) Is Nothing Then
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = DBNull.Value
                    Else
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId)
                    End If
                Else
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetDepotID(), dtCustomer)
                End If
                'To Fill Depot Details
                dsDepot = objCommonUI.pub_GetDepoDetail(intDepotId)
                'TO Fill Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)
                'TO Fill Foreign Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotIdForForeignCurrency(intDepotId, dtForeignBankDetail)
                'TO get Exchange Rate
                If dtCustomer.Rows.Count > 0 Then
                    If Not IsDBNull(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC)) Then
                        decExchangeRate = CDec(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC))
                    Else
                        decExchangeRate = pvt_GetExchangeRate(dtCustomer, dtBankDetail)
                    End If
                Else
                    decExchangeRate = 0
                End If

                objInvoiceGeneration.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtMiscellaneousInvoice, _
                                                                         decExchangeRate, fromDate, toDate, decCustomerAmount, _
                                                                         decDepotAmount)

                'Data Table Merged with DataSet
                Dim dtValidMisInvoice As New DataTable
                dtValidMisInvoice = dtMiscellaneousInvoice.Clone
                If dtMiscellaneousInvoice.Select(String.Concat(InvoiceGenerationData.AMNT_NC, " IS NOT NULL ")).Length > 0 Then
                    dtValidMisInvoice = dtMiscellaneousInvoice.Select(String.Concat(InvoiceGenerationData.AMNT_NC, " IS NOT NULL ")).CopyToDataTable
                End If
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Merge(dtValidMisInvoice)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Merge(dtCustomer)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Merge(dtBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._FOREIGN_BANK_DETAIL).Merge(dtForeignBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._DEPOT).Merge(dsDepot.Tables(InvoiceGenerationData._DEPOT))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dtCustomer.Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))
                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dtCustomer.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE)
                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = False
                Next
                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Copy())
                e.DataSource = dv

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Merge(dt)

                '51063 - Fix
                decCustomerAmount = CDec("0.00")
                decDepotAmount = CDec("0.00")

                e.OutputParameters.Add("Activity", "MISCELLANEOUS")
                e.OutputParameters.Add("ExRate", CDec(decExchangeRate).ToString("0.0000"))
                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If

            'Miscellaneous Heder Operation
            If Not e.Parameters("CheckState") Is Nothing Then
                dsInvoiceGeneration = RetrieveData(INVOICE_GENERATION)
                Dim blnCheckState As Boolean = e.Parameters("CheckState")

                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = blnCheckState
                Next
                'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE)
                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Copy())
                dv.RowFilter = strFilter
                e.DataSource = dv

                '51063 - Fix
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                Dim dtAmount As New DataTable
                dtAmount = dv.ToTable().Copy()
                If e.Parameters("CheckState") = True Then
                    decCustomerAmount = CDec(dtAmount.Compute("SUM(AMNT_NC)", "NOT AMNT_NC IS NULL"))
                    decDepotAmount = CDec(dtAmount.Compute("SUM(DPT_TTL_NC)", "NOT DPT_TTL_NC IS NULL"))
                Else
                    decCustomerAmount = CDec("0.00")
                    decDepotAmount = CDec("0.00")
                End If

                Dim dtBankDetail As New DataTable
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)

                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))

                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)

                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))


                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Merge(dt)
                CacheData("ifgMiscellaneous_Check", e.Parameters("CheckState"))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgMiscellaneous_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgMiscellaneous.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                If strDepotCurrency = String.Empty OrElse strCustomerCurrency = String.Empty Then
                    dsInvoiceGeneration = CType(RetrieveData(INVOICE_GENERATION), InvoiceGenerationDataSet)
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows.Count > 0 Then
                        strDepotCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows(0).Item(CommonUIData.CRRNCY_CD))
                    End If
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows.Count > 0 Then
                        strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(CommonUIData.CSTMR_CRRNCY_CD))
                    End If
                End If
                CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strDepotCurrency)
                CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strCustomerCurrency)

                'Header Check Box
                Dim ifgCheck As New CheckBox
                ifgCheck.EnableViewState = True
                'ifgCheck.Checked = True
                Dim blnCheck As Boolean = False
                If Not RetrieveData("ifgMiscellaneous_Check") Is Nothing Then
                    blnCheck = CBool(RetrieveData("ifgMiscellaneous_Check"))
                End If
                ifgCheck.Checked = False
                ifgCheck.Attributes.Add("onclick", "fnInvoiceHerderCheckBox('ifgMiscellaneous',this);return false;")
                e.Row.Cells(6).Controls.Add(ifgCheck)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                Dim chk As iFgCheckBox
                chk = CType(e.Row.Cells(6).Controls(0), iFgCheckBox)
                chk.Attributes.Add("onclick", "fnCalcMiscellaneousAmount(this);")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgMiscellaneous_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgMiscellaneous.RowUpdated
        Try
            CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgMiscellaneous_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgMiscellaneous.RowUpdating
        Try
            Dim strFilter As String = String.Empty
            If Not ifgMiscellaneous.DataSource Is Nothing AndAlso CType(ifgMiscellaneous.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgMiscellaneous.DataSource, DataView).RowFilter
            End If


            Dim dv As DataView = ifgMiscellaneous.DataSource
            dv.RowFilter = strFilter

            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows.Clear()
            Dim dt As DataTable = dv.ToTable()
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Merge(dt)

            e.OldValues(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows(e.RowIndex).Item(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub


#End Region


#Region "ifgCreditNote_ClientBind"

    Protected Sub ifgCreditNote_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgCreditNote.ClientBind
        Try
            Dim objCommon As New CommonData
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim intDepotId As Integer = objCommon.GetDepotID()
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" AndAlso objCommon.GetOrganizationTypeCD = "HQ" Then
                intDepotId = e.Parameters("DepotID")
            Else
                intDepotId = objCommon.GetDepotID()
            End If

            Dim strFilter As String = String.Empty

            If Not ifgCreditNote.DataSource Is Nothing AndAlso CType(ifgCreditNote.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgCreditNote.DataSource, DataView).RowFilter
            End If

            If Not e.Parameters("CustomerId") Is Nothing Then
                Dim dtMiscellaneousInvoice As New DataTable
                Dim dtStorage As New DataTable
                Dim dtCustomer As New DataTable
                Dim dtBankDetail As New DataTable
                Dim dtForeignBankDetail As New DataTable
                Dim objCommonUI As New CommonUI
                Dim dsDepot As DataSet
                Dim fromDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodFrom"))
                Dim toDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodTo"))
                Dim strCustomerType As String = e.Parameters("CustomerType")
                Dim i64CustomerID As Long = CommonUIs.iLng(e.Parameters("CustomerId"))
                Dim i32InvoiceTypeID As Long = CommonUIs.iInt(e.Parameters("InvoiceTypeID"))
                Dim decExchangeRate As Decimal
                Dim dtExchangeRate As New DataTable
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                'To fill Datatable Miscellaneous charge details
                dtMiscellaneousInvoice = objInvoiceGeneration.GetCreditByInvoicingPartyID(i64CustomerID, fromDate, _
                                                                                                             toDate, intDepotId).Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE)
                'To fill Customer Details
                If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                    '  objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID(), dtCustomer)
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID, dtCustomer)
                    If objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId) Is Nothing Then
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = DBNull.Value
                    Else
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId)
                    End If
                Else
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetDepotID(), dtCustomer)
                End If
                'To Fill Depot Details
                dsDepot = objCommonUI.pub_GetDepoDetail(intDepotId)
                'TO Fill Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)
                'TO Fill Foreign Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotIdForForeignCurrency(intDepotId, dtForeignBankDetail)
                'TO get Exchange Rate
                If dtCustomer.Rows.Count > 0 Then
                    If Not IsDBNull(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC)) Then
                        decExchangeRate = CDec(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC))
                    Else
                        decExchangeRate = pvt_GetExchangeRate(dtCustomer, dtBankDetail)
                    End If
                Else
                    decExchangeRate = 0
                End If

                objInvoiceGeneration.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtMiscellaneousInvoice, _
                                                                         decExchangeRate, fromDate, toDate, decCustomerAmount, _
                                                                         decDepotAmount)

                'Data Table Merged with DataSet
                Dim dtValidMisInvoice As New DataTable
                dtValidMisInvoice = dtMiscellaneousInvoice.Clone
                If dtMiscellaneousInvoice.Select(String.Concat(InvoiceGenerationData.AMNT_NC, " IS NOT NULL ")).Length > 0 Then
                    dtValidMisInvoice = dtMiscellaneousInvoice.Select(String.Concat(InvoiceGenerationData.AMNT_NC, " IS NOT NULL ")).CopyToDataTable
                End If
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Merge(dtValidMisInvoice)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Merge(dtCustomer)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Merge(dtBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._FOREIGN_BANK_DETAIL).Merge(dtForeignBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._DEPOT).Merge(dsDepot.Tables(InvoiceGenerationData._DEPOT))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dtCustomer.Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))
                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dtCustomer.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE)
                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = False
                Next
                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Copy())
                e.DataSource = dv

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Merge(dt)

                '51063 - Fix
                decCustomerAmount = CDec("0.00")
                decDepotAmount = CDec("0.00")

                e.OutputParameters.Add("Activity", "MISCELLANEOUS")
                e.OutputParameters.Add("ExRate", CDec(decExchangeRate).ToString("0.0000"))
                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If

            'Miscellaneous Heder Operation
            If Not e.Parameters("CheckState") Is Nothing Then
                dsInvoiceGeneration = RetrieveData(INVOICE_GENERATION)
                Dim blnCheckState As Boolean = e.Parameters("CheckState")

                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = blnCheckState
                Next
                'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE)
                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Copy())
                dv.RowFilter = strFilter
                e.DataSource = dv

                '51063 - Fix
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                Dim dtAmount As New DataTable
                dtAmount = dv.ToTable().Copy()
                If e.Parameters("CheckState") = True Then
                    decCustomerAmount = CDec(dtAmount.Compute("SUM(AMNT_NC)", "NOT AMNT_NC IS NULL"))
                    decDepotAmount = CDec(dtAmount.Compute("SUM(DPT_TTL_NC)", "NOT DPT_TTL_NC IS NULL"))
                Else
                    decCustomerAmount = CDec("0.00")
                    decDepotAmount = CDec("0.00")
                End If
                Dim dtBankDetail As New DataTable
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)

                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))

                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)

                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Merge(dt)
                CacheData("ifgCreditNote_Check", e.Parameters("CheckState"))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgCreditNote_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgCreditNote.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                If strDepotCurrency = String.Empty OrElse strCustomerCurrency = String.Empty Then
                    dsInvoiceGeneration = CType(RetrieveData(INVOICE_GENERATION), InvoiceGenerationDataSet)
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows.Count > 0 Then
                        strDepotCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows(0).Item(CommonUIData.CRRNCY_CD))
                    End If
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows.Count > 0 Then
                        strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(CommonUIData.CSTMR_CRRNCY_CD))
                    End If
                End If
                CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strDepotCurrency)
                CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strCustomerCurrency)

                'Header Check Box
                Dim ifgCheck As New CheckBox
                ifgCheck.EnableViewState = True
                'ifgCheck.Checked = True
                Dim blnCheck As Boolean = False
                If Not RetrieveData("ifgCreditNote_Check") Is Nothing Then
                    blnCheck = CBool(RetrieveData("ifgCreditNote_Check"))
                End If
                ifgCheck.Checked = False
                ifgCheck.Attributes.Add("onclick", "fnInvoiceHerderCheckBox('ifgCreditNote',this);return false;")
                e.Row.Cells(6).Controls.Add(ifgCheck)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                Dim chk As iFgCheckBox
                chk = CType(e.Row.Cells(6).Controls(0), iFgCheckBox)
                chk.Attributes.Add("onclick", "fnCalcCreditNoteAmount(this);")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgCreditNote_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgCreditNote.RowUpdated
        Try
            CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgCreditNote_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgCreditNote.RowUpdating
        Try
            Dim strFilter As String = String.Empty
            If Not ifgCreditNote.DataSource Is Nothing AndAlso CType(ifgCreditNote.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgCreditNote.DataSource, DataView).RowFilter
            End If


            Dim dv As DataView = ifgCreditNote.DataSource
            dv.RowFilter = strFilter

            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows.Clear()
            Dim dt As DataTable = dv.ToTable()
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Merge(dt)

            e.OldValues(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE).Rows(e.RowIndex).Item(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub


#End Region

#Region "ifgHeating_ClientBind"

    Protected Sub ifgHeating_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgHeating.ClientBind
        Try
            Dim objCommon As New CommonData
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim intDepotId As Integer = objCommon.GetDepotID()

            Dim strFilter As String = String.Empty

            If Not ifgHeating.DataSource Is Nothing AndAlso CType(ifgHeating.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgHeating.DataSource, DataView).RowFilter
            End If
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" AndAlso objCommon.GetOrganizationTypeCD = "HQ" Then
                intDepotId = CommonUIs.iInt(e.Parameters("DepotID"))
            End If
            If Not e.Parameters("CustomerId") Is Nothing Then
                Dim dtHeatingCharge As New DataTable
                Dim dtStorage As New DataTable
                Dim dtCustomer As New DataTable
                Dim dtBankDetail As New DataTable
                Dim dtForeignBankDetail As New DataTable
                Dim objCommonUI As New CommonUI
                Dim dsDepot As DataSet
                Dim fromDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodFrom"))
                Dim toDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodTo"))
                Dim strCustomerType As String = e.Parameters("CustomerType")
                Dim i64CustomerID As Long = CommonUIs.iLng(e.Parameters("CustomerId"))
                Dim decExchangeRate As Decimal
                Dim dtExchangeRate As New DataTable
                Dim i32InvoiceTypeID As Long = CommonUIs.iInt(e.Parameters("InvoiceTypeID"))
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                'To fill Datatable Miscellaneous charge details
                dtHeatingCharge = objInvoiceGeneration.pub_GetHeatingChargeByCustomerId(i64CustomerID, fromDate, _
                                                                                        toDate, intDepotId).Tables(InvoiceGenerationData._V_HEATING_CHARGE)
                'To fill Customer Details based on Multi Location Config if true, based on Head Quarter ID else Depot ID
                If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                    ' objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID(), dtCustomer)
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID, dtCustomer)
                    If objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId) Is Nothing Then
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = DBNull.Value
                    Else
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId)
                    End If
                Else
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetDepotID(), dtCustomer)
                End If

                'To Fill Depot Details
                dsDepot = objCommonUI.pub_GetDepoDetail(intDepotId)
                'TO Fill Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)
                'TO Fill Foreign Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotIdForForeignCurrency(intDepotId, dtForeignBankDetail)
                'TO get Exchange Rate
                If dtCustomer.Rows.Count > 0 Then
                    If Not IsDBNull(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC)) Then
                        decExchangeRate = CDec(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC))
                    Else
                        decExchangeRate = pvt_GetExchangeRate(dtCustomer, dtBankDetail)
                    End If
                Else
                    decExchangeRate = 0
                End If

                objInvoiceGeneration.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtHeatingCharge, _
                                                                         decExchangeRate, fromDate, toDate, decCustomerAmount, _
                                                                         decDepotAmount)

                'Data Table Merged with DataSet
                Dim dtNewHeatingCharge As New DataTable
                dtNewHeatingCharge = dtHeatingCharge.Clone
                If dtHeatingCharge.Select(String.Concat(InvoiceGenerationData.TTL_RT_NC, " IS NOT NULL AND ", InvoiceGenerationData.TTL_RT_NC, " >0 AND ", InvoiceGenerationData.TTL_RT_NC, " >0.00")).Length > 0 Then
                    dtNewHeatingCharge = dtHeatingCharge.Select(String.Concat(InvoiceGenerationData.TTL_RT_NC, " IS NOT NULL AND ", InvoiceGenerationData.TTL_RT_NC, " >0 AND ", InvoiceGenerationData.TTL_RT_NC, " >0.00")).CopyToDataTable
                End If
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Merge(dtNewHeatingCharge)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Merge(dtCustomer)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Merge(dtBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._FOREIGN_BANK_DETAIL).Merge(dtForeignBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._DEPOT).Merge(dsDepot.Tables(InvoiceGenerationData._DEPOT))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dtCustomer.Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))
                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dtCustomer.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Rows.Count > 0 Then
                    Dim dtInvoiceTable As New DataTable
                    dtInvoiceTable = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Copy
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Rows.Clear()
                    dtInvoiceTable = dtInvoiceTable.Select(String.Empty, String.Concat(InvoiceGenerationData.GTN_DT, " ASC")).CopyToDataTable
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Merge(dtInvoiceTable)
                End If

                'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE)

                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = False
                Next
                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Copy())
                e.DataSource = dv

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()

                '51063 - Fix
                decCustomerAmount = CDec("0.00")
                decDepotAmount = CDec("0.00")

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Merge(dt)

                e.OutputParameters.Add("Activity", "HEATING")
                e.OutputParameters.Add("ExRate", CDec(decExchangeRate).ToString("0.0000"))
                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If


            'Heating Heder Operation
            If Not e.Parameters("CheckState") Is Nothing Then
                dsInvoiceGeneration = RetrieveData(INVOICE_GENERATION)
                Dim blnCheckState As Boolean = e.Parameters("CheckState")

                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = blnCheckState
                Next
                'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE)
                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Copy())
                dv.RowFilter = strFilter
                e.DataSource = dv

                '51063 - Fix
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                Dim dtAmount As New DataTable
                dtAmount = dv.ToTable().Copy()
                If e.Parameters("CheckState") = True Then
                    decCustomerAmount = CDec(dtAmount.Compute("SUM(TTL_RT_NC)", "NOT TTL_RT_NC IS NULL"))
                    decDepotAmount = CDec(dtAmount.Compute("SUM(DPT_TTL_NC)", "NOT DPT_TTL_NC IS NULL"))
                Else
                    decCustomerAmount = CDec("0.00")
                    decDepotAmount = CDec("0.00")
                End If

                Dim dtBankDetail As New DataTable
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)

                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))

                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)

                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Merge(dt)
                CacheData("ifgHeating_Check", e.Parameters("CheckState"))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgHeating_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgHeating.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                If strDepotCurrency = String.Empty OrElse strCustomerCurrency = String.Empty Then
                    dsInvoiceGeneration = CType(RetrieveData(INVOICE_GENERATION), InvoiceGenerationDataSet)
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows.Count > 0 Then
                        strDepotCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows(0).Item(CommonUIData.CRRNCY_CD))
                    End If
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows.Count > 0 Then
                        strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(CommonUIData.CSTMR_CRRNCY_CD))
                    End If
                End If
                CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strDepotCurrency)
                CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strCustomerCurrency)

                'Header Check Box
                Dim ifgCheck As New CheckBox
                ifgCheck.EnableViewState = True
                'ifgCheck.Checked = True
                Dim blnCheck As Boolean = False
                If Not RetrieveData("ifgHeating_Check") Is Nothing Then
                    blnCheck = CBool(RetrieveData("ifgHeating_Check"))
                End If
                ifgCheck.Checked = False

                ifgCheck.Attributes.Add("onclick", "fnInvoiceHerderCheckBox('ifgHeating',this);return false;")
                e.Row.Cells(10).Controls.Add(ifgCheck)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                Dim chk As iFgCheckBox
                chk = CType(e.Row.Cells(10).Controls(0), iFgCheckBox)
                chk.Attributes.Add("onclick", "fnCalcHeatingAmount(this);")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgHeating_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgHeating.RowUpdated
        Try
            CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgHeating_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgHeating.RowUpdating
        Try
            Dim strFilter As String = String.Empty
            If Not ifgHeating.DataSource Is Nothing AndAlso CType(ifgHeating.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgHeating.DataSource, DataView).RowFilter
            End If

            Dim dv As DataView = ifgHeating.DataSource
            dv.RowFilter = strFilter

            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Rows.Clear()
            Dim dt As DataTable = dv.ToTable()
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Merge(dt)

            e.OldValues(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_HEATING_CHARGE).Rows(e.RowIndex).Item(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgTransportation_ClientBind"

    Protected Sub ifgTransportation_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgTransportation.ClientBind
        Try
            Dim objCommon As New CommonData
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim intDepotId As Integer = objCommon.GetDepotID()
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" AndAlso objCommon.GetOrganizationTypeCD = "HQ" Then
                intDepotId = e.Parameters("DepotID")
            Else
                intDepotId = objCommon.GetDepotID()
            End If
            Dim strFilter As String = String.Empty
            If Not ifgTransportation.DataSource Is Nothing AndAlso CType(ifgTransportation.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgTransportation.DataSource, DataView).RowFilter
            End If

            If Not e.Parameters("CustomerId") Is Nothing Then
                Dim dtTransportationCharge As New DataTable
                Dim dtStorage As New DataTable
                Dim dtCustomer As New DataTable
                Dim dtBankDetail As New DataTable
                Dim dtForeignBankDetail As New DataTable
                Dim objCommonUI As New CommonUI
                Dim dsDepot As DataSet
                Dim fromDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodFrom"))
                Dim toDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodTo"))
                Dim strCustomerType As String = e.Parameters("CustomerType")
                Dim i64CustomerID As Long = CommonUIs.iLng(e.Parameters("CustomerId"))
                Dim decExchangeRate As Decimal
                Dim dtExchangeRate As New DataTable
                Dim i32InvoiceTypeID As Long = CommonUIs.iInt(e.Parameters("InvoiceTypeID"))
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                'To fill Datatable Transportation charge details
                dtTransportationCharge = objInvoiceGeneration.pub_GetTransportationChargeByCustomerId(i64CustomerID, fromDate, _
                                                                                                      toDate, intDepotId).Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE)
                'To fill Customer Details
                If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                    '  objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID, dtCustomer)
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID, dtCustomer)
                    If objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId) Is Nothing Then
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = DBNull.Value
                    Else
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId)
                    End If
                Else
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, intDepotId, dtCustomer)
                End If
                'To Fill Depot Details
                dsDepot = objCommonUI.pub_GetDepoDetail(intDepotId)
                'TO Fill Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)
                'TO Fill Foreign Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotIdForForeignCurrency(intDepotId, dtForeignBankDetail)
                'TO get Exchange Rate
                If dtCustomer.Rows.Count > 0 Then
                    If Not IsDBNull(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC)) Then
                        decExchangeRate = CDec(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC))
                    Else
                        decExchangeRate = pvt_GetExchangeRate(dtCustomer, dtBankDetail)
                    End If
                Else
                    decExchangeRate = 0
                End If
                objInvoiceGeneration.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtTransportationCharge, _
                                                                         decExchangeRate, fromDate, toDate, decCustomerAmount, _
                                                                         decDepotAmount)

                'Data Table Merged with DataSet
                Dim dtNewTransportationCharge As New DataTable
                dtNewTransportationCharge = dtTransportationCharge.Clone
                If dtTransportationCharge.Select(String.Concat(InvoiceGenerationData.DPT_AMNT, " IS NOT NULL AND ", InvoiceGenerationData.DPT_AMNT, " >0 AND ", InvoiceGenerationData.DPT_AMNT, " >0.00")).Length > 0 Then
                    dtNewTransportationCharge = dtTransportationCharge.Select(String.Concat(InvoiceGenerationData.DPT_AMNT, " IS NOT NULL AND ", InvoiceGenerationData.DPT_AMNT, " >0 AND ", InvoiceGenerationData.DPT_AMNT, " >0.00")).CopyToDataTable
                End If
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Merge(dtNewTransportationCharge)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Merge(dtCustomer)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Merge(dtBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._FOREIGN_BANK_DETAIL).Merge(dtForeignBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._DEPOT).Merge(dsDepot.Tables(InvoiceGenerationData._DEPOT))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dtCustomer.Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))
                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dtCustomer.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Rows.Count > 0 Then
                    Dim dtInvoiceTable As New DataTable
                    dtInvoiceTable = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Copy
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Rows.Clear()
                    dtInvoiceTable = dtInvoiceTable.Select(String.Empty, String.Concat(InvoiceGenerationData.JB_STRT_DT, " ASC")).CopyToDataTable
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Merge(dtInvoiceTable)
                End If

                'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE)
                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = False
                Next

                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Copy())
                e.DataSource = dv

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Merge(dt)

                '51063 - Fix
                decCustomerAmount = CDec("0.00")
                decDepotAmount = CDec("0.00")

                e.OutputParameters.Add("Activity", "TRANSPORTATION")
                e.OutputParameters.Add("ExRate", CDec(decExchangeRate).ToString("0.0000"))
                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If

            'Transportation Header Operation
            If Not e.Parameters("CheckState") Is Nothing Then
                dsInvoiceGeneration = RetrieveData(INVOICE_GENERATION)
                Dim blnCheckState As Boolean = e.Parameters("CheckState")

                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = blnCheckState
                Next
                'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE)
                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Copy())
                dv.RowFilter = strFilter
                e.DataSource = dv

                '51063 - Fix
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                Dim dtAmount As New DataTable
                dtAmount = dv.ToTable().Copy()
                If e.Parameters("CheckState") = True Then
                    decCustomerAmount = CDec(dtAmount.Compute("SUM(CSTMR_AMNT)", "NOT CSTMR_AMNT IS NULL"))
                    decDepotAmount = CDec(dtAmount.Compute("SUM(DPT_AMNT)", "NOT DPT_AMNT IS NULL"))
                Else
                    decCustomerAmount = CDec("0.00")
                    decDepotAmount = CDec("0.00")
                End If

                Dim dtBankDetail As New DataTable
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)

                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))

                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)

                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))


                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Merge(dt)

                CacheData("ifgTransportation_Check", e.Parameters("CheckState"))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgTransportation_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgTransportation.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                If strDepotCurrency = String.Empty OrElse strCustomerCurrency = String.Empty Then
                    dsInvoiceGeneration = CType(RetrieveData(INVOICE_GENERATION), InvoiceGenerationDataSet)
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows.Count > 0 Then
                        strDepotCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows(0).Item(CommonUIData.CRRNCY_CD))
                    End If
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows.Count > 0 Then
                        strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(CommonUIData.CSTMR_CRRNCY_CD))
                    End If
                End If
                CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Trip Rate (", strDepotCurrency, ")")
                CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Weighment (", strDepotCurrency, ")")
                CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Token (", strDepotCurrency, ")")
                CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Rechargeable Cost (", strDepotCurrency, ")")
                CType(e.Row.Cells(11), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Finance Charges (", strDepotCurrency, ")")
                CType(e.Row.Cells(12), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Detention (", strDepotCurrency, ")")
                CType(e.Row.Cells(13), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Other Charges (", strDepotCurrency, ")")
                CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount (", strDepotCurrency, ")")
                CType(e.Row.Cells(15), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount (", strCustomerCurrency, ")")


                'Header Check Box
                Dim ifgCheck As New CheckBox
                ifgCheck.EnableViewState = True
                'ifgCheck.Checked = True
                Dim blnCheck As Boolean = False
                If Not RetrieveData("ifgTransportation_Check") Is Nothing Then
                    blnCheck = CBool(RetrieveData("ifgTransportation_Check"))
                End If
                ifgCheck.Checked = False

                ifgCheck.Attributes.Add("onclick", "fnInvoiceHerderCheckBox('ifgTransportation',this);return false;")
                e.Row.Cells(16).Controls.Add(ifgCheck)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                Dim chk As iFgCheckBox
                chk = CType(e.Row.Cells(16).Controls(0), iFgCheckBox)
                chk.Attributes.Add("onclick", "fnCalcTransportationAmount(this);")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgTransportation_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgTransportation.RowUpdated
        Try
            CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgTransportation_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgTransportation.RowUpdating
        Try
            Dim strFilter As String = String.Empty
            If Not ifgTransportation.DataSource Is Nothing AndAlso CType(ifgTransportation.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgTransportation.DataSource, DataView).RowFilter
            End If


            Dim dv As DataView = ifgTransportation.DataSource
            dv.RowFilter = strFilter

            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Rows.Clear()
            Dim dt As DataTable = dv.ToTable()
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Merge(dt)

            e.OldValues(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_TRANSPORTATION_INVOICE).Rows(e.RowIndex).Item(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgRental_ClientBind"
    Protected Sub ifgRental_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgRental.ClientBind
        Try
            Dim objCommon As New CommonData
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim intDepotId As Integer = objCommon.GetDepotID()
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" AndAlso objCommon.GetOrganizationTypeCD = "HQ" Then
                intDepotId = CommonUIs.iInt(e.Parameters("DepotID"))
            End If
            If Not e.Parameters("CustomerId") Is Nothing Then
                Dim dtRental As New DataTable
                Dim dtStorage As New DataTable
                Dim dtCustomer As New DataTable
                Dim dtBankDetail As New DataTable
                Dim dtForeignBankDetail As New DataTable
                Dim objCommonUI As New CommonUI
                Dim dsDepot As DataSet
                Dim fromDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodFrom"))
                Dim toDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodTo"))
                Dim strCustomerType As String = e.Parameters("CustomerType")
                Dim i64CustomerID As Long = CommonUIs.iLng(e.Parameters("CustomerId"))
                Dim i32InvoiceTypeID As Long = CommonUIs.iInt(e.Parameters("InvoiceTypeID"))
                Dim decExchangeRate As Decimal
                Dim dtExchangeRate As New DataTable

                'To fill Datatable from Rental Charge
                dtRental = objInvoiceGeneration.pub_GetRentalChargeByCustomerId(i64CustomerID, fromDate, toDate, intDepotId).Tables(InvoiceGenerationData._V_RENTAL_CHARGE)
                'To fill Customer Details
                If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                    '  objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID(), dtCustomer)
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetHeadQuarterID, dtCustomer)
                    If objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId) Is Nothing Then
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = DBNull.Value
                    Else
                        dtCustomer.Rows(0).Item("EXCHNG_RT_PR_UNT_NC") = objInvoiceGeneration.pub_GetExchangeRateForMultilocation(dtCustomer.Rows(0).Item("CRRNCY_ID"), intDepotId)
                    End If
                Else
                    objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, objCommon.GetDepotID(), dtCustomer)
                End If
                'To Fill Depot Details
                dsDepot = objCommonUI.pub_GetDepoDetail(intDepotId)
                'TO Fill Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)
                'TO Fill Foreign Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotIdForForeignCurrency(intDepotId, dtForeignBankDetail)
                'TO get Exchange Rate
                If dtCustomer.Rows.Count > 0 Then
                    If Not IsDBNull(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC)) Then
                        decExchangeRate = CDec(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC))
                    Else
                        decExchangeRate = pvt_GetExchangeRate(dtCustomer, dtBankDetail)
                    End If
                Else
                    decExchangeRate = 0
                End If

                objInvoiceGeneration.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtRental, _
                                                                         decExchangeRate, fromDate, toDate, 0, 0, i64CustomerID, CType(dsInvoiceGeneration, DataSet), _
                                                                         intDepotId)

                'Data Table Merged with DataSet
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_RENTAL_CHARGE).Merge(dtRental)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Merge(dtCustomer)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Merge(dtBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._FOREIGN_BANK_DETAIL).Merge(dtForeignBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._DEPOT).Merge(dsDepot.Tables(InvoiceGenerationData._DEPOT))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dtCustomer.Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))
                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dtCustomer.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                dsInvoiceGeneration = CType(dsInvoiceGeneration, InvoiceGenerationDataSet)

                Dim drPendingInvoice As DataRow
                For Each drRental As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_RENTAL_CHARGE).Rows
                    'To fill the dataset for pending invoice
                    drPendingInvoice = dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).NewRow()
                    For Each dc As DataColumn In dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).Columns
                        If dtRental.Columns.Contains(dc.ColumnName) AndAlso Not IsDBNull(drRental.Item(dc.ColumnName)) Then
                            drPendingInvoice.Item(dc.ColumnName) = drRental.Item(dc.ColumnName)
                        End If
                    Next
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).Rows.Add(drPendingInvoice)
                Next

                Dim decTotalCustomerAmount As Decimal
                Dim decTotalDepotAmount As Decimal
                For Each drRentalInvoice As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_RENTAL_CHARGE).Rows
                    decTotalCustomerAmount = decTotalCustomerAmount + CDec(drRentalInvoice.Item(InvoiceGenerationData.TTL_CSTS_NC))
                    decTotalDepotAmount = decTotalDepotAmount + CDec(drRentalInvoice.Item(InvoiceGenerationData.DPT_TTL_NC))
                Next

                e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_RENTAL_CHARGE)
                e.OutputParameters.Add("Activity", "RENTAL")
                e.OutputParameters.Add("ExRate", CDec(decExchangeRate).ToString("0.0000"))
                e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)
                e.OutputParameters.Add("CustAmount", decTotalDepotAmount.ToString("0.00"))
                e.OutputParameters.Add("DepotAmount", decTotalCustomerAmount.ToString("0.00"))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgRental_RowDataBound"
    Protected Sub ifgRental_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgRental.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                If strDepotCurrency = String.Empty OrElse strCustomerCurrency = String.Empty Then
                    dsInvoiceGeneration = CType(RetrieveData(INVOICE_GENERATION), InvoiceGenerationDataSet)
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows.Count > 0 Then
                        strDepotCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows(0).Item(CommonUIData.CRRNCY_CD))
                    End If
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows.Count > 0 Then
                        strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(CommonUIData.CSTMR_CRRNCY_CD))
                    End If
                End If
                CType(e.Row.Cells(11), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("On-Hire Survey in ", strDepotCurrency)
                CType(e.Row.Cells(12), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("On-Hire HNDOUT in ", strDepotCurrency)
                CType(e.Row.Cells(13), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Off-Hire Survey in ", strDepotCurrency)
                CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Off-Hire HNDIN in ", strDepotCurrency)
                CType(e.Row.Cells(15), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Other Charges in ", strDepotCurrency)
                CType(e.Row.Cells(16), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Total in ", strDepotCurrency)
                CType(e.Row.Cells(17), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Total in ", strCustomerCurrency)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgInspection_ClientBind"

    Protected Sub ifgInspection_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgInspection.ClientBind
        Try
            Dim objCommon As New CommonData
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim intDepotId As Integer = objCommon.GetDepotID()
            Dim strFilter As String = String.Empty

            If Not ifgCleaning.DataSource Is Nothing AndAlso CType(ifgInspection.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgInspection.DataSource, DataView).RowFilter
            End If

            If Not e.Parameters("CustomerId") Is Nothing Then
                Dim dtCleaningCharge As New DataTable
                Dim dtStorage As New DataTable
                Dim dtCustomer As New DataTable
                Dim dtBankDetail As New DataTable
                Dim dtForeignBankDetail As New DataTable
                Dim objCommonUI As New CommonUI
                Dim dsDepot As DataSet
                Dim fromDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodFrom"))
                Dim toDate As DateTime = CommonUIs.iDat(e.Parameters("PeriodTo"))
                Dim strCustomerType As String = e.Parameters("CustomerType")
                Dim i64CustomerID As Long = CommonUIs.iLng(e.Parameters("CustomerId"))
                Dim i32InvoiceTypeID As Long = CommonUIs.iInt(e.Parameters("InvoiceTypeID"))
                Dim decExchangeRate As Decimal
                Dim dtExchangeRate As New DataTable
                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                'To fill Datatable Cleaning charge details
                dtCleaningCharge = objInvoiceGeneration.pub_GetInspectionChargeByCustomerId(i64CustomerID, fromDate, _
                                                                                          toDate, intDepotId).Tables(InvoiceGenerationData._V_INSPECTION_CHARGES)

                'To fill Customer Details
                objInvoiceGeneration.pub_GetCustomerByDepotIdCustomerID(i64CustomerID, strCustomerType, intDepotId, dtCustomer)
                'To Fill Depot Details
                dsDepot = objCommonUI.pub_GetDepoDetail(intDepotId)
                'TO Fill Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)
                'TO Fill Foreign Bank Detail
                objInvoiceGeneration.pub_GetBankDetailByDepotIdForForeignCurrency(intDepotId, dtForeignBankDetail)
                'TO get Exchange Rate
                If dtCustomer.Rows.Count > 0 AndAlso Not IsDBNull(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC)) Then
                    decExchangeRate = CDec(dtCustomer.Rows(0).Item(InvoiceGenerationData.EXCHNG_RT_PR_UNT_NC))
                Else
                    decExchangeRate = 0
                End If
                objInvoiceGeneration.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtCleaningCharge, _
                                                                         decExchangeRate, fromDate, toDate, decCustomerAmount, _
                                                                         decDepotAmount)

                'Data Table Merged with DataSet
                Dim dtValidCleaningInvoice As New DataTable
                dtValidCleaningInvoice = dtCleaningCharge.Clone
                If dtCleaningCharge.Select(String.Concat(InvoiceGenerationData.TTL_CSTS_NC, " IS NOT NULL AND ", InvoiceGenerationData.TTL_CSTS_NC, " >0 AND ", InvoiceGenerationData.TTL_CSTS_NC, " >0.00")).Length > 0 Then
                    dtValidCleaningInvoice = dtCleaningCharge.Select(String.Concat(InvoiceGenerationData.TTL_CSTS_NC, " IS NOT NULL AND ", InvoiceGenerationData.TTL_CSTS_NC, " >0 AND ", InvoiceGenerationData.TTL_CSTS_NC, " >0.00")).CopyToDataTable
                End If
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Merge(dtValidCleaningInvoice)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Merge(dtCustomer)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Merge(dtBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._FOREIGN_BANK_DETAIL).Merge(dtForeignBankDetail)
                dsInvoiceGeneration.Tables(InvoiceGenerationData._DEPOT).Merge(dsDepot.Tables(InvoiceGenerationData._DEPOT))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dtCustomer.Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))
                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dtCustomer.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                Dim drPendingInvoice As DataRow
                For Each drCleaning As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Rows
                    'To fill the dataset for pending invoice
                    drPendingInvoice = dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).NewRow()
                    For Each dc As DataColumn In dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).Columns
                        If dtCleaningCharge.Columns.Contains(dc.ColumnName) AndAlso Not IsDBNull(drCleaning.Item(dc.ColumnName)) Then
                            drPendingInvoice.Item(dc.ColumnName) = drCleaning.Item(dc.ColumnName)
                        End If
                    Next
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._PENDING_INVOICE).Rows.Add(drPendingInvoice)
                Next

                If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Rows.Count > 0 Then
                    Dim dtInvoiceTable As New DataTable
                    dtInvoiceTable = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Copy
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Rows.Clear()
                    dtInvoiceTable = dtInvoiceTable.Select(String.Empty, String.Concat(InvoiceGenerationData.GTN_DT, " ASC")).CopyToDataTable
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Merge(dtInvoiceTable)
                End If

                'e.DataSource = dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE)
                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = False
                Next
                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Copy())
                e.DataSource = dv

                '51063 - Fix
                decCustomerAmount = CDec("0.00")
                decDepotAmount = CDec("0.00")

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Merge(dt)

                e.OutputParameters.Add("Activity", "INSPECTION")
                e.OutputParameters.Add("ExRate", CDec(decExchangeRate).ToString("0.0000"))
                'e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                'e.OutputParameters.Add("DepotCurrency", strDepotCurrency)
                'e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                'e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)

                'e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                'e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))
                'e.OutputParameters.Add("CustAmount", CDec(decDepotAmount.ToString("0.00")))
                'e.OutputParameters.Add("DepotAmount", CDec(decCustomerAmount.ToString("0.00")))

                e.OutputParameters.Add("CustCurrency", strDepotCurrency)
                e.OutputParameters.Add("DepotCurrency", strCustomerCurrency)
                e.OutputParameters.Add("CustCurrencyID", i32DepotCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32CustomerCurrencyID)

                e.OutputParameters.Add("DepotAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("CustAmount", CDec(decDepotAmount.ToString("0.00")))
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
                'CacheData("ifgCleaning_Check", "clientbind")
            End If

            'Inspection Heder Operation
            If Not e.Parameters("CheckState") Is Nothing Then
                dsInvoiceGeneration = RetrieveData(INVOICE_GENERATION)
                Dim blnCheckState As Boolean = e.Parameters("CheckState")

                For Each dr As DataRow In dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Rows
                    dr.Item(InvoiceGenerationData.CHECKED) = blnCheckState
                Next
                Dim dv As New DataView(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Copy())
                dv.RowFilter = strFilter
                e.DataSource = dv

                Dim decCustomerAmount As Decimal = CDec("0.00")
                Dim decDepotAmount As Decimal = CDec("0.00")

                Dim dtAmount As New DataTable
                dtAmount = dv.ToTable().Copy()
                If e.Parameters("CheckState") = True Then
                    decCustomerAmount = CDec(dtAmount.Compute("SUM(TTL_CSTS_NC)", "NOT TTL_CSTS_NC IS NULL"))
                    decDepotAmount = CDec(dtAmount.Compute("SUM(DPT_TTL_NC)", "NOT DPT_TTL_NC IS NULL"))
                Else
                    decCustomerAmount = CDec("0.00")
                    decDepotAmount = CDec("0.00")
                End If

                Dim dtBankDetail As New DataTable
                objInvoiceGeneration.pub_GetBankDetailByDepotId(intDepotId, dtBankDetail)

                i32DepotCurrencyID = CInt(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))
                i32CustomerCurrencyID = CInt(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CRRNCY_ID))

                strDepotCurrency = CStr(dtBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_CD))
                strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(InvoiceGenerationData.CSTMR_CRRNCY_CD))

                'e.OutputParameters.Add("CustCurrencyID", i32CustomerCurrencyID)
                'e.OutputParameters.Add("DepotCurrencyID", i32DepotCurrencyID)

                'e.OutputParameters.Add("CustCurrency", strCustomerCurrency)
                'e.OutputParameters.Add("DepotCurrency", strDepotCurrency)

                'e.OutputParameters.Add("CustAmount", CDec(decCustomerAmount.ToString("0.00")))
                'e.OutputParameters.Add("DepotAmount", CDec(decDepotAmount.ToString("0.00")))

                'e.OutputParameters.Add("CustAmount", CDec(decDepotAmount.ToString("0.00")))
                'e.OutputParameters.Add("DepotAmount", CDec(decCustomerAmount.ToString("0.00")))

                e.OutputParameters.Add("CustCurrencyID", i32DepotCurrencyID)
                e.OutputParameters.Add("DepotCurrencyID", i32CustomerCurrencyID)

                e.OutputParameters.Add("CustCurrency", strDepotCurrency)
                e.OutputParameters.Add("DepotCurrency", strCustomerCurrency)

                e.OutputParameters.Add("DepotAmount", CDec(decCustomerAmount.ToString("0.00")))
                e.OutputParameters.Add("CustAmount", CDec(decDepotAmount.ToString("0.00")))

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Rows.Clear()
                Dim dt As DataTable = dv.ToTable()

                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Merge(dt)
                CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
                CacheData("ifgInspection_Check", e.Parameters("CheckState"))
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgInspection_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgInspection.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                If strDepotCurrency = String.Empty OrElse strCustomerCurrency = String.Empty Then
                    dsInvoiceGeneration = CType(RetrieveData(INVOICE_GENERATION), InvoiceGenerationDataSet)
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows.Count > 0 Then
                        strDepotCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_BANK_DETAIL).Rows(0).Item(CommonUIData.CRRNCY_CD))
                    End If
                    If dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows.Count > 0 Then
                        strCustomerCurrency = CStr(dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CUSTOMER).Rows(0).Item(CommonUIData.CSTMR_CRRNCY_CD))
                    End If
                End If
                CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strDepotCurrency)
                CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Amount in ", strCustomerCurrency)

                'Header Check Box
                Dim ifgCheck As New CheckBox
                ifgCheck.EnableViewState = True
                Dim blnCheck As Boolean = False
                If Not RetrieveData("ifgInspection_Check") Is Nothing Then
                    blnCheck = CBool(RetrieveData("ifgInspection_Check"))
                End If
                ifgCheck.Checked = False

                ifgCheck.Attributes.Add("onclick", "fnInvoiceHerderCheckBox('ifgInspection',this);return false;")
                e.Row.Cells(7).Controls.Add(ifgCheck)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                Dim chk As iFgCheckBox
                chk = CType(e.Row.Cells(7).Controls(0), iFgCheckBox)
                chk.Attributes.Add("onclick", "fnCalcInspectionAmount(this);")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgInspection_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgInspection.RowUpdated
        Try
            CacheData(INVOICE_GENERATION, dsInvoiceGeneration)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgInspection_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgInspection.RowUpdating
        Try
            Dim strFilter As String = String.Empty
            If Not ifgInspection.DataSource Is Nothing AndAlso CType(ifgInspection.DataSource, DataView).RowFilter <> String.Empty Then
                strFilter = CType(ifgInspection.DataSource, DataView).RowFilter
            End If


            Dim dv As DataView = ifgInspection.DataSource
            dv.RowFilter = strFilter

            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Rows.Clear()
            Dim dt As DataTable = dv.ToTable()
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Merge(dt)

            e.OldValues(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_INSPECTION_CHARGES).Rows(e.RowIndex).Item(InvoiceGenerationData.CHECKED) = e.NewValues(InvoiceGenerationData.CHECKED)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub


#End Region

#Region "pvt_GetExchangeRate"
    Private Function pvt_GetExchangeRate(ByVal dtCustomer As DataTable, ByVal dtDepotBankDetail As DataTable) As Decimal
        Try
            Dim objCommon As New CommonData
            Dim decExchangeRate As New Decimal
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim intDepotCurrencyID As Integer = dtDepotBankDetail.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID)
            Dim intCustomerCurrencyID As Integer = dtCustomer.Rows(0).Item(InvoiceGenerationData.CRRNCY_ID)
            If intDepotCurrencyID = intCustomerCurrencyID Then
                decExchangeRate = 1.0
            Else
                Dim dtExchange As New DataTable
                If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                    dtExchange = objInvoiceGeneration.pub_GetExchangeRateWithEffectivedate(intDepotCurrencyID, intCustomerCurrencyID, DateTime.Now(), objCommon.GetHeadQuarterID())
                Else
                    dtExchange = objInvoiceGeneration.pub_GetExchangeRateWithEffectivedate(intDepotCurrencyID, intCustomerCurrencyID, DateTime.Now(), objCommon.GetDepotID())
                End If
                If dtExchange.Rows.Count > 0 Then
                    decExchangeRate = dtExchange.Rows(0).Item(RepairEstimateData.EXCHNG_RT_PR_UNT_NC).ToString
                Else
                    decExchangeRate = 0
                End If
            End If
            Return decExchangeRate
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "pvt_GetFromDateForCleaning"
    ''' <summary>
    ''' Get From and To Date for Cleaning
    ''' </summary>
    ''' <param name="bv_i32InvoiceTypeID"></param>
    ''' <param name="bv_i32ServicePartnerID"></param>
    ''' <param name="bv_strCustomerType"></param>
    ''' <param name="bv_i32DepotID"></param>
    ''' <remarks></remarks>
    Private Sub pvt_GetFromDateForCleaning(ByVal bv_i32InvoiceTypeID As Int32, _
                                ByVal bv_i32ServicePartnerID As Int32, _
                                ByVal bv_strCustomerType As String, _
                                ByVal bv_i32DepotID As Int32)
        Try
            Dim objInvoiceGeneration As New InvoiceGeneration
            Dim objCommon As New CommonData
            ' Dim i32DepotID As Int32
            If objCommon.GetMultiLocationSupportConfig.ToLower = "false" Then
                bv_i32DepotID = objCommon.GetDepotID()
            Else
                If objCommon.GetOrganizationTypeCD = "BO" Then
                    bv_i32DepotID = objCommon.GetDepotID()
                End If
            End If
            Dim dtInvoiceHistory As New DataTable
            Dim i64CustomerID As Int64 = 0
            Dim i64InvoicingPartyID As Int64 = 0
            Dim strInvoiceType As String = String.Empty
            Dim datfromDate As New DateTime
            Dim datToDate As New DateTime
            Dim strBackDated As String = "False"
            If bv_strCustomerType = "CUSTOMER" Then
                i64CustomerID = bv_i32ServicePartnerID
            ElseIf bv_strCustomerType = "PARTY" Then
                i64InvoicingPartyID = bv_i32ServicePartnerID
            ElseIf bv_strCustomerType.ToUpper = "AGENT" Then
                i64InvoicingPartyID = bv_i32ServicePartnerID
            End If

            'Get the Invoice Type for the corresponding Invoice type ID
            pvtGetInvoiceType(bv_i32InvoiceTypeID, strInvoiceType)

            dtInvoiceHistory = objInvoiceGeneration.pub_GetVInvoiceHistoryByInvoiceTypeCustomerBillingFlag(bv_i32DepotID, "FINAL", _
                                                                                                           strInvoiceType, i64CustomerID, _
                                                                                                           i64InvoicingPartyID).Tables(InvoiceGenerationData._INVOICE_HISTORY)
            Dim strFromDate As String = String.Empty
            If dtInvoiceHistory.Rows.Count > 0 AndAlso Not IsDBNull(dtInvoiceHistory.Rows(0).Item(InvoiceGenerationData.TO_BLLNG_DT)) Then
                CacheData("BackDated", strBackDated)
                Dim datActivityFromDate As DateTime
                datfromDate = CDate(dtInvoiceHistory.Rows(0).Item(InvoiceGenerationData.TO_BLLNG_DT)).AddDays(1).ToString("dd-MMM-yyy")
                If bv_strCustomerType = "CUSTOMER" Then
                    objInvoiceGeneration.pub_GetActivityStatusByCustomer(bv_i32DepotID, i64CustomerID, strFromDate, "Inspection", " AND INSPCTN_DT IS NOT NULL AND (CLNNG_BLLNG_FLG IS NULL OR CLNNG_BLLNG_FLG <> 'B') ORDER BY INSPCTN_DT ASC")
                ElseIf bv_strCustomerType = "PARTY" Then
                    objInvoiceGeneration.pub_GetCleaningChargeStatusByInvoicingParty(bv_i32DepotID, i64InvoicingPartyID, strFromDate, "Inspection", " AND BLLNG_FLG<>'B' ORDER BY ORGNL_INSPCTD_DT ASC ")
                End If

                If strFromDate <> "" Then
                    datActivityFromDate = CDate(strFromDate)
                    If datActivityFromDate < CDate(dtInvoiceHistory.Rows(0).Item(InvoiceGenerationData.TO_BLLNG_DT)) Then
                        strBackDated = "True"
                        datfromDate = New DateTime(datActivityFromDate.Year, datActivityFromDate.Month, 1)
                        CacheData("BackDated", strBackDated)
                    Else
                        CacheData("BackDated", strBackDated)
                    End If
                End If
                If (New DateTime(datfromDate.Year, datfromDate.Month, 1) = (New DateTime(Now.Year, Now.Month, 1))) Then
                    datfromDate = (New DateTime(Now.Year, Now.Month - 1, 1).ToString("dd-MMM-yyy"))
                    pub_SetCallbackReturnValue("WarningMsg", "Invoice for current month can be generated only from the first day of next month")
                Else
                    pub_SetCallbackReturnValue("WarningMsg", "")
                End If
                pub_SetCallbackReturnValue("FROMDATE", datfromDate.ToString("dd-MMM-yyy"))
                datToDate = (New DateTime(Now.Year, Now.Month, 1).AddDays(-1))
                pub_SetCallbackReturnValue("TODATE", datToDate.ToString("dd-MMM-yyy"))
            Else
                CacheData("BackDated", strBackDated)
                If bv_strCustomerType = "CUSTOMER" Then
                    objInvoiceGeneration.pub_GetActivityStatusByCustomer(bv_i32DepotID, i64CustomerID, strFromDate, "Inspection", " AND INSPCTN_DT IS NOT NULL AND CLNNG_INVC_PRTY_ID IS NULL ORDER BY INSPCTN_DT ASC ")
                ElseIf bv_strCustomerType = "PARTY" Then
                    objInvoiceGeneration.pub_GetCleaningChargeStatusByInvoicingParty(bv_i32DepotID, i64InvoicingPartyID, strFromDate, "Inspection", " AND BLLNG_FLG<>'B' ORDER BY ORGNL_INSPCTD_DT ASC")
                End If

                If strFromDate <> "" Then
                    datfromDate = CDate(strFromDate).ToString("dd-MMM-yyy")
                    datfromDate = New DateTime(datfromDate.Year, datfromDate.Month, 1)
                    If (New DateTime(datfromDate.Year, datfromDate.Month, 1) = (New DateTime(Now.Year, Now.Month, 1))) Then
                        datfromDate = (New DateTime(Now.Year, Now.Month - 1, 1).ToString("dd-MMM-yyy"))
                        pub_SetCallbackReturnValue("WarningMsg", "Invoice for current month can be generated only from the first day of next month")
                    Else
                        pub_SetCallbackReturnValue("WarningMsg", "")
                    End If
                    pub_SetCallbackReturnValue("FROMDATE", datfromDate.ToString("dd-MMM-yyy"))
                    datToDate = (New DateTime(Now.Year, Now.Month, 1).AddDays(-1))
                    pub_SetCallbackReturnValue("TODATE", datToDate.ToString("dd-MMM-yyy"))
                Else
                    pub_SetCallbackReturnValue("FROMDATE", "")
                    pub_SetCallbackReturnValue("TODATE", "")
                End If
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class