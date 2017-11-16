
Partial Class Operations_RepairInvoiceUpdate
    Inherits Pagebase
    Dim dsRepairInvoiceUpdate As New RepairInvoiceUpdateDataSet
    Dim dtRepairInvoiceUpdate As DataTable
    Dim objRepairInvoice As New RepairInvoiceUpdate
    Private Const REPAIR_INVOICE_UPATE As String = "REPAIR_INVOICE_UPATE"
    Private strMSGUPDATE As String = "Repair Invoice Update : Repair Charge Updated Successfully."

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim objCommondata As New CommonData
                Dim strSessionId As String = objCommondata.GetSessionID()
                Dim strActivityName As String = String.Empty
                If Not pub_GetQueryString("activityname") Is Nothing Then
                    strActivityName = pub_GetQueryString("activityname")
                End If
                objCommondata.FlushLockDataByActivityName(CleaningData.EQPMNT_NO, strSessionId, strActivityName)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/RepairInvoiceUpdate.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "UpdateRepairCharge"
                    pvt_UpdateRepairCharge(e.GetCallbackValue("Reason"), _
                                           e.GetCallbackValue("ActivityName"), _
                                           CInt(e.GetCallbackValue("ActivityId")))
                Case "ValidateCheckBox"
                    pvt_ValidateCheckBox()
                Case "RecordLockData"
                    pvt_GetLockData(CBool(e.GetCallbackValue("checkBit")), _
                                    e.GetCallbackValue("EquipmentNo"), _
                                    e.GetCallbackValue("ActivityName"))
                Case "ActivitySubmit"
                    pvt_ActivitySubmit(e.GetCallbackValue("ActivityId"))

            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
      
    End Sub
#End Region

#Region "ValidateCheckBox"
    Private Sub pvt_ValidateCheckBox()
        Try
            dsRepairInvoiceUpdate = CType(RetrieveData(REPAIR_INVOICE_UPATE), RepairInvoiceUpdateDataSet)
            Dim drARepair As DataRow()
            drARepair = dsRepairInvoiceUpdate.Tables(RepairInvoiceUpdateData._V_REPAIR_CHARGE).Select(RepairInvoiceUpdateData.CHECKED & "='True'")
            If Not drARepair.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
            Else
                pub_SetCallbackStatus(True)
            End If
        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "UpdateRepairCharge"
    Private Sub pvt_UpdateRepairCharge(ByVal bv_strReason As String, _
                                       ByVal bv_strActivityName As String, _
                                       ByVal bv_intActivityId As Integer)
        Try
            dsRepairInvoiceUpdate = CType(RetrieveData(REPAIR_INVOICE_UPATE), RepairInvoiceUpdateDataSet)
            Dim drARepair As DataRow()
            drARepair = dsRepairInvoiceUpdate.Tables(RepairInvoiceUpdateData._V_REPAIR_CHARGE).Select(RepairInvoiceUpdateData.CHECKED & "='True'")
            dtRepairInvoiceUpdate = dsRepairInvoiceUpdate.Tables(RepairInvoiceUpdateData._V_REPAIR_CHARGE).Clone()
            Dim objRepair As New RepairInvoiceUpdate
            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim strActivitySubmit As String = String.Empty

      

            objRepair.pub_UpdateRepairCharge(dsRepairInvoiceUpdate, _
                                             objCommon.GetCurrentUserName(), _
                                             CDate(objCommon.GetCurrentDate()), _
                                             bv_strReason, _
                                             intDPT_ID, _
                                             strActivitySubmit, _
                                             bv_intActivityId)
            Dim objCommondata As New CommonData
            Dim strCurrentSessionId As String = String.Empty
            strCurrentSessionId = GetSessionID()
            dsRepairInvoiceUpdate.AcceptChanges()
            For Each drLock As DataRow In dsRepairInvoiceUpdate.Tables(RepairInvoiceUpdateData._V_REPAIR_CHARGE).Select(RepairInvoiceUpdateData.CHECKED & "='True'")
                objCommondata.FlushLockData(RepairInvoiceUpdateData.RPR_CHRG_ID, CStr(drLock.Item(RepairInvoiceUpdateData.RPR_CHRG_ID)), strCurrentSessionId, bv_strActivityName)
            Next
            Dim intCheckedRows As Integer = 0
            Dim strSplitActivitySubmit() As String = Nothing
            Dim intSplitActivity As Integer = 0
            intCheckedRows = dsRepairInvoiceUpdate.Tables(RepairInvoiceUpdateData._V_REPAIR_CHARGE).Compute(String.Concat("COUNT(", RepairInvoiceUpdateData.RPR_CHRG_ID, ")"), String.Concat(RepairInvoiceUpdateData.CHECKED, "= 'True'"))
            CacheData(REPAIR_INVOICE_UPATE, dsRepairInvoiceUpdate)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgRepairInvoiceUpdate_ClientBind"
    Protected Sub ifgRepairInvoiceUpdate_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgRepairInvoiceUpdate.ClientBind
        Try
            Dim objcommon As New CommonData()
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            If objcommon.GetMultiLocationSupportConfig.ToLower = "true" AndAlso objcommon.GetOrganizationTypeCD = "HQ" Then
                dsRepairInvoiceUpdate = objRepairInvoice.GetRepairChargevaluesAllDepots()
            Else
                dsRepairInvoiceUpdate = objRepairInvoice.GetRepairChargevalues(intDepotID)

            End If

            e.DataSource = dsRepairInvoiceUpdate.Tables(RepairInvoiceUpdateData._V_REPAIR_CHARGE)
            CacheData(REPAIR_INVOICE_UPATE, dsRepairInvoiceUpdate)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgRepairInvoiceUpdate_RowDataBound"
    Protected Sub ifgRepairInvoiceUpdate_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgRepairInvoiceUpdate.RowDataBound
        Try
            Dim objCommondata As New CommonData
            If objCommondata.GetMultiLocationSupportConfig.ToLower = "false" Then
                CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                    Dim chkActive As iFgCheckBox
                    chkActive = CType(e.Row.Cells(7).Controls(0), iFgCheckBox)
                    chkActive.Attributes.Add("onClick", String.Concat("lockData(this,'", CStr(drv.Row.Item(RepairInvoiceUpdateData.EQPMNT_NO)), "','", CStr(drv.Row.Item(RepairInvoiceUpdateData.RPR_CHRG_ID)), "');"))
                    If e.Row.RowIndex > 6 Then
                        Dim lkpControl As iLookup
                        lkpControl = CType(DirectCast(DirectCast(e.Row.Cells(6), iFgFieldCell).ContainingField, LookupField).Lookup, iLookup)
                        lkpControl.LookupGrid.VerticalAlign = iLookupGridStyle.vAlign.Bottom
                    End If
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetLockData"
    Private Sub pvt_GetLockData(ByVal bv_blnCheckBit As Boolean, _
                                ByVal bv_strRefNo As String, _
                                ByVal bv_strActivityName As String)
        Try
            Dim objCommonData As New CommonData
            Dim strErrorMessage As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim strIpAddress As String = GetClientIPAddress()
            Dim strCurrentIpAddress As String = String.Empty
            Dim strUserName As String = String.Empty
            blnLockData = objCommonData.pub_GetLockData(bv_blnCheckBit, bv_strRefNo, strUserName, bv_strActivityName, strIpAddress, False, RepairInvoiceUpdateData.RPR_CHRG_ID)
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

#Region "pvt_ActivitySubmit()"
    Private Sub pvt_ActivitySubmit(ByVal bv_intActivityId As Integer)
        Try
            dsRepairInvoiceUpdate = CType(RetrieveData(REPAIR_INVOICE_UPATE), RepairInvoiceUpdateDataSet)
            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())

            ''22432
            Dim strRepairChargeIDs As String = ""
            Dim strErrorMessage As String = ""
            For Each drRepairCharge As DataRow In dsRepairInvoiceUpdate.Tables(RepairInvoiceUpdateData._V_REPAIR_CHARGE).Select(RepairInvoiceUpdateData.CHECKED & "='True'")
                If strRepairChargeIDs <> String.Empty Then
                    strRepairChargeIDs = String.Concat(strRepairChargeIDs, ",", drRepairCharge.Item(RepairInvoiceUpdateData.RPR_CHRG_ID))
                Else
                    strRepairChargeIDs = String.Concat(drRepairCharge.Item(RepairInvoiceUpdateData.RPR_CHRG_ID))
                End If
            Next
            If strRepairChargeIDs <> "" Then
                Dim objCommonUI As New CommonUI
                objCommonUI.pub_validateFinalizedInvoiceActivityWise(CommonUIData._REPAIR_CHARGE, intDPT_ID, strRepairChargeIDs, "", "", strErrorMessage, dsRepairInvoiceUpdate)
            End If
            If strErrorMessage <> "" Then
                pub_SetCallbackReturnValue("ActivitySubmit", "This action will not be allowed since one of the selected repair invoice(s) is already finalized.")
            Else
                pub_SetCallbackReturnValue("ActivitySubmit", "True")
            End If
            pub_SetCallbackStatus(True)
            ''
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class
