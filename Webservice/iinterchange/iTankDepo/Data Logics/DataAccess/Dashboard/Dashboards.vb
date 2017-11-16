Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
#Region "Dashboards"

Public Class Dashboards

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const V_PENDING_ACTIONSSelectQueryBy As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM,DPT_ID,GTN_PNDNG,ESTMT_PNDNG,LSS_APPRVL_PNDNG,OWNR_APRPVL_PNDNG,RPR_CMPLTN_PNDNG,SRVY_CMPLTN_PNDNG,GT_OT_PNDNG FROM V_PENDING_ACTIONS WHERE DPT_ID=@DPT_ID"
    Private Const V_EQUIPMENT_TRACKINGSelectQueryBy As String = "SELECT TRCKNG_ID,RFRNC_NO,ACTVTY_NAM,EQPMNT_NO,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EIR_DT,DPT_ID,DPT_CD,LSS_ID,LSS_CD,CSTMR_ID,CSTMR_CD,AUTH_NO,RPR_ESTMT_TRNSXN FROM V_EQUIPMENT_TRACKING WHERE DPT_ID=@DPT_ID"
    Private Const ActivitySelectQueryByActivityID As String = "SELECT ACTVTY_ID AS WDGT_ID,LST_TTL AS WDGT_NAM,PG_URL AS WDGT_URL,PG_TTL AS WDGT_TTL FROM ACTIVITY WHERE PRCSS_ID=@PRCSS_ID"
    Dim RoleRightSelectQueryByRoleIDAndActivityID As String = "SELECT RL_RGHT_ID,RL_ID,ACTVTY_ID,EDT_BT,CRT_BT,VW_BT FROM ROLE_RIGHT WHERE RL_ID=@RL_ID"
    Private ds As DashboardDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New DashboardDataSet
    End Sub

#End Region

#Region "GetVPendingActionsBy() TABLE NAME:V_PENDING_ACTIONS"

    Public Function Get_V_PendingActionsBy(ByVal bv_i64DepotId As Int64) As DashboardDataSet
        Try
            objData = New DataObjects(V_PENDING_ACTIONSSelectQueryBy, DashboardData.DPT_ID, CStr(bv_i64DepotId))
            objData.Fill(CType(ds, DataSet), DashboardData._V_PENDING_ACTIONS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetVEquipmentTrackingBy() TABLE NAME:V_EQUIPMENT_TRACKING"

    Public Function GetVEquipmentTrackingBy(ByVal bv_i32DepotID As Int32) As DashboardDataSet
        Try
            objData = New DataObjects(V_EQUIPMENT_TRACKINGSelectQueryBy, DashboardData.DPT_ID, CStr(bv_i32DepotID))
            objData.Fill(CType(ds, DataSet), DashboardData._V_EQUIPMENT_TRACKING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetActivityByProcessID() TABLE NAME:ACTIVITY"
    Public Function GetActivityByProcessID(ByVal bv_intProcessId As Int32) As DashboardDataSet
        Try
            objData = New DataObjects(ActivitySelectQueryByActivityID, DashboardData.PRCSS_ID, CStr(bv_intProcessId))
            objData.Fill(CType(ds, DataSet), DashboardData._WIDGET)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRoleRightByRoleIDAndActivityID()"
    Public Function GetRoleRightByRoleIDAndActivityIDs(ByVal bv_intRoleID As Integer, ByVal bv_strActivityIDs As String) As DashboardDataSet
        Try
            RoleRightSelectQueryByRoleIDAndActivityID = String.Concat(RoleRightSelectQueryByRoleIDAndActivityID, " AND ACTVTY_ID IN (", bv_strActivityIDs, ")")
            objData = New DataObjects(RoleRightSelectQueryByRoleIDAndActivityID, DashboardData.RL_ID, bv_intRoleID)
            objData.Fill(CType(ds, DataSet), DashboardData._ROLE_RIGHT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class

#End Region
