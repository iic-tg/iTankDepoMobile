#Region " Materials.vb"
'*********************************************************************************************************************
'Name :
'      Materials.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Materials.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 7:11:31 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Materials"

Public Class Materials

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const MaterialSelectQueryByMTRL_CD As String = "SELECT MTRL_ID,MTRL_CD,MTRL_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM MATERIAL WHERE MTRL_CD=@MTRL_CD"
    Private Const MaterialSelectQueryByDPT_ID As String = "SELECT MTRL_ID,MTRL_CD,MTRL_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM MATERIAL WHERE DPT_ID=@DPT_ID"
    Private Const MaterialInsertQuery As String = "INSERT INTO MATERIAL(MTRL_ID,MTRL_CD,MTRL_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@MTRL_ID,@MTRL_CD,@MTRL_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const MaterialUpdateQuery As String = "UPDATE MATERIAL SET MTRL_ID=@MTRL_ID, MTRL_CD=@MTRL_CD, MTRL_DSCRPTN_VC=@MTRL_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE MTRL_ID=@MTRL_ID"
    Private ds As MaterialDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New MaterialDataSet
    End Sub

#End Region

#Region "GetMaterial() TABLE NAME:Material"

    Public Function GetMaterial(ByVal bv_intDepotID As Integer) As MaterialDataSet
        Try
            objData = New DataObjects(MaterialSelectQueryByDPT_ID, MaterialData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), MaterialData._MATERIAL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetMaterialByMaterialCode() TABLE NAME:Material"

    Public Function GetMaterialByMaterialCode(ByVal bv_strMaterialCode As String, ByVal bv_intDepotID As Integer) As MaterialDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(MaterialData.DPT_ID, bv_intDepotID)
            hshParameters.Add(MaterialData.MTRL_CD, bv_strMaterialCode)
            objData = New DataObjects(MaterialSelectQueryByMTRL_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), MaterialData._MATERIAL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateMaterial() TABLE NAME:Material"

    Public Function CreateMaterial(ByVal bv_strMaterialCode As String, _
        ByVal bv_strMaterialDescription As String, _
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
            dr = ds.Tables(MaterialData._MATERIAL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(MaterialData._MATERIAL)
                .Item(MaterialData.MTRL_ID) = intMax
                .Item(MaterialData.MTRL_CD) = bv_strMaterialCode
                If bv_strMaterialDescription <> Nothing Then
                    .Item(MaterialData.MTRL_DSCRPTN_VC) = bv_strMaterialDescription
                Else
                    .Item(MaterialData.MTRL_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(MaterialData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(MaterialData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(MaterialData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(MaterialData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(MaterialData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(MaterialData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(MaterialData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(MaterialData.MDFD_DT) = DBNull.Value
                End If
                .Item(MaterialData.ACTV_BT) = bv_blnActiveBit
                .Item(MaterialData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, MaterialInsertQuery)
            dr = Nothing
            CreateMaterial = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateMaterial() TABLE NAME:Material"

    Public Function UpdateMaterial(ByVal bv_i64MaterialId As Int64, _
        ByVal bv_strMaterialCode As String, _
        ByVal bv_strMaterialDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(MaterialData._MATERIAL).NewRow()
            With dr
                .Item(MaterialData.MTRL_ID) = bv_i64MaterialId
                .Item(MaterialData.MTRL_CD) = bv_strMaterialCode
                If bv_strMaterialDescription <> Nothing Then
                    .Item(MaterialData.MTRL_DSCRPTN_VC) = bv_strMaterialDescription
                Else
                    .Item(MaterialData.MTRL_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(MaterialData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(MaterialData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(MaterialData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(MaterialData.MDFD_DT) = DBNull.Value
                End If
                .Item(MaterialData.ACTV_BT) = bv_blnActiveBit
                .Item(MaterialData.DPT_ID) = bv_intDepotID
            End With
            UpdateMaterial = objData.UpdateRow(dr, MaterialUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



End Class

#End Region
