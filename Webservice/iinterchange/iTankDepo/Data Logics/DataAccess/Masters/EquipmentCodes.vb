#Region " EquipmentCodes.vb"
'*********************************************************************************************************************
'Name :
'      EquipmentCodes.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EquipmentCodes.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 5:08:50 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "EquipmentCodes"

Public Class EquipmentCodes

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const EquipmentCodeSelectQueryByEQPMNT_CD_CD As String = "SELECT EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_CD_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM EQUIPMENT_CODE WHERE EQPMNT_CD_CD=@EQPMNT_CD_CD"
    Private Const EquipmentCodeSelectQueryByDPT_ID As String = "SELECT EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_CD_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM EQUIPMENT_CODE WHERE DPT_ID=@DPT_ID"
    Private Const EquipmentCodeInsertQuery As String = "INSERT INTO EQUIPMENT_CODE(EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_CD_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@EQPMNT_CD_ID,@EQPMNT_CD_CD,@EQPMNT_CD_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const EquipmentCodeUpdateQuery As String = "UPDATE EQUIPMENT_CODE SET EQPMNT_CD_ID=@EQPMNT_CD_ID, EQPMNT_CD_CD=@EQPMNT_CD_CD, EQPMNT_CD_DSCRPTN_VC=@EQPMNT_CD_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE EQPMNT_CD_ID=@EQPMNT_CD_ID"
    Private ds As EquipmentCodeDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New EquipmentCodeDataSet
    End Sub

#End Region

#Region "GetEquipmentCode() TABLE NAME:EquipmentCode"

    Public Function GetEquipmentCode(ByVal bv_intDepotID As Integer) As EquipmentCodeDataSet
        Try
            objData = New DataObjects(EquipmentCodeSelectQueryByDPT_ID, EquipmentCodeData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), EquipmentCodeData._EQUIPMENT_CODE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentCodeByEquipmentCodeCode() TABLE NAME:UNIT"

    Public Function GetEquipmentCodeByEquipmentCodeCode(ByVal bv_strEquipmentCodeCode As String, ByVal bv_intDepotID As Integer) As EquipmentCodeDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentCodeData.DPT_ID, bv_intDepotID)
            hshParameters.Add(EquipmentCodeData.EQPMNT_CD_CD, bv_strEquipmentCodeCode)
            objData = New DataObjects(EquipmentCodeSelectQueryByEQPMNT_CD_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), EquipmentCodeData._EQUIPMENT_CODE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateEquipmentCode() TABLE NAME:EquipmentCode"

    Public Function CreateEquipmentCode(ByVal bv_strEquipmentCodeCode As String, _
        ByVal bv_strEquipmentCodeDescription As String, _
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
            dr = ds.Tables(EquipmentCodeData._EQUIPMENT_CODE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EquipmentCodeData._EQUIPMENT_CODE)
                .Item(EquipmentCodeData.EQPMNT_CD_ID) = intMax
                .Item(EquipmentCodeData.EQPMNT_CD_CD) = bv_strEquipmentCodeCode
                If bv_strEquipmentCodeDescription <> Nothing Then
                    .Item(EquipmentCodeData.EQPMNT_CD_DSCRPTN_VC) = bv_strEquipmentCodeDescription
                Else
                    .Item(EquipmentCodeData.EQPMNT_CD_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(EquipmentCodeData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(EquipmentCodeData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(EquipmentCodeData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(EquipmentCodeData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(EquipmentCodeData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(EquipmentCodeData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(EquipmentCodeData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(EquipmentCodeData.MDFD_DT) = DBNull.Value
                End If
                .Item(EquipmentCodeData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentCodeData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, EquipmentCodeInsertQuery)
            dr = Nothing
            CreateEquipmentCode = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateEquipmentCode() TABLE NAME:EquipmentCode"

    Public Function UpdateEquipmentCode(ByVal bv_i64EquipmentCodeId As Int64, _
        ByVal bv_strEquipmentCodeCode As String, _
        ByVal bv_strEquipmentCodeDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentCodeData._EQUIPMENT_CODE).NewRow()
            With dr
                .Item(EquipmentCodeData.EQPMNT_CD_ID) = bv_i64EquipmentCodeId
                .Item(EquipmentCodeData.EQPMNT_CD_CD) = bv_strEquipmentCodeCode
                If bv_strEquipmentCodeDescription <> Nothing Then
                    .Item(EquipmentCodeData.EQPMNT_CD_DSCRPTN_VC) = bv_strEquipmentCodeDescription
                Else
                    .Item(EquipmentCodeData.EQPMNT_CD_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(EquipmentCodeData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(EquipmentCodeData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(EquipmentCodeData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(EquipmentCodeData.MDFD_DT) = DBNull.Value
                End If
                .Item(EquipmentCodeData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentCodeData.DPT_ID) = bv_intDepotID
            End With
            UpdateEquipmentCode = objData.UpdateRow(dr, EquipmentCodeUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



End Class

#End Region
