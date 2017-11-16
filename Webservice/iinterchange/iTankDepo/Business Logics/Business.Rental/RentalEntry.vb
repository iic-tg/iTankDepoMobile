Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class RentalEntry
#Region "getEqpmntNoFromEquipmentInfo() "
    Public Function getEqpmntNoFromEquipmentInfo(ByVal bv_DeoptID As Integer) As RentalEntryDataSet
        Try
            Dim dsRental As RentalEntryDataSet
            Dim objRentalEntrys As New RentalEntrys
            dsRental = objRentalEntrys.getEqpmntNoFromEquipmentInfo(bv_DeoptID)
            Return dsRental
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "getRentalEntryDetails() "
    Public Function getRentalEntryDetails(ByVal bv_DeoptID As Integer) As RentalEntryDataSet
        Try
            Dim dsRental As RentalEntryDataSet
            Dim objRentalEntrys As New RentalEntrys
            dsRental = objRentalEntrys.getRentalEntryDetails(bv_DeoptID)
            Return dsRental
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "getGateOutEquipments() "
    Public Function getGateOutEquipments(ByVal bv_DeoptID As Integer) As RentalEntryDataSet
        Try
            Dim dsRental As RentalEntryDataSet
            Dim objRentalEntrys As New RentalEntrys
            dsRental = objRentalEntrys.getGateOutEquipments(bv_DeoptID)
            Return dsRental
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetOtherChargeBy_RentalID() "
    Public Function pub_GetOtherChargeBy_RentalID(ByVal bv_RentalID As Integer) As RentalEntryDataSet
        Try
            Dim dsRental As RentalEntryDataSet
            Dim objRentalEntrys As New RentalEntrys
            dsRental = objRentalEntrys.pub_GetOtherChargeBy_RentalID(bv_RentalID)
            Return dsRental
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "UpdateRental"
    <OperationContract()> _
    Public Function UpdateRental(ByRef br_dsRental As RentalEntryDataSet, _
                                        ByVal bv_strModifiedBy As String, _
                                        ByVal bv_datModifiedDate As DateTime, _
                                        ByVal bv_intDepotID As Integer, _
                                        ByRef br_strRentalExist As String) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objRental As New RentalEntrys
            Dim dtRental As DataTable
            Dim stCleaningReference As String = ""
            Dim strRemarks As String = ""
            Dim objCommonUIs As New CommonUIs
            Dim lngCreated As Long
            Dim AdditionalRate As Decimal
            dtRental = br_dsRental.Tables(RentalEntryData._V_RENTAL_ENTRY)

            If Not dtRental Is Nothing Then
                For Each drRental As DataRow In dtRental.Select(RentalEntryData.CHECKED & "='True'")
                    Dim OnHireDate As Date
                    Dim PoRefNo As String = String.Empty
                    Dim OffHireDate As String = String.Empty
                    Dim Remarks As String = String.Empty
                    Dim RentalRefNo As String = String.Empty
                    Dim strYardLocation As String = String.Empty
                    Dim strGI_Trans_No As String = String.Empty
                    Dim cstmr_id As Integer
                    Dim dblOtherCharge As Double
                    Dim dblRate As Double
                    Dim dtActivityStatus As New DataTable
                    dtActivityStatus = br_dsRental.Tables(RentalEntryData._V_ACTIVITY_STATUS).Clone()
                    If drRental.RowState = DataRowState.Added Or drRental.RowState = DataRowState.Modified Then
                        If Not (drRental.Item(RentalEntryData.PO_RFRNC_NO) Is DBNull.Value) Then
                            PoRefNo = CStr(drRental.Item(RentalEntryData.PO_RFRNC_NO))
                        Else
                            PoRefNo = Nothing
                        End If
                        If Not (drRental.Item(RentalEntryData.ON_HR_DT) Is DBNull.Value) Then
                            OnHireDate = CDate(drRental.Item(RentalEntryData.ON_HR_DT))
                        Else
                            OnHireDate = Nothing
                        End If

                        If Not IsDBNull(drRental.Item(RentalEntryData.OFF_HR_DT)) Then
                            OffHireDate = CStr(drRental.Item(RentalEntryData.OFF_HR_DT))
                        End If

                        If ((drRental.Item(RentalEntryData.RMRKS_VC)) Is DBNull.Value) Then
                            Remarks = String.Empty
                        Else
                            Remarks = CStr(drRental.Item(RentalEntryData.RMRKS_VC))
                        End If

                        If Not (drRental.Item(RentalEntryData.OTHR_CHRG_NC) Is DBNull.Value) Then
                            dblOtherCharge = CDbl(drRental.Item(RentalEntryData.OTHR_CHRG_NC))
                        Else
                            dblOtherCharge = vbEmpty
                        End If
                        If br_dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Compute(String.Concat("SUM(", RentalEntryData.RT_NC, ")"), String.Concat(RentalEntryData.RNTL_ENTRY_ID, " = ", drRental.Item(RentalEntryData.RNTL_ENTRY_ID))).ToString <> "" Then
                            AdditionalRate = CDec(br_dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Compute(String.Concat("SUM(", RentalEntryData.RT_NC, ")"), String.Concat(RentalEntryData.RNTL_ENTRY_ID, " = ", drRental.Item(RentalEntryData.RNTL_ENTRY_ID))))
                        End If
                    End If
                    '  dtActivityStatus.Rows.Clear()
                    Select Case drRental.RowState
                        Case DataRowState.Added
                            Dim blnExist As Boolean = False
                            blnExist = objRental.GetRentalEntryEquipmentByID(CStr(drRental.Item(RentalEntryData.EQPMNT_NO)), bv_intDepotID, objTrans)
                            If blnExist = False Then
                                dtActivityStatus = objRental.GetEqpmntDetails((drRental.Item(RentalEntryData.EQPMNT_NO).ToString), _
                                                                                bv_intDepotID, _
                                                                                objTrans)
                                If dtActivityStatus.Rows.Count > 0 Then
                                    If Not (dtActivityStatus.Rows(0).Item(RentalEntryData.GI_TRNSCTN_NO) Is DBNull.Value) Then
                                        strGI_Trans_No = CStr(dtActivityStatus.Rows(0).Item(RentalEntryData.GI_TRNSCTN_NO))

                                    Else
                                        strGI_Trans_No = Nothing
                                    End If
                                Else
                                    strGI_Trans_No = Nothing
                                End If

                                'From Index Pattern
                                Dim objIndexPattern As New IndexPatterns
                                ' RentalRefNo = objIndexPattern.GetMaxReferenceNo(RentalEntryData._RENTAL_ENTRY, OnHireDate, objTrans, Nothing, bv_intDepotID)
                                RentalRefNo = objIndexPattern.GetMaxReferenceNo(RentalEntryData._RENTAL_ENTRY, Now, objTrans, Nothing, bv_intDepotID)
                                lngCreated = objRental.CreateRentalEntry((drRental.Item(RentalEntryData.EQPMNT_NO).ToString), _
                                                                              CLng(drRental.Item(RentalEntryData.CSTMR_ID)), _
                                                                              CStr(drRental.Item(RentalEntryData.CNTRCT_RFRNC_NO)), _
                                                                              PoRefNo, _
                                                                              OnHireDate, _
                                                                              OffHireDate, _
                                                                              dblOtherCharge, _
                                                                              Remarks, _
                                                                              RentalRefNo, _
                                                                              strGI_Trans_No, _
                                                                              bv_intDepotID, _
                                                                              bv_strModifiedBy, _
                                                                              bv_datModifiedDate, _
                                                                              bv_strModifiedBy, _
                                                                              bv_datModifiedDate, _
                                                                              AdditionalRate, _
                                                                              False, _
                                                                              objTrans)

                                If dtActivityStatus.Rows.Count > 0 Then
                                    If Not (dtActivityStatus.Rows(0).Item(RentalEntryData.YRD_LCTN) Is DBNull.Value) Then
                                        strYardLocation = CStr(dtActivityStatus.Rows(0).Item(RentalEntryData.YRD_LCTN))
                                    Else
                                        strYardLocation = Nothing
                                    End If
                                    If Not (dtActivityStatus.Rows(0).Item(RentalEntryData.GI_TRNSCTN_NO) Is DBNull.Value) Then
                                        strGI_Trans_No = CStr(dtActivityStatus.Rows(0).Item(RentalEntryData.GI_TRNSCTN_NO))
                                    Else
                                        strGI_Trans_No = Nothing

                                    End If
                                    cstmr_id = CInt(dtActivityStatus.Rows(0).Item(RentalEntryData.CSTMR_ID))
                                Else
                                    strYardLocation = Nothing
                                    strGI_Trans_No = RentalRefNo
                                    cstmr_id = CInt(drRental.Item(RentalEntryData.CSTMR_ID))

                                End If

                                'If Not (br_dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE)) Is Nothing Then
                                For Each drOtherCharge As DataRow In br_dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Select(RentalEntryData.RNTL_ENTRY_ID & "='" & drRental.Item(RentalEntryData.RNTL_ENTRY_ID).ToString & "'")
                                    If Not (drOtherCharge.Item(RentalEntryData.RT_NC) Is DBNull.Value) Then
                                        dblRate = CDbl(drOtherCharge.Item(RentalEntryData.RT_NC))
                                    Else
                                        dblRate = 0.0
                                    End If
                                    Dim lngCreatedOtherCharge As Long = objRental.CreateRentalOtherCharge(lngCreated, _
                                                                            CInt(drOtherCharge.Item(RentalEntryData.ADDTNL_CHRG_RT_ID)), _
                                                                            dblRate, _
                                                                            objTrans)
                                Next
                            Else
                                If br_strRentalExist.Length > 0 Then
                                    br_strRentalExist = String.Concat(br_strRentalExist, ",")
                                End If
                                br_strRentalExist = String.Concat(br_strRentalExist, drRental.Item(RentalEntryData.EQPMNT_NO))
                            End If
                        Case DataRowState.Modified
                            objRental.UpdateRentalEntry(CInt(drRental.Item(RentalEntryData.RNTL_ENTRY_ID)), _
                                                        (drRental.Item(RentalEntryData.EQPMNT_NO).ToString), _
                                                        CLng(drRental.Item(RentalEntryData.CSTMR_ID)), _
                                                        CStr(drRental.Item(RentalEntryData.CNTRCT_RFRNC_NO)), _
                                                        PoRefNo, _
                                                        OnHireDate, _
                                                        OffHireDate, _
                                                        dblOtherCharge, _
                                                        Remarks, _
                                                        CStr(drRental.Item(RentalEntryData.RNTL_RFRNC_NO)), _
                                                        bv_intDepotID, _
                                                        bv_strModifiedBy, _
                                                        bv_datModifiedDate, _
                                                        AdditionalRate, _
                                                        objTrans)

                            'If Not IsDBNull(drRental.Item(RentalEntryData.OFF_HR_DT)) Then
                            '    Dim objCommonUI As New CommonUIs
                            '    Dim strQuery As String = String.Empty
                            '    strQuery = String.Concat("UPDATE ", GateinData._GATEIN, " SET ", GateinData.GTN_DT, "='", CDate(OffHireDate).ToString("dd-MMM-yyyy"), "',", _
                            '                             GateinData.MDFD_BY, "='", bv_strModifiedBy, "',", GateinData.MDFD_DT, "='", bv_datModifiedDate, _
                            '                             "' WHERE ", GateinData.EQPMNT_NO, "='", CStr(drRental.Item(RentalEntryData.EQPMNT_NO)), "' AND ", _
                            '                             GateinData.CSTMR_ID, "=", CLng(drRental.Item(RentalEntryData.CSTMR_ID)), " AND ", _
                            '                             GateinData.RNTL_RFRNC_NO, "='", CStr(drRental.Item(RentalEntryData.RNTL_RFRNC_NO)), "' AND ", _
                            '                             GateinData.RNTL_BT, "=1", " AND ", GateinData.DPT_ID, "=", bv_intDepotID)

                            '    objCommonUIs.UpdateTable(strQuery, objTrans)

                            '    strQuery = String.Empty

                            '    strQuery = String.Concat("UPDATE ", GateinData._TRACKING, " SET ", GateinData.ACTVTY_DT, "='", CDate(OffHireDate).ToString("dd-MMM-yyyy"), "',", _
                            '                             GateinData.MDFD_BY, "='", bv_strModifiedBy, "',", GateinData.MDFD_DT, "='", bv_datModifiedDate, _
                            '                             "' WHERE ", GateinData.EQPMNT_NO, "='", CStr(drRental.Item(RentalEntryData.EQPMNT_NO)), "' AND ", _
                            '                             GateinData.CSTMR_ID, "=", CLng(drRental.Item(RentalEntryData.CSTMR_ID)), " AND ", _
                            '                             GateinData.RNTL_RFRNC_NO, "='", CStr(drRental.Item(RentalEntryData.RNTL_RFRNC_NO)), "' AND ", _
                            '                             GateinData.DPT_ID, "=", bv_intDepotID, " AND ", _
                            '                             GateinData.CNCLD_BY, " IS NULL AND ", GateinData.EQPMNT_STTS_ID, "=1")

                            '    objCommonUIs.UpdateTable(strQuery, objTrans)

                            '    strQuery = String.Empty

                            '    strQuery = String.Concat("UPDATE ", GateinData._ACTIVITY_STATUS, " SET ", GateinData.ACTVTY_DT, "='", CDate(OffHireDate).ToString("dd-MMM-yyyy"), "',", _
                            '                             GateinData.GTN_DT, "='", CDate(OffHireDate).ToString("dd-MMM-yyyy"), "' WHERE ", _
                            '                             GateinData.EQPMNT_NO, "='", CStr(drRental.Item(RentalEntryData.EQPMNT_NO)), "' AND ", _
                            '                             GateinData.CSTMR_ID, "=", CLng(drRental.Item(RentalEntryData.CSTMR_ID)), " AND ", _
                            '                             GateinData.GI_TRNSCTN_NO, "=( SELECT ", GateinData.GI_TRNSCTN_NO, " FROM ", GateinData._GATEIN, _
                            '                             " WHERE ", GateinData.EQPMNT_NO, "='", CStr(drRental.Item(RentalEntryData.EQPMNT_NO)), "' AND ", _
                            '                             GateinData.CSTMR_ID, "=", CLng(drRental.Item(RentalEntryData.CSTMR_ID)), " AND ", _
                            '                             GateinData.RNTL_RFRNC_NO, "='", CStr(drRental.Item(RentalEntryData.RNTL_RFRNC_NO)), "' AND ", _
                            '                             GateinData.RNTL_BT, "=1", " AND ", GateinData.DPT_ID, "=", bv_intDepotID, ") AND ", _
                            '                             GateinData.DPT_ID, "=", bv_intDepotID, " AND ", _
                            '                             GateinData.ACTV_BT, "=1 AND ", GateinData.EQPMNT_STTS_ID, "=1")

                            '    objCommonUIs.UpdateTable(strQuery, objTrans)
                            'End If

                            objRental.UpdateRentalCharge(drRental.Item(RentalEntryData.EQPMNT_NO).ToString, _
                                                         drRental.Item(RentalEntryData.RNTL_RFRNC_NO).ToString, _
                                                         AdditionalRate, _
                                                         OnHireDate, _
                                                         OffHireDate, _
                                                         objTrans)

                            If Not (br_dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE)) Is Nothing Then

                                For Each drOtherCharge As DataRow In br_dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Select(RentalEntryData.RNTL_ENTRY_ID & "='" & drRental.Item(RentalEntryData.RNTL_ENTRY_ID).ToString & "'")
                                    If drOtherCharge.RowState = DataRowState.Added Or drOtherCharge.RowState = DataRowState.Modified Then
                                        If Not (drOtherCharge.Item(RentalEntryData.RT_NC) Is DBNull.Value) Then
                                            dblRate = CDbl(drOtherCharge.Item(RentalEntryData.RT_NC))
                                        Else
                                            dblRate = 0.0
                                        End If
                                    End If

                                    Select Case drOtherCharge.RowState
                                        Case DataRowState.Added
                                            objRental.CreateRentalOtherCharge(CInt(drRental.Item(RentalEntryData.RNTL_ENTRY_ID)), _
                                                                           CInt(drOtherCharge.Item(RentalEntryData.ADDTNL_CHRG_RT_ID)), _
                                                                           dblRate, _
                                                                           objTrans)
                                        Case DataRowState.Modified
                                            objRental.UpdateRentalOtherCharge(CInt(drOtherCharge.Item(RentalEntryData.RNTL_OTHR_CHRG_ID)), _
                                                                           CInt(drRental.Item(RentalEntryData.RNTL_ENTRY_ID)), _
                                                                           CInt(drOtherCharge.Item(RentalEntryData.ADDTNL_CHRG_RT_ID)), _
                                                                          dblRate, _
                                                                           objTrans)
                                        Case DataRowState.Deleted
                                            objRental.DeleteRentalOtherCharge(CInt(drOtherCharge.Item(RentalEntryData.RNTL_OTHR_CHRG_ID, DataRowVersion.Original)), objTrans)

                                    End Select
                                Next
                                For Each drOtherCharge As DataRow In br_dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows
                                    Select Case drOtherCharge.RowState
                                        Case DataRowState.Deleted
                                            objRental.DeleteRentalOtherCharge(CInt(drOtherCharge.Item(RentalEntryData.RNTL_OTHR_CHRG_ID, DataRowVersion.Original)), objTrans)
                                    End Select
                                Next
                            End If
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

#Region "pvt_calculateTotalOtherCharge"
    Private Sub pvt_calculateTotalOtherCharge(ByVal bv_i64RentalId As Int64, _
                                              ByRef br_decTotalOtherCharge As Decimal, _
                                              ByRef br_dsRentalEntry As RentalEntryDataSet)
        Try

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Sub
#End Region

#Region "getSupplierEquipment() "
    Public Function getSupplierEquipment(ByVal bv_DeoptID As Integer) As RentalEntryDataSet
        Try
            Dim dsRental As RentalEntryDataSet
            Dim objRentalEntrys As New RentalEntrys
            dsRental = objRentalEntrys.getSupplierEquipment(bv_DeoptID)
            Return dsRental
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetAdditionalChargeRateByDepotId()"
    <OperationContract()> _
    Public Function pub_GetAdditionalChargeRateByDepotId(ByVal bv_i32DepotId As Int32) As RentalEntryDataSet
        Try
            Dim dsREntalData As RentalEntryDataSet
            Dim objREntal As New RentalEntrys
            dsREntalData = objREntal.GetAdditionalChargeRateByDepotId(bv_i32DepotId)
            Return dsREntalData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "getDefaultRates() "
    Public Function getDefaultRates(ByVal bv_DeoptID As Integer) As RentalEntryDataSet
        Try
            Dim dsRental As RentalEntryDataSet
            Dim objRentalEntrys As New RentalEntrys
            dsRental = objRentalEntrys.getDefaultRates(bv_DeoptID)
            Return dsRental
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "DeleteRental()"
    <OperationContract()> _
    Public Function DeleteRental(ByRef br_dsRental As RentalEntryDataSet, _
                                 ByVal bv_strModifiedBy As String, _
                                 ByVal bv_datModifiedDate As DateTime, _
                                 ByVal bv_intDepotID As Integer) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objRental As New RentalEntrys
            Dim dtRental As DataTable
            dtRental = br_dsRental.Tables(RentalEntryData._V_RENTAL_ENTRY)
            For Each drRental As DataRow In dtRental.Select(RentalEntryData.CHECKED & "='True'")
                objRental.DeleteRentalAdditionalCharge(CommonUIs.iLng(drRental.Item(RentalEntryData.RNTL_ENTRY_ID, DataRowVersion.Original)), objTrans)
                objRental.DeleteRentalEntry(CommonUIs.iLng(drRental.Item(RentalEntryData.RNTL_ENTRY_ID, DataRowVersion.Original)), objTrans)
            Next
            objTrans.commit()
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function
#End Region

#Region "pub_GetAdditionalChargeRateById() Table Name: Additional_Charge_Rate"
    <OperationContract()> _
    Public Function pub_GetAdditionalChargeRateById(ByVal bv_i64AdditionalChargeRateId As Int64, _
                                                    ByVal bv_i32DepotId As Int32) As Double
        Try
            Dim objRental As New RentalEntrys
            Dim dblChargeRate As Double = 0
            dblChargeRate = objRental.GetAdditionalChargeRateById(bv_i64AdditionalChargeRateId, bv_i32DepotId)
            Return dblChargeRate
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetOffHireDate() "
    <OperationContract()> _
    Public Function GetOffHireDate(ByVal bv_RentalId As Integer, ByVal strEquipmentNo As String) As RentalEntryDataSet
        Try
            Dim dsRental As RentalEntryDataSet
            Dim objRentalEntrys As New RentalEntrys
            dsRental = objRentalEntrys.GetOffHireDate(bv_RentalId, strEquipmentNo)
            Return dsRental
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRentalOtherCharges"
    Public Function pub_GetRentalOtherCharges() As RentalEntryDataSet
        Try
            Dim dsREntalData As RentalEntryDataSet
            Dim objREntal As New RentalEntrys
            dsREntalData = objREntal.GetRentalOtherChargesByDepotId()
            Return dsREntalData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
