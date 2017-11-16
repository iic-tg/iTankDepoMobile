Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
#Region "GateOuts"

Public Class GateOuts

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const HandlingChargeInsertQuery As String = "INSERT INTO HANDLING_CHARGE(HNDLNG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_TYP_ID,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,FR_DYS,NO_OF_DYS,HNDLNG_CST_NC,HNDLNG_TX_RT_NC,TTL_CSTS_NC,BLLNG_FLG,ACTV_BT,DPT_ID,IS_GT_OT_FLG,YRD_LCTN,BLLNG_TYP_CD,CSTMR_ID,HTNG_BT,GI_TRNSCTN_NO,AGNT_ID)VALUES(@HNDLNG_CHRG_ID,@EQPMNT_NO,@EQPMNT_CD_ID,@EQPMNT_TYP_ID,@CST_TYP,@RFRNC_EIR_NO_1,@RFRNC_EIR_NO_2,@FRM_BLLNG_DT,@TO_BLLNG_DT,@FR_DYS,@NO_OF_DYS,@HNDLNG_CST_NC,@HNDLNG_TX_RT_NC,@TTL_CSTS_NC,@BLLNG_FLG,@ACTV_BT,@DPT_ID,@IS_GT_OT_FLG,@YRD_LCTN,@BLLNG_TYP_CD,@CSTMR_ID,@HTNG_BT,@GI_TRNSCTN_NO,@AGNT_ID)"
    Private Const Gateout_RetInsertQuery As String = "INSERT INTO GATEOUT_RET(GO_SNO,GO_TRANSMISSION_NO,GO_COMPLETE,GO_SENT_EIR,GO_SENT_DATE,GO_REC_EIR,GO_REC_DATE,GO_REC_ADDR,GO_REC_TYPE,GO_EXPORTED,GO_EXPOR_DATE,GO_IMPORTED,GO_IMPOR_DATE,GO_TRNSXN,GO_ADVICE,GO_UNIT_NBR,GO_EQUIP_TYPE,GO_EQUIP_DESC,GO_EQUIP_CODE,GO_CONDITION,GO_COMP_ID_A,GO_COMP_ID_N,GO_COMP_ID_C,GO_COMP_TYPE,GO_COMP_DESC,GO_COMP_CODE,GO_EIR_DATE,GO_EIR_TIME,GO_REFERENCE,GO_MANU_DATE,GO_MATERIAL,GO_WEIGHT,GO_MEASURE,GO_UNITS,GO_CSC_REEXAM,GO_COUNTRY,GO_LIC_STATE,GO_LIC_REG,GO_LIC_EXPIRE,GO_LSR_OWNER,GO_SEND_EDI_1,GO_SSL_LSE,GO_SEND_EDI_2,GO_HAULIER,GO_SEND_EDI_3,GO_DPT_TRM,GO_SEND_EDI_4,GO_OTHERS_1,GO_OTHERS_2,GO_OTHERS_3,GO_OTHERS_4,GO_NOTE_1,GO_NOTE_2,GO_LOAD,GO_FHWA,GO_LAST_OH_LOC,GO_LAST_OH_DATE,GO_SENDER,GO_ATTENTION,GO_REVISION,GO_SEND_EDI_5,GO_SEND_EDI_6,GO_SEND_EDI_7,GO_SEND_EDI_8,GO_SEAL_PARTY_1,GO_SEAL_NUMBER_1,GO_SEAL_PARTY_2,GO_SEAL_NUMBER_2,GO_SEAL_PARTY_3,GO_SEAL_NUMBER_3,GO_SEAL_PARTY_4,GO_SEAL_NUMBER_4,GO_PORT_FUNC_CODE,GO_PORT_NAME,GO_VESSEL_NAME,GO_VOYAGE_NUM,GO_HAZ_MAT_CODE,GO_HAZ_MAT_DESC,GO_NOTE_3,GO_NOTE_4,GO_NOTE_5,GO_COMP_ID_A_2,GO_COMP_ID_N_2,GO_COMP_ID_C_2,GO_COMP_TYPE_2,GO_COMP_CODE_2,GO_COMP_DESC_2,GO_SHIPPER,GO_DRAY_ORDER,GO_RAIL_ID,GO_RAIL_RAMP,GO_VESSEL_CODE,GO_WGHT_CERT_1,GO_WGHT_CERT_2,GO_WGHT_CERT_3,GO_SEA_RAIL,GO_BILL_LADING,GO_LOC_IDENT,GO_PORT_LOC_QUAL,GO_STATUS,GO_PICK_DATE,GO_ERRSTATUS,GO_USERNAME,GO_LIVE_STATUS,GO_YARD_LOC,GO_MODE_PAYMENT,GO_BILLING_TYPE)VALUES(@GO_SNO,@GO_TRANSMISSION_NO,@GO_COMPLETE,@GO_SENT_EIR,@GO_SENT_DATE,@GO_REC_EIR,@GO_REC_DATE,@GO_REC_ADDR,@GO_REC_TYPE,@GO_EXPORTED,@GO_EXPOR_DATE,@GO_IMPORTED,@GO_IMPOR_DATE,@GO_TRNSXN,@GO_ADVICE,@GO_UNIT_NBR,@GO_EQUIP_TYPE,@GO_EQUIP_DESC,@GO_EQUIP_CODE,@GO_CONDITION,@GO_COMP_ID_A,@GO_COMP_ID_N,@GO_COMP_ID_C,@GO_COMP_TYPE,@GO_COMP_DESC,@GO_COMP_CODE,@GO_EIR_DATE,@GO_EIR_TIME,@GO_REFERENCE,@GO_MANU_DATE,@GO_MATERIAL,@GO_WEIGHT,@GO_MEASURE,@GO_UNITS,@GO_CSC_REEXAM,@GO_COUNTRY,@GO_LIC_STATE,@GO_LIC_REG,@GO_LIC_EXPIRE,@GO_LSR_OWNER,@GO_SEND_EDI_1,@GO_SSL_LSE,@GO_SEND_EDI_2,@GO_HAULIER,@GO_SEND_EDI_3,@GO_DPT_TRM,@GO_SEND_EDI_4,@GO_OTHERS_1,@GO_OTHERS_2,@GO_OTHERS_3,@GO_OTHERS_4,@GO_NOTE_1,@GO_NOTE_2,@GO_LOAD,@GO_FHWA,@GO_LAST_OH_LOC,@GO_LAST_OH_DATE,@GO_SENDER,@GO_ATTENTION,@GO_REVISION,@GO_SEND_EDI_5,@GO_SEND_EDI_6,@GO_SEND_EDI_7,@GO_SEND_EDI_8,@GO_SEAL_PARTY_1,@GO_SEAL_NUMBER_1,@GO_SEAL_PARTY_2,@GO_SEAL_NUMBER_2,@GO_SEAL_PARTY_3,@GO_SEAL_NUMBER_3,@GO_SEAL_PARTY_4,@GO_SEAL_NUMBER_4,@GO_PORT_FUNC_CODE,@GO_PORT_NAME,@GO_VESSEL_NAME,@GO_VOYAGE_NUM,@GO_HAZ_MAT_CODE,@GO_HAZ_MAT_DESC,@GO_NOTE_3,@GO_NOTE_4,@GO_NOTE_5,@GO_COMP_ID_A_2,@GO_COMP_ID_N_2,@GO_COMP_ID_C_2,@GO_COMP_TYPE_2,@GO_COMP_CODE_2,@GO_COMP_DESC_2,@GO_SHIPPER,@GO_DRAY_ORDER,@GO_RAIL_ID,@GO_RAIL_RAMP,@GO_VESSEL_CODE,@GO_WGHT_CERT_1,@GO_WGHT_CERT_2,@GO_WGHT_CERT_3,@GO_SEA_RAIL,@GO_BILL_LADING,@GO_LOC_IDENT,@GO_PORT_LOC_QUAL,@GO_STATUS,@GO_PICK_DATE,@GO_ERRSTATUS,@GO_USERNAME,@GO_LIVE_STATUS,@GO_YARD_LOC,@GO_MODE_PAYMENT,@GO_BILLING_TYPE)"
    Private Const GateinSelectQuery As String = "SELECT GTN_BIN AS GTOT_BIN,CSTMR_ID,CSTMR_CD,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,DPT_ID,GTN_TRNSXN,EIR_DT AS GTN_DT,GTN_BIN,OTWRDPSS_STTS_BT,RSRV_BKG_BT,EQPMNT_STTS_CD,EQPMNT_STTS_ID,CSTMR_NAM,EQPMNT_STTS_DSCRPTN_VC,YRD_LOC,GRD_CD  FROM V_GATEIN WHERE EQPMNT_STTS_CD IN('A','C','D')  AND (OTWRDPSS_STTS_BT IS NULL OR OTWRDPSS_STTS_BT <> 1) AND RSRV_BKG_BT IS NULL AND (RC_EST_STTS IS NULL OR RC_EST_STTS='C') AND DPT_ID=@DPT_ID ORDER BY GTN_BIN DESC"
    Private Const Equipment_Status_SelectQuery As String = "SELECT EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC FROM EQUIPMENT_STATUS WHERE ACTV_BT = 1 AND DPT_ID=@DPT_ID"
    Private Const V_GATEIN_DETAILSelectQueryBy As String = "SELECT GTN_DTL_ID,GTN_ID,GTN_CD,MNFCTR_DT,ACEP_CD,MTRL_ID,MTRL_CD,GRSS_WGHT_NC,TR_WGHT_NC,MSR_ID,MSR_CD,UNT_ID,UNT_CD,LST_OH_LOC,LST_OH_DT,TRCKR_CD,LOD_STTS_CD,CNTRY_ID,CNTRY_CD,LIC_STT,LIC_REG,LIC_EXPR,NT_1_VC,NT_2_VC,NT_3_VC,NT_4_VC,NT_5_VC,SL_PRTY_1_ID,SL_NMBR_1,SL_PRTY_1_CD,SL_PRTY_2_ID,SL_NMBR_2,SL_PRTY_2_CD,SL_PRTY_3_ID,SL_NMBR_3,SL_PRTY_3_CD,PRT_FNC_CD,PRT_NM,PRT_NO,PRT_LC_CD,VSSL_NM,VYG_NO,VSSL_CD,SHPPR_NAM,RL_ID_VC,RL_RMP_LOC,HAZ_MTL_CD,HAZ_MATL_DSC FROM V_GATEIN_DETAIL WHERE GTN_ID IN (SELECT GTN_ID FROM GATEIN WHERE DPT_ID=@DPT_ID)"
    'created
    Private Const GateinUpdateQuery As String = "UPDATE GATEIN SET GTOT_BT=@GTOT_BT WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const GateOutSelectQuery As String = "SELECT CSTMR_ID,CSTMR_CD,ORGNL_CSTMR_ID,ORGNL_CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,YRD_LCTN,EQPMNT_STTS_ID,EQPMNT_STTS_CD,PRDCT_CD,PRDCT_ID,GI_TRNSCTN_NO,EIR_NO,VHCL_NO,TRNSPRTR_CD,HTNG_BT,GTN_DT,GTN_ID,RNTL_CSTMR_ID,RNTL_CSTMR_CD,RNTL_RFRNC_NO,RNTL_BT,CAse EQPMNT_STTS_CD When 'AVL' Then 'A' End as GTOT_CNDTN ,DPT_ID,(SELECT CSC_VLDTY FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO =V.EQPMNT_NO AND DPT_ID=V.DPT_ID)CSC_VLDTY,(select COUNT (RPR_ESTMT_ID ) from ATTACHMENT where RPR_ESTMT_ID =V.GTN_ID)COUNT_ATTACH FROM V_GATEOUT_PENDING V WHERE DPT_ID=@DPT_ID"
    Private Const GateOutSelectQueryGWS As String = "SELECT CSTMR_ID,CSTMR_CD,ORGNL_CSTMR_ID,ORGNL_CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,YRD_LCTN,EQPMNT_STTS_ID,EQPMNT_STTS_CD,PRDCT_CD,PRDCT_ID,GI_TRNSCTN_NO,EIR_NO,VHCL_NO,TRNSPRTR_CD,HTNG_BT,GTN_DT,GTN_ID,RNTL_CSTMR_ID,RNTL_CSTMR_CD,RNTL_RFRNC_NO,RNTL_BT,CAse EQPMNT_STTS_CD When 'AVL' Then 'A' End as GTOT_CNDTN ,DPT_ID,(SELECT CSC_VLDTY FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO =V.EQPMNT_NO )CSC_VLDTY,(select COUNT (RPR_ESTMT_ID ) from ATTACHMENT where RPR_ESTMT_ID =V.GTN_ID)COUNT_ATTACH,BLL_CD FROM V_GATEOUT_PENDING_GWS V WHERE V.EQPMNT_STTS_CD NOT IN('APP')"
    Private Const GateOutInsertQuery As String = "INSERT INTO GATEOUT(GTOT_ID,GTOT_CD,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,EQPMNT_STTS_ID,YRD_LCTN,GTOT_DT,GTOT_TM,EIR_NO,VHCL_NO,TRNSPRTR_CD,GI_TRNSCTN_NO,RMRKS_VC,RNTL_CSTMR_ID,RNTL_RFRNC_NO,RNTL_BT,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,SL_NO,GRD_ID,AUTH_NO)VALUES(@GTOT_ID,@GTOT_CD,@CSTMR_ID,@EQPMNT_NO,@EQPMNT_TYP_ID,@EQPMNT_CD_ID,@EQPMNT_STTS_ID,@YRD_LCTN,@GTOT_DT,@GTOT_TM,@EIR_NO,@VHCL_NO,@TRNSPRTR_CD,@GI_TRNSCTN_NO,@RMRKS_VC,@RNTL_CSTMR_ID,@RNTL_RFRNC_NO,@RNTL_BT,@DPT_ID,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@SL_NO,@GRD_ID,@AUTH_NO)"
    Private Const Activity_StatusInsertQuery As String = "INSERT INTO ACTIVITY_STATUS(ACTVTY_STTS_ID,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,GTN_DT,GTOT_DT,PRDCT_ID,CLNNG_DT,EQPMNT_STTS_ID,ACTVTY_NAM,ACTVTY_DT,RMRKS_VCR,GI_TRNSCTN_NO,ACTV_BT,DPT_ID)VALUES(@ACTVTY_STTS_ID,@CSTMR_ID,@EQPMNT_NO,@EQPMNT_TYP_ID,@EQPMNT_CD_ID,@GTN_DT,@GTOT_DT,@PRDCT_ID,@CLNNG_DT,@EQPMNT_STTS_ID,@ACTVTY_NAM,@ACTVTY_DT,@RMRKS_VCR,@GI_TRNSCTN_NO,@ACTV_BT,@DPT_ID)"
    Private Const Customer_RateSelectQueryByCustomerID As String = "SELECT CSTMR_RT_ID,CSTMR_ID,EQPMNT_CD_ID,EQPMNT_TYP_ID,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,STRG_CHRGS_PR_DY_NC,FR_DYS,ACTV_BT,HNDLNG_CHRG_NC,WSHNG_CHRG_NC FROM CUSTOMER_RATE a WHERE CSTMR_ID=@CSTMR_ID"
    Private Const Activity_StatusUpdateQuery As String = "UPDATE ACTIVITY_STATUS SET ACTVTY_DT=@ACTVTY_DT, GTOT_DT=@GTOT_DT, YRD_LCTN=@YRD_LCTN WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const GateOutMysubmitSelectQuery As String = "SELECT GTOT_ID,GTOT_CD,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_STTS_ID,EQPMNT_STTS_CD,EQPMNT_STTS_DSCRPTN_VC,YRD_LCTN,GTOT_DT,AUTH_NO,GTOT_TM,EIR_NO,VHCL_NO,TRNSPRTR_CD,GI_TRNSCTN_NO,DPT_ID,(SELECT GTN_DT FROM GATEIN WHERE GI_TRNSCTN_NO=VGO.GI_TRNSCTN_NO AND DPT_ID=VGO.DPT_ID) AS GTN_DT,(SELECT HTNG_BT FROM GATEIN WHERE GI_TRNSCTN_NO=VGO.GI_TRNSCTN_NO AND DPT_ID=VGO.DPT_ID) AS HTNG_BT,(SELECT GTN_ID FROM GATEIN WHERE GI_TRNSCTN_NO=VGO.GI_TRNSCTN_NO AND DPT_ID=VGO.DPT_ID) AS GTN_ID,RMRKS_VC,(SELECT BLLNG_FLG FROM HANDLING_CHARGE WHERE EQPMNT_NO=VGO.EQPMNT_NO AND GI_TRNSCTN_NO=VGO.GI_TRNSCTN_NO AND CST_TYP='HNDOT' AND HTNG_BT = 0 AND DPT_ID=VGO.DPT_ID)BLLNG_FLG,RNTL_BT,RNTL_RFRNC_NO,COUNT_ATTACH,(select CSC_VLDTY from EQUIPMENT_INFORMATION where EQPMNT_NO =VGO.EQPMNT_NO AND DPT_ID=VGO.DPT_ID )CSC_VLDTY,SL_NO,CAse EQPMNT_STTS_CD When 'OUT' Then 'A' END as GTOT_CNDTN,GRD_ID,GRD_CD,BLL_CD FROM V_GATEOUT AS VGO WHERE DPT_ID=@DPT_ID AND ((GI_TRNSCTN_NO IN (SELECT TOP 1 GI_TRNSCTN_NO FROM ACTIVITY_STATUS WHERE EQPMNT_NO=VGO.EQPMNT_NO AND DPT_ID=VGO.DPT_ID ORDER BY ACTVTY_STTS_ID DESC) AND GI_TRNSCTN_NO IN (SELECT GI_TRNSCTN_NO FROM HANDLING_CHARGE WHERE BLLNG_FLG <> 'B' AND GI_TRNSCTN_NO=VGO.GI_TRNSCTN_NO AND CST_TYP = 'HNDOT' AND HTNG_BT = 0 AND DPT_ID=VGO.DPT_ID)) OR (GI_TRNSCTN_NO IN (SELECT GI_TRNSCTN_NO FROM RENTAL_ENTRY WHERE OFF_HR_DT IS NULL AND GTN_BT=0 AND DPT_ID=VGO.DPT_ID)) OR (GI_TRNSCTN_NO IN (SELECT RNTL_RFRNC_NO FROM RENTAL_ENTRY WHERE OFF_HR_DT IS NULL AND GTN_BT=0 AND DPT_ID=VGO.DPT_ID)))"
    Private Const GateOutUpdateQuery As String = "UPDATE GATEOUT SET YRD_LCTN=@YRD_LCTN, AUTH_NO=@AUTH_NO, SL_NO=@SL_NO, GRD_ID=@GRD_ID, GTOT_DT=@GTOT_DT, GTOT_TM=@GTOT_TM, EIR_NO=@EIR_NO, VHCL_NO=@VHCL_NO, TRNSPRTR_CD=@TRNSPRTR_CD, RMRKS_VC=@RMRKS_VC, RNTL_CSTMR_ID=@RNTL_CSTMR_ID, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE DPT_ID=@DPT_ID AND GTOT_ID=@GTOT_ID "
    Private Const UpdateHandlingStorageQuery As String = "UPDATE HANDLING_CHARGE SET FRM_BLLNG_DT=@FRM_BLLNG_DT, TO_BLLNG_DT=@TO_BLLNG_DT, YRD_LCTN=@YRD_LCTN,RFRNC_EIR_NO_1=@RFRNC_EIR_NO_1,HNDLNG_CST_NC=@HNDLNG_CST_NC,HNDLNG_TX_RT_NC=@HNDLNG_TX_RT_NC,TTL_CSTS_NC=@TTL_CSTS_NC WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CST_TYP='HNDOT'"
    Private Const updateStorageCharge As String = "UPDATE STORAGE_CHARGE SET TO_BLLNG_DT=@TO_BLLNG_DT,YRD_LCTN=@YRD_LCTN, RFRNC_EIR_NO_1=@RFRNC_EIR_NO_1 WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    ' Private Const StorageChargeUpdateQuery As String = "UPDATE STORAGE_CHARGE SET RFRNC_EIR_NO_1=@RFRNC_EIR_NO_1,RFRNC_EIR_NO_2=@RFRNC_EIR_NO_2,TO_BLLNG_DT=@TO_BLLNG_DT, NO_OF_DYS=@NO_OF_DYS,FR_DYS=@FR_DYS, STRG_TX_RT_NC=@STRG_TX_RT_NC, TTL_CSTS_NC=@TTL_CSTS_NC,IS_GT_OT_FLG=@IS_GT_OT_FLG,IS_LT_FLG=(CASE WHEN BLLNG_TLL_DT > @TO_BLLNG_DT THEN 1 WHEN BLLNG_TLL_DT < @TO_BLLNG_DT THEN 0 ELSE 0 END) WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    ''15031 Fix to avoid To Billing Update after Gateout for the records which was splitted already
    Private Const StorageChargeUpdateQuery As String = "UPDATE STORAGE_CHARGE SET AGNT_ID=@AGNT_ID, RFRNC_EIR_NO_1=@RFRNC_EIR_NO_1,RFRNC_EIR_NO_2=@RFRNC_EIR_NO_2,TO_BLLNG_DT=@TO_BLLNG_DT, NO_OF_DYS=@NO_OF_DYS,FR_DYS=@FR_DYS, STRG_TX_RT_NC=@STRG_TX_RT_NC, TTL_CSTS_NC=@TTL_CSTS_NC,IS_GT_OT_FLG=@IS_GT_OT_FLG,IS_LT_FLG=(CASE WHEN BLLNG_TLL_DT > @TO_BLLNG_DT THEN 1 WHEN BLLNG_TLL_DT < @TO_BLLNG_DT THEN 0 ELSE 0 END) WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID AND TO_BLLNG_DT IS  NULL"
    Private Const V_GATEINIDSelectQueryBy As String = "SELECT GTN_ID FROM GATEIN WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Customer_SelectQuery As String = "SELECT CSTMR_ID,CSTMR_CD,CSTMR_NAM,AGNT_CD FROM V_CUSTOMER WHERE ACTV_BT = 1 AND DPT_ID=@DPT_ID"
    Private Const CutomerChargeDetailSelectQuery As String = "SELECT CSTMR_CHRG_DTL_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_CD_ID,EQPMNT_CD_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,HNDLNG_IN_CHRG_NC,HNDLNG_OUT_CHRG_NC,ACTV_BT,DPT_ID FROM V_CUSTOMER_CHARGE_DETAIL WHERE DPT_ID=@DPT_ID AND EQPMNT_CD_ID=@EQPMNT_CD_ID AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID AND CSTMR_ID=@CSTMR_ID"
    Private Const GateOutRentalInsertQuery As String = "INSERT INTO GATEOUT(GTOT_ID,GTOT_CD,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,EQPMNT_STTS_ID,YRD_LCTN,GTOT_DT,GTOT_TM,EIR_NO,VHCL_NO,TRNSPRTR_CD,GI_TRNSCTN_NO,RMRKS_VC,RNTL_CSTMR_ID,RNTL_RFRNC_NO,RNTL_BT,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT)VALUES(@GTOT_ID,@GTOT_CD,@CSTMR_ID,@EQPMNT_NO,@EQPMNT_TYP_ID,@EQPMNT_CD_ID,@EQPMNT_STTS_ID,@YRD_LCTN,@GTOT_DT,@GTOT_TM,@EIR_NO,@VHCL_NO,@TRNSPRTR_CD,@GI_TRNSCTN_NO,@RMRKS_VC,@RNTL_CSTMR_ID,@RNTL_RFRNC_NO,@RNTL_BT,@DPT_ID,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT)"
    Private Const TrackingUpdateQuery As String = "UPDATE TRACKING SET ACTVTY_DT=@ACTVTY_DT,RFRNC_NO=@RFRNC_NO, ACTVTY_RMRKS=@ACTVTY_RMRKS, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE ACTVTY_NAM=@ACTVTY_NAM AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND EQPMNT_NO=@EQPMNT_NO"
    Private Const RentalEntryUpdateQuery As String = "UPDATE RENTAL_ENTRY SET ON_HR_DT=@ON_HR_DT WHERE EQPMNT_NO=@EQPMNT_NO AND RNTL_RFRNC_NO=@RNTL_RFRNC_NO"
    Private Const EquipmentNoActivityStatusSelectQuery As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,INSTRCTNS_VC,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,CERT_GNRTD_FLG,RPR_CMPLTN_DT FROM V_ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND ACTV_BT=1"
    Private Const RentalCustomerSelectQuery As String = "SELECT CSTMR_RNTL_ID,CSTMR_ID,CNTRCT_RFRNC_NO,CNTRCT_STRT_DT,CNTRCT_END_DT,MN_TNR_DY,RNTL_PR_DY,HNDLNG_OT,HNDLNG_IN,ON_HR_SRVY,OFF_HR_SRVY,RMRKS_VC FROM CUSTOMER_RENTAL WHERE CSTMR_ID=@CSTMR_ID AND CNTRCT_RFRNC_NO IN (SELECT TOP 1 CNTRCT_RFRNC_NO FROM RENTAL_ENTRY WHERE EQPMNT_NO=@EQPMNT_NO AND CSTMR_ID=@CSTMR_ID ORDER BY RNTL_ENTRY_ID DESC)"
    Private Const Rental_ChargeInsertQuery As String = "INSERT INTO RENTAL_CHARGE(RNTL_CHRG_ID,EQPMNT_NO,RNTL_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,HNDLNG_OT_NC,HNDLNG_IN_NC,ON_HR_SRVY_NC,OFF_HR_SRVY_NC,FR_DYS,NO_OF_DYS,RNTL_PR_DY_NC,RNTL_TX_RT_NC,TTL_CSTS_NC,RNTL_CNTN_FLG,BLLNG_FLG,IS_GT_IN_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,BLLNG_TYP_CD,CSTMR_ID,DPT_ID,GI_TRNSCTN_NO,RNTL_RFRNC_NO,ON_HR_BLLNG_FLG,OFF_HR_BLLNG_FLG)VALUES(@RNTL_CHRG_ID,@EQPMNT_NO,@RNTL_TYP,@RFRNC_EIR_NO_1,@RFRNC_EIR_NO_2,@FRM_BLLNG_DT,@TO_BLLNG_DT,@HNDLNG_OT_NC,@HNDLNG_IN_NC,@ON_HR_SRVY_NC,@OFF_HR_SRVY_NC,@FR_DYS,@NO_OF_DYS,@RNTL_PR_DY_NC,@RNTL_TX_RT_NC,@TTL_CSTS_NC,@RNTL_CNTN_FLG,@BLLNG_FLG,@IS_GT_IN_FLG,@ACTV_BT,@IS_LT_FLG,@BLLNG_TLL_DT,@YRD_LCTN,@BLLNG_TYP_CD,@CSTMR_ID,@DPT_ID,@GI_TRNSCTN_NO,@RNTL_RFRNC_NO,@ON_HR_BLLNG_FLG,@OFF_HR_BLLNG_FLG)"
    Private Const RentalChargeUpdateQuery As String = "UPDATE RENTAL_CHARGE SET FRM_BLLNG_DT=@FRM_BLLNG_DT,YRD_LCTN=@YRD_LCTN,IS_LT_FLG=@IS_LT_FLG WHERE EQPMNT_NO=@EQPMNT_NO AND RNTL_RFRNC_NO=@RNTL_RFRNC_NO AND DPT_ID=@DPT_ID"
    Private Const RentalEntrySelectQuery As String = " SELECT RNTL_ENTRY_ID,EQPMNT_NO,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT,RMRKS_VC,RNTL_RFRNC_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,OTHR_CHRG_NC,GTN_BT FROM V_RENTAL_ENTRY WHERE RNTL_RFRNC_NO=@RNTL_RFRNC_NO"
    Private Const RentalChargeSelectQuery As String = " SELECT  RNTL_CHRG_ID,EQPMNT_NO,RNTL_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,HNDLNG_OT_NC,HNDLNG_IN_NC,ON_HR_SRVY_NC,OFF_HR_SRVY_NC,FR_DYS,NO_OF_DYS,RNTL_PR_DY_NC,RNTL_TX_RT_NC,TTL_CSTS_NC,RNTL_CNTN_FLG,BLLNG_FLG,IS_GT_IN_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,BLLNG_TYP_CD,CSTMR_ID,DPT_ID,GI_TRNSCTN_NO,RNTL_RFRNC_NO,DRFT_INVC_NO,FNL_INVC_NO,OTHR_CHRG_NC,ON_HR_BLLNG_FLG,OFF_HR_BLLNG_FLG FROM RENTAL_CHARGE WHERE CSTMR_ID=@CSTMR_ID AND BLLNG_TLL_DT IS NOT NULL ORDER BY BLLNG_TLL_DT DESC"
    Private Const Activity_StatusUpdateQuery_NewMode As String = "UPDATE ACTIVITY_STATUS SET EQPMNT_STTS_ID=@EQPMNT_STTS_ID, ACTVTY_NAM=@ACTVTY_NAM, ACTVTY_DT=@ACTVTY_DT,ACTV_BT=@ACTV_BT, GTOT_DT=@GTOT_DT, YRD_LCTN=@YRD_LCTN WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const GateinSelectQueryByGateinTransactionNo As String = "SELECT GTN_ID,GTN_CD,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,EQPMNT_STTS_ID,YRD_LCTN,GTN_DT,GTN_TM,PRDCT_ID,EIR_NO,VHCL_NO,TRNSPRTR_CD,HTNG_BT,RMRKS_VC,GI_TRNSCTN_NO,RNTL_RFRNC_NO,GTOT_BT,RNTL_BT,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,SNO FROM GATEIN WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID = @DPT_ID"
    ''21787
    Private Const StorageChargeGateOutFlagUpdateQuery As String = "UPDATE STORAGE_CHARGE SET IS_GT_OT_FLG=@IS_GT_OT_FLG WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    ''
    Private ds As GateOutDataSet
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
    'Private Const getEquipmentCodeDescriptionQuery As String = "SELECT EQPMNT_CD_DSCRPTN_VC FROM EQUIPMENT_CODE WHERE EQPMNT_CD_CD=@EQPMNT_CD_CD"
    'Private Const Gateout_RetInsertQuerynotnull As String = "INSERT INTO GATEOUT_RET(GO_SNO,GO_TRANSMISSION_NO,GO_UNIT_NBR,GO_DPT_TRM,GO_LSR_OWNER,GO_CONDITION,GO_EIR_DATE,GO_TRNSXN,GO_EIR_TIME) VALUES (@GO_SNO,@GO_TRANSMISSION_NO,@GO_UNIT_NBR,@GO_DPT_TRM,@GO_LSR_OWNER,@GO_CONDITION,@GO_EIR_DATE,@GO_TRNSXN,@GO_EIR_TIME)"
    Private Const Gateout_RetInsertQuerynotnull As String = "INSERT INTO GATEOUT_RET(GO_SNO,GO_TRANSMISSION_NO,GO_UNIT_NBR,GO_DPT_TRM,GO_LSR_OWNER,GO_CONDITION,GO_EIR_DATE,GO_TRNSXN,GO_EQUIP_TYPE,GO_EQUIP_DESC,GO_EQUIP_CODE,GO_ADVICE) VALUES (@GO_SNO,@GO_TRANSMISSION_NO,@GO_UNIT_NBR,@GO_DPT_TRM,@GO_LSR_OWNER,@GO_CONDITION,@GO_EIR_DATE,@GO_TRNSXN,@GO_EQUIP_TYPE,@GO_EQUIP_DESC,@GO_EQUIP_CODE,@GO_ADVICE)"
    Private Const GetAttachmentByGateIN As String = "SELECT COUNT(RPR_ESTMT_ID)COUNT_ATTACH FROM ATTACHMENT WHERE RPR_ESTMT_ID=@RPR_ESTMT_ID AND ACTVTY_NAM=@ACTVTY_NAM"
    Private Const GetAttachmentByGateINAttachment As String = "SELECT ATTCHMNT_ID ,RPR_ESTMT_ID ,ACTVTY_NAM ,RPR_ESTMT_NO ,GI_TRNSCTN_NO ,ATTCHMNT_PTH ,ACTL_FL_NM ,MDFD_BY ,MDFD_DT ,DPT_ID  FROM ATTACHMENT WHERE RPR_ESTMT_ID=@RPR_ESTMT_ID AND ACTVTY_NAM=@ACTVTY_NAM"
    Private Const AttachmentSelectWithActivityQuery As String = "SELECT ATTCHMNT_ID,RPR_ESTMT_ID,ACTVTY_NAM,RPR_ESTMT_NO,GI_TRNSCTN_NO,ATTCHMNT_PTH,ACTL_FL_NM,MDFD_BY,MDFD_DT,DPT_ID,'False' AS ERR_FLG FROM ATTACHMENT WHERE DPT_ID=@DPT_ID AND RPR_ESTMT_ID=@RPR_ESTMT_ID AND ACTVTY_NAM=@ACTVTY_NAM"

    'Type & Code Merge
    Private Const getEquipmentCodeDescriptionQuery As String = "SELECT EQPMNT_TYP_DSCRPTN_VC FROM EQUIPMENT_TYPE WHERE EQPMNT_CD_CD=@EQPMNT_CD_CD"

    Private Const GetEquipmentInformationQuery As String = "SELECT MNFCTR_DT,CSC_VLDTY,(SELECT DISTINCT(INSPCTD_BY)  FROM EQUIPMENT_INSPECTION WHERE EQPMNT_NO=G.EQPMNT_NO)INSPCTD_BY FROM EQUIPMENT_INFORMATION G WHERE EQPMNT_NO=@EQPMNT_NO"

    'GateOut Approval Process - GWS
    Private Const pub_GetGateOutDetailsGWSWithApproval_SelectQry As String = "SELECT CSTMR_ID,CSTMR_CD,ORGNL_CSTMR_ID,ORGNL_CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,YRD_LCTN,EQPMNT_STTS_ID,EQPMNT_STTS_CD,PRDCT_CD,PRDCT_ID,GI_TRNSCTN_NO,EIR_NO,VHCL_NO,TRNSPRTR_CD,HTNG_BT,GTN_DT,GTN_ID,RNTL_CSTMR_ID,RNTL_CSTMR_CD,RNTL_RFRNC_NO,RNTL_BT,CAse  When EQPMNT_STTS_CD='AVL' OR EQPMNT_STTS_CD='APP' Then 'A' End as GTOT_CNDTN ,DPT_ID,(SELECT CSC_VLDTY FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO =V.EQPMNT_NO )CSC_VLDTY,(select COUNT (RPR_ESTMT_ID ) from ATTACHMENT where RPR_ESTMT_ID =V.GTN_ID)COUNT_ATTACH,BLL_CD,BKNG_AUTH_NO AS AUTH_NO FROM V_GATEOUT_PENDING_GWS V WHERE V.EQPMNT_STTS_CD IN('APP') ORDER BY EQPMNT_NO"
    Private Const GetAgenIdFromCustomer_SelectQry As String = "SELECT AGENT_ID FROM CUSTOMER WHERE CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"
    Private Const DeleteGateoutRET_DeleteQry As String = "DELETE FROM GATEOUT_RET WHERE GO_SNO=@GO_SNO"

#End Region

#Region "Constructor.."

    Sub New()
        ds = New GateOutDataSet
    End Sub

#End Region

#Region "GetGateIn() TABLE NAME:Outward_Pass"

    Public Function GetGateIn(ByVal bv_DepotId As Int64) As GateOutDataSet
        Try
            objData = New DataObjects(GateinSelectQuery, GateOutData.DPT_ID, bv_DepotId)
            objData.Fill(CType(ds, DataSet), GateOutData._V_GATEOUT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetVGateinDetailBy() TABLE NAME:V_GATEIN_DETAIL"

    Public Function GetVGateinDetailBy(ByVal bv_DepotID As Integer) As GateOutDataSet
        Try
            objData = New DataObjects(V_GATEIN_DETAILSelectQueryBy, GateOutData.DPT_ID, bv_DepotID)
            objData.Fill(CType(ds, DataSet), GateOutData._V_GATEIN_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetCustomerDetail"
    Public Function pub_GetCustomerDetail(ByVal bv_intDPT_ID As Integer) As GateOutDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(GateOutData.DPT_ID, bv_intDPT_ID)
            objData = New DataObjects(Customer_SelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), GateOutData._CUSTOMER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "getGateinId() "

    Public Function getGateinId(ByVal transno As String) As GateOutDataSet
        Try
            objData = New DataObjects(V_GATEINIDSelectQueryBy, GateOutData.GI_TRNSCTN_NO, transno)
            objData.Fill(CType(ds, DataSet), GateOutData._GATEIN)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCustomerRateByCustomerID() TABLE NAME:CUSTOMER_RATE"

    Public Function GetCustomerRateByCustomerID(ByVal bv_i64CustomerID As Int64) As GateOutDataSet
        Try
            objData = New DataObjects(Customer_RateSelectQueryByCustomerID, GateOutData.CSTMR_ID, CStr(bv_i64CustomerID))
            objData.Fill(CType(ds, DataSet), GateOutData._CUSTOMER_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateGateoutRet() TABLE NAME:GateOutData"

    Public Function CreateGateoutRet(ByVal bv_lngCreated As Int64, _
                                     ByVal bv_strGO_TRANSMISSION_NO As String, _
                                     ByVal bv_strGO_COMPLETE As String, _
                                     ByVal bv_strGO_SENT_EIR As String, _
                                     ByVal bv_strGO_SENT_DATE As String, _
                                     ByVal bv_strGO_REC_EIR As String, _
                                     ByVal bv_strGO_REC_DATE As String, _
                                     ByVal bv_strGO_REC_ADDR As String, _
                                     ByVal bv_strGO_REC_TYPE As String, _
                                     ByVal bv_strGO_EXPORTED As String, _
                                     ByVal bv_strGO_EXPOR_DATE As String, _
                                     ByVal bv_strGO_IMPORTED As String, _
                                     ByVal bv_strGO_IMPOR_DATE As String, _
                                     ByVal bv_strGO_TRNSXN As String, _
                                     ByVal bv_strGO_ADVICE As String, _
                                     ByVal bv_strGO_UNIT_NBR As String, _
                                     ByVal bv_strGO_EQUIP_TYPE As String, _
                                     ByVal bv_strGO_EQUIP_DESC As String, _
                                     ByVal bv_strGO_EQUIP_CODE As String, _
                                     ByVal bv_strGO_CONDITION As String, _
                                     ByVal bv_strGO_COMP_ID_A As String, _
                                     ByVal bv_strGO_COMP_ID_N As String, _
                                     ByVal bv_strGO_COMP_ID_C As String, _
                                     ByVal bv_strGO_COMP_TYPE As String, _
                                     ByVal bv_strGO_COMP_DESC As String, _
                                     ByVal bv_strGO_COMP_CODE As String, _
                                     ByVal bv_datGO_EIR_DATE As DateTime, _
                                     ByVal bv_strGO_EIR_TIME As String, _
                                     ByVal bv_strGO_REFERENCE As String, _
                                     ByVal bv_strGO_MANU_DATE As String, _
                                     ByVal bv_strGO_MATERIAL As String, _
                                     ByVal bv_strGO_WEIGHT As String, _
                                     ByVal bv_strGO_MEASURE As String, _
                                     ByVal bv_strGO_UNITS As String, _
                                     ByVal bv_strGO_CSC_REEXAM As String, _
                                     ByVal bv_strGO_COUNTRY As String, _
                                     ByVal bv_strGO_LIC_STATE As String, _
                                     ByVal bv_strGO_LIC_REG As String, _
                                     ByVal bv_strGO_LIC_EXPIRE As String, _
                                     ByVal bv_strGO_LSR_OWNER As String, _
                                     ByVal bv_strGO_SEND_EDI_1 As String, _
                                     ByVal bv_strGO_SSL_LSE As String, _
                                     ByVal bv_strGO_SEND_EDI_2 As String, _
                                     ByVal bv_strGO_HAULIER As String, _
                                     ByVal bv_strGO_SEND_EDI_3 As String, _
                                     ByVal bv_strGO_DPT_TRM As String, _
                                     ByVal bv_strGO_SEND_EDI_4 As String, _
                                     ByVal bv_strGO_OTHERS_1 As String, _
                                     ByVal bv_strGO_OTHERS_2 As String, _
                                     ByVal bv_strGO_OTHERS_3 As String, _
                                     ByVal bv_strGO_OTHERS_4 As String, _
                                     ByVal bv_strGO_NOTE_1 As String, _
                                     ByVal bv_strGO_NOTE_2 As String, _
                                     ByVal bv_strGO_LOAD As String, _
                                     ByVal bv_strGO_FHWA As String, _
                                     ByVal bv_strGO_LAST_OH_LOC As String, _
                                     ByVal bv_strGO_LAST_OH_DATE As String, _
                                     ByVal bv_strGO_SENDER As String, _
                                     ByVal bv_strGO_ATTENTION As String, _
                                     ByVal bv_strGO_REVISION As String, _
                                     ByVal bv_strGO_SEND_EDI_5 As String, _
                                     ByVal bv_strGO_SEND_EDI_6 As String, _
                                     ByVal bv_strGO_SEND_EDI_7 As String, _
                                     ByVal bv_strGO_SEND_EDI_8 As String, _
                                     ByVal bv_strGO_SEAL_PARTY_1 As String, _
                                     ByVal bv_strGO_SEAL_NUMBER_1 As String, _
                                     ByVal bv_strGO_SEAL_PARTY_2 As String, _
                                     ByVal bv_strGO_SEAL_NUMBER_2 As String, _
                                     ByVal bv_strGO_SEAL_PARTY_3 As String, _
                                     ByVal bv_strGO_SEAL_NUMBER_3 As String, _
                                     ByVal bv_strGO_SEAL_PARTY_4 As String, _
                                     ByVal bv_strGO_SEAL_NUMBER_4 As String, _
                                     ByVal bv_strGO_PORT_FUNC_CODE As String, _
                                     ByVal bv_strGO_PORT_NAME As String, _
                                     ByVal bv_strGO_VESSEL_NAME As String, _
                                     ByVal bv_strGO_VOYAGE_NUM As String, _
                                     ByVal bv_strGO_HAZ_MAT_CODE As String, _
                                     ByVal bv_strGO_HAZ_MAT_DESC As String, _
                                     ByVal bv_strGO_NOTE_3 As String, _
                                     ByVal bv_strGO_NOTE_4 As String, _
                                     ByVal bv_strGO_NOTE_5 As String, _
                                     ByVal bv_strGO_COMP_ID_A_2 As String, _
                                     ByVal bv_strGO_COMP_ID_N_2 As String, _
                                     ByVal bv_strGO_COMP_ID_C_2 As String, _
                                     ByVal bv_strGO_COMP_TYPE_2 As String, _
                                     ByVal bv_strGO_COMP_CODE_2 As String, _
                                     ByVal bv_strGO_COMP_DESC_2 As String, _
                                     ByVal bv_strGO_SHIPPER As String, _
                                     ByVal bv_strGO_DRAY_ORDER As String, _
                                     ByVal bv_strGO_RAIL_ID As String, _
                                     ByVal bv_strGO_RAIL_RAMP As String, _
                                     ByVal bv_strGO_VESSEL_CODE As String, _
                                     ByVal bv_strGO_WGHT_CERT_1 As String, _
                                     ByVal bv_strGO_WGHT_CERT_2 As String, _
                                     ByVal bv_strGO_WGHT_CERT_3 As String, _
                                     ByVal bv_strGO_SEA_RAIL As String, _
                                     ByVal bv_str As String, _
                                     ByVal bv_strGO_LOC_IDENT As String, _
                                     ByVal bv_strGO_PORT_LOC_QUAL As String, _
                                     ByVal bv_strGO_STATUS As String, _
                                     ByVal bv_datGO_PICK_DATE As DateTime, _
                                     ByVal bv_strGO_ERRSTATUS As String, _
                                     ByVal bv_strGO_USERNAME As String, _
                                     ByVal bv_i32GO_LIVE_STATUS As Int32, _
                                     ByVal bv_strGO_YARD_LOC As String, _
                                     ByVal bv_strGO_MODE_PAYMENT As String, _
                                     ByVal bv_strGO_BILLING_TYPE As String, _
                                     ByVal bv_intGateOutID As Integer, _
                                     ByRef br_objtrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._GATEOUT_RET).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GateOutData._GATEOUT_RET, br_objtrans)
                .Item(GateOutData.GO_SNO) = bv_lngCreated
                .Item(GateOutData.GO_TRANSMISSION_NO) = bv_strGO_TRANSMISSION_NO
                .Item(GateOutData.GO_COMPLETE) = bv_strGO_COMPLETE
                .Item(GateOutData.GO_SENT_EIR) = bv_strGO_SENT_EIR
                .Item(GateOutData.GO_SENT_DATE) = bv_strGO_SENT_DATE
                .Item(GateOutData.GO_REC_EIR) = bv_strGO_REC_EIR
                .Item(GateOutData.GO_REC_DATE) = bv_strGO_REC_DATE
                .Item(GateOutData.GO_REC_ADDR) = bv_strGO_REC_ADDR
                .Item(GateOutData.GO_REC_TYPE) = bv_strGO_REC_TYPE
                .Item(GateOutData.GO_EXPORTED) = bv_strGO_EXPORTED
                .Item(GateOutData.GO_EXPOR_DATE) = bv_strGO_EXPOR_DATE
                .Item(GateOutData.GO_IMPORTED) = bv_strGO_IMPORTED
                .Item(GateOutData.GO_IMPOR_DATE) = bv_strGO_IMPOR_DATE
                .Item(GateOutData.GO_TRNSXN) = bv_strGO_TRNSXN

                If bv_strGO_ADVICE <> Nothing Then
                    .Item(GateOutData.GO_ADVICE) = bv_strGO_ADVICE
                Else
                    .Item(GateOutData.GO_ADVICE) = DBNull.Value
                End If

                .Item(GateOutData.GO_UNIT_NBR) = bv_strGO_UNIT_NBR
                .Item(GateOutData.GO_EQUIP_TYPE) = bv_strGO_EQUIP_TYPE
                .Item(GateOutData.GO_EQUIP_DESC) = bv_strGO_EQUIP_DESC
                .Item(GateOutData.GO_EQUIP_CODE) = bv_strGO_EQUIP_CODE
                .Item(GateOutData.GO_CONDITION) = bv_strGO_CONDITION
                .Item(GateOutData.GO_COMP_ID_A) = bv_strGO_COMP_ID_A
                .Item(GateOutData.GO_COMP_ID_N) = bv_strGO_COMP_ID_N
                .Item(GateOutData.GO_COMP_ID_C) = bv_strGO_COMP_ID_C
                .Item(GateOutData.GO_COMP_TYPE) = bv_strGO_COMP_TYPE
                .Item(GateOutData.GO_COMP_DESC) = bv_strGO_COMP_DESC
                .Item(GateOutData.GO_COMP_CODE) = bv_strGO_COMP_CODE
                .Item(GateOutData.GO_EIR_DATE) = bv_datGO_EIR_DATE
                .Item(GateOutData.GO_EIR_TIME) = bv_strGO_EIR_TIME
                .Item(GateOutData.GO_REFERENCE) = bv_strGO_REFERENCE
                .Item(GateOutData.GO_MANU_DATE) = bv_strGO_MANU_DATE
                .Item(GateOutData.GO_MATERIAL) = bv_strGO_MATERIAL
                .Item(GateOutData.GO_WEIGHT) = bv_strGO_WEIGHT
                .Item(GateOutData.GO_MEASURE) = bv_strGO_MEASURE
                .Item(GateOutData.GO_UNITS) = bv_strGO_UNITS
                .Item(GateOutData.GO_CSC_REEXAM) = bv_strGO_CSC_REEXAM
                .Item(GateOutData.GO_COUNTRY) = bv_strGO_COUNTRY
                .Item(GateOutData.GO_LIC_STATE) = bv_strGO_LIC_STATE
                .Item(GateOutData.GO_LIC_REG) = bv_strGO_LIC_REG
                .Item(GateOutData.GO_LIC_EXPIRE) = bv_strGO_LIC_EXPIRE
                .Item(GateOutData.GO_LSR_OWNER) = bv_strGO_LSR_OWNER
                .Item(GateOutData.GO_SEND_EDI_1) = bv_strGO_SEND_EDI_1
                .Item(GateOutData.GO_SSL_LSE) = bv_strGO_SSL_LSE
                .Item(GateOutData.GO_SEND_EDI_2) = bv_strGO_SEND_EDI_2
                .Item(GateOutData.GO_HAULIER) = bv_strGO_HAULIER
                .Item(GateOutData.GO_SEND_EDI_3) = bv_strGO_SEND_EDI_3
                .Item(GateOutData.GO_DPT_TRM) = bv_strGO_DPT_TRM
                .Item(GateOutData.GO_SEND_EDI_4) = bv_strGO_SEND_EDI_4
                .Item(GateOutData.GO_OTHERS_1) = bv_strGO_OTHERS_1
                .Item(GateOutData.GO_OTHERS_2) = bv_strGO_OTHERS_2
                .Item(GateOutData.GO_OTHERS_3) = bv_strGO_OTHERS_3
                .Item(GateOutData.GO_OTHERS_4) = bv_strGO_OTHERS_4
                .Item(GateOutData.GO_NOTE_1) = bv_strGO_NOTE_1
                .Item(GateOutData.GO_NOTE_2) = bv_strGO_NOTE_2
                .Item(GateOutData.GO_LOAD) = bv_strGO_LOAD
                .Item(GateOutData.GO_FHWA) = bv_strGO_FHWA
                .Item(GateOutData.GO_LAST_OH_LOC) = bv_strGO_LAST_OH_LOC
                .Item(GateOutData.GO_LAST_OH_DATE) = bv_strGO_LAST_OH_DATE
                .Item(GateOutData.GO_SENDER) = bv_strGO_SENDER
                .Item(GateOutData.GO_ATTENTION) = bv_strGO_ATTENTION
                .Item(GateOutData.GO_REVISION) = bv_strGO_REVISION
                .Item(GateOutData.GO_SEND_EDI_5) = bv_strGO_SEND_EDI_5
                .Item(GateOutData.GO_SEND_EDI_6) = bv_strGO_SEND_EDI_6
                .Item(GateOutData.GO_SEND_EDI_7) = bv_strGO_SEND_EDI_7
                .Item(GateOutData.GO_SEND_EDI_8) = bv_strGO_SEND_EDI_8
                .Item(GateOutData.GO_SEAL_PARTY_1) = bv_strGO_SEAL_PARTY_1
                .Item(GateOutData.GO_SEAL_NUMBER_1) = bv_strGO_SEAL_NUMBER_1
                .Item(GateOutData.GO_SEAL_PARTY_2) = bv_strGO_SEAL_PARTY_2
                .Item(GateOutData.GO_SEAL_NUMBER_2) = bv_strGO_SEAL_NUMBER_2
                .Item(GateOutData.GO_SEAL_PARTY_3) = bv_strGO_SEAL_PARTY_3
                .Item(GateOutData.GO_SEAL_NUMBER_3) = bv_strGO_SEAL_NUMBER_3
                .Item(GateOutData.GO_SEAL_PARTY_4) = bv_strGO_SEAL_PARTY_4
                .Item(GateOutData.GO_SEAL_NUMBER_4) = bv_strGO_SEAL_NUMBER_4
                .Item(GateOutData.GO_PORT_FUNC_CODE) = bv_strGO_PORT_FUNC_CODE
                .Item(GateOutData.GO_PORT_NAME) = bv_strGO_PORT_NAME
                .Item(GateOutData.GO_VESSEL_NAME) = bv_strGO_VESSEL_NAME
                .Item(GateOutData.GO_VOYAGE_NUM) = bv_strGO_VOYAGE_NUM
                .Item(GateOutData.GO_HAZ_MAT_CODE) = bv_strGO_HAZ_MAT_CODE
                .Item(GateOutData.GO_HAZ_MAT_DESC) = bv_strGO_HAZ_MAT_DESC
                .Item(GateOutData.GO_NOTE_3) = bv_strGO_NOTE_3
                .Item(GateOutData.GO_NOTE_4) = bv_strGO_NOTE_4
                .Item(GateOutData.GO_NOTE_5) = bv_strGO_NOTE_5
                .Item(GateOutData.GO_COMP_ID_A_2) = bv_strGO_COMP_ID_A_2
                .Item(GateOutData.GO_COMP_ID_N_2) = bv_strGO_COMP_ID_N_2
                .Item(GateOutData.GO_COMP_ID_C_2) = bv_strGO_COMP_ID_C_2
                .Item(GateOutData.GO_COMP_TYPE_2) = bv_strGO_COMP_TYPE_2
                .Item(GateOutData.GO_COMP_CODE_2) = bv_strGO_COMP_CODE_2
                .Item(GateOutData.GO_COMP_DESC_2) = bv_strGO_COMP_DESC_2
                .Item(GateOutData.GO_SHIPPER) = bv_strGO_SHIPPER
                .Item(GateOutData.GO_DRAY_ORDER) = bv_strGO_DRAY_ORDER
                .Item(GateOutData.GO_RAIL_ID) = bv_strGO_RAIL_ID
                .Item(GateOutData.GO_RAIL_RAMP) = bv_strGO_RAIL_RAMP
                .Item(GateOutData.GO_VESSEL_CODE) = bv_strGO_VESSEL_CODE
                .Item(GateOutData.GO_WGHT_CERT_1) = bv_strGO_WGHT_CERT_1
                .Item(GateOutData.GO_WGHT_CERT_2) = bv_strGO_WGHT_CERT_2
                .Item(GateOutData.GO_WGHT_CERT_3) = bv_strGO_WGHT_CERT_3
                .Item(GateOutData.GO_SEA_RAIL) = bv_strGO_SEA_RAIL
                .Item(GateOutData.GO_BILL_LADING) = bv_str
                .Item(GateOutData.GO_LOC_IDENT) = bv_strGO_LOC_IDENT
                .Item(GateOutData.GO_PORT_LOC_QUAL) = bv_strGO_PORT_LOC_QUAL
                .Item(GateOutData.GO_STATUS) = bv_strGO_STATUS
                If bv_datGO_PICK_DATE <> Nothing Then
                    .Item(GateOutData.GO_PICK_DATE) = bv_datGO_PICK_DATE
                Else
                    .Item(GateOutData.GO_PICK_DATE) = DBNull.Value
                End If
                If bv_strGO_ERRSTATUS <> Nothing Then
                    .Item(GateOutData.GO_ERRSTATUS) = bv_strGO_ERRSTATUS
                Else
                    .Item(GateOutData.GO_ERRSTATUS) = DBNull.Value
                End If
                If bv_strGO_USERNAME <> Nothing Then
                    .Item(GateOutData.GO_USERNAME) = bv_strGO_USERNAME
                Else
                    .Item(GateOutData.GO_USERNAME) = DBNull.Value
                End If
                If bv_i32GO_LIVE_STATUS <> 0 Then
                    .Item(GateOutData.GO_LIVE_STATUS) = bv_i32GO_LIVE_STATUS
                Else
                    .Item(GateOutData.GO_LIVE_STATUS) = DBNull.Value
                End If
                If bv_strGO_YARD_LOC <> Nothing Then
                    .Item(GateOutData.GO_YARD_LOC) = bv_strGO_YARD_LOC
                Else
                    .Item(GateOutData.GO_YARD_LOC) = DBNull.Value
                End If
                If bv_strGO_MODE_PAYMENT <> Nothing Then
                    .Item(GateOutData.GO_MODE_PAYMENT) = bv_strGO_MODE_PAYMENT
                Else
                    .Item(GateOutData.GO_MODE_PAYMENT) = DBNull.Value
                End If
                If bv_strGO_BILLING_TYPE <> Nothing Then
                    .Item(GateOutData.GO_BILLING_TYPE) = bv_strGO_BILLING_TYPE
                Else
                    .Item(GateOutData.GO_BILLING_TYPE) = DBNull.Value
                End If
                ' .Item(GateOutData.GTOT_ID) = bv_intGateOutID
            End With
            '    objData.InsertRow(dr, Gateout_RetInsertQuery, br_objtrans)
            objData.InsertRow(dr, Gateout_RetInsertQuerynotnull, br_objtrans)
            dr = Nothing
            CreateGateoutRet = intMax
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
        ByVal bv_heating As Boolean, _
        ByVal bv_transno As String, _
        ByVal strAgentId As String, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._HANDLING_CHARGE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GateOutData._HANDLING_CHARGE, br_objTrans)
                .Item(GateOutData.HNDLNG_CHRG_ID) = intMax
                .Item(GateOutData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(GateOutData.EQPMNT_CD_ID) = bv_i64EQPMNTCodeID
                .Item(GateOutData.EQPMNT_TYP_ID) = bv_i64EQPMNT_TYP_ID
                .Item(GateOutData.CST_TYP) = bv_strCstType
                .Item(GateOutData.RFRNC_EIR_NO_1) = bv_strRFRNC_EIR_NO_1
                .Item(GateOutData.RFRNC_EIR_NO_2) = bv_strRFRNC_EIR_NO_2
                .Item(GateOutData.FRM_BLLNG_DT) = bv_datFRM_BLLNG_DT
                .Item(GateOutData.TO_BLLNG_DT) = bv_datTO_BLLNG_DT
                .Item(GateOutData.FR_DYS) = bv_i32FR_DYS
                .Item(GateOutData.NO_OF_DYS) = bv_i32NO_OF_DYS
                .Item(GateOutData.HNDLNG_CST_NC) = bv_strHNDLNG_CST_NC
                .Item(GateOutData.HNDLNG_TX_RT_NC) = bv_strHNDLNG_TX_RT_NC
                .Item(GateOutData.TTL_CSTS_NC) = bv_strTTL_CSTS_NC
                .Item(GateOutData.BLLNG_FLG) = bv_strBLLNG_FLG
                .Item(GateOutData.ACTV_BT) = bv_blnACTV_BT
                .Item(GateOutData.DPT_ID) = bv_i32DPT_ID
                .Item(GateOutData.IS_GT_OT_FLG) = bv_strIS_GT_OT_FLG
                If bv_strYRD_LCTN <> Nothing Then
                    .Item(GateOutData.YRD_LCTN) = bv_strYRD_LCTN
                Else
                    .Item(GateOutData.YRD_LCTN) = DBNull.Value
                End If
                If bv_strBLLNG_TYP_CD <> Nothing Then
                    .Item(GateOutData.BLLNG_TYP_CD) = bv_strBLLNG_TYP_CD
                Else
                    .Item(GateOutData.BLLNG_TYP_CD) = DBNull.Value
                End If
                .Item(GateOutData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(GateOutData.HTNG_BT) = bv_heating
                .Item(GateOutData.GI_TRNSCTN_NO) = bv_transno

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

#Region "UpdateStorageCharge() TABLE NAME:Storage_Charge"

    Public Function UpdateStorage(ByVal bv_strEquipmentNo As String, _
        ByVal bv_strReferenceEIRNo2 As String, _
        ByVal bv_datToBillingDate As DateTime, _
        ByVal bv_i32FreeDays As Int32, _
        ByVal bv_i32NoOfDays As Int32, _
        ByVal bv_strStorageCost As Decimal, _
        ByVal bv_strStorageTaxRate As Decimal, _
        ByVal bv_strTotalcost As Decimal, _
        ByVal bv_i64CustomerId As Int64,
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_strGTN_TRNSXN As String, _
        ByVal gtotFlag As String, _
        ByVal strAgentId As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._STORAGE_CHARGE).NewRow()
            With dr
                .Item(GateOutData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_strReferenceEIRNo2 <> Nothing Then
                    .Item(GateOutData.RFRNC_EIR_NO_1) = bv_strReferenceEIRNo2
                Else
                    .Item(GateOutData.RFRNC_EIR_NO_1) = DBNull.Value
                End If

                If bv_datToBillingDate <> Nothing And bv_datToBillingDate <> sqlDbnull Then
                    .Item(GateOutData.TO_BLLNG_DT) = bv_datToBillingDate
                Else
                    .Item(GateOutData.TO_BLLNG_DT) = DBNull.Value
                End If
                .Item(GateOutData.FR_DYS) = bv_i32FreeDays
                If bv_i32NoOfDays <> 0 Then
                    .Item(GateOutData.NO_OF_DYS) = bv_i32NoOfDays
                Else
                    .Item(GateOutData.NO_OF_DYS) = DBNull.Value
                End If
                '.Item(GateOutData.STRG_CST_NC) = bv_strStorageCost
                .Item(GateOutData.STRG_TX_RT_NC) = bv_strStorageTaxRate
                .Item(GateOutData.TTL_CSTS_NC) = bv_strTotalcost

                .Item(GateOutData.ACTV_BT) = bv_blnActiveBit

                .Item(GateOutData.CSTMR_ID) = bv_i64CustomerId
                .Item(GateOutData.DPT_ID) = bv_i32DepotId
                .Item(GateOutData.RFRNC_EIR_NO_2) = bv_strGTN_TRNSXN
                .Item(GateOutData.IS_GT_OT_FLG) = gtotFlag
                .Item(GateOutData.GI_TRNSCTN_NO) = bv_strGTN_TRNSXN

                If strAgentId <> Nothing Then
                    .Item(GateinData.AGNT_ID) = strAgentId
                Else
                    .Item(GateinData.AGNT_ID) = DBNull.Value
                End If

            End With
            UpdateStorage = objData.UpdateRow(dr, StorageChargeUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateGatein() TABLE NAME:Gatein"

    Public Function UpdateGatein(ByVal bv_Trans_no As String, _
        ByVal bv_blnOutwardPassStatausBit As Boolean, _
        ByRef br_objtrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._GATEIN).NewRow()
            With dr
                .Item(GateOutData.GI_TRNSCTN_NO) = bv_Trans_no
                .Item(GateOutData.GTOT_BT) = bv_blnOutwardPassStatausBit
            End With
            UpdateGatein = objData.UpdateRow(dr, GateinUpdateQuery, br_objtrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetEqupimentStatus"
    Public Function pub_GetEqupimentStatus(ByVal bv_intDPT_ID As Integer) As GateOutDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(GateOutData.DPT_ID, bv_intDPT_ID)
            objData = New DataObjects(Equipment_Status_SelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), GateOutData._EQUIPMENT_STATUS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetGateOutDetails()"

    Public Function pub_GetGateOutDetails(ByVal bv_intDepotId As Integer) As GateOutDataSet
        Try
            objData = New DataObjects(GateOutSelectQuery, GateOutData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), GateOutData._V_GATEOUT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetGateOutDetailsGWS()"

    Public Function pub_GetGateOutDetailsGWS(ByVal bv_intDepotId As Integer) As GateOutDataSet
        Try
            objData = New DataObjects(GateOutSelectQueryGWS, GateOutData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), GateOutData._V_GATEOUT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function pub_GetGateOutDetailsGWSWithApproval(ByVal bv_intDepotId As Integer) As GateOutDataSet
        Try
            objData = New DataObjects(pub_GetGateOutDetailsGWSWithApproval_SelectQry, GateOutData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), GateOutData._V_GATEOUT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetGateOutMySubmitDetails()"

    Public Function pub_GetGateOutMySubmitDetails(ByVal bv_intDepotId As Integer) As GateOutDataSet
        Try
            objData = New DataObjects(GateOutMysubmitSelectQuery, GateOutData.DPT_ID, bv_intDepotId)

            objData.Fill(CType(ds, DataSet), GateOutData._V_GATEOUT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateGateOut() TABLE NAME:GateOut"

    Public Function CreateGateOut(ByVal bv_i64CustomerID As Int64, _
        ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64EquipmentTypeId As Int64, _
        ByVal bv_i64EquipmentCodeId As Int64, _
        ByVal bv_i64EquipmentStatusId As Int64, _
        ByVal bv_i64GradeId As Int64, _
        ByVal bv_strYardLocation As String, _
        ByVal bv_datGateOutDate As DateTime, _
        ByVal bv_strGateOutTime As String, _
        ByVal bv_strEIR_NO As String, _
        ByVal bv_strSLNO As String, _
        ByVal bv_strVechicleNo As String, _
        ByVal bv_strTransporterCode As String, _
        ByVal bv_strAuthNo As String, _
        ByRef bv_strGateInTransactionNo As String, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_strRemarks As String, _
        ByVal bv_intRentalCstmrID As Integer, _
        ByVal bv_strRentalRefNo As String, _
        ByVal bv_rntlbt As Boolean, _
        ByVal bv_strGTOTCD As String, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            Dim intIndexMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._GATEOUT).NewRow()
            With dr
                intIndexMax = CommonUIs.GetIdentityValue("GATEOUT_EIR", br_objTrans)
                intMax = CommonUIs.GetIdentityValue(GateOutData._GATEOUT, br_objTrans)
                .Item(GateOutData.GTOT_ID) = intMax
                'From Index Pattern
                ' .Item(GateOutData.GTOT_CD) = CommonUIs.GetIdentityCode(GateOutData._GATEOUT, intMax, bv_datGateOutDate, br_objTrans)
                '.Item(GateOutData.GTOT_CD) = IndexPatterns.GetIdentityCode(GateOutData._GATEOUT, intMax, bv_datGateOutDate, bv_i32DepotId, br_objTrans)
                .Item(GateOutData.GTOT_CD) = bv_strGTOTCD
                .Item(GateOutData.CSTMR_ID) = bv_i64CustomerID
                .Item(GateOutData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateOutData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                .Item(GateOutData.EQPMNT_CD_ID) = bv_i64EquipmentCodeId
                .Item(GateOutData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                .Item(GateOutData.GRD_ID) = bv_i64GradeId
                If bv_strYardLocation <> Nothing Then
                    .Item(GateOutData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(GateOutData.YRD_LCTN) = DBNull.Value
                End If
                .Item(GateOutData.GTOT_DT) = bv_datGateOutDate
                If bv_strGateOutTime <> Nothing Then
                    .Item(GateOutData.GTOT_TM) = bv_strGateOutTime
                Else
                    .Item(GateOutData.GTOT_TM) = DBNull.Value
                End If
                If bv_strEIR_NO <> Nothing Then
                    .Item(GateOutData.EIR_NO) = bv_strEIR_NO
                Else
                    .Item(GateOutData.EIR_NO) = DBNull.Value
                End If
                If bv_strSLNO <> Nothing Then
                    .Item(GateOutData.SL_NO) = bv_strSLNO
                Else
                    .Item(GateOutData.SL_NO) = DBNull.Value
                End If
                If bv_strVechicleNo <> Nothing Then
                    .Item(GateOutData.VHCL_NO) = bv_strVechicleNo
                Else
                    .Item(GateOutData.VHCL_NO) = DBNull.Value
                End If
                If bv_strTransporterCode <> Nothing Then
                    .Item(GateOutData.TRNSPRTR_CD) = bv_strTransporterCode
                Else
                    .Item(GateOutData.TRNSPRTR_CD) = DBNull.Value
                End If
                If bv_strAuthNo <> Nothing Then
                    .Item(GateOutData.AUTH_NO) = bv_strAuthNo
                Else
                    .Item(GateOutData.AUTH_NO) = DBNull.Value
                End If

                .Item(GateOutData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(GateOutData.DPT_ID) = bv_i32DepotId
                .Item(GateOutData.CRTD_BY) = bv_strCreatedBy
                .Item(GateOutData.CRTD_DT) = bv_datCreatedDate
                .Item(GateOutData.MDFD_BY) = bv_strModifiedBy
                .Item(GateOutData.MDFD_DT) = bv_datModifiedDate
                If bv_strRemarks <> Nothing Then
                    .Item(GateOutData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(GateOutData.RMRKS_VC) = DBNull.Value
                End If
                If bv_strRentalRefNo <> Nothing Then
                    .Item(GateOutData.RNTL_RFRNC_NO) = bv_strRentalRefNo
                Else
                    .Item(GateOutData.RNTL_RFRNC_NO) = DBNull.Value
                End If
                If bv_intRentalCstmrID <> Nothing Then
                    .Item(GateOutData.RNTL_CSTMR_ID) = bv_intRentalCstmrID
                Else
                    .Item(GateOutData.RNTL_CSTMR_ID) = DBNull.Value
                End If
                .Item(GateOutData.RNTL_BT) = bv_rntlbt

            End With
            objData.InsertRow(dr, GateOutInsertQuery, br_objTrans)
            dr = Nothing
            CreateGateOut = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateActivityStatus() TABLE NAME:Activity_Status"
    Public Function UpdateActivityStatus(ByVal bv_strEquipmentNo As String, _
        ByVal bv_datActivityDate As DateTime, _
        ByVal bv_strGateInTransactionNo As String, _
        ByVal bv_i32DepoID As Int32, _
        ByVal bv_strYardLocation As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._V_ACTIVITY_STATUS).NewRow()
            With dr
                .Item(GateOutData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateOutData.ACTVTY_DT) = bv_datActivityDate
                .Item(GateOutData.GTOT_DT) = bv_datActivityDate
                .Item(GateOutData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(GateOutData.YRD_LCTN) = bv_strYardLocation
                .Item(GateOutData.DPT_ID) = bv_i32DepoID
            End With
            UpdateActivityStatus = objData.UpdateRow(dr, Activity_StatusUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateGateOut() TABLE NAME:Gatein"

    Public Function UpdateGateOut(ByVal bv_i64GateoutID As Int64, _
        ByVal bv_strYardLocation As String, _
        ByVal bv_datGateOutDate As DateTime, _
        ByVal bv_strGateOutTime As String, _
        ByVal bv_strEIR_NO As String, _
        ByVal bv_strVechicleNo As String, _
        ByVal bv_strTransporterCode As String, _
        ByRef bv_strGateInTransactionNo As String, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_remarks As String, _
        ByVal bv_intRentalCstmrID As Integer, _
        ByVal bv_StrSealNo As String, _
        ByVal bv_GradeId As Int32, _
        ByVal bv_GradeCode As String, _
        ByVal bv_AuthNo As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._GATEOUT).NewRow()
            With dr
                .Item(GateOutData.GTOT_ID) = bv_i64GateoutID
                If bv_strYardLocation <> Nothing Then
                    .Item(GateOutData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(GateOutData.YRD_LCTN) = DBNull.Value
                End If
                .Item(GateOutData.GTOT_DT) = bv_datGateOutDate
                If bv_strGateOutTime <> Nothing Then
                    .Item(GateOutData.GTOT_TM) = bv_strGateOutTime
                Else
                    .Item(GateOutData.GTOT_TM) = DBNull.Value
                End If
                If bv_strEIR_NO <> Nothing Then
                    .Item(GateOutData.EIR_NO) = bv_strEIR_NO
                Else
                    .Item(GateOutData.EIR_NO) = DBNull.Value
                End If
                If bv_strVechicleNo <> Nothing Then
                    .Item(GateOutData.VHCL_NO) = bv_strVechicleNo
                Else
                    .Item(GateOutData.VHCL_NO) = DBNull.Value
                End If
                If bv_strTransporterCode <> Nothing Then
                    .Item(GateOutData.TRNSPRTR_CD) = bv_strTransporterCode
                Else
                    .Item(GateOutData.TRNSPRTR_CD) = DBNull.Value
                End If
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(GateOutData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                Else
                    .Item(GateOutData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                If bv_strGateInTransactionNo <> Nothing Then
                    .Item(GateOutData.RMRKS_VC) = bv_remarks
                Else
                    .Item(GateOutData.RMRKS_VC) = DBNull.Value
                End If
                If bv_intRentalCstmrID <> Nothing Then
                    .Item(GateOutData.RNTL_CSTMR_ID) = bv_intRentalCstmrID
                Else
                    .Item(GateOutData.RNTL_CSTMR_ID) = DBNull.Value
                End If

                If bv_StrSealNo <> Nothing Then
                    .Item(GateOutData.SL_NO) = bv_StrSealNo
                Else
                    .Item(GateOutData.SL_NO) = DBNull.Value
                End If

                If bv_GradeId <> Nothing Then
                    .Item(GateOutData.GRD_ID) = bv_GradeId
                Else
                    .Item(GateOutData.GRD_ID) = DBNull.Value
                End If

                If bv_AuthNo <> Nothing Then
                    .Item(GateOutData.AUTH_NO) = bv_AuthNo
                Else
                    .Item(GateOutData.AUTH_NO) = DBNull.Value
                End If

                .Item(GateOutData.DPT_ID) = bv_i32DepotId
                .Item(GateOutData.MDFD_BY) = bv_strModifiedBy
                .Item(GateOutData.MDFD_DT) = bv_datModifiedDate
            End With
            UpdateGateOut = objData.UpdateRow(dr, GateOutUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateHandling_Charge"
    Public Function UpdateHandling_Charge(ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_datTo_BLLNG_DT As DateTime, _
        ByVal bv_strHNDLNG_CST_NC As Decimal, _
        ByVal bv_strHNDLNG_TX_RT_NC As Decimal, _
        ByVal bv_strTTL_CSTS_NC As Decimal, _
        ByVal bv_strYRD_LCTN As String, _
        ByVal strGI_Transaction As String, _
        ByVal intDepotId As Integer, _
        ByVal bv_strEIRNo As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._HANDLING_CHARGE).NewRow()
            With dr
                .Item(GateOutData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(GateOutData.FRM_BLLNG_DT) = bv_datTo_BLLNG_DT
                .Item(GateOutData.TO_BLLNG_DT) = bv_datTo_BLLNG_DT
                .Item(GateOutData.HNDLNG_CST_NC) = bv_strHNDLNG_CST_NC
                .Item(GateOutData.HNDLNG_TX_RT_NC) = bv_strHNDLNG_TX_RT_NC
                .Item(GateOutData.TTL_CSTS_NC) = bv_strTTL_CSTS_NC
                .Item(GateOutData.YRD_LCTN) = bv_strYRD_LCTN
                .Item(GateOutData.GI_TRNSCTN_NO) = strGI_Transaction
                .Item(GateOutData.RFRNC_EIR_NO_1) = bv_strEIRNo
                .Item(GateOutData.DPT_ID) = intDepotId
            End With
            UpdateHandling_Charge = objData.UpdateRow(dr, UpdateHandlingStorageQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateStorage_Charge"
    Public Function UpdateStorage_Charge(ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_datto_BLLNG_DT As DateTime, _
        ByVal bv_strYRD_LCTN As String, _
        ByVal bv_intDepotId As Integer, _
        ByVal strGI_Transaction As String, _
        ByVal bv_strEIRNo As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._STORAGE_CHARGE).NewRow()
            With dr
                .Item(GateOutData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(GateOutData.TO_BLLNG_DT) = bv_datto_BLLNG_DT
                .Item(GateOutData.YRD_LCTN) = bv_strYRD_LCTN
                .Item(GateOutData.DPT_ID) = bv_intDepotId
                .Item(GateOutData.GI_TRNSCTN_NO) = strGI_Transaction
                .Item(GateOutData.RFRNC_EIR_NO_1) = bv_strEIRNo
            End With
            UpdateStorage_Charge = objData.UpdateRow(dr, updateStorageCharge, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetHanldingInCharge"
    Public Function GetCustomerHanldingInCharge(ByVal bv_intCustomerID As Long, _
                                        ByVal bv_intCodeID As Integer, _
                                        ByVal bv_intTypeID As Integer, _
                                        ByVal bv_intDepotID As Integer, _
                                        ByRef br_ObjTransactions As Transactions) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dtHandlingCharge As New DataTable
            hshTable.Add(GateOutData.CSTMR_ID, bv_intCustomerID)
            hshTable.Add(GateOutData.EQPMNT_CD_ID, bv_intCodeID)
            hshTable.Add(GateOutData.EQPMNT_TYP_ID, bv_intTypeID)
            hshTable.Add(GateOutData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(CutomerChargeDetailSelectQuery, hshTable)
            objData.Fill(dtHandlingCharge)
            Return dtHandlingCharge
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateGateOut() TABLE NAME:GateOut"

    Public Function CreateRentalGateOut(ByVal bv_i64CustomerID As Int64, _
        ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64EquipmentTypeId As Int64, _
        ByVal bv_i64EquipmentCodeId As Int64, _
        ByVal bv_i64EquipmentStatusId As Int64, _
        ByVal bv_strYardLocation As String, _
        ByVal bv_datGateOutDate As DateTime, _
        ByVal bv_strGateOutTime As String, _
        ByVal bv_strEIR_NO As String, _
        ByVal bv_strVechicleNo As String, _
        ByVal bv_strTransporterCode As String, _
        ByRef bv_strGateInTransactionNo As String, _
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_strRemarks As String, _
        ByVal bv_intRntlCstmrID As Integer, _
        ByVal bv_strRntlRefNo As String, _
        ByVal bv_blnRntlBt As Boolean, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._GATEOUT).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GateOutData._GATEOUT, br_objTrans)
                .Item(GateOutData.GTOT_ID) = intMax
                'From Index Pattern
                .Item(GateOutData.GTOT_CD) = CommonUIs.GetIdentityCode(GateOutData._GATEOUT, intMax, bv_datGateOutDate, br_objTrans)
                '.Item(GateOutData.GTOT_CD) = IndexPatterns.GetIdentityCode(GateOutData._GATEOUT, intMax, bv_datGateOutDate, bv_i32DepotId, br_objTrans)
                .Item(GateOutData.CSTMR_ID) = bv_i64CustomerID
                .Item(GateOutData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateOutData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                .Item(GateOutData.EQPMNT_CD_ID) = bv_i64EquipmentCodeId
                .Item(GateOutData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                If bv_strYardLocation <> Nothing Then
                    .Item(GateOutData.YRD_LCTN) = bv_strYardLocation
                Else
                    .Item(GateOutData.YRD_LCTN) = DBNull.Value
                End If
                .Item(GateOutData.GTOT_DT) = bv_datGateOutDate
                If bv_strGateOutTime <> Nothing Then
                    .Item(GateOutData.GTOT_TM) = bv_strGateOutTime
                Else
                    .Item(GateOutData.GTOT_TM) = DBNull.Value
                End If
                If bv_strEIR_NO <> Nothing Then
                    .Item(GateOutData.EIR_NO) = bv_strEIR_NO
                Else
                    .Item(GateOutData.EIR_NO) = DBNull.Value
                End If
                If bv_strVechicleNo <> Nothing Then
                    .Item(GateOutData.VHCL_NO) = bv_strVechicleNo
                Else
                    .Item(GateOutData.VHCL_NO) = DBNull.Value
                End If
                If bv_strTransporterCode <> Nothing Then
                    .Item(GateOutData.TRNSPRTR_CD) = bv_strTransporterCode
                Else
                    .Item(GateOutData.TRNSPRTR_CD) = DBNull.Value
                End If
                .Item(GateOutData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(GateOutData.DPT_ID) = bv_i32DepotId
                .Item(GateOutData.CRTD_BY) = bv_strCreatedBy
                .Item(GateOutData.CRTD_DT) = bv_datCreatedDate
                .Item(GateOutData.MDFD_BY) = bv_strModifiedBy
                .Item(GateOutData.MDFD_DT) = bv_datModifiedDate
                If bv_strRemarks <> Nothing Then
                    .Item(GateOutData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(GateOutData.RMRKS_VC) = DBNull.Value
                End If
                If bv_strRntlRefNo <> Nothing Then
                    .Item(GateOutData.RNTL_RFRNC_NO) = bv_strRntlRefNo
                Else
                    .Item(GateOutData.RNTL_RFRNC_NO) = DBNull.Value
                End If
                If bv_intRntlCstmrID <> Nothing Then
                    .Item(GateOutData.RNTL_CSTMR_ID) = bv_intRntlCstmrID
                Else
                    .Item(GateOutData.RNTL_CSTMR_ID) = DBNull.Value
                End If
                .Item(GateOutData.RNTL_BT) = bv_blnRntlBt
            End With
            objData.InsertRow(dr, GateOutRentalInsertQuery, br_objTrans)
            dr = Nothing
            CreateRentalGateOut = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateActivityStatus_NewMode() TABLE NAME:Activity_Status"
    Public Function UpdateActivityStatus_NewMode(ByVal bv_strEquipmentNo As String, _
        ByVal bv_i64EquipmentStatusId As Int64, _
        ByVal bv_strActivity As String, _
        ByVal bv_datActivityDate As DateTime, _
        ByVal bv_avtivity_bit As Boolean, _
        ByVal bv_strGateInTransactionNo As String, _
        ByVal bv_i32DepoID As Int32, _
        ByVal bv_strYardLocation As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._V_ACTIVITY_STATUS).NewRow()
            With dr
                .Item(GateOutData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateOutData.EQPMNT_STTS_ID) = bv_i64EquipmentStatusId
                .Item(GateOutData.ACTVTY_NAM) = bv_strActivity
                .Item(GateOutData.ACTVTY_DT) = bv_datActivityDate
                .Item(GateOutData.GTOT_DT) = bv_datActivityDate
                .Item(GateOutData.ACTV_BT) = bv_avtivity_bit
                .Item(GateOutData.GI_TRNSCTN_NO) = bv_strGateInTransactionNo
                .Item(GateOutData.YRD_LCTN) = bv_strYardLocation
                .Item(GateOutData.DPT_ID) = bv_i32DepoID
            End With
            UpdateActivityStatus_NewMode = objData.UpdateRow(dr, Activity_StatusUpdateQuery_NewMode, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateTracking() TABLE NAME:Tracking"

    Public Function UpdateTracking(ByVal bv_strEQPMNT_NO As String, _
                ByVal bv_datActivity As DateTime, _
                ByVal bv_strActivityName As String, _
                ByVal strGI_TRNSCTN_NO As String, _
                ByVal bv_strReferenceNo As String, _
                ByVal bv_strRemarks As String, _
                ByVal bv_i32DepotId As Int32, _
                ByVal bv_strModifiedBy As String, _
                ByVal bv_dtModifiedDate As DateTime, _
                ByVal bv_intRntlCstmrID As Integer, _
                ByVal bv_strRntlRefNo As String, _
                ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._TRACKING).NewRow()
            With dr
                .Item(GateOutData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(GateOutData.ACTVTY_DT) = bv_datActivity
                If strGI_TRNSCTN_NO <> Nothing Then
                    .Item(GateOutData.GI_TRNSCTN_NO) = strGI_TRNSCTN_NO
                Else
                    .Item(GateOutData.GI_TRNSCTN_NO) = DBNull.Value
                End If
                If bv_strReferenceNo <> Nothing Then
                    .Item(GateOutData.RFRNC_NO) = bv_strReferenceNo
                Else
                    .Item(GateOutData.RFRNC_NO) = DBNull.Value
                End If
                .Item(GateOutData.DPT_ID) = bv_i32DepotId
                .Item(GateOutData.ACTVTY_NAM) = bv_strActivityName
                If bv_strRemarks <> Nothing Then
                    .Item(GateOutData.ACTVTY_RMRKS) = bv_strRemarks
                Else
                    .Item(GateOutData.ACTVTY_RMRKS) = DBNull.Value
                End If
                .Item(GateOutData.MDFD_BY) = bv_strModifiedBy
                .Item(GateOutData.MDFD_DT) = bv_dtModifiedDate
            End With
            UpdateTracking = objData.UpdateRow(dr, TrackingUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateRentalEntry()"
    Public Function UpdateRentalEntry(ByVal bv_strEquipmentNo As String, _
        ByVal bv_RentalRefNo As String, _
        ByVal bv_datGateOutDate As DateTime, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._RENTAL_ENTRY).NewRow()
            With dr
                .Item(GateOutData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateOutData.RNTL_RFRNC_NO) = bv_RentalRefNo
                .Item(GateOutData.ON_HR_DT) = bv_datGateOutDate
            End With
            UpdateRentalEntry = objData.UpdateRow(dr, RentalEntryUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEqpmntDetails() "
    Public Function GetEqpmntDetails(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Integer, ByRef br_ObjTransactions As Transactions) As GateOutDataSet
        Try
            Dim hshTable As New Hashtable()
            hshTable.Add(GateOutData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(GateOutData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(EquipmentNoActivityStatusSelectQuery, hshTable)
            objData.Fill(CType(ds, DataSet), GateOutData._V_ACTIVITY_STATUS, br_ObjTransactions)
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
            Dim dtRental As New DataTable
            hshTable.Add(GateOutData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(GateOutData.CSTMR_ID, bv_intCstmrID)
            objData = New DataObjects(RentalCustomerSelectQuery, hshTable)
            objData.Fill(dtRental, br_ObjTransactions)
            Return dtRental
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateRentalCharge() TABLE NAME:Rental_Charge"

    Public Function CreateRentalCharge(ByVal bv_strEQPMNT_NO As String, _
        ByVal bv_strRNTL_TYP As String, _
        ByVal bv_strRFRNC_EIR_NO_1 As String, _
        ByVal bv_strRFRNC_EIR_NO_2 As String, _
        ByVal bv_datFRM_BLLNG_DT As DateTime, _
        ByVal bv_datTO_BLLNG_DT As DateTime, _
        ByVal bv_dblHNDLNG_OT_NC As Decimal, _
        ByVal bv_dblHNDLNG_IN_NC As Decimal, _
        ByVal bv_dblON_HR_SRVY_NC As Decimal, _
        ByVal bv_dblOFF_HR_SRVY_NC As Decimal, _
        ByVal bv_i32FR_DYS As Int32, _
        ByVal bv_i32NO_OF_DYS As Int32, _
        ByVal bv_dblRNTL_PR_DY_NC As Double, _
        ByVal bv_dblRNTL_TX_RT_NC As Double, _
        ByVal bv_dblTTL_CSTS_NC As Double, _
        ByVal bv_strRNTL_CNTN_FLG As String, _
        ByVal bv_strBLLNG_FLG As String, _
        ByVal bv_strIS_GT_IN_FLG As String, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_blnIS_LT_FLG As Boolean, _
        ByVal bv_datBLLNG_TLL_DT As DateTime, _
        ByVal bv_strYRD_LCTN As String, _
        ByVal bv_strBLLNG_TYP_CD As String, _
        ByVal bv_i64CSTMR_ID As Int64, _
        ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_strGI_TRNSCTN_NO As String, _
        ByVal bv_strRNTL_RFRNC_NO As String, _
        ByVal bv_strON_HR_BLLNG_FLG As String, _
        ByVal bv_strOFF_HR_BLLNG_FLG As String, _
        ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._RENTAL_CHARGE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GateOutData._RENTAL_CHARGE, br_ObjTransactions)
                .Item(GateOutData.RNTL_CHRG_ID) = intMax
                .Item(GateOutData.EQPMNT_NO) = bv_strEQPMNT_NO
                .Item(GateOutData.RNTL_TYP) = bv_strRNTL_TYP
                .Item(GateOutData.RFRNC_EIR_NO_1) = bv_strRFRNC_EIR_NO_1
                If bv_strRFRNC_EIR_NO_2 <> Nothing Then
                    .Item(GateOutData.RFRNC_EIR_NO_2) = bv_strRFRNC_EIR_NO_2
                Else
                    .Item(GateOutData.RFRNC_EIR_NO_2) = DBNull.Value
                End If
                .Item(GateOutData.FRM_BLLNG_DT) = bv_datFRM_BLLNG_DT
                If bv_datTO_BLLNG_DT <> Nothing Then
                    .Item(GateOutData.TO_BLLNG_DT) = bv_datTO_BLLNG_DT
                Else
                    .Item(GateOutData.TO_BLLNG_DT) = DBNull.Value
                End If
                .Item(GateOutData.HNDLNG_OT_NC) = bv_dblHNDLNG_OT_NC
                .Item(GateOutData.HNDLNG_IN_NC) = bv_dblHNDLNG_IN_NC
                .Item(GateOutData.ON_HR_SRVY_NC) = bv_dblON_HR_SRVY_NC
                .Item(GateOutData.OFF_HR_SRVY_NC) = bv_dblOFF_HR_SRVY_NC
                .Item(GateOutData.FR_DYS) = bv_i32FR_DYS
                If bv_i32NO_OF_DYS <> 0 Then
                    .Item(GateOutData.NO_OF_DYS) = bv_i32NO_OF_DYS
                Else
                    .Item(GateOutData.NO_OF_DYS) = DBNull.Value
                End If
                .Item(GateOutData.RNTL_PR_DY_NC) = bv_dblRNTL_PR_DY_NC
                .Item(GateOutData.RNTL_TX_RT_NC) = bv_dblRNTL_TX_RT_NC
                .Item(GateOutData.TTL_CSTS_NC) = bv_dblTTL_CSTS_NC
                .Item(GateOutData.RNTL_CNTN_FLG) = bv_strRNTL_CNTN_FLG
                .Item(GateOutData.BLLNG_FLG) = bv_strBLLNG_FLG
                .Item(GateOutData.IS_GT_IN_FLG) = bv_strIS_GT_IN_FLG
                .Item(GateOutData.ACTV_BT) = bv_blnACTV_BT
                .Item(GateOutData.IS_LT_FLG) = bv_blnIS_LT_FLG
                If bv_datBLLNG_TLL_DT <> Nothing Then
                    .Item(GateOutData.BLLNG_TLL_DT) = bv_datBLLNG_TLL_DT
                Else
                    .Item(GateOutData.BLLNG_TLL_DT) = DBNull.Value
                End If
                If bv_strYRD_LCTN <> Nothing Then
                    .Item(GateOutData.YRD_LCTN) = bv_strYRD_LCTN
                Else
                    .Item(GateOutData.YRD_LCTN) = DBNull.Value
                End If
                If bv_strBLLNG_TYP_CD <> Nothing Then
                    .Item(GateOutData.BLLNG_TYP_CD) = bv_strBLLNG_TYP_CD
                Else
                    .Item(GateOutData.BLLNG_TYP_CD) = DBNull.Value
                End If
                .Item(GateOutData.CSTMR_ID) = bv_i64CSTMR_ID
                .Item(GateOutData.DPT_ID) = bv_i32DPT_ID
                .Item(GateOutData.GI_TRNSCTN_NO) = bv_strGI_TRNSCTN_NO
                .Item(GateOutData.RNTL_RFRNC_NO) = bv_strRNTL_RFRNC_NO
                .Item(GateOutData.ON_HR_BLLNG_FLG) = bv_strON_HR_BLLNG_FLG
                .Item(GateOutData.OFF_HR_BLLNG_FLG) = bv_strOFF_HR_BLLNG_FLG
            End With
            objData.InsertRow(dr, Rental_ChargeInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateRentalCharge = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateRentalCharge()"
    Public Function UpdateRentalCharge(ByVal bv_strEquipmentNo As String, _
                                       ByVal bv_datGateOutDate As DateTime, _
                                       ByVal bv_YardLocation As String, _
                                       ByVal bv_RentalRefNo As String, _
                                       ByVal bv_dptID As Integer, _
                                       ByVal bv_blnIsLateFlag As Boolean, _
                                       ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._RENTAL_CHARGE).NewRow()
            With dr
                .Item(GateOutData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(GateOutData.FRM_BLLNG_DT) = bv_datGateOutDate
                If bv_YardLocation <> Nothing Then
                    .Item(GateOutData.YRD_LCTN) = bv_YardLocation
                Else
                    .Item(GateOutData.YRD_LCTN) = DBNull.Value
                End If
                .Item(GateOutData.RNTL_RFRNC_NO) = bv_RentalRefNo
                .Item(GateOutData.DPT_ID) = bv_dptID
                .Item(GateOutData.IS_LT_FLG) = bv_blnIsLateFlag
            End With
            UpdateRentalCharge = objData.UpdateRow(dr, RentalChargeUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRentalEntryDetails() "
    Public Function GetRentalEntryDetails(ByVal bv_strEquipmentNo As String, ByVal bv_intCstmrID As Integer, ByVal bv_strRentalRefNo As String, ByRef br_ObjTransactions As Transactions) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dtRental As New DataTable
            hshTable.Add(GateOutData.EQPMNT_NO, bv_strEquipmentNo)
            hshTable.Add(GateOutData.CSTMR_ID, bv_intCstmrID)
            hshTable.Add(GateOutData.RNTL_RFRNC_NO, bv_strRentalRefNo)
            objData = New DataObjects(RentalEntrySelectQuery, hshTable)
            objData.Fill(dtRental, br_ObjTransactions)
            Return dtRental
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetRentalChargeDetails() "
    Public Function GetRentalChargeDetails(ByVal bv_intCstmrID As Integer, _
                                           ByVal bv_intDepotID As Integer, _
                                           ByRef br_ObjTransactions As Transactions) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dtRentalCharge As New DataTable
            hshTable.Add(GateOutData.CSTMR_ID, bv_intCstmrID)
            hshTable.Add(GateOutData.DPT_ID, bv_intDepotID)
            objData = New DataObjects(RentalChargeSelectQuery, hshTable)
            objData.Fill(dtRentalCharge, br_ObjTransactions)
            Return dtRentalCharge
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetGateInByGateinTransactionNo() TABLE NAME:GATEIN"

    Public Function GetGateInByGateinTransactionNo(ByVal bv_strGateinTrasnactionNo As String, _
                                                   ByVal bv_i32DepotId As Int32, _
                                                   ByRef br_objTrans As Transactions) As GateOutDataSet
        Try
            Dim hshparameters As New Hashtable
            Dim dsGateinDataSet As New GateOutDataSet
            hshparameters.Add(GateOutData.GI_TRNSCTN_NO, bv_strGateinTrasnactionNo)
            hshparameters.Add(GateOutData.DPT_ID, bv_i32DepotId)
            objData = New DataObjects(GateinSelectQueryByGateinTransactionNo, hshparameters)
            objData.Fill(CType(dsGateinDataSet, DataSet), GateOutData._GATEIN, br_objTrans)
            Return dsGateinDataSet
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    ''21787 Fix
#Region "UpdateStorageGateOutFlag() TABLE NAME: STORAGE_CHARGE"

    Public Function UpdateStorageGateOutFlag(
        ByVal bv_i32DepotId As Int32, _
        ByVal bv_strGTN_TRNSXN As String, _
        ByVal gtotFlag As String, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateOutData._STORAGE_CHARGE).NewRow()
            With dr

                .Item(GateOutData.DPT_ID) = bv_i32DepotId
                .Item(GateOutData.IS_GT_OT_FLG) = gtotFlag
                .Item(GateOutData.GI_TRNSCTN_NO) = bv_strGTN_TRNSXN
            End With
            UpdateStorageGateOutFlag = objData.UpdateRow(dr, StorageChargeGateOutFlagUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
    ''

#Region "GetGateOutRetTransxNo() "
    Public Function GetGateOutRetTransxNo(ByVal bv_lngGateoutId As Int64, ByVal bv_GOdate As DateTime, ByRef br_objTrans As Transactions) As String
        Try
            Dim hshTable As New Hashtable()
            Dim dtRental As New DataTable

            Dim strTransx As String = CommonUIs.GetIdentityCode(GateOutData._GATEOUT, bv_lngGateoutId, bv_GOdate, br_objTrans)
            'hshTable.Add(GateOutData.EQPMNT_NO, bv_strEquipmentNo)
            'hshTable.Add(GateOutData.CSTMR_ID, bv_intCstmrID)
            'hshTable.Add(GateOutData.RNTL_RFRNC_NO, bv_strRentalRefNo)
            'objData = New DataObjects(RentalEntrySelectQuery, hshTable)
            'objData.Fill(dtRental, br_ObjTransactions)
            Return strTransx
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "GetEquipmentDescription() "
    Public Function GetEquipmentDescription(ByVal bv_strEquipmentCode As String, ByRef br_objTrans As Transactions) As String
        Try
            Dim hshTable As New Hashtable()
            Dim dtRental As New DataTable
            Dim equipmentDescription As String = String.Empty

            '  Dim strTransx As String = CommonUIs.GetIdentityCode(GateOutData._GATEOUT, bv_lngGateoutId, bv_GOdate, br_objTrans)
            objData = New DataObjects(getEquipmentCodeDescriptionQuery, GateOutData.EQPMNT_CD_CD, bv_strEquipmentCode)
            objData.Fill(CType(ds, DataSet), GateinData._EQUIPMENT_TYPE)
            ' Return ds
            If ds.Tables("EQUIPMENT_TYPE").Rows.Count > 0 Then
                equipmentDescription = ds.Tables("EQUIPMENT_TYPE").Rows(0).Item("EQPMNT_TYP_DSCRPTN_VC")
            End If

            Return equipmentDescription
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetAttchemntbyGateIN()"

    Public Function pub_GetAttchemntbyGateIN(ByVal bv_intGateInID As Integer, ByVal bv_strActivity As String, ByRef objTrans As Transactions) As GateOutDataSet
        Try
            Dim hshTable As New Hashtable()
            Dim dtRental As New DataTable
            hshTable.Add(GateOutData.RPR_ESTMT_ID, bv_intGateInID)
            hshTable.Add(GateOutData.ACTVTY_NAM, bv_strActivity)
            objData = New DataObjects(GetAttachmentByGateIN, hshTable)
            objData.Fill(CType(ds, DataSet), GateOutData._V_GATEOUT, objTrans)
            Return ds

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
#Region "pub_GetAttchemntbyGateINAttachment()"

    Public Function pub_GetAttchemntbyGateINAttachment(ByVal bv_intGateInID As Integer, ByVal bv_strActivity As String, ByRef objTrans As Transactions) As GateOutDataSet
        Try
            Dim hshTable As New Hashtable()
            Dim dtRental As New DataTable
            hshTable.Add(GateOutData.RPR_ESTMT_ID, bv_intGateInID)
            hshTable.Add(GateOutData.ACTVTY_NAM, bv_strActivity)
            objData = New DataObjects(GetAttachmentByGateINAttachment, hshTable)
            objData.Fill(CType(ds, DataSet), GateOutData._ATTACHMENT, objTrans)
            Return ds

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
#Region "GetAttachmentByRepairEstimateId()"
    Public Function GetAttachmentByRepairEstimateId(ByVal bv_i32DepotID As Int32, ByVal bv_strActivity As String, ByVal bv_i64RepairEstimateId As Int64, _
                                                        ByRef br_objTransaction As Transactions) As GateOutDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(GateOutData.DPT_ID, bv_i32DepotID)
            hshparameters.Add(GateOutData.RPR_ESTMT_ID, bv_i64RepairEstimateId)
            hshparameters.Add(GateOutData.ACTVTY_NAM, bv_strActivity)
            objData = New DataObjects(AttachmentSelectWithActivityQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), GateOutData._ATTACHMENT, br_objTransaction)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetEquipmentInformation"
    Public Function pub_GetEquipmentInformation(bv_strEquipment As String) As GateOutDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(GateOutData.EQPMNT_NO, bv_strEquipment)
            objData = New DataObjects(GetEquipmentInformationQuery, hshTable)
            objData.Fill(CType(ds, DataSet), GateOutData._EQUIPMENT_INFORMATION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


    Public Function GetAgenIdFromCustomer(ByVal bv_StrCustomerId As String, _
                                        ByVal bv_intDepotID As Integer, _
                                        ByRef br_ObjTransactions As Transactions) As DataTable
        Try
            Dim hshTable As New Hashtable()
            Dim dt As New DataTable
            hshTable.Add(GateinData.CSTMR_ID, bv_StrCustomerId)
            hshTable.Add(GateinData.DPT_ID, bv_intDepotID)

            objData = New DataObjects(GetAgenIdFromCustomer_SelectQry, hshTable)
            objData.Fill(dt, br_ObjTransactions)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Delete from GateOut Ret Table - Avoid dupilicate Insert
    Public Function DeleteGateoutRET(ByVal bv_i64GateOutId As Int64, ByRef br_objtrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(GateOutData._GATEOUT_RET).NewRow()
            With dr
                .Item(GateOutData.GO_SNO) = bv_i64GateOutId
            End With
            DeleteGateoutRET = objData.DeleteRow(dr, DeleteGateoutRET_DeleteQry, br_objtrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class




#End Region