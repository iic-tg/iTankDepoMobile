package com.i_tankdepo;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.app.TimePickerDialog;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.widget.Toolbar;
import android.text.Html;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.Multi_Photo_Bean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Created by Admin on 12/29/2016.
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
public class CleaningInstruction extends CommonActivity {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu,im_startDate,im_endDate,im_startTime,im_endTime,im_info_cleaning,
            iv_back,add_new_heating,im_okay,im_cancel,iv_changeOfStatus,im_cleaning_startTime,im_org_cleanDate,im_lat_cleanDate,im_cleaning_endTime;
    private TextView tv_toolbarTitle,cleaning_text,text1,tv_lat_cleanDate,tv_org_cleanDate;
    Button bt_pending, bt_add, bt_mysubmit, bt_submit,bt_home, bt_refresh, im_add, im_print,cleaning,heating,inspection,Leaktest,bt_gateout,bt_cleaningInst;
    private LinearLayout LL_heat_submit,LL_heat;
    private RelativeLayout RL_Repair,RL_heating;
    private EditText ed_cust_name,ed_chemical_name,ed_cleanRefNo,ed_org_cleanDate,ed_lat_cleanDate,ed_ed_eqp_no,ed_in_Date,ed_type,ed_previous_cargo,ed_status,ed_remark;
    private Spinner sp_previ_cargo2,sp_previ_cargo3;
    private String curTime,systemDate;
    static final int DATE_DIALOG_ID = 1999;
    static final int DATE_DIALOG_ID_ORG= 1999;
    private Calendar c;
    private int year,month,day,second;
    TimePickerDialog timePickerDialog;
    private boolean latest_cleanDate=false;
    int mHour,mMinute;
    private String get_status,get_previous_cargo,get_status_id;
    private String get_ed_cust_name,get_ed_ed_eqp_no,get_ed_in_Date,get_ed_type,get_ed_previous_cargo,get_ed_chemical_name,get_ed_status,
            get_ed_cleanRefNo,get_ed_org_cleanDate,get_ed_lat_cleanDate,get_ed_remark;
    private CheckBox AddClean;
    private String sDate1;
    private Date date1;
    private Date original_date1;
    private Pattern pattern;
    private String REGEX;
    private Matcher matcher;
    private String ReportURL;
    private ImageView tv_view_remarks;
    private String get_remark;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.cleaning_instruction);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setOnClickListener(this);
        menu.setVisibility(View.GONE);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Cleaning Update");

        tv_view_remarks = (ImageView)findViewById(R.id.tv_view_remarks);
        tv_view_remarks.setOnClickListener(this);
        cleaning_text = (TextView)findViewById(R.id.tv_heating);
        cleaning_text.setGravity(Gravity.CENTER);
        heating = (Button)findViewById(R.id.heating);
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        Leaktest = (Button)findViewById(R.id.leakTest);
        inspection.setVisibility(View.GONE);
        heating.setVisibility(View.GONE);
        Leaktest.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);
        cleaning_text.setText("Print Cleaning Instruction");

        bt_gateout = (Button)findViewById(R.id.bt_gateout);
        bt_gateout.setOnClickListener(this);
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        cleaning_text.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                bt_gateout.performClick();
            }
        });
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);
        LL_heat = (LinearLayout) findViewById(R.id.LL_heat);
        LL_heat.setOnClickListener(this);
        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_heating.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_heat.performClick();
            }
        });
        RL_Repair.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_heat.performClick();
            }
        });
        RL_Repair.setVisibility(View.GONE);

        bt_home = (Button) findViewById(R.id.heat_home);
        bt_submit = (Button) findViewById(R.id.heat_submit);
        bt_home.setOnClickListener(this);
        bt_submit.setOnClickListener(this);
        bt_refresh = (Button) findViewById(R.id.heat_refresh);
        bt_refresh.setOnClickListener(this);

        text1 = (TextView)findViewById(R.id.text1);
        im_info_cleaning = (ImageView)findViewById(R.id.im_info_cleaning);
        text1.setText(GlobalConstants.equipment_no+", "+ GlobalConstants.customer_name+", "+ GlobalConstants.equip_status_type);
        AddClean = (CheckBox)findViewById(R.id.AddClean);
//        ed_ed_eqp_no,ed_in_Date,ed_type,ed_previous_cargo,
// ed_status,ed_remark
        ed_cust_name = (EditText)findViewById(R.id.ed_cust_name);
        ed_cleanRefNo = (EditText)findViewById(R.id.ed_cleanRefNo);
        ed_chemical_name = (EditText)findViewById(R.id.ed_chemical_name);
        ed_org_cleanDate = (EditText)findViewById(R.id.ed_org_cleanDate);
        ed_lat_cleanDate = (EditText)findViewById(R.id.ed_lat_cleanDate);
        ed_remark = (EditText)findViewById(R.id.ed_remark);
        ed_status = (EditText)findViewById(R.id.ed_status);
        ed_previous_cargo = (EditText)findViewById(R.id.ed_previous_cargo);
        ed_type = (EditText)findViewById(R.id.ed_type);
        ed_in_Date = (EditText)findViewById(R.id.ed_in_Date);
        ed_ed_eqp_no = (EditText)findViewById(R.id.ed_ed_eqp_no);



        bt_cleaningInst = (Button)findViewById(R.id.bt_cleaningInst);
        bt_cleaningInst.setOnClickListener(this);

        sp_previ_cargo2 = (Spinner)findViewById(R.id.sp_previ_cargo2);
        sp_previ_cargo3 = (Spinner)findViewById(R.id.sp_previ_cargo3);

        im_org_cleanDate = (ImageView)findViewById(R.id.im_org_cleanDate);
        im_lat_cleanDate = (ImageView)findViewById(R.id.im_lat_cleanDate);
        im_startTime = (ImageView)findViewById(R.id.im_startTime);
        im_endTime = (ImageView)findViewById(R.id.im_endTime);
        tv_org_cleanDate = (TextView)findViewById(R.id.tv_org_cleanDate);
        tv_lat_cleanDate = (TextView)findViewById(R.id.tv_lat_cleanDate);

        ed_lat_cleanDate.setOnClickListener(this);


        im_lat_cleanDate.setOnClickListener(this);

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




        String original = getColoredSpanned("Original Cleaning Date","#bbbbbb");
        String latest = getColoredSpanned("Latest Cleaning Date","#bbbbbb");

        String surName = getColoredSpanned("*","#cb0da5");

        tv_org_cleanDate.setText(Html.fromHtml(original+" "+surName));
        tv_lat_cleanDate.setText(Html.fromHtml(latest+" "+surName));

        ed_cust_name.setText(GlobalConstants.customer_name);
        ed_ed_eqp_no.setText(GlobalConstants.equipment_no);
        ed_in_Date.setText(GlobalConstants.indate);
        ed_previous_cargo.setText(GlobalConstants.previous_cargo);
        ed_status.setText(GlobalConstants.status);
        Log.i("cleaning_RefNo",GlobalConstants.cleaning_RefNo);
        sDate1 = GlobalConstants.indate;
        try {
            date1 = new SimpleDateFormat("dd-MMM-yyyy").parse(sDate1);
        } catch (Exception e) {

        }

        if(GlobalConstants.cleaning_RefNo.equalsIgnoreCase("null") ||GlobalConstants.cleaning_RefNo.equals(""))
        {
            ed_cleanRefNo.setText("");

        }else
        {
            ed_cleanRefNo.setText(GlobalConstants.cleaning_RefNo);

        }if(GlobalConstants.remark.equalsIgnoreCase("null") ||GlobalConstants.remark.equals(""))
        {
            ed_remark.setText("");

        }else
        {
            ed_remark.setText(GlobalConstants.remark);

        }
        ed_type.setText(GlobalConstants.equip_status_type);
        ed_chemical_name.setText(GlobalConstants.ChemicalName);
        if(GlobalConstants.org_cleaningDate.equals(null)||GlobalConstants.org_cleaningDate.equals(""))
        {
            Log.i("org_cleaningDate",GlobalConstants.org_cleaningDate);
            ed_org_cleanDate.setText(systemDate);

        }else
        {
            ed_org_cleanDate.setText(GlobalConstants.org_cleaningDate);


        }
        ed_lat_cleanDate.setText(systemDate);

        Log.i("remark",GlobalConstants.remark);
       if(GlobalConstants.add_cleaning_bit.equals("1"))
       {
           AddClean.setChecked(true);
           AddClean.setEnabled(false);
           ed_org_cleanDate.setClickable(false);
           im_org_cleanDate.setClickable(false);
           im_org_cleanDate.setOnClickListener(null);
           ed_org_cleanDate.setOnClickListener(null);
       }else
       {
           AddClean.setEnabled(false);
           im_org_cleanDate.setOnClickListener(this);
           ed_org_cleanDate.setOnClickListener(this);
           ed_org_cleanDate.setClickable(true);
           im_org_cleanDate.setClickable(true);
       }
        /* GlobalConstants.equipment_no = list.get(position).getEquipno();
                        GlobalConstants.customer_name = list.get(position).getCustomerName();
                        GlobalConstants.customer_Id = list.get(position).getCustomerId();
                        GlobalConstants.equip_status = "CLN";
                        GlobalConstants.equip_status_type = list.get(position).getEquipStatusType();
                        GlobalConstants.indate = list.get(position).getInDate();
                        GlobalConstants.previous_cargo = list.get(position).getPrevoiusCargo();
                        GlobalConstants.lastStatusDate = list.get(position).getLastStatusDate();
                        GlobalConstants.add_cleaning_bit = list.get(position).getAdditionalCleaningBit();
                        GlobalConstants.cleaning_id = list.get(position).getCleaningId();
                        GlobalConstants.cleaning_RefNo = list.get(position).getCleaningReferenceNo();
                        GlobalConstants.remark = list.get(position).getRemarks();
                        GlobalConstants.org_cleaningDate = list.get(position).getOriginalCleaningDate();
                        GlobalConstants.clean_rate = list.get(position).getCleaningRate();
                        GlobalConstants.cleaning_method = list.get(position).getCleaningmethod();
                        GlobalConstants.slab_rate = list.get(position).getSlabRate();
                        GlobalConstants.gi_trans_no = list.get(position).getGiTransactionNo();
                        GlobalConstants.status = "CLN";
*/


    }




    @Override
    public void onClick(View view) {
        switch (view.getId())
        {
            case R.id.iv_changeOfStatus:

                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));

                break;
            case R.id.im_info_cleaning:
                    get_status = GlobalConstants.status;
                get_status_id = GlobalConstants.status_id;
                get_previous_cargo = GlobalConstants.previous_cargo;

                popUp_equipment_info(GlobalConstants.equipment_no, get_status, get_status_id, get_previous_cargo, "", "", "", "");
                break;
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.tv_view_remarks:
                get_remark=ed_remark.getText().toString();
                popUp_remarks(get_remark);
                break;
            case R.id.bt_gateout:


                get_ed_cust_name=ed_cust_name.getText().toString();
                get_ed_ed_eqp_no=ed_ed_eqp_no.getText().toString();
                get_ed_in_Date=ed_in_Date.getText().toString();
                get_ed_type=ed_type.getText().toString();
                get_ed_previous_cargo=ed_previous_cargo.getText().toString();
                get_ed_chemical_name=ed_chemical_name.getText().toString();
                get_ed_status=ed_status.getText().toString();
                get_ed_cleanRefNo=ed_cleanRefNo.getText().toString();
                get_ed_org_cleanDate=ed_org_cleanDate.getText().toString();
                get_ed_lat_cleanDate=ed_lat_cleanDate.getText().toString();
                get_ed_remark=ed_remark.getText().toString();

                 pattern = Pattern.compile("[a-zA-Z0-9]*");
                 REGEX = "[^&%$#@!~]*";

                 matcher = pattern.matcher(get_ed_cleanRefNo);
                if (cd.isConnectingToInternet()) {

                    new Print_pdf().execute();

                }else{
                    shortToast(getApplicationContext(), "Please check your Internet Connection.");
                }

                break;
            case R.id.heat_submit:

            //ed_cust_name,ed_ed_eqp_no,ed_in_Date,ed_type,
                // ed_previous_cargo,ed_chemical_name,
                // ed_status,ed_cleanRefNo,ed_org_cleanDate,
                // ed_lat_cleanDate,ed_remark
                get_ed_cust_name=ed_cust_name.getText().toString();
                get_ed_ed_eqp_no=ed_ed_eqp_no.getText().toString();
                get_ed_in_Date=ed_in_Date.getText().toString();
                get_ed_type=ed_type.getText().toString();
                get_ed_previous_cargo=ed_previous_cargo.getText().toString();
                get_ed_chemical_name=ed_chemical_name.getText().toString();
                get_ed_status=ed_status.getText().toString();
                get_ed_cleanRefNo=ed_cleanRefNo.getText().toString();
                get_ed_org_cleanDate=ed_org_cleanDate.getText().toString();
                get_ed_lat_cleanDate=ed_lat_cleanDate.getText().toString();
                get_ed_remark=ed_remark.getText().toString();

                 pattern = Pattern.compile("[a-zA-Z0-9]*");
                 REGEX = "[^&%$#@!~]*";
              /*  int theCount = 0;
                String specialChars = "*//*!@#$%^&*()\"{}_[]|\\?/<>,";
                for (int i = 0; i < s.length(); i++) {
                    if (specialChars.contains(s.substring(i, 1))) {
                        theCount++;
                    }
                }*/
                 matcher = pattern.matcher(get_ed_cleanRefNo);
                if (!matcher.matches()) {
                    shortToast(getApplicationContext(), "Special Character Not Allowed in Cleaning Ref No");

                }else
                {
                    try
                    {
                        Date org_date=new SimpleDateFormat("dd-MMM-yyyy").parse(get_ed_org_cleanDate);
                        Date lat_date=new SimpleDateFormat("dd-MMM-yyyy").parse(get_ed_lat_cleanDate);


                        if(org_date.compareTo(lat_date) > 0) {

                            shortToast(getApplicationContext(), "Latest Cleaning Date should be greater than or equal to Original Cleaning Date");

                        }else{
                            new PostInfo().execute();
                        }

                    }catch (Exception e)
                    {
                        e.printStackTrace();
                    }



                }


                break;
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(),MainActivity.class));
                break;
            case R.id.iv_back:
                finish();
                onBackPressed();
                break;
            case R.id.ed_org_cleanDate:
                latest_cleanDate=false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.ed_lat_cleanDate:

                latest_cleanDate=true;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_org_cleanDate:
                latest_cleanDate=false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_lat_cleanDate:

                latest_cleanDate=true;
                showDialog(DATE_DIALOG_ID);
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

//            view.setMinDate(System.currentTimeMillis() - 1000);

            if(latest_cleanDate==true) {
                ed_lat_cleanDate.setText(formatDate(year, month, day));

            }
            else
            {
                ed_org_cleanDate.setText(formatDate(year, month, day));
            }

            //    System.out.println("am a new from date====>>" + str_From);

            //  date.setText(formatDate(year, month, day));

        }
    };


    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

    }
    public class Print_pdf extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        private JSONObject invitejsonObject;
        private JSONObject jsonObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(CleaningInstruction.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLReportCleaning);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                jsonObject = new JSONObject();





              /*  {
                    "bv_strEquipmentNo":"RAHU1111111",
                        "bv_intDepotID":"1",
                        "bv_intCleaningID":"0",
                        "bv_strGITransactionNo":"8521"
                }*/

                 jsonObject.put("bv_strEquipmentNo",  GlobalConstants.equipment_no);
                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("bv_intCleaningID","0");
                jsonObject.put("bv_strGITransactionNo",GlobalConstants.gi_trans_no );


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("Update request", jsonObject.toString());
                Log.d("Update responce", resp);
                JSONObject jsonResp = new JSONObject(resp);


                JSONObject returnMessage = jsonResp.getJSONObject("d");
                String message = returnMessage.getString("Status");
                 ReportURL = returnMessage.getString("ReportURL");
                responseString = message;
                Log.d("responseString", returnMessage.toString());
              /*  for(int i=0;i<returnMessage.length();i++)
                {
                    String message = returnMessage.getString(i);
                    responseString=message;
                    Log.i("....responseString...",message);
                    // loop and add it to array or arraylist
                }*/


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
            if (responseString != null) {

                if(responseString.equalsIgnoreCase("Success"))
                {

                    if(ReportURL.equals("")||ReportURL.equals(null)||ReportURL.equals("null"))
                    {
                        shortToast(getApplicationContext(),"Report Generation Failed");
                    }else {
                        Intent i = new Intent(Intent.ACTION_VIEW);
                        i.setData(Uri.parse(ReportURL));
                        startActivity(i);
                    }

                }else
                {
                    shortToast(getApplicationContext(),"Report Generation Failed");
                }


            }

            progressDialog.dismiss();

        }
    }

    public class PostInfo extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        private JSONObject invitejsonObject;
        private JSONObject jsonObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(CleaningInstruction.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCleaningUpdate_Pending);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                jsonObject = new JSONObject();



                SimpleDateFormat fromUser = new SimpleDateFormat("dd-MM-yyyy", Locale.ENGLISH);
                SimpleDateFormat myFormat = new SimpleDateFormat("dd-MMM-yyyy", Locale.ENGLISH);
                String datePattern = "\\d{2}-\\d{2}-\\d{4}";


                try {
                    if (get_ed_org_cleanDate.equals(null) || get_ed_org_cleanDate.length() < 0) {

                        get_ed_org_cleanDate = "";
                    } else {
                        Boolean is_edt_Date1 = get_ed_org_cleanDate.matches(datePattern);
                        if (is_edt_Date1 == true) {
                            get_ed_org_cleanDate = myFormat.format(fromUser.parse(get_ed_org_cleanDate));


                        }


                    }

                } catch (Exception e) {
                    get_ed_org_cleanDate = "";
                }
                try {
                    if (get_ed_lat_cleanDate.equals(null) || get_ed_lat_cleanDate.length() < 0) {

                        get_ed_lat_cleanDate = "";
                    } else {
                        Boolean is_edt_Date1 = get_ed_lat_cleanDate.matches(datePattern);
                        if (is_edt_Date1 == true) {
                            get_ed_lat_cleanDate = myFormat.format(fromUser.parse(get_ed_lat_cleanDate));


                        }
                    }

                } catch (Exception e) {
                    get_ed_lat_cleanDate = "";
                }

                                /*
                {
                "bv_strCleaningId":"1",
                "bv_strEquipmentNo":"AAAA1111111",
                "bv_strChemicalName":"Acetic Acid",
                "bv_strCleaningRate":"800.00",
                "bv_strOriginalCleaningDate":"01-Aug-2017",
                "bv_strLastCleaningDate":"01-Aug-2017",
                "bv_strEquipmentStatus":"5",
                "bv_strEquipmentStatusCD":"CLN",
                "bv_strCleaningReferenceNo":"Ref1234567",
                "bv_strRemarks":"Cleaning Remarks",
                "bv_CustomerId":"181",
                "bv_GateInDate":"13-Oct-2017",
                "bv_strGI_TRNSCTN_NO":"8496",
                "bv_intActivityId":"8496",
                "bv_blnAdditionalCleaningFlag":"False",
                "bv_blnSlabRateFlag":"False",
                "UserName":"ADMIN"

                }
            */
                 if(GlobalConstants.cleaning_id.equals("null")||GlobalConstants.cleaning_id.equals("")||
                         GlobalConstants.cleaning_id.equals(null))
                 {
                     jsonObject.put("bv_strCleaningId", "1");

                 }else
                 {
                     jsonObject.put("bv_strCleaningId", GlobalConstants.cleaning_id);

                 }
                jsonObject.put("bv_strEquipmentNo",  GlobalConstants.equipment_no);
                jsonObject.put("bv_strChemicalName",GlobalConstants.previous_cargo );
                if( GlobalConstants.clean_rate.equals("null")|| GlobalConstants.clean_rate.equals("")||
                        GlobalConstants.clean_rate.equals(null))
                {
                    jsonObject.put("bv_strCleaningRate", JSONObject.NULL );
                }else
                {
                    jsonObject.put("bv_strCleaningRate", GlobalConstants.clean_rate );
                }

                jsonObject.put("bv_strOriginalCleaningDate",get_ed_org_cleanDate );
                jsonObject.put("bv_strLastCleaningDate",get_ed_lat_cleanDate );
                jsonObject.put("bv_strEquipmentStatus",GlobalConstants.equip_status_id );
                jsonObject.put("bv_strEquipmentStatusCD",GlobalConstants.equip_status );
                jsonObject.put("bv_strCleaningReferenceNo", get_ed_cleanRefNo);
                jsonObject.put("bv_strRemarks",get_ed_remark );
                jsonObject.put("bv_CustomerId",GlobalConstants.customer_Id );
                jsonObject.put("bv_GateInDate",get_ed_in_Date );
                jsonObject.put("bv_strGI_TRNSCTN_NO",GlobalConstants.gi_trans_no );
                jsonObject.put("bv_intActivityId","84" );
                if(GlobalConstants.add_cleaning_bit.equals("1"))
                {
                    jsonObject.put("bv_blnAdditionalCleaningFlag","True");

                }else
                {
                    jsonObject.put("bv_blnAdditionalCleaningFlag","False");

                }

                jsonObject.put("UserName",sp.getString(SP_USER_ID,"user_Id"));

                jsonObject.put("bv_blnSlabRateFlag", GlobalConstants.bv_blnSlabRateFlag);

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("Update request", jsonObject.toString());
                Log.d("Update responce", resp);
                JSONObject jsonResp = new JSONObject(resp);


                JSONObject returnMessage = jsonResp.getJSONObject("d");
                String message = returnMessage.getString("Status");
                responseString = message;
                Log.d("responseString", returnMessage.toString());


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
            if (responseString != null) {

                if(responseString.equalsIgnoreCase("Success"))
                {
                    shortToast(getApplicationContext(),"Cleaning Created Successfully");
                    finish();
                    startActivity(new Intent(getApplicationContext(),Cleaning.class));
                }else
                {
                    shortToast(getApplicationContext(),"Cleaning Create Failed");
                }

            }

            progressDialog.dismiss();

        }
    }

}
