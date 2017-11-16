#Region " EquipmentStatus.vb"
'*********************************************************************************************************************
'Name :
'      EquipmentStatus.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EquipmentStatus.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 7:06:24 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class EquipmentStatus
    Inherits CodeBase
#Region "pub_EquipmentStatusGetEquipmentStatus() TABLE NAME:EquipmentStatus"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsEquipmentStatusData As EquipmentStatusDataSet
            Dim objEquipmentStatuss As New EquipmentStatuss
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsEquipmentStatusData = objEquipmentStatuss.GetEquipmentStatus(intDepotID)
            Return dsEquipmentStatusData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_EquipmentStatusGetEquipmentStatusByEquipmentStatusCode() TABLE NAME:EquipmentStatus"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strEquipmentStatusCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsEquipmentStatusData As EquipmentStatusDataSet
            Dim objEquipmentStatuss As New EquipmentStatuss
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsEquipmentStatusData = objEquipmentStatuss.GetEquipmentStatusByEquipmentStatusCode(bv_strEquipmentStatusCode, intDepotID)
            If dsEquipmentStatusData.Tables(EquipmentStatusData._EQUIPMENT_STATUS).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_EquipmentStatusCreateEquipmentStatus() TABLE NAME:EquipmentStatus"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strEquipmentStatusCode As String, _
     ByVal bv_strEquipmentStatusDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objEquipmentStatus As New EquipmentStatuss
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objEquipmentStatus.CreateEquipmentStatus(bv_strEquipmentStatusCode, _
                  bv_strEquipmentStatusDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_EquipmentStatusModifyEquipmentStatusEquipmentStatus() TABLE NAME:EquipmentStatus"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64EquipmentStatusId As Int32, _
     ByVal bv_strEquipmentStatusCode As String, _
     ByVal bv_strEquipmentStatusDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objEquipmentStatus As New EquipmentStatuss
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objEquipmentStatus.UpdateEquipmentStatus(bv_i64EquipmentStatusId, _
                bv_strEquipmentStatusCode, bv_strEquipmentStatusDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class