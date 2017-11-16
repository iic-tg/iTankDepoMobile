

Partial Class Billing_ViewInvoice
    Inherits Pagebase

#Region "Declarations.."
    Dim dsViewInvoice As New ViewInvoiceDataSet
    Dim dtViewInvoice As DataTable
    Dim objViewInvoice As New ViewInvoice
    Dim objCommon As New CommonData
    Private Const VIEW_INVOICE As String = "VIEW_INVOICE"
    Dim str_064GWS As String
    Dim bln_064GWSActive_Key As Boolean
    Dim objCommonConfig As New ConfigSetting()
    Private Const VIEW_INVOICEROLE As String = "VIEW_INVOICEROLE"
#End Region

#Region "Page_Load1"

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                
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
                Case "FinalInvoice"
                    pvt_fnFinalInvoice(e.GetCallbackValue("InvoiceBin"), e.GetCallbackValue("DepotID"))
                Case "InvoiceCancel"
                    pvt_fnInvoiceCancel(e.GetCallbackValue("InvoiceBin"), e.GetCallbackValue("InvoiceNo"), e.GetCallbackValue("Remarks"), e.GetCallbackValue("DepotID"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_fnFinalInvoice"
    Private Sub pvt_fnFinalInvoice(ByVal bv_i64InvoiceBin As Int64, ByVal bv_intDepotID As String)
        Try
            Dim objViewInvoice As New ViewInvoice
            Dim objCommon As New CommonData
            dsViewInvoice = CType(RetrieveData(VIEW_INVOICE), ViewInvoiceDataSet)
            Dim strTable1PrimaryID As String = String.Empty
            Dim strTable2PrimaryID As String = String.Empty
            Dim strDraftInvoiceNo As String = String.Empty
            Dim strFinalInvoiceNo As String = String.Empty
            Dim i64CustomerID As Int64 = 0
            Dim i64InvoicingPartyID As Int64 = 0
            Dim strInvoiceType As String = String.Empty
            Dim i32DepotID As Int32 = CInt(objCommon.GetDepotID())
            Dim blnGenerateInvoiceNo As Boolean = False
            Dim i64CustomerCurrencyID As Int64
            Dim i64DepotCurrencyID As Int64
            Dim decExchangeRate As Decimal
            Dim decTotalCustomerAmount As Decimal = 0.0
            Dim decTotalDepotAmount As Decimal = 0.0
            Dim datFromDate As Date
            Dim datToDate As Date
            Dim i32InvoiceTypeID As Int32
            Dim dtInvoiceTable As New DataTable
            Dim strInvoiceFileName As String = String.Empty
            Dim i64ServicePartnerID As Int64
            Dim strCustomerType As String = String.Empty
            Dim dtForeignBankDetail As New DataTable
            Dim dtCustomer As New DataTable
            Dim dtBankDetail As New DataTable
            Dim dtDepot As New DataTable
            Dim objCommonUI As New CommonUI
            Dim strEquipmentNo As String = String.Empty
            Dim strGITransactionNo As String = String.Empty
            Dim blnNoRecords As Boolean = False
            Dim intCountEquipment As Integer = 0
            Dim blnXmlbit As Boolean = False
            Dim str_067InvoiceGenerationGWSBit As String = Nothing
            Dim objCommonConfig As New ConfigSetting()

            str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", i32DepotID)
            dtViewInvoice = New DataTable
            dtViewInvoice = dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).Select(String.Concat(ViewInvoiceData.INVC_BIN, "=", bv_i64InvoiceBin)).CopyToDataTable
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                i32DepotID = bv_intDepotID
            End If
            If dtViewInvoice.Rows.Count > 0 Then
                With dtViewInvoice.Rows(0)
                    If Not IsDBNull(.Item(ViewInvoiceData.CSTMR_ID)) Then
                        i64CustomerID = CLng(.Item(ViewInvoiceData.CSTMR_ID))
                        i64ServicePartnerID = CLng(.Item(ViewInvoiceData.CSTMR_ID))
                        blnXmlbit = CBool(.Item(CustomerData.XML_BT))
                        strCustomerType = "CUSTOMER"
                    End If
                    If Not IsDBNull(.Item(ViewInvoiceData.INVCNG_PRTY_ID)) Then
                        i64InvoicingPartyID = CLng(.Item(ViewInvoiceData.INVCNG_PRTY_ID))
                        i64ServicePartnerID = CLng(.Item(ViewInvoiceData.INVCNG_PRTY_ID))
                        If str_067InvoiceGenerationGWSBit Then
                            strCustomerType = "AGENT"
                        Else
                            strCustomerType = "PARTY"
                        End If
                    End If
                    strDraftInvoiceNo = CStr(.Item(ViewInvoiceData.INVC_NO))
                    i64CustomerCurrencyID = CLng(.Item(ViewInvoiceData.INVC_CRRNCY_ID))
                    i64DepotCurrencyID = CLng(.Item(ViewInvoiceData.INVC_CRRNCY_ID))
                    decExchangeRate = CDec(.Item(ViewInvoiceData.EXCHNG_RT_NC))
                    'decTotalCustomerAmount = CDec(.Item(ViewInvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC))
                    'decTotalDepotAmount = CDec(.Item(ViewInvoiceData.TTL_CST_IN_INVC_CRRNCY_NC))
                    datFromDate = CDate(.Item(ViewInvoiceData.FRM_BLLNG_DT))
                    datToDate = CDate(.Item(ViewInvoiceData.TO_BLLNG_DT))
                    strInvoiceType = CStr(.Item(ViewInvoiceData.INVC_TYP))
                End With
            End If

            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_HANDLING_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_HANDLING_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_STORAGE_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_STORAGE_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._HANDLING_STORAGE_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_HEATING_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_HEATING_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._HEATING_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_CLEANING_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_CLEANING_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._CLEANING_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_REPAIR_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_REPAIR_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._REPAIR_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_MISCELLANEOUS_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_MISCELLANEOUS_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._MISCELLANEOUS_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_CUSTOMER) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_CUSTOMER).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._FOREIGN_BANK_DETAIL) Then
                dsViewInvoice.Tables(ViewInvoiceData._FOREIGN_BANK_DETAIL).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_BANK_DETAIL) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_BANK_DETAIL).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._DEPOT) Then
                dsViewInvoice.Tables(ViewInvoiceData._DEPOT).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_TRANSPORTATION_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_TRANSPORTATION_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._TRANSPORTATION_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Clear()
            End If

            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_RENTAL_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_RENTAL_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._RENTAL_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Clear()
            End If

            If dsViewInvoice.Tables.Contains(ViewInvoiceData._INSPECTION_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_INSPECTION_CHARGES) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_INSPECTION_CHARGES).Clear()
            End If
            'Get the Invoice Type for the corresponding Invoice type ID
            pvtGetInvoiceType(i32InvoiceTypeID, strInvoiceType)

            Select Case i32InvoiceTypeID
                Case 78 'Handling & Storage

                   
                    dtInvoiceTable = New DataTable

                    If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then

                        If strCustomerType.ToUpper() = "AGENT" Then
                            dtInvoiceTable = objViewInvoice.pub_GetGWSHANDLINGCHARGEByByDepotIDInvoiceNoByAgent(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_HANDLING_CHARGE)
                        Else
                            dtInvoiceTable = objViewInvoice.pub_GetHANDLINGCHARGEByByDepotIDInvoiceNoByCustomer(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_HANDLING_CHARGE)
                        End If


                    Else

                        dtInvoiceTable = objViewInvoice.pub_GetHANDLINGCHARGEByByDepotIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_HANDLING_CHARGE)
                    End If


                    ''''pub_GetGWSHANDLINGCHARGEByByDepotIDInvoiceNo

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.HNDLNG_CHRG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.HNDLNG_CHRG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                    Next

                    dsViewInvoice.Tables(ViewInvoiceData._V_HANDLING_CHARGE).Merge(dtInvoiceTable)

                    dtInvoiceTable = New DataTable
                    'dtInvoiceTable = objViewInvoice.pub_GetSTORAGECHARGEByByDepotIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_STORAGE_CHARGE)

                    If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then

                        If strCustomerType.ToUpper() = "AGENT" Then
                            dtInvoiceTable = objViewInvoice.pub_Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByAgent(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_STORAGE_CHARGE)
                        Else
                            dtInvoiceTable = objViewInvoice.pub_Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByCustomer(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_STORAGE_CHARGE)
                        End If
                    Else
                        dtInvoiceTable = objViewInvoice.pub_GetSTORAGECHARGEByByDepotIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_STORAGE_CHARGE)
                    End If

                    '''''pub_Get_GWSSTORAGECHARGEByByDepotIDInvoiceNo

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable2PrimaryID <> String.Empty Then
                            strTable2PrimaryID = String.Concat(strTable2PrimaryID, ",", drSelect.Item(ViewInvoiceData.STRG_CHRG_ID))
                        Else
                            strTable2PrimaryID = drSelect.Item(ViewInvoiceData.STRG_CHRG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                    Next

                    dsViewInvoice.Tables(ViewInvoiceData._V_STORAGE_CHARGE).Merge(dtInvoiceTable)



                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate, _
                                                                       i64CustomerID, CType(dsViewInvoice, DataSet), _
                                                                       i32DepotID, str_067InvoiceGenerationGWSBit)

                    dsViewInvoice = CType(dsViewInvoice, ViewInvoiceDataSet)

                    If dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        Dim dtNewInvoiceTable As New DataTable
                        dtNewInvoiceTable = dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Copy
                        dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Rows.Clear()
                        dtNewInvoiceTable = dtNewInvoiceTable.Select(String.Empty, String.Concat(ViewInvoiceData.GTN_DT, " ASC")).CopyToDataTable
                        dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Merge(dtNewInvoiceTable)
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
                Case 79 'HEATING
                    dtInvoiceTable = objViewInvoice.pub_GetHEATINGCHARGEByByDepotIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_HEATING_CHARGE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.HTNG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.HTNG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                      decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        Dim dtNewInvoiceTable As New DataTable
                        dtNewInvoiceTable = dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Copy
                        dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Rows.Clear()
                        dtNewInvoiceTable = dtNewInvoiceTable.Select(String.Empty, String.Concat(ViewInvoiceData.GTN_DT, " ASC")).CopyToDataTable
                        dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Merge(dtNewInvoiceTable)
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.TTL_RT_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.TTL_RT_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
                Case 80 'CLEANING
                    dtInvoiceTable = objViewInvoice.pub_GetCLEANINGCHARGEByByDepotIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_CLEANING_CHARGE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.CLNNG_CHRG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.CLNNG_CHRG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                      decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        Dim dtNewInvoiceTable As New DataTable
                        dtNewInvoiceTable = dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Copy
                        dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Rows.Clear()
                        dtNewInvoiceTable = dtNewInvoiceTable.Select(String.Empty, String.Concat(ViewInvoiceData.GTN_DT, " ASC")).CopyToDataTable
                        dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Merge(dtNewInvoiceTable)
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
                Case 81 'REPAIR


                    'Dim str_067InvoiceGenerationGWSBit As String = Nothing
                    'Dim objCommonConfig As New ConfigSetting()

                    str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", i32DepotID)
                    dtInvoiceTable = New DataTable

                    If str_067InvoiceGenerationGWSBit <> Nothing AndAlso str_067InvoiceGenerationGWSBit.ToUpper() = "TRUE" Then

                        If strCustomerType.ToUpper() = "AGENT" Then
                            dtInvoiceTable = objViewInvoice.pub_Get_GWS_REPAIRCHARGEByByAgentIdInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_REPAIR_CHARGE)
                        Else
                            dtInvoiceTable = objViewInvoice.pub_Get_GWS_REPAIRCHARGEByByCustomerIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_REPAIR_CHARGE)
                        End If

                    Else
                        dtInvoiceTable = objViewInvoice.pub_GetREPAIRCHARGEByByDepotIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_REPAIR_CHARGE)
                    End If



                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.RPR_CHRG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.RPR_CHRG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        Dim dtNewInvoiceTable As New DataTable
                        dtNewInvoiceTable = dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Copy
                        dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Rows.Clear()
                        dtNewInvoiceTable = dtNewInvoiceTable.Select(String.Empty, String.Concat(ViewInvoiceData.GTN_DT, " ASC")).CopyToDataTable
                        dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Merge(dtNewInvoiceTable)
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
                Case 82 'MISCELLANEOUS
                    dtInvoiceTable = objViewInvoice.pub_GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_MISCELLANEOUS_INVOICE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.MSCLLNS_INVC_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.MSCLLNS_INVC_ID)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If

                Case 140 'Credit Note
                    dtInvoiceTable = objViewInvoice.pub_GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.MSCLLNS_INVC_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.MSCLLNS_INVC_ID)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Clear()
                    dsViewInvoice.Tables(InvoiceGenerationData._CREDIT_NOTE).Clear()

                    dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Merge(dtInvoiceTable)
                    dsViewInvoice.Tables(InvoiceGenerationData._CREDIT_NOTE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If

                Case 83 'Transportation
                    dtInvoiceTable = objViewInvoice.pub_GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_TRANSPORTATION_INVOICE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.TRNSPRTTN_CHRG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.TRNSPRTTN_CHRG_ID)
                        End If
                        If strTable2PrimaryID <> String.Empty Then
                            strTable2PrimaryID = String.Concat(strTable2PrimaryID, ",", drSelect.Item(ViewInvoiceData.TRNSPRTTN_ID))
                        Else
                            strTable2PrimaryID = drSelect.Item(ViewInvoiceData.TRNSPRTTN_ID)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        Dim dtNewInvoiceTable As New DataTable
                        dtNewInvoiceTable = dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Copy
                        dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Rows.Clear()
                        dtNewInvoiceTable = dtNewInvoiceTable.Select(String.Empty, String.Concat(ViewInvoiceData.JB_STRT_DT, " ASC")).CopyToDataTable
                        dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Merge(dtNewInvoiceTable)
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.DPT_AMNT & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.DPT_AMNT & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.CSTMR_AMNT & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.CSTMR_AMNT & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
                Case 84 'Rental
                    Dim strRentalRefNo As String = String.Empty
                    dtInvoiceTable = objViewInvoice.pub_GetRentalChargeByByDepotIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_RENTAL_CHARGE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(InvoiceGenerationData.RNTL_CHRG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(InvoiceGenerationData.RNTL_CHRG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                        If strRentalRefNo <> String.Empty Then
                            strRentalRefNo = String.Concat(strRentalRefNo, ",", drSelect.Item(ViewInvoiceData.RNTL_RFRNC_NO))
                        Else
                            strRentalRefNo = drSelect.Item(ViewInvoiceData.RNTL_RFRNC_NO)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
                    'GWS
                Case 151 'Inspection
                    dtInvoiceTable = objViewInvoice.pub_GetINSPECTIONCHARGEByByDepotIDInvoiceNo(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_INSPECTION_CHARGES)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.INSPCTN_CHRG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.INSPCTN_CHRG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                      decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        Dim dtNewInvoiceTable As New DataTable
                        dtNewInvoiceTable = dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Copy
                        dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Rows.Clear()
                        dtNewInvoiceTable = dtNewInvoiceTable.Select(String.Empty, String.Concat(ViewInvoiceData.GTN_DT, " ASC")).CopyToDataTable
                        dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Merge(dtNewInvoiceTable)
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
            End Select

            If blnNoRecords Then
                pub_SetCallbackReturnValue("NoRecords", "True")
                pub_SetCallbackStatus(False)
                blnNoRecords = False
                Exit Sub
            End If


            Dim blnFinance As Boolean = False
            Dim strFinance As String
            Dim intDepotid As Integer
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                intDepotid = objCommon.GetHeadQuarterID()
            Else
                intDepotid = objCommon.GetDepotID()
            End If
            Dim objConfig As New ConfigSetting(intDepotid)

            strFinance = objConfig.FinanceIntegration

            If Not String.IsNullOrEmpty(strFinance) AndAlso CBool(strFinance) = True Then
                blnFinance = True
            End If

            Dim strError_Msg As String = String.Empty
            If strTable1PrimaryID <> String.Empty OrElse strTable2PrimaryID <> String.Empty Then
                objViewInvoice.pub_UpdateInvoiceNoBillingFlag(strTable1PrimaryID, _
                                                              strTable2PrimaryID, "B", _
                                                              strFinalInvoiceNo, _
                                                              i64CustomerCurrencyID, _
                                                              i64DepotCurrencyID, _
                                                              decExchangeRate, _
                                                              datFromDate, _
                                                              datToDate, _
                                                              decTotalCustomerAmount, _
                                                              decTotalDepotAmount, _
                                                              i64CustomerID, _
                                                              i64InvoicingPartyID, _
                                                              i32DepotID, _
                                                              objCommon.GetCurrentUserName(), _
                                                              True, _
                                                              i32InvoiceTypeID, _
                                                              strInvoiceType, _
                                                              strDraftInvoiceNo, _
                                                              strInvoiceFileName, _
                                                              strGITransactionNo, _
                                                              strEquipmentNo, _
                                                              CType(dsViewInvoice, DataSet), _
                                                              intCountEquipment, _
                                                              blnFinance, _
                                                              dtViewInvoice, _
                                                              strError_Msg)

                Select Case i32InvoiceTypeID
                    Case 78 'Handling & Storage
                        For Each dr As DataRow In dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Rows
                            dr.Item(ViewInvoiceData.FNL_INVC_NO) = strFinalInvoiceNo
                            dr.Item(ViewInvoiceData.BLLNG_FLG) = "B"
                        Next
                    Case 79 'HEATING
                        For Each dr As DataRow In dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Rows
                            dr.Item(ViewInvoiceData.FNL_INVC_NO) = strFinalInvoiceNo
                            dr.Item(ViewInvoiceData.BLLNG_FLG) = "B"
                        Next
                    Case 80 'CLEANING
                        For Each dr As DataRow In dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Rows
                            dr.Item(ViewInvoiceData.FNL_INVC_NO) = strFinalInvoiceNo
                            dr.Item(ViewInvoiceData.BLLNG_FLG) = "B"
                        Next
                    Case 81 'REPAIR
                        For Each dr As DataRow In dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Rows
                            dr.Item(ViewInvoiceData.FNL_INVC_NO) = strFinalInvoiceNo
                            dr.Item(ViewInvoiceData.BLLNG_FLG) = "B"
                        Next
                    Case 82 'MISCELLANEOUS
                        For Each dr As DataRow In dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Rows
                            dr.Item(ViewInvoiceData.FNL_INVC_NO) = strFinalInvoiceNo
                            dr.Item(ViewInvoiceData.BLLNG_FLG) = "B"
                        Next
                    Case 140 'Credit Note
                        For Each dr As DataRow In dsViewInvoice.Tables(InvoiceGenerationData._CREDIT_NOTE).Rows
                            dr.Item(ViewInvoiceData.FNL_INVC_NO) = strFinalInvoiceNo
                            dr.Item(ViewInvoiceData.BLLNG_FLG) = "B"
                        Next
                    Case 83 'Transportation
                        For Each dr As DataRow In dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Rows
                            dr.Item(ViewInvoiceData.FNL_INVC_NO) = strFinalInvoiceNo
                            dr.Item(ViewInvoiceData.BLLNG_FLG) = "B"
                        Next
                    Case 84 'Rental
                        For Each dr As DataRow In dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Rows
                            dr.Item(ViewInvoiceData.FNL_INVC_NO) = strFinalInvoiceNo
                            dr.Item(ViewInvoiceData.BLLNG_FLG) = "B"
                        Next
                    Case 151 'INSPECTION
                        For Each dr As DataRow In dsViewInvoice.Tables(ViewInvoiceData._INSPECTION_INVOICE).Rows
                            dr.Item(ViewInvoiceData.FNL_INVC_NO) = strFinalInvoiceNo
                            dr.Item(ViewInvoiceData.BLLNG_FLG) = "B"
                        Next
                End Select

                'To fill Customer Details
                If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                    objViewInvoice.pub_GetCustomerByDepotIdCustomerID(i64ServicePartnerID, strCustomerType, objCommon.GetHeadQuarterID(), dtCustomer)
                Else
                    objViewInvoice.pub_GetCustomerByDepotIdCustomerID(i64ServicePartnerID, strCustomerType, objCommon.GetDepotID(), dtCustomer)
                End If
                'To Fill Depot Details
                dtDepot = objCommonUI.pub_GetDepoDetail(i32DepotID).Tables(ViewInvoiceData._DEPOT)
                'TO Fill Bank Detail
                objViewInvoice.pub_GetBankDetailByDepotId(i32DepotID, dtBankDetail)
                'TO Fill Foreign Bank Detail
                objViewInvoice.pub_GetBankDetailByDepotIdForForeignCurrency(i32DepotID, dtForeignBankDetail)

                'Data Table Merged with DataSet
                dsViewInvoice.Tables(ViewInvoiceData._V_CUSTOMER).Merge(dtCustomer)
                dsViewInvoice.Tables(ViewInvoiceData._V_BANK_DETAIL).Merge(dtBankDetail)
                dsViewInvoice.Tables(ViewInvoiceData._FOREIGN_BANK_DETAIL).Merge(dtForeignBankDetail)
                dsViewInvoice.Tables(ViewInvoiceData._DEPOT).Merge(dtDepot)
            End If
            If blnXmlbit And i32InvoiceTypeID = 80 Then
                generateInvoiceinXml(dsViewInvoice, dtViewInvoice, strFinalInvoiceNo, i32InvoiceTypeID, objCommon.GetCurrentUserName(), strInvoiceFileName)
            ElseIf blnXmlbit And i32InvoiceTypeID = 83 Then
                generateInvoiceinXml(dsViewInvoice, dtViewInvoice, strFinalInvoiceNo, i32InvoiceTypeID, objCommon.GetCurrentUserName(), strInvoiceFileName)
            End If

            dtViewInvoice = New DataTable
            dtViewInvoice = objViewInvoice.pub_GetVInvoiceHistoryByDepotID(i32DepotID).Tables(ViewInvoiceData._V_INVOICE_HISTORY)
            dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).Clear()
            dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).Merge(dtViewInvoice)

            CacheData(VIEW_INVOICE, dsViewInvoice)
            pub_SetCallbackReturnValue("DraftInvoiceNo", strDraftInvoiceNo)
            pub_SetCallbackReturnValue("FinalInvoiceNo", strFinalInvoiceNo)
            pub_SetCallbackReturnValue("InvoiceFileName", strInvoiceFileName)
            pub_SetCallbackReturnValue("InvoiceTypeID", i32InvoiceTypeID)


            If strError_Msg <> String.Empty Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackReturnValue("Error_Msg", strError_Msg)
            Else
                pub_SetCallbackStatus(True)
            End If

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Invoice Cancel"

    Private Sub pvt_fnInvoiceCancel(ByVal bv_i64InvoiceBin As Int64, ByVal bv_strInvoiceNo As String, ByVal bv_strRemarks As String, ByVal bv_intDepotID As String)
        Try
            Dim objViewInvoice As New ViewInvoice
            Dim objCommon As New CommonData
            dsViewInvoice = CType(RetrieveData(VIEW_INVOICE), ViewInvoiceDataSet)
            Dim strTable1PrimaryID As String = String.Empty
            Dim strTable2PrimaryID As String = String.Empty
            Dim strDraftInvoiceNo As String = String.Empty
            Dim strFinalInvoiceNo As String = String.Empty
            Dim i64CustomerID As Int64 = 0
            Dim i64InvoicingPartyID As Int64 = 0
            Dim strInvoiceType As String = String.Empty
            'Dim i32DepotID As Int32 = CInt(objCommon.GetDepotID())
            Dim i32DepotID As Int32 = bv_intDepotID
            Dim blnGenerateInvoiceNo As Boolean = False
            Dim i64CustomerCurrencyID As Int64
            Dim i64DepotCurrencyID As Int64
            Dim decExchangeRate As Decimal
            Dim decTotalCustomerAmount As Decimal = 0.0
            Dim decTotalDepotAmount As Decimal = 0.0
            Dim datFromDate As Date
            Dim datToDate As Date
            Dim i32InvoiceTypeID As Int32
            Dim dtInvoiceTable As New DataTable
            Dim strInvoiceFileName As String = String.Empty
            Dim i64ServicePartnerID As Int64
            Dim strCustomerType As String = String.Empty
            Dim dtForeignBankDetail As New DataTable
            Dim dtCustomer As New DataTable
            Dim dtBankDetail As New DataTable
            Dim dtDepot As New DataTable
            Dim objCommonUI As New CommonUI
            Dim strEquipmentNo As String = String.Empty
            Dim strGITransactionNo As String = String.Empty
            Dim blnNoRecords As Boolean = False
            Dim intCountEquipment As Integer = 0
            Dim blnXmlbit As Boolean = False

            dtViewInvoice = New DataTable
            dtViewInvoice = dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).Select(String.Concat(ViewInvoiceData.INVC_BIN, "=", bv_i64InvoiceBin)).CopyToDataTable

            If dtViewInvoice.Rows.Count > 0 Then
                With dtViewInvoice.Rows(0)
                    If Not IsDBNull(.Item(ViewInvoiceData.CSTMR_ID)) Then
                        i64CustomerID = CLng(.Item(ViewInvoiceData.CSTMR_ID))
                        i64ServicePartnerID = CLng(.Item(ViewInvoiceData.CSTMR_ID))
                        blnXmlbit = CBool(.Item(CustomerData.XML_BT))
                        strCustomerType = "CUSTOMER"
                    End If
                    If Not IsDBNull(.Item(ViewInvoiceData.INVCNG_PRTY_ID)) Then
                        i64InvoicingPartyID = CLng(.Item(ViewInvoiceData.INVCNG_PRTY_ID))
                        i64ServicePartnerID = CLng(.Item(ViewInvoiceData.INVCNG_PRTY_ID))
                        strCustomerType = "PARTY"
                    End If
                    strDraftInvoiceNo = CStr(.Item(ViewInvoiceData.INVC_NO))
                    i64CustomerCurrencyID = CLng(.Item(ViewInvoiceData.INVC_CRRNCY_ID))
                    i64DepotCurrencyID = CLng(.Item(ViewInvoiceData.INVC_CRRNCY_ID))
                    decExchangeRate = CDec(.Item(ViewInvoiceData.EXCHNG_RT_NC))
                    'decTotalCustomerAmount = CDec(.Item(ViewInvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC))
                    'decTotalDepotAmount = CDec(.Item(ViewInvoiceData.TTL_CST_IN_INVC_CRRNCY_NC))
                    datFromDate = CDate(.Item(ViewInvoiceData.FRM_BLLNG_DT))
                    datToDate = CDate(.Item(ViewInvoiceData.TO_BLLNG_DT))
                    strInvoiceType = CStr(.Item(ViewInvoiceData.INVC_TYP))
                End With
            End If

            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_HANDLING_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_HANDLING_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_STORAGE_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_STORAGE_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._HANDLING_STORAGE_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_HEATING_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_HEATING_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._HEATING_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_CLEANING_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_CLEANING_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._CLEANING_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_REPAIR_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_REPAIR_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._REPAIR_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_MISCELLANEOUS_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_MISCELLANEOUS_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._MISCELLANEOUS_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_CUSTOMER) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_CUSTOMER).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._FOREIGN_BANK_DETAIL) Then
                dsViewInvoice.Tables(ViewInvoiceData._FOREIGN_BANK_DETAIL).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_BANK_DETAIL) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_BANK_DETAIL).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._DEPOT) Then
                dsViewInvoice.Tables(ViewInvoiceData._DEPOT).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_TRANSPORTATION_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_TRANSPORTATION_INVOICE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._TRANSPORTATION_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Clear()
            End If

            If dsViewInvoice.Tables.Contains(ViewInvoiceData._V_RENTAL_CHARGE) Then
                dsViewInvoice.Tables(ViewInvoiceData._V_RENTAL_CHARGE).Clear()
            End If
            If dsViewInvoice.Tables.Contains(ViewInvoiceData._RENTAL_INVOICE) Then
                dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Clear()
            End If

            'Get the Invoice Type for the corresponding Invoice type ID
            pvtGetInvoiceType(i32InvoiceTypeID, strInvoiceType)

            Select Case i32InvoiceTypeID
                Case 78 'Handling & Storage
                    dtInvoiceTable = objViewInvoice.GetHANDLINGCHARGEByByDepotIDInvoiceNoCancel(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_HANDLING_CHARGE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.HNDLNG_CHRG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.HNDLNG_CHRG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                    Next

                    dsViewInvoice.Tables(ViewInvoiceData._V_HANDLING_CHARGE).Merge(dtInvoiceTable)

                    dtInvoiceTable = New DataTable
                    dtInvoiceTable = objViewInvoice.GetSTORAGECHARGEByByDepotIDInvoiceNoCancel(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_STORAGE_CHARGE)
                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable2PrimaryID <> String.Empty Then
                            strTable2PrimaryID = String.Concat(strTable2PrimaryID, ",", drSelect.Item(ViewInvoiceData.STRG_CHRG_ID))
                        Else
                            strTable2PrimaryID = drSelect.Item(ViewInvoiceData.STRG_CHRG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                    Next

                    For Each dr As DataRow In dtInvoiceTable.Rows
                        dr.Item(ViewInvoiceData.BLLNG_TLL_DT) = DBNull.Value
                    Next

                    dsViewInvoice.Tables(ViewInvoiceData._V_STORAGE_CHARGE).Merge(dtInvoiceTable)



                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate, _
                                                                       i64CustomerID, CType(dsViewInvoice, DataSet), _
                                                                       i32DepotID)

                    dsViewInvoice = CType(dsViewInvoice, ViewInvoiceDataSet)

                    If dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        Dim dtNewInvoiceTable As New DataTable
                        dtNewInvoiceTable = dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Copy
                        dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Rows.Clear()
                        dtNewInvoiceTable = dtNewInvoiceTable.Select(String.Empty, String.Concat(ViewInvoiceData.GTN_DT, " ASC")).CopyToDataTable
                        dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Merge(dtNewInvoiceTable)
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._HANDLING_STORAGE_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
                Case 79 'HEATING
                    dtInvoiceTable = objViewInvoice.GetHEATINGCHARGEByByDepotIDInvoiceNoCancel(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_HEATING_CHARGE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.HTNG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.HTNG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                      decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        Dim dtNewInvoiceTable As New DataTable
                        dtNewInvoiceTable = dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Copy
                        dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Rows.Clear()
                        dtNewInvoiceTable = dtNewInvoiceTable.Select(String.Empty, String.Concat(ViewInvoiceData.GTN_DT, " ASC")).CopyToDataTable
                        dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Merge(dtNewInvoiceTable)
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.TTL_RT_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("SUM(" & CommonUIData.TTL_RT_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._HEATING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
                Case 80 'CLEANING
                    dtInvoiceTable = objViewInvoice.GetCLEANINGCHARGEByByDepotIDInvoiceNoCancel(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_CLEANING_CHARGE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.CLNNG_CHRG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.CLNNG_CHRG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                      decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        Dim dtNewInvoiceTable As New DataTable
                        dtNewInvoiceTable = dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Copy
                        dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Rows.Clear()
                        dtNewInvoiceTable = dtNewInvoiceTable.Select(String.Empty, String.Concat(ViewInvoiceData.GTN_DT, " ASC")).CopyToDataTable
                        dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Merge(dtNewInvoiceTable)
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._CLEANING_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
                Case 81 'REPAIR
                    dtInvoiceTable = objViewInvoice.GetREPAIRCHARGEByByDepotIDInvoiceNoCancel(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_REPAIR_CHARGE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.RPR_CHRG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.RPR_CHRG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        Dim dtNewInvoiceTable As New DataTable
                        dtNewInvoiceTable = dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Copy
                        dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Rows.Clear()
                        dtNewInvoiceTable = dtNewInvoiceTable.Select(String.Empty, String.Concat(ViewInvoiceData.GTN_DT, " ASC")).CopyToDataTable
                        dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Merge(dtNewInvoiceTable)
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._REPAIR_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
                Case 82 'MISCELLANEOUS
                    dtInvoiceTable = objViewInvoice.GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNoCancel(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_MISCELLANEOUS_INVOICE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.MSCLLNS_INVC_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.MSCLLNS_INVC_ID)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If

                Case 140 'Credit Note
                    dtInvoiceTable = objViewInvoice.GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNoCancel(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(InvoiceGenerationData._V_MISCELLANEOUS_INVOICE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.MSCLLNS_INVC_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.MSCLLNS_INVC_ID)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Clear()
                    dsViewInvoice.Tables(InvoiceGenerationData._CREDIT_NOTE).Clear()

                    dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Merge(dtInvoiceTable)
                    dsViewInvoice.Tables(InvoiceGenerationData._CREDIT_NOTE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("SUM(" & CommonUIData.AMNT_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._MISCELLANEOUS_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If

                Case 83 'Transportation
                    dtInvoiceTable = objViewInvoice.GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNoCancel(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_TRANSPORTATION_INVOICE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(ViewInvoiceData.TRNSPRTTN_CHRG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(ViewInvoiceData.TRNSPRTTN_CHRG_ID)
                        End If
                        If strTable2PrimaryID <> String.Empty Then
                            strTable2PrimaryID = String.Concat(strTable2PrimaryID, ",", drSelect.Item(ViewInvoiceData.TRNSPRTTN_ID))
                        Else
                            strTable2PrimaryID = drSelect.Item(ViewInvoiceData.TRNSPRTTN_ID)
                        End If
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate)

                    dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        Dim dtNewInvoiceTable As New DataTable
                        dtNewInvoiceTable = dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Copy
                        dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Rows.Clear()
                        dtNewInvoiceTable = dtNewInvoiceTable.Select(String.Empty, String.Concat(ViewInvoiceData.JB_STRT_DT, " ASC")).CopyToDataTable
                        dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Merge(dtNewInvoiceTable)
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.DPT_AMNT & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.DPT_AMNT & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.CSTMR_AMNT & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("SUM(" & CommonUIData.CSTMR_AMNT & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._TRANSPORTATION_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
                Case 84 'Rental
                    Dim strRentalRefNo As String = String.Empty
                    dtInvoiceTable = objViewInvoice.GetRentalChargeByByDepotIDInvoiceNoCancel(i32DepotID, i64ServicePartnerID, strDraftInvoiceNo).Tables(ViewInvoiceData._V_RENTAL_CHARGE)

                    For Each drSelect As DataRow In dtInvoiceTable.Rows
                        If strTable1PrimaryID <> String.Empty Then
                            strTable1PrimaryID = String.Concat(strTable1PrimaryID, ",", drSelect.Item(InvoiceGenerationData.RNTL_CHRG_ID))
                        Else
                            strTable1PrimaryID = drSelect.Item(InvoiceGenerationData.RNTL_CHRG_ID)
                        End If
                        If strEquipmentNo <> String.Empty Then
                            strEquipmentNo = String.Concat(strEquipmentNo, ",", drSelect.Item(ViewInvoiceData.EQPMNT_NO))
                        Else
                            strEquipmentNo = drSelect.Item(ViewInvoiceData.EQPMNT_NO)
                        End If
                        If strGITransactionNo <> String.Empty Then
                            strGITransactionNo = String.Concat(strGITransactionNo, ",", drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO))
                        Else
                            strGITransactionNo = drSelect.Item(ViewInvoiceData.GI_TRNSCTN_NO)
                        End If
                        If strRentalRefNo <> String.Empty Then
                            strRentalRefNo = String.Concat(strRentalRefNo, ",", drSelect.Item(ViewInvoiceData.RNTL_RFRNC_NO))
                        Else
                            strRentalRefNo = drSelect.Item(ViewInvoiceData.RNTL_RFRNC_NO)
                        End If
                    Next


                    For Each dr As DataRow In dtInvoiceTable.Rows
                        dr.Item(ViewInvoiceData.BLLNG_TLL_DT) = DBNull.Value
                    Next

                    objViewInvoice.pub_fillDatasetforInvoiceGeneration(i32InvoiceTypeID, dtInvoiceTable, _
                                                                       decExchangeRate, datFromDate, datToDate)


                    dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Merge(dtInvoiceTable)

                    If dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Rows.Count = 0 Then
                        blnNoRecords = True
                    Else
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty)) Then
                            decTotalDepotAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.DPT_TTL_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty)) Then
                            decTotalCustomerAmount = CDec(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("SUM(" & CommonUIData.TTL_CSTS_NC & ")", String.Empty))
                        End If
                        If Not IsDBNull(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty)) Then
                            intCountEquipment = CInt(dsViewInvoice.Tables(ViewInvoiceData._RENTAL_INVOICE).Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Empty))
                        End If
                    End If
            End Select

            If blnNoRecords Then
                pub_SetCallbackReturnValue("Error_Msg", "No Records Found")
                pub_SetCallbackStatus(False)
                blnNoRecords = False
                Exit Sub
            End If


            Dim blnFinance As Boolean = False
            Dim strFinance As String
            Dim objConfig As New ConfigSetting(objCommon.GetDepotID())

            strFinance = objConfig.FinanceIntegration

            If Not String.IsNullOrEmpty(strFinance) AndAlso CBool(strFinance) = True Then
                blnFinance = True
            End If

            Dim strError_Msg As String = String.Empty
            Dim strUserName As String = objCommon.GetCurrentUserName()

            strError_Msg = objViewInvoice.Sage_Invoice_Generations_InvoiceCancel(i32InvoiceTypeID, bv_strInvoiceNo, dtViewInvoice, dsViewInvoice, bv_strRemarks, i32DepotID, strUserName)

            dtViewInvoice = New DataTable
            dtViewInvoice = objViewInvoice.pub_GetVInvoiceHistoryByDepotID(i32DepotID).Tables(ViewInvoiceData._V_INVOICE_HISTORY)
            dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).Clear()
            dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).Merge(dtViewInvoice)

            CacheData(VIEW_INVOICE, dsViewInvoice)
            pub_SetCallbackReturnValue("DraftInvoiceNo", strDraftInvoiceNo)
            pub_SetCallbackReturnValue("FinalInvoiceNo", strFinalInvoiceNo)
            pub_SetCallbackReturnValue("InvoiceFileName", strInvoiceFileName)
            pub_SetCallbackReturnValue("InvoiceTypeID", i32InvoiceTypeID)


            If strError_Msg <> String.Empty And Not strError_Msg.ToLower = "true" Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackReturnValue("Error_Msg", strError_Msg)
            Else
                pub_SetCallbackStatus(True)
            End If

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvtGetInvoiceType"
    Private Sub pvtGetInvoiceType(ByRef i32InvoiceTypeID As Integer, _
                                  ByVal bv_strInvoiceType As String)
        Try
            Select Case bv_strInvoiceType
                Case "HS" 'Handling & Storage
                    i32InvoiceTypeID = 78
                Case "HT" 'HEATING
                    i32InvoiceTypeID = 79
                Case "CI" 'CLEANING
                    i32InvoiceTypeID = 80
                Case "RP" 'REPAIR
                    i32InvoiceTypeID = 81
                Case "MI" 'MISCELLANEOUS
                    i32InvoiceTypeID = 82
                Case "TP" 'Transportation
                    i32InvoiceTypeID = 83
                Case "RT" 'Rental
                    i32InvoiceTypeID = 84
                Case "CN" 'Credit Note
                    i32InvoiceTypeID = 140
                Case "IN" 'Inspection
                    i32InvoiceTypeID = 151
            End Select

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgViewInvoice, "ITab1_0")
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Billing/ViewInvoice.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgViewInvoice_ClientBind"
    Protected Sub ifgViewInvoice_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgViewInvoice.ClientBind
        Try
            Dim intDepotId As Integer
            dsViewInvoice = CType(RetrieveData(VIEW_INVOICE), ViewInvoiceDataSet)
            'If multi location Config is True, it will fetch all depot invoices(Multi Location Data)
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" AndAlso objCommon.GetOrganizationTypeCD = "HQ" Then
                Dim dtViewInvoice As New DataTable
                If IsNothing(dsViewInvoice) Then
                    dsViewInvoice = objViewInvoice.pub_GetVInvoiceHistoryForAllDepot()
                Else
                    dtViewInvoice = objViewInvoice.pub_GetVInvoiceHistoryForAllDepot().Tables(ViewInvoiceData._V_INVOICE_HISTORY)
                    dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).Clear()
                    dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).Merge(dtViewInvoice)
                End If
                dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).DefaultView.RowFilter = String.Empty
            Else
                intDepotId = objCommon.GetDepotID()
                Dim dtViewInvoice As New DataTable
                If IsNothing(dsViewInvoice) Then
                    dsViewInvoice = objViewInvoice.pub_GetVInvoiceHistoryByDepotID(intDepotId)
                Else
                    dtViewInvoice = objViewInvoice.pub_GetVInvoiceHistoryByDepotID(intDepotId).Tables(ViewInvoiceData._V_INVOICE_HISTORY)
                    dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).Clear()
                    dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).Merge(dtViewInvoice)
                End If
                dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY).DefaultView.RowFilter = String.Empty
            End If



            'Invoice Cancel

            'GetViewInvoiceRoleRights
            Dim dt As DataTable = objViewInvoice.GetViewInvoiceRoleRights(103, objCommon.GetCurrentUserName())

            ifgViewInvoice.PageIndex = 0
            'GWS 
            str_064GWS = objCommonConfig.pub_GetConfigSingleValue("064", intDepotId)
            bln_064GWSActive_Key = objCommonConfig.IsKeyExists
            If str_064GWS Then
                ifgViewInvoice.Columns.Item(1).HeaderText = "Customer / Agent"
            End If

            e.DataSource = dsViewInvoice.Tables(ViewInvoiceData._V_INVOICE_HISTORY)
            CacheData(VIEW_INVOICE, dsViewInvoice)
            CacheData(VIEW_INVOICEROLE, dt)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgViewInvoice_RowDataBound"
    Protected Sub ifgViewInvoice_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgViewInvoice.RowDataBound
        Try
            Dim objCommondata As New CommonData
            Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
            Dim objConfig As New ConfigSetting(objCommon.GetDepotID())
            Dim strFinance As String = Nothing
            strFinance = objConfig.FinanceIntegration
            Dim intCellIndex As Integer = 1

            If objCommondata.GetMultiLocationSupportConfig.ToLower = "false" Then
                CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
            Else
                objConfig = New ConfigSetting(objCommon.GetHeadQuarterID())
                strFinance = objConfig.FinanceIntegration
            End If
            If e.Row.RowType = DataControlRowType.Header Then

                If strFinance.ToUpper() = "TRUE" Then
                    e.Row.Cells(12 + intCellIndex).Style.Add("display", "table-cell")
                    'ifgViewInvoice.Rows.Item(12 + intCellIndex).Visible = True
                Else
                    e.Row.Cells(12 + intCellIndex).Style.Add("display", "none")
                End If

            End If

            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then
                Dim drv As System.Data.DataRowView
                drv = CType(e.Row.DataItem, Data.DataRowView)
                Dim imgPdfLink As Image
                imgPdfLink = CType(e.Row.Cells(8 + intCellIndex).Controls(0), Image)
                imgPdfLink.ToolTip = "PDF"
                imgPdfLink.Visible = True
                imgPdfLink.ImageUrl = "../Images/pdf.png"
                imgPdfLink.Attributes.Add("onclick", String.Concat("downloadInvoice('PDF','", drv.Item(ViewInvoiceData.BLLNG_FLG), "','", drv.Item(ViewInvoiceData.FL_NM), "'); return false;"))
                imgPdfLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                'GWS
                str_064GWS = objCommonConfig.pub_GetConfigSingleValue("064", intDepotId)
                bln_064GWSActive_Key = objCommonConfig.IsKeyExists
                If str_064GWS Then
                    ifgViewInvoice.Columns.Item(1 + intCellIndex).HeaderText = "Customer / Agent"
                End If

                Dim imgexcelLink As Image
                imgexcelLink = CType(e.Row.Cells(9 + intCellIndex).Controls(0), Image)
                imgexcelLink.ToolTip = "EXCEL"
                imgexcelLink.Visible = True
                imgexcelLink.ImageUrl = "../Images/excel.png"
                imgexcelLink.Attributes.Add("onclick", String.Concat("downloadInvoice('EXCEL','", drv.Item(ViewInvoiceData.BLLNG_FLG), "','", drv.Item(ViewInvoiceData.FL_NM), "'); return false;"))
                imgexcelLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")

                Dim imgwordLink As Image
                imgwordLink = CType(e.Row.Cells(10 + intCellIndex).Controls(0), Image)
                imgwordLink.ToolTip = "WORD"
                imgwordLink.Visible = True
                imgwordLink.ImageUrl = "../Images/word.png"
                imgwordLink.Attributes.Add("onclick", String.Concat("downloadInvoice('WORD','", drv.Item(ViewInvoiceData.BLLNG_FLG), "','", drv.Item(ViewInvoiceData.FL_NM), "'); return false;"))
                imgwordLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")

                Dim imgFinalizeLink As Image
                imgFinalizeLink = CType(e.Row.Cells(11 + intCellIndex).Controls(0), Image)
                imgFinalizeLink.ToolTip = "Finalize"
                imgFinalizeLink.Visible = True
                imgFinalizeLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                If drv.Item(ViewInvoiceData.BLLNG_FLG) = "DRAFT" Or drv.Item(ViewInvoiceData.BLLNG_FLG) = "CANCELLED" Then
                    e.Row.Cells(10 + intCellIndex).Enabled = True
                    imgFinalizeLink.ImageUrl = "../Images/final.png"
                    imgFinalizeLink.Attributes.Add("onclick", String.Concat("confirmFinalizingInvoice('", drv.Item(ViewInvoiceData.INVC_BIN), "','", drv.Item(ViewInvoiceData.DPT_ID), "'); return false;"))
                Else
                    e.Row.Cells(10 + intCellIndex).Enabled = False
                    imgFinalizeLink.ImageUrl = "../Images/final_Gray.png"
                    imgFinalizeLink.Attributes.Add("onclick", "")
                End If



                'Invoice Reversal

                If Not drv.Item(ViewInvoiceData.INVC_CNCL) Is DBNull.Value AndAlso drv.Item(ViewInvoiceData.INVC_CNCL).ToString().ToUpper() = "CREDITNOTE" Then
                    drv.Item(ViewInvoiceData.BLLNG_FLG) = "CANCELLED"
                End If

                Dim imgReversalLink As Image
                imgReversalLink = CType(e.Row.Cells(12 + intCellIndex).Controls(0), Image)
                imgReversalLink.ToolTip = "Invoice cancel"
                imgReversalLink.Visible = True

                Dim dt As New DataTable

                If Not RetrieveData(VIEW_INVOICEROLE) Is Nothing Then
                    dt = RetrieveData(VIEW_INVOICEROLE)
                Else
                    dt = objViewInvoice.GetViewInvoiceRoleRights(103, objCommon.GetCurrentUserName())
                    CacheData(VIEW_INVOICEROLE, dt)
                End If


                If drv.Item(ViewInvoiceData.BLLNG_FLG).ToString().ToUpper() = "FINAL" AndAlso dt.Rows.Count > 0 AndAlso Not drv.Item(ViewInvoiceData.INVC_NM).ToString().ToUpper() = "CREDIT_NOTE" AndAlso drv.Item(ViewInvoiceData.VIEW_BT) Is DBNull.Value Then

                    imgReversalLink.ImageUrl = "../Images/Cancel.png"
                    imgReversalLink.Attributes.Add("style", "cursor:pointer;")
                    imgReversalLink.Attributes.Add("onclick", String.Concat("confirmCancelInvoice('", drv.Item(ViewInvoiceData.INVC_BIN), "','", drv.Item(ViewInvoiceData.INVC_NO), "','", drv.Item(ViewInvoiceData.DPT_ID), "'); return false;"))
                Else
                    imgReversalLink.ImageUrl = "../Images/CancelDark.png"
                End If
                If strFinance.ToUpper() = "TRUE" Then
                    e.Row.Cells(12 + intCellIndex).Style.Add("display", "table-cell")
                    'ifgViewInvoice.Rows.Item(12 + intCellIndex).Visible = True
                Else
                    e.Row.Cells(12 + intCellIndex).Style.Add("display", "none")
                End If

            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "generateInvoiceinXml"
    ''' <summary>
    ''' Generate xml of invoice file
    ''' </summary>
    ''' <param name="bv_dsViewInvoice"></param>
    ''' <param name="bv_dtViewInvoice"></param>
    ''' <param name="bv_strFinalInvoiceNo"></param>
    ''' <param name="bv_i32InvoiceTypeID"></param>
    ''' <param name="bv_strUsername"></param>
    ''' <param name="bv_strInvoiceFileName"></param>
    ''' <remarks></remarks>
    Private Sub generateInvoiceinXml(ByVal bv_dsViewInvoice As DataSet, ByVal bv_dtViewInvoice As DataTable, ByVal bv_strFinalInvoiceNo As String, ByVal bv_i32InvoiceTypeID As Integer, ByVal bv_strUsername As String, ByVal bv_strInvoiceFileName As String)
        Try
            Dim objViewInvoice As New ViewInvoice
            objViewInvoice.pub_generateInvoiceXml(bv_dsViewInvoice, bv_dtViewInvoice, bv_strFinalInvoiceNo, bv_i32InvoiceTypeID, bv_strUsername, bv_strInvoiceFileName)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

End Class