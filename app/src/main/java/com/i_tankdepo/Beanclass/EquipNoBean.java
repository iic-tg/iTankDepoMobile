package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Admin on 12/23/2016.
 */

public class EquipNoBean implements Serializable {
    String Equip_no;
    String InDate;
    String CurrentStatus;
    String Type;
    String Customer;
    String Depot;
    String YrdLocation;

    public String getEquip_no() {
        return Equip_no;
    }

    public void setEquip_no(String equip_no) {
        Equip_no = equip_no;
    }

    public String getInDate() {
        return InDate;
    }

    public void setInDate(String inDate) {
        InDate = inDate;
    }

    public String getCurrentStatus() {
        return CurrentStatus;
    }

    public void setCurrentStatus(String currentStatus) {
        CurrentStatus = currentStatus;
    }

    public String getType() {
        return Type;
    }

    public void setType(String type) {
        Type = type;
    }

    public String getCustomer() {
        return Customer;
    }

    public void setCustomer(String customer) {
        Customer = customer;
    }

    public String getDepot() {
        return Depot;
    }

    public void setDepot(String depot) {
        Depot = depot;
    }

    public String getYrdLocation() {
        return YrdLocation;
    }

    public void setYrdLocation(String yrdLocation) {
        YrdLocation = yrdLocation;
    }
}
