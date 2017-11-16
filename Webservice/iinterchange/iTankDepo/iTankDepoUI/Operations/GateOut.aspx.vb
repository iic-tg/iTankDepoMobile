Option Strict On
Partial Class Operations_GateOut
    Inherits Pagebase
    Dim dsGateOutData As New GateOutDataSet
    Dim dtGateOutData As DataTable
    Private Const GATE_OUT As String = "GATE_OUT"
    Private Const GATE_OUT_DOCUMENT As String = "GATE_OUT_DOCUMENT"
    Private strMSGUPDATE As String = "Gate Out : Equipment(s) Updated Successfully."
    Private Const GateOutMode As String = "GateOutMode"
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"
    Dim str_008EIRTime As String
    Dim bln_008EIRTime_Key As Boolean
    Dim bln_005EqStatus_Key As Boolean
    Dim str_005EqStatus As String
    Dim strMode As String
    Dim bln_022RentalBit_Key As Boolean
    Dim str_022RentalBit As String
    Dim str_055 As String
    Dim bln_055 As Boolean

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim str_059Gateout As String = String.Empty
            Dim objCommonConfig As New ConfigSetting()
            Dim objCommondata As New CommonData
            Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
            str_059Gateout = objCommonConfig.pub_GetConfigSingleValue("059", intDepotID)
            pvt_SetChangesMade()
            hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
            If str_059Gateout.ToLower <> "true" Then
                DirectCast(ifgEquipmentDetail.Columns.Item(5), iInterchange.WebControls.v4.Data.LookupField).HeaderText = "Status"
                DirectCast(ifgEquipmentDetail.Columns.Item(5), iInterchange.WebControls.v4.Data.LookupField).HeaderTitle = "Status"
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
            CommonWeb.IncludeScript("Script/Operations/GateOut.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
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
                Case "UpdateGateOut"
                    pvt_UpdateGateOut(e.GetCallbackValue("WFData"))
                Case "fnGetData"
                    pvt_GetData()
                Case "ValidatePreviousActivityDate"
                    pvt_ValidatePreviousActivityDate(e.GetCallbackValue("EquipmentNo"), _
                                                     e.GetCallbackValue("EventDate"))
                Case "GOlockData"
                    pvt_GOlockData(e.GetCallbackValue("CheckBit"), _
                                  e.GetCallbackValue("EquipmentNo"), _
                                  e.GetCallbackValue("LockBit"))
                Case "ValidateGateINAttachment"
                    pvt_ValidateGateINAttachment(e.GetCallbackValue("GateInId"))
            End Select
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
                                                                          "Gate Out", _
                                                                          CInt(objCommon.GetDepotID()))
            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Equipment's Activity Date Should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_UpdateGateOut"
    Private Sub pvt_UpdateGateOut(ByVal bv_strWfData As String)
        Try
            strMode = CType(RetrieveData(GateOutMode), String)
            Dim dtGateOut As DataTable
            Dim objCommonUI As New CommonUI
            Dim objCommondata As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDepotID As Integer = CInt(objCommondata.GetDepotID())
            Dim intHQID As Integer = CInt(objCommondata.GetDepotID())
            dsGateOutData = CType(RetrieveData(GATE_OUT), GateOutDataSet)
            Dim drAGateOut As DataRow()
            drAGateOut = dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(GateOutData.CHECKED & "='True'")
            dtGateOut = dsGateOutData.Tables(GateOutData._V_GATEOUT).Clone()

            Dim str_067InvoiceGenerationGWSBit As String = Nothing
            str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", intDepotID)

            If Not drAGateOut.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
            End If

            ''Lock Implementation - Unlock after submit
            ''Lock Implementation 
            For Each dr As DataRow In dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(RepairCompletionData.CHECKED, "= 'True'"))
                pvt_GOlockData("FALSE", dr.Item(GateOutData.EQPMNT_NO).ToString, "FALSE")
            Next

            Dim objgateOut As New GateOut
            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim strEqpmntNO As String = String.Empty
            Dim blnAllowRental As Boolean
            RetrieveData("AttachmentClear")

            'GWS 
            str_055 = objCommonConfig.pub_GetConfigSingleValue("055", intDPT_ID)
            bln_055 = objCommonConfig.IsKeyExists

            objgateOut.pub_UpdateGateOut(dsGateOutData, _
                                         bv_strWfData, _
                                         False, _
                                         objCommon.GetCurrentUserName(), _
                                         CDate(objCommon.GetCurrentDate()), _
                                         strMode, _
                                         intDPT_ID, _
                                         CStr(RetrieveData("AttachmentClear")), str_055, str_067InvoiceGenerationGWSBit)
            CacheData("AttachmentClear", Nothing)
            Dim drGateOut As DataRow
            Dim dsGateOutStatus As New GateOutDataSet
            dsGateOutStatus.Tables(GateOutData._EQUIPMENT_STATUS).Rows.Clear()
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intHQID = CInt(objCommon.GetHeadQuarterID())
            End If
            dsGateOutStatus = objgateOut.pub_GetEqupimentStatus(intHQID)
            dsGateOutData.Tables(GateOutData._EQUIPMENT_STATUS).Clear()
            dsGateOutData.Merge(dsGateOutStatus.Tables(GateOutData._EQUIPMENT_STATUS))

            Dim dsGateoutCustomer As New GateOutDataSet
            dsGateoutCustomer.Tables(GateOutData._CUSTOMER).Clear()
            dsGateoutCustomer = objgateOut.pub_GetCustomerDetail(intHQID)
            dsGateOutData.Tables(GateOutData._CUSTOMER).Clear()
            dsGateOutData.Merge(dsGateoutCustomer.Tables(GateOutData._CUSTOMER))
            str_022RentalBit = objCommonConfig.pub_GetConfigSingleValue("022", intDPT_ID)
            bln_022RentalBit_Key = objCommonConfig.IsKeyExists

            If bln_022RentalBit_Key Then
                If str_022RentalBit = "False" Then
                    blnAllowRental = False
                Else
                    blnAllowRental = True
                End If
            End If
            Dim bln_051GwsBit As Boolean
            Dim str_051GWSKey As String
            str_051GWSKey = objCommonConfig.pub_GetConfigSingleValue("051", intDepotID)
            bln_051GwsBit = objCommonConfig.IsKeyExists
            For Each dr As DataRow In dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(GateOutData.CHECKED & "='True'")
                drGateOut = dtGateOut.NewRow()
                drGateOut(GateOutData.EQPMNT_NO) = dr(GateOutData.EQPMNT_NO)
                If Not (dr.Item(GateOutData.CSTMR_NAM) Is DBNull.Value) Then
                    drGateOut.Item(GateOutData.CSTMR_NAM) = dr.Item(GateOutData.CSTMR_NAM)
                Else
                    For Each drCstmrDetail As DataRow In dsGateOutData.Tables(GateOutData._CUSTOMER).Select(String.Concat(GateOutData.CSTMR_CD, "='", _
                                                                                                dr.Item(GateOutData.CSTMR_CD), "'"))
                        drGateOut.Item(GateOutData.CSTMR_NAM) = drCstmrDetail.Item(GateOutData.CSTMR_NAM)
                        If bln_051GwsBit Then
                            drGateOut.Item(GateOutData.AGNT_CD) = drCstmrDetail.Item(GateOutData.AGNT_CD)
                        End If

                        Exit For
                    Next
                End If
                'GWS
               
                If bln_051GwsBit Then
                    Dim dsGateoutEqp As New GateOutDataSet
                    dsGateoutEqp.Tables(GateOutData._EQUIPMENT_INFORMATION).Rows.Clear()
                    dsGateoutEqp = objgateOut.pub_GetEquipmentInformation(CStr(drGateOut(GateOutData.EQPMNT_NO)))
                    dsGateOutData.Tables(GateOutData._EQUIPMENT_INFORMATION).Clear()
                    dsGateOutData.Merge(dsGateoutEqp.Tables(GateOutData._EQUIPMENT_INFORMATION))
                    For Each drEqpDetail As DataRow In dsGateOutData.Tables(GateOutData._EQUIPMENT_INFORMATION).Rows
                        drGateOut.Item(GateOutData.CSC_VLDTY) = drEqpDetail.Item(GateOutData.CSC_VLDTY)
                        drGateOut.Item(GateOutData.MNFCTR_DT) = drEqpDetail.Item(GateOutData.MNFCTR_DT)
                        drGateOut.Item(GateOutData.INSPCTD_BY) = drEqpDetail.Item(GateOutData.INSPCTD_BY)

                    Next
                    For Each drCstmrDetail As DataRow In dsGateOutData.Tables(GateOutData._CUSTOMER).Select(String.Concat(GateOutData.CSTMR_CD, "='", _
                                                                                               dr.Item(GateOutData.CSTMR_CD), "'"))
                        drGateOut.Item(GateOutData.CSTMR_NAM) = drCstmrDetail.Item(GateOutData.CSTMR_NAM)
                        drGateOut.Item(GateOutData.AGNT_CD) = drCstmrDetail.Item(GateOutData.AGNT_CD)
                    Next
                End If
                drGateOut(GateOutData.CSTMR_ID) = dr(GateOutData.CSTMR_ID)
                drGateOut(GateOutData.CSTMR_CD) = dr(GateOutData.CSTMR_CD)
                drGateOut(GateOutData.EQPMNT_TYP_CD) = dr(GateOutData.EQPMNT_TYP_CD)
                drGateOut(GateOutData.EQPMNT_CD_CD) = dr(GateOutData.EQPMNT_CD_CD)
                drGateOut(GateOutData.YRD_LCTN) = dr(GateOutData.YRD_LCTN)
                drGateOut(GateOutData.GTOT_DT) = CDate(dr(GateOutData.GTOT_DT)).ToString("dd-MMM-yyyy").ToUpper
                drGateOut(GateOutData.GTOT_TM) = dr(GateOutData.GTOT_TM)
                drGateOut(GateOutData.VHCL_NO) = dr(GateOutData.VHCL_NO)
                drGateOut(GateOutData.EQPMNT_STTS_CD) = dr(GateOutData.EQPMNT_STTS_CD)
                drGateOut(GateOutData.TRNSPRTR_CD) = dr(GateOutData.TRNSPRTR_CD)
                drGateOut(GateOutData.GI_TRNSCTN_NO) = dr(GateOutData.GI_TRNSCTN_NO)
                drGateOut(GateOutData.EIR_NO) = dr(GateOutData.EIR_NO)
                drGateOut(GateOutData.RMRKS_VC) = dr(GateOutData.RMRKS_VC)
                drGateOut(GateOutData.RNTL_BT) = dr(GateOutData.RNTL_BT)
                drGateOut(GateOutData.ALLOW_RENTAL) = blnAllowRental
                If Not (dr.Item(GateinData.EQPMNT_STTS_DSCRPTN_VC) Is DBNull.Value) Then
                    drGateOut.Item(GateinData.EQPMNT_STTS_DSCRPTN_VC) = dr.Item(GateinData.EQPMNT_STTS_DSCRPTN_VC)
                Else
                    For Each drEqpStatus As DataRow In dsGateOutData.Tables(GateOutData._EQUIPMENT_STATUS).Select(String.Concat(GateOutData.EQPMNT_STTS_CD, "='", _
                                                                                    dr.Item(GateOutData.EQPMNT_STTS_CD), "'"))
                        drGateOut.Item(GateOutData.EQPMNT_STTS_DSCRPTN_VC) = drEqpStatus.Item(GateOutData.EQPMNT_STTS_DSCRPTN_VC)

                        Exit For
                    Next
                End If
                drGateOut.Item(GateinData.CHECKED) = dr.Item(GateinData.CHECKED)
                dtGateOut.Rows.Add(drGateOut)
            Next
            dsGateOutData.Tables(GateOutData._V_GATEOUT).Clear()
            dsGateOutData.Tables(GateOutData._V_GATEOUT).Merge(dtGateOut)
            CacheData(GATE_OUT, dsGateOutData)
            CacheData(GATE_OUT_DOCUMENT, dsGateOutData)
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

#Region "ifgEquipmentDetail_ClientBind"
    Protected Sub ifgEquipmentDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEquipmentDetail.ClientBind
        Try
            'TODO : uncomment the following lines for Dashboard pending actions .
            Dim strFilterName As String = ""
            Dim strFilterValue As String = ""
            Dim objCommonUI As New CommonUI()
            Dim dtGateOut As DataTable
            Dim dtRental As New DataTable
            Dim objGateOut As New GateOut()
            Dim objCommon As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim str_059Gateout As String = String.Empty
            Dim strGateOutApprovalProcess As String = Nothing

            str_059Gateout = objCommonConfig.pub_GetConfigSingleValue("059", intDPT_ID)
            strGateOutApprovalProcess = objCommonConfig.pub_GetConfigSingleValue("066", intDPT_ID)


            hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
            If Not e.Parameters("Mode") Is Nothing Then

                Dim strCurrentSessionId As String = objCommon.GetSessionID()
                objCommon.FlushLockDataByActivity(strCurrentSessionId, "Gate Out")

                hdnMode.Value = e.Parameters("Mode").ToString()
                Select Case e.Parameters("Mode").ToString()
                    Case strNew
                        'Dim blnShowEqStatus As Boolean = False
                        Dim dsEqpStatus As New DataSet
                        dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Gate Out", True, intDPT_ID)

                        If str_059Gateout.Trim.ToUpper = "TRUE" Then
                            dsGateOutData = objGateOut.pub_GetGateOutDetailsGWS(intDPT_ID)
                        Else
                            dsGateOutData = objGateOut.pub_GetGateOutDetails(intDPT_ID)
                        End If



                        If strGateOutApprovalProcess <> Nothing AndAlso CBool(strGateOutApprovalProcess) = True Then

                            dsGateOutData = New GateOutDataSet
                            dsGateOutData = objGateOut.pub_GetGateOutDetailsGWSWithApproval(intDPT_ID)

                        End If



                        dtGateOut = dsGateOutData.Tables(GateOutData._V_GATEOUT)
                        str_008EIRTime = objCommonConfig.pub_GetConfigSingleValue("008", intDPT_ID)
                        bln_008EIRTime_Key = objCommonConfig.IsKeyExists
                        Dim str_006YardLocation As String
                        str_006YardLocation = objCommon.GetYardLocation()
                        'If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                        '    blnShowEqStatus = True
                        'End If
                        For Each drGateOut As DataRow In dtGateOut.Rows
                            drGateOut.Item(GateOutData.GTOT_ID) = CommonWeb.GetNextIndex(dsGateOutData.Tables(GateOutData._V_GATEOUT), GateOutData.GTOT_ID)
                            'If blnShowEqStatus Then
                            '    drGateOut.Item(GateOutData.EQPMNT_STTS_CD) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.PENDING_STATUS)
                            '    drGateOut.Item(GateOutData.EQPMNT_STTS_ID) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)
                            'End If
                            If bln_008EIRTime_Key Then
                                drGateOut.Item(GateOutData.GTOT_TM) = str_008EIRTime
                            End If
                            If str_059Gateout.Trim.ToUpper = "TRUE" Then
                                drGateOut.Item(GateOutData.GTOT_TM) = DateTime.Now.ToString("H:mm").ToUpper
                            End If
                            drGateOut.Item(GateOutData.GTOT_DT) = hdnCurrentDate.Value
                            drGateOut.Item(GateOutData.YRD_LCTN) = str_006YardLocation
                        Next
                        'For Each drGateOut As DataRow In dtGateOut.Rows

                        ifgEquipmentDetail.AllowAdd = False
                        ifgEquipmentDetail.AllowDelete = False
                        ifgEquipmentDetail.ShowFooter = True

                    Case strEdit
                        dsGateOutData = objGateOut.pub_GetGateOutMySubmitDetails(intDPT_ID)
                End Select
                'For Attchemnt
                If Not e.Parameters("AttchMode") Is Nothing Then
                    If e.Parameters("AttchMode").ToString() = "ReBind" Then
                        dsGateOutData = CType(RetrieveData(GATE_OUT), GateOutDataSet)
                        Dim lngGateinId As Long = 0
                        Dim intFilesCount As Integer = 0
                        Dim drGateIn1 As DataRow() = Nothing
                        If Not e.Parameters("RepairEstimateId") Is Nothing Then
                            lngGateinId = CLng(e.Parameters("RepairEstimateId"))
                            drGateIn1 = dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(GateOutData.GTOT_ID, " = ", lngGateinId))
                            If drGateIn1.Length > 0 Then
                                intFilesCount = CInt(dsGateOutData.Tables(GateOutData._ATTACHMENT).Compute(String.Concat("COUNT(", RepairCompletionData.RPR_ESTMT_ID, ")"), String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = '", lngGateinId, "'")))
                                drGateIn1(0).Item(GateOutData.COUNT_ATTACH) = intFilesCount
                            End If
                            CacheData("AttachmentClear", intFilesCount)
                        End If
                    End If
                End If
                If dsGateOutData.Tables(GateOutData._V_GATEOUT).Rows.Count = 0 Then
                    e.OutputParameters.Add("norecordsfound", True)
                    ' hdnNoRecordFound.Value = "True"              
                End If
            End If
            Dim dsMoreInfo As DataSet
            Dim dsdepot As New DataSet
            dsMoreInfo = objGateOut.pub_V_GateinDetail(intDPT_ID)
            e.DataSource = dsGateOutData.Tables(GateOutData._V_GATEOUT)
            dsGateOutData.Merge(dsMoreInfo.Tables(GateinData._V_GATEIN_DETAIL))
            dsdepot = objCommonUI.pub_GetDepoDetail(intDPT_ID)
            dsGateOutData.Merge(dsdepot.Tables(CommonUIData._DEPOT))
            CacheData(GATE_OUT, dsGateOutData)
            CacheData("GateOutMode", e.Parameters("Mode").ToString())
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_RowDataBound"
    Protected Sub ifgEquipmentDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentDetail.RowDataBound
        Try

            Dim str_007EIRNo As String = String.Empty
            Dim str_059Gateout As String = String.Empty
            Dim bln_007EIRNo_Key As Boolean
            Dim objCommonConfig As New ConfigSetting()
            Dim objCommondata As New CommonData
            Dim intDepotID As Integer = CommonWeb.iInt(objCommondata.GetDepotID())
            str_007EIRNo = objCommonConfig.pub_GetConfigSingleValue("007", intDepotID)
            str_059Gateout = objCommonConfig.pub_GetConfigSingleValue("059", intDepotID)

            objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 5, str_059Gateout)
            objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 6, str_059Gateout)
            objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 12, str_059Gateout)
            objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 14, str_059Gateout)

            Dim strMode As String = RetrieveData("GateOutMode").ToString()


            bln_007EIRNo_Key = objCommonConfig.IsKeyExists
            If e.Row.RowType = DataControlRowType.Header Then
                If bln_007EIRNo_Key Then
                    CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = str_007EIRNo
                End If
                str_022RentalBit = objCommonConfig.pub_GetConfigSingleValue("022", intDepotID)
                bln_022RentalBit_Key = objCommonConfig.IsKeyExists

                If bln_022RentalBit_Key Then
                    If str_022RentalBit.ToLower = "false" OrElse objCommondata.GetDepotID <> objCommondata.GetHeadQuarterID Then
                        CType(e.Row.Cells(19), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "hide", 19, str_022RentalBit)
                        'ifgEquipmentDetail.Columns.Item(19).ControlStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(19).ItemStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(19).HeaderStyle.CssClass = "hide"

                    Else
                        CType(e.Row.Cells(19), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "hide", 19, "true")
                        'ifgEquipmentDetail.Columns.Item(19).ControlStyle.CssClass = "show"
                        'ifgEquipmentDetail.Columns.Item(19).ItemStyle.CssClass = "show"
                        'ifgEquipmentDetail.Columns.Item(19).HeaderStyle.CssClass = "show"
                    End If
                End If

                If str_059Gateout <> Nothing AndAlso str_059Gateout.ToString().ToUpper() = "FALSE" Then
                    ifgEquipmentDetail.Columns.Item(4).Visible = False
                    ifgEquipmentDetail.Columns.Item(6).Visible = False
                    ifgEquipmentDetail.Columns.Item(14).Visible = False
                    ifgEquipmentDetail.Columns.Item(12).Visible = False
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 4, str_059Gateout)
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 6, str_059Gateout)
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 14, str_059Gateout)
                    'CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")
                    'CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")
                    'CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")

                    If strMode.ToUpper() = "NEW" Then
                        CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "block")
                    End If

                Else
                    ifgEquipmentDetail.Columns.Item(4).Visible = True
                    ifgEquipmentDetail.Columns.Item(6).Visible = True
                    ifgEquipmentDetail.Columns.Item(14).Visible = True
                    If strMode.ToUpper() = "NEW" Then
                        CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                    Else
                        CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 4, str_059Gateout)
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 6, str_059Gateout)
                    'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 14, str_059Gateout)
                    'CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "block")
                    'CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "block")
                    'CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "block")

                    If strMode.ToUpper() = "EDIT" Then
                        ifgEquipmentDetail.Columns.Item(10).Visible = True
                        objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 10, "true")
                        'CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "block")
                    Else
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "show", 10, "false")
                        ' CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")
                        ifgEquipmentDetail.Columns.Item(10).Visible = False
                    End If

                End If



            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If bln_022RentalBit_Key Then
                    If str_022RentalBit.ToLower = "false" Then
                        CType(e.Row.Cells(19), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "hide", 19, str_022RentalBit)
                        'ifgEquipmentDetail.Columns.Item(19).ControlStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(19).ItemStyle.CssClass = "hide"
                        'ifgEquipmentDetail.Columns.Item(19).HeaderStyle.CssClass = "hide"
                    Else
                        'objCommondata.SetGridVisibilitybyIndex(ifgEquipmentDetail, "hide", 19, "true")
                        ifgEquipmentDetail.Columns.Item(19).Visible = True
                        ifgEquipmentDetail.Columns.Item(19).IsEditable = False

                        CType(e.Row.Cells(19), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(19), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = False
                        'ifgEquipmentDetail.Columns.Item(19).ControlStyle.CssClass = "show"
                        'ifgEquipmentDetail.Columns.Item(19).ItemStyle.CssClass = "show"
                        'ifgEquipmentDetail.Columns.Item(19).HeaderStyle.CssClass = "show"
                    End If
                End If
                If str_059Gateout <> Nothing AndAlso str_059Gateout.ToString().ToUpper() = "FALSE" Then
                    'CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")
                    'CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")
                    'CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")
                    ifgEquipmentDetail.Columns.Item(4).Visible = False
                    ifgEquipmentDetail.Columns.Item(6).Visible = False
                    ifgEquipmentDetail.Columns.Item(12).Visible = False
                    ifgEquipmentDetail.Columns.Item(14).Visible = False
                    If strMode.ToUpper() = "NEW" Then
                        ifgEquipmentDetail.Columns.Item(10).Visible = True
                        '   CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "block")
                        'Else
                        '    CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "block")
                    End If
                Else
                    'CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "block")
                    'CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "block")
                    'CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "block")
                    ifgEquipmentDetail.Columns.Item(4).Visible = True
                    ifgEquipmentDetail.Columns.Item(6).Visible = True
                    ifgEquipmentDetail.Columns.Item(14).Visible = True

                     If strMode.ToUpper() = "EDIT" Then
                        'CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "block")
                        ifgEquipmentDetail.Columns.Item(10).Visible = True
                    Else
                        ' CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).Style.Add("display", "none")
                        ifgEquipmentDetail.Columns.Item(10).Visible = False
                    End If

                End If

                'EIR No

               

                Dim drvBill As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim strEq As String = drvBill.Item(GateOutData.EQPMNT_NO).ToString

                If Not IsDBNull(drvBill.Item(GateOutData.BLLNG_FLG)) Then
                    If drvBill.Item(GateOutData.BLLNG_FLG).ToString = "B" Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        '  CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(11), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(13), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(15), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(16), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                        CType(e.Row.Cells(17), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = False
                    Else
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        ' CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(11), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(13), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(15), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(16), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = False
                        CType(e.Row.Cells(17), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = True
                    End If

                End If



                'Added for Attchement
                Dim hypPhotoUpload As Image
                Dim GateInID As String = drvBill(GateOutData.GTN_ID).ToString
                Dim GateOutId As String = drvBill(GateOutData.GTOT_ID).ToString
                hypPhotoUpload = CType(e.Row.Cells(18).Controls(0), Image)
                If GateInID <> "" Then
                    ' GateOutId = GateInID
                End If
                hypPhotoUpload.Attributes.Add("onclick", "showPhotoUpload('" + e.Row.RowIndex.ToString + "','" + GateOutId + "','" + GateInID + "' );return false;")
                hypPhotoUpload.ToolTip = "Attach Files"
                hypPhotoUpload.ImageUrl = "../Images/attachment.png"
                hypPhotoUpload.Attributes.Add("style", "cursor:pointer;margin-left:5px;")

                Dim imgFileUpload As Image
                imgFileUpload = CType(e.Row.Cells(18).Controls(0), Image)
                imgFileUpload.ToolTip = "Attach Files"
                If Not IsDBNull(drvBill.Item(GateOutData.COUNT_ATTACH)) Then
                    If Not CInt(drvBill.Item(GateOutData.COUNT_ATTACH)) > 0 Then
                        imgFileUpload.ImageUrl = "../Images/noattachment.png"
                    Else
                        imgFileUpload.ImageUrl = "../Images/attachment.png"
                    End If
                Else
                    imgFileUpload.ImageUrl = "../Images/noattachment.png"
                End If

                ' AboveAdded for Attchement

            End If
            Dim imgLink As Image
            Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If Not e.Row.DataItem Is Nothing Then
                '  Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                imgLink = CType(e.Row.Cells(16).Controls(0), Image)
                imgLink.ToolTip = "More Info"
                imgLink.ImageUrl = "../Images/info.png"
                Dim strEquipmentNo As String = String.Empty
                Dim strGateInID As String = String.Empty
                Dim strGateTransID As String = String.Empty
                Dim objgateout As New GateOut
                Dim dsGateinId As DataSet
                If Not drv.Row.Item(GateOutData.EQPMNT_NO) Is DBNull.Value Then
                    strEquipmentNo = CStr(drv.Row.Item(GateinData.EQPMNT_NO))
                End If
                If Not drv.Row.Item(GateOutData.GI_TRNSCTN_NO) Is DBNull.Value Then
                    strGateTransID = CStr(drv.Row.Item(GateOutData.GI_TRNSCTN_NO))
                    dsGateinId = objgateout.getGateinId(strGateTransID)
                    strGateInID = CStr(dsGateinId.Tables(GateOutData._GATEIN).Rows(0).Item(GateinData.GTN_ID))
                End If
                imgLink.Attributes.Add("onclick", String.Concat("openMoreInfo('", strGateInID, "','", strEquipmentNo, "');return false;"))
                imgLink.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                imgLink = CType(e.Row.Cells(16).Controls(0), Image)
                If IsDBNull(drv.Row.Item(GateOutData.GI_TRNSCTN_NO)) AndAlso CBool(drv.Row.Item(GateOutData.RNTL_BT)) Then
                    imgLink.ImageUrl = "../Images/info_gray.png"
                    imgLink.Attributes.Add("onclick", "")
                End If

                Dim chkActive As iFgCheckBox
                chkActive = CType(e.Row.Cells(17).Controls(0), iFgCheckBox)
                chkActive.Attributes.Add("onClick", String.Concat("GOlockData(this,'", strEquipmentNo, "');"))

            End If
            ''Added for Attchement
            'Dim hypPhotoUpload As Image
            'Dim GateInID As String = drv(GateOutData.GTN_ID).ToString
            'Dim GateOutId As String = drv(GateOutData.GTOT_ID).ToString
            'hypPhotoUpload = CType(e.Row.Cells(14).Controls(0), Image)
            'If GateInID <> "" Then
            '    ' GateOutId = GateInID
            'End If
            'hypPhotoUpload.Attributes.Add("onclick", "showPhotoUpload('" + e.Row.RowIndex.ToString + "','" + GateOutId + "' );return false;")
            'hypPhotoUpload.ToolTip = "Attach Files"
            'hypPhotoUpload.ImageUrl = "../Images/attachment.png"
            'hypPhotoUpload.Attributes.Add("style", "cursor:pointer;margin-left:5px;")

            'Dim imgFileUpload As Image
            'imgFileUpload = CType(e.Row.Cells(14).Controls(0), Image)
            'imgFileUpload.ToolTip = "Attach Files"
            'If Not IsDBNull(drv.Item(GateOutData.COUNT_ATTACH)) Then
            '    If Not CInt(drv.Item(GateOutData.COUNT_ATTACH)) > 0 Then
            '        imgFileUpload.ImageUrl = "../Images/noattachment.png"
            '    Else
            '        imgFileUpload.ImageUrl = "../Images/attachment.png"
            '    End If
            'Else
            '    imgFileUpload.ImageUrl = "../Images/noattachment.png"
            'End If

            '' AboveAdded for Attchement
            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then
                Dim datControl As iDate
                datControl = CType(DirectCast(DirectCast(e.Row.Cells(8),  _
                            iFgFieldCell).ContainingField,  _
                            DateField).iDate, iDate)
                datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                'CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Enabled = False

                '-----------------------added by sindhu for uig fix in chrome to make the column readonly------------
                Dim chkStatus As New iInterchange.WebControls.v4.Data.iFgCheckBox
                '  chkStatus = CType(e.Row.Cells(14).Controls(0), iInterchange.WebControls.v4.Data.iFgCheckBox)
                chkStatus = CType(e.Row.Cells(19).Controls(0), iInterchange.WebControls.v4.Data.iFgCheckBox)
                chkStatus.Enabled = False

                '-----------------------------------------------------------------------------------------------------
                If bln_022RentalBit_Key Then
                    If str_022RentalBit = "False" OrElse objCommondata.GetDepotID <> objCommondata.GetHeadQuarterID Then
                        ' CType(e.Row.Cells(14), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                        CType(e.Row.Cells(19), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = False
                    Else
                        CType(e.Row.Cells(19), iInterchange.WebControls.v4.Data.iFgFieldCell).Visible = True
                    End If
                End If

            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgEquipmentDetail, "ITab1_0")
    End Sub

#End Region

#Region "pvt_GetData"
    Private Sub pvt_GetData()
        Try
            Dim objCommon As New CommonData()
            Dim datGetDateTime As DateTime = CDate(objCommon.GetCurrentDate())
            Dim sbGateOut As New StringBuilder
            pub_SetCallbackReturnValue("Message", sbGateOut.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "ifgEquipmentDetail_RowUpdating"
    Protected Sub ifgEquipmentDetail_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgEquipmentDetail.RowUpdating
        Try
            If Not e.NewValues(GateOutData.GO_EIR_TIME) Is Nothing Then
                Dim strOUTWARDTime As String = CStr(e.NewValues(GateOutData.GO_EIR_TIME))
                Dim strHours As String = strOUTWARDTime.Substring(0, 2)
                Dim strMinutes As String = strOUTWARDTime.Substring(3, 2)
                Dim datOldDatetime As DateTime = CDate(e.NewValues(GateOutData.EIR_DT))
                Dim datNewDatetime As New DateTime(datOldDatetime.Year, datOldDatetime.Month, datOldDatetime.Day, CInt(strHours), CInt(strMinutes), 0)
                e.NewValues(GateOutData.EIR_DT) = datNewDatetime
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "GateOutLocking"
    Private Sub pvt_GOlockData(ByVal bv_strCheckBitFlag As String, _
                                ByVal bv_strEquipmentNo As String, _
                                ByRef LockBit As String)
        Try
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
            strCurrentSessionId = objCommonData.GetSessionID()
            strCurrentUserName = objCommonData.GetCurrentUserName()
            strCurrentIpAddress = GetClientIPAddress()
            strMode = CType(RetrieveData(GateOutMode), String)
            Dim strActivity As String = ""
            If strMode = MODE_EDIT Then
                strEquipmentStatus = "OUT"
            Else
                strEquipmentStatus = "AVL,STR"
            End If


            Dim strGateOutApprovalProcess As String = Nothing
            Dim objCommonConfig As New ConfigSetting()
            Dim objCommon As New CommonData
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())

            strGateOutApprovalProcess = objCommonConfig.pub_GetConfigSingleValue("066", intDPT_ID)

            If strGateOutApprovalProcess <> Nothing AndAlso CBool(strGateOutApprovalProcess) = True Then
                If strMode = MODE_EDIT Then
                    strEquipmentStatus = "OUT"
                Else
                    strEquipmentStatus = "APP"
                End If
            End If


            LockBit = "FALSE"
            If bv_strCheckBitFlag.ToUpper = "TRUE" Then
                strCurrentEquipmentStatus = objCommonUI.pub_GetEquipmentStaus(bv_strEquipmentNo, intDepotID)
                blnLockData = objCommonData.StoreLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentUserName, strCurrentSessionId, "Gate Out", strCurrentIpAddress, True)
                If Not strEquipmentStatus.Contains(strCurrentEquipmentStatus) And strCurrentEquipmentStatus <> Nothing Then
                    strErrorMessage = String.Concat("This equipment(", bv_strEquipmentNo, ") is already submitted by another user.")
                Else
                    If blnLockData Then
                        ''Get Activity Name
                        Dim blnGetLock As Boolean = objCommonData.GetLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, strCurrentUserName, strActivity)                        ''
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
                objCommonData.FlushLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, "Gate Out")
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

#Region "pvt_ValidateGateINAttachment"
    Private Sub pvt_ValidateGateINAttachment(ByVal bv_strGateINID As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objGateIn As New GateOut
            Dim dsGateOutAttchemnt As GateOutDataSet
            dsGateOutAttchemnt = objGateIn.pub_GetAttchemntbyGateIN(CInt(bv_strGateINID), "GateIn")
            If CInt(dsGateOutAttchemnt.Tables(GateOutData._V_GATEOUT).Rows(0).Item("COUNT_ATTACH")) > 0 Then
                pub_SetCallbackReturnValue("Message", "Yes")
            Else
                pub_SetCallbackReturnValue("Message", "No")
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class