package com.i_tankdepo;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Intent;
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
import android.widget.CompoundButton;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;

import com.i_tankdepo.Beanclass.CustomerDropdownBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.adapter.SpinnerCustomAdapter;
import com.i_tankdepo.customcomponents.CustomSpinner;
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
import java.util.Locale;
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
public class InspectionUpdate extends CommonActivity {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu,im_startDate,im_endDate,im_startTime,im_endTime,im_info_cleaning,iv_back,iv_changeOfStatus;
    private TextView tv_toolbarTitle,cleaning_text,tv_org_cleanDate,tv_lat_cleanDate;
    Button bt_pending, bt_add, bt_mysubmit, bt_home, bt_refresh, im_add, im_print,cleaning,heating,heat_submit,inspection,leakTest,bt_gateout;
    private RelativeLayout RL_heating,RL_Repair;
    private LinearLayout LL_hole,LL_heat_submit,LL_heat;
    private EditText ed_invoice_party,ed_cust_name,ed_eqp_no,ed_in_Date,ed_type,ed_previous_cargo,ed_chemical_name,ed_status,ed_cleanRefNo,
            ed_org_cleanDate,ed_lat_cleanDate,ed_org_inspection_Date,ed_lat_inspection_Date,
            ed_cleaned_for,ed_location_of_cleaning,ed_lidSealNo,ed_airlineSealNo,ed_bottomOutlet,ed_additional_cleaning,ed_eir_no,
            ed_cleaning_ref_no,ed_Cleaning_Rate,ed_remark;
    private Spinner sp_cleanStatus1,sp_cleanStatus2,sp_condition,sp_valueandFitting;
    private String get_ed_cust_name,get_ed_eqp_no,get_ed_in_Date,get_ed_type,get_ed_previous_cargo,get_ed_chemical_name,
            get_ed_status,get_ed_cleanRefNo,get_ed_org_cleanDate,get_ed_lat_cleanDate,get_ed_org_inspection_Date,get_ed_lat_inspection_Date,
            get_ed_location_of_cleaning,get_ed_lidSealNo,get_ed_airlineSealNo,get_ed_bottomOutlet,get_ed_eir_no,
            get_ed_cleaning_ref_no,get_ed_cleaned_for,get_ed_Cleaning_Rate,get_ed_remark;
    private String systemDate;
    private String sDate1;
    private Date date1;
    private boolean latest_cleanDate=false;
    private boolean original_cleanDate=false;
    private boolean latest_inspection_Date=false;
    private boolean original_inspection_Date=false;
    static final int DATE_DIALOG_ID = 1999;
    private int year,month,day,second;
    private ArrayList<CustomerDropdownBean> status_I_code_ArrayList;
    private CustomerDropdownBean customer_DropdownBean;
    private ArrayList<CustomerDropdownBean> status_II_code_ArrayList;
    private ArrayList<CustomerDropdownBean> ConditionDropDown_ArrayList;
    private ArrayList<CustomerDropdownBean> AdditionalCleaning_ArrayList;
    private CheckBox AddClean;
    private ArrayList<CustomerDropdownBean> CustomerDropdown_Invoice_ArrayList;
    private int invoi_count=0;
    private String get_sp_invoice_code="null",invoice_name="";
    private RadioGroup radio_group;
    private RadioButton radio_Customer,radio_Party;
    private LinearLayout LL_invoice_party,LL_aditional_two,LL_invoice_,LL_aditional_one;
    private String invoice_to;
    private CustomSpinner sp_aditional_cleaning,sp_invoice_party;
    private String get_sp_AdditionalCleaning_code=null,get_sp_AdditionalCleaning_name,get_sp_valueandFitting_code,get_sp_valueandFitting_name,get_sp_Condition_code,
            get_sp_Condition_name,get_sp_status_I_code_name,get_sp_status_II_code_name,get_sp_status_II_code,get_sp_status_I_code;
    private int AdditionalCleaning_count=0;
    String AddClean_status="False";
    String invoiceTo_status="False";
    private String slab_rate;
    private JSONObject getJsonObject;
    private String str1;
    private String str2;
    private ImageView tv_view_remarks;
    private String get_remark;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.inspection_update);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setOnClickListener(this);
        ed_Cleaning_Rate=(EditText)findViewById(R.id.ed_Cleaning_Rate);

        if (cd.isConnectingToInternet()) {
            new CleaningStatusOne_details().execute();
            new CleaningStatusTow_details().execute();
            new ConditionDropDown_details().execute();
            new ItemAliasSettingsList().execute();

        } else {
            shortToast(getApplicationContext(), "Please check your Internet Connection.");
        }


        tv_view_remarks = (ImageView)findViewById(R.id.tv_view_remarks);
        tv_view_remarks.setOnClickListener(this);
        menu.setVisibility(View.GONE);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Inspection Update");

        cleaning_text = (TextView)findViewById(R.id.tv_heating);
        heating = (Button)findViewById(R.id.heating);
        cleaning = (Button)findViewById(R.id.cleaning);
        heat_submit = (Button)findViewById(R.id.heat_submit);
        AddClean = (CheckBox)findViewById(R.id.AddClean);
        heat_submit.setOnClickListener(this);
        leakTest = (Button)findViewById(R.id.leakTest);
        cleaning.setVisibility(View.GONE);
        heating.setVisibility(View.GONE);
        leakTest.setVisibility(View.GONE);
        cleaning_text.setText("Print Cleaning Certificate");
        cleaning_text.setGravity(Gravity.CENTER_HORIZONTAL);
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        bt_gateout = (Button)findViewById(R.id.bt_gateout);
        inspection = (Button)findViewById(R.id.inspection);

        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_Repair.setVisibility(View.GONE);

        LL_hole = (LinearLayout) findViewById(R.id.LL_hole);
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);
        LL_heat = (LinearLayout) findViewById(R.id.LL_heat);

        LL_heat.setAlpha(0.5f);

        sp_cleanStatus1=(Spinner)findViewById(R.id.sp_cleanStatus1);
        sp_cleanStatus2=(Spinner)findViewById(R.id.sp_cleanStatus2);
        sp_condition=(Spinner)findViewById(R.id.sp_condition);
        sp_valueandFitting=(Spinner)findViewById(R.id.sp_valueandFitting);
        sp_aditional_cleaning=(CustomSpinner)findViewById(R.id.sp_aditional_cleaning);
        sp_invoice_party=(CustomSpinner)findViewById(R.id.sp_invoice_party);


        /*get_ed_cust_name,get_ed_eqp_no,get_ed_in_Date,get_ed_type,get_ed_previous_cargo,get_ed_chemical_name,
        get_ed_status,get_ed_cleanRefNo,get_ed_org_cleanDate,get_ed_lat_cleanDate,get_ed_org_inspection_Date,get_ed_lat_inspection_Date,
		get_ed_cleanget_ed_for,get_ed_location_of_cleaning,get_ed_lidSealNo,get_ed_airlineSealNo,get_ed_bottomOutlet,get_ed_eir_no,
		get_ed_cleaning_ref_no,get_ed_Cleaning_Rate,get_ed_remark*/

        ed_cust_name=(EditText)findViewById(R.id.ed_cust_name);
        ed_invoice_party=(EditText)findViewById(R.id.ed_invoice_party);
        ed_invoice_party.setOnClickListener(this);
        ed_eqp_no=(EditText)findViewById(R.id.ed_ed_eqp_no);
        ed_in_Date=(EditText)findViewById(R.id.ed_in_Date);
        ed_type=(EditText)findViewById(R.id.ed_type);
        ed_previous_cargo=(EditText)findViewById(R.id.ed_previous_cargo);
        ed_chemical_name=(EditText)findViewById(R.id.ed_chemical_name);
        ed_status=(EditText)findViewById(R.id.ed_status);
        ed_cleanRefNo=(EditText)findViewById(R.id.ed_cleanRefNo);
        ed_org_cleanDate=(EditText)findViewById(R.id.ed_org_cleanDate);
        ed_lat_cleanDate=(EditText)findViewById(R.id.ed_lat_cleanDate);
        ed_org_inspection_Date=(EditText)findViewById(R.id.ed_org_inspection_Date);
        ed_lat_inspection_Date=(EditText)findViewById(R.id.ed_lat_inspection_Date);
        ed_org_inspection_Date.setOnClickListener(this);
        ed_lat_inspection_Date.setOnClickListener(this);
        ed_cleaning_ref_no=(EditText)findViewById(R.id.ed_cleaning_ref_no);

        ed_bottomOutlet=(EditText)findViewById(R.id.ed_bottomOutlet);
        ed_airlineSealNo=(EditText)findViewById(R.id.ed_airlineSealNo);
        ed_cleaned_for=(EditText)findViewById(R.id.ed_cleaned_for);
        ed_lidSealNo=(EditText)findViewById(R.id.ed_lidSealNo);
        ed_location_of_cleaning=(EditText)findViewById(R.id.ed_location_of_cleaning);
        ed_eir_no=(EditText)findViewById(R.id.ed_eir_no);
        ed_additional_cleaning=(EditText)findViewById(R.id.ed_additional_cleaning);
        ed_additional_cleaning.setOnClickListener(this);
        ed_remark=(EditText)findViewById(R.id.ed_remark);


        radio_group=(RadioGroup)findViewById(R.id.radio_group);
        radio_Customer=(RadioButton)findViewById(R.id.radio_Customer);
        radio_Party=(RadioButton)findViewById(R.id.radio_Party);


        LL_aditional_one=(LinearLayout)findViewById(R.id.LL_aditional_one);
        LL_invoice_=(LinearLayout)findViewById(R.id.LL_invoice_);
        LL_aditional_two=(LinearLayout)findViewById(R.id.LL_aditional_two);
        LL_invoice_party=(LinearLayout)findViewById(R.id.LL_invoice_party);

        AddClean.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean isChecked) {

                if(isChecked)
                {
                    LL_aditional_one.setVisibility(View.VISIBLE);
                    LL_aditional_two.setVisibility(View.VISIBLE);
                    AdditionalCleaning_count=0;
                    new AdditionalCleaning_details().execute();
                    AddClean_status="True";

                }else {
                    LL_aditional_one.setVisibility(View.GONE);
                    LL_aditional_two.setVisibility(View.GONE);
                    AdditionalCleaning_count=0;
//                    new AdditionalCleaning_details().execute();
                    get_sp_AdditionalCleaning_name="";
                    get_sp_AdditionalCleaning_code="null";
                    AddClean_status="False";
                    ed_additional_cleaning.setText("");

                }
            }
        });

        radio_group.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup group, int checkedId) {

                if (checkedId == R.id.radio_Customer) {
                    //do work when radioButton1 is active
                    LL_invoice_.setVisibility(View.GONE);
                    LL_invoice_party.setVisibility(View.GONE);
                    invoi_count=0;
//                    new InvoiceParty_details().execute();
                    ed_invoice_party.setText("");
                    invoice_name="";
                    get_sp_invoice_code="null";
                    invoiceTo_status="False";


                } else  if (checkedId == R.id.radio_Party) {
                    //do work when radioButton2 is active
                    LL_invoice_.setVisibility(View.VISIBLE);
                    LL_invoice_party.setVisibility(View.VISIBLE);
                    invoi_count=0;
                    invoiceTo_status="True";
                    new InvoiceParty_details().execute();
                }

            }
        });

        bt_home = (Button) findViewById(R.id.heat_home);
        bt_home.setOnClickListener(this);
        bt_refresh = (Button) findViewById(R.id.heat_refresh);
        bt_refresh.setOnClickListener(this);

        tv_org_cleanDate = (TextView)findViewById(R.id.tv_org_inspection_Date);
        tv_lat_cleanDate = (TextView)findViewById(R.id.tv_lat_inspection_Date);

        String original = getColoredSpanned("Original Inspection Date","#bbbbbb");
        String latest = getColoredSpanned("Latest Inspection Date","#bbbbbb");

        String surName = getColoredSpanned("*","#cb0da5");

        tv_org_cleanDate.setText(Html.fromHtml(original+" "+surName));
        tv_lat_cleanDate.setText(Html.fromHtml(latest+" "+surName));

        systemDate = new SimpleDateFormat("dd-MMM-yyyy").format(new Date());
        ed_cust_name.setText(GlobalConstants.customer_name);
        ed_eqp_no.setText(GlobalConstants.equipment_no);
        ed_in_Date.setText(GlobalConstants.indate);
        ed_previous_cargo.setText(GlobalConstants.previous_cargo);
        ed_chemical_name.setText(GlobalConstants.previous_cargo);
        ed_status.setText(GlobalConstants.status);
        ed_location_of_cleaning.setText(GlobalConstants.LocationofCleaning);
        ed_cleaned_for.setText(GlobalConstants.Cleanedfor);
        ed_eir_no.setText(GlobalConstants.eir_no);
        ed_Cleaning_Rate.setText(GlobalConstants.clean_rate);
        Log.i("cleaning_RefNo",GlobalConstants.cleaning_RefNo);
        sDate1 = GlobalConstants.org_cleaningDate;
        ed_bottomOutlet.setText(GlobalConstants.BottomSealNo);
        ed_airlineSealNo.setText(GlobalConstants.TopSealNo);
        ed_lidSealNo.setText(GlobalConstants.ManLidSealNo);
        /*  GlobalConstants.BottomSealNo = list.get(position).getBottomSealNo();
                        GlobalConstants.TopSealNo = list.get(position).getTopSealNo();
                        GlobalConstants.ManLidSealNo = list.get(position).getManLidSealNo();*/


        try {
            date1 = new SimpleDateFormat("dd-MMM-yyyy").parse(sDate1);
        } catch (Exception e) {

        }

        if(GlobalConstants.cleaning_RefNo.equalsIgnoreCase("null") ||GlobalConstants.cleaning_RefNo.equals(""))
        {
            ed_cleaning_ref_no.setText("");

        }else
        {
            ed_cleaning_ref_no.setText(GlobalConstants.cleaning_RefNo);

        }  if(GlobalConstants.Certification_no.equalsIgnoreCase("null") ||GlobalConstants.Certification_no.equals(""))
        {
            ed_cleanRefNo.setText("");

        }else
        {
            ed_cleanRefNo.setText(GlobalConstants.Certification_no);

        }if(GlobalConstants.remark.equalsIgnoreCase("null") ||GlobalConstants.remark.equals(""))
        {
            ed_remark.setText("");

        }else
        {
//            ed_remark.setText(GlobalConstants.remark);

        }
        ed_type.setText(GlobalConstants.equip_status_type);

        if(GlobalConstants.org_cleaningDate.equals(null)||GlobalConstants.org_cleaningDate.equals("")
                || GlobalConstants.org_cleaningDate.equals("null"))
        {
            Log.i("org_cleaningDate",GlobalConstants.org_cleaningDate);
            ed_org_cleanDate.setText(systemDate);
        }else
        {
            ed_org_cleanDate.setText(GlobalConstants.org_cleaningDate);

        } if(GlobalConstants.org_inspectionDate.equals(null)||GlobalConstants.org_inspectionDate.equals("")
                || GlobalConstants.org_inspectionDate.equals("null"))
        {
            Log.i("org_cleaningDate",GlobalConstants.org_inspectionDate);
            ed_org_inspection_Date.setText(systemDate);
        }else
        {
            ed_org_inspection_Date.setText(GlobalConstants.org_inspectionDate);

        }
        ed_lat_cleanDate.setText(systemDate);
        ed_lat_inspection_Date.setText(systemDate);


        sp_invoice_party.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {


                if (invoi_count != 0) {
                    get_sp_invoice_code = CustomerDropdown_Invoice_ArrayList.get(i).getCode();
                    invoice_name = CustomerDropdown_Invoice_ArrayList.get(i).getName();
                    ed_invoice_party.setText(invoice_name);


                }

                invoi_count++;

            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        sp_cleanStatus1.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {



                    get_sp_status_I_code = status_I_code_ArrayList.get(i).getCode();
                    get_sp_status_I_code_name = status_I_code_ArrayList.get(i).getName();


            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
         sp_cleanStatus2.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                    @Override
                    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {



                            get_sp_status_II_code = status_II_code_ArrayList.get(i).getCode();
                            get_sp_status_II_code_name = status_II_code_ArrayList.get(i).getName();


                    }

                    @Override
                    public void onNothingSelected(AdapterView<?> adapterView) {

                    }
                });
            sp_condition.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                            @Override
                            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {



                                    get_sp_Condition_code = ConditionDropDown_ArrayList.get(i).getCode();
                                    get_sp_Condition_name = ConditionDropDown_ArrayList.get(i).getName();


                            }

                            @Override
                            public void onNothingSelected(AdapterView<?> adapterView) {

                            }
                        });
            sp_valueandFitting.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                                @Override
                                public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {



                                        get_sp_valueandFitting_code = ConditionDropDown_ArrayList.get(i).getCode();
                                        get_sp_valueandFitting_name = ConditionDropDown_ArrayList.get(i).getName();


                                }

                                @Override
                                public void onNothingSelected(AdapterView<?> adapterView) {

                                }
                            });
                sp_aditional_cleaning.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                                @Override
                                public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {


                                    if (AdditionalCleaning_count != 0) {
                                        get_sp_AdditionalCleaning_code = AdditionalCleaning_ArrayList.get(i).getCode();
                                        get_sp_AdditionalCleaning_name = AdditionalCleaning_ArrayList.get(i).getName();
                                        ed_additional_cleaning.setText(get_sp_AdditionalCleaning_name);

                                    }

                                    AdditionalCleaning_count++;




                                }

                                @Override
                                public void onNothingSelected(AdapterView<?> adapterView) {

                                }
                            });


    }



    @Override
    public void onClick(View view) {
        switch (view.getId()){
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(),MainActivity.class));
                break;
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.ed_invoice_party:

                sp_invoice_party.performClick();

                break;
            case R.id.tv_view_remarks:
                get_remark=ed_remark.getText().toString();
                popUp_remarks(get_remark);
                break;

            case R.id.ed_additional_cleaning:

                sp_aditional_cleaning.performClick();

                break;
            case R.id.heat_submit:
                get_ed_cust_name=ed_cust_name.getText().toString();
                get_ed_eqp_no=ed_eqp_no.getText().toString();
                get_ed_in_Date=ed_in_Date.getText().toString();
                get_ed_type=ed_type.getText().toString();
                get_ed_previous_cargo=ed_previous_cargo.getText().toString();
                get_ed_chemical_name=ed_chemical_name.getText().toString();
                get_ed_status=ed_status.getText().toString();
                get_ed_cleanRefNo=ed_cleanRefNo.getText().toString();
                get_ed_org_cleanDate=ed_org_cleanDate.getText().toString();
                get_ed_lat_cleanDate=ed_lat_cleanDate.getText().toString();
                get_ed_org_inspection_Date=ed_org_inspection_Date.getText().toString();
                get_ed_lat_inspection_Date=ed_lat_inspection_Date.getText().toString();
                get_ed_cleaned_for= ed_cleaned_for.getText().toString();
                get_ed_location_of_cleaning=ed_location_of_cleaning.getText().toString();
                get_ed_lidSealNo=ed_lidSealNo.getText().toString();
                get_ed_airlineSealNo=ed_airlineSealNo.getText().toString();
                get_ed_bottomOutlet=ed_bottomOutlet.getText().toString();
                get_ed_eir_no=ed_eir_no.getText().toString();
                get_ed_cleaning_ref_no=ed_cleaning_ref_no.getText().toString();
                get_ed_Cleaning_Rate=ed_Cleaning_Rate.getText().toString();


                get_ed_remark=ed_remark.getText().toString();
                invoice_name=ed_invoice_party.getText().toString();
                get_sp_AdditionalCleaning_name=ed_additional_cleaning.getText().toString();

                if(get_ed_org_inspection_Date.equals("")||get_ed_lat_inspection_Date.equals("") )
                {
                    shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                }else {
                    if(invoiceTo_status.equalsIgnoreCase("True") && invoice_name.equals(""))
                    {
                        shortToast(getApplicationContext(),"Invoicing To Party Required");

                    }else if(AddClean_status.equalsIgnoreCase("True") && get_sp_AdditionalCleaning_name.equals("")){

                        shortToast(getApplicationContext(),"Additional Cleaning Status & Remark Required ");

                    }else if(AddClean_status.equalsIgnoreCase("True") && !get_sp_AdditionalCleaning_name.equals("")&& get_ed_remark.equals("")){

                        shortToast(getApplicationContext(),"Remark Required ");

                    }else {


                        if(get_ed_Cleaning_Rate.equals(""))
                        {

                            GlobalConstants.clean_rate=get_ed_Cleaning_Rate;
                            new PostInfo().execute();

                        }else {
                            if (get_ed_Cleaning_Rate.contains(".")) {

                                String[] new_get_qt = get_ed_Cleaning_Rate.split(Pattern.quote("."));

                                String str3 = new_get_qt[0].toString();
                                String str3_1 = new_get_qt[1].toString();
                                if (str3.length() > 7 || str3_1.length() > 2) {
                                    shortToast(getApplicationContext(), "Invalid Cleaning Rate. Range must be from 0 to 999999999.99");

                                } else {

                                    GlobalConstants.clean_rate=get_ed_Cleaning_Rate;
                                    new PostInfo().execute();


                                }

                            }
                            else {

                                if(get_ed_Cleaning_Rate.length()>7)
                                {
                                    shortToast(getApplicationContext(), "Invalid Cleaning Rate. Range must be from 0 to 999999999.99");

                                }else {
                                    GlobalConstants.clean_rate=get_ed_Cleaning_Rate;
                                    new PostInfo().execute();

                                }

                            }

                            }

                    }

                }

                break;
            case R.id.iv_changeOfStatus:
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));
                break;
            case R.id.iv_back:
                finish();
                onBackPressed();
                break;
            case R.id.ed_org_cleanDate:
                latest_cleanDate=false;
                latest_inspection_Date=false;
                original_inspection_Date=false;
                original_cleanDate=true;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.ed_lat_cleanDate:
                latest_inspection_Date=false;
                original_inspection_Date=false;
                latest_cleanDate=true;
                original_cleanDate=false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.ed_org_inspection_Date:
                original_inspection_Date=true;
                latest_inspection_Date=false;
                latest_cleanDate=false;
                original_cleanDate=false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.ed_lat_inspection_Date:
                latest_inspection_Date=true;
                original_inspection_Date=false;
                latest_cleanDate=false;
                original_cleanDate=false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_org_cleanDate:
                latest_inspection_Date=false;
                original_inspection_Date=false;
                latest_cleanDate=false;
                original_cleanDate=true;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_lat_cleanDate:
                latest_inspection_Date=false;
                original_inspection_Date=false;
                latest_cleanDate=true;
                original_cleanDate=false;
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

            view.setMinDate(System.currentTimeMillis() - 1000);

            if(latest_cleanDate==true) {
                ed_lat_cleanDate.setText(formatDate(year, month, day));

            }
            else if(original_cleanDate)
            {
                ed_org_cleanDate.setText(formatDate(year, month, day));
            }else if(latest_inspection_Date)
            {
                ed_lat_inspection_Date.setText(formatDate(year, month, day));
            }else if(original_inspection_Date)
            {
                ed_org_inspection_Date.setText(formatDate(year, month, day));
            }

        }
    };


    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

    }

    public class CleaningStatusOne_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(InspectionUpdate.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCleaningStatusOne);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                Log.d("req", String.valueOf(jsonObject));
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



                        status_I_code_ArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("ENM_CD"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("ENM_CD"));
                            customer_DropdownBean.setCode(jsonObject.getString("ENM_ID"));

                            status_I_code_ArrayList.add(customer_DropdownBean);


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

            if ((progressDialog != null) && progressDialog.isShowing())
                progressDialog.dismiss();

            if(jsonarray!=null)
            {
            if (status_I_code_ArrayList != null) {
                SpinnerCustomAdapter cleanStatus_code = new SpinnerCustomAdapter(InspectionUpdate.this, R.layout.spinner_text, status_I_code_ArrayList);
                cleanStatus_code.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_cleanStatus1.setAdapter(cleanStatus_code);
               /* adapter = new UserListAdapter(Repair_Estimation_wizard.this, R.layout.line_item_row, lineitem_arraylist,worldlist,Sub_worldlist);
                list_line_items.setAdapter(adapter);*/

            }else {

            }
            } else {
                shortToast(getApplicationContext(), "Data Not Found");

            }


        }

    }
    public class CleaningStatusTow_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(InspectionUpdate.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCleaningStatusTwo);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                Log.d("req", String.valueOf(jsonObject));
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



                        status_II_code_ArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("ENM_CD"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("ENM_CD"));
                            customer_DropdownBean.setCode(jsonObject.getString("ENM_ID"));

                            status_II_code_ArrayList.add(customer_DropdownBean);


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

            if ((progressDialog != null) && progressDialog.isShowing())
                progressDialog.dismiss();

            if(jsonarray!=null)
            {
                if (status_I_code_ArrayList != null) {
                    SpinnerCustomAdapter cleanStatus_code = new SpinnerCustomAdapter(InspectionUpdate.this, R.layout.spinner_text, status_II_code_ArrayList);
                    cleanStatus_code.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    sp_cleanStatus2.setAdapter(cleanStatus_code);
               /* adapter = new UserListAdapter(Repair_Estimation_wizard.this, R.layout.line_item_row, lineitem_arraylist,worldlist,Sub_worldlist);
                list_line_items.setAdapter(adapter);*/

                }else {

                }
            } else {
                shortToast(getApplicationContext(), "Data Not Found");

            }


        }

    }
    public class ConditionDropDown_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(InspectionUpdate.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLConditionDropDown);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                Log.d("req", String.valueOf(jsonObject));
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



                        ConditionDropDown_ArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("ENM_CD"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("ENM_CD"));
                            customer_DropdownBean.setCode(jsonObject.getString("ENM_ID"));

                            ConditionDropDown_ArrayList.add(customer_DropdownBean);


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

            if ((progressDialog != null) && progressDialog.isShowing())
                progressDialog.dismiss();

            if(jsonarray!=null)
            {
                if (status_I_code_ArrayList != null) {
                    SpinnerCustomAdapter cleanStatus_code = new SpinnerCustomAdapter(InspectionUpdate.this, R.layout.spinner_text, ConditionDropDown_ArrayList);
                    cleanStatus_code.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    sp_condition.setAdapter(cleanStatus_code);
                    sp_valueandFitting.setAdapter(cleanStatus_code);
               /* adapter = new UserListAdapter(Repair_Estimation_wizard.this, R.layout.line_item_row, lineitem_arraylist,worldlist,Sub_worldlist);
                list_line_items.setAdapter(adapter);*/

                }else {

                }
            } else {
                shortToast(getApplicationContext(), "Data Not Found");

            }


        }

    }
    public class AdditionalCleaning_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(InspectionUpdate.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLAdditionalCleaning);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                Log.d("req", String.valueOf(jsonObject));
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



                        AdditionalCleaning_ArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("ENM_CD"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("ENM_CD"));
                            customer_DropdownBean.setCode(jsonObject.getString("ENM_ID"));

                            AdditionalCleaning_ArrayList.add(customer_DropdownBean);


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

            if ((progressDialog != null) && progressDialog.isShowing())
                progressDialog.dismiss();

            if(jsonarray!=null)
            {
                if (status_I_code_ArrayList != null) {
                    SpinnerCustomAdapter cleanStatus_code = new SpinnerCustomAdapter(InspectionUpdate.this, R.layout.spinner_text, AdditionalCleaning_ArrayList);
                    cleanStatus_code.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    sp_aditional_cleaning.setAdapter(cleanStatus_code);
               /* adapter = new UserListAdapter(Repair_Estimation_wizard.this, R.layout.line_item_row, lineitem_arraylist,worldlist,Sub_worldlist);
                list_line_items.setAdapter(adapter);*/

                }else {

                }
            } else {
                shortToast(getApplicationContext(), "Data Not Found");

            }


        }

    }
    public class InvoiceParty_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(InspectionUpdate.this);
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
                jsonObject.put("StatusName", "");


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("invoice_rep", resp);
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



                        CustomerDropdown_Invoice_ArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            customer_DropdownBean.setName(jsonObject.getString("INVCNG_PRTY_CD"));
                            customer_DropdownBean.setCode(jsonObject.getString("INVCNG_PRTY_ID"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("INVCNG_PRTY_CD"));


                            CustomerDropdown_Invoice_ArrayList.add(customer_DropdownBean);


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


            if (jsonarray != null)
            {
                if (CustomerDropdown_Invoice_ArrayList != null) {

/*
                for (int i = 0; i < Invoice_worldlist.size(); i++) {
                    try {
                        if (GlobalConstants.InvoiceParty_name.equals(Invoice_worldlist.get(i))) {

                            int index = Invoice_worldlist.indexOf(GlobalConstants.InvoiceParty_name);
                            Invoice_worldlist.remove(index);
                            System.out.println("before appartmentArray" + Invoice_worldlist.size());

                            Invoice_worldlist.add(0, GlobalConstants.InvoiceParty_name);
                            System.out.println("after appartmentArray" + Invoice_worldlist.size());

                            System.out.println("appartmentArray" + CustomerDropdown_Invoice_ArrayList.get(index));
                            CustomerDropdownBean appartment_nameBean = CustomerDropdown_Invoice_ArrayList.get(index);
                            CustomerDropdown_Invoice_ArrayList.remove(index);
                            CustomerDropdown_Invoice_ArrayList.add(0, appartment_nameBean);
                      */
/*  appartmentArray.remove(appartmentArray.get(i));
                        appartmentArray.add(0, appartmentname);*//*

                            //  tv_toolbarTitle.setBackgroundColor(Color.BLUE);


                        }
                    } catch (Exception e) {

                    }
                }
*/
                    SpinnerCustomAdapter CustomerAdapter = new SpinnerCustomAdapter(InspectionUpdate.this, R.layout.spinner_text, CustomerDropdown_Invoice_ArrayList);
//            ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, Repairworldlist);
                    CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    sp_invoice_party.setAdapter(CustomerAdapter);
/*
                if (GlobalConstants.Invoice_party > 0) {
                    try {
                        sp_invoicing_party.setSelection(Integer.parseInt(GlobalConstants.position));
                    } catch (Exception e) {
//                        sp_invoicing_party.setSelection(1);
                        ed_invoicing_party.setText(GlobalConstants.invoice_PartyName);
                    }
                }
*/
             /*   ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, Invoice_worldlist);
                CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_invoicing_party.setAdapter(CustomerAdapter);*/

                }else {
                    shortToast(getApplicationContext(), "Data Not Found");

                }

            }else {
                shortToast(getApplicationContext(), "Data Not Found");

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
            progressDialog = new ProgressDialog(InspectionUpdate.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCreateInspection);
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
                    if (get_ed_org_inspection_Date.equals(null) || get_ed_org_inspection_Date.length() < 0) {

                        get_ed_org_inspection_Date = "";
                    } else {
                        Boolean is_edt_Date1 = get_ed_org_inspection_Date.matches(datePattern);
                        if (is_edt_Date1 == true) {
                            get_ed_org_inspection_Date = myFormat.format(fromUser.parse(get_ed_org_inspection_Date));


                        }
                    }

                } catch (Exception e) {
                    get_ed_org_inspection_Date = "";
                }
                try {
                    if (get_ed_lat_inspection_Date.equals(null) || get_ed_lat_inspection_Date.length() < 0) {

                        get_ed_lat_inspection_Date = "";
                    } else {
                        Boolean is_edt_Date1 = get_ed_lat_inspection_Date.matches(datePattern);
                        if (is_edt_Date1 == true) {
                            get_ed_lat_inspection_Date = myFormat.format(fromUser.parse(get_ed_lat_inspection_Date));


                        }
                    }

                } catch (Exception e) {
                    get_ed_lat_inspection_Date = "";
                }


                   /*
                                   {
                    "bv_strCleaningId":"4380",
                    "bv_strEquipmentNo":"PPPP2222223",
                    "bv_strCrtfct_No":"",
                    "bv_strChemicalName":"Acetic Acid",
                    "bv_strCleaningRate":"800.00",
                    "bv_strOriginalCleaningDate":"01-Aug-2017",
                    "bv_strLastCleaningDate":"01-Aug-2017",
                    "bv_strOriginalInspectedDate":"02-Aug-2017",
                    "bv_strLastInspectedDate":"02-Aug-2017",
                    "bv_strEquipmentStatus":"6",
                    "bv_strEquipmentStatusCD":"INS",
                    "bv_strCleanedFor":"Chemicals 1",
                    "bv_strLocOfCleaning":"Chennai Chemical Lab",
                    "bv_strEquipmentCleaningStatus1":"60",
                    "bv_strEquipmentCleaningStatus1CD":"Dry",
                    "bv_strEquipmentCleaningStatus2":"64",
                    "bv_strEquipmentCleaningStatus2CD":"Odourless",
                    "bv_strEquipmentCondition":"62",
                    "bv_strEquipmentConditionCD":"Clean",
                    "bv_strValveandFittingCondition":"63",
                    "bv_strValveandFittingConditionCD":"UnClean",
                    "bv_strInvoicingTo":"183",
                    "bv_strInvoicingToCD":"TEST1",
                    "bv_strManLidSealNo":"Man",
                    "bv_strTopSealNo":"top",
                    "bv_strBottomSealNo":"bottom",
                    "bv_strCustomerReferenceNo":"",
                    "bv_strCleaningReferenceNo":"Ref1234567",
                    "bv_strRemarks":"Cleaning Remarks",
                    "bv_CustomerId":"181",
                    "bv_GateInDate":"13-Oct-2017",
                    "bv_strGI_TRNSCTN_NO":"8496",
                    "bv_intActivityId":"8496",
                    "bv_blnAdditionalCleaningFlag":"False",
                    "bv_strAdditionalCleaning_Status":"False",
                    "UserName":"ADMIN"

                    }
                                */

                jsonObject.put("bv_strCleaningId", GlobalConstants.cleaning_id);
                jsonObject.put("bv_strEquipmentNo",  GlobalConstants.equipment_no);
                jsonObject.put("bv_strCrtfct_No", get_ed_cleanRefNo );
                jsonObject.put("bv_strChemicalName",GlobalConstants.previous_cargo );
                if( GlobalConstants.clean_rate.equals("null")|| GlobalConstants.clean_rate.equals("")||
                        GlobalConstants.clean_rate.equals(null))
                {
                    jsonObject.put("bv_strCleaningRate", JSONObject.NULL);

                }else {
                    jsonObject.put("bv_strCleaningRate", GlobalConstants.clean_rate );

                }

                jsonObject.put("bv_strOriginalCleaningDate",get_ed_org_cleanDate );
                jsonObject.put("bv_strLastCleaningDate",get_ed_lat_cleanDate );
                jsonObject.put("bv_strOriginalInspectedDate",get_ed_org_inspection_Date );
                jsonObject.put("bv_strLastInspectedDate",get_ed_lat_inspection_Date );
                jsonObject.put("bv_strEquipmentStatus",GlobalConstants.status_id );
                jsonObject.put("bv_strEquipmentStatusCD",GlobalConstants.status );
                jsonObject.put("bv_strCleanedFor",get_ed_cleaned_for );
                jsonObject.put("bv_strLocOfCleaning",get_ed_location_of_cleaning);
                /*get_sp_AdditionalCleaning_code,get_sp_AdditionalCleaning_name,get_sp_valueandFitting_code,
                get_sp_valueandFitting_name,get_sp_Condition_code,get_sp_Condition_name,get_sp_status_I_code_name,
                get_sp_status_II_code_name,get_sp_status_II_code,get_sp_status_I_code*/

                jsonObject.put("bv_strEquipmentCleaningStatus1",get_sp_status_I_code );
                jsonObject.put("bv_strEquipmentCleaningStatus1CD",get_sp_status_I_code_name);
                jsonObject.put("bv_strEquipmentCleaningStatus2",get_sp_status_II_code);
                jsonObject.put("bv_strEquipmentCleaningStatus2CD",get_sp_status_II_code_name);
                jsonObject.put("bv_strEquipmentCondition",get_sp_Condition_code);
                jsonObject.put("bv_strEquipmentConditionCD",get_sp_Condition_name);
                jsonObject.put("bv_strValveandFittingCondition",get_sp_valueandFitting_code);
                jsonObject.put("bv_strValveandFittingConditionCD",get_sp_valueandFitting_name);

//                JSONObject.NULL
                if(get_sp_invoice_code.equals(null)||get_sp_invoice_code.equalsIgnoreCase("null"))
                {
                    jsonObject.put("bv_strInvoicingTo",JSONObject.NULL);
                }else
                {
                    jsonObject.put("bv_strInvoicingTo",get_sp_invoice_code);
                }

                jsonObject.put("bv_strInvoicingToCD",invoice_name);
                jsonObject.put("bv_strManLidSealNo",get_ed_lidSealNo);
                jsonObject.put("bv_strTopSealNo",get_ed_airlineSealNo);
                jsonObject.put("bv_strBottomSealNo",get_ed_bottomOutlet);
                jsonObject.put("bv_strCustomerReferenceNo","");
                jsonObject.put("bv_strCleaningReferenceNo", get_ed_cleaning_ref_no);
                jsonObject.put("bv_strRemarks",get_ed_remark );
                jsonObject.put("bv_CustomerId",GlobalConstants.customer_Id );
                jsonObject.put("bv_GateInDate",get_ed_in_Date );
                jsonObject.put("bv_strGI_TRNSCTN_NO",GlobalConstants.gi_trans_no );
                jsonObject.put("bv_intActivityId",GlobalConstants.gi_trans_no  );
                jsonObject.put("bv_blnAdditionalCleaningFlag",AddClean_status);
                jsonObject.put("bv_strAdditionalCleaning_Status",get_sp_AdditionalCleaning_name.toUpperCase());


                jsonObject.put("UserName",sp.getString(SP_USER_ID,"user_Id"));



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
                    shortToast(getApplicationContext(),"Inspection Created Successfully");
                    finish();
                    startActivity(new Intent(getApplicationContext(),InspectionPending.class));
                }else
                {
                    shortToast(getApplicationContext(),"Inspection Create Failed");
                }

            }else {
                shortToast(getApplicationContext(),"Inspection Create Failed");
            }

            progressDialog.dismiss();

        }
    }




    public class ItemAliasSettingsList extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(InspectionUpdate.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURL_CLNSlabrateSettingsList);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("KY_NAM", "073");


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("res_settings", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                 getJsonObject = jsonrootObject.getJSONObject("d");
                slab_rate = getJsonObject.getString("SettingsDirect");

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




                    if(slab_rate.equalsIgnoreCase("False"))
                    {
                        ed_Cleaning_Rate.setFocusable(true);
                    }else
                    {
                        ed_Cleaning_Rate.setFocusable(false);
                        ed_Cleaning_Rate.setBackgroundColor(getResources().getColor(R.color.hilight_color));

                    }


            }
            else
            {
            }



        }


    }


}
