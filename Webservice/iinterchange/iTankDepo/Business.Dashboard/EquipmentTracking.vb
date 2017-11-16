Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
<ServiceContract()> _
Public Class EquipmentTracking
#Region "pub_GetVEquipmentTrackingByDepotID() TABLE NAME:V_EQUIPMENT_TRACKING"

    <OperationContract()> _
    Public Function pub_GetVEquipmentTrackingByDepotID(ByVal bv_i32DepotId As Int32) As EquipmentTrackingDataSet
        Try
            Dim dsDashboardDataSet As EquipmentTrackingDataSet
            Dim objDashboards As New EquipmentTrackings
            dsDashboardDataSet = objDashboards.GetVEquipmentTrackingBy(bv_i32DepotId)
            Return dsDashboardDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
End Class
