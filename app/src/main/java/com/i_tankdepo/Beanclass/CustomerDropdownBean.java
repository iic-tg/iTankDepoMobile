package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Metaplore on 12/1/2016.
 */

public class CustomerDropdownBean implements Serializable {

    String name;
    String code;
    String item_Id;
    String Sub_item_Id;
    String TRFF_CD_DESCRPTN_VC;
    String TRFF_GRP_DESCRPTN_VC;

    public String getSub_item_Id() {
        return Sub_item_Id;
    }

    public void setSub_item_Id(String sub_item_Id) {
        Sub_item_Id = sub_item_Id;
    }

    public String getItem_Id() {
        return item_Id;
    }

    public void setItem_Id(String item_Id) {
        this.item_Id = item_Id;
    }

    public String getTRFF_GRP_DESCRPTN_VC() {
        return TRFF_GRP_DESCRPTN_VC;
    }

    public void setTRFF_GRP_DESCRPTN_VC(String TRFF_GRP_DESCRPTN_VC) {
        this.TRFF_GRP_DESCRPTN_VC = TRFF_GRP_DESCRPTN_VC;
    }

    public String getTRFF_CD_DESCRPTN_VC() {
        return TRFF_CD_DESCRPTN_VC;
    }

    public void setTRFF_CD_DESCRPTN_VC(String TRFF_CD_DESCRPTN_VC) {
        this.TRFF_CD_DESCRPTN_VC = TRFF_CD_DESCRPTN_VC;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }
}
