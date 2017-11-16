#Region " Upload.vb"
'*********************************************************************************************************************
'Name :
'      Upload.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Upload.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/20/2013 2:05:28 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class Upload
#Region "pub_GetUPLOADKEYSBySCHMID() TABLE NAME:UPLOAD_KEYS"

    <OperationContract()> _
    Public Function pub_GetUPLOADKEYSBySCHMID(ByVal bv_SCHM_ID As Int32) As UploadDataSet

        Try
            Dim dsUpload As UploadDataSet
            Dim objUpload As New Uploads
            dsUpload = objUpload.GetUPLOAD_KEYSBySCHM_ID(bv_SCHM_ID)
            Return dsUpload
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetUPLOADKEYSBySCHMID() TABLE NAME:UPLOAD_KEYS"

    <OperationContract()> _
    Public Function pub_GetUPLOAD_SCHEMABySCHM_ID(ByVal bv_SCHM_ID As Int32) As UploadDataSet

        Try
            Dim dsUpload As UploadDataSet
            Dim objUpload As New Uploads
            dsUpload = objUpload.GetUPLOAD_SCHEMABySCHM_ID(bv_SCHM_ID)
            Return dsUpload
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetUPLOAD_SCHEMADETAILBySCHM_NAM() TABLE NAME:UPLOAD_SCHEMA_DETAIL"

    <OperationContract()> _
    Public Function pub_GetUPLOAD_SCHEMADETAILBySCHM_NAM(ByVal bv_strSCHM_NAM As String) As UploadDataSet

        Try
            Dim dsUpload As UploadDataSet
            Dim objUpload As New Uploads
            dsUpload = objUpload.GetUPLOAD_SCHEMABySCHM_ID(bv_strSCHM_NAM)
            Return dsUpload
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetUPLOAD_SOURCE_TABLE() TABLE NAME:_UPLOAD_SOURCE_TABLE"

    <OperationContract()> _
    Public Function pub_GetUPLOAD_SOURCE_TABLE(ByVal bv_strSchemaQry As String, ByVal bv_i32Depot_ID As Int32) As UploadDataSet

        Try
            Dim dsUpload As UploadDataSet
            Dim objUpload As New Uploads
            dsUpload = objUpload.GetUPLOAD_SOURCE_TABLE(bv_strSchemaQry, bv_i32Depot_ID)
            Return dsUpload
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_GetIdColumn()"

    <OperationContract()> _
    Public Function pvt_GetIdColumn(ByVal bv_strTableName As String) As String
        Try
            Dim objUpload As New Uploads
            Return objUpload.GetIdColumn(bv_strTableName)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

    'UIG Fix Issue
#Region "pvt_CreateUpload() TABLE NAME:Upload Tables"

    <OperationContract()> _
    Public Function pvt_CreateUpload(ByVal bv_InsertQry As String, _
                                     ByVal bv_dsUpload As UploadDataSet, _
                                     ByVal strTableName As String, _
                                     ByVal strIDColumn As String, _
                                     ByVal bv_strPageType As String) As Long
        Dim objtrans As New Transactions()
        Try
            Dim lngCreated As Long
            Dim objUpload As New Uploads
            If strTableName.ToUpper = "TARIFF" Then
                objUpload.DeleteTable(strTableName, CommonUIs.iInt(bv_dsUpload.Tables(strTableName).Rows(0).Item(UploadData.DPT_ID)), CStr(bv_dsUpload.Tables(strTableName).Rows(0).Item(UploadData.EQPMNT_SZ)), _
                                      CStr(bv_dsUpload.Tables(strTableName).Rows(0).Item(UploadData.EQPMNT_TYP)), CommonUIs.iLng(bv_dsUpload.Tables(strTableName).Rows(0).Item(UploadData.CSTMR_ID)), objtrans)
            ElseIf strTableName.ToUpper = "TARIFF_CODE_DETAIL" Then
                objUpload.DeleteCustomerTariffTable(strTableName, CommonUIs.iInt(bv_dsUpload.Tables(strTableName).Rows(0).Item(UploadData.DPT_ID)), CommonUIs.iLng(bv_dsUpload.Tables(strTableName).Rows(0).Item(UploadData.TRFF_CD_ID)), objtrans)
            End If
            lngCreated = objUpload.CreateUpload(bv_InsertQry, bv_dsUpload, strTableName, strIDColumn, objtrans)
            objtrans.commit()
            Return lngCreated
        Catch ex As Exception
            objtrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pvt_GetCountCheckForeignkeySubItem()"

    <OperationContract()> _
    Public Function pvt_GetCountCheckForeignkeySubItem(ByVal bv_StrRefQuery As String, ByVal bv_ITM_ID As Int32) As Int64
        Try
            Dim objUpload As New Uploads
            Return objUpload.GetCountCheckForeignkeySubItem(bv_StrRefQuery, bv_ITM_ID)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_GetCountCheckForeignkeyEquipCode()"

    <OperationContract()> _
    Public Function pvt_GetCountCheckForeignkeyEquipCode(ByVal bv_StrRefQuery As String, ByVal bv_EquipTyp_ID As String) As Int64
        Try
            Dim objUpload As New Uploads
            Return objUpload.GetCountCheckForeignkeyEquipCode(bv_StrRefQuery, bv_EquipTyp_ID)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pvt_GetCountCheckForeignkey()"

    <OperationContract()> _
    Public Function pvt_GetCountCheckForeignkey(ByVal bv_StrRefQuery As String, ByVal bv_i32Depot_ID As Int32) As Int64
        Try
            Dim objUpload As New Uploads
            Return objUpload.GetCountCheckForeignkey(bv_StrRefQuery, bv_i32Depot_ID)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "Check Previous Activity"
    <OperationContract()> _
    Public Function pub_fnCheckPreviousActivity(ByVal bv_strLsrCode As String, ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Integer) As DataSet
        Try
            Dim dsCpActivity As DataSet
            Dim objUpload As New Uploads
            dsCpActivity = objUpload.pub_fnCheckPreviousActivity(bv_strLsrCode, bv_strEquipmentNo, bv_intDepotID)
            Return dsCpActivity
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Check Duplicate"
    <OperationContract()> _
    Public Function pub_GetDuplicateEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_strAuthNo As String) As Boolean
        Try
            Dim blnValid As Boolean
            Dim objUpload As New Uploads
            blnValid = objUpload.pub_GetDuplicateEquipment(bv_strEquipmentNo, bv_strAuthNo)
            Return blnValid
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Check Previous Activity"
    <OperationContract()> _
    Public Function Pub_GateIn_Check(ByVal bv_strCstmrCD As String, ByVal bv_strDepoID As String) As DataSet
        Try
            Dim dsCpActivity As DataSet
            Dim objUpload As New Uploads
            dsCpActivity = objUpload.Pub_GateIn_Check(bv_strCstmrCD, bv_strDepoID)
            Return dsCpActivity
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region ""
    Public Function pub_GetCustomerByCustmrId(ByVal bv_intCstmrId As String, ByVal bv_strDepotId As String) As DataSet
        Try
            Dim dsCustomer As DataSet
            Dim objUpload As New Uploads
            dsCustomer = objUpload.pub_GetCustomerByCustmrId(bv_intCstmrId, bv_strDepotId)
            Return dsCustomer
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
