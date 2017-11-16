Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class PreAdvice
#Region "pub_PreAdviceGetPreAdvicebyDepotID()"

    <OperationContract()> _
    Public Function pub_PreAdviceGetPreAdvicebyDepotID(ByVal bv_strWFDATA As String) As PreAdviceDataSet

        Try
            Dim dsPreAdviceData As PreAdviceDataSet
            Dim objPreAdvices As New PreAdvices
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, PreAdviceData.DPT_ID))
            objPreAdvices.GetPreAdviceAttachmentByDepotID(intDepotID)
            dsPreAdviceData = objPreAdvices.GetPreAdviceByDepotID(intDepotID)
            Return dsPreAdviceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    '<OperationContract()> _
    'Public Function pub_PreAdviceAttachmentGetPreAdvicebyDepotID(ByVal bv_strWFDATA As String) As PreAdviceDataSet

    '    Try
    '        Dim dsPreAdviceData As PreAdviceDataSet
    '        Dim objPreAdvices As New PreAdvices
    '        Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, PreAdviceData.DPT_ID))
    '        dsPreAdviceData = objPreAdvices.GetPreAdviceAttachmentByDepotID(intDepotID)
    '        Return dsPreAdviceData
    '    Catch ex As Exception
    '        Throw New FaultException(New FaultReason(ex.Message))
    '    End Try
    'End Function
#End Region

#Region "pub_ValidateEquipmentNoByDepotID"
    <OperationContract()> _
    Public Function pub_ValidateEquipmentNoByDepotID(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32, _
                                                     ByRef br_strCustomer As String) As Boolean

        Try
            Dim dsPreAdviceData As PreAdviceDataSet
            Dim objPreAdvices As New PreAdvices
            Dim blnValid As Boolean = True
            dsPreAdviceData = objPreAdvices.GetPreAdviceEquipmentFromGateIn(bv_strEquipmentNo, bv_intDepotID)
            If dsPreAdviceData.Tables(PreAdviceData._GATEIN).Rows.Count > 0 Then
                blnValid = CBool(dsPreAdviceData.Tables(PreAdviceData._GATEIN).Rows(0).Item(PreAdviceData.GTOT_BT))
                br_strCustomer = CStr(dsPreAdviceData.Tables(PreAdviceData._GATEIN).Rows(0).Item(PreAdviceData.CSTMR_CD))
            End If
            Return blnValid
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdatePreAdvice()"

    <OperationContract()> _
    Public Function pub_UpdatePreAdvice(ByRef br_dsPreAdvice As PreAdviceDataSet, _
                                        ByVal bv_strModifiedBy As String, _
                                        ByVal bv_datModifiedDate As DateTime, _
                                        ByVal bv_intDepotID As Integer) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objPreAdvice As New PreAdvices
            Dim dtPreAdvice As DataTable
            Dim intEqpSize As Long
            Dim intPreviousCargo As Long
            Dim stCleaningReference As String = ""
            Dim strRemarks As String = ""
            Dim lngCreatedAttachment As Long
            Dim objCommonUIs As New CommonUIs
            Dim strAuthNo As String = ""
            Dim strConsignee As String = ""
            dtPreAdvice = br_dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE)
            Dim lngEquipmentCode As Int64
            If Not dtPreAdvice Is Nothing Then
                For Each drPreAdvice As DataRow In dtPreAdvice.Rows
                    If drPreAdvice.RowState = DataRowState.Added Or drPreAdvice.RowState = DataRowState.Modified Then

                        If Not (drPreAdvice.Item(PreAdviceData.EQPMNT_SZ_ID) Is DBNull.Value) Then
                            intEqpSize = CLng(drPreAdvice.Item(PreAdviceData.EQPMNT_SZ_ID))
                        Else
                            intEqpSize = vbEmpty
                        End If
                        If Not ((drPreAdvice.Item(PreAdviceData.PRDCT_ID)) Is DBNull.Value) Then
                            intPreviousCargo = CInt(drPreAdvice.Item(PreAdviceData.PRDCT_ID))
                        Else
                            intPreviousCargo = vbEmpty

                        End If
                        If ((drPreAdvice.Item(PreAdviceData.CLNNG_RFRNC_NO)) Is DBNull.Value) Then
                            stCleaningReference = String.Empty
                        Else
                            stCleaningReference = CStr(drPreAdvice.Item(PreAdviceData.CLNNG_RFRNC_NO))
                        End If
                        If ((drPreAdvice.Item(PreAdviceData.RMRKS_VC)) Is DBNull.Value) Then
                            strRemarks = String.Empty
                        Else
                            strRemarks = CStr(drPreAdvice.Item(PreAdviceData.RMRKS_VC))
                        End If
                        '  AUTH_NO,CNSGN_NM
                        If ((drPreAdvice.Item(PreAdviceData.AUTH_NO)) Is DBNull.Value) Then
                            strAuthNo = String.Empty
                        Else
                            strAuthNo = CStr(drPreAdvice.Item(PreAdviceData.AUTH_NO))
                        End If
                        If ((drPreAdvice.Item(PreAdviceData.CNSGN_NM)) Is DBNull.Value) Then
                            strConsignee = String.Empty
                        Else
                            strConsignee = CStr(drPreAdvice.Item(PreAdviceData.CNSGN_NM))
                        End If

                    End If
                    Select Case drPreAdvice.RowState
                        Case DataRowState.Added
                            If Not IsDBNull(drPreAdvice.Item(PreAdviceData.EQPMNT_NO)) Then
                                Dim lngPreAdviceID As Long

                                If drPreAdvice.Item(PreAdviceData.EQPMNT_TYP_ID) Is DBNull.Value Then
                                    Dim dsEqpData As CommonUIDataSet
                                    Dim objConfigs As New CommonUIs
                                    If objConfigs.GetMultiLocationSupportConfig.ToLower = "true" Then
                                        dsEqpData = objConfigs.GetEquipmentType(CStr(drPreAdvice.Item(PreAdviceData.EQPMNT_TYP_CD)), CInt(objCommonUIs.GetHeadQuarterID()))
                                    Else
                                        dsEqpData = objConfigs.GetEquipmentType(CStr(drPreAdvice.Item(PreAdviceData.EQPMNT_TYP_CD)), bv_intDepotID)
                                    End If

                                    drPreAdvice.Item(PreAdviceData.EQPMNT_TYP_ID) = dsEqpData.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                                    dsEqpData.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Clear()
                                End If

                                lngPreAdviceID = objPreAdvice.CreatePreAdvice(CStr(drPreAdvice.Item(PreAdviceData.EQPMNT_NO)).ToString(), _
                                                                               CLng(drPreAdvice.Item(PreAdviceData.EQPMNT_TYP_ID)), _
                                                                               intEqpSize, CLng(drPreAdvice.Item(PreAdviceData.CSTMR_ID)), _
                                                                               intPreviousCargo, _
                                                                               stCleaningReference, _
                                                                               CDate(drPreAdvice.Item(PreAdviceData.ENTRD_DT)), _
                                                                               strRemarks, _
                                                                               bv_strModifiedBy, _
                                                                               bv_datModifiedDate, _
                                                                               bv_strModifiedBy, _
                                                                               bv_datModifiedDate, _
                                                                               bv_intDepotID, Nothing, _
                                                                               strAuthNo, strConsignee, _
                                                                                CLng(drPreAdvice.Item(PreAdviceData.EQPMNT_TYP_ID)), _
                                                                               objTrans)
                                'For Attachment Added
                                'For Each drAttachment As DataRow In br_dsPreAdvice.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", lngPreAdviceID))
                                For Each drAttachment As DataRow In br_dsPreAdvice.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", drPreAdvice.Item(PreAdviceData.PR_ADVC_ID)))
                                    lngCreatedAttachment = objCommonUIs.CreateAttachment(lngPreAdviceID,
                                                                        "Pre-Advice", _
                                                                        "", _
                                                                        "", _
                                                                        CStr(drAttachment.Item(CommonUIData.ATTCHMNT_PTH)), _
                                                                        CStr(drAttachment.Item(CommonUIData.ACTL_FL_NM)),
                                                                        bv_strModifiedBy, _
                                                                        bv_datModifiedDate, _
                                                                        bv_intDepotID,
                                                                        objTrans)
                                Next

                                'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                                'Dim strEquipmentInfoRemarks As String = String.Empty
                                'strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(drPreAdvice.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                '                                                               bv_intDepotID, _
                                '                                                               objTrans)

                                objCommonUIs.CreateTracking(lngPreAdviceID, _
                                                   CLng(drPreAdvice.Item(PreAdviceData.CSTMR_ID)), _
                                                   CStr(drPreAdvice.Item(PreAdviceData.EQPMNT_NO)).ToString, _
                                                   "Pre-Advice", _
                                                   0, _
                                                   CStr(lngPreAdviceID), _
                                                   CDate(drPreAdvice.Item(PreAdviceData.ENTRD_DT)), _
                                                   strRemarks, _
                                                   Nothing, _
                                                   Nothing, _
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
                                                   Nothing, _
                                                   False, _
                                                   objTrans)
                            End If
                        Case DataRowState.Modified
                            objPreAdvice.UpdatePreAdvice(CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID)), _
                                                                           CStr(drPreAdvice.Item(PreAdviceData.PR_ADVC_CD)).ToString(), _
                                                                           CStr(drPreAdvice.Item(PreAdviceData.EQPMNT_NO)).ToString(), _
                                                                           CLng(drPreAdvice.Item(PreAdviceData.EQPMNT_TYP_ID)), _
                                                                           intEqpSize, _
                                                                           CLng(drPreAdvice.Item(PreAdviceData.CSTMR_ID)), _
                                                                           intPreviousCargo, _
                                                                           stCleaningReference, _
                                                                           CDate(drPreAdvice.Item(PreAdviceData.ENTRD_DT)), _
                                                                           strRemarks, _
                                                                           bv_strModifiedBy, bv_datModifiedDate, _
                                                                           bv_intDepotID, Nothing, _
                                                                        strAuthNo, strConsignee, _
                                                                        CLng(drPreAdvice.Item(PreAdviceData.EQPMNT_TYP_ID)), _
                                                                           objTrans)

                            objPreAdvice.UpdateTracking(CStr(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID)), _
                                                        CStr(drPreAdvice.Item(PreAdviceData.EQPMNT_NO)).ToString(), _
                                                        CLng(drPreAdvice.Item(PreAdviceData.CSTMR_ID)), _
                                                        CDate(drPreAdvice.Item(PreAdviceData.ENTRD_DT)), _
                                                        strRemarks, _
                                                        Nothing, _
                                                        bv_intDepotID, _
                                                        bv_strModifiedBy, _
                                                        bv_datModifiedDate, _
                                                        objTrans)
                            'Attachment : Delete old record and add new
                            Dim blnDeleteAttachment As Boolean = False
                            For Each drAttachment As DataRow In br_dsPreAdvice.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID))))
                                If drAttachment.RowState <> DataRowState.Deleted Then
                                    blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO).ToString, _
                                                                                                      CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID)), _
                                                                                                      drAttachment.Item(CommonUIData.RPR_ESTMT_NO).ToString, _
                                                                                                      bv_intDepotID, _
                                                                                                      objTrans)
                                Else
                                    blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO, DataRowVersion.Original).ToString, _
                                                                                                      CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID)), _
                                                                                                     drAttachment.Item(CommonUIData.RPR_ESTMT_NO, DataRowVersion.Original).ToString, _
                                                                                                     bv_intDepotID, _
                                                                                                     objTrans)
                                End If
                            Next
                            If br_dsPreAdvice.Tables(CommonUIData._ATTACHMENT).Rows.Count = 0 Then
                                '    For Each drAttachment As DataRow In br_dsPreAdvice.Tables(CommonUIData._V_REPAIR_ESTIMATE).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID))))
                                For Each drAttachment As DataRow In br_dsPreAdvice.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID))))
                                    If drAttachment.RowState <> DataRowState.Deleted Then
                                        blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO).ToString, _
                                                                                                         CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID)), _
                                                                                                          drAttachment.Item(CommonUIData.RPR_ESTMT_NO).ToString, _
                                                                                                          bv_intDepotID, _
                                                                                                          objTrans)
                                    End If
                                Next
                            Else
                                For Each drAttachment As DataRow In br_dsPreAdvice.Tables(CommonUIData._ATTACHMENT).Rows
                                    If drAttachment.RowState = DataRowState.Deleted Then
                                        If CLng(drAttachment.Item(RepairCompletionData.RPR_ESTMT_ID, DataRowVersion.Original)) = CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID)) Then
                                            blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO, DataRowVersion.Original).ToString, _
                                                                                                    CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID)), _
                                                                                                   drAttachment.Item(CommonUIData.RPR_ESTMT_NO, DataRowVersion.Original).ToString, _
                                                                                                   bv_intDepotID, _
                                                                                                   objTrans)
                                        End If
                                    End If
                                Next
                            End If
                            For Each drAttachment As DataRow In br_dsPreAdvice.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID))))
                                If drAttachment.RowState = DataRowState.Deleted Then
                                    blnDeleteAttachment = objCommonUIs.DeleteAttachmentByActivityName(drAttachment.Item(CommonUIData.GI_TRNSCTN_NO, DataRowVersion.Original).ToString, _
                                                                                                     CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID)), _
                                                                                                     drAttachment.Item(CommonUIData.RPR_ESTMT_NO, DataRowVersion.Original).ToString, _
                                                                                                     bv_intDepotID, _
                                                                                                     objTrans)
                                End If
                            Next

                            For Each drAttachment As DataRow In br_dsPreAdvice.Tables(CommonUIData._ATTACHMENT).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID))))
                                lngCreatedAttachment = objCommonUIs.CreateAttachment(CLng(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID)),
                                                                    "Pre-Advice", _
                                                                    "", _
                                                                    "", _
                                                                    CStr(drAttachment.Item(CommonUIData.ATTCHMNT_PTH)), _
                                                                    CStr(drAttachment.Item(CommonUIData.ACTL_FL_NM)),
                                                                    bv_strModifiedBy, _
                                                                    bv_datModifiedDate, _
                                                                    bv_intDepotID,
                                                                    objTrans)
                            Next

                        Case DataRowState.Deleted
                            objPreAdvice.DeletePreAdvice(CInt(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID, DataRowVersion.Original)), _
                                                         objTrans)

                            objPreAdvice.DeleteTracking(CInt(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID, DataRowVersion.Original)), _
                                                            bv_intDepotID, _
                                                            "Pre-Advice", _
                                                            objTrans)
                            
                            objCommonUIs.DeleteAttachmentByActivityName("", _
                                                                        CInt(drPreAdvice.Item(PreAdviceData.PR_ADVC_ID, DataRowVersion.Original)), _
                                                                        "", _
                                                                        bv_intDepotID, _
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

#Region "GetRentalDetails"
    <OperationContract()> _
    Public Function GetRentalDetails(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32, ByRef br_strCustomer As String, ByRef blnAllowRental As Boolean) As Boolean

        Try
            Dim dsPreAdviceDataSet As New PreAdviceDataSet
            Dim objPreAdvice As New PreAdvices
            Dim blnValid As Boolean = True
            dsPreAdviceDataSet = objPreAdvice.GetRentalDetails(bv_strEquipmentNo, bv_intDepotID)
            If dsPreAdviceDataSet.Tables(PreAdviceData._V_RENTAL_ENTRY).Rows.Count > 0 Then
                blnAllowRental = CBool(dsPreAdviceDataSet.Tables(PreAdviceData._V_RENTAL_ENTRY).Rows(0).Item(PreAdviceData.ALLOW_RENTAL))
                br_strCustomer = CStr(dsPreAdviceDataSet.Tables(PreAdviceData._V_RENTAL_ENTRY).Rows(0).Item(PreAdviceData.CSTMR_CD))
                blnValid = False
            End If
            Return blnValid
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetSupplierDetails() TABLE NAME:SUPPLIER_EQUIPMENT_DETAIL"
    <OperationContract()> _
    Public Function pub_GetSupplierDetails(ByVal bv_strEquipmentNo As String, ByVal bv_DeoptID As Integer) As PreAdviceDataSet
        Try
            Dim dsPreAdvice As PreAdviceDataSet
            Dim objPreAdvices As New PreAdvices
            dsPreAdvice = objPreAdvices.GetSupplierDetails(bv_strEquipmentNo, bv_DeoptID)
            Return dsPreAdvice
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

    'For Attchment
#Region "pub_PreAdviceGetPreAdvicebyDepotIDAttchemnt()"

    <OperationContract()> _
    Public Function pub_PreAdviceGetPreAdvicebyDepotIDAttchemnt(ByVal bv_intDepoID As Int64) As PreAdviceDataSet

        Try
            Dim dsPreAdviceData As PreAdviceDataSet
            Dim objPreAdvices As New PreAdvices
            dsPreAdviceData = objPreAdvices.GetPreAdviceByDepotIDAttchment(bv_intDepoID)
            Return dsPreAdviceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ValidateEquipmentNoInPreAdvice"
    <OperationContract()> _
    Public Function pub_ValidateEquipmentNoInPreAdvice(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32) As Boolean

        Try
            Dim dsPreAdviceData As PreAdviceDataSet
            Dim objPreAdvices As New PreAdvices
            Dim blnValid As Boolean = True
            dsPreAdviceData = objPreAdvices.GetValidateEquipmentNoInPreAdvice(bv_strEquipmentNo, bv_intDepotID)
            If dsPreAdviceData.Tables(PreAdviceData._PRE_ADVICE).Rows.Count > 0 Then
                blnValid = False
            End If
            Return blnValid
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ValidateStatusOfEquipment"
    <OperationContract()> _
    Public Function pub_ValidateStatusOfEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32) As Boolean

        Try
            Dim objPreAdvices As New PreAdvices
            Dim intRowCount As Integer
            intRowCount = CInt(objPreAdvices.ValidateStatusOfEquipment(bv_strEquipmentNo, bv_intDepotID))
            If intRowCount > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class
