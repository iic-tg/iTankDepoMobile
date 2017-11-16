﻿Public Class HeatingData

#Region "Declaration Part.."
    'Data Tables..
    Public Const _V_GATEIN As String = "V_GATEIN"
    Public Const _V_HEATING_CHARGE As String = "V_HEATING_CHARGE"
    Public Const _CUSTOMER As String = "CUSTOMER"
    Public Const _HEATING_CHARGE As String = "HEATING_CHARGE"
    Public Const _HANDLING_CHARGE As String = "HANDLING_CHARGE"
    Public Const _TRACKING As String = "TRACKING"
    'Data Columns..

    
    'Table Name: V_HEATING_CHARGE
    Public Const HTNG_ID As String = "HTNG_ID"
    Public Const HTNG_CD As String = "HTNG_CD"
    Public Const EQPMNT_NO As String = "EQPMNT_NO"
    Public Const EQPMNT_TYP_ID As String = "EQPMNT_TYP_ID"
    Public Const EQPMNT_TYP_CD As String = "EQPMNT_TYP_CD"
    Public Const CSTMR_ID As String = "CSTMR_ID"
    Public Const CSTMR_CD As String = "CSTMR_CD"
    Public Const HTNG_STRT_DT As String = "HTNG_STRT_DT"
    Public Const HTNG_STRT_TM As String = "HTNG_STRT_TM"
    Public Const HTNG_END_DT As String = "HTNG_END_DT"
    Public Const HTNG_END_TM As String = "HTNG_END_TM"
    Public Const HTNG_TMPRTR As String = "HTNG_TMPRTR"
    Public Const TTL_HTN_PRD As String = "TTL_HTN_PRD"
    Public Const MIN_HTNG_RT_NC As String = "MIN_HTNG_RT_NC"
    Public Const HRLY_CHRG_NC As String = "HRLY_CHRG_NC"
    Public Const TTL_RT_NC As String = "TTL_RT_NC"
    Public Const GI_TRNSCTN_NO As String = "GI_TRNSCTN_NO"
    Public Const DPT_ID As String = "DPT_ID"
    Public Const CRTD_BY As String = "CRTD_BY"
    Public Const CRTD_DT As String = "CRTD_DT"
    Public Const MDFD_BY As String = "MDFD_BY"
    Public Const MDFD_DT As String = "MDFD_DT"
    Public Const MIN_HTNG_PRD_NC As String = "MIN_HTNG_PRD_NC"
    'Table Name: V_GATEIN
    Public Const GTN_ID As String = "GTN_ID"
    Public Const GTN_CD As String = "GTN_CD"
    Public Const CSTMR_NAM As String = "CSTMR_NAM"
    Public Const EQPMNT_CD_ID As String = "EQPMNT_CD_ID"
    Public Const EQPMNT_CD_CD As String = "EQPMNT_CD_CD"
    Public Const EQPMNT_STTS_ID As String = "EQPMNT_STTS_ID"
    Public Const EQPMNT_STTS_CD As String = "EQPMNT_STTS_CD"
    Public Const EQPMNT_STTS_DSCRPTN_VC As String = "EQPMNT_STTS_DSCRPTN_VC"
    Public Const YRD_LCTN As String = "YRD_LCTN"
    Public Const GTN_DT As String = "GTN_DT"
    Public Const GTN_TM As String = "GTN_TM"
    Public Const PRDCT_ID As String = "PRDCT_ID"
    Public Const PRDCT_CD As String = "PRDCT_CD"
    Public Const EIR_NO As String = "EIR_NO"
    Public Const VHCL_NO As String = "VHCL_NO"
    Public Const TRNSPRTR_CD As String = "TRNSPRTR_CD"
    Public Const HTNG_BT As String = "HTNG_BT"
    Public Const RMRKS_VC As String = "RMRKS_VC"
    Public Const GTOT_BT As String = "GTOT_BT"
  

    'Table Name: CUSTOMER
 
    Public Const CSTMR_CRRNCY_ID As String = "CSTMR_CRRNCY_ID"
    Public Const CNVRT_TO_CRRNCY_ID As String = "CNVRT_TO_CRRNCY_ID"
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
 
    Public Const CHK_DGT_VLDTN_BT As String = "CHK_DGT_VLDTN_BT"
    Public Const TRNSPRTTN_BT As String = "TRNSPRTTN_BT"
    Public Const RNTL_BT As String = "RNTL_BT"
   
    Public Const ACTV_BT As String = "ACTV_BT"
    Public Const CHECKED As String = "CHECKED"
    Public Const BLLNG_FLG As String = "BLLNG_FLG"

    Public Const ACTVTY_DT As String = "ACTVTY_DT"
    Public Const ACTVTY_NAM As String = "ACTVTY_NAM"
    Public Const CSTMR_CRRNCY_CD As String = "CSTMR_CRRNCY_CD"
    Public Const PRDCT_DSCRPTN_VC As String = "PRDCT_DSCRPTN_VC"
#End Region

End Class
