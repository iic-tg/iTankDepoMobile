#Region " Currencys.vb"
'*********************************************************************************************************************
'Name :
'      Currencys.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Currencys.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 5:06:13 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Currencys"

Public Class Currencys

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const CurrencySelectQueryByCRRNCY_CD As String = "SELECT CRRNCY_ID,CRRNCY_CD,CRRNCY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Currency WHERE CRRNCY_CD=@CRRNCY_CD"
    Private Const CurrencySelectQueryByDPT_ID As String = "SELECT CRRNCY_ID,CRRNCY_CD,CRRNCY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Currency WHERE DPT_ID=@DPT_ID"
    Private Const CurrencyInsertQuery As String = "INSERT INTO Currency(CRRNCY_ID,CRRNCY_CD,CRRNCY_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@CRRNCY_ID,@CRRNCY_CD,@CRRNCY_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const CurrencyUpdateQuery As String = "UPDATE Currency SET CRRNCY_ID=@CRRNCY_ID, CRRNCY_CD=@CRRNCY_CD, CRRNCY_DSCRPTN_VC=@CRRNCY_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE CRRNCY_ID=@CRRNCY_ID"
    Private ds As CurrencyDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New CurrencyDataSet
    End Sub

#End Region

#Region "GetCurrency() TABLE NAME:Currency"

    Public Function GetCurrency(ByVal bv_intDepotID As Integer) As CurrencyDataSet
        Try
            objData = New DataObjects(CurrencySelectQueryByDPT_ID, CurrencyData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), CurrencyData._CURRENCY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCurrencyByCurrencyCode() TABLE NAME:UNIT"

    Public Function GetCurrencyByCurrencyCode(ByVal bv_strCurrencyCode As String, ByVal bv_intDepotID As Integer) As CurrencyDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CurrencyData.DPT_ID, bv_intDepotID)
            hshParameters.Add(CurrencyData.CRRNCY_CD, bv_strCurrencyCode)
            objData = New DataObjects(CurrencySelectQueryByCRRNCY_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), CurrencyData._CURRENCY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateCurrency() TABLE NAME:Currency"

    Public Function CreateCurrency(ByVal bv_strCurrencyCode As String, _
        ByVal bv_strCurrencyDescription As String, _
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
            dr = ds.Tables(CurrencyData._CURRENCY).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CurrencyData._CURRENCY)
                .Item(CurrencyData.CRRNCY_ID) = intMax
                .Item(CurrencyData.CRRNCY_CD) = bv_strCurrencyCode
                If bv_strCurrencyDescription <> Nothing Then
                    .Item(CurrencyData.CRRNCY_DSCRPTN_VC) = bv_strCurrencyDescription
                Else
                    .Item(CurrencyData.CRRNCY_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(CurrencyData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(CurrencyData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(CurrencyData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(CurrencyData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(CurrencyData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(CurrencyData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(CurrencyData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(CurrencyData.MDFD_DT) = DBNull.Value
                End If
                .Item(CurrencyData.ACTV_BT) = bv_blnActiveBit
                .Item(CurrencyData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, CurrencyInsertQuery)
            dr = Nothing
            CreateCurrency = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateCurrency() TABLE NAME:Currency"

    Public Function UpdateCurrency(ByVal bv_i64CurrencyId As Int64, _
        ByVal bv_strCurrencyCode As String, _
        ByVal bv_strCurrencyDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CurrencyData._CURRENCY).NewRow()
            With dr
                .Item(CurrencyData.CRRNCY_ID) = bv_i64CurrencyId
                .Item(CurrencyData.CRRNCY_CD) = bv_strCurrencyCode
                If bv_strCurrencyDescription <> Nothing Then
                    .Item(CurrencyData.CRRNCY_DSCRPTN_VC) = bv_strCurrencyDescription
                Else
                    .Item(CurrencyData.CRRNCY_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(CurrencyData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(CurrencyData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(CurrencyData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(CurrencyData.MDFD_DT) = DBNull.Value
                End If
                .Item(CurrencyData.ACTV_BT) = bv_blnActiveBit
                .Item(CurrencyData.DPT_ID) = bv_intDepotID
            End With
            UpdateCurrency = objData.UpdateRow(dr, CurrencyUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class

#End Region
