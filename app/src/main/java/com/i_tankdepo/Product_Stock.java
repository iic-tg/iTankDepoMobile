package com.i_tankdepo;

/**
 * Created by Metaplore on 11/10/2016.
 */

public class Product_Stock {
    String name;
    String Id;
    boolean box;


     Product_Stock(String _describe,String _id, boolean _box) {
        name = _describe;
        Id = _id;
        box = _box;
    }
}