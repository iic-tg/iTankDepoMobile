package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Metaplore on 1/4/2017.
 */

public class CustomerReportBean implements Serializable {
    String Customer;
    String Type;
    String IND;
    String PHL;
    String ACN;
    String AWECLN;
    String AWE;
    String AAR;
    String AUR;
    String ASR;
    String SRV;
    String AVLCLN;
    String AVLINS;
    String INSRPC;
    String RPC;
    String STO;
    String AVL;
    String OUT;
    String TOTAL;

    public String getCustomer() {
        return Customer;
    }

    public void setCustomer(String customer) {
        Customer = customer;
    }

    public String getType() {
        return Type;
    }

    public void setType(String type) {
        Type = type;
    }

    public String getIND() {
        return IND;
    }

    public void setIND(String IND) {
        this.IND = IND;
    }

    public String getPHL() {
        return PHL;
    }

    public void setPHL(String PHL) {
        this.PHL = PHL;
    }

    public String getACN() {
        return ACN;
    }

    public void setACN(String ACN) {
        this.ACN = ACN;
    }

    public String getAWECLN() {
        return AWECLN;
    }

    public void setAWECLN(String AWECLN) {
        this.AWECLN = AWECLN;
    }

    public String getAWE() {
        return AWE;
    }

    public void setAWE(String AWE) {
        this.AWE = AWE;
    }

    public String getAAR() {
        return AAR;
    }

    public void setAAR(String AAR) {
        this.AAR = AAR;
    }

    public String getAUR() {
        return AUR;
    }

    public void setAUR(String AUR) {
        this.AUR = AUR;
    }

    public String getASR() {
        return ASR;
    }

    public void setASR(String ASR) {
        this.ASR = ASR;
    }

    public String getSRV() {
        return SRV;
    }

    public void setSRV(String SRV) {
        this.SRV = SRV;
    }

    public String getAVLCLN() {
        return AVLCLN;
    }

    public void setAVLCLN(String AVLCLN) {
        this.AVLCLN = AVLCLN;
    }

    public String getAVLINS() {
        return AVLINS;
    }

    public void setAVLINS(String AVLINS) {
        this.AVLINS = AVLINS;
    }

    public String getINSRPC() {
        return INSRPC;
    }

    public void setINSRPC(String INSRPC) {
        this.INSRPC = INSRPC;
    }

    public String getRPC() {
        return RPC;
    }

    public void setRPC(String RPC) {
        this.RPC = RPC;
    }

    public String getSTO() {
        return STO;
    }

    public void setSTO(String STO) {
        this.STO = STO;
    }

    public String getAVL() {
        return AVL;
    }

    public void setAVL(String AVL) {
        this.AVL = AVL;
    }

    public String getOUT() {
        return OUT;
    }

    public void setOUT(String OUT) {
        this.OUT = OUT;
    }

    public String getTOTAL() {
        return TOTAL;
    }

    public void setTOTAL(String TOTAL) {
        this.TOTAL = TOTAL;
    }
}
