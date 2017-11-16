
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class GateOutRequest

#Region "GetGateoutReuestRecords() TABLE NAME:Outward_Pass"

    <OperationContract()> _
    Public Function GetGateoutReuestRecords(ByVal bv_DepotId As Int64) As GateOutRequestDataSet
        Try
            Dim objGateOutRequests As New GateOutRequests
            Return objGateOutRequests.GetGateoutReuestRecords(bv_DepotId)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


#Region "UpdateActivity_Status"
    <OperationContract()> _
    Public Function UpdateActivity_Status(ByRef ds As GateOutRequestDataSet, ByVal bv_strUserName As String, ByVal bv_RequestedDate As DateTime) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objGateOutRequests As New GateOutRequests
            Dim statusFlg As Boolean = False

            For Each dr As DataRow In ds.Tables(GateOutData._V_ACTIVITY_STATUS).Select("SLCT='True'")

                objGateOutRequests.UpdateActivity_Status(dr.Item(GateOutRequestData.RQST_APPRVL_CNGNE).ToString(), _
                                                         dr.Item(GateOutRequestData.RQST_APPRVL_RMKRS).ToString(), _
                                                         bv_RequestedDate, _
                                                         bv_strUserName, _
                                                         dr.Item(GateOutRequestData.EQPMNT_NO).ToString(), _
                                                         dr.Item(GateOutRequestData.GI_TRNSCTN_NO).ToString(), _
                                                         CInt(dr.Item(GateOutRequestData.DPT_ID).ToString()), objTrans)
                statusFlg = True

            Next

            objTrans.commit()
            Return statusFlg
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class


