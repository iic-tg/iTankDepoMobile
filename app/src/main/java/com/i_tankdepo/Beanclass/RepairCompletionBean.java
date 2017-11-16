package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Admin on 12/21/2016.
 */

public class RepairCompletionBean implements Serializable {
    String Equip_no;
    String type;
    String code;
    String status;
    String estimate_no;
    String customer;
    String RepaiType;
    String location;
    String EstimatedManHor;
    String ActualManHour;
    String CompletionDate;
    String Time;
    String Remarks;
    String attchement;
    String Activity_Date;

    public String getActivity_Date() {
        return Activity_Date;
    }

    public void setActivity_Date(String activity_Date) {
        Activity_Date = activity_Date;
    }

    public String getEquip_no() {
        return Equip_no;
    }

    public void setEquip_no(String equip_no) {
        Equip_no = equip_no;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getEstimate_no() {
        return estimate_no;
    }

    public void setEstimate_no(String estimate_no) {
        this.estimate_no = estimate_no;
    }

    public String getCustomer() {
        return customer;
    }

    public void setCustomer(String customer) {
        this.customer = customer;
    }

    public String getRepaiType() {
        return RepaiType;
    }

    public void setRepaiType(String repaiType) {
        RepaiType = repaiType;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public String getEstimatedManHor() {
        return EstimatedManHor;
    }

    public void setEstimatedManHor(String estimatedManHor) {
        EstimatedManHor = estimatedManHor;
    }

    public String getActualManHour() {
        return ActualManHour;
    }

    public void setActualManHour(String actualManHour) {
        ActualManHour = actualManHour;
    }

    public String getCompletionDate() {
        return CompletionDate;
    }

    public void setCompletionDate(String completionDate) {
        CompletionDate = completionDate;
    }

    public String getTime() {
        return Time;
    }

    public void setTime(String time) {
        Time = time;
    }

    public String getRemarks() {
        return Remarks;
    }

    public void setRemarks(String remarks) {
        Remarks = remarks;
    }

    public String getAttchement() {
        return attchement;
    }

    public void setAttchement(String attchement) {
        this.attchement = attchement;
    }
}
