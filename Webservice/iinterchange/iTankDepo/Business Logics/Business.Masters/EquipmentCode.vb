#Region " EquipmentCode.vb"
'*********************************************************************************************************************
'Name :
'      EquipmentCode.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EquipmentCode.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 5:08:50 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class EquipmentCode
    Inherits CodeBase
#Region "pub_EquipmentCodeGetEquipmentCode() TABLE NAME:EquipmentCode"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsEquipmentCodeData As EquipmentCodeDataSet
            Dim objEquipmentCodes As New EquipmentCodes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsEquipmentCodeData = objEquipmentCodes.GetEquipmentCode(intDepotID)
            Return dsEquipmentCodeData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_EquipmentCodeGetEquipmentCodeByEquipmentCodeCode() TABLE NAME:EquipmentCode"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strEquipmentCodeCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsEquipmentCodeData As EquipmentCodeDataSet
            Dim objEquipmentCodes As New EquipmentCodes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsEquipmentCodeData = objEquipmentCodes.GetEquipmentCodeByEquipmentCodeCode(bv_strEquipmentCodeCode, intDepotID)
            If dsEquipmentCodeData.Tables(EquipmentCodeData._EQUIPMENT_CODE).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_EquipmentCodeCreateEquipmentCode() TABLE NAME:EquipmentCode"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strEquipmentCodeCode As String, _
     ByVal bv_strEquipmentCodeDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objEquipmentCode As New EquipmentCodes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objEquipmentCode.CreateEquipmentCode(bv_strEquipmentCodeCode, _
                  bv_strEquipmentCodeDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_EquipmentCodeModifyEquipmentCodeEquipmentCode() TABLE NAME:EquipmentCode"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64EquipmentCodeId As Int32, _
     ByVal bv_strEquipmentCodeCode As String, _
     ByVal bv_strEquipmentCodeDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objEquipmentCode As New EquipmentCodes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objEquipmentCode.UpdateEquipmentCode(bv_i64EquipmentCodeId, _
                bv_strEquipmentCodeCode, bv_strEquipmentCodeDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
