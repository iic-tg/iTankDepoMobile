Imports System.Data
Partial Class List
    Inherits Framebase

    Private pvt_strFuncValues As String
    Private pvt_Activity_Id As String
    Private pvt_strMode As String
    Private pvt_strFilterName As String
    Private pvt_strFilterValue As String
    Private pvt_strQryType As String
    Private Const PENDING As String = "Pending"
    Private Const MYSUBMITS As String = "MySubmits"
    Private Const EDIT As String = "edit"
    Private pvt_strTotalDepotCount As String

#Region "Page_Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtList As DataTable
        Dim objcommon As New CommonData()
        Dim intNoOfColumns As Integer
        Dim strPageTitle As String
        If Not Request.QueryString("activityid") Is Nothing Then
            pvt_Activity_Id = Request.QueryString("activityid")
        ElseIf Not Request.QueryString("ifgActivityId") Is Nothing Then
            pvt_Activity_Id = Request.QueryString("ifgActivityId")
        End If
        If Not Request.QueryString("activityid") Is Nothing Then
            pvt_strMode = Request.QueryString("mode")
            pvt_strFilterName = Request.QueryString("FLTR_NAM")
            pvt_strFilterValue = Request.QueryString("FLTR_VAL")
        End If
        pvt_strTotalDepotCount = Config.pub_GetAppConfigValue("TotalDepotCount")
        Try
            If Page.IsCallback AndAlso Not RetrieveData("listdata" + pvt_Activity_Id) Is Nothing Then
                dtList = CType(RetrieveData("listdata" + pvt_Activity_Id), DataTable)
                If dtList.TableName = RetrieveData("tablename" + pvt_Activity_Id) Then
                    intNoOfColumns = CInt(RetrieveData("listcolcount" + pvt_Activity_Id))
                    strPageTitle = CStr(RetrieveData("pagetitle" + pvt_Activity_Id))
                    pvt_strFuncValues = CStr(RetrieveData("funcvals" + pvt_Activity_Id))
                   
                    pvt_BindDataGrid(dtList, intNoOfColumns)

                    Dim blnCreateRight As Boolean = RetrieveData("createright" + pvt_Activity_Id)
                    If blnCreateRight Then
                        Dim aBtn As New ActionButton
                        aBtn.ID = "btnAdd"
                        aBtn.ValidateRowSelection = "false"
                        aBtn.OnClientClick = "onAddClick"
                        aBtn.Visible = "True"
                        aBtn.CSSClass = "btn btn-small btn-success"
                        aBtn.IconClass = "icon-plus"
                        ifgList.ActionButtons.Add(aBtn)
                        If Not pvt_Activity_Id = 26 Then
                            If pvt_Activity_Id = 74 Then
                                Dim str_044KeyValue As String = pvt_Get044KeyValue()
                                ifgList.ActionButtons.Item(0).Text = String.Concat("Add ", str_044KeyValue)
                            Else
                                ifgList.ActionButtons.Item(0).Text = String.Concat("Add ", strPageTitle)
                            End If
                            If strPageTitle = "Repair Estimate" Or strPageTitle = "Repair Approval" Or strPageTitle = "Survey Completion" Or strPageTitle = "Cleaning Certificate" Or strPageTitle = "Cleaning" Or strPageTitle = "Inspection" Then
                                ifgList.ActionButtons.Item(0).Visible = False
                            End If
                        Else
                            Dim bln070Config As Boolean
                            Dim objDepot As New Depot
                            If objcommon.GetConfigSetting("070", bln070Config).ToLower = "true" AndAlso pvt_strTotalDepotCount > objDepot.pub_GetAllDepotDetails().Tables(DepotData._V_DEPOT).Rows.Count Then
                                ifgList.ActionButtons.Item(0).Text = String.Concat("Add ", strPageTitle)
                            Else
                                ifgList.ActionButtons.Item(0).Visible = False
                            End If
                        End If

                        
                        'ifgList.AllowRefresh = True
                        Dim rBtn As New ActionButton
                        rBtn.ID = "btnRefresh"
                        rBtn.ValidateRowSelection = "false"
                        rBtn.OnClientClick = "onRefreshClick"
                        rBtn.Visible = "True"
                        rBtn.CSSClass = "btn btn-small btn-info"
                        rBtn.IconClass = "icon-refresh"
                        rBtn.Text = "Refresh"
                        ifgList.ActionButtons.Add(rBtn)

                    End If
                End If
            Else
                If Not Request.QueryString("activityid") Is Nothing Then
                    If Not IsPostBack Then
                        Pvt_ListGridBindPageLoad()
                    End If

                End If
            End If
            strPageTitle = CStr(RetrieveData("pagetitle" + pvt_Activity_Id))
            If strPageTitle = "Product" OrElse strPageTitle = "Cleaning Method" Then
                If Not Request.QueryString("Export") Is Nothing Then
                    If Request.QueryString("Export") = "True" Then
                        Dim objstream As New IO.StringWriter
                        If strPageTitle = "Product" Then
                            objstream = objcommon.ExportToExcel(CType(ifgList.DataSource, DataTable), strPageTitle, "Code,Description,CHMCL_NM,IMO_CLSS,UN_NO,Classification,GROUP_CLASSIFICATION,CLNNG_TTL_AMNT_NC,ACTV_BT", "PRDCT_CD", False)
                        ElseIf strPageTitle = "Cleaning Method" Then
                            Dim strColumnList As String
                            Dim dsCleaningMethod As New CommonUIDataSet
                            Dim objComminUI As New CommonUI
                            dsCleaningMethod = objComminUI.pvt_GetCleaningMethodDetail(CInt(objcommon.GetDepotID()), strColumnList)
                            objstream = objcommon.ExportToExcelByPassingDataTable(dsCleaningMethod.Tables("CLEANING_METHOD_EXPORT"), strPageTitle, strColumnList, "Type", False)
                        End If
                        Response.ContentType = "application/vnd.Excel"
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + strPageTitle + ".xls")
                        Response.Write(objstream.ToString())
                        Response.End()
                    End If
                End If
            End If
            Dim objConfigSetting As New ConfigSetting(CInt(objcommon.GetDepotID()))
            Dim strActivityIds As String
            strActivityIds = objConfigSetting.pub_GetConfigSingleValue("021", CInt(objcommon.GetDepotID()))
            If objConfigSetting.IsKeyExists Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Activities", "var activityIdList = new Array(" & strActivityIds & ");", True)
            End If


            pub_SetGridChanges(ifgList, "ListGrid")
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex.Message)
        Finally
            dtList = Nothing
            intNoOfColumns = Nothing
        End Try
    End Sub
#End Region


#Region "ifgList_ClientBind"
    Protected Sub ifgList_ClientBind(ByVal sender As Object, ByVal e As iFlexGridClientBindEventArgs) Handles ifgList.ClientBind
        Try
            Dim intActivityID As Int32
            Dim strMode As String = e.Parameters("mode")
            Dim intListColumnCount As Int32
            Dim objCommonUI As New CommonUI
            Dim dsCommon As CommonUIDataSet
            Dim dsListData As DataSet
            Dim dtListData As DataTable
            Dim objcommon As New CommonData
            Dim sbrClientFunction As New StringBuilder
            Dim strTableName As String = ""
            Dim strFilterName As String = ""
            Dim strFilterValue As String = ""
            Dim str_037KeyValue As String = String.Empty
            Dim bln_037KeyValue As Boolean = False
            str_037KeyValue = objcommon.GetConfigSetting("037", bln_037KeyValue)
            pvt_strQryType = e.Parameters("qrytype")
            If Not e.Parameters("FLTR_NAM") Is Nothing Then
                strFilterName = e.Parameters("FLTR_NAM")
                strFilterValue = e.Parameters("FLTR_VAL")
            End If
            If Not e.Parameters("activityid") Is Nothing Then
                intActivityID = CInt(e.Parameters("activityid"))
            End If
            dsCommon = objCommonUI.pub_GetActivityByActivityID(intActivityID)

            Dim objCommonData As New CommonData()

            With dsCommon.Tables(CommonUIData._ACTIVITY).Rows(0)
                sbrClientFunction.Append("loadWorkspace('")
                sbrClientFunction.Append(intActivityID)
                If strMode = MYSUBMITS Then
                    'REPAIR FLOW SETTING: 037    
                    'CR-001 (AWE_TO_AWP)
                    If bln_037KeyValue AndAlso (intActivityID = 85 OrElse intActivityID = 170) Then
                        intListColumnCount = .Item(CommonUIData.MY_SBMTS_CLCNT) + 1
                    Else
                        intListColumnCount = .Item(CommonUIData.MY_SBMTS_CLCNT)
                    End If

                    sbrClientFunction.Append("','edit")
                Else
                    intListColumnCount = .Item(CommonUIData.LST_CLCNT)
                    If (IsDBNull(.Item(CommonUIData.MY_SBMTS_QRY))) Then
                        sbrClientFunction.Append("','edit")
                    Else
                        sbrClientFunction.Append("','new")
                    End If

                End If
                Dim strWFdata As String = objCommonData.GenerateWFData(intActivityID)
                If strMode = MYSUBMITS Then
                    dsListData = objCommonUI.GetMySubmitsListRecords(dsCommon, strWFdata)
                Else
                    dsListData = objCommonUI.GetListRecords(dsCommon, strWFdata, intActivityID)
                End If

                If Not strFilterName Is Nothing And strFilterName <> "" Then
                    dtListData = dsListData.Tables(.Item(CommonUIData.ACTVTY_NAM))
                    If dtListData.Select(String.Concat(strFilterName, "='", strFilterValue, "'")).Length > 0 Then
                        dtListData = dtListData.Select(String.Concat(strFilterName, "='", strFilterValue, "'")).CopyToDataTable()
                    End If
                Else
                    dtListData = dsListData.Tables(.Item(CommonUIData.ACTVTY_NAM))
                End If
                pvt_strFuncValues = sbrClientFunction.ToString
                CacheData("createright" + pvt_Activity_Id, .Item(CommonUIData.CRT_RGHT_BT))

                'Based on Create Right Bit Add Action Button
                If .Item(CommonUIData.CRT_RGHT_BT) = True Then
                    If Not IsDBNull(.Item(CommonUIData.LST_QRY)) AndAlso Not IsDBNull(.Item(CommonUIData.MY_SBMTS_QRY)) Then
                        If ifgList.ActionButtons.Count > 0 Then
                            ifgList.ActionButtons.Item(0).Visible = False
                        End If
                    Else
                        Dim aBtn As New ActionButton
                        aBtn.ID = "btnAdd"
                        aBtn.ValidateRowSelection = "false"
                        aBtn.OnClientClick = "onAddClick"
                        aBtn.Visible = "True"
                        If pvt_Activity_Id = 74 Then
                            Dim str_044KeyValue As String = pvt_Get044KeyValue()
                            ifgList.ActionButtons.Item(0).Text = String.Concat("Add ", str_044KeyValue)
                        Else
                            ifgList.ActionButtons.Item(0).Text = String.Concat("Add ", Trim(.Item(CommonUIData.PG_TTL)))
                        End If
                        CacheData("createright" + pvt_Activity_Id, True)
                    End If
                Else
                    If ifgList.ActionButtons.Count > 0 Then
                        ifgList.ActionButtons.Item(0).Visible = False
                    End If
                End If
                CacheData("pagetitle" + pvt_Activity_Id, Trim(.Item(CommonUIData.PG_TTL)))
                strTableName = Trim(.Item(CommonUIData.TBL_NAM))
            End With

            ifgList.PageIndex = 0

            dtListData.TableName = strTableName
            'If Not e.Parameters("refresh") Is Nothing Then
            '    objCommonData.MaintainSortandSearch(dtListData)
            'End If


            pvt_BindDataGrid(dtListData, intListColumnCount)

            Dim intRowCount As Integer = dtListData.Rows.Count

            If dtListData.Rows.Count = 0 Then
                e.OutputParameters.Add("norecordsfound", True)
            End If
            e.OutputParameters.Add("listrowcount", dtListData.Rows.Count)

            CacheData("funcvals" + pvt_Activity_Id, pvt_strFuncValues)
            CacheData("listcolcount" + pvt_Activity_Id, intListColumnCount)
            CacheData("listdata" + pvt_Activity_Id, dtListData)
            CacheData("tablename" + pvt_Activity_Id, strTableName)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region
    Private Function pvt_Get044KeyValue()
        Dim bln_044Key As Boolean
        Dim objCommonData As New CommonData
        Dim str_044KeyValue As String = objCommonData.GetConfigSetting("044", bln_044Key)
        If bln_044Key Then
            Return str_044KeyValue
        End If
    End Function
#Region "pvt_ListGridBindPageLoad"
    Private Sub Pvt_ListGridBindPageLoad()
        Try
            Dim intActivityID As Int32 = pvt_Activity_Id
            Dim strMode As String = pvt_strMode
            Dim intListColumnCount As Int32
            Dim objCommonUI As New CommonUI
            Dim dsCommon As CommonUIDataSet
            Dim dsListData As DataSet
            Dim dtListData As DataTable
            Dim sbrClientFunction As New StringBuilder
            Dim strTableName As String = ""
            Dim strFilterName As String = pvt_strFilterName  'Request.QueryString("FLTR_NAM")
            Dim strFilterValue As String = pvt_strFilterValue 'Request.QueryString("FLTR_VAL")
            dsCommon = objCommonUI.pub_GetActivityByActivityID(intActivityID)
            Dim strNoRecordsFound As String = Nothing
            Dim strListRowCount As String = Nothing

            Dim objCommonData As New CommonData()

            With dsCommon.Tables(CommonUIData._ACTIVITY).Rows(0)
                If IsDBNull(.Item(CommonUIData.MY_SBMTS_QRY)) Then
                    divTab.Style.Add("display", "none")
                Else
                    divTab.Style.Add("display", "block")
                End If
                If strMode = MYSUBMITS Then
                    intListColumnCount = .Item(CommonUIData.MY_SBMTS_CLCNT)
                Else
                    intListColumnCount = .Item(CommonUIData.LST_CLCNT)
                End If

                Dim strWFdata As String = objCommonData.GenerateWFData(intActivityID)

                If strMode = MYSUBMITS Then
                    dsListData = objCommonUI.GetMySubmitsListRecords(dsCommon, strWFdata)
                Else
                    dsListData = objCommonUI.GetListRecords(dsCommon, strWFdata, intActivityID)
                End If
                If Not strFilterName Is Nothing And strFilterName <> "" Then
                    dtListData = dsListData.Tables(.Item(CommonUIData.ACTVTY_NAM))
                    If dtListData.Select(String.Concat(strFilterName, "='", strFilterValue, "'")).Length > 0 Then
                        dtListData = dtListData.Select(String.Concat(strFilterName, "='", strFilterValue, "'")).CopyToDataTable()
                    End If
                Else
                    dtListData = dsListData.Tables(.Item(CommonUIData.ACTVTY_NAM))
                End If

                'Build pending row click attributes
                sbrClientFunction.Append("loadWorkspace('")
                sbrClientFunction.Append(intActivityID)
                If strMode = MYSUBMITS Then
                    sbrClientFunction.Append("','edit")
                ElseIf strMode = EDIT Then
                    sbrClientFunction.Append("','edit")
                Else
                    sbrClientFunction.Append("','new")
                End If
                pvt_strFuncValues = sbrClientFunction.ToString
                CacheData("createright" + pvt_Activity_Id, .Item(CommonUIData.CRT_RGHT_BT))
                'Based on Create Right Bit Add Action Button
                If .Item(CommonUIData.CRT_RGHT_BT) = True Then
                    If Not IsDBNull(.Item(CommonUIData.LST_QRY)) AndAlso Not IsDBNull(.Item(CommonUIData.MY_SBMTS_QRY)) Then
                        If ifgList.ActionButtons.Count > 0 Then
                            ifgList.ActionButtons.Item(0).Visible = False
                        End If
                    Else
                        Dim aBtn As New ActionButton
                        aBtn.ID = "btnAdd"
                        aBtn.ValidateRowSelection = "false"
                        aBtn.OnClientClick = "onAddClick"
                        aBtn.Visible = "True"
                        aBtn.CSSClass = "btn btn-small btn-success"
                        aBtn.IconClass = "icon-plus"
                        ifgList.ActionButtons.Add(aBtn)
                        If Not pvt_Activity_Id = 26 Then
                            If pvt_Activity_Id = 74 Then
                                Dim str_044KeyValue As String = pvt_Get044KeyValue()
                                ifgList.ActionButtons.Item(0).Text = String.Concat("Add ", str_044KeyValue)
                            Else
                                ifgList.ActionButtons.Item(0).Text = String.Concat("Add ", Trim(.Item(CommonUIData.PG_TTL)))
                            End If
                        Else
                            Dim bln070Config As Boolean
                            Dim objcommon As New CommonData
                            Dim objDepot As New Depot
                            If objcommon.GetConfigSetting("070", bln070Config).ToLower = "true" AndAlso pvt_strTotalDepotCount > objDepot.pub_GetAllDepotDetails().Tables(DepotData._V_DEPOT).Rows.Count Then
                                ifgList.ActionButtons.Item(0).Text = String.Concat("Add ", Trim(.Item(CommonUIData.PG_TTL)))
                            Else
                                ifgList.ActionButtons.Item(0).Visible = False
                            End If
                        End If
                        If ifgList.ActionButtons.Item(0).Text = "Add Add Report" Then
                            ifgList.ActionButtons.Item(0).Text = "Add Report"
                        End If

                        CacheData("createright" + pvt_Activity_Id, True)
                        If Trim(.Item(CommonUIData.PG_TTL)) = "Product" OrElse Trim(.Item(CommonUIData.PG_TTL)) = "Cleaning Method" Then
                            Dim aBtnExport As New ActionButton
                            aBtnExport.ID = "btnAdd"
                            aBtnExport.ValidateRowSelection = "false"
                            aBtnExport.OnClientClick = "exportToExcel"
                            aBtnExport.Visible = "True"
                            aBtnExport.CSSClass = "btn btn-small btn-info"
                            aBtnExport.IconClass = "icon-download-alt"
                            ifgList.ActionButtons.Add(aBtnExport)
                            ifgList.ActionButtons.Item(1).Text = String.Concat("Export")
                        End If
                    End If
                    Dim rBtn As New ActionButton
                    rBtn.ID = "btnRefresh"
                    rBtn.ValidateRowSelection = "false"
                    rBtn.OnClientClick = "onRefreshClick"
                    rBtn.Visible = "True"
                    rBtn.CSSClass = "btn btn-small btn-info"
                    rBtn.IconClass = "icon-refresh"
                    rBtn.Text = "Refresh"
                    ifgList.ActionButtons.Add(rBtn)
                Else
                    If ifgList.ActionButtons.Count > 0 Then
                        ifgList.ActionButtons.Item(0).Visible = False
                    End If
                End If

                CacheData("pagetitle" + pvt_Activity_Id, Trim(.Item(CommonUIData.PG_TTL)))
                strTableName = Trim(.Item(CommonUIData.TBL_NAM))

            End With

            ifgList.PageIndex = 0

            dtListData.TableName = strTableName
            If Not Request.QueryString("refresh") Is Nothing Then
                objCommonData.MaintainSortandSearch(dtListData)
            End If


            pvt_BindDataGrid(dtListData, intListColumnCount)

            Dim intRowCount As Integer = dtListData.Rows.Count

            If dtListData.Rows.Count = 0 Then
                'e.OutputParameters.Add("norecordsfound", True)
                strNoRecordsFound = "True"
            End If
            ' e.OutputParameters.Add("listrowcount", dtListData.Rows.Count)
            If (dtListData.Rows.Count > 0) Then

                strListRowCount = dtListData.Rows.Count
            Else
                strListRowCount = "0"

            End If


            CacheData("funcvals" + pvt_Activity_Id, pvt_strFuncValues)
            CacheData("listcolcount" + pvt_Activity_Id, intListColumnCount)
            CacheData("listdata" + pvt_Activity_Id, dtListData)
            CacheData("tablename" + pvt_Activity_Id, strTableName)

            Page.ClientScript.RegisterStartupScript(GetType(String), "onAfterListBindPageLoad", _
                   "onAfterListBindPageLoad('" + strListRowCount + "','" + strNoRecordsFound + "');", True)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub

#End Region


#Region "ifgList_RowDataBound"
    ''' <summary>
    ''' Name     : ifgList_RowDataBound
    ''' Purpose  : This function is used to add attributes and restrict column width in pending grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ifgList_RowDataBound(ByVal sender As Object, ByVal e As iFlexGridRowEventArgs) Handles ifgList.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then
                e.Row.DataItem("rowindex") = e.Row.RowIndex
                e.Row.Attributes.Add("onclick", String.Concat(pvt_strFuncValues, "','", e.Row.RowIndex, "','", e.Row.DataItem(0), "');"))
                e.Row.Attributes.Add("style", "cursor:hand")
            End If

            If e.Row.RowType = DataControlRowType.Header Then
                For i As Integer = 0 To e.Row.Cells.Count - 1
                    e.Row.Cells(i).Style.Add("text-align", "left")
                Next
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex.Message)
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
            ifgList.Visible = True
            pvt_BindSchema(dt, NoOfCol)
            With ifgList
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
            ifgList.AutoGenerateColumns = False
            ifgList.Columns.Clear()
            For Each dc As DataColumn In dt.Columns
                If dc.DataType Is GetType(System.DateTime) Then
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
                    ifgList.Columns.Add(dfield)
                    'dfield.ItemStyle.Width = Unit.Pixel(90)
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
                    ifgList.Columns.Add(dfield)
                Else
                    Dim dfield As New TextboxField
                    dfield.DataField = dc.ColumnName
                    dfield.SortExpression = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.TextBox.CssClass = "lkp"
                    ifgList.Columns.Add(dfield)
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = True
                End If
                ifgList.Columns.Item(I).AllowSearch = True
                If I >= NoOfCol Then
                    ifgList.Columns.Item(I).Visible = False
                End If
                I = I + 1
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                   MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ParseRoleRights"
    Private Function ParseRoleRights(ByVal dtRights As DataTable) As String
        Dim sbrRights As New StringBuilder
        If Not dtRights Is Nothing Then
            For Each dcRights As DataColumn In dtRights.Columns
                If sbrRights.ToString() <> Nothing Then
                    sbrRights.Append("&")
                End If
                sbrRights.Append(UCase(dcRights.ColumnName.Substring(0, 4)))
                sbrRights.Append("=")
                If dtRights IsNot Nothing Then
                    With dtRights.Rows(0)
                        sbrRights.Append(.Item(dcRights.ColumnName))
                    End With
                End If
            Next
            Session.Add("rights" + pvt_Activity_Id, sbrRights.ToString())
        End If

        Return sbrRights.ToString()
    End Function
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/jquery.ui.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Callback.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Settings.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/List.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

End Class
