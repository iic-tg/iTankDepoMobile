Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class DAR

#Region "pub_GetV_Eqpmnt_Actvty() TABLE NAME:V_EQPMNT_ACTVTY"
    <OperationContract()> _
    Public Function pub_GetV_Eqpmnt_Actvty(ByVal bv_intDepotId As Int64, ByVal bv_strCustomerId As Integer) As DARDataSet
        Try
            Dim dsDAR As DARDataSet
            Dim objDARS As New DARs
            dsDAR = objDARS.GetV_Eqpmnt_Actvty(bv_intDepotId, bv_strCustomerId)
            dsDAR = objDARS.GetV_Eqpmnt_Actvty_Stts(bv_intDepotId)
            dsDAR = objDARS.Get_Depot(bv_intDepotId)
            Return dsDAR
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetV_Eqpmnt_Actvty_Stts() TABLE NAME:V_EQPMNT_ACTVTY_STTS"
    <OperationContract()> _
    Public Function pub_GetV_Eqpmnt_Actvty_Stts(ByVal bv_intDepotId As Int64) As DARDataSet
        Try
            Dim dsDAR As DARDataSet
            Dim objDARS As New DARs
            dsDAR = objDARS.GetV_Eqpmnt_Actvty_Stts(bv_intDepotId)
            Return dsDAR
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetV_Inventory() TABLE NAME:V_INVENTORY"
    <OperationContract()> _
    Public Function pub_GetV_Inventory(ByVal bv_intDepotId As Int64, ByVal bv_strCustomerCode As String) As DARDataSet
        Try
            Dim dsDAR As DARDataSet
            Dim objDARS As New DARs
            dsDAR = objDARS.GetV_Inventory(bv_intDepotId, bv_strCustomerCode)
            dsDAR = objDARS.Get_Depot(bv_intDepotId)
            Return dsDAR
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetDAR() TABLE NAME:DEPOT"
    <OperationContract()> _
    Public Function pub_Get_Depot(ByVal bv_intDepotId As Int64) As DARDataSet
        Try
            Dim dsDAR As DARDataSet
            Dim objDARS As New DARs
            dsDAR = objDARS.Get_Depot(bv_intDepotId)
            Return dsDAR
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomer() TABLE NAME:DEPOT"
    <OperationContract()> _
    Public Function pub_GetCustomer(ByVal bv_intDepotId As Int64) As DARDataSet
        Try
            Dim dsDAR As DARDataSet
            Dim objDARS As New DARs
            dsDAR = objDARS.Get_Customer(bv_intDepotId)
            Return dsDAR
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomer_inventory() TABLE NAME:DEPOT"
    <OperationContract()> _
    Public Function pub_GetCustomer_inventory(ByVal bv_intDepotId As Int64) As DARDataSet
        Try
            Dim dsDAR As DARDataSet
            Dim objDARS As New DARs
            dsDAR = objDARS.Get_Customer_inventory(bv_intDepotId)
            Return dsDAR
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
#Region "pub_UpdateRPT_Status() TABLE NAME:EQUIPMENT_ACTIVITY"
    <OperationContract()> _
    Public Function pub_UpdateRPT_Status(ByVal bv_intDepotId As Int64, ByVal bv_intCustomerID As Int64) As Boolean
        Try
            '  Dim dsDAR As DARDataSet
            Dim objDARS As New DARs
            objDARS.UpdateRPT_Status(bv_intDepotId, bv_intCustomerID)
            '    Return dsDAR
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEmailSetting() "
    <OperationContract()> _
    Public Function pub_GetEmailSetting(ByVal bv_intDepotId As Int64) As DARDataSet
        Try
            Dim dsDAR As DARDataSet
            Dim objDARS As New DARs
            dsDAR = objDARS.GetEmailSetting(bv_intDepotId)
            Return dsDAR
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateCustomerEmailSetting() "
    <OperationContract()> _
    Public Function pub_UpdateCustomerEmailSetting(ByVal bv_intDepotId As Int64, _
                                                   ByVal bv_intCustomerId As Integer, _
                                                   ByVal bv_intReportId As Integer, _
                                                   ByVal dtNextDate As Date, _
                                                   ByVal dtLastDate As Date, _
                                                   ByVal cstmr_email_id As Integer) As DARDataSet
        Try
            Dim dsDAR As DARDataSet
            Dim objDARS As New DARs
            objDARS.pub_UpdateCustomerEmailSetting(bv_intDepotId, _
                                                   bv_intCustomerId, _
                                                   bv_intReportId, _
                                                   dtNextDate, _
                                                   dtLastDate, _
                                                   cstmr_email_id)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetScheduleTime() "
    <OperationContract()> _
    Public Function pub_GetScheduleTime(ByVal intDepotID As Integer) As DARDataSet
        Try
            Dim dsDAR As DARDataSet
            Dim objDARS As New DARs
            dsDAR = objDARS.GetScheduleTime(intDepotID)
            Return dsDAR
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
#Region "pub_UpdateCustomerEmailSetting() "
    <OperationContract()> _
    Public Function pub_UpdateNextRunDate(ByVal bv_intDepotId As Int64, _
                                                   ByVal bv_intCustomerId As Integer, _
                                                   ByVal bv_intReportId As Integer, _
                                                   ByVal dtNextDate As Date, _
                                                   ByVal dtLastDate As Date, _
                                                   ByVal bv_intcustomerEmailSettingbin As Integer) As DARDataSet
        Try
            Dim dsDAR As DARDataSet
            Dim objDARS As New DARs
            objDARS.pub_UpdateNextRunDate(bv_intDepotId, _
                                                   bv_intCustomerId, _
                                                   bv_intReportId, _
                                                   dtNextDate, _
                                                   dtLastDate, _
                                                   bv_intcustomerEmailSettingbin)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
End Class