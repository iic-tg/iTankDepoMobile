#Region " Status.vb"
'*********************************************************************************************************************
'Name :
'      Status.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Status.vb)
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
Public Class Status
    Inherits CodeBase
#Region "pub_StatusGetStatus() TABLE NAME:Status"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsStatusData As StatusDataSet
            Dim objStatuss As New Statuss
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsStatusData = objStatuss.GetStatus(intDepotID)
            Return dsStatusData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_StatusGetStatusByStatusCode() TABLE NAME:Status"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strStatusCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsStatusData As StatusDataSet
            Dim objStatuss As New Statuss
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsStatusData = objStatuss.GetStatusByStatusCode(bv_strStatusCode, intDepotID)
            If dsStatusData.Tables(StatusData._Status).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_StatusCreateStatus() TABLE NAME:Status"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strStatusCode As String, _
     ByVal bv_strStatusDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objStatus As New Statuss
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objStatus.CreateStatus(bv_strStatusCode, _
                  bv_strStatusDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_StatusModifyStatusStatus() TABLE NAME:Status"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64StatusId As Int32, _
     ByVal bv_strStatusCode As String, _
     ByVal bv_strStatusDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objStatus As New Statuss
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objStatus.UpdateStatus(bv_i64StatusId, _
                bv_strStatusCode, bv_strStatusDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
