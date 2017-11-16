#Region " Locations.vb"
'*********************************************************************************************************************
'Name :
'      Locations.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Locations.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 7:10:45 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Locations"

Public Class Locations

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const LocationSelectQueryByLCTN_CD As String = "SELECT LCTN_ID,LCTN_CD,LCTN_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM LOCATION WHERE LCTN_CD=@LCTN_CD"
    Private Const LocationSelectQueryByDPT_ID As String = "SELECT LCTN_ID,LCTN_CD,LCTN_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM LOCATION WHERE DPT_ID=@DPT_ID"
    Private Const LocationInsertQuery As String = "INSERT INTO LOCATION(LCTN_ID,LCTN_CD,LCTN_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@LCTN_ID,@LCTN_CD,@LCTN_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const LocationUpdateQuery As String = "UPDATE LOCATION SET LCTN_ID=@LCTN_ID, LCTN_CD=@LCTN_CD, LCTN_DSCRPTN_VC=@LCTN_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE LCTN_ID=@LCTN_ID"
    Private ds As LocationDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New LocationDataSet
    End Sub

#End Region

#Region "GetLocation() TABLE NAME:Location"

    Public Function GetLocation(ByVal bv_intDepotID As Integer) As LocationDataSet
        Try
            objData = New DataObjects(LocationSelectQueryByDPT_ID, LocationData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), LocationData._LOCATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetLocationByLocationCode() TABLE NAME:Location"

    Public Function GetLocationByLocationCode(ByVal bv_strLocationCode As String, ByVal bv_intDepotID As Integer) As LocationDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(LocationData.DPT_ID, bv_intDepotID)
            hshParameters.Add(LocationData.LCTN_CD, bv_strLocationCode)
            objData = New DataObjects(LocationSelectQueryByLCTN_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), LocationData._LOCATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateLocation() TABLE NAME:Location"

    Public Function CreateLocation(ByVal bv_strLocationCode As String, _
        ByVal bv_strLocationDescription As String, _
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
            dr = ds.Tables(LocationData._LOCATION).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(LocationData._LOCATION)
                .Item(LocationData.LCTN_ID) = intMax
                .Item(LocationData.LCTN_CD) = bv_strLocationCode
                If bv_strLocationDescription <> Nothing Then
                    .Item(LocationData.LCTN_DSCRPTN_VC) = bv_strLocationDescription
                Else
                    .Item(LocationData.LCTN_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(LocationData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(LocationData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(LocationData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(LocationData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(LocationData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(LocationData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(LocationData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(LocationData.MDFD_DT) = DBNull.Value
                End If
                .Item(LocationData.ACTV_BT) = bv_blnActiveBit
                .Item(LocationData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, LocationInsertQuery)
            dr = Nothing
            CreateLocation = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateLocation() TABLE NAME:Location"

    Public Function UpdateLocation(ByVal bv_i64LocationId As Int64, _
        ByVal bv_strLocationCode As String, _
        ByVal bv_strLocationDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(LocationData._LOCATION).NewRow()
            With dr
                .Item(LocationData.LCTN_ID) = bv_i64LocationId
                .Item(LocationData.LCTN_CD) = bv_strLocationCode
                If bv_strLocationDescription <> Nothing Then
                    .Item(LocationData.LCTN_DSCRPTN_VC) = bv_strLocationDescription
                Else
                    .Item(LocationData.LCTN_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(LocationData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(LocationData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(LocationData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(LocationData.MDFD_DT) = DBNull.Value
                End If
                .Item(LocationData.ACTV_BT) = bv_blnActiveBit
                .Item(LocationData.DPT_ID) = bv_intDepotID
            End With
            UpdateLocation = objData.UpdateRow(dr, LocationUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



End Class

#End Region
