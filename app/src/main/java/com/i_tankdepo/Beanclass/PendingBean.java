package com.i_tankdepo.Beanclass;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by Admin on 11/24/2016.
 */

public class PendingBean implements Serializable {
    String filename;
    String customerName;
    String date;
    String time;
    String equipmentNo;
    String type;
    String previousCargo;
    String attachmentStatus;
    String gateIn_Id;
    String code;
    String location;
    String vechicle;
    String transport;
    String eir_no;
    String heating_bt;
    String status;
    String rental_bt;
    String remark;
    String Cust_code;
    String type_code;
    String code_Id;
    String selected_code;
    String prev_code;
    String prev_Id;
    String PR_ADVC_CD;
    String URL;

    //Added by Anantha krishnan
    String CNSGN_LCTN;
    String CNSGN_NAM;

    String GI_TRNSCTN_NO;
    String Attach_ID;
    String customerID;
    String EIMNFCTR_DT;
    String     EITR_WGHT_NC;
    String      EIGRSS_WGHT_NC;
    String      EICPCTY_NC;
    String      EILST_SRVYR_NM;
    String      EILST_TST_DT;
    String      EILST_TST_TYP_ID;
    String      EINXT_TST_DT;
    String     EINXT_TST_TYP_ID;
    String      EIRMRKS_VC;

    public String getURL() {
        return URL;
    }

    public void setURL(String URL) {
        this.URL = URL;
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

    public String getEILST_SRVYR_NM() {
        return EILST_SRVYR_NM;
    }

    public void setEILST_SRVYR_NM(String EILST_SRVYR_NM) {
        this.EILST_SRVYR_NM = EILST_SRVYR_NM;
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

    List<String> list1 = new ArrayList<>();

    public List<String> getList1() {
        return list1;
    }

    public void setList1(List<String> list1) {
        this.list1 = list1;
    }

    public String getCustomerName() {
        return customerName;
    }

    public void setCustomerName(String customerName) {
        this.customerName = customerName;
    }

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public String getTime() {
        return time;
    }

    public void setTime(String time) {
        this.time = time;
    }

    public String getEquipmentNo() {
        return equipmentNo;
    }

    public void setEquipmentNo(String equipmentNo) {
        this.equipmentNo = equipmentNo;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getPreviousCargo() {
        return previousCargo;
    }

    public void setPreviousCargo(String previousCargo) {
        this.previousCargo = previousCargo;
    }

    public String getAttachmentStatus() {
        return attachmentStatus;
    }

    public void setAttachmentStatus(String attachmentStatus) {
        this.attachmentStatus = attachmentStatus;
    }

    public String getGateIn_Id() {
        return gateIn_Id;
    }

    public void setGateIn_Id(String gateIn_Id) {
        this.gateIn_Id = gateIn_Id;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public String getVechicle() {
        return vechicle;
    }

    public void setVechicle(String vechicle) {
        this.vechicle = vechicle;
    }

    public String getTransport() {
        return transport;
    }

    public void setTransport(String transport) {
        this.transport = transport;
    }

    public String getEir_no() {
        return eir_no;
    }

    public void setEir_no(String eir_no) {
        this.eir_no = eir_no;
    }

    public String getHeating_bt() {
        return heating_bt;
    }

    public void setHeating_bt(String heating_bt) {
        this.heating_bt = heating_bt;
    }

    public String getRental_bt() {
        return rental_bt;
    }

    public void setRental_bt(String rental_bt) {
        this.rental_bt = rental_bt;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getCust_code() {
        return Cust_code;
    }

    public void setCust_code(String cust_code) {
        Cust_code = cust_code;
    }

    public String getType_code() {
        return type_code;
    }

    public void setType_code(String type_code) {
        this.type_code = type_code;
    }

    public String getPrev_code() {
        return prev_code;
    }

    public void setPrev_code(String prev_code) {
        this.prev_code = prev_code;
    }

    public String getPrev_Id() {
        return prev_Id;
    }

    public void setPrev_Id(String prev_Id) {
        this.prev_Id = prev_Id;
    }

    public String getCode_Id() {
        return code_Id;
    }

    public void setCode_Id(String code_Id) {
        this.code_Id = code_Id;
    }

    public String getSelected_code() {
        return selected_code;
    }

    public void setSelected_code(String selected_code) {
        this.selected_code = selected_code;
    }

    public String getPR_ADVC_CD() {
        return PR_ADVC_CD;
    }

    public void setPR_ADVC_CD(String PR_ADVC_CD) {
        this.PR_ADVC_CD = PR_ADVC_CD;
    }


    public String getGI_TRNSCTN_NO() {
        return GI_TRNSCTN_NO;
    }

    public void setGI_TRNSCTN_NO(String GI_TRNSCTN_NO) {
        this.GI_TRNSCTN_NO = GI_TRNSCTN_NO;
    }

    public String getFilename() {
        return filename;
    }

    public String getAttach_ID() {
        return Attach_ID;
    }

    public void setAttach_ID(String attach_ID) {
        Attach_ID = attach_ID;
    }

    public String getCustomerID() {
        return customerID;
    }

    public void setCustomerID(String customerID) {
        this.customerID = customerID;
    }

    public void setFilename(String filename) {
        this.filename = filename;
    }

    //Added by Anantha krishnan
    public String getConsigneeName() {
        return CNSGN_NAM;
    }

    public void setConsigneeName(String consigneeName) {
        this.CNSGN_NAM = consigneeName;
    }

    public String getConsigneeLocation() {
        return CNSGN_LCTN;
    }

    public void setConsigneeLocation(String consigneeLocation) {
        this.CNSGN_LCTN = consigneeLocation;
    }
}
