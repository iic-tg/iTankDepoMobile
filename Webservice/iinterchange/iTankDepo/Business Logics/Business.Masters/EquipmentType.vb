#Region " EQUIPMENTTYPE.vb"
'*********************************************************************************************************************
'Name :
'      EQUIPMENTTYPE.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EQUIPMENTTYPE.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/8/2013 7:07:21 PM
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
Public Class EquipmentType
    Inherits CodeBase
#Region "pub_EquipmentTypeGetEquipmentType() TABLE NAME:EquipmentType"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsEquipmentTypeData As EquipmentTypeDataSet
            Dim objEquipmentTypes As New EquipmentTypes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsEquipmentTypeData = objEquipmentTypes.GetEquipmentType(intDepotID)
            Return dsEquipmentTypeData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_EquipmentTypeGetEquipmentTypeByEquipmentTypeCode() TABLE NAME:EquipmentType"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strEquipmentTypeCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsEquipmentTypeData As EquipmentTypeDataSet
            Dim objEquipmentTypes As New EquipmentTypes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsEquipmentTypeData = objEquipmentTypes.GetEquipmentTypeByEquipmentTypeCode(bv_strEquipmentTypeCode, intDepotID)
            If dsEquipmentTypeData.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "Equipment Type & Code Merge"

    <OperationContract()> _
    Public Function GetEquipmentType(ByVal bv_intDepotID As Integer) As EquipmentTypeDataSet
        Try

            Dim objEquipmentTypes As New EquipmentTypes
            Return objEquipmentTypes.GetEquipmentType(bv_intDepotID)

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    <OperationContract()> _
    Public Function pub_UpdateEquipemtTypeandCode(ByVal ds As EquipmentTypeDataSet, ByVal bv_Dpt_ID As Int32, ByVal bv_CreatedBy As String, ByVal bv_CreatedDate As DateTime) As Boolean

        Dim objTrans As New Transactions
        Try
            Dim objEquipmentTypes As New EquipmentTypes
            Dim blnFlag As Boolean = False

            For Each dr As DataRow In ds.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows

                Select Case dr.RowState

                    Case DataRowState.Added
                        If IsDBNull(dr.Item(EquipmentTypeData.EQPMNT_GRP_CD)) Then
                            dr.Item(EquipmentTypeData.EQPMNT_GRP_CD) = String.Empty
                        End If
                        objEquipmentTypes.CreateEquipmentType(CStr(dr.Item(EquipmentTypeData.EQPMNT_TYP_CD)), _
                                                              CStr(dr.Item(EquipmentTypeData.EQPMNT_TYP_DSCRPTN_VC)), _
                                                              bv_CreatedBy, _
                                                              bv_CreatedDate, _
                                                              bv_CreatedBy, _
                                                              bv_CreatedDate, _
                                                              CBool(dr.Item(EquipmentTypeData.ACTV_BT)), _
                                                              bv_Dpt_ID, _
                                                              CStr(dr.Item(EquipmentTypeData.EQPMNT_CD_CD)), _
                                                              CStr(dr.Item(EquipmentTypeData.EQPMNT_GRP_CD)), _
                                                              objTrans)
                        blnFlag = True

                    Case DataRowState.Modified
                        If IsDBNull(dr.Item(EquipmentTypeData.EQPMNT_GRP_CD)) Then
                            dr.Item(EquipmentTypeData.EQPMNT_GRP_CD) = String.Empty
                        End If
                        objEquipmentTypes.UpdateEquipmentType(CLng(dr.Item(EquipmentTypeData.EQPMNT_TYP_ID)), _
                                                              CStr(dr.Item(EquipmentTypeData.EQPMNT_TYP_CD)), _
                                                              CStr(dr.Item(EquipmentTypeData.EQPMNT_TYP_DSCRPTN_VC)), _
                                                              bv_CreatedBy, _
                                                              bv_CreatedDate, _
                                                              CBool(dr.Item(EquipmentTypeData.ACTV_BT)), _
                                                              bv_Dpt_ID, _
                                                              CStr(dr.Item(EquipmentTypeData.EQPMNT_CD_CD)), _
                                                              CStr(dr.Item(EquipmentTypeData.EQPMNT_GRP_CD)), _
                                                              objTrans)
                        blnFlag = True

                    Case DataRowState.Deleted
                        objEquipmentTypes.DeleteEQUIPMENT_TYPE(CInt(dr.Item(EquipmentTypeData.EQPMNT_TYP_ID, DataRowVersion.Original)), objTrans)
                        blnFlag = True

                End Select

            Next


            objTrans.commit()
            Return blnFlag
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region


#Region "pub_GetEquipmentGroupCD"

    Public Function pub_GetEquipmentGroupCD(ByVal bv_EquipTypeId As String, ByVal bv_strWFData As String) As String
        Try
            Dim dsEquipmentTypeData As EquipmentTypeDataSet
            Dim objEquipmentTypes As New EquipmentTypes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsEquipmentTypeData = objEquipmentTypes.GetEquipmentGroupByEquipmentTypeId(bv_EquipTypeId, intDepotID)
            If dsEquipmentTypeData.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows.Count > 0 Then
                Return CStr(dsEquipmentTypeData.Tables(EquipmentTypeData._EQUIPMENT_TYPE).Rows(0).Item(EquipmentTypeData.EQPMNT_GRP_CD))
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region


#Region "pub_EquipmentTypeCreateEquipmentType() TABLE NAME:EquipmentType"

    '<OperationContract()> _
    'Public Overrides Function pub_CreateCodeMaster(ByVal bv_strEquipmentTypeCode As String, _
    ' ByVal bv_strEquipmentTypeDescription As String, _
    '  ByVal bv_blnActiveBit As Boolean, _
    ' ByVal bv_strCRTD_BY As String, _
    ' ByVal bv_datCRTD_DT As DateTime, _
    ' ByVal bv_strMDFD_BY As String, _
    ' ByVal bv_datMDFD_DT As DateTime, _
    ' ByVal bv_strWfData As String) As Long

    '    Try
    '        Dim objEquipmentType As New EquipmentTypes
    '        Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
    '        Dim lngCreated As Long
    '        lngCreated = objEquipmentType.CreateEquipmentType(bv_strEquipmentTypeCode, _
    '              bv_strEquipmentTypeDescription, bv_strCRTD_BY, _
    '              bv_datCRTD_DT, bv_strMDFD_BY, _
    '              bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
    '        Return lngCreated
    '    Catch ex As Exception
    '        Throw New FaultException(New FaultReason(ex.Message))
    '    End Try
    'End Function

#End Region

#Region "pub_EquipmentTypeModifyEquipmentTypeEquipmentType() TABLE NAME:EquipmentType"

    '<OperationContract()> _
    'Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64EquipmentTypeId As Int32, _
    ' ByVal bv_strEquipmentTypeCode As String, _
    ' ByVal bv_strEquipmentTypeDescription As String, _
    ' ByVal bv_blnActiveBit As Boolean, _
    ' ByVal bv_strMDFD_BY As String, _
    ' ByVal bv_datMDFD_DT As DateTime, _
    ' ByVal bv_strWfData As String) As Boolean

    '    Try
    '        Dim objEquipmentType As New EquipmentTypes
    '        Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
    '        Dim blnUpdated As Boolean
    '        blnUpdated = objEquipmentType.UpdateEquipmentType(bv_i64EquipmentTypeId, _
    '            bv_strEquipmentTypeCode, bv_strEquipmentTypeDescription, _
    '             bv_strMDFD_BY, _
    '              bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
    '        Return blnUpdated
    '    Catch ex As Exception
    '        Throw New FaultException(New FaultReason(ex.Message))
    '    End Try
    'End Function

#End Region

End Class


