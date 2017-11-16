#Region " Damage.vb"
'*********************************************************************************************************************
'Name :
'      Damage.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Damage.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 5:07:11 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class Damage
    Inherits CodeBase
#Region "pub_DamageGetDamage() TABLE NAME:Damage"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsDamageData As DamageDataSet
            Dim objDamages As New Damages
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsDamageData = objDamages.GetDamage(intDepotID)
            Return dsDamageData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_DamageGetDamageByDamageCode() TABLE NAME:Damage"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strDamageCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsDamageData As DamageDataSet
            Dim objDamages As New Damages
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsDamageData = objDamages.GetDamageByDamageCode(bv_strDamageCode, intDepotID)
            If dsDamageData.Tables(DamageData._DAMAGE).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_DamageCreateDamage() TABLE NAME:Damage"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strDamageCode As String, _
     ByVal bv_strDamageDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objDamage As New Damages
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objDamage.CreateDamage(bv_strDamageCode, _
                  bv_strDamageDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_DamageModifyDamageDamage() TABLE NAME:Damage"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64DamageId As Int32, _
     ByVal bv_strDamageCode As String, _
     ByVal bv_strDamageDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objDamage As New Damages
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objDamage.UpdateDamage(bv_i64DamageId, _
                bv_strDamageCode, bv_strDamageDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
