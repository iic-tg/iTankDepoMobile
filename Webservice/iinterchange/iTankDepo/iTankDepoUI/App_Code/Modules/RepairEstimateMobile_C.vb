Imports Microsoft.VisualBasic
Imports System.Web.Script.Serialization
Imports System.Globalization
Imports System.IO

Public Class RepairEstimateMobile_C
    Inherits Framebase


    Dim objEstimate As New RepairEstimate
    Dim objCommondata As New CommonData
    Private Const REPAIR_ESTIMATE As String = "REPAIR_ESTIMATE"
    Dim dsRepairEstimate As New RepairEstimateDataSet



    Public Function GetLineItems(ByVal bv_strEstimateId As String, ByVal Mode As String) As RepairEstimateDataSet


        If Mode = "new" Then

        Else
            dsRepairEstimate = objEstimate.pub_GetRepairEstimateDetailByRepairEstimationId(CLng(bv_strEstimateId))
        End If

       
        CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
        Return dsRepairEstimate

    End Function


    Public Function FetchLineItems(ByVal TariffId As String, ByVal TariffGroupId As String, ByVal bv_strEstimateId As String, ByVal Mode As String) As RepairEstimateDataSet


        'Dim dsRepairEstimate As New RepairEstimateDataSet

        dsRepairEstimate = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)

            Dim objCommon As New CommonData()
            Dim intDepotId As Integer
            Dim str_061KeyValue As String
            Dim bln_061KeyExist As Boolean
            str_061KeyValue = objCommondata.GetConfigSetting("061", bln_061KeyExist)
            Dim dtTariff As New DataTable
            Dim drNew As DataRow
            Dim decManHourRate As Decimal = 0D
            intDepotId = CInt(objCommon.GetDepotID())
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotId = CInt(objCommon.GetHeadQuarterID())
            End If

            If bln_061KeyExist Then


                dtTariff = objEstimate.GetTariffCodeTable(intDepotId, TariffId).Tables(RepairEstimateData._V_TARIFF_CODE_DETAIL)
                If dtTariff.Rows.Count > 0 And bln_061KeyExist Then
                    For Each dr As DataRow In dtTariff.Rows
                        drNew = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).NewRow()
                        drNew.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = CommonWeb.GetNextIndex(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), RepairEstimateData.RPR_ESTMT_DTL_ID)
                        drNew.Item(RepairEstimateData.ITM_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_LCTN_ID)
                        drNew.Item(RepairEstimateData.ITM_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_LCTN_CD)
                        drNew.Item(RepairEstimateData.SB_ITM_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_CMPNT_ID)
                        drNew.Item(RepairEstimateData.SB_ITM_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_CMPNT_CD)
                        drNew.Item(RepairEstimateData.DMG_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_DMG_ID)
                        drNew.Item(RepairEstimateData.DMG_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_DMG_CD)
                        drNew.Item(RepairEstimateData.MTRL_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_ID)
                        drNew.Item(RepairEstimateData.MTRL_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CD)
                        drNew.Item(RepairEstimateData.RPR_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_RPR_ID)
                        drNew.Item(RepairEstimateData.RPR_CD) = dr.Item(RepairEstimateData.TRFF_CD_DTL_RPR_CD)
                        If Not dr.Item(RepairEstimateData.TRFF_CD_DTL_MNHR) Is DBNull.Value Then
                            drNew.Item(RepairEstimateData.LBR_HRS) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MNHR)
                        Else
                            drNew.Item(RepairEstimateData.LBR_HRS) = "0.00"
                        End If
                        drNew.Item(RepairEstimateData.CHNG_BIT) = False
                        drNew.Item(RepairEstimateData.RSPNSBLTY_CD) = "O"
                        drNew.Item(RepairEstimateData.RSPNSBLTY_ID) = 9

                        drNew.Item(RepairEstimateData.TRFF_CD_DTL_ID) = dr.Item(RepairEstimateData.TRFF_CD_DTL_ID)

                        drNew.Item(RepairEstimateData.LBR_RT) = decManHourRate
                        drNew.Item(RepairEstimateData.LBR_HR_CST_NC) = CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HRS)) * CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_RT))
                        If Not dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CST) Is DBNull.Value Then
                            drNew.Item(RepairEstimateData.MTRL_CST_NC) = dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CST)
                        Else
                            drNew.Item(RepairEstimateData.MTRL_CST_NC) = "0.00"
                        End If

                        drNew.Item(RepairEstimateData.TTL_CST_NC) = CommonWeb.iDec(dr.Item(RepairEstimateData.TRFF_CD_DTL_MTRL_CST)) + CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HR_CST_NC))
                        drNew.Item(RepairEstimateData.DMG_RPR_DSCRPTN) = dr.Item(RepairEstimateData.TRFF_CD_DTL_RMRKS)
                        dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Add(drNew)
                    Next
                End If
            Else
                dtTariff = objEstimate.pub_GetLineDeatilbyTariffCodeId(intDepotId, CStr(TariffId), CStr(TariffGroupId)).Tables(RepairEstimateData._V_TARIFF_CODE)
                If dtTariff.Rows.Count > 0 Then
                    For Each dr As DataRow In dtTariff.Rows
                        drNew = dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).NewRow()
                        drNew.Item(RepairEstimateData.RPR_ESTMT_DTL_ID) = CommonWeb.GetNextIndex(dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL), RepairEstimateData.RPR_ESTMT_DTL_ID)
                        drNew.Item(RepairEstimateData.ITM_ID) = dr.Item(RepairEstimateData.ITM_ID)
                        drNew.Item(RepairEstimateData.ITM_CD) = dr.Item(RepairEstimateData.ITM_CD)
                        drNew.Item(RepairEstimateData.SB_ITM_ID) = dr.Item(RepairEstimateData.SB_ITM_ID)
                        drNew.Item(RepairEstimateData.SB_ITM_CD) = dr.Item(RepairEstimateData.SB_ITM_CD)
                        drNew.Item(RepairEstimateData.DMG_ID) = dr.Item(RepairEstimateData.DMG_ID)
                        drNew.Item(RepairEstimateData.DMG_CD) = dr.Item(RepairEstimateData.DMG_CD)
                        drNew.Item(RepairEstimateData.RPR_ID) = dr.Item(RepairEstimateData.RPR_ID)
                        drNew.Item(RepairEstimateData.RPR_CD) = dr.Item(RepairEstimateData.RPR_CD)
                        drNew.Item(RepairEstimateData.LBR_HRS) = dr.Item(RepairEstimateData.MN_HR)
                        drNew.Item(RepairEstimateData.LBR_RT) = decManHourRate
                        drNew.Item(RepairEstimateData.LBR_HR_CST_NC) = CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HRS)) * CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_RT))
                        drNew.Item(RepairEstimateData.MTRL_CST_NC) = dr.Item(RepairEstimateData.MTRL_CST)
                        drNew.Item(RepairEstimateData.TTL_CST_NC) = CommonWeb.iDec(dr.Item(RepairEstimateData.MTRL_CST_NC)) + CommonWeb.iDec(drNew.Item(RepairEstimateData.LBR_HR_CST_NC))
                        drNew.Item(RepairEstimateData.DMG_RPR_DSCRPTN) = dr.Item(RepairEstimateData.RMRKS_VC)
                        drNew.Item(RepairEstimateData.RSPNSBLTY_CD) = "C"
                        drNew.Item(RepairEstimateData.RSPNSBLTY_ID) = 66
                        dsRepairEstimate.Tables(RepairEstimateData._V_REPAIR_ESTIMATE_DETAIL).Rows.Add(drNew)
                    Next
                End If
            End If



            CacheData(REPAIR_ESTIMATE, dsRepairEstimate)
            Return dsRepairEstimate
    End Function


    

End Class
