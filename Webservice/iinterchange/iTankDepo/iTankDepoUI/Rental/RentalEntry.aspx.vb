Option Strict On
Partial Class Rental_RentalEntry
    Inherits Pagebase

#Region "Declarations"
    Private strMSGUPDATE As String = "Rental Entry : Equipment(s) Updated Successfully"
    Private strMSGDUPLICATE As String = "This combination already exists."
    Private Const RENTAL_ENTRY As String = "RENTAL_ENTRY"
    Private Const RENTAL_OTHERCHARGE As String = "RENTAL_OTHERCHARGE"
    Private Const RentalMode As String = "RentalMode"
#End Region

#Region "Parameters"
    Public dsRentalEntry As New RentalEntryDataSet
    Public dtRental As DataTable
    Dim intDepotId As Integer
    Private pvt_strTableKey As String
    Private pvt_strTableName As String
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim objCommondata As New CommonData
                If objCommondata.GetCurrentPageMode = MODE_EDIT Then
                    'ifgRentalEntry.Columns.Item(1).AllowSearch = True
                    'ifgRentalEntry.Columns.Item(3).AllowSearch = True
                    'ifgRentalEntry.Columns.Item(4).AllowSearch = True
                    'ifgRentalEntry.Columns.Item(5).AllowSearch = True
                    'ifgRentalEntry.Columns.Item(7).AllowSearch = True
                End If
                Dim strSessionId As String = objCommondata.GetSessionID()
                Dim strActivityName As String = String.Empty
                If Not pub_GetQueryString("activityname") Is Nothing Then
                    strActivityName = pub_GetQueryString("activityname")
                End If
                objCommondata.FlushLockDataByActivityName(RentalEntryData.EQPMNT_NO, strSessionId, strActivityName)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Rental/RentalEntry.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "UpdateRental"
                    UpdateRental(e.GetCallbackValue("WFDATA"), _
                                  e.GetCallbackValue("ActivityName"))
                Case "DeleteRental"
                    DeleteRental(e.GetCallbackValue("WFDATA"), _
                                 e.GetCallbackValue("ActivityName"))
                Case "DeleteRentalEquipment"
                    DeleteRentalEquipment(e.GetCallbackValue("WFDATA"))
                Case "ValidateOnHireOffHireDate"
                    ValidateOnHireOffHireDate(CStr(e.GetCallbackValue("RentalID")), _
                                              CStr(e.GetCallbackValue("EquipmentNo")), _
                                              CStr(e.GetCallbackValue("GateOutDate")), _
                                              CStr(e.GetCallbackValue("ContractStartDate")), _
                                              CStr(e.GetCallbackValue("ActivityDate")), _
                                              CStr(e.GetCallbackValue("GateInDate")), _
                                              CStr(e.GetCallbackValue("Activity")))
                Case "RecordLockData"
                    pvt_GetLockData(CBool(e.GetCallbackValue("checkBit")), _
                                    e.GetCallbackValue("EquipmentNo"), _
                                    e.GetCallbackValue("ActivityName"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ValidateOnHireOffHireDate"
    Public Sub ValidateOnHireOffHireDate(ByVal bv_RentalID As String, _
                                         ByVal bv_EquipmentNo As String, _
                                         ByVal bv_datGateOutDate As String, _
                                         ByVal bv_datContractStartDate As String, _
                                         ByVal bv_datActivityDate As String, _
                                         ByVal bv_datGateInDate As String, _
                                         ByVal bv_strActivity As String)
        Try
            If bv_strActivity = "ONHIRE" Then
                Dim objRental As New RentalEntry
                Dim dtRental As New DataTable
                dtRental = objRental.GetOffHireDate(CInt(bv_RentalID), bv_EquipmentNo).Tables(RentalEntryData._V_RENTAL_ENTRY)
                If dtRental.Rows.Count > 0 Then
                    If CDate(bv_datActivityDate) < CDate(dtRental.Rows(0).Item(RentalEntryData.OFF_HR_DT)) Then
                        pub_SetCallbackStatus(False)
                        pub_SetCallbackReturnValue("GateInDate", CDate(dtRental.Rows(0).Item(RentalEntryData.OFF_HR_DT)).ToString("dd-MMM-yyyy").ToUpper)
                        pub_SetCallbackReturnValue("GateInDateError", "True")
                        Exit Sub
                    End If
                End If
                If CDate(bv_datActivityDate) > CDate(bv_datGateOutDate) Then
                    pub_SetCallbackStatus(False)
                    pub_SetCallbackReturnValue("GateOutDate", CDate(bv_datGateOutDate).ToString("dd-MMM-yyyy").ToUpper)
                    pub_SetCallbackReturnValue("GateError", "True")
                    Exit Sub
                ElseIf CDate(bv_datActivityDate) < CDate(bv_datContractStartDate) Then
                    pub_SetCallbackStatus(False)
                    pub_SetCallbackReturnValue("CotnractStartDate", CDate(bv_datContractStartDate).ToString("dd-MMM-yyyy").ToUpper)
                    pub_SetCallbackReturnValue("ContractDateError", "True")
                    Exit Sub
                End If
            ElseIf bv_strActivity = "OFFHIRE" Then
                If CDate(bv_datActivityDate) < CDate(bv_datGateInDate) Then
                    pub_SetCallbackStatus(False)
                    pub_SetCallbackReturnValue("GateInDate", CDate(bv_datGateInDate).ToString("dd-MMM-yyyy").ToUpper)
                    pub_SetCallbackReturnValue("GateInError", "True")
                    Exit Sub
                End If
            End If

            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "UpdateRental"
    Public Sub UpdateRental(ByVal bv_strWFdata As String, _
                            ByVal bv_strActivityName As String)
        Try
            Dim objRental As New RentalEntry
            Dim objCommondata As New CommonData
            Dim dtRental As DataTable
            Dim strRentalExist As String = String.Empty
            Dim dtDefaultRental As New DataTable
            Dim dsOtherCharge As New DataSet
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Clear()
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Clear()
            dsRentalEntry = CType(RetrieveData(RENTAL_ENTRY), RentalEntryDataSet)
            dsOtherCharge = CType(RetrieveData(RENTAL_OTHERCHARGE), RentalEntryDataSet)
            dtRental = CType(ifgRentalEntry.DataSource, DataTable)
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Merge(dtRental)
            If objCommondata.GetMultiLocationSupportConfig.ToLower = "true" Then
                dtDefaultRental = objRental.getDefaultRates(CInt(objCommondata.GetHeadQuarterID())).Tables(RentalEntryData._DEFAULT_ADDITIONAL_RATE)
            Else
                dtDefaultRental = objRental.getDefaultRates(CInt(objCommondata.GetDepotID())).Tables(RentalEntryData._DEFAULT_ADDITIONAL_RATE)
            End If

            dsRentalEntry.Tables(RentalEntryData._DEFAULT_ADDITIONAL_RATE).Merge(dtDefaultRental)
            If Not (dsOtherCharge) Is Nothing Then
                dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(dsOtherCharge.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE))
            Else
                'pub_SetCallbackStatus(False)
                'pub_SetCallbackError("Additional charge default rates shoulb be saved before submit")
                'Exit Sub
            End If
            Dim drRental As DataRow()
            drRental = dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Select(RentalEntryData.CHECKED & "='True'")
            If Not drRental.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
            End If

            ''22375
            Dim strRentalReferenceNos As String = ""
            Dim strErrorMessage As String = ""
            For Each drRental1 As DataRow In dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Select(RentalEntryData.CHECKED & "='True'")
                If strRentalReferenceNos <> String.Empty Then
                    strRentalReferenceNos = String.Concat(strRentalReferenceNos, ",'", drRental1.Item("RNTL_RFRNC_NO"), "'")
                Else
                    strRentalReferenceNos = String.Concat("'", drRental1.Item("RNTL_RFRNC_NO"), "'")
                End If
            Next
            Dim objCommonUI As New CommonUI
            objCommonUI.pub_validateFinalizedInvoiceActivityWise(CommonUIData._RENTAL_CHARGE, CInt(objCommondata.GetDepotID()), "", strRentalReferenceNos, "", strErrorMessage, dsRentalEntry)
            If strErrorMessage <> "" Then
                pub_SetCallbackError("This action will not be allowed since one of the equipment(s) rental invoice is already finalized.")
                pub_SetCallbackStatus(False)
                Exit Sub
            End If
            ''
            objRental.UpdateRental(dsRentalEntry, _
                               objCommondata.GetCurrentUserName(), _
                               CDate(objCommondata.GetCurrentDate()), _
                               CInt(objCommondata.GetDepotID()), _
                               strRentalExist)
            Dim strCurrentSessionId As String = String.Empty
            strCurrentSessionId = GetSessionID()
            For Each drLock As DataRow In dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Select(RentalEntryData.CHECKED & "='True'")
                objCommondata.FlushLockData(HeatingData.EQPMNT_NO, CStr(drLock.Item(RentalEntryData.EQPMNT_NO)), strCurrentSessionId, bv_strActivityName)
            Next
            CacheData(RENTAL_OTHERCHARGE, dsRentalEntry)
            dtRental.AcceptChanges()
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            pub_SetCallbackReturnValue("RentalExistEquipmentNo", strRentalExist)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "DeleteRentalEquipment"
    Public Sub DeleteRentalEquipment(ByVal bv_strWFdata As String)
        Try
            Dim objRental As New RentalEntry
            Dim objCommondata As New CommonData
            Dim dtRental As DataTable
            Dim dtDefaultRental As New DataTable
            Dim dsOtherCharge As New DataSet
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Clear()
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Clear()
            dsRentalEntry = CType(RetrieveData(RENTAL_ENTRY), RentalEntryDataSet)
            dsOtherCharge = CType(RetrieveData(RENTAL_OTHERCHARGE), RentalEntryDataSet)
            dtRental = CType(ifgRentalEntry.DataSource, DataTable)
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Merge(dtRental)
            If objCommondata.GetMultiLocationSupportConfig.ToLower = "true" Then
                dtDefaultRental = objRental.getDefaultRates(CInt(objCommondata.GetHeadQuarterID())).Tables(RentalEntryData._DEFAULT_ADDITIONAL_RATE)
            Else
                dtDefaultRental = objRental.getDefaultRates(CInt(objCommondata.GetDepotID())).Tables(RentalEntryData._DEFAULT_ADDITIONAL_RATE)
            End If

            dsRentalEntry.Tables(RentalEntryData._DEFAULT_ADDITIONAL_RATE).Merge(dtDefaultRental)
            If Not (dsOtherCharge) Is Nothing Then
                dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(dsOtherCharge.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE))
            Else
            End If
            objRental.DeleteRental(dsRentalEntry, _
                                  objCommondata.GetCurrentUserName(), _
                                  CDate(objCommondata.GetCurrentDate()), _
                                  CInt(objCommondata.GetDepotID()))
            CacheData(RENTAL_OTHERCHARGE, dsRentalEntry)
            dtRental.AcceptChanges()
            pub_SetCallbackReturnValue("Message", "Rental Entry : Equipment(s) Deleted Successfully")
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "DeleteRental"
    Public Sub DeleteRental(ByVal bv_strWFdata As String, ByVal bv_strActivityName As String)
        Try
            Dim objRental As New RentalEntry
            Dim objCommondata As New CommonData
            Dim dtRental As DataTable
            Dim dtDefaultRental As New DataTable
            Dim dsOtherCharge As New DataSet
            Dim blnLockBit As Boolean = False
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Clear()
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Clear()
            dsRentalEntry = CType(RetrieveData(RENTAL_ENTRY), RentalEntryDataSet)
            dsOtherCharge = CType(RetrieveData(RENTAL_OTHERCHARGE), RentalEntryDataSet)
            dtRental = CType(ifgRentalEntry.DataSource, DataTable)
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Merge(dtRental)
            If objCommondata.GetMultiLocationSupportConfig.ToLower = "true" Then
                dtDefaultRental = objRental.getDefaultRates(CInt(objCommondata.GetHeadQuarterID())).Tables(RentalEntryData._DEFAULT_ADDITIONAL_RATE)
            Else
                dtDefaultRental = objRental.getDefaultRates(CInt(objCommondata.GetDepotID())).Tables(RentalEntryData._DEFAULT_ADDITIONAL_RATE)
            End If

            dsRentalEntry.Tables(RentalEntryData._DEFAULT_ADDITIONAL_RATE).Merge(dtDefaultRental)
            If Not (dsOtherCharge) Is Nothing Then
                dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(dsOtherCharge.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE))
            Else
            End If
            Dim drRental As DataRow()
            drRental = dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Select(RentalEntryData.CHECKED & "='True'")
            If Not drRental.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
            End If
            For Each drRentalEntry As DataRow In dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Select(RentalEntryData.CHECKED & "='True'")
                If Not (drRentalEntry.Item(RentalEntryData.ON_HR_DT) Is DBNull.Value) Then
                    pub_SetCallbackStatus(False)
                    pub_SetCallbackError(String.Concat("Rental Entry cannot be deleted for Equipment No (", drRentalEntry.Item(RentalEntryData.EQPMNT_NO), ")"))
                    blnLockBit = True
                    Exit Sub
                End If
            Next

            If blnLockBit = False Then
                Dim strCurrentSessionId As String = String.Empty
                strCurrentSessionId = GetSessionID()
                For Each drLock As DataRow In dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Select(RentalEntryData.CHECKED & "='True'")
                    objCommondata.FlushLockData(RentalEntryData.EQPMNT_NO, CStr(drLock.Item(RentalEntryData.EQPMNT_NO)), strCurrentSessionId, bv_strActivityName)
                Next
            End If

            'objRental.DeleteRental(dsRentalEntry, _
            '                       objCommondata.GetCurrentUserName(), _
            '                       CDate(objCommondata.GetCurrentDate()), _
            '                       CInt(objCommondata.GetDepotID()))
            'CacheData(RENTAL_OTHERCHARGE, dsRentalEntry)
            'dtRental.AcceptChanges()
            'pub_SetCallbackReturnValue("Message", "Rental Entry : Equipment(s) Deleted Successfully")
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRentalEntry_ClientBind"
    Protected Sub ifgRentalEntry_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgRentalEntry.ClientBind
        Try
            Dim objRentalEntry As New RentalEntry
            Dim dsEquipment As DataSet
            Dim dsRental As DataSet
            Dim dsSupplier As DataSet
            Dim objCommon As New CommonData
            Dim dtEqpmntNo As New DataTable
            Dim dtGateOutEquipment As New DataTable
            Dim dtAdditionalCharge As New DataTable
            Dim dtOtherCharge As New DataTable
            Dim drRentalEntry As DataRow
            Dim dtRentalEntry As New DataTable
            Dim rentalID As Integer = 0
            Dim lngRentalId As Long = 0
            Dim intHeadQuartersID As Integer = CInt(objCommon.GetDepotID())
            intDepotId = CommonWeb.iInt(objCommon.GetDepotID())
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                intHeadQuartersID = CInt(objCommon.GetHeadQuarterID())
            End If
            dtOtherCharge = objRentalEntry.pub_GetRentalOtherCharges().Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE)
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Clear()
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(dtOtherCharge)
            dtAdditionalCharge = objRentalEntry.pub_GetAdditionalChargeRateByDepotId(intHeadQuartersID).Tables(RentalEntryData._V_ADDITIONAL_CHARGE_RATE)
            dsRentalEntry.Tables(RentalEntryData._V_ADDITIONAL_CHARGE_RATE).Merge(dtAdditionalCharge)
            If Not e.Parameters("Mode") Is Nothing Then
                Dim strSessionId As String = objCommon.GetSessionID()
                Dim strActivityName As String = String.Empty
                If Not e.Parameters("ActivityName") Is Nothing Then
                    strActivityName = e.Parameters("ActivityName").ToString()
                End If
                objCommon.FlushLockDataByActivityName(RentalEntryData.EQPMNT_NO, strSessionId, strActivityName)
                hdnMode.Value = e.Parameters("Mode").ToString()
                objCommon.SetCurrentPageMode(e.Parameters("Mode").ToString())
                Select Case e.Parameters("Mode").ToString()
                    Case MODE_NEW
                        dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Clear()
                        dsRentalEntry.Tables(RentalEntryData._EQUIPMENT_INFORMATION).Rows.Clear()
                        dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Rows.Clear()
                        dtRentalEntry = dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Clone()
                        dsEquipment = objRentalEntry.getEqpmntNoFromEquipmentInfo(intDepotId)
                        dsRentalEntry.Merge(dsEquipment.Tables(RentalEntryData._EQUIPMENT_INFORMATION))
                        dsSupplier = objRentalEntry.getSupplierEquipment(intHeadQuartersID)
                        For Each dr As DataRow In dsRentalEntry.Tables(RentalEntryData._EQUIPMENT_INFORMATION).Rows
                            drRentalEntry = dtRentalEntry.NewRow()
                            drRentalEntry.Item(RentalEntryData.EQPMNT_INFRMTN_ID) = dr.Item(RentalEntryData.EQPMNT_INFRMTN_ID)
                            If Not dr.Item(RentalEntryData.RNTL_ENTRY_ID) Is DBNull.Value Then
                                drRentalEntry.Item(RentalEntryData.RNTL_ENTRY_ID) = dr.Item(RentalEntryData.RNTL_ENTRY_ID)
                                rentalID = CInt(dr.Item(RentalEntryData.RNTL_ENTRY_ID))
                                lngRentalId = rentalID
                            Else
                                drRentalEntry.Item(RentalEntryData.RNTL_ENTRY_ID) = rentalID + 1000000 + CommonWeb.GetNextIndex(dtRentalEntry, RentalEntryData.RNTL_ENTRY_ID)
                                lngRentalId = CLng(drRentalEntry.Item(RentalEntryData.RNTL_ENTRY_ID))

                            End If
                            Dim drSupplierEquipment As DataRow()
                            drSupplierEquipment = dsSupplier.Tables(RentalEntryData._SUPPLIER_EQUIPMENT).Select(String.Concat(RentalEntryData.EQPMNT_NO, "='", dr.Item(RentalEntryData.EQPMNT_NO), "'"))
                            pvt_BindTransportationDetailRate(lngRentalId)
                            'If drSupplierEquipment.Length > 0 Then
                            '    Dim drEquipmentNo As DataRow()
                            '    drEquipmentNo = dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Select(String.Concat(RentalEntryData.EQPMNT_NO, "='", dr.Item(RentalEntryData.EQPMNT_NO), "'"))
                            '    If drEquipmentNo.Length > 0 Then
                            '        drRentalEntry.Item(RentalEntryData.EQPMNT_NO) = dr.Item(RentalEntryData.EQPMNT_NO)
                            '    Else
                            '        If DirectCast(drSupplierEquipment.GetValue(0), iInterchange.iTankDepo.Data.RentalEntryDataSet.SUPPLIER_EQUIPMENTRow).CNTRCT_END_DT >= CDate(DateTime.Now) Then
                            '            drRentalEntry.Item(RentalEntryData.EQPMNT_NO) = dr.Item(RentalEntryData.EQPMNT_NO)
                            '        Else

                            '        End If
                            '    End If
                            'Else
                            drRentalEntry.Item(RentalEntryData.EQPMNT_NO) = dr.Item(RentalEntryData.EQPMNT_NO)
                            drRentalEntry.Item(RentalEntryData.IS_GTN_BT) = False
                            drRentalEntry.Item(RentalEntryData.IS_GTOT_BT) = False
                            dtRentalEntry.Rows.Add(drRentalEntry)
                        Next
                        dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Clear()
                        dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY).Merge(dtRentalEntry)
                        ifgRentalEntry.ActionButtons.Item(0).Visible = False
                        ifgRentalEntry.Columns.Item(1).AllowSearch = False
                        ifgRentalEntry.Columns.Item(3).AllowSearch = False
                        ifgRentalEntry.Columns.Item(4).AllowSearch = False
                        ifgRentalEntry.Columns.Item(5).AllowSearch = False
                        ifgRentalEntry.Columns.Item(7).AllowSearch = False
                    Case MODE_EDIT
                        dsRental = objRentalEntry.getRentalEntryDetails(intDepotId)
                        dsRentalEntry.Merge(dsRental.Tables(RentalEntryData._V_RENTAL_ENTRY))
                        'ifgRentalEntry.AllowEdit = False
                        ifgRentalEntry.ActionButtons.Item(0).Visible = True
                        ifgRentalEntry.Columns.Item(1).AllowSearch = True
                        ifgRentalEntry.Columns.Item(3).AllowSearch = True
                        ifgRentalEntry.Columns.Item(4).AllowSearch = True
                        ifgRentalEntry.Columns.Item(5).AllowSearch = True
                        ifgRentalEntry.Columns.Item(7).AllowSearch = True
                End Select
                e.DataSource = dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY)
                CacheData(RENTAL_ENTRY, dsRentalEntry)
                CacheData(RENTAL_OTHERCHARGE, dsRentalEntry)
                CacheData("RentalMode", e.Parameters("Mode").ToString())
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRentalEntry_RowDataBound"
    Protected Sub ifgRentalEntry_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgRentalEntry.RowDataBound
        Try
            Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            Dim strMode As String
            strMode = CType(RetrieveData(RentalMode), String)
            CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")
            CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")

            If e.Row.RowType = DataControlRowType.DataRow Then
                If (strMode = "edit") Then
                    CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    'CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                Else
                    'CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    'CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                End If


                Dim OtherCharge As HyperLink
                OtherCharge = CType(e.Row.Cells(6).Controls(0), HyperLink)
                OtherCharge.Attributes.Add("onclick", "openOtherChargeDetail();return false;")
                OtherCharge.NavigateUrl = "#"
                OtherCharge.Text = "Add/Edit"

                Dim chkActive As iFgCheckBox
                chkActive = CType(e.Row.Cells(8).Controls(0), iFgCheckBox)
                chkActive.Attributes.Add("onClick", String.Concat("lockData(this,'", CStr(drv.Row.Item(GateinData.EQPMNT_NO)), "','", String.Empty, "' );"))

                If Not IsNothing(drv.Item(RentalEntryData.OFF_HR_DT)) AndAlso drv.Item(RentalEntryData.OFF_HR_DT).ToString <> "" Then
                    CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                Else
                    CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                End If

                If CBool(drv.Item(RentalEntryData.IS_GTOT_BT)) Or Not IsNothing(drv.Item(RentalEntryData.ON_HR_DT)) AndAlso drv.Item(RentalEntryData.ON_HR_DT).ToString <> "" Then
                    CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                Else
                    CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                End If
                If CBool(drv.Item(RentalEntryData.IS_GTN_BT)) Or Not IsNothing(drv.Item(RentalEntryData.OFF_HR_DT)) AndAlso drv.Item(RentalEntryData.OFF_HR_DT).ToString <> "" Then
                    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                Else
                    CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                End If
                If CBool(drv.Item(RentalEntryData.IS_GTOT_BT)) Or Not IsNothing(drv.Item(RentalEntryData.ON_HR_DT)) AndAlso drv.Item(RentalEntryData.ON_HR_DT).ToString <> "" Then
                    If ((Not IsNothing(drv.Item(RentalEntryData.OFF_HR_DT)) AndAlso drv.Item(RentalEntryData.OFF_HR_DT).ToString <> "")) Then
                        CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    ElseIf (Not IsNothing(drv.Item(RentalEntryData.BLLNG_FLG)) AndAlso CBool(drv.Item(RentalEntryData.BLLNG_FLG))) Then
                        CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    Else
                        CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                    End If
                Else
                    CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                End If
                Dim datControl As iDate
                datControl = CType(DirectCast(DirectCast(e.Row.Cells(5),  _
                            iFgFieldCell).ContainingField,  _
                            DateField).iDate, iDate)
                datControl.Validator.CmpErrorMessage = "Off-Hire Date cannot be greater than Current Date."
                datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                If Not IsNothing(drv.Item(RentalEntryData.ALLOW_EDIT)) AndAlso drv.Item(RentalEntryData.ALLOW_EDIT).ToString <> "" Then
                    If Not CBool(drv.Item(RentalEntryData.ALLOW_EDIT)) Then
                        CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        'CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = False
                        'CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = False
                    End If
                End If
                If e.Row.RowIndex > 6 Then
                    Dim lkpCustomer As iLookup
                    lkpCustomer = CType(DirectCast(DirectCast(e.Row.Cells(1), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpCustomer.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom

                    Dim lkpContractRef As iLookup
                    lkpContractRef = CType(DirectCast(DirectCast(e.Row.Cells(2), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                    lkpContractRef.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                End If
            End If

            If strMode <> MODE_EDIT Then
                ifgRentalEntry.ActionButtons.Item(0).Visible = False
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_BindTransportationDetailRate"
    Private Sub pvt_BindTransportationDetailRate(ByVal bv_i64RentalEntryId As Int64)
        Try
            Dim dtRentalOtherCharge As New DataTable
            Dim drTransportationDetailRate As DataRow = Nothing
            Dim lngNextIndex As Long = 0
            ' If dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows.Count = 0 Then


            dtRentalOtherCharge = dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Clone()

            If dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows.Count > 0 Then
                If dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows(dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows.Count - 1).RowState = DataRowState.Deleted Then
                    lngNextIndex = CLng(dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows(dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows.Count - 1).Item(RentalEntryData.RNTL_OTHR_CHRG_ID, DataRowVersion.Original)) + 1
                Else
                    lngNextIndex = CLng(dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows(dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Rows.Count - 1).Item(RentalEntryData.RNTL_OTHR_CHRG_ID)) + 1
                End If
            Else
                lngNextIndex = 1
            End If
            For Each drAdditionalRate As DataRow In dsRentalEntry.Tables(RentalEntryData._V_ADDITIONAL_CHARGE_RATE).Rows
                drTransportationDetailRate = dtRentalOtherCharge.NewRow()
                drTransportationDetailRate.Item(RentalEntryData.RNTL_OTHR_CHRG_ID) = lngNextIndex
                drTransportationDetailRate.Item(RentalEntryData.RNTL_ENTRY_ID) = bv_i64RentalEntryId
                drTransportationDetailRate.Item(RentalEntryData.ADDTNL_CHRG_RT_ID) = drAdditionalRate.Item(RentalEntryData.ADDTNL_CHRG_RT_ID)
                drTransportationDetailRate.Item(RentalEntryData.ADDTNL_CHRG_RT_CD) = drAdditionalRate.Item(RentalEntryData.ADDTNL_CHRG_RT_CD)
                drTransportationDetailRate.Item(RentalEntryData.RT_NC) = drAdditionalRate.Item(RentalEntryData.RT_NC)
                drTransportationDetailRate.Item(RentalEntryData.DFLT_BT) = drAdditionalRate.Item(RentalEntryData.DFLT_BT)
                dtRentalOtherCharge.Rows.Add(drTransportationDetailRate)
                lngNextIndex = lngNextIndex + 1
            Next
            dsRentalEntry.Tables(RentalEntryData._V_RENTAL_OTHER_CHARGE).Merge(dtRentalOtherCharge)
            ' End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "ifgRentalEntry_RowUpdating"
    Protected Sub ifgRentalEntry_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgRentalEntry.RowUpdating
        Try
            Dim lngID As Long
            dsRentalEntry = CType(RetrieveData(RENTAL_ENTRY), RentalEntryDataSet)
            dtRental = dsRentalEntry.Tables(RentalEntryData._V_RENTAL_ENTRY)
            lngID = CommonWeb.GetNextIndex(dtRental, RentalEntryData.EQPMNT_INFRMTN_ID)
            e.NewValues(RentalEntryData.EQPMNT_INFRMTN_ID) = lngID
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                   MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgRentalEntry_RowDeleting"
    Protected Sub ifgRentalEntry_RowDeleting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgRentalEntry.RowDeleting
        Try
            Dim strCurrentSessionId As String = String.Empty
            Dim objCommondata As New CommonData
            Dim strActivityName As String = String.Empty
            strActivityName = e.InputParamters("ActivityName").ToString()
            strCurrentSessionId = GetSessionID()
            objCommondata.FlushLockData(HeatingData.EQPMNT_NO, CStr(e.Values(RentalEntryData.EQPMNT_NO)), strCurrentSessionId, strActivityName)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetLockData() Record Locking"
    Private Sub pvt_GetLockData(ByVal bv_blnCheckBit As Boolean, _
                                ByVal bv_strEquipmentNo As String, _
                                ByVal bv_strActivityName As String)
        Try
            Dim objCommonData As New CommonData
            Dim strErrorMessage As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim strIpAddress As String = GetClientIPAddress()
            Dim strCurrentIpAddress As String = String.Empty
            Dim strUserName As String = String.Empty
            blnLockData = objCommonData.pub_GetLockData(bv_blnCheckBit, bv_strEquipmentNo, strUserName, bv_strActivityName, strIpAddress, False, RentalEntryData.EQPMNT_NO)
            If blnLockData Then
                strCurrentIpAddress = GetClientIPAddress()
                If strCurrentIpAddress = strIpAddress Then
                    pub_SetCallbackReturnValue("IPError", "true")
                Else
                    pub_SetCallbackReturnValue("IPError", "false")
                End If
            End If
            pub_SetCallbackReturnValue("UserName", strUserName)
            pub_SetCallbackReturnValue("ActivityName", bv_strActivityName)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

    Protected Sub ifgRentalEntry_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgRentalEntry.RowUpdated

    End Sub
End Class
