package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.FragmentActivity;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.Customer_Calculation_Bean;
import com.i_tankdepo.Beanclass.Image_Bean;
import com.i_tankdepo.Beanclass.LineItem_Bean;
import com.i_tankdepo.Beanclass.RepairBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.helper.ServiceHandler;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.ParseException;
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
public class Repair_Estimation_Summary extends CommonActivity implements View.OnClickListener {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu, im_down, im_up, im_heat_close, im_heat_ok, attachments, iv_back;


    private TextView tv_toolbarTitle, tv_search_options, no_data;
    LinearLayout LL_hole, LL_heat_submit, LL_search_Value, LL_heat;

    private ProgressDialog progressDialog;
    private String get_ed_remarks, get_cleanline_bets, get_sp_invoice_code, invoice_name, get_sp_repairtype_code, invoice_party_name, repairType_name,
            get_repair_type, get_est_date, get_status, get_sp_last_test_type_id, get_sp_last_test_type, get_original_est_date, get_ed_last_testDate, get_next_test_type_id, get_next_test_type, get_last_survey,
            get_validate_period_for_test, get_next_test_date, get_labor_rate;
    private Intent mServiceIntent;
    int row_count = 0;

    private Button heat_refresh, heat_home, heat_submit, heating, cleaning, inspection;
    private FragmentActivity mycontaxt;
    private TextView tv_lineitem_count, tv_man_hour, tv_manhour_cost, tv_metrial_cost, Iv_manhour, Iv_manhour_cost, Iv_metrial_cost, Iv_total_cost,
            tv_totalCost, customer_name, equipment_no, eqip_type;
    private Button LL_Line_iteam, LL_add_details, LL_attachments, LL_summary;
    private Spinner sp_tarif_group, sp_tarif_code;
    private ImageView iv_changeOfStatus;
    private LinearLayout summary;
    private String lineItem_json, image_json, add_details_json;
    private JSONObject jsonrootObject;
    private JSONArray jsonarray;
    private JSONObject jsonObject;
    String MHC, MHR, TC, MC;
    private ArrayList<LineItem_Bean> lineitem_arraylist;
    private ArrayList<LineItem_Bean> lineitem_arraylist_I;
    private ArrayList<LineItem_Bean> lineitem_arraylist_row_state;
    private LineItem_Bean lineitem_bean;
    private int total = 0;
    private int cust_total = 0;
    private int Iv_totalcost = 0;
    private float man_hour = 0;
    private float man_hour_cost = 0;
    private float metrial_cost = 0;
    private float man_hr_rate = 0;
    private Image_Bean image_bean;
    private String IfAttchment;
    private JSONObject summary_object_head;
    private ArrayList<Image_Bean> Image_arraylist;
    private TextView customer_calc, customer_calc_rec, tv_depo_crr, tv_cust_crr, tv_amount;
    private RepairBean repair_bean;
    private JSONArray LineItems_Json;
    private ArrayList<Image_Bean> encodeArray;
    private String In_date, customer_Id, Line_item_Json, add_detail_jsonobject;
    private JSONObject object2;
    private JSONArray json_array;
    private JSONObject reqObj_lineitem;
    private String get_my_est_date, get_my_original_est_date, get_my_ed_last_testDate, get_my_approval_date, get_my_next_test_date;
    private String get_cust, get_cust_id, get_eqp_no, get_indate, get_previous_cargo, get_repair_type_cd,
            get_repair_type_id, get_laststatus_date, get_validate_period_test, get_lbr_rate, get_nexttest_date,
            get_lst_survey, get_last_test_type, get_nexttest_type, get_last_test_date, get_rematk, get_invoice_prty_cd, get_invoice_prty_id, get_revision_no, get_cust_app_ref_no, get_approval_date, get_Status, get_Status_id, get_invoice_prty_name, get_gi_trans_no,
            get_repair_est_id, get_party_app_ref_no, get_survey_name, get_survey_completion_date, get_rep_status, get_repair_est_no;
    private ArrayList<RepairBean> repair_arraylist;
    private TextView repair_estimate_text;
    private Button repair_completion, repair_estimate, repair_approval, survey_completion;
    private RelativeLayout RL_heating, RL_Repair;
    private ImageView more_info;
    private ImageView iv_send_mail;
    private float Iv_man_hour = 0;
    private float Iv_man_hour_cost = 0;
    private float Iv_metrialCost = 0;
    private float Iv_man_hr_rate = 0;
    private Button notification_text;
    private ArrayList<Customer_Calculation_Bean> Customer_Cal_ArrayList;
    private String CRRNCY_CD, CSTMR_ID, EXCHNG_RT_PR_UNT_NC;
    private String est_id, est_no, gi_transaction_no;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.summary);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);


        more_info = (ImageView) findViewById(R.id.more_info);
        more_info.setOnClickListener(this);
        encodeArray = GlobalConstants.multiple_encodeArray;
        add_detail_jsonobject = GlobalConstants.add_detail_jsonobject;
        Line_item_Json = GlobalConstants.Line_item_Json;

        customer_Id = GlobalConstants.cust_Id;
        In_date = GlobalConstants.indate;
        notification_text = (Button) findViewById(R.id.notification_text);
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
        sp_tarif_group = (Spinner) findViewById(R.id.sp_tarif_group);
        sp_tarif_code = (Spinner) findViewById(R.id.sp_tarif_code);
        repair_estimate_text = (TextView) findViewById(R.id.tv_heating);
        repair_estimate_text.setText("Repair Estimation");
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        attachments = (ImageView) findViewById(R.id.attach_camera);
        iv_back.setOnClickListener(this);
        iv_changeOfStatus = (ImageView) findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        menu.setVisibility(View.GONE);
        LL_Line_iteam = (Button) findViewById(R.id.Line_iteam);
        LL_Line_iteam.setOnClickListener(this);
        LL_add_details = (Button) findViewById(R.id.add_details);
        LL_add_details.setOnClickListener(this);
        LL_attachments = (Button) findViewById(R.id.attachments);
        LL_attachments.setOnClickListener(this);

        LL_summary = (Button) findViewById(R.id.summary);
        summary = (LinearLayout) findViewById(R.id.LL_summary);
        summary.setBackgroundColor(getResources().getColor(R.color.submit));

        LL_summary.setOnClickListener(this);
        LL_summary.setVisibility(View.GONE);
        tv_amount = (TextView) findViewById(R.id.tv_amount);
        tv_cust_crr = (TextView) findViewById(R.id.tv_cust_crr);
        tv_depo_crr = (TextView) findViewById(R.id.tv_depo_crr);
        customer_calc_rec = (TextView) findViewById(R.id.customer_calc_rec);
        customer_calc = (TextView) findViewById(R.id.customer_calc);
        tv_amount.setOnClickListener(this);

        heat_home = (Button) findViewById(R.id.heat_home);
        heat_refresh = (Button) findViewById(R.id.heat_refresh);
        heat_submit = (Button) findViewById(R.id.heat_submit);
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);

        LL_heat_submit.setOnClickListener(this);
        LL_heat = (LinearLayout) findViewById(R.id.LL_heat);
        LL_heat.setOnClickListener(this);

//        tabLayout.setupWithViewPager(viewPager);


        // tabLayout.clearOnTabSelectedListeners();


        heating = (Button) findViewById(R.id.heating);
        cleaning = (Button) findViewById(R.id.cleaning);
        inspection = (Button) findViewById(R.id.inspection);
        heat_submit.setOnClickListener(this);
        inspection.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);

        heat_refresh.setOnClickListener(this);
        heat_home.setOnClickListener(this);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        iv_send_mail = (ImageView) findViewById(R.id.iv_send_mail);
        iv_send_mail.setOnClickListener(this);
        Log.i("customer_name", GlobalConstants.customer_name);
        customer_name = (TextView) findViewById(R.id.text1);
        tv_lineitem_count = (TextView) findViewById(R.id.tv_lineitem_count);
        tv_man_hour = (TextView) findViewById(R.id.tv_man_hour);
        tv_manhour_cost = (TextView) findViewById(R.id.tv_manhour_cost);
        tv_metrial_cost = (TextView) findViewById(R.id.tv_metrial_cost);
        tv_totalCost = (TextView) findViewById(R.id.tv_totalCost);

        Iv_manhour = (TextView) findViewById(R.id.Iv_manhour);
        Iv_manhour_cost = (TextView) findViewById(R.id.Iv_manhour_cost);
        Iv_metrial_cost = (TextView) findViewById(R.id.Iv_metrial_cost);
        Iv_total_cost = (TextView) findViewById(R.id.Iv_total_cost);
        customer_name.setText(GlobalConstants.customer_name + " , " + GlobalConstants.equipment_no + ", " + GlobalConstants.type);
        equipment_no = (TextView) findViewById(R.id.text2);
//        equipment_no.setText(GlobalConstants.equipment_no);
        eqip_type = (TextView) findViewById(R.id.text3);
//        eqip_type.setText(GlobalConstants.previous_cargo);
        tv_toolbarTitle.setText("Summary");

        RL_heating = (RelativeLayout) findViewById(R.id.RL_heating);
        RL_Repair = (RelativeLayout) findViewById(R.id.RL_Repair);
        RL_heating.setVisibility(View.GONE);
        repair_estimate = (Button) findViewById(R.id.repair_estimate);
        repair_estimate.setOnClickListener(this);
        repair_approval = (Button) findViewById(R.id.repair_approval);
        repair_approval.setVisibility(View.GONE);
        repair_completion = (Button) findViewById(R.id.repair_completion);
        repair_completion.setVisibility(View.GONE);
        survey_completion = (Button) findViewById(R.id.survey_completion);
        survey_completion.setVisibility(View.GONE);
        System.out.println("lineItem_json" + Line_item_Json);

        if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

            iv_send_mail.setVisibility(View.VISIBLE);

            try {
                jsonrootObject = new JSONObject(Line_item_Json);
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

                        lineitem_arraylist = new ArrayList<LineItem_Bean>();
                        lineitem_arraylist_I = new ArrayList<LineItem_Bean>();
                        lineitem_arraylist_row_state = new ArrayList<LineItem_Bean>();

                        for (int j = 0; j < jsonarray.length(); j++) {

                            repair_bean = new RepairBean();
                            jsonObject = jsonarray.getJSONObject(j);


                            lineitem_bean = new LineItem_Bean();
                            JSONObject jsonObject_line = jsonarray.getJSONObject(j);

                            lineitem_bean.setRowState(jsonObject_line.getString("RowState"));
                            lineitem_arraylist_row_state.add(lineitem_bean);
                            if (jsonObject_line.getString("ResponsibilityCd").equalsIgnoreCase("I")) {
                                if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {


                                    if (jsonObject_line.getString("RPR_ESTMT_DTL_ID").equals("")) {
                                        lineitem_bean.setRPR_ESTMT_DTL_ID("1");

                                    } else {
                                        lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject_line.getString("RPR_ESTMT_DTL_ID"));

                                    }
                                    if (jsonObject_line.getString("RPR_ESTMT_ID").equals("")) {
                                        lineitem_bean.setRPR_ESTMT_ID("1");

                                    } else {
                                        lineitem_bean.setRPR_ESTMT_ID(jsonObject_line.getString("RPR_ESTMT_ID"));

                                    }

                                } else {
                                    lineitem_bean.setRPR_ESTMT_DTL_ID("1");
                                    lineitem_bean.setRPR_ESTMT_ID("1");
                                }

                                lineitem_bean.setItem(jsonObject_line.getString("Item"));
                                lineitem_bean.setItemCode(jsonObject_line.getString("Item"));
                                lineitem_bean.setRepair_Code(jsonObject_line.getString("Repair"));
                                lineitem_bean.setRepair_Code_Id(jsonObject_line.getString("Repair"));
                                lineitem_bean.setSubItem(jsonObject_line.getString("SubItem"));
                                lineitem_bean.setSuIitemCode(jsonObject_line.getString("SubItem"));
                                lineitem_bean.setDamage_Code(jsonObject_line.getString("Damage"));
                                lineitem_bean.setDamage_Code_Id(jsonObject_line.getString("Damage"));
                                lineitem_bean.setManHour(jsonObject_line.getString("ManHour"));
                                lineitem_bean.setManHour_Cost(jsonObject_line.getString("MHC"));
                                lineitem_bean.setMHR(jsonObject_line.getString("MHR"));
                                lineitem_bean.setMetial_Cost(jsonObject_line.getString("MC"));
                                lineitem_bean.setResponsibility_Cd(jsonObject_line.getString("ResponsibilityCd"));
                                lineitem_bean.setResponsibility(jsonObject_line.getString("Responsibility"));
                                lineitem_bean.setTotel(jsonObject_line.getString("TC"));
                                lineitem_bean.setRemark(jsonObject_line.getString("DmgRprRemarks"));
                                lineitem_bean.setRowState(jsonObject_line.getString("RowState"));
                                lineitem_arraylist_I.add(lineitem_bean);

                            } else {
                                if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {


                                    if (jsonObject_line.getString("RPR_ESTMT_DTL_ID").equals("")) {
                                        lineitem_bean.setRPR_ESTMT_DTL_ID("1");

                                    } else {
                                        lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject_line.getString("RPR_ESTMT_DTL_ID"));

                                    }
                                    if (jsonObject_line.getString("RPR_ESTMT_ID").equals("")) {
                                        lineitem_bean.setRPR_ESTMT_ID("1");

                                    } else {
                                        lineitem_bean.setRPR_ESTMT_ID(jsonObject_line.getString("RPR_ESTMT_ID"));

                                    }

                                } else {
                                    lineitem_bean.setRPR_ESTMT_DTL_ID("1");
                                    lineitem_bean.setRPR_ESTMT_ID("1");
                                }

                                lineitem_bean.setItem(jsonObject_line.getString("Item"));
                                lineitem_bean.setItemCode(jsonObject_line.getString("Item"));
                                lineitem_bean.setRepair_Code(jsonObject_line.getString("Repair"));
                                lineitem_bean.setRepair_Code_Id(jsonObject_line.getString("Repair"));
                                lineitem_bean.setSubItem(jsonObject_line.getString("SubItem"));
                                lineitem_bean.setSuIitemCode(jsonObject_line.getString("SubItem"));
                                lineitem_bean.setDamage_Code(jsonObject_line.getString("Damage"));
                                lineitem_bean.setDamage_Code_Id(jsonObject_line.getString("Damage"));
                                lineitem_bean.setManHour(jsonObject_line.getString("ManHour"));
                                lineitem_bean.setManHour_Cost(jsonObject_line.getString("MHC"));
                                lineitem_bean.setMHR(jsonObject_line.getString("MHR"));
                                lineitem_bean.setMetial_Cost(jsonObject_line.getString("MC"));
                                lineitem_bean.setResponsibility_Cd(jsonObject_line.getString("ResponsibilityCd"));
                                lineitem_bean.setResponsibility(jsonObject_line.getString("Responsibility"));
                                lineitem_bean.setTotel(jsonObject_line.getString("TC"));
                                lineitem_bean.setRemark(jsonObject_line.getString("DmgRprRemarks"));
                                lineitem_bean.setRowState(jsonObject_line.getString("RowState"));

                                lineitem_arraylist.add(lineitem_bean);

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

            } catch (JSONException e) {
                e.printStackTrace();
            }

        } else {

            try {
                jsonrootObject = new JSONObject(Line_item_Json);
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

                        lineitem_arraylist = new ArrayList<LineItem_Bean>();
                        lineitem_arraylist_I = new ArrayList<LineItem_Bean>();
                        lineitem_arraylist_row_state = new ArrayList<LineItem_Bean>();

                        for (int j = 0; j < jsonarray.length(); j++) {

                            repair_bean = new RepairBean();
                            jsonObject = jsonarray.getJSONObject(j);


                            lineitem_bean = new LineItem_Bean();
                            JSONObject jsonObject_line = jsonarray.getJSONObject(j);

                            lineitem_bean.setRowState(jsonObject_line.getString("RowState"));
                            lineitem_arraylist_row_state.add(lineitem_bean);
                            if (jsonObject_line.getString("ResponsibilityCd").equalsIgnoreCase("I")) {
                                if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {


                                    if (jsonObject_line.getString("RPR_ESTMT_DTL_ID").equals("")) {
                                        lineitem_bean.setRPR_ESTMT_DTL_ID("1");

                                    } else {
                                        lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject_line.getString("RPR_ESTMT_DTL_ID"));

                                    }
                                    if (jsonObject_line.getString("RPR_ESTMT_ID").equals("")) {
                                        lineitem_bean.setRPR_ESTMT_ID("1");

                                    } else {
                                        lineitem_bean.setRPR_ESTMT_ID(jsonObject_line.getString("RPR_ESTMT_ID"));

                                    }

                                } else {
                                    lineitem_bean.setRPR_ESTMT_DTL_ID("1");
                                    lineitem_bean.setRPR_ESTMT_ID("1");
                                }

                                lineitem_bean.setItem(jsonObject_line.getString("Item"));
                                lineitem_bean.setItemCode(jsonObject_line.getString("Item"));
                                lineitem_bean.setRepair_Code(jsonObject_line.getString("Repair"));
                                lineitem_bean.setRepair_Code_Id(jsonObject_line.getString("Repair"));
                                lineitem_bean.setSubItem(jsonObject_line.getString("SubItem"));
                                lineitem_bean.setSuIitemCode(jsonObject_line.getString("SubItem"));
                                lineitem_bean.setDamage_Code(jsonObject_line.getString("Damage"));
                                lineitem_bean.setDamage_Code_Id(jsonObject_line.getString("Damage"));
                                lineitem_bean.setManHour(jsonObject_line.getString("ManHour"));
                                lineitem_bean.setManHour_Cost(jsonObject_line.getString("MHC"));
                                lineitem_bean.setMHR(jsonObject_line.getString("MHR"));
                                lineitem_bean.setMetial_Cost(jsonObject_line.getString("MC"));
                                lineitem_bean.setResponsibility_Cd(jsonObject_line.getString("ResponsibilityCd"));
                                lineitem_bean.setResponsibility(jsonObject_line.getString("Responsibility"));
                                lineitem_bean.setTotel(jsonObject_line.getString("TC"));
                                lineitem_bean.setRemark(jsonObject_line.getString("DmgRprRemarks"));
                                lineitem_bean.setRowState(jsonObject_line.getString("RowState"));

                                lineitem_arraylist_I.add(lineitem_bean);

                            } else {
                                if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                        || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {


                                    if (jsonObject_line.getString("RPR_ESTMT_DTL_ID").equals("")) {
                                        lineitem_bean.setRPR_ESTMT_DTL_ID("1");

                                    } else {
                                        lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject_line.getString("RPR_ESTMT_DTL_ID"));

                                    }
                                    if (jsonObject_line.getString("RPR_ESTMT_ID").equals("")) {
                                        lineitem_bean.setRPR_ESTMT_ID("1");

                                    } else {
                                        lineitem_bean.setRPR_ESTMT_ID(jsonObject_line.getString("RPR_ESTMT_ID"));

                                    }

                                } else {
                                    lineitem_bean.setRPR_ESTMT_DTL_ID("1");
                                    lineitem_bean.setRPR_ESTMT_ID("1");
                                }

                                lineitem_bean.setItem(jsonObject_line.getString("Item"));
                                lineitem_bean.setItemCode(jsonObject_line.getString("Item"));
                                lineitem_bean.setRepair_Code(jsonObject_line.getString("Repair"));
                                lineitem_bean.setRepair_Code_Id(jsonObject_line.getString("Repair"));
                                lineitem_bean.setSubItem(jsonObject_line.getString("SubItem"));
                                lineitem_bean.setSuIitemCode(jsonObject_line.getString("SubItem"));
                                lineitem_bean.setDamage_Code(jsonObject_line.getString("Damage"));
                                lineitem_bean.setDamage_Code_Id(jsonObject_line.getString("Damage"));
                                lineitem_bean.setManHour(jsonObject_line.getString("ManHour"));
                                lineitem_bean.setManHour_Cost(jsonObject_line.getString("MHC"));
                                lineitem_bean.setMHR(jsonObject_line.getString("MHR"));
                                lineitem_bean.setMetial_Cost(jsonObject_line.getString("MC"));
                                lineitem_bean.setResponsibility_Cd(jsonObject_line.getString("ResponsibilityCd"));
                                lineitem_bean.setResponsibility(jsonObject_line.getString("Responsibility"));
                                lineitem_bean.setTotel(jsonObject_line.getString("TC"));
                                lineitem_bean.setRemark(jsonObject_line.getString("DmgRprRemarks"));
                                lineitem_bean.setRowState(jsonObject_line.getString("RowState"));

                                lineitem_arraylist.add(lineitem_bean);

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

            } catch (JSONException e) {
                e.printStackTrace();
            }

        }
        try {
            tv_lineitem_count.setText(String.valueOf(lineitem_arraylist.size() + lineitem_arraylist_I.size()));

        } catch (Exception e) {

        }
        if (lineitem_arraylist != null) {
            for (int i = 0; i < lineitem_arraylist.size(); i++) {
                TC = lineitem_arraylist.get(i).getTotel();
                total = total + (int) (Float.parseFloat(TC));
                cust_total = cust_total + (int) (Float.parseFloat(TC));
                //    man_hour=man_hour+Integer.parseInt(lineitem_arraylist.get(i).getManHour());
                if (lineitem_arraylist.get(i).getManHour().equals("")) {
                    man_hour = 0.0f;
                } else {
                    man_hour = man_hour + Float.valueOf(lineitem_arraylist.get(i).getManHour());
                }
                if (lineitem_arraylist.get(i).getManHour_Cost().length() > 0) {
                    man_hour_cost = man_hour_cost + Float.valueOf(lineitem_arraylist.get(i).getManHour_Cost());

                } else {
                    man_hour_cost = 0.0f;

                }
                if (lineitem_arraylist.get(i).getMetial_Cost().length() > 0) {
                    metrial_cost = metrial_cost + Float.valueOf(lineitem_arraylist.get(i).getMetial_Cost());

                } else {
                    metrial_cost = 0.0f;

                }
                if (lineitem_arraylist.get(i).getMHR().length() > 0) {
                    man_hr_rate = man_hr_rate + Float.valueOf(lineitem_arraylist.get(i).getMHR());

                } else {
                    man_hr_rate = 0.0f;

                }
            }
        }
        if (lineitem_arraylist_I != null) {
            if (lineitem_arraylist_I.size() > 0) {
                for (int i = 0; i < lineitem_arraylist_I.size(); i++) {
                    TC = lineitem_arraylist_I.get(i).getTotel();
                    total = total + (int) (Float.parseFloat(TC));
                    Iv_totalcost = Iv_totalcost + (int) (Float.parseFloat(TC));
                    //    man_hour=man_hour+Integer.parseInt(lineitem_arraylist.get(i).getManHour());
                    if (lineitem_arraylist_I.get(i).getManHour().equals("")) {
                        Iv_man_hour = 0.0f;
                    } else {
                        Iv_man_hour = Iv_man_hour + Float.valueOf(lineitem_arraylist_I.get(i).getManHour());
                    }
                    if (lineitem_arraylist_I.get(i).getManHour_Cost().length() > 0) {
                        Iv_man_hour_cost = Iv_man_hour_cost + Float.valueOf(lineitem_arraylist_I.get(i).getManHour_Cost());

                    } else {
                        Iv_man_hour_cost = 0.0f;

                    }
                    if (lineitem_arraylist_I.get(i).getMetial_Cost().length() > 0) {
                        Iv_metrialCost = Iv_metrialCost + Float.valueOf(lineitem_arraylist_I.get(i).getMetial_Cost());

                    } else {
                        Iv_metrialCost = 0.0f;

                    }
                    if (lineitem_arraylist_I.get(i).getMHR().length() > 0) {
                        Iv_man_hr_rate = Iv_man_hr_rate + Float.valueOf(lineitem_arraylist_I.get(i).getMHR());

                    } else {
                        Iv_man_hr_rate = 0.0f;

                    }
                }
                Iv_total_cost.setText(String.valueOf(Iv_totalcost).replace("-", "") + ".00");
                Iv_manhour.setText(String.valueOf(Iv_man_hour) + "0");
                String MH_tc = String.valueOf((int) Iv_man_hour_cost).replace(".", "");
                tv_manhour_cost.setText(MH_tc.replace("E", "").replace("-", "") + ".00");
//                Iv_manhour_cost.setText(String.valueOf(Iv_man_hour_cost) + "0");
                Iv_metrial_cost.setText(String.valueOf(Iv_metrialCost).replace("E", "").replace(".", "").replace("-", ""));

            }
        } else {

        }
        Log.i("Totel Cost===", String.valueOf(total));
        GlobalConstants.estimate_original_totel_amount = total;
        tv_totalCost.setText(String.valueOf(cust_total).replace("-", "") + ".00");
        tv_cust_crr.setText(String.valueOf(cust_total).replace("-", "") + ".00" + " " + GlobalConstants.currency);
        tv_depo_crr.setText(String.valueOf(cust_total).replace("-", "") + ".00" + " " + GlobalConstants.currency);
        tv_amount.setText(String.valueOf(total).replace("-", "") + ".00" + " " + GlobalConstants.currency);
        tv_man_hour.setText(String.valueOf(man_hour) + "0");

        String MH_tc = String.valueOf((int) man_hour_cost).replace(".", "");
        tv_manhour_cost.setText(MH_tc.replace("E", "").replace("-", "") + ".00");
        tv_metrial_cost.setText(String.valueOf(metrial_cost).replace("E", "").replace(".", "").replace("-", ""));

        if (cd.isConnectingToInternet()) {
            new Calculation().execute();
        } else {
            shortToast(getApplicationContext(), "Please Check Your Internet ");
        }

        JSONObject summary_object = new JSONObject();
        summary_object_head = new JSONObject();
        JSONArray summary_array = new JSONArray();
        try {

            /*"MH" :"2520.00",
              "MHC" :"0.0000",
              "MC" :"85.00",
              "TC" :"85.0000",
              "MHR" :"0.00",
              "ResponsibiltyID" :"1",
               "MHCSTSummary" :"1",
                "ResponsibiltyCD" :"C"*/
            summary_object.put("MH", String.valueOf(man_hour));
            summary_object.put("MHC", String.valueOf(man_hour_cost).replace("E", "").replace(".", "").replace("-", ""));
            summary_object.put("MC", String.valueOf(metrial_cost));
            summary_object.put("TC", String.valueOf(total + ".00"));
            summary_object.put("MHR", String.valueOf(man_hr_rate));
            summary_object.put("ResponsibiltyID", "1");
            summary_object.put("MHCSTSummary", "1");
            summary_object.put("ResponsibiltyCD", "C");
            summary_array.put(summary_object);
            summary_object_head.put("LineSummaryDetail", summary_array);
        } catch (Exception e) {
            e.printStackTrace();
        }

        Image_arraylist = GlobalConstants.multiple_encodeArray;

        add_details_json = GlobalConstants.add_detail_jsonobject;
        System.out.println("add_json" + add_details_json);

        try {

            if (add_details_json.equals("")) {


                get_est_date = GlobalConstants.indate;
                get_repair_type = GlobalConstants.repair_TypeCD;
                invoice_party_name = GlobalConstants.invoice_PartyCD;

//                    invoice_PartyID
                get_original_est_date = "";
                get_sp_last_test_type = GlobalConstants.last_testType;
                get_sp_last_test_type_id = GlobalConstants.next_testType;
                get_next_test_type = GlobalConstants.next_testType;
                if (GlobalConstants.next_testType.equalsIgnoreCase("Pneumatic")) {
                    get_next_test_type_id = "52";
                    get_sp_last_test_type_id = "51";
                } else {
                    get_next_test_type_id = "51";
                    get_sp_last_test_type_id = "52";
                }
                get_ed_last_testDate = GlobalConstants.last_testDate;
                get_last_survey = GlobalConstants.lastSurveyor;
                get_next_test_date = GlobalConstants.next_testDate;
                get_validate_period_for_test = GlobalConstants.ValidityPeriodforTest;
                get_labor_rate = GlobalConstants.lobor_rate;
                get_ed_remarks = GlobalConstants.remark;
                get_cleanline_bets = "False";
                get_sp_invoice_code = GlobalConstants.invoice_PartyID;
                get_sp_repairtype_code = GlobalConstants.repair_TypeID;
                try {
                    if (invoice_party_name.equals("")) {
                        invoice_party_name = "";
                    } else {
                        invoice_party_name = GlobalConstants.invoice_PartyCD;

                    }
                    if (get_sp_invoice_code.equals("")) {
                        get_sp_invoice_code = "0";
                    } else {
                        get_sp_invoice_code = GlobalConstants.invoice_PartyID;

                    }
                    if (get_repair_type.equals("")) {
                        get_repair_type = "";
                    } else {
                        get_repair_type = GlobalConstants.repair_TypeCD;

                    }
                    if (get_sp_repairtype_code.equals("")) {
                        get_sp_repairtype_code = "0";
                    } else {
                        get_sp_repairtype_code = GlobalConstants.repair_TypeID;

                    }
                } catch (Exception e) {
                    get_sp_repairtype_code = "0";
                    get_sp_invoice_code = "0";
                    get_repair_type = "";
                    invoice_party_name = "";

                }


            } else {
                jsonrootObject = new JSONObject(add_details_json);
                //  JSONObject getJsonObject = jsonrootObject.getJSONObject("d");
                //   jsonarray = getJsonObject.getJSONArray("ArrayOfFileParams");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list" + jsonarray);
                    if (jsonrootObject.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found");
                            }
                        });
                    } else {
                    /* obiect.put("get_est_date", get_est_date);
                    obiect.put("InvoiceParty_name", InvoiceParty_name);
                    obiect.put("get_sp_repairtype_code", get_sp_repairtype_code);
                    obiect.put("invoice_name", invoice_name);
                    obiect.put("get_status", get_status);
                    obiect.put("get_original_est_date", get_original_est_date);
                    obiect.put("get_sp_last_test_type", get_sp_last_test_type);
                    obiect.put("get_next_test_type", get_next_test_type);
                    obiect.put("get_last_survey", get_last_survey);
                    obiect.put("get_validate_period_for_test", get_validate_period_for_test);
                    obiect.put("get_labor_rate", get_labor_rate);
                    obiect.put("get_ed_remarks", get_ed_remarks);
                    obiect.put("get_cleanline_bets", get_cleanline_bets);*/

                       /*  obiect.put("get_repair_type", get_repair_type);
                                obiect.put("get_sp_repairtype_code", get_sp_repairtype_code);*/


                        get_est_date = jsonrootObject.getString("get_est_date");
                        get_repair_type = jsonrootObject.getString("get_repair_type");
                        invoice_party_name = GlobalConstants.invoice_PartyCD;

//                    invoice_PartyID
                        get_status = jsonrootObject.getString("get_status");
                        get_original_est_date = jsonrootObject.getString("get_original_est_date");
                        get_sp_last_test_type = jsonrootObject.getString("get_sp_last_test_type");
                        get_sp_last_test_type_id = jsonrootObject.getString("get_sp_last_test_type_id");
                        get_next_test_type = jsonrootObject.getString("get_next_test_type");
                        get_next_test_type_id = jsonrootObject.getString("get_next_test_type_id");
                        get_ed_last_testDate = jsonrootObject.getString("get_ed_last_testDate");
                        get_last_survey = jsonrootObject.getString("get_last_survey");
                        get_next_test_date = jsonrootObject.getString("get_next_test_date");
                        get_validate_period_for_test = jsonrootObject.getString("get_validate_period_for_test");
                        get_labor_rate = jsonrootObject.getString("get_labor_rate");
                        get_ed_remarks = jsonrootObject.getString("get_ed_remarks");
                        get_cleanline_bets = jsonrootObject.getString("get_cleanline_bets");
                        get_sp_invoice_code = jsonrootObject.getString("get_sp_invoice_code");
                        get_sp_repairtype_code = jsonrootObject.getString("get_sp_repairtype_code");
                        try {
                            if (invoice_party_name.equals("")) {
                                if(GlobalConstants.Invoice_party > 0)
                                {
                                    invoice_party_name =GlobalConstants.invoice_PartyCD;
                                }else
                                {
                                    invoice_party_name = "";
                                }

                            } else {
                                invoice_party_name = jsonrootObject.getString("InvoiceParty_name");

                            }
                            if (get_sp_invoice_code.equals("")) {
                                if(GlobalConstants.Invoice_party > 0)
                                {
                                    get_sp_invoice_code =GlobalConstants.invoice_PartyID;
                                }else
                                {
                                    get_sp_invoice_code = "0";
                                }

                            } else {
                                get_sp_invoice_code = jsonrootObject.getString("get_sp_invoice_code");

                            }
                            if (get_repair_type.equals("")) {
                                get_repair_type = "";
                            } else {
                                get_repair_type = jsonrootObject.getString("get_repair_type");

                            }
                            if (get_sp_repairtype_code.equals("")) {
                                get_sp_repairtype_code = "0";
                            } else {
                                get_sp_repairtype_code = jsonrootObject.getString("get_sp_repairtype_code");

                            }
                        } catch (Exception e) {
                            get_sp_repairtype_code = "0";
                            get_sp_invoice_code = "0";
                            get_repair_type = "";
                            invoice_party_name = "";

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
            }
        } catch (JSONException e) {
            e.printStackTrace();
        }


        drawer = (DrawerLayout) findViewById(R.id.drawer_layout);


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
            case R.id.Line_iteam:
                finish();
                if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {
                    GlobalConstants.from = "MySubmitRepair_Summary";

                } else {
                    GlobalConstants.from = "AddDetails";

                }
                encodeArray = GlobalConstants.multiple_encodeArray;
                GlobalConstants.multiple_encodeArray = encodeArray;
                GlobalConstants.Line_item_Json = Line_item_Json;
                String count = GlobalConstants.attach_count;
                GlobalConstants.attach_count = count;
                est_id = GlobalConstants.repair_EstimateID;
                est_no = GlobalConstants.repairEstimateNo;
                gi_transaction_no = GlobalConstants.gi_trans_no;
                GlobalConstants.repair_EstimateID = est_id;
                GlobalConstants.repair_EstimateNo = est_no;
                GlobalConstants.gi_trans_no = gi_transaction_no;
                int Invoice_party=GlobalConstants.Invoice_party;
                GlobalConstants.Invoice_party=Invoice_party;
                GlobalConstants.summaryfrom = "summaryfrom";
                finish();

                startActivity(new Intent(getApplicationContext(), Repair_Estimation_wizard.class));

                break;
            case R.id.iv_back:
                onBackPressed();
                break;
            case R.id.iv_send_mail:

             /*   GlobalConstants.print_string= String.valueOf(print_object);
                startActivity(new Intent(getApplicationContext(),SendMailActivity.class));*/
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
                }

                break;
            case R.id.repair_estimate:
                finish();
                startActivity(new Intent(getApplicationContext(), Repair_MainActivity.class));
                break;
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(), MainActivity.class));
                break;
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.add_details:
                finish();
                if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {
                    GlobalConstants.from = "MySubmitRepair_Summary";

                } else {
                    GlobalConstants.from = "AddDetails";

                }
                count = GlobalConstants.attach_count;
                GlobalConstants.attach_count = count;
                encodeArray = GlobalConstants.multiple_encodeArray;
                GlobalConstants.multiple_encodeArray = encodeArray;
                GlobalConstants.Line_item_Json = Line_item_Json;
                est_id = GlobalConstants.repair_EstimateID;
                est_no = GlobalConstants.repairEstimateNo;
                gi_transaction_no = GlobalConstants.gi_trans_no;
                GlobalConstants.repair_EstimateID = est_id;
                GlobalConstants.repair_EstimateNo = est_no;
                GlobalConstants.gi_trans_no = gi_transaction_no;
                GlobalConstants.summaryfrom = "summaryfrom";
                 Invoice_party=GlobalConstants.Invoice_party;
                GlobalConstants.Invoice_party=Invoice_party;
                finish();

                startActivity(new Intent(getApplicationContext(), AddRepair_Estimation_Details.class));

                break;
            case R.id.more_info:

                popUp_equipment_info(GlobalConstants.equipment_no, GlobalConstants.status, GlobalConstants.status_id, GlobalConstants.previous_cargo, "", "", "", "");


                break;
            case R.id.heat_submit:
                try {


                    for (int i = 0; i < lineitem_arraylist_row_state.size(); i++) {
                        String row_state = lineitem_arraylist_row_state.get(i).getRowState();
                        if (row_state.equalsIgnoreCase("Deleted")) {
                            row_count = row_count + 1;
                        }
                    }
                    if (row_count == lineitem_arraylist_row_state.size()) {

                        shortToast(getApplicationContext(), "Please select Atleast One LineItem");

                    } else {
                        if (!Line_item_Json.equals("") || !Line_item_Json.equals(null)) {
                            try {
                                if (get_est_date.equals("")) {


                                    shortToast(getApplicationContext(), "Please key-in Mandate Fields");

                                } else {

                                    if ((lineitem_arraylist_I.size()> 0)  ) {

                                        if((!GlobalConstants.invoice_PartyCD.equals("")))
                                        {
                                        json_array = new JSONArray();
                                        reqObj_lineitem = new JSONObject();
                                        GlobalConstants.lineItem_beanArrayList = lineitem_arraylist;
                                        try {

                                            if (lineitem_arraylist.size() > 0 || lineitem_arraylist_I.size() > 0) {
                                                for (int i = 0; i < lineitem_arraylist.size(); i++) {

                                                    object2 = new JSONObject();
                                                    object2.put("RPR_ESTMT_DTL_ID", lineitem_arraylist.get(i).getRPR_ESTMT_DTL_ID());
                                                    object2.put("RPR_ESTMT_ID", lineitem_arraylist.get(i).getRPR_ESTMT_ID());
                                                    object2.put("ItemCd", lineitem_arraylist.get(i).getItem());
                                                    object2.put("Item", lineitem_arraylist.get(i).getItemCode());
                                                    object2.put("RepairCd", lineitem_arraylist.get(i).getRepair_Code());
                                                    object2.put("Repair", lineitem_arraylist.get(i).getRepair_Code_Id());
                                                    object2.put("SubItemCd", lineitem_arraylist.get(i).getSubItem());
                                                    object2.put("SubItem", lineitem_arraylist.get(i).getSuIitemCode());
                                                    object2.put("DamageCd", lineitem_arraylist.get(i).getDamage_Code());
                                                    object2.put("Damage", lineitem_arraylist.get(i).getDamage_Code_Id());
                                                    object2.put("ManHour", lineitem_arraylist.get(i).getManHour());
                                                    object2.put("MHC", lineitem_arraylist.get(i).getManHour_Cost());
                                                    object2.put("MHR", lineitem_arraylist.get(i).getMHR());
                                                    object2.put("MC", lineitem_arraylist.get(i).getMetial_Cost());
                                                    object2.put("ResponsibilityCd", lineitem_arraylist.get(i).getResponsibility_Cd());
                                                    object2.put("Responsibility", lineitem_arraylist.get(i).getResponsibility());
                                                    object2.put("TC", lineitem_arraylist.get(i).getTotel());
                                                    object2.put("DmgRprRemarks", lineitem_arraylist.get(i).getRemark());
                                                    object2.put("DamageDescription", lineitem_arraylist.get(i).getDamage_Code_Id());
                                                    object2.put("RepairDescription", lineitem_arraylist.get(i).getRepair_Code());
                                                    object2.put("RowState", lineitem_arraylist.get(i).getRowState());
//                                     lineitem_bean.setRowState(jsonObject_line.getString("RowState"));
                                                    object2.put("CheckButton", "0");
                                                    object2.put("ChangingBit", "False");
                                                    json_array.put(object2);


                                                }
                                                for (int i = 0; i < lineitem_arraylist_I.size(); i++) {


                                                    object2 = new JSONObject();
                                                    object2.put("RPR_ESTMT_DTL_ID", lineitem_arraylist_I.get(i).getRPR_ESTMT_DTL_ID());
                                                    object2.put("RPR_ESTMT_ID", lineitem_arraylist_I.get(i).getRPR_ESTMT_ID());
                                                    object2.put("ItemCd", lineitem_arraylist_I.get(i).getItem());
                                                    object2.put("Item", lineitem_arraylist_I.get(i).getItemCode());
                                                    object2.put("RepairCd", lineitem_arraylist_I.get(i).getRepair_Code());
                                                    object2.put("Repair", lineitem_arraylist_I.get(i).getRepair_Code_Id());
                                                    object2.put("SubItemCd", lineitem_arraylist_I.get(i).getSubItem());
                                                    object2.put("SubItem", lineitem_arraylist_I.get(i).getSuIitemCode());
                                                    object2.put("DamageCd", lineitem_arraylist_I.get(i).getDamage_Code());
                                                    object2.put("Damage", lineitem_arraylist_I.get(i).getDamage_Code_Id());
                                                    object2.put("ManHour", lineitem_arraylist_I.get(i).getManHour());
                                                    object2.put("MHC", lineitem_arraylist_I.get(i).getManHour_Cost());
                                                    object2.put("MHR", lineitem_arraylist_I.get(i).getMHR());
                                                    object2.put("MC", lineitem_arraylist_I.get(i).getMetial_Cost());
                                                    object2.put("ResponsibilityCd", lineitem_arraylist_I.get(i).getResponsibility_Cd());
                                                    object2.put("Responsibility", lineitem_arraylist_I.get(i).getResponsibility());
                                                    object2.put("TC", lineitem_arraylist_I.get(i).getTotel());
                                                    object2.put("DmgRprRemarks", lineitem_arraylist_I.get(i).getRemark());
                                                    object2.put("DamageDescription", lineitem_arraylist_I.get(i).getDamage_Code_Id());
                                                    object2.put("RepairDescription", lineitem_arraylist_I.get(i).getRepair_Code());
                                                    object2.put("RowState", lineitem_arraylist_I.get(i).getRowState());
                                                    object2.put("CheckButton", "0");
                                                    try {
                                                        if (!lineitem_arraylist_I.get(i).getChangingBit().equals(null) || lineitem_arraylist_I.get(i).getChangingBit().equals("")) {
                                                            object2.put("ChangingBit", lineitem_arraylist_I.get(i).getChangingBit());

                                                        } else {
                                                            object2.put("ChangingBit", "False");
                                                        }
                                                    } catch (Exception e) {
                                                        object2.put("ChangingBit", "False");

                                                    }
                                                    json_array.put(object2);


                                                }

                                            } else {
                                                shortToast(getApplicationContext(), "Please select Atleast One LineItem");
                                            }


                                            reqObj_lineitem.put("LineItemList", json_array);
                                            Log.d("reqObj_lineitem", String.valueOf(reqObj_lineitem));


                                            new PostInfo().execute();

                                        } catch (Exception e) {
                                            e.printStackTrace();

                                        }
                                    }else {
                                            shortToast(getApplicationContext(), "Please key-in Mandate Fields");
                                        }
                                    } else
                                    {
                                        json_array = new JSONArray();
                                        reqObj_lineitem = new JSONObject();
                                        GlobalConstants.lineItem_beanArrayList = lineitem_arraylist;
                                        try {

                                            if (lineitem_arraylist.size() > 0 || lineitem_arraylist_I.size() > 0) {
                                                for (int i = 0; i < lineitem_arraylist.size(); i++) {

                                                    object2 = new JSONObject();
                                                    object2.put("RPR_ESTMT_DTL_ID", lineitem_arraylist.get(i).getRPR_ESTMT_DTL_ID());
                                                    object2.put("RPR_ESTMT_ID", lineitem_arraylist.get(i).getRPR_ESTMT_ID());
                                                    object2.put("ItemCd", lineitem_arraylist.get(i).getItem());
                                                    object2.put("Item", lineitem_arraylist.get(i).getItemCode());
                                                    object2.put("RepairCd", lineitem_arraylist.get(i).getRepair_Code());
                                                    object2.put("Repair", lineitem_arraylist.get(i).getRepair_Code_Id());
                                                    object2.put("SubItemCd", lineitem_arraylist.get(i).getSubItem());
                                                    object2.put("SubItem", lineitem_arraylist.get(i).getSuIitemCode());
                                                    object2.put("DamageCd", lineitem_arraylist.get(i).getDamage_Code());
                                                    object2.put("Damage", lineitem_arraylist.get(i).getDamage_Code_Id());
                                                    object2.put("ManHour", lineitem_arraylist.get(i).getManHour());
                                                    object2.put("MHC", lineitem_arraylist.get(i).getManHour_Cost());
                                                    object2.put("MHR", lineitem_arraylist.get(i).getMHR());
                                                    object2.put("MC", lineitem_arraylist.get(i).getMetial_Cost());
                                                    object2.put("ResponsibilityCd", lineitem_arraylist.get(i).getResponsibility_Cd());
                                                    object2.put("Responsibility", lineitem_arraylist.get(i).getResponsibility());
                                                    object2.put("TC", lineitem_arraylist.get(i).getTotel());
                                                    object2.put("DmgRprRemarks", lineitem_arraylist.get(i).getRemark());
                                                    object2.put("DamageDescription", lineitem_arraylist.get(i).getDamage_Code_Id());
                                                    object2.put("RepairDescription", lineitem_arraylist.get(i).getRepair_Code());
                                                    object2.put("RowState", lineitem_arraylist.get(i).getRowState());
//                                     lineitem_bean.setRowState(jsonObject_line.getString("RowState"));
                                                    object2.put("CheckButton", "0");
                                                    object2.put("ChangingBit", "False");
                                                    json_array.put(object2);


                                                }
                                                for (int i = 0; i < lineitem_arraylist_I.size(); i++) {


                                                    object2 = new JSONObject();
                                                    object2.put("RPR_ESTMT_DTL_ID", lineitem_arraylist_I.get(i).getRPR_ESTMT_DTL_ID());
                                                    object2.put("RPR_ESTMT_ID", lineitem_arraylist_I.get(i).getRPR_ESTMT_ID());
                                                    object2.put("ItemCd", lineitem_arraylist_I.get(i).getItem());
                                                    object2.put("Item", lineitem_arraylist_I.get(i).getItemCode());
                                                    object2.put("RepairCd", lineitem_arraylist_I.get(i).getRepair_Code());
                                                    object2.put("Repair", lineitem_arraylist_I.get(i).getRepair_Code_Id());
                                                    object2.put("SubItemCd", lineitem_arraylist_I.get(i).getSubItem());
                                                    object2.put("SubItem", lineitem_arraylist_I.get(i).getSuIitemCode());
                                                    object2.put("DamageCd", lineitem_arraylist_I.get(i).getDamage_Code());
                                                    object2.put("Damage", lineitem_arraylist_I.get(i).getDamage_Code_Id());
                                                    object2.put("ManHour", lineitem_arraylist_I.get(i).getManHour());
                                                    object2.put("MHC", lineitem_arraylist_I.get(i).getManHour_Cost());
                                                    object2.put("MHR", lineitem_arraylist_I.get(i).getMHR());
                                                    object2.put("MC", lineitem_arraylist_I.get(i).getMetial_Cost());
                                                    object2.put("ResponsibilityCd", lineitem_arraylist_I.get(i).getResponsibility_Cd());
                                                    object2.put("Responsibility", lineitem_arraylist_I.get(i).getResponsibility());
                                                    object2.put("TC", lineitem_arraylist_I.get(i).getTotel());
                                                    object2.put("DmgRprRemarks", lineitem_arraylist_I.get(i).getRemark());
                                                    object2.put("DamageDescription", lineitem_arraylist_I.get(i).getDamage_Code_Id());
                                                    object2.put("RepairDescription", lineitem_arraylist_I.get(i).getRepair_Code());
                                                    object2.put("RowState", lineitem_arraylist_I.get(i).getRowState());
                                                    object2.put("CheckButton", "0");
                                                    try {
                                                        if (!lineitem_arraylist_I.get(i).getChangingBit().equals(null) || lineitem_arraylist_I.get(i).getChangingBit().equals("")) {
                                                            object2.put("ChangingBit", lineitem_arraylist_I.get(i).getChangingBit());

                                                        } else {
                                                            object2.put("ChangingBit", "False");
                                                        }
                                                    } catch (Exception e) {
                                                        object2.put("ChangingBit", "False");

                                                    }
                                                    json_array.put(object2);


                                                }

                                            } else {
                                                shortToast(getApplicationContext(), "Please select Atleast One LineItem");
                                            }


                                            reqObj_lineitem.put("LineItemList", json_array);
                                            Log.d("reqObj_lineitem", String.valueOf(reqObj_lineitem));


                                            new PostInfo().execute();

                                        } catch (Exception e) {
                                            e.printStackTrace();

                                        }
                                    }
                                }
                            } catch (Exception e) {
                                shortToast(getApplicationContext(), "Please key-in Mandate Fields");
                                e.printStackTrace();
                            }
                        } else {
                            shortToast(getApplicationContext(), "Please select Atleast One LineItem");
                        }
                    }
                } catch (Exception e) {
                    shortToast(getApplicationContext(), "Please select Atleast One LineItem");
                    Log.d("Exception", String.valueOf(e));
                    e.printStackTrace();
                }

                break;
            case R.id.attachments:
                finish();
                if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {
                    GlobalConstants.from = "MySubmitRepair_Summary";

                } else {
                    GlobalConstants.from = "AddDetails";

                }
                encodeArray = GlobalConstants.multiple_encodeArray;
                GlobalConstants.multiple_encodeArray = encodeArray;
                GlobalConstants.summaryfrom = "summaryfrom";
                GlobalConstants.Line_item_Json = Line_item_Json;
                est_id = GlobalConstants.repair_EstimateID;
                est_no = GlobalConstants.repairEstimateNo;
                gi_transaction_no = GlobalConstants.gi_trans_no;
                GlobalConstants.repair_EstimateID = est_id;
                 Invoice_party=GlobalConstants.Invoice_party;
                GlobalConstants.Invoice_party=Invoice_party;
                GlobalConstants.repair_EstimateNo = est_no;
                GlobalConstants.gi_trans_no = gi_transaction_no;

                startActivity(new Intent(getApplicationContext(), Attach_Repair_Estimation.class));

                break;
            case R.id.iv_changeOfStatus:
                startActivity(new Intent(getApplicationContext(), ChangeOfStatus.class));

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

    public class PostInfo extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        private JSONArray image_jsonlist;
        private JSONObject imagejsonObject;
        private JSONObject reqObj;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Summary.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLRepair_Update);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                JSONObject jsonObject = new JSONObject();

                image_jsonlist = new JSONArray();
                reqObj = new JSONObject();

                try {
                /*
                    imagejsonObject = new JSONObject();
                    imagejsonObject.put("FileName", filename);
                    imagejsonObject.put("ContentLength",encodedImage.length());
                    imagejsonObject.put("base64imageString",encodedImage);
                    image_jsonlist.put(imagejsonObject);
                    reqObj.put("ArrayOfFileParams",image_jsonlist);*/
                    if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                        for (int i = 0; i < Image_arraylist.size(); i++) {

                            imagejsonObject = new JSONObject();
                            if (Image_arraylist.get(i).getAttachment_Id().equals("")) {
                                imagejsonObject.put("FileName", Image_arraylist.get(i).getImage_Name());
                                imagejsonObject.put("ContentLength", Image_arraylist.get(i).getImage_length());
                                imagejsonObject.put("base64imageString", Image_arraylist.get(i).getBase64());

                            } else {
                                imagejsonObject.put("FileName", Image_arraylist.get(i).getImage_Name());
                                imagejsonObject.put("base64imageString", Image_arraylist.get(i).getUriPath());
                            }

                            image_jsonlist.put(imagejsonObject);
                        }

                    } else {
                        for (int i = 0; i < Image_arraylist.size(); i++) {

                            imagejsonObject = new JSONObject();
                            imagejsonObject.put("FileName", Image_arraylist.get(i).getImage_Name());
                            imagejsonObject.put("ContentLength", Image_arraylist.get(i).getImage_length());
                            imagejsonObject.put("base64imageString", Image_arraylist.get(i).getBase64());

                            image_jsonlist.put(imagejsonObject);
                        }

                    }


                } catch (Exception e) {

                    Log.i("Exception ", String.valueOf(e));
                }
                Log.d("reqObj_lineitem", String.valueOf(reqObj_lineitem));


                try {
                    if (Image_arraylist.size() < 0) {

                    } else {
                        IfAttchment = "True";
                    }
                } catch (Exception e) {

                    IfAttchment = "False";
                }
                SimpleDateFormat fromUser = new SimpleDateFormat("dd-MM-yyyy", Locale.ENGLISH);
                SimpleDateFormat myFormat = new SimpleDateFormat("dd-MMM-yyyy", Locale.ENGLISH);
                String datePattern = "\\d{2}-\\d{2}-\\d{4}";


                try {
                    if (get_est_date.equals(null) || get_est_date.length() < 0) {

                        get_my_est_date = "";
                    } else {
                        Boolean is_edt_Date1 = get_est_date.matches(datePattern);
                        if (is_edt_Date1 == true) {
                            get_my_est_date = myFormat.format(fromUser.parse(get_est_date));


                        } else {
                            get_my_est_date = get_est_date;

                        }


                    }

                } catch (Exception e) {
                    get_my_est_date = "";
                }
                try {
                    if (get_original_est_date.equals("") || get_original_est_date.length() < 0) {
                        get_my_original_est_date = "";
                    } else {
                        Boolean is_ori_Date1 = get_original_est_date.matches(datePattern);
                        if (is_ori_Date1 == true) {
                            get_my_original_est_date = myFormat.format(fromUser.parse(get_original_est_date));

                        } else {
                            get_my_original_est_date = get_original_est_date;

                        }


                    }
                } catch (Exception e) {
                    get_my_original_est_date = "";
                }
                try {


                    if (get_ed_last_testDate.equals(null) || get_ed_last_testDate.length() < 0) {
                        get_my_ed_last_testDate = "";
                    } else {
                        Boolean is_last_Date1 = get_ed_last_testDate.matches(datePattern);
                        if (is_last_Date1 == true) {
                            get_my_ed_last_testDate = myFormat.format(fromUser.parse(get_ed_last_testDate));


                        } else {
                            get_my_ed_last_testDate = get_ed_last_testDate;

                        }

                    }
                } catch (Exception e) {
                    get_my_ed_last_testDate = "";
                }
                try {
                    if (get_next_test_date.equals(null) || get_next_test_date.length() < 0) {
                        get_my_next_test_date = "";
                    } else {
                        Boolean is_next_Date1 = get_next_test_date.matches(datePattern);
                        if (is_next_Date1 == true) {
                            get_my_next_test_date = myFormat.format(fromUser.parse(get_next_test_date));


                        } else {
                            get_my_next_test_date = get_next_test_date;

                        }

                    }

                } catch (Exception e) {
                    get_my_next_test_date = "";
                }
                try {
                    if (get_approval_date.equals(null) || get_approval_date.length() < 0) {
                        get_my_approval_date = "";
                    } else {
                        Boolean is_app_Date1 = get_approval_date.matches(datePattern);
                        if (is_app_Date1 == true) {
                            get_my_approval_date = myFormat.format(fromUser.parse(get_approval_date));

                        } else {
                            get_my_approval_date = get_approval_date;

                        }

                    }
                } catch (Exception e) {
                    get_my_approval_date = "";
                }

                if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                                /* String est_id=GlobalConstants.repair_EstimateID;
                        String est_no=GlobalConstants.repairEstimateNo;
                        String gi_transaction_no= GlobalConstants.gi_trans_no;
                        GlobalConstants.repair_EstimateID=est_id;
                        GlobalConstants.repair_EstimateNo=est_no;
                        GlobalConstants.gi_trans_no=gi_transaction_no;*/

                    jsonObject.put("Attchment", IfAttchment);
                    jsonObject.put("PageName", "Repair Estimate");
                    jsonObject.put("bv_strRepairEstimateId", GlobalConstants.repair_EstimateID);
                    jsonObject.put("bv_strCustomerID", customer_Id);
                    jsonObject.put("bv_strCustomerCode", GlobalConstants.customer_name);
                    jsonObject.put("bv_strEstimateDate", get_my_est_date);
                    jsonObject.put("bv_strOrginalEstimateDate", get_my_original_est_date);
                    jsonObject.put("bv_strStatusID", "9");
                    jsonObject.put("bv_strStatusCode", "AAR");
                    jsonObject.put("bv_strEquipmentNo", GlobalConstants.equipment_no);
                    jsonObject.put("bv_strEIRNo", GlobalConstants.repair_EstimateNo);
                    jsonObject.put("bv_strGateInTransaction", GlobalConstants.gi_trans_no);
                    jsonObject.put("bv_strLastTestDate", get_my_ed_last_testDate);
                    jsonObject.put("bv_strLastTestTypeID", get_sp_last_test_type_id);
                    jsonObject.put("bv_strLastTestTypeCode", get_sp_last_test_type);
                    jsonObject.put("bv_strValidityYear", get_validate_period_for_test);
                    jsonObject.put("bv_strNextTestDate", get_my_next_test_date);
                    jsonObject.put("bv_strLastSurveyor", get_last_survey);
                    jsonObject.put("bv_strNextTestTypeID", get_next_test_type_id);
                    jsonObject.put("bv_strNextTestTypeCode", get_next_test_type);

                    /* get_sp_repairtype_code = "0";
                        get_sp_invoice_code = "0";
                        get_repair_type = "";
                        invoice_party_name = "";*/
                    jsonObject.put("bv_strInvoicingPartyID", get_sp_invoice_code);
                    jsonObject.put("bv_strInvoicingPartyCode", invoice_party_name);
                    jsonObject.put("bv_strRepairTypeID", get_sp_repairtype_code);
                    jsonObject.put("bv_strRepairTypeCode", get_repair_type);
                    jsonObject.put("bv_strCertOfCleanlinessBit", get_cleanline_bets);
                    jsonObject.put("bv_strLaborRate", get_labor_rate);
                    jsonObject.put("bv_strApprovalAmount", "0.00");
                    jsonObject.put("bv_strApprovalDate", get_my_approval_date);
                    jsonObject.put("bv_strApprovalRef", "");
                    jsonObject.put("bv_strSurveyDate", "");
                    jsonObject.put("bv_strSurveyName", "");
                    jsonObject.put("bv_strWFData", "USERID=1&SYSTM_DT=15-JUN-2017 11:52:47 AM&USERNAME=ADMIN&RL_CD=ADMIN&RL_ID=1&DPT_ID=1&DPT_CD=DEPOT!&MSTR_ID_CSV=&QCK_LNK_ID_CSV=&CRT_BT=True&EDT_BT=True&VW_BT=True&ACTIVITYID=170&ACTN_DT=JUN 15, 2017");
                    jsonObject.put("bv_strEstimateId", GlobalConstants.repair_EstimateID);
                    jsonObject.put("bv_strRevisionNo", "0");
                    jsonObject.put("bv_strRemarks", get_ed_remarks);
                    jsonObject.put("bv_strEstimationNo", GlobalConstants.repair_EstimateNo);
                    jsonObject.put("bv_strCustomerEstimatedCost", "0.00");
                    jsonObject.put("bv_strCustomerApprovedCost", "0.00");
                    jsonObject.put("bv_strPartyApprovalRef", "");
                    jsonObject.put("bv_intActivityId", "170");
                    jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                    jsonObject.put("EquipmentNo", GlobalConstants.equipment_no);
                    jsonObject.put("GITransactionNo", GlobalConstants.gi_trans_no);
                    jsonObject.put("bv_strMode", "edit");


                } else {
                    jsonObject.put("Attchment", IfAttchment);
                    jsonObject.put("PageName", "Repair Estimate");
                    jsonObject.put("bv_strRepairEstimateId", "");
                    jsonObject.put("bv_strCustomerID", customer_Id);
                    jsonObject.put("bv_strCustomerCode", GlobalConstants.customer_name);
                    jsonObject.put("bv_strEstimateDate", get_my_est_date);
                    jsonObject.put("bv_strOrginalEstimateDate", get_my_original_est_date);
                    jsonObject.put("bv_strStatusID", "9");
                    jsonObject.put("bv_strStatusCode", "AAR");
                    jsonObject.put("bv_strEquipmentNo", GlobalConstants.equipment_no);
                    jsonObject.put("bv_strEIRNo", GlobalConstants.gi_trans_no);
                    jsonObject.put("bv_strGateInTransaction", GlobalConstants.gi_trans_no);
                    jsonObject.put("bv_strLastTestDate", get_my_ed_last_testDate);
                    jsonObject.put("bv_strLastTestTypeID", get_sp_last_test_type_id);
                    jsonObject.put("bv_strLastTestTypeCode", get_sp_last_test_type);
                    jsonObject.put("bv_strValidityYear", get_validate_period_for_test);
                    jsonObject.put("bv_strNextTestDate", get_my_next_test_date);
                    jsonObject.put("bv_strLastSurveyor", get_last_survey);
                    jsonObject.put("bv_strNextTestTypeID", get_next_test_type_id);
                    jsonObject.put("bv_strNextTestTypeCode", get_next_test_type);
                     /* get_sp_repairtype_code = "0";
                        get_sp_invoice_code = "0";
                        get_repair_type = "";
                        invoice_party_name = "";*/
                    jsonObject.put("bv_strInvoicingPartyID", get_sp_invoice_code);
                    jsonObject.put("bv_strInvoicingPartyCode", invoice_party_name);
                    jsonObject.put("bv_strRepairTypeID", get_sp_repairtype_code);
                    jsonObject.put("bv_strRepairTypeCode", get_repair_type);
                    jsonObject.put("bv_strCertOfCleanlinessBit", get_cleanline_bets);
                    jsonObject.put("bv_strLaborRate", get_labor_rate);
                    jsonObject.put("bv_strApprovalAmount", "0.00");
                    jsonObject.put("bv_strApprovalDate", "");
                    jsonObject.put("bv_strApprovalRef", "");
                    jsonObject.put("bv_strSurveyDate", "");
                    jsonObject.put("bv_strSurveyName", "");
                    jsonObject.put("bv_strWFData", "USERID=1&SYSTM_DT=15-JUN-2017 11:52:47 AM&USERNAME=ADMIN&RL_CD=ADMIN&RL_ID=1&DPT_ID=1&DPT_CD=DEPOT!&MSTR_ID_CSV=&QCK_LNK_ID_CSV=&CRT_BT=True&EDT_BT=True&VW_BT=True&ACTIVITYID=170&ACTN_DT=JUN 15, 2017");
                    jsonObject.put("bv_strEstimateId", "");
                    jsonObject.put("bv_strRevisionNo", "0");
                    jsonObject.put("bv_strRemarks", get_ed_remarks);
                    jsonObject.put("bv_strEstimationNo", "");
                    jsonObject.put("bv_strCustomerEstimatedCost", "0.00");
                    jsonObject.put("bv_strCustomerApprovedCost", "0.00");
                    jsonObject.put("bv_strPartyApprovalRef", "");
                    jsonObject.put("bv_intActivityId", "170");
                    jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                    jsonObject.put("EquipmentNo", GlobalConstants.equipment_no);
                    jsonObject.put("GITransactionNo", GlobalConstants.gi_trans_no);
                    jsonObject.put("bv_strMode", "new");

                }

                jsonObject.put("LineItem", reqObj_lineitem);
                jsonObject.put("SummaryDetail", summary_object_head);
                reqObj.put("ArrayOfFileParams", image_jsonlist);
                jsonObject.put("hfc", reqObj);


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("request", jsonObject.toString());
                Log.d("responce", resp);
                JSONObject jsonResp = new JSONObject(resp);


                JSONObject returnMessage = jsonResp.getJSONObject("d");

                String message = returnMessage.getString("RepairUpdate");
                responseString = message;
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
            if (responseString != null) {
                if (responseString.equalsIgnoreCase("Success")) {

                    if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                            || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {
                        GlobalConstants.Line_item_Json = "";
                        GlobalConstants.add_detail_jsonobject = "";
                        Toast.makeText(getApplicationContext(), "Repair Updated Successfully.", Toast.LENGTH_SHORT).show();
                        finish();
                        Intent i = new Intent(getApplication(), RepairEstimateMySubmit.class);
                        startActivity(i);
                    } else {
                        GlobalConstants.Line_item_Json = "";
                        GlobalConstants.add_detail_jsonobject = "";
                        Toast.makeText(getApplicationContext(), "Repair Created Successfully.", Toast.LENGTH_SHORT).show();
                        finish();
                        Intent i = new Intent(getApplication(), RepairEstimatePending.class);
                        startActivity(i);
                    }


                } else {
                    shortToast(getApplicationContext(), "Repair Created Failed.");

                }
            } else {
                shortToast(getApplicationContext(), "Repair Created Failed.");


            }
            if ((progressDialog != null) && progressDialog.isShowing())

                progressDialog.dismiss();

        }
    }

    public static boolean isValid(String value) {
        try {
            try {
                new SimpleDateFormat("dd-mm-yyyy").parse(value);
            } catch (java.text.ParseException e) {
                e.printStackTrace();
            }
            return true;
        } catch (ParseException e) {
            return false;
        }
    }

    public class Calculation extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Summary.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLRepair_calculation);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("CustomerId", customer_Id);


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

                        Customer_Cal_ArrayList = new ArrayList<Customer_Calculation_Bean>();
                        for (int i = 0; i < jsonarray.length(); i++) {


                            jsonObject = jsonarray.getJSONObject(i);

                                    /* "CSTMR_ID": "195",
                        "CSTMR_CD": "STOLT",
                        "CRRNCY_ID": "157",
                        "CRRNCY_CD": "USD",
                        "EXCHNG_RT_PR_UNT_NC": "0.2700"*/
                            CRRNCY_CD = jsonObject.getString("CRRNCY_CD");
                            EXCHNG_RT_PR_UNT_NC = jsonObject.getString("EXCHNG_RT_PR_UNT_NC");
                            CSTMR_ID = jsonObject.getString("CSTMR_ID");


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

            progressDialog.dismiss();
            if (jsonarray != null) {
                float amount = cust_total * Float.parseFloat(EXCHNG_RT_PR_UNT_NC);
                Log.i("amount", String.valueOf(amount));
                customer_calc.setText(String.valueOf(amount).replace("-", "") + "(" + CRRNCY_CD + ")");
                customer_calc_rec.setText(String.valueOf(amount).replace("-", "") + "(" + CRRNCY_CD + ")");

            } else {


            }


        }

    }

}
