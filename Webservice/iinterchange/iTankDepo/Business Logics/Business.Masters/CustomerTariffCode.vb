
#Region " CustomerTariffCode.vb"
'*********************************************************************************************************************
'Name :
'      CustomerTariffCode.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(CustomerTariffCode.vb)
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
Public Class CustomerTariffCode



#Region "pub_CreateTariffCode() TABLE NAME:TARIFF_CODE_HEADER"

    <OperationContract()> _
    Public Function pub_CreateTariffCode(ByVal bv_i64TRFF_CD_TYP As String, _
                                        ByVal bv_i64TRFF_CD_EQP_TYP_ID As Int64, _
                                        ByVal bv_i64GTRFF_CD_CSTMR_ID As Int64, _
                                        ByVal bv_i64GTRFF_CD_AGNT_ID As Int64, _
                                        ByVal bv_blnACTV_BT As Boolean, _
                                        ByVal bv_strCRTD_BY As String, _
                                        ByVal bv_datCRTD_DT As DateTime, _
                                        ByVal bv_strMDFD_BY As String, _
                                        ByVal bv_datMDFD_DT As DateTime, _
                                        ByVal bv_i32DPT_ID As Int32, _
                                        ByVal bv_strPageMode As String, _
                                        ByVal bv_strWfData As String, _
                                        ByRef br_dsCustomerTariffCode As CustomerTariffCodeDataSet) As Long
        Dim objTransaction As New Transactions()
        Try
            Dim objCustomerTariffCode As New CustomerTariffCodes

            Dim lngCreated As Long
            lngCreated = objCustomerTariffCode.CreateCustomerTariffCode(bv_i64TRFF_CD_TYP, _
                                                  bv_i64TRFF_CD_EQP_TYP_ID, _
                                                  bv_i64GTRFF_CD_CSTMR_ID, _
                                                  bv_i64GTRFF_CD_AGNT_ID, _
                                                  bv_blnACTV_BT, _
                                                  bv_strCRTD_BY, _
                                                  bv_datCRTD_DT, _
                                                  bv_strMDFD_BY, _
                                                  bv_datMDFD_DT, _
                                                  bv_i32DPT_ID, _
                                                  objTransaction)

            objCustomerTariffCode.DeleteTariffCodeByTariffId(lngCreated, objTransaction)
            If br_dsCustomerTariffCode.Tables(CustomerTariffCodeData._V_TARIFF_CODE_DETAIL).Rows.Count > 0 Then
                For Each drCustomerTariffcode As DataRow In br_dsCustomerTariffCode.Tables(CustomerTariffCodeData._V_TARIFF_CODE_DETAIL).Rows
                    Dim decMnHr As Decimal = 0D
                    Dim decMtrlCst As Decimal = 0D
                    Dim intCmpntId As Long = 0
                    Dim strRemarks As String = ""
                    Dim blnActvBt As Boolean = False
                    If drCustomerTariffcode.RowState <> DataRowState.Deleted Then
                        If Not drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_MNHR) Is DBNull.Value Then
                            decMnHr = CDec(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_MNHR))

                        End If

                        If Not drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_CST) Is DBNull.Value Then
                            decMtrlCst = CDec(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_CST))
                        End If
                        If Not drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_RMRKS) Is DBNull.Value Then
                            strRemarks = CStr(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_RMRKS))
                        End If
                        If Not drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_CMPNT_ID) Is DBNull.Value Then
                            intCmpntId = CLng(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_CMPNT_ID))
                        Else
                            intCmpntId = Nothing
                        End If
                        If Not drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_ACTV_BT) Is DBNull.Value Then
                            blnActvBt = CBool(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_ACTV_BT))
                        End If
                        objCustomerTariffCode.CreateCustomerTariffCodeDetail(lngCreated, _
                                                             CStr(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_CD)), _
                                                             CStr(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_DSC)), _
                                                             CLng(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_LCTN_ID)), _
                                                             intCmpntId, _
                                                             CLng(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_DMG_ID)), _
                                                             CLng(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_RPR_ID)), _
                                                             CLng(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_ID)), _
                                                             decMnHr, _
                                                             decMtrlCst, _
                                                             strRemarks, _
                                                             blnActvBt, _
                                                             bv_strCRTD_BY, _
                                                             bv_datCRTD_DT, _
                                                             bv_strMDFD_BY, _
                                                             bv_datMDFD_DT, _
                                                             bv_i32DPT_ID, _
                                                             objTransaction)
                    End If
                Next
            End If
            objTransaction.commit()
            Return lngCreated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function

#End Region



#Region "pub_ModifyCustomerTariffCode() TABLE NAME:TARIFF_CODE_DETAIL,TARIFF_CODE_HEADER"

    <OperationContract()> _
    Public Function pub_ModifyCustomerTariffCode(ByVal bv_i64TRFF_ID As Int64, _
                                                ByVal bv_i64TRFF_CD_TYP As String, _
                                                ByVal bv_i64TRFF_CD_EQP_TYP_ID As Int64, _
                                                ByVal bv_i64GTRFF_CD_CSTMR_ID As Int64, _
                                                ByVal bv_i64GTRFF_CD_AGNT_ID As Int64, _
                                                ByVal bv_blnACTV_BT As Boolean, _
                                                ByVal bv_strCRTD_BY As String, _
                                                ByVal bv_datCRTD_DT As DateTime, _
                                                ByVal bv_strMDFD_BY As String, _
                                                ByVal bv_datMDFD_DT As DateTime, _
                                                ByVal bv_i32DPT_ID As Int32, _
                                                ByVal bv_strPageMode As String, _
                                                ByVal bv_strWfData As String, _
                                                ByRef br_dsCustomerTariffCode As CustomerTariffCodeDataSet) As Boolean
        Dim objTransaction As New Transactions()
        Try
            Dim objCustomerTariffCodes As New CustomerTariffCodes
            Dim blnUpdated As Boolean
            Dim dtProductAttachDetail As New DataTable


            blnUpdated = objCustomerTariffCodes.UpdateCustomerTariffCode(bv_i64TRFF_ID, _
                                                                          bv_blnACTV_BT, _
                                                                          bv_i32DPT_ID, _
                                                                          objTransaction)

            objCustomerTariffCodes.DeleteTariffCodeByTariffId(bv_i64TRFF_ID, objTransaction)

            If br_dsCustomerTariffCode.Tables(CustomerTariffCodeData._V_TARIFF_CODE_DETAIL).Rows.Count > 0 Then
                For Each drCustomerTariffcode As DataRow In br_dsCustomerTariffCode.Tables(CustomerTariffCodeData._V_TARIFF_CODE_DETAIL).Rows
                    Dim decMnHr As Decimal = 0D
                    Dim decMtrlCst As Decimal = 0D
                    Dim strRemarks As String = ""
                    Dim intCmpntId As Long = 0
                    Dim blnActvBt As Boolean = False
                    If drCustomerTariffcode.RowState <> DataRowState.Deleted Then
                        If Not drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_MNHR) Is DBNull.Value Then
                            decMnHr = CDec(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_MNHR))
                        End If
                        If Not drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_CST) Is DBNull.Value Then
                            decMtrlCst = CDec(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_CST))
                        End If
                        If Not drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_RMRKS) Is DBNull.Value Then
                            strRemarks = CStr(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_RMRKS))
                        End If
                        If Not drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_ACTV_BT) Is DBNull.Value Then
                            blnActvBt = CBool(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_ACTV_BT))
                        End If
                        If Not drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_CMPNT_ID) Is DBNull.Value Then
                            intCmpntId = CLng(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_CMPNT_ID))
                        Else
                            intCmpntId = Nothing
                        End If
                        objCustomerTariffCodes.CreateCustomerTariffCodeDetail(bv_i64TRFF_ID, _
                                                             CStr(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_CD)), _
                                                             CStr(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_DSC)), _
                                                             CLng(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_LCTN_ID)), _
                                                             intCmpntId, _
                                                             CLng(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_DMG_ID)), _
                                                             CLng(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_RPR_ID)), _
                                                             CLng(drCustomerTariffcode.Item(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_ID)), _
                                                             decMnHr, _
                                                             decMtrlCst, _
                                                             strRemarks, _
                                                             blnActvBt, _
                                                             bv_strCRTD_BY, _
                                                             bv_datCRTD_DT, _
                                                             bv_strMDFD_BY, _
                                                             bv_datMDFD_DT, _
                                                             bv_i32DPT_ID, _
                                                             objTransaction)
                    End If
                Next
            End If
            objTransaction.commit()
            Return blnUpdated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTransaction = Nothing
        End Try
    End Function

#End Region





    '#Region "pub_ValidateCustomerTariffCode() TABLE NAME:Sub_Item"

    '    <OperationContract()> _
    '    Public Function pub_ValidateCustomerTariffCode(ByVal bv_strCustomerTariffCodeCode As String, ByVal bv_WfData As String) As Boolean

    '        Try
    '            Dim objCustomerTariffCodes As New CustomerTariffCodes
    '            Dim intRowCount As Integer
    '            intRowCount = CInt(objCustomerTariffCodes.GetCustomerTariffCodeByCustomerTariffCodeCode(bv_strCustomerTariffCodeCode, CLng(CommonUIs.ParseWFDATA(bv_WfData, CustomerTariffCodeData.DPT_ID))))
    '            If intRowCount > 0 Then
    '                Return False
    '            Else
    '                Return True
    '            End If
    '        Catch ex As Exception
    '            Throw New FaultException(New FaultReason(ex.Message))
    '        End Try
    '    End Function
    '#End Region

    '#Region "pub_UpdateCustomerTariffCode() TABLE NAME:CustomerTariffCode"

    '    <OperationContract()> _
    '    Public Function pub_UpdateCustomerTariffCode(ByRef br_dsCustomerTariffCode As CustomerTariffCodeDataSet, _
    '                                        ByVal bv_strModifiedBy As String, _
    '                                        ByVal bv_datModifiedDate As DateTime, _
    '                                        ByVal bv_strWfData As String) As Boolean
    '        Dim objTrans As New Transactions
    '        Try
    '            Dim objCustomerTariffCode As New CustomerTariffCodes
    '            Dim dtCustomerTariffCode As DataTable
    '            Dim dblMaterialCost As Double
    '            Dim strManHours As String = ""
    '            Dim strRemarks As String = ""
    '            Dim lngSubItem As Long

    '            dtCustomerTariffCode = br_dsCustomerTariffCode.Tables(CustomerTariffCodeData._TARIFF_CODE)

    '            If Not dtCustomerTariffCode Is Nothing Then
    '                For Each drCustomerTariffCode As DataRow In dtCustomerTariffCode.Rows
    '                    If drCustomerTariffCode.RowState = DataRowState.Added Or drCustomerTariffCode.RowState = DataRowState.Modified Then
    '                        If Not (drCustomerTariffCode.Item("MATERIAL_COST") Is DBNull.Value) Then
    '                            dblMaterialCost = CDbl(drCustomerTariffCode.Item("MATERIAL_COST"))
    '                        Else
    '                            dblMaterialCost = vbEmpty
    '                        End If
    '                        If Not (drCustomerTariffCode.Item("MAN_HOURS") Is DBNull.Value) Then
    '                            strManHours = CStr(drCustomerTariffCode.Item("MAN_HOURS"))
    '                        Else
    '                            strManHours = String.Empty
    '                        End If
    '                        If Not (drCustomerTariffCode.Item("REMARKS") Is DBNull.Value) Then
    '                            strRemarks = CStr(drCustomerTariffCode.Item("REMARKS"))
    '                        Else
    '                            strRemarks = String.Empty
    '                        End If
    '                        If Not (drCustomerTariffCode.Item(CustomerTariffCodeData.SB_ITM_ID) Is DBNull.Value) Then
    '                            lngSubItem = CLng(drCustomerTariffCode.Item(CustomerTariffCodeData.SB_ITM_ID))
    '                        Else
    '                            lngSubItem = vbEmpty
    '                        End If
    '                    End If
    '                    Select Case drCustomerTariffCode.RowState
    '                        Case DataRowState.Added
    '                            Dim lngCustomerTariffCodeId As Long
    '                            lngCustomerTariffCodeId = objCustomerTariffCode.CreateCustomerTariffCode(CStr(drCustomerTariffCode.Item("CODE")).ToString(), _
    '                                                            CStr(drCustomerTariffCode.Item("DESCRIPTION")).ToString(), CLng(drCustomerTariffCode.Item(CustomerTariffCodeData.ITM_ID)), _
    '                                                            lngSubItem, CLng(drCustomerTariffCode.Item(CustomerTariffCodeData.DMG_ID)), CLng(drCustomerTariffCode.Item(CustomerTariffCodeData.RPR_ID)), _
    '                                                            strManHours, dblMaterialCost, strRemarks, _
    '                                                            bv_strModifiedBy, bv_datModifiedDate, bv_strModifiedBy, bv_datModifiedDate, _
    '                                                            CBool(drCustomerTariffCode.Item("ACTIVE")), CInt(CommonUIs.ParseWFDATA(bv_strWfData, CustomerTariffCodeData.DPT_ID)), _
    '                                                            objTrans)
    '                        Case DataRowState.Modified
    '                            objCustomerTariffCode.UpdateCustomerTariffCode(CInt(drCustomerTariffCode.Item(CustomerTariffCodeData.TRFF_CD_ID)), _
    '                                                            CStr(drCustomerTariffCode.Item("CODE")).ToString(), CStr(drCustomerTariffCode.Item("DESCRIPTION")).ToString(), _
    '                                                            CLng(drCustomerTariffCode.Item(CustomerTariffCodeData.ITM_ID)), lngSubItem, _
    '                                                            CLng(drCustomerTariffCode.Item(CustomerTariffCodeData.DMG_ID)), CLng(drCustomerTariffCode.Item(CustomerTariffCodeData.RPR_ID)), _
    '                                                            strManHours, dblMaterialCost, strRemarks, _
    '                                                            bv_strModifiedBy, bv_datModifiedDate, bv_strModifiedBy, bv_datModifiedDate, _
    '                                                            CBool(drCustomerTariffCode.Item("ACTIVE")), CInt(CommonUIs.ParseWFDATA(bv_strWfData, CustomerTariffCodeData.DPT_ID)), _
    '                                                            objTrans)
    '                        Case DataRowState.Deleted
    '                            Dim objCommonUIs As New CommonUIs
    '                            Dim dsTariff_Code As CustomerTariffCodeDataSet
    '                            Dim objCustomerTariffCodes As New CustomerTariffCodes
    '                            Dim sbrTariffGroup As New StringBuilder

    '                            dsTariff_Code = objCustomerTariffCodes.GetTariffGroupCodeByDepot(CInt(drCustomerTariffCode.Item(CustomerTariffCodeData.TRFF_CD_ID, DataRowVersion.Original)), False, objTrans)
    '                            If dsTariff_Code.Tables(CustomerTariffCodeData._V_TARIFF_GROUP_DETAIL).Rows.Count > 0 Then
    '                                For Each dr As DataRow In dsTariff_Code.Tables(CustomerTariffCodeData._V_TARIFF_GROUP_DETAIL).Rows
    '                                    If sbrTariffGroup.ToString() <> Nothing Then
    '                                        sbrTariffGroup.Append(", ")
    '                                    End If
    '                                    sbrTariffGroup.Append(String.Concat(" and Tariff Group Code : ", dr.Item(CustomerTariffCodeData.TRFF_GRP_CD)))
    '                                Next
    '                            End If

    '                            objCommonUIs.CreateAuditLog(CStr(drCustomerTariffCode.Item("CODE", DataRowVersion.Original)), _
    '                                                                           String.Concat("TariffID : ", CInt(drCustomerTariffCode.Item(CustomerTariffCodeData.TRFF_CD_ID, DataRowVersion.Original))), _
    '                                                                          "Tariff Code", _
    '                                                                          "Delete", _
    '                                                                          CDate(Now), _
    '                                                                          "", _
    '                                                                          "", _
    '                                                                          String.Concat("Deleted from Tariff Code : ", CStr(drCustomerTariffCode.Item("CODE", DataRowVersion.Original)), sbrTariffGroup.ToString()), _
    '                                                                          bv_strModifiedBy, _
    '                                                                          CInt(CommonUIs.ParseWFDATA(bv_strWfData, CustomerTariffCodeData.DPT_ID)), objTrans)

    '                            objCustomerTariffCode.DeleteTariff_Code(CInt(drCustomerTariffCode.Item(CustomerTariffCodeData.TRFF_CD_ID, DataRowVersion.Original)), objTrans)
    '                            objCustomerTariffCode.DeleteTariff_Group_Detail(CInt(drCustomerTariffCode.Item(CustomerTariffCodeData.TRFF_CD_ID, DataRowVersion.Original)), objTrans)
    '                    End Select
    '                Next
    '            End If
    '            objTrans.commit()
    '            Return True
    '        Catch ex As Exception
    '            objTrans.RollBack()
    '            Throw New FaultException(New FaultReason(ex.Message))
    '        Finally
    '            objTrans = Nothing
    '        End Try
    '    End Function

    '#End Region

#Region "pub_GetCustomerTariffDeatilCodeByDepotID()"

    <OperationContract()> _
    Public Function pub_GetCustomerTariffDeatilCodeByDepotID(ByVal bv_intDepotid As Integer) As CustomerTariffCodeDataSet

        Try
            Dim dsCustomerTariffCodeData As CustomerTariffCodeDataSet
            Dim objCustomerTariffCodes As New CustomerTariffCodes

            dsCustomerTariffCodeData = objCustomerTariffCodes.GetCustomerTariffCodeDetailByDepotID(bv_intDepotid)
            Return dsCustomerTariffCodeData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerTariffCodeDetailByTariffID()"

    <OperationContract()> _
    Public Function pub_GetCustomerTariffCodeDetailByTariffID(ByVal bv_intTariffId As Integer) As CustomerTariffCodeDataSet

        Try
            Dim dsCustomerTariffCodeData As CustomerTariffCodeDataSet
            Dim objCustomerTariffCodes As New CustomerTariffCodes

            dsCustomerTariffCodeData = objCustomerTariffCodes.GetCustomerTariffCodeDetailByTariffID(bv_intTariffId)
            Return dsCustomerTariffCodeData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerTariffCodeHeaderByTariffEqpmntID()"

    <OperationContract()> _
    Public Function pub_GetCustomerTariffCodeHeaderByTariffEqpmntID(ByVal bv_i64TRFF_CD_TYP As String, _
                                      ByVal bv_i64TRFF_CD_EQP_TYP_ID As Long, _
                                      ByVal bv_i64GTRFF_CD_CSTMR_ID As Long, _
                                      ByVal bv_i64GTRFF_CD_AGNT_ID As Long) As CustomerTariffCodeDataSet

        Try
            Dim dsCustomerTariffCodeData As CustomerTariffCodeDataSet
            Dim objCustomerTariffCodes As New CustomerTariffCodes

            dsCustomerTariffCodeData = objCustomerTariffCodes.GetCustomerTariffCodeHeaderByTariffIDEqpmntId(bv_i64TRFF_CD_TYP, bv_i64TRFF_CD_EQP_TYP_ID, bv_i64GTRFF_CD_CSTMR_ID, bv_i64GTRFF_CD_AGNT_ID)
            Return dsCustomerTariffCodeData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region



#Region "pub_GetColumnAliasName"
    '<OperationContract()> _
    'Public Function pub_GetColumnAliasName(ByRef br_dsCustomerTariffCode As CustomerTariffCodeDataSet) As CustomerTariffCodeDataSet
    '    Try
    '        Dim dtCustomerTariffCodeData As New DataTable

    '        dtCustomerTariffCodeData = br_dsCustomerTariffCode.Tables(CustomerTariffCodeData._TARIFF_CODE)
    '        dtCustomerTariffCodeData.Columns(1).Caption = dtCustomerTariffCodeData.Columns(1).Caption
    '        dtCustomerTariffCodeData.Columns(1).ColumnName = "CODE"
    '        dtCustomerTariffCodeData.Columns(2).Caption = dtCustomerTariffCodeData.Columns(2).Caption
    '        dtCustomerTariffCodeData.Columns(2).ColumnName = "DESCRIPTION"
    '        dtCustomerTariffCodeData.Columns(16).Caption = dtCustomerTariffCodeData.Columns(16).Caption
    '        dtCustomerTariffCodeData.Columns(16).ColumnName = "ITEM_CODE"
    '        dtCustomerTariffCodeData.Columns(17).Caption = dtCustomerTariffCodeData.Columns(17).Caption
    '        dtCustomerTariffCodeData.Columns(17).ColumnName = "SUB_ITEM_CODE"
    '        dtCustomerTariffCodeData.Columns(18).Caption = dtCustomerTariffCodeData.Columns(18).Caption
    '        dtCustomerTariffCodeData.Columns(18).ColumnName = "DAMAGE_CODE"
    '        dtCustomerTariffCodeData.Columns(19).Caption = dtCustomerTariffCodeData.Columns(19).Caption
    '        dtCustomerTariffCodeData.Columns(19).ColumnName = "REPAIR_CODE"
    '        dtCustomerTariffCodeData.Columns(7).Caption = dtCustomerTariffCodeData.Columns(7).Caption
    '        dtCustomerTariffCodeData.Columns(7).ColumnName = "MAN_HOURS"
    '        dtCustomerTariffCodeData.Columns(8).Caption = dtCustomerTariffCodeData.Columns(8).Caption
    '        dtCustomerTariffCodeData.Columns(8).ColumnName = "MATERIAL_COST"
    '        dtCustomerTariffCodeData.Columns(9).Caption = dtCustomerTariffCodeData.Columns(9).Caption
    '        dtCustomerTariffCodeData.Columns(9).ColumnName = "REMARKS"
    '        dtCustomerTariffCodeData.Columns(14).Caption = dtCustomerTariffCodeData.Columns(14).Caption
    '        dtCustomerTariffCodeData.Columns(14).ColumnName = "ACTIVE"
    '        Return br_dsCustomerTariffCode
    '    Catch ex As Exception
    '        Throw New FaultException(New FaultReason(ex.Message))
    '    End Try
    'End Function
#End Region

    '#Region "pub_TariffGroupCodeGetByID()"

    '    <OperationContract()> _
    '    Public Function pub_TariffGroupCodeGetCustomerTariffCodeByDepot(ByVal bv_intCustomerTariffCodeID As Integer, ByVal bv_blnActive As Boolean) As CustomerTariffCodeDataSet

    '        Try
    '            Dim dsCustomerTariffCodeData As CustomerTariffCodeDataSet
    '            Dim objCustomerTariffCodes As New CustomerTariffCodes
    '            dsCustomerTariffCodeData = objCustomerTariffCodes.GetTariffGroupCodeByDepot(bv_intCustomerTariffCodeID, bv_blnActive)
    '            Return dsCustomerTariffCodeData
    '        Catch ex As Exception
    '            Throw New FaultException(New FaultReason(ex.Message))
    '        End Try
    '    End Function
    '#End Region

End Class