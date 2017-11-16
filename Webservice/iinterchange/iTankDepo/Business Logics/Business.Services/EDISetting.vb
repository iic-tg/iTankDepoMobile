Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization

<ServiceContract()> _
Public Class EDISetting
    Dim objEDISetting As EDISettings

#Region "GetCustomerNameAndMailId"
    <OperationContract()> _
    Public Function pvt_GetCustomerEmailFormat() As EDIDataSet
        Try
            Dim dsEDISetting As EDIDataSet
            Dim objEDISettings As New EDISettings
            dsEDISetting = objEDISettings.GetCustomerEmailFormat()
            Return dsEDISetting
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_UpdateNextRunTime"
    <OperationContract()> _
    Public Function pvt_UpdateNextRunTime(ByVal bv_intCSMR_ID As Integer, _
                                      ByVal bv_dtLST_RN As DateTime, _
                                      ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim objEDISettings As New EDISettings
            Dim blnUpdated As Boolean
            blnUpdated = objEDISettings.UpdateNextRunTime(bv_intCSMR_ID, bv_dtLST_RN, bv_intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region




End Class