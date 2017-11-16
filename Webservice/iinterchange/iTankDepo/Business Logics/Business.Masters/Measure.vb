#Region " Measure.vb"
'*********************************************************************************************************************
'Name :
'      Measure.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Measure.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/9/2013 9:48:47 AM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class Measure
    Inherits CodeBase
#Region "pub_MeasureGetMeasure() TABLE NAME:Measure"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsMeasureData As MeasureDataSet
            Dim objMeasures As New Measures
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsMeasureData = objMeasures.GetMeasure(intDepotID)
            Return dsMeasureData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_MeasureGetMeasureByMeasureCode() TABLE NAME:Measure"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strMeasureCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsMeasureData As MeasureDataSet
            Dim objMeasures As New Measures
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsMeasureData = objMeasures.GetMeasureByMeasureCode(bv_strMeasureCode, intDepotID)
            If dsMeasureData.Tables(MeasureData._MEASURE).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_MeasureCreateMeasure() TABLE NAME:Measure"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strMeasureCode As String, _
     ByVal bv_strMeasureDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objMeasure As New Measures
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objMeasure.CreateMeasure(bv_strMeasureCode, _
                  bv_strMeasureDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_MeasureModifyMeasureMeasure() TABLE NAME:Measure"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64MeasureId As Int32, _
     ByVal bv_strMeasureCode As String, _
     ByVal bv_strMeasureDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objMeasure As New Measures
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objMeasure.UpdateMeasure(bv_i64MeasureId, _
                bv_strMeasureCode, bv_strMeasureDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class

