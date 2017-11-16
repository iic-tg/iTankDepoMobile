#Region "InvoiceData"
Public Class InvoiceData

#Region "Declaration Part.."
    'Data Tables..
    Public Const _HANDLING_CHARGE As String = "HANDLING_CHARGE"
    Public Const _INVOICE As String = "INVOICE"
    Public Const _REPAIR_CHARGE As String = "REPAIR_CHARGE"
    Public Const _STORAGE_CHARGE As String = "STORAGE_CHARGE"
    Public Const _V_CUSTOMER_INVOICE As String = "V_CUSTOMER_INVOICE"
    Public Const _V_INVOICE As String = "V_INVOICE"
    Public Const _V_HANDLING_CHARGE As String = "V_HANDLING_CHARGE"
    Public Const _V_REPAIR_CHARGE As String = "V_REPAIR_CHARGE"
    Public Const _V_STORAGE_CHARGE As String = "V_STORAGE_CHARGE"
    Public Const _CUSTOMER As String = "CUSTOMER"
    Public Const _V_HANDLING_STORAGE_INVOICE = "V_HANDLING_STORAGE_INVOICE"
    Public Const _V_CHARGE_DETAILS = "V_CHARGE_DETAILS"
    Public Const _V_RPT_PENDINGINVOICE As String = "V_RPT_PENDINGINVOICE"

    'Data Columns..

    'Table Name: HANDLING_CHARGE
    Public Const HNDLNG_CHRG_ID As String = "HNDLNG_CHRG_ID"
    Public Const EQPMNT_NO As String = "EQPMNT_NO"
    Public Const EQPMNT_SZ_ID As String = "EQPMNT_SZ_ID"
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
    Public Const MD_PYMNT As String = "MD_PYMNT"
    Public Const BLLNG_TYP_CD As String = "BLLNG_TYP_CD"
    Public Const LSS_ID As String = "LSS_ID"
    Public Const CSTMR_ID As String = "CSTMR_ID"

    Public Const EQPMNT_CD_ID As String = "EQPMNT_CD_ID"
    Public Const EQPMNT_CD_CD As String = "EQPMNT_CD_CD"

    'Table Name: INVOICE
    Public Const INVC_BIN As String = "INVC_BIN"
    Public Const INVC_NO As String = "INVC_NO"
    Public Const INVC_DT As String = "INVC_DT"
    Public Const INVC_FL_PTH As String = "INVC_FL_PTH"
    Public Const INVC_TYP As String = "INVC_TYP"
    Public Const INVC_CRRNCY_ID As String = "INVC_CRRNCY_ID"
    Public Const EXCHNG_RT_NC As String = "EXCHNG_RT_NC"
    Public Const CSTMR_CRRNCY_ID As String = "CSTMR_CRRNCY_ID"
    Public Const BLLNG_TYP_ID As String = "BLLNG_TYP_ID"
    Public Const TTL_CST_IN_CSTMR_CRRNCY_NC As String = "TTL_CST_IN_CSTMR_CRRNCY_NC"
    Public Const TTL_CST_IN_INVC_CRRNCY_NC As String = "TTL_CST_IN_INVC_CRRNCY_NC"
    Public Const CRTD_BY As String = "CRTD_BY"
    Public Const CRTD_DT As String = "CRTD_DT"

    'Table Name: REPAIR_CHARGE
    Public Const RPR_CHRG_ID As String = "RPR_CHRG_ID"
    Public Const ESTMT_NO As String = "ESTMT_NO"
    Public Const RPR_APPRVL_DT As String = "RPR_APPRVL_DT"
    Public Const RPR_CMPLTN_DT As String = "RPR_CMPLTN_DT"
    Public Const MTRL_CST_NC As String = "MTRL_CST_NC"
    Public Const LBR_CST_NC As String = "LBR_CST_NC"
    Public Const RPR_TX_RT_NC As String = "RPR_TX_RT_NC"
    Public Const RPR_TX_AMNT_NC As String = "RPR_TX_AMNT_NC"
    Public Const RPR_APPRVL_AMNT_NC As String = "RPR_APPRVL_AMNT_NC"
    Public Const CLN_CST_NC As String = "CLN_CST_NC"
    Public Const TTL_SRV_TAX As String = "TTL_SRV_TAX"
    Public Const TTL_EST_INCL_SRV_TAX As String = "TTL_EST_INCL_SRV_TAX"
    Public Const INVC_TYPE As String = "INVC_TYPE"
    Public Const APPRVL_RF_NO As String = "APPRVL_RF_NO"
    Public Const RSPNSBLTY_ID As String = "RSPNSBLTY_ID"

    'Table Name: STORAGE_CHARGE
    Public Const STRG_CHRG_ID As String = "STRG_CHRG_ID"
    Public Const STRG_CST_NC As String = "STRG_CST_NC"
    Public Const STRG_TX_RT_NC As String = "STRG_TX_RT_NC"
    Public Const STRG_CNTN_FLG As String = "STRG_CNTN_FLG"
    Public Const IS_LT_FLG As String = "IS_LT_FLG"
    Public Const BLLNG_TLL_DT As String = "BLLNG_TLL_DT"
    Public Const MODE_PYMNT As String = "MODE_PYMNT"
    Public Const PTI_RT As String = "PTI_RT"
    Public Const HNDLNG_CHRG_NC As String = "HNDLNG_CHRG_NC"
    Public Const WSHNG_CHRG_NC As String = "WSHNG_CHRG_NC"

    'Table Name: V_CUSTOMER_INVOICE
    Public Const CSTMR_CD As String = "CSTMR_CD"
    Public Const CSTMR_NAM As String = "CSTMR_NAM"
    Public Const DPT_CD As String = "DPT_CD"
    Public Const CSTMR_CRRNCY_CD As String = "CSTMR_CRRNCY_CD"
    Public Const CNVRT_TO_CRRNCY_ID As String = "CNVRT_TO_CRRNCY_ID"
    Public Const CNVRT_TO_CRRNCY_CD As String = "CNVRT_TO_CRRNCY_CD"
    Public Const EXCHNG_RT As String = "EXCHNG_RT"
    Public Const CNTCT_PRSN_NAM As String = "CNTCT_PRSN_NAM"
    Public Const BLLNG_ADDRSS_LN1 As String = "BLLNG_ADDRSS_LN1"
    Public Const BLLNG_ADDRSS_LN2 As String = "BLLNG_ADDRSS_LN2"
    Public Const CSTMR_VT_NO As String = "CSTMR_VT_NO"
    Public Const DPT_VT_NO As String = "DPT_VT_NO"
    Public Const LBR_RT_PR_HR_NC As String = "LBR_RT_PR_HR_NC"

    'Table Name: V_INVOICE
    Public Const INVC_CRRNCY_CD As String = "INVC_CRRNCY_CD"
    Public Const LST_HS_INVC_GNRTD_DT As String = "LST_HS_INVC_GNRTD_DT"
    Public Const LST_RP_INVC_GNRTD_DT As String = "LST_RP_INVC_GNRTD_DT"


    'Data Columns..

    'Table Name: V_HANDLING_CHARGE   
    Public Const LSS_CD As String = "LSS_CD"
    Public Const EQPMNT_SZ_CD As String = "EQPMNT_SZ_CD"
    Public Const EQPMNT_TYP_CD As String = "EQPMNT_TYP_CD"

    'Table Name: CUSTOMER   
    Public Const ZP_CD As String = "ZP_CD"
    Public Const PHN_NO As String = "PHN_NO"
    Public Const FX_NO As String = "FX_NO"
    Public Const EML_ID As String = "EML_ID"
    Public Const SRVC_TX_RT_NC As String = "SRVC_TX_RT_NC"
    Public Const MDFD_BY As String = "MDFD_BY"
    Public Const MDFD_DT As String = "MDFD_DT"


    'Table Name: V_HANDLING_STORAGE_INVOICE    
    Public Const CST_NC As String = "CST_NC"
    Public Const TX_RT_NC As String = "TX_RT_NC"

    'V_CHARGE_DETAILS
    Public Const LFT_CHRG_NC As String = "LFT_CHRG_NC"
    Public Const TTL_NC As String = "TTL_NC"
    Public Const STRG_CHRG_NC As String = "STRG_CHRG_NC"
    Public Const SB_TTL_NC As String = "SB_TTL_NC"
    Public Const VAT_NC As String = "VAT_NC"

    'Table Name: V_RPT_PENDINGINVOICE
    Public Const DAYS As String = "DAYS"
    Public Const STORAGE As String = "STORAGE"
    Public Const LIFTS As String = "LIFTS"
    Public Const PERIODFROM As String = "PERIODFROM"
    Public Const PERIODTO As String = "PERIODTO"

#End Region

End Class

#End Region

