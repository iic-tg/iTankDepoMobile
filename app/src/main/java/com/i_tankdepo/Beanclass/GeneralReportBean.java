package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Metaplore on 1/4/2017.
 */

public class GeneralReportBean implements Serializable {
    String Depot;
    String Customer;
    String EquipmentNo;
    String Type;
    String Indate;
    String PreviousCargo;
    String EirNo;
    String CleaningCertNo;
    String CurrentStatusDate;
    String CurrentStatus;
    String CleaningDate;
    String InspectionDate;
    String Remarks;
    String NextTestDate;
    String NextTestType;

    public String getDepot() {
        return Depot;
    }

    public void setDepot(String depot) {
        Depot = depot;
    }

    public String getCustomer() {
        return Customer;
    }

    public void setCustomer(String customer) {
        Customer = customer;
    }

    public String getEquipmentNo() {
        return EquipmentNo;
    }

    public void setEquipmentNo(String equipmentNo) {
        EquipmentNo = equipmentNo;
    }

    public String getType() {
        return Type;
    }

    public void setType(String type) {
        Type = type;
    }

    public String getIndate() {
        return Indate;
    }

    public void setIndate(String indate) {
        Indate = indate;
    }

    public String getPreviousCargo() {
        return PreviousCargo;
    }

    public void setPreviousCargo(String previousCargo) {
        PreviousCargo = previousCargo;
    }

    public String getEirNo() {
        return EirNo;
    }

    public void setEirNo(String eirNo) {
        EirNo = eirNo;
    }

    public String getCleaningCertNo() {
        return CleaningCertNo;
    }

    public void setCleaningCertNo(String cleaningCertNo) {
        CleaningCertNo = cleaningCertNo;
    }

    public String getCurrentStatusDate() {
        return CurrentStatusDate;
    }

    public void setCurrentStatusDate(String currentStatusDate) {
        CurrentStatusDate = currentStatusDate;
    }

    public String getCurrentStatus() {
        return CurrentStatus;
    }

    public void setCurrentStatus(String currentStatus) {
        CurrentStatus = currentStatus;
    }

    public String getCleaningDate() {
        return CleaningDate;
    }

    public void setCleaningDate(String cleaningDate) {
        CleaningDate = cleaningDate;
    }

    public String getInspectionDate() {
        return InspectionDate;
    }

    public void setInspectionDate(String inspectionDate) {
        InspectionDate = inspectionDate;
    }

    public String getRemarks() {
        return Remarks;
    }

    public void setRemarks(String remarks) {
        Remarks = remarks;
    }

    public String getNextTestDate() {
        return NextTestDate;
    }

    public void setNextTestDate(String nextTestDate) {
        NextTestDate = nextTestDate;
    }

    public String getNextTestType() {
        return NextTestType;
    }

    public void setNextTestType(String nextTestType) {
        NextTestType = nextTestType;
    }
}
