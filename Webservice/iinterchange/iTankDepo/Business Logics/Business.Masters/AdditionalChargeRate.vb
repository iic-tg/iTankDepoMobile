Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class AdditionalChargeRate

#Region "pub_GetAdditionlChargesByDepotId() TABLE NAME:V_ADDITIONAL_CHARGE_RATE"

    <OperationContract()> _
    Public Function pub_GetAdditionlChargesByDepotId(ByVal bv_intDepotId As Integer) As AdditionalChargeRateDataSet

        Try
            Dim dsVAdditionalChargeRate As AdditionalChargeRateDataSet
            Dim objVAdditionalChargeRates As New AdditionalChargeRates
            dsVAdditionalChargeRate = objVAdditionalChargeRates.pub_GetAdditionlChargesByDepotId(bv_intDepotId)
            Return dsVAdditionalChargeRate
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_UpdateCharge()"
    <OperationContract()> _
    Public Function pub_UpdateCharge(ByRef br_dsChargeDetail As AdditionalChargeRateDataSet, _
                                        ByVal bv_strModifiedBy As String, _
                                        ByVal bv_datModifiedDate As DateTime, _
                                        ByVal bv_strWfData As String, _
                                        ByVal intDepotID As Integer) As Boolean
        Dim objTrans As New Transactions

        Try
            Dim objCharges As New AdditionalChargeRates
            Dim dtCharge As DataTable
            Dim dblRate As Double
            dtCharge = br_dsChargeDetail.Tables(AdditionalChargeRateData._V_ADDITIONAL_CHARGE_RATE)

            If Not dtCharge Is Nothing Then
                For Each drCharge As DataRow In dtCharge.Rows
                    If drCharge.RowState = DataRowState.Added Or drCharge.RowState = DataRowState.Modified Then
                        If Not (drCharge.Item(AdditionalChargeRateData.RT_NC) Is DBNull.Value) Then
                            dblRate = CDbl(drCharge.Item(AdditionalChargeRateData.RT_NC))
                        Else
                            dblRate = vbEmpty
                        End If
                    End If
                    Select Case drCharge.RowState
                        Case DataRowState.Added
                            Dim lngRouteId As Long
                            lngRouteId = objCharges.CreateAddtionalCharge(CLng((drCharge.Item(AdditionalChargeRateData.OPRTN_TYP_ID))), _
                                                                     (drCharge.Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_CD)).ToString, _
                                                                     (drCharge.Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_DSCRPTN_VC)).ToString, _
                                                                     dblRate, _
                                                                     CBool(drCharge.Item(AdditionalChargeRateData.DFLT_BT)), _
                                                                     CBool(drCharge.Item(AdditionalChargeRateData.ACTV_BT)), _
                                                                     bv_strModifiedBy, _
                                                                     bv_datModifiedDate, _
                                                                     bv_strModifiedBy, _
                                                                     bv_datModifiedDate, _
                                                                     intDepotID, _
                                                                     objTrans)

                        Case DataRowState.Modified
                            Dim blnUpdated As Boolean
                            blnUpdated = objCharges.UpdateAdditionalChargeRate(CLng((drCharge.Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_ID))), _
                                                                     CLng((drCharge.Item(AdditionalChargeRateData.OPRTN_TYP_ID))), _
                                                                     (drCharge.Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_CD)).ToString, _
                                                                     (drCharge.Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_DSCRPTN_VC)).ToString, _
                                                                     dblRate, _
                                                                     CBool(drCharge.Item(AdditionalChargeRateData.DFLT_BT)), _
                                                                     CBool(drCharge.Item(AdditionalChargeRateData.ACTV_BT)), _
                                                                     bv_strModifiedBy, _
                                                                     bv_datModifiedDate, _
                                                                     intDepotID, _
                                                                     objTrans)
                    End Select
                Next
            End If
            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_ValidateCharge() "

    <OperationContract()> _
    Public Function pub_ValidateCharge(ByVal bv_strChargeCode As String, ByVal bv_WfData As String) As Boolean

        Try
            Dim objAdditionalCharge As New AdditionalChargeRates
            Dim intRowCount As Integer
            intRowCount = CInt(objAdditionalCharge.GetChargeCodeByChargeCode(bv_strChargeCode, CLng(CommonUIs.ParseWFDATA(bv_WfData, AdditionalChargeRateData.DPT_ID))))
            If intRowCount > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class
