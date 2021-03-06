#Region "ViewInvoiceData"
	Public Class ViewInvoiceData

#Region "Declaration Part.."
    'Data Tables..
    Public Const _HANDLING_CHARGE As String = "HANDLING_CHARGE"
    Public Const _V_HANDLING_CHARGE As String = "V_HANDLING_CHARGE"
    Public Const _STORAGE_CHARGE As String = "STORAGE_CHARGE"
    Public Const _V_STORAGE_CHARGE As String = "V_STORAGE_CHARGE"
    Public Const _HANDLING_STORAGE_INVOICE As String = "HANDLING_STORAGE_INVOICE"
    Public Const _CUSTOMER As String = "CUSTOMER"
    Public Const _V_CUSTOMER As String = "V_CUSTOMER"
    Public Const _DEPOT As String = "DEPOT"
    Public Const _V_BANK_DETAIL As String = "V_BANK_DETAIL"
    Public Const _V_INVOICING_PARTY As String = "V_INVOICING_PARTY"
    Public Const _V_CUSTOMER_CHARGE_DETAIL As String = "V_CUSTOMER_CHARGE_DETAIL"
    Public Const _V_REPAIR_CHARGE As String = "V_REPAIR_CHARGE"
    Public Const _PENDING_INVOICE As String = "PENDING_INVOICE"
    Public Const _FOREIGN_BANK_DETAIL As String = "FOREIGN_BANK_DETAIL"
    Public Const _V_CLEANING_CHARGE As String = "V_CLEANING_CHARGE"
    Public Const _V_MISCELLANEOUS_INVOICE As String = "V_MISCELLANEOUS_INVOICE"
    Public Const _V_HEATING_CHARGE As String = "V_HEATING_CHARGE"
    Public Const _V_EXCHANGE_RATE As String = "V_EXCHANGE_RATE"
    Public Const _HEATING_INVOICE As String = "HEATING_INVOICE"
    Public Const _MISCELLANEOUS_INVOICE As String = "MISCELLANEOUS_INVOICE"
    Public Const _CLEANING_INVOICE As String = "CLEANING_INVOICE"
    Public Const _REPAIR_INVOICE As String = "REPAIR_INVOICE"
    Public Const _INVOICE_HISTORY As String = "INVOICE_HISTORY"
    Public Const _CLEANING_CHARGE As String = "CLEANING_CHARGE"
    Public Const _HEATING_CHARGE As String = "HEATING_CHARGE"
    Public Const _REPAIR_CHARGE As String = "REPAIR_CHARGE"
    Public Const _V_INVOICE_HISTORY As String = "V_INVOICE_HISTORY"
    Public Const _V_TRANSPORTATION_INVOICE As String = "V_TRANSPORTATION_INVOICE"
    Public Const _TRANSPORTATION_INVOICE As String = "TRANSPORTATION_INVOICE"
    Public Const _V_RENTAL_CHARGE As String = "V_RENTAL_CHARGE"
    Public Const _RENTAL_INVOICE As String = "RENTAL_INVOICE"
    Public Const _INVOICE_EDI_HISTORY As String = "INVOICE_EDI_HISTORY"
    Public Const _INVOICE_EDI_HISTORY_DETAIL As String = "INVOICE_EDI_HISTORY_DETAIL"
    Public Const _V_INSPECTION_CHARGES As String = "V_INSPECTION_CHARGES"
    Public Const _INSPECTION_INVOICE As String = "INSPECTION_INVOICE"
    'Data Columns..

    'Table Name: HANDLING_CHARGE
    Public Const HNDLNG_CHRG_ID As String = "HNDLNG_CHRG_ID"
    Public Const EQPMNT_NO As String = "EQPMNT_NO"
    Public Const EQPMNT_CD_ID As String = "EQPMNT_CD_ID"
    Public Const EQPMNT_TYP_ID As String = "EQPMNT_TYP_ID"
    Public Const CST_TYP As String = "CST_TYP"
    Public Const RFRNC_EIR_NO_1 As String = "RFRNC_EIR_NO_1"
    Public Const RFRNC_EIR_NO_2 As String = "RFRNC_EIR_NO_2"
    Public Const FRM_BLLNG_DT As String = "FRM_BLLNG_DT"
    Public Const TO_BLLNG_DT As String = "TO_BLLNG_DT"
    Public Const FR_DYS As String = "FR_DYS"
    Public Const NO_OF_DYS As String = "NO_OF_DYS"
    Public Const HNDLNG_CST_NC As String = "HNDLNG_CST_NC"
    Public Const HNDLNG_TX_RT_NC As String = "HNDLNG_TX_RT_NC"
    Public Const TTL_CSTS_NC As String = "TTL_CSTS_NC"
    Public Const BLLNG_FLG As String = "BLLNG_FLG"
    Public Const ACTV_BT As String = "ACTV_BT"
    Public Const DPT_ID As String = "DPT_ID"
    Public Const IS_GT_OT_FLG As String = "IS_GT_OT_FLG"
    Public Const YRD_LCTN As String = "YRD_LCTN"
    Public Const BLLNG_TYP_CD As String = "BLLNG_TYP_CD"
    Public Const CSTMR_ID As String = "CSTMR_ID"
    Public Const HTNG_BT As String = "HTNG_BT"
    Public Const GI_TRNSCTN_NO As String = "GI_TRNSCTN_NO"
    Public Const INVCNG_PRTY_ID As String = "INVCNG_PRTY_ID"
    Public Const FROM_DATE As String = "FROM_DATE"
    Public Const TO_DATE As String = "TO_DATE"

    'Table Name: STORAGE_CHARGE
    Public Const STRG_CHRG_ID As String = "STRG_CHRG_ID"
    Public Const STRG_CST_NC As String = "STRG_CST_NC"
    Public Const STRG_TX_RT_NC As String = "STRG_TX_RT_NC"
    Public Const STRG_CNTN_FLG As String = "STRG_CNTN_FLG"
    Public Const IS_LT_FLG As String = "IS_LT_FLG"
    Public Const BLLNG_TLL_DT As String = "BLLNG_TLL_DT"

    'Table Name: V_HANDLING_CHARGE
    Public Const CSTMR_CD As String = "CSTMR_CD"
    Public Const EQPMNT_CD_CD As String = "EQPMNT_CD_CD"
    Public Const EQPMNT_TYP_CD As String = "EQPMNT_TYP_CD"
    Public Const PRDCT_CD As String = "PRDCT_CD"
    Public Const GTN_DT As String = "GTN_DT"
    Public Const STORAGE_DAYS As String = "STORAGE_DAYS"
    Public Const CHARGEABLE_DAYS As String = "CHARGEABLE_DAYS"

    'Table Name: HANDLING_STORAGE_INVOCIE
    Public Const HNDLNG_STRG_ID As String = "HNDLNG_STRG_ID"
    Public Const HNDIN_CHRG As String = "HNDIN_CHRG"
    Public Const HNDOUT_CHRG As String = "HNDOUT_CHRG"
    Public Const STRG_CHRG As String = "STRG_CHRG"
    Public Const CHECKED As String = "CHECKED"
    Public Const HNDIN As String = "HNDIN"
    Public Const HNDOT As String = "HNDOT"
    Public Const STR As String = "STR"
    Public Const HANDLINGSTORAGE_INVOICE_DRAFT As String = "HANDLINGSTORAGE_INVOICE_DRAFT"

    'Table Name: EXCHANGE RATE
    Public Const FRM_CRRNCY_CD As String = "FRM_CRRNCY_CD"
    Public Const TO_CRRNCY_CD As String = "TO_CRRNCY_CD"
    Public Const EXCHNG_RT_PR_UNT_NC As String = "EXCHNG_RT_PR_UNT_NC"

    'Table Name: CUSTOMER_STORAGE_DETAIL
    Public Const CSTMR_STRG_DTL_ID As String = "CSTMR_STRG_DTL_ID"
    Public Const UP_TO_DYS As String = "UP_TO_DYS"
    Public Const STRG_CHRG_NC As String = "STRG_CHRG_NC"
    Public Const RMRKS_VC As String = "RMRKS_VC"

    'Table Name: V_CUSTOMER
    Public Const CSTMR_CRRNCY_ID As String = "CSTMR_CRRNCY_ID"
    Public Const CSTMR_CRRNCY_CD As String = "CSTMR_CRRNCY_CD"
    Public Const BLLNG_TYP_ID As String = "BLLNG_TYP_ID"
    Public Const CNTCT_PRSN_NAM As String = "CNTCT_PRSN_NAM"
    Public Const CNTCT_ADDRSS As String = "CNTCT_ADDRSS"
    Public Const BLLNG_ADDRSS As String = "BLLNG_ADDRSS"
    Public Const ZP_CD As String = "ZP_CD"
    Public Const PHN_NO As String = "PHN_NO"
    Public Const FX_NO As String = "FX_NO"
    Public Const RPRTNG_EML_ID As String = "RPRTNG_EML_ID"
    Public Const INVCNG_EML_ID As String = "INVCNG_EML_ID"
    Public Const RPR_TCH_EML_ID As String = "RPR_TCH_EML_ID"
    Public Const BLK_EML_FRMT_ID As String = "BLK_EML_FRMT_ID"
    Public Const BLK_EML_FRMT_CD As String = "BLK_EML_FRMT_CD"
    Public Const HYDR_AMNT_NC As String = "HYDR_AMNT_NC"
    Public Const PNMTC_AMNT_NC As String = "PNMTC_AMNT_NC"
    Public Const LBR_RT_PR_HR_NC As String = "LBR_RT_PR_HR_NC"
    Public Const LK_TST_RT_NC As String = "LK_TST_RT_NC"
    Public Const SRVY_ONHR_OFFHR_RT_NC As String = "SRVY_ONHR_OFFHR_RT_NC"
    Public Const PRDC_TST_TYP_ID As String = "PRDC_TST_TYP_ID"
    Public Const PRDC_TST_TYP_CD As String = "PRDC_TST_TYP_CD"
    Public Const VLDTY_PRD_TST_YRS As String = "VLDTY_PRD_TST_YRS"
    Public Const CHK_DGT_VLDTN_BT As String = "CHK_DGT_VLDTN_BT"
    Public Const TRNSPRTTN_BT As String = "TRNSPRTTN_BT"
    Public Const RNTL_BT As String = "RNTL_BT"
    Public Const INVC_CNCL As String = "INVC_CNCL"

    'Table Name: V_REPAIR_CHARGE
    Public Const RPR_CHRG_ID As String = "RPR_CHRG_ID"
    Public Const RPR_ESTMT_ID As String = "RPR_ESTMT_ID"
    Public Const ESTMT_NO As String = "ESTMT_NO"
    Public Const RPR_APPRVL_DT As String = "RPR_APPRVL_DT"
    Public Const RPR_CMPLTN_DT As String = "RPR_CMPLTN_DT"
    Public Const MTRL_CST_NC As String = "MTRL_CST_NC"
    Public Const LBR_CST_NC As String = "LBR_CST_NC"
    Public Const RPR_TX_RT_NC As String = "RPR_TX_RT_NC"
    Public Const RPR_TX_AMNT_NC As String = "RPR_TX_AMNT_NC"
    Public Const RPR_APPRVL_AMNT_NC As String = "RPR_APPRVL_AMNT_NC"
    Public Const DPT_CD As String = "DPT_CD"
    Public Const DPT_NAM As String = "DPT_NAM"
    Public Const CSTMR_NAM As String = "CSTMR_NAM"
    Public Const CLN_CST_NC As String = "CLN_CST_NC"
    Public Const INVCNG_PRTY_CD As String = "INVCNG_PRTY_CD"
    Public Const INVCNG_PRTY_NM As String = "INVCNG_PRTY_NM"
    Public Const TTL_SRV_TAX As String = "TTL_SRV_TAX"
    Public Const TTL_EST_INCL_SRV_TAX As String = "TTL_EST_INCL_SRV_TAX"
    Public Const INVC_TYPE As String = "INVC_TYPE"
    Public Const LEAK_TEST As String = "LEAK_TEST"
    Public Const PTI As String = "PTI"
    Public Const SURVEY As String = "SURVEY"
    Public Const SRVC_PRTNR_ID As String = "SRVC_PRTNR_ID"
    Public Const TOTALAMOUNT As String = "TOTALAMOUNT"
    Public Const REPAIRS As String = "REPAIRS"
    Public Const DPT_TTL_NC As String = "DPT_TTL_NC"
    Public Const REPAIR_INVOICE_DRAFT As String = "REPAIR_INVOICE_DRAFT"
    Public Const APPRVL_RF_NO As String = "APPRVL_RF_NO"
    Public Const RSPNSBLTY_ID As String = "RSPNSBLTY_ID"


    'Table Name: V_CLEANING_CHARGE
    Public Const CLNNG_CHRG_ID As String = "CLNNG_CHRG_ID"
    Public Const CLNNG_ID As String = "CLNNG_ID"
    Public Const PRDCT_DSCRPTN_VC As String = "PRDCT_DSCRPTN_VC"
    Public Const ORGNL_CLNNG_DT As String = "ORGNL_CLNNG_DT"
    Public Const ORGNL_INSPCTD_DT As String = "ORGNL_INSPCTD_DT"
    Public Const CLNNG_RT As String = "CLNNG_RT"
    Public Const CLNNG_CERT_NO As String = "CLNNG_CERT_NO"
    Public Const RFRNC_NO As String = "RFRNC_NO"
    Public Const CLEANING_INVOICE_DRAFT As String = "CLEANING_INVOICE_DRAFT"

    'Table Name: V_MISCELLANEOUS_INVOICE
    Public Const MSCLLNS_INVC_ID As String = "MSCLLNS_INVC_ID"
    Public Const INVCNG_TO_ID As String = "INVCNG_TO_ID"
    Public Const INVCNG_TO_CD As String = "INVCNG_TO_CD"
    Public Const INVCNG_TO_NAM As String = "INVCNG_TO_NAM"
    Public Const SRVC_PRTNR_TYP_CD As String = "SRVC_PRTNR_TYP_CD"
    Public Const ACTVTY_DT As String = "ACTVTY_DT"
    Public Const CHRG_DSCRPTN As String = "CHRG_DSCRPTN"
    Public Const AMNT_NC As String = "AMNT_NC"
    Public Const CRTD_BY As String = "CRTD_BY"
    Public Const CRTD_DT As String = "CRTD_DT"
    Public Const MDFD_BY As String = "MDFD_BY"
    Public Const MDFD_DT As String = "MDFD_DT"
    Public Const CNCLD_BY As String = "CNCLD_BY"
    Public Const CNCLD_DT As String = "CNCLD_DT"
    Public Const ADT_RMRKS As String = "ADT_RMRKS"
    Public Const MISCELLANEOUS_INVOICE_DRAFT As String = "MISCELLANEOUS_INVOICE_DRAFT"

    'Table Name: V_HEATING_CHARGE
    Public Const HTNG_ID As String = "HTNG_ID"
    Public Const HTNG_CD As String = "HTNG_CD"
    Public Const HTNG_STRT_DT As String = "HTNG_STRT_DT"
    Public Const HTNG_STRT_TM As String = "HTNG_STRT_TM"
    Public Const HTNG_END_DT As String = "HTNG_END_DT"
    Public Const HTNG_END_TM As String = "HTNG_END_TM"
    Public Const HTNG_TMPRTR As String = "HTNG_TMPRTR"
    Public Const TTL_HTN_PRD As String = "TTL_HTN_PRD"
    Public Const MIN_HTNG_RT_NC As String = "MIN_HTNG_RT_NC"
    Public Const HRLY_CHRG_NC As String = "HRLY_CHRG_NC"
    Public Const TTL_RT_NC As String = "TTL_RT_NC"
    Public Const HTNG_STRT_DT_TM As String = "HTNG_STRT_DT_TM"
    Public Const HTNG_END_DT_TM As String = "HTNG_END_DT_TM"
    Public Const DRFT_INVC_NO As String = "DRFT_INVC_NO"
    Public Const FNL_INVC_NO As String = "FNL_INVC_NO"
    Public Const HEATING_INVOICE_DRAFT As String = "HEATING_INVOICE_DRAFT"

    'Table Name: V_EXCHANGE_RATE
    Public Const EXCHNG_RT_ID As String = "EXCHNG_RT_ID"
    Public Const FRM_CRRNCY_ID As String = "FRM_CRRNCY_ID"
    Public Const TO_CRRNCY_ID As String = "TO_CRRNCY_ID"
    Public Const WTH_EFFCT_FRM_DT As String = "WTH_EFFCT_FRM_DT"

    'Table Name: V_BANK_DETAIL
    Public Const BNK_DTL_BIN As String = "BNK_DTL_BIN"
    Public Const BNK_TYP_ID As String = "BNK_TYP_ID"
    Public Const BNK_TYP_CD As String = "BNK_TYP_CD"
    Public Const BNK_NM As String = "BNK_NM"
    Public Const BNK_ADDRSS As String = "BNK_ADDRSS"
    Public Const ACCNT_NO As String = "ACCNT_NO"
    Public Const IBAN_NO As String = "IBAN_NO"
    Public Const SWIFT_CD As String = "SWIFT_CD"
    Public Const CRRNCY_ID As String = "CRRNCY_ID"
    Public Const CRRNCY_CD As String = "CRRNCY_CD"

    'Table Name: INVOICE_HISTORY
    Public Const INVC_BIN As String = "INVC_BIN"
    Public Const INVC_NO As String = "INVC_NO"
    Public Const INVC_DT As String = "INVC_DT"
    Public Const INVC_FL_PTH As String = "INVC_FL_PTH"
    Public Const INVC_TYP As String = "INVC_TYP"
    Public Const INVC_CRRNCY_ID As String = "INVC_CRRNCY_ID"
    Public Const EXCHNG_RT_NC As String = "EXCHNG_RT_NC"
    Public Const TTL_CST_IN_CSTMR_CRRNCY_NC As String = "TTL_CST_IN_CSTMR_CRRNCY_NC"
    Public Const TTL_CST_IN_INVC_CRRNCY_NC As String = "TTL_CST_IN_INVC_CRRNCY_NC"
    Public Const FL_NM As String = "FL_NM"
    Public Const INVC_NM As String = "INVC_NM"
    Public Const NO_OF_EQPMNT As String = "NO_OF_EQPMNT"
    Public Const VIEW_BT As String = "VIEW_BT"

    'Table Name: V_TRANSPORTATION_INVOICE
    Public Const TRNSPRTTN_CHRG_ID As String = "TRNSPRTTN_CHRG_ID"
    Public Const TRNSPRTTN_ID As String = "TRNSPRTTN_ID"
    Public Const EQPMNT_STT_ID As String = "EQPMNT_STT_ID"
    Public Const EQPMNT_STT_CD As String = "EQPMNT_STT_CD"
    Public Const RQST_NO As String = "RQST_NO"
    Public Const CSTMR_RF_NO As String = "CSTMR_RF_NO"
    Public Const RT_ID As String = "RT_ID"
    Public Const RT_CD As String = "RT_CD"
    Public Const RT_DSCRPTN_VC As String = "RT_DSCRPTN_VC"
    Public Const EVNT_DT As String = "EVNT_DT"
    Public Const TRP_RT_NC As String = "TRP_RT_NC"
    Public Const WGHTMNT_AMNT_NC As String = "WGHTMNT_AMNT_NC"
    Public Const TKN_AMNT_NC As String = "TKN_AMNT_NC"
    Public Const RCHRGBL_CST_AMNT_NC As String = "RCHRGBL_CST_AMNT_NC"
    Public Const FNNC_CHRG_AMNT_NC As String = "FNNC_CHRG_AMNT_NC"
    Public Const DTNTN_AMNT_NC As String = "DTNTN_AMNT_NC"
    Public Const OTHR_CHRG_AMNT_NC As String = "OTHR_CHRG_AMNT_NC"
    Public Const DPT_AMNT As String = "DPT_AMNT"
    Public Const CSTMR_AMNT As String = "CSTMR_AMNT"
    Public Const DPT_CRRNCY_ID As String = "DPT_CRRNCY_ID"
    Public Const DPT_CRRNCY_CD As String = "DPT_CRRNCY_CD"
    Public Const JB_STRT_DT As String = "JB_STRT_DT"
    Public Const JB_END_DT As String = "JB_END_DT"
    Public Const RNTL_RFRNC_NO As String = "RNTL_RFRNC_NO"

    'Table Name: INVOICE_EDI_HISTORY
    Public Const INVC_EDI_HSTRY_ID As String = "INVC_EDI_HSTRY_ID"
    Public Const ACTVTY_NAM As String = "ACTVTY_NAM"
    Public Const SNT_FL_NAM As String = "SNT_FL_NAM"
    Public Const SNT_DT As String = "SNT_DT"
    Public Const RCVD_FL_NAM As String = "RCVD_FL_NAM"
    Public Const GNRTD_DT As String = "GNRTD_DT"
    Public Const STTS As String = "STTS"
    Public Const RSND_BT As String = "RSND_BT"
    Public Const GNRTD_BY As String = "GNRTD_BY"

    'Table Name: INVOICE_EDI_HISTORY_DETAIL
    Public Const INVC_EDI_HSTRY_DTL_ID As String = "INVC_EDI_HSTRY_DTL_ID"
    Public Const LN_RSPNS_VC As String = "LN_RSPNS_VC"
    Public Const SPPRT_URL As String = "SPPRT_URL"
    Public Const INVC_FL_NAM As String = "INVC_FL_NAM"
    Public Const CUSTOMERCRRNCY As String = "CUSTOMERCRRNCY"

    'Table Nam e : V_INSPECTION_CHARGES
    Public Const INSPCTN_CHRG_ID As String = "INSPCTN_CHRG_ID"
    Public Const CSTMR_AGNT_ID As String = "CSTMR_AGNT_ID"
    Public Const INSPCTN_CHRG As String = "INSPCTN_CHRG"
#End Region

	End Class

#End Region

