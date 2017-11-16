#Region " Repairs.vb"
'*********************************************************************************************************************
'Name :
'      Repairs.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Repairs.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/9/2013 9:51:48 AM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Repairs"

Public Class Repairs

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const RepairSelectQueryByRPR_CD As String = "SELECT RPR_ID,RPR_CD,RPR_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM REPAIR WHERE RPR_CD=@RPR_CD"
    Private Const RepairSelectQueryByDPT_ID As String = "SELECT RPR_ID,RPR_CD,RPR_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM REPAIR WHERE DPT_ID=@DPT_ID"
    Private Const RepairInsertQuery As String = "INSERT INTO REPAIR(RPR_ID,RPR_CD,RPR_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@RPR_ID,@RPR_CD,@RPR_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const RepairUpdateQuery As String = "UPDATE REPAIR SET RPR_ID=@RPR_ID, RPR_CD=@RPR_CD, RPR_DSCRPTN_VC=@RPR_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE RPR_ID=@RPR_ID"
    Private ds As RepairDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New RepairDataSet
    End Sub

#End Region

#Region "GetRepair() TABLE NAME:Repair"

    Public Function GetRepair(ByVal bv_intDepotID As Integer) As RepairDataSet
        Try
            objData = New DataObjects(RepairSelectQueryByDPT_ID, RepairData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), RepairData._REPAIR)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRepairByRepairCode() TABLE NAME:Repair"

    Public Function GetRepairByRepairCode(ByVal bv_strRepairCode As String, ByVal bv_intDepotID As Integer) As RepairDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(RepairData.DPT_ID, bv_intDepotID)
            hshParameters.Add(RepairData.RPR_CD, bv_strRepairCode)
            objData = New DataObjects(RepairSelectQueryByRPR_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), RepairData._REPAIR)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateRepair() TABLE NAME:Repair"

    Public Function CreateRepair(ByVal bv_strRepairCode As String, _
        ByVal bv_strRepairDescription As String, _
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
            dr = ds.Tables(RepairData._REPAIR).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(RepairData._REPAIR)
                .Item(RepairData.RPR_ID) = intMax
                .Item(RepairData.RPR_CD) = bv_strRepairCode
                If bv_strRepairDescription <> Nothing Then
                    .Item(RepairData.RPR_DSCRPTN_VC) = bv_strRepairDescription
                Else
                    .Item(RepairData.RPR_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(RepairData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(RepairData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(RepairData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(RepairData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(RepairData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(RepairData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(RepairData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(RepairData.MDFD_DT) = DBNull.Value
                End If
                .Item(RepairData.ACTV_BT) = bv_blnActiveBit
                .Item(RepairData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, RepairInsertQuery)
            dr = Nothing
            CreateRepair = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateRepair() TABLE NAME:Repair"

    Public Function UpdateRepair(ByVal bv_i64RepairId As Int64, _
        ByVal bv_strRepairCode As String, _
        ByVal bv_strRepairDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(RepairData._REPAIR).NewRow()
            With dr
                .Item(RepairData.RPR_ID) = bv_i64RepairId
                .Item(RepairData.RPR_CD) = bv_strRepairCode
                If bv_strRepairDescription <> Nothing Then
                    .Item(RepairData.RPR_DSCRPTN_VC) = bv_strRepairDescription
                Else
                    .Item(RepairData.RPR_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(RepairData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(RepairData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(RepairData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(RepairData.MDFD_DT) = DBNull.Value
                End If
                .Item(RepairData.ACTV_BT) = bv_blnActiveBit
                .Item(RepairData.DPT_ID) = bv_intDepotID
            End With
            UpdateRepair = objData.UpdateRow(dr, RepairUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



End Class

#End Region
