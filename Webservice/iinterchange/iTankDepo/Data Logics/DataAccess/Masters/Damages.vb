#Region " Damages.vb"
'*********************************************************************************************************************
'Name :
'      Damages.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Damages.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 5:07:11 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Damages"

Public Class Damages

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const DamageSelectQueryByDMG_CD As String = "SELECT DMG_ID,DMG_CD,DMG_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM DAMAGE WHERE DMG_CD=@DMG_CD"
    Private Const DamageSelectQueryByDPT_ID As String = "SELECT DMG_ID,DMG_CD,DMG_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM DAMAGE WHERE DPT_ID=@DPT_ID"
    Private Const DamageInsertQuery As String = "INSERT INTO DAMAGE(DMG_ID,DMG_CD,DMG_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@DMG_ID,@DMG_CD,@DMG_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const DamageUpdateQuery As String = "UPDATE DAMAGE SET DMG_ID=@DMG_ID, DMG_CD=@DMG_CD, DMG_DSCRPTN_VC=@DMG_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE DMG_ID=@DMG_ID"
    Private ds As DamageDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New DamageDataSet
    End Sub

#End Region

#Region "GetDamage() TABLE NAME:Damage"

    Public Function GetDamage(ByVal bv_intDepotID As Integer) As DamageDataSet
        Try
            objData = New DataObjects(DamageSelectQueryByDPT_ID, DamageData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), DamageData._DAMAGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetDamageByDamageCode() TABLE NAME:Damage"

    Public Function GetDamageByDamageCode(ByVal bv_strDamageCode As String, ByVal bv_intDepotID As Integer) As DamageDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(DamageData.DPT_ID, bv_intDepotID)
            hshParameters.Add(DamageData.DMG_CD, bv_strDamageCode)
            objData = New DataObjects(DamageSelectQueryByDMG_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), DamageData._DAMAGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateDamage() TABLE NAME:Damage"

    Public Function CreateDamage(ByVal bv_strDamageCode As String, _
        ByVal bv_strDamageDescription As String, _
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
            dr = ds.Tables(DamageData._DAMAGE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(DamageData._DAMAGE)
                .Item(DamageData.DMG_ID) = intMax
                .Item(DamageData.DMG_CD) = bv_strDamageCode
                If bv_strDamageDescription <> Nothing Then
                    .Item(DamageData.DMG_DSCRPTN_VC) = bv_strDamageDescription
                Else
                    .Item(DamageData.DMG_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(DamageData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(DamageData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(DamageData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(DamageData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(DamageData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(DamageData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(DamageData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(DamageData.MDFD_DT) = DBNull.Value
                End If
                .Item(DamageData.ACTV_BT) = bv_blnActiveBit
                .Item(DamageData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, DamageInsertQuery)
            dr = Nothing
            CreateDamage = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateDamage() TABLE NAME:Damage"

    Public Function UpdateDamage(ByVal bv_i64DamageId As Int64, _
        ByVal bv_strDamageCode As String, _
        ByVal bv_strDamageDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(DamageData._DAMAGE).NewRow()
            With dr
                .Item(DamageData.DMG_ID) = bv_i64DamageId
                .Item(DamageData.DMG_CD) = bv_strDamageCode
                If bv_strDamageDescription <> Nothing Then
                    .Item(DamageData.DMG_DSCRPTN_VC) = bv_strDamageDescription
                Else
                    .Item(DamageData.DMG_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(DamageData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(DamageData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(DamageData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(DamageData.MDFD_DT) = DBNull.Value
                End If
                .Item(DamageData.ACTV_BT) = bv_blnActiveBit
                .Item(DamageData.DPT_ID) = bv_intDepotID
            End With
            UpdateDamage = objData.UpdateRow(dr, DamageUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



End Class

#End Region
