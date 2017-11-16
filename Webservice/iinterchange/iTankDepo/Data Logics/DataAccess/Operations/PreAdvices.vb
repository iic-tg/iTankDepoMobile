Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
Public Class PreAdvices

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const V_PRE_ADVICESelectQueryByID As String = "SELECT PR_ADVC_ID,PR_ADVC_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_SZ_ID,EQPMNT_SZ_CD,CSTMR_ID,CSTMR_CD,PRDCT_ID,PRDCT_CD,CLNNG_RFRNC_NO,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,GI_TRNSCTN_NO,ENTRD_DT,PRDCT_DSCRPTN_VC,COUNT_ATTACH,AUTH_NO,CNSGN_NM,EQPMNT_CD_ID,EQPMNT_CD_CD FROM V_PRE_ADVICE WHERE GI_TRNSCTN_NO IS NULL AND DPT_ID=@DPT_ID"
    Private Const PreAdviceEquipmentFromGateIn As String = "SELECT GTN_ID,GTN_CD,CSTMR_ID,(SELECT CSTMR_CD FROM CUSTOMER WHERE CSTMR_ID=GATEIN.CSTMR_ID)CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,EQPMNT_STTS_ID,YRD_LCTN,GTN_DT,GTN_TM,PRDCT_ID,EIR_NO,VHCL_NO,TRNSPRTR_CD,HTNG_BT,RMRKS_VC,GI_TRNSCTN_NO,GTOT_BT,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT FROM GATEIN WHERE DPT_ID=@DPT_ID AND EQPMNT_NO = @EQPMNT_NO ORDER BY GTN_ID DESC"
    Private Const Pre_AdviceInsertQuery As String = "INSERT INTO PRE_ADVICE(PR_ADVC_ID,PR_ADVC_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_SZ_ID,CSTMR_ID,PRDCT_ID,CLNNG_RFRNC_NO,ENTRD_DT,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,GI_TRNSCTN_NO,AUTH_NO,CNSGN_NM,EQPMNT_CD_ID)VALUES(@PR_ADVC_ID,@PR_ADVC_CD,@EQPMNT_NO,@EQPMNT_TYP_ID,@EQPMNT_SZ_ID,@CSTMR_ID,@PRDCT_ID,@CLNNG_RFRNC_NO,@ENTRD_DT,@RMRKS_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@DPT_ID,@GI_TRNSCTN_NO,@AUTH_NO,@CNSGN_NM,@EQPMNT_CD_ID)"
    Private Const Pre_AdviceUpdateQuery As String = "UPDATE PRE_ADVICE SET EQPMNT_NO=@EQPMNT_NO, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EQPMNT_SZ_ID=@EQPMNT_SZ_ID, CSTMR_ID=@CSTMR_ID, PRDCT_ID=@PRDCT_ID, CLNNG_RFRNC_NO=@CLNNG_RFRNC_NO,ENTRD_DT=@ENTRD_DT, RMRKS_VC=@RMRKS_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, DPT_ID=@DPT_ID, GI_TRNSCTN_NO=@GI_TRNSCTN_NO,CNSGN_NM=@CNSGN_NM,AUTH_NO=@AUTH_NO,EQPMNT_CD_ID=@EQPMNT_CD_ID WHERE PR_ADVC_ID=@PR_ADVC_ID"
    Private Const Pre_AdviceDeleteQuery As String = "DELETE FROM PRE_ADVICE WHERE PR_ADVC_ID=@PR_ADVC_ID"
    Private Const TrackingUpdateQuery As String = "UPDATE TRACKING SET CSTMR_ID=@CSTMR_ID, ACTVTY_DT=@ACTVTY_DT, ACTVTY_RMRKS=@ACTVTY_RMRKS,EQPMNT_NO=@EQPMNT_NO, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE DPT_ID=@DPT_ID AND ACTVTY_NO=@ACTVTY_NO AND ACTVTY_NAM = 'Pre-Advice'"
    Private Const TrackingDeleteQuery As String = "DELETE FROM TRACKING WHERE ACTVTY_NAM='Pre-Advice' AND DPT_ID=@DPT_ID AND ACTVTY_NO=@ACTVTY_NO"
    Private Const RentalSelectQueryByDepotId As String = "SELECT EQPMNT_NO,CSTMR_ID,CSTMR_CD,CSTMR_NAM,RNTL_RFRNC_NO,GI_TRNSCTN_NO,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT,(CASE WHEN(SELECT COUNT (RNTL_ENTRY_ID) FROM RENTAL_ENTRY WHERE EQPMNT_NO=VRE.EQPMNT_NO AND RNTL_RFRNC_NO=VRE.RNTL_RFRNC_NO AND ON_HR_DT IS NOT NULL)>0 THEN 1 ELSE 0 END)ALLOW_RENTAL FROM V_RENTAL_ENTRY VRE WHERE EQPMNT_NO=VRE.EQPMNT_NO AND DPT_ID=1 AND OFF_HR_DT IS NULL AND EQPMNT_NO NOT IN (SELECT EQPMNT_NO FROM TRACKING WHERE TRCKNG_ID > (SELECT TOP 1 TRCKNG_ID FROM TRACKING WHERE RNTL_RFRNC_NO=VRE.RNTL_RFRNC_NO ORDER BY TRCKNG_ID DESC) AND ACTVTY_NAM IN ('Pre-Advice','Gate In')) AND EQPMNT_NO = @EQPMNT_NO AND DPT_ID =@DPT_ID"
    Private Const SupplierEquipmentDetailsSelectQuery As String = "SELECT SPPLR_EQPMNT_DTL_ID,SPPLR_ID,SPPLR_CNTRCT_DTL_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD FROM V_SUPPLIER_EQUIPMENT_DETAIL WHERE EQPMNT_NO = @EQPMNT_NO"
    Private Const V_PRE_ADVICESelectQueryAttchmentByID As String = "SELECT PR_ADVC_ID,PR_ADVC_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_SZ_ID,EQPMNT_SZ_CD,CSTMR_ID,CSTMR_CD,PRDCT_ID,PRDCT_CD,CLNNG_RFRNC_NO,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,GI_TRNSCTN_NO,ENTRD_DT,PRDCT_DSCRPTN_VC,COUNT_ATTACH FROM V_PRE_ADVICE WHERE DPT_ID=@DPT_ID"
    Private Const V_PRE_ADVICEAttachmentSelectQueryByID As String = "SELECT TOP 1 PR_ADVC_ID,PR_ADVC_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_SZ_ID,EQPMNT_SZ_CD,CSTMR_ID,CSTMR_CD,PRDCT_ID,PRDCT_CD,CLNNG_RFRNC_NO,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,GI_TRNSCTN_NO,ENTRD_DT,PRDCT_DSCRPTN_VC,COUNT_ATTACH,AUTH_NO,CNSGN_NM FROM V_PRE_ADVICE WHERE  DPT_ID=@DPT_ID ORDER BY PR_ADVC_ID DESC"
    Private Const ValidateEquipmentNoInPreAdvice As String = "SELECT PR_ADVC_ID FROM PRE_ADVICE WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO is null AND DPT_ID <> @DPT_ID "
    Private Const ValidateStatusOfEquipmentQuery As String = "SELECT COUNT(*) FROM ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID <> @DPT_ID AND ACTV_BT=1"
    Private ds As PreAdviceDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New PreAdviceDataSet
    End Sub

#End Region
#Region "GetPreAdviceByDepotID() "

    Public Function GetPreAdviceByDepotID(ByVal bv_DepotID As Int64) As PreAdviceDataSet
        Try
            objData = New DataObjects(V_PRE_ADVICESelectQueryByID, PreAdviceData.DPT_ID, CStr(bv_DepotID))
            objData.Fill(CType(ds, DataSet), PreAdviceData._V_PRE_ADVICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetPreAdviceAttachmentByDepotID(ByVal bv_DepotID As Int64) As PreAdviceDataSet
        Try
            objData = New DataObjects(V_PRE_ADVICEAttachmentSelectQueryByID, PreAdviceData.DPT_ID, CStr(bv_DepotID))
            objData.Fill(CType(ds, DataSet), PreAdviceData._V_PRE_ADVICE_ATTACHMENT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "GetPreAdviceEquipmentByID() TABLE NAME:INWARD_PASS"

    Public Function GetPreAdviceEquipmentFromGateIn(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As PreAdviceDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(PreAdviceData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(PreAdviceData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(PreAdviceEquipmentFromGateIn, hshParameters)
            objData.Fill(CType(ds, DataSet), PreAdviceData._GATEIN)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreatePreAdvice() TABLE NAME:Pre_Advice"

    Public Function CreatePreAdvice(ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64EquipmentTypeId As Int64, _
        ByVal bv_i64EquipmentSizeId As Int64, _
        ByVal bv_i64CustomerId As Int64, _
        ByVal bv_i64ProductId As Int64, _
        ByVal bv_strCleaningReferenceNo As String, _
        ByVal bv_datEnteredDate As DateTime, _
        ByVal bv_strRemarks As String, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_i32DepotID As Int32, _
        ByVal bv_strGateInTransactionNo As String, _
        ByVal bv_strAuthNo As String, _
        ByVal bv_strConsignee As String, _
        ByVal bv_i64EquipmentCodeId As Int64, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(PreAdviceData._PRE_ADVICE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(PreAdviceData._PRE_ADVICE, br_objTrans)
                .Item(PreAdviceData.PR_ADVC_ID) = intMax
                .Item(PreAdviceData.PR_ADVC_CD) = intMax
                .Item(PreAdviceData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(PreAdviceData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                .Item(PreAdviceData.EQPMNT_CD_ID) = bv_i64EquipmentCodeId
                If bv_i64EquipmentSizeId <> 0 Then
                    .Item(PreAdviceData.EQPMNT_SZ_ID) = bv_i64EquipmentSizeId
                Else
                    .Item(PreAdviceData.EQPMNT_SZ_ID) = DBNull.Value
                End If
                .Item(PreAdviceData.CSTMR_ID) = bv_i64CustomerId
                If bv_i64ProductId <> 0 Then
                    .Item(PreAdviceData.PRDCT_ID) = bv_i64ProductId
                Else
                    .Item(PreAdviceData.PRDCT_ID) = DBNull.Value
                End If
                .Item(PreAdviceData.ENTRD_DT) = bv_datEnteredDate
                If bv_strCleaningReferenceNo <> Nothing Then
                    .Item(PreAdviceData.CLNNG_RFRNC_NO) = bv_strCleaningReferenceNo
                Else
                    .Item(PreAdviceData.CLNNG_RFRNC_NO) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(PreAdviceData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(PreAdviceData.RMRKS_VC) = DBNull.Value
                End If
                .Item(PreAdviceData.CRTD_BY) = bv_strCreatedBy
                .Item(PreAdviceData.CRTD_DT) = bv_datCreatedDate
                .Item(PreAdviceData.MDFD_BY) = bv_strModifiedBy
                .Item(PreAdviceData.MDFD_DT) = bv_datModifiedDate
                .Item(PreAdviceData.DPT_ID) = bv_i32DepotID
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(PreAdviceData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(PreAdviceData.GI_TRNSCTN_NO) = DBNull.Value
                End If

                If bv_strAuthNo <> Nothing Then
                    .Item(PreAdviceData.AUTH_NO) = bv_strAuthNo
                Else
                    .Item(PreAdviceData.AUTH_NO) = DBNull.Value
                End If
                If bv_strConsignee <> Nothing Then
                    .Item(PreAdviceData.CNSGN_NM) = bv_strConsignee
                Else
                    .Item(PreAdviceData.CNSGN_NM) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Pre_AdviceInsertQuery, br_objTrans)
            dr = Nothing
            CreatePreAdvice = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdatePreAdvice() TABLE NAME:Pre_Advice"

    Public Function UpdatePreAdvice(ByVal bv_i64PreAdviceID As Int64, _
        ByVal bv_strPreAdviceCD As String, _
        ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64EquipmentTypeId As Int64, _
        ByVal bv_i64EquipmentSizeId As Int64, _
        ByVal bv_i64CustomerId As Int64, _
        ByVal bv_i64ProductId As Int64, _
        ByVal bv_strCleaningReferenceNo As String, _
        ByVal bv_datEnteredDate As DateTime, _
        ByVal bv_strRemarks As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_i32DepotID As Int32, _
        ByVal bv_strGateInTransactionNo As String, _
        ByVal bv_strAuthNo As String, _
        ByVal bv_strConsignee As String, _
        ByVal bv_lngEquipmentType As Int64, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(PreAdviceData._PRE_ADVICE).NewRow()
            With dr
                .Item(PreAdviceData.PR_ADVC_ID) = bv_i64PreAdviceID
                .Item(PreAdviceData.PR_ADVC_CD) = bv_strPreAdviceCD
                .Item(PreAdviceData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(PreAdviceData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                .Item(PreAdviceData.EQPMNT_CD_ID) = bv_lngEquipmentType
                If bv_i64EquipmentSizeId <> 0 Then
                    .Item(PreAdviceData.EQPMNT_SZ_ID) = bv_i64EquipmentSizeId
                Else
                    .Item(PreAdviceData.EQPMNT_SZ_ID) = DBNull.Value
                End If
                .Item(PreAdviceData.CSTMR_ID) = bv_i64CustomerId
                If bv_i64ProductId <> 0 Then
                    .Item(PreAdviceData.PRDCT_ID) = bv_i64ProductId
                Else
                    .Item(PreAdviceData.PRDCT_ID) = DBNull.Value
                End If
                If bv_strCleaningReferenceNo <> Nothing Then
                    .Item(PreAdviceData.CLNNG_RFRNC_NO) = bv_strCleaningReferenceNo
                Else
                    .Item(PreAdviceData.CLNNG_RFRNC_NO) = DBNull.Value
                End If
                .Item(PreAdviceData.ENTRD_DT) = bv_datEnteredDate
                If bv_strRemarks <> Nothing Then
                    .Item(PreAdviceData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(PreAdviceData.RMRKS_VC) = DBNull.Value
                End If
                .Item(PreAdviceData.MDFD_BY) = bv_strModifiedBy
                .Item(PreAdviceData.MDFD_DT) = bv_datModifiedDate
                .Item(PreAdviceData.DPT_ID) = bv_i32DepotID
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(PreAdviceData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(PreAdviceData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                If bv_strAuthNo <> Nothing Then
                    .Item(PreAdviceData.AUTH_NO) = bv_strAuthNo
                Else
                    .Item(PreAdviceData.AUTH_NO) = DBNull.Value
                End If
                If bv_strConsignee <> Nothing Then
                    .Item(PreAdviceData.CNSGN_NM) = bv_strConsignee
                Else
                    .Item(PreAdviceData.CNSGN_NM) = DBNull.Value
                End If
            End With
            UpdatePreAdvice = objData.UpdateRow(dr, Pre_AdviceUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeletePreAdvice() TABLE NAME:Pre_Advice"

    Public Function DeletePreAdvice(ByVal bv_strPreAdviceID As Int64, ByRef br_objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(PreAdviceData._PRE_ADVICE).NewRow()
            With dr
                .Item(PreAdviceData.PR_ADVC_ID) = bv_strPreAdviceID
            End With
            DeletePreAdvice = objData.DeleteRow(dr, Pre_AdviceDeleteQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateTracking() TABLE NAME:Tracking"

    Public Function UpdateTracking(ByVal bv_i64PreAdviceID As String, _
                                ByVal bv_strEQPMNT_NO As String, _
                                ByVal bv_i64CustomerID As Int64, _
                                ByVal bv_datActivity As DateTime, _
                                ByVal bv_strActivityRemarks As String, _
                                ByVal strGI_TRNSCTN_NO As String, _
                                ByVal bv_i32DepotId As Int32, _
                                ByVal bv_ModifiedBy As String, _
                                ByVal bv_ModifiedDate As DateTime, _
                                ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(PreAdviceData._TRACKING).NewRow()
            With dr
                .Item(PreAdviceData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(PreAdviceData.CSTMR_ID) = bv_i64CustomerID
                .Item(PreAdviceData.ACTVTY_DT) = bv_datActivity
                .Item(PreAdviceData.ACTVTY_NO) = bv_i64PreAdviceID
                If bv_strActivityRemarks <> Nothing Then
                    .Item(PreAdviceData.ACTVTY_RMRKS) = bv_strActivityRemarks
                Else
                    .Item(PreAdviceData.ACTVTY_RMRKS) = DBNull.Value
                End If
                If strGI_TRNSCTN_NO <> Nothing Then
                    .Item(PreAdviceData.GI_TRNSCTN_NO) = strGI_TRNSCTN_NO
                Else
                    .Item(PreAdviceData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(PreAdviceData.DPT_ID) = bv_i32DepotId
                .Item(PreAdviceData.MDFD_BY) = bv_ModifiedBy
                .Item(PreAdviceData.MDFD_DT) = bv_ModifiedDate
            End With
            UpdateTracking = objData.UpdateRow(dr, TrackingUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteTracking() TABLE NAME:TRACKING"

    Public Function DeleteTracking(ByVal bv_strPreAdviceID As Int64, _
                                    ByVal bv_intDepotID As Integer, _
                                    ByVal bv_strActivityName As String, _
                                    ByRef br_objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(PreAdviceData._TRACKING).NewRow()
            With dr
                .Item(PreAdviceData.ACTVTY_NO) = bv_strPreAdviceID
                .Item(PreAdviceData.DPT_ID) = bv_intDepotID
                .Item(PreAdviceData.ACTVTY_NAM) = bv_strActivityName
            End With
            DeleteTracking = objData.DeleteRow(dr, TrackingDeleteQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRentalDetails() TABLE NAME:GATEIN"

    Public Function GetRentalDetails(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As PreAdviceDataSet
        Try
            Dim hshParameters As New Hashtable()
            Dim dsRentalEntry As New PreAdviceDataSet
            hshParameters.Add(PreAdviceData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(PreAdviceData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(RentalSelectQueryByDepotId, hshParameters)
            objData.Fill(CType(dsRentalEntry, DataSet), PreAdviceData._V_RENTAL_ENTRY)
            'Return objData.ExecuteScalar()
            Return dsRentalEntry
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetSupplierDetails() TABLE NAME:SUPPLIER_EQUIPMENT_DETAIL"

    Public Function GetSupplierDetails(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As PreAdviceDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(PreAdviceData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(PreAdviceData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(SupplierEquipmentDetailsSelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), PreAdviceData._V_SUPPLIER_EQUIPMENT_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    'For Attchemmet
#Region "GetPreAdviceByDepotIDAttchment() "

    Public Function GetPreAdviceByDepotIDAttchment(ByVal bv_DepotID As Int64) As PreAdviceDataSet
        Try
            objData = New DataObjects(V_PRE_ADVICESelectQueryAttchmentByID, PreAdviceData.DPT_ID, CStr(bv_DepotID))
            objData.Fill(CType(ds, DataSet), PreAdviceData._V_PRE_ADVICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetPreAdviceEquipmentByID() TABLE NAME:INWARD_PASS"

    Public Function GetValidateEquipmentNoInPreAdvice(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As PreAdviceDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(PreAdviceData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(PreAdviceData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(ValidateEquipmentNoInPreAdvice, hshParameters)
            objData.Fill(CType(ds, DataSet), PreAdviceData._PRE_ADVICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "ValidateStatusOfEquipment() TABLE NAME:ACTIVITY_STATUS"

    Public Function ValidateStatusOfEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentInformationData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(EquipmentInformationData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(ValidateStatusOfEquipmentQuery, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class
