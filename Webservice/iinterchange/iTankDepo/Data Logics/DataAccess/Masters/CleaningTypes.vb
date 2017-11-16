#Region " CleaningTypes.vb"
'*********************************************************************************************************************
'Name :
'      CleaningTypes.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(CleaningTypes.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 4:20:45 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "CleaningTypes"

Public Class CleaningTypes

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const CleaningTypeSelectQueryByCLNNG_TYP_CD As String = "SELECT CLNNG_TYP_ID,CLNNG_TYP_CD,CLNNG_TYP_DSCRPTN_VC,DFLT_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM CLEANING_TYPE WHERE CLNNG_TYP_CD=@CLNNG_TYP_CD"
    Private Const CleaningTypeSelectQueryByDPT_ID As String = "SELECT CLNNG_TYP_ID,CLNNG_TYP_CD,CLNNG_TYP_DSCRPTN_VC,DFLT_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM CLEANING_TYPE WHERE DPT_ID=@DPT_ID"
    Private Const CleaningTypeInsertQuery As String = "INSERT INTO CLEANING_TYPE(CLNNG_TYP_ID,CLNNG_TYP_CD,CLNNG_TYP_DSCRPTN_VC,DFLT_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@CLNNG_TYP_ID,@CLNNG_TYP_CD,@CLNNG_TYP_DSCRPTN_VC,@DFLT_BT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const CleaningTypeUpdateQuery As String = "UPDATE CLEANING_TYPE SET CLNNG_TYP_ID=@CLNNG_TYP_ID, CLNNG_TYP_CD=@CLNNG_TYP_CD, CLNNG_TYP_DSCRPTN_VC=@CLNNG_TYP_DSCRPTN_VC,DFLT_BT=@DFLT_BT, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE CLNNG_TYP_ID=@CLNNG_TYP_ID"
    Private ds As CleaningTypeDataSet
#End Region
    Private Const CleaningTypeSelectQueryByCleaningTypeCode As String = "SELECT CLNNG_TYP_ID,CLNNG_TYP_CD,CLNNG_TYP_DSCRPTN_VC,DFLT_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID   FROM CLEANING_TYPE a WHERE DPT_ID=@DPT_ID AND CLNNG_TYP_CD=@CLNNG_TYP_CD"

#Region "Constructor.."

    Sub New()
        ds = New CleaningTypeDataSet
    End Sub

#End Region

#Region "GetCleaningType() TABLE NAME:CleaningType"

    Public Function GetCleaningType(ByVal bv_intDepotID As Integer) As CleaningTypeDataSet
        Try
            objData = New DataObjects(CleaningTypeSelectQueryByDPT_ID, CleaningTypeData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), CleaningTypeData._CLEANING_TYPE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningTypeByCleaningTypeCode() TABLE NAME:UNIT"

    Public Function GetCleaningTypeByCleaningTypeCode(ByVal bv_strCleaningTypeCode As String, ByVal bv_intDepotID As Integer) As CleaningTypeDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CleaningTypeData.DPT_ID, bv_intDepotID)
            hshParameters.Add(CleaningTypeData.CLNNG_TYP_CD, bv_strCleaningTypeCode)
            objData = New DataObjects(CleaningTypeSelectQueryByCLNNG_TYP_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), CleaningTypeData._CLEANING_TYPE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateCleaningType() TABLE NAME:CleaningType"

    Public Function CreateCleaningType(ByVal bv_strCleaningTypeCode As String, _
        ByVal bv_strCleaningTypeDescription As String, _
        ByVal bv_blnDefaultBit As Boolean, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer, _
        ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CleaningTypeData._CLEANING_TYPE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CleaningTypeData._CLEANING_TYPE, br_objTransaction)
                .Item(CleaningTypeData.CLNNG_TYP_ID) = intMax
                .Item(CleaningTypeData.CLNNG_TYP_CD) = bv_strCleaningTypeCode
                If bv_strCleaningTypeDescription <> Nothing Then
                    .Item(CleaningTypeData.CLNNG_TYP_DSCRPTN_VC) = bv_strCleaningTypeDescription
                Else
                    .Item(CleaningTypeData.CLNNG_TYP_DSCRPTN_VC) = DBNull.Value
                End If
                .Item(CleaningTypeData.DFLT_BT) = bv_blnDefaultBit
                If bv_strCreatedBy <> Nothing Then
                    .Item(CleaningTypeData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(CleaningTypeData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(CleaningTypeData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(CleaningTypeData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(CleaningTypeData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(CleaningTypeData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(CleaningTypeData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(CleaningTypeData.MDFD_DT) = DBNull.Value
                End If
                .Item(CleaningTypeData.ACTV_BT) = bv_blnActiveBit
                .Item(CleaningTypeData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, CleaningTypeInsertQuery, br_objTransaction)
            dr = Nothing
            CreateCleaningType = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateCleaningType() TABLE NAME:CleaningType"

    Public Function UpdateCleaningType(ByVal bv_i64CleaningTypeId As Int64, _
        ByVal bv_strCleaningTypeCode As String, _
        ByVal bv_strCleaningTypeDescription As String, _
        ByVal bv_blnDefaultBit As Boolean, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer, _
        ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningTypeData._CLEANING_TYPE).NewRow()
            With dr
                .Item(CleaningTypeData.CLNNG_TYP_ID) = bv_i64CleaningTypeId
                .Item(CleaningTypeData.CLNNG_TYP_CD) = bv_strCleaningTypeCode
                If bv_strCleaningTypeDescription <> Nothing Then
                    .Item(CleaningTypeData.CLNNG_TYP_DSCRPTN_VC) = bv_strCleaningTypeDescription
                Else
                    .Item(CleaningTypeData.CLNNG_TYP_DSCRPTN_VC) = DBNull.Value
                End If
                .Item(CleaningTypeData.DFLT_BT) = bv_blnDefaultBit
                If bv_strModifiedBy <> Nothing Then
                    .Item(CleaningTypeData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(CleaningTypeData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(CleaningTypeData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(CleaningTypeData.MDFD_DT) = DBNull.Value
                End If
                .Item(CleaningTypeData.ACTV_BT) = bv_blnActiveBit
                .Item(CleaningTypeData.DPT_ID) = bv_intDepotID
            End With
            UpdateCleaningType = objData.UpdateRow(dr, CleaningTypeUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningTypeByCleaningTypeCode() TABLE NAME:Sub_Item"

    Public Function GetCleaningTypeByCleaningTypeCode(ByVal bv_strCleaningTypeCode As String, ByVal bv_intDepotId As Int64) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CleaningTypeData.CLNNG_TYP_CD, bv_strCleaningTypeCode)
            hshParameters.Add(CleaningTypeData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(CleaningTypeSelectQueryByCleaningTypeCode, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCleaningTypeByDepotID() TABLE NAME:Sub_Item"

    Public Function GetCleaningTypeByDepotID(ByVal bv_i32DepotID As Int32) As CleaningTypeDataSet
        Try
            objData = New DataObjects(CleaningTypeSelectQueryByDPT_ID, CleaningTypeData.DPT_ID, CStr(bv_i32DepotID))
            objData.Fill(CType(ds, DataSet), CleaningTypeData._CLEANING_TYPE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class

#End Region
