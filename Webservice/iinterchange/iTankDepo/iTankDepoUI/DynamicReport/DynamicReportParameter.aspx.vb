Imports Microsoft.Reporting.WebForms
Imports System.Xml
Imports System.IO

Partial Class DynamicReport_DynamicReportParameter
    Inherits Pagebase

#Region "Declaration"
    Dim dsAddDynamicReport As AddDynamicReportDataSet
    Private strReportsId As String
    Private Const PARAMETERDATA As String = "ParameterData"


#End Region

#Region "pvt_SetParameterNotRequired"
    Private Sub pvt_SetParameterNotRequired()
        lblReportColumns.Visible = False
        lblNoParameterRequired.Visible = True
    End Sub
#End Region

#Region "Page_Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtFrom As Date
        Try
            If Page.IsCallback And Not ifgParameter.DataSource Is Nothing Then
                Dim dtCB As DataTable
                dtCB = CType(ifgParameter.DataSource, DataTable)
                bindSchema(ifgParameter, dtCB)
            End If

            If Not pub_GetQueryString("activityid") Is Nothing Then
                strReportsId = pub_GetQueryString("activityid")
            ElseIf Not pub_GetQueryString("ifgActivityId") Then
                strReportsId = pub_GetQueryString("ifgActivityId")
            End If

            If Page.IsPostBack = False AndAlso Not GetQueryString("activityid") Is Nothing Then
                Dim objAddDynamicReport As New AddDynamicReport
                dsAddDynamicReport = objAddDynamicReport.Pub_GetReportParameterByReportIDValue(strReportsId)
                CacheData(PARAMETERDATA + CStr(strReportsId), dsAddDynamicReport)

                btnSubmit.Attributes.Add("onclick", "Submit_Click();return false;")
                Dim objReportingServices As Object = Nothing
                If RSVersion() = "2010" Then
                    objReportingServices = New RS2010.ReportingService2010()
                ElseIf RSVersion() = "2005" Then
                    objReportingServices = New RS2005.ReportingService2005(Config.pub_GetAppConfigValue("ReportServicePath"))
                End If

                objReportingServices.Credentials = System.Net.CredentialCache.DefaultCredentials
                If ConfigurationManager.AppSettings("ReportServerCredential").ToUpper = "TRUE" Then
                    If ConfigurationManager.AppSettings("ReportServerDomain") <> "" Then
                        objReportingServices.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("ReportServerUID"), ConfigurationManager.AppSettings("ReportServerPWD"), ConfigurationManager.AppSettings("ReportServerDomain"))
                    Else
                        objReportingServices.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("ReportServerUID"), ConfigurationManager.AppSettings("ReportServerPWD"))
                    End If
                End If

                Dim objReportParam As New ReportParam
                Dim strReportName As String = objReportParam.pub_GetReportNamebyReportID(strReportsId)
                btnRunReport.Attributes.Add("onclick", "RunReport('" & strReportName & "');return false;")

                Dim strReportPath As String = String.Concat(ConfigurationManager.AppSettings("ReportFolderpath"), strReportName)
                Dim forRendering As Boolean = False
                Dim historyID As String = Nothing
                Dim parameters As Object = Nothing
                Dim strParamQuery As String

                If RSVersion() = "2010" Then
                    Dim values As RS2010.ParameterValue() = Nothing
                    Dim credentials As RS2010.DataSourceCredentials() = Nothing
                    parameters = New RS2010.ItemParameter()
                    parameters = objReportingServices.GetItemParameters(strReportPath, historyID, forRendering, values, credentials)
                ElseIf RSVersion() = "2005" Then
                    Dim values As RS2005.ParameterValue() = Nothing
                    Dim credentials As RS2005.DataSourceCredentials() = Nothing
                    parameters = New RS2005.ReportParameter()
                    parameters = objReportingServices.GetReportParameters(strReportPath, historyID, forRendering, values, credentials)
                End If


                If parameters.Length = 0 Then
                    pvt_SetParameterNotRequired()
                    Exit Sub
                Else
                    lblNoParameterRequired.Visible = True
                    lblNoParameterRequired.Visible = False
                End If


                Dim dsParameterMasters As New DataSet
                Dim dsParameter As New ReportParamDataSet

                Dim dtParameterLists As New DataTable


                dtParameterLists.Columns.Add("parameter", GetType(String))
                dtParameterLists.Columns.Add("type", GetType(String))
                Dim dc As New DataColumn("Checked", GetType(Boolean))
                dc.DefaultValue = True
                dtParameterLists.Columns.Add(dc)
                dtParameterLists.Columns.Add("parametername", GetType(String))
                dtParameterLists.Columns.Add("parametervalue", GetType(String))
                dtParameterLists.Columns.Add("parameterquery", GetType(String))

                Dim objCommondata As New CommonData
                Dim intDepotID As Integer = CInt(objCommondata.GetDepotID())
                If RSVersion() = "2010" Then
                    For Each Parm As RS2010.ItemParameter In parameters
                        If Parm.PromptUser And Parm.Prompt <> String.Empty Then
                            Dim drParamList As DataRow = dtParameterLists.NewRow
                            drParamList("parameter") = Parm.Name
                            drParamList("parametername") = Parm.Prompt
                            If Parm.ParameterTypeName = "String" And Parm.Name = "reporttitle" Then
                                drParamList("type") = "title"
                                drParamList("parametervalue") = strReportName
                            ElseIf (Parm.ParameterTypeName = "String" Or Parm.ParameterTypeName = "Integer") And Parm.MultiValue Then
                                'Change Type as dropdown for master paramters
                                Dim blnDropDown As Boolean = False
                                If Not dsAddDynamicReport Is Nothing AndAlso dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Rows.Count > 0 Then
                                    Dim drColumnName As DataRow()
                                    drColumnName = dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Select(String.Concat(AddDynamicReportData.PRMTR_NAM, "='", Parm.Name, "'"))
                                    If drColumnName.Length > 0 Then
                                        blnDropDown = CBool(drColumnName(0).Item(AddDynamicReportData.PRMTR_DRPDWN))
                                    End If
                                End If
                                If Not blnDropDown Then
                                    drParamList("type") = "master"
                                Else
                                    drParamList("type") = "dropdown"
                                End If
                                strParamQuery = GetReportDatasetQuery(strReportPath, Parm.Name)
                                drParamList("parameterquery") = strParamQuery

                                If dsParameterMasters.Tables.Contains(Parm.Name) Then
                                    dsParameterMasters.Tables(Parm.Name).Clear()
                                    dsParameterMasters.Tables(Parm.Name).Merge(objReportParam.pub_GetParamTable(strParamQuery, True, Parm.Name, intDepotID).Tables(Parm.Name))
                                Else

                                    dsParameter = objReportParam.pub_GetParamTable(strParamQuery, True, Parm.Name, intDepotID)
                                    dsParameterMasters.Tables.Add(dsParameter.Tables(Parm.Name).Copy)
                                End If
                            ElseIf Parm.ParameterTypeName = "String" Then
                                drParamList("type") = "string"
                                If drParamList("parametername").ToString.Contains("UserName") Then
                                    drParamList("parametervalue") = ""
                                Else
                                    drParamList("parametervalue") = ""
                                End If
                            ElseIf Parm.ParameterTypeName = "Integer" Then
                                drParamList("type") = "integer"
                                drParamList("parametervalue") = 0
                            ElseIf Parm.ParameterTypeName = "DateTime" Then
                                drParamList("type") = "date"
                                dtFrom = DateTime.Today
                                If drParamList("parametername").ToString.Contains("From") Then
                                    drParamList("parametervalue") = CommonWeb.pub_Formatdate(dtFrom.Subtract(TimeSpan.FromHours(dtFrom.Hour + 720)))
                                ElseIf drParamList("parametername").ToString.Contains("To") Then
                                    drParamList("parametervalue") = CommonWeb.pub_Formatdate(dtFrom.Add(TimeSpan.FromHours(dtFrom.Hour + 24)))
                                Else
                                    drParamList("parametervalue") = CommonWeb.pub_Formatdate(dtFrom)
                                End If
                            End If
                            dtParameterLists.Rows.Add(drParamList)
                        End If
                    Next
                ElseIf RSVersion() = "2005" Then

                    For Each Parm As RS2005.ReportParameter In parameters
                        If Parm.PromptUser And Parm.Prompt <> String.Empty Then
                            Dim drParamList As DataRow = dtParameterLists.NewRow
                            drParamList("parameter") = Parm.Name
                            drParamList("parametername") = Parm.Prompt
                            If Parm.Type = RS2005.ParameterTypeEnum.String And Parm.Name = "reporttitle" Then
                                drParamList("type") = "title"
                                drParamList("parametervalue") = strReportName
                            ElseIf (Parm.Type = RS2005.ParameterTypeEnum.String Or Parm.Type = RS2005.ParameterTypeEnum.Integer) And Parm.MultiValue Then
                                Dim blnDropDown As Boolean = False
                                If Not dsAddDynamicReport Is Nothing AndAlso dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Rows.Count > 0 Then
                                    Dim drColumnName As DataRow()
                                    drColumnName = dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Select(String.Concat(AddDynamicReportData.PRMTR_NAM, "='", Parm.Name, "'"))
                                    If drColumnName.Length > 0 Then
                                        ' strColumnName = drColumnName(0).Item(AddDynamicReportData.PRMTR_NAM)
                                        blnDropDown = CBool(drColumnName(0).Item(AddDynamicReportData.PRMTR_DRPDWN))
                                    End If
                                End If
                                If Not blnDropDown Then
                                    drParamList("type") = "master"
                                Else
                                    drParamList("type") = "dropdown"
                                End If

                                strParamQuery = GetReportDatasetQuery(strReportPath, Parm.Name)
                                drParamList("parameterquery") = strParamQuery

                                'If Not blnDropDown Then
                                If dsParameterMasters.Tables.Contains(Parm.Name) Then
                                    dsParameterMasters.Tables(Parm.Name).Clear()
                                    If GetQueryString("mode") = MODE_NEW Then
                                        dsParameterMasters.Tables(Parm.Name).Merge(objReportParam.pub_GetParamTable(strParamQuery, True, Parm.Name, intDepotID).Tables(Parm.Name))
                                    Else
                                        dsParameterMasters.Tables(Parm.Name).Merge(objReportParam.pub_GetParamTable(strParamQuery, False, Parm.Name, intDepotID).Tables(Parm.Name))
                                    End If
                                Else
                                    If GetQueryString("mode") = MODE_NEW Then
                                        dsParameter = objReportParam.pub_GetParamTable(strParamQuery, True, Parm.Name, intDepotID)
                                    Else
                                        dsParameter = objReportParam.pub_GetParamTable(strParamQuery, False, Parm.Name, intDepotID)
                                    End If
                                    'pub_GetParamTable(strParamQuery, True, Parm.Name)
                                    dsParameterMasters.Tables.Add(dsParameter.Tables(Parm.Name).Copy)
                                End If
                                'End If

                                'dsParameterMasters.Tables.Add(objReportParam.GetParameterValue(Parm.Name, True))
                                ' dsParameterMasters.Tables.Add(objReportParameters.GetParamTable(Parm.Name, strTPs, (GetQueryString("mode") = MODE_NEW), lngUsrId
                            ElseIf Parm.Type = RS2005.ParameterTypeEnum.String Then
                                drParamList("type") = "string"
                                If drParamList("parametername").ToString.Contains("UserName") Then
                                    drParamList("parametervalue") = CStr(objCommondata.GetCurrentUserName())
                                Else
                                    drParamList("parametervalue") = ""
                                End If

                            ElseIf Parm.Type = RS2005.ParameterTypeEnum.Integer Then
                                drParamList("type") = "integer"
                                drParamList("parametervalue") = 0
                            ElseIf Parm.Type = RS2005.ParameterTypeEnum.DateTime Then

                                drParamList("type") = "date"
                                dtFrom = DateTime.Today
                                If drParamList("parametername").ToString.Contains("From") Then
                                    drParamList("parametervalue") = CommonWeb.pub_Formatdate(dtFrom.Subtract(TimeSpan.FromHours(dtFrom.Hour + 720)))
                                ElseIf drParamList("parametername").ToString.Contains("To") Then
                                    drParamList("parametervalue") = CommonWeb.pub_Formatdate(dtFrom.Add(TimeSpan.FromHours(dtFrom.Hour + 24)))
                                Else
                                    drParamList("parametervalue") = CommonWeb.pub_Formatdate(dtFrom)
                                End If
                            End If
                            dtParameterLists.Rows.Add(drParamList)
                        End If
                    Next
                End If



                Dim keys(0) As DataColumn
                keys(0) = dtParameterLists.Columns("parameter")
                dtParameterLists.PrimaryKey = keys

                If GetQueryString("mode") = MODE_NEW Then
                    txtReportTitle.Text = strReportName
                    CacheData("ReportHeader" + strReportsId, strReportName)
                Else
                    Dim rpthdrid As Integer
                    rpthdrid = GetSelectedRow(strReportsId)
                    rhid.Value = rpthdrid
                    Dim ExistingParameter As Microsoft.Reporting.WebForms.ReportParameter()
                    ExistingParameter = pvt_GetSavedParams(rpthdrid)
                    ApplySelectedParams(dtParameterLists, dsParameterMasters, ExistingParameter)
                End If

                dsParameterMasters.AcceptChanges()
                dtParameterLists.AcceptChanges()

                CacheData("reportds" + CStr(strReportsId), dsParameterMasters)
                CacheData("reportparamlist" + CStr(strReportsId), dtParameterLists)

                pvt_BindParameterGrid(strReportsId)
                ' required to set the parameter name in javascript - non master parameter requires this to run
                datParam.Attributes.Add("param", "")
                txtstrParam.Attributes.Add("param", "")
                txtintParam.Attributes.Add("param", "")
                txtReportTitle.Attributes.Add("param", "reporttitle")

            End If
            'lblReportColumns.Style.Add("letter-spacing", "1.5")
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Sub
#End Region

#Region "pvt_BindParameterGrid"
    Private Sub pvt_BindParameterGrid(ByVal intActivityId As Integer)
        Try
            Dim dt As DataTable
            dt = CType(RetrieveData("reportparamlist" + CStr(intActivityId)), DataTable)

            dt.DefaultView.RowFilter = "type not in ('title')"

            If Not dt Is Nothing Then
                ifgParameterList.DataSource = dt.DefaultView
                ifgParameterList.DataBind()
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "SaveParam"
                pvt_SaveParameters(e.GetCallbackValue("reporttitle"), _
                                   CLng(e.GetCallbackValue("activityid")), _
                                   e.GetCallbackValue("savemode"), _
                                   e.GetCallbackValue("rhid"))
            Case "SetSimpleParam"
                pvt_SetSimpleParam(e.GetCallbackValue("param"), _
                                   e.GetCallbackValue("paramvalue"))
            Case "ResetParams"
                pvt_ReSetParam()
            Case "validateReportTitle"
                pvt_ValidateReportTitle(e.GetCallbackValue("reporttitle"), _
                                   CLng(e.GetCallbackValue("activityid")))
            Case "validateReportParameters"
                pvt_ValidateReportParameters(e.GetCallbackValue("ReportId"), _
                                   e.GetCallbackValue("ReportName"))
            Case "bindDropDown"
                pvt_BindDropDown(e.GetCallbackValue("param"))
        End Select
    End Sub

#End Region

#Region "pvt_BindDropDown"

    Private Sub pvt_BindDropDown(ByVal paramname As String)
        Try
            Dim dtParam As DataTable
            Dim objCommondata As New CommonData
            dtParam = CType(RetrieveData("reportparamlist" + strReportsId), DataTable)
            dsAddDynamicReport = CType(RetrieveData(PARAMETERDATA + strReportsId), AddDynamicReportDataSet)

            Dim strDependent As String = String.Empty
            If Not dsAddDynamicReport Is Nothing AndAlso dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Rows.Count > 0 Then
                Dim drColumnName As DataRow()
                drColumnName = dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Select(String.Concat(AddDynamicReportData.PRMTR_NAM, "='", paramname, "'"))
                If drColumnName.Length > 0 Then
                    strDependent = drColumnName(0).Item(AddDynamicReportData.PRMTR_DPNDNT).ToString()
                End If
            End If
           

            Dim dtReportParameterValue As DataTable
            Dim strQSDropdownData As String = ""
            Dim strQuery As String = ""
            Dim sbrQuery As New StringBuilder
            For Each dr As DataRow In dtParam.Rows
                If dr.Item("type").ToString.ToLower() = "dropdown" And dr.Item("parameter").ToString = paramname Then
                    strQuery = dr("parameterquery")
                End If
            Next

            Dim intDepotID As Integer = CInt(objCommondata.GetDepotID())

            Dim strKeyword As String() = {"ORDER BY"}
            Dim strParts As String() = strQuery.Split(strKeyword, StringSplitOptions.None)
            Dim strOrderBy As String = ""
            If strParts.Length > 1 Then
                strQuery = strParts(0)
                strOrderBy = String.Concat(" ORDER BY ", strParts(1))
            End If

            If strQuery.IndexOf("WHERE") > 0 Then
                strQuery = strQuery.Replace("WHERE", String.Concat("WHERE ", ReportParamData.DPT_ID, "=", intDepotID, " AND "))
            Else
                strQuery = strQuery + String.Concat(" WHERE ", ReportParamData.DPT_ID, "=", intDepotID)
            End If

            If strDependent <> "" Then
                Dim drParam As DataRow()
                Dim strDependentValue As String = String.Empty
                If Not dtParam Is Nothing Then
                    drParam = dtParam.Select(String.Concat("parameter='", strDependent, "'"))
                End If
                If drParam.Length > 0 Then

                    If Not (drParam(0).Item("parametervalue") Is Nothing OrElse IsDBNull(drParam(0).Item("parametervalue")) OrElse drParam(0).Item("parametervalue") = "" OrElse drParam(0).Item("parametervalue") = "select") Then
                        strDependentValue = drParam(0).Item("parametervalue")
                    End If
                End If

                
                'If strQuery.IndexOf("WHERE") > 0 Then
                '    If strDependentValue <> "" Then
                '        strQuery = String.Concat(strQuery, " AND Dependent=", strDependentValue, strOrderBy)
                '    Else
                '        strDependentValue = "na"
                '        strQuery = String.Concat(strQuery, " AND Dependent='", strDependentValue, "'")
                '    End If
                'Else
                sbrQuery.Append("SELECT * FROM (")
                sbrQuery.Append(strQuery)
                sbrQuery.Append(")B WHERE DEPENDENT='")
                sbrQuery.Append(strDependentValue)
                sbrQuery.Append("' ")
                sbrQuery.Append(strOrderBy)
                'If strDependentValue <> "" Then
                'strQuery = String.Concat(strQuery, " WHERE Dependent=", strDependentValue, strOrderBy)
                'Else
                '   strDependentValue = "na"
                '  strQuery = String.Concat(strQuery, " WHERE Dependent='", strDependentValue, "'")
                'End If
                ' End If
            Else

                sbrQuery.Append(strQuery)

            End If

            Dim objReportParam As New ReportParam


            dtReportParameterValue = objReportParam.pub_GetParamTable(sbrQuery.ToString(), True, paramname, intDepotID, True).Tables(paramname)
            Dim dtSelectedColumns As DataTable = dtReportParameterValue.DefaultView.ToTable(False, "ID", "CODE")
            If Not dtSelectedColumns Is Nothing Then
                strQSDropdownData = CommonWeb.toQueryString(dtSelectedColumns)
            End If
            pub_SetCallbackStatus(True)
            pub_SetCallbackReturnValue("dropdownData", strQSDropdownData)

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateReportParameters"
    Private Sub pvt_ValidateReportParameters(ByVal bv_strReportID As String, ByVal bv_strReportName As String)
        Try

            dsAddDynamicReport = CType(RetrieveData(PARAMETERDATA + CStr(bv_strReportID)), AddDynamicReportDataSet)

            Dim dsParameterMasters As DataSet
            Dim dtParameter As DataTable


            dsParameterMasters = RetrieveData("reportds" + CStr(bv_strReportID))
            dtParameter = RetrieveData("reportparamlist" + CStr(bv_strReportID))

            If Not dsParameterMasters Is Nothing AndAlso dsParameterMasters.Tables.Contains("reportparamlist") Then
                dsParameterMasters.Tables.Remove("reportparamlist")
            End If

            If dsParameterMasters Is Nothing Then
                pub_SetCallbackReturnValue("status", "true")
                pub_SetCallbackStatus(True)
                Exit Sub
            End If

            Dim sbrParameter As New StringBuilder()
            Dim dts As New DataTable()
            dts.Columns.Add("paramname", GetType(String))
            dts.Columns.Add("status", GetType(Boolean))
            Dim dr As DataRow
            For Each dt As DataTable In dsParameterMasters.Tables

                'Dim strColumnName As String = String.Empty
                Dim blnParameterReq As Boolean = True
                Dim blnDropdown As Boolean = False
                If Not dsAddDynamicReport Is Nothing AndAlso dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Rows.Count > 0 Then
                    Dim drColumnName As DataRow()
                    drColumnName = dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Select(String.Concat(AddDynamicReportData.PRMTR_NAM, "='", dt.TableName, "'"))
                    If drColumnName.Length > 0 Then
                        ' strColumnName = drColumnName(0).Item(AddDynamicReportData.PRMTR_NAM)
                        blnParameterReq = CBool(drColumnName(0).Item(AddDynamicReportData.PRMTR_OPT))
                        blnDropdown = CBool(drColumnName(0).Item(AddDynamicReportData.PRMTR_DRPDWN))
                    End If
                End If
                Dim drs As DataRow()
                drs = dt.Select("Select=1")
                dr = dts.NewRow
                If drs.Length = 0 AndAlso blnParameterReq = True AndAlso Not blnDropdown Then
                    If sbrParameter.ToString() <> "" Then
                        sbrParameter.Append(", ")
                    End If
                    sbrParameter.Append(dt.TableName)
                    dr.Item("status") = False
                Else
                    dr.Item("status") = True
                End If
                dts.Rows.Add(dr)
            Next

            If Not dtParameter.Columns.Contains("status") Then
                dtParameter.Columns.Add("status", GetType(Boolean))
            End If

            For Each drItem As DataRow In dtParameter.Rows

                Dim blnParameterReq As Boolean = True
                If dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Rows.Count > 0 Then
                    Dim drColumnName As DataRow()
                    drColumnName = dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Select(String.Concat(AddDynamicReportData.PRMTR_NAM, "='", drItem("parameter"), "'"))
                    If drColumnName.Length > 0 Then
                        ' strColumnName = drColumnName(0).Item(AddDynamicReportData.PRMTR_NAM)
                        blnParameterReq = CBool(drColumnName(0).Item(AddDynamicReportData.PRMTR_OPT))
                    End If
                End If

                If (drItem("parametervalue") Is Nothing OrElse IsDBNull(drItem("parametervalue")) OrElse drItem("parametervalue") = "" _
                    OrElse (drItem("parametervalue") = "select" AndAlso drItem("type") = "dropdown")) AndAlso blnParameterReq = True Then

                    If sbrParameter.ToString() <> "" And drItem("type") <> "master" Then
                        sbrParameter.Append(", ")
                    End If
                    If drItem("type") <> "master" Then
                        sbrParameter.Append(drItem("parametername"))
                    End If
                    drItem.Item("status") = False
                Else
                    drItem.Item("status") = True
                End If


                If blnParameterReq = False AndAlso drItem("type") = "dropdown" Then
                    If Not (drItem("parametervalue") Is Nothing OrElse IsDBNull(drItem("parametervalue")) OrElse drItem("parametervalue") = "" OrElse drItem("parametervalue") = "select") AndAlso blnParameterReq = False Then
                        drItem("parametervalue") = ""
                    End If
                End If
                'If blnParameterReq = False AndAlso drItem("type") = "date" Then
                '    Dim dtFrom As Date
                '    dtFrom = DateTime.Today
                '    If IsDBNull(drItem("parametervalue")) OrElse drItem("parametervalue") Is Nothing OrElse drItem("parametervalue") = "" Then
                '        If drItem("parametername").ToString.Contains("From") Then
                '            drItem("parametervalue") = CommonWeb.pub_Formatdate(dtFrom.Subtract(TimeSpan.FromHours(dtFrom.Hour + 2160)))
                '        ElseIf drItem("parametername").ToString.Contains("To") Then
                '            drItem("parametervalue") = CommonWeb.pub_Formatdate(dtFrom.Add(TimeSpan.FromHours(dtFrom.Hour + 24)))
                '        Else
                '            drItem("parametervalue") = CommonWeb.pub_Formatdate(dtFrom)
                '        End If
                '    End If
                'End If

            Next

            Dim drse As DataRow()
            Dim strStatus As String = ""
            Dim strStatusError As String = ""
            drse = dts.Select("status=0")
            Dim strReportIDs As String = ""
            'Dim intReCount As Integer = 0
            'If (Not Config.pub_GetAppConfigValue("ReportConfigurationCSV").ToString = "") Then
            '    strReportIDs = Config.pub_GetAppConfigValue("ReportConfigurationCSV").ToString()
            '    For ir As Integer = 0 To strReportIDs.Split(",").Length() - 1
            '        If CInt(strReportIDs.Split(",")(ir).Split(":")(0)) = bv_strReportID Then
            '            intReCount = CInt(strReportIDs.Split(",")(ir).Split(":")(1))
            '        End If
            '    Next
            'End If

            'If intReCount > 0 Then
            '    If drse.Length > intReCount - 1 Then
            '        strStatus = "0"
            '        strStatusError = String.Concat("Please select the following parameters ", sbrParameter.ToString())
            '    End If
            'Else
            If drse.Length > 0 Then
                strStatus = "0"
                strStatusError = String.Concat("Please select the following parameters ", sbrParameter.ToString())
            End If
            ' End If


            drse = dtParameter.Select("status=0 and type<>'master'")
            If drse.Length > 0 Then
                strStatus = "0"
                strStatusError = String.Concat("Please select the following parameters ", sbrParameter.ToString())
            End If

            For Each drParam As DataRow In dtParameter.Rows
                If (drParam("type") = "date" AndAlso Not drParam("parametername").ToString.Contains("From") AndAlso Not drParam("parametervalue").ToString.Contains("23:59:59")) Then
                    drParam("parametervalue") = drParam("parametervalue") + " 23:59:59"
                End If

                If drParam.Item("type").ToString.ToLower() = "date" Then
                    Dim drsFromDate As DataRow() = dtParameter.Select("parametername='From Date'")
                    Dim drsToDate As DataRow() = dtParameter.Select("parametername='To Date'")

                    If drsFromDate.Length > 0 And drsToDate.Length > 0 Then
                        If drsFromDate(0).Item("parametervalue") <> "" AndAlso drsToDate(0).Item("parametervalue") <> "" Then
                            Dim datFromDate As Date = CDate(drsFromDate(0).Item("parametervalue"))
                            Dim datToDate As Date = CDate(drsToDate(0).Item("parametervalue"))
                            If datFromDate > datToDate Then
                                strStatus = "0"
                                strStatusError = "From Date must be less than To Date "
                            End If
                        End If
                    End If
                End If
            Next
            Dim strReferenceNo As String = ""

            If strStatusError = "" Then

                dtParameter.TableName = "reportparamlist"

                dsParameterMasters.Tables.Add(dtParameter)

            End If
            If strStatus <> "0" AndAlso strStatusError = "" Then
                pub_SetCallbackReturnValue("status", "true")
            Else
                pub_SetCallbackReturnValue("status", "false")
            End If
            pub_SetCallbackReturnValue("statuserror", strStatusError)

            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_ValidateAddReportCode"
    Private Sub pvt_ValidateReportTitle(ByVal bv_strReportName As String, ByVal bv_strActivityId As String)
        Try
            Dim objReportParam As New ReportParam
            Dim objCommondata As New CommonData

            Dim intReportCount As Integer


            Dim intDepotID As Integer = CInt(objCommondata.GetDepotID())
            intReportCount = objReportParam.pub_ValidateReport_HeaderbyReportTitle(bv_strReportName, bv_strActivityId, intDepotID)
            If intReportCount > 0 Then
                pub_SetCallbackReturnValue("pkValid", "false")
                pub_SetCallbackReturnValue("Message", "Report Title Already Exists.")
            Else
                pub_SetCallbackReturnValue("pkValid", "true")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_SaveParameters"
    Private Sub pvt_SaveParameters(ByVal strReportTitle As String, ByVal i32ActivityId As Long, ByVal strsavemode As String, ByVal rhid As String)
        Try
            Dim objCommondata As New CommonData
            Dim binform As Runtime.Serialization.Formatters.Binary.BinaryFormatter = New Runtime.Serialization.Formatters.Binary.BinaryFormatter
            Dim memstream As New IO.MemoryStream
            Dim ReportSource As New ReportViewerDataSource

            binform.Serialize(memstream, ReportSource.GetDynamicReportParameters(CType(RetrieveData("reportparamlist" + CStr(i32ActivityId)), DataTable), CType(RetrieveData("reportds" + CStr(i32ActivityId)), DataSet), objCommondata.GetCurrentUserName()))
            Dim btReportParameter() As Byte
            btReportParameter = memstream.ToArray

            Dim objReportParam As New ReportParam
            Dim intRHId As Integer


            Dim intDepotID As Integer = CInt(objCommondata.GetDepotID())
            Dim strUserName As String = CStr(objCommondata.GetCurrentUserName())

            Select Case strsavemode
                Case "new"
                    intRHId = objReportParam.pub_CreateReport_Header(strReportTitle, btReportParameter, i32ActivityId, strUserName, DateTime.Now, intDepotID)
                Case "edit"
                    objReportParam.pub_ModifyReport_Header(rhid, strReportTitle, btReportParameter, i32ActivityId, strUserName, DateTime.Now, intDepotID)
            End Select

            CType(RetrieveData("reportds" + CStr(i32ActivityId)), DataSet).AcceptChanges()
            CType(RetrieveData("reportparamlist" + CStr(i32ActivityId)), DataTable).AcceptChanges()

            pub_SetCallbackReturnValue("rhid", intRHId)

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            pub_SetCallbackReturnValue("Message", "Unable to Save.")
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetSavedParams"
    Private Function pvt_GetSavedParams(ByVal bv_i32ReportHeaderID As Integer) As ReportParameter()
        Try
            Dim objReportParam As New ReportParam
            Dim dsReportHeader As ReportParamDataSet
            Dim bytParam As Byte()
            Dim binform As Runtime.Serialization.Formatters.Binary.BinaryFormatter = New Runtime.Serialization.Formatters.Binary.BinaryFormatter
            Dim reportParam() As ReportParameter
            dsReportHeader = objReportParam.pub_GetReport_Header(bv_i32ReportHeaderID)
            If Not dsReportHeader Is Nothing Then
                If dsReportHeader.Tables(ReportParamData._REPORT_HEADER).Rows.Count > 0 Then
                    bytParam = dsReportHeader.Tables(ReportParamData._REPORT_HEADER).Rows(0).Item(ReportParamData.PRMTR_VL)
                    reportParam = CType(binform.Deserialize(New IO.MemoryStream(bytParam)), ReportParameter())
                End If
            End If
            Return reportParam
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "ApplySelectedParams"
    Private Sub ApplySelectedParams(ByRef br_dt As DataTable, ByRef br_ds As DataSet, ByVal bv_rpr() As ReportParameter)
        Try
            Dim dr() As DataRow
            Dim drActual() As DataRow

            For Each Parm As ReportParameter In bv_rpr

                dr = br_dt.Select(String.Concat("parameter='", Parm.Name, "'"))
                If dr.Length > 0 Then
                    Select Case dr(0).Item("type").ToString
                        Case "title"
                            dr(0).Item("parametervalue") = Parm.Values.Item(0)
                            'txtReportTitle.Text = Parm.Values.Item(0)
                        Case "string", "integer", "date", "droppdown"
                            dr(0).Item("parametervalue") = Parm.Values.Item(0)
                        Case "master"
                            For icount As Integer = 0 To Parm.Values.Count - 1
                                If Not Parm.Values(icount) Is Nothing Then
                                    ' changes done to accomodate container no selection possible
                                    If br_ds.Tables(Parm.Name).Columns("id").DataType Is GetType(String) Then
                                        drActual = br_ds.Tables(Parm.Name).Select(String.Concat("id = '", Parm.Values(icount), "'"))
                                    Else
                                        drActual = br_ds.Tables(Parm.Name).Select(String.Concat("id = ", Parm.Values(icount)))
                                    End If
                                    If drActual.Length > 0 Then
                                        drActual(0).Item("Select") = True
                                    End If
                                End If
                            Next
                    End Select
                End If
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Sub
#End Region

#Region "ifgParameter_ClientBind"
    Protected Sub ifgParameter_ClientBind(ByVal sender As Object, ByVal e As iFlexGridClientBindEventArgs) Handles ifgParameter.ClientBind
        Try
            Dim ds As DataSet = CType(RetrieveData("reportds" + GetQueryString("ifgActivityId")), DataSet)
            bindSchema(ifgParameter, ds.Tables(e.Parameters.Item("paramselected")))
            ifgParameter.Visible = True
            ifgParameter.DataSource = ds.Tables(e.Parameters.Item("paramselected"))
            Dim dtParamClientBind As DataTable = ds.Tables(e.Parameters.Item("paramselected"))
            Dim blnSelect As Boolean = CBool(e.Parameters.Item("Select"))
            If dtParamClientBind Is Nothing OrElse Not dtParamClientBind.Rows.Count > 0 Then
                e.OutputParameters.Add("norecordsfound", "True")
            ElseIf Not IsNothing(e.Parameters.Item("Select")) Then
                For Each dr As DataRow In dtParamClientBind.Rows
                    dr.Item("Select") = blnSelect
                Next
                ds.Tables(e.Parameters.Item("paramselected")).Merge(dtParamClientBind)
                CacheData("reportds" + GetQueryString("ifgActivityId"), ds)
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Sub
#End Region

#Region "ifgParameterList_RowDataBound"
    Protected Sub ifgParameterList_RowDataBound(ByVal sender As Object, ByVal e As iFlexGridRowEventArgs) Handles ifgParameterList.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then
                Dim drw As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim chkAll As New iFgCheckBox
                chkAll = CType(e.Row.Cells(0).Controls(0), iFgCheckBox)
                Dim sbrClientFunction As New StringBuilder
                sbrClientFunction.Append("ShowParam('")
                sbrClientFunction.Append(drw("parameter").ToString)
                sbrClientFunction.Append("','")
                sbrClientFunction.Append(drw("type").ToString)
                sbrClientFunction.Append("','")
                sbrClientFunction.Append(drw("parametername").ToString)
                sbrClientFunction.Append("','")
                sbrClientFunction.Append(e.Row.RowIndex)
                sbrClientFunction.Append("',")
                sbrClientFunction.Append("this")
                sbrClientFunction.Append(");")

                chkAll.Attributes.Add("onclick", sbrClientFunction.ToString)
                chkAll.Style.Add("cursor", "hand")

                e.Row.Attributes.Add("onclick", sbrClientFunction.ToString)
                e.Row.Style.Add("cursor", "hand")

                Dim hypPdflnk As HyperLink
                hypPdflnk = CType(e.Row.Cells(1).Controls(0), HyperLink)
                hypPdflnk.Attributes.Add("onclick", sbrClientFunction.ToString)
                hypPdflnk.NavigateUrl = "#"
                hypPdflnk.Text = drw("parametername").ToString
                hypPdflnk.ForeColor = Drawing.Color.Black
                hypPdflnk.CssClass = "lbl"
                hypPdflnk.Style.Add("text-decoration", "none")

                If drw("type") <> "master" Then
                    CType(e.Row.Cells(0), iFgFieldCell).ReadOnly = True
                Else
                    chkAll.Attributes.Add("onclick", sbrClientFunction.ToString)
                    CType(e.Row.Cells(1), iFgFieldCell).Attributes("onclick") += sbrClientFunction.ToString
                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Sub
#End Region

#Region "bindSchema"
    Private Sub bindSchema(ByRef ifg As iFlexGrid, ByRef dt As DataTable)
        Try
            Dim I As Integer
            I = 0
            ifg.AutoGenerateColumns = False
            ifg.Columns.Clear()

            For Each dc As DataColumn In dt.Columns
                If dc.ColumnName = "rowindex" Then
                    Continue For
                End If
                If dc.DataType Is GetType(System.DateTime) Then
                    Dim dfield As New DateField
                    dfield.DataField = dc.ColumnName
                    dfield.SortExpression = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.DataFormatString = "{0:dd-MMM-yyyy}"
                    dfield.HtmlEncode = False
                    dfield.iDate.DateIcon.CssClass = "dimg"
                    dfield.iDate.DateIcon.Src = iInterchange.WebControls.v4.Utilities.iUtil.AppPath & iInterchange.WebControls.v4.Utilities.iUtil.ImagesFolder & "calendar.png"
                    dfield.iDate.CssClass = "lkp"
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = False
                    dfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left
                    ifg.Columns.Add(dfield)
                    'dc.DefaultValue = "       "
                    'dfield.ItemStyle.Width = Unit.Pixel(100)
                ElseIf dc.DataType Is GetType(System.Int32) Then
                    Dim dfield As New TextboxField
                    dfield.DataField = dc.ColumnName
                    dfield.SortExpression = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.TextBox.CssClass = "lkp"
                    dfield.TextBox.iCase = TextCase.Numeric
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = False
                    dfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left
                    ifg.Columns.Add(dfield)
                ElseIf dc.DataType Is GetType(Boolean) Then
                    Dim dfield As New iInterchange.WebControls.v4.Data.CheckBoxField
                    dfield.DataField = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = False
                    dfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                    dfield.HeaderImageUrl = "../Images/flrsel.gif"
                    dfield.ItemStyle.Width = 50
                    ifg.Columns.Add(dfield)
                Else
                    Dim dfield As New iInterchange.WebControls.v4.Data.TextboxField
                    dfield.DataField = dc.ColumnName
                    dfield.SortExpression = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.TextBox.CssClass = "lkp"
                    'dfield.ItemStyle.Width = Unit.Pixel(75)
                    dfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left
                    ifg.Columns.Add(dfield)
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = False
                End If

                ifg.Columns.Item(I).AllowSearch = True
                ifg.Columns.Item(I).IsEditable = False
                If I = 0 Then ' ID column - hidden
                    ifg.Columns.Item(I).Visible = False
                ElseIf I = 1 Then ' checkbox column
                    ifg.Columns.Item(I).IsEditable = True
                    ifg.Columns.Item(I).AllowSearch = False
                End If
                I = I + 1
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Sub
#End Region

#Region "pvt_SetSimpleParam"
    Private Sub pvt_SetSimpleParam(ByVal strparam As String, ByVal strparamvalue As String)
        Try
            Dim dt As DataTable
            dt = CType(RetrieveData("reportparamlist" + GetQueryString("ifgActivityId")), DataTable)

            Dim foundRow As DataRow = dt.Rows.Find(strparam)

            If Not (foundRow Is Nothing) Then
                foundRow("parametervalue") = strparamvalue.Replace("""", "").Replace("'", "")
            End If

            CacheData("reportparamlist" + GetQueryString("ifgActivityId"), dt)

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            pub_SetCallbackReturnValue("Message", "Parameter Update Fails.")
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Sub
#End Region

#Region "pvt_ReSetParam"
    Private Sub pvt_ReSetParam()
        Try
            Dim dsParameterMasters As DataSet
            Dim dtParameterLists As DataTable

            dsParameterMasters = RetrieveData("reportds" + GetQueryString("ifgActivityId"))
            dtParameterLists = RetrieveData("reportparamlist" + GetQueryString("ifgActivityId"))

            dsParameterMasters.RejectChanges()
            dtParameterLists.RejectChanges()

            CacheData("reportds" + GetQueryString("ifgActivityId"), dsParameterMasters)
            CacheData("reportparamlist" + GetQueryString("ifgActivityId"), dtParameterLists)

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            pub_SetCallbackReturnValue("Message", "Parameter Update Fails.")
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "ifgParameterList_RowUpdating"
    Protected Sub ifgParameterList_RowUpdating(ByVal sender As Object, ByVal e As iFlexGridUpdateEventArgs) Handles ifgParameterList.RowUpdating
        Try
            Dim strParam As String
            strParam = e.OldValues("parameter")
            Select Case e.OldValues("type").ToString
                Case ""
                Case "master"
                    Dim dsParameterMasters As DataSet
                    dsParameterMasters = RetrieveData("reportds" + GetQueryString("ifgActivityId"))

                    For Each dr As DataRow In dsParameterMasters.Tables(strParam).Rows
                        If CBool(e.NewValues("Checked")) = True Then
                            dr("Select") = True
                        Else
                            dr("Select") = False
                        End If
                    Next
            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "GetSelectedRow"
    Private Function GetSelectedRow(ByVal intActivityID As Integer) As Integer
        Dim table As DataTable
        'Dim strFilter As String
        Dim dr As DataRow
        Dim dv As DataView
        Dim itemno As Integer
        Try
            itemno = Request.QueryString("itemno")
            table = RetrieveData("listdata" + CStr(intActivityID))
            'dr = table.Select(strFilter)
            dv = table.DefaultView
            dr = dv.ToTable.Rows(itemno)
            If dr.ItemArray.Length > 0 Then
                GetSelectedRow = CInt(dr.Item(ReportParamData.RPRT_HDR_ID))
                'txtReportTitle.Text = CStr(dr.Item("TITLE"))
                CacheData("ReportHeader" + CStr(intActivityID), CStr(dr.Item("TITLE")))
            End If
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetReportDatasetQuery"
    Private Function GetReportDatasetQuery(ByVal reportPath As String, ByVal bv_strDatasetName As String) As String
        Dim reportDefinition As Byte() = Nothing
        Dim xmldoc As XmlDocument = Nothing
        Dim objXMLNSManager As XmlNamespaceManager
        Dim objTempXMlDoc As New XmlDocument
        Dim objReportingServices As Object = Nothing
        Try
            If RSVersion() = "2010" Then
                objReportingServices = New RS2010.ReportingService2010()
                objReportingServices.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("ReportServerUID"), ConfigurationManager.AppSettings("ReportServerPWD"), ConfigurationManager.AppSettings("ReportServerDomain"))
                reportDefinition = objReportingServices.GetItemDefinition(reportPath)
            ElseIf RSVersion() = "2005" Then
                objReportingServices = New RS2005.ReportingService2005(Config.pub_GetAppConfigValue("ReportServicePath"))
                objReportingServices.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("ReportServerUID"), ConfigurationManager.AppSettings("ReportServerPWD"), ConfigurationManager.AppSettings("ReportServerDomain"))
                reportDefinition = objReportingServices.GetReportDefinition(reportPath)
            End If

            Using rdlFile As New MemoryStream(reportDefinition)
                xmldoc = New XmlDocument()
                xmldoc.Load(rdlFile)
                rdlFile.Close()
                objXMLNSManager = New XmlNamespaceManager(xmldoc.NameTable)
            End Using

            Dim root As XmlNode = xmldoc.DocumentElement
            ' Get all the elements under the root node.
            Dim nodelist As XmlNodeList = root.SelectNodes("*")
            '   Dim dsdataset As 
            For Each node As XmlNode In nodelist
                If node.Name = "DataSets" Then
                    ' Only search DataSets.
                    Dim childList As XmlNodeList = node.ChildNodes

                    For Each childnode As XmlNode In childList
                        If childnode.Name = "DataSet" Then

                            If childnode.Attributes("Name").Value = bv_strDatasetName Then
                                objTempXMlDoc.LoadXml(childnode.OuterXml)
                                objTempXMlDoc.GetElementsByTagName("CommandText")
                                Return objTempXMlDoc.GetElementsByTagName("CommandText")(0).InnerText

                            End If

                        End If
                    Next
                End If
            Next
            Return ""

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/jquery.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/DynamicReport/ParameterSelect.js", MyBase.Page)
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

#Region "ifgParameterList_ClientBind"
    Protected Sub ifgParameterList_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgParameterList.ClientBind
        Try
            Dim dt As DataTable
            dt = CType(RetrieveData("reportparamlist" + GetQueryString("ifgActivityId")), DataTable)

            If Not dt Is Nothing Then
                e.DataSource = dt
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.ToString, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message.ToString)
        End Try
    End Sub
#End Region

#Region "Page_LoadComplete1"

    Protected Sub Page_LoadComplete1(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ClientScript.RegisterStartupScript(GetType(System.String), "GetReportTitle", "GetReportTitle('" & GetQueryString("mode") & "','" & RetrieveData("ReportHeader" + GetQueryString("activityid")) & "');", True)

    End Sub
#End Region

#Region "RSVersion"
    Private Function RSVersion() As String
        If Config.pub_GetAppConfigValue("ReportServicePath") IsNot Nothing AndAlso Config.pub_GetAppConfigValue("ReportServicePath").Contains("2005") Then
            Return "2005"
        ElseIf Config.pub_GetAppConfigValue("ReportServicePath") IsNot Nothing AndAlso Config.pub_GetAppConfigValue("ReportServicePath").Contains("2010") Then
            Return "2010"
        End If
    End Function
#End Region

#Region "ifgParameter_RowDataBound"
    Protected Sub ifgParameter_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgParameter.RowDataBound
        If e.Row.Cells.Count > 4 Then
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(4).Visible = False
            End If
            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then
                Dim drw As DataRowView = CType(e.Row.DataItem, DataRowView)
                CType(e.Row.Cells(4), iFgFieldCell).Visible = False
            End If
        End If
    End Sub
#End Region

End Class
