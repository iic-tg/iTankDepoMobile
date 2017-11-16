
Partial Class Operations_RepairEstimateMassApplyInput
    Inherits Pagebase


#Region "Declarations.."
    Dim dsRepairEstimate As New RepairEstimateDataSet
    Private Const REPAIR_ESTIMATE As String = "REPAIR_ESTIMATE"
    Private Const REPAIR_ESTIMATE_MASS_APPLY As String = "REPAIR_ESTIMATE_MASS_APPLY"
    Private dsRepairEstimateMassApply As New RepairEstimateDataSet
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Operations/RepairEstimateMassApplyInput.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "massApplyInput"
                    pvt_PartyMassApplyInput(e.GetCallbackValue("PartyId"), _
                                            e.GetCallbackValue("PartyCode"), _
                                            e.GetCallbackValue("RepairEstimateDetailId"), _
                                            e.GetCallbackValue("RepairEstimateId"))

            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_PartyMassApplyInput"
    Private Sub pvt_PartyMassApplyInput(ByVal bv_strPartyId As String, _
                                        ByVal bv_strPartyCode As String, _
                                        ByVal bv_strRepairEstimateDetailId As String, _
                                        ByVal bv_strRepairEstimateId As String)
        Try
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            For Each drRepairEstimateDetail As DataRow In dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Select(String.Concat(RepairEstimateData.CHK_BT, " ='True'"))
                If drRepairEstimateDetail.RowState <> DataRowState.Deleted Then
                    drRepairEstimateDetail.Item(RepairEstimateData.RSPNSBLTY_ID) = bv_strPartyId
                    drRepairEstimateDetail.Item(RepairEstimateData.RSPNSBLTY_CD) = bv_strPartyCode
                End If
            Next
            CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class
