Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

<ServiceContract()> _
Public Class ViewXmlEdi

#Region "pub_GetInvoiceXmlEDIByCustomerId() TABLE NAME:INVOICE_EDI_HISTORY"



    <OperationContract()> _
    Public Function pub_GetInvoiceXmlEDIByCustomerId(ByVal bv_i64CSTMR_ID As Int64, _
                                                      ByVal bv_datPeriodFrom As DateTime, _
                                                      ByVal bv_datPeriodTo As DateTime, _
                                                      ByVal bv_intDepotId As Int32,
                                                      ByVal bv_strActivityname As String,
                                                      ByVal bv_strStatus As String) As ViewXmlEdiDataSet

        Try
            Dim dsViewXmlEdi As ViewXmlEdiDataSet
            Dim objViewXmlEdi As New ViewXmlEdis
            dsViewXmlEdi = objViewXmlEdi.GetInvoiceEDIHistoryByByCustomerID(bv_i64CSTMR_ID, bv_datPeriodFrom, bv_datPeriodTo, bv_intDepotId, bv_strActivityname, bv_strStatus)
            Return dsViewXmlEdi
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetInvoiceXmlEDIDetail() TABLE NAME:INVOICE_EDI_HISTORY_DETAIL"
    <OperationContract()> _
    Public Function pub_GetInvoiceXmlEDIDetail(ByVal bv_intEdiId As Integer) As DataTable

        Try
            Dim dtEDIDetail As New DataTable
            Dim objViewXmlEdi As New ViewXmlEdis
            dtEDIDetail = objViewXmlEdi.GetInvoiceXmlEDIDetail(bv_intEdiId)
            Return dtEDIDetail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateXmlStatus() TABLE NAME:INVOICE_EDI_HISTORY"

    <OperationContract()> _
    Public Function pub_UpdateXmlStatus(ByVal bv_strFilename As String, ByVal bv_strStatus As String, ByVal bv_strError As String, ByVal bv_datSentDate As DateTime) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim blnStatus As Boolean
            Dim objViewXmlEdi As New ViewXmlEdis
            blnStatus = objViewXmlEdi.UpdateXmlStatus(bv_strFilename, bv_strStatus, bv_strError, bv_datSentDate, objTrans)
            objTrans.commit()

        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateLineResponse:INVOICE_EDI_HISTORY_DEATIL"
    <OperationContract()> _
    Public Function pub_UpdateLineResponse(ByVal bv_strInvoiceNo As String, ByVal bv_strMoveNo As String, ByVal bv_strSupportUrl As String, ByVal bv_strLineResponse As String) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim blnStatus As Boolean
            Dim objViewXmlEdi As New ViewXmlEdis
            blnStatus = objViewXmlEdi.UpdateLineResponse(bv_strInvoiceNo, bv_strMoveNo, bv_strSupportUrl, bv_strLineResponse, objTrans)
            objTrans.commit()

        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateHeaderResponse:INVOICE_EDI_HISTORY"
    <OperationContract()> _
    Public Function pub_UpdateHeaderResponse(ByVal bv_strIFileName As String, ByVal bv_strInvoiceNo As String, ByVal bv_strHeaderResponse As String, ByVal bv_strStatus As String) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim blnStatus As Boolean
            Dim objViewXmlEdi As New ViewXmlEdis
            blnStatus = objViewXmlEdi.UpdateHeaderResponse(bv_strIFileName, bv_strInvoiceNo, bv_strHeaderResponse, bv_strStatus, Now, objTrans)
            objTrans.commit()
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetCustomerFtpCredentials:CUSTOMER"
    <OperationContract()> _
    Public Function GetCustomerFtpCredentials() As DataTable

        Try
            Dim dtCustomer As New DataTable
            Dim objViewXmlEdi As New ViewXmlEdis
            dtCustomer = objViewXmlEdi.GetCustomerFtpDetails()
            Return dtCustomer
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetPdfDetails:INVOICE_EDI_HISTORY"
    <OperationContract()> _
    Public Function GetPdfDetails(ByVal bv_intCustomerId As Integer) As DataTable

        Try
            Dim dtCustomer As New DataTable
            Dim objViewXmlEdi As New ViewXmlEdis
            dtCustomer = objViewXmlEdi.GetPdfDetails(bv_intCustomerId)
            Return dtCustomer
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdatePDFStatus:INVOICE_EDI_HISTORY"
    <OperationContract()> _
    Public Function pub_UpdatePDFStatus(ByVal bv_strFilename As String) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim blnStatus As Boolean
            Dim objViewXmlEdi As New ViewXmlEdis
            blnStatus = objViewXmlEdi.UpdatePDFStatus(bv_strFilename, Now, objTrans)
            objTrans.commit()
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateStatus:INVOICE_EDI_HISTORY"

    <OperationContract()> _
    Public Function pub_UpdateStatus(ByVal bv_strInvoiceNo As String, ByVal bv_strStatus As String) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim blnStatus As Boolean
            Dim objViewXmlEdi As New ViewXmlEdis
            blnStatus = objViewXmlEdi.UpdateInvoiceStatus(bv_strInvoiceNo, bv_strStatus, objTrans)
            objTrans.commit()
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetDepotDetails:DEPOT"
    <OperationContract()> _
    Public Function GetDepotDetails(ByVal bv_intDepotId As Integer) As DataTable
        Try
            Dim dtDepot As New DataTable
            Dim objViewXmlEdi As New ViewXmlEdis
            dtDepot = objViewXmlEdi.GetDepotDetails(bv_intDepotId)
            Return dtDepot
        Catch ex As Exception

            Throw New FaultException(New FaultReason(ex.Message))
        End Try

    End Function
#End Region

#Region "GetXmlDetailsINVOICE_EDI_HISTORY"
    <OperationContract()> _
    Public Function GetXmlDetails(ByVal bv_intCustomerId As Integer) As DataTable

        Try
            Dim dtCustomer As New DataTable
            Dim objViewXmlEdi As New ViewXmlEdis
            dtCustomer = objViewXmlEdi.GetXmlDetails(bv_intCustomerId)
            Return dtCustomer
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
End Class
