#Region " Ports.vb"
'*********************************************************************************************************************
'Name :
'      Ports.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Ports.vb)
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
#Region "Ports"

Public Class Ports

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const PortSelectQueryByPRT_CD As String = "SELECT PRT_ID,PRT_CD,PRT_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Port WHERE PRT_CD=@PRT_CD"
    Private Const PortSelectQueryByDPT_ID As String = "SELECT PRT_ID,PRT_CD,PRT_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Port WHERE DPT_ID=@DPT_ID"
    Private Const PortInsertQuery As String = "INSERT INTO Port(PRT_ID,PRT_CD,PRT_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@PRT_ID,@PRT_CD,@PRT_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const PortUpdateQuery As String = "UPDATE Port SET PRT_ID=@PRT_ID, PRT_CD=@PRT_CD, PRT_DSCRPTN_VC=@PRT_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE PRT_ID=@PRT_ID"
    Private ds As PortDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New PortDataSet
    End Sub

#End Region

#Region "GetPort() TABLE NAME:Port"

    Public Function GetPort(ByVal bv_intDepotID As Integer) As PortDataSet
        Try
            objData = New DataObjects(PortSelectQueryByDPT_ID, PortData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), PortData._Port)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetPortByPortCode() TABLE NAME:Port"

    Public Function GetPortByPortCode(ByVal bv_strPortCode As String, ByVal bv_intDepotID As Integer) As PortDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(PortData.DPT_ID, bv_intDepotID)
            hshParameters.Add(PortData.PRT_CD, bv_strPortCode)
            objData = New DataObjects(PortSelectQueryByPRT_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), PortData._Port)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreatePort() TABLE NAME:Port"

    Public Function CreatePort(ByVal bv_strPortCode As String, _
        ByVal bv_strPortDescription As String, _
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
            dr = ds.Tables(PortData._Port).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(PortData._Port)
                .Item(PortData.PRT_ID) = intMax
                .Item(PortData.PRT_CD) = bv_strPortCode
                If bv_strPortDescription <> Nothing Then
                    .Item(PortData.PRT_DSCRPTN_VC) = bv_strPortDescription
                Else
                    .Item(PortData.PRT_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(PortData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(PortData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(PortData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(PortData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(PortData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(PortData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(PortData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(PortData.MDFD_DT) = DBNull.Value
                End If
                .Item(PortData.ACTV_BT) = bv_blnActiveBit
                .Item(PortData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, PortInsertQuery)
            dr = Nothing
            CreatePort = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdatePort() TABLE NAME:Port"

    Public Function UpdatePort(ByVal bv_i64PortId As Int64, _
        ByVal bv_strPortCode As String, _
        ByVal bv_strPortDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(PortData._Port).NewRow()
            With dr
                .Item(PortData.PRT_ID) = bv_i64PortId
                .Item(PortData.PRT_CD) = bv_strPortCode
                If bv_strPortDescription <> Nothing Then
                    .Item(PortData.PRT_DSCRPTN_VC) = bv_strPortDescription
                Else
                    .Item(PortData.PRT_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(PortData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(PortData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(PortData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(PortData.MDFD_DT) = DBNull.Value
                End If
                .Item(PortData.ACTV_BT) = bv_blnActiveBit
                .Item(PortData.DPT_ID) = bv_intDepotID
            End With
            UpdatePort = objData.UpdateRow(dr, PortUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class

#End Region
