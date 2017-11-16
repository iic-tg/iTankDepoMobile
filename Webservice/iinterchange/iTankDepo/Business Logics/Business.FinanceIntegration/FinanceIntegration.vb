
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework


<ServiceContract()> _
Public Class FinanceIntegration


#Region "pub_GetFINANCE_INTEGRATION() TABLE NAME:FINANCE_INTEGRATION"

    <OperationContract()> _
    Public Function pub_GetFINANCE_INTEGRATION(ByVal bv_FNC_INTGRTN_ID As Int32) As FinanceIntegrationDataSet

        Try
            Dim dsFINANCEINTEGRATIONData As FinanceIntegrationDataSet
            Dim objFINANCEINTEGRATIONs As New FinanceIntegrations
            dsFINANCEINTEGRATIONData = objFINANCEINTEGRATIONs.GetFINANCE_INTEGRATIONByFNC_INTGRTN_ID(bv_FNC_INTGRTN_ID)
            Return dsFINANCEINTEGRATIONData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_GetFINANCE_INTEGRATION() As FinanceIntegrationDataSet

        Try
            Dim dsFINANCEINTEGRATIONData As FinanceIntegrationDataSet
            Dim objFINANCEINTEGRATIONs As New FinanceIntegrations
            dsFINANCEINTEGRATIONData = objFINANCEINTEGRATIONs.GetFINANCE_INTEGRATION()
            Return dsFINANCEINTEGRATIONData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

    <OperationContract()> _
    Public Function pub_UpdateFINANCE_INTEGRATION(ByVal ds As FinanceIntegrationDataSet, ByVal bv_Dpt_ID As Int32, ByVal bv_CreatedBy As String, ByVal bv_CreatedDate As DateTime) As Boolean

        Dim objTrans As New Transactions
        Try
            Dim objFINANCE_INTEGRATION As New FinanceIntegrations

            For Each dr As DataRow In ds.Tables(FinanceIntegrationData._FINANCE_INTEGRATION).Rows

                Select Case dr.RowState

                    Case DataRowState.Added

                        objFINANCE_INTEGRATION.CreateFINANCE_INTEGRATION(CInt(dr.Item(FinanceIntegrationData.INVC_TYP_ID)), _
                                                                         CStr(dr.Item(FinanceIntegrationData.INVC_TYP_CD)), _
                                                                         CInt(dr.Item(FinanceIntegrationData.EQPMNT_TYP_ID)), _
                                                                        CStr(dr.Item(FinanceIntegrationData.EQPMNT_TYP_CD)), _
                                                                        dr.Item(FinanceIntegrationData.CTGRY_CD).ToString(), _
                                                                        CStr(dr.Item(FinanceIntegrationData.ITEM_CD)), _
                                                                        CStr(dr.Item(FinanceIntegrationData.ITEM_DSCRPTN_VC)), _
                                                                        bv_Dpt_ID, bv_CreatedBy, bv_CreatedDate, objTrans)

                    Case DataRowState.Modified

                        objFINANCE_INTEGRATION.UpdateFINANCE_INTEGRATION(CInt(dr.Item(FinanceIntegrationData.FNC_INTGRTN_ID)), _
                                                                         CInt(dr.Item(FinanceIntegrationData.INVC_TYP_ID)), _
                                                                         CStr(dr.Item(FinanceIntegrationData.INVC_TYP_CD)), _
                                                                         CInt(dr.Item(FinanceIntegrationData.EQPMNT_TYP_ID)), _
                                                                         CStr(dr.Item(FinanceIntegrationData.EQPMNT_TYP_CD)), _
                                                                         dr.Item(FinanceIntegrationData.CTGRY_CD).ToString(), _
                                                                         CStr(dr.Item(FinanceIntegrationData.ITEM_CD)), _
                                                                         CStr(dr.Item(FinanceIntegrationData.ITEM_DSCRPTN_VC)), bv_CreatedBy, bv_CreatedDate, objTrans)

                    Case DataRowState.Deleted
                        objFINANCE_INTEGRATION.DeleteFINANCE_INTEGRATION(CInt(dr.Item(FinanceIntegrationData.FNC_INTGRTN_ID, DataRowVersion.Original)), objTrans)

                End Select

            Next


            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_GetFINANCEINTEGRATIONByINVCTYPCD() TABLE NAME:FINANCE_INTEGRATION"

    <OperationContract()> _
    Public Function pub_GetFINANCEINTEGRATIONByINVCTYPCD(ByVal bv_INVC_TYP_CD As String) As DataTable

        Try
            Dim dtFinanceIntegration As DataTable
            Dim objfillEnq As New FinanceIntegrations
            dtFinanceIntegration = objfillEnq.GetFINANCE_INTEGRATIONByINVC_TYP_CD(bv_INVC_TYP_CD)
            Return dtFinanceIntegration
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetFINANCEINTEGRATIONByEQPMNTTYPCD() TABLE NAME:FINANCE_INTEGRATION"

    <OperationContract()> _
    Public Function pub_GetFINANCEINTEGRATIONByEQPMNTTYPCD(ByVal bv_EQPMNT_TYP_CD As String) As FinanceIntegrationDataSet

        Try
            Dim objFINANCEINTEGRATION As FinanceIntegrationDataSet
            Dim objfillEnq As New FinanceIntegrations
            objFINANCEINTEGRATION = objfillEnq.GetFINANCE_INTEGRATIONByEQPMNT_TYP_CD(bv_EQPMNT_TYP_CD)
            Return objFINANCEINTEGRATION
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "Procedure Methouds"

    Public Function CreateInvoice_Procedure(ByVal hshParam As Hashtable, ByRef br_objtrans As Transactions) As Long

        Try
            Dim objFINANCE_INTEGRATION As New FinanceIntegrations

            Return objFINANCE_INTEGRATION.CreateInvoice_Procedure(hshParam, br_objtrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CreateInvoiceDetails_Procedure(ByVal hshParam As Hashtable, ByRef br_objtrans As Transactions) As Long

        Try
            Dim objFINANCE_INTEGRATION As New FinanceIntegrations

            Return objFINANCE_INTEGRATION.CreateInvoiceDetails_Procedure(hshParam, br_objtrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CreateInvoicePaymentSchedules_Procedure(ByVal hshParam As Hashtable, ByRef br_objtrans As Transactions) As Boolean

        Try
            Dim objFINANCE_INTEGRATION As New FinanceIntegrations

            Return objFINANCE_INTEGRATION.CreateInvoicePaymentSchedules_Procedure(hshParam, br_objtrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region




End Class
