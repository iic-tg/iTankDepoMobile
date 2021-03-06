#Region "DARData"
Public Class DARData

#Region "Declaration Part.."
    'Data Tables..
    Public Const _EQUIPMENT_ACTIVITY As String = "EQUIPMENT_ACTIVITY"
    Public Const _V_EQPMNT_ACTVTY As String = "V_EQPMNT_ACTVTY"
    Public Const _V_EQPMNT_ACTVTY_STTS As String = "V_EQPMNT_ACTVTY_STTS"
    Public Const _V_INVENTORY As String = "V_INVENTORY"
    Public Const _V_DEPOT As String = "V_DEPOT"
    Public Const _CUSTOMER As String = "CUSTOMER"
    Public Const _V_CUSTOMER_EMAIL_SETTING As String = "V_CUSTOMER_EMAIL_SETTING"
    Public Const _V_ACTIVITY_STATUS As String = "V_ACTIVITY_STATUS"
    Public Const _ACTIVITY_STATUS As String = "ACTIVITY_STATUS"
    Public Const _V_DAR_ACTIVITY_STATUS As String = "V_DAR_ACTIVITY_STATUS"
    Public Const _V_DAR_SUMMARY As String = "V_DAR_SUMMARY"
    Public Const _CUSTOMER_EMAIL_SETTING As String = "CUSTOMER_EMAIL_SETTING"
    Public Const _V_EMAIL_SETTING As String = "V_EMAIL_SETTING"
    'Data Columns..

    'Table Name : Depot
    Public Const CNTCT_PRSN_NAM As String = "CNTCT_PRSN_NAM"
    Public Const ADDRSS_LN1_VC As String = "ADDRSS_LN1_VC"
    Public Const ADDRSS_LN2_VC As String = "ADDRSS_LN2_VC"
    Public Const ADDRSS_LN3_VC As String = "ADDRSS_LN3_VC"
    Public Const CNTRY_NAM As String = "CNTRY_NAM"
    Public Const CTY_NAM As String = "CTY_NAM"
    Public Const ZP_CD As String = "ZP_CD"
    Public Const VT_NO As String = "VT_NO"
    Public Const EML_ID As String = "EML_ID"
    Public Const PHN_NO As String = "PHN_NO"
    Public Const FX_NO As String = "FX_NO"
    Public Const CRRNCY_ID As String = "CRRNCY_ID"
    Public Const CMPNY_LG_PTH As String = "CMPNY_LG_PTH"
    Public Const MDFD_BY As String = "MDFD_BY"
    Public Const MDFD_DT As String = "MDFD_DT"

    'Table Name: V_CUSTOMER_EMAIL_SETTING
    Public Const CSTMR_EML_STTNG_BIN As String = "CSTMR_EML_STTNG_BIN"
    Public Const RPRT_ID As String = "RPRT_ID"
    Public Const RPRT_CD As String = "RPRT_CD"
    Public Const GNRTN_TM As String = "GNRTN_TM"
    Public Const TO_EML As String = "TO_EML"
    Public Const CC_EML As String = "CC_EML"
    Public Const SBJCT_VCR As String = "SBJCT_VCR"
    Public Const ACTV_BT As String = "ACTV_BT"
    Public Const PRDC_FLTR_ID As String = "PRDC_FLTR_ID"
    Public Const PRDC_FLTR_CD As String = "PRDC_FLTR_CD"
    Public Const PRDC_DY_ID As String = "PRDC_DY_ID"
    Public Const PRDC_DY_CD As String = "PRDC_DY_CD"
    Public Const PRDC_DT_ID As String = "PRDC_DT_ID"
    Public Const PRDC_DT_CD As String = "PRDC_DT_CD"
    Public Const NXT_RN_DT_TM As String = "NXT_RN_DT_TM"
    Public Const LST_RN_DT_TM As String = "LST_RN_DT_TM"


    'Table Name: V_DAR_ACTIVITY_STATUS
    Public Const ACTVTY_STTS_ID As String = "ACTVTY_STTS_ID"
    Public Const CSTMR_ID As String = "CSTMR_ID"
    Public Const CSTMR_CD As String = "CSTMR_CD"
    Public Const CSTMR_NAM As String = "CSTMR_NAM"
    Public Const EQPMNT_NO As String = "EQPMNT_NO"
    Public Const EQPMNT_TYP_ID As String = "EQPMNT_TYP_ID"
    Public Const EQPMNT_TYP_CD As String = "EQPMNT_TYP_CD"
    Public Const EQPMNT_CD_ID As String = "EQPMNT_CD_ID"
    Public Const EQPMNT_CD_CD As String = "EQPMNT_CD_CD"
    Public Const GTN_DT As String = "GTN_DT"
    Public Const GTOT_DT As String = "GTOT_DT"
    Public Const PRDCT_ID As String = "PRDCT_ID"
    Public Const PRDCT_CD As String = "PRDCT_CD"
    Public Const PRDCT_DSCRPTN_VC As String = "PRDCT_DSCRPTN_VC"
    Public Const CLNNG_DT As String = "CLNNG_DT"
    Public Const EQPMNT_STTS_ID As String = "EQPMNT_STTS_ID"
    Public Const EQPMNT_STTS_CD As String = "EQPMNT_STTS_CD"
    Public Const EQPMNT_STTS_DSCRPTN_VC As String = "EQPMNT_STTS_DSCRPTN_VC"
    Public Const ACTVTY_NAM As String = "ACTVTY_NAM"
    Public Const ACTVTY_DT As String = "ACTVTY_DT"
    Public Const GI_TRNSCTN_NO As String = "GI_TRNSCTN_NO"
    Public Const INVC_GNRT_BT As String = "INVC_GNRT_BT"
    Public Const GI_RF_NO As String = "GI_RF_NO"
    Public Const VHCL_NO As String = "VHCL_NO"
    Public Const TRNSPRTR_CD As String = "TRNSPRTR_CD"
    Public Const INSTRCTNS_VC As String = "INSTRCTNS_VC"
    Public Const DPT_ID As String = "DPT_ID"
    Public Const DPT_CD As String = "DPT_CD"
    Public Const DPT_NAM As String = "DPT_NAM"
    Public Const YRD_LCTN As String = "YRD_LCTN"
    Public Const CERT_GNRTD_FLG As String = "CERT_GNRTD_FLG"
    Public Const RPR_CMPLTN_DT As String = "RPR_CMPLTN_DT"
    Public Const CHMCL_NM As String = "CHMCL_NM"
    Public Const CLNNG_CERT_NO As String = "CLNNG_CERT_NO"
    Public Const CRT_GNRTD_FLG As String = "CRT_GNRTD_FLG"
    Public Const INSPCTN_DT As String = "INSPCTN_DT"
    Public Const RMRKS_VC As String = "RMRKS_VC"
    Public Const NEXT_TEST_DATE As String = "NEXT_TEST_DATE"
    Public Const ASRMRKS As String = "ASRMRKS"
    Public Const RMRKS As String = "RMRKS"
    Public Const NM As String = "NM"
    Public Const NY As String = "NY"
    Public Const CM As String = "CM"
    Public Const CY As String = "CY"
    Public Const REMARKS As String = "REMARKS"
    Public Const NEXT_TEST_TYPE As String = "NEXT_TEST_TYPE"


    'Table Name: V_DAR_CUSTOMER_SUMMARY
    Public Const IND As String = "IND"
    Public Const PHL As String = "PHL"
    Public Const ACN As String = "ACN"
    Public Const AWECLN As String = "AWECLN"
    Public Const AWE As String = "AWE"
    Public Const AAR As String = "AAR"
    Public Const AUR As String = "AUR"
    Public Const AVLCLN As String = "AVLCLN"
    Public Const AVLCLNRPC As String = "AVLCLNRPC"
    Public Const RPC As String = "RPC"
    Public Const STO As String = "STO"
    Public Const AVL As String = "AVL"
    Public Const OUT As String = "OUT"
    Public Const SRV As String = "SRV"
    Public Const TOTAL As String = "TOTAL"
    Public Const ASR As String = "ASR"
#End Region

End Class

#End Region

