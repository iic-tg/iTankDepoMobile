
Partial Class Operations_Heating
    Inherits Pagebase

#Region "Declarations"
    Private strMSGUPDATE As String = "Heating : Equipment(s) Updated Successfully"
    Private strMSGDUPLICATE As String = "This combination already exists."
    Private Const HEATING As String = "HEATING"

#End Region

#Region "Parameters"
    Public dsHeating As HeatingDataSet
    Dim strDepotCurrency As String = String.Empty
    Dim intDepotId As Integer
    Private pvt_strTableKey As String
    Private pvt_strTableName As String
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "UpdateHeating"
                    UpdateHeating(e.GetCallbackValue("WFDATA"), _
                                  e.GetCallbackValue("ActivityName"), _
                                 CInt(e.GetCallbackValue("ActivityId")))
                Case "CalculateHeatingPeriod"
                    CalculateHeatingPeriod(e.GetCallbackValue("HeatingStartDate"), _
                                           e.GetCallbackValue("HeatingStartTime"), _
                                           e.GetCallbackValue("HeatingEndDate"), _
                                           e.GetCallbackValue("HeatingEndTime"))
                Case "ValidatePreviousActivityDate"
                    pvt_ValidatePreviousActivityDate(e.GetCallbackValue("EquipmentNo"), _
                                                     e.GetCallbackValue("EventDate"), _
                                                     e.GetCallbackValue("EndDate"))
                Case "ValidateHeatingEndDate"
                    pvt_ValidateHeatingEndDate(e.GetCallbackValue("EquipmentNo"), _
                                                     e.GetCallbackValue("EventDate"), _
                                                     e.GetCallbackValue("HeatingStartDate"))
                Case "ValidateHeatingEndTime"
                    pvt_ValidateHeatingEndTime(e.GetCallbackValue("StartTime"), _
                                                    e.GetCallbackValue("EndTime"))
                Case "ValidateHeatingStartTime"
                    pvt_ValidateHeatingStartTime(e.GetCallbackValue("StartTime"), _
                                                    e.GetCallbackValue("EndTime"))
                Case "CalculateTotalRate"
                    pvt_CalculateTotalRate(e.GetCallbackValue("MinRate"), _
                                                    e.GetCallbackValue("TotalPeriod"), _
                                                    e.GetCallbackValue("MinHeatingPeriod"), _
                                                    e.GetCallbackValue("HourlyRate"))
                Case "RecordLockData"
                    pvt_GetLockData(CBool(e.GetCallbackValue("checkBit")), _
                                    e.GetCallbackValue("EquipmentNo"), _
                                    e.GetCallbackValue("ActivityName"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

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
                                        MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateHeatingStartTime"
    Public Sub pvt_ValidateHeatingStartTime(ByVal bv_strStartTime As String, ByVal bv_strEndTime As String)
        Dim startTime As DateTime = bv_strStartTime
        Dim endTime As DateTime = bv_strEndTime
        Dim hour As Double = (endTime.Subtract(startTime)).TotalHours
        If hour <= 0.0 Then
            pub_SetCallbackReturnValue("Error", String.Concat("Heating Start Time Should be lesser than Heating End Time "))
            pub_SetCallbackStatus(True)
        End If
    End Sub

#End Region

#Region "pvt_CalculateTotalRate"
    Public Sub pvt_CalculateTotalRate(ByVal str_MinRate As String, _
                                      ByVal str_TotaPeriod As String, _
                                      ByVal str_MinHeatingPerod As String, _
                                      ByVal str_HourlyRate As String)
        Try
            Dim MinRate As Decimal = str_MinRate
            Dim TotaPeriod As Decimal = str_TotaPeriod
            Dim MinHeatingPerod As Decimal = str_MinHeatingPerod
            Dim HourlyRate As Decimal = str_HourlyRate
            Dim TotalRate As Decimal
            If CDbl(str_MinHeatingPerod) <= CDbl(str_TotaPeriod) Then
                TotalRate = str_MinRate + (str_TotaPeriod - str_MinHeatingPerod) * str_HourlyRate
            ElseIf CDbl(str_MinHeatingPerod) > CDbl(str_TotaPeriod) Then
                TotalRate = str_MinRate
            End If

            TotalRate = FormatNumber(CDbl(TotalRate), 2)
            pub_SetCallbackReturnValue("TotalRate", TotalRate)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateHeatingEndTime"
    Public Sub pvt_ValidateHeatingEndTime(ByVal bv_strStartTime As String, ByVal bv_strEndTime As String)
        Dim startTime As DateTime = bv_strStartTime
        Dim endTime As DateTime = bv_strEndTime
        Dim hour As Double = (endTime.Subtract(startTime)).TotalHours
        If hour <= 0.0 Then
            pub_SetCallbackReturnValue("Error", String.Concat("Heating End Time Should be greater than Heating Start Time "))
            pub_SetCallbackStatus(True)
        End If
    End Sub

#End Region

#Region "pvt_ValidatePreviousActivityDate"
    Private Sub pvt_ValidatePreviousActivityDate(ByVal bv_strEquipmentNo As String, ByVal bv_strEventDate As String, ByVal bv_strEndDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim blnCompareValid As Boolean = False
            Dim blnCompareEnddate As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objGateIn As New Gatein
            Dim HeatingEndDate As DateTime
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData
            Dim dsCommonUI As New CommonUIDataSet
            blnDateValid = objCommonUI.pub_GetEquipmentPreviousActivityDate(bv_strEquipmentNo, _
                                                                          CDate(bv_strEventDate), _
                                                                          dtPreviousDate, _
                                                                          "Heating", _
                                                                          CInt(objCommon.GetDepotID()))
            If (CDate(bv_strEventDate) > DateTime.Now) Then
                blnCompareValid = True
            End If
            If (Not (bv_strEndDate) = Nothing) Then
                If (CDate(bv_strEndDate) < CDate(bv_strEventDate)) Then
                    HeatingEndDate = CDate(bv_strEndDate)
                    blnCompareEnddate = True
                End If
            End If

            If blnCompareValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Heating Start Date must not be greater than Current Date"))
            End If
            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Heating Start Date Should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            If blnCompareEnddate = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Heating Start Date Should be lesser than or equal to Heating End Date (", HeatingEndDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateHeatingEndDate"
    Private Sub pvt_ValidateHeatingEndDate(ByVal bv_strEquipmentNo As String, ByVal bv_strEventDate As String, ByVal bv_strHeatingStartDate As String)
        Try
            Dim blnDateValid As Boolean = False
            Dim blnPreviousDateValid As Boolean = False
            Dim objCommonUI As New CommonUI
            Dim dtPreviousDate As DateTime = Nothing
            Dim blnCompareValid As Boolean = False
            Dim objCommon As New CommonData

            blnPreviousDateValid = objCommonUI.pub_GetEquipmentPreviousActivityDate(bv_strEquipmentNo, _
                                                                         CDate(bv_strEventDate), _
                                                                         dtPreviousDate, _
                                                                         "Heating", _
                                                                         CInt(objCommon.GetDepotID()))

            If (CDate(bv_strEventDate) > DateTime.Now) Then
                blnCompareValid = True
            End If

            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Heating End Date Should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), ")"))
            End If

            If blnCompareValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Heating End Date must not be greater than Current Date"))

            End If
            If (bv_strHeatingStartDate) = Nothing Then
                pub_SetCallbackStatus(True)
                Exit Sub
            End If
            If (Not (CDate(bv_strEventDate) >= CDate(bv_strHeatingStartDate))) Then
                blnDateValid = True
            End If
            Dim HeatingDate As DateTime = CDate(bv_strHeatingStartDate)

            If blnDateValid = True Then
                pub_SetCallbackReturnValue("Error", String.Concat("Heating End Date Should be greater than or equal to Heating Start Date(", HeatingDate.ToString("dd-MMM-yyyy"), ")"))
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("Error", "")
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "CalculateHeatingPeriod"
    Public Sub CalculateHeatingPeriod(ByVal str_HeatingStartDate As String, _
                                      ByVal str_HeatingStartTime As String, _
                                      ByVal str_HeatingEndDate As String, _
                                      ByVal str_HeatingEndTime As String)
        Try
            Dim startDate As Date = str_HeatingStartDate
            Dim startTime As DateTime = str_HeatingStartTime
            Dim endDate As Date = str_HeatingEndDate
            Dim endTime As DateTime = str_HeatingEndTime 'add 3 days to startDate
            Dim timeSpan As TimeSpan = endDate.Subtract(startDate)
            Dim hour As Double = (endTime.Subtract(startTime)).TotalHours
            Dim difHr As Double = (timeSpan.TotalHours) 'get 0 hours although 3 days difference
            Dim TotalHours As String = difHr + hour
            pub_SetCallbackReturnValue("TotalHeatingPeriod", TotalHours)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "UpdateHeating"

    Public Sub UpdateHeating(ByVal bv_strWFdata As String, _
                             ByVal bv_strActivityName As String, _
                             ByVal bv_intActivityId As Integer)
        Try
            Dim objHeating As New Heating
            Dim objCommondata As New CommonData
            Dim dtHeating As DataTable
            dsHeating = CType(RetrieveData(HEATING), HeatingDataSet)
            dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Clear()
            dtHeating = CType(ifgHeating.DataSource, DataTable)
            dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Merge(dtHeating)
            Dim drHeating As DataRow()
            Dim strActivitySubmit As String = String.Empty
            drHeating = dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Select(HeatingData.CHECKED & "='True'")
            If Not drHeating.Length > 0 Then
                pub_SetCallbackStatus(False)
                pub_SetCallbackError("Please Select Atleast One Equipment.")
                Exit Sub
            End If

            '''' 22432
            Dim strHeatingCodes As String = ""
            Dim strErrorMessage As String = ""
            For Each drHeating1 As DataRow In dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Select(HeatingData.CHECKED & "='True'")
                If drHeating1.Item(HeatingData.HTNG_ID).ToString <> "" Then
                    If strHeatingCodes <> String.Empty Then
                        strHeatingCodes = String.Concat(strHeatingCodes, ",'", drHeating1.Item(HeatingData.HTNG_CD), "'")
                    Else
                        strHeatingCodes = String.Concat("'", drHeating1.Item(HeatingData.HTNG_CD), "'")
                    End If
                End If
            Next
            Dim objCommonUI As New CommonUI
            objCommonUI.pub_validateFinalizedInvoiceActivityWise(CommonUIData._HEATING_CHARGE, CInt(objCommondata.GetDepotID()), strHeatingCodes, "", "", strErrorMessage, dsHeating)
            If strErrorMessage <> "" Then

                pub_SetCallbackError("This action will not be allowed since one of the equipment(s) heating invoice is already finalized.")
                pub_SetCallbackStatus(False)
                Exit Sub
            End If
            ''
            objHeating.pub_UpdateHeating(dsHeating, _
                                         objCommondata.GetCurrentUserName(), _
                                         CDate(objCommondata.GetCurrentDate()), _
                                         CInt(objCommondata.GetDepotID()), _
                                         strActivitySubmit, _
                                         bv_intActivityId)
            Dim strCurrentSessionId As String = String.Empty
            strCurrentSessionId = GetSessionID()
            For Each drLock As DataRow In dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Select(String.Concat(HeatingData.CHECKED, "='True'"))
                objCommondata.FlushLockData(HeatingData.EQPMNT_NO, CStr(drLock.Item(HeatingData.EQPMNT_NO)), strCurrentSessionId, bv_strActivityName)
            Next
            'Dim intCheckedRows As Integer = 0
            'Dim strSplitActivitySubmit() As String = Nothing
            'Dim intSplitActivity As Integer = 0
            'intCheckedRows = dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Compute(String.Concat("COUNT(", HeatingData.HTNG_ID, ")"), String.Concat(HeatingData.CHECKED, "= 'True'"))
            'If strActivitySubmit <> Nothing Then
            '    strSplitActivitySubmit = strActivitySubmit.Split(",")
            '    intSplitActivity = strSplitActivitySubmit.Length
            'End If
            dtHeating.AcceptChanges()
            'If intCheckedRows = intSplitActivity Then
            '    pub_SetCallbackReturnValue("ActivitySubmit", strActivitySubmit)
            'Else
            '    pub_SetCallbackReturnValue("ActivitySubmit", "")
            'End If
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            '  pub_SetCallbackReturnValue("ActivitySubmit", strActivitySubmit)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/Heating.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgHeating_ClientBind"
    Protected Sub ifgHeating_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgHeating.ClientBind
        Try
            Dim objHeating As New Heating
            Dim objCommon As New CommonData
            Dim drHeating As DataRow
            Dim dtHeating As DataTable
            Dim strWfData As String = CStr(e.Parameters("WFDATA"))
            intDepotId = CommonWeb.iInt(objCommon.GetDepotID())
            ifgHeating.UseCachedDataSource = True
            dsHeating = objHeating.pub_GetHeatingFromGateIn(intDepotId)
            Dim dsHeatingCustomer As New HeatingDataSet
            Dim dsHeatingCharge As New HeatingDataSet
            dtHeating = dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Clone()
            dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Rows.Clear()
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                dsHeatingCustomer = objHeating.pub_GetCustomerDetail(objCommon.GetHeadQuarterID())
            Else
                dsHeatingCustomer = objHeating.pub_GetCustomerDetail(intDepotId)
            End If

            dsHeating.Merge(dsHeatingCustomer.Tables(HeatingData._CUSTOMER))
            dsHeatingCustomer.Tables(HeatingData._HEATING_CHARGE).Rows.Clear()
            dsHeatingCharge = objHeating.pub_VHeatingChargeByDepot(intDepotId)
            dsHeating.Merge(dsHeatingCustomer.Tables(HeatingData._HEATING_CHARGE))

            For Each dr As DataRow In dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Rows
                drHeating = dtHeating.NewRow()
                drHeating.Item(HeatingData.HTNG_ID) = CommonWeb.GetNextIndex(dtHeating, HeatingData.HTNG_ID)
                drHeating.Item(HeatingData.CSTMR_ID) = dr.Item(HeatingData.CSTMR_ID)
                drHeating.Item(HeatingData.CSTMR_CD) = dr.Item(HeatingData.CSTMR_CD)
                drHeating.Item(HeatingData.EQPMNT_NO) = dr.Item(HeatingData.EQPMNT_NO)
                drHeating.Item(HeatingData.EQPMNT_TYP_ID) = dr.Item(HeatingData.EQPMNT_TYP_ID)
                drHeating.Item(HeatingData.EQPMNT_TYP_CD) = dr.Item(HeatingData.EQPMNT_TYP_CD)
                drHeating.Item(HeatingData.EQPMNT_CD_CD) = dr.Item(HeatingData.EQPMNT_CD_CD)
                drHeating.Item(HeatingData.EQPMNT_TYP_ID) = dr.Item(HeatingData.EQPMNT_TYP_ID)
                drHeating.Item(HeatingData.EQPMNT_CD_ID) = dr.Item(HeatingData.EQPMNT_CD_ID)
                drHeating.Item(HeatingData.PRDCT_ID) = dr.Item(HeatingData.PRDCT_ID)
                drHeating.Item(HeatingData.PRDCT_DSCRPTN_VC) = dr.Item(HeatingData.PRDCT_DSCRPTN_VC)
                drHeating.Item(HeatingData.GTN_DT) = CDate(dr.Item(HeatingData.GTN_DT)).ToString("dd-MMM-yyyy")
                drHeating.Item(HeatingData.GI_TRNSCTN_NO) = dr.Item(HeatingData.GI_TRNSCTN_NO)
                drHeating.Item(HeatingData.YRD_LCTN) = dr.Item(HeatingData.YRD_LCTN)

                Dim drHeatingcharge As DataRow()
                drHeatingcharge = dsHeatingCharge.Tables(HeatingData._HEATING_CHARGE).Select(String.Concat(HeatingData.GI_TRNSCTN_NO, "='", dr.Item(HeatingData.GI_TRNSCTN_NO), "'"))
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_CD) = drHeatingcharge(0).Item(HeatingData.HTNG_CD)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_STRT_DT) = drHeatingcharge(0).Item(HeatingData.HTNG_STRT_DT)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_STRT_TM) = drHeatingcharge(0).Item(HeatingData.HTNG_STRT_TM)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_END_DT) = drHeatingcharge(0).Item(HeatingData.HTNG_END_DT)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_END_TM) = drHeatingcharge(0).Item(HeatingData.HTNG_END_TM)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_TMPRTR) = drHeatingcharge(0).Item(HeatingData.HTNG_TMPRTR)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.TTL_HTN_PRD) = drHeatingcharge(0).Item(HeatingData.TTL_HTN_PRD)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.MIN_HTNG_RT_NC) = drHeatingcharge(0).Item(HeatingData.MIN_HTNG_RT_NC)
                Else
                    For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                dr.Item(GateinData.CSTMR_CD), "'"))
                        drHeating.Item(HeatingData.MIN_HTNG_RT_NC) = drCstmrDetail.Item(HeatingData.MIN_HTNG_RT_NC)
                        Exit For
                    Next
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HRLY_CHRG_NC) = drHeatingcharge(0).Item(HeatingData.HRLY_CHRG_NC)
                Else
                    For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                dr.Item(GateinData.CSTMR_CD), "'"))
                        drHeating.Item(HeatingData.HRLY_CHRG_NC) = drCstmrDetail.Item(HeatingData.HRLY_CHRG_NC)
                        Exit For
                    Next
                End If
                For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                dr.Item(GateinData.CSTMR_CD), "'"))
                    strDepotCurrency = drCstmrDetail.Item(CommonUIData.CRRNCY_CD)
                    drHeating.Item(HeatingData.CSTMR_CRRNCY_CD) = drCstmrDetail.Item(HeatingData.CSTMR_CRRNCY_CD)
                    Exit For
                Next
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.TTL_RT_NC) = drHeatingcharge(0).Item(HeatingData.TTL_RT_NC)
                End If
                If Not (dr.Item(HeatingData.MIN_HTNG_PRD_NC) Is DBNull.Value) Then
                    drHeating.Item(HeatingData.MIN_HTNG_PRD_NC) = dr.Item(HeatingData.MIN_HTNG_PRD_NC)
                Else
                    For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                dr.Item(GateinData.CSTMR_CD), "'"))
                        drHeating.Item(HeatingData.MIN_HTNG_PRD_NC) = drCstmrDetail.Item(HeatingData.MIN_HTNG_PRD_NC)
                        Exit For
                    Next
                End If
                dtHeating.Rows.Add(drHeating)
            Next
            dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Clear()
            dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Merge(dtHeating)
            e.DataSource = dtHeating
            CacheData(HEATING, dsHeating)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgHeating_RowDataBound"
    Protected Sub ifgHeating_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgHeating.RowDataBound
        Try
            If Not e.Row.DataItem Is Nothing Then
                Dim StartdatControl As iDate
                Dim EnddatControl As iDate
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim chkActive As iFgCheckBox
                chkActive = CType(e.Row.Cells(14).Controls(0), iFgCheckBox)
                chkActive.Attributes.Add("onClick", String.Concat("lockData(this,'", CStr(drv.Row.Item(GateinData.EQPMNT_NO)), "','", String.Empty, "' );"))
                If drv.Row.RowState = DataRowState.Unchanged Then
                    StartdatControl = CType(DirectCast(DirectCast(e.Row.Cells(5),  _
                                iFgFieldCell).ContainingField,  _
                                DateField).iDate, iDate)
                    StartdatControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                    StartdatControl.Validator.RangeValidation = True
                    StartdatControl.Validator.RngMaximumValue = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                    EnddatControl = CType(DirectCast(DirectCast(e.Row.Cells(7),  _
                                iFgFieldCell).ContainingField,  _
                                DateField).iDate, iDate)
                    EnddatControl.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                    EnddatControl.Validator.RangeValidation = True
                    EnddatControl.Validator.RngMaximumValue = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper
                End If
            ElseIf e.Row.RowType = DataControlRowType.Header Then
                If strDepotCurrency = String.Empty Then
                    Dim dsHeatingCustomer As New HeatingDataSet
                    dsHeatingCustomer = CType(RetrieveData(HEATING), HeatingDataSet)
                    If dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Rows.Count > 0 Then
                        strDepotCurrency = CStr(dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Rows(0).Item(CommonUIData.CRRNCY_CD))
                    End If
                End If
                CType(e.Row.Cells(13), iInterchange.WebControls.v4.Data.iFgFieldCell).Text = String.Concat("Total Rate in ", strDepotCurrency)
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetLockData"
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
            blnLockData = objCommonData.pub_GetLockData(bv_blnCheckBit, bv_strEquipmentNo, strUserName, bv_strActivityName, strIpAddress, False, HeatingData.EQPMNT_NO)
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
