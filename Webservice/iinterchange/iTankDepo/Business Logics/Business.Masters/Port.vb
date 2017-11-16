#Region " Port.vb"
'*********************************************************************************************************************
'Name :
'      Port.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Port.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/9/2013 9:53:16 AM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class Port
    Inherits CodeBase
#Region "pub_PortGetPort() TABLE NAME:Port"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsPortData As PortDataSet
            Dim objPorts As New Ports
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsPortData = objPorts.GetPort(intDepotID)
            Return dsPortData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_PortGetPortByPortCode() TABLE NAME:Port"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strPortCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsPortData As PortDataSet
            Dim objPorts As New Ports
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsPortData = objPorts.GetPortByPortCode(bv_strPortCode, intDepotID)
            If dsPortData.Tables(PortData._Port).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_PortCreatePort() TABLE NAME:Port"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strPortCode As String, _
     ByVal bv_strPortDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objPort As New Ports
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objPort.CreatePort(bv_strPortCode, _
                  bv_strPortDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_PortModifyPortPort() TABLE NAME:Port"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64PortId As Int32, _
     ByVal bv_strPortCode As String, _
     ByVal bv_strPortDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objPort As New Ports
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objPort.UpdatePort(bv_i64PortId, _
                bv_strPortCode, bv_strPortDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
