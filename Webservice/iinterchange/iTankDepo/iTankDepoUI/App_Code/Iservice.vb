Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Iservice" in both code and config file together.
<ServiceContract()>
Public Interface Iservice

    <OperationContract()>
    Function GetAttachmentsByRepairEstimateNo(depotId As Int64, estimateNo As String, estimateID As Int64, filterUpload As String) As DataSet

    <OperationContract()>
    Function GetAttachmentsByActivityName(ByVal depotId As Int64, ByVal estimateNo As String, ByVal activityName As String, ByVal uploadFilter As String) As DataSet
End Interface
