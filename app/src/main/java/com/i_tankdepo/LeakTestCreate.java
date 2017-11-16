package com.i_tankdepo;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.Html;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.CustomerDropdownBean;
import com.i_tankdepo.Beanclass.EquipNoBean;
import com.i_tankdepo.Beanclass.Equipment_HistoryBean;
import com.i_tankdepo.Beanclass.LeakTestBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.SQLite.DBAdapter;
import com.i_tankdepo.helper.ServiceHandler;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
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
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Locale;

/**
 * Created by Admin on 12/23/2016.
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
public class LeakTestCreate extends CommonActivity {
    private ImageView menu,iv_back,add_new_heating,im_testDate,im_in_Date;
    private TextView tv_toolbarTitle,leakTest_text,tv_heat_refresh,text1,text2;
    private Button heat_home,heat_refresh,bt_heating,cleaning,inspection,heat_submit,bt_revisionNo,leakTest,bt_gateout;
    private LinearLayout LL_heat_submit,LL_heat;
    private RelativeLayout RL_heating,RL_Repair;
    private EditText ed_customer,ed_in_Date,ed_testDate,ed_remarks,ed_current_status,ed_relief_value1,ed_relief_value2,ed_press_guage1,ed_press_guage2,ref_no;
    private String curTime,systemDate,Cust_Name,Equip_NO,Type,NoofTimesGenerated,TestDate,RevisionNo,relief_value1,relief_value2,pressureGauge1,pressureGauge2,
            shellTest,steamTubeTest,remark,current_status,Gi_trans_no,Checked;
    private Switch switch_shellTest,switch_steam;
    static final int DATE_DIALOG_ID = 1999;
    private Calendar c;
    private int year,month,day,second;
    private ProgressDialog progressDialog;
    private String getEquip_number,getCust_Name,getInDate,getTestDate,getCurrentStatus,getreliefvalue1,
            getreliefvalue2,getpress_guage1,getpress_guage2,getReamrk,get_switch_shellTest,get_switch_steam;
    private ArrayList<LeakTestBean> leakTest_arraylist = new ArrayList<>();
    private LeakTestBean leakTest_bean;
    private Spinner sp_equipment_no;
    ArrayList<String[]> dropdown_equipment_no_list = new ArrayList<>();
    private ArrayList<String> worldlist;
    private EquipNoBean equipNoBean;
    private Equipment_HistoryBean equipment_historyBean;
    ArrayList<EquipNoBean> equipNoBeanArrayList = new ArrayList<>();
    List<String> Cust_name = new ArrayList<>();
    List<String> equip_no = new ArrayList<>();
    List<String> inDate = new ArrayList<>();
    List<String> current_Status = new ArrayList<>();
    List<String> Eq_type = new ArrayList<>();
    List<String> depot = new ArrayList<>();
    List<String> location = new ArrayList<>();
    private String EquipmentNo,new_gate_In_date,get_type,Customer,Location,cureentStatus,type,Depot,Indate,EquipmentNumber;
    private TextView tv_equip_no,tv_name,tv_testDate;
    private ImageView iv_changeOfStatus;
    private DBAdapter db;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.create_leaktest);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

        db = new DBAdapter(LeakTestCreate.this);

        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        add_new_heating = (ImageView) findViewById(R.id.im_add);
        menu.setVisibility(View.GONE);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Add New Equipment");

        heat_home = (Button) findViewById(R.id.heat_home);
        heat_refresh = (Button) findViewById(R.id.heat_refresh);

        tv_equip_no = (TextView)findViewById(R.id.tv_equip_no);
        tv_name = (TextView)findViewById(R.id.tv_name);
        tv_testDate = (TextView)findViewById(R.id.tv_testDate);

        bt_heating = (Button) findViewById(R.id.heating);
        bt_heating.setVisibility(View.GONE);
        bt_gateout = (Button)findViewById(R.id.bt_gateout);
        bt_gateout.setVisibility(View.GONE);
        cleaning = (Button) findViewById(R.id.cleaning);
        inspection = (Button) findViewById(R.id.inspection);
        LL_heat = (LinearLayout) findViewById(R.id.LL_heat);
        inspection.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);
        LL_heat.setAlpha(0.5f);
        LL_heat.setClickable(false);
        leakTest_text = (TextView) findViewById(R.id.tv_heating);
        leakTest_text.setText("Add New");
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);
        heat_submit = (Button) findViewById(R.id.heat_submit);
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);



        tv_heat_refresh = (TextView) findViewById(R.id.tv_heat_refresh);
        tv_heat_refresh.setText("Reset");

        RL_heating = (RelativeLayout) findViewById(R.id.RL_heating);
        RL_Repair = (RelativeLayout) findViewById(R.id.RL_Repair);
        RL_Repair.setVisibility(View.GONE);

        sp_equipment_no = (Spinner)findViewById(R.id.sp_equipment_no);
        ed_customer = (EditText)findViewById(R.id.ed_customer);
        ed_testDate = (EditText)findViewById(R.id.ed_testDate);
        ref_no = (EditText)findViewById(R.id.ref_no);
        ed_relief_value1 = (EditText)findViewById(R.id.ed_relief_value1);
        ed_relief_value2 = (EditText)findViewById(R.id.ed_relief_value2);
        ed_press_guage1 = (EditText)findViewById(R.id.ed_press_guage1);
        ed_press_guage2 = (EditText)findViewById(R.id.ed_press_guage2);
        ed_remarks = (EditText)findViewById(R.id.ed_remarks);
        switch_shellTest = (Switch)findViewById(R.id.switch_shellTest);
        switch_steam = (Switch)findViewById(R.id.switch_steam);
        im_testDate = (ImageView)findViewById(R.id.im_testDate);
        bt_revisionNo = (Button) findViewById(R.id.bt_revisionNo);
        ed_in_Date = (EditText)findViewById(R.id.ed_in_Date);
        im_in_Date = (ImageView)findViewById(R.id.im_in_Date);
        ed_current_status = (EditText)findViewById(R.id.ed_current_status);

        heat_refresh.setOnClickListener(this);
        heat_home.setOnClickListener(this);
        iv_back.setOnClickListener(this);
        ed_testDate.setOnClickListener(this);
        im_testDate.setOnClickListener(this);
        heat_submit.setOnClickListener(this);
        bt_revisionNo.setOnClickListener(this);
     //   ed_in_Date.setOnClickListener(this);
     //   im_in_Date.setOnClickListener(this);


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
        SimpleDateFormat time = new SimpleDateFormat("hh:mm");
        curTime = time.format(new Date());
        systemDate = new SimpleDateFormat("dd-MMM-yyyy").format(new Date());

        ed_testDate.setText(systemDate);
        get_switch_shellTest="False";
        switch_shellTest.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if(b) {
                    get_switch_shellTest="True";
                }else
                {
                    get_switch_shellTest="False";

                }
            }
        });

        get_switch_steam="False";
        switch_steam.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if(b) {
                    get_switch_steam="True";
                }else
                {
                    get_switch_steam="False";

                }
            }
        });
    
        if(cd.isConnectingToInternet()){
            new Get_Equipment_Number_details().execute();
        }else{
            shortToast(getApplicationContext(),"Please check your Internet Connection..!");
        }

        String Equip_No = getColoredSpanned("Equipment Number","#bbbbbb");
        String customer = getColoredSpanned("Customer","#bbbbbb");
        String testDate = getColoredSpanned("Test Date","#bbbbbb");

        String surName = getColoredSpanned("*","#cb0da5");
        tv_equip_no.setText(Html.fromHtml(Equip_No+" "+surName));
        tv_name.setText(Html.fromHtml(customer+" "+surName));
        tv_testDate.setText(Html.fromHtml(testDate+" "+surName));

    }




    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.iv_changeOfStatus:
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));
                break;
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(), MainActivity.class));
                break;
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.iv_back:
                finish();
                onBackPressed();
                break;
            case R.id.ed_in_Date:
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_in_Date:
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.ed_test_date:
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_testDate:
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.heat_submit:

                getreliefvalue1 = ed_relief_value1.getText().toString();
                getreliefvalue2 = ed_relief_value2.getText().toString();
                getpress_guage1 = ed_press_guage1.getText().toString();
                getpress_guage2 = ed_press_guage2.getText().toString();
                getReamrk = ed_remarks.getText().toString();

                EquipmentNumber = sp_equipment_no.getSelectedItem().toString();
                getTestDate = ed_testDate.getText().toString();
                getInDate = ed_in_Date.getText().toString();

                getCust_Name =ed_customer.getText().toString();

                getCurrentStatus = ed_current_status.getText().toString();




                if(cd.isConnectingToInternet()) {
                    if ((getTestDate.trim().equals("") || getTestDate == null) || EquipmentNumber.equalsIgnoreCase("please select") ||
                            (getCust_Name.trim().equals("") || getCust_Name == null)) {
                        shortToast(getApplicationContext(), "Please key-in Mandate Fields");
                    } else {
                        if (cd.isConnectingToInternet()) {
                            new CreateLeakTest().execute();
                        } else {
                            shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                        }
                    }
                }else{
                    if ((getTestDate.trim().equals("") || getTestDate == null) || EquipmentNumber.equalsIgnoreCase("please select") ||
                            (getCust_Name.trim().equals("") || getCust_Name == null)) {
                        shortToast(getApplicationContext(), "Please key-in Mandate Fields");
                    } else {
                        db.open();

                        db.createLeakTest(sp.getString(SP_USER_ID, "user_Id"),"", EquipmentNumber,getTestDate,get_switch_shellTest,get_switch_steam,
                                get_type,getCurrentStatus, "True" ,getInDate,getCust_Name,getreliefvalue1,getreliefvalue2,getpress_guage1,getpress_guage2,getReamrk);

                        db.close();
                    }
                }
                break;
        }
    }
    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

    }
    @Override
    protected Dialog onCreateDialog(int id) {
        switch (id) {
            case DATE_DIALOG_ID:
                DatePickerDialog datePickerDialog = new DatePickerDialog(this, pickerListener, year, month, day);
                datePickerDialog.getDatePicker().setMaxDate(System.currentTimeMillis());
                // open datepicker dialog.
                // set date picker for current date
                // add pickerListener listner to date picker
                return new DatePickerDialog(this, pickerListener, year, month,day);



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


            ed_testDate.setText(formatDate(year, month, day));


        }
    };

    public class Get_Equipment_Number_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(LeakTestCreate.this);
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
            HttpGet httpGet = new HttpGet(ConstantValues.baseURLEquipmentNO);

            httpGet.setHeader("Content-Type", "application/json");

            try {
                response = httpClient.execute(httpGet);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);

                JSONObject jsonObject = new JSONObject(resp);


                JSONObject getJsonObject = jsonObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("arrayOfLTM");
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

                        dropdown_equipment_no_list = new ArrayList<>();

                        worldlist = new ArrayList<String>();
                        equipNoBeanArrayList=new ArrayList<EquipNoBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            equipNoBean = new EquipNoBean();
                            equipment_historyBean = new Equipment_HistoryBean();

                            jsonObject = jsonarray.getJSONObject(i);

                            SimpleDateFormat fromUser = new SimpleDateFormat("MM-dd-yyyy", Locale.ENGLISH);
                            SimpleDateFormat myFormat = new SimpleDateFormat("dd-MMM-yyyy",Locale.ENGLISH);

                            Indate=jsonObject.getString("InDate");
                            String[] In_date=Indate.split(" ");
                            Indate=In_date[0];
                            try {
                                if (Indate.equals(null) || Indate.length() < 0) {

                                    Indate = "";
                                } else {

                                    Indate = myFormat.format(fromUser.parse(Indate));

                                }

                            }catch (Exception e)
                            {
                                e.printStackTrace();
                            }
                            Log.i("Indate",Indate);
                            equipNoBean.setEquip_no(jsonObject.getString("EQPMNT_NO"));
                            equipNoBean.setInDate(Indate);
                            equipNoBean.setCurrentStatus(jsonObject.getString("CurrentStatus"));
                            equipNoBean.setType(jsonObject.getString("Type"));
                            equipNoBean.setCustomer(jsonObject.getString("Customer"));
                            equipNoBean.setDepot(jsonObject.getString("Depot"));
                            equipNoBean.setYrdLocation(jsonObject.getString("YrdLocation"));

                            equipment_historyBean.setEQPMNT_NO(jsonObject.getString("EQPMNT_NO"));
                            EquipmentNo = jsonObject.getString("EQPMNT_NO");
                            Gi_trans_no = equipment_historyBean.getGI_TRNSCTN_NO();

//                            Indate = jsonObject.getString("InDate");
                            runOnUiThread(new Runnable(){

                                @Override
                                public void run(){
                                    //update ui here
                                    // display toast here

                                }
                            });

                            cureentStatus = jsonObject.getString("CurrentStatus");
                            type = jsonObject.getString("Type");
                            Customer = jsonObject.getString("Customer");
                            Depot = jsonObject.getString("Depot");
                            Location = jsonObject.getString("YrdLocation");
                            String[] set1 = new String[7];
                            set1[0] = EquipmentNo;
                            set1[1] = Indate;
                            set1[2] = cureentStatus;
                            set1[3] = type;
                            set1[4] = Customer;
                            set1[5] = Depot;
                            set1[6] = Location;
                            dropdown_equipment_no_list.add(set1);
                            equip_no.add(set1[0]);
                            inDate.add(set1[1]);
                            current_Status.add(set1[2]);
                            Eq_type.add(set1[3]);
                            Cust_name.add(set1[4]);
                            depot.add(set1[5]);
                            location.add(set1[6]);
                            equipNoBeanArrayList.add(equipNoBean);
                            worldlist.add(EquipmentNo);


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



            if(dropdown_equipment_no_list!=null)
            {


                worldlist.add(0,"Please Select");
                ArrayAdapter<String> EquipmentNoAdapter = new ArrayAdapter<>(getApplicationContext(),R.layout.spinner_text,worldlist);
                EquipmentNoAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_equipment_no.setAdapter(EquipmentNoAdapter);

                sp_equipment_no.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                    @Override
                    public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                        EquipmentNumber = sp_equipment_no.getSelectedItem().toString();

                        if(EquipmentNumber.equalsIgnoreCase("Please Select"))
                        {
                           // shortToast();
                        }else
                        {
                            int Id_position =position -1;
                           // getTostatusID = ToStatusDropdownArrayList.get(Id_position).getCode();
                            get_type=equipNoBeanArrayList.get(Id_position).getType();
                            new_gate_In_date=equipNoBeanArrayList.get(Id_position).getInDate();
                            Log.d("EQUIPMENT NUMBER",get_type);
                            getInDate = ed_in_Date.getText().toString();
                            getCust_Name =ed_customer.getText().toString();
                            getCurrentStatus = ed_current_status.getText().toString();
                            if(cd.isConnectingToInternet()){
                                new EquipmentNoValidation().execute();
                            }else{
                                shortToast(getApplicationContext(),"Please Check your Internet Connection..!");
                            }
                        }
                    }

                    @Override
                    public void onNothingSelected(AdapterView<?> parent) {

                    }
                });



            }
            else if(dropdown_equipment_no_list.size()<1)
            {
                shortToast(getApplicationContext(),"Data Not Found");

            }

            progressDialog.dismiss();

        }

    }

    public class CreateLeakTest extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        private JSONArray invite_jsonlist;
        private JSONObject invitejsonObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(LeakTestCreate.this);
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
            //  HttpPost httppost = new HttpPost("http://49.207.183.45/HH/api/accounts/RegisterUser");
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLUpdateLeakTest);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();



                jsonObject.put("LK_TST_ID", "");
                jsonObject.put("EQPMNT_NO", EquipmentNumber);
                jsonObject.put("TST_DT", getTestDate);
                jsonObject.put("SHLL_TST_BT",get_switch_shellTest);
                jsonObject.put("STM_TB_TST_BT", get_switch_steam);
                jsonObject.put("EQPMNT_TYP_CD", get_type);
                jsonObject.put("EQPMNT_STTS_CD", getCurrentStatus);
                jsonObject.put("CHECKED", "True");
                jsonObject.put("GTN_DT", Indate);
                jsonObject.put("CSTMR_CD", Customer);
                jsonObject.put("RLF_VLV_SRL_1", getreliefvalue1);
                jsonObject.put("RLF_VLV_SRL_2", getreliefvalue2);
                jsonObject.put("PG_1", getpress_guage1);
                jsonObject.put("PG_2", getpress_guage2);
                jsonObject.put("RMRKS_VC",getReamrk );


                jsonObject.put("UserName",sp.getString(SP_USER_ID,"user_Id"));



                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("request", jsonObject.toString());
                Log.d("responce", resp);
                JSONObject jsonResp = new JSONObject(resp);


                JSONObject returnMessage = jsonResp.getJSONObject("d");

                responseString = returnMessage.getString("status");

                Log.d("responseString", returnMessage.toString());
/*
                for(int i=0;i<returnMessage.length();i++)
                {
                    String message = returnMessage.getString(i);
                    responseString=message;
                    Log.i("....responseString...",message);
                    // loop and add it to array or arraylist
                }
*/


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
            if(responseString!=null) {
                if (responseString.equalsIgnoreCase("Updated Successfully") ) {
                    Toast.makeText(getApplicationContext(), "Leak Test Created Successfully.", Toast.LENGTH_SHORT).show();
                    finish();
                    Intent i = new Intent(getApplication(), LeakTest.class);
                    startActivity(i);

                } else {
                    Toast.makeText(getApplicationContext(), "Leak Test Creation Failed..!", Toast.LENGTH_SHORT).show();

                }
            }else
            {
                Toast.makeText(getApplicationContext(), "Connection TimeOut", Toast.LENGTH_SHORT).show();

            }
            progressDialog.dismiss();

        }
    }


    public class EquipmentNoValidation extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        private JSONArray invite_jsonlist;
        private JSONObject invitejsonObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(LeakTestCreate.this);
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
            //  HttpPost httppost = new HttpPost("http://49.207.183.45/HH/api/accounts/RegisterUser");
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLEquipmentNOValidation);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("equipmentNo",EquipmentNumber);
                jsonObject.put("gi_TransactionNo", Gi_trans_no);



                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("request", jsonObject.toString());
                Log.d("responce", resp);
                JSONObject jsonResp = new JSONObject(resp);


                JSONObject returnMessage = jsonResp.getJSONObject("d");

                responseString = returnMessage.getString("status");

                Log.d("responseString", returnMessage.toString());
/*
                for(int i=0;i<returnMessage.length();i++)
                {
                    String message = returnMessage.getString(i);
                    responseString=message;
                    Log.i("....responseString...",message);
                    // loop and add it to array or arraylist
                }
*/


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
            if(responseString!=null) {
                if (responseString.equalsIgnoreCase("Success") ) {

                            ed_in_Date.setText(new_gate_In_date);


                    ed_customer.setText(Customer);
                    ed_current_status.setText(cureentStatus);

                } else {
                    Toast.makeText(getApplicationContext(), "This Equipment No already Exist", Toast.LENGTH_SHORT).show();

                }
            }else
            {
                Toast.makeText(getApplicationContext(), "Connection Time Out", Toast.LENGTH_SHORT).show();

            }
            progressDialog.dismiss();

        }
    }


}
