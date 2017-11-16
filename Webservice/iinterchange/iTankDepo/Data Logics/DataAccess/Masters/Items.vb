#Region " Items.vb"
'*********************************************************************************************************************
'Name :
'      Items.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Items.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 4:20:45 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Items"

Public Class Items

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const ItemSelectQueryByITM_CD As String = "SELECT ITM_ID,ITM_CD,ITM_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Item WHERE ITM_CD=@ITM_CD"
    Private Const ItemSelectQueryByDPT_ID As String = "SELECT ITM_ID,ITM_CD,ITM_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Item WHERE DPT_ID=@DPT_ID"
    Private Const ItemInsertQuery As String = "INSERT INTO ITEM (ITM_ID,ITM_CD,ITM_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@ITM_ID,@ITM_CD,@ITM_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const ItemUpdateQuery As String = "UPDATE ITEM SET ITM_ID=@ITM_ID, ITM_CD=@ITM_CD, ITM_DSCRPTN_VC=@ITM_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE ITM_ID=@ITM_ID"
    Private Const V_ItemSelectQueryByItemCode As String = "SELECT ITM_ID,ITM_CD,ITM_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM ITEM WHERE ITM_CD=@ITM_CD"
    Private Const V_SubItemSelectQueryByItemCode As String = "SELECT SB_ITM_ID,SB_ITM_CD,SB_ITM_DSCRPTN_VC,ITM_ID,ITM_CD,ACTV_BT FROM V_SUB_ITEM WHERE SB_ITM_CD=@SB_ITM_CD AND ITM_ID=@ITM_ID"
    Private Const V_SubItemSelectQueryByItemID As String = "SELECT SB_ITM_ID,SB_ITM_CD,SB_ITM_DSCRPTN_VC,ITM_ID,ITM_CD,ACTV_BT FROM V_SUB_ITEM WHERE ITM_ID=@ITM_ID"
    Private Const Sub_ItemInsertQuery As String = "INSERT INTO SUB_ITEM(SB_ITM_ID,SB_ITM_CD,SB_ITM_DSCRPTN_VC,ITM_ID,ACTV_BT)VALUES(@SB_ITM_ID,@SB_ITM_CD,@SB_ITM_DSCRPTN_VC,@ITM_ID,@ACTV_BT)"
    Private Const Sub_ItemUpdateQuery As String = "UPDATE SUB_ITEM SET SB_ITM_ID=@SB_ITM_ID, SB_ITM_CD=@SB_ITM_CD, SB_ITM_DSCRPTN_VC=@SB_ITM_DSCRPTN_VC,ITM_ID=@ITM_ID, ACTV_BT=@ACTV_BT WHERE SB_ITM_ID=@SB_ITM_ID"
    Private Const Sub_ItemDeleteQuery As String = "DELETE FROM SUB_ITEM WHERE SB_ITM_ID=@SB_ITM_ID"

    Private ds As ItemDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New ItemDataSet
    End Sub

#End Region

#Region "GetItem() TABLE NAME:Item"

    Public Function GetItem(ByVal bv_intDepotID As Integer) As ItemDataSet
        Try
            objData = New DataObjects(ItemSelectQueryByDPT_ID, ItemData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), ItemData._ITEM)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetItemByItemCode() TABLE NAME:UNIT"

    Public Function GetItemByItemCode(ByVal bv_strItemCode As String, ByVal bv_intDepotID As Integer) As ItemDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(ItemData.DPT_ID, bv_intDepotID)
            hshParameters.Add(ItemData.ITM_CD, bv_strItemCode)
            objData = New DataObjects(ItemSelectQueryByITM_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), ItemData._ITEM)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateItem() TABLE NAME:Item"

    Public Function CreateItem(ByVal bv_strItemCode As String, _
                               ByVal bv_strItemDescription As String, _
                               ByVal bv_strCreatedBy As String, _
                               ByVal bv_datCreatedDate As DateTime, _
                               ByVal bv_strModifiedBy As String, _
                               ByVal bv_datModifiedDate As DateTime, _
                               ByVal bv_blnActiveBit As Boolean, _
                               ByVal bv_intDepotID As Integer, _
                               ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ItemData._ITEM).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ItemData._ITEM, br_objTrans)
                .Item(ItemData.ITM_ID) = intMax
                .Item(ItemData.ITM_CD) = bv_strItemCode
                If bv_strItemDescription <> Nothing Then
                    .Item(ItemData.ITM_DSCRPTN_VC) = bv_strItemDescription
                Else
                    .Item(ItemData.ITM_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(ItemData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(ItemData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(ItemData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(ItemData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(ItemData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(ItemData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(ItemData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(ItemData.MDFD_DT) = DBNull.Value
                End If
                .Item(ItemData.ACTV_BT) = bv_blnActiveBit
                .Item(ItemData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, ItemInsertQuery, br_objTrans)
            dr = Nothing
            CreateItem = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateItem() TABLE NAME:ITEM"

    Public Function UpdateItem(ByVal bv_i64ItemId As Int64, _
                               ByVal bv_strItemCode As String, _
                               ByVal bv_strItemDescription As String, _
                               ByVal bv_strModifiedBy As String, _
                               ByVal bv_datModifiedDate As DateTime, _
                               ByVal bv_blnActiveBit As Boolean, _
                               ByVal bv_intDepotID As Integer, _
                               ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ItemData._ITEM).NewRow()
            With dr
                .Item(ItemData.ITM_ID) = bv_i64ItemId
                .Item(ItemData.ITM_CD) = bv_strItemCode
                If bv_strItemDescription <> Nothing Then
                    .Item(ItemData.ITM_DSCRPTN_VC) = bv_strItemDescription
                Else
                    .Item(ItemData.ITM_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(ItemData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(ItemData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(ItemData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(ItemData.MDFD_DT) = DBNull.Value
                End If
                .Item(ItemData.ACTV_BT) = bv_blnActiveBit
                .Item(ItemData.DPT_ID) = bv_intDepotID
            End With
            UpdateItem = objData.UpdateRow(dr, ItemUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetItemByItemCode() TABLE NAME:ITEM"

    Public Function pub_GetItemByItemCode(ByVal bv_strItemCode As String) As ItemDataSet
        Try
            objData = New DataObjects(V_ItemSelectQueryByItemCode, ItemData.ITM_CD, bv_strItemCode)
            objData.Fill(CType(ds, DataSet), ItemData._ITEM)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetSubItemByItemCode() TABLE NAME:SUB_ITEM"

    Public Function pub_GetSubItemByItemCode(ByVal bv_strSubItemCode As String, ByVal bv_strItemID As String) As ItemDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(ItemData.SB_ITM_CD, bv_strSubItemCode)
            hshParameters.Add(ItemData.ITM_ID, bv_strItemID)
            objData = New DataObjects(V_SubItemSelectQueryByItemCode, hshParameters)
            objData.Fill(CType(ds, DataSet), ItemData._V_SUB_ITEM)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetSubItemByItemID() TABLE NAME:SUB_ITEM"

    Public Function pub_GetSubItemByItemID(ByVal bv_i64ItemId As Int64) As ItemDataSet
        Try
            objData = New DataObjects(V_SubItemSelectQueryByItemID, ItemData.ITM_ID, bv_i64ItemId)
            objData.Fill(CType(ds, DataSet), ItemData._SUB_ITEM)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateSub_Item() TABLE NAME:SUB_ITEM"

    Public Function CreateSub_Item(ByVal bv_strSubItemCode As String, _
                                   ByVal bv_strSubItemDescription As String, _
                                   ByVal bv_i64ItemId As Int64, _
                                   ByVal bv_blnActiveBit As Boolean, _
                                   ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ItemData._SUB_ITEM).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ItemData._SUB_ITEM, br_ObjTransactions)
                .Item(ItemData.SB_ITM_ID) = intMax
                .Item(ItemData.SB_ITM_CD) = bv_strSubItemCode
                .Item(ItemData.SB_ITM_DSCRPTN_VC) = bv_strSubItemDescription
                .Item(ItemData.ITM_ID) = bv_i64ItemId
                .Item(ItemData.ACTV_BT) = bv_blnActiveBit
            End With
            objData.InsertRow(dr, Sub_ItemInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateSub_Item = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateSub_Item() TABLE NAME:SUB_ITEM"

    Public Function UpdateSub_Item(ByVal bv_i64SubItemId As Int64, _
                                   ByVal bv_strSubItemCode As String, _
                                   ByVal bv_strSubItemDescription As String, _
                                   ByVal bv_i64ItemId As Int64, _
                                   ByVal bv_blnActiveBit As Boolean, _
                                   ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ItemData._SUB_ITEM).NewRow()
            With dr
                .Item(ItemData.SB_ITM_ID) = bv_i64SubItemId
                .Item(ItemData.SB_ITM_CD) = bv_strSubItemCode
                .Item(ItemData.SB_ITM_DSCRPTN_VC) = bv_strSubItemDescription
                .Item(ItemData.ITM_ID) = bv_i64ItemId
                .Item(ItemData.ACTV_BT) = bv_blnActiveBit
            End With
            UpdateSub_Item = objData.UpdateRow(dr, Sub_ItemUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "DeleteSub_Item() TABLE NAME:SUB_ITEM"

    Public Function DeleteSub_Item(ByVal bvSubItemID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(ItemData._SUB_ITEM).NewRow()
            With dr
                .Item(ItemData.SB_ITM_ID) = bvSubItemID
            End With
            DeleteSub_Item = objData.DeleteRow(dr, Sub_ItemDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class

#End Region
