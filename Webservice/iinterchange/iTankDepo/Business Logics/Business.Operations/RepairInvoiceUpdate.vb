Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.Text

<ServiceContract()> _
Public Class RepairInvoiceUpdate

#Region "GetRepairChargevalues()"
    <OperationContract()> _
    Public Function GetRepairChargevalues(ByVal bv_DeoptID As Integer) As RepairInvoiceUpdateDataSet
        Try
            Dim dsRepairInvoiceDataSet As RepairInvoiceUpdateDataSet
            Dim objRepairInvoices As New RepairInvoiceUpdates
            dsRepairInvoiceDataSet = objRepairInvoices.GetRepairChargevalues(bv_DeoptID)
            Return dsRepairInvoiceDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateRepairCharge()"
    <OperationContract()> _
    Public Function pub_UpdateRepairCharge(ByRef br_dsRepairDataset As RepairInvoiceUpdateDataSet, _
                                     ByVal bv_strModifiedBy As String, _
                                     ByVal bv_datModifiedDate As DateTime, _
                                     ByVal bv_strReason As String, _
                                     ByVal bv_intDepotID As Integer, _
                                     ByRef br_strActivitySubmit As String, _
                                     ByVal bv_intActivityId As Integer) As Boolean
        Dim objTrans As New Transactions
        Dim objRepairs As New RepairInvoiceUpdates
        Dim dtRepair As New DataTable
        Dim sbrOldValue As New StringBuilder
        Dim sbrNewValue As New StringBuilder
        Dim objCommonUIs As New CommonUIs
        Dim lngCreated As Long
        Try
            For Each drRepair As DataRow In br_dsRepairDataset.Tables(RepairInvoiceUpdateData._V_REPAIR_CHARGE).Select(RepairInvoiceUpdateData.CHECKED & "='True'")
                Dim strRefNo As String = ""
                Dim strPartyRefNo As String = String.Empty
                Dim strRepairEstimateRefNo As String = ""
                Dim blnParty As Boolean = False
                Dim decAmount As Decimal = 0
                Dim intInvoicingParty As Integer = 0
                If Not (drRepair.Item(RepairInvoiceUpdateData.APPRVL_RF_NO) Is DBNull.Value) Then
                    strRefNo = CStr(drRepair.Item(RepairInvoiceUpdateData.APPRVL_RF_NO))
                End If
                If Not (drRepair.Item(RepairInvoiceUpdateData.INVCNG_PRTY_ID) Is DBNull.Value) Then
                    blnParty = True
                End If

                If Not (drRepair.Item(RepairInvoiceUpdateData.RPR_APPRVL_AMNT_NC) Is DBNull.Value) Then
                    decAmount = CDec(drRepair.Item(RepairInvoiceUpdateData.RPR_APPRVL_AMNT_NC))
                Else
                    decAmount = 0
                End If
                If Not ((drRepair.Item(RepairInvoiceUpdateData.INVCNG_PRTY_ID)) Is DBNull.Value) AndAlso Not ((drRepair.Item(RepairInvoiceUpdateData.INVCNG_PRTY_CD)) Is DBNull.Value) Then
                    intInvoicingParty = CInt(drRepair.Item(RepairInvoiceUpdateData.INVCNG_PRTY_ID))
                Else
                    intInvoicingParty = 0
                End If
                dtRepair.Rows.Clear()
                dtRepair = objRepairs.GetRepairEstimateDetails(CInt(drRepair.Item(RepairInvoiceUpdateData.RPR_CHRG_ID)), _
                                                             CStr(drRepair.Item(RepairInvoiceUpdateData.EQPMNT_NO)), _
                                                             drRepair.Item(RepairInvoiceUpdateData.GI_TRNSCTN_NO).ToString, _
                                                             drRepair.Item(RepairInvoiceUpdateData.ESTMT_NO).ToString, _
                                                             bv_intDepotID, _
                                                             objTrans).Tables(RepairInvoiceUpdateData._V_REPAIR_ESTIMATE)
                bv_intDepotID = CInt(drRepair.Item(RepairInvoiceUpdateData.DPT_ID))

                objRepairs.UpdateRepairCharge(CInt(drRepair.Item(RepairInvoiceUpdateData.RPR_CHRG_ID)), _
                                              CStr(drRepair.Item(RepairInvoiceUpdateData.EQPMNT_NO)), _
                                              drRepair.Item(RepairInvoiceUpdateData.GI_TRNSCTN_NO).ToString, _
                                              strRefNo, _
                                              decAmount, _
                                              intInvoicingParty, _
                                              bv_strModifiedBy, _
                                              bv_datModifiedDate, _
                                              bv_intDepotID, _
                                              objTrans)
                sbrNewValue.Clear()
                sbrOldValue.Clear()

                If Not IsDBNull(dtRepair.Rows(0).Item(RepairInvoiceUpdateData.OWNR_APPRVL_RF).ToString) Then
                    strRepairEstimateRefNo = CStr(dtRepair.Rows(0).Item(RepairInvoiceUpdateData.OWNR_APPRVL_RF).ToString)
                End If

                If Not String.Compare(strRefNo, strRepairEstimateRefNo) = 0 Then
                    If strRefNo <> "" Then
                        sbrNewValue.Append("Approval Reference No :")
                        sbrNewValue.Append(strRefNo)
                    End If
                    If Not IsDBNull(dtRepair.Rows(0).Item(RepairInvoiceUpdateData.OWNR_APPRVL_RF)) Then
                        If Not strRefNo Is Nothing Then
                            sbrOldValue.Append("Approval Reference No :")
                            sbrOldValue.Append(dtRepair.Rows(0).Item(RepairInvoiceUpdateData.OWNR_APPRVL_RF).ToString)
                        End If
                    End If
                End If
                If Not String.Compare(CStr(decAmount), CStr(CommonUIs.iDbl(dtRepair.Rows(0).Item(RepairInvoiceUpdateData.APPRVL_AMNT_NC)))) = 0 Then
                    If decAmount <> Nothing Then
                        If sbrNewValue.Length > 0 Then
                            sbrNewValue.Append(", ")
                        End If
                        If sbrOldValue.Length > 0 Then
                            sbrOldValue.Append(", ")
                        End If
                        sbrNewValue.Append("Amount :")
                        sbrNewValue.Append(decAmount)
                        sbrOldValue.Append("Amount :")
                        sbrOldValue.Append(dtRepair.Rows(0).Item(RepairInvoiceUpdateData.APPRVL_AMNT_NC))
                    End If
                End If

                If Not String.Compare(drRepair.Item(RepairInvoiceUpdateData.INVCNG_PRTY_CD).ToString, (dtRepair.Rows(0).Item(RepairInvoiceUpdateData.INVCNG_PRTY_CD).ToString)) = 0 Then
                    If sbrNewValue.Length > 0 Then
                        sbrNewValue.Append(", ")
                    End If
                    If sbrOldValue.Length > 0 Then
                        sbrOldValue.Append(", ")
                    End If
                    sbrNewValue.Append("Invoicing Party :")
                    sbrNewValue.Append(drRepair.Item(RepairInvoiceUpdateData.INVCNG_PRTY_CD).ToString)
                    sbrOldValue.Append("Invoicing Party :")
                    sbrOldValue.Append(dtRepair.Rows(0).Item(RepairInvoiceUpdateData.INVCNG_PRTY_CD))
                    'End If
                End If


                If Not decAmount <> Nothing Or strRefNo <> "" Or Not intInvoicingParty <> Nothing Then
                    If sbrNewValue.Length > 0 Then
                        lngCreated = objCommonUIs.CreateAuditLog(CStr(drRepair.Item(RepairInvoiceUpdateData.EQPMNT_NO)), _
                                                                          drRepair.Item(RepairInvoiceUpdateData.ESTMT_NO).ToString, _
                                                                         "Repair Invoice Update", _
                                                                         "Update", _
                                                                         CDate(Now), _
                                                                         sbrOldValue.ToString(), _
                                                                         sbrNewValue.ToString(), _
                                                                         bv_strReason, _
                                                                         bv_strModifiedBy, _
                                                                         bv_intDepotID, _
                                                                         objTrans)
                    End If
                End If
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

#Region "Pub_GetActivitySubmit()"
    <OperationContract()> _
    Public Sub Pub_GetActivitySubmit(ByRef br_strActivitySubmit As String, _
                                     ByVal bv_intActivityId As Integer, _
                                     ByVal bv_dsRepairInvoiceUpdate As RepairInvoiceUpdateDataSet)
        Try
            Dim objCommonUIs As New CommonUIs
            For Each drRepair As DataRow In bv_dsRepairInvoiceUpdate.Tables(RepairInvoiceUpdateData._V_REPAIR_CHARGE).Select(RepairInvoiceUpdateData.CHECKED & "='True'")
                Dim blnActivitySubmit As Boolean = False
                blnActivitySubmit = objCommonUIs.GetActivitySubmit(bv_intActivityId, drRepair, False)
                If blnActivitySubmit Then
                    If br_strActivitySubmit.Length > 0 Then
                        br_strActivitySubmit = String.Concat(br_strActivitySubmit, ",")
                    End If
                    br_strActivitySubmit = String.Concat(br_strActivitySubmit, drRepair.Item(RepairInvoiceUpdateData.EQPMNT_NO))
                End If
            Next
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Sub
#End Region

#Region "GetRepairChargevaluesAllDepots()"
    <OperationContract()> _
    Public Function GetRepairChargevaluesAllDepots() As RepairInvoiceUpdateDataSet
        Try
            Dim dsRepairInvoiceDataSet As RepairInvoiceUpdateDataSet
            Dim objRepairInvoices As New RepairInvoiceUpdates
            dsRepairInvoiceDataSet = objRepairInvoices.GetRepairChargevaluesAllDepots()
            Return dsRepairInvoiceDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class
