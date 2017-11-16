#Region " Statuss.vb"
'*********************************************************************************************************************
'Name :
'      Statuss.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Statuss.vb)
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
#Region "Statuss"

Public Class Statuss

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const StatusSelectQueryBySTTS_CD As String = "SELECT STTS_ID,STTS_CD,STTS_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Status WHERE STTS_CD=@STTS_CD"
    Private Const StatusSelectQueryByDPT_ID As String = "SELECT STTS_ID,STTS_CD,STTS_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Status WHERE DPT_ID=@DPT_ID"
    Private Const StatusInsertQuery As String = "INSERT INTO Status(STTS_ID,STTS_CD,STTS_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@STTS_ID,@STTS_CD,@STTS_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const StatusUpdateQuery As String = "UPDATE Status SET STTS_ID=@STTS_ID, STTS_CD=@STTS_CD, STTS_DSCRPTN_VC=@STTS_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE STTS_ID=@STTS_ID"
    Private ds As StatusDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New StatusDataSet
    End Sub

#End Region

#Region "GetStatus() TABLE NAME:Status"

    Public Function GetStatus(ByVal bv_intDepotID As Integer) As StatusDataSet
        Try
            objData = New DataObjects(StatusSelectQueryByDPT_ID, StatusData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), StatusData._Status)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetStatusByStatusCode() TABLE NAME:Status"

    Public Function GetStatusByStatusCode(ByVal bv_strStatusCode As String, ByVal bv_intDepotID As Integer) As StatusDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(StatusData.DPT_ID, bv_intDepotID)
            hshParameters.Add(StatusData.STTS_CD, bv_strStatusCode)
            objData = New DataObjects(StatusSelectQueryBySTTS_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), StatusData._Status)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateStatus() TABLE NAME:Status"

    Public Function CreateStatus(ByVal bv_strStatusCode As String, _
        ByVal bv_strStatusDescription As String, _
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
            dr = ds.Tables(StatusData._Status).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(StatusData._Status)
                .Item(StatusData.STTS_ID) = intMax
                .Item(StatusData.STTS_CD) = bv_strStatusCode
                If bv_strStatusDescription <> Nothing Then
                    .Item(StatusData.STTS_DSCRPTN_VC) = bv_strStatusDescription
                Else
                    .Item(StatusData.STTS_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(StatusData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(StatusData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(StatusData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(StatusData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(StatusData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(StatusData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(StatusData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(StatusData.MDFD_DT) = DBNull.Value
                End If
                .Item(StatusData.ACTV_BT) = bv_blnActiveBit
                .Item(StatusData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, StatusInsertQuery)
            dr = Nothing
            CreateStatus = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateStatus() TABLE NAME:Status"

    Public Function UpdateStatus(ByVal bv_i64StatusId As Int64, _
        ByVal bv_strStatusCode As String, _
        ByVal bv_strStatusDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(StatusData._Status).NewRow()
            With dr
                .Item(StatusData.STTS_ID) = bv_i64StatusId
                .Item(StatusData.STTS_CD) = bv_strStatusCode
                If bv_strStatusDescription <> Nothing Then
                    .Item(StatusData.STTS_DSCRPTN_VC) = bv_strStatusDescription
                Else
                    .Item(StatusData.STTS_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(StatusData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(StatusData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(StatusData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(StatusData.MDFD_DT) = DBNull.Value
                End If
                .Item(StatusData.ACTV_BT) = bv_blnActiveBit
                .Item(StatusData.DPT_ID) = bv_intDepotID
            End With
            UpdateStatus = objData.UpdateRow(dr, StatusUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class

#End Region
