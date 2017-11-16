Option Strict On
Partial Class Rental_RentalOtherCharge
    Inherits Pagebase
#Region "Decalration"
    Dim dsRental As New RentalEntryDataSet
    Dim dtRental As DataTable
    Private Const RENTAL_OTHERCHARGE As String = "RENTAL_OTHERCHARGE"
    Private Const RENTAL_ENTRY As String = "RENTAL_ENTRY"
    Private Const RENTAL_OTHERCHARGE_TEMP As String = "RENTAL_OTHERCHARGE_TEMP"
#End Region

#Region "Page_PreLoad"
    Protected Sub Page_PreLoad(sender As Object, e As System.EventArgs) Handles Me.PreLoad
        Try
            If Not Page.IsPostBack AndAlso Not Page.IsCallback Then
                hdnEquipmentNo.Value = CStr(Request.QueryString("EquipmentNo"))
                hdnCustomerId.Value = (Request.QueryString("CustomerId"))
                hdnContractNo.Value = CStr(Request.QueryString("ContractNo"))
                hdnRentalId.Value = Request.QueryString("RentalID")
                hdnEqpmntID.Value = Request.QueryString("EqpmntID")
                pvt_SetChangesMade()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgRentalOtherCharge, "tabOtherCharge")
    End Sub

#End Region

#Region "ifgRentalOtherCharge_ClientBind"
    Protected Sub ifgRentalOtherCharge_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgRentalOtherCharge.ClientBind
        Try
            Dim drA As DataRow() = Nothing
            Dim objRental As New RentalEntry
            Dim dtAdditionalCharge As New DataTable
            Dim strEqpmntNo As String = e.Parameters("EquipmentNo").ToString()
            Dim intCstmr_id As String = (e.Parameters("CustomerID").ToString())
            Dim strContractNo As String = e.Parameters("ContractNo").ToString()
            Dim intRentalId As String = (e.Parameters("RentalID").ToString())
            Dim intEpmntId As String = (e.Parameters("EqpmntID").ToString())
            Dim dtCurrentData As New DataTable
            Dim drRentalChargeRate As DataRow = Nothing
            Dim objCommonData As New CommonData
            Dim intDepotId As Int32 = CInt(objCommonData.GetDepotID())
            dsRental = CType(RetrieveData(RENTAL_ENTRY), RentalEntryDataSet)

            'If Not IsNothing(e.Parameters("Offhiredate")) AndAlso CStr(e.Parameters("Offhiredate")) <> String.Empty Then
            If Not CStr(e.Parameters("Offhiredate")) = "null" Then
                ifgRentalOtherCharge.AllowAdd = False
                ifgRentalOtherCharge.AllowDelete = False
                ifgRentalOtherCharge.AllowEdit = False
            Else                ifgRentalOtherCharge.AllowAdd = True
                ifgRentalOtherCharge.AllowDelete = True
                ifgRentalOtherCharge.AllowEdit = True
            End If

            If Not dsRental Is Nothing Then
                dtRental = dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE)
                If Not intRentalId = "" Then
                    drA = dtRental.Select(String.Concat(RentalEntryData.RNTL_ENTRY_ID, "=", intRentalId))
                    If drA.Length = 0 Then
                        drA = dtRental.Select(String.Concat(RentalEntryData.RNTL_ENTRY_ID, "=", intRentalId), "", DataViewRowState.Deleted)
                        If drA.Length = 0 Then
                            dtAdditionalCharge = objRental.pub_GetAdditionalChargeRateByDepotId(intDepotId).Tables(TransportationData._V_ADDITIONAL_CHARGE_RATE)
                            drA = Nothing
                        End If
                    End If
                Else
                    drA = dtRental.Select(String.Concat(RentalEntryData.EQPMNT_INFRMTN_ID, "=", intEpmntId))
                    If drA.Length = 0 Then
                        drA = dtRental.Select(String.Concat(RentalEntryData.EQPMNT_INFRMTN_ID, "=", intEpmntId), "", DataViewRowState.Deleted)
                        If drA.Length = 0 Then
                            dtAdditionalCharge = objRental.pub_GetAdditionalChargeRateByDepotId(intDepotId).Tables(TransportationData._V_ADDITIONAL_CHARGE_RATE)
                            drA = Nothing
                        End If
                    End If
                End If

            End If
            If drA Is Nothing Then
                If Not intRentalId = "" Then
                    If dsRental Is Nothing Then
                        dsRental = objRental.pub_GetOtherChargeBy_RentalID(CInt(intRentalId))
                        dtRental = dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE)
                        '  dtAdditionalCharge = objRental.pub_GetAdditionalChargeRateByDepotId(intDepotId).Tables(TransportationData._V_ADDITIONAL_CHARGE_RATE)
                    Else
                        dtRental = objRental.pub_GetOtherChargeBy_RentalID(CInt(intRentalId)).Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE)
                        dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(dtRental)
                        '  dtAdditionalCharge = objRental.pub_GetAdditionalChargeRateByDepotId(intDepotId).Tables(TransportationData._V_ADDITIONAL_CHARGE_RATE)
                    End If
                Else
                    If dsRental Is Nothing Then
                        dsRental = objRental.pub_GetOtherChargeBy_RentalID(CInt(intEpmntId))
                        dtRental = dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE)
                        ' dtAdditionalCharge = objRental.pub_GetAdditionalChargeRateByDepotId(intDepotId).Tables(TransportationData._V_ADDITIONAL_CHARGE_RATE)
                    Else
                        dtRental = objRental.pub_GetOtherChargeBy_RentalID(CInt(intEpmntId)).Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE)
                        dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(dtRental)
                        '  dtAdditionalCharge = objRental.pub_GetAdditionalChargeRateByDepotId(intDepotId).Tables(TransportationData._V_ADDITIONAL_CHARGE_RATE)
                    End If
                End If
            End If
            CacheData(RENTAL_OTHERCHARGE_TEMP, dsRental)
            If Not intRentalId = "" Then
                Dim drs() As DataRow = dtRental.Select(String.Concat(RentalEntryData.RNTL_ENTRY_ID, "=", intRentalId))
                dtCurrentData = dtRental.Clone
                If drs.Length > 0 Then
                    dtCurrentData = drs.CopyToDataTable()
                End If
            Else
                Dim drs() As DataRow = dtRental.Select(String.Concat(RentalEntryData.EQPMNT_INFRMTN_ID, "=", intEpmntId))
                dtCurrentData = dtRental.Clone
                If drs.Length > 0 Then
                    dtCurrentData = drs.CopyToDataTable()
                End If
            End If
            'Dim lngNextIndex As Long
            'If dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows.Count > 0 Then
            '    If dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows(dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows.Count - 1).RowState = DataRowState.Deleted Then
            '        lngNextIndex = CLng(dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows(dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows.Count - 1).Item(RentalEntryData.RNTL_OTHR_CHRG_ID, DataRowVersion.Original)) + 1
            '    Else
            '        lngNextIndex = CLng(dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows(dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows.Count - 1).Item(RentalEntryData.RNTL_OTHR_CHRG_ID)) + 1

            '    End If
            'Else
            '    lngNextIndex = 1
            'End If


            'If dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows.Count > 0 Then
            '    lngNextIndex = CLng(dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows(dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows.Count - 1).Item(RentalEntryData.RNTL_OTHR_CHRG_ID)) + 1
            'Else
            '    lngNextIndex = 1
            'End If
            ' dtCurrentData = dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Copy()
            'If dtCurrentData.Rows.Count <= 0 Then
            '    For Each drAdditional As DataRow In dtAdditionalCharge.Rows
            '        drRentalChargeRate = dtCurrentData.NewRow()
            '        drRentalChargeRate.Item(RentalEntryData.RNTL_OTHR_CHRG_ID) = lngNextIndex 'CommonWeb.GetNextIndex(dtCurrentData, TransportationData.TRNSPRTTN_DTL_RT_ID)
            '        drRentalChargeRate.Item(RentalEntryData.RNTL_ENTRY_ID) = intRentalId
            '        drRentalChargeRate.Item(RentalEntryData.ADDTNL_CHRG_RT_ID) = drAdditional.Item(TransportationData.ADDTNL_CHRG_RT_ID)
            '        drRentalChargeRate.Item(RentalEntryData.ADDTNL_CHRG_RT_CD) = drAdditional.Item(TransportationData.ADDTNL_CHRG_RT_CD)
            '        drRentalChargeRate.Item(RentalEntryData.RT_NC) = drAdditional.Item(TransportationData.RT_NC)
            '        drRentalChargeRate.Item(RentalEntryData.DFLT_BT) = drAdditional.Item(TransportationData.DFLT_BT)
            '        dtCurrentData.Rows.Add(drRentalChargeRate)
            '        lngNextIndex = lngNextIndex + 1
            '    Next
            ' dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(dtCurrentData)
            ' dtCurrentData.AcceptChanges()

            'dsTransportation.AcceptChanges()
            '  End If
            ifgRentalOtherCharge.DataSource = dtCurrentData
            ifgRentalOtherCharge.DataBind()
            'CacheData(RENTAL_OTHERCHARGE_TEMP, dsRental)
            hdnRentalId.Value = CStr(intRentalId)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "GetOtherCharge"
                    GetOtherCharge(CInt(e.GetCallbackValue("RentalID")))
                Case "getChargeRate"
                    pvt_getClientChargeRate(e.GetCallbackValue("AdditionalChargeRateId"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                               MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_getClientChargeRate"
    Private Sub pvt_getClientChargeRate(ByVal bv_AdditionalChargeRateId As String)
        Try
            Dim dblChargeRate As Double = 0
            Dim objRental As New RentalEntry
            Dim objCommonData As New CommonData
            Dim intDepotId As Int32 = CInt(objCommonData.GetDepotID())
            dblChargeRate = objRental.pub_GetAdditionalChargeRateById(CInt(bv_AdditionalChargeRateId), intDepotId)
            pub_SetCallbackReturnValue("ChargeRate", CStr(dblChargeRate))
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "GetOtherCharge"
    Public Sub GetOtherCharge(ByVal RentalID As Int64)
        Try

            dsRental = CType(RetrieveData(RENTAL_OTHERCHARGE_TEMP), RentalEntryDataSet)
            dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(CType(ifgRentalOtherCharge.DataSource, DataTable))
            CacheData(RENTAL_OTHERCHARGE, dsRental)
            CacheData(RENTAL_OTHERCHARGE_TEMP, dsRental)
            Dim dblOtherRate As Double = 0
            Dim dtTemp As DataTable
            dtTemp = CType(ifgRentalOtherCharge.DataSource, DataTable)
            For Each drOtherRate As DataRow In dtTemp.Select(String.Concat(RentalEntryData.RT_NC, " IS NOT NULL"))
                dblOtherRate += CInt(drOtherRate.Item(RentalEntryData.RT_NC))
            Next
            Dim drDelete As DataRow()
            drDelete = dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Select(String.Concat(RentalEntryData.RNTL_ENTRY_ID, "=", RentalID))
            Dim drTotalOtherCharge As DataRow() = Nothing
            drTotalOtherCharge = dsRental.Tables(RentalEntryData._V_RENTAL_ENTRY).Select(String.Concat(RentalEntryData.RNTL_ENTRY_ID, "=", RentalID))
            If drTotalOtherCharge.Length > 0 Then
                drTotalOtherCharge(0).Item(RentalEntryData.TTL_OTHR_CHRG_NC) = dblOtherRate
            End If
            CacheData(RENTAL_OTHERCHARGE, dsRental)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                               MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Rental/RentalOtherCharge.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRentalOtherCharge_RowDataBound"
    Protected Sub ifgRentalOtherCharge_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgRentalOtherCharge.RowDataBound
        Try
            Dim objCommondata As New CommonData
            Dim strDeporCurrencyCD As String = (objCommondata.GetDepotLocalCurrencyCode()).ToString
            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Rate", " (", strDeporCurrencyCD, ")")
            End If
            If e.Row.RowIndex > 0 Then
                Dim lkpControl As iLookup
                lkpControl = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                lkpControl.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                    If CommonUIs.iBool(drv.Item(ProductData.DFLT_BT)) Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    Else
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                    End If
                    'End If
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRentalOtherCharge_RowDeleted"
    Protected Sub ifgRentalOtherCharge_RowDeleted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeletedEventArgs) Handles ifgRentalOtherCharge.RowDeleted
        Try
            Dim dblRate As Double = 0
            Dim lngRentalDetailId As Int64 = CLng(RetrieveData("RentalID"))
            dsRental = CType(RetrieveData(RENTAL_OTHERCHARGE_TEMP), RentalEntryDataSet)
            dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(CType(ifgRentalOtherCharge.DataSource, DataTable))
            If Not dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Compute(String.Concat("SUM(", RentalEntryData.RT_NC, ")"), String.Concat(RentalEntryData.RNTL_ENTRY_ID, "=", lngRentalDetailId)).ToString = "" Then
                dblRate = CDbl(CType(ifgRentalOtherCharge.DataSource, DataTable).Compute(String.Concat("SUM(", RentalEntryData.RT_NC, ")"), String.Concat(RentalEntryData.RNTL_ENTRY_ID, "=", lngRentalDetailId)))
            End If
            e.OutputParamters.Add("Rate", dblRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRentalOtherCharge_RowDeleting"
    Protected Sub ifgRentalOtherCharge_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgRentalOtherCharge.RowDeleting
        Try
            CacheData("RentalID", CLng(e.InputParamters("RentalID")))
            Dim dtRental As New DataTable
            'Can not Delete Default Value
            dsRental = CType(RetrieveData(RENTAL_OTHERCHARGE_TEMP), RentalEntryDataSet)
            dtRental = dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Copy()
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgRentalOtherCharge.PageSize * ifgRentalOtherCharge.PageIndex + e.RowIndex

            Dim drChkDefault As DataRow()
            drChkDefault = dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Select(String.Concat(RentalEntryData.ADDTNL_CHRG_RT_ID, "=", e.Values.Item(RentalEntryData.ADDTNL_CHRG_RT_ID), " AND ", RentalEntryData.DFLT_BT, "=True"))
            If drChkDefault.Length > 0 Then
                e.Cancel = True
                e.OutputParamters.Add("CheckDefault", String.Concat("Additional Charge Rate : Default value ", dtRental.Rows(intRowIndex).Item(RentalEntryData.ADDTNL_CHRG_RT_CD).ToString, " cannot be deleted"))
            Else
                'e.OutputParamters("Delete") = String.Concat("Additional Charge Rate : ", dtRental.Rows(intRowIndex).Item(RentalEntryData.ADDTNL_CHRG_RT_CD).ToString, " has been deleted from Product. Click submit to save changes.")
                'e.OutputParamters("Delete") = String.Concat("Additional Charge Rate : ", dtRental.Select(String.Concat(RentalEntryData.RNTL_OTHR_CHRG_ID, " = "), e.Keys(0))(0).Item(RentalEntryData.ADDTNL_CHRG_RT_CD).ToString, " has been deleted from Additional Charge. Click submit to save changes.")
                e.OutputParamters("Delete") = String.Concat("Additional Charge Rate :  ", dtRental.Select(String.Concat(RentalEntryData.RNTL_OTHR_CHRG_ID, "=", e.Keys(0)))(0).Item(RentalEntryData.ADDTNL_CHRG_RT_CD).ToString, " has been deleted. Click submit to save changes.")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                         MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRentalOtherCharge_RowInserted"
    Protected Sub ifgRentalOtherCharge_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgRentalOtherCharge.RowInserted
        Try
            Dim dblRate As Double = 0
            Dim lngRentalDetailId As Int64 = CLng(RetrieveData("RentalID"))
            dsRental = CType(RetrieveData(RENTAL_OTHERCHARGE_TEMP), RentalEntryDataSet)
            dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(CType(ifgRentalOtherCharge.DataSource, DataTable))
            If Not dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Compute(String.Concat("SUM(", RentalEntryData.RT_NC, ")"), String.Concat(RentalEntryData.RNTL_ENTRY_ID, "=", lngRentalDetailId)).ToString = "" Then
                dblRate = CDbl(dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Compute(String.Concat("SUM(", RentalEntryData.RT_NC, ")"), String.Concat(RentalEntryData.RNTL_ENTRY_ID, "=", lngRentalDetailId)))
            End If
            e.OutputParamters.Add("Rate", dblRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRentalOtherCharge_RowInserting"
    Protected Sub ifgRentalOtherCharge_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgRentalOtherCharge.RowInserting
        Try
            Dim lngID As Long

            dsRental = CType(RetrieveData(RENTAL_OTHERCHARGE_TEMP), RentalEntryDataSet)
            dtRental = dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE)
            If dtRental.Rows.Count = 0 AndAlso CType(ifgRentalOtherCharge.DataSource, DataTable).Rows.Count = 0 Then
                lngID = CommonWeb.GetNextIndex(dtRental, RentalEntryData.RNTL_OTHR_CHRG_ID)
            ElseIf dtRental.Rows.Count = 0 AndAlso CType(ifgRentalOtherCharge.DataSource, DataTable).Rows.Count > 0 Then
                lngID = CType(ifgRentalOtherCharge.DataSource, DataTable).Rows.Count
                lngID = lngID + CType(ifgRentalOtherCharge.DataSource, DataTable).Rows.Count
            Else
                lngID = CommonWeb.GetNextIndex(dtRental, RentalEntryData.RNTL_OTHR_CHRG_ID)
                lngID = lngID + CType(ifgRentalOtherCharge.DataSource, DataTable).Rows.Count
            End If


         
            e.Values(RentalEntryData.RNTL_OTHR_CHRG_ID) = lngID
            e.Values(RentalEntryData.EQPMNT_INFRMTN_ID) = CLng(e.InputParamters("EqpmntID"))
            If Not CStr(e.InputParamters("RentalID")) = "" Then
                e.Values(RentalEntryData.RNTL_ENTRY_ID) = CLng(e.InputParamters("RentalID"))
            End If
            CacheData("RentalID", CLng(e.InputParamters("RentalID")))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                             MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRentalOtherCharge_RowUpdated"
    Protected Sub ifgRentalOtherCharge_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgRentalOtherCharge.RowUpdated
        Try
            Dim dblRate As Double = 0
            Dim lngRentalDetailId As Int64 = CLng(RetrieveData("RentalID"))
            dsRental = CType(RetrieveData(RENTAL_OTHERCHARGE_TEMP), RentalEntryDataSet)
            dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(CType(ifgRentalOtherCharge.DataSource, DataTable))
            If Not dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Compute(String.Concat("SUM(", RentalEntryData.RT_NC, ")"), String.Concat(RentalEntryData.RNTL_ENTRY_ID, "=", lngRentalDetailId)).ToString = "" Then
                dblRate = CDbl(dsRental.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Compute(String.Concat("SUM(", RentalEntryData.RT_NC, ")"), String.Concat(RentalEntryData.RNTL_ENTRY_ID, "=", lngRentalDetailId)))
            End If
            e.OutputParamters.Add("Rate", dblRate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                          MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRentalOtherCharge_RowUpdating"
    Protected Sub ifgRentalOtherCharge_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgRentalOtherCharge.RowUpdating
        Try
            CacheData("RentalID", CLng(e.InputParamters("RentalID")))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
     
End Class
