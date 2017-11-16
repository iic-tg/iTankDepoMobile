package com.i_tankdepo.Beanclass;

import java.io.Serializable;


/**
 * Created by Admin on 12/26/2016.
 */

public class CurrentStatusBean implements Serializable {
    String name;
    String ID;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getID() {
        return ID;
    }

    public void setID(String ID) {
        this.ID = ID;
    }
}
