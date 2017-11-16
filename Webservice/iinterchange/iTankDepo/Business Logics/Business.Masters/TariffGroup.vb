Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class TariffGroup
#Region "pub_TariffGroupGetTariffGroup() TABLE NAME:Tariff_Group"

    <OperationContract()> _
    Public Function pub_TariffGroupGetTariffGroup(ByVal bv_i32TariffGroupID As Int64) As TariffGroupData

        Try
            Dim dsTariffGroupData As TariffGroupData
            Dim objTariffGroups As New TariffGroups
            'dsTariffGroupData = objTariffGroups.GetTariff_GroupByTariffGroupID(bv_i32TariffGroupID)
            Return dsTariffGroupData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_TariffGroupCreateTariffGroup() TABLE NAME:Tariff_Group"

    <OperationContract()> _
    Public Function pub_TariffGroupCreateTariffGroup(ByVal bv_strTariffGroupCode As String, _
         ByVal bv_strTariffGroupDescription As String, _
         ByVal bv_strCreatedBy As String, _
         ByVal bv_datCreatedDate As DateTime, _
         ByVal bv_strModifiedBy As String, _
         ByVal bv_datModifiedDate As DateTime, _
         ByVal bv_i32DepotId As Int32, _
         ByVal bv_blnActiveBit As Boolean, _
         ByVal bv_strWfData As String, _
         ByRef br_dsTariffGroupDataset As TariffGroupDataSet) As Long
        Dim objTransaction As New Transactions()
        Try
            Dim objTariff_Group As New TariffGroups
            Dim lngCreated As Long
            lngCreated = objTariff_Group.CreateTariffGroup(bv_strTariffGroupCode, _
                  bv_strTariffGroupDescription, bv_strCreatedBy, _
                  bv_datCreatedDate, bv_strModifiedBy, _
                  bv_datModifiedDate, bv_blnActiveBit, _
                  bv_i32DepotId, objTransaction)
            pub_UpdateTariffGroupDetail(br_dsTariffGroupDataset, CInt(lngCreated), objTransaction)
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

#Region "pub_UpdateBankDetails"
    <OperationContract()> _
    Public Function pub_UpdateTariffGroupDetail(ByRef dsTariffGroupDataset As TariffGroupDataSet, _
                                         ByVal tariffGroupID As Integer, ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dtTariffGroup As DataTable
            Dim ObjTariffGroups As New TariffGroups
            Dim objTransaction As New Transactions()
            dtTariffGroup = dsTariffGroupDataset.Tables(TariffGroupData._TARIFF_GROUP_DETAIL)
            For Each drTariffGroup As DataRow In dtTariffGroup.Rows
                Select Case drTariffGroup.RowState
                    Case DataRowState.Added
                        Dim lngCreated As Long = ObjTariffGroups.CreateTariffDetail(tariffGroupID, _
                                                                                    CInt(drTariffGroup.Item(TariffGroupData.TRFF_CD_ID)), _
                                                                                    CBool(drTariffGroup.Item(TariffGroupData.ACTV_BT)), _
                                                                                    br_ObjTransactions)
                        drTariffGroup.Item(TariffGroupData.TRFF_GRP_DTL_ID) = lngCreated
                    Case DataRowState.Modified
                        ObjTariffGroups.UpdateTariffDetail(CInt(drTariffGroup.Item(TariffGroupData.TRFF_GRP_DTL_ID)), _
                                                                                    CInt(drTariffGroup.Item(TariffGroupData.TRFF_CD_ID)), _
                                                                                    tariffGroupID, _
                                                                                    CBool(drTariffGroup.Item(TariffGroupData.ACTV_BT)), _
                                                                                    br_ObjTransactions)
                        'drTariffGroup.Item(TariffGroupData.TRFF_GRP_DTL_ID) = lngCreated
                        'Case DataRowState.Deleted
                        '    ObjTariffGroups.DeleteTariffDetail(CInt(drTariffGroup.Item(TariffGroupData.TRFF_GRP_DTL_ID, DataRowVersion.Original)), br_ObjTransactions)
                End Select
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_TariffGroupModifyTariffGroupTariff_Group() TABLE NAME:Tariff_Group"

    <OperationContract()> _
    Public Function pub_TariffGroupModifyTariff_Group(ByVal bv_i64TariffGroupID As Int64, _
         ByVal bv_strTariffGroupCode As String, _
         ByVal bv_strTariffGroupDescription As String, _
         ByVal bv_strModifiedBy As String, _
         ByVal bv_datModifiedDate As DateTime, _
         ByVal bv_blnActiveBit As Boolean, _
         ByVal bv_i32DepotId As Int32, _
         ByVal bv_strWfData As String, ByRef br_dsTariffGroupDataset As TariffGroupDataSet) As Boolean
        Dim objTransaction As New Transactions()
        Try
            Dim objTariff_Group As New TariffGroups
            Dim blnUpdated As Boolean
            blnUpdated = objTariff_Group.UpdateTariffGroup(bv_i64TariffGroupID, _
                                    bv_strTariffGroupCode, bv_strTariffGroupDescription, _
                                    bv_strModifiedBy, bv_datModifiedDate, _
                                    bv_blnActiveBit, bv_i32DepotId, objTransaction)
            pub_UpdateTariffGroupDetail(br_dsTariffGroupDataset, CInt(bv_i64TariffGroupID), objTransaction)
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

#Region "VALIDATE : pub_ValidatePKDepotr() TABLE NAME:DEPOT"
    ''' <summary>
    ''' This method is used to validate the User Name
    ''' </summary>
    ''' <param name="bv_strUserName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_ValidatePKTariffGroupCode(ByVal bv_strTariffGroupCode As String) As Boolean
        Dim dsTariffGroupDataSet As TariffGroupDataSet
        Dim objTariffGroups As New TariffGroups
        Try
            dsTariffGroupDataSet = objTariffGroups.GetTARIFFGROUPByCD(bv_strTariffGroupCode)
            If dsTariffGroupDataSet.Tables(TariffGroupData._TARIFF_GROUP).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetTariffGroupDetail()"
    <OperationContract()> _
    Public Function pub_GetTariffGroupDetail(ByVal bv_i32DPT_ID As Int32) As TariffGroupDataSet

        Try
            Dim dsTariffGroupDataSet As TariffGroupDataSet
            Dim objTariffGroups As New TariffGroups
            dsTariffGroupDataSet = objTariffGroups.GetTariffGroupDetail(bv_i32DPT_ID)
            Return (dsTariffGroupDataSet)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetTariffGroupDetailbyGroupID()"
    <OperationContract()> _
    Public Function pub_GetTariffGroupDetailbyGroupID(ByVal bv_i32TariffGroup_ID As Int32) As TariffGroupDataSet

        Try
            Dim dsTariffGroupDataSet As TariffGroupDataSet
            Dim objTariffGroups As New TariffGroups
            dsTariffGroupDataSet = objTariffGroups.GetTariffGroupDetailID(bv_i32TariffGroup_ID)
            Return (dsTariffGroupDataSet)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class