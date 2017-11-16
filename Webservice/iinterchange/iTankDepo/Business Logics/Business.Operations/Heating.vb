Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class Heating
#Region "pub_GetHeatingFromGateIn() "
    Public Function pub_GetHeatingFromGateIn(ByVal bv_DeoptID As Integer) As HeatingDataSet
        Try
            Dim dsHeating As HeatingDataSet
            Dim objHeatings As New Heatings
            dsHeating = objHeatings.GetGateInDetails(bv_DeoptID)
            Return dsHeating
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerDetail() "
    Public Function pub_GetCustomerDetail(ByVal bv_DeoptID As Integer) As HeatingDataSet
        Try
            Dim dsHeating As HeatingDataSet
            Dim objHeatings As New Heatings
            dsHeating = objHeatings.pub_GetCustomerDetail(bv_DeoptID)
            Return dsHeating
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_VHeatingChargeByDepot() TABLE NAME:V_HEATING_CHARGE"

    <OperationContract()> _
    Public Function pub_VHeatingChargeByDepot(ByVal bv_DeoptID As Integer) As HeatingDataSet

        Try
            Dim dsVHeatingChargeData As HeatingDataSet
            Dim objVHeatingCharges As New Heatings
            dsVHeatingChargeData = objVHeatingCharges.GetVHeatingChargeBy(bv_DeoptID)
            Return dsVHeatingChargeData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_UpdateHeating()"

    <OperationContract()> _
    Public Function pub_UpdateHeating(ByRef br_dsHeating As HeatingDataSet, _
                                        ByVal bv_strModifiedBy As String, _
                                        ByVal bv_datModifiedDate As DateTime, _
                                        ByVal bv_intDepotID As Integer, _
                                        ByRef br_strActivitySubmit As String, _
                                        ByVal bv_intActivityId As Integer) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objHeating As New Heatings
            Dim dtHeating As DataTable
            Dim stCleaningReference As String = ""
            Dim strRemarks As String = ""
            Dim objCommonUIs As New CommonUIs
            Dim strEquipmentInfoRemarks As String = String.Empty
            Dim lngCreated As Long
            dtHeating = br_dsHeating.Tables(HeatingData._V_HEATING_CHARGE)

            If Not dtHeating Is Nothing Then
                For Each drHeating As DataRow In dtHeating.Select(GateinData.CHECKED & "='True'")
                    Dim StartDate As Date
                    Dim StartTime As String = String.Empty
                    Dim EndDate As Date
                    Dim EndTime As String = String.Empty
                    Dim strYardLocation As String = String.Empty

                    If drHeating.RowState = DataRowState.Added Or drHeating.RowState = DataRowState.Modified Then
                        If Not (drHeating.Item(HeatingData.YRD_LCTN) Is DBNull.Value) Then
                            strYardLocation = CStr(drHeating.Item(HeatingData.YRD_LCTN))
                        Else
                            strYardLocation = Nothing
                        End If
                        If Not (drHeating.Item(HeatingData.HTNG_STRT_DT) Is DBNull.Value) Then
                            StartDate = CDate(drHeating.Item(HeatingData.HTNG_STRT_DT))
                        Else
                            StartDate = Nothing
                        End If
                        If Not ((drHeating.Item(HeatingData.HTNG_STRT_TM)) Is DBNull.Value) Then
                            StartTime = (drHeating.Item(HeatingData.HTNG_STRT_TM)).ToString
                        Else
                            StartTime = String.Empty
                        End If
                        If ((drHeating.Item(HeatingData.HTNG_END_DT)) Is DBNull.Value) Then
                            EndDate = Nothing
                        Else
                            EndDate = CDate(drHeating.Item(HeatingData.HTNG_END_DT))
                        End If
                        If ((drHeating.Item(HeatingData.HTNG_END_TM)) Is DBNull.Value) Then
                            EndTime = String.Empty
                        Else
                            EndTime = CStr(drHeating.Item(HeatingData.HTNG_END_TM))
                        End If
                    End If
                    Select Case drHeating.RowState
                        Case DataRowState.Added
                            Dim HeatingCount As Integer
                            HeatingCount = CInt(objHeating.getHeatingCount((drHeating.Item(HeatingData.EQPMNT_NO).ToString), _
                               (drHeating.Item(HeatingData.GI_TRNSCTN_NO)).ToString, _
                            bv_intDepotID, _
                            objTrans))
                            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(drHeating.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                           bv_intDepotID, _
                                                                                           objTrans)
                            If HeatingCount = 0 Then
                                lngCreated = objHeating.CreateHeatingCharge((drHeating.Item(HeatingData.EQPMNT_NO).ToString), _
                                                                              CLng(drHeating.Item(HeatingData.EQPMNT_TYP_ID)), _
                                                                              CLng(drHeating.Item(HeatingData.CSTMR_ID)), _
                                                                              StartDate, _
                                                                              StartTime, _
                                                                              EndDate, _
                                                                              EndTime, _
                                                                              CStr(drHeating.Item(HeatingData.HTNG_TMPRTR)), _
                                                                              CDbl(drHeating.Item(HeatingData.TTL_HTN_PRD)), _
                                                                              CDbl(drHeating.Item(HeatingData.MIN_HTNG_RT_NC)), _
                                                                              CDbl(drHeating.Item(HeatingData.HRLY_CHRG_NC)), _
                                                                              CDbl(drHeating.Item(HeatingData.TTL_RT_NC)), _
                                                                              "U", _
                                                                              (drHeating.Item(HeatingData.GI_TRNSCTN_NO)).ToString, _
                                                                              bv_intDepotID, _
                                                                              bv_strModifiedBy, _
                                                                              bv_datModifiedDate, _
                                                                              bv_strModifiedBy, _
                                                                              bv_datModifiedDate, _
                                                                              objTrans)



                                objCommonUIs.CreateTracking(lngCreated, _
                                                            CLng(drHeating.Item(HeatingData.CSTMR_ID)), _
                                                            drHeating.Item(HeatingData.EQPMNT_NO).ToString, _
                                                            "Heating", _
                                                            0, _
                                                            CStr(lngCreated), _
                                                            EndDate, _
                                                            Nothing, _
                                                            strYardLocation, _
                                                            drHeating.Item(HeatingData.GI_TRNSCTN_NO).ToString, _
                                                            Nothing, _
                                                            Nothing, _
                                                            bv_strModifiedBy, _
                                                            bv_datModifiedDate, _
                                                            bv_strModifiedBy, _
                                                            bv_datModifiedDate, _
                                                            Nothing, _
                                                            Nothing, _
                                                            Nothing, _
                                                            bv_intDepotID, _
                                                            0, _
                                                            Nothing, _
                                                            strEquipmentInfoRemarks, _
                                                            False, _
                                                            objTrans)

                            Else

                                objHeating.UpdateHeatingCharge((drHeating.Item(HeatingData.EQPMNT_NO).ToString), _
                                                                              StartDate, _
                                                                              StartTime, _
                                                                              EndDate, _
                                                                              EndTime, _
                                                                              CStr(drHeating.Item(HeatingData.HTNG_TMPRTR)), _
                                                                              CDbl(drHeating.Item(HeatingData.TTL_HTN_PRD)), _
                                                                              CDbl(drHeating.Item(HeatingData.MIN_HTNG_RT_NC)), _
                                                                              CDbl(drHeating.Item(HeatingData.HRLY_CHRG_NC)), _
                                                                              CDbl(drHeating.Item(HeatingData.TTL_RT_NC)), _
                                                                               "U", _
                                                                              (drHeating.Item(HeatingData.GI_TRNSCTN_NO)).ToString, _
                                                                              bv_intDepotID, _
                                                                              bv_strModifiedBy, _
                                                                              bv_datModifiedDate, _
                                                                              objTrans)

                                objHeating.UpdateHandlingCharge((drHeating.Item(HeatingData.EQPMNT_NO).ToString), _
                                               "0", _
                                              (drHeating.Item(HeatingData.GI_TRNSCTN_NO)).ToString, _
                                              bv_intDepotID, _
                                              objTrans)

                                objCommonUIs.UpdateTracking_Date_Remarks_And_YardLocation(drHeating.Item(HeatingData.EQPMNT_NO).ToString, _
                                                  EndDate, _
                                                  "Heating", _
                                                  drHeating.Item(HeatingData.GI_TRNSCTN_NO).ToString, _
                                                  Nothing, _
                                                  Nothing, _
                                                  strYardLocation, _
                                                  bv_intDepotID, _
                                                  bv_strModifiedBy, _
                                                  bv_datModifiedDate, _
                                                  strEquipmentInfoRemarks, _
                                                  objTrans)

                            End If
                        Case DataRowState.Modified
                            objHeating.CreateHeatingCharge((drHeating.Item(HeatingData.EQPMNT_NO).ToString), _
                                                                           CLng(drHeating.Item(HeatingData.EQPMNT_TYP_ID)), _
                                                                           CLng(drHeating.Item(HeatingData.CSTMR_ID)), _
                                                                           CDate(drHeating.Item(HeatingData.HTNG_STRT_DT)), _
                                                                           CStr(drHeating.Item(HeatingData.HTNG_STRT_TM)), _
                                                                           CDate(drHeating.Item(HeatingData.HTNG_END_TM)), _
                                                                           CStr(drHeating.Item(HeatingData.HTNG_END_DT)), _
                                                                           CStr(drHeating.Item(HeatingData.HTNG_TMPRTR)), _
                                                                           CDbl(drHeating.Item(HeatingData.TTL_HTN_PRD)), _
                                                                           CDbl(drHeating.Item(HeatingData.MIN_HTNG_RT_NC)), _
                                                                           CDbl(drHeating.Item(HeatingData.HRLY_CHRG_NC)), _
                                                                           CDbl(drHeating.Item(HeatingData.TTL_RT_NC)), _
                                                                            "U", _
                                                                           (drHeating.Item(HeatingData.GI_TRNSCTN_NO)).ToString, _
                                                                           bv_intDepotID, _
                                                                           bv_strModifiedBy, _
                                                                           bv_datModifiedDate, _
                                                                           bv_strModifiedBy, _
                                                                           bv_datModifiedDate, _
                                                                           objTrans)

                            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(drHeating.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                           bv_intDepotID, _
                                                                                           objTrans)
                            objCommonUIs.UpdateTrackingEIRemarks(drHeating.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                 (drHeating.Item(HeatingData.GI_TRNSCTN_NO)).ToString, _
                                                                 "Heating", _
                                                                 strEquipmentInfoRemarks, _
                                                                 bv_intDepotID, _
                                                                 bv_strModifiedBy, _
                                                                 bv_datModifiedDate, _
                                                                 objTrans)


                    End Select
                Next

            End If
            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function

#End Region

End Class
