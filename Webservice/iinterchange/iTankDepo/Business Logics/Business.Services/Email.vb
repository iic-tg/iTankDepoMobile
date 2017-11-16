Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class Email

#Region "pub_GetBulk_Email() TABLE NAME:BULK_EMAIL"
    <OperationContract()> _
    Public Function pub_GetBulk_Email() As BulkEmailDataSet
        Try
            Dim objBulkEmail As BulkEmailDataSet
            Dim objBulkEmails As New Emails
            objBulkEmail = objBulkEmails.GetBulk_Email()
            Return objBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetBulkEmailDetailbyblk_eml_bin() TABLE NAME:BULK_EMAIL_DETAIL"
    <OperationContract()> _
    Public Function pub_GetBulkEmailDetailbyblk_eml_bin(ByVal bv_Blk_Eml_Bin As Int64) As BulkEmailDataSet

        Try
            Dim objBulkEmail As BulkEmailDataSet
            Dim objBulkEmails As New Emails
            objBulkEmail = objBulkEmails.GetBulkEmailDetailbyblk_eml_bin(bv_Blk_Eml_Bin)
            Return objBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRepair_EstimateBy_Transmission_No() TABLE NAME:V_REPAIR_ESTIMATE"
    <OperationContract()> _
    Public Function pub_GetRepair_EstimateBy_Transmission_No(ByVal bv_strWM_Transmission_No As String, ByVal bv_strWM_Unit_Nbr As String) As BulkEmailDataSet

        Try
            Dim objBulkEmail As BulkEmailDataSet
            Dim objBulkEmails As New Emails
            objBulkEmail = objBulkEmails.GetRepair_EstimateBy_Transmission_No(bv_strWM_Transmission_No, bv_strWM_Unit_Nbr)
            Return objBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetV_Repair_Estimate_DetailByRpr_Estmt_Id() TABLE NAME:V_REPAIR_ESTIMATE_DETAIL"
    <OperationContract()> _
    Public Function pub_GetV_Repair_Estimate_DetailByRpr_Estmt_Id(ByVal bv_i64RPR_ESTMT_ID As Int64) As BulkEmailDataSet

        Try
            Dim objBulkEmail As BulkEmailDataSet
            Dim objBulkEmails As New Emails
            objBulkEmail = objBulkEmails.GetV_Repair_Estimate_DetailByRpr_Estmt_Id(bv_i64RPR_ESTMT_ID)
            Return objBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "UpdateBulk_Email() TABLE NAME:Bulk_Email"
    <OperationContract()> _
    Public Function UpdateBulk_Email(ByVal bv_i64Blk_Eml_Bin As Int64, ByVal bv_datSntdt As DateTime) As Boolean
        Try
            Dim objBulkEmails As New Emails
            Dim blnUpdated As New Boolean
            blnUpdated = objBulkEmails.UpdateBulk_Email(bv_i64Blk_Eml_Bin, bv_datSntdt)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetReportDetails() TABLE NAME:BULK_EMAIL"
    <OperationContract()> _
    Public Function GetReportDetails(ByRef br_Estimate As DataRow) As BulkEmailDataSet
        Try
            If (br_Estimate.Item(EmailData.EQPMNT_STTS_CD).ToString() = "J") Or (br_Estimate.Item(EmailData.EQPMNT_STTS_CD).ToString() = "K") Then
                br_Estimate.Item(EmailData.LSS_APPRVL_AMNT_NC) = CDec(br_Estimate.Item(EmailData.OWNR_APPRVL_AMNT_NC))
                If Not br_Estimate.Item(EmailData.OWNR_APPRVL_DT).ToString() = "" Then
                    br_Estimate.Item(EmailData.LSS_APPRVL_DT) = CDate(br_Estimate.Item(EmailData.OWNR_APPRVL_DT))
                End If
            End If
            Dim dsBulkEmail As BulkEmailDataSet
            Dim objBulkEmails As New Emails
            dsBulkEmail = objBulkEmails.GetV_Repair_Estimate_DetailByRpr_Estmt_Id(CLng(br_Estimate.Item(EmailData.RPR_ESTMT_ID)))
            Dim decMaterialCost As Decimal
            Dim decCleaningCost As Decimal
            Dim dtRepairEstimate As DataTable = dsBulkEmail.Tables(EmailData._SUMMARY_DETAIL)
            pvt_CalculateRepairCharges(dsBulkEmail, CommonUIs.iDec(br_Estimate.Item(EmailData.ESTMTN_TTL_NC)), decCleaningCost, CommonUIs.iDec(br_Estimate.Item(EmailData.TTL_LBR_CST_NC)), decMaterialCost, CommonUIs.iDec(br_Estimate.Item(EmailData.TTL_SRVC_TX_NC)), CommonUIs.iDec(br_Estimate.Item(EmailData.TX_RT_NC)), CommonUIs.iDec(br_Estimate.Item(EmailData.EST_TTL_TXD_NC)), True)
            Dim drSummary As DataRow = dsBulkEmail.Tables(TrackingData._REPAIR_SUMMARY_DETAIL).NewRow()
            dsBulkEmail.Tables(TrackingData._REPAIR_SUMMARY_DETAIL).Rows.Add(drSummary)
            drSummary.Item(EmailData.USR_LH) = CommonUIs.iDec(dtRepairEstimate.Rows(0).Item(EmailData.USR_SMMRY))
            drSummary.Item(EmailData.USR_LHC) = CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(EmailData.USR_SMMRY))
            drSummary.Item(EmailData.USR_MC) = CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(EmailData.USR_SMMRY))
            drSummary.Item(EmailData.USR_TX) = CommonUIs.iDec(dtRepairEstimate.Rows(3).Item(EmailData.USR_SMMRY))
            drSummary.Item(EmailData.USR_TTL) = CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(EmailData.USR_SMMRY))
            drSummary.Item(EmailData.INSRR_LH) = CommonUIs.iDec(dtRepairEstimate.Rows(0).Item(EmailData.INSR_SMMRY))
            drSummary.Item(EmailData.INSRR_LHC) = CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(EmailData.INSR_SMMRY))
            drSummary.Item(EmailData.INSRR_MC) = CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(EmailData.INSR_SMMRY))
            drSummary.Item(EmailData.INSRR_TX) = CommonUIs.iDec(dtRepairEstimate.Rows(3).Item(EmailData.INSR_SMMRY))
            drSummary.Item(EmailData.INSRR_TTL) = CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(EmailData.INSR_SMMRY))
            drSummary.Item(EmailData.OWNR_LH) = CommonUIs.iDec(dtRepairEstimate.Rows(0).Item(EmailData.OWNR_SMMRY))
            drSummary.Item(EmailData.OWNR_LHC) = CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(EmailData.OWNR_SMMRY))
            drSummary.Item(EmailData.OWNR_MC) = CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(EmailData.OWNR_SMMRY))
            drSummary.Item(EmailData.OWNR_TX) = CommonUIs.iDec(dtRepairEstimate.Rows(3).Item(EmailData.OWNR_SMMRY))
            drSummary.Item(EmailData.OWNR_TTL) = CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(EmailData.OWNR_SMMRY))
            drSummary.Item(EmailData.DPT_LH) = CommonUIs.iDec(dtRepairEstimate.Rows(0).Item(EmailData.DPT_SMMRY))
            drSummary.Item(EmailData.DPT_LHC) = CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(EmailData.DPT_SMMRY))
            drSummary.Item(EmailData.DPT_MC) = CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(EmailData.DPT_SMMRY))
            drSummary.Item(EmailData.DPT_TX) = CommonUIs.iDec(dtRepairEstimate.Rows(3).Item(EmailData.DPT_SMMRY))
            drSummary.Item(EmailData.DPT_TTL) = CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(EmailData.DPT_SMMRY))
            drSummary.Item(EmailData.OTHR_LH) = CommonUIs.iDec(dtRepairEstimate.Rows(0).Item(EmailData.OTHRS_SMMRY))
            drSummary.Item(EmailData.OTHR_LHC) = CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(EmailData.OTHRS_SMMRY))
            drSummary.Item(EmailData.OTHR_MC) = CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(EmailData.OTHRS_SMMRY))
            drSummary.Item(EmailData.OTHR_TX) = CommonUIs.iDec(dtRepairEstimate.Rows(3).Item(EmailData.OTHRS_SMMRY))
            drSummary.Item(EmailData.OTHR_TTL) = CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(EmailData.OTHRS_SMMRY))
            drSummary.Item(EmailData.DLTN_LH) = CommonUIs.iDec(dtRepairEstimate.Rows(0).Item(EmailData.DLTNS_SMMRY))
            drSummary.Item(EmailData.DLTN_LHC) = CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(EmailData.DLTNS_SMMRY))
            drSummary.Item(EmailData.DLTN_MC) = CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(EmailData.DLTNS_SMMRY))
            drSummary.Item(EmailData.DLTN_TX) = CommonUIs.iDec(dtRepairEstimate.Rows(3).Item(EmailData.DLTNS_SMMRY))
            drSummary.Item(EmailData.DLTN_TTL) = CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(EmailData.DLTNS_SMMRY))
            drSummary.Item(EmailData.TTL_EST_CST) = CDec(br_Estimate.Item(EmailData.ESTMTN_TTL_NC))
            drSummary.Item(EmailData.TTL_CLN_CST) = decCleaningCost
            drSummary.Item(EmailData.TTL_LBR_CST) = CommonUIs.iDec(br_Estimate.Item(EmailData.TTL_LBR_CST_NC))
            drSummary.Item(EmailData.TTL_SRVC_CST) = CDec(br_Estimate.Item(EmailData.TTL_SRVC_TX_NC))
            drSummary.Item(EmailData.TTL_EST_SRVC_CST) = CDec(br_Estimate.Item(EmailData.EST_TTL_TXD_NC))
            For Each drRepairDetail As DataRow In dsBulkEmail.Tables(EmailData._V_REPAIR_ESTIMATE_DETAIL).Rows
                drRepairDetail.Item(EmailData.DMG_DSCRPTN_VC) = String.Concat(CStr(drRepairDetail.Item(EmailData.RPR_DSCRPTN_VC)), ",", CStr(drRepairDetail.Item(EmailData.DMG_DSCRPTN_VC)), ",", CStr(drRepairDetail.Item(EmailData.CMPNNT_DSCRPTN_VC)))
            Next
            Return dsBulkEmail
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_CalculateRepairCharges"
    Private Sub pvt_CalculateRepairCharges(ByVal bv_dsRepairEstimate As BulkEmailDataSet, _
                                           ByRef br_dblTotalEstimateAmount As Decimal, _
                                           ByRef br_dblTotalCleaningcost As Decimal, _
                                           ByRef br_dblTotallabourCost As Decimal, _
                                           ByRef br_dblTotalMaterialCost As Decimal, _
                                           ByRef br_dblTotalServicetax As Decimal, _
                                           ByRef br_dblServicetax As Decimal, _
                                           ByRef br_dblTotalTotalEstimateWithtax As Decimal, ByVal bv_blnReCalulateSummary As Boolean)
        Try
            If bv_blnReCalulateSummary Then
                For i = 0 To 4
                    Dim drRepairEst As DataRow = bv_dsRepairEstimate.Tables(EmailData._SUMMARY_DETAIL).NewRow()
                    drRepairEst.Item(EmailData.SMMRY_ID) = i + 1
                    drRepairEst.Item(EmailData.USR_SMMRY) = 0.0
                    drRepairEst.Item(EmailData.INSR_SMMRY) = 0.0
                    drRepairEst.Item(EmailData.OWNR_SMMRY) = 0.0
                    drRepairEst.Item(EmailData.DPT_SMMRY) = 0.0
                    drRepairEst.Item(EmailData.OTHRS_SMMRY) = 0.0
                    drRepairEst.Item(EmailData.DLTNS_SMMRY) = 0.0
                    bv_dsRepairEstimate.Tables(EmailData._SUMMARY_DETAIL).Rows.Add(drRepairEst)
                Next
                For Each drRepair As DataRow In bv_dsRepairEstimate.Tables(EmailData._V_REPAIR_ESTIMATE_DETAIL).Rows
                    pvt_CalculateSummaryDetail(drRepair.Item(EmailData.LBR_HRS).ToString, drRepair.Item(EmailData.LBR_HR_CST_NC).ToString, _
                                               drRepair.Item(EmailData.MTRL_CST_NC).ToString, br_dblServicetax.ToString, drRepair.Item(EmailData.TX_RL_CD).ToString, _
                                               drRepair.Item(EmailData.RSPNSBLTY_CD).ToString, bv_dsRepairEstimate.Tables(EmailData._SUMMARY_DETAIL))
                Next
            End If

            Dim dtRepairEstimate As DataTable
            dtRepairEstimate = bv_dsRepairEstimate.Tables(EmailData._SUMMARY_DETAIL)
            br_dblTotalEstimateAmount = CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(EmailData.USR_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(EmailData.INSR_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(EmailData.OWNR_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(EmailData.DPT_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(4).Item(EmailData.OTHRS_SMMRY))
            br_dblTotalCleaningcost = CommonUIs.iDec(bv_dsRepairEstimate.Tables(EmailData._V_REPAIR_ESTIMATE_DETAIL).Compute("SUM([" & EmailData.LBR_HR_CST_NC & "])", EmailData.RPR_CD & "='CC'")) + _
                CommonUIs.iDec(bv_dsRepairEstimate.Tables(EmailData._V_REPAIR_ESTIMATE_DETAIL).Compute("SUM([" & EmailData.MTRL_CST_NC & "])", EmailData.RPR_CD & "='CC'"))
            br_dblTotallabourCost = CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(EmailData.USR_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(EmailData.INSR_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(EmailData.OWNR_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(EmailData.DPT_SMMRY)) + _
                                     CommonUIs.iDec(dtRepairEstimate.Rows(1).Item(EmailData.OTHRS_SMMRY))


            br_dblTotalMaterialCost = CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(EmailData.USR_SMMRY)) + _
                                CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(EmailData.INSR_SMMRY)) + _
                                CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(EmailData.OWNR_SMMRY)) + _
                                CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(EmailData.DPT_SMMRY)) + _
                                CommonUIs.iDec(dtRepairEstimate.Rows(2).Item(EmailData.OTHRS_SMMRY))

            If br_dblTotalEstimateAmount > 0 Then
                br_dblTotalServicetax = br_dblTotalEstimateAmount * br_dblServicetax / 100
                br_dblTotalTotalEstimateWithtax = br_dblTotalEstimateAmount + br_dblTotalServicetax
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Sub

#End Region

#Region "pvt_CalculateSummaryDetail"
    Private Sub pvt_CalculateSummaryDetail(ByVal bv_strLabourHour As String, ByVal bv_strLabourHourCost As String, _
                                           ByVal bv_strMaterialCost As String, ByVal bv_strTax As String, ByVal bv_strTaxType As String, ByVal bv_strResponsibilityType As String, ByRef br_dtSummaryDetail As DataTable)
        Try
            Select Case bv_strResponsibilityType
                Case "O"
                    bv_strResponsibilityType = EmailData.OWNR_SMMRY
                Case "D"
                    bv_strResponsibilityType = EmailData.DPT_SMMRY
                Case "I"
                    bv_strResponsibilityType = EmailData.INSR_SMMRY
                Case "U"
                    bv_strResponsibilityType = EmailData.USR_SMMRY
                Case "X"
                    bv_strResponsibilityType = EmailData.DLTNS_SMMRY
                Case Else
                    bv_strResponsibilityType = EmailData.OTHRS_SMMRY
            End Select
            Dim dblLabourCost As Decimal
            Dim dblMaterialCost As Decimal
            If Not bv_strLabourHour = "" Then
                br_dtSummaryDetail.Rows(0).Item(bv_strResponsibilityType) = CommonUIs.iDec(br_dtSummaryDetail.Rows(0).Item(bv_strResponsibilityType)) + CommonUIs.iDec(bv_strLabourHour)
            End If
            If Not bv_strLabourHourCost = "" Then
                dblLabourCost = CommonUIs.iDec(bv_strLabourHourCost)
            End If
            If Not bv_strMaterialCost = "" Then
                dblMaterialCost = CommonUIs.iDec(bv_strMaterialCost)
            End If
            If CommonUIs.iDec(bv_strTax) > 0 Then
                Dim dblTaxRate As Decimal = CommonUIs.iDec(bv_strTax)
                Dim dblTotalTax As Decimal
                Select Case bv_strTaxType
                    Case "B"
                        If dblLabourCost > 0 Then
                            dblTotalTax = (dblLabourCost + dblMaterialCost) * (dblTaxRate / 100)
                        Else
                            dblTotalTax = CommonUIs.iDec(0)
                        End If
                    Case "L"
                        If dblLabourCost > 0 Then
                            dblTotalTax = dblLabourCost * (dblTaxRate / 100)
                        Else
                            dblTotalTax = CommonUIs.iDec(0)
                        End If
                    Case "M"
                        If dblMaterialCost > 0 Then
                            dblTotalTax = dblMaterialCost * (dblTaxRate / 100)
                        Else
                            dblTotalTax = CommonUIs.iDec(0)
                        End If
                    Case "N"
                        br_dtSummaryDetail.Rows(3).Item(bv_strResponsibilityType) = CommonUIs.iDec(br_dtSummaryDetail.Rows(3).Item(bv_strResponsibilityType)) + CommonUIs.iDec(0)
                End Select
                br_dtSummaryDetail.Rows(3).Item(bv_strResponsibilityType) = CommonUIs.iDec(br_dtSummaryDetail.Rows(3).Item(bv_strResponsibilityType)) + dblTotalTax
            Else
                br_dtSummaryDetail.Rows(3).Item(bv_strResponsibilityType) = CommonUIs.iDec(0)
            End If
            br_dtSummaryDetail.Rows(1).Item(bv_strResponsibilityType) = CommonUIs.iDec(br_dtSummaryDetail.Rows(1).Item(bv_strResponsibilityType)) + dblLabourCost
            br_dtSummaryDetail.Rows(2).Item(bv_strResponsibilityType) = CommonUIs.iDec(br_dtSummaryDetail.Rows(2).Item(bv_strResponsibilityType)) + dblMaterialCost
            br_dtSummaryDetail.Rows(4).Item(bv_strResponsibilityType) = CommonUIs.iDec(br_dtSummaryDetail.Compute("SUM([" & bv_strResponsibilityType & "])", EmailData.SMMRY_ID & ">1 AND " & EmailData.SMMRY_ID & "< 5"))
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))

        End Try
    End Sub
#End Region

End Class
