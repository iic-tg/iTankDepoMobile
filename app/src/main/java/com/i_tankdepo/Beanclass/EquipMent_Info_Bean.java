package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Metaplore on 12/5/2016.
 */

public class EquipMent_Info_Bean implements Serializable {

    /* "__type": "EquipmentInfoMobileModel",
    "EIEQPMNT_NO": "TEST3333333",
   EIMNFCTR_DT,
      EITR_WGHT_NC,
      EIGRSS_WGHT_NC,
      EICPCTY_NC,
      EILST_SRVYR_NM,
      EILST_TST_DT,
      EILST_TST_TYP_ID,
       EINXT_TST_DT,
       EINXT_TST_TYP_ID,
       EIRMRKS_VC,
       EIACTV_BT,
       EIRNTL_BT,
       EIAttachment,
       EIHasChanges,
       PageName,
       GateinTransactionNo
    * */
    String EIEQPMNT_TYP_CD;
    String EIMNFCTR_DT;
    String EITR_WGHT_NC;
    String EIGRSS_WGHT_NC;
    String EICPCTY_NC;
    String EILST_SRVYR_NM;
    String EILST_TST_DT;
    String EILST_TST_TYP_ID;
    String EINXT_TST_DT;
    String EINXT_TST_TYP_ID;
    String EIRMRKS_VC;
    String EIACTV_BT;
    String EIRNTL_BT;

    public String getEIEQPMNT_TYP_CD() {
        return EIEQPMNT_TYP_CD;
    }

    public void setEIEQPMNT_TYP_CD(String EIEQPMNT_TYP_CD) {
        this.EIEQPMNT_TYP_CD = EIEQPMNT_TYP_CD;
    }

    public String getEIMNFCTR_DT() {
        return EIMNFCTR_DT;
    }

    public void setEIMNFCTR_DT(String EIMNFCTR_DT) {
        this.EIMNFCTR_DT = EIMNFCTR_DT;
    }

    public String getEITR_WGHT_NC() {
        return EITR_WGHT_NC;
    }

    public void setEITR_WGHT_NC(String EITR_WGHT_NC) {
        this.EITR_WGHT_NC = EITR_WGHT_NC;
    }

    public String getEIGRSS_WGHT_NC() {
        return EIGRSS_WGHT_NC;
    }

    public void setEIGRSS_WGHT_NC(String EIGRSS_WGHT_NC) {
        this.EIGRSS_WGHT_NC = EIGRSS_WGHT_NC;
    }

    public String getEICPCTY_NC() {
        return EICPCTY_NC;
    }

    public void setEICPCTY_NC(String EICPCTY_NC) {
        this.EICPCTY_NC = EICPCTY_NC;
    }

    public String getEILST_TST_DT() {
        return EILST_TST_DT;
    }

    public void setEILST_TST_DT(String EILST_TST_DT) {
        this.EILST_TST_DT = EILST_TST_DT;
    }

    public String getEILST_TST_TYP_ID() {
        return EILST_TST_TYP_ID;
    }

    public void setEILST_TST_TYP_ID(String EILST_TST_TYP_ID) {
        this.EILST_TST_TYP_ID = EILST_TST_TYP_ID;
    }

    public String getEILST_SRVYR_NM() {
        return EILST_SRVYR_NM;
    }

    public void setEILST_SRVYR_NM(String EILST_SRVYR_NM) {
        this.EILST_SRVYR_NM = EILST_SRVYR_NM;
    }

    public String getEINXT_TST_DT() {
        return EINXT_TST_DT;
    }

    public void setEINXT_TST_DT(String EINXT_TST_DT) {
        this.EINXT_TST_DT = EINXT_TST_DT;
    }

    public String getEINXT_TST_TYP_ID() {
        return EINXT_TST_TYP_ID;
    }

    public void setEINXT_TST_TYP_ID(String EINXT_TST_TYP_ID) {
        this.EINXT_TST_TYP_ID = EINXT_TST_TYP_ID;
    }

    public String getEIRMRKS_VC() {
        return EIRMRKS_VC;
    }

    public void setEIRMRKS_VC(String EIRMRKS_VC) {
        this.EIRMRKS_VC = EIRMRKS_VC;
    }

    public String getEIACTV_BT() {
        return EIACTV_BT;
    }

    public void setEIACTV_BT(String EIACTV_BT) {
        this.EIACTV_BT = EIACTV_BT;
    }

    public String getEIRNTL_BT() {
        return EIRNTL_BT;
    }

    public void setEIRNTL_BT(String EIRNTL_BT) {
        this.EIRNTL_BT = EIRNTL_BT;
    }
}
