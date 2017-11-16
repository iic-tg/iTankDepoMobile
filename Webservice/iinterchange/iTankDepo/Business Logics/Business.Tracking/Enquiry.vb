Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class Enquiry

#Region "pub_GetProductDetails()"
    <OperationContract()> _
    Public Function pub_GetProductDetails(ByVal bv_DeoptID As Integer) As EnquiryDataSet
        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnqirys As New Enquirys
            dsEnquiryDataSet = objEnqirys.GetProductDetails(bv_DeoptID)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetVCustomerByDepotId() TABLE NAME:V_CUSTOMER"

    <OperationContract()> _
    Public Function pub_GetVCustomerByDepotId(ByVal bv_depotId As Integer) As EnquiryDataSet

        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnqirys As New Enquirys
            dsEnquiryDataSet = objEnqirys.GetV_CustomerByDepot(bv_depotId)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetProductCleaningRateByDepotID() TABLE NAME:V_ENQUIRY_PRODUCT_CLEANING_RATE"

    <OperationContract()> _
    Public Function pub_GetProductCleaningRateByDepotID(ByVal intDepotId As Int64) As EnquiryDataSet

        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnquirys As New Enquirys
            dsEnquiryDataSet = objEnquirys.GetV_Enquiry_Product_Cleaning_Rate(intDepotId)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerCleaningRateByDepotID() TABLE NAME:V_ENQUIRY_CUSTOMER_CLEANING_RATE"

    <OperationContract()> _
    Public Function pub_GetCustomerCleaningRateByDepotID(ByVal intDepotId As Int64) As EnquiryDataSet

        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnquirys As New Enquirys
            dsEnquiryDataSet = objEnquirys.GetCleaning_RateBy(intDepotId)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerChargeByDepotID() TABLE NAME:V_ENQUIRY_CUSTOMER_CHARGE"

    <OperationContract()> _
    Public Function pub_GetCustomerChargeByDepotID(ByVal intCustomerId As Integer, ByVal intDepotId As Int64) As EnquiryDataSet

        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnquirys As New Enquirys
            dsEnquiryDataSet = objEnquirys.GetV_Enquiry_Customer_Charge(intCustomerId, intDepotId)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerCleaningSlabByDepotID"
#Region "pub_GetCustomerCleaningSlabByDepotID() TABLE NAME:V_ENQUIRY_CUSTOMER_CHARGE"

    <OperationContract()> _
    Public Function pub_GetCustomerCleaningSlabByDepotID(ByVal intCustomerId As Integer, ByVal intDepotId As Int64) As EnquiryDataSet

        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnquirys As New Enquirys
            dsEnquiryDataSet = objEnquirys.GetV_Enquiry_Customer_CleaningSlab_Charge(intCustomerId, intDepotId)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
#End Region
#Region "pub_GetV_CustomerByCustomerID() TABLE NAME:V_CUSTOMER"

    <OperationContract()> _
    Public Function pub_GetV_CustomerByCustomerID(ByVal intCustomerId As Integer, ByVal bv_intDepotID As Integer) As EnquiryDataSet

        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnquirys As New Enquirys
            dsEnquiryDataSet = objEnquirys.GetVCustomer(intCustomerId, bv_intDepotID)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRentalCustomerDetails()"

    <OperationContract()> _
    Public Function pub_GetRentalCustomerDetails(ByVal intCustomerId As Integer) As EnquiryDataSet

        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnquirys As New Enquirys
            dsEnquiryDataSet = objEnquirys.pub_GetRentalCustomerDetails(intCustomerId)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerTransportation()"

    <OperationContract()> _
    Public Function pub_GetCustomerTransportation(ByVal intCustomerId As Integer) As EnquiryDataSet

        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnquirys As New Enquirys
            dsEnquiryDataSet = objEnquirys.pub_GetCustomerTransportation(intCustomerId)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_RouteDetailByDepotId()"

    <OperationContract()> _
    Public Function pub_RouteDetailByDepotId(ByVal bv_depotId As Integer) As EnquiryDataSet

        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnqirys As New Enquirys
            dsEnquiryDataSet = objEnqirys.pub_RouteDetailByDepotId(bv_depotId)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRouteByRouteID()"

    <OperationContract()> _
    Public Function pub_GetRouteByRouteID(ByVal bv_RouteID As Integer, ByVal bv_depotId As Integer) As EnquiryDataSet

        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnqirys As New Enquirys
            dsEnquiryDataSet = objEnqirys.pub_GetRouteByRouteID(bv_RouteID, bv_depotId)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerRouteDetails()"

    <OperationContract()> _
    Public Function pub_GetCustomerRouteDetails(ByVal bv_RouteID As Integer, ByVal bv_depotId As Integer) As EnquiryDataSet

        Try
            Dim dsEnquiryDataSet As EnquiryDataSet
            Dim objEnqirys As New Enquirys
            dsEnquiryDataSet = objEnqirys.pub_GetCustomerRouteDetails(bv_RouteID, bv_depotId)
            Return dsEnquiryDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetTransporterRouteDetailByRouteID() TABLE NAME:TRANSPORTER_ROUTE_DETAIL"

    <OperationContract()> _
    Public Function pub_GetTransporterRouteDetailByRouteID(ByVal bv_i64RouteID As Int64, _
                                                           ByVal bv_intDepotId As Integer) As EnquiryDataSet

        Try
            Dim dsEnquiry As EnquiryDataSet
            Dim objEnquirys As New Enquirys
            dsEnquiry = objEnquirys.GetTransporterRouteDetailByRouteID(bv_i64RouteID, bv_intDepotId)
            Return dsEnquiry
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


End Class
