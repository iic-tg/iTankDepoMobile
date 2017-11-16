package com.i_tankdepo;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.Editable;
import android.text.Html;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.LeakTestBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
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

import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

/**
 * Created by Admin on 12/22/2016.
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
public class LeakTestUpdate extends CommonActivity{
    private ImageView menu,iv_back,add_new_heating,im_testDate;
    private TextView tv_toolbarTitle,leakTest_text,tv_heat_refresh,text1,text2,tv_testDate;
    private Button heat_home,heat_refresh,bt_heating,cleaning,inspection,Leaktest,heat_submit,bt_revisionNo,bt_gateout;
    private LinearLayout LL_heat_submit,LL_heat;
    private RelativeLayout RL_heating,RL_Repair;
    private EditText ed_remarks,ed_current_status,ed_relief_value1,ed_relief_value2,ed_press_guage1,ed_press_guage2,ref_no,ed_testDate;
    private String curTime,systemDate,Cust_Name,Equip_NO,Type,NoofTimesGenerated,TestDate,RevisionNo,relief_value1,relief_value2,pressureGauge1,pressureGauge2,
            shellTest,steamTubeTest,remark,current_status,Gi_trans_no,Checked,LeakTestID,InDate;
    private  Switch switch_shellTest,switch_steam;
    static final int DATE_DIALOG_ID = 1999;
    private Calendar c;
    private int year,month,day,second;
    private ProgressDialog progressDialog;
    private String getEquipNo,getTestDate,getreliefvalue1,getreliefvalue2,getpress_guage1,getpress_guage2,getReamrk,get_switch_shellTest,get_switch_steam;
    private ArrayList<LeakTestBean> leakTest_arraylist = new ArrayList<>();
    private LeakTestBean leakTest_bean;
    private ImageView iv_changeOfStatus;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.leaktest_update);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        menu=(ImageView)findViewById(R.id.iv_menu) ;
        iv_back = (ImageView)findViewById(R.id.iv_back);
        add_new_heating = (ImageView)findViewById(R.id.im_add);
        menu.setVisibility(View.GONE);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Leak Test Update");

        heat_home = (Button)findViewById(R.id.heat_home);
        heat_refresh = (Button)findViewById(R.id.heat_refresh);


        bt_heating = (Button)findViewById(R.id.heating);
        bt_heating.setVisibility(View.GONE);
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        LL_heat = (LinearLayout)findViewById(R.id.LL_heat);
        bt_gateout = (Button)findViewById(R.id.bt_gateout);
        bt_gateout.setVisibility(View.GONE);
        Leaktest = (Button)findViewById(R.id.leakTest);
        inspection.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);
        LL_heat.setAlpha(0.5f);
        LL_heat.setClickable(false);
        leakTest_text = (TextView)findViewById(R.id.tv_heating);
        leakTest_text.setText("Add New");
        LL_heat_submit = (LinearLayout)findViewById(R.id.LL_heat_submit);
        heat_submit = (Button) findViewById(R.id.heat_submit);

        tv_heat_refresh = (TextView)findViewById(R.id.tv_heat_refresh);
        tv_heat_refresh.setText("Reset");

        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_Repair.setVisibility(View.GONE);

        text1 = (TextView)findViewById(R.id.text1);
        text2 = (TextView)findViewById(R.id.text2);
        ed_testDate = (EditText)findViewById(R.id.ed_testDate);
        ref_no = (EditText)findViewById(R.id.ref_no);
        ed_relief_value1 = (EditText)findViewById(R.id.ed_relief_value1);
        ed_relief_value2 = (EditText)findViewById(R.id.ed_relief_value2);
        ed_press_guage1 = (EditText)findViewById(R.id.ed_press_guage1);
        ed_press_guage2 = (EditText)findViewById(R.id.ed_press_guage2);
        ed_remarks = (EditText)findViewById(R.id.ed_remarks);
        ed_current_status = (EditText)findViewById(R.id.ed_current_status);
        switch_shellTest = (Switch)findViewById(R.id.switch_shellTest);
        switch_steam = (Switch)findViewById(R.id.switch_steam);
        im_testDate = (ImageView)findViewById(R.id.im_testDate);
        bt_revisionNo = (Button) findViewById(R.id.bt_revisionNo);

        tv_testDate = (TextView)findViewById(R.id.tv_testDate);

        heat_refresh.setOnClickListener(this);
        heat_home.setOnClickListener(this);
        iv_back.setOnClickListener(this);
        ed_testDate.setOnClickListener(this);
        im_testDate.setOnClickListener(this);
        heat_submit.setOnClickListener(this);
        bt_revisionNo.setOnClickListener(this);

        Cust_Name = GlobalConstants.customer_name;
        Equip_NO = GlobalConstants.equipment_no;
        Type = GlobalConstants.type;
        NoofTimesGenerated = GlobalConstants.no_of_tms_granted;
        TestDate = GlobalConstants.TestDate;
        RevisionNo = GlobalConstants.revisionNo;
        relief_value1 = GlobalConstants.RLFVLV1;
        relief_value2 = GlobalConstants.RLFVLV2;
        pressureGauge1 = GlobalConstants.PG1;
        pressureGauge2 = GlobalConstants.PG2;
        remark = GlobalConstants.remark;
        shellTest = GlobalConstants.SHLL_TST_BT;
        steamTubeTest = GlobalConstants.STM_TB_TST_BT;
        current_status = GlobalConstants.EQPMNT_STTS_CD;
        Gi_trans_no = GlobalConstants.gt_transaction_no;
        LeakTestID = GlobalConstants.leskTestID;
        InDate = GlobalConstants.date;
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);



        String[] parts = TestDate.split(" ");
        String part1_date = parts[0];

        SimpleDateFormat fromUser = new SimpleDateFormat("dd-MM-yyyy", Locale.ENGLISH);
        SimpleDateFormat myFormat = new SimpleDateFormat("dd-MMM-yyyy", Locale.ENGLISH);
        String createddate = null;
        try {
            createddate = myFormat.format(fromUser.parse(part1_date));

        } catch (ParseException e) {
            e.printStackTrace();
        }
        Log.i("date", createddate);


        text1.setText(Cust_Name + " , " + Equip_NO + " , " + Type);
        text2.setText(NoofTimesGenerated);
        ed_testDate.setText(createddate);
        ed_relief_value1.setText(relief_value1);
        ed_relief_value2.setText(relief_value2);
        ed_press_guage1.setText(pressureGauge1);
        ed_press_guage2.setText(pressureGauge2);
        ed_remarks.setText(remark);
        ed_current_status.setText(current_status);
        bt_revisionNo.setText(RevisionNo);

        if(shellTest.equalsIgnoreCase("True")){
            switch_shellTest.setChecked(true);
            get_switch_shellTest="True";
        }else{
            switch_shellTest.setChecked(false);
            get_switch_shellTest="False";
        }

        if(steamTubeTest.equalsIgnoreCase("True")){
            switch_steam.setChecked(true);
            get_switch_steam="True";

        }else{
            switch_steam.setChecked(false);
            get_switch_steam="False";

        }


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
        systemDate = new SimpleDateFormat("dd-MM-yyyy").format(new Date());

        String testDate = getColoredSpanned("Test Date","#bbbbbb");

        String surName = getColoredSpanned("*","#cb0da5");
        tv_testDate.setText(Html.fromHtml(testDate+" "+surName));


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
            case R.id.ed_test_date:
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_testDate:
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.bt_revisionNo:
                   startActivity(new Intent(getApplicationContext(),RevisionNo.class));
                break;
            case R.id.heat_submit:
                    getTestDate = ed_testDate.getText().toString();
                    getreliefvalue1 = ed_relief_value1.getText().toString();
                    getreliefvalue2 = ed_relief_value2.getText().toString();
                    getpress_guage1 = ed_press_guage1.getText().toString();
                    getpress_guage2 = ed_press_guage2.getText().toString();
                    getReamrk = ed_remarks.getText().toString();

                if(getTestDate == "" && getTestDate == null){
                    shortToast(getApplicationContext(),"Please enter the Testdate..!");
                }else{
                    if(cd.isConnectingToInternet()){
                        new UpdateLeakTest().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                }

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

            //  view.setMinDate(System.currentTimeMillis() - 1000);


                ed_testDate.setText(formatDate(year, month, day));


        }
    };



    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

    }


    public class UpdateLeakTest extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        private JSONArray invite_jsonlist;
        private JSONObject invitejsonObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(LeakTestUpdate.this);
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



                jsonObject.put("LK_TST_ID", LeakTestID);
                jsonObject.put("EQPMNT_NO", Equip_NO );
                jsonObject.put("TST_DT", getTestDate);
                jsonObject.put("SHLL_TST_BT",get_switch_shellTest);
                jsonObject.put("STM_TB_TST_BT", get_switch_steam);
                jsonObject.put("EQPMNT_TYP_CD", Type);
                jsonObject.put("EQPMNT_STTS_CD", current_status);
                jsonObject.put("CHECKED", "True");
                jsonObject.put("GTN_DT", InDate);
                jsonObject.put("CSTMR_CD", Cust_Name);
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
                    Toast.makeText(getApplicationContext(), "Leak Test Updated Successfully.", Toast.LENGTH_SHORT).show();

                    finish();
                    Intent i = new Intent(getApplication(), LeakTest.class);
                    startActivity(i);

                } else {
                    Toast.makeText(getApplicationContext(), "Leak Test not Updated..!", Toast.LENGTH_SHORT).show();

                }
            }else
            {
                Toast.makeText(getApplicationContext(), "Connection TimeOut", Toast.LENGTH_SHORT).show();

            }
            progressDialog.dismiss();

        }
    }


}
