#Region " CleaningType.vb"
'*********************************************************************************************************************
'Name :
'      CleaningType.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(CleaningType.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 11:15:02 AM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
Imports iInterchange.iTankDepo.GatewayFramework

<ServiceContract()> _
Public Class CleaningType
    Inherits CodeBase

#Region "pub_ValidateCleaningType() TABLE NAME:CLEANING_TYPE"

    <OperationContract()> _
    Public Function pub_ValidateCleaningType(ByVal bv_strCleaningTypeCode As String, ByVal bv_WfData As String) As Boolean

        Try
            Dim objCleaningTypes As New CleaningTypes
            Dim intRowCount As Integer
            intRowCount = CInt(objCleaningTypes.GetCleaningTypeByCleaningTypeCode(bv_strCleaningTypeCode, CLng(CommonUIs.ParseWFDATA(bv_WfData, CleaningTypeData.DPT_ID))))
            If intRowCount > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ValidatePK() TABLE NAME:CLEANING_TYPE"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strCleaningTypeCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsCleaningTypeData As CleaningTypeDataSet
            Dim objCleaningTypes As New CleaningTypes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsCleaningTypeData = objCleaningTypes.GetCleaningTypeByCleaningTypeCode(bv_strCleaningTypeCode, intDepotID)
            If dsCleaningTypeData.Tables(CleaningTypeData._CLEANING_TYPE).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CleaningTypeGetCleaningTypeByDepotID() TABLE NAME:CLEANING_TYPE"

    <OperationContract()> _
    Public Function pub_CleaningTypeGetCleaningTypeByDepotID(ByVal bv_strWFDATA As String) As CleaningTypeDataSet

        Try
            Dim dsCleaningTypeData As CleaningTypeDataSet
            Dim objCleaningTypes As New CleaningTypes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CleaningTypeData.DPT_ID))
            dsCleaningTypeData = objCleaningTypes.GetCleaningTypeByDepotID(intDepotID)
            Return dsCleaningTypeData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateCleaningType() TABLE NAME:CLEANING_TYPE"

    <OperationContract()> _
    Public Function pub_UpdateCleaningType(ByRef br_dsCleaningType As CleaningTypeDataSet, _
                                        ByVal bv_strModifiedBy As String, _
                                        ByVal bv_datModifiedDate As DateTime, _
                                        ByVal bv_strWfData As String) As Boolean
        Dim objTransaction As New Transactions()
        Try
            Dim objCleaningType As New CleaningTypes
            Dim dtCleaningType As DataTable
            dtCleaningType = br_dsCleaningType.Tables(CleaningTypeData._CLEANING_TYPE).GetChanges(DataRowState.Added)

            If Not dtCleaningType Is Nothing Then
                For Each drCleaningType As DataRow In dtCleaningType.Rows
                    Dim lngCleaningTypeId As Long
                    lngCleaningTypeId = objCleaningType.CreateCleaningType(drCleaningType.Item("Code").ToString(), _
                                                                           drCleaningType.Item("Description").ToString(), _
                                                                           CBool(drCleaningType.Item("Default")), _
                                                                           bv_strModifiedBy, _
                                                                           bv_datModifiedDate, _
                                                                           bv_strModifiedBy, _
                                                                           bv_datModifiedDate, _
                                                                           CBool(drCleaningType.Item("Active")), _
                                                                           CInt(CommonUIs.ParseWFDATA(bv_strWfData, CleaningTypeData.DPT_ID)), _
                                                                           objTransaction)
                Next
            End If
            dtCleaningType = br_dsCleaningType.Tables(CleaningTypeData._CLEANING_TYPE).GetChanges(DataRowState.Modified)
            If Not dtCleaningType Is Nothing Then
                For Each drCleaningType As DataRow In dtCleaningType.Rows
                    objCleaningType.UpdateCleaningType(CLng(drCleaningType.Item(CleaningTypeData.CLNNG_TYP_ID)), _
                                                       drCleaningType.Item("Code").ToString(), _
                                                       drCleaningType.Item("Description").ToString(), _
                                                       CBool(drCleaningType.Item("Default")), _
                                                       bv_strModifiedBy, _
                                                       bv_datModifiedDate, _
                                                       CBool(drCleaningType.Item("Active")), _
                                                       CInt(CommonUIs.ParseWFDATA(bv_strWfData, CleaningTypeData.DPT_ID)), _
                                                       objTransaction)
                Next
            End If
            objTransaction.commit()
            Return True
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function

#End Region

#Region "pub_GetColumnAliasName"
    <OperationContract()> _
    Public Function pub_GetColumnAliasName(ByRef br_dsCleaningType As CleaningTypeDataSet) As CleaningTypeDataSet
        Try
            Dim dtCleaningTypeData As New DataTable

            dtCleaningTypeData = br_dsCleaningType.Tables(CleaningTypeData._CLEANING_TYPE)
            dtCleaningTypeData.Columns(1).Caption = dtCleaningTypeData.Columns(1).Caption
            dtCleaningTypeData.Columns(1).ColumnName = "Code"
            dtCleaningTypeData.Columns(2).Caption = dtCleaningTypeData.Columns(2).Caption
            dtCleaningTypeData.Columns(2).ColumnName = "Description"
            dtCleaningTypeData.Columns(7).Caption = dtCleaningTypeData.Columns(7).Caption
            dtCleaningTypeData.Columns(7).ColumnName = "Active"
            dtCleaningTypeData.Columns(9).Caption = dtCleaningTypeData.Columns(9).Caption
            dtCleaningTypeData.Columns(9).ColumnName = "Default"
            Return br_dsCleaningType
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


End Class
