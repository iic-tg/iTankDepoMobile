Public Class BulkEmailData


#Region "Declaration Part.."
    'Data Tables..
    Public Const _V_BULKEMAIL_TRACKING As String = "V_BULKEMAIL_TRACKING"
    Public Const _BULK_EMAIL_DETAIL As String = "BULK_EMAIL_DETAIL"
    Public Const _BULK_EMAIL_DETAIL_RESEND As String = "BULK_EMAIL_DETAIL_RESEND"
    Public Const _V_BULK_EMAIL_DETAIL_RESEND As String = "V_BULK_EMAIL_DETAIL_RESEND"
    Public Const _CUSTOMER As String = "CUSTOMER"
    Public Const _BULK_EMAIL As String = "BULK_EMAIL"
    Public Const _REPAIR_ESTIMATE As String = "REPAIR_ESTIMATE"
    Public Const _CLEANING As String = "CLEANING"
    Public Const _BULK_EMAILDETAIL As String = "BULK_EMAILDETAIL"
    Public Const _V_BULK_EMAIL_DETAIL As String = "V_BULK_EMAIL_DETAIL"
    Public Const _DEPOT As String = "DEPOT"
    Public Const _BULK_EMAIL_RESEND_BIT As String = "BULK_EMAIL_RESEND_BIT"
    Public Const _BULK_EMAIL_DETAIL_VIEW As String = "BULK_EMAIL_DETAIL_VIEW"
    Public Const _ACTIVITY_STATUS As String = "ACTIVITY_STATUS"
    Public Const _V_REPAIR_ESTIMATE_DETAIL As String = "V_REPAIR_ESTIMATE_DETAIL"
    Public Const _TRACKING As String = "TRACKING"

    'Data Columns..

    'Table Name: V_TRACKING
    Public Const TRCKNG_ID As String = "TRCKNG_ID"
    Public Const CSTMR_ID As String = "CSTMR_ID"
    Public Const CSTMR_CD As String = "CSTMR_CD"
    Public Const EQPMNT_NO As String = "EQPMNT_NO"
    Public Const ACTVTY_NAM As String = "ACTVTY_NAM"
    Public Const ACTVTY_NM As String = "ACTVTY_NM"
    Public Const EQPMNT_STTS_ID As String = "EQPMNT_STTS_ID"
    Public Const EQPMNT_STTS_CD As String = "EQPMNT_STTS_CD"
    Public Const ACTVTY_NO As String = "ACTVTY_NO"
    Public Const ACTVTY_DT As String = "ACTVTY_DT"
    Public Const ACTVTY_RMRKS As String = "ACTVTY_RMRKS"
    Public Const GI_TRNSCTN_NO As String = "GI_TRNSCTN_NO"
    Public Const INVCNG_PRTY_ID As String = "INVCNG_PRTY_ID"
    Public Const CRTD_BY As String = "CRTD_BY"
    Public Const CRTD_DT As String = "CRTD_DT"
    Public Const CNCLD_BY As String = "CNCLD_BY"
    Public Const CNCLD_DT As String = "CNCLD_DT"
    Public Const ADT_RMRKS As String = "ADT_RMRKS"
    Public Const DPT_ID As String = "DPT_ID"
    Public Const CHECKED As String = "CHECKED"
    Public Const LST_SNT_DT As String = "LST_SNT_DT"
    Public Const GTN_DT As String = "GTN_DT"
    Public Const LST_SNT_BY As String = "LST_SNT_BY"
    Public Const RPRT As String = "RPRT"
    Public Const TMS_SNT As String = "TMS_SNT"
    Public Const CLNNG_GNRTN_BT As String = "CLNNG_GNRTN_BT"
    Public Const CERT_GNRTD_FLG As String = "CERT_GNRTD_FLG"
    Public Const CLNNG_CERT_NO As String = "CLNNG_CERT_NO"
    Public Const ACTVTY_ID As String = "ACTVTY_ID"

    'TableName :BulkEmailDetail
    Public Const TO_EML As String = "TO_EML"
    Public Const CC_EML As String = "CC_EML"
    Public Const BCC_EML As String = "BCC_EML"
    Public Const SBJCT_VC As String = "SBJCT_VC"
    Public Const SNT_DT As String = "SNT_DT"
    Public Const BLK_EML_ID As String = "BLK_EML_ID"
    Public Const ATTCHMNT_PTH As String = "ATTCHMNT_PTH"

    'Table Name :Customer
    Public Const RPRTNG_EML_ID As String = "RPRTNG_EML_ID"
    Public Const INVCNG_EML_ID As String = "INVCNG_EML_ID"
    Public Const RPR_TCH_EML_ID As String = "RPR_TCH_EML_ID"
    Public Const BLK_EML_FRMT_ID As String = "BLK_EML_FRMT_ID"

    'Data Columns..

    'Table Name: BULK_EMAIL
    Public Const FRM_EML As String = "FRM_EML"
    Public Const BDY_VC As String = "BDY_VC"
    Public Const SNT_BT As String = "SNT_BT"
    'Table Name: BULK_EMAIL_DETAIL
    Public Const BLK_EML_DTL_ID As String = "BLK_EML_DTL_ID"
    Public Const AMNT_NC As String = "AMNT_NC"
    Public Const CRRNCY_ID As String = "CRRNCY_ID"
    Public Const STTS_VC As String = "STTS_VC"
    Public Const CSTMR_CRRNCY_ID As String = "CSTMR_CRRNCY_ID"
    Public Const ERR_RMRKS As String = "ERR_RMRKS"
    Public Const ERR_FLG As String = "ERR_FLG"

    'Table Name:RepairEstimate
    Public Const ESTMTN_TTL_NC As String = "ESTMTN_TTL_NC"
    Public Const RPR_ESTMT_ID As String = "RPR_ESTMT_ID"

    'Table Name:Cleaning
    Public Const CLNNG_ID As String = "CLNNG_ID"
    Public Const CLNNG_RT As String = "CLNNG_RT"

    'Table Name:DEPOT
    Public Const BNK_TYP_ID As String = "BNK_TYP_ID"
    Public Const RFRNC_NO As String = "RFRNC_NO"
    Public Const GI_RF_NO As String = "GI_RF_NO"
    Public Const CRRNCY_CD As String = "CRRNCY_CD"

    Public Const RSND_BT As String = "RSND_BT"
    Public Const TTL_CST_NC As String = "TTL_CST_NC"
    Public Const SRVC_PRTNR_ID As String = "SRVC_PRTNR_ID"
    Public Const SRVC_PRTNR_CD As String = "SRVC_PRTNR_CD"
    Public Const RSPNSBLTY_ID As String = "RSPNSBLTY_ID"
    Public Const SRVC_PRTNR_TYP_CD As String = "SRVC_PRTNR_TYP_CD"

    'Table Name: Agent
    Public Const AGNT_ID As String = "AGNT_ID"

#End Region

End Class

