#Region " Countrys.vb"
'*********************************************************************************************************************
'Name :
'      Countrys.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Countrys.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 5:05:11 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Countrys"

Public Class Countrys

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const CountrySelectQueryByCNTRY_CD As String = "SELECT CNTRY_ID,CNTRY_CD,CNTRY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Country WHERE CNTRY_CD=@CNTRY_CD"
    Private Const CountrySelectQueryByDPT_ID As String = "SELECT CNTRY_ID,CNTRY_CD,CNTRY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Country WHERE DPT_ID=@DPT_ID"
    Private Const CountryInsertQuery As String = "INSERT INTO Country(CNTRY_ID,CNTRY_CD,CNTRY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@CNTRY_ID,@CNTRY_CD,@CNTRY_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const CountryUpdateQuery As String = "UPDATE Country SET CNTRY_ID=@CNTRY_ID, CNTRY_CD=@CNTRY_CD, CNTRY_DSCRPTN_VC=@CNTRY_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE CNTRY_ID=@CNTRY_ID"
    Private ds As CountryDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New CountryDataSet
    End Sub

#End Region

#Region "GetCountry() TABLE NAME:Country"

    Public Function GetCountry(ByVal bv_intDepotID As Integer) As CountryDataSet
        Try
            objData = New DataObjects(CountrySelectQueryByDPT_ID, CountryData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), CountryData._COUNTRY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCountryByCountryCode() TABLE NAME:UNIT"

    Public Function GetCountryByCountryCode(ByVal bv_strCountryCode As String, ByVal bv_intDepotID As Integer) As CountryDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CountryData.DPT_ID, bv_intDepotID)
            hshParameters.Add(CountryData.CNTRY_CD, bv_strCountryCode)
            objData = New DataObjects(CountrySelectQueryByCNTRY_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), CountryData._COUNTRY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateCountry() TABLE NAME:Country"

    Public Function CreateCountry(ByVal bv_strCountryCode As String, _
        ByVal bv_strCountryDescription As String, _
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
            dr = ds.Tables(CountryData._COUNTRY).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CountryData._COUNTRY)
                .Item(CountryData.CNTRY_ID) = intMax
                .Item(CountryData.CNTRY_CD) = bv_strCountryCode
                If bv_strCountryDescription <> Nothing Then
                    .Item(CountryData.CNTRY_DSCRPTN_VC) = bv_strCountryDescription
                Else
                    .Item(CountryData.CNTRY_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(CountryData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(CountryData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(CountryData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(CountryData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(CountryData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(CountryData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(CountryData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(CountryData.MDFD_DT) = DBNull.Value
                End If
                .Item(CountryData.ACTV_BT) = bv_blnActiveBit
                .Item(CountryData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, CountryInsertQuery)
            dr = Nothing
            CreateCountry = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateCountry() TABLE NAME:Country"

    Public Function UpdateCountry(ByVal bv_i64CountryId As Int64, _
        ByVal bv_strCountryCode As String, _
        ByVal bv_strCountryDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CountryData._COUNTRY).NewRow()
            With dr
                .Item(CountryData.CNTRY_ID) = bv_i64CountryId
                .Item(CountryData.CNTRY_CD) = bv_strCountryCode
                If bv_strCountryDescription <> Nothing Then
                    .Item(CountryData.CNTRY_DSCRPTN_VC) = bv_strCountryDescription
                Else
                    .Item(CountryData.CNTRY_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(CountryData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(CountryData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(CountryData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(CountryData.MDFD_DT) = DBNull.Value
                End If
                .Item(CountryData.ACTV_BT) = bv_blnActiveBit
                .Item(CountryData.DPT_ID) = bv_intDepotID
            End With
            UpdateCountry = objData.UpdateRow(dr, CountryUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class

#End Region
