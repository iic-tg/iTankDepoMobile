
Partial Class Billing_Invoice
    Inherits Pagebase

#Region "Declarations"
    Dim dsInvoiceDataSet As New InvoiceDataSet
    Private Const INVOICE As String = "INVOICE"
    Dim blnKeyExistForNPT As Boolean
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim objCommon As New CommonData
            Dim str_032KeyValue As String = ""
            Dim bln_032KeyExist As Boolean = False
            Dim str_032KeyID As String = ""
            hdnSysDate.Value = CommonWeb.pub_Formatdate(Now())
            pvt_SetChangesMade()

            '' Show Default Value to Lookup for Export Invoice
            'str_032KeyValue = objCommon.GetConfigSetting("032", bln_032KeyExist)
            'str_032KeyID = objCommon.GetEnumID(str_032KeyValue, str_032KeyValue)
            'If bln_032KeyExist Then
            '    lkpINVFormat.LookupColumns(0).InitialValue = str_032KeyID
            '    lkpINVFormat.Text = str_032KeyValue
            'End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Operations/Invoice.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"

    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(lkpCustomer)
    End Sub

#End Region

#Region "Page_OnCallback"

    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "GetInvoiceDetails"
                pvt_GetInvoiceDetails(e.GetCallbackValue("Customer_Id"), e.GetCallbackValue("Invc_Type"))
            Case "CreateInvoice"
                pvt_CreateInvoice(e.GetCallbackValue("Customer_Id"), e.GetCallbackValue("Invc_Type"), _
                                  e.GetCallbackValue("datFromDate"), e.GetCallbackValue("datToDate"), _
                                  e.GetCallbackValue("wfData"), e.GetCallbackValue("btnType"), _
                                  e.GetCallbackValue("InvcBin"), e.GetCallbackValue("InvcNo"))
        End Select
    End Sub

#End Region

#Region "pvt_GetInvoiceDetails"

    Private Sub pvt_GetInvoiceDetails(ByVal bv_strCustomerId As String, ByVal bv_strInvc_Type As String)
        Try
            dsInvoiceDataSet = CType(RetrieveData(INVOICE), InvoiceDataSet)
            If dsInvoiceDataSet Is Nothing Then
                dsInvoiceDataSet = New InvoiceDataSet
            End If

            Dim objInvoice As New Invoice
            Dim objCommon As New CommonData
            Dim dsInvoice As InvoiceDataSet

            dsInvoice = objInvoice.pub_GetVCUSTOMERINVOICEByCSTMRID(CLng(bv_strCustomerId), objCommon.GetDepotID())

            dsInvoiceDataSet.Tables(InvoiceData._V_CUSTOMER_INVOICE).Clear()
            dsInvoiceDataSet.Tables(InvoiceData._V_CUSTOMER_INVOICE).Merge(dsInvoice.Tables(InvoiceData._V_CUSTOMER_INVOICE))

            If dsInvoiceDataSet.Tables(InvoiceData._V_CUSTOMER_INVOICE).Rows.Count > 0 Then
                With dsInvoiceDataSet.Tables(InvoiceData._V_CUSTOMER_INVOICE).Rows(0)

                    'Get the time period for the invoice
                    Dim firstDayOfCurrentMonth = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
                    Dim dtFromDate As Date
                    Dim dtToDate As Date

                    Dim dtLstInvoiceDate As Date
                    If bv_strInvc_Type <> "" Then
                        If bv_strInvc_Type = "HS" Then
                            If IsDBNull(.Item(InvoiceData.LST_HS_INVC_GNRTD_DT)) Then
                                dtLstInvoiceDate = firstDayOfCurrentMonth
                            Else
                                dtLstInvoiceDate = .Item(InvoiceData.LST_HS_INVC_GNRTD_DT)
                            End If
                        Else
                            If IsDBNull(.Item(InvoiceData.LST_RP_INVC_GNRTD_DT)) Then
                                dtLstInvoiceDate = firstDayOfCurrentMonth
                            Else
                                dtLstInvoiceDate = .Item(InvoiceData.LST_RP_INVC_GNRTD_DT)
                            End If
                        End If

                        If .Item(InvoiceData.BLLNG_TYP_CD) = "DAILY" Then
                            If dtLstInvoiceDate = firstDayOfCurrentMonth Then
                                dtFromDate = firstDayOfCurrentMonth
                            Else
                                dtFromDate = DateAdd(DateInterval.Day, 1, dtLstInvoiceDate)
                            End If
                            'datToDate.DateIcon.IsVisible = True
                        ElseIf .Item(InvoiceData.BLLNG_TYP_CD) = "FORTHNIGHTLY" Then
                            If dtLstInvoiceDate = firstDayOfCurrentMonth Then
                                dtFromDate = firstDayOfCurrentMonth
                                dtToDate = dtFromDate.AddDays(14)
                            Else
                                dtFromDate = DateAdd(DateInterval.Day, 1, dtLstInvoiceDate)
                                If Day(dtLstInvoiceDate) = 15 Then
                                    Dim dtNewFromDate = dtFromDate.AddDays(-15)
                                    dtToDate = dtNewFromDate.AddMonths(1).AddDays(-1)
                                Else
                                    dtToDate = dtFromDate.AddDays(14)
                                End If
                            End If
                            datToDate.DateIcon.IsVisible = False
                        ElseIf .Item(InvoiceData.BLLNG_TYP_CD) = "MONTHLY" Then
                            If dtLstInvoiceDate = firstDayOfCurrentMonth Then
                                dtFromDate = firstDayOfCurrentMonth
                                dtToDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, dtFromDate))
                            Else
                                dtFromDate = DateAdd(DateInterval.Day, 1, dtLstInvoiceDate)
                                dtToDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, dtFromDate))
                            End If
                        End If
                        ' datToDate.DateIcon.IsVisible = False
                    End If
                    'Ends

                    'Get BtnNAme
                    Dim strBtnType As String = ""

                    If dtFromDate <> "12:00:00 AM" AndAlso (dtToDate <> "12:00:00 AM" Or .Item(InvoiceData.BLLNG_TYP_CD) = "DAILY") Then

                        If .Item(InvoiceData.BLLNG_TYP_CD) = "MONTHLY" Then
                            If DateDiff(DateInterval.Day, dtToDate, firstDayOfCurrentMonth) > 1 Then
                                'Generate Next button is visible & Generate & verified- invisible
                                strBtnType = "GenerateNext"
                            ElseIf DateDiff(DateInterval.Day, dtToDate, firstDayOfCurrentMonth) = 1 Then
                                'Generate  button is visible & GenerateNext & verified - invisible
                                strBtnType = "Generate"
                            Else
                                strBtnType = "Generate"
                            End If
                        ElseIf .Item(InvoiceData.BLLNG_TYP_CD) = "FORTHNIGHTLY" Then
                            If DateDiff(DateInterval.Day, firstDayOfCurrentMonth, DateTime.Now) > 15 Then
                                Dim dat_ftndate As New DateTime(Year(DateTime.Now), Month(DateTime.Now), 16)
                                If DateDiff(DateInterval.Day, dtToDate, dat_ftndate) > 1 Then
                                    'Generate Next button is visible & Generate & verified- invisible
                                    strBtnType = "GenerateNext"
                                ElseIf DateDiff(DateInterval.Day, dtToDate, dat_ftndate) = 1 Then
                                    'Generate  button is visible & GenerateNext & verified - invisible
                                    strBtnType = "Generate"
                                Else
                                    strBtnType = "Generate"
                                End If
                            ElseIf DateDiff(DateInterval.Day, firstDayOfCurrentMonth, DateTime.Now) < 15 Then
                                If DateDiff(DateInterval.Day, dtToDate, firstDayOfCurrentMonth) > 1 Then
                                    'Generate Next button is visible & Generate & verified- invisible
                                    strBtnType = "GenerateNext"
                                ElseIf DateDiff(DateInterval.Day, dtToDate, firstDayOfCurrentMonth) = 1 Then
                                    'Generate  button is visible & GenerateNext & verified - invisible
                                    strBtnType = "Generate"
                                Else
                                    strBtnType = "Generate"

                                End If
                            End If
                        ElseIf .Item(InvoiceData.BLLNG_TYP_CD) = "DAILY" Then
                            strBtnType = "Generate"
                        End If
                    Else
                        strBtnType = "billed"
                    End If

                    'ENds
                    pub_SetCallbackReturnValue("CustCurrency", .Item(InvoiceData.CSTMR_CRRNCY_CD))
                    If IsDBNull(.Item(InvoiceData.CNVRT_TO_CRRNCY_CD)) Then
                        pub_SetCallbackReturnValue("ConvToCurrency", "")
                    Else
                        pub_SetCallbackReturnValue("ConvToCurrency", .Item(InvoiceData.CNVRT_TO_CRRNCY_CD))
                    End If
                    pub_SetCallbackReturnValue("ExchangeRate", .Item(InvoiceData.EXCHNG_RT))
                    pub_SetCallbackReturnValue("FromDate", dtFromDate.ToString("dd-MMM-yyyy").ToUpper())
                    pub_SetCallbackReturnValue("btnType", strBtnType)
                    If Not .Item(InvoiceData.BLLNG_TYP_CD) = "DAILY" Then
                        pub_SetCallbackReturnValue("ToDate", dtToDate.ToString("dd-MMM-yyyy").ToUpper())
                    Else
                        pub_SetCallbackReturnValue("ToDate", "")
                    End If
                    'pub_SetCallbackReturnValue("PageID", .Item(InvoiceData.CSTMR_ID))
                End With
                pub_SetCallbackReturnValue("Message", "")

            Else
                pub_SetCallbackReturnValue("Message", "No records found in Customer Rates and Exchange Rate.")
            End If
            CacheData(INVOICE, dsInvoiceDataSet)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "ifgInvoice_ClientBind"

    Protected Sub ifgInvoice_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgInvoice.ClientBind
        Try
            If Not e.Parameters("Invc_Type") Is Nothing And Not e.Parameters("Invc_Type").ToString = "" Then
                Dim objInvoice As New Invoice
                Dim objCommon As New CommonData
                Dim strInvc_Typ As String = e.Parameters("Invc_Type").ToString()

                dsInvoiceDataSet = objInvoice.pub_GetV_INVOICEByINVC_TYP(strInvc_Typ, objCommon.GetDepotID())

                If dsInvoiceDataSet.Tables(InvoiceData._V_INVOICE).Rows.Count > 0 Then
                    e.DataSource = dsInvoiceDataSet.Tables(InvoiceData._V_INVOICE)
                Else
                    e.OutputParameters.Add("norecordsfound", "True")
                End If
                
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "pvt_CreateInvoice"
    Private Sub pvt_CreateInvoice(ByVal bv_strCustomerId As String, ByVal bv_strInvc_Type As String, _
                                  ByVal bv_datFromDate As Date, ByVal bv_datToDate As Date, _
                                  ByVal bv_strWFdata As String, ByVal bv_strbtnType As String, _
                                 ByVal bv_lngInvcBin As Long, ByVal bv_strInvc_No As String)
        Try
            dsInvoiceDataSet = CType(RetrieveData(INVOICE), InvoiceDataSet)

            Dim dsInvoice As InvoiceDataSet
            Dim objInvoice As New Invoice
            Dim objCommon As New CommonData
            Dim datTempToDate As Date = Nothing

            If dsInvoiceDataSet.Tables(InvoiceData._V_CUSTOMER_INVOICE).Rows(0).Item(InvoiceData.BLLNG_TYP_CD).ToString = "DAILY" And (bv_datFromDate > bv_datToDate) And bv_datToDate <> "12:00:00 AM" Then
                pub_SetCallbackReturnValue("Message", "From Date should not be greater than To Date")

            ElseIf dsInvoiceDataSet.Tables(InvoiceData._V_CUSTOMER_INVOICE).Rows(0).Item(InvoiceData.BLLNG_TYP_CD).ToString = "DAILY" And (bv_datFromDate = bv_datToDate) And bv_datToDate <> "12:00:00 AM" Then
                pub_SetCallbackReturnValue("Message", "From Date and To Date should not be same")

            ElseIf dsInvoiceDataSet.Tables(InvoiceData._V_CUSTOMER_INVOICE).Rows(0).Item(InvoiceData.BLLNG_TYP_CD).ToString = "DAILY" And bv_datToDate = "12:00:00 AM" Then
                pub_SetCallbackReturnValue("Message", "Invalid To Date")

            ElseIf dsInvoiceDataSet.Tables(InvoiceData._V_CUSTOMER_INVOICE).Rows(0).Item(InvoiceData.BLLNG_TYP_CD).ToString = "DAILY" And (bv_datToDate > Now) Then
                pub_SetCallbackReturnValue("Message", "Future Date is not allowed for To Date")

            Else
                pub_SetCallbackReturnValue("Message", "")
                If bv_strInvc_Type = "HS" Then

                    datTempToDate = CDate(bv_datToDate.ToString("MM/dd/yyyy") + " 23:59:59")

                    dsInvoice = objInvoice.pub_GetV_HANDLING_CHARGEByCSTMR_ID(bv_strCustomerId, objCommon.GetDepotID(), datTempToDate)

                    dsInvoiceDataSet.Tables(InvoiceData._V_HANDLING_CHARGE).Clear()
                    dsInvoiceDataSet.Merge(dsInvoice.Tables(InvoiceData._V_HANDLING_CHARGE))

                    dsInvoiceDataSet.Tables(InvoiceData._V_HANDLING_STORAGE_INVOICE).Clear()

                    If dsInvoiceDataSet.Tables(InvoiceData._V_HANDLING_CHARGE).Rows.Count > 0 Then
                        For Each drwHndlngStr_Invoice In dsInvoiceDataSet.Tables(InvoiceData._V_HANDLING_CHARGE).Rows
                            Dim drwCharge As DataRow = dsInvoiceDataSet.Tables(InvoiceData._V_HANDLING_STORAGE_INVOICE).NewRow()
                            drwCharge.Item(InvoiceData.EQPMNT_NO) = drwHndlngStr_Invoice.Item(InvoiceData.EQPMNT_NO)
                            drwCharge.Item(InvoiceData.EQPMNT_CD_CD) = drwHndlngStr_Invoice.Item(InvoiceData.EQPMNT_CD_CD)
                            drwCharge.Item(InvoiceData.EQPMNT_TYP_CD) = drwHndlngStr_Invoice.Item(InvoiceData.EQPMNT_TYP_CD)
                            drwCharge.Item(InvoiceData.CST_TYP) = drwHndlngStr_Invoice.Item(InvoiceData.CST_TYP)
                            drwCharge.Item(InvoiceData.RFRNC_EIR_NO_1) = drwHndlngStr_Invoice.Item(InvoiceData.RFRNC_EIR_NO_1)
                            drwCharge.Item(InvoiceData.RFRNC_EIR_NO_2) = drwHndlngStr_Invoice.Item(InvoiceData.RFRNC_EIR_NO_1)
                            drwCharge.Item(InvoiceData.FRM_BLLNG_DT) = drwHndlngStr_Invoice.Item(InvoiceData.FRM_BLLNG_DT)
                            drwCharge.Item(InvoiceData.TO_BLLNG_DT) = drwHndlngStr_Invoice.Item(InvoiceData.TO_BLLNG_DT)
                            drwCharge.Item(InvoiceData.FR_DYS) = drwHndlngStr_Invoice.Item(InvoiceData.FR_DYS)
                            drwCharge.Item(InvoiceData.NO_OF_DYS) = drwHndlngStr_Invoice.Item(InvoiceData.NO_OF_DYS)
                            drwCharge.Item(InvoiceData.CST_NC) = drwHndlngStr_Invoice.Item(InvoiceData.HNDLNG_CST_NC)
                            drwCharge.Item(InvoiceData.TX_RT_NC) = drwHndlngStr_Invoice.Item(InvoiceData.HNDLNG_TX_RT_NC)
                            drwCharge.Item(InvoiceData.TTL_CSTS_NC) = drwHndlngStr_Invoice.Item(InvoiceData.TTL_CSTS_NC)
                            drwCharge.Item(InvoiceData.LSS_CD) = drwHndlngStr_Invoice.Item(InvoiceData.LSS_CD)
                            drwCharge.Item(InvoiceData.YRD_LCTN) = drwHndlngStr_Invoice.Item(InvoiceData.YRD_LCTN)
                            dsInvoiceDataSet.Tables(InvoiceData._V_HANDLING_STORAGE_INVOICE).Rows.Add(drwCharge)
                        Next
                    End If

                    dsInvoice = objInvoice.pub_GetV_STORAGE_CHARGEByCSTMR_ID(bv_strCustomerId, objCommon.GetDepotID(), datTempToDate)
                    dsInvoiceDataSet.Tables(InvoiceData._V_STORAGE_CHARGE).Clear()
                    dsInvoiceDataSet.Merge(dsInvoice.Tables(InvoiceData._V_STORAGE_CHARGE))

                    If dsInvoiceDataSet.Tables(InvoiceData._V_STORAGE_CHARGE).Rows.Count > 0 Then
                        pvt_HSStorage_Calculation(dsInvoiceDataSet.Tables(InvoiceData._V_STORAGE_CHARGE), bv_datFromDate, datTempToDate)

                        For Each drwHndlngStr_Invoice In dsInvoiceDataSet.Tables(InvoiceData._V_STORAGE_CHARGE).Rows
                            Dim drwCharge As DataRow = dsInvoiceDataSet.Tables(InvoiceData._V_HANDLING_STORAGE_INVOICE).NewRow()
                            drwCharge.Item(InvoiceData.EQPMNT_NO) = drwHndlngStr_Invoice.Item(InvoiceData.EQPMNT_NO)
                            drwCharge.Item(InvoiceData.EQPMNT_CD_CD) = drwHndlngStr_Invoice.Item(InvoiceData.EQPMNT_CD_CD)
                            drwCharge.Item(InvoiceData.EQPMNT_TYP_CD) = drwHndlngStr_Invoice.Item(InvoiceData.EQPMNT_TYP_CD)
                            drwCharge.Item(InvoiceData.CST_TYP) = drwHndlngStr_Invoice.Item(InvoiceData.CST_TYP)
                            drwCharge.Item(InvoiceData.RFRNC_EIR_NO_1) = drwHndlngStr_Invoice.Item(InvoiceData.RFRNC_EIR_NO_1)
                            drwCharge.Item(InvoiceData.RFRNC_EIR_NO_2) = drwHndlngStr_Invoice.Item(InvoiceData.RFRNC_EIR_NO_1)
                            drwCharge.Item(InvoiceData.FRM_BLLNG_DT) = drwHndlngStr_Invoice.Item(InvoiceData.FRM_BLLNG_DT)
                            drwCharge.Item(InvoiceData.TO_BLLNG_DT) = drwHndlngStr_Invoice.Item(InvoiceData.TO_BLLNG_DT)
                            drwCharge.Item(InvoiceData.FR_DYS) = drwHndlngStr_Invoice.Item(InvoiceData.FR_DYS)
                            drwCharge.Item(InvoiceData.NO_OF_DYS) = drwHndlngStr_Invoice.Item(InvoiceData.NO_OF_DYS)
                            drwCharge.Item(InvoiceData.CST_NC) = drwHndlngStr_Invoice.Item(InvoiceData.STRG_CST_NC)
                            drwCharge.Item(InvoiceData.TX_RT_NC) = drwHndlngStr_Invoice.Item(InvoiceData.STRG_TX_RT_NC)
                            drwCharge.Item(InvoiceData.TTL_CSTS_NC) = drwHndlngStr_Invoice.Item(InvoiceData.TTL_CSTS_NC)
                            drwCharge.Item(InvoiceData.LSS_CD) = drwHndlngStr_Invoice.Item(InvoiceData.LSS_CD)
                            drwCharge.Item(InvoiceData.YRD_LCTN) = drwHndlngStr_Invoice.Item(InvoiceData.YRD_LCTN)
                            drwCharge.Item(InvoiceData.PTI_RT) = drwHndlngStr_Invoice.Item(InvoiceData.PTI_RT)
                            drwCharge.Item(InvoiceData.HNDLNG_CHRG_NC) = drwHndlngStr_Invoice.Item(InvoiceData.HNDLNG_CHRG_NC)
                            drwCharge.Item(InvoiceData.WSHNG_CHRG_NC) = drwHndlngStr_Invoice.Item(InvoiceData.WSHNG_CHRG_NC)
                            dsInvoiceDataSet.Tables(InvoiceData._V_HANDLING_STORAGE_INVOICE).Rows.Add(drwCharge)
                        Next

                    End If
                    'Added For NPT Customer
                    Dim strKeyValue As String
                    strKeyValue = objCommon.GetConfigSetting("011", blnKeyExistForNPT)
                    If blnKeyExistForNPT Then
                        dsInvoice = objInvoice.pub_GetV_RPT_PENDINGINVOICEByCSTMR_ID(bv_strCustomerId, objCommon.GetDepotID(), datTempToDate)
                        dsInvoiceDataSet.Tables(InvoiceData._V_RPT_PENDINGINVOICE).Clear()
                        dsInvoiceDataSet.Merge(dsInvoice.Tables(InvoiceData._V_RPT_PENDINGINVOICE))
                        Dim drChargeDetails As DataRow = dsInvoiceDataSet.Tables(InvoiceData._V_CHARGE_DETAILS).NewRow()
                        If dsInvoiceDataSet.Tables(InvoiceData._V_RPT_PENDINGINVOICE).Rows.Count > 0 Then
                            drChargeDetails.Item(InvoiceData.STRG_CHRG_NC) = CommonUIs.iDec(dsInvoiceDataSet.Tables(InvoiceData._V_RPT_PENDINGINVOICE).Compute("SUM([" & InvoiceData.STRG_CST_NC & "])", InvoiceData.CST_TYP & "='STR'"))
                            drChargeDetails.Item(InvoiceData.HNDLNG_CHRG_NC) = CommonUIs.iDec(dsInvoiceDataSet.Tables(InvoiceData._V_RPT_PENDINGINVOICE).Compute("SUM([" & InvoiceData.HNDLNG_CHRG_NC & "])", InvoiceData.CST_TYP & "='STR'"))
                            drChargeDetails.Item(InvoiceData.WSHNG_CHRG_NC) = CommonUIs.iDec(dsInvoiceDataSet.Tables(InvoiceData._V_RPT_PENDINGINVOICE).Compute("SUM([" & InvoiceData.WSHNG_CHRG_NC & "])", InvoiceData.CST_TYP & "='STR'"))
                            drChargeDetails.Item(InvoiceData.PTI_RT) = CommonUIs.iDec(dsInvoiceDataSet.Tables(InvoiceData._V_RPT_PENDINGINVOICE).Compute("SUM([" & InvoiceData.PTI_RT & "])", InvoiceData.CST_TYP & "='STR'"))
                            drChargeDetails.Item(InvoiceData.LFT_CHRG_NC) = CommonUIs.iDec(dsInvoiceDataSet.Tables(InvoiceData._V_RPT_PENDINGINVOICE).Compute("SUM([" & InvoiceData.LFT_CHRG_NC & "])", InvoiceData.CST_TYP & "='STR'"))
                            drChargeDetails.Item(InvoiceData.SB_TTL_NC) = (CommonUIs.iDec(drChargeDetails.Item(InvoiceData.STRG_CHRG_NC)) + CommonUIs.iDec(drChargeDetails.Item(InvoiceData.HNDLNG_CHRG_NC)) + CommonUIs.iDec(drChargeDetails.Item(InvoiceData.WSHNG_CHRG_NC)) + CommonUIs.iDec(drChargeDetails.Item(InvoiceData.PTI_RT)) + CommonUIs.iDec(drChargeDetails.Item(InvoiceData.LFT_CHRG_NC)))
                            drChargeDetails.Item(InvoiceData.VAT_NC) = (CommonUIs.iDec(drChargeDetails.Item(InvoiceData.SB_TTL_NC))) / 100 * 15
                            drChargeDetails.Item(InvoiceData.TTL_NC) = (CommonUIs.iDec(drChargeDetails.Item(InvoiceData.SB_TTL_NC)) + CommonUIs.iDec(drChargeDetails.Item(InvoiceData.VAT_NC)))
                        Else
                            drChargeDetails.Item(InvoiceData.STRG_CHRG_NC) = 0
                            drChargeDetails.Item(InvoiceData.HNDLNG_CHRG_NC) = 0
                            drChargeDetails.Item(InvoiceData.WSHNG_CHRG_NC) = 0
                            drChargeDetails.Item(InvoiceData.PTI_RT) = 0
                            drChargeDetails.Item(InvoiceData.LFT_CHRG_NC) = 0
                            drChargeDetails.Item(InvoiceData.SB_TTL_NC) = 0
                            drChargeDetails.Item(InvoiceData.VAT_NC) = 0
                            drChargeDetails.Item(InvoiceData.TTL_NC) = 0
                        End If

                        dsInvoiceDataSet.Tables(InvoiceData._V_CHARGE_DETAILS).Rows.Add(drChargeDetails)
                    End If
                    'dsInvoice.Tables(InvoiceData._V_STORAGE_CHARGE) = dtStorageCharge
                    'dsInvoiceDataSet.Tables(InvoiceData._V_STORAGE_CHARGE).Clear()
                    'dsInvoiceDataSet.Merge(dtStorageCharge)
                    Dim blnBLLNG_FLG As String = ""

                    ' Dim blnUpdated As Boolean = objInvoice.pub_ModifyCustomerCharge_CHARGE(bv_datFromDate, bv_datToDate, blnBLLNG_FLG, bv_strCustomerId, objCommon.GetDepotID(), bv_strbtnType, bv_strWFdata)

                    'Dim strCstmrCD As String = dsInvoiceDataSet.Tables(InvoiceData._V_CUSTOMER_INVOICE).Rows(0).Item(InvoiceData.CSTMR_CD)


                ElseIf bv_strInvc_Type = "RP" Then
                    datTempToDate = CDate(bv_datToDate.ToString("MM/dd/yyyy") + " 23:59:59")

                    dsInvoice = objInvoice.pub_GetV_REPAIR_CHARGEByCSTMR_ID(bv_strCustomerId, objCommon.GetDepotID(), datTempToDate)

                    dsInvoiceDataSet.Tables(InvoiceData._V_REPAIR_CHARGE).Clear()
                    dsInvoiceDataSet.Merge(dsInvoice.Tables(InvoiceData._V_REPAIR_CHARGE))
                End If

                Dim strModifiedby As String = objCommon.GetCurrentUserName()
                Dim datModifiedDate As String = objCommon.GetCurrentDate()
                Dim intDepotID As Integer = objCommon.GetDepotID()

                Dim bv_i64CSTMR_CRRNCY_ID, bv_i64INVC_CRRNCY_ID, bv_i64BLLNG_TYP_ID As Int64
                Dim bv_EXCHNG_RT_NC, bv_HNDLNG_TTL_CST, bv_STR_TTL_CST, bv_TTL_CST_CSTMR_CRRNCY, bv_TTL_CST_INVC_CRRNCY As Decimal
                Dim decApprovalAmount, bv_RPR_TTL_CST As Decimal
                With dsInvoiceDataSet.Tables(InvoiceData._V_CUSTOMER_INVOICE).Rows(0)
                    bv_i64CSTMR_CRRNCY_ID = .Item(InvoiceData.CSTMR_CRRNCY_ID)
                    If Not IsDBNull(.Item(InvoiceData.CNVRT_TO_CRRNCY_CD)) Then
                        bv_i64INVC_CRRNCY_ID = .Item(InvoiceData.CNVRT_TO_CRRNCY_ID)
                    Else
                        bv_i64INVC_CRRNCY_ID = bv_i64CSTMR_CRRNCY_ID
                    End If
                    bv_EXCHNG_RT_NC = .Item(InvoiceData.EXCHNG_RT)
                    bv_i64BLLNG_TYP_ID = .Item(InvoiceData.BLLNG_TYP_ID)
                End With

                If bv_strInvc_Type = "HS" Then
                    For Each drHndlng_chrg As DataRow In dsInvoiceDataSet.Tables(InvoiceData._V_HANDLING_CHARGE).Rows
                        With drHndlng_chrg
                            bv_HNDLNG_TTL_CST = bv_HNDLNG_TTL_CST + Convert.ToDecimal(.Item(InvoiceData.TTL_CSTS_NC))
                        End With
                    Next
                    For Each drStrg_chrg As DataRow In dsInvoiceDataSet.Tables(InvoiceData._V_STORAGE_CHARGE).Rows
                        With drStrg_chrg
                            bv_STR_TTL_CST = bv_STR_TTL_CST + Convert.ToDecimal(.Item(InvoiceData.TTL_CSTS_NC))
                        End With
                    Next
                    bv_TTL_CST_CSTMR_CRRNCY = bv_HNDLNG_TTL_CST + bv_STR_TTL_CST
                    bv_TTL_CST_INVC_CRRNCY = (bv_TTL_CST_CSTMR_CRRNCY * bv_EXCHNG_RT_NC)
                ElseIf bv_strInvc_Type = "RP" Then
                    For Each drRP_chrg As DataRow In dsInvoiceDataSet.Tables(InvoiceData._V_REPAIR_CHARGE).Rows
                        With drRP_chrg
                            decApprovalAmount = Convert.ToDecimal(.Item(InvoiceData.RPR_APPRVL_AMNT_NC))
                            bv_RPR_TTL_CST = bv_RPR_TTL_CST + decApprovalAmount
                        End With

                    Next

                    bv_TTL_CST_CSTMR_CRRNCY = bv_RPR_TTL_CST
                    bv_TTL_CST_INVC_CRRNCY = (bv_TTL_CST_CSTMR_CRRNCY * bv_EXCHNG_RT_NC)
                End If

                Dim blnUpdateChargeFlg As Boolean = False

                If dsInvoiceDataSet.Tables(InvoiceData._V_HANDLING_STORAGE_INVOICE).Rows.Count > 0 Then
                    blnUpdateChargeFlg = True
                End If


                If dsInvoiceDataSet.Tables(InvoiceData._V_REPAIR_CHARGE).Rows.Count > 0 Then
                    blnUpdateChargeFlg = True
                End If

                dsInvoiceDataSet.Tables(InvoiceData._INVOICE).Clear()
                dsInvoiceDataSet.Tables(InvoiceData._INVOICE).Merge(objInvoice.pub_GetINVOICEbyPeriod(bv_datFromDate, bv_datToDate, _
                                                                                                    objCommon.GetDepotID(), bv_strCustomerId, "Draft", bv_strInvc_Type).Tables(InvoiceData._INVOICE))

                Dim blnInvoiceFlag As Boolean

                If dsInvoiceDataSet.Tables(InvoiceData._INVOICE).Rows.Count > 0 Then
                    blnInvoiceFlag = False
                    If Not bv_lngInvcBin > 0 Then
                        With dsInvoiceDataSet.Tables(InvoiceData._INVOICE).Rows(0)
                            bv_strInvc_No = .Item(InvoiceData.INVC_NO)
                            bv_lngInvcBin = .Item(InvoiceData.INVC_BIN)
                        End With
                    End If
                Else
                    blnInvoiceFlag = True
                End If

                dsInvoice = objInvoice.pub_CreateINVOICE(bv_lngInvcBin, bv_strInvc_No, datModifiedDate, _
                                                                      "", bv_strInvc_Type, bv_i64INVC_CRRNCY_ID, _
                                                                        bv_EXCHNG_RT_NC, bv_i64CSTMR_CRRNCY_ID, _
                                                                        bv_i64BLLNG_TYP_ID, bv_datFromDate, bv_datToDate, _
                                                                        bv_TTL_CST_CSTMR_CRRNCY, bv_TTL_CST_INVC_CRRNCY, _
                                                                        bv_strCustomerId, objCommon.GetDepotID(), _
                                                                        True, strModifiedby, datModifiedDate, bv_strbtnType, _
                                                                        bv_strWFdata, blnUpdateChargeFlg, blnInvoiceFlag)
                dsInvoiceDataSet.Tables(InvoiceData._INVOICE).Clear()
                dsInvoiceDataSet.Tables(InvoiceData._INVOICE).Merge(dsInvoice.Tables(InvoiceData._INVOICE))

                CacheData(INVOICE, dsInvoiceDataSet)
                pub_SetCallbackReturnValue("ID", CStr(bv_lngInvcBin))
                pub_SetCallbackReturnValue("Invc_No", bv_strInvc_No)
                If blnUpdateChargeFlg = False Then
                    pub_SetCallbackReturnValue("ChargeMsg", "No Charges Found.")
                Else
                    pub_SetCallbackReturnValue("ChargeMsg", "")
                End If

            End If

            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_HSStorage_Calculation"

    Private Function pvt_HSStorage_Calculation(ByRef bv_dtStorageCharge As DataTable, _
                                              ByVal bv_frmInvoiceDate As String, _
                                              ByVal bv_toInvoiceDate As String) As DataTable
        Try
            Dim intnoofdays As Integer

            For Each drSc As DataRow In bv_dtStorageCharge.Rows
                If IsDBNull(drSc.Item(InvoiceData.NO_OF_DYS)) Then
                    drSc.Item(InvoiceData.NO_OF_DYS) = 0
                End If

                If Not IsDBNull(drSc.Item(InvoiceData.FRM_BLLNG_DT)) AndAlso Not IsDBNull(drSc.Item(InvoiceData.TO_BLLNG_DT)) Then
                    If drSc.Item(InvoiceData.IS_LT_FLG) = "False" Then
                        With drSc
                            If ((Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)) < Convert.ToDateTime(bv_frmInvoiceDate)) And _
                                (Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT)) <= Convert.ToDateTime(bv_toInvoiceDate)) And _
                                (Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT)) >= Convert.ToDateTime(bv_frmInvoiceDate))) Then

                                If ((DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), _
                                    Convert.ToDateTime(bv_frmInvoiceDate)) < Convert.ToInt32(.Item(InvoiceData.FR_DYS)))) Then

                                    If ((DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), _
                                        Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT))) + 1) > Convert.ToInt32(.Item(InvoiceData.FR_DYS))) Then
                                        .Item(InvoiceData.NO_OF_DYS) = ((DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT))) + 1) - (DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), _
                                        Convert.ToDateTime(bv_frmInvoiceDate))) - (Convert.ToInt32(.Item(InvoiceData.FR_DYS)) - (DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(bv_frmInvoiceDate)))))
                                        intnoofdays = .Item(InvoiceData.NO_OF_DYS)
                                    Else
                                        .Item(InvoiceData.NO_OF_DYS) = 0
                                        intnoofdays = .Item(InvoiceData.NO_OF_DYS)
                                    End If

                                ElseIf DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(bv_frmInvoiceDate)) >= Convert.ToInt32(.Item(InvoiceData.FR_DYS)) Then
                                    .Item(InvoiceData.NO_OF_DYS) = (DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT))) + 1) - (DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(bv_frmInvoiceDate)))
                                    intnoofdays = .Item(InvoiceData.NO_OF_DYS)
                                End If

                            End If

                            If ((Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)) >= Convert.ToDateTime(bv_frmInvoiceDate)) And _
                               (Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT)) <= Convert.ToDateTime(bv_toInvoiceDate)) And _
                               (Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT)) >= Convert.ToDateTime(bv_frmInvoiceDate))) Then

                                If ((DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT))) + 1) > Convert.ToInt32(.Item(InvoiceData.FR_DYS))) Then
                                    .Item(InvoiceData.NO_OF_DYS) = ((DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT))) + 1) - Convert.ToInt32(.Item(InvoiceData.FR_DYS)))
                                    intnoofdays = .Item(InvoiceData.NO_OF_DYS)
                                ElseIf ((DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT)))) <= Convert.ToInt32(.Item(InvoiceData.FR_DYS))) Then
                                    .Item(InvoiceData.NO_OF_DYS) = 0
                                    intnoofdays = .Item(InvoiceData.NO_OF_DYS)
                                End If
                            End If
                            If ((Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)) < Convert.ToDateTime(bv_frmInvoiceDate)) And _
                                (Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT)) <= Convert.ToDateTime(bv_toInvoiceDate)) And _
                                (Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT)) < Convert.ToDateTime(bv_frmInvoiceDate))) Then


                                If ((DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT))) + 1) >= Convert.ToInt32(.Item(InvoiceData.FR_DYS))) Then
                                    .Item(InvoiceData.NO_OF_DYS) = ((DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT))) + 1) - Convert.ToInt32(.Item(InvoiceData.FR_DYS)))
                                    intnoofdays = .Item(InvoiceData.NO_OF_DYS)
                                Else
                                    .Item(InvoiceData.NO_OF_DYS) = 0
                                    intnoofdays = .Item(InvoiceData.NO_OF_DYS)
                                End If



                            End If
                        End With

                    ElseIf drSc.Item(InvoiceData.IS_LT_FLG) = "True" Then
                        With drSc
                            If ((DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(bv_frmInvoiceDate))) < Convert.ToInt32(.Item(InvoiceData.FR_DYS))) Then
                                .Item(InvoiceData.NO_OF_DYS) = 0
                            Else
                                If ((DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT)))) > Convert.ToInt32(.Item(InvoiceData.FR_DYS))) Then
                                    .Item(InvoiceData.NO_OF_DYS) = -(DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT)), Convert.ToDateTime(bv_frmInvoiceDate)) - 1)
                                    intnoofdays = .Item(InvoiceData.NO_OF_DYS)
                                ElseIf ((DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(.Item(InvoiceData.TO_BLLNG_DT)))) <= Convert.ToInt32(.Item(InvoiceData.FR_DYS))) Then
                                    .Item(InvoiceData.NO_OF_DYS) = -(DateDiff(DateInterval.Day, Convert.ToDateTime(.Item(InvoiceData.FRM_BLLNG_DT)), Convert.ToDateTime(bv_frmInvoiceDate)) - Convert.ToInt32(.Item(InvoiceData.FR_DYS)))
                                    intnoofdays = .Item(InvoiceData.NO_OF_DYS)
                                End If
                            End If
                        End With

                    End If

                    'TotalCost
                    With drSc
                        intnoofdays = .Item(InvoiceData.NO_OF_DYS)
                        .Item(InvoiceData.TTL_CSTS_NC) = ((Convert.ToInt32(.Item(InvoiceData.NO_OF_DYS)) * Convert.ToDecimal(.Item(InvoiceData.STRG_CST_NC))))
                        If .Item(InvoiceData.TTL_CSTS_NC) <> 0 Then
                            .Item(InvoiceData.TTL_CSTS_NC) = .Item(InvoiceData.TTL_CSTS_NC) + Convert.ToDecimal(.Item(InvoiceData.STRG_TX_RT_NC))
                        End If
                    End With

                End If
            Next
            'TotalCost
            'For Each drSc As DataRow In bv_dtStorageCharge.Rows   'Later want to do wit one loop
            '    For intcount As Integer = 0 To dtStorageCharge.Rows.Count - 1
            '    With drSc
            '        intnoofdays = .Item(InvoiceData.NO_OF_DYS)
            '        .Item("TotalCost") = ((Convert.ToInt32(.Item(InvoiceData.NO_OF_DYS)) * Convert.ToDecimal(.Item("Cost"))))
            '        If .Item("TotalCost") <> 0 Then
            '            .Item("TotalCost") = .Item("TotalCost") + Convert.ToDecimal(.Item("TaxRate"))
            '        End If
            '    End With

            'Next

            Return bv_dtStorageCharge

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function

#End Region

#Region "ifgInvoice_RowDataBound"
    Protected Sub ifgInvoice_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgInvoice.RowDataBound
        Try
            Dim hypPdflnk As HyperLink
            Dim hypXllnk As HyperLink
            Dim hypWordlnk As HyperLink
            hypPdflnk = CType(e.Row.Cells(7).Controls(0), HyperLink)
            hypPdflnk.Attributes.Add("onclick", "openInvoice('" + hypPdflnk.Text + "')")
            hypPdflnk.NavigateUrl = "#"
            hypPdflnk.Text = "PDF"
            hypXllnk = CType(e.Row.Cells(8).Controls(0), HyperLink)
            hypXllnk.Text = "XLS"
            hypXllnk.Attributes.Add("onclick", "openInvoice('" + hypXllnk.Text + "')")
            hypXllnk.NavigateUrl = "#"

            hypWordlnk = CType(e.Row.Cells(9).Controls(0), HyperLink)
            hypWordlnk.Text = "DOC"
            hypWordlnk.Attributes.Add("onclick", "openInvoice('" + hypWordlnk.Text + "')")
            hypWordlnk.NavigateUrl = "#"

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class
