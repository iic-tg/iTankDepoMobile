#Region " Responsibilitys.vb"
'*********************************************************************************************************************
'Name :
'      Responsibilitys.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Responsibilitys.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/9/2013 9:53:16 AM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Responsibilitys"

Public Class Responsibilitys

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const ResponsibilitySelectQueryByRSPNSBLTY_CD As String = "SELECT RSPNSBLTY_ID,RSPNSBLTY_CD,RSPNSBLTY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM RESPONSIBILITY WHERE RSPNSBLTY_CD=@RSPNSBLTY_CD"
    Private Const ResponsibilitySelectQueryByDPT_ID As String = "SELECT RSPNSBLTY_ID,RSPNSBLTY_CD,RSPNSBLTY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM RESPONSIBILITY WHERE DPT_ID=@DPT_ID"
    Private Const ResponsibilityInsertQuery As String = "INSERT INTO RESPONSIBILITY(RSPNSBLTY_ID,RSPNSBLTY_CD,RSPNSBLTY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@RSPNSBLTY_ID,@RSPNSBLTY_CD,@RSPNSBLTY_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const ResponsibilityUpdateQuery As String = "UPDATE RESPONSIBILITY SET RSPNSBLTY_ID=@RSPNSBLTY_ID, RSPNSBLTY_CD=@RSPNSBLTY_CD, RSPNSBLTY_DSCRPTN_VC=@RSPNSBLTY_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE RSPNSBLTY_ID=@RSPNSBLTY_ID"
    Private ds As ResponsibilityDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New ResponsibilityDataSet
    End Sub

#End Region

#Region "GetResponsibility() TABLE NAME:Responsibility"

    Public Function GetResponsibility(ByVal bv_intDepotID As Integer) As ResponsibilityDataSet
        Try
            objData = New DataObjects(ResponsibilitySelectQueryByDPT_ID, ResponsibilityData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), ResponsibilityData._RESPONSIBILITY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetResponsibilityByResponsibilityCode() TABLE NAME:Responsibility"

    Public Function GetResponsibilityByResponsibilityCode(ByVal bv_strResponsibilityCode As String, ByVal bv_intDepotID As Integer) As ResponsibilityDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(ResponsibilityData.DPT_ID, bv_intDepotID)
            hshParameters.Add(ResponsibilityData.RSPNSBLTY_CD, bv_strResponsibilityCode)
            objData = New DataObjects(ResponsibilitySelectQueryByRSPNSBLTY_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), ResponsibilityData._RESPONSIBILITY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateResponsibility() TABLE NAME:Responsibility"

    Public Function CreateResponsibility(ByVal bv_strResponsibilityCode As String, _
        ByVal bv_strResponsibilityDescription As String, _
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
            dr = ds.Tables(ResponsibilityData._RESPONSIBILITY).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ResponsibilityData._RESPONSIBILITY)
                .Item(ResponsibilityData.RSPNSBLTY_ID) = intMax
                .Item(ResponsibilityData.RSPNSBLTY_CD) = bv_strResponsibilityCode
                If bv_strResponsibilityDescription <> Nothing Then
                    .Item(ResponsibilityData.RSPNSBLTY_DSCRPTN_VC) = bv_strResponsibilityDescription
                Else
                    .Item(ResponsibilityData.RSPNSBLTY_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(ResponsibilityData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(ResponsibilityData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(ResponsibilityData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(ResponsibilityData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(ResponsibilityData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(ResponsibilityData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(ResponsibilityData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(ResponsibilityData.MDFD_DT) = DBNull.Value
                End If
                .Item(ResponsibilityData.ACTV_BT) = bv_blnActiveBit
                .Item(ResponsibilityData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, ResponsibilityInsertQuery)
            dr = Nothing
            CreateResponsibility = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateResponsibility() TABLE NAME:Responsibility"

    Public Function UpdateResponsibility(ByVal bv_i64ResponsibilityId As Int64, _
        ByVal bv_strResponsibilityCode As String, _
        ByVal bv_strResponsibilityDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(ResponsibilityData._RESPONSIBILITY).NewRow()
            With dr
                .Item(ResponsibilityData.RSPNSBLTY_ID) = bv_i64ResponsibilityId
                .Item(ResponsibilityData.RSPNSBLTY_CD) = bv_strResponsibilityCode
                If bv_strResponsibilityDescription <> Nothing Then
                    .Item(ResponsibilityData.RSPNSBLTY_DSCRPTN_VC) = bv_strResponsibilityDescription
                Else
                    .Item(ResponsibilityData.RSPNSBLTY_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(ResponsibilityData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(ResponsibilityData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(ResponsibilityData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(ResponsibilityData.MDFD_DT) = DBNull.Value
                End If
                .Item(ResponsibilityData.ACTV_BT) = bv_blnActiveBit
                .Item(ResponsibilityData.DPT_ID) = bv_intDepotID
            End With
            UpdateResponsibility = objData.UpdateRow(dr, ResponsibilityUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class

#End Region
