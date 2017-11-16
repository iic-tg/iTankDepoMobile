package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Metaplore on 12/8/2016.
 */

public class Equipment_Info_TypeDropdownBean implements Serializable {

    String id;
    String code;
    String TRFF_CD_DESCRPTN_VC;

    public String getTRFF_CD_DESCRPTN_VC() {
        return TRFF_CD_DESCRPTN_VC;
    }

    public void setTRFF_CD_DESCRPTN_VC(String TRFF_CD_DESCRPTN_VC) {
        this.TRFF_CD_DESCRPTN_VC = TRFF_CD_DESCRPTN_VC;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }
}
