Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

<ServiceContract()> _
Public Class InvoiceGeneration

#Region "pub_HandlingChargeGetHandlingChargeByCSTMRID() TABLE NAME:HANDLING_CHARGE"

    <OperationContract()> _
    Public Function pub_GetHandlingChargeByCustomerId(ByVal bv_i64CSTMR_ID As Int64, _
                                                      ByVal bv_datPeriodFrom As DateTime, _
                                                      ByVal bv_datPeriodTo As DateTime, _
                                                      ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetHandlingChargeByCustomerId(bv_i64CSTMR_ID, _
                                                                                     bv_datPeriodFrom, _
                                                                                     bv_datPeriodTo, _
                                                                                     bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function Pub_Get_GWSHandlingChargeByCustomerId(ByVal bv_i64CSTMR_ID As Int64, _
                                                      ByVal bv_datPeriodFrom As DateTime, _
                                                      ByVal bv_datPeriodTo As DateTime, _
                                                      ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.Get_GWSHandlingChargeByCustomerId(bv_i64CSTMR_ID, _
                                                                                     bv_datPeriodFrom, _
                                                                                     bv_datPeriodTo, _
                                                                                     bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function


    <OperationContract()> _
    Public Function pub_Get_GWSHandlingChargeByAgentId(ByVal bv_i64AgentId As Int64, _
                                                      ByVal bv_datPeriodFrom As DateTime, _
                                                      ByVal bv_datPeriodTo As DateTime, _
                                                      ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.Get_GWSHandlingChargeByAgentId(bv_i64AgentId, _
                                                                                     bv_datPeriodFrom, _
                                                                                     bv_datPeriodTo, _
                                                                                     bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function


#End Region

#Region "pub_GetStorageChargeByCustomerId() TABLE NAME:STORAGE_CHARGE"

    <OperationContract()> _
    Public Function pub_GetStorageChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                     ByVal bv_datPeriodFrom As DateTime, _
                                                     ByVal bv_datPeriodTo As DateTime, _
                                                     ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetStorageChargeByCustomerId(bv_i64CustomerId, _
                                                                                    bv_datPeriodFrom, _
                                                                                    bv_datPeriodTo, _
                                                                                    bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_Get_GWSStorageChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                     ByVal bv_datPeriodFrom As DateTime, _
                                                     ByVal bv_datPeriodTo As DateTime, _
                                                     ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.Get_GWSStorageChargeByCustomerId(bv_i64CustomerId, _
                                                                                    bv_datPeriodFrom, _
                                                                                    bv_datPeriodTo, _
                                                                                    bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function


    <OperationContract()> _
    Public Function pub_Get_GWSStorageChargeByAgentId(ByVal bv_i64AgentId As Int64, _
                                                     ByVal bv_datPeriodFrom As DateTime, _
                                                     ByVal bv_datPeriodTo As DateTime, _
                                                     ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.Get_GWSStorageChargeByAgentId(bv_i64AgentId, _
                                                                                    bv_datPeriodFrom, _
                                                                                    bv_datPeriodTo, _
                                                                                    bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerDetail() TABLE NAME:CUSTOMER"
    <OperationContract()> _
    Public Function pub_GetCustomerDetail(ByVal bv_i64CustomerId As Int64, ByVal bv_intDepotId As Integer) As InvoiceGenerationDataSet
        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetCustomerDetail(bv_i64CustomerId, bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBankDetailByDepotId() TABLE NAME:BANK_DETAIL"

    <OperationContract()> _
    Public Function pub_GetBankDetailByDepotId(ByVal bv_intDepotId As Int32, _
                                               ByRef br_dtBankDetail As DataTable) As Boolean

        Try
            Dim objCommonUIs As New CommonUIs
            br_dtBankDetail = objCommonUIs.GetBankDetailByDepotId(bv_intDepotId).Tables(InvoiceGenerationData._V_BANK_DETAIL)
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBankDetailByDepotIdForForeignCurrency() TABLE NAME:BANK_DETAIL"

    <OperationContract()> _
    Public Function pub_GetBankDetailByDepotIdForForeignCurrency(ByVal bv_intDepotId As Int32, _
                                                                 ByRef br_dtBankDetail As DataTable) As Boolean

        Try
            Dim objCommonUIs As New CommonUIs
            br_dtBankDetail = objCommonUIs.GetBankDetailByDepotIdForForeignCurrency(bv_intDepotId).Tables(InvoiceGenerationData._FOREIGN_BANK_DETAIL)
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetInvoicingParty() TABLE NAME:INVOICING_PAPRTY"
    <OperationContract()> _
    Public Function pub_GetInvoicingPartyExchangeRate(ByVal bv_i64InvoicingPartyId As Int64, ByVal bv_intDepotId As Integer) As InvoiceGenerationDataSet
        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetInvoicingPartyExchangeRate(bv_i64InvoicingPartyId, bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRepairChargeByCustomerId() TABLE NAME:REPAIR_CHARGE"

    <OperationContract()> _
    Public Function pub_GetRepairChargeByCustomerId(ByVal bv_i64CustomerID As Int64, _
                                                    ByVal bv_datPeriodFrom As DateTime, _
                                                    ByVal bv_datPeriodTo As DateTime, _
                                                    ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetRepairChargeByCustomerId(bv_i64CustomerID, _
                                                                                   bv_datPeriodFrom, _
                                                                                   bv_datPeriodTo, _
                                                                                   bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_GetRepairChargeGWS_ByCustomerId(ByVal bv_i64CustomerID As Int64, _
                                                    ByVal bv_datPeriodFrom As DateTime, _
                                                    ByVal bv_datPeriodTo As DateTime, _
                                                    ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetRepairChargeGWS_ByCustomerId(bv_i64CustomerID, _
                                                                                   bv_datPeriodFrom, _
                                                                                   bv_datPeriodTo, _
                                                                                   bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_GetRepairChargeGWS_ByAgentId(ByVal bv_i64AgentID As Int64, _
                                                    ByVal bv_datPeriodFrom As DateTime, _
                                                    ByVal bv_datPeriodTo As DateTime, _
                                                    ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetRepairChargeGWS_ByAgentId(bv_i64AgentID, _
                                                                                   bv_datPeriodFrom, _
                                                                                   bv_datPeriodTo, _
                                                                                   bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCleaningChargeByCustomerId() TABLE NAME:CLEANING_CHARGE"

    <OperationContract()> _
    Public Function pub_GetCleaningChargeByCustomerId(ByVal bv_i64CustomerID As Int64, _
                                                      ByVal bv_datPeriodFrom As DateTime, _
                                                      ByVal bv_datPeriodTo As DateTime, _
                                                      ByVal bv_intDepotId As Int32, _
                                                      ByVal bv_strCustomerType As String, _
                                                      ByVal bv_strBackDated As String) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As New InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            Dim objCommon As New CommonUIs
            Dim str_073Key As String
            str_073Key = objCommon.DecryptString(CStr(objCommon.GetConfigByKeyName("073", CLng(IIf(objCommon.GetMultiLocationSupportConfig.ToLower = "true", objCommon.GetHeadQuarterID(), bv_intDepotId))).Tables(ConfigData._CONFIG).Rows(0).Item(ConfigData.KY_VL)))
            ''If Slab Rate is true for cleaning
            If str_073Key.ToLower = "true" Then
                Dim tempFromDate As DateTime = bv_datPeriodFrom
                Dim tempToDate As DateTime
                Dim tempDtCleaning As DataTable
                Dim tempDtEqptype As DataTable
                Dim decSlabRate As Decimal
                Dim dtCustomer As DataTable
                Dim strBillingFlag As String = String.Empty
                If bv_strBackDated.ToLower = "true" Then
                    strBillingFlag = ""
                Else
                    strBillingFlag = " AND BLLNG_FLG<>'B' "
                End If
                For i = 0 To CInt(DateDiff(DateInterval.Month, bv_datPeriodFrom, bv_datPeriodTo))
                    tempToDate = tempFromDate.AddMonths(1).AddDays(-1)

                    ''For Slab Rate Flag =0
                    tempDtCleaning = objInvoiceGeneration.GetCleaningChargeByCustomerIdWithoutSlab(bv_i64CustomerID, _
                                                                                tempFromDate, _
                                                                                tempToDate, _
                                                                                bv_intDepotId)
                    dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Merge(tempDtCleaning)
                    tempDtCleaning.Clear()
                    tempDtEqptype = objInvoiceGeneration.GetDistinctEquipmentTypeByCustomerIDWithSlab(bv_i64CustomerID, tempFromDate, _
                                                                               tempToDate, bv_intDepotId, strBillingFlag)

                    ''For Slab Rate Flag = 0 and with different equipment Types
                    For countEquipType As Integer = 0 To tempDtEqptype.Rows.Count - 1
                        Dim totEquipCount As Long
                        tempDtCleaning = objInvoiceGeneration.GetCleaningChargeByCustomerIdWithSlabEquipType(bv_i64CustomerID, _
                                                                             tempFromDate, _
                                                                             tempToDate, _
                                                                             CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), _
                                                                             bv_intDepotId, _
                                                                             strBillingFlag)
                        totEquipCount = objInvoiceGeneration.GetEquipmentCountByEqpmtType(bv_i64CustomerID, _
                                                                            tempFromDate, _
                                                                            tempToDate, _
                                                                            CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)))
                        If tempDtCleaning.Rows.Count = 0 Then
                            Continue For
                        End If
                        If bv_strCustomerType.ToLower = "party" Then
                            Dim dtCustomerParty As DataTable = tempDtCleaning.DefaultView.ToTable(True, "CSTMR_ID")
                            For Each partycount As DataRow In dtCustomerParty.Rows
                                Dim totEquipCountForParty As Long
                                tempDtCleaning.Clear()
                                tempDtCleaning = objInvoiceGeneration.GetCleaningChargeByCustomerIdAndPartyWithSlabEquipType(bv_i64CustomerID, _
                                                                                                                             CLng(partycount.Item(InvoiceGenerationData.CSTMR_ID)), _
                                                                              tempFromDate, _
                                                                              tempToDate, _
                                                                              CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), _
                                                                              bv_intDepotId, _
                                                                              strBillingFlag)
                                totEquipCountForParty = objInvoiceGeneration.GetEquipmentCountForPartyByEqpmtType(bv_i64CustomerID, _
                                                                                                                             CLng(partycount.Item(InvoiceGenerationData.CSTMR_ID)), _
                                                                              tempFromDate, _
                                                                              tempToDate, _
                                                                              CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)))
                                dtCustomer = objInvoiceGeneration.GetCleaningSlabRateByEquipTypeCustomerID(CLng(tempDtCleaning.Rows(0).Item(InvoiceGenerationData.CSTMR_ID)), CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), totEquipCountForParty)
                                If dtCustomer.Rows.Count > 0 Then
                                    decSlabRate = CDec(dtCustomer.Rows(0).Item(CustomerData.CLNNG_RT))
                                Else
                                    decSlabRate = 0
                                End If
                                If bv_strBackDated.ToLower = "true" Then
                                    Dim drBackDatedDataRows() As DataRow = tempDtCleaning.Select("BLLNG_FLG='U' OR BLLNG_FLG='D'")
                                    For j = 0 To drBackDatedDataRows.Length - 1
                                        drBackDatedDataRows(j).Item(InvoiceGenerationData.CLNNG_RT) = decSlabRate
                                    Next
                                    For Each drCleaning As DataRow In tempDtCleaning.Rows
                                        If drCleaning.RowState = DataRowState.Unchanged Then
                                            drCleaning.Delete()
                                        End If
                                    Next
                                    tempDtCleaning.AcceptChanges()
                                Else
                                    For j = 0 To tempDtCleaning.Rows.Count - 1
                                        tempDtCleaning.Rows(j).Item(InvoiceGenerationData.CLNNG_RT) = decSlabRate
                                    Next
                                End If
                                dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Merge(tempDtCleaning)
                            Next

                        Else
                            dtCustomer = objInvoiceGeneration.GetCleaningSlabRateByEquipTypeCustomerID(bv_i64CustomerID, CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), totEquipCount)
                            If dtCustomer.Rows.Count > 0 Then
                                decSlabRate = CDec(dtCustomer.Rows(0).Item(CustomerData.CLNNG_RT))
                            Else
                                decSlabRate = 0
                            End If
                            If bv_strBackDated.ToLower = "true" Then
                                Dim drBackDatedDataRows() As DataRow = tempDtCleaning.Select("BLLNG_FLG='U' OR BLLNG_FLG='D'")
                                For j = 0 To drBackDatedDataRows.Length - 1
                                    drBackDatedDataRows(j).Item(InvoiceGenerationData.CLNNG_RT) = decSlabRate
                                Next
                                For Each drCleaning As DataRow In tempDtCleaning.Rows
                                    If drCleaning.RowState = DataRowState.Unchanged Then
                                        drCleaning.Delete()
                                    End If
                                Next
                                tempDtCleaning.AcceptChanges()
                            Else
                                For j = 0 To tempDtCleaning.Rows.Count - 1
                                    tempDtCleaning.Rows(j).Item(InvoiceGenerationData.CLNNG_RT) = decSlabRate
                                Next
                            End If
                            dsInvoiceGeneration.Tables(InvoiceGenerationData._V_CLEANING_CHARGE).Merge(tempDtCleaning)
                        End If
                    Next

                    tempDtCleaning.Clear()
                    tempFromDate = tempFromDate.AddMonths(1)
                Next
            Else
                dsInvoiceGeneration = objInvoiceGeneration.GetCleaningChargeByCustomerId(bv_i64CustomerID, _
                                                                                     bv_datPeriodFrom, _
                                                                                     bv_datPeriodTo, _
                                                                                     bv_intDepotId)
            End If

            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetMiscellaneousInvoiceeByInvoicingPartyID() TABLE NAME:MISCELLANEOUS_INVOICE"

    <OperationContract()> _
    Public Function pub_GetMiscellaneousInvoiceeByInvoicingPartyID(ByVal bv_i64CustomerID As Int64, _
                                                                   ByVal bv_datPeriodFrom As DateTime, _
                                                                   ByVal bv_datPeriodTo As DateTime, _
                                                                   ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetMiscellaneousInvoiceeByInvoicingPartyID(bv_i64CustomerID, _
                                                                                                  bv_datPeriodFrom, _
                                                                                                  bv_datPeriodTo, _
                                                                                                  bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetHeatingChargeByCustomerId() TABLE NAME:HEATING_CHARGE"

    <OperationContract()> _
    Public Function pub_GetHeatingChargeByCustomerId(ByVal bv_i64CustomerID As Int64, _
                                                     ByVal bv_datPeriodFrom As DateTime, _
                                                     ByVal bv_datPeriodTo As DateTime, _
                                                     ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetHeatingChargeByCustomerId(bv_i64CustomerID, _
                                                                                    bv_datPeriodFrom, _
                                                                                    bv_datPeriodTo, _
                                                                                    bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerByDepotIdCustomerID() TABLE NAME:V_CUSTOMER"

    <OperationContract()> _
    Public Function pub_GetCustomerByDepotIdCustomerID(ByVal bv_i64ServiceId As Int64, _
                                                       ByVal bv_strCustomerType As String, _
                                                       ByVal bv_i32DepotId As Int32, _
                                                       ByRef br_dtCustomer As DataTable) As Boolean

        Try
            Dim objCommonUIs As New CommonUIs
            br_dtCustomer = objCommonUIs.GetCustomerByDepotIdCustomerID(bv_i64ServiceId, _
                                                                        bv_strCustomerType, _
                                                                        bv_i32DepotId).Tables(InvoiceGenerationData._V_CUSTOMER)


            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetExchangeRateByDepotId() TABLE NAME:V_EXCHANGE_RATE"

    <OperationContract()> _
    Public Function pub_GetExchangeRateByDepotId(ByVal bv_i64ServiceId As Int64, _
                                                 ByVal bv_strCustomerType As String, _
                                                 ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetExchangeRateByDepotId(bv_i64ServiceId, _
                                                                                bv_strCustomerType, _
                                                                                bv_i32DepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateMiscInvoiceNoBillingFlag() TABLE NAME:MISCELLANEOUS_INVOICE"
    <OperationContract()> _
    Public Function pub_UpdateMiscInvoiceNoBillingFlag(ByVal bv_strTable1PrimaryID As String, _
                                                       ByVal bv_strTable2PrimaryID As String, _
                                                       ByVal bv_strBillingFlag As String, _
                                                       ByRef bv_strInvoiceNo As String, _
                                                       ByVal bv_i64CustomerCurrencyID As Int64, _
                                                       ByVal bv_i64DepotCurrencyID As Int64, _
                                                       ByVal bv_decExchangeRate As Decimal, _
                                                       ByVal bv_datFromDate As Date, _
                                                       ByVal bv_datToDate As Date, _
                                                       ByVal bv_dblTotalCustomerAmount As Double, _
                                                       ByVal bv_dblTotalDepotAmount As Double, _
                                                       ByVal bv_i64CustomerID As Int64, _
                                                       ByVal bv_i64InvoicingPartyID As Int64, _
                                                       ByVal bv_i32DepotID As Int32, _
                                                       ByVal bv_strUserName As String, _
                                                       ByVal bv_blnGenerateInvoiceNo As Boolean, _
                                                       ByVal bv_i32InvoiceTypeID As Int32, _
                                                       ByVal bv_strInvoiceType As String, _
                                                       ByRef br_strInvoiceFileName As String, _
                                                       ByVal intCountEquipment As Integer, _
                                                       ByVal bv_dsInvoiceGeneration As DataSet) As Boolean
        Try
            Dim objCommonUIs As New CommonUIs

            objCommonUIs.pub_UpdateInvoiceTableInvoiceNoBillingFlag(bv_strTable1PrimaryID, _
                                                                    bv_strTable2PrimaryID, _
                                                                    bv_strBillingFlag, _
                                                                    bv_strInvoiceNo, _
                                                                    bv_i64CustomerCurrencyID, _
                                                                    bv_i64DepotCurrencyID, _
                                                                    bv_decExchangeRate, _
                                                                    bv_datFromDate, _
                                                                    bv_datToDate, _
                                                                    bv_dblTotalCustomerAmount, _
                                                                    bv_dblTotalDepotAmount, _
                                                                    bv_i64CustomerID, _
                                                                    bv_i64InvoicingPartyID, _
                                                                    bv_i32DepotID, _
                                                                    bv_strUserName, _
                                                                    bv_blnGenerateInvoiceNo, _
                                                                    bv_i32InvoiceTypeID, _
                                                                    bv_strInvoiceType, _
                                                                    "Draft", _
                                                                    String.Empty, _
                                                                    br_strInvoiceFileName, _
                                                                    String.Empty, _
                                                                    String.Empty, _
                                                                    bv_dsInvoiceGeneration, _
                                                                    intCountEquipment,
                                                                    "", _
                                                                    False, Nothing, Nothing)

            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))

        End Try
    End Function
#End Region

#Region "pub_GetInvoice_HistoryByFromToBillingDate() TABLE NAME:Invoice_History"

    <OperationContract()> _
    Public Function pub_GetInvoice_HistoryByFromToBillingDate(ByVal bv_i64CustomerId As Int64, _
                                                              ByVal bv_strBillingFlag As String, _
                                                              ByVal bv_i32DepotId As Int32, _
                                                              ByVal bv_datFromBillingDate As Date, _
                                                              ByVal bv_datToBillingDate As Date, _
                                                              ByVal bv_i64InvoicingPartyId As Int64, _
                                                              ByVal bv_strInvoiceType As String) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetInvoice_HistoryByFromToBillingDate(bv_i64CustomerId, _
                                                                                             bv_strBillingFlag, _
                                                                                             bv_i32DepotId, _
                                                                                             bv_datFromBillingDate, _
                                                                                             bv_datToBillingDate, _
                                                                                             bv_i64InvoicingPartyId, _
                                                                                             bv_strInvoiceType)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_fillDatasetforInvoiceGeneration"
    <OperationContract()> _
    Public Function pub_fillDatasetforInvoiceGeneration(ByVal bv_i32InvoiceTypeID As Int32, _
                                                        ByRef bv_dtInvoicetable As DataTable, _
                                                        ByVal bv_decExchangeRate As Decimal, _
                                                        ByVal bv_datFromDate As Date, _
                                                        ByVal bv_datToDate As Date, _
                                                        ByRef bv_decCustomerAmount As Decimal, _
                                                        ByRef bv_decDepotAmount As Decimal, _
                                                        Optional ByVal bv_i32CustomerID As Int32 = 0, _
                                                        Optional ByRef bv_dsDataset As DataSet = Nothing, _
                                                        Optional ByVal bv_i32DepotID As Int32 = 0, _
                                                        Optional ByVal str_067InvoiceGenerationGWSBit As String = Nothing) As Boolean

        Try
            Dim objInvoiceGeneration As New CommonUIs
            objInvoiceGeneration.fillDatasetforInvoiceGeneration(bv_i32InvoiceTypeID, _
                                                                 bv_dtInvoicetable, _
                                                                 bv_decExchangeRate, _
                                                                 bv_datFromDate, _
                                                                 bv_datToDate, _
                                                                 bv_decCustomerAmount, _
                                                                 bv_decDepotAmount, _
                                                                 bv_i32CustomerID, _
                                                                 bv_dsDataset, _
                                                                 bv_i32DepotID, _
                                                                 str_067InvoiceGenerationGWSBit)
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetVInvoiceHistoryByInvoiceTypeCustomerBillingFlag() TABLE NAME:V_INVOICE_HISTORY"

    <OperationContract()> _
    Public Function pub_GetVInvoiceHistoryByInvoiceTypeCustomerBillingFlag(ByVal bv_i32DepotID As Int64, _
                                                                           ByVal bv_strBilingFlag As String, _
                                                                           ByVal bv_strInvoiceType As String, _
                                                                           ByVal bv_i64CustomerId As Int64, _
                                                                           ByVal bv_i64InvoicingPartyId As Int64) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGenerationDataSet As InvoiceGenerationDataSet
            Dim objInvoiceGenerations As New InvoiceGenerations

            dsInvoiceGenerationDataSet = objInvoiceGenerations.GetVInvoiceHistoryByInvoiceTypeCustomerBillingFlag(bv_i32DepotID, bv_strBilingFlag, _
                                                                                                                  bv_strInvoiceType, bv_i64CustomerId, _
                                                                                                                  bv_i64InvoicingPartyId)
            Return dsInvoiceGenerationDataSet
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetActivityStatusByCustomer() TABLE NAME:ACTIVITY_STATUS"

    <OperationContract()> _
    Public Function pub_GetActivityStatusByCustomer(ByVal bv_i32DepotID As Int64, _
                                                    ByVal bv_i64CustomerId As Int64, _
                                                    ByRef bv_strFromDate As String, _
                                                    ByVal bv_strActivityName As String, _
                                                    ByVal bv_strBillingFlag As String) As Boolean

        Try
            Dim dtActivityStatus As New DataTable
            Dim objInvoiceGenerations As New InvoiceGenerations
            dtActivityStatus = objInvoiceGenerations.GetActivityStatusByCustomer(bv_i32DepotID, bv_i64CustomerId, bv_strBillingFlag)
            If bv_strActivityName = "GateIn" Then
                If dtActivityStatus.Rows.Count > 0 AndAlso Not IsDBNull(dtActivityStatus.Rows(0).Item(InvoiceGenerationData.GTN_DT)) Then
                    bv_strFromDate = CDate(dtActivityStatus.Rows(0).Item(InvoiceGenerationData.GTN_DT)).ToString("dd-MMM-yyy")
                End If
            ElseIf bv_strActivityName = "Inspection" Then
                If dtActivityStatus.Rows.Count > 0 AndAlso Not IsDBNull(dtActivityStatus.Rows(0).Item(CleaningData.INSPCTN_DT)) Then
                    bv_strFromDate = CDate(dtActivityStatus.Rows(0).Item(CleaningData.INSPCTN_DT)).ToString("dd-MMM-yyy")
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    <OperationContract()> _
    Public Function GetActivityStatusByAgent(ByVal bv_i32DepotID As Int64, _
                                                    ByVal bv_i64AgentId As Int64, _
                                                    ByRef bv_strFromDate As String) As Boolean

        Try
            Dim dtActivityStatus As New DataTable
            Dim objInvoiceGenerations As New InvoiceGenerations
            dtActivityStatus = objInvoiceGenerations.GetActivityStatusByAgent(bv_i32DepotID, bv_i64AgentId)
            If dtActivityStatus.Rows.Count > 0 AndAlso Not IsDBNull(dtActivityStatus.Rows(0).Item(InvoiceGenerationData.GTN_DT)) Then
                bv_strFromDate = CDate(dtActivityStatus.Rows(0).Item(InvoiceGenerationData.GTN_DT)).ToString("dd-MMM-yyy")
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetTransportationChargeByCustomerId () TABLE NAME:HEATING_CHARGE"

    <OperationContract()> _
    Public Function pub_GetTransportationChargeByCustomerId(ByVal bv_i64CustomerID As Int64, _
                                                            ByVal bv_datPeriodFrom As DateTime, _
                                                            ByVal bv_datPeriodTo As DateTime, _
                                                            ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetTransportationChargeByCustomerId(bv_i64CustomerID, _
                                                                                           bv_datPeriodFrom, _
                                                                                           bv_datPeriodTo, _
                                                                                           bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRentalChargeByCustomerId() TABLE NAME:RENTAL_CHARGE"

    <OperationContract()> _
    Public Function pub_GetRentalChargeByCustomerId(ByVal bv_i64CSTMR_ID As Int64, _
                                                      ByVal bv_datPeriodFrom As DateTime, _
                                                      ByVal bv_datPeriodTo As DateTime, _
                                                      ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetRentalChargeByCustomerId(bv_i64CSTMR_ID, _
                                                                                     bv_datPeriodFrom, _
                                                                                     bv_datPeriodTo, _
                                                                                     bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRentalOnHireDateByCustomer() TABLE NAME:RENTAL_ENTRY"

    <OperationContract()> _
    Public Function pub_GetRentalOnHireDateByCustomer(ByVal bv_i32DepotID As Int64, _
                                                    ByVal bv_i64CustomerId As Int64, _
                                                    ByRef bv_strFromDate As String) As Boolean

        Try
            Dim dtRentalEntry As New DataTable
            Dim objInvoiceGenerations As New InvoiceGenerations
            dtRentalEntry = objInvoiceGenerations.GetRentalOnHireDateByCustomer(bv_i32DepotID, bv_i64CustomerId)
            If dtRentalEntry.Rows.Count > 0 AndAlso Not IsDBNull(dtRentalEntry.Rows(0).Item(InvoiceGenerationData.ON_HR_DT)) Then
                bv_strFromDate = CDate(dtRentalEntry.Rows(0).Item(InvoiceGenerationData.ON_HR_DT)).ToString("dd-MMM-yyy")
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    ''Validation 22294
#Region "pub_validateFinalizedInvoice()"
    <OperationContract()> _
    Public Function pub_validateFinalizedInvoice(ByVal bv_i32DepotID As Int64, _
                                                 ByVal bv_i64InvoicePartyId As Int64, _
                                                 ByVal bv_strInvoiceType As String, _
                                                 ByVal bv_strFromDate As String, _
                                                 ByRef br_strErrorMessage As String) As Boolean
        Try
            Dim objInvoiceGenerations As New InvoiceGenerations
            Dim strToBillingDate As String = ""
            strToBillingDate = objInvoiceGenerations.validateFinalizedInvoice(bv_i32DepotID, bv_i64InvoicePartyId, bv_strInvoiceType)
            If strToBillingDate <> Nothing AndAlso strToBillingDate <> "" Then
                If CDate(bv_strFromDate) <= CDate(strToBillingDate) Then
                    br_strErrorMessage = "True"
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_validateFinalizedInvoiceByEquipmentActivity()"
    <OperationContract()> _
    Public Function pub_validateFinalizedInvoiceByEquipmentTransactionID(ByVal bv_i32DepotID As Int64, _
                                                 ByVal bv_strTable1PrimaryID As String, _
                                                 ByVal bv_strInvoiceType As String, _
                                                 ByRef br_strErrorMessage As String) As Boolean
        Try
            Dim objInvoiceGenerations As New InvoiceGenerations
            Dim i32TransactionCount As Int32 = 0
            Dim strQuery As String = ""
            If bv_strInvoiceType = "CI" Then
                strQuery = String.Concat("SELECT COUNT(CLNNG_CHRG_ID) FROM ", InvoiceGenerationData._CLEANING_CHARGE, " WHERE ACTV_BT=1 AND BLLNG_FLG='B' AND CLNNG_CHRG_ID IN (", bv_strTable1PrimaryID, ") AND DPT_ID=", bv_i32DepotID)
            ElseIf bv_strInvoiceType = "HT" Then
                strQuery = String.Concat("SELECT COUNT(HTNG_ID) FROM ", InvoiceGenerationData._HEATING_CHARGE, " WHERE  BLLNG_FLG='B' AND HTNG_ID IN (", bv_strTable1PrimaryID, ") AND DPT_ID=", bv_i32DepotID)
            ElseIf bv_strInvoiceType = "MI" Then
                strQuery = String.Concat("SELECT COUNT(MSCLLNS_INVC_ID) FROM ", InvoiceGenerationData._MISCELLANEOUS_INVOICE, " WHERE  BLLNG_FLG='B' AND MSCLLNS_INVC_ID IN (", bv_strTable1PrimaryID, ") AND DPT_ID=", bv_i32DepotID)
            ElseIf bv_strInvoiceType = "TP" Then
                strQuery = String.Concat("SELECT COUNT(TRNSPRTTN_CHRG_ID) FROM ", InvoiceGenerationData._TRANSPORTATION_CHARGE, " WHERE  BLLNG_FLG='B' AND TRNSPRTTN_CHRG_ID IN (", bv_strTable1PrimaryID, ") AND DPT_ID=", bv_i32DepotID)
            ElseIf bv_strInvoiceType = "RP" Then
                strQuery = String.Concat("SELECT COUNT(RPR_CHRG_ID) FROM ", InvoiceGenerationData._REPAIR_CHARGE, " WHERE  BLLNG_FLG='B' AND RPR_CHRG_ID IN (", bv_strTable1PrimaryID, ") AND DPT_ID=", bv_i32DepotID)
            ElseIf bv_strInvoiceType = "IN" Then
                strQuery = String.Concat("SELECT COUNT(INSPCTN_CHRG_ID) FROM ", InvoiceGenerationData._V_INSPECTION_CHARGES, " WHERE ACTV_BT=1 AND BLLNG_FLG='B' AND INSPCTN_CHRG_ID IN (", bv_strTable1PrimaryID, ") AND DPT_ID=", bv_i32DepotID)

            End If
            If strQuery <> "" Then
                i32TransactionCount = objInvoiceGenerations.validateFinalizedInvoiceTransactionWise(strQuery)
            End If
            If i32TransactionCount > 0 Then
                br_strErrorMessage = "True"
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
    ''End validation

    'Finance integrations
#Region "pub_GetMiscellaneousInvoiceeByInvoicingPartyID() TABLE NAME:MISCELLANEOUS_INVOICE"

    <OperationContract()> _
    Public Function GetCreditByInvoicingPartyID(ByVal bv_i64CustomerID As Int64, _
                                                                   ByVal bv_datPeriodFrom As DateTime, _
                                                                   ByVal bv_datPeriodTo As DateTime, _
                                                                   ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetCreditByInvoicingPartyID(bv_i64CustomerID, _
                                                                                   bv_datPeriodFrom, _
                                                                                   bv_datPeriodTo, _
                                                                                   bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

    'GWS Inspection
#Region "pub_GetInspectionChargeByCustomerId() TABLE NAME:V_INSPECTION_CHARGES"

    <OperationContract()> _
    Public Function pub_GetInspectionChargeByCustomerId(ByVal bv_i64CustomerID As Int64, _
                                                      ByVal bv_datPeriodFrom As DateTime, _
                                                      ByVal bv_datPeriodTo As DateTime, _
                                                      ByVal bv_intDepotId As Int32) As InvoiceGenerationDataSet

        Try
            Dim dsInvoiceGeneration As InvoiceGenerationDataSet
            Dim objInvoiceGeneration As New InvoiceGenerations
            dsInvoiceGeneration = objInvoiceGeneration.GetInspectionChargeByCustomerId(bv_i64CustomerID, _
                                                                                     bv_datPeriodFrom, _
                                                                                     bv_datPeriodTo, _
                                                                                     bv_intDepotId)
            Return dsInvoiceGeneration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET :pub_GetExchangeRateWithEffectivedate() "
    ''' <summary>
    ''' Get Exchange Rate from exchange rate master
    ''' </summary>
    ''' <param name="bv_lngFromCurrencyId"></param>
    ''' <param name="bv_lngToCurrencyId"></param>
    ''' <param name="bv_datCurrentDateTime"></param>
    ''' <param name="bv_intDepotId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetExchangeRateWithEffectivedate(ByVal bv_lngFromCurrencyId As Int64, ByVal bv_lngToCurrencyId As Int64, ByVal bv_datCurrentDateTime As DateTime, ByVal bv_intDepotId As Int32) As DataTable

        Try
            Dim dtExchangeRate As New DataTable
            Dim objInvoiceGenerations As New InvoiceGenerations
            dtExchangeRate = objInvoiceGenerations.GetExchangeRateWithEffectiveDate(bv_lngFromCurrencyId, bv_lngToCurrencyId, bv_datCurrentDateTime, bv_intDepotId)
            Return dtExchangeRate
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

    'Exchange Rate for Multiple Locxation
#Region "GetExchangeRateForMultilocation"
    <OperationContract()> _
    Public Function pub_GetExchangeRateForMultilocation(ByVal bv_intTO_CRRNCY_ID As Int64, ByVal bv_intFRM_CRRNCY_ID As Int64) As String
        Try
            Dim objCommonUIs As New CommonUIs
            Return objCommonUIs.GetExchangeRateForMultilocation(bv_intTO_CRRNCY_ID, bv_intFRM_CRRNCY_ID)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

    <OperationContract()> _
    Public Function pub_GetCleaningChargeStatusByInvoicingParty(ByVal bv_i32DepotID As Int64, _
                                                    ByVal bv_i64CustomerId As Int64, _
                                                    ByRef bv_strFromDate As String, _
                                                    ByVal bv_strActivityName As String, _
                                                    ByVal bv_strBillingFlag As String) As Boolean

        Try
            Dim dtActivityStatus As New DataTable
            Dim objInvoiceGenerations As New InvoiceGenerations
            dtActivityStatus = objInvoiceGenerations.GetCleaningChargeByInvoicingParty(bv_i32DepotID, bv_i64CustomerId, bv_strBillingFlag)
            If dtActivityStatus.Rows.Count > 0 AndAlso Not IsDBNull(dtActivityStatus.Rows(0).Item(CleaningData.ORGNL_INSPCTD_DT)) Then
                bv_strFromDate = CDate(dtActivityStatus.Rows(0).Item(CleaningData.ORGNL_INSPCTD_DT)).ToString("dd-MMM-yyy")
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
