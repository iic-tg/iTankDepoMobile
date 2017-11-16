
Partial Class Masters_Supplier
    Inherits Pagebase

#Region "Declaration"
    Private Const KEY_ID As String = "KEY_ID"
    Private Const KEY_MODE As String = "KEY_MODE"
    Private strMSGINSERT As String = "Inserted Successfully."
    Private strMSGUPDATE As String = "Updated Successfully."
    Private strMSGDUPLICATE As String = "This combination already exists."
    Private pvt_lngID As Long
    Dim dsSupplier As SupplierDataSet
    Private Const SUPPLIER As String = "SUPPLIER"
    Private Const SUPPLIERDETAILS As String = "SUPPLIERDETAILS"
    Dim dtSupplier As DataTable
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                pvt_SetChangesMade()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgContractDetails, "ITab1_0")
        CommonWeb.pub_AttachHasChanges(txtCode)
        CommonWeb.pub_AttachHasChanges(txtDescription)
        CommonWeb.pub_AttachHasChanges(chkActive)
    End Sub

#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "fnGetData"
                pvt_GetData(e.GetCallbackValue("mode"))
            Case "ValidateStartDate"
                pvt_ValidateStartDate(e.GetCallbackValue("StartDate"), _
                                                    e.GetCallbackValue("EndDate"))
            Case "ValidateEndDate"
                pvt_ValidateEndDate(e.GetCallbackValue("StartDate"), _
                                                    e.GetCallbackValue("EndDate"))
            Case "CreateSupplier"
                pvt_CreateSupplier(e.GetCallbackValue("Code"), _
                                   e.GetCallbackValue("Description"), _
                                   CBool(e.GetCallbackValue("Active")), _
                                   e.GetCallbackValue("wfData"))
            Case "UpdateSupplier"
                pvt_UpdateSupplier(e.GetCallbackValue("ID"), _
                                   e.GetCallbackValue("Code"), _
                                   e.GetCallbackValue("Description"), _
                                   CBool(e.GetCallbackValue("Active")), _
                                   e.GetCallbackValue("wfData"))
            Case "ValidateCode"
                pvt_ValidateSupplierCode(e.GetCallbackValue("Code"))
            Case "ValidateContractReferenceNo"
                pvt_ValidateContractReferenceNo(e.GetCallbackValue("ReferenceNo"), _
                                                e.GetCallbackValue("SupplierCode"), _
                                                e.GetCallbackValue("RowState"))
        End Select
    End Sub
#End Region

#Region "pvt_ValidateContractReferenceNo"
    Public Sub pvt_ValidateContractReferenceNo(ByVal bv_strContractRefNo As String, ByVal bv_SupplierCode As String, _
                                               ByVal bv_strRowState As String)

        Try
            Dim objSupplier As New Supplier
            Dim objCommon As New CommonData
            Dim strSupplier As String = String.Empty
            Dim intDepotID As Int32 = CInt(objCommon.GetDepotID())
            Dim blnValid As Boolean = True

            If bv_strRowState = "Added" Or bv_strRowState = "Modified" Then
                blnValid = objSupplier.pub_GetContractRefNo(bv_strContractRefNo, intDepotID, strSupplier)
                If blnValid Then
                    pub_SetCallbackReturnValue("valid", "true")
                Else
                    pub_SetCallbackReturnValue("valid", String.Concat("This Contract Reference No already exists for the Supplier (", strSupplier, ")"))
                End If
            End If
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateSupplierCode"
    Public Sub pvt_ValidateSupplierCode(ByVal bv_strSupplierCode As String)

        Try
            Dim objSupplier As New Supplier
            Dim objCommon As New CommonData
            Dim intDepotID As Int32 = CInt(objCommon.GetDepotID())
            Dim blnValid As Boolean
            blnValid = objSupplier.pub_GetSupplierCode(bv_strSupplierCode, intDepotID)
            If blnValid Then
                pub_SetCallbackReturnValue("valid", "true")
            Else
                pub_SetCallbackReturnValue("valid", "This Supplier Code already exists.")
            End If
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_CreateSupplier"
    Public Function pvt_CreateSupplier(ByVal bv_strSupplier_CD As String, _
                                       ByVal bv_strDescription As String, _
                                       ByVal bv_blnACTV_BT As Boolean, _
                                       ByVal bv_strWfData As String) As Long
        Dim objcommon As New CommonData
        Try
            Dim objSupplier As New Supplier
            Dim lngCreated As Long
            Dim dtSupplierContract As DataTable
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))

            dsSupplier = CType(RetrieveData(SUPPLIER), SupplierDataSet)
            dtSupplierContract = CType(ifgContractDetails.DataSource, DataTable)
            If Not dtSupplierContract Is Nothing Then
                dsSupplier.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).Merge(dtSupplierContract)
                If RetrieveData(SUPPLIERDETAILS) IsNot Nothing Then
                    dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Merge(CType(RetrieveData(SUPPLIERDETAILS), SupplierDataSet).Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL))
                End If
                For Each drSupplier As DataRow In dsSupplier.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).Rows
                    If drSupplier.RowState = DataRowState.Added Or drSupplier.RowState = DataRowState.Modified Then
                        Dim contractId As Integer = dsSupplier.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).Rows(0).Item(SupplierData.SPPLR_CNTRCT_DTL_ID)

                        Dim intResultIndex() As System.Data.DataRow = dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Select(String.Concat("SPPLR_CNTRCT_DTL_ID = ", drSupplier.Item(SupplierData.SPPLR_CNTRCT_DTL_ID)))

                        If intResultIndex.Length = 0 Then
                            pub_SetCallbackError(String.Concat("At least one Equipment must be added for the Contract Reference No (", drSupplier.Item(SupplierData.CNTRCT_RFRNC_NO), ")"))

                            pub_SetCallbackStatus(False)
                            Return 0
                            Exit Function
                        End If
                    End If
                Next
            End If

            lngCreated = objSupplier.pub_CreateSupplier((bv_strSupplier_CD), _
                                                         bv_strDescription, _
                                                         strModifiedby, _
                                                         datModifiedDate, _
                                                         bv_blnACTV_BT, _
                                                         intDepotID, _
                                                         bv_strWfData, _
                                                         dsSupplier)
            dsSupplier.AcceptChanges()
            pub_SetCallbackReturnValue("ID", CStr(lngCreated))
            pub_SetCallbackReturnValue("Message", String.Concat("Supplier : ", bv_strSupplier_CD, " ", strMSGINSERT))
            pub_SetCallbackStatus(True)
            dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Rows.Clear()
            CacheData(SUPPLIER, dsSupplier)
            CacheData(SUPPLIERDETAILS, dsSupplier)
            Return lngCreated
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "pvt_UpdateSupplier"

    Private Sub pvt_UpdateSupplier(ByVal bv_strSupplierId As Integer, _
                                  bv_strSupplier_CD As String, _
                                       ByVal bv_strDescription As String, _
                                       ByVal bv_blnACTV_BT As Boolean, _
                                       ByVal bv_strWfData As String)
        Try
            Dim objsupplier As New Supplier
            Dim boolUpdated As Boolean

            Dim objcommon As New CommonData
            Dim strModifiedby As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()

            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))

            dsSupplier = CType(RetrieveData(SUPPLIER), SupplierDataSet)
            dtSupplier = CType(ifgContractDetails.DataSource, DataTable)
            dsSupplier.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).Merge(dtSupplier)

            If RetrieveData(SUPPLIERDETAILS) IsNot Nothing Then
                dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Merge(CType(RetrieveData(SUPPLIERDETAILS), SupplierDataSet).Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL))
            Else
                Dim dtSupplier As New DataTable
                dtSupplier = objsupplier.getSupplierEquipment(bv_strSupplierId).Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL)
                dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Merge(dtSupplier)
                'pub_SetCallbackError("Please Save Equipment Details Before Submit)")
                'pub_SetCallbackStatus(False)
                'Exit Sub
            End If
            For Each drSupplier As DataRow In dsSupplier.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).Rows
                If drSupplier.RowState = DataRowState.Added Then
                    Dim contractId As Integer = dsSupplier.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).Rows(0).Item(SupplierData.SPPLR_CNTRCT_DTL_ID)

                    Dim intResultIndex() As System.Data.DataRow = dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Select(String.Concat("SPPLR_CNTRCT_DTL_ID = ", drSupplier.Item(SupplierData.SPPLR_CNTRCT_DTL_ID)))

                    If intResultIndex.Length = 0 Then
                        pub_SetCallbackError(String.Concat("At least one Equipment must be added for the Contract Reference No (", drSupplier.Item(SupplierData.CNTRCT_RFRNC_NO), ")"))
                        pub_SetCallbackStatus(False)
                        Exit Sub
                    End If
                End If
            Next
            boolUpdated = objsupplier.pub_ModifySupplier(CInt(bv_strSupplierId), _
                                                       (bv_strSupplier_CD), _
                                                         bv_strDescription, _
                                                         strModifiedby, _
                                                         datModifiedDate, _
                                                         bv_blnACTV_BT, _
                                                         intDepotID, _
                                                         bv_strWfData, _
                                                         dsSupplier)

            dsSupplier.AcceptChanges()
            pub_SetCallbackReturnValue("Message", String.Concat("Supplier : ", bv_strSupplier_CD, " ", strMSGUPDATE))
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "pvt_ValidateEndDate"
    Private Sub pvt_ValidateEndDate(ByVal bv_strStart As String, ByVal bv_strEndDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim blnCompareValid As Boolean = False
            If (bv_strStart) = Nothing Then
                pub_SetCallbackReturnValue("Error", String.Concat("Contract Start Date required"))
                pub_SetCallbackStatus(True)
                Exit Sub
            End If
            If (bv_strEndDate) = Nothing Then
                pub_SetCallbackReturnValue("Error", String.Concat("Contract End Date required"))
                pub_SetCallbackStatus(True)
                Exit Sub
            End If
            If (Not (CDate(bv_strEndDate) >= CDate(bv_strStart))) Then
                blnDateValid = True
            End If
            Dim StartDate As DateTime = CDate(bv_strStart)
            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Contract End Date should be greater than or equal to Contract Start Date (", StartDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateStartDate"
    Private Sub pvt_ValidateStartDate(ByVal bv_strStart As String, ByVal bv_strEndDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim blnCompareValid As Boolean = False
            If (bv_strStart) = Nothing Then
                pub_SetCallbackReturnValue("Error", String.Concat("Contract Start Date required"))
                pub_SetCallbackStatus(True)
                Exit Sub
            End If
            If (Not (CDate(bv_strEndDate) >= CDate(bv_strStart))) Then
                blnDateValid = True
            End If
            Dim EndDate As DateTime = CDate(bv_strEndDate)
            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Contract Start Date should be lesser than or equal to Contract End Date (", EndDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Masters/Supplier.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetData"
    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim sdSupplier As New StringBuilder
            Dim dtRentalEntry As New DataTable

            dsSupplier = New SupplierDataSet
            dtRentalEntry = dsSupplier.Tables(SupplierData._SUPPLIER).Clone()
            If bv_strMode = MODE_EDIT Then
                sdSupplier.Append(CommonWeb.GetTextValuesJSO(txtCode, PageSubmitPane.pub_GetPageAttribute(SupplierData.SPPLR_CD)))
                If PageSubmitPane.pub_GetPageAttribute(SupplierData.SPPLR_DSCRPTN_VC) Is Nothing Then
                    sdSupplier.Append(CommonWeb.GetTextValuesJSO(txtDescription, ""))
                Else
                    sdSupplier.Append(CommonWeb.GetTextValuesJSO(txtDescription, PageSubmitPane.pub_GetPageAttribute(SupplierData.SPPLR_DSCRPTN_VC)))
                End If
                sdSupplier.Append(CommonWeb.GetCheckboxValuesJSO(chkActive, CBool(PageSubmitPane.pub_GetPageAttribute(SupplierData.ACTV_BT))))
                sdSupplier.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute(SupplierData.SPPLR_ID), "');"))
                sdSupplier.Append(CommonWeb.GetHiddenTextValuesJSO(hdnSupplierId, PageSubmitPane.pub_GetPageAttribute(SupplierData.SPPLR_ID)))
            ElseIf bv_strMode = MODE_NEW Then
                sdSupplier.Append(String.Concat("setPageID('", CommonWeb.GetNextIndex(dtRentalEntry, SupplierData.SPPLR_ID), "');"))
            Else
                dsSupplier = New SupplierDataSet
                Dim objCommonData As New CommonData
            End If


            dsSupplier = CType(RetrieveData(SUPPLIER), SupplierDataSet)
            dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).Rows.Clear()
            CacheData(SUPPLIER, dsSupplier)
            CacheData(SUPPLIERDETAILS, dsSupplier)

            pub_SetCallbackReturnValue("Message", sdSupplier.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgContractDetails_ClientBind"
    Protected Sub ifgContractDetails_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgContractDetails.ClientBind
        Try
            Dim strWFData As String = e.Parameters("WFDATA").ToString()
            Dim strSupplier As String = e.Parameters("SupplierID").ToString()
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(strWFData, CommonUIData.DPT_ID))
            Dim objSupplier As New Supplier
            dsSupplier = objSupplier.pub_GetSupplierContractDetails(strSupplier)
            e.DataSource = dsSupplier.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL)
            CacheData(SUPPLIER, dsSupplier)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgContractDetails_RowDataBound"
    Protected Sub ifgContractDetails_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgContractDetails.RowDataBound
        Try
            Dim objCommon As New CommonUI
            Dim hypAddLink As HyperLink
            Dim objCommondata As New CommonData
            Dim strDeporCurrencyCD As String = (objCommondata.GetDepotLocalCurrencyCode()).ToString
            'If e.Row.RowType = DataControlRowType.Header Then
            '    CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Rental Per Day", " (", strDeporCurrencyCD, ")", " *")
            'End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                hypAddLink = CType(e.Row.Cells(4).Controls(0), HyperLink)
                hypAddLink.Attributes.Add("onclick", "showEquipmentDetails();return false;")
                hypAddLink.NavigateUrl = "#"
                hypAddLink.Text = "Add/Edit"
                hypAddLink.ToolTip = "Click the link to Add/Edit Equipment Details"
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                    MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgContractDetails_RowDeleting"
    Protected Sub ifgContractDetails_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgContractDetails.RowDeleting
        Try
            Dim intRowIndex As Integer = 0
            intRowIndex = ifgContractDetails.PageSize * ifgContractDetails.PageIndex + e.RowIndex
            dsSupplier = CType(RetrieveData(SUPPLIER), SupplierDataSet)
            Dim dtContract As Data.DataTable = dsSupplier.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).Copy
            'String.Concat("ADDTNL_CHRG_RT_CD", "='", bv_strCode, "'", " AND OPRTN_TYP_CD ='", bv_strOperation, "'"
            'If CType(ifgContractDetails.DataSource, DataTable).Select(String.Concat(SupplierData.SPPLR_CNTRCT_DTL_ID, "=", e.Keys(0), " AND ", (SupplierData.CNTRCT_STRT_DT), "<", CStr(DateTime.Now)))(0).RowState <> DataRowState.Added Then
            If CType(ifgContractDetails.DataSource, DataTable).Select(String.Concat(SupplierData.SPPLR_CNTRCT_DTL_ID, "=", e.Keys(0)))(0).RowState <> DataRowState.Added Then

                If (Date.Compare(CDate(dtContract.Rows(intRowIndex).Item(SupplierData.CNTRCT_STRT_DT)), CDate(DateTime.Now)) = -1) Then
                    e.Cancel = True
                    e.OutputParamters("Delete") = String.Concat("Contract Reference No (", dtContract.Rows(intRowIndex).Item(SupplierData.CNTRCT_RFRNC_NO).ToString, ") cannot be deleted")
                    Exit Sub
                Else
                    e.OutputParamters("Delete") = String.Concat("Contract Reference No (", dtContract.Rows(intRowIndex).Item(SupplierData.CNTRCT_RFRNC_NO).ToString, ") has been be deleted. Click submit to save changes.")
                    Exit Sub
                End If

            Else
                e.OutputParamters("Delete") = String.Concat("Contract Reference No (", dtContract.Rows(intRowIndex).Item(SupplierData.CNTRCT_RFRNC_NO).ToString, ") has been be deleted. Click submit to save changes.")
                Exit Sub
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgContractDetails_RowInserting"
    Protected Sub ifgContractDetails_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgContractDetails.RowInserting
        Try
            dsSupplier = CType(RetrieveData(SUPPLIER), SupplierDataSet)
            dtSupplier = dsSupplier.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL)
            e.Values(SupplierData.SPPLR_CNTRCT_DTL_ID) = CommonWeb.GetNextIndex(dtSupplier, SupplierData.SPPLR_CNTRCT_DTL_ID)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
