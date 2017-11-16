Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
Imports iInterchange.iTankDepo.GatewayFramework
Public Class Supplier
#Region "pub_SupplierContractDetailGetSupplierContractDetailBySupplierId() TABLE NAME:SUPPLIER_CONTRACT_DETAIL"

    <OperationContract()> _
    Public Function pub_GetSupplierContractDetails(ByVal bv_strSupplierId As Int64) As SupplierDataSet

        Try
            Dim dsSupplierContractDetailData As SupplierDataSet
            Dim objSupplierContractDetails As New Suppliers
            dsSupplierContractDetailData = objSupplierContractDetails.pub_GetSupplierContractDetails(bv_strSupplierId)
            Return dsSupplierContractDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_VSupplierEquipmentDetailGetVSupplierEquipmentDetailBy() TABLE NAME:V_SUPPLIER_EQUIPMENT_DETAIL"

    <OperationContract()> _
    Public Function GetVSupplierEquipmentDetailBySupplierId(ByVal bv_strSupplierId As Int64, ByVal bv_ContractId As Int64) As SupplierDataSet

        Try
            Dim dsVSupplierEquipmentDetailData As SupplierDataSet
            Dim objVSupplierEquipmentDetails As New Suppliers
            dsVSupplierEquipmentDetailData = objVSupplierEquipmentDetails.GetVSupplierEquipmentDetailBySupplierId(bv_strSupplierId, bv_ContractId)
            Return dsVSupplierEquipmentDetailData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_CreateSupplier() "
    <OperationContract()> _
    Public Function pub_CreateSupplier(ByVal Code As String, _
                                       ByVal Description As String, _
                                       ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_blnActiveBit As Boolean, _
                                       ByVal bv_i32DepotID As Int32, _
                                       ByVal bv_strWfData As String, _
                                       ByRef br_dsSupplier As SupplierDataSet) As Long
        Dim objTransaction As New Transactions()

        Try
            Dim objSuppliers As New Suppliers
            Dim objCommonUI As New CommonUIs
            Dim lngCreated As Long
            lngCreated = objSuppliers.CreateSupplier(Code, _
                                                    Description, _
                                                    bv_blnActiveBit, _
                                                    bv_strModifiedBy, _
                                                    bv_datModifiedDate, _
                                                    bv_strModifiedBy, _
                                                    bv_datModifiedDate, _
                                                    bv_i32DepotID, _
                                                    objTransaction)
            pub_UpdateSupplierDetail(br_dsSupplier, CLng(lngCreated), bv_strModifiedBy, bv_datModifiedDate, bv_i32DepotID, objTransaction)
            'br_dsSupplier.Tables(SupplierData._SUPPLIER).Rows(0).Item(SupplierData.SPPLR_ID) = lngCreated
            objTransaction.commit()
            Return lngCreated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function
#End Region

#Region "pub_UpdateSupplierDetail()"
    <OperationContract()> _
    Public Function pub_UpdateSupplierDetail(ByRef dsSupplier As SupplierDataSet, _
                                           ByVal bv_SupplierID As Int64, _
                                           ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_depotId As Integer, _
                                           ByRef br_ObjTransactions As Transactions) As Boolean


        Try
            Dim dtSupplierContract As DataTable
            Dim ObjSupplier As New Suppliers
            Dim bolupdatebt As Boolean
            Dim lngCreated As Long
            dtSupplierContract = dsSupplier.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL)
            For Each drContract As DataRow In dtSupplierContract.Rows
                Select Case drContract.RowState
                    Case DataRowState.Added
                        lngCreated = ObjSupplier.CreateSupplierContract(bv_SupplierID, _
                                                                            (drContract.Item(SupplierData.CNTRCT_RFRNC_NO).ToString), _
                                                                             CDate(drContract.Item(SupplierData.CNTRCT_STRT_DT)), _
                                                                             CDate(drContract.Item(SupplierData.CNTRCT_END_DT)), _
                                                                             CommonUIs.iDbl(drContract.Item(SupplierData.RNTL_PR_DY)), _
                                                                             (drContract.Item(SupplierData.RMRKS_VC).ToString), _
                                                                             br_ObjTransactions)

                        pub_UpdateEquipmentDetails(dsSupplier, bv_SupplierID, lngCreated, CommonUIs.iLng(drContract.Item(SupplierData.SPPLR_CNTRCT_DTL_ID)), bv_strModifiedBy, bv_datModifiedDate, bv_depotId, br_ObjTransactions)
                        drContract.Item(SupplierData.SPPLR_CNTRCT_DTL_ID) = lngCreated
                    Case DataRowState.Modified
                        bolupdatebt = ObjSupplier.UpdateSupplierContract(CommonUIs.iLng(drContract.Item(SupplierData.SPPLR_CNTRCT_DTL_ID)), _
                                                                              bv_SupplierID, _
                                                                              (drContract.Item(SupplierData.CNTRCT_RFRNC_NO).ToString), _
                                                                              CDate(drContract.Item(SupplierData.CNTRCT_STRT_DT)), _
                                                                              CDate(drContract.Item(SupplierData.CNTRCT_END_DT)), _
                                                                              CommonUIs.iDbl(drContract.Item(SupplierData.RNTL_PR_DY)), _
                                                                              (drContract.Item(SupplierData.RMRKS_VC).ToString), _
                                                                              br_ObjTransactions)

                        pub_UpdateEquipmentDetails(dsSupplier, bv_SupplierID, lngCreated, CommonUIs.iLng(drContract.Item(SupplierData.SPPLR_CNTRCT_DTL_ID)), bv_strModifiedBy, bv_datModifiedDate, bv_depotId, br_ObjTransactions)
                        lngCreated = CommonUIs.iLng(drContract.Item(SupplierData.SPPLR_CNTRCT_DTL_ID))
                    Case DataRowState.Deleted
                        'pub_UpdateEquipmentDetails(dsSupplier, bv_SupplierID, lngCreated, CommonUIs.iLng(drContract.Item(SupplierData.SPPLR_CNTRCT_DTL_ID)), bv_strModifiedBy, bv_datModifiedDate, bv_depotId, br_ObjTransactions)
                        ObjSupplier.DeleteEquipmentDetail(CommonUIs.iLng(drContract.Item(SupplierData.SPPLR_CNTRCT_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                        ObjSupplier.DeleteContractDetails(CommonUIs.iLng(drContract.Item(SupplierData.SPPLR_CNTRCT_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                        'ObjSupplier.DeleteEquipmentInformation((drContract.Item(SupplierData.EQPMNT_NO).ToString), _
                        '                                                         bv_depotId, _
                        '                                                         br_ObjTransactions)
                    Case DataRowState.Unchanged
                        lngCreated = CommonUIs.iLng(drContract.Item(SupplierData.SPPLR_CNTRCT_DTL_ID))
                        pub_UpdateEquipmentDetails(dsSupplier, bv_SupplierID, lngCreated, CommonUIs.iLng(drContract.Item(SupplierData.SPPLR_CNTRCT_DTL_ID)), bv_strModifiedBy, bv_datModifiedDate, bv_depotId, br_ObjTransactions)
                End Select
            Next

            Return bolupdatebt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateEquipmentDetails()"
    <OperationContract()> _
    Public Function pub_UpdateEquipmentDetails(ByRef br_dsSupplier As SupplierDataSet, _
                                            ByVal bv_SupplierID As Int64, _
                                            ByVal bv_ContractID As Int64, _
                                            ByVal bv_oldContractID As Int64, _
                                            ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_intDepotId As Integer, _
                                            ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dtEquipmentDetails As DataTable
            Dim ObjSuppliers As New Suppliers
            Dim bolupdatebt As Boolean
            Dim intEpqType As Integer
            Dim intEpqCode As Integer
            Dim objEqpmntInfo As New EquipmentInformations
            dtEquipmentDetails = br_dsSupplier.Tables(SupplierData._V_SUPPLIER_EQUIPMENT_DETAIL)
            For Each drStorage As DataRow In dtEquipmentDetails.Select(String.Concat(SupplierData.SPPLR_CNTRCT_DTL_ID, "=", bv_oldContractID))
                If Not ((drStorage.Item(SupplierData.EQPMNT_TYP_ID)) Is DBNull.Value) Then
                    intEpqType = CInt(drStorage.Item(SupplierData.EQPMNT_TYP_ID))
                Else
                    intEpqType = vbEmpty

                End If
                If Not ((drStorage.Item(SupplierData.EQPMNT_TYP_ID)) Is DBNull.Value) Then
                    intEpqCode = CInt(drStorage.Item(SupplierData.EQPMNT_TYP_ID))
                Else
                    intEpqCode = vbEmpty

                End If

                Select Case drStorage.RowState
                    Case DataRowState.Added
                        If (bv_ContractID <> 0) Then
                            Dim lngCreated As Long = ObjSuppliers.CreateSupplierEquipmentDetail(bv_SupplierID, _
                                                                                                bv_ContractID, _
                                                                                               (drStorage.Item(SupplierData.EQPMNT_NO).ToString), _
                                                                                               intEpqType, _
                                                                                                intEpqCode, _
                                                                                                br_ObjTransactions)
                        End If
                        drStorage.Item(SupplierData.SPPLR_ID) = bv_SupplierID

                        Dim intEquipCount As Integer = 0
                        intEquipCount = CInt(ObjSuppliers.GetEquipmentInfo((drStorage.Item(SupplierData.EQPMNT_NO).ToString), _
                                                                           bv_intDepotId, br_ObjTransactions))

                        If intEquipCount > 0 Then
                            ObjSuppliers.UpdateEquipment_Info((drStorage.Item(SupplierData.EQPMNT_NO).ToString), _
                                                           intEpqType, _
                                                           bv_intDepotId, _
                                                           True, _
                                                           br_ObjTransactions)

                        Else

                            objEqpmntInfo.CreateEquipmentInformation((drStorage.Item(SupplierData.EQPMNT_NO).ToString), _
                                                             intEpqType, _
                                                             Nothing, _
                                                             Nothing, _
                                                             Nothing, _
                                                             Nothing, _
                                                             bv_strModifiedBy, _
                                                             Now, _
                                                             bv_strModifiedBy, _
                                                             Now, _
                                                             bv_intDepotId, _
                                                             True, _
                                                             Nothing, _
                                                             Nothing, _
                                                             0, _
                                                             Nothing, _
                                                             0, _
                                                             Nothing, _
                                                             True, _
                                                             Nothing, _
                                                             br_ObjTransactions)
                        End If

                        'drStorage.Item(SupplierData.SPPLR_EQPMNT_DTL_ID) = lngCreated

                    Case DataRowState.Modified
                        bolupdatebt = ObjSuppliers.UpdateSupplierEquipmentDetail(CommonUIs.iLng(drStorage.Item(SupplierData.SPPLR_EQPMNT_DTL_ID)), _
                                                                            bv_SupplierID, _
                                                                            bv_ContractID, _
                                                                            (drStorage.Item(SupplierData.EQPMNT_NO).ToString), _
                                                                            intEpqType, _
                                                                            intEpqCode, _
                                                                            br_ObjTransactions)
                        ObjSuppliers.UpdateEquipmentInformation((drStorage.Item(SupplierData.EQPMNT_NO).ToString), _
                                                                   intEpqType, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    bv_strModifiedBy, _
                                                                    Now, _
                                                                    bv_intDepotId, _
                                                                    True, _
                                                                    br_ObjTransactions)

                    Case DataRowState.Deleted
                        ObjSuppliers.DeleteEqpmntDetails(CInt(drStorage.Item(SupplierData.SPPLR_EQPMNT_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                        ObjSuppliers.DeleteEquipmentInformation((drStorage.Item(SupplierData.EQPMNT_NO).ToString), _
                                                                                    bv_intDepotId, _
                                                                                    br_ObjTransactions)
                End Select
            Next

            For Each drStorage As DataRow In dtEquipmentDetails.Rows
                Select Case drStorage.RowState
                    Case DataRowState.Deleted
                        Dim intCount As Integer
                        intCount = CInt(ObjSuppliers.GetCountFromRentalEntry(CStr((drStorage.Item(SupplierData.EQPMNT_NO, DataRowVersion.Original))), bv_intDepotId, _
                                                                        br_ObjTransactions))
                        If intCount = 0 Then
                            ObjSuppliers.DeleteEqpmntDetails(CInt(drStorage.Item(SupplierData.SPPLR_EQPMNT_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                            ObjSuppliers.DeleteEquipmentInformation(CStr((drStorage.Item(SupplierData.EQPMNT_NO, DataRowVersion.Original))), _
                                                                                      bv_intDepotId, _
                                                                                      br_ObjTransactions)
                        End If
                        
                End Select
            Next

            Return bolupdatebt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ValidateEquipmentNoByDepotID"
    <OperationContract()> _
    Public Function pub_ValidateEquipmentNoByDepotID(ByVal bv_strEquipmentNo As String, ByVal SupplierContractId As Integer, ByVal bv_intDepotID As Int32) As Boolean

        Try
            Dim objEqpNo As New Suppliers
            Dim intRowCount As Integer
            intRowCount = CInt(objEqpNo.GetEquipmentInformationByID(bv_strEquipmentNo, SupplierContractId, bv_intDepotID))
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

#Region "pubValidateEquipment"
    <OperationContract()> _
    Public Function pubValidateEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32) As Boolean
        Try
            Dim objEqpNo As New Suppliers
            Dim intRowCount As Integer
            intRowCount = CInt(objEqpNo.GetEquipmentInfoDetails(bv_strEquipmentNo, bv_intDepotID))
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

#Region "pub_ModifySupplier()"

    <OperationContract()> _
    Public Function pub_ModifySupplier(ByVal bv_i64SupplierID As Int64, _
                                       ByVal Code As String, _
                                       ByVal Description As String, _
                                       ByVal bv_strModifiedBy As String, _
                                       ByVal bv_datModifiedDate As DateTime, _
                                       ByVal bv_blnActiveBit As Boolean, _
                                       ByVal bv_i32DepotID As Int32, _
                                       ByVal bv_strWfData As String, _
                                       ByRef br_dsSupplier As SupplierDataSet) As Boolean
        Dim objTransaction As New Transactions()
        Try
            Dim objSupplier As New Suppliers
            Dim blnUpdated As Boolean
            blnUpdated = objSupplier.UpdateSupplier(bv_i64SupplierID, _
                                                    Code, _
                                                    Description, _
                                                    bv_blnActiveBit, _
                                                    bv_strModifiedBy, _
                                                    bv_datModifiedDate, _
                                                    bv_i32DepotID, _
                                                    objTransaction)
            pub_UpdateSupplierDetail(br_dsSupplier, CLng(bv_i64SupplierID), bv_strModifiedBy, bv_datModifiedDate, bv_i32DepotID, objTransaction)
            objTransaction.commit()
            Return blnUpdated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function

#End Region

#Region "VALIDATE :pub_GetSupplierCode "
    <OperationContract()> _
    Public Function pub_GetSupplierCode(ByVal bv_strSupplierCode As String, ByVal bv_DepotId As Integer) As Boolean
        Dim dsSupplierdataset As SupplierDataSet
        Dim objSuppliers As New Suppliers
        Try
            dsSupplierdataset = objSuppliers.pub_GetSupplierCode(bv_strSupplierCode, bv_DepotId)
            If dsSupplierdataset.Tables(SupplierData._SUPPLIER).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "VALIDATE :pub_GetContractRefNo "
    <OperationContract()> _
    Public Function pub_GetContractRefNo(ByVal bv_strContractRefNo As String, ByVal bv_DepotId As Integer, ByRef br_SupplierCode As String) As Boolean
        Dim dsSupplierdataset As SupplierDataSet
        Dim objSuppliers As New Suppliers
        Try
            dsSupplierdataset = objSuppliers.pub_GetContractRefNo(bv_strContractRefNo, bv_DepotId, br_SupplierCode)
            If dsSupplierdataset.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).Rows.Count > 0 Then
                br_SupplierCode = CStr(dsSupplierdataset.Tables(SupplierData._SUPPLIER_CONTRACT_DETAIL).Rows(0).Item(SupplierData.SPPLR_CD))
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#Region "getSupplierEquipmentDetails() TABLE NAME:SUPPLIER_CONTRACT_DETAIL"

    <OperationContract()> _
    Public Function getSupplierEquipmentDetails(ByVal bv_ContractID As Int64) As SupplierDataSet

        Try
            Dim dsSupplierContractDetailData As SupplierDataSet
            Dim objSupplierContractDetails As New Suppliers
            dsSupplierContractDetailData = objSupplierContractDetails.getSupplierEquipmentDetails(bv_ContractID)
            Return dsSupplierContractDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
#End Region

#Region "pub_GetContractNoDetail"
    <OperationContract()> _
    Public Function pub_GetContractNoDetail(ByVal bv_Supplr_cntrct_id As Integer, ByVal bv_Eqpmnt_no As String, ByVal intDptId As Integer) As SupplierDataSet
        Try
            Dim dsSupplierContractDetailData As SupplierDataSet
            Dim objSupplierContractDetails As New Suppliers
            dsSupplierContractDetailData = objSupplierContractDetails.pub_GetContractNoDetail(bv_Supplr_cntrct_id, bv_Eqpmnt_no, intDptId)
            Return dsSupplierContractDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "GetEquipmentDetail"
    <OperationContract()> _
    Public Function getSupplierEquipment(ByVal bv_Supplr_id As Integer) As SupplierDataSet
        Try
            Dim dsSupplierDetailData As SupplierDataSet
            Dim objSupplieDetails As New Suppliers
            dsSupplierDetailData = objSupplieDetails.getSupplierEquipment(bv_Supplr_id)
            Return dsSupplierDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "GetRentalEquipment"
    <OperationContract()> _
    Public Function GetRentalEquipment(ByVal bv_DepotID As Integer) As SupplierDataSet
        Try
            Dim dsSupplierDetailData As SupplierDataSet
            Dim objSupplieDetails As New Suppliers
            dsSupplierDetailData = objSupplieDetails.GetRentalEquipment(bv_DepotID)
            Return dsSupplierDetailData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region
End Class