package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Admin on 12/26/2016.
 */

public class ChanfeOfStatusBean implements Serializable {
    String EquipmentNo;
    String type;
    String Customer;
    String InDate;
    String PreviousCargo;
    String CurrentStatus;
    String CurrentStatusDate;
    String YardLocation;
    String Remarks;

    public String getEquipmentNo() {
        return EquipmentNo;
    }

    public void setEquipmentNo(String equipmentNo) {
        EquipmentNo = equipmentNo;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getCustomer() {
        return Customer;
    }

    public void setCustomer(String customer) {
        Customer = customer;
    }

    public String getInDate() {
        return InDate;
    }

    public void setInDate(String inDate) {
        InDate = inDate;
    }

    public String getPreviousCargo() {
        return PreviousCargo;
    }

    public void setPreviousCargo(String previousCargo) {
        PreviousCargo = previousCargo;
    }

    public String getCurrentStatus() {
        return CurrentStatus;
    }

    public void setCurrentStatus(String currentStatus) {
        CurrentStatus = currentStatus;
    }

    public String getCurrentStatusDate() {
        return CurrentStatusDate;
    }

    public void setCurrentStatusDate(String currentStatusDate) {
        CurrentStatusDate = currentStatusDate;
    }

    public String getYardLocation() {
        return YardLocation;
    }

    public void setYardLocation(String yardLocation) {
        YardLocation = yardLocation;
    }

    public String getRemarks() {
        return Remarks;
    }

    public void setRemarks(String remarks) {
        Remarks = remarks;
    }
}
