package com.i_tankdepo;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
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
import com.i_tankdepo.Beanclass.Equipment_Info_TypeDropdownBean;
import com.i_tankdepo.Beanclass.Image_Bean;
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
public class AddRepair_Estimation_Details_MySubmit extends CommonActivity implements View.OnClickListener {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView iv_changeOfStatus, im_original_est_endDate, menu, im_down, im_up, im_heat_close, im_heat_ok, line_items, im_skip, im_remark_info, iv_back;


    private TextView tv_toolbarTitle, tv_search_options, no_data;
    LinearLayout LL_hole, LL_heat_submit, LL_search_Value, LL_heat;

    private ProgressDialog progressDialog;

    private Intent mServiceIntent;
    int count = 0;
    private Button heat_refresh, heat_home, heat_submit, heating, cleaning, inspection;
    private FragmentActivity mycontaxt;
    private TextView next_test_type_code, customer_name, equipment_no, eqip_type;
    private Button LL_Line_iteam, bt_refresh, LL_attachments, LL_summary;
    private Spinner sp_tarif_group, sp_repair_type, sp_tarif_code;
    private CustomSpinner sp_invoicing_party;
    private int invoice_count = 0;
    private ArrayList<Equipment_Info_TypeDropdownBean> dropdown_MoreInfo_arraylist;
    private Equipment_Info_TypeDropdownBean moreInfo_DropdownBean;
    private String infoId, infoCode;
    ArrayList<String> dropdown_MoreInfo_list = new ArrayList<>();
    List<String> Cargo_ID = new ArrayList<>();
    List<String> Cargo_Code = new ArrayList<>();
    List<String> Cargo_Description = new ArrayList<>();
    private Spinner sp_last_test_type;
    private EditText ed_estimate_date, ed_next_type;
    private ArrayList<Object> dropdown_customer_list;
    private ArrayList<String> worldlist;
    private ArrayList<String> Invoice_worldlist;
    private ArrayList<CustomerDropdownBean> CustomerDropdownArrayList;
    private ArrayList<CustomerDropdownBean> CustomerDropdown_Invoice_ArrayList;
    private CustomerDropdownBean customer_DropdownBean;
    private String RepairType_Name, RepairType_Code;
    List<String> Cust_name = new ArrayList<>();
    List<String> Cust_code = new ArrayList<>();
    private String get_repair_type, invoice_name, get_sp_last_test_type, get_sp_last_test_type_id, get_sp_repairtype_code, repairType_name, get_sp_invoice_code;
    private EditText edit_original_est_date, ed_original_est_date, ed_status, ed_repair_type, edit_last_testtype, ed_invoicing_party, edit_invoicing_party;
    private ImageView im_next_testDate, im_last_testDate, im_endDate;
    private Switch switch_heating;
    private EditText ed_next_test_date, ed_remarks, ed_labor_rate, ed_validate_period_for_test, ed_last_survey, ed_last_testDate;
    static final int DATE_DIALOG_ID = 1999;
    private Calendar c;
    private int year, month, day, second;
    private String curTime, systemDate;
    private boolean im_estimate = false, ed_estimate = false,
            im_original_date = false, ed_original_date = false,
            im_next_test_date = false, ed_nxt_test_date = false,
            last_test_date = false, im_last_test_date = false;
    private String get_cleanline_bets, get_est_date, get_status, get_next_test_type_id, get_original_est_date, get_next_test_type, get_last_survey, get_validate_period_for_test, get_labor_rate,
            get_ed_remarks, get_next_test_date, get_ed_last_testDate;
    private LinearLayout LL_Line;
    private Button LL_add_details;
    private LinearLayout LL_add_details_back;
    private String get_cust, get_eqp_no, get_indate, get_previous_cargo, get_repair_type_cd,
            get_repair_type_id, get_laststatus_date, get_validate_period_test, get_lbr_rate, get_nexttest_date,
            get_lst_survey, get_last_test_type, get_nexttest_type, get_last_test_date, get_rematk, get_invoice_prty_cd, get_invoice_prty_id, get_revision_no, get_cust_app_ref_no, get_approval_date, get_invoice_prty_name, get_gi_trans_no,
            get_repair_est_id, get_party_app_ref_no, get_survey_name, get_survey_completion_date, get_rep_status, get_repair_est_no;
    private RelativeLayout LL_originel;
    private TextView tv_amount;
    private JSONObject obiect;
    private String status;
    private SharedPreferences.Editor editor;
    private String add_detail_jsonobject;
    private ArrayList<Image_Bean> encodeArray;
    private String Line_item_Json, In_date, customer_Id;
    private TextView tv_invoice_party, invoice_id, tv_status, tv_Estimation_date, repair_estimate_text;
    private Button repair_completion, repair_estimate, repair_approval, survey_completion;
    private RelativeLayout RL_heating, RL_Repair;
    private ImageView more_info;
    private ImageView iv_send_mail;
    private boolean check = false;
    private int Invoice_party;
    private Button notification_text;
    private String sDate1;
    private Date date1;
    private String totale_amount;
    private int invoi_count=0;
    private String ReportURL;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.add_repair_details);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);


        customer_Id = GlobalConstants.cust_Id;
        In_date = GlobalConstants.indate;
        add_detail_jsonobject = GlobalConstants.add_detail_jsonobject;
        encodeArray = GlobalConstants.multiple_encodeArray;
        Line_item_Json = GlobalConstants.Line_item_Json;
        notification_text = (Button) findViewById(R.id.notification_text);
        sDate1 = GlobalConstants.indate;
        try {
            date1 = new SimpleDateFormat("dd-MM-yyyy").parse(sDate1);
        } catch (Exception e) {

        }

        sp_repair_type = (Spinner) findViewById(R.id.sp_repair_type);
        sp_invoicing_party = (CustomSpinner) findViewById(R.id.sp_invoicing_party);
        repair_estimate_text = (TextView) findViewById(R.id.tv_heating);
        tv_Estimation_date = (TextView) findViewById(R.id.tv_Estimation_date);
        tv_status = (TextView) findViewById(R.id.tv_status);
        invoice_id = (TextView) findViewById(R.id.invoice_id);
        invoice_id.setText(GlobalConstants.InvoiceParty_Id);
        tv_invoice_party = (TextView) findViewById(R.id.tv_invoice_party);

        Invoice_party = GlobalConstants.Invoice_party;
        String surName = getColoredSpanned("*", "#cb0da5");

        if (Invoice_party > 0) {
            String lable_invoice_party = getColoredSpanned("Invoice Party", "#bbbbbb");
            tv_invoice_party.setText(Html.fromHtml(lable_invoice_party + " " + surName));

        }
        String lable_repair = getColoredSpanned("Estimate Date", "#bbbbbb");
        String lable_Status = getColoredSpanned("Status", "#bbbbbb");


        tv_Estimation_date.setText(Html.fromHtml(lable_repair + " " + surName));
        tv_status.setText(Html.fromHtml(lable_Status + " " + surName));

        repair_estimate_text.setText("Repair Estimation");
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        im_remark_info = (ImageView) findViewById(R.id.im_remark_info);
        im_remark_info.setOnClickListener(this);
        iv_back.setOnClickListener(this);
        im_original_est_endDate = (ImageView) findViewById(R.id.im_original_est_endDate);
        iv_changeOfStatus = (ImageView) findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        im_skip = (ImageView) findViewById(R.id.im_skip);
        line_items = (ImageView) findViewById(R.id.line_items);
        im_skip.setOnClickListener(this);
        line_items.setOnClickListener(this);
        bt_refresh = (Button) findViewById(R.id.heat_refresh);
        bt_refresh.setOnClickListener(this);
        menu.setVisibility(View.GONE);
        switch_heating = (Switch) findViewById(R.id.switch_heating);
        sp_last_test_type = (Spinner) findViewById(R.id.sp_last_testtype);
        ed_next_type = (EditText) findViewById(R.id.ed_next_type);
        ed_estimate_date = (EditText) findViewById(R.id.ed_estimate_date);
        ed_last_testDate = (EditText) findViewById(R.id.ed_last_testDate);
        ed_last_survey = (EditText) findViewById(R.id.ed_last_survey);
        ed_validate_period_for_test = (EditText) findViewById(R.id.ed_validate_period_for_test);
        ed_labor_rate = (EditText) findViewById(R.id.ed_labor_rate);
        ed_labor_rate.setText(GlobalConstants.lobor_rate);
        ed_remarks = (EditText) findViewById(R.id.ed_remarks);
        if (GlobalConstants.from.equalsIgnoreCase("Repairpending")) {
            ed_remarks.setFocusable(true);
        }

        repair_estimate = (Button) findViewById(R.id.repair_estimate);
        repair_estimate.setOnClickListener(this);
        repair_approval = (Button) findViewById(R.id.repair_approval);
        repair_approval.setVisibility(View.GONE);
        repair_completion = (Button) findViewById(R.id.repair_completion);
        repair_completion.setVisibility(View.GONE);
        survey_completion = (Button) findViewById(R.id.survey_completion);
        survey_completion.setVisibility(View.GONE);

        ed_validate_period_for_test = (EditText) findViewById(R.id.ed_validate_period_for_test);
        ed_next_test_date = (EditText) findViewById(R.id.ed_next_test_date);
        im_next_testDate = (ImageView) findViewById(R.id.im_next_testDate);
        ed_repair_type = (EditText) findViewById(R.id.ed_repair_type);
        ed_status = (EditText) findViewById(R.id.ed_status);
        ed_original_est_date = (EditText) findViewById(R.id.ed_original_est_date);
        edit_original_est_date = (EditText) findViewById(R.id.edit_original_est_date);
        im_endDate = (ImageView) findViewById(R.id.im_endDate);
        im_last_testDate = (ImageView) findViewById(R.id.im_last_testDate);
        ed_invoicing_party = (EditText) findViewById(R.id.ed_invoicing_party);
        edit_last_testtype = (EditText) findViewById(R.id.edit_last_testtype);
        edit_invoicing_party = (EditText) findViewById(R.id.edit_invoicing_party);
        ed_repair_type.setOnClickListener(this);
        ed_invoicing_party.setOnClickListener(this);
        LL_attachments = (Button) findViewById(R.id.attachments);

        RL_heating = (RelativeLayout) findViewById(R.id.RL_heating);
        RL_Repair = (RelativeLayout) findViewById(R.id.RL_Repair);
        RL_heating.setVisibility(View.GONE);

        ed_estimate_date.setOnClickListener(this);
//        im_original_est_endDate.setOnClickListener(this);

        ed_last_testDate.setOnClickListener(this);
        im_last_testDate.setOnClickListener(this);

//        ed_next_test_date.setOnClickListener(this);
//        im_next_testDate.setOnClickListener(this);

//        ed_original_est_date.setOnClickListener(this);
//        im_original_est_endDate.setOnClickListener(this);
        LL_Line_iteam = (Button) findViewById(R.id.Line_iteam);
        LL_Line = (LinearLayout) findViewById(R.id.LL_Line_iteam);
        LL_add_details = (Button) findViewById(R.id.add_details);
        LL_add_details_back = (LinearLayout) findViewById(R.id.LL_add_details);
        LL_add_details_back.setBackgroundColor(getResources().getColor(R.color.submit));

        LL_add_details.setOnClickListener(this);
        LL_attachments = (Button) findViewById(R.id.attachments);
        LL_attachments.setOnClickListener(this);

        LL_summary = (Button) findViewById(R.id.summary);
        LL_summary.setOnClickListener(this);
        LL_summary.setVisibility(View.GONE);
        tv_amount = (TextView) findViewById(R.id.tv_amount);
        tv_amount.setOnClickListener(this);
        tv_amount.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_summary.performClick();
            }
        });
        line_items.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_Line_iteam.performClick();
            }
        });
        LL_Line_iteam.setOnClickListener(this);
        tv_amount.setText(GlobalConstants.totale_amount);
        heat_home = (Button) findViewById(R.id.heat_home);
        heat_refresh = (Button) findViewById(R.id.heat_refresh);
        heat_submit = (Button) findViewById(R.id.heat_submit);
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);
        LL_originel = (RelativeLayout) findViewById(R.id.LL_originel);

        LL_heat_submit.setClickable(false);
        LL_heat_submit.setAlpha(0.5f);
        LL_heat = (LinearLayout) findViewById(R.id.LL_heat);
        LL_heat.setOnClickListener(this);
        more_info = (ImageView) findViewById(R.id.more_info);
        more_info.setOnClickListener(this);
        iv_send_mail = (ImageView) findViewById(R.id.iv_send_mail);
        iv_send_mail.setVisibility(View.VISIBLE);
        iv_send_mail.setOnClickListener(this);
//        tabLayout.setupWithViewPager(viewPager);


        // tabLayout.clearOnTabSelectedListeners();


        heating = (Button) findViewById(R.id.heating);
        cleaning = (Button) findViewById(R.id.cleaning);
        inspection = (Button) findViewById(R.id.inspection);
        inspection.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);

        heat_refresh.setOnClickListener(this);
        heat_home.setOnClickListener(this);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);

        Log.i("customer_name", GlobalConstants.customer_name);
        customer_name = (TextView) findViewById(R.id.text1);
        next_test_type_code = (TextView) findViewById(R.id.code);
        customer_name.setText(GlobalConstants.customer_name + " , " + GlobalConstants.equipment_no + ", " + GlobalConstants.type);
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


        /* String last_testDate=GlobalConstants.last_testDate;
                        GlobalConstants.last_testDate=last_testDate;
                        String next_testDate = GlobalConstants.next_testDate;
                        GlobalConstants.next_testDate = next_testDate;
                        String lastSurveyor = GlobalConstants.lastSurveyor;
                        GlobalConstants.lastSurveyor = lastSurveyor;*/

            ed_next_test_date.setText(GlobalConstants.next_testDate);
            ed_last_testDate.setText(GlobalConstants.last_testDate);
            ed_last_survey.setText(GlobalConstants.lastSurveyor );

        if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")
                || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")) {

            ed_estimate_date.setText(In_date);
            ed_original_est_date.setText(In_date);
            iv_send_mail.setVisibility(View.VISIBLE);
            if (GlobalConstants.summaryfrom.equalsIgnoreCase("summaryfrom")) {


                if (!add_detail_jsonobject.equals("") || !add_detail_jsonobject.equals(null)) {
                    try {


                        JSONObject jsonrootObject = new JSONObject(add_detail_jsonobject);
                        ed_estimate_date.setText(jsonrootObject.getString("get_est_date"));
                        ed_invoicing_party.setText(jsonrootObject.getString("InvoiceParty_name"));
                        jsonrootObject.getString("get_sp_repairtype_code");
//                        ed_invoicing_party.setText(jsonrootObject.getString("invoice_name"));
                        jsonrootObject.getString("get_sp_invoice_code");
                        ed_original_est_date.setText(jsonrootObject.getString("get_original_est_date"));
                        jsonrootObject.getString("get_sp_last_test_type");
                        jsonrootObject.getString("get_sp_last_test_type_id");
                        jsonrootObject.getString("get_next_test_type");
                        jsonrootObject.getString("get_next_test_type_id");
                        ed_last_survey.setText(jsonrootObject.getString("get_last_survey"));
                        ed_next_test_date.setText(jsonrootObject.getString("get_next_test_date"));
                        ed_last_testDate.setText(jsonrootObject.getString("get_ed_last_testDate"));
                        ed_validate_period_for_test.setText(jsonrootObject.getString("get_validate_period_for_test"));
                        ed_labor_rate.setText(jsonrootObject.getString("get_labor_rate"));
                        ed_remarks.setText(jsonrootObject.getString("get_ed_remarks"));
                        ed_repair_type.setText(jsonrootObject.getString("get_repair_type"));
                        /*  obiect.put("get_repair_type", get_repair_type);
                                obiect.put("get_sp_repairtype_code", get_sp_repairtype_code);*/

                        jsonrootObject.getString("get_cleanline_bets");

                    } catch (Exception e) {

                    }

                }
            } else {
                ed_estimate_date.setText(In_date);
                if (!add_detail_jsonobject.equals("") || !add_detail_jsonobject.equals(null)) {
                    try {


                        JSONObject jsonrootObject = new JSONObject(add_detail_jsonobject);
                        ed_estimate_date.setText(jsonrootObject.getString("get_est_date"));
                        ed_invoicing_party.setText(jsonrootObject.getString("InvoiceParty_name"));
                        jsonrootObject.getString("get_sp_repairtype_code");
//                        ed_invoicing_party.setText(jsonrootObject.getString("invoice_name"));
                        jsonrootObject.getString("get_sp_invoice_code");
                        ed_original_est_date.setText(jsonrootObject.getString("get_original_est_date"));
                        jsonrootObject.getString("get_sp_last_test_type");
                        jsonrootObject.getString("get_sp_last_test_type_id");
                        jsonrootObject.getString("get_next_test_type");
                        jsonrootObject.getString("get_next_test_type_id");
                        ed_last_survey.setText(jsonrootObject.getString("get_last_survey"));
                        ed_next_test_date.setText(jsonrootObject.getString("get_next_test_date"));
                        ed_last_testDate.setText(jsonrootObject.getString("get_ed_last_testDate"));
                        ed_validate_period_for_test.setText(jsonrootObject.getString("get_validate_period_for_test"));
                        ed_labor_rate.setText(jsonrootObject.getString("get_labor_rate"));
                        ed_remarks.setText(jsonrootObject.getString("get_ed_remarks"));
                        ed_repair_type.setText(jsonrootObject.getString("get_repair_type"));
                        /*  obiect.put("get_repair_type", get_repair_type);
                                obiect.put("get_sp_repairtype_code", get_sp_repairtype_code);*/

                        jsonrootObject.getString("get_cleanline_bets");

                    } catch (Exception e) {

                    }

                }

//                new Get_Repair_MySubmit_details().execute();
            }

            im_remark_info.setVisibility(View.VISIBLE);

        } else if (GlobalConstants.from.equalsIgnoreCase("PendinglineItem")) {
            ed_estimate_date.setText(systemDate);
            ed_original_est_date.setText(systemDate);
            ed_last_testDate.setText(GlobalConstants.last_testDate);
            //  ed_time.setText(curTime);
//            ed_next_test_date.setText(systemDate);

        } else {


            if (!add_detail_jsonobject.equals("") || !add_detail_jsonobject.equals(null)) {
                try {


                    JSONObject jsonrootObject = new JSONObject(add_detail_jsonobject);
                    ed_estimate_date.setText(jsonrootObject.getString("get_est_date"));
                    ed_invoicing_party.setText(jsonrootObject.getString("InvoiceParty_name"));
                    jsonrootObject.getString("get_sp_repairtype_code");
                    ed_invoicing_party.setText(jsonrootObject.getString("invoice_name"));
                    jsonrootObject.getString("get_sp_invoice_code");
                    ed_original_est_date.setText(jsonrootObject.getString("get_original_est_date"));
                    jsonrootObject.getString("get_sp_last_test_type");
                    jsonrootObject.getString("get_sp_last_test_type_id");
                    jsonrootObject.getString("get_next_test_type");
                    jsonrootObject.getString("get_next_test_type_id");
                    ed_last_survey.setText(jsonrootObject.getString("get_last_survey"));
                    ed_next_test_date.setText(jsonrootObject.getString("get_next_test_date"));
                    ed_last_testDate.setText(jsonrootObject.getString("get_ed_last_testDate"));
                    ed_validate_period_for_test.setText(jsonrootObject.getString("get_validate_period_for_test"));
                    ed_labor_rate.setText(jsonrootObject.getString("get_labor_rate"));
                    ed_remarks.setText(jsonrootObject.getString("get_ed_remarks"));

                    jsonrootObject.getString("get_cleanline_bets");

                } catch (Exception e) {

                }

            }
        }


        if (cd.isConnectingToInternet()) {
            new Create_GateIn_moreInfo_list_details().execute();
            new Repait_Type_details().execute();
            if (Invoice_party > 0|| invoice_count>0) {
                new InvoiceParty_details().execute();
            }
        } else {
            shortToast(getApplicationContext(), "Please check your Internet Connection.");
        }
        get_cleanline_bets = "False";
        switch_heating.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if (b) {
                    get_cleanline_bets = "True";
                } else {
                    get_cleanline_bets = "False";

                }
            }
        });
        try {
            if (GlobalConstants.attach_count != null) {
                notification_text.setText((GlobalConstants.attach_count));

            } else {
//                notification_text.setText((GlobalConstants.attach_count));
                notification_text.setText(String.valueOf(encodeArray.size()));

            }

        } catch (Exception e) {
            notification_text.setText((GlobalConstants.attach_count));
        }
        sp_last_test_type.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                get_sp_last_test_type = sp_last_test_type.getSelectedItem().toString();
                get_sp_last_test_type_id = dropdown_MoreInfo_arraylist.get(i).getId();
                if (i == 0) {
                    ed_next_type.setText(dropdown_MoreInfo_arraylist.get(1).getCode());
                    next_test_type_code.setText(dropdown_MoreInfo_arraylist.get(1).getId());
                } else if (i == 1) {
                    ed_next_type.setText(dropdown_MoreInfo_arraylist.get(0).getCode());
                    next_test_type_code.setText(dropdown_MoreInfo_arraylist.get(0).getId());
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        sp_repair_type.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {

                repairType_name = sp_repair_type.getSelectedItem().toString();
                get_sp_repairtype_code = CustomerDropdownArrayList.get(i).getCode();
                ed_repair_type.setText(repairType_name);

            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        sp_invoicing_party.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {

                if (Invoice_party > 0) {

                    get_sp_invoice_code = CustomerDropdown_Invoice_ArrayList.get(i).getCode();
                    invoice_name = CustomerDropdown_Invoice_ArrayList.get(i).getName();
                    ed_invoicing_party.setText(invoice_name);
                    GlobalConstants.invoice_PartyCD=invoice_name;
                    invoice_id.setText(get_sp_invoice_code);

                }else {
                    if (invoi_count != 0) {
                        get_sp_invoice_code = CustomerDropdown_Invoice_ArrayList.get(i).getCode();
                        invoice_name = CustomerDropdown_Invoice_ArrayList.get(i).getName();
                        ed_invoicing_party.setText(invoice_name);
                        GlobalConstants.invoice_PartyCD=invoice_name;
                        invoice_id.setText(get_sp_invoice_code);

                    }

                    invoi_count++;
                }
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
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;

            case R.id.repair_estimate:
                finish();
                startActivity(new Intent(getApplicationContext(), Repair_MainActivity.class));
                break;
            case R.id.more_info:

                popUp_equipment_info(GlobalConstants.equipment_no, GlobalConstants.status, GlobalConstants.status_id, GlobalConstants.previous_cargo, "", "", "", "");


                break;
            case R.id.im_remark_info:
                get_ed_remarks = ed_remarks.getText().toString();

                popUp_remarks(get_ed_remarks);
                break;
            case R.id.Line_iteam:

                get_est_date = ed_estimate_date.getText().toString();
                //  InvoiceParty_name,get_sp_repairtype_code
                //  invoice_name,get_sp_invoice_code
                get_status = ed_status.getText().toString();
                get_original_est_date = ed_original_est_date.getText().toString();
//                get_sp_last_test_type
                get_next_test_type = ed_next_type.getText().toString();
                get_next_test_type_id = next_test_type_code.getText().toString();

                get_last_survey = ed_last_survey.getText().toString();
                get_validate_period_for_test = ed_validate_period_for_test.getText().toString();
                get_labor_rate = ed_labor_rate.getText().toString();
                get_ed_remarks = ed_remarks.getText().toString();
                get_next_test_date = ed_next_test_date.getText().toString();
                get_ed_last_testDate = ed_last_testDate.getText().toString();
                invoice_name = ed_invoicing_party.getText().toString();
                get_repair_type = ed_repair_type.getText().toString();
                if (Invoice_party > 0) {
                    if (ed_invoicing_party.getText().toString().equals("")) {
                       /* if(get_est_date.equals("") || get_status.equals("") )
                        {
                            shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                        }else {*/
                        obiect = new JSONObject();
                        try {
                            obiect.put("get_est_date", get_est_date);
                            if (!invoice_name.equals("")) {
                                if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")
                                        || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")) {

                                } else {
                                    obiect.put("get_sp_invoice_code", GlobalConstants.invoice_PartyID);

                                }

                            } else {
                                obiect.put("get_sp_invoice_code", "0");

                            }
                            obiect.put("InvoiceParty_name", invoice_name);
                            obiect.put("get_sp_repairtype_code", get_sp_repairtype_code);
                            obiect.put("get_repair_type", get_repair_type);
                            obiect.put("get_status", get_status);
                            obiect.put("get_original_est_date", get_original_est_date);
                            obiect.put("get_sp_last_test_type", get_sp_last_test_type);
                            obiect.put("get_sp_last_test_type_id", get_sp_last_test_type_id);
                            obiect.put("get_next_test_type", get_next_test_type);
                            obiect.put("get_next_test_type_id", get_next_test_type_id);
                            obiect.put("get_last_survey", get_last_survey);
                            obiect.put("get_next_test_date", get_next_test_date);
                            obiect.put("get_ed_last_testDate", get_ed_last_testDate);
                            obiect.put("get_validate_period_for_test", get_validate_period_for_test);
                            obiect.put("get_labor_rate", get_labor_rate);
                            obiect.put("get_ed_remarks", get_ed_remarks);

                            obiect.put("get_cleanline_bets", get_cleanline_bets);
                        } catch (Exception e) {

                        }
                        Log.i("obiect", String.valueOf(obiect));
                        GlobalConstants.add_detail_jsonobject = String.valueOf(obiect);
                        status = GlobalConstants.status;
                        GlobalConstants.status = status;
                        GlobalConstants.multiple_encodeArray = encodeArray;
                        GlobalConstants.cust_Id = customer_Id;
                        GlobalConstants.indate = In_date;
                        totale_amount = GlobalConstants.totale_amount;
                        String est_id = GlobalConstants.repair_EstimateID;
                        String est_no = GlobalConstants.repairEstimateNo;
                        String invoice_id = GlobalConstants.invoice_PartyID;
                        String invoice_cd = GlobalConstants.invoice_PartyCD;
                        GlobalConstants.invoice_PartyID = invoice_id;
                        GlobalConstants.invoice_PartyCD = invoice_cd;
                        GlobalConstants.Invoice_party=Invoice_party;
                        String gi_transaction_no = GlobalConstants.gi_trans_no;
                        GlobalConstants.repair_EstimateID = est_id;
                        GlobalConstants.repair_EstimateNo = est_no;
                        GlobalConstants.gi_trans_no = gi_transaction_no;
                        String count = GlobalConstants.attach_count;
                        GlobalConstants.attach_count = count;
                        GlobalConstants.totale_amount = totale_amount;
                        String last_testDate=GlobalConstants.last_testDate;
                        GlobalConstants.last_testDate=get_ed_last_testDate;
                        String next_testDate = GlobalConstants.next_testDate;
                        GlobalConstants.next_testDate = get_next_test_date;
                        String lastSurveyor = GlobalConstants.lastSurveyor;
                        GlobalConstants.lastSurveyor = get_last_survey;
                        editor = sp.edit();
                        if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")
                                || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")) {

                            GlobalConstants.from = "MysubmitAddDetails";
                        } else {
                            GlobalConstants.from = "AddDetails";
                        }
                        editor.putString(SP_Add_datails_Json, String.valueOf(obiect));
                        editor.commit();

                        finish();
                        GlobalConstants.Line_item_Json = Line_item_Json;
                        startActivity(new Intent(getApplicationContext(), Repair_Estimation_wizard_MySubmit.class));
//                        }
                    } else {
                    /*    if(get_est_date.equals("") || get_status.equals("") )
                        {
                            shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                        }else {*/
                        obiect = new JSONObject();
                        try {
                            obiect.put("get_est_date", get_est_date);
                            if (!invoice_name.equals("")) {
                                obiect.put("get_sp_invoice_code", invoice_id.getText().toString());

                            } else {
                                obiect.put("get_sp_invoice_code", "0");

                            }
                            obiect.put("InvoiceParty_name", invoice_name);
                            obiect.put("get_sp_repairtype_code", get_sp_repairtype_code);
                            obiect.put("get_repair_type", get_repair_type);
                            obiect.put("get_status", get_status);
                            obiect.put("get_original_est_date", get_original_est_date);
                            obiect.put("get_sp_last_test_type", get_sp_last_test_type);
                            obiect.put("get_sp_last_test_type_id", get_sp_last_test_type_id);
                            obiect.put("get_next_test_type", get_next_test_type);
                            obiect.put("get_next_test_type_id", get_next_test_type_id);
                            obiect.put("get_last_survey", get_last_survey);
                            obiect.put("get_next_test_date", get_next_test_date);
                            obiect.put("get_ed_last_testDate", get_ed_last_testDate);
                            obiect.put("get_validate_period_for_test", get_validate_period_for_test);
                            obiect.put("get_labor_rate", get_labor_rate);
                            obiect.put("get_ed_remarks", get_ed_remarks);

                            obiect.put("get_cleanline_bets", get_cleanline_bets);
                        } catch (Exception e) {

                        }
                        Log.i("obiect", String.valueOf(obiect));
                        GlobalConstants.add_detail_jsonobject = String.valueOf(obiect);
                        status = GlobalConstants.status;
                        GlobalConstants.status = status;
                        GlobalConstants.multiple_encodeArray = encodeArray;
                        GlobalConstants.cust_Id = customer_Id;
                        GlobalConstants.indate = In_date;
                        String est_id = GlobalConstants.repair_EstimateID;
                        String est_no = GlobalConstants.repairEstimateNo;
                        String gi_transaction_no = GlobalConstants.gi_trans_no;
                        GlobalConstants.repair_EstimateID = est_id;
                        GlobalConstants.repair_EstimateNo = est_no;
                        String invoice_id = GlobalConstants.invoice_PartyID;
                        String invoice_cd = GlobalConstants.invoice_PartyCD;
                        GlobalConstants.invoice_PartyID = invoice_id;
                        GlobalConstants.invoice_PartyCD = invoice_cd;
                        String count = GlobalConstants.attach_count;
                        GlobalConstants.attach_count = count;
                        GlobalConstants.gi_trans_no = gi_transaction_no;
                        totale_amount = GlobalConstants.totale_amount;
                        GlobalConstants.totale_amount = totale_amount;
                        String last_testDate=GlobalConstants.last_testDate;
                        GlobalConstants.last_testDate=get_ed_last_testDate;
                        String next_testDate = GlobalConstants.next_testDate;
                        GlobalConstants.next_testDate = get_next_test_date;
                        String lastSurveyor = GlobalConstants.lastSurveyor;
                        GlobalConstants.lastSurveyor = get_last_survey;
                        editor = sp.edit();
                        if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")
                                || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")) {

                            GlobalConstants.from = "MysubmitAddDetails";
                        } else {
                            GlobalConstants.from = "AddDetails";
                        }
                        editor.putString(SP_Add_datails_Json, String.valueOf(obiect));
                        editor.commit();

                        finish();
                        GlobalConstants.Line_item_Json = Line_item_Json;
                        startActivity(new Intent(getApplicationContext(), Repair_Estimation_wizard_MySubmit.class));
//                        }
                    }
                } else {
                    /*if(get_est_date.equals("") || get_status.equals("") )
                    {
                        shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                    }else {*/
                    obiect = new JSONObject();
                    try {
                        obiect.put("get_est_date", get_est_date);
                        if (!invoice_name.equals("")) {
                            obiect.put("get_sp_invoice_code", invoice_id.getText().toString());

                        } else {
                            obiect.put("get_sp_invoice_code", "0");

                        }
                        obiect.put("InvoiceParty_name", invoice_name);
                        obiect.put("get_sp_repairtype_code", get_sp_repairtype_code);
                        obiect.put("get_repair_type", get_repair_type);
                        obiect.put("get_status", get_status);
                        obiect.put("get_original_est_date", get_original_est_date);
                        obiect.put("get_sp_last_test_type", get_sp_last_test_type);
                        obiect.put("get_sp_last_test_type_id", get_sp_last_test_type_id);
                        obiect.put("get_next_test_type", get_next_test_type);
                        obiect.put("get_next_test_type_id", get_next_test_type_id);
                        obiect.put("get_last_survey", get_last_survey);
                        obiect.put("get_next_test_date", get_next_test_date);
                        obiect.put("get_ed_last_testDate", get_ed_last_testDate);
                        obiect.put("get_validate_period_for_test", get_validate_period_for_test);
                        obiect.put("get_labor_rate", get_labor_rate);
                        obiect.put("get_ed_remarks", get_ed_remarks);

                        obiect.put("get_cleanline_bets", get_cleanline_bets);
                    } catch (Exception e) {

                    }
                    Log.i("obiect", String.valueOf(obiect));
                    GlobalConstants.add_detail_jsonobject = String.valueOf(obiect);
                    status = GlobalConstants.status;
                    GlobalConstants.status = status;
                    GlobalConstants.multiple_encodeArray = encodeArray;
                    GlobalConstants.cust_Id = customer_Id;
                    GlobalConstants.indate = In_date;
                    String est_id = GlobalConstants.repair_EstimateID;
                    String est_no = GlobalConstants.repairEstimateNo;
                    String gi_transaction_no = GlobalConstants.gi_trans_no;
                    GlobalConstants.repair_EstimateID = est_id;
                    GlobalConstants.repair_EstimateNo = est_no;
                    String count = GlobalConstants.attach_count;
                    String invoice_id = GlobalConstants.invoice_PartyID;
                    String invoice_cd = GlobalConstants.invoice_PartyCD;
                    GlobalConstants.invoice_PartyID = invoice_id;
                    GlobalConstants.invoice_PartyCD = invoice_cd;
                    String last_testDate=GlobalConstants.last_testDate;
                    GlobalConstants.last_testDate=get_ed_last_testDate;
                    String next_testDate = GlobalConstants.next_testDate;
                    GlobalConstants.next_testDate = get_next_test_date;
                    String lastSurveyor = GlobalConstants.lastSurveyor;
                    GlobalConstants.lastSurveyor = get_last_survey;
                    GlobalConstants.attach_count = count;
                    GlobalConstants.gi_trans_no = gi_transaction_no;
                    totale_amount = GlobalConstants.totale_amount;
                    GlobalConstants.totale_amount = totale_amount;
                    editor = sp.edit();
                    if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")
                            || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")) {

                        GlobalConstants.from = "MysubmitAddDetails";
                    } else {
                        GlobalConstants.from = "AddDetails";
                    }
                    editor.putString(SP_Add_datails_Json, String.valueOf(obiect));
                    editor.commit();

                    finish();
                    GlobalConstants.Line_item_Json = Line_item_Json;
                    startActivity(new Intent(getApplicationContext(), Repair_Estimation_wizard_MySubmit.class));
//                    }
                }

                break;
            case R.id.iv_back:
                onBackPressed();
                break;
            case R.id.im_skip:

                LL_attachments.performClick();

                break;
            case R.id.ed_invoicing_party:
                check = true;
                sp_invoicing_party.performClick();

                break;
            case R.id.ed_repair_type:
                sp_repair_type.performClick();

                break;
            case R.id.attachments:

                get_est_date = ed_estimate_date.getText().toString();
                //  InvoiceParty_name,get_sp_repairtype_code
                //  invoice_name,get_sp_invoice_code
                get_status = ed_status.getText().toString();
                get_original_est_date = ed_original_est_date.getText().toString();
//                get_sp_last_test_type
                get_next_test_type = ed_next_type.getText().toString();
                get_next_test_type_id = next_test_type_code.getText().toString();
                get_last_survey = ed_last_survey.getText().toString();
                get_validate_period_for_test = ed_validate_period_for_test.getText().toString();
                get_labor_rate = ed_labor_rate.getText().toString();
                get_ed_remarks = ed_remarks.getText().toString();
                get_next_test_date = ed_next_test_date.getText().toString();
                get_ed_last_testDate = ed_last_testDate.getText().toString();
                invoice_name = ed_invoicing_party.getText().toString();
                get_repair_type = ed_repair_type.getText().toString();

                if (Invoice_party > 0) {
                  /*  if (ed_invoicing_party.getText().toString().equals("")) {
                        shortToast(getApplicationContext(), "Please Select the invoice party");
                    } else {*/
                       /* if(get_est_date.equals("") || get_status.equals(""))
                        {
                            shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                        }else {*/

                        obiect = new JSONObject();
                        try {
                            obiect.put("get_est_date", get_est_date);
                            if (!invoice_name.equals("")) {
                                obiect.put("get_sp_invoice_code", invoice_id.getText().toString());

                            } else {
                                obiect.put("get_sp_invoice_code", "0");

                            }
                            obiect.put("InvoiceParty_name", invoice_name);
                            obiect.put("get_sp_repairtype_code",get_sp_repairtype_code);
                            obiect.put("get_repair_type", get_repair_type);
                            obiect.put("get_status", get_status);
                            obiect.put("get_original_est_date", get_original_est_date);
                            obiect.put("get_sp_last_test_type", get_sp_last_test_type);
                            obiect.put("get_sp_last_test_type_id", get_sp_last_test_type_id);
                            obiect.put("get_next_test_type", get_next_test_type);
                            obiect.put("get_next_test_type_id", get_next_test_type_id);
                            obiect.put("get_last_survey", get_last_survey);
                            obiect.put("get_next_test_date", get_next_test_date);
                            obiect.put("get_ed_last_testDate", get_ed_last_testDate);
                            obiect.put("get_validate_period_for_test", get_validate_period_for_test);
                            obiect.put("get_labor_rate", get_labor_rate);
                            obiect.put("get_ed_remarks", get_ed_remarks);

                            obiect.put("get_cleanline_bets", get_cleanline_bets);
                        } catch (Exception e) {

                        }
                        Log.i("obiect", String.valueOf(obiect));
                        GlobalConstants.add_detail_jsonobject = String.valueOf(obiect);
                        status = GlobalConstants.status;
                        GlobalConstants.status = status;
                        GlobalConstants.multiple_encodeArray = encodeArray;
                        GlobalConstants.cust_Id = customer_Id;
                        GlobalConstants.indate = In_date;
                        String est_id = GlobalConstants.repair_EstimateID;
                        String est_no = GlobalConstants.repairEstimateNo;
                        String gi_transaction_no = GlobalConstants.gi_trans_no;
                        GlobalConstants.repair_EstimateID = est_id;
                        GlobalConstants.repair_EstimateNo = est_no;
                        GlobalConstants.gi_trans_no = gi_transaction_no;
                        String invoice_id = GlobalConstants.invoice_PartyID;
                        String invoice_cd = GlobalConstants.invoice_PartyCD;
                        GlobalConstants.invoice_PartyID = invoice_id;
                        GlobalConstants.invoice_PartyCD = invoice_cd;
                        totale_amount = GlobalConstants.totale_amount;
                        GlobalConstants.totale_amount = totale_amount;
                    String last_testDate=GlobalConstants.last_testDate;
                    GlobalConstants.last_testDate=get_ed_last_testDate;
                    String next_testDate = GlobalConstants.next_testDate;
                    GlobalConstants.next_testDate = get_next_test_date;
                    String lastSurveyor = GlobalConstants.lastSurveyor;
                    GlobalConstants.lastSurveyor = get_last_survey;
                        editor = sp.edit();
                        if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")
                                || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")) {

                            GlobalConstants.from = "MysubmitAddDetails";
                        } else {
                            GlobalConstants.from = "AddDetails";
                        }
                        editor.putString(SP_Add_datails_Json, String.valueOf(obiect));
                        editor.commit();
                        GlobalConstants.Line_item_Json = Line_item_Json;
                        finish();
                        startActivity(new Intent(getApplicationContext(), Attach_Repair_Estimation_MySubmit.class));
//                        }
//                    }
                } else {
                   /* if(get_est_date.equals("") || get_status.equals("") )
                    {
                        shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                    }else {
*/
                    obiect = new JSONObject();
                    try {
                        obiect.put("get_est_date", get_est_date);
                        if (!invoice_name.equals("")) {
                            obiect.put("get_sp_invoice_code", invoice_id.getText().toString());

                        } else {
                            obiect.put("get_sp_invoice_code", "0");

                        }
                        obiect.put("InvoiceParty_name", invoice_name);
                        obiect.put("get_sp_repairtype_code", get_sp_repairtype_code);
                        obiect.put("get_repair_type", get_repair_type);
                        obiect.put("get_status", get_status);
                        obiect.put("get_original_est_date", get_original_est_date);
                        obiect.put("get_sp_last_test_type", get_sp_last_test_type);
                        obiect.put("get_sp_last_test_type_id", get_sp_last_test_type_id);
                        obiect.put("get_next_test_type", get_next_test_type);
                        obiect.put("get_next_test_type_id", get_next_test_type_id);
                        obiect.put("get_last_survey", get_last_survey);
                        obiect.put("get_next_test_date", get_next_test_date);
                        obiect.put("get_ed_last_testDate", get_ed_last_testDate);
                        obiect.put("get_validate_period_for_test", get_validate_period_for_test);
                        obiect.put("get_labor_rate", get_labor_rate);
                        obiect.put("get_ed_remarks", get_ed_remarks);

                        obiect.put("get_cleanline_bets", get_cleanline_bets);
                    } catch (Exception e) {

                    }
                    Log.i("obiect", String.valueOf(obiect));
                    GlobalConstants.add_detail_jsonobject = String.valueOf(obiect);
                    status = GlobalConstants.status;
                    GlobalConstants.status = status;
                    GlobalConstants.multiple_encodeArray = encodeArray;
                    GlobalConstants.cust_Id = customer_Id;
                    GlobalConstants.indate = In_date;
                    String est_id = GlobalConstants.repair_EstimateID;
                    String est_no = GlobalConstants.repairEstimateNo;
                    String gi_transaction_no = GlobalConstants.gi_trans_no;
                    GlobalConstants.repair_EstimateID = est_id;
                    GlobalConstants.repair_EstimateNo = est_no;
                    String invoice_id = GlobalConstants.invoice_PartyID;
                    String invoice_cd = GlobalConstants.invoice_PartyCD;
                    GlobalConstants.invoice_PartyID = invoice_id;
                    String last_testDate=GlobalConstants.last_testDate;
                    GlobalConstants.last_testDate=get_ed_last_testDate;
                    String next_testDate = GlobalConstants.next_testDate;
                    GlobalConstants.next_testDate = get_next_test_date;
                    String lastSurveyor = GlobalConstants.lastSurveyor;
                    GlobalConstants.lastSurveyor = get_last_survey;
                    GlobalConstants.invoice_PartyCD = invoice_cd;

                    GlobalConstants.gi_trans_no = gi_transaction_no;
                    totale_amount = GlobalConstants.totale_amount;
                    GlobalConstants.totale_amount = totale_amount;
                    editor = sp.edit();
                    if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")
                            || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")) {

                        GlobalConstants.from = "MysubmitAddDetails";
                    } else {
                        GlobalConstants.from = "AddDetails";
                    }
                    editor.putString(SP_Add_datails_Json, String.valueOf(obiect));
                    editor.commit();
                    GlobalConstants.Line_item_Json = Line_item_Json;
                    finish();
                    startActivity(new Intent(getApplicationContext(), Attach_Repair_Estimation_MySubmit.class));
//                    }
                }

                break;
            case R.id.summary:

                get_est_date = ed_estimate_date.getText().toString();
                //  InvoiceParty_name,get_sp_repairtype_code
                //  invoice_name,get_sp_invoice_code
                get_status = ed_status.getText().toString();
                get_original_est_date = ed_original_est_date.getText().toString();
//                get_sp_last_test_type
                get_next_test_type = ed_next_type.getText().toString();
                get_next_test_type_id = next_test_type_code.getText().toString();
                get_last_survey = ed_last_survey.getText().toString();
                get_validate_period_for_test = ed_validate_period_for_test.getText().toString();
                get_labor_rate = ed_labor_rate.getText().toString();
                get_ed_remarks = ed_remarks.getText().toString();
                get_repair_type = ed_repair_type.getText().toString();
                get_next_test_date = ed_next_test_date.getText().toString();
                get_ed_last_testDate = ed_last_testDate.getText().toString();
                invoice_name = ed_invoicing_party.getText().toString();

                if (Invoice_party > 0) {
                    if (ed_invoicing_party.getText().toString().equals("")) {
                      /*  if(get_est_date.equals("") || get_status.equals("") )
                        {
                            shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                        }else {*/
                        obiect = new JSONObject();
                        try {
                            obiect.put("get_est_date", get_est_date);
                            if (!invoice_name.equals("")) {
                                obiect.put("get_sp_invoice_code", invoice_id.getText().toString());

                            } else {
                                obiect.put("get_sp_invoice_code", "0");

                            }
                            obiect.put("InvoiceParty_name", invoice_name);
                            obiect.put("get_sp_repairtype_code", get_sp_repairtype_code);
                            obiect.put("get_repair_type", get_repair_type);
                            obiect.put("get_status", get_status);
                            obiect.put("get_original_est_date", get_original_est_date);
                            obiect.put("get_sp_last_test_type", get_sp_last_test_type);
                            obiect.put("get_sp_last_test_type_id", get_sp_last_test_type_id);
                            obiect.put("get_next_test_type", get_next_test_type);
                            obiect.put("get_next_test_type_id", get_next_test_type_id);
                            obiect.put("get_last_survey", get_last_survey);
                            obiect.put("get_next_test_date", get_next_test_date);
                            obiect.put("get_ed_last_testDate", get_ed_last_testDate);
                            obiect.put("get_validate_period_for_test", get_validate_period_for_test);
                            obiect.put("get_labor_rate", get_labor_rate);
                            obiect.put("get_ed_remarks", get_ed_remarks);

                            obiect.put("get_cleanline_bets", get_cleanline_bets);
                        } catch (Exception e) {

                        }
                        Log.i("obiect", String.valueOf(obiect));
                        GlobalConstants.add_detail_jsonobject = String.valueOf(obiect);
                        status = GlobalConstants.status;
                        GlobalConstants.status = status;
                        GlobalConstants.multiple_encodeArray = encodeArray;
                        GlobalConstants.cust_Id = customer_Id;
                        GlobalConstants.indate = In_date;
                        String est_id = GlobalConstants.repair_EstimateID;
                        String est_no = GlobalConstants.repairEstimateNo;
                        String gi_transaction_no = GlobalConstants.gi_trans_no;
                        GlobalConstants.repair_EstimateID = est_id;
                        GlobalConstants.repair_EstimateNo = est_no;
                        String invoice_id = GlobalConstants.invoice_PartyID;
                        String invoice_cd = GlobalConstants.invoice_PartyCD;
                        GlobalConstants.invoice_PartyID = invoice_id;
                        GlobalConstants.invoice_PartyCD = invoice_cd;
                        GlobalConstants.gi_trans_no = gi_transaction_no;
                        String count = GlobalConstants.attach_count;
                        GlobalConstants.attach_count = count;
                        totale_amount = GlobalConstants.totale_amount;
                        GlobalConstants.totale_amount = totale_amount;
                        String last_testDate=GlobalConstants.last_testDate;
                        GlobalConstants.last_testDate=get_ed_last_testDate;
                        String next_testDate = GlobalConstants.next_testDate;
                        GlobalConstants.next_testDate = get_next_test_date;
                        String lastSurveyor = GlobalConstants.lastSurveyor;
                        GlobalConstants.lastSurveyor = get_last_survey;
                        editor = sp.edit();
                        if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")
                                || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")) {

                            GlobalConstants.from = "MysubmitAddDetails";
                        } else {
                            GlobalConstants.from = "AddDetails";
                        }
                        editor.putString(SP_Add_datails_Json, String.valueOf(obiect));
                        editor.commit();

                        finish();
                        GlobalConstants.Line_item_Json = Line_item_Json;
                        startActivity(new Intent(getApplicationContext(), Repair_Estimation_Summary_MySubmit.class));
//                        }
                    } else {
                       /* if(get_est_date.equals("") || get_status.equals("") )
                        {
                            shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                        }else {*/
                        obiect = new JSONObject();
                        try {
                            obiect.put("get_est_date", get_est_date);
                            if (!invoice_name.equals("")) {
                                obiect.put("get_sp_invoice_code", invoice_id.getText().toString());

                            } else {
                                obiect.put("get_sp_invoice_code", "0");

                            }
                            obiect.put("InvoiceParty_name", invoice_name);
                            obiect.put("get_sp_repairtype_code", get_sp_repairtype_code);
                            obiect.put("get_repair_type", get_repair_type);
                            obiect.put("get_status", get_status);
                            obiect.put("get_original_est_date", get_original_est_date);
                            obiect.put("get_sp_last_test_type", get_sp_last_test_type);
                            obiect.put("get_sp_last_test_type_id", get_sp_last_test_type_id);
                            obiect.put("get_next_test_type", get_next_test_type);
                            obiect.put("get_next_test_type_id", get_next_test_type_id);
                            obiect.put("get_last_survey", get_last_survey);
                            obiect.put("get_next_test_date", get_next_test_date);
                            obiect.put("get_ed_last_testDate", get_ed_last_testDate);
                            obiect.put("get_validate_period_for_test", get_validate_period_for_test);
                            obiect.put("get_labor_rate", get_labor_rate);
                            obiect.put("get_ed_remarks", get_ed_remarks);

                            obiect.put("get_cleanline_bets", get_cleanline_bets);
                        } catch (Exception e) {

                        }
                        Log.i("obiect", String.valueOf(obiect));
                        GlobalConstants.add_detail_jsonobject = String.valueOf(obiect);
                        status = GlobalConstants.status;
                        GlobalConstants.status = status;
                        GlobalConstants.multiple_encodeArray = encodeArray;
                        GlobalConstants.cust_Id = customer_Id;
                        GlobalConstants.indate = In_date;
                        String est_id = GlobalConstants.repair_EstimateID;
                        String est_no = GlobalConstants.repairEstimateNo;
                        String gi_transaction_no = GlobalConstants.gi_trans_no;
                        GlobalConstants.repair_EstimateID = est_id;
                        GlobalConstants.repair_EstimateNo = est_no;
                        String invoice_id = GlobalConstants.invoice_PartyID;
                        String invoice_cd = GlobalConstants.invoice_PartyCD;
                        GlobalConstants.invoice_PartyID = invoice_id;
                        GlobalConstants.invoice_PartyCD = invoice_cd;
                        String count = GlobalConstants.attach_count;
                        GlobalConstants.attach_count = count;
                        GlobalConstants.gi_trans_no = gi_transaction_no;
                        totale_amount = GlobalConstants.totale_amount;
                        GlobalConstants.totale_amount = totale_amount;
                        String last_testDate=GlobalConstants.last_testDate;
                        GlobalConstants.last_testDate=get_ed_last_testDate;
                        String next_testDate = GlobalConstants.next_testDate;
                        GlobalConstants.next_testDate = get_next_test_date;
                        String lastSurveyor = GlobalConstants.lastSurveyor;
                        GlobalConstants.lastSurveyor = get_last_survey;
                        editor = sp.edit();
                        if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")
                                || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                                || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")) {

                            GlobalConstants.from = "MysubmitAddDetails";
                        } else {
                            GlobalConstants.from = "AddDetails";
                        }
                        editor.putString(SP_Add_datails_Json, String.valueOf(obiect));
                        editor.commit();

                        finish();
                        GlobalConstants.Line_item_Json = Line_item_Json;
                        startActivity(new Intent(getApplicationContext(), Repair_Estimation_Summary_MySubmit.class));
//                        }
                    }
                } else {
                   /* if(get_est_date.equals("") || get_status.equals("") )
                    {
                        shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                    }else {*/
                    obiect = new JSONObject();
                    try {
                        obiect.put("get_est_date", get_est_date);
                        if (!invoice_name.equals("")) {
                            obiect.put("get_sp_invoice_code", invoice_id.getText().toString());

                        } else {
                            obiect.put("get_sp_invoice_code", "0");

                        }
                        obiect.put("InvoiceParty_name", invoice_name);
                        obiect.put("get_sp_repairtype_code",get_sp_repairtype_code);
                        obiect.put("get_repair_type", get_repair_type);
                        obiect.put("get_status", get_status);
                        obiect.put("get_original_est_date", get_original_est_date);
                        obiect.put("get_sp_last_test_type", get_sp_last_test_type);
                        obiect.put("get_sp_last_test_type_id", get_sp_last_test_type_id);
                        obiect.put("get_next_test_type", get_next_test_type);
                        obiect.put("get_next_test_type_id", get_next_test_type_id);
                        obiect.put("get_last_survey", get_last_survey);
                        obiect.put("get_next_test_date", get_next_test_date);
                        obiect.put("get_ed_last_testDate", get_ed_last_testDate);
                        obiect.put("get_validate_period_for_test", get_validate_period_for_test);
                        obiect.put("get_labor_rate", get_labor_rate);
                        obiect.put("get_ed_remarks", get_ed_remarks);

                        obiect.put("get_cleanline_bets", get_cleanline_bets);
                    } catch (Exception e) {

                    }
                    Log.i("obiect", String.valueOf(obiect));
                    GlobalConstants.add_detail_jsonobject = String.valueOf(obiect);
                    status = GlobalConstants.status;
                    GlobalConstants.status = status;
                    GlobalConstants.multiple_encodeArray = encodeArray;
                    GlobalConstants.cust_Id = customer_Id;
                    GlobalConstants.indate = In_date;
                    String est_id = GlobalConstants.repair_EstimateID;
                    String est_no = GlobalConstants.repairEstimateNo;
                    String gi_transaction_no = GlobalConstants.gi_trans_no;
                    GlobalConstants.repair_EstimateID = est_id;
                    GlobalConstants.repair_EstimateNo = est_no;
                    String count = GlobalConstants.attach_count;
                    String invoice_id = GlobalConstants.invoice_PartyID;
                    String invoice_cd = GlobalConstants.invoice_PartyCD;
                    GlobalConstants.invoice_PartyID = invoice_id;
                    GlobalConstants.invoice_PartyCD = invoice_cd;
                    GlobalConstants.attach_count = count;
                    GlobalConstants.gi_trans_no = gi_transaction_no;
                    totale_amount = GlobalConstants.totale_amount;
                    GlobalConstants.totale_amount = totale_amount;
                    String last_testDate=GlobalConstants.last_testDate;
                    GlobalConstants.last_testDate=get_ed_last_testDate;
                    String next_testDate = GlobalConstants.next_testDate;
                    GlobalConstants.next_testDate = get_next_test_date;
                    String lastSurveyor = GlobalConstants.lastSurveyor;
                    GlobalConstants.lastSurveyor = get_last_survey;
                    editor = sp.edit();
                    if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")
                            || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")) {

                        GlobalConstants.from = "MysubmitAddDetails";
                    } else {
                        GlobalConstants.from = "AddDetails";
                    }
                    editor.putString(SP_Add_datails_Json, String.valueOf(obiect));
                    editor.commit();

                    finish();
                    GlobalConstants.Line_item_Json = Line_item_Json;
                    startActivity(new Intent(getApplicationContext(), Repair_Estimation_Summary_MySubmit.class));
//                    }
                }

                break;
            case R.id.iv_changeOfStatus:
                startActivity(new Intent(getApplicationContext(), ChangeOfStatus.class));

                break;

            case R.id.ed_estimate_date:
                ed_estimate = true;
                last_test_date = false;
                ed_nxt_test_date = false;
                ed_original_date = false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_repair_estimation:
                ed_estimate = false;
                im_estimate = true;
                last_test_date = false;
                ed_nxt_test_date = false;
                ed_original_date = false;

                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.ed_original_est_date:
                ed_estimate = false;
                last_test_date = false;
                ed_nxt_test_date = false;
                ed_original_date = true;


                showDialog(DATE_DIALOG_ID);

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
                    email.putExtra(Intent.EXTRA_EMAIL, new String[]{sendemail});
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
                }*/

                break;
            case R.id.im_original_est_endDate:
                ed_estimate = false;
                last_test_date = false;
                ed_nxt_test_date = false;
                ed_original_date = false;
                im_original_date = true;

                showDialog(DATE_DIALOG_ID);

                break;
            case R.id.ed_last_testDate:
                ed_estimate = false;
                last_test_date = true;
                ed_nxt_test_date = false;
                ed_original_date = false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_last_testDate:
                ed_estimate = false;
                im_last_test_date = true;
                last_test_date = false;
                ed_nxt_test_date = false;
                ed_original_date = false;

                showDialog(DATE_DIALOG_ID);
                break;

            case R.id.ed_next_test_date:

                ed_estimate = false;
                last_test_date = false;
                ed_nxt_test_date = true;
                ed_original_date = false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_next_testDate:
                ed_estimate = false;
                last_test_date = false;
                ed_nxt_test_date = false;
                im_next_test_date = true;
                ed_original_date = false;
                showDialog(DATE_DIALOG_ID);
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


    public class Create_GateIn_moreInfo_list_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(AddRepair_Estimation_Details_MySubmit.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLEquipmentInfoDropdownType);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));

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

                    System.out.println("Am HashMap list" + jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found.");
                            }
                        });
                    } else {

                        dropdown_MoreInfo_arraylist = new ArrayList<Equipment_Info_TypeDropdownBean>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            moreInfo_DropdownBean = new Equipment_Info_TypeDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            moreInfo_DropdownBean.setId(jsonObject.getString("ENM_ID"));
                            moreInfo_DropdownBean.setCode(jsonObject.getString("ENM_CD"));
                            infoId = jsonObject.getString("ENM_ID");
                            infoCode = jsonObject.getString("ENM_CD");
                            String[] set1 = new String[2];
                            set1[0] = infoId;
                            set1[1] = infoCode;
                            dropdown_MoreInfo_list.add(infoCode);
                            Cargo_ID.add(set1[0]);
                            Cargo_Code.add(set1[1]);
                            dropdown_MoreInfo_arraylist.add(moreInfo_DropdownBean);
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


            if (jsonarray != null) {
//                UserListAdapter adapter = new UserListAdapter(Create_GateIn.this, R.layout.list_item_row, pending_arraylist);
//                listview.setAdapter(adapter);
                ArrayAdapter<String> CargoAdapter = new ArrayAdapter<>(getApplicationContext(), R.layout.spinner_text, dropdown_MoreInfo_list);
                CargoAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_last_test_type.setAdapter(CargoAdapter);

            } else {
                shortToast(getApplicationContext(), "Data Not Found");


            }

            progressDialog.dismiss();

        }

    }

    public class Repait_Type_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(AddRepair_Estimation_Details_MySubmit.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLRepair_Type);
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


                        worldlist = new ArrayList<String>();
                        CustomerDropdownArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            customer_DropdownBean.setName(jsonObject.getString("ENM_CD"));
                            customer_DropdownBean.setCode(jsonObject.getString("ENM_ID"));
                            RepairType_Name = jsonObject.getString("ENM_CD");
                            RepairType_Code = jsonObject.getString("ENM_ID");

                            String[] set1 = new String[2];
                            set1[0] = RepairType_Name;
                            set1[1] = RepairType_Code;
                            dropdown_customer_list.add(set1);
                            Cust_name.add(set1[0]);
                            Cust_code.add(set1[1]);
                            CustomerDropdownArrayList.add(customer_DropdownBean);
                            worldlist.add(RepairType_Name);


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
                ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, worldlist);
                CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_repair_type.setAdapter(CustomerAdapter);

            } else {
                shortToast(getApplicationContext(), "Data Not Found");

            }

            progressDialog.dismiss();

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
                if (ed_estimate) {
                    dialog.getDatePicker().setMinDate(date1.getTime());
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

            year = selectedYear;
            month = selectedMonth;
            day = selectedDay;


            if (ed_estimate == true || im_estimate == true) {
                ed_estimate_date.setText(formatDate(year, month, day));


            } else if (last_test_date == true || im_last_test_date == true) {

                ed_last_testDate.setText(formatDate(year, month, day));
                ed_next_test_date.setText(formatDate(year, month, day));


            } else if (ed_nxt_test_date == true || im_next_test_date == true) {
                ed_next_test_date.setText(formatDate(year, month, day));
            } else if (ed_original_date == true || im_original_date == true) {
                ed_original_est_date.setText(formatDate(year, month, day));
            }

            //    System.out.println("am a new from date====>>" + str_From);

            //  date.setText(formatDate(year, month, day));

        }
    };


    public class InvoiceParty_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(AddRepair_Estimation_Details_MySubmit.this);
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
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("INVCNG_PRTY_CD"));

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


            if (jsonarray != null) {
            if (dropdown_customer_list != null) {

                if (Invoice_party <= 0) {
                    SpinnerCustomAdapter CustomerAdapter = new SpinnerCustomAdapter(AddRepair_Estimation_Details_MySubmit.this, R.layout.spinner_text, CustomerDropdown_Invoice_ArrayList);
                    CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    sp_invoicing_party.setAdapter(CustomerAdapter);
                } else {

                    SpinnerCustomAdapter CustomerAdapter = new SpinnerCustomAdapter(AddRepair_Estimation_Details_MySubmit.this, R.layout.spinner_text, CustomerDropdown_Invoice_ArrayList);
                    CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    sp_invoicing_party.setAdapter(CustomerAdapter);
                    for (int i = 0; i < Invoice_worldlist.size(); i++) {
                        try {
                            if (GlobalConstants.invoice_PartyCD.equals(Invoice_worldlist.get(i))) {


                                sp_invoicing_party.setSelection(i);
                            }
                        } catch (Exception e) {

                        }
                    }

                }

               /* if (GlobalConstants.Invoice_party > 0) {
                    try {
                        sp_invoicing_party.setSelection(Integer.parseInt(GlobalConstants.position));
                    } catch (Exception e) {
//                        sp_invoicing_party.setSelection(1);
                        ed_invoicing_party.setText(GlobalConstants.invoice_PartyName);
                    }
                }*/
             /*   ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, Invoice_worldlist);
                CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_invoicing_party.setAdapter(CustomerAdapter);*/

            }
        }else {
                shortToast(getApplicationContext(), "Data Not Found");

            }

            progressDialog.dismiss();

        }

    }

    public class Get_Repair_MySubmit_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray LineItems_Json;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(AddRepair_Estimation_Details_MySubmit.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
//            progressDialog.show();

        }

        @Override
        protected Void doInBackground(Void... params) {

            ServiceHandler sh = new ServiceHandler();
            HttpParams httpParameters = new BasicHttpParams();
            DefaultHttpClient httpClient = new DefaultHttpClient(httpParameters);
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLRepairEstimateList);
            httpPost.setHeader("Content-Type", "application/json");

            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("Mode", "edit");
                jsonObject.put("PageName", "Repair Estimate");


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep_mysubmit", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("Response");
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


                        for (int j = 0; j < jsonarray.length(); j++) {

                            jsonObject = jsonarray.getJSONObject(j);

                            if (GlobalConstants.equipment_no.equalsIgnoreCase(jsonObject.getString("EquipmentNo"))) {

                                jsonObject = jsonarray.getJSONObject(j);

          /* "EquipmentNo": "PRAS0976544","Customer": "TSTCUST","InDate": "1-9-2017 00:00:00","PreviousCargo": "Acetic Acid",
        "LastStatusDate": "6-6-2017 00:00:00","LaborRate": "0.00",
        "LastTestType": "51", "NextTestType": "","LastTestDate": "09-Jan-2017","NextTestDate": "","LastSurveyor": "",
        "ValidityPeriodforTest": "2.5","RepairTypeID": "55","RepairTypeCD": "Regular Repair","Remarks": "", "InvoicingPartyCD": "",
        "InvoicingPartyID": "","InvoicingPartyName": "","GiTransactionNo": "8574","RepairEstimateID": "13747","RevisionNo": "0",
        "CustomerAppRef": "","ApprovalDate": "6-6-2017 00:00:00","PartyAppRef": "","SurveyorName": "",
        "SurveyCompletionDate": "6-6-2017 00:00:00","LineItems": null,"attchement": [],"RepairEstimateNo": "9233",
        "EquipmentStatusId": "9","EquipmentStatusCd": "AAR"*/

                                get_cust = jsonObject.getString("Customer");
                                get_eqp_no = jsonObject.getString("EquipmentNo");
                                get_indate = jsonObject.getString("InDate");
                                get_previous_cargo = jsonObject.getString("PreviousCargo");
                                get_laststatus_date = jsonObject.getString("LastStatusDate");
                                get_lbr_rate = jsonObject.getString("LaborRate");
                                get_last_test_type = jsonObject.getString("LastTestType");
                                get_nexttest_type = jsonObject.getString("NextTestType");
                                get_last_test_date = jsonObject.getString("LastTestDate");
                                get_nexttest_date = jsonObject.getString("NextTestDate");
                                get_lst_survey = jsonObject.getString("LastSurveyor");
                                get_validate_period_test = jsonObject.getString("ValidityPeriodforTest");
                                get_repair_type_id = jsonObject.getString("RepairTypeID");
                                get_repair_type_cd = jsonObject.getString("RepairTypeCD");
                                get_rematk = jsonObject.getString("Remarks");
                                get_invoice_prty_cd = jsonObject.getString("InvoicingPartyCD");
                                get_invoice_prty_id = jsonObject.getString("InvoicingPartyID");
                                get_invoice_prty_name = jsonObject.getString("InvoicingPartyName");
                                get_gi_trans_no = jsonObject.getString("GiTransactionNo");
                                get_repair_est_id = jsonObject.getString("RepairEstimateID");
                                get_revision_no = jsonObject.getString("RevisionNo");
                                get_cust_app_ref_no = jsonObject.getString("CustomerAppRef");
                                get_approval_date = jsonObject.getString("ApprovalDate");
                                get_party_app_ref_no = jsonObject.getString("PartyAppRef");
                                get_survey_name = jsonObject.getString("SurveyorName");
                                get_survey_completion_date = jsonObject.getString("SurveyCompletionDate");
                                get_repair_est_no = jsonObject.getString("RepairEstimateNo");
                                get_rep_status = jsonObject.getString("EquipmentStatusCd");
                                // totel_amount=totel_amount+Integer.parseInt(jsonObject.getString("TotelCost"));

                                Log.i("edit_repair_estimate", get_last_test_type + "-" + get_nexttest_type + "-" + get_lbr_rate + "-" + get_invoice_prty_cd);


                            }


                        }
                    }
                } else if (jsonarray.length() < 1) {
                    runOnUiThread(new Runnable() {

                        @Override
                        public void run() {
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(), "No Records Found");


                        }
                    });

                }
            } catch (Exception e) {
                e.printStackTrace();
            }


            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {


            if (jsonarray != null) {
                ed_estimate_date.setText(In_date);

                ed_original_est_date.setText(get_est_date);
                ed_original_est_date.setClickable(false);
                ed_labor_rate.setText(get_lbr_rate);
                ed_invoicing_party.setText(get_invoice_prty_cd);
                invoice_id.setText(get_invoice_prty_id);
                ed_invoicing_party.setClickable(false);
                ed_last_survey.setText(get_lst_survey);
                ed_next_type.setText(get_nexttest_type);
                ed_next_type.setClickable(false);
                ed_last_testDate.setText(get_last_test_date);
                ed_next_test_date.setText(get_next_test_date);
                ed_next_test_date.setClickable(false);
                ed_validate_period_for_test.setText(get_validate_period_test);

                //  switch_heating.setChecked(true);

            } else {
                shortToast(getApplicationContext(), "Data Not Found");


            }
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
            progressDialog = new ProgressDialog(AddRepair_Estimation_Details_MySubmit.this);
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
                jsonObject.put("pageName","Repair Estimate");
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
