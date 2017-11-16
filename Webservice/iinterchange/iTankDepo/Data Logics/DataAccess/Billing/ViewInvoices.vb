#Region " ViewInvoices.vb"
'*********************************************************************************************************************
'Name :
'      ViewInvoices.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(ViewInvoices.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      02/01/2014 18:29:54
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

#Region "ViewInvoices"

Public Class ViewInvoices

#Region "Declaration Part.. "

	Dim objData As DataObjects
    Private Const V_INVOICE_HISTORYSelectQueryByDepotID As String = "SELECT INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_NM,INVC_CRRNCY_ID,INVC_CRRNCY_CD,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,CSTMR_CRRNCY_CD,BLLNG_TYP_ID,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,INVCNG_PRTY_ID,DPT_ID,DPT_CD,DPT_NAM,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,SRVC_PRTNR_ID,SRVC_PRTNR_CD,SRVC_PRTNR_NM,FL_NM,XML_BT,CSTMR_CD,CUSTOMERCRRNCY,INVC_CNCL,VIEW_BT,(SELECT CRRNCY_ID FROM V_BANK_DETAIL WHERE DPT_ID =H.DPT_ID AND BNK_TYP_ID=44 )DPT_CRRNCY_ID FROM V_INVOICE_HISTORY H WHERE DPT_ID=@DPT_ID AND ACTV_BT=1 ORDER BY INVC_DT DESC"
    Private Const CLEANING_CHARGESelectQueryByDepotIDInvoiceNo As String = "SELECT CLNNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_CD,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,BLLNG_FLG,CLNNG_ID,DPT_ID,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_DSCRPTN_VC,GTN_DT,ACTV_BT,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RT,CLNNG_CERT_NO,SRVC_PRTNR_ID,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,'CLN' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,ORGNL_CLNNG_DT AS ACTIVITY_DATE,RFRNC_NO,PRDCT_CD,EQPMNT_CD_ID,EQPMNT_TYP_ID,DRFT_INVC_NO,FNL_INVC_NO FROM V_CLEANING_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND SRVC_PRTNR_ID=@SRVC_PRTNR_ID"
    Private Const REPAIR_CHARGESelectQueryByDepotIDInvoiceNo As String = "SELECT RPR_CHRG_ID,EQPMNT_NO,RPR_ESTMT_ID,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,ESTMT_NO,RPR_APPRVL_DT,RPR_CMPLTN_DT,MTRL_CST_NC,LBR_CST_NC,RPR_TX_RT_NC,RPR_TX_AMNT_NC,TTL_CSTS_NC,RPR_APPRVL_AMNT_NC,BLLNG_FLG,YRD_LCTN,DPT_ID,DPT_CD,DPT_NAM,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_CD,GTN_DT,CLN_CST_NC,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,ACTV_BT,TTL_SRV_TAX,TTL_EST_INCL_SRV_TAX,INVC_TYPE,'RPC' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,RPR_CMPLTN_DT AS ACTIVITY_DATE,LEAK_TEST,PTI,SURVEY,SRVC_PRTNR_ID,TOTALAMOUNT,APPRVL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,ADDTNL_CLNNG_CHRG_NC,APPRVL_RF_NO FROM V_REPAIR_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND SRVC_PRTNR_ID=@SRVC_PRTNR_ID"
    Private Const HEATING_CHARGESelectQueryByDepotIDInvoiceNo As String = "SELECT HTNG_ID,HTNG_CD,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,HTNG_STRT_DT,HTNG_STRT_TM,HTNG_END_DT,HTNG_END_TM,HTNG_TMPRTR,TTL_HTN_PRD,MIN_HTNG_RT_NC,HRLY_CHRG_NC,TTL_RT_NC,BLLNG_FLG,GI_TRNSCTN_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,PRDCT_CD,PRDCT_DSCRPTN_VC,GTN_DT,SRVC_PRTNR_ID,HTNG_STRT_DT_TM,HTNG_END_DT_TM,DRFT_INVC_NO,FNL_INVC_NO FROM V_HEATING_CHARGE WHERE DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO  AND SRVC_PRTNR_ID=@SRVC_PRTNR_ID"
    Private Const MISCELLANEOUS_INVOICESelectQueryByDepotIDInvoiceNo As String = "SELECT MSCLLNS_INVC_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,INVCNG_TO_ID,INVCNG_TO_CD,INVCNG_TO_NAM,SRVC_PRTNR_TYP_CD,ACTVTY_DT,CHRG_DSCRPTN,AMNT_NC,BLLNG_FLG,DPT_ID,DPT_CD,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DRFT_INVC_NO,FNL_INVC_NO,MIS_TYP,MIS_CTGRY FROM V_MISCELLANEOUS_INVOICE WHERE DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND INVCNG_TO_ID=@INVCNG_TO_ID"
    Private Const HANDLING_CHARGESelectQueryByDepotIDInvoiceNo As String = "SELECT HNDLNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT, TO_BLLNG_DT,GTN_DT,FR_DYS,NO_OF_DYS,HNDLNG_CST_NC,HNDLNG_TX_RT_NC,TTL_CSTS_NC,BLLNG_FLG,ACTV_BT,DPT_ID,IS_GT_OT_FLG,YRD_LCTN,CSTMR_ID,CSTMR_CD,CSTMR_NAM,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) AS [STOARGE_DAYS],DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) - FR_DYS AS [CHARGEABLE_DAYS],(CASE CST_TYP WHEN 'HNDIN' THEN 'IN' ELSE 'OUT' END) AS ACTIVITY,CSTMR_NAM AS INVOICING_PARTY,FRM_BLLNG_DT AS ACTIVITY_DATE,DRFT_INVC_NO,FNL_INVC_NO FROM V_HANDLING_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND CSTMR_ID=@CSTMR_ID"
    Private Const STORAGE_CHARGESelectQueryByDepotIDInvoiceNo As String = "SELECT STRG_CHRG_ID,STRG_CHRG_ID AS CSTMR_CHRG_DTL_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,(CASE WHEN TO_BLLNG_DT IS NOT NULL THEN TO_BLLNG_DT ELSE NULL END)GTOT_DT,GTN_DT,FR_DYS,NO_OF_DYS,STRG_CST_NC,STRG_TX_RT_NC,TTL_CSTS_NC,STRG_CNTN_FLG,BLLNG_FLG,IS_GT_OT_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,CSTMR_ID,CSTMR_CD,DPT_ID,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,PRDCT_DSCRPTN_VC,DRFT_INVC_NO,FNL_INVC_NO,CSTMR_CHNG_BT FROM V_STORAGE_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND CSTMR_ID=@CSTMR_ID"
    Private Const TRANSPORTATION_INVOICESelectQueryByDepotIDInvoiceNo As String = "SELECT TRNSPRTTN_CHRG_ID,INVC_DT,CSTMR_ID,CSTMR_CD,CSTMR_NAM,TRNSPRTTN_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_STT_ID,EQPMNT_STT_CD,RQST_NO,CSTMR_RF_NO,RT_ID,RT_CD,RT_DSCRPTN_VC,EVNT_DT,TRP_RT_NC,WGHTMNT_AMNT_NC,TKN_AMNT_NC,RCHRGBL_CST_AMNT_NC,FNNC_CHRG_AMNT_NC,DTNTN_AMNT_NC,OTHR_CHRG_AMNT_NC,TTL_RT_NC,DPT_AMNT,CSTMR_AMNT,DPT_CRRNCY_ID,CSTMR_CRRNCY_ID,DPT_CRRNCY_CD,CSTMR_CRRNCY_CD,EXCHNG_RT_PR_UNT_NC,FNL_INVC_NO,DRFT_INVC_NO,BLLNG_FLG,DPT_ID,JB_STRT_DT,JB_END_DT,NO_OF_TRIPS,EMPTY_SNGL_ID,EMPTY_SNGL_CD FROM V_TRANSPORTATION_INVOICE WHERE DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND CSTMR_ID=@CSTMR_ID"
    Private Const RENTAL_CHARGESelectQueryByDepotIDInvoiceNo As String = "SELECT RNTL_CHRG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RNTL_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,HNDLNG_OT_NC,HNDLNG_IN_NC,ON_HR_SRVY_NC,OFF_HR_SRVY_NC,FR_DYS,NO_OF_DYS,RNTL_PR_DY_NC,RNTL_TX_RT_NC,TTL_CSTS_NC,RNTL_CNTN_FLG,BLLNG_FLG,IS_GT_IN_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,BLLNG_TYP_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,DPT_ID,DPT_CD,DPT_NAM,GI_TRNSCTN_NO,RNTL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,OTHR_CHRG_NC FROM V_RENTAL_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND CSTMR_ID=@CSTMR_ID"
    ''insert into fact table for xml generation
    Private Const INVOICE_EDI_HISTORY_DETAILInsertQuery As String = "INSERT INTO INVOICE_EDI_HISTORY_DETAIL(INVC_EDI_HSTRY_DTL_ID,INVC_EDI_HSTRY_ID,EQPMNT_NO,INVC_NO,LN_RSPNS_VC,SPPRT_URL)VALUES(@INVC_EDI_HSTRY_DTL_ID,@INVC_EDI_HSTRY_ID,@EQPMNT_NO,@INVC_NO,@LN_RSPNS_VC,@SPPRT_URL)"
    Private Const INVOICE_EDI_HISTORYInsertQuery As String = "INSERT INTO INVOICE_EDI_HISTORY(INVC_EDI_HSTRY_ID,CSTMR_ID,CSTMR_CD,ACTVTY_NAM,INVC_NO,SNT_FL_NAM,SNT_DT,RCVD_FL_NAM,GNRTD_DT,STTS,RMRKS_VC,RSND_BT,GNRTD_BY,DPT_ID,INVC_FL_NAM)VALUES(@INVC_EDI_HSTRY_ID,@CSTMR_ID,@CSTMR_CD,@ACTVTY_NAM,@INVC_NO,@SNT_FL_NAM,@SNT_DT,@RCVD_FL_NAM,@GNRTD_DT,@STTS,@RMRKS_VC,@RSND_BT,@GNRTD_BY,@DPT_ID,@INVC_FL_NAM)"
    Private Const Inspection_SelectQueryByInvoicingPartyID As String = "SELECT INSPCTN_CHRG_ID,EQPMNT_NO,CSTMR_AGNT_ID,INSPCTD_DT,INSPCTN_CHRG,BLLNG_FLG,INSPCTD_BY,ACTV_BT,DPT_ID,GI_TRNSCTN_NO, DRFT_INVC_NO,FNL_INVC_NO,'INS' AS ACTIVITY,EQPMNT_TYP_ID,EQPMNT_TYP_CD,GTN_DT,EIR_NO,BLL_CD,(CASE WHEN BLL_CD = 'CUSTOMER' THEN (SELECT CSTMR_CD FROM CUSTOMER WHERE CSTMR_ID= IC.CSTMR_AGNT_ID) ELSE (SELECT AGNT_CD  FROM AGENT  WHERE AGNT_ID = IC.CSTMR_AGNT_ID) END)INVOICING_PARTY,INSPCTD_DT  AS ACTIVITY_DATE,EQPMNT_TYP_ID,DRFT_INVC_NO,FNL_INVC_NO FROM V_INSPECTION_CHARGES IC WHERE CSTMR_AGNT_ID=@CSTMR_AGNT_ID AND ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO"

    'For GWS
    Private Const GetGWSHANDLINGCHARGEByByDepotIDInvoiceNo_SelectQry As String = "SELECT HNDLNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT, TO_BLLNG_DT,GTN_DT,FR_DYS,NO_OF_DYS,HNDLNG_CST_NC,HNDLNG_TX_RT_NC,TTL_CSTS_NC,BLLNG_FLG,ACTV_BT,DPT_ID,IS_GT_OT_FLG,YRD_LCTN,CSTMR_ID,CSTMR_CD,CSTMR_NAM,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) AS [STOARGE_DAYS],DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) - FR_DYS AS [CHARGEABLE_DAYS],(CASE CST_TYP WHEN 'HNDIN' THEN 'IN' ELSE 'OUT' END) AS ACTIVITY,CSTMR_NAM AS INVOICING_PARTY,FRM_BLLNG_DT AS ACTIVITY_DATE,DRFT_INVC_NO,FNL_INVC_NO,AGNT_ID FROM V_HANDLING_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND AGNT_ID=@AGNT_ID"
    Private Const GetHANDLINGCHARGEByByDepotIDInvoiceNoByCustomer_Qry As String = "SELECT HNDLNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT, TO_BLLNG_DT,GTN_DT,FR_DYS,NO_OF_DYS,HNDLNG_CST_NC,HNDLNG_TX_RT_NC,TTL_CSTS_NC,BLLNG_FLG,ACTV_BT,DPT_ID,IS_GT_OT_FLG,YRD_LCTN,CSTMR_ID,CSTMR_CD,CSTMR_NAM,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) AS [STOARGE_DAYS],DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) - FR_DYS AS [CHARGEABLE_DAYS],(CASE CST_TYP WHEN 'HNDIN' THEN 'IN' ELSE 'OUT' END) AS ACTIVITY,CSTMR_NAM AS INVOICING_PARTY,FRM_BLLNG_DT AS ACTIVITY_DATE,DRFT_INVC_NO,FNL_INVC_NO,AGNT_ID FROM V_HANDLING_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND CSTMR_ID=@CSTMR_ID and AGNT_ID is null"
    Private Const GetGWSHANDLINGCHARGEByByDepotIDInvoiceNoByAgent_Qry As String = "SELECT HNDLNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT, TO_BLLNG_DT,GTN_DT,FR_DYS,NO_OF_DYS,HNDLNG_CST_NC,HNDLNG_TX_RT_NC,TTL_CSTS_NC,BLLNG_FLG,ACTV_BT,DPT_ID,IS_GT_OT_FLG,YRD_LCTN,CSTMR_ID,CSTMR_CD,CSTMR_NAM,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) AS [STOARGE_DAYS],DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) - FR_DYS AS [CHARGEABLE_DAYS],(CASE CST_TYP WHEN 'HNDIN' THEN 'IN' ELSE 'OUT' END) AS ACTIVITY,CSTMR_NAM AS INVOICING_PARTY,FRM_BLLNG_DT AS ACTIVITY_DATE,DRFT_INVC_NO,FNL_INVC_NO,AGNT_ID FROM V_HANDLING_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND AGNT_ID=@AGNT_ID"
    'Invoice Cancel
    Private Const GetViewInvoiceRoleRights_SelectQry As String = "SELECT RL_RGHT_ID FROM V_USER_RIGHTS WHERE USR_NAM =@USR_NAM AND ACTVTY_ID=@ACTVTY_ID AND CNCL_BT =1"
    Private Const GetHANDLINGCHARGEByByDepotIDInvoiceNoCancel_Qry As String = "SELECT HNDLNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT, TO_BLLNG_DT,GTN_DT,FR_DYS,NO_OF_DYS,HNDLNG_CST_NC,HNDLNG_TX_RT_NC,TTL_CSTS_NC,BLLNG_FLG,ACTV_BT,DPT_ID,IS_GT_OT_FLG,YRD_LCTN,CSTMR_ID,CSTMR_CD,CSTMR_NAM,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) AS [STOARGE_DAYS],DATEDIFF(DAY, GTN_DT,TO_BLLNG_DT) - FR_DYS AS [CHARGEABLE_DAYS],(CASE CST_TYP WHEN 'HNDIN' THEN 'IN' ELSE 'OUT' END) AS ACTIVITY,CSTMR_NAM AS INVOICING_PARTY,FRM_BLLNG_DT AS ACTIVITY_DATE,DRFT_INVC_NO,FNL_INVC_NO FROM V_HANDLING_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND FNL_INVC_NO=@FNL_INVC_NO AND CSTMR_ID=@CSTMR_ID"
    Private Const GetSTORAGECHARGEByByDepotIDInvoiceNoCancel_Qry As String = "SELECT STRG_CHRG_ID,STRG_CHRG_ID AS CSTMR_CHRG_DTL_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,(CASE WHEN TO_BLLNG_DT IS NOT NULL THEN TO_BLLNG_DT ELSE NULL END)GTOT_DT,GTN_DT,FR_DYS,NO_OF_DYS,STRG_CST_NC,STRG_TX_RT_NC,TTL_CSTS_NC,STRG_CNTN_FLG,BLLNG_FLG,IS_GT_OT_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,CSTMR_ID,CSTMR_CD,DPT_ID,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,PRDCT_DSCRPTN_VC,DRFT_INVC_NO,FNL_INVC_NO,CSTMR_CHNG_BT FROM V_STORAGE_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND FNL_INVC_NO=@FNL_INVC_NO AND CSTMR_ID=@CSTMR_ID"
    Private Const GetHEATINGCHARGEByByDepotIDInvoiceNoCancel_Qry As String = "SELECT HTNG_ID,HTNG_CD,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,HTNG_STRT_DT,HTNG_STRT_TM,HTNG_END_DT,HTNG_END_TM,HTNG_TMPRTR,TTL_HTN_PRD,MIN_HTNG_RT_NC,HRLY_CHRG_NC,TTL_RT_NC,BLLNG_FLG,GI_TRNSCTN_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,PRDCT_CD,PRDCT_DSCRPTN_VC,GTN_DT,SRVC_PRTNR_ID,HTNG_STRT_DT_TM,HTNG_END_DT_TM,DRFT_INVC_NO,FNL_INVC_NO FROM V_HEATING_CHARGE WHERE DPT_ID=@DPT_ID AND FNL_INVC_NO=@FNL_INVC_NO  AND SRVC_PRTNR_ID=@SRVC_PRTNR_ID"
    Private Const GetCLEANINGCHARGEByByDepotIDInvoiceNoCancel_Qry As String = "SELECT CLNNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_CD,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,BLLNG_FLG,CLNNG_ID,DPT_ID,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_DSCRPTN_VC,GTN_DT,ACTV_BT,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RT,CLNNG_CERT_NO,SRVC_PRTNR_ID,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,'CLN' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,ORGNL_CLNNG_DT AS ACTIVITY_DATE,RFRNC_NO,PRDCT_CD,EQPMNT_CD_ID,EQPMNT_TYP_ID,DRFT_INVC_NO,FNL_INVC_NO FROM V_CLEANING_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND FNL_INVC_NO=@FNL_INVC_NO AND SRVC_PRTNR_ID=@SRVC_PRTNR_ID"
    Private Const GetREPAIRCHARGEByByDepotIDInvoiceNoCancel_Qry As String = "SELECT RPR_CHRG_ID,EQPMNT_NO,RPR_ESTMT_ID,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,ESTMT_NO,RPR_APPRVL_DT,RPR_CMPLTN_DT,MTRL_CST_NC,LBR_CST_NC,RPR_TX_RT_NC,RPR_TX_AMNT_NC,TTL_CSTS_NC,RPR_APPRVL_AMNT_NC,BLLNG_FLG,YRD_LCTN,DPT_ID,DPT_CD,DPT_NAM,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_CD,GTN_DT,CLN_CST_NC,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,ACTV_BT,TTL_SRV_TAX,TTL_EST_INCL_SRV_TAX,INVC_TYPE,'RPC' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,RPR_CMPLTN_DT AS ACTIVITY_DATE,LEAK_TEST,PTI,SURVEY,SRVC_PRTNR_ID,TOTALAMOUNT,APPRVL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,ADDTNL_CLNNG_CHRG_NC,APPRVL_RF_NO FROM V_REPAIR_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND FNL_INVC_NO=@FNL_INVC_NO AND SRVC_PRTNR_ID=@SRVC_PRTNR_ID"
    Private Const GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNoCancel_Qry As String = "SELECT MSCLLNS_INVC_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,INVCNG_TO_ID,INVCNG_TO_CD,INVCNG_TO_NAM,SRVC_PRTNR_TYP_CD,ACTVTY_DT,CHRG_DSCRPTN,AMNT_NC,BLLNG_FLG,DPT_ID,DPT_CD,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DRFT_INVC_NO,FNL_INVC_NO,MIS_TYP,MIS_CTGRY FROM V_MISCELLANEOUS_INVOICE WHERE DPT_ID=@DPT_ID AND FNL_INVC_NO=@FNL_INVC_NO AND INVCNG_TO_ID=@INVCNG_TO_ID"
    Private Const GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNoCancel_Qry As String = "SELECT TRNSPRTTN_CHRG_ID,INVC_DT,CSTMR_ID,CSTMR_CD,CSTMR_NAM,TRNSPRTTN_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_STT_ID,EQPMNT_STT_CD,RQST_NO,CSTMR_RF_NO,RT_ID,RT_CD,RT_DSCRPTN_VC,EVNT_DT,TRP_RT_NC,WGHTMNT_AMNT_NC,TKN_AMNT_NC,RCHRGBL_CST_AMNT_NC,FNNC_CHRG_AMNT_NC,DTNTN_AMNT_NC,OTHR_CHRG_AMNT_NC,TTL_RT_NC,DPT_AMNT,CSTMR_AMNT,DPT_CRRNCY_ID,CSTMR_CRRNCY_ID,DPT_CRRNCY_CD,CSTMR_CRRNCY_CD,EXCHNG_RT_PR_UNT_NC,FNL_INVC_NO,DRFT_INVC_NO,BLLNG_FLG,DPT_ID,JB_STRT_DT,JB_END_DT,NO_OF_TRIPS,EMPTY_SNGL_ID,EMPTY_SNGL_CD FROM V_TRANSPORTATION_CHARGE WHERE DPT_ID=@DPT_ID AND FNL_INVC_NO=@FNL_INVC_NO AND CSTMR_ID=@CSTMR_ID"
    Private Const GetRentalChargeByByDepotIDInvoiceNoCancel_Qry As String = "SELECT RNTL_CHRG_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RNTL_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,HNDLNG_OT_NC,HNDLNG_IN_NC,ON_HR_SRVY_NC,OFF_HR_SRVY_NC,FR_DYS,NO_OF_DYS,RNTL_PR_DY_NC,RNTL_TX_RT_NC,TTL_CSTS_NC,RNTL_CNTN_FLG,BLLNG_FLG,IS_GT_IN_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,BLLNG_TYP_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,DPT_ID,DPT_CD,DPT_NAM,GI_TRNSCTN_NO,RNTL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,OTHR_CHRG_NC FROM V_RENTAL_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND FNL_INVC_NO=@FNL_INVC_NO AND CSTMR_ID=@CSTMR_ID"

    'Invoice Cancel Query's
    Private Const Cancel_CleaningInvoice_Update_Query As String = "UPDATE CLEANING_CHARGE SET BLLNG_FLG='D', FNL_INVC_NO =NULL  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Get_CleaningInvoice_DraftNo_Qry As String = "SELECT DRFT_INVC_NO FROM CLEANING_CHARGE  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const CreateCreditNote_InvoiceHistory_InsertQuery As String = "INSERT INTO INVOICE_HISTORY(INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_CRRNCY_ID,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,INVCNG_PRTY_ID,DPT_ID,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,FL_NM,NO_OF_EQPMNT,INVC_CNCL)VALUES(@INVC_BIN,@INVC_NO,@INVC_DT,@INVC_FL_PTH,@INVC_TYP,@INVC_CRRNCY_ID,@EXCHNG_RT_NC,@CSTMR_CRRNCY_ID,@BLLNG_TYP_ID,@FRM_BLLNG_DT,@TO_BLLNG_DT,@TTL_CST_IN_CSTMR_CRRNCY_NC,@TTL_CST_IN_INVC_CRRNCY_NC,@BLLNG_FLG,@CSTMR_ID,@INVCNG_PRTY_ID,@DPT_ID,@ACTV_BT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@FL_NM,@NO_OF_EQPMNT,@INVC_CNCL)"
    Private Const Get_RepairInvoice_DraftNo_DraftNo_Qry As String = "SELECT DRFT_INVC_NO FROM REPAIR_CHARGE  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Cancel_RepairInvoice_Update_Query As String = "UPDATE REPAIR_CHARGE SET BLLNG_FLG='D', FNL_INVC_NO =NULL  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Get_TransPortationInvoice_DraftNo_Qry As String = "SELECT DRFT_INVC_NO FROM TRANSPORTATION_CHARGE  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Cancel_TransportationInvoice_Update_Query As String = "UPDATE TRANSPORTATION_CHARGE SET BLLNG_FLG='D', FNL_INVC_NO =NULL  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Get_RentalInvoice_DraftNo_Qry As String = "SELECT DRFT_INVC_NO FROM RENTAL_CHARGE  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Cancel_RentalInvoice_Update_Query As String = "UPDATE RENTAL_CHARGE SET BLLNG_FLG='D', FNL_INVC_NO =NULL,BLLNG_TLL_DT=NULL,NO_OF_DYS=NULL,ON_HR_BLLNG_FLG='N',OFF_HR_BLLNG_FLG='N',TTL_CSTS_NC=0  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Get_HandlingInvoice_DraftNo_DraftNo_Qry As String = "SELECT DRFT_INVC_NO FROM HANDLING_CHARGE  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Cancel_HandlingInvoice_Update_Query As String = "UPDATE HANDLING_CHARGE SET BLLNG_FLG='D', FNL_INVC_NO =NULL  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Get_StorageInvoice_DraftNo_DraftNo_Qry As String = "SELECT DRFT_INVC_NO FROM STORAGE_CHARGE  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Cancel_StorageInvoice_Update_Query As String = "UPDATE STORAGE_CHARGE SET BLLNG_FLG='D',NO_OF_DYS=NULL,TTL_CSTS_NC=0,BLLNG_TLL_DT=NULL, FNL_INVC_NO =NULL,TO_BLLNG_DT=NULL  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Get_HeatingInvoice_DraftNo_Qry As String = "SELECT DRFT_INVC_NO FROM HEATING_CHARGE  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Cancel_HeatingInvoice_Update_Query As String = "UPDATE HEATING_CHARGE SET BLLNG_FLG='D', FNL_INVC_NO =NULL  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Get_MISCELLANEOUSInvoice_DraftNo_Qry As String = "SELECT DRFT_INVC_NO FROM MISCELLANEOUS_INVOICE  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"
    Private Const Cancel_MISCELLANEOUSInvoice_Update_Query As String = "UPDATE MISCELLANEOUS_INVOICE SET BLLNG_FLG='D', FNL_INVC_NO =NULL  WHERE FNL_INVC_NO=@FNL_INVC_NO AND DPT_ID=@DPT_ID"


    Private Const DeActive_FinalInvoice_Update_Query As String = "UPDATE INVOICE_HISTORY SET ACTV_BT=0 WHERE INVC_NO =@INVC_NO AND DPT_ID =@DPT_ID"
    Private Const Active_DraftInvoice_Update_Query As String = "UPDATE INVOICE_HISTORY SET ACTV_BT=1, INVC_CNCL ='CREDITNOTE' WHERE INVC_NO =@INVC_NO AND DPT_ID =@DPT_ID"
    Private Const Delete_InvoiceHistoryDetails_Query As String = "DELETE FROM INVOICE_HISTORY_DETAIL WHERE INVC_NO=@INVC_NO"
    Private Const Get_BilledTransportationDetail_Select_Qry As String = "SELECT TRNSPRTTN_DTL_ID,TRNSPRTTN_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CSTMR_RF_NO,TTL_RT_NC,JB_STRT_DT,JB_END_DT,RMRKS_VC,BLLNG_FLG,EMPTY_SNGL_ID,EMPTY_SNGL_CD FROM V_TRANSPORTATION_DETAIL WHERE BLLNG_FLG='Y' AND TRNSPRTTN_ID=@TRNSPRTTN_ID"
    'For GWS
    Private Const Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByCustomer_Qry As String = "SELECT STRG_CHRG_ID,STRG_CHRG_ID AS CSTMR_CHRG_DTL_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,(CASE WHEN TO_BLLNG_DT IS NOT NULL THEN TO_BLLNG_DT ELSE NULL END)GTOT_DT,GTN_DT,FR_DYS,NO_OF_DYS,STRG_CST_NC,STRG_TX_RT_NC,TTL_CSTS_NC,STRG_CNTN_FLG,BLLNG_FLG,IS_GT_OT_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,CSTMR_ID,CSTMR_CD,DPT_ID,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,PRDCT_DSCRPTN_VC,DRFT_INVC_NO,FNL_INVC_NO,CSTMR_CHNG_BT FROM V_STORAGE_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND CSTMR_ID=@CSTMR_ID AND AGNT_ID IS NULL"
    Private Const Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByAgent_Qry As String = "SELECT STRG_CHRG_ID,STRG_CHRG_ID AS CSTMR_CHRG_DTL_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,(CASE WHEN TO_BLLNG_DT IS NOT NULL THEN TO_BLLNG_DT ELSE NULL END)GTOT_DT,GTN_DT,FR_DYS,NO_OF_DYS,STRG_CST_NC,STRG_TX_RT_NC,TTL_CSTS_NC,STRG_CNTN_FLG,BLLNG_FLG,IS_GT_OT_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,CSTMR_ID,CSTMR_CD,DPT_ID,HTNG_BT,GI_TRNSCTN_NO,PRDCT_CD,PRDCT_DSCRPTN_VC,DRFT_INVC_NO,FNL_INVC_NO,CSTMR_CHNG_BT FROM V_STORAGE_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND AGNT_ID=@AGNT_ID"
    Private Const Get_GWS_REPAIRCHARGEByByAgentIdInvoiceNo_SelectQry As String = "SELECT RPR_CHRG_ID,EQPMNT_NO,RPR_ESTMT_ID,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,ESTMT_NO,RPR_APPRVL_DT,RPR_CMPLTN_DT,MTRL_CST_NC,LBR_CST_NC,RPR_TX_RT_NC,RPR_TX_AMNT_NC,TTL_CSTS_NC,RPR_APPRVL_AMNT_NC,BLLNG_FLG,YRD_LCTN,DPT_ID,DPT_CD,DPT_NAM,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_CD,GTN_DT,CLN_CST_NC,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,ACTV_BT,TTL_SRV_TAX,TTL_EST_INCL_SRV_TAX,INVC_TYPE,'RPC' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,RPR_CMPLTN_DT AS ACTIVITY_DATE,LEAK_TEST,PTI,SURVEY,SRVC_PRTNR_ID,TOTALAMOUNT,APPRVL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,ADDTNL_CLNNG_CHRG_NC,APPRVL_RF_NO,EQPMNT_CD_CD FROM V_REPAIR_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND AGNT_ID=@AGNT_ID"
    Private Const Get_GWS_REPAIRCHARGEByByCustomerIDInvoiceNo_SelectQry As String = "SELECT RPR_CHRG_ID,EQPMNT_NO,RPR_ESTMT_ID,EQPMNT_TYP_ID,EQPMNT_TYP_CD,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,ESTMT_NO,RPR_APPRVL_DT,RPR_CMPLTN_DT,MTRL_CST_NC,LBR_CST_NC,RPR_TX_RT_NC,RPR_TX_AMNT_NC,TTL_CSTS_NC,RPR_APPRVL_AMNT_NC,BLLNG_FLG,YRD_LCTN,DPT_ID,DPT_CD,DPT_NAM,CSTMR_ID,GI_TRNSCTN_NO,CSTMR_CD,CSTMR_NAM,PRDCT_CD,GTN_DT,CLN_CST_NC,INVCNG_PRTY_ID,INVCNG_PRTY_CD,INVCNG_PRTY_NM,ACTV_BT,TTL_SRV_TAX,TTL_EST_INCL_SRV_TAX,INVC_TYPE,'RPC' AS ACTIVITY,(CASE WHEN INVCNG_PRTY_ID IS NULL THEN CSTMR_NAM ELSE INVCNG_PRTY_NM END)INVOICING_PARTY,RPR_CMPLTN_DT AS ACTIVITY_DATE,LEAK_TEST,PTI,SURVEY,SRVC_PRTNR_ID,TOTALAMOUNT,APPRVL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,ADDTNL_CLNNG_CHRG_NC,APPRVL_RF_NO,EQPMNT_CD_CD FROM V_REPAIR_CHARGE WHERE ACTV_BT=1 AND DPT_ID=@DPT_ID AND DRFT_INVC_NO=@DRFT_INVC_NO AND SRVC_PRTNR_ID=@SRVC_PRTNR_ID and AGNT_ID is null"

    'For MultiLocation
    Private Const V_INVOICE_HISTORYSelectQueryForAllDepot As String = "SELECT INVC_BIN,INVC_NO,INVC_DT,INVC_FL_PTH,INVC_TYP,INVC_NM,INVC_CRRNCY_ID,INVC_CRRNCY_CD,EXCHNG_RT_NC,CSTMR_CRRNCY_ID,CSTMR_CRRNCY_CD,BLLNG_TYP_ID,FRM_BLLNG_DT,TO_BLLNG_DT,TTL_CST_IN_CSTMR_CRRNCY_NC,TTL_CST_IN_INVC_CRRNCY_NC,BLLNG_FLG,CSTMR_ID,INVCNG_PRTY_ID,DPT_ID,DPT_CD,DPT_NAM,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,SRVC_PRTNR_ID,SRVC_PRTNR_CD,SRVC_PRTNR_NM,FL_NM,XML_BT,CSTMR_CD,CUSTOMERCRRNCY,INVC_CNCL,VIEW_BT,(SELECT CRRNCY_ID FROM V_BANK_DETAIL WHERE DPT_ID =H.DPT_ID AND BNK_TYP_ID=44 )DPT_CRRNCY_ID FROM V_INVOICE_HISTORY H WHERE ACTV_BT=1 ORDER BY INVC_DT DESC"
    Private ds As ViewInvoiceDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New ViewInvoiceDataSet
    End Sub

#End Region

#Region "GetVInvoiceHistoryByByDepotID() TABLE NAME:V_INVOICE_HISTORY"

    Public Function GetVInvoiceHistoryByByDepotID(ByVal bv_i32DepotID As Int64) As ViewInvoiceDataSet
        Try
            objData = New DataObjects(V_INVOICE_HISTORYSelectQueryByDepotID, ViewInvoiceData.DPT_ID, CStr(bv_i32DepotID))
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_INVOICE_HISTORY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetVInvoiceHistoryForAllDepot() TABLE NAME:V_INVOICE_HISTORY"
    ''' <summary>
    ''' This method is used for returning Invoice detail for all Depots - Multi location 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVInvoiceHistoryForAllDepot() As ViewInvoiceDataSet
        Try
            objData = New DataObjects(V_INVOICE_HISTORYSelectQueryForAllDepot)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_INVOICE_HISTORY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCLEANINGCHARGEByByDepotIDInvoiceNo() TABLE NAME:CLEANING_CHARGE"

    Public Function GetCLEANINGCHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                          ByVal bv_i64ServicePartnerID As Int64, _
                                                          ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.SRVC_PRTNR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(CLEANING_CHARGESelectQueryByDepotIDInvoiceNo, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_CLEANING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetHEATINGCHARGEByByDepotIDInvoiceNo() TABLE NAME:HEATING_CHARGE"

    Public Function GetHEATINGCHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_i64ServicePartnerID As Int64, _
                                                         ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.SRVC_PRTNR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(HEATING_CHARGESelectQueryByDepotIDInvoiceNo, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_HEATING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNo() TABLE NAME:MISCELLANEOUS_INVOICE"

    Public Function GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                                ByVal bv_i64ServicePartnerID As Int64, _
                                                                ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.INVCNG_TO_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(MISCELLANEOUS_INVOICESelectQueryByDepotIDInvoiceNo, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_MISCELLANEOUS_INVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetHANDLINGCHARGEByByDepotIDInvoiceNo() TABLE NAME:HANDLING_CHARGE"

    Public Function GetHANDLINGCHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                          ByVal bv_i64ServicePartnerID As Int64, _
                                                          ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(HANDLING_CHARGESelectQueryByDepotIDInvoiceNo, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_HANDLING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetHANDLINGCHARGEByByDepotIDInvoiceNoByCustomer(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_i64ServicePartnerID As Int64, _
                                                         ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(GetHANDLINGCHARGEByByDepotIDInvoiceNoByCustomer_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_HANDLING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetGWSHANDLINGCHARGEByByDepotIDInvoiceNoByAgent(ByVal bv_i32DepotID As Int64, _
                                                          ByVal bv_i64ServicePartnerID As Int64, _
                                                          ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(GateinData.AGNT_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(GetGWSHANDLINGCHARGEByByDepotIDInvoiceNoByAgent_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_HANDLING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetREPAIRCHARGEByByDepotIDInvoiceNo() TABLE NAME:REPAIR_CHARGE"
    Public Function GetREPAIRCHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                        ByVal bv_i64ServicePartnerID As Int64, _
                                                        ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.SRVC_PRTNR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(REPAIR_CHARGESelectQueryByDepotIDInvoiceNo, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_REPAIR_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_GWS_REPAIRCHARGEByByCustomerIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                       ByVal bv_i64ServicePartnerID As Int64, _
                                                       ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.SRVC_PRTNR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(Get_GWS_REPAIRCHARGEByByCustomerIDInvoiceNo_SelectQry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_REPAIR_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Get_GWS_REPAIRCHARGEByByAgentIdInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                       ByVal bv_i64AgentID As Int64, _
                                                       ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(GateinData.AGNT_ID, bv_i64AgentID)
            objData = New DataObjects(Get_GWS_REPAIRCHARGEByByAgentIdInvoiceNo_SelectQry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_REPAIR_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "GetSTORAGECHARGEByByDepotIDInvoiceNo() TABLE NAME:STORAGE_CHARGE"
    Public Function GetSTORAGECHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_i64ServicePartnerID As Int64, _
                                                         ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(STORAGE_CHARGESelectQueryByDepotIDInvoiceNo, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_STORAGE_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByCustomer(ByVal bv_i32DepotID As Int64, _
                                                        ByVal bv_i64ServicePartnerID As Int64, _
                                                        ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(GateinData.CSTMR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByCustomer_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_STORAGE_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByAgent(ByVal bv_i32DepotID As Int64, _
                                                        ByVal bv_i64ServicePartnerID As Int64, _
                                                        ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(GateinData.AGNT_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(Get_GWSSTORAGECHARGEByByDepotIDInvoiceNoByAgent_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_STORAGE_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function



#End Region

#Region "GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNo() TABLE NAME:V_TRANSPORTATION_INVOICE"
    Public Function GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                                 ByVal bv_i64ServicePartnerID As Int64, _
                                                                 ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(TRANSPORTATION_INVOICESelectQueryByDepotIDInvoiceNo, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_TRANSPORTATION_INVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRentalChargeByByDepotIDInvoiceNo() TABLE NAME:RENTAL_CHARGE (View: V_Rental_CHarge)"
    Public Function GetRentalChargeByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                        ByVal bv_i64ServicePartnerID As Int64, _
                                                        ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(RENTAL_CHARGESelectQueryByDepotIDInvoiceNo, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_RENTAL_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Insert Into Fact Table"
#Region "CreateXMLEDI() TABLE NAME:INVOICE_EDI_HISTORY"
    Public Function CreateXMLEDI(ByVal bv_i64CustomerId As Int64, _
                                   ByVal bv_strCustomercd As String, _
                                   ByRef br_strActivityName As String, _
                                   ByVal bv_strInvoiceno As String, _
                                   ByVal bv_strSentfilename As String, _
                                   ByVal bv_datSentDate As DateTime, _
                                   ByVal bv_strReceivedFilename As String, _
                                   ByVal bv_datGeneratedDate As DateTime, _
                                   ByVal bv_strStatus As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_blnResendbit As Boolean, _
                                   ByVal bv_strGeneratedby As String, _
                                   ByVal bv_intDepotId As Integer, _
                                   ByVal bv_strInvoiceUrl As String, _
                                   ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ViewInvoiceData._INVOICE_EDI_HISTORY).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ViewInvoiceData._INVOICE_EDI_HISTORY, br_objTrans)

                .Item(ViewInvoiceData.INVC_EDI_HSTRY_ID) = intMax

                .Item(ViewInvoiceData.CSTMR_ID) = bv_i64CustomerId

                If bv_strCustomercd <> Nothing Then
                    .Item(ViewInvoiceData.CSTMR_CD) = bv_strCustomercd
                Else
                    .Item(ViewInvoiceData.CSTMR_CD) = DBNull.Value
                End If
                .Item(ViewInvoiceData.ACTVTY_NAM) = br_strActivityName

                .Item(ViewInvoiceData.INVC_NO) = bv_strInvoiceno
                .Item(ViewInvoiceData.SNT_FL_NAM) = bv_strSentfilename
                If bv_datSentDate <> Nothing Then
                    .Item(ViewInvoiceData.SNT_DT) = bv_datSentDate
                Else
                    .Item(ViewInvoiceData.SNT_DT) = DBNull.Value
                End If
                If bv_strReceivedFilename <> Nothing Then
                    .Item(ViewInvoiceData.RCVD_FL_NAM) = bv_strReceivedFilename
                Else
                    .Item(ViewInvoiceData.RCVD_FL_NAM) = DBNull.Value
                End If
                .Item(ViewInvoiceData.GNRTD_DT) = bv_datGeneratedDate
                .Item(ViewInvoiceData.STTS) = bv_strStatus
                .Item(ViewInvoiceData.RMRKS_VC) = bv_strRemarks
                .Item(ViewInvoiceData.RSND_BT) = bv_blnResendbit
                .Item(ViewInvoiceData.GNRTD_BY) = bv_strGeneratedby
                .Item(ViewInvoiceData.DPT_ID) = bv_intDepotId
                .Item(ViewInvoiceData.INVC_FL_NAM) = bv_strInvoiceUrl
            End With
            objData.InsertRow(dr, INVOICE_EDI_HISTORYInsertQuery, br_objTrans)
            dr = Nothing
            CreateXMLEDI = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateXMLEDIDetail() TABLE NAME:INVOICE_EDI_HISTORY_DETAIL"
    Public Function CreateXMLEDIDetail(ByVal bv_i64InvoiceEdiId As Int64, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByVal bv_strInvoiceno As String, _
                                   ByVal bv_strLineResponse As String, _
                                   ByVal bv_strSupportUrl As String, _
                                   ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(ViewInvoiceData._INVOICE_EDI_HISTORY_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(ViewInvoiceData._INVOICE_EDI_HISTORY_DETAIL, br_objTrans)

                .Item(ViewInvoiceData.INVC_EDI_HSTRY_DTL_ID) = intMax
                .Item(ViewInvoiceData.INVC_EDI_HSTRY_ID) = bv_i64InvoiceEdiId
                .Item(ViewInvoiceData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(ViewInvoiceData.INVC_NO) = bv_strInvoiceno
                If bv_strLineResponse <> Nothing Then
                    .Item(ViewInvoiceData.LN_RSPNS_VC) = bv_strInvoiceno
                Else
                    .Item(ViewInvoiceData.LN_RSPNS_VC) = DBNull.Value
                End If
                If bv_strSupportUrl <> Nothing Then
                    .Item(ViewInvoiceData.SPPRT_URL) = bv_strSupportUrl
                Else
                    .Item(ViewInvoiceData.SPPRT_URL) = DBNull.Value
                End If

            End With
            objData.InsertRow(dr, INVOICE_EDI_HISTORY_DETAILInsertQuery, br_objTrans)
            dr = Nothing
            CreateXMLEDIDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#End Region

#Region "GetINSPECTIONCHARGEByByDepotIDInvoiceNo() TABLE NAME:CLEANING_CHARGE"

    Public Function GetINSPECTIONCHARGEByByDepotIDInvoiceNo(ByVal bv_i32DepotID As Int64, _
                                                          ByVal bv_i64ServicePartnerID As Int64, _
                                                          ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.DRFT_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_AGNT_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(Inspection_SelectQueryByInvoicingPartyID, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_INSPECTION_CHARGES)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Invoice Cancel Operation"

    'Get View invoice Role rights Information - about Logged user
    Public Function GetViewInvoiceRoleRights(ByVal bv_ActivityID As Int64, ByVal bv_UserName As String) As DataTable
        Try
            Dim dt As New DataTable

            Dim hshParams As New Hashtable()
            hshParams.Add(CommonUIData.USR_NAM, bv_UserName)
            hshParams.Add(CommonUIData.ACTVTY_ID, bv_ActivityID)

            objData = New DataObjects(GetViewInvoiceRoleRights_SelectQry, hshParams)
            objData.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Get Handling charge for Finalized Invoice
    Public Function GetHANDLINGCHARGEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_i64ServicePartnerID As Int64, _
                                                         ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(GetHANDLINGCHARGEByByDepotIDInvoiceNoCancel_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_HANDLING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Get Handling charge for Finalized Invoice
    Public Function Get_HandlingInvoice_DraftNo(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_strInvoiceNo As String, _
                                                         ByRef objTrans As Transactions) As DataTable
        Try
            Dim hshParams As New Hashtable()
            Dim dt As New DataTable
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)

            objData = New DataObjects(Get_HandlingInvoice_DraftNo_DraftNo_Qry, hshParams)
            objData.Fill(dt, objTrans)
            dt.TableName = CommonUIData._HANDLING_CHARGE
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Cancel_HandlingInvoice(ByVal bv_i32DepotID As Int64, _
                                           ByVal bv_strInvoiceNo As String, _
                                           ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._HANDLING_CHARGE).NewRow()

            With dr
                .Item(CommonUIData.DPT_ID) = bv_i32DepotID
                .Item(CommonUIData.FNL_INVC_NO) = bv_strInvoiceNo
            End With

            Cancel_HandlingInvoice = objData.UpdateRow(dr, Cancel_HandlingInvoice_Update_Query, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Get Storage charge for Finalized Invoice
    Public Function GetSTORAGECHARGEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                        ByVal bv_i64ServicePartnerID As Int64, _
                                                        ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(GetSTORAGECHARGEByByDepotIDInvoiceNoCancel_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_STORAGE_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Get Storage charge for Finalized Invoice
    Public Function Get_StorageInvoice_DraftNo(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_strInvoiceNo As String, _
                                                         ByRef objTrans As Transactions) As DataTable
        Try
            Dim hshParams As New Hashtable()
            Dim dt As New DataTable
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)

            objData = New DataObjects(Get_StorageInvoice_DraftNo_DraftNo_Qry, hshParams)
            objData.Fill(dt, objTrans)
            dt.TableName = CommonUIData._STORAGE_CHARGE
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Cancel_StorageInvoice(ByVal bv_i32DepotID As Int64, _
                                           ByVal bv_strInvoiceNo As String, _
                                           ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._STORAGE_CHARGE).NewRow()

            With dr
                .Item(CommonUIData.DPT_ID) = bv_i32DepotID
                .Item(CommonUIData.FNL_INVC_NO) = bv_strInvoiceNo
            End With

            Cancel_StorageInvoice = objData.UpdateRow(dr, Cancel_StorageInvoice_Update_Query, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Get HEATING charge for Finalized Invoice
    Public Function GetHEATINGCHARGEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                        ByVal bv_i64ServicePartnerID As Int64, _
                                                        ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.SRVC_PRTNR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(GetHEATINGCHARGEByByDepotIDInvoiceNoCancel_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_HEATING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Get Storage charge for Finalized Invoice
    Public Function Get_HeatingInvoice_DraftNo(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_strInvoiceNo As String, _
                                                         ByRef objTrans As Transactions) As DataTable
        Try
            Dim hshParams As New Hashtable()
            Dim dt As New DataTable
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)

            objData = New DataObjects(Get_HeatingInvoice_DraftNo_Qry, hshParams)
            objData.Fill(dt, objTrans)
            dt.TableName = CommonUIData._HEATING_CHARGE
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Cancel_HeatingInvoice(ByVal bv_i32DepotID As Int64, _
                                           ByVal bv_strInvoiceNo As String, _
                                           ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._HEATING_CHARGE).NewRow()

            With dr
                .Item(CommonUIData.DPT_ID) = bv_i32DepotID
                .Item(CommonUIData.FNL_INVC_NO) = bv_strInvoiceNo
            End With

            Cancel_HeatingInvoice = objData.UpdateRow(dr, Cancel_HeatingInvoice_Update_Query, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Get Cleaning charge for Finalized Invoice
    Public Function Get_CleaningInvoice_DraftNo(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_strInvoiceNo As String, _
                                                         ByRef objTrans As Transactions) As DataTable
        Try
            Dim hshParams As New Hashtable()
            Dim dt As New DataTable
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)

            objData = New DataObjects(Get_CleaningInvoice_DraftNo_Qry, hshParams)
            objData.Fill(dt, objTrans)
            dt.TableName = ViewInvoiceData._CLEANING_CHARGE
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Cancel_CleaningInvoice(ByVal bv_i32DepotID As Int64, _
                                           ByVal bv_strInvoiceNo As String, _
                                           ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._CLEANING_CHARGE).NewRow()

            With dr
                .Item(CommonUIData.DPT_ID) = bv_i32DepotID
                .Item(CommonUIData.FNL_INVC_NO) = bv_strInvoiceNo
            End With

            Cancel_CleaningInvoice = objData.UpdateRow(dr, Cancel_CleaningInvoice_Update_Query, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    'Get Repair charge for Finalized Invoice
    Public Function GetREPAIRCHARGEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                        ByVal bv_i64ServicePartnerID As Int64, _
                                                        ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.SRVC_PRTNR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(GetREPAIRCHARGEByByDepotIDInvoiceNoCancel_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_REPAIR_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Get Repair charge for Finalized Invoice
    Public Function Get_RepairInvoice_DraftNo(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_strInvoiceNo As String, _
                                                         ByRef objTrans As Transactions) As DataTable
        Try
            Dim hshParams As New Hashtable()
            Dim dt As New DataTable
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)

            objData = New DataObjects(Get_RepairInvoice_DraftNo_DraftNo_Qry, hshParams)
            objData.Fill(dt, objTrans)
            dt.TableName = ViewInvoiceData._REPAIR_CHARGE
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Cancel_RepairInvoice(ByVal bv_i32DepotID As Int64, _
                                           ByVal bv_strInvoiceNo As String, _
                                           ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._REPAIR_CHARGE).NewRow()

            With dr
                .Item(CommonUIData.DPT_ID) = bv_i32DepotID
                .Item(CommonUIData.FNL_INVC_NO) = bv_strInvoiceNo
            End With

            Cancel_RepairInvoice = objData.UpdateRow(dr, Cancel_RepairInvoice_Update_Query, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Get Miscellanceous and Credit Note charge for Finalized Invoice
    Public Function GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                               ByVal bv_i64ServicePartnerID As Int64, _
                                                               ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.INVCNG_TO_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(GetMISCELLANEOUSINVOICEByByDepotIDInvoiceNoCancel_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_MISCELLANEOUS_INVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Get Storage charge for Finalized Invoice
    Public Function Get_MISCELLANEOUSInvoice_DraftNo(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_strInvoiceNo As String, _
                                                         ByRef objTrans As Transactions) As DataTable
        Try
            Dim hshParams As New Hashtable()
            Dim dt As New DataTable
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)

            objData = New DataObjects(Get_MISCELLANEOUSInvoice_DraftNo_Qry, hshParams)
            objData.Fill(dt, objTrans)
            dt.TableName = CommonUIData._MISCELLANEOUS_INVOICE
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Cancel_MISCELLANEOUSInvoice(ByVal bv_i32DepotID As Int64, _
                                           ByVal bv_strInvoiceNo As String, _
                                           ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._MISCELLANEOUS_INVOICE).NewRow()

            With dr
                .Item(CommonUIData.DPT_ID) = bv_i32DepotID
                .Item(CommonUIData.FNL_INVC_NO) = bv_strInvoiceNo
            End With

            Cancel_MISCELLANEOUSInvoice = objData.UpdateRow(dr, Cancel_MISCELLANEOUSInvoice_Update_Query, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Get Transportation charge for Finalized Invoice
    Public Function GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                                ByVal bv_i64ServicePartnerID As Int64, _
                                                                ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(GetTRANSPORTATIONINVOICEByByDepotIDInvoiceNoCancel_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_TRANSPORTATION_INVOICE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Get Repair charge for Finalized Invoice
    Public Function Get_TransPortationInvoice_DraftNo(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_strInvoiceNo As String, _
                                                         ByRef objTrans As Transactions) As DataTable
        Try
            Dim hshParams As New Hashtable()
            Dim dt As New DataTable
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)

            objData = New DataObjects(Get_TransPortationInvoice_DraftNo_Qry, hshParams)
            objData.Fill(dt, objTrans)
            dt.TableName = CommonUIData._TRANSPORTATION_CHARGE
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Cancel_TransportationInvoice(ByVal bv_i32DepotID As Int64, _
                                           ByVal bv_strInvoiceNo As String, _
                                           ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._TRANSPORTATION_CHARGE).NewRow()

            With dr
                .Item(CommonUIData.DPT_ID) = bv_i32DepotID
                .Item(CommonUIData.FNL_INVC_NO) = bv_strInvoiceNo
            End With

            Cancel_TransportationInvoice = objData.UpdateRow(dr, Cancel_TransportationInvoice_Update_Query, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Get Rental charge for Finalized Invoice
    Public Function GetRentalChargeByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                       ByVal bv_i64ServicePartnerID As Int64, _
                                                       ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.CSTMR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(GetRentalChargeByByDepotIDInvoiceNoCancel_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_RENTAL_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    'Get Rental charge for Finalized Invoice
    Public Function Get_RentalInvoice_DraftNo(ByVal bv_i32DepotID As Int64, _
                                                         ByVal bv_strInvoiceNo As String, _
                                                         ByRef objTrans As Transactions) As DataTable
        Try
            Dim hshParams As New Hashtable()
            Dim dt As New DataTable
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)

            objData = New DataObjects(Get_RentalInvoice_DraftNo_Qry, hshParams)
            objData.Fill(dt, objTrans)
            dt.TableName = CommonUIData._RENTAL_CHARGE
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Cancel_RentalInvoice(ByVal bv_i32DepotID As Int64, _
                                           ByVal bv_strInvoiceNo As String, _
                                           ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._RENTAL_CHARGE).NewRow()

            With dr
                .Item(CommonUIData.DPT_ID) = bv_i32DepotID
                .Item(CommonUIData.FNL_INVC_NO) = bv_strInvoiceNo
            End With

            Cancel_RentalInvoice = objData.UpdateRow(dr, Cancel_RentalInvoice_Update_Query, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Update Cleaning Charge Table


    Public Function GetCLEANINGCHARGEByByDepotIDInvoiceNoCancel(ByVal bv_i32DepotID As Int64, _
                                                        ByVal bv_i64ServicePartnerID As Int64, _
                                                        ByVal bv_strInvoiceNo As String) As ViewInvoiceDataSet
        Try
            Dim hshParams As New Hashtable()
            hshParams.Add(ViewInvoiceData.FNL_INVC_NO, bv_strInvoiceNo)
            hshParams.Add(ViewInvoiceData.DPT_ID, bv_i32DepotID)
            hshParams.Add(ViewInvoiceData.SRVC_PRTNR_ID, bv_i64ServicePartnerID)
            objData = New DataObjects(GetCLEANINGCHARGEByByDepotIDInvoiceNoCancel_Qry, hshParams)
            objData.Fill(CType(ds, DataSet), ViewInvoiceData._V_CLEANING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Disable Final Invoice No - For Invoice Cancel

    Public Function DeActive_FinalInvoice(ByVal bv_i32DepotID As Int64, _
                                          ByVal bv_strInvoiceNo As String, _
                                          ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._INVOICE_HISTORY).NewRow()

            With dr
                .Item(CommonUIData.DPT_ID) = bv_i32DepotID
                .Item(CommonUIData.INVC_NO) = bv_strInvoiceNo
            End With

            DeActive_FinalInvoice = objData.UpdateRow(dr, DeActive_FinalInvoice_Update_Query, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Active_DraftInvoice(ByVal bv_i32DepotID As Int64, _
                                         ByVal bv_strInvoiceNo As String, _
                                         ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._INVOICE_HISTORY).NewRow()

            With dr
                .Item(CommonUIData.DPT_ID) = bv_i32DepotID
                .Item(CommonUIData.INVC_NO) = bv_strInvoiceNo
            End With

            Active_DraftInvoice = objData.UpdateRow(dr, Active_DraftInvoice_Update_Query, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Delete_InvoiceHistoryDetails(ByVal bv_strFinalInvoiceNo As String, ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._INVOICE_HISTORY_DETAIL).NewRow()

            With dr
                .Item(CommonUIData.INVC_NO) = bv_strFinalInvoiceNo
            End With

            Delete_InvoiceHistoryDetails = objData.DeleteRow(dr, Delete_InvoiceHistoryDetails_Query, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CreateCreditNote_InvoiceHistory(ByVal bv_strInvoiceNo As String, _
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
                                             ByVal bv_strFileName As String, _
                                             ByVal bv_i64NoofEquipment As Int64, _
                                             ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CommonUIData._INVOICE_HISTORY).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CommonUIData._INVOICE_HISTORY, br_objTrans)
                .Item(CommonUIData.INVC_BIN) = intMax
                .Item(CommonUIData.INVC_NO) = bv_strInvoiceNo
                .Item(CommonUIData.INVC_DT) = bv_datInvoiceDate
                If bv_strInvoiceFilePath <> Nothing Then
                    .Item(CommonUIData.INVC_FL_PTH) = bv_strInvoiceFilePath
                Else
                    .Item(CommonUIData.INVC_FL_PTH) = DBNull.Value
                End If
                .Item(CommonUIData.INVC_TYP) = bv_strInvoiceType
                .Item(CommonUIData.INVC_CRRNCY_ID) = bv_i64InvoiceCurrencyID
                .Item(CommonUIData.EXCHNG_RT_NC) = bv_decExchangeRate
                .Item(CommonUIData.CSTMR_CRRNCY_ID) = bv_i64CustomerCurrencyID
                If bv_i64BillingTypeID <> 0 Then
                    .Item(CommonUIData.BLLNG_TYP_ID) = bv_i64BillingTypeID
                Else
                    .Item(CommonUIData.BLLNG_TYP_ID) = DBNull.Value
                End If
                .Item(CommonUIData.FRM_BLLNG_DT) = bv_datFromBillingDate
                .Item(CommonUIData.TO_BLLNG_DT) = bv_datToBillingDate
                .Item(CommonUIData.TTL_CST_IN_CSTMR_CRRNCY_NC) = bv_decTotalCostinCustomerCurrency
                .Item(CommonUIData.TTL_CST_IN_INVC_CRRNCY_NC) = bv_decTotalCostinInvoiceCurrency
                .Item(CommonUIData.BLLNG_FLG) = bv_strBillingFlag
                If bv_i64CustomerID <> 0 Then
                    .Item(CommonUIData.CSTMR_ID) = bv_i64CustomerID
                Else
                    .Item(CommonUIData.CSTMR_ID) = DBNull.Value
                End If
                If bv_i64InvoicingPartyID <> 0 Then
                    .Item(CommonUIData.INVCNG_PRTY_ID) = bv_i64InvoicingPartyID
                Else
                    .Item(CommonUIData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                .Item(CommonUIData.DPT_ID) = bv_i32DepotID
                .Item(CommonUIData.ACTV_BT) = bv_blnActiveBit
                .Item(CommonUIData.CRTD_BY) = bv_strCreatedBy
                .Item(CommonUIData.CRTD_DT) = bv_datCreatedDate
                .Item(CommonUIData.MDFD_BY) = bv_strCreatedBy
                .Item(CommonUIData.MDFD_DT) = bv_datCreatedDate

                If bv_strFileName <> Nothing Then
                    .Item(CommonUIData.FL_NM) = bv_strFileName
                Else
                    .Item(CommonUIData.FL_NM) = DBNull.Value
                End If

                If bv_i64NoofEquipment <> Nothing Then
                    .Item(CommonUIData.NO_OF_EQPMNT) = bv_i64NoofEquipment
                Else
                    .Item(CommonUIData.NO_OF_EQPMNT) = DBNull.Value
                End If

                .Item(InvoiceReversalData.INVC_CNCL) = "CREDITNOTE"

            End With
            objData.InsertRow(dr, CreateCreditNote_InvoiceHistory_InsertQuery, br_objTrans)
            dr = Nothing
            CreateCreditNote_InvoiceHistory = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "Get_BilledTransportationDetailByTransportId"
    Public Function Get_BilledTransportationDetailByTransportId(ByVal bv_intTransportId As Integer, ByRef br_objTransaction As Transactions) As DataTable
        Try
            Dim hshParams As New Hashtable()
            Dim dtTransportDetail As New DataTable
            hshParams.Add(ViewInvoiceData.TRNSPRTTN_ID, bv_intTransportId)
            objData = New DataObjects(Get_BilledTransportationDetail_Select_Qry, hshParams)
            objData.Fill(dtTransportDetail, br_objTransaction)
            Return dtTransportDetail
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Update_TransportationStatusByTransportId"

    Public Function Update_TransportationStatusByTransportId(ByVal bv_intTransportId As Integer, ByVal bv_intTransportStatus As Integer, ByVal bv_intDepotId As Integer, ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            Dim dsTrans As New TransportationDataSet

            objData = New DataObjects()
            dr = dsTrans.Tables(TransportationData._TRANSPORTATION).NewRow()

            With dr
                .Item(TransportationData.TRNSPRTTN_ID) = bv_intTransportId
                .Item(TransportationData.DPT_ID) = bv_intDepotId
            End With

            Update_TransportationStatusByTransportId = objData.UpdateRow(dr, String.Concat("UPDATE TRANSPORTATION SET TRNSPRTTN_STTS_ID = ", bv_intTransportStatus, " WHERE TRNSPRTTN_ID=@TRNSPRTTN_ID AND DPT_ID=@DPT_ID"), objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class

#End Region
