
Partial Class Transportation_TransportationMassApplyInput
    Inherits Pagebase

#Region "Declarations.."
    Dim dsTransportation As New TransportationDataSet
    Private Const TRANSPORTATION As String = "TRANSPORTATION"
    Private Const TRANSPORTATION_MASS_APPLY As String = "TRANSPORTATION_MASS_APPLY"
    Private dsTransportationMassApply As New TransportationDataSet
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "massApplyInput"
                    pvt_MassApplyInput(e.GetCallbackValue("EquipmentTypeId"), _
                                       e.GetCallbackValue("EquipmentTypeCode"), _
                                       e.GetCallbackValue("CustomerRefNo"), _
                                       e.GetCallbackValue("JobStartDate"), _
                                       e.GetCallbackValue("JobEndDate"), _
                                       e.GetCallbackValue("PreviousCargoId"), _
                                       e.GetCallbackValue("PreviousCargoCode"))
                    ', _e.GetCallbackValue("Remarks")
                Case "getChargeRate"
                    pvt_getClientChargeRate(e.GetCallbackValue("AdditionalChargeRateId"))

            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_getClientChargeRate"
    Private Sub pvt_getClientChargeRate(ByVal bv_AdditionalChargeRateId As String)
        Try
            Dim dblChargeRate As Double = 0
            Dim objTransportation As New Transportation
            Dim objCommonData As New CommonData
            Dim intDepotId As Int32 = objCommonData.GetDepotID()
            dblChargeRate = objTransportation.pub_GetAdditionalChargeRateById(bv_AdditionalChargeRateId, intDepotId)
            pub_SetCallbackReturnValue("ChargeRate", dblChargeRate)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Transportation/TransportationMassApplyInput.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                hdnEmptyRate.Value = RetrieveData("EmptyRate")
                hdnFullRate.Value = RetrieveData("FullRate")
                pvt_SetChangeMade()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangeMade"
    Private Sub pvt_SetChangeMade()
        Try
            CommonWeb.pub_AttachHasChanges(lkpEqpType)
            CommonWeb.pub_AttachHasChanges(txtCustomerRefNo)
            CommonWeb.pub_AttachHasChanges(datJobStartDate)
            CommonWeb.pub_AttachHasChanges(datJobEndDate)
            'CommonWeb.pub_AttachHasChanges(txtMassRemarks)
            pub_SetGridChanges(ifgMassApplyInput, "tabTransportationMassApplyInput")
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgMassApplyInput_ClientBind"
    Protected Sub ifgMassApplyInput_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgMassApplyInput.ClientBind
        Try
            Dim dtAdditionalCharge As New DataTable
            Dim objCommonData As New CommonData
            Dim objTransportation As New Transportation
            Dim drTransportationDetailRate As DataRow = Nothing
            Dim i64TransportationId As Int64
            Dim i64TransportationDetailId As Int64

            If Not e.Parameters("TransportationId") Is Nothing Then
                i64TransportationId = CLng(e.Parameters("TransportationId"))
            End If
            If Not e.Parameters("TransportationDetailId") Is Nothing Then
                i64TransportationDetailId = CLng(e.Parameters("TransportationDetailId"))
            End If
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            Dim intDepotId As Int32 = objCommonData.GetDepotID()
            dtAdditionalCharge = objTransportation.pub_GetAdditionalChargeRateByDepotId(intDepotId).Tables(TransportationData._V_ADDITIONAL_CHARGE_RATE)
            Dim dtCurrentData As DataTable = dsTransportationMassApply.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Clone()
            If dtAdditionalCharge.Rows.Count > 0 Then
                For Each drAdditional As DataRow In dtAdditionalCharge.Rows
                    drTransportationDetailRate = dtCurrentData.NewRow()
                    drTransportationDetailRate.Item(TransportationData.TRNSPRTTN_DTL_RT_ID) = CommonWeb.GetNextIndex(dtCurrentData, TransportationData.TRNSPRTTN_DTL_RT_ID)
                    drTransportationDetailRate.Item(TransportationData.TRNSPRTTN_ID) = i64TransportationId
                    drTransportationDetailRate.Item(TransportationData.TRNSPRTTN_DTL_ID) = i64TransportationDetailId
                    drTransportationDetailRate.Item(TransportationData.ADDTNL_CHRG_RT_ID) = drAdditional.Item(TransportationData.ADDTNL_CHRG_RT_ID)
                    drTransportationDetailRate.Item(TransportationData.ADDTNL_CHRG_RT_CD) = drAdditional.Item(TransportationData.ADDTNL_CHRG_RT_CD)
                    drTransportationDetailRate.Item(TransportationData.ADDTNL_CHRG_RT_NC) = drAdditional.Item(TransportationData.RT_NC)
                    drTransportationDetailRate.Item(TransportationData.DFLT_BT) = drAdditional.Item(TransportationData.DFLT_BT)
                    dtCurrentData.Rows.Add(drTransportationDetailRate)
                Next
                dsTransportationMassApply.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(dtCurrentData)
            End If
            e.DataSource = dtCurrentData
            dtCurrentData.AcceptChanges()
            dsTransportationMassApply.AcceptChanges()
            CacheData(TRANSPORTATION_MASS_APPLY, dsTransportationMassApply)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgMassApplyInput_RowDataBound"
    Protected Sub ifgMassApplyInput_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgMassApplyInput.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowIndex > 1 Then
                    Dim lkpControl As iLookup
                    lkpControl = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpControl.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgMassApplyInput_RowInserting"
    Protected Sub ifgMassApplyInput_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgMassApplyInput.RowInserting
        Try
            Dim lngID As Long
            Dim dtTransportation As New DataTable
            dsTransportationMassApply = CType(RetrieveData(TRANSPORTATION_MASS_APPLY), TransportationDataSet)
            dtTransportation = dsTransportationMassApply.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE)
            If dtTransportation.Rows.Count = 0 AndAlso CType(ifgMassApplyInput.DataSource, DataTable).Rows.Count = 0 Then
                lngID = CommonWeb.GetNextIndex(dtTransportation, TransportationData.TRNSPRTTN_DTL_RT_ID)
            ElseIf dtTransportation.Rows.Count = 0 AndAlso CType(ifgMassApplyInput.DataSource, DataTable).Rows.Count > 0 Then
                lngID = CType(ifgMassApplyInput.DataSource, DataTable).Rows.Count
                lngID = lngID + CType(ifgMassApplyInput.DataSource, DataTable).Rows.Count
            Else
                lngID = CommonWeb.GetNextIndex(dtTransportation, TransportationData.TRNSPRTTN_DTL_RT_ID)
                lngID = lngID + CType(ifgMassApplyInput.DataSource, DataTable).Rows.Count
            End If
            e.Values(TransportationData.TRNSPRTTN_DTL_RT_ID) = lngID
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgMassApplyInput_RowDeleting"
    Protected Sub ifgMassApplyInput_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgMassApplyInput.RowDeleting
        Try
            CacheData("TransportationDetailId", CLng(e.InputParamters("TransportationDetailId")))
            Dim dtTransportation As New DataTable
            'Can not Delete Default Value         
            dsTransportation = CType(RetrieveData(TRANSPORTATION_MASS_APPLY), TransportationDataSet)
            dtTransportation = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Copy()
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgMassApplyInput.PageSize * ifgMassApplyInput.PageIndex + e.RowIndex

            Dim drChkDefault As DataRow()
            drChkDefault = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Select(String.Concat(TransportationData.ADDTNL_CHRG_RT_ID, "=", e.Values.Item(TransportationData.ADDTNL_CHRG_RT_ID), " AND ", TransportationData.DFLT_BT, "=True"))
            If drChkDefault.Length > 0 Then
                e.Cancel = True
                e.OutputParamters.Add("CheckDefault", String.Concat("Additional Charge Rate : Default value ", dtTransportation.Rows(intRowIndex).Item(TransportationData.ADDTNL_CHRG_RT_CD).ToString, " cannot be deleted"))
            Else
                ' If CType(ifgMassApplyInput.DataSource, DataTable).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_RT_ID, " = ") & e.Keys(0))(0).RowState <> DataRowState.Added Then
                dtTransportation = ifgMassApplyInput.DataSource
                e.OutputParamters("Delete") = String.Concat("Additional Charge Rate: ", dtTransportation.Select(String.Concat(TransportationData.TRNSPRTTN_DTL_RT_ID, " = ") & e.Keys(0))(0).Item(TransportationData.ADDTNL_CHRG_RT_CD).ToString, " has been deleted. Click submit to save changes.")
                'e.OutputParamters("Delete") = String.Concat("Additional Charge Rate : ", dtTransportation.Rows(intRowIndex).Item(TransportationData.ADDTNL_CHRG_RT_CD).ToString, " has been deleted from Product. Click submit to save changes.")
                'End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_MassApplyInput"
    Private Sub pvt_MassApplyInput(ByVal bv_strEquipmentTypeId As String, _
                                   ByVal bv_strEquipmentTypeCode As String, _
                                   ByVal bv_strCustomerRefNo As String, _
                                   ByVal bv_strJobStartDate As String, _
                                   ByVal bv_strJobEndDate As String, _
                                   ByVal bv_strPreviousCargoId As String, _
                                   ByVal bv_strPreviousCargoCode As String)
        ', _ByVal bv_strRemarks As String
        Try
            Dim dtTransportation As New DataTable
            Dim drTransportation As DataRow
            Dim objTransportation As New Transportation
            Dim dtTransportationMassApply As New DataTable
            dtTransportationMassApply = CType(ifgMassApplyInput.DataSource, DataTable)
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            dsTransportationMassApply = CType(RetrieveData(TRANSPORTATION_MASS_APPLY), TransportationDataSet)
            dtTransportation = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Clone()
            For Each drTrasnportationDetail As DataRow In dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Select(String.Concat(TransportationData.CHK_BT, " ='True'"))
                For Each drTransportationRateDetail As DataRow In dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", drTrasnportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)))
                    If Not dtTransportationMassApply Is Nothing Then
                        If dtTransportationMassApply.Rows.Count > 0 Then
                            drTransportationRateDetail.Delete()
                        End If
                    End If
                Next

                '   For Each drMassApply As DataRow In dsTransportationMassApply.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows
                For Each drMassApply As DataRow In dtTransportationMassApply.Rows
                    If drMassApply.RowState <> DataRowState.Deleted Then
                        drTransportation = dtTransportation.NewRow()
                        drTransportation.Item(TransportationData.TRNSPRTTN_DTL_RT_ID) = CommonWeb.GetNextIndex(dtTransportation, TransportationData.TRNSPRTTN_DTL_RT_ID)
                        drTransportation.Item(TransportationData.TRNSPRTTN_DTL_ID) = drTrasnportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)
                        drTransportation.Item(TransportationData.TRNSPRTTN_ID) = drTrasnportationDetail.Item(TransportationData.TRNSPRTTN_ID)
                        drTransportation.Item(TransportationData.ADDTNL_CHRG_RT_ID) = drMassApply.Item(TransportationData.ADDTNL_CHRG_RT_ID)
                        drTransportation.Item(TransportationData.ADDTNL_CHRG_RT_CD) = drMassApply.Item(TransportationData.ADDTNL_CHRG_RT_CD)
                        drTransportation.Item(TransportationData.ADDTNL_CHRG_RT_NC) = drMassApply.Item(TransportationData.ADDTNL_CHRG_RT_NC)
                        dtTransportation.Rows.Add(drTransportation)
                    End If
                Next
                Dim drTripRate As DataRow()
                Dim decTripRate As Decimal = 0
                Dim decAddiotnalRate As Decimal = 0
                drTripRate = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, " = ", drTrasnportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID)))

                If Not dtTransportation.Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, " = ", drTrasnportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID))).ToString = "" Then
                    decAddiotnalRate = CDec(dtTransportation.Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, " = ", drTrasnportationDetail.Item(TransportationData.TRNSPRTTN_DTL_ID))))
                    decAddiotnalRate = decAddiotnalRate
                    drTrasnportationDetail.Item(TransportationData.TTL_RT_NC) = decAddiotnalRate
                End If
                If Not bv_strEquipmentTypeId Is Nothing Then
                    drTrasnportationDetail.Item(TransportationData.EQPMNT_TYP_ID) = bv_strEquipmentTypeId
                    drTrasnportationDetail.Item(TransportationData.EQPMNT_TYP_CD) = bv_strEquipmentTypeCode
                End If
                If Not bv_strCustomerRefNo Is Nothing Then
                    drTrasnportationDetail.Item(TransportationData.CSTMR_RF_NO) = bv_strCustomerRefNo
                End If
                If Not bv_strJobStartDate Is Nothing Then
                    drTrasnportationDetail.Item(TransportationData.JB_STRT_DT) = bv_strJobStartDate
                End If
                If Not bv_strJobEndDate Is Nothing Then
                    drTrasnportationDetail.Item(TransportationData.JB_END_DT) = bv_strJobEndDate
                End If
                If Not bv_strPreviousCargoId Is Nothing Then
                    drTrasnportationDetail.Item(TransportationData.PRDCT_ID) = bv_strPreviousCargoId
                    drTrasnportationDetail.Item(TransportationData.PRDCT_DSCRPTN_VC) = bv_strPreviousCargoCode
                End If
                'If Not bv_strPrevoiusCargeDescription Is Nothing Then
                '    drTrasnportationDetail.Item(TransportationData.PRDCT_DSCRPTN_VC) = bv_strPrevoiusCargeDescription
                '    drTrasnportationDetail.Item(TransportationData.PRDCT_DSCRPTN_VC) = bv_strPrevoiusCargeDescription
                'End If

                'If Not bv_strRemarks Is Nothing Then
                '    drTrasnportationDetail.Item(TransportationData.RMRKS_VC) = bv_strRemarks
                'End If
            Next
            dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(dtTransportation)
            CacheData(TRANSPORTATION, dsTransportation)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class