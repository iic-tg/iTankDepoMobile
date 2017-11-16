#Region " Unit.vb"
'*********************************************************************************************************************
'Name :
'      Unit.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Unit.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/7/2013 12:25:39 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class Unit
    Inherits CodeBase
#Region "pub_UnitGetUnit() TABLE NAME:Unit"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsUnitData As UnitDataSet
            Dim objUnits As New Units
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsUnitData = objUnits.GetUnit(intDepotID)
            Return dsUnitData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UnitGetUnitByUnitCode() TABLE NAME:UNIT"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strUnitCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsUnitData As UnitDataSet
            Dim objUnits As New Units
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsUnitData = objUnits.GetUnitByUnitCode(bv_strUnitCode, intDepotID)
            If dsUnitData.Tables(UnitData._UNIT).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UnitCreateUnit() TABLE NAME:Unit"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strUnitCode As String, _
     ByVal bv_strUnitDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objUnit As New Units
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objUnit.CreateUnit(bv_strUnitCode, _
                  bv_strUnitDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_UnitModifyUnitUnit() TABLE NAME:Unit"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64UnitId As Int32, _
     ByVal bv_strUnitCode As String, _
     ByVal bv_strUnitDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objUnit As New Units
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objUnit.UpdateUnit(bv_i64UnitId, _
                bv_strUnitCode, bv_strUnitDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
