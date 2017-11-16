#Region " EDIs.vb"
'*********************************************************************************************************************
'Name :
'      EDIs.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EDIs.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      7/9/2013 4:29:19 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

#Region "EDIs"

Public Class EDIs

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const GATEIN_RETSelectQueryByGI_DPT_TRMGICOndition As String = "SELECT GI_SNO,GI_TRANSMISSION_NO,GI_COMPLETE,GI_SENT_EIR,GI_SENT_DATE,GI_REC_EIR,GI_REC_DATE,GI_REC_ADDR,GI_REC_TYPE,GI_EXPORTED,GI_EXPOR_DATE,GI_IMPORTED,GI_IMPOR_DATE,GI_TRNSXN,GI_ADVICE,GI_UNIT_NBR,GI_EQUIP_TYPE,GI_EQUIP_DESC,GI_EQUIP_CODE,GI_CONDITION,GI_COMP_ID_A,GI_COMP_ID_N,GI_COMP_ID_C,GI_COMP_TYPE,GI_COMP_DESC,GI_COMP_CODE,GI_EIR_DATE,GI_EIR_TIME,GI_REFERENCE,GI_MANU_DATE,GI_MATERIAL,GI_WEIGHT,GI_MEASURE,GI_UNITS,GI_CSC_REEXAM,GI_COUNTRY,GI_LIC_STATE,GI_LIC_REG,GI_LIC_EXPIRE,GI_LSR_OWNER,GI_SEND_EDI_1,GI_SSL_LSE,GI_SEND_EDI_2,GI_HAULIER,GI_SEND_EDI_3,GI_DPT_TRM,GI_SEND_EDI_4,GI_OTHERS_1,GI_OTHERS_2,GI_OTHERS_3,GI_OTHERS_4,GI_NOTE_1,GI_NOTE_2,GI_LOAD,GI_FHWA,GI_LAST_OH_LOC,GI_LAST_OH_DATE,GI_SENDER,GI_ATTENTION,GI_REVISION,GI_SEND_EDI_5,GI_SEND_EDI_6,GI_SEND_EDI_7,GI_SEND_EDI_8,GI_SEAL_PARTY_1,GI_SEAL_NUMBER_1,GI_SEAL_PARTY_2,GI_SEAL_NUMBER_2,GI_SEAL_PARTY_3,GI_SEAL_NUMBER_3,GI_SEAL_PARTY_4,GI_SEAL_NUMBER_4,GI_PORT_FUNC_CODE,GI_PORT_NAME,GI_VESSEL_NAME,GI_VOYAGE_NUM,GI_HAZ_MAT_CODE,GI_HAZ_MAT_DESC,GI_NOTE_3,GI_NOTE_4,GI_NOTE_5,GI_COMP_ID_A_2,GI_COMP_ID_N_2,GI_COMP_ID_C_2,GI_COMP_TYPE_2,GI_COMP_CODE_2,GI_COMP_DESC_2,GI_SHIPPER,GI_DRAY_ORDER,GI_RAIL_ID,GI_RAIL_RAMP,GI_VESSEL_CODE,GI_WGHT_CERT_1,GI_WGHT_CERT_2,GI_WGHT_CERT_3,GI_SEA_RAIL,GI_LOC_IDENT,GI_PORT_LOC_QUAL,GI_STATUS,GI_PICK_DATE,GI_ESTSTATUS,GI_ERRSTATUS,GI_USERNAME,GI_LIVE_STATUS,GI_ISACTIVE,GI_YARD_LOC,GI_MODE_PAYMENT,GI_BILLING_TYPE,GI_RESERVE_BKG,GI_RCESTSTATUS,OP_SNO,OP_STATUS FROM GATEIN_RET WHERE GI_DPT_TRM=@GI_DPT_TRM AND GI_STATUS=@GI_STATUS AND GI_LSR_OWNER=@GI_LSR_OWNER AND GI_CONDITION=@GI_CONDITION"
    Private Const GATEIN_RETSelectQueryByGI_DPT_TRM As String = "SELECT GI_SNO,GI_TRANSMISSION_NO,GI_COMPLETE,GI_SENT_EIR,GI_SENT_DATE,GI_REC_EIR,GI_REC_DATE,GI_REC_ADDR,GI_REC_TYPE,GI_EXPORTED,GI_EXPOR_DATE,GI_IMPORTED,GI_IMPOR_DATE,GI_TRNSXN,GI_ADVICE,GI_UNIT_NBR,GI_EQUIP_TYPE,GI_EQUIP_DESC,GI_EQUIP_CODE,GI_CONDITION,GI_COMP_ID_A,GI_COMP_ID_N,GI_COMP_ID_C,GI_COMP_TYPE,GI_COMP_DESC,GI_COMP_CODE,GI_EIR_DATE,GI_EIR_TIME,GI_REFERENCE,GI_MANU_DATE,GI_MATERIAL,GI_WEIGHT,GI_MEASURE,GI_UNITS,GI_CSC_REEXAM,GI_COUNTRY,GI_LIC_STATE,GI_LIC_REG,GI_LIC_EXPIRE,GI_LSR_OWNER,GI_SEND_EDI_1,GI_SSL_LSE,GI_SEND_EDI_2,GI_HAULIER,GI_SEND_EDI_3,GI_DPT_TRM,GI_SEND_EDI_4,GI_OTHERS_1,GI_OTHERS_2,GI_OTHERS_3,GI_OTHERS_4,GI_NOTE_1,GI_NOTE_2,GI_LOAD,GI_FHWA,GI_LAST_OH_LOC,GI_LAST_OH_DATE,GI_SENDER,GI_ATTENTION,GI_REVISION,GI_SEND_EDI_5,GI_SEND_EDI_6,GI_SEND_EDI_7,GI_SEND_EDI_8,GI_SEAL_PARTY_1,GI_SEAL_NUMBER_1,GI_SEAL_PARTY_2,GI_SEAL_NUMBER_2,GI_SEAL_PARTY_3,GI_SEAL_NUMBER_3,GI_SEAL_PARTY_4,GI_SEAL_NUMBER_4,GI_PORT_FUNC_CODE,GI_PORT_NAME,GI_VESSEL_NAME,GI_VOYAGE_NUM,GI_HAZ_MAT_CODE,GI_HAZ_MAT_DESC,GI_NOTE_3,GI_NOTE_4,GI_NOTE_5,GI_COMP_ID_A_2,GI_COMP_ID_N_2,GI_COMP_ID_C_2,GI_COMP_TYPE_2,GI_COMP_CODE_2,GI_COMP_DESC_2,GI_SHIPPER,GI_DRAY_ORDER,GI_RAIL_ID,GI_RAIL_RAMP,GI_VESSEL_CODE,GI_WGHT_CERT_1,GI_WGHT_CERT_2,GI_WGHT_CERT_3,GI_SEA_RAIL,GI_LOC_IDENT,GI_PORT_LOC_QUAL,GI_STATUS,GI_PICK_DATE,GI_ESTSTATUS,GI_ERRSTATUS,GI_USERNAME,GI_LIVE_STATUS,GI_ISACTIVE,GI_YARD_LOC,GI_MODE_PAYMENT,GI_BILLING_TYPE,GI_RESERVE_BKG,GI_RCESTSTATUS,OP_SNO,OP_STATUS FROM GATEIN_RET WHERE GI_DPT_TRM=@GI_DPT_TRM AND GI_STATUS=@GI_STATUS AND GI_LSR_OWNER=@GI_LSR_OWNER"
    Private Const Gatein_RetDeleteQuery As String = "DELETE FROM GateIn_Ret WHERE GI_SNO=@GI_SNO"
    'Private Const TrackingUpdateQuery As String = "UPDATE Tracking SET PCKP_STTS_VC=@PCKP_STTS_VC, PCKP_DT=@PCKP_DT WHERE TRNSMSSN_NO=@TRNSMSSN_NO"
    Private Const TrackingUpdateQuery As String = "UPDATE Tracking SET PCKP_STTS_VC=@PCKP_STTS_VC, PCKP_DT=@PCKP_DT WHERE ACTVTY_NO=@ACTVTY_NO"
    Private Const Repair_Estimate_RetDeleteQuery As String = "DELETE FROM Repair_Estimate_Ret WHERE WM_SNO=@WM_SNO"
    Private Const GATEOUT_RETSelectQueryByGO_DPT_TRM As String = "SELECT GO_SNO,GO_TRANSMISSION_NO,GO_COMPLETE,GO_SENT_EIR,GO_SENT_DATE,GO_REC_EIR,GO_REC_DATE,GO_REC_ADDR,GO_REC_TYPE,GO_EXPORTED,GO_EXPOR_DATE,GO_IMPORTED,GO_IMPOR_DATE,GO_TRNSXN,GO_ADVICE,GO_UNIT_NBR,GO_EQUIP_TYPE,GO_EQUIP_DESC,GO_EQUIP_CODE,GO_CONDITION,GO_COMP_ID_A,GO_COMP_ID_N,GO_COMP_ID_C,GO_COMP_TYPE,GO_COMP_DESC,GO_COMP_CODE,GO_EIR_DATE,GO_EIR_TIME,GO_REFERENCE,GO_MANU_DATE,GO_MATERIAL,GO_WEIGHT,GO_MEASURE,GO_UNITS,GO_CSC_REEXAM,GO_COUNTRY,GO_LIC_STATE,GO_LIC_REG,GO_LIC_EXPIRE,GO_LSR_OWNER,GO_SEND_EDI_1,GO_SSL_LSE,GO_SEND_EDI_2,GO_HAULIER,GO_SEND_EDI_3,GO_DPT_TRM,GO_SEND_EDI_4,GO_OTHERS_1,GO_OTHERS_2,GO_OTHERS_3,GO_OTHERS_4,GO_NOTE_1,GO_NOTE_2,GO_LOAD,GO_FHWA,GO_LAST_OH_LOC,GO_LAST_OH_DATE,GO_SENDER,GO_ATTENTION,GO_REVISION,GO_SEND_EDI_5,GO_SEND_EDI_6,GO_SEND_EDI_7,GO_SEND_EDI_8,GO_SEAL_PARTY_1,GO_SEAL_NUMBER_1,GO_SEAL_PARTY_2,GO_SEAL_NUMBER_2,GO_SEAL_PARTY_3,GO_SEAL_NUMBER_3,GO_SEAL_PARTY_4,GO_SEAL_NUMBER_4,GO_PORT_FUNC_CODE,GO_PORT_NAME,GO_VESSEL_NAME,GO_VOYAGE_NUM,GO_HAZ_MAT_CODE,GO_HAZ_MAT_DESC,GO_NOTE_3,GO_NOTE_4,GO_NOTE_5,GO_COMP_ID_A_2,GO_COMP_ID_N_2,GO_COMP_ID_C_2,GO_COMP_TYPE_2,GO_COMP_CODE_2,GO_COMP_DESC_2,GO_SHIPPER,GO_DRAY_ORDER,GO_RAIL_ID,GO_RAIL_RAMP,GO_VESSEL_CODE,GO_WGHT_CERT_1,GO_WGHT_CERT_2,GO_WGHT_CERT_3,GO_SEA_RAIL,GO_BILL_LADING,GO_LOC_IDENT,GO_PORT_LOC_QUAL,GO_STATUS,GO_PICK_DATE,GO_ERRSTATUS,GO_USERNAME,GO_LIVE_STATUS,GO_YARD_LOC,GO_MODE_PAYMENT,GO_BILLING_TYPE FROM GATEOUT_RET WHERE GO_DPT_TRM=@GO_DPT_TRM AND GO_STATUS=@GO_STATUS AND GO_LSR_OWNER=@GO_LSR_OWNER"
    Private Const Gateout_RetDeleteQuery As String = "DELETE FROM Gateout_Ret WHERE GO_SNO=@GO_SNO"
    Private Const REPAIR_ESTIMATE_RETSelectQueryByDPT_TRM As String = "SELECT WM_SNO,WM_TRANSMISSION_NO,WM_COMPLETE,WM_SENT_EIR,WM_SENT_DATE,WM_REC_EIR,WM_REC_DATE,WM_REC_ADDR,WM_REC_TYPE,WM_EXPORTED,WM_EXPOR_DATE,WM_IMPORTED,WM_IMPOR_DATE,WM_TRNSXN,WM_PTY_RSPONS,WM_REVISION,WM_ESTIM_DATE,WM_UNIT_NBR,WM_REFERENCE,WM_EQUIP_TYPE,WM_EQUIP_CODE,WM_EQUIP_DESC,WM_TERM_LOCA,WM_TERM_DATE,WM_TERM_TIME,WM_LAST_OH_LOC,WM_LAST_OH_DATE,WM_CONDITION,WM_MANU_DATE,WM_CSC_REEXAM,WM_LOAD,WM_SENDER,WM_ATTENTION,WM_LSR_OWNER,WM_SEND_EDI_1,WM_SSL_LSE,WM_SEND_EDI_2,WM_HAULIER,WM_SEND_EDI_3,WM_DPT_TRM,WM_SEND_EDI_4,WM_INSURER,WM_SURVEYOR,WM_OTHERS_1,WM_TAX_RATE,WM_FILLER,WM_NOTE_1,WM_NOTE_2,WM_NOTE_3,WM_BAS_CURR,WM_LABOR_RATE,WM_DPP_CURR,WM_DPP_AMT,WM_WEIGHT,WM_MEASURE,WM_UNITS,WM_MATERIAL,WM_U_LABOR,WM_U_MATERIAL,WM_U_HANDLING,WM_U_TAX,WM_U_TOTAL,WM_I_LABOR,WM_I_MATERIAL,WM_I_HANDLING,WM_I_TAX,WM_I_TOTAL,WM_O_LABOR,WM_O_MATERIAL,WM_O_HANDLING,WM_O_TAX,WM_O_TOTAL,WM_D_LABOR,WM_D_MATERIAL,WM_D_HANDLING,WM_D_TAX,WM_D_TOTAL,WM_S_LABOR,WM_S_MATERIAL,WM_S_HANDLING,WM_S_TAX,WM_S_TOTAL,WM_X_LABOR,WM_X_MATERIAL,WM_X_HANDLING,WM_X_TAX,WM_X_TOTAL,WM_EST_TOTAL,WM_ADVICE,WM_EIR_NUM,WM_AUTH_NUM,WM_AUTH_AMT,WM_AUTH_PTY,WM_AUTH_DATE,WM_O_ESTIM_DATE,WM_OTHERS_2,WM_SEND_EDI_5,WM_SEND_EDI_6,WM_SEND_EDI_7,WM_SEND_EDI_8,WM_NOTE_4,WM_NOTE_5,WM_WEIGHT_2,WM_MEASURE_2,WM_INVOICE_TYPE,WM_ODOMETER_HOURS,WM_OUT_SVC_DATE,WM_RET_SVC_DATE,WM_OWN_INSP_DATE,WM_MECHANIC_NAME,WM_BILLEE_CODE,WM_SUB_REPAIR_TYPE,WM_OUT_SVC_TIME,WM_RET_SVC_TIME,WM_EXCHG_RATE,WM_STATUS,WM_PICK_DATE,WM_ESTSTATUS,WM_Errors,WM_MatchStatus,WM_ERRSTATUS,WM_USERNAME,WM_LIVE_STATUS,WM_EST_TOTAL_TAXED,WM_CC_TOTAL,WM_SRVC_TAX_RATE,WM_TOTAL_LABOR_COST,WM_TOTAL_SRVC_TAX,WM_EQUIP_SIZE,WM_YARD_LOC,WM_RCESTSTATUS,WM_ESTIMATE_TYPE FROM REPAIR_ESTIMATE_RET WHERE WM_DPT_TRM=@WM_DPT_TRM AND WM_STATUS=@WM_STATUS AND WM_LSR_OWNER=@WM_LSR_OWNER"

    Private Const Repair_Estimate_Detail_RetSelectQueryPk As String = "SELECT WD_SNO,WD_TRANSMISSION_NO,WD_EXPORTED,WD_EXPOR_DATE,WD_IMPORTED,WD_IMPOR_DATE,WD_TRNSXN,WD_REVISION,WD_ESTIM_DATE,WD_UNIT_NBR,WD_REFERENCE,WD_LABOR_RATE,WD_LINE,WD_REPAIR,WD_REPEATS,WD_DAMAGE,WD_COMPONENT,WD_COMP_MATL,WD_LOCATION,WD_LENGTH,WD_WIDTH,WD_HEIGHT,WD_UNITS,WD_HOURS,WD_SCALE,WD_MAT_COST,WD_PTY_RSPONS,WD_TAX_RULE,WD_AAR_JOB,WD_JOBCODE,WD_DMGREP_DESC,WD_OFF_TIRE_SIZE,WD_OFF_BRAND,WD_OFF_SERIAL_NUM,WD_OFF_LOT_NUM,WD_OFF_TREAD_DEPTH,WD_ON_TIRE_SIZE,WD_ON_BRAND,WD_ON_SERIAL_NUM,WD_ON_LOT_NUM,WD_ON_TREAD_DEPTH,WD_SUPPLYTIRE,WD_SUPPLYTIREAMT,WD_ON_RETREAD_SER,WD_OFF_RETREAD_SER,WD_STATUS,WD_PICK_DATE,WM_SNo,WD_LIVE_STATUS,WD_TARIFF_CODE FROM REPAIR_ESTIMATE_DETAIL_RET WHERE WM_SNO IN "
    Private Const Repair_Estimate_Detail_RetDeleteQuery As String = "DELETE FROM Repair_Estimate_Detail_Ret WHERE WD_SNO=@WD_SNO"

    Private Const Edi_File_IdentifierSelectQueryPk As String = "SELECT FL_IDNTFR_ID,FL_IDNTTY,LNE_SG_INFO,FL_DSCRPTN_VC,DPT_ID FROM EDI_FILE_IDENTIFIER WHERE DPT_ID=@DPT_ID"
    Private Const Edi_MovementSelectQueryPk As String = "SELECT MVMNT_ID,MVMNT_NAM,MVMNT_DSCRPTN_VC,FL_IDNTFR_ID,MVMNT_PRNT_ID,DPT_ID FROM EDI_MOVEMENT WHERE DPT_ID=@DPT_ID"
    Private Const Edi_Segment_DetailSelectQueryPk As String = "SELECT SGMNT_DTL_ID,SGMNT_HDR_ID,SGMNT_DTL,SGMNT_DTL_SZ,SGMNT_DTL_ORDR,SGMNT_DTL_DLMTR,SGMNT_DTL_PSTN,TRNS_CLMN,SGMNT_DTL_DSCRPTN_VC,SGMNT_DTL_CMMNTS,MNDTRY,MVMNT_ID,RFRNC,DPT_ID FROM EDI_SEGMENT_DETAIL WHERE DPT_ID=@DPT_ID"
    Private Const Edi_Segment_HeaderSelectQueryPk As String = "SELECT SGMNT_HDR_ID,SGMNT_HDR,SGMNT_ORDR,SGMNT_SZ,EDI_VRSN_ID,LN_DLMTR,SGMNT_DLMTR,SGMNT_PSTN,SGMNT_DSCRPTN_VC,SGMNT_PRNT,SGMNT_TYP,MVMNT_ID,MNDTRY,DPT_ID FROM EDI_SEGMENT_HEADER WHERE DPT_ID=@DPT_ID"
    Private Const Edi_VersionSelectQueryPk As String = "SELECT EDI_VRSN_ID,EDI_VRSN,EDI_VRSN_PSTN,EDI_VRSN_DSCRPTN_VC,FL_IDNTFR_ID,DPT_ID FROM EDI_VERSION WHERE DPT_ID=@DPT_ID"

    Private Const GATEIN_RETNoRCSelectQueryByGI_DPT_TRM As String = "SELECT GI_SNO,GI_TRANSMISSION_NO,GI_COMPLETE,GI_SENT_EIR,GI_SENT_DATE,GI_REC_EIR,GI_REC_DATE,GI_REC_ADDR,GI_REC_TYPE,GI_EXPORTED,GI_EXPOR_DATE,GI_IMPORTED,GI_IMPOR_DATE,GI_TRNSXN,GI_ADVICE,GI_UNIT_NBR,GI_EQUIP_TYPE,GI_EQUIP_DESC,GI_EQUIP_CODE,GI_CONDITION,GI_COMP_ID_A,GI_COMP_ID_N,GI_COMP_ID_C,GI_COMP_TYPE,GI_COMP_DESC,GI_COMP_CODE,GI_EIR_DATE,GI_EIR_TIME,GI_REFERENCE,GI_MANU_DATE,GI_MATERIAL,GI_WEIGHT,GI_MEASURE,GI_UNITS,GI_CSC_REEXAM,GI_COUNTRY,GI_LIC_STATE,GI_LIC_REG,GI_LIC_EXPIRE,GI_LSR_OWNER,GI_SEND_EDI_1,GI_SSL_LSE,GI_SEND_EDI_2,GI_HAULIER,GI_SEND_EDI_3,GI_DPT_TRM,GI_SEND_EDI_4,GI_OTHERS_1,GI_OTHERS_2,GI_OTHERS_3,GI_OTHERS_4,GI_NOTE_1,GI_NOTE_2,GI_LOAD,GI_FHWA,GI_LAST_OH_LOC,GI_LAST_OH_DATE,GI_SENDER,GI_ATTENTION,GI_REVISION,GI_SEND_EDI_5,GI_SEND_EDI_6,GI_SEND_EDI_7,GI_SEND_EDI_8,GI_SEAL_PARTY_1,GI_SEAL_NUMBER_1,GI_SEAL_PARTY_2,GI_SEAL_NUMBER_2,GI_SEAL_PARTY_3,GI_SEAL_NUMBER_3,GI_SEAL_PARTY_4,GI_SEAL_NUMBER_4,GI_PORT_FUNC_CODE,GI_PORT_NAME,GI_VESSEL_NAME,GI_VOYAGE_NUM,GI_HAZ_MAT_CODE,GI_HAZ_MAT_DESC,GI_NOTE_3,GI_NOTE_4,GI_NOTE_5,GI_COMP_ID_A_2,GI_COMP_ID_N_2,GI_COMP_ID_C_2,GI_COMP_TYPE_2,GI_COMP_CODE_2,GI_COMP_DESC_2,GI_SHIPPER,GI_DRAY_ORDER,GI_RAIL_ID,GI_RAIL_RAMP,GI_VESSEL_CODE,GI_WGHT_CERT_1,GI_WGHT_CERT_2,GI_WGHT_CERT_3,GI_SEA_RAIL,GI_LOC_IDENT,GI_PORT_LOC_QUAL,GI_STATUS,GI_PICK_DATE,GI_ESTSTATUS,GI_ERRSTATUS,GI_USERNAME,GI_LIVE_STATUS,GI_ISACTIVE,GI_YARD_LOC,GI_MODE_PAYMENT,GI_BILLING_TYPE,GI_RESERVE_BKG,GI_RCESTSTATUS,OP_SNO,OP_STATUS FROM GATEIN_RET WHERE GI_DPT_TRM=@GI_DPT_TRM AND GI_STATUS=@GI_STATUS AND GI_LSR_OWNER=@GI_LSR_OWNER AND GI_CONDITION<>'C'"
    Private Const GATEIN_RETRCSelectQueryByGI_DPT_TRM As String = "SELECT GI_SNO,GI_TRANSMISSION_NO,GI_COMPLETE,GI_SENT_EIR,GI_SENT_DATE,GI_REC_EIR,GI_REC_DATE,GI_REC_ADDR,GI_REC_TYPE,GI_EXPORTED,GI_EXPOR_DATE,GI_IMPORTED,GI_IMPOR_DATE,GI_TRNSXN,GI_ADVICE,GI_UNIT_NBR,GI_EQUIP_TYPE,GI_EQUIP_DESC,GI_EQUIP_CODE,GI_CONDITION,GI_COMP_ID_A,GI_COMP_ID_N,GI_COMP_ID_C,GI_COMP_TYPE,GI_COMP_DESC,GI_COMP_CODE,GI_EIR_DATE,GI_EIR_TIME,GI_REFERENCE,GI_MANU_DATE,GI_MATERIAL,GI_WEIGHT,GI_MEASURE,GI_UNITS,GI_CSC_REEXAM,GI_COUNTRY,GI_LIC_STATE,GI_LIC_REG,GI_LIC_EXPIRE,GI_LSR_OWNER,GI_SEND_EDI_1,GI_SSL_LSE,GI_SEND_EDI_2,GI_HAULIER,GI_SEND_EDI_3,GI_DPT_TRM,GI_SEND_EDI_4,GI_OTHERS_1,GI_OTHERS_2,GI_OTHERS_3,GI_OTHERS_4,GI_NOTE_1,GI_NOTE_2,GI_LOAD,GI_FHWA,GI_LAST_OH_LOC,GI_LAST_OH_DATE,GI_SENDER,GI_ATTENTION,GI_REVISION,GI_SEND_EDI_5,GI_SEND_EDI_6,GI_SEND_EDI_7,GI_SEND_EDI_8,GI_SEAL_PARTY_1,GI_SEAL_NUMBER_1,GI_SEAL_PARTY_2,GI_SEAL_NUMBER_2,GI_SEAL_PARTY_3,GI_SEAL_NUMBER_3,GI_SEAL_PARTY_4,GI_SEAL_NUMBER_4,GI_PORT_FUNC_CODE,GI_PORT_NAME,GI_VESSEL_NAME,GI_VOYAGE_NUM,GI_HAZ_MAT_CODE,GI_HAZ_MAT_DESC,GI_NOTE_3,GI_NOTE_4,GI_NOTE_5,GI_COMP_ID_A_2,GI_COMP_ID_N_2,GI_COMP_ID_C_2,GI_COMP_TYPE_2,GI_COMP_CODE_2,GI_COMP_DESC_2,GI_SHIPPER,GI_DRAY_ORDER,GI_RAIL_ID,GI_RAIL_RAMP,GI_VESSEL_CODE,GI_WGHT_CERT_1,GI_WGHT_CERT_2,GI_WGHT_CERT_3,GI_SEA_RAIL,GI_LOC_IDENT,GI_PORT_LOC_QUAL,GI_STATUS,GI_PICK_DATE,GI_ESTSTATUS,GI_ERRSTATUS,GI_USERNAME,GI_LIVE_STATUS,GI_ISACTIVE,GI_YARD_LOC,GI_MODE_PAYMENT,GI_BILLING_TYPE,GI_RESERVE_BKG,GI_RCESTSTATUS,OP_SNO,OP_STATUS FROM GATEIN_RET WHERE GI_DPT_TRM=@GI_DPT_TRM AND GI_STATUS=@GI_STATUS AND GI_LSR_OWNER=@GI_LSR_OWNER AND GI_CONDITION='C'"

    Private Const GET_EDI_DETAILSbyActivityandCustomer As String = "SELECT EDI_ID,CSTMR_CD,CSTMR_ID,EDI_ACTVTY_NAM,GNRTD_DT_TM,ANC_FL_NM,CDC_FL_NM,CRTD_BY,CRTD_DT,FLE_FRMT,LST_SNT_BY,LST_SNT_DT,TMS_SNT FROM EDI  WHERE DPT_ID =@DPT_ID ORDER BY GNRTD_DT_TM DESC"
    Private Const EDIInsertQuery As String = "INSERT INTO EDI (EDI_ID,EDI_ACTVTY_NAM,GNRTD_DT_TM,RSND_BIT,ANC_FL_NM,CDC_FL_NM,CRTD_BY,CRTD_DT,CSTMR_CD,FLE_FRMT,CSTMR_ID,TMS_SNT,DPT_ID,DPT_CD) VALUES (@EDI_ID,@EDI_ACTVTY_NAM,@GNRTD_DT_TM,@RSND_BIT,@ANC_FL_NM,@CDC_FL_NM,@CRTD_BY,@CRTD_DT,@CSTMR_CD,@FLE_FRMT,@CSTMR_ID,@TMS_SNT,@DPT_ID,@DPT_CD)"
    Private Const DepotDetailsDetailsQuery As String = "SELECT BNK_TYP_ID,DPT_ID,CRRNCY_ID,CRRNCY_CD FROM V_BANK_DETAIL WHERE BNK_TYP_ID=44 AND DPT_ID=@DPT_ID"
    Private Const Edi_Email_DetailInsertQuery As String = "INSERT INTO EDI_EMAIL_DETAIL(ED_EML_ID,EDI_ID,CSTMR_ID,FRM_EML,TO_EML,BCC_EML,SBJCT_VC,BDY_VC,DPT_ID,SNT_BT,SNT_DT,CRTD_DT,CRTD_BY)VALUES(@ED_EML_ID,@EDI_ID,@CSTMR_ID,@FRM_EML,@TO_EML,@BCC_EML,@SBJCT_VC,@BDY_VC,@DPT_ID,@SNT_BT,@SNT_DT,@CRTD_DT,@CRTD_BY)"
    Private Const update_EDI_Emaildetailquery As String = "UPDATE EDI SET LST_SNT_BY=@LST_SNT_BY,LST_SNT_DT=@LST_SNT_DT,TMS_SNT=TMS_SNT+1 WHERE EDI_ID=@EDI_ID"
    Private Const GET_EDI_EMAIL_DETAILquery As String = "SELECT ED_EML_ID ,EDI_ID,CSTMR_ID ,FRM_EML ,TO_EML ,BCC_EML ,SBJCT_VC ,BDY_VC ,DPT_ID ,SNT_BT ,SNT_DT ,CRTD_BY ,CRTD_DT,(SELECT CSTMR_CD FROM CUSTOMER WHERE CSTMR_ID=E.CSTMR_ID)CSTMR_CD FROM EDI_EMAIL_DETAIL E WHERE EDI_ID=@EDI_ID"
    Private Const GET_EDI_Settings_DETAILSelectquery As String = "SELECT EDI_STTNGS_ID ,CSTMR_ID ,CSTMR_CD ,TO_EML ,CC_EML ,NXT_RN_DT_TM ,ACTV_BT ,CRTD_BY ,CRTD_DT,FLE_FRMT_ID,FLE_FRMT_CD,GNRTN_TM,SBJCT_VCR FROM V_EDI_SETTINGS WHERE DPT_ID=@DPT_ID"
    Private Const Edi_SettingsInsertQuery As String = "INSERT INTO EDI_SETTINGS(EDI_STTNGS_ID,CSTMR_ID,TO_EML,CC_EML,NXT_RN_DT_TM,ACTV_BT,CRTD_DT,CRTD_BY,FLE_FRMT_ID,SBJCT_VCR,GNRTN_TM,DPT_ID)VALUES(@EDI_STTNGS_ID,@CSTMR_ID,@TO_EML,@CC_EML,@NXT_RN_DT_TM,@ACTV_BT,@CRTD_DT,@CRTD_BY,@FLE_FRMT_ID,@SBJCT_VCR,@GNRTN_TM,@DPT_ID)"
    Private Const Edi_SettingsUpdateQuery As String = "UPDATE EDI_SETTINGS SET EDI_STTNGS_ID=@EDI_STTNGS_ID, CSTMR_ID=@CSTMR_ID, TO_EML=@TO_EML, CC_EML=@CC_EML,ACTV_BT=@ACTV_BT, FLE_FRMT_ID=@FLE_FRMT_ID,SBJCT_VCR=@SBJCT_VCR,GNRTN_TM=@GNRTN_TM,NXT_RN_DT_TM=@NXT_RN_DT_TM WHERE EDI_STTNGS_ID=@EDI_STTNGS_ID"
    Private Const EdiSettings_DeleteQuery As String = "DELETE FROM EDI_SETTINGS WHERE EDI_STTNGS_ID=@EDI_STTNGS_ID"
    Private Const GetEdiSettings As String = "SELECT EDI_STTNGS_ID,CSTMR_ID,CSTMR_CD,TO_EML,CC_EML,NXT_RN_DT_TM,ACTV_BT,CRTD_DT,CRTD_BY,FLE_FRMT_ID,SBJCT_VCR,GNRTN_TM FROM V_EDI_SETTINGS where CSTMR_ID=@CSTMR_ID AND DPT_ID=@DPT_ID"

    Private ds As EDIDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New EDIDataSet
    End Sub

#End Region


#Region "GetGateinRetByGI_DPT_TRM() TABLE NAME:GATEIN_RET"

    Public Function GetGateinRetByGI_DPT_TRM(ByVal bv_strGI_DPT_TRM As String, ByVal bv_strGi_Status As String, ByVal bv_strCustomerCode As String, ByVal bv_strGI_Condition As String) As DataTable
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EDIData.GI_DPT_TRM, bv_strGI_DPT_TRM)
            hshTable.Add(EDIData.GI_STATUS, bv_strGi_Status)
            hshTable.Add(EDIData.GI_LSR_OWNER, bv_strCustomerCode)
            hshTable.Add(EDIData.GI_CONDITION, bv_strGI_Condition)
            Dim dtGateinRet As New DataTable
            dtGateinRet.TableName = EDIData._GATEIN_RET
            objData = New DataObjects(GATEIN_RETSelectQueryByGI_DPT_TRMGICOndition, hshTable)
            objData.Fill(dtGateinRet)
            Return dtGateinRet
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetGateinRetByGI_DPT_TRM() TABLE NAME:GATEIN_RET"

    Public Function GetGateinRetByGI_DPT_TRM(ByVal bv_strGI_DPT_TRM As String, ByVal bv_strGi_Status As String, ByVal bv_strCustomerCode As String) As DataTable
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EDIData.GI_DPT_TRM, bv_strGI_DPT_TRM)
            hshTable.Add(EDIData.GI_STATUS, bv_strGi_Status)
            hshTable.Add(EDIData.GI_LSR_OWNER, bv_strCustomerCode)
            '  hshTable.Add(EDIData.GI_CONDITION, bv_strGI_Condition)
            Dim dtGateinRet As New DataTable
            dtGateinRet.TableName = EDIData._GATEIN_RET
            objData = New DataObjects(GATEIN_RETSelectQueryByGI_DPT_TRM, hshTable)
            objData.Fill(dtGateinRet)
            Return dtGateinRet
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTracking() TABLE NAME:TRACKING"

    Public Function UpdateTracking(ByVal bv_strPCKP_STTS_VC As String, _
  ByVal bv_datPCKP_DT As Date, ByVal bv_strTRNSMSSN_NO As String, ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TrackingData._TRACKING).NewRow()
            With dr
                .Item(TrackingData.PCKP_STTS_VC) = bv_strPCKP_STTS_VC
                .Item(TrackingData.PCKP_DT) = bv_datPCKP_DT
                '.Item(TrackingData.TRNSMSSN_NO) = bv_strTRNSMSSN_NO
                .Item(TrackingData.ACTVTY_NO) = bv_strTRNSMSSN_NO
            End With
            UpdateTracking = objData.UpdateRow(dr, TrackingUpdateQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "GetGateoutRetByGO_DPT_TRM() TABLE NAME:GATEOUT_RET"

    Public Function GetGateoutRetByGO_DPT_TRM(ByVal bv_strGO_DPT_TRM As String, ByVal bv_strGO_Status As String, ByVal bv_strCustomerCode As String) As DataTable
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EDIData.GO_DPT_TRM, bv_strGO_DPT_TRM)
            hshTable.Add(EDIData.GO_STATUS, bv_strGO_Status)
            hshTable.Add(EDIData.GO_LSR_OWNER, bv_strCustomerCode)
            Dim dtGateOutRet As New DataTable
            dtGateOutRet.TableName = EDIData._GATEOUT_RET
            objData = New DataObjects(GATEOUT_RETSelectQueryByGO_DPT_TRM, hshTable)
            objData.Fill(dtGateOutRet)
            Return dtGateOutRet
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


#Region "DeleteGateinRet() TABLE NAME:Gatein_Ret"

    Public Function DeleteGateinRet(ByVal bv_strGI_SNO As Int64, ByRef objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(EDIData._GATEIN_RET).NewRow()
            With dr
                .Item(EDIData.GI_SNO) = bv_strGI_SNO
            End With
            DeleteGateinRet = objData.DeleteRow(dr, Gatein_RetDeleteQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "DeleteGateoutRet() TABLE NAME:Gateout_Ret"

    Public Function DeleteGateoutRet(ByVal bv_strGO_SNO As Int64, ByRef objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(EDIData._GATEOUT_RET).NewRow()
            With dr
                .Item(EDIData.GO_SNO) = bv_strGO_SNO
            End With
            DeleteGateoutRet = objData.DeleteRow(dr, Gateout_RetDeleteQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "GetRepairEstimateRetByDPT_TRM() TABLE NAME:REPAIR_ESTIMATE_RET"

    Public Function GetRepairEstimateRetByDPT_TRM(ByVal bv_strDPT_TRM As String, ByVal bv_strWM_Status As String, ByVal bv_strCustomerCode As String) As DataTable
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EDIData.WM_DPT_TRM, bv_strDPT_TRM)
            hshTable.Add(EDIData.WM_STATUS, bv_strWM_Status)
            hshTable.Add(EDIData.WM_LSR_OWNER, bv_strCustomerCode)
            Dim dtRepairEstimateRet As New DataTable
            dtRepairEstimateRet.TableName = EDIData._REPAIR_ESTIMATE_RET
            objData = New DataObjects(REPAIR_ESTIMATE_RETSelectQueryByDPT_TRM, hshTable)
            objData.Fill(dtRepairEstimateRet)
            Return dtRepairEstimateRet
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


#Region "DeleteRepairEstimateRet() TABLE NAME:Repair_Estimate_Ret"

    Public Function DeleteRepairEstimateRet(ByVal bv_strSNO As Int64, ByRef objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(EDIData._REPAIR_ESTIMATE_RET).NewRow()
            With dr
                .Item(EDIData.WM_SNO) = bv_strSNO
            End With
            DeleteRepairEstimateRet = objData.DeleteRow(dr, Repair_Estimate_RetDeleteQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "GetRepairEstimateDetailRetBySNO() TABLE NAME:Repair_Estimate_Detail_Ret"

    Public Function GetRepairEstimateDetailRetBySNO(ByVal bv_strWm_SNO As String, wd_status As String) As DataTable
        Try
            Dim dtRepairEstimateDetRet As New DataTable
            dtRepairEstimateDetRet.TableName = EDIData._REPAIR_ESTIMATE_DETAIL_RET
            '  Dim hshTable As New Hashtable
            ' hshTable.Add(EDIData.WD_SNO, bv_strWm_SNO)
            'hshTable.Add(EDIData.WD_STATUS, wd_status)
            objData = New DataObjects(Repair_Estimate_Detail_RetSelectQueryPk & "(" & bv_strWm_SNO & ") AND WD_STATUS='" & wd_status & "'")
            objData.Fill(dtRepairEstimateDetRet)
            Return dtRepairEstimateDetRet
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteRepairEstimateDetailRet() TABLE NAME:Repair_Estimate_Detail_Ret"

    Public Function DeleteRepairEstimateDetailRet(ByVal bv_strSNO As Int64, ByRef objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(EDIData._REPAIR_ESTIMATE_DETAIL_RET).NewRow()
            With dr
                .Item(EDIData.WD_SNO) = bv_strSNO
                '  .Item(EDIData.WM_SNO) = bv_strSNO
            End With
            DeleteRepairEstimateDetailRet = objData.DeleteRow(dr, Repair_Estimate_Detail_RetDeleteQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "GetEdiFileIdentifierByDpt_Id () TABLE NAME:Edi_File_Identifier"

    Public Function GetEdiFileIdentifierByDpt_Id(ByVal bv_i32Dpt_Id As Int32) As EDIDataSet
        Try
            objData = New DataObjects(Edi_File_IdentifierSelectQueryPk, EDIData.DPT_ID, CStr(bv_i32Dpt_Id))
            objData.Fill(CType(ds, DataSet), EDIData._EDI_FILE_IDENTIFIER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEdiMovementByDpt_Id() TABLE NAME:Edi_Movement"

    Public Function GetEdiMovementByDpt_Id(ByVal bv_i32Dpt_Id As Int32) As EDIDataSet
        Try
            objData = New DataObjects(Edi_MovementSelectQueryPk, EDIData.DPT_ID, CStr(bv_i32Dpt_Id))
            objData.Fill(CType(ds, DataSet), EDIData._EDI_MOVEMENT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEdiSegmentDetailByDpt_Id() TABLE NAME:Edi_Segment_Detail"

    Public Function GetEdiSegmentDetailByDpt_Id(ByVal bv_i32Dpt_Id As Int32) As EDIDataSet
        Try
            objData = New DataObjects(Edi_Segment_DetailSelectQueryPk, EDIData.DPT_ID, CStr(bv_i32Dpt_Id))
            objData.Fill(CType(ds, DataSet), EDIData._EDI_SEGMENT_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEdiSegmentHeaderByDpt_Id() TABLE NAME:Edi_Segment_Header"

    Public Function GetEdiSegmentHeaderByDpt_Id(ByVal bv_i32Dpt_Id As Int32) As EDIDataSet
        Try
            objData = New DataObjects(Edi_Segment_HeaderSelectQueryPk, EDIData.DPT_ID, CStr(bv_i32Dpt_Id))
            objData.Fill(CType(ds, DataSet), EDIData._EDI_SEGMENT_HEADER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEdiVersionByDpt_id() TABLE NAME:Edi_Version"

    Public Function GetEdiVersionByDpt_Id(ByVal bv_i32Dpt_Id As Int32) As EDIDataSet
        Try
            objData = New DataObjects(Edi_VersionSelectQueryPk, EDIData.DPT_ID, CStr(bv_i32Dpt_Id))
            objData.Fill(CType(ds, DataSet), EDIData._EDI_VERSION)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "GetGateinRetByGI_DPT_TRMDS() TABLE NAME:GATEIN_RET"

    Public Function GetGateinRetByGI_DPT_TRMDS(ByVal bv_strGI_DPT_TRM As String, ByVal bv_strGi_Status As String, ByVal bv_strCustomerCode As String) As EDIDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EDIData.GI_DPT_TRM, bv_strGI_DPT_TRM)
            hshTable.Add(EDIData.GI_STATUS, bv_strGi_Status)
            hshTable.Add(EDIData.GI_LSR_OWNER, bv_strCustomerCode)
            'objData = New DataObjects(GATEIN_RETSelectQueryByGI_DPT_TRM, hshTable)
            objData = New DataObjects(GATEIN_RETNoRCSelectQueryByGI_DPT_TRM, hshTable)
            objData.Fill(CType(ds, DataSet), EDIData._GATEIN_RET)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetGateoutRetByGO_DPT_TRMDS() TABLE NAME:GATEOUT_RET"

    Public Function GetGateoutRetByGO_DPT_TRMDS(ByVal bv_strGO_DPT_TRM As String, ByVal bv_strGO_Status As String, ByVal bv_strCustomerCode As String) As EDIDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EDIData.GO_DPT_TRM, bv_strGO_DPT_TRM)
            hshTable.Add(EDIData.GO_STATUS, bv_strGO_Status)
            hshTable.Add(EDIData.GO_LSR_OWNER, bv_strCustomerCode)
            objData = New DataObjects(GATEOUT_RETSelectQueryByGO_DPT_TRM, hshTable)
            objData.Fill(CType(ds, DataSet), EDIData._GATEOUT_RET)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetGateinRetNoRCByGI_DPT_TRMDS() TABLE NAME:GATEIN_RET"

    Public Function GetGateinRetNoRCByGI_DPT_TRMDS(ByVal bv_strGI_DPT_TRM As String, ByVal bv_strGi_Status As String, ByVal bv_strCustomerCode As String) As EDIDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EDIData.GI_DPT_TRM, bv_strGI_DPT_TRM)
            hshTable.Add(EDIData.GI_STATUS, bv_strGi_Status)
            hshTable.Add(EDIData.GI_LSR_OWNER, bv_strCustomerCode)
            objData = New DataObjects(GATEIN_RETNoRCSelectQueryByGI_DPT_TRM, hshTable)
            objData.Fill(CType(ds, DataSet), EDIData._GATEIN_RET)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRepairCompleteRetNoRCByGI_DPT_TRMDS() TABLE NAME:GATEIN_RET"

    Public Function GetRepairCompleteRetNoRCByGI_DPT_TRMDS(ByVal bv_strGI_DPT_TRM As String, ByVal bv_strGi_Status As String, ByVal bv_strCustomerCode As String, ByVal bv_GICondition As String) As EDIDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EDIData.GI_DPT_TRM, bv_strGI_DPT_TRM)
            hshTable.Add(EDIData.GI_STATUS, bv_strGi_Status)
            hshTable.Add(EDIData.GI_LSR_OWNER, bv_strCustomerCode)
            hshTable.Add(EDIData.GI_CONDITION, bv_GICondition)
            objData = New DataObjects(GATEIN_RETRCSelectQueryByGI_DPT_TRM, hshTable)
            objData.Fill(CType(ds, DataSet), EDIData._GATEIN_RET)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEDIDetails() TABLE NAME:EDI"

    Public Function GetEDIDetails(ByVal intDepotID As Int32) As EDIDataSet
        'ByVal bv_strCustomerCode As String, ByVal strActivity As String
        Try
            Dim hshTable As New Hashtable

            'hshTable.Add(EDIData.EDI_ACTVTY_NAM, strActivity)
            hshTable.Add(EDIData.DPT_ID, intDepotID)
            objData = New DataObjects(GET_EDI_DETAILSbyActivityandCustomer, hshTable)
            objData.Fill(CType(ds, DataSet), EDIData._EDI)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateEDI() TABLE NAME:EDI"

    Public Function CreateEDI(ByVal bv_intCstme_id As Int64, ByVal bv_strCustomerCode As String, ByVal strActivity As String, ByVal strGenarated As DateTime, _
                              ByVal bv_strResnd As String, ByVal bv_strAscFile As String, ByVal bv_strFileformat As String, _
                              ByVal bv_strCreated As String, ByVal bv_strCreatedDt As DateTime, _
                              ByVal bv_intDPT_ID As Int32, ByVal bv_strDPT_CD As String, ByRef br_objTransaction As Transactions) As Long


        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(EDIData._EDI).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EDIData._EDI, br_objTransaction)
                .Item(EDIData.EDI_ID) = intMax
                .Item(EDIData.CSTMR_CD) = bv_strCustomerCode
                If strActivity <> Nothing Then
                    .Item(EDIData.EDI_ACTVTY_NAM) = strActivity
                Else
                    .Item(EDIData.EDI_ACTVTY_NAM) = DBNull.Value
                End If
                .Item(EDIData.GNRTD_DT_TM) = strGenarated
                .Item(EDIData.RSND_BIT) = bv_strResnd
                .Item(EDIData.CSTMR_ID) = bv_intCstme_id

                If bv_strAscFile <> Nothing Then
                    .Item(EDIData.ANC_FL_NM) = bv_strAscFile
                    .Item(EDIData.CDC_FL_NM) = bv_strAscFile
                Else
                    .Item(EDIData.ANC_FL_NM) = DBNull.Value
                    .Item(EDIData.CDC_FL_NM) = DBNull.Value
                End If
                If bv_strFileformat <> Nothing Then
                    .Item(EDIData.FLE_FRMT) = bv_strFileformat
                Else
                    .Item(EDIData.FLE_FRMT) = DBNull.Value
                End If

                .Item(EDIData.TMS_SNT) = 0
                .Item(EDIData.CRTD_BY) = bv_strCreated
                .Item(EDIData.CRTD_DT) = bv_strCreatedDt
                .Item(EDIData.DPT_ID) = bv_intDPT_ID
                .Item(EDIData.DPT_CD) = bv_strDPT_CD

            End With
            objData.InsertRow(dr, EDIInsertQuery, br_objTransaction)
            dr = Nothing
            CreateEDI = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_GetDepot"
    Public Function GetDepoDetails(ByVal intDepot As Integer) As EDIDataSet
        Try
            objData = New DataObjects(DepotDetailsDetailsQuery, BulkEmailData.DPT_ID, intDepot)
            objData.Fill(CType(ds, DataSet), BulkEmailData._DEPOT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "CreateEDIEmail() TABLE NAME:EDI"

    Public Function CreateEDIEmail(ByVal bv_lngEdi As Int64, ByVal bv_intCustomer As Int64, _
                                   ByVal bv_strFromMain As String, _
                                   ByVal bv_strTomail As String, _
                                   ByVal bv_strBCCmail As String, _
                                   ByVal bv_strSubject As String, _
                                   ByVal bv_strEmailbody As String, _
                                   ByVal bv_intDepoId As Int64, _
                                   ByVal bv_strUser As String, _
                                   ByVal bv_dtCurrent As DateTime, _
                                   ByVal bv_strmailMode As String, _
                                   ByVal bv_blnSent As Boolean, _
                                   ByRef br_objTransaction As Transactions) As Long


        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(EDIData._EDI_EMAIL_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EDIData._EDI_EMAIL_DETAIL, br_objTransaction)
                .Item(EDIData.ED_EML_ID) = intMax
                .Item(EDIData.EDI_ID) = bv_lngEdi
                .Item(EDIData.CSTMR_ID) = bv_intCustomer
                .Item(EDIData.TO_EML) = bv_strTomail
                .Item(EDIData.FRM_EML) = bv_strFromMain
                .Item(EDIData.DPT_ID) = bv_intDepoId

                If bv_strBCCmail <> Nothing Then
                    .Item(EDIData.BCC_EML) = bv_strBCCmail
                Else
                    .Item(EDIData.BCC_EML) = DBNull.Value

                End If
                If bv_strSubject <> Nothing Then
                    .Item(EDIData.SBJCT_VC) = bv_strSubject
                Else
                    .Item(EDIData.SBJCT_VC) = DBNull.Value
                End If
                If bv_strEmailbody <> Nothing Then
                    .Item(EDIData.BDY_VC) = bv_strEmailbody
                Else
                    .Item(EDIData.BDY_VC) = DBNull.Value
                End If


                .Item(EDIData.CRTD_BY) = bv_strUser
                .Item(EDIData.CRTD_DT) = bv_dtCurrent
                .Item(EDIData.SNT_BT) = 1
                .Item(EDIData.SNT_DT) = bv_dtCurrent

            End With
            objData.InsertRow(dr, Edi_Email_DetailInsertQuery, br_objTransaction)
            dr = Nothing
            CreateEDIEmail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
#Region "UpdateEDIEmail() TABLE NAME:EDI"

    Public Function UpdateEDIEmaildetail(ByVal bv_lngEdi As Int64, ByVal bv_strUser As String, _
  ByVal bv_dtCurrent As DateTime, ByRef objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EDIData._EDI).NewRow()
            With dr
                .Item(EDIData.LST_SNT_BY) = bv_strUser
                .Item(EDIData.LST_SNT_DT) = bv_dtCurrent
                .Item(EDIData.EDI_ID) = bv_lngEdi
            End With
            UpdateEDIEmaildetail = objData.UpdateRow(dr, update_EDI_Emaildetailquery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetEDIEmailDetails() TABLE NAME:EDI"

    Public Function GetEDIEmailDetails(ByVal intEdiNo As Int64) As EDIDataSet
        Try
            objData = New DataObjects(GET_EDI_EMAIL_DETAILquery, EDIData.EDI_ID, intEdiNo)
            objData.Fill(CType(ds, DataSet), EDIData._EDI_EMAIL_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
#Region "GetEDIEmailDetail()   TABLE NAME:Bulk_Email_Detail"

    Public Function GetEDIEmailDetail(ByVal bv_intEdiId As Int64) As EDIDataSet
        Try
            objData = New DataObjects(GET_EDI_EMAIL_DETAILquery, EDIData.EDI_ID, bv_intEdiId)
            objData.Fill(CType(ds, DataSet), EDIData._EDI_EMAIL_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetEDISettingsDetails() TABLE NAME:EDI_Settings"

    Public Function GetEDISettingsDetails(ByVal bv_i32DepotID As Integer) As EDIDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EDIData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(GET_EDI_Settings_DETAILSelectquery, hshTable)
            objData.Fill(CType(ds, DataSet), EDIData._V_EDI_SETTINGS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateEdiSettings() TABLE NAME:EDI_SETTINGS"

    Public Function CreateEdiSettings(ByVal bv_CstmrId As Int32, _
        ByVal bv_i32FileType As Int32, _
        ByVal bv_strToMail As String, _
        ByVal bv_strCCMail As String, _
        ByVal bv_dtGenaratedTime As DateTime, _
         ByVal bv_strGenaratedTime As String, _
         ByVal bv_strMailSubject As String, _
        ByVal bv_blnActive As Boolean, _
        ByVal bv_datCreated As DateTime, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_intDepotID As Integer, _
         ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(EDIData._EDI_SETTINGS).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(EDIData._EDI_SETTINGS, br_objTransaction)
                .Item(EDIData.EDI_STTNGS_ID) = intMax
                If bv_CstmrId <> 0 Then
                    .Item(EDIData.CSTMR_ID) = bv_CstmrId
                Else
                    .Item(EDIData.CSTMR_ID) = DBNull.Value
                End If
                If bv_i32FileType <> 0 Then
                    .Item(EDIData.FLE_FRMT_ID) = bv_i32FileType
                Else
                    .Item(EDIData.FLE_FRMT_ID) = DBNull.Value
                End If
                If bv_strToMail <> Nothing Then
                    .Item(EDIData.TO_EML) = bv_strToMail
                Else
                    .Item(EDIData.TO_EML) = DBNull.Value
                End If
                If bv_strCCMail <> Nothing Then
                    .Item(EDIData.CC_EML) = bv_strCCMail
                Else
                    .Item(EDIData.CC_EML) = DBNull.Value
                End If
                If bv_dtGenaratedTime <> Nothing Then
                    .Item(EDIData.NXT_RN_DT_TM) = bv_dtGenaratedTime
                Else
                    .Item(EDIData.NXT_RN_DT_TM) = DBNull.Value
                End If
                If bv_blnActive <> Nothing Then
                    .Item(EDIData.ACTV_BT) = bv_blnActive
                Else
                    .Item(EDIData.ACTV_BT) = DBNull.Value
                End If
                If bv_datCreated <> Nothing Then
                    .Item(EDIData.CRTD_DT) = bv_datCreated
                Else
                    .Item(EDIData.CRTD_DT) = DBNull.Value
                End If
                If bv_strCreatedBy <> Nothing Then
                    .Item(EDIData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(EDIData.CRTD_BY) = DBNull.Value
                End If
                If bv_strGenaratedTime <> Nothing Then
                    .Item(EDIData.GNRTN_TM) = bv_strGenaratedTime
                Else
                    .Item(EDIData.GNRTN_TM) = DBNull.Value
                End If
                .Item(EDIData.DPT_ID) = bv_intDepotID
                .Item(EDIData.SBJCT_VCR) = bv_strMailSubject
            End With
            objData.InsertRow(dr, Edi_SettingsInsertQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "UpdateEdiSettings() TABLE NAME:Edi_Settings"

    Public Function UpdateEdiSettings(ByVal bv_i32EdiId As Int32, _
                                      ByVal bv_i32Cstomr_iD As Int32, _
                                      ByVal bv_i32FileFrmat As Int32, _
                                      ByVal bv_strTomail As String, _
                                      ByVal bv_strCCmail As String, _
                                      ByVal bv_datGenDate As DateTime, _
                                      ByVal bv_strTime As String, _
                                      ByVal bv_strSubject As String, _
                                      ByVal bv_blnActive As Boolean, _
                                      ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EDIData._EDI_SETTINGS).NewRow()
            With dr
                If bv_i32EdiId <> 0 Then
                    .Item(EDIData.EDI_STTNGS_ID) = bv_i32EdiId
                Else
                    .Item(EDIData.EDI_STTNGS_ID) = DBNull.Value
                End If
                .Item(EDIData.FLE_FRMT_ID) = bv_i32FileFrmat
                If bv_i32Cstomr_iD <> 0 Then
                    .Item(EDIData.CSTMR_ID) = bv_i32Cstomr_iD
                Else
                    .Item(EDIData.CSTMR_ID) = DBNull.Value
                End If
                If bv_strTomail <> Nothing Then
                    .Item(EDIData.TO_EML) = bv_strTomail
                Else
                    .Item(EDIData.TO_EML) = DBNull.Value
                End If
                If bv_strCCmail <> Nothing Then
                    .Item(EDIData.CC_EML) = bv_strCCmail
                Else
                    .Item(EDIData.CC_EML) = DBNull.Value
                End If
                If bv_datGenDate <> Nothing Then
                    .Item(EDIData.NXT_RN_DT_TM) = bv_datGenDate
                Else
                    .Item(EDIData.NXT_RN_DT_TM) = DBNull.Value
                End If
                If bv_blnActive <> Nothing Then
                    .Item(EDIData.ACTV_BT) = bv_blnActive
                Else
                    .Item(EDIData.ACTV_BT) = DBNull.Value
                End If
                .Item(EDIData.SBJCT_VCR) = bv_strSubject
                .Item(EDIData.GNRTN_TM) = bv_strTime
            End With
            UpdateEdiSettings = objData.UpdateRow(dr, Edi_SettingsUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteEDISettins() TABLE NAME:EDI_SETTINGS"

    Public Function DeleteEDISettings(ByVal bv_streDIsETTINGS As Int64, ByRef objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(EDIData._EDI_SETTINGS).NewRow()
            With dr
                .Item(EDIData.EDI_STTNGS_ID) = bv_streDIsETTINGS
            End With
            DeleteEDISettings = objData.DeleteRow(dr, EdiSettings_DeleteQuery, objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "ValidateEDISettingsDetails() TABLE NAME:EDI_Settings"

    Public Function ValidateEDISettingsDetails(ByVal int_cstmr_id As Int64, ByVal bv_intDepotID As Integer) As EDIDataSet
        Try
            Dim hshTable As New Hashtable
            hshTable.Add(EDIData.CSTMR_ID, int_cstmr_id)
            hshTable.Add(EDIData.DPT_ID, bv_intDepotID)
            '  hshTable.Add(EDIData.FLE_FRMT_CD, strFile)
            objData = New DataObjects(GetEdiSettings, hshTable)
            objData.Fill(CType(ds, DataSet), EDIData._V_EDI_SETTINGS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class
#End Region
