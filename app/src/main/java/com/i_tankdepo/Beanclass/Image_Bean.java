package com.i_tankdepo.Beanclass;

import java.io.Serializable;

/**
 * Created by Metaplore on 5/19/2017.
 */

public class Image_Bean implements Serializable {
    String image;
    String image_Name;
    String image_length;
    String uriPath;
    String attachment_Id;
    String Base64;

    public String getBase64() {
        return Base64;
    }

    public void setBase64(String base64) {
        Base64 = base64;
    }

    public String getAttachment_Id() {
        return attachment_Id;
    }

    public void setAttachment_Id(String attachment_Id) {
        this.attachment_Id = attachment_Id;
    }

    public String getUriPath() {
        return uriPath;
    }

    public void setUriPath(String uriPath) {
        this.uriPath = uriPath;
    }

    public String getImage_length() {
        return image_length;
    }

    public void setImage_length(String image_length) {
        this.image_length = image_length;
    }

    public String getImage() {
        return image;
    }

    public void setImage(String image) {
        this.image = image;
    }

    public String getImage_Name() {
        return image_Name;
    }

    public void setImage_Name(String image_Name) {
        this.image_Name = image_Name;
    }
}
