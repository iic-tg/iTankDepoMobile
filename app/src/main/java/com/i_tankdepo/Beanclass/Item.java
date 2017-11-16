package com.i_tankdepo.Beanclass;

/**
 * Created by Metaplore on 11/10/2016.
 */

public class Item {
    public final String name;
    final String drawableId;
    boolean box;

    public Item(String name, String drawableId)
    {
        this.name = name;
        this.drawableId = drawableId;
    }
}