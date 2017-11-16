Option Strict On
Partial Class Operations_EquipmentInspection
    Inherits Pagebase

#Region "Declarations"
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"
    Private Const strMSGUPDATE As String = "Inspection : Equipment(s) Updated Successfully."
    Dim strMode As String
    Private Const EQUIPMENT_INSPECTION As String = "EQUIP_INSPECTION"
    Dim dsEquipmentInspection As New EquipmentInspectionDataSet
    Dim dtEquipmentInspection As DataTable
#End Region

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim str_PageTitle As String = Request.QueryString("pagetitle")
        ifgEquipmentDetail.AllowAdd = False
        ifgEquipmentDetail.AllowDelete = False
        If Not Page.IsPostBack Then
            hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
            pvt_SetChangesMade()
        End If
        ifgEquipmentDetail.ActionButtons.Item(0).Text = "Export"

    End Sub
    'todo: need to be added

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "GIlockData"
                    pvt_GIlockData(e.GetCallbackValue("CheckBit"), _
                    e.GetCallbackValue("EquipmentNo"), _
                    e.GetCallbackValue("LockBit"))
                Case "updateEquipmentInspection"
                    pvt_UpdateEquipmentInspection(e.GetCallbackValue("WfData"))
                Case "ValidatePreviousActivityDate"
                    pvt_ValidatePreviousActivityDate(e.GetCallbackValue("EquipmentNo"), _
                                                    e.GetCallbackValue("EventDate"), _
                                                    e.GetCallbackValue("PageName"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/EquipmentInformation.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/EquipmentInspection.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgEquipmentDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEquipmentDetail.ClientBind
        Try
            If Not e.Parameters("Mode") Is Nothing Then
                Dim objCommon As New CommonData
                Dim strCurrentSessionId As String = objCommon.GetSessionID()
                objCommon.FlushLockDataByActivity(strCurrentSessionId, "Equipment Inspection")
                Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
                hdnMode.Value = e.Parameters("Mode").ToString()
                CacheData("EquipmentInspectionMode", e.Parameters("Mode").ToString())
                Select Case e.Parameters("Mode").ToString()
                    Case strNew

                        Dim objEquipInspection As New EquipmentInspection

                        dsEquipmentInspection = objEquipInspection.GetEquipmentInspectionDetail(intDepotID)
                        dtEquipmentInspection = dsEquipmentInspection.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION)

                        Dim objCommonUI As New CommonUI()
                        Dim dsEqpStatus As New DataSet
                        Dim dsEqpmntTyp As New DataSet

                        Dim dtEquipmentType As New DataTable
                        Dim objCommonConfig As New ConfigSetting()
                        Dim blnShowEqStatus As Boolean = False

                        dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Equipment Inspection", True, intDepotID)

                        dtEquipmentType = objCommonUI.GetAllEquipmentCode(intDepotID)

                        If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                            blnShowEqStatus = True
                        End If

                        For Each drEquipmentInspction As DataRow In dtEquipmentInspection.Rows
                            drEquipmentInspction.Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID) = CommonWeb.GetNextIndex(dsEquipmentInspection.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION), EquipmentInspectionData.EQPMNT_INSPCTN_ID)
                            If blnShowEqStatus Then
                                drEquipmentInspction.Item(EquipmentInspectionData.EQPMNT_STTS_CD) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS)
                                drEquipmentInspction.Item(EquipmentInspectionData.EQPMNT_STTS_ID) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)
                            End If

                            If Not dtEquipmentType Is Nothing AndAlso dtEquipmentType.Rows.Count > 0 Then

                                If dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drEquipmentInspction.Item(EquipmentInspectionData.EQPMNT_TYP_ID))).Length > 0 Then
                                    Dim dr() As DataRow = dtEquipmentType.Select(String.Concat("EQPMNT_TYP_ID=", drEquipmentInspction.Item(EquipmentInspectionData.EQPMNT_TYP_ID)))

                                    drEquipmentInspction.Item(EquipmentInspectionData.EQPMNT_CD_ID) = dr(0).Item(EquipmentInspectionData.EQPMNT_TYP_ID)
                                    drEquipmentInspction.Item(EquipmentInspectionData.EQPMNT_CD_CD) = dr(0).Item(EquipmentInspectionData.EQPMNT_CD_CD)
                                End If
                            End If
                            drEquipmentInspction.Item(EquipmentInspectionData.INSPCTD_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                            drEquipmentInspction.Item(EquipmentInspectionData.INSPCTD_BY) = objCommon.GetCurrentUserName()

                            hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
                        Next
                        e.DataSource = dsEquipmentInspection.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION)
                    Case strEdit
                        Dim objCommonUI As New CommonUI()
                        Dim objEquipInspection As New EquipmentInspection
                        Dim dsStatusID As New DataSet
                        Dim dsEqpmntTyp As New DataSet
                        Dim dsEqpmntCode As New DataSet
                        Dim objCommonConfig As New ConfigSetting()

                        dsEquipmentInspection = objEquipInspection.GetEquipmentInspectionMySubmits(intDepotID)
                        ifgEquipmentDetail.Columns.Item(0).IsEditable = False
                        ifgEquipmentDetail.Columns.Item(1).IsEditable = False
                        ifgEquipmentDetail.Columns.Item(2).IsEditable = False
                        ifgEquipmentDetail.Columns.Item(3).IsEditable = False
                        ifgEquipmentDetail.Columns.Item(4).IsEditable = False

                        e.DataSource = dsEquipmentInspection.Tables(EquipmentInspectionData._EQUIPMENT_INSPECTION)
                End Select
                ifgEquipmentDetail.AllowSearch = True
                ifgEquipmentDetail.AllowRefresh = True
                ifgEquipmentDetail.ShowPageSizer = True
                ifgEquipmentDetail.ShowEmptyPager = True

                CacheData(EQUIPMENT_INSPECTION, dsEquipmentInspection)
                pub_CacheData(EQUIPMENT_INSPECTION, dsEquipmentInspection)
                CacheData("EquipmentInspectioNMode", e.Parameters("Mode").ToString())
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgEquipmentDetail, "ITab1_0")
    End Sub

    Protected Sub ifgEquipmentDetail_RowCreated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentDetail.RowCreated

    End Sub

    Protected Sub ifgEquipmentDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentDetail.RowDataBound
        Dim objCommondata As New CommonData
        Dim objCommonConfig As New ConfigSetting()
        Dim str_007EIRNo As String
        Dim bln_007EIRNo_Key As Boolean
        Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
        If e.Row.RowType = DataControlRowType.Header Then
            str_007EIRNo = objCommonConfig.pub_GetConfigSingleValue("007", intDepotID)
            bln_007EIRNo_Key = objCommonConfig.IsKeyExists
            If bln_007EIRNo_Key Then
                CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = str_007EIRNo
                CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ToolTip = str_007EIRNo
            End If
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim datControl As iDate
            Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If Not e.Row.DataItem Is Nothing Then
                datControl = CType(DirectCast(DirectCast(e.Row.Cells(12),  _
                            iFgFieldCell).ContainingField,  _
                            DateField).iDate, iDate)
                datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                datControl.Validator.RangeValidation = True
            End If
            ifgEquipmentDetail.Columns.Item(7).IsEditable = True
        End If
    End Sub

    Protected Sub ifgEquipmentDetail_RowInserted(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertedEventArgs) Handles ifgEquipmentDetail.RowInserted

    End Sub

    Protected Sub ifgEquipmentDetail_RowInserting(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgEquipmentDetail.RowInserting
        Try
            Dim lngGateInbin As Long
            dsEquipmentInspection = CType(RetrieveData(EQUIPMENT_INSPECTION), EquipmentInspectionDataSet)
            lngGateInbin = CommonWeb.GetNextIndex(dsEquipmentInspection.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION), EquipmentInspectionData.EQPMNT_INSPCTN_ID)
            e.Values(EquipmentInspectionData.EQPMNT_INSPCTN_ID) = lngGateInbin
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Protected Sub ifgEquipmentDetail_RowUpdated(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdatedEventArgs) Handles ifgEquipmentDetail.RowUpdated

    End Sub

    Protected Sub ifgEquipmentDetail_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgEquipmentDetail.RowUpdating

    End Sub


#Region "EquipmentInspectioNLocking"
    Private Sub pvt_GIlockData(ByVal bv_strCheckBitFlag As String, _
                                ByVal bv_strEquipmentNo As String, _
                                ByRef LockBit As String)
        Try
            If False Then
                Dim objCommonData As New CommonData
                Dim objCommonUI As New CommonUI
                Dim strEquipmentStatus As String = ""
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

               
                strEquipmentStatus = "INS"

                dsEquipmentInspection = CType(RetrieveData(EQUIPMENT_INSPECTION), EquipmentInspectionDataSet)
                Dim dr() As DataRow = dsEquipmentInspection.Tables("V_EQUIPMENT_INSPECTION").Select(String.Concat(EquipmentInspectionData.EQPMNT_NO, "='", bv_strEquipmentNo, "'"))
                If dr.Length > 0 Then
                    dr(0).Item(EquipmentInspectionData.CHECKED) = bv_strCheckBitFlag
                End If
                CacheData(EQUIPMENT_INSPECTION, dsEquipmentInspection)
                LockBit = "FALSE"
                If bv_strCheckBitFlag.ToUpper = "TRUE" Then
                    strCurrentEquipmentStatus = objCommonUI.pub_GetEquipmentStaus(bv_strEquipmentNo, intDepotID)
                    blnLockData = objCommonData.StoreLockData(EquipmentInspectionData.EQPMNT_NO, bv_strEquipmentNo, strCurrentUserName, strCurrentSessionId, "Equipment Inspection", strCurrentIpAddress, True)
                    If strCurrentEquipmentStatus <> Nothing AndAlso Not strEquipmentStatus.Contains(strCurrentEquipmentStatus) Then
                        strErrorMessage = "EquipmentInspectioN already done for this equipment."
                    Else
                        If blnLockData Then
                            LockBit = "TRUE"
                            ''Get Activity Name
                            Dim blnGetLock As Boolean = objCommonData.GetLockData(EquipmentInspectionData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, strCurrentUserName, strActivity)                        ''
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
                    objCommonData.FlushLockData(EquipmentInspectionData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, "Equipment Inspection")
                End If
                pub_SetCallbackReturnValue("ErrorMessage", strErrorMessage)
                pub_SetCallbackReturnValue("Activity", strActivity)
                pub_SetCallbackStatus(True)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_UpdateEquipmentInspection"
    Private Sub pvt_UpdateEquipmentInspection(ByVal bv_strWfData As String)
        Try
            strMode = CType(RetrieveData("EquipmentInspectioNMode"), String)
            Dim dtEquipmentInspectioNDocument As DataTable
            Dim drEquipmentInspectioN As DataRow
            Dim objCommonUI As New CommonUI
            Dim objCommondata As New CommonData
            Dim dsDepot As DataSet
            dsEquipmentInspection = CType(RetrieveData(EQUIPMENT_INSPECTION), EquipmentInspectionDataSet)
            Dim drAEquipmentInspection As DataRow()
            Dim blnAllowRental As Boolean = False
            If strMode = "new" Then
                drAEquipmentInspection = dsEquipmentInspection.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION).Select(EquipmentInspectionData.CHECKED & "='True'")
                dtEquipmentInspectioNDocument = dsEquipmentInspection.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION).Clone()
            Else
                drAEquipmentInspection = dsEquipmentInspection.Tables(EquipmentInspectionData._EQUIPMENT_INSPECTION).Select(EquipmentInspectionData.CHECKED & "='True'")
                dtEquipmentInspectioNDocument = dsEquipmentInspection.Tables(EquipmentInspectionData._EQUIPMENT_INSPECTION).Clone()
            End If


            If Not drAEquipmentInspection.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
            End If
            ''Lock Implementation - Unlock after submit
            ''Lock Implementation 
            For Each dr As DataRow In dsEquipmentInspection.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION).Select(EquipmentInspectionData.CHECKED & "='True'")
                pvt_GIlockData("FALSE", dr.Item(EquipmentInspectionData.EQPMNT_NO).ToString, "FALSE")
                dr.Item(EquipmentInspectionData.CHECKED) = True
            Next

            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())

            Dim objEquipmentInspection As New EquipmentInspection

            Dim strRemarks As String = String.Empty
            Dim strLockingRecords As String = String.Empty

            If Not pub_RetrieveData("Equip_Remarks") Is Nothing Then
                strRemarks = pub_RetrieveData("Equip_Remarks").ToString()
            End If
            pub_CacheData("Equip_Remarks", "")
            RetrieveData("AttachmentClear")
            objEquipmentInspection.pub_UpdateEquipInspection(dsEquipmentInspection, bv_strWfData, _
                                       objCommon.GetCurrentUserName(), CDate(objCommon.GetCurrentDate()), _
                                       strMode, intDPT_ID, strRemarks, strLockingRecords)

            CacheData("AttachmentClear", Nothing)
            Dim dsEquipmentInspectioNStatus As New EquipmentInspectionDataSet
            dsEquipmentInspectioNStatus.Tables(EquipmentInspectionData._EQUIPMENT_STATUS).Rows.Clear()
            dsEquipmentInspectioNStatus = objEquipmentInspectioN.pub_GetEqupimentStatus(intDPT_ID)
            dsEquipmentInspection.Tables(EquipmentInspectionData._EQUIPMENT_STATUS).Clear()
            dsEquipmentInspection.Merge(dsEquipmentInspectioNStatus.Tables(EquipmentInspectionData._EQUIPMENT_STATUS))

            Dim dsEquipmentInspectioNCustomer As New EquipmentInspectionDataSet
            dsEquipmentInspectioNCustomer.Tables(EquipmentInspectionData._CUSTOMER).Rows.Clear()
            dsEquipmentInspectioNCustomer = objEquipmentInspectioN.pub_GetCustomerDetail(intDPT_ID)
            dsEquipmentInspection.Tables(EquipmentInspectionData._CUSTOMER).Clear()
            dsEquipmentInspection.Merge(dsEquipmentInspectioNCustomer.Tables(EquipmentInspectionData._CUSTOMER))
            ' If strMode = MODE_NEW Then

            For Each dr As DataRow In dsEquipmentInspection.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION).Select(EquipmentInspectionData.CHECKED & "='True'")
                drEquipmentInspectioN = dtEquipmentInspectioNDocument.NewRow()
                drEquipmentInspectioN.Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID) = dr.Item(EquipmentInspectionData.EQPMNT_INSPCTN_ID)
                drEquipmentInspectioN.Item(EquipmentInspectionData.EQPMNT_NO) = dr.Item(EquipmentInspectionData.EQPMNT_NO)
                drEquipmentInspectioN.Item(EquipmentInspectionData.EQPMNT_TYP_ID) = dr.Item(EquipmentInspectionData.EQPMNT_TYP_ID)
                drEquipmentInspectioN.Item(EquipmentInspectionData.EQPMNT_TYP_CD) = dr.Item(EquipmentInspectionData.EQPMNT_TYP_CD)
                drEquipmentInspectioN.Item(EquipmentInspectionData.EQPMNT_CD_ID) = dr.Item(EquipmentInspectionData.EQPMNT_TYP_ID)
                drEquipmentInspectioN.Item(EquipmentInspectionData.EQPMNT_CD_CD) = dr.Item(EquipmentInspectionData.EQPMNT_CD_CD)
                drEquipmentInspectioN.Item(EquipmentInspectionData.EQPMNT_STTS_ID) = dr.Item(EquipmentInspectionData.EQPMNT_STTS_ID)
                drEquipmentInspectioN.Item(EquipmentInspectionData.EQPMNT_STTS_CD) = dr.Item(EquipmentInspectionData.EQPMNT_STTS_CD)
                drEquipmentInspectioN.Item(EquipmentInspectionData.YRD_LCTN) = dr.Item(EquipmentInspectionData.YRD_LCTN)
                drEquipmentInspectioN.Item(EquipmentInspectionData.GTN_DT) = dr.Item(EquipmentInspectionData.GTN_DT)
                Dim strEirNumEquipmentInspectioNRet As String
                If drEquipmentInspectioN.Item(EquipmentInspectionData.EIR_NO).ToString.Trim.Length > 14 Then
                    strEirNumEquipmentInspectioNRet = drEquipmentInspectioN.Item(EquipmentInspectionData.EIR_NO).ToString.Trim.Substring(0, 14).ToString()
                Else
                    strEirNumEquipmentInspectioNRet = drEquipmentInspectioN.Item(EquipmentInspectionData.EIR_NO).ToString()
                End If
                drEquipmentInspectioN.Item(EquipmentInspectionData.EIR_NO) = strEirNumEquipmentInspectioNRet

                drEquipmentInspectioN.Item(EquipmentInspectionData.AGNT_ID) = dr.Item(EquipmentInspectionData.AGNT_ID)
                drEquipmentInspectioN.Item(EquipmentInspectionData.BLL_ID) = dr.Item(EquipmentInspectionData.BLL_ID)
                drEquipmentInspectioN.Item(EquipmentInspectionData.GRD_ID) = dr.Item(EquipmentInspectionData.GRD_ID)
                drEquipmentInspectioN.Item(EquipmentInspectionData.RMRKS_VC) = dr.Item(EquipmentInspectionData.RMRKS_VC)
                If Not (dr.Item(EquipmentInspectionData.CSTMR_NAM) Is DBNull.Value) Then
                    drEquipmentInspectioN.Item(EquipmentInspectionData.CSTMR_ID) = dr.Item(EquipmentInspectionData.CSTMR_ID)
                    drEquipmentInspectioN.Item(EquipmentInspectionData.CSTMR_NAM) = dr.Item(EquipmentInspectionData.CSTMR_NAM)
                    drEquipmentInspectioN.Item(EquipmentInspectionData.CSTMR_CD) = dr.Item(EquipmentInspectionData.CSTMR_CD)

                Else
                    For Each drCstmrDetail As DataRow In dsEquipmentInspection.Tables(EquipmentInspectionData._CUSTOMER).Select(String.Concat(EquipmentInspectionData.CSTMR_CD, "='", _
                                                                                               dr.Item(EquipmentInspectionData.CSTMR_CD), "'"))
                        drEquipmentInspectioN.Item(EquipmentInspectionData.CSTMR_NAM) = drCstmrDetail.Item(EquipmentInspectionData.CSTMR_NAM)
                        drEquipmentInspectioN.Item(EquipmentInspectionData.CSTMR_CD) = dr.Item(EquipmentInspectionData.CSTMR_CD)
                        drEquipmentInspectioN.Item(EquipmentInspectionData.CSTMR_ID) = dr.Item(EquipmentInspectionData.CSTMR_ID)
                        Exit For
                    Next
                End If

                If Not (dr.Item(EquipmentInspectionData.EQPMNT_STTS_DSCRPTN_VC) Is DBNull.Value) Then
                    drEquipmentInspectioN.Item(EquipmentInspectionData.EQPMNT_STTS_DSCRPTN_VC) = dr.Item(EquipmentInspectionData.EQPMNT_STTS_DSCRPTN_VC)
                Else
                    For Each drEqpStatus As DataRow In dsEquipmentInspection.Tables(EquipmentInspectionData._EQUIPMENT_STATUS).Select(String.Concat(EquipmentInspectionData.EQPMNT_STTS_CD, "='", _
                                                                                dr.Item(EquipmentInspectionData.EQPMNT_STTS_CD), "'"))
                        drEquipmentInspectioN.Item(EquipmentInspectionData.EQPMNT_STTS_DSCRPTN_VC) = drEqpStatus.Item(EquipmentInspectionData.EQPMNT_STTS_DSCRPTN_VC)
                        Exit For
                    Next
                    drEquipmentInspectioN.Item(EquipmentInspectionData.CHECKED) = dr.Item(EquipmentInspectionData.CHECKED)
                End If

                dtEquipmentInspectioNDocument.Rows.Add(drEquipmentInspectioN)
            Next
            dsEquipmentInspection.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION).Clear()
            dsEquipmentInspection.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION).Merge(dtEquipmentInspectioNDocument)
            'End If

            If strLockingRecords <> Nothing Then
                Dim intCheckedRows As Integer = 0
                Dim strSplitLockingRecords() As String = Nothing
                Dim intSplitActivity As Integer = 0
                intCheckedRows = CInt(dsEquipmentInspection.Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION).Compute(String.Concat("COUNT(", EquipmentInspectionData.EQPMNT_INSPCTN_ID, ")"), String.Concat(EquipmentInspectionData.CHECKED, "= 'True'")))
                strSplitLockingRecords = strLockingRecords.Split(CChar(","))
                intSplitActivity = strSplitLockingRecords.Length

                If intCheckedRows = intSplitActivity Then
                    pub_SetCallbackReturnValue("LockRecordBit", "true")
                Else
                    pub_SetCallbackReturnValue("LockRecordBit", "false")
                End If
                pub_SetCallbackReturnValue("LockRecord", strLockingRecords)
            End If

            dsEquipmentInspection.AcceptChanges()
            dsDepot = objCommonUI.pub_GetDepoDetail(intDPT_ID)
            dsEquipmentInspection.Merge(dsDepot.Tables(CommonUIData._DEPOT))
            dsEquipmentInspection.AcceptChanges()
            CacheData(EQUIPMENT_INSPECTION, dsEquipmentInspection)
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            CacheData("DeleteFlag", Nothing)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_ValidatePreviousActivityDate"
    Private Sub pvt_ValidatePreviousActivityDate(ByVal bv_strEquipmentNo As String, _
                                                 ByVal bv_strEventDate As String, _
                                                 ByVal bv_strPageName As String)
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
                                                                            "Inspection", _
                                                                            CInt(objCommon.GetDepotID()))
            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("" + bv_strPageName + "  Date should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), " )"))
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

End Class
