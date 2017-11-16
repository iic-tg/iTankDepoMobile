#Region " EquipmentTypes.vb"
'*********************************************************************************************************************
'Name :
'      EquipmentTypes.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EquipmentTypes.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 7:07:21 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "EquipmentTypes"

Public Class EquipmentTypes

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const EquipmentTypeSelectQueryByEQPMNT_TYP_CD As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_CD=@EQPMNT_TYP_CD"
    Private Const EquipmentGroupSelectQueryByEQPMNT_TYP_ID As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD,EQPMNT_GRP_CD FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    Private Const EquipmentTypeSelectQueryByDPT_ID As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD,EQPMNT_GRP_CD FROM EQUIPMENT_TYPE WHERE DPT_ID=@DPT_ID"
    Private Const EquipmentTypeInsertQuery As String = "INSERT INTO EQUIPMENT_TYPE(EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD,EQPMNT_GRP_CD)VALUES(@EQPMNT_TYP_ID,@EQPMNT_TYP_CD,@EQPMNT_TYP_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID,@EQPMNT_CD_CD,@EQPMNT_GRP_CD)"
    Private Const EquipmentTypeUpdateQuery As String = "UPDATE EQUIPMENT_TYPE SET EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EQPMNT_TYP_CD=@EQPMNT_TYP_CD, EQPMNT_TYP_DSCRPTN_VC=@EQPMNT_TYP_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID,EQPMNT_CD_CD=@EQPMNT_CD_CD, EQPMNT_GRP_CD=@EQPMNT_GRP_CD WHERE EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    Private ds As EquipmentTypeDataSet

    'Equipment Type & Code - Merge
    Private Const EQUIPMENT_TYPESelectQueryPk As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    Private Const EQUIPMENT_TYPESelectQuery As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE"
    Private Const EQUIPMENT_TYPESelectQueryByEQPMNT_TYP_ID As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    Private Const EQUIPMENT_TYPESelectQueryByEQPMNT_TYP_CD As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_CD=@EQPMNT_TYP_CD"
    Private Const EQUIPMENT_TYPESelectQueryByEQPMNT_TYP_DSCRPTN_VC As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_DSCRPTN_VC=@EQPMNT_TYP_DSCRPTN_VC"
    Private Const EQUIPMENT_TYPESelectQueryByCRTD_BY As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE CRTD_BY=@CRTD_BY"
    Private Const EQUIPMENT_TYPESelectQueryByCRTD_DT As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE CRTD_DT=@CRTD_DT"
    Private Const EQUIPMENT_TYPESelectQueryByMDFD_BY As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE MDFD_BY=@MDFD_BY"
    Private Const EQUIPMENT_TYPESelectQueryByMDFD_DT As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE MDFD_DT=@MDFD_DT"
    Private Const EQUIPMENT_TYPESelectQueryByACTV_BT As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE ACTV_BT=@ACTV_BT"
    Private Const EQUIPMENT_TYPESelectQueryByDPT_ID As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE DPT_ID=@DPT_ID"
    Private Const EQUIPMENT_TYPESelectQueryByEQPMNT_CD_CD As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE EQPMNT_CD_CD=@EQPMNT_CD_CD"
    Private Const EQUIPMENT_TYPEInsertQuery As String = "INSERT INTO EQUIPMENT_TYPE(EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD)VALUES(@EQPMNT_TYP_ID,@EQPMNT_TYP_CD,@EQPMNT_TYP_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID,@EQPMNT_CD_CD)"
    Private Const EQUIPMENT_TYPEUpdateQuery As String = "UPDATE EQUIPMENT_TYPE SET EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EQPMNT_TYP_CD=@EQPMNT_TYP_CD, EQPMNT_TYP_DSCRPTN_VC=@EQPMNT_TYP_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID, EQPMNT_CD_CD=@EQPMNT_CD_CD WHERE EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    Private Const EQUIPMENT_TYPEDeleteQuery As String = "DELETE FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_ID=@EQPMNT_TYP_ID"

#End Region

#Region "Constructor.."

    Sub New()
        ds = New EquipmentTypeDataSet
    End Sub

#End Region

#Region "GetEquipmentType() TABLE NAME:EquipmentType"

    Public Function GetEquipmentType(ByVal bv_intDepotID As Integer) As EquipmentTypeDataSet
        Try
            objData = New DataObjects(EquipmentTypeSelectQueryByDPT_ID, EquipmentTypeData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), EquipmentTypeData._EQUIPMENT_TYPE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentTypeByEquipmentTypeType() TABLE NAME:EquipmentType"

    Public Function GetEquipmentGroupByEquipmentTypeId(ByVal bv_strEquipmentTypeId As String, ByVal bv_intDepotID As Integer) As EquipmentTypeDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentTypeData.DPT_ID, bv_intDepotID)
            hshParameters.Add(EquipmentTypeData.EQPMNT_TYP_ID, bv_strEquipmentTypeId)
            objData = New DataObjects(EquipmentGroupSelectQueryByEQPMNT_TYP_ID, hshParameters)
            objData.Fill(CType(ds, DataSet), EquipmentTypeData._EQUIPMENT_TYPE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentTypeByEquipmentTypeType() TABLE NAME:EquipmentType"

    Public Function GetEquipmentTypeByEquipmentTypeCode(ByVal bv_strEquipmentTypeCode As String, ByVal bv_intDepotID As Integer) As EquipmentTypeDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentTypeData.DPT_ID, bv_intDepotID)
            hshParameters.Add(EquipmentTypeData.EQPMNT_TYP_CD, bv_strEquipmentTypeCode)
            objData = New DataObjects(EquipmentTypeSelectQueryByEQPMNT_TYP_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), EquipmentTypeData._EQUIPMENT_TYPE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateEquipmentType() TABLE NAME:EquipmentType"

    Public Function CreateEquipmentType(ByVal bv_strEquipmentTypeCode As String, _
        ByVal bv_strEquipmentTypeDescription As String, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer, _
        ByVal bv_strEquipment_Code As String, _
        ByVal bv_strEquipment_Group As String, _
        ByRef br_objTrans As Transactions) As Long

        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(EquipmentTypeData._EQUIPMENT_TYPE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EquipmentTypeData._EQUIPMENT_TYPE, br_objTrans)
                .Item(EquipmentTypeData.EQPMNT_TYP_ID) = intMax
                .Item(EquipmentTypeData.EQPMNT_TYP_CD) = bv_strEquipmentTypeCode
                If bv_strEquipmentTypeDescription <> Nothing Then
                    .Item(EquipmentTypeData.EQPMNT_TYP_DSCRPTN_VC) = bv_strEquipmentTypeDescription
                Else
                    .Item(EquipmentTypeData.EQPMNT_TYP_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(EquipmentTypeData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(EquipmentTypeData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(EquipmentTypeData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(EquipmentTypeData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(EquipmentTypeData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(EquipmentTypeData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(EquipmentTypeData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(EquipmentTypeData.MDFD_DT) = DBNull.Value
                End If
                .Item(EquipmentTypeData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentTypeData.DPT_ID) = bv_intDepotID

                If bv_strEquipment_Code <> Nothing Then
                    .Item(EquipmentTypeData.EQPMNT_CD_CD) = bv_strEquipment_Code
                Else
                    .Item(EquipmentTypeData.EQPMNT_CD_CD) = DBNull.Value
                End If
                If bv_strEquipment_Group <> Nothing Then
                    .Item(EquipmentTypeData.EQPMNT_GRP_CD) = bv_strEquipment_Group
                Else
                    .Item(EquipmentTypeData.EQPMNT_GRP_CD) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, EquipmentTypeInsertQuery, br_objTrans)
            dr = Nothing
            CreateEquipmentType = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateEquipmentType() TABLE NAME:EquipmentType"

    Public Function UpdateEquipmentType(ByVal bv_i64EquipmentTypeId As Int64, _
        ByVal bv_strEquipmentTypeCode As String, _
        ByVal bv_strEquipmentTypeDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer, _
        ByVal bv_strEQPMNT_CD_CD As String, _
        ByVal bv_strEquipment_Group As String, _
        ByRef br_objTrans As Transactions) As Boolean

        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentTypeData._EQUIPMENT_TYPE).NewRow()
            With dr
                .Item(EquipmentTypeData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                .Item(EquipmentTypeData.EQPMNT_TYP_CD) = bv_strEquipmentTypeCode
                If bv_strEquipmentTypeDescription <> Nothing Then
                    .Item(EquipmentTypeData.EQPMNT_TYP_DSCRPTN_VC) = bv_strEquipmentTypeDescription
                Else
                    .Item(EquipmentTypeData.EQPMNT_TYP_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(EquipmentTypeData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(EquipmentTypeData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(EquipmentTypeData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(EquipmentTypeData.MDFD_DT) = DBNull.Value
                End If
                .Item(EquipmentTypeData.ACTV_BT) = bv_blnActiveBit
                .Item(EquipmentTypeData.DPT_ID) = bv_intDepotID
                If bv_strEQPMNT_CD_CD <> Nothing Then
                    .Item(EquipmentTypeData.EQPMNT_CD_CD) = bv_strEQPMNT_CD_CD
                Else
                    .Item(EquipmentTypeData.EQPMNT_CD_CD) = DBNull.Value
                End If
                If bv_strEquipment_Group <> Nothing Then
                    .Item(EquipmentTypeData.EQPMNT_GRP_CD) = bv_strEquipment_Group
                Else
                    .Item(EquipmentTypeData.EQPMNT_GRP_CD) = DBNull.Value
                End If
            End With
            UpdateEquipmentType = objData.UpdateRow(dr, EquipmentTypeUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "Equipment Type & Code - Merge"



#Region "GetEQUIPMENT_TYPEByEQPMNT_TYP_ID() TABLE NAME:EQUIPMENT_TYPE"

    Public Function GetEQUIPMENT_TYPEByEQPMNT_TYP_ID(ByVal bv_i64EQPMNT_TYP_ID As Int64) As EquipmentTypeDataSet
        Try
            objData = New DataObjects(EQUIPMENT_TYPESelectQueryPk, EquipmentTypeData.EQPMNT_TYP_ID, CStr(bv_i64EQPMNT_TYP_ID))
            objData.Fill(CType(ds, DataSet), EquipmentTypeData._EQUIPMENT_TYPE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEQUIPMENT_TYPE() TABLE NAME:EQUIPMENT_TYPE"

    Public Function GetEQUIPMENT_TYPE() As EquipmentTypeDataSet
        Try
            objData = New DataObjects(EQUIPMENT_TYPESelectQuery)
            objData.Fill(CType(ds, DataSet), EquipmentTypeData._EQUIPMENT_TYPE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEQUIPMENT_TYPEByEQPMNT_TYP_CD() TABLE NAME:EQUIPMENT_TYPE"

    Public Function GetEQUIPMENT_TYPEByEQPMNT_TYP_CD(ByVal bv_strEQPMNT_TYP_CD As String) As EquipmentTypeDataSet
        Try
            objData = New DataObjects(EQUIPMENT_TYPESelectQueryByEQPMNT_TYP_CD, EquipmentTypeData.EQPMNT_TYP_CD, bv_strEQPMNT_TYP_CD)
            objData.Fill(CType(ds, DataSet), EquipmentTypeData._EQUIPMENT_TYPE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEQUIPMENT_TYPEByDPT_ID() TABLE NAME:EQUIPMENT_TYPE"

    Public Function GetEQUIPMENT_TYPEByDPT_ID(ByVal bv_i32DPT_ID As Int32) As EquipmentTypeDataSet
        Try
            objData = New DataObjects(EQUIPMENT_TYPESelectQueryByDPT_ID, EquipmentTypeData.DPT_ID, CStr(bv_i32DPT_ID))
            objData.Fill(CType(ds, DataSet), EquipmentTypeData._EQUIPMENT_TYPE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEQUIPMENT_TYPEByEQPMNT_CD_CD() TABLE NAME:EQUIPMENT_TYPE"

    Public Function GetEQUIPMENT_TYPEByEQPMNT_CD_CD(ByVal bv_strEQPMNT_CD_CD As String) As EquipmentTypeDataSet
        Try
            objData = New DataObjects(EQUIPMENT_TYPESelectQueryByEQPMNT_CD_CD, EquipmentTypeData.EQPMNT_CD_CD, bv_strEQPMNT_CD_CD)
            objData.Fill(CType(ds, DataSet), EquipmentTypeData._EQUIPMENT_TYPE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region



#Region "DeleteEQUIPMENT_TYPE() TABLE NAME:EQUIPMENT_TYPE"

    Public Function DeleteEQUIPMENT_TYPE(ByVal bv_EQPMNT_TYP_ID As Int64, ByRef br_objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(EquipmentTypeData._EQUIPMENT_TYPE).NewRow()
            With dr
                .Item(EquipmentTypeData.EQPMNT_TYP_ID) = bv_EQPMNT_TYP_ID
            End With
            DeleteEQUIPMENT_TYPE = objData.DeleteRow(dr, EQUIPMENT_TYPEDeleteQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#End Region

End Class

#End Region
