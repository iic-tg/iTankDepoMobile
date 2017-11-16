package com.i_tankdepo;

/**
 * Created by Metaplore on 11/10/2016.
 */
/*
* GlobalConstants this class for global variable (using for share the details from one class to another class)
* ConstantValues this class for Service link class(using for what are All the service or API link in my application, Maintained here)
* sp.getString(SP_USER_ID, "user_Id") like this (Storing the values on Shared Preferance, This Shared preferance in Comman Activity calss)
*  Comman Activity (Am using extends CommanActivity for all clsss because some method am using comman for all activity

			- MoreInfo PopUp().
			- ShortTost().
			- LongTost().
			-RegularFont().
			-showDialogWithLV().
			-Remark PopUp.
			-SharedPreferences
)
* */
public class COS_Product {
    String name;
    String pre_cargo;
    String inDate;
    String equip_no;
    String current_status;
    String current_status_date;
    String type;
    String location;
    String remarks;
    boolean box;
/* products.add(new COS_Product(jsonObject.getString("EquipmentNo"),jsonObject.getString("PreviousCargo"),
                                    jsonObject.getString("InDate") ,jsonObject.getString("Customer"),
                                    jsonObject.getString("CurrentStatus"),
                                    jsonObject.getString("Type"),
                                    jsonObject.getString("YardLocation"),
                                    jsonObject.getString("Remarks"),false));

*/

     COS_Product(String _describe,String _pre_crg,String _indate,String _equip_no,String _current_status,String _current_status_date,String _type,String _location,String _remarks, boolean _box) {
        name = _describe;
        pre_cargo = _pre_crg;
        inDate = _indate;
        equip_no = _equip_no;
        current_status = _current_status;
        current_status_date = _current_status_date;
        type = _type;
        location = _location;
        remarks = _remarks;
        box = _box;
    }
}