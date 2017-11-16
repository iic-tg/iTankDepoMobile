Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Services.Protocols
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Configuration
Imports System.Net
Imports iInterchange.Framework.RS2005
Imports System.Xml
Imports System.IO


Partial Class DynamicReports_AddDynamicReport
    Inherits Pagebase

#Region "Declaration"

    Private strMSGUPDATE As String = "Record Updated Successfully."
    Private strMSGINSERT As String = "Record Inserted Successfully."
    Private Const ADDREPORT As String = "AddReport"
    Dim dsAddDynamicReport As AddDynamicReportDataSet

#End Region

#Region "ifgAddReAddReport_ClientBind"
    'Protected Sub ifgAddReport_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgAddReport.ClientBind
    '    Try
    '        'initialization
    '        Dim objAddReport As New AddDynamicReport
    '        'storing the data in grid session
    '        ifgAddReport.UseCachedDataSource = True
    '        'get the data from database
    '        Dim bv_i32ProcessId = objAddReport.GetProcessIDByProcessName("Reports")
    '        Dim strType = "DynamicReport/DynamicReportParameter.aspx"
    '        dsAddDynamicReport = objAddReport.GetActivityByProcessId(bv_i32ProcessId, strType)
    '        'bind the grid from dataset
    '        e.DataSource = dsAddDynamicReport.Tables(AddDynamicReportData._ACTIVITY)

    '        dsAddDynamicReport.Tables(AddDynamicReportData._ACTIVITY).AcceptChanges()
    '        CacheData(ADDREPORT, dsAddDynamicReport)
    '    Catch ex As Exception
    '        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
    '    End Try
    'End Sub
#End Region

#Region "Page_Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            pvt_SetChangesMade()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "fnGetData"
                pvt_GetData(e.GetCallbackValue("mode"))
            Case "validateAddReportName"
                pvt_ValidateAddReportName(e.GetCallbackValue("ReportName"), _
                                    e.GetCallbackValue("ProcessId"), _
                                    e.GetCallbackValue("AddProcessFlag"))
            Case "validateProcess"
                pvt_ValidateProcess(e.GetCallbackValue("PRCSS_NAM"))
            Case "valProcessbyRptName"
                pvt_ValidateProcessbyRptName(e.GetCallbackValue("PRCSS_ID"), e.GetCallbackValue("ACTVTY_NAM"))
            Case "createReport"
                pvt_CreateReport(e.GetCallbackValue("ReportName"), _
                                    e.GetCallbackValue("ProcessName"), _
                                    e.GetCallbackValue("ReportTitle"), _
                                    e.GetCallbackValue("AddProcessFlag"), _
                                    e.GetCallbackValue("Active"))
            Case "updateReport"
                pvt_UpdateAddReport(e.GetCallbackValue("ActivityId"), _
                                    e.GetCallbackValue("ProcessName"), _
                                    e.GetCallbackValue("ReportName"), _
                                     e.GetCallbackValue("ReportTitle"), _
                                    e.GetCallbackValue("AddProcessFlag"), _
                                     e.GetCallbackValue("OrderFlag"), _
                                    e.GetCallbackValue("Active"), _
                                    e.GetCallbackValue("FetchParameter"))
        End Select
    End Sub
#End Region

#Region "SetChangesMade"
    ''' <summary>
    ''' This method is to set changes to all the fields
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pvt_SetChangesMade()
        pub_SetGridChanges(ifgParameters, "ITab1_0")
        CommonWeb.pub_AttachHasChanges(txtProcess)
        CommonWeb.pub_AttachHasChanges(txtReportName)
        CommonWeb.pub_AttachHasChanges(chkActiveBit)
        CommonWeb.pub_AttachHasChanges(txtReportTitle)
        CommonWeb.pub_AttachHasChanges(lkpProcess)
    End Sub
#End Region

#Region "pvt_GetData()"

    Private Sub pvt_GetData(ByVal bv_strMode As String)
        Try
            Dim sbDynamicReport As New StringBuilder
            If bv_strMode = MODE_EDIT Then
                sbDynamicReport.Append(CommonWeb.GetTextValuesJSO(txtReportName, PageSubmitPane.pub_GetPageAttribute("REPORT NAME")))
                sbDynamicReport.Append(CommonWeb.GetTextValuesJSO(txtReportTitle, PageSubmitPane.pub_GetPageAttribute("REPORT TITLE")))
                sbDynamicReport.Append(CommonWeb.GetCheckboxValuesJSO(chkActiveBit, CBool(PageSubmitPane.pub_GetPageAttribute("ACTIVE"))))
                If PageSubmitPane.pub_GetPageAttribute("PROCESS NAME") <> "" And PageSubmitPane.pub_GetPageAttribute(AddDynamicReportData.PRCSS_ID) <> "" Then
                    sbDynamicReport.Append(CommonWeb.GetLookupValuesJSO(lkpProcess, PageSubmitPane.pub_GetPageAttribute(AddDynamicReportData.PRCSS_ID), PageSubmitPane.pub_GetPageAttribute("PROCESS NAME")))
                Else
                    sbDynamicReport.Append(CommonWeb.GetLookupValuesJSO(lkpProcess, "", ""))
                End If
                sbDynamicReport.Append(String.Concat("setPageID('", PageSubmitPane.pub_GetPageAttribute("ID"), "');"))
            End If
            pub_SetCallbackReturnValue("Message", sbDynamicReport.ToString)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateAddReportCode"
    Private Sub pvt_ValidateAddReportName(ByVal bv_strReportName As String, ByVal bv_strProcessId As String, ByVal bv_strAddProcessFlag As String)
        Try
            Dim objAddReport As New AddDynamicReport
            'Dim i32ProcessId As Int32 = objAddReport.pub_GetProcessIDByProcessName("Custom Reports")
            Dim arr_Reports As Array = GetReports()

            Dim blnValid As Boolean = True

            If bv_strProcessId <> String.Empty AndAlso bv_strAddProcessFlag = False Then
                If CBool(bv_strAddProcessFlag) Then
                    blnValid = objAddReport.pub_ValidatePKAddDynamicReport(bv_strReportName, CInt(bv_strProcessId))
                End If
            End If


            If IsNothing(arr_Reports) Then
                pub_SetCallbackStatus(True)
                pub_SetCallbackReturnValue("pkValid", "false")
                pub_SetCallbackReturnValue("Message", "Report does not exist in the server.")
                Exit Sub
            End If

            If blnValid = True Then
                For Each Item As Report In arr_Reports
                    If Item.Name.ToUpper() = bv_strReportName.ToUpper() Then
                        pub_SetCallbackReturnValue("pkValid", "true")
                        blnValid = True
                        Exit For
                    Else
                        blnValid = False
                    End If
                Next
                If Not blnValid Then
                    pub_SetCallbackReturnValue("pkValid", "false")
                    pub_SetCallbackReturnValue("Message", "Report does not exist in the server.")
                End If
            Else
                pub_SetCallbackReturnValue("pkValid", "false")
                pub_SetCallbackReturnValue("Message", "Report Name already exists.")
            End If
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_ValidateProcess"
    Private Sub pvt_ValidateProcess(ByVal bv_strProcess As String)
        Try
            Dim objAddReport As New AddDynamicReport

            Dim i32ProcessId As Int32 = objAddReport.pub_GetProcessIDByProcessName(bv_strProcess)

            If i32ProcessId > 0 Then
                pub_SetCallbackReturnValue("pkValid", "false")
                pub_SetCallbackReturnValue("Message", "Process Name Already Exists.")
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

#Region "pvt_ValidateProcessbyRptName"
    Private Sub pvt_ValidateProcessbyRptName(ByVal bv_strProcess As String, ByVal bv_strActivityName As String)
        Try
            Dim objAddReport As New AddDynamicReport
            Dim blnResult As Boolean = False
            If bv_strActivityName <> "" Then
                blnResult = objAddReport.Pub_GetProcessIDByProcessIDActivityName(bv_strProcess, bv_strActivityName)
            End If

            If blnResult = True Then
                pub_SetCallbackReturnValue("pkValid", "false")
                pub_SetCallbackReturnValue("Message", "Report Name Already Exists for " & bv_strProcess & " process.")
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

#Region "pvt_CreateReport"
    Private Sub pvt_CreateReport(ByVal strReportName As String, _
                                    ByVal strProcessName As String, _
                                    ByVal strReportTitle As String, _
                                    ByVal strAddProcessFlag As String, _
                                    ByVal strActiveBit As String)
        Try
            Dim objAddReport As New AddDynamicReport
            Dim i32ParentProcess As Int32 = objAddReport.pub_GetProcessIDByProcessName("Custom Reports")

            Dim intActivityId As Integer
            Dim intProcessId As Integer
            objAddReport.pub_CreateReport(strProcessName, i32ParentProcess, intActivityId, _
                                             strReportName, "List.aspx", "Reports >> " & strReportName, _
                                             3, "DynamicReport/DynamicReportParameter.aspx", _
                                             strReportName, strReportName, True, CBool(strAddProcessFlag), intProcessId)
            pub_SetCallbackReturnValue("ID", intActivityId)
            pub_SetCallbackReturnValue("ProcessID", intProcessId)
            pub_SetCallbackReturnValue("Message", strMSGINSERT)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "pvt_UpdateAddReport"
    Private Sub pvt_UpdateAddReport(ByVal i32ActivityId As Integer, ByVal strProcessName As String, _
                                    ByVal strReportName As String, _
                                    ByVal strReportTitle As String, _
                                    ByVal strAddProcessFlag As String, _
                                    ByVal strOrderFlag As String, _
                                    ByVal strActiveBit As String, _
                                    ByVal strFetchparameters As String)
        Try
            Dim objAddReport As New AddDynamicReport
            Dim i32ParentProcess As Int32 = objAddReport.pub_GetProcessIDByProcessName("Custom Reports")

            Dim dtParameter As New DataTable
            dtParameter = CType(RetrieveData(ADDREPORT), DataTable)

            objAddReport.pub_UpdateReport(strProcessName, i32ParentProcess, i32ActivityId, strReportName, "List.aspx", "DynamicReport/DynamicReportParameter.aspx", _
                                          strReportTitle, strReportName, _
                                          strActiveBit, strAddProcessFlag, strOrderFlag, strFetchparameters, dtParameter)
            pub_SetCallbackReturnValue("Message", strMSGUPDATE)
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "ifgParameters_ClientBind"
    Protected Sub ifgParameters_ClientBind(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgParameters.ClientBind
        Try
            Dim objAddReport As New AddDynamicReport
            Dim strReportID As String = e.Parameters("ReportID").ToString()
            Dim strReportName As String = e.Parameters("ReportName").ToString()
            'Server Report Parameter

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
            Dim strReportPath As String = String.Concat(ConfigurationManager.AppSettings("ReportFolderpath"), strReportName)
            Dim forRendering As Boolean = False
            Dim historyID As String = Nothing
            Dim parameters As Object = Nothing

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

            Dim dtParameterLists As DataTable
            dsAddDynamicReport = New AddDynamicReportDataSet
            dtParameterLists = dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER).Clone()


            If parameters.Length = 0 Then
                Exit Sub
            End If
            Dim strParamQuery As String

            If RSVersion() = "2010" Then
                For Each Parm As RS2010.ItemParameter In parameters
                    If Parm.PromptUser And Parm.Prompt <> String.Empty Then
                        Dim drParamList As DataRow = dtParameterLists.NewRow
                        drParamList(AddDynamicReportData.PRMTR_NAM) = Parm.Name
                        drParamList(AddDynamicReportData.PRMTR_DSPLY_NAM) = Parm.Prompt
                        drParamList(AddDynamicReportData.RPRT_ID) = strReportID
                        If Parm.ParameterTypeName = "String" And Parm.Name = "reporttitle" Then
                            drParamList(AddDynamicReportData.PRMTR_TYP) = "title"
                        ElseIf (Parm.ParameterTypeName = "String" Or Parm.ParameterTypeName = "Integer") And Parm.MultiValue Then
                            drParamList(AddDynamicReportData.PRMTR_TYP) = "master"
                        ElseIf Parm.ParameterTypeName = "String" Then
                            drParamList(AddDynamicReportData.PRMTR_TYP) = "string"
                        ElseIf Parm.ParameterTypeName = "Integer" Then
                            drParamList(AddDynamicReportData.PRMTR_TYP) = "integer"
                        ElseIf Parm.ParameterTypeName = "DateTime" Then
                            drParamList(AddDynamicReportData.PRMTR_TYP) = "date"
                        End If

                        strParamQuery = GetReportDatasetQuery(strReportPath, Parm.Name)
                        drParamList(AddDynamicReportData.PRMTR_QRY) = strParamQuery

                        dtParameterLists.Rows.Add(drParamList)
                    End If
                Next
            ElseIf RSVersion() = "2005" Then

                For Each Parm As RS2005.ReportParameter In parameters
                    If Parm.PromptUser And Parm.Prompt <> String.Empty Then
                        Dim drParamList As DataRow = dtParameterLists.NewRow
                        drParamList(AddDynamicReportData.PRMTR_NAM) = Parm.Name
                        drParamList(AddDynamicReportData.PRMTR_DSPLY_NAM) = Parm.Prompt
                        drParamList(AddDynamicReportData.RPRT_ID) = strReportID
                        If Parm.Type = RS2005.ParameterTypeEnum.String And Parm.Name = "reporttitle" Then
                            drParamList(AddDynamicReportData.PRMTR_TYP) = "title"
                        ElseIf (Parm.Type = RS2005.ParameterTypeEnum.String Or Parm.Type = RS2005.ParameterTypeEnum.Integer) And Parm.MultiValue Then
                            drParamList(AddDynamicReportData.PRMTR_TYP) = "master"
                        ElseIf Parm.Type = RS2005.ParameterTypeEnum.String Then
                            drParamList(AddDynamicReportData.PRMTR_TYP) = "string"

                        ElseIf Parm.Type = RS2005.ParameterTypeEnum.Integer Then
                            drParamList(AddDynamicReportData.PRMTR_TYP) = "integer"
                        ElseIf Parm.Type = RS2005.ParameterTypeEnum.DateTime Then
                            drParamList(AddDynamicReportData.PRMTR_TYP) = "date"
                        End If

                        strParamQuery = GetReportDatasetQuery(strReportPath, Parm.Name)
                        drParamList(AddDynamicReportData.PRMTR_QRY) = strParamQuery

                        dtParameterLists.Rows.Add(drParamList)
                    End If
                Next
            End If

            dtParameterLists.AcceptChanges()

            dsAddDynamicReport = objAddReport.pub_GetReportParameters(dtParameterLists, strReportName)
            e.DataSource = dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER)
            CacheData(ADDREPORT, dsAddDynamicReport.Tables(AddDynamicReportData._REPORT_PARAMETER))
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgParameters_RowUpdating"
    Protected Sub ifgParameters_RowUpdating(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridUpdateEventArgs) Handles ifgParameters.RowUpdating
        e.NewValues(AddDynamicReportData.PRMTR_ID) = e.OldValues(AddDynamicReportData.PRMTR_ID)
    End Sub
#End Region

#Region "ifgParameters_RowDataBound"
    Protected Sub ifgParameters_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgParameters.RowDataBound
        Try
            Dim chkDropdown As iInterchange.WebControls.v4.Data.iFgCheckBox
            Dim strDropdownFlag As String = "true"

            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then

                Dim drv As System.Data.DataRowView
                drv = CType(e.Row.DataItem, Data.DataRowView)

                chkDropdown = CType(e.Row.Cells(3).Controls(0), iInterchange.WebControls.v4.Data.iFgCheckBox)
                If chkDropdown.Checked = False Then
                    CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    strDropdownFlag = "false"
                End If
               

                If Not drv.Row.Item(AddDynamicReportData.PRMTR_QRY).ToString().ToLower().Contains("dependent") Then
                    CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    strDropdownFlag = "false"
                End If

                If drv.Row.Item(AddDynamicReportData.PRMTR_TYP) <> "master" Then
                    chkDropdown.Enabled = False
                    CType(e.Row.Cells(4), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                End If
                Dim sbrClientFunction As New StringBuilder
                sbrClientFunction.Append("onParameterClick('")
                sbrClientFunction.Append(e.Row.RowIndex)
                sbrClientFunction.Append("',")
                sbrClientFunction.Append("this,'")
                sbrClientFunction.Append(strDropdownFlag)
                sbrClientFunction.Append("');")

                chkDropdown.Attributes.Add("onclick", sbrClientFunction.ToString)
                chkDropdown.Style.Add("cursor", "hand")
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgAddReAddReport_RowDataBound"
    'Protected Sub ifgAddReAddReport_RowDataBound(sender As Object, e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgAddReport.RowDataBound
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow AndAlso Not e.Row.DataItem Is Nothing Then
    '            Dim drv As System.Data.DataRowView
    '            drv = CType(e.Row.DataItem, Data.DataRowView)
    '            If drv.Row.RowState = DataRowState.Unchanged Then
    '                CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
    '    End Try
    'End Sub
#End Region

#Region "ifgAddReAddReport_RowDeleting"
    'Protected Sub ifgAddReport_RowDeleting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgAddReport.RowDeleting
    '    Try
    '        Dim intRowIndex As Integer = 0
    '        intRowIndex = ifgAddReport.PageSize * ifgAddReport.PageIndex + e.RowIndex
    '        If CType(ifgAddReport.DataSource, DataTable).Select(AddDynamicReportData.ACTVTY_ID & "=" & e.Keys(0))(0).RowState <> DataRowState.Added Then
    '            e.Cancel = True
    '            e.OutputParamters("Delete") = "Dynamic Report cannot be deleted"
    '        End If
    '    Catch ex As Exception
    '        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
    '    End Try
    'End Sub
#End Region

#Region "ifgAddReAddReport_RowInserting"
    'Protected Sub ifgAddReAddReport_RowInserting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgAddReport.RowInserting
    '    Try
    '        dsAddDynamicReport = CType(RetrieveData(ADDREPORT), AddDynamicReportDataSet)
    '        Dim lngID As Long
    '        lngID = CommonWeb.GetNextIndex(dsAddDynamicReport.Tables(AddDynamicReportData._ACTIVITY), AddDynamicReportData.ACTVTY_ID)
    '        e.Values(AddDynamicReportData.ACTVTY_ID) = lngID
    '    Catch ex As Exception
    '        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
    '    End Try

    'End Sub

#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/DynamicReport/AddDynamicReport.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Callback.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
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

#Region "Get Reports List from Server"
    Public Function GetReports() As Report()
        Try
            Dim reportingService As Object = Nothing
            If RSVersion() = "2010" Then
                reportingService = New RS2010.ReportingService2010()
            ElseIf RSVersion() = "2005" Then
                reportingService = New RS2005.ReportingService2005(Config.pub_GetAppConfigValue("ReportServicePath"))
            End If
            reportingService.Url = pvt_Getreportservice()

            Dim strUserName As String = ConfigurationManager.AppSettings("ReportServerUID")
            Dim strPassword As String = ConfigurationManager.AppSettings("ReportServerPWD")

            Dim objcredentials = New NetworkCredential(strUserName, strPassword, "")

            reportingService.Credentials = objcredentials

            Dim reports As IList(Of Report) = New List(Of Report)()
            If RSVersion() = "2010" Then
                Dim catalogItems As RS2010.CatalogItem() = reportingService.ListChildren("/", True)

                For Each item As RS2010.CatalogItem In catalogItems
                    If item.TypeName = "Report" Then
                        Dim report As New Report()
                        report.Name = item.Name
                        report.Path = item.Path
                        report.Description = item.Description
                        report.UrlAccess = [String].Concat(pvt_Getreportserver, "?", item.Path)
                        reports.Add(report)
                    End If
                Next

            ElseIf RSVersion() = "2005" Then
                Dim catalogItems As RS2005.CatalogItem() = reportingService.ListChildren("/", True)

                For Each item As RS2005.CatalogItem In catalogItems
                    If CType(item.Type, ItemTypeEnum).ToString = "Report" Then
                        Dim report As New Report()
                        report.Name = item.Name
                        report.Path = item.Path
                        report.Description = item.Description
                        report.UrlAccess = [String].Concat(pvt_Getreportserver, "?", item.Path)
                        reports.Add(report)
                    End If
                Next
            End If
            Return reports.ToArray()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog("ReportRepository", Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
        Return Nothing
    End Function
#End Region

#Region "pvt_Getreportserver"
    Private Function pvt_Getreportserver() As String
        Dim strUri As New System.UriBuilder(ConfigurationManager.AppSettings("ReportServerPath"))
        Return strUri.Uri.ToString()
    End Function
#End Region

#Region "pvt_Getreportservice"
    Private Function pvt_Getreportservice() As String
        Dim strUri As New System.UriBuilder(ConfigurationManager.AppSettings("ReportServicePath"))
        Return strUri.Uri.ToString()
    End Function
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

#Region "Report "
    Public Class Report
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(ByVal value As String)
                m_Name = value
            End Set
        End Property
        Private m_Name As String

        Public Property Path() As String
            Get
                Return m_Path
            End Get
            Set(ByVal value As String)
                m_Path = value
            End Set
        End Property
        Private m_Path As String

        Public Property UrlAccess() As String
            Get
                Return m_UrlAccess
            End Get
            Set(ByVal value As String)
                m_UrlAccess = value
            End Set
        End Property
        Private m_UrlAccess As String

        Public Property Description() As String
            Get
                Return m_Description
            End Get
            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property
        Private m_Description As String
    End Class
#End Region

#Region ""

#End Region
End Class