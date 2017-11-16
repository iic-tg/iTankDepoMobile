
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Public Class Cleanings
#Region "Declaration Part"
    Dim objData As DataObjects
    Private Const CleaningInsertQuery As String = "INSERT INTO CLEANING(CLNNG_ID,CLNNG_CD,CSTMR_ID,EQPMNT_NO,CLNNG_CERT_NO,CHMCL_NM,CLNNG_RT,ORGNL_CLNNG_DT,LST_CLNNG_DT,ORGNL_INSPCTD_DT,LST_INSPCTD_DT,EQPMNT_STTS_ID,CLND_FR_VCR,LCTN_OF_CLNG,EQPMNT_CLNNG_STTS_1,EQPMNT_CLNNG_STTS_2,EQPMNT_CNDTN_ID,VLV_FTTNG_CNDTN,MN_LID_SL_NO,TP_SL_NO,BTTM_SL_NO,INVCNG_PRTY_ID,CSTMR_RFRNC_NO,CLNNG_RFRNC_NO,RMRKS_VC,DPT_ID,GI_TRNSCTN_NO,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,CERT_GNRTD_FLG,IS_CHNG_OF_STTS,ADDTNL_CLNNG_BT,ADDTNL_CLNNG_FLG)VALUES(@CLNNG_ID,@CLNNG_CD,@CSTMR_ID,@EQPMNT_NO,@CLNNG_CERT_NO,@CHMCL_NM,@CLNNG_RT,@ORGNL_CLNNG_DT,@LST_CLNNG_DT,@ORGNL_INSPCTD_DT,@LST_INSPCTD_DT,@EQPMNT_STTS_ID,@CLND_FR_VCR,@LCTN_OF_CLNG,@EQPMNT_CLNNG_STTS_1,@EQPMNT_CLNNG_STTS_2,@EQPMNT_CNDTN_ID,@VLV_FTTNG_CNDTN,@MN_LID_SL_NO,@TP_SL_NO,@BTTM_SL_NO,@INVCNG_PRTY_ID,@CSTMR_RFRNC_NO,@CLNNG_RFRNC_NO,@RMRKS_VC,@DPT_ID,@GI_TRNSCTN_NO,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@CERT_GNRTD_FLG,@IS_CHNG_OF_STTS,@ADDTNL_CLNNG_BT,@ADDTNL_CLNNG_FLG)"
    Private Const Cleaning_ChargeInsertQuery As String = "INSERT INTO CLEANING_CHARGE(CLNNG_CHRG_ID,EQPMNT_NO,CSTMR_ID,INVCNG_PRTY_ID,CLNNG_ID,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RT,BLLNG_FLG,CLNNG_CERT_NO,ACTV_BT,DPT_ID,GI_TRNSCTN_NO,SLB_RT_BT)VALUES(@CLNNG_CHRG_ID,@EQPMNT_NO,@CSTMR_ID,@INVCNG_PRTY_ID,@CLNNG_ID,@ORGNL_CLNNG_DT,@ORGNL_INSPCTD_DT,@CLNNG_RT,@BLLNG_FLG,@CLNNG_CERT_NO,@ACTV_BT,@DPT_ID,@GI_TRNSCTN_NO,@SLB_RT_BT)"
    Private Const Activity_StatusUpdateQuery As String = "UPDATE ACTIVITY_STATUS SET CLNNG_DT=@CLNNG_DT,CLNNG_INSPCTN_REF_NO=@CLNNG_INSPCTN_REF_NO, INSPCTN_DT=@INSPCTN_DT,EQPMNT_STTS_ID=@EQPMNT_STTS_ID, ACTVTY_NAM=@ACTVTY_NAM, ACTVTY_DT=@ACTVTY_DT,GI_RF_NO=@GI_RF_NO, RMRKS_VC=@RMRKS_VC, SCHDL_DT=@SCHDL_DT WHERE  DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Activity_StatusUpdateQuery_ins As String = "UPDATE ACTIVITY_STATUS SET CLNNG_DT=@CLNNG_DT,CLNNG_INSPCTN_REF_NO=@CLNNG_INSPCTN_REF_NO, INSPCTN_DT=@INSPCTN_DT, ACTVTY_DT=@ACTVTY_DT,GI_RF_NO=@GI_RF_NO, RMRKS_VC=@RMRKS_VC, SCHDL_DT=@SCHDL_DT WHERE  DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Activity_StatusUpdateQuery_insMySub As String = "UPDATE ACTIVITY_STATUS SET CLNNG_INSPCTN_REF_NO=@CLNNG_INSPCTN_REF_NO, GI_RF_NO=@GI_RF_NO, RMRKS_VC=@RMRKS_VC, SCHDL_DT=@SCHDL_DT WHERE  DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const ActivityStatusDetailsSelectQuery As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO AND CLNNG_ID=@CLNNG_ID) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO AND CLNNG_ID=@CLNNG_ID) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const CleaningUpdateQuery As String = "UPDATE CLEANING SET CLNNG_RT=@CLNNG_RT,ORGNL_INSPCTD_DT=@ORGNL_INSPCTD_DT, LST_CLNNG_DT=@LST_CLNNG_DT, LST_INSPCTD_DT=@LST_INSPCTD_DT, CLND_FR_VCR=@CLND_FR_VCR, LCTN_OF_CLNG=@LCTN_OF_CLNG, EQPMNT_CLNNG_STTS_1=@EQPMNT_CLNNG_STTS_1, EQPMNT_CLNNG_STTS_2=@EQPMNT_CLNNG_STTS_2, EQPMNT_CNDTN_ID=@EQPMNT_CNDTN_ID, VLV_FTTNG_CNDTN=@VLV_FTTNG_CNDTN, MN_LID_SL_NO=@MN_LID_SL_NO, TP_SL_NO=@TP_SL_NO, BTTM_SL_NO=@BTTM_SL_NO, INVCNG_PRTY_ID=@INVCNG_PRTY_ID, CSTMR_RFRNC_NO=@CSTMR_RFRNC_NO, CLNNG_RFRNC_NO=@CLNNG_RFRNC_NO, RMRKS_VC=@RMRKS_VC,IS_CHNG_OF_STTS=@IS_CHNG_OF_STTS, DPT_ID=@DPT_ID,MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT,EQPMNT_STTS_ID = @EQPMNT_STTS_ID,ADDTNL_CLNNG_BT=@ADDTNL_CLNNG_BT,ADDTNL_CLNNG_FLG=@ADDTNL_CLNNG_FLG WHERE CLNNG_ID=@CLNNG_ID"
    Private Const CleaningUpdateQuery_MYSub As String = "UPDATE CLEANING SET CLNNG_RT=@CLNNG_RT, CLND_FR_VCR=@CLND_FR_VCR, LCTN_OF_CLNG=@LCTN_OF_CLNG, EQPMNT_CLNNG_STTS_1=@EQPMNT_CLNNG_STTS_1, EQPMNT_CLNNG_STTS_2=@EQPMNT_CLNNG_STTS_2, EQPMNT_CNDTN_ID=@EQPMNT_CNDTN_ID, VLV_FTTNG_CNDTN=@VLV_FTTNG_CNDTN, MN_LID_SL_NO=@MN_LID_SL_NO, TP_SL_NO=@TP_SL_NO, BTTM_SL_NO=@BTTM_SL_NO, INVCNG_PRTY_ID=@INVCNG_PRTY_ID, CSTMR_RFRNC_NO=@CSTMR_RFRNC_NO, CLNNG_RFRNC_NO=@CLNNG_RFRNC_NO, RMRKS_VC=@RMRKS_VC,IS_CHNG_OF_STTS=@IS_CHNG_OF_STTS, DPT_ID=@DPT_ID,MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT,EQPMNT_STTS_ID = @EQPMNT_STTS_ID,ADDTNL_CLNNG_BT=@ADDTNL_CLNNG_BT,ADDTNL_CLNNG_FLG=@ADDTNL_CLNNG_FLG WHERE CLNNG_ID=@CLNNG_ID"
    'Added IMO and UN # for CR NO -002[MMS]
    Private Const CleaningDetailsSelectQuery As String = "SELECT CLNNG_ID,CLNNG_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,CLNNG_CERT_NO,PRDCT_ID,PRDCT_CD,CHMCL_NM,CLNNG_RT,ORGNL_CLNNG_DT,LST_CLNNG_DT,ORGNL_INSPCTD_DT,LST_INSPCTD_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC,CLND_FR_VCR,LCTN_OF_CLNG,EQPMNT_CLNNG_STTS_1,EQPMNT_CLNNG_STTS_1_CD,EQPMNT_CLNNG_STTS_2,EQPMNT_CLNNG_STTS_2_CD,EQPMNT_CNDTN_CD,EQPMNT_CNDTN_ID,VLV_FTTNG_CNDTN,VLV_FTTNG_CNDTN_CD,MN_LID_SL_NO,TP_SL_NO,BTTM_SL_NO,INVCNG_PRTY_ID,INVCNG_PRTY_CD,CSTMR_RFRNC_NO,CLNNG_RFRNC_NO,RMRKS_VC,DPT_ID,GI_TRNSCTN_NO,CRTD_BY,(SELECT EQPMNT_TYP_CD FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_ID=(SELECT EQPMNT_TYP_ID FROM ACTIVITY_STATUS WHERE GI_TRNSCTN_NO=VC.GI_TRNSCTN_NO)) AS EQPMNT_TYP_CD,MDFD_BY,PRDCT_DSCRPTN_VC,IMO_CLSS,UN_NO FROM  V_CLEANING VC WHERE CLNNG_ID=@CLNNG_ID AND DPT_ID=@DPT_ID"
    Private Const SelectDepotDetailQuery As String = "SELECT DPT_ID,DPT_CD,DPT_NAM,CNTCT_PRSN_NAM,ADDRSS_LN1_VC,ADDRSS_LN2_VC,ADDRSS_LN3_VC,VT_NO,EML_ID,PHN_NO,FX_NO,CMPNY_LG_PTH,MDFD_BY,MDFD_DT FROM DEPOT WHERE DPT_ID=@DPT_ID"
    Private Const UpdateCleaningCertificateNoQuery As String = "UPDATE CLEANING SET CLNNG_CERT_NO=@CLNNG_CERT_NO,CERT_GNRTD_FLG=@CERT_GNRTD_FLG WHERE CLNNG_ID=@CLNNG_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CERT_GNRTD_FLG=0"
    Private Const UpdateCleaningChargeQuery As String = "UPDATE CLEANING_CHARGE SET CLNNG_CERT_NO=@CLNNG_CERT_NO WHERE CLNNG_ID=@CLNNG_ID AND DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Cleaning_ChargeUpdateQuery As String = "UPDATE CLEANING_CHARGE SET CSTMR_ID=@CSTMR_ID,INVCNG_PRTY_ID=@INVCNG_PRTY_ID,ORGNL_CLNNG_DT=@ORGNL_CLNNG_DT, ORGNL_INSPCTD_DT=@ORGNL_INSPCTD_DT, CLNNG_RT=@CLNNG_RT,DPT_ID=@DPT_ID WHERE CLNNG_ID=@CLNNG_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Cleaning_ChargeUpdateQuery_Ins_MySub As String = "UPDATE CLEANING_CHARGE SET CSTMR_ID=@CSTMR_ID,INVCNG_PRTY_ID=@INVCNG_PRTY_ID, CLNNG_RT=@CLNNG_RT,DPT_ID=@DPT_ID WHERE CLNNG_ID=@CLNNG_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    'Private Const TrackingUpdateQuery As String = "UPDATE TRACKING SET ACTVTY_DT=@ACTVTY_DT, ACTVTY_RMRKS=@ACTVTY_RMRKS, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT , EQPMNT_INFRMTN_RMRKS_VC=@EQPMNT_INFRMTN_RMRKS_VC WHERE ACTVTY_NO=@ACTVTY_NO AND ACTVTY_NAM=@ACTVTY_NAM AND DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const TrackingUpdateQuery As String = "UPDATE TRACKING SET ACTVTY_DT=@ACTVTY_DT, ACTVTY_RMRKS=@ACTVTY_RMRKS,INVCNG_PRTY_ID=@INVCNG_PRTY_ID, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT , EQPMNT_INFRMTN_RMRKS_VC=@EQPMNT_INFRMTN_RMRKS_VC WHERE ACTVTY_NO=@ACTVTY_NO AND ACTVTY_NAM=@ACTVTY_NAM AND DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const TrackingUpdateQuery_InsMySub As String = "UPDATE TRACKING SET ACTVTY_RMRKS=@ACTVTY_RMRKS,INVCNG_PRTY_ID=@INVCNG_PRTY_ID, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT , EQPMNT_INFRMTN_RMRKS_VC=@EQPMNT_INFRMTN_RMRKS_VC WHERE ACTVTY_NO=@ACTVTY_NO AND ACTVTY_NAM=@ACTVTY_NAM AND DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const CleaningChargeSelectQuery As String = "SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const CurrencyCodeSelectQuery As String = "SELECT CRRNCY_CD,CRRNCY_ID FROM CURRENCY WHERE CRRNCY_ID=(SELECT BS_CRRNCY_ID FROM INVOICING_PARTY WHERE INVCNG_PRTY_ID=@INVCNG_PRTY_ID AND DPT_ID=@DPT_ID)"
    Private Const Cleaning_InfoCountQuery As String = "SELECT COUNT(CLNNG_ID) FROM CLEANING WHERE CLNNG_ID=@CLNNG_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CERT_GNRTD_FLG=1"
    Private Const TrackingCleaningCertUpdateQuery As String = "UPDATE TRACKING SET RFRNC_NO=@RFRNC_NO WHERE ACTVTY_NAM IN ('Cleaning','Inspection') AND ACTVTY_NO=@ACTVTY_NO AND DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
    Private Const Activity_StatusUpdateRemarksQuery As String = "UPDATE ACTIVITY_STATUS SET RMRKS_VC=@RMRKS_VC,GI_RF_NO=@GI_RF_NO WHERE  DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Cleaning_ChargeBilledSelectQuery As String = "SELECT CLNNG_CHRG_ID,EQPMNT_NO,CSTMR_ID,INVCNG_PRTY_ID,CLNNG_ID,ORGNL_CLNNG_DT,ORGNL_INSPCTD_DT,CLNNG_RT,BLLNG_FLG,CLNNG_CERT_NO,ACTV_BT,DPT_ID,GI_TRNSCTN_NO,DRFT_INVC_NO,FNL_INVC_NO FROM CLEANING_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CLNNG_ID=@CLNNG_ID AND DPT_ID=@DPT_ID AND BLLNG_FLG='B'"
    Private Const Activity_StatusUpdateStatusQuery As String = "UPDATE ACTIVITY_STATUS SET EQPMNT_STTS_ID = @EQPMNT_STTS_ID,SCHDL_DT=@SCHDL_DT WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const Cleaning_AdditionalCleaning_UpdateQuery As String = "UPDATE CLEANING SET ADDTNL_CLNNG_BT= @ADDTNL_CLNNG_BT,RMRKS_VC=@RMRKS_VC,EQPMNT_STTS_ID=@EQPMNT_STTS_ID,ADDTNL_CLNNG_FLG=@ADDTNL_CLNNG_FLG,MN_LID_SL_NO=@MN_LID_SL_NO,TP_SL_NO=@TP_SL_NO,BTTM_SL_NO=@BTTM_SL_NO,CLNNG_RFRNC_NO=@CLNNG_RFRNC_NO WHERE CLNNG_ID=@CLNNG_ID AND DPT_ID=@DPT_ID"
    Private Const CleaningUpdateQueryBy_AdditionalCleaningFlag As String = "UPDATE CLEANING SET CLNNG_RT=@CLNNG_RT, LST_CLNNG_DT=@LST_CLNNG_DT, LST_INSPCTD_DT=@LST_INSPCTD_DT, CLND_FR_VCR=@CLND_FR_VCR, LCTN_OF_CLNG=@LCTN_OF_CLNG, EQPMNT_CLNNG_STTS_1=@EQPMNT_CLNNG_STTS_1, EQPMNT_CLNNG_STTS_2=@EQPMNT_CLNNG_STTS_2, EQPMNT_CNDTN_ID=@EQPMNT_CNDTN_ID, VLV_FTTNG_CNDTN=@VLV_FTTNG_CNDTN, MN_LID_SL_NO=@MN_LID_SL_NO, TP_SL_NO=@TP_SL_NO, BTTM_SL_NO=@BTTM_SL_NO, INVCNG_PRTY_ID=@INVCNG_PRTY_ID, CSTMR_RFRNC_NO=@CSTMR_RFRNC_NO, CLNNG_RFRNC_NO=@CLNNG_RFRNC_NO, RMRKS_VC=@RMRKS_VC, DPT_ID=@DPT_ID,MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT,EQPMNT_STTS_ID = @EQPMNT_STTS_ID,ADDTNL_CLNNG_BT=@ADDTNL_CLNNG_BT,ADDTNL_CLNNG_FLG=@ADDTNL_CLNNG_FLG,ORGNL_INSPCTD_DT=@ORGNL_INSPCTD_DT WHERE CLNNG_ID=@CLNNG_ID"
    Private Const Cleaning_ChargeUpdateQueryByCleaningId As String = "UPDATE CLEANING_CHARGE SET CLNNG_RT=@CLNNG_RT WHERE CLNNG_ID=@CLNNG_ID"
    Private ds As CleaningDataSet
    'Cleaning Certificate num updated in Cleaning Charge 
    Private Const CleaningCertficateUpdate_Charge As String = "UPDATE CLEANING_CHARGE SET CLNNG_CERT_NO=@CLNNG_CERT_NO WHERE CLNNG_ID=@CLNNG_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    'CR : Cleaning & Inspection - Split
    Private Const CleaningUpdateQuery_Clean As String = "UPDATE CLEANING SET CLNNG_RT=@CLNNG_RT, LST_CLNNG_DT=@LST_CLNNG_DT, EQPMNT_STTS_ID=@EQPMNT_STTS_ID, CLNNG_RFRNC_NO=@CLNNG_RFRNC_NO, RMRKS_VC=@RMRKS_VC, DPT_ID=@DPT_ID, GI_TRNSCTN_NO=@GI_TRNSCTN_NO, ADDTNL_CLNNG_BT=@ADDTNL_CLNNG_BT, ADDTNL_CLNNG_FLG=@ADDTNL_CLNNG_FLG, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT  WHERE CLNNG_ID=@CLNNG_ID"
    Private Const Cleaning_ChargeUpdateQuery_Clean As String = "UPDATE CLEANING_CHARGE SET CSTMR_ID=@CSTMR_ID, ORGNL_CLNNG_DT=@ORGNL_CLNNG_DT, CLNNG_RT=@CLNNG_RT,DPT_ID=@DPT_ID WHERE CLNNG_ID=@CLNNG_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Activity_StatusUpdateRemarksQuery_Clean As String = "UPDATE ACTIVITY_STATUS SET RMRKS_VC=@RMRKS_VC WHERE  DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const CleaningUpdateQueryBy_AdditionalCleaningFlag_Clean As String = "UPDATE CLEANING SET CLNNG_RT=@CLNNG_RT, LST_CLNNG_DT=@LST_CLNNG_DT, EQPMNT_STTS_ID=@EQPMNT_STTS_ID,INVCNG_PRTY_ID=@INVCNG_PRTY_ID, CLNNG_RFRNC_NO=@CLNNG_RFRNC_NO, RMRKS_VC=@RMRKS_VC, DPT_ID=@DPT_ID, GI_TRNSCTN_NO=@GI_TRNSCTN_NO, ADDTNL_CLNNG_BT=@ADDTNL_CLNNG_BT, ADDTNL_CLNNG_FLG=@ADDTNL_CLNNG_FLG, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE CLNNG_ID=@CLNNG_ID"
    Private Const Activity_StatusUpdateQuery_Clean As String = "UPDATE ACTIVITY_STATUS SET ACTVTY_DT=@ACTVTY_DT,CLNNG_DT=@CLNNG_DT, EQPMNT_STTS_ID=@EQPMNT_STTS_ID, ACTVTY_NAM=@ACTVTY_NAM, RMRKS_VC=@RMRKS_VC  WHERE  DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Cleaning_ChargeInsertQuery_Clean As String = "INSERT INTO CLEANING_CHARGE(CLNNG_CHRG_ID,EQPMNT_NO,CSTMR_ID,CLNNG_ID,ORGNL_CLNNG_DT,CLNNG_RT,BLLNG_FLG,ACTV_BT,DPT_ID,GI_TRNSCTN_NO,SLB_RT_BT)VALUES(@CLNNG_CHRG_ID,@EQPMNT_NO,@CSTMR_ID,@CLNNG_ID,@ORGNL_CLNNG_DT,@CLNNG_RT,@BLLNG_FLG,@ACTV_BT,@DPT_ID,@GI_TRNSCTN_NO,@SLB_RT_BT)"
    Private Const CleaningInsertQuery_Clean As String = "INSERT INTO CLEANING (CLNNG_ID,CLNNG_CD,CSTMR_ID,EQPMNT_NO,CHMCL_NM,CLNNG_RT,ORGNL_CLNNG_DT,LST_CLNNG_DT,EQPMNT_STTS_ID,CLNNG_RFRNC_NO,RMRKS_VC,DPT_ID, GI_TRNSCTN_NO,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,CERT_GNRTD_FLG,ADDTNL_CLNNG_BT,ADDTNL_CLNNG_FLG)  VALUES(@CLNNG_ID,@CLNNG_CD,@CSTMR_ID,@EQPMNT_NO,@CHMCL_NM,@CLNNG_RT,@ORGNL_CLNNG_DT, @LST_CLNNG_DT,@EQPMNT_STTS_ID,@CLNNG_RFRNC_NO,@RMRKS_VC,@DPT_ID,@GI_TRNSCTN_NO,@CRTD_BY,@CRTD_DT, @MDFD_BY,@MDFD_DT,@CERT_GNRTD_FLG,@ADDTNL_CLNNG_BT,@ADDTNL_CLNNG_FLG)"
    Private Const TrackingUpdateQuery_Clean As String = "UPDATE TRACKING SET ACTVTY_DT=@ACTVTY_DT, ACTVTY_RMRKS=@ACTVTY_RMRKS, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT , EQPMNT_INFRMTN_RMRKS_VC=@EQPMNT_INFRMTN_RMRKS_VC WHERE ACTVTY_NO=@ACTVTY_NO AND ACTVTY_NAM=@ACTVTY_NAM AND DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Cleaning_DeleteQuery_Ins As String = "DELETE FROM CLEANING WHERE CLNNG_ID=@CLNNG_ID AND DPT_ID=@DPT_ID"
    Private Const Cleaning_Charge_DeleteQuery_Ins As String = "DELETE FROM CLEANING_CHARGE WHERE CLNNG_ID=@CLNNG_ID AND DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const UpdateCleaning_Inspection_UpdateQuery As String = "UPDATE CLEANING SET CLNNG_RT=@CLNNG_RT, LST_CLNNG_DT=@LST_CLNNG_DT, LST_INSPCTD_DT=@LST_INSPCTD_DT, CLND_FR_VCR=@CLND_FR_VCR, LCTN_OF_CLNG=@LCTN_OF_CLNG, EQPMNT_CLNNG_STTS_1=@EQPMNT_CLNNG_STTS_1, EQPMNT_CLNNG_STTS_2=@EQPMNT_CLNNG_STTS_2, EQPMNT_CNDTN_ID=@EQPMNT_CNDTN_ID, VLV_FTTNG_CNDTN=@VLV_FTTNG_CNDTN, MN_LID_SL_NO=@MN_LID_SL_NO, TP_SL_NO=@TP_SL_NO, BTTM_SL_NO=@BTTM_SL_NO, INVCNG_PRTY_ID=@INVCNG_PRTY_ID, CSTMR_RFRNC_NO=@CSTMR_RFRNC_NO, CLNNG_RFRNC_NO=@CLNNG_RFRNC_NO, RMRKS_VC=@RMRKS_VC,IS_CHNG_OF_STTS=@IS_CHNG_OF_STTS, DPT_ID=@DPT_ID,MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT,EQPMNT_STTS_ID = @EQPMNT_STTS_ID,ADDTNL_CLNNG_BT=@ADDTNL_CLNNG_BT,ADDTNL_CLNNG_FLG=@ADDTNL_CLNNG_FLG,ORGNL_INSPCTD_DT=@ORGNL_INSPCTD_DT WHERE CLNNG_ID=@CLNNG_ID"
    Private Const CustomerEDIValidation_SelectQry As String = "SELECT COUNT(CSTMR_ID) FROM CUSTOMER WHERE CSTMR_CD=@CSTMR_CD AND DPT_ID=@DPT_ID AND XML_BT=1"
    'Private Const GetCleaningInsructionReportDetails_SelectQry As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC, GTN_DT,GI_RF_NO,GI_TRNSCTN_NO,DPT_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC,ACTVTY_NAM,ACTVTY_DT,IMO_CLSS,UN_NO,NXT_TST_DT,NXT_TST_TYP_CD, YRD_LCTN,RMRKS_VC,CLNNG_MTHD_TYP_CD,CLNNG_INSPCTN_NO FROM V_PRINT_CLEANING_INSTRUCTION WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    'Customer Master Changes
    Private Const GetCleaningInsructionReportDetails_SelectQry As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC, GTN_DT,GI_RF_NO,GI_TRNSCTN_NO,DPT_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC,ACTVTY_NAM,ACTVTY_DT,IMO_CLSS,UN_NO,NXT_TST_DT,NXT_TST_TYP_CD, YRD_LCTN,RMRKS_VC,CLNNG_MTHD_TYP_CD,CLNNG_INSPCTN_NO,SHELL,STUBE,(SELECT DPT_CD FROM DEPOT WHERE DPT_ID=V.DPT_ID)DPT_CD FROM V_PRINT_CLEANING_INSTRUCTION V WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const CheckCleaningCombination_SelectQuery As String = "SELECT COUNT(CLNNG_ID) FROM CLEANING WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private Const UpdateCleaning_CleanCombination_UpdateQry As String = "UPDATE CLEANING SET CHMCL_NM=@CHMCL_NM , CLNNG_RT=@CLNNG_RT, ORGNL_CLNNG_DT=@ORGNL_CLNNG_DT, LST_CLNNG_DT=@LST_CLNNG_DT, EQPMNT_STTS_ID=@EQPMNT_STTS_ID, CLNNG_RFRNC_NO=@CLNNG_RFRNC_NO, RMRKS_VC=@RMRKS_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private Const UpdateCleaningCharge_CleanCombination_UpdateQry As String = "UPDATE CLEANING_CHARGE SET ORGNL_CLNNG_DT=@ORGNL_CLNNG_DT, CLNNG_RT=@CLNNG_RT, BLLNG_FLG=@BLLNG_FLG, ACTV_BT=@ACTV_BT,SLB_RT_BT=@SLB_RT_BT WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private Const UpdateActvity_CleaningInspectionRefNo_UpdateQry As String = "UPDATE ACTIVITY_STATUS SET CLNNG_INSPCTN_REF_NO=@CLNNG_INSPCTN_REF_NO WHERE EQPMNT_NO=@EQPMNT_NO"
    Private Const GetCustomerDetailByCustomer_id As String = "SELECT CSTMR_ID,CSTMR_CD ,CSTMR_NAM ,CSTMR_CRRNCY_ID ,CSTMR_CRRNCY_CD ,CNTCT_PRSN_NAM ,CNTCT_ADDRSS ,BLLNG_ADDRSS ,ZP_CD ,PHN_NO ,FX_NO ,INVCNG_EML_ID  FROM V_CUSTOMER WHERE CSTMR_ID=@CSTMR_ID"

    'Cleaning Slab Rate Changes
    Private Const Cleaning_Slab_rate_ByCustomerID As String = "SELECT CSTMR_CLNNG_DTL_ID,CSTMR_CHRG_DTL_ID,CSTMR_ID,UP_TO_CNTNRS,CLNNG_RT,RMRKS_VC FROM V_CUSTOMER_CLEANING_DETAIL WHERE CSTMR_ID=@CSTMR_ID AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
#End Region

#Region "Constructor.."
    Sub New()
        ds = New CleaningDataSet
    End Sub
#End Region

#Region "GetCleaningInfo"
    Public Function GetCleaningInfo(ByVal CleaningId As Integer, _
                                     ByVal bv_intDepotID As Integer, _
                                     ByVal strGI_TRNSCTN_NO As String, _
                                     ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(CleaningData.CLNNG_ID, CleaningId)
            hshTable.Add(CleaningData.DPT_ID, bv_intDepotID)
            hshTable.Add(CleaningData.GI_TRNSCTN_NO, strGI_TRNSCTN_NO)
            objData = New DataObjects(Cleaning_InfoCountQuery, hshTable)
            Return objData.ExecuteScalar(br_ObjTransactions)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCleaningDetails"
    Public Function GetCleaningDetails(ByVal bv_intCleaningid As Int64, ByVal intDPT_ID As Int64) As CleaningDataSet
        Try
            Dim dt As New DataTable
            Dim hshTable As New Hashtable()
            hshTable.Add(CleaningData.CLNNG_ID, bv_intCleaningid)
            hshTable.Add(CleaningData.DPT_ID, intDPT_ID)
            objData = New DataObjects(CleaningDetailsSelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), CleaningData._V_CLEANING)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCleaningDetails"
    Public Function GerCurrencyCode(ByVal bv_intInvoicingPartyid As Int64, ByVal intDPT_ID As Int64) As CleaningDataSet
        Try
            Dim dt As New DataTable
            Dim hshTable As New Hashtable()
            hshTable.Add(CleaningData.INVCNG_PRTY_ID, bv_intInvoicingPartyid)
            hshTable.Add(CleaningData.DPT_ID, intDPT_ID)
            objData = New DataObjects(CurrencyCodeSelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), CleaningData._INVOICING_PARTY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCleaningDetails"
    Public Function GetCleaningDetails(ByVal bv_intCleaningid As Int64, ByVal intDPT_ID As Int64, ByRef br_objTrans As Transactions) As CleaningDataSet
        Try
            Dim dt As New DataTable
            Dim hshTable As New Hashtable()
            hshTable.Add(CleaningData.CLNNG_ID, bv_intCleaningid)
            hshTable.Add(CleaningData.DPT_ID, intDPT_ID)
            objData = New DataObjects(CleaningDetailsSelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), CleaningData._V_CLEANING, br_objTrans)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCustomerDetailsByCustomerId() TABLE NAME:CUSTOMER"
    ''' <summary>
    ''' Get Customer details based on customer ID
    ''' </summary>
    ''' <param name="bv_i64CSTMR_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomerDetailsByCustomerId(ByVal bv_i64CSTMR_id As Int64) As DataTable
        Try
            objData = New DataObjects(GetCustomerDetailByCustomer_id, CleaningData.CSTMR_ID, CStr(bv_i64CSTMR_id))
            objData.Fill(CType(ds, DataSet), CleaningData._V_CUSTOMER)
            Return ds.Tables(CleaningData._V_CUSTOMER)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateCleaningCertificateNo"

    Public Function UpdateCleaningCertificateNo(ByVal bv_intCleaningid As Integer, _
                                                bv_strEquipmentNo As String, _
                                                ByVal bv_intDepotID As Int32, _
                                                ByVal bv_strGITransactionNo As String, _
                                                ByVal bv_CertGenerationFlag As Boolean, _
                                                ByRef strCleaningCertificateNo As String, _
                                                ByRef br_objTrans As Transactions) As String
        Try
            Dim intMax As Integer = CommonUIs.GetIdentityValue(CleaningData._CLEANING_CERTIFICATE, br_objTrans)
            ' strCleaningCertificateNo = CommonUIs.GetIdentityCode(CleaningData._CLEANING_CERTIFICATE, intMax, Now, br_objTrans)
            'From Index Pattern
            '  strCleaningCertificateNo = IndexPatterns.GetIdentityCode(CleaningData._CLEANING, intMax, Now, bv_intDepotID, br_objTrans)

            CommonUIs.GetIdentityValue(CleaningData._CLEANING_INSPECTION, br_objTrans)
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING).NewRow()
            With dr
                .Item(CleaningData.CLNNG_ID) = bv_intCleaningid
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGITransactionNo
                .Item(CleaningData.CLNNG_CERT_NO) = strCleaningCertificateNo
                .Item(CleaningData.CERT_GNRTD_FLG) = bv_CertGenerationFlag
            End With
            UpdateCleaningCertificateNo = objData.UpdateRow(dr, UpdateCleaningCertificateNoQuery, br_objTrans)
            dr = Nothing
            Return strCleaningCertificateNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    'Added for Update Cleaning Certificate Number in Cleaning_Charge

#Region "UpdateCleaningCertificateNo_Charge"

    Public Function UpdateCleaningCertificateNo_Charge(ByVal bv_intCleaningid As Integer, _
                                                bv_strEquipmentNo As String, _
                                                ByVal bv_intDepotID As Int32, _
                                                ByVal bv_strGITransactionNo As String, _
                                                ByVal bv_CertGenerationFlag As Boolean, _
                                                ByRef strCleaningCertificateNo As String, _
                                                ByRef br_objTrans As Transactions) As String
        Try
            CommonUIs.GetIdentityValue(CleaningData._CLEANING_CHARGE, br_objTrans)
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING).NewRow()
            With dr
                .Item(CleaningData.CLNNG_ID) = bv_intCleaningid
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGITransactionNo
                .Item(CleaningData.CLNNG_CERT_NO) = strCleaningCertificateNo
                ' .Item(CleaningData.CERT_GNRTD_FLG) = bv_CertGenerationFlag
            End With
            UpdateCleaningCertificateNo_Charge = objData.UpdateRow(dr, CleaningCertficateUpdate_Charge, br_objTrans)
            dr = Nothing
            Return strCleaningCertificateNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateCleaningCharge"
    Public Function UpdateCleaningChargeCertNo(ByVal intCleaningID As Integer, _
                                                ByVal bv_strEquipmentNo As String, _
                                                ByVal bv_intDepotID As Int32, _
                                                ByVal bv_strGITransactionNo As String, _
                                                ByVal bv_CertCertNo As String, _
                                                ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING_CHARGE).NewRow()
            With dr
                .Item(CleaningData.CLNNG_ID) = intCleaningID
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(CleaningData.DPT_ID) = bv_intDepotID
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGITransactionNo
                .Item(CleaningData.CLNNG_CERT_NO) = bv_CertCertNo
            End With
            UpdateCleaningChargeCertNo = objData.UpdateRow(dr, UpdateCleaningChargeQuery, br_objTrans)
            dr = Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "pub_GetDepotDetail"
    Public Function getDepotDetails(ByVal bv_intDepotID As Integer) As DataTable
        Try
            Dim dt As New DataTable
            objData = New DataObjects(SelectDepotDetailQuery, CommonUIData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), CommonUIData._DEPOT)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetGateInDetails"
    Public Function GetActivityStatusDetails(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32, _
                                             ByVal bv_intCleaningID As Integer, ByVal bv_strGITransactionNo As String) As CleaningDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CleaningData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(CleaningData.DPT_ID, bv_intDepotID)
            hshParameters.Add(CleaningData.GI_TRNSCTN_NO, bv_strGITransactionNo)
            hshParameters.Add(CleaningData.CLNNG_ID, bv_intCleaningID)
            objData = New DataObjects(ActivityStatusDetailsSelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), CleaningData._V_ACTIVITY_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningCharge"
    Public Function GetCleaningCharge(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotID As Int32, ByVal bv_strGITransactionNo As String) As CleaningDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CleaningData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(CleaningData.DPT_ID, bv_intDepotID)
            hshParameters.Add(CleaningData.GI_TRNSCTN_NO, bv_strGITransactionNo)
            objData = New DataObjects(CleaningChargeSelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), CleaningData._CLEANING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateCleaning() TABLE NAME:Cleaning"
    Public Function CreateCleaning(ByVal bv_i64CustomerId As Int64, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByRef br_strCleaningCertificateNo As String, _
                                   ByVal bv_strChemicalName As String, _
                                   ByVal bv_dblCleaningRate As String, _
                                   ByVal bv_datOriginalCleaningDate As DateTime, _
                                   ByVal bv_datLastCleaningDate As DateTime, _
                                   ByVal bv_datOriginalInspectedDate As DateTime, _
                                   ByVal bv_datLastInspectedDate As DateTime, _
                                   ByVal bv_i64EquipmentStatusId As Int64, _
                                   ByVal bv_strCleanedFor As String, _
                                   ByVal bv_strLocationofcleaning As String, _
                                   ByVal bv_i64EquipmentCleaningstatus1 As Int64, _
                                   ByVal bv_i64EquipmentCleaningstatus2 As Int64, _
                                   ByVal bv_i64EquipmentCondition As Int64, _
                                   ByVal bv_i32Valvefittingcondition As Int64, _
                                   ByVal bv_strLidSealNo As String, _
                                   ByVal bv_strTopSealNo As String, _
                                   ByVal bv_strBottomSealNo As String, _
                                   ByVal bv_i64InvoicingPartyId As Int64, _
                                   ByVal bv_strCustomerReferencceNo As String, _
                                   ByVal bv_strCleaningRefNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_i32DepotId As Int32, _
                                   ByVal bv_strGateInTransactionNo As String, _
                                   ByVal bv_strCreatedBy As String, _
                                   ByVal bv_datCreatedDate As DateTime, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByVal bv_blnCleaningGenerationFlag As Boolean, _
                                   ByVal bv_StrIsChangeOfStatus As String, _
                                   ByVal bv_blnAdditionalCleaningFlag As Boolean, _
                                   ByVal bv_blnCleaningFlag As Boolean, _
                                   ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CleaningData._CLEANING, br_objTrans)
                If bv_StrIsChangeOfStatus = "C" Then
                    Dim intMaxNo As Integer = CommonUIs.GetIdentityValue(CleaningData._CLEANING_CERTIFICATE, br_objTrans)
                    'br_strCleaningCertificateNo = CommonUIs.GetIdentityCode(CleaningData._CLEANING_CERTIFICATE, intMaxNo, Now, br_objTrans)
                    'from Index Pattern
                    'br_strCleaningCertificateNo = IndexPatterns.GetIdentityCode(CleaningData._CLEANING, intMaxNo, Now, bv_i32DepotId, br_objTrans)
                End If
                .Item(CleaningData.CLNNG_ID) = intMax
                .Item(CleaningData.CLNNG_CD) = intMax
                .Item(CleaningData.CSTMR_ID) = bv_i64CustomerId
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                If br_strCleaningCertificateNo <> Nothing Then
                    .Item(CleaningData.CLNNG_CERT_NO) = br_strCleaningCertificateNo
                Else
                    .Item(CleaningData.CLNNG_CERT_NO) = DBNull.Value
                End If
                If bv_strChemicalName <> Nothing Then
                    .Item(CleaningData.CHMCL_NM) = bv_strChemicalName
                Else
                    .Item(CleaningData.CHMCL_NM) = DBNull.Value
                End If
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If
                .Item(CleaningData.ORGNL_CLNNG_DT) = bv_datOriginalCleaningDate
                .Item(CleaningData.LST_CLNNG_DT) = bv_datLastCleaningDate
                .Item(CleaningData.ORGNL_INSPCTD_DT) = bv_datOriginalInspectedDate
                .Item(CleaningData.LST_INSPCTD_DT) = bv_datLastInspectedDate
                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                If bv_strCleanedFor <> Nothing Then
                    .Item(CleaningData.CLND_FR_VCR) = bv_strCleanedFor
                Else
                    .Item(CleaningData.CLND_FR_VCR) = DBNull.Value
                End If
                If bv_strLocationofcleaning <> Nothing Then
                    .Item(CleaningData.LCTN_OF_CLNG) = bv_strLocationofcleaning
                Else
                    .Item(CleaningData.LCTN_OF_CLNG) = DBNull.Value
                End If
                If bv_i64EquipmentCleaningstatus1 <> 0 Then
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_1) = bv_i64EquipmentCleaningstatus1
                Else
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_1) = DBNull.Value
                End If
                If bv_i64EquipmentCleaningstatus2 <> 0 Then
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_2) = bv_i64EquipmentCleaningstatus2
                Else
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_2) = DBNull.Value
                End If
                If bv_i64EquipmentCondition <> 0 Then
                    .Item(CleaningData.EQPMNT_CNDTN_ID) = bv_i64EquipmentCondition
                Else
                    .Item(CleaningData.EQPMNT_CNDTN_ID) = DBNull.Value
                End If
                If bv_i32Valvefittingcondition <> 0 Then
                    .Item(CleaningData.VLV_FTTNG_CNDTN) = bv_i32Valvefittingcondition
                Else
                    .Item(CleaningData.VLV_FTTNG_CNDTN) = DBNull.Value
                End If
                If bv_strLidSealNo <> Nothing Then
                    .Item(CleaningData.MN_LID_SL_NO) = bv_strLidSealNo
                Else
                    .Item(CleaningData.MN_LID_SL_NO) = DBNull.Value
                End If
                If bv_strTopSealNo <> Nothing Then
                    .Item(CleaningData.TP_SL_NO) = bv_strTopSealNo
                Else
                    .Item(CleaningData.TP_SL_NO) = DBNull.Value
                End If
                If bv_strBottomSealNo <> Nothing Then
                    .Item(CleaningData.BTTM_SL_NO) = bv_strBottomSealNo
                Else
                    .Item(CleaningData.BTTM_SL_NO) = DBNull.Value
                End If
                If bv_i64InvoicingPartyId <> 0 Then
                    .Item(CleaningData.INVCNG_PRTY_ID) = bv_i64InvoicingPartyId
                Else
                    .Item(CleaningData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                If bv_strCustomerReferencceNo <> Nothing Then
                    .Item(CleaningData.CSTMR_RFRNC_NO) = bv_strCustomerReferencceNo
                Else
                    .Item(CleaningData.CSTMR_RFRNC_NO) = DBNull.Value
                End If
                If bv_strCleaningRefNo <> Nothing Then
                    .Item(CleaningData.CLNNG_RFRNC_NO) = bv_strCleaningRefNo
                Else
                    .Item(CleaningData.CLNNG_RFRNC_NO) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i32DepotId
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(CleaningData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(CleaningData.CRTD_BY) = bv_strCreatedBy
                .Item(CleaningData.CRTD_DT) = bv_datCreatedDate
                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_datModifiedDate
                .Item(CleaningData.CERT_GNRTD_FLG) = bv_blnCleaningGenerationFlag
                .Item(CleaningData.ADDTNL_CLNNG_BT) = bv_blnAdditionalCleaningFlag
                .Item(CleaningData.ADDTNL_CLNNG_FLG) = bv_blnCleaningFlag
                If bv_StrIsChangeOfStatus <> Nothing Then
                    .Item(CleaningData.IS_CHNG_OF_STTS) = bv_StrIsChangeOfStatus
                Else
                    .Item(CleaningData.IS_CHNG_OF_STTS) = DBNull.Value
                End If

            End With
            objData.InsertRow(dr, CleaningInsertQuery, br_objTrans)
            dr = Nothing
            CreateCleaning = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateCleaningCharge() TABLE NAME:Cleaning_Charge"
    Public Function CreateCleaningCharge(ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_i64CustomerID As Int64, _
                                         ByVal bv_i64InvoicePartyId As Int64, _
                                         ByVal bv_intCleaningID As Int64, _
                                         ByVal bv_datOriginalCleaningDate As DateTime, _
                                         ByVal bv_datOriginalInspectedDate As DateTime, _
                                         ByVal bv_dblCleaningRate As String, _
                                         ByVal bv_strBillingFlag As String, _
                                         ByVal bv_strCleaningCertificateNo As String, _
                                         ByVal bv_blnActiveBit As Boolean, _
                                         ByVal bv_i64DepotId As Int64, _
                                         ByVal bv_strGateInTransactionNo As String, _
                                         ByVal bv_blnSlabRate As Boolean, _
                                         ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING_CHARGE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CleaningData._CLEANING_CHARGE, br_objTrans)
                .Item(CleaningData.CLNNG_CHRG_ID) = intMax
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(CleaningData.CSTMR_ID) = bv_i64CustomerID
                If bv_i64InvoicePartyId <> 0 Then
                    .Item(CleaningData.INVCNG_PRTY_ID) = bv_i64InvoicePartyId
                Else
                    .Item(CleaningData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                .Item(CleaningData.CLNNG_ID) = bv_intCleaningID
                .Item(CleaningData.ORGNL_CLNNG_DT) = bv_datOriginalCleaningDate
                If bv_datOriginalInspectedDate <> Nothing Then
                    .Item(CleaningData.ORGNL_INSPCTD_DT) = bv_datOriginalInspectedDate
                Else
                    .Item(CleaningData.ORGNL_INSPCTD_DT) = DBNull.Value
                End If
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If
                .Item(CleaningData.BLLNG_FLG) = bv_strBillingFlag
                If bv_strCleaningCertificateNo <> Nothing Then
                    .Item(CleaningData.CLNNG_CERT_NO) = bv_strCleaningCertificateNo
                Else
                    .Item(CleaningData.CLNNG_CERT_NO) = DBNull.Value
                End If
                .Item(CleaningData.ACTV_BT) = bv_blnActiveBit
                .Item(CleaningData.DPT_ID) = bv_i64DepotId
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(CleaningData.SLB_RT_BT) = bv_blnSlabRate
            End With
            objData.InsertRow(dr, Cleaning_ChargeInsertQuery, br_objTrans)
            dr = Nothing
            CreateCleaningCharge = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateActivityStatus() TABLE NAME:Activity_Status"
    Public Function UpdateActivityStatus(ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_datOriginalCleaningDate As DateTime, _
                                         ByVal bv_datLastInspectedDate As DateTime, _
                                         ByVal bv_i64EquipmentStatusId As Int64, _
                                         ByVal bv_strActivity As String, _
                                         ByVal bv_datActivityDate As DateTime, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strGateInTransactionNo As String, _
                                         ByVal bv_strCustomerReferenceNo As String, _
                                         ByVal bv_i32DepoID As Int32, _
                                         ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_datOriginalCleaningDate <> Nothing Then
                    .Item(CleaningData.CLNNG_DT) = bv_datOriginalCleaningDate
                Else
                    .Item(CleaningData.CLNNG_DT) = bv_datLastInspectedDate
                End If
                'If bv_datLastInspectedDate <> Nothing Then
                '    .Item(CleaningData.INSPCTN_DT) = bv_datLastInspectedDate
                'Else
                '    .Item(CleaningData.INSPCTN_DT) = DBNull.Value
                'End If
                .Item(CleaningData.ACTVTY_DT) = bv_datLastInspectedDate
                .Item(CleaningData.INSPCTN_DT) = bv_datLastInspectedDate
                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                .Item(CleaningData.ACTVTY_NAM) = bv_strActivity
                '.Item(CleaningData.ACTVTY_DT) = bv_datActivityDate
                .Item(CleaningData.SCHDL_DT) = DBNull.Value
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(CleaningData.GI_RF_NO) = bv_strCustomerReferenceNo
                .Item(CleaningData.DPT_ID) = bv_i32DepoID
                .Item(CleaningData.CLNNG_INSPCTN_REF_NO) = DBNull.Value
            End With
            UpdateActivityStatus = objData.UpdateRow(dr, Activity_StatusUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateActivityStatus_ins(ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_datOriginalCleaningDate As DateTime, _
                                         ByVal bv_datLastInspectedDate As DateTime, _
                                         ByVal bv_i64EquipmentStatusId As Int64, _
                                         ByVal bv_strActivity As String, _
                                         ByVal bv_datActivityDate As DateTime, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strGateInTransactionNo As String, _
                                         ByVal bv_strCustomerReferenceNo As String, _
                                         ByVal bv_i32DepoID As Int32, _
                                         ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_datOriginalCleaningDate <> Nothing Then
                    .Item(CleaningData.CLNNG_DT) = bv_datOriginalCleaningDate
                Else
                    .Item(CleaningData.CLNNG_DT) = bv_datLastInspectedDate
                End If
                'If bv_datLastInspectedDate <> Nothing Then
                '    .Item(CleaningData.INSPCTN_DT) = bv_datLastInspectedDate
                'Else
                '    .Item(CleaningData.INSPCTN_DT) = DBNull.Value
                'End If
                .Item(CleaningData.ACTVTY_DT) = bv_datLastInspectedDate
                .Item(CleaningData.INSPCTN_DT) = bv_datLastInspectedDate
                '.Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                '.Item(CleaningData.ACTVTY_NAM) = bv_strActivity
                '.Item(CleaningData.ACTVTY_DT) = bv_datActivityDate
                .Item(CleaningData.SCHDL_DT) = DBNull.Value
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(CleaningData.GI_RF_NO) = bv_strCustomerReferenceNo
                .Item(CleaningData.DPT_ID) = bv_i32DepoID
                .Item(CleaningData.CLNNG_INSPCTN_REF_NO) = DBNull.Value
            End With
            UpdateActivityStatus_ins = objData.UpdateRow(dr, Activity_StatusUpdateQuery_ins, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function UpdateActivityStatus_ins_MySub(ByVal bv_strEquipmentNo As String, _
                                       ByVal bv_datOriginalCleaningDate As DateTime, _
                                       ByVal bv_datLastInspectedDate As DateTime, _
                                       ByVal bv_i64EquipmentStatusId As Int64, _
                                       ByVal bv_strActivity As String, _
                                       ByVal bv_datActivityDate As DateTime, _
                                       ByVal bv_strRemarks As String, _
                                       ByVal bv_strGateInTransactionNo As String, _
                                       ByVal bv_strCustomerReferenceNo As String, _
                                       ByVal bv_i32DepoID As Int32, _
                                       ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                'If bv_datOriginalCleaningDate <> Nothing Then
                '    .Item(CleaningData.CLNNG_DT) = bv_datOriginalCleaningDate
                'Else
                '    .Item(CleaningData.CLNNG_DT) = bv_datLastInspectedDate
                'End If
                'If bv_datLastInspectedDate <> Nothing Then
                '    .Item(CleaningData.INSPCTN_DT) = bv_datLastInspectedDate
                'Else
                '    .Item(CleaningData.INSPCTN_DT) = DBNull.Value
                'End If
                '.Item(CleaningData.ACTVTY_DT) = bv_datLastInspectedDate
                '.Item(CleaningData.INSPCTN_DT) = bv_datLastInspectedDate
                '.Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                '.Item(CleaningData.ACTVTY_NAM) = bv_strActivity
                '.Item(CleaningData.ACTVTY_DT) = bv_datActivityDate
                .Item(CleaningData.SCHDL_DT) = DBNull.Value
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(CleaningData.GI_RF_NO) = bv_strCustomerReferenceNo
                .Item(CleaningData.DPT_ID) = bv_i32DepoID
                .Item(CleaningData.CLNNG_INSPCTN_REF_NO) = DBNull.Value
            End With
            UpdateActivityStatus_ins_MySub = objData.UpdateRow(dr, Activity_StatusUpdateQuery_insMySub, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "UpdateCleaning() TABLE NAME:Cleaning"
    Public Function UpdateCleaning(ByVal bv_i64CleaningId As Int64, _
                                   ByVal bv_dblCleaningRate As String, _
                                   ByVal bv_datLastCleaningDate As DateTime, _
                                   ByVal bv_datOriginalInspectedDate As DateTime, _
                                   ByVal bv_datLastInspectedDate As DateTime, _
                                   ByVal bv_i64EquipmentStatusId As Int64, _
                                   ByVal bv_strCleanedFor As String, _
                                   ByVal bv_strLocationofcleaning As String, _
                                   ByVal bv_i64EquipmentCleaningstatus1 As Int64, _
                                   ByVal bv_i64EquipmentCleaningstatus2 As Int64, _
                                   ByVal bv_i64EquipmentCondition As Int64, _
                                   ByVal bv_i32Valvefittingcondition As Int64, _
                                   ByVal bv_strLidSealNo As String, _
                                   ByVal bv_strTopSealNo As String, _
                                   ByVal bv_strBottomSealNo As String, _
                                   ByVal bv_i64InvoicingPartyId As Int64, _
                                   ByVal bv_strCustomerReferencceNo As String, _
                                   ByVal bv_strCleaningRefNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_i32DepotId As Int32, _
                                   ByVal bv_strGateInTransactionNo As String, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByVal bv_strIsChangeOfStatus As String, _
                                   ByVal bv_blnAdditionalCleaningBit As Boolean, _
                                   ByVal bv_blnCleaningFlag As Boolean, _
                                   ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING).NewRow()
            With dr
                .Item(CleaningData.CLNNG_ID) = bv_i64CleaningId
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If
                .Item(CleaningData.LST_CLNNG_DT) = bv_datLastCleaningDate
                .Item(CleaningData.LST_INSPCTD_DT) = bv_datLastInspectedDate
                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                If bv_strCleanedFor <> Nothing Then
                    .Item(CleaningData.CLND_FR_VCR) = bv_strCleanedFor
                Else
                    .Item(CleaningData.CLND_FR_VCR) = DBNull.Value
                End If
                If bv_strLocationofcleaning <> Nothing Then
                    .Item(CleaningData.LCTN_OF_CLNG) = bv_strLocationofcleaning
                Else
                    .Item(CleaningData.LCTN_OF_CLNG) = DBNull.Value
                End If
                If bv_i64EquipmentCleaningstatus1 <> 0 Then
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_1) = bv_i64EquipmentCleaningstatus1
                Else
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_1) = DBNull.Value
                End If
                If bv_i64EquipmentCleaningstatus2 <> 0 Then
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_2) = bv_i64EquipmentCleaningstatus2
                Else
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_2) = DBNull.Value
                End If
                If bv_i64EquipmentCondition <> 0 Then
                    .Item(CleaningData.EQPMNT_CNDTN_ID) = bv_i64EquipmentCondition
                Else
                    .Item(CleaningData.EQPMNT_CNDTN_ID) = DBNull.Value
                End If
                If bv_i32Valvefittingcondition <> 0 Then
                    .Item(CleaningData.VLV_FTTNG_CNDTN) = bv_i32Valvefittingcondition
                Else
                    .Item(CleaningData.VLV_FTTNG_CNDTN) = DBNull.Value
                End If
                If bv_strLidSealNo <> Nothing Then
                    .Item(CleaningData.MN_LID_SL_NO) = bv_strLidSealNo
                Else
                    .Item(CleaningData.MN_LID_SL_NO) = DBNull.Value
                End If
                If bv_strTopSealNo <> Nothing Then
                    .Item(CleaningData.TP_SL_NO) = bv_strTopSealNo
                Else
                    .Item(CleaningData.TP_SL_NO) = DBNull.Value
                End If
                If bv_strBottomSealNo <> Nothing Then
                    .Item(CleaningData.BTTM_SL_NO) = bv_strBottomSealNo
                Else
                    .Item(CleaningData.BTTM_SL_NO) = DBNull.Value
                End If
                If bv_i64InvoicingPartyId <> 0 Then
                    .Item(CleaningData.INVCNG_PRTY_ID) = bv_i64InvoicingPartyId
                Else
                    .Item(CleaningData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                If bv_strCustomerReferencceNo <> Nothing Then
                    .Item(CleaningData.CSTMR_RFRNC_NO) = bv_strCustomerReferencceNo
                Else
                    .Item(CleaningData.CSTMR_RFRNC_NO) = DBNull.Value
                End If
                If bv_strCleaningRefNo <> Nothing Then
                    .Item(CleaningData.CLNNG_RFRNC_NO) = bv_strCleaningRefNo
                Else
                    .Item(CleaningData.CLNNG_RFRNC_NO) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i32DepotId
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(CleaningData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(CleaningData.IS_CHNG_OF_STTS) = bv_strIsChangeOfStatus
                .Item(CleaningData.ADDTNL_CLNNG_BT) = bv_blnAdditionalCleaningBit
                .Item(CleaningData.ADDTNL_CLNNG_FLG) = bv_blnCleaningFlag
                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_datModifiedDate

                If bv_datOriginalInspectedDate <> Nothing Then
                    .Item(CleaningData.ORGNL_INSPCTD_DT) = bv_datOriginalInspectedDate
                Else
                    .Item(CleaningData.ORGNL_INSPCTD_DT) = DBNull.Value
                End If

            End With
            UpdateCleaning = objData.UpdateRow(dr, CleaningUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateCleaning_Ins_Mysubmit(ByVal bv_i64CleaningId As Int64, _
                                ByVal bv_dblCleaningRate As String, _
                                ByVal bv_datLastCleaningDate As DateTime, _
                                ByVal bv_datOriginalInspectedDate As DateTime, _
                                ByVal bv_datLastInspectedDate As DateTime, _
                                ByVal bv_i64EquipmentStatusId As Int64, _
                                ByVal bv_strCleanedFor As String, _
                                ByVal bv_strLocationofcleaning As String, _
                                ByVal bv_i64EquipmentCleaningstatus1 As Int64, _
                                ByVal bv_i64EquipmentCleaningstatus2 As Int64, _
                                ByVal bv_i64EquipmentCondition As Int64, _
                                ByVal bv_i32Valvefittingcondition As Int64, _
                                ByVal bv_strLidSealNo As String, _
                                ByVal bv_strTopSealNo As String, _
                                ByVal bv_strBottomSealNo As String, _
                                ByVal bv_i64InvoicingPartyId As Int64, _
                                ByVal bv_strCustomerReferencceNo As String, _
                                ByVal bv_strCleaningRefNo As String, _
                                ByVal bv_strRemarks As String, _
                                ByVal bv_i32DepotId As Int32, _
                                ByVal bv_strGateInTransactionNo As String, _
                                ByVal bv_strModifiedBy As String, _
                                ByVal bv_datModifiedDate As DateTime, _
                                ByVal bv_strIsChangeOfStatus As String, _
                                ByVal bv_blnAdditionalCleaningBit As Boolean, _
                                ByVal bv_blnCleaningFlag As Boolean, _
                                ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING).NewRow()
            With dr
                .Item(CleaningData.CLNNG_ID) = bv_i64CleaningId
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If
                '.Item(CleaningData.LST_CLNNG_DT) = bv_datLastCleaningDate
                '.Item(CleaningData.LST_INSPCTD_DT) = bv_datLastInspectedDate
                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                If bv_strCleanedFor <> Nothing Then
                    .Item(CleaningData.CLND_FR_VCR) = bv_strCleanedFor
                Else
                    .Item(CleaningData.CLND_FR_VCR) = DBNull.Value
                End If
                If bv_strLocationofcleaning <> Nothing Then
                    .Item(CleaningData.LCTN_OF_CLNG) = bv_strLocationofcleaning
                Else
                    .Item(CleaningData.LCTN_OF_CLNG) = DBNull.Value
                End If
                If bv_i64EquipmentCleaningstatus1 <> 0 Then
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_1) = bv_i64EquipmentCleaningstatus1
                Else
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_1) = DBNull.Value
                End If
                If bv_i64EquipmentCleaningstatus2 <> 0 Then
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_2) = bv_i64EquipmentCleaningstatus2
                Else
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_2) = DBNull.Value
                End If
                If bv_i64EquipmentCondition <> 0 Then
                    .Item(CleaningData.EQPMNT_CNDTN_ID) = bv_i64EquipmentCondition
                Else
                    .Item(CleaningData.EQPMNT_CNDTN_ID) = DBNull.Value
                End If
                If bv_i32Valvefittingcondition <> 0 Then
                    .Item(CleaningData.VLV_FTTNG_CNDTN) = bv_i32Valvefittingcondition
                Else
                    .Item(CleaningData.VLV_FTTNG_CNDTN) = DBNull.Value
                End If
                If bv_strLidSealNo <> Nothing Then
                    .Item(CleaningData.MN_LID_SL_NO) = bv_strLidSealNo
                Else
                    .Item(CleaningData.MN_LID_SL_NO) = DBNull.Value
                End If
                If bv_strTopSealNo <> Nothing Then
                    .Item(CleaningData.TP_SL_NO) = bv_strTopSealNo
                Else
                    .Item(CleaningData.TP_SL_NO) = DBNull.Value
                End If
                If bv_strBottomSealNo <> Nothing Then
                    .Item(CleaningData.BTTM_SL_NO) = bv_strBottomSealNo
                Else
                    .Item(CleaningData.BTTM_SL_NO) = DBNull.Value
                End If
                If bv_i64InvoicingPartyId <> 0 Then
                    .Item(CleaningData.INVCNG_PRTY_ID) = bv_i64InvoicingPartyId
                Else
                    .Item(CleaningData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                If bv_strCustomerReferencceNo <> Nothing Then
                    .Item(CleaningData.CSTMR_RFRNC_NO) = bv_strCustomerReferencceNo
                Else
                    .Item(CleaningData.CSTMR_RFRNC_NO) = DBNull.Value
                End If
                If bv_strCleaningRefNo <> Nothing Then
                    .Item(CleaningData.CLNNG_RFRNC_NO) = bv_strCleaningRefNo
                Else
                    .Item(CleaningData.CLNNG_RFRNC_NO) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i32DepotId
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(CleaningData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(CleaningData.IS_CHNG_OF_STTS) = bv_strIsChangeOfStatus
                .Item(CleaningData.ADDTNL_CLNNG_BT) = bv_blnAdditionalCleaningBit
                .Item(CleaningData.ADDTNL_CLNNG_FLG) = bv_blnCleaningFlag
                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_datModifiedDate

                'If bv_datOriginalInspectedDate <> Nothing Then
                '    .Item(CleaningData.ORGNL_INSPCTD_DT) = bv_datOriginalInspectedDate
                'Else
                '    .Item(CleaningData.ORGNL_INSPCTD_DT) = DBNull.Value
                'End If

            End With
            UpdateCleaning_Ins_Mysubmit = objData.UpdateRow(dr, CleaningUpdateQuery_MYSub, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCleaningCharge() TABLE NAME:Cleaning_Charge"

    Public Function UpdateCleaning_Charge(ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64CustomerID As Int64, _
        ByVal bv_i64InvoicePartyId As Int64, _
        ByVal bv_datOriginalCleaningDate As DateTime, _
        ByVal bv_datOriginalInspectedDate As DateTime, _
        ByVal bv_dblCleaningRate As String, _
        ByVal bv_i64DepotId As Int64, _
        ByVal bv_strGateInTransactionNo As String, _
        ByVal bv_intCleaningID As Int64, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING_CHARGE).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(CleaningData.CSTMR_ID) = bv_i64CustomerID
                If bv_i64InvoicePartyId <> 0 Then
                    .Item(CleaningData.INVCNG_PRTY_ID) = bv_i64InvoicePartyId
                Else
                    .Item(CleaningData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                .Item(CleaningData.ORGNL_CLNNG_DT) = bv_datOriginalCleaningDate
                If bv_datOriginalInspectedDate <> Nothing Then
                    .Item(CleaningData.ORGNL_INSPCTD_DT) = bv_datOriginalInspectedDate
                Else
                    .Item(CleaningData.ORGNL_INSPCTD_DT) = DBNull.Value
                End If
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i64DepotId
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(CleaningData.CLNNG_ID) = bv_intCleaningID
            End With
            UpdateCleaning_Charge = objData.UpdateRow(dr, Cleaning_ChargeUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateCleaning_Charge_Ins_Mysub(ByVal bv_strEquipmentNo As String, _
      ByVal bv_i64CustomerID As Int64, _
      ByVal bv_i64InvoicePartyId As Int64, _
      ByVal bv_datOriginalCleaningDate As DateTime, _
      ByVal bv_datOriginalInspectedDate As DateTime, _
      ByVal bv_dblCleaningRate As String, _
      ByVal bv_i64DepotId As Int64, _
      ByVal bv_strGateInTransactionNo As String, _
      ByVal bv_intCleaningID As Int64, _
      ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING_CHARGE).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(CleaningData.CSTMR_ID) = bv_i64CustomerID
                If bv_i64InvoicePartyId <> 0 Then
                    .Item(CleaningData.INVCNG_PRTY_ID) = bv_i64InvoicePartyId
                Else
                    .Item(CleaningData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                '.Item(CleaningData.ORGNL_CLNNG_DT) = bv_datOriginalCleaningDate
                'If bv_datOriginalInspectedDate <> Nothing Then
                '    .Item(CleaningData.ORGNL_INSPCTD_DT) = bv_datOriginalInspectedDate
                'Else
                '    .Item(CleaningData.ORGNL_INSPCTD_DT) = DBNull.Value
                'End If
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i64DepotId
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(CleaningData.CLNNG_ID) = bv_intCleaningID
            End With
            UpdateCleaning_Charge_Ins_Mysub = objData.UpdateRow(dr, Cleaning_ChargeUpdateQuery_Ins_MySub, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateTracking() TABLE NAME:Tracking"
    Public Function UpdateTracking(ByVal bv_strEqpmntNo As String, _
                                      ByVal bv_datActivitydate As DateTime, _
                                      ByVal bv_strActivityRemarks As String, _
                                      ByVal bv_strGateInTransactionNumber As String, _
                                      ByVal bv_i64InvoicingPartyId As Int64, _
                                      ByVal bv_i32DepotID As Int32, _
                                      ByVal bv_strActivityName As String, _
                                      ByVal bv_intActvtyNo As Integer, _
                                      ByVal bv_strModifiedBy As String, _
                                      ByVal bv_dtModifiedDate As DateTime, _
                                      ByVal bv_strEquipmentInformationRemarks As String, _
                                      ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._TRACKING).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEqpmntNo
                .Item(CleaningData.ACTVTY_DT) = bv_datActivitydate
                If bv_strActivityRemarks <> Nothing Then
                    .Item(CleaningData.ACTVTY_RMRKS) = bv_strActivityRemarks
                Else
                    .Item(CleaningData.ACTVTY_RMRKS) = DBNull.Value
                End If
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNumber
                If bv_i64InvoicingPartyId <> 0 Then
                    .Item(CleaningData.INVCNG_PRTY_ID) = bv_i64InvoicingPartyId
                Else
                    .Item(CleaningData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i32DepotID
                .Item(CleaningData.ACTVTY_NAM) = bv_strActivityName
                .Item(CleaningData.ACTVTY_NO) = bv_intActvtyNo
                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_dtModifiedDate
                If bv_strEquipmentInformationRemarks <> Nothing Then
                    .Item(CleaningData.EQPMNT_INFRMTN_RMRKS_VC) = bv_strEquipmentInformationRemarks
                Else
                    .Item(CleaningData.EQPMNT_INFRMTN_RMRKS_VC) = DBNull.Value
                End If
            End With
            UpdateTracking = objData.UpdateRow(dr, TrackingUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateTracking_Ins_MySub(ByVal bv_strEqpmntNo As String, _
                                   ByVal bv_datActivitydate As DateTime, _
                                   ByVal bv_strActivityRemarks As String, _
                                   ByVal bv_strGateInTransactionNumber As String, _
                                   ByVal bv_i64InvoicingPartyId As Int64, _
                                   ByVal bv_i32DepotID As Int32, _
                                   ByVal bv_strActivityName As String, _
                                   ByVal bv_intActvtyNo As Integer, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_dtModifiedDate As DateTime, _
                                   ByVal bv_strEquipmentInformationRemarks As String, _
                                   ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._TRACKING).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEqpmntNo
                '.Item(CleaningData.ACTVTY_DT) = bv_datActivitydate
                If bv_strActivityRemarks <> Nothing Then
                    .Item(CleaningData.ACTVTY_RMRKS) = bv_strActivityRemarks
                Else
                    .Item(CleaningData.ACTVTY_RMRKS) = DBNull.Value
                End If
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNumber
                If bv_i64InvoicingPartyId <> 0 Then
                    .Item(CleaningData.INVCNG_PRTY_ID) = bv_i64InvoicingPartyId
                Else
                    .Item(CleaningData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i32DepotID
                .Item(CleaningData.ACTVTY_NAM) = bv_strActivityName
                .Item(CleaningData.ACTVTY_NO) = bv_intActvtyNo
                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_dtModifiedDate
                If bv_strEquipmentInformationRemarks <> Nothing Then
                    .Item(CleaningData.EQPMNT_INFRMTN_RMRKS_VC) = bv_strEquipmentInformationRemarks
                Else
                    .Item(CleaningData.EQPMNT_INFRMTN_RMRKS_VC) = DBNull.Value
                End If
            End With
            UpdateTracking_Ins_MySub = objData.UpdateRow(dr, TrackingUpdateQuery_InsMySub, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateTrackingCertNo() TABLE NAME:Tracking"

    Public Function UpdateTrackingCertNo(ByVal int_cleaningID As Integer, _
                                         ByVal bv_strEqpmntNo As String, _
                                           ByVal bv_i32DepotID As Int32, _
                                    ByVal bv_strGateInTransactionNumber As String, _
                                    ByVal bv_strCleaningCertNo As String, _
                                    ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._TRACKING).NewRow()
            With dr
                .Item(CleaningData.ACTVTY_NO) = int_cleaningID
                .Item(CleaningData.EQPMNT_NO) = bv_strEqpmntNo
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNumber
                .Item(CleaningData.DPT_ID) = bv_i32DepotID
                If bv_strCleaningCertNo <> Nothing Then
                    .Item(CleaningData.RFRNC_NO) = bv_strCleaningCertNo
                Else
                    .Item(CleaningData.RFRNC_NO) = DBNull.Value
                End If
            End With
            UpdateTrackingCertNo = objData.UpdateRow(dr, TrackingCleaningCertUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateActivityRemarksStatus() Table Name: Activity_Status"
    'CR- 003 [REMARKS IN STATUS,EQUIPMENT_HISTORY]
    Public Function UpdateActivityRemarksStatus(ByVal bv_strEquipmentNo As String, _
                                                ByVal bv_strRemarks As String, _
                                                ByVal bv_strGateInTransactionNo As String, _
                                                ByVal bv_strCustomerReferenceNo As String, _
                                                ByVal bv_i32DepoID As Int32, _
                                                ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(CleaningData.GI_RF_NO) = bv_strCustomerReferenceNo
                .Item(CleaningData.DPT_ID) = bv_i32DepoID
            End With
            UpdateActivityRemarksStatus = objData.UpdateRow(dr, Activity_StatusUpdateRemarksQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningChargeBilled()"
    Public Function GetCleaningChargeBilled(ByVal bv_strEquipmentNo As String, _
                                            ByVal bv_intDepotID As Int32, _
                                            ByVal bv_i64CleaningID As Long, _
                                            ByVal bv_strGITransactionNo As String) As CleaningDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CleaningData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(CleaningData.DPT_ID, bv_intDepotID)
            hshParameters.Add(CleaningData.GI_TRNSCTN_NO, bv_strGITransactionNo)
            hshParameters.Add(CleaningData.CLNNG_ID, bv_i64CleaningID)
            objData = New DataObjects(Cleaning_ChargeBilledSelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), CleaningData._CLEANING_CHARGE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCleaningCharge"
    Public Function UpdateActivityStatusIdByEqpmntNo(ByVal bv_strEquipmentNo As String, _
                                                     ByVal bv_intDepotID As Int32, _
                                                     ByVal bv_strGITransactionNo As String, _
                                                     ByVal bv_int64EquipmentStatusId As Int64, _
                                                     ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(CleaningData.EQPMNT_STTS_ID) = bv_int64EquipmentStatusId
                .Item(CleaningData.DPT_ID) = bv_intDepotID
                .Item(CleaningData.SCHDL_DT) = Nothing
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGITransactionNo
            End With
            UpdateActivityStatusIdByEqpmntNo = objData.UpdateRow(dr, Activity_StatusUpdateStatusQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "UpdateCleaning() TABLE NAME:Cleaning"
    Public Function UpdateCleaning(ByVal bv_i64CleaningId As Int64, _
                                   ByVal bv_dblCleaningRate As String, _
                                   ByVal bv_datOrginalInspectedDate As DateTime, _
                                   ByVal bv_datLastCleaningDate As DateTime, _
                                   ByVal bv_datLastInspectedDate As DateTime, _
                                   ByVal bv_i64EquipmentStatusId As Int64, _
                                   ByVal bv_strCleanedFor As String, _
                                   ByVal bv_strLocationofcleaning As String, _
                                   ByVal bv_i64EquipmentCleaningstatus1 As Int64, _
                                   ByVal bv_i64EquipmentCleaningstatus2 As Int64, _
                                   ByVal bv_i64EquipmentCondition As Int64, _
                                   ByVal bv_i32Valvefittingcondition As Int64, _
                                   ByVal bv_strLidSealNo As String, _
                                   ByVal bv_strTopSealNo As String, _
                                   ByVal bv_strBottomSealNo As String, _
                                   ByVal bv_i64InvoicingPartyId As Int64, _
                                   ByVal bv_strCustomerReferencceNo As String, _
                                   ByVal bv_strCleaningRefNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_i32DepotId As Int32, _
                                   ByVal bv_strGateInTransactionNo As String, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByVal bv_blnAdditionalCleaningBit As Boolean, _
                                   ByVal bv_blnCleaningFlag As Boolean, _
                                   ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING).NewRow()
            With dr
                .Item(CleaningData.CLNNG_ID) = bv_i64CleaningId
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If

                .Item(CleaningData.ORGNL_INSPCTD_DT) = bv_datOrginalInspectedDate
                .Item(CleaningData.LST_CLNNG_DT) = bv_datLastCleaningDate
                .Item(CleaningData.LST_INSPCTD_DT) = bv_datLastInspectedDate
                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                If bv_strCleanedFor <> Nothing Then
                    .Item(CleaningData.CLND_FR_VCR) = bv_strCleanedFor
                Else
                    .Item(CleaningData.CLND_FR_VCR) = DBNull.Value
                End If
                If bv_strLocationofcleaning <> Nothing Then
                    .Item(CleaningData.LCTN_OF_CLNG) = bv_strLocationofcleaning
                Else
                    .Item(CleaningData.LCTN_OF_CLNG) = DBNull.Value
                End If
                If bv_i64EquipmentCleaningstatus1 <> 0 Then
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_1) = bv_i64EquipmentCleaningstatus1
                Else
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_1) = DBNull.Value
                End If
                If bv_i64EquipmentCleaningstatus2 <> 0 Then
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_2) = bv_i64EquipmentCleaningstatus2
                Else
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_2) = DBNull.Value
                End If
                If bv_i64EquipmentCondition <> 0 Then
                    .Item(CleaningData.EQPMNT_CNDTN_ID) = bv_i64EquipmentCondition
                Else
                    .Item(CleaningData.EQPMNT_CNDTN_ID) = DBNull.Value
                End If
                If bv_i32Valvefittingcondition <> 0 Then
                    .Item(CleaningData.VLV_FTTNG_CNDTN) = bv_i32Valvefittingcondition
                Else
                    .Item(CleaningData.VLV_FTTNG_CNDTN) = DBNull.Value
                End If
                If bv_strLidSealNo <> Nothing Then
                    .Item(CleaningData.MN_LID_SL_NO) = bv_strLidSealNo
                Else
                    .Item(CleaningData.MN_LID_SL_NO) = DBNull.Value
                End If
                If bv_strTopSealNo <> Nothing Then
                    .Item(CleaningData.TP_SL_NO) = bv_strTopSealNo
                Else
                    .Item(CleaningData.TP_SL_NO) = DBNull.Value
                End If
                If bv_strBottomSealNo <> Nothing Then
                    .Item(CleaningData.BTTM_SL_NO) = bv_strBottomSealNo
                Else
                    .Item(CleaningData.BTTM_SL_NO) = DBNull.Value
                End If
                If bv_i64InvoicingPartyId <> 0 Then
                    .Item(CleaningData.INVCNG_PRTY_ID) = bv_i64InvoicingPartyId
                Else
                    .Item(CleaningData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                If bv_strCustomerReferencceNo <> Nothing Then
                    .Item(CleaningData.CSTMR_RFRNC_NO) = bv_strCustomerReferencceNo
                Else
                    .Item(CleaningData.CSTMR_RFRNC_NO) = DBNull.Value
                End If
                If bv_strCleaningRefNo <> Nothing Then
                    .Item(CleaningData.CLNNG_RFRNC_NO) = bv_strCleaningRefNo
                Else
                    .Item(CleaningData.CLNNG_RFRNC_NO) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i32DepotId
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(CleaningData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(CleaningData.ADDTNL_CLNNG_BT) = bv_blnAdditionalCleaningBit
                .Item(CleaningData.ADDTNL_CLNNG_FLG) = bv_blnCleaningFlag
                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_datModifiedDate
            End With
            UpdateCleaning = objData.UpdateRow(dr, CleaningUpdateQueryBy_AdditionalCleaningFlag, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCleaningCharge"
    Public Function UpdateCleaningCharge(ByVal bv_i64CleaningId As Int64, _
                                         ByVal bv_decCleaningRate As Decimal, _
                                         ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING_CHARGE).NewRow()
            With dr
                .Item(CleaningData.CLNNG_ID) = bv_i64CleaningId
                .Item(CleaningData.CLNNG_RT) = bv_decCleaningRate
            End With
            UpdateCleaningCharge = objData.UpdateRow(dr, Cleaning_ChargeUpdateQueryByCleaningId, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

    'Cleaning & Inspection - Split

#Region "Cleaning Inspection - Split"

#Region "Cleaning - Operations"

    Public Function UpdateCleaning_Clean(ByVal bv_i64CleaningId As Int64, _
                                ByVal bv_dblCleaningRate As String, _
                                ByVal bv_datLastCleaningDate As DateTime, _
                                ByVal bv_i64EquipmentStatusId As Int64, _
                                ByVal bv_strCleaningRefNo As String, _
                                ByVal bv_strRemarks As String, _
                                ByVal bv_i32DepotId As Int32, _
                                ByVal bv_strGateInTransactionNo As String, _
                                ByVal bv_strModifiedBy As String, _
                                ByVal bv_datModifiedDate As DateTime, _
                                ByVal bv_strIsChangeOfStatus As String, _
                                ByVal bv_blnAdditionalCleaningBit As Boolean, _
                                ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING).NewRow()
            With dr
                .Item(CleaningData.CLNNG_ID) = bv_i64CleaningId
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If


                .Item(CleaningData.LST_CLNNG_DT) = bv_datLastCleaningDate
                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                If bv_strCleaningRefNo <> Nothing Then
                    .Item(CleaningData.CLNNG_RFRNC_NO) = bv_strCleaningRefNo
                Else
                    .Item(CleaningData.CLNNG_RFRNC_NO) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i32DepotId
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(CleaningData.GI_TRNSCTN_NO) = DBNull.Value
                End If

                'If bv_strIsChangeOfStatus <> Nothing Then
                '    .Item(CleaningData.IS_CHNG_OF_STTS) = bv_strIsChangeOfStatus
                'Else
                '    .Item(CleaningData.IS_CHNG_OF_STTS) = DBNull.Value
                'End If

                '.Item(CleaningData.ADDTNL_CLNNG_BT) = bv_blnAdditionalCleaningBit
                '.Item(CleaningData.ADDTNL_CLNNG_FLG) = bv_blnAdditionalCleaningBit


                .Item(CleaningData.ADDTNL_CLNNG_BT) = False
                .Item(CleaningData.ADDTNL_CLNNG_FLG) = bv_blnAdditionalCleaningBit

                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_datModifiedDate

            End With
            UpdateCleaning_Clean = objData.UpdateRow(dr, CleaningUpdateQuery_Clean, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateCleaning_Charge_Clean(ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64CustomerID As Int64, _
        ByVal bv_datOriginalInspectedDate As DateTime, _
        ByVal bv_dblCleaningRate As String, _
        ByVal bv_i64DepotId As Int64, _
        ByVal bv_strGateInTransactionNo As String, _
        ByVal bv_intCleaningID As Int64, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING_CHARGE).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(CleaningData.CSTMR_ID) = bv_i64CustomerID

                If bv_datOriginalInspectedDate <> Nothing Then
                    .Item(CleaningData.ORGNL_CLNNG_DT) = bv_datOriginalInspectedDate
                Else
                    .Item(CleaningData.ORGNL_CLNNG_DT) = DBNull.Value
                End If
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i64DepotId
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(CleaningData.CLNNG_ID) = bv_intCleaningID
            End With

            UpdateCleaning_Charge_Clean = objData.UpdateRow(dr, Cleaning_ChargeUpdateQuery_Clean, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateActivityRemarksStatus_Clean(ByVal bv_strEquipmentNo As String, _
                                                ByVal bv_strRemarks As String, _
                                                ByVal bv_strGateInTransactionNo As String, _
                                                ByVal bv_i32DepoID As Int32, _
                                                ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(CleaningData.DPT_ID) = bv_i32DepoID
            End With

            UpdateActivityRemarksStatus_Clean = objData.UpdateRow(dr, Activity_StatusUpdateRemarksQuery_Clean, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateCleaning_Clean(ByVal bv_i64CleaningId As Int64, _
                                   ByVal bv_dblCleaningRate As Double, _
                                   ByVal bv_datLastCleaningDate As DateTime, _
                                   ByVal bv_i64EquipmentStatusId As Int64, _
                                   ByVal bv_i64InvoicingPartyId As Int64, _
                                   ByVal bv_strCleaningRefNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_i32DepotId As Int32, _
                                   ByVal bv_strGateInTransactionNo As String, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByVal bv_blnAdditionalCleaningBit As Boolean, _
                                   ByVal bv_blnCleaningFlag As Boolean, _
                                   ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING).NewRow()
            With dr

                .Item(CleaningData.CLNNG_ID) = bv_i64CleaningId
                .Item(CleaningData.CLNNG_RT) = bv_dblCleaningRate
                .Item(CleaningData.LST_CLNNG_DT) = bv_datLastCleaningDate
                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId

                If bv_i64InvoicingPartyId <> 0 Then
                    .Item(CleaningData.INVCNG_PRTY_ID) = bv_i64InvoicingPartyId
                Else
                    .Item(CleaningData.INVCNG_PRTY_ID) = DBNull.Value
                End If

                If bv_strCleaningRefNo <> Nothing Then
                    .Item(CleaningData.CLNNG_RFRNC_NO) = bv_strCleaningRefNo
                Else
                    .Item(CleaningData.CLNNG_RFRNC_NO) = DBNull.Value
                End If

                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If

                .Item(CleaningData.DPT_ID) = bv_i32DepotId

                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(CleaningData.GI_TRNSCTN_NO) = DBNull.Value
                End If

                .Item(CleaningData.ADDTNL_CLNNG_BT) = bv_blnAdditionalCleaningBit
                .Item(CleaningData.ADDTNL_CLNNG_FLG) = bv_blnCleaningFlag
                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_datModifiedDate
            End With
            UpdateCleaning_Clean = objData.UpdateRow(dr, CleaningUpdateQueryBy_AdditionalCleaningFlag_Clean, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateActivityStatus_Clean(ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_datLastCleaningDate As DateTime, _
                                         ByVal bv_i64EquipmentStatusId As Int64, _
                                         ByVal bv_strActivity As String, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strGateInTransactionNo As String, _
                                         ByVal bv_i32DepoID As Int32, _
                                         ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo

                If bv_datLastCleaningDate <> Nothing Then
                    .Item(CleaningData.CLNNG_DT) = bv_datLastCleaningDate
                Else
                    .Item(CleaningData.CLNNG_DT) = DBNull.Value
                End If
                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                .Item(CleaningData.ACTVTY_NAM) = bv_strActivity

                .Item(CleaningData.ACTVTY_DT) = bv_datLastCleaningDate

                '.Item(CleaningData.SCHDL_DT) = DBNull.Value
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo

                .Item(CleaningData.DPT_ID) = bv_i32DepoID
            End With
            UpdateActivityStatus_Clean = objData.UpdateRow(dr, Activity_StatusUpdateQuery_Clean, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CreateCleaningCharge_Clean(ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_i64CustomerID As Int64, _
                                         ByVal bv_intCleaningID As Int64, _
                                         ByVal bv_datOriginalCleaningDate As DateTime, _
                                         ByVal bv_dblCleaningRate As String, _
                                         ByVal bv_strBillingFlag As String, _
                                         ByVal bv_blnActiveBit As Boolean, _
                                         ByVal bv_i64DepotId As Int64, _
                                         ByVal bv_strGateInTransactionNo As String, _
                                         ByVal bv_blnSlabRate As Boolean, _
                                         ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING_CHARGE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CleaningData._CLEANING_CHARGE, br_objTrans)
                .Item(CleaningData.CLNNG_CHRG_ID) = intMax
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(CleaningData.CSTMR_ID) = bv_i64CustomerID
                .Item(CleaningData.CLNNG_ID) = bv_intCleaningID
                .Item(CleaningData.ORGNL_CLNNG_DT) = bv_datOriginalCleaningDate
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If

                .Item(CleaningData.BLLNG_FLG) = bv_strBillingFlag

                .Item(CleaningData.ACTV_BT) = bv_blnActiveBit
                .Item(CleaningData.DPT_ID) = bv_i64DepotId
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(CleaningData.SLB_RT_BT) = bv_blnSlabRate
            End With
            objData.InsertRow(dr, Cleaning_ChargeInsertQuery_Clean, br_objTrans)
            dr = Nothing
            CreateCleaningCharge_Clean = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function UpdateCleaningCharge_CleanCombination(ByVal bv_strEquipmentNo As String, _
                                        ByVal bv_i64CustomerID As Int64, _
                                        ByVal bv_intCleaningID As Int64, _
                                        ByVal bv_datOriginalCleaningDate As DateTime, _
                                        ByVal bv_dblCleaningRate As String, _
                                        ByVal bv_strBillingFlag As String, _
                                        ByVal bv_blnActiveBit As Boolean, _
                                        ByVal bv_i64DepotId As Int64, _
                                        ByVal bv_strGateInTransactionNo As String, _
                                        ByVal bv_blnSlabRate As Boolean, _
                                        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING_CHARGE).NewRow()
            With dr


                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(CleaningData.CSTMR_ID) = bv_i64CustomerID
                .Item(CleaningData.ORGNL_CLNNG_DT) = bv_datOriginalCleaningDate

                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If

                .Item(CleaningData.BLLNG_FLG) = bv_strBillingFlag

                .Item(CleaningData.ACTV_BT) = bv_blnActiveBit
                .Item(CleaningData.DPT_ID) = bv_i64DepotId
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(CleaningData.SLB_RT_BT) = bv_blnSlabRate
            End With

            UpdateCleaningCharge_CleanCombination = objData.UpdateRow(dr, UpdateCleaningCharge_CleanCombination_UpdateQry, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CreateCleaning_Clean(ByVal bv_i64CustomerId As Int64, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByVal bv_strChemicalName As String, _
                                   ByVal bv_dblCleaningRate As String, _
                                   ByVal bv_datOriginalCleaningDate As DateTime, _
                                   ByVal bv_datLastCleaningDate As DateTime, _
                                   ByVal bv_i64EquipmentStatusId As Int64, _
                                   ByVal bv_strCleaningRefNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_i32DepotId As Int32, _
                                   ByVal bv_strGateInTransactionNo As String, _
                                   ByVal bv_strCreatedBy As String, _
                                   ByVal bv_datCreatedDate As DateTime, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_datModifiedDate As DateTime, _
                                   ByVal bv_blnCleaningGenerationFlag As Boolean, _
                                   ByVal bv_StrIsChangeOfStatus As String, _
                                   ByVal bv_blnAdditionalCleaningFlag As Boolean, _
                                   ByVal bv_blnCleaningFlag As Boolean, _
                                   ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CleaningData._CLEANING, br_objTrans)
                If bv_StrIsChangeOfStatus = "C" Then
                    Dim intMaxNo As Integer = CommonUIs.GetIdentityValue(CleaningData._CLEANING_CERTIFICATE, br_objTrans)
                End If
                .Item(CleaningData.CLNNG_ID) = intMax
                .Item(CleaningData.CLNNG_CD) = intMax
                .Item(CleaningData.CSTMR_ID) = bv_i64CustomerId
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo

                If bv_strChemicalName <> Nothing Then
                    .Item(CleaningData.CHMCL_NM) = bv_strChemicalName
                Else
                    .Item(CleaningData.CHMCL_NM) = DBNull.Value
                End If
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If

                .Item(CleaningData.ORGNL_CLNNG_DT) = bv_datOriginalCleaningDate
                .Item(CleaningData.LST_CLNNG_DT) = bv_datLastCleaningDate

                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId

                If bv_strCleaningRefNo <> Nothing Then
                    .Item(CleaningData.CLNNG_RFRNC_NO) = bv_strCleaningRefNo
                Else
                    .Item(CleaningData.CLNNG_RFRNC_NO) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i32DepotId
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(CleaningData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(CleaningData.CRTD_BY) = bv_strCreatedBy
                .Item(CleaningData.CRTD_DT) = bv_datCreatedDate
                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_datModifiedDate
                .Item(CleaningData.CERT_GNRTD_FLG) = bv_blnCleaningGenerationFlag
                '.Item(CleaningData.ADDTNL_CLNNG_BT) = bv_blnAdditionalCleaningFlag
                '.Item(CleaningData.ADDTNL_CLNNG_FLG) = bv_blnCleaningFlag
                .Item(CleaningData.ADDTNL_CLNNG_BT) = False
                .Item(CleaningData.ADDTNL_CLNNG_FLG) = False
                'If bv_StrIsChangeOfStatus <> Nothing Then
                '    .Item(CleaningData.IS_CHNG_OF_STTS) = bv_StrIsChangeOfStatus
                'Else
                '    .Item(CleaningData.IS_CHNG_OF_STTS) = DBNull.Value
                'End If

                'Dim strCleaningCertificateNo As String = CommonUIs.GetIdentityCode(CleaningData._CLEANING_CERTIFICATE, intMax, Now, br_objTrans)

                'If strCleaningCertificateNo <> Nothing Then
                '    .Item(CleaningData.CLNNG_CERT_NO) = strCleaningCertificateNo
                'Else
                '    .Item(CleaningData.CLNNG_CERT_NO) = DBNull.Value
                'End If


            End With
            objData.InsertRow(dr, CleaningInsertQuery_Clean, br_objTrans)
            dr = Nothing
            CreateCleaning_Clean = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateCleaning_CleanCombination(ByVal bv_i64CustomerId As Int64, _
                                 ByVal bv_strEquipmentNo As String, _
                                 ByVal bv_strChemicalName As String, _
                                 ByVal bv_dblCleaningRate As String, _
                                 ByVal bv_datOriginalCleaningDate As DateTime, _
                                 ByVal bv_datLastCleaningDate As DateTime, _
                                 ByVal bv_i64EquipmentStatusId As Int64, _
                                 ByVal bv_strCleaningRefNo As String, _
                                 ByVal bv_strRemarks As String, _
                                 ByVal bv_i32DepotId As Int32, _
                                 ByVal bv_strGateInTransactionNo As String, _
                                 ByVal bv_strCreatedBy As String, _
                                 ByVal bv_datCreatedDate As DateTime, _
                                 ByVal bv_strModifiedBy As String, _
                                 ByVal bv_datModifiedDate As DateTime, _
                                 ByVal bv_blnCleaningGenerationFlag As Boolean, _
                                 ByVal bv_StrIsChangeOfStatus As String, _
                                 ByVal bv_blnAdditionalCleaningFlag As Boolean, _
                                 ByVal bv_blnCleaningFlag As Boolean, _
                                 ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow

            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING).NewRow()
            With dr

                .Item(CleaningData.CSTMR_ID) = bv_i64CustomerId
                .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo

                If bv_strChemicalName <> Nothing Then
                    .Item(CleaningData.CHMCL_NM) = bv_strChemicalName
                Else
                    .Item(CleaningData.CHMCL_NM) = DBNull.Value
                End If
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If
                .Item(CleaningData.ORGNL_CLNNG_DT) = bv_datOriginalCleaningDate
                .Item(CleaningData.LST_CLNNG_DT) = bv_datLastCleaningDate

                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId

                If bv_strCleaningRefNo <> Nothing Then
                    .Item(CleaningData.CLNNG_RFRNC_NO) = bv_strCleaningRefNo
                Else
                    .Item(CleaningData.CLNNG_RFRNC_NO) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i32DepotId
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(CleaningData.GI_TRNSCTN_NO) = DBNull.Value
                End If

                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_datModifiedDate
                .Item(CleaningData.CERT_GNRTD_FLG) = bv_blnCleaningGenerationFlag

                '.Item(CleaningData.ADDTNL_CLNNG_BT) = False
                '.Item(CleaningData.ADDTNL_CLNNG_FLG) = False





            End With

            UpdateCleaning_CleanCombination = objData.UpdateRow(dr, UpdateCleaning_CleanCombination_UpdateQry, br_objTrans)

            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Function UpdateTracking_Clean(ByVal bv_strEqpmntNo As String, _
                                  ByVal bv_datActivitydate As DateTime, _
                                  ByVal bv_strActivityRemarks As String, _
                                  ByVal bv_strGateInTransactionNumber As String, _
                                  ByVal bv_i32DepotID As Int32, _
                                  ByVal bv_strActivityName As String, _
                                  ByVal bv_intActvtyNo As Integer, _
                                  ByVal bv_strModifiedBy As String, _
                                  ByVal bv_dtModifiedDate As DateTime, _
                                  ByVal bv_strEquipmentInformationRemarks As String, _
                                  ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._TRACKING).NewRow()
            With dr
                .Item(CleaningData.EQPMNT_NO) = bv_strEqpmntNo
                .Item(CleaningData.ACTVTY_DT) = bv_datActivitydate
                If bv_strActivityRemarks <> Nothing Then
                    .Item(CleaningData.ACTVTY_RMRKS) = bv_strActivityRemarks
                Else
                    .Item(CleaningData.ACTVTY_RMRKS) = DBNull.Value
                End If
                .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNumber

                .Item(CleaningData.DPT_ID) = bv_i32DepotID
                .Item(CleaningData.ACTVTY_NAM) = bv_strActivityName
                .Item(CleaningData.ACTVTY_NO) = bv_intActvtyNo
                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_dtModifiedDate
                If bv_strEquipmentInformationRemarks <> Nothing Then
                    .Item(CleaningData.EQPMNT_INFRMTN_RMRKS_VC) = bv_strEquipmentInformationRemarks
                Else
                    .Item(CleaningData.EQPMNT_INFRMTN_RMRKS_VC) = DBNull.Value
                End If
            End With
            UpdateTracking_Clean = objData.UpdateRow(dr, TrackingUpdateQuery_Clean, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Check the Cleaning combination
    Public Function CheckCleaningCombination(ByVal bv_strEQPMNT_NO As String, ByVal bv_strGI_TRNSCTN_NO As String, _
                                             ByVal intCSTMR_ID As Int64, ByVal intDPT_ID As Int64, ByRef br_objTrans As Transactions) As Int32
        Try

            Dim hshTable As New Hashtable()
            hshTable.Add(CleaningData.EQPMNT_NO, bv_strEQPMNT_NO)
            hshTable.Add(CleaningData.GI_TRNSCTN_NO, bv_strGI_TRNSCTN_NO)
            hshTable.Add(CleaningData.CSTMR_ID, intCSTMR_ID)
            hshTable.Add(CleaningData.DPT_ID, intDPT_ID)


            objData = New DataObjects(CheckCleaningCombination_SelectQuery, hshTable)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Update Print Cleaning inspection ref No

    Public Function UpdateActvity_CleaningInspectionRefNo(ByVal bv_strEqpmntNo As String, _
                                  ByVal bv_strCleaningInspectionRefNo As String, _
                                  ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._ACTIVITY_STATUS).NewRow()
            With dr

                .Item(CleaningData.EQPMNT_NO) = bv_strEqpmntNo
                .Item(CleaningData.CLNNG_INSPCTN_REF_NO) = bv_strCleaningInspectionRefNo

            End With
            UpdateActvity_CleaningInspectionRefNo = objData.UpdateRow(dr, UpdateActvity_CleaningInspectionRefNo_UpdateQry, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Inspection - Operations"

    'Public Function DeleteCleaning(ByVal bv_i64CleaningId As Int64, ByVal bv_i32DepotId As Int32, ByRef br_objTrans As Transactions)

    '    Dim dr As DataRow
    '    objData = New DataObjects()
    '    Try
    '        dr = ds.Tables(CleaningData._CLEANING).NewRow()
    '        With dr
    '            .Item(CleaningData.CLNNG_ID) = bv_i64CleaningId
    '            .Item(CleaningData.DPT_ID) = bv_i32DepotId
    '        End With
    '        DeleteCleaning = objData.DeleteRow(dr, Cleaning_DeleteQuery_Ins, br_objTrans)
    '        dr = Nothing
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Public Function DeleteCleaning_Charge(ByVal bv_strEquipmentNo As String, ByVal bv_intCleaningID As Int64, ByVal bv_i32DepotId As Int32, _
    '                                       ByVal bv_strGateInTransactionNo As String, ByRef br_objTrans As Transactions)
    '    Dim dr As DataRow
    '    objData = New DataObjects()
    '    Try
    '        dr = ds.Tables(CleaningData._CLEANING_CHARGE).NewRow()
    '        With dr
    '            .Item(CleaningData.EQPMNT_NO) = bv_strEquipmentNo
    '            .Item(CleaningData.CLNNG_ID) = bv_intCleaningID
    '            .Item(CleaningData.DPT_ID) = bv_i32DepotId
    '            .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
    '        End With
    '        DeleteCleaning_Charge = objData.DeleteRow(dr, Cleaning_Charge_DeleteQuery_Ins, br_objTrans)
    '        dr = Nothing
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function UpdateCleaning_Inspection(ByVal bv_i64CleaningId As Int64, _
                                 ByVal bv_dblCleaningRate As String, _
                                 ByVal bv_datLastCleaningDate As DateTime, _
                                 ByVal bv_datOrginalInspectedDate As DateTime, _
                                 ByVal bv_datLastInspectedDate As DateTime, _
                                 ByVal bv_i64EquipmentStatusId As Int64, _
                                 ByVal bv_strCleanedFor As String, _
                                 ByVal bv_strLocationofcleaning As String, _
                                 ByVal bv_i64EquipmentCleaningstatus1 As Int64, _
                                 ByVal bv_i64EquipmentCleaningstatus2 As Int64, _
                                 ByVal bv_i64EquipmentCondition As Int64, _
                                 ByVal bv_i32Valvefittingcondition As Int64, _
                                 ByVal bv_strLidSealNo As String, _
                                 ByVal bv_strTopSealNo As String, _
                                 ByVal bv_strBottomSealNo As String, _
                                 ByVal bv_i64InvoicingPartyId As Int64, _
                                 ByVal bv_strCustomerReferencceNo As String, _
                                 ByVal bv_strCleaningRefNo As String, _
                                 ByVal bv_strRemarks As String, _
                                 ByVal bv_i32DepotId As Int32, _
                                 ByVal bv_strGateInTransactionNo As String, _
                                 ByVal bv_strModifiedBy As String, _
                                 ByVal bv_datModifiedDate As DateTime, _
                                 ByVal bv_strIsChangeOfStatus As String, _
                                 ByVal bv_blnAdditionalCleaningBit As Boolean, _
                                 ByVal bv_blnCleaningFlag As Boolean, _
                                 ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningData._CLEANING).NewRow()
            With dr
                .Item(CleaningData.CLNNG_ID) = bv_i64CleaningId
                If bv_dblCleaningRate <> Nothing Then
                    .Item(CleaningData.CLNNG_RT) = CDbl(bv_dblCleaningRate)
                Else
                    .Item(CleaningData.CLNNG_RT) = DBNull.Value
                End If

                .Item(CleaningData.LST_CLNNG_DT) = bv_datLastCleaningDate
                .Item(CleaningData.LST_INSPCTD_DT) = bv_datLastInspectedDate
                .Item(CleaningData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                If bv_strCleanedFor <> Nothing Then
                    .Item(CleaningData.CLND_FR_VCR) = bv_strCleanedFor
                Else
                    .Item(CleaningData.CLND_FR_VCR) = DBNull.Value
                End If
                If bv_strLocationofcleaning <> Nothing Then
                    .Item(CleaningData.LCTN_OF_CLNG) = bv_strLocationofcleaning
                Else
                    .Item(CleaningData.LCTN_OF_CLNG) = DBNull.Value
                End If
                If bv_datOrginalInspectedDate <> Nothing Then
                    .Item(CleaningData.ORGNL_INSPCTD_DT) = bv_datOrginalInspectedDate
                Else
                    .Item(CleaningData.ORGNL_INSPCTD_DT) = DBNull.Value
                End If
                If bv_i64EquipmentCleaningstatus1 <> 0 Then
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_1) = bv_i64EquipmentCleaningstatus1
                Else
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_1) = DBNull.Value
                End If
                If bv_i64EquipmentCleaningstatus2 <> 0 Then
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_2) = bv_i64EquipmentCleaningstatus2
                Else
                    .Item(CleaningData.EQPMNT_CLNNG_STTS_2) = DBNull.Value
                End If
                If bv_i64EquipmentCondition <> 0 Then
                    .Item(CleaningData.EQPMNT_CNDTN_ID) = bv_i64EquipmentCondition
                Else
                    .Item(CleaningData.EQPMNT_CNDTN_ID) = DBNull.Value
                End If
                If bv_i32Valvefittingcondition <> 0 Then
                    .Item(CleaningData.VLV_FTTNG_CNDTN) = bv_i32Valvefittingcondition
                Else
                    .Item(CleaningData.VLV_FTTNG_CNDTN) = DBNull.Value
                End If
                If bv_strLidSealNo <> Nothing Then
                    .Item(CleaningData.MN_LID_SL_NO) = bv_strLidSealNo
                Else
                    .Item(CleaningData.MN_LID_SL_NO) = DBNull.Value
                End If
                If bv_strTopSealNo <> Nothing Then
                    .Item(CleaningData.TP_SL_NO) = bv_strTopSealNo
                Else
                    .Item(CleaningData.TP_SL_NO) = DBNull.Value
                End If
                If bv_strBottomSealNo <> Nothing Then
                    .Item(CleaningData.BTTM_SL_NO) = bv_strBottomSealNo
                Else
                    .Item(CleaningData.BTTM_SL_NO) = DBNull.Value
                End If
                If bv_i64InvoicingPartyId <> 0 Then
                    .Item(CleaningData.INVCNG_PRTY_ID) = bv_i64InvoicingPartyId
                Else
                    .Item(CleaningData.INVCNG_PRTY_ID) = DBNull.Value
                End If
                If bv_strCustomerReferencceNo <> Nothing Then
                    .Item(CleaningData.CSTMR_RFRNC_NO) = bv_strCustomerReferencceNo
                Else
                    .Item(CleaningData.CSTMR_RFRNC_NO) = DBNull.Value
                End If
                If bv_strCleaningRefNo <> Nothing Then
                    .Item(CleaningData.CLNNG_RFRNC_NO) = bv_strCleaningRefNo
                Else
                    .Item(CleaningData.CLNNG_RFRNC_NO) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(CleaningData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(CleaningData.RMRKS_VC) = DBNull.Value
                End If
                .Item(CleaningData.DPT_ID) = bv_i32DepotId
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(CleaningData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(CleaningData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                .Item(CleaningData.IS_CHNG_OF_STTS) = bv_strIsChangeOfStatus
                .Item(CleaningData.ADDTNL_CLNNG_BT) = bv_blnAdditionalCleaningBit
                .Item(CleaningData.ADDTNL_CLNNG_FLG) = bv_blnCleaningFlag
                .Item(CleaningData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningData.MDFD_DT) = bv_datModifiedDate


            End With
            UpdateCleaning_Inspection = objData.UpdateRow(dr, UpdateCleaning_Inspection_UpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function pub_CustomerEDIValidation(ByVal bv_strCustomerCode As String, ByVal bv_intDPT_ID As Int32) As Int32

        Try

            objData = New DataObjects()
            Dim dt As New DataTable
            Dim hshTable As New Hashtable()
            hshTable.Add(CleaningData.CSTMR_CD, bv_strCustomerCode)
            hshTable.Add(CleaningData.DPT_ID, bv_intDPT_ID)
            objData = New DataObjects(CustomerEDIValidation_SelectQry, hshTable)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function pub_GetCleaningInsructionReportDetails(ByVal bv_strEquipmentNo As String, ByVal bv_intDPT_ID As Int32) As DataTable

        Try
            Dim dt As New DataTable
            objData = New DataObjects()
            Dim hshTable As New Hashtable()
            hshTable.Add(CleaningData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(CleaningData.DPT_ID, bv_intDPT_ID)
            objData = New DataObjects(GetCleaningInsructionReportDetails_SelectQry, hshTable)
            objData.Fill(dt)
            dt.TableName = CleaningData._PRINT_CLEANING_INSPECTION
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function



#End Region



#End Region

#Region "Cleaning Slab Rate"

#Region "GetCleaningSlabRate"
    Public Function GetCleaningSlabRate(ByVal bv_i32Cstmr_id As Integer, ByVal bv_Eqpmnt_Typ_ID As Integer) As DataTable
        Try
            Dim dt As New DataTable
            Dim hshParameters As New Hashtable()
            hshParameters.Add(CleaningData.CSTMR_ID, bv_i32Cstmr_id)
            hshParameters.Add(CleaningData.EQPMNT_TYP_ID, bv_Eqpmnt_Typ_ID)
            objData = New DataObjects(Cleaning_Slab_rate_ByCustomerID, hshParameters)
            objData.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#End Region

End Class
