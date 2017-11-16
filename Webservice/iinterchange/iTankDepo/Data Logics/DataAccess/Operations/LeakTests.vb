Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

#Region "LeakTests"

Public Class LeakTests

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const Leak_TestSelectQueryPk As String = "SELECT LK_TST_ID,EQPMNT_NO,GI_TRNSCTN_NO,ESTMT_NO,TST_DT,RLF_VLV_SRL_1,RLF_VLV_SRL_2,PG_1,PG_2,RVSN_NO,LST_GNRTD_BY,LST_GNRTD_DT,LTST_RPRT_NO,NO_OF_TMS_GNRTD,SHLL_TST_BT,STM_TB_TST_BT,RMRKS_VC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT FROM LEAK_TEST WHERE LK_TST_ID=@LK_TST_ID"
    Private Const Leak_TestSelectQueryByActiveBit As String = "SELECT LK_TST_ID,EQPMNT_NO,GI_TRNSCTN_NO,ESTMT_NO,EQPMNT_TYP_CD,GTN_DT,CSTMR_CD,CSTMR_ID,EQPMNT_STTS_CD,TST_DT,RLF_VLV_SRL_1,RLF_VLV_SRL_2,PG_1,PG_2,RVSN_NO,LST_GNRTD_BY,LST_GNRTD_DT,LTST_RPRT_NO,NO_OF_TMS_GNRTD,SHLL_TST_BT,STM_TB_TST_BT,RMRKS_VC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,YRD_LCTN,DPT_ID FROM V_LEAK_TEST WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 AND GI_TRNSCTN_NO IN (SELECT GI_TRNSCTN_NO FROM ACTIVITY_STATUS WHERE ACTV_BT = 1)"
    Private Const Leak_TestSelectQueryByEqpmntNo As String = "SELECT LK_TST_ID,EQPMNT_NO,GI_TRNSCTN_NO,ESTMT_NO,EQPMNT_TYP_CD,GTN_DT,CSTMR_CD,EQPMNT_STTS_CD,TST_DT,RLF_VLV_SRL_1,RLF_VLV_SRL_2,PG_1,PG_2,RVSN_NO,LST_GNRTD_BY,LST_GNRTD_DT,LTST_RPRT_NO,NO_OF_TMS_GNRTD,SHLL_TST_BT,STM_TB_TST_BT,RMRKS_VC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT FROM V_LEAK_TEST WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND ACTV_BT=1 AND GI_TRNSCTN_NO IN (SELECT GI_TRNSCTN_NO FROM ACTIVITY_STATUS WHERE ACTV_BT = 1)"
    Private Const Leak_TestInsertQuery As String = "INSERT INTO LEAK_TEST(LK_TST_ID,EQPMNT_NO,GI_TRNSCTN_NO,ESTMT_NO,TST_DT,RLF_VLV_SRL_1,RLF_VLV_SRL_2,PG_1,PG_2,RVSN_NO,NO_OF_TMS_GNRTD,SHLL_TST_BT,STM_TB_TST_BT,RMRKS_VC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID)VALUES(@LK_TST_ID,@EQPMNT_NO,@GI_TRNSCTN_NO,@ESTMT_NO,@TST_DT,@RLF_VLV_SRL_1,@RLF_VLV_SRL_2,@PG_1,@PG_2,@RVSN_NO,@NO_OF_TMS_GNRTD,@SHLL_TST_BT,@STM_TB_TST_BT,@RMRKS_VC,@ACTV_BT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@DPT_ID)"
    Private Const Leak_TestUpdateQuery As String = "UPDATE LEAK_TEST SET LK_TST_ID=@LK_TST_ID,LTST_RPRT_NO=@LTST_RPRT_NO, EQPMNT_NO=@EQPMNT_NO, GI_TRNSCTN_NO=@GI_TRNSCTN_NO, ESTMT_NO=@ESTMT_NO, TST_DT=@TST_DT, RLF_VLV_SRL_1=@RLF_VLV_SRL_1, RLF_VLV_SRL_2=@RLF_VLV_SRL_2, PG_1=@PG_1, PG_2=@PG_2, RVSN_NO=@RVSN_NO,NO_OF_TMS_GNRTD=@NO_OF_TMS_GNRTD, SHLL_TST_BT=@SHLL_TST_BT, STM_TB_TST_BT=@STM_TB_TST_BT, RMRKS_VC=@RMRKS_VC, ACTV_BT=@ACTV_BT, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE LK_TST_ID=@LK_TST_ID"
    Private Const Leak_TestSelectByEqpmntNo As String = "SELECT LK_TST_ID,TST_DT,EQPMNT_NO,EQPMNT_TYP_CD,CSTMR_CD,EQPMNT_STTS_CD,GTN_DT,RLF_VLV_SRL_1,RLF_VLV_SRL_2,GI_TRNSCTN_NO,PG_1,PG_2,SHLL_TST_BT,STM_TB_TST_BT,RVSN_NO,LST_GNRTD_BY,LST_GNRTD_DT,LTST_RPRT_NO,NO_OF_TMS_GNRTD,RMRKS_VC, CASE WHEN SHLL_TST_BT = 1 THEN 'Y' ELSE 'N' END SHLL_TST,CASE WHEN STM_TB_TST_BT=1 THEN 'Y' ELSE 'N' END STM_TB_TST FROM V_LEAK_TEST  WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID  ORDER BY RVSN_NO DESC"
    Private Const Leak_Test_GITransactionQuery As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,GTN_DT,GTOT_DT,PRDCT_ID,CLNNG_DT,INSPCTN_DT,EQPMNT_STTS_ID,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,ACTV_BT,DPT_ID,YRD_LCTN FROM ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND ACTV_BT = 1 AND DPT_ID=@DPT_ID"
    Private Const LeakTestSelectQueryByDepotId As String = "SELECT LK_TST_ID FROM LEAK_TEST WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const Leak_Test_RevNo_SelectQuery As String = "SELECT LK_TST_ID,EQPMNT_NO,GI_TRNSCTN_NO,RVSN_NO,NO_OF_TMS_GNRTD FROM LEAK_TEST WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO ORDER BY LK_TST_ID DESC"
    Private Const Leak_Test_ActiveBit As String = "UPDATE LEAK_TEST SET ACTV_BT=0 WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND ACTV_BT = 1"
    Private Const Leak_Test_Update_GeneratedQuery As String = "UPDATE LEAK_TEST SET NO_OF_TMS_GNRTD=@NO_OF_TMS_GNRTD,LTST_RPRT_NO=@LTST_RPRT_NO,LST_GNRTD_BY=@LST_GNRTD_BY,LST_GNRTD_DT=@LST_GNRTD_DT WHERE DPT_ID=@DPT_ID AND LK_TST_ID=@LK_TST_ID"
    Private Const Customer_SelectQuery As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM,CNTCT_ADDRSS,CNTCT_PRSN_NAM FROM CUSTOMER WHERE ACTV_BT = 1 AND DPT_ID=@DPT_ID AND CSTMR_ID=@CSTMR_ID"
    Private Const TrackingUpdateQuery As String = "UPDATE TRACKING SET ACTVTY_DT=@ACTVTY_DT, ACTVTY_RMRKS=@ACTVTY_RMRKS WHERE ACTVTY_NAM=@ACTVTY_NAM AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND EQPMNT_NO=@EQPMNT_NO"
    Private Const Leak_Test_Activity_Status_SelectQuery As String = "SELECT ACTVTY_STTS_ID,ACTVTY_STTS_ID AS LK_TST_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,REPLACE(CONVERT(VARCHAR(11),GTN_DT,106),' ','-') AS GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE FROM V_ACTIVITY_STATUS WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Leak_TestSelectRepairEstimate As String = "SELECT LK_TST_ID,TST_DT,EQPMNT_NO,EQPMNT_TYP_CD,CSTMR_CD,EQPMNT_STTS_CD,REPLACE(CONVERT(VARCHAR(11),GTN_DT,106),' ','-') AS GTN_DT,RLF_VLV_SRL_1,RLF_VLV_SRL_2,GI_TRNSCTN_NO,PG_1,PG_2,SHLL_TST_BT,STM_TB_TST_BT,RVSN_NO,LST_GNRTD_BY,LST_GNRTD_DT,LTST_RPRT_NO,NO_OF_TMS_GNRTD,RMRKS_VC,YRD_LCTN FROM V_LEAK_TEST WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID AND ACTV_BT=1 ORDER BY RVSN_NO DESC"

    'Private Const Leak_TestDetailSelectQuery As String = "SELECT LK_TST_ID,LT_TST_DT,EQPMNT_NO,GI_TRNSCTN_NO,ESTMT_NO,EQPMNT_TYP_CD,GTN_DT,CSTMR_CD,CSTMR_ID,EQPMNT_STTS_CD,TST_DT,RLF_VLV_SRL_1,RLF_VLV_SRL_2,PG_1,PG_2,RVSN_NO,LST_GNRTD_BY,LST_GNRTD_DT,LTST_RPRT_NO,NO_OF_TMS_GNRTD,SHLL_TST_BT,STM_TB_TST_BT,RMRKS_VC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,YRD_LCTN,'FALSE' AS CHECKED FROM V_LEAK_TEST V"
    Private Const Leak_TestDetailSelectQuery As String = "SELECT LK_TST_ID,LT_TST_DT,EQPMNT_NO,GI_TRNSCTN_NO,ESTMT_NO,EQPMNT_TYP_CD,GTN_DT,CSTMR_CD,CSTMR_ID,EQPMNT_STTS_CD,TST_DT,RLF_VLV_SRL_1,RLF_VLV_SRL_2,PG_1,PG_2,RVSN_NO,LST_GNRTD_BY,LST_GNRTD_DT,LTST_RPRT_NO,NO_OF_TMS_GNRTD,SHLL_TST_BT,STM_TB_TST_BT,RMRKS_VC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,YRD_LCTN,'FALSE' AS CHECKED FROM V_LEAK_TEST V where LK_TST_ID =(select MAX(LK_TST_ID) from LEAK_TEST where GI_TRNSCTN_NO = V.GI_TRNSCTN_NO)"
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"

    Private ds As LeakTestDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New LeakTestDataSet
    End Sub

#End Region

#Region "GetLeakTestByLeakTestID() TABLE NAME:LEAK_TEST"

    Public Function GetLeakTestByLeakTestID(ByVal bv_i64LeakTestID As Int64, ByRef br_objTrans As Transactions) As LeakTestDataSet
        Try
            objData = New DataObjects(Leak_TestSelectQueryPk, LeakTestData.LK_TST_ID, CStr(bv_i64LeakTestID))
            objData.Fill(CType(ds, DataSet), LeakTestData._LEAK_TEST, br_objTrans)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetLeakTestByEqpmntNo() TABLE NAME:Leak_Test"

    Public Function GetLeakTestByEqpmntNo(ByVal bv_strEquipmentNo As String, _
                                          ByVal bv_strGateinTransaction As String, _
                                          ByVal bv_intDepotID As Integer, _
                                          ByRef br_objTrans As Transactions) As LeakTestDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(LeakTestData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(LeakTestData.GI_TRNSCTN_NO, bv_strGateinTransaction)
            hshparameters.Add(LeakTestData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(Leak_TestSelectByEqpmntNo, hshparameters)
            objData.Fill(CType(ds, DataSet), LeakTestData._LEAK_TEST, br_objTrans)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetLeakTestByEqpmntNoByDepotID() TABLE NAME:Leak_Test"

    Public Function GetLeakTestByEqpmntNoByDepotID(ByVal bv_strEquipmentNo As String, _
                                          ByVal bv_strGateinTransaction As String, _
                                          ByVal bv_intDepotID As Integer) As LeakTestDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(LeakTestData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(LeakTestData.GI_TRNSCTN_NO, bv_strGateinTransaction)
            hshparameters.Add(LeakTestData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(Leak_TestSelectRepairEstimate, hshparameters)
            objData.Fill(CType(ds, DataSet), LeakTestData._LEAK_TEST)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetLeakTestByEqpmntNo() TABLE NAME:Leak_Test"

    Public Function GetLeakTestRevisionHistory(ByVal bv_strEquipmentNo As String, _
                                          ByVal bv_strGateinTransaction As String, _
                                          ByVal bv_intDepotID As Integer) As LeakTestDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(LeakTestData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(LeakTestData.GI_TRNSCTN_NO, bv_strGateinTransaction)
            hshparameters.Add(LeakTestData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(Leak_TestSelectByEqpmntNo, hshparameters)
            objData.Fill(CType(ds, DataSet), LeakTestData._LEAK_TEST)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "GetLeakTestGITransactionByEqpmntNo() TABLE NAME:GATEIN"

    Public Function GetLeakTestGITransactionByEqpmntNo(ByVal bv_strEquipmentNo As String, _
                                                       ByVal bv_intDepotID As Integer, _
                                                       ByRef br_objTrans As Transactions) As DataTable
        Try
            Dim dtActivityStatus As New DataTable
            Dim hshparameters As New Hashtable
            hshparameters.Add(LeakTestData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(LeakTestData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(Leak_Test_GITransactionQuery, hshparameters)
            objData.Fill(dtActivityStatus, br_objTrans)
            Return dtActivityStatus
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetLeakTestByEqpmntNo() TABLE NAME:Leak_Test"

    Public Function GetLeakTestByEqpmntNo(ByVal bv_strEquipmentNo As String, _
                                            ByVal bv_strGateInTransaction As String, _
                                            ByVal bv_intDepotId As Int32) As DataTable
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(LeakTestData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(LeakTestData.GI_TRNSCTN_NO, bv_strGateInTransaction)
            hshparameters.Add(LeakTestData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(Leak_Test_RevNo_SelectQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), LeakTestData._LEAK_TEST)
            Return ds.Tables(LeakTestData._LEAK_TEST)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentInformationByID() TABLE NAME:LEAK_TEST"

    Public Function GetEquipmentInformationByID(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(LeakTestData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(LeakTestData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(LeakTestSelectQueryByDepotId, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateLeakTest() TABLE NAME:Leak_Test"

    Public Function CreateLeakTest(ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_strGateInTransactionNo As String, _
                                         ByVal bv_strEstmateNo As String, _
                                         ByVal bv_datTestDate As DateTime, _
                                         ByVal bv_strReliefValveSerial1 As String, _
                                         ByVal bv_strReliefValveSerial2 As String, _
                                         ByVal bv_strPG1 As String, _
                                         ByVal bv_strPG2 As String, _
                                         ByVal bv_i32RevisionNo As Int32, _
                                         ByVal bv_strLastGeneratedBy As String, _
                                         ByVal bv_datLastGeneratedDate As DateTime, _
                                         ByVal bv_strLastestReportNo As String, _
                                         ByVal bv_i32NumberofTimesGenerated As Int32, _
                                         ByVal bv_blnShellTestBit As Boolean, _
                                         ByVal bv_blnSteamTubeTestBit As Boolean, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_blnActiveBit As Boolean, _
                                         ByVal bv_strCreatedBy As String, _
                                         ByVal bv_datCreatedDate As DateTime, _
                                         ByVal bv_strModifiedBy As String, _
                                         ByVal bv_datModifiedDate As DateTime, _
                                         ByVal bv_intDepotId As Int32, _
                                         ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(LeakTestData._LEAK_TEST).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(LeakTestData._LEAK_TEST, br_objTrans)
                .Item(LeakTestData.LK_TST_ID) = intMax
                .Item(LeakTestData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(LeakTestData.DPT_ID) = bv_intDepotId
                .Item(LeakTestData.RVSN_NO) = bv_i32RevisionNo
                .Item(LeakTestData.NO_OF_TMS_GNRTD) = bv_i32NumberofTimesGenerated
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(LeakTestData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(LeakTestData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                If bv_strEstmateNo <> Nothing Then
                    .Item(LeakTestData.ESTMT_NO) = bv_strEstmateNo
                Else
                    .Item(LeakTestData.ESTMT_NO) = DBNull.Value
                End If
                .Item(LeakTestData.TST_DT) = bv_datTestDate
                If bv_strReliefValveSerial1 <> Nothing Then
                    .Item(LeakTestData.RLF_VLV_SRL_1) = bv_strReliefValveSerial1
                Else
                    .Item(LeakTestData.RLF_VLV_SRL_1) = DBNull.Value
                End If
                If bv_strReliefValveSerial2 <> Nothing Then
                    .Item(LeakTestData.RLF_VLV_SRL_2) = bv_strReliefValveSerial2
                Else
                    .Item(LeakTestData.RLF_VLV_SRL_2) = DBNull.Value
                End If
                If bv_strPG1 <> Nothing Then
                    .Item(LeakTestData.PG_1) = bv_strPG1
                Else
                    .Item(LeakTestData.PG_1) = DBNull.Value
                End If
                If bv_strPG2 <> Nothing Then
                    .Item(LeakTestData.PG_2) = bv_strPG2
                Else
                    .Item(LeakTestData.PG_2) = DBNull.Value
                End If
                If bv_strLastGeneratedBy <> Nothing Then
                    .Item(LeakTestData.LST_GNRTD_BY) = bv_strLastGeneratedBy
                Else
                    .Item(LeakTestData.LST_GNRTD_BY) = DBNull.Value
                End If
                If bv_datLastGeneratedDate <> Nothing Then
                    .Item(LeakTestData.LST_GNRTD_DT) = bv_datLastGeneratedDate
                Else
                    .Item(LeakTestData.LST_GNRTD_DT) = DBNull.Value
                End If

                .Item(LeakTestData.SHLL_TST_BT) = bv_blnShellTestBit
                .Item(LeakTestData.STM_TB_TST_BT) = bv_blnSteamTubeTestBit
             
                If bv_strRemarks <> Nothing Then
                    .Item(LeakTestData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(LeakTestData.RMRKS_VC) = DBNull.Value
                End If
                .Item(LeakTestData.ACTV_BT) = bv_blnActiveBit
                If bv_strCreatedBy <> Nothing Then
                    .Item(LeakTestData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(LeakTestData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(LeakTestData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(LeakTestData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(LeakTestData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(LeakTestData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(LeakTestData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(LeakTestData.MDFD_DT) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Leak_TestInsertQuery, br_objTrans)
            dr = Nothing
            CreateLeakTest = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetLeakTestByActiveBit() TABLE NAME:Leak_Test"

    Public Function GetLeakTestByActiveBit(ByVal bv_i32DepotID As Int32) As LeakTestDataSet
        Try
            objData = New DataObjects(Leak_TestSelectQueryByActiveBit, LeakTestData.DPT_ID, bv_i32DepotID)
            objData.Fill(CType(ds, DataSet), LeakTestData._LEAK_TEST)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetLeakTestByEqpmntNo() TABLE NAME:Leak_Test"

    Public Function GetLeakTestByGateTrnsxn(ByVal bv_i32DepotID As Int32, _
                                            ByVal bv_strEquipmentNo As String, _
                                            ByVal bv_strGateinTransacion As String) As LeakTestDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(LeakTestData.DPT_ID, bv_i32DepotID)
            hshparameters.Add(LeakTestData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(LeakTestData.GI_TRNSCTN_NO, bv_strGateinTransacion)
            objData = New DataObjects(Leak_Test_Activity_Status_SelectQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), LeakTestData._ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateActiveBit Table Name: LEAK_TEST"
    Public Function UpdateActiveBit(ByVal bv_strEquipmentNo As String, _
                                    ByVal bv_strGateInTransaction As String, _
                                    ByVal bv_intDepotId As Int32, _
                                    ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(LeakTestData._LEAK_TEST).NewRow()
            With dr
                dr.Item(LeakTestData.EQPMNT_NO) = bv_strEquipmentNo
                dr.Item(LeakTestData.GI_TRNSCTN_NO) = bv_strGateInTransaction
                dr.Item(LeakTestData.DPT_ID) = bv_intDepotId
            End With
            UpdateActiveBit = objData.UpdateRow(dr, Leak_Test_ActiveBit, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateNoofTimesGeneration Table Name: LEAK_TEST"
    Public Function UpdateNoofTimesGeneration(ByVal bv_i64LeakTestId As Int64, _
                                              ByVal bv_i32noOfTimes As Int32, _
                                              ByRef str_latestReportNo As String, _
                                              ByVal bv_LastGeneratedDate As DateTime, _
                                              ByVal bv_LastGeneratedBy As String, _
                                              ByVal bv_intDepotId As Int32, _
                                              ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            Dim intLeakTestNo As Int64 = 0
            objData = New DataObjects()
            dr = ds.Tables(LeakTestData._LEAK_TEST).NewRow()
            With dr
                intLeakTestNo = CommonUIs.GetIdentityValue(LeakTestData._LEAK_TEST_NO, br_objTransaction)
                ' str_latestReportNo = CommonUIs.GetIdentityCode(LeakTestData._LEAK_TEST, intLeakTestNo, Now, br_objTransaction)
                'Via Index Pattern
                '  str_latestReportNo = IndexPatterns.GetIdentityCode(LeakTestData._LEAK_TEST, intLeakTestNo, Now, bv_intDepotId, br_objTransaction)
                dr.Item(LeakTestData.LK_TST_ID) = bv_i64LeakTestId
                dr.Item(LeakTestData.NO_OF_TMS_GNRTD) = bv_i32noOfTimes
                dr.Item(LeakTestData.LTST_RPRT_NO) = str_latestReportNo
                dr.Item(LeakTestData.LST_GNRTD_BY) = bv_LastGeneratedBy
                dr.Item(LeakTestData.LST_GNRTD_DT) = bv_LastGeneratedDate
                dr.Item(LeakTestData.DPT_ID) = bv_intDepotId
            End With
            UpdateNoofTimesGeneration = objData.UpdateRow(dr, Leak_Test_Update_GeneratedQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetCustomerDetail  Table Name:CUSTOMER"
    Public Function GetCustomerDetail(ByVal bv_intDepotId As Integer, ByVal customer_id As Integer) As LeakTestDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(LeakTestData.DPT_ID, bv_intDepotId)
            hshparameters.Add(LeakTestData.CSTMR_ID, customer_id)
            objData = New DataObjects(Customer_SelectQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), LeakTestData._CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateTracking() TABLE NAME:Tracking"

    Public Function UpdateTracking(ByVal bv_strEQPMNT_NO As String, _
                ByVal bv_datActivity As DateTime, _
                ByVal bv_strActivityName As String, _
                ByVal strGI_TRNSCTN_NO As String, _
                ByVal bv_strRemarks As String, _
                ByVal bv_i32DepotId As Int32, _
                ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(HeatingData._TRACKING).NewRow()
            With dr
                .Item(LeakTestData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(LeakTestData.ACTVTY_DT) = bv_datActivity
                If strGI_TRNSCTN_NO <> Nothing Then
                    .Item(LeakTestData.GI_TRNSCTN_NO) = strGI_TRNSCTN_NO
                Else
                    .Item(LeakTestData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(LeakTestData.DPT_ID) = bv_i32DepotId
                .Item(LeakTestData.ACTVTY_NAM) = bv_strActivityName
                If bv_strRemarks <> Nothing Then
                    .Item(LeakTestData.ACTVTY_RMRKS) = bv_strRemarks
                Else
                    .Item(LeakTestData.ACTVTY_RMRKS) = DBNull.Value
                End If
            End With
            UpdateTracking = objData.UpdateRow(dr, TrackingUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetLeakTestDetail  Table Name:V_LEAK_TEST"
    Public Function GetLeakTestDetail(ByVal bv_intCustomer_id As Integer, ByVal bv_datInDateFrom As String, _
                                           ByVal bv_datInDateTo As String, ByVal bv_datTestDateFrom As String, _
                                           ByVal bv_datTestDateTo As String, ByVal bv_strEquipemntNo As String, ByVal bv_intDepot_id As Integer) As LeakTestDataSet
        Try
            Dim strQuery As String = Leak_TestDetailSelectQuery
            Dim intParamCount As Integer = 6
            Dim dtCurrentDate As Date = Now.Date
            If bv_intCustomer_id <> 0 Then
                strQuery = String.Concat(strQuery, " AND ", LeakTestData.CSTMR_ID, " = ", bv_intCustomer_id)
            End If
            If bv_intDepot_id <> 0 Then
                strQuery = String.Concat(strQuery, " AND ", LeakTestData.DPT_ID, " = ", bv_intDepot_id)
            End If
            If (Not bv_datInDateFrom Is Nothing And Not bv_datInDateTo Is Nothing) Then
                strQuery = String.Concat(strQuery, " AND ", LeakTestData.LT_GTN_DT, " >= '", bv_datInDateFrom, _
                                                                               "' AND ", LeakTestData.LT_GTN_DT, " <= '", bv_datInDateTo, "'")


            ElseIf (Not bv_datInDateFrom Is Nothing And bv_datInDateTo Is Nothing) Then
                'strQuery = String.Concat(strQuery, " AND ", LeakTestData.LT_GTN_DT, " >= '", bv_datInDateFrom, _
                '                                           "' AND ", LeakTestData.LT_GTN_DT, " <= '", dtCurrentDate, "'")
                strQuery = String.Concat(strQuery, " AND ", LeakTestData.LT_GTN_DT, " >= '", bv_datInDateFrom)



            ElseIf (bv_datInDateFrom Is Nothing And Not bv_datInDateTo Is Nothing) Then
                'strQuery = String.Concat(strQuery, " AND ", LeakTestData.LT_GTN_DT, " >= '", dtCurrentDate, _
                '                                                       "' AND ", LeakTestData.LT_GTN_DT, " <= '", dtCurrentDate, "'")
                strQuery = String.Concat(strQuery, " AND ", LeakTestData.LT_GTN_DT, " <= '", bv_datInDateTo)

            End If

            If (Not bv_datTestDateFrom Is Nothing And Not bv_datTestDateTo Is Nothing) Then
                strQuery = String.Concat(strQuery, " AND ", LeakTestData.TST_DT, " >= '", bv_datTestDateFrom, _
                                                                                  "' AND ", LeakTestData.TST_DT, " <= '", bv_datTestDateTo, "'")

            ElseIf (Not bv_datTestDateFrom Is Nothing And bv_datTestDateTo Is Nothing) Then
                'strQuery = String.Concat(strQuery, " AND ", LeakTestData.TST_DT, " >= '", bv_datTestDateFrom, _
                '                                                                  "' AND ", LeakTestData.TST_DT, " <= '", dtCurrentDate, "'")

                strQuery = String.Concat(strQuery, " AND ", LeakTestData.TST_DT, " >= '", bv_datTestDateFrom)


            ElseIf (bv_datTestDateFrom Is Nothing And Not bv_datTestDateTo Is Nothing) Then
                'strQuery = String.Concat(strQuery, " AND ", LeakTestData.TST_DT, " >= '", dtCurrentDate, _
                '                                                                  "' AND ", LeakTestData.TST_DT, " <= '", bv_datTestDateTo, "'")
                strQuery = String.Concat(strQuery, " AND ", LeakTestData.TST_DT, " <= '", bv_datTestDateTo)
            End If
            If Not bv_strEquipemntNo Is Nothing Then
                strQuery = String.Concat(strQuery, " AND ", LeakTestData.EQPMNT_NO, " = '", bv_strEquipemntNo, "'")

            End If


            objData = New DataObjects(strQuery)




            objData.Fill(CType(ds, DataSet), LeakTestData._V_LEAK_TEST)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class

#End Region
