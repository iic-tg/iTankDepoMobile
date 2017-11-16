#Region " Uploads.vb"
'*********************************************************************************************************************
'Name :
'      Uploads.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Uploads.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/20/2013 2:05:28 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
#Region "Uploads"

Public Class Uploads

#Region "Declaration Part.. "

	Dim objData As DataObjects
	Private Const UPLOAD_SCHEMASelectQueryPk As String= "SELECT SCHM_ID,SCHM_NAM,SCHM_QRY,RF_OBJCT_NAM,UPLD_TBL_NAM FROM UPLOAD_SCHEMA WHERE SCHM_ID=@SCHM_ID"
    Private Const UPLOAD_KEYSSelectQueryBySCHM_ID As String = "SELECT KY_ID,SCHM_ID,KY_NAM,CLMN_NAM,RF_TBL_NAM,RF_CLMN_NAM,KY_TYP FROM UPLOAD_KEYS WHERE SCHM_ID=@SCHM_ID"
    Private Const UPLOAD_SCHEMA_DETAILSelectQueryBySCHM_NAM As String = "SELECT COLUMN_NAME [CLMN_NAM],IS_NULLABLE [IS_NLLBL],DATA_TYPE [DT_TYP],CHARACTER_MAXIMUM_LENGTH [CHRCTR_MX_LNGTH] FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME in (SELECT name FROM SYSCOLUMNS WHERE id in (select id from sysobjects where name=@SCHM_NAM)) AND TABLE_NAME=@SCHM_NAM"
    Private Const UPLOAD_SchemaQry = "SELECT * FROM"
    Private Const UPLOAD_IDColumnSelectQuery As String = "SELECT [name] FROM syscolumns WHERE [id] IN (SELECT [id] FROM sysobjects  WHERE [name] =@TBL_NAM) AND colid IN (SELECT SIK.colid FROM sysindexkeys SIK JOIN sysobjects SO ON SIK.[id] = SO.[id] WHERE(SIK.indid = 1) AND SO.[name] = @TBL_NAM)"
    Private Const UPLOAD_CONDITIONWH As String = " WHERE DPT_ID=@DPT_ID"
    Private Const UPLOAD_CONDITION As String = " AND DPT_ID=@DPT_ID"
    Private Const Check_Previous_ActivityQuery As String = "SELECT * FROM INWARD_PASS"
    Private Const Upload_GateIn_checking As String = "SELECT EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_TYP_ID,DPT_ID,LSS_ID,LSS_CD,CSTMR_ID,EQPMNT_STTS_CD,CSTMR_NAM FROM V_GATEIN WHERE  GTN_CNDTN IN('A','C','D') AND (RC_EST_STTS <>'E' OR RC_EST_STTS IS NULL) AND (RSRV_BKG_BT <> 1 OR RSRV_BKG_BT IS NULL) AND (EST_STTS='C' OR EST_STTS IS NULL) AND OTWRDPSS_STTS_BT IS NULL AND DPT_ID=@DPT_ID and CSTMR_ID=(SELECT CSTMR_ID FROM CUSTOMER WHERE CSTMR_CD=@CSTMR_CD)"
    Private Const INWARD_PASSSelectQuery As String = "SELECT COUNT(*) as COUNT FROM INWARD_PASS"
    Private Const UPLOAD_CONDITIONSUB_ITEM As String = " AND ITM_ID=@ITM_ID"
    Private Const UPLOAD_CONDITIONEQUIP_TYPE As String = " AND EQPMNT_TYP_CD=@EQPMNT_TYP_CD"
    Private Const strEquipSelectQuery As String = "SELECT EQPMNT_NO FROM GATEIN WHERE EQPMNT_NO=@EQPMNT_NO AND GTOT_BT =0"
    Private Const Customer_SelectQuery As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM,CHK_DGT_VLDTN_BT FROM CUSTOMER WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"

    Private ds As UploadDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New UploadDataSet
    End Sub

#End Region

#Region "GetUPLOAD_SCHEMABySCHM_ID() TABLE NAME:UPLOAD_SCHEMA"

    Public Function GetUPLOAD_SCHEMABySCHM_ID(ByVal bv_i32SCHM_ID As Int32) As UploadDataSet
        Try
            objData = New DataObjects(UPLOAD_SCHEMASelectQueryPk, UploadData.SCHM_ID, CStr(bv_i32SCHM_ID))
            objData.Fill(CType(ds, DataSet), UploadData._UPLOAD_SCHEMA)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetUPLOAD_KEYSBySCHM_ID() TABLE NAME:UPLOAD_KEYS"

    Public Function GetUPLOAD_KEYSBySCHM_ID(ByVal bv_i32SCHM_ID As Int32) As UploadDataSet
        Try
            objData = New DataObjects(UPLOAD_KEYSSelectQueryBySCHM_ID, UploadData.SCHM_ID, CStr(bv_i32SCHM_ID))
            objData.Fill(CType(ds, DataSet), UploadData._UPLOAD_KEYS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetUPLOAD_SCHEMADETAILBySCHM_NAM() TABLE NAME:UPLOAD_SCHEMA_DETAIL"

    Public Function GetUPLOAD_SCHEMABySCHM_ID(ByVal bv_strSCHM_NAM As String) As UploadDataSet
        Try
            objData = New DataObjects(UPLOAD_SCHEMA_DETAILSelectQueryBySCHM_NAM, UploadData.SCHM_NAM, bv_strSCHM_NAM)
            objData.Fill(CType(ds, DataSet), UploadData._UPLOAD_SCHEMA_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetUPLOAD_SOURCE_TABLE() TABLE NAME:_UPLOAD_SOURCE_TABLE"

    Public Function GetUPLOAD_SOURCE_TABLE(ByVal bv_strSchemaQry As String, ByVal bv_i32Depot_ID As Int32) As UploadDataSet
        Try
            Dim dt As New DataTable(UploadData._UPLOAD_SOURCE_TABLE)
            Dim strQuery As String = String.Concat(bv_strSchemaQry, UPLOAD_CONDITIONWH)
            objData = New DataObjects(strQuery, UploadData.DPT_ID, bv_i32Depot_ID)
            objData.Fill(dt)
            ds.Tables.Add(dt)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateUpload() TABLE NAME:Upload Tables"

    Public Function CreateUpload(ByVal bv_InsertQry As String, _
        ByVal bv_dsUpload As UploadDataSet, _
        ByVal strTableName As String, ByVal strIDColumn As String, ByRef br_objtrans As Transactions) As Long
        Try
            Dim intMax As Long
            For Each dr As DataRow In bv_dsUpload.Tables(strTableName).Rows
                Dim drRow As DataRow = ds.Tables(strTableName).NewRow()
                If dr.Item("Valid") = True Then
                    objData = New DataObjects()
                    With dr
                        intMax = CommonUIs.GetIdentityValue(strTableName, br_objtrans)
                        drRow.Item(strIDColumn) = intMax
                        For Each dc As DataColumn In ds.Tables(strTableName).Columns
                            If dc.ColumnName <> strIDColumn Then

                                If (dr.Item(dc.ColumnName).ToString.Length = 0) Then
                                    drRow.Item(dc.ColumnName) = DBNull.Value
                                Else
                                    drRow.Item(dc.ColumnName) = dr.Item(dc.ColumnName)
                                End If
                            End If

                        Next
                    End With
                    If strTableName.ToUpper = "PRE_ADVICE" Then
                        drRow.Item("PR_ADVC_CD") = intMax
                        Dim objCommonUIs As New CommonUIs
                        objCommonUIs.CreateTracking(intMax, _
                                                 CLng(drRow.Item("CSTMR_ID")), _
                                                 CStr(drRow.Item("EQPMNT_NO")).ToString, _
                                                 "Pre-Advice", _
                                                 0, _
                                                 CStr(intMax), _
                                                 CDate(drRow.Item("ENTRD_DT")), _
                                                 IIf(IsDBNull(drRow.Item("RMRKS_VC")), "", drRow.Item("RMRKS_VC")), _
                                                 Nothing, _
                                                 Nothing, _
                                                 Nothing, _
                                                 Nothing, _
                                                 drRow.Item("CRTD_BY"), _
                                                 drRow.Item("CRTD_DT"), _
                                                 drRow.Item("MDFD_BY"), _
                                                 drRow.Item("MDFD_DT"), _
                                                 Nothing, _
                                                 Nothing, _
                                                 Nothing, _
                                                 drRow.Item("DPT_ID"), _
                                                 0, _
                                                 Nothing, _
                                                 Nothing, _
                                                 False, _
                                                 br_objtrans)
                    End If
                    objData.InsertRow(drRow, bv_InsertQry, br_objtrans)
                    drRow = Nothing
                End If
            Next
            CreateUpload = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetIdColumn()"

    Public Function GetIdColumn(ByVal bv_strTableName As String) As String
        Try
            objData = New DataObjects(UPLOAD_IDColumnSelectQuery, UploadData.TBL_NAM, bv_strTableName)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "GetCountCheckForeignkey()"

    Public Function GetCountCheckForeignkeySubItem(ByVal bv_StrRefQuery As String, ByVal bv_ITM_ID As Int32) As Int64
        Try
            Dim lngFKid As Int64
            Dim strQuery As String = String.Concat(bv_StrRefQuery, UPLOAD_CONDITIONSUB_ITEM)
            objData = New DataObjects(strQuery, UploadData.ITM_ID, bv_ITM_ID)
            lngFKid = objData.ExecuteScalar()
            Return lngFKid
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCountCheckForeignkey()"

    Public Function GetCountCheckForeignkeyEquipCode(ByVal bv_StrRefQuery As String, ByVal bv_EquipType_ID As String) As Int64
        Try
            Dim lngFKid As Int64
            Dim strQuery As String = String.Concat(bv_StrRefQuery, UPLOAD_CONDITIONEQUIP_TYPE)
            objData = New DataObjects(strQuery, UploadData.EQPMNT_TYP_CD, bv_EquipType_ID)
            lngFKid = objData.ExecuteScalar()
            If lngFKid <> Nothing Then
                objData = New DataObjects(String.Concat("SELECT EQPMNT_CD_ID FROM EQUIPMENT_CODE WHERE EQPMNT_CD_CD = ", lngFKid))
                lngFKid = objData.ExecuteScalar()
            End If
            Return lngFKid
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCountCheckForeignkey()"

    Public Function GetCountCheckForeignkey(ByVal bv_StrRefQuery As String, ByVal bv_i32Depot_ID As Int32) As Int64
        Try
            Dim lngFKid As Int64
            Dim strQuery As String = String.Concat(bv_StrRefQuery, UPLOAD_CONDITION)
            objData = New DataObjects(strQuery, UploadData.DPT_ID, bv_i32Depot_ID)
            lngFKid = objData.ExecuteScalar()
            Return lngFKid
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteTable()"

    Public Function DeleteTable(ByVal bv_strTableName As String, ByVal bv_lngDPT_ID As Int64, ByVal bv_EQPMNT_SZ As String, ByVal bv_EQPMNT_TYP As String, ByVal bv_I64Cstmr_Id As Int64, ByRef objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(bv_strTableName).NewRow()
            With dr
                .Item(UploadData.DPT_ID) = bv_lngDPT_ID
                .Item(UploadData.EQPMNT_SZ) = bv_EQPMNT_SZ
                .Item(UploadData.EQPMNT_TYP) = bv_EQPMNT_TYP
                .Item(UploadData.CSTMR_ID) = bv_I64Cstmr_Id
            End With
            Dim strDeleteQuery = "DELETE FROM " & bv_strTableName & " WHERE DPT_ID=@DPT_ID AND EQPMNT_SZ=@EQPMNT_SZ AND EQPMNT_TYP=@EQPMNT_TYP AND CSTMR_ID=@CSTMR_ID"
            DeleteTable = objData.DeleteRow(dr, strDeleteQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Check Previous Activity"
    Public Function pub_fnCheckPreviousActivity(ByVal bv_strLsrCode As String, ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Integer) As DataSet
        Try
            objData = New DataObjects(String.Concat(Check_Previous_ActivityQuery, " WHERE EQPMNT_NO='" & bv_strEquipmentNo & "' AND LSS_ID= (SELECT LSS_ID FROM LESSEE WHERE LSS_CD='" & bv_strLsrCode & "' and DPT_ID=" & bv_intDepotID & ") ORDER BY INWRDPSS_BIN DESC"))
            objData.Fill(CType(ds, DataSet), UploadData._UPLOAD_SCHEMA)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Check Duplicate"
    Public Function pub_GetDuplicateEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_strAuthNo As String) As Boolean
        Try
            objData = New DataObjects(String.Concat(INWARD_PASSSelectQuery, " WHERE EQPMNT_NO='" & bv_strEquipmentNo & "' AND AUTH_NO= '" & bv_strAuthNo & "'"))
            objData.Fill(CType(ds, DataSet), UploadData._UPLOAD_SCHEMA)
            If ds.Tables(UploadData._UPLOAD_SCHEMA).Rows(0).Item("COUNT") > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Check Previous GateIn-Entry"
    Public Function Pub_GateIn_Check(ByVal bv_strCstmrCD As String, ByVal bv_strDepoID As String) As DataSet
        Try
            Dim hvParams As New Hashtable
            hvParams.Add(UploadData.CSTMR_CD, bv_strCstmrCD)
            hvParams.Add(UploadData.DPT_ID, bv_strDepoID)
            'objData = New DataObjects(String.Concat(Upload_GateIn_checking, hvParams))
            objData = New DataObjects(Upload_GateIn_checking, hvParams)
            objData.Fill(CType(ds, DataSet), UploadData._UPLOAD_SCHEMA)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    Public Function DeleteCustomerTariffTable(ByVal bv_strTableName As String, ByVal bv_intDepotId As Integer, bv_intTariffDetailId As Long, objtrans As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(bv_strTableName).NewRow()
            With dr
                .Item(UploadData.DPT_ID) = bv_intDepotId
                .Item(UploadData.TRFF_CD_ID) = bv_intTariffDetailId
            End With
            Dim strDeleteQuery = "DELETE FROM " & bv_strTableName & " WHERE DPT_ID=@DPT_ID AND TRFF_CD_ID=@TRFF_CD_ID"
            DeleteCustomerTariffTable = objData.DeleteRow(dr, strDeleteQuery, objtrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Check Previous GateIn-Entry"
    Public Function pub_GateInCheckByEquipNo(ByVal bv_strEquipNo As String, ByVal bv_strDepoID As String) As Boolean
        Try
            Dim hvParams As New Hashtable
            hvParams.Add(UploadData.EQPMNT_NO, bv_strEquipNo)
            hvParams.Add(UploadData.DPT_ID, bv_strDepoID)
            'objData = New DataObjects(String.Concat(Upload_GateIn_checking, hvParams))
            objData = New DataObjects(strEquipSelectQuery, hvParams)
            objData.Fill(CType(ds, DataSet), UploadData._UPLOAD_SCHEMA)
            If ds.Tables(UploadData._UPLOAD_SCHEMA).Rows.Count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    Public Function pub_GetCustomerByCustmrId(ByVal bv_strCstmr_id As String, ByVal bv_intDepotId As String) As DataSet
        Try
            Dim hvParams As New Hashtable
            hvParams.Add(UploadData.CSTMR_ID, bv_strCstmr_id)
            hvParams.Add(UploadData.DPT_ID, bv_intDepotId)
            'objData = New DataObjects(String.Concat(Upload_GateIn_checking, hvParams))
            objData = New DataObjects(Customer_SelectQuery, hvParams)
            objData.Fill(CType(ds, DataSet), "Customer")
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

#End Region
