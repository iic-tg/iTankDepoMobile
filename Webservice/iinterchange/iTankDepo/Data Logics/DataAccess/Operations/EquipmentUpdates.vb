Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

#Region "Class EquipmentUpdates"
Public Class EquipmentUpdates

#Region "Declaration Part.."
    Dim objData As DataObjects
    Private Const V_Equipment_UpdateSelectQuery As String = "SELECT EQPMNT_INFRMTN_ID,EQPMNT_NO,MNFCTR_DT,TR_WGHT_NC,GRSS_WGHT_NC,CPCTY_NC,LST_TST_DT,NXT_TST_DT,VLDTY_PRD_TST_YRS,LST_TST_TYP_ID,LST_TST_TYP_CD,NXT_TST_TYP_ID,NXT_TST_TYP_CD,LST_SRVYR_NM,ACTV_BT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,GI_RF_NO,INSTRCTNS_VC,ACTVTY_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CSTMR_ID,CSTMR_CD,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,INVC_GNRT_BT,GI_TRNSCTN_NO,RNTL_BT,DPT_ID,DPT_CD,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT FROM V_EQUIPMENT_UPDATE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const EquipmentUpdateSelectQuery As String = "SELECT EQUIPMENT_UPDATE_ID,TABLE_NAME,UPDATE_FIELDS,ACTIVE_BIT FROM EQUIPMENT_UPDATE WHERE ACTIVE_BIT=1"
    Private Const ActivityStatusUpdateQuery As String = "UPDATE ACTIVITY_STATUS SET PRDCT_ID=@PRDCT_ID, GI_RF_NO=@GI_RF_NO, INSTRCTNS_VC=@INSTRCTNS_VC WHERE "
    Private Const EquipmentInformationUpdateQuery As String = "UPDATE EQUIPMENT_INFORMATION SET LST_TST_DT=@LST_TST_DT,EQPMNT_TYP_ID=@EQPMNT_TYP_ID,NXT_TST_DT=@NXT_TST_DT,LST_TST_TYP_ID=@LST_TST_TYP_ID,NXT_TST_TYP_ID=@NXT_TST_TYP_ID,VLDTY_PRD_TST_YRS=@VLDTY_PRD_TST_YRS,LST_SRVYR_NM=@LST_SRVYR_NM,RMRKS_VC=@RMRKS_VC WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const Audit_LogInsertQuery As String = "INSERT INTO AUDIT_LOG(ADT_LG_ID,EQPMNT_NO,ACTVTY_NAM,ACTN_VC,ACTN_DT,OLD_VL,NEW_VL,RSN_VC,MDFD_BY,DPT_ID)VALUES(@ADT_LG_ID,@EQPMNT_NO,@ACTVTY_NAM,@ACTN_VC,@ACTN_DT,@OLD_VL,@NEW_VL,@RSN_VC,@MDFD_BY,@DPT_ID)"
    Private Const ActivityStatusSelectQueryByDepotId As String = "SELECT ACTVTY_STTS_ID FROM ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND ACTV_BT=1"
    Private Const GateinUpdateQuery As String = "UPDATE GATEIN SET PRDCT_ID=@PRDCT_ID WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const GateinUpdateEirNoQuery As String = "UPDATE GATEIN SET EIR_NO=@EIR_NO WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const PreAdviceSelectQuery As String = "SELECT PR_ADVC_ID FROM PRE_ADVICE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const GateoutSelectQuery As String = "SELECT GTOT_DT FROM GATEOUT WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID= @DPT_ID"
    Private Const GateoutUpdateQuery As String = "UPDATE GATEOUT SET EIR_NO=@EIR_NO WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const Activity_StatusSelectQuery As String = "SELECT ACTVTY_STTS_ID,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,GTN_DT,GTOT_DT,CLNNG_DT,INSPCTN_DT,RPR_CMPLTN_DT,PRDCT_ID,EQPMNT_STTS_ID,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSTRCTNS_VC,YRD_LCTN,ACTV_BT,DPT_ID FROM ACTIVITY_STATUS WHERE EQPMNT_NO = @EQPMNT_NO AND DPT_ID = @DPT_ID"
    Private Const Equipment_InformationSelectQuery As String = "SELECT COUNT(EQPMNT_INFRMTN_ID) AS EQPMNT_INFRMTN_ID FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID= @DPT_ID"
    Private Const ActivityStatusGateOutSelectQuery As String = "SELECT COUNT(ACTVTY_STTS_ID) AS ACTVTY_STTS_ID  FROM V_ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO  AND GTOT_DT IS NOT NULL "
    Private Const HandlingChargeUpdateQuery As String = "UPDATE HANDLING_CHARGE SET HNDLNG_CST_NC =@HNDLNG_CST_NC, TTL_CSTS_NC=@TTL_CSTS_NC WHERE EQPMNT_NO=@EQPMNT_NO AND CST_TYP=@CST_TYP AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const CustomerHandlingInSelectQueryById As String = "SELECT HNDLNG_IN_CHRG_NC FROM CUSTOMER_CHARGE_DETAIL WHERE CSTMR_ID=@CSTMR_ID AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID AND EQPMNT_CD_ID=@EQPMNT_CD_ID"
    Private Const CustomerHandlingOutSelectQueryById As String = "SELECT HNDLNG_OUT_CHRG_NC FROM CUSTOMER_CHARGE_DETAIL WHERE CSTMR_ID=@CSTMR_ID AND EQPMNT_TYP_ID=@EQPMNT_TYP_ID AND EQPMNT_CD_ID=@EQPMNT_CD_ID"
    Private Const SupplierEquipmentDetailSelectQuery As String = "SELECT SPPLR_ID FROM SUPPLIER WHERE SPPLR_ID IN  (SELECT SPPLR_ID FROM SUPPLIER_EQUIPMENT_DETAIL WHERE EQPMNT_NO= @EQPMNT_NO) AND DPT_ID =@DPT_ID"
    Dim sqlDbnull As DateTime = "1900-01-01 00:00:00.000"
    Dim sqlDtnull = "19000101"
    Dim sqlMnthnull = "01/00"

    'Release 3 Changes
    Private Const Get_RentalStatus_select As String = "SELECT EQPMNT_INFRMTN_ID FROM V_RENTAL_EQUIPMENT WHERE DPT_ID=@DPT_ID AND EQPMNT_NO=@EQPMNT_NO"
    Private Const Get_LastActivityStatus_Select As String = "SELECT EQPMNT_STTS_CD FROM V_ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO =@GI_TRNSCTN_NO ORDER BY ACTVTY_STTS_ID DESC"
    Private Const Get_ValidateActivityStatus_Select As String = "SELECT ACTVTY_STTS_ID FROM V_ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND EQPMNT_STTS_CD IN ('IND','AVL') ORDER BY ACTVTY_STTS_ID DESC"
    Private Const Get_GateIn_GITransaction_No_Select As String = "select GI_TRNSCTN_NO from  V_EQUIPMENT_UPDATE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const Get_GateIn_Date_Select As String = "SELECT GTN_DT FROM GATEIN WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const Get_Equipment_Info_Select As String = "SELECT EQPMNT_NO,EQPMNT_TYP_ID,MNFCTR_DT,TR_WGHT_NC,GRSS_WGHT_NC,CPCTY_NC,LST_TST_DT,NXT_TST_DT,VLDTY_PRD_TST_YRS,LST_TST_TYP_ID,NXT_TST_TYP_ID,LST_SRVYR_NM,DPT_ID,ACTV_BT,RNTL_BT,RMRKS_VC FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const Get_GateIn_Info_Select As String = "SELECT GTN_ID,GTN_CD,CSTMR_ID,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_CD_ID,EQPMNT_STTS_ID,YRD_LCTN,GTN_DT,GTN_TM,PRDCT_ID,EIR_NO,VHCL_NO,TRNSPRTR_CD,HTNG_BT,RMRKS_VC,GI_TRNSCTN_NO,RNTL_RFRNC_NO,GTOT_BT,RNTL_BT,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,SNO FROM GATEIN WHERE DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const get_LastActivity_Date_Select As String = "SELECT actvty_dt FROM V_ACTIVITY_STATUS WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO =@GI_TRNSCTN_NO ORDER BY ACTVTY_STTS_ID DESC"
    Private Const Get_RentalStatus_select1 As String = "SELECT RNTL_ENTRY_ID,EQPMNT_NO,CSTMR_ID,CSTMR_CD,CSTMR_NAM,CNTRCT_RFRNC_NO,PO_RFRNC_NO,ON_HR_DT,OFF_HR_DT,RMRKS_VC,RNTL_RFRNC_NO,DPT_ID,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,EQPMNT_INFRMTN_ID,IS_GTOT_BT,IS_GTN_BT,BLLNG_FLG,GTOT_DT,CNTRCT_STRT_DT,GTIN_DT,ALLOW_EDIT FROM V_RENTAL_ENTRY RE WHERE DPT_ID=@DPT_ID AND RNTL_RFRNC_NO NOT IN (SELECT RNTL_RFRNC_NO FROM RENTAL_CHARGE WHERE RNTL_RFRNC_NO=RE.RNTL_RFRNC_NO AND EQPMNT_NO=RE.EQPMNT_NO AND BLLNG_FLG='B' AND RNTL_CNTN_FLG='S')  and eqpmnt_no=@eqpmnt_no  ORDER BY RNTL_ENTRY_ID"
    Private Const get_LastActivity_Date_Select1 As String = "SELECT MAX(ACTVTY_DT) AS ACTVTY_DT FROM TRACKING WHERE EQPMNT_NO=@EQPMNT_NO AND ACTVTY_NAM <> 'CHANGE OF STATUS' AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CNCLD_DT IS NULL"
    Private Const ValidateEquipment_Exist_select1 As String = "SELECT COUNT(EQPMNT_INFRMTN_ID) FROM  EQUIPMENT_INFORMATION WHERE EQPMNT_NO =@EQPMNT_NO AND DPT_ID =@DPT_ID"
    Private Const get_EffectiveFrom_Date_Select As String = "SELECT STRG_CHRG_ID,EQPMNT_NO,EQPMNT_CD_ID,EQPMNT_TYP_ID,CST_TYP,RFRNC_EIR_NO_1,RFRNC_EIR_NO_2,FRM_BLLNG_DT,TO_BLLNG_DT,FR_DYS,NO_OF_DYS,STRG_CST_NC,STRG_TX_RT_NC,TTL_CSTS_NC,STRG_CNTN_FLG,BLLNG_FLG,IS_GT_OT_FLG,ACTV_BT,IS_LT_FLG,BLLNG_TLL_DT,YRD_LCTN,BLLNG_TYP_CD,CSTMR_ID,DPT_ID,HTNG_BT,GI_TRNSCTN_NO,DRFT_INVC_NO,FNL_INVC_NO FROM STORAGE_CHARGE WHERE GI_TRNSCTN_NO =@GI_TRNSCTN_NO  AND DPT_ID=@DPT_ID"
    Private Const get_EffectiveFrom_DateOnly_Select As String = "SELECT max(FRM_BLLNG_DT) as FRM_BLLNG_DT FROM STORAGE_CHARGE WHERE GI_TRNSCTN_NO =@GI_TRNSCTN_NO  AND DPT_ID=@DPT_ID"
    Private Const Get_Storage_chargeCount_Select As String = "SELECT COUNT(STRG_CHRG_ID) FROM STORAGE_CHARGE WHERE "

    'Release 3 Changes - Biiling Validation
    Private Const BillingValidation_CleaningCharge_Select As String = "SELECT CLNNG_CHRG_ID FROM CLEANING_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND BLLNG_FLG ='B' AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const BillingValidation_HandlingCharge_Select As String = "SELECT HNDLNG_CHRG_ID FROM HANDLING_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND BLLNG_FLG ='B' AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const BillingValidation_StorageCharge_Select As String = "SELECT STRG_CHRG_ID FROM STORAGE_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND BLLNG_FLG ='B' AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const BillingValidation_RepairCharge_Select As String = "SELECT RPR_CHRG_ID FROM REPAIR_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND BLLNG_FLG ='B' AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const BillingValidation_HeatingCharge_Select As String = "SELECT HTNG_ID FROM HEATING_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND BLLNG_FLG ='B' AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const BillingValidation_RentalCharge_Select As String = "SELECT RNTL_CHRG_ID FROM RENTAL_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND BLLNG_FLG ='B' AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO"
    Private Const Validation_StorageCharge_Select As String = "SELECT COUNT(STRG_CHRG_ID) FROM STORAGE_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND FRM_BLLNG_DT > @FRM_BLLNG_DT AND DPT_ID=@DPT_ID"
    Private Const Delete_StorageCharge_Qry As String = "DELETE FROM STORAGE_CHARGE WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND FRM_BLLNG_DT > @FRM_BLLNG_DT AND DPT_ID=@DPT_ID"
    Private Const Validate_Histroy_Delete_Select As String = "SELECT  MAX(TRCKNG_ID) AS TRCKNG_ID FROM V_TRACKING WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID AND CNCLD_DT IS NULL"
    Private Const Delete_Tracking_Qry As String = "DELETE FROM TRACKING WHERE TRCKNG_ID=@TRCKNG_ID AND DPT_ID=@DPT_ID"


    'Last Invoice Date
    Private Const Get_LastInvoiceDate_Select As String = " SELECT MAX(TO_BLLNG_DT)AS TO_BLLNG_DT FROM ( SELECT MAX(TO_BLLNG_DT) AS TO_BLLNG_DT FROM  HANDLING_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND BLLNG_FLG='B' UNION  SELECT MAX(ORGNL_CLNNG_DT) AS TO_BLLNG_DT FROM  CLEANING_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND BLLNG_FLG='B' UNION  SELECT MAX(TO_BLLNG_DT) AS TO_BLLNG_DT FROM  STORAGE_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND BLLNG_FLG='B' UNION  SELECT MAX(RPR_CMPLTN_DT) AS TO_BLLNG_DT FROM  REPAIR_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND BLLNG_FLG='B' UNION  SELECT MAX(HTNG_END_DT) AS TO_BLLNG_DT  FROM  HEATING_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND BLLNG_FLG='B' UNION  SELECT MAX(TO_BLLNG_DT) AS TO_BLLNG_DT  FROM  RENTAL_CHARGE WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND BLLNG_FLG='B') A"

    'Update Query
    Private Const UpdateEquipmentNo_UpdateQuery As String = "UPDATE EQUIPMENT_INFORMATION SET EQPMNT_NO=@NEW_EQPMNT_NO, EQPMNT_TYP_ID=@EQPMNT_TYP_ID ,RMRKS_VC=@RMRKS_VC WHERE EQPMNT_NO=@EQPMNT_NO AND DPT_ID=@DPT_ID"
    Private Const UpdateGateIn_UpdateQuery As String = "UPDATE GATEIN SET EQPMNT_NO=@EQPMNT_NO, CSTMR_ID=@CSTMR_ID, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EQPMNT_CD_ID=@EQPMNT_CD_ID WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const UpdateRental_Entry_UpdateQuery As String = "UPDATE RENTAL_ENTRY SET EQPMNT_NO=@EQPMNT_NO, CSTMR_ID=@CSTMR_ID WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const UpdateRental_Charge_UpdateQuery As String = "UPDATE RENTAL_CHARGE SET EQPMNT_NO=@EQPMNT_NO, CSTMR_ID=@CSTMR_ID WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const UpdatePre_Advice_UpdateQuery As String = "UPDATE PRE_ADVICE SET EQPMNT_NO=@EQPMNT_NO, CSTMR_ID=@CSTMR_ID, EQPMNT_TYP_ID=@EQPMNT_TYP_ID WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const UpdateTracking_UpdateQuery As String = "UPDATE TRACKING SET EQPMNT_NO=@EQPMNT_NO, CSTMR_ID=@CSTMR_ID WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const UpdateHandling_Charge_UpdateQuery As String = "UPDATE HANDLING_CHARGE SET EQPMNT_NO=@EQPMNT_NO, CSTMR_ID=@CSTMR_ID,EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EQPMNT_CD_ID=@EQPMNT_CD_ID, HNDLNG_CST_NC=@HNDLNG_CST_NC, TTL_CSTS_NC=@TTL_CSTS_NC  WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const UpdateStorage_Charge_UpdateQuery As String = "UPDATE STORAGE_CHARGE SET EQPMNT_NO=@EQPMNT_NO, CSTMR_ID=@CSTMR_ID, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EQPMNT_CD_ID=@EQPMNT_CD_ID WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    'Private Const UpdateActivity_Status_UpdateQuery As String = "UPDATE ACTIVITY_STATUS SET EQPMNT_NO=@EQPMNT_NO, CSTMR_ID=@CSTMR_ID, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EQPMNT_CD_ID=@EQPMNT_CD_ID, ACTVTY_DT=@ACTVTY_DT WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const UpdateActivity_Status_UpdateQuery As String = "UPDATE ACTIVITY_STATUS SET EQPMNT_NO=@EQPMNT_NO, CSTMR_ID=@CSTMR_ID, EQPMNT_TYP_ID=@EQPMNT_TYP_ID, EQPMNT_CD_ID=@EQPMNT_CD_ID, PRDCT_ID=@PRDCT_ID, GI_RF_NO=@GI_RF_NO, INSTRCTNS_VC=@INSTRCTNS_VC WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const UpdateHeating_Charge_UpdateQuery As String = "UPDATE HEATING_CHARGE SET EQPMNT_NO=@EQPMNT_NO, CSTMR_ID=@CSTMR_ID WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID"
    Private Const UpdatePrevious_Storage_UpdateQuery As String = "UPDATE STORAGE_CHARGE SET TO_BLLNG_DT=@TO_BLLNG_DT WHERE EQPMNT_NO=@EQPMNT_NO AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID AND CSTMR_ID=@CSTMR_ID"

    ''18989 -Scenario 2
    Private Const Delete_EquipmentInformation_Qry As String = "DELETE FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO=@EQPMNT_NO"
    Private Const GetOld_Storage_Charge_Select As String = "SELECT CSTMR_ID AS PREV_CSTMR_ID,FRM_BLLNG_DT,TO_BLLNG_DT AS PREV_TO_BLLNG_DT,STRG_CHRG_ID AS PREV_STRG_CHRG_ID,(SELECT TOP 1 STRG_CHRG_ID  FROM STORAGE_CHARGE  WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CSTMR_ID=@CSTMR_ID  AND TO_BLLNG_DT IS NULL)AS STRG_CHRG_ID   FROM STORAGE_CHARGE WHERE STRG_CHRG_ID IN(SELECT MAX(STRG_CHRG_ID) FROM STORAGE_CHARGE WHERE TO_BLLNG_DT IS NOT NULL AND  GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND DPT_ID=@DPT_ID AND CSTMR_ID=@CSTMR_ID)"
    Private Const Get_StorageChargeIDbyGITransaction_No_Select As String = "SELECT TOP 1 STRG_CHRG_ID  FROM STORAGE_CHARGE WHERE GI_TRNSCTN_NO=@GI_TRNSCTN_NO  AND TO_BLLNG_DT IS NULL"
    Private Const DeletePrevious_Storage_ChargeByStorageChargeID_Query As String = "DELETE FROM STORAGE_CHARGE WHERE STRG_CHRG_ID=@STRG_CHRG_ID"
    Private Const UpdatePrevious_Storage_ByStorageChargeID_Query As String = "UPDATE STORAGE_CHARGE SET TO_BLLNG_DT=NULL,EQPMNT_NO=@EQPMNT_NO WHERE STRG_CHRG_ID=@STRG_CHRG_ID"
    Private Const get_LastActivityName_Select As String = "SELECT MAX(eqpmnt_stts_id) AS ACTVTY_DT FROM TRACKING WHERE ACTVTY_NAM <> 'CHANGE OF STATUS' AND GI_TRNSCTN_NO=@GI_TRNSCTN_NO AND CNCLD_DT IS NULL"

    'Type & Code Merge
    Private Const UpdateGateInRet_UpdateQuery As String = "UPDATE GATEIN_RET SET GI_UNIT_NBR=@GI_UNIT_NBR, GI_LSR_OWNER=@GI_LSR_OWNER, GI_EQUIP_CODE=@GI_EQUIP_CODE, GI_EQUIP_TYPE=@GI_EQUIP_TYPE, GI_EQUIP_DESC=@GI_EQUIP_DESC WHERE GI_TRANSMISSION_NO=@GI_TRANSMISSION_NO"
    Private Const UpdateGateOutRet_UpdateQuery As String = "UPDATE GATEOUT_RET SET GO_UNIT_NBR=@GO_UNIT_NBR, GO_LSR_OWNER=@GO_LSR_OWNER,  GO_EQUIP_CODE=@GO_EQUIP_CODE, GO_EQUIP_TYPE=@GO_EQUIP_TYPE, GO_EQUIP_DESC=@GO_EQUIP_DESC WHERE GO_TRANSMISSION_NO=@GO_TRANSMISSION_NO"
    Private Const UpdateRepairEstimateRet_UpdateQuery As String = "UPDATE REPAIR_ESTIMATE_RET SET WM_UNIT_NBR=@WM_UNIT_NBR, WM_LSR_OWNER=@WM_LSR_OWNER,  WM_EQUIP_CODE=@WM_EQUIP_CODE, WM_EQUIP_TYPE=@WM_EQUIP_TYPE, WM_EQUIP_DESC=@WM_EQUIP_DESC WHERE WM_TRANSMISSION_NO=@WM_TRANSMISSION_NO"
    Private Const UpdateRepairEstimateDetailsRet_UpdateQuery As String = "UPDATE REPAIR_ESTIMATE_DETAIL_RET SET WD_UNIT_NBR=@WD_UNIT_NBR  WHERE WD_TRANSMISSION_NO=@WD_TRANSMISSION_NO"
    Private Const EquipmentTypeSelectQueryByDPT_ID As String = "SELECT EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_TYP_DSCRPTN_VC,EQPMNT_GRP_CD,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID,EQPMNT_CD_CD FROM EQUIPMENT_TYPE WHERE DPT_ID=@DPT_ID"

    Private ds As EquipmentUpdateDataSet
    Private Const GetEquipmentInformationIDByEquipmentNo_SelectQry As String = "SELECT EQPMNT_INFRMTN_ID FROM EQUIPMENT_INFORMATION WHERE EQPMNT_NO=@EQPMNT_NO"
    Private Const DeleteEquipmentInformationDetailsByEquipmentInformationId_Qry As String = "DELETE FROM EQUIPMENT_INFORMATION_DETAIL WHERE EQPMNT_INFRMTN_ID=@EQPMNT_INFRMTN_ID"
#End Region

#Region "Constructor"
    Sub New()
        ds = New EquipmentUpdateDataSet
    End Sub
#End Region

#Region "GetEquipmentInformationByEqpmntNo() Table Name: Equipment_Information"

    Public Function GetEquipmentInformationByEqpmntNo(ByVal bv_strEqpmntNo As String, _
                                                      ByVal bv_i32DepotId As Int32) As EquipmentUpdateDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEqpmntNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_i32DepotId)
            objData = New DataObjects(V_Equipment_UpdateSelectQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), EquipmentUpdateData._V_EQUIPMENT_UPDATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetActivityStatusByEqpmntNo() Table Name: Activity_status"

    Public Function GetActivityStatusByEqpmntNo(ByVal bv_strEqpmntNo As String, _
                                                ByVal bv_i32DepotId As Int32, _
                                                ByRef br_objTransaction As Transactions) As DataTable
        Try
            Dim hshparameters As New Hashtable
            Dim dtActivityStatus As New DataTable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEqpmntNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_i32DepotId)
            objData = New DataObjects(Activity_StatusSelectQuery, hshparameters)
            objData.Fill(dtActivityStatus, br_objTransaction)
            Return dtActivityStatus
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentInformationByEqpmntNo() Table Name: Equipment_information"

    Public Function GetEquipmentInformationByEqpmntNo(ByVal bv_strEqpmntNo As String, _
                                                      ByVal bv_i32DepotId As Int32, _
                                                      ByRef br_objTransaction As Transactions) As DataTable
        Try
            Dim hshparameters As New Hashtable
            Dim dtEquipmentInformation As New DataTable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEqpmntNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_i32DepotId)
            objData = New DataObjects(Equipment_InformationSelectQuery, hshparameters)
            objData.Fill(dtEquipmentInformation, br_objTransaction)
            Return dtEquipmentInformation
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEquipmentUpdate() Table Name: EQUIPMENT_UPDATE"
    Public Function GetEquipmentUpdate(ByRef br_objTransaction As Transactions) As EquipmentUpdateDataSet
        Try
            Dim dt As New DataTable
            objData = New DataObjects(EquipmentUpdateSelectQuery)
            objData.Fill(CType(ds, DataSet), EquipmentUpdateData._EQUIPMENT_UPDATE, br_objTransaction)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateEquipment() "
    Public Function UpdateEquipment(ByVal bv_strUpdateCondition As String, _
                                    ByVal bv_strTableName As String, _
                                    ByVal bv_strWhereCondition As String, _
                                    ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim strQuery As String = String.Concat("UPDATE ", bv_strTableName, " SET ", bv_strUpdateCondition, " WHERE ", bv_strWhereCondition)
            objData = New DataObjects(strQuery)
            objData.ExecuteScalar(br_objTransaction)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateGatein()"
    Public Function UpdateGatein(ByVal bv_strEquipmentNo As String, _
                                 ByVal bv_strGateinTransaction As String, _
                                 ByVal bv_i64ProductId As Int64, _
                                 ByVal bv_intDepotId As Int32, _
                                 ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._GATEIN).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(EquipmentUpdateData.GI_TRNSCTN_NO) = bv_strGateinTransaction
                .Item(EquipmentUpdateData.PRDCT_ID) = bv_i64ProductId
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdateGatein = objData.UpdateRow(dr, GateinUpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateGateOutEirNo Table Name: Gateout"
    Public Function UpdateGateoutEirNo(ByVal bv_strEquipmentNo As String, _
                                      ByVal bv_strGateinTransaction As String, _
                                      ByVal bv_strGateinReferenceNo As String, _
                                      ByVal bv_intDepotId As Int32, _
                                      ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._GATEIN).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(EquipmentUpdateData.GI_TRNSCTN_NO) = bv_strGateinTransaction
                .Item(EquipmentUpdateData.EIR_NO) = bv_strGateinReferenceNo
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdateGateoutEirNo = objData.UpdateRow(dr, GateoutUpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateGateinEirNo() Table Name: GateIn"
    Public Function UpdateGateinEirNo(ByVal bv_strEquipmentNo As String, _
                                      ByVal bv_strGateinTransaction As String, _
                                      ByVal bv_strGateinReferenceNo As String, _
                                      ByVal bv_intDepotId As Int32, _
                                      ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._GATEIN).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(EquipmentUpdateData.GI_TRNSCTN_NO) = bv_strGateinTransaction
                .Item(EquipmentUpdateData.EIR_NO) = bv_strGateinReferenceNo
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdateGateinEirNo = objData.UpdateRow(dr, GateinUpdateEirNoQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateActivityStatus  Table Name: Activity_Status"
    Public Function UpdateActivityStatus(ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_i64ProductId As Int64, _
                                         ByVal bv_strJTSEirNo As String, _
                                         ByVal bv_strRemarks As String, _
                                         ByVal bv_strDepotId As String, _
                                         ByVal bv_strTypeId As String, _
                                         ByVal bv_strCodeId As String, _
                                         ByVal bv_strCustomerId As String, _
                                         ByVal bv_strGateinTransaction As String, _
                                         ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim strWhere As String = String.Empty
            If Not IsNothing(bv_strDepotId) Then
                strWhere = String.Concat(strWhere, EquipmentUpdateData.DPT_ID, " IN (", bv_strDepotId, ") ")
            End If
            If bv_strGateinTransaction <> "" Then
                strWhere = String.Concat(strWhere, " AND ", EquipmentUpdateData.GI_TRNSCTN_NO, " IN ('", bv_strGateinTransaction, "')")
            End If
            If bv_strEquipmentNo <> "" Then
                strWhere = String.Concat(strWhere, " AND ", EquipmentUpdateData.EQPMNT_NO, " IN ('", bv_strEquipmentNo, "')")
            End If
            If bv_strCustomerId <> "" Then
                strWhere = String.Concat(strWhere, " AND ", EquipmentUpdateData.CSTMR_ID, " IN (", bv_strCustomerId, ") ")
            End If
            If bv_strTypeId <> "" Then
                strWhere = String.Concat(strWhere, " AND ", EquipmentUpdateData.EQPMNT_TYP_ID, " IN (", bv_strTypeId, ")")
            End If
            If bv_strCodeId <> "" Then
                strWhere = String.Concat(strWhere, " AND ", EquipmentUpdateData.EQPMNT_CD_ID, " IN (", bv_strCodeId, ")")
            End If

            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_i64ProductId = 0 Then
                    .Item(EquipmentUpdateData.PRDCT_ID) = DBNull.Value
                Else
                    .Item(EquipmentUpdateData.PRDCT_ID) = bv_i64ProductId
                End If
                .Item(EquipmentUpdateData.GI_RF_NO) = bv_strJTSEirNo
                .Item(EquipmentUpdateData.INSTRCTNS_VC) = bv_strRemarks
                UpdateActivityStatus = objData.UpdateRow(dr, String.Concat(ActivityStatusUpdateQuery, strWhere), br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateGateInRet(ByVal strGI_TransactionNo As String, _
                                    ByVal strEquipmentNo As String, _
                                    ByVal strCustomer As String, _
                                    ByVal strEquipmentType As String, _
                                    ByVal strEquipmentCode As String, _
                                    ByVal strEquipmentDescription As String, _
                                    ByRef br_objTransaction As Transactions)
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._GATEIN_RET).NewRow()
            With dr
                .Item(EquipmentUpdateData.GI_UNIT_NBR) = strEquipmentNo
                .Item(EquipmentUpdateData.GI_LSR_OWNER) = strCustomer
                .Item(EquipmentUpdateData.GI_EQUIP_CODE) = strEquipmentCode
                .Item(EquipmentUpdateData.GI_EQUIP_TYPE) = strEquipmentType
                .Item(EquipmentUpdateData.GI_EQUIP_DESC) = strEquipmentDescription
                .Item(EquipmentUpdateData.GI_TRANSMISSION_NO) = strGI_TransactionNo

                UpdateGateInRet = objData.UpdateRow(dr, UpdateGateInRet_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function UpdateGateOutRet(ByVal strGI_TransactionNo As String, _
                                    ByVal strEquipmentNo As String, _
                                    ByVal strCustomer As String, _
                                    ByVal strEquipmentType As String, _
                                    ByVal strEquipmentCode As String, _
                                    ByVal strEquipmentDescription As String, _
                                    ByRef br_objTransaction As Transactions)
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._GATEOUT_RET).NewRow()
            With dr
                .Item(EquipmentUpdateData.GO_UNIT_NBR) = strEquipmentNo
                .Item(EquipmentUpdateData.GO_LSR_OWNER) = strCustomer
                .Item(EquipmentUpdateData.GO_EQUIP_CODE) = strEquipmentCode
                .Item(EquipmentUpdateData.GO_EQUIP_TYPE) = strEquipmentType
                .Item(EquipmentUpdateData.GO_EQUIP_DESC) = strEquipmentDescription
                .Item(EquipmentUpdateData.GO_TRANSMISSION_NO) = strGI_TransactionNo

                UpdateGateOutRet = objData.UpdateRow(dr, UpdateGateOutRet_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function UpdateRepairEstimateRet(ByVal strGI_TransactionNo As String, _
                                            ByVal strEquipmentNo As String, _
                                            ByVal strCustomer As String, _
                                            ByVal strEquipmentType As String, _
                                            ByVal strEquipmentCode As String, _
                                            ByVal strEquipmentDescription As String, _
                                            ByRef br_objTransaction As Transactions)
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._REPAIR_ESTIMATE_RET).NewRow()
            With dr
                .Item(EquipmentUpdateData.WM_UNIT_NBR) = strEquipmentNo
                .Item(EquipmentUpdateData.WM_LSR_OWNER) = strCustomer
                .Item(EquipmentUpdateData.WM_EQUIP_CODE) = strEquipmentCode
                .Item(EquipmentUpdateData.WM_EQUIP_TYPE) = strEquipmentType
                .Item(EquipmentUpdateData.WM_EQUIP_DESC) = strEquipmentDescription
                .Item(EquipmentUpdateData.WM_TRANSMISSION_NO) = strGI_TransactionNo

                UpdateRepairEstimateRet = objData.UpdateRow(dr, UpdateRepairEstimateRet_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Function UpdateRepairEstimateDetailsRet(ByVal strGI_TransactionNo As String, _
                                            ByVal strEquipmentNo As String, _
                                            ByRef br_objTransaction As Transactions)
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._REPAIR_ESTIMATE_DETAIL_RET).NewRow()
            With dr
                .Item(EquipmentUpdateData.WD_UNIT_NBR) = strEquipmentNo
                .Item(EquipmentUpdateData.WD_TRANSMISSION_NO) = strGI_TransactionNo

                UpdateRepairEstimateDetailsRet = objData.UpdateRow(dr, UpdateRepairEstimateDetailsRet_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetEquipmentType(ByVal bv_intDepotID As Integer, ByRef br_objTransaction As Transactions) As DataTable
        Try
            Dim dt As New DataTable
            objData = New DataObjects(EquipmentTypeSelectQueryByDPT_ID, EquipmentTypeData.DPT_ID, bv_intDepotID)
            objData.Fill(dt, br_objTransaction)
            dt.TableName = EquipmentTypeData._EQUIPMENT_TYPE
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "UpdateEquipmentInformation  Table Name: Equipment_information"
    Public Function UpdateEquipmentInformation(ByVal bv_strEquipmentNo As String, _
                                               ByVal bv_i64EquipmentTypeId As Int64, _
                                               ByVal bv_strLastSurveyor As String, _
                                               ByVal bv_datLastTestDate As DateTime, _
                                               ByVal bv_datNextTestDate As DateTime, _
                                               ByVal bv_i64LastTestTypeId As Int64, _
                                               ByVal bv_i64NextTestTypeId As Int64, _
                                               ByVal bv_strValidityYears As String, _
                                               ByVal bv_strRemarks As String, _
                                               ByVal bv_i32DepotId As Int32, _
                                               ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(EquipmentUpdateData.EQPMNT_TYP_ID) = bv_i64EquipmentTypeId
                .Item(EquipmentUpdateData.LST_SRVYR_NM) = bv_strLastSurveyor
                If bv_datLastTestDate <> Nothing And bv_datLastTestDate <> sqlDbnull Then
                    .Item(EquipmentUpdateData.LST_TST_DT) = bv_datLastTestDate
                Else
                    .Item(EquipmentUpdateData.LST_TST_DT) = DBNull.Value
                End If
                If bv_datNextTestDate <> Nothing And bv_datNextTestDate <> sqlDbnull Then
                    .Item(EquipmentUpdateData.NXT_TST_DT) = bv_datNextTestDate
                Else
                    .Item(EquipmentUpdateData.NXT_TST_DT) = DBNull.Value
                End If
                If bv_i64LastTestTypeId <> 0 AndAlso bv_i64LastTestTypeId <> Nothing Then
                    .Item(EquipmentUpdateData.LST_TST_TYP_ID) = bv_i64LastTestTypeId
                Else
                    .Item(EquipmentUpdateData.LST_TST_TYP_ID) = DBNull.Value
                End If
                If bv_i64NextTestTypeId <> 0 AndAlso bv_i64NextTestTypeId <> Nothing Then
                    .Item(EquipmentUpdateData.NXT_TST_TYP_ID) = bv_i64NextTestTypeId
                Else
                    .Item(EquipmentUpdateData.NXT_TST_TYP_ID) = DBNull.Value
                End If
                If bv_strValidityYears <> Nothing Then
                    .Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS) = bv_strValidityYears
                Else
                    .Item(EquipmentUpdateData.VLDTY_PRD_TST_YRS) = DBNull.Value
                End If
                If bv_strRemarks <> Nothing Then
                    .Item(EquipmentUpdateData.RMRKS_VC) = bv_strRemarks
                Else
                    .Item(EquipmentUpdateData.RMRKS_VC) = DBNull.Value
                End If
                .Item(EquipmentUpdateData.DPT_ID) = bv_i32DepotId
                UpdateEquipmentInformation = objData.UpdateRow(dr, EquipmentInformationUpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateAuditLog() TABLE NAME:Audit_Log"

    Public Function CreateAuditLog(ByVal bv_strEquipmentNo As String, _
                                   ByVal bv_strActivity As String, _
                                   ByVal bv_strAction As String, _
                                   ByVal bv_datActionDate As DateTime, _
                                   ByVal bv_stroldValue As String, _
                                   ByVal bv_strNewValue As String, _
                                   ByVal bv_strReason As String, _
                                   ByVal bv_strModifiedBy As String, _
                                   ByVal bv_intDepotId As Int32, _
                                   ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._AUDIT_LOG).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EquipmentUpdateData._AUDIT_LOG, br_objTransaction)
                .Item(EquipmentUpdateData.ADT_LG_ID) = intMax
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_strEquipmentNo
                If bv_strActivity <> Nothing Then
                    .Item(EquipmentUpdateData.ACTVTY_NAM) = bv_strActivity
                Else
                    .Item(EquipmentUpdateData.ACTVTY_NAM) = DBNull.Value
                End If
                If bv_strAction <> Nothing Then
                    .Item(EquipmentUpdateData.ACTN_VC) = bv_strAction
                Else
                    .Item(EquipmentUpdateData.ACTN_VC) = DBNull.Value
                End If
                If bv_datActionDate <> Nothing Or bv_datActionDate <> sqlDbnull Then
                    .Item(EquipmentUpdateData.ACTN_DT) = bv_datActionDate
                Else
                    .Item(EquipmentUpdateData.ACTN_DT) = DBNull.Value
                End If
                If bv_stroldValue <> Nothing Then
                    .Item(EquipmentUpdateData.OLD_VL) = bv_stroldValue
                Else
                    .Item(EquipmentUpdateData.OLD_VL) = DBNull.Value
                End If
                If bv_strNewValue <> Nothing Then
                    .Item(EquipmentUpdateData.NEW_VL) = bv_strNewValue
                Else
                    .Item(EquipmentUpdateData.NEW_VL) = DBNull.Value
                End If
                If bv_strReason <> Nothing Then
                    .Item(EquipmentUpdateData.RSN_VC) = bv_strReason
                Else
                    .Item(EquipmentUpdateData.RSN_VC) = DBNull.Value
                End If
                'If bv_strHeaderName <> Nothing Then
                '    .Item(EquipmentUpdateData.HDR_NM) = bv_strHeaderName
                'Else
                '    .Item(EquipmentUpdateData.HDR_NM) = DBNull.Value
                'End If
                .Item(EquipmentUpdateData.MDFD_BY) = bv_strModifiedBy
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
            End With
            objData.InsertRow(dr, Audit_LogInsertQuery, br_objTransaction)
            dr = Nothing
            CreateAuditLog = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEquipmentInformationByID() TABLE NAME:EQUIPMENT_INFORMATION"

    Public Function GetEquipmentInformationByID(ByVal bv_strEquipmentNo As String, ByVal bv_i32DepotID As Int32) As DataTable
        Try
            Dim dtActivityStatus As New DataTable
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(EquipmentUpdateData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(ActivityStatusSelectQueryByDepotId, hshParameters)
            objData.Fill(dtActivityStatus)
            Return dtActivityStatus
            'Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetGateOutDateByEqpmntNo() TABLE NAME: GateOut"

    Public Function GetGateOutDateByEqpmntNo(ByVal bv_strEquipmentNo As String, _
                                             ByVal bv_strGateinTransactionNo As String, _
                                             ByVal bv_i32DepotID As Int32, _
                                             ByRef br_objTransaction As Transactions) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(EquipmentUpdateData.GI_TRNSCTN_NO, bv_strGateinTransactionNo)
            hshParameters.Add(EquipmentUpdateData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(GateoutSelectQuery, hshParameters)
            Return objData.ExecuteScalar(br_objTransaction)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetPreAdvice()  Table Name:PRE_ADVICE"
    Public Function GetPreAdviceByEqpmntNo(ByVal bv_strEquipmentNo As String, _
                                           ByVal bv_intDepotId As Int32) As String
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(PreAdviceSelectQuery, hshparameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetActivityStatusGateoutByEqpmntNo()  Table Name: ACTIVITY_STATUS"
    Public Function GetActivityStatusGateoutByEqpmntNo(ByVal bv_strEquipmentNo As String, _
                                                       ByVal bv_strGateinTransaction As String, _
                                                       ByVal bv_intDepotId As Int32, _
                                                       ByRef br_objTransaction As Transactions) As Decimal
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(EquipmentUpdateData.GI_TRNSCTN_NO, bv_strGateinTransaction)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(ActivityStatusGateOutSelectQuery, hshparameters)
            Return objData.ExecuteScalar(br_objTransaction)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCustomerHandlingInRatesByCustomerId()  Table Name: Customer_Charge_Detail"
    Public Function GetCustomerHandlingInRatesByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                           ByVal bv_i64TypeId As Int64, _
                                                           ByVal bv_i64CodeId As Int64, _
                                                           ByRef br_objTransaction As Transactions) As Decimal
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.CSTMR_ID, bv_i64CustomerId)
            hshparameters.Add(EquipmentUpdateData.EQPMNT_TYP_ID, bv_i64TypeId)
            hshparameters.Add(EquipmentUpdateData.EQPMNT_CD_ID, bv_i64CodeId)
            objData = New DataObjects(CustomerHandlingInSelectQueryById, hshparameters)
            Return objData.ExecuteScalar(br_objTransaction)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCustomerHandlingOutRatesByCustomerId()  Table Name: Customer_Charge_Detail"
    Public Function GetCustomerHandlingOutRatesByCustomerId(ByVal bv_i64CustomerId As Int64, _
                                                            ByVal bv_i64TypeId As Int64, _
                                                            ByVal bv_i64CodeId As Int64, _
                                                            ByRef br_objTransaction As Transactions) As Decimal
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.CSTMR_ID, bv_i64CustomerId)
            hshparameters.Add(EquipmentUpdateData.EQPMNT_TYP_ID, bv_i64TypeId)
            hshparameters.Add(EquipmentUpdateData.EQPMNT_CD_ID, bv_i64CodeId)
            objData = New DataObjects(CustomerHandlingOutSelectQueryById, hshparameters)
            Return objData.ExecuteScalar(br_objTransaction)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateHandlingCharge()   Table Name: Handling_Charge"
    Public Function UpdateHandlingCharge(ByVal bv_strEquipmentNo As String, _
                                         ByVal bv_strGateinTransaction As String, _
                                         ByVal bv_strCostType As String, _
                                         ByVal bv_decHandlingCost As Decimal, _
                                         ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._HANDLING_CHARGE).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_strEquipmentNo
                .Item(EquipmentUpdateData.GI_TRNSCTN_NO) = bv_strGateinTransaction
                .Item(EquipmentUpdateData.CST_TYP) = bv_strCostType
                .Item(EquipmentUpdateData.HNDLNG_CST_NC) = bv_decHandlingCost
                .Item(EquipmentUpdateData.TTL_CSTS_NC) = bv_decHandlingCost
                UpdateHandlingCharge = objData.UpdateRow(dr, HandlingChargeUpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetSupplierDetailsByEqpNo() TABLE NAME:SIPPLIER_DETAIL"

    Public Function GetSupplierDetailsByEqpNo(ByVal bv_strEquipmentNo As String, _
                                              ByVal bv_i32DepotID As Int32) As DataTable
        Try
            Dim dtSupplier As New DataTable
            Dim hshParameters As New Hashtable()
            hshParameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshParameters.Add(EquipmentUpdateData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(SupplierEquipmentDetailSelectQuery, hshParameters)
            objData.Fill(dtSupplier)
            Return dtSupplier
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Equipment Update - Release 3 Changes"

    Public Function ValidateRentalStatus(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32) As Long
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            ' objData = New DataObjects(Get_RentalStatus_select, hshparameters)
            objData = New DataObjects(Get_RentalStatus_select1, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidateEquipment_Exist(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, ByRef br_objTransaction As Transactions) As Long
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            ' objData = New DataObjects(Get_RentalStatus_select, hshparameters)
            objData = New DataObjects(ValidateEquipment_Exist_select1, hshparameters)
            Return objData.ExecuteScalar(br_objTransaction)

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function get_LastActivityStatus(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String) As String
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)

            objData = New DataObjects(Get_LastActivityStatus_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function get_LastActivity_Date(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String) As String
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)

            'objData = New DataObjects(get_LastActivity_Date_Select, hshparameters)
            objData = New DataObjects(get_LastActivity_Date_Select1, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function get_LastActivityStatus_Date(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String) As String
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)

            objData = New DataObjects(get_LastActivity_Date_Select, hshparameters)

            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function get_EffectiveFromDate_Info(ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String) As DataTable
        Try
            Dim hshparameters As New Hashtable
            Dim dtEquipment As New DataTable

            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)

            objData = New DataObjects(get_EffectiveFrom_Date_Select, hshparameters)
            objData.Fill(dtEquipment)

            Return dtEquipment
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function get_EffectiveFrom_Date(ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String) As String
        Try
            Dim hshparameters As New Hashtable

            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)

            objData = New DataObjects(get_EffectiveFrom_DateOnly_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidateActivityStatus(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32) As Long
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(Get_ValidateActivityStatus_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_GateIn_GITransaction_No(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32) As String
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(Get_GateIn_GITransaction_No_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_Storage_chargeCount(ByVal bv_strWhereCondition As String, ByRef br_objTransaction As Transactions) As Long
        Try
            Dim strQry As String = String.Concat(Get_Storage_chargeCount_Select, " ", bv_strWhereCondition)
            Dim strResult As String
            Dim lngValues As Long = 0

            objData = New DataObjects(strQry)
            strResult = objData.ExecuteScalar(br_objTransaction)

            If (Not String.IsNullOrEmpty(strResult) AndAlso Not String.IsNullOrWhiteSpace(strResult)) Then
                lngValues = CLng(strResult)
            End If

            Return lngValues

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Get_GateIn_Date(ByVal bv_strEquipmentNo As String, ByVal bv_strGITransaction_No As String, ByVal bv_intDepotId As Int32) As String
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(Get_GateIn_Date_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_LastInvoiceDate(ByVal bv_strEquipmentNo As String, ByVal bv_strGITransaction_No As String, _
                                               ByVal bv_intDepotId As Int32) As String
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(Get_LastInvoiceDate_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_Equipment_Info(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32) As DataTable
        Try
            Dim dtEquipment As New DataTable
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(Get_Equipment_Info_Select, hshparameters)
            objData.Fill(dtEquipment)
            Return dtEquipment

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_GateIn_Info(ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String, ByRef br_objTransaction As Transactions) As DataTable
        Try
            Dim dtEquipment As New DataTable
            Dim hshparameters As New Hashtable

            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)

            objData = New DataObjects(Get_GateIn_Info_Select, hshparameters)
            objData.Fill(dtEquipment, br_objTransaction)
            Return dtEquipment

        Catch ex As Exception
            Throw ex
        End Try
    End Function




#Region "Billing Equipment Validation"

    Public Function BillingValidation_CleaningCharge(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, _
                                                     ByVal bv_strGITransaction_No As String) As Long
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(BillingValidation_CleaningCharge_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function BillingValidation_HandlingCharge(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, _
                                                     ByVal bv_strGITransaction_No As String) As Long
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(BillingValidation_HandlingCharge_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function BillingValidation_StorageCharge(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, _
                                                    ByVal bv_strGITransaction_No As String) As Long
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(BillingValidation_StorageCharge_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function BillingValidation_RepairCharge(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, _
                                                   ByVal bv_strGITransaction_No As String) As Long
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(BillingValidation_RepairCharge_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function BillingValidation_HeatingCharge(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, _
                                                  ByVal bv_strGITransaction_No As String) As Long
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(BillingValidation_HeatingCharge_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function BillingValidation_RentalCharge(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, _
                                              ByVal bv_strGITransaction_No As String) As Long
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(BillingValidation_RentalCharge_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Validation_StorageCharge(ByVal bv_strEquipmentNo As String, ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String, _
                                             ByVal bv_GateIn_Date As DateTime, ByRef br_objTransaction As Transactions) As Long
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)
            hshparameters.Add(GateinData.FRM_BLLNG_DT, bv_GateIn_Date)

            objData = New DataObjects(Validation_StorageCharge_Select, hshparameters)
            Return objData.ExecuteScalar(br_objTransaction)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Delete_StorageCharge(ByVal bv_intDepotId As Int32, ByVal bv_strGITransaction_No As String, _
                                            ByVal bv_GateIn_Date As DateTime, ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._STORAGE_CHARGE).NewRow()
            With dr
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
                .Item(GateinData.DPT_ID) = bv_intDepotId
                .Item(GateinData.FRM_BLLNG_DT) = bv_GateIn_Date
                Delete_StorageCharge = objData.DeleteRow(dr, Delete_StorageCharge_Qry, br_objTransaction)
                dr = Nothing
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Validate_Histroy_Delete(ByVal bv_strGITransaction_No As String, ByVal bv_intDepotId As Int32) As String
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            hshparameters.Add(GateinData.DPT_ID, bv_intDepotId)

            objData = New DataObjects(Validate_Histroy_Delete_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Delete_Tracking(ByVal bv_TrackingId As Int32, ByVal bv_intDepotId As Int32, ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._TRACKING).NewRow()
            With dr
                .Item(GateinData.DPT_ID) = bv_intDepotId
                .Item(GateinData.TRCKNG_ID) = bv_TrackingId
                Delete_Tracking = objData.DeleteRow(dr, Delete_Tracking_Qry, br_objTransaction)
                dr = Nothing
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    Public Function UpdateEquipmentNo(ByVal bv_OldEquipmentNo As String, _
                                      ByVal bv_NewEquipmentNo As String, _
                                      ByVal bv_intEqpmnt_Typ_ID As Int32, _
                                      ByVal bv_intDepotId As Int32, _
                                      ByVal bv_Remarks As String, _
                                      ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_OldEquipmentNo
                .Item(EquipmentUpdateData.New_EQPMNT_NO) = bv_NewEquipmentNo
                .Item(GateinData.EQPMNT_TYP_ID) = bv_intEqpmnt_Typ_ID
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                .Item(EquipmentUpdateData.RMRKS_VC) = bv_Remarks
                UpdateEquipmentNo = objData.UpdateRow(dr, UpdateEquipmentNo_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateGateIn_Equipment(ByVal bv_EquipmentNo As String, _
                                 ByVal bv_strGITransaction_No As String, _
                                 ByVal bv_intDepotId As Int32, _
                                 ByVal bv_intCustomerId As Int32, _
                                 ByVal bv_intEqpmnt_Typ_ID As Int32, _
                                 ByVal bv_intEqpmnt_CD_ID As Int32, _
                                 ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EquipmentUpdateData._GATEIN).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_EquipmentNo
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
                .Item(GateinData.CSTMR_ID) = bv_intCustomerId
                .Item(GateinData.EQPMNT_TYP_ID) = bv_intEqpmnt_Typ_ID
                .Item(GateinData.EQPMNT_CD_ID) = bv_intEqpmnt_CD_ID
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdateGateIn_Equipment = objData.UpdateRow(dr, UpdateGateIn_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateRental_Entry(ByVal bv_EquipmentNo As String, _
                                ByVal bv_strGITransaction_No As String, _
                                ByVal bv_intDepotId As Int32, _
                                ByVal bv_intCustomerId As Int32, _
                                ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(RentalEntryData._RENTAL_ENTRY).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_EquipmentNo
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
                .Item(GateinData.CSTMR_ID) = bv_intCustomerId
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdateRental_Entry = objData.UpdateRow(dr, UpdateRental_Entry_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateRental_Charge(ByVal bv_EquipmentNo As String, _
                               ByVal bv_strGITransaction_No As String, _
                               ByVal bv_intDepotId As Int32, _
                               ByVal bv_intCustomerId As Int32, _
                               ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(RentalEntryData._RENTAL_CHARGE).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_EquipmentNo
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
                .Item(GateinData.CSTMR_ID) = bv_intCustomerId
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdateRental_Charge = objData.UpdateRow(dr, UpdateRental_Charge_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdatePre_Advice(ByVal bv_EquipmentNo As String, _
                              ByVal bv_strGITransaction_No As String, _
                              ByVal bv_intDepotId As Int32, _
                              ByVal bv_intCustomerId As Int32, _
                              ByVal bv_intEqpmnt_Typ_ID As Int32, _
                              ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(PreAdviceData._PRE_ADVICE).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_EquipmentNo
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
                .Item(GateinData.CSTMR_ID) = bv_intCustomerId
                .Item(GateinData.EQPMNT_TYP_ID) = bv_intEqpmnt_Typ_ID
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdatePre_Advice = objData.UpdateRow(dr, UpdatePre_Advice_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateTracking(ByVal bv_EquipmentNo As String, _
                             ByVal bv_strGITransaction_No As String, _
                             ByVal bv_intDepotId As Int32, _
                             ByVal bv_intCustomerId As Int32, _
                             ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TrackingData._TRACKING).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_EquipmentNo
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
                .Item(GateinData.CSTMR_ID) = bv_intCustomerId
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdateTracking = objData.UpdateRow(dr, UpdateTracking_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateHandling_Charge(ByVal bv_EquipmentNo As String, _
                               ByVal bv_strGITransaction_No As String, _
                               ByVal bv_intDepotId As Int32, _
                               ByVal bv_intCustomerId As Int32, _
                               ByVal bv_intEqpmnt_Typ_ID As Int32, _
                               ByVal bv_intEqpmnt_CD_ID As Int32, _
                               ByVal bv_decHndlng_cst_nc As Decimal, _
                               ByVal bv_decttl_csts_nc As Decimal, _
                               ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._HANDLING_CHARGE).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_EquipmentNo
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
                .Item(GateinData.CSTMR_ID) = bv_intCustomerId
                .Item(GateinData.EQPMNT_TYP_ID) = bv_intEqpmnt_Typ_ID
                .Item(GateinData.EQPMNT_CD_ID) = bv_intEqpmnt_CD_ID

                .Item(GateinData.HNDLNG_CST_NC) = bv_decHndlng_cst_nc
                .Item(GateinData.TTL_CSTS_NC) = bv_decttl_csts_nc

                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdateHandling_Charge = objData.UpdateRow(dr, UpdateHandling_Charge_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateStorage_Charge(ByVal bv_EquipmentNo As String, _
                               ByVal bv_strGITransaction_No As String, _
                               ByVal bv_intDepotId As Int32, _
                               ByVal bv_intCustomerId As Int32, _
                               ByVal bv_intEqpmnt_Typ_ID As Int32, _
                               ByVal bv_intEqpmnt_CD_ID As Int32, _
                               ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._STORAGE_CHARGE).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_EquipmentNo
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
                .Item(GateinData.CSTMR_ID) = bv_intCustomerId
                .Item(GateinData.EQPMNT_TYP_ID) = bv_intEqpmnt_Typ_ID
                .Item(GateinData.EQPMNT_CD_ID) = bv_intEqpmnt_CD_ID
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdateStorage_Charge = objData.UpdateRow(dr, UpdateStorage_Charge_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdatePrevious_Storage_Charge(ByVal bv_OldEquipmentNo As String, _
                               ByVal bv_strGITransaction_No As String, _
                               ByVal bv_intDepotId As Int32, _
                               ByVal bv_OldCustomerId As Int32, _
                               ByVal bv_To_BillingDt As DateTime, _
                               ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._STORAGE_CHARGE).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_OldEquipmentNo
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
                .Item(GateinData.CSTMR_ID) = bv_OldCustomerId
                .Item(GateinData.TO_BLLNG_DT) = bv_To_BillingDt
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdatePrevious_Storage_Charge = objData.UpdateRow(dr, UpdatePrevious_Storage_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function UpdateHeating_Charge(ByVal bv_EquipmentNo As String, _
                              ByVal bv_strGITransaction_No As String, _
                              ByVal bv_intDepotId As Int32, _
                              ByVal bv_intCustomerId As Int32, _
                              ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._STORAGE_CHARGE).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_EquipmentNo
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
                .Item(GateinData.CSTMR_ID) = bv_intCustomerId
                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdateHeating_Charge = objData.UpdateRow(dr, UpdateHeating_Charge_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateActivity_Status(ByVal bv_EquipmentNo As String, _
                              ByVal bv_strGITransaction_No As String, _
                              ByVal bv_intDepotId As Int32, _
                              ByVal bv_intCustomerId As Int32, _
                              ByVal bv_intEqpmnt_Typ_ID As Int32, _
                              ByVal bv_intEqpmnt_CD_ID As Int32, _
                              ByVal bv_intProductId As Int64, _
                              ByVal bv_strJTSEIRNo As String, _
                              ByVal bv_strRemarks As String, _
                              ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._ACTIVITY_STATUS).NewRow()
            With dr
                .Item(EquipmentUpdateData.EQPMNT_NO) = bv_EquipmentNo
                .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
                .Item(GateinData.CSTMR_ID) = bv_intCustomerId
                .Item(GateinData.EQPMNT_TYP_ID) = bv_intEqpmnt_Typ_ID
                .Item(GateinData.EQPMNT_CD_ID) = bv_intEqpmnt_CD_ID

                If bv_intProductId <> Nothing Then
                    .Item(EquipmentUpdateData.PRDCT_ID) = bv_intProductId
                Else
                    .Item(EquipmentUpdateData.PRDCT_ID) = DBNull.Value
                End If

                If bv_strJTSEIRNo <> Nothing Then
                    .Item(EquipmentUpdateData.GI_RF_NO) = bv_strJTSEIRNo
                Else
                    .Item(EquipmentUpdateData.GI_RF_NO) = DBNull.Value
                End If

                If bv_strRemarks <> Nothing Then
                    .Item(EquipmentUpdateData.INSTRCTNS_VC) = bv_strRemarks
                Else
                    .Item(EquipmentUpdateData.INSTRCTNS_VC) = DBNull.Value
                End If


                .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
                UpdateActivity_Status = objData.UpdateRow(dr, UpdateActivity_Status_UpdateQuery, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function UpdateActivity_Status(ByVal bv_EquipmentNo As String, _
    '                         ByVal bv_strGITransaction_No As String, _
    '                         ByVal bv_intDepotId As Int32, _
    '                         ByVal bv_intCustomerId As Int32, _
    '                         ByVal bv_intEqpmnt_Typ_ID As Int32, _
    '                         ByVal bv_intEqpmnt_CD_ID As Int32, _
    '                         ByVal bv_ActivityDate As DateTime, _
    '                         ByRef br_objTransaction As Transactions) As Boolean
    '    Try
    '        Dim dr As DataRow
    '        objData = New DataObjects()
    '        dr = ds.Tables(GateinData._ACTIVITY_STATUS).NewRow()
    '        With dr
    '            .Item(EquipmentUpdateData.EQPMNT_NO) = bv_EquipmentNo
    '            .Item(GateinData.GI_TRNSCTN_NO) = bv_strGITransaction_No
    '            .Item(GateinData.CSTMR_ID) = bv_intCustomerId
    '            .Item(GateinData.EQPMNT_TYP_ID) = bv_intEqpmnt_Typ_ID
    '            .Item(GateinData.EQPMNT_CD_ID) = bv_intEqpmnt_CD_ID
    '            .Item(GateinData.ACTVTY_DT) = bv_ActivityDate
    '            .Item(EquipmentUpdateData.DPT_ID) = bv_intDepotId
    '            UpdateActivity_Status = objData.UpdateRow(dr, UpdateActivity_Status_UpdateQuery, br_objTransaction)
    '            dr = Nothing
    '        End With
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

#End Region

#Region "18989 Fix"
#Region "GetPreviousStorageCharge() Table Name: storage_charge"
    Public Function GetPreviousStorageChargeByGITransactionNo(ByVal bv_strGITransaction_No As String, _
                                                              ByVal bv_intDepotId As Int32, _
                                                              ByVal bv_strNewCustomerId As String) As DataTable
        Try
            Dim hshparameters As New Hashtable
            Dim dtPreviousStorageCharge As New DataTable
            hshparameters.Add(EquipmentUpdateData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            hshparameters.Add(EquipmentUpdateData.DPT_ID, bv_intDepotId)
            hshparameters.Add(EquipmentUpdateData.CSTMR_ID, bv_strNewCustomerId)
            objData = New DataObjects(GetOld_Storage_Charge_Select, hshparameters)
            objData.Fill(dtPreviousStorageCharge)
            Return dtPreviousStorageCharge
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Get_StorageChargeIDbyGITransaction_No() Table Name: storage_charge"
    Public Function Get_StorageChargeIDbyGITransaction_No(ByVal bv_strGITransaction_No As String, ByVal bv_intDepotId As Int32, ByRef br_objTransaction As Transactions) As Int32
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            objData = New DataObjects(Get_StorageChargeIDbyGITransaction_No_Select, hshparameters)
            Return objData.ExecuteScalar(br_objTransaction)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeletePrevious_Storage_ChargeByStorageChargeID"
    Public Function DeletePrevious_Storage_ChargeByStorageChargeID(ByVal bv_intStorageChargeId As Int32, _
                                                                   ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._STORAGE_CHARGE).NewRow()
            With dr
                .Item("STRG_CHRG_ID") = bv_intStorageChargeId
                DeletePrevious_Storage_ChargeByStorageChargeID = objData.DeleteRow(dr, DeletePrevious_Storage_ChargeByStorageChargeID_Query, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdatePrevious_Storage_ChargeByStorageChargeID"
    Public Function UpdatePrevious_Storage_ChargeByStorageChargeID(ByVal bv_intStorageChargeId As Int32, _
                                                                   ByVal bv_strEquipmentNo As String, _
                                                                   ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._STORAGE_CHARGE).NewRow()
            With dr
                .Item("STRG_CHRG_ID") = bv_intStorageChargeId
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                UpdatePrevious_Storage_ChargeByStorageChargeID = objData.UpdateRow(dr, UpdatePrevious_Storage_ByStorageChargeID_Query, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteEquipmentInformationByEquipmentNo"
    Public Function DeleteEquipmentInformationByEquipmentNo(ByVal bv_strEquipmentNo As String, _
                                                                   ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._STORAGE_CHARGE).NewRow()
            With dr
                .Item(GateinData.EQPMNT_NO) = bv_strEquipmentNo
                DeleteEquipmentInformationByEquipmentNo = objData.DeleteRow(dr, Delete_EquipmentInformation_Qry, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetEquipmentInformationIDByEquipmentNo(ByVal bv_strEquipmentNo As String, _
                                                                  ByRef br_objTransaction As Transactions) As DataTable
        Try
            Dim dt As New DataTable

            Dim hshParam As New Hashtable
            hshParam.Add(EquipmentUpdateData.EQPMNT_NO, bv_strEquipmentNo)
            objData = New DataObjects(GetEquipmentInformationIDByEquipmentNo_SelectQry, hshParam)
            'objData.DeleteRow(dr, GetEquipmentInformationIDByEquipmentNo_SelectQry, br_objTransaction)
            objData.Fill(dt, br_objTransaction)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteEquipmentInformationDetailsByEquipmentInformationId(ByVal bv_strEquipmentInformationId As String, _
                                                                   ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GateinData._EQUIPMENT_INFORMATION).NewRow()
            With dr
                .Item(GateinData.EQPMNT_INFRMTN_ID) = bv_strEquipmentInformationId
                DeleteEquipmentInformationDetailsByEquipmentInformationId = objData.DeleteRow(dr, DeleteEquipmentInformationDetailsByEquipmentInformationId_Qry, br_objTransaction)
                dr = Nothing
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetLastEquipmentStatus"
    Public Function GetLastEquipmentStatus(ByVal bv_strGITransaction_No As String) As Int32
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(GateinData.GI_TRNSCTN_NO, bv_strGITransaction_No)
            objData = New DataObjects(get_LastActivityName_Select, hshparameters)
            Return objData.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#End Region

End Class
#End Region
