
Partial Class Operations_RepairEstimateSummary
    Inherits Pagebase

    Dim dsRepairEstimate As New RepairEstimateDataSet
    Dim dtRepairEstimate As DataTable
    Private Const REPAIR_ESTIMATE As String = "REPAIR_ESTIMATE"


#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim dtSummary As New DataTable
            dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Clear()
            dtSummary = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Clone()
            If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count = 0 Then
                SummaryInitCall(dtSummary)
                ifgSummaryDetail.DataSource = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL)
                ifgSummaryDetail.DataBind()
            Else
                pvt_CalculateSummaryDetail(dsRepairEstimate)
                ifgSummaryDetail.DataSource = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL)
                ifgSummaryDetail.DataBind()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CalculateSummaryDetail"
    Private Sub pvt_CalculateSummaryDetail(ByRef br_dsRepairEstimate As RepairEstimateDataSet)
        Try
            Dim decManHour As Decimal = 0
            Dim decManHourRate As Decimal = 0
            Dim decManHourTotal As Decimal = 0
            Dim decMaterialCost As Decimal = 0
            Dim deciManHour As Decimal = 0
            Dim deciManHourRate As Decimal = 0
            Dim deciManHourTotal As Decimal = 0
            Dim deciMaterialCost As Decimal = 0
            If dsRepairEstimate.Tables(RepairCompletionData._SUMMARY_DETAIL).Rows.Count = 0 Then
                For i = 0 To 1
                    Dim drRepairEstimate As DataRow = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).NewRow()
                    drRepairEstimate.Item(RepairEstimateData.SMMRY_ID) = i + 1
                    drRepairEstimate.Item(RepairEstimateData.MN_HR_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.LBR_RT_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.MN_HR_RT_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.MTRL_CST_SMMRY) = 0.0
                    drRepairEstimate.Item(RepairEstimateData.TTL_CST_SMMRY) = 0.0
                    dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows.Add(drRepairEstimate)
                Next
            End If
            Dim dtSummaryDetail As DataTable = dsRepairEstimate.Tables(RepairCompletionData._V_REPAIR_ESTIMATE_DETAIL).Copy()
            dtSummaryDetail.AcceptChanges()
            For Each drSummary As DataRow In dtSummaryDetail.Rows

                Select Case drSummary.Item(RepairEstimateData.RSPNSBLTY_CD).ToString
                    Case "C"
                        decManHour = decManHour + CommonWeb.iDec(drSummary.Item(RepairEstimateData.LBR_HRS))
                        decManHourRate = decManHourRate + CommonWeb.iDec(drSummary.Item(RepairEstimateData.LBR_RT))
                        decManHourTotal = decManHourTotal + CommonWeb.iDec(drSummary.Item(RepairEstimateData.LBR_HR_CST_NC))
                        decMaterialCost = decMaterialCost + CommonWeb.iDec(drSummary.Item(RepairEstimateData.MTRL_CST_NC))
                        dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(0).Item(RepairEstimateData.MN_HR_SMMRY) = decManHour
                        dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(0).Item(RepairEstimateData.LBR_RT_SMMRY) = decManHourRate
                        dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(0).Item(RepairEstimateData.MN_HR_RT_SMMRY) = decManHourTotal
                        dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(0).Item(RepairEstimateData.MTRL_CST_SMMRY) = decMaterialCost
                        dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(0).Item(RepairEstimateData.TTL_CST_SMMRY) = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'C'")))
                    Case "I"
                        deciManHour = deciManHour + CommonWeb.iDec(drSummary.Item(RepairEstimateData.LBR_HRS))
                        deciManHourRate = deciManHourRate + CommonWeb.iDec(drSummary.Item(RepairEstimateData.LBR_RT))
                        deciManHourTotal = deciManHourTotal + CommonWeb.iDec(drSummary.Item(RepairEstimateData.LBR_HR_CST_NC))
                        deciMaterialCost = deciMaterialCost + CommonWeb.iDec(drSummary.Item(RepairEstimateData.MTRL_CST_NC))
                        dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(1).Item(RepairEstimateData.MN_HR_SMMRY) = deciManHour
                        dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(1).Item(RepairEstimateData.LBR_RT_SMMRY) = deciManHourRate
                        dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(1).Item(RepairEstimateData.MN_HR_RT_SMMRY) = deciManHourTotal
                        dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(1).Item(RepairEstimateData.MTRL_CST_SMMRY) = deciMaterialCost
                        dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows(1).Item(RepairEstimateData.TTL_CST_SMMRY) = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", String.Concat(RepairEstimateData.RSPNSBLTY_CD, "= 'I'")))
                End Select

                Dim dblApprovalAmount As Double = 0
                If dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Count > 0 Then
                    dblApprovalAmount = CDbl(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Compute("sum(TTL_CST_NC)", ""))
                    CacheData("ApprovalAmount", dblApprovalAmount)
                End If
            Next
            CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "SummaryInitCall"
    Private Sub SummaryInitCall(ByRef dtSummary As DataTable)
        Try
            For i = 0 To 1
                Dim drRepairEstimate As DataRow = dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).NewRow()
                drRepairEstimate.Item(RepairEstimateData.SMMRY_ID) = i + 1
                drRepairEstimate.Item(RepairEstimateData.MN_HR_SMMRY) = 0.0
                drRepairEstimate.Item(RepairEstimateData.LBR_RT_SMMRY) = 0.0
                drRepairEstimate.Item(RepairEstimateData.MN_HR_RT_SMMRY) = 0.0
                drRepairEstimate.Item(RepairEstimateData.MTRL_CST_SMMRY) = 0.0
                drRepairEstimate.Item(RepairEstimateData.TTL_CST_SMMRY) = 0.0
                dsRepairEstimate.Tables(RepairEstimateData._SUMMARY_DETAIL).Rows.Add(drRepairEstimate)
            Next
            CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region



End Class
