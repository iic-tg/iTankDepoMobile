Option Strict On
Partial Class Operations_ChangeOfStatus
    Inherits Pagebase
    Dim dsChangeOfStatus As New ChangeOfStatusDataSet
    Dim dtCommon As DataTable
    Private Const CHANGE_OF_STATUS As String = "CHANGE_OF_STATUS"
    Private Const CHANGE_OF_STATUS_TEMP As String = "CHANGE_OF_STATUS_TEMP"
    Private strMSGUPDATE As String = "Change of Status : New Status Updated Successfully."

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                CacheData("CheckAll", "false")
                ifgEquipmentDetail.AllowSearch = True
                ifgEquipmentDetail.AllowRefresh = True
                ifgEquipmentDetail.ShowFooter = True
                dateStatus.Validator.RangeValidation = True
                dateStatus.Validator.Type = ValidationDataType.Date
                dateStatus.Validator.RngErrorMessage = "Status Date should not be greater than current date"
                dateStatus.Validator.RngMaximumValue = DateTime.Now.ToString("dd-MMM-yyyy")
                dateStatus.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy")
                pvt_SetChangesMade()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "updateChangeOfStatus"
                    pvt_UpdateChangeOfStatus(e.GetCallbackValue("WFData"))
                Case "fnGetData"
                    pvt_GetData()
                Case "COSlockData"
                    pvt_COSlockData(e.GetCallbackValue("CheckBit"), _
                                    e.GetCallbackValue("EquipmentNo"), _
                                    e.GetCallbackValue("EquipmentStatus"), _
                                    e.GetCallbackValue("LockBit"))
                Case "ResetLockedRecords"
                    pvt_ResetLockedRecords()
            End Select
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
            CommonWeb.IncludeScript("Script/Operations/ChangeOfStatus.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(lkpStatus)
        CommonWeb.pub_AttachHasChanges(dateStatus)
        CommonWeb.pub_AttachHasChanges(txtRemarks)
        CommonWeb.pub_AttachHasChanges(txtYardLocation)
        pub_SetGridChanges(ifgEquipmentDetail, "ITab1_0")
    End Sub

#End Region

#Region "pvt_GetData"
    Private Sub pvt_GetData()
        Try
            Dim objCommon As New CommonData()
            Dim sbChangeofStatus As New StringBuilder
            sbChangeofStatus.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCurrentDate, Now.ToString("dd-MMM-yyyy").ToUpper))
            sbChangeofStatus.Append(CommonWeb.GetTextDateValuesJSO(dateStatus, Now.ToString("dd-MMM-yyyy").ToUpper))
            pub_SetCallbackReturnValue("Message", sbChangeofStatus.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "pvt_UpdateChangeOfStatus"
    Private Sub pvt_UpdateChangeOfStatus(ByVal bv_strWfData As String)
        Try
            dsChangeOfStatus = CType(RetrieveData(CHANGE_OF_STATUS), ChangeOfStatusDataSet)
            Dim objActivityStatus As New ChangeOfStatus
            Dim objCommon As New CommonData
            Dim strModifiedby As String = objCommon.GetCurrentUserName()
            Dim datModifiedDate As String = objCommon.GetCurrentDate()
            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            ''The purpose of this parameter is to create cleaning from change of status directly 23609
            Dim strCleaningFromCode As String = "ACN"
            Dim strCleaningToCode As String = "CLN"
            ''
            Dim drChecked As DataRow()
            drChecked = dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Select(String.Concat(ChangeOfStatusData.CHECKED, "= 'True'"))
            If drChecked.Length = 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
            End If

            Dim str_014KeyValue As String = String.Empty
            Dim str_015KeyValue As String = String.Empty
            Dim str_016KeyValue As String = String.Empty
            Dim str_017KeyValue As String = String.Empty
            Dim str_018KeyValue As String = String.Empty
            Dim str_019KeyValue As String = String.Empty
            Dim intCleaningStatus1 As Integer = 0
            Dim intCleaningStatus2 As Integer = 0
            Dim intCondition As Integer = 0
            Dim intValveConditionId As Integer = 0

            Dim objCommonConfig As New ConfigSetting()

            str_014KeyValue = objCommonConfig.pub_GetConfigSingleValue("014", intDepotID)
            str_015KeyValue = objCommon.GetYardLocation()
            str_016KeyValue = objCommonConfig.pub_GetConfigSingleValue("016", intDepotID)
            str_017KeyValue = objCommonConfig.pub_GetConfigSingleValue("017", intDepotID)
            str_018KeyValue = objCommonConfig.pub_GetConfigSingleValue("018", intDepotID)
            str_019KeyValue = objCommonConfig.pub_GetConfigSingleValue("019", intDepotID)

            intCondition = CInt(objCommon.GetEnumID("EQUIPMENTCONDTN", str_016KeyValue))
            intValveConditionId = CInt(objCommon.GetEnumID("EQUIPMENTCONDTN", str_017KeyValue))

            intCleaningStatus1 = CInt(objCommon.GetEnumID("EQUIPMENTSTATUS", str_018KeyValue))
            intCleaningStatus2 = CInt(objCommon.GetEnumID("EQUIPMENTSTATUS2", str_019KeyValue))

            ''Lock Implementation - Unlock after submit
            ''Lock Implementation 
            For Each dr As DataRow In dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Select(String.Concat(ChangeOfStatusData.CHECKED, "= 'True'"))
                pvt_COSlockData("FALSE", dr.Item(ChangeOfStatusData.EQPMNT_NO).ToString, dr.Item(ChangeOfStatusData.EQPMNT_STTS_CD).ToString, "FALSE")
            Next
            ''
            objActivityStatus.pub_UpdateChangeofStatus(dsChangeOfStatus, _
                                                       strModifiedby, _
                                                       CDate(datModifiedDate), _
                                                       intDepotID, _
                                                       bv_strWfData, _
                                                       strCleaningFromCode, _
                                                       strCleaningToCode, _
                                                       str_014KeyValue, _
                                                       str_015KeyValue, _
                                                       intCleaningStatus1, _
                                                       intCleaningStatus2, _
                                                       intCondition, _
                                                       intValveConditionId)
            dsChangeOfStatus = CType(dsChangeOfStatus.GetChanges(), ChangeOfStatusDataSet)
            CacheData(CHANGE_OF_STATUS, dsChangeOfStatus)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_ClientBind"
    Protected Sub ifgEquipmentDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEquipmentDetail.ClientBind
        Try
            Dim objChangeofStatus As New ChangeOfStatus()
            Dim objCommon As New CommonData
            Dim dateCompare As String
            Dim strFilterCode As String
            Dim strMode As String = e.Parameters("Mode").ToString
            Dim StatusID As String = e.Parameters("StatusID").ToString
            Dim CustomerID As String = e.Parameters("CustomerID").ToString
            Dim EquipmentNo As String = e.Parameters("EquipmentNo").ToString
            Dim selectAll As String = e.Parameters("checkselect").ToString
            Dim ToStatusID As String = e.Parameters("ToStatusID").ToString
            Dim ToStatus As String = e.Parameters("ToStatus").ToString
            Dim strRemarks As String = e.Parameters("Remarks").ToString
            Dim strYardLocation As String = e.Parameters("YardLocation").ToString
            Dim strStatusDate As String = e.Parameters("StatusDate").ToString
            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            Dim dtTemp As New DataTable
            Dim sbChangeofStatus As New StringBuilder
            Dim strLockBit As String = ""
            Dim strLockWarningMessage As String = ""

            CacheData("CheckAll", selectAll)
            dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Rows.Clear()
            dsChangeOfStatus = objChangeofStatus.pub_GetActivityStatusBySearch(StatusID, _
                                                                               EquipmentNo, _
                                                                               CustomerID, _
                                                                               intDepotID)
            dtTemp = dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Copy()
            CacheData(CHANGE_OF_STATUS_TEMP, dtTemp)
            If selectAll = "true" Then
             
                For Each drchangeofstatus As DataRow In dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Rows
                    strLockBit = ""
                    drchangeofstatus.Item(ChangeOfStatusData.EQPMNT_STTS_ID) = ToStatusID
                    drchangeofstatus.Item(ChangeOfStatusData.NEW_EQPMNT_STTS_CD) = ToStatus
                    If Not strRemarks = "" Then
                        drchangeofstatus.Item(ChangeOfStatusData.RMRKS_VC) = strRemarks
                    End If
                    If Not strYardLocation = "" Then
                        drchangeofstatus.Item(ChangeOfStatusData.YRD_LCTN) = strYardLocation
                    End If
                    dateCompare = CStr(DateTime.Compare(CDate(strStatusDate), CDate(drchangeofstatus.Item(ChangeOfStatusData.ACTVTY_DT))))
                    If CInt(dateCompare) >= 0 Then
                        drchangeofstatus.Item(ChangeOfStatusData.CHECKED) = True
                        drchangeofstatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT) = strStatusDate
                        'Lock Implementation 
                        COSlockDataSelectAll("TRUE", drchangeofstatus.Item(ChangeOfStatusData.EQPMNT_NO).ToString, drchangeofstatus.Item(ChangeOfStatusData.EQPMNT_STTS_CD).ToString, strLockBit)
                        If strLockBit = "TRUE" Then
                            drchangeofstatus.Item(ChangeOfStatusData.CHECKED) = False
                            strLockWarningMessage = "Unselected Equipment(s) is being locked by other User or same user in another session"
                        End If
                        ''
                    Else
                        If sbChangeofStatus.Length = 0 Then
                            sbChangeofStatus.Append(drchangeofstatus.Item(ChangeOfStatusData.EQPMNT_NO))
                        Else
                            sbChangeofStatus.Append(", ")
                            sbChangeofStatus.Append(drchangeofstatus.Item(ChangeOfStatusData.EQPMNT_NO))
                        End If
                        drchangeofstatus.Item(ChangeOfStatusData.CHECKED) = False
                        drchangeofstatus.Item(ChangeOfStatusData.NEW_ACTVTY_DT) = DBNull.Value
                        ''Lock Implementation 
                        COSlockDataSelectAll("FALSE", drchangeofstatus.Item(ChangeOfStatusData.EQPMNT_NO).ToString, drchangeofstatus.Item(ChangeOfStatusData.EQPMNT_STTS_CD).ToString, strLockBit)
                        ''
                    End If


                Next
            ElseIf selectAll = "false" Then
                For Each drchangeofstatus As DataRow In dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Rows
                    drchangeofstatus.Item(ChangeOfStatusData.CHECKED) = False
                    ''Lock Implementation 
                    COSlockDataSelectAll("FALSE", drchangeofstatus.Item(ChangeOfStatusData.EQPMNT_NO).ToString, drchangeofstatus.Item(ChangeOfStatusData.EQPMNT_STTS_CD).ToString, strLockBit)
                    ''
                Next
            ElseIf (selectAll = "test") Then
                ' dsChangeOfStatus = CType(RetrieveData(CHANGE_OF_STATUS), ChangeOfStatusDataSet)
            End If

            If sbChangeofStatus.Length = 0 Then
                If dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Rows.Count = 0 Then
                    e.OutputParameters.Add("norecordsfound", True)
                Else
                    e.OutputParameters.Add("norecordsfound", False)
                    strFilterCode = dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Rows(0).Item(ChangeOfStatusData.FILTER_CODE).ToString
                    e.OutputParameters.Add("filter", strFilterCode)
                End If
            Else
                e.OutputParameters.Add("validateEquipment", sbChangeofStatus.ToString)
            End If
            ''Lock Implementation 
            e.OutputParameters.Add("LockWarningMessage", strLockWarningMessage)
            ''
            Dim bln_052GwsBit As Boolean
            Dim str_052GWSKey As String
            Dim objCommondata As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            str_052GWSKey = objCommonConfig.pub_GetConfigSingleValue("052", intDepotID)
            bln_052GwsBit = objCommonConfig.IsKeyExists

            If bln_052GwsBit Then
                If str_052GWSKey.ToLower = "true" Then
                    ifgEquipmentDetail.Columns.Item(4).ControlStyle.CssClass = "hide"
                    ifgEquipmentDetail.Columns.Item(4).ItemStyle.CssClass = "hide"
                    ifgEquipmentDetail.Columns.Item(4).HeaderStyle.CssClass = "hide"
                    ifgEquipmentDetail.Columns.Item(4).ShowHeader = False
                End If
            End If
            e.OutputParameters.Add("HidePrevCargo", str_052GWSKey.ToLower)
            e.DataSource = dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS)
            CacheData(CHANGE_OF_STATUS, dsChangeOfStatus)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_RowCreated"
    Protected Sub ifgEquipmentDetail_RowCreated(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentDetail.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.SetRenderMethodDelegate(New RenderMethod(AddressOf RowCreate))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Header Created"
    Sub RowCreate(ByVal writer As HtmlTextWriter, ByVal ctl As Control)
        Try
            Dim bln_052GwsBit As Boolean
            Dim str_052GWSKey As String
            Dim objCommondata As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
            str_052GWSKey = objCommonConfig.pub_GetConfigSingleValue("052", intDepotID)
            bln_052GwsBit = objCommonConfig.IsKeyExists
            If bln_052GwsBit Then
                If str_052GWSKey.ToLower = "true" Then
                    objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 4, "false")
                    'ifgEquipmentDetail.Columns.Item(4).ControlStyle.CssClass = "hide"
                    'ifgEquipmentDetail.Columns.Item(4).ItemStyle.CssClass = "hide"
                    'ifgEquipmentDetail.Columns.Item(4).HeaderStyle.CssClass = "hide"
                Else
                    objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 4, "true")
                End If
            End If
            Dim iLoop As Integer
            For iLoop = 0 To ctl.Controls.Count - 1
                If iLoop <> 11 Then
                    CType(ctl.Controls.Item(iLoop), WebControl).CssClass = ""
                    ctl.Controls.Item(iLoop).RenderControl(writer)
                End If
                If iLoop = 11 Then
                    writer.Write("<TH class="""">")
                    writer.Write("<input id=""chbSelectAll"" type=""checkbox""")
                    If String.Equals(RetrieveData("CheckAll").ToString, "true") Then
                        writer.Write("onclick=""SelectAll(this);"" checked />")
                    Else
                        writer.Write("onclick=""SelectAll(this);"" unchecked />")
                    End If
                End If
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_RowDataBound"
    Protected Sub ifgEquipmentDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentDetail.RowDataBound
        Try
            Dim bln_052GwsBit As Boolean
            Dim str_052GWSKey As String
            Dim objCommondata As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
            str_052GWSKey = objCommonConfig.pub_GetConfigSingleValue("052", intDepotID)
            bln_052GwsBit = objCommonConfig.IsKeyExists
            If e.Row.RowType = DataControlRowType.Header Then
                If bln_052GwsBit Then
                    If str_052GWSKey.ToLower = "true" Then

                        'ifgEquipmentDetail.Columns.Item(4).ShowHeader = False
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 4, "false")
                        'e.Row.Cells(4).ControlStyle.CssClass = "hide"
                        'e.Row.Cells(4).CssClass = "hide"
                        e.Row.Cells(4).Style.Add("display", "none")
                        'e.Row.Cells(4).ItemStyle.CssClass = "hide"
                        'e.Row.Cells(4).HeaderStyle.CssClass = "hide"

                    Else
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 4, "true")
                    End If
                End If
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If bln_052GwsBit Then
                    If str_052GWSKey.ToLower = "true" Then
                        e.Row.Cells(4).Style.Add("display", "none")
                        '  objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "hide", 4, str_052GWSKey)
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 4, "false")
                        'ifgEquipmentDetail.Columns.Item(4).ShowHeader = False
                        'ifgEquipmentDetail.Columns.Item(4).ControlStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(4).ItemStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(4).HeaderStyle.CssClass = "hide"
                    Else
                        objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "", 4, "true")
                    End If
                End If
                Dim strEuipmentNo As String = ""
                If Not e.Row.DataItem Is Nothing Then
                    Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                    strEuipmentNo = drv.Item(ChangeOfStatusData.EQPMNT_NO).ToString
                End If
                Dim chk As iFgCheckBox
                chk = CType(e.Row.Cells(11).Controls(0), iFgCheckBox)
                chk.Attributes.Add("onclick", String.Concat("updateStatus(this,'", strEuipmentNo, "');"))
                Dim datControl As iDate
                datControl = CType(DirectCast(DirectCast(e.Row.Cells(10),  _
                            iFgFieldCell).ContainingField,  _
                            DateField).iDate, iDate)
                datControl.Validator.CmpErrorMessage = "Status Date should be Less than or equal to Current Date."
                datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_RowUpdating"

    Protected Sub ifgEquipmentDetail_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgEquipmentDetail.RowUpdated

    End Sub

    Protected Sub ifgEquipmentDetail_RowUpdating(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgEquipmentDetail.RowUpdating
        Try
            Dim objCommondata As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
            Dim bln_052key As Boolean
            Dim str_052Value As String
            str_052Value = objCommonConfig.pub_GetConfigSingleValue("052", intDepotID)
            bln_052key = objCommonConfig.IsKeyExists
            Dim objChngeOfStatus As New ChangeOfStatus
            dsChangeOfStatus = CType(RetrieveData(CHANGE_OF_STATUS), ChangeOfStatusDataSet)
            e.NewValues(ChangeOfStatusData.EQPMNT_STTS_CD) = e.NewValues(ChangeOfStatusData.EQPMNT_STTS_CD)
            e.NewValues(ChangeOfStatusData.ACTVTY_DT) = e.NewValues(ChangeOfStatusData.NEW_ACTVTY_DT)
            e.NewValues(ChangeOfStatusData.CHECKED) = e.NewValues(ChangeOfStatusData.CHECKED)
            dsChangeOfStatus.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Rows(e.RowIndex).Item(ChangeOfStatusData.CHECKED) = e.NewValues(ChangeOfStatusData.CHECKED)
            If CBool(e.NewValues(ChangeOfStatusData.CHECKED)) = False Then
                Dim dtTemp As New DataTable
                Dim drTemp As DataRow()
                dtTemp = CType(RetrieveData(CHANGE_OF_STATUS_TEMP), DataTable)
                For Each drSelect As DataRow In dsChangeOfStatus.Tables(RepairEstimateData._V_ACTIVITY_STATUS).Select(String.Concat(ChangeOfStatusData.CHECKED, "='False'"))
                    drTemp = dtTemp.Select(String.Concat(ChangeOfStatusData.ACTVTY_STTS_ID, "=", drSelect(ChangeOfStatusData.ACTVTY_STTS_ID).ToString))
                    If drTemp.Length = 1 Then
                        e.NewValues(ChangeOfStatusData.YRD_LCTN) = drTemp(0).Item(ChangeOfStatusData.YRD_LCTN)
                        e.NewValues(ChangeOfStatusData.RMRKS_VC) = drTemp(0).Item(ChangeOfStatusData.RMRKS_VC)
                        e.RequiresRebind = True
                    End If
                Next
            End If
            CacheData(CHANGE_OF_STATUS, dsChangeOfStatus)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "COSlockData"
    Private Sub pvt_COSlockData(ByVal bv_strCheckBitFlag As String, _
                                ByVal bv_strEquipmentNo As String, _
                                ByVal bv_strEquipmentStatus As String, _
                                ByRef LockBit As String)
        Try
            Dim objCommonData As New CommonData
            Dim objCommonUI As New CommonUI
            Dim objChangeOfStatus As New ChangeOfStatus
            Dim strCurrentEquipmentStatus As String = ""
            Dim strCurrentSessionId As String = String.Empty
            Dim strCurrentUserName As String = String.Empty
            Dim strCurrentIpAddress As String = String.Empty
            Dim strSessionId As String = String.Empty
            Dim strUserName As String = String.Empty
            Dim strIpAddress As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim intDepotID As Integer = CInt(objCommonData.GetDepotID())
            Dim strErrorMessage As String = ""
            Dim strActivity As String = ""
            strCurrentSessionId = objCommonData.GetSessionID()
            strCurrentUserName = objCommonData.GetCurrentUserName()
            strCurrentIpAddress = GetClientIPAddress()

            LockBit = "FALSE"
            If bv_strCheckBitFlag.ToUpper = "TRUE" Then
                strCurrentEquipmentStatus = objCommonUI.pub_GetEquipmentStaus(bv_strEquipmentNo, intDepotID)
                If strCurrentEquipmentStatus <> bv_strEquipmentStatus Then
                    strErrorMessage = "This Equipment's current status has been changed by other user."
                Else
                    blnLockData = objCommonData.StoreLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentUserName, strCurrentSessionId, "Change Of Status", strCurrentIpAddress, True)

                    If blnLockData Then
                        LockBit = "TRUE"
                        ''Get Activity Name

                        Dim blnGetLock As Boolean = objCommonData.GetLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, strCurrentUserName, strActivity)

                        ''
                        strErrorMessage = String.Concat("This record (", bv_strEquipmentNo, ") cannot be modified because it is already being used by <b>", strCurrentUserName, "</b> user ")
                            strSessionId = objCommonData.GetSessionID()
                            strUserName = objCommonData.GetCurrentUserName()
                            strIpAddress = GetClientIPAddress()
                            If strCurrentIpAddress <> strIpAddress Then
                            strErrorMessage = String.Concat(strErrorMessage, " in another place. ")
                            Else
                            strErrorMessage = String.Concat(strErrorMessage, " in another session. ")
                        End If

                     

                    End If

                End If
            Else
                objCommonData.FlushLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, "Change Of Status")
            End If
            pub_SetCallbackReturnValue("ErrorMessage", strErrorMessage)
            pub_SetCallbackReturnValue("Activity", strActivity)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "COSlockDataSelectAll"
    Private Sub COSlockDataSelectAll(ByVal bv_strCheckBitFlag As String, _
                                ByVal bv_strEquipmentNo As String, _
                                ByVal bv_strEquipmentStatus As String, _
                                ByRef LockBit As String)
        Try
            Dim objCommonData As New CommonData
            Dim objCommonUI As New CommonUI
            Dim objChangeOfStatus As New ChangeOfStatus
            Dim strCurrentEquipmentStatus As String = ""
            Dim strCurrentSessionId As String = String.Empty
            Dim strCurrentUserName As String = String.Empty
            Dim strCurrentIpAddress As String = String.Empty
            Dim strSessionId As String = String.Empty
            Dim strUserName As String = String.Empty
            Dim strIpAddress As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim intDepotID As Integer = CInt(objCommonData.GetDepotID())
            Dim strErrorMessage As String = ""
            Dim strActivity As String = ""
            strCurrentSessionId = objCommonData.GetSessionID()
            strCurrentUserName = objCommonData.GetCurrentUserName()
            strCurrentIpAddress = GetClientIPAddress()

            LockBit = "FALSE"
            If bv_strCheckBitFlag.ToUpper = "TRUE" Then
                strCurrentEquipmentStatus = objCommonUI.pub_GetEquipmentStaus(bv_strEquipmentNo, intDepotID)
                If strCurrentEquipmentStatus <> bv_strEquipmentStatus Then
                    strErrorMessage = "This Equipment's status has been changed by other user."
                Else
                    blnLockData = objCommonData.StoreLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentUserName, strCurrentSessionId, "Change Of Status", strCurrentIpAddress, True)
                    If blnLockData Then
                        LockBit = "TRUE"  
                    End If
                End If
            Else
                objCommonData.FlushLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, "Change Of Status")
            End If
       
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ResetLockedRecords"
    Private Sub pvt_ResetLockedRecords()
        Try
            Dim objCommonData As New CommonData
            Dim strCurrentSessionId As String = objCommonData.GetSessionID()
            objCommonData.FlushLockDataByActivity(strCurrentSessionId, "Change Of Status")

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub

#End Region

End Class
