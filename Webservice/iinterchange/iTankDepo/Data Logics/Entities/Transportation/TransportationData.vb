Public Class TransportationData
#Region "Declaration Part.."
    'Data Tables..
    Public Const _TRANSPORTATION As String = "TRANSPORTATION"
    Public Const _TRANSPORTATION_DETAIL As String = "TRANSPORTATION_DETAIL"
    Public Const _TRANSPORTATION_DETAIL_RATE As String = "TRANSPORTATION_DETAIL_RATE"
    Public Const _TRANSPORTATION_INVOICE As String = "TRANSPORTATION_INVOICE"
    Public Const _CUSTOMER As String = "CUSTOMER"
    Public Const _ROUTE As String = "ROUTE"
    Public Const _AUDIT_LOG As String = "AUDIT_LOG"
    Public Const _CUSTOMER_TRANSPORTATION As String = "CUSTOMER_TRANSPORTATION"
    Public Const _V_TRANSPORTATION As String = "V_TRANSPORTATION"
    Public Const _V_TRANSPORTATION_DETAIL As String = "V_TRANSPORTATION_DETAIL"
    Public Const _V_ROUTE As String = "V_ROUTE"
    Public Const _V_CUSTOMER As String = "V_CUSTOMER"
    Public Const _V_TRANSPORTATION_DETAIL_RATE As String = "V_TRANSPORTATION_DETAIL_RATE"
    Public Const _V_ADDITIONAL_CHARGE_RATE As String = "V_ADDITIONAL_CHARGE_RATE"
    Public Const _TRANSPORTATION_CHARGE As String = "TRANSPORTATION_CHARGE"
    Public Const _V_TRANSPORTATION_CHARGE As String = "V_TRANSPORTATION_CHARGE"
    Public Const _V_CUSTOMER_TRANSPORTATION As String = "V_CUSTOMER_TRANSPORTATION"
    Public Const _V_TRANSPORTER_ROUTE_DETAIL As String = "V_TRANSPORTER_ROUTE_DETAIL"
    Public Const _V_TRANSPORTATION_TRIP As String = "V_TRANSPORTATION_TRIP"

    'Data Columns..

    'Table Name: TRANSPORTATION
    Public Const TRNSPRTTN_ID As String = "TRNSPRTTN_ID"
    Public Const CSTMR_ID As String = "CSTMR_ID"
    Public Const RT_ID As String = "RT_ID"
    Public Const TRNSPRTTN_STTS_ID As String = "TRNSPRTTN_STTS_ID"
    Public Const RQST_NO As String = "RQST_NO"
    Public Const RQST_DT As String = "RQST_DT"
    Public Const RMRKS_VC As String = "RMRKS_VC"
    Public Const MDFD_BY As String = "MDFD_BY"
    Public Const MDFD_DT As String = "MDFD_DT"
    Public Const CRTD_BY As String = "CRTD_BY"
    Public Const CRTD_DT As String = "CRTD_DT"
    Public Const DPT_ID As String = "DPT_ID"
    Public Const NO_OF_TRPS As String = "NO_OF_TRPS"

    'View Name: V_TRANSPORTATION
    Public Const CSTMR_CD As String = "CSTMR_CD"
    Public Const TRNSPRTTN_STTS_CD As String = "TRNSPRTTN_STTS_CD"
    Public Const RT_CD As String = "RT_CD"
    Public Const DPT_CD As String = "DPT_CD"
    Public Const CSTMR_CRRNCY_CD As String = "CSTMR_CRRNCY_CD"

    'Table Name: TRANSPORTATION_DETAIL
    Public Const TRNSPRTTN_DTL_ID As String = "TRNSPRTTN_DTL_ID"
    Public Const EQPMNT_NO As String = "EQPMNT_NO"
    Public Const EQPMNT_TYP_ID As String = "EQPMNT_TYP_ID"
    Public Const EQPMNT_TYP_CD As String = "EQPMNT_TYP_CD"
    Public Const EQPMNT_STT_ID As String = "EQPMNT_STT_ID"
    Public Const EQPMNT_STT_CD As String = "EQPMNT_STT_CD"
    Public Const EMPTY_SNGL_ID As String = "EMPTY_SNGL_ID"
    Public Const EMPTY_SNGL_CD As String = "EMPTY_SNGL_CD"
    Public Const CSTMR_RF_NO As String = "CSTMR_RF_NO"
    Public Const PRDCT_ID As String = "PRDCT_ID"
    Public Const PRDCT_CD As String = "PRDCT_CD"
    Public Const TTL_RT_NC As String = "TTL_RT_NC"
    Public Const JB_STRT_DT As String = "JB_STRT_DT"
    Public Const JB_END_DT As String = "JB_END_DT"
    Public Const CHK_BT As String = "CHK_BT"
    Public Const EXCHNG_RT_PR_UNT_NC As String = "EXCHNG_RT_PR_UNT_NC"
    Public Const SPPLR_RT_NC As String = "SPPLR_RT_NC"

    'Table Name: TRANSPORTATION_DETAIL_RATE
    Public Const TRNSPRTTN_DTL_RT_ID As String = "TRNSPRTTN_DTL_RT_ID"
    Public Const ADDTNL_CHRG_RT_ID As String = "ADDTNL_CHRG_RT_ID"
    Public Const ADDTNL_CHRG_RT_NC As String = "ADDTNL_CHRG_RT_NC"
    Public Const PRDCT_DSCRPTN_VC As String = "PRDCT_DSCRPTN_VC"
    'Table Name: TRANSPORTATION_INVOICE
    Public Const TRNSPRTTN_INVC_ID As String = "TRNSPRTTN_INVC_ID"
    Public Const INVC_NO As String = "INVC_NO"
    Public Const INVC_DT As String = "INVC_DT"
    Public Const EVNT_DT As String = "EVNT_DT"
    Public Const TRP_CST_NC As String = "TRP_CST_NC"
    Public Const WGHTMNT As String = "WGHTMNT"
    Public Const TKN As String = "TKN"
    Public Const RCHRGBL_CST_NC As String = "RCHRGBL_CST_NC"
    Public Const FNNC_CHRG_NC As String = "FNNC_CHRG_NC"
    Public Const DTNTN As String = "DTNTN"
    Public Const OTHR_CHRG_NC As String = "OTHR_CHRG_NC"

    'Table Name: V_ROUTE
    Public Const RT_DSCRPTN_VC As String = "RT_DSCRPTN_VC"
    Public Const PCK_UP_LCTN_ID As String = "PCK_UP_LCTN_ID"
    Public Const PCK_UP_LCTN_CD As String = "PCK_UP_LCTN_CD"
    Public Const DRP_OFF_LCTN_ID As String = "DRP_OFF_LCTN_ID"
    Public Const DRP_OFF_LCTN_CD As String = "DRP_OFF_LCTN_CD"
    Public Const ACTVTY_ID As String = "ACTVTY_ID"
    Public Const ACTVTY_CD As String = "ACTVTY_CD"
    Public Const ACTVTY_LCTN_ID As String = "ACTVTY_LCTN_ID"
    Public Const ACTVTY_LCTN_CD As String = "ACTVTY_LCTN_CD"
    Public Const EMPTY_TRP_RT_NC As String = "EMPTY_TRP_RT_NC"
    Public Const FLL_TRP_RT_NC As String = "FLL_TRP_RT_NC"
    Public Const ACTV_BT As String = "ACTV_BT"

    'Table Name: V_ADDITIONAL_CHARGE_RATE
    Public Const OPRTN_TYP_ID As String = "OPRTN_TYP_ID"
    Public Const OPRTN_TYP_CD As String = "OPRTN_TYP_CD"
    Public Const ADDTNL_CHRG_RT_CD As String = "ADDTNL_CHRG_RT_CD"
    Public Const ADDTNL_CHRG_RT_DSCRPTN_VC As String = "ADDTNL_CHRG_RT_DSCRPTN_VC"
    Public Const RT_NC As String = "RT_NC"
    Public Const DFLT_BT As String = "DFLT_BT"

    'Table Name: Enum
    Public Const ENM_ID As String = "ENM_ID"
    Public Const ENM_CD As String = "ENM_CD"

    'Table Name: TRANSPORTATION_CHARGE
    Public Const TRNSPRTTN_CHRG_ID As String = "TRNSPRTTN_CHRG_ID"
    Public Const DRFT_INVC_NO As String = "DRFT_INVC_NO"
    Public Const FNL_INVC_NO As String = "FNL_INVC_NO"
    Public Const TRP_RT_NC As String = "TRP_RT_NC"
    Public Const BLLNG_FLG As String = "BLLNG_FLG"

    'Table Name: V_AUDIT
    Public Const ADT_LG_ID As String = "ADT_LG_ID"
    Public Const RFRNC_NO As String = "RFRNC_NO"
    Public Const ACTVTY_NAM As String = "ACTVTY_NAM"
    Public Const ACTN_VC As String = "ACTN_VC"
    Public Const CNCLD_BY As String = "CNCLD_BY"
    Public Const CNCLD_DT As String = "CNCLD_DT"
    Public Const ADT_RMRKS As String = "ADT_RMRKS"
    Public Const GI_TRNSCTN_NO As String = "GI_TRNSCTN_NO"
    Public Const HDR_NM As String = "HDR_NM"
    Public Const OLD_VL As String = "OLD_VL"
    Public Const NEW_VL As String = "NEW_VL"
    Public Const ACTN_DT As String = "ACTN_DT"
    Public Const RSN_VC As String = "RSN_VC"
    Public Const CHK_DGT_VLDTN_BT As String = "CHK_DGT_VLDTN_BT"

    'Table Name: TRANSPORTER
    Public Const TRNSPRTR_ID As String = "TRNSPRTR_ID"
    Public Const TRNSPRTR_CD As String = "TRNSPRTR_CD"
    Public Const EMPTY_TRP_CSTMR_RT_NC As String = "EMPTY_TRP_CSTMR_RT_NC"
    Public Const FLL_TRP_CSTMR_RT_NC As String = "FLL_TRP_CSTMR_RT_NC"
    Public Const TRNSPRTR_TRP_RT_NC As String = "TRNSPRTR_TRP_RT_NC"

#End Region
End Class
