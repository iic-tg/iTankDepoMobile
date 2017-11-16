Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Business

#Region "Gateins"

Public Class GateIns

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const AgentNameByCustmrID_SelectQuery As String = "SELECT AGENT_ID FROM CUSTOMER WHERE CSTMR_CD=@CSTMR_CD"
    Private Const GateinRetInsertQuery As String = "INSERT INTO GATEIN_RET(GI_SNO,GI_TRANSMISSION_NO,GI_COMPLETE,GI_SENT_EIR,GI_SENT_DATE,GI_REC_EIR,GI_REC_DATE,GI_REC_ADDR,GI_REC_TYPE,GI_EXPORTED,GI_EXPOR_DATE,GI_IMPORTED,GI_IMPOR_DATE,GI_TRNSXN,GI_ADVICE,GI_UNIT_NBR,GI_EQUIP_TYPE,GI_EQUIP_DESC,GI_EQUIP_CODE,GI_CONDITION,GI_COMP_ID_A,GI_COMP_ID_N,GI_COMP_ID_C,GI_COMP_TYPE,GI_COMP_DESC,GI_COMP_CODE,GI_EIR_DATE,GI_EIR_TIME,GI_REFERENCE,GI_MANU_DATE,GI_MATERIAL,GI_WEIGHT,GI_MEASURE,GI_UNITS,GI_CSC_REEXAM,GI_COUNTRY,GI_LIC_STATE,GI_LIC_REG,GI_LIC_EXPIRE,GI_LSR_OWNER,GI_SEND_EDI_1,GI_SSL_LSE,GI_SEND_EDI_2,GI_HAULIER,GI_SEND_EDI_3,GI_DPT_TRM,GI_SEND_EDI_4,GI_OTHERS_1,GI_OTHERS_2,GI_OTHERS_3,GI_OTHERS_4,GI_NOTE_1,GI_NOTE_2,GI_LOAD,GI_FHWA,GI_LAST_OH_LOC,GI_LAST_OH_DATE,GI_SENDER,GI_ATTENTION,GI_REVISION,GI_SEND_EDI_5,GI_SEND_EDI_6,GI_SEND_EDI_7,GI_SEND_EDI_8,GI_SEAL_PARTY_1,GI_SEAL_NUMBER_1,GI_SEAL_PARTY_2,GI_SEAL_NUMBER_2,GI_SEAL_PARTY_3,GI_SEAL_NUMBER_3,GI_SEAL_PARTY_4,GI_SEAL_NUMBER_4,GI_PORT_FUNC_CODE,GI_PORT_NAME,GI_VESSEL_NAME,GI_VOYAGE_NUM,GI_HAZ_MAT_CODE,GI_HAZ_MAT_DESC,GI_NOTE_3,GI_NOTE_4,GI_NOTE_5,GI_COMP_ID_A_2,GI_COMP_ID_N_2,GI_COMP_ID_C_2,GI_COMP_TYPE_2,GI_COMP_CODE_2,GI_COMP_DESC_2,GI_SHIPPER,GI_DRAY_ORDER,GI_RAIL_ID,GI_RAIL_RAMP,GI_VESSEL_CODE,GI_WGHT_CERT_1,GI_WGHT_CERT_2,GI_WGHT_CERT_3,GI_SEA_RAIL,GI_LOC_IDENT,GI_PORT_LOC_QUAL,GI_STATUS,GI_PICK_DATE,GI_ESTSTATUS,GI_ERRSTATUS,GI_USERNAME,GI_LIVE_STATUS,GI_ISACTIVE,GI_YARD_LOC,GI_MODE_PAYMENT,GI_BILLING_TYPE,GI_RESERVE_BKG,GI_RCESTSTATUS,OP_SNO,OP_STATUS)VALUES(@GI_SNO,@GI_TRANSMISSION_NO,@GI_COMPLETE,@GI_SENT_EIR,@GI_SENT_DATE,@GI_REC_EIR,@GI_REC_DATE,@GI_REC_ADDR,@GI_REC_TYPE,@GI_EXPORTED,@GI_EXPOR_DATE,@GI_IMPORTED,@GI_IMPOR_DATE,@GI_TRNSXN,@GI_ADVICE,@GI_UNIT_NBR,@GI_EQUIP_TYPE,@GI_EQUIP_DESC,@GI_EQUIP_CODE,@GI_CONDITION,@GI_COMP_ID_A,@GI_COMP_ID_N,@GI_COMP_ID_C,@GI_COMP_TYPE,@GI_COMP_DESC,@GI_COMP_CODE,@GI_EIR_DATE,@GI_EIR_TIME,@GI_REFERENCE,@GI_MANU_DATE,@GI_MATERIAL,@GI_WEIGHT,@GI_MEASURE,@GI_UNITS,@GI_CSC_REEXAM,@GI_COUNTRY,@GI_LIC_STATE,@GI_LIC_REG,@GI_LIC_EXPIRE,@GI_LSR_OWNER,@GI_SEND_EDI_1,@GI_SSL_LSE,@GI_SEND_EDI_2,@GI_HAULIER,@GI_SEND_EDI_3,@GI_DPT_TRM,@GI_SEND_EDI_4,@GI_OTHERS_1,@GI_OTHERS_2,@GI_OTHERS_3,@GI_OTHERS_4,@GI_NOTE_1,@GI_NOTE_2,@GI_LOAD,@GI_FHWA,@GI_LAST_OH_LOC,@GI_LAST_OH_DATE,@GI_SENDER,@GI_ATTENTION,@GI_REVISION,@GI_SEND_EDI_5,@GI_SEND_EDI_6,@GI_SEND_EDI_7,@GI_SEND_EDI_8,@GI_SEAL_PARTY_1,@GI_SEAL_NUMBER_1,@GI_SEAL_PARTY_2,@GI_SEAL_NUMBER_2,@GI_SEAL_PARTY_3,@GI_SEAL_NUMBER_3,@GI_SEAL_PARTY_4,@GI_SEAL_NUMBER_4,@GI_PORT_FUNC_CODE,@GI_PORT_NAME,@GI_VESSEL_NAME,@GI_VOYAGE_NUM,@GI_HAZ_MAT_CODE,@GI_HAZ_MAT_DESC,@GI_NOTE_3,@GI_NOTE_4,@GI_NOTE_5,@GI_COMP_ID_A_2,@GI_COMP_ID_N_2,@GI_COMP_ID_C_2,@GI_COMP_TYPE_2,@GI_COMP_CODE_2,@GI_COMP_DESC_2,@GI_SHIPPER,@GI_DRAY_ORDER,@GI_RAIL_ID,@GI_RAIL_RAMP,@GI_VESSEL_CODE,@GI_WGHT_CERT_1,@GI_WGHT_CERT_2,@GI_WGHT_CERT_3,@GI_SEA_RAIL,@GI_LOC_IDENT,@GI_PORT_LOC_QUAL,@GI_STATUS,@GI_PICK_DATE,@GI_ESTSTATUS,@GI_ERRSTATUS,@GI_USERNAME,@GI_LIVE_STATUS,@GI_ISACTIVE,@GI_YARD_LOC,@GI_MODE_PAYMENT,@GI_BILLING_TYPE,@GI_RESERVE_BKG,@GI_RCESTSTATUS,@OP_SNO,@OP_STATUS)"
    Private Const GateinSelectQueryByDepotId = "SELECT GTN_ID,GTN_CD,CSTMR_ID,(SELECT CSTMR_CD FROM CUSTOMER WHERE CSTMR_ID=GATEIN.CSTMR_ID)CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,EQPMNT_STTS_ID,YRD_LCTN,GTN_DT,GTN_TM,PRDCT_ID,EIR_NO,VHCL_NO,TRNSPRTR_CD,HTNG_BT,RMRKS_VC,GI_TRNSCTN_NO,GTOT_BT,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT FROM GATEIN WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID ORDER BY GTN_ID DESC"
    Private Const GateinSelectQuerybyMoreInfo As String = "SELECT EQPMNT_NO,MNFCTR_DT,ACEP_CD,MTRL_CD,MSR_CD,UNT_CD,LST_OH_LOC, LST_OH_DT,CNTRY_CD,TRCKR_CD ,LDD_STTS_CD,LIC_STT,LIC_REG,LIC_EXPR,NT_1_VC,NT_2_VC,NT_3_VC,NT_4_VC,NT_5_VC,SL_PRTY_1_CD,SL_PRTY_2_CD,SL_PRTY_3_CD,SL_NMBR_1,SL_NMBR_2,SL_NMBR_3,PRT_FNC_CD,PRT_NM,LOC_DNT,PRT_LC_CD,SHPPR_NAM,RL_ID_VC,RL_RMP_LOC,VSSL_NM,VYG_NO,VSSL_CD,HAZ_MTL_CD,HAZ_MATL_DSC,GRSS_WGHT_NC,TR_WGHT_NC,(SELECT CSC_VLDTY  FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO=V.EQPMNT_NO AND DPT_ID=V.DPT_ID)CSC_VLDTY FROM V_GATEIN V WHERE EQPMNT_NO=@EQPMNT_NO"
    Private Const Equipment_Status_SelectQuery As String = "SELECT EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC FROM EQUIPMENT_STATUS WHERE ACTV_BT = 1 AND DPT_ID=@DPT_ID"
    Private Const Customer_SelectQuery As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM FROM CUSTOMER WHERE ACTV_BT = 1 AND DPT_ID=@DPT_ID"
    Private Const GateinSelectQueryfromPreAdvice As String = "SELECT PR_ADVC_ID,PR_ADVC_ID AS GTN_BIN,PR_ADVC_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_SZ_ID,EQPMNT_SZ_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,PRDCT_ID,PRDCT_CD,CLNNG_RFRNC_NO,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,GI_TRNSCTN_NO,PRDCT_DSCRPTN_VC,COUNT_ATTACH,AGNT_ID,BLL_ID,BLL_CD,AGNT_CD,GRD_ID,AUTH_NO AS 'RDL_ATH',CNSGN_NM AS 'CNSGNE',(SELECT MNFCTR_DT FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO =V.EQPMNT_NO )MNFCTR_DT,(SELECT CSC_VLDTY FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO =V.EQPMNT_NO )CSC_VLDTY FROM V_PRE_ADVICE V WHERE DPT_ID=@DPT_ID AND GI_TRNSCTN_NO IS NULL ORDER BY PR_ADVC_ID DESC"
    Private Const GateinSelectQueryfromGateIn As String = "SELECT GTN_ID,GTN_CD,CSTMR_ID,CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC,YRD_LCTN,GTN_DT,GTN_TM,PRDCT_ID,PRDCT_CD,EIR_NO,VHCL_NO,TRNSPRTR_CD,HTNG_BT,RMRKS_VC,GI_TRNSCTN_NO,RNTL_RFRNC_NO,RNTL_BT,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,CSTMR_NAM,(CASE WHEN (SELECT COUNT(HNDLNG_CHRG_ID) FROM HANDLING_CHARGE WHERE GI_TRNSCTN_NO=VG.GI_TRNSCTN_NO AND BLLNG_FLG='B')=1 THEN 1 WHEN (SELECT COUNT(HTNG_ID) FROM HEATING_CHARGE WHERE GI_TRNSCTN_NO=VG.GI_TRNSCTN_NO)=1 THEN 1 ELSE 0 END)HTNG_EDIT,PRDCT_DSCRPTN_VC,COUNT_ATTACH,NULL as PR_ADVC_ID,RDL_ATH,GRD_ID,BLL_ID,(SELECT CASE WHEN BLL_ID='CUSTOMER' THEN 'CUSTOMER' WHEN BLL_ID='AGENT' THEN 'AGENT' END) AS 'BLL_CD',CNSGNE,GRD_CD,AGNT_CD,(SELECT CSC_VLDTY  FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO=VG.EQPMNT_NO AND DPT_ID=VG.DPT_ID)CSC_VLDTY,(SELECT MNFCTR_DT FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO =VG.EQPMNT_NO AND DPT_ID=VG.DPT_ID )MNFCTR_DT,AGNT_ID FROM V_GATEIN AS VG WHERE GI_TRNSCTN_NO IN (SELECT GI_TRNSCTN_NO FROM ACTIVITY_STATUS WHERE EQPMNT_STTS_ID IN (SELECT DEFAULT_STATUS_ID FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME ='Gate In' AND DPT_ID=@DPT_ID)) AND DPT_ID=@DPT_ID AND EQPMNT_NO NOT IN(SELECT EQPMNT_NO FROM RENTAL_CHARGE WHERE OFF_HR_BLLNG_FLG='Y' AND RNTL_RFRNC_NO=VG.RNTL_RFRNC_NO) ORDER BY GTN_ID DESC"
    Private Const GateinInsertQuery As String = "INSERT INTO GATEIN (GTN_ID,GTN_CD,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,EQPMNT_STTS_ID,YRD_LCTN,GTN_DT,GTN_TM,PRDCT_ID,EIR_NO,VHCL_NO,TRNSPRTR_CD,HTNG_BT,RMRKS_VC,GI_TRNSCTN_NO,RNTL_RFRNC_NO,GTOT_BT,RNTL_BT,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,BLL_ID,RDL_ATH,CNSGNE,GRD_ID,AGNT_ID)VALUES(@GTN_ID,@GTN_CD,@CSTMR_ID,@EQPMNT_NO,@EQPMNT_TYP_ID,@EQPMNT_CD_ID,@EQPMNT_STTS_ID,@YRD_LCTN,@GTN_DT,@GTN_TM,@PRDCT_ID,@EIR_NO,@VHCL_NO,@TRNSPRTR_CD,@HTNG_BT,@RMRKS_VC,@GI_TRNSCTN_NO,@RNTL_RFRNC_NO,@GTOT_BT,@RNTL_BT,@DPT_ID,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@BLL_ID,@RDL_ATH,@CNSGNE,@GRD_ID,@AGNT_ID)"
    Private Const GateinUpdateQuery As String = "UPDATE GATEIN SET AGNT_ID=@AGNT_ID, YRD_LCTN=@YRD_LCTN, GTN_DT=@GTN_DT, GTN_TM=@GTN_TM, PRDCT_ID=@PRDCT_ID, EIR_NO=@EIR_NO, VHCL_NO=@VHCL_NO, TRNSPRTR_CD=@TRNSPRTR_CD, HTNG_BT=@HTNG_BT, RMRKS_VC=@RMRKS_VC,MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT,BLL_ID=@BLL_ID,CNSGNE=@CNSGNE,RDL_ATH=@RDL_ATH,GRD_CD=@GRD_CD,GRD_ID=@GRD_ID WHERE EQPMNT_NO=@EQPMNT_NO AND GTN_ID=@GTN_ID AND DPT_ID=@DPT_ID"
    Private Const Gatein_DetailInsertQuery As String = "INSERT INTO GATEIN_DETAIL(GTN_DTL_ID,GTN_ID,MNFCTR_DT,ACEP_CD,MTRL_ID,GRSS_WGHT_NC,TR_WGHT_NC,MSR_ID,UNT_ID,LST_OH_LOC,LST_OH_DT,TRCKR_CD,LOD_STTS_CD,CNTRY_ID,LIC_STT,LIC_REG,LIC_EXPR,NT_1_VC,NT_2_VC,NT_3_VC,NT_4_VC,NT_5_VC,SL_PRTY_1_ID,SL_NMBR_1,SL_PRTY_2_ID,SL_NMBR_2,SL_PRTY_3_ID,SL_NMBR_3,PRT_FNC_CD,PRT_NM,PRT_NO,PRT_LC_CD,VSSL_NM,VYG_NO,VSSL_CD,SHPPR_NAM,RL_ID_VC,RL_RMP_LOC,HAZ_MTL_CD,HAZ_MATL_DSC)VALUES(@GTN_DTL_ID,@GTN_ID,@MNFCTR_DT,@ACEP_CD,@MTRL_ID,@GRSS_WGHT_NC,@TR_WGHT_NC,@MSR_ID,@UNT_ID,@LST_OH_LOC,@LST_OH_DT,@TRCKR_CD,@LOD_STTS_CD,@CNTRY_ID,@LIC_STT,@LIC_REG,@LIC_EXPR,@NT_1_VC,@NT_2_VC,@NT_3_VC,@NT_4_VC,@NT_5_VC,@SL_PRTY_1_ID,@SL_NMBR_1,@SL_PRTY_2_ID,@SL_NMBR_2,@SL_PRTY_3_ID,@SL_NMBR_3,@PRT_FNC_CD,@PRT_NM,@PRT_NO,@PRT_LC_CD,@VSSL_NM,@VYG_NO,@VSSL_CD,@SHPPR_NAM,@RL_ID_VC,@RL_RMP_LOC,@HAZ_MTL_CD,@HAZ_MATL_DSC)"
    Private Const Gatein_DetailUpdateQuery As String = "UPDATE GATEIN_DETAIL SET GTN_ID=@GTN_ID, MNFCTR_DT=@MNFCTR_DT, ACEP_CD=@ACEP_CD, MTRL_ID=@MTRL_ID, GRSS_WGHT_NC=@GRSS_WGHT_NC, TR_WGHT_NC=@TR_WGHT_NC, MSR_ID=@MSR_ID, UNT_ID=@UNT_ID, LST_OH_LOC=@LST_OH_LOC, LST_OH_DT=@LST_OH_DT, TRCKR_CD=@TRCKR_CD, LOD_STTS_CD=@LOD_STTS_CD, CNTRY_ID=@CNTRY_ID, LIC_STT=@LIC_STT, LIC_REG=@LIC_REG, LIC_EXPR=@LIC_EXPR, NT_1_VC=@NT_1_VC, NT_2_VC=@NT_2_VC, NT_3_VC=@NT_3_VC, NT_4_VC=@NT_4_VC, NT_5_VC=@NT_5_VC, SL_PRTY_1_ID=@SL_PRTY_1_ID, SL_NMBR_1=@SL_NMBR_1, SL_PRTY_2_ID=@SL_PRTY_2_ID, SL_NMBR_2=@SL_NMBR_2, SL_PRTY_3_ID=@SL_PRTY_3_ID, SL_NMBR_3=@SL_NMBR_3, PRT_FNC_CD=@PRT_FNC_CD, PRT_NM=@PRT_NM, PRT_NO=@PRT_NO, PRT_LC_CD=@PRT_LC_CD, VSSL_NM=@VSSL_NM, VYG_NO=@VYG_NO, VSSL_CD=@VSSL_CD, SHPPR_NAM=@SHPPR_NAM, RL_ID_VC=@RL_ID_VC, RL_RMP_LOC=@RL_RMP_LOC, HAZ_MTL_CD=@HAZ_MTL_CD, HAZ_MATL_DSC=@HAZ_MATL_DSC WHERE GTN_DTL_ID=@GTN_DTL_ID"
    Private Const V_Equipment_InformationSelectQuery As String = "SELECT EQPMNT_INFRMTN_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,MNFCTR_DT,TR_WGHT_NC,GRSS_WGHT_NC,CPCTY_NC,LST_SRVYR_NM,LST_TST_DT,LST_TST_TYP_ID,LST_TST_TYP_CD,NXT_TST_DT,NXT_TST_TYP_ID,NXT_TST_TYP_CD,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,ACTV_BT FROM V_EQUIPMENT_INFORMATION WHERE EQPMNT_NO=@EQPMNT_NO"
    Private Const HandlingChargeInsertQuery As String = "INSERT INTO HANDLING_CHARGE(HNDLNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_TYP_ID,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,FR_DYS,NO_OF_DYS,HNDLNG_CST_NC,HNDLNG_TX_RT_NC,TTL_CSTS_NC,BLLNG_FLG,ACTV_BT,DPT_ID,IS_GT_OT_FLG,YRD_LCTN,BLLNG_TYP_CD,CSTMR_ID,HTNG_BT,GI_TRNSCTN_NO,AGNT_ID)VALUES(@HNDLNG_CHRG_ID,@EQPMNT_NO,@EQPMNT_CD_ID,@EQPMNT_TYP_ID,@CST_TYP,@RFRNC_EIR_NO_1,@RFRNC_EIR_NO_2,@FRM_BLLNG_DT,@TO_BLLNG_DT,@FR_DYS,@NO_OF_DYS,@HNDLNG_CST_NC,@HNDLNG_TX_RT_NC,@TTL_CSTS_NC,@BLLNG_FLG,@ACTV_BT,@DPT_ID,@IS_GT_OT_FLG,@YRD_LCTN,@BLLNG_TYP_CD,@CSTMR_ID,@HTNG_BT,@GI_TRNSCTN_NO,@AGNT_ID)"
    Private Const UpdateHandlingStorageQuery As String = "UPDATE HANDLING_CHARGE SET AGNT_ID=@AGNT_ID, FRM_BLLNG_DT=@FRM_BLLNG_DT, TO_BLLNG_DT=@TO_BLLNG_DT, YRD_LCTN=@YRD_LCTN, HTNG_BT=@HTNG_BT, RFRNC_EIR_NO_1=@RFRNC_EIR_NO_1,HNDLNG_CST_NC=@HNDLNG_CST_NC,TTL_CSTS_NC=@TTL_CSTS_NC WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CST_TYP = 'HNDIN'"
    Private Const StorageChargeInsertQuery As String = "INSERT INTO STORAGE_CHARGE(STRG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_TYP_ID,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,FR_DYS,NO_OF_DYS,STRG_CST_NC,STRG_TX_RT_NC,TTL_CSTS_NC,STRG_CNTN_FLG,BLLNG_FLG,IS_GT_OT_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,BLLNG_TYP_CD,CSTMR_ID,DPT_ID,HTNG_BT,GI_TRNSCTN_NO,AGNT_ID)VALUES(@STRG_CHRG_ID,@EQPMNT_NO,@EQPMNT_CD_ID,@EQPMNT_TYP_ID,@CST_TYP,@RFRNC_EIR_NO_1,@RFRNC_EIR_NO_2,@FRM_BLLNG_DT,@TO_BLLNG_DT,@FR_DYS,@NO_OF_DYS,@STRG_CST_NC,@STRG_TX_RT_NC,@TTL_CSTS_NC,@STRG_CNTN_FLG,@BLLNG_FLG,@IS_GT_OT_FLG,@ACTV_BT,@IS_LT_FLG,@BLLNG_TLL_DT,@YRD_LCTN,@BLLNG_TYP_CD,@CSTMR_ID,@DPT_ID,@HTNG_BT,@GI_TRNSCTN_NO,@AGNT_ID)"
    Private Const Activity_StatusInsertQuery As String = "INSERT INTO ACTIVITY_STATUS(ACTVTY_STTS_ID,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,GTN_DT,GTOT_DT,PRDCT_ID,CLNNG_DT,EQPMNT_STTS_ID,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,ACTV_BT,GI_RF_NO,DPT_ID,YRD_LCTN,INVC_GNRT_BT)VALUES(@ACTVTY_STTS_ID,@CSTMR_ID,@EQPMNT_NO,@EQPMNT_TYP_ID,@EQPMNT_CD_ID,@GTN_DT,@GTOT_DT,@PRDCT_ID,@CLNNG_DT,@EQPMNT_STTS_ID,@ACTVTY_NAM,@ACTVTY_DT,@RMRKS_VC,@GI_TRNSCTN_NO,@ACTV_BT,@GI_RF_NO,@DPT_ID,@YRD_LCTN,@INVC_GNRT_BT)"
    Private Const Pre_AdviceUpdateQuery As String = "UPDATE PRE_ADVICE SET GI_TRNSCTN_NO=@GI_TRNSCTN_NO WHERE GI_TRNSCTN_NO IS NULL AND EQPMNT_NO = @EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const V_GATEIN_DETAILSelectQueryBy As String = "SELECT GTN_DTL_ID,GTN_ID,GTN_CD,MNFCTR_DT,ACEP_CD,MTRL_ID,MTRL_CD,GRSS_WGHT_NC,TR_WGHT_NC,MSR_ID,MSR_CD,UNT_ID,UNT_CD,LST_OH_LOC,LST_OH_DT,TRCKR_CD,LOD_STTS_CD,CNTRY_ID,CNTRY_CD,LIC_STT,LIC_REG,LIC_EXPR,NT_1_VC,NT_2_VC,NT_3_VC,NT_4_VC,NT_5_VC,SL_PRTY_1_ID,SL_NMBR_1,SL_PRTY_1_CD,SL_PRTY_2_ID,SL_NMBR_2,SL_PRTY_2_CD,SL_PRTY_3_ID,SL_NMBR_3,SL_PRTY_3_CD,PRT_FNC_CD,PRT_NM,PRT_NO,PRT_LC_CD,VSSL_NM,VYG_NO,VSSL_CD,SHPPR_NAM,RL_ID_VC,RL_RMP_LOC,HAZ_MTL_CD,HAZ_MATL_DSC FROM V_GATEIN_DETAIL WHERE GTN_ID IN (SELECT GTN_ID FROM GATEIN WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GTN_ID=@GTN_ID)"
    Private Const GateinDetailCountQuery As String = "SELECT COUNT(GTN_DTL_ID) FROM GATEIN_DETAIL WHERE GTN_ID=@GTN_ID"
    Private Const updateStorageCharge As String = "UPDATE STORAGE_CHARGE SET AGNT_ID=@AGNT_ID, FRM_BLLNG_DT=@FRM_BLLNG_DT,YRD_LCTN=@YRD_LCTN, HTNG_BT=@HTNG_BT, RFRNC_EIR_NO_1=@RFRNC_EIR_NO_1 WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const TrackingUpdateQuery As String = "UPDATE TRACKING SET CSTMR_ID=@CSTMR_ID, ACTVTY_DT=@ACTVTY_DT,RFRNC_NO=@RFRNC_NO, ACTVTY_RMRKS=@ACTVTY_RMRKS,MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT,RNTL_CSTMR_ID=@RNTL_CSTMR_ID,EQPMNT_INFRMTN_RMRKS_VC=@EQPMNT_INFRMTN_RMRKS_VC WHERE DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND EQPMNT_NO=@EQPMNT_NO AND ACTVTY_NAM='Gate In'"
    Private Const Gatein_RetUpdateQuery As String = "UPDATE GATEIN_RET SET GI_COMPLETE=@GI_COMPLETE, GI_SENT_EIR=@GI_SENT_EIR, GI_SENT_DATE=@GI_SENT_DATE, GI_REC_EIR=@GI_REC_EIR, GI_REC_DATE=@GI_REC_DATE, GI_REC_ADDR=@GI_REC_ADDR, GI_REC_TYPE=@GI_REC_TYPE, GI_EXPORTED=@GI_EXPORTED, GI_EXPOR_DATE=@GI_EXPOR_DATE, GI_IMPORTED=@GI_IMPORTED, GI_IMPOR_DATE=@GI_IMPOR_DATE, GI_TRNSXN=@GI_TRNSXN, GI_ADVICE=@GI_ADVICE, GI_UNIT_NBR=@GI_UNIT_NBR, GI_EQUIP_TYPE=@GI_EQUIP_TYPE, GI_EQUIP_DESC=@GI_EQUIP_DESC, GI_EQUIP_CODE=@GI_EQUIP_CODE, GI_CONDITION=@GI_CONDITION, GI_COMP_ID_A=@GI_COMP_ID_A, GI_COMP_ID_N=@GI_COMP_ID_N, GI_COMP_ID_C=@GI_COMP_ID_C, GI_COMP_TYPE=@GI_COMP_TYPE, GI_COMP_DESC=@GI_COMP_DESC, GI_COMP_CODE=@GI_COMP_CODE, GI_EIR_DATE=@GI_EIR_DATE, GI_EIR_TIME=@GI_EIR_TIME, GI_REFERENCE=@GI_REFERENCE, GI_MANU_DATE=@GI_MANU_DATE, GI_MATERIAL=@GI_MATERIAL, GI_WEIGHT=@GI_WEIGHT, GI_MEASURE=@GI_MEASURE, GI_UNITS=@GI_UNITS, GI_CSC_REEXAM=@GI_CSC_REEXAM, GI_COUNTRY=@GI_COUNTRY, GI_LIC_STATE=@GI_LIC_STATE, GI_LIC_REG=@GI_LIC_REG, GI_LIC_EXPIRE=@GI_LIC_EXPIRE, GI_LSR_OWNER=@GI_LSR_OWNER, GI_SEND_EDI_1=@GI_SEND_EDI_1, GI_SSL_LSE=@GI_SSL_LSE, GI_SEND_EDI_2=@GI_SEND_EDI_2, GI_HAULIER=@GI_HAULIER, GI_SEND_EDI_3=@GI_SEND_EDI_3, GI_DPT_TRM=@GI_DPT_TRM, GI_SEND_EDI_4=@GI_SEND_EDI_4, GI_OTHERS_1=@GI_OTHERS_1, GI_OTHERS_2=@GI_OTHERS_2, GI_OTHERS_3=@GI_OTHERS_3, GI_OTHERS_4=@GI_OTHERS_4, GI_NOTE_1=@GI_NOTE_1, GI_NOTE_2=@GI_NOTE_2, GI_LOAD=@GI_LOAD, GI_FHWA=@GI_FHWA, GI_LAST_OH_LOC=@GI_LAST_OH_LOC, GI_LAST_OH_DATE=@GI_LAST_OH_DATE, GI_SENDER=@GI_SENDER, GI_ATTENTION=@GI_ATTENTION, GI_REVISION=@GI_REVISION, GI_SEND_EDI_5=@GI_SEND_EDI_5, GI_SEND_EDI_6=@GI_SEND_EDI_6, GI_SEND_EDI_7=@GI_SEND_EDI_7, GI_SEND_EDI_8=@GI_SEND_EDI_8, GI_SEAL_PARTY_1=@GI_SEAL_PARTY_1, GI_SEAL_NUMBER_1=@GI_SEAL_NUMBER_1, GI_SEAL_PARTY_2=@GI_SEAL_PARTY_2, GI_SEAL_NUMBER_2=@GI_SEAL_NUMBER_2, GI_SEAL_PARTY_3=@GI_SEAL_PARTY_3, GI_SEAL_NUMBER_3=@GI_SEAL_NUMBER_3, GI_SEAL_PARTY_4=@GI_SEAL_PARTY_4, GI_SEAL_NUMBER_4=@GI_SEAL_NUMBER_4, GI_PORT_FUNC_CODE=@GI_PORT_FUNC_CODE, GI_PORT_NAME=@GI_PORT_NAME, GI_VESSEL_NAME=@GI_VESSEL_NAME, GI_VOYAGE_NUM=@GI_VOYAGE_NUM, GI_HAZ_MAT_CODE=@GI_HAZ_MAT_CODE, GI_HAZ_MAT_DESC=@GI_HAZ_MAT_DESC, GI_NOTE_3=@GI_NOTE_3, GI_NOTE_4=@GI_NOTE_4, GI_NOTE_5=@GI_NOTE_5, GI_COMP_ID_A_2=@GI_COMP_ID_A_2, GI_COMP_ID_N_2=@GI_COMP_ID_N_2, GI_COMP_ID_C_2=@GI_COMP_ID_C_2, GI_COMP_TYPE_2=@GI_COMP_TYPE_2, GI_COMP_CODE_2=@GI_COMP_CODE_2, GI_COMP_DESC_2=@GI_COMP_DESC_2, GI_SHIPPER=@GI_SHIPPER, GI_DRAY_ORDER=@GI_DRAY_ORDER, GI_RAIL_ID=@GI_RAIL_ID, GI_RAIL_RAMP=@GI_RAIL_RAMP, GI_VESSEL_CODE=@GI_VESSEL_CODE, GI_WGHT_CERT_1=@GI_WGHT_CERT_1, GI_WGHT_CERT_2=@GI_WGHT_CERT_2, GI_WGHT_CERT_3=@GI_WGHT_CERT_3, GI_SEA_RAIL=@GI_SEA_RAIL, GI_LOC_IDENT=@GI_LOC_IDENT, GI_PORT_LOC_QUAL=@GI_PORT_LOC_QUAL, GI_STATUS=@GI_STATUS, GI_PICK_DATE=@GI_PICK_DATE, GI_ESTSTATUS=@GI_ESTSTATUS, GI_ERRSTATUS=@GI_ERRSTATUS, GI_USERNAME=@GI_USERNAME, GI_LIVE_STATUS=@GI_LIVE_STATUS, GI_ISACTIVE=@GI_ISACTIVE, GI_YARD_LOC=@GI_YARD_LOC, GI_MODE_PAYMENT=@GI_MODE_PAYMENT, GI_BILLING_TYPE=@GI_BILLING_TYPE, GI_RESERVE_BKG=@GI_RESERVE_BKG, GI_RCESTSTATUS=@GI_RCESTSTATUS, OP_SNO=@OP_SNO, OP_STATUS=@OP_STATUS WHERE GI_TRANSMISSION_NO=@GI_TRANSMISSION_NO"
    Private Const Equipment_InfoUpdateQuery As String = "UPDATE EQUIPMENT_INFORMATION SET EQPMNT_TYP_ID=@EQPMNT_TYP_ID,RMRKS_VC=@RMRKS_VC WHERE EQPMNT_NO = @EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const Equipment_InfoCountQuery As String = "SELECT COUNT(EQPMNT_NO) FROM EQUIPMENT_INFORMATION WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO"
    Private Const Tracking_Pre_AdviceUpdateQuery As String = "UPDATE TRACKING SET GI_TRNSCTN_NO=@GI_TRNSCTN_NO WHERE ACTVTY_NAM=@ACTVTY_NAM AND GI_TRNSCTN_NO IS NULL AND EQPMNT_NO = @EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const CutomerChargeDetailSelectQuery As String = "SELECT CSTMR_CHRG_DTL_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,ACTV_BT,DPT_ID FROM V_CUSTOMER_CHARGE_DETAIL WHERE DPT_ID=@DPT_ID AND EQPMNT_CD_ID=@EQPMNT_CD_ID AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID AND CSTMR_ID=@CSTMR_ID"
    Private Const Heating_ChargeDeleteQuery As String = "DELETE FROM HEATING_CHARGE WHERE BLLNG_FLG <> 'B' AND DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Activity_StatusUpdateQuery As String = "UPDATE ACTIVITY_STATUS SET EQPMNT_STTS_ID=@EQPMNT_STTS_ID,ACTVTY_DT=@ACTVTY_DT,GTN_DT=@GTN_DT,GI_RF_NO=@GI_RF_NO, YRD_LCTN=@YRD_LCTN,PRDCT_ID=@PRDCT_ID WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const CountryDetailsSelectQuery As String = "SELECT CNTRY_ID,CNTRY_CD FROM COUNTRY WHERE DPT_ID=@DPT_ID"
    Private Const MaterialDetailsSelectQuery As String = "SELECT MTRL_ID,MTRL_CD FROM MATERIAL WHERE DPT_ID=@DPT_ID"
    Private Const MeasureDetailsSelectQuery As String = "SELECT MSR_ID,MSR_CD FROM MEASURE WHERE DPT_ID=@DPT_ID"
    Private Const PartyDetailsSelectQuery As String = "SELECT INVCNG_PRTY_ID,INVCNG_PRTY_CD FROM INVOICING_PARTY WHERE DPT_ID=@DPT_ID"
    Private Const UnitDetailsSelectQuery As String = "SELECT UNT_ID,UNT_CD FROM UNIT WHERE DPT_ID=@DPT_ID"
    Private Const RentalSelectQueryByDepotId As String = "SELECT EQPMNT_NO,CSTMR_ID,CSTMR_CD,CSTMR_NAM,RNTL_RFRNC_NO,GI_TRNSCTN_NO,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT FROM V_RENTAL_ENTRY WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND OFF_HR_DT IS NULL"
    Private Const RentalEntrySelectQueryByDepotId As String = "SELECT EQPMNT_NO,CSTMR_ID,CSTMR_CD,CSTMR_NAM,RNTL_RFRNC_NO,GI_TRNSCTN_NO,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT,OTHR_CHRG_NC FROM V_RENTAL_ENTRY WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND OFF_HR_DT IS NULL AND CSTMR_ID=@CSTMR_ID"
    Private Const RentalEntryUpdateQuery As String = "UPDATE RENTAL_ENTRY SET OFF_HR_DT=@OFF_HR_DT,GTN_BT=@GTN_BT WHERE EQPMNT_NO=@EQPMNT_NO AND RNTL_RFRNC_NO=@RNTL_RFRNC_NO AND CSTMR_ID=@CSTMR_ID "
    Private Const RentalChargeUpdateQuery As String = "UPDATE RENTAL_CHARGE SET TO_BLLNG_DT=@TO_BLLNG_DT,YRD_LCTN=@YRD_LCTN, HNDLNG_IN_NC=@HNDLNG_IN_NC, OFF_HR_SRVY_NC=@OFF_HR_SRVY_NC,OTHR_CHRG_NC=@OTHR_CHRG_NC WHERE EQPMNT_NO=@EQPMNT_NO AND RNTL_RFRNC_NO=@RNTL_RFRNC_NO AND DPT_ID=@DPT_ID"
    Private Const RentalEntryValidateQuery As String = "SELECT RNTL_ENTRY_ID,EQPMNT_NO,CSTMR_ID,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT,RMRKS_VC,RNTL_RFRNC_NO,GI_TRNSCTN_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,GTN_BT,OTHR_CHRG_NC,(CASE WHEN(SELECT COUNT (GTOT_ID) FROM GATEOUT WHERE RNTL_BT=1 AND EQPMNT_NO=R.EQPMNT_NO AND RNTL_RFRNC_NO=R.RNTL_RFRNC_NO)>0 THEN 1 ELSE 0 END)IS_GTOT_BT FROM RENTAL_ENTRY R WHERE EQPMNT_NO =@EQPMNT_NO AND DPT_ID=@DPT_ID AND OFF_HR_DT IS NULL"
    Private Const RentalCustomerSelectQuery As String = "SELECT CSTMR_RNTL_ID,CSTMR_ID,CNTRCT_RFRNC_NO,CNTRCT_STRT_DT,CNTRCT_END_DT,MN_TNR_DY,RNTL_PR_DY,HNDLNG_OT,HNDLNG_IN,ON_HR_SRVY,OFF_HR_SRVY,RMRKS_VC FROM CUSTOMER_RENTAL WHERE CSTMR_ID=@CSTMR_ID AND CNTRCT_RFRNC_NO  IN (SELECT TOP 1 CNTRCT_RFRNC_NO FROM RENTAL_ENTRY WHERE EQPMNT_NO=@EQPMNT_NO AND CSTMR_ID=@CSTMR_ID ORDER BY RNTL_ENTRY_ID DESC)"
    Private Const RentalChargeDetailUpdateQuery As String = "UPDATE RENTAL_CHARGE SET TO_BLLNG_DT=@TO_BLLNG_DT,YRD_LCTN=@YRD_LCTN, HNDLNG_IN_NC=@HNDLNG_IN_NC, OFF_HR_SRVY_NC=@OFF_HR_SRVY_NC WHERE EQPMNT_NO=@EQPMNT_NO AND RNTL_RFRNC_NO=@RNTL_RFRNC_NO AND DPT_ID=@DPT_ID"
    Private Const SupplierEquipmentDetailsSelectQuery As String = "SELECT SPPLR_EQPMNT_DTL_ID,SPPLR_ID,SPPLR_CNTRCT_DTL_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD FROM V_SUPPLIER_EQUIPMENT_DETAIL WHERE EQPMNT_NO = @EQPMNT_NO"
    Private Const GateoutRentalDetailsByEqpmntNo As String = "SELECT GTOT_ID,GTOT_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC,YRD_LCTN,GTOT_DT,GTOT_TM,EIR_NO,VHCL_NO,TRNSPRTR_CD,GI_TRNSCTN_NO,RNTL_CSTMR_ID,RNTL_CSTMR_CD,RNTL_CSTMR_NAM,RNTL_RFRNC_NO,RNTL_BT,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,RMRKS_VC FROM V_GATEOUT WHERE EQPMNT_NO IN (SELECT EQPMNT_NO FROM RENTAL_ENTRY WHERE EQPMNT_NO =@EQPMNT_NO AND DPT_ID=@DPT_ID) AND DPT_ID=@DPT_ID"
    Private Const Equipment_Information_DetailInsertQuery As String = "INSERT INTO EQUIPMENT_INFORMATION_DETAIL (EQPMNT_INFRMTN_DTL_ID,EQPMNT_INFRMTN_ID,ATTCHMNT_PTH,ACTL_FL_NM) VALUES(@EQPMNT_INFRMTN_DTL_ID,@EQPMNT_INFRMTN_ID,@ATTCHMNT_PTH,@ACTL_FL_NM)"
    Private Const Equipment_Information_DetailDeleteQuery As String = "DELETE FROM EQUIPMENT_INFORMATION_DETAIL WHERE EQPMNT_INFRMTN_ID=(SELECT EQPMNT_INFRMTN_ID FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID)"
    Private Const TrackingEquipmentInfoRemarksSelectQuery As String = "SELECT TRCKNG_ID,CSTMR_ID,EQPMNT_NO,ACTVTY_NAM,EQPMNT_STTS_ID,ACTVTY_NO,ACTVTY_DT,ACTVTY_RMRKS,YRD_LCTN,GI_TRNSCTN_NO,INVCNG_PRTY_ID,RFRNC_NO,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,CNCLD_BY,CNCLD_DT,ADT_RMRKS,DPT_ID,RNTL_CSTMR_ID,RNTL_RFRNC_NO,EQPMNT_INFRMTN_RMRKS_VC FROM TRACKING  WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND ACTVTY_NAM=@ACTVTY_NAM AND GI_TRNSCTN_NO <> @GI_TRNSCTN_NO ORDER BY TRCKNG_ID DESC"
    Private Const GateinLockSelectQuery As String = "SELECT GTN_ID,GTN_CD,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,EQPMNT_STTS_ID,YRD_LCTN,GTN_DT,GTN_TM,PRDCT_ID,EIR_NO,VHCL_NO,TRNSPRTR_CD,HTNG_BT,RMRKS_VC,GI_TRNSCTN_NO,GTOT_BT,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT FROM GATEIN WHERE EQPMNT_NO=@EQPMNT_NO AND CSTMR_ID = @CSTMR_ID AND DPT_ID=@DPT_ID AND GTOT_BT=0 ORDER BY GTN_ID DESC"
    Private Const ValidateStatusOfEquipmentQuery As String = "SELECT COUNT(*) FROM ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID <> @DPT_ID AND ACTV_BT=1"
    Private Const ValidateEquipmentNoInPreAdvice As String = "SELECT COUNT(*) FROM PRE_ADVICE WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO IS null AND DPT_ID <> @DPT_ID "

    Private Const GetProductUNNOQuery As String = "SELECT UN_NO,PRDCT_CD ,PRDCT_ID ,PRDCT_DSCRPTN_VC  FROM PRODUCT WHERE PRDCT_ID=@PRDCT_ID"
    Private Const GetCleaningDetail As String = "SELECT CHMCL_NM ,LST_CLNNG_DT  FROM CLEANING WHERE EQPMNT_NO =@EQPMNT_NO"
    Private ds As GateinDataSet
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
    'Private Const getEquipmentCodeDescriptionQuery As String = "SELECT EQPMNT_CD_DSCRPTN_VC FROM EQUIPMENT_CODE WHERE EQPMNT_CD_CD=@EQPMNT_CD_CD"
    Private Const getEquipmentCodeDescriptionQuery As String = "SELECT EQPMNT_TYP_DSCRPTN_VC FROM EQUIPMENT_TYPE WHERE EQPMNT_TYP_ID=@EQPMNT_TYP_ID"
    'For Attchemnt : getting PreAdvice No
    Private Const getPreAdviceNoSelectQuery As String = "SELECT TOP 1 PR_ADVC_ID AS GTN_ID,GI_TRNSCTN_NO,COUNT_ATTACH FROM V_PRE_ADVICE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO IS NULL"
    'AND GI_TRNSCTN_NO IS NULL"
    Private Const GetAttachmentByGateIN As String = "SELECT COUNT(RPR_ESTMT_ID)COUNT_ATTACH FROM ATTACHMENT WHERE RPR_ESTMT_ID=@RPR_ESTMT_ID"
    Private Const AttachmentSelectQuery As String = "SELECT ATTCHMNT_ID,RPR_ESTMT_ID,ACTVTY_NAM,RPR_ESTMT_NO,GI_TRNSCTN_NO,ATTCHMNT_PTH,ACTL_FL_NM,MDFD_BY,MDFD_DT,DPT_ID,'False' AS ERR_FLG FROM ATTACHMENT WHERE DPT_ID=@DPT_ID AND RPR_ESTMT_ID=@RPR_ESTMT_ID"
    Private Const AttachmentSelectWithActivityQuery As String = "SELECT ATTCHMNT_ID,RPR_ESTMT_ID,ACTVTY_NAM,RPR_ESTMT_NO,GI_TRNSCTN_NO,ATTCHMNT_PTH,ACTL_FL_NM,MDFD_BY,MDFD_DT,DPT_ID,'False' AS ERR_FLG FROM ATTACHMENT WHERE DPT_ID=@DPT_ID AND RPR_ESTMT_ID=@RPR_ESTMT_ID AND ACTVTY_NAM=@ACTVTY_NAM"
    Private Const AttachmentSelectGateInQuery As String = "SELECT ATTCHMNT_ID,RPR_ESTMT_ID,ACTVTY_NAM,RPR_ESTMT_NO,GI_TRNSCTN_NO,ATTCHMNT_PTH,ACTL_FL_NM,MDFD_BY,MDFD_DT,DPT_ID,'False' AS ERR_FLG FROM ATTACHMENT WHERE DPT_ID=@DPT_ID AND RPR_ESTMT_ID=@RPR_ESTMT_ID AND ACTVTY_NAM=@ACTVTY_NAM"

    Private Const GetEquipmentInformationQuery As String = "SELECT MNFCTR_DT,CSC_VLDTY FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO=@EQPMNT_NO"

    'For GWS
    Private Const GetAgentHanldingInCharge_SelectQry As String = "SELECT AGNT_CHRG_DTL_ID,AGNT_ID,AGNT_CD,AGNT_NAM,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,ACTV_BT,DPT_ID FROM V_AGENT_CHARGE_DETAIL WHERE DPT_ID=@DPT_ID AND EQPMNT_CD_ID=@EQPMNT_CD_ID AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID AND AGNT_ID=@AGNT_ID"

#End Region

#Region "Constructor.."

    Sub New()
        ds = New GateinDataSet
    End Sub

#End Region

#Region "GetGateInEquipmentByID() TABLE NAME:GATEIN"

    Public Function GetGateInEquipmentByID(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As GateinDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(GateinData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(GateinData.DPT_ID, bv_i32DepotID)
            Console.WriteLine("GateinSelectQueryByDepotId")

            objData = New DataObjects(GateinSelectQueryByDepotId, hshParameters)
            objData.Fill(CType(ds, DataSet), GateinData._GATEIN)
            'Return objData.ExecuteScalar()
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateGatein() TABLE NAME:Gatein"

    Public Function CreateGatein(ByVal bv_i64CustomerID As Int64, _
        ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64EquipmentTypeId As Int64, _
        ByVal bv_i64EquipmentCodeId As Int64, _
        ByVal bv_i64EquipmentStatusId As Int64, _
        ByVal bv_strYardLocation As String, _
        ByVal bv_datGateInDate As DateTime, _
        ByVal bv_strGateInTime As String, _
        ByVal bv_i64ProductId As Int64, _
        ByVal bv_strEIR_NO As String, _
        ByVal bv_strVechicleNo As String, _
        ByVal bv_strTransporterCode As String, _
        ByVal bv_blnHeatingBit As Boolean, _
        ByVal bv_strRemarks As String, _
        ByRef bv_strGateInTransactionNo As String, _
        ByVal bv_rentalReferenceNo As String, _
        ByVal bv_GateOutBit As Boolean, _
        ByVal bv_RentalBit As Boolean, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal intBillId As String, _
        ByVal strRedelAuth As String, _
        ByVal strConsignee As String, _
        ByVal intGradeID As String, _
        ByVal intAgentID As String, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateinData._GATEIN).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GateinData._GATEIN, br_objTrans)
                Dim intIndexPatern As Long = CommonUIs.GetIdentityValue("GATEIN_EIR", br_objTrans)
                .Item(GateinData.GTN_ID) = intMax
                'From Index Pattern
                ' bv_strGateInTransactionNo = CommonUIs.GetIdentityCode(GateinData._GATEIN, intMax, bv_datGateInDate, br_objTrans)
                ' bv_strGateInTransactionNo = IndexPatterns.GetIdentityCode(GateinData._GATEIN, intMax, bv_datGateInDate, bv_i32DepotId, br_objTrans)
                .Item(GateinData.GTN_CD) = bv_strGateInTransactionNo
                .Item(GateinData.CSTMR_ID) = bv_i64CustomerID
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateinData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                .Item(GateinData.EQPMNT_CD_ID) = bv_i64EquipmentCodeId
                .Item(GateinData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                If bv_strYardLocation <> Nothing Then
                    .Item(GateinData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(GateinData.YRD_LCTN) = DBNull.Value
                End If
                .Item(GateinData.GTN_DT) = bv_datGateInDate
                If bv_strGateInTime <> Nothing Then
                    .Item(GateinData.GTN_TM) = bv_strGateInTime
                Else
                    .Item(GateinData.GTN_TM) = DBNull.Value
                End If
                .Item(GateinData.PRDCT_ID) = bv_i64ProductId
                If bv_strEIR_NO <> Nothing Then
                    .Item(GateinData.EIR_NO) = bv_strEIR_NO
                Else
                    .Item(GateinData.EIR_NO) = DBNull.Value
                End If
                If bv_strVechicleNo <> Nothing Then
                    .Item(GateinData.VHCL_NO) = bv_strVechicleNo
                Else
                    .Item(GateinData.VHCL_NO) = DBNull.Value
                End If
                If bv_strTransporterCode <> Nothing Then
                    .Item(GateinData.TRNSPRTR_CD) = bv_strTransporterCode
                Else
                    .Item(GateinData.TRNSPRTR_CD) = DBNull.Value
                End If
                .Item(GateinData.HTNG_BT) = bv_blnHeatingBit
                If bv_strRemarks <> Nothing Then
                    .Item(GateinData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(GateinData.RMRKS_VC) = DBNull.Value
                End If
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                If bv_rentalReferenceNo <> Nothing Then
                    .Item(GateinData.RNTL_RFRNC_NO) = bv_rentalReferenceNo
                Else
                    .Item(GateinData.RNTL_RFRNC_NO) = DBNull.Value
                End If
                If intBillId <> Nothing Then
                    .Item(GateinData.BLL_ID) = intBillId
                Else
                    .Item(GateinData.BLL_ID) = DBNull.Value
                End If
                If strRedelAuth <> Nothing Then
                    .Item(GateinData.RDL_ATH) = strRedelAuth
                Else
                    .Item(GateinData.RDL_ATH) = DBNull.Value
                End If
                If strConsignee <> Nothing Then
                    .Item(GateinData.CNSGNE) = strConsignee
                Else
                    .Item(GateinData.CNSGNE) = DBNull.Value
                End If
                If intGradeID <> Nothing Then
                    .Item(GateinData.GRD_ID) = intGradeID
                Else
                    .Item(GateinData.GRD_ID) = DBNull.Value
                End If
                If intAgentID <> Nothing Then
                    .Item(GateinData.AGNT_ID) = intAgentID
                Else
                    .Item(GateinData.AGNT_ID) = DBNull.Value
                End If
                .Item(GateinData.GTOT_BT) = bv_GateOutBit
                .Item(GateinData.RNTL_BT) = bv_RentalBit
                .Item(GateinData.DPT_ID) = bv_i32DepotId
                .Item(GateinData.CRTD_BY) = bv_strCreatedBy
                .Item(GateinData.CRTD_DT) = bv_datCreatedDate
                .Item(GateinData.MDFD_BY) = bv_strModifiedBy
                .Item(GateinData.MDFD_DT) = bv_datModifiedDate
            End With
            objData.InsertRow(dr, GateinInsertQuery, br_objTrans)
            dr = Nothing
            CreateGatein = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdatePre_Advice"
    Public Function UpdatePre_Advice(ByVal bv_strGateInTransactionNo As String, _
                                     ByVal bv_strEquipmentNo As String, _
                                     ByVal bv_intDepotID As Integer, _
                                     ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._PRE_ADVICE).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateinData.DPT_ID) = bv_intDepotID
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
            End With

            UpdatePre_Advice = objData.UpdateRow(dr, Pre_AdviceUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "CreateGateinDetail() TABLE NAME:Gatein_Detail"

    Public Function CreateGateinDetail(ByVal bv_i64GateInID As Int64, _
        ByVal bv_datManufactureDate As DateTime, _
        ByVal bv_strAcceptanceCode As String, _
        ByVal bv_i64MaterialID As Int64, _
        ByVal bv_dblGrossWeightNc As Double, _
        ByVal bv_dblTareWeightNc As Double, _
        ByVal bv_i64MeasureID As Int64, _
        ByVal bv_i64UnitID As Int64, _
        ByVal bv_strOnHireLocation As String, _
        ByVal bv_datOnHireDate As DateTime, _
        ByVal bv_strTrackerCode As String, _
        ByVal bv_strLoadStatusCode As String, _
        ByVal bv_i64CountryID As Int64, _
        ByVal bv_strLicenseState As String, _
        ByVal bv_strLicenseNumber As String, _
        ByVal bv_datLicenseExpiryDate As DateTime, _
        ByVal bv_strNotes1 As String, _
        ByVal bv_strNotes2 As String, _
        ByVal bv_strNotes3 As String, _
        ByVal bv_strNotes4 As String, _
        ByVal bv_strNotes5 As String, _
        ByVal bv_i64Party1 As Int64, _
        ByVal bv_strPartyNumber1 As String, _
        ByVal bv_i64Party2 As Int64, _
        ByVal bv_strPartyNumber2 As String, _
        ByVal bv_i64Party3 As Int64, _
        ByVal bv_strPartyNumber3 As String, _
        ByVal bv_strPortCode As String, _
        ByVal bv_strPortName As String, _
        ByVal bv_strPortNumber As String, _
        ByVal bv_strPortLocationCode As String, _
        ByVal bv_strVesselName As String, _
        ByVal bv_strVoyageNo As String, _
        ByVal bv_strVesselCode As String, _
        ByVal bv_strShipperName As String, _
        ByVal bv_strRailId As String, _
        ByVal bv_strRailRampLocation As String, _
        ByVal bv_strHazardMaterialCode As String, _
        ByVal bv_strHazardMaterialDesc As String, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateinData._GATEIN_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GateinData._GATEIN_DETAIL, br_objTrans)
                .Item(GateinData.GTN_DTL_ID) = intMax
                .Item(GateinData.GTN_ID) = bv_i64GateInID
                If bv_datManufactureDate <> Nothing Then
                    .Item(GateinData.MNFCTR_DT) = bv_datManufactureDate
                Else
                    .Item(GateinData.MNFCTR_DT) = DBNull.Value
                End If
                If bv_strAcceptanceCode <> Nothing Then
                    .Item(GateinData.ACEP_CD) = bv_strAcceptanceCode
                Else
                    .Item(GateinData.ACEP_CD) = DBNull.Value
                End If
                If bv_i64MaterialID <> 0 Then
                    .Item(GateinData.MTRL_ID) = bv_i64MaterialID
                Else
                    .Item(GateinData.MTRL_ID) = DBNull.Value
                End If
                If bv_dblGrossWeightNc <> 0 Then
                    .Item(GateinData.GRSS_WGHT_NC) = bv_dblGrossWeightNc
                Else
                    .Item(GateinData.GRSS_WGHT_NC) = DBNull.Value
                End If
                If bv_dblTareWeightNc <> 0 Then
                    .Item(GateinData.TR_WGHT_NC) = bv_dblTareWeightNc
                Else
                    .Item(GateinData.TR_WGHT_NC) = DBNull.Value
                End If
                If bv_i64MeasureID <> 0 Then
                    .Item(GateinData.MSR_ID) = bv_i64MeasureID
                Else
                    .Item(GateinData.MSR_ID) = DBNull.Value
                End If
                If bv_i64UnitID <> 0 Then
                    .Item(GateinData.UNT_ID) = bv_i64UnitID
                Else
                    .Item(GateinData.UNT_ID) = DBNull.Value
                End If
                If bv_strOnHireLocation <> Nothing Then
                    .Item(GateinData.LST_OH_LOC) = bv_strOnHireLocation
                Else
                    .Item(GateinData.LST_OH_LOC) = DBNull.Value
                End If
                If bv_datOnHireDate <> Nothing Then
                    .Item(GateinData.LST_OH_DT) = bv_datOnHireDate
                Else
                    .Item(GateinData.LST_OH_DT) = DBNull.Value
                End If
                If bv_strTrackerCode <> Nothing Then
                    .Item(GateinData.TRCKR_CD) = bv_strTrackerCode
                Else
                    .Item(GateinData.TRCKR_CD) = DBNull.Value
                End If
                If bv_strLoadStatusCode <> Nothing Then
                    .Item(GateinData.LOD_STTS_CD) = bv_strLoadStatusCode
                Else
                    .Item(GateinData.LOD_STTS_CD) = DBNull.Value
                End If
                If bv_i64CountryID <> 0 Then
                    .Item(GateinData.CNTRY_ID) = bv_i64CountryID
                Else
                    .Item(GateinData.CNTRY_ID) = DBNull.Value
                End If
                If bv_strLicenseState <> Nothing Then
                    .Item(GateinData.LIC_STT) = bv_strLicenseState
                Else
                    .Item(GateinData.LIC_STT) = DBNull.Value
                End If
                If bv_strLicenseNumber <> Nothing Then
                    .Item(GateinData.LIC_REG) = bv_strLicenseNumber
                Else
                    .Item(GateinData.LIC_REG) = DBNull.Value
                End If
                If bv_datLicenseExpiryDate <> Nothing Then
                    .Item(GateinData.LIC_EXPR) = bv_datLicenseExpiryDate
                Else
                    .Item(GateinData.LIC_EXPR) = DBNull.Value
                End If
                If bv_strNotes1 <> Nothing Then
                    .Item(GateinData.NT_1_VC) = bv_strNotes1
                Else
                    .Item(GateinData.NT_1_VC) = DBNull.Value
                End If
                If bv_strNotes2 <> Nothing Then
                    .Item(GateinData.NT_2_VC) = bv_strNotes2
                Else
                    .Item(GateinData.NT_2_VC) = DBNull.Value
                End If
                If bv_strNotes3 <> Nothing Then
                    .Item(GateinData.NT_3_VC) = bv_strNotes3
                Else
                    .Item(GateinData.NT_3_VC) = DBNull.Value
                End If
                If bv_strNotes4 <> Nothing Then
                    .Item(GateinData.NT_4_VC) = bv_strNotes4
                Else
                    .Item(GateinData.NT_4_VC) = DBNull.Value
                End If
                If bv_strNotes5 <> Nothing Then
                    .Item(GateinData.NT_5_VC) = bv_strNotes5
                Else
                    .Item(GateinData.NT_5_VC) = DBNull.Value
                End If
                If bv_i64Party1 <> 0 Then
                    .Item(GateinData.SL_PRTY_1_ID) = bv_i64Party1
                Else
                    .Item(GateinData.SL_PRTY_1_ID) = DBNull.Value
                End If
                If bv_strPartyNumber1 <> Nothing Then
                    .Item(GateinData.SL_NMBR_1) = bv_strPartyNumber1
                Else
                    .Item(GateinData.SL_NMBR_1) = DBNull.Value
                End If
                If bv_i64Party2 <> 0 Then
                    .Item(GateinData.SL_PRTY_2_ID) = bv_i64Party2
                Else
                    .Item(GateinData.SL_PRTY_2_ID) = DBNull.Value
                End If
                If bv_strPartyNumber2 <> Nothing Then
                    .Item(GateinData.SL_NMBR_2) = bv_strPartyNumber2
                Else
                    .Item(GateinData.SL_NMBR_2) = DBNull.Value
                End If
                If bv_i64Party3 <> 0 Then
                    .Item(GateinData.SL_PRTY_3_ID) = bv_i64Party3
                Else
                    .Item(GateinData.SL_PRTY_3_ID) = DBNull.Value
                End If
                If bv_strPartyNumber3 <> Nothing Then
                    .Item(GateinData.SL_NMBR_3) = bv_strPartyNumber3
                Else
                    .Item(GateinData.SL_NMBR_3) = DBNull.Value
                End If
                If bv_strPortCode <> Nothing Then
                    .Item(GateinData.PRT_FNC_CD) = bv_strPortCode
                Else
                    .Item(GateinData.PRT_FNC_CD) = DBNull.Value
                End If
                If bv_strPortName <> Nothing Then
                    .Item(GateinData.PRT_NM) = bv_strPortName
                Else
                    .Item(GateinData.PRT_NM) = DBNull.Value
                End If
                If bv_strPortNumber <> Nothing Then
                    .Item(GateinData.PRT_NO) = bv_strPortNumber
                Else
                    .Item(GateinData.PRT_NO) = DBNull.Value
                End If
                If bv_strPortLocationCode <> Nothing Then
                    .Item(GateinData.PRT_LC_CD) = bv_strPortLocationCode
                Else
                    .Item(GateinData.PRT_LC_CD) = DBNull.Value
                End If
                If bv_strVesselName <> Nothing Then
                    .Item(GateinData.VSSL_NM) = bv_strVesselName
                Else
                    .Item(GateinData.VSSL_NM) = DBNull.Value
                End If
                If bv_strVoyageNo <> Nothing Then
                    .Item(GateinData.VYG_NO) = bv_strVoyageNo
                Else
                    .Item(GateinData.VYG_NO) = DBNull.Value
                End If
                If bv_strVesselCode <> Nothing Then
                    .Item(GateinData.VSSL_CD) = bv_strVesselCode
                Else
                    .Item(GateinData.VSSL_CD) = DBNull.Value
                End If
                If bv_strShipperName <> Nothing Then
                    .Item(GateinData.SHPPR_NAM) = bv_strShipperName
                Else
                    .Item(GateinData.SHPPR_NAM) = DBNull.Value
                End If
                If bv_strRailId <> Nothing Then
                    .Item(GateinData.RL_ID_VC) = bv_strRailId
                Else
                    .Item(GateinData.RL_ID_VC) = DBNull.Value
                End If
                If bv_strRailRampLocation <> Nothing Then
                    .Item(GateinData.RL_RMP_LOC) = bv_strRailRampLocation
                Else
                    .Item(GateinData.RL_RMP_LOC) = DBNull.Value
                End If
                If bv_strHazardMaterialCode <> Nothing Then
                    .Item(GateinData.HAZ_MTL_CD) = bv_strHazardMaterialCode
                Else
                    .Item(GateinData.HAZ_MTL_CD) = DBNull.Value
                End If
                If bv_strHazardMaterialDesc <> Nothing Then
                    .Item(GateinData.HAZ_MATL_DSC) = bv_strHazardMaterialDesc
                Else
                    .Item(GateinData.HAZ_MATL_DSC) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Gatein_DetailInsertQuery, br_objTrans)
            dr = Nothing
            CreateGateinDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateGateinDetail() TABLE NAME:Gatein_Detail"

    Public Function UpdateGateinDetail(ByVal bv_i64GateInDetailID As Int64, _
        ByVal bv_i64GateInID As Int64, _
        ByVal bv_datManufactureDate As DateTime, _
        ByVal bv_strAcceptanceCode As String, _
        ByVal bv_i64MaterialID As Int64, _
        ByVal bv_dblGrossWeightNc As Double, _
        ByVal bv_dblTareWeightNc As Double, _
        ByVal bv_i64MeasureID As Int64, _
        ByVal bv_i64UnitID As Int64, _
        ByVal bv_strOnHireLocation As String, _
        ByVal bv_datOnHireDate As DateTime, _
        ByVal bv_strTrackerCode As String, _
        ByVal bv_strLoadStatusCode As String, _
        ByVal bv_i64CountryID As Int64, _
        ByVal bv_strLicenseState As String, _
        ByVal bv_strLicenseNumber As String, _
        ByVal bv_datLicenseExpiryDate As DateTime, _
        ByVal bv_strNotes1 As String, _
        ByVal bv_strNotes2 As String, _
        ByVal bv_strNotes3 As String, _
        ByVal bv_strNotes4 As String, _
        ByVal bv_strNotes5 As String, _
        ByVal bv_i64Party1 As Int64, _
        ByVal bv_strPartyNumber1 As String, _
        ByVal bv_i64Party2 As Int64, _
        ByVal bv_strPartyNumber2 As String, _
        ByVal bv_i64Party3 As Int64, _
        ByVal bv_strPartyNumber3 As String, _
        ByVal bv_strPortCode As String, _
        ByVal bv_strPortName As String, _
        ByVal bv_strPortNumber As String, _
        ByVal bv_strPortLocationCode As String, _
        ByVal bv_strVesselName As String, _
        ByVal bv_strVoyageNo As String, _
        ByVal bv_strVesselCode As String, _
        ByVal bv_strShipperName As String, _
        ByVal bv_strRailId As String, _
        ByVal bv_strRailRampLocation As String, _
        ByVal bv_strHazardMaterialCode As String, _
        ByVal bv_strHazardMaterialDesc As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._GATEIN_DETAIL).NewRow()
            With dr
                .Item(GateinData.GTN_DTL_ID) = bv_i64GateInDetailID
                .Item(GateinData.GTN_ID) = bv_i64GateInID
                If bv_datManufactureDate <> Nothing Then
                    .Item(GateinData.MNFCTR_DT) = bv_datManufactureDate
                Else
                    .Item(GateinData.MNFCTR_DT) = DBNull.Value
                End If
                If bv_strAcceptanceCode <> Nothing Then
                    .Item(GateinData.ACEP_CD) = bv_strAcceptanceCode
                Else
                    .Item(GateinData.ACEP_CD) = DBNull.Value
                End If
                If bv_i64MaterialID <> 0 Then
                    .Item(GateinData.MTRL_ID) = bv_i64MaterialID
                Else
                    .Item(GateinData.MTRL_ID) = DBNull.Value
                End If
                .Item(GateinData.GRSS_WGHT_NC) = bv_dblGrossWeightNc
                .Item(GateinData.TR_WGHT_NC) = bv_dblTareWeightNc
                If bv_i64MeasureID <> 0 Then
                    .Item(GateinData.MSR_ID) = bv_i64MeasureID
                Else
                    .Item(GateinData.MSR_ID) = DBNull.Value
                End If
                If bv_i64UnitID <> 0 Then
                    .Item(GateinData.UNT_ID) = bv_i64UnitID
                Else
                    .Item(GateinData.UNT_ID) = DBNull.Value
                End If
                If bv_strOnHireLocation <> Nothing Then
                    .Item(GateinData.LST_OH_LOC) = bv_strOnHireLocation
                Else
                    .Item(GateinData.LST_OH_LOC) = DBNull.Value
                End If
                If bv_datOnHireDate <> Nothing Then
                    .Item(GateinData.LST_OH_DT) = bv_datOnHireDate
                Else
                    .Item(GateinData.LST_OH_DT) = DBNull.Value
                End If
                If bv_strTrackerCode <> Nothing Then
                    .Item(GateinData.TRCKR_CD) = bv_strTrackerCode
                Else
                    .Item(GateinData.TRCKR_CD) = DBNull.Value
                End If
                If bv_strLoadStatusCode <> Nothing Then
                    .Item(GateinData.LOD_STTS_CD) = bv_strLoadStatusCode
                Else
                    .Item(GateinData.LOD_STTS_CD) = DBNull.Value
                End If
                If bv_i64CountryID <> 0 Then
                    .Item(GateinData.CNTRY_ID) = bv_i64CountryID
                Else
                    .Item(GateinData.CNTRY_ID) = DBNull.Value
                End If
                If bv_strLicenseState <> Nothing Then
                    .Item(GateinData.LIC_STT) = bv_strLicenseState
                Else
                    .Item(GateinData.LIC_STT) = DBNull.Value
                End If
                If bv_strLicenseNumber <> Nothing Then
                    .Item(GateinData.LIC_REG) = bv_strLicenseNumber
                Else
                    .Item(GateinData.LIC_REG) = DBNull.Value
                End If
                If bv_datLicenseExpiryDate <> Nothing Then
                    .Item(GateinData.LIC_EXPR) = bv_datLicenseExpiryDate
                Else
                    .Item(GateinData.LIC_EXPR) = DBNull.Value
                End If
                If bv_strNotes1 <> Nothing Then
                    .Item(GateinData.NT_1_VC) = bv_strNotes1
                Else
                    .Item(GateinData.NT_1_VC) = DBNull.Value
                End If
                If bv_strNotes2 <> Nothing Then
                    .Item(GateinData.NT_2_VC) = bv_strNotes2
                Else
                    .Item(GateinData.NT_2_VC) = DBNull.Value
                End If
                If bv_strNotes3 <> Nothing Then
                    .Item(GateinData.NT_3_VC) = bv_strNotes3
                Else
                    .Item(GateinData.NT_3_VC) = DBNull.Value
                End If
                If bv_strNotes4 <> Nothing Then
                    .Item(GateinData.NT_4_VC) = bv_strNotes4
                Else
                    .Item(GateinData.NT_4_VC) = DBNull.Value
                End If
                If bv_strNotes5 <> Nothing Then
                    .Item(GateinData.NT_5_VC) = bv_strNotes5
                Else
                    .Item(GateinData.NT_5_VC) = DBNull.Value
                End If
                If bv_i64Party1 <> 0 Then
                    .Item(GateinData.SL_PRTY_1_ID) = bv_i64Party1
                Else
                    .Item(GateinData.SL_PRTY_1_ID) = DBNull.Value
                End If
                If bv_strPartyNumber1 <> Nothing Then
                    .Item(GateinData.SL_NMBR_1) = bv_strPartyNumber1
                Else
                    .Item(GateinData.SL_NMBR_1) = DBNull.Value
                End If
                If bv_i64Party2 <> 0 Then
                    .Item(GateinData.SL_PRTY_2_ID) = bv_i64Party2
                Else
                    .Item(GateinData.SL_PRTY_2_ID) = DBNull.Value
                End If
                If bv_strPartyNumber2 <> Nothing Then
                    .Item(GateinData.SL_NMBR_2) = bv_strPartyNumber2
                Else
                    .Item(GateinData.SL_NMBR_2) = DBNull.Value
                End If
                If bv_i64Party3 <> 0 Then
                    .Item(GateinData.SL_PRTY_3_ID) = bv_i64Party3
                Else
                    .Item(GateinData.SL_PRTY_3_ID) = DBNull.Value
                End If
                If bv_strPartyNumber3 <> Nothing Then
                    .Item(GateinData.SL_NMBR_3) = bv_strPartyNumber3
                Else
                    .Item(GateinData.SL_NMBR_3) = DBNull.Value
                End If
                If bv_strPortCode <> Nothing Then
                    .Item(GateinData.PRT_FNC_CD) = bv_strPortCode
                Else
                    .Item(GateinData.PRT_FNC_CD) = DBNull.Value
                End If
                If bv_strPortName <> Nothing Then
                    .Item(GateinData.PRT_NM) = bv_strPortName
                Else
                    .Item(GateinData.PRT_NM) = DBNull.Value
                End If
                If bv_strPortNumber <> Nothing Then
                    .Item(GateinData.PRT_NO) = bv_strPortNumber
                Else
                    .Item(GateinData.PRT_NO) = DBNull.Value
                End If
                If bv_strPortLocationCode <> Nothing Then
                    .Item(GateinData.PRT_LC_CD) = bv_strPortLocationCode
                Else
                    .Item(GateinData.PRT_LC_CD) = DBNull.Value
                End If
                If bv_strVesselName <> Nothing Then
                    .Item(GateinData.VSSL_NM) = bv_strVesselName
                Else
                    .Item(GateinData.VSSL_NM) = DBNull.Value
                End If
                If bv_strVoyageNo <> Nothing Then
                    .Item(GateinData.VYG_NO) = bv_strVoyageNo
                Else
                    .Item(GateinData.VYG_NO) = DBNull.Value
                End If
                If bv_strVesselCode <> Nothing Then
                    .Item(GateinData.VSSL_CD) = bv_strVesselCode
                Else
                    .Item(GateinData.VSSL_CD) = DBNull.Value
                End If
                If bv_strShipperName <> Nothing Then
                    .Item(GateinData.SHPPR_NAM) = bv_strShipperName
                Else
                    .Item(GateinData.SHPPR_NAM) = DBNull.Value
                End If
                If bv_strRailId <> Nothing Then
                    .Item(GateinData.RL_ID_VC) = bv_strRailId
                Else
                    .Item(GateinData.RL_ID_VC) = DBNull.Value
                End If
                If bv_strRailRampLocation <> Nothing Then
                    .Item(GateinData.RL_RMP_LOC) = bv_strRailRampLocation
                Else
                    .Item(GateinData.RL_RMP_LOC) = DBNull.Value
                End If
                If bv_strHazardMaterialCode <> Nothing Then
                    .Item(GateinData.HAZ_MTL_CD) = bv_strHazardMaterialCode
                Else
                    .Item(GateinData.HAZ_MTL_CD) = DBNull.Value
                End If
                If bv_strHazardMaterialDesc <> Nothing Then
                    .Item(GateinData.HAZ_MATL_DSC) = bv_strHazardMaterialDesc
                Else
                    .Item(GateinData.HAZ_MATL_DSC) = DBNull.Value
                End If
            End With
            UpdateGateinDetail = objData.UpdateRow(dr, Gatein_DetailUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentInformation() TABLE NAME:Equipment_Information"

    Public Function GetEquipmentInformation(ByVal strEquipmentNo As String) As GateinDataSet
        Try
            objData = New DataObjects(V_Equipment_InformationSelectQuery, GateinData.EQPMNT_NO, strEquipmentNo)
            objData.Fill(CType(ds, DataSet), GateinData._V_EQUIPMENT_INFORMATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateGatein() TABLE NAME:Gatein"

    Public Function UpdateGatein(ByVal bv_i64GateInID As Int64, _
        ByVal bv_strEequipmentNo As String, _
        ByVal bv_strYardLocation As String, _
        ByVal bv_datGateInDate As DateTime, _
        ByVal bv_strGateInTime As String, _
        ByVal bv_i64ProductId As Int64, _
        ByVal bv_strEIR_NO As String, _
        ByVal bv_strVechicleNo As String, _
        ByVal bv_strTransporterCode As String, _
        ByVal bv_blnHeatingBit As Boolean, _
        ByVal bv_strRemarks As String, _
        ByVal bv_strGateInTransactionNo As String, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_strConsignee As String, _
        ByVal bv_strRedelAuth As String, _
        ByVal bv_strBillTo As String, _
        ByVal bv_strBillID As String, _
        ByVal bv_strGradeID As String, _
        ByVal bv_strGradeCD As String, _
        ByVal bv_strAgentID As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._GATEIN).NewRow()
            With dr
                .Item(GateinData.GTN_ID) = bv_i64GateInID
                .Item(GateinData.EQPMNT_NO) = bv_strEequipmentNo
                If bv_strYardLocation <> Nothing Then
                    .Item(GateinData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(GateinData.YRD_LCTN) = DBNull.Value
                End If
                .Item(GateinData.GTN_DT) = bv_datGateInDate
                If bv_strGateInTime <> Nothing Then
                    .Item(GateinData.GTN_TM) = bv_strGateInTime
                Else
                    .Item(GateinData.GTN_TM) = DBNull.Value
                End If
                .Item(GateinData.PRDCT_ID) = bv_i64ProductId
                If bv_strEIR_NO <> Nothing Then
                    .Item(GateinData.EIR_NO) = bv_strEIR_NO
                Else
                    .Item(GateinData.EIR_NO) = DBNull.Value
                End If
                If bv_strVechicleNo <> Nothing Then
                    .Item(GateinData.VHCL_NO) = bv_strVechicleNo
                Else
                    .Item(GateinData.VHCL_NO) = DBNull.Value
                End If
                If bv_strTransporterCode <> Nothing Then
                    .Item(GateinData.TRNSPRTR_CD) = bv_strTransporterCode
                Else
                    .Item(GateinData.TRNSPRTR_CD) = DBNull.Value
                End If
                .Item(GateinData.HTNG_BT) = bv_blnHeatingBit
                If bv_strRemarks <> Nothing Then
                    .Item(GateinData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(GateinData.RMRKS_VC) = DBNull.Value
                End If
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(GateinData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(GateinData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                If bv_strConsignee <> Nothing Then
                    .Item(GateinData.CNSGNE) = bv_strConsignee
                Else
                    .Item(GateinData.CNSGNE) = DBNull.Value
                End If
                If bv_strRedelAuth <> Nothing Then
                    .Item(GateinData.RDL_ATH) = bv_strRedelAuth
                Else
                    .Item(GateinData.RDL_ATH) = DBNull.Value
                End If
                If bv_strGradeID <> Nothing Then
                    .Item(GateinData.GRD_ID) = bv_strGradeID
                Else
                    .Item(GateinData.GRD_ID) = DBNull.Value
                End If
                If bv_strGradeCD <> Nothing Then
                    .Item(GateinData.GRD_CD) = bv_strGradeCD
                Else
                    .Item(GateinData.GRD_CD) = DBNull.Value
                End If
                If bv_strBillTo <> Nothing Then
                    .Item(GateinData.BLL_CD) = bv_strBillTo
                Else
                    .Item(GateinData.BLL_CD) = DBNull.Value
                End If
                If bv_strBillID <> Nothing Then
                    .Item(GateinData.BLL_ID) = bv_strBillID
                Else
                    .Item(GateinData.BLL_ID) = DBNull.Value
                End If
                .Item(GateinData.DPT_ID) = bv_i32DepotId
                .Item(GateinData.MDFD_BY) = bv_strModifiedBy
                .Item(GateinData.MDFD_DT) = bv_datModifiedDate

                If bv_strAgentID <> Nothing Then
                    .Item(GateinData.AGNT_ID) = bv_strAgentID
                Else
                    .Item(GateinData.AGNT_ID) = DBNull.Value
                End If

            End With
            UpdateGatein = objData.UpdateRow(dr, GateinUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateGateinRet() TABLE NAME:Gatein_Ret"

    Public Function CreateGateinRet(ByVal bv_lnglngCreated As Long, _
        ByVal bv_strGI_TRANSMISSION_NO As String, _
        ByVal bv_strGI_COMPLETE As String, _
        ByVal bv_strGI_SENT_EIR As String, _
        ByVal bv_strGI_SENT_DATE As String, _
        ByVal bv_strGI_REC_EIR As String, _
        ByVal bv_strGI_REC_DATE As String, _
        ByVal bv_strGI_REC_ADDR As String, _
        ByVal bv_strGI_REC_TYPE As String, _
        ByVal bv_strGI_EXPORTED As String, _
        ByVal bv_strGI_EXPOR_DATE As String, _
        ByVal bv_strGI_IMPORTED As String, _
        ByVal bv_strGI_IMPOR_DATE As String, _
        ByVal bv_strGI_TRNSXN As String, _
        ByVal bv_strGI_ADVICE As String, _
        ByVal bv_strGI_UNIT_NBR As String, _
        ByVal bv_strGI_EQUIP_TYPE As String, _
        ByVal bv_strGI_EQUIP_DESC As String, _
        ByVal bv_strGI_EQUIP_CODE As String, _
        ByVal bv_strGI_CONDITION As String, _
        ByVal bv_strGI_COMP_ID_A As String, _
        ByVal bv_strGI_COMP_ID_N As String, _
        ByVal bv_strGI_COMP_ID_C As String, _
        ByVal bv_strGI_COMP_TYPE As String, _
        ByVal bv_strGI_COMP_DESC As String, _
        ByVal bv_strGI_COMP_CODE As String, _
        ByVal bv_datGI_EIR_DATE As DateTime, _
        ByVal bv_strGI_EIR_TIME As String, _
        ByVal bv_strGI_REFERENCE As String, _
        ByVal bv_strGI_MANU_DATE As String, _
        ByVal bv_strGI_MATERIAL As String, _
        ByVal bv_strGI_WEIGHT As String, _
        ByVal bv_strGI_MEASURE As String, _
        ByVal bv_strGI_UNITS As String, _
        ByVal bv_strGI_CSC_REEXAM As String, _
        ByVal bv_strGI_COUNTRY As String, _
        ByVal bv_strGI_LIC_STATE As String, _
        ByVal bv_strGI_LIC_REG As String, _
        ByVal bv_strGI_LIC_EXPIRE As String, _
        ByVal bv_strGI_LSR_OWNER As String, _
        ByVal bv_strGI_SEND_EDI_1 As String, _
        ByVal bv_strGI_SSL_LSE As String, _
        ByVal bv_strGI_SEND_EDI_2 As String, _
        ByVal bv_strGI_HAULIER As String, _
        ByVal bv_strGI_SEND_EDI_3 As String, _
        ByVal bv_strGI_DPT_TRM As String, _
        ByVal bv_strGI_SEND_EDI_4 As String, _
        ByVal bv_strGI_OTHERS_1 As String, _
        ByVal bv_strGI_OTHERS_2 As String, _
        ByVal bv_strGI_OTHERS_3 As String, _
        ByVal bv_strGI_OTHERS_4 As String, _
        ByVal bv_strGI_NOTE_1 As String, _
        ByVal bv_strGI_NOTE_2 As String, _
        ByVal bv_strGI_LOAD As String, _
        ByVal bv_strGI_FHWA As String, _
        ByVal bv_strGI_LAST_OH_LOC As String, _
        ByVal bv_strGI_LAST_OH_DATE As String, _
        ByVal bv_strGI_SENDER As String, _
        ByVal bv_strGI_ATTENTION As String, _
        ByVal bv_strGI_REVISION As String, _
        ByVal bv_strGI_SEND_EDI_5 As String, _
        ByVal bv_strGI_SEND_EDI_6 As String, _
        ByVal bv_strGI_SEND_EDI_7 As String, _
        ByVal bv_strGI_SEND_EDI_8 As String, _
        ByVal bv_strGI_SEAL_PARTY_1 As String, _
        ByVal bv_strGI_SEAL_NUMBER_1 As String, _
        ByVal bv_strGI_SEAL_PARTY_2 As String, _
        ByVal bv_strGI_SEAL_NUMBER_2 As String, _
        ByVal bv_strGI_SEAL_PARTY_3 As String, _
        ByVal bv_strGI_SEAL_NUMBER_3 As String, _
        ByVal bv_strGI_SEAL_PARTY_4 As String, _
        ByVal bv_strGI_SEAL_NUMBER_4 As String, _
        ByVal bv_strGI_PORT_FUNC_CODE As String, _
        ByVal bv_strGI_PORT_NAME As String, _
        ByVal bv_strGI_VESSEL_NAME As String, _
        ByVal bv_strGI_VOYAGE_NUM As String, _
        ByVal bv_strGI_HAZ_MAT_CODE As String, _
        ByVal bv_strGI_HAZ_MAT_DESC As String, _
        ByVal bv_strGI_NOTE_3 As String, _
        ByVal bv_strGI_NOTE_4 As String, _
        ByVal bv_strGI_NOTE_5 As String, _
        ByVal bv_strGI_COMP_ID_A_2 As String, _
        ByVal bv_strGI_COMP_ID_N_2 As String, _
        ByVal bv_strGI_COMP_ID_C_2 As String, _
        ByVal bv_strGI_COMP_TYPE_2 As String, _
        ByVal bv_strGI_COMP_CODE_2 As String, _
        ByVal bv_strGI_COMP_DESC_2 As String, _
        ByVal bv_strGI_SHIPPER As String, _
        ByVal bv_strGI_DRAY_ORDER As String, _
        ByVal bv_strGI_RAIL_ID As String, _
        ByVal bv_strGI_RAIL_RAMP As String, _
        ByVal bv_strGI_VESSEL_CODE As String, _
        ByVal bv_strGI_WGHT_CERT_1 As String, _
        ByVal bv_strGI_WGHT_CERT_2 As String, _
        ByVal bv_strGI_WGHT_CERT_3 As String, _
        ByVal bv_strGI_SEA_RAIL As String, _
        ByVal bv_strGI_LOC_IDENT As String, _
        ByVal bv_strGI_PORT_LOC_QUAL As String, _
        ByVal bv_strGI_STATUS As String, _
        ByVal bv_datGI_PICK_DATE As DateTime, _
        ByVal bv_strGI_ESTSTATUS As String, _
        ByVal bv_strGI_ERRSTATUS As String, _
        ByVal bv_strGI_USERNAME As String, _
        ByVal bv_i32GI_LIVE_STATUS As Int32, _
        ByVal bv_blnGI_ISACTIVE As Boolean, _
        ByVal bv_strGI_YARD_LOC As String, _
        ByVal bv_strGI_MODE_PAYMENT As String, _
        ByVal bv_strGI_BILLING_TYPE As String, _
        ByVal bv_blnGI_RESERVE_BKG As Boolean, _
        ByVal bv_strGI_RCESTSTATUS As String, _
        ByVal bv_i32OP_SNO As Int32, _
        ByVal bv_blnOP_STATUS As Boolean, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateinData._GATEIN_RET).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GateinData._GATEIN_RET, br_objTrans)
                .Item(GateinData.GI_SNO) = bv_lnglngCreated
                .Item(GateinData.GI_TRANSMISSION_NO) = bv_strGI_TRANSMISSION_NO
                If bv_strGI_COMPLETE <> Nothing Then
                    .Item(GateinData.GI_COMPLETE) = bv_strGI_COMPLETE
                Else
                    .Item(GateinData.GI_COMPLETE) = DBNull.Value
                End If
                If bv_strGI_SENT_EIR <> Nothing Then
                    .Item(GateinData.GI_SENT_EIR) = bv_strGI_SENT_EIR
                Else
                    .Item(GateinData.GI_SENT_EIR) = DBNull.Value
                End If
                .Item(GateinData.GI_SENT_DATE) = bv_strGI_SENT_DATE
                If bv_strGI_REC_EIR <> Nothing Then
                    .Item(GateinData.GI_REC_EIR) = bv_strGI_REC_EIR
                Else
                    .Item(GateinData.GI_REC_EIR) = DBNull.Value
                End If
                If bv_strGI_REC_DATE <> Nothing Then
                    .Item(GateinData.GI_REC_DATE) = bv_strGI_REC_DATE
                Else
                    .Item(GateinData.GI_REC_DATE) = DBNull.Value
                End If
                If bv_strGI_REC_ADDR <> Nothing Then
                    .Item(GateinData.GI_REC_ADDR) = bv_strGI_REC_ADDR
                Else
                    .Item(GateinData.GI_REC_ADDR) = DBNull.Value
                End If
                If bv_strGI_REC_TYPE <> Nothing Then
                    .Item(GateinData.GI_REC_TYPE) = bv_strGI_REC_TYPE
                Else
                    .Item(GateinData.GI_REC_TYPE) = DBNull.Value
                End If
                If bv_strGI_EXPORTED <> Nothing Then
                    .Item(GateinData.GI_EXPORTED) = bv_strGI_EXPORTED
                Else
                    .Item(GateinData.GI_EXPORTED) = DBNull.Value
                End If
                If bv_strGI_EXPOR_DATE <> Nothing Then
                    .Item(GateinData.GI_EXPOR_DATE) = bv_strGI_EXPOR_DATE
                Else
                    .Item(GateinData.GI_EXPOR_DATE) = DBNull.Value
                End If
                If bv_strGI_IMPORTED <> Nothing Then
                    .Item(GateinData.GI_IMPORTED) = bv_strGI_IMPORTED
                Else
                    .Item(GateinData.GI_IMPORTED) = DBNull.Value
                End If
                If bv_strGI_IMPOR_DATE <> Nothing Then
                    .Item(GateinData.GI_IMPOR_DATE) = bv_strGI_IMPOR_DATE
                Else
                    .Item(GateinData.GI_IMPOR_DATE) = DBNull.Value
                End If
                .Item(GateinData.GI_TRNSXN) = bv_strGI_TRNSXN
                .Item(GateinData.GI_ADVICE) = bv_strGI_ADVICE
                .Item(GateinData.GI_UNIT_NBR) = bv_strGI_UNIT_NBR
                .Item(GateinData.GI_EQUIP_TYPE) = bv_strGI_EQUIP_TYPE
                If bv_strGI_EQUIP_DESC <> Nothing Then
                    .Item(GateinData.GI_EQUIP_DESC) = bv_strGI_EQUIP_DESC
                Else
                    .Item(GateinData.GI_EQUIP_DESC) = DBNull.Value
                End If
                .Item(GateinData.GI_EQUIP_CODE) = bv_strGI_EQUIP_CODE
                If bv_strGI_CONDITION <> Nothing Then
                    .Item(GateinData.GI_CONDITION) = bv_strGI_CONDITION
                Else
                    .Item(GateinData.GI_CONDITION) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_A <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_A) = bv_strGI_COMP_ID_A
                Else
                    .Item(GateinData.GI_COMP_ID_A) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_N <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_N) = bv_strGI_COMP_ID_N
                Else
                    .Item(GateinData.GI_COMP_ID_N) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_C <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_C) = bv_strGI_COMP_ID_C
                Else
                    .Item(GateinData.GI_COMP_ID_C) = DBNull.Value
                End If
                If bv_strGI_COMP_TYPE <> Nothing Then
                    .Item(GateinData.GI_COMP_TYPE) = bv_strGI_COMP_TYPE
                Else
                    .Item(GateinData.GI_COMP_TYPE) = DBNull.Value
                End If
                If bv_strGI_COMP_DESC <> Nothing Then
                    .Item(GateinData.GI_COMP_DESC) = bv_strGI_COMP_DESC
                Else
                    .Item(GateinData.GI_COMP_DESC) = DBNull.Value
                End If
                If bv_strGI_COMP_CODE <> Nothing Then
                    .Item(GateinData.GI_COMP_CODE) = bv_strGI_COMP_CODE
                Else
                    .Item(GateinData.GI_COMP_CODE) = DBNull.Value
                End If
                .Item(GateinData.GI_EIR_DATE) = bv_datGI_EIR_DATE
                If bv_strGI_EIR_TIME <> Nothing Then
                    .Item(GateinData.GI_EIR_TIME) = bv_strGI_EIR_TIME
                Else
                    .Item(GateinData.GI_EIR_TIME) = DBNull.Value
                End If
                If bv_strGI_REFERENCE <> Nothing Then
                    .Item(GateinData.GI_REFERENCE) = bv_strGI_REFERENCE
                Else
                    .Item(GateinData.GI_REFERENCE) = DBNull.Value
                End If
                If bv_strGI_MANU_DATE <> Nothing Then
                    .Item(GateinData.GI_MANU_DATE) = bv_strGI_MANU_DATE
                Else
                    .Item(GateinData.GI_MANU_DATE) = DBNull.Value
                End If
                If bv_strGI_MATERIAL <> Nothing Then
                    .Item(GateinData.GI_MATERIAL) = bv_strGI_MATERIAL
                Else
                    .Item(GateinData.GI_MATERIAL) = DBNull.Value
                End If
                If bv_strGI_WEIGHT <> Nothing Then
                    .Item(GateinData.GI_WEIGHT) = bv_strGI_WEIGHT
                Else
                    .Item(GateinData.GI_WEIGHT) = DBNull.Value
                End If
                If bv_strGI_MEASURE <> Nothing Then
                    .Item(GateinData.GI_MEASURE) = bv_strGI_MEASURE
                Else
                    .Item(GateinData.GI_MEASURE) = DBNull.Value
                End If
                If bv_strGI_UNITS <> Nothing Then
                    .Item(GateinData.GI_UNITS) = bv_strGI_UNITS
                Else
                    .Item(GateinData.GI_UNITS) = DBNull.Value
                End If
                If bv_strGI_CSC_REEXAM <> Nothing Then
                    .Item(GateinData.GI_CSC_REEXAM) = bv_strGI_CSC_REEXAM
                Else
                    .Item(GateinData.GI_CSC_REEXAM) = DBNull.Value
                End If
                If bv_strGI_COUNTRY <> Nothing Then
                    .Item(GateinData.GI_COUNTRY) = bv_strGI_COUNTRY
                Else
                    .Item(GateinData.GI_COUNTRY) = DBNull.Value
                End If
                If bv_strGI_LIC_STATE <> Nothing Then
                    .Item(GateinData.GI_LIC_STATE) = bv_strGI_LIC_STATE
                Else
                    .Item(GateinData.GI_LIC_STATE) = DBNull.Value
                End If
                If bv_strGI_LIC_REG <> Nothing Then
                    .Item(GateinData.GI_LIC_REG) = bv_strGI_LIC_REG
                Else
                    .Item(GateinData.GI_LIC_REG) = DBNull.Value
                End If
                If bv_strGI_LIC_EXPIRE <> Nothing Then
                    .Item(GateinData.GI_LIC_EXPIRE) = bv_strGI_LIC_EXPIRE
                Else
                    .Item(GateinData.GI_LIC_EXPIRE) = DBNull.Value
                End If
                .Item(GateinData.GI_LSR_OWNER) = bv_strGI_LSR_OWNER
                If bv_strGI_SEND_EDI_1 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_1) = bv_strGI_SEND_EDI_1
                Else
                    .Item(GateinData.GI_SEND_EDI_1) = DBNull.Value
                End If
                .Item(GateinData.GI_SSL_LSE) = bv_strGI_SSL_LSE
                If bv_strGI_SEND_EDI_2 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_2) = bv_strGI_SEND_EDI_2
                Else
                    .Item(GateinData.GI_SEND_EDI_2) = DBNull.Value
                End If
                If bv_strGI_HAULIER <> Nothing Then
                    .Item(GateinData.GI_HAULIER) = bv_strGI_HAULIER
                Else
                    .Item(GateinData.GI_HAULIER) = DBNull.Value
                End If
                If bv_strGI_SEND_EDI_3 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_3) = bv_strGI_SEND_EDI_3
                Else
                    .Item(GateinData.GI_SEND_EDI_3) = DBNull.Value
                End If
                .Item(GateinData.GI_DPT_TRM) = bv_strGI_DPT_TRM
                If bv_strGI_SEND_EDI_4 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_4) = bv_strGI_SEND_EDI_4
                Else
                    .Item(GateinData.GI_SEND_EDI_4) = DBNull.Value
                End If
                If bv_strGI_OTHERS_1 <> Nothing Then
                    .Item(GateinData.GI_OTHERS_1) = bv_strGI_OTHERS_1
                Else
                    .Item(GateinData.GI_OTHERS_1) = DBNull.Value
                End If
                If bv_strGI_OTHERS_2 <> Nothing Then
                    .Item(GateinData.GI_OTHERS_2) = bv_strGI_OTHERS_2
                Else
                    .Item(GateinData.GI_OTHERS_2) = DBNull.Value
                End If
                If bv_strGI_OTHERS_3 <> Nothing Then
                    .Item(GateinData.GI_OTHERS_3) = bv_strGI_OTHERS_3
                Else
                    .Item(GateinData.GI_OTHERS_3) = DBNull.Value
                End If
                If bv_strGI_OTHERS_4 <> Nothing Then
                    .Item(GateinData.GI_OTHERS_4) = bv_strGI_OTHERS_4
                Else
                    .Item(GateinData.GI_OTHERS_4) = DBNull.Value
                End If
                If bv_strGI_NOTE_1 <> Nothing Then
                    .Item(GateinData.GI_NOTE_1) = bv_strGI_NOTE_1
                Else
                    .Item(GateinData.GI_NOTE_1) = DBNull.Value
                End If
                If bv_strGI_NOTE_2 <> Nothing Then
                    .Item(GateinData.GI_NOTE_2) = bv_strGI_NOTE_2
                Else
                    .Item(GateinData.GI_NOTE_2) = DBNull.Value
                End If
                If bv_strGI_LOAD <> Nothing Then
                    .Item(GateinData.GI_LOAD) = bv_strGI_LOAD
                Else
                    .Item(GateinData.GI_LOAD) = DBNull.Value
                End If
                If bv_strGI_FHWA <> Nothing Then
                    .Item(GateinData.GI_FHWA) = bv_strGI_FHWA
                Else
                    .Item(GateinData.GI_FHWA) = DBNull.Value
                End If
                If bv_strGI_LAST_OH_LOC <> Nothing Then
                    .Item(GateinData.GI_LAST_OH_LOC) = bv_strGI_LAST_OH_LOC
                Else
                    .Item(GateinData.GI_LAST_OH_LOC) = DBNull.Value
                End If
                If bv_strGI_LAST_OH_DATE <> Nothing Then
                    .Item(GateinData.GI_LAST_OH_DATE) = bv_strGI_LAST_OH_DATE
                Else
                    .Item(GateinData.GI_LAST_OH_DATE) = DBNull.Value
                End If
                If bv_strGI_SENDER <> Nothing Then
                    .Item(GateinData.GI_SENDER) = bv_strGI_SENDER
                Else
                    .Item(GateinData.GI_SENDER) = DBNull.Value
                End If
                If bv_strGI_ATTENTION <> Nothing Then
                    .Item(GateinData.GI_ATTENTION) = bv_strGI_ATTENTION
                Else
                    .Item(GateinData.GI_ATTENTION) = DBNull.Value
                End If
                If bv_strGI_REVISION <> Nothing Then
                    .Item(GateinData.GI_REVISION) = bv_strGI_REVISION
                Else
                    .Item(GateinData.GI_REVISION) = DBNull.Value
                End If
                If bv_strGI_SEND_EDI_5 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_5) = bv_strGI_SEND_EDI_5
                Else
                    .Item(GateinData.GI_SEND_EDI_5) = DBNull.Value
                End If
                If bv_strGI_SEND_EDI_6 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_6) = bv_strGI_SEND_EDI_6
                Else
                    .Item(GateinData.GI_SEND_EDI_6) = DBNull.Value
                End If
                If bv_strGI_SEND_EDI_7 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_7) = bv_strGI_SEND_EDI_7
                Else
                    .Item(GateinData.GI_SEND_EDI_7) = DBNull.Value
                End If
                If bv_strGI_SEND_EDI_8 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_8) = bv_strGI_SEND_EDI_8
                Else
                    .Item(GateinData.GI_SEND_EDI_8) = DBNull.Value
                End If
                If bv_strGI_SEAL_PARTY_1 <> Nothing Then
                    .Item(GateinData.GI_SEAL_PARTY_1) = bv_strGI_SEAL_PARTY_1
                Else
                    .Item(GateinData.GI_SEAL_PARTY_1) = DBNull.Value
                End If
                If bv_strGI_SEAL_NUMBER_1 <> Nothing Then
                    .Item(GateinData.GI_SEAL_NUMBER_1) = bv_strGI_SEAL_NUMBER_1
                Else
                    .Item(GateinData.GI_SEAL_NUMBER_1) = DBNull.Value
                End If
                If bv_strGI_SEAL_PARTY_2 <> Nothing Then
                    .Item(GateinData.GI_SEAL_PARTY_2) = bv_strGI_SEAL_PARTY_2
                Else
                    .Item(GateinData.GI_SEAL_PARTY_2) = DBNull.Value
                End If
                If bv_strGI_SEAL_NUMBER_2 <> Nothing Then
                    .Item(GateinData.GI_SEAL_NUMBER_2) = bv_strGI_SEAL_NUMBER_2
                Else
                    .Item(GateinData.GI_SEAL_NUMBER_2) = DBNull.Value
                End If
                If bv_strGI_SEAL_PARTY_3 <> Nothing Then
                    .Item(GateinData.GI_SEAL_PARTY_3) = bv_strGI_SEAL_PARTY_3
                Else
                    .Item(GateinData.GI_SEAL_PARTY_3) = DBNull.Value
                End If
                If bv_strGI_SEAL_NUMBER_3 <> Nothing Then
                    .Item(GateinData.GI_SEAL_NUMBER_3) = bv_strGI_SEAL_NUMBER_3
                Else
                    .Item(GateinData.GI_SEAL_NUMBER_3) = DBNull.Value
                End If
                If bv_strGI_SEAL_PARTY_4 <> Nothing Then
                    .Item(GateinData.GI_SEAL_PARTY_4) = bv_strGI_SEAL_PARTY_4
                Else
                    .Item(GateinData.GI_SEAL_PARTY_4) = DBNull.Value
                End If
                If bv_strGI_SEAL_NUMBER_4 <> Nothing Then
                    .Item(GateinData.GI_SEAL_NUMBER_4) = bv_strGI_SEAL_NUMBER_4
                Else
                    .Item(GateinData.GI_SEAL_NUMBER_4) = DBNull.Value
                End If
                If bv_strGI_PORT_FUNC_CODE <> Nothing Then
                    .Item(GateinData.GI_PORT_FUNC_CODE) = bv_strGI_PORT_FUNC_CODE
                Else
                    .Item(GateinData.GI_PORT_FUNC_CODE) = DBNull.Value
                End If
                If bv_strGI_PORT_NAME <> Nothing Then
                    .Item(GateinData.GI_PORT_NAME) = bv_strGI_PORT_NAME
                Else
                    .Item(GateinData.GI_PORT_NAME) = DBNull.Value
                End If
                If bv_strGI_VESSEL_NAME <> Nothing Then
                    .Item(GateinData.GI_VESSEL_NAME) = bv_strGI_VESSEL_NAME
                Else
                    .Item(GateinData.GI_VESSEL_NAME) = DBNull.Value
                End If
                If bv_strGI_VOYAGE_NUM <> Nothing Then
                    .Item(GateinData.GI_VOYAGE_NUM) = bv_strGI_VOYAGE_NUM
                Else
                    .Item(GateinData.GI_VOYAGE_NUM) = DBNull.Value
                End If
                If bv_strGI_HAZ_MAT_CODE <> Nothing Then
                    .Item(GateinData.GI_HAZ_MAT_CODE) = bv_strGI_HAZ_MAT_CODE
                Else
                    .Item(GateinData.GI_HAZ_MAT_CODE) = DBNull.Value
                End If
                If bv_strGI_HAZ_MAT_DESC <> Nothing Then
                    .Item(GateinData.GI_HAZ_MAT_DESC) = bv_strGI_HAZ_MAT_DESC
                Else
                    .Item(GateinData.GI_HAZ_MAT_DESC) = DBNull.Value
                End If
                If bv_strGI_NOTE_3 <> Nothing Then
                    .Item(GateinData.GI_NOTE_3) = bv_strGI_NOTE_3
                Else
                    .Item(GateinData.GI_NOTE_3) = DBNull.Value
                End If
                If bv_strGI_NOTE_4 <> Nothing Then
                    .Item(GateinData.GI_NOTE_4) = bv_strGI_NOTE_4
                Else
                    .Item(GateinData.GI_NOTE_4) = DBNull.Value
                End If
                If bv_strGI_NOTE_5 <> Nothing Then
                    .Item(GateinData.GI_NOTE_5) = bv_strGI_NOTE_5
                Else
                    .Item(GateinData.GI_NOTE_5) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_A_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_A_2) = bv_strGI_COMP_ID_A_2
                Else
                    .Item(GateinData.GI_COMP_ID_A_2) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_N_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_N_2) = bv_strGI_COMP_ID_N_2
                Else
                    .Item(GateinData.GI_COMP_ID_N_2) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_C_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_C_2) = bv_strGI_COMP_ID_C_2
                Else
                    .Item(GateinData.GI_COMP_ID_C_2) = DBNull.Value
                End If
                If bv_strGI_COMP_TYPE_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_TYPE_2) = bv_strGI_COMP_TYPE_2
                Else
                    .Item(GateinData.GI_COMP_TYPE_2) = DBNull.Value
                End If
                If bv_strGI_COMP_CODE_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_CODE_2) = bv_strGI_COMP_CODE_2
                Else
                    .Item(GateinData.GI_COMP_CODE_2) = DBNull.Value
                End If
                If bv_strGI_COMP_DESC_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_DESC_2) = bv_strGI_COMP_DESC_2
                Else
                    .Item(GateinData.GI_COMP_DESC_2) = DBNull.Value
                End If
                If bv_strGI_SHIPPER <> Nothing Then
                    .Item(GateinData.GI_SHIPPER) = bv_strGI_SHIPPER
                Else
                    .Item(GateinData.GI_SHIPPER) = DBNull.Value
                End If
                If bv_strGI_DRAY_ORDER <> Nothing Then
                    .Item(GateinData.GI_DRAY_ORDER) = bv_strGI_DRAY_ORDER
                Else
                    .Item(GateinData.GI_DRAY_ORDER) = DBNull.Value
                End If
                If bv_strGI_RAIL_ID <> Nothing Then
                    .Item(GateinData.GI_RAIL_ID) = bv_strGI_RAIL_ID
                Else
                    .Item(GateinData.GI_RAIL_ID) = DBNull.Value
                End If
                If bv_strGI_RAIL_RAMP <> Nothing Then
                    .Item(GateinData.GI_RAIL_RAMP) = bv_strGI_RAIL_RAMP
                Else
                    .Item(GateinData.GI_RAIL_RAMP) = DBNull.Value
                End If
                If bv_strGI_VESSEL_CODE <> Nothing Then
                    .Item(GateinData.GI_VESSEL_CODE) = bv_strGI_VESSEL_CODE
                Else
                    .Item(GateinData.GI_VESSEL_CODE) = DBNull.Value
                End If
                If bv_strGI_WGHT_CERT_1 <> Nothing Then
                    .Item(GateinData.GI_WGHT_CERT_1) = bv_strGI_WGHT_CERT_1
                Else
                    .Item(GateinData.GI_WGHT_CERT_1) = DBNull.Value
                End If
                If bv_strGI_WGHT_CERT_2 <> Nothing Then
                    .Item(GateinData.GI_WGHT_CERT_2) = bv_strGI_WGHT_CERT_2
                Else
                    .Item(GateinData.GI_WGHT_CERT_2) = DBNull.Value
                End If
                If bv_strGI_WGHT_CERT_3 <> Nothing Then
                    .Item(GateinData.GI_WGHT_CERT_3) = bv_strGI_WGHT_CERT_3
                Else
                    .Item(GateinData.GI_WGHT_CERT_3) = DBNull.Value
                End If
                If bv_strGI_SEA_RAIL <> Nothing Then
                    .Item(GateinData.GI_SEA_RAIL) = bv_strGI_SEA_RAIL
                Else
                    .Item(GateinData.GI_SEA_RAIL) = DBNull.Value
                End If
                If bv_strGI_LOC_IDENT <> Nothing Then
                    .Item(GateinData.GI_LOC_IDENT) = bv_strGI_LOC_IDENT
                Else
                    .Item(GateinData.GI_LOC_IDENT) = DBNull.Value
                End If
                If bv_strGI_PORT_LOC_QUAL <> Nothing Then
                    .Item(GateinData.GI_PORT_LOC_QUAL) = bv_strGI_PORT_LOC_QUAL
                Else
                    .Item(GateinData.GI_PORT_LOC_QUAL) = DBNull.Value
                End If
                If bv_strGI_STATUS <> Nothing Then
                    .Item(GateinData.GI_STATUS) = bv_strGI_STATUS
                Else
                    .Item(GateinData.GI_STATUS) = DBNull.Value
                End If
                If bv_datGI_PICK_DATE <> Nothing And bv_datGI_PICK_DATE <> sqlDbnull Then
                    .Item(GateinData.GI_PICK_DATE) = bv_datGI_PICK_DATE
                Else
                    .Item(GateinData.GI_PICK_DATE) = DBNull.Value
                End If
                If bv_strGI_ESTSTATUS <> Nothing Then
                    .Item(GateinData.GI_ESTSTATUS) = bv_strGI_ESTSTATUS
                Else
                    .Item(GateinData.GI_ESTSTATUS) = DBNull.Value
                End If
                If bv_strGI_ERRSTATUS <> Nothing Then
                    .Item(GateinData.GI_ERRSTATUS) = bv_strGI_ERRSTATUS
                Else
                    .Item(GateinData.GI_ERRSTATUS) = DBNull.Value
                End If
                If bv_strGI_USERNAME <> Nothing Then
                    .Item(GateinData.GI_USERNAME) = bv_strGI_USERNAME
                Else
                    .Item(GateinData.GI_USERNAME) = DBNull.Value
                End If
                If bv_i32GI_LIVE_STATUS <> 0 Then
                    .Item(GateinData.GI_LIVE_STATUS) = bv_i32GI_LIVE_STATUS
                Else
                    .Item(GateinData.GI_LIVE_STATUS) = DBNull.Value
                End If
                If bv_blnGI_ISACTIVE <> Nothing Then
                    .Item(GateinData.GI_ISACTIVE) = bv_blnGI_ISACTIVE
                Else
                    .Item(GateinData.GI_ISACTIVE) = DBNull.Value
                End If
                If bv_strGI_YARD_LOC <> Nothing Then
                    .Item(GateinData.GI_YARD_LOC) = bv_strGI_YARD_LOC
                Else
                    .Item(GateinData.GI_YARD_LOC) = DBNull.Value
                End If
                If bv_strGI_MODE_PAYMENT <> Nothing Then
                    .Item(GateinData.GI_MODE_PAYMENT) = bv_strGI_MODE_PAYMENT
                Else
                    .Item(GateinData.GI_MODE_PAYMENT) = DBNull.Value
                End If
                If bv_strGI_BILLING_TYPE <> Nothing Then
                    .Item(GateinData.GI_BILLING_TYPE) = bv_strGI_BILLING_TYPE
                Else
                    .Item(GateinData.GI_BILLING_TYPE) = DBNull.Value
                End If
                If bv_blnGI_RESERVE_BKG <> Nothing Then
                    .Item(GateinData.GI_RESERVE_BKG) = bv_blnGI_RESERVE_BKG
                Else
                    .Item(GateinData.GI_RESERVE_BKG) = DBNull.Value
                End If
                If bv_strGI_RCESTSTATUS <> Nothing Then
                    .Item(GateinData.GI_RCESTSTATUS) = bv_strGI_RCESTSTATUS
                Else
                    .Item(GateinData.GI_RCESTSTATUS) = DBNull.Value
                End If
                If bv_i32OP_SNO <> 0 Then
                    .Item(GateinData.OP_SNO) = bv_i32OP_SNO
                Else
                    .Item(GateinData.OP_SNO) = DBNull.Value
                End If
                If bv_blnOP_STATUS <> Nothing Then
                    .Item(GateinData.OP_STATUS) = bv_blnOP_STATUS
                Else
                    .Item(GateinData.OP_STATUS) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, GateinRetInsertQuery, br_objTrans)
            dr = Nothing
            CreateGateinRet = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateHandlingCharge() TABLE NAME:Handling_Charge"

    Public Function CreateHandlingCharge(ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_i64EQPMNTCodeID As Int64, _
        ByVal bv_i64EQPMNT_TYP_ID As Int64, _
        ByVal bv_strCstType As String, _
        ByVal bv_strRFRNC_EIR_NO_1 As String, _
        ByVal bv_strRFRNC_EIR_NO_2 As String, _
        ByVal bv_datFRM_BLLNG_DT As DateTime, _
        ByVal bv_datTO_BLLNG_DT As DateTime, _
        ByVal bv_i32FR_DYS As Int32, _
        ByVal bv_i32NO_OF_DYS As Int32, _
        ByVal bv_strHNDLNG_CST_NC As Decimal, _
        ByVal bv_strHNDLNG_TX_RT_NC As Decimal, _
        ByVal bv_strTTL_CSTS_NC As Decimal, _
        ByVal bv_strBLLNG_FLG As String, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_strIS_GT_OT_FLG As String, _
        ByVal bv_strYRD_LCTN As String, _
        ByVal bv_strBLLNG_TYP_CD As String, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_blnHeatingBT As Boolean, _
        ByVal strGI_TRNSCTN_NO As String, _
        ByVal strAgentId As String, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateinData._HANDLING_CHARGE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GateinData._HANDLING_CHARGE, br_objTrans)
                .Item(GateinData.HNDLNG_CHRG_ID) = intMax
                .Item(GateinData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(GateinData.EQPMNT_CD_ID) = bv_i64EQPMNTCodeID
                .Item(GateinData.EQPMNT_TYP_ID) = bv_i64EQPMNT_TYP_ID
                .Item(GateinData.CST_TYP) = bv_strCstType
                .Item(GateinData.RFRNC_EIR_NO_1) = bv_strRFRNC_EIR_NO_1
                .Item(GateinData.RFRNC_EIR_NO_2) = bv_strRFRNC_EIR_NO_2
                .Item(GateinData.FRM_BLLNG_DT) = bv_datFRM_BLLNG_DT
                .Item(GateinData.TO_BLLNG_DT) = bv_datTO_BLLNG_DT
                .Item(GateinData.FR_DYS) = bv_i32FR_DYS
                .Item(GateinData.NO_OF_DYS) = bv_i32NO_OF_DYS
                .Item(GateinData.HNDLNG_CST_NC) = bv_strHNDLNG_CST_NC
                .Item(GateinData.HNDLNG_TX_RT_NC) = bv_strHNDLNG_TX_RT_NC
                .Item(GateinData.TTL_CSTS_NC) = bv_strTTL_CSTS_NC
                .Item(GateinData.BLLNG_FLG) = bv_strBLLNG_FLG
                .Item(GateinData.ACTV_BT) = bv_blnACTV_BT
                .Item(GateinData.DPT_ID) = bv_i32DPT_ID
                .Item(GateinData.IS_GT_OT_FLG) = bv_strIS_GT_OT_FLG
                If bv_strYRD_LCTN <> Nothing Then
                    .Item(GateinData.YRD_LCTN) = bv_strYRD_LCTN
                Else
                    .Item(GateinData.YRD_LCTN) = DBNull.Value
                End If
                If bv_strBLLNG_TYP_CD <> Nothing Then
                    .Item(GateinData.BLLNG_TYP_CD) = bv_strBLLNG_TYP_CD
                Else
                    .Item(GateinData.BLLNG_TYP_CD) = DBNull.Value
                End If
                .Item(GateinData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(GateinData.HTNG_BT) = bv_blnHeatingBT
                If strGI_TRNSCTN_NO <> Nothing Then
                    .Item(GateinData.GI_TRNSCTN_NO) = strGI_TRNSCTN_NO
                Else
                    .Item(GateinData.GI_TRNSCTN_NO) = DBNull.Value
                End If

                If strAgentId <> Nothing Then
                    .Item(GateinData.AGNT_ID) = strAgentId
                Else
                    .Item(GateinData.AGNT_ID) = DBNull.Value
                End If

            End With
            objData.InsertRow(dr, HandlingChargeInsertQuery, br_objTrans)
            dr = Nothing
            CreateHandlingCharge = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateHandlingStorage"
    Public Function UpdateHandling(ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_datFRM_BLLNG_DT As DateTime, _
        ByVal bv_strYRD_LCTN As String, _
        ByVal strGI_Transaction As String, _
        ByVal intDepotId As Integer, _
        ByVal bv_blnHeatingBit As Boolean, _
        ByVal bv_strEIRNo As String, _
        ByVal bv_decHandlingCharge As Decimal, _
        ByVal strAgentId As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._HANDLING_CHARGE).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(GateinData.FRM_BLLNG_DT) = bv_datFRM_BLLNG_DT
                .Item(GateinData.TO_BLLNG_DT) = bv_datFRM_BLLNG_DT
                .Item(GateinData.YRD_LCTN) = bv_strYRD_LCTN
                .Item(GateinData.HTNG_BT) = bv_blnHeatingBit
                .Item(GateinData.GI_TRNSCTN_NO) = strGI_Transaction
                .Item(GateinData.RFRNC_EIR_NO_1) = bv_strEIRNo
                .Item(GateinData.HNDLNG_CST_NC) = bv_decHandlingCharge
                .Item(GateinData.TTL_CSTS_NC) = bv_decHandlingCharge
                .Item(GateinData.DPT_ID) = intDepotId
                If strAgentId <> Nothing Then
                    .Item(GateinData.AGNT_ID) = strAgentId
                Else
                    .Item(GateinData.AGNT_ID) = DBNull.Value
                End If
            End With
            UpdateHandling = objData.UpdateRow(dr, UpdateHandlingStorageQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateStorage"
    Public Function UpdateStorage(ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_datFRM_BLLNG_DT As DateTime, _
        ByVal bv_strYRD_LCTN As String, _
        ByVal bv_intDepotId As Integer, _
        ByVal bv_blnHeatingBit As Boolean, _
        ByVal strGI_Transaction As String, _
        ByVal bv_strEIRNo As String, _
        ByVal strAgentId As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._STORAGE_CHARGE).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(GateinData.FRM_BLLNG_DT) = bv_datFRM_BLLNG_DT
                .Item(GateinData.YRD_LCTN) = bv_strYRD_LCTN
                .Item(GateinData.DPT_ID) = bv_intDepotId
                .Item(GateinData.HTNG_BT) = bv_blnHeatingBit
                .Item(GateinData.GI_TRNSCTN_NO) = strGI_Transaction
                .Item(GateinData.RFRNC_EIR_NO_1) = bv_strEIRNo

                If strAgentId <> Nothing Then
                    .Item(GateinData.AGNT_ID) = strAgentId
                Else
                    .Item(GateinData.AGNT_ID) = DBNull.Value
                End If

            End With
            UpdateStorage = objData.UpdateRow(dr, updateStorageCharge, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateStorageCharge() TABLE NAME:Storage_Charge"

    Public Function CreateStorageCharge(ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64EquipmentCodeId As Int64, _
        ByVal bv_i64EquipmentTypeId As Int64, _
        ByVal bv_strCstType As String, _
        ByVal bv_strReferenceEIRNo1 As String, _
        ByVal bv_strReferenceEIRNo2 As String, _
        ByVal bv_datFromBillingDate As DateTime, _
        ByVal bv_datToBillingDate As DateTime, _
        ByVal bv_i32FreeDays As Int32, _
        ByVal bv_i32NoOfDays As Int32, _
        ByVal bv_strStorageCost As Decimal, _
        ByVal bv_strStorageTaxRate As Decimal, _
        ByVal bv_strTotalcost As Decimal, _
        ByVal bv_strStorageConditionFlag As String, _
        ByVal bv_strBillingFlag As String, _
        ByVal bv_strIsGateOutFlag As String, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_blnIslateflag As Boolean, _
        ByVal bv_datbillingTotal As DateTime, _
        ByVal bv_strYardLocation As String, _
        ByVal bv_strBillingtypeCode As String, _
        ByVal bv_i64CustomerId As Int64, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_blnHeatingBT As Boolean, _
        ByVal strGI_TRNSCTN_NO As String, _
        ByVal strAgentId As String, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateinData._STORAGE_CHARGE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GateinData._STORAGE_CHARGE, br_objTrans)
                .Item(GateinData.STRG_CHRG_ID) = intMax
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateinData.EQPMNT_CD_ID) = bv_i64EquipmentCodeId
                .Item(GateinData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                .Item(GateinData.CST_TYP) = bv_strCstType
                .Item(GateinData.RFRNC_EIR_NO_1) = bv_strReferenceEIRNo1
                If bv_strReferenceEIRNo2 <> Nothing Then
                    .Item(GateinData.RFRNC_EIR_NO_2) = bv_strReferenceEIRNo2
                Else
                    .Item(GateinData.RFRNC_EIR_NO_2) = DBNull.Value
                End If
                .Item(GateinData.FRM_BLLNG_DT) = bv_datFromBillingDate
                If bv_datToBillingDate <> Nothing And bv_datToBillingDate <> sqlDbnull Then
                    .Item(GateinData.TO_BLLNG_DT) = bv_datToBillingDate
                Else
                    .Item(GateinData.TO_BLLNG_DT) = DBNull.Value
                End If
                .Item(GateinData.FR_DYS) = bv_i32FreeDays
                If bv_i32NoOfDays <> 0 Then
                    .Item(GateinData.NO_OF_DYS) = bv_i32NoOfDays
                Else
                    .Item(GateinData.NO_OF_DYS) = DBNull.Value
                End If
                .Item(GateinData.STRG_CST_NC) = bv_strStorageCost
                .Item(GateinData.STRG_TX_RT_NC) = bv_strStorageTaxRate
                .Item(GateinData.TTL_CSTS_NC) = bv_strTotalcost
                .Item(GateinData.STRG_CNTN_FLG) = bv_strStorageConditionFlag
                .Item(GateinData.BLLNG_FLG) = bv_strBillingFlag
                .Item(GateinData.IS_GT_OT_FLG) = bv_strIsGateOutFlag
                .Item(GateinData.ACTV_BT) = bv_blnActiveBit
                .Item(GateinData.IS_LT_FLG) = bv_blnIslateflag
                If bv_datbillingTotal <> Nothing Then
                    .Item(GateinData.BLLNG_TLL_DT) = bv_datbillingTotal
                Else
                    .Item(GateinData.BLLNG_TLL_DT) = DBNull.Value
                End If
                If bv_strYardLocation <> Nothing Then
                    .Item(GateinData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(GateinData.YRD_LCTN) = DBNull.Value
                End If
                If bv_strBillingtypeCode <> Nothing Then
                    .Item(GateinData.BLLNG_TYP_CD) = bv_strBillingtypeCode
                Else
                    .Item(GateinData.BLLNG_TYP_CD) = DBNull.Value
                End If
                .Item(GateinData.CSTMR_ID) = bv_i64CustomerId
                .Item(GateinData.DPT_ID) = bv_i32DepotId
                .Item(GateinData.HTNG_BT) = bv_blnHeatingBT
                If strGI_TRNSCTN_NO <> Nothing Then
                    .Item(GateinData.GI_TRNSCTN_NO) = strGI_TRNSCTN_NO
                Else
                    .Item(GateinData.GI_TRNSCTN_NO) = DBNull.Value
                End If

                If strAgentId <> Nothing Then
                    .Item(GateinData.AGNT_ID) = strAgentId
                Else
                    .Item(GateinData.AGNT_ID) = DBNull.Value
                End If

            End With
            objData.InsertRow(dr, StorageChargeInsertQuery, br_objTrans)
            dr = Nothing
            CreateStorageCharge = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetGateInDetails() TABLE NAME:GATEIN"

    Public Function GetGateInPreAdviceDetail(ByVal bv_intDepotId As Integer) As GateinDataSet
        Try
            Console.WriteLine("GateinSelectQueryfromPreAdvice")


            objData = New DataObjects(GateinSelectQueryfromPreAdvice, GateinData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), GateinData._V_GATEIN)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetGateIn() TABLE NAME:GATEIN"
    Public Function GetGateIn(ByVal bv_intDepotId As Integer) As GateinDataSet
        Try
            'Console.WriteLine("GateinSelectQueryfromGateIn");



            objData = New DataObjects(GateinSelectQueryfromGateIn, GateinData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), GateinData._V_GATEIN)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetGateInStatusId"
    Public Function pub_GetEqupimentStatus(ByVal bv_intDPT_ID As Integer) As GateinDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(GateinData.DPT_ID, bv_intDPT_ID)
            objData = New DataObjects(Equipment_Status_SelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), GateinData._EQUIPMENT_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetCustomerDetail"
    Public Function pub_GetCustomerDetail(bv_intDPT_ID As Integer) As GateinDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(GateinData.DPT_ID, bv_intDPT_ID)
            objData = New DataObjects(Customer_SelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), GateinData._CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateActivityStatus() TABLE NAME:Activity_Status"

    Public Function CreateActivityStatus(ByVal bv_i64CustomerId As Int64, _
                                         ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_i64EquipmentTypeId As Int64, _
                                         ByVal bv_i64EquipmentCodeId As Int64, _
                                         ByVal bv_datGateInDate As DateTime, _
                                         ByVal bv_datGateOutDate As DateTime, _
                                         ByVal bv_i64ProductId As Int64, _
                                         ByVal bv_datCleaningDate As DateTime, _
                                         ByVal bv_i64EquipmentStatusId As Int64, _
                                         ByVal bv_strActivityId As String, _
                                         ByVal bv_datActivityDate As DateTime, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strGateInTransactionNo As String, _
                                         ByVal bv_blnActivityBit As Boolean, _
                                         ByVal bv_GI_Ref_no As String, _
                                         ByVal bv_i32DepotId As Int32, _
                                         ByVal bv_strYardLocation As String,
                                         ByVal bv_blnInvoiceGeneratedBit As Boolean, _
                                         ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateinData._ACTIVITY_STATUS).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GateinData._ACTIVITY_STATUS, br_objTrans)
                .Item(GateinData.ACTVTY_STTS_ID) = intMax
                .Item(GateinData.CSTMR_ID) = bv_i64CustomerId
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateinData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                If bv_i64EquipmentCodeId <> 0 Then
                    .Item(GateinData.EQPMNT_CD_ID) = bv_i64EquipmentCodeId
                Else
                    .Item(GateinData.EQPMNT_CD_ID) = DBNull.Value
                End If
                .Item(GateinData.GTN_DT) = bv_datGateInDate
                If bv_datGateOutDate <> Nothing Then
                    .Item(GateinData.GTOT_DT) = bv_datGateOutDate
                Else
                    .Item(GateinData.GTOT_DT) = DBNull.Value
                End If
                .Item(GateinData.PRDCT_ID) = bv_i64ProductId
                If bv_datCleaningDate <> Nothing Then
                    .Item(GateinData.CLNNG_DT) = bv_datCleaningDate
                Else
                    .Item(GateinData.CLNNG_DT) = DBNull.Value
                End If
                .Item(GateinData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                .Item(GateinData.ACTVTY_NAM) = bv_strActivityId
                .Item(GateinData.ACTVTY_DT) = bv_datActivityDate
                If bv_strRemarks <> Nothing Then
                    .Item(GateinData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(GateinData.RMRKS_VC) = DBNull.Value
                End If
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(GateinData.ACTV_BT) = bv_blnActivityBit
                .Item(GateinData.GI_RF_NO) = bv_GI_Ref_no
                .Item(GateinData.DPT_ID) = bv_i32DepotId
                If bv_strYardLocation <> Nothing Then
                    .Item(GateinData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(GateinData.YRD_LCTN) = DBNull.Value
                End If
                .Item(GateinData.INVC_GNRT_BT) = bv_blnInvoiceGeneratedBit

            End With
            objData.InsertRow(dr, Activity_StatusInsertQuery, br_objTrans)
            dr = Nothing
            CreateActivityStatus = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetVGateinDetailBy() TABLE NAME:V_GATEIN_DETAIL"

    Public Function GetVGateinDetailBy(ByVal bv_GateinID As Int64, ByVal bv_EquipmentNo As String, ByVal bv_DepotID As Integer) As GateinDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(GateinData.GTN_ID, bv_GateinID)
            hshTable.Add(GateinData.DPT_ID, bv_DepotID)
            hshTable.Add(GateinData.EQPMNT_NO, bv_EquipmentNo)
            Console.WriteLine("V_GATEIN_DETAILSelectQueryBy")

            objData = New DataObjects(V_GATEIN_DETAILSelectQueryBy, hshTable)
            objData.Fill(CType(ds, DataSet), GateinData._V_GATEIN_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetGateinDetailCount"
    Public Function GetGateinDetailCount(ByVal bv_i64Gatein_Id As Long, ByRef br_ObjTransactions As Transactions) As Long
        Try
            objData = New DataObjects(GateinDetailCountQuery, GateinData.GTN_ID, bv_i64Gatein_Id)
            Return objData.ExecuteScalar(br_ObjTransactions)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTracking() TABLE NAME:Tracking"

    Public Function UpdateTracking(ByVal bv_strEQPMNT_NO As String, _
                ByVal bv_i64CustomerID As Int64, _
                ByVal bv_datActivity As DateTime, _
                ByVal bv_strActivityRemarks As String, _
                ByVal strGI_TRNSCTN_NO As String, _
                ByVal strReferenceNo As String, _
                ByVal bv_i32DepotId As Int32, _
                ByVal bv_ModifiedBy As String, _
                ByVal bv_ModifiedDate As DateTime, _
                ByVal bv_RentalCustomerID As Integer, _
                 ByVal bv_strEquipmentInfoRemarks As String, _
                ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._TRACKING).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(GateinData.CSTMR_ID) = bv_i64CustomerID
                .Item(GateinData.ACTVTY_DT) = bv_datActivity
                If bv_strActivityRemarks <> Nothing Then
                    .Item(GateinData.ACTVTY_RMRKS) = bv_strActivityRemarks
                Else
                    .Item(GateinData.ACTVTY_RMRKS) = DBNull.Value
                End If
                If strGI_TRNSCTN_NO <> Nothing Then
                    .Item(GateinData.GI_TRNSCTN_NO) = strGI_TRNSCTN_NO
                Else
                    .Item(GateinData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                If strReferenceNo <> Nothing Then
                    .Item(GateinData.RFRNC_NO) = strReferenceNo
                Else
                    .Item(GateinData.RFRNC_NO) = DBNull.Value
                End If
                If bv_RentalCustomerID <> Nothing Then
                    .Item(GateinData.RNTL_CSTMR_ID) = bv_RentalCustomerID
                Else
                    .Item(GateinData.RNTL_CSTMR_ID) = DBNull.Value
                End If
                .Item(GateinData.DPT_ID) = bv_i32DepotId
                .Item(GateinData.MDFD_BY) = bv_ModifiedBy
                .Item(GateinData.MDFD_DT) = bv_ModifiedDate
                If bv_strEquipmentInfoRemarks <> Nothing Then
                    .Item(GateinData.EQPMNT_INFRMTN_RMRKS_VC) = bv_strEquipmentInfoRemarks
                Else
                    .Item(GateinData.EQPMNT_INFRMTN_RMRKS_VC) = DBNull.Value
                End If
            End With
            UpdateTracking = objData.UpdateRow(dr, TrackingUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateGateinRet() TABLE NAME:Gatein_Ret"

    Public Function UpdateGateinRet(ByVal bv_strGI_TRANSMISSION_NO As String, _
        ByVal bv_strGI_COMPLETE As String, _
        ByVal bv_strGI_SENT_EIR As String, _
        ByVal bv_strGI_SENT_DATE As String, _
        ByVal bv_strGI_REC_EIR As String, _
        ByVal bv_strGI_REC_DATE As String, _
        ByVal bv_strGI_REC_ADDR As String, _
        ByVal bv_strGI_REC_TYPE As String, _
        ByVal bv_strGI_EXPORTED As String, _
        ByVal bv_strGI_EXPOR_DATE As String, _
        ByVal bv_strGI_IMPORTED As String, _
        ByVal bv_strGI_IMPOR_DATE As String, _
        ByVal bv_strGI_TRNSXN As String, _
        ByVal bv_strGI_ADVICE As String, _
        ByVal bv_strGI_UNIT_NBR As String, _
        ByVal bv_strGI_EQUIP_TYPE As String, _
        ByVal bv_strGI_EQUIP_DESC As String, _
        ByVal bv_strGI_EQUIP_CODE As String, _
        ByVal bv_strGI_CONDITION As String, _
        ByVal bv_strGI_COMP_ID_A As String, _
        ByVal bv_strGI_COMP_ID_N As String, _
        ByVal bv_strGI_COMP_ID_C As String, _
        ByVal bv_strGI_COMP_TYPE As String, _
        ByVal bv_strGI_COMP_DESC As String, _
        ByVal bv_strGI_COMP_CODE As String, _
        ByVal bv_datGI_EIR_DATE As DateTime, _
        ByVal bv_strGI_EIR_TIME As String, _
        ByVal bv_strGI_REFERENCE As String, _
        ByVal bv_strGI_MANU_DATE As String, _
        ByVal bv_strGI_MATERIAL As String, _
        ByVal bv_strGI_WEIGHT As String, _
        ByVal bv_strGI_MEASURE As String, _
        ByVal bv_strGI_UNITS As String, _
        ByVal bv_strGI_CSC_REEXAM As String, _
        ByVal bv_strGI_COUNTRY As String, _
        ByVal bv_strGI_LIC_STATE As String, _
        ByVal bv_strGI_LIC_REG As String, _
        ByVal bv_strGI_LIC_EXPIRE As String, _
        ByVal bv_strGI_LSR_OWNER As String, _
        ByVal bv_strGI_SEND_EDI_1 As String, _
        ByVal bv_strGI_SSL_LSE As String, _
        ByVal bv_strGI_SEND_EDI_2 As String, _
        ByVal bv_strGI_HAULIER As String, _
        ByVal bv_strGI_SEND_EDI_3 As String, _
        ByVal bv_strGI_DPT_TRM As String, _
        ByVal bv_strGI_SEND_EDI_4 As String, _
        ByVal bv_strGI_OTHERS_1 As String, _
        ByVal bv_strGI_OTHERS_2 As String, _
        ByVal bv_strGI_OTHERS_3 As String, _
        ByVal bv_strGI_OTHERS_4 As String, _
        ByVal bv_strGI_NOTE_1 As String, _
        ByVal bv_strGI_NOTE_2 As String, _
        ByVal bv_strGI_LOAD As String, _
        ByVal bv_strGI_FHWA As String, _
        ByVal bv_strGI_LAST_OH_LOC As String, _
        ByVal bv_strGI_LAST_OH_DATE As String, _
        ByVal bv_strGI_SENDER As String, _
        ByVal bv_strGI_ATTENTION As String, _
        ByVal bv_strGI_REVISION As String, _
        ByVal bv_strGI_SEND_EDI_5 As String, _
        ByVal bv_strGI_SEND_EDI_6 As String, _
        ByVal bv_strGI_SEND_EDI_7 As String, _
        ByVal bv_strGI_SEND_EDI_8 As String, _
        ByVal bv_strGI_SEAL_PARTY_1 As String, _
        ByVal bv_strGI_SEAL_NUMBER_1 As String, _
        ByVal bv_strGI_SEAL_PARTY_2 As String, _
        ByVal bv_strGI_SEAL_NUMBER_2 As String, _
        ByVal bv_strGI_SEAL_PARTY_3 As String, _
        ByVal bv_strGI_SEAL_NUMBER_3 As String, _
        ByVal bv_strGI_SEAL_PARTY_4 As String, _
        ByVal bv_strGI_SEAL_NUMBER_4 As String, _
        ByVal bv_strGI_PORT_FUNC_CODE As String, _
        ByVal bv_strGI_PORT_NAME As String, _
        ByVal bv_strGI_VESSEL_NAME As String, _
        ByVal bv_strGI_VOYAGE_NUM As String, _
        ByVal bv_strGI_HAZ_MAT_CODE As String, _
        ByVal bv_strGI_HAZ_MAT_DESC As String, _
        ByVal bv_strGI_NOTE_3 As String, _
        ByVal bv_strGI_NOTE_4 As String, _
        ByVal bv_strGI_NOTE_5 As String, _
        ByVal bv_strGI_COMP_ID_A_2 As String, _
        ByVal bv_strGI_COMP_ID_N_2 As String, _
        ByVal bv_strGI_COMP_ID_C_2 As String, _
        ByVal bv_strGI_COMP_TYPE_2 As String, _
        ByVal bv_strGI_COMP_CODE_2 As String, _
        ByVal bv_strGI_COMP_DESC_2 As String, _
        ByVal bv_strGI_SHIPPER As String, _
        ByVal bv_strGI_DRAY_ORDER As String, _
        ByVal bv_strGI_RAIL_ID As String, _
        ByVal bv_strGI_RAIL_RAMP As String, _
        ByVal bv_strGI_VESSEL_CODE As String, _
        ByVal bv_strGI_WGHT_CERT_1 As String, _
        ByVal bv_strGI_WGHT_CERT_2 As String, _
        ByVal bv_strGI_WGHT_CERT_3 As String, _
        ByVal bv_strGI_SEA_RAIL As String, _
        ByVal bv_strGI_LOC_IDENT As String, _
        ByVal bv_strGI_PORT_LOC_QUAL As String, _
        ByVal bv_strGI_STATUS As String, _
        ByVal bv_datGI_PICK_DATE As DateTime, _
        ByVal bv_strGI_ESTSTATUS As String, _
        ByVal bv_strGI_ERRSTATUS As String, _
        ByVal bv_strGI_USERNAME As String, _
        ByVal bv_i32GI_LIVE_STATUS As Int32, _
        ByVal bv_blnGI_ISACTIVE As Boolean, _
        ByVal bv_strGI_YARD_LOC As String, _
        ByVal bv_strGI_MODE_PAYMENT As String, _
        ByVal bv_strGI_BILLING_TYPE As String, _
        ByVal bv_blnGI_RESERVE_BKG As Boolean, _
        ByVal bv_strGI_RCESTSTATUS As String, _
        ByVal bv_i32OP_SNO As Int32, _
        ByVal bv_blnOP_STATUS As Boolean, _
         ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._GATEIN_RET).NewRow()
            With dr
                .Item(GateinData.GI_TRANSMISSION_NO) = bv_strGI_TRANSMISSION_NO
                If bv_strGI_COMPLETE <> Nothing Then
                    .Item(GateinData.GI_COMPLETE) = bv_strGI_COMPLETE
                Else
                    .Item(GateinData.GI_COMPLETE) = DBNull.Value
                End If
                If bv_strGI_SENT_EIR <> Nothing Then
                    .Item(GateinData.GI_SENT_EIR) = bv_strGI_SENT_EIR
                Else
                    .Item(GateinData.GI_SENT_EIR) = DBNull.Value
                End If
                .Item(GateinData.GI_SENT_DATE) = bv_strGI_SENT_DATE
                If bv_strGI_REC_EIR <> Nothing Then
                    .Item(GateinData.GI_REC_EIR) = bv_strGI_REC_EIR
                Else
                    .Item(GateinData.GI_REC_EIR) = DBNull.Value
                End If
                If bv_strGI_REC_DATE <> Nothing Then
                    .Item(GateinData.GI_REC_DATE) = bv_strGI_REC_DATE
                Else
                    .Item(GateinData.GI_REC_DATE) = DBNull.Value
                End If
                If bv_strGI_REC_ADDR <> Nothing Then
                    .Item(GateinData.GI_REC_ADDR) = bv_strGI_REC_ADDR
                Else
                    .Item(GateinData.GI_REC_ADDR) = DBNull.Value
                End If
                If bv_strGI_REC_TYPE <> Nothing Then
                    .Item(GateinData.GI_REC_TYPE) = bv_strGI_REC_TYPE
                Else
                    .Item(GateinData.GI_REC_TYPE) = DBNull.Value
                End If
                If bv_strGI_EXPORTED <> Nothing Then
                    .Item(GateinData.GI_EXPORTED) = bv_strGI_EXPORTED
                Else
                    .Item(GateinData.GI_EXPORTED) = DBNull.Value
                End If
                If bv_strGI_EXPOR_DATE <> Nothing Then
                    .Item(GateinData.GI_EXPOR_DATE) = bv_strGI_EXPOR_DATE
                Else
                    .Item(GateinData.GI_EXPOR_DATE) = DBNull.Value
                End If
                If bv_strGI_IMPORTED <> Nothing Then
                    .Item(GateinData.GI_IMPORTED) = bv_strGI_IMPORTED
                Else
                    .Item(GateinData.GI_IMPORTED) = DBNull.Value
                End If
                If bv_strGI_IMPOR_DATE <> Nothing Then
                    .Item(GateinData.GI_IMPOR_DATE) = bv_strGI_IMPOR_DATE
                Else
                    .Item(GateinData.GI_IMPOR_DATE) = DBNull.Value
                End If
                .Item(GateinData.GI_TRNSXN) = bv_strGI_TRNSXN
                .Item(GateinData.GI_ADVICE) = bv_strGI_ADVICE
                .Item(GateinData.GI_UNIT_NBR) = bv_strGI_UNIT_NBR
                .Item(GateinData.GI_EQUIP_TYPE) = bv_strGI_EQUIP_TYPE
                If bv_strGI_EQUIP_DESC <> Nothing Then
                    .Item(GateinData.GI_EQUIP_DESC) = bv_strGI_EQUIP_DESC
                Else
                    .Item(GateinData.GI_EQUIP_DESC) = DBNull.Value
                End If
                .Item(GateinData.GI_EQUIP_CODE) = bv_strGI_EQUIP_CODE
                If bv_strGI_CONDITION <> Nothing Then
                    .Item(GateinData.GI_CONDITION) = bv_strGI_CONDITION
                Else
                    .Item(GateinData.GI_CONDITION) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_A <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_A) = bv_strGI_COMP_ID_A
                Else
                    .Item(GateinData.GI_COMP_ID_A) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_N <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_N) = bv_strGI_COMP_ID_N
                Else
                    .Item(GateinData.GI_COMP_ID_N) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_C <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_C) = bv_strGI_COMP_ID_C
                Else
                    .Item(GateinData.GI_COMP_ID_C) = DBNull.Value
                End If
                If bv_strGI_COMP_TYPE <> Nothing Then
                    .Item(GateinData.GI_COMP_TYPE) = bv_strGI_COMP_TYPE
                Else
                    .Item(GateinData.GI_COMP_TYPE) = DBNull.Value
                End If
                If bv_strGI_COMP_DESC <> Nothing Then
                    .Item(GateinData.GI_COMP_DESC) = bv_strGI_COMP_DESC
                Else
                    .Item(GateinData.GI_COMP_DESC) = DBNull.Value
                End If
                If bv_strGI_COMP_CODE <> Nothing Then
                    .Item(GateinData.GI_COMP_CODE) = bv_strGI_COMP_CODE
                Else
                    .Item(GateinData.GI_COMP_CODE) = DBNull.Value
                End If
                .Item(GateinData.GI_EIR_DATE) = bv_datGI_EIR_DATE
                If bv_strGI_EIR_TIME <> Nothing Then
                    .Item(GateinData.GI_EIR_TIME) = bv_strGI_EIR_TIME
                Else
                    .Item(GateinData.GI_EIR_TIME) = DBNull.Value
                End If
                If bv_strGI_REFERENCE <> Nothing Then
                    .Item(GateinData.GI_REFERENCE) = bv_strGI_REFERENCE
                Else
                    .Item(GateinData.GI_REFERENCE) = DBNull.Value
                End If
                If bv_strGI_MANU_DATE <> Nothing Then
                    .Item(GateinData.GI_MANU_DATE) = bv_strGI_MANU_DATE
                Else
                    .Item(GateinData.GI_MANU_DATE) = DBNull.Value
                End If
                If bv_strGI_MATERIAL <> Nothing Then
                    .Item(GateinData.GI_MATERIAL) = bv_strGI_MATERIAL
                Else
                    .Item(GateinData.GI_MATERIAL) = DBNull.Value
                End If
                If bv_strGI_WEIGHT <> Nothing Then
                    .Item(GateinData.GI_WEIGHT) = bv_strGI_WEIGHT
                Else
                    .Item(GateinData.GI_WEIGHT) = DBNull.Value
                End If
                If bv_strGI_MEASURE <> Nothing Then
                    .Item(GateinData.GI_MEASURE) = bv_strGI_MEASURE
                Else
                    .Item(GateinData.GI_MEASURE) = DBNull.Value
                End If
                If bv_strGI_UNITS <> Nothing Then
                    .Item(GateinData.GI_UNITS) = bv_strGI_UNITS
                Else
                    .Item(GateinData.GI_UNITS) = DBNull.Value
                End If
                If bv_strGI_CSC_REEXAM <> Nothing Then
                    .Item(GateinData.GI_CSC_REEXAM) = bv_strGI_CSC_REEXAM
                Else
                    .Item(GateinData.GI_CSC_REEXAM) = DBNull.Value
                End If
                If bv_strGI_COUNTRY <> Nothing Then
                    .Item(GateinData.GI_COUNTRY) = bv_strGI_COUNTRY
                Else
                    .Item(GateinData.GI_COUNTRY) = DBNull.Value
                End If
                If bv_strGI_LIC_STATE <> Nothing Then
                    .Item(GateinData.GI_LIC_STATE) = bv_strGI_LIC_STATE
                Else
                    .Item(GateinData.GI_LIC_STATE) = DBNull.Value
                End If
                If bv_strGI_LIC_REG <> Nothing Then
                    .Item(GateinData.GI_LIC_REG) = bv_strGI_LIC_REG
                Else
                    .Item(GateinData.GI_LIC_REG) = DBNull.Value
                End If
                If bv_strGI_LIC_EXPIRE <> Nothing Then
                    .Item(GateinData.GI_LIC_EXPIRE) = bv_strGI_LIC_EXPIRE
                Else
                    .Item(GateinData.GI_LIC_EXPIRE) = DBNull.Value
                End If
                .Item(GateinData.GI_LSR_OWNER) = bv_strGI_LSR_OWNER
                If bv_strGI_SEND_EDI_1 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_1) = bv_strGI_SEND_EDI_1
                Else
                    .Item(GateinData.GI_SEND_EDI_1) = DBNull.Value
                End If
                If bv_strGI_SSL_LSE <> Nothing Then
                    .Item(GateinData.GI_SSL_LSE) = bv_strGI_SSL_LSE
                Else
                    .Item(GateinData.GI_SSL_LSE) = DBNull.Value
                End If
                If bv_strGI_SEND_EDI_2 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_2) = bv_strGI_SEND_EDI_2
                Else
                    .Item(GateinData.GI_SEND_EDI_2) = DBNull.Value
                End If
                If bv_strGI_HAULIER <> Nothing Then
                    .Item(GateinData.GI_HAULIER) = bv_strGI_HAULIER
                Else
                    .Item(GateinData.GI_HAULIER) = DBNull.Value
                End If
                If bv_strGI_SEND_EDI_3 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_3) = bv_strGI_SEND_EDI_3
                Else
                    .Item(GateinData.GI_SEND_EDI_3) = DBNull.Value
                End If
                .Item(GateinData.GI_DPT_TRM) = bv_strGI_DPT_TRM
                If bv_strGI_SEND_EDI_4 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_4) = bv_strGI_SEND_EDI_4
                Else
                    .Item(GateinData.GI_SEND_EDI_4) = DBNull.Value
                End If
                If bv_strGI_OTHERS_1 <> Nothing Then
                    .Item(GateinData.GI_OTHERS_1) = bv_strGI_OTHERS_1
                Else
                    .Item(GateinData.GI_OTHERS_1) = DBNull.Value
                End If
                If bv_strGI_OTHERS_2 <> Nothing Then
                    .Item(GateinData.GI_OTHERS_2) = bv_strGI_OTHERS_2
                Else
                    .Item(GateinData.GI_OTHERS_2) = DBNull.Value
                End If
                If bv_strGI_OTHERS_3 <> Nothing Then
                    .Item(GateinData.GI_OTHERS_3) = bv_strGI_OTHERS_3
                Else
                    .Item(GateinData.GI_OTHERS_3) = DBNull.Value
                End If
                If bv_strGI_OTHERS_4 <> Nothing Then
                    .Item(GateinData.GI_OTHERS_4) = bv_strGI_OTHERS_4
                Else
                    .Item(GateinData.GI_OTHERS_4) = DBNull.Value
                End If
                If bv_strGI_NOTE_1 <> Nothing Then
                    .Item(GateinData.GI_NOTE_1) = bv_strGI_NOTE_1
                Else
                    .Item(GateinData.GI_NOTE_1) = DBNull.Value
                End If
                If bv_strGI_NOTE_2 <> Nothing Then
                    .Item(GateinData.GI_NOTE_2) = bv_strGI_NOTE_2
                Else
                    .Item(GateinData.GI_NOTE_2) = DBNull.Value
                End If
                If bv_strGI_LOAD <> Nothing Then
                    .Item(GateinData.GI_LOAD) = bv_strGI_LOAD
                Else
                    .Item(GateinData.GI_LOAD) = DBNull.Value
                End If
                If bv_strGI_FHWA <> Nothing Then
                    .Item(GateinData.GI_FHWA) = bv_strGI_FHWA
                Else
                    .Item(GateinData.GI_FHWA) = DBNull.Value
                End If
                If bv_strGI_LAST_OH_LOC <> Nothing Then
                    .Item(GateinData.GI_LAST_OH_LOC) = bv_strGI_LAST_OH_LOC
                Else
                    .Item(GateinData.GI_LAST_OH_LOC) = DBNull.Value
                End If
                If bv_strGI_LAST_OH_DATE <> Nothing Then
                    .Item(GateinData.GI_LAST_OH_DATE) = bv_strGI_LAST_OH_DATE
                Else
                    .Item(GateinData.GI_LAST_OH_DATE) = DBNull.Value
                End If
                If bv_strGI_SENDER <> Nothing Then
                    .Item(GateinData.GI_SENDER) = bv_strGI_SENDER
                Else
                    .Item(GateinData.GI_SENDER) = DBNull.Value
                End If
                If bv_strGI_ATTENTION <> Nothing Then
                    .Item(GateinData.GI_ATTENTION) = bv_strGI_ATTENTION
                Else
                    .Item(GateinData.GI_ATTENTION) = DBNull.Value
                End If
                If bv_strGI_REVISION <> Nothing Then
                    .Item(GateinData.GI_REVISION) = bv_strGI_REVISION
                Else
                    .Item(GateinData.GI_REVISION) = DBNull.Value
                End If
                If bv_strGI_SEND_EDI_5 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_5) = bv_strGI_SEND_EDI_5
                Else
                    .Item(GateinData.GI_SEND_EDI_5) = DBNull.Value
                End If
                If bv_strGI_SEND_EDI_6 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_6) = bv_strGI_SEND_EDI_6
                Else
                    .Item(GateinData.GI_SEND_EDI_6) = DBNull.Value
                End If
                If bv_strGI_SEND_EDI_7 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_7) = bv_strGI_SEND_EDI_7
                Else
                    .Item(GateinData.GI_SEND_EDI_7) = DBNull.Value
                End If
                If bv_strGI_SEND_EDI_8 <> Nothing Then
                    .Item(GateinData.GI_SEND_EDI_8) = bv_strGI_SEND_EDI_8
                Else
                    .Item(GateinData.GI_SEND_EDI_8) = DBNull.Value
                End If
                If bv_strGI_SEAL_PARTY_1 <> Nothing Then
                    .Item(GateinData.GI_SEAL_PARTY_1) = bv_strGI_SEAL_PARTY_1
                Else
                    .Item(GateinData.GI_SEAL_PARTY_1) = DBNull.Value
                End If
                If bv_strGI_SEAL_NUMBER_1 <> Nothing Then
                    .Item(GateinData.GI_SEAL_NUMBER_1) = bv_strGI_SEAL_NUMBER_1
                Else
                    .Item(GateinData.GI_SEAL_NUMBER_1) = DBNull.Value
                End If
                If bv_strGI_SEAL_PARTY_2 <> Nothing Then
                    .Item(GateinData.GI_SEAL_PARTY_2) = bv_strGI_SEAL_PARTY_2
                Else
                    .Item(GateinData.GI_SEAL_PARTY_2) = DBNull.Value
                End If
                If bv_strGI_SEAL_NUMBER_2 <> Nothing Then
                    .Item(GateinData.GI_SEAL_NUMBER_2) = bv_strGI_SEAL_NUMBER_2
                Else
                    .Item(GateinData.GI_SEAL_NUMBER_2) = DBNull.Value
                End If
                If bv_strGI_SEAL_PARTY_3 <> Nothing Then
                    .Item(GateinData.GI_SEAL_PARTY_3) = bv_strGI_SEAL_PARTY_3
                Else
                    .Item(GateinData.GI_SEAL_PARTY_3) = DBNull.Value
                End If
                If bv_strGI_SEAL_NUMBER_3 <> Nothing Then
                    .Item(GateinData.GI_SEAL_NUMBER_3) = bv_strGI_SEAL_NUMBER_3
                Else
                    .Item(GateinData.GI_SEAL_NUMBER_3) = DBNull.Value
                End If
                If bv_strGI_SEAL_PARTY_4 <> Nothing Then
                    .Item(GateinData.GI_SEAL_PARTY_4) = bv_strGI_SEAL_PARTY_4
                Else
                    .Item(GateinData.GI_SEAL_PARTY_4) = DBNull.Value
                End If
                If bv_strGI_SEAL_NUMBER_4 <> Nothing Then
                    .Item(GateinData.GI_SEAL_NUMBER_4) = bv_strGI_SEAL_NUMBER_4
                Else
                    .Item(GateinData.GI_SEAL_NUMBER_4) = DBNull.Value
                End If
                If bv_strGI_PORT_FUNC_CODE <> Nothing Then
                    .Item(GateinData.GI_PORT_FUNC_CODE) = bv_strGI_PORT_FUNC_CODE
                Else
                    .Item(GateinData.GI_PORT_FUNC_CODE) = DBNull.Value
                End If
                If bv_strGI_PORT_NAME <> Nothing Then
                    .Item(GateinData.GI_PORT_NAME) = bv_strGI_PORT_NAME
                Else
                    .Item(GateinData.GI_PORT_NAME) = DBNull.Value
                End If
                If bv_strGI_VESSEL_NAME <> Nothing Then
                    .Item(GateinData.GI_VESSEL_NAME) = bv_strGI_VESSEL_NAME
                Else
                    .Item(GateinData.GI_VESSEL_NAME) = DBNull.Value
                End If
                If bv_strGI_VOYAGE_NUM <> Nothing Then
                    .Item(GateinData.GI_VOYAGE_NUM) = bv_strGI_VOYAGE_NUM
                Else
                    .Item(GateinData.GI_VOYAGE_NUM) = DBNull.Value
                End If
                If bv_strGI_HAZ_MAT_CODE <> Nothing Then
                    .Item(GateinData.GI_HAZ_MAT_CODE) = bv_strGI_HAZ_MAT_CODE
                Else
                    .Item(GateinData.GI_HAZ_MAT_CODE) = DBNull.Value
                End If
                If bv_strGI_HAZ_MAT_DESC <> Nothing Then
                    .Item(GateinData.GI_HAZ_MAT_DESC) = bv_strGI_HAZ_MAT_DESC
                Else
                    .Item(GateinData.GI_HAZ_MAT_DESC) = DBNull.Value
                End If
                If bv_strGI_NOTE_3 <> Nothing Then
                    .Item(GateinData.GI_NOTE_3) = bv_strGI_NOTE_3
                Else
                    .Item(GateinData.GI_NOTE_3) = DBNull.Value
                End If
                If bv_strGI_NOTE_4 <> Nothing Then
                    .Item(GateinData.GI_NOTE_4) = bv_strGI_NOTE_4
                Else
                    .Item(GateinData.GI_NOTE_4) = DBNull.Value
                End If
                If bv_strGI_NOTE_5 <> Nothing Then
                    .Item(GateinData.GI_NOTE_5) = bv_strGI_NOTE_5
                Else
                    .Item(GateinData.GI_NOTE_5) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_A_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_A_2) = bv_strGI_COMP_ID_A_2
                Else
                    .Item(GateinData.GI_COMP_ID_A_2) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_N_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_N_2) = bv_strGI_COMP_ID_N_2
                Else
                    .Item(GateinData.GI_COMP_ID_N_2) = DBNull.Value
                End If
                If bv_strGI_COMP_ID_C_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_ID_C_2) = bv_strGI_COMP_ID_C_2
                Else
                    .Item(GateinData.GI_COMP_ID_C_2) = DBNull.Value
                End If
                If bv_strGI_COMP_TYPE_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_TYPE_2) = bv_strGI_COMP_TYPE_2
                Else
                    .Item(GateinData.GI_COMP_TYPE_2) = DBNull.Value
                End If
                If bv_strGI_COMP_CODE_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_CODE_2) = bv_strGI_COMP_CODE_2
                Else
                    .Item(GateinData.GI_COMP_CODE_2) = DBNull.Value
                End If
                If bv_strGI_COMP_DESC_2 <> Nothing Then
                    .Item(GateinData.GI_COMP_DESC_2) = bv_strGI_COMP_DESC_2
                Else
                    .Item(GateinData.GI_COMP_DESC_2) = DBNull.Value
                End If
                If bv_strGI_SHIPPER <> Nothing Then
                    .Item(GateinData.GI_SHIPPER) = bv_strGI_SHIPPER
                Else
                    .Item(GateinData.GI_SHIPPER) = DBNull.Value
                End If
                If bv_strGI_DRAY_ORDER <> Nothing Then
                    .Item(GateinData.GI_DRAY_ORDER) = bv_strGI_DRAY_ORDER
                Else
                    .Item(GateinData.GI_DRAY_ORDER) = DBNull.Value
                End If
                If bv_strGI_RAIL_ID <> Nothing Then
                    .Item(GateinData.GI_RAIL_ID) = bv_strGI_RAIL_ID
                Else
                    .Item(GateinData.GI_RAIL_ID) = DBNull.Value
                End If
                If bv_strGI_RAIL_RAMP <> Nothing Then
                    .Item(GateinData.GI_RAIL_RAMP) = bv_strGI_RAIL_RAMP
                Else
                    .Item(GateinData.GI_RAIL_RAMP) = DBNull.Value
                End If
                If bv_strGI_VESSEL_CODE <> Nothing Then
                    .Item(GateinData.GI_VESSEL_CODE) = bv_strGI_VESSEL_CODE
                Else
                    .Item(GateinData.GI_VESSEL_CODE) = DBNull.Value
                End If
                If bv_strGI_WGHT_CERT_1 <> Nothing Then
                    .Item(GateinData.GI_WGHT_CERT_1) = bv_strGI_WGHT_CERT_1
                Else
                    .Item(GateinData.GI_WGHT_CERT_1) = DBNull.Value
                End If
                If bv_strGI_WGHT_CERT_2 <> Nothing Then
                    .Item(GateinData.GI_WGHT_CERT_2) = bv_strGI_WGHT_CERT_2
                Else
                    .Item(GateinData.GI_WGHT_CERT_2) = DBNull.Value
                End If
                If bv_strGI_WGHT_CERT_3 <> Nothing Then
                    .Item(GateinData.GI_WGHT_CERT_3) = bv_strGI_WGHT_CERT_3
                Else
                    .Item(GateinData.GI_WGHT_CERT_3) = DBNull.Value
                End If
                If bv_strGI_SEA_RAIL <> Nothing Then
                    .Item(GateinData.GI_SEA_RAIL) = bv_strGI_SEA_RAIL
                Else
                    .Item(GateinData.GI_SEA_RAIL) = DBNull.Value
                End If
                If bv_strGI_LOC_IDENT <> Nothing Then
                    .Item(GateinData.GI_LOC_IDENT) = bv_strGI_LOC_IDENT
                Else
                    .Item(GateinData.GI_LOC_IDENT) = DBNull.Value
                End If
                If bv_strGI_PORT_LOC_QUAL <> Nothing Then
                    .Item(GateinData.GI_PORT_LOC_QUAL) = bv_strGI_PORT_LOC_QUAL
                Else
                    .Item(GateinData.GI_PORT_LOC_QUAL) = DBNull.Value
                End If
                If bv_strGI_STATUS <> Nothing Then
                    .Item(GateinData.GI_STATUS) = bv_strGI_STATUS
                Else
                    .Item(GateinData.GI_STATUS) = DBNull.Value
                End If
                If bv_datGI_PICK_DATE <> Nothing Then
                    .Item(GateinData.GI_PICK_DATE) = bv_datGI_PICK_DATE
                Else
                    .Item(GateinData.GI_PICK_DATE) = DBNull.Value
                End If
                If bv_strGI_ESTSTATUS <> Nothing Then
                    .Item(GateinData.GI_ESTSTATUS) = bv_strGI_ESTSTATUS
                Else
                    .Item(GateinData.GI_ESTSTATUS) = DBNull.Value
                End If
                If bv_strGI_ERRSTATUS <> Nothing Then
                    .Item(GateinData.GI_ERRSTATUS) = bv_strGI_ERRSTATUS
                Else
                    .Item(GateinData.GI_ERRSTATUS) = DBNull.Value
                End If
                If bv_strGI_USERNAME <> Nothing Then
                    .Item(GateinData.GI_USERNAME) = bv_strGI_USERNAME
                Else
                    .Item(GateinData.GI_USERNAME) = DBNull.Value
                End If
                If bv_i32GI_LIVE_STATUS <> 0 Then
                    .Item(GateinData.GI_LIVE_STATUS) = bv_i32GI_LIVE_STATUS
                Else
                    .Item(GateinData.GI_LIVE_STATUS) = DBNull.Value
                End If
                If bv_blnGI_ISACTIVE <> Nothing Then
                    .Item(GateinData.GI_ISACTIVE) = bv_blnGI_ISACTIVE
                Else
                    .Item(GateinData.GI_ISACTIVE) = DBNull.Value
                End If
                If bv_strGI_YARD_LOC <> Nothing Then
                    .Item(GateinData.GI_YARD_LOC) = bv_strGI_YARD_LOC
                Else
                    .Item(GateinData.GI_YARD_LOC) = DBNull.Value
                End If
                If bv_strGI_MODE_PAYMENT <> Nothing Then
                    .Item(GateinData.GI_MODE_PAYMENT) = bv_strGI_MODE_PAYMENT
                Else
                    .Item(GateinData.GI_MODE_PAYMENT) = DBNull.Value
                End If
                If bv_strGI_BILLING_TYPE <> Nothing Then
                    .Item(GateinData.GI_BILLING_TYPE) = bv_strGI_BILLING_TYPE
                Else
                    .Item(GateinData.GI_BILLING_TYPE) = DBNull.Value
                End If
                If bv_blnGI_RESERVE_BKG <> Nothing Then
                    .Item(GateinData.GI_RESERVE_BKG) = bv_blnGI_RESERVE_BKG
                Else
                    .Item(GateinData.GI_RESERVE_BKG) = DBNull.Value
                End If
                If bv_strGI_RCESTSTATUS <> Nothing Then
                    .Item(GateinData.GI_RCESTSTATUS) = bv_strGI_RCESTSTATUS
                Else
                    .Item(GateinData.GI_RCESTSTATUS) = DBNull.Value
                End If
                If bv_i32OP_SNO <> 0 Then
                    .Item(GateinData.OP_SNO) = bv_i32OP_SNO
                Else
                    .Item(GateinData.OP_SNO) = DBNull.Value
                End If
                If bv_blnOP_STATUS <> Nothing Then
                    .Item(GateinData.OP_STATUS) = bv_blnOP_STATUS
                Else
                    .Item(GateinData.OP_STATUS) = DBNull.Value
                End If
            End With
            UpdateGateinRet = objData.UpdateRow(dr, Gatein_RetUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentInfo"
    Public Function GetEquipmentInfo(ByVal bv_strEquipmentNo As String, _
                                     ByVal bv_intDepotID As Integer, _
                                     ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(GateinData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(GateinData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(Equipment_InfoCountQuery, hshTable)
            Return objData.ExecuteScalar(br_ObjTransactions)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateEquipment_Info"
    Public Function UpdateEquipment_Info(ByVal bv_strEquipmentNo As String, _
                                     ByVal bv_intEquipmentTypeID As Int32, _
                                     ByVal bv_intDepotID As Integer, _
                                      ByVal bv_strRemarks As String, _
                                     ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._PRE_ADVICE).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateinData.EQPMNT_TYP_ID) = bv_intEquipmentTypeID
                .Item(GateinData.DPT_ID) = bv_intDepotID

                If bv_strRemarks <> Nothing Then
                    .Item(GateinData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(GateinData.RMRKS_VC) = DBNull.Value
                End If

            End With
            UpdateEquipment_Info = objData.UpdateRow(dr, Equipment_InfoUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "UpdateTracking_PreAdvice"
    Public Function UpdateTracking_PreAdvice(ByVal bv_strGateInTransactionNo As String, _
                                     ByVal bv_strEquipmentNo As String, _
                                     ByVal bv_strActivityName As String, _
                                     ByVal bv_intDepotID As Integer, _
                                     ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._TRACKING).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateinData.DPT_ID) = bv_intDepotID
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(GateinData.ACTVTY_NAM) = bv_strActivityName
            End With

            UpdateTracking_PreAdvice = objData.UpdateRow(dr, Tracking_Pre_AdviceUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "GetHanldingInCharge"
    Public Function GetHanldingInCharge(ByVal bv_intCustomerID As Integer, _
                                        ByVal bv_intCodeID As Integer, _
                                        ByVal bv_intTypeID As Integer, _
                                        ByVal bv_intDepotID As Integer, _
                                        ByRef br_ObjTransactions As Transactions) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dthandlingCharge As New DataTable
            hshTable.Add(GateinData.CSTMR_ID, bv_intCustomerID)
            hshTable.Add(GateinData.EQPMNT_CD_ID, bv_intCodeID)
            hshTable.Add(GateinData.EQPMNT_TYP_ID, bv_intTypeID)
            hshTable.Add(GateinData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(CutomerChargeDetailSelectQuery, hshTable)
            objData.Fill(dthandlingCharge, br_ObjTransactions)
            Return dthandlingCharge
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAgentHanldingInCharge(ByVal bv_intAgentID As String, _
                                        ByVal bv_intCodeID As Integer, _
                                        ByVal bv_intTypeID As Integer, _
                                        ByVal bv_intDepotID As Integer, _
                                        ByRef br_ObjTransactions As Transactions) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dthandlingCharge As New DataTable
            hshTable.Add(GateinData.AGNT_ID, bv_intAgentID)
            hshTable.Add(GateinData.EQPMNT_CD_ID, bv_intCodeID)
            hshTable.Add(GateinData.EQPMNT_TYP_ID, bv_intTypeID)
            hshTable.Add(GateinData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(GetAgentHanldingInCharge_SelectQry, hshTable)
            objData.Fill(dthandlingCharge, br_ObjTransactions)
            Return dthandlingCharge
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteHeating_Charge() TABLE NAME:Heating_Charge"

    Public Function DeleteHeating_Charge(ByVal bv_EquipmentNo As String, _
                                        ByVal bv_strGateInTransactionNo As String, _
                                        ByVal bv_DPT_ID As Integer, _
                                        ByRef br_objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(GateinData._HEATING_CHARGE).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_EquipmentNo
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(GateinData.DPT_ID) = bv_DPT_ID
            End With
            DeleteHeating_Charge = objData.DeleteRow(dr, Heating_ChargeDeleteQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateActivityStatus() TABLE NAME:Activity_Status"
    Public Function UpdateActivityStatus(ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_i64EquipmentStatusId As Int64, _
                                         ByVal bv_strActivity As String, _
                                         ByVal bv_datActivityDate As DateTime, _
                                         ByVal bv_avtivity_bit As Boolean, _
                                         ByVal bv_strGateInTransactionNo As String, _
                                         ByVal bv_GI_Ref_no As String, _
                                         ByVal bv_i32DepoID As Int32, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strYardLocation As String, _
                                         ByVal bv_i64ProductID As Int64, _
                                         ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateinData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                .Item(GateinData.ACTVTY_NAM) = bv_strActivity
                .Item(GateinData.ACTVTY_DT) = bv_datActivityDate
                .Item(GateinData.GTN_DT) = bv_datActivityDate
                .Item(GateinData.ACTV_BT) = bv_avtivity_bit
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(GateinData.GI_RF_NO) = bv_GI_Ref_no
                If bv_strRemarks <> Nothing Then
                    .Item(GateinData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(GateinData.RMRKS_VC) = DBNull.Value
                End If
                If bv_strYardLocation <> Nothing Then
                    .Item(GateinData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(GateinData.YRD_LCTN) = DBNull.Value
                End If
                .Item(GateinData.DPT_ID) = bv_i32DepoID
                .Item(GateinData.PRDCT_ID) = bv_i64ProductID
            End With
            UpdateActivityStatus = objData.UpdateRow(dr, Activity_StatusUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCountryDetails"
    Public Function GetCountryDetails(ByVal bv_intDepotId As Integer) As GateinDataSet
        Try
            objData = New DataObjects(CountryDetailsSelectQuery, GateinData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), GateinData._COUNTRY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetMaterialDetails"
    Public Function GetMaterialDetails(ByVal bv_intDepotId As Integer) As GateinDataSet
        Try
            objData = New DataObjects(MaterialDetailsSelectQuery, GateinData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), GateinData._MATERIAL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetMeasureDetails"
    Public Function GetMeasureDetails(ByVal bv_intDepotId As Integer) As GateinDataSet
        Try
            objData = New DataObjects(MeasureDetailsSelectQuery, GateinData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), GateinData._MEASURE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getPartyDetails"
    Public Function getPartyDetails(ByVal bv_intDepotId As Integer) As GateinDataSet
        Try
            objData = New DataObjects(PartyDetailsSelectQuery, GateinData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), GateinData._INVOICING_PARTY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetUnitDetails"
    Public Function GetUnitDetails(ByVal bv_intDepotId As Integer) As GateinDataSet
        Try
            objData = New DataObjects(UnitDetailsSelectQuery, GateinData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), GateinData._UNIT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRentalDetails() TABLE NAME:GATEIN"

    Public Function GetRentalDetails(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As GateinDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(GateinData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(GateinData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(RentalSelectQueryByDepotId, hshParameters)
            objData.Fill(CType(ds, DataSet), GateinData._V_RENTAL_ENTRY)
            'Return objData.ExecuteScalar()
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRentalEntryDetails() "

    Public Function GetRentalEntryDetails(ByVal bv_strEquipmentNo As String, ByVal bv_cstmrID As Int64, ByVal bv_i32DepotID As Int32, ByRef br_objTrans As Transactions) As DataTable
        Try
            Dim hshParameters As New Hashtable()
            Dim dtRental As New DataTable
            hshParameters.Add(GateinData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(GateinData.CSTMR_ID, bv_cstmrID)
            hshParameters.Add(GateinData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(RentalEntrySelectQueryByDepotId, hshParameters)
            objData.Fill(dtRental, br_objTrans)
            'Return objData.ExecuteScalar()
            Return dtRental
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateRentalEntry"
    Public Function UpdateRentalEntry(ByVal bv_strEquipmentNo As String, _
                                     ByVal bv_CsrmrID As Int64, _
                                     ByVal bv_datGateINDate As DateTime, _
                                     ByVal bv_StrRentalRefNo As String, _
                                     ByVal bv_intDepotID As Integer, _
                                     ByVal bv_blnGateInBit As Boolean, _
                                     ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._RENTAL_ENTRY).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateinData.CSTMR_ID) = bv_CsrmrID
                .Item(GateinData.OFF_HR_DT) = bv_datGateINDate
                .Item(GateinData.RNTL_RFRNC_NO) = bv_StrRentalRefNo
                .Item(GateinData.DPT_ID) = bv_intDepotID
                .Item(GateinData.GTN_BT) = bv_blnGateInBit
            End With
            UpdateRentalEntry = objData.UpdateRow(dr, RentalEntryUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "UpdateRentalCharge()"
    Public Function UpdateRentalCharge(ByVal bv_strEquipmentNo As String, _
        ByVal bv_datGateINDate As DateTime, _
        ByVal bv_YardLocation As String, _
        ByVal bv_RentalRefNo As String, _
        ByVal bv_dptID As Integer, _
        ByVal bv_decOffHireCharge As Decimal, _
        ByVal bv_decHndlingInCharge As Decimal, _
        ByVal dblOtherCharge As Decimal, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._RENTAL_CHARGE).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateinData.TO_BLLNG_DT) = bv_datGateINDate
                If bv_YardLocation <> Nothing Then
                    .Item(GateinData.YRD_LCTN) = bv_YardLocation
                Else
                    .Item(GateinData.YRD_LCTN) = DBNull.Value
                End If
                .Item(GateinData.RNTL_RFRNC_NO) = bv_RentalRefNo
                .Item(GateinData.DPT_ID) = bv_dptID
                .Item(GateinData.OFF_HR_SRVY_NC) = bv_decOffHireCharge
                .Item(GateinData.HNDLNG_IN_NC) = bv_decHndlingInCharge
                .Item(GateinData.OTHR_CHRG_NC) = dblOtherCharge
            End With
            UpdateRentalCharge = objData.UpdateRow(dr, RentalChargeUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRentalEntry() TABLE NAME:RENTAL_ENTRY"

    Public Function GetRentalEntry(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32, ByRef br_GateOutBit As Boolean) As GateinDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(GateinData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(GateinData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(RentalEntryValidateQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), GateinData._V_RENTAL_ENTRY)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRentalCustomer() "
    Public Function GetRentalCustomer(ByVal bv_strEquipmentNo As String, ByVal bv_intCstmrID As Integer, ByRef br_ObjTransactions As Transactions) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dtCustomer As New DataTable
            hshTable.Add(GateOutData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(GateOutData.CSTMR_ID, bv_intCstmrID)
            objData = New DataObjects(RentalCustomerSelectQuery, hshTable)
            objData.Fill(dtCustomer, br_ObjTransactions)
            Return dtCustomer
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateRentalChargeDetail()"
    Public Function UpdateRentalChargeDetail(ByVal bv_strEquipmentNo As String, _
        ByVal bv_datGateINDate As DateTime, _
        ByVal bv_YardLocation As String, _
        ByVal bv_RentalRefNo As String, _
        ByVal bv_dptID As Integer, _
        ByVal bv_decOffHireCharge As Decimal, _
        ByVal bv_decHndlingInCharge As Decimal, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._RENTAL_CHARGE).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateinData.TO_BLLNG_DT) = bv_datGateINDate
                If bv_YardLocation <> Nothing Then
                    .Item(GateinData.YRD_LCTN) = bv_YardLocation
                Else
                    .Item(GateinData.YRD_LCTN) = DBNull.Value
                End If
                .Item(GateinData.RNTL_RFRNC_NO) = bv_RentalRefNo
                .Item(GateinData.DPT_ID) = bv_dptID
                .Item(GateinData.OFF_HR_SRVY_NC) = bv_decOffHireCharge
                .Item(GateinData.HNDLNG_IN_NC) = bv_decHndlingInCharge
            End With
            UpdateRentalChargeDetail = objData.UpdateRow(dr, RentalChargeDetailUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetSupplierDetails() TABLE NAME:SUPPLIER_EQUIPMENT_DETAIL"

    Public Function GetSupplierDetails(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As GateinDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(GateinData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(GateinData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(GateoutRentalDetailsByEqpmntNo, hshParameters)
            objData.Fill(CType(ds, DataSet), GateinData._V_GATEOUT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    '#Region "CreateEquipmentInformationDetail() TABLE NAME:EQUIPMENT_INFORMATION_DETAIL"

    '    Public Function CreateEquipmentInformationDetail(ByVal bv_lngEquipmentInformationId As Int64, _
    '                                                     ByVal bv_strAttachmentPath As String, _
    '                                                     ByVal bv_strActualFileName As String, _
    '                                                     ByRef br_objTrans As Transactions) As Long
    '        Try
    '            Dim dr As DataRow
    '            Dim intMax As Long
    '            objData = New DataObjects()
    '            dr = ds.Tables(GateinData._EQUIPMENT_INFORMATION_DETAIL).NewRow()
    '            With dr
    '                intMax = CommonUIs.GetIdentityValue(GateinData._EQUIPMENT_INFORMATION_DETAIL, br_objTrans)
    '                .Item(GateinData.EQPMNT_INFRMTN_DTL_ID) = intMax
    '                .Item(GateinData.EQPMNT_INFRMTN_ID) = bv_lngEquipmentInformationId
    '                .Item(GateinData.ATTCHMNT_PTH) = bv_strAttachmentPath
    '                .Item(GateinData.ACTL_FL_NM) = bv_strActualFileName
    '            End With
    '            objData.InsertRow(dr, Equipment_Information_DetailInsertQuery, br_objTrans)
    '            dr = Nothing
    '            CreateEquipmentInformationDetail = intMax
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '#End Region

    '#Region "DeleteEquipmentInformationDetail() Table Name: EQUIPMENT_INFORMATION_DETAIL"

    '    Public Function DeleteEquipmentInformationDetail(ByVal bv_strEquipmentNo As String, _
    '                                                     ByVal bv_intDepotId As Int32, _
    '                                                     ByRef br_objTransaction As Transactions) As Boolean
    '        Dim dr As DataRow
    '        objData = New DataObjects()
    '        Try
    '            dr = ds.Tables(GateinData._EQUIPMENT_INFORMATION).NewRow()
    '            With dr
    '                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
    '                .Item(GateinData.DPT_ID) = bv_intDepotId
    '            End With
    '            DeleteEquipmentInformationDetail = objData.DeleteRow(dr, Equipment_Information_DetailDeleteQuery, br_objTransaction)
    '            dr = Nothing
    '            Return True
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function
    '#End Region

#Region "GetEquipmentInfoRemaksTracking() TABLE NAME:Tracking"

    Public Function GetEquipmentInfoRemaksTracking(ByVal strEquipmentNo As String, _
                                                   ByVal bv_intDepotID As Integer, _
                                                   ByVal bv_strActivityName As String, _
                                                   ByVal bv_strGateinTransactionNo As String, _
                                                   ByRef br_objTransaction As Transactions) As GateinDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(GateinData.EQPMNT_NO, strEquipmentNo)
            hshParameters.Add(GateinData.DPT_ID, bv_intDepotID)
            hshParameters.Add(GateinData.ACTVTY_NAM, bv_strActivityName)
            hshParameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGateinTransactionNo)
            objData = New DataObjects(TrackingEquipmentInfoRemarksSelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), GateinData._TRACKING, br_objTransaction)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetGateInLockingEquipmentByID() TABLE NAME:GATEIN"

    Public Function GetGateInLockingEquipmentByID(ByVal bv_strEquipmentNo As String, _
                                                  ByVal bv_i64CustomerId As Int64, _
                                                  ByVal bv_i32DepotID As Int32, _
                                                  ByRef br_ObjTrans As Transactions) As Boolean
        Try
            Dim hshParameters As New Hashtable()
            Dim dsGate As New GateinDataSet
            hshParameters.Add(GateinData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(GateinData.CSTMR_ID, bv_i64CustomerId)
            hshParameters.Add(GateinData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(GateinLockSelectQuery, hshParameters)
            objData.Fill(CType(dsGate, DataSet), GateinData._GATEIN, br_ObjTrans)
            If dsGate.Tables(GateinData._GATEIN).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetProductUN_NO"
    Public Function GetProductUN_NO(ByVal bv_intProductId As Integer, ByRef br_objTrans As Transactions) As GateinDataSet
        Try
            objData = New DataObjects(GetProductUNNOQuery, GateinData.PRDCT_ID, bv_intProductId)

            objData.Fill(CType(ds, DataSet), GateinData._PRODUCT, br_objTrans)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "GetCleaningDetails"
    Public Function GetCleaningDetails(ByVal bv_intEquipment As String, ByRef br_objTrans As Transactions) As GateinDataSet
        Try
            objData = New DataObjects(GetCleaningDetail, GateinData.EQPMNT_NO, bv_intEquipment)
            objData.Fill(CType(ds, DataSet), GateinData._CLEANING, br_objTrans)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentDescription() "
    Public Function GetEquipmentDescription(ByVal bv_strEquipmentTypeId As String, ByRef br_objTrans As Transactions) As String
        Try
            Dim hshTable As New Hashtable()
            Dim dtRental As New DataTable

            '  Dim strTransx As String = CommonUIs.GetIdentityCode(GateOutData._GATEOUT, bv_lngGateoutId, bv_GOdate, br_objTrans)
            objData = New DataObjects(getEquipmentCodeDescriptionQuery, GateinData.EQPMNT_TYP_ID, bv_strEquipmentTypeId)
            objData.Fill(CType(ds, DataSet), GateinData._EQUIPMENT_TYPE)
            ' Return ds
            Dim equipmentDescription As String = String.Empty

            If ds.Tables("EQUIPMENT_TYPE").Rows.Count > 0 Then
                equipmentDescription = ds.Tables("EQUIPMENT_TYPE").Rows(0).Item("EQPMNT_TYP_DSCRPTN_VC")
            End If
            Return equipmentDescription
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    'For Attchment : Getting PreAdvice
#Region "GetPreAdvice() "
    Public Function GetPreAdviceNo(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int64, ByRef br_objTrans As Transactions) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dtPreAdvice As New DataTable
            hshTable.Add(GateinData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(GateinData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(getPreAdviceNoSelectQuery, hshTable)
            objData.Fill(dtPreAdvice, br_objTrans)
            Return dtPreAdvice

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetAttchemntbyGateIN()"

    Public Function pub_GetAttchemntbyGateIN(ByVal bv_intGateInID As Integer, ByVal bv_strActivity As String) As GateinDataSet
        Try
            Dim hshTable As New Hashtable()
            Dim dtRental As New DataTable
            hshTable.Add(GateinData.RPR_ESTMT_ID, bv_intGateInID)
            hshTable.Add(GateinData.ACTVTY_NAM, bv_strActivity)
            objData = New DataObjects(GetAttachmentByGateIN, hshTable)
            objData.Fill(CType(ds, DataSet), GateinData._V_GATEIN)
            Return ds

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetAttachmentByRepairEstimateId() TABLE NAME:ATTACHMENT"

    Public Function GetAttachmentByRepairEstimateId(ByVal bv_i32DepotID As Int32, ByVal bv_i64RepairEstimateId As Int64, _
                                                    ByRef br_objTransaction As Transactions) As GateinDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(GateinData.DPT_ID, bv_i32DepotID)
            hshparameters.Add(GateinData.RPR_ESTMT_ID, bv_i64RepairEstimateId)
            objData = New DataObjects(AttachmentSelectQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), GateinData._ATTACHMENT, br_objTransaction)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAttachmentByRepairEstimateId(ByVal bv_i32DepotID As Int32, ByVal bv_strActivity As String, ByVal bv_i64RepairEstimateId As Int64, _
                                                        ByRef br_objTransaction As Transactions) As GateinDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(GateinData.DPT_ID, bv_i32DepotID)
            hshparameters.Add(GateinData.RPR_ESTMT_ID, bv_i64RepairEstimateId)
            hshparameters.Add(GateinData.ACTVTY_NAM, bv_strActivity)
            objData = New DataObjects(AttachmentSelectWithActivityQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), GateinData._ATTACHMENT, br_objTransaction)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAttachmentGateIN(ByVal bv_i32DepotID As Int32, ByVal bv_i64RepairEstimateId As Int64, _
                                                ByVal bv_strActivity As String, ByRef br_objTransaction As Transactions) As GateinDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(GateinData.DPT_ID, bv_i32DepotID)
            hshparameters.Add(GateinData.RPR_ESTMT_ID, bv_i64RepairEstimateId)
            hshparameters.Add(GateinData.ACTVTY_NAM, bv_strActivity)
            objData = New DataObjects(AttachmentSelectGateInQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), GateinData._ATTACHMENT, br_objTransaction)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetEquipmentInformation"
    Public Function pub_GetEquipmentInformation(bv_strEquipment As String) As GateinDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(GateinData.EQPMNT_NO, bv_strEquipment)
            objData = New DataObjects(GetEquipmentInformationQuery, hshTable)
            objData.Fill(CType(ds, DataSet), GateinData._EQUIPMENT_INFORMATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "ValidateStatusOfEquipment() TABLE NAME:ACTIVITY_STATUS"

    Public Function ValidateStatusOfEquipment(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentInformationData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(EquipmentInformationData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(ValidateStatusOfEquipmentQuery, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetPreAdviceEquipmentByID() TABLE NAME:INWARD_PASS"

    Public Function GetValidateEquipmentNoInPreAdvice(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(PreAdviceData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(PreAdviceData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(ValidateEquipmentNoInPreAdvice, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    Function GetAgentNameByCustmrId(bv_strCustCode As String) As String
        Dim hshTable As New Hashtable
        Dim agntID As Integer
        Dim dsAgntName As String
        hshTable.Add(GateinData.CSTMR_CD, bv_strCustCode)
        objData = New DataObjects(AgentNameByCustmrID_SelectQuery, hshTable)
        agntID = objData.ExecuteScalar()
        hshTable = New Hashtable
        hshTable.Add(AgentData.AGNT_ID, agntID)
        objData = New DataObjects("Select Top 1 AGNT_CD from Agent where AGNT_ID=@AGNT_ID", hshTable)
        dsAgntName = objData.ExecuteScalar()
        Return dsAgntName
    End Function

    Function GetAgentIDByCode(bv_strAgentCd As String) As String
        Dim hshTable As New Hashtable
        Dim dsAgntName As String
        hshTable = New Hashtable
        hshTable.Add(AgentData.AGNT_CD, bv_strAgentCd)
        objData = New DataObjects("Select AGNT_ID from Agent where AGNT_CD=@AGNT_CD", hshTable)
        dsAgntName = objData.ExecuteScalar()
        Return dsAgntName
    End Function

    Function GetMySubmitAgentIDByCode(bv_strAgentCd As String) As String
        Dim hshTable As New Hashtable
        Dim dsAgntName As String
        hshTable = New Hashtable
        hshTable.Add(AgentData.AGNT_CD, bv_strAgentCd)
        objData = New DataObjects("Select AGNT_ID from Agent where AGNT_CD=@AGNT_CD", hshTable)
        dsAgntName = objData.ExecuteScalar()
        Return dsAgntName
    End Function

    Function GetGradeIDByCode(bv_strGradeCd As String) As String
        Dim hshTable As New Hashtable
        Dim dsGradeID As String
        hshTable = New Hashtable
        hshTable.Add(GateinData.GRD_CD, bv_strGradeCd)
        objData = New DataObjects("Select GRD_ID from GRADE where GRD_CD=@GRD_CD", hshTable)
        dsGradeID = objData.ExecuteScalar()
        Return dsGradeID
    End Function
    Function GetPartyById(bv_intPartyId As Integer) As String
        Dim hshTable As New Hashtable
        Dim strPartyCode As String
        hshTable = New Hashtable
        hshTable.Add(GateinData.PRTY_ID, bv_intPartyId)
        objData = New DataObjects("Select PRTY_CD from PARTY where PRTY_ID=@PRTY_ID", hshTable)
        strPartyCode = objData.ExecuteScalar()
        Return strPartyCode
    End Function
End Class

#End Region

