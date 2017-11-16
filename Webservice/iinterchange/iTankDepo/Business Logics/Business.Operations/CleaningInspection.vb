Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class CleaningInspection


#Region "UpdateCleaningInspectionNo"
    <OperationContract()> _
    Public Function UpdateCleaningInstructionNo(ByRef strCleaningInspectionNo As String, _
                                                ByVal bv_strModifiedBy As String, _
                                                ByVal bv_datModifiedDate As DateTime, _
                                                ByVal bv_i32DepotId As Int32, _
                                                ByRef br_dsCleaningInstruction As CleaningInspectionDataSet) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objInspections As New CleaningInspections
            Dim objCommonuis As New CommonUIs
            Dim strRemarks As String = String.Empty
            Dim strYardcoation As String = String.Empty

            'From Index pattern
            Dim strCleaningInspNo As String = objInspections.UpdateCleaningInstructionNo(strCleaningInspectionNo, _
                                                                                      objTrans)
            'From Index pattern
            Dim objIndexPattern As New IndexPatterns
            strCleaningInspectionNo = objIndexPattern.GetMaxReferenceNo(String.Concat(CleaningData._CLEANING_INSPECTION, ",", "Cleaning Instruction"), Now, objTrans, Nothing, bv_i32DepotId)
            'CR- 003 [CLEANING INSTRUCTION RAISED THEN IND_TO_ACN]
            For Each drCleaning As DataRow In br_dsCleaningInstruction.Tables(CleaningInspectionData._V_CLEANING_INSTRUCTION).Select(String.Concat(CleaningInspectionData.CHECKED, " = 'True'"))
                If Not IsDBNull(drCleaning.Item(CleaningInspectionData.RMRKS_VC)) Then
                    strRemarks = CStr(drCleaning.Item(CleaningInspectionData.RMRKS_VC))
                End If
                If Not IsDBNull(drCleaning.Item(CleaningInspectionData.YRD_LCTN)) Then
                    strYardcoation = CStr(drCleaning.Item(CleaningInspectionData.YRD_LCTN))
                End If
                '3 - ACN
                'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                Dim strEquipmentInfoRemarks As String = String.Empty
                strEquipmentInfoRemarks = objCommonuis.GetEquipmentInformation(drCleaning.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                               bv_i32DepotId, _
                                                                               objTrans)
                objCommonuis.CreateTracking(Nothing, _
                                           CommonUIs.iLng(drCleaning.Item(CleaningInspectionData.CSTMR_ID)), _
                                           drCleaning.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                           "Change of Status", _
                                            3, _
                                           drCleaning.Item(CleaningInspectionData.ACTVTY_STTS_ID).ToString, _
                                           CDate(Now.ToString("dd-MMM-yyyy")), _
                                           strRemarks, _
                                           strYardcoation, _
                                           drCleaning.Item(CleaningInspectionData.GI_TRNSCTN_NO).ToString, _
                                           Nothing, _
                                           Nothing, _
                                           bv_strModifiedBy, _
                                           bv_datModifiedDate, _
                                           bv_strModifiedBy, _
                                           bv_datModifiedDate, _
                                           Nothing, _
                                           Nothing, _
                                           Nothing, _
                                           bv_i32DepotId, _
                                           0, _
                                           Nothing, _
                                           strEquipmentInfoRemarks, _
                                           False, _
                                           objTrans)

                objCommonuis.UpdateActivityStatus_WithRefNO(CLng(drCleaning.Item(CleaningInspectionData.ACTVTY_STTS_ID)), _
                                                  3, _
                                                  CDate(Now.ToString("dd-MMM-yyyy")), _
                                                  strRemarks, _
                                                  bv_i32DepotId, _
                                                  strYardcoation, _
                                                  strCleaningInspectionNo, _
                                                  objTrans)
            Next

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


#Region "pub_GetCleaningInstruction()"
    <OperationContract()> _
    Public Function pub_GetCleaningInstruction(ByVal bv_DeoptID As Integer) As CleaningInspectionDataSet
        Try
            Dim dsCleaningInspectionDataSet As CleaningInspectionDataSet
            Dim objCleaningInspections As New CleaningInspections
            dsCleaningInspectionDataSet = objCleaningInspections.GetVCleaningInstruction(bv_DeoptID)
            Return dsCleaningInspectionDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class