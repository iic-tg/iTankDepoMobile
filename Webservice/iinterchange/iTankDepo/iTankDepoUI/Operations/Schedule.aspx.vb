
Partial Class Operations_Schedule
    Inherits Pagebase

    Dim dsSchedule As New ScheduleDataSet
    Dim dtSchedule As DataTable
    Private Const SCHEDULE As String = "SCHEDULE"
    Private Const ACTIVITY As String = "ACTIVITY"
    Private Const CURRENT_STATUS As String = "CURRENT_STATUS"
    Private Const BIND_COLUMN_COUNT As String = "BIND_COLUMN_COUNT"
    Private strMSGUPDATE As String = "Schedule : Schedule Date Updated Successfully."
    Dim objCommon As New CommonData

#Region "Page_PreRender1()"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/Schedule.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Documents.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                              MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_Load1()"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            datScheduleDate.Validator.ValueToCompare = DateTime.Now.ToString("dd-MMM-yyyy")
            If Page.IsCallback AndAlso Not RetrieveData(ACTIVITY) Is Nothing AndAlso Not RetrieveData(BIND_COLUMN_COUNT) Is Nothing Then
                dsSchedule = CType(RetrieveData(SCHEDULE), ScheduleDataSet)
                pvt_BindDataGrid(dsSchedule.Tables(CStr(RetrieveData(ACTIVITY))), CInt(RetrieveData(BIND_COLUMN_COUNT)))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback()"
    Protected Sub Page_OnCallback(sender As Object, e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "fnGetData"
                    pvt_GetData()
                Case "updateScheduleDate"
                    pvt_UpdateScheduleDate(e.GetCallbackValue("ActivityId"))
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_GetData()"
    Private Sub pvt_GetData()
        Try
            Dim objCommon As New CommonData()
            Dim sbSchedule As New StringBuilder
            Dim objCommonConfig As New ConfigSetting()
            Dim str_040KeyValue As String = String.Empty
            str_040KeyValue = objCommonConfig.pub_GetConfigSingleValue("040", CInt(objCommon.GetDepotID()))
            sbSchedule.Append(CommonWeb.GetTextDateValuesJSO(datScheduleDate, Now.ToString("dd-MMM-yyyy").ToUpper))
            sbSchedule.Append(CommonWeb.GetHiddenTextValuesJSO(hdnCurrentDate, Now.ToString("dd-MMM-yyyy").ToUpper))
            If Not str_040KeyValue Is Nothing Then
                sbSchedule.Append(CommonWeb.GetLookupValuesJSO(lkpSchedule, objCommon.GetEnumID("SCHEDULE", str_040KeyValue), UCase(str_040KeyValue)))
            End If
            pub_SetCallbackReturnValue("Message", sbSchedule.ToString())
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_UpdateScheduleDate()"
    Private Sub pvt_UpdateScheduleDate(ByVal bv_strActivityId As String)
        Try
            If Not RetrieveData(ACTIVITY) Is Nothing Then
                dsSchedule = CType(RetrieveData(SCHEDULE), ScheduleDataSet)

                Dim objSchedule As New Schedule
                Dim intActivityID As Integer = 0
                Dim strDataKey As String = String.Empty
                Dim strUpdateTableName As String = String.Empty
                If Not bv_strActivityId Is Nothing Then
                    intActivityID = CInt(bv_strActivityId)
                End If
                If Not bv_strActivityId Is Nothing Then
                    If CInt(bv_strActivityId) = 123 Then 'Cleaning
                        strDataKey = ScheduleData.ACTVTY_STTS_ID
                        strUpdateTableName = ScheduleData._ACTIVITY_STATUS
                    ElseIf CInt(bv_strActivityId) = 124 Then 'Repair Estimate
                        strDataKey = ScheduleData.RPR_ESTMT_ID
                        strUpdateTableName = ScheduleData._REPAIR_ESTIMATE
                    ElseIf CInt(bv_strActivityId) = 125 Then 'Survey Completion
                        strDataKey = ScheduleData.ACTVTY_STTS_ID
                        strUpdateTableName = ScheduleData._ACTIVITY_STATUS
                    ElseIf CInt(bv_strActivityId) = 137 Then 'Inspection
                        strDataKey = ScheduleData.ACTVTY_STTS_ID
                        strUpdateTableName = ScheduleData._ACTIVITY_STATUS
                    End If
                End If
                Dim drSchedule As DataRow()
                drSchedule = dsSchedule.Tables(CStr(RetrieveData(ACTIVITY))).Select(ScheduleData.CHECKED & "='True'")
                If Not drSchedule.Length > 0 Then
                    pub_SetCallbackStatus(False)
                    pub_SetCallbackError("Please select at least one Equipment to update the schedule date.")
                    Exit Sub
                End If
                objSchedule.pub_UpdateActivityById(CStr(RetrieveData(ACTIVITY)), strDataKey, intActivityID, strUpdateTableName, dsSchedule)
            End If
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgSchedule_ClientBind"
    Protected Sub ifgSchedule_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgSchedule.ClientBind
        Try
            If Not e.Parameters("Mode") Is Nothing Then
                If CStr(e.Parameters("Mode")) = "fetch" Then
                    Dim objSchedule As New Schedule
                    Dim intColumns As Integer = 0
                    Dim intScheduleId As Integer = 0
                    Dim strTableName As String = String.Empty

                    If Not e.Parameters("ScheduleId") Is Nothing Then
                        intScheduleId = CInt(e.Parameters("ScheduleId"))
                    End If
                    If CInt(e.Parameters("ActivityId")) = 123 Then 'Cleaning
                        strTableName = ScheduleData._V_ACTIVITY_STATUS
                        intColumns = 9
                    ElseIf CInt(e.Parameters("ActivityId")) = 124 Then 'Repair Estimate
                        strTableName = ScheduleData._V_REPAIR_ESTIMATE
                        intColumns = 11
                    ElseIf CInt(e.Parameters("ActivityId")) = 125 Then 'Survey Completion
                        strTableName = ScheduleData._V_REPAIR_ESTIMATE
                        intColumns = 7

                    ElseIf CInt(e.Parameters("ActivityId")) = 137 Then 'Inspection
                        strTableName = ScheduleData._V_ACTIVITY_STATUS
                        intColumns = 9
                    End If
                    dsSchedule = objSchedule.Pub_GetActivityByActivityId(CInt(e.Parameters("ActivityId")), strTableName, intScheduleId, objCommon.GetDepotID())
                    pvt_BindDataGrid(dsSchedule.Tables(strTableName), intColumns)
                    If dsSchedule.Tables(strTableName).Rows.Count = 0 Then
                        e.OutputParameters.Add("norecordsfound", True)
                    Else
                        e.OutputParameters.Add("norecordsfound", False)
                    End If

                    '--------------------------------------------------------------
                    Dim ds_Schedule As ScheduleDataSet
                    '    Dim objCommon As New CommonData
                    Dim strCleanActivity As String = String.Empty
                    Dim strInspectionActivity As String = String.Empty
                    Dim dt As DataTable

                    ds_Schedule = objSchedule.GetScheduleActivity(objCommon.GetCurrentUserName())
                    dt = ds_Schedule.Tables(ScheduleData._ENUM)

                    'Cleaning Rights
                    'pvt_CheckScheduleRights(ds_Schedule.Tables(ScheduleData._V_SCHEDULE_USER_RIGHTS), e.Parameters("ActivityName"))

                    If e.Parameters("ActivityName").ToString().ToLower() = "inspection" Or e.Parameters("ActivityName").ToString().ToLower() = "cleaning" Then
                        pvt_CheckScheduleRights(ds_Schedule.Tables(ScheduleData._V_SCHEDULE_USER_RIGHTS), e.Parameters("ActivityName"))
                    Else
                        CacheData(CURRENT_STATUS, "")
                    End If

                    'CacheData(CURRENT_STATUS, "")
                    '-------------------------------------------------------------------------------

                    'If Not RetrieveData(CURRENT_STATUS) Is Nothing Then

                    '    Dim strRightValidation As String = RetrieveData(CURRENT_STATUS)

                    '    If strRightValidation.ToLower() = "view" Then
                    '        e.OutputParameters.Add("GridValidation", True)
                    '    End If

                    'End If

                    CacheData(ACTIVITY, strTableName)
                    CacheData(BIND_COLUMN_COUNT, intColumns)
                    CacheData(SCHEDULE, dsSchedule)
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. 
                                                  MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgSchedule_RowDataBound"
    Protected Sub ifgSchedule_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgSchedule.RowDataBound
        Try

            'CURRENT_STATUS

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim chk As iFgCheckBox
                chk = CType(e.Row.Cells(0).Controls(0), iFgCheckBox)
                chk.Attributes.Add("onclick", String.Concat("updateDate(this);"))
                Dim datControl As iDate
                datControl = CType(DirectCast(DirectCast(e.Row.Cells(3),  _
                            iFgFieldCell).ContainingField,  _
                            DateField).iDate, iDate)
                datControl.Validator.CustomValidation = True
                datControl.Validator.CustomValidationFunction = "CheckDate"
                datControl.Validator.ErrorText = "*"
                If Not RetrieveData(CURRENT_STATUS) Is Nothing Then
                    Dim strRightValidation As String = RetrieveData(CURRENT_STATUS)

                    If strRightValidation.ToUpper() = "READONLY" Then
                        For Each cell As TableCell In e.Row.Cells
                            cell.Enabled = False
                        Next
                    End If
                End If

                Dim i As Int32 = 0
                e.Row.Cells(0).Style.Add("width", "30px")
                For i = 1 To e.Row.Cells.Count - 1
                    e.Row.Cells(i).Style.Add("width", "100px")
                Next

            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgSchedule_RowUpdating"
    Protected Sub ifgSchedule_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgSchedule.RowUpdating
        Try
            e.NewValues(ScheduleData.SCHDL_DT) = e.NewValues("Schedule Date")
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "pvt_BindDataGrid"
    ''' <summary>
    ''' This function is used to bind the pending data and schema from pending query
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="NoOfCol"></param>
    ''' <remarks></remarks>
    Private Sub pvt_BindDataGrid(ByRef dt As DataTable, ByVal NoOfCol As Integer)
        Try
            Dim I As Integer = 0
            If NoOfCol = 0 Then
                Throw New Exception("Query returns results, but Number of visible columns is zero")
            End If
            ifgSchedule.Visible = True
            pvt_BindSchema(dt, NoOfCol)
            With ifgSchedule
                .DataSource = dt
                .DataBind()
            End With
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_BindSchema"
    ''' <summary>
    ''' This function is used to generate schema based on the type of data column
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="NoOfCol"></param>
    ''' <remarks></remarks>
    Private Sub pvt_BindSchema(ByRef dt As DataTable, ByVal NoOfCol As Integer)
        Try
            Dim I As Integer
            I = 0
            ifgSchedule.AutoGenerateColumns = False
            ifgSchedule.Columns.Clear()
            For Each dc As DataColumn In dt.Columns
                If dc.ColumnName = "CHECKED" Then
                    Dim cbfChecked As New iInterchange.WebControls.v4.Data.CheckBoxField
                    cbfChecked.DataField = "CHECKED"
                    cbfChecked.SortExpression = ""
                    cbfChecked.HeaderText = "Select"
                    cbfChecked.HeaderTitle = "Select"
                    cbfChecked.ReadOnly = False
                    ifgSchedule.Columns.Add(cbfChecked)
                ElseIf dc.DataType Is GetType(System.DateTime) Then
                    Dim dfield As New DateField
                    dfield.DataField = dc.ColumnName
                    dfield.SortExpression = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.DataFormatString = "{0:dd-MMM-yyyy}"
                    dfield.HtmlEncode = False
                    dfield.iDate.DateIcon.CssClass = "dimg"
                    dfield.iDate.DateIcon.Src = iUtil.AppPath & _
                                    iUtil.ImagesFolder & "calendar.png"
                    dfield.iDate.CssClass = "lkp"
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = False
                    dfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left
                    dfield.iDate.MaxLength = 23
                    dfield.iDate.Validator.CompareValidation = True
                    dfield.iDate.Validator.Operator = ValidationCompareOperator.GreaterThanEqual
                    dfield.iDate.Validator.Validate = True
                    dfield.iDate.Validator.IsRequired = True
                    dfield.iDate.Validator.ReqErrorMessage = "Schedule Date Required."
                    dfield.iDate.Validator.ValidationGroup = "divSchedule"
                    dfield.iDate.Validator.ErrorText = "*"
                    ifgSchedule.Columns.Add(dfield)
                ElseIf dc.DataType Is GetType(System.Int32) Or dc.DataType Is GetType(System.Int64) Or _
                    dc.DataType Is GetType(System.Decimal) Then
                    Dim dfield As New TextboxField
                    dfield.DataField = dc.ColumnName
                    dfield.SortExpression = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.TextBox.CssClass = "lkp"
                    dfield.TextBox.iCase = TextCase.Numeric
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = False
                    dfield.TextBox.MaxLength = 20
                    ifgSchedule.Columns.Add(dfield)

                Else
                    Dim dfield As New TextboxField
                    dfield.DataField = dc.ColumnName
                    dfield.SortExpression = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.TextBox.CssClass = "lkp"
                    ifgSchedule.Columns.Add(dfield)
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = True
                End If


                ifgSchedule.Columns.Item(I).AllowSearch = True
                If I >= NoOfCol Then
                    ifgSchedule.Columns.Item(I).Visible = False
                End If
                If dc.ColumnName.Trim() = "Schedule Date" Then
                    ifgSchedule.Columns.Item(I).IsEditable = True

                ElseIf dc.ColumnName.Trim() = "CHECKED" Then
                    ifgSchedule.Columns.Item(I).IsEditable = True
                Else
                    ifgSchedule.Columns.Item(I).IsEditable = False
                End If
                I = I + 1
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                   MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Schedule Role Rights"


    Protected Sub lkpSchedulingType_Search(sender As Object, e As iInterchange.WebControls.v4.Input.SearchEventArgs) Handles lkpSchedulingType.Search
        Try

            Dim ds_Schedule As ScheduleDataSet
            Dim objSchedule As New Schedule
            Dim objCommon As New CommonData
            Dim strCleanActivity As String = String.Empty
            Dim strInspectionActivity As String = String.Empty
            Dim dt As DataTable

            ds_Schedule = objSchedule.GetScheduleActivity(objCommon.GetCurrentUserName())
            dt = ds_Schedule.Tables(ScheduleData._ENUM)

            'Cleaning Rights
            strCleanActivity = pvt_CheckScheduleRights(ds_Schedule.Tables(ScheduleData._V_SCHEDULE_USER_RIGHTS), "Cleaning")

            If strCleanActivity = "RemoveFilter" Then
                dt = ds_Schedule.Tables(ScheduleData._ENUM).Select("ENM_CD not in ('Cleaning')").CopyToDataTable()
            End If

            'Inspection Rights
            strInspectionActivity = pvt_CheckScheduleRights(ds_Schedule.Tables(ScheduleData._V_SCHEDULE_USER_RIGHTS), "Inspection")

            If strInspectionActivity = "RemoveFilter" Then
                dt = ds_Schedule.Tables(ScheduleData._ENUM).Select("ENM_CD not in ('Inspection')").CopyToDataTable()
            End If
            dt.AcceptChanges()
            'lkpSchedulingType.DataSource = ds_Schedule.Tables(ScheduleData._ENUM)

            lkpSchedulingType.DataSource = dt
            lkpSchedulingType.DataBind()


        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

    Private Function pvt_CheckScheduleRights(ByVal dt As DataTable, ByVal strActivityName As String) As String

        Try

            Dim blnAddRight As Boolean = False
            Dim blnEditRight As Boolean = False
            Dim blnViewRight As Boolean = False

            Dim dr() As DataRow = dt.Select(String.Concat("ACTVTY_NAM='", strActivityName, "'"))

            If dr.Length > 0 Then

                blnAddRight = dr(0).Item("CRT_BT")
                blnEditRight = dr(0).Item("EDT_BT")
                blnViewRight = dr(0).Item("VW_BT")

                If blnAddRight = False AndAlso blnEditRight = False AndAlso blnViewRight = False Then
                    CacheData(strActivityName.ToUpper(), "View")
                    CacheData(CURRENT_STATUS, "READONLY")
                    Return "RemoveFilter"
                ElseIf blnAddRight = True Or blnEditRight = True Then
                    CacheData(CURRENT_STATUS, "")
                    Return "Validate"
                Else
                    CacheData(strActivityName.ToUpper(), "View")
                    CacheData(CURRENT_STATUS, "READONLY")
                    Return "View"

                End If

            Else
                CacheData(strActivityName.ToUpper(), "View")
                CacheData(CURRENT_STATUS, "READONLY")
                Return "RemoveFilter"
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Function


#End Region


End Class
