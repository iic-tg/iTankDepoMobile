
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Business.Common
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
Imports System.Xml
Imports System.IO
Imports System.Configuration


Public Class InvoiceReversal


#Region "Invoice Reversal Header Operations"


#Region "pub_GetINVOICE_CANCEL() TABLE NAME:INVOICE_CANCEL"

    Public Function pub_GetINVOICE_CANCEL(ByVal bv_INVC_CNCL_ID As Int64) As InvoiceReversalDataSet

        Try

            Dim objInvoiceReversals As New InvoiceReversals
            Return objInvoiceReversals.GetINVOICE_CANCEL_DETAILByINVC_CNCL_ID(bv_INVC_CNCL_ID)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetINVOICECANCELByINVCNO() TABLE NAME:INVOICE_CANCEL"

    Public Function pub_GetINVOICECANCELByINVCNO(ByVal bv_INVC_NO As String) As InvoiceReversalDataSet

        Try

            Dim objInvoiceReversals As New InvoiceReversals
            Return objInvoiceReversals.GetINVOICE_CANCELByINVC_NO(bv_INVC_NO)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    '#Region "pub_CreateINVOICE_CANCEL() TABLE NAME:INVOICE_CANCEL"

    '    Public Function pub_CreateINVOICE_CANCEL(ByVal bv_strINVC_TYP As String, _
    '        ByVal bv_strINVC_NO As String, _
    '        ByVal bv_datFRM_BLLNG_DT As DateTime, _
    '        ByVal bv_datTO_BLLNG_DT As DateTime, _
    '        ByVal bv_strINVC_TO As String, _
    '        ByVal bv_datINVC_FNLD_DT As DateTime, _
    '        ByVal bv_strGNRTD_BY As String, _
    '        ByVal bv_datINVC_CNCLD_DT As DateTime, _
    '        ByVal bv_dblCSTMR_TOT_CRRNCY As Double, _
    '        ByVal bv_dblDPT_TOT_CRRNCY As Double, _
    '        ByVal bv_i32DPT_ID As Int32, _
    '        ByVal bv_strCRTD_BY As String, _
    '        ByVal bv_datCRTD_DT As DateTime, _
    '        ByVal bv_blnACTV_BT As Boolean, _
    '        ByVal bv_strWfData As String) As Long

    '        Try
    '            Dim objINVOICE_CANCEL As New INVOICECANCELDETAILs
    '            Dim lngCreated As Long
    '            lngCreated = objINVOICE_CANCEL.CreateINVOICE_CANCEL(bv_strINVC_TYP, _
    '                  bv_strINVC_NO, bv_datFRM_BLLNG_DT, _
    '                  bv_datTO_BLLNG_DT, bv_strINVC_TO, _
    '                  bv_datINVC_FNLD_DT, bv_strGNRTD_BY, _
    '                  bv_datINVC_CNCLD_DT, bv_dblCSTMR_TOT_CRRNCY, _
    '                  bv_dblDPT_TOT_CRRNCY, bv_i32DPT_ID, _
    '                  bv_strCRTD_BY, bv_datCRTD_DT, _
    '                  bv_blnACTV_BT)
    '            Return lngCreated
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '#End Region

    '#Region "pub_ModifyINVOICE_CANCEL() TABLE NAME:INVOICE_CANCEL"

    '    Public Function pub_ModifyINVOICE_CANCEL(ByVal bv_i64INVC_CNCL_ID As Int64, _
    '        ByVal bv_strINVC_TYP As String, _
    '        ByVal bv_strINVC_NO As String, _
    '        ByVal bv_datFRM_BLLNG_DT As DateTime, _
    '        ByVal bv_datTO_BLLNG_DT As DateTime, _
    '        ByVal bv_strINVC_TO As String, _
    '        ByVal bv_datINVC_FNLD_DT As DateTime, _
    '        ByVal bv_strGNRTD_BY As String, _
    '        ByVal bv_datINVC_CNCLD_DT As DateTime, _
    '        ByVal bv_dblCSTMR_TOT_CRRNCY As Double, _
    '        ByVal bv_dblDPT_TOT_CRRNCY As Double, _
    '        ByVal bv_i32DPT_ID As Int32, _
    '        ByVal bv_blnACTV_BT As Boolean, _
    '        ByVal bv_strWfData As String) As Boolean

    '        Try
    '            Dim objINVOICE_CANCEL As New INVOICECANCELDETAILs
    '            Dim blnUpdated As Boolean
    '            blnUpdated = objINVOICE_CANCEL.UpdateINVOICE_CANCEL(bv_i64INVC_CNCL_ID, _
    '                  bv_strINVC_TYP, bv_strINVC_NO, _
    '                  bv_datFRM_BLLNG_DT, bv_datTO_BLLNG_DT, _
    '                  bv_strINVC_TO, bv_datINVC_FNLD_DT, _
    '                  bv_strGNRTD_BY, bv_datINVC_CNCLD_DT, _
    '                  bv_dblCSTMR_TOT_CRRNCY, bv_dblDPT_TOT_CRRNCY, _
    '                  bv_i32DPT_ID, bv_blnACTV_BT)
    '            Return blnUpdated
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '#End Region

    '#Region "pub_DeleteINVOICE_CANCEL() TABLE NAME:INVOICE_CANCEL"

    '    Public Function pub_DeleteINVOICE_CANCEL(ByVal bv_i64INVC_CNCL_ID As Int64) As Boolean
    '        Try
    '            Dim objINVOICE_CANCEL As New INVOICECANCELDETAILs
    '            Dim blnDeleted As Boolean
    '            blnDeleted = objINVOICE_CANCEL.DeleteINVOICE_CANCEL(bv_i64INVC_CNCL_ID)
    '            Return blnDeleted
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function
    '#End Region

#End Region


    '#Region "Invoice Cancel Details"



    '#Region "pub_GetINVOICE_CANCEL_DETAIL() TABLE NAME:INVOICE_CANCEL_DETAIL"

    '    Public Function pub_GetINVOICE_CANCEL_DETAIL(ByVal bv_INVC_CNCL_DTL_ID As Int64) As InvoiceReversalDataSet

    '        Try
    '            Dim objInvoiceReversals As New InvoiceReversals

    '            Return objInvoiceReversals.GetINVOICE_CANCEL_DETAILByINVC_CNCL_DTL_ID(bv_INVC_CNCL_DTL_ID)

    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function
    '#End Region

    '#Region "pub_GetINVOICECANCELDETAILByINVCCNCLID() TABLE NAME:INVOICE_CANCEL_DETAIL"

    '    Public Function pub_GetINVOICECANCELDETAILByINVCCNCLID(ByVal bv_INVC_CNCL_ID As Int64) As INVOICECANCELDETAILData

    '        Try
    '            Dim objINVOICECANCELDETAIL As INVOICECANCELDETAILData
    '            Dim objfillEnq As New INVOICECANCELDETAILs
    '            objINVOICECANCELDETAIL = objfillEnq.GetINVOICE_CANCEL_DETAILByINVC_CNCL_ID(bv_INVC_CNCL_ID)
    '            Return objINVOICECANCELDETAIL
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function
    '#End Region

    '#Region "pub_CreateINVOICE_CANCEL_DETAIL() TABLE NAME:INVOICE_CANCEL_DETAIL"

    '    Public Function pub_CreateINVOICE_CANCEL_DETAIL(ByVal bv_i64INVC_CNCL_ID As Int64, _
    '        ByVal bv_strEQPMNT_NO As String, _
    '        ByVal bv_strEQPMNT_TYP As String, _
    '        ByVal bv_strRFRNC_NO As String, _
    '        ByVal bv_dblCSTMR_TOT_CRRNCY As Double, _
    '        ByVal bv_dblDPT_TOT_CRRNCY As Double, _
    '        ByVal bv_blnACTV_BT As Boolean, _
    '        ByVal bv_strWfData As String) As Long

    '        Try
    '            Dim objINVOICE_CANCEL_DETAIL As New INVOICECANCELDETAILs
    '            Dim lngCreated As Long
    '            lngCreated = objINVOICE_CANCEL_DETAIL.CreateINVOICE_CANCEL_DETAIL(bv_i64INVC_CNCL_ID, _
    '                  bv_strEQPMNT_NO, bv_strEQPMNT_TYP, _
    '                  bv_strRFRNC_NO, bv_dblCSTMR_TOT_CRRNCY, _
    '                  bv_dblDPT_TOT_CRRNCY, bv_blnACTV_BT)
    '            Return lngCreated
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '#End Region

    '#Region "pub_ModifyINVOICE_CANCEL_DETAIL() TABLE NAME:INVOICE_CANCEL_DETAIL"

    '    Public Function pub_ModifyINVOICE_CANCEL_DETAIL(ByVal bv_i64INVC_CNCL_DTL_ID As Int64, _
    '        ByVal bv_i64INVC_CNCL_ID As Int64, _
    '        ByVal bv_strEQPMNT_NO As String, _
    '        ByVal bv_strEQPMNT_TYP As String, _
    '        ByVal bv_strRFRNC_NO As String, _
    '        ByVal bv_dblCSTMR_TOT_CRRNCY As Double, _
    '        ByVal bv_dblDPT_TOT_CRRNCY As Double, _
    '        ByVal bv_blnACTV_BT As Boolean, _
    '        ByVal bv_strWfData As String) As Boolean

    '        Try
    '            Dim objINVOICE_CANCEL_DETAIL As New INVOICECANCELDETAILs
    '            Dim blnUpdated As Boolean
    '            blnUpdated = objINVOICE_CANCEL_DETAIL.UpdateINVOICE_CANCEL_DETAIL(bv_i64INVC_CNCL_DTL_ID, _
    '                  bv_i64INVC_CNCL_ID, bv_strEQPMNT_NO, _
    '                  bv_strEQPMNT_TYP, bv_strRFRNC_NO, _
    '                  bv_dblCSTMR_TOT_CRRNCY, bv_dblDPT_TOT_CRRNCY, _
    '                  bv_blnACTV_BT)
    '            Return blnUpdated
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '#End Region

    '#Region "pub_DeleteINVOICE_CANCEL_DETAIL() TABLE NAME:INVOICE_CANCEL_DETAIL"

    '    Public Function pub_DeleteINVOICE_CANCEL_DETAIL(ByVal bv_i64INVC_CNCL_DTL_ID As Int64) As Boolean
    '        Try
    '            Dim objINVOICE_CANCEL_DETAIL As New INVOICECANCELDETAILs
    '            Dim blnDeleted As Boolean
    '            blnDeleted = objINVOICE_CANCEL_DETAIL.DeleteINVOICE_CANCEL_DETAIL(bv_i64INVC_CNCL_DTL_ID)
    '            Return blnDeleted
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function
    '#End Region


    '#End Region

End Class

