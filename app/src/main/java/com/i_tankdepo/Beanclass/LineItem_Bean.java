package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Metaplore on 5/22/2017.
 */

public class LineItem_Bean implements Serializable {
    String item;
    String itemCode;
    String suIitemCode;
    String subItem;
    String tariff_Code;
    String tariff_Group;
    String damage_Code;
    String damage_Code_Id;
    String repair_Code;
    String repair_Code_Id;
    String manHour;
    String manHour_Cost;
    String metial_Cost;
    String Responsibility;
    String Responsibility_Cd;
    String totel;
    String Remark;
    String MHR;
    String RPR_ESTMT_DTL_ID;
    String RPR_ESTMT_ID;
    String DamageDescription;
    String RepairDescription;
    String CheckButton;
    String ChangingBit;
    String CurencyCD;
    String RowState;


    public String getRowState() {
        return RowState;
    }

    public void setRowState(String rowState) {
        RowState = rowState;
    }

    public String getCurencyCD() {
        return CurencyCD;
    }

    public void setCurencyCD(String curencyCD) {
        CurencyCD = curencyCD;
    }
    /*"DamageDescription": "new mat",
        "RepairDescription": "Burnt",
        "CheckButton": "False",
        "ChangingBit": "False"*/

    public String getDamageDescription() {
        return DamageDescription;
    }

    public void setDamageDescription(String damageDescription) {
        DamageDescription = damageDescription;
    }

    public String getRepairDescription() {
        return RepairDescription;
    }

    public void setRepairDescription(String repairDescription) {
        RepairDescription = repairDescription;
    }

    public String getCheckButton() {
        return CheckButton;
    }

    public void setCheckButton(String checkButton) {
        CheckButton = checkButton;
    }

    public String getChangingBit() {
        return ChangingBit;
    }

    public void setChangingBit(String changingBit) {
        ChangingBit = changingBit;
    }

    public String getMHR() {
        return MHR;
    }

    public void setMHR(String MHR) {
        this.MHR = MHR;
    }

    public String getRPR_ESTMT_DTL_ID() {
        return RPR_ESTMT_DTL_ID;
    }

    public void setRPR_ESTMT_DTL_ID(String RPR_ESTMT_DTL_ID) {
        this.RPR_ESTMT_DTL_ID = RPR_ESTMT_DTL_ID;
    }

    public String getRPR_ESTMT_ID() {
        return RPR_ESTMT_ID;
    }

    public void setRPR_ESTMT_ID(String RPR_ESTMT_ID) {
        this.RPR_ESTMT_ID = RPR_ESTMT_ID;
    }

    public String getResponsibility_Cd() {
        return Responsibility_Cd;
    }

    public void setResponsibility_Cd(String responsibility_Cd) {
        Responsibility_Cd = responsibility_Cd;
    }

    public String getRemark() {
        return Remark;
    }

    public void setRemark(String remark) {
        Remark = remark;
    }

    public String getItemCode() {
        return itemCode;
    }

    public void setItemCode(String itemCode) {
        this.itemCode = itemCode;
    }

    public String getSuIitemCode() {
        return suIitemCode;
    }

    public void setSuIitemCode(String suIitemCode) {
        this.suIitemCode = suIitemCode;
    }

    public String getDamage_Code_Id() {
        return damage_Code_Id;
    }

    public void setDamage_Code_Id(String damage_Code_Id) {
        this.damage_Code_Id = damage_Code_Id;
    }

    public String getRepair_Code_Id() {
        return repair_Code_Id;
    }

    public void setRepair_Code_Id(String repair_Code_Id) {
        this.repair_Code_Id = repair_Code_Id;
    }

    public String getItem() {
        return item;
    }

    public void setItem(String item) {
        this.item = item;
    }

    public String getSubItem() {
        return subItem;
    }

    public void setSubItem(String subItem) {
        this.subItem = subItem;
    }

    public String getTariff_Code() {
        return tariff_Code;
    }

    public void setTariff_Code(String tariff_Code) {
        this.tariff_Code = tariff_Code;
    }

    public String getTariff_Group() {
        return tariff_Group;
    }

    public void setTariff_Group(String tariff_Group) {
        this.tariff_Group = tariff_Group;
    }

    public String getDamage_Code() {
        return damage_Code;
    }

    public void setDamage_Code(String damage_Code) {
        this.damage_Code = damage_Code;
    }

    public String getRepair_Code() {
        return repair_Code;
    }

    public void setRepair_Code(String repair_Code) {
        this.repair_Code = repair_Code;
    }

    public String getManHour() {
        return manHour;
    }

    public void setManHour(String manHour) {
        this.manHour = manHour;
    }

    public String getManHour_Cost() {
        return manHour_Cost;
    }

    public void setManHour_Cost(String manHour_Cost) {
        this.manHour_Cost = manHour_Cost;
    }

    public String getMetial_Cost() {
        return metial_Cost;
    }

    public void setMetial_Cost(String metial_Cost) {
        this.metial_Cost = metial_Cost;
    }

    public String getResponsibility() {
        return Responsibility;
    }

    public void setResponsibility(String responsibility) {
        Responsibility = responsibility;
    }

    public String getTotel() {
        return totel;
    }

    public void setTotel(String totel) {
        this.totel = totel;
    }
}
