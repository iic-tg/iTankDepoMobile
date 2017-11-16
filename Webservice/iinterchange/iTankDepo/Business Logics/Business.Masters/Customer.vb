Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class Customer
    Inherits CodeBase

#Region "pub_GetV_CustomerByCstmr_Id() TABLE NAME:Customer"
    <OperationContract()>
    Public Function pub_GetV_CustomerByCstmr_Id(ByVal bv_intCSTMR_ID As Integer, ByVal bv_strWFDATA As String) As CustomerDataSet

        Try
            Dim objCustomer As CustomerDataSet
            Dim obCustomers As New Customers
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            objCustomer = obCustomers.GetV_CustomerByCstmr_Id(bv_intCSTMR_ID, intDepotID)
            Return objCustomer
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerTransportationByCustomerId() TABLE NAME:CUSTOMER_TRANSPORTATION"

    <OperationContract()> _
    Public Function pub_GetCustomerTransportationByCustomerId(ByVal bv_i64CustomerId As Int64) As CustomerDataSet

        Try
            Dim dsCustomerTransportationData As CustomerDataSet
            Dim objCustomerTransportations As New Customers
            dsCustomerTransportationData = objCustomerTransportations.GetCustomerTransportationByCustomerId(bv_i64CustomerId)
            Return dsCustomerTransportationData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerRentalByCustomerId() TABLE NAME:CUSTOMER_RENTAL"

    <OperationContract()> _
    Public Function pub_GetCustomerRentalByCustomerId(ByVal bv_i64CustomerId As Int64) As CustomerDataSet

        Try
            Dim dsCustomerTransportationData As CustomerDataSet
            Dim objCustomerTransportations As New Customers
            dsCustomerTransportationData = objCustomerTransportations.GetCustomerRentalByCustomerId(bv_i64CustomerId)
            Return dsCustomerTransportationData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ValidateRouteIdByRouteCode"
    <OperationContract()> _
    Public Function pub_ValidateRouteIdByRouteCode(ByVal bv_strRouteCode As String, ByVal bv_strCustomerID As String) As Boolean

        Try
            Dim objCustomers As New Customers
            Dim intRowCount As Integer
            intRowCount = CInt(objCustomers.ValidateRouteIdByRouteCode(bv_strRouteCode, bv_strCustomerID))
            If intRowCount > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ValidateRouteIdByRouteCode"
    <OperationContract()> _
    Public Function pub_ValidateContractNoByContractNo(ByVal bv_strContractNo As String) As Boolean
        Try
            Dim objCustomers As New Customers
            Dim intRowCount As Integer
            intRowCount = CInt(objCustomers.pub_ValidateContractNoByContractNo(bv_strContractNo))
            If intRowCount > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateCustomer() TABLE NAME:Customer"
    <OperationContract()> _
    Public Function pub_CreateCustomer(ByVal bv_strCustomerCode As String, _
                                       ByVal bv_strCustomerName As String, _
                                       ByVal bv_i64CustomerCurrencyID As Int64, _
                                       ByVal bv_i64BillingType As Int64, _
                                       ByVal bv_i64BulkEmailFormatID As Int64, _
                                       ByVal bv_strContactPersonName As String, _
                                       ByVal bv_strContactAddress As String, _
                                       ByVal bv_strBillingAddress As String, _
                                       ByVal bv_strZipCode As String, _
                                       ByVal bv_strPhoneNumber As String, _
                                       ByVal bv_strFaxNumber As String, _
                                       ByVal bv_strReportingEmailID As String, _
                                       ByVal bv_strInvoicingEmailID As String, _
                                       ByVal bv_strRepairTechEmailID As String, _
                                       ByVal bv_decHYDR_AMNT_NC As Decimal, _
                                       ByVal bv_decPNMTC_AMNT_NC As Decimal, _
                                       ByVal bv_decLBR_RT_PR_HR_NC As Decimal, _
                                       ByVal bv_decLK_TST_RT_NC As Decimal, _
                                       ByVal bv_decSRVY_ONHR_OFFHR_RT_NC As Decimal, _
                                       ByVal bv_i64PRDC_TST_TYP_ID As Int64, _
                                       ByVal bv_strVLDTY_PRD_TST_YRS As String, _
                                       ByVal bv_strMinHeatingRate As Decimal, _
                                       ByVal bv_strMinHeatingPeriod As Decimal, _
                                       ByVal bv_strHourlyCharge As Decimal, _
                                       ByVal bv_blnTransportationBit As Boolean, _
                                       ByVal bv_blnRentalBit As Boolean, _
                                       ByVal bv_blnCheckDigitValidationBit As Boolean, _
                                       ByVal bv_blnXML_BT As Boolean, _
                                       ByVal bv_strCreatedBy As String, _
                                       ByVal bv_datCreatedDate As DateTime, _
                                       ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_blnActiveBit As Boolean, _
                                       ByVal bv_i32DepotID As Int32, _
                                       ByVal bv_strServerUrl As String, _
                                       ByVal bv_strServerName As String, _
                                       ByVal bv_strPassword As String, _
                                       ByVal bv_strEdiCode As String, _
                                       ByVal FinanceIntegrationBit As Boolean, _
                                       ByVal bv_LedgerId As String, _
                                       ByVal bv_blnShell As Boolean, _
                                       ByVal bv_blnSTube As Boolean, _
                                       ByVal bv_strWfData As String, _
                                       ByVal bv_strCustVatNo As String, _
                                       ByVal bv_strAgent As String, _
                                       ByVal bv_strStorageTax As String, _
                                       ByVal bv_strServiceTax As String, _
                                       ByVal bv_strHandlingTax As String, _
                                       ByVal bv_strLaborRate As String, _
                                       ByRef br_dsCustomer As CustomerDataSet) As Long
        Dim objTransaction As New Transactions()

        Try
            Dim objCustomer As New Customers
            Dim objCommonUI As New CommonUIs
            Dim lngCreated As Long
            bv_blnTransportationBit = CBool(IIf(bv_blnTransportationBit = Nothing, False, True))
            bv_blnRentalBit = CBool(IIf(bv_blnRentalBit = Nothing, False, True))
            lngCreated = objCustomer.CreateCustomer(bv_strCustomerCode, _
                                                    bv_strCustomerName, _
                                                    bv_i64CustomerCurrencyID, _
                                                    bv_i64BillingType, _
                                                    bv_i64BulkEmailFormatID, _
                                                    bv_strContactPersonName, _
                                                    bv_strContactAddress, _
                                                    bv_strBillingAddress, _
                                                    bv_strZipCode, _
                                                    bv_strPhoneNumber, _
                                                    bv_strFaxNumber, _
                                                    bv_strReportingEmailID, _
                                                    bv_strInvoicingEmailID, _
                                                    bv_strRepairTechEmailID, _
                                                    bv_decHYDR_AMNT_NC, _
                                                    bv_decPNMTC_AMNT_NC, _
                                                    bv_decLBR_RT_PR_HR_NC, _
                                                    bv_decLK_TST_RT_NC, _
                                                    bv_decSRVY_ONHR_OFFHR_RT_NC, _
                                                    bv_i64PRDC_TST_TYP_ID, _
                                                    bv_strVLDTY_PRD_TST_YRS, _
                                                    bv_strMinHeatingRate, _
                                                    bv_strMinHeatingPeriod, _
                                                    bv_strHourlyCharge, _
                                                    bv_blnCheckDigitValidationBit, _
                                                    bv_blnTransportationBit, _
                                                    bv_blnRentalBit, _
                                                    bv_strCreatedBy, _
                                                    bv_datCreatedDate, _
                                                    bv_strModifiedBy, _
                                                    bv_datModifiedDate, _
                                                    bv_blnActiveBit, _
                                                    bv_i32DepotID, _
                                                    bv_blnXML_BT, _
                                                    bv_strServerUrl, _
                                                    bv_strServerName, _
                                                    bv_strPassword, _
                                                    bv_strEdiCode, _
                                                    FinanceIntegrationBit, _
                                                    bv_LedgerId, _
                                                    bv_blnShell, _
                                                    bv_blnSTube, _
                                                    bv_strCustVatNo, _
                                                    bv_strAgent, _
                                                    bv_strStorageTax, _
                                                    bv_strServiceTax, _
                                                    bv_strHandlingTax, _
                                                    objTransaction)

            objCommonUI.CreateServicePartner(lngCreated, "CUSTOMER", bv_i32DepotID, objTransaction)

            pub_UpdateChargeDetail(br_dsCustomer, CLng(lngCreated), objTransaction)

            For Each drEmail As DataRow In br_dsCustomer.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).Rows

                Dim intPRDC_DY_ID As Integer = 0
                Dim intPRDC_DT_ID As Integer = 0
                Dim strCC_EML As String = String.Empty
                Dim intPRDC_FLTR_ID As Integer = CInt(drEmail.Item(CustomerData.PRDC_FLTR_ID))
                Dim strGNRTN_TM As String = CStr(drEmail.Item(CustomerData.GNRTN_TM))
                Dim dtNXT_RN_DT_TM As Date = Nothing
                Dim dtLstRunUpdate As Date = Nothing
                Dim strPRDC_DY_CD As String = String.Empty

                If Not IsDBNull(drEmail.Item(CustomerData.PRDC_DT_ID)) Then
                    intPRDC_DT_ID = CInt(drEmail.Item(CustomerData.PRDC_DT_ID))
                End If
                If Not IsDBNull(drEmail.Item(CustomerData.PRDC_DY_ID)) Then
                    intPRDC_DY_ID = CInt(drEmail.Item(CustomerData.PRDC_DY_ID))
                    strPRDC_DY_CD = CStr(drEmail.Item(CustomerData.PRDC_DY_CD))
                End If
                If Not IsDBNull(drEmail.Item(CustomerData.CC_EML)) Then
                    strCC_EML = CStr(drEmail.Item(CustomerData.CC_EML))
                End If

                dtNXT_RN_DT_TM = pub_getNextRunDateTime(intPRDC_FLTR_ID, strGNRTN_TM, intPRDC_DT_ID, strPRDC_DY_CD)
                dtLstRunUpdate = CDate(System.DateTime.Now.Date & " " & strGNRTN_TM)
                'CInt(drEmail.Item(CustomerData.CSTMR_ID))lngCreated
                objCustomer.CreateEmailSetting(lngCreated, _
                                               CInt(drEmail.Item(CustomerData.RPRT_ID)), _
                                               strGNRTN_TM, _
                                               CStr(drEmail.Item(CustomerData.TO_EML)), _
                                               strCC_EML, _
                                               CStr(drEmail.Item(CustomerData.SBJCT_VCR)), _
                                               intPRDC_DT_ID, _
                                               intPRDC_DY_ID, _
                                               intPRDC_FLTR_ID, _
                                               CBool(drEmail.Item(CustomerData.ACTV_BT)), _
                                               dtNXT_RN_DT_TM, _
                                               dtLstRunUpdate, _
                                               objTransaction)

            Next

            For Each drCustomerTransportation As DataRow In br_dsCustomer.Tables(CustomerData._V_CUSTOMER_TRANSPORTATION).Rows
                Dim strRouteId As String = ""
                Dim decEmptyRate As Decimal = 0
                Dim decFullRate As Decimal = 0
                If Not IsDBNull(drCustomerTransportation.Item(CustomerData.EMPTY_TRP_RT_NC)) Then
                    decEmptyRate = CDec(drCustomerTransportation.Item(CustomerData.EMPTY_TRP_RT_NC))
                    If Not IsDBNull(drCustomerTransportation.Item(CustomerData.FLL_TRP_RT_NC)) Then
                        decFullRate = CDec(drCustomerTransportation.Item(CustomerData.FLL_TRP_RT_NC))
                    End If
                    If drCustomerTransportation.RowState <> DataRowState.Deleted Then
                        strRouteId = objCustomer.GetRouteIdByRouteCode(CStr(drCustomerTransportation.Item(CustomerData.RT_CD)), bv_i32DepotID)
                    End If
                End If

                objCustomer.CreateCustomerTransportation(lngCreated, _
                                                         CLng(strRouteId), _
                                                         decEmptyRate, _
                                                         decFullRate, _
                                                         objTransaction)

            Next
            For Each drCustomerRental As DataRow In br_dsCustomer.Tables(CustomerData._V_CUSTOMER_RENTAL).Rows
                Dim decHandlingOut As Decimal = 0
                Dim decHandlingIn As Decimal = 0
                Dim decOnHireSurvey As Decimal = 0
                Dim decOffHireSurvey As Decimal = 0
                Dim datContratStart As DateTime = Nothing
                Dim datContractEnd As DateTime = Nothing
                Dim intTenureDays As Int64 = 0
                Dim dblRentalPerDay As Double = 0
                Dim strContractReferenceNo As String = String.Empty
                Dim strRemarks As String = String.Empty
                Dim strSupplierId As String = ""
                If drCustomerRental.RowState <> DataRowState.Deleted Then
                    '  strSupplierId = objCustomer.GetSupplierIdByContractNo(CStr(drCustomerRental.Item(CustomerData.CNTRCT_RFRNC_NO)))

                    If Not IsDBNull(drCustomerRental.Item(CustomerData.HNDLNG_OT)) Then
                        decHandlingOut = CDec(drCustomerRental.Item(CustomerData.HNDLNG_OT))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.HNDLNG_IN)) Then
                        decHandlingIn = CDec(drCustomerRental.Item(CustomerData.HNDLNG_IN))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.ON_HR_SRVY)) Then
                        decOnHireSurvey = CDec(drCustomerRental.Item(CustomerData.ON_HR_SRVY))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.OFF_HR_SRVY)) Then
                        decOffHireSurvey = CDec(drCustomerRental.Item(CustomerData.OFF_HR_SRVY))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.CNTRCT_RFRNC_NO)) Then
                        strContractReferenceNo = CStr(drCustomerRental.Item(CustomerData.CNTRCT_RFRNC_NO))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.RMRKS_VC)) Then
                        strRemarks = CStr(drCustomerRental.Item(CustomerData.RMRKS_VC))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.CNTRCT_STRT_DT)) Then
                        datContratStart = CDate(drCustomerRental.Item(CustomerData.CNTRCT_STRT_DT))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.CNTRCT_END_DT)) Then
                        datContractEnd = CDate(drCustomerRental.Item(CustomerData.CNTRCT_END_DT))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.MN_TNR_DY)) Then
                        intTenureDays = CLng(drCustomerRental.Item(CustomerData.MN_TNR_DY))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.RNTL_PR_DY)) Then
                        dblRentalPerDay = CDbl(drCustomerRental.Item(CustomerData.RNTL_PR_DY))
                    End If
                End If
                objCustomer.CreateCustomerRental(lngCreated, _
                                                 strContractReferenceNo, _
                                                 datContratStart, _
                                                 datContractEnd, _
                                                 intTenureDays, _
                                                 dblRentalPerDay, _
                                                 decHandlingOut, _
                                                 decHandlingIn, _
                                                 decOnHireSurvey, _
                                                 decOffHireSurvey, _
                                                 strRemarks, _
                                                 objTransaction)
            Next
            objTransaction.commit()
            Return lngCreated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function
#End Region

#Region "pub_ModifyCustomer() TABLE NAME:Customer"
    <OperationContract()> _
    Public Function pub_ModifyCustomer(ByVal bv_i64CSTMR_ID As Int64, _
                                       ByVal bv_strCustomerCode As String, _
                                       ByVal bv_strCustomerName As String, _
                                       ByVal bv_i64CustomerCurrencyID As Int64, _
                                       ByVal bv_i64BillingType As Int64, _
                                       ByVal bv_i64BulkEmailFormatID As Int64, _
                                       ByVal bv_strContactPersonName As String, _
                                       ByVal bv_strContactAddress As String, _
                                       ByVal bv_strBillingAddress As String, _
                                       ByVal bv_strZipCode As String, _
                                       ByVal bv_strPhoneNumber As String, _
                                       ByVal bv_strFaxNumber As String, _
                                       ByVal bv_strReportingEmailID As String, _
                                       ByVal bv_strInvoicingEmailID As String, _
                                       ByVal bv_strRepairTechEmailID As String, _
                                       ByVal bv_decHYDR_AMNT_NC As Decimal, _
                                       ByVal bv_decPNMTC_AMNT_NC As Decimal, _
                                       ByVal bv_decLBR_RT_PR_HR_NC As Decimal, _
                                       ByVal bv_decLK_TST_RT_NC As Decimal, _
                                       ByVal bv_decSRVY_ONHR_OFFHR_RT_NC As Decimal, _
                                       ByVal bv_i64PRDC_TST_TYP_ID As Int64, _
                                       ByVal bv_strVLDTY_PRD_TST_YRS As String, _
                                       ByVal bv_strMinHeatingRate As Decimal, _
                                       ByVal bv_strMinHeatingPeriod As Decimal, _
                                       ByVal bv_strHourlyCharge As Decimal, _
                                       ByVal bv_blnCheckDigitValidationBit As Boolean, _
                                       ByVal bv_blnXML_BT As Boolean, _
                                       ByVal bv_blnTransportationBit As Boolean, _
                                       ByVal bv_blnRentalBit As Boolean, _
                                       ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_blnActiveBit As Boolean, _
                                       ByVal bv_i32DepotID As Int32, _
                                       ByVal bv_strServerUrl As String, _
                                       ByVal bv_strServerName As String, _
                                       ByVal bv_strPassword As String, _
                                       ByVal bv_strEdiCode As String, _
                                       ByVal FinanceIntegrationBit As Boolean, _
                                       ByVal bv_LedgerId As String, _
                                       ByVal bv_blnShell As Boolean, _
                                       ByVal bv_blnSTube As Boolean, _
                                       ByVal bv_strWfData As String, _
                                       ByVal bv_strCustVatNo As String, _
                                       ByVal bv_strAgent As String, _
                                       ByVal bv_strStorageTax As String, _
                                       ByVal bv_strServiceTax As String, _
                                       ByVal bv_strHandlingTax As String, _
                                       ByRef br_dsCustomer As CustomerDataSet) As Boolean
        Dim objTransaction As New Transactions()
        Try
            Dim objCustomer As New Customers
            Dim blnUpdated As Boolean
            bv_blnTransportationBit = CBool(IIf(bv_blnTransportationBit = Nothing, False, True))
            bv_blnRentalBit = CBool(IIf(bv_blnRentalBit = Nothing, False, True))
            blnUpdated = objCustomer.UpdateCustomer(bv_i64CSTMR_ID, _
                                                    bv_strCustomerCode, _
                                                    bv_strCustomerName, _
                                                    bv_i64CustomerCurrencyID, _
                                                    bv_i64BillingType, _
                                                    bv_i64BulkEmailFormatID, _
                                                    bv_strContactPersonName, _
                                                    bv_strContactAddress, _
                                                    bv_strBillingAddress, _
                                                    bv_strZipCode, _
                                                    bv_strPhoneNumber, _
                                                    bv_strFaxNumber, _
                                                    bv_strReportingEmailID, _
                                                    bv_strInvoicingEmailID, _
                                                    bv_strRepairTechEmailID, _
                                                    bv_decHYDR_AMNT_NC, _
                                                    bv_decPNMTC_AMNT_NC, _
                                                    bv_decLBR_RT_PR_HR_NC, _
                                                    bv_decLK_TST_RT_NC, _
                                                    bv_decSRVY_ONHR_OFFHR_RT_NC, _
                                                    bv_i64PRDC_TST_TYP_ID, _
                                                    bv_strVLDTY_PRD_TST_YRS, _
                                                    bv_strMinHeatingRate, _
                                                    bv_strMinHeatingPeriod, _
                                                    bv_strHourlyCharge, _
                                                    bv_blnCheckDigitValidationBit, _
                                                    bv_blnTransportationBit, _
                                                    bv_blnRentalBit, _
                                                    bv_strModifiedBy, _
                                                    bv_datModifiedDate, _
                                                    bv_blnActiveBit, _
                                                    bv_i32DepotID, _
                                                    bv_blnXML_BT, _
                                                    bv_strServerUrl, _
                                                    bv_strServerName, _
                                                    bv_strPassword, _
                                                    bv_strEdiCode, _
                                                    FinanceIntegrationBit, _
                                                    bv_LedgerId, _
                                                    bv_blnShell, _
                                                    bv_blnSTube, _
                                                    bv_strCustVatNo, _
                                                    bv_strAgent, _
                                                    bv_strStorageTax, _
                                                    bv_strServiceTax, _
                                                    bv_strHandlingTax, _
                                                    objTransaction)
            pub_UpdateChargeDetail(br_dsCustomer, bv_i64CSTMR_ID, objTransaction)

            objCustomer.DeletePreviousEmailSetting(bv_i64CSTMR_ID, objTransaction)

            For Each drEmail As DataRow In br_dsCustomer.Tables(CustomerData._V_CUSTOMER_EMAIL_SETTING).Rows
                Dim intPRDC_DY_ID As Integer = 0
                Dim intPRDC_DT_ID As Integer = 0
                Dim strCC_EML As String = String.Empty
                Dim intPRDC_FLTR_ID As Integer = CInt(drEmail.Item(CustomerData.PRDC_FLTR_ID))
                Dim strGNRTN_TM As String = CStr(drEmail.Item(CustomerData.GNRTN_TM))
                Dim dtNXT_RN_DT_TM As Date
                Dim dtLST_RN_DT_TM As Date
                Dim strPRDC_DY_CD As String = String.Empty

                If Not IsDBNull(drEmail.Item(CustomerData.PRDC_DT_ID)) Then
                    intPRDC_DT_ID = CInt(drEmail.Item(CustomerData.PRDC_DT_ID))
                End If
                If Not IsDBNull(drEmail.Item(CustomerData.PRDC_DY_ID)) Then
                    intPRDC_DY_ID = CInt(drEmail.Item(CustomerData.PRDC_DY_ID))
                    strPRDC_DY_CD = CStr(drEmail.Item(CustomerData.PRDC_DY_CD))
                End If
                If Not IsDBNull(drEmail.Item(CustomerData.CC_EML)) Then
                    strCC_EML = CStr(drEmail.Item(CustomerData.CC_EML))
                End If

                dtNXT_RN_DT_TM = pub_getNextRunDateTime(intPRDC_FLTR_ID, strGNRTN_TM, intPRDC_DT_ID, strPRDC_DY_CD)
                dtLST_RN_DT_TM = CDate(System.DateTime.Now.Date & " " & strGNRTN_TM)

                objCustomer.CreateEmailSetting(CInt(drEmail.Item(CustomerData.CSTMR_ID)), _
                                              CInt(drEmail.Item(CustomerData.RPRT_ID)), _
                                              strGNRTN_TM, _
                                              CStr(drEmail.Item(CustomerData.TO_EML)), _
                                              strCC_EML, _
                                              CStr(drEmail.Item(CustomerData.SBJCT_VCR)), _
                                              intPRDC_DT_ID, _
                                              intPRDC_DY_ID, _
                                              intPRDC_FLTR_ID, _
                                              CBool(drEmail.Item(CustomerData.ACTV_BT)), _
                                              dtNXT_RN_DT_TM, _
                                              dtLST_RN_DT_TM, _
                                              objTransaction)
            Next

            For Each drCustomerTransportation As DataRow In br_dsCustomer.Tables(CustomerData._V_CUSTOMER_TRANSPORTATION).Rows
                Dim strRouteId As String = ""
                Dim decEmptyRate As Decimal = 0
                Dim decFullRate As Decimal = 0

                If drCustomerTransportation.RowState <> DataRowState.Deleted Then
                    strRouteId = objCustomer.GetRouteIdByRouteCode(CStr(drCustomerTransportation.Item(CustomerData.RT_CD)), bv_i32DepotID)
                    If Not IsDBNull(drCustomerTransportation.Item(CustomerData.EMPTY_TRP_RT_NC)) Then
                        decEmptyRate = CDec(drCustomerTransportation.Item(CustomerData.EMPTY_TRP_RT_NC))
                    End If
                    If Not IsDBNull(drCustomerTransportation.Item(CustomerData.FLL_TRP_RT_NC)) Then
                        decFullRate = CDec(drCustomerTransportation.Item(CustomerData.FLL_TRP_RT_NC))
                    End If
                End If
                If drCustomerTransportation.RowState = DataRowState.Modified Then
                    objCustomer.UpdateCustomerTransportation(CLng(drCustomerTransportation.Item(CustomerData.CSTMR_TRNSPRTTN_ID)), _
                                                             bv_i64CSTMR_ID, _
                                                             CLng(strRouteId), _
                                                             decEmptyRate, _
                                                             decFullRate, _
                                                             objTransaction)
                ElseIf drCustomerTransportation.RowState = DataRowState.Added Then
                    objCustomer.CreateCustomerTransportation(bv_i64CSTMR_ID, _
                                                             CLng(strRouteId), _
                                                             decEmptyRate, _
                                                             decFullRate, _
                                                             objTransaction)
                ElseIf drCustomerTransportation.RowState = DataRowState.Deleted Then
                    objCustomer.DeleteCustomerTransportation(CommonUIs.iLng(drCustomerTransportation.Item(CustomerData.CSTMR_TRNSPRTTN_ID, DataRowVersion.Original)), objTransaction)
                End If
            Next

            For Each drCustomerRental As DataRow In br_dsCustomer.Tables(CustomerData._V_CUSTOMER_RENTAL).Rows
                Dim decHandlingOut As Decimal = 0
                Dim decHandlingIn As Decimal = 0
                Dim decOnHireSurvey As Decimal = 0
                Dim decOffHireSurvey As Decimal = 0
                Dim datContratStart As DateTime = Nothing
                Dim datContractEnd As DateTime = Nothing
                Dim intTenureDays As Int64 = 0
                Dim dblRentalPerDay As Double = 0
                Dim strContractReferenceNo As String = String.Empty
                Dim strRemarks As String = String.Empty
                If drCustomerRental.RowState <> DataRowState.Deleted Then
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.HNDLNG_OT)) Then
                        decHandlingOut = CDec(drCustomerRental.Item(CustomerData.HNDLNG_OT))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.HNDLNG_IN)) Then
                        decHandlingIn = CDec(drCustomerRental.Item(CustomerData.HNDLNG_IN))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.ON_HR_SRVY)) Then
                        decOnHireSurvey = CDec(drCustomerRental.Item(CustomerData.ON_HR_SRVY))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.OFF_HR_SRVY)) Then
                        decOffHireSurvey = CDec(drCustomerRental.Item(CustomerData.OFF_HR_SRVY))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.CNTRCT_RFRNC_NO)) Then
                        strContractReferenceNo = CStr(drCustomerRental.Item(CustomerData.CNTRCT_RFRNC_NO))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.CNTRCT_STRT_DT)) Then
                        datContratStart = CDate(drCustomerRental.Item(CustomerData.CNTRCT_STRT_DT))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.CNTRCT_END_DT)) Then
                        datContractEnd = CDate(drCustomerRental.Item(CustomerData.CNTRCT_END_DT))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.MN_TNR_DY)) Then
                        intTenureDays = CLng(drCustomerRental.Item(CustomerData.MN_TNR_DY))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.RNTL_PR_DY)) Then
                        dblRentalPerDay = CDbl(drCustomerRental.Item(CustomerData.RNTL_PR_DY))
                    End If
                    If Not IsDBNull(drCustomerRental.Item(CustomerData.RMRKS_VC)) Then
                        strRemarks = CStr(drCustomerRental.Item(CustomerData.RMRKS_VC))
                    End If
                    '  Dim strSupplierId As String = ""
                    '   strSupplierId = objCustomer.GetSupplierIdByContractNo(CStr(drCustomerRental.Item(CustomerData.CNTRCT_RFRNC_NO)))
                End If
                If drCustomerRental.RowState = DataRowState.Modified Then
                    objCustomer.UpdateCustomerRental(CLng(drCustomerRental.Item(CustomerData.CSTMR_RNTL_ID)), _
                                                     bv_i64CSTMR_ID, _
                                                     strContractReferenceNo, _
                                                     datContratStart, _
                                                     datContractEnd, _
                                                     intTenureDays, _
                                                     dblRentalPerDay, _
                                                     decHandlingOut, _
                                                     decHandlingIn, _
                                                     decOnHireSurvey, _
                                                     decOffHireSurvey, _
                                                     strRemarks, _
                                                     objTransaction)
                ElseIf drCustomerRental.RowState = DataRowState.Added Then
                    objCustomer.CreateCustomerRental(bv_i64CSTMR_ID, _
                                                    strContractReferenceNo, _
                                                     datContratStart, _
                                                     datContractEnd, _
                                                     intTenureDays, _
                                                     dblRentalPerDay, _
                                                     decHandlingOut, _
                                                     decHandlingIn, _
                                                     decOnHireSurvey, _
                                                     decOffHireSurvey, _
                                                     strRemarks, _
                                                     objTransaction)
                ElseIf drCustomerRental.RowState = DataRowState.Deleted Then
                    objCustomer.DeleteCustomerRental(CommonUIs.iLng(drCustomerRental.Item(CustomerData.CSTMR_RNTL_ID, DataRowVersion.Original)), objTransaction)
                End If
            Next
            objTransaction.commit()
            Return blnUpdated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function

#End Region

#Region "pub_CustomerChargeDetailDeleteCustomerChargeDetail() TABLE NAME:CUSTOMER_CHARGE_DETAIL"

    <OperationContract()> _
    Public Function pub_CustomerChargeDetailDeleteCustomerChargeDetail(ByVal bv_i64CustomerChargeID As Int64, ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim objCustomers As New Customers
            Dim blnDeleted As Boolean
            blnDeleted = objCustomers.DeleteCustomerChargeDetail(bv_i64CustomerChargeID, br_objTransaction)
            Return blnDeleted
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateChargeDetail()"
    <OperationContract()> _
    Public Function pub_UpdateChargeDetail(ByRef dsCustomer As CustomerDataSet, _
                                           ByVal bv_CustomerID As Int64, _
                                           ByRef br_ObjTransactions As Transactions) As Boolean


        Try
            Dim dtCustomerChargeDetail As DataTable
            Dim ObjCustomers As New Customers
            Dim bolupdatebt As Boolean
            Dim lngCreated As Long
            dtCustomerChargeDetail = dsCustomer.Tables(CustomerData._V_CUSTOMER_CHARGE_DETAIL)
            For Each drCharge As DataRow In dtCustomerChargeDetail.Rows
                Select Case drCharge.RowState
                    Case DataRowState.Added
                        lngCreated = ObjCustomers.CreateCustomerChargeDetail(bv_CustomerID, _
                                                                             CommonUIs.iLng(drCharge.Item(CustomerData.EQPMNT_TYP_ID)), _
                                                                             CommonUIs.iLng(drCharge.Item(CustomerData.EQPMNT_TYP_ID)), _
                                                                             CommonUIs.iDec(drCharge.Item(CustomerData.HNDLNG_IN_CHRG_NC)), _
                                                                             CommonUIs.iDec(drCharge.Item(CustomerData.HNDLNG_OUT_CHRG_NC)), _
                                                                             CommonUIs.iBool(drCharge.Item(CustomerData.ACTV_BT)), _
                                                                             CommonUIs.iDec(drCharge.Item(CustomerData.INSPCTN_CHRGS)), _
                                                                             br_ObjTransactions)

                        pub_UpdateStorageDetail(dsCustomer, bv_CustomerID, lngCreated, CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID)), br_ObjTransactions)
                        pub_UpdateCleaningDetail(dsCustomer, bv_CustomerID, lngCreated, CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID)), br_ObjTransactions)
                        drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID) = lngCreated
                    Case DataRowState.Modified
                        bolupdatebt = ObjCustomers.UpdateCustomerChargeDetail(CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID)), _
                                                                              bv_CustomerID, _
                                                                              CommonUIs.iLng(drCharge.Item(CustomerData.EQPMNT_TYP_ID)), _
                                                                              CommonUIs.iLng(drCharge.Item(CustomerData.EQPMNT_TYP_ID)), _
                                                                              CommonUIs.iDec(drCharge.Item(CustomerData.HNDLNG_IN_CHRG_NC)), _
                                                                              CommonUIs.iDec(drCharge.Item(CustomerData.HNDLNG_OUT_CHRG_NC)), _
                                                                              CommonUIs.iDec(drCharge.Item(CustomerData.INSPCTN_CHRGS)), _
                                                                              CommonUIs.iBool(drCharge.Item(CustomerData.ACTV_BT)), _
                                                                              br_ObjTransactions)
                        pub_UpdateStorageDetail(dsCustomer, bv_CustomerID, lngCreated, CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID)), br_ObjTransactions)
                        pub_UpdateCleaningDetail(dsCustomer, bv_CustomerID, lngCreated, CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID)), br_ObjTransactions)
                        lngCreated = CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID))
                    Case DataRowState.Deleted
                        ObjCustomers.DeleteCustomerStorageDetailByChargeDetailID(CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                        ObjCustomers.DeleteCustomerCleaningDetailByChargeDetailID(CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                        pub_CustomerChargeDetailDeleteCustomerChargeDetail(CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                    Case DataRowState.Unchanged
                        lngCreated = CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID))
                        pub_UpdateStorageDetail(dsCustomer, bv_CustomerID, lngCreated, CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID)), br_ObjTransactions)
                        pub_UpdateCleaningDetail(dsCustomer, bv_CustomerID, lngCreated, CommonUIs.iLng(drCharge.Item(CustomerData.CSTMR_CHRG_DTL_ID)), br_ObjTransactions)
                End Select
            Next

            Return bolupdatebt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateStorageDetail()"
    <OperationContract()> _
    Public Function pub_UpdateStorageDetail(ByRef br_dsCustomer As CustomerDataSet, _
                                            ByVal bv_i64CustomerID As Int64, _
                                            ByVal bv_CustomerChargeDetailID As Int64, _
                                            ByVal bv_oldCustomerChargeDetailID As Int64, _
                                            ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dtCustomerStorageDetail As DataTable
            Dim ObjCustomers As New Customers
            Dim bolupdatebt As Boolean

            dtCustomerStorageDetail = br_dsCustomer.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL)
            For Each drStorage As DataRow In dtCustomerStorageDetail.Select(String.Concat(CustomerData.CSTMR_CHRG_DTL_ID, "=", bv_oldCustomerChargeDetailID))
                Select Case drStorage.RowState
                    Case DataRowState.Added
                        Dim lngCreated As Long = ObjCustomers.CreateCustomerStorageDetail(bv_CustomerChargeDetailID, _
                                                                                          bv_i64CustomerID, _
                                                                                          CommonUIs.iInt(drStorage.Item(CustomerData.UP_TO_DYS)), _
                                                                                          CommonUIs.iDec(drStorage.Item(CustomerData.STRG_CHRG_NC)), _
                                                                                          drStorage.Item(CustomerData.RMRKS_VC).ToString, _
                                                                                          br_ObjTransactions)

                        drStorage.Item(CustomerData.CSTMR_STRG_DTL_ID) = lngCreated

                    Case DataRowState.Modified
                        bolupdatebt = ObjCustomers.UpdateCustomerStorageDetail(CommonUIs.iLng(drStorage.Item(CustomerData.CSTMR_STRG_DTL_ID)), _
                                                                               bv_CustomerChargeDetailID, _
                                                                               bv_i64CustomerID, _
                                                                               CommonUIs.iInt(drStorage.Item(CustomerData.UP_TO_DYS)), _
                                                                               CommonUIs.iDec(drStorage.Item(CustomerData.STRG_CHRG_NC)), _
                                                                               drStorage.Item(CustomerData.RMRKS_VC).ToString, _
                                                                               br_ObjTransactions)
                    Case DataRowState.Deleted
                        ObjCustomers.DeleteCustomerStorageDetailByStorageDetailID(CInt(drStorage.Item(CustomerData.CSTMR_STRG_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                End Select
            Next

            For Each drStorage As DataRow In dtCustomerStorageDetail.Rows
                Select Case drStorage.RowState
                    Case DataRowState.Deleted
                        ObjCustomers.DeleteCustomerStorageDetailByStorageDetailID(CInt(drStorage.Item(CustomerData.CSTMR_STRG_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                End Select
            Next

            Return bolupdatebt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateCleaningDetail()"
    <OperationContract()> _
    Public Function pub_UpdateCleaningDetail(ByRef br_dsCustomer As CustomerDataSet, _
                                            ByVal bv_i64CustomerID As Int64, _
                                            ByVal bv_CustomerChargeDetailID As Int64, _
                                            ByVal bv_oldCustomerChargeDetailID As Int64, _
                                            ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dtCustomerCleaningDetail As DataTable
            Dim ObjCustomers As New Customers
            Dim bolupdatebt As Boolean

            dtCustomerCleaningDetail = br_dsCustomer.Tables(CustomerData._CUSTOMER_CLEANING_DETAIL)
            For Each drCleaning As DataRow In dtCustomerCleaningDetail.Select(String.Concat(CustomerData.CSTMR_CHRG_DTL_ID, "=", bv_oldCustomerChargeDetailID))
                Select Case drCleaning.RowState
                    Case DataRowState.Added
                        Dim lngCreated As Long = ObjCustomers.CreateCustomerCleaningDetail(bv_CustomerChargeDetailID, _
                                                                                          bv_i64CustomerID, _
                                                                                          CommonUIs.iInt(drCleaning.Item(CustomerData.UP_TO_CNTNRS)), _
                                                                                          CommonUIs.iDec(drCleaning.Item(CustomerData.CLNNG_RT)), _
                                                                                          drCleaning.Item(CustomerData.RMRKS_VC).ToString, _
                                                                                          br_ObjTransactions)

                        drCleaning.Item(CustomerData.CSTMR_CLNNG_DTL_ID) = lngCreated

                    Case DataRowState.Modified
                        bolupdatebt = ObjCustomers.UpdateCustomerCleaningDetail(CommonUIs.iLng(drCleaning.Item(CustomerData.CSTMR_CLNNG_DTL_ID)), _
                                                                               bv_CustomerChargeDetailID, _
                                                                               bv_i64CustomerID, _
                                                                               CommonUIs.iInt(drCleaning.Item(CustomerData.UP_TO_CNTNRS)), _
                                                                               CommonUIs.iDec(drCleaning.Item(CustomerData.CLNNG_RT)), _
                                                                               drCleaning.Item(CustomerData.RMRKS_VC).ToString, _
                                                                               br_ObjTransactions)
                    Case DataRowState.Deleted
                        ObjCustomers.DeleteCustomerCleaningDetailByCleaningDetailID(CInt(drCleaning.Item(CustomerData.CSTMR_CLNNG_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                End Select
            Next

            For Each drCleaning As DataRow In dtCustomerCleaningDetail.Rows
                Select Case drCleaning.RowState
                    Case DataRowState.Deleted
                        ObjCustomers.DeleteCustomerCleaningDetailByCleaningDetailID(CInt(drCleaning.Item(CustomerData.CSTMR_CLNNG_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                End Select
            Next

            Return bolupdatebt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerChargeDetailByCustomerID() TABLE NAME:CUSTOMER_CHARGE_DETAIL"

    <OperationContract()> _
    Public Function pub_GetCustomerChargeDetailByCustomerID(ByVal bv_i64CustomerID As Int64) As CustomerDataSet
        Try
            Dim dsCustomerStorageDetailData As CustomerDataSet
            Dim objCustomers As New Customers
            dsCustomerStorageDetailData = objCustomers.CustomerChargeDetailByCustomerID(bv_i64CustomerID)
            objCustomers.GetV_CUSTOMER_EMAIL_SETTING(bv_i64CustomerID)
            Return dsCustomerStorageDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerChargeDetailByCustomerID() TABLE NAME:CUSTOMER_CHARGE_DETAIL"

    <OperationContract()> _
    Public Function pub_GetCustomerChargeDetailByCustomerID(ByVal bv_i64CustomerID As Int64, ByVal bv_i64CustomerChargeDetailID As Int64) As CustomerDataSet
        Try
            Dim dsCustomerStorageDetailData As CustomerDataSet
            Dim objCustomers As New Customers
            dsCustomerStorageDetailData = objCustomers.CustomerStorageDetailByCustomerChargeDetailID(bv_i64CustomerID, bv_i64CustomerChargeDetailID)
            Return dsCustomerStorageDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerCleaningChargeDetailByCustomerID() TABLE NAME:CUSTOMER_CHARGE_DETAIL"

    <OperationContract()> _
    Public Function pub_GetCustomerCleaningChargeDetailByCustomerID(ByVal bv_i64CustomerID As Int64, ByVal bv_i64CustomerChargeDetailID As Int64) As CustomerDataSet
        Try
            Dim dsCustomerStorageDetailData As CustomerDataSet
            Dim objCustomers As New Customers
            dsCustomerStorageDetailData = objCustomers.CustomerCleaningDetailByCustomerChargeDetailID(bv_i64CustomerID, bv_i64CustomerChargeDetailID)
            Return dsCustomerStorageDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_getNextRunDateTime()"
    <OperationContract()> _
    Private Function pub_getNextRunDateTime(ByVal intPRDC_FLTR_ID As Integer, ByVal strGNRTN_TM As String, ByVal intPRDC_DT_ID As Integer, ByVal strPRDC_DY_CD As String) As DateTime
        Try
            Dim dtNXT_RN_DT_TM As Date
            Dim today As Date = Date.Today

            If intPRDC_FLTR_ID = 33 Then
                If DateTime.Now > CDate(New DateTime(Now.Year, Now.Month, Now.Day) & " " & strGNRTN_TM) Then
                    dtNXT_RN_DT_TM = CDate(Date.Now.Date.AddDays(1) & " " & strGNRTN_TM)
                Else
                    dtNXT_RN_DT_TM = CDate(Date.Now.Date & " " & strGNRTN_TM)
                End If
            ElseIf intPRDC_FLTR_ID = 34 Then
                dtNXT_RN_DT_TM = CDate(Date.Now.Date.AddDays(intPRDC_DT_ID) & " " & strGNRTN_TM)
            ElseIf intPRDC_FLTR_ID = 35 Then
                Dim dayIndex As Integer = today.DayOfWeek
                Dim dayDiff As Integer
                Dim NxtRnDat As Date
                Dim intPRDC_DY_ID As Integer

                Select Case strPRDC_DY_CD
                    Case "MONDAY"
                        intPRDC_DY_ID = 1
                    Case "TUESDAY"
                        intPRDC_DY_ID = 2
                    Case "WEDNESDAY"
                        intPRDC_DY_ID = 3
                    Case "THURSDAY"
                        intPRDC_DY_ID = 4
                    Case "FRIDAY"
                        intPRDC_DY_ID = 5
                    Case "SATURDAY"
                        intPRDC_DY_ID = 6
                    Case "SUNDAY"
                        intPRDC_DY_ID = 7
                End Select

                If dayIndex >= intPRDC_DY_ID Then
                    dayDiff = today.DayOfWeek - intPRDC_DY_ID
                    NxtRnDat = today.AddDays(-dayDiff)
                    If DateTime.Now > CDate(New DateTime(Now.Year, Now.Month, Now.Day) & " " & strGNRTN_TM) Then
                        dtNXT_RN_DT_TM = CDate(NxtRnDat.AddDays(7) & " " & strGNRTN_TM)
                    Else
                        dtNXT_RN_DT_TM = CDate(Date.Now.Date & " " & strGNRTN_TM)
                    End If
                Else
                    dayDiff = today.DayOfWeek - intPRDC_DY_ID
                    NxtRnDat = today.AddDays(-dayDiff)
                    dtNXT_RN_DT_TM = CDate(NxtRnDat & " " & strGNRTN_TM)
                End If
            ElseIf intPRDC_FLTR_ID = 36 Then
                If today.Day >= intPRDC_DT_ID AndAlso DateTime.Now > CDate(New DateTime(Now.Year, Now.Month, intPRDC_DT_ID) & " " & strGNRTN_TM) Then
                    If intPRDC_DT_ID > 28 Then
                        dtNXT_RN_DT_TM = CDate(New DateTime(Now.Year, Now.Month, intPRDC_DT_ID) & " " & strGNRTN_TM).AddMonths(1).AddDays(-1)
                    Else
                        dtNXT_RN_DT_TM = CDate(New DateTime(Now.Year, Now.Month + 1, intPRDC_DT_ID) & " " & strGNRTN_TM)
                    End If
                Else
                    If intPRDC_DT_ID > 28 Then
                        dtNXT_RN_DT_TM = CDate(New DateTime(Now.Year, Now.Month, 1) & " " & strGNRTN_TM).AddMonths(1).AddDays(-1)
                    Else
                        dtNXT_RN_DT_TM = CDate(New DateTime(Now.Year, Now.Month, intPRDC_DT_ID) & " " & strGNRTN_TM)
                    End If
                End If
            End If

            Return dtNXT_RN_DT_TM

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_getReportType TABLE NAME:ENUM"
    <OperationContract()> _
    Public Function pub_getReportType(ByVal bv_strReportValeus As String) As CustomerDataSet

        Try
            Dim dsReportDataSet As CustomerDataSet
            Dim objCustomers As New Customers
            dsReportDataSet = objCustomers.GetReportType(bv_strReportValeus)
            Return (dsReportDataSet)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerEmailSettingsByCustomerID() TABLE NAME:Customer_Email_Settings"
    <OperationContract()> _
    Public Function pub_GetCustomerEmailSettingsByCustomerID(ByVal bv_i32CustomerID As Int32) As CustomerDataSet

        Try
            Dim dsCustomerDataSet As CustomerDataSet
            Dim objCustomers As New Customers
            dsCustomerDataSet = objCustomers.GetV_CUSTOMER_EMAIL_SETTING(bv_i32CustomerID)

            objCustomers.GetV_CUSTOMER_EMAIL_SETTING(bv_i32CustomerID)
            Return (dsCustomerDataSet)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


#Region "pub_GetRouteTripRateById() TABLE NAME:V_ROUTE"

    <OperationContract()> _
    Public Function pub_GetRouteTripRateById(ByVal bv_i64RouteId As Int64, _
                                             ByVal bv_intDepotId As Int32) As CustomerDataSet

        Try
            Dim dsCustomerTransportationData As CustomerDataSet
            Dim objCustomerTransportations As New Customers
            dsCustomerTransportationData = objCustomerTransportations.GetRouteTripRateById(bv_i64RouteId, bv_intDepotId)
            Return dsCustomerTransportationData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRentalGateoutDetails() Table Name: Rental_Entry"
    <OperationContract()> _
    Public Function pub_GetRentalGateoutDetails(ByVal bv_strContractRefNo As String) As String
        Try
            Dim strIsGateout As String = String.Empty
            Dim objCustomers As New Customers
            strIsGateout = objCustomers.GetRentalGateoutDetails(bv_strContractRefNo)
            Return strIsGateout
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


#Region "getISOCODEbyCustomer() Table Name: customer"
    <OperationContract()> _
    Public Function pub_getISOCODEbyCustomer(ByVal bv_intCustomer As Int64) As String
        Try
            Dim strIsGateout As String = String.Empty
            Dim dsCustomerData As CustomerDataSet
            Dim objCustomers As New Customers
            dsCustomerData = objCustomers.getISOCODEbyCustomer(bv_intCustomer)
            'If dsCustomerData.Tables(CustomerData._V_CUSTOMER).Rows.Item(0) = "" Then

            'End If
            Return strIsGateout
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEnumOnCustomerRates"
    Public Function pub_GetEnumOnCustomerRates() As CustomerDataSet
        Try
            Dim objCustomers As New Customers
            Dim dsCustomerEnum As New CustomerDataSet
            dsCustomerEnum = objCustomers.GetEnumOnCustomerRates()
            Return dsCustomerEnum
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
    

End Class