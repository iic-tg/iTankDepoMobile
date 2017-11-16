#Region " SubItems.vb"
'*********************************************************************************************************************
'Name :
'      SubItems.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(SubItems.vb)
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
#Region "SubItems"

Public Class SubItems

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const SubItemSelectQueryBySB_ITM_CD As String = "SELECT SB_ITM_ID,SB_ITM_CD,SB_ITM_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM SUB_ITEM WHERE SB_ITM_CD=@SB_ITM_CD"
    Private Const SubItemSelectQueryByDPT_ID As String = "SELECT SB_ITM_ID,SB_ITM_CD,SB_ITM_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM SUB_ITEM WHERE DPT_ID=@DPT_ID"
    Private Const SubItemInsertQuery As String = "INSERT INTO SUB_ITEM(SB_ITM_ID,SB_ITM_CD,SB_ITM_DSCRPTN_VC,ITM_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@SB_ITM_ID,@SB_ITM_CD,@SB_ITM_DSCRPTN_VC,@ITM_ID,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const SubItemUpdateQuery As String = "UPDATE SUB_ITEM SET SB_ITM_ID=@SB_ITM_ID, SB_ITM_CD=@SB_ITM_CD, SB_ITM_DSCRPTN_VC=@SB_ITM_DSCRPTN_VC,ITM_ID=@ITM_ID, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE SB_ITM_ID=@SB_ITM_ID"
    Private Const SubItemSelectQueryByDepotID As String = "SELECT SB_ITM_ID,SB_ITM_CD,SB_ITM_DSCRPTN_VC,ITM_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,(SELECT ITM_CD FROM ITEM WHERE ITM_ID=a.ITM_ID) AS ITM_CD  FROM SUB_ITEM a WHERE DPT_ID=@DPT_ID"
    Private Const SubItemSelectQueryBySubItemCode As String = "SELECT SB_ITM_ID,SB_ITM_CD,SB_ITM_DSCRPTN_VC,ITM_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,(SELECT ITM_CD FROM ITEM WHERE ITM_ID=a.ITM_ID) AS ITM_CD  FROM SUB_ITEM a WHERE DPT_ID=@DPT_ID AND SB_ITM_CD=@SB_ITM_CD"

    Private ds As SubItemDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New SubItemDataSet
    End Sub

#End Region

#Region "GetSubItem() TABLE NAME:SubItem"

    Public Function GetSubItem(ByVal bv_intDepotID As Integer) As SubItemDataSet
        Try
            objData = New DataObjects(SubItemSelectQueryByDPT_ID, SubItemData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), SubItemData._SUB_ITEM)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetSubItemBySubItemCode() TABLE NAME:SUB_ITEM"

    Public Function GetSubItemBySubItemCode(ByVal bv_strSubItemCode As String, ByVal bv_intDepotID As Integer) As SubItemDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(SubItemData.DPT_ID, bv_intDepotID)
            hshParameters.Add(SubItemData.SB_ITM_CD, bv_strSubItemCode)
            objData = New DataObjects(SubItemSelectQueryBySB_ITM_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), SubItemData._SUB_ITEM)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateSubItem() TABLE NAME:SUB_ITEM"

    Public Function CreateSubItem(ByVal bv_strSubItemCode As String, _
        ByVal bv_strSubItemDescription As String, _
        ByVal bv_lngItemId As Long, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(SubItemData._SUB_ITEM).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(SubItemData._SUB_ITEM)
                .Item(SubItemData.SB_ITM_ID) = intMax
                .Item(SubItemData.SB_ITM_CD) = bv_strSubItemCode
                If bv_strSubItemDescription <> Nothing Then
                    .Item(SubItemData.SB_ITM_DSCRPTN_VC) = bv_strSubItemDescription
                Else
                    .Item(SubItemData.SB_ITM_DSCRPTN_VC) = DBNull.Value
                End If
                .Item(SubItemData.ITM_ID) = bv_lngItemId
                If bv_strCreatedBy <> Nothing Then
                    .Item(SubItemData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(SubItemData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(SubItemData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(SubItemData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(SubItemData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(SubItemData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(SubItemData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(SubItemData.MDFD_DT) = DBNull.Value
                End If
                .Item(SubItemData.ACTV_BT) = bv_blnActiveBit
                .Item(SubItemData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, SubItemInsertQuery)
            dr = Nothing
            CreateSubItem = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateSubItem() TABLE NAME:SubItem"

    Public Function UpdateSubItem(ByVal bv_i64SubItemId As Int64, _
        ByVal bv_strSubItemCode As String, _
        ByVal bv_strSubItemDescription As String, _
        ByVal bv_lngItemId As Long, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(SubItemData._SUB_ITEM).NewRow()
            With dr
                .Item(SubItemData.SB_ITM_ID) = bv_i64SubItemId
                .Item(SubItemData.SB_ITM_CD) = bv_strSubItemCode
                If bv_strSubItemDescription <> Nothing Then
                    .Item(SubItemData.SB_ITM_DSCRPTN_VC) = bv_strSubItemDescription
                Else
                    .Item(SubItemData.SB_ITM_DSCRPTN_VC) = DBNull.Value
                End If
                .Item(SubItemData.ITM_ID) = bv_lngItemId
                If bv_strModifiedBy <> Nothing Then
                    .Item(SubItemData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(SubItemData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(SubItemData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(SubItemData.MDFD_DT) = DBNull.Value
                End If
                .Item(SubItemData.ACTV_BT) = bv_blnActiveBit
                .Item(SubItemData.DPT_ID) = bv_intDepotID
            End With
            UpdateSubItem = objData.UpdateRow(dr, SubItemUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetSubItemByDepotID() TABLE NAME:Sub_Item"

    Public Function GetSubItemByDepotID(ByVal bv_i32DepotID As Int32) As SubItemDataSet
        Try
            objData = New DataObjects(SubItemSelectQueryByDepotID, SubItemData.DPT_ID, CStr(bv_i32DepotID))
            objData.Fill(CType(ds, DataSet), SubItemData._SUB_ITEM)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


#Region "GetSubItemBySubItemCode() TABLE NAME:Sub_Item"

    Public Function GetSubItemBySubItemCode(ByVal bv_strSubItemCode As String, ByVal bv_intDepotId As Int64) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(SubItemData.SB_ITM_CD, bv_strSubItemCode)
            hshParameters.Add(SubItemData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(SubItemSelectQueryBySubItemCode, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class

#End Region
