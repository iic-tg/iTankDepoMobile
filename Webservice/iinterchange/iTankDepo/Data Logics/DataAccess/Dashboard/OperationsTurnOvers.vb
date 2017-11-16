Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class OperationsTurnOvers
#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const V_EQPMNT_ACTVTYSelectQueryByDepotId As String = "SELECT MNTH_YR,ACTVTY,ACTVTY_CNT,CSTMR_ID,ACTVTY_DT,DPT_ID  FROM V_EQUIPMENT_ACTIVITY WHERE DPT_ID=@DPT_ID"
    Private ds As OperationsTurnOverDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New OperationsTurnOverDataSet
    End Sub

#End Region

#Region "GetVEqpmntActvtyBy() TABLE NAME:V_EQPMNT_ACTVTY"

    Public Function GetOperationsTurnOverByDepotID(ByVal bv_intDepotId As Integer) As OperationsTurnOverDataSet
        Try
            objData = New DataObjects(V_EQPMNT_ACTVTYSelectQueryByDepotId, OperationsTurnOverData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), OperationsTurnOverData._V_EQUIPMENT_ACTIVITY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
