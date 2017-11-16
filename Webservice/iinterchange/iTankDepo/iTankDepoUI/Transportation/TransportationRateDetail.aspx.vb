
Partial Class Transportation_TransportationRateDetail
    Inherits Pagebase

#Region "Declarations.."
    Dim dsTransportation As New TransportationDataSet
    Private Const TRANSPORTATION As String = "TRANSPORTATION"
    Private Const TRANSPORTATION_TEMP As String = "TRANSPORTATION_TEMP"
    Private Const TRANSPORTATION_DETAIL_RATE As String = "TRANSPORTATION_DETAIL_RATE"
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "saveTransportationRate"
                    pvt_saveTransportationRate(e.GetCallbackValue("TransportationDetailId"))
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
            dblChargeRate = objTransportation.pub_GetAdditionalChargeRateById(CLng(bv_AdditionalChargeRateId), intDepotId)
            pub_SetCallbackReturnValue("ChargeRate", dblChargeRate)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then

                
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Transportation/TransportationRateDetail.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_bindCharge"
    Private Sub pvt_bindCharge(ByVal bv_i64TransportationId As Int64, ByVal bv_i64TransportationDetailId As Int64)
        Try
            Dim dtTransportation As New DataTable
            Dim dtAdditionalCharge As New DataTable
            Dim objTransportation As New Transportation
            Dim objCommonData As New CommonData
            Dim intDepotId As Int32 = objCommonData.GetDepotID()
            Dim drCharge As DataRow() = Nothing
            Dim drBilling As DataRow() = Nothing
            Dim drTransportationDetailRate As DataRow = Nothing
            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Rows.Count > 0 Then
                drBilling = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, " = ", bv_i64TransportationDetailId))
                If drBilling.Length > 0 Then
                    CacheData("BillingFlag", drBilling(0).Item(TransportationData.BLLNG_FLG).ToString)
                Else
                    CacheData("BillingFlag", "")
                End If
            Else
                CacheData("BillingFlag", "")
            End If
            If Not dsTransportation Is Nothing Then
                dtTransportation = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE)
                drCharge = dtTransportation.Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", bv_i64TransportationDetailId))
                If drCharge.Length = 0 Then
                    drCharge = dtTransportation.Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", bv_i64TransportationDetailId), "", DataViewRowState.Deleted)
                    If drCharge.Length = 0 Then
                        dtAdditionalCharge = objTransportation.pub_GetAdditionalChargeRateByDepotId(intDepotId).Tables(TransportationData._V_ADDITIONAL_CHARGE_RATE)
                        drCharge = Nothing
                    End If
                End If
                If drCharge Is Nothing Then
                    If bv_i64TransportationDetailId > 0 Then
                        If dsTransportation Is Nothing Then
                            dsTransportation = objTransportation.pub_GetTransportationDetailRate(bv_i64TransportationId, bv_i64TransportationDetailId)
                            dtTransportation = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE)
                        Else
                            dtTransportation = objTransportation.pub_GetTransportationDetailRate(bv_i64TransportationId, bv_i64TransportationDetailId).Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE)
                            dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(dtTransportation)
                        End If
                    End If
                End If
            End If

            Dim drs() As DataRow = dtTransportation.Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", bv_i64TransportationDetailId))
            Dim dtCurrentData As DataTable = dtTransportation.Clone
            If drs.Length > 0 Then
                dtCurrentData = drs.CopyToDataTable()
            End If
            ' Dim lngNextIndex As Long

            'If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows.Count > 0 Then
            '    If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows.Count - 1).RowState = DataRowState.Deleted Then
            '        lngNextIndex = CLng(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows.Count - 1).Item(TransportationData.TRNSPRTTN_DTL_RT_ID, DataRowVersion.Original)) + 1
            '    Else
            '        lngNextIndex = CLng(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Rows.Count - 1).Item(TransportationData.TRNSPRTTN_DTL_RT_ID)) + 1
            '    End If
            'Else
            '    lngNextIndex = 1
            'End If

            'If dtCurrentData.Rows.Count <= 0 Then
            '    For Each drAdditional As DataRow In dtAdditionalCharge.Rows
            '        drTransportationDetailRate = dtCurrentData.NewRow()
            '        drTransportationDetailRate.Item(TransportationData.TRNSPRTTN_DTL_RT_ID) = lngNextIndex 'CommonWeb.GetNextIndex(dtCurrentData, TransportationData.TRNSPRTTN_DTL_RT_ID)
            '        drTransportationDetailRate.Item(TransportationData.TRNSPRTTN_ID) = bv_i64TransportationId
            '        drTransportationDetailRate.Item(TransportationData.TRNSPRTTN_DTL_ID) = bv_i64TransportationDetailId
            '        drTransportationDetailRate.Item(TransportationData.ADDTNL_CHRG_RT_ID) = drAdditional.Item(TransportationData.ADDTNL_CHRG_RT_ID)
            '        drTransportationDetailRate.Item(TransportationData.ADDTNL_CHRG_RT_CD) = drAdditional.Item(TransportationData.ADDTNL_CHRG_RT_CD)
            '        drTransportationDetailRate.Item(TransportationData.ADDTNL_CHRG_RT_NC) = drAdditional.Item(TransportationData.RT_NC)
            '        drTransportationDetailRate.Item(TransportationData.DFLT_BT) = drAdditional.Item(TransportationData.DFLT_BT)
            '        dtCurrentData.Rows.Add(drTransportationDetailRate)
            '        lngNextIndex = lngNextIndex + 1
            '    Next
            '    dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(dtCurrentData)
            '    dtCurrentData.AcceptChanges()

            '    'dsTransportation.AcceptChanges()
            'End If

            ifgTransportationDetailRate.DataSource = dtCurrentData
            ifgTransportationDetailRate.DataBind()
            CacheData(TRANSPORTATION_DETAIL_RATE, dsTransportation)
            hdnTransportationDetailId.Value = CStr(bv_i64TransportationDetailId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "ifgTransportationDetailRate_ClientBind"
    Protected Sub ifgTransportationDetailRate_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgTransportationDetailRate.ClientBind
        Try
            Dim decTotalAdditionalRate As Decimal = 0
            pvt_bindCharge(CLng(e.Parameters("TransportationId")), CLng(e.Parameters("TransportationDetailId")))
            If Not IsNothing(e.Parameters("BillFlag")) AndAlso (e.Parameters("BillFlag") = "Y") Then
                ifgTransportationDetailRate.AllowAdd = False
                ifgTransportationDetailRate.AllowDelete = False
                ifgTransportationDetailRate.AllowEdit = False
            Else                ifgTransportationDetailRate.AllowAdd = True
                ifgTransportationDetailRate.AllowDelete = True
                ifgTransportationDetailRate.AllowEdit = True
            End If
            'If Not RetrieveData("TransportationStatusId") Is Nothing Or Not RetrieveData("BillingFlag") Is Nothing Then
            '    If CLng(RetrieveData("TransportationStatusId")) = 90 Or RetrieveData("BillingFlag").ToString = "Y" Then
            '        ifgTransportationDetailRate.AllowAdd = False
            '        ifgTransportationDetailRate.AllowDelete = False
            '    End If
            'End If
            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, " = ", CLng(e.Parameters("TransportationDetailId")))) <> 0 Then
                decTotalAdditionalRate = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, " = ", CLng(e.Parameters("TransportationDetailId"))))
            End If
            e.OutputParameters.Add("TripRate", decTotalAdditionalRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTransportationDetailRate_RowDataBound"
    Protected Sub ifgTransportationDetailRate_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgTransportationDetailRate.RowDataBound
        Try
            Dim strDepotCurrency As String = String.Empty
            Dim objCommonData As New CommonData
            strDepotCurrency = objCommonData.GetDepotLocalCurrencyCode()
            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Rate (", strDepotCurrency, ")")
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                    If RetrieveData("BillingFlag").ToString = "Y" Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                    If CommonUIs.iBool(drv.Item(ProductData.DFLT_BT)) Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    Else
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                    End If
                    If e.Row.RowIndex > 2 Then
                        Dim lkpControl As iLookup
                        lkpControl = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                        lkpControl.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                    End If
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                          MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTransportationDetailRate_RowInserting"
    Protected Sub ifgTransportationDetailRate_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgTransportationDetailRate.RowInserting
        Try
            Dim lngID As Long
            Dim dtTransportation As New DataTable
            dsTransportation = CType(RetrieveData(TRANSPORTATION_DETAIL_RATE), TransportationDataSet)
            dtTransportation = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE)
            If dtTransportation.Rows.Count = 0 AndAlso CType(ifgTransportationDetailRate.DataSource, DataTable).Rows.Count = 0 Then
                lngID = CommonWeb.GetNextIndex(dtTransportation, TransportationData.TRNSPRTTN_DTL_RT_ID)
            ElseIf dtTransportation.Rows.Count = 0 AndAlso CType(ifgTransportationDetailRate.DataSource, DataTable).Rows.Count > 0 Then
                lngID = CType(ifgTransportationDetailRate.DataSource, DataTable).Rows.Count
                lngID = lngID + CType(ifgTransportationDetailRate.DataSource, DataTable).Rows.Count
            Else
                lngID = CommonWeb.GetNextIndex(dtTransportation, TransportationData.TRNSPRTTN_DTL_RT_ID)
                lngID = lngID + CType(ifgTransportationDetailRate.DataSource, DataTable).Rows.Count
            End If
            e.Values(TransportationData.TRNSPRTTN_DTL_RT_ID) = lngID
            e.Values(TransportationData.TRNSPRTTN_ID) = CLng(e.InputParamters("TransportationId"))
            e.Values(TransportationData.TRNSPRTTN_DTL_ID) = CLng(e.InputParamters("TransportationDetailId"))
            CacheData("TransportationDetailId", CLng(e.InputParamters("TransportationDetailId")))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTransportationDetailRate_RowInserted"
    Protected Sub ifgTransportationDetailRate_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgTransportationDetailRate.RowInserted
        Try
            Dim dblTripRate As Double = 0
            Dim lngTransportationDetailId As Int64 = RetrieveData("TransportationDetailId")
            dsTransportation = CType(RetrieveData(TRANSPORTATION_DETAIL_RATE), TransportationDataSet)
            dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(CType(ifgTransportationDetailRate.DataSource, DataTable))
            If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", lngTransportationDetailId)).ToString = "" Then
                dblTripRate = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", lngTransportationDetailId))
            End If
            e.OutputParamters.Add("TripRate", dblTripRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTransportationDetailRate_RowUpdating"
    Protected Sub ifgTransportationDetailRate_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgTransportationDetailRate.RowUpdating
        Try
            'Dim strCancel As String = String.Empty
            'If Not e.InputParamters("CancelBit") Is Nothing Then
            '    strCancel = e.InputParamters("CancelBit").ToString.ToLower
            'End If
            'If strCancel = "true" Then
            '    e.NewValues(TransportationData.ADDTNL_CHRG_RT_NC) = e.OldValues(TransportationData.ADDTNL_CHRG_RT_NC)
            'End If

            CacheData("TransportationDetailId", CLng(e.InputParamters("TransportationDetailId")))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTransportationDetailRate_RowUpdated"
    Protected Sub ifgTransportationDetailRate_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgTransportationDetailRate.RowUpdated
        Try
            Dim dblTripRate As Double = 0
            Dim lngTransportationDetailId As Int64 = RetrieveData("TransportationDetailId")
            dsTransportation = CType(RetrieveData(TRANSPORTATION_DETAIL_RATE), TransportationDataSet)
            dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(CType(ifgTransportationDetailRate.DataSource, DataTable))
            If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", lngTransportationDetailId)).ToString = "" Then
                dblTripRate = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", lngTransportationDetailId))
            End If
            e.OutputParamters.Add("TripRate", dblTripRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                          MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTransportationDetailRate_RowDeleting"
    Protected Sub ifgTransportationDetailRate_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgTransportationDetailRate.RowDeleting
        Try
            CacheData("TransportationDetailId", CLng(e.InputParamters("TransportationDetailId")))
            Dim dtTransportation As New DataTable
            'Can not Delete Default Value
            dsTransportation = CType(RetrieveData(TRANSPORTATION_DETAIL_RATE), TransportationDataSet)
            dtTransportation = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Copy()
            Dim drChkDefault As DataRow()
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgTransportationDetailRate.PageSize * ifgTransportationDetailRate.PageIndex + e.RowIndex
            drChkDefault = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Select(String.Concat(TransportationData.ADDTNL_CHRG_RT_ID, "=", e.Values.Item(TransportationData.ADDTNL_CHRG_RT_ID), " AND ", TransportationData.DFLT_BT, "=True"))
            If drChkDefault.Length > 0 Then
                e.Cancel = True
                e.OutputParamters.Add("CheckDefault", String.Concat("Additional Charge Rate: Default value ", dtTransportation.Rows(intRowIndex).Item(TransportationData.ADDTNL_CHRG_RT_CD).ToString, " cannot be deleted"))
            Else
                dtTransportation = ifgTransportationDetailRate.DataSource
                e.OutputParamters("Delete") = String.Concat("Additional Charge Rate:", dtTransportation.Select(String.Concat(TransportationData.TRNSPRTTN_DTL_RT_ID, " = ") & e.Keys(0))(0).Item(TransportationData.ADDTNL_CHRG_RT_CD).ToString, " has been deleted. Click submit to save changes.")
                ' e.OutputParamters("Delete") = String.Concat("Additional Charge Rate : ", dtTransportation.Rows(intRowIndex).Item(TransportationData.ADDTNL_CHRG_RT_CD).ToString, " has been deleted from Product. Click submit to save changes.")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                         MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgTransportationDetailRate_RowDeleted"
    Protected Sub ifgTransportationDetailRate_RowDeleted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeletedEventArgs) Handles ifgTransportationDetailRate.RowDeleted
        Try
            Dim dblTripRate As Double = 0

            Dim lngTransportationDetailId As Int64 = RetrieveData("TransportationDetailId")
            dsTransportation = CType(RetrieveData(TRANSPORTATION_DETAIL_RATE), TransportationDataSet)
            Dim dtTrans As New DataTable
            dtTrans.Merge(CType(ifgTransportationDetailRate.DataSource, DataTable))
            'dtTrans.Merge(CType(ifgTransportationDetailRate.DataSource, DataTable))
            dtTrans.AcceptChanges()
            'dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(CType(ifgTransportationDetailRate.DataSource, DataTable))
            If Not dtTrans.Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", lngTransportationDetailId)).ToString = "" Then
                dblTripRate = dtTrans.Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", lngTransportationDetailId))
            End If
            e.OutputParamters.Add("TripRate", dblTripRate)
            '  CacheData(TRANSPORTATION_DETAIL_RATE, dsTransportation)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                         MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_CalculateTripsDetails"
    Private Sub pvt_CalculateTripsDetails(ByRef br_intNoOfTrips As Integer, _
                                          ByRef br_intNoOfEquipment As Integer, _
                                          ByRef br_decTripsRate As Decimal)
        Try
            Dim intSingleTrip As Integer = 0
            Dim intDoubleTrip As Integer = 0
            Dim intDefaultTrip As Integer = 0
            Dim decRate As Decimal = 0
            Dim intDoubleTotalRate As Integer = 0
            Dim intDefaultTotalRate As Integer = 0
            Dim intRemainder As Integer = 0
            Dim intDivider As Integer = 0
            Dim intDefaultRemainder As Integer = 0
            Dim intDefaultDivider As Integer = 0
            Dim strEquipmentStatus As String = String.Empty

            dsTransportation = CType(RetrieveData(TRANSPORTATION), TransportationDataSet)
            If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), "").ToString = "" Then
                br_decTripsRate = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), "")
            End If

            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION).Rows.Count > 0 Then
                strEquipmentStatus = dsTransportation.Tables(TransportationData._V_TRANSPORTATION).Rows(0).Item(TransportationData.EQPMNT_STT_CD).ToString
            Else
                If Not RetrieveData("EquipmentStateCode") Is Nothing Then
                    strEquipmentStatus = CStr(RetrieveData("EquipmentStateCode"))
                End If
            End If

            If strEquipmentStatus.ToUpper() = "EMPTY" Then
                '   If dsTransportation.Tables(TransportationData._V_TRANSPORTATION).Rows(0).Item(TransportationData.EQPMNT_STT_CD).ToString = "EMPTY" Then
                If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='Y'")).ToString = "" Then
                    intSingleTrip = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='Y'"))
                End If
                If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='N'")).ToString = "" Then
                    intDoubleTrip = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, "='N'"))
                End If
                If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, " IS NULL")).ToString = "" Then
                    intDefaultTrip = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), String.Concat(TransportationData.EMPTY_SNGL_CD, " IS NULL"))
                End If
                If intDoubleTrip <> 0 Then
                    intDivider = Math.Truncate(intDoubleTrip / 2)
                    Math.DivRem(intDoubleTrip, 2, intRemainder)
                    intDoubleTotalRate = intRemainder + intDivider
                End If
                If intDefaultTrip <> 0 Then
                    intDefaultDivider = Math.Truncate(intDefaultTrip / 2)
                    Math.DivRem(intDefaultTrip, 2, intDefaultRemainder)
                    intDefaultTotalRate = intDefaultDivider + intDefaultRemainder
                End If

                br_intNoOfTrips = intDoubleTotalRate + intSingleTrip + intDefaultTotalRate
            Else
                If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), "").ToString = "" Then
                    br_intNoOfTrips = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.TRNSPRTTN_DTL_ID, ")"), "")
                End If
            End If
            'End If
            If Not dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.EQPMNT_NO, ")"), "").ToString = "" Then
                br_intNoOfEquipment = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Compute(String.Concat("COUNT(", TransportationData.EQPMNT_NO, ")"), "")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "saveTransportationDetailRate"
    Private Sub pvt_saveTransportationRate(ByVal bv_strTransportationDetailId As String)
        Try
            dsTransportation = CType(RetrieveData(TRANSPORTATION_DETAIL_RATE), TransportationDataSet)
            Dim dtTrans As New DataTable
            dtTrans.Merge(CType(ifgTransportationDetailRate.DataSource, DataTable))
            dtTrans.AcceptChanges()

            'dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(dtTrans)

            If Not dtTrans Is Nothing Then
                'dtTransportation = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", bv_strTransportationDetailId)).CopyToDataTable()
                For Each drTransportationDetailRate As DataRow In dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", bv_strTransportationDetailId))
                    drTransportationDetailRate.Delete()
                Next
                dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(dtTrans)
            End If

            CacheData(TRANSPORTATION, dsTransportation)
            CacheData(TRANSPORTATION_DETAIL_RATE, dsTransportation)
            Dim decTotalTripRate As Decimal = 0
            Dim decTripRate As Decimal = 0
            Dim decRate As Decimal = 0
            Dim dtTemp As DataTable
            Dim intNoOfTrips As Integer = 0
            Dim intNoOfEquipment As Integer = 0
            Dim decStateRate As Decimal = 0
            Dim decAdditionalRate As Decimal = 0

            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows.Count > 0 Then
                decStateRate = CDec(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_TRIP).Rows(0).Item(TransportationData.TRNSPRTR_TRP_RT_NC))
                'Else
                '    If Not RetrieveData("TripRate") Is Nothing Then
                '        decStateRate = CDec(RetrieveData("TripRate"))
                '    End If
            End If
            dtTemp = CType(ifgTransportationDetailRate.DataSource, DataTable)
            pvt_CalculateTripsDetails(intNoOfTrips, intNoOfEquipment, decTripRate)
            decTotalTripRate = (decStateRate * intNoOfTrips) + decTripRate
            If dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, " = ", bv_strTransportationDetailId)).ToString <> "" Then
                decAdditionalRate = CDec(dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Compute(String.Concat("SUM(", TransportationData.ADDTNL_CHRG_RT_NC, ")"), String.Concat(TransportationData.TRNSPRTTN_DTL_ID, " = ", bv_strTransportationDetailId)))
            End If
            Dim drSelect As DataRow()
            Dim drDelete As DataRow()
            drDelete = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", bv_strTransportationDetailId))
            drSelect = dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL).Select(String.Concat(TransportationData.TRNSPRTTN_DTL_ID, "=", bv_strTransportationDetailId))
            If drSelect.Length > 0 Then
                drSelect(0).Item(TransportationData.TTL_RT_NC) = decAdditionalRate
            End If
            dsTransportation.Tables(TransportationData._V_TRANSPORTATION_DETAIL_RATE).Merge(dtTemp)
            CacheData(TRANSPORTATION, dsTransportation)
            pub_SetCallbackReturnValue("TotalRate", decTotalTripRate)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                           MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region



End Class
