#Region "CustomerData"
Public Class CustomerData

#Region "Declaration Part.."
    'Data Tables..
    Public Const _CUSTOMER As String = "CUSTOMER"
    Public Const _V_CUSTOMER As String = "V_CUSTOMER"
    Public Const _CUSTOMER_CHARGE_DETAIL As String = "CUSTOMER_CHARGE_DETAIL"
    Public Const _V_CUSTOMER_CHARGE_DETAIL As String = "V_CUSTOMER_CHARGE_DETAIL"
    Public Const _CUSTOMER_STORAGE_DETAIL As String = "CUSTOMER_STORAGE_DETAIL"
    Public Const _CUSTOMER_EDI_SETTING As String = "CUSTOMER_EDI_SETTING"
    Public Const _V_CUSTOMER_EDI_SETTING As String = "V_CUSTOMER_EDI_SETTING"
    Public Const _V_CUSTOMER_EMAIL_SETTING As String = "V_CUSTOMER_EMAIL_SETTING"
    Public Const _CUSTOMER_EMAIL_SETTING As String = "CUSTOMER_EMAIL_SETTING"
    Public Const _ENUM As String = "ENUM"
    Public Const _REPORT_NAME As String = "REPORT_NAME"
    Public Const _SERVICE_PARTNER As String = "SERVICE_PARTNER"
    Public Const _V_CUSTOMER_TRANSPORTATION As String = "V_CUSTOMER_TRANSPORTATION"
    Public Const _CUSTOMER_TRANSPORTATION As String = "CUSTOMER_TRANSPORTATION"
    Public Const _V_CUSTOMER_RENTAL As String = "V_CUSTOMER_RENTAL"
    Public Const _CUSTOMER_RENTAL As String = "CUSTOMER_RENTAL"
    Public Const _V_ROUTE As String = "V_ROUTE"
    Public Const _CUSTOMER_CLEANING_DETAIL As String = "CUSTOMER_CLEANING_DETAIL"

    'Data Columns..

    'Table Name: CUSTOMER
    Public Const CSTMR_ID As String = "CSTMR_ID"
    Public Const CSTMR_CD As String = "CSTMR_CD"
    Public Const CSTMR_NAM As String = "CSTMR_NAM"
    Public Const CSTMR_CRRNCY_ID As String = "CSTMR_CRRNCY_ID"
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
    Public Const HYDR_AMNT_NC As String = "HYDR_AMNT_NC"
    Public Const PNMTC_AMNT_NC As String = "PNMTC_AMNT_NC"
    Public Const LBR_RT_PR_HR_NC As String = "LBR_RT_PR_HR_NC"
    Public Const LK_TST_RT_NC As String = "LK_TST_RT_NC"
    Public Const SRVY_ONHR_OFFHR_RT_NC As String = "SRVY_ONHR_OFFHR_RT_NC"
    Public Const PRDC_TST_TYP_ID As String = "PRDC_TST_TYP_ID"
    Public Const VLDTY_PRD_TST_YRS As String = "VLDTY_PRD_TST_YRS"
    Public Const MIN_HTNG_RT_NC As String = "MIN_HTNG_RT_NC"
    Public Const MIN_HTNG_PRD_NC As String = "MIN_HTNG_PRD_NC"
    Public Const HRLY_CHRG_NC As String = "HRLY_CHRG_NC"
    Public Const CHK_DGT_VLDTN_BT As String = "CHK_DGT_VLDTN_BT"
    Public Const TRNSPRTTN_BT As String = "TRNSPRTTN_BT"
    Public Const RNTL_BT As String = "RNTL_BT"
    Public Const ACTV_BT As String = "ACTV_BT"
    Public Const CRTD_BY As String = "CRTD_BY"
    Public Const CRTD_DT As String = "CRTD_DT"
    Public Const MDFD_BY As String = "MDFD_BY"
    Public Const MDFD_DT As String = "MDFD_DT"
    Public Const DPT_ID As String = "DPT_ID"
    Public Const INVCNG_PRTY_ID As String = "INVCNG_PRTY_ID"

    Public Const EDI_FRMT_CD As String = "EDI_FRMT_CD"
    Public Const TO_EML_ID As String = "TO_EML_ID"
    Public Const GNRTN_FRMT_CD As String = "GNRTN_FRMT_CD"
    Public Const GNRTN_TM As String = "GNRTN_TM"
    Public Const EDI_FRMT As String = "EDI_FRMT"
    Public Const GNRTN_FRMT As String = "GNRTN_FRMT"
    Public Const CSTMR_EDI_STTNG_BIN As String = "CSTMR_EDI_STTNG_BIN"
    Public Const LST_RN As String = "LST_RN"

    Public Const CSTMR_VAT_NO As String = "CSTMR_VAT_NO"
    Public Const AGENT_ID As String = "AGENT_ID"
    Public Const HANDLNG_TX As String = "HANDLNG_TX"
    Public Const STORG_TX As String = "STORG_TX"
    Public Const SERVC_TX As String = "SERVC_TX"

    'Finance Integration
    Public Const LDGR_ID As String = "LDGR_ID"
    Public Const SHELL As String = "SHELL"
    Public Const STUBE As String = "STUBE"


    'Table Name: CUSTOMER_CHARGE_DETAIL
    Public Const CSTMR_CHRG_DTL_ID As String = "CSTMR_CHRG_DTL_ID"
    Public Const EQPMNT_CD_ID As String = "EQPMNT_CD_ID"
    Public Const EQPMNT_TYP_ID As String = "EQPMNT_TYP_ID"
    Public Const HNDLNG_IN_CHRG_NC As String = "HNDLNG_IN_CHRG_NC"
    Public Const HNDLNG_OUT_CHRG_NC As String = "HNDLNG_OUT_CHRG_NC"
    Public Const INSPCTN_CHRGS As String = "INSPCTN_CHRGS"

    'Table Name: CUSTOMER_STORAGE_DETAIL
    Public Const CSTMR_STRG_DTL_ID As String = "CSTMR_STRG_DTL_ID"
    Public Const UP_TO_DYS As String = "UP_TO_DYS"
    Public Const STRG_CHRG_NC As String = "STRG_CHRG_NC"
    Public Const RMRKS_VC As String = "RMRKS_VC"

    'Table Name: CUSTOMER_CLEANING_DETAIL
    Public Const CSTMR_CLNNG_DTL_ID As String = "CSTMR_CLNNG_DTL_ID"
    Public Const UP_TO_CNTNRS As String = "UP_TO_CNTNRS"
    Public Const CLNNG_RT As String = "CLNNG_RT"

    'View Name: V_CUSTOMER
    Public Const BLK_EML_FRMT_CD As String = "BLK_EML_FRMT_CD"
    Public Const CSTMR_CRRNCY_CD As String = "CSTMR_CRRNCY_CD"
    Public Const CNVRT_TO_CRRNCY_CD As String = "CNVRT_TO_CRRNCY_CD"
    Public Const BLLNG_TYP_CD As String = "BLLNG_TYP_CD"
    Public Const PRDC_TST_TYP_CD As String = "PRDC_TST_TYP_CD"
    Public Const DPT_CD As String = "DPT_CD"
    Public Const ISO_CD As String = "ISO_CD"
    Public Const AGNT_CD As String = "AGNT_CD"

    'View Name: V_CUSTOMER_CHARGE_DETAIL
    Public Const EQPMNT_CD_CD As String = "EQPMNT_CD_CD"
    Public Const EQPMNT_TYP_CD As String = "EQPMNT_TYP_CD"

    'Table Name:ENUM
    Public Const ENM_ID As String = "ENM_ID"
    Public Const ENM_CD As String = "ENM_CD"
    Public Const ENM_TYP_ID As String = "ENM_TYP_ID"

    'Table Name: V_CUSTOMER_EMAIL_SETTING
    Public Const CSTMR_EML_STTNG_BIN As String = "CSTMR_EML_STTNG_BIN"
    'Public Const RPRT_ID As String = "RPRT_ID"
    Public Const RPRT_CD As String = "RPRT_CD"
    Public Const TO_EML As String = "TO_EML"
    Public Const CC_EML As String = "CC_EML"
    ' Public Const BCC_EML As String = "BCC_EML"
    Public Const SBJCT_VCR As String = "SBJCT_VCR"
    Public Const NXT_RN_DT_TM As String = "NXT_RN_DT_TM"
    Public Const LST_RN_DT_TM As String = "LST_RN_DT_TM"
    Public Const PRDC_DT As String = "PRDC_DT"

    Public Const PRDC_DY_ID As String = "PRDC_DY_ID"
    Public Const PRDC_DY_CD As String = "PRDC_DY_CD"
    Public Const PRDC_FLTR_ID As String = "PRDC_FLTR_ID"
    Public Const PRDC_FLTR_CD As String = "PRDC_FLTR_CD"
    Public Const PRDC_DT_ID As String = "PRDC_DT_ID"
    Public Const PRDC_DT_CD As String = "PRDC_DT_CD"
    Public Const RPRT_ID As String = "RPRT_ID"

    'Customer Transportation
    Public Const CSTMR_TRNSPRTTN_ID As String = "CSTMR_TRNSPRTTN_ID"
    Public Const RT_ID As String = "RT_ID"
    Public Const RT_CD As String = "RT_CD"
    Public Const PCK_UP_LCTN_CD As String = "PCK_UP_LCTN_CD"
    Public Const DRP_OFF_LCTN_CD As String = "DRP_OFF_LCTN_CD"
    Public Const ACTVTY_CD As String = "ACTVTY_CD"
    Public Const ACTVTY_LCTN_CD As String = "ACTVTY_LCTN_CD"
    Public Const EMPTY_TRP_RT_NC As String = "EMPTY_TRP_RT_NC"
    Public Const FLL_TRP_RT_NC As String = "FLL_TRP_RT_NC"

    'Customer Rental
    Public Const CSTMR_RNTL_ID As String = "CSTMR_RNTL_ID"
    Public Const SPPLR_ID As String = "SPPLR_ID"
    Public Const CNTRCT_RFRNC_NO As String = "CNTRCT_RFRNC_NO"
    Public Const CNTRCT_STRT_DT As String = "CNTRCT_STRT_DT"
    Public Const CNTRCT_END_DT As String = "CNTRCT_END_DT"
    Public Const RNTL_PR_DY As String = "RNTL_PR_DY"
    Public Const SPPLR_RMRKS_VC As String = "SPPLR_RMRKS_VC"
    Public Const HNDLNG_OT As String = "HNDLNG_OT"
    Public Const HNDLNG_IN As String = "HNDLNG_IN"
    Public Const ON_HR_SRVY As String = "ON_HR_SRVY"
    Public Const OFF_HR_SRVY As String = "OFF_HR_SRVY"
    Public Const MN_TNR_DY As String = "MN_TNR_DY"
    Public Const DEPOT_CRRNCY_CD As String = "DEPOT_CRRNCY_CD"
    Public Const EQPMNT_NO As String = "EQPMNT_NO"
    Public Const GTOT_BT As String = "GTOT_BT"
    Public Const XML_BT As String = "XML_BT"
    Public Const FTP_SRVR_URL As String = "FTP_SRVR_URL"
    Public Const FTP_USR_NAM As String = "FTP_USR_NAM"
    Public Const FTP_PSSWRD As String = "FTP_PSSWRD"

#End Region

End Class

#End Region

