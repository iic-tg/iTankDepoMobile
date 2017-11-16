#Region " Material.vb"
'*********************************************************************************************************************
'Name :
'      Material.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Material.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 7:11:31 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class Material
    Inherits CodeBase
#Region "pub_MaterialGetMaterial() TABLE NAME:Material"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsMaterialData As MaterialDataSet
            Dim objMaterials As New Materials
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsMaterialData = objMaterials.GetMaterial(intDepotID)
            Return dsMaterialData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_MaterialGetMaterialByMaterialCode() TABLE NAME:Material"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strMaterialCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsMaterialData As MaterialDataSet
            Dim objMaterials As New Materials
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsMaterialData = objMaterials.GetMaterialByMaterialCode(bv_strMaterialCode, intDepotID)
            If dsMaterialData.Tables(MaterialData._MATERIAL).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_MaterialCreateMaterial() TABLE NAME:Material"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strMaterialCode As String, _
     ByVal bv_strMaterialDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objMaterial As New Materials
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objMaterial.CreateMaterial(bv_strMaterialCode, _
                  bv_strMaterialDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_MaterialModifyMaterialMaterial() TABLE NAME:Material"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64MaterialId As Int32, _
     ByVal bv_strMaterialCode As String, _
     ByVal bv_strMaterialDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objMaterial As New Materials
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objMaterial.UpdateMaterial(bv_i64MaterialId, _
                bv_strMaterialCode, bv_strMaterialDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
