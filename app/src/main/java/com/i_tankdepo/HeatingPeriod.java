package com.i_tankdepo;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.app.TimePickerDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.media.Image;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.text.Html;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.TimePicker;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.HeatingBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.SQLite.DBAdapter;
import com.i_tankdepo.helper.ServiceHandler;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpParams;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.w3c.dom.Text;

import java.io.IOException;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;


/**
 * Created by Metaplore on 10/18/2016.
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
public class HeatingPeriod extends CommonActivity  {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu,im_startDate,im_endDate,im_startTime,im_endTime,iv_back,add_new_heating,im_okay,im_cancel;
    private ListView heating_list_view;

    private TextView tv_totalCost,tv_heat_refresh,text1,text2,text3,
            df_startDate,df_endDate,df_temp,df_total_period,text4,tv_toolbarTitle,tv_startdate,tv_startTime,tv_endDate,tv_temp,tv_heating,tv_endTime;
    LinearLayout LL_Add_New,LL_heat_submit;
    private ProgressDialog progressDialog;
    private ArrayList<HeatingBean> heating_arraylist=new ArrayList<>();
    private HeatingBean heating_bean;
    /*private ViewHolder holder;
    private UserListAdapter adapter;*/
    private ListView listview,heat_list;
    String curTime,systemDate,get_HourlyRate,get_MinRate,get_Totalheat_Cost,
            heat_startDate,heat_endDate,heat_startTime,heat_temp,heat_endTime,get_Totalheat_Period ,Equip_NO,Cust_Name,Equip_Type,In_date,Cargo,Equipment_No,Total_Peroid,Min_Rate,Hourly_Rate,Min_Htng_Prd,Total_Cost,get_startDate,get_startTime,get_endDate,get_htngPeriod,get_endTime,get_temp;
    private Intent mServiceIntent;
    private EditText ed_startDate,ed_endTime,ed_startTime,ed_endDate,ed_temp,ed_totalPrd,ed_minRate,ed_hourlyRate;
    static final int DATE_DIALOG_ID = 1999;
    private Calendar c;
    private int year,month,day,second;
    private boolean  endDate = false;
    TimePickerDialog timePickerDialog;
    int mHour,mMinute;
    private Calendar mcurrentTime;
    private int hour,minute;
    private TimePickerDialog mTimePicker;
    private String total_htng_period,total_htng_rate;
    private Button heat_refresh,heat_home,heat_submit,bt_heating,cleaning,inspection,leakTest,bt_gateout;
    private String responseStatus;
    private RelativeLayout RL_heating,RL_Repair;
    private ImageView iv_changeOfStatus;
    private DBAdapter db;
    private Date date1;
    private String temperature;
    private String surName;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.heating_period);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

        db = new DBAdapter(HeatingPeriod.this);
        
        menu=(ImageView)findViewById(R.id.iv_menu) ;
        iv_back = (ImageView)findViewById(R.id.iv_back);
        add_new_heating = (ImageView)findViewById(R.id.im_add);
        menu.setVisibility(View.GONE);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Heating Update");


        LL_Add_New=(LinearLayout)findViewById(R.id.LL_add_new_heating);

//        LL_Add_New.setVisibility(View.GONE);
        heat_home = (Button)findViewById(R.id.heat_home);
        heat_refresh = (Button)findViewById(R.id.heat_refresh);
        bt_heating = (Button)findViewById(R.id.heating);
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        leakTest = (Button)findViewById(R.id.leakTest);
        inspection.setVisibility(View.GONE);
        leakTest.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);
        heat_submit = (Button)findViewById(R.id.heat_submit);
        LL_heat_submit = (LinearLayout)findViewById(R.id.LL_heat_submit);
        tv_heat_refresh = (TextView)findViewById(R.id.tv_heat_refresh);
        tv_heat_refresh.setText("Reset");
        bt_gateout = (Button)findViewById(R.id.bt_gateout);
        bt_gateout.setVisibility(View.GONE);

        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_Repair.setVisibility(View.GONE);

        LL_heat_submit.setOnClickListener(this);
        ed_totalPrd = (EditText)findViewById(R.id.ed_totalPrd);
        ed_minRate = (EditText)findViewById(R.id.ed_minRate);
        ed_hourlyRate = (EditText)findViewById(R.id.ed_hourlyRate);
        tv_totalCost = (TextView) findViewById(R.id.tv_totalCost);
        ed_startDate = (EditText)findViewById(R.id.ed_startDate);
        ed_endTime = (EditText)findViewById(R.id.ed_endTime);
        ed_startTime = (EditText)findViewById(R.id.ed_startTime);
        ed_endDate = (EditText)findViewById(R.id.ed_endDate);
        ed_temp = (EditText)findViewById(R.id.ed_temp);
        im_startDate = (ImageView)findViewById(R.id.im_startDate);
        im_endDate = (ImageView)findViewById(R.id.im_endDate);
        im_startTime = (ImageView)findViewById(R.id.im_startTime);
        im_endTime = (ImageView)findViewById(R.id.im_endTime);
        im_okay = (ImageView)findViewById(R.id.im_okay);
        im_cancel = (ImageView)findViewById(R.id.im_cancel);
        text1 = (TextView)findViewById(R.id.text1);
        text2 = (TextView)findViewById(R.id.text2);
        text3 = (TextView)findViewById(R.id.text3);
        text4 = (TextView)findViewById(R.id.text4);
        tv_startdate = (TextView)findViewById(R.id.tv_startDate);
        tv_startTime = (TextView)findViewById(R.id.tv_startTime);
        tv_endDate = (TextView)findViewById(R.id.tv_endDate);
        tv_endTime = (TextView)findViewById(R.id.tv_endTime);
        tv_heating = (TextView)findViewById(R.id.tv_heating);
        tv_heating.setText("Heating");
        tv_heating.setGravity(Gravity.CENTER_HORIZONTAL);
        tv_temp = (TextView)findViewById(R.id.tv_temp);
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);

        df_startDate = (TextView)findViewById(R.id.startDate);
        df_endDate = (TextView)findViewById(R.id.endDate);
        df_temp = (TextView)findViewById(R.id.temp);
        df_total_period = (TextView)findViewById(R.id.total_period);

        Cust_Name = GlobalConstants.customer_name;
        Equip_NO = GlobalConstants.equipment_no;
        Equip_Type = GlobalConstants.type;
        In_date = GlobalConstants.date;
        Log.i("date1", String.valueOf(In_date));

        try {
            date1 = new SimpleDateFormat("dd-MMM-yyyy").parse(In_date);
            Log.i("date1", String.valueOf(date1));
        }catch (Exception e)
        {

        }

        Cargo = GlobalConstants.previous_cargo;
        Total_Peroid = GlobalConstants.ttl_htngPrd;
        Min_Rate = GlobalConstants.min_htng_rate;
        Hourly_Rate = GlobalConstants.hourly_charge;
        Total_Cost = GlobalConstants.ttl_RT_NC;
        Min_Htng_Prd = GlobalConstants.min_htngPrd;
        Equipment_No = GlobalConstants.equipment_no;
        heat_startDate = GlobalConstants.htng_startDate;
        heat_endDate = GlobalConstants.htng_EndDate;
        heat_startTime = GlobalConstants.htng_startTime;
        heat_endTime = GlobalConstants.htng_EndTime;
        heat_temp = GlobalConstants.htng_temp;

        Log.i("heat_startDate",heat_startDate);
        Log.i("heat_endDate",heat_endDate);

        if((Min_Rate==null || Min_Rate.equals("") ||Min_Rate=="null") ||
                (Hourly_Rate==null || Hourly_Rate.equals("") ||Hourly_Rate=="null") ||
                (Min_Htng_Prd==null || Min_Htng_Prd.equals("") ||Min_Htng_Prd=="null")||
                (Total_Peroid==null|| Total_Peroid.equals("") || Total_Peroid=="null"))
        {
            Min_Rate="0.00";
            Hourly_Rate="0.00";
            Min_Htng_Prd="0.00";
            Total_Peroid="0.00";
        }
        SimpleDateFormat fromUser = new SimpleDateFormat("dd-MM-yyyy", Locale.ENGLISH);
        SimpleDateFormat myFormat = new SimpleDateFormat("dd-MMM-yyyy",Locale.ENGLISH);
        String datePattern = "\\d{2}-\\d{2}-\\d{4}";
        try {
            if (heat_startDate.equals(null) || heat_startDate.length() < 0) {
                heat_startDate = "";
            } else {
                Boolean is_app_Date1 = heat_startDate.matches(datePattern);
                if (is_app_Date1 == true) {
                    heat_startDate = myFormat.format(fromUser.parse(heat_startDate));

                }

            }
        }catch (Exception e)
        {
            heat_startDate = "";
        }
        try {
            if (heat_endDate.equals(null) || heat_endDate.length() < 0) {
                heat_endDate = "";
            } else {
                Boolean is_app_Date1 = heat_endDate.matches(datePattern);
                if (is_app_Date1 == true) {
                    heat_endDate = myFormat.format(fromUser.parse(heat_endDate));

                }

            }
        }catch (Exception e)
        {
            heat_endDate = "";
        }
        ed_startDate.setText(heat_startDate);
        ed_startTime.setText(heat_startTime);
        ed_endDate.setText(heat_endDate);
        ed_endTime.setText(heat_endTime);
        get_HourlyRate = ed_hourlyRate.getText().toString();
        get_MinRate = ed_minRate.getText().toString();
/*
        add_new_heating.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                ed_temp.requestFocus();
                LL_Add_New.setVisibility(View.VISIBLE);
                ed_startDate.setText(systemDate);
                ed_startTime.setText(curTime);
                ed_endDate.setText(systemDate);
                ed_endTime.setText(curTime);
                get_HourlyRate = ed_hourlyRate.getText().toString();
                get_MinRate = ed_minRate.getText().toString();
            }
        });
*/
        text1.setText(Cust_Name);
        text2.setText(Equip_NO +" , "+Equip_Type);
        text3.setText(In_date);
        text4.setText(Cargo);
        ed_totalPrd.setText(Total_Peroid);
        ed_totalPrd.getEditableText();
        ed_minRate.setText(Min_Rate);
        ed_hourlyRate.setText(Hourly_Rate);
        if((heat_startDate==null||heat_startDate=="null"|| heat_startDate.trim().equals("")) ||
                (heat_startTime==null||heat_startTime=="null"|| heat_startTime.trim().equals("")) )
        {
            df_startDate.setText("Start Date, Start Time ");
            df_endDate.setText("End Date, End Time");
        }else{
            String[] start_date = heat_startDate.split(" ");
            String[] end_date = heat_endDate.split(" ");
            String part1_startdate = start_date[0];
            String part1_enddate = end_date[0];
            df_startDate.setText(part1_startdate+","+heat_startTime );
            df_endDate.setText(part1_enddate+","+ heat_endTime);
        }

        if (cd.isConnectingToInternet()) {

            new TemperatureType().execute();

        } else {
            shortToast(getApplicationContext(), "Please check your Internet Connection.");
        }



        ed_temp.setText(heat_temp);
        df_total_period.setText(Total_Peroid);

        if(Total_Cost == null || Total_Cost.equals("")|| Total_Cost=="null"){
            tv_totalCost.setText("0.00");
        }else{
            tv_totalCost.setText(Total_Cost);
        }
        iv_back.setOnClickListener(this);
        im_cancel.setOnClickListener(this);
        im_okay.setOnClickListener(this);
        ed_startDate.setOnClickListener(this);
        ed_startTime.setOnClickListener(this);
        ed_endDate.setOnClickListener(this);
        ed_endTime.setOnClickListener(this);
        im_startDate.setOnClickListener(this);
        im_startTime.setOnClickListener(this);
        im_endDate.setOnClickListener(this);
        im_endTime.setOnClickListener(this);
        heat_home.setOnClickListener(this);
        heat_refresh.setOnClickListener(this);
        heat_submit.setOnClickListener(this);
        bt_heating.setOnClickListener(this);

        c = Calendar.getInstance();
        year = c.get(Calendar.YEAR);
        month = c.get(Calendar.MONTH);
        day = c.get(Calendar.DAY_OF_MONTH);
        second = c.get(Calendar.SECOND);
        int minute = c.get(Calendar.MINUTE);
        //12 hour format
        int hour = c.get(Calendar.HOUR);
        //24 hour format
        int hourofday = c.get(Calendar.HOUR_OF_DAY);
        SimpleDateFormat time = new SimpleDateFormat("HH:MM");
        curTime = time.format(new Date());
        systemDate = new SimpleDateFormat("dd-MMM-yyyy").format(new Date());



        
        String startDate = getColoredSpanned("Start Date","#bbbbbb");
        String startTime = getColoredSpanned("Start Time","#bbbbbb");
        String endDate = getColoredSpanned("End Date","#bbbbbb");
        String endTime = getColoredSpanned("End Time","#bbbbbb");
        String temp = getColoredSpanned("Temp","#bbbbbb");
        String degree = getColoredSpanned("(°C)","#bbbbbb");

         surName = getColoredSpanned("*","#cb0da5");

        tv_startdate.setText(Html.fromHtml(startDate+" "+surName));
        tv_startTime.setText(Html.fromHtml(startTime+" "+surName));
        tv_endDate.setText(Html.fromHtml(endDate+" "+surName));
        tv_endTime.setText(Html.fromHtml(endTime+" "+surName));
//        tv_temp.setText(Html.fromHtml(temp+ degree +" "+surName));





        drawer = (DrawerLayout) findViewById(R.id.drawer_layout);



        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();

       menu.setOnClickListener(this);
        /*NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        menu.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(drawer.isDrawerOpen(Gravity.START))
                    drawer.closeDrawer(Gravity.END);
                else
                    drawer.openDrawer(Gravity.START);
            }
        });

        navigationView.setNavigationItemSelectedListener(this);*/
    }



    @Override
    public void onClick(View view) {
        switch (view.getId())
        {
            case R.id.iv_changeOfStatus:
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));
                break;
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(),MainActivity.class));
                break;
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.iv_back:
                finish();
                onBackPressed();
                break;
            case R.id.ed_startDate:

                endDate = false;

                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.ed_endDate:

                endDate = true;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_startDate:

                endDate = false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_endDate:

                endDate = true;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.ed_startTime:

                 mcurrentTime = Calendar.getInstance();
                 hour = mcurrentTime.get(Calendar.HOUR_OF_DAY);
                 minute = mcurrentTime.get(Calendar.MINUTE);

                mTimePicker = new TimePickerDialog(HeatingPeriod.this, new TimePickerDialog.OnTimeSetListener() {
                    @Override
                    public void onTimeSet(TimePicker timePicker, int selectedHour, int selectedMinute) {
                        ed_startTime.setText(String.format("%02d:%02d",selectedHour,selectedMinute));
                        get_startDate = ed_startDate.getText().toString();
                        get_startTime = ed_startTime.getText().toString();
                        get_endDate = ed_endDate.getText().toString();
                        get_endTime = ed_endTime.getText().toString();

                        if((get_startDate.equals("") || get_startDate == null) ||
                                (get_startTime.equals("") || get_startTime == null )||
                                (get_endDate.equals("") || get_endDate == null) ||
                                (get_endTime.equals("")|| get_endTime == null) ){
//                            shortToast(getApplicationContext(),"Please select the Mandatory Fields.");
                        }else{
                            if (cd.isConnectingToInternet()){
                                SimpleDateFormat parser = new SimpleDateFormat("HH:mm");


                                try {
                                    Date start = parser.parse(get_startTime);
                                    Date end = parser.parse(get_endTime);
                                    if(get_startDate.compareTo(get_endDate)==0)
                                    {
                                        if (end.before(start)) {
                                            shortToast(getApplicationContext(),"cannot select past time");
                                        }else
                                        {
                                            new Post_Calc_Heating_period().execute();

                                        }
                                    }else
                                    {
                                        new Post_Calc_Heating_period().execute();

                                    }

                                } catch (ParseException e) {

                                    // Invalid date was entered

                                }


                            }else{
                                shortToast(getApplicationContext(),"Please Check Your Internet Connection");
                            }
                        }

                    }
                }, hour, minute,true
                );
                mTimePicker.show();
                break;
            case R.id.ed_endTime:

                 mcurrentTime = Calendar.getInstance();
                 hour = mcurrentTime.get(Calendar.HOUR_OF_DAY);
                 minute = mcurrentTime.get(Calendar.MINUTE);
                mTimePicker = new TimePickerDialog(HeatingPeriod.this, new TimePickerDialog.OnTimeSetListener() {
                    @Override
                    public void onTimeSet(TimePicker timePicker, int selectedHour, int selectedMinute) {
                        ed_endTime.setText(String.format("%02d:%02d",selectedHour,selectedMinute));


                        get_startDate = ed_startDate.getText().toString();
                        get_startTime = ed_startTime.getText().toString();
                        get_endDate = ed_endDate.getText().toString();
                        get_endTime = ed_endTime.getText().toString();

                        if((get_startDate.equals("") || get_startDate == null) ||
                                (get_startTime.equals("") || get_startTime == null ) ||
                                (get_endDate.equals("") || get_endDate == null) ||
                                (get_endTime.equals("")|| get_endTime == null) ){
//                            shortToast(getApplicationContext(),"Please select the Mandatory Fields.");
                        }else{
                            if (cd.isConnectingToInternet()){
                                SimpleDateFormat parser = new SimpleDateFormat("HH:mm");


                                try {
                                    Date start = parser.parse(get_startTime);
                                    Date end = parser.parse(get_endTime);
                                    if(get_startDate.compareTo(get_endDate)==0)
                                    {
                                        if (end.before(start)) {
                                            shortToast(getApplicationContext(),"cannot select past time");
                                        }else
                                        {
                                            new Post_Calc_Heating_period().execute();

                                        }
                                    }else
                                    {
                                        new Post_Calc_Heating_period().execute();

                                    }

                                } catch (ParseException e) {

                                    // Invalid date was entered

                                }


                            }else{
                                shortToast(getApplicationContext(),"Please Check Your Internet Connection");
                            }
                        }
                    }
                }, hour, minute,true
                );

                mTimePicker.show();
                break;

            case R.id.im_startTime:

                 mcurrentTime = Calendar.getInstance();
                 hour = mcurrentTime.get(Calendar.HOUR_OF_DAY);
                 minute = mcurrentTime.get(Calendar.MINUTE);

                mTimePicker = new TimePickerDialog(HeatingPeriod.this, new TimePickerDialog.OnTimeSetListener() {
                    @Override
                    public void onTimeSet(TimePicker timePicker, int selectedHour, int selectedMinute) {
                        ed_startTime.setText(String.format("%02d:%02d",selectedHour,selectedMinute));

                        get_startDate = ed_startDate.getText().toString();
                        get_startTime = ed_startTime.getText().toString();
                        get_endDate = ed_endDate.getText().toString();
                        get_endTime = ed_endTime.getText().toString();

                        if((get_startDate.equals("") || get_startDate == null) ||
                                (get_startTime.equals("") || get_startTime == null )||
                                (get_endDate.equals("") || get_endDate == null) ||
                                (get_endTime.equals("")|| get_endTime == null) ){
//                            shortToast(getApplicationContext(),"Please select the Mandatory Fields.");
                        }else{
                            if (cd.isConnectingToInternet()){
                                SimpleDateFormat parser = new SimpleDateFormat("HH:mm");


                                try {
                                    Date start = parser.parse(get_startTime);
                                    Date end = parser.parse(get_endTime);
                                    if(get_startDate.compareTo(get_endDate)==0)
                                    {
                                        if (end.before(start)) {
                                            shortToast(getApplicationContext(),"cannot select past time");
                                        }else
                                        {
                                            new Post_Calc_Heating_period().execute();

                                        }
                                    }else
                                    {
                                        new Post_Calc_Heating_period().execute();

                                    }

                                } catch (ParseException e) {

                                    // Invalid date was entered

                                }


                            }else{
                                shortToast(getApplicationContext(),"Please Check Your Internet Connection");
                            }
                        }
                    }
                }, hour, minute,true
                );

                mTimePicker.show();
                break;
            case R.id.im_endTime:
                 mcurrentTime = Calendar.getInstance();
                 hour = mcurrentTime.get(Calendar.HOUR_OF_DAY);
                 minute = mcurrentTime.get(Calendar.MINUTE);

                mTimePicker = new TimePickerDialog(HeatingPeriod.this, new TimePickerDialog.OnTimeSetListener() {
                    @Override
                    public void onTimeSet(TimePicker timePicker, int selectedHour, int selectedMinute) {
//                        ed_endTime.setText(selectedHour + ":" + selectedMinute);
                        ed_endTime.setText(String.format("%02d:%02d",selectedHour,selectedMinute));

                        get_startDate = ed_startDate.getText().toString();
                        get_startTime = ed_startTime.getText().toString();
                        get_endDate = ed_endDate.getText().toString();
                        get_endTime = ed_endTime.getText().toString();

                        if((get_startDate.equals("") || get_startDate == null) ||
                                (get_startTime.equals("") || get_startTime == null )||
                                (get_endDate.equals("") || get_endDate == null) ||
                                (get_endTime.equals("")|| get_endTime == null) ){
//                            shortToast(getApplicationContext(),"Please select the Mandatory Fields.");
                        }else{
                            if (cd.isConnectingToInternet()){
                                SimpleDateFormat parser = new SimpleDateFormat("HH:mm");


                                try {
                                    Date start = parser.parse(get_startTime);
                                    Date end = parser.parse(get_endTime);
                                    if(get_startDate.compareTo(get_endDate)==0)
                                    {
                                        if (end.before(start)) {
                                            shortToast(getApplicationContext(),"Heating End Time Should be greater than Heating Start Time");
                                        }else
                                        {
                                            new Post_Calc_Heating_period().execute();

                                        }
                                    }else
                                    {
                                        new Post_Calc_Heating_period().execute();

                                    }

                                } catch (ParseException e) {

                                    // Invalid date was entered

                                }


                            }else{
                                shortToast(getApplicationContext(),"Please Check Your Internet Connection");
                            }
                        }


                    }
                }, hour, minute,true
                );
                mTimePicker.show();
                break;

            case R.id.heat_submit:
                get_startDate = ed_startDate.getText().toString();
                get_startTime = ed_startTime.getText().toString();
                get_endDate = ed_endDate.getText().toString();
                get_endTime = ed_endTime.getText().toString();
                get_temp = ed_temp.getText().toString();
                get_HourlyRate = ed_hourlyRate.getText().toString();
                get_MinRate = ed_minRate.getText().toString();
                get_Totalheat_Cost = tv_totalCost.getText().toString();
                get_Totalheat_Period = ed_totalPrd.getText().toString();
                Log.d("Total Cost",get_Totalheat_Cost);
                Log.d("Total Period",get_Totalheat_Period);
                if(cd.isConnectingToInternet()) {
                    if ((get_startDate.equals("") || get_startDate == null) ||
                            (get_startTime.equals("") || get_startTime == null) ||
                            (get_endDate.equals("") || get_endDate == null) ||
                            (get_endTime.equals("") || get_endTime == null) ||
                            (get_temp.equals("") || get_temp == null)) {
                        shortToast(getApplicationContext(), "Please select the Mandatory Fields.");
                    } else {
                        if (cd.isConnectingToInternet()) {

                            if(get_endDate.compareTo(get_startDate)<0) {

                                shortToast(getApplicationContext(), "Heating End Date should be greater then or equal to Heating Start Date");

                            }else{
                                try {
                                    SimpleDateFormat parser = new SimpleDateFormat("HH:mm");

                                    Date start = parser.parse(get_startTime);
                                    Date end = parser.parse(get_endTime);
                                    if (get_startDate.compareTo(get_endDate) == 0) {

                                        if (end.before(start)) {
                                            shortToast(getApplicationContext(), "Heating End Time Should be greater than Heating Start Time");
                                        } else {
                                            new Post_Heating_Update().execute();
                                        }
                                    }else {
                                        new Post_Heating_Update().execute();
                                    }

                                }catch (Exception e)
                                {

                                }
                            }




                        } else {
                            shortToast(getApplicationContext(), "Please check your Internet Connection");
                        }
                    }
                }else{
                    if ((get_startDate.equals("") || get_startDate == null) ||
                            (get_startTime.equals("") || get_startTime == null) ||
                            (get_endDate.equals("") || get_endDate == null) ||
                            (get_endTime.equals("") || get_endTime == null) ||
                            (get_temp.equals("") || get_temp == null)) {
                        shortToast(getApplicationContext(), "Please select the Mandatory Fields.");
                    }else {
                        db.open();

                        db.heatingUpdate(sp.getString(SP_USER_ID, "user_Id"), Equip_NO, get_startDate, get_startTime, get_endDate, get_endTime, get_temp, get_HourlyRate, get_MinRate, get_Totalheat_Cost, get_Totalheat_Period);

                        db.close();
                    }
                }
                break;
            case R.id.heating:
                finish();
                startActivity(new Intent(getApplicationContext(),Heating.class));
                break;
        }

    }

    

    @Override
    protected Dialog onCreateDialog(int id) {
        Calendar c = Calendar.getInstance();
        int cyear = c.get(Calendar.YEAR);
        int cmonth = c.get(Calendar.MONTH);
        int cday = c.get(Calendar.DAY_OF_MONTH);
        switch (id) {
            case DATE_DIALOG_ID:
                //start changes...
                DatePickerDialog dialog = new DatePickerDialog(this, pickerListener, cyear, cmonth, cday);
                dialog.getDatePicker().setMaxDate(new Date().getTime());

                dialog.getDatePicker().setMinDate(date1.getTime());

                return dialog;
            //end changes...
        }
        return null;
    }
    private static String formatDate(int year, int month, int day) {

        Calendar cal = Calendar.getInstance();
        cal.setTimeInMillis(0);
        cal.set(year, month, day);
        Date date = cal.getTime();
        SimpleDateFormat sdf = new SimpleDateFormat("dd-MMM-yyyy");

        return sdf.format(date).toString();
    }

    private DatePickerDialog.OnDateSetListener pickerListener = new DatePickerDialog.OnDateSetListener() {

        // when dialog box is closed, below method will be called.
        @Override
        public void onDateSet(DatePicker view, int selectedYear,
                              int selectedMonth, int selectedDay) {

            year  = selectedYear;
            month = selectedMonth;
            day   = selectedDay;

            view.setMinDate(System.currentTimeMillis() - 1000);

            if(endDate==true)
            {
                ed_endDate.setText(formatDate(year, month, day));
                get_startDate = ed_startDate.getText().toString();
                get_startTime = ed_startTime.getText().toString();
                get_endDate = ed_endDate.getText().toString();
                get_endTime = ed_endTime.getText().toString();

                if((get_startDate.equals("") || get_startDate == null) ||
                        (get_startTime.equals("") || get_startTime == null )||
                        (get_endDate.equals("") || get_endDate == null) ||
                        (get_endTime.equals("")|| get_endTime == null) ){
//                    shortToast(getApplicationContext(),"Please select the Mandatory Fields.");
                }else{
                    if (cd.isConnectingToInternet()){
                        SimpleDateFormat parser = new SimpleDateFormat("HH:mm");


                        try {
                            Date start = parser.parse(get_startTime);
                            Date end = parser.parse(get_endTime);
                            if(get_startDate.compareTo(get_endDate)==0)
                            {
                                if (end.before(start)) {
                                    shortToast(getApplicationContext(),"cannot select past time");
                                }else
                                {
                                    new Post_Calc_Heating_period().execute();

                                }
                            }else
                            {
                                new Post_Calc_Heating_period().execute();

                            }

                        } catch (ParseException e) {

                            // Invalid date was entered

                        }


                    }else{
                        shortToast(getApplicationContext(),"Please Check Your Internet Connection");
                    }
                }

            }else {
                ed_startDate.setText(formatDate(year, month, day));

                get_startDate = ed_startDate.getText().toString();
                get_startTime = ed_startTime.getText().toString();
                get_endDate = ed_endDate.getText().toString();
                get_endTime = ed_endTime.getText().toString();

                if((get_startDate.equals("") || get_startDate == null) ||
                        (get_startTime.equals("") || get_startTime == null )||
                        (get_endDate.equals("") || get_endDate == null) ||
                        (get_endTime.equals("")|| get_endTime == null) ){
//                    shortToast(getApplicationContext(),"Please select the Mandatory Fields.");
                }else{
                    if (cd.isConnectingToInternet()){
                        SimpleDateFormat parser = new SimpleDateFormat("HH:mm");


                        try {
                            Date start = parser.parse(get_startTime);
                            Date end = parser.parse(get_endTime);
                            if(get_startDate.compareTo(get_endDate)==0)
                            {
                                if (end.before(start)) {
                                    shortToast(getApplicationContext(),"cannot select past time");
                                }else
                                {
                                    new Post_Calc_Heating_period().execute();

                                }
                            }else
                            {
                                new Post_Calc_Heating_period().execute();

                            }

                        } catch (ParseException e) {

                            // Invalid date was entered

                        }


                    }else{
                        shortToast(getApplicationContext(),"Please Check Your Internet Connection");
                    }
                }
            }

        }
    };





    @Override
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

    }


    public class Post_Calc_Heating_period extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        JSONObject jsonobject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(HeatingPeriod.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
            //  progressDialog.show();
        }

        @Override
        protected Void doInBackground(Void... params) {

            DefaultHttpClient httpClient = new DefaultHttpClient();
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpClient httpclient = new DefaultHttpClient();

            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCalcHeatingPeriod);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("str_HeatingStartDate", get_startDate);
                jsonObject.put("str_HeatingStartTime",get_startTime);
                jsonObject.put("str_HeatingEndDate", get_endDate);
                jsonObject.put("str_HeatingEndTime", get_endTime);

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("responce", resp);
                Log.d("req_heating_prd", jsonObject.toString());
                JSONObject jsonResp = new JSONObject(resp);

                jsonobject = jsonResp.getJSONObject("d");

                if (jsonobject != null) {

                    total_htng_period=jsonobject.getString("TotalHeatingPeriod");

                }
            } catch (ClientProtocolException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } catch (JSONException e) {
                e.printStackTrace();
            }
            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {
            super.onPostExecute(aVoid);


            if(jsonobject!=null)
            {
                if(total_htng_period == null || total_htng_period.equals("")|| total_htng_period=="null") {
                    ed_totalPrd.setText("0.00");
                    get_htngPeriod = ed_totalPrd.getText().toString();
                    new Post_Calc_Total_Rate().execute();
                }else{
                    ed_totalPrd.setText(total_htng_period);
                    get_htngPeriod = ed_totalPrd.getText().toString();
                    new Post_Calc_Total_Rate().execute();
                }
            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");
            }

            progressDialog.dismiss();

        }
    }

    public class Post_Calc_Total_Rate extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        JSONObject jsonobject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(HeatingPeriod.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
            progressDialog.show();
        }

        @Override
        protected Void doInBackground(Void... params) {

            DefaultHttpClient httpClient = new DefaultHttpClient();
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpClient httpclient = new DefaultHttpClient();

            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCalcTotalRate);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("str_MinRate",get_MinRate);
                jsonObject.put("str_TotaPeriod",get_htngPeriod);
                jsonObject.put("str_MinHeatingPerod", Min_Htng_Prd);
                jsonObject.put("str_HourlyRate", get_HourlyRate);

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("total amount responce", resp);
                Log.d("req", jsonObject.toString());
                JSONObject jsonResp = new JSONObject(resp);

                jsonobject = jsonResp.getJSONObject("d");

                if (jsonobject != null) {

                    total_htng_rate=jsonobject.getString("TotalHeatingRate");

                }
            } catch (ClientProtocolException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } catch (JSONException e) {
                e.printStackTrace();
            }
            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {
            super.onPostExecute(aVoid);


            if(jsonobject!=null)
            {
                tv_totalCost.setText(total_htng_rate);

            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");
            }

            progressDialog.dismiss();

        }
    }


    public class Post_Heating_Update extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        JSONObject jsonobject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(HeatingPeriod.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
            progressDialog.show();
        }

        @Override
        protected Void doInBackground(Void... params) {

            DefaultHttpClient httpClient = new DefaultHttpClient();
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpClient httpclient = new DefaultHttpClient();

            HttpPost httpPost = new HttpPost(ConstantValues.baseURLHeatingUpdate);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("UserName",sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("EquipmentNo",Equip_NO);
                jsonObject.put("StartDate", get_startDate +" "+ get_startTime);
                jsonObject.put("StartTime", get_startTime);
                jsonObject.put("EndDate", get_endDate+" "+ get_endTime);
                jsonObject.put("EndTime", get_endTime);
                jsonObject.put("Temp", get_temp);
                jsonObject.put("TotalPeriod", get_Totalheat_Period);
                jsonObject.put("MinRate", get_MinRate);
                jsonObject.put("HourlyRate", get_HourlyRate);
                jsonObject.put("TotalRate", get_Totalheat_Cost);

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("total amount responce", resp);
                Log.d("req", jsonObject.toString());
                JSONObject jsonResp = new JSONObject(resp);

                jsonobject = jsonResp.getJSONObject("d");

                if (jsonobject != null) {

                    responseStatus =jsonobject.getString("Status");

                }
            } catch (ClientProtocolException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } catch (JSONException e) {
                e.printStackTrace();
            }
            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {
            super.onPostExecute(aVoid);


            if(jsonobject!=null)
            {
                if(responseStatus.equalsIgnoreCase("Heating Updated")){
                    shortToast(getApplicationContext(),responseStatus);
                    finish();
                    startActivity(new Intent(getApplicationContext(),Heating.class));
                }else{
                    shortToast(getApplicationContext(),"Heating Not Updated..!");
                }
            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");
            }

            progressDialog.dismiss();

        }
    }

    public class TemperatureType extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;
        private JSONObject getJsonObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(HeatingPeriod.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
            progressDialog.show();
        }

        @Override
        protected Void doInBackground(Void... params) {

            ServiceHandler sh = new ServiceHandler();
            HttpParams httpParameters = new BasicHttpParams();
            DefaultHttpClient httpClient = new DefaultHttpClient(httpParameters);
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpPost httpPost = new HttpPost(ConstantValues.baseURL_TemperatureType);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("KY_NAM", "075");


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("res_settings", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                 getJsonObject = jsonrootObject.getJSONObject("d");
                temperature = getJsonObject.getString("SettingsDirect");

//                jsonarray = getJsonObject.getJSONArray("SettingsList");


            } catch (JSONException e) {
                e.printStackTrace();
            } catch (ClientProtocolException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }


            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {
            progressDialog.dismiss();

            if(getJsonObject!=null) {


                final String DEGREE  = "\u00b0";


                if(temperature.equals("")||temperature.equals("null")||temperature.equals(null))
                {
                    df_temp.setText(heat_temp+"("+DEGREE+"C)");
                    tv_temp.setText(Html.fromHtml("Temp"+"("+DEGREE+"C) "+surName));
                }else
                {
                    df_temp.setText(heat_temp+"("+DEGREE+temperature+")");
                    tv_temp.setText("Temp"+"("+DEGREE+temperature+")");
                    tv_temp.setText(Html.fromHtml("Temp"+"("+DEGREE+temperature+")"+surName));
                }


            }
            else
            {
            }



        }


    }


}
