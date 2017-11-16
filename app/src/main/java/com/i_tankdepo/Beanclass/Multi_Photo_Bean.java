package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Sriram on 2/7/2017.
 */

public class Multi_Photo_Bean implements Serializable {

    String base64;
    String attachment_Id;
    String name;
    String length;
    String pathUrl;
    String EquipmentNo;
    String attchPath;


    public String getAttchPath() {
        return attchPath;
    }

    public void setAttchPath(String attchPath) {
        this.attchPath = attchPath;
    }

    public String getEquipmentNo() {
        return EquipmentNo;
    }

    public void setEquipmentNo(String equipmentNo) {
        EquipmentNo = equipmentNo;
    }

    public String getPathUrl() {
        return pathUrl;
    }

    public void setPathUrl(String pathUrl) {
        this.pathUrl = pathUrl;
    }

    public String getAttachment_Id() {
        return attachment_Id;
    }

    public void setAttachment_Id(String attachment_Id) {
        this.attachment_Id = attachment_Id;
    }

    public String getBase64() {
        return base64;
    }

    public void setBase64(String base64) {
        this.base64 = base64;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getLength() {
        return length;
    }

    public void setLength(String length) {
        this.length = length;
    }
}
