Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

<ServiceContract()> _
Public Class InvoiceParty
#Region "pub_InvoicePartyGetInvoiceParty() TABLE NAME:INVOICE_PARTY"

    <OperationContract()> _
    Public Function pub_InvoicePartyGetInvoiceParty(ByVal bv_strInvoicePartyID As Int64) As InvoicePartyDataSet

        Try
            Dim objTransaction As New Transactions()
            Dim dsInvoicePartyData As InvoicePartyDataSet
            Dim objInvoicePartys As New InvoicePartys
            dsInvoicePartyData = objInvoicePartys.GetInvoicePartyByInvoicePartyID(bv_strInvoicePartyID)
            Return dsInvoicePartyData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_InvoicePartyGetInvoicePartyByDepotId() TABLE NAME:INVOICE_PARTY"

    <OperationContract()> _
    Public Function pub_InvoicePartyGetInvoicePartyByDepotId(ByVal bv_strDepotId As Int32) As InvoicePartyDataSet

        Try
            Dim objTransaction As New Transactions()
            Dim dsInvoicePartyData As InvoicePartyDataSet
            Dim objInvoicePartys As New InvoicePartys
            dsInvoicePartyData = objInvoicePartys.GetInvoicePartyByDepotId(bv_strDepotId)
            Return dsInvoicePartyData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateInvoiceParty() TABLE NAME:INVOICE_PARTY"

    <OperationContract()> _
    Public Function pub_CreateInvoiceParty(ByVal bv_strInvoicePartyCode As String, _
                                           ByVal bv_strInvoicePartyName As String, _
                                           ByVal bv_strContactPersonName As String, _
                                           ByVal bv_strContactJobTitle As String, _
                                           ByVal bv_strContactAddressLine As String, _
                                           ByVal bv_strBillingAddressLine As String, _
                                           ByVal bv_strZipCode As String, _
                                           ByVal bv_strRemarks As String, _
                                           ByVal bv_strPhoneNumber As String, _
                                           ByVal bv_strFaxNumber As String, _
                                           ByVal bv_strReportingEmailID As String, _
                                           ByVal bv_strInvoicingEmailID As String, _
                                           ByVal bv_i64BaseCurrency As Int64, _
                                           ByVal bv_strModifiedBy As String, _
                                           ByVal bv_datModifiedDate As DateTime, _
                                           ByVal bv_i32DepotId As Int32, _
                                           ByVal bv_blnActiveBit As Boolean, _
                                           ByVal bv_FinanceIntegrationBit As Boolean, _
                                           ByVal bv_LedgerId As String, _
                                           ByVal bv_strWfData As String) As Long
        Dim objTransaction As New Transactions()
        Try
            Dim objInvoice_Party As New InvoicePartys
            Dim objCommonUI As New CommonUIs
            Dim lngCreated As Long
            lngCreated = objInvoice_Party.CreateInvoiceParty(bv_strInvoicePartyCode, _
                                                             bv_strInvoicePartyName, _
                                                             bv_strContactPersonName, _
                                                             bv_strContactJobTitle, _
                                                             bv_strContactAddressLine, _
                                                             bv_strBillingAddressLine, _
                                                             bv_strZipCode, _
                                                             bv_strRemarks, _
                                                             bv_strPhoneNumber, _
                                                             bv_strFaxNumber, _
                                                             bv_strReportingEmailID, _
                                                             bv_strInvoicingEmailID, _
                                                             bv_i64BaseCurrency, _
                                                             bv_strModifiedBy, _
                                                             bv_datModifiedDate, _
                                                             bv_strModifiedBy, _
                                                             bv_datModifiedDate, _
                                                             bv_i32DepotId, _
                                                             bv_blnActiveBit, _
                                                             bv_FinanceIntegrationBit, _
                                                             bv_LedgerId, _
                                                             objTransaction)
            objCommonUI.CreateServicePartner(lngCreated, "PARTY", bv_i32DepotId, objTransaction)
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

#Region "UpdateInvoiceParty() TABLE NAME:INVOICE_PARTY"

    <OperationContract()> _
    Public Function UpdateInvoiceParty(ByVal bv_i64InvoicePartyID As Int64, _
                                       ByVal bv_strInvoicePartyCode As String, _
                                       ByVal bv_strInvoicePartyName As String, _
                                       ByVal bv_strContactPersonName As String, _
                                       ByVal bv_strContactJobTitle As String, _
                                       ByVal bv_strContactAddressLine As String, _
                                       ByVal bv_strBillingAddressLine As String, _
                                       ByVal bv_strZipCode As String, _
                                       ByVal bv_strRemarks As String, _
                                       ByVal bv_strPhoneNumber As String, _
                                       ByVal bv_strFaxNumber As String, _
                                       ByVal bv_strReportingEmailID As String, _
                                       ByVal bv_strInvoicingEmailID As String, _
                                       ByVal bv_i64BaseCurrency As Int64, _
                                       ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_i32DepotId As Int32, _
                                       ByVal bv_blnActiveBit As Boolean, _
                                       ByVal bv_FinanceIntegrationBit As Boolean, _
                                       ByVal bv_LedgerId As String, _
                                       ByVal bv_strWfData As String) As Boolean
        Dim objTransaction As New Transactions()
        Try
            Dim objInvoice_Party As New InvoicePartys
            Dim blnUpdated As Boolean
            blnUpdated = objInvoice_Party.UpdateInvoiceParty(bv_i64InvoicePartyID, _
                                                             bv_strInvoicePartyCode, _
                                                             bv_strInvoicePartyName, _
                                                             bv_strContactPersonName, _
                                                             bv_strContactJobTitle, _
                                                             bv_strContactAddressLine, _
                                                             bv_strBillingAddressLine, _
                                                             bv_strZipCode, _
                                                             bv_strRemarks, _
                                                             bv_strPhoneNumber, _
                                                             bv_strFaxNumber, _
                                                             bv_strReportingEmailID, _
                                                             bv_strInvoicingEmailID, _
                                                             bv_i64BaseCurrency, _
                                                             bv_strModifiedBy, _
                                                             bv_datModifiedDate, _
                                                             bv_i32DepotId, _
                                                             bv_blnActiveBit, _
                                                             bv_FinanceIntegrationBit, _
                                                             bv_LedgerId, _
                                                             objTransaction)
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

#Region "pub_InvoicePartyDeleteInvoiceParty() TABLE NAME:INVOICE_PARTY"

    <OperationContract()> _
    Public Function pub_InvoicePartyDeleteInvoiceParty(ByVal bv_i64InvoicePartyID As Int64) As Boolean
        Try
            Dim objTransaction As New Transactions()
            Dim objInvoice_Party As New InvoicePartys
            Dim blnDeleted As Boolean
            blnDeleted = objInvoice_Party.DeleteInvoiceParty(bv_i64InvoicePartyID)
            Return blnDeleted
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class