
Partial Class Operations_EquipmentUpdate
    Inherits Pagebase

#Region "Declarations"
    Dim dsEquipmentUpdate As New EquipmentUpdateDataSet
    Private Const EQUIPMENT_UPDATE As String = "EQUIPMENT_UPDATE"
    Private strMSGUPDATE As String = "Equipment Updated Successfully."
    Private strMSGGateIn As String = "Effective From date should be greater than or equal to Gate In Date."
    Private strMSGEffectiveFromDate As String = "Effective From date should be greater than or equal to Last Effective From Date."
    Private strMSGCurrentDate As String = "Effective From date should be less than or equal to Current Date."
    Private strMSGActivity As String = "Effective From date should be greater than Last Activity Date."
    Private strMSGActivityPrograss As String = "Equipment already prograss for (Handling & Storage / Repair / Cleaning / Heating) charges."
    Private strMSGRental As String = "Equipment is mapped for Rental, unable to change the customer."
    Private strMSGInvoice As String = "Effective From date should be greater than Last Invoice Date."
    Private strNoChanges As String = "No Changes to Save."
    Dim dtAdditionalData As New DataTable
    Private Const EQUIPMENT_ADDITIONAL As String = "EQUIPMENT_ADDITIONAL"
    Dim objCommon As New CommonUI
    Dim objCommonConfig As New ConfigSetting()
    Dim bln_006Active_Key As Boolean
    Dim str_006KeyValue As String
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/EquipmentUpdate.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                Dim objCommon As New CommonData
                hdnDepot.Value = objCommon.GetDepotID()
                pvt_SetChangesMade()
                datEffectiveFrom.Validator.ValueToCompare = DateTime.Now
                Dim strSessionId As String = objCommon.GetSessionID()
                Dim strActivityName As String = String.Empty
                If Not pub_GetQueryString("activityname") Is Nothing Then
                    strActivityName = pub_GetQueryString("activityname")
                End If
                objCommon.FlushLockDataByActivityName(CleaningData.EQPMNT_NO, strSessionId, strActivityName)
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
                Case "UpdateEquipmentUpdate"
                    pvt_UpdateEquipmentUpdate(e.GetCallbackValue("NewEquipmentNo"), _
                                              e.GetCallbackValue("NewCustomerId"), _
                                              e.GetCallbackValue("NewCustomerCd"), _
                                              e.GetCallbackValue("NewTypeId"), _
                                              e.GetCallbackValue("NewTypeCd"), _
                                              e.GetCallbackValue("NewCodeId"), _
                                              e.GetCallbackValue("NewCodeCd"), _
                                              e.GetCallbackValue("oldEquipmentNo"), _
                                              e.GetCallbackValue("oldTypeId"), _
                                              e.GetCallbackValue("oldTypeCd"), _
                                              e.GetCallbackValue("oldCodeId"), _
                                              e.GetCallbackValue("oldCodeCd"), _
                                              e.GetCallbackValue("oldCustomerId"), _
                                              e.GetCallbackValue("oldCustomerCd"), _
                                              e.GetCallbackValue("BillingBit"), _
                                               e.GetCallbackValue("EffectiveFromDate"), _
                                              e.GetCallbackValue("Reason"))
                Case "checkHasChanges"
                    pvt_checkHasChanges(e.GetCallbackValue("NewEquipmentNo"), _
                                        e.GetCallbackValue("NewCustomerCd"), _
                                        e.GetCallbackValue("NewTypeCd"), _
                                        e.GetCallbackValue("NewCodeCd"), _
                                        e.GetCallbackValue("oldEquipmentNo"), _
                                        e.GetCallbackValue("bv_strOldCustomerId"), _
                                        e.GetCallbackValue("NewCustomerId"))

                Case "ValidateEquipment"
                    pvt_ValidateEquipment(e.GetCallbackValue("EquipmentNo"))

                Case "validateEffective_Date"
                    validateEffective_Date(e.GetCallbackValue("OldEquipmentNo"), e.GetCallbackValue("EquipmentNo"), e.GetCallbackValue("EffectiveDate"))

                Case "validateRental"
                    validateRental(e.GetCallbackValue("OldEquipmentNo"))
                Case "RecordLockData"
                    pvt_GetLockData(e.GetCallbackValue("EquipmentNo"), _
                                    e.GetCallbackValue("ActivityName"))

                Case "GetEquipmentCode"
                    pvt_GetEquipmentCode(e.GetCallbackValue("Type"), e.GetCallbackValue("OldCode"))
                Case "GetEquipmentCodebyTypeId"
                    pvt_GetEquipmentCodebyTypeId(e.GetCallbackValue("TypeID"))

            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetEquipmentCode"

    Private Sub pvt_GetEquipmentCodebyTypeId(ByVal bv_strTypeId As String)
        Try
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData

            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = objCommon.GetHeadQuarterID()
            End If
            Dim dt As New DataTable

            dt = objCommonUI.GetEquipmentCodeByTypeID(bv_strTypeId, intDepotID)

            If dt.Rows.Count > 0 Then
                pub_SetCallbackReturnValue("Code", dt.Rows(0).Item(GateinData.EQPMNT_CD_CD).ToString())
                pub_SetCallbackReturnValue("ID", dt.Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString())
                pub_SetCallbackStatus(True)
            Else
                pub_SetCallbackStatus(False)
            End If

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

    Private Sub pvt_GetEquipmentCode(ByVal bv_strType As String, ByVal bv_strOldCode As String)
        Try
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData

            Dim intDepotID As Integer = CInt(objCommon.GetDepotID())
            Dim dt As New DataTable
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                intDepotID = objCommon.GetHeadQuarterID()
            End If

            dt = objCommonUI.GetEquipmentCodeByTypeWithoutCode(bv_strType, bv_strOldCode, intDepotID)

            If dt.Rows.Count > 0 Then
                pub_SetCallbackReturnValue("Code", dt.Rows(0).Item(GateinData.EQPMNT_CD_CD).ToString())
                pub_SetCallbackReturnValue("ID", dt.Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString())
                pub_SetCallbackStatus(True)
            Else
                pub_SetCallbackStatus(False)
            End If

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "validateRental"

    Private Sub validateRental(ByVal strOldEquipmentNo As String)

        Try
            Dim objCommon As New CommonData
            Dim objEquipment As New EquipmentUpdate
            Dim blnRental As Boolean
            Dim intDepotId As Int32

            intDepotId = objCommon.GetDepotID()

            blnRental = objEquipment.ValidateRentalStatus(strOldEquipmentNo, intDepotId)

            If blnRental = False Then
                pub_SetCallbackStatus(True)

            Else
                pub_SetCallbackStatus(False)
                pub_SetCallbackReturnValue("Message", strMSGRental)
            End If

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub

#End Region

#Region "validateEffective_Date"

    Private Sub validateEffective_Date(ByVal strOldEquipmentNo As String, ByVal strEquipmentNo As String, ByVal strEffectiveDate As String)

        Try

            Dim objEquipment As New EquipmentUpdate
            Dim str_GITransaction_No As String
            Dim str_LastActivityStatus As String
            Dim datLastInvc_dt As DateTime
            Dim datLastActvty_dt As DateTime
            Dim strActivityDt As String
            Dim intDepotId As Int32
            Dim objCommon As New CommonData
            Dim blnRental As Boolean
            Dim dt_LastEffectiveFrom_Date As DateTime
            Dim str_LastEffectiveFrom_Date As String

            intDepotId = objCommon.GetDepotID()


            str_GITransaction_No = objEquipment.Get_GateIn_GITransaction_No(strEquipmentNo, intDepotId)

            str_LastActivityStatus = objEquipment.get_LastActivityStatus(strEquipmentNo, intDepotId, str_GITransaction_No) 'Get Last Activity Status
            datLastInvc_dt = objEquipment.Get_LastInvoiceDate(strEquipmentNo, str_GITransaction_No, intDepotId) ' Get Last invoice Date
            strActivityDt = objEquipment.get_LastActivity_Date(strEquipmentNo, intDepotId, str_GITransaction_No) ' Get Last Activity Date


            str_LastEffectiveFrom_Date = objEquipment.get_EffectiveFrom_Date(intDepotId, str_GITransaction_No)

            If (Not String.IsNullOrEmpty(str_LastEffectiveFrom_Date) AndAlso Not String.IsNullOrWhiteSpace(str_LastEffectiveFrom_Date)) Then
                dt_LastEffectiveFrom_Date = str_LastEffectiveFrom_Date
            End If

            If (String.IsNullOrEmpty(strActivityDt) Or String.IsNullOrWhiteSpace(strActivityDt)) Then
                datLastActvty_dt = objEquipment.get_LastActivityStatus_Date(strEquipmentNo, intDepotId, str_GITransaction_No)
            Else
                datLastActvty_dt = strActivityDt
            End If

            blnRental = objEquipment.ValidateRentalStatus(strOldEquipmentNo, intDepotId)

            If CDate(strEffectiveDate) > DateTime.Today Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackReturnValue("Message", strMSGCurrentDate)
                Exit Sub
            End If

            If blnRental = False Then

                'Validate Gate In Date
                If str_LastActivityStatus.ToUpper() = "IND" Then

                    If CDate(strEffectiveDate) >= dt_LastEffectiveFrom_Date Then

                        If strEffectiveDate >= datLastActvty_dt Then
                            pub_SetCallbackStatus(True)
                        Else
                            pub_SetCallbackStatus(False)
                            pub_SetCallbackReturnValue("Message", strMSGGateIn)
                            Exit Sub
                        End If

                    Else
                        pub_SetCallbackStatus(False)
                        pub_SetCallbackReturnValue("Message", strMSGEffectiveFromDate)
                        Exit Sub
                    End If

                    If datLastActvty_dt = Nothing Then
                        datLastActvty_dt = dt_LastEffectiveFrom_Date
                    End If

                    'Validate Last Invoice or Last Activity Date
                ElseIf str_LastActivityStatus.ToUpper() = "AVL" Then

                    If datLastActvty_dt > datLastInvc_dt Then

                        If strEffectiveDate > datLastActvty_dt Then
                            pub_SetCallbackStatus(True)
                        Else
                            pub_SetCallbackStatus(False)
                            pub_SetCallbackReturnValue("Message", strMSGActivity)
                        End If

                    ElseIf datLastInvc_dt > datLastActvty_dt Then

                        If strEffectiveDate > datLastInvc_dt Then
                            pub_SetCallbackStatus(True)
                        Else
                            pub_SetCallbackStatus(False)
                            pub_SetCallbackReturnValue("Message", strMSGInvoice)
                        End If

                    ElseIf datLastInvc_dt = datLastActvty_dt Then

                        If strEffectiveDate > datLastActvty_dt Then
                            pub_SetCallbackStatus(True)
                        Else
                            pub_SetCallbackStatus(False)
                            pub_SetCallbackReturnValue("Message", strMSGActivity)
                        End If

                    End If

                Else

                    pub_SetCallbackStatus(False)
                    pub_SetCallbackReturnValue("Message", strMSGActivityPrograss)

                End If
            Else
                pub_SetCallbackStatus(False)
                pub_SetCallbackReturnValue("Message", strMSGRental)
            End If


        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub

#End Region

#Region "pvt_checkHasChanges"
    Private Sub pvt_checkHasChanges(ByVal bv_strNewEquipmentNo As String, _
                                    ByVal bv_strNewCustomerCd As String, _
                                    ByVal bv_strNewTypeCd As String, _
                                    ByVal bv_strNewCodeCd As String, _
                                    ByVal bv_stroldEquipmentNo As String, _
                                    ByVal bv_stroldCustomerId As String, _
                                    ByVal bv_strNewCustomerId As String)
        Try
            Dim dtEquipmentUpdate As New DataTable
            Dim objEquipmentUpdate As New EquipmentUpdate
            Dim strCustomerUpdate As String = "True"
            Dim blnCustomerChange As Boolean
            Dim intDepotId As Int32
            Dim objCommon As New CommonData
            intDepotId = objCommon.GetDepotID()
            dsEquipmentUpdate = CType(RetrieveData(EQUIPMENT_UPDATE), EquipmentUpdateDataSet)
            dtEquipmentUpdate = dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).GetChanges()
            If bv_strNewCustomerId <> Nothing Then
                Dim GITransaction_No As String = objEquipmentUpdate.Get_GateIn_GITransaction_No(bv_stroldEquipmentNo, intDepotId)
                blnCustomerChange = objEquipmentUpdate.pub_GetPreviousStorageChargeByGITransactionNo(GITransaction_No, intDepotId, bv_stroldCustomerId, bv_strNewCustomerId, strCustomerUpdate)
                If strCustomerUpdate = "Allow" Then
                    strCustomerUpdate = ""
                End If
                pub_SetCallbackReturnValue("Message", strCustomerUpdate)
            Else
                If dtEquipmentUpdate Is Nothing And bv_strNewEquipmentNo = "" And bv_strNewCustomerCd = "" And bv_strNewTypeCd = "" And bv_strNewCodeCd = "" Then
                    pub_SetCallbackReturnValue("Message", "True")
                End If
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        CommonWeb.pub_AttachHasChanges(txtNewEqpNo)
        CommonWeb.pub_AttachHasChanges(lkpCustomer)
        CommonWeb.pub_AttachHasChanges(lkpEqpType)
        'CommonWeb.pub_AttachHasChanges(lkpEqpCode)
        CommonWeb.pub_AttachHasChanges(datEffectiveFrom)
        pub_SetGridChanges(ifgEquipmentUpdate, "tabEquipmentUpdate")
    End Sub
#End Region

#Region "pvt_ValidateEquipment"
    Private Sub pvt_ValidateEquipment(ByVal bv_strEquipmentNo As String)
        Try
            Dim blndsValid As Boolean
            Dim objCommon As New CommonData
            Dim blnDuplicateEquipment As Boolean = True
            Dim blnPreadviceEquipment As Boolean = True
            Dim objEquipmentUpdate As New EquipmentUpdate
            Dim objGateIn As New Gatein
            blndsValid = objEquipmentUpdate.pub_ValidateEquipmentNoByDepotID(bv_strEquipmentNo, CInt(objCommon.GetDepotID()))

            If blndsValid = True Then
                blnPreadviceEquipment = objGateIn.pub_ValidateEquipmentNoInPreAdvice(bv_strEquipmentNo, CInt(objCommon.GetDepotID()))
                If blnPreadviceEquipment = False Then
                    'Newly added if Equiment No already available in Preadvice in some other depot                
                    pub_SetCallbackReturnValue("EquipmentNoInAnotherDepot", "false")
                End If

                blnDuplicateEquipment = objGateIn.pub_ValidateStatusOfEquipment(bv_strEquipmentNo, CInt(objCommon.GetDepotID()))
                If blnDuplicateEquipment = False Then
                    'Newly added if Equiment No already available in Preadvice in some other depot                
                    pub_SetCallbackReturnValue("StatusOfEquipment", "false")
                End If
            End If
            If blndsValid = True Then

                pub_SetCallbackReturnValue("bNotExists", "true")
            Else
                pub_SetCallbackReturnValue("bNotExists", "false")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_ClientBind"
    Protected Sub ifgEquipmentDetail_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgEquipmentUpdate.ClientBind
        Try
            Dim strActivityName As String = String.Empty
            Dim objcommonData As New CommonData
            Dim intDepotID As Integer = CInt(objcommonData.GetDepotID())

            If Not e.Parameters("ActivtyName") Is Nothing Then
                strActivityName = e.Parameters("ActivtyName").ToString()
            End If
            If e.Parameters("Mode") = "Search" Then
                Dim strEqType As String = String.Empty
                Dim strEqTypeId As String = String.Empty
                Dim strCodeId As String = String.Empty
                Dim strCustomerId As String = String.Empty
                Dim strEqCode As String = String.Empty
                Dim strActivityDate As String = String.Empty
                Dim strCurrentStatus As String = String.Empty
                Dim strCustomer As String = String.Empty
                Dim strGenBit As String = ""
                dsEquipmentUpdate.Tables.Clear()
                Dim objEquipmentUpdate As New EquipmentUpdate
                Dim strModifiedDate As String = objcommonData.GetCurrentDate()
                dsEquipmentUpdate = objEquipmentUpdate.pub_GetEquipmentInformationByEqpmntNo(e.Parameters("EquipmentNo").ToString(), intDepotID)
                dtAdditionalData = dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Copy()
                If dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Rows.Count > 0 Then
                    With dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).Rows(0)
                        If Not IsDBNull(.Item(EquipmentUpdateData.EQPMNT_TYP_CD)) Then
                            strEqType = CStr(.Item(EquipmentUpdateData.EQPMNT_TYP_CD))
                            strEqTypeId = CStr(.Item(EquipmentUpdateData.EQPMNT_TYP_ID))
                        End If
                        If Not IsDBNull(.Item(EquipmentUpdateData.EQPMNT_CD_CD)) Then
                            strEqCode = CStr(.Item(EquipmentUpdateData.EQPMNT_CD_CD))
                            strCodeId = CStr(.Item(EquipmentUpdateData.EQPMNT_CD_ID))
                        End If
                        If Not IsDBNull(.Item(EquipmentUpdateData.ACTVTY_DT)) Then
                            strActivityDate = CDate(.Item(EquipmentUpdateData.ACTVTY_DT)).ToString("dd-MMM-yyyy").ToUpper
                        End If
                        If Not IsDBNull(.Item(EquipmentUpdateData.EQPMNT_STTS_CD)) Then
                            strCurrentStatus = CStr(.Item(EquipmentUpdateData.EQPMNT_STTS_CD))
                        End If
                        If Not IsDBNull(.Item(EquipmentUpdateData.CSTMR_CD)) Then
                            strCustomer = CStr(.Item(EquipmentUpdateData.CSTMR_CD))
                            strCustomerId = CStr(.Item(EquipmentUpdateData.CSTMR_ID))
                        End If
                        If Not IsDBNull(.Item(EquipmentUpdateData.INVC_GNRT_BT)) Then
                            strGenBit = .Item(EquipmentUpdateData.INVC_GNRT_BT)
                        End If
                        e.OutputParameters.Add("Status", strCurrentStatus)
                        e.OutputParameters.Add("ActivityDate", strActivityDate)
                        e.OutputParameters.Add("Customer", strCustomer)
                        e.OutputParameters.Add("Type", strEqType)
                        e.OutputParameters.Add("Code", strEqCode)
                        e.OutputParameters.Add("TypeId", strEqTypeId)
                        e.OutputParameters.Add("CodeId", strEqTypeId)
                        e.OutputParameters.Add("CustomerId", strCustomerId)
                        If strCurrentStatus.ToUpper() = "AVL" Or strCurrentStatus.ToUpper() = "IND" Then
                            strGenBit = False
                        Else
                            strGenBit = True
                        End If

                        'Dim datLastInvc_or_Actvty_dt As DateTime
                        Dim LastActivity_Date As String
                        Dim strLastInvc_dt As DateTime
                        Dim str_GITransaction_No As String

                        str_GITransaction_No = objEquipmentUpdate.Get_GateIn_GITransaction_No(e.Parameters("EquipmentNo").ToString(), intDepotID) 'Get GI_Transaction No
                        strLastInvc_dt = objEquipmentUpdate.Get_LastInvoiceDate(e.Parameters("EquipmentNo").ToString(), str_GITransaction_No, intDepotID) ' Get Last invoice Date
                        LastActivity_Date = objEquipmentUpdate.get_LastActivity_Date(e.Parameters("EquipmentNo").ToString(), intDepotID, str_GITransaction_No)
                        e.OutputParameters.Add("BillingBit", strGenBit)
                    End With
                    e.DataSource = dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE)
                Else
                    Dim blnPreAdvice As Boolean
                    blnPreAdvice = objEquipmentUpdate.pub_GetPreAdviceByEqpmntNo(e.Parameters("EquipmentNo").ToString(), intDepotID)
                    If blnPreAdvice Then
                        e.OutputParameters.Add("norecordsfound", "true")
                    Else
                        e.OutputParameters.Add("preadvice", "true")
                    End If
                End If
            ElseIf e.Parameters("Mode") = "Reset" Then
                Dim strSessionId As String = GetSessionID()
                objcommonData.FlushLockData(EquipmentUpdateData.EQPMNT_NO, e.Parameters("EquipmentNo").ToString(), strSessionId, strActivityName)
                dsEquipmentUpdate.Tables.Clear()
            End If
            str_006KeyValue = objCommonConfig.pub_GetConfigSingleValue("007", intDepotID)
            bln_006Active_Key = objCommonConfig.IsKeyExists
            If bln_006Active_Key Then
                ifgEquipmentUpdate.Columns.Item(1).HeaderText = str_006KeyValue
                ifgEquipmentUpdate.Columns.Item(1).HeaderTitle = str_006KeyValue
            End If
            CacheData(EQUIPMENT_UPDATE, dsEquipmentUpdate)
            CacheData(EQUIPMENT_ADDITIONAL, dtAdditionalData)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgEquipmentDetail_RowUpdating"
    Protected Sub ifgEquipmentDetail_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgEquipmentUpdate.RowUpdating
        Try
            e.OutputParamters.Add("Status", e.OldValues(EquipmentUpdateData.EQPMNT_STTS_CD))
            e.OutputParamters.Add("ActivityDate", CDate(e.OldValues(EquipmentUpdateData.ACTVTY_DT)).ToString("dd-MMM-yyyy").ToUpper)
            e.OutputParamters.Add("Customer", e.OldValues(EquipmentUpdateData.CSTMR_CD))
            e.OutputParamters.Add("Type", e.OldValues(EquipmentUpdateData.EQPMNT_TYP_CD))
            e.OutputParamters.Add("Code", e.OldValues(EquipmentUpdateData.EQPMNT_CD_CD))
            e.OutputParamters.Add("TypeId", e.OldValues(EquipmentUpdateData.EQPMNT_TYP_ID))
            e.OutputParamters.Add("CodeId", e.OldValues(EquipmentUpdateData.EQPMNT_CD_ID))
            e.OutputParamters.Add("CustomerId", e.OldValues(EquipmentUpdateData.CSTMR_ID))
            e.OutputParamters.Add("BillingBit", e.OldValues(EquipmentUpdateData.INVC_GNRT_BT))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateEquipmentUpdate"
    Private Sub pvt_UpdateEquipmentUpdate(ByVal bv_strNewEquipmentNo As String, _
                                          ByVal bv_strNewCustomerId As String, _
                                          ByVal bv_strNewCustomerCd As String, _
                                          ByVal bv_strNewTypeId As String, _
                                          ByVal bv_strNewTypeCd As String, _
                                          ByVal bv_strNewCodeId As String, _
                                          ByVal bv_strNewCodeCd As String, _
                                          ByVal bv_strOldEquipmentNo As String, _
                                          ByVal bv_strOldEquipmentTypeId As String, _
                                          ByVal bv_strOldEquipmentTypeCd As String, _
                                          ByVal bv_strOldEquipmentCodeId As String, _
                                          ByVal bv_strOldEquipmentCodeCd As String, _
                                          ByVal bv_strOldCustomerId As String, _
                                          ByVal bv_strOldCustomerCd As String, _
                                          ByVal bv_strBillingBit As String, _
                                          ByVal bv_strEffective_dt As String, _
                                          ByVal bv_strReason As String)
        Try

            If bv_strEffective_dt <> Nothing AndAlso bv_strNewCustomerId <> Nothing Then

                Dim objEquipmentUpdate As New EquipmentUpdate
                Dim objCommon As New CommonData
                Dim intDepotId As Int32
                Dim strUserName As String
                Dim strModifiedDate As String
                Dim blnStatus As Boolean

                ''This bit is used for C1->C2->C1
                Dim strCustomerUpdate As String = ""
                Dim blnCustomerChange As Boolean = False
                ''
                intDepotId = objCommon.GetDepotID()

                ''c1->c2->c1 
                Dim GITransaction_No As String = objEquipmentUpdate.Get_GateIn_GITransaction_No(bv_strOldEquipmentNo, intDepotId)
                blnCustomerChange = objEquipmentUpdate.pub_GetPreviousStorageChargeByGITransactionNo(GITransaction_No, intDepotId, bv_strOldCustomerId, bv_strNewCustomerId, strCustomerUpdate)
                If strCustomerUpdate = "Show Confirmation Message" Then
                    blnCustomerChange = True
                Else
                    blnCustomerChange = False
                End If
                ''
                strUserName = objCommon.GetCurrentUserName()
                strModifiedDate = objCommon.GetCurrentDate()
                dsEquipmentUpdate = CType(RetrieveData(EQUIPMENT_UPDATE), EquipmentUpdateDataSet)
                dtAdditionalData = CType(RetrieveData(EQUIPMENT_ADDITIONAL), DataTable)
                dtAdditionalData.TableName = "OLD_V_EQUIPMENT_UPDATE"
                dsEquipmentUpdate.Merge(dtAdditionalData)

                blnStatus = objEquipmentUpdate.Update_Equipment_Information(bv_strNewEquipmentNo, bv_strNewCustomerId, bv_strNewCustomerCd, bv_strNewTypeId, bv_strNewTypeCd, _
                                                                bv_strNewCodeId, bv_strNewCodeCd, bv_strOldEquipmentNo, bv_strOldEquipmentTypeId, bv_strOldEquipmentTypeCd, _
                                                                bv_strOldEquipmentCodeId, bv_strOldEquipmentCodeCd, bv_strOldCustomerId, bv_strOldCustomerCd, bv_strBillingBit, _
                                                                bv_strEffective_dt, strUserName, strModifiedDate, bv_strReason, intDepotId, blnCustomerChange, dsEquipmentUpdate)

                If blnStatus = True Then
                    pub_SetCallbackStatus(True)
                    pub_SetCallbackReturnValue("Message", strMSGUPDATE)
                Else
                    pub_SetCallbackStatus(True)
                    pub_SetCallbackReturnValue("Message", strNoChanges)
                End If

            Else




                Dim objCommon As New CommonData
                Dim objEquipmentUpdate As New EquipmentUpdate
                Dim intDepotId As Integer
                Dim strUserName As String = ""
                intDepotId = objCommon.GetDepotID()
                strUserName = objCommon.GetCurrentUserName()
                Dim strModifiedDate As String = objCommon.GetCurrentDate()
                dsEquipmentUpdate = CType(RetrieveData(EQUIPMENT_UPDATE), EquipmentUpdateDataSet)
                dtAdditionalData = CType(RetrieveData(EQUIPMENT_ADDITIONAL), DataTable)
                dtAdditionalData.TableName = "OLD_V_EQUIPMENT_UPDATE"
                dsEquipmentUpdate.Merge(dtAdditionalData)
                objEquipmentUpdate.pub_UpdateEquipmentUpdate(bv_strNewEquipmentNo, _
                                                             bv_strNewCustomerId, _
                                                             bv_strNewCustomerCd, _
                                                             bv_strNewTypeId, _
                                                             bv_strNewTypeCd, _
                                                             bv_strNewCodeId, _
                                                             bv_strNewCodeCd, _
                                                             bv_strOldEquipmentNo, _
                                                             bv_strOldEquipmentTypeId, _
                                                             bv_strOldEquipmentTypeCd, _
                                                             bv_strOldEquipmentCodeId, _
                                                             bv_strOldEquipmentCodeCd, _
                                                             bv_strOldCustomerId, _
                                                             bv_strOldCustomerCd, _
                                                             dsEquipmentUpdate, _
                                                             CBool(bv_strBillingBit), _
                                                             bv_strReason, _
                                                             intDepotId, _
                                                             strUserName, _
                                                             strModifiedDate)
                Dim dtEquipmentUpdate As New DataTable
                dtEquipmentUpdate = dsEquipmentUpdate.Tables(EquipmentUpdateData._V_EQUIPMENT_UPDATE).GetChanges()
                CacheData(EQUIPMENT_UPDATE, dsEquipmentUpdate)
                If dtEquipmentUpdate Is Nothing And bv_strNewEquipmentNo = "" And bv_strNewCustomerCd = "" And bv_strNewTypeCd = "" And bv_strNewCodeCd = "" Then
                    pub_SetCallbackReturnValue("Message", strNoChanges)
                Else
                    pub_SetCallbackReturnValue("Message", strMSGUPDATE)
                End If
                pub_SetCallbackStatus(True)



            End If


        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                        MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetLockData"
    Private Function pvt_GetLockData(ByVal bv_strEquipmentNo As String, _
                                     ByRef br_strIpError As String, _
                                      ByRef br_strActivityName As String) As String
        Try
            Dim objCommonData As New CommonData
            Dim strErrorMessage As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim strIpAddress As String = GetClientIPAddress()
            Dim strCurrentIpAddress As String = String.Empty
            Dim strUserName As String = String.Empty
            If Not pub_GetQueryString("activityname") Is Nothing Then
                br_strActivityName = pub_GetQueryString("activityname")
            End If
            blnLockData = objCommonData.pub_GetLockData(False, bv_strEquipmentNo, strUserName, br_strActivityName, strIpAddress, True, RepairEstimateData.EQPMNT_NO)

            If blnLockData Then
                strCurrentIpAddress = GetClientIPAddress()
                If strCurrentIpAddress = strIpAddress Then
                    br_strIpError = "true"
                Else
                    br_strIpError = "false"
                End If
            End If
            Return strUserName
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
            Return String.Empty
        End Try
    End Function
#End Region

#Region "pvt_GetLockData()"
    Private Sub pvt_GetLockData(ByVal bv_strEquipmentNo As String, _
                                ByVal bv_strActivityName As String)
        Try
            Dim objCommonData As New CommonData
            Dim strErrorMessage As String = String.Empty
            Dim blnLockData As Boolean = False
            Dim strIpAddress As String = GetClientIPAddress()
            Dim strCurrentIpAddress As String = String.Empty
            Dim strUserName As String = String.Empty
            blnLockData = objCommonData.pub_GetLockData(False, bv_strEquipmentNo, strUserName, bv_strActivityName, strIpAddress, True, EquipmentUpdateData.EQPMNT_NO)
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

End Class
