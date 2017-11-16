#Region " EquipmentStatuss.vb"
'*********************************************************************************************************************
'Name :
'      EquipmentStatuss.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EquipmentStatuss.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 7:06:24 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "EquipmentStatuss"

Public Class EquipmentStatuss

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const EquipmentStatusSelectQueryByEQPMNT_STTS_CD As String = "SELECT EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM EQUIPMENT_STATUS WHERE EQPMNT_STTS_CD=@EQPMNT_STTS_CD"
    Private Const EquipmentStatusSelectQueryByDPT_ID As String = "SELECT EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM EQUIPMENT_STATUS WHERE DPT_ID=@DPT_ID"
    Private Const EquipmentStatusInsertQuery As String = "INSERT INTO EQUIPMENT_STATUS(EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@EQPMNT_STTS_ID,@EQPMNT_STTS_CD,@EQPMNT_STTS_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const EquipmentStatusUpdateQuery As String = "UPDATE EQUIPMENT_STATUS SET EQPMNT_STTS_ID=@EQPMNT_STTS_ID, EQPMNT_STTS_CD=@EQPMNT_STTS_CD, EQPMNT_STTS_DSCRPTN_VC=@EQPMNT_STTS_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE EQPMNT_STTS_ID=@EQPMNT_STTS_ID"
    Private ds As EquipmentStatusDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New EquipmentStatusDataSet
    End Sub

#End Region

#Region "GetEquipmentStatus() TABLE NAME:EquipmentStatus"

    Public Function GetEquipmentStatus(ByVal bv_intDepotID As Integer) As EquipmentStatusDataSet
        Try
            objData = New DataObjects(EquipmentStatusSelectQueryByDPT_ID, EquipmentStatusData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), EquipmentStatusData._EQUIPMENT_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentStatusByEquipmentStatusCode() TABLE NAME:EquipmentStatus"

    Public Function GetEquipmentStatusByEquipmentStatusCode(ByVal bv_strEquipmentStatusCode As String, ByVal bv_intDepotID As Integer) As EquipmentStatusDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentStatusData.DPT_ID, bv_intDepotID)
            hshParameters.Add(EquipmentStatusData.EQPMNT_STTS_CD, bv_strEquipmentStatusCode)
            objData = New DataObjects(EquipmentStatusSelectQueryByEQPMNT_STTS_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), EquipmentStatusData._EQUIPMENT_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateEquipmentStatus() TABLE NAME:EquipmentStatus"

    Public Function CreateEquipmentStatus(ByVal bv_strEquipmentStatusCode As String, _
        ByVal bv_strEquipmentStatusDescription As String, _
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
            dr = ds.Tables(EquipmentStatusData._EQUIPMENT_STATUS).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EquipmentStatusData._EQUIPMENT_STATUS)
                .Item(EquipmentStatusData.EQPMNT_STTS_ID) = intMax
                .Item(EquipmentStatusData.EQPMNT_STTS_CD) = bv_strEquipmentStatusCode
                If bv_strEquipmentStatusDescription <> Nothing Then
                    .Item(EquipmentStatusData.EQPMNT_STTS_DSCRPTN_VC) = bv_strEquipmentStatusDescription
                Else
                    .Item(EquipmentStatusData.EQPMNT_STTS_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(EquipmentStatusData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(EquipmentStatusData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(EquipmentStatusData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(EquipmentStatusData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(EquipmentStatusData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(EquipmentStatusData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(EquipmentStatusData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(EquipmentStatusData.MDFD_DT) = DBNull.Value
                End If
                .Item(EquipmentStatusData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentStatusData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, EquipmentStatusInsertQuery)
            dr = Nothing
            CreateEquipmentStatus = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateEquipmentStatus() TABLE NAME:EquipmentStatus"

    Public Function UpdateEquipmentStatus(ByVal bv_i64EquipmentStatusId As Int64, _
        ByVal bv_strEquipmentStatusCode As String, _
        ByVal bv_strEquipmentStatusDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentStatusData._EQUIPMENT_STATUS).NewRow()
            With dr
                .Item(EquipmentStatusData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                .Item(EquipmentStatusData.EQPMNT_STTS_CD) = bv_strEquipmentStatusCode
                If bv_strEquipmentStatusDescription <> Nothing Then
                    .Item(EquipmentStatusData.EQPMNT_STTS_DSCRPTN_VC) = bv_strEquipmentStatusDescription
                Else
                    .Item(EquipmentStatusData.EQPMNT_STTS_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(EquipmentStatusData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(EquipmentStatusData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(EquipmentStatusData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(EquipmentStatusData.MDFD_DT) = DBNull.Value
                End If
                .Item(EquipmentStatusData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentStatusData.DPT_ID) = bv_intDepotID
            End With
            UpdateEquipmentStatus = objData.UpdateRow(dr, EquipmentStatusUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class

#End Region
