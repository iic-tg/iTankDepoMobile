#Region " Country.vb"
'*********************************************************************************************************************
'Name :
'      Country.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Country.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 5:05:11 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class Country
    Inherits CodeBase

#Region "pub_CountryGetCountry() TABLE NAME:Country"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsCountryData As CountryDataSet
            Dim objCountrys As New Countrys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsCountryData = objCountrys.GetCountry(intDepotID)
            Return dsCountryData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CountryGetCountryByCountryCode() TABLE NAME:UNIT"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strCountryCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsCountryData As CountryDataSet
            Dim objCountrys As New Countrys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsCountryData = objCountrys.GetCountryByCountryCode(bv_strCountryCode, intDepotID)
            If dsCountryData.Tables(CountryData._COUNTRY).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CountryCreateCountry() TABLE NAME:Country"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strCountryCode As String, _
     ByVal bv_strCountryDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objCountry As New Countrys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objCountry.CreateCountry(bv_strCountryCode, _
                  bv_strCountryDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_CountryModifyCountryCountry() TABLE NAME:Country"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64CountryId As Int32, _
     ByVal bv_strCountryCode As String, _
     ByVal bv_strCountryDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objCountry As New Countrys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objCountry.UpdateCountry(bv_i64CountryId, _
                bv_strCountryCode, bv_strCountryDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
