Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Business.Common
<ServiceContract()> _
Public Class Cleaning

#Region "UpdateCleaningCertificateNo"
    <OperationContract()> _
    Public Function UpdateCleaningCertificateNo(ByVal bv_CleaningID As Integer, _
                                                    bv_strEquipmentNo As String, _
                                                    ByVal bv_intDepotID As Int32, _
                                                    ByVal bv_strGITransactionNo As String, _
                                                    ByRef strCleaningCertificateNo As String) As String
        Dim objTrans As New Transactions
        Try
            Dim objCleanings As New Cleanings

            Dim objIndexPattern As New IndexPatterns
            strCleaningCertificateNo = objIndexPattern.GetMaxReferenceNo(CleaningData._CLEANING, Now, objTrans, Nothing, bv_intDepotID)

            Dim strCleaningCertNo As String = objCleanings.UpdateCleaningCertificateNo(bv_CleaningID, _
                                                     bv_strEquipmentNo, _
                                                     bv_intDepotID, _
                                                     bv_strGITransactionNo, _
                                                     True, _
                                                     strCleaningCertificateNo, _
                                                     objTrans)
            objCleanings.UpdateCleaningChargeCertNo(bv_CleaningID, _
                                                    bv_strEquipmentNo, _
                                                    bv_intDepotID, _
                                                    bv_strGITransactionNo, _
                                                    strCleaningCertificateNo, _
                                                    objTrans)
            objCleanings.UpdateTrackingCertNo(bv_CleaningID, _
                                              bv_strEquipmentNo, _
                                                    bv_intDepotID, _
                                                    bv_strGITransactionNo, _
                                                    strCleaningCertificateNo, _
                                                    objTrans)
            objTrans.commit()
            Return strCleaningCertNo
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function
#End Region

#Region "pub_GetCleaningDetails"
    <OperationContract()> _
    Public Function pub_GetCleaningDetails(ByVal bv_intCleaningId As Int64, ByVal intDPT_ID As Int64) As CleaningDataSet
        Try
            Dim dsCleaningDataset As CleaningDataSet
            Dim objCleanings As New Cleanings
            dsCleaningDataset = objCleanings.GetCleaningDetails(bv_intCleaningId, intDPT_ID)
            Return (dsCleaningDataset)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GerCurrencyCode"
    <OperationContract()> _
    Public Function GerCurrencyCode(ByVal bv_intInvoicingPartyId As Int64, ByVal intDPT_ID As Int64) As CleaningDataSet
        Try
            Dim dsCleaningDataset As CleaningDataSet
            Dim objCleanings As New Cleanings
            dsCleaningDataset = objCleanings.GerCurrencyCode(bv_intInvoicingPartyId, intDPT_ID)
            Return (dsCleaningDataset)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetGateInDetails"
    <OperationContract()> _
    Public Function pub_GetActivityStatusDetails(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32, _
                                                 ByVal bv_intCleaningID As Integer, ByVal bv_strGITransactionNo As String) As CleaningDataSet
        Try
            Dim dsCleaningDataset As CleaningDataSet
            Dim objCleanings As New Cleanings
            dsCleaningDataset = objCleanings.GetActivityStatusDetails(bv_strEquipmentNo, bv_intDepotID, bv_intCleaningID, bv_strGITransactionNo)
            Return (dsCleaningDataset)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetCleaningCharge"
    <OperationContract()> _
    Public Function GetCleaningCharge(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32, ByVal bv_strGITransactionNo As String) As CleaningDataSet
        Try
            Dim dsCleaningDataset As CleaningDataSet
            Dim objCleanings As New Cleanings
            dsCleaningDataset = objCleanings.GetCleaningCharge(bv_strEquipmentNo, bv_intDepotID, bv_strGITransactionNo)
            Return (dsCleaningDataset)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "INSERT Pending: pub_CreateCleaning() TABLE NAME:Cleaning"
    <OperationContract()> _
    Public Function pub_CreateCleaning(ByVal bv_i64CleaningId As Int64, _
                                       ByVal bv_strEquipmentNo As String, _
                                       ByVal bv_strCleaningCertificateNo As String, _
                                       ByVal bv_strChemicalName As String, _
                                       ByVal bv_dblCleaningRate As String, _
                                       ByVal bv_datOriginalCleaningDate As DateTime, _
                                       ByVal bv_datLastCleaningDate As DateTime, _
                                       ByVal bv_datOriginalInspectedDate As DateTime, _
                                       ByVal bv_datLastInspectedDate As DateTime, _
                                       ByVal bv_intEquipmentStatus As Int64, _
                                       ByVal bv_strEquipmentStatusCD As String, _
                                       ByVal bv_strCleanedFor As String, _
                                       ByVal bv_strLocOfCleaning As String, _
                                       ByVal bv_intEquipmentCleaningStatus1 As Int64, _
                                       ByVal bv_strEquipmentCleaningStatus1CD As String, _
                                       ByVal bv_intEquipmentCleaningStatus2 As Int64, _
                                       ByVal bv_strEquipmentCleaningStatus2CD As String, _
                                       ByVal bv_intEquipmentCondition As Int64, _
                                       ByVal bv_strEquipmentConditionCD As String, _
                                       ByVal bv_intValveandFittingCondition As Int64, _
                                       ByVal bv_strValveandFittingConditionCD As String, _
                                       ByVal bv_intInvoicingTo As Int64, _
                                       ByVal bv_strInvoicingToCD As String, _
                                       ByVal bv_strManLidSealNo As String, _
                                       ByVal bv_strTopSealNo As String, _
                                       ByVal bv_strBottomSealNo As String, _
                                       ByVal bv_strCustomerReferenceNo As String, _
                                       ByVal bv_strCleaningReferenceNo As String, _
                                       ByVal bv_strRemarks As String, _
                                       ByVal bv_i64CustomerId As Int64, _
                                       ByVal bv_GateinDate As DateTime, _
                                       ByVal bv_strGI_TRANSCTN_NO As String, _
                                       ByVal bv_intDepotId As Int32, _
                                       ByVal bv_strCreatedBy As String, _
                                       ByVal bv_datCreatedDate As DateTime, _
                                       ByRef br_cleaningData As CleaningDataSet, _
                                       ByRef br_strActivitySubmit As String, _
                                       ByVal bv_intActivityId As Integer, _
                                       ByVal bv_blnAdditionalCleaningFlag As Boolean) As Long
        Dim objTrans As New Transactions
        Try
            Dim lngCreated As Long
            Dim objCommonUIs As New CommonUIs
            Dim objCleanings As New Cleanings
            Dim strEquipmentInfoRemarks As String = String.Empty
            Dim strYardLocation As String = String.Empty
            Dim objIndexPattern As New IndexPatterns
            bv_strCleaningCertificateNo = objIndexPattern.GetMaxReferenceNo(CleaningData._CLEANING, Now, objTrans, Nothing, bv_intDepotId)
            If bv_blnAdditionalCleaningFlag = False Then
                lngCreated = objCleanings.CreateCleaning(bv_i64CustomerId, bv_strEquipmentNo, _
                                                         bv_strCleaningCertificateNo, _
                                                         bv_strChemicalName, bv_dblCleaningRate, _
                                                         bv_datOriginalCleaningDate, bv_datLastCleaningDate, bv_datOriginalInspectedDate, _
                                                         bv_datLastInspectedDate, bv_intEquipmentStatus, _
                                                         bv_strCleanedFor, bv_strLocOfCleaning, _
                                                         bv_intEquipmentCleaningStatus1, bv_intEquipmentCleaningStatus2, _
                                                         bv_intEquipmentCondition, bv_intValveandFittingCondition, _
                                                         bv_strManLidSealNo, bv_strTopSealNo, _
                                                         bv_strBottomSealNo, bv_intInvoicingTo, _
                                                         bv_strCustomerReferenceNo, bv_strCleaningReferenceNo, _
                                                         bv_strRemarks, bv_intDepotId, _
                                                         bv_strGI_TRANSCTN_NO, bv_strCreatedBy, _
                                                         bv_datCreatedDate, bv_strCreatedBy, _
                                                         bv_datCreatedDate, Nothing, Nothing, bv_blnAdditionalCleaningFlag, False, objTrans)
                objCleanings.CreateCleaningCharge(bv_strEquipmentNo, _
                                                 bv_i64CustomerId, _
                                                 bv_intInvoicingTo, _
                                                 lngCreated, _
                                                 bv_datOriginalCleaningDate, _
                                                 bv_datOriginalInspectedDate, _
                                                 bv_dblCleaningRate, _
                                                 "U", _
                                                 bv_strCleaningCertificateNo, _
                                                 True, _
                                                 bv_intDepotId, _
                                                 bv_strGI_TRANSCTN_NO, _
                                                 False, _
                                                 objTrans)
            Else
                'objCleanings.UpdateCleaning(bv_i64CleaningId, _
                '                          bv_dblCleaningRate, _
                '                          bv_datLastCleaningDate, _
                '                          bv_datLastInspectedDate, _
                '                          5, _
                '                          bv_strCleanedFor, _
                '                          bv_strLocOfCleaning, _
                '                          bv_intEquipmentCleaningStatus1, _
                '                          bv_intEquipmentCleaningStatus2, _
                '                          bv_intEquipmentCondition, _
                '                          bv_intValveandFittingCondition, _
                '                          bv_strManLidSealNo, _
                '                          bv_strTopSealNo, _
                '                          bv_strBottomSealNo, _
                '                          bv_intInvoicingTo, _
                '                          bv_strCustomerReferenceNo, _
                '                          bv_strCleaningReferenceNo, _
                '                          bv_strRemarks, _
                '                          bv_intDepotId, _
                '                          bv_strGI_TRANSCTN_NO, _
                '                          bv_strCreatedBy, _
                '                          bv_datCreatedDate, _
                '                          "M", _
                '                          False, _
                '                          True, _
                '                          objTrans)


                objCleanings.UpdateCleaning_Inspection(bv_i64CleaningId, _
                                          bv_dblCleaningRate, _
                                          bv_datLastCleaningDate, _
                                          bv_datOriginalInspectedDate, _
                                          bv_datLastInspectedDate, _
                                          5, _
                                          bv_strCleanedFor, _
                                          bv_strLocOfCleaning, _
                                          bv_intEquipmentCleaningStatus1, _
                                          bv_intEquipmentCleaningStatus2, _
                                          bv_intEquipmentCondition, _
                                          bv_intValveandFittingCondition, _
                                          bv_strManLidSealNo, _
                                          bv_strTopSealNo, _
                                          bv_strBottomSealNo, _
                                          bv_intInvoicingTo, _
                                          bv_strCustomerReferenceNo, _
                                          bv_strCleaningReferenceNo, _
                                          bv_strRemarks, _
                                          bv_intDepotId, _
                                          bv_strGI_TRANSCTN_NO, _
                                          bv_strCreatedBy, _
                                          bv_datCreatedDate, _
                                          "M", _
                                          False, _
                                          True, _
                                          objTrans)


                objCleanings.UpdateCleaning_Charge(bv_strEquipmentNo, _
                                                 bv_i64CustomerId, _
                                                 bv_intInvoicingTo, _
                                                 bv_datOriginalCleaningDate, _
                                                 bv_datOriginalInspectedDate, _
                                                 bv_dblCleaningRate, _
                                                 bv_intDepotId, _
                                                 bv_strGI_TRANSCTN_NO, _
                                                 bv_i64CleaningId, _
                                                 objTrans)
                lngCreated = bv_i64CleaningId
            End If

            objCleanings.UpdateActivityStatus(bv_strEquipmentNo, _
                                             bv_datOriginalCleaningDate, _
                                             bv_datOriginalInspectedDate, _
                                             bv_intEquipmentStatus, _
                                             "Cleaning", _
                                             bv_datOriginalInspectedDate, _
                                             bv_strRemarks, _
                                             bv_strGI_TRANSCTN_NO, _
                                             bv_strCustomerReferenceNo, _
                                             bv_intDepotId, objTrans)



            If br_cleaningData.Tables(CleaningData._V_ACTIVITY_STATUS).Rows.Count > 0 Then
                If Not IsDBNull(br_cleaningData.Tables(CleaningData._V_ACTIVITY_STATUS).Rows(0).Item(CleaningData.YRD_LCTN)) Then
                    strYardLocation = CStr(br_cleaningData.Tables(CleaningData._V_ACTIVITY_STATUS).Rows(0).Item(CleaningData.YRD_LCTN))
                End If
            End If

            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(bv_strEquipmentNo, _
                                                                           bv_intDepotId, _
                                                                           objTrans)

            objCommonUIs.CreateTracking(lngCreated, _
                                            bv_i64CustomerId, _
                                            bv_strEquipmentNo, _
                                            "Cleaning", _
                                            bv_intEquipmentStatus, _
                                            CStr(lngCreated), _
                                            bv_datOriginalCleaningDate, _
                                            bv_strRemarks, _
                                            strYardLocation, _
                                            bv_strGI_TRANSCTN_NO, _
                                            bv_intInvoicingTo, _
                                            bv_strCleaningCertificateNo, _
                                            bv_strCreatedBy, _
                                            bv_datCreatedDate, _
                                            bv_strCreatedBy, _
                                            bv_datCreatedDate, _
                                            Nothing, _
                                            Nothing, _
                                            Nothing, _
                                            bv_intDepotId, _
                                            0, _
                                            Nothing, _
                                            strEquipmentInfoRemarks, _
                                            bv_blnAdditionalCleaningFlag, _
                                            objTrans)

            objCommonUIs.CreateTracking(lngCreated, _
                                            bv_i64CustomerId, _
                                            bv_strEquipmentNo, _
                                            "Inspection", _
                                            6, _
                                            CStr(lngCreated), _
                                            bv_datOriginalInspectedDate, _
                                            bv_strRemarks, _
                                            strYardLocation, _
                                            bv_strGI_TRANSCTN_NO, _
                                            bv_intInvoicingTo, _
                                            bv_strCleaningCertificateNo, _
                                            bv_strCreatedBy, _
                                            bv_datCreatedDate, _
                                            bv_strCreatedBy, _
                                            bv_datCreatedDate, _
                                            Nothing, _
                                            Nothing, _
                                            Nothing, _
                                            bv_intDepotId, _
                                            0, _
                                            Nothing, _
                                            strEquipmentInfoRemarks, _
                                            bv_blnAdditionalCleaningFlag, _
                                            objTrans)
            objTrans.commit()
            Return lngCreated
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function
#End Region

#Region "Modify Cleaning:My Submits"
    <OperationContract()> _
    Public Function ModifyCleaning(ByVal bv_intCleaningId As Int64, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByVal bv_strCertificateNo As String, _
                                   ByVal bv_strChemicalName As String, _
                                   ByVal bv_dblCleaningRate As String, _
                                   ByVal bv_datOriginalCleaningDate As DateTime, _
                                   ByVal bv_datLastCleaningDate As DateTime, _
                                   ByVal bv_datOriginalInspectedDate As DateTime, _
                                   ByVal bv_datLastInspectedDate As DateTime, _
                                   ByVal bv_int64EquipmentStatusId As Int64, _
                                   ByVal bv_strEquipmentStatusCD As String, _
                                   ByVal bv_strCleanedFor As String, _
                                   ByVal bv_strLocOfCleaning As String, _
                                   ByVal bv_intEquipmentCleaningStatus1 As Int64, _
                                   ByVal bv_strEquipmentCleaningStatus1CD As String, _
                                   ByVal bv_intEquipmentCleaningStatus2 As Int64, _
                                   ByVal bv_strEquipmentCleaningStatus2CD As String, _
                                   ByVal bv_intEquipmentCondition As Int64, _
                                   ByVal bv_strEquipmentConditionCD As String, _
                                   ByVal bv_intValveandFittingCondition As Int64, _
                                   ByVal bv_strValveandFittingConditionCD As String, _
                                   ByVal bv_intInvoicingTo As Int64, _
                                   ByVal bv_strInvoicingToCD As String, _
                                   ByVal bv_strManLidSealNo As String, _
                                   ByVal bv_strTopSealNo As String, _
                                   ByVal bv_strBottomSealNo As String, _
                                   ByVal bv_strCustomerReferenceNo As String, _
                                   ByVal bv_strCleaningReferenceNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_int64CustomerId As Int64, _
                                   ByVal bv_GateinDate As DateTime, _
                                   ByVal bv_strGI_TRANSCTN_NO As String, _
                                   ByVal bv_intDepotId As Int32, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByRef br_cleaningData As CleaningDataSet, _
                                   ByRef br_strActivitySubmit As String, _
                                   ByVal bv_intActivityId As Integer, _
                                   ByVal bv_blnAdditionalCleaning As Boolean) As Boolean

        Dim objTrans As New Transactions
        Try
            Dim objCleanings As New Cleanings
            Dim objCommonUIs As New CommonUIs
            Dim strEquipmentInfoRemarks As String = String.Empty

            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(bv_strEquipmentNo, _
                                                                           bv_intDepotId, _
                                                                           objTrans)

            If bv_blnAdditionalCleaning Then

                objCleanings.UpdateCleaning(bv_intCleaningId, _
                                            bv_dblCleaningRate, _
                                            bv_datOriginalInspectedDate, _
                                            bv_datLastCleaningDate, _
                                            bv_datLastInspectedDate, _
                                            bv_int64EquipmentStatusId, _
                                            bv_strCleanedFor, _
                                            bv_strLocOfCleaning, _
                                            bv_intEquipmentCleaningStatus1, _
                                            bv_intEquipmentCleaningStatus2, _
                                            bv_intEquipmentCondition, _
                                            bv_intValveandFittingCondition, _
                                            bv_strManLidSealNo, _
                                            bv_strTopSealNo, _
                                            bv_strBottomSealNo, _
                                            bv_intInvoicingTo, _
                                            bv_strCustomerReferenceNo, _
                                            bv_strCleaningReferenceNo, _
                                            bv_strRemarks, _
                                            bv_intDepotId, _
                                            bv_strGI_TRANSCTN_NO, _
                                            bv_strModifiedBy, _
                                            bv_datModifiedDate, _
                                            bv_blnAdditionalCleaning, _
                                            bv_blnAdditionalCleaning, _
                                            objTrans)

                objCleanings.UpdateCleaningCharge(bv_intCleaningId, _
                                                  CDec(bv_dblCleaningRate), _
                                                  objTrans)

                objCleanings.UpdateActivityStatusIdByEqpmntNo(bv_strEquipmentNo, _
                                                              bv_intDepotId, _
                                                              bv_strGI_TRANSCTN_NO, _
                                                              3, _
                                                              objTrans)
                objCommonUIs.CreateTracking(bv_intCleaningId, _
                                            bv_int64CustomerId, _
                                            bv_strEquipmentNo, _
                                            "Additional Cleaning", _
                                            3, _
                                            CStr(bv_intCleaningId), _
                                            bv_datLastInspectedDate, _
                                            bv_strRemarks, _
                                            bv_strLocOfCleaning, _
                                            bv_strGI_TRANSCTN_NO, _
                                            Nothing, _
                                            bv_strCertificateNo, _
                                            bv_strModifiedBy, _
                                            bv_datModifiedDate, _
                                            bv_strModifiedBy, _
                                            bv_datModifiedDate, _
                                            Nothing, _
                                            Nothing, _
                                            Nothing, _
                                            bv_intDepotId, _
                                            0, _
                                            Nothing, _
                                            strEquipmentInfoRemarks, _
                                            bv_blnAdditionalCleaning, _
                                            objTrans)


            Else
                objCleanings.UpdateCleaning(bv_intCleaningId, _
                                           bv_dblCleaningRate, _
                                           bv_datLastCleaningDate, _
                                           bv_datOriginalInspectedDate, _
                                           bv_datLastInspectedDate, _
                                           bv_int64EquipmentStatusId, _
                                           bv_strCleanedFor, _
                                           bv_strLocOfCleaning, _
                                           bv_intEquipmentCleaningStatus1, _
                                           bv_intEquipmentCleaningStatus2, _
                                           bv_intEquipmentCondition, _
                                           bv_intValveandFittingCondition, _
                                           bv_strManLidSealNo, _
                                           bv_strTopSealNo, _
                                           bv_strBottomSealNo, _
                                           bv_intInvoicingTo, _
                                           bv_strCustomerReferenceNo, _
                                           bv_strCleaningReferenceNo, _
                                           bv_strRemarks, _
                                           bv_intDepotId, _
                                           bv_strGI_TRANSCTN_NO, _
                                           bv_strModifiedBy, _
                                           bv_datModifiedDate, _
                                           "M", _
                                           bv_blnAdditionalCleaning, _
                                           False, _
                                           objTrans)
                objCleanings.UpdateCleaning_Charge(bv_strEquipmentNo, _
                                                 bv_int64CustomerId, _
                                                 bv_intInvoicingTo, _
                                                 bv_datOriginalCleaningDate, _
                                                 bv_datOriginalInspectedDate, _
                                                 bv_dblCleaningRate, _
                                                 bv_intDepotId, _
                                                 bv_strGI_TRANSCTN_NO, _
                                                 bv_intCleaningId, _
                                                 objTrans)
                objCleanings.UpdateTracking(bv_strEquipmentNo, _
                                            bv_datOriginalCleaningDate, _
                                            bv_strRemarks, _
                                            bv_strGI_TRANSCTN_NO, _
                                            bv_intInvoicingTo, _
                                            bv_intDepotId, _
                                            "Cleaning", _
                                            CInt(bv_intCleaningId), _
                                            bv_strModifiedBy, _
                                            bv_datModifiedDate, _
                                            strEquipmentInfoRemarks, _
                                            objTrans)



                objCleanings.UpdateTracking(bv_strEquipmentNo, _
                                            bv_datOriginalInspectedDate, _
                                            bv_strRemarks, _
                                            bv_strGI_TRANSCTN_NO, _
                                            bv_intInvoicingTo, _
                                            bv_intDepotId, _
                                            "Inspection", _
                                            CInt(bv_intCleaningId), _
                                            bv_strModifiedBy, _
                                            bv_datModifiedDate, _
                                            strEquipmentInfoRemarks, _
                                            objTrans)

                'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                objCleanings.UpdateActivityRemarksStatus(bv_strEquipmentNo, _
                                                        bv_strRemarks, _
                                                        bv_strGI_TRANSCTN_NO, _
                                                        bv_strCustomerReferenceNo, _
                                                        bv_intDepotId, objTrans)

            End If





            objTrans.commit()
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function
#End Region

#Region "GenerateCleaningCertificate- RDLC Report"

    Public Function pub_GenerateCleaningCertificate(ByRef bv_param As String) As CleaningDataSet
        Dim objTrans As New Transactions
        Try
            Dim dsCleaning As New CleaningDataSet
            Dim objCleanings As New Cleanings
            Dim objCommon As New CommonUIs
            Dim certno As String = String.Empty
            Dim dtCleaning As New DataTable
            Dim dtDepot As New DataTable
            Dim dtCustomer As New DataTable
            Dim intCleaningId As Integer = CInt(objCommon.pub_GetParameter("CleaningID", bv_param))
            Dim intDepotId As Int32 = CInt(objCommon.pub_GetParameter("DepotID", bv_param))
            Dim strCleaningCertificateNo As String = objCommon.pub_GetParameter("CleanginCertificateNo", bv_param)
            Dim strEquipmentNo As String = objCommon.pub_GetParameter("EquipmentNo", bv_param)
            Dim strGI_TRNSCTN_NO As String = objCommon.pub_GetParameter("GI_TRNSCTN_NO", bv_param)
            Dim intCount As Integer = 0
            intCount = CInt(objCleanings.GetCleaningInfo(intCleaningId, intDepotId, strGI_TRNSCTN_NO, objTrans))
            If intCount = 0 Then
                If strCleaningCertificateNo = "" OrElse IsDBNull(strCleaningCertificateNo) Then
                    objCleanings.UpdateCleaningCertificateNo(intCleaningId, _
                                                       strEquipmentNo, _
                                                       intDepotId, _
                                                       strGI_TRNSCTN_NO, _
                                                       True, _
                                                       strCleaningCertificateNo, _
                                                       objTrans)

                    objCleanings.UpdateCleaningChargeCertNo(intCleaningId, _
                                                            strEquipmentNo, _
                                                            intDepotId, _
                                                            strGI_TRNSCTN_NO, _
                                                            strCleaningCertificateNo, _
                                                            objTrans)
                    objCleanings.UpdateTrackingCertNo(intCleaningId, _
                                                      strEquipmentNo, _
                                                      intDepotId, _
                                                      strGI_TRNSCTN_NO, _
                                                      strCleaningCertificateNo, _
                                                      objTrans)
                End If
            End If
            Dim objConfigs As New CommonUIs

            dtCleaning = objCleanings.GetCleaningDetails(intCleaningId, intDepotId, objTrans).Tables(CleaningData._V_CLEANING)
            dtDepot = objConfigs.pub_GetDepotDetail(intDepotId, objTrans).Tables(CommonUIData._DEPOT)
            dtCustomer = objCleanings.GetCustomerDetailsByCustomerId(CLng(dtCleaning.Rows(0).Item(CleaningData.CSTMR_ID)))
            If dsCleaning.Tables(CleaningData._V_CUSTOMER).Rows.Count > 0 Then
                dsCleaning.Tables(CleaningData._V_CUSTOMER).Rows.Clear()
            End If
            dsCleaning.Merge(dtCustomer)
            dsCleaning.Merge(dtCleaning)
            dsCleaning.Merge(dtDepot)

            objTrans.commit()

            Return dsCleaning
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try

    End Function

#End Region

#Region "pub_GetCleaningChargeBilled"
    <OperationContract()> _
    Public Function pub_GetCleaningChargeBilled(ByVal bv_strEquipmentNo As String, _
                                                ByVal bv_intDepotID As Int32, _
                                                ByVal bv_i64CleaningID As Long, _
                                                ByVal bv_strGITransactionNo As String, _
                                                ByVal bv_decOldRate As Decimal) As Boolean
        Try
            Dim dsCleaningDataset As CleaningDataSet
            Dim objCleanings As New Cleanings
            dsCleaningDataset = objCleanings.GetCleaningChargeBilled(bv_strEquipmentNo, bv_intDepotID, bv_i64CleaningID, bv_strGITransactionNo)
            If dsCleaningDataset.Tables(CleaningData._CLEANING_CHARGE).Rows.Count > 0 Then
                Dim decBilledRate As Decimal = 0
                If Not IsDBNull(dsCleaningDataset.Tables(CleaningData._CLEANING_CHARGE).Rows(0).Item(CleaningData.CLNNG_RT)) Then
                    decBilledRate = CDec(dsCleaningDataset.Tables(CleaningData._CLEANING_CHARGE).Rows(0).Item(CleaningData.CLNNG_RT))
                    If bv_decOldRate = decBilledRate Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "Cleaning Inspection - Split"

#Region "Cleaning - Operations"

    <OperationContract()> _
    Public Function pub_CreateCleaning_Clean(ByVal bv_i64CleaningId As Int64, _
                                       ByVal bv_strEquipmentNo As String, _
                                       ByVal bv_strChemicalName As String, _
                                       ByVal bv_dblCleaningRate As String, _
                                       ByVal bv_datOriginalCleaningDate As DateTime, _
                                       ByVal bv_datLastCleaningDate As DateTime, _
                                       ByVal bv_intEquipmentStatus As Int64, _
                                       ByVal bv_strEquipmentStatusCD As String, _
                                       ByVal bv_strCleaningReferenceNo As String, _
                                       ByVal bv_strRemarks As String, _
                                       ByVal bv_i64CustomerId As Int64, _
                                       ByVal bv_GateinDate As DateTime, _
                                       ByVal bv_strGI_TRANSCTN_NO As String, _
                                       ByVal bv_intDepotId As Int32, _
                                       ByVal bv_strCreatedBy As String, _
                                       ByVal bv_datCreatedDate As DateTime, _
                                       ByRef br_cleaningData As CleaningDataSet, _
                                       ByRef br_strActivitySubmit As String, _
                                       ByVal bv_intActivityId As Integer, _
                                       ByVal bv_blnAdditionalCleaningFlag As Boolean, _
                                       ByVal bv_blnSlabRateFlag As Boolean) As Long
        Dim objTrans As New Transactions
        Try
            Dim lngCreated As Long = 0
            Dim objCommonUIs As New CommonUIs
            Dim objCleanings As New Cleanings
            Dim strEquipmentInfoRemarks As String = String.Empty
            Dim strYardLocation As String = String.Empty
            'If bv_blnAdditionalCleaningFlag = False Then
            '    lngCreated = objCleanings.CreateCleaning_Clean(bv_i64CustomerId, bv_strEquipmentNo, _
            '                                             bv_strChemicalName, bv_dblCleaningRate, _
            '                                             bv_datOriginalCleaningDate, bv_datLastCleaningDate, bv_intEquipmentStatus, _
            '                                              bv_strCleaningReferenceNo, _
            '                                             bv_strRemarks, bv_intDepotId, _
            '                                             bv_strGI_TRANSCTN_NO, bv_strCreatedBy, _
            '                                             bv_datCreatedDate, bv_strCreatedBy, _
            '                                             bv_datCreatedDate, Nothing, Nothing, bv_blnAdditionalCleaningFlag, False, objTrans)
            '    objCleanings.CreateCleaningCharge_Clean(bv_strEquipmentNo, _
            '                                     bv_i64CustomerId, _
            '                                     lngCreated, _
            '                                     bv_datOriginalCleaningDate, _
            '                                     bv_dblCleaningRate, _
            '                                     "U", _
            '                                     True, _
            '                                     bv_intDepotId, _
            '                                     bv_strGI_TRANSCTN_NO, _
            '                                     objTrans)
            'Else
            '    objCleanings.UpdateCleaning_Clean(bv_i64CleaningId, _
            '                              bv_dblCleaningRate, _
            '                              bv_datLastCleaningDate, _
            '                              5, _
            '                              bv_strCleaningReferenceNo, _
            '                              bv_strRemarks, _
            '                              bv_intDepotId, _
            '                              bv_strGI_TRANSCTN_NO, _
            '                              bv_strCreatedBy, _
            '                              bv_datCreatedDate, _
            '                              "M", _
            '                              True, _
            '                              objTrans)
            '    objCleanings.UpdateCleaning_Charge_Clean(bv_strEquipmentNo, _
            '                                     bv_i64CustomerId, _
            '                                     bv_datOriginalCleaningDate, _
            '                                     bv_dblCleaningRate, _
            '                                     bv_intDepotId, _
            '                                     bv_strGI_TRANSCTN_NO, _
            '                                     bv_i64CleaningId, _
            '                                     objTrans)
            '    lngCreated = bv_i64CleaningId
            'End If

            '''''''''''''''''''''''''''''''''''''''''''''''''
            If bv_blnAdditionalCleaningFlag = False Then

                Dim intCleaningCombination As Int32 = 0

                intCleaningCombination = objCleanings.CheckCleaningCombination(bv_strEquipmentNo, bv_strGI_TRANSCTN_NO, bv_i64CustomerId, bv_intDepotId, objTrans)

                If intCleaningCombination = 0 Then



                    lngCreated = objCleanings.CreateCleaning_Clean(bv_i64CustomerId, bv_strEquipmentNo, _
                                                            bv_strChemicalName, bv_dblCleaningRate, _
                                                            bv_datOriginalCleaningDate, bv_datLastCleaningDate, bv_intEquipmentStatus, _
                                                             bv_strCleaningReferenceNo, _
                                                            bv_strRemarks, bv_intDepotId, _
                                                            bv_strGI_TRANSCTN_NO, bv_strCreatedBy, _
                                                            bv_datCreatedDate, bv_strCreatedBy, _
                                                            bv_datCreatedDate, Nothing, Nothing, bv_blnAdditionalCleaningFlag, False, objTrans)
                    objCleanings.CreateCleaningCharge_Clean(bv_strEquipmentNo, _
                                                     bv_i64CustomerId, _
                                                     lngCreated, _
                                                     bv_datLastCleaningDate, _
                                                     bv_dblCleaningRate, _
                                                     "U", _
                                                     True, _
                                                     bv_intDepotId, _
                                                     bv_strGI_TRANSCTN_NO, _
                                                     bv_blnSlabRateFlag, _
                                                     objTrans)

                Else

                    objCleanings.UpdateCleaning_CleanCombination(bv_i64CustomerId, bv_strEquipmentNo, _
                                                           bv_strChemicalName, bv_dblCleaningRate, _
                                                           bv_datOriginalCleaningDate, bv_datLastCleaningDate, bv_intEquipmentStatus, _
                                                            bv_strCleaningReferenceNo, _
                                                           bv_strRemarks, bv_intDepotId, _
                                                           bv_strGI_TRANSCTN_NO, bv_strCreatedBy, _
                                                           bv_datCreatedDate, bv_strCreatedBy, _
                                                           bv_datCreatedDate, Nothing, Nothing, bv_blnAdditionalCleaningFlag, False, objTrans)

                    objCleanings.UpdateCleaningCharge_CleanCombination(bv_strEquipmentNo, _
                                                   bv_i64CustomerId, _
                                                   lngCreated, _
                                                   bv_datLastCleaningDate, _
                                                   bv_dblCleaningRate, _
                                                   "U", _
                                                   True, _
                                                   bv_intDepotId, _
                                                   bv_strGI_TRANSCTN_NO, _
                                                   bv_blnSlabRateFlag, _
                                                   objTrans)

                    'UpdateCleaning_CleanCombination
                    'UpdateCleaningCharge_CleanCombination
                End If

                objCleanings.UpdateActivityStatus_Clean(bv_strEquipmentNo, _
                                                 bv_datLastCleaningDate, _
                                                 bv_intEquipmentStatus, _
                                                 "Cleaning", _
                                                 bv_strRemarks, _
                                                 bv_strGI_TRANSCTN_NO, _
                                                 bv_intDepotId, objTrans)




            Else

                objCleanings.UpdateCleaning_Clean(bv_i64CleaningId, _
                                          bv_dblCleaningRate, _
                                          bv_datLastCleaningDate, _
                                          5, _
                                          bv_strCleaningReferenceNo, _
                                          bv_strRemarks, _
                                          bv_intDepotId, _
                                          bv_strGI_TRANSCTN_NO, _
                                          bv_strCreatedBy, _
                                          bv_datCreatedDate, _
                                          "M", _
                                          True, _
                                          objTrans)

                objCleanings.UpdateCleaning_Charge_Clean(bv_strEquipmentNo, _
                                                 bv_i64CustomerId, _
                                                 bv_datLastCleaningDate, _
                                                 bv_dblCleaningRate, _
                                                 bv_intDepotId, _
                                                 bv_strGI_TRANSCTN_NO, _
                                                 bv_i64CleaningId, _
                                                 objTrans)


                objCleanings.UpdateActivityStatus_Clean(bv_strEquipmentNo, _
                                                 bv_datLastCleaningDate, _
                                                 bv_intEquipmentStatus, _
                                                 "Cleaning", _
                                                 bv_strRemarks, _
                                                 bv_strGI_TRANSCTN_NO, _
                                                 bv_intDepotId, objTrans)

            End If


            If br_cleaningData.Tables(CleaningData._V_ACTIVITY_STATUS).Rows.Count > 0 Then
                If Not IsDBNull(br_cleaningData.Tables(CleaningData._V_ACTIVITY_STATUS).Rows(0).Item(CleaningData.YRD_LCTN)) Then
                    strYardLocation = CStr(br_cleaningData.Tables(CleaningData._V_ACTIVITY_STATUS).Rows(0).Item(CleaningData.YRD_LCTN))
                End If
            End If

            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(bv_strEquipmentNo, _
                                                                           bv_intDepotId, _
                                                                           objTrans)
            If lngCreated = 0 Then
                lngCreated = bv_i64CleaningId
            End If

            objCommonUIs.CreateTracking_Clean(bv_i64CustomerId, _
                                            bv_strEquipmentNo, _
                                            "Cleaning", _
                                            bv_intEquipmentStatus, _
                                            CStr(lngCreated), _
                                            bv_strRemarks, _
                                            strYardLocation, _
                                            bv_strGI_TRANSCTN_NO, _
                                            bv_datLastCleaningDate, _
                                            bv_strCreatedBy, _
                                            bv_datCreatedDate, _
                                            bv_strCreatedBy, _
                                            bv_datCreatedDate, _
                                            Nothing, _
                                            Nothing, _
                                            Nothing, _
                                            bv_intDepotId, _
                                            0, _
                                            Nothing, _
                                            strEquipmentInfoRemarks, _
                                            bv_blnAdditionalCleaningFlag, _
                                            objTrans)


            'objCommonUIs.CreateTracking(bv_i64CustomerId, _
            '                                bv_strEquipmentNo, _
            '                                "Inspection", _
            '                                6, _
            '                                CStr(lngCreated), _
            '                                bv_strRemarks, _
            '                                strYardLocation, _
            '                                bv_strGI_TRANSCTN_NO, _
            '                                bv_intInvoicingTo, _
            '                                bv_strCreatedBy, _
            '                                bv_datCreatedDate, _
            '                                bv_strCreatedBy, _
            '                                bv_datCreatedDate, _
            '                                Nothing, _
            '                                Nothing, _
            '                                Nothing, _
            '                                bv_intDepotId, _
            '                                0, _
            '                                Nothing, _
            '                                strEquipmentInfoRemarks, _
            '                                bv_blnAdditionalCleaningFlag, _
            '                                objTrans)
            objTrans.commit()
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                   MethodBase.GetCurrentMethod.Name, String.Concat("EQPMNT_NO : ", bv_strEquipmentNo, _
                                                                     "  GI_TRNSCTN_NO : ", bv_strGI_TRANSCTN_NO, _
                                                                    "ADDTNL_CLNNG_FLG : ", bv_blnAdditionalCleaningFlag))
            Return lngCreated
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function

    <OperationContract()> _
    Public Function ModifyCleaning_Clean(ByVal bv_intCleaningId As Int64, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByVal bv_strChemicalName As String, _
                                   ByVal bv_dblCleaningRate As String, _
                                   ByVal bv_datOriginalCleaningDate As DateTime, _
                                   ByVal bv_datLastCleaningDate As DateTime, _
                                   ByVal bv_int64EquipmentStatusId As Int64, _
                                   ByVal bv_strEquipmentStatusCD As String, _
                                   ByVal bv_strCleaningReferenceNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_int64CustomerId As Int64, _
                                   ByVal bv_GateinDate As DateTime, _
                                   ByVal bv_strGI_TRANSCTN_NO As String, _
                                   ByVal bv_intDepotId As Int32, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByRef br_cleaningData As CleaningDataSet, _
                                   ByRef br_strActivitySubmit As String, _
                                   ByVal bv_intActivityId As Integer, _
                                   ByVal bv_blnAdditionalCleaning As Boolean) As Boolean

        Dim objTrans As New Transactions
        Try
            Dim objCleanings As New Cleanings
            Dim objCommonUIs As New CommonUIs
            Dim strEquipmentInfoRemarks As String = String.Empty

            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(bv_strEquipmentNo, _
                                                                           bv_intDepotId, _
                                                                           objTrans)

            'If bv_blnAdditionalCleaning Then

            '    objCleanings.UpdateCleaning_Clean(bv_intCleaningId, _
            '                                bv_dblCleaningRate, _
            '                                bv_datLastCleaningDate, _
            '                                bv_int64EquipmentStatusId, _
            '                                bv_strCleaningReferenceNo, _
            '                                bv_strRemarks, _
            '                                bv_intDepotId, _
            '                                bv_strGI_TRANSCTN_NO, _
            '                                bv_strModifiedBy, _
            '                                bv_datModifiedDate, _
            '                                Nothing, _
            '                                bv_blnAdditionalCleaning, _
            '                                objTrans)

            '    objCleanings.UpdateCleaningCharge(bv_intCleaningId, _
            '                                      CDec(bv_dblCleaningRate), _
            '                                      objTrans)

            '    objCleanings.UpdateActivityStatusIdByEqpmntNo(bv_strEquipmentNo, _
            '                                                  bv_intDepotId, _
            '                                                  bv_strGI_TRANSCTN_NO, _
            '                                                  3, _
            '                                                  objTrans)
            '    objCommonUIs.CreateTracking_Clean(bv_int64CustomerId, _
            '                                bv_strEquipmentNo, _
            '                                "Additional Cleaning", _
            '                                3, _
            '                                CStr(bv_intCleaningId), _
            '                                bv_strRemarks, _
            '                                "yrd", _
            '                                bv_strGI_TRANSCTN_NO, _
            '                                Nothing, _
            '                                bv_strModifiedBy, _
            '                                bv_datModifiedDate, _
            '                                bv_strModifiedBy, _
            '                                bv_datModifiedDate, _
            '                                Nothing, _
            '                                Nothing, _
            '                                Nothing, _
            '                                bv_intDepotId, _
            '                                0, _
            '                                Nothing, _
            '                                strEquipmentInfoRemarks, _
            '                                bv_blnAdditionalCleaning, _
            '                                objTrans)


            'Else
            '    objCleanings.UpdateCleaning_Clean(bv_intCleaningId, _
            '                               bv_dblCleaningRate, _
            '                               bv_datLastCleaningDate, _
            '                               bv_int64EquipmentStatusId, _
            '                               bv_strCleaningReferenceNo, _
            '                               bv_strRemarks, _
            '                               bv_intDepotId, _
            '                               bv_strGI_TRANSCTN_NO, _
            '                               bv_strModifiedBy, _
            '                               bv_datModifiedDate, _
            '                               "M", _
            '                               False, _
            '                               objTrans)
            '    objCleanings.UpdateCleaning_Charge_Clean(bv_strEquipmentNo, _
            '                                     bv_int64CustomerId, _
            '                                     bv_datOriginalCleaningDate, _
            '                                     bv_dblCleaningRate, _
            '                                     bv_intDepotId, _
            '                                     bv_strGI_TRANSCTN_NO, _
            '                                     bv_intCleaningId, _
            '                                     objTrans)
            '    objCleanings.UpdateTracking(bv_strEquipmentNo, _
            '                                bv_datOriginalCleaningDate, _
            '                                bv_strRemarks, _
            '                                bv_strGI_TRANSCTN_NO, _
            '                                bv_intDepotId, _
            '                                "Cleaning", _
            '                                CInt(bv_intCleaningId), _
            '                                bv_strModifiedBy, _
            '                                bv_datModifiedDate, _
            '                                strEquipmentInfoRemarks, _
            '                                objTrans)



            '    'objCleanings.UpdateTracking(bv_strEquipmentNo, _
            '    '                            bv_datOriginalCleaningDate, _
            '    '                            bv_strRemarks, _
            '    '                            bv_strGI_TRANSCTN_NO, _
            '    '                            bv_intInvoicingTo, _
            '    '                            bv_intDepotId, _
            '    '                            "Inspection", _
            '    '                            CInt(bv_intCleaningId), _
            '    '                            bv_strModifiedBy, _
            '    '                            bv_datModifiedDate, _
            '    '                            strEquipmentInfoRemarks, _
            '    '                            objTrans)

            '    'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
            '    objCleanings.UpdateActivityRemarksStatus_Clean(bv_strEquipmentNo, _
            '                                            bv_strRemarks, _
            '                                            bv_strGI_TRANSCTN_NO, _
            '                                            bv_intDepotId, objTrans)

            'End If

            objCleanings.UpdateCleaning_Clean(bv_intCleaningId, _
                                          bv_dblCleaningRate, _
                                          bv_datLastCleaningDate, _
                                          bv_int64EquipmentStatusId, _
                                          bv_strCleaningReferenceNo, _
                                          bv_strRemarks, _
                                          bv_intDepotId, _
                                          bv_strGI_TRANSCTN_NO, _
                                          bv_strModifiedBy, _
                                          bv_datModifiedDate, _
                                          "M", _
                                          False, _
                                          objTrans)
            objCleanings.UpdateCleaning_Charge_Clean(bv_strEquipmentNo, _
                                             bv_int64CustomerId, _
                                             bv_datOriginalCleaningDate, _
                                             bv_dblCleaningRate, _
                                             bv_intDepotId, _
                                             bv_strGI_TRANSCTN_NO, _
                                             bv_intCleaningId, _
                                             objTrans)
            objCleanings.UpdateTracking_Clean(bv_strEquipmentNo, _
                                        bv_datOriginalCleaningDate, _
                                        bv_strRemarks, _
                                        bv_strGI_TRANSCTN_NO, _
                                        bv_intDepotId, _
                                        "Cleaning", _
                                        CInt(bv_intCleaningId), _
                                        bv_strModifiedBy, _
                                        bv_datModifiedDate, _
                                        strEquipmentInfoRemarks, _
                                        objTrans)

            objCleanings.UpdateActivityStatus_Clean(bv_strEquipmentNo, _
                                                bv_datLastCleaningDate, _
                                                bv_int64EquipmentStatusId, _
                                                "Cleaning", _
                                                bv_strRemarks, _
                                                bv_strGI_TRANSCTN_NO, _
                                                bv_intDepotId, objTrans)

            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
            objCleanings.UpdateActivityRemarksStatus_Clean(bv_strEquipmentNo, _
                                                    bv_strRemarks, _
                                                    bv_strGI_TRANSCTN_NO, _
                                                    bv_intDepotId, objTrans)



            objTrans.commit()
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, String.Concat("EQPMNT_NO : ", bv_strEquipmentNo, _
                                                                    "  GI_TRNSCTN_NO : ", bv_strGI_TRANSCTN_NO, _
                                                                   "    ADDTNL_CLNNG_FLG : ", bv_blnAdditionalCleaning))
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function


    'Update Print Cleaning inspection ref No
    <OperationContract()> _
    Public Function UpdateActvity_CleaningInspectionRefNo(ByVal bv_strEqpmntNo As String) As String

        Dim objTrans As New Transactions

        Try
            Dim objCleanings As New Cleanings

            Dim objInspections As New CleaningInspections
            Dim strCleaningInspectionRefNo As String = objInspections.UpdateCleaningInstructionNo(strCleaningInspectionRefNo, _
                                                                                                   objTrans)

            objCleanings.UpdateActvity_CleaningInspectionRefNo(bv_strEqpmntNo, strCleaningInspectionRefNo, objTrans)

            objTrans.commit()

            Return strCleaningInspectionRefNo
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "Inspection - Operations"

    <OperationContract()> _
    Public Function pub_Create_Inspection(ByVal bv_i64CleaningId As Int64, _
                                       ByVal bv_strEquipmentNo As String, _
                                       ByVal bv_strCleaningCertificateNo As String, _
                                       ByVal bv_strChemicalName As String, _
                                       ByVal bv_dblCleaningRate As String, _
                                       ByVal bv_datOriginalCleaningDate As DateTime, _
                                       ByVal bv_datLastCleaningDate As DateTime, _
                                       ByVal bv_datOriginalInspectedDate As DateTime, _
                                       ByVal bv_datLastInspectedDate As DateTime, _
                                       ByVal bv_intEquipmentStatus As Int64, _
                                       ByVal bv_strEquipmentStatusCD As String, _
                                       ByVal bv_strCleanedFor As String, _
                                       ByVal bv_strLocOfCleaning As String, _
                                       ByVal bv_intEquipmentCleaningStatus1 As Int64, _
                                       ByVal bv_strEquipmentCleaningStatus1CD As String, _
                                       ByVal bv_intEquipmentCleaningStatus2 As Int64, _
                                       ByVal bv_strEquipmentCleaningStatus2CD As String, _
                                       ByVal bv_intEquipmentCondition As Int64, _
                                       ByVal bv_strEquipmentConditionCD As String, _
                                       ByVal bv_intValveandFittingCondition As Int64, _
                                       ByVal bv_strValveandFittingConditionCD As String, _
                                       ByVal bv_intInvoicingTo As Int64, _
                                       ByVal bv_strInvoicingToCD As String, _
                                       ByVal bv_strManLidSealNo As String, _
                                       ByVal bv_strTopSealNo As String, _
                                       ByVal bv_strBottomSealNo As String, _
                                       ByVal bv_strCustomerReferenceNo As String, _
                                       ByVal bv_strCleaningReferenceNo As String, _
                                       ByVal bv_strRemarks As String, _
                                       ByVal bv_i64CustomerId As Int64, _
                                       ByVal bv_GateinDate As DateTime, _
                                       ByVal bv_strGI_TRANSCTN_NO As String, _
                                       ByVal bv_intDepotId As Int32, _
                                       ByVal bv_strCreatedBy As String, _
                                       ByVal bv_datCreatedDate As DateTime, _
                                       ByRef br_cleaningData As CleaningDataSet, _
                                       ByRef br_strActivitySubmit As String, _
                                       ByVal bv_intActivityId As Integer, _
                                       ByVal bv_blnAdditionalCleaningFlag As Boolean, _
                                       ByVal bv_strAdditionalCleaning_Status As String) As Long
        Dim objTrans As New Transactions
        Try
            Dim lngCreated As Long
            Dim objCommonUIs As New CommonUIs
            Dim objCleanings As New Cleanings
            Dim strEquipmentInfoRemarks As String = String.Empty
            Dim strYardLocation As String = String.Empty
            Dim strCleaningCertificateNo As String = String.Empty
            'If bv_blnAdditionalCleaningFlag = False Then
            'lngCreated = objCleanings.CreateCleaning(bv_i64CustomerId, bv_strEquipmentNo, _
            '                                         bv_strCleaningCertificateNo, _
            '                                         bv_strChemicalName, bv_dblCleaningRate, _
            '                                         bv_datOriginalCleaningDate, bv_datLastCleaningDate, bv_datOriginalInspectedDate, _
            '                                         bv_datLastInspectedDate, bv_intEquipmentStatus, _
            '                                         bv_strCleanedFor, bv_strLocOfCleaning, _
            '                                         bv_intEquipmentCleaningStatus1, bv_intEquipmentCleaningStatus2, _
            '                                         bv_intEquipmentCondition, bv_intValveandFittingCondition, _
            '                                         bv_strManLidSealNo, bv_strTopSealNo, _
            '                                         bv_strBottomSealNo, bv_intInvoicingTo, _
            '                                         bv_strCustomerReferenceNo, bv_strCleaningReferenceNo, _
            '                                         bv_strRemarks, bv_intDepotId, _
            '                                         bv_strGI_TRANSCTN_NO, bv_strCreatedBy, _
            '                                         bv_datCreatedDate, bv_strCreatedBy, _
            '                                         bv_datCreatedDate, Nothing, Nothing, bv_blnAdditionalCleaningFlag, False, objTrans)
            '    objCleanings.CreateCleaningCharge(bv_strEquipmentNo, _
            '                                     bv_i64CustomerId, _
            '                                     bv_intInvoicingTo, _
            '                                     lngCreated, _
            '                                     bv_datOriginalCleaningDate, _
            '                                     bv_datOriginalInspectedDate, _
            '                                     bv_dblCleaningRate, _
            '                                     "U", _
            '                                     bv_strCleaningCertificateNo, _
            '                                     True, _
            '                                     bv_intDepotId, _
            '                                     bv_strGI_TRANSCTN_NO, _
            '                                     objTrans)
            'Else
            '    objCleanings.UpdateCleaning(bv_i64CleaningId, _
            '                              bv_dblCleaningRate, _
            '                              bv_datLastCleaningDate, _
            '                              bv_datLastInspectedDate, _
            '                              5, _
            '                              bv_strCleanedFor, _
            '                              bv_strLocOfCleaning, _
            '                              bv_intEquipmentCleaningStatus1, _
            '                              bv_intEquipmentCleaningStatus2, _
            '                              bv_intEquipmentCondition, _
            '                              bv_intValveandFittingCondition, _
            '                              bv_strManLidSealNo, _
            '                              bv_strTopSealNo, _
            '                              bv_strBottomSealNo, _
            '                              bv_intInvoicingTo, _
            '                              bv_strCustomerReferenceNo, _
            '                              bv_strCleaningReferenceNo, _
            '                              bv_strRemarks, _
            '                              bv_intDepotId, _
            '                              bv_strGI_TRANSCTN_NO, _
            '                              bv_strCreatedBy, _
            '                              bv_datCreatedDate, _
            '                              "M", _
            '                              False, _
            '                              True, _
            '                              objTrans)
            '    objCleanings.UpdateCleaning_Charge(bv_strEquipmentNo, _
            '                                     bv_i64CustomerId, _
            '                                     bv_intInvoicingTo, _
            '                                     bv_datOriginalCleaningDate, _
            '                                     bv_datOriginalInspectedDate, _
            '                                     bv_dblCleaningRate, _
            '                                     bv_intDepotId, _
            '                                     bv_strGI_TRANSCTN_NO, _
            '                                     bv_i64CleaningId, _
            '                                     objTrans)
            '    lngCreated = bv_i64CleaningId
            'End If

            If bv_blnAdditionalCleaningFlag = False Then

                objCleanings.UpdateCleaning_Inspection(bv_i64CleaningId, _
                                                 bv_dblCleaningRate, _
                                                 bv_datLastCleaningDate, _
                                                 bv_datOriginalInspectedDate, _
                                                 bv_datLastInspectedDate, _
                                                 6, _
                                                 bv_strCleanedFor, _
                                                 bv_strLocOfCleaning, _
                                                 bv_intEquipmentCleaningStatus1, _
                                                 bv_intEquipmentCleaningStatus2, _
                                                 bv_intEquipmentCondition, _
                                                 bv_intValveandFittingCondition, _
                                                 bv_strManLidSealNo, _
                                                 bv_strTopSealNo, _
                                                 bv_strBottomSealNo, _
                                                 bv_intInvoicingTo, _
                                                 bv_strCustomerReferenceNo, _
                                                 bv_strCleaningReferenceNo, _
                                                 bv_strRemarks, _
                                                 bv_intDepotId, _
                                                 bv_strGI_TRANSCTN_NO, _
                                                 bv_strCreatedBy, _
                                                 bv_datCreatedDate, _
                                                 "M", _
                                                 False, _
                                                 True, _
                                                 objTrans)

                objCleanings.UpdateCleaning_Charge(bv_strEquipmentNo, _
                                                 bv_i64CustomerId, _
                                                 bv_intInvoicingTo, _
                                                 bv_datOriginalCleaningDate, _
                                                 bv_datOriginalInspectedDate, _
                                                 bv_dblCleaningRate, _
                                                 bv_intDepotId, _
                                                 bv_strGI_TRANSCTN_NO, _
                                                 bv_i64CleaningId, _
                                                 objTrans)

                objCleanings.UpdateActivityStatus(bv_strEquipmentNo, _
                                           bv_datLastCleaningDate, _
                                           bv_datLastInspectedDate, _
                                           6, _
                                           "Inspection", _
                                           bv_datLastInspectedDate, _
                                           bv_strRemarks, _
                                           bv_strGI_TRANSCTN_NO, _
                                           bv_strCustomerReferenceNo, _
                                           bv_intDepotId, objTrans)

                objCommonUIs.CreateTracking(lngCreated, _
                                                bv_i64CustomerId, _
                                                bv_strEquipmentNo, _
                                                "Inspection", _
                                                6, _
                                                CStr(bv_i64CleaningId), _
                                                bv_datLastInspectedDate, _
                                                bv_strRemarks, _
                                                strYardLocation, _
                                                bv_strGI_TRANSCTN_NO, _
                                                bv_intInvoicingTo, _
                                                bv_strCleaningCertificateNo, _
                                                bv_strCreatedBy, _
                                                bv_datCreatedDate, _
                                                bv_strCreatedBy, _
                                                bv_datCreatedDate, _
                                                Nothing, _
                                                Nothing, _
                                                Nothing, _
                                                bv_intDepotId, _
                                                0, _
                                                Nothing, _
                                                strEquipmentInfoRemarks, _
                                                bv_blnAdditionalCleaningFlag, _
                                                objTrans)

                'Create Cleaning Certificate
                'from index pattern

                Dim objIndexPattern As New IndexPatterns
                '  strCleaningCertificateNo = objIndexPattern.GetMaxReferenceNo(String.Concat(CleaningData._CLEANING_INSPECTION, ",", "Inspection"), Now, objTrans, Nothing, bv_intDepotId)
                strCleaningCertificateNo = objIndexPattern.GetMaxReferenceNo(String.Concat(CleaningData._CLEANING_CERTIFICATE, ",", "Inspection", ",", bv_i64CustomerId), Now, objTrans, Nothing, bv_intDepotId)
                objCleanings.UpdateCleaningCertificateNo(CInt(bv_i64CleaningId), _
                                                     bv_strEquipmentNo, _
                                                     bv_intDepotId, _
                                                     bv_strGI_TRANSCTN_NO, _
                                                     True, _
                                                     strCleaningCertificateNo, _
                                                     objTrans)

                objCleanings.UpdateCleaningCertificateNo_Charge(CInt(bv_i64CleaningId), _
                                                bv_strEquipmentNo, _
                                                bv_intDepotId, _
                                                bv_strGI_TRANSCTN_NO, _
                                                True, _
                                                strCleaningCertificateNo, _
                                                objTrans)

            Else
                'Additional Cleaning - Back to Cleaning Status
                Dim intEquipment_StatusId As Int64 = 3
                If bv_strAdditionalCleaning_Status.ToLower() = "repair" Then
                    intEquipment_StatusId = 7
                End If



                objCleanings.UpdateCleaning(bv_i64CleaningId, _
                                         bv_dblCleaningRate, _
                                             bv_datLastCleaningDate, _
                                             bv_datOriginalInspectedDate, _
                                             bv_datLastInspectedDate, _
                                             intEquipment_StatusId, _
                                             bv_strCleanedFor, _
                                             bv_strLocOfCleaning, _
                                             bv_intEquipmentCleaningStatus1, _
                                             bv_intEquipmentCleaningStatus2, _
                                             bv_intEquipmentCondition, _
                                             bv_intValveandFittingCondition, _
                                             bv_strManLidSealNo, _
                                             bv_strTopSealNo, _
                                             bv_strBottomSealNo, _
                                             bv_intInvoicingTo, _
                                             bv_strCustomerReferenceNo, _
                                             bv_strCleaningReferenceNo, _
                                             bv_strRemarks, _
                                             bv_intDepotId, _
                                             bv_strGI_TRANSCTN_NO, _
                                             bv_strCreatedBy, _
                                             bv_datCreatedDate, _
                                             "M", _
                                             True, _
                                             True, _
                                             objTrans)

                objCleanings.UpdateCleaning_Charge(bv_strEquipmentNo, _
                                                 bv_i64CustomerId, _
                                                 bv_intInvoicingTo, _
                                                 bv_datOriginalCleaningDate, _
                                                 bv_datOriginalInspectedDate, _
                                                 bv_dblCleaningRate, _
                                                 bv_intDepotId, _
                                                 bv_strGI_TRANSCTN_NO, _
                                                 bv_i64CleaningId, _
                                                 objTrans)


                '' ''Delete existing Cleaning Information & Cleaning Charge, In case of additional cleaning

                ' ''objCleanings.DeleteCleaning(bv_i64CleaningId, bv_intDepotId, objTrans)

                ' ''objCleanings.DeleteCleaning_Charge(bv_strEquipmentNo, bv_i64CleaningId, bv_intDepotId, bv_strGI_TRANSCTN_NO, objTrans)

                'Update Activity Status
                objCleanings.UpdateActivityStatus(bv_strEquipmentNo, _
                                         bv_datLastCleaningDate, _
                                         bv_datLastInspectedDate, _
                                         intEquipment_StatusId, _
                                         "Cleaning", _
                                         bv_datLastInspectedDate, _
                                         bv_strRemarks, _
                                         bv_strGI_TRANSCTN_NO, _
                                         bv_strCustomerReferenceNo, _
                                         bv_intDepotId, objTrans)


                objCommonUIs.CreateTracking(lngCreated, _
                                                bv_i64CustomerId, _
                                                bv_strEquipmentNo, _
                                                "Additional Cleaning", _
                                                intEquipment_StatusId, _
                                               CStr(bv_i64CleaningId), _
                                                bv_datLastInspectedDate, _
                                                bv_strRemarks, _
                                                strYardLocation, _
                                                bv_strGI_TRANSCTN_NO, _
                                                bv_intInvoicingTo, _
                                                bv_strCleaningCertificateNo, _
                                                bv_strCreatedBy, _
                                                bv_datCreatedDate, _
                                                bv_strCreatedBy, _
                                                bv_datCreatedDate, _
                                                Nothing, _
                                                Nothing, _
                                                Nothing, _
                                                bv_intDepotId, _
                                                0, _
                                                Nothing, _
                                                strEquipmentInfoRemarks, _
                                                bv_blnAdditionalCleaningFlag, _
                                                objTrans)

            End If

            'objCleanings.UpdateActivityStatus(bv_strEquipmentNo, _
            '                                 bv_datOriginalCleaningDate, _
            '                                 bv_datOriginalInspectedDate, _
            '                                 bv_intEquipmentStatus, _
            '                                 "Cleaning", _
            '                                 bv_datOriginalInspectedDate, _
            '                                 bv_strRemarks, _
            '                                 bv_strGI_TRANSCTN_NO, _
            '                                 bv_strCustomerReferenceNo, _
            '                                 bv_intDepotId, objTrans)



            If br_cleaningData.Tables(CleaningData._V_ACTIVITY_STATUS).Rows.Count > 0 Then
                If Not IsDBNull(br_cleaningData.Tables(CleaningData._V_ACTIVITY_STATUS).Rows(0).Item(CleaningData.YRD_LCTN)) Then
                    strYardLocation = CStr(br_cleaningData.Tables(CleaningData._V_ACTIVITY_STATUS).Rows(0).Item(CleaningData.YRD_LCTN))
                End If
            End If

            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(bv_strEquipmentNo, _
                                                                           bv_intDepotId, _
                                                                           objTrans)

            'objCommonUIs.CreateTracking(lngCreated, _
            '                                bv_i64CustomerId, _
            '                                bv_strEquipmentNo, _
            '                                "Cleaning", _
            '                                bv_intEquipmentStatus, _
            '                                CStr(lngCreated), _
            '                                bv_datOriginalCleaningDate, _
            '                                bv_strRemarks, _
            '                                strYardLocation, _
            '                                bv_strGI_TRANSCTN_NO, _
            '                                bv_intInvoicingTo, _
            '                                bv_strCleaningCertificateNo, _
            '                                bv_strCreatedBy, _
            '                                bv_datCreatedDate, _
            '                                bv_strCreatedBy, _
            '                                bv_datCreatedDate, _
            '                                Nothing, _
            '                                Nothing, _
            '                                Nothing, _
            '                                bv_intDepotId, _
            '                                0, _
            '                                Nothing, _
            '                                strEquipmentInfoRemarks, _
            '                                bv_blnAdditionalCleaningFlag, _
            '                                objTrans)

            'objCommonUIs.CreateTracking(lngCreated, _
            '                                bv_i64CustomerId, _
            '                                bv_strEquipmentNo, _
            '                                "Inspection", _
            '                                6, _
            '                                CStr(lngCreated), _
            '                                bv_datOriginalInspectedDate, _
            '                                bv_strRemarks, _
            '                                strYardLocation, _
            '                                bv_strGI_TRANSCTN_NO, _
            '                                bv_intInvoicingTo, _
            '                                bv_strCleaningCertificateNo, _
            '                                bv_strCreatedBy, _
            '                                bv_datCreatedDate, _
            '                                bv_strCreatedBy, _
            '                                bv_datCreatedDate, _
            '                                Nothing, _
            '                                Nothing, _
            '                                Nothing, _
            '                                bv_intDepotId, _
            '                                0, _
            '                                Nothing, _
            '                                strEquipmentInfoRemarks, _
            '                                bv_blnAdditionalCleaningFlag, _
            '                                objTrans)
            objTrans.commit()
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, String.Concat("EQPMNT_NO : ", bv_strEquipmentNo, _
                                                                    "  GI_TRNSCTN_NO : ", bv_strGI_TRANSCTN_NO, _
                                                                   "    ADDTNL_CLNNG_FLG : ", bv_blnAdditionalCleaningFlag))
            Return lngCreated
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function

    <OperationContract()> _
    Public Function ModifyCleaning_Inspection(ByVal bv_intCleaningId As Int64, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByVal bv_strCertificateNo As String, _
                                   ByVal bv_strChemicalName As String, _
                                   ByVal bv_dblCleaningRate As String, _
                                   ByVal bv_datOriginalCleaningDate As DateTime, _
                                   ByVal bv_datLastCleaningDate As DateTime, _
                                   ByVal bv_datOriginalInspectedDate As DateTime, _
                                   ByVal bv_datLastInspectedDate As DateTime, _
                                   ByVal bv_int64EquipmentStatusId As Int64, _
                                   ByVal bv_strEquipmentStatusCD As String, _
                                   ByVal bv_strCleanedFor As String, _
                                   ByVal bv_strLocOfCleaning As String, _
                                   ByVal bv_intEquipmentCleaningStatus1 As Int64, _
                                   ByVal bv_strEquipmentCleaningStatus1CD As String, _
                                   ByVal bv_intEquipmentCleaningStatus2 As Int64, _
                                   ByVal bv_strEquipmentCleaningStatus2CD As String, _
                                   ByVal bv_intEquipmentCondition As Int64, _
                                   ByVal bv_strEquipmentConditionCD As String, _
                                   ByVal bv_intValveandFittingCondition As Int64, _
                                   ByVal bv_strValveandFittingConditionCD As String, _
                                   ByVal bv_intInvoicingTo As Int64, _
                                   ByVal bv_strInvoicingToCD As String, _
                                   ByVal bv_strManLidSealNo As String, _
                                   ByVal bv_strTopSealNo As String, _
                                   ByVal bv_strBottomSealNo As String, _
                                   ByVal bv_strCustomerReferenceNo As String, _
                                   ByVal bv_strCleaningReferenceNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_int64CustomerId As Int64, _
                                   ByVal bv_GateinDate As DateTime, _
                                   ByVal bv_strGI_TRANSCTN_NO As String, _
                                   ByVal bv_intDepotId As Int32, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByRef br_cleaningData As CleaningDataSet, _
                                   ByRef br_strActivitySubmit As String, _
                                   ByVal bv_intActivityId As Integer, _
                                   ByVal bv_blnAdditionalCleaning As Boolean, _
                                   ByVal bv_NextAcitivity As String) As Boolean

        Dim objTrans As New Transactions
        Try
            Dim objCleanings As New Cleanings
            Dim objCommonUIs As New CommonUIs
            Dim strEquipmentInfoRemarks As String = String.Empty

            'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
            strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(bv_strEquipmentNo, _
                                                                           bv_intDepotId, _
                                                                           objTrans)

            If bv_NextAcitivity.ToLower() = "next" Then



                objCleanings.UpdateCleaning_Ins_Mysubmit(bv_intCleaningId, _
                                               bv_dblCleaningRate, _
                                               bv_datLastCleaningDate, _
                                               bv_datOriginalInspectedDate, _
                                               bv_datLastInspectedDate, _
                                               6, _
                                               bv_strCleanedFor, _
                                               bv_strLocOfCleaning, _
                                               bv_intEquipmentCleaningStatus1, _
                                               bv_intEquipmentCleaningStatus2, _
                                               bv_intEquipmentCondition, _
                                               bv_intValveandFittingCondition, _
                                               bv_strManLidSealNo, _
                                               bv_strTopSealNo, _
                                               bv_strBottomSealNo, _
                                               bv_intInvoicingTo, _
                                               bv_strCustomerReferenceNo, _
                                               bv_strCleaningReferenceNo, _
                                               bv_strRemarks, _
                                               bv_intDepotId, _
                                               bv_strGI_TRANSCTN_NO, _
                                               bv_strModifiedBy, _
                                               bv_datModifiedDate, _
                                               "M", _
                                               bv_blnAdditionalCleaning, _
                                               False, _
                                               objTrans)
                objCleanings.UpdateCleaning_Charge_Ins_Mysub(bv_strEquipmentNo, _
                                                 bv_int64CustomerId, _
                                                 bv_intInvoicingTo, _
                                                 bv_datOriginalCleaningDate, _
                                                 bv_datOriginalInspectedDate, _
                                                 bv_dblCleaningRate, _
                                                 bv_intDepotId, _
                                                 bv_strGI_TRANSCTN_NO, _
                                                 bv_intCleaningId, _
                                                 objTrans)




                objCleanings.UpdateTracking_Ins_MySub(bv_strEquipmentNo, _
                                            bv_datLastInspectedDate, _
                                            bv_strRemarks, _
                                            bv_strGI_TRANSCTN_NO, _
                                            bv_intInvoicingTo, _
                                            bv_intDepotId, _
                                            "Inspection", _
                                            CInt(bv_intCleaningId), _
                                            bv_strModifiedBy, _
                                            bv_datModifiedDate, _
                                            strEquipmentInfoRemarks, _
                                            objTrans)

                'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                objCleanings.UpdateActivityRemarksStatus(bv_strEquipmentNo, _
                                                        bv_strRemarks, _
                                                        bv_strGI_TRANSCTN_NO, _
                                                        bv_strCustomerReferenceNo, _
                                                        bv_intDepotId, objTrans)

                objCleanings.UpdateActivityStatus_ins_MySub(bv_strEquipmentNo, _
                                              bv_datLastCleaningDate, _
                                              bv_datLastInspectedDate, _
                                              6, _
                                              "Inspection", _
                                              bv_datLastInspectedDate, _
                                              bv_strRemarks, _
                                              bv_strGI_TRANSCTN_NO, _
                                              bv_strCustomerReferenceNo, _
                                              bv_intDepotId, objTrans)

            ElseIf bv_NextAcitivity.ToLower() = "current" Then

                objCleanings.UpdateCleaning(bv_intCleaningId, _
                                           bv_dblCleaningRate, _
                                           bv_datLastCleaningDate, _
                                           bv_datOriginalInspectedDate, _
                                           bv_datLastInspectedDate, _
                                           6, _
                                           bv_strCleanedFor, _
                                           bv_strLocOfCleaning, _
                                           bv_intEquipmentCleaningStatus1, _
                                           bv_intEquipmentCleaningStatus2, _
                                           bv_intEquipmentCondition, _
                                           bv_intValveandFittingCondition, _
                                           bv_strManLidSealNo, _
                                           bv_strTopSealNo, _
                                           bv_strBottomSealNo, _
                                           bv_intInvoicingTo, _
                                           bv_strCustomerReferenceNo, _
                                           bv_strCleaningReferenceNo, _
                                           bv_strRemarks, _
                                           bv_intDepotId, _
                                           bv_strGI_TRANSCTN_NO, _
                                           bv_strModifiedBy, _
                                           bv_datModifiedDate, _
                                           "M", _
                                           bv_blnAdditionalCleaning, _
                                           False, _
                                           objTrans)

                objCleanings.UpdateCleaning_Charge(bv_strEquipmentNo, _
                                                 bv_int64CustomerId, _
                                                 bv_intInvoicingTo, _
                                                 bv_datOriginalCleaningDate, _
                                                 bv_datOriginalInspectedDate, _
                                                 bv_dblCleaningRate, _
                                                 bv_intDepotId, _
                                                 bv_strGI_TRANSCTN_NO, _
                                                 bv_intCleaningId, _
                                                 objTrans)


                objCleanings.UpdateTracking(bv_strEquipmentNo, _
                                            bv_datLastInspectedDate, _
                                            bv_strRemarks, _
                                            bv_strGI_TRANSCTN_NO, _
                                            bv_intInvoicingTo, _
                                            bv_intDepotId, _
                                            "Inspection", _
                                            CInt(bv_intCleaningId), _
                                            bv_strModifiedBy, _
                                            bv_datModifiedDate, _
                                            strEquipmentInfoRemarks, _
                                            objTrans)

                'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                objCleanings.UpdateActivityRemarksStatus(bv_strEquipmentNo, _
                                                        bv_strRemarks, _
                                                        bv_strGI_TRANSCTN_NO, _
                                                        bv_strCustomerReferenceNo, _
                                                        bv_intDepotId, objTrans)

                objCleanings.UpdateActivityStatus_ins(bv_strEquipmentNo, _
                                              bv_datLastCleaningDate, _
                                              bv_datLastInspectedDate, _
                                              6, _
                                              "Inspection", _
                                              bv_datLastInspectedDate, _
                                              bv_strRemarks, _
                                              bv_strGI_TRANSCTN_NO, _
                                              bv_strCustomerReferenceNo, _
                                              bv_intDepotId, objTrans)





            End If
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, String.Concat("EQPMNT_NO : ", bv_strEquipmentNo, _
                                                                    "  GI_TRNSCTN_NO : ", bv_strGI_TRANSCTN_NO, _
                                                                   "    ADDTNL_CLNNG_FLG : ", bv_blnAdditionalCleaning))
            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function


    Public Function pub_CustomerEDIValidation(ByVal bv_strCustomerCode As String, ByVal bv_intDPT_ID As Int32) As Int32
        Try
            Dim objCleanings As New Cleanings
            Return objCleanings.pub_CustomerEDIValidation(bv_strCustomerCode, bv_intDPT_ID)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    Public Function pub_GetCleaningInsructionReportDetails(ByVal bv_strEquipmentNo As String, ByVal bv_intDPT_ID As Int32) As DataTable
        Try
            Dim objCleanings As New Cleanings
            Return objCleanings.pub_GetCleaningInsructionReportDetails(bv_strEquipmentNo, bv_intDPT_ID)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region


#End Region

#Region "Cleaning Slab Rate"

#Region "pub_CheckSlabRateExists"
    <OperationContract()> _
    Public Function pub_CheckSlabRateExists(ByVal bv_i32Cstmr_Id As Integer, ByVal bv_Eqpmnt_Typ_ID As Integer) As Boolean
        Try
            Dim objcleanings As New Cleanings
            Dim dtCleaningRate As New DataTable
            dtCleaningRate = objcleanings.GetCleaningSlabRate(bv_i32Cstmr_Id, bv_Eqpmnt_Typ_ID)
            If dtCleaningRate.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#End Region
End Class