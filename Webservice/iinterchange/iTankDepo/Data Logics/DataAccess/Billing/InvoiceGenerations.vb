Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

#Region "Invoice Generations"
Public Class InvoiceGenerations

#Region "Declaration Part.."
    Dim objData As DataObjects

    Private Const HANDLING_CHARGESelectQueryByCustomerId As String = "SELECT HNDLNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT, TO_BLLNG_DT,GTN_DT,FR_DYS,NO_OF_DYS,HNDLNG_CST_NC,HNDLNG_TX_RT_NC,TTL_CSTS_NC,BLLNG_FLG,ACTV_BT,DPT_ID,IS_GT_OT_FLG,YRD_LCTN,CSTMR_ID,CSTMR_CD,CSTMR_NAM,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) AS [STOARGE_DAYS],DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) - FR_DYS AS [CHARGEABLE_DAYS],(CASE CST_TYP WHEN 'HNDIN' THEN 'IN' ELSE 'OUT' END) AS ACTIVITY,CSTMR_NAM AS INVOICING_PARTY,FRM_BLLNG_DT AS ACTIVITY_DATE,DRFT_INVC_NO,FNL_INVC_NO,PRDCT_DSCRPTN_VC FROM V_HANDLING_CHARGE WHERE CSTMR_ID=@CSTMR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND TO_BLLNG_DT<=@TO_BLLNG_DT AND HTNG_BT=0  AND BLLNG_FLG<>'B'"
    Private Const STORAGE_CHARGESelectQueryByCustomerId As String = "SELECT STRG_CHRG_ID,STRG_CHRG_ID AS CSTMR_CHRG_DTL_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,(CASE WHEN TO_BLLNG_DT IS NOT NULL THEN TO_BLLNG_DT ELSE NULL END)GTOT_DT,GTN_DT,FR_DYS,NO_OF_DYS,STRG_CST_NC,STRG_TX_RT_NC,TTL_CSTS_NC,STRG_CNTN_FLG,BLLNG_FLG,IS_GT_OT_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,CSTMR_ID,CSTMR_CD,DPT_ID,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,DRFT_INVC_NO,FNL_INVC_NO,PRDCT_DSCRPTN_VC,CSTMR_CHNG_BT FROM V_STORAGE_CHARGE WHERE CSTMR_ID=@CSTMR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND FRM_BLLNG_DT<=@TO_BLLNG_DT AND HTNG_BT=0 AND  ((BLLNG_FLG = 'B' AND [STRG_CNTN_FLG]<>'S') OR BLLNG_FLG='U'  OR BLLNG_FLG='D')"
    Private Const Customer_SelectQuery As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM,CNTCT_ADDRSS,CSTMR_CRRNCY_ID,CSTMR_CRRNCY_CD,BLLNG_TYP_CD,CNTCT_PRSN_NAM,DPT_ID,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=V_CUSTOMER.CSTMR_CRRNCY_ID AND ACTV_BT=1 AND DPT_ID=V_CUSTOMER.DPT_ID) AS  FRM_CRRNCY_CD,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM V_BANK_DETAIL WHERE DPT_ID=V_CUSTOMER.DPT_ID AND BNK_TYP_ID=44)AND ACTV_BT=1 AND DPT_ID=V_CUSTOMER.DPT_ID) AS  TO_CRRNCY_CD,CSTMR_CRRNCY_ID AS [BSE_CRRNCY_ID] FROM V_CUSTOMER  WHERE ACTV_BT = 1 AND CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private Const Invoicing_Party_SelectQuery As String = "SELECT INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,CNTCT_PRSN_NM,CNTCT_JB_TTL,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,RMRKS_VC,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,BS_CRRNCY_ID,BS_CRRNCY_CD,DPT_ID,DPT_CD,ACTV_BT,MDFD_BY,MDFD_DT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=V_INVOICING_PARTY.BS_CRRNCY_ID AND ACTV_BT=1 AND DPT_ID=V_INVOICING_PARTY.DPT_ID) AS  FRM_CRRNCY_CD,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM V_BANK_DETAIL WHERE DPT_ID=V_INVOICING_PARTY.DPT_ID AND BNK_TYP_ID=44)AND ACTV_BT=1 AND DPT_ID=V_INVOICING_PARTY.DPT_ID) AS  TO_CRRNCY_CD,BS_CRRNCY_ID AS [BSE_CRRNCY_ID] FROM V_INVOICING_PARTY WHERE INVCNG_PRTY_ID=@INVCNG_PRTY_ID AND DPT_ID=@DPT_ID AND ACTV_BT=1"
    Private Const REPAIR_CHARGESelectQueryByCustomerID As String = "SELECT RPR_CHRG_ID,EQPMNT_NO,RPR_ESTMT_ID,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,ESTMT_NO,RPR_APPRVL_DT,RPR_CMPLTN_DT,MTRL_CST_NC,LBR_CST_NC,RPR_TX_RT_NC,RPR_TX_AMNT_NC,TTL_CSTS_NC,RPR_APPRVL_AMNT_NC,BLLNG_FLG,YRD_LCTN,DPT_ID,DPT_CD,DPT_NAM,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_CD,GTN_DT,CLN_CST_NC,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,ACTV_BT,TTL_SRV_TAX,TTL_EST_INCL_SRV_TAX,INVC_TYPE,'RPC' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,RPR_CMPLTN_DT AS ACTIVITY_DATE,LEAK_TEST,PTI,SURVEY,SRVC_PRTNR_ID,TOTALAMOUNT,APPRVL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,ADDTNL_CLNNG_CHRG_NC,APPRVL_RF_NO FROM V_REPAIR_CHARGE WHERE SRVC_PRTNR_ID=@SRVC_PRTNR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND RPR_CMPLTN_DT>=@FRM_BLLNG_DT AND RPR_CMPLTN_DT<=@TO_BLLNG_DT AND BLLNG_FLG<>'B'"
    Private Const CLEANING_CHARGESelectQueryByCustomerID As String = "SELECT CLNNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_CD,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,BLLNG_FLG,CLNNG_ID,DPT_ID,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_DSCRPTN_VC,GTN_DT,ACTV_BT,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RT,CLNNG_CERT_NO,SRVC_PRTNR_ID,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,'CLN' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,ORGNL_CLNNG_DT AS ACTIVITY_DATE,RFRNC_NO,PRDCT_CD,EQPMNT_CD_ID,EQPMNT_TYP_ID,DRFT_INVC_NO,FNL_INVC_NO,SLB_RT_BT FROM V_CLEANING_CHARGE WHERE SRVC_PRTNR_ID=@SRVC_PRTNR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND ORGNL_INSPCTD_DT>=@FRM_BLLNG_DT AND ORGNL_INSPCTD_DT<=@TO_BLLNG_DT AND BLLNG_FLG<>'B'"
    Private Const Miscellaneous_InvoiceSelectQueryByInvoicingPartyID As String = "SELECT MSCLLNS_INVC_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,INVCNG_TO_ID,INVCNG_TO_CD,INVCNG_TO_NAM,SRVC_PRTNR_TYP_CD,ACTVTY_DT,CHRG_DSCRPTN,AMNT_NC,BLLNG_FLG,DPT_ID,DPT_CD,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DRFT_INVC_NO,FNL_INVC_NO FROM V_MISCELLANEOUS_INVOICE WHERE INVCNG_TO_ID=@INVCNG_TO_ID AND DPT_ID=@DPT_ID AND ACTVTY_DT>=@FRM_BLLNG_DT AND ACTVTY_DT<=@TO_BLLNG_DT AND BLLNG_FLG<>'B' AND (MIS_TYP is null or MIS_TYP <> 'CREDIT NOTE')"
    Private Const HEATING_CHARGESelectQueryByCustomerID As String = "SELECT HTNG_ID,HTNG_CD,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,HTNG_STRT_DT,HTNG_STRT_TM,HTNG_END_DT,HTNG_END_TM,HTNG_TMPRTR,TTL_HTN_PRD,MIN_HTNG_RT_NC,HRLY_CHRG_NC,TTL_RT_NC,BLLNG_FLG,GI_TRNSCTN_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,PRDCT_CD,PRDCT_DSCRPTN_VC,GTN_DT,SRVC_PRTNR_ID,HTNG_STRT_DT_TM,HTNG_END_DT_TM,DRFT_INVC_NO,FNL_INVC_NO FROM V_HEATING_CHARGE WHERE SRVC_PRTNR_ID=@SRVC_PRTNR_ID AND DPT_ID=@DPT_ID AND HTNG_STRT_DT>=@FRM_BLLNG_DT AND HTNG_END_DT<=@TO_BLLNG_DT AND BLLNG_FLG<>'B'"
    Private Const ExchangeRateSelectQueryByDepotIDServicePartner As String = "SELECT TOP 1 EXCHNG_RT_ID,FRM_CRRNCY_ID,FRM_CRRNCY_CD,TO_CRRNCY_ID,TO_CRRNCY_CD,EXCHNG_RT_PR_UNT_NC,WTH_EFFCT_FRM_DT,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM V_EXCHANGE_RATE WHERE FRM_CRRNCY_ID =(SELECT CRRNCY_ID FROM V_SERVICE_PARTNER WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 AND SRVC_PRTNR_ACTL_ID=@SRVC_PRTNR_ID AND SRVC_PRTNR_TYP_CD=@SRVC_PRTNR_TYP_CD) AND TO_CRRNCY_ID=(SELECT CRRNCY_ID FROM V_BANK_DETAIL WHERE DPT_ID=@DPT_ID AND BNK_TYP_ID=44) AND ACTV_BT=1 AND DPT_ID=@DPT_ID ORDER BY WTH_EFFCT_FRM_DT DESC"
    Private Const INVOICETableUpdateQueryInvoiceNoBillingFlag As String = "UPDATE @INVOICE_TABLE SET BLLNG_FLG=@BLLNG_FLG,DRFT_INVC_NO=@DRFT_INVC_NO WHERE DPT_ID=@DPT_ID AND "
    Private Const Invoice_HistoryInsertQuery As String = "INSERT INTO INVOICE_HISTORY(INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_CRRNCY_ID,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,INVCNG_PRTY_ID,DPT_ID,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT)VALUES(@INVC_BIN,@INVC_NO,@INVC_DT,@INVC_FL_PTH,@INVC_TYP,@INVC_CRRNCY_ID,@EXCHNG_RT_NC,@CSTMR_CRRNCY_ID,@BLLNG_TYP_ID,@FRM_BLLNG_DT,@TO_BLLNG_DT,@TTL_CST_IN_CSTMR_CRRNCY_NC,@TTL_CST_IN_INVC_CRRNCY_NC,@BLLNG_FLG,@CSTMR_ID,@INVCNG_PRTY_ID,@DPT_ID,@ACTV_BT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT)"
    'Private Const Invoice_HistorySelectQueryByFromToBillingDate As String = "SELECT INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_CRRNCY_ID,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,INVCNG_PRTY_ID,DPT_ID,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,FL_NM FROM INVOICE_HISTORY WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 AND INVC_TYP=@INVC_TYP AND  BLLNG_FLG=@BLLNG_FLG AND ((FRM_BLLNG_DT>=@FRM_BLLNG_DT AND FRM_BLLNG_DT<=@TO_BLLNG_DT)OR (TO_BLLNG_DT>=@FRM_BLLNG_DT AND TO_BLLNG_DT<=@TO_BLLNG_DT)) AND "
    Private Const Invoice_HistorySelectQueryByFromToBillingDate As String = "SELECT INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_CRRNCY_ID,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,INVCNG_PRTY_ID,DPT_ID,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,FL_NM FROM INVOICE_HISTORY WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 AND INVC_TYP=@INVC_TYP AND  BLLNG_FLG=@BLLNG_FLG AND ((TO_BLLNG_DT>=@FRM_BLLNG_DT)) AND "
    Private Const Invoice_HistorySelectQueryByInvoiceNo As String = "SELECT INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_CRRNCY_ID,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,INVCNG_PRTY_ID,DPT_ID,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,FL_NM FROM INVOICE_HISTORY WHERE  ACTV_BT=1 AND DPT_ID=@DPT_ID AND INVC_NO=@INVC_NO"
    Private Const Invoice_HistoryUpdateQuery As String = "UPDATE INVOICE_HISTORY SET INVC_DT=@INVC_DT,INVC_CRRNCY_ID=@INVC_CRRNCY_ID,EXCHNG_RT_NC=@EXCHNG_RT_NC,CSTMR_CRRNCY_ID=@CSTMR_CRRNCY_ID,FRM_BLLNG_DT=@FRM_BLLNG_DT,TO_BLLNG_DT=@TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC=@TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC=@TTL_CST_IN_INVC_CRRNCY_NC,MDFD_BY=@MDFD_BY,MDFD_DT=@MDFD_DT WHERE INVC_BIN=@INVC_BIN AND DPT_ID=@DPT_ID"
    Private Const INVOICE_TABLEUpdateQueryInvoiceNo As String = "UPDATE @INVOICE_TABLE SET DRFT_INVC_NO=NULL WHERE DPT_ID=@DPT_ID AND DRFT_INVC_NO =@DRFT_INVC_NO"
    Private Const INOVICE_HISTORYUpdateQuerybyInvoiceNo As String = "UPDATE INVOICE_HISTORY SET ACTV_BT=0 WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 AND INVC_TYP=@INVC_TYP AND INVC_NO<>@INVC_NO AND  BLLNG_FLG=@BLLNG_FLG AND ((FRM_BLLNG_DT>=@FRM_BLLNG_DT AND FRM_BLLNG_DT<=@TO_BLLNG_DT)OR (TO_BLLNG_DT>=@FRM_BLLNG_DT AND TO_BLLNG_DT<=@TO_BLLNG_DT)) AND "
    Private Const INVOICE_HISTORYSelectQueryByInvoiceTypeCustomerBillingFlag As String = "SELECT INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_CRRNCY_ID,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,INVCNG_PRTY_ID,DPT_ID,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,FL_NM FROM INVOICE_HISTORY WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 AND INVC_TYP=@INVC_TYP AND BLLNG_FLG=@BLLNG_FLG AND "
    Private Const ACTIVITY_STATUSSelectQueryByCustomer As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,GTN_DT,GTOT_DT,CLNNG_DT,INSPCTN_DT,RPR_CMPLTN_DT,PRDCT_ID,EQPMNT_STTS_ID,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSTRCTNS_VC,YRD_LCTN,ACTV_BT,DPT_ID,CLNNG_BLLNG_FLG,CLNNG_INVC_PRTY_ID FROM V_ACTIVITY_STATUS WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID @WHERE "
    Private Const V_TRANSPORTATION_INVOICESelectQuery As String = "SELECT TRNSPRTTN_CHRG_ID,INVC_DT,CSTMR_ID,CSTMR_CD,CSTMR_NAM,TRNSPRTTN_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_STT_ID,EQPMNT_STT_CD,RQST_NO,CSTMR_RF_NO,RT_ID,RT_CD,RT_DSCRPTN_VC,EVNT_DT,TRP_RT_NC,WGHTMNT_AMNT_NC,TKN_AMNT_NC,RCHRGBL_CST_AMNT_NC,FNNC_CHRG_AMNT_NC,DTNTN_AMNT_NC,OTHR_CHRG_AMNT_NC,TTL_RT_NC,DPT_AMNT,CSTMR_AMNT,DPT_CRRNCY_ID,CSTMR_CRRNCY_ID,DPT_CRRNCY_CD,CSTMR_CRRNCY_CD,EXCHNG_RT_PR_UNT_NC,FNL_INVC_NO,DRFT_INVC_NO,BLLNG_FLG,DPT_ID,JB_STRT_DT,JB_END_DT,NO_OF_TRIPS,EMPTY_SNGL_ID,EMPTY_SNGL_CD FROM V_TRANSPORTATION_INVOICE WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND JB_END_DT>=@FRM_BLLNG_DT AND JB_END_DT<=@TO_BLLNG_DT"
    Private Const V_RENTAL_CHARGESelectQueryByCustomerId As String = "SELECT RNTL_CHRG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RNTL_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,HNDLNG_OT_NC,HNDLNG_IN_NC,ON_HR_SRVY_NC,OFF_HR_SRVY_NC,FR_DYS,NO_OF_DYS,RNTL_PR_DY_NC,RNTL_TX_RT_NC,TTL_CSTS_NC,RNTL_CNTN_FLG,BLLNG_FLG,IS_GT_IN_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,BLLNG_TYP_CD,CSTMR_ID,DPT_ID,GI_TRNSCTN_NO,RNTL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,OTHR_CHRG_NC FROM V_RENTAL_CHARGE WHERE CSTMR_ID=@CSTMR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND FRM_BLLNG_DT<=@TO_BLLNG_DT AND ((BLLNG_FLG = 'B' AND [RNTL_CNTN_FLG]<>'S') OR BLLNG_FLG='U' OR BLLNG_FLG='D')"
    Private Const RENTAL_ENTRYSelectQueryByCustomer As String = "SELECT RNTL_ENTRY_ID,EQPMNT_NO,CSTMR_ID,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT,RMRKS_VC,RNTL_RFRNC_NO,GI_TRNSCTN_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,GTN_BT,OTHR_CHRG_NC FROM RENTAL_ENTRY WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID AND ON_HR_DT IS NOT NULL ORDER BY ON_HR_DT ASC"

    '' Validation 22294
    Private Const GetToBillingDateFinalizedInvoiceQuery As String = "SELECT MAX(TO_BLLNG_DT) FROM INVOICE_HISTORY WHERE BLLNG_FLG='FINAL' AND INVC_TYP=@INVC_TYP AND CSTMR_ID=@CSTMR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID"

    'Credit Note
    Private Const CreditNote_SelectQueryByInvoicingPartyID As String = "SELECT MSCLLNS_INVC_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,INVCNG_TO_ID,INVCNG_TO_CD,INVCNG_TO_NAM,SRVC_PRTNR_TYP_CD,ACTVTY_DT,CHRG_DSCRPTN,AMNT_NC,BLLNG_FLG,DPT_ID,DPT_CD,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DRFT_INVC_NO,FNL_INVC_NO FROM V_MISCELLANEOUS_INVOICE WHERE INVCNG_TO_ID=@INVCNG_TO_ID AND DPT_ID=@DPT_ID AND ACTVTY_DT>=@FRM_BLLNG_DT AND ACTVTY_DT<=@TO_BLLNG_DT AND BLLNG_FLG<>'B' and MIS_TYP ='CREDIT NOTE'"

    'GWS Inspection
    Private Const Inspection_SelectQueryByInvoicingPartyID As String = "SELECT INSPCTN_CHRG_ID,EQPMNT_NO,CSTMR_AGNT_ID,INSPCTD_DT,INSPCTN_CHRG,BLLNG_FLG,INSPCTD_BY,ACTV_BT,DPT_ID,GI_TRNSCTN_NO, DRFT_INVC_NO,FNL_INVC_NO,'INS' AS ACTIVITY,EQPMNT_TYP_ID,EQPMNT_TYP_CD,GTN_DT,EIR_NO,BLL_CD,(CASE WHEN BLL_CD = 'CUSTOMER' THEN (SELECT CSTMR_CD FROM CUSTOMER WHERE CSTMR_ID= IC.CSTMR_AGNT_ID) ELSE (SELECT AGNT_CD  FROM AGENT  WHERE AGNT_ID = IC.CSTMR_AGNT_ID) END)INVOICING_PARTY,INSPCTD_DT  AS ACTIVITY_DATE,EQPMNT_TYP_ID,DRFT_INVC_NO,FNL_INVC_NO FROM V_INSPECTION_CHARGES IC WHERE CSTMR_AGNT_ID=@CSTMR_AGNT_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND INSPCTD_DT>=@FRM_BLLNG_DT AND INSPCTD_DT<=@TO_BLLNG_DT AND BLLNG_FLG<>'B'"
    Private Const GetActivityStatusByAgent_SelectQry As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,GTN_DT,GTOT_DT,CLNNG_DT,INSPCTN_DT,RPR_CMPLTN_DT,PRDCT_ID,EQPMNT_STTS_ID,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSTRCTNS_VC,YRD_LCTN,ACTV_BT,DPT_ID,AGNT_ID,BLL_ID FROM V_ACTIVITY_STATUS WHERE BLL_ID='AGENT' AND AGNT_ID=@AGNT_ID AND DPT_ID=@DPT_ID ORDER BY GTN_DT ASC"

    'GWS Handling Charge
    Private Const Get_GWSHandlingChargeByCustomerId_SelectQry As String = "SELECT HNDLNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT, TO_BLLNG_DT,GTN_DT,FR_DYS,NO_OF_DYS,HNDLNG_CST_NC,HNDLNG_TX_RT_NC,TTL_CSTS_NC,BLLNG_FLG,ACTV_BT,DPT_ID,IS_GT_OT_FLG,YRD_LCTN,CSTMR_ID,CSTMR_CD,CSTMR_NAM,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) AS [STOARGE_DAYS],DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) - FR_DYS AS [CHARGEABLE_DAYS],(CASE CST_TYP WHEN 'HNDIN' THEN 'IN' ELSE 'OUT' END) AS ACTIVITY,CSTMR_NAM AS INVOICING_PARTY,FRM_BLLNG_DT AS ACTIVITY_DATE,DRFT_INVC_NO,FNL_INVC_NO,PRDCT_DSCRPTN_VC FROM V_HANDLING_CHARGE WHERE CSTMR_ID=@CSTMR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND TO_BLLNG_DT<=@TO_BLLNG_DT AND HTNG_BT=0  AND BLLNG_FLG<>'B' AND AGNT_ID IS NULL"
    Private Const Get_GWSHandlingChargeByAgentId_SelectQry As String = "SELECT HNDLNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT, TO_BLLNG_DT,GTN_DT,FR_DYS,NO_OF_DYS,HNDLNG_CST_NC,HNDLNG_TX_RT_NC,TTL_CSTS_NC,BLLNG_FLG,ACTV_BT,DPT_ID,IS_GT_OT_FLG,YRD_LCTN,CSTMR_ID,CSTMR_CD,CSTMR_NAM,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) AS [STOARGE_DAYS],DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) - FR_DYS AS [CHARGEABLE_DAYS],(CASE CST_TYP WHEN 'HNDIN' THEN 'IN' ELSE 'OUT' END) AS ACTIVITY,CSTMR_NAM AS INVOICING_PARTY,FRM_BLLNG_DT AS ACTIVITY_DATE,DRFT_INVC_NO,FNL_INVC_NO,PRDCT_DSCRPTN_VC,AGNT_ID FROM V_HANDLING_CHARGE WHERE AGNT_ID=@AGNT_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND TO_BLLNG_DT<=@TO_BLLNG_DT AND HTNG_BT=0  AND BLLNG_FLG<>'B'"

    'GWS Storage Charge
    Private Const Get_GWSStorageChargeByCustomerId_SelectQry As String = "SELECT STRG_CHRG_ID,STRG_CHRG_ID AS CSTMR_CHRG_DTL_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,(CASE WHEN TO_BLLNG_DT IS NOT NULL THEN TO_BLLNG_DT ELSE NULL END)GTOT_DT,GTN_DT,FR_DYS,NO_OF_DYS,STRG_CST_NC,STRG_TX_RT_NC,TTL_CSTS_NC,STRG_CNTN_FLG,BLLNG_FLG,IS_GT_OT_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,CSTMR_ID,CSTMR_CD,DPT_ID,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,DRFT_INVC_NO,FNL_INVC_NO,PRDCT_DSCRPTN_VC,CSTMR_CHNG_BT FROM V_STORAGE_CHARGE WHERE CSTMR_ID=@CSTMR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND FRM_BLLNG_DT<=@TO_BLLNG_DT AND HTNG_BT=0 AND  ((BLLNG_FLG = 'B' AND [STRG_CNTN_FLG]<>'S') OR BLLNG_FLG='U'  OR BLLNG_FLG='D') AND AGNT_ID IS NULL"
    Private Const Get_GWSStorageChargeByAgentId_SelectQry As String = "SELECT STRG_CHRG_ID,STRG_CHRG_ID AS CSTMR_CHRG_DTL_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,(CASE WHEN TO_BLLNG_DT IS NOT NULL THEN TO_BLLNG_DT ELSE NULL END)GTOT_DT,GTN_DT,FR_DYS,NO_OF_DYS,STRG_CST_NC,STRG_TX_RT_NC,TTL_CSTS_NC,STRG_CNTN_FLG,BLLNG_FLG,IS_GT_OT_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,CSTMR_ID,CSTMR_CD,DPT_ID,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,DRFT_INVC_NO,FNL_INVC_NO,PRDCT_DSCRPTN_VC,CSTMR_CHNG_BT,AGNT_ID FROM V_STORAGE_CHARGE WHERE AGNT_ID=@AGNT_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND FRM_BLLNG_DT<=@TO_BLLNG_DT AND HTNG_BT=0 AND  ((BLLNG_FLG = 'B' AND [STRG_CNTN_FLG]<>'S') OR BLLNG_FLG='U'  OR BLLNG_FLG='D')"

    Private Const GetRepairChargeGWS_ByCustomerId_SelectQry As String = "SELECT RPR_CHRG_ID,EQPMNT_NO,RPR_ESTMT_ID,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,ESTMT_NO,RPR_APPRVL_DT,RPR_CMPLTN_DT,MTRL_CST_NC,LBR_CST_NC,RPR_TX_RT_NC,RPR_TX_AMNT_NC,TTL_CSTS_NC,RPR_APPRVL_AMNT_NC,BLLNG_FLG,YRD_LCTN,DPT_ID,DPT_CD,DPT_NAM,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_CD,GTN_DT,CLN_CST_NC,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,ACTV_BT,TTL_SRV_TAX,TTL_EST_INCL_SRV_TAX,INVC_TYPE,'RPC' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,RPR_CMPLTN_DT AS ACTIVITY_DATE,LEAK_TEST,PTI,SURVEY,SRVC_PRTNR_ID,TOTALAMOUNT,APPRVL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,ADDTNL_CLNNG_CHRG_NC,APPRVL_RF_NO,AGNT_ID,EQPMNT_CD_CD FROM V_REPAIR_CHARGE WHERE SRVC_PRTNR_ID=@SRVC_PRTNR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND RPR_CMPLTN_DT>=@FRM_BLLNG_DT AND RPR_CMPLTN_DT<=@TO_BLLNG_DT AND BLLNG_FLG<>'B' AND AGNT_ID IS NULL"
    Private Const GetRepairChargeGWS_ByAgentId_SelectQry As String = "SELECT RPR_CHRG_ID,EQPMNT_NO,RPR_ESTMT_ID,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,ESTMT_NO,RPR_APPRVL_DT,RPR_CMPLTN_DT,MTRL_CST_NC,LBR_CST_NC,RPR_TX_RT_NC,RPR_TX_AMNT_NC,TTL_CSTS_NC,RPR_APPRVL_AMNT_NC,BLLNG_FLG,YRD_LCTN,DPT_ID,DPT_CD,DPT_NAM,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_CD,GTN_DT,CLN_CST_NC,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,ACTV_BT,TTL_SRV_TAX,TTL_EST_INCL_SRV_TAX,INVC_TYPE,'RPC' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,RPR_CMPLTN_DT AS ACTIVITY_DATE,LEAK_TEST,PTI,SURVEY,SRVC_PRTNR_ID,TOTALAMOUNT,APPRVL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,ADDTNL_CLNNG_CHRG_NC,APPRVL_RF_NO,AGNT_ID,EQPMNT_CD_CD FROM V_REPAIR_CHARGE WHERE AGNT_ID=@AGNT_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND RPR_CMPLTN_DT>=@FRM_BLLNG_DT AND RPR_CMPLTN_DT<=@TO_BLLNG_DT AND BLLNG_FLG<>'B'"

    Private Const ExchangeRateSelectQuery As String = "SELECT EXCHNG_RT_ID,FRM_CRRNCY_ID,TO_CRRNCY_ID,EXCHNG_RT_PR_UNT_NC, WTH_EFFCT_FRM_DT FROM EXCHANGE_RATE WHERE FRM_CRRNCY_ID=@FRM_CRRNCY_ID AND TO_CRRNCY_ID=@TO_CRRNCY_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID ORDER BY WTH_EFFCT_FRM_DT DESC"

    ''Cleaning Slab Rate Fetch Query
    Private Const CLEANING_CHARGESelectQueryByCustomerIDWithoutSlab As String = "SELECT CLNNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_CD,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,BLLNG_FLG,CLNNG_ID,DPT_ID,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_DSCRPTN_VC,GTN_DT,ACTV_BT,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RT,CLNNG_CERT_NO,SRVC_PRTNR_ID,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,'CLN' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,ORGNL_CLNNG_DT AS ACTIVITY_DATE,RFRNC_NO,PRDCT_CD,EQPMNT_CD_ID,EQPMNT_TYP_ID,DRFT_INVC_NO,FNL_INVC_NO,SLB_RT_BT FROM V_CLEANING_CHARGE WHERE SRVC_PRTNR_ID=@SRVC_PRTNR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND ORGNL_INSPCTD_DT>=@FRM_BLLNG_DT AND ORGNL_INSPCTD_DT<=@TO_BLLNG_DT AND BLLNG_FLG<>'B' AND SLB_RT_BT=0"
    Private Const CLEANING_CHARGESelectQueryByCustomerIDWithSlab As String = "SELECT CLNNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_CD,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,BLLNG_FLG,CLNNG_ID,DPT_ID,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_DSCRPTN_VC,GTN_DT,ACTV_BT,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RT,CLNNG_CERT_NO,SRVC_PRTNR_ID,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,'CLN' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,ORGNL_CLNNG_DT AS ACTIVITY_DATE,RFRNC_NO,PRDCT_CD,EQPMNT_CD_ID,EQPMNT_TYP_ID,DRFT_INVC_NO,FNL_INVC_NO,SLB_RT_BT FROM V_CLEANING_CHARGE WHERE SRVC_PRTNR_ID=@SRVC_PRTNR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND ORGNL_INSPCTD_DT>=@FRM_BLLNG_DT AND ORGNL_INSPCTD_DT<=@TO_BLLNG_DT AND SLB_RT_BT=1 AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    Private Const CLEANING_CHARGESelectQueryByCustomerIDWithInvoicePartyWithSlab As String = "SELECT CLNNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_CD,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,BLLNG_FLG,CLNNG_ID,DPT_ID,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_DSCRPTN_VC,GTN_DT,ACTV_BT,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RT,CLNNG_CERT_NO,SRVC_PRTNR_ID,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,'CLN' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,ORGNL_CLNNG_DT AS ACTIVITY_DATE,RFRNC_NO,PRDCT_CD,EQPMNT_CD_ID,EQPMNT_TYP_ID,DRFT_INVC_NO,FNL_INVC_NO,SLB_RT_BT FROM V_CLEANING_CHARGE WHERE SRVC_PRTNR_ID=@SRVC_PRTNR_ID AND CSTMR_ID=@CSTMR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND ORGNL_INSPCTD_DT>=@FRM_BLLNG_DT AND ORGNL_INSPCTD_DT<=@TO_BLLNG_DT AND SLB_RT_BT=1 AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    Private Const GetEquipCOuntCustomerIDWithInvoicePartyWithSlab As String = "SELECT COUNT(CLNNG_CHRG_ID) FROM V_CLEANING_CHARGE WHERE SRVC_PRTNR_ID=@SRVC_PRTNR_ID AND CSTMR_ID=@CSTMR_ID AND ACTV_BT=1 AND ORGNL_INSPCTD_DT>=@FRM_BLLNG_DT AND ORGNL_INSPCTD_DT<=@TO_BLLNG_DT AND SLB_RT_BT=1 AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    Private Const CleaingSlabFromCustomerByCustomerID As String = "SELECT CSTMR_CLNNG_DTL_ID,CSTMR_CHRG_DTL_ID,CSTMR_ID,UP_TO_CNTNRS,CLNNG_RT,RMRKS_VC,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_CD FROM V_CUSTOMER_CLEANING_DETAIL WHERE CSTMR_ID=@CSTMR_ID AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID @WHERE "
    Private Const CLEANING_CHARGE_GetDistinctEquipType As String = "SELECT DISTINCT EQPMNT_TYP_ID FROM V_CLEANING_CHARGE WHERE SRVC_PRTNR_ID=@SRVC_PRTNR_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND ORGNL_INSPCTD_DT>=@FRM_BLLNG_DT AND ORGNL_INSPCTD_DT<=@TO_BLLNG_DT AND SLB_RT_BT=1"
    Private Const CLEANING_CHARGE_GetChargeByInvoicingParty As String = "SELECT CLNNG_CHRG_ID,EQPMNT_NO,CSTMR_ID,INVCNG_PRTY_ID,CLNNG_ID,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RT,BLLNG_FLG,CLNNG_CERT_NO,ACTV_BT,DPT_ID,GI_TRNSCTN_NO,DRFT_INVC_NO,FNL_INVC_NO,SLB_RT_BT FROM CLEANING_CHARGE WHERE INVCNG_PRTY_ID=@INVCNG_PRTY_ID AND DPT_ID=@DPT_ID "
    Private Const GetEquipCountForAllDepotsByEquipType As String = "SELECT COUNT(CLNNG_CHRG_ID) FROM V_CLEANING_CHARGE WHERE SRVC_PRTNR_ID=@SRVC_PRTNR_ID AND ACTV_BT=1 AND ORGNL_INSPCTD_DT>=@FRM_BLLNG_DT AND ORGNL_INSPCTD_DT<=@TO_BLLNG_DT AND SLB_RT_BT=1 AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    Private ds As InvoiceGenerationDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New InvoiceGenerationDataSet
    End Sub

#End Region

#Region "GetHandlingChargeByCustomerId() TABLE NAME:HANDLING_CHARGE"

    Public Function GetHandlingChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                  ByVal bv_datPeriodFrom As DateTime, _
                                                  ByVal bv_datPeriodTo As DateTime, _
                                                  ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.CSTMR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(HANDLING_CHARGESelectQueryByCustomerId, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_HANDLING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_GWSHandlingChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                  ByVal bv_datPeriodFrom As DateTime, _
                                                  ByVal bv_datPeriodTo As DateTime, _
                                                  ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.CSTMR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(Get_GWSHandlingChargeByCustomerId_SelectQry, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_HANDLING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_GWSHandlingChargeByAgentId(ByVal bv_i64AgentId As Int64, _
                                                 ByVal bv_datPeriodFrom As DateTime, _
                                                 ByVal bv_datPeriodTo As DateTime, _
                                                 ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(GateinData.AGNT_ID, bv_i64AgentId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(Get_GWSHandlingChargeByAgentId_SelectQry, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_HANDLING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "GetStorageChargeByCustomerId() TABLE NAME:STORAGE_CHARGE"

    Public Function GetStorageChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                 ByVal bv_datPeriodFrom As DateTime, _
                                                 ByVal bv_datPeriodTo As DateTime, _
                                                 ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.CSTMR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(STORAGE_CHARGESelectQueryByCustomerId, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_STORAGE_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_GWSStorageChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                ByVal bv_datPeriodFrom As DateTime, _
                                                ByVal bv_datPeriodTo As DateTime, _
                                                ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.CSTMR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(Get_GWSStorageChargeByCustomerId_SelectQry, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_STORAGE_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_GWSStorageChargeByAgentId(ByVal bv_i64AgentId As Int64, _
                                                ByVal bv_datPeriodFrom As DateTime, _
                                                ByVal bv_datPeriodTo As DateTime, _
                                                ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(GateinData.AGNT_ID, bv_i64AgentId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(Get_GWSStorageChargeByAgentId_SelectQry, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_STORAGE_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetCustomerDetail  Table Name:CUSTOMER"
    Public Function GetCustomerDetail(ByVal bv_i64CustomerId As Int64, ByVal bv_intDepotId As Integer) As InvoiceGenerationDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(InvoiceGenerationData.CSTMR_ID, bv_i64CustomerId)
            hshparameters.Add(InvoiceGenerationData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(Customer_SelectQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetInvoicingParty  Table Name:INVOICING_PARTY"
    Public Function GetInvoicingPartyExchangeRate(ByVal bv_i64InvoicingPartyId As Int64, ByVal bv_intDepotId As Integer) As InvoiceGenerationDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(InvoiceGenerationData.INVCNG_PRTY_ID, bv_i64InvoicingPartyId)
            hshparameters.Add(InvoiceGenerationData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(Invoicing_Party_SelectQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_INVOICING_PARTY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRepairChargeByCustomerId() TABLE NAME:REPAIR_CHARGE"

    Public Function GetRepairChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                ByVal bv_datPeriodFrom As DateTime, _
                                                ByVal bv_datPeriodTo As DateTime, _
                                                ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(REPAIR_CHARGESelectQueryByCustomerID, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_REPAIR_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetRepairChargeGWS_ByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                               ByVal bv_datPeriodFrom As DateTime, _
                                               ByVal bv_datPeriodTo As DateTime, _
                                               ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(GetRepairChargeGWS_ByCustomerId_SelectQry, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_REPAIR_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetRepairChargeGWS_ByAgentId(ByVal bv_i64AgentId As Int64, _
                                               ByVal bv_datPeriodFrom As DateTime, _
                                               ByVal bv_datPeriodTo As DateTime, _
                                               ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(GateinData.AGNT_ID, bv_i64AgentId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(GetRepairChargeGWS_ByAgentId_SelectQry, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_REPAIR_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCleaningChargeByCustomerId() TABLE NAME:REPAIR_CHARGE"

    Public Function GetCleaningChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                  ByVal bv_datPeriodFrom As DateTime, _
                                                  ByVal bv_datPeriodTo As DateTime, _
                                                  ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(CLEANING_CHARGESelectQueryByCustomerID, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_CLEANING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetMiscellaneousInvoiceeByInvoicingPartyID() TABLE NAME:MISCELLANEOUS_INVOICE"

    Public Function GetMiscellaneousInvoiceeByInvoicingPartyID(ByVal bv_i64CustomerId As Int64, _
                                                               ByVal bv_datPeriodFrom As DateTime, _
                                                               ByVal bv_datPeriodTo As DateTime, _
                                                               ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.INVCNG_TO_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(Miscellaneous_InvoiceSelectQueryByInvoicingPartyID, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_MISCELLANEOUS_INVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetHeatingChargeByCustomerId() TABLE NAME:HEATING_CHARGE"

    Public Function GetHeatingChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                 ByVal bv_datPeriodFrom As DateTime, _
                                                 ByVal bv_datPeriodTo As DateTime, _
                                                 ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(HEATING_CHARGESelectQueryByCustomerID, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_HEATING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetExchangeRateByDepotId() TABLE NAME:V_EXCHANGE_RATE"

    Public Function GetExchangeRateByDepotId(ByVal bv_i64ServiceId As Int64, _
                                             ByVal bv_strCustomerType As String, _
                                             ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            Dim strQuery As String = String.Empty
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_ID, bv_i64ServiceId)
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_TYP_CD, bv_strCustomerType)
            objData = New DataObjects(ExchangeRateSelectQueryByDepotIDServicePartner, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_EXCHANGE_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateInvoiceTableInvoiceNo()"
    Public Function UpdateInvoiceTableInvoiceNo(ByRef bv_strInvoiceNo As String, _
                                                ByVal bv_i32DepotID As Int32, _
                                                ByVal bv_strInvoiceTableName As String, _
                                                ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(bv_strInvoiceTableName).NewRow()
            With dr
                .Item(InvoiceGenerationData.DPT_ID) = bv_i32DepotID
                .Item(InvoiceGenerationData.DRFT_INVC_NO) = bv_strInvoiceNo
            End With
            UpdateInvoiceTableInvoiceNo = objData.UpdateRow(dr, INVOICE_TABLEUpdateQueryInvoiceNo.Replace("@INVOICE_TABLE", bv_strInvoiceTableName), br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateInvoiceNoBillingFlag()"
    Public Function UpdateInvoiceNoBillingFlag(ByVal bv_strPrimaryID As String, _
                                               ByVal bv_strBillingFlag As String, _
                                               ByRef bv_strInvoiceNo As String, _
                                               ByVal bv_i32DepotID As Int32, _
                                               ByVal bv_blnGenerateInvoiceNo As Boolean, _
                                               ByVal bv_strInvoiceTableName As String, _
                                               ByVal bv_strPrimaryKey As String, _
                                               ByVal bv_strInvoiceFormat As String, _
                                               ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            Dim intMax As Integer
            objData = New DataObjects()
            dr = ds.Tables(bv_strInvoiceTableName).NewRow()
            If bv_blnGenerateInvoiceNo Then
                intMax = CommonUIs.GetIdentityValue(bv_strInvoiceFormat, br_objTrans)
                bv_strInvoiceNo = CommonUIs.GetIdentityCode(bv_strInvoiceFormat, intMax, Now.Date, br_objTrans)
            End If
            With dr
                .Item(InvoiceGenerationData.BLLNG_FLG) = bv_strBillingFlag
                .Item(InvoiceGenerationData.DRFT_INVC_NO) = bv_strInvoiceNo
                .Item(InvoiceGenerationData.DPT_ID) = bv_i32DepotID
            End With
            UpdateInvoiceNoBillingFlag = objData.UpdateRow(dr, String.Concat(INVOICETableUpdateQueryInvoiceNoBillingFlag, bv_strPrimaryKey, " IN (", bv_strPrimaryID, ")").Replace("@INVOICE_TABLE", bv_strInvoiceTableName), br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateInvoiceHistory() TABLE NAME:Invoice_History"

    Public Function CreateInvoiceHistory(ByVal bv_strInvoiceNo As String, _
                                         ByVal bv_datInvoiceDate As DateTime, _
                                         ByVal bv_strInvoiceFilePath As String, _
                                         ByVal bv_strInvoiceType As String, _
                                         ByVal bv_i64InvoiceCurrencyID As Int64, _
                                         ByVal bv_decExchangeRate As Decimal, _
                                         ByVal bv_i64CustomerCurrencyID As Int64, _
                                         ByVal bv_i64BillingTypeID As Int64, _
                                         ByVal bv_datFromBillingDate As DateTime, _
                                         ByVal bv_datToBillingDate As DateTime, _
                                         ByVal bv_decTotalCostinCustomerCurrency As Decimal, _
                                         ByVal bv_decTotalCostinInvoiceCurrency As Decimal, _
                                         ByVal bv_strBillingFlag As String, _
                                         ByVal bv_i64CustomerID As Int64, _
                                         ByVal bv_i64InvoicingPartyID As Int64, _
                                         ByVal bv_i32DepotID As Int32, _
                                         ByVal bv_blnActiveBit As Boolean, _
                                         ByVal bv_strCreatedBy As String, _
                                         ByVal bv_datCreatedDate As DateTime, _
                                         ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(InvoiceGenerationData._INVOICE_HISTORY).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(InvoiceGenerationData._INVOICE_HISTORY, br_objTrans)
                .Item(InvoiceGenerationData.INVC_BIN) = intMax
                .Item(InvoiceGenerationData.INVC_NO) = bv_strInvoiceNo
                .Item(InvoiceGenerationData.INVC_DT) = bv_datInvoiceDate
                If bv_strInvoiceFilePath <> Nothing Then
                    .Item(InvoiceGenerationData.INVC_FL_PTH) = bv_strInvoiceFilePath
                Else
                    .Item(InvoiceGenerationData.INVC_FL_PTH) = DBNull.Value
                End If
                .Item(InvoiceGenerationData.INVC_TYP) = bv_strInvoiceType
                .Item(InvoiceGenerationData.INVC_CRRNCY_ID) = bv_i64InvoiceCurrencyID
                .Item(InvoiceGenerationData.EXCHNG_RT_NC) = bv_decExchangeRate
                .Item(InvoiceGenerationData.CSTMR_CRRNCY_ID) = bv_i64CustomerCurrencyID
                If bv_i64BillingTypeID <> 0 Then
                    .Item(InvoiceGenerationData.BLLNG_TYP_ID) = bv_i64BillingTypeID
                Else
                    .Item(InvoiceGenerationData.BLLNG_TYP_ID) = DBNull.Value
                End If
                .Item(InvoiceGenerationData.FRM_BLLNG_DT) = bv_datFromBillingDate
                .Item(InvoiceGenerationData.TO_BLLNG_DT) = bv_datToBillingDate
                .Item(InvoiceGenerationData.TTL_CST_IN_CSTMR_CRRNCY_NC) = bv_decTotalCostinCustomerCurrency
                .Item(InvoiceGenerationData.TTL_CST_IN_INVC_CRRNCY_NC) = bv_decTotalCostinInvoiceCurrency
                .Item(InvoiceGenerationData.BLLNG_FLG) = bv_strBillingFlag
                If bv_i64CustomerID <> 0 Then
                    .Item(InvoiceGenerationData.CSTMR_ID) = bv_i64CustomerID
                Else
                    .Item(InvoiceGenerationData.CSTMR_ID) = DBNull.Value
                End If
                If bv_i64InvoicingPartyID <> 0 Then
                    .Item(InvoiceGenerationData.INVCNG_PRTY_ID) = bv_i64InvoicingPartyID
                Else
                    .Item(InvoiceGenerationData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                .Item(InvoiceGenerationData.DPT_ID) = bv_i32DepotID
                .Item(InvoiceGenerationData.ACTV_BT) = bv_blnActiveBit
                .Item(InvoiceGenerationData.CRTD_BY) = bv_strCreatedBy
                .Item(InvoiceGenerationData.CRTD_DT) = bv_datCreatedDate
                .Item(InvoiceGenerationData.MDFD_BY) = bv_strCreatedBy
                .Item(InvoiceGenerationData.MDFD_DT) = bv_datCreatedDate
            End With
            objData.InsertRow(dr, Invoice_HistoryInsertQuery, br_objTrans)
            dr = Nothing
            CreateInvoiceHistory = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetInvoice_HistoryByInvoiceNo() TABLE NAME:Invoice_History"

    Public Function GetInvoice_HistoryByInvoiceNo(ByVal bv_strInvoiceNo As String, _
                                                  ByVal bv_i32DepotId As Int32, _
                                                  ByRef br_objTrans As Transactions) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            Dim strQuery As String = String.Empty
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.INVC_NO, bv_strInvoiceNo)
            objData = New DataObjects(Invoice_HistorySelectQueryByInvoiceNo, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._INVOICE_HISTORY, br_objTrans)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetInvoice_HistoryByFromToBillingDate() TABLE NAME:Invoice_History"

    Public Function GetInvoice_HistoryByFromToBillingDate(ByVal bv_i64CustomerId As Int64, _
                                                          ByVal bv_strBillingFlag As String, _
                                                          ByVal bv_i32DepotId As Int32, _
                                                          ByVal bv_datFromBillingDate As Date, _
                                                          ByVal bv_datToBillingDate As Date, _
                                                          ByVal bv_i64InvoicingPartyId As Int64, _
                                                          ByVal bv_strInvoiceType As String) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            Dim strQuery As String = String.Empty
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.BLLNG_FLG, bv_strBillingFlag)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datFromBillingDate)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datToBillingDate)
            hshparamters.Add(InvoiceGenerationData.INVC_TYP, bv_strInvoiceType)
            If bv_i64CustomerId <> 0 Then
                strQuery = String.Concat(Invoice_HistorySelectQueryByFromToBillingDate, InvoiceGenerationData.CSTMR_ID, "=", bv_i64CustomerId)
            ElseIf bv_i64InvoicingPartyId <> 0 Then
                strQuery = String.Concat(Invoice_HistorySelectQueryByFromToBillingDate, InvoiceGenerationData.INVCNG_PRTY_ID, "=", bv_i64InvoicingPartyId)
            End If
            objData = New DataObjects(strQuery, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._INVOICE_HISTORY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateInvoiceHistory() TABLE NAME:Invoice_History"

    Public Function UpdateInvoiceHistory(ByVal bv_i64InvoiceBin As Int64, _
                                         ByVal bv_datInvoiceDate As DateTime, _
                                         ByVal bv_i64InvoiceCurrencyID As Int64, _
                                         ByVal bv_decExchangeRate As Decimal, _
                                         ByVal bv_i64CustomerCurrencyID As Int64, _
                                         ByVal bv_datFromBillingDate As DateTime, _
                                         ByVal bv_datToBillingDate As DateTime, _
                                         ByVal bv_decTotalCostinCustomerCurrency As Decimal, _
                                         ByVal bv_decTotalCostinInvoiceCurrency As Decimal, _
                                         ByVal bv_i32DepotID As Int32, _
                                         ByVal bv_strModifiedBy As String, _
                                         ByVal bv_datModifiedDate As DateTime, _
                                         ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(InvoiceGenerationData._INVOICE_HISTORY).NewRow()
            With dr
                .Item(InvoiceGenerationData.INVC_BIN) = bv_i64InvoiceBin
                .Item(InvoiceGenerationData.INVC_DT) = bv_datInvoiceDate
                .Item(InvoiceGenerationData.INVC_CRRNCY_ID) = bv_i64InvoiceCurrencyID
                .Item(InvoiceGenerationData.EXCHNG_RT_NC) = bv_decExchangeRate
                .Item(InvoiceGenerationData.CSTMR_CRRNCY_ID) = bv_i64CustomerCurrencyID
                .Item(InvoiceGenerationData.FRM_BLLNG_DT) = bv_datFromBillingDate
                .Item(InvoiceGenerationData.TO_BLLNG_DT) = bv_datToBillingDate
                .Item(InvoiceGenerationData.TTL_CST_IN_CSTMR_CRRNCY_NC) = bv_decTotalCostinCustomerCurrency
                .Item(InvoiceGenerationData.TTL_CST_IN_INVC_CRRNCY_NC) = bv_decTotalCostinInvoiceCurrency
                .Item(InvoiceGenerationData.DPT_ID) = bv_i32DepotID
                .Item(InvoiceGenerationData.MDFD_BY) = bv_strModifiedBy
                .Item(InvoiceGenerationData.MDFD_DT) = bv_datModifiedDate
            End With
            UpdateInvoiceHistory = objData.UpdateRow(dr, Invoice_HistoryUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateInvoiceHistoryActiveBit() TABLE NAME:Invoice_History"

    Public Function UpdateInvoiceHistoryActiveBit(ByVal bv_i64CustomerId As Int64, _
                                                  ByVal bv_strBillingFlag As String, _
                                                  ByVal bv_i32DepotId As Int32, _
                                                  ByVal bv_datFromBillingDate As Date, _
                                                  ByVal bv_datToBillingDate As Date, _
                                                  ByVal bv_i64InvoicingPartyId As Int64, _
                                                  ByVal bv_strInvoiceType As String, _
                                                  ByVal bv_strInvoiceNo As String, _
                                                  ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            Dim strQuery As String = String.Empty
            dr = ds.Tables(InvoiceGenerationData._INVOICE_HISTORY).NewRow()
            With dr
                .Item(InvoiceGenerationData.INVC_TYP) = bv_strInvoiceType
                .Item(InvoiceGenerationData.FRM_BLLNG_DT) = bv_datFromBillingDate
                .Item(InvoiceGenerationData.TO_BLLNG_DT) = bv_datToBillingDate
                .Item(InvoiceGenerationData.DPT_ID) = bv_i32DepotId
                .Item(InvoiceGenerationData.INVC_NO) = bv_strInvoiceNo
                .Item(InvoiceGenerationData.BLLNG_FLG) = bv_strBillingFlag
            End With
            If bv_i64CustomerId <> 0 Then
                strQuery = String.Concat(INOVICE_HISTORYUpdateQuerybyInvoiceNo, InvoiceGenerationData.CSTMR_ID, "=", bv_i64CustomerId)
            ElseIf bv_i64InvoicingPartyId <> 0 Then
                strQuery = String.Concat(INOVICE_HISTORYUpdateQuerybyInvoiceNo, InvoiceGenerationData.INVCNG_PRTY_ID, "=", bv_i64InvoicingPartyId)
            End If
            UpdateInvoiceHistoryActiveBit = objData.UpdateRow(dr, strQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetVInvoiceHistoryByInvoiceTypeCustomerBillingFlag() TABLE NAME:INVOICE_HISTORY"

    Public Function GetVInvoiceHistoryByInvoiceTypeCustomerBillingFlag(ByVal bv_i32DepotID As Int64, _
                                                                       ByVal bv_strBilingFlag As String, _
                                                                       ByVal bv_strInvoiceType As String, _
                                                                       ByVal bv_i64CustomerId As Int64, _
                                                                       ByVal bv_i64InvoicingPartyId As Int64) As InvoiceGenerationDataSet
        Try
            Dim strQuery As String = String.Empty
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.BLLNG_FLG, bv_strBilingFlag)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.INVC_TYP, bv_strInvoiceType)
            If bv_i64CustomerId <> 0 Then
                strQuery = String.Concat(INVOICE_HISTORYSelectQueryByInvoiceTypeCustomerBillingFlag, CommonUIData.CSTMR_ID, "=", bv_i64CustomerId)
            ElseIf bv_i64InvoicingPartyId <> 0 Then
                strQuery = String.Concat(INVOICE_HISTORYSelectQueryByInvoiceTypeCustomerBillingFlag, CommonUIData.INVCNG_PRTY_ID, "=", bv_i64InvoicingPartyId)
            End If
            objData = New DataObjects(String.Concat(strQuery, " ORDER BY ", InvoiceGenerationData.INVC_BIN, " DESC"), hshParams)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._INVOICE_HISTORY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetActivityStatusByCustomer() TABLE NAME:ACTIVITY_STATUS"

    Public Function GetActivityStatusByCustomer(ByVal bv_i32DepotID As Int64, _
                                                ByVal bv_i64CustomerId As Int64, _
                                                ByVal bv_strBillingFlag As String) As DataTable
        Try
            Dim strQuery As String = String.Empty
            Dim dtACTIVITY_STATUS As New DataTable
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_ID, bv_i64CustomerId)
            objData = New DataObjects(ACTIVITY_STATUSSelectQueryByCustomer.Replace("@WHERE", bv_strBillingFlag), hshParams)
            objData.Fill(dtACTIVITY_STATUS)
            Return dtACTIVITY_STATUS
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetActivityStatusByAgent(ByVal bv_i32DepotID As Int64, _
                                             ByVal bv_i64AgentId As Int64) As DataTable
        Try
            Dim strQuery As String = String.Empty
            Dim dtACTIVITY_STATUS As New DataTable
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(GateinData.AGNT_ID, bv_i64AgentId)
            objData = New DataObjects(GetActivityStatusByAgent_SelectQry, hshParams)
            objData.Fill(dtACTIVITY_STATUS)
            Return dtACTIVITY_STATUS
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetTransportationChargeByCustomerId() TABLE NAME:TRANSPORTATION_INVOICE"

    Public Function GetTransportationChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                        ByVal bv_datPeriodFrom As DateTime, _
                                                        ByVal bv_datPeriodTo As DateTime, _
                                                        ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.CSTMR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(V_TRANSPORTATION_INVOICESelectQuery, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_TRANSPORTATION_INVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRentalChargeByCustomerId() TABLE NAME:RENTAL_CHARGE"

    Public Function GetRentalChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                  ByVal bv_datPeriodFrom As DateTime, _
                                                  ByVal bv_datPeriodTo As DateTime, _
                                                  ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.CSTMR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(V_RENTAL_CHARGESelectQueryByCustomerId, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_RENTAL_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRentalOnHireDateByCustomer() TABLE NAME:RENTAL_ENTRY"

    Public Function GetRentalOnHireDateByCustomer(ByVal bv_i32DepotID As Int64, _
                                                ByVal bv_i64CustomerId As Int64) As DataTable
        Try
            Dim strQuery As String = String.Empty
            Dim dtRentalEntry As New DataTable
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_ID, bv_i64CustomerId)
            objData = New DataObjects(RENTAL_ENTRYSelectQueryByCustomer, hshParams)
            objData.Fill(dtRentalEntry)
            Return dtRentalEntry
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Billing DraftValidations"
    Public Function validateFinalizedInvoice(ByVal bv_i32DepotID As Int64, _
                                             ByVal bv_i64CustomerId As Int64, _
                                             ByVal bv_strInvoiceType As String) As String
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotID)
            hshparameters.Add(InvoiceGenerationData.CSTMR_ID, bv_i64CustomerId)
            hshparameters.Add(InvoiceGenerationData.INVC_TYP, bv_strInvoiceType)
            objData = New DataObjects(GetToBillingDateFinalizedInvoiceQuery, hshparameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "validateFinalizedInvoiceTransactionWise() Validation"
    Public Function validateFinalizedInvoiceTransactionWise(ByVal bv_strQuery As String) As Int32
        Try
            objData = New DataObjects(bv_strQuery)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCreditByInvoicingPartyID() TABLE NAME:MISCELLANEOUS_INVOICE"

    Public Function GetCreditByInvoicingPartyID(ByVal bv_i64CustomerId As Int64, _
                                                               ByVal bv_datPeriodFrom As DateTime, _
                                                               ByVal bv_datPeriodTo As DateTime, _
                                                               ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.INVCNG_TO_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(CreditNote_SelectQueryByInvoicingPartyID, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_MISCELLANEOUS_INVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    'GWS Inspection
#Region "GetInspectionChargeByCustomerId() TABLE NAME:V_INSPECTION_CHARGES"

    Public Function GetInspectionChargeByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                  ByVal bv_datPeriodFrom As DateTime, _
                                                  ByVal bv_datPeriodTo As DateTime, _
                                                  ByVal bv_i32DepotId As Int32) As InvoiceGenerationDataSet
        Try
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.CSTMR_AGNT_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(Inspection_SelectQueryByInvoicingPartyID, hshparamters)
            objData.Fill(CType(ds, DataSet), InvoiceGenerationData._V_INSPECTION_CHARGES)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


#Region "GetExchangeRateWithEffectiveDate"
    ''' <summary>
    ''' Get Exchange Rate with effective date
    ''' </summary>
    ''' <param name="bv_i64FromCuurencyId"></param>
    ''' <param name="bv_i64ToCuurencyId"></param>
    ''' <param name="bv_datCurrentDateTime"></param>
    ''' <param name="bv_intDepotId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetExchangeRateWithEffectiveDate(ByVal bv_i64FromCuurencyId As Int64, ByVal bv_i64ToCuurencyId As Int64, ByVal bv_datCurrentDateTime As DateTime, ByVal bv_intDepotId As Int32) As DataTable
        Try
            Dim hshParameters As New Hashtable()
            Dim dtExchangeRate As New DataTable
            hshParameters.Add(RepairEstimateData.FRM_CRRNCY_ID, bv_i64FromCuurencyId)
            hshParameters.Add(RepairEstimateData.TO_CRRNCY_ID, bv_i64ToCuurencyId)
            hshParameters.Add(RepairEstimateData.WTH_EFFCT_FRM_DT, bv_datCurrentDateTime)
            hshParameters.Add(RepairEstimateData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(ExchangeRateSelectQuery, hshParameters)
            objData.Fill(dtExchangeRate)
            Return dtExchangeRate
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningChargeByCustomerIdWithoutSlab"
    Public Function GetCleaningChargeByCustomerIdWithoutSlab(ByVal bv_i64CustomerId As Int64, _
                                                             ByVal bv_datPeriodFrom As DateTime, _
                                                             ByVal bv_datPeriodTo As DateTime, _
                                                             ByVal bv_i32DepotId As Int32) As DataTable
        Try
            Dim dtCleaning As New InvoiceGenerationDataSet
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(String.Concat(CLEANING_CHARGESelectQueryByCustomerIDWithoutSlab), hshparamters)
            objData.Fill(dtCleaning.Tables(InvoiceGenerationData._V_CLEANING_CHARGE))
            Return dtCleaning.Tables(InvoiceGenerationData._V_CLEANING_CHARGE)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "GetCleaningChargeByCustomerIdWithSlab"
    Public Function GetCleaningChargeByCustomerIdWithSlabEquipType(ByVal bv_i64CustomerId As Int64, _
                                                             ByVal bv_datPeriodFrom As DateTime, _
                                                             ByVal bv_datPeriodTo As DateTime, _
                                                             ByVal bv_i32EquipTypeID As Integer, _
                                                             ByVal bv_i32DepotId As Int32, _
                                                             ByVal bv_strBillingFlag As String) As DataTable
        Try
            Dim dtCleaning As New InvoiceGenerationDataSet
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            hshparamters.Add(InvoiceGenerationData.EQPMNT_TYP_ID, bv_i32EquipTypeID)
            objData = New DataObjects(String.Concat(CLEANING_CHARGESelectQueryByCustomerIDWithSlab, bv_strBillingFlag), hshparamters)
            objData.Fill(dtCleaning.Tables(InvoiceGenerationData._V_CLEANING_CHARGE))
            Return dtCleaning.Tables(InvoiceGenerationData._V_CLEANING_CHARGE)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningChargeByCustomerIdWithSlab"
    Public Function GetCleaningChargeByCustomerIdAndPartyWithSlabEquipType(ByVal bv_i64ServcPartnerId As Int64, _
                                                                           ByVal bv_i64CustomerId As Int64, _
                                                             ByVal bv_datPeriodFrom As DateTime, _
                                                             ByVal bv_datPeriodTo As DateTime, _
                                                             ByVal bv_i32EquipTypeID As Integer, _
                                                             ByVal bv_i32DepotId As Int32, _
                                                             ByVal bv_strBillingFlag As String) As DataTable
        Try
            Dim dtCleaning As New InvoiceGenerationDataSet
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_ID, bv_i64ServcPartnerId)
            hshparamters.Add(InvoiceGenerationData.CSTMR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            hshparamters.Add(InvoiceGenerationData.EQPMNT_TYP_ID, bv_i32EquipTypeID)
            objData = New DataObjects(String.Concat(CLEANING_CHARGESelectQueryByCustomerIDWithInvoicePartyWithSlab, bv_strBillingFlag), hshparamters)
            objData.Fill(dtCleaning.Tables(InvoiceGenerationData._V_CLEANING_CHARGE))
            Return dtCleaning.Tables(InvoiceGenerationData._V_CLEANING_CHARGE)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetDistinctEquipmentTypeByCustomerIDWithSlab"
    Public Function GetDistinctEquipmentTypeByCustomerIDWithSlab(ByVal bv_i64CustomerId As Int64, _
                                                             ByVal bv_datPeriodFrom As DateTime, _
                                                             ByVal bv_datPeriodTo As DateTime, _
                                                             ByVal bv_i32DepotId As Int32, _
                                                             ByVal bv_strBillingFlag As String) As DataTable
        Try
            Dim dtCleaning As New InvoiceGenerationDataSet
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            objData = New DataObjects(String.Concat(CLEANING_CHARGE_GetDistinctEquipType, bv_strBillingFlag), hshparamters)
            objData.Fill(dtCleaning.Tables(InvoiceGenerationData._V_CLEANING_CHARGE))
            Return dtCleaning.Tables(InvoiceGenerationData._V_CLEANING_CHARGE)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningSlabRateByCustomerID"
    Public Function GetCleaningSlabRateByEquipTypeCustomerID(ByVal bv_i64CustomerID As Int64, _
                                                             ByVal bv_intEquipType As Integer, _
                                                             ByVal bv_UpToContainers As Int64) As DataTable
        Try
            Dim hshparamters As New Hashtable
            Dim dtCustomerCleaningTable As New DataTable
            hshparamters.Add(CustomerData.CSTMR_ID, bv_i64CustomerID)
            hshparamters.Add(CustomerData.UP_TO_CNTNRS, bv_UpToContainers)
            hshparamters.Add(CustomerData.EQPMNT_TYP_ID, bv_intEquipType)
            objData = New DataObjects(CleaingSlabFromCustomerByCustomerID.Replace("@WHERE", " AND UP_TO_CNTNRS >= @UP_TO_CNTNRS ORDER BY UP_TO_CNTNRS"), hshparamters)
            objData.Fill(dtCustomerCleaningTable)
            If dtCustomerCleaningTable.Rows.Count = 0 Then
                objData = New DataObjects(CleaingSlabFromCustomerByCustomerID.Replace("@WHERE", " ORDER BY UP_TO_CNTNRS DESC"), hshparamters)
                objData.Fill(dtCustomerCleaningTable)
            End If
            Return dtCustomerCleaningTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "GetCleaningChargeByInvoicingParty"
    Public Function GetCleaningChargeByInvoicingParty(ByVal bv_i32DepotID As Int64, _
                                                    ByVal bv_i64InvoicingParytId As Int64, _
                                                    ByVal bv_strBillingFlag As String) As DataTable
        Try
            Dim dtCleaning As New InvoiceGenerationDataSet
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.INVCNG_PRTY_ID, bv_i64InvoicingParytId)
            hshparamters.Add(InvoiceGenerationData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(String.Concat(CLEANING_CHARGE_GetChargeByInvoicingParty, bv_strBillingFlag), hshparamters)
            objData.Fill(dtCleaning.Tables(InvoiceGenerationData._V_CLEANING_CHARGE))
            Return dtCleaning.Tables(InvoiceGenerationData._V_CLEANING_CHARGE)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentCountByEqpmtType"
    Public Function GetEquipmentCountByEqpmtType(ByVal bv_i64CustomerId As Int64, _
                                                             ByVal bv_datPeriodFrom As DateTime, _
                                                             ByVal bv_datPeriodTo As DateTime, _
                                                             ByVal bv_i32EquipTypeID As Integer) As Long
        Try
            Dim totalEquipCount As Long
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            hshparamters.Add(InvoiceGenerationData.EQPMNT_TYP_ID, bv_i32EquipTypeID)
            objData = New DataObjects(GetEquipCountForAllDepotsByEquipType, hshparamters)
            totalEquipCount = objData.ExecuteScalar()
            Return totalEquipCount
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentCountForPartyByEqpmtType"
    Public Function GetEquipmentCountForPartyByEqpmtType(ByVal bv_i64ServcPartnerId As Int64, _
                                                                           ByVal bv_i64CustomerId As Int64, _
                                                             ByVal bv_datPeriodFrom As DateTime, _
                                                             ByVal bv_datPeriodTo As DateTime, _
                                                             ByVal bv_i32EquipTypeID As Integer) As Long
        Try
            Dim lngEquipCountByParty As Long
            Dim hshparamters As New Hashtable
            hshparamters.Add(InvoiceGenerationData.SRVC_PRTNR_ID, bv_i64ServcPartnerId)
            hshparamters.Add(InvoiceGenerationData.CSTMR_ID, bv_i64CustomerId)
            hshparamters.Add(InvoiceGenerationData.FRM_BLLNG_DT, bv_datPeriodFrom)
            hshparamters.Add(InvoiceGenerationData.TO_BLLNG_DT, bv_datPeriodTo)
            hshparamters.Add(InvoiceGenerationData.EQPMNT_TYP_ID, bv_i32EquipTypeID)
            objData = New DataObjects(GetEquipCOuntCustomerIDWithInvoicePartyWithSlab, hshparamters)
            lngEquipCountByParty = objData.ExecuteScalar()
            Return lngEquipCountByParty
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
#End Region

