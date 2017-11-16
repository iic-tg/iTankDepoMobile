#Region " Invoice.vb"
'*********************************************************************************************************************
'Name :
'      Invoice.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Invoice.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      6/4/2013 10:52:05 AM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class Invoice

#Region "pub_GetVCUSTOMERINVOICEByCSTMRID() TABLE NAME:V_CUSTOMER_INVOICE"

    <OperationContract()> _
    Public Function pub_GetVCUSTOMERINVOICEByCSTMRID(ByVal bv_CSTMR_ID As Int64, ByVal bv_DepotID As String) As InvoiceDataSet

        Try
            Dim dsInvoiceDataSet As InvoiceDataSet
            Dim objInvoices As New Invoices
            dsInvoiceDataSet = objInvoices.GetV_CUSTOMER_INVOICEByCSTMR_ID(bv_CSTMR_ID, CInt(bv_DepotID))
            Return dsInvoiceDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetV_INVOICEByINVC_TYP() TABLE NAME:V_INVOICE"

    <OperationContract()> _
    Public Function pub_GetV_INVOICEByINVC_TYP(ByVal bv_INVC_TYP As String, ByVal bv_DepotID As String) As InvoiceDataSet

        Try
            Dim dsInvoiceDataSet As InvoiceDataSet
            Dim objInvoices As New Invoices
            dsInvoiceDataSet = objInvoices.GetV_INVOICEByINVC_TYP(bv_INVC_TYP, CInt(bv_DepotID))
            Return dsInvoiceDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


#Region "pub_GetV_HANDLING_CHARGEByCSTMR_ID() TABLE NAME:V_HANDLING_CHARGE"

    <OperationContract()> _
    Public Function pub_GetV_HANDLING_CHARGEByCSTMR_ID(ByVal bv_CSTMR_ID As Int64, ByVal bv_DepotID As String, ByVal bv_TO_BLLNG_DT As Date) As InvoiceDataSet

        Try
            Dim dsInvoiceDataSet As InvoiceDataSet
            Dim objInvoices As New Invoices
            dsInvoiceDataSet = objInvoices.GetV_HANDLING_CHARGEByCSTMR_ID(bv_CSTMR_ID, CInt(bv_DepotID), bv_TO_BLLNG_DT)
            Return dsInvoiceDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetV_REPAIR_CHARGEByCSTMR_ID() TABLE NAME:V_REPAIR_CHARGE"

    <OperationContract()> _
    Public Function pub_GetV_REPAIR_CHARGEByCSTMR_ID(ByVal bv_CSTMR_ID As Int64, ByVal bv_DepotID As String, ByVal bv_TO_BLLNG_DT As Date) As InvoiceDataSet

        Try
            Dim dsInvoiceDataSet As InvoiceDataSet
            Dim objInvoices As New Invoices
            dsInvoiceDataSet = objInvoices.GetV_REPAIR_CHARGEByCSTMR_ID(bv_CSTMR_ID, CInt(bv_DepotID), bv_TO_BLLNG_DT)
            Return dsInvoiceDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetV_STORAGE_CHARGEByCSTMR_ID() TABLE NAME:V_STORAGE_CHARGE"

    <OperationContract()> _
    Public Function pub_GetV_STORAGE_CHARGEByCSTMR_ID(ByVal bv_CSTMR_ID As Int64, ByVal bv_DepotID As String, ByVal bv_TO_BLLNG_DT As Date) As InvoiceDataSet

        Try
            Dim dsInvoiceDataSet As InvoiceDataSet
            Dim objInvoices As New Invoices
            dsInvoiceDataSet = objInvoices.GetV_STORAGE_CHARGEByCSTMR_ID(bv_CSTMR_ID, CInt(bv_DepotID), bv_TO_BLLNG_DT)
            Return dsInvoiceDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetV_RPT_PENDINGINVOICEByCSTMR_ID() TABLE NAME:V_RPT_PENDINGINVOICE"

    <OperationContract()> _
    Public Function pub_GetV_RPT_PENDINGINVOICEByCSTMR_ID(ByVal bv_CSTMR_ID As Int64, ByVal bv_DepotID As String, ByVal bv_TO_BLLNG_DT As Date) As InvoiceDataSet

        Try
            Dim dsInvoiceDataSet As InvoiceDataSet
            Dim objInvoices As New Invoices
            dsInvoiceDataSet = objInvoices.GetV_RPT_PENDINGINVOICE(bv_CSTMR_ID, CInt(bv_DepotID), bv_TO_BLLNG_DT)
            Return dsInvoiceDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_ModifyCustomerCharge_CHARGE() TABLE NAME:STORAGE_CHARGE,HANDLING_CHARGE"

    <OperationContract()> _
    Public Function pub_ModifyCustomerCharge_CHARGE(
        ByVal bv_datFRM_BLLNG_DT As DateTime, _
        ByVal bv_datTO_BLLNG_DT As DateTime, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_strbtnType As String, ByVal bv_strINVC_TYP As String, _
        ByVal bv_strWfData As String, ByRef objtrans As Transactions) As Boolean
        Try
            Dim objInvoices As New Invoices
            Dim blnUpdated As Boolean
            Dim strBLLNG_FLG As String
            If bv_strbtnType = "Verified" Then
                strBLLNG_FLG = "B"
            Else
                strBLLNG_FLG = "D"
            End If
            If bv_strINVC_TYP = "HS" Then
                blnUpdated = objInvoices.UpdateHANDLING_CHARGE(bv_datTO_BLLNG_DT, strBLLNG_FLG, CInt(bv_i64CSTMR_ID), bv_i32DPT_ID, objtrans)

                If bv_strbtnType = "Verfied" Then
                    blnUpdated = objInvoices.UpdateSTORAGE_CHARGE_STRG_CNTN_FLG(bv_datTO_BLLNG_DT, strBLLNG_FLG, CInt(bv_i64CSTMR_ID), bv_i32DPT_ID, False, objtrans)
                Else
                    blnUpdated = objInvoices.UpdateSTORAGE_CHARGE_BLLNG_FLG(bv_datTO_BLLNG_DT, strBLLNG_FLG, CInt(bv_i64CSTMR_ID), bv_i32DPT_ID, objtrans)
                End If
            ElseIf bv_strINVC_TYP = "RP" Then
                blnUpdated = objInvoices.UpdateREPAIR_CHARGE(bv_i64CSTMR_ID, bv_datTO_BLLNG_DT, strBLLNG_FLG, bv_i32DPT_ID)
            End If

            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region


#Region "pub_CreateINVOICE() TABLE NAME:INVOICE"

    <OperationContract()> _
    Public Function pub_CreateINVOICE(ByRef bv_i64INVC_BIN As Long, _
        ByRef br_strINVC_NO As String, _
        ByVal bv_datINVC_DT As DateTime, _
        ByVal bv_strINVC_FL_PTH As String, _
        ByVal bv_strINVC_TYP As String, _
        ByVal bv_i64INVC_CRRNCY_ID As Int64, _
        ByVal bv_strEXCHNG_RT_NC As Decimal, _
        ByVal bv_i64CSTMR_CRRNCY_ID As Int64, _
        ByVal bv_i64BLLNG_TYP_ID As Int64, _
        ByVal bv_datFRM_BLLNG_DT As DateTime, _
        ByVal bv_datTO_BLLNG_DT As DateTime, _
        ByVal bv_strTTL_CST_IN_CSTMR_CRRNCY_NC As Decimal, _
        ByVal bv_strTTL_CST_IN_INVC_CRRNCY_NC As Decimal, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_strCRTD_BY As String, _
        ByVal bv_strCRTD_DT As String, _
        ByVal bv_strbtnType As String, _
        ByVal bv_strWfData As String, _
        ByVal blnUpdateChargeFlg As Boolean, _
        ByVal blnInvoiceFlg As Boolean) As InvoiceDataSet
        Dim objtrans As New Transactions()

        Try
            Dim objINVOICE As New Invoices
            Dim lngCreated As Long
            If blnUpdateChargeFlg = True Then
                pub_ModifyCustomerCharge_CHARGE(bv_datFRM_BLLNG_DT, bv_datTO_BLLNG_DT, _
                                            bv_i64CSTMR_ID, bv_i32DPT_ID, bv_strbtnType, bv_strINVC_TYP, bv_strWfData, objtrans)
            End If
            If bv_strbtnType <> "Verified" Then
                Dim strBLLNG_FLG As String = "Draft"
                ' Dim dsInvoice As InvoiceDataSet = objINVOICE.GetINVOICEbyPeriod(bv_datFRM_BLLNG_DT, bv_datTO_BLLNG_DT, bv_i32DPT_ID, _
                '                                                      bv_i64CSTMR_ID, strBLLNG_FLG, objtrans)
                If blnInvoiceFlg = True Then
                    lngCreated = objINVOICE.CreateINVOICE(br_strINVC_NO, bv_datINVC_DT, _
                          bv_strINVC_FL_PTH, bv_strINVC_TYP, _
                          bv_i64INVC_CRRNCY_ID, bv_strEXCHNG_RT_NC, _
                          bv_i64CSTMR_CRRNCY_ID, bv_i64BLLNG_TYP_ID, _
                          bv_datFRM_BLLNG_DT, bv_datTO_BLLNG_DT, _
                          bv_strTTL_CST_IN_CSTMR_CRRNCY_NC, bv_strTTL_CST_IN_INVC_CRRNCY_NC, _
                          strBLLNG_FLG, bv_i64CSTMR_ID, _
                          bv_i32DPT_ID, bv_blnACTV_BT, _
                          bv_strCRTD_BY, bv_strCRTD_DT, objtrans)
                Else
                    Dim blnUpdated As Boolean = objINVOICE.UpdateINVOICE(bv_i64INVC_BIN, br_strINVC_NO, bv_datINVC_DT, _
                    bv_strINVC_FL_PTH, bv_strINVC_TYP, _
                    bv_i64INVC_CRRNCY_ID, bv_strEXCHNG_RT_NC, _
                    bv_i64CSTMR_CRRNCY_ID, bv_i64BLLNG_TYP_ID, _
                    bv_datFRM_BLLNG_DT, bv_datTO_BLLNG_DT, _
                    bv_strTTL_CST_IN_CSTMR_CRRNCY_NC, bv_strTTL_CST_IN_INVC_CRRNCY_NC, _
                    strBLLNG_FLG, bv_i64CSTMR_ID, _
                    bv_i32DPT_ID, bv_blnACTV_BT, _
                    bv_strCRTD_BY, bv_strCRTD_DT, objtrans)
                    lngCreated = bv_i64INVC_BIN
                End If


            ElseIf bv_strbtnType = "Verified" Then
                Dim strBLLNG_FLG As String = "Final"
                Dim blnUpdated As Boolean = objINVOICE.UpdateINVOICE(bv_i64INVC_BIN, br_strINVC_NO, bv_datINVC_DT, _
                      bv_strINVC_FL_PTH, bv_strINVC_TYP, _
                      bv_i64INVC_CRRNCY_ID, bv_strEXCHNG_RT_NC, _
                      bv_i64CSTMR_CRRNCY_ID, bv_i64BLLNG_TYP_ID, _
                      bv_datFRM_BLLNG_DT, bv_datTO_BLLNG_DT, _
                      bv_strTTL_CST_IN_CSTMR_CRRNCY_NC, bv_strTTL_CST_IN_INVC_CRRNCY_NC, _
                      strBLLNG_FLG, bv_i64CSTMR_ID, _
                      bv_i32DPT_ID, bv_blnACTV_BT, _
                      bv_strCRTD_BY, bv_strCRTD_DT, objtrans)

                blnUpdated = objINVOICE.UpdateCUSTOMER(bv_i64CSTMR_ID, bv_i32DPT_ID, _
                                                                   bv_datTO_BLLNG_DT, bv_strINVC_TYP, objtrans)
                lngCreated = bv_i64INVC_BIN

            End If
            Dim dsInvoiceDataSet As InvoiceDataSet
            dsInvoiceDataSet = objINVOICE.GetINVOICEByINVC_BIN(lngCreated, bv_i32DPT_ID, objtrans)

            objtrans.commit()
            Return dsInvoiceDataSet
        Catch ex As Exception
            objtrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_GetINVOICE() TABLE NAME:INVOICE"

    Public Function pub_GetINVOICEbyPeriod(ByVal bv_FRM_BLLNG_DT As Date, ByVal bv_TO_BLLNG_DT As Date, _
                                   ByVal bv_DPT_ID As Int32, ByVal bv_CSTMR_ID As Long, _
                                   ByVal bv_BLLNG_FLG As String, ByVal bv_strINVC_TYP As String) As InvoiceDataSet

        Try
            Dim dsInvoiceData As InvoiceDataSet
            Dim objInvoices As New Invoices
            dsInvoiceData = objInvoices.GetINVOICEbyPeriod(bv_FRM_BLLNG_DT, bv_TO_BLLNG_DT, bv_DPT_ID, _
                                                                         bv_CSTMR_ID, bv_BLLNG_FLG, bv_strINVC_TYP)
            Return dsInvoiceData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class
