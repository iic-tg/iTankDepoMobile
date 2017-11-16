
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class Users

#Region "Declarations..."
    Private Const UserDetailSelectQueryByEmailID As String = "SELECT USR_ID,USR_NAM,PSSWRD,FRST_NAM,LST_NAM,EML_ID,RL_ID,RL_CD,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID ,DPT_CD,DPT_NAM,CMPNY_LG_PTH,LGN_DT FROM V_USER_DETAIL WHERE EML_ID=@EML_ID"
    Private Const UserDetailSelectQueryByUserName As String = "SELECT USR_ID,USR_NAM,PSSWRD,FRST_NAM,LST_NAM,EML_ID,RL_ID,RL_CD,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,CMPNY_LG_PTH,DPT_VT_NO,FAV_ACTVTY_ID_CSV,LGN_DT,PHT_PTH,THM_NAM,(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VU.DPT_ID AND BNK_TYP_ID = 44)LCL_CRRNCY_ID,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID IN (SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VU.DPT_ID AND BNK_TYP_ID = 44))LCL_CRRNCY_CD,ORGNZTN_TYP_ID,ORGNZTN_TYP_CD,YRD_LCTN,LCTN_OF_CLNNG FROM V_USER_DETAIL VU WHERE USR_NAM=@USR_NAM"
    Private Const UserDetailSelectQueryByUserID As String = "SELECT USR_ID,USR_NAM,PSSWRD,FRST_NAM,LST_NAM,EML_ID,RL_ID,RL_CD,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,CMPNY_LG_PTH,DPT_VT_NO,LGN_DT,THM_NAM,PHT_PTH FROM V_USER_DETAIL WHERE USR_ID=@USR_ID"
    Private Const UserDetailSelectQueryByUserNameAndDepotId As String = "SELECT USR_ID,USR_NAM,PSSWRD,FRST_NAM,LST_NAM,EML_ID,RL_ID,RL_CD,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,CMPNY_LG_PTH,FAV_ACTVTY_ID_CSV,LGN_DT FROM V_USER_DETAIL WHERE USR_NAM=@USR_NAM AND DPT_ID=@DPT_ID"
    Private Const UserDetailUpdateQueryWithoutPassword As String = "UPDATE USER_DETAIL SET FRST_NAM=@FRST_NAM, LST_NAM=@LST_NAM, EML_ID=@EML_ID, DPT_ID=@DPT_ID, RL_ID=@RL_ID, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT,THM_NAM=@THM_NAM,PHT_PTH=@PHT_PTH WHERE USR_ID=@USR_ID"
    Private Const UserDetailSelectQueryByUserNameAndPassword As String = "SELECT USR_ID,USR_NAM,PSSWRD,FRST_NAM,LST_NAM,EML_ID,RL_ID,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM USER_DETAIL WHERE USR_NAM=@USR_NAM AND PSSWRD=@PSSWRD"
    Private Const UserDetailUpdatePasswordQuery As String = "UPDATE USER_DETAIL SET PSSWRD=@PSSWRD,MDFD_DT=@MDFD_DT WHERE USR_NAM=@USR_NAM"
    Private Const UserDetailSelectQueryByUserNameAndEmailID As String = "SELECT USR_ID,USR_NAM,PSSWRD,FRST_NAM,LST_NAM,EML_ID,RL_ID,RL_CD,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,DPT_CD FROM V_USER_DETAIL WHERE USR_NAM=@USR_NAM AND EML_ID=@EML_ID"
    Private Const ActivitySelectQuery As String = "SELECT ACTVTY_ID,ACTVTY_NAM,PRCSS_ID,LST_QRY,LST_URL,LST_TTL,LST_CLCNT,PG_URL,PG_TTL,TBL_NAM,ORDR_NO,CRT_RGHT_BT,EDT_RGHT_BT,ACTV_BT,MSTR_ID_CSV,QCK_LNK_ID_CSV FROM ACTIVITY"
    Private Const EnumSelectQuery As String = "SELECT ENM_ID,ENM_CD,ENM_DSCRPTN_VC,ENM_TYP_ID,ENM_TYP_CD FROM ENUM"

    'UserDetail
    Private Const UserDetailInsertQuery As String = "INSERT INTO USER_DETAIL(USR_ID,USR_NAM,PSSWRD,FRST_NAM,LST_NAM,EML_ID,RL_ID,MDFD_BY,MDFD_DT,ACTV_BT,THM_NAM,PHT_PTH,DPT_ID)VALUES(@USR_ID,@USR_NAM,@PSSWRD,@FRST_NAM,@LST_NAM,@EML_ID,@RL_ID,@MDFD_BY,@MDFD_DT,@ACTV_BT,@THM_NAM,@PHT_PTH,@DPT_ID)"
    Private Const UserDetailUpdateQuery As String = "UPDATE USER_DETAIL SET USR_ID=@USR_ID, USR_NAM=@USR_NAM, PSSWRD=@PSSWRD, FRST_NAM=@FRST_NAM, LST_NAM=@LST_NAM, EML_ID=@EML_ID, RL_ID=@RL_ID, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT,THM_NAM=@THM_NAM,PHT_PTH=@PHT_PTH, DPT_ID=@DPT_ID WHERE USR_ID=@USR_ID"

    'UserLog
    Private Const USER_LOGInsertQuery As String = "INSERT INTO USER_LOG(USR_NAM,LGN_DT,LGT_DT,IP_ADDRSS,SSN_ID,USR_AGNT,BRWSR,SCRN_SZ)VALUES(@USR_NAM,@LGN_DT,@LGT_DT,@IP_ADDRSS,@SSN_ID,@USR_AGNT,@BRWSR,@SCRN_SZ)"
    Private Const USER_LOGUpdateQuery As String = "UPDATE USER_LOG SET USR_NAM=@USR_NAM, IP_ADDRSS=@IP_ADDRSS, SSN_ID=@SSN_ID,LGT_DT=@LGT_DT WHERE USR_NAM=@USR_NAM AND SSN_ID=@SSN_ID AND LGT_DT IS NULL  "


    'Depot
    Private Const DepotSelectQueryPk As String = "SELECT DPT_ID,DPT_CD,DPT_NAM,CNTCT_PRSN_NAM,ADDRSS_LN1_VC,ADDRSS_LN2_VC,ADDRSS_LN3_VC,CNTRY_ID,CTY_ID,ZP_CD,VT_NO,PHN_NO,FX_NO,CRRNCY_ID,CMPNY_LG_PTH,MDFD_BY,MDFD_DT FROM Depot WHERE DPT_ID=@DPT_ID"

    Private Const APP_VersionSelectQuery As String = "SELECT APP_VERSION_ID,APP_VERSION_NO,APP_VERSION_CHANGES,ACTV_BT,DPT_ID FROM APP_VERSION WHERE ACTV_BT = 1 ORDER BY APP_VERSION_ID DESC"
    Private Const UserDetailSelectQueryByUserNameMultipleDepot As String = "SELECT USR_ID,USR_NAM,PSSWRD,FRST_NAM,LST_NAM,EML_ID,RL_ID,RL_CD,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,CMPNY_LG_PTH,FAV_ACTVTY_ID_CSV,LGN_DT FROM V_USER_DETAIL WHERE USR_NAM=@USR_NAM"
    Dim objData As DataObjects
    Dim ds As UserDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New UserDataSet
    End Sub

#End Region

#Region "CreateUserDetail() TABLE NAME:USER_DETAIL"

    Public Function CreateUserDetail(ByVal bv_strUSR_NAM As String, _
        ByVal bv_strPSSWRD As String, _
        ByVal bv_strFRST_NAM As String, _
        ByVal bv_strLST_NAM As String, _
        ByVal bv_strEML_ID As String, _
        ByVal bv_i32RL_ID As Int32, _
        ByVal bv_strMDFD_BY As String, _
        ByVal bv_datMDFD_DT As DateTime, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_strPHT_PTH As String, _
        ByVal bv_strTHM_NAM As String, _
        ByVal bv_i32DPT_ID As Int32) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(UserData._USER_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(UserData._USER_DETAIL)
                .Item(UserData.USR_ID) = intMax
                .Item(UserData.USR_NAM) = bv_strUSR_NAM
                .Item(UserData.PSSWRD) = bv_strPSSWRD
                If bv_strFRST_NAM <> Nothing Then
                    .Item(UserData.FRST_NAM) = bv_strFRST_NAM
                Else
                    .Item(UserData.FRST_NAM) = DBNull.Value
                End If
                If bv_strLST_NAM <> Nothing Then
                    .Item(UserData.LST_NAM) = bv_strLST_NAM
                Else
                    .Item(UserData.LST_NAM) = DBNull.Value
                End If
                If bv_strEML_ID <> Nothing Then
                    .Item(UserData.EML_ID) = bv_strEML_ID
                Else
                    .Item(UserData.EML_ID) = DBNull.Value
                End If
                If bv_i32RL_ID <> 0 Then
                    .Item(UserData.RL_ID) = bv_i32RL_ID
                Else
                    .Item(UserData.RL_ID) = DBNull.Value
                End If
                If bv_strMDFD_BY <> Nothing Then
                    .Item(UserData.MDFD_BY) = bv_strMDFD_BY
                Else
                    .Item(UserData.MDFD_BY) = DBNull.Value
                End If
                If bv_datMDFD_DT <> Nothing Then
                    .Item(UserData.MDFD_DT) = bv_datMDFD_DT
                Else
                    .Item(UserData.MDFD_DT) = DBNull.Value
                End If
                If bv_blnACTV_BT <> Nothing Then
                    .Item(UserData.ACTV_BT) = bv_blnACTV_BT
                Else
                    .Item(UserData.ACTV_BT) = DBNull.Value
                End If
                If bv_i32DPT_ID <> 0 Then
                    .Item(UserData.DPT_ID) = bv_i32DPT_ID
                Else
                    .Item(UserData.DPT_ID) = DBNull.Value
                End If
                If bv_strPHT_PTH <> Nothing Then
                    .Item(UserData.PHT_PTH) = bv_strPHT_PTH
                Else
                    .Item(UserData.PHT_PTH) = DBNull.Value
                End If
                If bv_strTHM_NAM <> Nothing Then
                    .Item(UserData.THM_NAM) = bv_strTHM_NAM
                Else
                    .Item(UserData.THM_NAM) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, UserDetailInsertQuery)
            dr = Nothing
            CreateUserDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateUserDetail() TABLE NAME:UserDetail"

    Public Function UpdateUserDetail(ByVal bv_i32USR_ID As Int32, _
        ByVal bv_strUSR_NAM As String, _
        ByVal bv_strPSSWRD As String, _
        ByVal bv_strFRST_NAM As String, _
        ByVal bv_strLST_NAM As String, _
        ByVal bv_strEML_ID As String, _
        ByVal bv_i32RL_ID As Int32, _
        ByVal bv_strMDFD_BY As String, _
        ByVal bv_datMDFD_DT As DateTime, _
        ByVal bv_blnACTV_BT As Boolean, _
             ByVal bv_strPHT_PTH As String, _
        ByVal bv_strTHM_NAM As String, _
        ByVal bv_i32DPT_ID As Int32) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(UserData._USER_DETAIL).NewRow()
            Dim strUpdateQuery As String = ""
            With dr
                .Item(UserData.USR_ID) = bv_i32USR_ID
                .Item(UserData.USR_NAM) = bv_strUSR_NAM
                .Item(UserData.PSSWRD) = bv_strPSSWRD
                If bv_strFRST_NAM <> Nothing Then
                    .Item(UserData.FRST_NAM) = bv_strFRST_NAM
                Else
                    .Item(UserData.FRST_NAM) = DBNull.Value
                End If
                If bv_strLST_NAM <> Nothing Then
                    .Item(UserData.LST_NAM) = bv_strLST_NAM
                Else
                    .Item(UserData.LST_NAM) = DBNull.Value
                End If
                If bv_strEML_ID <> Nothing Then
                    .Item(UserData.EML_ID) = bv_strEML_ID
                Else
                    .Item(UserData.EML_ID) = DBNull.Value
                End If
                If bv_i32RL_ID <> 0 Then
                    .Item(UserData.RL_ID) = bv_i32RL_ID
                Else
                    .Item(UserData.RL_ID) = DBNull.Value
                End If
                If bv_strMDFD_BY <> Nothing Then
                    .Item(UserData.MDFD_BY) = bv_strMDFD_BY
                Else
                    .Item(UserData.MDFD_BY) = DBNull.Value
                End If
                If bv_datMDFD_DT <> Nothing Then
                    .Item(UserData.MDFD_DT) = bv_datMDFD_DT
                Else
                    .Item(UserData.MDFD_DT) = DBNull.Value
                End If
                If bv_blnACTV_BT <> Nothing Then
                    .Item(UserData.ACTV_BT) = bv_blnACTV_BT
                Else
                    .Item(UserData.ACTV_BT) = DBNull.Value
                End If
                If bv_i32DPT_ID <> 0 Then
                    .Item(UserData.DPT_ID) = bv_i32DPT_ID
                Else
                    .Item(UserData.DPT_ID) = DBNull.Value
                End If
                If bv_strPHT_PTH <> Nothing Then
                    .Item(UserData.PHT_PTH) = bv_strPHT_PTH
                Else
                    .Item(UserData.PHT_PTH) = DBNull.Value
                End If
                If bv_strTHM_NAM <> Nothing Then
                    .Item(UserData.THM_NAM) = bv_strTHM_NAM
                Else
                    .Item(UserData.THM_NAM) = DBNull.Value
                End If
            End With
            If bv_strPSSWRD = "" Then
                strUpdateQuery = UserDetailUpdateQueryWithoutPassword
            Else
                strUpdateQuery = UserDetailUpdateQuery
            End If
            UpdateUserDetail = objData.UpdateRow(dr, strUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateUSER_LOG() TABLE NAME:USER_LOG"

    Public Function CreateUSER_LOG(ByVal bv_strUSR_NAM As String, _
        ByVal bv_datLGN_DT As DateTime, _
        ByVal bv_datLGT_DT As DateTime, _
        ByVal bv_strIP_ADDRSS As String, _
        ByVal bv_strSSN_ID As String, _
        ByVal bv_strUSR_AGNT As String, _
        ByVal bv_strBRWSR As String, _
        ByVal bv_strSCRN_SZ As String) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(UserData._USER_LOG).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(UserData._USER_LOG)
                .Item(UserData.USR_NAM) = bv_strUSR_NAM
                .Item(UserData.LGN_DT) = bv_datLGN_DT
                If bv_datLGT_DT <> Nothing Then
                    .Item(UserData.LGT_DT) = bv_datLGT_DT
                Else
                    .Item(UserData.LGT_DT) = DBNull.Value
                End If
                .Item(UserData.IP_ADDRSS) = bv_strIP_ADDRSS
                .Item(UserData.SSN_ID) = bv_strSSN_ID
                .Item(UserData.USR_AGNT) = bv_strUSR_AGNT
                If bv_strBRWSR <> Nothing Then
                    .Item(UserData.BRWSR) = bv_strBRWSR
                Else
                    .Item(UserData.BRWSR) = DBNull.Value
                End If
                .Item(UserData.SCRN_SZ) = bv_strSCRN_SZ
            End With
            objData.InsertRow(dr, USER_LOGInsertQuery)
            dr = Nothing
            CreateUSER_LOG = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateUSER_LOG() TABLE NAME:USER_LOG"

    Public Function UpdateUSER_LOG(ByVal bv_strUSR_NAM As String, _
                                   ByVal bv_datLgtDat As DateTime, _
        ByVal bv_strIP_ADDRSS As String, _
        ByVal bv_strSSN_ID As String) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(UserData._USER_LOG).NewRow()
            With dr
                .Item(UserData.USR_NAM) = bv_strUSR_NAM
                .Item(UserData.LGT_DT) = bv_datLgtDat
                .Item(UserData.IP_ADDRSS) = bv_strIP_ADDRSS
                .Item(UserData.SSN_ID) = bv_strSSN_ID
            End With
            UpdateUSER_LOG = objData.UpdateRow(dr, USER_LOGUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetUserDetailByUserName"

    Public Function GetUserDetailByUserName(ByVal bv_strUserName As String) As UserDataSet
        Try
            objData = New DataObjects(UserDetailSelectQueryByUserName, UserData.USR_NAM, bv_strUserName)
            objData.Fill(CType(ds, DataSet), UserData._V_USER_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetUserDetailByUserId"

    Public Function GetUserDetailByUserId(ByVal bv_intUserId As Integer) As UserDataSet
        Try
            objData = New DataObjects(UserDetailSelectQueryByUserID, UserData.USR_ID, bv_intUserId)
            objData.Fill(CType(ds, DataSet), UserData._V_USER_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetUserDetailByUserNameAndDepotId"

    Public Function GetUserDetailByUserNameAndDepotId(ByVal bv_strUserName As String, ByVal bv_i32DepotId As Int32) As UserDataSet
        Try
            Dim htblParm As New Hashtable
            htblParm.Add(UserData.USR_NAM, bv_strUserName)
            htblParm.Add(UserData.DPT_ID, bv_i32DepotId)
            objData = New DataObjects(UserDetailSelectQueryByUserNameAndDepotId, htblParm)
            objData.Fill(CType(ds, DataSet), UserData._V_USER_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetUserByUserNameAndPassword"
    Public Function GetUserByUserNameAndPassword(ByVal bv_strPassword As String, ByVal bv_strUserName As String) As UserDataSet
        Try
            Dim htblParm As New Hashtable
            htblParm.Add(UserData.PSSWRD, bv_strPassword)
            htblParm.Add(UserData.USR_NAM, bv_strUserName)
            objData = New DataObjects(UserDetailSelectQueryByUserNameAndPassword, htblParm)
            objData.Fill(CType(ds, DataSet), UserData._V_USER_DETAIL)
            htblParm = Nothing
            Return ds
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "GetUserDetailByEmailID"

    Public Function GetUserDetailByEmailID(ByVal bv_strEmailID As String) As UserDataSet
        Try
            objData = New DataObjects(UserDetailSelectQueryByEmailID, UserData.EML_ID, bv_strEmailID)
            objData.Fill(CType(ds, DataSet), UserData._V_USER_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdatePassword"
    Public Function UpdatePassword(ByVal bv_strUserName As String, ByVal bv_strPassword As String) As Boolean
        Try
            Dim dr As DataRow
            Dim blnUpdate As Boolean
            objData = New DataObjects()
            dr = ds.Tables(UserData._USER_DETAIL).NewRow()
            With dr
                .Item(UserData.USR_NAM) = bv_strUserName
                .Item(UserData.PSSWRD) = bv_strPassword
                .Item(UserData.MDFD_DT) = DateTime.Now

            End With

            blnUpdate = objData.UpdateRow(dr, UserDetailUpdatePasswordQuery)
            Return blnUpdate
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetUserDetailByUserNameAndEmailID"

    Public Function GetUserDetailByUserNameAndEmailID(ByVal bv_strUserName As String, ByVal bv_strEmailID As String) As UserDataSet
        Try
            Dim htblParm As New Hashtable
            htblParm.Add(UserData.USR_NAM, bv_strUserName)
            htblParm.Add(UserData.EML_ID, bv_strEmailID)
            objData = New DataObjects(UserDetailSelectQueryByUserNameAndEmailID, htblParm)
            objData.Fill(CType(ds, DataSet), UserData._V_USER_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetActivities"

    Public Function GetActivities() As UserDataSet
        Try
            objData = New DataObjects(ActivitySelectQuery)
            objData.Fill(CType(ds, DataSet), UserData._ACTIVITY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Get Enum Table"

    Public Function GetEnumTable() As UserDataSet
        Try
            objData = New DataObjects(EnumSelectQuery)
            objData.Fill(CType(ds, DataSet), UserData._ENUM)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetDepotByDPT_ID() TABLE NAME:Depot"

    Public Function GetDepotByDPT_ID(ByVal bv_i32DPT_ID As Int32) As UserDataSet

        Try
            objData = New DataObjects(DepotSelectQueryPk, UserData.DPT_ID, CStr(bv_i32DPT_ID))
            objData.Fill(CType(ds, DataSet), UserData._DEPOT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetAPP_Version"
    Public Function GetAPP_Version() As UserDataSet
        Try
            objData = New DataObjects(APP_VersionSelectQuery)
            objData.Fill(CType(ds, DataSet), UserData._APP_VERSION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "GetUserDetailByUserNameAndDepotId"

    Public Function GetUserDetailByUserNameAndDepotId(ByVal bv_strUserName As String) As UserDataSet
        Try
            Dim htblParm As New Hashtable
            htblParm.Add(UserData.USR_NAM, bv_strUserName)
            objData = New DataObjects(UserDetailSelectQueryByUserNameMultipleDepot, htblParm)
            objData.Fill(CType(ds, DataSet), UserData._V_USER_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class