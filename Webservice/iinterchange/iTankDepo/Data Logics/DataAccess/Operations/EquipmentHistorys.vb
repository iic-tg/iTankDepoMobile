Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

#Region "EquipmentHistorys"

Public Class EquipmentHistorys

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const V_Equipment_HistorySelectQuery As String = "SELECT TRCKNG_ID,CSTMR_ID,CSTMR_CD,EQPMNT_NO,ACTVTY_NAM,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,ACTVTY_NO,ACTVTY_DT,ACTVTY_RMRKS AS RMRKS_VC,ACTVTY_RMRKS,GI_TRNSCTN_NO,INVCNG_PRTY_ID,INVCNG_PRTY_CD,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,EIR_NO,RF_NO,INVC_GNRTN,CRTD_BY,CRTD_DT,CNCLD_BY,CNCLD_DT,ADT_RMRKS,DPT_ID,YRD_LCTN,CSTMR_NAM,RNTL_CSTMR_ID,RNTL_RFRNC_NO,ADDTNL_CLNNG_BT,AGNT_CD,STTS_CD,(SELECT DPT_CD FROM DEPOT WHERE DPT_ID=V.DPT_ID )DPT_CD FROM V_EQUIPMENT_HISTORY V WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND CNCLD_DT IS NULL ORDER BY TRCKNG_ID DESC"
    Private Const EQUIPMENT_DELETESelectQuery As String = "SELECT ACTIVITY_DELETE_ID,ACTIVITY_NAME,PARENT_TABLE,CHILD_TABLE,UPDATE_TABLE,FIELD_NAME,IS_DELETE,CONDITION,ALIAS_CONDITION,NOT_NULL_FILTERS FROM EQUIPMENT_DELETE WHERE ACTIVITY_NAME=@ACTIVITY_NAME ORDER BY ACTIVITY_DELETE_ID "
    Private Const TRACKING_UpdateQuery As String = "UPDATE TRACKING SET CNCLD_BY=@CNCLD_BY, CNCLD_DT=@CNCLD_DT, ADT_RMRKS=@ADT_RMRKS WHERE TRCKNG_ID=@TRCKNG_ID AND DPT_ID=@DPT_ID"
    Private Const V_Equipment_History_CleaningSelectQuery As String = "SELECT TRCKNG_ID,CSTMR_ID,CSTMR_CD,EQPMNT_NO,ACTVTY_NAM,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,ACTVTY_NO,ACTVTY_DT,ACTVTY_RMRKS AS RMRKS_VC,ACTVTY_RMRKS,GI_TRNSCTN_NO,INVCNG_PRTY_ID,INVCNG_PRTY_CD,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,EIR_NO,RF_NO,INVC_GNRTN,CRTD_BY,CRTD_DT,CNCLD_BY,CNCLD_DT,ADT_RMRKS,DPT_ID,YRD_LCTN,CSTMR_NAM FROM V_EQUIPMENT_HISTORY WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND CNCLD_DT IS NULL AND TRCKNG_ID < @TRCKNG_ID ORDER BY TRCKNG_ID DESC"
    Private Const ACTIVITY_STATUS_UpdateQuery As String = "UPDATE ACTIVITY_STATUS SET EQPMNT_STTS_ID=@EQPMNT_STTS_ID WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"

    Private ds As EquipmentHistoryDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New EquipmentHistoryDataSet
    End Sub

#End Region

#Region "GetEquipemntHistory() TABLE NAME:V_EQUIPMENT_HISTORY"

    Public Function GetEquipemntHistory(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As EquipmentHistoryDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentHistoryData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(EquipmentHistoryData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(V_Equipment_HistorySelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), EquipmentHistoryData._V_EQUIPMENT_HISTORY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentDelete() TABLE NAME:EQUIPMENT_DELETE"

    Public Function GetEquipmentDelete(ByVal bv_strActivityName As String, ByRef br_objTrans As Transactions) As EquipmentHistoryDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentHistoryData.ACTIVITY_NAME, bv_strActivityName)
            objData = New DataObjects(EQUIPMENT_DELETESelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), EquipmentHistoryData._EQUIPMENT_DELETE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UPDATE : UpdateEquipmentActivity() TABLE NAME:ACTIVITY"

    Public Function UpdateEquipmentActivity(ByVal bv_strQuery As String, _
                                            ByVal bv_strWhere As String, _
                                            ByVal bv_strTableName As String, _
                                            ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim strQuery As String = String.Concat("UPDATE ", bv_strTableName, " SET ", bv_strQuery, " WHERE ", bv_strWhere)
            objData = New DataObjects(strQuery)
            objData.ExecuteScalar(br_objTrans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTracking() TABLE NAME:TRACKING"

    Public Function UpdateTracking(ByVal bv_TrackingID As Int64, _
        ByVal bv_strCanceledBy As String, _
        ByVal bv_datCanceledDate As DateTime, _
        ByVal bv_strAuditRemarks As String, _
        ByVal bv_i32DepotId As Int32, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentHistoryData._V_TRACKING).NewRow()
            With dr
                .Item(EquipmentHistoryData.TRCKNG_ID) = bv_TrackingID
                .Item(EquipmentHistoryData.CNCLD_BY) = bv_strCanceledBy
                .Item(EquipmentHistoryData.CNCLD_DT) = bv_datCanceledDate
                .Item(EquipmentHistoryData.DPT_ID) = bv_i32DepotId
                .Item(EquipmentHistoryData.ADT_RMRKS) = bv_strAuditRemarks
            End With
            UpdateTracking = objData.UpdateRow(dr, TRACKING_UpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DELETE : DeleteEquipmentActivity() TABLE NAME:ACTIVITY"

    Public Function DeleteEquipmentActivity(ByVal bv_strWhere As String, _
                                            ByVal bv_strTableName As String, _
                                            ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim strQuery As String = String.Concat("DELETE FROM ", bv_strTableName, " WHERE ", bv_strWhere)
            objData = New DataObjects(strQuery)
            objData.ExecuteScalar(br_objTrans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCleaningDetail() TABLE NAME:V_EQUIPMENT_HISTORY"

    Public Function GetCleaningDetail(ByVal bv_intTrackingID As Int64, ByVal bv_i32DepotID As Int32, _
                                      ByVal bv_strEquipmentNo As String, ByRef br_objTrans As Transactions) As EquipmentHistoryDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentHistoryData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(EquipmentHistoryData.TRCKNG_ID, bv_intTrackingID)
            hshParameters.Add(EquipmentHistoryData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(V_Equipment_History_CleaningSelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), EquipmentHistoryData._V_EQUIPMENT_HISTORY, br_objTrans)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateActivityStatus() TABLE NAME:ACTIVITY_STATUS"

    Public Function UpdateActivityStatus(ByVal bv_strGI_TransactionNo As String, _
                                         ByVal bv_intEqpmntStatusID As Integer, _
                                         ByVal bv_i32DepotId As Int32, _
                                         ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentHistoryData._V_TRACKING).NewRow()
            With dr
                .Item(EquipmentHistoryData.GI_TRNSCTN_NO) = bv_strGI_TransactionNo
                .Item(EquipmentHistoryData.EQPMNT_STTS_ID) = bv_intEqpmntStatusID
                .Item(EquipmentHistoryData.DPT_ID) = bv_i32DepotId
            End With
            UpdateActivityStatus = objData.UpdateRow(dr, ACTIVITY_STATUS_UpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Additional Cleaning Equipment Delete"
    Public Function check_AddtionalFlag_ExecuteScalar(ByVal bv_strQuery As String, ByRef br_objTrans As Transactions) As Boolean
        Try
            objData = New DataObjects(bv_strQuery)
            Return objData.ExecuteScalar(br_objTrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Query_ExecuteScalar(ByVal bv_strQuery As String, ByRef br_objTrans As Transactions) As String
        Try
            objData = New DataObjects(bv_strQuery)
            Return objData.ExecuteScalar(br_objTrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Query_Execute(ByVal bv_strQuery As String, ByRef br_objTrans As Transactions) As Int32
        Try
            objData = New DataObjects()
            Return objData.ExecuteNonQuery(bv_strQuery, br_objTrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class

#End Region
