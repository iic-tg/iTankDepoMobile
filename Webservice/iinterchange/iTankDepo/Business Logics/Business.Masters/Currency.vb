#Region " Currency.vb"
'*********************************************************************************************************************
'Name :
'      Currency.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Currency.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 5:06:13 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class Currency
    Inherits CodeBase
#Region "pub_CurrencyGetCurrency() TABLE NAME:Currency"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsCurrencyData As CurrencyDataSet
            Dim objCurrencys As New Currencys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsCurrencyData = objCurrencys.GetCurrency(intDepotID)
            Return dsCurrencyData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CurrencyGetCurrencyByCurrencyCode() TABLE NAME:Currency"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strCurrencyCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsCurrencyData As CurrencyDataSet
            Dim objCurrencys As New Currencys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsCurrencyData = objCurrencys.GetCurrencyByCurrencyCode(bv_strCurrencyCode, intDepotID)
            If dsCurrencyData.Tables(CurrencyData._CURRENCY).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CurrencyCreateCurrency() TABLE NAME:Currency"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strCurrencyCode As String, _
     ByVal bv_strCurrencyDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objCurrency As New Currencys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objCurrency.CreateCurrency(bv_strCurrencyCode, _
                  bv_strCurrencyDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_CurrencyModifyCurrencyCurrency() TABLE NAME:Currency"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64CurrencyId As Int32, _
     ByVal bv_strCurrencyCode As String, _
     ByVal bv_strCurrencyDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objCurrency As New Currencys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objCurrency.UpdateCurrency(bv_i64CurrencyId, _
                bv_strCurrencyCode, bv_strCurrencyDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
