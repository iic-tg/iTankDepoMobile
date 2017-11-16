Public Class EquipmentUpdateData
#Region "Declaration Part.."
    'Data Tables..
    Public Const _EQUIPMENT_INFORMATION As String = "EQUIPMENT_INFORMATION"
    Public Const _HANDLING_CHARGE As String = "HANDLING_CHARGE"
    Public Const _V_EQUIPMENT_INFORMATION As String = "V_EQUIPMENT_INFORMATION"
    Public Const _ACTIVITY_STATUS As String = "ACTIVITY_STATUS"
    Public Const _V_ACTIVITY_STATUS As String = "V_ACTIVITY_STATUS"
    Public Const _EQUIPMENT_UPDATE As String = "EQUIPMENT_UPDATE"
    Public Const _NEW_EQUIPMENT_UPDATE As String = "NEW_EQUIPMENT_UPDATE"
    Public Const _V_EQUIPMENT_UPDATE As String = "V_EQUIPMENT_UPDATE"
    Public Const _AUDIT_LOG As String = "AUDIT_LOG"
    Public Const _GATEIN As String = "GATEIN"
    Public Const _STORAGE_CHARGE As String = "STORAGE_CHARGE"
    Public Const _TRACKING As String = "TRACKING"

    'Type & Code Merge
    Public Const _GATEIN_RET As String = "GATEIN_RET"
    Public Const _GATEOUT_RET As String = "GATEOUT_RET"
    Public Const _REPAIR_ESTIMATE_RET As String = "REPAIR_ESTIMATE_RET"
    Public Const _REPAIR_ESTIMATE_DETAIL_RET As String = "REPAIR_ESTIMATE_DETAIL_RET"

    'Data Columns..

    'Table Name: EQUIPMENT_INFORMATION
    Public Const EQPMNT_INFRMTN_ID As String = "EQPMNT_INFRMTN_ID"
    Public Const EQPMNT_NO As String = "EQPMNT_NO"
    Public Const EQPMNT_TYP_ID As String = "EQPMNT_TYP_ID"
    Public Const MNFCTR_DT As String = "MNFCTR_DT"
    Public Const TR_WGHT_NC As String = "TR_WGHT_NC"
    Public Const GRSS_WGHT_NC As String = "GRSS_WGHT_NC"
    Public Const CPCTY_NC As String = "CPCTY_NC"
    Public Const LST_TST_DT As String = "LST_TST_DT"
    Public Const NXT_TST_DT As String = "NXT_TST_DT"
    Public Const VLDTY_PRD_TST_YRS As String = "VLDTY_PRD_TST_YRS"
    Public Const LST_TST_TYP_ID As String = "LST_TST_TYP_ID"
    Public Const NXT_TST_TYP_ID As String = "NXT_TST_TYP_ID"
    Public Const LST_TST_TYP_CD As String = "LST_TST_TYP_CD"
    Public Const NXT_TST_TYP_CD As String = "NXT_TST_TYP_CD"
    Public Const LST_SRVYR_NM As String = "LST_SRVYR_NM"
    Public Const INSTRCTNS_VC As String = "INSTRCTNS_VC"
    Public Const CRTD_BY As String = "CRTD_BY"
    Public Const CRTD_DT As String = "CRTD_DT"
    Public Const MDFD_BY As String = "MDFD_BY"
    Public Const MDFD_DT As String = "MDFD_DT"
    Public Const DPT_ID As String = "DPT_ID"
    Public Const DPT_CD As String = "DPT_CD"
    Public Const ACTV_BT As String = "ACTV_BT"
    Public Const RNTL_BT As String = "RNTL_BT"
    Public Const RMRKS_VC As String = "RMRKS_VC"
    Public Const New_EQPMNT_NO As String = "New_EQPMNT_NO"

    'Table Name: V_EQUIPMENT_UPDATE   
    Public Const PRDCT_ID As String = "PRDCT_ID"
    Public Const PRDCT_CD As String = "PRDCT_CD"
    Public Const GI_RF_NO As String = "GI_RF_NO"
    Public Const ACTVTY_DT As String = "ACTVTY_DT"
    Public Const EQPMNT_STTS_ID As String = "EQPMNT_STTS_ID"
    Public Const EQPMNT_STTS_CD As String = "EQPMNT_STTS_CD"
    Public Const CSTMR_ID As String = "CSTMR_ID"
    Public Const CSTMR_CD As String = "CSTMR_CD"
    Public Const EQPMNT_TYP_CD As String = "EQPMNT_TYP_CD"
    Public Const EQPMNT_CD_CD As String = "EQPMNT_CD_CD"
    Public Const EQPMNT_CD_ID As String = "EQPMNT_CD_ID"
    Public Const INVC_GNRT_BT As String = "INVC_GNRT_BT"
    Public Const RFRNC_EIR_NO_1 As String = "RFRNC_EIR_NO_1"
 
    'Table Name: EQUIPMENT_UPDATE
    Public Const TABLE_NAME As String = "TABLE_NAME"
    Public Const UPDATE_FIELDS As String = "UPDATE_FIELDS"
    Public Const OLD_EQPMNT_NO As String = "OLD_EQPMNT_NO"
    Public Const OLD_CSTMR_ID As String = "OLD_CSTMR_ID"
    Public Const OLD_EQPMNT_TYP_ID As String = "OLD_EQPMNT_TYP_ID"
    Public Const OLD_EQPMNT_CD_ID As String = "OLD_EQPMNT_CD_ID"
    Public Const OLD_CSTMR_CD As String = "OLD_CSTMR_CD"
    Public Const OLD_EQPMNT_TYP_CD As String = "OLD_EQPMNT_TYP_CD"
    Public Const OLD_EQPMNT_CD_CD As String = "OLD_EQPMNT_CD_CD"
    Public Const OLD_RFRNC_EIR_NO_1 As String = "OLD_RFRNC_EIR_NO_1"
    Public Const CST_TYP As String = "CST_TYP"


    'Table Name: AUDIT_LOG
    Public Const ADT_LG_ID As String = "ADT_LG_ID"
    Public Const ACTVTY_NAM As String = "ACTVTY_NAM"
    Public Const ACTN_VC As String = "ACTN_VC"
    Public Const ACTN_DT As String = "ACTN_DT"
    Public Const OLD_VL As String = "OLD_VL"
    Public Const NEW_VL As String = "NEW_VL"
    Public Const RSN_VC As String = "RSN_VC"
    Public Const PRDCT_DSCRPTN_VC As String = "PRDCT_DSCRPTN_VC"
    Public Const GI_TRNSCTN_NO As String = "GI_TRNSCTN_NO"
    Public Const OLD_GI_TRNSCTN_NO As String = "OLD_GI_TRNSCTN_NO"
    Public Const EIR_NO As String = "EIR_NO"
    Public Const HDR_NM As String = "HDR_NM"
    Public Const HNDLNG_CST_NC As String = "HNDLNG_CST_NC"
    Public Const TTL_CSTS_NC As String = "TTL_CSTS_NC"

    'Gate In Ret
    Public Const GI_UNIT_NBR As String = "GI_UNIT_NBR"
    Public Const GI_LSR_OWNER As String = "GI_LSR_OWNER"
    Public Const GI_REFERENCE As String = "GI_REFERENCE"
    Public Const GI_EQUIP_CODE As String = "GI_EQUIP_CODE"
    Public Const GI_EQUIP_TYPE As String = "GI_EQUIP_TYPE"
    Public Const GI_EQUIP_DESC As String = "GI_EQUIP_DESC"
    Public Const GI_TRANSMISSION_NO As String = "GI_TRANSMISSION_NO"

    'Gate Out ret
    Public Const GO_UNIT_NBR As String = "GO_UNIT_NBR"
    Public Const GO_LSR_OWNER As String = "GO_LSR_OWNER"
    Public Const GO_REFERENCE As String = "GO_REFERENCE"
    Public Const GO_EQUIP_CODE As String = "GO_EQUIP_CODE"
    Public Const GO_EQUIP_TYPE As String = "GO_EQUIP_TYPE"
    Public Const GO_EQUIP_DESC As String = "GO_EQUIP_DESC"
    Public Const GO_TRANSMISSION_NO As String = "GO_TRANSMISSION_NO"

    'Repair Estimate Ret

    Public Const WM_UNIT_NBR As String = "WM_UNIT_NBR"
    Public Const WM_LSR_OWNER As String = "WM_LSR_OWNER"
    Public Const WM_REFERENCE As String = "WM_REFERENCE"
    Public Const WM_EQUIP_CODE As String = "WM_EQUIP_CODE"
    Public Const WM_EQUIP_TYPE As String = "WM_EQUIP_TYPE"
    Public Const WM_EQUIP_DESC As String = "WM_EQUIP_DESC"
    Public Const WM_TRANSMISSION_NO As String = "WM_TRANSMISSION_NO"


    'Repair Estimate Details Ret
    Public Const WD_UNIT_NBR As String = "WD_UNIT_NBR"
    Public Const WD_REFERENCE As String = "WD_REFERENCE"
    Public Const WD_TRANSMISSION_NO As String = "WD_TRANSMISSION_NO"
   

#End Region

End Class
