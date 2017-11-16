package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Metaplore on 7/27/2017.
 */

public class Customer_Calculation_Bean implements Serializable {

    String CSTMR_ID;
    String CSTMR_CD;
    String CRRNCY_ID;
    String CRRNCY_CD;
    String EXCHNG_RT_PR_UNT_NC;

    public String getCSTMR_ID() {
        return CSTMR_ID;
    }

    public void setCSTMR_ID(String CSTMR_ID) {
        this.CSTMR_ID = CSTMR_ID;
    }

    public String getCSTMR_CD() {
        return CSTMR_CD;
    }

    public void setCSTMR_CD(String CSTMR_CD) {
        this.CSTMR_CD = CSTMR_CD;
    }

    public String getCRRNCY_ID() {
        return CRRNCY_ID;
    }

    public void setCRRNCY_ID(String CRRNCY_ID) {
        this.CRRNCY_ID = CRRNCY_ID;
    }

    public String getCRRNCY_CD() {
        return CRRNCY_CD;
    }

    public void setCRRNCY_CD(String CRRNCY_CD) {
        this.CRRNCY_CD = CRRNCY_CD;
    }

    public String getEXCHNG_RT_PR_UNT_NC() {
        return EXCHNG_RT_PR_UNT_NC;
    }

    public void setEXCHNG_RT_PR_UNT_NC(String EXCHNG_RT_PR_UNT_NC) {
        this.EXCHNG_RT_PR_UNT_NC = EXCHNG_RT_PR_UNT_NC;
    }
}
