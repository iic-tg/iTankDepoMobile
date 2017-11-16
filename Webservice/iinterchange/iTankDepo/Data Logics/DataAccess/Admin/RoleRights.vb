Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class RoleRights

#Region "Declaration Part.. "
    Dim objData As DataObjects
    Private Const ActivitySelectQuery As String = "SELECT ACTVTY_ID,ACTVTY_NAM,PRCSS_ID,LST_QRY,LST_URL,LST_TTL,LST_CLCNT,PG_URL,PG_TTL,TBL_NAM,ORDR_NO,CRT_RGHT_BT,EDT_RGHT_BT,ACTV_BT,ACTVTY_RL,EXCPTN_BT,CNCL_RGHT_BT FROM ACTIVITY WHERE ACTV_BT=1 ORDER BY PRCSS_ID,ORDR_NO ASC"
    Private Const ProcessSelectQuery As String = "SELECT PRCSS_ID,PRCSS_NAM,PRNT_ID,PRCSS_ORDR,PRCSS_RL FROM PROCESS"
    Private Const ParentProcessSelectQuery As String = "SELECT PRCSS_ID,PRCSS_NAM,PRNT_ID,PRCSS_ORDR ,PRCSS_RL FROM PROCESS WHERE PRNT_ID IS NULL"
    Private Const RoleRightDeleteQuery As String = "DELETE FROM ROLE_RIGHT WHERE RL_ID=@RL_ID"
    Private Const RoleRightInsertQuery As String = "INSERT INTO ROLE_RIGHT(RL_RGHT_ID,RL_ID,ACTVTY_ID,VW_BT,EDT_BT,CRT_BT,CNCL_BT)VALUES(@RL_RGHT_ID,@RL_ID,@ACTVTY_ID,@VW_BT,@EDT_BT,@CRT_BT,@CNCL_BT)"
    Private Const RoleRightSelectQueryPk As String = "SELECT RL_RGHT_ID,RL_ID,ACTVTY_ID,(SELECT PRCSS_ID FROM ACTIVITY WHERE ACTIVITY.ACTVTY_ID=ROLE_RIGHT.ACTVTY_ID) AS PRCSS_ID , VW_BT,EDT_BT,CRT_BT,CNCL_BT FROM ROLE_RIGHT WHERE RL_ID=@RL_ID"
    Private Const RoleSelectQueryPk As String = "SELECT RL_ID,RL_CD,RL_DSCRPTN_VC,ACTV_BT FROM ROLE WHERE RL_CD=@RL_CD AND RL_ID<>@RL_ID"
    ' Private Const RoleInsertQuery As String = "INSERT INTO ROLE(ACTV_BT,RL_ID,RL_CD,RL_DSCRPTN_VC,MDFD_BY,MDFD_DT,HD_QRTRS_ID)VALUES(@ACTV_BT,@RL_ID,@RL_CD,@RL_DSCRPTN_VC,@MDFD_BY,@MDFD_DT,@HD_QRTRS_ID)"
    Private Const RoleRightProcessParentQueryByRoleId As String = "SELECT PRCSS_ID ,PRCSS_NAM,PRNT_ID,PRCSS_ORDR,PRCSS_RL FROM PROCESS WHERE PRCSS_ID IN (SELECT DISTINCT(PRCSS_ID) FROM ACTIVITY WHERE ACTV_BT=1 AND ACTVTY_ID IN (SELECT ACTVTY_ID FROM ROLE_RIGHT WHERE RL_ID=@RL_ID)) UNION " & _
                                                                 "SELECT PRCSS_ID ,PRCSS_NAM,PRNT_ID,PRCSS_ORDR,PRCSS_RL FROM PROCESS WHERE PRCSS_ID IN (SELECT DISTINCT(PRNT_ID) FROM PROCESS WHERE PRCSS_ID IN(SELECT DISTINCT(PRCSS_ID) " & _
                                                                 "FROM ACTIVITY WHERE ACTV_BT=1 AND ACTVTY_ID IN (SELECT ACTVTY_ID FROM ROLE_RIGHT WHERE RL_ID=@RL_ID))) AND PRNT_ID IS NULL ORDER BY PRCSS_ORDR ASC"
    Private Const RoleRightActivitySelectQueryByRoleId As String = " SELECT ACTVTY_ID,ACTVTY_NAM,PRCSS_ID,LST_QRY,LST_URL,LST_TTL,LST_CLCNT,PG_URL,PG_TTL,TBL_NAM,ORDR_NO,CRT_RGHT_BT,EDT_RGHT_BT,ACTV_BT,ACTVTY_RL,CRT_RGHT_BT FROM ACTIVITY  WHERE ACTVTY_ID IN (SELECT ACTVTY_ID FROM ROLE_RIGHT WHERE RL_ID=@RL_ID) ORDER BY PRCSS_ID,ORDR_NO ASC"
    Private Const RoleRightProcessSelectQueryByRoleId As String = "SELECT PRCSS_ID ,PRCSS_NAM,PRNT_ID,PRCSS_ORDR,PRCSS_RL  FROM PROCESS WHERE PRCSS_ID IN (SELECT DISTINCT(PRCSS_ID) FROM ACTIVITY WHERE ACTVTY_ID IN (SELECT ACTVTY_ID FROM ROLE_RIGHT WHERE RL_ID=@RL_ID)) ORDER BY PRCSS_ORDR ASC"
    'Private Const RoleUpdateQuery As String = "UPDATE ROLE SET ACTV_BT=@ACTV_BT, RL_CD=@RL_CD, RL_DSCRPTN_VC=@RL_DSCRPTN_VC,MDFD_BY=@MDFD_BY,MDFD_DT=@MDFD_DT,HD_QRTRS_ID=@HD_QRTRS_ID WHERE RL_ID=@RL_ID"
    Private Const RoleInsertQuery As String = "INSERT INTO ROLE(RL_ID,RL_CD,RL_DSCRPTN_VC,MDFD_DT,MDFD_BY,ACTV_BT,DPT_ID,DSHBRD_BT)VALUES(@RL_ID,@RL_CD,@RL_DSCRPTN_VC,@MDFD_DT,@MDFD_BY,@ACTV_BT,@DPT_ID,@DSHBRD_BT)"
    Private Const RoleUpdateQuery As String = "UPDATE ROLE SET RL_ID=@RL_ID, RL_CD=@RL_CD, RL_DSCRPTN_VC=@RL_DSCRPTN_VC, MDFD_DT=@MDFD_DT, MDFD_BY=@MDFD_BY, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID,DSHBRD_BT=@DSHBRD_BT WHERE RL_ID=@RL_ID"
    Private Const RoleSelectQueryByRoleID As String = "SELECT RL_CD ,RL_DSCRPTN_VC ,DSHBRD_BT ,ACTV_BT  FROM ROLE WHERE RL_ID=@RL_ID AND DSHBRD_BT=1"
    Private ds As RoleRightDataSet

#End Region

#Region "Constructor.."

    Sub New()
        ds = New RoleRightDataSet
    End Sub

#End Region

#Region "GetActivity() TABLE NAME@Activity"
    ''' <summary>
    ''' This method is to get activity data
    ''' </summary>
    ''' <returns>RoleRightDataSet</returns>
    ''' <remarks></remarks>
    Public Function GetActivity() As RoleRightDataSet
        Try
            objData = New DataObjects(ActivitySelectQuery)
            objData.Fill(CType(ds, DataSet), RoleRightData._ACTIVITY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetProcess() TABLE NAME@Process"
    ''' <summary>
    ''' This method is to get process data from process table
    ''' </summary>
    ''' <returns>RoleRightDataSet</returns>
    ''' <remarks></remarks>
    Public Function GetProcess() As RoleRightDataSet
        Try
            objData = New DataObjects(ProcessSelectQuery)
            objData.Fill(CType(ds, DataSet), RoleRightData._PROCESS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetParentProcess() TABLE NAME@Process"
    ''' <summary>
    ''' This method is to get parent process for menu
    ''' </summary>
    ''' <returns>RoleRightDataSet</returns>
    ''' <remarks></remarks>
    Public Function GetParentProcess() As RoleRightDataSet
        Try
            objData = New DataObjects(ParentProcessSelectQuery)
            objData.Fill(CType(ds, DataSet), RoleRightData._PROCESS_PARENT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRoleRightByRoleID()"
    ''' <summary>
    ''' This method is to get role rights by role id
    ''' </summary>
    ''' <param name="bv_intRoleID"></param>
    ''' <returns>RoleRightDataSet</returns>
    ''' <remarks></remarks>
    Public Function GetRoleRightByRoleID(ByVal bv_intRoleID As Int32) As RoleRightDataSet
        Try
            objData = New DataObjects(RoleRightSelectQueryPk, RoleRightData.RL_ID, CStr(bv_intRoleID))
            objData.Fill(CType(ds, DataSet), RoleRightData._ROLE_RIGHT)
            Return ds
        Catch ex As Exception
        End Try
    End Function
#End Region

#Region "GetParentProcessByRoleID() TABLE NAME@Activity"
    ''' <summary>
    ''' Added to get parent processes by RoleID
    ''' </summary>
    ''' <param name="bv_intRoleID"></param>
    ''' <returns>RoleRightDataSet</returns>
    ''' <remarks></remarks>
    Public Function GetParentProcessByRoleID(ByVal bv_intRoleID As Int32) As RoleRightDataSet
        Try
            objData = New DataObjects(RoleRightProcessParentQueryByRoleId, RoleRightData.RL_ID, CStr(bv_intRoleID))
            objData.Fill(CType(ds, DataSet), RoleRightData._PARENTPROCESS_ROLE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRoleRightActivityByRoleID()"
    ''' <summary>
    ''' This method is to get activity by role id
    ''' </summary>
    ''' <param name="bv_intRoleID"></param>
    ''' <returns>RoleRightDataSet</returns>
    ''' <remarks></remarks>
    Public Function GetRoleRightActivityByRoleID(ByVal bv_intRoleID As Int32) As RoleRightDataSet
        Try
            objData = New DataObjects(RoleRightActivitySelectQueryByRoleId, RoleRightData.RL_ID, CStr(bv_intRoleID))
            objData.Fill(CType(ds, DataSet), RoleRightData._ACTIVITY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRoleByRoleID()"
    ''' <summary>
    ''' This method is to get role by role id
    ''' </summary>
    ''' <param name="bv_strRole"></param>
    ''' <param name="bv_intRoleID"></param>
    ''' <returns>RoleRightDataSet</returns>
    ''' <remarks></remarks>
    Public Function GetRoleByRoleID(ByVal bv_strRole As String, ByVal bv_intRoleID As Int32) As RoleRightDataSet
        Try
            Dim hstbl As New Hashtable
            hstbl.Add(RoleRightData.RL_ID, bv_intRoleID)
            hstbl.Add(RoleRightData.RL_CD, bv_strRole)
            objData = New DataObjects(RoleSelectQueryPk, hstbl)
            objData.Fill(CType(ds, DataSet), RoleRightData._ROLE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRoleRightProcessByRoleID() TABLE NAME@Process"
    ''' <summary>
    ''' This method is to get process based on role id
    ''' </summary>
    ''' <param name="bv_intRoleID"></param>
    ''' <returns>RoleRightDataSet</returns>
    ''' <remarks></remarks>
    Public Function GetRoleRightProcessByRoleID(ByVal bv_intRoleID As Int32) As RoleRightDataSet
        Try
            objData = New DataObjects(RoleRightProcessSelectQueryByRoleId, RoleRightData.RL_ID, CStr(bv_intRoleID))
            objData.Fill(CType(ds, DataSet), RoleRightData._PROCESS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateRole()"
    ''' <summary>
    ''' This method is to insert role in db
    ''' </summary>
    ''' <param name="bv_strRoleCode"></param>
    ''' <param name="bv_strRoleDescription"></param>
    ''' <param name="bv_blnActiveBit"></param>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Function CreateRole(ByVal bv_strRoleCode As String, _
                                ByVal bv_strRoleDescription As String, _
                                ByVal bv_strModifiedBy As String, _
                                ByVal bv_datModifiedDate As DateTime, _
                                ByVal bv_blnActiveBit As Boolean, _
                                ByVal bv_i32DPT_ID As Integer, _
                                ByVal bv_blnDashboardActiviveBit As Boolean) As Long
        Dim dr As DataRow
        Dim intMax As Long
        objData = New DataObjects()
        Try
            dr = ds.Tables(RoleRightData._ROLE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(RoleRightData._ROLE)
                .Item(RoleRightData.RL_ID) = intMax
                .Item(RoleRightData.RL_CD) = bv_strRoleCode
                .Item(RoleRightData.RL_DSCRPTN_VC) = bv_strRoleDescription
                .Item(RoleRightData.MDFD_BY) = bv_strModifiedBy
                .Item(RoleRightData.MDFD_DT) = bv_datModifiedDate
                If bv_blnActiveBit <> False Then
                    .Item(RoleRightData.ACTV_BT) = 1
                Else
                    .Item(RoleRightData.ACTV_BT) = 0
                End If
                If bv_blnDashboardActiviveBit <> False Then
                    .Item(RoleRightData.DSHBRD_BT) = 1
                Else
                    .Item(RoleRightData.DSHBRD_BT) = 0
                End If
                .Item(RoleRightData.DPT_ID) = bv_i32DPT_ID
            End With
            objData.InsertRow(dr, RoleInsertQuery)
            dr = Nothing
            CreateRole = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateRole()"
    ''' <summary>
    ''' This method is to update role in db
    ''' </summary>
    ''' <param name="bv_intRoleID"></param>
    ''' <param name="bv_strRoleCode"></param>
    ''' <param name="bv_strRoleDescription"></param>
    ''' <param name="bv_blnActiveBit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function UpdateRole(ByVal bv_intRoleID As Int32, _
                                ByVal bv_strRoleCode As String, _
                                ByVal bv_strRoleDescription As String, _
                                ByVal bv_strModifiedBy As String, _
                                ByVal bv_datModifiedDate As DateTime, _
                                ByVal bv_blnActiveBit As Boolean, _
                                ByVal bv_i32DPT_ID As Integer, _
                                ByVal bv_blnDashboardActiviveBit As Boolean) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(RoleRightData._ROLE).NewRow()
            With dr

                .Item(RoleRightData.RL_ID) = bv_intRoleID
                .Item(RoleRightData.RL_CD) = bv_strRoleCode
                .Item(RoleRightData.RL_DSCRPTN_VC) = bv_strRoleDescription
                .Item(RoleRightData.MDFD_BY) = bv_strModifiedBy
                .Item(RoleRightData.MDFD_DT) = bv_datModifiedDate
                If bv_blnActiveBit <> False Then
                    .Item(RoleRightData.ACTV_BT) = 1
                Else
                    .Item(RoleRightData.ACTV_BT) = 0
                End If
                .Item(RoleRightData.DPT_ID) = bv_i32DPT_ID
                If bv_blnDashboardActiviveBit <> False Then
                    .Item(RoleRightData.DSHBRD_BT) = 1
                Else
                    .Item(RoleRightData.DSHBRD_BT) = 0
                End If

            End With
            UpdateRole = objData.UpdateRow(dr, RoleUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateRoleRight()"
    ''' <summary>
    ''' This method is to insert role rights in db
    ''' </summary>
    ''' <param name="bv_intRoleID"></param>
    ''' <param name="bv_intActivityID"></param>
    ''' <param name="bv_blnViewBit"></param>
    ''' <param name="bv_blnEditBit"></param>
    ''' <param name="bv_blnCreateBit"></param>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Function CreateRoleRight(ByVal bv_intRoleID As Int32, _
                                    ByVal bv_intActivityID As Int32, _
                                    ByVal bv_blnViewBit As Boolean, _
                                    ByVal bv_blnEditBit As Boolean, _
                                    ByVal bv_blnCreateBit As Boolean, _
                                    ByVal bv_blnCancelBit As Boolean) As Long
        Dim dr As DataRow
        Dim intMax As Long
        objData = New DataObjects()
        Try
            dr = ds.Tables(RoleRightData._ROLE_RIGHT).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(RoleRightData._ROLE_RIGHT)
                .Item(RoleRightData.RL_RGHT_ID) = intMax
                .Item(RoleRightData.RL_ID) = bv_intRoleID
                .Item(RoleRightData.ACTVTY_ID) = bv_intActivityID
                If bv_blnViewBit <> False Then
                    .Item(RoleRightData.VW_BT) = 1
                Else
                    .Item(RoleRightData.VW_BT) = 0
                End If
                If bv_blnEditBit <> False Then
                    .Item(RoleRightData.EDT_BT) = 1
                Else
                    .Item(RoleRightData.EDT_BT) = 0
                End If
                If bv_blnCreateBit <> False Then
                    .Item(RoleRightData.CRT_BT) = 1
                Else
                    .Item(RoleRightData.CRT_BT) = 0
                End If

                If bv_blnCancelBit <> False Then
                    .Item(RoleRightData.CNCL_BT) = 1
                Else
                    .Item(RoleRightData.CNCL_BT) = 0
                End If

            End With
            objData.InsertRow(dr, RoleRightInsertQuery)
            dr = Nothing
            CreateRoleRight = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "DeleteRoleRight()"
    ''' <summary>
    ''' This method is to delete from role rights
    ''' </summary>
    ''' <param name="bv_intRoleID"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteRoleRight(ByVal bv_intRoleID As Int32) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(RoleRightData._ROLE_RIGHT).NewRow()
            With dr
                .Item(RoleRightData.RL_ID) = bv_intRoleID
            End With
            DeleteRoleRight = objData.DeleteRow(dr, RoleRightDeleteQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "ShowDashboard()"

    '''<summery>
    ''' This method for Show Dashboard
    ''' </summery>
    Public Function ShowDashboard(ByVal bv_intRoleID As Int32) As RoleRightDataSet

        Try
            objData = New DataObjects(RoleSelectQueryByRoleID, RoleRightData.RL_ID, CStr(bv_intRoleID))
            objData.Fill(CType(ds, DataSet), RoleRightData._ROLE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class
