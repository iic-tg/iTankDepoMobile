
Partial Class Masters_Customer
    Inherits Pagebase

#Region "Declaration"

    Private Const KEY_ID As String = "ID"
    Private Const KEY_MODE As String = "mode"
    Private strMSGINSERT As String = "Inserted Successfully."
    Private strMSGUPDATE As String = "Updated Successfully."
    Private strMSGDUPLICATE As String = "This combination already exists."
    Private pvt_lngID As Long
    Dim strCommissionDetailDuplicateRowCondition As String() = {CustomerData.EQPMNT_TYP_CD, CustomerData.EQPMNT_CD_CD}
    Dim dsCustomer As CustomerDataSet
    Dim dsEDISetting As CustomerDataSet
    Private Const CUSTOMER As String = "CUSTOMER"
    Private Const CUSTOMER_EDI_SETTING As String = "CUSTOMER_EDI_SETTING"
    Dim dtCustomerData As DataTable
    Dim blnKeyExistForNPT As Boolean
    Dim bln_31KeyExist As Boolean
    Dim bln_023EqType_Key As Boolean
    Dim str_023EqType As String
    Dim bln_022EqType_Key As Boolean
    Dim str_022EqType As String
    Dim str_041EqType As String
    Dim bln_041EqType_Key As Boolean
    Dim str_050EqType As String
    Dim bln_050EqType_Key As Boolean
    Dim objCommonConfig As New ConfigSetting()

#End Region

#Region "Page_Load"
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                pvt_SetChangesMade()
                chkTransportationBit.Attributes.Add("onclick", "checkTransportation(this)")
                chkRentalBit.Attributes.Add("onclick", "checkRental(this)")
                chkXML.Attributes.Add("onclick", "checkFtp(this)")
                Dim objCommon As New CommonData
                Dim intDepotID As Integer = objCommon.GetDepotID()
                str_022EqType = objCommonConfig.pub_GetConfigSingleValue("022", intDepotID)
                bln_022EqType_Key = objCommonConfig.IsKeyExists
                str_023EqType = objCommonConfig.pub_GetConfigSingleValue("023", intDepotID)
                bln_023EqType_Key = objCommonConfig.IsKeyExists

                str_041EqType = objCommonConfig.pub_GetConfigSingleValue("41", intDepotID)
                bln_041EqType_Key = objCommonConfig.IsKeyExists
                If bln_023EqType_Key Then
                    If str_023EqType = "False" Then
                        lblTransportationBit.Visible = False
                        chkTransportationBit.Visible = False
                        tabTransportation.Visible = False
                    Else
                        lblTransportationBit.Visible = True
                        chkTransportationBit.Visible = True
                        tabTransportation.Visible = True
                    End If
                End If
                If bln_022EqType_Key Then
                    If str_022EqType = "False" Then
                        lblRentalBit.Visible = False
                        chkRentalBit.Visible = False
                        'tabRental.Visible = False
                    Else
                        lblRentalBit.Visible = True
                        chkRentalBit.Visible = True
                        'tabRental.Visible = True
                    End If
                End If
                If bln_041EqType_Key Then
                    If str_041EqType = "False" Then
                        lblXmlBit.Visible = False
                        chkXML.Visible = False
                        'tabFtp.Visible = False
                    Else
                        lblXmlBit.Visible = True
                        chkXML.Visible = True
                        'tabFtp.Visible = True
                    End If

                End If
                bln_050EqType_Key = objCommonConfig.IsKeyExists
                If bln_050EqType_Key Then
                    str_050EqType = objCommonConfig.pub_GetConfigSingleValue("050", intDepotID)
                    pvt_HideMenuGWS(str_050EqType)
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "fnGetData"
                pvt_GetData(e.GetCallbackValue("mode"))
            Case "CreateCustomerGWS"
                pv_CreateCustomerGWS(e.GetCallbackValue("bv_strCSTMR_CD"), _
                                   e.GetCallbackValue("bv_strCSTMR_NAM"), _
                                   CInt(e.GetCallbackValue("bv_i64CSTMR_CRRNCY_ID")), _
                                   CInt(e.GetCallbackValue("bv_i64BLLNG_TYP_ID")), _
                                   CInt(e.GetCallbackValue("bv_i64BLK_EML_FRMT_ID")), _
                                   e.GetCallbackValue("bv_strCNTCT_PRSN_NAM"),
                                   e.GetCallbackValue("bv_strCNTCT_ADDRSS"), _
                                   e.GetCallbackValue("bv_strBLLNG_ADDRSS"), _
                                   e.GetCallbackValue("bv_strZP_CD"), _
                                   e.GetCallbackValue("bv_strPHN_NO"), _
                                   e.GetCallbackValue("bv_strFX_NO"), _
                                   e.GetCallbackValue("bv_strRPRTNG_EML_ID"), _
                                   e.GetCallbackValue("bv_strINVCNG_EML_ID"), _
                                   e.GetCallbackValue("bv_strRPR_TCH_EML_ID"), _
                                   e.GetCallbackValue("bv_blnCHK_DGT_VLDTN_BT"), _
                                   e.GetCallbackValue("bv_blnACTV_BT"), _
                                   e.GetCallbackValue("bv_strEdiCode"), _
                                   e.GetCallbackValue("bv_strCustVatNo"), _
                                   e.GetCallbackValue("bv_strAgent"), _
                                   e.GetCallbackValue("bv_strStorageTax"), _
                                   e.GetCallbackValue("bv_strServiceTax"), _
                                   e.GetCallbackValue("bv_strHandlingTax"), _
                                   e.GetCallbackValue("bv_strLaborRate"), _
                                   e.GetCallbackValue("wfData"))
            Case "UpdateCustomerGWS"
                pvt_UpdateCustomerGWS(e.GetCallbackValue("ID"), _
                                    e.GetCallbackValue("bv_strCSTMR_CD"), _
                                    e.GetCallbackValue("bv_strCSTMR_NAM"), _
                                    CInt(e.GetCallbackValue("bv_i64CSTMR_CRRNCY_ID")), _
                                    CInt(e.GetCallbackValue("bv_i64BLLNG_TYP_ID")), _
                                    CInt(e.GetCallbackValue("bv_i64BLK_EML_FRMT_ID")), _
                                    e.GetCallbackValue("bv_strCNTCT_PRSN_NAM"),
                                    e.GetCallbackValue("bv_strCNTCT_ADDRSS"), _
                                    e.GetCallbackValue("bv_strBLLNG_ADDRSS"), _
                                    e.GetCallbackValue("bv_strZP_CD"), _
                                    e.GetCallbackValue("bv_strPHN_NO"), _
                                    e.GetCallbackValue("bv_strFX_NO"), _
                                    e.GetCallbackValue("bv_strRPRTNG_EML_ID"), _
                                    e.GetCallbackValue("bv_strINVCNG_EML_ID"), _
                                    e.GetCallbackValue("bv_strRPR_TCH_EML_ID"), _
                                    CBool(e.GetCallbackValue("bv_blnCHK_DGT_VLDTN_BT")), _
                                    CBool(e.GetCallbackValue("bv_blnACTV_BT")), _
                                    e.GetCallbackValue("bv_strEdiCode"), _
                                     e.GetCallbackValue("bv_strCustVatNo"), _
                                     e.GetCallbackValue("bv_strAgent"), _
                                      e.GetCallbackValue("bv_strStorageTax"), _
                                   e.GetCallbackValue("bv_strServiceTax"), _
                                   e.GetCallbackValue("bv_strHandlingTax"), _
                                   e.GetCallbackValue("bv_strLaborRate"), _
                                    e.GetCallbackValue("wfData"))
            Case "CreateCustomer"
                pvt_CreateCustomer(e.GetCallbackValue("bv_strCSTMR_CD"), _
                                   e.GetCallbackValue("bv_strCSTMR_NAM"), _
                                   CInt(e.GetCallbackValue("bv_i64CSTMR_CRRNCY_ID")), _
                                   CInt(e.GetCallbackValue("bv_i64BLLNG_TYP_ID")), _
                                   CInt(e.GetCallbackValue("bv_i64BLK_EML_FRMT_ID")), _
                                   e.GetCallbackValue("bv_strCNTCT_PRSN_NAM"),
                                   e.GetCallbackValue("bv_strCNTCT_ADDRSS"), _
                                   e.GetCallbackValue("bv_strBLLNG_ADDRSS"), _
                                   e.GetCallbackValue("bv_strZP_CD"), _
                                   e.GetCallbackValue("bv_strPHN_NO"), _
                                   e.GetCallbackValue("bv_strFX_NO"), _
                                   e.GetCallbackValue("bv_strRPRTNG_EML_ID"), _
                                   e.GetCallbackValue("bv_strINVCNG_EML_ID"), _
                                   e.GetCallbackValue("bv_strRPR_TCH_EML_ID"), _
                                   CDec(e.GetCallbackValue("bv_decHYDR_AMNT_NC")), _
                                   CDec(e.GetCallbackValue("bv_decPNMTC_AMNT_NC")), _
                                   CDec(e.GetCallbackValue("bv_decLBR_RT_PR_HR_NC")), _
                                   CDec(e.GetCallbackValue("bv_decLK_TST_RT_NC")), _
                                   CDec(e.GetCallbackValue("bv_decSRVY_ONHR_OFFHR_RT_NC")), _
                                   CInt(e.GetCallbackValue("bv_i64PRDC_TST_TYP_ID")), _
                                   e.GetCallbackValue("bv_decVLDTY_PRD_TST_YRS"), _
                                   CDec(e.GetCallbackValue("bv_decMIN_HTNG_RT_NC")), _
                                   CDec(e.GetCallbackValue("bv_decMIN_HTNG_PRD_NC")), _
                                   CDec(e.GetCallbackValue("bv_decHRLY_CHRG_NC")), _
                                   CBool(e.GetCallbackValue("bv_blnTRNSPRTTN_BT")), _
                                   CBool(e.GetCallbackValue("bv_blnRNTL_BT")), _
                                   CBool(e.GetCallbackValue("bv_blnCHK_DGT_VLDTN_BT")), _
                                   CBool(e.GetCallbackValue("bv_blnXML_BT")), _
                                   CBool(e.GetCallbackValue("bv_blnACTV_BT")), _
                                   e.GetCallbackValue("bv_strServerUrl"), _
                                   e.GetCallbackValue("bv_strServerName"), _
                                   e.GetCallbackValue("bv_strPassword"), _
                                   e.GetCallbackValue("bv_strEdiCode"), _
                                   e.GetCallbackValue("FinanceIntegrationBit"), _
                                   e.GetCallbackValue("bv_LedgerId"), _
                                   e.GetCallbackValue("bv_blnShell"), _
                                   e.GetCallbackValue("bv_blnSTube"), _
                                   e.GetCallbackValue("wfData"))

            Case "UpdateCustomer"
                pvt_UpdateCustomer(e.GetCallbackValue("ID"), _
                                   e.GetCallbackValue("bv_strCSTMR_CD"), _
                                   e.GetCallbackValue("bv_strCSTMR_NAM"), _
                                   CInt(e.GetCallbackValue("bv_i64CSTMR_CRRNCY_ID")), _
                                   CInt(e.GetCallbackValue("bv_i64BLLNG_TYP_ID")), _
                                   CInt(e.GetCallbackValue("bv_i64BLK_EML_FRMT_ID")), _
                                   e.GetCallbackValue("bv_strCNTCT_PRSN_NAM"),
                                   e.GetCallbackValue("bv_strCNTCT_ADDRSS"), _
                                   e.GetCallbackValue("bv_strBLLNG_ADDRSS"), _
                                   e.GetCallbackValue("bv_strZP_CD"), _
                                   e.GetCallbackValue("bv_strPHN_NO"), _
                                   e.GetCallbackValue("bv_strFX_NO"), _
                                   e.GetCallbackValue("bv_strRPRTNG_EML_ID"), _
                                   e.GetCallbackValue("bv_strINVCNG_EML_ID"), _
                                   e.GetCallbackValue("bv_strRPR_TCH_EML_ID"), _
                                   CDec(e.GetCallbackValue("bv_decHYDR_AMNT_NC")), _
                                   CDec(e.GetCallbackValue("bv_decPNMTC_AMNT_NC")), _
                                   CDec(e.GetCallbackValue("bv_decLBR_RT_PR_HR_NC")), _
                                   CDec(e.GetCallbackValue("bv_decLK_TST_RT_NC")), _
                                   CDec(e.GetCallbackValue("bv_decSRVY_ONHR_OFFHR_RT_NC")), _
                                   CInt(e.GetCallbackValue("bv_i64PRDC_TST_TYP_ID")), _
                                   e.GetCallbackValue("bv_decVLDTY_PRD_TST_YRS"), _
                                   CDec(e.GetCallbackValue("bv_decMIN_HTNG_RT_NC")), _
                                   CDec(e.GetCallbackValue("bv_decMIN_HTNG_PRD_NC")), _
                                   CDec(e.GetCallbackValue("bv_decHRLY_CHRG_NC")), _
                                   CBool(e.GetCallbackValue("bv_blnTRNSPRTTN_BT")),
                                   CBool(e.GetCallbackValue("bv_blnRNTL_BT")),
                                   CBool(e.GetCallbackValue("bv_blnCHK_DGT_VLDTN_BT")),
                                   CBool(e.GetCallbackValue("bv_blnXML_BT")),
                                   CBool(e.GetCallbackValue("bv_blnACTV_BT")),
                                   e.GetCallbackValue("bv_strServerUrl"), _
                                   e.GetCallbackValue("bv_strServerName"), _
                                   e.GetCallbackValue("bv_strPassword"), _
                                   e.GetCallbackValue("bv_strEdiCode"), _
                                   e.GetCallbackValue("FinanceIntegrationBit"), _
                                   e.GetCallbackValue("bv_LedgerId"), _
                                   e.GetCallbackValue("bv_blnShell"), _
                                   e.GetCallbackValue("bv_blnSTube"), _
                                   e.GetCallbackValue("wfData"))

            Case "ValidateCode"
                pvt_ValidateCustomerCode(e.GetCallbackValue("Code"))
            Case "validateRouteCode"
                pvt_ValidateRouteCode(e.GetCallbackValue("RouteCode"), _
                                      e.GetCallbackValue("GridIndex"), _
                                      e.GetCallbackValue("RowState"), _
                                      e.GetCallbackValue("CustomerID"))
            Case "validateContractNo"
                pvt_ValidateContractNo(e.GetCallbackValue("ContractNo"), _
                                      e.GetCallbackValue("GridIndex"), _
                                      e.GetCallbackValue("RowState"), _
                                      e.GetCallbackValue("CustomerID"))
            Case "getTripRate"
                pvt_getTripRate(e.GetCallbackValue("RouteId"))
            Case "GetEquipmentCode"
                pvt_GetEquipmentCode(e.GetCallbackValue("Type"))
            Case "GetEquipmentCodebyTypeId"
                pvt_GetEquipmentCodebyTypeId(e.GetCallbackValue("TypeID"))
        End Select
    End Sub
#End Region

#Region "pvt_GetEquipmentCode"

    Private Sub pvt_GetEquipmentCodebyTypeId(ByVal bv_strTypeId As String)
        Try
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData
            Dim intDepotID As Integer
            Dim dt As New DataTable

            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommon.GetDepotID())
            End If

            dt = objCommonUI.GetEquipmentCodeByTypeID(bv_strTypeId, intDepotID)

            If dt.Rows.Count > 0 Then
                pub_SetCallbackReturnValue("Code", dt.Rows(0).Item(GateinData.EQPMNT_CD_CD).ToString())
                pub_SetCallbackReturnValue("ID", dt.Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString())
                pub_SetCallbackStatus(True)
            Else
                pub_SetCallbackStatus(False)
            End If

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Private Sub pvt_GetEquipmentCode(ByVal bv_strType As String)
        Try
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData
            Dim intDepotID As Integer
            Dim dt As New DataTable

            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommon.GetDepotID())
            End If

            dt = objCommonUI.GetEquipmentCodeByType(bv_strType, intDepotID)

            If dt.Rows.Count > 0 Then
                pub_SetCallbackReturnValue("Code", dt.Rows(0).Item(GateinData.EQPMNT_CD_CD).ToString())
                pub_SetCallbackReturnValue("ID", dt.Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString())
                pub_SetCallbackStatus(True)
            Else
                pub_SetCallbackStatus(False)
            End If

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_getTripRate"
    Private Sub pvt_getTripRate(ByVal bv_i64RouteId As String)
        Try
            Dim dsTemp As New CustomerDataSet
            Dim objCustomer As New Customer
            Dim objCommonData As New CommonData
            Dim decEmptyTripRate As Decimal = 0
            Dim decFullTripRate As Decimal = 0
            Dim intDepotId As Int32 = objCommonData.GetDepotID()
            dsTemp = objCustomer.pub_GetRouteTripRateById(CLng(bv_i64RouteId), intDepotId)
            If dsTemp.Tables(CustomerData._V_ROUTE).Rows.Count > 0 Then
                If Not IsDBNull(dsTemp.Tables(CustomerData._V_ROUTE).Rows(0).Item(CustomerData.EMPTY_TRP_RT_NC)) Then
                    decEmptyTripRate = CDec(dsTemp.Tables(CustomerData._V_ROUTE).Rows(0).Item(CustomerData.EMPTY_TRP_RT_NC))
                End If
                If Not IsDBNull(dsTemp.Tables(CustomerData._V_ROUTE).Rows(0).Item(CustomerData.FLL_TRP_RT_NC)) Then
                    decFullTripRate = CDec(dsTemp.Tables(CustomerData._V_ROUTE).Rows(0).Item(CustomerData.FLL_TRP_RT_NC))
                End If
            End If
            pub_SetCallbackReturnValue("EmptyTripRate", decEmptyTripRate)
            pub_SetCallbackReturnValue("FullTripRate", decFullTripRate)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_calculateDayDifference"
    Private Sub pvt_calculateDayDifference(ByVal bv_strStartDate As String, ByVal bv_strEndDate As String)
        Try
            Dim intTotalDays As Int32 = 0
            intTotalDays = CDate(bv_strEndDate).Subtract(CDate(bv_strStartDate)).TotalDays
            pub_SetCallbackReturnValue("TotalDays", intTotalDays)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetData"
    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim sbCustomer As New StringBuilder
            Dim objCommon As New CommonData
            Dim intDepotID As Integer = objCommon.GetDepotID()
            Dim strDepotCurrency As String = String.Empty
            strDepotCurrency = objCommon.GetDepotLocalCurrencyCode()
            If bv_strMode = MODE_EDIT Then
                str_050EqType = objCommonConfig.pub_GetConfigSingleValue("050", intDepotID)
                bln_050EqType_Key = objCommonConfig.IsKeyExists
                sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtCustomerCode, PageSubmitPane.pub_GetPageAttribute(CustomerData.CSTMR_CD)))
                sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtCustomerName, PageSubmitPane.pub_GetPageAttribute(CustomerData.CSTMR_NAM)))
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.CNTCT_PRSN_NAM) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtContactPersonName, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtContactPersonName, PageSubmitPane.pub_GetPageAttribute(CustomerData.CNTCT_PRSN_NAM)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.CNTCT_ADDRSS) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtContactAddress, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtContactAddress, PageSubmitPane.pub_GetPageAttribute(CustomerData.CNTCT_ADDRSS)))
                End If
                'for EDI Code

                If PageSubmitPane.pub_GetPageAttribute(CustomerData.ISO_CD) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtEdiCode, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtEdiCode, PageSubmitPane.pub_GetPageAttribute(CustomerData.ISO_CD)))
                End If

                If PageSubmitPane.pub_GetPageAttribute(CustomerData.BLLNG_ADDRSS) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtBillingAddress, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtBillingAddress, PageSubmitPane.pub_GetPageAttribute(CustomerData.BLLNG_ADDRSS)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.ZP_CD) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtZipCode, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtZipCode, PageSubmitPane.pub_GetPageAttribute(CustomerData.ZP_CD)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.PHN_NO) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtPhoneNo, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtPhoneNo, PageSubmitPane.pub_GetPageAttribute(CustomerData.PHN_NO)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.FX_NO) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtFax, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtFax, PageSubmitPane.pub_GetPageAttribute(CustomerData.FX_NO)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.RPRTNG_EML_ID) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtEmailforReporting, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtEmailforReporting, PageSubmitPane.pub_GetPageAttribute(CustomerData.RPRTNG_EML_ID)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.INVCNG_EML_ID) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtEmailforInvoicing, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtEmailforInvoicing, PageSubmitPane.pub_GetPageAttribute(CustomerData.INVCNG_EML_ID)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.RPR_TCH_EML_ID) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtEmailforRepairTech, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtEmailforRepairTech, PageSubmitPane.pub_GetPageAttribute(CustomerData.RPR_TCH_EML_ID)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.BLK_EML_FRMT_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CustomerData.BLK_EML_FRMT_ID) <> "" Then
                    sbCustomer.Append(CommonWeb.GetLookupValuesJSO(lkpBulkEmailFormat, PageSubmitPane.pub_GetPageAttribute(CustomerData.BLK_EML_FRMT_ID), PageSubmitPane.pub_GetPageAttribute(CustomerData.BLK_EML_FRMT_CD)))
                Else
                    sbCustomer.Append(CommonWeb.GetLookupValuesJSO(lkpBulkEmailFormat, "", ""))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.BLLNG_TYP_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CustomerData.BLLNG_TYP_ID) <> "" Then
                    sbCustomer.Append(CommonWeb.GetLookupValuesJSO(lkpBillingType, PageSubmitPane.pub_GetPageAttribute(CustomerData.BLLNG_TYP_ID), PageSubmitPane.pub_GetPageAttribute(CustomerData.BLLNG_TYP_CD)))
                Else
                    sbCustomer.Append(CommonWeb.GetLookupValuesJSO(lkpBillingType, "", ""))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.CSTMR_CRRNCY_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CustomerData.CSTMR_CRRNCY_ID) <> "" Then
                    sbCustomer.Append(CommonWeb.GetLookupValuesJSO(lkpCustomerCurrency, PageSubmitPane.pub_GetPageAttribute(CustomerData.CSTMR_CRRNCY_ID), PageSubmitPane.pub_GetPageAttribute(CustomerData.CSTMR_CRRNCY_CD)))
                Else
                    sbCustomer.Append(CommonWeb.GetLookupValuesJSO(lkpCustomerCurrency, "", ""))
                End If

                If PageSubmitPane.pub_GetPageAttribute(CustomerData.HYDR_AMNT_NC) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtHydroAmount, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtHydroAmount, PageSubmitPane.pub_GetPageAttribute(CustomerData.HYDR_AMNT_NC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.PNMTC_AMNT_NC) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtPneumaticAmount, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtPneumaticAmount, PageSubmitPane.pub_GetPageAttribute(CustomerData.PNMTC_AMNT_NC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.LK_TST_RT_NC) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtLeakTestRate, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtLeakTestRate, PageSubmitPane.pub_GetPageAttribute(CustomerData.LK_TST_RT_NC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.SRVY_ONHR_OFFHR_RT_NC) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtSurveyRate, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtSurveyRate, PageSubmitPane.pub_GetPageAttribute(CustomerData.SRVY_ONHR_OFFHR_RT_NC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.PRDC_TST_TYP_ID) <> "NULLABLE" And PageSubmitPane.pub_GetPageAttribute(CustomerData.PRDC_TST_TYP_ID) <> "" Then
                    sbCustomer.Append(CommonWeb.GetLookupValuesJSO(lkpPeriodicTestType, PageSubmitPane.pub_GetPageAttribute(CustomerData.PRDC_TST_TYP_ID), PageSubmitPane.pub_GetPageAttribute(CustomerData.PRDC_TST_TYP_CD)))
                Else
                    sbCustomer.Append(CommonWeb.GetLookupValuesJSO(lkpPeriodicTestType, "", ""))
                End If

                If PageSubmitPane.pub_GetPageAttribute(CustomerData.MIN_HTNG_RT_NC) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtMinHeatingRate, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtMinHeatingRate, PageSubmitPane.pub_GetPageAttribute(CustomerData.MIN_HTNG_RT_NC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.MIN_HTNG_PRD_NC) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtMinHeatingPeriod, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtMinHeatingPeriod, PageSubmitPane.pub_GetPageAttribute(CustomerData.MIN_HTNG_PRD_NC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.HRLY_CHRG_NC) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtHourlyCharge, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtHourlyCharge, PageSubmitPane.pub_GetPageAttribute(CustomerData.HRLY_CHRG_NC)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.VLDTY_PRD_TST_YRS) Is Nothing Then
                    sbCustomer.Append("setText(el('lblValidityPeriodTest'),'');")
                Else
                    sbCustomer.Append(String.Concat("setText(el('lblValidityPeriodTest'),'" + PageSubmitPane.pub_GetPageAttribute(RepairEstimateData.VLDTY_PRD_TST_YRS) + "');"))
                End If
                If strDepotCurrency <> "NULLABLE" And strDepotCurrency <> "" Then
                    sbCustomer.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotCurrency, strDepotCurrency))
                Else
                    sbCustomer.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotCurrency, "", ""))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.FTP_SRVR_URL) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtFtpServer, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtFtpServer, PageSubmitPane.pub_GetPageAttribute(CustomerData.FTP_SRVR_URL)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.FTP_USR_NAM) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtFtpUserName, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtFtpUserName, PageSubmitPane.pub_GetPageAttribute(CustomerData.FTP_USR_NAM)))
                End If
                If PageSubmitPane.pub_GetPageAttribute(CustomerData.FTP_PSSWRD) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtPassword, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtPassword, PageSubmitPane.pub_GetPageAttribute(CustomerData.FTP_PSSWRD)))
                End If
                If bln_050EqType_Key Then
                    If str_050EqType.ToLower = "true" Then
                        If PageSubmitPane.pub_GetPageAttribute(CustomerData.AGENT_ID) <> "" Then
                            sbCustomer.Append(CommonWeb.GetLookupValuesJSO(lkpAgent, PageSubmitPane.pub_GetPageAttribute(CustomerData.AGENT_ID), PageSubmitPane.pub_GetPageAttribute(CustomerData.AGNT_CD)))
                        Else
                            sbCustomer.Append(CommonWeb.GetLookupValuesJSO(lkpAgent, "", ""))
                        End If
                        If PageSubmitPane.pub_GetPageAttribute(CustomerData.SERVC_TX) Is Nothing Then
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtSrvc_Tx_Rt, ""))
                        Else
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtSrvc_Tx_Rt, PageSubmitPane.pub_GetPageAttribute(CustomerData.SERVC_TX)))
                        End If
                        If PageSubmitPane.pub_GetPageAttribute(CustomerData.STORG_TX) Is Nothing Then
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtStorage_Tx_Rt, ""))
                        Else
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtStorage_Tx_Rt, PageSubmitPane.pub_GetPageAttribute(CustomerData.STORG_TX)))
                        End If
                        If PageSubmitPane.pub_GetPageAttribute(CustomerData.HANDLNG_TX) Is Nothing Then
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtHndlng_Tx_Rt, ""))
                        Else
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtHndlng_Tx_Rt, PageSubmitPane.pub_GetPageAttribute(CustomerData.HANDLNG_TX)))
                        End If
                        If PageSubmitPane.pub_GetPageAttribute(CustomerData.LBR_RT_PR_HR_NC) Is Nothing Then
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, ""))
                        Else
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtLaborRate, PageSubmitPane.pub_GetPageAttribute(CustomerData.LBR_RT_PR_HR_NC)))
                        End If
                    Else
                        If PageSubmitPane.pub_GetPageAttribute(CustomerData.LBR_RT_PR_HR_NC) Is Nothing Then
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtLaborRateperHour, ""))
                        Else
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtLaborRateperHour, PageSubmitPane.pub_GetPageAttribute(CustomerData.LBR_RT_PR_HR_NC)))
                        End If
                    End If
                End If


                If PageSubmitPane.pub_GetPageAttribute(CustomerData.FTP_SRVR_URL) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtFtpServer, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtFtpServer, PageSubmitPane.pub_GetPageAttribute(CustomerData.FTP_SRVR_URL)))
                End If
                'for isocode

                If PageSubmitPane.pub_GetPageAttribute(CustomerData.ISO_CD) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtEdiCode, ""))
                Else
                    sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtEdiCode, PageSubmitPane.pub_GetPageAttribute(CustomerData.ISO_CD)))
                End If

                str_022EqType = objCommonConfig.pub_GetConfigSingleValue("022", intDepotID)
                bln_022EqType_Key = objCommonConfig.IsKeyExists
                str_023EqType = objCommonConfig.pub_GetConfigSingleValue("023", intDepotID)
                bln_023EqType_Key = objCommonConfig.IsKeyExists
                str_041EqType = objCommonConfig.pub_GetConfigSingleValue("41", intDepotID)
                bln_041EqType_Key = objCommonConfig.IsKeyExists
                If bln_023EqType_Key Then
                    If Not str_023EqType = "False" AndAlso Not str_050EqType.ToLower = "true" Then
                        sbCustomer.Append(CommonWeb.GetCheckboxValuesJSO(chkTransportationBit, CBool(PageSubmitPane.pub_GetPageAttribute(CustomerData.TRNSPRTTN_BT))))
                    End If
                End If
                If bln_022EqType_Key Then
                    If Not str_022EqType = "False" AndAlso Not str_050EqType.ToLower = "true" Then
                        sbCustomer.Append(CommonWeb.GetCheckboxValuesJSO(chkRentalBit, CBool(PageSubmitPane.pub_GetPageAttribute(CustomerData.RNTL_BT))))
                    End If
                End If
                If bln_041EqType_Key Then
                    If Not str_041EqType = "False" AndAlso Not str_050EqType.ToLower = "true" Then
                        sbCustomer.Append(CommonWeb.GetCheckboxValuesJSO(chkXML, CBool(PageSubmitPane.pub_GetPageAttribute(CustomerData.XML_BT))))
                    End If
                End If
                sbCustomer.Append(CommonWeb.GetCheckboxValuesJSO(chkCheckDigitValidationBit, CBool(PageSubmitPane.pub_GetPageAttribute(CustomerData.CHK_DGT_VLDTN_BT))))
                sbCustomer.Append(CommonWeb.GetCheckboxValuesJSO(chkActiveBit, CBool(PageSubmitPane.pub_GetPageAttribute(CustomerData.ACTV_BT))))
                sbCustomer.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(CustomerData.CSTMR_ID), "');"))
                sbCustomer.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCustomerId, PageSubmitPane.pub_GetPageAttribute(CustomerData.CSTMR_ID)))

                'Customer Master Changes
                If Not PageSubmitPane.pub_GetPageAttribute(CustomerData.SHELL) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetCheckboxValuesJSO(chkShell, CBool(PageSubmitPane.pub_GetPageAttribute(CustomerData.SHELL))))
                Else
                    sbCustomer.Append(CommonWeb.GetCheckboxValuesJSO(chkShell, CBool(False)))
                End If

                If Not PageSubmitPane.pub_GetPageAttribute(CustomerData.STUBE) Is Nothing Then
                    sbCustomer.Append(CommonWeb.GetCheckboxValuesJSO(ChkSTube, CBool(PageSubmitPane.pub_GetPageAttribute(CustomerData.STUBE))))
                Else
                    sbCustomer.Append(CommonWeb.GetCheckboxValuesJSO(ChkSTube, CBool(False)))
                End If

                'Finance Integration
                If bln_050EqType_Key Then
                    If Not str_050EqType.ToLower = "true" Then
                        If PageSubmitPane.pub_GetPageAttribute(CustomerData.LDGR_ID) Is Nothing Then
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtLedgerId, ""))
                        Else
                            sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtLedgerId, PageSubmitPane.pub_GetPageAttribute(CustomerData.LDGR_ID)))
                        End If
                    End If
                End If

            Else
                dsCustomer = New CustomerDataSet
                sbCustomer.Append(CommonWeb.GetHiddenTextValuesJSO(hdnDepotCurrency, strDepotCurrency))
                Dim objCommonData As New CommonData
                sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtHydroAmount, "0.00"))
                sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtPneumaticAmount, "0.00"))
                sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtLaborRateperHour, "0.00"))
                sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtLeakTestRate, "0.00"))
                sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtSurveyRate, "0.00"))
                sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtMinHeatingRate, "0.00"))
                sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtMinHeatingPeriod, "0.00"))
                sbCustomer.Append(CommonWeb.GetTextValuesJSO(txtHourlyCharge, "0.00"))
            End If
            pub_SetCallbackReturnValue("Message", sbCustomer.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgChargeDetail_ClientBind"
    Protected Sub ifgChargeDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgChargeDetail.ClientBind
        Try
            Dim strWFData As String = e.Parameters("WFDATA").ToString()
            Dim strCustomerID As String = e.Parameters("CustomerID").ToString()
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(strWFData, CommonUIData.DPT_ID))
            Dim objCustomer As New Customer
            dsCustomer = objCustomer.pub_GetCustomerChargeDetailByCustomerID(strCustomerID)
            e.DataSource = dsCustomer.Tables(CustomerData._V_CUSTOMER_CHARGE_DETAIL)
            CacheData(CUSTOMER, dsCustomer)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgChargeDetail_RowInserting"
    Protected Sub ifgChargeDetail_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgChargeDetail.RowInserting
        Dim lngCustomerChargeDetailID As Long
        Dim lngCustomerID As Long
        Dim strPageMode As String
        Dim objCustomer As New Customer
        lngCustomerID = CLng(e.InputParamters("CustomerID"))
        strPageMode = CStr(e.InputParamters("mode"))
        Try
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            If CommonWeb.pub_IsRowAlreadyExists(dsCustomer.Tables(CustomerData._V_CUSTOMER_CHARGE_DETAIL), CType(e.Values, OrderedDictionary), strCommissionDetailDuplicateRowCondition, strPageMode, CustomerData.CSTMR_CHRG_DTL_ID, CInt(e.Values(CustomerData.CSTMR_CHRG_DTL_ID))) Then
                e.OutputParamters.Add("Error", "Same Combination cannot be entered when there is already record existing with the given combination.")
                e.Cancel = True
                Exit Sub
            Else
                If strPageMode = "edit" And CommonWeb.pub_IsRowAlreadyExists(objCustomer.pub_GetCustomerChargeDetailByCustomerID(lngCustomerID).Tables(CustomerData._V_CUSTOMER_CHARGE_DETAIL), CType(e.Values, OrderedDictionary), strCommissionDetailDuplicateRowCondition, "new", CustomerData.CSTMR_CHRG_DTL_ID, CInt(e.Values(CustomerData.CSTMR_CHRG_DTL_ID))) Then
                    e.OutputParamters.Add("Error", "Same Combination cannot be entered when there is already record existing with the given combination.")
                    e.Cancel = True
                    Exit Sub
                Else
                    lngCustomerChargeDetailID = CommonWeb.GetNextIndex(dsCustomer.Tables(CustomerData._V_CUSTOMER_CHARGE_DETAIL), CustomerData.CSTMR_CHRG_DTL_ID)
                    e.Values(CustomerData.CSTMR_CHRG_DTL_ID) = lngCustomerChargeDetailID
                    e.Values(CustomerData.CSTMR_ID) = lngCustomerID
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                       MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgChargeDetail_RowUpdating"
    Protected Sub ifgChargeDetail_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgChargeDetail.RowUpdating
        Dim objCustomer As New Customer
        Try
            Dim strPageMode As String = CStr(e.InputParamters("mode"))
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            If CommonWeb.pub_IsRowAlreadyExists(dsCustomer.Tables(CustomerData._V_CUSTOMER_CHARGE_DETAIL), CType(e.NewValues, OrderedDictionary), strCommissionDetailDuplicateRowCondition, "edit", CustomerData.CSTMR_CHRG_DTL_ID, CInt(e.OldValues(CustomerData.CSTMR_CHRG_DTL_ID))) Then
                e.OutputParamters.Add("Error", "Same Combination cannot be entered when there is already record existing with the given combination.")
                e.Cancel = True
                e.RequiresRebind = True
                Exit Sub
            Else
                If CommonWeb.pub_IsRowAlreadyExists(objCustomer.pub_GetCustomerChargeDetailByCustomerID(CInt(e.OldValues(CustomerData.CSTMR_ID))).Tables(CustomerData._V_CUSTOMER_CHARGE_DETAIL), CType(e.NewValues, OrderedDictionary), strCommissionDetailDuplicateRowCondition, "New", CustomerData.CSTMR_CHRG_DTL_ID, CInt(e.OldValues(CustomerData.CSTMR_CHRG_DTL_ID))) Then
                    e.OutputParamters.Add("Error", "Same Combination cannot be entered when there is already record existing with the given combination.")
                    e.Cancel = True
                    e.RequiresRebind = True
                    Exit Sub
                Else
                    e.NewValues(CustomerData.CSTMR_CHRG_DTL_ID) = e.OldValues(CustomerData.CSTMR_CHRG_DTL_ID)
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                     MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgChargeDetail_RowDeleting"
    Protected Sub ifgChargeDetail_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgChargeDetail.RowDeleting
        Try
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            dtCustomerData = dsCustomer.Tables(CustomerData._V_CUSTOMER_CHARGE_DETAIL).Copy()
            Dim lngCustomerId As Long = CLng(e.InputParamters("CustomerID"))
            Dim dr As DataRow() = dtCustomerData.Select(String.Concat(CustomerData.CSTMR_ID, "=", lngCustomerId))
            If dr.Length = 1 Then
                e.OutputParamters.Add("Error", "Atleast one charge detail should be present.")
                e.Cancel = True
                Exit Sub
            Else
                e.OutputParamters("Delete") = String.Concat("Customer Rates : Code ", dtCustomerData.Rows(e.RowIndex).Item(CustomerData.EQPMNT_CD_CD).ToString, " has been be deleted from Customer. Click submit to save changes.")
            End If
            Dim lngCustomerChargeDetailID As Long
            lngCustomerChargeDetailID = CLng(dsCustomer.Tables(CustomerData._V_CUSTOMER_CHARGE_DETAIL).Rows(e.RowIndex)(CustomerData.CSTMR_CHRG_DTL_ID).ToString)
            For Each drChargeHead As DataRow In dsCustomer.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL).Select(String.Concat(CustomerData.CSTMR_CHRG_DTL_ID, "=", lngCustomerChargeDetailID))
                drChargeHead.Delete()
            Next
            For Each drChargeHead As DataRow In dsCustomer.Tables(CustomerData._CUSTOMER_CLEANING_DETAIL).Select(String.Concat(CustomerData.CSTMR_CHRG_DTL_ID, "=", lngCustomerChargeDetailID))
                drChargeHead.Delete()
            Next
            pub_CacheData(CUSTOMER, dsCustomer)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgStorageDetail_Expanded"
    Protected Sub ifgStorageDetail_Expanded(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridExpandedEventArgs) Handles ifgStorageDetail.Expanded
        Dim intCustomerChargeDetailID As Integer = Nothing
        Dim objCustomer As New Customer
        Dim intCustomerID As Integer
        Dim dv As DataView
        Dim strFilter As String = String.Empty
        dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
        intCustomerID = CInt(e.Parameters("CustomerID").ToString())
        Try
            If e.Parameters("CustomerChargeDetailID").ToString() <> String.Empty Then
                intCustomerChargeDetailID = CInt(e.Parameters("CustomerChargeDetailID"))
                strFilter = String.Concat(CustomerData.CSTMR_CHRG_DTL_ID, "=", intCustomerChargeDetailID)
            End If
            If dsCustomer.Tables(CustomerData._ENUM).Rows(e.RowIndex).Item(CustomerData.ENM_CD).ToString.ToUpper = "STORAGE CHARGE" Then
                If (strFilter <> String.Empty AndAlso dsCustomer.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL).Select(strFilter).Length = 0) Then
                    dtCustomerData = objCustomer.pub_GetCustomerChargeDetailByCustomerID(intCustomerID, intCustomerChargeDetailID).Tables(CustomerData._CUSTOMER_STORAGE_DETAIL)
                    dsCustomer.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL).Merge(dtCustomerData)
                End If
                ifgStorageDetail.Columns().Item(2).Visible = False
                ifgStorageDetail.Columns().Item(3).Visible = False
                dv = dsCustomer.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL).DefaultView
            ElseIf dsCustomer.Tables(CustomerData._ENUM).Rows(e.RowIndex).Item(CustomerData.ENM_CD).ToString.ToUpper = "CLEANING CHARGE" Then
                If (strFilter <> String.Empty AndAlso dsCustomer.Tables(CustomerData._CUSTOMER_CLEANING_DETAIL).Select(strFilter).Length = 0) Then
                    dtCustomerData = objCustomer.pub_GetCustomerCleaningChargeDetailByCustomerID(intCustomerID, intCustomerChargeDetailID).Tables(CustomerData._CUSTOMER_CLEANING_DETAIL)
                    dtCustomerData.Columns.Add("CSTMR_STRG_DTL_ID", GetType(System.Int32))
                    For i As Int32 = 0 To dtCustomerData.Rows.Count - 1
                        dtCustomerData.Rows(i).Item(CustomerData.CSTMR_STRG_DTL_ID) = dtCustomerData.Rows(i).Item(CustomerData.CSTMR_CLNNG_DTL_ID)
                    Next
                    dsCustomer.Tables(CustomerData._CUSTOMER_CLEANING_DETAIL).Merge(dtCustomerData)
                End If
                ifgStorageDetail.Columns().Item(0).Visible = False
                ifgStorageDetail.Columns().Item(1).Visible = False
                dv = dsCustomer.Tables(CustomerData._CUSTOMER_CLEANING_DETAIL).DefaultView
            End If
            dv.RowFilter = strFilter
            e.DataSource = dv
            CacheData("RowIndex", e.Parameters("RowIndex"))
            CacheData(CUSTOMER, dsCustomer)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgStorageDetail_RowInserting"
    Protected Sub ifgStorageDetail_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgStorageDetail.RowInserting
        Dim lngCustomerStorageDetailID As Long
        Dim lngCustomerCleaningDetailID As Long
        Dim lngCustomerChargeDetailID As Long
        Dim lngCustomerID As Long
        lngCustomerID = CLng(e.InputParamters("CustomerID"))
        lngCustomerChargeDetailID = CLng(e.InputParamters("CustomerChargeDetailID"))
        Try
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            If dsCustomer.Tables(CustomerData._ENUM).Rows(e.ParentRowIndex).Item(CustomerData.ENM_CD).ToString.ToUpper = "STORAGE CHARGE" Then
                lngCustomerStorageDetailID = CommonWeb.GetNextIndex(dsCustomer.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL), CustomerData.CSTMR_STRG_DTL_ID)
                e.Values(CustomerData.CSTMR_STRG_DTL_ID) = lngCustomerStorageDetailID
                e.Values(CustomerData.CSTMR_CHRG_DTL_ID) = lngCustomerChargeDetailID
                e.Values(CustomerData.CSTMR_ID) = lngCustomerID
            ElseIf dsCustomer.Tables(CustomerData._ENUM).Rows(e.ParentRowIndex).Item(CustomerData.ENM_CD).ToString.ToUpper = "CLEANING CHARGE" Then
                lngCustomerCleaningDetailID = CommonWeb.GetNextIndex(dsCustomer.Tables(CustomerData._CUSTOMER_CLEANING_DETAIL), CustomerData.CSTMR_CLNNG_DTL_ID)
                e.Values(CustomerData.CSTMR_STRG_DTL_ID) = CommonWeb.GetNextIndex(dsCustomer.Tables(CustomerData._CUSTOMER_CLEANING_DETAIL), CustomerData.CSTMR_CLNNG_DTL_ID)
                e.Values(CustomerData.CSTMR_CLNNG_DTL_ID) = lngCustomerCleaningDetailID
                e.Values(CustomerData.CSTMR_CHRG_DTL_ID) = lngCustomerChargeDetailID
                e.Values(CustomerData.CSTMR_ID) = lngCustomerID
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                     MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgStorageDetail_RowDeleting"
    Protected Sub ifgStorageDetail_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgStorageDetail.RowDeleting
        Try
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            Dim paramCleaningDetail As String = e.InputParamters("CleaningDetail")
            Dim intCustomerChargeDetailID As Long = CLng(e.InputParamters("CustomerChargeDetailID"))
            Dim rowIndex As String = RetrieveData("RowIndex")

            If paramCleaningDetail <> Nothing AndAlso rowIndex = 1 Then
                e.OutputParamters("Delete") = String.Concat("Cleaning Charge : Up to Containers ", dsCustomer.Tables(CustomerData._CUSTOMER_CLEANING_DETAIL).Rows(paramCleaningDetail).Item(CustomerData.UP_TO_CNTNRS).ToString, " has been be deleted from Customer. Click submit to save changes.")

            ElseIf rowIndex = 0 Then
                dtCustomerData = dsCustomer.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL).Copy()
                Dim dr As DataRow() = dtCustomerData.Select(String.Concat(CustomerData.CSTMR_CHRG_DTL_ID, "=", intCustomerChargeDetailID))
                Dim intCount = e.RowIndex + 1
                If dr.Length = 1 Then
                    e.OutputParamters.Add("Error", "At least one storage detail must be present")
                    e.Cancel = True
                    Exit Sub
                Else
                    e.OutputParamters("Delete") = String.Concat("Storage Charge : Up to Days ", dtCustomerData.Rows(e.RowIndex).Item(CustomerData.UP_TO_DYS).ToString, " has been be deleted from Customer. Click submit to save changes.")
                    Exit Sub
                End If
            End If
            
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateCustomer"
    Public Function pvt_CreateCustomer(ByVal bv_strCSTMR_CD As String, _
                                       ByVal bv_strCSTMR_NAM As String, _
                                       ByVal bv_i64CSTMR_CRRNCY_ID As Int64, _
                                       ByVal bv_i64BLLNG_TYP_ID As Int64, _
                                       ByVal bv_i64BLK_EML_FRMT_ID As Int64, _
                                       ByVal bv_strCNTCT_PRSN_NAM As String, _
                                       ByVal bv_strCNTCT_ADDRSS As String, _
                                       ByVal bv_strBLLNG_ADDRSS As String, _
                                       ByVal bv_strZP_CD As String, _
                                       ByVal bv_strPHN_NO As String, _
                                       ByVal bv_strFX_NO As String, _
                                       ByVal bv_strRPRTNG_EML_ID As String, _
                                       ByVal bv_strINVCNG_EML_ID As String, _
                                       ByVal bv_strRPR_TCH_EML_ID As String, _
                                       ByVal bv_decHYDR_AMNT_NC As Decimal, _
                                       ByVal bv_decPNMTC_AMNT_NC As Decimal, _
                                       ByVal bv_decLBR_RT_PR_HR_NC As Decimal, _
                                       ByVal bv_decLK_TST_RT_NC As Decimal, _
                                       ByVal bv_decSRVY_ONHR_OFFHR_RT_NC As Decimal, _
                                       ByVal bv_i64PRDC_TST_TYP_ID As Int64, _
                                       ByVal bv_decVLDTY_PRD_TST_YRS As String, _
                                       ByVal bv_decMIN_HTNG_RT_NC As Decimal, _
                                       ByVal bv_decMIN_HTNG_PRD_NC As Decimal, _
                                       ByVal bv_decHRLY_CHRG_NC As Decimal, _
                                       ByVal bv_blnTRNSPRTTN_BT As Boolean, _
                                       ByVal bv_blnRNTL_BT As Boolean, _
                                       ByVal bv_blnCHK_DGT_VLDTN_BT As Boolean, _
                                       ByVal bv_blnXML_BT As Boolean, _
                                       ByVal bv_blnACTV_BT As Boolean, _
                                       ByVal bv_strServerUrl As String, _
                                       ByVal bv_strServerName As String, _
                                       ByVal bv_strPassword As String, _
                                       ByVal bv_strEdiCode As String, _
                                       ByVal FinanceIntegrationBit As Boolean, _
                                       ByVal bv_LedgerId As String, _
                                       ByVal bv_blnShell As Boolean, _
                                       ByVal bv_blnSTube As Boolean, _
                                       ByVal bv_strWfData As String) As Long
        Dim objcommon As New CommonData
        Try
            Dim objCustomer As New Customer
            Dim lngCreated As Long
            Dim dtCustomerChargeDetail As DataTable
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            End If
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            dtCustomerChargeDetail = CType(ifgChargeDetail.DataSource, DataTable)

            If dtCustomerChargeDetail.Rows.Count = 0 Then
                pub_SetCallbackError("Customer Rate is a must for atleast an Equipment Code and Type.")
                pub_SetCallbackStatus(False)
                Return 0
            End If

            If Not pvt_CheckEmptyCustomerStorageDetail(dtCustomerChargeDetail.GetChanges()) Then
                pub_SetCallbackError("Storage Charge is a must for selected Equipment Code and Type.")
                pub_SetCallbackStatus(False)
                Return 0
            End If

            If bv_strCNTCT_ADDRSS.Contains("""") Or bv_strCNTCT_ADDRSS.Contains("'") Then
                pub_SetCallbackError("Single or Double Quotes are not allowed in Billing Address.")
                pub_SetCallbackStatus(False)
                Return 0
            End If

            If bv_strBLLNG_ADDRSS <> Nothing Then
                If bv_strBLLNG_ADDRSS.Contains("""") Or bv_strBLLNG_ADDRSS.Contains("'") Then
                    pub_SetCallbackError("Single or Double Quotes are not allowed in Billing Address.")
                    pub_SetCallbackStatus(False)
                    Return 0
                End If
            End If

            lngCreated = objCustomer.pub_CreateCustomer((bv_strCSTMR_CD), _
                                                         bv_strCSTMR_NAM, _
                                                         bv_i64CSTMR_CRRNCY_ID, _
                                                         bv_i64BLLNG_TYP_ID, _
                                                         bv_i64BLK_EML_FRMT_ID, _
                                                         bv_strCNTCT_PRSN_NAM, _
                                                         bv_strCNTCT_ADDRSS, _
                                                         bv_strBLLNG_ADDRSS, _
                                                         bv_strZP_CD, _
                                                         bv_strPHN_NO, _
                                                         bv_strFX_NO, _
                                                         bv_strRPRTNG_EML_ID, _
                                                         bv_strINVCNG_EML_ID, _
                                                         bv_strRPR_TCH_EML_ID, _
                                                         bv_decHYDR_AMNT_NC, _
                                                         bv_decPNMTC_AMNT_NC, _
                                                         bv_decLBR_RT_PR_HR_NC, _
                                                         bv_decLK_TST_RT_NC, _
                                                         bv_decSRVY_ONHR_OFFHR_RT_NC, _
                                                         bv_i64PRDC_TST_TYP_ID, _
                                                         bv_decVLDTY_PRD_TST_YRS, _
                                                         bv_decMIN_HTNG_RT_NC, _
                                                         bv_decMIN_HTNG_PRD_NC, _
                                                         bv_decHRLY_CHRG_NC, _
                                                         bv_blnTRNSPRTTN_BT, _
                                                         bv_blnRNTL_BT, _
                                                         bv_blnCHK_DGT_VLDTN_BT, _
                                                         bv_blnXML_BT, _
                                                         strModifiedby, _
                                                         datModifiedDate, _
                                                         strModifiedby, _
                                                         datModifiedDate, _
                                                         bv_blnACTV_BT, _
                                                         intDepotID, _
                                                         bv_strServerUrl, _
                                                         bv_strServerName, _
                                                         bv_strPassword, _
                                                         bv_strEdiCode, _
                                                         FinanceIntegrationBit, _
                                                         bv_LedgerId, _
                                                         bv_blnShell, _
                                                         bv_blnSTube, _
                                                         bv_strWfData, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         dsCustomer)
            dsCustomer.AcceptChanges()
            pub_SetCallbackReturnValue("ID", CStr(lngCreated))
            pub_SetCallbackReturnValue("Message", String.Concat("Customer : ", bv_strCSTMR_CD, " ", strMSGINSERT))
            pub_SetCallbackStatus(True)
            Return lngCreated
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "pvt_UpdateCustomer"
    Private Sub pvt_UpdateCustomer(ByVal bv_strCSTMR_ID As String, _
                                   ByVal bv_strCSTMR_CD As String, _
                                   ByVal bv_strCSTMR_NAM As String, _
                                   ByVal bv_i64CSTMR_CRRNCY_ID As Int64, _
                                   ByVal bv_i64BLLNG_TYP_ID As Int64, _
                                   ByVal bv_i64BLK_EML_FRMT_ID As Int64, _
                                   ByVal bv_strCNTCT_PRSN_NAM As String, _
                                   ByVal bv_strCNTCT_ADDRSS As String, _
                                   ByVal bv_strBLLNG_ADDRSS As String, _
                                   ByVal bv_strZP_CD As String, _
                                   ByVal bv_strPHN_NO As String, _
                                   ByVal bv_strFX_NO As String, _
                                   ByVal bv_strRPRTNG_EML_ID As String, _
                                   ByVal bv_strINVCNG_EML_ID As String, _
                                   ByVal bv_strRPR_TCH_EML_ID As String, _
                                   ByVal bv_decHYDR_AMNT_NC As Decimal, _
                                   ByVal bv_decPNMTC_AMNT_NC As Decimal, _
                                   ByVal bv_decLBR_RT_PR_HR_NC As Decimal, _
                                   ByVal bv_decLK_TST_RT_NC As Decimal, _
                                   ByVal bv_decSRVY_ONHR_OFFHR_RT_NC As Decimal, _
                                   ByVal bv_i64PRDC_TST_TYP_ID As Int64, _
                                   ByVal bv_strVLDTY_PRD_TST_YRS As String, _
                                   ByVal bv_decMIN_HTNG_RT_NC As Decimal, _
                                   ByVal bv_decMIN_HTNG_PRD_NC As Decimal, _
                                   ByVal bv_decHRLY_CHRG_NC As Decimal, _
                                   ByVal bv_blnTRNSPRTTN_BT As Boolean, _
                                   ByVal bv_blnRNTL_BT As Boolean, _
                                   ByVal bv_blnCHK_DGT_VLDTN_BT As Boolean, _
                                   ByVal bv_blnXML_BT As Boolean, _
                                   ByVal bv_blnACTV_BT As Boolean, _
                                   ByVal bv_strServerUrl As String, _
                                   ByVal bv_strServerName As String, _
                                   ByVal bv_strPassword As String, _
                                   ByVal bv_strEdiCode As String, _
                                   ByVal FinanceIntegrationBit As Boolean, _
                                   ByVal bv_LedgerId As String, _
                                   ByVal bv_blnShell As Boolean, _
                                   ByVal bv_blnSTube As Boolean, _
                                   ByVal bv_strWfData As String)
        Try
            Dim objCustomer As New Customer
            Dim boolUpdated As Boolean

            Dim objcommon As New CommonData
            Dim dtCustomerChargeDetail As DataTable
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            End If

            Dim dsCustomerData As DataSet = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            dtCustomerChargeDetail = CType(ifgChargeDetail.DataSource, DataTable)
            If Not pvt_CheckEmptyCustomerStorageDetail(dtCustomerChargeDetail) Then
                pub_SetCallbackError("Customer Rate is a must for atleast an Equipment Code and Type")
                pub_SetCallbackStatus(False)
                Exit Sub
            End If

            If bv_strCNTCT_ADDRSS.Contains("""") Or bv_strCNTCT_ADDRSS.Contains("'") Then
                pub_SetCallbackError("Single or Double Quotes are not allowed in Contact Address.")
                pub_SetCallbackStatus(False)
            End If

            If bv_strBLLNG_ADDRSS <> Nothing Then
                If bv_strBLLNG_ADDRSS.Contains("""") Or bv_strBLLNG_ADDRSS.Contains("'") Then
                    pub_SetCallbackError("Single or Double Quotes are not allowed in Billing Address.")
                    pub_SetCallbackStatus(False)
                End If
            End If


            boolUpdated = objCustomer.pub_ModifyCustomer(CLng(bv_strCSTMR_ID), _
                                                        bv_strCSTMR_CD, _
                                                        bv_strCSTMR_NAM, _
                                                        bv_i64CSTMR_CRRNCY_ID, _
                                                        bv_i64BLLNG_TYP_ID, _
                                                        bv_i64BLK_EML_FRMT_ID, _
                                                        bv_strCNTCT_PRSN_NAM, _
                                                        bv_strCNTCT_ADDRSS, _
                                                        bv_strBLLNG_ADDRSS, _
                                                        bv_strZP_CD, _
                                                        bv_strPHN_NO, _
                                                        bv_strFX_NO, _
                                                        bv_strRPRTNG_EML_ID, _
                                                        bv_strINVCNG_EML_ID, _
                                                        bv_strRPR_TCH_EML_ID, _
                                                        bv_decHYDR_AMNT_NC, _
                                                        bv_decPNMTC_AMNT_NC, _
                                                        bv_decLBR_RT_PR_HR_NC, _
                                                        bv_decLK_TST_RT_NC, _
                                                        bv_decSRVY_ONHR_OFFHR_RT_NC, _
                                                        bv_i64PRDC_TST_TYP_ID, _
                                                        bv_strVLDTY_PRD_TST_YRS, _
                                                        bv_decMIN_HTNG_RT_NC, _
                                                        bv_decMIN_HTNG_PRD_NC, _
                                                        bv_decHRLY_CHRG_NC, _
                                                        bv_blnCHK_DGT_VLDTN_BT, _
                                                        bv_blnXML_BT, _
                                                        bv_blnTRNSPRTTN_BT, _
                                                        bv_blnRNTL_BT, _
                                                        strModifiedby, _
                                                        datModifiedDate, _
                                                        bv_blnACTV_BT, _
                                                        intDepotID, _
                                                        bv_strServerUrl, _
                                                        bv_strServerName, _
                                                        bv_strPassword, _
                                                        bv_strEdiCode, _
                                                        FinanceIntegrationBit, _
                                                        bv_LedgerId, _
                                                        bv_blnShell, _
                                                        bv_blnSTube, _
                                                        bv_strWfData, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        dsCustomer)
            dsCustomer.AcceptChanges()
            pub_SetCallbackReturnValue("Message", String.Concat("Customer : ", bv_strCSTMR_CD, " ", strMSGUPDATE))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "pvt_ValidateCustomerCode"
    Public Sub pvt_ValidateCustomerCode(ByVal bv_strCustomerCode As String)

        Try
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData
            Dim intDepotID As Int32 = CInt(objCommon.GetDepotID())
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            End If
            Dim blnValid As Boolean
            Dim strServiceType As String = String.Empty
            blnValid = objCommonUI.pub_GetServicePartnerByCode(bv_strCustomerCode, strServiceType, intDepotID)
            If blnValid Then
                pub_SetCallbackReturnValue("valid", "true")
            Else
                If strServiceType = "CUSTOMER" Then
                    pub_SetCallbackReturnValue("valid", "The code is already present for an existing Customer.")
                ElseIf strServiceType = "AGENT" Then
                    pub_SetCallbackReturnValue("valid", "The code is already present for an existing Agent.")
                Else
                    pub_SetCallbackReturnValue("valid", "The code is already present for an existing Invoicing Party.")
                End If
            End If

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgChargeDetail, tabCustomerRates)
        pub_SetGridChanges(ifgStorageDetail, tabCustomerRates)
        CommonWeb.pub_AttachHasChanges(txtCustomerCode)
        CommonWeb.pub_AttachHasChanges(txtCustomerName)
        CommonWeb.pub_AttachHasChanges(txtContactPersonName)
        CommonWeb.pub_AttachDescMaxlength(txtContactAddress)
        CommonWeb.pub_AttachDescMaxlength(txtBillingAddress)
        CommonWeb.pub_AttachHasChanges(txtZipCode)
        CommonWeb.pub_AttachHasChanges(txtPhoneNo)
        CommonWeb.pub_AttachHasChanges(txtFax)
        CommonWeb.pub_AttachDescMaxlength(txtEmailforReporting)
        CommonWeb.pub_AttachDescMaxlength(txtEmailforInvoicing)
        CommonWeb.pub_AttachDescMaxlength(txtEmailforRepairTech)
        CommonWeb.pub_AttachHasChanges(lkpBulkEmailFormat)
        CommonWeb.pub_AttachHasChanges(lkpBillingType)
        CommonWeb.pub_AttachHasChanges(lkpCustomerCurrency)
        CommonWeb.pub_AttachHasChanges(txtHydroAmount)
        CommonWeb.pub_AttachHasChanges(txtPneumaticAmount)
        CommonWeb.pub_AttachHasChanges(txtLeakTestRate)
        CommonWeb.pub_AttachHasChanges(txtSurveyRate)
        CommonWeb.pub_AttachHasChanges(lkpPeriodicTestType)
        CommonWeb.pub_AttachHasChanges(txtMinHeatingRate)
        CommonWeb.pub_AttachHasChanges(txtMinHeatingPeriod)
        CommonWeb.pub_AttachHasChanges(txtHourlyCharge)
        CommonWeb.pub_AttachHasChanges(chkCheckDigitValidationBit)
        CommonWeb.pub_AttachHasChanges(chkActiveBit)
        CommonWeb.pub_AttachHasChanges(txtLaborRateperHour)
        CommonWeb.pub_AttachHasChanges(txtHndlng_Tx_Rt)
        CommonWeb.pub_AttachHasChanges(txtStorage_Tx_Rt)
        CommonWeb.pub_AttachHasChanges(txtSrvc_Tx_Rt)
        bln_050EqType_Key = objCommonConfig.IsKeyExists
        Dim objCommon As New CommonData
        Dim intDepotID As Integer = objCommon.GetDepotID()
        Dim objCommonData As New CommonData
        str_050EqType = objCommonData.GetConfigSetting("050", bln_050EqType_Key)
        If bln_050EqType_Key Then
            If str_050EqType.ToLower <> "true" Then
                CommonWeb.pub_AttachHasChanges(chkTransportationBit)
                CommonWeb.pub_AttachHasChanges(chkRentalBit)
                pub_SetGridChanges(ifgCustomerTransportation, tabCustomerRates)
                pub_SetGridChanges(ifgCustomerRental, tabCustomerRates)
                CommonWeb.pub_AttachHasChanges(chkXML)
                CommonWeb.pub_AttachHasChanges(txtLedgerId)
                CommonWeb.pub_AttachHasChanges(chkShell)
                CommonWeb.pub_AttachHasChanges(ChkSTube)
                CommonWeb.pub_AttachHasChanges(txtFtpServer)
                CommonWeb.pub_AttachHasChanges(txtFtpUserName)
                CommonWeb.pub_AttachHasChanges(txtPassword)
            End If
        End If
    End Sub
#End Region

#Region "pvt_CheckEmptyTarrifDetail"
    Private Function pvt_CheckEmptyCustomerStorageDetail(ByVal bv_dtCustomerChargeDetail As DataTable) As Boolean
        Dim intCustomerChargeDetailID As Long
        Dim dr() As DataRow
        Try
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            If Not bv_dtCustomerChargeDetail Is Nothing Then
                For Each drStorage As DataRow In bv_dtCustomerChargeDetail.Rows
                    If drStorage.RowState = DataRowState.Added Then
                        intCustomerChargeDetailID = CLng(drStorage.Item(CustomerData.CSTMR_CHRG_DTL_ID))
                        dr = dsCustomer.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL).Select(String.Concat(CustomerData.CSTMR_CHRG_DTL_ID, "= ", intCustomerChargeDetailID))
                        If dr.Length = 0 Then
                            Return False
                        End If
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                               MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/Customer.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgCustomerTransportation_ClientBind"
    Protected Sub ifgCustomerTransportation_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgCustomerTransportation.ClientBind
        Try
            Dim objCustomer As New Customer
            Dim dtCustomerTransportation As New DataTable
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            If Not e.Parameters("CustomerID") Is Nothing Then
                dtCustomerTransportation = objCustomer.pub_GetCustomerTransportationByCustomerId(CLng(e.Parameters("CustomerID"))).Tables(CustomerData._V_CUSTOMER_TRANSPORTATION)
            End If
            dsCustomer.Tables(CustomerData._V_CUSTOMER_TRANSPORTATION).Merge(dtCustomerTransportation)
            e.DataSource = dsCustomer.Tables(CustomerData._V_CUSTOMER_TRANSPORTATION)
            CacheData(CUSTOMER, dsCustomer)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateRouteCode"
    Private Sub pvt_ValidateRouteCode(ByVal bv_strRouteCode As String, _
                                      ByVal bv_intGridIndex As Integer, _
                                      ByVal bv_strRowstate As String, _
                                      ByVal bv_strCustomerID As String)
        Try
            Dim blndsValid As Boolean
            Dim dtCustomerTransportation As New DataTable
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            dtCustomerTransportation = dsCustomer.Tables(CustomerData._V_CUSTOMER_TRANSPORTATION)
            Dim intResultIndex() As System.Data.DataRow = dtCustomerTransportation.Select(String.Concat(CustomerData.RT_CD, "='", bv_strRouteCode, "' "))
            Dim strExistRoute As String = ""
            If intResultIndex.Length > 0 Then
                If dtCustomerTransportation.Rows.Count > bv_intGridIndex Then
                    If dtCustomerTransportation.Rows(bv_intGridIndex).RowState = Data.DataRowState.Deleted Then
                        strExistRoute = String.Empty
                    ElseIf dtCustomerTransportation.Rows(bv_intGridIndex).RowState <> Data.DataRowState.Deleted Then
                        strExistRoute = dtCustomerTransportation.Rows(bv_intGridIndex)(CustomerData.RT_CD).ToString
                    End If
                End If

                If bv_strRouteCode = strExistRoute Then
                    blndsValid = True
                Else
                    blndsValid = False
                End If
            Else
                blndsValid = True
            End If
            'Checking whether the entered code is available in database
            'If blndsValid = True Then
            '    If bv_strRowstate = "Added" Or bv_strRowstate = "Modified" Then
            '        Dim objCustomer As New Customer
            '        Dim objCommon As New CommonData
            '        blndsValid = objCustomer.pub_ValidateRouteIdByRouteCode(bv_strRouteCode, bv_strCustomerID)
            '    End If
            'End If

            If blndsValid = True Then
                pub_SetCallbackReturnValue("bNotExists", "true")
            Else
                pub_SetCallbackReturnValue("bNotExists", "false")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateContractNo"
    Private Sub pvt_ValidateContractNo(ByVal bv_strContractNo As String, _
                                       ByVal bv_intGridIndex As Integer, _
                                       ByVal bv_strRowstate As String, _
                                       ByVal bv_strCustomerID As String)
        Try
            Dim blndsValid As Boolean
            Dim dtCustomerRental As New DataTable
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            dtCustomerRental = dsCustomer.Tables(CustomerData._V_CUSTOMER_RENTAL)
            Dim intResultIndex() As System.Data.DataRow = dtCustomerRental.Select(String.Concat(CustomerData.CNTRCT_RFRNC_NO, "='", bv_strContractNo, "' "))
            Dim strExistRoute As String = ""
            If intResultIndex.Length > 0 Then
                If dtCustomerRental.Rows.Count > bv_intGridIndex Then
                    If dtCustomerRental.Rows(bv_intGridIndex).RowState = Data.DataRowState.Deleted Then
                        strExistRoute = String.Empty
                    ElseIf dtCustomerRental.Rows(bv_intGridIndex).RowState <> Data.DataRowState.Deleted Then
                        strExistRoute = dtCustomerRental.Rows(bv_intGridIndex)(CustomerData.CNTRCT_RFRNC_NO).ToString
                    End If
                End If

                If bv_strContractNo = strExistRoute Then
                    blndsValid = True
                Else
                    blndsValid = False
                End If
            Else
                blndsValid = True
            End If
            'Checking whether the entered code is available in database
            If blndsValid = True Then
                If bv_strRowstate = "Added" Or bv_strRowstate = "Modified" Then
                    Dim objCustomer As New Customer
                    Dim objCommon As New CommonData
                    blndsValid = objCustomer.pub_ValidateContractNoByContractNo(bv_strContractNo)
                End If
            End If

            If blndsValid = True Then
                pub_SetCallbackReturnValue("bNotExists", "true")
            Else
                pub_SetCallbackReturnValue("bNotExists", "false")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgCustomerTransportation_RowDataBound"
    Protected Sub ifgCustomerTransportation_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgCustomerTransportation.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowIndex > 2 Then
                    Dim lkpControl As iLookup
                    lkpControl = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpControl.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCustomerTransportation_RowInserting"
    Protected Sub ifgCustomerTransportation_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgCustomerTransportation.RowInserting
        Try
            Dim lngCustomerTransportationId As Long
            Dim objCommon As New CommonData
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            lngCustomerTransportationId = CommonWeb.GetNextIndex(dsCustomer.Tables(CustomerData._V_CUSTOMER_TRANSPORTATION), CustomerData.CSTMR_TRNSPRTTN_ID)
            e.Values(CustomerData.CSTMR_TRNSPRTTN_ID) = lngCustomerTransportationId

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCustomerRental_ClientBind"
    Protected Sub ifgCustomerRental_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgCustomerRental.ClientBind
        Try
            Dim objCustomer As New Customer
            Dim dtCustomerRental As New DataTable
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            If Not e.Parameters("CustomerID") Is Nothing Then
                dtCustomerRental = objCustomer.pub_GetCustomerRentalByCustomerId(CLng(e.Parameters("CustomerID"))).Tables(CustomerData._V_CUSTOMER_RENTAL)
            End If
            dsCustomer.Tables(CustomerData._V_CUSTOMER_RENTAL).Merge(dtCustomerRental)
            e.DataSource = dsCustomer.Tables(CustomerData._V_CUSTOMER_RENTAL)
            CacheData(CUSTOMER, dsCustomer)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCustomerRental_RowDataBound"
    Protected Sub ifgCustomerRental_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgCustomerRental.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim objCustomer As New Customer
                Dim strIsGateOut As String = String.Empty
                If Not drv Is Nothing Then
                    If CInt(drv.Item(CustomerData.GTOT_BT)) = 1 Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    Else
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                    End If
                End If

            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCustomerRental_RowInserting"
    Protected Sub ifgCustomerRental_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgCustomerRental.RowInserting
        Try
            Dim lngCustomerRentalId As Long
            Dim objCommon As New CommonData
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            lngCustomerRentalId = CommonWeb.GetNextIndex(dsCustomer.Tables(CustomerData._V_CUSTOMER_RENTAL), CustomerData.CSTMR_RNTL_ID)
            e.Values(CustomerData.CSTMR_RNTL_ID) = lngCustomerRentalId
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgChargeDetail_RowDataBound"
    Protected Sub ifgChargeDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgChargeDetail.RowDataBound
        Try
            Dim objCommonData As New CommonData
            str_050EqType = objCommonData.GetConfigSetting("050", bln_050EqType_Key)
            If bln_050EqType_Key Then
                Dim objCommon As New CommonData
                If str_050EqType.ToLower <> "true" Then
                    'ifgChargeDetail.Columns.Item(5).ControlStyle.CssClass = "hide"
                    'ifgChargeDetail.Columns.Item(5).ItemStyle.CssClass = "hide"
                    'ifgChargeDetail.Columns.Item(5).HeaderStyle.CssClass = "hide"
                    ifgChargeDetail.Columns.Item(5).Visible = False
                End If
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                If Not e.Row.DataItem Is Nothing Then
                    drv = CType(e.Row.DataItem, Data.DataRowView)
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCustomerRental_RowDeleting"
    Protected Sub ifgCustomerRental_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgCustomerRental.RowDeleting
        Try
            Dim dtCustomerRental As New DataTable
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            dtCustomerRental = dsCustomer.Tables(CustomerData._V_CUSTOMER_RENTAL).Copy()
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgCustomerRental.PageSize * ifgCustomerRental.PageIndex + e.RowIndex

            Dim drChkDefault As DataRow()
            drChkDefault = dsCustomer.Tables(CustomerData._V_CUSTOMER_RENTAL).Select(String.Concat(CustomerData.CSTMR_RNTL_ID, "=", e.Keys.Item(CustomerData.CSTMR_RNTL_ID), " AND ", CustomerData.GTOT_BT, "=1"))
            If drChkDefault.Length > 0 Then
                e.Cancel = True
                e.OutputParamters("CheckDefault") = String.Concat("Rental : Contract Reference No ", e.Values(CustomerData.CNTRCT_RFRNC_NO), "  cannot be deleted.")
            Else
                e.OutputParamters("Delete") = String.Concat("Rental : Contract Reference No ", dtCustomerRental.Rows(intRowIndex).Item(CustomerData.CNTRCT_RFRNC_NO).ToString, " has been deleted from Customer(" + e.InputParamters("CustomerName").ToString + ")"" . Click submit to save changes.")
            End If

            'If CInt(e.Values(CustomerData.GTOT_BT)) = 1 Then
            '    e.OutputParamters("Delete") = String.Concat("Customer > Rental : Contract Reference No ", e.Values(CustomerData.CNTRCT_RFRNC_NO), "  cannot be deleted.")
            '    Exit Sub
            'End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_HideMenuGWS"
    Private Function pvt_HideMenuGWS(ByVal strKeyValue As String) As Boolean
        Try
            If strKeyValue <> Nothing AndAlso strKeyValue.ToLower = "true" Then
                'chkRentalBit.Checked = False
                chkRentalBit.Visible = False
                chkRentalBit.Enabled = False
                'chkTransportationBit.Checked = False
                chkTransportationBit.Enabled = False
                chkTransportationBit.Visible = False
                lblRentalBit.Visible = False
                lblTransportationBit.Visible = False
                'chkXML.Checked = False
                chkXML.Enabled = False
                chkXML.Visible = False
                lblXmlBit.Enabled = False
                lblXmlBit.Visible = False
                lblLedgerId.Visible = False
                txtLedgerId.Enabled = False
                txtLedgerId.Visible = False
                lblLedgerIdReq.Visible = False
                'lblEDICode.Text = "Customer VAT No"
                'txtEdiCode.Visible = False
                'lblAgentCode.Enabled = True
                tabStandardRates.Visible = False
                'tabRental.Visible = False
                'tabTransportation.Visible = False
                'tabFtp.Visible = False
            ElseIf strKeyValue.ToLower = "false" Then
                lblCustVatNo.Visible = False
                txtCustVatNo.Visible = False
                lblAgent.Visible = False
                lblHandlingTaxRate.Visible = False
                lblLaborRate.Visible = False
                lblLbrRtReq.Visible = False
                txtLaborRate.Visible = False
                txtHndlng_Tx_Rt.Visible = False
                txtSrvc_Tx_Rt.Visible = False
                lblServiceTaxRate.Visible = False
                lblStorageTaxRate.Visible = False
                txtStorage_Tx_Rt.Visible = False
                lblTaxes.Visible = False
                lkpAgent.Visible = False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "pv_CreateCustomerGWS"
    Private Function pv_CreateCustomerGWS(ByVal bv_strCSTMR_CD As String, _
                                       ByVal bv_strCSTMR_NAM As String, _
                                       ByVal bv_i64CSTMR_CRRNCY_ID As Int64, _
                                       ByVal bv_i64BLLNG_TYP_ID As Int64, _
                                       ByVal bv_i64BLK_EML_FRMT_ID As Int64, _
                                       ByVal bv_strCNTCT_PRSN_NAM As String, _
                                       ByVal bv_strCNTCT_ADDRSS As String, _
                                       ByVal bv_strBLLNG_ADDRSS As String, _
                                       ByVal bv_strZP_CD As String, _
                                       ByVal bv_strPHN_NO As String, _
                                       ByVal bv_strFX_NO As String, _
                                       ByVal bv_strRPRTNG_EML_ID As String, _
                                       ByVal bv_strINVCNG_EML_ID As String, _
                                       ByVal bv_strRPR_TCH_EML_ID As String, _
                                       ByVal bv_blnCHK_DGT_VLDTN_BT As Boolean, _
                                       ByVal bv_blnACTV_BT As Boolean, _
                                       ByVal str_EDICode As String, _
                                       ByVal bv_strCustVatNo As String, _
                                       ByVal bv_strAgent As String, _
                                       ByVal bv_strStorageTax As String, _
                                       ByVal bv_strServiceTax As String, _
                                       ByVal bv_strHandlingTax As String, _
                                       ByVal bv_strLaborRate As String, _
                                       ByVal bv_strWfData As String) As Long
        Dim objcommon As New CommonData
        Try
            Dim objCustomer As New Customer
            Dim lngCreated As Long
            Dim dtCustomerChargeDetail As DataTable
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            End If

            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            dtCustomerChargeDetail = CType(ifgChargeDetail.DataSource, DataTable)

            If dtCustomerChargeDetail.Rows.Count = 0 Then
                pub_SetCallbackError("Customer Rate is a must for atleast an Equipment Code and Type.")
                pub_SetCallbackStatus(False)
                Return 0
            End If

            If Not pvt_CheckEmptyCustomerStorageDetail(dtCustomerChargeDetail.GetChanges()) Then
                pub_SetCallbackError("Storage Charge is a must for selected Equipment Code and Type.")
                pub_SetCallbackStatus(False)
                Return 0
            End If

            If bv_strCNTCT_ADDRSS.Contains("""") Or bv_strCNTCT_ADDRSS.Contains("'") Then
                pub_SetCallbackError("Single or Double Quotes are not allowed in Billing Address.")
                pub_SetCallbackStatus(False)
                Return 0
            End If

            If bv_strBLLNG_ADDRSS <> Nothing Then
                If bv_strBLLNG_ADDRSS.Contains("""") Or bv_strBLLNG_ADDRSS.Contains("'") Then
                    pub_SetCallbackError("Single or Double Quotes are not allowed in Billing Address.")
                    pub_SetCallbackStatus(False)
                    Return 0
                End If
            End If

            lngCreated = objCustomer.pub_CreateCustomer((bv_strCSTMR_CD), _
                                                         bv_strCSTMR_NAM, _
                                                         bv_i64CSTMR_CRRNCY_ID, _
                                                         bv_i64BLLNG_TYP_ID, _
                                                         bv_i64BLK_EML_FRMT_ID, _
                                                         bv_strCNTCT_PRSN_NAM, _
                                                         bv_strCNTCT_ADDRSS, _
                                                         bv_strBLLNG_ADDRSS, _
                                                         bv_strZP_CD, _
                                                         bv_strPHN_NO, _
                                                         bv_strFX_NO, _
                                                         bv_strRPRTNG_EML_ID, _
                                                         bv_strINVCNG_EML_ID, _
                                                         bv_strRPR_TCH_EML_ID, _
                                                         Nothing, _
                                                         Nothing, _
                                                         CDec(bv_strLaborRate), _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         bv_blnCHK_DGT_VLDTN_BT, _
                                                         Nothing, _
                                                         strModifiedby, _
                                                         datModifiedDate, _
                                                         strModifiedby, _
                                                         datModifiedDate, _
                                                         bv_blnACTV_BT, _
                                                         intDepotID, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         str_EDICode, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         Nothing, _
                                                         bv_strWfData, _
                                                         bv_strCustVatNo, _
                                                         bv_strAgent, _
                                                         CDec(bv_strStorageTax), _
                                                         CDec(bv_strServiceTax), _
                                                         CDec(bv_strHandlingTax), _
                                                         CDec(bv_strLaborRate), _
                                                         dsCustomer)

            dsCustomer.AcceptChanges()
            pub_SetCallbackReturnValue("ID", CStr(lngCreated))
            pub_SetCallbackReturnValue("Message", String.Concat("Customer : ", bv_strCSTMR_CD, " ", strMSGINSERT))
            pub_SetCallbackStatus(True)
            Return lngCreated
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "pvt_UpdateCustomerGWS"
    Private Sub pvt_UpdateCustomerGWS(ByVal bv_strCSTMR_ID As String, _
                                   ByVal bv_strCSTMR_CD As String, _
                                   ByVal bv_strCSTMR_NAM As String, _
                                   ByVal bv_i64CSTMR_CRRNCY_ID As Int64, _
                                   ByVal bv_i64BLLNG_TYP_ID As Int64, _
                                   ByVal bv_i64BLK_EML_FRMT_ID As Int64, _
                                   ByVal bv_strCNTCT_PRSN_NAM As String, _
                                   ByVal bv_strCNTCT_ADDRSS As String, _
                                   ByVal bv_strBLLNG_ADDRSS As String, _
                                   ByVal bv_strZP_CD As String, _
                                   ByVal bv_strPHN_NO As String, _
                                   ByVal bv_strFX_NO As String, _
                                   ByVal bv_strRPRTNG_EML_ID As String, _
                                   ByVal bv_strINVCNG_EML_ID As String, _
                                   ByVal bv_strRPR_TCH_EML_ID As String, _
                                   ByVal bv_blnCHK_DGT_VLDTN_BT As Boolean, _
                                   ByVal bv_blnACTV_BT As Boolean, _
                                   ByVal bv_strEdiCode As String, _
                                   ByVal bv_strCustVatNo As String, _
                                   ByVal bv_strAgent As String, _
                                   ByVal bv_strStorageTax As String, _
                                   ByVal bv_strServiceTax As String, _
                                   ByVal bv_strHandlingTax As String, _
                                   ByVal bv_strLaborRate As String, _
                                   ByVal bv_strWfData As String)
        Try
            Dim objCustomer As New Customer
            Dim boolUpdated As Boolean

            Dim objcommon As New CommonData
            Dim dtCustomerChargeDetail As DataTable
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            Dim intDepotID As Integer
            If objcommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = CInt(objcommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            End If

            Dim dsCustomerData As DataSet = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            dtCustomerChargeDetail = CType(ifgChargeDetail.DataSource, DataTable)
            If Not pvt_CheckEmptyCustomerStorageDetail(dtCustomerChargeDetail) Then
                pub_SetCallbackError("Customer Rate is a must for atleast an Equipment Code and Type")
                pub_SetCallbackStatus(False)
                Exit Sub
            End If

            If bv_strCNTCT_ADDRSS.Contains("""") Or bv_strCNTCT_ADDRSS.Contains("'") Then
                pub_SetCallbackError("Single or Double Quotes are not allowed in Contact Address.")
                pub_SetCallbackStatus(False)
            End If

            If bv_strBLLNG_ADDRSS <> Nothing Then
                If bv_strBLLNG_ADDRSS.Contains("""") Or bv_strBLLNG_ADDRSS.Contains("'") Then
                    pub_SetCallbackError("Single or Double Quotes are not allowed in Billing Address.")
                    pub_SetCallbackStatus(False)
                End If
            End If


            boolUpdated = objCustomer.pub_ModifyCustomer(CLng(bv_strCSTMR_ID), _
                                                        bv_strCSTMR_CD, _
                                                        bv_strCSTMR_NAM, _
                                                        bv_i64CSTMR_CRRNCY_ID, _
                                                        bv_i64BLLNG_TYP_ID, _
                                                        bv_i64BLK_EML_FRMT_ID, _
                                                        bv_strCNTCT_PRSN_NAM, _
                                                        bv_strCNTCT_ADDRSS, _
                                                        bv_strBLLNG_ADDRSS, _
                                                        bv_strZP_CD, _
                                                        bv_strPHN_NO, _
                                                        bv_strFX_NO, _
                                                        bv_strRPRTNG_EML_ID, _
                                                        bv_strINVCNG_EML_ID, _
                                                        bv_strRPR_TCH_EML_ID, _
                                                        Nothing, _
                                                        Nothing, _
                                                        CDec(bv_strLaborRate), _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        bv_blnCHK_DGT_VLDTN_BT, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        strModifiedby, _
                                                        datModifiedDate, _
                                                        bv_blnACTV_BT, _
                                                        intDepotID, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        bv_strEdiCode, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Nothing, _
                                                        bv_strWfData, _
                                                        bv_strCustVatNo, _
                                                        bv_strAgent, _
                                                        CDec(bv_strStorageTax), _
                                                        CDec(bv_strServiceTax), _
                                                        CDec(bv_strHandlingTax), _
                                                        dsCustomer)
            dsCustomer.AcceptChanges()
            pub_SetCallbackReturnValue("Message", String.Concat("Customer : ", bv_strCSTMR_CD, " ", strMSGUPDATE))
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try


    End Sub
#End Region

    Protected Sub ifgChargeDetail_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgChargeDetail.RowInserted
        Try
            CacheData(CUSTOMER, dsCustomer)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ifgChargeDetail_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgChargeDetail.RowUpdated
        Try

        Catch ex As Exception

        End Try
    End Sub
#Region "ifgList_Expanded"
    Protected Sub ifgList_Expanded(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridExpandedEventArgs) Handles ifgList.Expanded
        Try
            Dim dv As New DataView
            Dim objCustomer As New Customer
            Dim dsCustomerRatesEnum As New CustomerDataSet
            Dim objCommon As New CommonData
            Dim bln_073EqType_Key As Boolean
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)

            dsCustomerRatesEnum = objCustomer.pub_GetEnumOnCustomerRates()
            If objCommon.GetConfigSetting("073", bln_073EqType_Key).ToLower = "true" Then
                dv = dsCustomerRatesEnum.Tables(CustomerData._ENUM).DefaultView
            Else
                dv = dsCustomerRatesEnum.Tables(CustomerData._ENUM).Select("ENM_ID = 157").CopyToDataTable.DefaultView
            End If

            dsCustomer.Tables(CustomerData._ENUM).Clear()
            dsCustomer.Tables(CustomerData._ENUM).Merge(dv.ToTable)
            e.DataSource = dv
            CacheData(CUSTOMER, dsCustomer)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region


    Protected Sub ifgStorageDetail_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgStorageDetail.RowInserted

    End Sub

    Protected Sub ifgStorageDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgStorageDetail.RowDataBound
        Try
            Dim rowIndex As String = RetrieveData("RowIndex")
            If rowIndex = 0 Then
                ifgStorageDetail.Columns().Item(2).Visible = False
                ifgStorageDetail.Columns().Item(3).Visible = False
            ElseIf rowIndex = 1 Then
                ifgStorageDetail.Columns().Item(0).Visible = False
                ifgStorageDetail.Columns().Item(1).Visible = False
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

    Protected Sub ifgStorageDetail_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgStorageDetail.RowUpdating
        Try
            Dim rowIndex As String = RetrieveData("RowIndex")
            dsCustomer = CType(RetrieveData(CUSTOMER), CustomerDataSet)
            If rowIndex = 1 Then
                e.NewValues(CustomerData.CSTMR_STRG_DTL_ID) = e.OldValues(CustomerData.CSTMR_STRG_DTL_ID)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ifgStorageDetail_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgStorageDetail.RowUpdated

    End Sub

    Protected Sub ifgStorageDetail_RowDeleted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeletedEventArgs) Handles ifgStorageDetail.RowDeleted

    End Sub
End Class
