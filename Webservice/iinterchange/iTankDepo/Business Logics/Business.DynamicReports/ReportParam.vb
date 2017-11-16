Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess

<ServiceContract()> _
Public Class ReportParam
    <OperationContract()> _
    Public Function pub_GetParamTable(ByVal strQuery As String, ByVal bv_selected As Boolean, ByVal bv_StrTableName As String, ByVal bv_i32DepotID As Integer, Optional ByVal blnDropdown As Boolean = False) As ReportParamDataSet

        Try

            Dim dsReportParam As New ReportParamDataSet
            Dim objReportParam As New ReportParams

            If blnDropdown = False Then
                strQuery = strQuery.Replace("WHERE", String.Concat("WHERE ", ReportParamData.DPT_ID, "=", bv_i32DepotID, " AND "))
            End If

            dsReportParam = objReportParam.GetParamTable(strQuery, bv_StrTableName)


            If Not dsReportParam.Tables(bv_StrTableName) Is Nothing AndAlso dsReportParam.Tables(bv_StrTableName).Rows.Count > 0 Then

                Dim dc As New DataColumn("Select", GetType(Boolean))
                If bv_selected Then
                    dc.DefaultValue = True
                Else
                    dc.DefaultValue = False
                End If
                dsReportParam.Tables(bv_StrTableName).Columns.Add(dc)
                dc.SetOrdinal(1)
            End If
            Return dsReportParam

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function


#Region "pub_ValidateReport_HeaderbyReportTitle() TABLE NAME:Report_Header"
    <OperationContract()> _
    Public Function pub_ValidateReport_HeaderbyReportTitle(ByVal bv_strReportTitle As String, ByVal bv_int32ActivityId As Int32, ByVal bv_int32DepotId As String) As Integer
        Try
            Dim objReportParams As New ReportParams
            Dim intReportCount As Integer
            intReportCount = objReportParams.ValidateReport_HeaderbyReportTitle(bv_strReportTitle, bv_int32ActivityId, bv_int32DepotId)
            Return intReportCount
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetReportNamebyReportID() TABLE NAME:Activity"
    <OperationContract()> _
    Public Function pub_GetReportNamebyReportID(ByVal bv_int32ActivityId As Int32) As String
        Try
            Dim objReportParams As New ReportParams
            Dim strReportName As String
            strReportName = objReportParams.GetReportNamebyReportID(bv_int32ActivityId)
            Return strReportName
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


#Region "pub_GetReport_Header() TABLE NAME:Report_Header"
    <OperationContract()> _
    Public Function pub_GetReport_Header(ByVal bv_i32ReportHeaderID As Int32) As ReportParamDataSet
        Try
            Dim dsReportHeader As ReportParamDataSet
            Dim objReportParams As New ReportParams
            dsReportHeader = objReportParams.GetReport_HeaderByReportHeaderID(bv_i32ReportHeaderID)
            Return dsReportHeader
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateReport_Header() TABLE NAME:Report_Header"
    <OperationContract()> _
    Public Function pub_CreateReport_Header(ByVal bv_strReportTitle As String, _
                                            ByVal bv_ParameterValue As Byte(), _
                                            ByVal bv_i32ActivityID As Int32, _
                                            ByVal bv_strModifiedBy As String, _
                                            ByVal bv_datModifiedDate As DateTime, _
                                            ByVal bv_i32DepotID As Int32) As Long
        Try
            Dim objReportParams As New ReportParams
            pub_CreateReport_Header = objReportParams.CreateReport_Header(bv_strReportTitle, _
                                                                     bv_ParameterValue, _
                                                                     bv_i32ActivityID, _
                                                                     bv_strModifiedBy, _
                                                                     bv_datModifiedDate, _
                                                                     bv_i32DepotID)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_ModifyReport_Header() TABLE NAME:Report_Header"
    <OperationContract()> _
    Public Function pub_ModifyReport_Header(ByVal bv_i32ReportHeaderID As Int32, _
                                            ByVal bv_strReportTitle As String, _
                                            ByVal bv_ParameterValue As Byte(), _
                                            ByVal bv_i32ActivityID As Int32, _
                                            ByVal bv_strModifiedBy As String, _
                                            ByVal bv_datModifiedDate As DateTime, _
                                            ByVal bv_i32DepotID As Int32) As Boolean
        Try
            Dim objReportParams As New ReportParams
            pub_ModifyReport_Header = objReportParams.UpdateReport_Header(bv_i32ReportHeaderID, _
                                                                     bv_strReportTitle, _
                                                                     bv_ParameterValue, _
                                                                     bv_i32ActivityID, _
                                                                     bv_strModifiedBy, _
                                                                     bv_datModifiedDate, _
                                                                     bv_i32DepotID)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
