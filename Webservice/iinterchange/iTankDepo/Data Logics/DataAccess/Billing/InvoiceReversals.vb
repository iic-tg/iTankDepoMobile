

Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework


Public Class InvoiceReversals

    'Declarations

    Dim objData As DataObjects

    'Invoice Cancel Header Operations

    Private Const INVOICE_CANCELSelectQuery As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL"
    Private Const INVOICE_CANCELSelectQueryByINVC_CNCL_ID As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE INVC_CNCL_ID=@INVC_CNCL_ID"
    Private Const INVOICE_CANCELSelectQueryByINVC_TYP As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE INVC_TYP=@INVC_TYP"
    Private Const INVOICE_CANCELSelectQueryByINVC_NO As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE INVC_NO=@INVC_NO"
    Private Const INVOICE_CANCELSelectQueryByFRM_BLLNG_DT As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE FRM_BLLNG_DT=@FRM_BLLNG_DT"
    Private Const INVOICE_CANCELSelectQueryByTO_BLLNG_DT As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE TO_BLLNG_DT=@TO_BLLNG_DT"
    Private Const INVOICE_CANCELSelectQueryByINVC_TO As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE INVC_TO=@INVC_TO"
    Private Const INVOICE_CANCELSelectQueryByINVC_FNLD_DT As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE INVC_FNLD_DT=@INVC_FNLD_DT"
    Private Const INVOICE_CANCELSelectQueryByGNRTD_BY As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE GNRTD_BY=@GNRTD_BY"
    Private Const INVOICE_CANCELSelectQueryByINVC_CNCLD_DT As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE INVC_CNCLD_DT=@INVC_CNCLD_DT"
    Private Const INVOICE_CANCELSelectQueryByCSTMR_TOT_CRRNCY As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE CSTMR_TOT_CRRNCY=@CSTMR_TOT_CRRNCY"
    Private Const INVOICE_CANCELSelectQueryByDPT_TOT_CRRNCY As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE DPT_TOT_CRRNCY=@DPT_TOT_CRRNCY"
    Private Const INVOICE_CANCELSelectQueryByDPT_ID As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE DPT_ID=@DPT_ID"
    Private Const INVOICE_CANCELSelectQueryByCRTD_BY As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE CRTD_BY=@CRTD_BY"
    Private Const INVOICE_CANCELSelectQueryByCRTD_DT As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE CRTD_DT=@CRTD_DT"
    Private Const INVOICE_CANCELSelectQueryByACTV_BT As String = "SELECT INVC_CNCL_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT FROM INVOICE_CANCEL WHERE ACTV_BT=@ACTV_BT"
    Private Const INVOICE_CANCELInsertQuery As String = "INSERT INTO INVOICE_CANCEL(INVC_CNCL_ID,INVC_TYP_ID,INVC_TYP,INVC_NO,FRM_BLLNG_DT,TO_BLLNG_DT,INVC_TO_ID,INVC_TO,INVC_FNLD_DT,GNRTD_BY,INVC_CNCLD_DT,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,DPT_ID,CRTD_BY,CRTD_DT,ACTV_BT,CSTMR_CRRNCY_ID,DPT_CRRNCY_ID)VALUES(@INVC_CNCL_ID,@INVC_TYP_ID,@INVC_TYP,@INVC_NO,@FRM_BLLNG_DT,@TO_BLLNG_DT,@INVC_TO_ID,@INVC_TO,@INVC_FNLD_DT,@GNRTD_BY,@INVC_CNCLD_DT,@CSTMR_TOT_CRRNCY,@DPT_TOT_CRRNCY,@DPT_ID,@CRTD_BY,@CRTD_DT,@ACTV_BT,@CSTMR_CRRNCY_ID,@DPT_CRRNCY_ID)"
    Private Const INVOICE_CANCELUpdateQuery As String = "UPDATE INVOICE_CANCEL SET INVC_CNCL_ID=@INVC_CNCL_ID, INVC_TYP=@INVC_TYP, INVC_NO=@INVC_NO, FRM_BLLNG_DT=@FRM_BLLNG_DT, TO_BLLNG_DT=@TO_BLLNG_DT, INVC_TO=@INVC_TO, INVC_FNLD_DT=@INVC_FNLD_DT, GNRTD_BY=@GNRTD_BY, INVC_CNCLD_DT=@INVC_CNCLD_DT, CSTMR_TOT_CRRNCY=@CSTMR_TOT_CRRNCY, DPT_TOT_CRRNCY=@DPT_TOT_CRRNCY, DPT_ID=@DPT_ID, ACTV_BT=@ACTV_BT WHERE INVC_CNCL_ID=@INVC_CNCL_ID"
    Private Const INVOICE_CANCELDeleteQuery As String = "DELETE FROM INVOICE_CANCEL WHERE INVC_CNCL_ID=@INVC_CNCL_ID"


    'Invoice Cancel Details
    Private Const INVOICE_CANCEL_DETAILSelectQuery As String = "SELECT INVC_CNCL_DTL_ID,INVC_CNCL_ID,EQPMNT_NO,EQPMNT_TYP,RFRNC_NO,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,ACTV_BT FROM INVOICE_CANCEL_DETAIL"
    Private Const INVOICE_CANCEL_DETAILSelectQueryByINVC_CNCL_DTL_ID As String = "SELECT INVC_CNCL_DTL_ID,INVC_CNCL_ID,EQPMNT_NO,EQPMNT_TYP,RFRNC_NO,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,ACTV_BT FROM INVOICE_CANCEL_DETAIL WHERE INVC_CNCL_DTL_ID=@INVC_CNCL_DTL_ID"
    Private Const INVOICE_CANCEL_DETAILSelectQueryByINVC_CNCL_ID As String = "SELECT INVC_CNCL_DTL_ID,INVC_CNCL_ID,EQPMNT_NO,EQPMNT_TYP,RFRNC_NO,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,ACTV_BT FROM INVOICE_CANCEL_DETAIL WHERE INVC_CNCL_ID=@INVC_CNCL_ID"
    Private Const INVOICE_CANCEL_DETAILSelectQueryByEQPMNT_NO As String = "SELECT INVC_CNCL_DTL_ID,INVC_CNCL_ID,EQPMNT_NO,EQPMNT_TYP,RFRNC_NO,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,ACTV_BT FROM INVOICE_CANCEL_DETAIL WHERE EQPMNT_NO=@EQPMNT_NO"
    Private Const INVOICE_CANCEL_DETAILSelectQueryByEQPMNT_TYP As String = "SELECT INVC_CNCL_DTL_ID,INVC_CNCL_ID,EQPMNT_NO,EQPMNT_TYP,RFRNC_NO,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,ACTV_BT FROM INVOICE_CANCEL_DETAIL WHERE EQPMNT_TYP=@EQPMNT_TYP"
    Private Const INVOICE_CANCEL_DETAILSelectQueryByRFRNC_NO As String = "SELECT INVC_CNCL_DTL_ID,INVC_CNCL_ID,EQPMNT_NO,EQPMNT_TYP,RFRNC_NO,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,ACTV_BT FROM INVOICE_CANCEL_DETAIL WHERE RFRNC_NO=@RFRNC_NO"
    Private Const INVOICE_CANCEL_DETAILSelectQueryByCSTMR_TOT_CRRNCY As String = "SELECT INVC_CNCL_DTL_ID,INVC_CNCL_ID,EQPMNT_NO,EQPMNT_TYP,RFRNC_NO,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,ACTV_BT FROM INVOICE_CANCEL_DETAIL WHERE CSTMR_TOT_CRRNCY=@CSTMR_TOT_CRRNCY"
    Private Const INVOICE_CANCEL_DETAILSelectQueryByDPT_TOT_CRRNCY As String = "SELECT INVC_CNCL_DTL_ID,INVC_CNCL_ID,EQPMNT_NO,EQPMNT_TYP,RFRNC_NO,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,ACTV_BT FROM INVOICE_CANCEL_DETAIL WHERE DPT_TOT_CRRNCY=@DPT_TOT_CRRNCY"
    Private Const INVOICE_CANCEL_DETAILSelectQueryByACTV_BT As String = "SELECT INVC_CNCL_DTL_ID,INVC_CNCL_ID,EQPMNT_NO,EQPMNT_TYP,RFRNC_NO,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,ACTV_BT FROM INVOICE_CANCEL_DETAIL WHERE ACTV_BT=@ACTV_BT"
    Private Const INVOICE_CANCEL_DETAILInsertQuery As String = "INSERT INTO INVOICE_CANCEL_DETAIL(INVC_CNCL_DTL_ID,INVC_CNCL_ID,EQPMNT_NO,EQPMNT_TYP,RFRNC_NO,CSTMR_TOT_CRRNCY,DPT_TOT_CRRNCY,ACTV_BT)VALUES(@INVC_CNCL_DTL_ID,@INVC_CNCL_ID,@EQPMNT_NO,@EQPMNT_TYP,@RFRNC_NO,@CSTMR_TOT_CRRNCY,@DPT_TOT_CRRNCY,@ACTV_BT)"
    Private Const INVOICE_CANCEL_DETAILUpdateQuery As String = "UPDATE INVOICE_CANCEL_DETAIL SET INVC_CNCL_DTL_ID=@INVC_CNCL_DTL_ID, INVC_CNCL_ID=@INVC_CNCL_ID, EQPMNT_NO=@EQPMNT_NO, EQPMNT_TYP=@EQPMNT_TYP, RFRNC_NO=@RFRNC_NO, CSTMR_TOT_CRRNCY=@CSTMR_TOT_CRRNCY, DPT_TOT_CRRNCY=@DPT_TOT_CRRNCY, ACTV_BT=@ACTV_BT WHERE INVC_CNCL_DTL_ID=@INVC_CNCL_DTL_ID"
    Private Const INVOICE_CANCEL_DETAILDeleteQuery As String = "DELETE FROM INVOICE_CANCEL_DETAIL WHERE INVC_CNCL_DTL_ID=@INVC_CNCL_DTL_ID"

    Dim ds As InvoiceReversalDataSet
#Region "Constructor.."

    Sub New()
        ds = New InvoiceReversalDataSet
    End Sub

#End Region

#Region "Invoice Cancel Header Operations"

#Region "GetINVOICE_CANCEL() TABLE NAME:INVOICE_CANCEL"

    Public Function GetINVOICE_CANCEL() As InvoiceReversalDataSet
        Try
            objData = New DataObjects(INVOICE_CANCELSelectQuery)
            objData.Fill(CType(ds, DataSet), InvoiceReversalData._INVOICE_CANCEL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetINVOICE_CANCELByINVC_NO() TABLE NAME:INVOICE_CANCEL"

    Public Function GetINVOICE_CANCELByINVC_NO(ByVal bv_strINVC_NO As String) As InvoiceReversalDataSet
        Try
            objData = New DataObjects(INVOICE_CANCELSelectQueryByINVC_NO, InvoiceReversalData.INVC_NO, bv_strINVC_NO)
            objData.Fill(CType(ds, DataSet), InvoiceReversalData._INVOICE_CANCEL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateINVOICE_CANCEL() TABLE NAME:INVOICE_CANCEL"

    Public Function CreateINVOICE_CANCEL(ByVal bv_strINVC_TYP_ID As Int32, _
                                         ByVal bv_strINVC_TYP As String, _
                                         ByVal bv_strINVC_NO As String, _
                                         ByVal bv_datFRM_BLLNG_DT As DateTime, _
                                         ByVal bv_datTO_BLLNG_DT As DateTime, _
                                         ByVal bv_strINVC_TO_ID As Int32, _
                                         ByVal bv_strINVC_TO As String, _
                                         ByVal bv_datINVC_FNLD_DT As DateTime, _
                                         ByVal bv_strGNRTD_BY As String, _
                                         ByVal bv_datINVC_CNCLD_DT As DateTime, _
                                         ByVal bv_dblCSTMR_TOT_CRRNCY As Double, _
                                         ByVal bv_dblDPT_TOT_CRRNCY As Double, _
                                         ByVal bv_i32DPT_ID As Int32, _
                                         ByVal bv_i32CustomerCurrencyID As Int32, _
                                         ByVal bv_i32DepoCurrencyID As Int32, _
                                         ByVal bv_strCRTD_BY As String, _
                                         ByVal bv_datCRTD_DT As DateTime, _
                                         ByVal bv_blnACTV_BT As Boolean, _
                                         ByRef objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(InvoiceReversalData._INVOICE_CANCEL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(InvoiceReversalData._INVOICE_CANCEL, objTrans)
                .Item(InvoiceReversalData.INVC_CNCL_ID) = intMax
                .Item(InvoiceReversalData.INVC_TYP_ID) = bv_strINVC_TYP_ID
                .Item(InvoiceReversalData.INVC_TYP) = bv_strINVC_TYP
                .Item(InvoiceReversalData.INVC_NO) = bv_strINVC_NO
                .Item(InvoiceReversalData.FRM_BLLNG_DT) = bv_datFRM_BLLNG_DT
                .Item(InvoiceReversalData.TO_BLLNG_DT) = bv_datTO_BLLNG_DT
                .Item(InvoiceReversalData.INVC_TO_ID) = bv_strINVC_TO_ID
                .Item(InvoiceReversalData.INVC_TO) = bv_strINVC_TO
                .Item(InvoiceReversalData.INVC_FNLD_DT) = bv_datINVC_FNLD_DT
                .Item(InvoiceReversalData.GNRTD_BY) = bv_strGNRTD_BY
                .Item(InvoiceReversalData.INVC_CNCLD_DT) = bv_datINVC_CNCLD_DT
                .Item(InvoiceReversalData.CSTMR_TOT_CRRNCY) = bv_dblCSTMR_TOT_CRRNCY
                .Item(InvoiceReversalData.CSTMR_CRRNCY_ID) = bv_i32CustomerCurrencyID
                .Item(InvoiceReversalData.DPT_CRRNCY_ID) = bv_i32DepoCurrencyID
                .Item(InvoiceReversalData.DPT_TOT_CRRNCY) = bv_dblDPT_TOT_CRRNCY
                .Item(InvoiceReversalData.DPT_ID) = bv_i32DPT_ID
                .Item(InvoiceReversalData.CRTD_BY) = bv_strCRTD_BY
                .Item(InvoiceReversalData.CRTD_DT) = bv_datCRTD_DT
                .Item(InvoiceReversalData.ACTV_BT) = bv_blnACTV_BT
            End With
            objData.InsertRow(dr, INVOICE_CANCELInsertQuery, objTrans)
            dr = Nothing
            CreateINVOICE_CANCEL = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateINVOICE_CANCEL() TABLE NAME:INVOICE_CANCEL"

    Public Function UpdateINVOICE_CANCEL(ByVal bv_i64INVC_CNCL_ID As Int64, _
        ByVal bv_strINVC_TYP As String, _
        ByVal bv_strINVC_NO As String, _
        ByVal bv_datFRM_BLLNG_DT As DateTime, _
        ByVal bv_datTO_BLLNG_DT As DateTime, _
        ByVal bv_strINVC_TO As String, _
        ByVal bv_datINVC_FNLD_DT As DateTime, _
        ByVal bv_strGNRTD_BY As String, _
        ByVal bv_datINVC_CNCLD_DT As DateTime, _
        ByVal bv_dblCSTMR_TOT_CRRNCY As Double, _
        ByVal bv_dblDPT_TOT_CRRNCY As Double, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(InvoiceReversalData._INVOICE_CANCEL).NewRow()
            With dr
                .Item(InvoiceReversalData.INVC_CNCL_ID) = bv_i64INVC_CNCL_ID
                .Item(InvoiceReversalData.INVC_TYP) = bv_strINVC_TYP
                .Item(InvoiceReversalData.INVC_NO) = bv_strINVC_NO
                .Item(InvoiceReversalData.FRM_BLLNG_DT) = bv_datFRM_BLLNG_DT
                .Item(InvoiceReversalData.TO_BLLNG_DT) = bv_datTO_BLLNG_DT
                .Item(InvoiceReversalData.INVC_TO) = bv_strINVC_TO
                .Item(InvoiceReversalData.INVC_FNLD_DT) = bv_datINVC_FNLD_DT
                .Item(InvoiceReversalData.GNRTD_BY) = bv_strGNRTD_BY
                .Item(InvoiceReversalData.INVC_CNCLD_DT) = bv_datINVC_CNCLD_DT
                .Item(InvoiceReversalData.CSTMR_TOT_CRRNCY) = bv_dblCSTMR_TOT_CRRNCY
                .Item(InvoiceReversalData.DPT_TOT_CRRNCY) = bv_dblDPT_TOT_CRRNCY
                .Item(InvoiceReversalData.DPT_ID) = bv_i32DPT_ID
                .Item(InvoiceReversalData.ACTV_BT) = bv_blnACTV_BT
            End With
            UpdateINVOICE_CANCEL = objData.UpdateRow(dr, INVOICE_CANCELUpdateQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteINVOICE_CANCEL() TABLE NAME:INVOICE_CANCEL"

    Public Function DeleteINVOICE_CANCEL(ByVal bv_INVC_CNCL_ID As Int64, ByRef objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(InvoiceReversalData._INVOICE_CANCEL).NewRow()
            With dr
                .Item(InvoiceReversalData.INVC_CNCL_ID) = bv_INVC_CNCL_ID
            End With
            DeleteINVOICE_CANCEL = objData.DeleteRow(dr, INVOICE_CANCELDeleteQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



#End Region

#Region "Invoice Cancel Details Operations"


#Region "GetINVOICE_CANCEL_DETAIL() TABLE NAME:INVOICE_CANCEL_DETAIL"

    Public Function GetINVOICE_CANCEL_DETAIL() As InvoiceReversalDataSet
        Try
            objData = New DataObjects(INVOICE_CANCEL_DETAILSelectQuery)
            objData.Fill(CType(ds, DataSet), InvoiceReversalData._INVOICE_CANCEL_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetINVOICE_CANCEL_DETAILByINVC_CNCL_ID() TABLE NAME:INVOICE_CANCEL_DETAIL"

    Public Function GetINVOICE_CANCEL_DETAILByINVC_CNCL_ID(ByVal bv_i64INVC_CNCL_ID As Int64) As InvoiceReversalDataSet
        Try
            objData = New DataObjects(INVOICE_CANCEL_DETAILSelectQueryByINVC_CNCL_ID, InvoiceReversalData.INVC_CNCL_ID, CStr(bv_i64INVC_CNCL_ID))
            objData.Fill(CType(ds, DataSet), InvoiceReversalData._INVOICE_CANCEL_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateINVOICE_CANCEL_DETAIL() TABLE NAME:INVOICE_CANCEL_DETAIL"

    Public Function CreateINVOICE_CANCEL_DETAIL(ByVal bv_i64INVC_CNCL_ID As Int64, _
        ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_strEQPMNT_TYP As String, _
        ByVal bv_strRFRNC_NO As String, _
        ByVal bv_dblCSTMR_TOT_CRRNCY As Double, _
        ByVal bv_dblDPT_TOT_CRRNCY As Double, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByRef objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(InvoiceReversalData._INVOICE_CANCEL_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(InvoiceReversalData._INVOICE_CANCEL_DETAIL, objTrans)
                .Item(InvoiceReversalData.INVC_CNCL_DTL_ID) = intMax
                .Item(InvoiceReversalData.INVC_CNCL_ID) = bv_i64INVC_CNCL_ID
                .Item(InvoiceReversalData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(InvoiceReversalData.EQPMNT_TYP) = bv_strEQPMNT_TYP
                .Item(InvoiceReversalData.RFRNC_NO) = bv_strRFRNC_NO
                .Item(InvoiceReversalData.CSTMR_TOT_CRRNCY) = bv_dblCSTMR_TOT_CRRNCY
                .Item(InvoiceReversalData.DPT_TOT_CRRNCY) = bv_dblDPT_TOT_CRRNCY
                .Item(InvoiceReversalData.ACTV_BT) = bv_blnACTV_BT
            End With
            objData.InsertRow(dr, INVOICE_CANCEL_DETAILInsertQuery, objTrans)
            dr = Nothing
            CreateINVOICE_CANCEL_DETAIL = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateINVOICE_CANCEL_DETAIL() TABLE NAME:INVOICE_CANCEL_DETAIL"

    Public Function UpdateINVOICE_CANCEL_DETAIL(ByVal bv_i64INVC_CNCL_DTL_ID As Int64, _
        ByVal bv_i64INVC_CNCL_ID As Int64, _
        ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_strEQPMNT_TYP As String, _
        ByVal bv_strRFRNC_NO As String, _
        ByVal bv_dblCSTMR_TOT_CRRNCY As Double, _
        ByVal bv_dblDPT_TOT_CRRNCY As Double, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(InvoiceReversalData._INVOICE_CANCEL_DETAIL).NewRow()
            With dr
                .Item(InvoiceReversalData.INVC_CNCL_DTL_ID) = bv_i64INVC_CNCL_DTL_ID
                .Item(InvoiceReversalData.INVC_CNCL_ID) = bv_i64INVC_CNCL_ID
                .Item(InvoiceReversalData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(InvoiceReversalData.EQPMNT_TYP) = bv_strEQPMNT_TYP
                .Item(InvoiceReversalData.RFRNC_NO) = bv_strRFRNC_NO
                .Item(InvoiceReversalData.CSTMR_TOT_CRRNCY) = bv_dblCSTMR_TOT_CRRNCY
                .Item(InvoiceReversalData.DPT_TOT_CRRNCY) = bv_dblDPT_TOT_CRRNCY
                .Item(InvoiceReversalData.ACTV_BT) = bv_blnACTV_BT
            End With
            UpdateINVOICE_CANCEL_DETAIL = objData.UpdateRow(dr, INVOICE_CANCEL_DETAILUpdateQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteINVOICE_CANCEL_DETAIL() TABLE NAME:INVOICE_CANCEL_DETAIL"

    Public Function DeleteINVOICE_CANCEL_DETAIL(ByVal bv_INVC_CNCL_DTL_ID As Int64, ByRef objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(InvoiceReversalData._INVOICE_CANCEL_DETAIL).NewRow()
            With dr
                .Item(InvoiceReversalData.INVC_CNCL_DTL_ID) = bv_INVC_CNCL_DTL_ID
            End With
            DeleteINVOICE_CANCEL_DETAIL = objData.DeleteRow(dr, INVOICE_CANCEL_DETAILDeleteQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#End Region

End Class
