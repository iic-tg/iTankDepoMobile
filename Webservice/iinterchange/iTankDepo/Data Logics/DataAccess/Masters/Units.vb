#Region " Units.vb"
'*********************************************************************************************************************
'Name :
'      Units.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Units.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/7/2013 12:25:39 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Units"

Public Class Units

#Region "Declaration Part.. "

	Dim objData As DataObjects
    Private Const UnitSelectQuery As String = "SELECT UNT_ID,UNT_CD,UNT_DSCRPTN_VC,CRTD_BY ,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM UNIT WHERE DPT_ID=@DPT_ID"
    Private Const UNITSelectQueryByUnitCode As String = "SELECT UNT_ID,UNT_CD,UNT_DSCRPTN_VC,CRTD_BY ,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM UNIT WHERE UNT_CD=@UNT_CD"
    Private Const UnitInsertQuery As String = "INSERT INTO UNIT(UNT_ID,UNT_CD,UNT_DSCRPTN_VC,CRTD_BY ,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@UNT_ID,@UNT_CD,@UNT_DSCRPTN_VC,@CRTD_BY ,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const UnitUpdateQuery As String = "UPDATE Unit SET UNT_ID=@UNT_ID, UNT_CD=@UNT_CD, UNT_DSCRPTN_VC=@UNT_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT,DPT_ID=@DPT_ID WHERE UNT_ID=@UNT_ID"
    Private ds As UnitDataSet
# End Region

#Region "Constructor.."

	Sub New()
        ds = New UnitDataSet
	End Sub

#End Region

#Region "GetUnit() TABLE NAME:Unit"

    Public Function GetUnit(ByVal bv_intDepotID As Integer) As UnitDataSet
        Try
            objData = New DataObjects(UnitSelectQuery, UnitData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), UnitData._UNIT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetUnitByUnitCode() TABLE NAME:UNIT"

    Public Function GetUnitByUnitCode(ByVal bv_strUnitCode As String, ByVal bv_intDepotID As Integer) As UnitDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(UnitData.DPT_ID, bv_intDepotID)
            hshParameters.Add(UnitData.UNT_CD, bv_strUnitCode)
            objData = New DataObjects(UNITSelectQueryByUnitCode, hshParameters)
            objData.Fill(CType(ds, DataSet), UnitData._UNIT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateUnit() TABLE NAME:Unit"

    Public Function CreateUnit(ByVal bv_strUnitCode As String, _
        ByVal bv_strUnitDescription As String, _
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
            dr = ds.Tables(UnitData._UNIT).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(UnitData._UNIT)
                .Item(UnitData.UNT_ID) = intMax
                .Item(UnitData.UNT_CD) = bv_strUnitCode
                If bv_strUnitDescription <> Nothing Then
                    .Item(UnitData.UNT_DSCRPTN_VC) = bv_strUnitDescription
                Else
                    .Item(UnitData.UNT_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(UnitData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(UnitData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(UnitData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(UnitData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(UnitData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(UnitData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(UnitData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(UnitData.MDFD_DT) = DBNull.Value
                End If
                .Item(UnitData.ACTV_BT) = bv_blnActiveBit
                .Item(UnitData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, UnitInsertQuery)
            dr = Nothing
            CreateUnit = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateUnit() TABLE NAME:Unit"

    Public Function UpdateUnit(ByVal bv_i64UnitId As Int64, _
        ByVal bv_strUnitCode As String, _
        ByVal bv_strUnitDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(UnitData._UNIT).NewRow()
            With dr
                .Item(UnitData.UNT_ID) = bv_i64UnitId
                .Item(UnitData.UNT_CD) = bv_strUnitCode
                If bv_strUnitDescription <> Nothing Then
                    .Item(UnitData.UNT_DSCRPTN_VC) = bv_strUnitDescription
                Else
                    .Item(UnitData.UNT_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(UnitData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(UnitData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(UnitData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(UnitData.MDFD_DT) = DBNull.Value
                End If
                .Item(UnitData.ACTV_BT) = bv_blnActiveBit
                .Item(UnitData.DPT_ID) = bv_intDepotID
            End With
            UpdateUnit = objData.UpdateRow(dr, UnitUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class

#End Region
