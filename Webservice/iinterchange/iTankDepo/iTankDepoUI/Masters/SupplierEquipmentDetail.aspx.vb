
Partial Class Masters_SupplierEquipmentDetail
    Inherits Pagebase
#Region "Declaration"

    Private Const KEY_ID As String = "KEY_ID"
    Private Const KEY_MODE As String = "KEY_MODE"
    Private strMSGINSERT As String = "Inserted Successfully."
    Private strMSGUPDATE As String = "Updated Successfully."
    Private strMSGDUPLICATE As String = "This combination already exists."
    Private pvt_lngID As Long
    Dim dsSupplier As SupplierDataSet
    Private Const SUPPLIERDETAILS As String = "SUPPLIERDETAILS"
    Private Const SUPPLIERTEMP As String = "SUPPLIERTEMP"
    Private Const SUPPLIER As String = "SUPPLIER"
    Dim dtSupplier As DataTable
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack AndAlso Not Page.IsCallback Then
                hdnSupplierId.Value = Request.QueryString("SupplierID")
                hdnSupplierContractId.Value = Request.QueryString(SupplierData.SPPLR_CNTRCT_DTL_ID)
                hdnContractRefNo.Value = Request.QueryString("ContractRefNo")
                hdnEndDate.Value = Request.QueryString("ContractEndDate")
                'If (CDate(Request.QueryString("ContractEndDate")).ToString("dd-MMM-yyyy")) < CDate(DateTime.Now).ToString("dd-MMM-yyyy") Then
                '    lblMessage.Text = "Contract Reference has been expired hence cannot Add or Delete Equipment."
                'End If

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
        pub_SetGridChanges(ifgEquipmentDetails, "tabEquipmentDetails")

    End Sub

#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "GetEquipmentDetails"
                    pub_GetEquipmentDetails(e.GetCallbackValue("ContractRefNo"), _
                                            e.GetCallbackValue("ContractId"))
                Case "ValidateEquipment"
                    pvt_ValidateEquipment(e.GetCallbackValue("EquipmentId"), _
                                          CInt(e.GetCallbackValue("SupplierContractId")), _
                                          CDate(e.GetCallbackValue("ContractEndDate")), _
                                          CInt(e.GetCallbackValue("GridIndex")), _
                                          e.GetCallbackValue("RowState"))
                Case "ValidateDate"
                    pvt_ValidateDate(e.GetCallbackValue("ContractEndDate"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                               MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateDate"
    Private Sub pvt_ValidateDate(ByVal bv_strEndDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim blnCompareValid As Boolean = False
            If (bv_strEndDate) = Nothing Then
                pub_SetCallbackReturnValue("Error", String.Concat("Contract End Date Required"))
                pub_SetCallbackStatus(True)
                Exit Sub
            End If
            If ((CDate(bv_strEndDate) < CDate(DateTime.Now))) Then
                blnDateValid = True
            End If
            Dim EndDate As DateTime = CDate(bv_strEndDate)
            'If blnDateValid = True Then
            '    ifgEquipmentDetails.AllowAdd = False
            '    ifgEquipmentDetails.AllowDelete = False
            '    pub_SetCallbackReturnValue("Error", "Contract Reference has been expired hence cannot add Equipment.")
            'End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateEquipment"
    Private Sub pvt_ValidateEquipment(ByVal bv_strEquipmentNo As String, ByVal SupplierContractId As Integer, ByVal enddate As Date, ByVal bv_intGridIndex As Integer, ByVal bv_strRowstate As String)
        Try
            Dim blndsValid As Boolean
            Dim blnEquipmentInfo As Boolean
            Dim objEquipmentNo As New Supplier
            Dim objCommon As New CommonData
            Dim dtContractNo As New DataTable
            dsSupplier = CType(RetrieveData(SUPPLIERDETAILS), SupplierDataSet)
            ' dtSupplier = dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL)
            dtSupplier = ifgEquipmentDetails.DataSource
            Dim intResultIndex() As System.Data.DataRow = dtSupplier.Select(String.Concat(SupplierData.EQPMNT_NO, "='", bv_strEquipmentNo, "' "))
            Dim strExistEquipment As String = ""
            If intResultIndex.Length > 0 Then
                If dtSupplier.Rows.Count > bv_intGridIndex Then
                    If dtSupplier.Rows(bv_intGridIndex).RowState = Data.DataRowState.Deleted Then
                        strExistEquipment = String.Empty
                    ElseIf dtSupplier.Rows(bv_intGridIndex).RowState <> Data.DataRowState.Deleted Then
                        strExistEquipment = dtSupplier.Rows(bv_intGridIndex)(SupplierData.EQPMNT_NO).ToString
                    End If
                End If

                If bv_strEquipmentNo = strExistEquipment Then
                    blndsValid = True
                Else
                    blndsValid = False
                End If
            Else
                blndsValid = True
            End If
            'Checking whether the entered code is available in database
            If blndsValid = True Then
                If bv_strRowstate = "Added" Or bv_strRowstate = "Modified" Then
               
                    blndsValid = objEquipmentNo.pub_ValidateEquipmentNoByDepotID(bv_strEquipmentNo, SupplierContractId, CInt(objCommon.GetDepotID()))
                End If
            End If

            If blndsValid = False Then
                '    pub_SetCallbackReturnValue("bNotExists", "true")
                'Else
                'pub_SetCallbackReturnValue("bNotExists", "false")
                dtContractNo = objEquipmentNo.pub_GetContractNoDetail(SupplierContractId, bv_strEquipmentNo, CInt(objCommon.GetDepotID())).Tables(SupplierData._SUPPLIER_CNTRACT_DTL)
                If dtContractNo.Rows.Count > 0 Then
                    pub_SetCallbackReturnValue("Error", String.Concat("This Equipment No already Exist for Contract Reference No (", dtContractNo.Rows(0).Item(SupplierData.CNTRCT_RFRNC_NO), ") for the Supplier (", dtContractNo.Rows(0).Item(SupplierData.SPPLR_CD), ")"))
                Else
                    pub_SetCallbackReturnValue("Error", "This Equipment No already exists")
                End If
            Else
                blnEquipmentInfo = objEquipmentNo.pubValidateEquipment(bv_strEquipmentNo, CInt(objCommon.GetDepotID()))
            End If
            If blnEquipmentInfo = False Then
                pub_SetCallbackReturnValue("Error", String.Concat("This Equipment No Already Exist In Operational Work Flow"))
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pub_GetEquipmentDetails"
    Public Sub pub_GetEquipmentDetails(ByVal bv_contractrefNo As String, ByVal bv_contractId As Int64)
        Try

            dsSupplier = CType(RetrieveData(SUPPLIERTEMP), SupplierDataSet)
            'Dim dsTemp As DataSet = CType(RetrieveData(Supplier), SupplierDataSet)
            'For Each drContract As DataRow In dsTemp.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).Select(String.Concat(SupplierData.SPPLR_CNTRCT_DTL_ID, "=", bv_contractId))
            '    drContract.Item(SupplierData.CHECKED) = True
            'Next
            dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Merge(CType(ifgEquipmentDetails.DataSource, DataTable))
            Dim dt As DataTable = (CType(ifgEquipmentDetails.DataSource, DataTable))

            CacheData(SUPPLIERDETAILS, dsSupplier)
            CacheData(SUPPLIERTEMP, dsSupplier)
            'CacheData(SUPPLIER, dsTemp)
            Dim dtTemp As DataTable
            dtTemp = CType(ifgEquipmentDetails.DataSource, DataTable)

            Dim drDelete As DataRow()
            drDelete = dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Select(String.Concat(SupplierData.SPPLR_CNTRCT_DTL_ID, "=", bv_contractId))
            dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Merge(dtTemp)
            CacheData(SUPPLIERDETAILS, dsSupplier)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                           MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetails_RowDataBound"

    Protected Sub ifgEquipmentDetails_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentDetails.RowDataBound

        If Not e.Row.DataItem Is Nothing Then
            Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If drv.Row.RowState = DataRowState.Added Then
                CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
            ElseIf drv.Row.RowState = DataRowState.Unchanged Then
                CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
            End If
            If e.Row.RowIndex > 5 Then
                Dim lkpTypControl As iLookup
                'Dim lkpCodeControl As iLookup
                lkpTypControl = CType(DirectCast(DirectCast(e.Row.Cells(1), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                lkpTypControl.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                'lkpCodeControl = CType(DirectCast(DirectCast(e.Row.Cells(2), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                'lkpCodeControl.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
            End If
        End If

    End Sub
#End Region

#Region "ifgEquipmentDetails_RowInserting"
    Protected Sub ifgEquipmentDetails_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgEquipmentDetails.RowInserting
        Try
            Dim lngID As Long
            dsSupplier = CType(RetrieveData(SUPPLIERTEMP), SupplierDataSet)
            dtSupplier = dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL)
            If dtSupplier.Rows.Count = 0 AndAlso CType(ifgEquipmentDetails.DataSource, DataTable).Rows.Count = 0 Then
                lngID = CommonWeb.GetNextIndex(dtSupplier, SupplierData.SPPLR_EQPMNT_DTL_ID)
            ElseIf dtSupplier.Rows.Count = 0 AndAlso CType(ifgEquipmentDetails.DataSource, DataTable).Rows.Count > 0 Then
                lngID = CType(ifgEquipmentDetails.DataSource, DataTable).Rows.Count
                lngID = lngID + CType(ifgEquipmentDetails.DataSource, DataTable).Rows.Count
            Else
                lngID = CommonWeb.GetNextIndex(dtSupplier, SupplierData.SPPLR_EQPMNT_DTL_ID)
                lngID = lngID + CType(ifgEquipmentDetails.DataSource, DataTable).Rows.Count
            End If

            e.Values(SupplierData.SPPLR_EQPMNT_DTL_ID) = lngID
            e.Values(SupplierData.SPPLR_CNTRCT_DTL_ID) = CLng(e.InputParamters("SupplierContractId"))
            e.Values(SupplierData.SPPLR_ID) = CLng(e.InputParamters("SupplierID"))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetails_ClientBind"
    Protected Sub ifgEquipmentDetails_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEquipmentDetails.ClientBind
        Try
            Dim bv_i64SupplierID As String = e.Parameters("SupplierID").ToString()
            Dim bv_int64SupplierContractId As String = e.Parameters("SupplierContractId").ToString()
            Dim contractRef As String = e.Parameters("ContractRefNo").ToString()
            Dim ContractEnddate As String = e.Parameters("ContractEndDate").ToString()
            Dim drA As DataRow() = Nothing
            Dim objSupplier As New Supplier
            dsSupplier = CType(RetrieveData(SUPPLIERDETAILS), SupplierDataSet)
            If bv_i64SupplierID <> 0 Then
                If Not dsSupplier Is Nothing Then
                    dtSupplier = dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL)
                    drA = dtSupplier.Select(String.Concat(SupplierData.SPPLR_CNTRCT_DTL_ID, "=", bv_int64SupplierContractId))
                    If drA.Length = 0 Then
                        drA = dtSupplier.Select(String.Concat(SupplierData.SPPLR_CNTRCT_DTL_ID, "=", bv_int64SupplierContractId), "", DataViewRowState.Deleted)
                        If drA.Length = 0 Then
                            drA = Nothing
                        End If
                    End If
                End If
            End If
           
            If drA Is Nothing Then

                If dsSupplier Is Nothing Then
                    dsSupplier = objSupplier.GetVSupplierEquipmentDetailBySupplierId(bv_i64SupplierID, bv_int64SupplierContractId)
                    dtSupplier = dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL)
                Else
                    dtSupplier = objSupplier.GetVSupplierEquipmentDetailBySupplierId(bv_i64SupplierID, bv_int64SupplierContractId).Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL)
                    dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Merge(dtSupplier)
                End If
            Else
            End If
            CacheData(SUPPLIERTEMP, dsSupplier)
            Dim drs() As DataRow = dtSupplier.Select(String.Concat(SupplierData.SPPLR_CNTRCT_DTL_ID, "=", bv_int64SupplierContractId, " AND ", SupplierData.SPPLR_ID, " IS NOT NULL"))
            Dim dtCurrentData As DataTable = dtSupplier.Clone

            If drs.Length > 0 Then
                dtCurrentData = drs.CopyToDataTable()
            End If
            'If (CDate(ContractEnddate).ToString("dd-MMM-yyyy") < (DateTime.Now).ToString("dd-MMM-yyyy")) Then
            '    ifgEquipmentDetails.AllowAdd = False
            '    ifgEquipmentDetails.AllowDelete = False
            '    ifgEquipmentDetails.AllowEdit = False
            '    'btnSubmit.Disabled = True
            '    lblMessage.Text = "Contract Reference has been expired hence cannot add Equipment."
            '    lblMessage.Visible = True
            '    e.OutputParameters.Add("ValidateEndDate", True)
            'Else
            e.OutputParameters.Add("ValidateEndDate", False)
            'End If
            'ifgEquipmentDetails.AllowAdd = False
            'ifgEquipmentDetails.AllowDelete = False

            If dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Rows.Count > 0 Then
                ifgEquipmentDetails.DataSource = dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL)
            Else
                ifgEquipmentDetails.DataSource = dtCurrentData
            End If

            ' ifgEquipmentDetails.DataSource = dtCurrentData
            ' ifgEquipmentDetails.DataBind()
            hdnSupplierContractId.Value = CStr(bv_int64SupplierContractId)
            CacheData(SUPPLIERDETAILS, dsSupplier)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Masters/SupplierEquipmentDetail.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
   
    Protected Sub ifgEquipmentDetails_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgEquipmentDetails.RowDeleting
        Try
            dsSupplier = CType(RetrieveData(SUPPLIERDETAILS), SupplierDataSet)
            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim dtRentalEntry As New DataTable
            Dim objSupplier As New Supplier
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgEquipmentDetails.PageSize * ifgEquipmentDetails.PageIndex + e.RowIndex
            dtRentalEntry = objSupplier.GetRentalEquipment(intDPT_ID).Tables(SupplierData._V_RENTAL_ENTRY)
            Dim drChkDefault As DataRow()
            drChkDefault = dtRentalEntry.Select(String.Concat(SupplierData.EQPMNT_NO, "='", e.Values.Item(SupplierData.EQPMNT_NO), "'"))
            If drChkDefault.Length > 0 Then
                e.Cancel = True
                e.OutputParamters.Add("CheckDefault", String.Concat("Equipment Detail: Equipment No (", dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Select(String.Concat(SupplierData.SPPLR_EQPMNT_DTL_ID, "=", e.Keys(0)))(0).Item(SupplierData.EQPMNT_NO).ToString, ") cannot be deleted since Rental Entry is created."))
            Else
                e.OutputParamters("Delete") = String.Concat("Supplier Equipment Detail : Equipment No (", dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Select(String.Concat(SupplierData.SPPLR_EQPMNT_DTL_ID, "=", e.Keys(0)))(0).Item(SupplierData.EQPMNT_NO).ToString, ") has been deleted. Click submit to save changes.")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

    Protected Sub ifgEquipmentDetails_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgEquipmentDetails.RowInserted
        Try

        Catch ex As Exception

        End Try
    End Sub
End Class
