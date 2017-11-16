#Region " Invoices.vb"
'*********************************************************************************************************************
'Name :
'      Invoices.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Invoices.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      6/4/2013 10:52:05 AM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
#Region "Invoices"

Public Class Invoices

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const V_CUSTOMER_INVOICESelectQueryByCSTMR_ID As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM,BLLNG_TYP_ID,BLLNG_TYP_CD,DPT_ID,DPT_CD,CSTMR_CRRNCY_ID,CSTMR_CRRNCY_CD,CNVRT_TO_CRRNCY_ID,CNVRT_TO_CRRNCY_CD,EXCHNG_RT,CNTCT_PRSN_NAM,BLLNG_ADDRSS_LN1,BLLNG_ADDRSS_LN2,STRG_TX_RT_NC,CSTMR_VT_NO,DPT_VT_NO,LBR_RT_PR_HR_NC,LST_HS_INVC_GNRTD_DT,LST_RP_INVC_GNRTD_DT FROM V_CUSTOMER_INVOICE WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private Const V_INVOICESelectQueryByINVC_TYP As String = "SELECT INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_CRRNCY_ID,INVC_CRRNCY_CD,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,CSTMR_CRRNCY_CD,BLLNG_TYP_ID,BLLNG_TYP_CD,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,CSTMR_CD,LST_HS_INVC_GNRTD_DT,LST_RP_INVC_GNRTD_DT,DPT_ID,ACTV_BT,CRTD_BY,CRTD_DT FROM V_INVOICE WHERE INVC_TYP=@INVC_TYP AND DPT_ID=@DPT_ID ORDER BY CRTD_DT DESC"
    Private Const V_HANDLING_CHARGESelectQueryByHNDLNG_CHRG_ID As String = "SELECT HNDLNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,CASE WHEN TO_BLLNG_DT IS NULL OR TO_BLLNG_DT > @TO_BLLNG_DT THEN @TO_BLLNG_DT ELSE TO_BLLNG_DT END AS TO_BLLNG_DT,FR_DYS,NO_OF_DYS,HNDLNG_CST_NC,HNDLNG_TX_RT_NC,TTL_CSTS_NC,LSS_ID,LSS_CD,YRD_LCTN,BLLNG_FLG,DPT_ID,CSTMR_ID FROM V_HANDLING_CHARGE WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND TO_BLLNG_DT<=@TO_BLLNG_DT AND BLLNG_FLG<>'B'"
    Private Const V_STORAGE_CHARGESelectQueryBySTRG_CHRG_ID As String = "SELECT STRG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,CASE WHEN TO_BLLNG_DT IS NULL OR TO_BLLNG_DT > @TO_BLLNG_DT THEN @TO_BLLNG_DT ELSE TO_BLLNG_DT END AS TO_BLLNG_DT,FR_DYS,NO_OF_DYS,STRG_CST_NC,STRG_TX_RT_NC,TTL_CSTS_NC,IS_LT_FLG,LSS_ID,LSS_CD,YRD_LCTN,DPT_ID,CSTMR_ID,PTI_RT,HNDLNG_CHRG_NC,WSHNG_CHRG_NC FROM V_STORAGE_CHARGE WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND FRM_BLLNG_DT<=@TO_BLLNG_DT AND ((BLLNG_FLG = 'B' AND STRG_CNTN_FLG<>'S') OR BLLNG_FLG='U' OR BLLNG_FLG='D')"
    Private Const V_REPAIR_CHARGESelectQueryByRPR_CHRG_ID As String = "SELECT RPR_CHRG_ID,EQPMNT_NO,EQPMNT_SZ_ID,EQPMNT_SZ_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,ESTMT_NO,RPR_APPRVL_DT,RPR_CMPLTN_DT,MTRL_CST_NC,LBR_CST_NC,RPR_TX_RT_NC,RPR_TX_AMNT_NC,TTL_CSTS_NC,RPR_APPRVL_AMNT_NC,TTL_SRV_TAX,LSS_ID,LSS_CD,YRD_LCTN,CLN_CST_NC,TTL_EST_INCL_SRV_TAX,INVC_TYPE,DPT_ID,CSTMR_ID FROM V_REPAIR_CHARGE WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND RPR_CMPLTN_DT<=@RPR_CMPLTN_DT AND BLLNG_FLG<>'B'"
    Private Const HANDLING_CHARGEUpdateQuery As String = "UPDATE HANDLING_CHARGE SET BLLNG_FLG=@BLLNG_FLG WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND TO_BLLNG_DT<=@TO_BLLNG_DT AND BLLNG_FLG<>'B' AND ACTV_BT=1"
    Private Const STORAGE_CHARGE_BLLNG_FLGUpdateQuery As String = "UPDATE STORAGE_CHARGE SET BLLNG_FLG=@BLLNG_FLG WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND FRM_BLLNG_DT<=@FRM_BLLNG_DT AND ACTV_BT=1 AND BLLNG_FLG<>'B'"
    Private Const STORAGE_CHARGE_STRG_CNTN_FLGUpdateQuery As String = "UPDATE STORAGE_CHARGE SET BLLNG_FLG=@BLLNG_FLG,STRG_CNTN_FLG=CASE WHEN IS_GT_OT_FLG='O; THEN 'S' ELSE 'C' END, BLLNG_TLL_DT=@BLLNG_TLL_DT,ACTV_BT=@ACTV_BT WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND FRM_BLLNG_DT<=@FRM_BLLNG_DT AND ACTV_BT=1"
    Private Const IdentitySelectQry = "SELECT MX_NO from MAX_SNO WHERE TBL_NM = @TBL_NM"
    Private Const IdentityUpdateQry = "UPDATE MAX_SNO set MX_NO = MX_NO + 1 WHERE TBL_NM = @TBL_NM"
    Private Const CUSTOMEHSUpdateQuery As String = "UPDATE CUSTOMER SET LST_HS_INVC_GNRTD_DT=@LST_HS_INVC_GNRTD_DT WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private Const CUSTOMERRPUpdateQuery As String = "UPDATE CUSTOMER SET LST_RP_INVC_GNRTD_DT=@LST_RP_INVC_GNRTD_DT WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private Const REPAIR_CHARGEUpdateQuery As String = "UPDATE REPAIR_CHARGE SET BLLNG_FLG=@BLLNG_FLG WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND BLLNG_FLG<>'B' AND ACTV_BT=1 AND RPR_CMPLTN_DT<=@RPR_CMPLTN_DT"
    'Private Const INVOICESelectQuery As String = "Select COUNT(INVC_NO) FROM INVOICE WHERE FRM_BLLNG_DT=@FRM_BLLNG_DT AND TO_BLLNG_DT=@TO_BLLNG_DT AND BLLNG_FLG=@BLLNG_FLG AND DPT_ID=@DPT_ID AND CSTMR_ID=@CSTMR_ID"
    Private Const INVOICESelectQuery As String = "SELECT INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_CRRNCY_ID,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,DPT_ID,ACTV_BT,CRTD_BY,CRTD_DT FROM INVOICE WHERE FRM_BLLNG_DT=@FRM_BLLNG_DT AND TO_BLLNG_DT=@TO_BLLNG_DT AND BLLNG_FLG=@BLLNG_FLG AND DPT_ID=@DPT_ID AND CSTMR_ID=@CSTMR_ID AND INVC_TYP=@INVC_TYP"
    Private Const INVOICESelectQueryByINVC_BIN As String = "SELECT INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_CRRNCY_ID,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,DPT_ID,ACTV_BT,CRTD_BY,CRTD_DT FROM INVOICE WHERE INVC_BIN=@INVC_BIN"
    Private Const INVOICEInsertQuery As String = "INSERT INTO INVOICE(INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_CRRNCY_ID,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,DPT_ID,ACTV_BT,CRTD_BY,CRTD_DT)VALUES(@INVC_BIN,@INVC_NO,@INVC_DT,@INVC_FL_PTH,@INVC_TYP,@INVC_CRRNCY_ID,@EXCHNG_RT_NC,@CSTMR_CRRNCY_ID,@BLLNG_TYP_ID,@FRM_BLLNG_DT,@TO_BLLNG_DT,@TTL_CST_IN_CSTMR_CRRNCY_NC,@TTL_CST_IN_INVC_CRRNCY_NC,@BLLNG_FLG,@CSTMR_ID,@DPT_ID,@ACTV_BT,@CRTD_BY,@CRTD_DT)"
    Private Const INVOICEUpdateQuery As String = "UPDATE INVOICE SET INVC_BIN=@INVC_BIN, INVC_NO=@INVC_NO, INVC_DT=@INVC_DT, INVC_FL_PTH=@INVC_FL_PTH, INVC_TYP=@INVC_TYP, INVC_CRRNCY_ID=@INVC_CRRNCY_ID, EXCHNG_RT_NC=@EXCHNG_RT_NC, CSTMR_CRRNCY_ID=@CSTMR_CRRNCY_ID, BLLNG_TYP_ID=@BLLNG_TYP_ID, FRM_BLLNG_DT=@FRM_BLLNG_DT, TO_BLLNG_DT=@TO_BLLNG_DT, TTL_CST_IN_CSTMR_CRRNCY_NC=@TTL_CST_IN_CSTMR_CRRNCY_NC, TTL_CST_IN_INVC_CRRNCY_NC=@TTL_CST_IN_INVC_CRRNCY_NC, BLLNG_FLG=@BLLNG_FLG, CSTMR_ID=@CSTMR_ID, DPT_ID=@DPT_ID, ACTV_BT=@ACTV_BT,CRTD_BY=@CRTD_BY,CRTD_DT=@CRTD_DT WHERE INVC_BIN=@INVC_BIN"
    Private Const V_RPT_PendingInvoiceSelectQuery As String = "SELECT STRG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,FR_DYS,NO_OF_DYS,STRG_TX_RT_NC,TTL_CSTS_NC,IS_GT_OT_FLG,IS_LT_FLG,LSS_ID,BLLNG_FLG,STRG_CNTN_FLG,BLLNG_TYP_CD,LSS_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,YRD_LCTN,MODE_PYMNT,DPT_ID,DPT_CD,PTI_RT,HNDLNG_CHRG_NC,WSHNG_CHRG_NC,STRG_CST_NC,LFT_CHRG_NC,DAYS,STORAGE,LIFTS,PERIODFROM,PERIODTO FROM V_RPT_PENDINGINVOICE  WHERE TO_BLLNG_DT<=@TO_BLLNG_DT AND CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID  "


    Private ds As InvoiceDataSet
# End Region

#Region "Constructor.."

	Sub New()
        ds = New InvoiceDataSet
	End Sub

#End Region


#Region "GetV_CUSTOMER_INVOICEByCSTMR_ID() TABLE NAME:V_CUSTOMER_INVOICE"

    Public Function GetV_CUSTOMER_INVOICEByCSTMR_ID(ByVal bv_i64CSTMR_ID As Int64, ByVal bvi32DPT_ID As Int32) As InvoiceDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(InvoiceData.CSTMR_ID, bv_i64CSTMR_ID)
            hshParameters.Add(InvoiceData.DPT_ID, bvi32DPT_ID)
            objData = New DataObjects(V_CUSTOMER_INVOICESelectQueryByCSTMR_ID, hshParameters)
            objData.Fill(CType(ds, DataSet), InvoiceData._V_CUSTOMER_INVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetV_INVOICEByINVC_TYP() TABLE NAME:V_INVOICE"

    Public Function GetV_INVOICEByINVC_TYP(ByVal bv_strINVC_TYP As String, ByVal bvi32DPT_ID As Int32) As InvoiceDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(InvoiceData.INVC_TYP, bv_strINVC_TYP)
            hshParameters.Add(InvoiceData.DPT_ID, bvi32DPT_ID)
            objData = New DataObjects(V_INVOICESelectQueryByINVC_TYP, hshParameters)
            objData.Fill(CType(ds, DataSet), InvoiceData._V_INVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetV_HANDLING_CHARGEByCSTMR_ID() TABLE NAME:V_HANDLING_CHARGE"

    Public Function GetV_HANDLING_CHARGEByCSTMR_ID(ByVal bv_i64CSTMR_ID As Int64, ByVal bvi32DPT_ID As Int32, ByVal bvdatTO_BLLNG_DT As Date) As InvoiceDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(InvoiceData.CSTMR_ID, bv_i64CSTMR_ID)
            hshParameters.Add(InvoiceData.DPT_ID, bvi32DPT_ID)
            hshParameters.Add(InvoiceData.TO_BLLNG_DT, bvdatTO_BLLNG_DT)
            objData = New DataObjects(V_HANDLING_CHARGESelectQueryByHNDLNG_CHRG_ID, hshParameters)
            objData.Fill(CType(ds, DataSet), InvoiceData._V_HANDLING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetV_REPAIR_CHARGEByCSTMR_ID() TABLE NAME:V_REPAIR_CHARGE"

    Public Function GetV_REPAIR_CHARGEByCSTMR_ID(ByVal bv_i64CSTMR_ID As Int64, ByVal bvi32DPT_ID As Int32, ByVal bvdatTO_BLLNG_DT As Date) As InvoiceDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(InvoiceData.CSTMR_ID, bv_i64CSTMR_ID)
            hshParameters.Add(InvoiceData.DPT_ID, bvi32DPT_ID)
            hshParameters.Add(InvoiceData.RPR_CMPLTN_DT, bvdatTO_BLLNG_DT)
            objData = New DataObjects(V_REPAIR_CHARGESelectQueryByRPR_CHRG_ID, hshParameters)
            objData.Fill(CType(ds, DataSet), InvoiceData._V_REPAIR_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetV_STORAGE_CHARGEByCSTMR_ID() TABLE NAME:V_STORAGE_CHARGE"

    Public Function GetV_STORAGE_CHARGEByCSTMR_ID(ByVal bv_i64CSTMR_ID As Int64, ByVal bvi32DPT_ID As Int32, ByVal bvdatTO_BLLNG_DT As Date) As InvoiceDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(InvoiceData.CSTMR_ID, bv_i64CSTMR_ID)
            hshParameters.Add(InvoiceData.DPT_ID, bvi32DPT_ID)
            hshParameters.Add(InvoiceData.TO_BLLNG_DT, bvdatTO_BLLNG_DT)
            objData = New DataObjects(V_STORAGE_CHARGESelectQueryBySTRG_CHRG_ID, hshParameters)
            objData.Fill(CType(ds, DataSet), InvoiceData._V_STORAGE_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetV_RPT_PENDINGINVOICE() TABLE NAME:V_RPT_PENDINGINVOICE"

    Public Function GetV_RPT_PENDINGINVOICE(ByVal bv_i64CSTMR_ID As Int64, ByVal bvi32DPT_ID As Int32, ByVal bvdatTO_BLLNG_DT As Date) As InvoiceDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(InvoiceData.CSTMR_ID, bv_i64CSTMR_ID)
            hshParameters.Add(InvoiceData.DPT_ID, bvi32DPT_ID)
            hshParameters.Add(InvoiceData.TO_BLLNG_DT, bvdatTO_BLLNG_DT)
            objData = New DataObjects(V_RPT_PendingInvoiceSelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), InvoiceData._V_RPT_PENDINGINVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateHANDLING_CHARGE() TABLE NAME:HANDLING_CHARGE"

    Public Function UpdateHANDLING_CHARGE(ByVal bv_datTO_BLLNG_DT As DateTime, _
        ByVal bv_strBLLNG_FLG As String, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_i64CSTMR_ID As Int64, ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(InvoiceData._HANDLING_CHARGE).NewRow()
            With dr
                .Item(InvoiceData.TO_BLLNG_DT) = bv_datTO_BLLNG_DT
                .Item(InvoiceData.BLLNG_FLG) = bv_strBLLNG_FLG
                .Item(InvoiceData.DPT_ID) = bv_i32DPT_ID
                .Item(InvoiceData.CSTMR_ID) = bv_i64CSTMR_ID
            End With
            UpdateHANDLING_CHARGE = objData.UpdateRow(dr, HANDLING_CHARGEUpdateQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateSTORAGE_CHARGE_BLLNG_FLG() TABLE NAME:STORAGE_CHARGE"

    Public Function UpdateSTORAGE_CHARGE_BLLNG_FLG(ByVal bv_datTO_BLLNG_DT As DateTime, _
        ByVal bv_strBLLNG_FLG As String, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_i32DPT_ID As Int32, ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(InvoiceData._STORAGE_CHARGE).NewRow()
            With dr
                .Item(InvoiceData.FRM_BLLNG_DT) = bv_datTO_BLLNG_DT
                .Item(InvoiceData.BLLNG_FLG) = bv_strBLLNG_FLG
                .Item(InvoiceData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(InvoiceData.DPT_ID) = bv_i32DPT_ID
            End With
            UpdateSTORAGE_CHARGE_BLLNG_FLG = objData.UpdateRow(dr, STORAGE_CHARGE_BLLNG_FLGUpdateQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateSTORAGE_CHARGE_STRG_CNTN_FLG() TABLE NAME:STORAGE_CHARGE"

    Public Function UpdateSTORAGE_CHARGE_STRG_CNTN_FLG(ByVal bv_datTO_BLLNG_DT As DateTime, _
        ByVal bv_strBLLNG_FLG As String, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_blnActv_bt As Boolean, ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(InvoiceData._STORAGE_CHARGE).NewRow()
            With dr
                .Item(InvoiceData.FRM_BLLNG_DT) = bv_datTO_BLLNG_DT
                .Item(InvoiceData.BLLNG_FLG) = bv_strBLLNG_FLG
                If bv_datTO_BLLNG_DT <> Nothing Then
                    .Item(InvoiceData.BLLNG_TLL_DT) = bv_datTO_BLLNG_DT
                Else
                    .Item(InvoiceData.BLLNG_TLL_DT) = DBNull.Value
                End If
                .Item(InvoiceData.ACTV_BT) = bv_blnActv_bt

                .Item(InvoiceData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(InvoiceData.DPT_ID) = bv_i32DPT_ID
            End With
            UpdateSTORAGE_CHARGE_STRG_CNTN_FLG = objData.UpdateRow(dr, STORAGE_CHARGE_STRG_CNTN_FLGUpdateQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateINVOICE() TABLE NAME:INVOICE"

    Public Function CreateINVOICE(ByRef br_strINVC_NO As String, _
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
        ByVal bv_strBLLNG_FLG As String, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_strCRTD_BY As String, _
        ByVal bv_strCRTD_DT As String, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(InvoiceData._INVOICE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(InvoiceData._INVOICE)
                .Item(InvoiceData.INVC_BIN) = intMax
                br_strINVC_NO = GetInvoiceIdentityValue(bv_strINVC_TYP, bv_datFRM_BLLNG_DT, br_objTrans)
                .Item(InvoiceData.INVC_NO) = br_strINVC_NO
                .Item(InvoiceData.INVC_DT) = bv_datINVC_DT
                If bv_strINVC_FL_PTH <> Nothing Then
                    .Item(InvoiceData.INVC_FL_PTH) = bv_strINVC_FL_PTH
                Else
                    .Item(InvoiceData.INVC_FL_PTH) = DBNull.Value
                End If
                .Item(InvoiceData.INVC_TYP) = bv_strINVC_TYP
                If bv_i64INVC_CRRNCY_ID <> 0 Then
                    .Item(InvoiceData.INVC_CRRNCY_ID) = bv_i64INVC_CRRNCY_ID
                Else
                    .Item(InvoiceData.INVC_CRRNCY_ID) = DBNull.Value
                End If
                '.Item(InvoiceData.INVC_CRRNCY_ID) = bv_i64INVC_CRRNCY_ID
                .Item(InvoiceData.EXCHNG_RT_NC) = bv_strEXCHNG_RT_NC
                .Item(InvoiceData.CSTMR_CRRNCY_ID) = bv_i64CSTMR_CRRNCY_ID
                .Item(InvoiceData.BLLNG_TYP_ID) = bv_i64BLLNG_TYP_ID
                .Item(InvoiceData.FRM_BLLNG_DT) = bv_datFRM_BLLNG_DT
                .Item(InvoiceData.TO_BLLNG_DT) = bv_datTO_BLLNG_DT
                .Item(InvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC) = bv_strTTL_CST_IN_CSTMR_CRRNCY_NC
                .Item(InvoiceData.TTL_CST_IN_INVC_CRRNCY_NC) = bv_strTTL_CST_IN_INVC_CRRNCY_NC
                .Item(InvoiceData.BLLNG_FLG) = bv_strBLLNG_FLG
                .Item(InvoiceData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(InvoiceData.DPT_ID) = bv_i32DPT_ID
                .Item(InvoiceData.ACTV_BT) = bv_blnACTV_BT
                .Item(InvoiceData.CRTD_BY) = bv_strCRTD_BY
                .Item(InvoiceData.CRTD_DT) = bv_strCRTD_DT
            End With
            objData.InsertRow(dr, INVOICEInsertQuery, br_objTrans)
            dr = Nothing
            CreateINVOICE = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateINVOICE() TABLE NAME:INVOICE"

    Public Function UpdateINVOICE(ByVal bv_i64INVC_BIN As Int64, _
        ByVal bv_strINVC_NO As String, _
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
        ByVal bv_strBLLNG_FLG As String, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_strCRTD_BY As String, _
        ByVal bv_strCRTD_DT As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(InvoiceData._INVOICE).NewRow()
            With dr
                .Item(InvoiceData.INVC_BIN) = bv_i64INVC_BIN
                .Item(InvoiceData.INVC_NO) = bv_strINVC_NO
                .Item(InvoiceData.INVC_DT) = bv_datINVC_DT
                If bv_strINVC_FL_PTH <> Nothing Then
                    .Item(InvoiceData.INVC_FL_PTH) = bv_strINVC_FL_PTH
                Else
                    .Item(InvoiceData.INVC_FL_PTH) = DBNull.Value
                End If
                .Item(InvoiceData.INVC_TYP) = bv_strINVC_TYP
                If bv_i64INVC_CRRNCY_ID <> 0 Then
                    .Item(InvoiceData.INVC_CRRNCY_ID) = bv_i64INVC_CRRNCY_ID
                Else
                    .Item(InvoiceData.INVC_CRRNCY_ID) = DBNull.Value
                End If
                ' .Item(InvoiceData.INVC_CRRNCY_ID) = bv_i64INVC_CRRNCY_ID
                .Item(InvoiceData.EXCHNG_RT_NC) = bv_strEXCHNG_RT_NC
                .Item(InvoiceData.CSTMR_CRRNCY_ID) = bv_i64CSTMR_CRRNCY_ID
                .Item(InvoiceData.BLLNG_TYP_ID) = bv_i64BLLNG_TYP_ID
                .Item(InvoiceData.FRM_BLLNG_DT) = bv_datFRM_BLLNG_DT
                .Item(InvoiceData.TO_BLLNG_DT) = bv_datTO_BLLNG_DT
                .Item(InvoiceData.TTL_CST_IN_CSTMR_CRRNCY_NC) = bv_strTTL_CST_IN_CSTMR_CRRNCY_NC
                .Item(InvoiceData.TTL_CST_IN_INVC_CRRNCY_NC) = bv_strTTL_CST_IN_INVC_CRRNCY_NC
                .Item(InvoiceData.BLLNG_FLG) = bv_strBLLNG_FLG
                .Item(InvoiceData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(InvoiceData.DPT_ID) = bv_i32DPT_ID
                .Item(InvoiceData.ACTV_BT) = bv_blnACTV_BT
                .Item(InvoiceData.CRTD_BY) = bv_strCRTD_BY
                .Item(InvoiceData.CRTD_DT) = bv_strCRTD_DT
            End With
            UpdateINVOICE = objData.UpdateRow(dr, INVOICEUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCUSTOMER() TABLE NAME:CUSTOMER"

    Public Function UpdateCUSTOMER(ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_LST_INVC_GNRTD_DT As Date, ByVal bv_Invc_Type As String, _
        ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(InvoiceData._CUSTOMER).NewRow()
            With dr
                .Item(InvoiceData.CSTMR_ID) = bv_i64CSTMR_ID

                If bv_i32DPT_ID <> 0 Then
                    .Item(InvoiceData.DPT_ID) = bv_i32DPT_ID
                Else
                    .Item(InvoiceData.DPT_ID) = DBNull.Value
                End If
                If bv_Invc_Type = "HS" Then
                    .Item(InvoiceData.LST_HS_INVC_GNRTD_DT) = bv_LST_INVC_GNRTD_DT
                    UpdateCUSTOMER = objData.UpdateRow(dr, CUSTOMEHSUpdateQuery, br_ObjTransactions)
                Else
                    .Item(InvoiceData.LST_RP_INVC_GNRTD_DT) = bv_LST_INVC_GNRTD_DT
                    UpdateCUSTOMER = objData.UpdateRow(dr, CUSTOMERRPUpdateQuery, br_ObjTransactions)
                End If
            End With
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateREPAIR_CHARGE() TABLE NAME:REPAIR_CHARGE"

    Public Function UpdateREPAIR_CHARGE(ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_datRPR_CMPLTN_DT As DateTime, _
        ByVal bv_strBLLNG_FLG As String, _
        ByVal bv_i32DPT_ID As Int32) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(InvoiceData._REPAIR_CHARGE).NewRow()
            With dr
                .Item(InvoiceData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(InvoiceData.RPR_CMPLTN_DT) = bv_datRPR_CMPLTN_DT
                .Item(InvoiceData.BLLNG_FLG) = bv_strBLLNG_FLG
                .Item(InvoiceData.DPT_ID) = bv_i32DPT_ID
            End With
            UpdateREPAIR_CHARGE = objData.UpdateRow(dr, REPAIR_CHARGEUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetInvoiceIdentityValue"
    Public Shared Function GetInvoiceIdentityValue(ByVal bv_strINVC_TYP As String, ByVal bv_datbllng_dt As Date, ByRef br_objTrans As Transactions) As String
        Try
            Dim objData As DataObjects
            Dim intIdentityValue As Int32
            Dim hshTable As New Hashtable
            Dim strTableName As String
            If bv_strINVC_TYP = "HS" Then
                strTableName = "HSINVOICE"
            Else
                strTableName = "RPINVOICE"
            End If
            hshTable.Add("@TBL_NM", strTableName)
            objData = New DataObjects(IdentitySelectQry, hshTable)
            intIdentityValue = objData.ExecuteScalar(br_objTrans)
            UpdateInvoiceIdentityValue(strTableName, br_objTrans)
            Dim strInvoiceNo As String = String.Concat(bv_strINVC_TYP, Month(bv_datbllng_dt).ToString.PadLeft(2, "0"c), Year(bv_datbllng_dt).ToString.Substring(Year(bv_datbllng_dt).ToString.Length - 2), intIdentityValue.ToString.PadLeft(4, "0"c))
            Return strInvoiceNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateInvoiceIdentityValue"

    Public Shared Function UpdateInvoiceIdentityValue(ByVal bv_strTableName As String, ByRef br_objTrans As Transactions) As Long
        Try
            Dim objData As DataObjects
            objData = New DataObjects(IdentityUpdateQry, "@TBL_NM", bv_strTableName)
            Return objData.ExecuteScalar(br_objTrans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetINVOICEbyPeriod() TABLE NAME:INVOICE"

    Public Function GetINVOICEbyPeriod(ByVal bvdatFRM_BLLNG_DT As Date, ByVal bvdatTO_BLLNG_DT As Date, _
                                   ByVal bv_i32DPT_ID As Int32, ByVal bv_i32CSTMR_ID As Long, _
                                   ByVal bv_strBLLNG_FLG As String, ByVal bv_strINVC_TYP As String) As InvoiceDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(InvoiceData.FRM_BLLNG_DT, bvdatFRM_BLLNG_DT)
            hshTable.Add(InvoiceData.TO_BLLNG_DT, bvdatTO_BLLNG_DT)
            hshTable.Add(InvoiceData.DPT_ID, bv_i32DPT_ID)
            hshTable.Add(InvoiceData.CSTMR_ID, bv_i32CSTMR_ID)
            hshTable.Add(InvoiceData.BLLNG_FLG, bv_strBLLNG_FLG)
            hshTable.Add(InvoiceData.INVC_TYP, bv_strINVC_TYP)
            objData = New DataObjects(INVOICESelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), InvoiceData._INVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetINVOICEByINVC_BIN() TABLE NAME:INVOICE"

    Public Function GetINVOICEByINVC_BIN(ByVal bv_i64INVC_BIN As Int64, ByVal bv_i32DPT_ID As Int32, _
                                         ByVal br_objTrans As Transactions) As InvoiceDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(InvoiceData.INVC_BIN, bv_i64INVC_BIN)
            hshParameters.Add(InvoiceData.DPT_ID, bv_i32DPT_ID)
            objData = New DataObjects(INVOICESelectQueryByINVC_BIN, hshParameters)
            objData.Fill(CType(ds, DataSet), InvoiceData._INVOICE, br_objTrans)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class

#End Region
