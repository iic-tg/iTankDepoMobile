package com.i_tankdepo.SQLite;

import android.content.ContentResolver;

import android.content.ContentValues;

import android.content.Context;
import android.content.UriMatcher;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.database.sqlite.SQLiteQueryBuilder;
import android.database.sqlite.SQLiteStatement;
import android.net.Uri;
import android.os.Environment;
import android.support.annotation.Nullable;
import android.support.annotation.StringDef;
import android.util.Log;
import android.widget.Switch;
import android.widget.Toast;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.channels.FileChannel;
import java.util.HashMap;

import static com.i_tankdepo.R.id.default_activity_button;
import static com.i_tankdepo.R.id.temp;

public class  DBAdapter  {

    private SQLiteDatabase mDatabase;
    public static final String KEY_ROWID = "_id";
    public static final String KEY_NAME = "name";
    public static final String KEY_EMAIL = "email";

    private static final String TAG = "DB";
    private static final String DATABASE_NAME = "ItankDB";

    private static final String DATABASE_TABLE = "create_gatein";
    private static final String DATABASE_TABLE_HEAT = "heating_calc";
    private static final String DATABASE_TABLE_LEAK_TEST = "leak_test";
    private static final String DATABASE_TABLE_UPDATE_GATEOUT = "gateout";

    private static final int DATABASE_VERSION = 1;


    /*GATE IN*/

    public static  final String CUSTOMER = "customer";
    public static  final String EQUIPMENT_NO = "equipment_no";
    public static  final String TYPE = "type";

    public static  final String CODE = "code";
    public static  final String CODE_ID = "code_id";
    public static  final String GATEIN_ID = "gate_id";
    public static  final String MODE = "mode";
    public static  final String CSTMR_ID = "customer_id";
    public static  final String EQPMNT_TYP_ID = "eqp_type_id";
    public static  final String EQPMNT_CD_ID = "eqp_type_cd";
    public static  final String PRDCT_ID = "product_id";
    public static  final String PRDCT_CD = "product_cd";
    public static  final String PAGE_NAME = "pagename";
    public static  final String ATT_STATUS = "att_status";
    public static  final String CHECK = "checked_status";
    public static  final String USER_NAME = "username";
    public static  final String REP_EST_ID = "rep_est_id";
    public static  final String INFO_ATT = "info_att";
    public static  final String INFO_CHANGE = "info_changes";
    public static  final String GATEIN_TRAN_NO = "gatein_trans_no";
    public static  final String LOCATION = "location";
    public static  final String INDATE = "indate";
    public static  final String TIME = "time";
    public static  final String PREVIOUSCARGO = "previous_crg";
    public static  final String EIRNO = "eir_no";
    public static  final String VEHICLE_NO = "vehicle_no";
    public static  final String TRANSPORTER = "transporter";
    public static  final String REMARKS = "remarks";
    public static  final String HEATING_BIT = "heat_bit";
    public static  final String RENTAL_BIT = "rental_bit";

    public static  final String MANUF_DATE = "manuf_date";
    public static  final String TARE_WEIGHT = "tare_weight";
    public static  final String GROSS_WEIGHT = "gross_weight";
    public static  final String CAPACITY = "capacity";
    public static  final String LAST_SURVEYOR = "last_surveyor";
    public static  final String LAST_TESTDATE = "last_testdate";
    public static  final String LAST_TESTTYPE = "last_testtype";
    public static  final String NEXT_TESTDATE = "next_testdate";
    public static  final String NEXT_TESTTYPE = "next_testtype";
    public static  final String INFO_REMARKS = "info_remarks";
    public static  final String INFO_ACTIVE_BIT = "info_active";
    public static  final String INFO_RENTAL_BIT = "info_rental";

    //Added by Anantha krishnan
    public static  final String CONSIGNEE_NAME = "get_consignee_name";
    public static  final String CONSIGNEE_LOCATION = "get_consignee_location";




    private static final String DATABASE_CREATE_GATEIN =
            "create table create_gatein (_id integer primary key autoincrement, "
                    + "customer text not null, customer_id text not null, gate_id text not null, mode text not null, pagename text not null, rep_est_id text not null, " +
                    "username text not null, info_att text not null, info_changes text not null, gatein_trans_no text, att_status text not null, checked_status text not null," +
                    " equipment_no text not null, type text not null, eqp_type_id text not null, code text not null, code_id text not null, location text , indate text not null," +
                    " time text not null, previous_crg text not null, product_id text not null, product_cd text not null, eir_no text , vehicle_no text , transporter text , " +
                    " remarks text , heat_bit text , rental_bit text , manuf_date text , tare_weight text , gross_weight text , capacity text , last_surveyor text ," +
                    " last_testdate text , last_testtype text , next_testdate text , next_testtype text , info_remarks text , info_active text , info_rental text , get_consignee_name text, get_consignee_location text);";

    /*HEATING*/
    public static final String USER_NAME_HEAT="userName";
    public static  final String EQUIP_NO = "equip_no";
    public static  final String START_DATE = "start_date";
    public static  final String START_TIME = "start_time";
    public static  final String END_DATE = "end_date";
    public static  final String END_TIME = "end_time";
    public static  final String TEMP = "temp";
    public static  final String HOURLY_RATE = "hour_rate";
    public static  final String MIN_RATE = "min_rate";
    public static  final String TOTAL_HEAT_COST = "heat_cost";
    public static  final String TOTAL_HEAT_PERIOD = "heat_period";

    private static final String DATABASE_HEATING =
            "create table heating_calc (_id integer primary key autoincrement, " +
                    " userName text not null , equip_no text not null , start_date text not null , start_time text not null , end_date text not null , end_time text not null , temp text not null , " +
                    " hour_rate text , min_rate text , heat_cost text , heat_period text );";


    //*LEAK TEST*//
    public static final String USER_NAME_LEAK="user_Name";
    public static  final String  LEAK_TEST_ID= "leak_test_id";
    public static  final String  EQUIP_NUMBER= "equip_num";
    public static  final String  TEST_DATE= "test_date";
    public static  final String  SHELL_TEST= "shell_test";
    public static  final String  STEAM_TUBE_TEST= "steam_test";
    public static  final String  TYPE_LEAK= "type";
    public static  final String  CURRENT_STATUS= "current_status";
    public static  final String  CHECKED= "checked";
    public static  final String  INDATE_TIME= "indate_time";
    public static  final String  CUSTOMER_NAME= "customer_name";
    public static  final String  RL_VALUE_1= "rl1";
    public static  final String  RL_VALUE_2= "rl2";
    public static  final String  PG_VALUE_1= "pg1";
    public static  final String  PG_VALUE_2= "pg2";
    public static  final String  REMARK= "remark";

    private static final String DATABASE_CREATE_LEAKTEST =
            "create table leak_test (_id integer primary key autoincrement, " +
                    " user_Name text not null , leak_test_id text , equip_num text not null , test_date text not null ,  shell_test text , steam_test text , type text , " +
                    " current_status text , checked text not null ,  indate_time text not null , customer_name text not null , " +
                    " rl1 text , rl2 text , pg1 text , pg2 text , remark text );";



    //*UPDATE GATEOUT*//
    public static final String USERNAME_GATEOUT = "user_name";
    public static final String EQUIP_NUMBER_OUT = "equip_no";
    public static final String YRD_LOCATION = "location";
    public static final String OUT_DATE = "out_date";
    public static final String OUT_TIME = "out_time";
    public static final String OUT_EIR_NO = "eir_no";
    public static final String OUT_VHL_NO = "vhl_no";
    public static final String OUT_TRANSPORT= "transport";
    public static final String OUT_REMARKS= "remark";
    public static final String OUT_RENTAL= "rental";
    public static final String OUT_REPAIR_ID= "repair_id";
    public static final String OUT_ATTACHMENT= "attach";
    public static final String OUT_MODE= "mode";

    //Added by Anantha krishnan
    public static  final String BOOKING_AUTH_NO = "booking_auth_no";
    public static  final String LESSEE_NAME = "lessee_name";
    public static  final String LESSEE_LOCATION = "lessee_location";


    public static final String DATABASE_UPDATE_GATEOUT =
            "create table gateout (_id integer primary key autoincrement, " +
                    " user_name text not null , equip_no text not null , location text , out_date text not null , out_time text not null , " +
                    " eir_no text , vhl_no text , transport text , remark text , rental text , repair_id text , attach text not null , mode text, booking_auth_no text ,lessee_name text , Lessee_location );";


    private final Context context;
    private DatabaseHelper DBHelper;
    private SQLiteDatabase db;
    private static final String SAMPLE_DB_NAME = "TrekBook";
    private SQLiteOpenHelper dbOpenHelper;
    private android.database.sqlite.SQLiteDatabase SQLiteDatabaseInstance_;


    public DBAdapter(Context ctx)
    {
        this.context = ctx;
        DBHelper = new DatabaseHelper(context);

    }




    /*DATABASE FUNCTIONS*/
    private static class DatabaseHelper extends SQLiteOpenHelper
    {
        DatabaseHelper(Context context)
        {
            super(context, DATABASE_NAME, null, DATABASE_VERSION);

        }

        @Override
        public void onCreate(SQLiteDatabase db)
        {
            try {
                db.execSQL(DATABASE_CREATE_GATEIN);
                db.execSQL(DATABASE_HEATING);
                db.execSQL(DATABASE_CREATE_LEAKTEST);
                db.execSQL(DATABASE_UPDATE_GATEOUT);
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }

        @Override
        public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            Log.w(TAG, "Upgrading database from version " + oldVersion + " to "
                    + newVersion + ", which will destroy all old data");
            db.execSQL("DROP TABLE IF EXISTS create_gatein");
            db.execSQL("DROP TABLE IF EXISTS heating_calc");
            db.execSQL("DROP TABLE IF EXISTS leak_test");
            db.execSQL("DROP TABLE IF EXISTS gateout");
            onCreate(db);
        }
    }


    //---opens the database---//

    public DBAdapter open() throws SQLException
    {
        db = DBHelper.getWritableDatabase();
        return this;
    }


    //---closes the database---//

    public void close()
    {
        DBHelper.close();
    }


    //---insert a Gatein into the database---//

    public long insertGateIn(String customer, String customer_id, String gateinId, String mode, String pagename, String rep_est_id, String username, String info_att, String info_changes,
                             String gatein_tran_no, String att_status, String checked, String equipment_no, String type, String type_id, String code, String code_id, String location, String indate, String time
            , String previous_crg, String pre_crg_id, String pre_crg_code, String vehicle_no, String eir_no, String transporter, String remarks, String heat_bit, String rental_bit
            , String manuf_date, String tare_weight, String gross_weight, String capacity, String last_surveyor, String last_testdate, String last_testtype
            , String next_testdate, String next_testtype, String info_remarks, String info_active_bit, String info_rental_bit, String get_consignee_name, String get_consignee_location)
    {

        ContentValues gateInValues = new ContentValues();
        gateInValues.put(CUSTOMER,customer);
        gateInValues.put(CSTMR_ID,customer_id);
        gateInValues.put(GATEIN_ID,gateinId);
        gateInValues.put(MODE,mode);
        gateInValues.put(PAGE_NAME,pagename);
        gateInValues.put(REP_EST_ID,rep_est_id);
        gateInValues.put(USER_NAME,username);
        gateInValues.put(INFO_ATT,info_att);
        gateInValues.put(INFO_CHANGE,info_changes);
        gateInValues.put(GATEIN_TRAN_NO,gatein_tran_no);
        gateInValues.put(ATT_STATUS,att_status);
        gateInValues.put(CHECK,checked);
        gateInValues.put(EQUIPMENT_NO,equipment_no);
        gateInValues.put(TYPE,type);
        gateInValues.put(EQPMNT_TYP_ID,type_id);
        gateInValues.put(CODE,code);
        gateInValues.put(CODE_ID,code_id);
        gateInValues.put(LOCATION,location);
        gateInValues.put(INDATE,indate);
        gateInValues.put(TIME,time);
        gateInValues.put(PREVIOUSCARGO,previous_crg);
        gateInValues.put(PRDCT_ID,pre_crg_id);
        gateInValues.put(PRDCT_CD,pre_crg_code);
        gateInValues.put(VEHICLE_NO,vehicle_no);
        gateInValues.put(EIRNO,eir_no);
        gateInValues.put(TRANSPORTER,transporter);
        gateInValues.put(REMARKS,remarks);
        gateInValues.put(HEATING_BIT,heat_bit);
        gateInValues.put(RENTAL_BIT,rental_bit);
        gateInValues.put(MANUF_DATE,manuf_date);
        gateInValues.put(TARE_WEIGHT,tare_weight);
        gateInValues.put(GROSS_WEIGHT,gross_weight);
        gateInValues.put(CAPACITY,capacity);
        gateInValues.put(LAST_SURVEYOR,last_surveyor);
        gateInValues.put(LAST_TESTDATE,last_testdate);
        gateInValues.put(LAST_TESTTYPE,last_testtype);
        gateInValues.put(NEXT_TESTDATE,next_testdate);
        gateInValues.put(NEXT_TESTTYPE,next_testtype);
        gateInValues.put(INFO_REMARKS,info_remarks);
        gateInValues.put(INFO_ACTIVE_BIT,info_active_bit);
        gateInValues.put(INFO_RENTAL_BIT,info_rental_bit);

        //Added by Anantha krishnan
        gateInValues.put(CONSIGNEE_NAME,get_consignee_name);
        gateInValues.put(CONSIGNEE_LOCATION,get_consignee_location);

        Toast.makeText(this.context, "Stored In Database..!", Toast.LENGTH_SHORT).show();

        return db.insert(DATABASE_TABLE, null, gateInValues);

    }

    private ContentResolver getContentResolver() {
        return null;
    }

    //---insert a Heating Update into the database---//

    public long heatingUpdate(String user_name,String equip_no,String start_date, String start_time , String end_date, String end_time, String temp , String hour_rate
            , String min_rate, String heat_cost , String heat_period){
        ContentValues heatingValues = new ContentValues();

        heatingValues.put(USER_NAME_HEAT,user_name);
        heatingValues.put(EQUIP_NO,equip_no);
        heatingValues.put(START_DATE,start_date);
        heatingValues.put(START_TIME,start_time);
        heatingValues.put(END_DATE,end_date);
        heatingValues.put(END_TIME,end_time);
        heatingValues.put(TEMP,temp);
        heatingValues.put(HOURLY_RATE,hour_rate);
        heatingValues.put(MIN_RATE,min_rate);
        heatingValues.put(TOTAL_HEAT_COST,heat_cost);
        heatingValues.put(TOTAL_HEAT_PERIOD,heat_period);

        Toast.makeText(this.context, "Heating Stored In Database..!", Toast.LENGTH_SHORT).show();
        return db.insert(DATABASE_TABLE_HEAT,null,heatingValues);
    }

    //---insert a Leak Test into the database---//

    public long createLeakTest(String username ,String LeakTestID,String equip_no, String test_date,   String shell_test,  String steam_tube_test, String type, String current_status , String checked,String indate_time
                             ,String cust_name, String rl_value1, String rl_value2, String pg_value1, String pg_value2, String remark){

        ContentValues leaktestValues = new ContentValues();

        leaktestValues.put(USER_NAME_LEAK,username);
        leaktestValues.put(LEAK_TEST_ID,LeakTestID);
        leaktestValues.put(EQUIP_NUMBER,equip_no);
        leaktestValues.put(TEST_DATE,test_date);
        leaktestValues.put(SHELL_TEST,shell_test);
        leaktestValues.put(STEAM_TUBE_TEST,steam_tube_test);
        leaktestValues.put(TYPE_LEAK,type);
        leaktestValues.put(CURRENT_STATUS,current_status);
        leaktestValues.put(CHECKED,checked);
        leaktestValues.put(INDATE_TIME,indate_time);
        leaktestValues.put(CUSTOMER_NAME,cust_name);
        leaktestValues.put(RL_VALUE_1,rl_value1);
        leaktestValues.put(RL_VALUE_2,rl_value2);
        leaktestValues.put(PG_VALUE_1,pg_value1);
        leaktestValues.put(PG_VALUE_2,pg_value2);
        leaktestValues.put(REMARK,remark);

        Toast.makeText(this.context, "Leak Test Stored In Database..!", Toast.LENGTH_SHORT).show();
        return db.insert(DATABASE_TABLE_LEAK_TEST,null,leaktestValues);
    }

        public long updateGateOut(String username , String equip_no , String location , String out_date , String out_time , String eir_no , String vhl_no ,
                                  String transport , String remark , String rental , String repair_id , String attach , String mode, String booking_auth_no, String lessee_name, String lessee_location){
            ContentValues gateoutValues = new ContentValues();

            gateoutValues.put(USERNAME_GATEOUT,username);
            gateoutValues.put(EQUIP_NUMBER_OUT,equip_no);
            gateoutValues.put(YRD_LOCATION,location);
            gateoutValues.put(OUT_DATE,out_date);
            gateoutValues.put(OUT_TIME,out_time);
            gateoutValues.put(OUT_EIR_NO,eir_no);
            gateoutValues.put(OUT_VHL_NO,vhl_no);
            gateoutValues.put(OUT_TRANSPORT,transport);
            gateoutValues.put(OUT_REMARKS,remark);
            gateoutValues.put(OUT_RENTAL,rental);
            gateoutValues.put(OUT_REPAIR_ID,repair_id);
            gateoutValues.put(OUT_ATTACHMENT,attach);
            gateoutValues.put(OUT_MODE,mode);

            //Added by Anantha krishnan
            gateoutValues.put(BOOKING_AUTH_NO,booking_auth_no);
            gateoutValues.put(LESSEE_NAME,lessee_name);
            gateoutValues.put(LESSEE_LOCATION,lessee_location);


            Toast.makeText(this.context, "GateOut Stored In Database..!", Toast.LENGTH_SHORT).show();
            return db.insert(DATABASE_TABLE_UPDATE_GATEOUT,null,gateoutValues);
        }


        /*GET METHODS*/

    public Cursor getCreateGateIN()
    {
        return db.query(DATABASE_TABLE, new String[] {CUSTOMER,CSTMR_ID,GATEIN_ID,MODE,PAGE_NAME,REP_EST_ID,USER_NAME,INFO_ATT,INFO_CHANGE,GATEIN_TRAN_NO,
                        ATT_STATUS,CHECK,EQUIPMENT_NO,TYPE,EQPMNT_TYP_ID,CODE,CODE_ID,LOCATION,INDATE,TIME,PREVIOUSCARGO,PRDCT_ID,PRDCT_CD,VEHICLE_NO,EIRNO,TRANSPORTER
                        ,REMARKS,HEATING_BIT,RENTAL_BIT,MANUF_DATE,TARE_WEIGHT,GROSS_WEIGHT,CAPACITY,LAST_SURVEYOR,LAST_TESTDATE,LAST_TESTTYPE,NEXT_TESTDATE,
                        NEXT_TESTTYPE,INFO_REMARKS,INFO_ACTIVE_BIT,INFO_RENTAL_BIT,CONSIGNEE_NAME,CONSIGNEE_LOCATION},
                null, null, null, null,null);
    }

    public Cursor getHeating(){

        return db.query(DATABASE_TABLE_HEAT,new String[]{USER_NAME_HEAT,EQUIP_NO,START_DATE,START_TIME,END_DATE,END_TIME,TEMP,
                HOURLY_RATE,MIN_RATE,TOTAL_HEAT_COST,TOTAL_HEAT_PERIOD},null,null,null,null,null);


    }

    public Cursor getLeakTest(){
        return db.query(DATABASE_TABLE_LEAK_TEST,new String[]{USER_NAME_LEAK,LEAK_TEST_ID,EQUIP_NUMBER,TEST_DATE,SHELL_TEST,STEAM_TUBE_TEST,
                TYPE_LEAK,CURRENT_STATUS,CHECKED,INDATE_TIME,CUSTOMER_NAME,RL_VALUE_1,RL_VALUE_2,PG_VALUE_1,PG_VALUE_2,REMARK},null,null,null,null,null);
    }

    public Cursor updateGateOut(){
        return db.query(DATABASE_TABLE_UPDATE_GATEOUT,new String[]{USERNAME_GATEOUT,EQUIP_NUMBER_OUT,YRD_LOCATION,OUT_DATE,OUT_TIME,OUT_EIR_NO,
                OUT_VHL_NO,OUT_TRANSPORT,OUT_REMARKS,OUT_RENTAL,OUT_REPAIR_ID,OUT_ATTACHMENT,OUT_MODE,BOOKING_AUTH_NO,LESSEE_NAME,LESSEE_LOCATION},null,null,null,null,null);
    }

    /*Calculating Counts*/
    public int getGateInCount() {
        String countQuery = "SELECT  * FROM " + DATABASE_TABLE;
        SQLiteDatabase db = DBHelper.getReadableDatabase();
        Cursor cursor = db.rawQuery(countQuery, null);
        int cnt = cursor.getCount();
        cursor.close();
        return cnt;

    }

    public int getHeatingCount() {

        String heatConut = "SELECT * FROM "+ DATABASE_TABLE_HEAT;
        SQLiteDatabase db = DBHelper.getReadableDatabase();
        Cursor cursor1 = db.rawQuery(heatConut,null);
        int r_cnt = cursor1.getCount();
        cursor1.close();
        return r_cnt;

    }

    public int getLeakTestCount(){
        String leakConut = "SELECT * FROM "+ DATABASE_TABLE_LEAK_TEST;
        SQLiteDatabase db = DBHelper.getReadableDatabase();
        Cursor cursor2 = db.rawQuery(leakConut,null);
        int l_cnt = cursor2.getCount();
        cursor2.close();
        return l_cnt;
    }

    public int getGateOutCount(){
        String leakConut = "SELECT * FROM "+ DATABASE_TABLE_UPDATE_GATEOUT;
        SQLiteDatabase db = DBHelper.getReadableDatabase();
        Cursor cursor3 = db.rawQuery(leakConut,null);
        int g_cnt = cursor3.getCount();
        cursor3.close();
        return g_cnt;
    }

    public boolean deleteContact( long rowID)
    {
        Toast.makeText(this.context, "Deleted Succesfully..!", Toast.LENGTH_SHORT).show();
        return db.delete(DATABASE_TABLE, KEY_ROWID + "=" + rowID, null) > 0;
    }

    public void delete(String id) {
        db.execSQL("delete from "+ DATABASE_TABLE +" where Google='"+id+"'");
    }

    //---retrieves a particular contact---
    public Cursor getContact(long rowId) throws SQLException
    {
        Cursor mCursor =db.query(true, DATABASE_TABLE, new String[] {KEY_ROWID,
                        KEY_NAME, KEY_EMAIL}, KEY_ROWID + "=" + rowId, null,
                null, null, null, null);
        if (mCursor != null) {
            mCursor.moveToFirst();
        }
        return mCursor;
    }


    //---updates a contact---
    public boolean updateContact(long rowId, String name, String email)
    {
        ContentValues args = new ContentValues();
        args.put(KEY_NAME, name);
        args.put(KEY_EMAIL, email);
        return db.update(DATABASE_TABLE, args, KEY_ROWID + "=" + rowId, null) > 0;
    }


}
