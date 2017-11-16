#Region " REPAIR.vb"
'*********************************************************************************************************************
'Name :
'      REPAIR.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(REPAIR.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/9/2013 9:51:48 AM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class Repair
    Inherits CodeBase
#Region "pub_RepairGetRepair() TABLE NAME:Repair"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsRepairData As RepairDataSet
            Dim objRepairs As New Repairs
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsRepairData = objRepairs.GetRepair(intDepotID)
            Return dsRepairData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_RepairGetRepairByRepairCode() TABLE NAME:Repair"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strRepairCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsRepairData As RepairDataSet
            Dim objRepairs As New Repairs
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsRepairData = objRepairs.GetRepairByRepairCode(bv_strRepairCode, intDepotID)
            If dsRepairData.Tables(RepairData._REPAIR).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_RepairCreateRepair() TABLE NAME:Repair"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strRepairCode As String, _
     ByVal bv_strRepairDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objRepair As New Repairs
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objRepair.CreateRepair(bv_strRepairCode, _
                  bv_strRepairDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_RepairModifyRepairRepair() TABLE NAME:Repair"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64RepairId As Int32, _
     ByVal bv_strRepairCode As String, _
     ByVal bv_strRepairDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objRepair As New Repairs
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objRepair.UpdateRepair(bv_i64RepairId, _
                bv_strRepairCode, bv_strRepairDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
