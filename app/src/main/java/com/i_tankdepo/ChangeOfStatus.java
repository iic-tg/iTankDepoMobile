package com.i_tankdepo;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.text.Editable;
import android.text.Html;
import android.text.TextWatcher;
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
import android.widget.CompoundButton;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.ChanfeOfStatusBean;
import com.i_tankdepo.Beanclass.CurrentStatusBean;
import com.i_tankdepo.Beanclass.CustomerDropdownBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.helper.ServiceHandler;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpParams;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collections;
import java.util.Date;
import java.util.List;
import java.util.Locale;

/**
 * Created by Admin on 12/26/2016.
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
public class ChangeOfStatus extends CommonActivity implements NavigationView.OnNavigationItemSelectedListener {
    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu, equip_up, equip_down, im_ok, im_close,im_date,iv_back,im_search;
    private LinearLayout LL_hole,accordian_LL,LL_status_submit,LL_run;
    private TextView tv_status_no,tv_toolbarTitle,list_noData;
    private Button status_home,status_refresh,status_submit;
    private Spinner sp_current_status,sp_status_customer,sp_to_status;
    private EditText ed_equip_no,ed_date, ed_time,ed_remarks,ed_to_statusDate,searchView2;
    private Intent mServiceIntent;
    private CurrentStatusBean currentStatusBean;
    private ArrayList<CurrentStatusBean> currentStatusBeanArrayList;
    private String StatusId,StatusName,ToStatusName,ToStatusID;
    private int year,month,day,second;
    ArrayList<String> dropdown_currentStatus_list = new ArrayList<>();
    ArrayList<String> dropdown_toStatus_list = new ArrayList<>();
    List<String> Status_ID = new ArrayList<>();
    List<String> Status_Name = new ArrayList<>();
    private ArrayList<String> worldlist;
    ArrayList<String[]> dropdown_customer_list = new ArrayList<>();
    List<String> Cust_name = new ArrayList<>();
    List<String> Cust_code = new ArrayList<>();
    private String CustomerName,CustomerCode;
    private CustomerDropdownBean customer_DropdownBean;
    ArrayList<CustomerDropdownBean> CustomerDropdownArrayList = new ArrayList<>();
    ArrayList<CustomerDropdownBean> ToStatusDropdownArrayList = new ArrayList<>();
    private String getCurrentStatus,getCustomer,getdate,getremark;
    ProgressDialog progressDialog;
    private ArrayList<ChanfeOfStatusBean> Change_Status_arraylist = new ArrayList<>();
    private ChanfeOfStatusBean change_Status_bean;
    private ViewHolder holder;
    private UserListAdapter adapter;
    private ListView status_list_view;
    private String getEquipmentNo,getCustomerID,getCurrentStatusID,getCurrentStatusName,get_sp_to_status,getTostatusName,getTostatusID;
    private TextView tv_name,tv_equip_no,tv_date,tv_status;
    private ArrayList<COS_Product> box;
    private ArrayList<COS_Product> products;
    private ListAdapter boxAdapter;
    private ArrayList<String> selected_name;
    private ArrayList<String[]> selected_list;
    static final int DATE_DIALOG_ID = 1999;
    private Calendar c;
    private String systemDate,curTime,getTime;
    List<String> To_Status_name = new ArrayList<>();
    List<String> To_Status_Code = new ArrayList<>();
    private String returnstatus;
    private ImageView iv_changeOfStatus;
    private String ToStatus;
    private String Letter,Number;
    int count=0;
    int count_status=0;
    private EditText ed_status_cust,ed_current_status;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.change_of_status);

        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        c = Calendar.getInstance();
        year = c.get(Calendar.YEAR);
        month = c.get(Calendar.MONTH);
        day = c.get(Calendar.DAY_OF_MONTH);
        second = c.get(Calendar.SECOND);
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        im_date = (ImageView) findViewById(R.id.im_date);
        im_date.setOnClickListener(this);
        iv_back.setVisibility(View.GONE);
        equip_down = (ImageView) findViewById(R.id.equip_down);
        equip_up = (ImageView) findViewById(R.id.equip_up);
        LL_hole = (LinearLayout)findViewById(R.id.LL_hole);
        accordian_LL = (LinearLayout)findViewById(R.id.accordian_LL);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_status_no = (TextView) findViewById(R.id.tv_status_no);
        tv_status_no.setText(GlobalConstants.status_id);
        tv_toolbarTitle.setText("Change of Status");
        status_list_view=(ListView)findViewById(R.id.status_list_view);


        LL_status_submit = (LinearLayout)findViewById(R.id.LL_status_submit);
        LL_run = (LinearLayout)findViewById(R.id.LL_run);
        LL_run.setVisibility(View.GONE);

        sp_current_status = (Spinner)findViewById(R.id.sp_current_status);

        sp_status_customer = (Spinner)findViewById(R.id.sp_status_customer);
        ed_status_cust = (EditText)findViewById(R.id.ed_status_cust);
        ed_current_status = (EditText)findViewById(R.id.ed_current_status);
        ed_current_status.setText(GlobalConstants.status);
        ed_current_status.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sp_current_status.performClick();
            }
        });
        ed_status_cust.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sp_status_customer.performClick();
            }
        });
        sp_to_status = (Spinner)findViewById(R.id.sp_to_status);

        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setVisibility(View.GONE);
        list_noData = (TextView)findViewById(R.id.list_noData);
        list_noData.setVisibility(View.GONE);

        equip_up.setVisibility(View.GONE);
        LL_hole.setVisibility(View.GONE);
        accordian_LL.setVisibility(View.GONE);

        ed_equip_no = (EditText)findViewById(R.id.ed_equip_no);
        ed_equip_no.setText(GlobalConstants.equipment_no);
        ed_to_statusDate = (EditText)findViewById(R.id.ed_to_statusDate);
        ed_remarks = (EditText)findViewById(R.id.ed_remarks);
        im_close = (ImageView) findViewById(R.id.im_close);
        im_search = (ImageView) findViewById(R.id.im_search);
        searchView2 = (EditText)findViewById(R.id.searchView2);

        status_home = (Button)findViewById(R.id.status_home);
        status_refresh = (Button)findViewById(R.id.status_refresh);
        status_submit = (Button)findViewById(R.id.status_submit);

        tv_name = (TextView)findViewById(R.id.tv_name);
        tv_equip_no = (TextView)findViewById(R.id.tv_equip_no);
        tv_status = (TextView)findViewById(R.id.tv_status);
        tv_date = (TextView)findViewById(R.id.tv_date);

        systemDate = new SimpleDateFormat("dd-MMM-yyyy").format(new Date());
        SimpleDateFormat time = new SimpleDateFormat("hh:mm");
        curTime = time.format(new Date());

        ed_to_statusDate.setText(systemDate);



        status_home.setOnClickListener(this);
        status_refresh.setOnClickListener(this);
        status_submit.setOnClickListener(this);
        ed_to_statusDate.setOnClickListener(this);
        im_close.setOnClickListener(this);
        im_search.setOnClickListener(this);

        equip_down.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_hole.setVisibility(View.VISIBLE);
                equip_down.setVisibility(View.GONE);
                equip_up.setVisibility(View.VISIBLE);

            }
        });

        equip_up.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_hole.setVisibility(View.GONE);
                equip_down.setVisibility(View.VISIBLE);
                equip_up.setVisibility(View.GONE);
            }
        });




        drawer = (DrawerLayout) findViewById(R.id.drawer_layout);

        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        menu.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if (drawer.isDrawerOpen(Gravity.START))

                    drawer.closeDrawer(Gravity.END);
                else
                    drawer.openDrawer(Gravity.START);
            }
        });

        navigationView.setNavigationItemSelectedListener(this);

        if(cd.isConnectingToInternet()){

            new Get_Current_Status().execute();
            new CurrentStatus_Customer_details().execute();

        }else{
            shortToast(getApplicationContext(),"Please check your Internet Connection...!");
        }

        sp_status_customer.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {


            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(count!=0) {
                    getCustomer = sp_status_customer.getSelectedItem().toString();
                    getCustomerID = CustomerDropdownArrayList.get(position).getCode();
                    ed_status_cust.setText(getCustomer);
                }
                count++;
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        sp_current_status.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(count_status!=0) {
                    getCurrentStatusName = sp_current_status.getSelectedItem().toString();

                    getCurrentStatusID = currentStatusBeanArrayList.get(position).getID();
                    ed_current_status.setText(getCurrentStatusName);
                    tv_status_no.setText(getCurrentStatusID);
                    if(cd.isConnectingToInternet()){
                        new Get_To_Status().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                }

                count_status++;


            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
        sp_to_status.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                ToStatus=sp_to_status.getSelectedItem().toString();
                if(ToStatus.equalsIgnoreCase("Please Select"))
                {

                }else{
                    getTostatusName = sp_to_status.getSelectedItem().toString();

                    int Id_position =position -1;
                    getTostatusID = ToStatusDropdownArrayList.get(Id_position).getCode();
                    Log.d("TO Status Name",getTostatusName);
                    Log.d("TO STatus ID",getTostatusID);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });


        String customer = getColoredSpanned("Current Status","#bbbbbb");
        String equip_no = getColoredSpanned("Equipment Number","#bbbbbb");
        String toStatus = getColoredSpanned("To Status","#bbbbbb");
        String toStatusDate = getColoredSpanned("To Status Date","#bbbbbb");

        String surName = getColoredSpanned("*","#cb0da5");

        tv_name.setText(Html.fromHtml(customer+" "+surName));
        tv_equip_no.setText(Html.fromHtml(equip_no+" "+surName));
        tv_status.setText(Html.fromHtml(toStatus+" "+surName));
        tv_date.setText(Html.fromHtml(toStatusDate+" "+surName));


    }


    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.status_home:
                startActivity(new Intent(getApplicationContext(),MainActivity.class));
                break;
            case R.id.status_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.ed_to_statusDate:

                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_date:
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_close:
                LL_hole.setVisibility(View.GONE);
                accordian_LL.setVisibility(View.GONE);
                finish();
                startActivity(getIntent());
                break;
            case R.id.im_search:
                equip_up.setVisibility(View.VISIBLE);
                LL_hole.setVisibility(View.VISIBLE);
                accordian_LL.setVisibility(View.VISIBLE);
                equip_down.setVisibility(View.GONE);
                getCurrentStatusName = ed_current_status.getText().toString();
                getCurrentStatusID = tv_status_no.getText().toString();
                getEquipmentNo = ed_equip_no.getText().toString();
                String current_status = ed_current_status.getText().toString();

                if(getEquipmentNo.length()> 0 && getEquipmentNo.length()<11 ){

                    shortToast(getApplicationContext(),"Please Enter the Valid Equipment Number ..!");

                }else
                {
                    if (cd.isConnectingToInternet()){

                        if(!current_status.equals("")||! getEquipmentNo.equals(""))
                        {
                            new Get_To_Status().execute();
                            new Get_ChangeOfStatus_details().execute();
                        }else {
                            shortToast(getApplicationContext(),"Current Status or Equipment No is required to perform Change of Status Operation");

                        }
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                  /*  Letter = getEquipmentNo.substring(0,4);
                    Number = getEquipmentNo.substring(4,11);

                    if(Character.isLetter(Letter.charAt(0)) && Character.isDigit(Number.charAt(4))){
                        Log.d("Character",Letter);
                        Log.d("Numbers",Number);
                        if (cd.isConnectingToInternet()) {
                            new Get_To_Status().execute();
                            new Get_ChangeOfStatus_details().execute();
                        } else {
                            shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                        }
                    }else{
                        shortToast(getApplicationContext(),"This Equipment Number is Not Valid..!");
                    }*/
                }





                break;
            case R.id.status_submit:
                selected_list=new ArrayList<>();
                getCurrentStatusName = ed_current_status.getText().toString();
                getCustomer = sp_status_customer.getSelectedItem().toString();
                getdate=ed_to_statusDate.getText().toString();
                getremark=ed_remarks.getText().toString();
                getEquipmentNo = ed_equip_no.getText().toString();
              try
              {
                  getTostatusName = sp_to_status.getSelectedItem().toString();

                  if(boxAdapter.getBox().size()==0) {
                      shortToast(getApplicationContext(), "Please Select atleast one Equipment..!");
                  }else {
                      for (COS_Product p : boxAdapter.getBox()) {
                          if (p.box) {
                              if (p.box == true) {

                                  String[] set = new String[5];
                                  set[0] = p.equip_no;
                                  set[1] = p.remarks;
                                  set[2] = p.location;

                                  selected_list.add(set);


                                  //   selected_name.add(set[0]);
                                  LL_hole.setVisibility(View.GONE);


                                  /*if (ToStatus.equalsIgnoreCase("Please Select")) {

                                      shortToast(getApplicationContext(), "Please Select the To Status");
                                      LL_hole.setVisibility(View.VISIBLE);


                                  } else {
                                      if (cd.isConnectingToInternet()) {
                                          new Post_COS_details().execute();
                                      } else {
                                          shortToast(getApplicationContext(), "Please check Your Internet Connection");
                                      }
                                  }*/

                              } else {
                                  shortToast(getApplicationContext(), "Please Select Customer Name");
                              }
                          }
                      }

                      if (cd.isConnectingToInternet()) {
                          if(getTostatusName.equalsIgnoreCase("Please select"))
                          {
                              shortToast(getApplicationContext(), "Please Select To Status..!");
                              LL_hole.setVisibility(View.VISIBLE);

                          }else
                          {
                              new Post_COS_details().execute();
                              LL_hole.setVisibility(View.GONE);

                          }
                      } else {
                          shortToast(getApplicationContext(), "Please check Your Internet Connection");
                      }
                  }

              }catch (Exception e){
                  shortToast(getApplicationContext(),"Please click the Search..!");
              }


                break;
        }
    }
    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

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

           // view.setMinDate(System.currentTimeMillis() - 1000);


            ed_to_statusDate.setText(formatDate(year, month, day));



            //    System.out.println("am a new from date====>>" + str_From);

            //  date.setText(formatDate(year, month, day));

        }
    };



    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem item) {
        // Handle navigation view item clicks here.
        //
        int id = item.getItemId();

        if (id == R.id.nav_home) {
            finish();
            startActivity(new Intent(getApplicationContext(),MainActivity.class));
            // Handle the camera action
        }else if (id == R.id.nav_changePwd) {
            startActivity(new Intent(getApplicationContext(),Change_Password.class));
        } else if (id == R.id.nav_Logout) {
            if(mServiceIntent!=null)
                getApplicationContext().stopService(mServiceIntent);
            SharedPreferences.Editor editor = sp.edit();
            editor.putBoolean(SP_LOGGED_IN, false);
            editor.commit();
            finish();
            Intent in = new Intent(getApplicationContext(), Login_Activity.class);
            startActivity(in);

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    public class Get_Current_Status extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(ChangeOfStatus.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCurrentStatusList);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));

               /* JSONObject jsonObject1 = new JSONObject();
                jsonObject1.put("Credentials",jsonObject);*/

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("SearchResult");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list"+jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found.");
                            }
                        });
                    }else {

                        currentStatusBeanArrayList = new ArrayList<CurrentStatusBean>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            currentStatusBean = new CurrentStatusBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            currentStatusBean.setID(jsonObject.getString("StatusID"));
                            currentStatusBean.setName(jsonObject.getString("StatusName"));
                            StatusName = jsonObject.getString("StatusName");
                            StatusId = jsonObject.getString("StatusID");
                            String[] set1 = new String[2];
                            set1[0] = StatusName;
                            set1[1] = StatusId;
                            dropdown_currentStatus_list.add(StatusName);

                            currentStatusBeanArrayList.add(currentStatusBean);
                        }
                    }
                }else if(jsonarray.length()<1){
                    runOnUiThread(new Runnable(){

                        @Override
                        public void run(){
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(),"No Records Found.");
                        }
                    });

                }

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
        protected void onPostExecute (Void aVoid){



            if(jsonarray!=null)
            {
//                UserListAdapter adapter = new UserListAdapter(Create_GateIn.this, R.layout.list_item_row, pending_arraylist);
//                listview.setAdapter(adapter);

                for(int i=0;i<dropdown_currentStatus_list.size();i++)
                {
                    try {
                        Log.i("GlobalConstants.status",GlobalConstants.status);
                        if (GlobalConstants.status.equals(dropdown_currentStatus_list.get(i))) {

                            int index = dropdown_currentStatus_list.indexOf(GlobalConstants.status);
                            dropdown_currentStatus_list.remove(index);

                            dropdown_currentStatus_list.add(0, GlobalConstants.status);
                            System.out.println("after appartmentArray" + dropdown_currentStatus_list.size());

                            System.out.println("appartmentArray" + currentStatusBeanArrayList.get(index));
                            CurrentStatusBean appartment_nameBean = currentStatusBeanArrayList.get(index);
                            currentStatusBeanArrayList.remove(index);
                            currentStatusBeanArrayList.add(0, appartment_nameBean);
                      /*  appartmentArray.remove(appartmentArray.get(i));
                        appartmentArray.add(0, appartmentname);*/
                            //  tv_toolbarTitle.setBackgroundColor(Color.BLUE);


                        }
                    }catch (Exception e)
                    {
                        Log.i("Exception", String.valueOf(e));
                    }
                }

                ArrayAdapter<String> CargoAdapter = new ArrayAdapter<>(getApplicationContext(),R.layout.spinner_text,dropdown_currentStatus_list);
                CargoAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_current_status.setAdapter(CargoAdapter);





            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }

            progressDialog.dismiss();

        }

    }

    public class CurrentStatus_Customer_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(ChangeOfStatus.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCreateGateInCustomer);
//            httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
//            httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));

               /* JSONObject jsonObject1 = new JSONObject();
                jsonObject1.put("Credentials",jsonObject);*/

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("arrayOfDropdowns");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list"+jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found.");
                            }
                        });
                    }else {

                        dropdown_customer_list = new ArrayList<>();



                        worldlist = new ArrayList<String>();
                        CustomerDropdownArrayList=new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("Name"));
                            customer_DropdownBean.setCode(jsonObject.getString("Code"));
                            CustomerName = jsonObject.getString("Name");
                            CustomerCode = jsonObject.getString("Code");
                            String[] set1 = new String[2];
                            set1[0] = CustomerName;
                            set1[1] = CustomerCode;
                            dropdown_customer_list.add(set1);
                            Cust_name.add(set1[0]);
                            Cust_code.add(set1[1]);
                            CustomerDropdownArrayList.add(customer_DropdownBean);
                            worldlist.add(CustomerName);


                        }
                    }
                }else if(jsonarray.length()<1){
                    runOnUiThread(new Runnable(){

                        @Override
                        public void run(){
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(),"No Records Found.");


                        }
                    });

                }

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
        protected void onPostExecute (Void aVoid){



            if(CustomerDropdownArrayList!=null)
            {
                ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<>(getApplicationContext(),R.layout.spinner_text,worldlist);
                CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_status_customer.setAdapter(CustomerAdapter);

            }
            else if(CustomerDropdownArrayList.size()<1)
            {
                shortToast(getApplicationContext(),"Data Not Found");

            }

            progressDialog.dismiss();

        }

    }


    public class Get_To_Status extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(ChangeOfStatus.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLToStatusList);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("CurrentStatus",getCurrentStatusName);

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("To rep", resp);
                Log.d("To Status",jsonObject.toString());
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("SearchResult");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list"+jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found.");
                            }
                        });
                    }else {

                        dropdown_customer_list = new ArrayList<>();



                        worldlist = new ArrayList<String>();
                        ToStatusDropdownArrayList=new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("StatusName"));
                            customer_DropdownBean.setCode(jsonObject.getString("StatusID"));
                            ToStatusName = jsonObject.getString("StatusName");
                            ToStatusID = jsonObject.getString("StatusID");
                            String[] set1 = new String[2];
                            set1[0] = ToStatusName;
                            set1[1] = ToStatusID;
                            dropdown_customer_list.add(set1);
                            To_Status_name.add(set1[0]);
                            To_Status_Code.add(set1[1]);
                            ToStatusDropdownArrayList.add(customer_DropdownBean);
                            worldlist.add(ToStatusName);


                        }
                    }
                }else if(jsonarray.length()<1){
                    runOnUiThread(new Runnable(){

                        @Override
                        public void run(){
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(),"No Records Found.");


                        }
                    });

                }

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
        protected void onPostExecute (Void aVoid){



            if(jsonarray!=null)
            {

                worldlist.add(0,"Please Select");
                for (int i=0; i<worldlist.size();i++){
                    if("Please Select".equalsIgnoreCase(worldlist.get(i))){
                        int index = worldlist.indexOf("Please Select");
                        worldlist.remove(index);
                        worldlist.add(0,"Please Select");



                    }
                }




                ArrayAdapter<String> CargoAdapter = new ArrayAdapter<>(getApplicationContext(),R.layout.spinner_text,worldlist);
                CargoAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_to_status.setAdapter(CargoAdapter);


            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");
            }
            progressDialog.dismiss();
        }

    }

    public class Get_ChangeOfStatus_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(ChangeOfStatus.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
//            if ((progressDialog != null) && progressDialog.isShowing()) {
            progressDialog.show();
//            }
        }

        @Override
        protected Void doInBackground(Void... params) {

            ServiceHandler sh = new ServiceHandler();
            HttpParams httpParameters = new BasicHttpParams();
            DefaultHttpClient httpClient = new DefaultHttpClient(httpParameters);
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLSearch);

            httpPost.setHeader("Content-Type", "application/json");


            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("StatusID",getCurrentStatusID);
                jsonObject.put("EquipmentNo",getEquipmentNo);
                try {
                    if (getCustomerID.equals("")) {
                        jsonObject.put("CustomerID", "");

                    }else
                    {
                        jsonObject.put("CustomerID", getCustomerID);

                    }
                }catch (Exception e)
                {
                    jsonObject.put("CustomerID", "");

                }

               /* JSONObject jsonObject1 = new JSONObject();
                jsonObject1.put("Credentials",jsonObject);*/

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("search-rep", resp);
                Log.d("Request",jsonObject.toString());
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("SearchResult");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list" + jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found");
                            }
                        });
                    } else {

                        Change_Status_arraylist = new ArrayList<ChanfeOfStatusBean>();
                        products=new ArrayList<COS_Product>();

                        for (int i = 0; i < jsonarray.length(); i++) {

                            change_Status_bean = new ChanfeOfStatusBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            change_Status_bean.setEquipmentNo(jsonObject.getString("EquipmentNo"));
                            change_Status_bean.setType(jsonObject.getString("Type"));
                            change_Status_bean.setCustomer(jsonObject.getString("Customer"));
                            change_Status_bean.setInDate(jsonObject.getString("InDate"));
                            change_Status_bean.setPreviousCargo(jsonObject.getString("PreviousCargo"));
                            change_Status_bean.setCurrentStatus(jsonObject.getString("CurrentStatus"));
                            change_Status_bean.setCurrentStatusDate(jsonObject.getString("CurrentStatusDate"));
                            change_Status_bean.setYardLocation(jsonObject.getString("YardLocation"));
                            change_Status_bean.setRemarks(jsonObject.getString("Remarks"));

                            String[] indate = jsonObject.getString("InDate").split(" ");
                            String in_date = indate[0];
                            String[] current_status_date = jsonObject.getString("CurrentStatusDate").split(" ");
                            String current_date = current_status_date[0];
                            Change_Status_arraylist.add(change_Status_bean);
                            products.add(new COS_Product(jsonObject.getString("Customer"),jsonObject.getString("PreviousCargo"),
                                    in_date ,jsonObject.getString("EquipmentNo"),jsonObject.getString("CurrentStatus"),current_date,
                                    jsonObject.getString("Type"),jsonObject.getString("YardLocation"),jsonObject.getString("Remarks"),false));



                        }
                    }
                } else {
                    runOnUiThread(new Runnable() {

                        @Override
                        public void run() {
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(), "No Records Found");


                        }
                    });

                }

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


//            if ((progressDialog != null) && progressDialog.isShowing()) {
            progressDialog.dismiss();
//            }
            if (jsonarray != null) {
              /*  adapter = new ChangeOfStatus.UserListAdapter(ChangeOfStatus.this, R.layout.cleaning_list_item_row, Change_Status_arraylist);
                status_list_view.setAdapter(adapter);*/

                boxAdapter = new ListAdapter(ChangeOfStatus.this, products);
                status_list_view.setAdapter(boxAdapter);
                status_list_view.setVisibility(View.VISIBLE);
                list_noData.setVisibility(View.GONE);

                searchView2.addTextChangedListener(new TextWatcher() {

                    @Override
                    public void afterTextChanged(Editable arg0) {
                        // TODO Auto-generated method stub
                        String text = searchView2.getText().toString().toLowerCase(Locale.getDefault());
                        boxAdapter.filter(text);
                    }

                    @Override
                    public void beforeTextChanged(CharSequence arg0, int arg1,
                                                  int arg2, int arg3) {
                        // TODO Auto-generated method stub
                    }

                    @Override
                    public void onTextChanged(CharSequence arg0, int arg1, int arg2,
                                              int arg3) {
                        // TODO Auto-generated method stub
                    }
                });


            } else {
                shortToast(getApplicationContext(), "Data Not Found");
                try {
                    status_list_view.setVisibility(View.GONE);

                    list_noData.setVisibility(View.VISIBLE);
                }catch (Exception e)
                {

                }

            }

        }

    }
    public class ListAdapter extends BaseAdapter {
        private final ArrayList<COS_Product> arraylist;
        Context ctx;
        LayoutInflater lInflater;
        ArrayList<COS_Product> objects;
        COS_Product p;

        ListAdapter(Context context, ArrayList<COS_Product> products) {
            ctx = context;
            objects = products;
            this.arraylist = new ArrayList<COS_Product>();
            this.arraylist.addAll(objects);
            lInflater = (LayoutInflater) ctx
                    .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        }

        @Override
        public int getCount() {
            return objects.size();
        }

        @Override
        public Object getItem(int position) {
            return objects.get(position);
        }

        @Override
        public long getItemId(int position) {
            return position;
        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {
            View view = convertView;
            if (view == null) {

                view = lInflater.inflate(R.layout.change_of_status_list_item_row, parent, false);
            }

            p = getProduct(position);

            ((TextView) view.findViewById(R.id.text1)).setText(p.name); // cust name
            ((TextView) view.findViewById(R.id.text2)).setText(p.equip_no+","+p.type); // eqp no and type
            ((TextView) view.findViewById(R.id.text3)).setText(p.inDate); // in-date
            ((TextView) view.findViewById(R.id.text4)).setText(p.pre_cargo); // prev-cargo
            ((TextView) view.findViewById(R.id.tv_cleaning_method)).setText(p.current_status_date); // curent status date
            ((TextView) view.findViewById(R.id.tv_additional_cleraning)).setText(p.current_status); // current status
            ((TextView) view.findViewById(R.id.text7)).setText(p.remarks);


            CheckBox cbBuy = (CheckBox) view.findViewById(R.id.checkbox);
            cbBuy.setVisibility(View.VISIBLE);
            cbBuy.setOnCheckedChangeListener(myCheckChangList);
            cbBuy.setTag(position);
            cbBuy.setChecked(p.box);




          /*  for(int i=0;i<selected_member_arraylist.size();i++)
            {
                if(p.memberId .equalsIgnoreCase(String.valueOf(selected_member_arraylist.get(i).getId())))
                {
                    cbBuy.setChecked(true);
                }
            }
*/
            return view;
        }

        COS_Product getProduct(int position) {
            return ((COS_Product) getItem(position));
        }

        ArrayList<COS_Product> getBox() {


            box = new ArrayList<COS_Product>();
            box.clear();
            for (COS_Product p : objects) {
                if (p.box)
                    box.add(p);
            }
            return box;
        }

        CompoundButton.OnCheckedChangeListener myCheckChangList = new CompoundButton.OnCheckedChangeListener() {
            public void onCheckedChanged(CompoundButton buttonView,
                                         boolean isChecked) {
                getProduct((Integer) buttonView.getTag()).box = isChecked;
            }
        };

        public void filter(String charText) {
            charText = charText.toLowerCase(Locale.getDefault());
            objects.clear();
            if (charText.length() == 0) {
                objects.addAll(arraylist);
            } else {
                for (COS_Product wp : arraylist) {
                    if (wp.name.toLowerCase(Locale.getDefault()).contains(charText) ||
                            wp.equip_no.toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.pre_cargo.toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.type.toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.current_status.toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.current_status_date.toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.inDate.toLowerCase(Locale.getDefault()).contains(charText))
                    {
                        objects.add(wp);
                    }/*else{
                        list_noData.setVisibility(View.VISIBLE);
                    }*/
                }
            }
            notifyDataSetChanged();
        }

    }

    public class UserListAdapter extends BaseAdapter {
        private final ArrayList<ChanfeOfStatusBean> arraylist;
        Context context;
        ArrayList<ChanfeOfStatusBean> list = new ArrayList<>();
        int resource;
        private ChanfeOfStatusBean userListBean;
        int lastPosition = -1;

        public UserListAdapter(Context context, int resource, ArrayList<ChanfeOfStatusBean> list) {
            this.context = context;
            this.list = list;
            this.resource = resource;
            this.arraylist = new ArrayList<ChanfeOfStatusBean>();
            this.arraylist.addAll(list);
        }

        @Override
        public int getCount() {
            return list.size();
        }

        @Override
        public Object getItem(int position) {
            return list.get(position);
        }

        @Override
        public long getItemId(int position) {
            return position;
        }

        @Override
        public View getView(final int position, View convertView, ViewGroup parent) {
            if (convertView == null) {
                LayoutInflater inflater = LayoutInflater.from(context);
                convertView = inflater.inflate(resource, null);
                holder = new ViewHolder();



                holder.whole = (LinearLayout) convertView.findViewById(R.id.LL_whole);
                holder.Cust_Name = (TextView) convertView.findViewById(R.id.text1);
                holder.equip_no = (TextView) convertView.findViewById(R.id.text2);
                holder.date = (TextView) convertView.findViewById(R.id.text3);
                holder.previous_crg = (TextView) convertView.findViewById(R.id.text4);
                holder.location = (TextView)convertView.findViewById(R.id.text5);
                holder.remark = (TextView)convertView.findViewById(R.id.text6);
                holder.currentStatusDate = (TextView)convertView.findViewById(R.id.tv_cleaning_method);
                holder.currentStatus = (TextView)convertView.findViewById(R.id.tv_additional_cleraning);



                convertView.setTag(holder);
            } else {
                holder = (ViewHolder) convertView.getTag();
            }
            if (list.size() < 1) {
                Toast.makeText(getApplicationContext(), "NO DATA FOUND", Toast.LENGTH_LONG).show();
            } else {
                userListBean = list.get(position);
                String[] parts = userListBean.getInDate().split(" ");
                String part1_date = parts[0];
                String[] parts1 = userListBean.getCurrentStatusDate().split(" ");
                String part2_date = parts[0];
//                String part1_time = parts[1];
//                System.out.println("from date after split" + part1_date);
                holder.equip_no.setText(userListBean.getEquipmentNo() + "," + userListBean.getType());
                holder.Cust_Name.setText(userListBean.getCustomer());
                holder.date.setText(part1_date);
                holder.previous_crg.setText(userListBean.getPreviousCargo());
                holder.currentStatusDate.setText(part2_date);
                holder.currentStatus.setText(userListBean.getCurrentStatus());
                holder.location.setText(userListBean.getYardLocation());
                holder.remark.setText(userListBean.getRemarks());



                holder.whole.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
//                        Intent i = new Intent(getApplicationContext(), HeatingPeriod.class);

                        GlobalConstants.equipment_no = list.get(position).getEquipmentNo();
                        GlobalConstants.customer_name = list.get(position).getCustomer();
                        GlobalConstants.date = list.get(position).getInDate();
                        GlobalConstants.type = list.get(position).getType();
                        GlobalConstants.previous_cargo = list.get(position).getPreviousCargo();
                        GlobalConstants.location = list.get(position).getYardLocation();
                        GlobalConstants.remark = list.get(position).getRemarks();
                        GlobalConstants.currentStatus = list.get(position).getCurrentStatus();
                        GlobalConstants.currentStatusDate = list.get(position).getCurrentStatusDate();


//                        startActivity(i);
                    }
                });

            }
            return convertView;
        }

        public void filter(String charText) {
            charText = charText.toLowerCase(Locale.getDefault());
            list.clear();
            if (charText.length() == 0) {
                list.addAll(arraylist);
            } else {
                for (ChanfeOfStatusBean wp : arraylist) {
                    if (wp.getCustomer().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getEquipmentNo().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getPreviousCargo().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getInDate().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getType().toLowerCase(Locale.getDefault()).contains(charText)
                            ) {
                        list.add(wp);
                    }
                }
            }
            notifyDataSetChanged();
        }




    }

    static class ViewHolder {
        TextView equip_no, date, Cust_Name,currentStatusDate, previous_crg, currentStatus, location, remark,startDate, startTime, pre_id, TTL_RT_NC, cust_code, type_id, code_id,
                endTime, endDate, temp, min_htngPRD_NC, hourly_charge, ttl_htngPrd, cust_currency, min_htngRate_NC, type;
        CheckBox checkBox;

        LinearLayout whole;
    }


    public class Post_COS_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray preadvicejsonlist;
        private JSONObject preadvicejsonObject;
        private JSONObject SearchValuesObject;
        private String preadviceObject;
        private JSONObject getJsonObject;


        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(ChangeOfStatus.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLChange_Of_Status_Update);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try{
                JSONObject jsonObject = new JSONObject();

                preadvicejsonlist = new JSONArray();
                SearchValuesObject=new JSONObject();

                /*  "EquipmentNo":"LOOO2222222",
                    "Remarks":"",
                    "YardLocation":"test",
                    "Checked":"True",
                    "NEW_EQPMNT_STTS_CD":"ASR",
                    "NEW_ACTVTY_DT":"11-29-2016 12:00:00 AM",
                     "EQPMNT_STTS_ID":"18"*/
                for (int i = 0; i <selected_list.size(); i++) {
                    String[] retailer = selected_list.get(i);

                    preadvicejsonObject=new JSONObject();
                    preadvicejsonObject.put("EquipmentNo", retailer[0]);
                    preadvicejsonObject.put("Remarks", retailer[1]);
                    preadvicejsonObject.put("YardLocation",  retailer[2]);
                    preadvicejsonObject.put("Checked", "True");
                    preadvicejsonObject.put("NEW_EQPMNT_STTS_CD", getTostatusName);
                    preadvicejsonObject.put("NEW_ACTVTY_DT", getdate+" "+curTime);
                    preadvicejsonObject.put("EQPMNT_STTS_ID", getTostatusID);
                    preadvicejsonlist.put(preadvicejsonObject);
                }


                SearchValuesObject.put("ArrayOfEquipmentListCOS",preadvicejsonlist);


                /* "UserName":"ADMIN",
                 "StatusID":"1",
                 "EquipmentNo":"",
                 "CustomerID":"",*/

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("EquipmentNo", getEquipmentNo);
                try {
                    if (getCustomerID.equals("")) {
                        jsonObject.put("CustomerID", "");

                    }else
                    {
                        jsonObject.put("CustomerID", getCustomerID);

                    }
                }catch (Exception e)
                {
                    jsonObject.put("CustomerID", "");

                }

                jsonObject.put("StatusID", getCurrentStatusID);
                jsonObject.put("EquipmentListCOS", SearchValuesObject);

               /* JSONObject jsonObject1 = new JSONObject();
                jsonObject1.put("Credentials",jsonObject);*/

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                Log.d("Search_request", jsonObject.toString());

                JSONObject jsonrootObject = new JSONObject(resp);
                 getJsonObject = jsonrootObject.getJSONObject("d");


                if (getJsonObject != null) {

                    System.out.println("Am HashMap list"+jsonarray);
                    if (getJsonObject.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found");
                            }
                        });
                    }else {

                        returnstatus=getJsonObject.getString("Status");

                    }
                }else if(getJsonObject.length()<1){
                    runOnUiThread(new Runnable(){

                        @Override
                        public void run(){
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(),"No Records Found");


                        }
                    });

                }

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
        protected void onPostExecute (Void aVoid){



            if(getJsonObject!=null)
            {
                if(returnstatus.equalsIgnoreCase("Success"))
                {
                    shortToast(getApplicationContext(),"Status Changed Successfully..!");
                    ed_equip_no.setText("");
                    finish();
                    startActivity(getIntent());
                }else
                {
                    shortToast(getApplicationContext(),"Status does not Changed..!");
                }

            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }

            progressDialog.dismiss();

        }

    }




}
