#Region " Partys.vb"
'*********************************************************************************************************************
'Name :
'      Partys.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Partys.vb)
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
#Region "Partys"

Public Class Partys

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const PartyselectQueryByPRTY_CD As String = "SELECT PRTY_ID,PRTY_CD,PRTY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Party WHERE PRTY_CD=@PRTY_CD"
    Private Const PartyselectQueryByDPT_ID As String = "SELECT PRTY_ID,PRTY_CD,PRTY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Party WHERE DPT_ID=@DPT_ID"
    Private Const PartyInsertQuery As String = "INSERT INTO Party(PRTY_ID,PRTY_CD,PRTY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@PRTY_ID,@PRTY_CD,@PRTY_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const PartyUpdateQuery As String = "UPDATE Party SET PRTY_ID=@PRTY_ID, PRTY_CD=@PRTY_CD, PRTY_DSCRPTN_VC=@PRTY_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE PRTY_ID=@PRTY_ID"
    Private ds As PartyDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New PartyDataSet
    End Sub

#End Region

#Region "GetParty() TABLE NAME:Party"

    Public Function GetParty(ByVal bv_intDepotID As Integer) As PartyDataSet
        Try
            objData = New DataObjects(PartyselectQueryByDPT_ID, PartyData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), PartyData._Party)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetPartyByPartyCode() TABLE NAME:Party"

    Public Function GetPartyByPartyCode(ByVal bv_strPartyCode As String, ByVal bv_intDepotID As Integer) As PartyDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(PartyData.DPT_ID, bv_intDepotID)
            hshParameters.Add(PartyData.PRTY_CD, bv_strPartyCode)
            objData = New DataObjects(PartyselectQueryByPRTY_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), PartyData._Party)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateParty() TABLE NAME:Party"

    Public Function CreateParty(ByVal bv_strPartyCode As String, _
        ByVal bv_strPartyDescription As String, _
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
            dr = ds.Tables(PartyData._Party).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(PartyData._Party)
                .Item(PartyData.PRTY_ID) = intMax
                .Item(PartyData.PRTY_CD) = bv_strPartyCode
                If bv_strPartyDescription <> Nothing Then
                    .Item(PartyData.PRTY_DSCRPTN_VC) = bv_strPartyDescription
                Else
                    .Item(PartyData.PRTY_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(PartyData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(PartyData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(PartyData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(PartyData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(PartyData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(PartyData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(PartyData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(PartyData.MDFD_DT) = DBNull.Value
                End If
                .Item(PartyData.ACTV_BT) = bv_blnActiveBit
                .Item(PartyData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, PartyInsertQuery)
            dr = Nothing
            CreateParty = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateParty() TABLE NAME:Party"

    Public Function UpdateParty(ByVal bv_i64PartyId As Int64, _
        ByVal bv_strPartyCode As String, _
        ByVal bv_strPartyDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(PartyData._Party).NewRow()
            With dr
                .Item(PartyData.PRTY_ID) = bv_i64PartyId
                .Item(PartyData.PRTY_CD) = bv_strPartyCode
                If bv_strPartyDescription <> Nothing Then
                    .Item(PartyData.PRTY_DSCRPTN_VC) = bv_strPartyDescription
                Else
                    .Item(PartyData.PRTY_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(PartyData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(PartyData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(PartyData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(PartyData.MDFD_DT) = DBNull.Value
                End If
                .Item(PartyData.ACTV_BT) = bv_blnActiveBit
                .Item(PartyData.DPT_ID) = bv_intDepotID
            End With
            UpdateParty = objData.UpdateRow(dr, PartyUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class

#End Region
