package com.i_tankdepo;

import android.content.ContentValues;
import android.content.Context;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

/**
 * Created by Admin on 1/20/2017.
 */

public class DB {


    public static  final String CUSTOMER = "customer";
    public static  final String EQUIPMENT_NO = "equipment_no";
    public static  final String TYPE = "type";
    public static  final String CODE = "code";
    public static  final String STATUS = "status";
    public static  final String LOCATION = "location";
    public static  final String INDATE = "indate";
    public static  final String TIME = "time";
    public static  final String PREVIOUSCARGO = "previous_crg";
    public static  final String EIRNO = "eir_no";
    public static  final String VEHICLE_NO = "vehicle_no";
    public static  final String TRANSPORTER = "transporter";
    public static  final String REMARKS = "remarks";
   /* public static  final String HEATING_BIT = "heat_bit";
    public static  final String RENTAL_BIT = "rental_bit";
    public static  final String ATTACHMENT = "attach_files";
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
    public static  final String INFO_ACTIVE_BIT = "info_active_bit";
    public static  final String INFO_RENTAL_BIT = "info_rental_bit";*/

    private static final String TAG = "DB";
    private static final String DB_NAME = "CREATE_GATEIN.DB";
    public static final String TABLE_NAME = "CREATE_GATEIN";
    private static final int DB_VERSION = 1;

   /* private static final String CREATE_TABLE = "create_table"+TABLE_NAME+"("+CUSTOMER+"TEXT,"+EQUIPMENT_NO+"TEXT,"+TYPE+"TEXT,"+CODE+"TEXT,"
            +STATUS+"TEXT,"+LOCATION+"TEXT,"+INDATE+"TEXT,"+TIME+"TEXT,"+PREVIOUSCARGO+"TEXT,"+EIRNO+"TEXT,"+VEHICLE_NO+"TEXT,"+TRANSPORTER+"TEXT,"
            +REMARKS+"TEXT,"+HEATING_BIT+"TEXT,"+RENTAL_BIT+"TEXT,"+ATTACHMENT+"TEXT,"+MANUF_DATE+"TEXT,"+TARE_WEIGHT+"TEXT,"+GROSS_WEIGHT+"TEXT,"
            +CAPACITY+"TEXT,"+LAST_SURVEYOR+"TEXT,"+LAST_TESTDATE+"TEXT,"+LAST_TESTTYPE+"TEXT,"+NEXT_TESTDATE+"TEXT,"+NEXT_TESTTYPE+"TEXT,"+INFO_REMARKS+"TEXT,"
            +INFO_ACTIVE_BIT+"TEXT,"+INFO_RENTAL_BIT+"TEXT";*/



    private final Context context;
    private DBCreateGateInHelper DBHelper;
    private SQLiteDatabase db;

    public DB(Context context) {
        this.context = context;
        DBHelper = new DBCreateGateInHelper(context);
    }


    public class DBCreateGateInHelper extends SQLiteOpenHelper {

        DBCreateGateInHelper(Context context) {
            super(context, DB_NAME, null, DB_VERSION);
        }

        @Override
        public void onCreate(SQLiteDatabase db) {

            String CREATE_TABLE = "CREATE_TABLE"+TABLE_NAME+"("+ CUSTOMER+" text not null," +EQUIPMENT_NO+ "text not null,"+TYPE+"text not null,"+CODE+"text not null"
                       +STATUS+"text not null,"+LOCATION+"text not null,"+INDATE+"text not null,"+TIME+"text not null,"+PREVIOUSCARGO+"text not null,"
                       +EIRNO+"text not null,"+VEHICLE_NO+"text not null,"+TRANSPORTER+"text not null,"+REMARKS+"text not null,"+");";
             db.execSQL(CREATE_TABLE);

            /* try{
                db.execSQL(CREATE_TABLE);
            }catch (SQLException e){
                e.printStackTrace();
            }*/

        }

        @Override
        public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
            db.execSQL("DROP TABLE IF EXISTS" + TABLE_NAME);
            onCreate(db);
        }

    }


    public DB open() throws SQLException{
        db =  DBHelper.getWritableDatabase();
        return this;
    }

    public void close(){
        DBHelper.close();
    }

    public long insertData(String customer , String equipment_no, String type, String code,String status, String location, String indate, String time
   ,String previous_crg, String vehicle_no , String eir_no,String transporter,String remarks/*, String heat_bit, String rental_bit, String attachment
    ,String manuf_date , String tare_weight, String gross_weight, String capacity, String last_surveyor , String last_testdate, String last_testtype
    ,String next_testdate, String next_testtype , String info_remarks, String info_active_bit, String info_rental_bit*/){



        ContentValues contentValues = new ContentValues();
        contentValues.put(CUSTOMER,customer);
        contentValues.put(EQUIPMENT_NO,equipment_no);
        contentValues.put(TYPE,type);
        contentValues.put(CODE,code);
        contentValues.put(STATUS,status);
        contentValues.put(LOCATION,location);
        contentValues.put(INDATE,indate);
        contentValues.put(TIME,time);
        contentValues.put(PREVIOUSCARGO,previous_crg);
        contentValues.put(VEHICLE_NO,vehicle_no);
        contentValues.put(EIRNO,eir_no);
        contentValues.put(TRANSPORTER,transporter);
        contentValues.put(REMARKS,remarks);
       /* contentValues.put(HEATING_BIT,heat_bit);
        contentValues.put(RENTAL_BIT,rental_bit);
        contentValues.put(ATTACHMENT,attachment);
        contentValues.put(MANUF_DATE,manuf_date);
        contentValues.put(TARE_WEIGHT,tare_weight);
        contentValues.put(GROSS_WEIGHT,gross_weight);
        contentValues.put(CAPACITY,capacity);
        contentValues.put(LAST_SURVEYOR,last_surveyor);
        contentValues.put(LAST_TESTDATE,last_testdate);
        contentValues.put(LAST_TESTTYPE,last_testtype);
        contentValues.put(NEXT_TESTDATE,next_testdate);
        contentValues.put(NEXT_TESTTYPE,next_testtype);
        contentValues.put(INFO_REMARKS,info_remarks);
        contentValues.put(INFO_ACTIVE_BIT,info_active_bit);
        contentValues.put(INFO_RENTAL_BIT,info_rental_bit);*/
        return db.insert(TABLE_NAME,null,contentValues);
        
    }


}