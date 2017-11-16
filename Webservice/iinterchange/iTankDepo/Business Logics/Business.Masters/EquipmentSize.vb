#Region " EquipmentSize.vb"
'*********************************************************************************************************************
'Name :
'      EquipmentSize.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EquipmentSize.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 7:05:17 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class EquipmentSize
    Inherits CodeBase
#Region "pub_EquipmentSizeGetEquipmentSize() TABLE NAME:EquipmentSize"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsEquipmentSizeData As EquipmentSizeDataSet
            Dim objEquipmentSizes As New EquipmentSizes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsEquipmentSizeData = objEquipmentSizes.GetEquipmentSize(intDepotID)
            Return dsEquipmentSizeData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_EquipmentSizeGetEquipmentSizeByEquipmentSizeCode() TABLE NAME:EquipmentSize"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strEquipmentSizeCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsEquipmentSizeData As EquipmentSizeDataSet
            Dim objEquipmentSizes As New EquipmentSizes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsEquipmentSizeData = objEquipmentSizes.GetEquipmentSizeByEquipmentSizeCode(bv_strEquipmentSizeCode, intDepotID)
            If dsEquipmentSizeData.Tables(EquipmentSizeData._EQUIPMENT_SIZE).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_EquipmentSizeCreateEquipmentSize() TABLE NAME:EquipmentSize"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strEquipmentSizeCode As String, _
     ByVal bv_strEquipmentSizeDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objEquipmentSize As New EquipmentSizes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objEquipmentSize.CreateEquipmentSize(bv_strEquipmentSizeCode, _
                  bv_strEquipmentSizeDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_EquipmentSizeModifyEquipmentSizeEquipmentSize() TABLE NAME:EquipmentSize"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64EquipmentSizeId As Int32, _
     ByVal bv_strEquipmentSizeCode As String, _
     ByVal bv_strEquipmentSizeDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objEquipmentSize As New EquipmentSizes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objEquipmentSize.UpdateEquipmentSize(bv_i64EquipmentSizeId, _
                bv_strEquipmentSizeCode, bv_strEquipmentSizeDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region


End Class
