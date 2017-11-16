#Region " Item.vb"
'*********************************************************************************************************************
'Name :
'      Item.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Item.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 11:15:02 AM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class Item
    Inherits CodeBase

#Region "pub_GetCodeMaster() TABLE NAME:ITEM"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsItemData As ItemDataSet
            Dim objItems As New Items
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsItemData = objItems.GetItem(intDepotID)
            Return dsItemData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ValidatePK() TABLE NAME:ITEM"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strItemCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsItemData As ItemDataSet
            Dim objItems As New Items
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsItemData = objItems.GetItemByItemCode(bv_strItemCode, intDepotID)
            If dsItemData.Tables(ItemData._ITEM).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateItem() TABLE NAME:ITEM"

    <OperationContract()> _
    Public Function pub_CreateItem(ByVal bv_strItemCode As String, _
                                   ByVal bv_strItemDescription As String, _
                                   ByVal bv_strCRTD_BY As String, _
                                   ByVal bv_datCRTD_DT As DateTime, _
                                   ByVal bv_strMDFD_BY As String, _
                                   ByVal bv_datMDFD_DT As DateTime, _
                                   ByVal bv_blnActiveBit As Boolean, _
                                   ByVal bv_i32DPT_ID As Int32, _
                                   ByVal bv_strWfData As String, _
                                   ByRef br_dsSubItem As ItemDataSet) As Long
        Dim ObjTransaction As New Transactions
        Try
            Dim objItem As New Items
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objItem.CreateItem(bv_strItemCode, _
                                            bv_strItemDescription, _
                                            bv_strCRTD_BY, _
                                            bv_datCRTD_DT, _
                                            bv_strMDFD_BY, _
                                            bv_datMDFD_DT, _
                                            bv_blnActiveBit, _
                                            intDepotID, _
                                            ObjTransaction)
            pub_UpdateSubItem(br_dsSubItem, CommonUIs.iLng(lngCreated), ObjTransaction)
            ObjTransaction.commit()
            Return lngCreated
        Catch ex As Exception
            ObjTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            ObjTransaction = Nothing
        End Try
    End Function

#End Region

#Region "pub_UpdateItem() TABLE NAME:ITEM"

    <OperationContract()> _
    Public Function pub_UpdateItem(ByVal bv_i64ItemId As Int64, _
                                   ByVal bv_strItemCode As String, _
                                   ByVal bv_strItemDescription As String, _
                                   ByVal bv_strMDFD_BY As String, _
                                   ByVal bv_datMDFD_DT As DateTime, _
                                   ByVal bv_blnActiveBit As Boolean, _
                                   ByVal bv_i32DepotId As Int32, _
                                   ByRef br_dsSubItem As ItemDataSet) As Boolean
        Dim ObjTransaction As New Transactions
        Try
            Dim objItem As New Items

            Dim blnUpdated As Boolean
            blnUpdated = objItem.UpdateItem(bv_i64ItemId, _
                                            bv_strItemCode, _
                                            bv_strItemDescription, _
                                            bv_strMDFD_BY, _
                                            bv_datMDFD_DT, _
                                            bv_blnActiveBit, _
                                            bv_i32DepotId, _
                                            ObjTransaction)
            pub_UpdateSubItem(br_dsSubItem, bv_i64ItemId, ObjTransaction)
            ObjTransaction.commit()
            Return blnUpdated
        Catch ex As Exception
            ObjTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            ObjTransaction = Nothing
        End Try
    End Function

#End Region

#Region "pub_GetItemByItemCode() TABLE NAME:ITEM"

    <OperationContract()> _
    Public Function pub_GetItemByItemCode(ByVal bv_strItemCode As String) As ItemDataSet
        Try
            Dim dsItemData As ItemDataSet
            Dim objItems As New Items
            dsItemData = objItems.pub_GetItemByItemCode(bv_strItemCode)
            Return dsItemData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetItemByItemCode() TABLE NAME:ITEM"

    <OperationContract()> _
    Public Function pub_GetSubItemByItemCode(ByVal bv_strSubItemCode As String, ByVal bv_strItemID As String) As ItemDataSet
        Try
            Dim dsItemData As ItemDataSet
            Dim objItems As New Items
            dsItemData = objItems.pub_GetSubItemByItemCode(bv_strSubItemCode, bv_strItemID)
            Return dsItemData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetSubItemByItemID() TABLE NAME:SUB_ITEM"

    <OperationContract()> _
    Public Function pub_GetSubItemByItemID(ByVal bv_i64ItemId As Int64) As ItemDataSet
        Try
            Dim dsSubItemData As ItemDataSet
            Dim objSubItems As New Items
            dsSubItemData = objSubItems.pub_GetSubItemByItemID(bv_i64ItemId)
            Return dsSubItemData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateSubItem"
    <OperationContract()> _
    Public Function pub_UpdateSubItem(ByRef br_dsItem As ItemDataSet, ByVal bv_i64ItemID As Int64, ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dtSubItem As DataTable
            Dim ObjItems As New Items
            Dim bolupdatebt As Boolean

            dtSubItem = br_dsItem.Tables(ItemData._SUB_ITEM)
            For Each drSubItem As DataRow In dtSubItem.Rows
                Select Case drSubItem.RowState
                    Case DataRowState.Added
                        Dim lngCreated As Long = ObjItems.CreateSub_Item(drSubItem.Item(SubItemData.SB_ITM_CD).ToString, _
                                                                         drSubItem.Item(SubItemData.SB_ITM_DSCRPTN_VC).ToString, _
                                                                         CommonUIs.iLng(bv_i64ItemID), _
                                                                         CommonUIs.iBool(drSubItem.Item(SubItemData.ACTV_BT)), _
                                                                         br_objTransaction)

                        drSubItem.Item(ItemData.SB_ITM_ID) = lngCreated
                    Case DataRowState.Modified
                        bolupdatebt = ObjItems.UpdateSub_Item(CommonUIs.iLng(drSubItem.Item(ItemData.SB_ITM_ID)), _
                                                              drSubItem.Item(ItemData.SB_ITM_CD).ToString, _
                                                              drSubItem.Item(ItemData.SB_ITM_DSCRPTN_VC).ToString, _
                                                              CommonUIs.iLng(drSubItem.Item(ItemData.ITM_ID)), _
                                                              CommonUIs.iBool(drSubItem.Item(SubItemData.ACTV_BT)), _
                                                              br_objTransaction)


                        'Case DataRowState.Deleted
                        '    ObjItems.DeleteSub_Item(CommonUIs.iLng(drSubItem.Item(ItemData.SB_ITM_ID, DataRowVersion.Original)), br_objTransaction)
                End Select
            Next

            Return bolupdatebt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class