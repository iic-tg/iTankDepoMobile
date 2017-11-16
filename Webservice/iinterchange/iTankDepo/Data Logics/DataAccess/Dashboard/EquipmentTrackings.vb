
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class EquipmentTrackings

    Private Const V_EQUIPMENT_TRACKINGSelectQueryBy As String = "SELECT TRCKNG_ID,RFRNC_NO,ACTVTY_NAM,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EIR_DT,DPT_ID,DPT_CD,LSS_ID,LSS_CD,CSTMR_ID,CSTMR_CD,AUTH_NO,RPR_ESTMT_TRNSXN FROM V_EQUIPMENT_TRACKING WHERE DPT_ID=@DPT_ID"
    Dim objData As DataObjects
    Private ds As New EquipmentTrackingDataSet
#Region "GetVEquipmentTrackingBy() TABLE NAME:V_EQUIPMENT_TRACKING"
    Public Function GetVEquipmentTrackingBy(ByVal bv_i32DepotID As Int32) As EquipmentTrackingDataSet
        Try
            objData = New DataObjects(V_EQUIPMENT_TRACKINGSelectQueryBy, EquipmentTrackingData.DPT_ID, CStr(bv_i32DepotID))
            objData.Fill(CType(ds, DataSet), EquipmentTrackingData._V_EQUIPMENT_TRACKING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class
