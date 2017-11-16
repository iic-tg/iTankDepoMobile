Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
Public Class Suppliers
#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const SUPPLIER_CONTRACT_DETAILSelectQueryBySupplierId As String = "SELECT SPPLR_CNTRCT_DTL_ID,SPPLR_ID,CNTRCT_RFRNC_NO,CNTRCT_STRT_DT,CNTRCT_END_DT,RNTL_PR_DY,RMRKS_VC FROM SUPPLIER_CONTRACT_DETAIL WHERE SPPLR_ID=@SPPLR_ID"
    Private Const V_SUPPLIER_EQUIPMENT_DETAILSelectQueryBy As String = "SELECT SPPLR_EQPMNT_DTL_ID,SPPLR_ID,SPPLR_CNTRCT_DTL_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD FROM V_SUPPLIER_EQUIPMENT_DETAIL WHERE SPPLR_ID=@SPPLR_ID AND SPPLR_CNTRCT_DTL_ID=@SPPLR_CNTRCT_DTL_ID"
    Private Const SupplierInsertQuery As String = "INSERT INTO SUPPLIER(SPPLR_ID,SPPLR_CD,SPPLR_DSCRPTN_VC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID)VALUES(@SPPLR_ID,@SPPLR_CD,@SPPLR_DSCRPTN_VC,@ACTV_BT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@DPT_ID)"
    Private Const Supplier_Contract_DetailInsertQuery As String = "INSERT INTO SUPPLIER_CONTRACT_DETAIL(SPPLR_CNTRCT_DTL_ID,SPPLR_ID,CNTRCT_RFRNC_NO,CNTRCT_STRT_DT,CNTRCT_END_DT,RNTL_PR_DY,RMRKS_VC)VALUES(@SPPLR_CNTRCT_DTL_ID,@SPPLR_ID,@CNTRCT_RFRNC_NO,@CNTRCT_STRT_DT,@CNTRCT_END_DT,@RNTL_PR_DY,@RMRKS_VC)"
    Private Const Supplier_Contract_DetailUpdateQuery As String = "UPDATE SUPPLIER_CONTRACT_DETAIL SET SPPLR_CNTRCT_DTL_ID=@SPPLR_CNTRCT_DTL_ID, SPPLR_ID=@SPPLR_ID, CNTRCT_RFRNC_NO=@CNTRCT_RFRNC_NO, CNTRCT_STRT_DT=@CNTRCT_STRT_DT, CNTRCT_END_DT=@CNTRCT_END_DT, RNTL_PR_DY=@RNTL_PR_DY, RMRKS_VC=@RMRKS_VC WHERE SPPLR_CNTRCT_DTL_ID=@SPPLR_CNTRCT_DTL_ID"
    Private Const Supplier_Equipment_DetailInsertQuery As String = "INSERT INTO SUPPLIER_EQUIPMENT_DETAIL(SPPLR_EQPMNT_DTL_ID,SPPLR_ID,SPPLR_CNTRCT_DTL_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID)VALUES(@SPPLR_EQPMNT_DTL_ID,@SPPLR_ID,@SPPLR_CNTRCT_DTL_ID,@EQPMNT_NO,@EQPMNT_TYP_ID,@EQPMNT_CD_ID)"
    Private Const Supplier_Equipment_DetailUpdateQuery As String = "UPDATE SUPPLIER_EQUIPMENT_DETAIL SET SPPLR_EQPMNT_DTL_ID=@SPPLR_EQPMNT_DTL_ID, SPPLR_ID=@SPPLR_ID, SPPLR_CNTRCT_DTL_ID=@SPPLR_CNTRCT_DTL_ID, EQPMNT_NO=@EQPMNT_NO, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EQPMNT_CD_ID=@EQPMNT_CD_ID WHERE SPPLR_EQPMNT_DTL_ID=@SPPLR_EQPMNT_DTL_ID"
    Private Const SupplierUpdateQuery As String = "UPDATE SUPPLIER SET SPPLR_ID=@SPPLR_ID, SPPLR_CD=@SPPLR_CD, SPPLR_DSCRPTN_VC=@SPPLR_DSCRPTN_VC, ACTV_BT=@ACTV_BT, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, DPT_ID=@DPT_ID WHERE SPPLR_ID=@SPPLR_ID"
    Private Const ValidateSupplierCodeQuery As String = "SELECT SPPLR_ID,SPPLR_CD,SPPLR_DSCRPTN_VC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM SUPPLIER WHERE SPPLR_CD=@SPPLR_CD AND DPT_ID=@DPT_ID"
    Private Const SupplierEqpInfoSelectQueryByDepotId As String = "SELECT SPPLR_EQPMNT_DTL_ID FROM V_SUPPLIER_EQUIPMENT_DETAIL WHERE (EQPMNT_NO=@EQPMNT_NO )"
    Private Const EquipmentDeleteQuery As String = "DELETE FROM SUPPLIER_EQUIPMENT_DETAIL WHERE SPPLR_CNTRCT_DTL_ID=@SPPLR_CNTRCT_DTL_ID "
    Private Const ContractDeleteQuery As String = "DELETE FROM SUPPLIER_CONTRACT_DETAIL WHERE SPPLR_CNTRCT_DTL_ID=@SPPLR_CNTRCT_DTL_ID"
    Private Const EquipmentDetailDeleteQuery As String = "DELETE FROM SUPPLIER_EQUIPMENT_DETAIL WHERE SPPLR_EQPMNT_DTL_ID=@SPPLR_EQPMNT_DTL_ID"
    Private Const Equipment_InformationUpdateQuery As String = "UPDATE EQUIPMENT_INFORMATION SET EQPMNT_NO=@EQPMNT_NO, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, DPT_ID=@DPT_ID, ACTV_BT=@ACTV_BT WHERE EQPMNT_INFRMTN_ID=@EQPMNT_INFRMTN_ID"
    Private Const Equipment_InfoCountQuery As String = "SELECT COUNT(EQPMNT_NO) FROM EQUIPMENT_INFORMATION WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO"
    Private Const Equipment_InfoUpdateQuery As String = "UPDATE EQUIPMENT_INFORMATION SET EQPMNT_TYP_ID=@EQPMNT_TYP_ID,RNTL_BT=@RNTL_BT WHERE EQPMNT_NO = @EQPMNT_NO AND DPT_ID=@DPT_ID AND ACTV_BT=1"
    Private Const Equipment_InformationDeleteQuery As String = "DELETE FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const ValidateContractRefQuery As String = "SELECT SPPLR_CNTRCT_DTL_ID,SPPLR_ID,(SELECT SPPLR_CD FROM SUPPLIER WHERE SPPLR_ID=S.SPPLR_ID) AS SPPLR_CD,CNTRCT_RFRNC_NO FROM SUPPLIER_CONTRACT_DETAIL S WHERE CNTRCT_RFRNC_NO=@CNTRCT_RFRNC_NO"
    Private Const SUPPLIER_EqpmntDetailSelectQuery As String = "SELECT SPPLR_EQPMNT_DTL_ID,SPPLR_ID,SPPLR_CNTRCT_DTL_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID FROM SUPPLIER_EQUIPMENT_DETAIL WHERE SPPLR_CNTRCT_DTL_ID=@SPPLR_CNTRCT_DTL_ID"
    Private Const EquipmentInfoSelectQuery As String = "SELECT ACTVTY_STTS_ID FROM ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND ACTV_BT=1"
    Private Const GetContractDetailsQuery As String = "SELECT SPPLR_EQPMNT_DTL_ID,SPPLR_CNTRCT_DTL_ID,(SELECT CNTRCT_RFRNC_NO FROM SUPPLIER_CONTRACT_DETAIL WHERE SPPLR_CNTRCT_DTL_ID=VS.SPPLR_CNTRCT_DTL_ID) AS CNTRCT_RFRNC_NO ,(SELECT SPPLR_CD FROM SUPPLIER WHERE SPPLR_ID=VS.SPPLR_ID) AS SPPLR_CD  FROM V_SUPPLIER_EQUIPMENT_DETAIL VS WHERE EQPMNT_NO=@EQPMNT_NO"
    Private Const RentalEntryCountQuery As String = "SELECT COUNT(RNTL_ENTRY_ID) FROM RENTAL_ENTRY WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const SupplierDetailsQuery As String = "SELECT SPPLR_EQPMNT_DTL_ID,SPPLR_ID,SPPLR_CNTRCT_DTL_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID FROM SUPPLIER_EQUIPMENT_DETAIL WHERE SPPLR_ID=@SPPLR_ID"
    Private Const RentalEntrySelectQuery As String = "SELECT RNTL_ENTRY_ID,EQPMNT_NO,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT,RMRKS_VC,RNTL_RFRNC_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT FROM V_RENTAL_ENTRY V WHERE DPT_ID=@DPT_ID"
    Private ds As SupplierDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New SupplierDataSet
    End Sub

#End Region

#Region "UpdateEquipment_Info"
    Public Function UpdateEquipment_Info(ByVal bv_strEquipmentNo As String, _
                                     ByVal bv_intEquipmentTypeID As Int32, _
                                     ByVal bv_intDepotID As Integer, _
                                     ByVal bv_RentalBit As Boolean, _
                                     ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(SupplierData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                .Item(SupplierData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(SupplierData.EQPMNT_TYP_ID) = bv_intEquipmentTypeID
                .Item(SupplierData.DPT_ID) = bv_intDepotID
                .Item(SupplierData.RNTL_BT) = bv_RentalBit

            End With
            UpdateEquipment_Info = objData.UpdateRow(dr, Equipment_InfoUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "GetEquipmentInfo"
    Public Function GetEquipmentInfo(ByVal bv_strEquipmentNo As String, _
                                     ByVal bv_intDepotID As Integer, _
                                     ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(SupplierData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(SupplierData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(Equipment_InfoCountQuery, hshTable)
            Return objData.ExecuteScalar(br_ObjTransactions)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentInformationByID() TABLE NAME:EQUIPMENT_INFORMATION"

    Public Function GetEquipmentInformationByID(ByVal bv_strEquipmentNo As String, ByVal SupplierContractId As Integer, ByVal bv_i32DepotID As Int32) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(SupplierData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(SupplierData.SPPLR_CNTRCT_DTL_ID, SupplierContractId)
            objData = New DataObjects(SupplierEqpInfoSelectQueryByDepotId, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetSupplierCode()"

    Public Function pub_GetSupplierCode(ByVal bv_strSupplier_CD As String, ByVal bv_DepotId As Integer) As SupplierDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(SupplierData.SPPLR_CD, bv_strSupplier_CD)
            hshParameters.Add(SupplierData.DPT_ID, bv_DepotId)
            objData = New DataObjects(ValidateSupplierCodeQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), SupplierData._SUPPLIER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetSupplierContractDetailBySupplierId() TABLE NAME:SUPPLIER_CONTRACT_DETAIL"

    Public Function pub_GetSupplierContractDetails(ByVal bv_i64SupplierId As Int64) As SupplierDataSet
        Try
            objData = New DataObjects(SUPPLIER_CONTRACT_DETAILSelectQueryBySupplierId, SupplierData.SPPLR_ID, (bv_i64SupplierId))
            objData.Fill(CType(ds, DataSet), SupplierData._SUPPLIER_CONTRACT_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetVSupplierEquipmentDetailBySupplierId() TABLE NAME:V_SUPPLIER_EQUIPMENT_DETAIL"

    Public Function GetVSupplierEquipmentDetailBySupplierId(ByVal bv_i64SupplierId As Int64, ByVal bv_i64ContractId As Int64) As SupplierDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(SupplierData.SPPLR_ID, bv_i64SupplierId)
            hshParameters.Add(SupplierData.SPPLR_CNTRCT_DTL_ID, bv_i64ContractId)
            objData = New DataObjects(V_SUPPLIER_EQUIPMENT_DETAILSelectQueryBy, hshParameters)
            objData.Fill(CType(ds, DataSet), SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateSupplier() TABLE NAME:Supplier"

    Public Function CreateSupplier(ByVal bv_strSupplierCode As String, _
        ByVal bv_strSupplierDescription As String, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strMNodifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_i32DepotID As Int32, _
        ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(SupplierData._SUPPLIER).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(SupplierData._SUPPLIER, br_ObjTransactions)
                .Item(SupplierData.SPPLR_ID) = intMax
                .Item(SupplierData.SPPLR_CD) = bv_strSupplierCode
                .Item(SupplierData.SPPLR_DSCRPTN_VC) = bv_strSupplierDescription
                .Item(SupplierData.ACTV_BT) = bv_blnActiveBit
                .Item(SupplierData.CRTD_BY) = bv_strCreatedBy
                .Item(SupplierData.CRTD_DT) = bv_datCreatedDate
                .Item(SupplierData.MDFD_BY) = bv_strMNodifiedBy
                .Item(SupplierData.MDFD_DT) = bv_datModifiedDate
                .Item(SupplierData.DPT_ID) = bv_i32DepotID
            End With
            objData.InsertRow(dr, SupplierInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateSupplier = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateSupplier() TABLE NAME:Supplier"

    Public Function UpdateSupplier(ByVal bv_i64SupplierID As Int64, _
        ByVal bv_strSupplierCode As String, _
        ByVal bv_strSupplierDescription As String, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_strMNodifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_i32DepotID As Int32, _
        ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(SupplierData._SUPPLIER).NewRow()
            With dr
                .Item(SupplierData.SPPLR_ID) = bv_i64SupplierID
                .Item(SupplierData.SPPLR_CD) = bv_strSupplierCode
                .Item(SupplierData.SPPLR_DSCRPTN_VC) = bv_strSupplierDescription
                .Item(SupplierData.ACTV_BT) = bv_blnActiveBit
                .Item(SupplierData.MDFD_BY) = bv_strMNodifiedBy
                .Item(SupplierData.MDFD_DT) = bv_datModifiedDate
                .Item(SupplierData.DPT_ID) = bv_i32DepotID
            End With
            UpdateSupplier = objData.UpdateRow(dr, SupplierUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateSupplierEquipmentDetail() TABLE NAME:Supplier_Equipment_Detail"

    Public Function CreateSupplierEquipmentDetail(ByVal bv_i64SupplierId As Int64, _
        ByVal bv_i64SupplierContractDetailId As Int64, _
        ByVal bv_strEqpmntNo As String, _
        ByVal bv_i64EqpmntTypId As Int64, _
        ByVal bv_i64EqpmntCodeId As Int64, _
        ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(SupplierData._SUPPLIER_EQUIPMENT_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(SupplierData._SUPPLIER_EQUIPMENT_DETAIL, br_ObjTransactions)
                .Item(SupplierData.SPPLR_EQPMNT_DTL_ID) = intMax
                .Item(SupplierData.SPPLR_ID) = bv_i64SupplierId
                .Item(SupplierData.SPPLR_CNTRCT_DTL_ID) = bv_i64SupplierContractDetailId
                .Item(SupplierData.EQPMNT_NO) = bv_strEqpmntNo
                .Item(SupplierData.EQPMNT_TYP_ID) = bv_i64EqpmntTypId
                If bv_i64EqpmntCodeId <> 0 Then
                    .Item(SupplierData.EQPMNT_CD_ID) = bv_i64EqpmntCodeId
                Else
                    .Item(SupplierData.EQPMNT_CD_ID) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Supplier_Equipment_DetailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateSupplierEquipmentDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateSupplierContractDetail() TABLE NAME:Supplier_Contract_Detail"

    Public Function CreateSupplierContract(ByVal bv_i64SupplierId As Int64, _
        ByVal bv_strContractReferenceNo As String, _
        ByVal bv_datContractStartDate As DateTime, _
        ByVal bv_datContractEndDate As DateTime, _
        ByVal bv_dblRentalPerDay As Double, _
        ByVal bv_strRemarks As String, _
         ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(SupplierData._SUPPLIER_CONTRACT_DETAIL, br_ObjTransactions)
                .Item(SupplierData.SPPLR_CNTRCT_DTL_ID) = intMax
                .Item(SupplierData.SPPLR_ID) = bv_i64SupplierId
                .Item(SupplierData.CNTRCT_RFRNC_NO) = bv_strContractReferenceNo
                .Item(SupplierData.CNTRCT_STRT_DT) = bv_datContractStartDate
                .Item(SupplierData.CNTRCT_END_DT) = bv_datContractEndDate
                .Item(SupplierData.RNTL_PR_DY) = bv_dblRentalPerDay
                If bv_strRemarks <> Nothing Then
                    .Item(SupplierData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(SupplierData.RMRKS_VC) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Supplier_Contract_DetailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateSupplierContract = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "DeleteEquipmentDetail() "

    Public Function DeleteEquipmentDetail(ByVal bv_ContractId As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).NewRow()
            With dr
                .Item(SupplierData.SPPLR_CNTRCT_DTL_ID) = bv_ContractId
            End With
            DeleteEquipmentDetail = objData.DeleteRow(dr, EquipmentDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteContractDetails() "

    Public Function DeleteContractDetails(ByVal bv_ContractId As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).NewRow()
            With dr
                .Item(SupplierData.SPPLR_CNTRCT_DTL_ID) = bv_ContractId
            End With
            DeleteContractDetails = objData.DeleteRow(dr, ContractDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteEqpmntDetails() "

    Public Function DeleteEqpmntDetails(ByVal bv_EqpmntId As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL).NewRow()
            With dr
                .Item(SupplierData.SPPLR_EQPMNT_DTL_ID) = bv_EqpmntId
            End With
            DeleteEqpmntDetails = objData.DeleteRow(dr, EquipmentDetailDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteEquipmentInformation() TABLE NAME:Equipment_Information"

    Public Function DeleteEquipmentInformation(ByVal EqpmntNo As String, _
                                               ByVal bv_DPT_ID As Integer, _
                                               ByRef br_objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(SupplierData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                .Item(SupplierData.EQPMNT_NO) = EqpmntNo
                .Item(SupplierData.DPT_ID) = bv_DPT_ID
            End With
            DeleteEquipmentInformation = objData.DeleteRow(dr, Equipment_InformationDeleteQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateSupplierContract() TABLE NAME:Supplier_Contract_Detail"

    Public Function UpdateSupplierContract(ByVal bv_i64SupplierContractDetailId As Int64, _
        ByVal bv_i64SupplierId As Int64, _
        ByVal bv_strContractReferenceNo As String, _
        ByVal bv_datContractStartDate As DateTime, _
        ByVal bv_datContractEndDate As DateTime, _
        ByVal bv_dblRentalPerDay As Double, _
        ByVal bv_strRemarks As String, _
         ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).NewRow()
            With dr
                .Item(SupplierData.SPPLR_CNTRCT_DTL_ID) = bv_i64SupplierContractDetailId
                .Item(SupplierData.SPPLR_ID) = bv_i64SupplierId
                .Item(SupplierData.CNTRCT_RFRNC_NO) = bv_strContractReferenceNo
                .Item(SupplierData.CNTRCT_STRT_DT) = bv_datContractStartDate
                .Item(SupplierData.CNTRCT_END_DT) = bv_datContractEndDate
                .Item(SupplierData.RNTL_PR_DY) = bv_dblRentalPerDay
                If bv_strRemarks <> Nothing Then
                    .Item(SupplierData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(SupplierData.RMRKS_VC) = DBNull.Value
                End If
            End With
            UpdateSupplierContract = objData.UpdateRow(dr, Supplier_Contract_DetailUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateEquipmentInformation() TABLE NAME:Equipment_Information"

    Public Function UpdateEquipmentInformation(ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64EquipmentTypeID As Int64, _
        ByVal bv_datManufactureDate As DateTime, _
        ByVal bv_dblTareWeight As Double, _
        ByVal bv_dblGrossWeight As Double, _
        ByVal bv_dblCapacity As Double, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_blnActiveBit As Boolean, _
          ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentInformationData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                .Item(EquipmentInformationData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_i64EquipmentTypeID <> 0 Then
                    .Item(EquipmentInformationData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                Else
                    .Item(EquipmentInformationData.EQPMNT_TYP_ID) = DBNull.Value
                End If
                If bv_datManufactureDate <> Nothing Then
                    .Item(EquipmentInformationData.MNFCTR_DT) = bv_datManufactureDate
                Else
                    .Item(EquipmentInformationData.MNFCTR_DT) = DBNull.Value
                End If
                .Item(EquipmentInformationData.TR_WGHT_NC) = bv_dblTareWeight
                .Item(EquipmentInformationData.GRSS_WGHT_NC) = bv_dblGrossWeight
                .Item(EquipmentInformationData.CPCTY_NC) = bv_dblCapacity
                .Item(EquipmentInformationData.MDFD_BY) = bv_strModifiedBy
                .Item(EquipmentInformationData.MDFD_DT) = bv_datModifiedDate
                .Item(EquipmentInformationData.DPT_ID) = bv_i32DepotId
                .Item(EquipmentInformationData.ACTV_BT) = bv_blnActiveBit
            End With
            UpdateEquipmentInformation = objData.UpdateRow(dr, Equipment_InformationUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateSupplierEquipmentDetail() TABLE NAME:Supplier_Equipment_Detail"

    Public Function UpdateSupplierEquipmentDetail(ByVal bv_i64SupplierEqpmntDetailId As Int64, _
        ByVal bv_i64SupplierId As Int64, _
        ByVal bv_i64SupplierContractDetailId As Int64, _
        ByVal bv_strEqpmntNo As String, _
        ByVal bv_i64EqpmntTypId As Int64, _
        ByVal bv_i64EqpmntCodeId As Int64, _
         ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(SupplierData._SUPPLIER_EQUIPMENT_DETAIL).NewRow()
            With dr
                .Item(SupplierData.SPPLR_EQPMNT_DTL_ID) = bv_i64SupplierEqpmntDetailId
                .Item(SupplierData.SPPLR_ID) = bv_i64SupplierId
                .Item(SupplierData.SPPLR_CNTRCT_DTL_ID) = bv_i64SupplierContractDetailId
                .Item(SupplierData.EQPMNT_NO) = bv_strEqpmntNo
                .Item(SupplierData.EQPMNT_TYP_ID) = bv_i64EqpmntTypId
                If bv_i64EqpmntCodeId <> 0 Then
                    .Item(SupplierData.EQPMNT_CD_ID) = bv_i64EqpmntCodeId
                Else
                    .Item(SupplierData.EQPMNT_CD_ID) = DBNull.Value
                End If
            End With
            UpdateSupplierEquipmentDetail = objData.UpdateRow(dr, Supplier_Equipment_DetailUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetContractRefNo()"

    Public Function pub_GetContractRefNo(ByVal bv_strContractRefNo As String, ByVal bv_DepotId As Integer, ByRef br_SupplierCode As String) As SupplierDataSet
        Try
            objData = New DataObjects(ValidateContractRefQuery, SupplierData.CNTRCT_RFRNC_NO, bv_strContractRefNo)
            objData.Fill(CType(ds, DataSet), SupplierData._SUPPLIER_CONTRACT_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getSupplierEquipmentDetails() TABLE NAME:SUPPLIER_CONTRACT_DETAIL"

    Public Function getSupplierEquipmentDetails(ByVal bv_i64ContractID As Int64) As SupplierDataSet
        Try
            objData = New DataObjects(SUPPLIER_EqpmntDetailSelectQuery, SupplierData.SPPLR_CNTRCT_DTL_ID, (bv_i64ContractID))
            objData.Fill(CType(ds, DataSet), SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentInfoDetails() TABLE NAME:EQUIPMENT_INFORMATION"

    Public Function GetEquipmentInfoDetails(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(SupplierData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(SupplierData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(EquipmentInfoSelectQuery, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetContractNoDetail()"

    Public Function pub_GetContractNoDetail(ByVal bv_strCntrct_dtl_id As Integer, ByVal bv_eqpmnt_no As String, ByVal bv_DepotId As Integer) As SupplierDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(SupplierData.SPPLR_CNTRCT_DTL_ID, bv_strCntrct_dtl_id)
            hshParameters.Add(SupplierData.EQPMNT_NO, bv_eqpmnt_no)
            hshParameters.Add(SupplierData.DPT_ID, bv_DepotId)
            objData = New DataObjects(GetContractDetailsQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), SupplierData._SUPPLIER_CNTRACT_DTL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCountFromRentalEntry"
    Public Function GetCountFromRentalEntry(ByVal bv_strEquipmentNo As String, _
                                     ByVal bv_intDepotID As Integer, _
                                     ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(SupplierData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(SupplierData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(RentalEntryCountQuery, hshTable)
            Return objData.ExecuteScalar(br_ObjTransactions)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "getSupplierEquipment()"

    Public Function getSupplierEquipment(ByVal bv_strdtl_id As Integer) As SupplierDataSet
        Try
            Dim hshParameters As New Hashtable()
            objData = New DataObjects(SupplierDetailsQuery, SupplierData.SPPLR_ID, bv_strdtl_id)
            objData.Fill(CType(ds, DataSet), SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRentalEquipment"
    Public Function GetRentalEquipment(ByVal bv_DepotId As Integer) As SupplierDataSet
        Try
            objData = New DataObjects(RentalEntrySelectQuery, SupplierData.DPT_ID, bv_DepotId)
            objData.Fill(CType(ds, DataSet), SupplierData._V_RENTAL_ENTRY)
            Return ds

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class
