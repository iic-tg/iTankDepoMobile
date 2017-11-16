Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
<ServiceContract()> _
Public Class OperationsTurnOver
#Region "pub_GetOperationsTurnOverByDepotID()"

    <OperationContract()> _
    Public Function pub_GetOperationsTurnOverByDepotID(ByVal bv_intDepotId As Integer) As OperationsTurnOverDataSet
        Try
            Dim dsOperationsTurnOverDataSet As OperationsTurnOverDataSet
            Dim objOperationsTurnOvers As New OperationsTurnOvers
            dsOperationsTurnOverDataSet = objOperationsTurnOvers.GetOperationsTurnOverByDepotID(bv_intDepotId)
            Return dsOperationsTurnOverDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class