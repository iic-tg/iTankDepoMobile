#Region " Responsibility.vb"
'*********************************************************************************************************************
'Name :
'      Responsibility.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Responsibility.vb)
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
Public Class Responsibility
    Inherits CodeBase
#Region "pub_ResponsibilityGetResponsibility() TABLE NAME:Responsibility"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsResponsibilityData As ResponsibilityDataSet
            Dim objResponsibilitys As New Responsibilitys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsResponsibilityData = objResponsibilitys.GetResponsibility(intDepotID)
            Return dsResponsibilityData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ResponsibilityGetResponsibilityByResponsibilityCode() TABLE NAME:Responsibility"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strResponsibilityCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsResponsibilityData As ResponsibilityDataSet
            Dim objResponsibilitys As New Responsibilitys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsResponsibilityData = objResponsibilitys.GetResponsibilityByResponsibilityCode(bv_strResponsibilityCode, intDepotID)
            If dsResponsibilityData.Tables(ResponsibilityData._RESPONSIBILITY).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ResponsibilityCreateResponsibility() TABLE NAME:Responsibility"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strResponsibilityCode As String, _
     ByVal bv_strResponsibilityDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objResponsibility As New Responsibilitys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objResponsibility.CreateResponsibility(bv_strResponsibilityCode, _
                  bv_strResponsibilityDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_ResponsibilityModifyResponsibilityResponsibility() TABLE NAME:Responsibility"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64ResponsibilityId As Int32, _
     ByVal bv_strResponsibilityCode As String, _
     ByVal bv_strResponsibilityDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objResponsibility As New Responsibilitys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objResponsibility.UpdateResponsibility(bv_i64ResponsibilityId, _
                bv_strResponsibilityCode, bv_strResponsibilityDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
