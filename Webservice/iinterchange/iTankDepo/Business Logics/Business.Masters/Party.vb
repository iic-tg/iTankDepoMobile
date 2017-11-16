#Region " Party.vb"
'*********************************************************************************************************************
'Name :
'      Party.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Party.vb)
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
Public Class Party
    Inherits CodeBase
#Region "pub_PartyGetParty() TABLE NAME:Party"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsPartyData As PartyDataSet
            Dim objPartys As New Partys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsPartyData = objPartys.GetParty(intDepotID)
            Return dsPartyData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_PartyGetPartyByPartyCode() TABLE NAME:Party"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strPartyCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsPartyData As PartyDataSet
            Dim objPartys As New Partys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsPartyData = objPartys.GetPartyByPartyCode(bv_strPartyCode, intDepotID)
            If dsPartyData.Tables(PartyData._Party).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_PartyCreateParty() TABLE NAME:Party"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strPartyCode As String, _
     ByVal bv_strPartyDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objParty As New Partys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objParty.CreateParty(bv_strPartyCode, _
                  bv_strPartyDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_PartyModifyPartyParty() TABLE NAME:Party"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64PartyId As Int32, _
     ByVal bv_strPartyCode As String, _
     ByVal bv_strPartyDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objParty As New Partys
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objParty.UpdateParty(bv_i64PartyId, _
                bv_strPartyCode, bv_strPartyDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
