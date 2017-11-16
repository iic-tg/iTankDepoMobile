#Region " MiscellaneousInvoice.vb"
'*********************************************************************************************************************
'Name :
'      MiscellaneousInvoice.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(MiscellaneousInvoice.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      16-Nov-2013 11:14:04 AM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

<ServiceContract()> _
Public Class MiscellaneousInvoice

#Region "pub_GetMiscellaneousInvoice() TABLE NAME:Miscellaneous_Invoice"

    <OperationContract()> _
    Public Function pub_GetMiscellaneousInvoice(ByVal bv_intDepotID As Integer) As MiscellaneousInvoiceDataSet

        Try
            Dim dsMiscellaneousInvoiceData As MiscellaneousInvoiceDataSet
            Dim objMiscellaneousInvoices As New MiscellaneousInvoices
            dsMiscellaneousInvoiceData = objMiscellaneousInvoices.GetMiscellaneousInvoice(bv_intDepotID)
            Return dsMiscellaneousInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateEquipmentInformation"
    <OperationContract()> _
    Public Function pub_UpdateMiscellaneous_Invoice(ByRef br_dsMiscInvoice As MiscellaneousInvoiceDataSet, ByVal bv_intDepotID As Integer, _
                                                    ByVal bv_strUserName As String, ByVal bv_dtCurrentDate As DateTime, _
                                                    ByRef br_strActivitySubmit As String, _
                                                    ByVal bv_intActivityId As Integer) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objMiscInvoice As New MiscellaneousInvoices
            Dim lngCreated As Long

            For Each drMiscInvoice As DataRow In br_dsMiscInvoice.Tables(MiscellaneousInvoiceData._V_MISCELLANEOUS_INVOICE).Rows

                Dim strChargeDesc As String = String.Empty

                Select Case drMiscInvoice.RowState
                    Case DataRowState.Added
                        If Not drMiscInvoice.Item(MiscellaneousInvoiceData.CHRG_DSCRPTN) Is DBNull.Value Then
                            strChargeDesc = CStr(drMiscInvoice.Item(MiscellaneousInvoiceData.CHRG_DSCRPTN))
                        End If

                        If Not drMiscInvoice.Item(MiscellaneousInvoiceData.NO_OF_EQPMNT_NO) Is DBNull.Value Then
                            Dim dsEqpData As CommonUIDataSet
                            Dim objConfigs As New CommonUIs

                            If drMiscInvoice.Item(MiscellaneousInvoiceData.EQPMNT_TYP_ID) Is DBNull.Value Then
                                If objConfigs.GetMultiLocationSupportConfig.ToLower = "true" Then
                                    dsEqpData = objConfigs.GetEquipmentType(CStr(drMiscInvoice.Item(MiscellaneousInvoiceData.EQPMNT_TYP_CD)), CInt(objConfigs.GetHeadQuarterID))
                                Else
                                    dsEqpData = objConfigs.GetEquipmentType(CStr(drMiscInvoice.Item(MiscellaneousInvoiceData.EQPMNT_TYP_CD)), bv_intDepotID)
                                End If
                                drMiscInvoice.Item(MiscellaneousInvoiceData.EQPMNT_TYP_ID) = dsEqpData.Tables(CommonUIData._EQUIPMENT_TYPE).Rows(0).Item(CommonUIData.EQPMNT_TYP_ID).ToString
                                dsEqpData.Tables(CommonUIData._EQUIPMENT_TYPE).Rows.Clear()
                            End If

                            lngCreated = objMiscInvoice.CreateMiscellaneousInvoice(drMiscInvoice.Item(MiscellaneousInvoiceData.NO_OF_EQPMNT_NO).ToString, _
                                                                                   CommonUIs.iLng(drMiscInvoice.Item(MiscellaneousInvoiceData.EQPMNT_TYP_ID)), _
                                                                                   CommonUIs.iLng(drMiscInvoice.Item(MiscellaneousInvoiceData.INVCNG_TO_ID)), _
                                                                                   CommonUIs.iDat(drMiscInvoice.Item(MiscellaneousInvoiceData.ACTVTY_DT)), _
                                                                                   strChargeDesc, _
                                                                                   CDec(drMiscInvoice.Item(MiscellaneousInvoiceData.AMNT_NC)), _
                                                                                   "U", _
                                                                                   bv_intDepotID, _
                                                                                   bv_strUserName, _
                                                                                   bv_dtCurrentDate, _
                                                                                   bv_strUserName, _
                                                                                   bv_dtCurrentDate, _
                                                                                   drMiscInvoice.Item(MiscellaneousInvoiceData.MIS_TYP).ToString(), _
                                                                                   drMiscInvoice.Item(MiscellaneousInvoiceData.MIS_CTGRY).ToString(), _
                                                                                   objTrans)

                            drMiscInvoice.Item(MiscellaneousInvoiceData.MSCLLNS_INVC_ID) = lngCreated
                        End If
                    Case DataRowState.Modified
                        If Not drMiscInvoice.Item(MiscellaneousInvoiceData.CHRG_DSCRPTN) Is DBNull.Value Then
                            strChargeDesc = CStr(drMiscInvoice.Item(MiscellaneousInvoiceData.CHRG_DSCRPTN))
                        End If

                        objMiscInvoice.UpdateMiscellaneousInvoice(CommonUIs.iLng(drMiscInvoice.Item(MiscellaneousInvoiceData.MSCLLNS_INVC_ID)), _
                                                                  drMiscInvoice.Item(MiscellaneousInvoiceData.NO_OF_EQPMNT_NO).ToString, _
                                                                  CommonUIs.iLng(drMiscInvoice.Item(MiscellaneousInvoiceData.EQPMNT_TYP_ID)), _
                                                                  CommonUIs.iLng(drMiscInvoice.Item(MiscellaneousInvoiceData.INVCNG_TO_ID)), _
                                                                  CommonUIs.iDat(drMiscInvoice.Item(MiscellaneousInvoiceData.ACTVTY_DT)), _
                                                                  strChargeDesc, _
                                                                  CDec(drMiscInvoice.Item(MiscellaneousInvoiceData.AMNT_NC)), _
                                                                  "U", _
                                                                  bv_intDepotID, _
                                                                  bv_strUserName, _
                                                                  bv_dtCurrentDate, _
                                                                  drMiscInvoice.Item(MiscellaneousInvoiceData.MIS_TYP).ToString(), _
                                                                  drMiscInvoice.Item(MiscellaneousInvoiceData.MIS_CTGRY).ToString(), _
                                                                  objTrans)
                    Case DataRowState.Deleted
                        objMiscInvoice.DeleteMiscellaneousInvoice(CLng(drMiscInvoice.Item(MiscellaneousInvoiceData.MSCLLNS_INVC_ID, DataRowVersion.Original)), _
                                                                            bv_intDepotID, _
                                                                            objTrans)
                End Select
            Next
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

End Class
