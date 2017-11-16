Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class AddDynamicReport

#Region "GetActivityByProcessId()"

    <OperationContract()> _
    Public Function GetActivityByProcessId(ByVal bv_i32ProcessId As Int32, ByVal bv_strType As String) As AddDynamicReportDataSet

        Try
            Dim dsAddDynamicReportData As AddDynamicReportDataSet
            Dim objAddReports As New AddDynamicReports
            dsAddDynamicReportData = objAddReports.GetActivityByProcessId(bv_i32ProcessId, bv_strType)
            Return dsAddDynamicReportData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "INSERT: pub_CreateReport"
    <OperationContract()> _
    Public Function pub_CreateReport(ByVal bv_strProcessName As String, ByVal bv_i32ParentProcess As Integer, _
                                        ByRef br_intActivityID As Integer, ByVal bv_strActivityName As String, _
                                         ByVal bv_strLST_URL As String, ByVal bv_strLST_TTL As String, _
                                        ByVal bv_i32LST_CLCNT As Int32, ByVal bv_strPageURL As String, ByVal bv_strPageTitle As String, _
                                        ByVal bv_strTableName As String,
                                        ByVal bv_blnActiveBit As Boolean, _
                                        ByVal bv_strAddProcessFlag As String,
                                        ByRef br_i32ProcessId As Int32) As Boolean
        Dim objTransaction As New Transactions()
        Dim objAddDynamicReports As New AddDynamicReports
        Try
            If CBool(bv_strAddProcessFlag) Then
                br_i32ProcessId = CInt(objAddDynamicReports.CreateProcess(bv_strProcessName, bv_i32ParentProcess, "", objTransaction))
            Else
                br_i32ProcessId = CInt(bv_strProcessName)
            End If

            br_intActivityID = CInt(objAddDynamicReports.CreateActivity(br_intActivityID, bv_strActivityName, br_i32ProcessId, _
                                                                    bv_strLST_URL, bv_strLST_TTL, bv_i32LST_CLCNT, 0, _
                                                                    bv_strPageURL, bv_strPageTitle, bv_strTableName, _
                                                                     True, True, bv_blnActiveBit, "", False, "", "", objTransaction))

            objTransaction.commit()
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try

    End Function
#End Region

#Region "UPDATE: pub_UpdateReport"
    <OperationContract()> _
    Public Function pub_UpdateReport(ByVal bv_strProcessName As String, ByVal bv_i32ParentProcess As Integer, _
                                        ByRef br_intActivityID As Integer, ByVal bv_strActivityName As String, _
                                        ByVal bv_strLST_URL As String, ByVal bv_strPageURL As String, ByVal bv_strPageTitle As String, _
                                        ByVal bv_strTableName As String, _
                                        ByVal bv_blnActiveBit As Boolean, _
                                        ByVal bv_strAddProcessFlag As String, _
                                         ByVal bv_strOrderFlag As Boolean, _
                                         ByVal bv_strFetchparameters As Boolean, _
                                         ByVal bv_dtParameters As DataTable) As Boolean
        Dim objTransaction As New Transactions()
        Dim objAddDynamicReports As New AddDynamicReports
        Try
            Dim intProcessId As Integer
            If CBool(bv_strAddProcessFlag) Then
                intProcessId = CInt(objAddDynamicReports.CreateProcess(bv_strProcessName, bv_i32ParentProcess, "", objTransaction))
            Else
                intProcessId = CInt(bv_strProcessName)
            End If

            objAddDynamicReports.UpdateActivity(br_intActivityID, bv_strActivityName, bv_strLST_URL, bv_strPageURL, bv_strPageTitle, bv_strTableName, intProcessId, bv_blnActiveBit, bv_strOrderFlag, objTransaction)

            If bv_strFetchparameters = True Then
                For Each drParameters As DataRow In bv_dtParameters.Rows
                    If drParameters.RowState = DataRowState.Modified Then
                        objAddDynamicReports.UpdateReportParameter(CInt(drParameters.Item(AddDynamicReportData.PRMTR_ID)), _
                                                 CInt(drParameters.Item(AddDynamicReportData.RPRT_ID)), _
                                               CStr(drParameters.Item(AddDynamicReportData.PRMTR_DSPLY_NAM)), _
                                               CBool(drParameters.Item(AddDynamicReportData.PRMTR_OPT)), _
                                                CBool(drParameters.Item(AddDynamicReportData.PRMTR_DRPDWN)), _
                                               drParameters.Item(AddDynamicReportData.PRMTR_DPNDNT).ToString(), objTransaction)
                    End If
                Next
            End If

            objTransaction.commit()
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try

    End Function
#End Region

#Region "pub_ValidatePKAddDynamicReport()"
    <OperationContract()> _
    Public Function pub_ValidatePKAddDynamicReport(ByVal bv_strAddReportCode As String, ByVal intProcessId As Integer) As Boolean

        Try
            Dim objAddReports As New AddDynamicReports
            Dim intRowCount As Integer
            intRowCount = CInt(objAddReports.GetActivityByActivityNameProcessID(bv_strAddReportCode, intProcessId))
            If intRowCount > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetProcessIDByProcessName() TABLE NAME:ACTIVITY"
    <OperationContract()> _
    Public Function pub_GetProcessIDByProcessName(ByVal bv_strProcessName As String) As Integer
        Try
            Dim objAddReports As New AddDynamicReports
            Dim intProcessId As Integer
            intProcessId = CInt(objAddReports.GetProcessIDByProcessName(bv_strProcessName))
            Return CInt(intProcessId)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "Pub_GetProcessIDByProcessNameActivityName() TABLE NAME:ACTIVITY"
    <OperationContract()> _
    Public Function Pub_GetProcessIDByProcessIDActivityName(ByVal bv_strProcessID As Int32, ByVal bv_strActivityName As String) As Boolean
        Try
            Dim objAddReports As New AddDynamicReports
            Dim blnResult As Boolean = False
            Dim intProcessId As Integer
            intProcessId = CInt(objAddReports.GetProcessIDByProcessIDActivityName(bv_strProcessID, bv_strActivityName))
            If intProcessId > 0 Then
                blnResult = True
            Else
                blnResult = False
            End If
            Return blnResult
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetReportParameters() TABLE NAME:REPORT_PARAMETER"

    <OperationContract()> _
    Public Function pub_GetReportParameters(ByVal bv_dtParameters As DataTable, ByVal bv_strReportName As String) As AddDynamicReportDataSet
        Dim objTransaction As New Transactions()
        Try
            Dim dsAddDynamicReportData As AddDynamicReportDataSet
            Dim objAddReports As New AddDynamicReports
            Dim intReportID As Int32

            If Not bv_dtParameters Is Nothing Then
                For Each drParameters As DataRow In bv_dtParameters.Rows

                    Dim lngCreated As Long
                    intReportID = CInt(drParameters.Item(AddDynamicReportData.RPRT_ID))

                    Dim i32Parameter As Int32 = objAddReports.GetReportParameterByParameterID(CStr(drParameters.Item(AddDynamicReportData.PRMTR_NAM)), intReportID, objTransaction)

                    If i32Parameter = 0 Then
                        lngCreated = objAddReports.CreateReportParameter(CInt(drParameters.Item(AddDynamicReportData.RPRT_ID)), _
                                         CStr(drParameters.Item(AddDynamicReportData.PRMTR_NAM)), _
                                       CStr(drParameters.Item(AddDynamicReportData.PRMTR_DSPLY_NAM)), _
                                        CStr(drParameters.Item(AddDynamicReportData.PRMTR_TYP)), _
                                       Nothing, _
                                       CBool(drParameters.Item(AddDynamicReportData.PRMTR_OPT)), _
                                       Nothing, _
                                       Nothing, _
                                       Nothing, _
                                       Nothing, _
                                        CBool(drParameters.Item(AddDynamicReportData.PRMTR_DRPDWN)), _
                                       Nothing, objTransaction)
                    End If

                Next
            End If

            dsAddDynamicReportData = objAddReports.GetReportParameterByReportID(intReportID, objTransaction)

            If dsAddDynamicReportData.Tables(AddDynamicReportData._REPORT_PARAMETER).Rows.Count > 0 Then
                For Each drAddReport As DataRow In dsAddDynamicReportData.Tables(AddDynamicReportData._REPORT_PARAMETER).Rows
                    With drAddReport
                        If bv_dtParameters.Select(String.Concat(AddDynamicReportData.PRMTR_NAM, "='", drAddReport.Item(AddDynamicReportData.PRMTR_NAM).ToString(), "'")).Length = 0 Then
                            objAddReports.DeleteReportParameter(CInt(drAddReport.Item(AddDynamicReportData.PRMTR_ID).ToString()), objTransaction)
                        End If
                    End With
                Next
            End If

            dsAddDynamicReportData.Tables(AddDynamicReportData._REPORT_PARAMETER).Rows.Clear()
            dsAddDynamicReportData = objAddReports.GetReportParameterByReportID(intReportID, objTransaction)
            objTransaction.commit()
            Return dsAddDynamicReportData
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function

#End Region

#Region "Pub_GetReportParameterByReportIDValue() TABLE NAME:REPORT_PARAMETER"
    <OperationContract()> _
    Public Function Pub_GetReportParameterByReportIDValue(ByVal bv_intReportID As Int32) As AddDynamicReportDataSet
        Try
            Dim objAddReports As New AddDynamicReports
            Dim dsAddDynamicReportData As AddDynamicReportDataSet
            dsAddDynamicReportData = objAddReports.GetReportParameterByReportIDValue(bv_intReportID)
            Return dsAddDynamicReportData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region ""

#End Region

End Class

