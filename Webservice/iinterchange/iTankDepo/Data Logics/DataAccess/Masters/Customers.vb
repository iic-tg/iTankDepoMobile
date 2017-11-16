Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

Public Class Customers

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const V_CustomerSelectQueryByCstmr_Id As String = "SELECT CSTMR_CD,CSTMR_NAM,PHN_NO,BLLNG_TYP_ID,BLLNG_TYP_CD,MDFD_BY,MDFD_DT,ACTV_BT,CSTMR_CRRNCY_ID,CNTCT_PRSN_NAM,ZP_CD,FX_NO,LBR_RT_PR_HR_NC,CRTD_BY,CRTD_DT,DPT_ID,CSTMR_ID,CSTMR_CRRNCY_CD,XML_BT,CSTMR_VAT_NO,AGENT_ID,AGNT_CD,AGENT_ID  FROM V_CUSTOMER WHERE CSTMR_ID=@CSTMR_ID and DPT_ID=@DPT_ID"
    'Private Const CustomerInsertQuery As String = "INSERT INTO CUSTOMER(CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,CNTCT_PRSN_NAM,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,RPR_TCH_EML_ID,BLK_EML_FRMT_ID,HYDR_AMNT_NC,PNMTC_AMNT_NC,LBR_RT_PR_HR_NC,LK_TST_RT_NC,SRVY_ONHR_OFFHR_RT_NC,PRDC_TST_TYP_ID,VLDTY_PRD_TST_YRS,MIN_HTNG_RT_NC,MIN_HTNG_PRD_NC,HRLY_CHRG_NC,CHK_DGT_VLDTN_BT,TRNSPRTTN_BT,RNTL_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,XML_BT,FTP_SRVR_URL,FTP_USR_NAM,FTP_PSSWRD,ISO_CD)VALUES(@CSTMR_ID,@CSTMR_CD,@CSTMR_NAM,@CSTMR_CRRNCY_ID,@BLLNG_TYP_ID,@CNTCT_PRSN_NAM,@CNTCT_ADDRSS,@BLLNG_ADDRSS,@ZP_CD,@PHN_NO,@FX_NO,@RPRTNG_EML_ID,@INVCNG_EML_ID,@RPR_TCH_EML_ID,@BLK_EML_FRMT_ID,@HYDR_AMNT_NC,@PNMTC_AMNT_NC,@LBR_RT_PR_HR_NC,@LK_TST_RT_NC,@SRVY_ONHR_OFFHR_RT_NC,@PRDC_TST_TYP_ID,@VLDTY_PRD_TST_YRS,@MIN_HTNG_RT_NC,@MIN_HTNG_PRD_NC,@HRLY_CHRG_NC,@CHK_DGT_VLDTN_BT,@TRNSPRTTN_BT,@RNTL_BT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID,@XML_BT,@FTP_SRVR_URL,@FTP_USR_NAM,@FTP_PSSWRD,@ISO_CD)"
    Private Const CustomerInsertQuery As String = "INSERT INTO CUSTOMER(CSTMR_ID,CSTMR_CD,CSTMR_NAM,CSTMR_CRRNCY_ID,BLLNG_TYP_ID,CNTCT_PRSN_NAM,CNTCT_ADDRSS,BLLNG_ADDRSS,ZP_CD,PHN_NO,FX_NO,RPRTNG_EML_ID,INVCNG_EML_ID,RPR_TCH_EML_ID,BLK_EML_FRMT_ID,HYDR_AMNT_NC,PNMTC_AMNT_NC,LBR_RT_PR_HR_NC,LK_TST_RT_NC,SRVY_ONHR_OFFHR_RT_NC,PRDC_TST_TYP_ID,VLDTY_PRD_TST_YRS,MIN_HTNG_RT_NC,MIN_HTNG_PRD_NC,HRLY_CHRG_NC,CHK_DGT_VLDTN_BT,TRNSPRTTN_BT,RNTL_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,XML_BT,FTP_SRVR_URL,FTP_USR_NAM,FTP_PSSWRD,ISO_CD,LDGR_ID,SHELL,STUBE,CSTMR_VAT_NO,AGENT_ID,HANDLNG_TX,STORG_TX,SERVC_TX)VALUES(@CSTMR_ID,@CSTMR_CD,@CSTMR_NAM,@CSTMR_CRRNCY_ID,@BLLNG_TYP_ID,@CNTCT_PRSN_NAM,@CNTCT_ADDRSS,@BLLNG_ADDRSS,@ZP_CD,@PHN_NO,@FX_NO,@RPRTNG_EML_ID,@INVCNG_EML_ID,@RPR_TCH_EML_ID,@BLK_EML_FRMT_ID,@HYDR_AMNT_NC,@PNMTC_AMNT_NC,@LBR_RT_PR_HR_NC,@LK_TST_RT_NC,@SRVY_ONHR_OFFHR_RT_NC,@PRDC_TST_TYP_ID,@VLDTY_PRD_TST_YRS,@MIN_HTNG_RT_NC,@MIN_HTNG_PRD_NC,@HRLY_CHRG_NC,@CHK_DGT_VLDTN_BT,@TRNSPRTTN_BT,@RNTL_BT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID,@XML_BT,@FTP_SRVR_URL,@FTP_USR_NAM,@FTP_PSSWRD,@ISO_CD,@LDGR_ID,@SHELL,@STUBE,@CSTMR_VAT_NO,@AGENT_ID,@HANDLNG_TX,@STORG_TX,@SERVC_TX)"
    Private Const CustomerUpdateQuery As String = "UPDATE Customer SET CSTMR_ID=@CSTMR_ID, CSTMR_CD=@CSTMR_CD, CSTMR_NAM=@CSTMR_NAM, CSTMR_CRRNCY_ID=@CSTMR_CRRNCY_ID,  BLLNG_TYP_ID=@BLLNG_TYP_ID, CNTCT_PRSN_NAM=@CNTCT_PRSN_NAM, CNTCT_ADDRSS=@CNTCT_ADDRSS, BLLNG_ADDRSS=@BLLNG_ADDRSS, ZP_CD=@ZP_CD, PHN_NO=@PHN_NO, FX_NO=@FX_NO, RPRTNG_EML_ID=@RPRTNG_EML_ID, INVCNG_EML_ID=@INVCNG_EML_ID, RPR_TCH_EML_ID=@RPR_TCH_EML_ID, BLK_EML_FRMT_ID=@BLK_EML_FRMT_ID, HYDR_AMNT_NC=@HYDR_AMNT_NC, PNMTC_AMNT_NC=@PNMTC_AMNT_NC, LBR_RT_PR_HR_NC=@LBR_RT_PR_HR_NC, LK_TST_RT_NC=@LK_TST_RT_NC, SRVY_ONHR_OFFHR_RT_NC=@SRVY_ONHR_OFFHR_RT_NC, PRDC_TST_TYP_ID=@PRDC_TST_TYP_ID, VLDTY_PRD_TST_YRS=@VLDTY_PRD_TST_YRS, MIN_HTNG_RT_NC=@MIN_HTNG_RT_NC, MIN_HTNG_PRD_NC=@MIN_HTNG_PRD_NC, HRLY_CHRG_NC=@HRLY_CHRG_NC, CHK_DGT_VLDTN_BT=@CHK_DGT_VLDTN_BT, TRNSPRTTN_BT=@TRNSPRTTN_BT, RNTL_BT=@RNTL_BT, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID,XML_BT=@XML_BT,FTP_SRVR_URL=@FTP_SRVR_URL,FTP_USR_NAM=@FTP_USR_NAM,FTP_PSSWRD=@FTP_PSSWRD,ISO_CD=@ISO_CD, SHELL=@SHELL, STUBE=@STUBE, CSTMR_VAT_NO=@CSTMR_VAT_NO, AGENT_ID=@AGENT_ID,HANDLNG_TX=@HANDLNG_TX,STORG_TX=@STORG_TX,SERVC_TX=@SERVC_TX WHERE CSTMR_ID=@CSTMR_ID"
    Private Const CustomerUpdateQueryWithLedger As String = "UPDATE Customer SET CSTMR_ID=@CSTMR_ID, CSTMR_CD=@CSTMR_CD, CSTMR_NAM=@CSTMR_NAM, CSTMR_CRRNCY_ID=@CSTMR_CRRNCY_ID,  BLLNG_TYP_ID=@BLLNG_TYP_ID, CNTCT_PRSN_NAM=@CNTCT_PRSN_NAM, CNTCT_ADDRSS=@CNTCT_ADDRSS, BLLNG_ADDRSS=@BLLNG_ADDRSS, ZP_CD=@ZP_CD, PHN_NO=@PHN_NO, FX_NO=@FX_NO, RPRTNG_EML_ID=@RPRTNG_EML_ID, INVCNG_EML_ID=@INVCNG_EML_ID, RPR_TCH_EML_ID=@RPR_TCH_EML_ID, BLK_EML_FRMT_ID=@BLK_EML_FRMT_ID, HYDR_AMNT_NC=@HYDR_AMNT_NC, PNMTC_AMNT_NC=@PNMTC_AMNT_NC, LBR_RT_PR_HR_NC=@LBR_RT_PR_HR_NC, LK_TST_RT_NC=@LK_TST_RT_NC, SRVY_ONHR_OFFHR_RT_NC=@SRVY_ONHR_OFFHR_RT_NC, PRDC_TST_TYP_ID=@PRDC_TST_TYP_ID, VLDTY_PRD_TST_YRS=@VLDTY_PRD_TST_YRS, MIN_HTNG_RT_NC=@MIN_HTNG_RT_NC, MIN_HTNG_PRD_NC=@MIN_HTNG_PRD_NC, HRLY_CHRG_NC=@HRLY_CHRG_NC, CHK_DGT_VLDTN_BT=@CHK_DGT_VLDTN_BT, TRNSPRTTN_BT=@TRNSPRTTN_BT, RNTL_BT=@RNTL_BT, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID,XML_BT=@XML_BT,FTP_SRVR_URL=@FTP_SRVR_URL,FTP_USR_NAM=@FTP_USR_NAM,FTP_PSSWRD=@FTP_PSSWRD,ISO_CD=@ISO_CD,LDGR_ID=@LDGR_ID,SHELL=@SHELL, STUBE=@STUBE WHERE CSTMR_ID=@CSTMR_ID"
    Private Const Customer_RateInsertQuery As String = "INSERT INTO CUSTOMER_RATE(CSTMR_RT_ID,CSTMR_ID,EQPMNT_CD_ID,EQPMNT_TYP_ID,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,STRG_CHRGS_PR_DY_NC,FR_DYS,ACTV_BT,HNDLNG_CHRG_NC,WSHNG_CHRG_NC)VALUES(@CSTMR_RT_ID,@CSTMR_ID,@EQPMNT_CD_ID,@EQPMNT_TYP_ID,@HNDLNG_IN_CHRG_NC,@HNDLNG_OUT_CHRG_NC,@STRG_CHRGS_PR_DY_NC,@FR_DYS,@ACTV_BT,@HNDLNG_CHRG_NC,@WSHNG_CHRG_NC)"
    Private Const Customer_RateUpdateQuery As String = "UPDATE CUSTOMER_RATE SET CSTMR_RT_ID=@CSTMR_RT_ID, CSTMR_ID=@CSTMR_ID, EQPMNT_CD_ID=@EQPMNT_CD_ID, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, HNDLNG_IN_CHRG_NC=@HNDLNG_IN_CHRG_NC, HNDLNG_OUT_CHRG_NC=@HNDLNG_OUT_CHRG_NC, STRG_CHRGS_PR_DY_NC=@STRG_CHRGS_PR_DY_NC, FR_DYS=@FR_DYS, ACTV_BT=@ACTV_BT, HNDLNG_CHRG_NC=@HNDLNG_CHRG_NC, WSHNG_CHRG_NC=@WSHNG_CHRG_NC WHERE CSTMR_RT_ID=@CSTMR_RT_ID"
    Private Const V_Customer_RateSelectQueryByCstmr_id As String = "SELECT CSTMR_RT_ID,CSTMR_ID,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,STRG_CHRGS_PR_DY_NC,FR_DYS,ACTV_BT,HNDLNG_CHRG_NC,WSHNG_CHRG_NC FROM V_CUSTOMER_RATE WHERE CSTMR_ID=@CSTMR_ID"
    Private Const Customer_RateSelectQuery As String = "SELECT CSTMR_RT_ID,CSTMR_ID,EQPMNT_CD_ID,EQPMNT_TYP_ID,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,STRG_CHRGS_PR_DY_NC,FR_DYS,ACTV_BT,HNDLNG_CHRG_NC,WSHNG_CHRG_NC FROM CUSTOMER_RATE"
    Private Const Customer_RateDeleteQuery As String = "DELETE FROM CUSTOMER_RATE WHERE CSTMR_RT_ID=@CSTMR_RT_ID"
    Private Const EDISettingUpdateQuery As String = "UPDATE CUSTOMER_EDI_SETTING SET EDI_FRMT=@EDI_FRMT,TO_EML_ID=@TO_EML_ID,GNRTN_FRMT=@GNRTN_FRMT,GNRTN_TM=@GNRTN_TM,LST_RN=@LST_RN WHERE CSTMR_ID=@CSTMR_ID"
    Private Const EDISettingInsertQuery As String = "INSERT INTO CUSTOMER_EDI_SETTING(CSTMR_EDI_STTNG_BIN,CSTMR_ID,EDI_FRMT,TO_EML_ID,GNRTN_FRMT,GNRTN_TM,LST_RN)VALUES(@CSTMR_EDI_STTNG_BIN,@CSTMR_ID,@EDI_FRMT,@TO_EML_ID,@GNRTN_FRMT,@GNRTN_TM,@LST_RN)"
    Private Const V_CUSTOMER_EDI_SETTINGSelectQueryByCstmr_id As String = "SELECT CSTMR_ID,CSTMR_CD,EDI_FRMT,EDI_FRMT_CD,TO_EML_ID,GNRTN_FRMT,GNRTN_FRMT_CD,GNRTN_TM FROM V_CUSTOMER_EDI_SETTING WHERE CSTMR_ID=@CSTMR_ID"
    Private Const CountEDISettingQuery As String = "SELECT COUNT(CSTMR_ID) FROM V_CUSTOMER_EDI_SETTING WHERE CSTMR_ID=@CSTMR_ID"
    Private Const EnumSelectQuery As String = "SELECT DISTINCT ENM_ID AS RPRT_ID,ENM_CD AS RPRT_CD,ENM_TYP_ID FROM ENUM WHERE ENM_TYP_ID=11 AND ENM_CD IN ("
    Private Const EmailSettingCountQuery As String = "SELECT COUNT(CSTMR_ID) FROM V_CUSTOMER_EMAIL_SETTING WHERE (CSTMR_ID=@CSTMR_ID AND ENM_ID=@ENM_ID)"
    Private Const EmailSettingInsertQuery As String = "INSERT INTO CUSTOMER_EMAIL_SETTING(CSTMR_EML_STTNG_BIN,CSTMR_ID,RPRT_ID,GNRTN_TM,TO_EML,CC_EML,SBJCT_VCR,PRDC_DT_ID,PRDC_DY_ID,PRDC_FLTR_ID,NXT_RN_DT_TM,LST_RN_DT_TM,ACTV_BT)VALUES(@CSTMR_EML_STTNG_BIN,@CSTMR_ID,@RPRT_ID,@GNRTN_TM,@TO_EML,@CC_EML,@SBJCT_VCR,@PRDC_DT_ID,@PRDC_DY_ID,@PRDC_FLTR_ID,@NXT_RN_DT_TM,@LST_RN_DT_TM,@ACTV_BT)"
    Private Const EmailSettingUpdateQuery As String = "UPDATE CUSTOMER_EMAIL_SETTING SET RPRT_ID=@ENM_ID,GNRTN_TM=@GNRTN_TM,TO_EML=@TO_EML,CC_EML=@CC_EML,BCC_EML=@BCC_EML,SBJCT_VCR=@SBJCT_VCR,ACTV_BT=@ACTV_BT WHERE (CSTMR_ID=@CSTMR_ID AND RPRT_ID=@ENM_ID)"
    Private Const V_CUSTOMER_EMAIL_SETTINGSelectQueryByCstmr_id As String = "SELECT CSTMR_EML_STTNG_BIN,RPRT_ID,RPRT_CD,CSTMR_ID,CSTMR_CD,GNRTN_TM,TO_EML,CC_EML,SBJCT_VCR,ACTV_BT,PRDC_FLTR_ID,PRDC_FLTR_CD,PRDC_DY_CD,PRDC_DY_ID,PRDC_DT_ID,PRDC_DT_CD,NXT_RN_DT_TM FROM V_CUSTOMER_EMAIL_SETTING WHERE CSTMR_ID=@CSTMR_ID ORDER BY RPRT_ID"
    Private Const CustomerEmailSettingDeleteQuery As String = "DELETE FROM CUSTOMER_EMAIL_SETTING WHERE CSTMR_ID=@CSTMR_ID"
    Private Const Customer_Charge_DetailInsertQuery As String = "INSERT INTO CUSTOMER_CHARGE_DETAIL(CSTMR_CHRG_DTL_ID,CSTMR_ID,EQPMNT_CD_ID,EQPMNT_TYP_ID,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,ACTV_BT,INSPCTN_CHRGS)VALUES(@CSTMR_CHRG_DTL_ID,@CSTMR_ID,@EQPMNT_CD_ID,@EQPMNT_TYP_ID,@HNDLNG_IN_CHRG_NC,@HNDLNG_OUT_CHRG_NC,@ACTV_BT,@INSPCTN_CHRGS)"
    Private Const Customer_Charge_DetailUpdateQuery As String = "UPDATE CUSTOMER_CHARGE_DETAIL SET CSTMR_CHRG_DTL_ID=@CSTMR_CHRG_DTL_ID, CSTMR_ID=@CSTMR_ID, EQPMNT_CD_ID=@EQPMNT_CD_ID, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, HNDLNG_IN_CHRG_NC=@HNDLNG_IN_CHRG_NC, HNDLNG_OUT_CHRG_NC=@HNDLNG_OUT_CHRG_NC, ACTV_BT=@ACTV_BT, INSPCTN_CHRGS=@INSPCTN_CHRGS WHERE CSTMR_CHRG_DTL_ID=@CSTMR_CHRG_DTL_ID"
    Private Const Customer_Storage_DetailInsertQuery As String = "INSERT INTO CUSTOMER_STORAGE_DETAIL(CSTMR_STRG_DTL_ID,CSTMR_CHRG_DTL_ID,CSTMR_ID,UP_TO_DYS,STRG_CHRG_NC,RMRKS_VC)VALUES(@CSTMR_STRG_DTL_ID,@CSTMR_CHRG_DTL_ID,@CSTMR_ID,@UP_TO_DYS,@STRG_CHRG_NC,@RMRKS_VC)"
    Private Const Customer_Storage_DetailUpdateQuery As String = "UPDATE CUSTOMER_STORAGE_DETAIL SET CSTMR_STRG_DTL_ID=@CSTMR_STRG_DTL_ID, CSTMR_CHRG_DTL_ID=@CSTMR_CHRG_DTL_ID, CSTMR_ID=@CSTMR_ID, UP_TO_DYS=@UP_TO_DYS, STRG_CHRG_NC=@STRG_CHRG_NC, RMRKS_VC=@RMRKS_VC WHERE CSTMR_STRG_DTL_ID=@CSTMR_STRG_DTL_ID"
    Private Const CUSTOMER_CHARGE_DETAILSelectQueryByCustomerID As String = "SELECT CSTMR_CHRG_DTL_ID,CSTMR_ID,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,ACTV_BT,INSPCTN_CHRGS FROM V_CUSTOMER_CHARGE_DETAIL WHERE CSTMR_ID=@CSTMR_ID"
    Private Const CUSTOMER_STORAGE_DETAILSelectQueryByCustomerChargeDetailID As String = "SELECT CSTMR_STRG_DTL_ID,CSTMR_CHRG_DTL_ID,CSTMR_ID,UP_TO_DYS,STRG_CHRG_NC,RMRKS_VC FROM CUSTOMER_STORAGE_DETAIL WHERE CSTMR_ID=@CSTMR_ID AND CSTMR_CHRG_DTL_ID=@CSTMR_CHRG_DTL_ID"
    Private Const Customer_Storage_DetailDeleteQueryBySorageDetailID As String = "DELETE FROM CUSTOMER_STORAGE_DETAIL WHERE CSTMR_STRG_DTL_ID=@CSTMR_STRG_DTL_ID"
    Private Const Customer_Storage_DetailDeleteQueryByChargeDetailID As String = "DELETE FROM CUSTOMER_STORAGE_DETAIL WHERE CSTMR_CHRG_DTL_ID=@CSTMR_CHRG_DTL_ID"
    Private Const Customer_Charge_DetailDeleteQuery As String = "DELETE FROM CUSTOMER_CHARGE_DETAIL WHERE CSTMR_CHRG_DTL_ID=@CSTMR_CHRG_DTL_ID"
    Private Const Customer_TransportationSelectQueryPk As String = "SELECT CSTMR_TRNSPRTTN_ID,CSTMR_ID,CSTMR_CD,RT_ID,RT_CD,PCK_UP_LCTN_CD,DRP_OFF_LCTN_CD,ACTVTY_CD,ACTVTY_LCTN_CD,EMPTY_TRP_RT_NC,FLL_TRP_RT_NC FROM V_CUSTOMER_TRANSPORTATION WHERE CSTMR_ID=@CSTMR_ID"
    Private Const RouteSelectQueryByRouteCode As String = "SELECT COUNT(RT_ID) AS RT_ID FROM V_CUSTOMER_TRANSPORTATION WHERE RT_CD=@RT_CD AND CSTMR_ID=@CSTMR_ID"
    Private Const CustomerRentalValidateQuery As String = "SELECT CSTMR_RNTL_ID FROM CUSTOMER_RENTAL WHERE CNTRCT_RFRNC_NO=@CNTRCT_RFRNC_NO"
    Private Const Customer_TransportationInsertQuery As String = "INSERT INTO CUSTOMER_TRANSPORTATION(CSTMR_TRNSPRTTN_ID,CSTMR_ID,RT_ID,EMPTY_TRP_RT_NC,FLL_TRP_RT_NC)VALUES(@CSTMR_TRNSPRTTN_ID,@CSTMR_ID,@RT_ID,@EMPTY_TRP_RT_NC,@FLL_TRP_RT_NC)"
    Private Const Customer_TransportationUpdateQuery As String = "UPDATE CUSTOMER_TRANSPORTATION SET CSTMR_TRNSPRTTN_ID=@CSTMR_TRNSPRTTN_ID, CSTMR_ID=@CSTMR_ID, RT_ID=@RT_ID, EMPTY_TRP_RT_NC=@EMPTY_TRP_RT_NC, FLL_TRP_RT_NC=@FLL_TRP_RT_NC WHERE CSTMR_TRNSPRTTN_ID=@CSTMR_TRNSPRTTN_ID"
    Private Const RouteRouteIdSelectQuery As String = "SELECT RT_ID FROM ROUTE WHERE RT_CD=@RT_CD AND DPT_ID=@DPT_ID"
    Private Const Customer_TransportationDeleteQuery As String = "DELETE FROM CUSTOMER_TRANSPORTATION WHERE CSTMR_TRNSPRTTN_ID=@CSTMR_TRNSPRTTN_ID"
    Private Const Customer_RentalInsertQuery As String = "INSERT INTO CUSTOMER_RENTAL(CSTMR_RNTL_ID,CSTMR_ID,CNTRCT_RFRNC_NO,CNTRCT_STRT_DT,CNTRCT_END_DT,MN_TNR_DY,RNTL_PR_DY,HNDLNG_OT,HNDLNG_IN,ON_HR_SRVY,OFF_HR_SRVY,RMRKS_VC)VALUES(@CSTMR_RNTL_ID,@CSTMR_ID,@CNTRCT_RFRNC_NO,@CNTRCT_STRT_DT,@CNTRCT_END_DT,@MN_TNR_DY,@RNTL_PR_DY,@HNDLNG_OT,@HNDLNG_IN,@ON_HR_SRVY,@OFF_HR_SRVY,@RMRKS_VC)"
    Private Const Customer_RentalUpdateQuery As String = "UPDATE CUSTOMER_RENTAL SET  CSTMR_ID=@CSTMR_ID,CNTRCT_RFRNC_NO=@CNTRCT_RFRNC_NO,CNTRCT_STRT_DT=@CNTRCT_STRT_DT,CNTRCT_END_DT=@CNTRCT_END_DT,MN_TNR_DY=@MN_TNR_DY,RNTL_PR_DY=@RNTL_PR_DY, HNDLNG_OT=@HNDLNG_OT, HNDLNG_IN=@HNDLNG_IN, ON_HR_SRVY=@ON_HR_SRVY, OFF_HR_SRVY=@OFF_HR_SRVY, RMRKS_VC=@RMRKS_VC WHERE CSTMR_RNTL_ID=@CSTMR_RNTL_ID"
    Private Const Customer_RentalDeleteQuery As String = "DELETE FROM CUSTOMER_RENTAL WHERE CSTMR_RNTL_ID=@CSTMR_RNTL_ID"
    Private Const Customer_RentalSelectQuery As String = "SELECT CSTMR_RNTL_ID,CSTMR_ID,CNTRCT_RFRNC_NO,CNTRCT_STRT_DT,CNTRCT_END_DT,MN_TNR_DY,RNTL_PR_DY,HNDLNG_OT,HNDLNG_IN,ON_HR_SRVY,OFF_HR_SRVY,RMRKS_VC,GTOT_BT FROM V_CUSTOMER_RENTAL WHERE CSTMR_ID=@CSTMR_ID"
    Private Const Customer_RentalSupplierIdQuery As String = "SELECT SPPLR_ID FROM SUPPLIER_CONTRACT_DETAIL WHERE CNTRCT_RFRNC_NO=@CNTRCT_RFRNC_NO"
    Private Const V_ROUTESelectQueryById As String = "SELECT RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_ID,PCK_UP_LCTN_CD,DRP_OFF_LCTN_ID,DRP_OFF_LCTN_CD,ACTVTY_ID,ACTVTY_CD,ACTVTY_LCTN_ID,ACTVTY_LCTN_CD,EMPTY_TRP_RT_NC,FLL_TRP_RT_NC,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM V_ROUTE WHERE RT_ID= @RT_ID AND DPT_ID=@DPT_ID AND ACTV_BT=1"
    Private Const CustomerRentalGateoutSelectQuery As String = "SELECT COUNT(GTOT_ID) AS GTOT_ID  FROM GATEOUT WHERE RNTL_RFRNC_NO IN (SELECT RNTL_RFRNC_NO FROM RENTAL_ENTRY WHERE CNTRCT_RFRNC_NO=@CNTRCT_RFRNC_NO)"

    'Cleaning Slab Rate Changes
    Private Const CUSTOMER_CLEANING_DETAILSelectQueryByCustomerChargeDetailID As String = "SELECT CSTMR_CLNNG_DTL_ID,CSTMR_CHRG_DTL_ID,CSTMR_ID,UP_TO_CNTNRS,CLNNG_RT,RMRKS_VC FROM CUSTOMER_CLEANING_DETAIL WHERE CSTMR_ID=@CSTMR_ID AND CSTMR_CHRG_DTL_ID=@CSTMR_CHRG_DTL_ID"
    Private Const getCustomerRateFromEnum As String = "SELECT ENM_ID,ENM_CD,ENM_DSCRPTN_VC,ENM_TYP_ID,ENM_TYP_CD FROM ENUM WHERE ENM_TYP_ID=@ENM_TYP_ID"
    Private Const Customer_Cleaning_DetailInsertQuery As String = "INSERT INTO CUSTOMER_CLEANING_DETAIL(CSTMR_CLNNG_DTL_ID,CSTMR_CHRG_DTL_ID,CSTMR_ID,UP_TO_CNTNRS,CLNNG_RT,RMRKS_VC)VALUES(@CSTMR_CLNNG_DTL_ID,@CSTMR_CHRG_DTL_ID,@CSTMR_ID,@UP_TO_CNTNRS,@CLNNG_RT,@RMRKS_VC)"
    Private Const Customer_Cleaning_DetailUpdateQuery As String = "UPDATE CUSTOMER_CLEANING_DETAIL SET CSTMR_CHRG_DTL_ID=@CSTMR_CHRG_DTL_ID, CSTMR_ID=@CSTMR_ID, UP_TO_CNTNRS=@UP_TO_CNTNRS, CLNNG_RT=@CLNNG_RT, RMRKS_VC=@RMRKS_VC WHERE CSTMR_CLNNG_DTL_ID=@CSTMR_CLNNG_DTL_ID"
    Private Const Customer_Cleaning_DetailDeleteQueryByChargeDetailID As String = "DELETE FROM CUSTOMER_CLEANING_DETAIL WHERE CSTMR_CHRG_DTL_ID=@CSTMR_CHRG_DTL_ID"
    Private Const Customer_Cleaning_DetailDeleteQueryByCleaningDetailID As String = "DELETE FROM CUSTOMER_CLEANING_DETAIL WHERE CSTMR_CLNNG_DTL_ID=@CSTMR_CLNNG_DTL_ID"

    'for iso
    Private Const getCustomerISOcodebyCustomerIDquery As String = "SELECT CSTMR_CD,ISO_CD FROM V_CUSTOMER WHERE CSTMR_ID=@CSTMR_ID"
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
    Dim sqlDateDbnull As DateTime = "01/01/1900"
    Private ds As CustomerDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New CustomerDataSet
    End Sub

#End Region

#Region "GetV_CustomerByCSTMR_ID() TABLE NAME:V_Customer"

    Public Function GetV_CustomerByCstmr_Id(ByVal bv_intCstmr_ID As Integer, ByVal bv_intDepotId As Integer) As CustomerDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CustomerData.DPT_ID, bv_intDepotId)
            hshParameters.Add(CustomerData.CSTMR_ID, bv_intCstmr_ID)
            objData = New DataObjects(V_CustomerSelectQueryByCstmr_Id, hshParameters)
            objData.Fill(CType(ds, DataSet), CustomerData._V_CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCustomerTransportationByCustomerID() TABLE NAME:Customer_Transportation"

    Public Function GetCustomerTransportationByCustomerId(ByVal bv_i64CustomerId As Int64) As CustomerDataSet
        Try
            objData = New DataObjects(Customer_TransportationSelectQueryPk, CustomerData.CSTMR_ID, bv_i64CustomerId)
            objData.Fill(CType(ds, DataSet), CustomerData._V_CUSTOMER_TRANSPORTATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCustomerRentalByCustomerId() TABLE NAME:Customer_Rental"

    Public Function GetCustomerRentalByCustomerId(ByVal bv_i64CustomerId As Int64) As CustomerDataSet
        Try
            objData = New DataObjects(Customer_RentalSelectQuery, CustomerData.CSTMR_ID, bv_i64CustomerId)
            objData.Fill(CType(ds, DataSet), CustomerData._V_CUSTOMER_RENTAL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "ValidateRouteIdByRouteCode"

    Public Function ValidateRouteIdByRouteCode(ByVal bv_strRouteCode As String, ByVal bv_strCustomerID As String) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CustomerData.RT_CD, bv_strRouteCode)
            hshParameters.Add(CustomerData.CSTMR_ID, bv_strCustomerID)
            objData = New DataObjects(RouteSelectQueryByRouteCode, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_ValidateContractNoByContractNo"

    Public Function pub_ValidateContractNoByContractNo(ByVal bv_strContractNo As String) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CustomerData.CNTRCT_RFRNC_NO, bv_strContractNo)
            'hshParameters.Add(CustomerData.CSTMR_ID, bv_strCustomerID)
            objData = New DataObjects(CustomerRentalValidateQuery, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRouteIdByRouteCode"

    Public Function GetRouteIdByRouteCode(ByVal bv_strRouteCode As String, _
                                          ByVal bv_i32DepotId As Int32) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CustomerData.RT_CD, bv_strRouteCode)
            hshParameters.Add(CustomerData.DPT_ID, bv_i32DepotId)
            objData = New DataObjects(RouteRouteIdSelectQuery, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRouteIdByRouteCode"

    Public Function GetSupplierIdByContractNo(ByVal bv_strContractNo As String) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CustomerData.CNTRCT_RFRNC_NO, bv_strContractNo)
            objData = New DataObjects(Customer_RentalSupplierIdQuery, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "CreateCustomer() TABLE NAME:Customer"
    Public Function CreateCustomer(ByVal bv_strCustomerCode As String, _
                                   ByVal bv_strCustomerName As String, _
                                   ByVal bv_i64CustomerCurrencyID As Int64, _
                                   ByVal bv_i64BillingType As Int64, _
                                   ByVal bv_i64BulkEmailFormatID As Int64, _
                                   ByVal bv_strContactPersonName As String, _
                                   ByVal bv_strContactAddress As String, _
                                   ByVal bv_strBillingAddress As String, _
                                   ByVal bv_strZipCode As String, _
                                   ByVal bv_strPhoneNumber As String, _
                                   ByVal bv_strFaxNumber As String, _
                                   ByVal bv_strReportingEmailID As String, _
                                   ByVal bv_strInvoicingEmailID As String, _
                                   ByVal bv_strRepairTechEmailID As String, _
                                   ByVal bv_decHYDR_AMNT_NC As Decimal, _
                                   ByVal bv_decPNMTC_AMNT_NC As Decimal, _
                                   ByVal bv_decLBR_RT_PR_HR_NC As Decimal, _
                                   ByVal bv_decLK_TST_RT_NC As Decimal, _
                                   ByVal bv_decSRVY_ONHR_OFFHR_RT_NC As Decimal, _
                                   ByVal bv_i64PRDC_TST_TYP_ID As Int64, _
                                   ByVal bv_strVLDTY_PRD_TST_YRS As String, _
                                   ByVal bv_strMinHeatingRate As Decimal, _
                                   ByVal bv_strMinHeatingPeriod As Decimal, _
                                   ByVal bv_strHourlyCharge As Decimal, _
                                   ByVal bv_blnCheckDigitValidationBit As Boolean, _
                                   ByVal bv_blnTransportationBit As Boolean, _
                                   ByVal bv_blnRentalBit As Boolean, _
                                   ByVal bv_strCreatedBy As String, _
                                   ByVal bv_datCreatedDate As DateTime, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByVal bv_blnActiveBit As Boolean, _
                                   ByVal bv_i32DepotID As Int32, _
                                   ByVal bv_blnXML_BT As Boolean, _
                                   ByVal bv_strServerUrl As String, _
                                   ByVal bv_strServerName As String, _
                                   ByVal bv_strPassword As String, _
                                   ByVal bv_strEdiCode As String, _
                                   ByVal bv_FinanceIntegrationBit As Boolean, _
                                   ByVal bv_LedgerId As String, _
                                   ByVal bv_blnShell As Boolean, _
                                   ByVal bv_blnSTube As Boolean, _
                                   ByVal bv_strCustVatNo As String, _
                                   ByVal bv_strAgent As String, _
                                   ByVal bv_strStorageTax As String, _
                                   ByVal bv_strServiceTax As String, _
                                   ByVal bv_strHandlingTax As String, _
                                   ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CustomerData._SERVICE_PARTNER, br_ObjTransactions)
                .Item(CustomerData.CSTMR_ID) = intMax
                .Item(CustomerData.CSTMR_CD) = bv_strCustomerCode
                .Item(CustomerData.CSTMR_NAM) = bv_strCustomerName
                .Item(CustomerData.CSTMR_CRRNCY_ID) = bv_i64CustomerCurrencyID
                If bv_i64BillingType <> 0 Then
                    .Item(CustomerData.BLLNG_TYP_ID) = bv_i64BillingType
                Else
                    .Item(CustomerData.BLLNG_TYP_ID) = DBNull.Value
                End If
                .Item(CustomerData.CNTCT_PRSN_NAM) = bv_strContactPersonName
                .Item(CustomerData.CNTCT_ADDRSS) = bv_strContactAddress
                If bv_strBillingAddress <> Nothing Then
                    .Item(CustomerData.BLLNG_ADDRSS) = bv_strBillingAddress
                Else
                    .Item(CustomerData.BLLNG_ADDRSS) = DBNull.Value
                End If
                If bv_strZipCode <> Nothing Then
                    .Item(CustomerData.ZP_CD) = bv_strZipCode
                Else
                    .Item(CustomerData.ZP_CD) = DBNull.Value
                End If
                If bv_strPhoneNumber <> Nothing Then
                    .Item(CustomerData.PHN_NO) = bv_strPhoneNumber
                Else
                    .Item(CustomerData.PHN_NO) = DBNull.Value
                End If
                If bv_strFaxNumber <> Nothing Then
                    .Item(CustomerData.FX_NO) = bv_strFaxNumber
                Else
                    .Item(CustomerData.FX_NO) = DBNull.Value
                End If
                If bv_strReportingEmailID <> Nothing Then
                    .Item(CustomerData.RPRTNG_EML_ID) = bv_strReportingEmailID
                Else
                    .Item(CustomerData.RPRTNG_EML_ID) = DBNull.Value
                End If
                If bv_strInvoicingEmailID <> Nothing Then
                    .Item(CustomerData.INVCNG_EML_ID) = bv_strInvoicingEmailID
                Else
                    .Item(CustomerData.INVCNG_EML_ID) = DBNull.Value
                End If
                If bv_strRepairTechEmailID <> Nothing Then
                    .Item(CustomerData.RPR_TCH_EML_ID) = bv_strRepairTechEmailID
                Else
                    .Item(CustomerData.RPR_TCH_EML_ID) = DBNull.Value
                End If
                .Item(CustomerData.HYDR_AMNT_NC) = bv_decHYDR_AMNT_NC
                .Item(CustomerData.PNMTC_AMNT_NC) = bv_decPNMTC_AMNT_NC
                .Item(CustomerData.LBR_RT_PR_HR_NC) = bv_decLBR_RT_PR_HR_NC
                .Item(CustomerData.LK_TST_RT_NC) = bv_decLK_TST_RT_NC
                .Item(CustomerData.SRVY_ONHR_OFFHR_RT_NC) = bv_decSRVY_ONHR_OFFHR_RT_NC
                If bv_i64PRDC_TST_TYP_ID <> 0 Then
                    .Item(CustomerData.PRDC_TST_TYP_ID) = bv_i64PRDC_TST_TYP_ID
                Else
                    .Item(CustomerData.PRDC_TST_TYP_ID) = DBNull.Value
                End If
                If bv_strVLDTY_PRD_TST_YRS <> Nothing Then
                    .Item(CustomerData.VLDTY_PRD_TST_YRS) = bv_strVLDTY_PRD_TST_YRS
                Else
                    .Item(CustomerData.VLDTY_PRD_TST_YRS) = DBNull.Value
                End If
                If bv_i64BulkEmailFormatID <> 0 Then
                    .Item(CustomerData.BLK_EML_FRMT_ID) = bv_i64BulkEmailFormatID
                Else
                    .Item(CustomerData.BLK_EML_FRMT_ID) = DBNull.Value
                End If

                .Item(CustomerData.MIN_HTNG_RT_NC) = bv_strMinHeatingRate
                .Item(CustomerData.MIN_HTNG_PRD_NC) = bv_strMinHeatingPeriod
                .Item(CustomerData.HRLY_CHRG_NC) = bv_strHourlyCharge
                .Item(CustomerData.CHK_DGT_VLDTN_BT) = bv_blnCheckDigitValidationBit
                .Item(CustomerData.TRNSPRTTN_BT) = bv_blnTransportationBit
                .Item(CustomerData.RNTL_BT) = bv_blnRentalBit
                .Item(CustomerData.CRTD_BY) = bv_strCreatedBy
                .Item(CustomerData.CRTD_DT) = bv_datCreatedDate
                .Item(CustomerData.MDFD_BY) = bv_strModifiedBy
                .Item(CustomerData.MDFD_DT) = bv_datModifiedDate
                .Item(CustomerData.ACTV_BT) = bv_blnActiveBit
                .Item(CustomerData.DPT_ID) = bv_i32DepotID
                .Item(CustomerData.XML_BT) = bv_blnXML_BT
                If bv_strServerUrl <> Nothing Then
                    .Item(CustomerData.FTP_SRVR_URL) = bv_strServerUrl
                Else
                    .Item(CustomerData.FTP_SRVR_URL) = DBNull.Value
                End If
                If bv_strServerName <> Nothing Then
                    .Item(CustomerData.FTP_USR_NAM) = bv_strServerName
                Else
                    .Item(CustomerData.FTP_USR_NAM) = DBNull.Value
                End If
                If bv_strPassword <> Nothing Then
                    .Item(CustomerData.FTP_PSSWRD) = bv_strPassword
                Else
                    .Item(CustomerData.FTP_PSSWRD) = DBNull.Value
                End If
                If bv_strEdiCode <> Nothing Then
                    .Item(CustomerData.ISO_CD) = bv_strEdiCode
                Else
                    .Item(CustomerData.ISO_CD) = DBNull.Value
                End If

                'Customer Master Changes
                .Item(CustomerData.SHELL) = bv_blnShell
                .Item(CustomerData.STUBE) = bv_blnSTube

                'Finance Integration
                If bv_FinanceIntegrationBit = True Then
                    .Item(CustomerData.LDGR_ID) = bv_LedgerId
                Else
                    .Item(CustomerData.LDGR_ID) = DBNull.Value
                End If
                If bv_strAgent <> Nothing Then
                    .Item(CustomerData.AGENT_ID) = bv_strAgent
                Else
                    .Item(CustomerData.AGENT_ID) = DBNull.Value
                End If
                If bv_strCustVatNo <> Nothing Then
                    .Item(CustomerData.CSTMR_VAT_NO) = bv_strCustVatNo
                Else
                    .Item(CustomerData.CSTMR_VAT_NO) = DBNull.Value
                End If
                If bv_strServiceTax <> Nothing Then
                    .Item(CustomerData.SERVC_TX) = bv_strServiceTax
                Else
                    .Item(CustomerData.SERVC_TX) = DBNull.Value
                End If
                If bv_strStorageTax <> Nothing Then
                    .Item(CustomerData.STORG_TX) = bv_strStorageTax
                Else
                    .Item(CustomerData.STORG_TX) = DBNull.Value
                End If
                If bv_strHandlingTax <> Nothing Then
                    .Item(CustomerData.HANDLNG_TX) = bv_strHandlingTax
                Else
                    .Item(CustomerData.HANDLNG_TX) = DBNull.Value
                End If

            End With
            objData.InsertRow(dr, CustomerInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateCustomer = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCustomer() TABLE NAME:Customer"
    Public Function UpdateCustomer(ByVal bv_i64CustomerID As Int64, _
                                   ByVal bv_strCustomerCode As String, _
                                   ByVal bv_strCustomerName As String, _
                                   ByVal bv_i64CustomerCurrencyID As Int64, _
                                   ByVal bv_i64BillingType As Int64, _
                                   ByVal bv_i64BulkEmailFormatID As Int64, _
                                   ByVal bv_strContactPersonName As String, _
                                   ByVal bv_strContactAddress As String, _
                                   ByVal bv_strBillingAddress As String, _
                                   ByVal bv_strZipCode As String, _
                                   ByVal bv_strPhoneNumber As String, _
                                   ByVal bv_strFaxNumber As String, _
                                   ByVal bv_strReportingEmailID As String, _
                                   ByVal bv_strInvoicingEmailID As String, _
                                   ByVal bv_strRepairTechEmailID As String, _
                                   ByVal bv_decHydroAmount As Decimal, _
                                   ByVal bv_decPneumaticAmount As Decimal, _
                                   ByVal bv_decLaborRatePerHour As Decimal, _
                                   ByVal bv_decLeakTestRate As Decimal, _
                                   ByVal bv_decSurveyAmount As Decimal, _
                                   ByVal bv_i64PeriodicTestTypeID As Int64, _
                                   ByVal bv_strValidityPeriodTest As String, _
                                   ByVal bv_strMinHeatingRate As Decimal, _
                                   ByVal bv_strMinHeatingPeriod As Decimal, _
                                   ByVal bv_strHourlyCharge As Decimal, _
                                   ByVal bv_blnCheckDigitValidationBit As Boolean, _
                                   ByVal bv_blnTransportationBit As Boolean, _
                                   ByVal bv_blnRentalBit As Boolean, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByVal bv_blnActiveBit As Boolean, _
                                   ByVal bv_i32DepotID As Int32, _
                                   ByVal bv_blnXML_BT As Boolean, _
                                   ByVal bv_strServerUrl As String, _
                                   ByVal bv_strServerName As String, _
                                   ByVal bv_strPassword As String, _
                                   ByVal bv_strEdiCode As String, _
                                   ByVal bv_FinanceIntegrationBit As Boolean, _
                                   ByVal bv_LedgerId As String, _
                                   ByVal bv_blnShell As Boolean, _
                                   ByVal bv_blnSTube As Boolean, _
                                   ByVal bv_strCustVatNo As String, _
                                   ByVal bv_strAgent As String, _
                                   ByVal bv_strStorageTax As String, _
                                   ByVal bv_strServiceTax As String, _
                                   ByVal bv_strHandlingTax As String, _
                                   ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER).NewRow()
            With dr
                .Item(CustomerData.CSTMR_ID) = bv_i64CustomerID
                .Item(CustomerData.CSTMR_CD) = bv_strCustomerCode
                .Item(CustomerData.CSTMR_NAM) = bv_strCustomerName
                .Item(CustomerData.CSTMR_CRRNCY_ID) = bv_i64CustomerCurrencyID
                If bv_i64BillingType <> 0 Then
                    .Item(CustomerData.BLLNG_TYP_ID) = bv_i64BillingType
                Else
                    .Item(CustomerData.BLLNG_TYP_ID) = DBNull.Value
                End If
                .Item(CustomerData.CNTCT_PRSN_NAM) = bv_strContactPersonName
                .Item(CustomerData.CNTCT_ADDRSS) = bv_strContactAddress
                If bv_strBillingAddress <> Nothing Then
                    .Item(CustomerData.BLLNG_ADDRSS) = bv_strBillingAddress
                Else
                    .Item(CustomerData.BLLNG_ADDRSS) = DBNull.Value
                End If
                If bv_strZipCode <> Nothing Then
                    .Item(CustomerData.ZP_CD) = bv_strZipCode
                Else
                    .Item(CustomerData.ZP_CD) = DBNull.Value
                End If
                If bv_strPhoneNumber <> Nothing Then
                    .Item(CustomerData.PHN_NO) = bv_strPhoneNumber
                Else
                    .Item(CustomerData.PHN_NO) = DBNull.Value
                End If
                If bv_strFaxNumber <> Nothing Then
                    .Item(CustomerData.FX_NO) = bv_strFaxNumber
                Else
                    .Item(CustomerData.FX_NO) = DBNull.Value
                End If
                If bv_strReportingEmailID <> Nothing Then
                    .Item(CustomerData.RPRTNG_EML_ID) = bv_strReportingEmailID
                Else
                    .Item(CustomerData.RPRTNG_EML_ID) = DBNull.Value
                End If
                If bv_strInvoicingEmailID <> Nothing Then
                    .Item(CustomerData.INVCNG_EML_ID) = bv_strInvoicingEmailID
                Else
                    .Item(CustomerData.INVCNG_EML_ID) = DBNull.Value
                End If
                If bv_strRepairTechEmailID <> Nothing Then
                    .Item(CustomerData.RPR_TCH_EML_ID) = bv_strRepairTechEmailID
                Else
                    .Item(CustomerData.RPR_TCH_EML_ID) = DBNull.Value
                End If
                .Item(CustomerData.BLK_EML_FRMT_ID) = bv_i64BulkEmailFormatID
                .Item(CustomerData.HYDR_AMNT_NC) = bv_decHydroAmount
                .Item(CustomerData.PNMTC_AMNT_NC) = bv_decPneumaticAmount
                .Item(CustomerData.LBR_RT_PR_HR_NC) = bv_decLaborRatePerHour
                .Item(CustomerData.LK_TST_RT_NC) = bv_decLeakTestRate
                .Item(CustomerData.SRVY_ONHR_OFFHR_RT_NC) = bv_decSurveyAmount
                If bv_i64PeriodicTestTypeID <> 0 Then
                    .Item(CustomerData.PRDC_TST_TYP_ID) = bv_i64PeriodicTestTypeID
                Else
                    .Item(CustomerData.PRDC_TST_TYP_ID) = DBNull.Value
                End If
                .Item(CustomerData.VLDTY_PRD_TST_YRS) = bv_strValidityPeriodTest
                .Item(CustomerData.MIN_HTNG_RT_NC) = bv_strMinHeatingRate
                .Item(CustomerData.MIN_HTNG_PRD_NC) = bv_strMinHeatingPeriod
                .Item(CustomerData.HRLY_CHRG_NC) = bv_strHourlyCharge
                .Item(CustomerData.CHK_DGT_VLDTN_BT) = bv_blnCheckDigitValidationBit
                .Item(CustomerData.TRNSPRTTN_BT) = bv_blnTransportationBit
                .Item(CustomerData.RNTL_BT) = bv_blnRentalBit
                .Item(CustomerData.MDFD_BY) = bv_strModifiedBy
                .Item(CustomerData.MDFD_DT) = bv_datModifiedDate
                .Item(CustomerData.ACTV_BT) = bv_blnActiveBit
                .Item(CustomerData.DPT_ID) = bv_i32DepotID
                .Item(CustomerData.XML_BT) = bv_blnXML_BT
                .Item(CustomerData.FTP_SRVR_URL) = bv_strServerUrl
                .Item(CustomerData.FTP_USR_NAM) = bv_strServerName
                .Item(CustomerData.FTP_PSSWRD) = bv_strPassword
                If bv_strEdiCode <> Nothing Then
                    .Item(CustomerData.ISO_CD) = bv_strEdiCode
                Else
                    .Item(CustomerData.ISO_CD) = DBNull.Value
                End If

                'Customer Master Changes
                .Item(CustomerData.SHELL) = bv_blnShell
                .Item(CustomerData.STUBE) = bv_blnSTube

                'Finance Integration
                If bv_FinanceIntegrationBit = True Then
                    .Item(CustomerData.LDGR_ID) = bv_LedgerId
                Else
                    .Item(CustomerData.LDGR_ID) = DBNull.Value
                End If
                If bv_strAgent <> Nothing Then
                    .Item(CustomerData.AGENT_ID) = bv_strAgent
                Else
                    .Item(CustomerData.AGENT_ID) = DBNull.Value
                End If
                If bv_strCustVatNo <> Nothing Then
                    .Item(CustomerData.CSTMR_VAT_NO) = bv_strCustVatNo
                Else
                    .Item(CustomerData.CSTMR_VAT_NO) = DBNull.Value
                End If
                If bv_strServiceTax <> Nothing Then
                    .Item(CustomerData.SERVC_TX) = bv_strServiceTax
                Else
                    .Item(CustomerData.SERVC_TX) = DBNull.Value
                End If
                If bv_strStorageTax <> Nothing Then
                    .Item(CustomerData.STORG_TX) = bv_strStorageTax
                Else
                    .Item(CustomerData.STORG_TX) = DBNull.Value
                End If
                If bv_strHandlingTax <> Nothing Then
                    .Item(CustomerData.HANDLNG_TX) = bv_strHandlingTax
                Else
                    .Item(CustomerData.HANDLNG_TX) = DBNull.Value
                End If

            End With

            'Finance Integration
            If bv_FinanceIntegrationBit = True Then
                UpdateCustomer = objData.UpdateRow(dr, CustomerUpdateQueryWithLedger, br_ObjTransactions)
            Else
                UpdateCustomer = objData.UpdateRow(dr, CustomerUpdateQuery, br_ObjTransactions)
            End If

            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "CreateCustomerChargeDetail() TABLE NAME:Customer_Charge_Detail"
    Public Function CreateCustomerChargeDetail(ByVal bv_i64CustomerID As Int64, _
                                               ByVal bv_i64EquipmentCodeID As Int64, _
                                               ByVal bv_i64EquipmentTypeID As Int64, _
                                               ByVal bv_strHandlingInCharges As Decimal, _
                                               ByVal bv_strHandlingOutCharge As Decimal, _
                                               ByVal bv_blnActiveBit As Boolean, _
                                               ByVal bv_decInspctnCharge As Decimal, _
                                               ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_CHARGE_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CustomerData._CUSTOMER_CHARGE_DETAIL, br_ObjTransactions)
                .Item(CustomerData.CSTMR_CHRG_DTL_ID) = intMax
                .Item(CustomerData.CSTMR_ID) = bv_i64CustomerID
                .Item(CustomerData.EQPMNT_CD_ID) = bv_i64EquipmentCodeID
                .Item(CustomerData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                .Item(CustomerData.HNDLNG_IN_CHRG_NC) = bv_strHandlingInCharges
                .Item(CustomerData.HNDLNG_OUT_CHRG_NC) = bv_strHandlingOutCharge
                If bv_decInspctnCharge <> Nothing Then
                    .Item(CustomerData.INSPCTN_CHRGS) = bv_decInspctnCharge
                Else
                    .Item(CustomerData.INSPCTN_CHRGS) = DBNull.Value
                End If
                .Item(CustomerData.ACTV_BT) = bv_blnActiveBit
            End With
            objData.InsertRow(dr, Customer_Charge_DetailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateCustomerChargeDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCustomerChargeDetail() TABLE NAME:Customer_Charge_Detail"
    Public Function UpdateCustomerChargeDetail(ByVal bv_i64CustomerChargeID As Int64, _
                                               ByVal bv_i64CustomerID As Int64, _
                                               ByVal bv_i64EquipmentCodeID As Int64, _
                                               ByVal bv_i64EquipmentTypeID As Int64, _
                                               ByVal bv_strHandlingInCharges As Decimal, _
                                               ByVal bv_strHandlingOutCharge As Decimal, _
                                               ByVal bv_decInspctnCharge As Decimal, _
                                               ByVal bv_blnActiveBit As Boolean, _
                                               ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_CHARGE_DETAIL).NewRow()
            With dr
                .Item(CustomerData.CSTMR_CHRG_DTL_ID) = bv_i64CustomerChargeID
                .Item(CustomerData.CSTMR_ID) = bv_i64CustomerID
                .Item(CustomerData.EQPMNT_CD_ID) = bv_i64EquipmentCodeID
                .Item(CustomerData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeID
                .Item(CustomerData.HNDLNG_IN_CHRG_NC) = bv_strHandlingInCharges
                .Item(CustomerData.HNDLNG_OUT_CHRG_NC) = bv_strHandlingOutCharge
                .Item(CustomerData.INSPCTN_CHRGS) = bv_decInspctnCharge
                .Item(CustomerData.ACTV_BT) = bv_blnActiveBit
            End With
            UpdateCustomerChargeDetail = objData.UpdateRow(dr, Customer_Charge_DetailUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteCustomerChargeDetail() TABLE NAME:CUSTOMER_CHARGE_DETAIL"

    Public Function DeleteCustomerChargeDetail(ByVal bv_blnCustomerChargeID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(CustomerData._CUSTOMER_CHARGE_DETAIL).NewRow()
            With dr
                .Item(CustomerData.CSTMR_CHRG_DTL_ID) = bv_blnCustomerChargeID
            End With
            DeleteCustomerChargeDetail = objData.DeleteRow(dr, Customer_Charge_DetailDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateCustomerStorageDetail() TABLE NAME:Customer_Storage_Detail"
    Public Function CreateCustomerStorageDetail(ByVal bv_i64CustomerChargeDetailID As Int64, _
                                                ByVal bv_i64CustomerID As Int64, _
                                                ByVal bv_i32UptoDays As Int32, _
                                                ByVal bv_strStorageCharge As Decimal, _
                                                ByVal bv_strRemarks As String, _
                                                ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CustomerData._CUSTOMER_STORAGE_DETAIL, br_ObjTransactions)
                .Item(CustomerData.CSTMR_STRG_DTL_ID) = intMax
                .Item(CustomerData.CSTMR_CHRG_DTL_ID) = bv_i64CustomerChargeDetailID
                .Item(CustomerData.CSTMR_ID) = bv_i64CustomerID
                .Item(CustomerData.UP_TO_DYS) = bv_i32UptoDays
                .Item(CustomerData.STRG_CHRG_NC) = bv_strStorageCharge
                If bv_strRemarks <> Nothing Then
                    .Item(CustomerData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CustomerData.RMRKS_VC) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Customer_Storage_DetailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateCustomerStorageDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCustomerStorageDetail() TABLE NAME:Customer_Storage_Detail"
    Public Function UpdateCustomerStorageDetail(ByVal bv_i64CustomerStorageDetailID As Int64, _
                                                ByVal bv_i64CustomerChargeDetailID As Int64, _
                                                ByVal bv_i64CustomerID As Int64, _
                                                ByVal bv_i32UptoDays As Int32, _
                                                ByVal bv_strStorageCharge As Decimal, _
                                                ByVal bv_strRemarks As String, _
                                                ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL).NewRow()
            With dr
                .Item(CustomerData.CSTMR_STRG_DTL_ID) = bv_i64CustomerStorageDetailID
                .Item(CustomerData.CSTMR_CHRG_DTL_ID) = bv_i64CustomerChargeDetailID
                .Item(CustomerData.CSTMR_ID) = bv_i64CustomerID
                .Item(CustomerData.UP_TO_DYS) = bv_i32UptoDays
                .Item(CustomerData.STRG_CHRG_NC) = bv_strStorageCharge
                If bv_strRemarks <> Nothing Then
                    .Item(CustomerData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CustomerData.RMRKS_VC) = DBNull.Value
                End If
            End With
            UpdateCustomerStorageDetail = objData.UpdateRow(dr, Customer_Storage_DetailUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CustomerChargeDetailByCustomerID() TABLE NAME:CUSTOMER_CHARGE_DETAIL"

    Public Function CustomerChargeDetailByCustomerID(ByVal bv_i64CustomerID As Int64) As CustomerDataSet
        Try
            objData = New DataObjects(CUSTOMER_CHARGE_DETAILSelectQueryByCustomerID, CustomerData.CSTMR_ID, CStr(bv_i64CustomerID))
            objData.Fill(CType(ds, DataSet), CustomerData._V_CUSTOMER_CHARGE_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CustomerStorageDetailByCustomerChargeDetailID() TABLE NAME:CUSTOMER_STORAGE_DETAIL"

    Public Function CustomerStorageDetailByCustomerChargeDetailID(ByVal bv_i64CustomerID As Int64, ByVal bv_i64CustomerChargeDetailID As Int64) As CustomerDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CustomerData.CSTMR_ID, bv_i64CustomerID)
            hshParameters.Add(CustomerData.CSTMR_CHRG_DTL_ID, bv_i64CustomerChargeDetailID)
            objData = New DataObjects(CUSTOMER_STORAGE_DETAILSelectQueryByCustomerChargeDetailID, hshParameters)
            objData.Fill(CType(ds, DataSet), CustomerData._CUSTOMER_STORAGE_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CustomerStorageDetailByCustomerChargeDetailID() TABLE NAME:CUSTOMER_STORAGE_DETAIL"

    Public Function CustomerCleaningDetailByCustomerChargeDetailID(ByVal bv_i64CustomerID As Int64, ByVal bv_i64CustomerChargeDetailID As Int64) As CustomerDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CustomerData.CSTMR_ID, bv_i64CustomerID)
            hshParameters.Add(CustomerData.CSTMR_CHRG_DTL_ID, bv_i64CustomerChargeDetailID)
            objData = New DataObjects(CUSTOMER_Cleaning_DETAILSelectQueryByCustomerChargeDetailID, hshParameters)
            objData.Fill(CType(ds, DataSet), CustomerData._CUSTOMER_CLEANING_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "DeleteCustomerStorageDetailByStorageDetailID() TABLE NAME:Customer_Storage_Detail"

    Public Function DeleteCustomerStorageDetailByStorageDetailID(ByVal bv_strCustomerStorageDetailID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL).NewRow()
            With dr
                .Item(CustomerData.CSTMR_STRG_DTL_ID) = bv_strCustomerStorageDetailID
            End With
            DeleteCustomerStorageDetailByStorageDetailID = objData.DeleteRow(dr, Customer_Storage_DetailDeleteQueryBySorageDetailID, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteCustomerStorageDetailByChargeDetailID() TABLE NAME:Customer_Storage_Detail"

    Public Function DeleteCustomerStorageDetailByChargeDetailID(ByVal bv_strCustomerChargeDetailID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL).NewRow()
            With dr
                .Item(CustomerData.CSTMR_CHRG_DTL_ID) = bv_strCustomerChargeDetailID
            End With
            DeleteCustomerStorageDetailByChargeDetailID = objData.DeleteRow(dr, Customer_Storage_DetailDeleteQueryByChargeDetailID, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateEDISetting"
    Public Function UpdateEDISetting(ByVal bv_i64CSTMR_ID As Int64, _
        ByVal strEmailId As String, _
        ByVal intGenerationFormat As Integer, _
        ByVal strGenerationTime As String, _
        ByVal intEDIFormat As Integer, _
        ByVal dtLastRun As DateTime,
        ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_EDI_SETTING).NewRow()
            With dr
                .Item(CustomerData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(CustomerData.TO_EML_ID) = strEmailId
                .Item(CustomerData.GNRTN_FRMT) = intGenerationFormat
                .Item(CustomerData.GNRTN_TM) = strGenerationTime
                .Item(CustomerData.EDI_FRMT) = intEDIFormat
                If dtLastRun <> Nothing Then
                    .Item(CustomerData.LST_RN) = dtLastRun
                Else
                    .Item(CustomerData.LST_RN) = DBNull.Value
                End If
                '.Item(CustomerData.NXT_RN_TM) = bv_dtNextRunTime
                'If dtLastRun <> Nothing Then
                '    .Item(CustomerData.NXT_RN_DT) = bv_dtNextRunDateTime
                'Else
                '    .Item(CustomerData.NXT_RN_DT) = DBNull.Value
                'End If
            End With
            UpdateEDISetting = objData.UpdateRow(dr, EDISettingUpdateQuery, br_ObjTransactions)

            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateEDISetting"
    Public Function CreateEDISetting(ByVal bv_i64CSTMR_ID As Int64, _
        ByVal strEmailId As String, _
        ByVal intGenerationFormat As Integer, _
        ByVal strGenerationTime As String, _
        ByVal intEDIFormat As Integer, _
        ByVal dtLastRun As DateTime, _
        ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim intMax As Long
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_EDI_SETTING).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CustomerData._CUSTOMER_EDI_SETTING, br_ObjTransactions)
                .Item(CustomerData.CSTMR_EDI_STTNG_BIN) = intMax
                .Item(CustomerData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(CustomerData.TO_EML_ID) = strEmailId
                .Item(CustomerData.GNRTN_FRMT) = intGenerationFormat
                .Item(CustomerData.GNRTN_TM) = strGenerationTime
                .Item(CustomerData.EDI_FRMT) = intEDIFormat
                If dtLastRun <> Nothing Then
                    .Item(CustomerData.LST_RN) = dtLastRun
                Else
                    .Item(CustomerData.LST_RN) = DBNull.Value
                End If
                '.Item(CustomerData.NXT_RN_TM) = bv_dtNextRunTime
                'If dtLastRun <> Nothing Then
                '    .Item(CustomerData.NXT_RN_DT) = bv_dtNextRunDateTime
                'Else
                '    .Item(CustomerData.NXT_RN_DT) = DBNull.Value
                'End If
            End With
            objData.InsertRow(dr, EDISettingInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateEDISetting = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetV_CUSTOMER_EDI_SETTING()"
    Public Function GetV_CUSTOMER_EDI_SETTING(ByVal bv_i64Cstmr_ID As Int64) As CustomerDataSet
        Try
            objData = New DataObjects(V_CUSTOMER_EDI_SETTINGSelectQueryByCstmr_id, CustomerData.CSTMR_ID, CStr(bv_i64Cstmr_ID))
            objData.Fill(CType(ds, DataSet), CustomerData._V_CUSTOMER_EDI_SETTING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetV_CUSTOMER_EMAIL_SETTING()"
    Public Function GetV_CUSTOMER_EMAIL_SETTING(ByVal bv_i64Cstmr_ID As Int64) As CustomerDataSet
        Try
            objData = New DataObjects(V_CUSTOMER_EMAIL_SETTINGSelectQueryByCstmr_id, CustomerData.CSTMR_ID, CStr(bv_i64Cstmr_ID))
            objData.Fill(CType(ds, DataSet), CustomerData._V_CUSTOMER_EMAIL_SETTING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getCountEmailSetting"
    Public Function getCountEmailSetting(ByVal bv_i64CSTMR_ID As Long, ByVal intReportID As Integer, ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim hash As New Hashtable
            hash.Add(CustomerData.CSTMR_ID, bv_i64CSTMR_ID)
            hash.Add(CustomerData.ENM_ID, intReportID)
            objData = New DataObjects(EmailSettingCountQuery, hash)
            Return objData.ExecuteScalar(br_ObjTransactions)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetReportType TABLE NAME:ENUM"
    Public Function GetReportType(ByVal bv_strReportValeus As String) As CustomerDataSet
        Try
            objData = New DataObjects(String.Concat(EnumSelectQuery, bv_strReportValeus, ") ORDER BY ENM_ID"))
            objData.Fill(CType(ds, DataSet), CustomerData._REPORT_NAME)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateEmailSetting"
    Public Function CreateEmailSetting(ByVal bv_i64CSTMR_ID As Int64, _
                                       ByVal bv_i64RPRT_ID As Int64, _
                                       ByVal bv_strGNRTN_TM As String, _
                                       ByVal bv_strTO_EML As String, _
                                       ByVal bv_strCC_EML As String, _
                                       ByVal bv_strSBJCT_VCR As String, _
                                       ByVal bv_i32PRDC_DT_ID As Int32, _
                                       ByVal bv_i32PRDC_DY_ID As Int32, _
                                       ByVal bv_i32PRDC_FLTR_ID As Int32, _
                                       ByVal bv_blnACTV_BT As Boolean, _
                                       ByVal bv_dtNXT_RN_DT_TM As DateTime, _
                                       ByVal bv_dtLST_RN_DT_TM As DateTime, _
                                       ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            'Dim dtNxtRun As Date
            Dim intMax As Long
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_EMAIL_SETTING).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CustomerData._CUSTOMER_EMAIL_SETTING, br_ObjTransactions)
                .Item(CustomerData.CSTMR_EML_STTNG_BIN) = intMax
                .Item(CustomerData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(CustomerData.RPRT_ID) = bv_i64RPRT_ID
                .Item(CustomerData.GNRTN_TM) = bv_strGNRTN_TM
                .Item(CustomerData.TO_EML) = bv_strTO_EML
                .Item(CustomerData.CC_EML) = bv_strCC_EML
                .Item(CustomerData.SBJCT_VCR) = bv_strSBJCT_VCR
                If bv_i32PRDC_DT_ID > 0 Then
                    .Item(CustomerData.PRDC_DT_ID) = bv_i32PRDC_DT_ID
                Else
                    .Item(CustomerData.PRDC_DT_ID) = DBNull.Value
                End If
                If bv_i32PRDC_DY_ID > 0 Then
                    .Item(CustomerData.PRDC_DY_ID) = bv_i32PRDC_DY_ID
                Else
                    .Item(CustomerData.PRDC_DY_ID) = DBNull.Value
                End If
                If bv_i32PRDC_FLTR_ID > 0 Then
                    .Item(CustomerData.PRDC_FLTR_ID) = bv_i32PRDC_FLTR_ID
                Else
                    .Item(CustomerData.PRDC_FLTR_ID) = DBNull.Value
                End If
                .Item(CustomerData.ACTV_BT) = bv_blnACTV_BT
                .Item(CustomerData.NXT_RN_DT_TM) = bv_dtNXT_RN_DT_TM
                .Item(CustomerData.LST_RN_DT_TM) = bv_dtLST_RN_DT_TM
            End With
            objData.InsertRow(dr, EmailSettingInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateEmailSetting = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateEmailSetting"
    Public Function UpdateEmailSetting(ByVal bv_i64CSTMR_ID As Int64, _
                                       ByVal intReportID As Integer, _
                                       ByVal strGenerationTime As String, _
                                       ByVal strToEmail As String, _
                                       ByVal strCC As String, _
                                       ByVal strBCC As String, _
                                       ByVal strSubject As String, _
                                       ByVal strActive As String, _
                                       ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_EMAIL_SETTING).NewRow()
            With dr
                .Item(CustomerData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(CustomerData.ENM_ID) = intReportID
                .Item(CustomerData.GNRTN_TM) = strGenerationTime
                .Item(CustomerData.TO_EML) = strToEmail
                .Item(CustomerData.CC_EML) = strCC
                '  .Item(CustomerData.BCC_EML) = strBCC
                .Item(CustomerData.SBJCT_VCR) = strSubject
                .Item(CustomerData.ACTV_BT) = strActive
            End With
            UpdateEmailSetting = objData.UpdateRow(dr, EmailSettingUpdateQuery, br_ObjTransactions)

            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeletePreviousEmailSetting() "

    Public Function DeletePreviousEmailSetting(ByVal bv_i64CSTMR_ID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(CustomerData._CUSTOMER_EMAIL_SETTING).NewRow()
            With dr
                .Item(CustomerData.CSTMR_ID) = bv_i64CSTMR_ID
            End With
            DeletePreviousEmailSetting = objData.DeleteRow(dr, CustomerEmailSettingDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateCustomerTransportation() TABLE NAME:Customer_Transportation"

    Public Function CreateCustomerTransportation(ByVal bv_i64CustomerId As Int64, _
                                                 ByVal bv_i64RouteId As Int64, _
                                                 ByVal bv_dblEmptyTripRate As Double, _
                                                 ByVal bv_dblFullTripRate As Double, _
                                                 ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_TRANSPORTATION).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CustomerData._CUSTOMER_TRANSPORTATION, br_objTransaction)
                .Item(CustomerData.CSTMR_TRNSPRTTN_ID) = intMax
                .Item(CustomerData.CSTMR_ID) = bv_i64CustomerId
                .Item(CustomerData.RT_ID) = bv_i64RouteId
                .Item(CustomerData.EMPTY_TRP_RT_NC) = bv_dblEmptyTripRate
                .Item(CustomerData.FLL_TRP_RT_NC) = bv_dblFullTripRate
            End With
            objData.InsertRow(dr, Customer_TransportationInsertQuery, br_objTransaction)
            dr = Nothing
            CreateCustomerTransportation = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateCustomerRental() TABLE NAME:Customer_Rental"

    Public Function CreateCustomerRental(ByVal bv_i64CustomerId As Int64, _
                                         ByVal bv_strContractReferenceNo As String, _
                                         ByVal bv_datContractStart As DateTime, _
                                         ByVal bv_datContractEnd As DateTime, _
                                         ByVal bv_intMinTenureDays As Int64, _
                                         ByVal bv_dblRentalPerDay As Double, _
                                         ByVal bv_dblHandlingOut As Double, _
                                         ByVal bv_dblHandlingIn As Double, _
                                         ByVal bv_dblOnHireSurvey As Double, _
                                         ByVal bv_dblOffHireSurvey As Double, _
                                         ByVal bv_strRemarks As String, _
                                         ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_RENTAL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CustomerData._CUSTOMER_RENTAL, br_objTransaction)
                .Item(CustomerData.CSTMR_RNTL_ID) = intMax
                .Item(CustomerData.CSTMR_ID) = bv_i64CustomerId
                If bv_strContractReferenceNo <> Nothing Then
                    .Item(CustomerData.CNTRCT_RFRNC_NO) = bv_strContractReferenceNo
                Else
                    .Item(CustomerData.CNTRCT_RFRNC_NO) = DBNull.Value
                End If
                If bv_datContractStart <> Nothing AndAlso bv_datContractStart <> sqlDateDbnull AndAlso bv_datContractStart <> sqlDbnull Then
                    .Item(CustomerData.CNTRCT_STRT_DT) = bv_datContractStart
                Else
                    .Item(CustomerData.CNTRCT_STRT_DT) = DBNull.Value
                End If
                If bv_datContractEnd <> Nothing AndAlso bv_datContractEnd <> sqlDateDbnull AndAlso bv_datContractEnd <> sqlDbnull Then
                    .Item(CustomerData.CNTRCT_END_DT) = bv_datContractEnd
                Else
                    .Item(CustomerData.CNTRCT_END_DT) = DBNull.Value
                End If
                If bv_intMinTenureDays <> 0 AndAlso bv_intMinTenureDays <> Nothing Then
                    .Item(CustomerData.MN_TNR_DY) = bv_intMinTenureDays
                Else
                    .Item(CustomerData.MN_TNR_DY) = 0
                End If
                If bv_dblRentalPerDay <> 0 AndAlso bv_dblRentalPerDay <> Nothing Then
                    .Item(CustomerData.RNTL_PR_DY) = bv_dblRentalPerDay
                Else
                    .Item(CustomerData.RNTL_PR_DY) = 0
                End If
                If bv_dblHandlingOut <> 0 AndAlso bv_dblHandlingOut <> Nothing Then
                    .Item(CustomerData.HNDLNG_OT) = bv_dblHandlingOut
                Else
                    .Item(CustomerData.HNDLNG_OT) = 0
                End If
                If bv_dblHandlingIn <> 0 AndAlso bv_dblHandlingIn <> Nothing Then
                    .Item(CustomerData.HNDLNG_IN) = bv_dblHandlingIn
                Else
                    .Item(CustomerData.HNDLNG_IN) = 0
                End If
                If bv_dblOnHireSurvey <> 0 And bv_dblOnHireSurvey <> Nothing Then
                    .Item(CustomerData.ON_HR_SRVY) = bv_dblOnHireSurvey
                Else
                    .Item(CustomerData.ON_HR_SRVY) = 0
                End If
                If bv_dblOffHireSurvey <> 0 AndAlso bv_dblOffHireSurvey <> Nothing Then
                    .Item(CustomerData.OFF_HR_SRVY) = bv_dblOffHireSurvey
                Else
                    .Item(CustomerData.OFF_HR_SRVY) = 0
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(CustomerData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CustomerData.RMRKS_VC) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Customer_RentalInsertQuery, br_objTransaction)
            dr = Nothing
            CreateCustomerRental = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



#Region "DeleteCustomerRental() TABLE NAME:Customer_Rental"

    Public Function DeleteCustomerRental(ByVal bv_strCustomerRentalID As Int64, ByRef br_objTransaction As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(CustomerData._CUSTOMER_RENTAL).NewRow()
            With dr
                .Item(CustomerData.CSTMR_RNTL_ID) = bv_strCustomerRentalID
            End With
            DeleteCustomerRental = objData.DeleteRow(dr, Customer_RentalDeleteQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCustomerTransportation() TABLE NAME:Customer_Transportation"

    Public Function UpdateCustomerTransportation(ByVal bv_i64CustomerTransportationID As Int64, _
                                                 ByVal bv_i64CustomerId As Int64, _
                                                 ByVal bv_i64RouteId As Int64, _
                                                 ByVal bv_dblEmptyTripRate As Double, _
                                                 ByVal bv_dblFullTripRate As Double, _
                                                 ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_TRANSPORTATION).NewRow()
            With dr
                .Item(CustomerData.CSTMR_TRNSPRTTN_ID) = bv_i64CustomerTransportationID
                .Item(CustomerData.CSTMR_ID) = bv_i64CustomerId
                .Item(CustomerData.RT_ID) = bv_i64RouteId
                .Item(CustomerData.EMPTY_TRP_RT_NC) = bv_dblEmptyTripRate
                .Item(CustomerData.FLL_TRP_RT_NC) = bv_dblFullTripRate
            End With
            UpdateCustomerTransportation = objData.UpdateRow(dr, Customer_TransportationUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCustomerRental() TABLE NAME:Customer_Rental"

    Public Function UpdateCustomerRental(ByVal bv_i64CustomerRentalID As Int64, _
                                         ByVal bv_i64CustomerId As Int64, _
                                         ByVal bv_strContractReferenceNo As String, _
                                         ByVal bv_datContractStart As DateTime, _
                                         ByVal bv_datContractEnd As DateTime, _
                                         ByVal bv_intMinTenureDays As Int64, _
                                         ByVal bv_dblRentalPerDay As Double, _
                                         ByVal bv_dblHandlingOut As Double, _
                                         ByVal bv_dblHandlingIn As Double, _
                                         ByVal bv_dblOnHireSurvey As Double, _
                                         ByVal bv_dblOffHireSurvey As Double, _
                                         ByVal bv_strRemarks As String, _
                                         ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_RENTAL).NewRow()
            With dr
                .Item(CustomerData.CSTMR_RNTL_ID) = bv_i64CustomerRentalID
                .Item(CustomerData.CSTMR_ID) = bv_i64CustomerId

                If bv_strContractReferenceNo <> Nothing Then
                    .Item(CustomerData.CNTRCT_RFRNC_NO) = bv_strContractReferenceNo
                Else
                    .Item(CustomerData.CNTRCT_RFRNC_NO) = DBNull.Value
                End If
                If bv_datContractStart <> Nothing AndAlso bv_datContractStart <> sqlDateDbnull AndAlso bv_datContractStart <> sqlDbnull Then
                    .Item(CustomerData.CNTRCT_STRT_DT) = bv_datContractStart
                Else
                    .Item(CustomerData.CNTRCT_STRT_DT) = DBNull.Value
                End If
                If bv_datContractEnd <> Nothing AndAlso bv_datContractEnd <> sqlDateDbnull AndAlso bv_datContractEnd <> sqlDbnull Then
                    .Item(CustomerData.CNTRCT_END_DT) = bv_datContractEnd
                Else
                    .Item(CustomerData.CNTRCT_END_DT) = DBNull.Value
                End If
                If bv_intMinTenureDays <> 0 AndAlso bv_intMinTenureDays <> Nothing Then
                    .Item(CustomerData.MN_TNR_DY) = bv_intMinTenureDays
                Else
                    .Item(CustomerData.MN_TNR_DY) = 0
                End If
                If bv_dblRentalPerDay <> 0 AndAlso bv_dblRentalPerDay <> Nothing Then
                    .Item(CustomerData.RNTL_PR_DY) = bv_dblRentalPerDay
                Else
                    .Item(CustomerData.RNTL_PR_DY) = 0
                End If
                If bv_dblHandlingOut <> 0 Then
                    .Item(CustomerData.HNDLNG_OT) = bv_dblHandlingOut
                Else
                    .Item(CustomerData.HNDLNG_OT) = 0
                End If
                If bv_dblHandlingIn <> 0 Then
                    .Item(CustomerData.HNDLNG_IN) = bv_dblHandlingIn
                Else
                    .Item(CustomerData.HNDLNG_IN) = 0
                End If
                If bv_dblOnHireSurvey <> 0 Then
                    .Item(CustomerData.ON_HR_SRVY) = bv_dblOnHireSurvey
                Else
                    .Item(CustomerData.ON_HR_SRVY) = 0
                End If
                If bv_dblOffHireSurvey <> 0 Then
                    .Item(CustomerData.OFF_HR_SRVY) = bv_dblOffHireSurvey
                Else
                    .Item(CustomerData.OFF_HR_SRVY) = 0
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(CustomerData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CustomerData.RMRKS_VC) = DBNull.Value
                End If
            End With
            UpdateCustomerRental = objData.UpdateRow(dr, Customer_RentalUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "DeleteCustomerTransportation() TABLE NAME:Customer_Transportation"

    Public Function DeleteCustomerTransportation(ByVal bv_dblCustomerTransportationID As Int64, _
                                                 ByRef br_objTransaction As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(CustomerData._CUSTOMER_TRANSPORTATION).NewRow()
            With dr
                .Item(CustomerData.CSTMR_TRNSPRTTN_ID) = bv_dblCustomerTransportationID
            End With
            DeleteCustomerTransportation = objData.DeleteRow(dr, Customer_TransportationDeleteQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRouteTripRateById() TABLE NAME:V_ROUTE"

    Public Function GetRouteTripRateById(ByVal bv_i64RouteId As Int64, _
                                         ByVal bv_intDepotId As Int32) As CustomerDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(CustomerData.RT_ID, bv_i64RouteId)
            hshparameters.Add(CustomerData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(V_ROUTESelectQueryById, hshparameters)
            objData.Fill(CType(ds, DataSet), CustomerData._V_ROUTE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRentalGateoutDetails()  Table Name: Rental_Entry"

    Public Function GetRentalGateoutDetails(ByVal bv_strContractRefNo As String) As String
        Try
            objData = New DataObjects(CustomerRentalGateoutSelectQuery, CustomerData.CNTRCT_RFRNC_NO, bv_strContractRefNo)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getISOCODEbyCustomer table: Customer"

    Public Function getISOCODEbyCustomer(ByVal bv_intCustomerID As Int64) As CustomerDataSet
        Try
            objData = New DataObjects(getCustomerISOcodebyCustomerIDquery, CustomerData.CSTMR_ID, bv_intCustomerID)
            objData.Fill(CType(ds, DataSet), CustomerData._V_CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getISOCODEbyCustomer(ByVal bv_intCustomerID As Int64, ByRef objTrans As Transactions) As CustomerDataSet
        Try
            objData = New DataObjects(getCustomerISOcodebyCustomerIDquery, CustomerData.CSTMR_ID, bv_intCustomerID)
            objData.Fill(CType(ds, DataSet), CustomerData._V_CUSTOMER, objTrans)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function



#End Region

#Region "GetEnumOnCustomerRates"
    Public Function GetEnumOnCustomerRates() As CustomerDataSet
        Try
            objData = New DataObjects(getCustomerRateFromEnum, CustomerData.ENM_TYP_ID, 50)
            objData.Fill(CType(ds, DataSet), CustomerData._ENUM)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
    
#Region "CreateCustomerCleaningDetail() TABLE NAME:Customer_Storage_Detail"
    Public Function CreateCustomerCleaningDetail(ByVal bv_i64CustomerChargeDetailID As Int64, _
                                                ByVal bv_i64CustomerID As Int64, _
                                                ByVal bv_i32UptoContainers As Int32, _
                                                ByVal bv_strCleaningCharge As Decimal, _
                                                ByVal bv_strRemarks As String, _
                                                ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_CLEANING_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CustomerData._CUSTOMER_CLEANING_DETAIL, br_ObjTransactions)
                .Item(CustomerData.CSTMR_CLNNG_DTL_ID) = intMax
                .Item(CustomerData.CSTMR_CHRG_DTL_ID) = bv_i64CustomerChargeDetailID
                .Item(CustomerData.CSTMR_ID) = bv_i64CustomerID
                .Item(CustomerData.UP_TO_CNTNRS) = bv_i32UptoContainers
                .Item(CustomerData.CLNNG_RT) = bv_strCleaningCharge
                If bv_strRemarks <> Nothing Then
                    .Item(CustomerData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CustomerData.RMRKS_VC) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Customer_Cleaning_DetailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateCustomerCleaningDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCustomerStorageDetail() TABLE NAME:Customer_Storage_Detail"
    Public Function UpdateCustomerCleaningDetail(ByVal bv_i64CustomerCleaningDetailID As Int64, _
                                                ByVal bv_i64CustomerChargeDetailID As Int64, _
                                                ByVal bv_i64CustomerID As Int64, _
                                                ByVal bv_i32UptoContainers As Int32, _
                                                ByVal bv_strCleaningCharge As Decimal, _
                                                ByVal bv_strRemarks As String, _
                                                ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CustomerData._CUSTOMER_CLEANING_DETAIL).NewRow()
            With dr
                .Item(CustomerData.CSTMR_CLNNG_DTL_ID) = bv_i64CustomerCleaningDetailID
                .Item(CustomerData.CSTMR_CHRG_DTL_ID) = bv_i64CustomerChargeDetailID
                .Item(CustomerData.CSTMR_ID) = bv_i64CustomerID
                .Item(CustomerData.UP_TO_CNTNRS) = bv_i32UptoContainers
                .Item(CustomerData.CLNNG_RT) = bv_strCleaningCharge
                If bv_strRemarks <> Nothing Then
                    .Item(CustomerData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CustomerData.RMRKS_VC) = DBNull.Value
                End If
            End With
            UpdateCustomerCleaningDetail = objData.UpdateRow(dr, Customer_Cleaning_DetailUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteCustomerCleaningDetailByChargeDetailID() TABLE NAME:Customer_Cleaning_Detail"

    Public Function DeleteCustomerCleaningDetailByChargeDetailID(ByVal bv_strCustomerChargeDetailID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(CustomerData._CUSTOMER_STORAGE_DETAIL).NewRow()
            With dr
                .Item(CustomerData.CSTMR_CHRG_DTL_ID) = bv_strCustomerChargeDetailID
            End With
            DeleteCustomerCleaningDetailByChargeDetailID = objData.DeleteRow(dr, Customer_Cleaning_DetailDeleteQueryByChargeDetailID, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteCustomerCleaningDetailByCleaningDetailID() TABLE NAME:Customer_Cleaning_Detail"

    Public Function DeleteCustomerCleaningDetailByCleaningDetailID(ByVal bv_strCustomerCleaningDetailID As Int64, ByRef br_ObjTransactions As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(CustomerData._CUSTOMER_CLEANING_DETAIL).NewRow()
            With dr
                .Item(CustomerData.CSTMR_CLNNG_DTL_ID) = bv_strCustomerCleaningDetailID
            End With
            DeleteCustomerCleaningDetailByCleaningDetailID = objData.DeleteRow(dr, Customer_Cleaning_DetailDeleteQueryByCleaningDetailID, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class
