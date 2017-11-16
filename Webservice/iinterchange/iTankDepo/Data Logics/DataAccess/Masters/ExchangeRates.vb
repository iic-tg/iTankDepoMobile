#Region " ExchangeRates.vb"
'*********************************************************************************************************************
'Name :
'      ExchangeRates.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(ExchangeRates.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/9/2013 4:11:58 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "ExchangeRates"

Public Class ExchangeRates

#Region "Declaration Part.. "

	Dim objData As DataObjects
    Private Const V_EXCHANGE_RATESelectQueryByDPT_ID As String = "SELECT EXCHNG_RT_ID,FRM_CRRNCY_ID,FRM_CRRNCY_CD,TO_CRRNCY_ID,TO_CRRNCY_CD,EXCHNG_RT_PR_UNT_NC,WTH_EFFCT_FRM_DT,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM V_EXCHANGE_RATE WHERE DPT_ID=@DPT_ID"
	Private Const EXCHANGE_RATEInsertQuery As String= "INSERT INTO EXCHANGE_RATE(EXCHNG_RT_ID,FRM_CRRNCY_ID,TO_CRRNCY_ID,EXCHNG_RT_PR_UNT_NC,WTH_EFFCT_FRM_DT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@EXCHNG_RT_ID,@FRM_CRRNCY_ID,@TO_CRRNCY_ID,@EXCHNG_RT_PR_UNT_NC,@WTH_EFFCT_FRM_DT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
	Private Const EXCHANGE_RATEUpdateQuery As String = "UPDATE EXCHANGE_RATE SET EXCHNG_RT_ID=@EXCHNG_RT_ID, FRM_CRRNCY_ID=@FRM_CRRNCY_ID, TO_CRRNCY_ID=@TO_CRRNCY_ID, EXCHNG_RT_PR_UNT_NC=@EXCHNG_RT_PR_UNT_NC, WTH_EFFCT_FRM_DT=@WTH_EFFCT_FRM_DT, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE EXCHNG_RT_ID=@EXCHNG_RT_ID"
    Private ds As ExchangeRateDataSet
# End Region

#Region "Constructor.."
    Private _updateExchangeRate As Boolean

    Sub New()
        ds = New ExchangeRateDataSet
    End Sub

#End Region

#Region "GetEXCHANGE_RATEByDPT_ID() TABLE NAME:EXCHANGE_RATE"

    Property UpdateExchangeRate(ByVal p1 As Integer, ByVal p2 As Integer, ByVal p3 As Integer, ByVal p4 As Decimal, ByVal p5 As Date, ByVal bv_strModifiedBy As String, ByVal bv_datModifieddate As Date, ByVal p8 As Boolean, ByVal p9 As Object) As Boolean
        Get
            Return _updateExchangeRate
        End Get
        Set(ByVal value As Boolean)
            _updateExchangeRate = value
        End Set
    End Property

    Public Function GetEXCHANGE_RATEByDPT_ID(ByVal bv_i32DPT_ID As Int32) As ExchangeRateDataSet
        Try
            objData = New DataObjects(V_EXCHANGE_RATESelectQueryByDPT_ID, ExchangeRateData.DPT_ID, CStr(bv_i32DPT_ID))
            objData.Fill(CType(ds, DataSet), ExchangeRateData._EXCHANGE_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateEXCHANGE_RATE() TABLE NAME:EXCHANGE_RATE"

	Public Function CreateEXCHANGE_RATE(ByVal bv_i64FRM_CRRNCY_ID As Int64,  _
					ByVal bv_i64TO_CRRNCY_ID As Int64,  _
					ByVal bv_strEXCHNG_RT_PR_UNT_NC As Decimal,  _
					ByVal bv_datWTH_EFFCT_FRM_DT As DateTime,  _
					ByVal bv_strCRTD_BY As String,  _
					ByVal bv_datCRTD_DT As DateTime,  _
					ByVal bv_strMDFD_BY As String,  _
					ByVal bv_datMDFD_DT As DateTime,  _
					ByVal bv_blnACTV_BT As Boolean,  _
					ByVal bv_i32DPT_ID As Int32) As Long
	Try
	Dim dr As DataRow
	Dim intMax As Long
	objData = New DataObjects()
	dr =  ds.Tables(ExchangeRateData._EXCHANGE_RATE).NewRow()
	With dr
                intMax = CommonUIs.GetIdentityValue(ExchangeRateData._EXCHANGE_RATE)
		.Item(ExchangeRateData.EXCHNG_RT_ID) = intMax
		.Item(ExchangeRateData.FRM_CRRNCY_ID)=bv_i64FRM_CRRNCY_ID
		.Item(ExchangeRateData.TO_CRRNCY_ID)=bv_i64TO_CRRNCY_ID
		.Item(ExchangeRateData.EXCHNG_RT_PR_UNT_NC)=bv_strEXCHNG_RT_PR_UNT_NC
		.Item(ExchangeRateData.WTH_EFFCT_FRM_DT)=bv_datWTH_EFFCT_FRM_DT
		If bv_strCRTD_BY <> Nothing Then
			.Item(ExchangeRateData.CRTD_BY)=bv_strCRTD_BY
		Else
			.Item(ExchangeRateData.CRTD_BY)=DBNull.Value
		End if
		If bv_datCRTD_DT <> Nothing Then
			.Item(ExchangeRateData.CRTD_DT)=bv_datCRTD_DT
		Else
			.Item(ExchangeRateData.CRTD_DT)=DBNull.Value
		End if
		If bv_strMDFD_BY <> Nothing Then
			.Item(ExchangeRateData.MDFD_BY)=bv_strMDFD_BY
		Else
			.Item(ExchangeRateData.MDFD_BY)=DBNull.Value
		End if
		If bv_datMDFD_DT <> Nothing Then
			.Item(ExchangeRateData.MDFD_DT)=bv_datMDFD_DT
		Else
			.Item(ExchangeRateData.MDFD_DT)=DBNull.Value
		End if
		.Item(ExchangeRateData.ACTV_BT)=bv_blnACTV_BT
		If bv_i32DPT_ID <> 0 Then
			.Item(ExchangeRateData.DPT_ID)=bv_i32DPT_ID
		Else
			.Item(ExchangeRateData.DPT_ID)=DBNull.Value
		End if
	End With
	objData.InsertRow(dr,EXCHANGE_RATEInsertQuery)
	dr = Nothing
	CreateEXCHANGE_RATE = intMax
	Catch ex As Exception
		Throw ex
	End Try
	End Function

#End Region

#Region "UpdateEXCHANGE_RATE() TABLE NAME:EXCHANGE_RATE"

	Public Function UpdateEXCHANGE_RATE(ByVal bv_i64EXCHNG_RT_ID As Int64,  _
					ByVal bv_i64FRM_CRRNCY_ID As Int64,  _
					ByVal bv_i64TO_CRRNCY_ID As Int64,  _
					ByVal bv_strEXCHNG_RT_PR_UNT_NC As Decimal,  _
					ByVal bv_datWTH_EFFCT_FRM_DT As DateTime,  _
					ByVal bv_strMDFD_BY As String,  _
					ByVal bv_datMDFD_DT As DateTime,  _
					ByVal bv_blnACTV_BT As Boolean,  _
					ByVal bv_i32DPT_ID As Int32) As Boolean
	Try
	Dim dr As DataRow
	objData = New DataObjects()
	dr =  ds.Tables(ExchangeRateData._EXCHANGE_RATE).NewRow()
	With dr
		.Item(ExchangeRateData.EXCHNG_RT_ID)=bv_i64EXCHNG_RT_ID
		.Item(ExchangeRateData.FRM_CRRNCY_ID)=bv_i64FRM_CRRNCY_ID
		.Item(ExchangeRateData.TO_CRRNCY_ID)=bv_i64TO_CRRNCY_ID
		.Item(ExchangeRateData.EXCHNG_RT_PR_UNT_NC)=bv_strEXCHNG_RT_PR_UNT_NC
		.Item(ExchangeRateData.WTH_EFFCT_FRM_DT)=bv_datWTH_EFFCT_FRM_DT
		If bv_strMDFD_BY <> Nothing Then
			.Item(ExchangeRateData.MDFD_BY)=bv_strMDFD_BY
		Else
			.Item(ExchangeRateData.MDFD_BY)=DBNull.Value
		End if
		If bv_datMDFD_DT <> Nothing Then
			.Item(ExchangeRateData.MDFD_DT)=bv_datMDFD_DT
		Else
			.Item(ExchangeRateData.MDFD_DT)=DBNull.Value
		End if
		.Item(ExchangeRateData.ACTV_BT)=bv_blnACTV_BT
		If bv_i32DPT_ID <> 0 Then
			.Item(ExchangeRateData.DPT_ID)=bv_i32DPT_ID
		Else
			.Item(ExchangeRateData.DPT_ID)=DBNull.Value
		End if
	End With
	UpdateEXCHANGE_RATE = objData.UpdateRow(dr,EXCHANGE_RATEUpdateQuery)
	dr = Nothing
	Catch ex As Exception
		Throw ex
	End Try
	End Function
#End Region

End Class

#End Region
