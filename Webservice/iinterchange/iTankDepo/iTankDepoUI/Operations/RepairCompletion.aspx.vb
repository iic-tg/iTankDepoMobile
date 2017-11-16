
Option Strict On
Partial Class Operations_RepairCompletion
    Inherits Pagebase
    Dim dsRepairCompletion As New RepairCompletionDataSet
    Dim dtRepairEstimateData As DataTable
    Private Const REPAIR_COMPLETION As String = "REPAIR_COMPLETION"
    Private Const REPAIR_COMPLETION_DOCUMENT As String = "REPAIR_COMPLETION_DOCUMENT"
    Private strMSGUPDATE As String = "Repair Completion : Equipment(s) Updated Successfully."
    Private Const RE_ESTIMATE_PAGE_TITLE = "Repair Re-Estimate Completion"
    Dim bln_020KeyExist As Boolean
    Dim str_020KeyValue As String
    Dim str_056GWS As String
    Dim bln_056GWSActive_Key As Boolean

#Region "Page_OnCallback"

    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "UpdateRepairCompletion"
                    pvt_UpdateRepairCompletion(e.GetCallbackValue("WFData"), _
                                               e.GetCallbackValue("PageTitle"), _
                                               e.GetCallbackValue("RevisionNo"), _
                                               e.GetCallbackValue("Mode"))
                Case "ValidatePreviousActivityDate"
                    pvt_ValidatePreviousActivityDate(e.GetCallbackValue("EquipmentNo"), _
                                                     e.GetCallbackValue("EventDate"))
                Case "RClockData"
                    pvt_RClockData(e.GetCallbackValue("CheckBit"), _
                                  e.GetCallbackValue("EquipmentNo"), _
                                  e.GetCallbackValue("LockBit"))
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
            CommonWeb.IncludeScript("Script/Operations/RepairCompletion.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
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
            Dim objRepairCompletion As New RepairCompletion
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData
            Dim dsCommonUI As New CommonUIDataSet

            If bv_strEquipmentNo <> Nothing And bv_strEventDate <> Nothing Then
                blnDateValid = objCommonUI.pub_GetEquipmentPreviousActivityDate(bv_strEquipmentNo, _
                                                                         CDate(bv_strEventDate), _
                                                                         dtPreviousDate, _
                                                                         "Repair Completion", _
                                                                         CInt(objCommon.GetDepotID()))
                If blnDateValid = True Then
                    pub_SetCallbackReturnValue("Error", String.Concat("Equipment's Activity Date Should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), ")"))
                End If
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_UpdateRepairCompletion"
    Private Sub pvt_UpdateRepairCompletion(ByVal bv_strWfData As String, _
                                           ByVal bv_strPagetitle As String, _
                                           ByVal bv_strRevisionNo As String, _
                                           ByVal bv_strMode As String)
        Try
            Dim dtRepairCompltion As DataTable
            Dim objcommon As New CommonData
            Dim objCommonUI As New CommonUI
            Dim intDepotId As Integer = CommonWeb.iInt(objcommon.GetDepotID())
            Dim strCurrentUserName As String = objcommon.GetCurrentUserName()
            Dim datModifiedDate As String = objcommon.GetCurrentDate()
            Dim drRP As DataRow
            Dim blnEdI As Boolean = CBool(ConfigurationManager.AppSettings("GenerateEDI"))
            dsRepairCompletion = CType(RetrieveData(REPAIR_COMPLETION), RepairCompletionDataSet)
            Dim objRepairEstimate As New RepairCompletion
            Dim drRepairCompletion As DataRow()
            drRepairCompletion = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(RepairCompletionData.CHECKED & "='True'")
            dtRepairCompltion = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Clone()
            If Not drRepairCompletion.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
            End If

            ''Lock Implementation - Unlock after submit
            ''Lock Implementation 
            For Each dr As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(RepairCompletionData.CHECKED, "= 'True'"))
                pvt_RClockData("FALSE", dr.Item(RepairCompletionData.EQPMNT_NO).ToString, "FALSE")
            Next



            Dim str_067InvoiceGenerationGWSBit As String = Nothing
            Dim objCommonConfig As New ConfigSetting()

            str_067InvoiceGenerationGWSBit = objCommonConfig.pub_GetConfigSingleValue("067", intDepotId)




            objRepairEstimate.pub_UpdateRepairCompletion(dsRepairCompletion, _
                                                         bv_strWfData, _
                                                         bv_strRevisionNo, _
                                                         CommonUIs.iDat(datModifiedDate), _
                                                         bv_strMode, _
                                                         intDepotId, _
                                                         strCurrentUserName, _
                                                         str_067InvoiceGenerationGWSBit)

            dsRepairCompletion.AcceptChanges()
            For Each dr As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(RepairCompletionData.CHECKED, "=", "True"))
                drRP = dtRepairCompltion.NewRow()
                drRP(RepairCompletionData.RPR_ESTMT_ID) = dr(RepairCompletionData.RPR_ESTMT_ID)
                drRP(RepairCompletionData.EQPMNT_NO) = dr(RepairCompletionData.EQPMNT_NO)
                drRP(RepairCompletionData.RPR_ESTMT_NO) = dr(RepairCompletionData.RPR_ESTMT_NO)
                drRP(RepairCompletionData.ACTVTY_DT) = dr(RepairCompletionData.RPR_CMPLTN_DT)
                drRP(RepairCompletionData.YRD_LCTN) = dr(RepairCompletionData.YRD_LCTN)
                drRP(RepairCompletionData.CSTMR_CD) = dr(RepairCompletionData.CSTMR_CD)
                drRP(RepairCompletionData.INVCNG_PRTY_CD) = dr(RepairCompletionData.INVCNG_PRTY_CD)
                drRP(RepairCompletionData.PRDCT_CD) = dr(RepairCompletionData.PRDCT_CD)
                drRP(RepairCompletionData.CSTMR_NAM) = dr(RepairCompletionData.CSTMR_NAM)
                drRP(RepairCompletionData.RPR_TYP_CD) = dr(RepairCompletionData.RPR_TYP_CD)
                drRP(RepairCompletionData.DPT_NAM) = dr(RepairCompletionData.DPT_NAM)
                drRP(RepairCompletionData.DPT_CD) = dr(RepairCompletionData.DPT_CD)
                dtRepairCompltion.Rows.Add(drRP)
            Next
            dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Clear()
            dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Merge(dtRepairCompltion)
            CacheData(REPAIR_COMPLETION, dsRepairCompletion)
            CacheData(REPAIR_COMPLETION_DOCUMENT, dsRepairCompletion)
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
            Dim objcommon As New CommonData()
            Dim objCommonConfig As New ConfigSetting()
            Dim dtActivityStatus As New DataTable
            Dim objRepairCompletion As New RepairCompletion()
            Dim strMode As String = ""
            Dim intDepotID As Integer = CInt(objcommon.GetDepotID())
            hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
            Dim bln_056GwsBit As Boolean
            Dim str_056GWSKey As String
            str_056GWSKey = objCommonConfig.pub_GetConfigSingleValue("056", intDepotID)
            bln_056GwsBit = objCommonConfig.IsKeyExists
            If Not e.Parameters("Mode") Is Nothing Then
                Dim strCurrentSessionId As String = objcommon.GetSessionID()
                objcommon.FlushLockDataByActivity(strCurrentSessionId, "Repair Completion")

                hdnMode.Value = e.Parameters("Mode").ToString()
                Select Case e.Parameters("Mode").ToString()
                    Case MODE_NEW
                        strMode = MODE_NEW
                        Dim objCommonUI As New CommonUI()
                        Dim dsEqpStatus As New DataSet
                        Dim blnShowEqStatus As Boolean = False
                        dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Repair Completion", True, intDepotID)
                        If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                            blnShowEqStatus = True
                        End If
                        If bln_056GwsBit Then
                            If str_056GWSKey.ToLower = "true" Then
                                dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimatePendingByDepotId(intDepotID)
                            Else
                                dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimatePendingJTSByDepotId(intDepotID)
                            End If
                        End If

                        For Each drRepairCompletion As DataRow In dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Rows
                            drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_DT) = hdnCurrentDate.Value
                            drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM) = "00:00"
                            If bln_056GwsBit Then
                                If str_056GWSKey.ToLower = "true" Then
                                    drRepairCompletion.Item(RepairCompletionData.RPR_CMPLTN_TM) = DateTime.Now.ToString("H:mm")
                                End If
                            End If
                        Next
                    Case MODE_EDIT
                        strMode = MODE_EDIT
                        If bln_056GwsBit Then
                            If str_056GWSKey.ToLower = "true" Then
                                dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimateMySubmitByDepotId(intDepotID)
                            Else
                                dsRepairCompletion = objRepairCompletion.pub_GetRepairEstimateMySubmitJTSByDepotId(intDepotID)
                            End If
                        End If
                    Case "ReBind"
                        dsRepairCompletion = CType(RetrieveData(REPAIR_COMPLETION), RepairCompletionDataSet)
                        Dim lngRepairEstimateId As Long = 0
                        Dim intFilesCount As Integer = 0
                        Dim drRepairCompletion As DataRow() = Nothing
                        If Not e.Parameters("RepairEstimateId") Is Nothing Then
                            lngRepairEstimateId = CLng(e.Parameters("RepairEstimateId"))
                            drRepairCompletion = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE).Select(String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = ", lngRepairEstimateId))
                            If drRepairCompletion.Length > 0 Then
                                intFilesCount = CInt(dsRepairCompletion.Tables(RepairCompletionData._ATTACHMENT).Compute(String.Concat("COUNT(", RepairCompletionData.RPR_ESTMT_ID, ")"), String.Concat(RepairCompletionData.RPR_ESTMT_ID, " = '", lngRepairEstimateId, "'")))
                                drRepairCompletion(0).Item(RepairCompletionData.COUNT_ATTACH) = intFilesCount
                            End If
                        End If
                End Select
            End If
            str_056GWS = objCommonConfig.pub_GetConfigSingleValue("056", intDepotID)
            bln_056GWSActive_Key = objCommonConfig.IsKeyExists
            If str_056GWS.ToString = "True" Then
                objcommon.SetGridVisibilitybyIndex(ifgEquipmentDetail, "hide", 6, str_056GWS)
            End If



            e.DataSource = dsRepairCompletion.Tables(RepairCompletionData._V_REPAIR_ESTIMATE)
            e.OutputParameters.Add("Mode", hdnMode.Value)
            CacheData("Mode", hdnMode.Value)
            CacheData(REPAIR_COMPLETION, dsRepairCompletion)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_RowDataBound"
    Protected Sub ifgEquipmentDetail_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgEquipmentDetail.RowDataBound
        Try
            Dim strEquipmentNo As String = ""
            Dim objCommonData As New CommonData
            Dim objCommon As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim blnkey As Boolean
            Dim intDepotID As Integer = CInt(objCommonData.GetDepotID())
            str_056GWS = objCommonConfig.pub_GetConfigSingleValue("056", intDepotID)
            bln_056GWSActive_Key = objCommonConfig.IsKeyExists
            If str_056GWS.ToLower = "true" Then
                objCommon.SetGridVisibilitybyIndex(ifgEquipmentDetail, "hide", 6, "false")
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                'Hide Repair Type


                Dim datControl As iDate
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                '' 
                strEquipmentNo = drv(RepairCompletionData.EQPMNT_NO).ToString()


                ''
                datControl = CType(DirectCast(DirectCast(e.Row.Cells(10),  _
                            iFgFieldCell).ContainingField,  _
                            DateField).iDate, iDate)
                datControl.Validator.CmpErrorMessage = "Completion Date should not be greater than Current Date."
                datControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper

                Dim strMode As String = RetrieveData("Mode").ToString
                If strMode = MODE_EDIT Then
                    If Not drv.Row.Item(RepairCompletionData.BLLNG_FLG) Is DBNull.Value Then
                        If (CInt(drv.Row.Item(RepairCompletionData.BLLNG_FLG)) > 0) Then
                            CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(1), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(2), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(3), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(5), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(6), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(7), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(8), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(9), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(10), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(11), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            CType(e.Row.Cells(12), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                            'Dim hypBllng As Image
                            'hypBllng = CType(e.Row.Cells(13).Controls(0), Image)
                            'hypBllng.ToolTip = "Attach Files"
                            'hypBllng.ImageUrl = "../Images/attachment.png"
                            'hypBllng.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                            'hypBllng.Attributes.Add("onclick", "")
                            'CType(e.Row.Cells(14).Controls(0), iInterchange.WebControls.v4.Data.iFgCheckBox).Enabled = False
                            ' chk.Visible = False
                        Else
                            'Dim hypPhotoUpload As Image
                            'Dim RepairEstimateId As String = drv(RepairCompletionData.RPR_ESTMT_ID).ToString
                            'hypPhotoUpload = CType(e.Row.Cells(13).Controls(0), Image)
                            'hypPhotoUpload.Attributes.Add("onclick", "showPhotoUpload('" + e.Row.RowIndex.ToString + "','" + RepairEstimateId + "' );return false;")
                            'hypPhotoUpload.ToolTip = "Attach Files"
                            'hypPhotoUpload.ImageUrl = "../Images/attachment.png"
                            'hypPhotoUpload.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                            'CType(e.Row.Cells(14).Controls(0), iInterchange.WebControls.v4.Data.iFgCheckBox).Enabled = True
                        End If

                    End If
                End If

                'In RPC - Always user can perform attachments
                Dim hypPhotoUpload As Image
                Dim RepairEstimateId As String = drv(RepairCompletionData.RPR_ESTMT_ID).ToString
                hypPhotoUpload = CType(e.Row.Cells(13).Controls(0), Image)
                hypPhotoUpload.Attributes.Add("onclick", "showPhotoUpload('" + e.Row.RowIndex.ToString + "','" + RepairEstimateId + "' );return false;")
                hypPhotoUpload.ToolTip = "Attach Files"
                hypPhotoUpload.ImageUrl = "../Images/attachment.png"
                hypPhotoUpload.Attributes.Add("style", "cursor:pointer;margin-left:5px;")
                CType(e.Row.Cells(14).Controls(0), iInterchange.WebControls.v4.Data.iFgCheckBox).Enabled = True

                Dim imgFileUpload As Image
                imgFileUpload = CType(e.Row.Cells(13).Controls(0), Image)
                imgFileUpload.ToolTip = "Attach Files"
                If Not IsDBNull(drv.Item(RepairCompletionData.COUNT_ATTACH)) Then
                    If Not CInt(drv.Item(RepairCompletionData.COUNT_ATTACH)) > 0 Then
                        imgFileUpload.ImageUrl = "../Images/noattachment.png"
                    Else
                        imgFileUpload.ImageUrl = "../Images/attachment.png"
                    End If
                Else
                    imgFileUpload.ImageUrl = "../Images/noattachment.png"
                End If

                Dim chkActive As iFgCheckBox
                chkActive = CType(e.Row.Cells(14).Controls(0), iFgCheckBox)
                chkActive.Attributes.Add("onClick", String.Concat("RClockData(this,'", strEquipmentNo, "');"))

            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "RepairCompletionlockData"
    Private Sub pvt_RClockData(ByVal bv_strCheckBitFlag As String, _
                                ByVal bv_strEquipmentNo As String, _
                                ByRef LockBit As String)
        Try
            Dim objCommonData As New CommonData
            Dim objCommonUI As New CommonUI
            Dim objChangeOfStatus As New ChangeOfStatus
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
            Dim strMode As String = RetrieveData("Mode").ToString
            Dim strActivity As String = ""

            If strMode = MODE_EDIT Then
                strEquipmentStatus = "RPC"
            Else
                strEquipmentStatus = "AUR"
            End If


            LockBit = "FALSE"
            If bv_strCheckBitFlag.ToUpper = "TRUE" Then
                strCurrentEquipmentStatus = objCommonUI.pub_GetEquipmentStaus(bv_strEquipmentNo, intDepotID)
                blnLockData = objCommonData.StoreLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentUserName, strCurrentSessionId, "Repair Completion", strCurrentIpAddress, True)
                'If strCurrentEquipmentStatus <> strEquipmentStatus Then
                If strCurrentEquipmentStatus <> strEquipmentStatus AndAlso strCurrentEquipmentStatus.ToUpper() <> "INS" Then
                    ' strErrorMessage = "Repair Completion already done for this Equipment."
                    strErrorMessage = ""
                Else
                    If blnLockData Then
                        LockBit = "TRUE"
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
                objCommonData.FlushLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentSessionId, "Repair Completion")
            End If
            pub_SetCallbackReturnValue("Activity", strActivity)
                pub_SetCallbackReturnValue("ErrorMessage", strErrorMessage)
                pub_SetCallbackStatus(True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

End Class
