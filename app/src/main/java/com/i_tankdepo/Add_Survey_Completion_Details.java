package com.i_tankdepo;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.app.TimePickerDialog;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.FragmentActivity;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.text.Html;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.TimePicker;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.CustomerDropdownBean;
import com.i_tankdepo.Beanclass.Equipment_Info_TypeDropdownBean;
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
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

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
public class Add_Survey_Completion_Details extends CommonActivity implements View.OnClickListener {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView iv_changeOfStatus,im_original_est_endDate,menu, im_down, im_up, im_heat_close, im_heat_ok, im_skip,iv_back;


    private TextView tv_toolbarTitle, tv_search_options,no_data;
    LinearLayout LL_hole, LL_heat_submit,LL_search_Value,LL_heat;

    private ProgressDialog progressDialog;

    private Intent mServiceIntent;

    private Button heat_refresh, heat_home, heat_submit,heating,cleaning,inspection;
    private FragmentActivity mycontaxt;
    private TextView next_test_type_code,customer_name,equipment_no,eqip_type;
    private Button LL_Line_iteam,bt_refresh,LL_summary;
    private Spinner sp_tarif_group,sp_invoicing_party,sp_repair_type,sp_tarif_code;
    private ArrayList<Equipment_Info_TypeDropdownBean> dropdown_MoreInfo_arraylist;
    private Equipment_Info_TypeDropdownBean moreInfo_DropdownBean;
    private String infoId,infoCode;
    ArrayList<String> dropdown_MoreInfo_list = new ArrayList<>();
    List<String> Cargo_ID = new ArrayList<>();
    List<String> Cargo_Code = new ArrayList<>();
    List<String> Cargo_Description = new ArrayList<>();
    private Spinner sp_last_test_type;
    private EditText ed_estimate_date,ed_next_type;
    private ArrayList<Object> dropdown_customer_list;
    private ArrayList<String> worldlist;
    private ArrayList<String> Invoice_worldlist;
    private ArrayList<CustomerDropdownBean> CustomerDropdownArrayList;
    private ArrayList<CustomerDropdownBean> CustomerDropdown_Invoice_ArrayList;
    private CustomerDropdownBean customer_DropdownBean;
    private String RepairType_Name,RepairType_Code;
    List<String> Cust_name = new ArrayList<>();
    List<String> Cust_code = new ArrayList<>();
    static final int DATE_DIALOG_ID = 1999;
    private Calendar c;
    private int year,month,day,second;
    private String curTime,systemDate;
    private boolean MH_date=false,MH_time=false,completion_date=false,completion_time=false,survey_date=false;
    private LinearLayout LL_Line;
    private Button LL_add_details;
    private LinearLayout LL_add_completion_details,LL_add_details_back;
    private int mHour,mMinute;
    private TimePickerDialog timePickerDialog;
    private EditText ed_MH_date,ed_invoicing_party,ed_survey_name,ed_survey_completion_date,ed_completion_date,ed_actual_MH_time,ed_survey_remark,ed_completion_time;
    private String  SurveyCompletionDate,SurveyorName,InvoicingPartyCD,RepairEstimateID,InvoicingPartyName,InvoicingPartyID,bv_strCustomerID,Remarks;
    private String invoice_name,get_sp_invoice_code;
    private String totale_amount;
    private TextView tv_amount;
    private TextView tv_sur_completion_date,tv_survey_name,repair_estimate_text;
    private Button repair_completion,repair_estimate,repair_approval,survey_completion;
    private RelativeLayout RL_heating,RL_Repair;
    private ImageView more_info;
    private String remark,Survey_CompletionDate,survey_name;
    private ImageView line_item_id,iv_send_mail;
    private TextView tv_totalCost;
    private String current_date;
    private Date date2,date1;
    private String last_status_date;
    private Date last_status_date1;
    private String customer_Id;
    private String ReportURL;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.add_survey_completion);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

        totale_amount=GlobalConstants.totale_amount;
        tv_amount = (TextView) findViewById(R.id.tv_amount);
        tv_amount.setText(GlobalConstants.totale_amount);

        last_status_date= GlobalConstants.lastStatusDate;
        customer_Id= GlobalConstants.customer_Id;
        try {
            last_status_date1 = new SimpleDateFormat("dd-MM-yyyy").parse(last_status_date);
        }catch (Exception e)
        {

        }
        tv_amount.setOnClickListener(this);
        iv_send_mail = (ImageView) findViewById(R.id.iv_send_mail);
        line_item_id = (ImageView) findViewById(R.id.line_item_id);
        line_item_id.setOnClickListener(this);
        iv_send_mail.setOnClickListener(this);
        LL_add_completion_details = (LinearLayout) findViewById(R.id.LL_add_completion_details);
        LL_add_completion_details.setVisibility(View.GONE);
        repair_estimate_text = (TextView)findViewById(R.id.tv_heating);
        tv_survey_name = (TextView)findViewById(R.id.tv_survey_name);
        tv_sur_completion_date = (TextView)findViewById(R.id.tv_sur_completion_date);
        tv_totalCost = (TextView)findViewById(R.id.tv_totalCost);
        tv_totalCost.setText(totale_amount);
        tv_totalCost.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                im_skip.performClick();
            }
        });
        repair_estimate_text.setText("Survey Completion");
        sp_invoicing_party=(Spinner)findViewById(R.id.sp_invoicing_party);
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setOnClickListener(this);
        iv_changeOfStatus = (ImageView) findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        im_skip = (ImageView) findViewById(R.id.im_skip);
        im_skip.setVisibility(View.GONE);
        im_skip.setOnClickListener(this);
        bt_refresh = (Button) findViewById(R.id.heat_refresh);
        bt_refresh.setOnClickListener(this);
        menu.setVisibility(View.GONE);
        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_heating.setVisibility(View.GONE);
        ed_completion_time=(EditText)findViewById(R.id.ed_completion_time);
        ed_survey_remark=(EditText)findViewById(R.id.ed_survey_remark);
        ed_completion_date=(EditText)findViewById(R.id.ed_completion_date);
        ed_survey_completion_date=(EditText)findViewById(R.id.ed_survey_completion_date);


        ed_survey_name=(EditText)findViewById(R.id.ed_survey_name);
        ed_invoicing_party=(EditText)findViewById(R.id.ed_invoicing_party);
        ed_survey_completion_date.setOnClickListener(this);
        ed_actual_MH_time=(EditText)findViewById(R.id.ed_actual_MH_time);
        ed_MH_date=(EditText)findViewById(R.id.ed_MH_date);

        ed_MH_date.setOnClickListener(this);
        ed_actual_MH_time.setOnClickListener(this);
        ed_completion_date.setOnClickListener(this);
        ed_completion_time.setOnClickListener(this);


        LL_Line_iteam = (Button) findViewById(R.id.Line_iteam);
        LL_Line = (LinearLayout) findViewById(R.id.LL_Line_iteam);
        LL_add_details = (Button) findViewById(R.id.add_details);
        LL_add_details_back = (LinearLayout) findViewById(R.id.LL_add_details);
        LL_add_details_back.setBackgroundColor(getResources().getColor(R.color.submit));

        LL_add_details.setOnClickListener(this);
//        LL_attachments = (Button) findViewById(R.id.attachments);
//        LL_attachments.setOnClickListener(this);
        LL_Line_iteam.setOnClickListener(this);

        LL_summary=(Button)findViewById(R.id.summary);
        LL_summary.setOnClickListener(this);
        LL_summary.setVisibility(View.GONE);
        heat_home = (Button) findViewById(R.id.heat_home);
        heat_refresh = (Button) findViewById(R.id.heat_refresh);
        heat_submit = (Button) findViewById(R.id.heat_submit);
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);
        more_info = (ImageView) findViewById(R.id.more_info);
        more_info.setOnClickListener(this);
        LL_heat_submit.setClickable(false);
        LL_heat = (LinearLayout)findViewById(R.id.LL_heat);
        LL_heat.setOnClickListener(this);
        LL_heat_submit.setAlpha(0.5f);
        LL_heat_submit.setClickable(false);

//        tabLayout.setupWithViewPager(viewPager);


       // tabLayout.clearOnTabSelectedListeners();

        repair_estimate = (Button)findViewById(R.id.repair_estimate);
        repair_estimate.setOnClickListener(this);
        repair_approval = (Button)findViewById(R.id.repair_approval);
        repair_approval.setVisibility(View.GONE);
        repair_completion = (Button)findViewById(R.id.repair_completion);
        repair_completion.setVisibility(View.GONE);
        survey_completion = (Button)findViewById(R.id.survey_completion);
        survey_completion.setVisibility(View.GONE);

        heating = (Button)findViewById(R.id.heating);
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        inspection.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);
        String surName = getColoredSpanned("*","#cb0da5");
        String lable_Status = getColoredSpanned("Survey Completion date","#bbbbbb");
        String lable_Name = getColoredSpanned("Survey Name","#bbbbbb");


        tv_sur_completion_date.setText(Html.fromHtml(lable_Status+" "+surName));
        tv_survey_name.setText(Html.fromHtml(lable_Name+" "+surName));


      /*  heat_refresh.setOnClickListener(this);
        heat_home.setOnClickListener(this);*/
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        LL_heat_submit.setAlpha(0.5f);
        heat_submit.setAlpha(0.5f);
        LL_heat_submit.setClickable(false);
        Log.i("customer_name",GlobalConstants.customer_name);
        customer_name = (TextView) findViewById(R.id.text1);
        next_test_type_code = (TextView) findViewById(R.id.code);
        customer_name.setText(GlobalConstants.customer_name+" , "+GlobalConstants.equipment_no );
        equipment_no = (TextView) findViewById(R.id.text2);
//        equipment_no.setText(GlobalConstants.equipment_no);
        eqip_type = (TextView) findViewById(R.id.text3);
//        eqip_type.setText(GlobalConstants.previous_cargo);
        tv_toolbarTitle.setText("Add Details");

        drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
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

        ed_MH_date.setText(systemDate);
        ed_completion_date.setText(systemDate);
        ed_survey_completion_date.setText(systemDate);
        ed_actual_MH_time.setText(curTime);
        ed_completion_time.setText(curTime);

        iv_send_mail.setVisibility(View.VISIBLE);

        if(cd.isConnectingToInternet())
        {
            if( GlobalConstants.Survey_Invoice_party > 0) {
                new InvoiceParty_details().execute();
            }

            if((GlobalConstants.from.equalsIgnoreCase("MysubmitAttach") || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                    ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails") )) {
              if(GlobalConstants.summaryfrom.equalsIgnoreCase("summaryfrom")) {

                  ed_survey_completion_date.setText(GlobalConstants.Survey_CompletionDate);
                  ed_survey_remark.setText(GlobalConstants.remark);
                  ed_survey_name.setText(GlobalConstants.Surveyor_name);

              }else
              {
                  new Get_Repair_MySubmit_details().toString();
                  ed_MH_date.setText(systemDate);
                  ed_completion_date.setText(systemDate);
                  ed_survey_completion_date.setText(systemDate);
                  ed_actual_MH_time.setText(curTime);
                  ed_completion_time.setText(curTime);

              }

            }else
            {
                if(GlobalConstants.summaryfrom.equalsIgnoreCase("summaryfrom")) {

                    ed_survey_completion_date.setText(GlobalConstants.Survey_CompletionDate);
                    ed_survey_remark.setText(GlobalConstants.remark);
                    ed_survey_name.setText(GlobalConstants.Surveyor_name);

                }else
                {
                    new Get_Repair_MySubmit_details().toString();
                    ed_MH_date.setText(systemDate);
                    ed_completion_date.setText(systemDate);
                    ed_survey_completion_date.setText(systemDate);
                    ed_actual_MH_time.setText(curTime);
                    ed_completion_time.setText(curTime);

                }

            }

        }else
        {
            shortToast(getApplicationContext(),"Please Check Your InterNet");
        }



        sp_invoicing_party.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {

                invoice_name = sp_invoicing_party.getSelectedItem().toString();
                get_sp_invoice_code = CustomerDropdown_Invoice_ArrayList.get(i).getCode();
                ed_invoicing_party.setText(invoice_name);

            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });




        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        menu.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (drawer.isDrawerOpen(
                        Gravity.START))
                    drawer.closeDrawer(Gravity.END);
                else
                    drawer.openDrawer(Gravity.START);
            }
        });



    }





    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(), MainActivity.class));
                break;
            case R.id.line_item_id:
                LL_Line_iteam.performClick();
                break;
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.repair_estimate:
                finish();
                startActivity(new Intent(getApplicationContext(),Repair_MainActivity.class));
                break;

            case R.id.Line_iteam:
                finish();
                startActivity(new Intent(getApplicationContext(),Survey_Completion_wizard.class));

                break;
            case R.id.more_info:

                popUp_equipment_info(GlobalConstants.equipment_no,GlobalConstants.status,GlobalConstants.status_id,GlobalConstants.previous_cargo,"","","","");


                break;
            case R.id.tv_amount:
                LL_summary.performClick();

                break;
            case R.id.iv_back:
                onBackPressed();
                break;
            case R.id.im_skip:
                survey_name=ed_survey_name.getText().toString();
                Survey_CompletionDate=ed_survey_completion_date.getText().toString();
                remark=ed_survey_remark.getText().toString();
                try {
                    current_date = new SimpleDateFormat("dd-MM-yyyy").format(new Date());
                    SimpleDateFormat sdf = new SimpleDateFormat("dd-MM-yyyy");
                    date1 = sdf.parse(ed_survey_completion_date.getText().toString());
                    date2 = sdf.parse(current_date);
                    String time = ed_completion_time.getText().toString();
    /* historyDate <= todayDate <= futureDate */

                    if ((date1.compareTo(date2) > 0)) {
                        shortToast(getApplicationContext(), "cannot select future date");
                    }else {
//                        if((survey_name.length()>0)||Survey_CompletionDate.equals("") ){
                            if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                    || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                    || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                                GlobalConstants.from = "MysubmitAddDetails";
                            } else {
                                GlobalConstants.from = "AddDetails";
                            }
                            finish();
                            GlobalConstants.totale_amount = totale_amount;
                            GlobalConstants.Surveyor_name = survey_name;
                            GlobalConstants.Survey_CompletionDate =Survey_CompletionDate;
                            GlobalConstants.remark = remark;
                            GlobalConstants.customer_Id = customer_Id;

                            GlobalConstants.repairEstimateId = RepairEstimateID;
                            String est_id=GlobalConstants.repair_EstimateID;
                            String invoice_prty_cd=GlobalConstants.invoice_PartyCD;
                            String invoice_prty_id=GlobalConstants.invoice_PartyID;
                            GlobalConstants.invoice_PartyCD=invoice_prty_cd;
                            GlobalConstants.invoice_PartyID=invoice_prty_id;
                            String est_no=GlobalConstants.repairEstimateNo;
                            String gi_transaction_no= GlobalConstants.gi_trans_no;
                            GlobalConstants.gi_trans_no=gi_transaction_no;
                            GlobalConstants.repair_EstimateID=est_id;
                            GlobalConstants.repair_EstimateNo=est_no;

                            startActivity(new Intent(getApplicationContext(), SurveyCompletion_Summary.class));
//                        }else
//                        {
//                            shortToast(getApplicationContext(),"Survey Name Required");
//                        }
                    }
                }catch (Exception e)
                {
                    Log.i("Exception", String.valueOf(e));
                }
                break;

            case R.id.ed_repair_type:
                sp_repair_type.performClick();

                break;
            case R.id.summary:
                 survey_name=ed_survey_name.getText().toString();
                Survey_CompletionDate=ed_survey_completion_date.getText().toString();
                remark=ed_survey_remark.getText().toString();

                current_date = new SimpleDateFormat("dd-MM-yyyy").format(new Date());
                try {
                    SimpleDateFormat sdf = new SimpleDateFormat("dd-MM-yyyy");
                     date1 = sdf.parse(ed_survey_completion_date.getText().toString());
                     date2 = sdf.parse(current_date);
                    String time = ed_completion_time.getText().toString();
    /* historyDate <= todayDate <= futureDate */

                if ((date1.compareTo(date2) > 0)) {
                    shortToast(getApplicationContext(), "cannot select future date");
                }else {
//                    if((survey_name.length()>0)||Survey_CompletionDate.equals("") ){
                        if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                            GlobalConstants.from = "MysubmitAddDetails";
                        } else {
                            GlobalConstants.from = "AddDetails";
                        }
                        finish();
                        GlobalConstants.totale_amount = totale_amount;
                        GlobalConstants.Surveyor_name = survey_name;
                        GlobalConstants.Survey_CompletionDate =Survey_CompletionDate;
                        GlobalConstants.remark = remark;
                        String est_id=GlobalConstants.repair_EstimateID;
                        String est_no=GlobalConstants.repairEstimateNo;
                        String gi_transaction_no= GlobalConstants.gi_trans_no;
                        GlobalConstants.gi_trans_no=gi_transaction_no;
                        GlobalConstants.repair_EstimateID=est_id;
                        String invoice_prty_cd=GlobalConstants.invoice_PartyCD;
                        String invoice_prty_id=GlobalConstants.invoice_PartyID;
                        GlobalConstants.invoice_PartyCD=invoice_prty_cd;
                        GlobalConstants.invoice_PartyID=invoice_prty_id;
                        GlobalConstants.repair_EstimateNo=est_no;
                        GlobalConstants.customer_Id = customer_Id;
                         invoice_prty_cd=GlobalConstants.invoice_PartyCD;
                         invoice_prty_id=GlobalConstants.invoice_PartyID;
                        GlobalConstants.invoice_PartyCD=invoice_prty_cd;
                        GlobalConstants.invoice_PartyID=invoice_prty_id;
                        GlobalConstants.repairEstimateId = RepairEstimateID;

                        startActivity(new Intent(getApplicationContext(), SurveyCompletion_Summary.class));
//                    }else
//                    {
//                        shortToast(getApplicationContext(),"Survey Name Required");
//                    }
                }
                }catch (Exception e)
                {
                    Log.i("Exception", String.valueOf(e));
                }
                break;

            case R.id.iv_changeOfStatus:
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));

                break;
            case R.id.ed_invoicing_party:
                sp_invoicing_party.performClick();
                break;



            case R.id.ed_survey_completion_date:
                survey_date=true;
                completion_date=false;
                MH_date=false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_actual_MH_time:

                mHour = c.get(Calendar.HOUR_OF_DAY);
                mMinute = c.get(Calendar.MINUTE);

                // Launch Time Picker Dialog
                timePickerDialog = new TimePickerDialog(this,
                        new TimePickerDialog.OnTimeSetListener() {

                            @Override
                            public void onTimeSet(TimePicker view, int hourOfDay,
                                                  int minute) {
                                boolean isPM = (hourOfDay >= 12);
                                ed_actual_MH_time.setText(String.format("%02d:%02d %s", (hourOfDay == 12 || hourOfDay == 0) ? 12 : hourOfDay % 12,minute, isPM ? "PM" : "AM"));
                                //   from_time.setText(hourOfDay + ":" + minute);
                            }
                        }, mHour, mMinute, false);
                timePickerDialog.show();


                break;
            case R.id.iv_send_mail:
                if (cd.isConnectingToInternet()) {

                    // Survey Completion
                    // Repair Approval
                    new Print_pdf().execute();

                }else{
                    shortToast(getApplicationContext(), "Please check your Internet Connection.");
                }


           /*  *//*   GlobalConstants.print_string= String.valueOf(print_object);
                startActivity(new Intent(getApplicationContext(),SendMailActivity.class));*//*
                try {
                    String targetPdf = "/sdcard/test.pdf";

                    String sendemail = "";
                    Intent email = new Intent(Intent.ACTION_SEND);
                    email.putExtra(Intent.EXTRA_EMAIL, new String[]{ sendemail});
                    email.putExtra(Intent.EXTRA_SUBJECT, "Repair Estimate");
                    email.setType("message/rfc822");
                    if (targetPdf != null) {
                        email.putExtra(Intent.EXTRA_STREAM, targetPdf);
                    }
                    startActivity(Intent.createChooser(email, "Select Email Client"));



                } catch (Throwable t) {
                    Toast.makeText(this,
                            "Request failed try again: " + t.toString(),
                            Toast.LENGTH_LONG).show();
                }
*/
                break;
            case R.id.im_completion_time:

                mHour = c.get(Calendar.HOUR_OF_DAY);
                mMinute = c.get(Calendar.MINUTE);

                // Launch Time Picker Dialog
                timePickerDialog = new TimePickerDialog(this,
                        new TimePickerDialog.OnTimeSetListener() {

                            @Override
                            public void onTimeSet(TimePicker view, int hourOfDay,
                                                  int minute) {
                                boolean isPM = (hourOfDay >= 12);
                                ed_completion_time.setText(String.format("%02d:%02d %s", (hourOfDay == 12 || hourOfDay == 0) ? 12 : hourOfDay % 12,minute, isPM ? "PM" : "AM"));
                                //   from_time.setText(hourOfDay + ":" + minute);
                            }
                        }, mHour, mMinute, false);
                timePickerDialog.show();

                break;

        }

    }












    @Override
    public void onPause() {
        super.onPause();

        if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();
        progressDialog = null;
    }

    @Override
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

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

                if(survey_date) {
                    dialog.getDatePicker().setMinDate(last_status_date1.getTime());
                }

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
        SimpleDateFormat sdf = new SimpleDateFormat("dd-MM-yyyy");

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



            ed_survey_completion_date.setText(formatDate(year, month, day));


            //    System.out.println("am a new from date====>>" + str_From);

            //  date.setText(formatDate(year, month, day));

        }
    };


    public class Get_Repair_MySubmit_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray LineItems_Json;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Add_Survey_Completion_Details.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLSurveyCompletionList);
            httpPost.setHeader("Content-Type", "application/json");

            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                if(GlobalConstants.from.equalsIgnoreCase("MysubmitAttach") || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") )

                {
                    jsonObject.put("Mode","edit");

                }else
                {
                    jsonObject.put("Mode","new");

                }
                jsonObject.put("PageName","Survey Completion");

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("Response");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list"+jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found");
                            }
                        });
                    }else {


                        for (int j = 0; j < jsonarray.length(); j++) {

                            jsonObject = jsonarray.getJSONObject(j);

                            if(GlobalConstants.equipment_no.equalsIgnoreCase(jsonObject.getString("EquipmentNo")))
                            {

                                    jsonObject = jsonarray.getJSONObject(j);

          /* "EquipmentNo": "PRAS0976544","Customer": "TSTCUST","InDate": "1-9-2017 00:00:00","PreviousCargo": "Acetic Acid",
        "LastStatusDate": "6-6-2017 00:00:00","LaborRate": "0.00",
        "LastTestType": "51", "NextTestType": "","LastTestDate": "09-Jan-2017","NextTestDate": "","LastSurveyor": "",
        "ValidityPeriodforTest": "2.5","RepairTypeID": "55","RepairTypeCD": "Regular Repair","Remarks": "", "InvoicingPartyCD": "",
        "InvoicingPartyID": "","InvoicingPartyName": "","GiTransactionNo": "8574","RepairEstimateID": "13747","RevisionNo": "0",
        "CustomerAppRef": "","ApprovalDate": "6-6-2017 00:00:00","PartyAppRef": "","SurveyorName": "",
        "SurveyCompletionDate": "6-6-2017 00:00:00","LineItems": null,"attchement": [],"RepairEstimateNo": "9233",
        "EquipmentStatusId": "9","EquipmentStatusCd": "AAR"*/

                                 SurveyCompletionDate=jsonObject.getString("SurveyCompletionDate");
                                 SurveyorName=jsonObject.getString("SurveyorName");
                                 InvoicingPartyCD=jsonObject.getString("InvoicingPartyCD");
                                 InvoicingPartyID=jsonObject.getString("InvoicingPartyID");
                                InvoicingPartyName=jsonObject.getString("InvoicingPartyName");
                                RepairEstimateID=jsonObject.getString("RepairEstimateID");
                                 Remarks=jsonObject.getString("Remarks");
                                bv_strCustomerID=jsonObject.getString("CSTMR_ID");

                                System.out.println("SurveyCompletionDate"+SurveyCompletionDate);

                            }



                        }
                    }
                }else if(jsonarray.length()<1){
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


                progressDialog.dismiss();
            if(jsonarray!=null)
            {
                String[] Part1 = SurveyCompletionDate.split(" ");
                String SurveyCompletion_date = Part1[0];
                ed_survey_completion_date.setText(SurveyCompletion_date);
                ed_survey_name.setText(SurveyorName);
                if( GlobalConstants.Survey_Invoice_party > 0) {
                    if (InvoicingPartyName.equals(null) || InvoicingPartyName.equalsIgnoreCase("")) {

                    } else {
                        ed_invoicing_party.setText(InvoicingPartyName);

                    }

                }

            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }
        }

    }

    public class InvoiceParty_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Add_Survey_Completion_Details.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLInvoice_Party);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("StatusName","");


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

                    System.out.println("Am HashMap list" + jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found.");
                            }
                        });
                    } else {

                        dropdown_customer_list = new ArrayList<>();


                        Invoice_worldlist = new ArrayList<String>();
                        CustomerDropdown_Invoice_ArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            customer_DropdownBean.setName(jsonObject.getString("INVCNG_PRTY_CD"));
                            customer_DropdownBean.setCode(jsonObject.getString("INVCNG_PRTY_ID"));
                            RepairType_Name = jsonObject.getString("INVCNG_PRTY_CD");
                            RepairType_Code = jsonObject.getString("INVCNG_PRTY_ID");

                            String[] set1 = new String[2];
                            set1[0] = RepairType_Name;
                            set1[1] = RepairType_Code;
                            dropdown_customer_list.add(set1);
                            Cust_name.add(set1[0]);
                            Cust_code.add(set1[1]);
                            CustomerDropdown_Invoice_ArrayList.add(customer_DropdownBean);
                            Invoice_worldlist.add(RepairType_Name);


                        }
                    }
                } else if (jsonarray.length() < 1) {
                    runOnUiThread(new Runnable() {

                        @Override
                        public void run() {
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(), "No Records Found.");


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


            if (dropdown_customer_list != null) {
                ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, Invoice_worldlist);
                CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_invoicing_party.setAdapter(CustomerAdapter);

            } else {
                shortToast(getApplicationContext(), "Data Not Found");

            }

            progressDialog.dismiss();

        }

    }

    public class Print_pdf extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        private JSONObject invitejsonObject;
        private JSONObject jsonObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Add_Survey_Completion_Details.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURL_RepairEstimatePrintDocument);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                jsonObject = new JSONObject();

              /*
                               {
               "pageName":"Repair Estimate",
                "activityID":"8493",
                "repairEstimateID":"13718",
                "UserName":"ADMIN",
                "depotid":"1"

                }*/

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("pageName","Survey Completion");
                jsonObject.put("repairEstimateID",GlobalConstants.repair_EstimateID);
                jsonObject.put("activityID",GlobalConstants.gi_trans_no);
                jsonObject.put("depotid","1");



                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("Update request", jsonObject.toString());
                Log.d("Update responce", resp);
                JSONObject jsonResp = new JSONObject(resp);


              /*  JSONObject returnMessage = jsonResp.getJSONObject("d");
                String message = returnMessage.getString("Status");*/
                ReportURL = jsonResp.getString("d");

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
            if (ReportURL != null) {

                if(ReportURL.contains(".pdf"))
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

}
