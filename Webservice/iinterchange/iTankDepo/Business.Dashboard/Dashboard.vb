Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
<ServiceContract()> _
Public Class Dashboard
#Region "pub_GetVPendingActionsByDepotId() TABLE NAME:V_PENDING_ACTIONS"

    <OperationContract()> _
    Public Function pub_GetVPendingActionsByDepotId(ByVal bv_i32DepotId As Int32) As DashboardDataSet

        Try
            Dim dsDashboardDataSet As DashboardDataSet
            Dim objDashboards As New Dashboards
            dsDashboardDataSet = objDashboards.Get_V_PendingActionsBy(bv_i32DepotId)
            Return dsDashboardDataSet
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



#Region "pub_GetDashboardWidgets()"

    <OperationContract()> _
    Public Function pub_GetDashboardWidgets(ByVal bv_intRoleID As Int32) As DashboardDataSet
        Try
            Dim dsDashboardDataSet As DashboardDataSet
            Dim dtDashboard As DataTable
            Dim dvDashboardData As DataView
            Dim objDashboards As New Dashboards
            Dim intProcessId As Integer = 17
            dsDashboardDataSet = objDashboards.GetActivityByProcessID(intProcessId)
            If dsDashboardDataSet.Tables(DashboardData._WIDGET).Rows.Count > 0 Then
                dtDashboard = objDashboards.GetRoleRightByRoleIDAndActivityIDs(bv_intRoleID, CommonUIs.ColToCSVstring(dsDashboardDataSet.Tables(DashboardData._WIDGET), DashboardData.WDGT_ID)).Tables(DashboardData._ROLE_RIGHT)
                If dtDashboard.Rows.Count > 0 Then
                    dvDashboardData = dsDashboardDataSet.Tables(DashboardData._WIDGET).DefaultView
                    dvDashboardData.RowFilter = String.Concat(DashboardData.WDGT_ID, " IN (", CommonUIs.ColToCSVstring(dtDashboard, DashboardData.ACTVTY_ID), ")")
                    'If dvDashboardData.Count > 0 Then
                    '    dsDashboardDataSet.Tables(DashboardData._WIDGET).Clear()
                    '    dsDashboardDataSet.Tables(DashboardData._WIDGET).Merge(dvDashboardData.ToTable)
                    'End If
                End If
            End If
            Return dsDashboardDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
End Class
