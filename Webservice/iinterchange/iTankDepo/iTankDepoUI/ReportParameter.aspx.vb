Imports System.Data
Imports System.Configuration
Imports iInterchange.WebControls.v4.Data
Partial Class ReportParameter
    Inherits Pagebase

#Region "Declarations"
    Private strReportsId As String
#End Region

#Region "ifgParamList_ClientBind"

    Protected Sub ifgParamList_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgParamList.ClientBind
        Try
            Dim dt As DataTable
            dt = CType(pub_RetrieveData("parameters" + strReportsId), DataTable)

            If Not dt Is Nothing Then
                e.DataSource = dt
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Sub
#End Region

#Region "ifgParamList_RowDataBound"

    Protected Sub ifgParamList_RowDataBound(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgParamList.RowDataBound
        Try
            Dim objCommonSettings As New CommonData()
            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then
                Dim drw As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim chkAll As New iInterchange.WebControls.v4.Data.iFgCheckBox
                chkAll = CType(e.Row.Cells(0).Controls(0), iInterchange.WebControls.v4.Data.iFgCheckBox)
                Dim sbrClientFunction As New StringBuilder
                sbrClientFunction.Append("ShowParam('")
                sbrClientFunction.Append(drw(CommonUIData.PRMTR_NAM).ToString)
                sbrClientFunction.Append("','")
                sbrClientFunction.Append(drw(CommonUIData.PRMTR_TYP).ToString.ToLower())
                sbrClientFunction.Append("','")
                sbrClientFunction.Append(e.Row.RowIndex)
                sbrClientFunction.Append("');")

                e.Row.Attributes.Add("onclick", sbrClientFunction.ToString)
                e.Row.Style.Add("cursor", "hand")
                Dim str_070Key As String
                Dim blnKeyExist As Boolean = False
                'Check whether Multilocation settings are Enable
                str_070Key = objCommonSettings.GetConfigSetting("070", blnKeyExist)
                Dim blnDepotReadonly As Boolean = False
                If str_070Key Then
                    If (objCommonSettings.GetHeadQuarterID() <> objCommonSettings.GetDepotID()) And drw(CommonUIData.PRMTR_NAM) = "Depot" Then
                        blnDepotReadonly = True
                    End If
                End If
                If drw(CommonUIData.PRMTR_TYP).ToString.ToLower() <> "multivalue" OrElse blnDepotReadonly = True Then
                    CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                Else
                    chkAll.Attributes.Add("onclick", sbrClientFunction.ToString)
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                          MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "ifgParamValue_ClientBind"

    Protected Sub ifgParamValue_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgParamValue.ClientBind
        Try
            Dim dtParamValue As New DataTable
            Dim strTableName As String = e.Parameters("paramselected")

            dtParamValue = CType(pub_RetrieveData(String.Concat(strTableName, strReportsId)), DataTable)
            If Not dtParamValue Is Nothing Then
                e.OutputParameters.Add("count", dtParamValue.Rows.Count)
                e.DataSource = dtParamValue
            End If
            pub_CacheData(strTableName, dtParamValue)
            pub_CacheData("SelRepParam", strTableName)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try

    End Sub

#End Region

#Region "ifgParamList_RowUpdating"

    Protected Sub ifgParamList_RowUpdating(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgParamList.RowUpdating
        Try

            Dim strParam As String
            strParam = e.OldValues(CommonUIData.PRMTR_NAM)

            Select Case e.OldValues(CommonUIData.PRMTR_TYP).ToString.ToLower
                Case "multivalue"

                    Dim dtParamValue As DataTable

                    dtParamValue = pub_RetrieveData(strParam)

                    For Each dr As DataRow In dtParamValue.Rows
                        If CBool(e.NewValues("Checked")) = True Then
                            dr("Checked") = True
                        Else
                            dr("Checked") = False
                        End If
                    Next
                    pub_CacheData(strParam, dtParamValue)
            End Select

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "Page_Load1"

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim intReportID As Integer
            Dim objCommonSettings As New CommonData()
            hdnSysDate.Value = CommonWeb.pub_Formatdate(Now())
            If Not pub_GetQueryString("activityid") Is Nothing Then
                intReportID = pub_GetQueryString("activityid")
                strReportsId = pub_GetQueryString("activityid")
            ElseIf Not pub_GetQueryString("ifgActivityId") Then
                intReportID = pub_GetQueryString("ifgActivityId")
                strReportsId = pub_GetQueryString("ifgActivityId")
            End If

            If Not pub_GetQueryString("activityname") = "" Then
                hdnReportName.Value = Request.QueryString("activityname")
            End If
            Dim objcommon As New CommonUI
            Dim objCommonData As New CommonData

            Dim dsReport As CommonUIDataSet
            dsReport = objcommon.pub_GetReportParameter(intReportID)
            'Multi Location 
            Dim count As Integer = 0
            Dim str_070Key As String
            Dim blnKeyExist As Boolean = False
            'Check whether Multilocation settings are Enable
            str_070Key = objCommonSettings.GetConfigSetting("070", blnKeyExist)
            If str_070Key = False Then
                For Each dr As DataRow In dsReport.Tables(CommonUIData._REPORT_PARAMETER).Rows
                    If dr.Item("PRMTR_NAM") = "Depot" Then
                        Exit For
                    Else
                        count = count + 1
                    End If

                Next
                dsReport.Tables(CommonUIData._REPORT_PARAMETER).Rows(count).Delete()
                dsReport.Tables(CommonUIData._REPORT_PARAMETER).AcceptChanges()
            End If

            'For replacing Config value in Parameter Name
            For Each dr As DataRow In dsReport.Tables(CommonUIData._REPORT_PARAMETER).Rows
                If dr.Item(CommonUIData.PRMTR_NAM).ToString.Contains(":") Then
                    Dim bln_007Key As Boolean
                    Dim strConfigCode() As String = dr.Item(CommonUIData.PRMTR_NAM).ToString.Split(":")
                    dr.Item(CommonUIData.PRMTR_NAM) = objCommonData.GetConfigSetting(strConfigCode(1), bln_007Key)
                    dr.Item(CommonUIData.PRMTR_DSPLY_NAM) = objCommonData.GetConfigSetting(strConfigCode(1), bln_007Key)
                End If
                dsReport.Tables(CommonUIData._REPORT_PARAMETER).AcceptChanges()
            Next

            'Multi Location
            Dim objCommonConfig As New ConfigSetting()
            Dim str_026NonEditDate As String = String.Empty
            Dim bln_026NonEditDate_Key As Boolean = False
            Dim str_027NonEditDateReportID As String
            Dim bln_027NonEditDateReportID_Key As Boolean
            Dim str_029CurrentDateReportID As String
            Dim bln_029CurrentDateReportID_Key As Boolean
            Dim bln_DefaultDate As Boolean = False
            Dim drParamName As DataRow() = Nothing
            Dim blnUpdate As Boolean = False

            str_027NonEditDateReportID = objCommonConfig.pub_GetConfigSingleValue("027", objCommonData.GetDepotID())
            bln_027NonEditDateReportID_Key = objCommonConfig.IsKeyExists

            Dim strSplit_027Condition() As String = str_027NonEditDateReportID.Split(",")
            For i = 0 To strSplit_027Condition.Length - 1
                If strSplit_027Condition(i) = intReportID Then
                    bln_DefaultDate = True
                    Exit For
                End If
            Next

            If bln_DefaultDate Then
                str_026NonEditDate = objCommonConfig.pub_GetConfigSingleValue("026", objCommonData.GetDepotID())
                bln_026NonEditDate_Key = objCommonConfig.IsKeyExists
            End If
            If Not pub_GetQueryString("mode") Is Nothing Then
                If pub_GetQueryString("mode") = "view" Then
                    If dsReport IsNot Nothing Then
                        If dsReport.Tables(CommonUIData._REPORT_PARAMETER).Rows.Count > 0 Then
                            Dim dtParam As Data.DataTable = dsReport.Tables(CommonUIData._REPORT_PARAMETER)
                            Dim dc As New Data.DataColumn("Checked")
                            dc.DataType = GetType(Boolean)
                            If Not dtParam.Columns.Contains("Checked") Then
                                dtParam.Columns.Add(dc)
                            End If
                            dc = New Data.DataColumn("prmtr_val")
                            dc.DataType = GetType(String)
                            If Not dtParam.Columns.Contains("prmtr_val") Then
                                dtParam.Columns.Add(dc)
                            End If
                            For Each dr As DataRow In dtParam.Rows
                                dr("Checked") = dr.Item(CommonUIData.PRMTR_OPT)
                            Next
                            Dim keys(0) As DataColumn
                            keys(0) = dtParam.Columns("prmtr_id")
                            dtParam.PrimaryKey = keys
                            Dim strQuery As String
                            Dim dtReportParameterValue As DataTable

                            For Each dr As DataRow In dtParam.Rows
                                If Not dr("prmtr_rl").ToString = "" And Not dr("prmtr_rl").ToString = String.Empty Then
                                    Dim strItems() As String
                                    strItems = dr("prmtr_rl").ToString.Split(CChar(","))
                                    For i As Integer = 0 To strItems.Length - 1
                                        If strItems(i) <> "" Then
                                            Dim result As String = strItems(i).Substring(strItems(i).IndexOf("<") + 1, strItems(i).IndexOf(">", strItems(i).IndexOf("<")) - strItems(i).IndexOf("<") - 1)
                                            Dim bln_KeyExist As Boolean
                                            Dim strKeyValue As String = objCommonData.GetConfigSetting(result, bln_KeyExist)
                                            If bln_KeyExist Then
                                                dr("prmtr_dsply_nam") = strKeyValue
                                            End If
                                        End If
                                    Next
                                End If

                                Select Case dr(CommonUIData.PRMTR_TYP).ToString().ToLower()
                                    Case "multivalue"
                                        Dim strParamName As String
                                        strQuery = dr(CommonUIData.PRMTR_QRY)
                                        If objCommonData.GetHeadQuarterID() = objCommonData.GetDepotID() And objCommonData.GetConfigSetting("070", False) Then

                                        Else
                                            If dr.Item(CommonUIData.REQ_DPT_ID) = "True" Then
                                                If Not strQuery.IndexOf("WHERE") = -1 Then
                                                    strQuery = String.Concat(strQuery, " AND DPT_ID=", objCommonData.GetDepotID())
                                                Else
                                                    strQuery = String.Concat(strQuery, " WHERE DPT_ID=", objCommonData.GetDepotID())
                                                End If
                                            End If
                                        End If
                                       
                                        If Not dr.Item(CommonUIData.PRMTR_ORDR_CLMN) Is DBNull.Value And Not dr.Item(CommonUIData.PRMTR_ORDR_TYP) Is DBNull.Value Then
                                            strQuery = String.Concat(strQuery, " ORDER BY ", dr.Item(CommonUIData.PRMTR_ORDR_CLMN), " ", dr.Item(CommonUIData.PRMTR_ORDR_TYP))
                                        End If
                                        dr(CommonUIData.PRMTR_QRY) = strQuery
                                        strParamName = dr(CommonUIData.PRMTR_NAM)
                                        dtReportParameterValue = objcommon.pub_GetReportParameterValue(strQuery)
                                        dc = New Data.DataColumn("Checked")
                                        dc.DataType = GetType(Boolean)
                                        ' dc.DefaultValue =True 
                                        dc.DefaultValue = dr(CommonUIData.PRMTR_OPT)
                                        'Dim dc1 As New Data.DataColumn("UnChecked")
                                        'dc1.DataType = GetType(Boolean)
                                        'dc1 = New Data.DataColumn("UnChecked")
                                        'dc1.DataType = GetType(Boolean)
                                        'dc1.DefaultValue = dr(CommonUIData.PRMTR_OPT)
                                        If strParamName = "Depot" And objCommonData.GetHeadQuarterID() = objCommonData.GetDepotID() And objCommonData.GetConfigSetting("070", False) Then

                                            dtReportParameterValue.Columns.Add(dc)
                                            For i = 0 To dtReportParameterValue.Rows.Count - 1
                                                If dtReportParameterValue.Rows(i).Item(0) = objCommonData.GetDepotCD() Then
                                                    '   dtReportParameterValue.Columns.Add(dc)
                                                    'dtReportParameterValue.Columns.Add(dc)
                                                    dtReportParameterValue.Rows(i).Item(3) = 1
                                                Else
                                                    dtReportParameterValue.Rows(i).Item(3) = 0
                                                End If

                                            Next
                                        Else
                                            If Not dtReportParameterValue.Columns.Contains("Checked") Then
                                                dtReportParameterValue.Columns.Add(dc)
                                            End If

                                        End If
                                        
                                        pub_CacheData(String.Concat(dr(CommonUIData.PRMTR_NAM), strReportsId), dtReportParameterValue)
                                        'modified for HQ
                                    Case "dropdown"
                                        dr("prmtr_val") = ""
                                        strQuery = dr(CommonUIData.PRMTR_QRY)
                                        If objCommonData.GetHeadQuarterID() = objCommonData.GetDepotID() And objCommonData.GetConfigSetting("070", False) Then  'HQ
                                            If dr.Item(CommonUIData.REQ_DPT_ID) = "True" Then
                                                'If Not strQuery.IndexOf("WHERE") = -1 Then
                                                '    strQuery = String.Concat(strQuery, " AND DPT_ID=", objCommonData.GetDepotID())
                                                'Else
                                                '    strQuery = String.Concat(strQuery, " WHERE DPT_ID=", objCommonData.GetDepotID())
                                                'End If
                                            End If
                                        Else
                                            If dr.Item(CommonUIData.REQ_DPT_ID) = "True" Then
                                                If Not strQuery.IndexOf("WHERE") = -1 Then
                                                    strQuery = String.Concat(strQuery, " AND DPT_ID=", objCommonData.GetDepotID())
                                                Else
                                                    strQuery = String.Concat(strQuery, " WHERE DPT_ID=", objCommonData.GetDepotID())
                                                End If
                                            End If
                                        End If

                                        If Not dr.Item(CommonUIData.PRMTR_ORDR_CLMN) Is DBNull.Value And Not dr.Item(CommonUIData.PRMTR_ORDR_TYP) Is DBNull.Value Then
                                            strQuery = String.Concat(strQuery, " ORDER BY ", dr.Item(CommonUIData.PRMTR_ORDR_CLMN), " ", dr.Item(CommonUIData.PRMTR_ORDR_TYP))
                                        End If
                                        dr(CommonUIData.PRMTR_QRY) = strQuery
                                    Case "string"
                                        dr("prmtr_val") = ""
                                    Case "date"
                                        Dim dt As DateTime = DateTime.Now.ToString()
                                        Dim strSplit_026Condition() As String = str_026NonEditDate.Split(",")
                                        If bln_DefaultDate = True AndAlso bln_026NonEditDate_Key Then
                                            For i = 0 To strSplit_026Condition.Length - 1
                                                If strSplit_026Condition(i) = dr("PRMTR_ID") Then
                                                    If dr(CommonUIData.PRMTR_NAM).ToString().ToLower().Contains("date from") Then
                                                        dr("prmtr_val") = Now.AddDays(-2).ToString("dd-MMM-yyyy").ToUpper()
                                                    End If
                                                    If dr(CommonUIData.PRMTR_NAM).ToString().ToLower().Contains("date to") Then
                                                        dr("prmtr_val") = Now.ToString("dd-MMM-yyyy").ToUpper()
                                                    End If
                                                End If
                                            Next
                                        End If
                                End Select
                            Next
                            pub_CacheData("parameters" + strReportsId, dtParam)
                            pvt_BindParameterGrid()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "pvt_BindParameterGrid"

    Private Sub pvt_BindParameterGrid()
        Try
            Dim dtParam As DataTable
            dtParam = CType(pub_RetrieveData("parameters" + strReportsId), DataTable)
            If Not dtParam Is Nothing Then
                ifgParamList.DataSource = dtParam.DefaultView
                ifgParamList.DataBind()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgParamValue_RowUpdating"

    Protected Sub ifgParamValue_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgParamValue.RowDataBound
        Dim objCommonData As New CommonData
        If pub_RetrieveData("SelRepParam") = "Depot" Then
            If objCommonData.GetHeadQuarterID() = objCommonData.GetDepotID() And objCommonData.GetConfigSetting("070", False) Then
            Else
                If e.Row.RowType <> DataControlRowType.Header Then
                    e.Row.Cells(0).Enabled = False
                End If

            End If
        End If
       

    End Sub

    Protected Sub ifgParamValue_RowUpdating(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgParamValue.RowUpdating

        Try
            Dim dtParamValue As DataTable
            Dim strParamName As String = pub_RetrieveData("SelRepParam")
            dtParamValue = pub_RetrieveData(String.Concat(strParamName, strReportsId))
            Dim drs As DataRow()
            drs = dtParamValue.Select(String.Concat("Code='", e.OldValues("Code"), "'"))
            If drs.Length > 0 Then
                If CBool(e.NewValues("Checked")) = True Then
                    drs(0).Item("Checked") = True
                Else
                    drs(0).Item("Checked") = False
                End If
                pub_CacheData(String.Concat(strParamName, strReportsId), dtParamValue)
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"

    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "SetSimpleParam"
                pvt_SetSimpleParam(e.GetCallbackValue("paramname"), e.GetCallbackValue("paramvalue"))
            Case "SetMultiParamValue"
                pvt_SetMultiValueParam((e.GetCallbackValue("ReportName")))
            Case "BindDropDown"
                pvt_BindDropDown(e.GetCallbackValue("param"))
            Case "GetParamValue"
                pvt_GetParamValue(e.GetCallbackValue("paramname"))
                'Case "CheckValidation"
                '    pvt_CheckValidation(e.GetCallbackValue("ReportName"))
        End Select
    End Sub

#End Region

#Region "pvt_SetSimpleParam"

    Private Sub pvt_SetSimpleParam(ByVal bv_strParamName As String, ByVal bv_strParamValue As String)

        Try
            Dim dtParam As DataTable
            dtParam = CType(pub_RetrieveData("parameters" + strReportsId), DataTable)
            Dim drsFoundRow As DataRow() = dtParam.Select(String.Concat("prmtr_nam='", bv_strParamName, "'"), "")
            If drsFoundRow.Length > 0 Then
                drsFoundRow(0)("prmtr_val") = bv_strParamValue
            Else
                drsFoundRow(0)("prmtr_val") = ""
            End If
            pub_CacheData("parameters" + strReportsId, dtParam)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            pub_SetCallbackReturnValue("Message", "Parameter Update Fails.")
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub

#End Region

#Region "pvt_SetMultiValueParam"

    Private Sub pvt_SetMultiValueParam(ByVal bv_strReportName As String)
        Try

            Dim dtParam As DataTable
            dtParam = CType(pub_RetrieveData("parameters" + strReportsId), DataTable)
            For Each dr As DataRow In dtParam.Rows
                Dim sbrParameter As New StringBuilder
                If dr.Item(CommonUIData.PRMTR_TYP).ToString.ToLower() = "multivalue" Then
                    Dim strParamName As String = dr.Item(CommonUIData.PRMTR_NAM)
                    Dim dtParamValue As DataTable = CType(pub_RetrieveData(String.Concat(strParamName, strReportsId)), DataTable)
                    Dim drsCheckedRows As DataRow() = dtParamValue.Select("Checked=1", "")
                    Dim drsFoundRow As DataRow() = dtParam.Select(String.Concat("prmtr_nam='", strParamName, "'"), "")

                    If drsCheckedRows.Length > 0 Then
                        Dim strIDs As String = ""
                        For Each drv As DataRow In drsCheckedRows
                            If strIDs <> "" Then
                                strIDs = String.Concat(strIDs, ",")
                            End If

                            strIDs = String.Concat(strIDs, "'", drv.Item("ID"), "'")
                        Next
                        If drsFoundRow.Length > 0 Then
                            drsFoundRow(0)("prmtr_val") = strIDs
                        End If
                    Else
                        drsFoundRow(0)("prmtr_val") = ""
                        If dr.Item(CommonUIData.PRMTR_OPT) Then
                            sbrParameter.Append("The Parameter ")
                            sbrParameter.Append(strParamName)
                            sbrParameter.Append(" is missing value.")
                            pub_SetCallbackReturnValue("error", sbrParameter.ToString())
                        End If
                    End If
                End If
                If dr.Item(CommonUIData.PRMTR_TYP).ToString.ToLower() = "date" Then
                    Dim drsFromDate As DataRow() = dtParam.Select(String.Concat(CommonUIData.PRMTR_NAM, "='From Date'"), "")
                    Dim drsToDate As DataRow() = dtParam.Select(String.Concat(CommonUIData.PRMTR_NAM, "='To Date'"), "")

                    If drsFromDate.Length > 0 And drsToDate.Length > 0 Then
                        If drsFromDate(0).Item("prmtr_val") <> "" AndAlso drsToDate(0).Item("prmtr_val") <> "" Then
                            Dim datFromDate As Date = CDate(drsFromDate(0).Item("prmtr_val"))
                            Dim datToDate As Date = CDate(drsToDate(0).Item("prmtr_val"))
                            If datFromDate > datToDate Then
                                sbrParameter.Append("From Date must be less than To Date ")
                                pub_SetCallbackReturnValue("error", sbrParameter.ToString())
                            End If
                        End If
                    End If
                End If
                If dr.Item(CommonUIData.PRMTR_TYP).ToString.ToLower() = "dropdown" And dr.Item(CommonUIData.PRMTR_OPT) Then
                    If dr.Item("prmtr_val").ToString.ToLower() = "select" Or dr.Item("prmtr_val").ToString = "" Then
                        Dim strParamName As String = dr.Item(CommonUIData.PRMTR_NAM)
                        sbrParameter.Append("The Parameter ")
                        sbrParameter.Append(strParamName)
                        sbrParameter.Append(" is missing value.")
                        pub_SetCallbackReturnValue("error", sbrParameter.ToString())
                    End If
                End If
            Next
            pub_CacheData("parameters" + strReportsId, dtParam)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            pub_SetCallbackReturnValue("Message", "Parameter Update Fails.")
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "pvt_BindDropDown"

    Private Sub pvt_BindDropDown(ByVal paramname As String)
        Try
            Dim dtParam As DataTable
            dtParam = CType(pub_RetrieveData("parameters" + strReportsId), DataTable)
            Dim dtReportParameterValue As DataTable
            Dim strQSDropdownData As String = ""
            Dim strQuery As String = ""
            For Each dr As DataRow In dtParam.Rows
                If dr.Item(CommonUIData.PRMTR_TYP).ToString.ToLower() = "dropdown" And dr.Item(CommonUIData.PRMTR_NAM).ToString = paramname Then
                    strQuery = dr(CommonUIData.PRMTR_QRY)
                End If
            Next
            Dim objcommon As New CommonUI
            dtReportParameterValue = objcommon.pub_GetReportParameterValue(strQuery)
            If Not dtReportParameterValue Is Nothing Then
                strQSDropdownData = CommonWeb.toQueryString(dtReportParameterValue)
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("dropdownData", strQSDropdownData)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Reports/ReportParameter.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/HelpTip.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Callback.js", MyBase.Page)
            'CommonWeb.IncludeScript("Script/UI/Home.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_LoadComplete1"

    Protected Sub Page_LoadComplete1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        ClientScript.RegisterStartupScript(GetType(System.String), "GetReportTitle", "GetReportTitle();", True)

    End Sub

#End Region

#Region "pvt_GetParamValue"

    Private Sub pvt_GetParamValue(ByVal paramname As String)
        Try
            Dim dtParam As DataTable
            dtParam = CType(pub_RetrieveData("parameters" + strReportsId), DataTable)
            Dim drsFoundRow As DataRow() = dtParam.Select(String.Concat("prmtr_nam='", paramname, "'"), "")
            If drsFoundRow.Length > 0 Then
                pub_SetCallbackReturnValue("Value", IIf(IsDBNull(drsFoundRow(0)("prmtr_val")), "", drsFoundRow(0)("prmtr_val")))
                pub_SetCallbackStatus(True)
            End If
        Catch ex As Exception
            pub_SetCallbackStatus(False)
        End Try
    End Sub

#End Region


End Class

