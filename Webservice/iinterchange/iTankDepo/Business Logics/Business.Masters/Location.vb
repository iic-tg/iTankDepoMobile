#Region " Location.vb"
'*********************************************************************************************************************
'Name :
'      Location.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Location.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 7:10:45 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class Location
    Inherits CodeBase
#Region "pub_LocationGetLocation() TABLE NAME:Location"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsLocationData As LocationDataSet
            Dim objLocations As New Locations
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsLocationData = objLocations.GetLocation(intDepotID)
            Return dsLocationData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_LocationGetLocationByLocationCode() TABLE NAME:Location"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strLocationCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsLocationData As LocationDataSet
            Dim objLocations As New Locations
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsLocationData = objLocations.GetLocationByLocationCode(bv_strLocationCode, intDepotID)
            If dsLocationData.Tables(LocationData._LOCATION).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_LocationCreateLocation() TABLE NAME:Location"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strLocationCode As String, _
     ByVal bv_strLocationDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objLocation As New Locations
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objLocation.CreateLocation(bv_strLocationCode, _
                  bv_strLocationDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_LocationModifyLocationLocation() TABLE NAME:Location"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64LocationId As Int32, _
     ByVal bv_strLocationCode As String, _
     ByVal bv_strLocationDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objLocation As New Locations
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objLocation.UpdateLocation(bv_i64LocationId, _
                bv_strLocationCode, bv_strLocationDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class


