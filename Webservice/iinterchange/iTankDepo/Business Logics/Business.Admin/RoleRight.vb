Option Strict On
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.ServiceModel
<ServiceContract()> _
Public Class RoleRight

#Region "pub_GetActivity()"
    ''' <summary>
    ''' This method is to retrieve datas for new mode
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetActivity(ByVal bv_strRemoveProcessIDs As String, ByVal bv_strexceptionalActivityIDs As String) As RoleRightDataSet
        Dim dsRoleRight As New RoleRightDataSet
        Dim objRoleRight As New RoleRights
        Try
            Dim dvProcess As DataView
            Dim dvActivity As DataView
            dvProcess = objRoleRight.GetProcess().Tables(RoleRightData._PROCESS).DefaultView
            dvActivity = objRoleRight.GetActivity().Tables(RoleRightData._ACTIVITY).DefaultView
            If Not bv_strRemoveProcessIDs = "" Then
                dvProcess.RowFilter = String.Concat(RoleRightData.PRCSS_ID, " NOT IN (", bv_strRemoveProcessIDs, ")")
            End If
            If Not bv_strexceptionalActivityIDs = "" Then
                dvActivity.RowFilter = String.Concat(RoleRightData.EXCPTN_BT, "='False' AND ", RoleRightData.ACTVTY_ID, " NOT IN (", bv_strexceptionalActivityIDs, ")")
            Else
                dvActivity.RowFilter = String.Concat(RoleRightData.EXCPTN_BT, "='False'")
            End If
            dsRoleRight.Tables(RoleRightData._PROCESS).Merge(dvProcess.ToTable)
            dsRoleRight.Tables(RoleRightData._ACTIVITY).Merge(dvActivity.ToTable)
            Return dsRoleRight
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRoleCodeByRoleId() TABLE NAME: Role"
    ''' <summary>
    ''' This method is to retrive role by role id
    ''' </summary>
    ''' <param name="bv_strRole"></param>
    ''' <param name="bv_intRoleID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetRoleCodeByRoleId(ByVal bv_strRole As String, ByVal bv_intRoleID As Int32) As Boolean
        Dim dsRoleRight As RoleRightDataSet
        Dim objRoleRight As New RoleRights
        Try
            dsRoleRight = objRoleRight.GetRoleByRoleID(bv_strRole, bv_intRoleID)
            If dsRoleRight.Tables(RoleRightData._ROLE).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRoleRightByRoleID()"
    ''' <summary>
    ''' This method is to retrieve datas in edit mode
    ''' </summary>
    ''' <param name="bv_intRoleID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_GetRoleRightByRoleID(ByVal bv_intRoleID As Int32) As RoleRightDataSet
        Dim dsRoleRight As RoleRightDataSet
        Dim objRoleRight As New RoleRights
        Try
            objRoleRight.GetRoleRightProcessByRoleID(bv_intRoleID)
            objRoleRight.GetRoleRightActivityByRoleID(bv_intRoleID)
            objRoleRight.GetParentProcess()
            objRoleRight.GetParentProcessByRoleID(bv_intRoleID)
            dsRoleRight = objRoleRight.GetRoleRightByRoleID(bv_intRoleID)
            Return dsRoleRight
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateRole()"
    ''' <summary>
    ''' This method is to insert data in new mode
    ''' </summary>
    ''' <param name="bv_strRoleCode"></param>
    ''' <param name="bv_strRoleDescription"></param>
    ''' <param name="bv_blnActiveBit"></param>
    ''' <param name="br_dsRoleRight"></param>
    ''' <param name="bv_i32DPT_ID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_CreateRole(ByVal bv_strRoleCode As String, _
                                     ByVal bv_strRoleDescription As String, _
                                     ByRef br_dsRoleRight As RoleRightDataSet, _
                                     ByVal bv_strModifiedBy As String, _
                                     ByVal bv_datModifiedDate As DateTime, _
                                     ByVal bv_blnActiveBit As Boolean, _
                                     ByVal bv_i32DPT_ID As Integer, _
                                     ByVal bv_blnDashboardActiviveBit As Boolean) As Long
        Try
            Dim objRoleRight As New RoleRights
            Dim lngCreated As Long
            Dim intActivityID As Integer
            Dim blnAdd, blnEdit, blnView, blnCancel As Boolean

            lngCreated = objRoleRight.CreateRole(bv_strRoleCode, bv_strRoleDescription, bv_strModifiedBy, bv_datModifiedDate, bv_blnActiveBit, bv_i32DPT_ID, bv_blnDashboardActiviveBit)

            For Each dr As DataRow In br_dsRoleRight.Tables(RoleRightData._ROLE_RIGHT).Rows
                intActivityID = CInt(dr.Item(RoleRightData.ACTVTY_ID))
                blnAdd = CBool(dr.Item(RoleRightData.CRT_BT))
                blnEdit = CBool(dr.Item(RoleRightData.EDT_BT))
                blnView = CBool(dr.Item(RoleRightData.VW_BT))
                blnCancel = CBool(dr.Item(RoleRightData.CNCL_BT))
                objRoleRight.CreateRoleRight(CInt(lngCreated), intActivityID, blnView, blnEdit, blnAdd, blnCancel)
            Next
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_ModifyRole()"
    ''' <summary>
    ''' This method is to insert data in edit mode
    ''' </summary>
    ''' <param name="bv_intRoleID"></param>
    ''' <param name="bv_strRoleCode"></param>
    ''' <param name="bv_strRoleDescription"></param>
    ''' <param name="bv_blnActiveBit"></param>
    ''' <param name="br_dsRoleRight"></param>
    ''' <param name="bv_i32DPT_ID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_ModifyRole(ByVal bv_intRoleID As Int32, _
                                 ByVal bv_strRoleCode As String, _
                                 ByVal bv_strRoleDescription As String, _
                                 ByRef br_dsRoleRight As RoleRightDataSet, _
                                 ByVal bv_strModifiedBy As String, _
                                 ByVal bv_datModifiedDate As DateTime, _
                                 ByVal bv_blnActiveBit As Boolean, _
                                 ByVal bv_i32DPT_ID As Integer, _
                                 ByVal bv_blnDashboardActiviveBit As Boolean) As Boolean
        Try
            Dim objRoleRight As New RoleRights
            Dim blnUpdated As Boolean
            Dim intActivityID As Integer
            Dim blnAdd, blnEdit, blnView, blnCancel As Boolean

            blnUpdated = objRoleRight.UpdateRole(bv_intRoleID, bv_strRoleCode, bv_strRoleDescription, bv_strModifiedBy, bv_datModifiedDate, bv_blnActiveBit, bv_i32DPT_ID, bv_blnDashboardActiviveBit)

            objRoleRight.DeleteRoleRight(bv_intRoleID)
            For Each dr As DataRow In br_dsRoleRight.Tables(RoleRightData._ROLE_RIGHT).Select(String.Concat(RoleRightData.RL_ID, " is NULL"), "")
                intActivityID = CInt(dr.Item(RoleRightData.ACTVTY_ID))
                blnAdd = CBool(dr.Item(RoleRightData.CRT_BT))
                blnEdit = CBool(dr.Item(RoleRightData.EDT_BT))
                blnView = CBool(dr.Item(RoleRightData.VW_BT))
                blnView = CBool(dr.Item(RoleRightData.VW_BT))
                blnCancel = CBool(dr.Item(RoleRightData.CNCL_BT))
                objRoleRight.CreateRoleRight(bv_intRoleID, intActivityID, blnView, blnEdit, blnAdd, blnCancel)

            Next
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_DeleteRoleRight()"
    ''' <summary>
    ''' This method is to delete role rights by role 
    ''' </summary>
    ''' <param name="bv_intRoleRightID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_DeleteRoleRight(ByVal bv_intRoleRightID As Int32) As Boolean
        Dim objRoleRight As New RoleRights
        Dim blnDeleted As Boolean
        Try
            blnDeleted = objRoleRight.DeleteRoleRight(bv_intRoleRightID)
            Return blnDeleted
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "ShowDashboard()"
    <OperationContract()> _
    Public Function pub_ShowDashboard(ByVal bv_intRoleID As Int32) As Boolean
        Dim dsRoleRight As RoleRightDataSet
        Dim objRoleRight As New RoleRights
        Try
            dsRoleRight = objRoleRight.ShowDashboard(bv_intRoleID)
            If dsRoleRight.Tables(RoleRightData._ROLE).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
End Class
