package com.i_tankdepo;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.app.TimePickerDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.FragmentActivity;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.text.Html;
import android.text.InputFilter;
import android.text.method.DigitsKeyListener;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
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
import com.i_tankdepo.Beanclass.Image_Bean;
import com.i_tankdepo.Beanclass.LineItem_Bean;
import com.i_tankdepo.Constants.GlobalConstants;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

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

public class AddRepair_Completion_Details_MySubmit extends CommonActivity implements View.OnClickListener {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView iv_changeOfStatus,im_original_est_endDate,menu, im_down, im_up, im_heat_close, im_heat_ok, im_skip,iv_back;


    private TextView tv_toolbarTitle, tv_search_options,no_data;
    LinearLayout LL_hole, LL_heat_submit,LL_search_Value,LL_heat;

    private ProgressDialog progressDialog;

    private Intent mServiceIntent;

    private Button heat_refresh, heat_home, heat_submit,heating,cleaning,inspection;
    private FragmentActivity mycontaxt;
    private TextView next_test_type_code,customer_name,tv_completion_time,tv_completion_date,equipment_no,eqip_type;
    private Button LL_Line_iteam,bt_refresh,LL_attachments,LL_summary;
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
    private boolean MH_date=false,MH_time=false,completion_date=false,completion_time=false;
    private LinearLayout LL_survey_completion,LL_Line;
    private Button LL_add_details;
    private LinearLayout LL_add_details_back;
    private int mHour,mMinute;
    private TimePickerDialog timePickerDialog;
    private EditText ed_MH_date,ed_completion_date,ed_temp,remark,ed_actual_MH_time,ed_completion_time;
    private String get_MH, get_Actual_MH,get_CMT_date,get_time,get_remark,get_location;
    private String status;
    private ArrayList<LineItem_Bean> lineitem_arraylist;
    private boolean survey_date;
    private TextView repair_estimate_text;
    private Button repair_completion,repair_estimate,repair_approval,survey_completion;
    private RelativeLayout RL_heating,RL_Repair;
    private ImageView more_info;
    private ImageView iv_send_mail;
    private Pattern pattern;
    private Matcher matcher;
    private static final String TIME12HOURS_PATTERN = "([01]?[0-9]|2[0-3]):[0-5][0-9]";
    private String Line_item_Json;
    private ArrayList<Image_Bean> encodeArray;
    private Button notification_text;
    private ImageView line_item;
    private Date Activity_Date1;
    private String Activity_Date;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.add_repair_details_completion);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);


        Line_item_Json=GlobalConstants.Line_item_Json;
        lineitem_arraylist=GlobalConstants.lineItem_beanArrayList;
        more_info = (ImageView) findViewById(R.id.more_info);
        more_info.setOnClickListener(this);
        LL_survey_completion = (LinearLayout) findViewById(R.id.LL_survey_completion);
        LL_survey_completion.setVisibility(View.GONE);
        Activity_Date= GlobalConstants.ActivityDate;
        try {
            Activity_Date1 = new SimpleDateFormat("dd-MM-yyyy").parse(Activity_Date);
        }catch (Exception e)
        {

        }


        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setOnClickListener(this);
        iv_changeOfStatus = (ImageView) findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        im_skip = (ImageView) findViewById(R.id.im_skip);
        im_skip.setOnClickListener(this);
        bt_refresh = (Button) findViewById(R.id.heat_refresh);
        bt_refresh.setOnClickListener(this);
        menu.setVisibility(View.GONE);
        notification_text = (Button) findViewById(R.id.notification_text);

        LL_attachments=(Button)findViewById(R.id.attachments);
        ed_completion_time=(EditText)findViewById(R.id.ed_completion_time);
        ed_completion_date=(EditText)findViewById(R.id.ed_completion_date);
        ed_actual_MH_time=(EditText)findViewById(R.id.ed_actual_MH_time);
        ed_actual_MH_time.setFilters(new InputFilter[] { new InputFilter.LengthFilter(12) });
        ed_actual_MH_time.setKeyListener(DigitsKeyListener.getInstance(true,true));
        ed_temp=(EditText)findViewById(R.id.ed_temp);
        remark=(EditText)findViewById(R.id.remark);
        ed_MH_date=(EditText)findViewById(R.id.ed_MH_date);
        tv_completion_date=(TextView)findViewById(R.id.tv_completion_date);
        tv_completion_time=(TextView)findViewById(R.id.tv_completion_time);

        /*  String act_mh=GlobalConstants.actual_mh;
                String time=GlobalConstants.time;
                String remarks=GlobalConstants.remark;
                GlobalConstants.actual_mh=act_mh;
                GlobalConstants.time=time;
                GlobalConstants.remark=remarks;*/

        String surName = getColoredSpanned("*","#cb0da5");
        String lable_com_date = getColoredSpanned("Completion Date","#bbbbbb");
        String lable_com_time = getColoredSpanned("Completion Time","#bbbbbb");
        tv_completion_date.setText(Html.fromHtml(lable_com_date+" "+surName));
        tv_completion_time.setText(Html.fromHtml(lable_com_time+" "+surName));
        ed_MH_date.setOnClickListener(this);
        ed_actual_MH_time.setOnClickListener(this);
        ed_completion_date.setOnClickListener(this);
        ed_completion_time.setOnClickListener(this);

        encodeArray=GlobalConstants.multiple_encodeArray;

        try {
            if(GlobalConstants.attach_count!=null)
            {
                notification_text.setText((GlobalConstants.attach_count));

            }else
            {
//                notification_text.setText((GlobalConstants.attach_count));
                notification_text.setText(String.valueOf(encodeArray.size()));

            }

        } catch (Exception e) {
            notification_text.setText((GlobalConstants.attach_count));
        }
        LL_Line_iteam = (Button) findViewById(R.id.Line_iteam);
        line_item = (ImageView) findViewById(R.id.line_item);
        line_item.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_Line_iteam.performClick();
            }
        });
        LL_Line = (LinearLayout) findViewById(R.id.LL_Line_iteam);
        LL_add_details = (Button) findViewById(R.id.add_details);
        LL_add_details_back = (LinearLayout) findViewById(R.id.LL_add_details);
        LL_add_details_back.setBackgroundColor(getResources().getColor(R.color.submit));

        LL_add_details.setOnClickListener(this);
        LL_attachments = (Button) findViewById(R.id.attachments);
        LL_attachments.setOnClickListener(this);
        LL_Line_iteam.setOnClickListener(this);

       // LL_summary=(Button)findViewById(R.id.summary);
      //  LL_summary.setOnClickListener(this);
        heat_home = (Button) findViewById(R.id.heat_home);
        heat_refresh = (Button) findViewById(R.id.heat_refresh);
        heat_submit = (Button) findViewById(R.id.heat_submit);
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);
        repair_estimate_text = (TextView)findViewById(R.id.tv_heating);
        repair_estimate_text.setText("Repair Completion");
        LL_heat = (LinearLayout)findViewById(R.id.LL_heat);
        LL_heat.setOnClickListener(this);
        LL_heat_submit.setAlpha(0.5f);
        LL_heat_submit.setClickable(false);
        iv_send_mail = (ImageView) findViewById(R.id.iv_send_mail);
        iv_send_mail.setOnClickListener(this);
        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_heating.setVisibility(View.GONE);
        repair_estimate = (Button)findViewById(R.id.repair_estimate);
        repair_estimate.setOnClickListener(this);
        repair_approval = (Button)findViewById(R.id.repair_approval);
        repair_approval.setVisibility(View.GONE);
        repair_completion = (Button)findViewById(R.id.repair_completion);
        repair_completion.setVisibility(View.GONE);
        survey_completion = (Button)findViewById(R.id.survey_completion);
        survey_completion.setVisibility(View.GONE);
//        tabLayout.setupWithViewPager(viewPager);


       // tabLayout.clearOnTabSelectedListeners();


        heating = (Button)findViewById(R.id.heating);
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        inspection.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);

        heat_refresh.setOnClickListener(this);
        heat_home.setOnClickListener(this);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);

        Log.i("customer_name",GlobalConstants.customer_name);
        customer_name = (TextView) findViewById(R.id.text1);
        next_test_type_code = (TextView) findViewById(R.id.code);
        customer_name.setText(GlobalConstants.customer_name + " , " + GlobalConstants.equipment_no + " , " + GlobalConstants.type);
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



            //   repair_arraylist=GlobalConstants.repair_arraylist;
            if(cd.isConnectingToInternet())
            {


                ed_completion_date.setText(GlobalConstants.completion_date);
                ed_actual_MH_time.setText(GlobalConstants.actual_manHr);
                remark.setText(GlobalConstants.remark);
                ed_MH_date.setText(GlobalConstants.estimated_manHr);
                ed_temp.setText(GlobalConstants.location);
                ed_completion_time.setText(GlobalConstants.time);
            }else
            {
                shortToast(getApplicationContext(), "Please check your Internet Connection.");
            }





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
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.more_info:

                popUp_equipment_info(GlobalConstants.equipment_no,GlobalConstants.status,GlobalConstants.status_id,GlobalConstants.previous_cargo,"","","","");


                break;
            case R.id.repair_estimate:
                finish();
                startActivity(new Intent(getApplicationContext(),Repair_MainActivity.class));
                break;
            case R.id.iv_send_mail:

             /*   GlobalConstants.print_string= String.valueOf(print_object);
                startActivity(new Intent(getApplicationContext(),SendMailActivity.class));*/
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

                break;
            case R.id.ed_completion_time:
                mHour = c.get(Calendar.HOUR_OF_DAY);
                mMinute = c.get(Calendar.MINUTE);

                // Launch Time Picker Dialog
                timePickerDialog = new TimePickerDialog(this,
                        new TimePickerDialog.OnTimeSetListener() {

                            @Override
                            public void onTimeSet(TimePicker view, int hourOfDay,
                                                  int minute) {
                                boolean isPM = (hourOfDay >= 12);
                                ed_completion_time.setText(String.format("%02d:%02d", (hourOfDay == 12 || hourOfDay == 0) ? 12 : hourOfDay % 12,minute));
                                //   from_time.setText(hourOfDay + ":" + minute);
                            }
                        }, mHour, mMinute, false);
                timePickerDialog.show();
                break;
            case R.id.Line_iteam:

//                GlobalConstants.customer_Id=customer_Id;
                status=GlobalConstants.status;
                GlobalConstants.status=status;
                GlobalConstants.actual_manHr=ed_actual_MH_time.getText().toString();
                GlobalConstants.estimated_manHr=ed_MH_date.getText().toString();
                GlobalConstants.completion_date=ed_completion_date.getText().toString();
                GlobalConstants.time=ed_completion_time.getText().toString();
                GlobalConstants.remark=remark.getText().toString();
                String count=GlobalConstants.attach_count;
                GlobalConstants.attach_count=count;
                GlobalConstants.location=ed_temp.getText().toString();
                String equipment_no = GlobalConstants.equipment_no;
                GlobalConstants.equipment_no = equipment_no;
                GlobalConstants.lineItem_beanArrayList=lineitem_arraylist;
                if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                    GlobalConstants.from="MysubmitAddDetails";
                }else
                {
                    GlobalConstants.from="AddDetails";
                }
                finish();
                startActivity(new Intent(getApplicationContext(),Repair_Completions_wizard_MySubmit.class));

                break;
            case R.id.iv_back:
                onBackPressed();
                break;
            case R.id.im_skip:


              /*  if (ed_completion_date.getText().toString().equals("") ||ed_completion_time.getText().toString().equals("") )
                {
                    shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                }else {*/
                    try {
                        current_date = new SimpleDateFormat("dd-MMM-yyyy").format(new Date());

                        SimpleDateFormat sdf = new SimpleDateFormat("dd-MMM-yyyy");
                        Date date1 = sdf.parse(ed_completion_date.getText().toString());
                        Date date2 = sdf.parse(current_date);
                        String time = ed_completion_time.getText().toString();
    /* historyDate <= todayDate <= futureDate */

                        if ((date1.compareTo(date2) > 0)) {
                            shortToast(getApplicationContext(), "cannot select future date");
                        } else if (!time.matches(TIME12HOURS_PATTERN)) {
                            shortToast(getApplicationContext(), "Completion Time in wrong format");

                        } else {
                            finish();
//                GlobalConstants.customer_Id=customer_Id;
                            status = GlobalConstants.status;
                            GlobalConstants.status = status;
                            GlobalConstants.actual_manHr=ed_actual_MH_time.getText().toString();
                            GlobalConstants.estimated_manHr=ed_MH_date.getText().toString();
                            GlobalConstants.completion_date=ed_completion_date.getText().toString();
                            GlobalConstants.time=ed_completion_time.getText().toString();
                            GlobalConstants.remark=remark.getText().toString();
                            GlobalConstants.location=ed_temp.getText().toString();
                            GlobalConstants.lineItem_beanArrayList = lineitem_arraylist;
                            equipment_no = GlobalConstants.equipment_no;
                            GlobalConstants.equipment_no = equipment_no;
                            GlobalConstants.Line_item_Json = Line_item_Json;
                            if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                    || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                    || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                                GlobalConstants.from = "MysubmitAddDetails";
                            } else {
                                GlobalConstants.from = "AddDetails";
                            }
                            startActivity(new Intent(getApplicationContext(), Attach_Repair_Completion_MySubmit.class));

                        }
                    } catch (Exception e) {
                        Log.i("Exception", String.valueOf(e));
                    }
//                }

                break;

            case R.id.ed_repair_type:
                sp_repair_type.performClick();

                break;
            case R.id.ed_completion_date:
                completion_date=true;
                MH_date=false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.attachments:
              /*  if (ed_completion_date.getText().toString().equals("") ||ed_completion_time.getText().toString().equals("") )
                {
                    shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                }else {*/
                    try {
                        current_date = new SimpleDateFormat("dd-MMM-yyyy").format(new Date());

                        SimpleDateFormat sdf = new SimpleDateFormat("dd-MMM-yyyy");
                        Date date1 = sdf.parse(ed_completion_date.getText().toString());
                        Date date2 = sdf.parse(current_date);
                        String time = ed_completion_time.getText().toString();
    /* historyDate <= todayDate <= futureDate */

                        if ((date1.compareTo(date2) > 0)) {
                            shortToast(getApplicationContext(), "cannot select future date");
                        } else if (!time.matches(TIME12HOURS_PATTERN)) {
                            shortToast(getApplicationContext(), "Completion Time in wrong format");

                        } else {
                            if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                    || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                    || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                                GlobalConstants.from = "MysubmitAddDetails";
                            } else {
                                GlobalConstants.from = "AddDetails";
                            }

                            finish();
                            //                GlobalConstants.customer_Id=customer_Id;
                            status = GlobalConstants.status;
                            GlobalConstants.status = status;
                            GlobalConstants.actual_manHr=ed_actual_MH_time.getText().toString();
                            GlobalConstants.estimated_manHr=ed_MH_date.getText().toString();
                            GlobalConstants.completion_date=ed_completion_date.getText().toString();
                            GlobalConstants.time=ed_completion_time.getText().toString();
                            GlobalConstants.remark=remark.getText().toString();
                            GlobalConstants.location=ed_temp.getText().toString();
                            GlobalConstants.lineItem_beanArrayList = lineitem_arraylist;
                            GlobalConstants.Line_item_Json = Line_item_Json;
                            equipment_no = GlobalConstants.equipment_no;
                            GlobalConstants.equipment_no = equipment_no;
                            startActivity(new Intent(getApplicationContext(), Attach_Repair_Completion_MySubmit.class));
                        }
                    } catch (Exception e) {
                        Log.i("Exception", String.valueOf(e));
                    }

//                }

                break;

            case R.id.iv_changeOfStatus:
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));

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
                dialog.getDatePicker().setMinDate(Activity_Date1.getTime());
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

    private String current_date;
    private DatePickerDialog.OnDateSetListener pickerListener = new DatePickerDialog.OnDateSetListener() {

        // when dialog box is closed, below method will be called.
        @Override
        public void onDateSet(DatePicker view, int selectedYear,
                              int selectedMonth, int selectedDay) {

            year  = selectedYear;
            month = selectedMonth;
            day   = selectedDay;

            ed_completion_date.setText(formatDate(year, month, day));



        }
    };


    public boolean validate(final String time){
        pattern = Pattern.compile(TIME12HOURS_PATTERN);
        matcher = pattern.matcher(time);
        return matcher.matches();
    }
}
