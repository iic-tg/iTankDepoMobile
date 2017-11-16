#Region " EquipmentHistory.vb"
'*********************************************************************************************************************
'Name :
'      EquipmentInformation.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EquipmentHistory.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      16/Oct/13 11:57:36 a.m.
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
Imports System.Text

<ServiceContract()> _
Public Class EquipmentHistory

#Region "pub_GetEquipemntHistory"
    <OperationContract()> _
    Public Function pub_GetEquipmentHistory(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32) As EquipmentHistoryDataSet

        Try
            Dim dsEquipemntHistory As New EquipmentHistoryDataSet
            Dim objEquipemntHistory As New EquipmentHistorys
            dsEquipemntHistory = objEquipemntHistory.GetEquipemntHistory(bv_strEquipmentNo, bv_intDepotID)
            Return dsEquipemntHistory
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_DeleteEquipmentActivity"
    <OperationContract()> _
    Public Function pub_DeleteEquipmentActivity(ByRef br_dsEquipmentHistory As EquipmentHistoryDataSet, _
                                                ByVal bv_intTrackingID As Integer, _
                                                ByVal bv_strActivityName As String, _
                                                ByVal bv_strAuditRemarks As String, _
                                                ByVal bv_strCanceledBy As String, _
                                                ByVal bv_dtCancelDate As DateTime, _
                                                ByVal bv_intDepotID As Integer) As Boolean

        Dim objTrans As New Transactions
        Try
            Dim objEquipmentHistory As New EquipmentHistorys
            Dim dtEquipmentDelete As New DataTable
            Dim dtCleaningDetail As New DataTable

            Dim strEquipmentNo As String = String.Empty
            Dim strGI_TRNCTN_NO As String = String.Empty
            Dim intActivityID As Integer = 0
            Dim blnAdditionalFlag As Boolean = False
            Dim strFilter As String = String.Concat(EquipmentHistoryData.TRCKNG_ID, "=", bv_intTrackingID)

            Dim strNotFilter As String = String.Concat(EquipmentHistoryData.TRCKNG_ID, "<>", bv_intTrackingID)

            dtEquipmentDelete = objEquipmentHistory.GetEquipmentDelete(bv_strActivityName, objTrans).Tables(EquipmentHistoryData._EQUIPMENT_DELETE)

            For Each drActivity As DataRow In dtEquipmentDelete.Rows
                Dim sbrQuery As New StringBuilder
                Dim sbrWhereCondition As New StringBuilder
                'Get Where Condition
                For Each drEquipHistory As DataRow In br_dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Select(strFilter)
                    Dim strCondition As String = String.Empty
                    Dim strAliasCondition As String = String.Empty
                    Dim strSplitAliasCondition() As String = Nothing
                    Dim strSplitCondition() As String = Nothing


                    If CBool(drEquipHistory.Item(EquipmentHistoryData.ADDTNL_CLNNG_BT)) Then
                        blnAdditionalFlag = CBool(drEquipHistory.Item(EquipmentHistoryData.ADDTNL_CLNNG_BT))
                    End If

                    strEquipmentNo = CStr(drEquipHistory.Item(EquipmentHistoryData.EQPMNT_NO))

                    If Not IsDBNull(drActivity.Item(EquipmentHistoryData.ALIAS_CONDITION)) Then
                        strAliasCondition = (CStr(drActivity.Item(EquipmentHistoryData.ALIAS_CONDITION)))
                        strSplitAliasCondition = strAliasCondition.Split(CChar(","))
                    End If

                    strCondition = CStr(drActivity.Item(EquipmentHistoryData.CONDITION))
                    strSplitCondition = strCondition.Split(CChar(","))

                    For i = 0 To strSplitCondition.Length - 1
                        If sbrWhereCondition.Length > 0 Then
                            sbrWhereCondition.Append(" AND ")
                        End If
                        If strSplitAliasCondition Is Nothing Then
                            If strSplitCondition(i).Contains("=") Then
                                sbrWhereCondition.Append(Trim(strSplitCondition(i)))
                            Else
                                sbrWhereCondition.Append(String.Concat(Trim(strSplitCondition(i)), "='", drEquipHistory.Item(Trim(strSplitCondition(i))), "'"))
                            End If
                        Else
                            'CR: ADDITIONAL CLEANING (SPLIT THE ALIAS CONDITION)
                            If strSplitAliasCondition(i).Contains("=") Then
                                sbrWhereCondition.Append(Trim(strSplitAliasCondition(i)))
                            Else
                                sbrWhereCondition.Append(String.Concat(Trim(strSplitCondition(i)), "='", drEquipHistory.Item(Trim(strSplitAliasCondition(i))), "'"))
                            End If
                        End If
                    Next
                Next

                'Check Whether Update or Delete
                If CBool(drActivity.Item(EquipmentHistoryData.IS_DELETE)) Then
                    Dim strChildTableName() As String = CStr(drActivity.Item(EquipmentHistoryData.CHILD_TABLE)).Split(CChar(","))
                    For i = 0 To strChildTableName.Length - 1
                        If blnAdditionalFlag = False Then
                            objEquipmentHistory.DeleteEquipmentActivity(sbrWhereCondition.ToString(), Trim(strChildTableName(i)), objTrans)
                        End If
                    Next
                Else
                    Dim strFieldName() As String = CStr(drActivity.Item(EquipmentHistoryData.FIELD_NAME)).Split(CChar(","))
                    Dim strNotNullFilters() As String = Nothing


                    If Not IsDBNull(drActivity.Item(EquipmentHistoryData.NOT_NULL_FILTERS)) Then
                        strNotNullFilters = CStr(drActivity.Item(EquipmentHistoryData.NOT_NULL_FILTERS)).Split(CChar(","))
                    End If

                    For i = 0 To strFieldName.Length - 1
                        Dim blnNotNUll As Boolean = False
                        Dim strColumnName As String = String.Empty

                        If Trim(strFieldName(i)).Contains("=") Then
                            If sbrQuery.Length > 0 Then
                                sbrQuery.Append(",")
                            End If
                            sbrQuery.Append(Trim(strFieldName(i)))
                        Else

                            If Not (strNotNullFilters) Is Nothing Then
                                For j = 0 To strNotNullFilters.Length - 1
                                    If Trim(strFieldName(i)) = Trim(strNotNullFilters(j)) Then
                                        strColumnName = Trim(strNotNullFilters(j))
                                        Exit For
                                    End If
                                Next
                            End If

                            For Each dr As DataRow In br_dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Select(strNotFilter, String.Concat(EquipmentHistoryData.TRCKNG_ID, " DESC"))
                                If sbrQuery.Length > 0 Then
                                    sbrQuery.Append(",")
                                End If

                                If strColumnName <> String.Empty AndAlso Not IsDBNull(dr.Item(Trim(strFieldName(i)))) Then
                                    sbrQuery.Append(String.Concat(Trim(strFieldName(i)), "='", dr.Item(Trim(strFieldName(i))), "'"))
                                    Exit For
                                ElseIf strColumnName = String.Empty AndAlso Not IsDBNull(dr.Item(Trim(strFieldName(i)))) Then
                                    sbrQuery.Append(String.Concat(Trim(strFieldName(i)), "='", Replace(CStr(dr.Item(Trim(strFieldName(i)))), "'", "''"), "'"))
                                    Exit For
                                ElseIf strColumnName = String.Empty AndAlso IsDBNull(dr.Item(Trim(strFieldName(i)))) Then
                                    sbrQuery.Append(String.Concat(Trim(strFieldName(i)), " = NULL"))
                                    Exit For
                                End If
                            Next
                        End If
                    Next

                    Dim strTableName() As String = CStr(drActivity.Item(EquipmentHistoryData.UPDATE_TABLE)).Split(CChar(","))

                    If sbrQuery.Length > 0 Then
                        For i = 0 To strTableName.Length - 1

                            If (bv_strActivityName.ToLower() = "gate out" AndAlso strTableName(i).ToLower() = "storage_charge") Then

                                Dim objEquipmentUpdates As New EquipmentUpdates
                                Dim lngCount As Long

                                lngCount = objEquipmentUpdates.Get_Storage_chargeCount(sbrWhereCondition.ToString(), objTrans)

                                If lngCount > 1 Then

                                    Dim strQry As String = String.Concat(" AND STRG_CHRG_ID IN ( SELECT MAX(STRG_CHRG_ID) FROM STORAGE_CHARGE WHERE ", sbrWhereCondition.ToString(), ")")

                                    sbrWhereCondition.Append(strQry)

                                    objEquipmentHistory.UpdateEquipmentActivity(sbrQuery.ToString(), sbrWhereCondition.ToString(), Trim(strTableName(i)), objTrans)
                                Else
                                    objEquipmentHistory.UpdateEquipmentActivity(sbrQuery.ToString(), sbrWhereCondition.ToString(), Trim(strTableName(i)), objTrans)
                                End If

                            Else

                                'Addtional cleaning Implemetation - Using Child table Column

                                If Not drActivity.Item(EquipmentHistoryData.CHILD_TABLE) Is DBNull.Value Then
                                    Dim strChildtable As String = drActivity.Item(EquipmentHistoryData.CHILD_TABLE).ToString()
                                    Dim strChildtableQry As String = String.Concat("SELECT ADDTNL_CLNNG_FLG FROM ", strChildtable, "  where  ", sbrWhereCondition.ToString())

                                    Dim blnAdditionalCleaning As Boolean = objEquipmentHistory.check_AddtionalFlag_ExecuteScalar(strChildtableQry, objTrans)

                                    If blnAdditionalCleaning = True Then
                                        objEquipmentHistory.UpdateEquipmentActivity(sbrQuery.ToString(), sbrWhereCondition.ToString(), Trim(strTableName(i)), objTrans)
                                    End If

                                Else
                                    objEquipmentHistory.UpdateEquipmentActivity(sbrQuery.ToString(), sbrWhereCondition.ToString(), Trim(strTableName(i)), objTrans)
                                End If


                                ' objEquipmentHistory.UpdateEquipmentActivity(sbrQuery.ToString(), sbrWhereCondition.ToString(), Trim(strTableName(i)), objTrans)
                            End If

                        Next

                    Else
                        Dim objEqUpdates As New EquipmentUpdates
                        objEqUpdates.Delete_Tracking(bv_intTrackingID, bv_intDepotID, objTrans)
                    End If


                End If
            Next

            objEquipmentHistory.UpdateTracking(bv_intTrackingID, bv_strCanceledBy, bv_dtCancelDate, bv_strAuditRemarks, bv_intDepotID, objTrans)

            'Addtional Cleaning Implemetation
            'Dim intRowCount As Integer = 0
            'For Each dr As DataRow In br_dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Select(strNotFilter, String.Concat(EquipmentHistoryData.TRCKNG_ID, " DESC"))
            '    If intRowCount = 0 AndAlso Not IsDBNull(dr.Item(EquipmentHistoryData.EQPMNT_STTS_ID)) AndAlso CInt(dr.Item(EquipmentHistoryData.EQPMNT_STTS_ID)) = 6 Then
            '        objEquipmentHistory.UpdateActivityStatus(CStr(dr.Item(EquipmentHistoryData.GI_TRNSCTN_NO)), _
            '                                                     CInt(5), bv_intDepotID, objTrans)
            '    End If
            '    Exit For
            'Next

            'If bv_strActivityName = "Inspection" Then
            '    dtCleaningDetail = objEquipmentHistory.GetCleaningDetail(bv_intTrackingID, bv_intDepotID, strEquipmentNo, objTrans).Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY)
            '    If dtCleaningDetail.Rows.Count > 0 Then
            '        objEquipmentHistory.UpdateTracking(CLng(dtCleaningDetail.Rows(0).Item(EquipmentHistoryData.TRCKNG_ID)), bv_strCanceledBy, bv_dtCancelDate, bv_strAuditRemarks, bv_intDepotID, objTrans)
            '        If dtCleaningDetail.Rows.Count > 1 Then
            '            objEquipmentHistory.UpdateActivityStatus(CStr(dtCleaningDetail.Rows(0).Item(EquipmentHistoryData.GI_TRNSCTN_NO)), _
            '                                                     CInt(dtCleaningDetail.Rows(1).Item(EquipmentHistoryData.EQPMNT_STTS_ID)), bv_intDepotID, objTrans)
            '        End If
            '    End If
            'End If

            Dim strGI_TransactionNo As String = String.Empty
            Dim strEquipment_No As String = String.Empty
            Dim strAddtionalQry As String = String.Empty
            Dim intAddtionalCount As Int32 = 0
            Dim strCleaningQry As String = String.Empty

            'bv_intDepotID

            If bv_strActivityName.ToLower() = "additional cleaning" Then

                If br_dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows.Count > 0 Then

                    strGI_TransactionNo = br_dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0).Item(EquipmentHistoryData.GI_TRNSCTN_NO).ToString()
                    strEquipment_No = br_dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0).Item(EquipmentHistoryData.EQPMNT_NO).ToString()

                    strAddtionalQry = String.Concat("SELECT COUNT(TRCKNG_ID) FROM TRACKING  WHERE EQPMNT_NO='", strEquipment_No, "' AND GI_TRNSCTN_NO='", strGI_TransactionNo, "' AND DPT_ID=", bv_intDepotID, " AND ACTVTY_NAM='", bv_strActivityName, "' AND CNCLD_BY IS NULL AND CNCLD_DT IS NULL")

                    intAddtionalCount = CInt(objEquipmentHistory.Query_ExecuteScalar(strAddtionalQry, objTrans))

                    If intAddtionalCount = 0 Then

                        strCleaningQry = String.Concat("UPDATE CLEANING SET ADDTNL_CLNNG_FLG=0 WHERE EQPMNT_NO='", strEquipment_No, "' and GI_TRNSCTN_NO='", strGI_TransactionNo, "' and DPT_ID=", bv_intDepotID)
                        objEquipmentHistory.Query_Execute(strCleaningQry, objTrans)
                    End If


                End If

            End If

            If bv_strActivityName.ToLower() = "inspection" Then

                If br_dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows.Count > 0 Then

                    strGI_TransactionNo = br_dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0).Item(EquipmentHistoryData.GI_TRNSCTN_NO).ToString()
                    strEquipment_No = br_dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0).Item(EquipmentHistoryData.EQPMNT_NO).ToString()

                    strAddtionalQry = String.Concat("SELECT COUNT(TRCKNG_ID) FROM TRACKING  WHERE EQPMNT_NO='", strEquipment_No, "' AND GI_TRNSCTN_NO='", strGI_TransactionNo, "' AND DPT_ID=", bv_intDepotID, " AND ACTVTY_NAM='", "Cleaning", "' AND CNCLD_BY IS NULL AND CNCLD_DT IS NULL")

                    intAddtionalCount = CInt(objEquipmentHistory.Query_ExecuteScalar(strAddtionalQry, objTrans))

                    If intAddtionalCount = 0 Then

                        strCleaningQry = String.Concat("UPDATE CLEANING SET ADDTNL_CLNNG_FLG=0 WHERE EQPMNT_NO='", strEquipment_No, "' and GI_TRNSCTN_NO='", strGI_TransactionNo, "' and DPT_ID=", bv_intDepotID)
                        objEquipmentHistory.Query_Execute(strCleaningQry, objTrans)
                    End If


                End If

            End If


            objTrans.commit()
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function
#End Region

End Class