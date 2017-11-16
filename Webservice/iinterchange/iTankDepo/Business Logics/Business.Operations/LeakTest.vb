Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

<ServiceContract()> _
Public Class LeakTest

#Region "pub_GetLeakTest() TABLE NAME:LEAK_TEST_DETAIL"
    <OperationContract()> _
    Public Function pub_GetLeakTestByActiveBit(ByVal bv_intDepotID As Integer) As LeakTestDataSet
        Try
            Dim dsLeakTestData As LeakTestDataSet
            Dim objLeakTests As New LeakTests
            dsLeakTestData = objLeakTests.GetLeakTestByActiveBit(bv_intDepotID)
            Return dsLeakTestData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetLeakTestByEqpmntNoByDepotID() TABLE NAME:LEAK_TEST_DETAIL"
    <OperationContract()> _
    Public Function GetLeakTestByEqpmntNoByDepotID(ByVal bv_strEquipmentNo As String, ByVal bv_strGateinTransaction As String, ByVal bv_intDepotID As Integer) As LeakTestDataSet
        Try
            Dim dsLeakTestData As LeakTestDataSet
            Dim objLeakTests As New LeakTests
            dsLeakTestData = objLeakTests.GetLeakTestByEqpmntNoByDepotID(bv_strEquipmentNo, bv_strGateinTransaction, bv_intDepotID)
            If dsLeakTestData.Tables(LeakTestData._LEAK_TEST).Rows.Count = 0 Then
                dsLeakTestData = objLeakTests.GetLeakTestByGateTrnsxn(bv_intDepotID, bv_strEquipmentNo, bv_strGateinTransaction)
            End If
            Return dsLeakTestData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetLeakTestByEqpmntNo() TABLE NAME:LEAK_TEST_DETAIL"
    <OperationContract()> _
    Public Function pub_GetLeakTestRevisionHistory(ByVal bv_strEquipmentNo As String, ByVal bv_strGateinTransaction As String, ByVal bv_intDepotID As Integer) As LeakTestDataSet
        Try
            Dim dsLeakTestData As LeakTestDataSet
            Dim objLeakTests As New LeakTests
            dsLeakTestData = objLeakTests.GetLeakTestRevisionHistory(bv_strEquipmentNo, bv_strGateinTransaction, bv_intDepotID)
            Return dsLeakTestData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_ValidateEquipmentNoByDepotID()  TABLE NAME:LEAK_TEST_DETAIL"
    <OperationContract()> _
    Public Function pub_ValidateEquipmentNoByDepotID(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32) As Boolean

        Try
            Dim objLeakTests As New LeakTests
            Dim intRowCount As Integer
            intRowCount = CInt(objLeakTests.GetEquipmentInformationByID(bv_strEquipmentNo, bv_intDepotID))
            If intRowCount > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateGenerationDetails"
    <OperationContract()> _
    Public Function pub_UpdateGenerationDetails(ByVal br_dsLeakTest As LeakTestDataSet, _
                                                ByVal bv_intDepotID As Integer, _
                                                ByVal bv_strUserName As String, _
                                                ByVal bv_datModifiedDate As DateTime) As Boolean
        Dim objTrans As New Transactions
        Dim dtLeakTest As New DataTable
        'Dim strLatestReportNo As String = String.Empty
        Dim objLeakTests As New LeakTests
        Dim objIndexPattern As New IndexPatterns
        Try

            For Each drLeakTest As DataRow In br_dsLeakTest.Tables(LeakTestData._V_LEAK_TEST).Select(String.Concat(LeakTestData.CHECKED, "= ", "True"))
                Dim strLatestReportNo As String = String.Empty
                strLatestReportNo = objIndexPattern.GetMaxReferenceNo(String.Concat(LeakTestData._LEAK_TEST_NO, ",", "Leak Test Detail"), Now, objTrans, Nothing, bv_intDepotID)
                objLeakTests.UpdateNoofTimesGeneration(CommonUIs.iLng(drLeakTest.Item(LeakTestData.LK_TST_ID)), _
                                                       CommonUIs.iInt(drLeakTest.Item(LeakTestData.NO_OF_TMS_GNRTD)) + 1, _
                                                        strLatestReportNo, _
                                                        bv_datModifiedDate, _
                                                        bv_strUserName, _
                                                        bv_intDepotID, _
                                                        objTrans)
                drLeakTest.Item(LeakTestData.LTST_RPRT_NO) = strLatestReportNo
            Next
            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function
#End Region

#Region "pub_CreateLeakTest Table Name: Leak_Test"
    <OperationContract()> _
    Public Function pub_CreateLeakTest(ByRef br_dsLeakTest As LeakTestDataSet, _
                                             ByVal bv_intDepotID As Integer, _
                                             ByVal bv_strUserName As String, _
                                             ByVal bv_datModifiedDate As DateTime) As Boolean
        Dim objTrans As New Transactions
        Dim lngLeakTestId As Long
        Dim dtLeakTest As New DataTable
        Dim objLeakTest As New LeakTests
        Dim objCommonUIs As New CommonUIs
        Try
            For Each drLeakTest As DataRow In br_dsLeakTest.Tables(LeakTestData._LEAK_TEST).Select(String.Concat(LeakTestData.CHECKED, "= ", "True"))
                Dim strGateinTransaction As String = String.Empty
                Dim intNoOfTimesGenerated As Int32 = 0
                Dim intLastRevisionNo As Int32 = 0
                Dim datTST_DT As DateTime = Nothing
                Dim intCustomerID As Integer = 0
                Dim dtActivityStatus As New DataTable
                Dim strYardLoc As String = Nothing
                dtActivityStatus = objLeakTest.GetLeakTestGITransactionByEqpmntNo(drLeakTest.Item(LeakTestData.EQPMNT_NO).ToString, bv_intDepotID, objTrans)
                If dtActivityStatus.Rows.Count > 0 Then
                    strGateinTransaction = dtActivityStatus.Rows(0).Item(LeakTestData.GI_TRNSCTN_NO).ToString()
                    intCustomerID = CInt(dtActivityStatus.Rows(0).Item(LeakTestData.CSTMR_ID))
                    If Not IsDBNull(dtActivityStatus.Rows(0).Item(LeakTestData.YRD_LCTN)) Then
                        strYardLoc = CStr(dtActivityStatus.Rows(0).Item(LeakTestData.YRD_LCTN))
                    End If

                End If
                dtLeakTest.Rows.Clear()
                dtLeakTest = objLeakTest.GetLeakTestByEqpmntNo(drLeakTest.Item(LeakTestData.EQPMNT_NO).ToString, drLeakTest.Item(LeakTestData.GI_TRNSCTN_NO).ToString, bv_intDepotID, objTrans).Tables(LeakTestData._LEAK_TEST)
                If dtLeakTest.Rows.Count > 0 Then
                    intLastRevisionNo = CommonUIs.iInt(dtLeakTest.Rows(0).Item(LeakTestData.RVSN_NO))
                    intNoOfTimesGenerated = CommonUIs.iInt(dtLeakTest.Rows(0).Item(LeakTestData.NO_OF_TMS_GNRTD))
                End If
                'Previous Activity Revision Make 0
                objLeakTest.UpdateActiveBit(drLeakTest.Item(LeakTestData.EQPMNT_NO).ToString, drLeakTest.Item(LeakTestData.GI_TRNSCTN_NO).ToString, bv_intDepotID, objTrans)
                Dim dsTemp As LeakTestDataSet
                dsTemp = objLeakTest.GetLeakTestByLeakTestID(CommonUIs.iLng(drLeakTest.Item(LeakTestData.LK_TST_ID)), objTrans)
                If dsTemp.Tables(LeakTestData._LEAK_TEST).Rows.Count = 0 Then
                    intLastRevisionNo = 0
                Else
                    intLastRevisionNo = intLastRevisionNo + 1
                End If
                datTST_DT = CDate(drLeakTest.Item(LeakTestData.TST_DT))
               

                lngLeakTestId = objLeakTest.CreateLeakTest(drLeakTest.Item(LeakTestData.EQPMNT_NO).ToString, _
                                                                              strGateinTransaction, _
                                                                              Nothing, _
                                                                              datTST_DT, _
                                                                              drLeakTest.Item(LeakTestData.RLF_VLV_SRL_1).ToString, _
                                                                              drLeakTest.Item(LeakTestData.RLF_VLV_SRL_2).ToString, _
                                                                              drLeakTest.Item(LeakTestData.PG_1).ToString, _
                                                                              drLeakTest.Item(LeakTestData.PG_2).ToString, _
                                                                              intLastRevisionNo, _
                                                                              bv_strUserName, _
                                                                              CommonUIs.iDat(Now), _
                                                                              "", _
                                                                              intNoOfTimesGenerated, _
                                                                              CommonUIs.iBool(drLeakTest.Item(LeakTestData.SHLL_TST_BT)), _
                                                                              CommonUIs.iBool(drLeakTest.Item(LeakTestData.STM_TB_TST_BT)), _
                                                                              drLeakTest.Item(LeakTestData.RMRKS_VC).ToString, _
                                                                              True, _
                                                                              bv_strUserName, _
                                                                              bv_datModifiedDate, _
                                                                              bv_strUserName, _
                                                                              bv_datModifiedDate, _
                                                                              bv_intDepotID, _
                                                                              objTrans)

                If drLeakTest.RowState = DataRowState.Added Then
                    'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                    Dim strEquipmentInfoRemarks As String = String.Empty
                    strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(drLeakTest.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                   bv_intDepotID, _
                                                                                   objTrans)
                    objCommonUIs.CreateTracking(lngLeakTestId, _
                                                intCustomerID, _
                                                drLeakTest.Item(LeakTestData.EQPMNT_NO).ToString, _
                                                "Leak Test", _
                                                0, _
                                                CStr(lngLeakTestId), _
                                                datTST_DT, _
                                                drLeakTest.Item(LeakTestData.RMRKS_VC).ToString, _
                                                strYardLoc, _
                                                strGateinTransaction, _
                                                Nothing, _
                                                Nothing, _
                                                bv_strUserName, _
                                                bv_datModifiedDate, _
                                                bv_strUserName, _
                                                bv_datModifiedDate, _
                                                Nothing, _
                                                Nothing, _
                                                Nothing, _
                                                bv_intDepotID, _
                                                0, _
                                                Nothing, _
                                                strEquipmentInfoRemarks, _
                                                False, _
                                                objTrans)
                ElseIf drLeakTest.RowState = DataRowState.Modified Then
                    'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
                    Dim strEquipmentInfoRemarks As String = String.Empty
                    strEquipmentInfoRemarks = objCommonUIs.GetEquipmentInformation(drLeakTest.Item(CleaningInspectionData.EQPMNT_NO).ToString, _
                                                                                   bv_intDepotID, _
                                                                                   objTrans)
                    objCommonUIs.UpdateTracking_Date_Remarks(drLeakTest.Item(HeatingData.EQPMNT_NO).ToString, _
                                                      datTST_DT, _
                                                      "Leak Test", _
                                                      strGateinTransaction, _
                                                      Nothing, _
                                                      drLeakTest.Item(LeakTestData.RMRKS_VC).ToString, _
                                                      bv_intDepotID, _
                                                      bv_strUserName, _
                                                      bv_datModifiedDate, _
                                                      strEquipmentInfoRemarks, _
                                                      objTrans)
                End If
            Next
            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function
#End Region

#Region "pub_GetCustomerDetail() TABLE NAME:CUSTOMER"
    <OperationContract()> _
    Public Function pub_GetCustomerDetail(ByVal bv_intDepotId As Integer, ByVal cstmr_id As Integer) As LeakTestDataSet
        Try
            Dim dsLeakTestDataSet As LeakTestDataSet
            Dim objLeakTests As New LeakTests
            dsLeakTestDataSet = objLeakTests.GetCustomerDetail(bv_intDepotId, cstmr_id)
            Return dsLeakTestDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetLeakTestDetail() TABLE NAME:V_LEAK_TEST"
    <OperationContract()> _
    Public Function pub_GetLeakTestDetail(ByVal bv_intCustomer_id As Integer, ByVal bv_datInDateFrom As String, _
                                           ByVal bv_datInDateTo As String, ByVal bv_datTestDateFrom As String, _
                                           ByVal bv_datTestDateTo As String, ByVal bv_strEquipemntNo As String, _
                                           ByVal bv_intDepot_id As Integer) As LeakTestDataSet
        Try
            Dim dsLeakTestDataSet As LeakTestDataSet
            Dim objLeakTests As New LeakTests
            dsLeakTestDataSet = objLeakTests.GetLeakTestDetail(bv_intCustomer_id, bv_datInDateFrom, _
                                                               bv_datInDateTo, bv_datTestDateFrom, bv_datTestDateTo, bv_strEquipemntNo, bv_intDepot_id)
            Return dsLeakTestDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region




End Class