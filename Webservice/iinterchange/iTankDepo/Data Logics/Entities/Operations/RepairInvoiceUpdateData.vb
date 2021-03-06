﻿Public Class RepairInvoiceUpdateData
#Region "Declaration Part.."
    'Data Tables..
    Public Const _V_REPAIR_CHARGE As String = "V_REPAIR_CHARGE"
    Public Const _REPAIR_CHARGE As String = "REPAIR_CHARGE"
    Public Const _AUDIT_LOG As String = "AUDIT_LOG"
    Public Const _OLD_REPAIR_CHARGE As String = "OLD_REPAIR_CHARGE"
    Public Const _NEW_REPAIR_CHARGE As String = "NEW_REPAIR_CHARGE"
    Public Const _REPAIR_ESTIMATE As String = "REPAIR_ESTIMATE"
    Public Const _V_REPAIR_ESTIMATE As String = "V_REPAIR_ESTIMATE"
    'Data Columns..

    'Table Name: V_REPAIR_CHARGE
    Public Const RPR_CHRG_ID As String = "RPR_CHRG_ID"
    Public Const EQPMNT_NO As String = "EQPMNT_NO"
    Public Const RPR_ESTMT_ID As String = "RPR_ESTMT_ID"
    Public Const EQPMNT_TYP_ID As String = "EQPMNT_TYP_ID"
    Public Const EQPMNT_TYP_CD As String = "EQPMNT_TYP_CD"
    Public Const RFRNC_EIR_NO_1 As String = "RFRNC_EIR_NO_1"
    Public Const RFRNC_EIR_NO_2 As String = "RFRNC_EIR_NO_2"
    Public Const ESTMT_NO As String = "ESTMT_NO"
    Public Const RPR_APPRVL_DT As String = "RPR_APPRVL_DT"
    Public Const RPR_CMPLTN_DT As String = "RPR_CMPLTN_DT"
    Public Const MTRL_CST_NC As String = "MTRL_CST_NC"
    Public Const LBR_CST_NC As String = "LBR_CST_NC"
    Public Const RPR_TX_RT_NC As String = "RPR_TX_RT_NC"
    Public Const RPR_TX_AMNT_NC As String = "RPR_TX_AMNT_NC"
    Public Const TTL_CSTS_NC As String = "TTL_CSTS_NC"
    Public Const RPR_APPRVL_AMNT_NC As String = "RPR_APPRVL_AMNT_NC"
    Public Const BLLNG_FLG As String = "BLLNG_FLG"
    Public Const YRD_LCTN As String = "YRD_LCTN"
    Public Const DPT_ID As String = "DPT_ID"
    Public Const DPT_CD As String = "DPT_CD"
    Public Const DPT_NAM As String = "DPT_NAM"
    Public Const CSTMR_ID As String = "CSTMR_ID"
    Public Const GI_TRNSCTN_NO As String = "GI_TRNSCTN_NO"
    Public Const CSTMR_CD As String = "CSTMR_CD"
    Public Const CSTMR_NAM As String = "CSTMR_NAM"
    Public Const PRDCT_CD As String = "PRDCT_CD"
    Public Const GTN_DT As String = "GTN_DT"
    Public Const CLN_CST_NC As String = "CLN_CST_NC"
    Public Const INVCNG_PRTY_ID As String = "INVCNG_PRTY_ID"
    Public Const INVCNG_PRTY_CD As String = "INVCNG_PRTY_CD"
    Public Const INVCNG_PRTY_NM As String = "INVCNG_PRTY_NM"
    Public Const ACTV_BT As String = "ACTV_BT"
    Public Const TTL_SRV_TAX As String = "TTL_SRV_TAX"
    Public Const TTL_EST_INCL_SRV_TAX As String = "TTL_EST_INCL_SRV_TAX"
    Public Const INVC_TYPE As String = "INVC_TYPE"
    Public Const LEAK_TEST As String = "LEAK_TEST"
    Public Const PTI As String = "PTI"
    Public Const SURVEY As String = "SURVEY"
    Public Const TOTALAMOUNT As String = "TOTALAMOUNT"
    Public Const SRVC_PRTNR_ID As String = "SRVC_PRTNR_ID"
    Public Const APPRVL_RFRNC_NO As String = "APPRVL_RFRNC_NO"
    Public Const DRFT_INVC_NO As String = "DRFT_INVC_NO"
    Public Const FNL_INVC_NO As String = "FNL_INVC_NO"
    Public Const CHECKED As String = "CHECKED"
    Public Const APPRVL_RF_NO As String = "APPRVL_RF_NO"
    Public Const RSPNSBLTY_ID As String = "RSPNSBLTY_ID"

    'Data Columns..

    'Table Name: AUDIT_LOG
    Public Const ADT_LG_ID As String = "ADT_LG_ID"
    Public Const ACTVTY_NAM As String = "ACTVTY_NAM"
    Public Const ACTN_VC As String = "ACTN_VC"
    Public Const ACTN_DT As String = "ACTN_DT"
    Public Const OLD_VL As String = "OLD_VL"
    Public Const NEW_VL As String = "NEW_VL"
    Public Const RSN_VC As String = "RSN_VC"
    Public Const MDFD_BY As String = "MDFD_BY"
    Public Const RFRNC_NO As String = "RFRNC_NO"

    'Table Name: REPAIR_ESTIMATE
    Public Const RPR_ESTMT_NO As String = "RPR_ESTMT_NO"
    Public Const RVSN_NO As String = "RVSN_NO"
    Public Const RPR_ESTMT_DT As String = "RPR_ESTMT_DT"
    Public Const ACTVTY_DT As String = "ACTVTY_DT"
    Public Const ACTVTY_NM As String = "ACTVTY_NM"
    Public Const ORGNL_ESTMN_DT As String = "ORGNL_ESTMN_DT"
    Public Const EQPMNT_STTS_ID As String = "EQPMNT_STTS_ID"
    Public Const LBR_RT_NC As String = "LBR_RT_NC"
    Public Const RPR_TYP_ID As String = "RPR_TYP_ID"
    Public Const CRT_OF_CLNLNSS_BT As String = "CRT_OF_CLNLNSS_BT"
    Public Const ESTMTN_TTL_NC As String = "ESTMTN_TTL_NC"
    Public Const SRVYR_NM As String = "SRVYR_NM"
    Public Const OWNR_APPRVL_RF As String = "OWNR_APPRVL_RF"
    Public Const APPRVL_AMNT_NC As String = "APPRVL_AMNT_NC"
    Public Const ORGNL_ESTMTN_AMNT_NC As String = "ORGNL_ESTMTN_AMNT_NC"
    Public Const CSTMR_ESTMTN_TTL_NC As String = "CSTMR_ESTMTN_TTL_NC"
    Public Const CSTMR_APPRVL_AMNT_NC As String = "CSTMR_APPRVL_AMNT_NC"
    Public Const ACTL_MN_HR_NC As String = "ACTL_MN_HR_NC"
    Public Const RMRKS_VC As String = "RMRKS_VC"
    Public Const CRTD_BY As String = "CRTD_BY"
    Public Const CRTD_DT As String = "CRTD_DT"
    Public Const MDFD_DT As String = "MDFD_DT"
    Public Const PRTY_APPRVL_RF As String = "PRTY_APPRVL_RF"
    Public Const RPR_UPDT_APPRVL_RFRNC_NO As String = "RPR_UPDT_APPRVL_RFRNC_NO"

#End Region


End Class
