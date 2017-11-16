Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class Schedule

#Region "Pub_GetActivityByActivityId()"
    ''' <summary>
    ''' This function is used to get activity by activityId
    ''' </summary>
    ''' <param name="bv_intActivityId"></param>
    ''' <param name="bv_strTableName"></param>
    ''' <param name="bv_intScheduleId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function Pub_GetActivityByActivityId(ByVal bv_intActivityId As Integer, _
                                                ByVal bv_strTableName As String, _
                                                ByVal bv_intScheduleId As Integer, _
                                                ByVal bv_intDepotID As Int32) As ScheduleDataSet
        Try
            Dim dsSchedule As New ScheduleDataSet
            Dim objSchedules As New Schedules
            dsSchedule = objSchedules.GetActivityByActivityId(bv_intActivityId, bv_strTableName, bv_intScheduleId, bv_intDepotID)
            Return dsSchedule
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateActivityById()"
    ''' <summary>
    ''' This function is used to update the schedule date to respective table (Repair_Estimate, Activity_Status)
    ''' </summary>
    ''' <param name="bv_strTableName"></param>
    ''' <param name="bv_strDataKey"></param>
    ''' <param name="bv_intActivityId"></param>
    ''' <param name="bv_strUpdateTableName"></param>
    ''' <param name="br_dsScheduleDataset"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_UpdateActivityById(ByVal bv_strTableName As String, _
                                           ByVal bv_strDataKey As String, _
                                           ByVal bv_intActivityId As Integer, _
                                           ByVal bv_strUpdateTableName As String, _
                                           ByRef br_dsScheduleDataset As ScheduleDataSet) As Boolean
        Dim objTransaction As New Transactions
        Dim objSchedules As New Schedules
        Try
            For Each drSchedule As DataRow In br_dsScheduleDataset.Tables(bv_strTableName).Select(String.Concat(ScheduleData.CHECKED, "='True' AND ", ScheduleData.SCHDL_DT, " IS NOT NULL"))
                objSchedules.UpdateActivityById(CLng(drSchedule.Item(bv_strDataKey)), _
                                                bv_intActivityId, _
                                                CDate(drSchedule.Item(ScheduleData.SCHDL_DT)), _
                                                bv_strUpdateTableName, _
                                                objTransaction)
            Next
            objTransaction.commit()
            Return True
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function
#End Region

#Region "pub_GetRepairScheduleReport() TABLE NAME: REPAIR_ESTIMATE"
    ''' <summary>
    ''' This function is used to get the scheduled date repair details from Repair_Estimate table.
    ''' </summary>
    ''' <param name="bv_param"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetRepairScheduleReport(ByRef bv_param As String) As DataSet
        Try            Dim objCommonUIs As New CommonUIs            Dim objSchedules As New Schedules
            Dim dsScheduleDataset As New ScheduleDataSet
            Dim dtScheduleDate As DateTime
            If objCommonUIs.pub_GetParameter("SCHEDULE_DATE", bv_param) <> Nothing Then
                dtScheduleDate = CDate(objCommonUIs.pub_GetParameter("SCHEDULE_DATE", bv_param))
                dsScheduleDataset = objSchedules.GetRepairScheduleReport(dtScheduleDate, CInt(objCommonUIs.pub_GetParameter("DEPOT", bv_param)))
            End If
            Return dsScheduleDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetSurveyScheduleReport() TABLE NAME: ACTIVITY_STATUS"
    ''' <summary>
    ''' This function is used to get scheduled survey report from Activity_Status
    ''' </summary>
    ''' <param name="bv_param"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetSurveyScheduleReport(ByRef bv_param As String) As DataSet
        Try            Dim objCommonUIs As New CommonUIs            Dim objSchedules As New Schedules
            Dim dsScheduleDataset As New ScheduleDataSet
            Dim dtScheduleDate As DateTime
            If objCommonUIs.pub_GetParameter("SCHEDULE_DATE", bv_param) <> Nothing Then
                dtScheduleDate = CDate(objCommonUIs.pub_GetParameter("SCHEDULE_DATE", bv_param))
                dsScheduleDataset = objSchedules.GetSurveyScheduleReport(dtScheduleDate, CInt(objCommonUIs.pub_GetParameter("DEPOT", bv_param)))
            End If
            Return dsScheduleDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCleaningScheduleReport() TABLE NAME: ACTIVITY_STATUS"
    ''' <summary>
    ''' This function is used to get scheduled cleaning details from Activity_Status table.
    ''' </summary>
    ''' <param name="bv_param"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetCleaningScheduleReport(ByRef bv_param As String) As DataSet
        Try            Dim objCommonUIs As New CommonUIs            Dim objSchedules As New Schedules
            Dim dsScheduleDataset As New ScheduleDataSet
            Dim dtScheduleDate As DateTime
            Dim intDepotId As Int32
            If objCommonUIs.pub_GetParameter("SCHEDULE_DATE", bv_param) <> Nothing Then
                dtScheduleDate = CDate(objCommonUIs.pub_GetParameter("SCHEDULE_DATE", bv_param))
                intDepotId = CInt(objCommonUIs.pub_GetParameter("DEPOT", bv_param))
                dsScheduleDataset = objSchedules.GetCleaningScheduleReport(dtScheduleDate, intDepotId)
            End If
            Return dsScheduleDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "Schedule Role Rights"

    Public Function GetScheduleActivity(ByVal bv_strUserName As String) As ScheduleDataSet
        Try
            Dim objSchedules As New Schedules

            objSchedules.GetScheduleRights(bv_strUserName)
            Return objSchedules.GetScheduleActivity()

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    'Print - Report
    <OperationContract()> _
    Public Function pub_GetInspectionScheduleReport(ByRef bv_param As String) As DataSet
        Try            Dim objCommonUIs As New CommonUIs            Dim objSchedules As New Schedules
            Dim dsScheduleDataset As New ScheduleDataSet
            Dim dtScheduleDate As DateTime
            If objCommonUIs.pub_GetParameter("SCHEDULE_DATE", bv_param) <> Nothing Then
                dtScheduleDate = CDate(objCommonUIs.pub_GetParameter("SCHEDULE_DATE", bv_param))
                dsScheduleDataset = objSchedules.GetInspectionScheduleReport(dtScheduleDate, CInt(objCommonUIs.pub_GetParameter("DEPOT", bv_param)))
            End If
            Return dsScheduleDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
