
Partial Class Operations_PreAdvice
    Inherits Pagebase

#Region "Declarations"
    Private strMSGUPDATE As String = "Pre-Advice : Equipment(s) Updated Successfully."
    Private strMSGDUPLICATE As String = "This combination already exists."
    Private Const PREADVICE As String = "PREADVICE"
    Dim bln_013EqType_Key As Boolean
    Dim str_013EqType As String
    Dim str_054GWS As String
    Dim bln_054GWSActive_Key As Boolean
#End Region

#Region "Parameters"
    Public dsPreAdvice As PreAdviceDataSet
    Public dtPreAdvice As DataTable
    Private pvt_strTableKey As String
    Private pvt_strTableName As String
#End Region

#Region "PageLoad"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim strpagetitle As String = Request.QueryString("pagetitle")
            ifgPreAdvice.DeleteButtonText = "Delete Row"
            ifgPreAdvice.RefreshButtonText = "Refresh"
            ifgPreAdvice.AddButtonText = "Add Row"
            hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Client Bind"
    Protected Sub ifgPreAdvice_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgPreAdvice.ClientBind
        Try
            Dim objPreAdvice As New PreAdvice
            Dim objCommon As New CommonData
            Dim strWfData As String = CStr(e.Parameters("WFDATA"))
            ifgPreAdvice.UseCachedDataSource = True
            dsPreAdvice = objPreAdvice.pub_PreAdviceGetPreAdvicebyDepotID(strWfData)

            Dim objCommonGWS As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDepotID As Integer = objCommonGWS.GetDepotID()

            If dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE).Rows.Count = 0 Then
                Dim drPreAdviceInfo As DataRow = dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE).NewRow()

                Dim objCommonUI As New CommonUI()
                Dim dsEqpmntType As New CommonUIDataSet

                str_013EqType = objCommonConfig.pub_GetConfigSingleValue("013", intDepotID)
                bln_013EqType_Key = objCommonConfig.IsKeyExists

                If bln_013EqType_Key Then
                    If Not str_013EqType = "" Then
                        If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                            dsEqpmntType = objCommonUI.pub_GetEquipmentType(str_013EqType, CInt(objCommon.GetHeadQuarterID()))
                        Else
                            dsEqpmntType = objCommonUI.pub_GetEquipmentType(str_013EqType, intDepotID)
                        End If

                        If dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                            drPreAdviceInfo.Item(PreAdviceData.PR_ADVC_ID) = CommonWeb.GetNextIndex(dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE), PreAdviceData.PR_ADVC_ID)
                            drPreAdviceInfo.Item(PreAdviceData.EQPMNT_TYP_ID) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                            drPreAdviceInfo.Item(PreAdviceData.EQPMNT_TYP_CD) = str_013EqType
                            drPreAdviceInfo.Item(PreAdviceData.ENTRD_DT) = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                        End If
                    End If
                End If
                dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE).Rows.Add(drPreAdviceInfo)
                ifgPreAdvice.AllowSearch = False
            Else
                For Each dr As DataRow In dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE).Rows
                    dr.Item(PreAdviceData.ENTRD_DT) = CDate(dr.Item(PreAdviceData.ENTRD_DT)).ToString("dd-MMM-yyyy").ToUpper
                Next
            End If
            If e.Parameters("Mode") = "ReBind" Then
                'dsPreAdvice = CType(RetrieveData(PREADVICE), PreAdviceDataSet)
                dsPreAdvice = CType(pub_RetrieveData(PREADVICE), PreAdviceDataSet)
                Dim lngRepairEstimateId As Long = 0
                Dim intFilesCount As Integer = 0
                Dim drRepairCompletion As DataRow() = Nothing
                If Not e.Parameters("RepairEstimateId") Is Nothing Then
                    lngRepairEstimateId = CLng(e.Parameters("RepairEstimateId"))
                    drRepairCompletion = dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE).Select(String.Concat(PreAdviceData.PR_ADVC_ID, " = ", lngRepairEstimateId))
                    If drRepairCompletion.Length > 0 Then
                        intFilesCount = CInt(dsPreAdvice.Tables(PreAdviceData._ATTACHMENT).Compute(String.Concat("COUNT(", RepairCompletionData.RPR_ESTMT_ID, ")"), String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = '", lngRepairEstimateId, "'")))
                        drRepairCompletion(0).Item(RepairCompletionData.COUNT_ATTACH) = intFilesCount
                    End If
                End If
                If intFilesCount = 0 Then
                    ' dsPreAdvice.Tables(PreAdviceData._ATTACHMENT).Clear()
                End If
            End If

            str_054GWS = objCommonConfig.pub_GetConfigSingleValue("054", intDepotID)
            bln_054GWSActive_Key = objCommonConfig.IsKeyExists
            Dim objCommondata As New CommonData
            If str_054GWS Then
                'objCommondata.SetGridVisibilitybyIndex(ifgPreAdvice, "show", 3, "True")
                'objCommondata.SetGridVisibilitybyIndex(ifgPreAdvice, "hide", 5, "False")
                'objCommondata.SetGridVisibilitybyIndex(ifgPreAdvice, "hide", 4, "False")
                'e.Row.Cells(3).Style.Add("display", "block")
                'e.Row.Cells(4).Style.Add("display", "none")
                'e.Row.Cells(5).Style.Add("display", "none")
            Else
                'objCommondata.SetGridVisibilitybyIndex(ifgPreAdvice, "hide", 7, "False")
                'objCommondata.SetGridVisibilitybyIndex(ifgPreAdvice, "hide", 6, "False")
                'objCommondata.SetGridVisibilitybyIndex(ifgPreAdvice, "hide", 3, "False")
            End If
            dtPreAdvice = dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE)
            e.DataSource = dtPreAdvice
            CacheData(PREADVICE, dsPreAdvice)
            pub_CacheData(PREADVICE, dsPreAdvice)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "OnCallBack"

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "ValidateEquipment"
                    pvt_ValidateEquipment(e.GetCallbackValue("EquipmentId"), CInt(e.GetCallbackValue("GridIndex")), e.GetCallbackValue("RowState"))
                Case "UpdatePreAdvice"
                    updatePreAdvice(e.GetCallbackValue("WFDATA"))
                Case "ValidatePreviousActivityDate"
                    pvt_ValidatePreviousActivityDate(e.GetCallbackValue("EquipmentNo"), _
                                                     e.GetCallbackValue("EventDate"))
                Case "GetEquipmentCode"
                    pvt_GetEquipmentCode(e.GetCallbackValue("Type"))

            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateEquipment"
    Private Sub pvt_ValidateEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_intGridIndex As Integer, ByVal bv_strRowstate As String)
        Try
            Dim strCustomer As String = String.Empty
            Dim blndsValid As Boolean
            Dim blnRental As Boolean
            Dim blnAllowRental As Boolean
            Dim blnDuplicateEquipment As Boolean = True
            Dim blnStatusOfEquipment As Boolean = True
            Dim objCommon As New CommonData
            Dim objPreAdvice As New PreAdvice
            Dim dsSupplierDetails As New PreAdviceDataSet
            dsPreAdvice = CType(RetrieveData(PREADVICE), PreAdviceDataSet)
            dtPreAdvice = dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE)
            ' ''Checking whether the entered code is available in grid
            Dim intResultIndex() As System.Data.DataRow = dtPreAdvice.Select(String.Concat(PreAdviceData.EQPMNT_NO, "='", bv_strEquipmentNo, "' "))
            Dim strExistEquipment As String = ""
            If intResultIndex.Length > 0 Then
                If dtPreAdvice.Rows.Count > bv_intGridIndex Then
                    If dtPreAdvice.Rows(bv_intGridIndex).RowState = Data.DataRowState.Deleted Then
                        strExistEquipment = String.Empty
                    ElseIf dtPreAdvice.Rows(bv_intGridIndex).RowState <> Data.DataRowState.Deleted Then
                        strExistEquipment = dtPreAdvice.Rows(bv_intGridIndex)(PreAdviceData.EQPMNT_NO).ToString
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

            If blndsValid = False Then
                For Each drGateIn As DataRow In dtPreAdvice.Select(String.Concat(PreAdviceData.EQPMNT_NO, "='", bv_strEquipmentNo, "'"))
                    strCustomer = CStr(drGateIn.Item(PreAdviceData.CSTMR_CD))
                Next
            End If

            If blndsValid = True Then
                blndsValid = objPreAdvice.pub_ValidateEquipmentNoByDepotID(bv_strEquipmentNo, CInt(objCommon.GetDepotID()), strCustomer)
            End If

            blnRental = objPreAdvice.GetRentalDetails(bv_strEquipmentNo, CInt(objCommon.GetDepotID()), strCustomer, blnAllowRental)

            dsSupplierDetails = objPreAdvice.pub_GetSupplierDetails(bv_strEquipmentNo, CInt(objCommon.GetDepotID()))
            If dsSupplierDetails.Tables(GateinData._V_SUPPLIER_EQUIPMENT_DETAIL).Rows.Count > 0 Then
                pub_SetCallbackReturnValue("EquipmentTypeId", dsSupplierDetails.Tables(GateinData._V_SUPPLIER_EQUIPMENT_DETAIL).Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString)
                pub_SetCallbackReturnValue("EquipmentTypeCode", dsSupplierDetails.Tables(GateinData._V_SUPPLIER_EQUIPMENT_DETAIL).Rows(0).Item(GateinData.EQPMNT_TYP_CD).ToString)
            Else
                pub_SetCallbackReturnValue("EquipmentTypeId", "") 'Added by Sakthivel on 14-OCT-2014 for Pre Advice showing Important Message
                pub_SetCallbackReturnValue("EquipmentTypeCode", "") 'Added by Sakthivel on 14-OCT-2014 for Pre Advice showing Important Message
            End If

            'Code here to check whether this Equpt no is used for Pre advice in any other Depot start here---
            ''        
            If blndsValid = True Then
                blnDuplicateEquipment = objPreAdvice.pub_ValidateEquipmentNoInPreAdvice(bv_strEquipmentNo, CInt(objCommon.GetDepotID()))
                If blnDuplicateEquipment = False Then
                    'Newly added if Equiment No already available in Preadvice in some other depot                
                    pub_SetCallbackReturnValue("EquipmentNoInAnotherDepot", "false")
                End If
                blnStatusOfEquipment = objPreAdvice.pub_ValidateStatusOfEquipment(bv_strEquipmentNo, CInt(objCommon.GetDepotID()))
                If blnStatusOfEquipment = False Then
                    'Newly added if Equiment No already available in Preadvice in some other depot                
                    pub_SetCallbackReturnValue("StatusOfEquipment", "false")
                End If
            End If           
            ''--Ends here

            If blndsValid = True Then
                If blnRental = False Then
                    pub_SetCallbackReturnValue("Customer", strCustomer)
                    pub_SetCallbackReturnValue("bRentalNotExists", "false")
                    pub_SetCallbackReturnValue("AllowRental", blnAllowRental)
                    pub_SetCallbackStatus(True)
                Else
                    pub_SetCallbackReturnValue("bNotExists", "true")
                End If
            Else                
                If blnRental = True Then
                    pub_SetCallbackReturnValue("bRentalNotExists", "true")
                    pub_SetCallbackReturnValue("Customer", strCustomer)
                    pub_SetCallbackReturnValue("bNotExists", "false")
                Else
                    pub_SetCallbackReturnValue("Customer", strCustomer)
                    pub_SetCallbackReturnValue("bRentalNotExists", "false")
                    pub_SetCallbackReturnValue("AllowRental", blnAllowRental)
                    pub_SetCallbackStatus(True)
                End If
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "updatePreAdvice"
    Private Sub updatePreAdvice(ByVal bv_strWFData As String)
        Try
            Dim objPreAdvice As New PreAdvice
            Dim objCommondata As New CommonData
            dtPreAdvice = CType(ifgPreAdvice.DataSource, DataTable)
            dsPreAdvice = CType(RetrieveData(PREADVICE), PreAdviceDataSet)
            'objPreAdvice.pub_UpdatePreAdvice(CType(dtPreAdvice.DataSet, PreAdviceDataSet), _
            '                                 objCommondata.GetCurrentUserName(), CDate(objCommondata.GetCurrentDate()), _
            '                                 CInt(objCommondata.GetDepotID()))

            objPreAdvice.pub_UpdatePreAdvice(dsPreAdvice, _
                                            objCommondata.GetCurrentUserName(), CDate(objCommondata.GetCurrentDate()), _
                                            CInt(objCommondata.GetDepotID()))
            dtPreAdvice.AcceptChanges()
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender"

    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/PreAdvice.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgPreAdvice_RowDeleting"

    Protected Sub ifgPreAdvice_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgPreAdvice.RowDeleting
        Try
            Dim ObjEquipInfo As New EquipmentInformation()
            Dim objCommon As New CommonData
            ifgPreAdvice.AllowSearch = True
            Dim lngEquipmentID As Int64 = e.InputParamters("PreAdviceID").ToString()
            Dim strEquipmentNo As String = e.InputParamters("EquipmentNo").ToString()
            Dim strGI_TransactionNo As String = e.InputParamters("GI_TransactionNo").ToString()

            If strGI_TransactionNo = "" Then
                If strEquipmentNo <> Nothing Then
                    e.OutputParamters("Success") = String.Concat("Equipment : ", strEquipmentNo, " has been deleted from Pre-Advice. Click submit to save changes.")
                End If
            Else
                e.OutputParamters("Error") = String.Concat("Equipment : ", strEquipmentNo, " cannot be deleted from Pre-Advice as it has been Invoiced (or) crossed Gate In.")
                e.Cancel = True
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgPreAdvice_RowDeleted"

    Protected Sub ifgPreAdvice_RowDeleted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeletedEventArgs) Handles ifgPreAdvice.RowDeleted
        Try
            dsPreAdvice = CType(RetrieveData(PREADVICE), PreAdviceDataSet)
            Dim dtPreAdvice As New DataTable
            dtPreAdvice = dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE).Copy()
            dtPreAdvice.AcceptChanges()

            If dtPreAdvice.Rows.Count = 0 Then
                Dim objCommon As New CommonData
                Dim intDepotID As Integer = objCommon.GetDepotID()
                Dim drPreAdviceInfo As DataRow = dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE).NewRow()

                Dim objCommonUI As New CommonUI()
                Dim dsEqpmntType As New CommonUIDataSet
                Dim objCommonConfig As New ConfigSetting()

                str_013EqType = objCommonConfig.pub_GetConfigSingleValue("013", intDepotID)
                bln_013EqType_Key = objCommonConfig.IsKeyExists

                If bln_013EqType_Key Then
                    If Not str_013EqType = "" Then
                        dsEqpmntType = objCommonUI.pub_GetEquipmentType(str_013EqType, intDepotID)
                        If dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                            drPreAdviceInfo.Item(PreAdviceData.PR_ADVC_ID) = CommonWeb.GetNextIndex(dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE), PreAdviceData.PR_ADVC_ID)
                            drPreAdviceInfo.Item(PreAdviceData.EQPMNT_TYP_ID) = dsEqpmntType.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                            drPreAdviceInfo.Item(PreAdviceData.EQPMNT_TYP_CD) = str_013EqType
                            drPreAdviceInfo.Item(PreAdviceData.ENTRD_DT) = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                        End If
                    End If
                End If
                dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE).Rows.Add(drPreAdviceInfo)
                ifgPreAdvice.AllowSearch = False
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgPreAdvice_RowDataBound"

    Protected Sub ifgPreAdvice_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgPreAdvice.RowDataBound
        Try
            Dim objCommonGWS As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDepotID As Integer = objCommonGWS.GetDepotID()
            str_054GWS = objCommonConfig.pub_GetConfigSingleValue("054", intDepotID)
            bln_054GWSActive_Key = objCommonConfig.IsKeyExists
            Dim objCommondata As New CommonData
            If str_054GWS Then
                ifgPreAdvice.Columns.Item(3).Visible = True
                'e.Row.Cells(3).Style.Add("visibility", "visible")
                'e.Row.Cells(4).Style.Add("display", "none")
                'e.Row.Cells(5).Style.Add("display", "none")
                ifgPreAdvice.Columns.Item(4).Visible = False
                ifgPreAdvice.Columns.Item(5).Visible = False
            Else
                ifgPreAdvice.Columns.Item(3).Visible = False
                ifgPreAdvice.Columns.Item(7).Visible = False
                ifgPreAdvice.Columns.Item(6).Visible = False
                'e.Row.Cells(3).Style.Add("display", "none")
                'e.Row.Cells(7).Style.Add("display", "none")
                'e.Row.Cells(6).Style.Add("display", "none")
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim datControl As iDate
                If Not e.Row.DataItem Is Nothing Then
                    ' Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        datControl = CType(DirectCast(DirectCast(e.Row.Cells(8),  _
                                   iFgFieldCell).ContainingField,  _
                                   DateField).iDate, iDate)
                        If Not CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell) Is Nothing Then
                            datControl.Text = CType(e.Row.Cells(8), iFgFieldCell).Text
                        Else
                            datControl.Text = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                        End If
                        datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                    ElseIf drv.Row.RowState = DataRowState.Added Or drv.Row.RowState = DataRowState.Modified Then
                        Dim objCommon As New CommonData
                        Dim objPreAdvice As New PreAdvice
                        Dim dsSupplierDetails As New PreAdviceDataSet
                        dsSupplierDetails = objPreAdvice.pub_GetSupplierDetails(drv.Item(PreAdviceData.EQPMNT_NO).ToString, CInt(objCommon.GetDepotID()))
                        If dsSupplierDetails.Tables(PreAdviceData._V_SUPPLIER_EQUIPMENT_DETAIL).Rows.Count > 0 Then
                            CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        End If
                    End If

                End If
                Dim hypPhotoUpload As Image
                Dim PreAdviceId As String = drv(PreAdviceData.PR_ADVC_ID).ToString
                hypPhotoUpload = CType(e.Row.Cells(10).Controls(0), Image)
                hypPhotoUpload.Attributes.Add("onclick", "showPhotoUpload('" + e.Row.RowIndex.ToString + "','" + PreAdviceId + "' );return false;")
                hypPhotoUpload.ToolTip = "Attach Files"
                hypPhotoUpload.ImageUrl = "../Images/attachment.png"
                hypPhotoUpload.Attributes.Add("style", "cursor:pointer;margin-left:5px;")

                Dim imgFileUpload As Image
                ' imgFileUpload = CType(e.Row.Cells(7).Controls(0), Image)
                imgFileUpload = CType(e.Row.Cells(10).Controls(0), Image)
                imgFileUpload.ToolTip = "Attach Files"
                If Not IsDBNull(drv.Item(PreAdviceData.COUNT_ATTACH)) Then
                    If Not CInt(drv.Item(PreAdviceData.COUNT_ATTACH)) > 0 Then
                        imgFileUpload.ImageUrl = "../Images/noattachment.png"
                    Else
                        imgFileUpload.ImageUrl = "../Images/attachment.png"
                    End If
                Else
                    imgFileUpload.ImageUrl = "../Images/noattachment.png"
                End If
                If e.Row.RowIndex > 6 Then
                    Dim lkpCustomer As iLookup
                    lkpCustomer = CType(DirectCast(DirectCast(e.Row.Cells(0), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpCustomer.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom

                    Dim lkpType As iLookup
                    lkpType = CType(DirectCast(DirectCast(e.Row.Cells(2), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpType.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom

                    Dim lkpCargo As iLookup
                    lkpCargo = CType(DirectCast(DirectCast(e.Row.Cells(4), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpCargo.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidatePreviousActivityDate"
    Private Sub pvt_ValidatePreviousActivityDate(ByVal bv_strEquipmentNo As String, ByVal bv_strEventDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objGateIn As New Gatein
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData
            Dim dsCommonUI As New CommonUIDataSet
            blnDateValid = objCommonUI.pub_GetEquipmentPreviousActivityDate(bv_strEquipmentNo, _
                                                                          CDate(bv_strEventDate), _
                                                                          dtPreviousDate, _
                                                                          "Pre-Advice", _
                                                                          CInt(objCommon.GetDepotID()))
            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Equipment's Activity Date Should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "")
        Catch ex As Exception

            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "ifgPreAdvice_RowInserting"
    Protected Sub ifgPreAdvice_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgPreAdvice.RowInserting
        Try
            Dim lng As Long
            Dim objCommon As New CommonData
            Dim objPreAdvice As New PreAdvice
            'dsPreAdvice = objPreAdvice.pub_PreAdviceGetPreAdvicebyDepotIDAttchemnt(objCommon.GetDepotID())
            dsPreAdvice = CType(RetrieveData(PREADVICE), PreAdviceDataSet)
            lng = CommonWeb.GetNextIndex(dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE_ATTACHMENT), PreAdviceData.PR_ADVC_ID)

            Dim dr As DataRow = dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE_ATTACHMENT).NewRow()
            dr.Item(PreAdviceData.EQPMNT_NO) = e.Values(PreAdviceData.EQPMNT_NO)
            dr.Item(PreAdviceData.PR_ADVC_ID) = lng
            dsPreAdvice.Tables(PreAdviceData._V_PRE_ADVICE_ATTACHMENT).Rows.Add(dr)
            e.Values(PreAdviceData.PR_ADVC_ID) = lng
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

    Private Sub pvt_GetEquipmentCode(ByVal bv_strEquipTypeCode As String)
        Try
            Dim objEquipType As New EquipmentTypes
            Dim objCommonGWS As New CommonData
            Dim intDepotID As Integer = objCommonGWS.GetDepotID()
            Dim dsEquipType As New DataSet
            dsEquipType = objEquipType.GetEquipmentTypeByEquipmentTypeCode(bv_strEquipTypeCode, intDepotID)
            pub_SetCallbackReturnValue("Code", dsEquipType.Tables(0).Rows(0).Item(EquipmentTypeData.EQPMNT_CD_CD))
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                       MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub



End Class