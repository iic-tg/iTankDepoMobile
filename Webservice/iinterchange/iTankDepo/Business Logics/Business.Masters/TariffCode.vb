
#Region " TariffCode.vb"
'*********************************************************************************************************************
'Name :
'      TariffCode.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(TariffCode.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      10/10/2013 2:02:54 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.Text

<ServiceContract()> _
Public Class TariffCode

#Region "pub_ValidateTariffCode() TABLE NAME:Sub_Item"

    <OperationContract()> _
    Public Function pub_ValidateTariffCode(ByVal bv_strTariffCodeCode As String, ByVal bv_WfData As String) As Boolean

        Try
            Dim objTariffCodes As New TariffCodes
            Dim intRowCount As Integer
            intRowCount = CInt(objTariffCodes.GetTariffCodeByTariffCodeCode(bv_strTariffCodeCode, CLng(CommonUIs.ParseWFDATA(bv_WfData, TariffCodeData.DPT_ID))))
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

#Region "pub_UpdateTariffCode() TABLE NAME:TariffCode"

    <OperationContract()> _
    Public Function pub_UpdateTariffCode(ByRef br_dsTariffCode As TariffCodeDataSet, _
                                        ByVal bv_strModifiedBy As String, _
                                        ByVal bv_datModifiedDate As DateTime, _
                                        ByVal bv_strWfData As String) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objTariffCode As New TariffCodes
            Dim dtTariffCode As DataTable
            Dim dblMaterialCost As Double
            Dim strManHours As String = ""
            Dim strRemarks As String = ""
            Dim lngSubItem As Long

            dtTariffCode = br_dsTariffCode.Tables(TariffCodeData._TARIFF_CODE)

            If Not dtTariffCode Is Nothing Then
                For Each drTariffCode As DataRow In dtTariffCode.Rows
                    If drTariffCode.RowState = DataRowState.Added Or drTariffCode.RowState = DataRowState.Modified Then
                        If Not (drTariffCode.Item("MATERIAL_COST") Is DBNull.Value) Then
                            dblMaterialCost = CDbl(drTariffCode.Item("MATERIAL_COST"))
                        Else
                            dblMaterialCost = vbEmpty
                        End If
                        If Not (drTariffCode.Item("MAN_HOURS") Is DBNull.Value) Then
                            strManHours = CStr(drTariffCode.Item("MAN_HOURS"))
                        Else
                            strManHours = String.Empty
                        End If
                        If Not (drTariffCode.Item("REMARKS") Is DBNull.Value) Then
                            strRemarks = CStr(drTariffCode.Item("REMARKS"))
                        Else
                            strRemarks = String.Empty
                        End If
                        If Not (drTariffCode.Item(TariffCodeData.SB_ITM_ID) Is DBNull.Value) Then
                            lngSubItem = CLng(drTariffCode.Item(TariffCodeData.SB_ITM_ID))
                        Else
                            lngSubItem = vbEmpty
                        End If
                    End If
                    Select Case drTariffCode.RowState
                        Case DataRowState.Added
                            Dim lngTariffCodeId As Long
                            lngTariffCodeId = objTariffCode.CreateTariffCode(CStr(drTariffCode.Item("CODE")).ToString(), _
                                                            CStr(drTariffCode.Item("DESCRIPTION")).ToString(), CLng(drTariffCode.Item(TariffCodeData.ITM_ID)), _
                                                            lngSubItem, CLng(drTariffCode.Item(TariffCodeData.DMG_ID)), CLng(drTariffCode.Item(TariffCodeData.RPR_ID)), _
                                                            strManHours, dblMaterialCost, strRemarks, _
                                                            bv_strModifiedBy, bv_datModifiedDate, bv_strModifiedBy, bv_datModifiedDate, _
                                                            CBool(drTariffCode.Item("ACTIVE")), CInt(CommonUIs.ParseWFDATA(bv_strWfData, TariffCodeData.DPT_ID)), _
                                                            objTrans)
                        Case DataRowState.Modified
                            objTariffCode.UpdateTariffCode(CInt(drTariffCode.Item(TariffCodeData.TRFF_CD_ID)), _
                                                            CStr(drTariffCode.Item("CODE")).ToString(), CStr(drTariffCode.Item("DESCRIPTION")).ToString(), _
                                                            CLng(drTariffCode.Item(TariffCodeData.ITM_ID)), lngSubItem, _
                                                            CLng(drTariffCode.Item(TariffCodeData.DMG_ID)), CLng(drTariffCode.Item(TariffCodeData.RPR_ID)), _
                                                            strManHours, dblMaterialCost, strRemarks, _
                                                            bv_strModifiedBy, bv_datModifiedDate, bv_strModifiedBy, bv_datModifiedDate, _
                                                            CBool(drTariffCode.Item("ACTIVE")), CInt(CommonUIs.ParseWFDATA(bv_strWfData, TariffCodeData.DPT_ID)), _
                                                            objTrans)
                        Case DataRowState.Deleted
                            Dim objCommonUIs As New CommonUIs
                            Dim dsTariff_Code As TariffCodeDataSet
                            Dim objTariffCodes As New TariffCodes
                            Dim sbrTariffGroup As New StringBuilder

                            dsTariff_Code = objTariffCodes.GetTariffGroupCodeByDepot(CInt(drTariffCode.Item(TariffCodeData.TRFF_CD_ID, DataRowVersion.Original)), False, objTrans)
                            If dsTariff_Code.Tables(TariffCodeData._V_TARIFF_GROUP_DETAIL).Rows.Count > 0 Then
                                For Each dr As DataRow In dsTariff_Code.Tables(TariffCodeData._V_TARIFF_GROUP_DETAIL).Rows
                                    If sbrTariffGroup.ToString() <> Nothing Then
                                        sbrTariffGroup.Append(", ")
                                    End If
                                    sbrTariffGroup.Append(String.Concat(" and Tariff Group Code : ", dr.Item(TariffCodeData.TRFF_GRP_CD)))
                                Next
                            End If

                            objCommonUIs.CreateAuditLog(CStr(drTariffCode.Item("CODE", DataRowVersion.Original)), _
                                                                           String.Concat("TariffID : ", CInt(drTariffCode.Item(TariffCodeData.TRFF_CD_ID, DataRowVersion.Original))), _
                                                                          "Tariff Code", _
                                                                          "Delete", _
                                                                          CDate(Now), _
                                                                          "", _
                                                                          "", _
                                                                          String.Concat("Deleted from Tariff Code : ", CStr(drTariffCode.Item("CODE", DataRowVersion.Original)), sbrTariffGroup.ToString()), _
                                                                          bv_strModifiedBy, _
                                                                          CInt(CommonUIs.ParseWFDATA(bv_strWfData, TariffCodeData.DPT_ID)), objTrans)

                            objTariffCode.DeleteTariff_Code(CInt(drTariffCode.Item(TariffCodeData.TRFF_CD_ID, DataRowVersion.Original)), objTrans)
                            objTariffCode.DeleteTariff_Group_Detail(CInt(drTariffCode.Item(TariffCodeData.TRFF_CD_ID, DataRowVersion.Original)), objTrans)
                    End Select
                Next
            End If
            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function

#End Region

#Region "pub_TariffCodeGetByID()"

    <OperationContract()> _
    Public Function pub_TariffCodeGetTariffCodeByDepotID(ByVal bv_strWFDATA As String) As TariffCodeDataSet

        Try
            Dim dsTariffCodeData As TariffCodeDataSet
            Dim objTariffCodes As New TariffCodes
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, TariffCodeData.DPT_ID))
            dsTariffCodeData = objTariffCodes.GetTariffCodeByDepotID(intDepotID)
            Return dsTariffCodeData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetColumnAliasName"
    <OperationContract()> _
    Public Function pub_GetColumnAliasName(ByRef br_dsTariffCode As TariffCodeDataSet) As TariffCodeDataSet
        Try
            Dim dtTariffCodeData As New DataTable

            dtTariffCodeData = br_dsTariffCode.Tables(TariffCodeData._TARIFF_CODE)
            dtTariffCodeData.Columns(1).Caption = dtTariffCodeData.Columns(1).Caption
            dtTariffCodeData.Columns(1).ColumnName = "CODE"
            dtTariffCodeData.Columns(2).Caption = dtTariffCodeData.Columns(2).Caption
            dtTariffCodeData.Columns(2).ColumnName = "DESCRIPTION"
            dtTariffCodeData.Columns(16).Caption = dtTariffCodeData.Columns(16).Caption
            dtTariffCodeData.Columns(16).ColumnName = "ITEM_CODE"
            dtTariffCodeData.Columns(17).Caption = dtTariffCodeData.Columns(17).Caption
            dtTariffCodeData.Columns(17).ColumnName = "SUB_ITEM_CODE"
            dtTariffCodeData.Columns(18).Caption = dtTariffCodeData.Columns(18).Caption
            dtTariffCodeData.Columns(18).ColumnName = "DAMAGE_CODE"
            dtTariffCodeData.Columns(19).Caption = dtTariffCodeData.Columns(19).Caption
            dtTariffCodeData.Columns(19).ColumnName = "REPAIR_CODE"
            dtTariffCodeData.Columns(7).Caption = dtTariffCodeData.Columns(7).Caption
            dtTariffCodeData.Columns(7).ColumnName = "MAN_HOURS"
            dtTariffCodeData.Columns(8).Caption = dtTariffCodeData.Columns(8).Caption
            dtTariffCodeData.Columns(8).ColumnName = "MATERIAL_COST"
            dtTariffCodeData.Columns(9).Caption = dtTariffCodeData.Columns(9).Caption
            dtTariffCodeData.Columns(9).ColumnName = "REMARKS"
            dtTariffCodeData.Columns(14).Caption = dtTariffCodeData.Columns(14).Caption
            dtTariffCodeData.Columns(14).ColumnName = "ACTIVE"
            Return br_dsTariffCode
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_TariffGroupCodeGetByID()"

    <OperationContract()> _
    Public Function pub_TariffGroupCodeGetTariffCodeByDepot(ByVal bv_intTariffCodeID As Integer, ByVal bv_blnActive As Boolean) As TariffCodeDataSet

        Try
            Dim dsTariffCodeData As TariffCodeDataSet
            Dim objTariffCodes As New TariffCodes
            dsTariffCodeData = objTariffCodes.GetTariffGroupCodeByDepot(bv_intTariffCodeID, bv_blnActive)
            Return dsTariffCodeData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "Trariff Codes for Repair Estimate"


    'Customer Trariff
    <OperationContract()> _
    Public Function GetTariffCodeByCustomerID(ByVal bv_CustomerID As String, ByVal bv_EquipmentTypeID As String, ByVal bv_DepotID As String) As DataTable
        Try

            Dim objTariffCodes As New TariffCodes
            Return objTariffCodes.GetTariffCodeByCustomerID(bv_CustomerID, bv_EquipmentTypeID, bv_DepotID)

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    'Agent Trariff
    <OperationContract()> _
    Public Function GetTariffCodeByAgentID(ByVal bv_AgentID As String, ByVal bv_EquipmentTypeID As String, ByVal bv_DepotID As String) As DataTable
        Try

            Dim objTariffCodes As New TariffCodes
            Return objTariffCodes.GetTariffCodeByAgentID(bv_AgentID, bv_EquipmentTypeID, bv_DepotID)

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    'Standard Trariff
    <OperationContract()> _
    Public Function GetStandardTariffCode(ByVal bv_EquipmentTypeID As String, ByVal bv_DepotID As String) As DataTable
        Try

            Dim objTariffCodes As New TariffCodes
            Return objTariffCodes.GetStandardTariffCode(bv_EquipmentTypeID, bv_DepotID)

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function


#End Region

End Class