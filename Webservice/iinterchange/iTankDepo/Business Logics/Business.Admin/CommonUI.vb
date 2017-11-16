Option Strict On
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.ServiceModel
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Text

<ServiceContract()> _
Public Class CommonUI

#Region "Declarations.."
    Private Const RepairApprovalPendingQuery As String = " AND EQPMNT_NO IN (SELECT EQPMNT_NO FROM BULK_EMAIL_DETAIL WHERE ACTVTY_NO = RPR_ESTMT_ID)   ORDER BY RPR_ESTMT_ID DESC"
    Private Const RepairApprovalPendingFlowQuery As String = " ORDER BY RPR_ESTMT_ID DESC"
    Private Const RepairEstimatePendingQuery As String = "  AND EQPMNT_STTS_ID =(SELECT EQPMNT_STTS_ID FROM ACTIVITY_STATUS WHERE EQPMNT_NO =VAS.EQPMNT_NO AND GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO)  ORDER BY GTN_DT"
    Private Const RepairEstimatePendingFlowQuery As String = "  ORDER BY GTN_DT"

#End Region

#Region "pub_GetActivityByActivityID()"
    <OperationContract()> _
    Public Function pub_GetActivityByActivityID(ByVal bv_intActivityID As Int32) As CommonUIDataSet
        Dim dsCommon As CommonUIDataSet
        Dim objCommons As New CommonUIs
        Try
            dsCommon = objCommons.GetActivityByActivityID(bv_intActivityID)
            Return dsCommon
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "ValidateLicense"
    <OperationContract()> _
    Public Function ValidateLicense(ByVal bv_i32DepotID As Integer) As Boolean
        Try
            Dim strFileName As String = String.Concat(AppDomain.CurrentDomain.BaseDirectory.ToString, "\SerialNumber.txt")
            Dim strSerialNumber As String = ""
            If System.IO.File.Exists(strFileName) = True Then
                Dim objReader As New System.IO.StreamReader(strFileName)
                strSerialNumber = objReader.ReadToEnd()
                objReader.Close()
                strSerialNumber = Trim(strSerialNumber)
                If Not strSerialNumber = "" Then
                    Dim dsConfigData As ConfigDataSet
                    Dim strActivationCode As String = ""
                    Dim objConfigs As New CommonUI
                    dsConfigData = objConfigs.pub_GetConfigByKeyName("001", bv_i32DepotID)
                    If Not dsConfigData.Tables(CommonUIData._CONFIG).Rows.Count > 0 Then
                        Return False
                    Else
                        strActivationCode = DecryptString(dsConfigData.Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL).ToString)
                        If String.Compare(strActivationCode, strSerialNumber) = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_GetPendingQuery()"
    'REPAIR FLOW SETTING: 037
    'CR-001 (AWE_TO_AWP)
    <OperationContract()> _
    Private Function pvt_GetQuery(ByRef br_strQuery As String, ByVal bv_intActivityId As Integer) As String
        Try
            If bv_intActivityId = 85 OrElse bv_intActivityId = 170 Then ' 85- Repair Estimate
                br_strQuery = String.Concat(br_strQuery, RepairEstimatePendingQuery)
            ElseIf bv_intActivityId = 89 OrElse bv_intActivityId = 171 Then '89- Repair Approval
                br_strQuery = String.Concat(br_strQuery, RepairApprovalPendingQuery)
            End If
            Return br_strQuery
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetListRecords"
    <OperationContract()> _
    Public Function GetListRecords(ByVal bv_dsCommon As CommonUIDataSet, ByVal bv_strWFdata As String, ByVal bv_intActivityID As Int64) As DataSet
        Try
            With bv_dsCommon.Tables(CommonUIData._ACTIVITY).Rows(0)
                Dim strQuery As String = ""
                Dim strPageTitle As String = ""
                Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFdata, CommonUIData.DPT_ID))
                If Not .Item(CommonUIData.ACTVTY_RL).ToString = "" And Not .Item(CommonUIData.ACTVTY_RL).ToString = String.Empty Then
                    Dim strItems() As String = .Item(CommonUIData.ACTVTY_RL).ToString.Split(CChar(","))
                    For i As Integer = 0 To strItems.Length - 1
                        If strItems(i).Substring(strItems(i).IndexOf("<") + 1, strItems(i).IndexOf(":", strItems(i).IndexOf("<")) - strItems(i).IndexOf("<") - 1) = "L" _
                            OrElse strItems(i).Substring(strItems(i).IndexOf("<") + 1, strItems(i).IndexOf(":", strItems(i).IndexOf("<")) - strItems(i).IndexOf("<") - 1) = "M" Then
                            Dim result As String = strItems(i).Substring(strItems(i).IndexOf(":") + 1, strItems(i).IndexOf(">", strItems(i).IndexOf(":")) - strItems(i).IndexOf(":") - 1)
                            Dim bln_KeyExist As Boolean
                            Dim strKeyValue As String = GetSettings(result, intDepotID, bln_KeyExist)
                            If bln_KeyExist Then
                                If CStr(.Item(CommonUIData.LST_QRY)).Contains(strItems(i)) OrElse strQuery = "" Then
                                    If strQuery = "" Then
                                        strQuery = CStr(.Item(CommonUIData.LST_QRY))
                                        strQuery.Replace(strItems(i), strKeyValue)
                                    Else
                                        strQuery = strQuery.Replace(strItems(i), strKeyValue)
                                    End If
                                End If
                            Else
                                strQuery.Replace(strItems(i), "Lessee Code")
                            End If
                        End If
                    Next
                Else
                    strQuery = CStr(.Item(CommonUIData.LST_QRY))
                End If
                'Multilocation handling
                strPageTitle = CStr(.Item(CommonUIData.LST_TTL))
                ' Dim strActivityId As String = CommonUIData.ACTVTY_ID
                Dim strKeyword As String() = {"ORDER BY"}
                Dim strParts As String() = strQuery.Split(strKeyword, StringSplitOptions.None)
                Dim strOrderBy As String = ""
                If strParts.Length > 1 Then
                    strQuery = strParts(0)
                    strOrderBy = String.Concat(" ORDER BY ", strParts(1))
                End If

                'Validate Exclude Activity for depot

                Dim dsConfigData As ConfigDataSet
                Dim strActivityID As String = ""
                'Dim objConfigs As New CommonUIs
                dsConfigData = pub_GetConfigByKeyName("036", intDepotID)
                If Not dsConfigData.Tables(CommonUIData._CONFIG).Rows.Count > 0 Then
                    strActivityID = ""
                Else
                    If Not IsDBNull(dsConfigData.Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL)) AndAlso Not dsConfigData.Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL).ToString() = "" Then
                        strActivityID = DecryptString(dsConfigData.Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL).ToString)
                    End If

                End If

                If strPageTitle.ToLower.Contains("master") And GetMultiLocationSupportConfig() = "True" Then
                    'If bv_intActivityID = 99 Then
                    '    If strQuery.IndexOf("WHERE") > 0 Then
                    '        strQuery = String.Concat(strQuery, " AND ", CommonUIData.DPT_ID, "=", intDepotID, strOrderBy)
                    '    Else
                    '        strQuery = String.Concat(strQuery, " WHERE ", CommonUIData.DPT_ID, "=", intDepotID, strOrderBy)
                    '    End If
                    'Else
                    strQuery = String.Concat(strQuery, " ", strOrderBy)
                    'End If
                Else

                    If Not (bv_intActivityID = 26 OrElse bv_intActivityID = 151) Then
                        Dim blnKeyExist As Boolean = False
                        If strQuery.IndexOf("WHERE") > 0 Then
                            If (CInt(GetHeadQuarterID()) = intDepotID And GetMultiLocationSupportConfig() = "True" And Not (bv_intActivityID = 92 OrElse bv_intActivityID = 84 OrElse bv_intActivityID = 85 OrElse bv_intActivityID = 89 OrElse bv_intActivityID = 148 OrElse bv_intActivityID = 170 OrElse bv_intActivityID = 171)) Then
                                strQuery = String.Concat(strQuery, " ", strOrderBy)
                            Else
                                If strActivityID = "" OrElse (CInt(.Item(CommonUIData.ACTVTY_ID)) <> CInt(strActivityID)) Then
                                    strQuery = String.Concat(strQuery, " AND ", CommonUIData.DPT_ID, "=", intDepotID, strOrderBy)
                                Else
                                    strQuery = String.Concat(strQuery, " ", strOrderBy)
                                End If
                            End If

                        Else
                            If (CInt(GetHeadQuarterID()) = intDepotID And GetMultiLocationSupportConfig() = "True" And Not (bv_intActivityID = 102)) Then
                                strQuery = String.Concat(strQuery, " ", strOrderBy)
                            Else
                                If strActivityID = "" OrElse (CInt(.Item(CommonUIData.ACTVTY_ID)) <> CInt(strActivityID)) Then
                                    strQuery = String.Concat(strQuery, " WHERE ", CommonUIData.DPT_ID, "=", intDepotID, strOrderBy)
                                Else
                                    strQuery = String.Concat(strQuery, " ", strOrderBy)
                                End If
                            End If

                        End If
                    End If

                End If



                'Check the Configuration Settings for enable repair work flow
                Dim bln037_KeyExist As Boolean = False
                Dim intActivityId As Integer
                Dim strActivityName As String = String.Empty
                If Not IsDBNull(.Item(CommonUIData.ACTVTY_ID)) Then
                    intActivityId = CInt((.Item(CommonUIData.ACTVTY_ID)))
                End If
                If Not IsDBNull(.Item(CommonUIData.ACTVTY_NAM)) Then
                    strActivityName = CStr(.Item(CommonUIData.ACTVTY_NAM))
                End If
                'REPAIR FLOW SETTING: 037
                'CR-001 (AWE_TO_AWP)
                GetSettings("037", intDepotID, bln037_KeyExist)
                If bln037_KeyExist Then
                    pvt_GetQuery(strQuery, intActivityId)
                Else
                    If intActivityId = 85 OrElse intActivityId = 170 Then
                        strQuery = String.Concat(strQuery, RepairEstimatePendingFlowQuery)
                    ElseIf intActivityId = 89 OrElse intActivityId = 171 Then
                        strQuery = String.Concat(strQuery, RepairApprovalPendingFlowQuery)
                    End If

                End If
                Dim dsListData As New DataSet()
                Dim objData As New DataObjects(strQuery)
                objData.Fill(dsListData, CStr(.Item(CommonUIData.ACTVTY_NAM)))
                dsListData.Tables(CStr(.Item(CommonUIData.ACTVTY_NAM))).Columns.Add("rowindex", GetType(Int64))
                Return dsListData
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetMySubmitsListRecords"
    <OperationContract()> _
    Public Function GetMySubmitsListRecords(ByVal bv_dsCommon As CommonUIDataSet, ByVal bv_strWFdata As String) As DataSet
        Try
            With bv_dsCommon.Tables(CommonUIData._ACTIVITY).Rows(0)
                Dim strQuery As String = ""
                Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFdata, CommonUIData.DPT_ID))
                If Not .Item(CommonUIData.ACTVTY_RL).ToString = "" And Not .Item(CommonUIData.ACTVTY_RL).ToString = String.Empty Then
                    Dim strItems() As String = .Item(CommonUIData.ACTVTY_RL).ToString.Split(CChar(","))
                    For i As Integer = 0 To strItems.Length - 1
                        If strItems(i).Substring(strItems(i).IndexOf("<") + 1, strItems(i).IndexOf(":", strItems(i).IndexOf("<")) - strItems(i).IndexOf("<") - 1) = "L" _
                            OrElse strItems(i).Substring(strItems(i).IndexOf("<") + 1, strItems(i).IndexOf(":", strItems(i).IndexOf("<")) - strItems(i).IndexOf("<") - 1) = "M" Then
                            Dim result As String = strItems(i).Substring(strItems(i).IndexOf(":") + 1, strItems(i).IndexOf(">", strItems(i).IndexOf(":")) - strItems(i).IndexOf(":") - 1)
                            Dim bln_KeyExist As Boolean
                            Dim strKeyValue As String = GetSettings(result, intDepotID, bln_KeyExist)
                            If bln_KeyExist Then
                                If CStr(.Item(CommonUIData.MY_SBMTS_QRY)).Contains(strItems(i)) Then
                                    If strQuery = "" Then
                                        strQuery = CStr(.Item(CommonUIData.MY_SBMTS_QRY)).Replace(strItems(i), strKeyValue)
                                    Else
                                        strQuery = strQuery.Replace(strItems(i), strKeyValue)
                                    End If
                                End If
                            Else
                                strQuery.Replace(strItems(i), "Lessee Code")
                            End If
                        End If
                    Next
                Else
                    strQuery = CStr(.Item(CommonUIData.MY_SBMTS_QRY))
                End If
                ' Dim strActivityId As String = CommonUIData.ACTVTY_ID
                Dim strKeyword As String() = {"ORDER BY"}
                Dim strParts As String() = strQuery.Split(strKeyword, StringSplitOptions.None)
                Dim strOrderBy As String = ""
                If strParts.Length > 1 Then
                    strQuery = strParts(0)
                    strOrderBy = String.Concat(" ORDER BY ", strParts(1))
                End If

                If strQuery.IndexOf("WHERE") > 0 Then
                    strQuery = String.Concat(strQuery, " AND ", CommonUIData.DPT_ID, "=", intDepotID, strOrderBy)
                Else
                    strQuery = String.Concat(strQuery, " WHERE ", CommonUIData.DPT_ID, "=", intDepotID, strOrderBy)
                End If
                Dim dsListData As New DataSet()
                Dim objData As New DataObjects(strQuery)
                objData.Fill(dsListData, CStr(.Item(CommonUIData.ACTVTY_NAM)))
                dsListData.Tables(CStr(.Item(CommonUIData.ACTVTY_NAM))).Columns.Add("rowindex", GetType(Int64))
                Return dsListData
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetRoleRightsByRoleIDAndActivityID()"
    <OperationContract()> _
    Public Function pub_GetRoleRightByRoleIDAndActivityID(ByVal bv_intRoleID As Integer, ByVal bv_intActivityID As Integer) As CommonUIDataSet
        Dim objCommonUIs As New CommonUIs
        Try
            Dim dsCommonUI As CommonUIDataSet

            dsCommonUI = objCommonUIs.GetRoleRightByRoleIDAndActivityID(bv_intRoleID, bv_intActivityID)

            Return dsCommonUI
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "pub_GetRoleRightByRoleID()"
    <OperationContract()> _
    Public Function pub_GetRoleRightByRoleID(ByVal bv_intRoleID As Integer) As CommonUIDataSet
        Dim objCommonUIs As New CommonUIs
        Try
            Dim dsCommonUI As CommonUIDataSet

            dsCommonUI = objCommonUIs.GetRoleRightByRoleID(bv_intRoleID)

            Return dsCommonUI
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "pub_UpdateUserDetailFavourites"
    <OperationContract()> _
    Public Function pub_UpdateUserDetailFavourites(ByVal bv_i32UserId As Integer, ByVal bv_strFavourites As String) As Boolean
        Dim objCommonUIs As New CommonUIs
        Try
            objCommonUIs.UpdateUserDetailFavourites(bv_i32UserId, bv_strFavourites)
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "ReportParameter"

#Region "pub_GetParameter() TABLE NAME:Hashtable"
    <OperationContract()> _
    Public Function pub_GetParameter(ByVal bv_strKey As String, ByVal bv_paramdata As String) As String
        Dim NMColl As Hashtable
        Dim strKeyValue As String = ""
        NMColl = ParseParameter(bv_paramdata)
        If NMColl.Contains(bv_strKey) Then
            If NMColl.Item(bv_strKey).ToString <> "" Then
                strKeyValue = NMColl.Item(bv_strKey).ToString
            End If
        End If
        Return strKeyValue
    End Function
#End Region

#Region "ParseParameter"
    <OperationContract()> _
    Public Shared Function ParseParameter(ByVal qrystr As String) As Hashtable
        Dim hstble As New Hashtable
        Dim strItems() As String
        strItems = qrystr.Split(CChar("&"))
        If strItems.Length = 0 Then
            Throw New Exception("Input parameter is not valid")
        End If
        For i As Integer = 0 To strItems.Length - 1
            If strItems(i) <> "" Then
                hstble.Add(strItems(i).Split(CChar("="))(0), strItems(i).Split(CChar("="))(1))
            End If
        Next i

        ParseParameter = hstble

        hstble = Nothing
        strItems = Nothing
    End Function
#End Region

#Region "pub_GetREPORTPARAMETERByRPRTID() TABLE NAME:REPORT_PARAMETER"
    <OperationContract()> _
    Public Function pub_GetReportParameter(ByVal bv_RPRT_ID As Int32) As CommonUIDataSet
        Try
            Dim dsReportData As CommonUIDataSet
            Dim objCommon As New CommonUIs
            dsReportData = objCommon.GetREPORT_PARAMETERByRPRT_ID(bv_RPRT_ID)
            Return dsReportData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetReportFields() TABLE NAME:REPORT_PARAMETER"
    <OperationContract()> _
    Public Function pub_GetReportFields(ByVal bv_RPRT_ID As Int32) As DataTable
        Try
            Dim dsReportData As CommonUIDataSet
            Dim objCommon As New CommonUIs

            dsReportData = objCommon.GetReportFields(bv_RPRT_ID)

            Dim dtReportFields As New DataTable

            Dim dc As New DataColumn("Checked")
            dc.DataType = GetType(Boolean)
            dc.DefaultValue = True
            If Not dtReportFields.Columns.Contains("Checked") Then
                dtReportFields.Columns.Add(dc)
            End If

            dc = New DataColumn("FieldName")
            dc.DataType = GetType(String)
            If Not dtReportFields.Columns.Contains("FieldName") Then
                dtReportFields.Columns.Add(dc)
            End If

            Dim keys(0) As DataColumn
            keys(0) = dtReportFields.Columns("FieldName")
            dtReportFields.PrimaryKey = keys

            If dsReportData.Tables(CommonUIData._ACTIVITY).Rows.Count > 0 Then
                Dim strListQuery As String
                strListQuery = dsReportData.Tables(CommonUIData._ACTIVITY).Rows(0).Item(CommonUIData.LST_QRY).ToString()
                Dim strColumns() As String
                strColumns = strListQuery.Split(CChar(","))
                Dim drFields As DataRow
                For Each strField As String In strColumns
                    drFields = dtReportFields.NewRow
                    drFields.Item("FieldName") = strField
                    dtReportFields.Rows.Add(drFields)
                Next
            End If
            Return dtReportFields
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetReportParameterValue() TABLE NAME:REPORT_PARAMETER"
    <OperationContract()> _
    Public Function pub_GetReportParameterValue(ByVal bv_strQuery As String) As DataTable

        Try
            Dim dtReportData As DataTable
            Dim objCommon As New CommonUIs
            dtReportData = objCommon.GetReportParameterValue(bv_strQuery)
            Return dtReportData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#End Region

#Region "Reports"

#Region "Status Report"
    <OperationContract()> _
    Public Function GetStatusReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String, _
                                    ByVal bv_intReportID As Int32) As CommonUIDataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsCommonUIDataSet As CommonUIDataSet
        Dim dtCustomerSummary As DataTable
        Dim dtEqpmntTyp As DataTable
        Try
            Dim IndatFromDate As DateTime = Nothing
            Dim IndatToDate As DateTime = Nothing
            Dim CleaningdatFromDate As DateTime = Nothing
            Dim CleaningdatToDate As DateTime = Nothing
            Dim InspdatFromDate As DateTime = Nothing
            Dim InspdatToDate As DateTime = Nothing
            Dim CrrntDateFrom As DateTime = Nothing
            Dim CrrntDateTo As DateTime = Nothing
            Dim NextTestDateFrom As DateTime = Nothing
            Dim NextTestDateTo As DateTime = Nothing
            Dim strCurrentStatus As String = String.Empty
            Dim strNxtTestType As String = String.Empty
            Dim strEirNo As String = String.Empty
            Dim strPreviousCargo As String = String.Empty
            Dim strEquipmentNo As String = String.Empty
            Dim strEquipmentTyp As String = String.Empty
            Dim OutFromDate As DateTime = Nothing
            Dim OutToDate As DateTime = Nothing
            Dim str_037KeyValue As String = String.Empty
            Dim bln_037KeyValue As Boolean = False
            Dim strDepotId As String = String.Empty
            Dim str_007Value As String = objCommonUIs.DecryptString(CStr(objCommonUIs.GetConfigByKeyName("007", CLng(IIf(objCommonUIs.GetMultiLocationSupportConfig.ToLower = "true", objCommonUIs.GetHeadQuarterID(), intDepotID))).Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL)))
            Dim strCustomer As String = pub_GetParameter("Customer", bv_param)
            If pub_GetParameter("Depot", bv_param) = "" Then
                strDepotId = CStr(intDepotID)
            Else
                strDepotId = pub_GetParameter("Depot", bv_param)
            End If
            ''Changes done for send DAR Report containt all depot details.        
            'If pub_GetParameter("MultiLocationDepotMail", bv_param) = "True" Then
            '    strDepotId = pub_GetParameter("MultiLocationDepotID", bv_param)
            'End If


            If pub_GetParameter("In Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            End If
            If pub_GetParameter("In Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("In Date To", bv_param)) AndAlso pub_GetParameter("In Date From", bv_param) = "" Then
                IndatFromDate = IndatToDate.AddDays(-IndatToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("In Date From", bv_param)) AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                IndatToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strPreviousCargo = pub_GetParameter("Previous Cargo", bv_param)
            End If
            If pub_GetParameter("Cleaning Date From", bv_param) <> "" Then
                CleaningdatFromDate = CDate(pub_GetParameter("Cleaning Date From", bv_param))
            End If
            If pub_GetParameter("Cleaning Date To", bv_param) <> "" Then
                CleaningdatToDate = CDate(pub_GetParameter("Cleaning Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Cleaning Date To", bv_param)) AndAlso pub_GetParameter("Cleaning Date From", bv_param) = "" Then
                CleaningdatFromDate = CleaningdatToDate.AddDays(-CleaningdatToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("Cleaning Date From", bv_param)) AndAlso pub_GetParameter("Cleaning Date To", bv_param) = "" Then
                CleaningdatToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Inspection Date From", bv_param) <> "" Then
                InspdatFromDate = CDate(pub_GetParameter("Inspection Date From", bv_param))
            End If
            If pub_GetParameter("Inspection Date To", bv_param) <> "" Then
                InspdatToDate = CDate(pub_GetParameter("Inspection Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Inspection Date To", bv_param)) AndAlso pub_GetParameter("Inspection Date From", bv_param) = "" Then
                InspdatFromDate = InspdatToDate.AddDays(-InspdatToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("Inspection Date From", bv_param)) AndAlso pub_GetParameter("Inspection Date To", bv_param) = "" Then
                InspdatToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Current Status Date From", bv_param) <> "" Then
                CrrntDateFrom = CDate(pub_GetParameter("Current Status Date From", bv_param))
            End If
            If pub_GetParameter("Current Status Date To", bv_param) <> "" Then
                CrrntDateTo = CDate(pub_GetParameter("Current Status Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Current Status Date To", bv_param)) AndAlso pub_GetParameter("Current Status Date From", bv_param) = "" Then
                CrrntDateFrom = CrrntDateTo.AddDays(-CrrntDateTo.Day + 1)
            ElseIf IsDate(pub_GetParameter("Current Status Date From", bv_param)) AndAlso pub_GetParameter("Current Status Date To", bv_param) = "" Then
                CrrntDateTo = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Next Test Date From", bv_param) <> "" Then
                NextTestDateFrom = CDate(pub_GetParameter("Next Test Date From", bv_param))
                NextTestDateFrom = CDate(String.Concat("01-", NextTestDateFrom.ToString("MMM"), "-", NextTestDateFrom.ToString("yyyy")))
            End If
            If pub_GetParameter("Next Test Date To", bv_param) <> "" Then
                NextTestDateTo = CDate(pub_GetParameter("Next Test Date To", bv_param))
                NextTestDateTo = New DateTime(NextTestDateTo.Year, NextTestDateTo.Month, DateTime.DaysInMonth(NextTestDateTo.Year, NextTestDateTo.Month))
            End If

            If IsDate(pub_GetParameter("Next Test Date To", bv_param)) AndAlso pub_GetParameter("Next Test Date From", bv_param) = "" Then
                NextTestDateFrom = NextTestDateTo.AddDays(-NextTestDateTo.Day + 1)
                NextTestDateFrom = CDate(String.Concat("01-", NextTestDateFrom.ToString("MMM"), "-", NextTestDateFrom.ToString("yyyy")))
            ElseIf IsDate(pub_GetParameter("Next Test Date From", bv_param)) AndAlso pub_GetParameter("Next Test Date To", bv_param) = "" Then
                NextTestDateTo = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
                NextTestDateTo = New DateTime(NextTestDateTo.Year, NextTestDateTo.Month, DateTime.DaysInMonth(NextTestDateTo.Year, NextTestDateTo.Month))
            End If

            If pub_GetParameter("Current Status", bv_param) <> "" Then
                strCurrentStatus = (pub_GetParameter("Current Status", bv_param))
                ''REPAIR FLOW SETTING: 037
                ''  CR(-1(AWE_TO_AWP))
                If strCurrentStatus.Contains("7") Then
                    strCurrentStatus = String.Concat(strCurrentStatus, ",'19'")
                End If
            End If
            If pub_GetParameter("Next Test Type", bv_param) <> "" Then
                strNxtTestType = pub_GetParameter("Next Test Type", bv_param)
            End If
            If pub_GetParameter(str_007Value, bv_param) <> "" Then
                strEirNo = pub_GetParameter(str_007Value, bv_param)
            End If
            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strEquipmentNo = pub_GetParameter("Equipment No", bv_param)
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strEquipmentTyp = pub_GetParameter("Equipment Type", bv_param)
            End If

            If pub_GetParameter("Out Date From", bv_param) <> "" Then
                OutFromDate = CDate(pub_GetParameter("Out Date From", bv_param))
            End If
            If pub_GetParameter("Out Date To", bv_param) <> "" Then
                OutToDate = CDate(pub_GetParameter("Out Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Out Date To", bv_param)) AndAlso pub_GetParameter("Out Date From", bv_param) = "" Then
                OutFromDate = OutToDate.AddDays(-OutToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("Out Date From", bv_param)) AndAlso pub_GetParameter("Out Date To", bv_param) = "" Then
                OutToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            Dim strWhere As String = String.Empty
            Dim strInvWhere As String = String.Empty
            Dim strInvSplitWhere As String = String.Empty
            Dim strOutWhere As String = String.Empty
            strWhere = getStatusReportWhereCondtion(strCustomer, _
                                            IndatFromDate, _
                                            IndatToDate, _
                                            strPreviousCargo, _
                                            CleaningdatFromDate, _
                                            CleaningdatToDate, _
                                            InspdatFromDate, _
                                            InspdatToDate, _
                                            CrrntDateFrom, _
                                            CrrntDateTo, _
                                            NextTestDateFrom, _
                                            NextTestDateTo, _
                                            strCurrentStatus, _
                                            strNxtTestType, _
                                            strEirNo, _
                                            strEquipmentNo, _
                                            strEquipmentTyp, _
                                            OutFromDate, _
                                            OutToDate, _
                                            strDepotId, _
                                            bv_ReportName)


            If bv_ReportName = "Inventory" Then
                If OutFromDate <> Nothing And OutToDate <> Nothing Then
                    strInvWhere = String.Concat(" AND ", CommonUIData.GTOT_DT, " >= '", OutFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTOT_DT, " <= '", OutToDate.ToString("dd-MMM-yyyy"), "')")
                End If
                strInvSplitWhere = strWhere.ToString()
                strOutWhere = strWhere.Replace("ACTV_BT = 1", "ACTV_BT = 0")
                strWhere = String.Concat(strWhere, " OR (", strWhere.Replace("ACTV_BT = 1", "ACTV_BT = 0"), strInvWhere)
            Else
                OutFromDate = Now
                OutToDate = Now
                strInvSplitWhere = strWhere.ToString()
                strOutWhere = strWhere.ToString()
            End If

            dsCommonUIDataSet = objCommonUIs.GetDAR_ACTIVITY_STATUS(strWhere)
            If bv_intReportID = 101 OrElse bv_intReportID = 108 Then
                dtCustomerSummary = objCommonUIs.GetCustomerSummary(strWhere, OutFromDate, OutToDate, intDepotID, strInvSplitWhere, strOutWhere).Tables(CommonUIData._V_DAR_CUSTOMER_SUMMARY)
                dtEqpmntTyp = objCommonUIs.GetEquipmentTypeSummary(strWhere, OutFromDate, OutToDate, intDepotID, strInvSplitWhere, strOutWhere).Tables(CommonUIData._V_DAR_TYPE_SUMMARY)
            ElseIf bv_intReportID = 159 OrElse bv_intReportID = 160 Then
                dtCustomerSummary = objCommonUIs.GetCustomerSummaryGWS(strWhere, OutFromDate, OutToDate, intDepotID, strInvSplitWhere, strOutWhere).Tables(CommonUIData._V_DAR_CUSTOMER_SUMMARY)
                dtEqpmntTyp = objCommonUIs.GetEquipmentTypeSummaryGWS(strWhere, OutFromDate, OutToDate, intDepotID, strInvSplitWhere, strOutWhere).Tables(CommonUIData._V_DAR_TYPE_SUMMARY)
            End If


            If Not dsCommonUIDataSet Is Nothing AndAlso (bv_intReportID = 159 OrElse bv_intReportID = 160) Then

                For Each dr As DataRow In dsCommonUIDataSet.Tables(CommonUIData._V_DAR_ACTIVITY_STATUS).Rows

                    If dr.Item(CommonUIData.EQPMNT_STTS_DSCRPTN_VC).ToString().ToUpper() = "AWAITING CLEANING" Then
                        dr.Item(CommonUIData.CLNNG_DT) = DBNull.Value
                        dr.Item(CommonUIData.INSPCTN_DT) = DBNull.Value
                        dr.Item(CommonUIData.CLNNG_CERT_NO) = DBNull.Value
                    ElseIf dr.Item(CommonUIData.EQPMNT_STTS_DSCRPTN_VC).ToString().ToUpper() = "CLEANING" Then
                        dr.Item(CommonUIData.INSPCTN_DT) = DBNull.Value
                    End If

                Next
            End If

            dsCommonUIDataSet.Merge(dtCustomerSummary)
            dsCommonUIDataSet.Merge(dtEqpmntTyp)
            dsCommonUIDataSet.DataSetName = "StatusReportDataSet"
            Return dsCommonUIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "Status ReportGWS"
    <OperationContract()> _
    Public Function GetStatusReportGWS(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As CommonUIDataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsCommonUIDataSet As CommonUIDataSet
        Dim dtCustomerSummary As DataTable
        Dim dtEqpmntTyp As DataTable
        Try
            Dim IndatFromDate As DateTime = Nothing
            Dim IndatToDate As DateTime = Nothing
            Dim InspdatFromDate As DateTime = Nothing
            Dim InspdatToDate As DateTime = Nothing
            Dim CrrntDateFrom As DateTime = Nothing
            Dim CrrntDateTo As DateTime = Nothing
            Dim strCurrentStatus As String = String.Empty
            Dim strEirNo As String = String.Empty
            Dim strEquipmentNo As String = String.Empty
            Dim strEquipmentTyp As String = String.Empty
            Dim OutFromDate As DateTime = Nothing
            Dim OutToDate As DateTime = Nothing
            'Dim str_037KeyValue As String = String.Empty
            'Dim bln_037KeyValue As Boolean = False
            Dim str_007Value As String = objCommonUIs.DecryptString(CStr(objCommonUIs.GetConfigByKeyName("007", CLng(IIf(objCommonUIs.GetMultiLocationSupportConfig.ToLower = "true", objCommonUIs.GetHeadQuarterID(), intDepotID))).Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL)))
            Dim strCustomer As String = pub_GetParameter("Customer", bv_param)


            If pub_GetParameter("In Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            End If
            If pub_GetParameter("In Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("In Date To", bv_param)) AndAlso pub_GetParameter("In Date From", bv_param) = "" Then
                IndatFromDate = IndatToDate.AddDays(-IndatToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("In Date From", bv_param)) AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                IndatToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Inspection Date From", bv_param) <> "" Then
                InspdatFromDate = CDate(pub_GetParameter("Inspection Date From", bv_param))
            End If
            If pub_GetParameter("Inspection Date To", bv_param) <> "" Then
                InspdatToDate = CDate(pub_GetParameter("Inspection Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Inspection Date To", bv_param)) AndAlso pub_GetParameter("Inspection Date From", bv_param) = "" Then
                InspdatFromDate = InspdatToDate.AddDays(-InspdatToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("Inspection Date From", bv_param)) AndAlso pub_GetParameter("Inspection Date To", bv_param) = "" Then
                InspdatToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Current Status Date From", bv_param) <> "" Then
                CrrntDateFrom = CDate(pub_GetParameter("Current Status Date From", bv_param))
            End If
            If pub_GetParameter("Current Status Date To", bv_param) <> "" Then
                CrrntDateTo = CDate(pub_GetParameter("Current Status Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Current Status Date To", bv_param)) AndAlso pub_GetParameter("Current Status Date From", bv_param) = "" Then
                CrrntDateFrom = CrrntDateTo.AddDays(-CrrntDateTo.Day + 1)
            ElseIf IsDate(pub_GetParameter("Current Status Date From", bv_param)) AndAlso pub_GetParameter("Current Status Date To", bv_param) = "" Then
                CrrntDateTo = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Current Status", bv_param) <> "" Then
                strCurrentStatus = (pub_GetParameter("Current Status", bv_param))
                ''REPAIR FLOW SETTING: 037
                ''  CR(-1(AWE_TO_AWP))
                If strCurrentStatus.Contains("7") Then
                    strCurrentStatus = String.Concat(strCurrentStatus, ",'19'")
                End If
            End If

            If pub_GetParameter("Out Date From", bv_param) <> "" Then
                OutFromDate = CDate(pub_GetParameter("Out Date From", bv_param))
            End If
            If pub_GetParameter("Out Date To", bv_param) <> "" Then
                OutToDate = CDate(pub_GetParameter("Out Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Out Date To", bv_param)) AndAlso pub_GetParameter("Out Date From", bv_param) = "" Then
                OutFromDate = OutToDate.AddDays(-OutToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("Out Date From", bv_param)) AndAlso pub_GetParameter("Out Date To", bv_param) = "" Then
                OutToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter(str_007Value, bv_param) <> "" Then
                strEirNo = pub_GetParameter(str_007Value, bv_param)
            End If
            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strEquipmentNo = pub_GetParameter("Equipment No", bv_param)
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strEquipmentTyp = pub_GetParameter("Equipment Type", bv_param)
            End If

            Dim strWhere As String = String.Empty
            Dim strInvWhere As String = String.Empty
            Dim strInvSplitWhere As String = String.Empty
            Dim strOutWhere As String = String.Empty
            strWhere = getStatusReportWhereCondtion(strCustomer, _
                                            IndatFromDate, _
                                            IndatToDate, _
                                            "", _
                                            Nothing, _
                                            Nothing, _
                                            InspdatFromDate, _
                                            InspdatToDate, _
                                            CrrntDateFrom, _
                                            CrrntDateTo, _
                                            Nothing, _
                                            Nothing, _
                                            strCurrentStatus, _
                                            "", _
                                            strEirNo, _
                                            strEquipmentNo, _
                                            strEquipmentTyp, _
                                            OutFromDate, _
                                            OutToDate, _
                                            CStr(intDepotID), _
                                            bv_ReportName)


            If bv_ReportName = "Inventory" Then
                If OutFromDate <> Nothing And OutToDate <> Nothing Then
                    strInvWhere = String.Concat(" AND ", CommonUIData.GTOT_DT, " >= '", OutFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTOT_DT, " <= '", OutToDate.ToString("dd-MMM-yyyy"), "')")
                End If
                strInvSplitWhere = strWhere.ToString()
                strOutWhere = strWhere.Replace("ACTV_BT = 1", "ACTV_BT = 0")
                strWhere = String.Concat(strWhere, " OR (", strWhere.Replace("ACTV_BT = 1", "ACTV_BT = 0"), strInvWhere)
            Else
                OutFromDate = Now
                OutToDate = Now
                strInvSplitWhere = strWhere.ToString()
                strOutWhere = strWhere.ToString()
            End If

            dsCommonUIDataSet = objCommonUIs.GetDAR_ACTIVITY_STATUS(strWhere)
            dtCustomerSummary = objCommonUIs.GetCustomerSummaryGWS(strWhere, OutFromDate, OutToDate, intDepotID, strInvSplitWhere, strOutWhere).Tables(CommonUIData._V_DAR_CUSTOMER_SUMMARY)
            dtEqpmntTyp = objCommonUIs.GetEquipmentTypeSummaryGWS(strWhere, OutFromDate, OutToDate, intDepotID, strInvSplitWhere, strOutWhere).Tables(CommonUIData._V_DAR_TYPE_SUMMARY)

            'If Not dsCommonUIDataSet Is Nothing Then

            '    For Each dr As DataRow In dsCommonUIDataSet.Tables(CommonUIData._V_DAR_ACTIVITY_STATUS).Rows

            '        If dr.Item(CommonUIData.EQPMNT_STTS_DSCRPTN_VC).ToString().ToUpper() = "AWAITING CLEANING" Then
            '            dr.Item(CommonUIData.CLNNG_DT) = DBNull.Value
            '            dr.Item(CommonUIData.INSPCTN_DT) = DBNull.Value
            '            dr.Item(CommonUIData.CLNNG_CERT_NO) = DBNull.Value
            '        ElseIf dr.Item(CommonUIData.EQPMNT_STTS_DSCRPTN_VC).ToString().ToUpper() = "CLEANING" Then
            '            dr.Item(CommonUIData.INSPCTN_DT) = DBNull.Value
            '        End If

            '    Next
            'End If

            dsCommonUIDataSet.Merge(dtCustomerSummary)
            dsCommonUIDataSet.Merge(dtEqpmntTyp)
            dsCommonUIDataSet.DataSetName = "StatusReportDataSet"
            Return dsCommonUIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "Pending Inspection Report"
    <OperationContract()> _
    Public Function GetPendingInspectionReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As CommonUIDataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsCommonUIDataSet As CommonUIDataSet
        Dim dtCustomerSummary As DataTable
        Dim dtEqpmntTyp As DataTable
        Try
            Dim IndatFromDate As DateTime = Nothing
            Dim IndatToDate As DateTime = Nothing
            Dim CleaningdatFromDate As DateTime = Nothing
            Dim CleaningdatToDate As DateTime = Nothing
            Dim InspdatFromDate As DateTime = Nothing
            Dim InspdatToDate As DateTime = Nothing
            Dim CrrntDateFrom As DateTime = Nothing
            Dim CrrntDateTo As DateTime = Nothing
            Dim NextTestDateFrom As DateTime = Nothing
            Dim NextTestDateTo As DateTime = Nothing
            Dim strCurrentStatus As String = String.Empty
            Dim strNxtTestType As String = String.Empty
            Dim strEirNo As String = String.Empty
            Dim strPreviousCargo As String = String.Empty
            Dim strEquipmentNo As String = String.Empty
            Dim strEquipmentTyp As String = String.Empty
            Dim OutFromDate As DateTime = Nothing
            Dim OutToDate As DateTime = Nothing
            Dim str_037KeyValue As String = String.Empty
            Dim bln_037KeyValue As Boolean = False
            Dim strDepot As String = String.Empty
            Dim strCustomer As String = pub_GetParameter("Customer", bv_param)

            If pub_GetParameter("Depot", bv_param) <> "" Then
                strDepot = pub_GetParameter("Depot", bv_param)
            Else
                strDepot = CStr(intDepotID)
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            End If
            If pub_GetParameter("In Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("In Date To", bv_param)) AndAlso pub_GetParameter("In Date From", bv_param) = "" Then
                IndatFromDate = IndatToDate.AddDays(-IndatToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("In Date From", bv_param)) AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                IndatToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strPreviousCargo = pub_GetParameter("Previous Cargo", bv_param)
            End If
            If pub_GetParameter("Cleaning Date From", bv_param) <> "" Then
                CleaningdatFromDate = CDate(pub_GetParameter("Cleaning Date From", bv_param))
            End If
            If pub_GetParameter("Cleaning Date To", bv_param) <> "" Then
                CleaningdatToDate = CDate(pub_GetParameter("Cleaning Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Cleaning Date To", bv_param)) AndAlso pub_GetParameter("Cleaning Date From", bv_param) = "" Then
                CleaningdatFromDate = CleaningdatToDate.AddDays(-CleaningdatToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("Cleaning Date From", bv_param)) AndAlso pub_GetParameter("Cleaning Date To", bv_param) = "" Then
                CleaningdatToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            'If pub_GetParameter("Inspection Date From", bv_param) <> "" Then
            '    InspdatFromDate = CDate(pub_GetParameter("Inspection Date From", bv_param))
            'End If
            'If pub_GetParameter("Inspection Date To", bv_param) <> "" Then
            '    InspdatToDate = CDate(pub_GetParameter("Inspection Date To", bv_param))
            'End If

            'If IsDate(pub_GetParameter("Inspection Date To", bv_param)) AndAlso pub_GetParameter("Inspection Date From", bv_param) = "" Then
            '    InspdatFromDate = InspdatToDate.AddDays(-InspdatToDate.Day + 1)
            'ElseIf IsDate(pub_GetParameter("Inspection Date From", bv_param)) AndAlso pub_GetParameter("Inspection Date To", bv_param) = "" Then
            '    InspdatToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            'End If

            'If pub_GetParameter("Current Status Date From", bv_param) <> "" Then
            '    CrrntDateFrom = CDate(pub_GetParameter("Current Status Date From", bv_param))
            'End If
            'If pub_GetParameter("Current Status Date To", bv_param) <> "" Then
            '    CrrntDateTo = CDate(pub_GetParameter("Current Status Date To", bv_param))
            'End If

            'If IsDate(pub_GetParameter("Current Status Date To", bv_param)) AndAlso pub_GetParameter("Current Status Date From", bv_param) = "" Then
            '    CrrntDateFrom = CrrntDateTo.AddDays(-CrrntDateTo.Day + 1)
            'ElseIf IsDate(pub_GetParameter("Current Status Date From", bv_param)) AndAlso pub_GetParameter("Current Status Date To", bv_param) = "" Then
            '    CrrntDateTo = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            'End If

            'If pub_GetParameter("Next Test Date From", bv_param) <> "" Then
            '    NextTestDateFrom = CDate(pub_GetParameter("Next Test Date From", bv_param))
            '    NextTestDateFrom = CDate(String.Concat("01-", NextTestDateFrom.ToString("MMM"), "-", NextTestDateFrom.ToString("yyyy")))
            'End If
            'If pub_GetParameter("Next Test Date To", bv_param) <> "" Then
            '    NextTestDateTo = CDate(pub_GetParameter("Next Test Date To", bv_param))
            '    NextTestDateTo = New DateTime(NextTestDateTo.Year, NextTestDateTo.Month, DateTime.DaysInMonth(NextTestDateTo.Year, NextTestDateTo.Month))
            'End If

            'If IsDate(pub_GetParameter("Next Test Date To", bv_param)) AndAlso pub_GetParameter("Next Test Date From", bv_param) = "" Then
            '    NextTestDateFrom = NextTestDateTo.AddDays(-NextTestDateTo.Day + 1)
            '    NextTestDateFrom = CDate(String.Concat("01-", NextTestDateFrom.ToString("MMM"), "-", NextTestDateFrom.ToString("yyyy")))
            'ElseIf IsDate(pub_GetParameter("Next Test Date From", bv_param)) AndAlso pub_GetParameter("Next Test Date To", bv_param) = "" Then
            '    NextTestDateTo = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            '    NextTestDateTo = New DateTime(NextTestDateTo.Year, NextTestDateTo.Month, DateTime.DaysInMonth(NextTestDateTo.Year, NextTestDateTo.Month))
            'End If

            'If pub_GetParameter("Current Status", bv_param) <> "" Then
            '    strCurrentStatus = (pub_GetParameter("Current Status", bv_param))
            '    ''REPAIR FLOW SETTING: 037
            '    ''  CR(-1(AWE_TO_AWP))
            '    If strCurrentStatus.Contains("7") Then
            '        strCurrentStatus = String.Concat(strCurrentStatus, ",'19'")
            '    End If
            'End If
            'If pub_GetParameter("Next Test Type", bv_param) <> "" Then
            '    strNxtTestType = pub_GetParameter("Next Test Type", bv_param)
            'End If
            'If pub_GetParameter("JTS EIR No", bv_param) <> "" Then
            '    strEirNo = pub_GetParameter("JTS EIR No", bv_param)
            'End If
            'If pub_GetParameter("Equipment No", bv_param) <> "" Then
            '    strEquipmentNo = pub_GetParameter("Equipment No", bv_param)
            'End If
            'If pub_GetParameter("Equipment Type", bv_param) <> "" Then
            '    strEquipmentTyp = pub_GetParameter("Equipment Type", bv_param)
            'End If

            'If pub_GetParameter("Out Date From", bv_param) <> "" Then
            '    OutFromDate = CDate(pub_GetParameter("Out Date From", bv_param))
            'End If
            'If pub_GetParameter("Out Date To", bv_param) <> "" Then
            '    OutToDate = CDate(pub_GetParameter("Out Date To", bv_param))
            'End If

            'If IsDate(pub_GetParameter("Out Date To", bv_param)) AndAlso pub_GetParameter("Out Date From", bv_param) = "" Then
            '    OutFromDate = OutToDate.AddDays(-OutToDate.Day + 1)
            'ElseIf IsDate(pub_GetParameter("Out Date From", bv_param)) AndAlso pub_GetParameter("Out Date To", bv_param) = "" Then
            '    OutToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            'End If

            Dim strWhere As String = String.Empty
            Dim strInvWhere As String = String.Empty
            Dim strInvSplitWhere As String = String.Empty
            Dim strOutWhere As String = String.Empty
            strWhere = getStatusReportWhereCondtion(strCustomer, _
                                            IndatFromDate, _
                                            IndatToDate, _
                                            strPreviousCargo, _
                                            CleaningdatFromDate, _
                                            CleaningdatToDate, _
                                            InspdatFromDate, _
                                            InspdatToDate, _
                                            CrrntDateFrom, _
                                            CrrntDateTo, _
                                            NextTestDateFrom, _
                                            NextTestDateTo, _
                                            strCurrentStatus, _
                                            strNxtTestType, _
                                            strEirNo, _
                                            strEquipmentNo, _
                                            strEquipmentTyp, _
                                            OutFromDate, _
                                            OutToDate, _
                                            strDepot, _
                                            bv_ReportName)

            If bv_ReportName = "Inventory" Then
                If OutFromDate <> Nothing And OutToDate <> Nothing Then
                    strInvWhere = String.Concat(" AND ", CommonUIData.GTOT_DT, " >= '", OutFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTOT_DT, " <= '", OutToDate.ToString("dd-MMM-yyyy"), "')")
                End If
                strInvSplitWhere = strWhere.ToString()
                strOutWhere = strWhere.Replace("ACTV_BT = 1", "ACTV_BT = 0")
                strWhere = String.Concat(strWhere, " OR (", strWhere.Replace("ACTV_BT = 1", "ACTV_BT = 0"), strInvWhere)
            Else
                OutFromDate = Now
                OutToDate = Now
                strInvSplitWhere = strWhere.ToString()
                strOutWhere = strWhere.ToString()
            End If
            strWhere = String.Concat(strWhere, " and EQPMNT_STTS_id=5 ")
            dsCommonUIDataSet = objCommonUIs.GetDAR_ACTIVITY_STATUS(strWhere)
            'dtCustomerSummary = objCommonUIs.GetCustomerSummary(strWhere, OutFromDate, OutToDate, intDepotID, strInvSplitWhere, strOutWhere).Tables(CommonUIData._V_DAR_CUSTOMER_SUMMARY)
            'dtEqpmntTyp = objCommonUIs.GetEquipmentTypeSummary(strWhere, OutFromDate, OutToDate, intDepotID, strInvSplitWhere, strOutWhere).Tables(CommonUIData._V_DAR_TYPE_SUMMARY)

            'dsCommonUIDataSet.Merge(dtCustomerSummary)
            'dsCommonUIDataSet.Merge(dtEqpmntTyp)
            dsCommonUIDataSet.DataSetName = "StatusReportDataSet"
            Return dsCommonUIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "getStatusReportWhereCondtion"
    <OperationContract()> _
    Private Function getStatusReportWhereCondtion(ByVal bv_strCustomer As String, _
                                              ByVal bv_datInDateFrom As Date, _
                                              ByVal bv_datInDateTo As Date, _
                                              ByVal strPreviousCargo As String, _
                                              ByVal CleaningdatFromDate As Date, _
                                              ByVal CleaningdatToDate As Date, _
                                              ByVal InspdatFromDate As Date, _
                                              ByVal InspdatToDate As Date, _
                                              ByVal CrrntDateFrom As Date, _
                                              ByVal CrrntDateTo As Date, _
                                              ByVal NextTestDateFrom As Date, _
                                              ByVal NextTestDateTo As Date, _
                                              ByVal strCurrentStatus As String, _
                                              ByVal strNxtTestType As String, _
                                              ByVal strEirNo As String, _
                                              ByVal strEquipmentNo As String, _
                                              ByVal strEquipmentType As String, _
                                              ByVal dtOutDateFrom As Date, _
                                              ByVal dtOutDateTo As Date, _
                                              ByVal intDepotID As String, _
                                              ByVal bv_ReportName As String) As String

        Try
            Dim strWhere As String = String.Empty
            ' strWhere = String.Concat(" (", CommonUIData.DPT_ID, " = ", intDepotID)
            strWhere = String.Concat(" (", CommonUIData.DPT_ID, " IN (", intDepotID, ")")
            If bv_strCustomer <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", bv_strCustomer, ")")
            End If
            If bv_datInDateFrom <> Nothing And bv_datInDateTo <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTN_DT, " >= '", bv_datInDateFrom.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", bv_datInDateTo.ToString("dd-MMM-yyyy"), "'")
            End If
            If CleaningdatFromDate <> Nothing And CleaningdatToDate <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CLNNG_DT, " >= '", CleaningdatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.CLNNG_DT, " <= '", CleaningdatToDate.ToString("dd-MMM-yyyy"), "'")
            End If
            If InspdatFromDate <> Nothing And InspdatToDate <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.INSPCTN_DT, " >= '", InspdatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.INSPCTN_DT, " <= '", InspdatToDate.ToString("dd-MMM-yyyy"), "'")
            End If
            If CrrntDateFrom <> Nothing And CrrntDateTo <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.ACTVTY_DT, " >= '", CrrntDateFrom.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.ACTVTY_DT, " <= '", CrrntDateTo.ToString("dd-MMM-yyyy"), "'")
            End If
            If NextTestDateFrom <> Nothing And NextTestDateTo <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.NEXT_TEST_DATE, " >= '", NextTestDateFrom.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.NEXT_TEST_DATE, " <= '", NextTestDateTo.ToString("dd-MMM-yyyy"), "'")
            End If

            If strCurrentStatus <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_STTS_ID, " IN (", strCurrentStatus, ")")
            End If
            If strNxtTestType <> "" Then
                If strNxtTestType.Contains(",") AndAlso strNxtTestType.Split(CChar(",")).Length = 2 Then
                    strWhere = String.Concat(strWhere, " AND (", CommonUIData.NXT_TYP_ID, " IS NULL OR ", CommonUIData.NXT_TYP_ID, " IN (", strNxtTestType, "))")
                Else
                    strWhere = String.Concat(strWhere, " AND ", CommonUIData.NXT_TYP_ID, " IN (", strNxtTestType, ")")
                End If
            End If
            If strEirNo <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GI_RF_NO, " IN ('", strEirNo, "')")
            End If
            If strEquipmentNo <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " IN ('", strEquipmentNo, "')")
            End If
            If strEquipmentType <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", strEquipmentType, ")")
            End If
            If strPreviousCargo <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", strPreviousCargo, ")")
            End If
            If bv_ReportName = "Inventory" Then
                Return String.Concat(strWhere, " AND ACTV_BT = 1)")
            Else
                Return String.Concat(strWhere, ")")
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "GetAvailableUnitReport"
    <OperationContract()> _
    Function GetAvailableUnitReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As CommonUIDataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsCommonUIDataSet As CommonUIDataSet
        Dim dtCustomerSummary As DataTable
        Dim dtEqpmntTyp As DataTable
        Try
            Dim strCurrentStatus As String = String.Empty
            Dim strEquipmentNo As String = String.Empty
            Dim strEquipmentTyp As String = String.Empty
            Dim datToDate As Date
            Dim strCustomer As String = pub_GetParameter("Customer", bv_param)
            If bv_ReportName = "Awaiting Estimates" Then
                strCurrentStatus = "7"
            ElseIf pub_GetParameter("Status", bv_param) <> "" Then
                strCurrentStatus = (pub_GetParameter("Status", bv_param))
            End If

            If pub_GetParameter("Period To", bv_param) <> "" Then
                datToDate = CDate(pub_GetParameter("Period To", bv_param))
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strEquipmentNo = pub_GetParameter("Equipment No", bv_param)
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strEquipmentTyp = pub_GetParameter("Equipment Type", bv_param)
            End If

            Dim strWhere As String = String.Empty

            'Multilocation
            If pub_GetParameter("Depot", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " WHERE ", CommonUIData.DPT_ID, " IN (", intDepotID, ")")
            Else
                strWhere = String.Concat(strWhere, " WHERE ", CommonUIData.DPT_ID, " IN (", pub_GetParameter("Depot", bv_param), ")")
            End If

            If strCustomer <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", strCustomer, ")")
            End If

            If strCurrentStatus <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_STTS_ID, " IN (", strCurrentStatus, ")")
            End If
            If strEquipmentNo <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " IN ('", strEquipmentNo, "')")
            End If
            If strEquipmentTyp <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", strEquipmentTyp, ")")
            End If
            If datToDate <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.ACTVTY_DT, " <= '", datToDate.ToString("dd-MMM-yyyy"), " 23:59:59.999'")
            Else
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.ACTVTY_DT, " <= '", Now, "'  ")
            End If

            dsCommonUIDataSet = objCommonUIs.GetAvailableUnitData(strWhere, intDepotID)
            dtCustomerSummary = objCommonUIs.GetAvlUnitCustomerSummaryGWS(strWhere, intDepotID).Tables(CommonUIData._V_DAR_CUSTOMER_SUMMARY)
            dtEqpmntTyp = objCommonUIs.GetAvlUnitEquipmentTypeSummaryGWS(strWhere, intDepotID).Tables(CommonUIData._V_DAR_TYPE_SUMMARY)

            dsCommonUIDataSet.Merge(dtCustomerSummary)
            dsCommonUIDataSet.Merge(dtEqpmntTyp)
            dsCommonUIDataSet.DataSetName = "AvailableUnitsDataSet"
            Return dsCommonUIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#End Region

#Region "Documents"
#Region "GET: pub_GetDocumentByDocumentID"
    ''' <summary>
    ''' This method is used to get Document Templates by passing Document ID.
    ''' </summary>
    ''' <param name="bv_i32DocumentID">Denotes Document ID</param>
    ''' <returns>CommonUIDataSet</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetDocumentByDocumentID(ByVal bv_i32DocumentID As Int32) As CommonUIDataSet
        Dim dsCommonUI As CommonUIDataSet
        Dim objCommonUIs As New CommonUIs
        Try
            dsCommonUI = objCommonUIs.GetDocumentTemplateDocumentByDocumentID(bv_i32DocumentID)
            Return dsCommonUI
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            dsCommonUI = Nothing
            objCommonUIs = Nothing
        End Try

    End Function
#End Region


#Region "GET: pub_GetCustomerDetail"

    Public Function pub_GetCustomerDetail(ByVal intCustomerID As Integer) As CommonUIDataSet
        Try
            Dim dsCommonUI As CommonUIDataSet
            Dim objCommonUIs As New CommonUIs
            dsCommonUI = objCommonUIs.pub_GetCustomerDetail(intCustomerID)
            Return dsCommonUI
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET: pub_GetDocumentTemplateByTemplateID"
    ''' <summary>
    ''' This method is used to get Document Template Detail by passing Document Template ID.
    ''' </summary>
    ''' <param name="bv_i32DocumentTemplateID">Denotes Document Template ID</param>
    ''' <returns>CommonUIDataSet</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetDocumentTemplateByTemplateID(ByVal bv_i32DocumentTemplateID As Int32) As CommonUIDataSet
        Dim dsCommonUI As CommonUIDataSet
        Dim objCommonUIs As New CommonUIs
        Try
            dsCommonUI = objCommonUIs.GetDocumentTemplateByDocumentTemplateID(bv_i32DocumentTemplateID)
            Return dsCommonUI
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            dsCommonUI = Nothing
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#End Region

#Region "Config"
#Region "pub_GetConfigTemplate() TABLE NAME:CONFIG"
    <OperationContract()> _
    Public Function pub_GetConfigByKeyName(ByVal bv_strKeyName As String, ByVal bv_i64DepotId As Int64) As ConfigDataSet

        Try
            Dim intDepotID As Int64 = bv_i64DepotId
            Dim strGetMultiLocationSupport As String = GetMultiLocationSupportConfig()
            If strGetMultiLocationSupport.ToLower = "true" Then
                intDepotID = CLng(GetHeadQuarterID())
            End If
            Dim dsConfigData As ConfigDataSet
            Dim objConfigs As New CommonUIs
            dsConfigData = objConfigs.GetConfigByKeyName(bv_strKeyName, intDepotID)
            Return dsConfigData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetConfigTemplate() TABLE NAME:CONFIG"
    <OperationContract()> _
    Public Function pub_GetConfigTemplate(ByVal bv_i64DepotId As Int64) As ConfigDataSet

        Try
            Dim dsConfigData As ConfigDataSet
            Dim objConfigs As New CommonUIs
            Dim dtConfigData As DataTable
            dsConfigData = objConfigs.GetConfigByDepotId(bv_i64DepotId)
            dtConfigData = objConfigs.GetConfigTemplate().Tables(CommonUIData._CONFIG_TEMPLATE)
            For Each drconfig As DataRow In dsConfigData.Tables(CommonUIData._CONFIG).Rows
                Dim drArr As DataRow() = dtConfigData.Select(CommonUIData.CNFG_TMPLT_ID & "= '" & drconfig.Item(CommonUIData.CNFG_TMPLT_ID).ToString & "'")
                drconfig.Item(CommonUIData.CNFG_TYP) = drArr(0).Item(CommonUIData.CNFG_TYP)
            Next
            For Each drConfig As DataRow In dtConfigData.Rows
                Dim drNewConfig As DataRow = dsConfigData.Tables(CommonUIData._CONFIG).NewRow()
                drNewConfig.Item(CommonUIData.CNFG_TMPLT_ID) = drConfig.Item(CommonUIData.CNFG_TMPLT_ID)
                drNewConfig.Item(CommonUIData.KY_NAM) = drConfig.Item(CommonUIData.KY_NAM)
                drNewConfig.Item(CommonUIData.KY_DSCRPTION) = drConfig.Item(CommonUIData.KY_DSCRPTION)
                drNewConfig.Item(CommonUIData.KY_VL) = drConfig.Item(CommonUIData.KY_VL)
                drNewConfig.Item(CommonUIData.CNFG_TYP) = drConfig.Item(CommonUIData.CNFG_TYP)
                dsConfigData.Tables(CommonUIData._CONFIG).Rows.Add(drNewConfig)
            Next
            dsConfigData.AcceptChanges()
            Return dsConfigData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateConfig() TABLE NAME:Config"
    <OperationContract()> _
    Public Function pub_UpdateConfig(ByRef br_dsConfigDataset As ConfigDataSet) As Boolean
        Dim objtrans As New Transactions
        Try
            Dim objConfig As New CommonUIs
            For Each drConfig As DataRow In br_dsConfigDataset.Tables(CommonUIData._CONFIG).Rows
                Select Case drConfig.RowState
                    Case DataRowState.Modified
                        If IsDBNull(drConfig.Item(CommonUIData.CNFG_ID)) Then
                            Dim lngCreated As Long
                            If CommonUIs.iBool(drConfig.Item(CommonUIData.ENBLD_BT)) Then
                                lngCreated = pvt_CreateConfig(CommonUIs.iLng(drConfig.Item(CommonUIData.CNFG_TMPLT_ID)), drConfig.Item(CommonUIData.KY_NAM).ToString, drConfig.Item(CommonUIData.KY_DSCRPTION).ToString, _
                                                drConfig.Item(CommonUIData.KY_VL).ToString, CommonUIs.iInt(drConfig.Item(CommonUIData.DPT_ID)), CommonUIs.iBool(drConfig.Item(CommonUIData.ENBLD_BT)), True, objtrans)
                                drConfig.Item(CommonUIData.CNFG_ID) = lngCreated
                            End If
                        Else
                            pvt_ModifyConfig(CommonUIs.iLng(drConfig.Item(CommonUIData.CNFG_ID)), CommonUIs.iLng(drConfig.Item(CommonUIData.CNFG_TMPLT_ID)), drConfig.Item(CommonUIData.KY_NAM).ToString, drConfig.Item(CommonUIData.KY_DSCRPTION).ToString, _
                                                drConfig.Item(CommonUIData.KY_VL).ToString, CommonUIs.iInt(drConfig.Item(CommonUIData.DPT_ID)), CommonUIs.iBool(drConfig.Item(CommonUIData.ENBLD_BT)), True, objtrans)
                        End If
                End Select
            Next
            objtrans.commit()
            Return True
        Catch ex As Exception
            objtrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pvt_CreateConfig() TABLE NAME:Config"
    <OperationContract()> _
    Private Function pvt_CreateConfig(ByVal bv_i64ConfigTemplateId As Int64, _
     ByVal bv_strKeyName As String, _
     ByVal bv_strKeyDescription As String, _
     ByVal bv_strKeyValue As String, _
     ByVal bv_i32DepotId As Int32, _
     ByVal bv_blnEnabledBit As Boolean, _
     ByVal bv_blnActiveBit As Boolean, ByRef br_objTrans As Transactions) As Long

        Try
            Dim objConfig As New CommonUIs
            Dim lngCreated As Long
            lngCreated = objConfig.CreateConfig(bv_i64ConfigTemplateId, bv_strKeyName, _
                  bv_strKeyDescription, bv_strKeyValue, _
                  bv_i32DepotId, bv_blnEnabledBit, _
                  bv_blnActiveBit, br_objTrans)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pvt_ModifyConfig() TABLE NAME:Config"
    <OperationContract()> _
    Private Function pvt_ModifyConfig(ByVal bv_i64ConfigId As Int64, _
     ByVal bv_i64ConfigTemplateId As Int64, _
     ByVal bv_strKeyName As String, _
     ByVal bv_strKeyDescription As String, _
     ByVal bv_strKeyValue As String, _
     ByVal bv_i32DepotId As Int32, _
     ByVal bv_blnEnabledBit As Boolean, _
     ByVal bv_blnActiveBit As Boolean, _
     ByRef br_objTrans As Transactions) As Boolean

        Try
            Dim objConfig As New CommonUIs
            Dim blnUpdated As Boolean
            blnUpdated = objConfig.UpdateConfig(bv_i64ConfigId, _
                  bv_i64ConfigTemplateId, bv_strKeyName, _
                  bv_strKeyDescription, bv_strKeyValue, _
                  bv_i32DepotId, bv_blnEnabledBit, _
                  bv_blnActiveBit, br_objTrans)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "DecryptString"
    Private Function DecryptString(Message As String) As String
        Dim Results As Byte()
        Dim UTF8 As New System.Text.UTF8Encoding()
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        Dim pvt_strKeyPhrase As String
        Try
            'pvt_strKeyPhrase = IO.File.GetLastWriteTime(String.Concat(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings("KeyFileName"))).ToString("yyMMddHHHmmss")
            pvt_strKeyPhrase = "IIC"
            Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(pvt_strKeyPhrase))
            TDESAlgorithm.Key = TDESKey
            TDESAlgorithm.Mode = CipherMode.ECB
            TDESAlgorithm.Padding = PaddingMode.PKCS7
            Dim DataToDecrypt As Byte() = Convert.FromBase64String(Message)
            Dim Decryptor As ICryptoTransform = TDESAlgorithm.CreateDecryptor()
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return UTF8.GetString(Results)
    End Function
#End Region


#Region "GetHeadQuarterID"
    Public Function GetHeadQuarterID() As String
        Try
            Dim strHeadQuarterID As String = String.Empty
            Dim dsDepot As New DepotDataSet
            Dim objDepot As New Depot
            Dim drDepot As DataRow()
            dsDepot = objDepot.pub_GetAllDepotDetails()
            drDepot = dsDepot.Tables(DepotData._V_DEPOT).Select(String.Concat(DepotData.ORGNZTN_TYP_CD, " = 'HQ' "))
            If drDepot.Length > 0 Then
                For Each dr As DataRow In drDepot
                    strHeadQuarterID = CStr(dr.Item(DepotData.DPT_ID))
                Next
            End If
            Return strHeadQuarterID
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetMultiLocationSupportConfig"
    Public Function GetMultiLocationSupportConfig() As String
        Try
            Dim strKeyValue As String = ""
            Dim objCommonUIs As New CommonUIs
            Dim dsConfig As New DataSet
            Dim intHQId As Int64 = CLng(GetHeadQuarterID())
            dsConfig = objCommonUIs.GetConfigByKeyName("070", intHQId)
            If dsConfig.Tables(CommonUIData._CONFIG).Rows.Count > 0 Then
                strKeyValue = DecryptString(dsConfig.Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL).ToString)
            End If
            Return strKeyValue
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#End Region

#Region "GetSettings()"
    Public Function GetSettings(ByVal bv_strKeyName As String, ByVal bv_intDepotId As Integer, ByRef br_blnKeyExist As Boolean) As String
        Try
            Dim dsConfiguration As ConfigDataSet
            Dim strKeyValue As String = ""
            dsConfiguration = pub_GetConfigByKeyName(bv_strKeyName, bv_intDepotId)
            If Not dsConfiguration.Tables(CommonUIData._CONFIG).Rows.Count > 0 Then
                br_blnKeyExist = False
            Else
                br_blnKeyExist = True
                strKeyValue = DecryptString(dsConfiguration.Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL).ToString)
            End If
            Return strKeyValue
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_GetMeasureId() TABLE NAME:MEASURE"
    <OperationContract()> _
    Public Function pub_GetMeasureId(ByVal bv_strMsrCode As String, ByVal bv_intDepotID As Integer) As MeasureDataSet
        Try
            Dim dsMeasure As MeasureDataSet
            Dim objConfigs As New CommonUIs
            dsMeasure = objConfigs.pub_GetMeasureId(bv_strMsrCode, bv_intDepotID)
            Return dsMeasure
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEquipmentStatus() TABLE NAME:EQUIPMENT_STATUS"
    <OperationContract()> _
    Public Function pub_GetEquipmentStatus(ByVal bv_strEquipmentStatus As String, ByVal bv_intDepotID As Integer) As CommonUIDataSet
        Try
            Dim dsEqpData As CommonUIDataSet
            Dim objConfigs As New CommonUIs
            dsEqpData = objConfigs.GetEquipmentStatus(bv_strEquipmentStatus, bv_intDepotID)
            Return dsEqpData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEquipmentType() TABLE NAME:EQUIPMENT_TYPE"
    <OperationContract()> _
    Public Function pub_GetEquipmentType(ByVal bv_strEquipmentTypeCode As String, ByVal bv_intDepotID As Integer) As CommonUIDataSet
        Try
            Dim dsEqpData As CommonUIDataSet
            Dim objConfigs As New CommonUIs
            dsEqpData = objConfigs.GetEquipmentType(bv_strEquipmentTypeCode, bv_intDepotID)
            Return dsEqpData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEquipmentCode() TABLE NAME:EQUIPMENT_CODE"
    <OperationContract()> _
    Public Function GetAllEquipmentCode(ByVal bv_intDepotID As Integer) As DataTable
        Try

            Dim objConfigs As New CommonUIs
            Return objConfigs.GetAllEquipmentCode(bv_intDepotID)

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function GetEquipmentCodeByType(ByVal bv_strType As String, ByVal bv_intDepotID As Integer) As DataTable
        Try

            Dim objConfigs As New CommonUIs
            Return objConfigs.GetEquipmentCodeByType(bv_strType, bv_intDepotID)

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function GetEquipmentCodeByTypeID(ByVal bv_strTypeID As String, ByVal bv_intDepotID As Integer) As DataTable
        Try

            Dim objConfigs As New CommonUIs
            Return objConfigs.GetEquipmentCodeByTypeId(bv_strTypeID, bv_intDepotID)

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function GetEquipmentCodeByTypeWithoutCode(ByVal bv_strType As String, ByVal bv_strOldCode As String, ByVal bv_intDepotID As Integer) As DataTable
        Try

            Dim objConfigs As New CommonUIs
            Return objConfigs.GetEquipmentCodeByTypeWithoutCode(bv_strType, bv_strOldCode, bv_intDepotID)

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_GetDepoDetail()"
    <OperationContract()> _
    Public Function pub_GetDepoDetail(ByVal bv_intDepotID As Integer) As CommonUIDataSet
        Try
            Dim dsDepot As CommonUIDataSet
            Dim objConfigs As New CommonUIs
            dsDepot = objConfigs.pub_GetDepotDetail(bv_intDepotID)
            Return dsDepot
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetWorkFlowActivity() TABLE NAME:WF_ACTIVITY"
    <OperationContract()> _
    Public Function pub_GetWorkFlowActivity(ByVal bv_strActivity As String, ByVal bv_ActvBit As Boolean, ByVal bv_intDepotID As Integer) As CommonUIDataSet
        Try
            Dim dsWFActData As CommonUIDataSet
            Dim objConfigs As New CommonUIs
            dsWFActData = objConfigs.GetWorkFlowActivity(bv_strActivity, bv_ActvBit, bv_intDepotID)
            Return dsWFActData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEquipmentPreviousActivityDate() TABLE NAME:TRACKING"
    <OperationContract()> _
    Public Function pub_GetEquipmentPreviousActivityDate(ByVal bv_strEquipmentNo As String, _
                                                         ByVal bv_dtEventDate As DateTime, _
                                                         ByRef bv_dtPreviousDate As DateTime, _
                                                         ByVal bv_strActivityName As String, _
                                                         ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dsWFActData As CommonUIDataSet
            Dim objConfigs As New CommonUIs
            Dim blnDateValid As Boolean = False
            dsWFActData = objConfigs.GetEquipmentPreviousActivityDate(bv_strEquipmentNo, bv_intDepotID, bv_strActivityName)

            If dsWFActData.Tables(CommonUIData._TRACKING).Rows.Count > 0 Then
                With dsWFActData.Tables(CommonUIData._TRACKING).Rows(0)
                    If CDate(.Item(CommonUIData.ACTVTY_DT)) > CDate(bv_dtEventDate) Then
                        bv_dtPreviousDate = CDate(.Item(CommonUIData.ACTVTY_DT))
                        blnDateValid = True
                    End If
                End With
            End If

            Return blnDateValid
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetServicePartnerByCode() TABLE NAME:SERVICE_PARTNER"

    <OperationContract()> _
    Public Function pub_GetServicePartnerByCode(ByVal bv_strCode As String, _
                                                ByRef br_strServiceType As String, _
                                                ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dsCommonUI As CommonUIDataSet
            Dim objCommonUI As New CommonUIs
            dsCommonUI = objCommonUI.GetServicePartnerByCode(bv_strCode, bv_intDepotID)
            If dsCommonUI.Tables(CommonUIData._V_SERVICE_PARTNER).Rows.Count > 0 Then
                br_strServiceType = CStr(dsCommonUI.Tables(CommonUIData._V_SERVICE_PARTNER).Rows(0).Item(CommonUIData.SRVC_PRTNR_TYP_CD))
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetEquipemntActivityReport"
    <OperationContract()> _
    Public Function GetEquipemntActivityReport(ByRef bv_param As String, _
                                               ByVal intDepotID As Integer, _
                                               ByVal bv_ReportName As String) As CommonUIDataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsCommonUIDataSet As New CommonUIDataSet
        Dim dtEquipmentActivity As New DataTable
        Dim dtEqCustomer As New DataTable
        Dim dsAdditionalData As New DataTable
        Dim intDay As Integer = 0
        Dim drActivityDay() As DataRow
        Dim drNewRow As DataRow
        Dim strWhere As String = String.Empty
        Dim dtEquipmentType As New DataTable
        Dim strDepot As String = String.Empty
        Try
            Dim PeriodFromDate As DateTime = Nothing
            Dim PeriodToDate As DateTime = Nothing
            Dim strCustomer As String = pub_GetParameter("Customer", bv_param)
            Dim strEquipmentTyp As String = pub_GetParameter("Equipment Type", bv_param)

            If pub_GetParameter("Period From", bv_param) <> "" Then
                PeriodFromDate = CDate(pub_GetParameter("Period From", bv_param))
            End If
            If pub_GetParameter("Period To", bv_param) <> "" Then
                PeriodToDate = CDate(pub_GetParameter("Period To", bv_param))
            End If

            If IsDate(pub_GetParameter("Period To", bv_param)) AndAlso pub_GetParameter("Period From", bv_param) = "" Then
                PeriodFromDate = PeriodToDate.AddDays(-PeriodToDate.Day + 1)
                PeriodFromDate = CDate(String.Concat("01-", PeriodFromDate.ToString("MMM"), "-", PeriodFromDate.ToString("yyyy")))
            ElseIf IsDate(pub_GetParameter("Period From", bv_param)) AndAlso pub_GetParameter("Period To", bv_param) = "" Then
                PeriodToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            ElseIf pub_GetParameter("Period From", bv_param) = "" AndAlso pub_GetParameter("Period To", bv_param) = "" Then
                PeriodToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
                PeriodFromDate = CDate(String.Concat("01-", PeriodToDate.ToString("MMM"), "-", PeriodToDate.ToString("yyyy")))
            End If
            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strDepot = pub_GetParameter("Depot", bv_param)
            Else                strDepot = CStr(intDepotID)
            End If

            'dsCommonUIDataSet = objCommonUIs.GetEquipmentActivityReport(strCustomer, strEquipmentTyp, PeriodFromDate, PeriodToDate, intDepotID)
            dsCommonUIDataSet = objCommonUIs.GetEquipmentActivityReport(strCustomer, strEquipmentTyp, PeriodFromDate, PeriodToDate, strDepot)

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If
            If strEquipmentTyp <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", strEquipmentTyp, ")", " AND ", CommonUIData.DPT_ID, " IN (", strDepot, ")")
            End If
            Dim dtPreviousDate As Date
            Dim rowCount As Integer = 0
            Dim intLastNumber As Integer = 0
            For Each dr As DataRow In dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVTY_DATE).Rows
                dr.Item("OPENSTOCK") = objCommonUIs.GetOpenStockReportData(strWhere, CDate(dr.Item(CommonUIData.ACTVTY_DT)))
            Next
            'If dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVTY_DATE).Rows.Count = 0 Then
            '    Dim dsDepot As DataSet = pub_GetDepoDetail(strDepot)
            '    Dim drDepot As DataRow = Nothing
            '    drDepot = dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVTY_DATE).NewRow()
            '    drDepot.Item(CommonUIData.DPT_CD) = "DAMM"
            '    dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVTY_DATE).Rows.Add(drDepot)
            'End If
            Dim drDate As DataRow = Nothing
            dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVITY_DATE_DETAIL).Clear()
            If PeriodFromDate <> Nothing AndAlso PeriodToDate <> Nothing Then
                drDate = dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVITY_DATE_DETAIL).NewRow()
                drDate.Item(CommonUIData.PERD_FRM_DT) = PeriodFromDate
                drDate.Item(CommonUIData.PERD_TO_DT) = PeriodToDate
                dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVITY_DATE_DETAIL).Rows.Add(drDate)
            End If
            intDay = CInt(DateDiff(DateInterval.Day, PeriodFromDate, PeriodToDate))
            'For Avoid Empty
            '    If dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVTY_DATE).Rows.Count > 0 Then

            Dim intSlno As Integer = 1
            For i = 0 To intDay
                drActivityDay = dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVTY_DATE).Select(String.Concat(CommonUIData.ACTVTY_DT, "='" & PeriodFromDate & "'"))
                drActivityDay = dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVTY_DATE).Select(String.Concat(CommonUIData.ACTVTY_DT, "='" & PeriodFromDate & "'"))
                drNewRow = dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVTY_DATE).NewRow()
                If drActivityDay.Length = 0 Then
                    drNewRow.Item(CommonUIData.ACTVTY_DT) = CDate(PeriodFromDate)
                    drNewRow.Item("Received") = 0
                    drNewRow.Item("Released") = 0
                    drNewRow.Item("Heated") = 0
                    drNewRow.Item("Cleaned") = 0
                    drNewRow.Item("Inspected") = 0
                    drNewRow.Item("Repaired") = 0
                    drNewRow.Item("LeakTested") = 0
                    drNewRow.Item("EquipmentInspected") = 0
                    drNewRow.Item("On_Off_Hire_Surveyed") = 0
                    drNewRow.Item("TOTAL") = 0
                    drNewRow.Item("OPENSTOCK") = objCommonUIs.GetOpenStockReportData(strWhere, PeriodFromDate)
                    'drNewRow.Item("AVAILABLE") = objCommonUIs.GetAvailableReportData(strWhere, PeriodFromDate)
                    drNewRow.Item("AVAILABLE") = 0
                    dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVTY_DATE).Rows.Add(drNewRow)
                End If
                PeriodFromDate = PeriodFromDate.AddDays(1)
            Next
            For Each dr As DataRow In dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVTY_DATE).Select(String.Concat(CommonUIData.ACTVTY_DT, " IS NOT NULL"), String.Concat(CommonUIData.ACTVTY_DT, " ASC"))
                If strCustomer.Contains(",") Then
                    dr.Item(CommonUIData.CSTMR_CD_DSPLY) = "All Customers"
                Else
                    dr.Item(CommonUIData.CSTMR_CD_DSPLY) = dsCommonUIDataSet.Tables(CommonUIData._V_EQUIPMENT_ACTIVTY_DATE).Rows(0).Item(CommonUIData.CSTMR_CD)
                End If
                If Not IsDBNull(dr.Item(CommonUIData.ACTVTY_DT)) Then
                    If dtPreviousDate = Nothing Then
                        intLastNumber = 1
                        dr.Item(CommonUIData.SLNO) = intLastNumber
                    ElseIf Date.Compare(dtPreviousDate, CDate(dr.Item(CommonUIData.ACTVTY_DT))) = 0 Then
                        dr.Item(CommonUIData.SLNO) = 0
                    ElseIf Date.Compare(dtPreviousDate, CDate(dr.Item(CommonUIData.ACTVTY_DT))) <> 0 Then
                        intLastNumber = intLastNumber + 1
                        dr.Item(CommonUIData.SLNO) = intLastNumber
                    End If
                    dtPreviousDate = CDate(dr.Item(CommonUIData.ACTVTY_DT))
                End If
            Next
            ' End If
            dsCommonUIDataSet.DataSetName = "EquipmentActivityDataSet"
            Return dsCommonUIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "pvt_getSerialNumber"
    <OperationContract()> _
    Private Sub pvt_getSerialNumber(ByVal bv_dtCurrentDate As Date, _
                                    ByVal bv_dtPreviousDate As Date, _
                                    ByVal bv_intCount As Integer, _
                                    ByRef br_rowNumber As Integer)
        Try
            If bv_intCount = 0 Then
                br_rowNumber = 1
            Else

            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
        End Try
    End Sub
#End Region

#Region "Gate Moves Report"
    <OperationContract()> _
    Public Function GetGateMovesReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String, _
                                       ByVal bv_intReportId As Integer) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsGateMoves As New DataSet
        Dim dtGateMoves As New DataTable
        Dim dtGateMovesDetail As New DataTable

        Try
            Dim PeriodFromDate As DateTime = Nothing
            Dim PeriodToDate As DateTime = Nothing
            Dim strCustomer As String = String.Empty
            Dim strMoveType As String = String.Empty
            Dim strEirNo As String = String.Empty
            Dim strPreviousCargo As String = String.Empty
            Dim strEquipmentNo As String = String.Empty
            Dim strEquipmentTyp As String = String.Empty
            Dim strGateInDate As String = String.Empty
            Dim strGateOutDate As String = String.Empty
            '   Dim strWhere As String = String.Concat(" DPT_ID = ", intDepotID)
            Dim strWhere As String
            Dim str_007Value As String = objCommonUIs.DecryptString(CStr(objCommonUIs.GetConfigByKeyName("007", CLng(IIf(objCommonUIs.GetMultiLocationSupportConfig.ToLower = "true", objCommonUIs.GetHeadQuarterID(), intDepotID))).Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL)))
            '  objCommonData.GetConfigSetting("070", False)
            If pub_GetConfigByKey("070", intDepotID) Then
                '    strWhere = String.Concat(" DPT_ID = ", pub_GetParameter("Depot", bv_param))
                strWhere = String.Concat(CommonUIData.DPT_ID, " IN (", pub_GetParameter("Depot", bv_param), ")")
            Else
                strWhere = String.Concat(" DPT_ID = ", intDepotID)
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If
            If bv_intReportId = 111 Then
                If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
                End If
                If pub_GetParameter(str_007Value, bv_param) <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", CommonUIData.GI_RF_NO, " ='", pub_GetParameter(str_007Value, bv_param), "' ")
                End If
            ElseIf bv_intReportId = 162 Then
                If pub_GetParameter(str_007Value, bv_param) <> "" Then
                    strWhere = String.Concat(strWhere, " AND (", CommonUIData.EIR_NO, " ='", pub_GetParameter(str_007Value, bv_param), "')")
                End If
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Period From", bv_param) <> "" Then
                PeriodFromDate = CDate(pub_GetParameter("Period From", bv_param))
            End If
            If pub_GetParameter("Period To", bv_param) <> "" Then
                PeriodToDate = CDate(pub_GetParameter("Period To", bv_param))
            End If

            If IsDate(pub_GetParameter("Period From", bv_param)) AndAlso pub_GetParameter("Period To", bv_param) = "" Then
                PeriodToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Move Type", bv_param).Contains("11") AndAlso Not (pub_GetParameter("Move Type", bv_param).Contains("12")) _
                                AndAlso pub_GetParameter("Period From", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTN_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.GTN_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strGateInDate = String.Concat(CommonUIData.GTN_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strGateOutDate = "0"
            End If

            If pub_GetParameter("Move Type", bv_param).Contains("12") AndAlso Not (pub_GetParameter("Move Type", bv_param).Contains("11")) _
                                AndAlso pub_GetParameter("Period From", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTOT_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.GTOT_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strGateOutDate = String.Concat(CommonUIData.GTOT_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTOT_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strGateInDate = "0"
            End If

            If pub_GetParameter("Move Type", bv_param).Contains("12") AndAlso pub_GetParameter("Move Type", bv_param).Contains("11") _
                                AndAlso pub_GetParameter("Period From", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ((", CommonUIData.GTN_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "') ")
                strWhere = String.Concat(strWhere, " OR (", CommonUIData.GTOT_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTOT_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "')) ")
                strGateInDate = String.Concat(CommonUIData.GTN_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strGateOutDate = String.Concat(CommonUIData.GTOT_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTOT_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            End If

            If pub_GetParameter("Move Type", bv_param).Contains("12") AndAlso Not (pub_GetParameter("Move Type", bv_param).Contains("11")) _
                                AndAlso (pub_GetParameter("Period From", bv_param) = "") AndAlso (pub_GetParameter("Period To", bv_param) = "") Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTOT_DT, " IS NOT NULL")
                strGateOutDate = String.Concat(CommonUIData.GTOT_DT, " IS NOT NULL")
                strGateInDate = "0"
            ElseIf pub_GetParameter("Move Type", bv_param).Contains("11") AndAlso Not (pub_GetParameter("Move Type", bv_param).Contains("12")) _
                                AndAlso (pub_GetParameter("Period From", bv_param) = "") AndAlso (pub_GetParameter("Period To", bv_param) = "") Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTN_DT, " IS NOT NULL ")
                strGateInDate = String.Concat(CommonUIData.GTN_DT, " IS NOT NULL")
                strGateOutDate = "0"
            ElseIf pub_GetParameter("Move Type", bv_param).Contains("11") AndAlso (pub_GetParameter("Move Type", bv_param).Contains("12")) _
                                           AndAlso (pub_GetParameter("Period From", bv_param) = "") AndAlso (pub_GetParameter("Period To", bv_param) = "") Then
                strGateInDate = String.Concat(CommonUIData.GTN_DT, " IS NOT NULL")
                strGateOutDate = String.Concat(CommonUIData.GTOT_DT, " IS NOT NULL")
            End If

            If strGateInDate.ToString.Length > 1 Then
                strGateInDate = String.Concat("(CASE WHEN ", strGateInDate, " THEN 1 ELSE 0 END)")
            End If

            If strGateOutDate.ToString.Length > 1 Then
                strGateOutDate = String.Concat("(CASE WHEN ", strGateOutDate, " THEN 1 ELSE 0 END)")
            End If

            dtGateMoves = objCommonUIs.GetGateMovesReport(strWhere)
            dtGateMovesDetail = objCommonUIs.GetGateMoves_DetailReport(strGateInDate, strGateOutDate, strWhere)
            dtGateMoves.TableName = "V_DAR_ACTIVITY_STATUS"
            dtGateMovesDetail.TableName = "V_DAR_ACTIVITY_STATUS_DETAIL"
            dsGateMoves.Tables.Add(dtGateMovesDetail)
            dsGateMoves.Tables.Add(dtGateMoves)
            Return dsGateMoves
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "Equipment Yard Location Report"
    <OperationContract()> _
    Public Function pub_GetEquipmentYardLocationReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsEqYardLoc As New DataSet
        Dim dtEqYardLoc As New DataTable
        Try
            Dim PeriodFromDate As DateTime = Nothing
            Dim PeriodToDate As DateTime = Nothing
            Dim strCustomer As String = String.Empty
            Dim strMoveType As String = String.Empty
            Dim strEirNo As String = String.Empty
            Dim strPreviousCargo As String = String.Empty
            Dim strEquipmentNo As String = String.Empty
            Dim strEquipmentTyp As String = String.Empty
            Dim strWhere As String = String.Empty
            'Check Multiple Depot
            If pub_GetParameter("Depot", bv_param) = "" Then
                'strDepotId = CStr(intDepotID)
                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID, " AND ", CommonUIData.YRD_LCTN, " IS NOT NULL AND ACTV_BT = 1 ")
            Else
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ") AND ", CommonUIData.YRD_LCTN, " IS NOT NULL AND ACTV_BT = 1 ")
            End If

            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Yard Location", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.YRD_LCTN, " ='", pub_GetParameter("Yard Location", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Period From", bv_param) <> "" Then
                PeriodFromDate = CDate(pub_GetParameter("Period From", bv_param))
            End If
            If pub_GetParameter("Period To", bv_param) <> "" Then
                PeriodToDate = CDate(pub_GetParameter("Period To", bv_param))
            End If
            If pub_GetParameter("EIR No", bv_param) <> "" Then
                PeriodToDate = CDate(pub_GetParameter("EIR No", bv_param))
                strWhere = String.Concat(strWhere, " AND ", pub_GetParameter("EIR No", bv_param))
            End If
            If pub_GetParameter("Period From", bv_param) <> "" AndAlso pub_GetParameter("Period To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTN_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.GTN_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf pub_GetParameter("Period From", bv_param) <> "" AndAlso pub_GetParameter("Period To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Period From", bv_param) = "" AndAlso pub_GetParameter("Period To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtEqYardLoc = objCommonUIs.GetEquipmentYardLocationReport(strWhere)
            dtEqYardLoc.TableName = "V_ACTIVITY_STATUS"
            dsEqYardLoc.Tables.Add(dtEqYardLoc)
            Return dsEqYardLoc
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "Tank Test Detail Report"
    <OperationContract()> _
    Public Function pub_GetTankTestDetailReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsTankTest As New DataSet
        Dim dtTankTest As New DataTable
        Try
            'For Multilocation
            Dim strWhere As String = String.Empty
            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If


            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Test Status", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.TEST_STATUS_ID, " IN (", pub_GetParameter("Test Status", bv_param), ") ")
            End If

            If pub_GetParameter("Test Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.NXT_TST_TYP_ID, " IN (", pub_GetParameter("Test Type", bv_param), ") ")
            End If

            dtTankTest = objCommonUIs.GetTankTestDetailReport(strWhere)
            dtTankTest.TableName = "VM_TANK_TEST_DETAIL"
            dsTankTest.Tables.Add(dtTankTest)

            Return dsTankTest
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "Heating Report"
    <OperationContract()> _
    Public Function pub_GetHeatingReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsHeating As New DataSet
        Dim dtHeating As New DataTable
        Try
            Dim IndatFromDate As Date = Nothing
            Dim IndatToDate As Date = Nothing
            Dim HeatingStartFromDate As Date = Nothing
            Dim HeatingStartToDate As Date = Nothing
            Dim HeatingEndFromDate As Date = Nothing
            Dim HeatingEndToDate As Date = Nothing
            Dim strWhere As String = String.Empty
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ") ")
            Else
                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If


            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            End If
            If pub_GetParameter("In Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("In Date To", bv_param)) AndAlso pub_GetParameter("In Date From", bv_param) = "" Then
                IndatFromDate = IndatToDate.AddDays(-IndatToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("In Date From", bv_param)) AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                IndatToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Heating Start Date From", bv_param) <> "" Then
                HeatingStartFromDate = CDate(pub_GetParameter("Heating Start Date From", bv_param))
            End If
            If pub_GetParameter("Heating Start Date To", bv_param) <> "" Then
                HeatingStartToDate = CDate(pub_GetParameter("Heating Start Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Heating Start Date To", bv_param)) AndAlso pub_GetParameter("Heating Start Date From", bv_param) = "" Then
                HeatingStartFromDate = HeatingStartToDate.AddDays(-HeatingStartToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("Heating Start Date From", bv_param)) AndAlso pub_GetParameter("Heating Start Date To", bv_param) = "" Then
                HeatingStartToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Heating Start Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating Start Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.HTNG_STRT_DT, " >= '", HeatingStartFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.HTNG_STRT_DT, " <= '", HeatingStartToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Heating Start Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating Start Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.HTNG_STRT_DT, " >= '", HeatingStartFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Heating Start Date From", bv_param) = "" AndAlso pub_GetParameter("Heating Start Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.HTNG_STRT_DT, " <= '", HeatingStartToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Heating End Date From", bv_param) <> "" Then
                HeatingEndFromDate = CDate(pub_GetParameter("Heating End Date From", bv_param))
            End If
            If pub_GetParameter("Heating End Date To", bv_param) <> "" Then
                HeatingEndToDate = CDate(pub_GetParameter("Heating End Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Heating End Date To", bv_param)) AndAlso pub_GetParameter("Heating End Date From", bv_param) = "" Then
                HeatingEndFromDate = HeatingEndToDate.AddDays(-HeatingEndToDate.Day + 1)
            ElseIf IsDate(pub_GetParameter("Heating End Date From", bv_param)) AndAlso pub_GetParameter("Heating End Date To", bv_param) = "" Then
                HeatingEndToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Heating End Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.HTNG_END_DT, " >= '", HeatingEndFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.HTNG_END_DT, " <= '", HeatingEndToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Heating End Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating End Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.HTNG_END_DT, " >= '", HeatingEndFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Heating End Date From", bv_param) = "" AndAlso pub_GetParameter("Heating End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.HTNG_END_DT, " <= '", HeatingEndToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtHeating = objCommonUIs.GetHeatingReport(strWhere)
            dtHeating.TableName = "VM_HEATING"
            dsHeating.Tables.Add(dtHeating)
            Return dsHeating
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "Finance Heating"
    <OperationContract()> _
    Public Function pub_GetHeatingRevenueReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsRevenueHeating As New DataSet
        Dim dtRevenueHeating As New DataTable
        Try
            Dim IndatFromDate As Date = Nothing
            Dim IndatToDate As Date = Nothing
            Dim HeatingStartFromDate As Date = Nothing
            Dim HeatingStartToDate As Date = Nothing
            Dim HeatingEndFromDate As Date = Nothing
            Dim HeatingEndToDate As Date = Nothing
            Dim strWhere As String = String.Empty

            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If

            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If
            If pub_GetParameter("Billed", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND BILLED_ID IN (", pub_GetParameter("Billed", bv_param), ") ")
            End If
            'If pub_GetParameter("In Date From", bv_param) <> "" Then
            '    IndatFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            'End If
            'If pub_GetParameter("In Date To", bv_param) <> "" Then
            '    IndatToDate = CDate(pub_GetParameter("In Date To", bv_param))
            'End If

            'If IsDate(pub_GetParameter("In Date To", bv_param)) AndAlso pub_GetParameter("In Date From", bv_param) = "" Then
            '    IndatFromDate = IndatToDate.AddDays(-IndatToDate.Day + 1)
            'ElseIf IsDate(pub_GetParameter("In Date From", bv_param)) AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
            '    IndatToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            'End If

            'If pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
            '    strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            'End If

            If pub_GetParameter("In Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            End If
            If pub_GetParameter("In Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) = "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            'If pub_GetParameter("Heating Start Date From", bv_param) <> "" Then
            '    HeatingStartFromDate = CDate(pub_GetParameter("Heating Start Date From", bv_param))
            'End If
            'If pub_GetParameter("Heating Start Date To", bv_param) <> "" Then
            '    HeatingStartToDate = CDate(pub_GetParameter("Heating Start Date To", bv_param))
            'End If

            'If IsDate(pub_GetParameter("Heating Start Date To", bv_param)) AndAlso pub_GetParameter("Heating Start Date From", bv_param) = "" Then
            '    HeatingStartFromDate = HeatingStartToDate.AddDays(-HeatingStartToDate.Day + 1)
            'ElseIf IsDate(pub_GetParameter("Heating Start Date From", bv_param)) AndAlso pub_GetParameter("Heating Start Date To", bv_param) = "" Then
            '    HeatingStartToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            'End If

            'If pub_GetParameter("Heating Start Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating Start Date To", bv_param) <> "" Then
            '    strWhere = String.Concat(strWhere, " AND (", CommonUIData.HTNG_STRT_DT, " >= '", HeatingStartFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.HTNG_STRT_DT, " <= '", HeatingStartToDate.ToString("dd-MMM-yyyy"), "') ")
            'End If

            If pub_GetParameter("Heating Start Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("Heating Start Date From", bv_param))
            End If
            If pub_GetParameter("Heating Start Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("Heating Start Date To", bv_param))
            End If

            If pub_GetParameter("Heating Start Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating Start Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Heating Start Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating Start Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Heating Start Date From", bv_param) = "" AndAlso pub_GetParameter("Heating Start Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            'If pub_GetParameter("Heating End Date From", bv_param) <> "" Then
            '    HeatingEndFromDate = CDate(pub_GetParameter("Heating End Date From", bv_param))
            'End If
            'If pub_GetParameter("Heating End Date To", bv_param) <> "" Then
            '    HeatingEndToDate = CDate(pub_GetParameter("Heating End Date To", bv_param))
            'End If

            'If IsDate(pub_GetParameter("Heating End Date To", bv_param)) AndAlso pub_GetParameter("Heating End Date From", bv_param) = "" Then
            '    HeatingEndFromDate = HeatingEndToDate.AddDays(-HeatingEndToDate.Day + 1)
            'ElseIf IsDate(pub_GetParameter("Heating End Date From", bv_param)) AndAlso pub_GetParameter("Heating End Date To", bv_param) = "" Then
            '    HeatingEndToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            'End If

            'If pub_GetParameter("Heating End Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating End Date To", bv_param) <> "" Then
            '    strWhere = String.Concat(strWhere, " AND (", CommonUIData.HTNG_END_DT, " >= '", HeatingEndFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.HTNG_END_DT, " <= '", HeatingEndToDate.ToString("dd-MMM-yyyy"), "') ")
            'End If
            If pub_GetParameter("Heating End Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("Heating End Date From", bv_param))
            End If
            If pub_GetParameter("Heating End Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("Heating End Date To", bv_param))
            End If

            If pub_GetParameter("Heating End Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Heating End Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating End Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Heating End Date From", bv_param) = "" AndAlso pub_GetParameter("Heating End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtRevenueHeating = objCommonUIs.GetRevenueHeatingReport(strWhere)

            Dim dtDistinctCustomer As New DataTable
            Dim dsReport As New DataSet
            Dim strCustomer As String
            Dim i As Integer = 0
            dtDistinctCustomer = objCommonUIs.GetFinanceDistinctCustomer(String.Concat("VM_FINANCE_HEATING ", strWhere))
            If dtDistinctCustomer.Rows.Count > 0 Then

                For Each dr As DataRow In dtDistinctCustomer.Rows
                    i = i + 1
                    If i = dtDistinctCustomer.Rows.Count Then
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")))
                    Else
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")), "/")
                    End If
                Next
            End If

            If Not dtRevenueHeating.Columns.Contains("DepotString") Then
                dtRevenueHeating.Columns.Add("DepotString", GetType(System.String))
            End If

            For Each dr As DataRow In dtRevenueHeating.Rows
                dr.Item("DepotString") = strCustomer
            Next

            dtRevenueHeating.TableName = "VM_FINANCE_HEATING"
            dsRevenueHeating.Tables.Add(dtRevenueHeating)
            Return dsRevenueHeating
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "Finance Cleaning"
    <OperationContract()> _
    Public Function pub_GetCleaningRevenueReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsRevenueCleaning As New DataSet
        Dim dtRevenueCleaning As New DataTable
        Try
            Dim IndatFromDate As Date = Nothing
            Dim IndatToDate As Date = Nothing
            Dim CleaningFromDate As Date = Nothing
            Dim CleaningToDate As Date = Nothing
            Dim InspectionFromDate As Date = Nothing
            Dim InspectionToDate As Date = Nothing
            Dim strWhere As String = String.Empty
            Dim str_073Key As String
            str_073Key = objCommonUIs.DecryptString(CStr(objCommonUIs.GetConfigByKeyName("073", CLng(objCommonUIs.GetHeadQuarterID())).Tables(ConfigData._CONFIG).Rows(0).Item(ConfigData.KY_VL)))

         
            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If

           

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            End If
            If pub_GetParameter("In Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) = "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Cleaning From", bv_param) <> "" Then
                CleaningFromDate = CDate(pub_GetParameter("Cleaning From", bv_param))
            End If
            If pub_GetParameter("Cleaning To", bv_param) <> "" Then
                CleaningToDate = CDate(pub_GetParameter("Cleaning To", bv_param))
            End If

            If pub_GetParameter("Cleaning From", bv_param) <> "" AndAlso pub_GetParameter("Cleaning To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.ORGNL_CLNNG_DT, " >= '", CleaningFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.ORGNL_CLNNG_DT, " <= '", CleaningToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Cleaning From", bv_param) <> "" AndAlso pub_GetParameter("Cleaning To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.ORGNL_CLNNG_DT, " >= '", CleaningFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Cleaning From", bv_param) = "" AndAlso pub_GetParameter("Cleaning To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.ORGNL_CLNNG_DT, " <= '", CleaningToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Inspection From", bv_param) <> "" Then
                InspectionFromDate = CDate(pub_GetParameter("Inspection From", bv_param))
            End If
            If pub_GetParameter("Inspection To", bv_param) <> "" Then
                InspectionToDate = CDate(pub_GetParameter("Inspection To", bv_param))
            End If

            If pub_GetParameter("Inspection From", bv_param) <> "" AndAlso pub_GetParameter("Inspection To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.ORGNL_INSPCTD_DT, " >= '", InspectionFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.ORGNL_INSPCTD_DT, " <= '", InspectionToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Inspection From", bv_param) <> "" AndAlso pub_GetParameter("Inspection To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.ORGNL_INSPCTD_DT, " >= '", InspectionFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Inspection From", bv_param) = "" AndAlso pub_GetParameter("Inspection To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.ORGNL_INSPCTD_DT, " <= '", InspectionToDate.ToString("dd-MMM-yyyy"), "') ")
            End If
            'Including a variable to hold the value of parameters without Depot ID for fetching the count across all depots
            Dim strWhereWithoutDepotID As String = strWhere
            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(strWhere, " AND DPT_ID = ", intDepotID)
            End If

            If str_073Key.ToLower = "true" Then
                If pub_GetParameter("Depot", bv_param) <> "" Then
                    Dim strDepotId() As String = pub_GetParameter("Depot", bv_param).Split(CChar(","))
                    Dim strInvoiceTo As String = pub_GetParameter("Invoice To", bv_param)
                    Dim strCstmr() As String = pub_GetParameter("Invoice To", bv_param).Split(CChar(","))
                    Dim dtTempRevenueCleaning As DataTable = dtRevenueCleaning.Clone()
                    Dim strTempWhere As String = strWhere.Replace("WHERE", " AND ")
                    For intDepotCount As Integer = 0 To strDepotId.Length - 1
                        For intCustId As Integer = 0 To strCstmr.Length - 1
                            Dim strCustomerType As String = CStr(objCommonUIs.GetServicePartnerByID(CInt(strCstmr(intCustId).Replace("'", "")), CInt(objCommonUIs.GetHeadQuarterID())).Rows(0).Item(CommonUIData.SRVC_PRTNR_TYP_CD))
                            Dim dtActivityStatus As New DataTable
                            Dim objInvoiceGenerations As New InvoiceGenerations
                            If pub_GetParameter("Inspection From", bv_param) = "" Then
                                If strCustomerType.ToLower = "party" Then
                                    dtActivityStatus = objInvoiceGenerations.GetCleaningChargeByInvoicingParty(CLng(strDepotId(intDepotCount).Replace("'", "")), CLng(strCstmr(intCustId).Replace("'", "")), " ORDER BY ORGNL_INSPCTD_DT ASC  ")
                                    If dtActivityStatus.Rows.Count > 0 AndAlso Not IsDBNull(dtActivityStatus.Rows(0).Item(InvoiceGenerationData.ORGNL_INSPCTD_DT)) Then
                                        InspectionFromDate = CDate(dtActivityStatus.Rows(0).Item(InvoiceGenerationData.ORGNL_INSPCTD_DT))
                                    End If
                                Else
                                    dtActivityStatus = objInvoiceGenerations.GetActivityStatusByCustomer(CLng(strDepotId(intDepotCount).Replace("'", "")), CLng(strCstmr(intCustId).Replace("'", "")), " AND INSPCTN_DT IS NOT NULL ORDER BY INSPCTN_DT ASC  ")
                                    If dtActivityStatus.Rows.Count > 0 AndAlso Not IsDBNull(dtActivityStatus.Rows(0).Item(CommonUIData.INSPCTN_DT)) Then
                                        InspectionFromDate = CDate(dtActivityStatus.Rows(0).Item(CommonUIData.INSPCTN_DT))
                                    End If
                                End If
                                If InspectionFromDate = Nothing Then
                                    InspectionFromDate = Now
                                End If
                            End If

                            If pub_GetParameter("Inspection To", bv_param) <> "" Then
                                InspectionToDate = CDate(pub_GetParameter("Inspection To", bv_param))
                            Else
                                InspectionToDate = Now
                            End If
                            pub_CalculateCleaningSlabRevenue(dtTempRevenueCleaning, CDate(InspectionFromDate.ToString("dd-MMM-yyyy")), CDate(InspectionToDate.ToString("dd-MMM-yyyy")), CInt(strCstmr(intCustId).Replace("'", "")), strCustomerType, CInt(strDepotId(intDepotCount).Replace("'", "")), strTempWhere, strWhereWithoutDepotID)
                            dtRevenueCleaning.Merge(dtTempRevenueCleaning)
                            dtRevenueCleaning.AcceptChanges()
                            dtTempRevenueCleaning.Clear()
                        Next
                        dtTempRevenueCleaning.Clear()
                    Next
                    If strInvoiceTo <> "" Then
                        strWhere = String.Concat(strWhere, " AND ", CommonUIData.SRVC_PRTNR_ID, " IN (", strInvoiceTo, ")")
                    End If
                    dtTempRevenueCleaning = objCommonUIs.GetFinanceCleaningReportbilledSlabRate(dtTempRevenueCleaning, strWhere)
                    dtRevenueCleaning.Merge(dtTempRevenueCleaning)
                End If
            Else
                If pub_GetParameter("Invoicing To", bv_param) <> "" Then
                    strWhere = String.Concat(strWhere, " AND ", CommonUIData.SRVC_PRTNR_ID, " IN (", pub_GetParameter("Invoicing To", bv_param), ")")
                End If

                dtRevenueCleaning = objCommonUIs.GetFinanceCleaningReport(strWhere)
            End If



            If pub_GetParameter("Billed", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND BILLED_ID IN (", pub_GetParameter("Billed", bv_param), ") ")
            End If

            Dim dtDistinctCustomer As New DataTable
            Dim dsReport As New DataSet
            Dim strCustomer As String
            Dim i As Integer = 0
            dtDistinctCustomer = objCommonUIs.GetFinanceDistinctCustomer(String.Concat("VM_FINANCE_CLEANING ", strWhere))
            If dtDistinctCustomer.Rows.Count > 0 Then

                For Each dr As DataRow In dtDistinctCustomer.Rows
                    i = i + 1
                    If i = dtDistinctCustomer.Rows.Count Then
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")))
                    Else
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")), "/")
                    End If
                Next
            End If

            If Not dtRevenueCleaning.Columns.Contains("DepotString") Then
                dtRevenueCleaning.Columns.Add("DepotString", GetType(System.String))
            End If

            For Each dr As DataRow In dtRevenueCleaning.Rows
                dr.Item("DepotString") = strCustomer
            Next
            dtRevenueCleaning.TableName = "VM_FINANCE_CLEANING"

            'Disable Unbilled Amount for equipments, which are not inspected
            If str_073Key.ToLower = "false" Then
                For Each dr As DataRow In dtRevenueCleaning.Select("ORGNL_INSPCTD_DT IS NULL")
                    dr.Item("UNBILLED_DPT_AMNT") = "0.00"
                    dr.Item("DPT_AMNT") = "0.00"
                Next
            End If

            dsRevenueCleaning.Tables.Add(dtRevenueCleaning)
            Return dsRevenueCleaning
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "Inspection Revenue"

    Public Function pub_GetInspectionRevenueReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsRevenueInspection As New DataSet
        Dim dtRevenueInspection As New DataTable
        Try
            Dim IndatFromDate As Date = Nothing
            Dim IndatToDate As Date = Nothing
            Dim CleaningFromDate As Date = Nothing
            Dim CleaningToDate As Date = Nothing
            Dim InspectionFromDate As Date = Nothing
            Dim InspectionToDate As Date = Nothing
            Dim strWhere As String = String.Empty
            'String.Concat(" AND DPT_ID = ", intDepotID)

            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" AND DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" AND DPT_ID = ", intDepotID)
            End If

            If pub_GetParameter("Customer/Agent", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer/Agent", bv_param), ") OR ", CommonUIData.AGNT_ID, " IN (", pub_GetParameter("Customer/Agent", bv_param), " ))")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Invoice Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("Invoice Date From", bv_param))
            End If
            If pub_GetParameter("Invoice Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("Invoice Date To", bv_param))
            End If

            If pub_GetParameter("Invoice Date From", bv_param) <> "" AndAlso pub_GetParameter("Invoice Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.TO_BLLNG_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.TO_BLLNG_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Invoice Date From", bv_param) <> "" AndAlso pub_GetParameter("Invoice Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.TO_BLLNG_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Invoice Date From", bv_param) = "" AndAlso pub_GetParameter("Invoice Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.TO_BLLNG_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtRevenueInspection = objCommonUIs.GetFinanceInspectionReport(strWhere)
            dtRevenueInspection.TableName = "VM_FINANCE_INSPECTION"
            dsRevenueInspection.Tables.Add(dtRevenueInspection)
            Return dsRevenueInspection
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region
#Region "Finance Repair"
    <OperationContract()> _
    Public Function pub_GetRepairRevenueReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsRevenueRepair As New DataSet
        Dim dtRevenueRepair As New DataTable
        Try
            Dim strWhere As String = String.Empty
            Dim IndatFromDate As Date = Nothing
            Dim IndatToDate As Date = Nothing
            Dim EstimatedatFromDate As Date = Nothing
            Dim EstimatedatToDate As Date = Nothing
            Dim ApprovaldatFromDate As Date = Nothing
            Dim ApprovaldatToDate As Date = Nothing
            Dim CompletiondatFromDate As Date = Nothing
            Dim CompletiondatToDate As Date = Nothing

            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If

            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Customer/Agent", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND  (", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer/Agent", bv_param), ") OR ", CommonUIData.AGNT_ID, " IN (", pub_GetParameter("Customer/Agent", bv_param), " ))")
            End If

            If pub_GetParameter("Invoicing To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.SRVC_PRTNR_ID, " IN (", pub_GetParameter("Invoicing To", bv_param), ")")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Billed", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND BILLED_ID IN (", pub_GetParameter("Billed", bv_param), ") ")
            End If

            Dim decRepairCost As Decimal = Nothing
            If pub_GetParameter("Repair Cost Limit", bv_param) <> "" AndAlso IsNumeric(pub_GetParameter("Repair Cost Limit", bv_param)) Then
                decRepairCost = CDec(pub_GetParameter("Repair Cost Limit", bv_param))
            Else
                decRepairCost = 0
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            End If
            If pub_GetParameter("In Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) = "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Estimate Date From", bv_param) <> "" Then
                EstimatedatFromDate = CDate(pub_GetParameter("Estimate Date From", bv_param))
            End If
            If pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                EstimatedatToDate = CDate(pub_GetParameter("Estimate Date To", bv_param))
            End If

            If pub_GetParameter("Estimate Date From", bv_param) <> "" AndAlso pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_ESTMT_DT, " >= '", EstimatedatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.RPR_ESTMT_DT, " <= '", EstimatedatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Estimate Date From", bv_param) <> "" AndAlso pub_GetParameter("Estimate Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_ESTMT_DT, " >= '", EstimatedatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Estimate Date From", bv_param) = "" AndAlso pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_ESTMT_DT, " <= '", EstimatedatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Approval Date From", bv_param) <> "" Then
                ApprovaldatFromDate = CDate(pub_GetParameter("Approval Date From", bv_param))
            End If
            If pub_GetParameter("Approval Date To", bv_param) <> "" Then
                ApprovaldatToDate = CDate(pub_GetParameter("Approval Date To", bv_param))
            End If

            If pub_GetParameter("Approval Date From", bv_param) <> "" AndAlso pub_GetParameter("Approval Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_APPRVL_DT, " >= '", ApprovaldatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.RPR_APPRVL_DT, " <= '", ApprovaldatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Approval Date From", bv_param) <> "" AndAlso pub_GetParameter("Approval Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_APPRVL_DT, " >= '", ApprovaldatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Approval Date From", bv_param) = "" AndAlso pub_GetParameter("Approval Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_APPRVL_DT, " <= '", ApprovaldatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Completion Date From", bv_param) <> "" Then
                CompletiondatFromDate = CDate(pub_GetParameter("Completion Date From", bv_param))
            End If
            If pub_GetParameter("Completion Date To", bv_param) <> "" Then
                CompletiondatToDate = CDate(pub_GetParameter("Completion Date To", bv_param))
            End If

            If pub_GetParameter("Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Completion Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_CMPLTN_DT, " >= '", CompletiondatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.RPR_CMPLTN_DT, " <= '", CompletiondatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Completion Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_CMPLTN_DT, " >= '", CompletiondatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Completion Date From", bv_param) = "" AndAlso pub_GetParameter("Completion Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_CMPLTN_DT, " <= '", CompletiondatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtRevenueRepair = objCommonUIs.GetFinanceRepairReport(strWhere, decRepairCost)
            Dim dtDistinctCustomer As New DataTable
            Dim dsReport As New DataSet
            Dim strCustomer As String
            Dim i As Integer = 0
            dtDistinctCustomer = objCommonUIs.GetFinanceDistinctCustomer(String.Concat("VM_FINANCE_REPAIR ", strWhere))
            If dtDistinctCustomer.Rows.Count > 0 Then

                For Each dr As DataRow In dtDistinctCustomer.Rows
                    i = i + 1
                    If i = dtDistinctCustomer.Rows.Count Then
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")))
                    Else
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")), "/")
                    End If
                Next
            End If

            If Not dtRevenueRepair.Columns.Contains("DepotString") Then
                dtRevenueRepair.Columns.Add("DepotString", GetType(System.String))
            End If

            For Each dr As DataRow In dtRevenueRepair.Rows
                dr.Item("DepotString") = strCustomer
            Next
            dtRevenueRepair.TableName = "VM_FINANCE_REPAIR"
            If Not (dtRevenueRepair.Columns.Contains("REPAIR_COST")) Then
                dtRevenueRepair.Columns.Add("REPAIR_COST", GetType(System.Decimal))
            End If
            If dtRevenueRepair.Rows.Count > 0 Then
                dtRevenueRepair.Rows(0).Item("REPAIR_COST") = decRepairCost
            End If
            dsRevenueRepair.Tables.Add(dtRevenueRepair)
            Return dsRevenueRepair
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "Cleaning Activity Report"
    <OperationContract()> _
    Public Function pub_GetCleaningActivityReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsTankTest As New DataSet
        Dim dtTankTest As New DataTable
        Try
            Dim InDateFrom As DateTime = Nothing
            Dim InDateTo As DateTime = Nothing
            Dim CleaningDateFrom As DateTime = Nothing
            Dim CleaningDateTo As DateTime = Nothing
            Dim InsDateFrom As DateTime = Nothing
            Dim InsDateTo As DateTime = Nothing
            Dim strWhere As String = String.Empty
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" DPT_ID  IN (", pub_GetParameter("Depot", bv_param), ")")
            Else
                strWhere = String.Concat(" DPT_ID = ", intDepotID)
            End If
            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" Then
                InDateFrom = CDate(pub_GetParameter("In Date From", bv_param))
            End If

            If pub_GetParameter("In Date To", bv_param) <> "" Then
                InDateTo = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If pub_GetParameter("Cleaning Date From", bv_param) <> "" Then
                CleaningDateFrom = CDate(pub_GetParameter("Cleaning Date From", bv_param))
            End If

            If pub_GetParameter("Cleaning Date To", bv_param) <> "" Then
                CleaningDateTo = CDate(pub_GetParameter("Cleaning Date To", bv_param))
            End If

            If pub_GetParameter("Inspection Date From", bv_param) <> "" Then
                InsDateFrom = CDate(pub_GetParameter("Inspection Date From", bv_param))
            End If

            If pub_GetParameter("Inspection Date To", bv_param) <> "" Then
                InsDateTo = CDate(pub_GetParameter("Inspection Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("In Date From", bv_param)) AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                InDateTo = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If
            If IsDate(pub_GetParameter("Cleaning Date From", bv_param)) AndAlso pub_GetParameter("Cleaning Date To", bv_param) = "" Then
                CleaningDateTo = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If
            If IsDate(pub_GetParameter("Inspection Date From", bv_param)) AndAlso pub_GetParameter("Inspection Date To", bv_param) = "" Then
                InsDateTo = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTN_DT, " >= ('", InDateFrom.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.GTN_DT, " <= '", InDateTo.ToString("dd-MMM-yyyy"), "' ")
            ElseIf pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", InDateFrom.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) = "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " <= '", InDateTo.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Cleaning Date From", bv_param) <> "" AndAlso pub_GetParameter("Cleaning Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CLNNG_DT, " >= ('", CleaningDateFrom.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.CLNNG_DT, " <= '", CleaningDateTo.ToString("dd-MMM-yyyy"), "' ")
            ElseIf pub_GetParameter("Cleaning Date From", bv_param) <> "" AndAlso pub_GetParameter("Cleaning Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.CLNNG_DT, " >= '", CleaningDateFrom.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Cleaning Date From", bv_param) = "" AndAlso pub_GetParameter("Cleaning Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.CLNNG_DT, " <= '", CleaningDateTo.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Inspection Date From", bv_param) <> "" AndAlso pub_GetParameter("Inspection Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.INSPCTN_DT, " >= ('", InsDateFrom.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.INSPCTN_DT, " <= '", InsDateTo.ToString("dd-MMM-yyyy"), "' ")
            ElseIf pub_GetParameter("Inspection Date From", bv_param) <> "" AndAlso pub_GetParameter("Inspection Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.INSPCTN_DT, " >= '", CleaningDateFrom.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Inspection Date From", bv_param) = "" AndAlso pub_GetParameter("Inspection Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.INSPCTN_DT, " <= '", CleaningDateTo.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtTankTest = objCommonUIs.GetDAR_ACTIVITY_STATUS_CleaningActivity(strWhere)
            dtTankTest.TableName = "V_DAR_ACTIVITY_STATUS"
            dsTankTest.Tables.Add(dtTankTest)
            Return dsTankTest
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "Finance Invoice Register"
    <OperationContract()> _
    Public Function pub_GetInvoiceRegisterReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsRevenueInvoiceRegister As New DataSet
        Dim dtRevenueInvoiceRegister As New DataTable
        Try
            Dim PeriodFromDate As DateTime = Nothing
            Dim PeriodToDate As DateTime = Nothing
            Dim strWhere As String = String.Empty

            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If

            If pub_GetParameter("Activity Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ACTVTY_TYP_ID IN (", pub_GetParameter("Activity Type", bv_param), ") ")
            End If

            If pub_GetParameter("Invoicing To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.SRVC_PRTNR_ID, " IN (", pub_GetParameter("Invoicing To", bv_param), ")")
            End If

            If pub_GetParameter("Invoice Date From", bv_param) <> "" Then
                PeriodFromDate = CDate(pub_GetParameter("Invoice Date From", bv_param))
            End If
            If pub_GetParameter("Invoice Date To", bv_param) <> "" Then
                PeriodToDate = CDate(pub_GetParameter("Invoice Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Invoice Date From", bv_param)) AndAlso IsDate(pub_GetParameter("Invoice Date To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.TO_BLLNG_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf Not IsDate(pub_GetParameter("Invoice Date From", bv_param)) AndAlso IsDate(pub_GetParameter("Invoice Date To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf IsDate(pub_GetParameter("Invoice Date From", bv_param)) AndAlso Not IsDate(pub_GetParameter("Invoice Date To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' ")
            End If

            dtRevenueInvoiceRegister = objCommonUIs.GetFinanceInvoiceRegisterReport(strWhere)
            Dim dtDistinctCustomer As New DataTable
            Dim dsReport As New DataSet
            Dim strCustomer As String
            Dim i As Integer = 0
            dtDistinctCustomer = objCommonUIs.GetFinanceDistinctCustomer(String.Concat("VM_INVOICE_REGISTER ", strWhere))
            If dtDistinctCustomer.Rows.Count > 0 Then

                For Each dr As DataRow In dtDistinctCustomer.Rows
                    i = i + 1
                    If i = dtDistinctCustomer.Rows.Count Then
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")))
                    Else
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")), "/")
                    End If
                Next
            End If

            If Not dtRevenueInvoiceRegister.Columns.Contains("DepotString") Then
                dtRevenueInvoiceRegister.Columns.Add("DepotString", GetType(System.String))
            End If

            For Each dr As DataRow In dtRevenueInvoiceRegister.Rows
                dr.Item("DepotString") = strCustomer
            Next
            dtRevenueInvoiceRegister.TableName = "VM_INVOICE_REGISTER"
            dsRevenueInvoiceRegister.Tables.Add(dtRevenueInvoiceRegister)
            Return dsRevenueInvoiceRegister
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "Equipment Repair Status"
    <OperationContract()> _
    Public Function pub_GetEquipmentRepairStatusReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsRepairStatus As New DataSet
        Dim dtRepairStatus As New DataTable
        Try
            Dim PeriodFromDate As DateTime = Nothing
            Dim PeriodToDate As DateTime = Nothing
            Dim strWhere As String = String.Empty
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If
            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Repair Type", bv_param) <> "" AndAlso pub_GetParameter("Repair Type", bv_param).Contains(CChar(",")) AndAlso pub_GetParameter("Repair Type", bv_param).Split(CChar(",")).Length = objCommonUIs.GetCountRepairType() Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_TYP_ID, " IN (", pub_GetParameter("Repair Type", bv_param), ") OR (", CommonUIData.RPR_TYP_ID, " IS NULL))")
            ElseIf pub_GetParameter("Repair Type", bv_param) <> "" AndAlso Not (pub_GetParameter("Repair Type", bv_param).Contains(CChar(","))) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RPR_TYP_ID, " IN (", pub_GetParameter("Repair Type", bv_param), ") ")
            End If

            If pub_GetParameter("Estimate Date From", bv_param) <> "" Then
                PeriodFromDate = CDate(pub_GetParameter("Estimate Date From", bv_param))
            End If
            If pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                PeriodToDate = CDate(pub_GetParameter("Estimate Date To", bv_param))
            End If

            If pub_GetParameter("Estimate Date From", bv_param) <> "" AndAlso pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RPR_ESTMT_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.RPR_ESTMT_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf pub_GetParameter("Estimate Date From", bv_param) <> "" AndAlso pub_GetParameter("Estimate Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_ESTMT_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Estimate Date From", bv_param) = "" AndAlso pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_ESTMT_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtRepairStatus = objCommonUIs.GetEquipmentRepairStatusReport(strWhere)
            dtRepairStatus.TableName = "VM_EQUIPMENT_REPAIR_STATUS"
            dsRepairStatus.Tables.Add(dtRepairStatus)

            Return dsRepairStatus
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "Finance Pending Invoice Register"
    <OperationContract()> _
    Public Function pub_GetPendingInvoiceRegisterReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsRevenuePendingInvoiceRegister As New DataSet
        Dim dtRevenuePendingInvoiceRegister As New DataTable
        Try
            Dim PeriodFromDate As DateTime = Nothing
            Dim PeriodToDate As DateTime = Nothing
            Dim InFromDate As DateTime = Nothing
            Dim InToDate As DateTime = Nothing
            Dim strWhere As String = String.Empty
            Dim strWhereWithoutDepotID As String = " WHERE TRCKNG_ID IN (SELECT TRCKNG_ID FROM TRACKING) "
            Dim str_073Key As String
            str_073Key = objCommonUIs.DecryptString(CStr(objCommonUIs.GetConfigByKeyName("073", CLng(objCommonUIs.GetHeadQuarterID())).Tables(ConfigData._CONFIG).Rows(0).Item(ConfigData.KY_VL)))
            'multiDepo

            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" Then
                InFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            End If
            If pub_GetParameter("In Date To", bv_param) <> "" Then
                InToDate = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("In Date From", bv_param)) AndAlso IsDate(pub_GetParameter("In Date To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTN_DT, " >= ('", InFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.GTN_DT, " <= '", InToDate.ToString("dd-MMM-yyyy"), "' ")
                strWhereWithoutDepotID = String.Concat(strWhereWithoutDepotID, " AND ", CommonUIData.GTN_DT, " >= ('", InFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.GTN_DT, " <= '", InToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf Not IsDate(pub_GetParameter("In Date From", bv_param)) AndAlso IsDate(pub_GetParameter("In Date To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTN_DT, " <= '", InToDate.ToString("dd-MMM-yyyy"), "' ")
                strWhereWithoutDepotID = String.Concat(strWhereWithoutDepotID, " AND ", CommonUIData.GTN_DT, " <= '", InToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf IsDate(pub_GetParameter("In Date From", bv_param)) AndAlso Not IsDate(pub_GetParameter("In Date To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTN_DT, " >= '", InFromDate.ToString("dd-MMM-yyyy"), "' ")
                strWhereWithoutDepotID = String.Concat(strWhereWithoutDepotID, " AND ", CommonUIData.GTN_DT, " >= '", InFromDate.ToString("dd-MMM-yyyy"), "' ")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
                strWhereWithoutDepotID = String.Concat(strWhereWithoutDepotID, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If

            If pub_GetParameter("Period From", bv_param) <> "" Then
                PeriodFromDate = CDate(pub_GetParameter("Period From", bv_param))
            End If
            If pub_GetParameter("Period To", bv_param) <> "" Then
                PeriodToDate = CDate(pub_GetParameter("Period To", bv_param))
            End If

            If IsDate(pub_GetParameter("Period From", bv_param)) AndAlso IsDate(pub_GetParameter("Period To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.ACTVTY_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.ACTVTY_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strWhereWithoutDepotID = String.Concat(strWhereWithoutDepotID, " AND ", CommonUIData.ACTVTY_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.ACTVTY_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf Not IsDate(pub_GetParameter("Period From", bv_param)) AndAlso IsDate(pub_GetParameter("Period To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.ACTVTY_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strWhereWithoutDepotID = String.Concat(strWhereWithoutDepotID, " AND ", CommonUIData.ACTVTY_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf IsDate(pub_GetParameter("Period From", bv_param)) AndAlso Not IsDate(pub_GetParameter("Period To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.ACTVTY_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' ")
                strWhereWithoutDepotID = String.Concat(strWhereWithoutDepotID, " AND ", CommonUIData.ACTVTY_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' ")
            End If
            If pub_GetParameter("Invoicing To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.SRVC_PRTNR_ID, " IN (", pub_GetParameter("Invoicing To", bv_param), ")")
            End If
            Dim strActivityType As String = pub_GetParameter("Activity Type", bv_param)
            ''If Slab Rate is true for cleaning
            If str_073Key.ToLower = "true" AndAlso pub_GetParameter("Activity Type", bv_param).Contains("106") Then
                If pub_GetParameter("Depot", bv_param) <> "" Then
                    Dim strDepotId() As String = pub_GetParameter("Depot", bv_param).Split(CChar(","))
                    Dim strCstmr() As String = pub_GetParameter("Invoicing To", bv_param).Split(CChar(","))
                    Dim dtTempRevenueCleaning As DataTable = dtRevenuePendingInvoiceRegister.Clone()
                    Dim strTempWhere As String = strWhere.Replace("WHERE", " AND ")
                    For intDepotCount As Integer = 0 To strDepotId.Length - 1
                        For intCustId As Integer = 0 To strCstmr.Length - 1
                            Dim dtActivityStatus As New DataTable
                            Dim objInvoiceGenerations As New InvoiceGenerations
                            Dim strCustomerType As String = CStr(objCommonUIs.GetServicePartnerByID(CInt(strCstmr(intCustId).Replace("'", "")), CInt(objCommonUIs.GetHeadQuarterID())).Rows(0).Item(CommonUIData.SRVC_PRTNR_TYP_CD))
                            If pub_GetParameter("Period From", bv_param) = "" Then
                                If strCustomerType.ToLower = "customer" Then
                                    dtActivityStatus = objInvoiceGenerations.GetActivityStatusByCustomer(CLng(strDepotId(intDepotCount).Replace("'", "")), CLng(strCstmr(intCustId).Replace("'", "")), " ORDER BY INSPCTN_DT ASC  ")
                                    If dtActivityStatus.Rows.Count > 0 AndAlso Not IsDBNull(dtActivityStatus.Rows(0).Item(InvoiceGenerationData.GTN_DT)) Then
                                        PeriodFromDate = CDate(dtActivityStatus.Rows(0).Item(InvoiceGenerationData.GTN_DT))
                                    End If
                                Else
                                    dtActivityStatus = objInvoiceGenerations.GetCleaningChargeByInvoicingParty(CLng(strDepotId(intDepotCount).Replace("'", "")), CLng(strCstmr(intCustId).Replace("'", "")), " ORDER BY ORGNL_INSPCTD_DT ASC ")
                                    If dtActivityStatus.Rows.Count > 0 AndAlso Not IsDBNull(dtActivityStatus.Rows(0).Item(InvoiceGenerationData.ORGNL_INSPCTD_DT)) Then
                                        PeriodFromDate = CDate(dtActivityStatus.Rows(0).Item(CleaningData.ORGNL_INSPCTD_DT))
                                    End If
                                End If
                                If PeriodFromDate = Nothing Then
                                    PeriodFromDate = Now
                                End If
                            Else
                                PeriodFromDate = CDate(pub_GetParameter("Period From", bv_param))
                            End If


                            If pub_GetParameter("Period To", bv_param) <> "" Then
                                PeriodToDate = CDate(pub_GetParameter("Period To", bv_param))
                            Else
                                PeriodToDate = Now
                            End If
                            pub_CalculateCleaningSlab(dtTempRevenueCleaning, CDate(PeriodFromDate.ToString("dd-MMM-yyyy")), CDate(PeriodToDate.ToString("dd-MMM-yyyy")), CInt(strCstmr(intCustId).Replace("'", "")), strCustomerType, CInt(strDepotId(intDepotCount).Replace("'", "")), strTempWhere, strWhereWithoutDepotID)
                            dtRevenuePendingInvoiceRegister.Merge(dtTempRevenueCleaning)
                            dtRevenuePendingInvoiceRegister.AcceptChanges()
                            dtTempRevenueCleaning.Clear()
                        Next
                    Next
                End If
                If strActivityType.Contains("'106',") Then
                    strActivityType = strActivityType.Replace("'106',", "")
                ElseIf strActivityType.Contains(",'106'") Then
                    strActivityType = strActivityType.Replace(",'106'", "")
                ElseIf strActivityType.Contains("'106'") Then
                    strActivityType = strActivityType.Replace("'106'", "")
                End If
            End If
            If str_073Key.ToLower = "true" Then
                Dim dtTempRevenueCleaning As DataTable = dtRevenuePendingInvoiceRegister.Clone()
                If strActivityType <> "" Then
                    strWhere = String.Concat(strWhere, " AND PNDNG_INVC_TYP_ID IN (", strActivityType, ") ")
                    dtTempRevenueCleaning = objCommonUIs.GetFinancePendingInvoiceRegisterReport(dtRevenuePendingInvoiceRegister, strWhere, "VM_PENDING_INVOICE_REGISTER")
                    dtRevenuePendingInvoiceRegister.Merge(dtTempRevenueCleaning)
                End If
            Else
                strWhere = String.Concat(strWhere, " AND PNDNG_INVC_TYP_ID IN (", pub_GetParameter("Activity Type", bv_param), ") ")
                dtRevenuePendingInvoiceRegister = objCommonUIs.GetFinancePendingInvoiceRegisterReport(dtRevenuePendingInvoiceRegister, strWhere, "VM_PENDING_INVOICE_REGISTER")
            End If
         
            'Repair
            If strActivityType.Contains("105") Then
                Dim dtPendingInvoiceRepair As DataTable = dtRevenuePendingInvoiceRegister.Clone
                Dim lngMaxNo As Int64
                lngMaxNo = GetNextIndex(dtRevenuePendingInvoiceRegister, CommonUIData.TRCKNG_ID)
                dtPendingInvoiceRepair = objCommonUIs.GetFinancePendingInvoiceRegisterReport(dtPendingInvoiceRepair, strWhere, "VM_PENDING_INVOICE_REGISTER_REPAIR")
                For Each drRepair As DataRow In dtPendingInvoiceRepair.Rows
                    drRepair.Item(CommonUIData.TRCKNG_ID) = lngMaxNo
                    lngMaxNo += 1
                Next
                dtRevenuePendingInvoiceRegister.Merge(dtPendingInvoiceRepair)
            End If
            dtRevenuePendingInvoiceRegister.TableName = "VM_PENDING_INVOICE_REGISTER"
            dsRevenuePendingInvoiceRegister.Tables.Add(dtRevenuePendingInvoiceRegister)
            'Transportation
            If (pub_GetParameter("Activity Type", bv_param) <> "" AndAlso pub_GetParameter("Activity Type", bv_param).Contains("114")) OrElse pub_GetParameter("Activity Type", bv_param) = "" Then
                Dim intCount As Integer = 0
                If dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Rows.Count > 0 Then
                    intCount = CInt(dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Compute("MAX(" & CommonUIData.TRCKNG_ID & ")", String.Empty))
                End If
                dtRevenuePendingInvoiceRegister = New DataTable
                dtRevenuePendingInvoiceRegister = objCommonUIs.GetFinancePendingInvoiceRegisterTransport(strWhere)
                For Each drPendingRegister As DataRow In dtRevenuePendingInvoiceRegister.Rows
                    intCount = intCount + 1
                    drPendingRegister.Item(CommonUIData.TRCKNG_ID) = intCount
                Next
                If dtRevenuePendingInvoiceRegister.Rows.Count > 0 Then
                    dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Merge(dtRevenuePendingInvoiceRegister)
                End If
            End If
            'OnHire Off Hire
            If (pub_GetParameter("Activity Type", bv_param) <> "" AndAlso (pub_GetParameter("Activity Type", bv_param).Contains("116") OrElse _
                pub_GetParameter("Activity Type", bv_param).Contains("115"))) OrElse pub_GetParameter("Activity Type", bv_param) = "" Then
                Dim intCount As Integer = 0
                If dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Rows.Count > 0 Then
                    intCount = CInt(dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Compute("MAX(" & CommonUIData.TRCKNG_ID & ")", String.Empty))
                End If
                dtRevenuePendingInvoiceRegister = New DataTable
                dtRevenuePendingInvoiceRegister = objCommonUIs.GetFinancePendingInvoiceRegisterOnHireOffHire(strWhere)
                For Each drPendingRegister As DataRow In dtRevenuePendingInvoiceRegister.Rows
                    intCount = intCount + 1
                    drPendingRegister.Item(CommonUIData.TRCKNG_ID) = intCount
                Next
                If dtRevenuePendingInvoiceRegister.Rows.Count > 0 Then
                    dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Merge(dtRevenuePendingInvoiceRegister)
                End If
            End If

            dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Columns.Add(CommonUIData.FROM_BILLING_DATE, GetType(System.DateTime))
            dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Columns.Add(CommonUIData.TO_BILLING_DATE, GetType(System.DateTime))
            'Rental 
            If (pub_GetParameter("Activity Type", bv_param) <> "" AndAlso pub_GetParameter("Activity Type", bv_param).Contains("117")) OrElse pub_GetParameter("Activity Type", bv_param) = "" Then
                Dim intCount As Integer = 0
                If dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Rows.Count > 0 Then
                    intCount = CInt(dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Compute("MAX(" & CommonUIData.TRCKNG_ID & ")", String.Empty))
                End If
                dtRevenuePendingInvoiceRegister = New DataTable
                dtRevenuePendingInvoiceRegister = objCommonUIs.GetFinancePendingInvoiceRegisterRentalCharge(strWhere)
                If pub_GetParameter("Period To", bv_param) = "" Then
                    PeriodToDate = DateTime.Now
                End If
                If dtRevenuePendingInvoiceRegister.Rows.Count > 0 Then
                    objCommonUIs.pvt_RentalInvoiceCalculation(dtRevenuePendingInvoiceRegister, Nothing, _
                                                              PeriodToDate, 1, False)
                    For Each drPendingRegister As DataRow In dtRevenuePendingInvoiceRegister.Rows
                        intCount = intCount + 1
                        drPendingRegister.Item(CommonUIData.TRCKNG_ID) = intCount
                        Dim drRentalPending As DataRow = dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").NewRow
                        For Each dcRental As DataColumn In dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Columns
                            If dtRevenuePendingInvoiceRegister.Columns.Contains(dcRental.ColumnName) AndAlso _
                                Not IsDBNull(drPendingRegister.Item(dcRental.ColumnName)) Then
                                drRentalPending.Item(dcRental.ColumnName) = drPendingRegister.Item(dcRental.ColumnName)
                            End If
                        Next
                        dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Rows.Add(drRentalPending)
                    Next
                End If
            End If
            'Storage
            If (pub_GetParameter("Activity Type", bv_param) <> "" AndAlso pub_GetParameter("Activity Type", bv_param).Contains("113")) OrElse pub_GetParameter("Activity Type", bv_param) = "" Then
                Dim intCount As Integer = 0
                If dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Rows.Count > 0 Then
                    intCount = CInt(dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Compute("MAX(" & CommonUIData.TRCKNG_ID & ")", String.Empty))
                End If
                dtRevenuePendingInvoiceRegister = New DataTable
                dtRevenuePendingInvoiceRegister = objCommonUIs.GetFinancePendingInvoiceRegisterStorageCharge(strWhere)
                If pub_GetParameter("Period To", bv_param) = "" Then
                    PeriodToDate = DateTime.Now
                End If
                If dtRevenuePendingInvoiceRegister.Rows.Count > 0 Then
                    objCommonUIs.pub_HSStorage_Calculation(dtRevenuePendingInvoiceRegister, Nothing, _
                                                           PeriodToDate, False)
                    Dim dtRevenue As New DataTable
                    dtRevenue.Clear()
                    If dtRevenuePendingInvoiceRegister.Select(String.Concat(CommonUIData.CSTMR_AMNT, " IS NOT NULL AND ", CommonUIData.CSTMR_AMNT, " >0 AND ", CommonUIData.CSTMR_AMNT, " >0.00")).Length > 0 Then
                        dtRevenue = dtRevenuePendingInvoiceRegister.Select(String.Concat(CommonUIData.CSTMR_AMNT, " IS NOT NULL AND ", CommonUIData.CSTMR_AMNT, " >0 AND ", CommonUIData.CSTMR_AMNT, " > 0.00")).CopyToDataTable
                    End If
                    For Each drPendingRegister As DataRow In dtRevenue.Rows
                        intCount = intCount + 1
                        drPendingRegister.Item(CommonUIData.TRCKNG_ID) = intCount
                        Dim drRentalPending As DataRow = dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").NewRow
                        For Each dcRental As DataColumn In dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Columns
                            If dtRevenue.Columns.Contains(dcRental.ColumnName) AndAlso _
                                Not IsDBNull(drPendingRegister.Item(dcRental.ColumnName)) Then
                                drRentalPending.Item(dcRental.ColumnName) = drPendingRegister.Item(dcRental.ColumnName)
                                drRentalPending.Item(CommonUIData.COUNT_EQPMNT_NO) = 1
                            End If
                        Next
                        dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Rows.Add(drRentalPending)
                    Next
                End If
            End If

            Dim dtDistinctCustomer As New DataTable
            Dim dsReport As New DataSet
            Dim strCustomer As String
            Dim i As Integer = 0
            dtDistinctCustomer = objCommonUIs.GetFinanceDistinctCustomer(String.Concat("VM_PENDING_INVOICE_REGISTER ", strWhere))
            If dtDistinctCustomer.Rows.Count > 0 Then

                For Each dr As DataRow In dtDistinctCustomer.Rows
                    i = i + 1
                    If i = dtDistinctCustomer.Rows.Count Then
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")))
                    Else
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")), "/")
                    End If
                Next
            End If

            If Not dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Columns.Contains("DepotString") Then
                dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Columns.Add("DepotString", GetType(System.String))
            End If

            For Each dr As DataRow In dsRevenuePendingInvoiceRegister.Tables("VM_PENDING_INVOICE_REGISTER").Rows
                dr.Item("DepotString") = strCustomer
            Next
            Return dsRevenuePendingInvoiceRegister
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "Metrics Heating"
    <OperationContract()> _
    Public Function pub_GetHeatingKPIReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsKPIHeating As New DataSet
        Dim dtKPIHeating As New DataTable
        Try
            Dim IndatFromDate As Date = Nothing
            Dim IndatToDate As Date = Nothing
            Dim HeatingStartFromDate As Date = Nothing
            Dim HeatingStartToDate As Date = Nothing
            Dim HeatingEndFromDate As Date = Nothing
            Dim HeatingEndToDate As Date = Nothing
            Dim strWhere As String = String.Empty
            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If
            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            End If
            If pub_GetParameter("In Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) = "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Heating End Date From", bv_param) <> "" Then
                HeatingEndFromDate = CDate(pub_GetParameter("Heating End Date From", bv_param))
            End If
            If pub_GetParameter("Heating End Date To", bv_param) <> "" Then
                HeatingEndToDate = CDate(pub_GetParameter("Heating End Date To", bv_param))
            End If

            If pub_GetParameter("Heating End Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.HTNG_END_DT, " >= '", HeatingEndFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.HTNG_END_DT, " <= '", HeatingEndToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Heating End Date From", bv_param) <> "" AndAlso pub_GetParameter("Heating End Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.HTNG_END_DT, " >= '", HeatingEndFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Heating End Date From", bv_param) = "" AndAlso pub_GetParameter("Heating End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.HTNG_END_DT, " <= '", HeatingEndToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtKPIHeating = objCommonUIs.GetKPIHeatingReport(strWhere)
            dtKPIHeating.TableName = "VM_METRICS_HEATING"
            dsKPIHeating.Tables.Add(dtKPIHeating)
            Return dsKPIHeating
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "Metrics Cleaning"
    <OperationContract()> _
    Public Function pub_GetCleaningKPIReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsKPICleaning As New DataSet
        Dim dtKPICleaning As New DataTable
        Try
            Dim IndatFromDate As Date = Nothing
            Dim IndatToDate As Date = Nothing
            Dim InspectiondatFromDate As Date = Nothing
            Dim InspectiondatToDate As Date = Nothing
            Dim strWhere As String = String.Empty
            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If

            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            End If
            If pub_GetParameter("In Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) = "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Inspection Date From", bv_param) <> "" Then
                InspectiondatFromDate = CDate(pub_GetParameter("Inspection Date From", bv_param))
            End If
            If pub_GetParameter("Inspection Date To", bv_param) <> "" Then
                InspectiondatToDate = CDate(pub_GetParameter("Inspection Date To", bv_param))
            End If

            If pub_GetParameter("Inspection Date From", bv_param) <> "" AndAlso pub_GetParameter("Inspection Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.ORGNL_INSPCTD_DT, " >= '", InspectiondatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.ORGNL_INSPCTD_DT, " <= '", InspectiondatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Inspection Date From", bv_param) <> "" AndAlso pub_GetParameter("Inspection Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.ORGNL_INSPCTD_DT, " >= '", InspectiondatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Inspection Date From", bv_param) = "" AndAlso pub_GetParameter("Inspection Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.ORGNL_INSPCTD_DT, " <= '", InspectiondatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtKPICleaning = objCommonUIs.GetKPICleaningReport(strWhere)
            dtKPICleaning.TableName = "VM_METRICS_CLEANING"
            dsKPICleaning.Tables.Add(dtKPICleaning)
            Return dsKPICleaning
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "Metrics Repair"
    <OperationContract()> _
    Public Function pub_GetRepairKPIReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsKPIRepair As New DataSet
        Dim dtKPIRepair As New DataTable
        Dim dtRepairType As New DataTable
        Try
            Dim EstimatedatFromDate As Date = Nothing
            Dim EstimatedatToDate As Date = Nothing
            Dim CompletiondatFromDate As Date = Nothing
            Dim CompletiondatToDate As Date = Nothing
            Dim strWhereRepair As String = String.Empty
            Dim strWhere As String = String.Empty
            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" DPT_ID = ", intDepotID)
            End If

            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If

            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Estimate Date From", bv_param) <> "" Then
                EstimatedatFromDate = CDate(pub_GetParameter("Estimate Date From", bv_param))
            End If
            If pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                EstimatedatToDate = CDate(pub_GetParameter("Estimate Date To", bv_param))
            End If

            If pub_GetParameter("Estimate Date From", bv_param) <> "" AndAlso pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_ESTMT_DT, " >= '", EstimatedatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.RPR_ESTMT_DT, " <= '", EstimatedatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Estimate Date From", bv_param) <> "" AndAlso pub_GetParameter("Estimate Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_ESTMT_DT, " >= '", EstimatedatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Estimate Date From", bv_param) = "" AndAlso pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_ESTMT_DT, " <= '", EstimatedatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Completion Date From", bv_param) <> "" Then
                CompletiondatFromDate = CDate(pub_GetParameter("Completion Date From", bv_param))
            End If
            If pub_GetParameter("Completion Date To", bv_param) <> "" Then
                CompletiondatToDate = CDate(pub_GetParameter("Completion Date To", bv_param))
            End If

            If pub_GetParameter("Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Completion Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_CMPLTN_DT, " >= '", CompletiondatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.RPR_CMPLTN_DT, " <= '", CompletiondatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Completion Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_CMPLTN_DT, " >= '", CompletiondatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Completion Date From", bv_param) = "" AndAlso pub_GetParameter("Completion Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_CMPLTN_DT, " <= '", CompletiondatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Repair Type", bv_param) <> "" AndAlso pub_GetParameter("Repair Type", bv_param).Contains(CChar(",")) AndAlso pub_GetParameter("Repair Type", bv_param).Split(CChar(",")).Length = objCommonUIs.GetCountRepairType() Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_TYP_ID, " IN (", pub_GetParameter("Repair Type", bv_param), ") OR (", CommonUIData.RPR_TYP_ID, " IS NULL))")
            ElseIf pub_GetParameter("Repair Type", bv_param) <> "" AndAlso pub_GetParameter("Repair Type", bv_param).Contains(CChar(",")) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RPR_TYP_ID, " IN (", pub_GetParameter("Repair Type", bv_param), ") ")
            ElseIf pub_GetParameter("Repair Type", bv_param) <> "" AndAlso Not (pub_GetParameter("Repair Type", bv_param).Contains(CChar(","))) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RPR_TYP_ID, " IN (", pub_GetParameter("Repair Type", bv_param), ") ")
            End If

            If strWhere.Length > 0 Then
                strWhereRepair = String.Concat(strWhere, " AND ", CommonUIData.COUNT_EQPMNT_NO, " = 1")
            End If

            dtKPIRepair = objCommonUIs.GetKPIRepairReport(strWhereRepair)
            dtRepairType = objCommonUIs.GetKPIReport_RepairType(String.Concat(strWhere, " ORDER BY RPR_ESTMT_ID"))
            dtKPIRepair.TableName = "VM_METRICS_REPAIR"

            Dim sbrRepairType As New StringBuilder
            'For Each drKPIRepair As DataRow In dtKPIRepair.Rows
            '    For Each dr As DataRow In dtRepairType.Select(String.Concat(CommonUIData.GI_TRNSCTN_NO, "='", drKPIRepair.Item(CommonUIData.GI_TRNSCTN_NO), "' AND ", CommonUIData.BLLNG_FLG, "='B'"))
            '        If sbrRepairType.ToString <> Nothing Then
            '            sbrRepairType.Append(", ")
            '        End If
            '        sbrRepairType.Append(dr.Item(CommonUIData.RPR_TYP_CD))
            '    Next
            '    If sbrRepairType.Length > 0 Then
            '        drKPIRepair.Item(CommonUIData.RPR_TYP_CD) = sbrRepairType.ToString()
            '        sbrRepairType.Remove(0, sbrRepairType.Length)
            '    End If
            'Next

            dsKPIRepair.Tables.Add(dtKPIRepair)
            Return dsKPIRepair
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "Metrics Repair Labor"
    <OperationContract()> _
    Public Function pub_GetRepairLaborKPIReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsKPIRepairLabor As New DataSet
        Dim dtKPIRepairLabor As New DataTable
        Try
            Dim IndatFromDate As Date = Nothing
            Dim IndatToDate As Date = Nothing
            Dim EstimatedatFromDate As Date = Nothing
            Dim EstimatedatToDate As Date = Nothing
            Dim CompletiondatFromDate As Date = Nothing
            Dim CompletiondatToDate As Date = Nothing
            Dim strWhere As String = String.Empty

            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If
            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If

            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Repair Type", bv_param) <> "" AndAlso pub_GetParameter("Repair Type", bv_param).Contains(CChar(",")) AndAlso pub_GetParameter("Repair Type", bv_param).Split(CChar(",")).Length = objCommonUIs.GetCountRepairType() Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_TYP_ID, " IN (", pub_GetParameter("Repair Type", bv_param), ") OR (", CommonUIData.RPR_TYP_ID, " IS NULL))")
            ElseIf pub_GetParameter("Repair Type", bv_param) <> "" AndAlso Not (pub_GetParameter("Repair Type", bv_param).Contains(CChar(","))) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RPR_TYP_ID, " IN (", pub_GetParameter("Repair Type", bv_param), ") ")
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("In Date From", bv_param))
            End If

            If pub_GetParameter("In Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) = "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Estimate Date From", bv_param) <> "" Then
                EstimatedatFromDate = CDate(pub_GetParameter("Estimate Date From", bv_param))
            End If
            If pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                EstimatedatToDate = CDate(pub_GetParameter("Estimate Date To", bv_param))
            End If

            If pub_GetParameter("Estimate Date From", bv_param) <> "" AndAlso pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_ESTMT_DT, " >= '", EstimatedatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.RPR_ESTMT_DT, " <= '", EstimatedatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Estimate Date From", bv_param) <> "" AndAlso pub_GetParameter("Estimate Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_ESTMT_DT, " >= '", EstimatedatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Estimate Date From", bv_param) = "" AndAlso pub_GetParameter("Estimate Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_ESTMT_DT, " <= '", EstimatedatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Completion Date From", bv_param) <> "" Then
                CompletiondatFromDate = CDate(pub_GetParameter("Completion Date From", bv_param))
            End If
            If pub_GetParameter("Completion Date To", bv_param) <> "" Then
                CompletiondatToDate = CDate(pub_GetParameter("Completion Date To", bv_param))
            End If

            If pub_GetParameter("Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Completion Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_CMPLTN_DT, " >= '", CompletiondatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.RPR_CMPLTN_DT, " <= '", CompletiondatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Completion Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_CMPLTN_DT, " >= '", CompletiondatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Completion Date From", bv_param) = "" AndAlso pub_GetParameter("Completion Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_CMPLTN_DT, " <= '", CompletiondatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtKPIRepairLabor = objCommonUIs.GetKPIRepairLaborReport(strWhere)
            dtKPIRepairLabor.TableName = "VM_METRICS_REPAIR_LABOR"
            dsKPIRepairLabor.Tables.Add(dtKPIRepairLabor)
            Return dsKPIRepairLabor
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "Metrics Overall Report"
    <OperationContract()> _
    Public Function GetOverallKPIReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsKPIOverall As New DataSet
        Dim dtOverall As New DataTable
        Dim dtRepairType As New DataTable
        Try
            Dim InDateFrom As DateTime = Nothing
            Dim InDateTo As DateTime = Nothing
            Dim strCustomer As String = String.Empty
            Dim strPreviousCargo As String = String.Empty
            Dim strEquipmentNo As String = String.Empty
            Dim strEquipmentTyp As String = String.Empty
            Dim InspectionDateFrom As DateTime = Nothing
            Dim InspectionDateTo As DateTime = Nothing
            Dim RepairCompletionDateFrom As DateTime = Nothing
            Dim RepairCompletionDateTo As DateTime = Nothing
            Dim SurveyCompletionDateFrom As DateTime = Nothing
            Dim SurveyCompletionDateTo As DateTime = Nothing
            Dim OutDateFrom As DateTime = Nothing
            Dim OutDateTo As DateTime = Nothing
            Dim strWhere As String = String.Empty

            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" DPT_ID = ", intDepotID)
            End If
            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" Then
                InDateFrom = CDate(pub_GetParameter("In Date From", bv_param))
            End If
            If pub_GetParameter("In Date To", bv_param) <> "" Then
                InDateTo = CDate(pub_GetParameter("In Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("In Date From", bv_param)) AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                InDateTo = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTN_DT, " >= ('", InDateFrom.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.GTN_DT, " <= '", InDateTo.ToString("dd-MMM-yyyy"), "' ")
            ElseIf pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " >= '", InDateFrom.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("In Date From", bv_param) <> "" AndAlso pub_GetParameter("In Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.GTN_DT, " <= '", InDateTo.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Inspection Date From", bv_param) <> "" Then
                InspectionDateFrom = CDate(pub_GetParameter("Inspection Date From", bv_param))
            End If
            If pub_GetParameter("Inspection Date To", bv_param) <> "" Then
                InspectionDateTo = CDate(pub_GetParameter("Inspection Date To", bv_param))
            End If

            If pub_GetParameter("Inspection Date From", bv_param) <> "" AndAlso pub_GetParameter("Inspection Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.INSPCTN_DT, " >= ('", InspectionDateFrom.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.INSPCTN_DT, " <= '", InspectionDateTo.ToString("dd-MMM-yyyy"), "' ")
            ElseIf pub_GetParameter("Inspection Date From", bv_param) <> "" AndAlso pub_GetParameter("Inspection Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.INSPCTN_DT, " >= '", InspectionDateFrom.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Inspection Date From", bv_param) <> "" AndAlso pub_GetParameter("Inspection Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.INSPCTN_DT, " <= '", InspectionDateTo.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Repair Completion Date From", bv_param) <> "" Then
                RepairCompletionDateFrom = CDate(pub_GetParameter("Repair Completion Date From", bv_param))
            End If
            If pub_GetParameter("Repair Completion Date To", bv_param) <> "" Then
                RepairCompletionDateTo = CDate(pub_GetParameter("Repair Completion Date To", bv_param))
            End If

            If pub_GetParameter("Repair Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Repair Completion Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RPR_CMPLTN_DT, " >= ('", RepairCompletionDateFrom.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.RPR_CMPLTN_DT, " <= '", RepairCompletionDateTo.ToString("dd-MMM-yyyy"), "' ")
            ElseIf pub_GetParameter("Repair Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Repair Completion Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_CMPLTN_DT, " >= '", RepairCompletionDateFrom.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Repair Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Repair Completion Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.RPR_CMPLTN_DT, " <= '", RepairCompletionDateTo.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Survey Completion Date From", bv_param) <> "" Then
                SurveyCompletionDateFrom = CDate(pub_GetParameter("Survey Completion Date From", bv_param))
            End If
            If pub_GetParameter("Survey Completion Date To", bv_param) <> "" Then
                SurveyCompletionDateTo = CDate(pub_GetParameter("Survey Completion Date To", bv_param))
            End If

            If pub_GetParameter("Survey Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Survey Completion Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND SRV_DT >= ('", SurveyCompletionDateFrom.ToString("dd-MMM-yyyy"), "') AND SRV_DT <= '", SurveyCompletionDateTo.ToString("dd-MMM-yyyy"), "' ")
            ElseIf pub_GetParameter("Survey Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Survey Completion Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND ( SRV_DT >= '", SurveyCompletionDateFrom.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Survey Completion Date From", bv_param) <> "" AndAlso pub_GetParameter("Survey Completion Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND ( SRV_DT <= '", SurveyCompletionDateTo.ToString("dd-MMM-yyyy"), "') ")
            End If

            If pub_GetParameter("Out Date From", bv_param) <> "" Then
                OutDateFrom = CDate(pub_GetParameter("Out Date From", bv_param))
            End If
            If pub_GetParameter("Out Date To", bv_param) <> "" Then
                OutDateTo = CDate(pub_GetParameter("Out Date To", bv_param))
            End If

            If pub_GetParameter("Out Date From", bv_param) <> "" AndAlso pub_GetParameter("Out Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.GTOT_DT, " >= ('", OutDateFrom.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.GTOT_DT, " <= '", OutDateTo.ToString("dd-MMM-yyyy"), "' ")
            ElseIf pub_GetParameter("Out Date From", bv_param) <> "" AndAlso pub_GetParameter("Out Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND ( ", CommonUIData.GTOT_DT, " >= '", OutDateFrom.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Out Date From", bv_param) <> "" AndAlso pub_GetParameter("Out Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND ( ", CommonUIData.GTOT_DT, " <= '", OutDateTo.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtOverall = objCommonUIs.GetKPIOVerallReport(strWhere)
            dtRepairType = objCommonUIs.GetKPIReport_RepairType(String.Concat(strWhere, " ORDER BY RPR_ESTMT_ID"))
            dtOverall.TableName = "VM_METRICS_OVERALL"

            Dim sbrRepairType As New StringBuilder
            For Each drOverall As DataRow In dtOverall.Rows
                For Each dr As DataRow In dtRepairType.Select(String.Concat(CommonUIData.GI_TRNSCTN_NO, "='", drOverall.Item(CommonUIData.GI_TRNSCTN_NO), "'"))
                    If sbrRepairType.ToString <> Nothing Then
                        sbrRepairType.Append(", ")
                    End If
                    sbrRepairType.Append(dr.Item(CommonUIData.RPR_TYP_CD))
                Next
                If sbrRepairType.Length > 0 Then
                    drOverall.Item(CommonUIData.RPR_TYP_CD) = sbrRepairType.ToString()
                    sbrRepairType.Remove(0, sbrRepairType.Length)
                End If
            Next

            dsKPIOverall.Tables.Add(dtOverall)
            Return dsKPIOverall
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "Transportation Report"
    <OperationContract()> _
    Public Function pub_GetTransportationDetails(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsTransportation As New DataSet
        Dim dtTransportation As New DataTable
        Try
            Dim datFromDate As Date = Nothing
            Dim datToDate As Date = Nothing
            Dim HeatingStartFromDate As Date = Nothing
            Dim HeatingStartToDate As Date = Nothing
            Dim HeatingEndFromDate As Date = Nothing
            Dim HeatingEndToDate As Date = Nothing
            Dim strWhere As String = String.Empty

            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else
                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If
            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            'Relase - 2 
            If pub_GetParameter("Transporter", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.TRNSPRTR_ID, " IN (", pub_GetParameter("Transporter", bv_param), ")")
                strWhere = String.Concat(strWhere, " OR ", CommonUIData.TRNSPRTR_ID, " IS NULL)")
            End If

            If pub_GetParameter("Route", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RT_ID, " IN (", pub_GetParameter("Route", bv_param), ")")
            End If

            'Release - 2
            If pub_GetParameter("Activity", bv_param) <> "" Then
                Dim strActivityID As String = String.Empty
                strActivityID = pub_GetParameter("Activity", bv_param)
                If strActivityID.Contains("120") Then
                    strActivityID = String.Concat(strActivityID, ",'93'")
                End If
                If strActivityID.Contains("121") Or strActivityID.Contains("122") Then
                    strActivityID = String.Concat(strActivityID, ",'94'")
                End If
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_STT_ID, " IN (", strActivityID, ") ")
            End If

            If pub_GetParameter("Request No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RQST_NO, " ='", pub_GetParameter("Request No", bv_param), "' ")
            End If

            If pub_GetParameter("Customer Ref No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_RF_NO, " ='", pub_GetParameter("Customer Ref No", bv_param), "' ")
            End If

            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Job End Date From", bv_param) <> "" Then
                datFromDate = CDate(pub_GetParameter("Job End Date From", bv_param))
            End If
            If pub_GetParameter("Job End Date To", bv_param) <> "" Then
                datToDate = CDate(pub_GetParameter("Job End Date To", bv_param))
            End If

            If pub_GetParameter("Job End Date From", bv_param) <> "" AndAlso pub_GetParameter("Job End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.EVNT_DT, " >= '", datFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.EVNT_DT, " <= '", datToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Job End Date From", bv_param) <> "" AndAlso pub_GetParameter("Job End Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.EVNT_DT, " >= '", datFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Job End Date From", bv_param) = "" AndAlso pub_GetParameter("Job End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.EVNT_DT, " <= '", datToDate.ToString("dd-MMM-yyyy"), "') ")
            End If
            dtTransportation = objCommonUIs.GetTransportationReport(strWhere)
            dtTransportation.TableName = "VM_TRANSPORTATION"
            dsTransportation.Tables.Add(dtTransportation)
            Return dsTransportation
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "Finance Transportation"
    <OperationContract()> _
    Public Function pub_GetTransportationRevenueReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsRevenueTransportation As New DataSet
        Dim dtRevenueTransportation As New DataTable
        Try
            Dim datJobEndFromDate As Date = Nothing
            Dim datJobEndToDate As Date = Nothing
            Dim strWhere As String = String.Empty

            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If
            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Transporter", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.TRNSPRTR_ID, " IN (", pub_GetParameter("Transporter", bv_param), ")")
                strWhere = String.Concat(strWhere, " OR ", CommonUIData.TRNSPRTR_ID, " IS NULL)")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If

            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Billed", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND BILLED_ID IN (", pub_GetParameter("Billed", bv_param), ") ")
            End If

            If pub_GetParameter("Route", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RT_ID, " IN (", pub_GetParameter("Route", bv_param), ") ")
            End If

            If pub_GetParameter("Activity", bv_param) <> "" Then
                Dim strActivityID As String = String.Empty
                strActivityID = pub_GetParameter("Activity", bv_param)
                If strActivityID.Contains("120") Then
                    strActivityID = String.Concat(strActivityID, ",'93'")
                End If
                If strActivityID.Contains("121") Or strActivityID.Contains("122") Then
                    strActivityID = String.Concat(strActivityID, ",'94'")
                End If
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_STT_ID, " IN (", strActivityID, ") ")
            End If

            If pub_GetParameter("Request No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RQST_NO, " IN ('", pub_GetParameter("Request No", bv_param), "') ")
            End If

            If pub_GetParameter("Customer Ref No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_RF_NO, " IN ('", pub_GetParameter("Customer Ref No", bv_param), "') ")
            End If

            If pub_GetParameter("Job End Date From", bv_param) <> "" Then
                datJobEndFromDate = CDate(pub_GetParameter("Job End Date From", bv_param))
            End If
            If pub_GetParameter("Job End Date To", bv_param) <> "" Then
                datJobEndToDate = CDate(pub_GetParameter("Job End Date To", bv_param))
            End If

            If pub_GetParameter("Job End Date From", bv_param) <> "" AndAlso pub_GetParameter("Job End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.JB_END_DT, " >= '", datJobEndFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.JB_END_DT, " <= '", datJobEndToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Job End Date From", bv_param) <> "" AndAlso pub_GetParameter("Job End Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.JB_END_DT, " >= '", datJobEndFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Job End Date From", bv_param) = "" AndAlso pub_GetParameter("Job End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.JB_END_DT, " <= '", datJobEndToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtRevenueTransportation = objCommonUIs.GetFinanceTransportationReport(strWhere)
            Dim dtDistinctCustomer As New DataTable
            Dim dsReport As New DataSet
            Dim strCustomer As String
            Dim i As Integer = 0
            dtDistinctCustomer = objCommonUIs.GetFinanceDistinctCustomer(String.Concat("VM_FINANCE_TRANSPORTATION ", strWhere))
            If dtDistinctCustomer.Rows.Count > 0 Then

                For Each dr As DataRow In dtDistinctCustomer.Rows
                    i = i + 1
                    If i = dtDistinctCustomer.Rows.Count Then
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")))
                    Else
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")), "/")
                    End If
                Next
            End If

            If Not dtRevenueTransportation.Columns.Contains("DepotString") Then
                dtRevenueTransportation.Columns.Add("DepotString", GetType(System.String))
            End If

            For Each dr As DataRow In dtRevenueTransportation.Rows
                dr.Item("DepotString") = strCustomer
            Next
            dtRevenueTransportation.TableName = "VM_FINANCE_TRANSPORTATION"
            dsRevenueTransportation.Tables.Add(dtRevenueTransportation)

            pvt_GetSummary(dtRevenueTransportation, dsRevenueTransportation, "REVENUE")

            Return dsRevenueTransportation
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "Metrics Transportation"
    <OperationContract()> _
    Public Function pub_GetTransportationKPIReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsKPITransportation As New DataSet
        Dim dtKPITransportation As New DataTable
        Try
            Dim datJobEndFromDate As Date = Nothing
            Dim datJobEndToDate As Date = Nothing
            Dim strWhere As String = String.Empty

            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Transporter", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.TRNSPRTR_ID, " IN (", pub_GetParameter("Transporter", bv_param), ")")
                strWhere = String.Concat(strWhere, " OR ", CommonUIData.TRNSPRTR_ID, " IS NULL)")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If

            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Billed", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND BILLED_ID IN (", pub_GetParameter("Billed", bv_param), ") ")
            End If

            If pub_GetParameter("Route", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RT_ID, " IN (", pub_GetParameter("Route", bv_param), ") ")
            End If

            If pub_GetParameter("Activity", bv_param) <> "" Then
                Dim strActivityID As String = String.Empty
                strActivityID = pub_GetParameter("Activity", bv_param)
                If strActivityID.Contains("120") Then
                    strActivityID = String.Concat(strActivityID, ",'93'")
                End If
                If strActivityID.Contains("121") Or strActivityID.Contains("122") Then
                    strActivityID = String.Concat(strActivityID, ",'94'")
                End If
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_STT_ID, " IN (", strActivityID, ") ")
            End If

            If pub_GetParameter("Request No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.RQST_NO, " IN ('", pub_GetParameter("Request No", bv_param), "') ")
            End If

            If pub_GetParameter("Customer Ref No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_RF_NO, " IN ('", pub_GetParameter("Customer Ref No", bv_param), "') ")
            End If

            If pub_GetParameter("Job End Date From", bv_param) <> "" Then
                datJobEndFromDate = CDate(pub_GetParameter("Job End Date From", bv_param))
            End If
            If pub_GetParameter("Job End Date To", bv_param) <> "" Then
                datJobEndToDate = CDate(pub_GetParameter("Job End Date To", bv_param))
            End If

            If pub_GetParameter("Job End Date From", bv_param) <> "" AndAlso pub_GetParameter("Job End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.JB_END_DT, " >= '", datJobEndFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.JB_END_DT, " <= '", datJobEndToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Job End Date From", bv_param) <> "" AndAlso pub_GetParameter("Job End Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.JB_END_DT, " >= '", datJobEndFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Job End Date From", bv_param) = "" AndAlso pub_GetParameter("Job End Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.JB_END_DT, " <= '", datJobEndToDate.ToString("dd-MMM-yyyy"), "') ")
            End If

            dtKPITransportation = objCommonUIs.GetKPITransportationReport(strWhere)
            dtKPITransportation.TableName = "VM_METRICS_TRANSPORTATION"
            dsKPITransportation.Tables.Add(dtKPITransportation)

            pvt_GetSummary(dtKPITransportation, dsKPITransportation, "KPI")

            Return dsKPITransportation
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "pvt_GetSummary"
    Private Function pvt_GetSummary(ByVal dtTransportation As DataTable, _
                                    ByRef dsTransportation As DataSet, _
                                    ByVal bv_strActivity As String) As Boolean
        Try
            Dim Distinctdata As New DatasetHelpers(CType(dsTransportation, DataSet))
            Dim dtDistinctCustomer As New DataTable
            Dim dtDistinctRoute As New DataTable

            dtDistinctCustomer = Distinctdata.SelectGroupByInto("CUSTOMER_SUMMARY", dtTransportation, String.Concat(CommonUIData.CSTMR_ID, ",", CommonUIData.CSTMR_CD, ",", CommonUIData.CSTMR_NAM), "", CommonUIData.CSTMR_ID)
            dtDistinctRoute = Distinctdata.SelectGroupByInto("ROUTE_SUMMARY", dtTransportation, String.Concat(CommonUIData.RT_ID, ",", CommonUIData.RT_CD, ",", CommonUIData.RT_DSCRPTN_VC), "", CommonUIData.RT_ID)

            pvt_CalculateSummaryValues(dtDistinctCustomer, bv_strActivity, CommonUIData.CSTMR_ID, dtTransportation)
            pvt_CalculateSummaryValues(dtDistinctRoute, bv_strActivity, CommonUIData.RT_ID, dtTransportation)

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pvt_CalculateSummaryValues"
    Private Function pvt_CalculateSummaryValues(ByRef dtTable As DataTable, ByVal bv_strActivity As String, _
                                                ByVal bv_strGroupBy As String, ByVal dtTransportation As DataTable) As Boolean
        Try
            If Not (dtTable.Columns.Contains("NO_OF_TRIPS")) Then
                dtTable.Columns.Add("NO_OF_TRIPS", GetType(System.Int32))
            End If
            If bv_strActivity = "KPI" Then
                If Not (dtTable.Columns.Contains("TRIP_TIME")) Then
                    dtTable.Columns.Add("TRIP_TIME", GetType(System.Int32))
                End If

                If Not (dtTable.Columns.Contains("AVERAGE_TRIP")) Then
                    dtTable.Columns.Add("AVERAGE_TRIP", GetType(System.Int32))
                End If

                If Not (dtTable.Columns.Contains("AVERAGE_TRIP_DAYS")) Then
                    dtTable.Columns.Add("AVERAGE_TRIP_DAYS", GetType(System.Int32))
                End If

                If Not (dtTable.Columns.Contains("AVERAGE_TRIPS_PER_DAYS")) Then
                    dtTable.Columns.Add("AVERAGE_TRIPS_PER_DAYS", GetType(System.Int32))
                End If

                If Not (dtTable.Columns.Contains("TOTAL_TRIP_TIME")) Then
                    dtTable.Columns.Add("TOTAL_TRIP_TIME", GetType(System.Int32))
                End If

                If Not (dtTable.Columns.Contains("TOTAL_AVERAGE_TRIP")) Then
                    dtTable.Columns.Add("TOTAL_AVERAGE_TRIP", GetType(System.Int32))
                End If

                If Not (dtTable.Columns.Contains("TOTAL_AVERAGE_TRIP_DAYS")) Then
                    dtTable.Columns.Add("TOTAL_AVERAGE_TRIP_DAYS", GetType(System.Int32))
                End If

                If Not (dtTable.Columns.Contains("TOTAL_AVERAGE_TRIPS_PER_DAYS")) Then
                    dtTable.Columns.Add("TOTAL_AVERAGE_TRIPS_PER_DAYS", GetType(System.Int32))
                End If
            ElseIf bv_strActivity = "REVENUE" Then
                If Not (dtTable.Columns.Contains("BILLED_DPT_AMNT")) Then
                    dtTable.Columns.Add("BILLED_DPT_AMNT", GetType(System.Decimal))
                End If
                If Not (dtTable.Columns.Contains("UNBILLED_DPT_AMNT")) Then
                    dtTable.Columns.Add("UNBILLED_DPT_AMNT", GetType(System.Decimal))
                End If

                If Not (dtTable.Columns.Contains("DPT_AMNT")) Then
                    dtTable.Columns.Add("DPT_AMNT", GetType(System.Decimal))
                End If
            End If
            Dim intFullTrips As Integer = 0
            Dim intEmptySingleTrips As Integer = 0
            Dim intEmptyNonSingleTrips As Integer = 0
            Dim totalTripTime As Integer = 0
            'If bv_strActivity = "KPI" Then
            '    If Not IsDBNull(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(CommonUIData.EQPMNT_STT_ID, "=94"))) Then
            '        intFullTrips = CInt(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(CommonUIData.EQPMNT_STT_ID, "=94")))
            '    End If
            '    If Not IsDBNull(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(CommonUIData.EQPMNT_STT_ID, "=93 AND EMPTY_SNGL_ID=108"))) Then
            '        intEmptySingleTrips = CInt(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(CommonUIData.EQPMNT_STT_ID, "=93 AND EMPTY_SNGL_ID=108")))
            '    End If
            '    If Not IsDBNull(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(CommonUIData.EQPMNT_STT_ID, "=93 AND (EMPTY_SNGL_ID=109 OR EMPTY_SNGL_ID IS NULL)"))) Then
            '        intEmptyNonSingleTrips = CInt(Math.Ceiling(CInt(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(CommonUIData.EQPMNT_STT_ID, "=93 AND (EMPTY_SNGL_ID=109 OR EMPTY_SNGL_ID IS NULL)"))) / 2))
            '    End If
            '    If Not IsDBNull(dtTransportation.Compute("SUM(" & "TRIP_TIME" & ")", String.Empty)) Then
            '        totalTripTime = CInt(dtTransportation.Compute("SUM(" & "TRIP_TIME" & ")", String.Empty))
            '    End If
            'End If
            For Each dr As DataRow In dtTable.Rows
                Dim intCustomerFullTrips As Integer = 0
                Dim intCustomerEmptySingleTrips As Integer = 0
                Dim intCustomerEmptyNonSingleTrips As Integer = 0
                If Not IsDBNull(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND ", CommonUIData.EQPMNT_STT_ID, " IN(94,121,122)"))) Then
                    intCustomerFullTrips = CInt(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND ", CommonUIData.EQPMNT_STT_ID, " IN(94,121,122)")))
                End If
                If Not IsDBNull(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND ", CommonUIData.EQPMNT_STT_ID, " IN(93,120) AND EMPTY_SNGL_ID=108"))) Then
                    intCustomerEmptySingleTrips = CInt(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND ", CommonUIData.EQPMNT_STT_ID, " IN(93,120) AND EMPTY_SNGL_ID=108")))
                End If
                If Not IsDBNull(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND ", CommonUIData.EQPMNT_STT_ID, " IN(93,120) AND (EMPTY_SNGL_ID=109 OR EMPTY_SNGL_ID IS NULL)"))) Then
                    intCustomerEmptyNonSingleTrips = CInt(Math.Ceiling(CInt(dtTransportation.Compute("COUNT(" & CommonUIData.EQPMNT_NO & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND ", CommonUIData.EQPMNT_STT_ID, " IN(93,120) AND (EMPTY_SNGL_ID=109 OR EMPTY_SNGL_ID IS NULL)"))) / 2))
                End If
                dr("NO_OF_TRIPS") = intCustomerFullTrips + intCustomerEmptySingleTrips + intCustomerEmptyNonSingleTrips
                If bv_strActivity = "KPI" Then
                    Dim totalCustomerTripTime As Integer = 0
                    If Not IsDBNull(dtTransportation.Compute("SUM(" & "TRIP_TIME" & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy))))) Then
                        totalCustomerTripTime = CInt(dtTransportation.Compute("SUM(" & "TRIP_TIME" & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)))))
                    End If
                    Dim datMinJobStartDate As Date
                    Dim datMaxJobEndDate As Date
                    If Not IsDBNull(dtTransportation.Compute("MIN(" & CommonUIData.JB_STRT_DT & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy))))) Then
                        datMinJobStartDate = CDate(dtTransportation.Compute("MIN(" & CommonUIData.JB_STRT_DT & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)))))
                    End If
                    If Not IsDBNull(dtTransportation.Compute("MAX(" & CommonUIData.JB_END_DT & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy))))) Then
                        datMaxJobEndDate = CDate(dtTransportation.Compute("MAX(" & CommonUIData.JB_END_DT & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)))))
                    End If
                    dr("TRIP_TIME") = totalCustomerTripTime
                    'dr("TOTAL_TRIP_TIME") = totalTripTime
                    If totalCustomerTripTime <> 0 Then
                        dr("AVERAGE_TRIP") = Math.Ceiling((intCustomerFullTrips + intCustomerEmptySingleTrips + intCustomerEmptyNonSingleTrips) / totalCustomerTripTime)
                    Else
                        dr("AVERAGE_TRIP") = 0
                    End If
                    If totalCustomerTripTime <> 0 Then
                        dr("AVERAGE_TRIP_DAYS") = Math.Ceiling(totalCustomerTripTime / (intCustomerFullTrips + intCustomerEmptySingleTrips + intCustomerEmptyNonSingleTrips))
                    Else
                        dr("AVERAGE_TRIP_DAYS") = 0
                    End If
                    If DateDiff(DateInterval.Day, datMinJobStartDate, datMaxJobEndDate) + 1 <> 0 Then
                        dr("AVERAGE_TRIPS_PER_DAYS") = Math.Ceiling((intCustomerFullTrips + intCustomerEmptySingleTrips + intCustomerEmptyNonSingleTrips) / (DateDiff(DateInterval.Day, datMinJobStartDate, datMaxJobEndDate) + 1))
                    Else
                        dr("AVERAGE_TRIPS_PER_DAYS") = 0
                    End If
                ElseIf bv_strActivity = "REVENUE" Then
                    Dim decBilledAmount As Decimal = CDec("0.00")
                    Dim decUnBilledAmount As Decimal = CDec("0.00")
                    If Not IsDBNull(dtTransportation.Compute("SUM(" & "BILLED_DPT_AMNT" & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND BILLED_ID=98"))) Then
                        decBilledAmount = Math.Round(CDec(dtTransportation.Compute("SUM(" & "BILLED_DPT_AMNT" & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND BILLED_ID=98"))), 2)
                    End If
                    If Not IsDBNull(dtTransportation.Compute("SUM(" & "UNBILLED_DPT_AMNT" & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND BILLED_ID=99"))) Then
                        decUnBilledAmount = Math.Round(CDec(dtTransportation.Compute("SUM(" & "UNBILLED_DPT_AMNT" & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND BILLED_ID=99"))), 2)
                    End If
                    dr("BILLED_DPT_AMNT") = decBilledAmount
                    dr("UNBILLED_DPT_AMNT") = decUnBilledAmount
                    dr("DPT_AMNT") = decUnBilledAmount + decBilledAmount
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Rental Report"
    <OperationContract()> _
    Public Function pub_GetRentalDetails(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsRental As New DataSet
        Dim dtRental As New DataTable
        Dim dtRentalDetail As New DataTable
        Try
            Dim PeriodFromDate As Date = Nothing
            Dim PeriodToDate As Date = Nothing
            Dim strOnHireDate As String = String.Empty
            Dim strOffHireDate As String = String.Empty
            Dim strWhere As String = String.Empty
            'Multilocation
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else
                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If
            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If

            If pub_GetParameter("Period From", bv_param) <> "" Then
                PeriodFromDate = CDate(pub_GetParameter("Period From", bv_param))
            End If
            If pub_GetParameter("Period To", bv_param) <> "" Then
                PeriodToDate = CDate(pub_GetParameter("Period To", bv_param))
            End If

            If IsDate(pub_GetParameter("Period From", bv_param)) AndAlso pub_GetParameter("Period To", bv_param) = "" Then
                PeriodToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
            End If

            If pub_GetParameter("Activity", bv_param).Contains("110") AndAlso Not (pub_GetParameter("Activity", bv_param).Contains("111")) _
                           AndAlso pub_GetParameter("Period From", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.OFF_HR_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.OFF_HR_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strOffHireDate = String.Concat(CommonUIData.OFF_HR_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.OFF_HR_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strOnHireDate = "0"
            End If

            If pub_GetParameter("Activity", bv_param).Contains("111") AndAlso Not (pub_GetParameter("Activity", bv_param).Contains("110")) _
                                AndAlso pub_GetParameter("Period From", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.ON_HR_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.ON_HR_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strOnHireDate = String.Concat(CommonUIData.ON_HR_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.ON_HR_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strOffHireDate = "0"
            End If

            If pub_GetParameter("Activity", bv_param).Contains("111") AndAlso pub_GetParameter("Activity", bv_param).Contains("110") _
                                AndAlso pub_GetParameter("Period From", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ((", CommonUIData.OFF_HR_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.OFF_HR_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "') ")
                strWhere = String.Concat(strWhere, " OR (", CommonUIData.ON_HR_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.ON_HR_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "')) ")
                strOffHireDate = String.Concat(CommonUIData.OFF_HR_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.OFF_HR_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                strOnHireDate = String.Concat(CommonUIData.ON_HR_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.ON_HR_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            End If

            If pub_GetParameter("Activity", bv_param).Contains("111") AndAlso Not (pub_GetParameter("Activity", bv_param).Contains("110")) _
                                AndAlso (pub_GetParameter("Period From", bv_param) = "") AndAlso (pub_GetParameter("Period To", bv_param) = "") Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.ON_HR_DT, " IS NOT NULL")
                strOnHireDate = String.Concat(CommonUIData.ON_HR_DT, " IS NOT NULL")
                strOffHireDate = "0"
            ElseIf pub_GetParameter("Activity", bv_param).Contains("110") AndAlso Not (pub_GetParameter("Activity", bv_param).Contains("111")) _
                                AndAlso (pub_GetParameter("Period From", bv_param) = "") AndAlso (pub_GetParameter("Period To", bv_param) = "") Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.OFF_HR_DT, " IS NOT NULL ")
                strOffHireDate = String.Concat(CommonUIData.OFF_HR_DT, " IS NOT NULL")
                strOnHireDate = "0"
            ElseIf pub_GetParameter("Activity", bv_param).Contains("110") AndAlso (pub_GetParameter("Activity", bv_param).Contains("111")) _
                                           AndAlso (pub_GetParameter("Period From", bv_param) = "") AndAlso (pub_GetParameter("Period To", bv_param) = "") Then
                strOffHireDate = String.Concat(CommonUIData.OFF_HR_DT, " IS NOT NULL")
                strOnHireDate = String.Concat(CommonUIData.ON_HR_DT, " IS NOT NULL")

            ElseIf pub_GetParameter("Activity", bv_param).Contains("110") AndAlso (pub_GetParameter("Activity", bv_param).Contains("111")) _
                                       AndAlso (pub_GetParameter("Period From", bv_param) = "") AndAlso (pub_GetParameter("Period To", bv_param) <> "") Then
                strOffHireDate = String.Concat(CommonUIData.OFF_HR_DT, " IS NOT NULL")
                strOnHireDate = String.Concat(CommonUIData.ON_HR_DT, " IS NOT NULL")

            ElseIf pub_GetParameter("Activity", bv_param).Contains("110") AndAlso Not (pub_GetParameter("Activity", bv_param).Contains("111")) _
                                 AndAlso (pub_GetParameter("Period From", bv_param) = "") AndAlso (pub_GetParameter("Period To", bv_param) <> "") Then

                strWhere = String.Concat(strWhere, " AND ", CommonUIData.OFF_HR_DT, " IS NOT NULL ")
                strOffHireDate = String.Concat(CommonUIData.OFF_HR_DT, " IS NOT NULL")
                strOnHireDate = "0"
            ElseIf pub_GetParameter("Activity", bv_param).Contains("111") AndAlso Not (pub_GetParameter("Activity", bv_param).Contains("110")) _
                           AndAlso (pub_GetParameter("Period From", bv_param) = "") AndAlso (pub_GetParameter("Period To", bv_param) <> "") Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.ON_HR_DT, " IS NOT NULL")
                strOnHireDate = String.Concat(CommonUIData.ON_HR_DT, " IS NOT NULL")
                strOffHireDate = "0"

            End If

            If strOnHireDate.ToString.Length > 1 Then
                strOnHireDate = String.Concat("(CASE WHEN ", strOnHireDate, " THEN 1 ELSE 0 END)")
            End If

            If strOffHireDate.ToString.Length > 1 Then
                strOffHireDate = String.Concat("(CASE WHEN ", strOffHireDate, " THEN 1 ELSE 0 END)")
            End If

            dtRental = objCommonUIs.GetRentalReport(strWhere)
            dtRentalDetail = objCommonUIs.GetRentalDetailReport(strOnHireDate, strOffHireDate, strWhere)
            dtRental.TableName = "VM_RENTAL"
            dtRentalDetail.TableName = "VM_RENTAL_DETAIL"
            dsRental.Tables.Add(dtRental)
            dsRental.Tables.Add(dtRentalDetail)
            Return dsRental
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "KPI Rental"
    <OperationContract()> _
    Public Function pub_GetRentalKPIDetails(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsRental As New DataSet
        Dim dtRental As New DataTable
        Dim dtRentalDetail As New DataTable
        Try
            Dim PeriodFromDate As Date = Nothing
            Dim PeriodToDate As Date = Nothing
            Dim strOffHireDateTo As String = String.Empty
            Dim strOffHireDateFrom As String = String.Empty
            Dim strWhere As String = String.Empty

            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If
            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If

            If pub_GetParameter("Off-Hire Date From", bv_param) <> "" Then
                PeriodFromDate = CDate(pub_GetParameter("Off-Hire Date From", bv_param))
                strOffHireDateFrom = CStr(PeriodFromDate)
            End If
            If pub_GetParameter("Off-Hire Date To", bv_param) <> "" Then
                PeriodToDate = CDate(pub_GetParameter("Off-Hire Date To", bv_param))
                strOffHireDateTo = CStr(PeriodToDate)
            End If

            If IsDate(pub_GetParameter("Off-Hire Date From", bv_param)) AndAlso pub_GetParameter("Off-Hire Date To", bv_param) = "" Then
                PeriodToDate = CDate(Now.ToString("dd-MMM-yyyy").ToUpper())
                strOffHireDateTo = CStr(PeriodToDate)
            End If

            If pub_GetParameter("Off-Hire Date From", bv_param) <> "" AndAlso pub_GetParameter("Off-Hire Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.OFF_HR_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.OFF_HR_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                PeriodFromDate = CDate(pub_GetParameter("Off-Hire Date From", bv_param))
                strOffHireDateFrom = CStr(PeriodFromDate)
                PeriodToDate = CDate(pub_GetParameter("Off-Hire Date To", bv_param))
                strOffHireDateTo = CStr(PeriodToDate)
            End If

            If pub_GetParameter("Off-Hire Date From", bv_param) <> "" AndAlso pub_GetParameter("Off-Hire Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.OFF_HR_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.OFF_HR_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
                PeriodFromDate = CDate(pub_GetParameter("Off-Hire Date From", bv_param))
                strOffHireDateFrom = CStr(PeriodFromDate)
                strOffHireDateTo = "0"
            End If

            If pub_GetParameter("Off-Hire Date From", bv_param) = "" AndAlso pub_GetParameter("Off-Hire Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.OFF_HR_DT, " <= ('", PeriodToDate.ToString("dd-MMM-yyyy"), "')  ")
                PeriodToDate = CDate(pub_GetParameter("Off-Hire Date To", bv_param))
                strOffHireDateTo = CStr(PeriodToDate)
                strOffHireDateFrom = "0"
            End If

            If pub_GetParameter("Off-Hire Date From", bv_param) = "" AndAlso pub_GetParameter("Off-Hire Date To", bv_param) = "" Then
                strOffHireDateFrom = String.Concat(CommonUIData.OFF_HR_DT, " IS NOT NULL")
                strOffHireDateTo = String.Concat(CommonUIData.OFF_HR_DT, " IS NOT NULL")
            End If

            If strOffHireDateFrom.ToString.Length > 1 Then
                strOffHireDateFrom = String.Concat("(CASE WHEN ", strOffHireDateFrom, " THEN 1 ELSE 0 END)")
            End If

            If strOffHireDateTo.ToString.Length > 1 Then
                strOffHireDateTo = String.Concat("(CASE WHEN ", strOffHireDateTo, " THEN 1 ELSE 0 END)")
            End If

            dtRental = objCommonUIs.GetRentalKPIReport(strWhere)
            dtRental.TableName = "VM_KPI_RENTAL"
            dsRental.Tables.Add(dtRental)
            Return dsRental
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function

#End Region

#Region "Finance Rental"
    <OperationContract()> _
    Public Function pub_GetRentalRevenueReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsRevenueRental As New DataSet
        Dim dtRevenueRental As New DataTable
        Try
            Dim PeriodFromDate As Date = Nothing
            Dim PeriodToDate As Date = Nothing
            Dim strOnHireDate As String = String.Empty
            Dim strOffHireDate As String = String.Empty
            Dim strWhere As String = String.Empty

            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), ")")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If

            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Invoice Date From", bv_param) <> "" Then
                PeriodFromDate = CDate(pub_GetParameter("Invoice Date From", bv_param))
            End If
            If pub_GetParameter("Invoice Date To", bv_param) <> "" Then
                PeriodToDate = CDate(pub_GetParameter("Invoice Date To", bv_param))
            End If

            If IsDate(pub_GetParameter("Invoice Date From", bv_param)) AndAlso IsDate(pub_GetParameter("Invoice Date To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.TO_BLLNG_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf Not IsDate(pub_GetParameter("Invoice Date From", bv_param)) AndAlso IsDate(pub_GetParameter("Invoice Date To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf IsDate(pub_GetParameter("Invoice Date From", bv_param)) AndAlso Not IsDate(pub_GetParameter("Invoice Date To", bv_param)) Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' ")
            End If
            Dim dtDistinctCustomer As New DataTable
            dtDistinctCustomer = objCommonUIs.GetFinanceDistinctCustomer(String.Concat(" VM_FINANCE_RENTAL ", strWhere))
            If strWhere.Length > 0 Then
                strWhere = String.Concat(strWhere, "GROUP BY EQPMNT_NO,INVC_NO,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_CD,EQPMNT_TYP_CD,PO_RFRNC_NO,CNTRCT_RFRNC_NO,ON_HR_DT,OFF_HR_DT,RNTL_PR_DY_NC,DPT_CD,DPT_CRRNCY_CD,STRG_DYS,INVC_DT,TO_BLLNG_DT,DPT_ID")
            End If

            dtRevenueRental = objCommonUIs.GetFinanceRentalReport(strWhere)
            Dim dsReport As New DataSet
            Dim strCustomer As String
            Dim i As Integer = 0
            If dtDistinctCustomer.Rows.Count > 0 Then

                For Each dr As DataRow In dtDistinctCustomer.Rows
                    i = i + 1
                    If i = dtDistinctCustomer.Rows.Count Then
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")))
                    Else
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")), "/")
                    End If
                Next
            End If

            If Not dtRevenueRental.Columns.Contains("DepotString") Then
                dtRevenueRental.Columns.Add("DepotString", GetType(System.String))
            End If

            For Each dr As DataRow In dtRevenueRental.Rows
                dr.Item("DepotString") = strCustomer
            Next

            dtRevenueRental.TableName = "VM_FINANCE_RENTAL"
            dsRevenueRental.Tables.Add(dtRevenueRental)
            pvt_GetSummaryDetail(dtRevenueRental, dsRevenueRental)

            ' pvt_GetRentalSummary(dtRevenueRental, dsRevenueRental, "REVENUE")

            Return dsRevenueRental
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "pvt_GetSummary"
    Private Function pvt_GetRentalSummary(ByVal dtRental As DataTable, _
                                          ByRef dsTransportation As DataSet, _
                                          ByVal bv_strActivity As String) As Boolean
        Try
            Dim Distinctdata As New DatasetHelpers(CType(dsTransportation, DataSet))
            Dim dtDistinctCustomer As New DataTable
            Dim dtDistinctEqType As New DataTable

            dtDistinctCustomer = Distinctdata.SelectGroupByInto("RENTAL_CUSTOMER_SUMMARY", dtRental, String.Concat(CommonUIData.CSTMR_ID, ",", CommonUIData.CSTMR_CD, ",", CommonUIData.CSTMR_NAM), "", CommonUIData.CSTMR_ID)
            dtDistinctEqType = Distinctdata.SelectGroupByInto("RENTAL_EQUIPMENT_TYPE_SUMMARY", dtRental, String.Concat(CommonUIData.ENM_TYP_ID, ",", CommonUIData.ENM_TYP_CD, ",", CommonUIData.EQPMNT_TYP_DSCRPTN_VC), "", CommonUIData.ENM_TYP_ID)

            pvt_CalculateRentalSummaryValues(dtDistinctCustomer, bv_strActivity, CommonUIData.CSTMR_ID, dtRental)
            pvt_CalculateRentalSummaryValues(dtDistinctEqType, bv_strActivity, CommonUIData.ENM_TYP_ID, dtRental)

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pvt_CalculateSummaryValues()"
    Private Function pvt_CalculateRentalSummaryValues(ByRef dtTable As DataTable, _
                                                      ByVal bv_strActivity As String, _
                                                      ByVal bv_strGroupBy As String, _
                                                      ByVal dtRental As DataTable) As Boolean
        Try
            If bv_strActivity = "REVENUE" Then
                If Not (dtTable.Columns.Contains("BILLED_DPT_AMNT")) Then
                    dtTable.Columns.Add("BILLED_DPT_AMNT", GetType(System.Decimal))
                End If
                If Not (dtTable.Columns.Contains("UNBILLED_DPT_AMNT")) Then
                    dtTable.Columns.Add("UNBILLED_DPT_AMNT", GetType(System.Decimal))
                End If

                If Not (dtTable.Columns.Contains("DPT_AMNT")) Then
                    dtTable.Columns.Add("DPT_AMNT", GetType(System.Decimal))
                End If
            End If
            Dim intFullTrips As Integer = 0
            Dim intEmptySingleTrips As Integer = 0
            Dim intEmptyNonSingleTrips As Integer = 0
            Dim totalTripTime As Integer = 0

            For Each dr As DataRow In dtTable.Rows
                If bv_strActivity = "REVENUE" Then
                    Dim decBilledAmount As Decimal = CDec("0.00")
                    Dim decUnBilledAmount As Decimal = CDec("0.00")
                    If Not IsDBNull(dtRental.Compute("SUM(" & "BILLED_DPT_AMNT" & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND BILLED_ID=98"))) Then
                        decBilledAmount = Math.Round(CDec(dtRental.Compute("SUM(" & "BILLED_DPT_AMNT" & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND BILLED_ID=98"))), 2)
                    End If
                    If Not IsDBNull(dtRental.Compute("SUM(" & "UNBILLED_DPT_AMNT" & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND BILLED_ID=99"))) Then
                        decUnBilledAmount = Math.Round(CDec(dtRental.Compute("SUM(" & "UNBILLED_DPT_AMNT" & ")", String.Concat(bv_strGroupBy, " = ", CStr(dr.Item(bv_strGroupBy)), " AND BILLED_ID=99"))), 2)
                    End If
                    dr("BILLED_DPT_AMNT") = decBilledAmount
                    dr("UNBILLED_DPT_AMNT") = decUnBilledAmount
                    dr("DPT_AMNT") = decUnBilledAmount + decBilledAmount
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Finance Handling and Storage"
    <OperationContract()> _
    Public Function pub_GetHandlingAndStorageRevenueReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsHandlingStorage As New DataSet
        Dim dtHandlingStorage As New DataTable
        Try
            Dim strWhere As String = String.Empty
            Dim IndatFromDate As Date = Nothing
            Dim IndatToDate As Date = Nothing
            Dim EstimatedatFromDate As Date = Nothing
            Dim EstimatedatToDate As Date = Nothing
            Dim ApprovaldatFromDate As Date = Nothing
            Dim ApprovaldatToDate As Date = Nothing
            Dim CompletiondatFromDate As Date = Nothing
            Dim CompletiondatToDate As Date = Nothing

            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If

            If pub_GetParameter("Previous Cargo", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.PRDCT_ID, " IN (", pub_GetParameter("Previous Cargo", bv_param), ") ")
            End If
            If pub_GetParameter("Customer/Agent", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer/Agent", bv_param), ") OR ", CommonUIData.AGNT_ID, " IN (", pub_GetParameter("Customer/Agent", bv_param), " ))")
            End If

            If pub_GetParameter("Customer", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.CSTMR_ID, " IN (", pub_GetParameter("Customer", bv_param), "))")
            End If

            If pub_GetParameter("Equipment No", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_NO, " ='", pub_GetParameter("Equipment No", bv_param), "' ")
            End If
            If pub_GetParameter("Equipment Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.EQPMNT_TYP_ID, " IN (", pub_GetParameter("Equipment Type", bv_param), ") ")
            End If

            If pub_GetParameter("Invoice Date From", bv_param) <> "" Then
                IndatFromDate = CDate(pub_GetParameter("Invoice Date From", bv_param))
            End If
            If pub_GetParameter("Invoice Date To", bv_param) <> "" Then
                IndatToDate = CDate(pub_GetParameter("Invoice Date To", bv_param))
            End If

            If pub_GetParameter("Invoice Date From", bv_param) <> "" AndAlso pub_GetParameter("Invoice Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.TO_BLLNG_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "' AND ", CommonUIData.TO_BLLNG_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Invoice Date From", bv_param) <> "" AndAlso pub_GetParameter("Invoice Date To", bv_param) = "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.TO_BLLNG_DT, " >= '", IndatFromDate.ToString("dd-MMM-yyyy"), "') ")
            ElseIf pub_GetParameter("Invoice Date From", bv_param) = "" AndAlso pub_GetParameter("Invoice Date To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND (", CommonUIData.TO_BLLNG_DT, " <= '", IndatToDate.ToString("dd-MMM-yyyy"), "') ")
            End If
            Dim dtDistinctCustomer As New DataTable
            dtDistinctCustomer = objCommonUIs.GetFinanceDistinctCustomer(String.Concat("VM_FINANCE_HANDLINGANDSTORAGE ", strWhere))
            If strWhere.Length > 0 Then
                ''   If CBool(GetMultiLocationSupportConfig()) Then
                strWhere = String.Concat(strWhere, "GROUP BY EQPMNT_NO,CSTMR_ID,AGNT_ID,CSTMR_CD,AGNT_CD,CSTMR_NAM,AGNT_NAM,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,GTN_DT,CSTMR_CRRNCY_ID,CSTMR_CRRNCY_CD,CRRNCY_DSCRPTN_VC,TO_BLLNG_DT,INVC_NO,STRG_FRM_BLLNG_DT,STRG_TO_BLLNG_DT,FR_DYS,DPT_CRRNCY_CD,STRG_AMNT_NC,STRG_DYS_AGNT,CSTMR_CRRNCY_ID_AGNT,FR_DYS_AGNT,DPT_ID")
                ' Else
                ' strWhere = String.Concat(strWhere, "GROUP BY EQPMNT_NO,CSTMR_ID,AGNT_ID,CSTMR_CD,AGNT_CD,CSTMR_NAM,AGNT_NAM,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,GTN_DT,CSTMR_CRRNCY_ID,CSTMR_CRRNCY_CD,CRRNCY_DSCRPTN_VC,TO_BLLNG_DT,INVC_NO,STRG_FRM_BLLNG_DT,STRG_TO_BLLNG_DT,FR_DYS,DPT_CRRNCY_CD,STRG_AMNT_NC,STRG_DYS_AGNT,CSTMR_CRRNCY_ID_AGNT,FR_DYS_AGNT")
                'End If

            End If

            dtHandlingStorage = objCommonUIs.GetFinanceHandlingAndStorageReport(strWhere)

            Dim dsReport As New DataSet
            Dim strCustomer As String
            Dim i As Integer = 0
            If dtDistinctCustomer.Rows.Count > 0 Then
                For Each dr As DataRow In dtDistinctCustomer.Rows
                    i = i + 1
                    If i = dtDistinctCustomer.Rows.Count Then
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")))
                    Else
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")), "/")
                    End If
                Next
            End If

            If Not dtHandlingStorage.Columns.Contains("DepotString") Then
                dtHandlingStorage.Columns.Add("DepotString", GetType(System.String))
            End If

            For Each dr As DataRow In dtHandlingStorage.Rows
                dr.Item("DepotString") = strCustomer
            Next

            dtHandlingStorage.TableName = "VM_FINANCE_HANDLINGANDSTORAGE"
            dsHandlingStorage.Tables.Add(dtHandlingStorage)
            Return dsHandlingStorage
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "pvt_GetSummaryDetail"
    Private Sub pvt_GetSummaryDetail(ByVal bv_dtCommon As DataTable, _
                                     ByRef br_dsRental As DataSet)
        Try
            Dim drRow As DataRow = Nothing
            Dim dtSummary As New DataTable
            If Not (dtSummary.Columns.Contains(CommonUIData.CSTMR_CD)) Then
                dtSummary.Columns.Add(CommonUIData.CSTMR_CD, GetType(System.String))
            End If
            If Not (dtSummary.Columns.Contains(CommonUIData.EQPMNT_TYP_CD)) Then
                dtSummary.Columns.Add(CommonUIData.EQPMNT_TYP_CD, GetType(System.String))
            End If
            If Not (dtSummary.Columns.Contains(CommonUIData.ON_HR_SRVY_NC)) Then
                dtSummary.Columns.Add(CommonUIData.ON_HR_SRVY_NC, GetType(System.Decimal))
            End If
            If Not (dtSummary.Columns.Contains(CommonUIData.HNDLNG_OT_NC)) Then
                dtSummary.Columns.Add(CommonUIData.HNDLNG_OT_NC, GetType(System.Decimal))
            End If
            If Not (dtSummary.Columns.Contains(CommonUIData.HNDLNG_IN_NC)) Then
                dtSummary.Columns.Add(CommonUIData.HNDLNG_IN_NC, GetType(System.Decimal))
            End If
            If Not (dtSummary.Columns.Contains(CommonUIData.ON_HR_EQPMNT)) Then
                dtSummary.Columns.Add(CommonUIData.ON_HR_EQPMNT, GetType(System.String))
            End If
            If Not (dtSummary.Columns.Contains(CommonUIData.OFF_HR_SRVY_NC)) Then
                dtSummary.Columns.Add(CommonUIData.OFF_HR_SRVY_NC, GetType(System.Decimal))
            End If
            If Not (dtSummary.Columns.Contains(CommonUIData.OFF_HR_EQPMNT)) Then
                dtSummary.Columns.Add(CommonUIData.OFF_HR_EQPMNT, GetType(System.String))
            End If
            If Not (dtSummary.Columns.Contains(CommonUIData.RNTL_CHRG_NC)) Then
                dtSummary.Columns.Add(CommonUIData.RNTL_CHRG_NC, GetType(System.Decimal))
            End If
            If Not (dtSummary.Columns.Contains(CommonUIData.RNTL_CHRG_EQPMNT)) Then
                dtSummary.Columns.Add(CommonUIData.RNTL_CHRG_EQPMNT, GetType(System.String))
            End If
            For Each drCommon As DataRow In bv_dtCommon.Rows
                drRow = dtSummary.NewRow()
                drRow.Item(CommonUIData.CSTMR_CD) = drCommon.Item(CommonUIData.CSTMR_CD)
                drRow.Item(CommonUIData.EQPMNT_TYP_CD) = drCommon.Item(CommonUIData.EQPMNT_TYP_CD)
                If Not IsDBNull(drCommon.Item(CommonUIData.ON_HR_SRVY_NC)) AndAlso CDec(drCommon.Item(CommonUIData.ON_HR_SRVY_NC)) <> 0 Then
                    drRow.Item(CommonUIData.ON_HR_EQPMNT) = drCommon.Item(CommonUIData.EQPMNT_NO)
                    drRow.Item(CommonUIData.HNDLNG_OT_NC) = drCommon.Item(CommonUIData.HNDLNG_OT_AMNT_NC)
                    drRow.Item(CommonUIData.ON_HR_SRVY_NC) = drCommon.Item(CommonUIData.ON_HR_SRVY_NC)
                End If
                If Not IsDBNull(drCommon.Item(CommonUIData.OFF_HR_SRVY_NC)) AndAlso CDec(drCommon.Item(CommonUIData.OFF_HR_SRVY_NC)) <> 0 Then
                    drRow.Item(CommonUIData.OFF_HR_EQPMNT) = drCommon.Item(CommonUIData.EQPMNT_NO)
                    drRow.Item(CommonUIData.OFF_HR_SRVY_NC) = drCommon.Item(CommonUIData.OFF_HR_SRVY_NC)
                    drRow.Item(CommonUIData.HNDLNG_IN_NC) = drCommon.Item(CommonUIData.HNDLNG_IN_AMNT_NC)
                End If
                If Not IsDBNull(drCommon.Item(CommonUIData.STRG_AMNT_NC)) Then
                    drRow.Item(CommonUIData.RNTL_CHRG_NC) = drCommon.Item(CommonUIData.STRG_AMNT_NC)
                    drRow.Item(CommonUIData.RNTL_CHRG_EQPMNT) = drCommon.Item(CommonUIData.EQPMNT_NO)
                End If
                dtSummary.Rows.Add(drRow)
            Next
            dtSummary.TableName = "VM_FINANCE_RENTAL_SUMMARY"
            If br_dsRental.Tables.Contains("VM_FINANCE_RENTAL_SUMMARY") Then
                br_dsRental.Tables.Remove("VM_FINANCE_RENTAL_SUMMARY")
            End If
            br_dsRental.Tables.Add(dtSummary)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Sub
#End Region

#Region "pub_getIdentityValue()"
    Public Function pub_getIdentityValue(ByVal bv_strTableName As String, _
                                         ByVal bv_strSelectColumn As String, _
                                         ByVal bv_strOrderByColumn As String, _
                                         ByVal bv_strOrder As String, _
                                         ByVal bv_blnFormatValue As Boolean, _
                                         ByVal bv_intIncrmentValue As Int32) As String
        Try
            Dim strValue As String = String.Empty
            Dim objCommonUIs As New CommonUIs
            strValue = objCommonUIs.GetIdentityValue(bv_strTableName, bv_strSelectColumn, bv_strOrderByColumn, bv_strOrder, bv_blnFormatValue, bv_intIncrmentValue)
            Return strValue
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GetNextIndex"
    ''' <summary>
    ''' This method is used to get next index from data table
    ''' </summary>
    ''' <param name="br_table">Denotes Datatable box oject</param>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Shared Function GetNextIndex(ByRef br_table As System.Data.DataTable, ByVal bv_colname As String) As Long
        Dim maxcount As Long
        Dim objvalue As Object

        objvalue = br_table.Compute(String.Concat("max(", bv_colname, ")"), "")
        If objvalue Is DBNull.Value Then
            maxcount = 0
        Else
            maxcount = CType(objvalue, Integer)
        End If
        maxcount = maxcount + 1

        objvalue = Nothing
        Return maxcount
    End Function
#End Region

#Region "pub_GetEquipmentStaus"
    <OperationContract()> _
    Public Function pub_GetEquipmentStaus(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotId As Int32) As String
        Try
            Dim objActivityStatuss As New CommonUIs
            Dim strEquipmentStatus As String = objActivityStatuss.GetEquipment_Status(bv_strEquipmentNo, bv_i32DepotId)
            Return strEquipmentStatus
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_validateFinalizedInvoiceActivityWise"
    ''' <summary>
    ''' Activity Submit
    ''' </summary>
    ''' <param name="bv_strTableName"></param>
    ''' <param name="bv_i32DepotID"></param>
    ''' <param name="bv_strTransactionID"></param>
    ''' <param name="bv_strTransactionNo"></param>
    ''' <param name="bv_strEquipmentNo"></param>
    ''' <param name="br_strErrorMessage"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_validateFinalizedInvoiceActivityWise(ByVal bv_strTableName As String, _
                                                             ByVal bv_i32DepotID As Int64, _
                                                             ByVal bv_strTransactionID As String, _
                                                             ByVal bv_strTransactionNo As String, _
                                                             ByVal bv_strEquipmentNo As String, _
                                                             ByRef br_strErrorMessage As String, _
                                                             ByVal bv_dsTable As DataSet) As Boolean
        Try
            Dim objCommonUIs As New CommonUIs
            Dim i32TransactionCount As Int32 = 0
            Dim strQuery As String = ""
            'If bv_strTableName = CommonUIData._TRANSPORTATION_CHARGE Then
            '    strQuery = String.Concat("SELECT COUNT(TRNSPRTTN_CHRG_ID) FROM ", CommonUIData._TRANSPORTATION_CHARGE, " WHERE  BLLNG_FLG='B' AND EQPMNT_NO IN (", bv_strEquipmentNo, ") AND TRNSPRTTN_ID IN (", bv_strTransactionID, ") AND DPT_ID=", bv_i32DepotID)
            If bv_strTableName = CommonUIData._RENTAL_CHARGE Then
                Dim datOffHire As DateTime
                For Each drRental As DataRow In bv_dsTable.Tables(RentalEntryData._V_RENTAL_ENTRY).Select(RentalEntryData.CHECKED & "='True'")
                    If Not IsDBNull(drRental.Item(RentalEntryData.OFF_HR_DT)) Then
                        Dim datBillngTilDt As String
                        datOffHire = CDate(drRental.Item(RentalEntryData.OFF_HR_DT))
                        datBillngTilDt = objCommonUIs.GetBllngTilDt(CStr(drRental.Item(RentalEntryData.EQPMNT_NO)), CStr(drRental.Item("RNTL_RFRNC_NO")))
                        If datBillngTilDt <> "" Then
                            If Not IsDBNull(datBillngTilDt) And datOffHire <= CDate(datBillngTilDt) Then
                                br_strErrorMessage = "True"
                                Return True
                            End If
                        Else
                            Return True
                        End If

                        'strQuery = String.Concat("SELECT COUNT(RNTL_CHRG_ID) FROM ", CommonUIData._RENTAL_CHARGE, " WHERE (BLLNG_TLL_DT >= ", CDate(drRental.Item(RentalEntryData.OFF_HR_DT)), ") AND ACTV_BT=1 AND RNTL_RFRNC_NO IN (", bv_strTransactionNo, ")  AND DPT_ID=", bv_i32DepotID)
                    End If
                Next
                'strQuery = String.Concat("SELECT COUNT(RNTL_CHRG_ID) FROM ", CommonUIData._RENTAL_CHARGE, " WHERE  BLLNG_FLG='B' AND ACTV_BT=1 AND RNTL_RFRNC_NO IN (", bv_strTransactionNo, ")  AND DPT_ID=", bv_i32DepotID)
                'strQuery = String.Concat("SELECT COUNT(RNTL_CHRG_ID) FROM ", CommonUIData._RENTAL_CHARGE, " WHERE  (BLLNG_FLG='B' OR RNTL_CNTN_FLG='S') AND ACTV_BT=1 AND RNTL_RFRNC_NO IN (", bv_strTransactionNo, ")  AND DPT_ID=", bv_i32DepotID)
            ElseIf bv_strTableName = CommonUIData._CLEANING_CHARGE Then
                strQuery = String.Concat("SELECT COUNT(CLNNG_CHRG_ID) FROM ", CommonUIData._CLEANING_CHARGE, " WHERE  BLLNG_FLG='B' AND ACTV_BT=1 AND CLNNG_ID IN (", bv_strTransactionID, ")  AND DPT_ID=", bv_i32DepotID)
            ElseIf bv_strTableName = CommonUIData._HEATING_CHARGE Then
                strQuery = String.Concat("SELECT COUNT(HTNG_ID) FROM ", CommonUIData._HEATING_CHARGE, " WHERE  BLLNG_FLG='B' AND HTNG_CD IN (", bv_strTransactionID, ")  AND DPT_ID=", bv_i32DepotID)
            ElseIf bv_strTableName = CommonUIData._MISCELLANEOUS_INVOICE Then
                strQuery = String.Concat("SELECT COUNT(MSCLLNS_INVC_ID) FROM ", CommonUIData._MISCELLANEOUS_INVOICE, " WHERE  BLLNG_FLG='B' AND MSCLLNS_INVC_ID IN (", bv_strTransactionID, ")  AND DPT_ID=", bv_i32DepotID)
            ElseIf bv_strTableName = CommonUIData._REPAIR_CHARGE Then
                strQuery = String.Concat("SELECT COUNT(RPR_CHRG_ID) FROM ", CommonUIData._REPAIR_CHARGE, " WHERE  BLLNG_FLG='B'  AND ACTV_BT=1 AND RPR_CHRG_ID IN (", bv_strTransactionID, ")  AND DPT_ID=", bv_i32DepotID)
            End If
            If strQuery <> "" Then
                i32TransactionCount = objCommonUIs.validateFinalizedInvoiceActivityWise(strQuery)
            End If
            If i32TransactionCount > 0 Then
                br_strErrorMessage = "True"
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    ''pvt_GetCleaningMethodDetail
#Region "pvt_GetCleaningMethodDetail"
    Public Function pvt_GetCleaningMethodDetail(ByVal bv_i32DepotID As Int32, ByRef strcolumnlist As String) As CommonUIDataSet
        Dim dsCleaningType As New CommonUIDataSet
        Dim objCommonUIs As New CommonUIs
        Dim br_dtCleaningMethod As New DataTable
        Try
            Dim dsCleaningMethod As New CommonUIDataSet
            Dim drRow As DataRow = Nothing
            Dim strcleaningType1 As String = ""
            Dim strcleaningType2 As String = ""
            Dim strcleaningType3 As String = ""
            Dim strcleaningType4 As String = ""
            Dim strcleaningType5 As String = ""
            Dim strcleaningType6 As String = ""
            Dim strcleaningType7 As String = ""
            Dim strCleaningTypeIDs As String = "2,3,7,8,9,10,11"
            ''Get Cleaning Types
            dsCleaningType = objCommonUIs.pub_GetCleaningTypes(String.Concat("SELECT CLNNG_TYP_ID,CLNNG_TYP_DSCRPTN_VC FROM CLEANING_TYPE WHERE CLNNG_TYP_ID IN(", strCleaningTypeIDs, ") AND DPT_ID=", bv_i32DepotID))
            If dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows.Count > 0 Then
                strcleaningType1 = dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows(0).Item("CLNNG_TYP_DSCRPTN_VC").ToString
            End If
            If dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows.Count > 1 Then
                strcleaningType2 = dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows(1).Item("CLNNG_TYP_DSCRPTN_VC").ToString
            End If
            If dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows.Count > 2 Then
                strcleaningType3 = dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows(2).Item("CLNNG_TYP_DSCRPTN_VC").ToString
            End If
            If dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows.Count > 3 Then
                strcleaningType4 = dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows(3).Item("CLNNG_TYP_DSCRPTN_VC").ToString
            End If
            If dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows.Count > 4 Then
                strcleaningType5 = dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows(4).Item("CLNNG_TYP_DSCRPTN_VC").ToString
            End If
            If dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows.Count > 5 Then
                strcleaningType6 = dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows(5).Item("CLNNG_TYP_DSCRPTN_VC").ToString
            End If
            If dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows.Count > 6 Then
                strcleaningType7 = dsCleaningType.Tables(CommonUIData._CLEANING_TYPE).Rows(6).Item("CLNNG_TYP_DSCRPTN_VC").ToString
            End If

            ''Schema generation for Export
            ''26506
            br_dtCleaningMethod.TableName = "CLEANING_METHOD_EXPORT"
            If Not (br_dtCleaningMethod.Columns.Contains("Type")) Then
                br_dtCleaningMethod.Columns.Add("Type", GetType(System.String))
                strcolumnlist = "Type"
            End If
            '' Add Default columns
            If strcleaningType1 <> "" Then
                If Not (br_dtCleaningMethod.Columns.Contains(strcleaningType1)) Then
                    br_dtCleaningMethod.Columns.Add(strcleaningType1, GetType(System.String))
                    strcolumnlist = String.Concat(strcolumnlist, ",", strcleaningType1)
                End If
            End If
            If strcleaningType2 <> "" Then
                If Not (br_dtCleaningMethod.Columns.Contains(strcleaningType2)) Then
                    br_dtCleaningMethod.Columns.Add(strcleaningType2, GetType(System.String))
                    strcolumnlist = String.Concat(strcolumnlist, ",", strcleaningType2)
                End If
            End If
            If strcleaningType3 <> "" Then
                If Not (br_dtCleaningMethod.Columns.Contains(strcleaningType3)) Then
                    br_dtCleaningMethod.Columns.Add(strcleaningType3, GetType(System.String))
                    strcolumnlist = String.Concat(strcolumnlist, ",", strcleaningType3)
                End If
            End If
            If strcleaningType4 <> "" Then
                If Not (br_dtCleaningMethod.Columns.Contains(strcleaningType4)) Then
                    br_dtCleaningMethod.Columns.Add(strcleaningType4, GetType(System.String))
                    strcolumnlist = String.Concat(strcolumnlist, ",", strcleaningType4)
                End If
            End If
            If strcleaningType5 <> "" Then
                If Not (br_dtCleaningMethod.Columns.Contains(strcleaningType5)) Then
                    br_dtCleaningMethod.Columns.Add(strcleaningType5, GetType(System.String))
                    strcolumnlist = String.Concat(strcolumnlist, ",", strcleaningType5)
                End If
            End If
            If strcleaningType6 <> "" Then
                If Not (br_dtCleaningMethod.Columns.Contains(strcleaningType6)) Then
                    br_dtCleaningMethod.Columns.Add(strcleaningType6, GetType(System.String))
                    strcolumnlist = String.Concat(strcolumnlist, ",", strcleaningType6)
                End If
            End If
            If strcleaningType7 <> "" Then
                If Not (br_dtCleaningMethod.Columns.Contains(strcleaningType7)) Then
                    br_dtCleaningMethod.Columns.Add(strcleaningType7, GetType(System.String))
                    strcolumnlist = String.Concat(strcolumnlist, ",", strcleaningType7)
                End If
            End If

            dsCleaningMethod = objCommonUIs.pub_GetCleaningMethodDetail(bv_i32DepotID)
            If dsCleaningMethod.Tables(CommonUIData._V_CLEANING_METHOD_DETAIL).Rows.Count > 0 Then
                For Each dr As DataRow In dsCleaningMethod.Tables(CommonUIData._V_CLEANING_METHOD_DETAIL).Rows
                    If Not (br_dtCleaningMethod.Columns.Contains(dr.Item(CommonUIData.CLNNG_TYP_DSCRPTN_VC).ToString)) Then
                        br_dtCleaningMethod.Columns.Add(dr.Item(CommonUIData.CLNNG_TYP_DSCRPTN_VC).ToString, GetType(System.String))
                        strcolumnlist = String.Concat(strcolumnlist, ",", dr.Item(CommonUIData.CLNNG_TYP_DSCRPTN_VC).ToString)
                    End If
                Next
            End If

            If Not (br_dtCleaningMethod.Columns.Contains("Active")) Then
                br_dtCleaningMethod.Columns.Add("Active", GetType(System.String))
                strcolumnlist = String.Concat(strcolumnlist, ",", "Active")
            End If

            '''''''''
            If Not dsCleaningMethod.Tables.Contains("CLEANING_METHOD_EXPORT") Then
                dsCleaningMethod.Tables.Add(br_dtCleaningMethod)
            End If

            ''Fill Type in to Dataset Table 

            dsCleaningType = objCommonUIs.pub_GetCleaningMethodTypes(bv_i32DepotID)

            Dim strType As String = ""
            If dsCleaningType.Tables(CommonUIData._V_CLEANING_METHOD_DETAIL).Rows.Count > 0 Then
                For Each dr As DataRow In dsCleaningType.Tables(CommonUIData._V_CLEANING_METHOD_DETAIL).Rows
                    If strType = "" Then
                        strType = dr.Item("CLNNG_MTHD_TYP_CD").ToString
                        ''Add new row
                        Dim drFields As DataRow
                        drFields = dsCleaningMethod.Tables("CLEANING_METHOD_EXPORT").NewRow()
                        drFields.Item("Type") = strType
                        drFields.Item("Active") = CBool(dr.Item("ACTV_BT")).ToString
                        dsCleaningMethod.Tables("CLEANING_METHOD_EXPORT").Rows.Add(drFields)
                    Else
                        If strType <> dr.Item("CLNNG_MTHD_TYP_CD").ToString Then
                            ''Add new row
                            Dim drFields As DataRow
                            drFields = dsCleaningMethod.Tables("CLEANING_METHOD_EXPORT").NewRow()
                            drFields.Item("Type") = dr.Item("CLNNG_MTHD_TYP_CD").ToString
                            drFields.Item("Active") = CBool(dr.Item("ACTV_BT")).ToString
                            dsCleaningMethod.Tables("CLEANING_METHOD_EXPORT").Rows.Add(drFields)
                            ''Add new row
                        End If
                    End If
                Next
            End If
            ''
            ''Fill Column values
            For Each dr As DataRow In dsCleaningMethod.Tables("CLEANING_METHOD_EXPORT").Rows
                For Each dc As DataColumn In dsCleaningMethod.Tables("CLEANING_METHOD_EXPORT").Columns
                    If dc.ColumnName <> "Type" AndAlso dc.ColumnName <> "Active" Then
                        Dim drfilter As DataRow()
                        drfilter = dsCleaningMethod.Tables(CommonUIData._V_CLEANING_METHOD_DETAIL).Select(String.Concat("CLNNG_MTHD_TYP_CD ='", dr.Item("Type"), "'AND CLNNG_TYP_DSCRPTN_VC='", dc.ColumnName, "'"))
                        If drfilter.Length > 0 Then
                            dr.Item(dc.ColumnName) = drfilter(0).Item("CMMNTS_VC")
                        Else
                            dr.Item(dc.ColumnName) = DBNull.Value
                        End If

                    End If
                Next
            Next
            ''
            Return dsCleaningMethod
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

    'Invoice Cancel
#Region "Opration Invoice Cancel"
    <OperationContract()> _
    Public Function pub_GetInvoiceCancelReport(ByRef bv_param As String, ByVal intDepotID As Integer, ByVal bv_ReportName As String) As DataSet
        Dim objCommonUIs As New CommonUIs
        Dim dsInvoiceCancel As New DataSet
        Dim dtInvoiceRegisterCancel As New DataTable
        Dim dtInvoiceRegisterCancelDetails As New DataTable

        Try
            Dim PeriodFromDate As DateTime = Nothing
            Dim PeriodToDate As DateTime = Nothing
            Dim CancelDate As DateTime = Nothing
            Dim strWhere As String = String.Empty
            'multiDepo
            If pub_GetParameter("Depot", bv_param) <> "" Then
                strWhere = String.Concat(" WHERE DPT_ID IN (", pub_GetParameter("Depot", bv_param), ")")
            Else                strWhere = String.Concat(" WHERE DPT_ID = ", intDepotID)
            End If
            Dim strDetailWhere As New StringBuilder
            strDetailWhere.Append("WHERE INVC_CNCL_ID IN (")

            'Paramter information
            If pub_GetParameter("Activity Type", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND INVC_TYP_ID IN (", pub_GetParameter("Activity Type", bv_param), ") ")
            End If

            If pub_GetParameter("Invoicing To", bv_param) <> "" Then
                strWhere = String.Concat(strWhere, " AND ", InvoiceReversalData.INVC_TO_ID, " IN (", pub_GetParameter("Invoicing To", bv_param), ")")
            End If

            If pub_GetParameter("From Billing Date", bv_param) <> "" Then
                PeriodFromDate = CDate(pub_GetParameter("From Billing Date", bv_param))
            End If
            If pub_GetParameter("To Billing Date", bv_param) <> "" Then
                PeriodToDate = CDate(pub_GetParameter("To Billing Date", bv_param))
            End If

            If pub_GetParameter("Cancellation Date", bv_param) <> "" Then
                CancelDate = CDate(pub_GetParameter("Cancellation Date", bv_param))
            End If

            'If IsDate(pub_GetParameter("Invoice Date From", bv_param)) AndAlso IsDate(pub_GetParameter("Invoice Date To", bv_param)) Then
            '    strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.TO_BLLNG_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            'ElseIf Not IsDate(pub_GetParameter("Invoice Date From", bv_param)) AndAlso IsDate(pub_GetParameter("Invoice Date To", bv_param)) Then
            '    strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            'ElseIf IsDate(pub_GetParameter("Invoice Date From", bv_param)) AndAlso Not IsDate(pub_GetParameter("Invoice Date To", bv_param)) Then
            '    strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' ")
            'End If

            If PeriodFromDate <> Nothing AndAlso PeriodToDate <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " >= ('", PeriodFromDate.ToString("dd-MMM-yyyy"), "') AND ", CommonUIData.TO_BLLNG_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf PeriodFromDate = Nothing AndAlso PeriodToDate <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " <= '", PeriodToDate.ToString("dd-MMM-yyyy"), "' ")
            ElseIf PeriodFromDate <> Nothing AndAlso PeriodToDate = Nothing Then
                strWhere = String.Concat(strWhere, " AND ", CommonUIData.TO_BLLNG_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' ")
            End If

            'If IsDate(pub_GetParameter("Cancellation Date", bv_param)) AndAlso Not IsDate(pub_GetParameter("Cancellation Date", bv_param)) Then
            '    strWhere = String.Concat(strWhere, " AND ", InvoiceReversalData.INVC_CNCLD_DT, " >= '", PeriodFromDate.ToString("dd-MMM-yyyy"), "' ")
            'End If

            If CancelDate <> Nothing Then
                strWhere = String.Concat(strWhere, " AND ", InvoiceReversalData.INVC_CNCLD_DT, " >= '", CancelDate.ToString("dd-MMM-yyyy"), "' ")
            End If

            dtInvoiceRegisterCancel = objCommonUIs.pub_GetInvoiceCancelReport(strWhere)
            dtInvoiceRegisterCancel.TableName = "INVOICE_CANCEL"

            If dtInvoiceRegisterCancel.Rows.Count > 0 Then

                'Details Information
                For Each dr As DataRow In dtInvoiceRegisterCancel.Rows
                    strDetailWhere.Append(dr.Item(InvoiceReversalData.INVC_CNCL_ID))
                    strDetailWhere.Append(",")
                Next
                strDetailWhere.Length = strDetailWhere.Length - 1
                strDetailWhere.Append(")")
                'strDetailWhere
                dtInvoiceRegisterCancelDetails = objCommonUIs.pub_GetInvoiceCancelDetailReport(strDetailWhere.ToString())

            End If
            'Multi Depo

            Dim dtDistinctCustomer As New DataTable
            Dim dsReport As New DataSet
            Dim strCustomer As String
            Dim i As Integer = 0
            dtDistinctCustomer = objCommonUIs.GetFinanceDistinctCustomer(String.Concat("V_INVOICE_CANCEL ", strWhere))
            If dtDistinctCustomer.Rows.Count > 0 Then

                For Each dr As DataRow In dtDistinctCustomer.Rows
                    i = i + 1
                    If i = dtDistinctCustomer.Rows.Count Then
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")))
                    Else
                        strCustomer = String.Concat(strCustomer, CStr(dr.Item("DPT_CRRNCY_CD")), "/")
                    End If
                Next
            End If

            If Not dtInvoiceRegisterCancel.Columns.Contains("DepotString") Then
                dtInvoiceRegisterCancel.Columns.Add("DepotString", GetType(System.String))
            End If

            For Each dr As DataRow In dtInvoiceRegisterCancel.Rows
                dr.Item("DepotString") = strCustomer
            Next
            If Not dtInvoiceRegisterCancelDetails.Columns.Contains("DepotString") Then
                dtInvoiceRegisterCancelDetails.Columns.Add("DepotString", GetType(System.String))
            End If

            For Each dr As DataRow In dtInvoiceRegisterCancelDetails.Rows
                dr.Item("DepotString") = strCustomer
            Next

            dtInvoiceRegisterCancelDetails.TableName = "INVOICE_CANCEL_DETAIL"
            dsInvoiceCancel.Tables.Add(dtInvoiceRegisterCancel)
            dsInvoiceCancel.Tables.Add(dtInvoiceRegisterCancelDetails)

            Return dsInvoiceCancel
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objCommonUIs = Nothing
        End Try
    End Function
#End Region

#Region "pub_GetConfigTemplate() TABLE NAME:CONFIG"
    <OperationContract()> _
    Public Function pub_GetConfigByKey(ByVal bv_strKeyName As String, ByVal bv_i64DepotId As Int64) As Boolean

        Try
            Dim intDepotID As Int64 = bv_i64DepotId
            Dim strGetMultiLocationSupport As String = GetMultiLocationSupportConfig()
            If strGetMultiLocationSupport.ToLower = "true" Then
                intDepotID = CLng(GetHeadQuarterID())
            End If
            Dim dsConfigData As ConfigDataSet
            Dim objConfigs As New CommonUIs
            dsConfigData = objConfigs.GetConfigByKeyName(bv_strKeyName, intDepotID)
            Return CBool(DecryptString(dsConfigData.Tables(CommonUIData._CONFIG).Rows(0).Item(CommonUIData.KY_VL).ToString))
            'If dsConfigData.Tables(0).Rows.Count = 0 Then
            '    Return False
            'Else
            '    Return True
            'End If

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetDepoDetail()"
    <OperationContract()> _
    Public Function pub_GetDepoDetail(ByVal bv_intDepotID As String) As CommonUIDataSet
        Try
            Dim dsDepot As CommonUIDataSet
            Dim objConfigs As New CommonUIs
            '    dsDepot = objConfigs.pub_GetDepotDetail(bv_intDepotID)
            Return dsDepot
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CalculateCleaningSlab()"
    Public Function pub_CalculateCleaningSlab(ByRef dtPendingInvoiceCleaning As DataTable, _
                                              ByVal bv_datPeriodFrom As DateTime, _
                                              ByVal bv_datPeriodTo As DateTime, _
                                              ByVal bv_i64CustomerID As Integer, _
                                              ByVal bv_strCustomerType As String, _
                                              ByVal bv_intDepotId As Integer, _
                                              ByVal bv_strWhere As String, _
                                              ByVal bv_strWhereWithoutDepotID As String) As DataTable
        Try
            Dim objCommonUIs As New CommonUIs
            Dim firstDayOfMonth As DateTime = New DateTime(bv_datPeriodFrom.Year, bv_datPeriodFrom.Month, 1)
            Dim tempFromDate As DateTime = firstDayOfMonth
            Dim tempToDate As DateTime = New DateTime(bv_datPeriodFrom.Year, bv_datPeriodFrom.Month, 1).AddMonths(1).AddDays(-1)
            Dim decSlabRate As Decimal
            Dim dtCustomer As DataTable
            For i = 0 To CInt(DateDiff(DateInterval.Month, bv_datPeriodFrom, bv_datPeriodTo))

                If (New DateTime(tempToDate.Year, tempToDate.Month, 1)) = (New DateTime(bv_datPeriodTo.Year, bv_datPeriodTo.Month, 1)) Then
                    tempToDate = bv_datPeriodTo
                End If

                Dim tempDtCleaning As DataTable = dtPendingInvoiceCleaning.Clone()
                Dim tempDtEqptype As DataTable = dtPendingInvoiceCleaning.Clone()
                ''For Slab Rate Flag = 0
                tempDtCleaning = objCommonUIs.GetCleaningChargeByCustomerIdWithoutSlab(bv_i64CustomerID, _
                                                                            tempFromDate, _
                                                                            tempToDate, _
                                                                            bv_intDepotId, _
                                                                            bv_strWhere)
                dtPendingInvoiceCleaning.Merge(tempDtCleaning)
                tempDtCleaning.Clear()
                tempDtEqptype = objCommonUIs.GetDistinctEquipmentTypeByCustomerIDWithSlab(tempDtCleaning.Clone(), bv_i64CustomerID, tempFromDate, _
                                                                           tempToDate, _
                                                                           bv_intDepotId, _
                                                                           bv_strWhere)

                ''For Slab Rate Flag = 0 and with different equipment Types
                For countEquipType As Integer = 0 To tempDtEqptype.Rows.Count - 1
                    Dim lngEquipmentCountByEquipType As Long
                    tempDtCleaning = objCommonUIs.GetCleaningChargeByCustomerIdWithSlabEquipType(tempDtCleaning.Clone(), bv_i64CustomerID, _
                                                                         tempFromDate, _
                                                                         tempToDate, _
                                                                         CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), _
                                                                         bv_intDepotId, _
                                                                         bv_strWhere)
                    lngEquipmentCountByEquipType = objCommonUIs.GetCleaningChargeCountByCustomerIdWithSlabEquipType(tempDtCleaning.Clone(), bv_i64CustomerID, _
                                                                         tempFromDate, _
                                                                         tempToDate, _
                                                                         CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), _
                                                                         bv_strWhereWithoutDepotID)
                    If tempDtCleaning.Rows.Count = 0 Then
                        Continue For
                    End If
                    If bv_strCustomerType.ToLower = "party" Then
                        Dim dtCustomerParty As DataTable = tempDtCleaning.DefaultView.ToTable(True, "CSTMR_ID")
                        For Each partycount As DataRow In dtCustomerParty.Rows
                            Dim lngEquipPartyCount As Long
                            tempDtCleaning.Clear()
                            tempDtCleaning = objCommonUIs.GetCleaningChargeByCustomerIdAndPartyWithSlabEquipType(tempDtCleaning, bv_i64CustomerID, _
                                                                                                                 CLng(partycount.Item(InvoiceGenerationData.CSTMR_ID)), _
                                                                                                                 tempFromDate, _
                                                                                                                 tempToDate, _
                                                                                                                 CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), _
                                                                                                                 bv_intDepotId, _
                                                                                                                 "", bv_strWhere)
                            lngEquipPartyCount = objCommonUIs.GetCleaningChargeCountByCustomerIdAndPartyWithSlabEquipType(tempDtCleaning, bv_i64CustomerID, _
                                                                                                                 CLng(partycount.Item(InvoiceGenerationData.CSTMR_ID)), _
                                                                                                                 tempFromDate, _
                                                                                                                 tempToDate, _
                                                                                                                 CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), _
                                                                                                                 "", bv_strWhereWithoutDepotID)
                            dtCustomer = objCommonUIs.GetCleaningSlabRateByEquipTypeCustomerID(CLng(tempDtCleaning.Rows(0).Item(InvoiceGenerationData.CSTMR_ID)), CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), lngEquipPartyCount)
                            If dtCustomer.Rows.Count > 0 Then
                                decSlabRate = CDec(dtCustomer.Rows(0).Item(CustomerData.CLNNG_RT))
                            Else
                                decSlabRate = 0
                            End If
                            For j = 0 To tempDtCleaning.Rows.Count - 1
                                tempDtCleaning.Rows(j).Item(CommonUIData.DPT_AMNT) = decSlabRate
                                tempDtCleaning.Rows(j).Item(CommonUIData.CSTMR_AMNT) = decSlabRate * CDec(tempDtCleaning.Rows(j).Item(CommonUIData.EXCHNG_RT_PR_UNT_NC))
                            Next
                            dtPendingInvoiceCleaning.Merge(tempDtCleaning)
                        Next
                    Else
                        dtCustomer = objCommonUIs.GetCleaningSlabRateByEquipTypeCustomerID(bv_i64CustomerID, CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), lngEquipmentCountByEquipType)
                        If tempDtCleaning.Rows.Count > 0 Then
                            Dim strCustomerCurrencyID As Integer = CInt(tempDtCleaning.Rows(0).Item(CommonUIData.CSTMR_CRRNCY_ID))
                            Dim strDepotCurrencyID As Integer = CInt(tempDtCleaning.Rows(0).Item(CommonUIData.DPT_CRRNCY_ID))
                        End If
                        If dtCustomer.Rows.Count > 0 Then
                            decSlabRate = CDec(dtCustomer.Rows(0).Item(CustomerData.CLNNG_RT))
                        Else
                            decSlabRate = 0
                        End If

                        For j = 0 To tempDtCleaning.Rows.Count - 1
                            tempDtCleaning.Rows(j).Item(CommonUIData.DPT_AMNT) = decSlabRate
                            tempDtCleaning.Rows(j).Item(CommonUIData.CSTMR_AMNT) = decSlabRate * CDec(tempDtCleaning.Rows(j).Item(CommonUIData.EXCHNG_RT_PR_UNT_NC))
                        Next
                        dtPendingInvoiceCleaning.Merge(tempDtCleaning)
                    End If
                    tempDtCleaning.Clear()
                Next

                tempDtCleaning.Clear()
                tempFromDate = tempToDate.AddDays(1)
                tempToDate = tempFromDate.AddMonths(1).AddDays(-1)
            Next
            Return dtPendingInvoiceCleaning
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "pub_CalculateCleaningSlabRevenue()"
    Public Function pub_CalculateCleaningSlabRevenue(ByRef dtPendingInvoiceCleaning As DataTable, _
                                              ByVal bv_datPeriodFrom As DateTime, _
                                              ByVal bv_datPeriodTo As DateTime, _
                                              ByVal bv_i64CustomerID As Integer, _
                                              ByVal bv_strCustomerType As String, _
                                              ByVal bv_intDepotId As Integer, _
                                              ByVal bv_strWhere As String, _
                                              ByVal bv_strWhereWithoutDepot As String) As DataTable
        Try
            Dim objCommonUIs As New CommonUIs
            Dim firstDayOfMonth As DateTime = New DateTime(bv_datPeriodFrom.Year, bv_datPeriodFrom.Month, 1)
            Dim tempFromDate As DateTime = firstDayOfMonth
            Dim tempToDate As DateTime = New DateTime(bv_datPeriodFrom.Year, bv_datPeriodFrom.Month, 1).AddMonths(1).AddDays(-1)
            Dim decSlabRate As Decimal
            Dim dtCustomer As DataTable
            For i = 0 To CInt(DateDiff(DateInterval.Month, bv_datPeriodFrom, bv_datPeriodTo))

                If (New DateTime(tempToDate.Year, tempToDate.Month, 1)) = (New DateTime(bv_datPeriodTo.Year, bv_datPeriodTo.Month, 1)) Then
                    tempToDate = bv_datPeriodTo
                End If
                Dim tempDtCleaning As DataTable = dtPendingInvoiceCleaning.Clone()
                Dim tempDtEqptype As DataTable = dtPendingInvoiceCleaning.Clone()
                ''For Slab Rate Flag = 0
                tempDtCleaning = objCommonUIs.GetCleaningChargeByCustomerIdWithoutSlabRevenue(bv_i64CustomerID, _
                                                                            tempFromDate, _
                                                                            tempToDate, _
                                                                            bv_intDepotId, _
                                                                            bv_strWhere)
                dtPendingInvoiceCleaning.Merge(tempDtCleaning)
                tempDtCleaning.Clear()
                tempDtCleaning = dtPendingInvoiceCleaning.Clone()
                tempDtEqptype = objCommonUIs.GetDistinctEquipmentTypeByCustomerIDWithSlabRevenue(tempDtCleaning, bv_i64CustomerID, tempFromDate, _
                                                                           tempToDate, _
                                                                           bv_intDepotId, _
                                                                           bv_strWhere)

                ''For Slab Rate Flag = 0 and with different equipment Types
                For countEquipType As Integer = 0 To tempDtEqptype.Rows.Count - 1
                    Dim lngEquipCount As Long
                    tempDtCleaning = objCommonUIs.GetCleaningChargeByCustomerIdWithSlabEquipTypeRevenue(tempDtCleaning, bv_i64CustomerID, _
                                                                         tempFromDate, _
                                                                         tempToDate, _
                                                                         CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), _
                                                                         bv_intDepotId, _
                                                                         bv_strWhere)
                    lngEquipCount = objCommonUIs.GetCleaningChargeCountByCustomerIdWithSlabEquipTypeRevenue(tempDtCleaning, bv_i64CustomerID, _
                                                                         tempFromDate, _
                                                                         tempToDate, _
                                                                         CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), _
                                                                         bv_strWhereWithoutDepot)
                    If tempDtCleaning.Rows.Count = 0 Then
                        Continue For
                    End If
                    If bv_strCustomerType.ToLower = "party" Then
                        Dim dtCustomerParty As DataTable = tempDtCleaning.DefaultView.ToTable(True, "CSTMR_ID")
                        For Each partycount As DataRow In dtCustomerParty.Rows
                            Dim lngEquipCountByParty As Long
                            tempDtCleaning.Clear()
                            tempDtCleaning = objCommonUIs.GetCleaningChargeByCustomerIdAndPartyWithSlabEquipTypeRevenue(tempDtCleaning, bv_i64CustomerID, _
                                                                                                                 CLng(partycount.Item(InvoiceGenerationData.CSTMR_ID)), _
                                                                                                                 tempFromDate, _
                                                                                                                 tempToDate, _
                                                                                                                 CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), _
                                                                                                                 bv_intDepotId, _
                                                                                                                 bv_strWhere)
                            lngEquipCountByParty = objCommonUIs.GetCleaningChargeCountByCustomerIdAndPartyWithSlabEquipTypeRevenue(tempDtCleaning, bv_i64CustomerID, _
                                                                                                                 CLng(partycount.Item(InvoiceGenerationData.CSTMR_ID)), _
                                                                                                                 tempFromDate, _
                                                                                                                 tempToDate, _
                                                                                                                 CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), _
                                                                                                                 bv_strWhereWithoutDepot)
                            dtCustomer = objCommonUIs.GetCleaningSlabRateByEquipTypeCustomerID(CLng(tempDtCleaning.Rows(0).Item(InvoiceGenerationData.CSTMR_ID)), CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), lngEquipCountByParty)
                            If dtCustomer.Rows.Count > 0 Then
                                decSlabRate = CDec(dtCustomer.Rows(0).Item(CustomerData.CLNNG_RT))
                            Else
                                decSlabRate = 0
                            End If
                            For j = 0 To tempDtCleaning.Rows.Count - 1
                                tempDtCleaning.Rows(j).Item(CommonUIData.DPT_AMNT) = decSlabRate
                                tempDtCleaning.Rows(j).Item(CommonUIData.CSTMR_AMNT) = decSlabRate * CDec(tempDtCleaning.Rows(j).Item(CommonUIData.EXCHNG_RT_PR_UNT_NC))
                                tempDtCleaning.Rows(j).Item("UNBILLED_DPT_AMNT") = decSlabRate * CDec(tempDtCleaning.Rows(j).Item(CommonUIData.EXCHNG_RT_PR_UNT_NC))
                            Next
                            dtPendingInvoiceCleaning.Merge(tempDtCleaning)
                        Next
                    Else
                        dtCustomer = objCommonUIs.GetCleaningSlabRateByEquipTypeCustomerID(bv_i64CustomerID, CInt(tempDtEqptype.Rows(countEquipType).Item(InvoiceGenerationData.EQPMNT_TYP_ID)), lngEquipCount)
                        If dtCustomer.Rows.Count > 0 Then
                            decSlabRate = CDec(dtCustomer.Rows(0).Item(CustomerData.CLNNG_RT))
                        Else
                            decSlabRate = 0
                        End If
                        For j = 0 To tempDtCleaning.Rows.Count - 1
                            tempDtCleaning.Rows(j).Item(InvoiceGenerationData.CLNNG_RT) = decSlabRate
                            tempDtCleaning.Rows(j).Item(InvoiceGenerationData.DPT_AMNT) = decSlabRate
                            tempDtCleaning.Rows(j).Item(InvoiceGenerationData.CSTMR_AMNT) = decSlabRate * CDec(tempDtCleaning.Rows(j).Item(CommonUIData.EXCHNG_RT_PR_UNT_NC))
                            tempDtCleaning.Rows(j).Item("UNBILLED_DPT_AMNT") = decSlabRate * CDec(tempDtCleaning.Rows(j).Item(CommonUIData.EXCHNG_RT_PR_UNT_NC))
                        Next
                        dtPendingInvoiceCleaning.Merge(tempDtCleaning)
                    End If
                    tempDtCleaning.Clear()
                Next
                tempDtCleaning.Clear()
                tempFromDate = tempToDate.AddDays(1)
                tempToDate = tempFromDate.AddMonths(1).AddDays(-1)
            Next
            Return dtPendingInvoiceCleaning
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

End Class