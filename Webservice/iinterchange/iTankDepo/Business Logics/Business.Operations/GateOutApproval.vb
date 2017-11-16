
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class GateOutApproval

    <OperationContract()> _
    Public Function GetGateoutApprovalPending_Records(ByVal bv_DepotId As Int64) As GateOutApprovalDataSet
        Try
            Dim objGateOutApprovals As New GateOutApprovals
            Return objGateOutApprovals.GetGateoutApprovalPending_Records(bv_DepotId)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function GetGateoutApprovalMySubmit_Records(ByVal bv_DepotId As Int64) As GateOutApprovalDataSet
        Try
            Dim objGateOutApprovals As New GateOutApprovals
            Return objGateOutApprovals.GetGateoutApprovalMySubmit_Records(bv_DepotId)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function


#Region "UpdatePendingActivity_Status"
    <OperationContract()> _
    Public Function UpdatePendingActivity_Status(ByRef ds As GateOutApprovalDataSet, ByVal bv_strUserName As String, ByVal bv_RequestedDate As DateTime) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objGateOutApprovals As New GateOutApprovals
            Dim statusFlg As Boolean = False

            For Each dr As DataRow In ds.Tables(GateOutData._V_ACTIVITY_STATUS).Select("GTAPPRVL_BY='True'")

                objGateOutApprovals.UpdatePendingActivity_Status(dr.Item(GateOutRequestData.RQST_APPRVL_CNGNE).ToString(), _
                                                         dr.Item(GateOutRequestData.RQST_APPRVL_RMKRS).ToString(), _
                                                         bv_RequestedDate, _
                                                         bv_strUserName, _
                                                         dr.Item(GateOutRequestData.EQPMNT_NO).ToString(), _
                                                         dr.Item(GateOutRequestData.GI_TRNSCTN_NO).ToString(), _
                                                         dr.Item(ReserveBookingData.BKNG_AUTH_NO).ToString(),
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

#Region "UpdateActivity_Status"
    <OperationContract()> _
    Public Function UpdatemySubmitActivity_Status(ByRef ds As GateOutApprovalDataSet, ByVal bv_strUserName As String, ByVal bv_RequestedDate As DateTime) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objGateOutApprovals As New GateOutApprovals
            Dim statusFlg As Boolean = False

            'For Cancelled Equipments
            For Each dr As DataRow In ds.Tables(GateOutData._V_ACTIVITY_STATUS).Select("CNCL='True'")

                objGateOutApprovals.UpdateSubmitCancelActivity_Status(dr.Item(GateOutRequestData.EQPMNT_NO).ToString(), _
                                                         dr.Item(GateOutRequestData.GI_TRNSCTN_NO).ToString(), _
                                                         CInt(dr.Item(GateOutRequestData.DPT_ID).ToString()), objTrans)
                statusFlg = True

            Next

            'For Selected Equipments
            For Each dr As DataRow In ds.Tables(GateOutData._V_ACTIVITY_STATUS).Select("SLCT='True' and CNCL <> 'True'")

                objGateOutApprovals.UpdateMySubmitActivity_Status(dr.Item(GateOutRequestData.RQST_APPRVL_CNGNE).ToString(), _
                                                         dr.Item(GateOutRequestData.RQST_APPRVL_RMKRS).ToString(), _
                                                         bv_RequestedDate, _
                                                         bv_strUserName, _
                                                         dr.Item(GateOutRequestData.EQPMNT_NO).ToString(), _
                                                         dr.Item(GateOutRequestData.GI_TRNSCTN_NO).ToString(), _
                                                          dr.Item(ReserveBookingData.BKNG_AUTH_NO).ToString(), _
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
