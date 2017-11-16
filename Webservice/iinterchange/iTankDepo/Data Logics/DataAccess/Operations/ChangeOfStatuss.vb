Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

Public Class ChangeOfStatuss

#Region "Declarations"
    Dim objData As DataObjects
    Dim V_ActivityStatusSelectQueryByActivityName As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,CHMCL_NM,PRDCT_DSCRPTN_VC,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT, RMRKS_VC,GI_TRNSCTN_NO,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,GI_RF_NO,FILTER_CODE,CLNNG_ID,ADDTNL_CLNNG_BT FROM V_ACTIVITY_STATUS WHERE ACTV_BT = 1 AND "
    Dim WF_CHANGE_OF_STATUSSelectQueryByEquipmentStatusCode As String = "SELECT WF_CS_ID,CURRENT_STATUS,TO_STATUS,EQUIPMENT_TYPE,EQUIPMENT_CODE,DPT_ID,FILTER_COLUMN FROM WF_CHANGE_OF_STATUS WHERE CURRENT_STATUS IN (SELECT EQPMNT_STTS_CD FROM EQUIPMENT_STATUS WHERE EQPMNT_STTS_ID=@EQPMNT_STTS_ID)"
    Dim V_Activity_StatusSelectQueryByEquipmentNo As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,CHMCL_NM,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT, RMRKS_VC,GI_TRNSCTN_NO,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,GI_RF_NO,CLNNG_ID,ADDTNL_CLNNG_BT FROM V_ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID ORDER BY ACTVTY_STTS_ID DESC"
    Dim Activity_StatusSelectQueryByEquipmentNo As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,EQPMNT_NO,EQPMNT_CD_ID,GTN_DT,GTOT_DT,PRDCT_ID,EQPMNT_STTS_ID,(SELECT EQPMNT_STTS_ID FROM EQUIPMENT_STATUS WHERE EQPMNT_STTS_ID=ACTIVITY_STATUS.EQPMNT_STTS_ID) AS EQPMNT_STTS_CD ,ACTVTY_DT, RMRKS_VC,GI_TRNSCTN_NO,ACTV_BT,DPT_ID,YRD_LCTN FROM ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID ORDER BY ACTVTY_STTS_ID DESC"
    'Private Const ActivityStatusDetailsSelectQuery As String = "SELECT CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const ActivityStatusDetailsSelectQuery As String = "SELECT CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Cleaning_AdditionalCleaning_UpdateQuery As String = "UPDATE CLEANING SET ADDTNL_CLNNG_BT= @ADDTNL_CLNNG_BT,RMRKS_VC=@RMRKS_VC,EQPMNT_STTS_ID=@EQPMNT_STTS_ID,ORGNL_CLNNG_DT=@ORGNL_CLNNG_DT,LST_CLNNG_DT=@LST_CLNNG_DT,ORGNL_INSPCTD_DT=@ORGNL_INSPCTD_DT,LST_INSPCTD_DT=@LST_INSPCTD_DT WHERE CLNNG_ID=@CLNNG_ID AND DPT_ID=@DPT_ID"
    Private Const SelectEquipmentStatusByCode As String = "SELECT TOP 1 EQPMNT_STTS_ID FROM EQUIPMENT_STATUS WHERE EQPMNT_STTS_CD=@EQPMNT_STTS_CD AND DPT_ID=@DPT_ID"
    Private Const CheckCleaningCombination_SelectQuery As String = "SELECT COUNT(CLNNG_ID) FROM CLEANING WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private ds As ChangeOfStatusDataSet
#End Region

#Region "Constructor.."
    Sub New()
        ds = New ChangeOfStatusDataSet
    End Sub
#End Region

#Region "pub_ActivityStatusByActivityName"
    Public Function pub_ActivityStatusByActivityName(ByVal bv_strEquipmentStatusID As String, _
                                                     ByVal bv_strEquipmentNo As String, _
                                                     ByVal bv_strCustomerID As String, _
                                                     ByVal bv_strDepotID As Integer, _
                                                     ByVal bv_strFilterColumn As String) As ChangeOfStatusDataSet
        Try

            Dim strWhere As String = String.Empty
            If Not IsNothing(bv_strDepotID) Then
                strWhere = String.Concat(strWhere, ChangeOfStatusData.DPT_ID, " IN (", bv_strDepotID, ") ")
            End If
            If bv_strEquipmentStatusID <> "" Then
                strWhere = String.Concat(strWhere, " AND ", ChangeOfStatusData.EQPMNT_STTS_ID, " IN (", bv_strEquipmentStatusID, ")")
            End If
            If bv_strEquipmentNo <> "" Then
                strWhere = String.Concat(strWhere, " AND ", ChangeOfStatusData.EQPMNT_NO, " IN ('", bv_strEquipmentNo, "')")
            End If
            If bv_strCustomerID <> "" Then
                strWhere = String.Concat(strWhere, " AND ", ChangeOfStatusData.CSTMR_ID, " IN (", bv_strCustomerID, ") ")
            End If
            If bv_strFilterColumn <> "" Then
                strWhere = String.Concat(strWhere, " AND ", bv_strFilterColumn)
            End If

            objData = New DataObjects(String.Concat(V_ActivityStatusSelectQueryByActivityName, strWhere, " ORDER BY GTN_DT DESC"))
            objData.Fill(CType(ds, DataSet), ChangeOfStatusData._V_ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Get_WF_CHANGE_OF_STATUS"
    Public Function Get_WF_CHANGE_OF_STATUS(ByVal bv_strEquipmentStatusID As String, _
                                                     ByVal bv_strDepotID As Integer) As ChangeOfStatusDataSet
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(ChangeOfStatusData.EQPMNT_STTS_ID, bv_strEquipmentStatusID)
            hshTable.Add(ChangeOfStatusData.DPT_ID, bv_strDepotID)
            objData = New DataObjects(WF_CHANGE_OF_STATUSSelectQueryByEquipmentStatusCode, hshTable)
            objData.Fill(CType(ds, DataSet), ChangeOfStatusData._WF_CHANGE_OF_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Get_Activity_Status"
    Public Function Get_Activity_Status(ByVal bv_strEquipmentNo As String, _
                                        ByVal bv_strDepotID As Integer) As ChangeOfStatusDataSet
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(ChangeOfStatusData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(ChangeOfStatusData.DPT_ID, bv_strDepotID)
            objData = New DataObjects(V_Activity_StatusSelectQueryByEquipmentNo, hshTable)
            objData.Fill(CType(ds, DataSet), ChangeOfStatusData._V_ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Get_Activity_Status"
    Public Function Get_ActivityStatus(ByVal bv_strEquipmentNo As String, _
                                        ByVal bv_strDepotID As Integer) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dtActivitiStatus As New DataTable
            hshTable.Add(ChangeOfStatusData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(ChangeOfStatusData.DPT_ID, bv_strDepotID)
            objData = New DataObjects(Activity_StatusSelectQueryByEquipmentNo, hshTable)
            objData.Fill(dtActivitiStatus)
            Return dtActivitiStatus
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningRate"
    Public Function GetCleaningRate(ByVal bv_strEquipmentNo As String, _
                                    ByVal bv_intDepotID As Int32, _
                                    ByVal bv_strGITransactionNo As String, _
                                    ByRef br_objTransaction As Transactions) As DataTable
        Try
            Dim hshParameters As New Hashtable()
            Dim dsTemp As New DataSet
            Dim decCleaningRate As Decimal = 0
            hshParameters.Add(ChangeOfStatusData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(ChangeOfStatusData.DPT_ID, bv_intDepotID)
            hshParameters.Add(ChangeOfStatusData.GI_TRNSCTN_NO, bv_strGITransactionNo)
            objData = New DataObjects(ActivityStatusDetailsSelectQuery, hshParameters)
            objData.Fill(CType(dsTemp, DataSet), ChangeOfStatusData._V_ACTIVITY_STATUS, br_objTransaction)
            'If dsTemp.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Rows.Count > 0 Then
            '    decCleaningRate = CDec(dsTemp.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS).Rows(0).Item(ChangeOfStatusData.CLNNG_RT))
            'End If
            Return dsTemp.Tables(ChangeOfStatusData._V_ACTIVITY_STATUS)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCleaning"
    Public Function UpdateCleaning(ByVal bv_i64CleaningId As Int64, _
                                   ByVal bv_intDepotID As Int32, _
                                   ByVal bv_datCleaningDate As Date, _
                                   ByVal bv_datLastInspectionDate As Date, _
                                   ByVal bv_i64EquipmentStatusId As Int64, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_blnAdditionalCleaning As Boolean, _
                                   ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ChangeOfStatusData._CLEANING).NewRow()
            With dr
                .Item(ChangeOfStatusData.CLNNG_ID) = bv_i64CleaningId
                .Item(ChangeOfStatusData.RMRKS_VC) = bv_strRemarks
                .Item(ChangeOfStatusData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                .Item(ChangeOfStatusData.ADDTNL_CLNNG_BT) = bv_blnAdditionalCleaning
                .Item(ChangeOfStatusData.LST_CLNNG_DT) = bv_datLastInspectionDate
                .Item(ChangeOfStatusData.LST_INSPCTD_DT) = bv_datLastInspectionDate
                .Item(ChangeOfStatusData.ORGNL_CLNNG_DT) = bv_datCleaningDate
                .Item(ChangeOfStatusData.ORGNL_INSPCTD_DT) = bv_datLastInspectionDate
                .Item(ChangeOfStatusData.DPT_ID) = bv_intDepotID
            End With
            UpdateCleaning = objData.UpdateRow(dr, Cleaning_AdditionalCleaning_UpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

    Public Function pub_GetNewEquipStatus(ByVal equipStatus As String, ByVal bv_strDepotID As Integer)
        Dim hshTable As New Hashtable()
        hshTable.Add(ChangeOfStatusData.EQPMNT_STTS_CD, equipStatus)
        hshTable.Add(ChangeOfStatusData.DPT_ID, bv_strDepotID)
        objData = New DataObjects(SelectEquipmentStatusByCode, hshTable)
        Return objData.ExecuteScalar()
    End Function

    Public Function CheckCleaningCombination(ByVal bv_strEQPMNT_NO As String, ByVal bv_strGI_TRNSCTN_NO As String, _
                                          ByVal intCSTMR_ID As Int64, ByVal intDPT_ID As Int64, ByRef br_objTrans As Transactions) As Int32
        Try

            Dim hshTable As New Hashtable()
            hshTable.Add(CleaningData.EQPMNT_NO, bv_strEQPMNT_NO)
            hshTable.Add(CleaningData.GI_TRNSCTN_NO, bv_strGI_TRNSCTN_NO)
            hshTable.Add(CleaningData.CSTMR_ID, intCSTMR_ID)
            hshTable.Add(CleaningData.DPT_ID, intDPT_ID)


            objData = New DataObjects(CheckCleaningCombination_SelectQuery, hshTable)
            Return objData.ExecuteScalar(br_objTrans)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
