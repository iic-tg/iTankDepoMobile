#Region " EquipmentSizes.vb"
'*********************************************************************************************************************
'Name :
'      EquipmentSizes.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EquipmentSizes.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 7:05:17 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "EquipmentSizes"

Public Class EquipmentSizes

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const EquipmentSizeSelectQueryByEQPMNT_SZ_CD As String = "SELECT EQPMNT_SZ_ID,EQPMNT_SZ_CD,EQPMNT_SZ_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM EQUIPMENT_SIZE WHERE EQPMNT_SZ_CD=@EQPMNT_SZ_CD"
    Private Const EquipmentSizeSelectQueryByDPT_ID As String = "SELECT EQPMNT_SZ_ID,EQPMNT_SZ_CD,EQPMNT_SZ_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM EQUIPMENT_SIZE WHERE DPT_ID=@DPT_ID"
    Private Const EquipmentSizeInsertQuery As String = "INSERT INTO EQUIPMENT_SIZE(EQPMNT_SZ_ID,EQPMNT_SZ_CD,EQPMNT_SZ_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@EQPMNT_SZ_ID,@EQPMNT_SZ_CD,@EQPMNT_SZ_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const EquipmentSizeUpdateQuery As String = "UPDATE EQUIPMENT_SIZE SET EQPMNT_SZ_ID=@EQPMNT_SZ_ID, EQPMNT_SZ_CD=@EQPMNT_SZ_CD, EQPMNT_SZ_DSCRPTN_VC=@EQPMNT_SZ_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE EQPMNT_SZ_ID=@EQPMNT_SZ_ID"
    Private ds As EquipmentSizeDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New EquipmentSizeDataSet
    End Sub

#End Region

#Region "GetEquipmentSize() TABLE NAME:EquipmentSize"

    Public Function GetEquipmentSize(ByVal bv_intDepotID As Integer) As EquipmentSizeDataSet
        Try
            objData = New DataObjects(EquipmentSizeSelectQueryByDPT_ID, EquipmentSizeData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), EquipmentSizeData._EQUIPMENT_SIZE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentSizeByEquipmentSizeSize() TABLE NAME:EquipmentSize"

    Public Function GetEquipmentSizeByEquipmentSizeCode(ByVal bv_strEquipmentSizeCode As String, ByVal bv_intDepotID As Integer) As EquipmentSizeDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentSizeData.DPT_ID, bv_intDepotID)
            hshParameters.Add(EquipmentSizeData.EQPMNT_SZ_CD, bv_strEquipmentSizeCode)
            objData = New DataObjects(EquipmentSizeSelectQueryByEQPMNT_SZ_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), EquipmentSizeData._EQUIPMENT_SIZE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateEquipmentSize() TABLE NAME:EquipmentSize"

    Public Function CreateEquipmentSize(ByVal bv_strEquipmentSizeCode As String, _
        ByVal bv_strEquipmentSizeDescription As String, _
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
            dr = ds.Tables(EquipmentSizeData._EQUIPMENT_SIZE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EquipmentSizeData._EQUIPMENT_SIZE)
                .Item(EquipmentSizeData.EQPMNT_SZ_ID) = intMax
                .Item(EquipmentSizeData.EQPMNT_SZ_CD) = bv_strEquipmentSizeCode
                If bv_strEquipmentSizeDescription <> Nothing Then
                    .Item(EquipmentSizeData.EQPMNT_SZ_DSCRPTN_VC) = bv_strEquipmentSizeDescription
                Else
                    .Item(EquipmentSizeData.EQPMNT_SZ_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(EquipmentSizeData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(EquipmentSizeData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(EquipmentSizeData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(EquipmentSizeData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(EquipmentSizeData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(EquipmentSizeData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(EquipmentSizeData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(EquipmentSizeData.MDFD_DT) = DBNull.Value
                End If
                .Item(EquipmentSizeData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentSizeData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, EquipmentSizeInsertQuery)
            dr = Nothing
            CreateEquipmentSize = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateEquipmentSize() TABLE NAME:EquipmentSize"

    Public Function UpdateEquipmentSize(ByVal bv_i64EquipmentSizeId As Int64, _
        ByVal bv_strEquipmentSizeCode As String, _
        ByVal bv_strEquipmentSizeDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentSizeData._EQUIPMENT_SIZE).NewRow()
            With dr
                .Item(EquipmentSizeData.EQPMNT_SZ_ID) = bv_i64EquipmentSizeId
                .Item(EquipmentSizeData.EQPMNT_SZ_CD) = bv_strEquipmentSizeCode
                If bv_strEquipmentSizeDescription <> Nothing Then
                    .Item(EquipmentSizeData.EQPMNT_SZ_DSCRPTN_VC) = bv_strEquipmentSizeDescription
                Else
                    .Item(EquipmentSizeData.EQPMNT_SZ_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(EquipmentSizeData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(EquipmentSizeData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(EquipmentSizeData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(EquipmentSizeData.MDFD_DT) = DBNull.Value
                End If
                .Item(EquipmentSizeData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentSizeData.DPT_ID) = bv_intDepotID
            End With
            UpdateEquipmentSize = objData.UpdateRow(dr, EquipmentSizeUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class

#End Region
