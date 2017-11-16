package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Context;
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
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.Image_Bean;
import com.i_tankdepo.Beanclass.LineItem_Bean;
import com.i_tankdepo.Beanclass.RepairBean;
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
public class SurveyCompletion_Summary extends CommonActivity implements View.OnClickListener {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu, im_down, im_up, im_heat_close, im_heat_ok, iv_back;


    private TextView tv_toolbarTitle, tv_search_options,no_data;
    LinearLayout LL_hole, LL_heat_submit,LL_search_Value,LL_heat;

    private ProgressDialog progressDialog;
    private String get_est_date,get_original_est_date,get_ed_last_testDate,get_next_test_type_id,get_next_test_type,get_last_survey,
            get_next_test_date;
    private Intent mServiceIntent;

    private Button heat_refresh, heat_home, heat_submit,heating,cleaning,inspection;
    private FragmentActivity mycontaxt;
    private TextView tv_lineitem_count,tv_man_hour,tv_manhour_cost,tv_cust_crr,tv_depo_crr,tv_metrial_cost,tv_totalCost,customer_name,equipment_no,eqip_type;
    private Button LL_Line_iteam,LL_add_details,LL_summary;
    private Spinner sp_tarif_group,sp_tarif_code;
    private ImageView iv_changeOfStatus;
    private LinearLayout summary;
    private String lineItem_json,image_json,add_details_json;
    private JSONObject jsonrootObject;
    private JSONArray jsonarray;
    private JSONObject jsonObject;
    String MHC,MHR,TC,MC;
    private ArrayList<LineItem_Bean> lineitem_arraylist;
    private LineItem_Bean lineitem_bean;
    private int total=0;
    private float man_hour=0;
    private float man_hour_cost=0;
    private float metrial_cost=0;
    private Image_Bean image_bean;
    private String IfAttchment;
    private JSONObject summary_object_head;
    private ArrayList<Image_Bean> Image_arraylist;
    private int totel_amount;
    private TextView tv_amount;
    private String get_cust,get_cust_id,get_eqp_no,get_indate,get_previous_cargo,get_repair_type_cd,
            get_repair_type_id,get_laststatus_date,get_validate_period_test,get_lbr_rate,get_nexttest_date,
            get_lst_survey,get_last_test_type,get_nexttest_type,get_last_test_date,get_rematk,get_invoice_prty_cd
            ,get_invoice_prty_id,get_revision_no,get_cust_app_ref_no,get_approval_date,get_Status,get_Status_id,get_invoice_prty_name,get_gi_trans_no,
            get_repair_est_id,get_party_app_ref_no,get_survey_name,get_survey_completion_date,bv_strRepairEstimateId,get_rep_status,get_rep_status_id,get_repair_est_no;
    private JSONArray json_array;
    private JSONObject object2;
    private JSONObject reqObj_lineitem;
    private String get_survey_date;
    private JSONArray LineItems_Json;
    private String Line_item_Json;
    private ArrayList<RepairBean> repair_arraylist;
    private RepairBean repair_bean;
    private float man_hr_rate=0;
    private TextView repair_estimate_text;
    private Button repair_completion,repair_estimate,repair_approval,survey_completion;
    private RelativeLayout RL_heating,RL_Repair;
    private ImageView more_info;
    private ImageView iv_send_mail;
    private ImageView add;
    private String survey_name;
    private String date;
    private String ReportURL;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.survey_summary);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);



        tv_amount = (TextView) findViewById(R.id.tv_amount);

        tv_amount.setOnClickListener(this);
        more_info = (ImageView) findViewById(R.id.more_info);
        more_info.setOnClickListener(this);
        sp_tarif_group=(Spinner)findViewById(R.id.sp_tarif_group);
        sp_tarif_code=(Spinner)findViewById(R.id.sp_tarif_code);
        repair_estimate_text = (TextView)findViewById(R.id.tv_heating);
        repair_estimate_text.setText("Survey Completion");
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setOnClickListener(this);
        iv_changeOfStatus = (ImageView) findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        menu.setVisibility(View.GONE);
        LL_Line_iteam=(Button)findViewById(R.id.Line_iteam);
        LL_Line_iteam.setOnClickListener(this);
        LL_add_details=(Button)findViewById(R.id.add_details);
        add=(ImageView)findViewById(R.id.add);
        add.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_add_details.performClick();
            }
        });
        LL_add_details.setOnClickListener(this);
      //  LL_attachments.setOnClickListener(this);
        LL_summary=(Button)findViewById(R.id.summary);
        summary=(LinearLayout)findViewById(R.id.LL_summary);
        summary.setBackgroundColor(getResources().getColor(R.color.submit));
        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_heating.setVisibility(View.GONE);
        LL_summary.setOnClickListener(this);
        LL_summary.setVisibility(View.GONE);
        heat_home = (Button) findViewById(R.id.heat_home);
        heat_refresh = (Button) findViewById(R.id.heat_refresh);
        heat_submit = (Button) findViewById(R.id.heat_submit);
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);
        LL_heat = (LinearLayout) findViewById(R.id.LL_heat);
        LL_heat.setOnClickListener(this);
        heat_submit.setOnClickListener(this);
//        tabLayout.setupWithViewPager(viewPager);

        iv_send_mail = (ImageView) findViewById(R.id.iv_send_mail);
        iv_send_mail.setOnClickListener(this);
        repair_estimate = (Button)findViewById(R.id.repair_estimate);
        repair_estimate.setOnClickListener(this);
        repair_approval = (Button)findViewById(R.id.repair_approval);
        repair_approval.setVisibility(View.GONE);
        repair_completion = (Button)findViewById(R.id.repair_completion);
        repair_completion.setVisibility(View.GONE);
        survey_completion = (Button)findViewById(R.id.survey_completion);
        survey_completion.setVisibility(View.GONE);
       // tabLayout.clearOnTabSelectedListeners();


        heating = (Button)findViewById(R.id.heating);
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        if (cd.isConnectingToInternet())
        {
            iv_send_mail.setVisibility(View.VISIBLE);

            new Get_Repair_MySubmit_details().execute();

        }else
        {

        }



        inspection.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);

        heat_refresh.setOnClickListener(this);
        heat_home.setOnClickListener(this);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);

        Log.i("customer_name",GlobalConstants.customer_name);
        customer_name = (TextView) findViewById(R.id.text1);
        tv_lineitem_count = (TextView) findViewById(R.id.tv_lineitem_count);
        tv_man_hour = (TextView) findViewById(R.id.tv_man_hour);
        tv_manhour_cost = (TextView) findViewById(R.id.tv_manhour_cost);
        tv_metrial_cost = (TextView) findViewById(R.id.tv_metrial_cost);
        tv_depo_crr = (TextView) findViewById(R.id.tv_depo_crr);
        tv_cust_crr = (TextView) findViewById(R.id.tv_cust_crr);
        tv_totalCost = (TextView) findViewById(R.id.tv_totalCost);
        customer_name.setText(GlobalConstants.customer_name+" , "+GlobalConstants.equipment_no+ " , "+
                GlobalConstants.status +"-"+ GlobalConstants.status_id );
        equipment_no = (TextView) findViewById(R.id.text2);
//        equipment_no.setText(GlobalConstants.equipment_no);
        eqip_type = (TextView) findViewById(R.id.text3);
//        eqip_type.setText(GlobalConstants.previous_cargo);
        tv_toolbarTitle.setText("Summary");


        Line_item_Json=sp.getString(SP_LINE_ITEM_JSON,"line_item_json");
        System.out.println("lineItem_json" + Line_item_Json);

            try {

                JSONObject jsonrootObject = new JSONObject(Line_item_Json);

                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("Response");
                repair_arraylist = new ArrayList<RepairBean>();

                lineitem_arraylist = new ArrayList<LineItem_Bean>();
                for (int j = 0; j < jsonarray.length(); j++) {

                    repair_bean = new RepairBean();
                    jsonObject = jsonarray.getJSONObject(j);

                    if (GlobalConstants.equipment_no.equalsIgnoreCase(jsonObject.getString("EquipmentNo"))) {

                        LineItems_Json = jsonObject.getJSONArray("LineItems");

                        for (int i = 0; i < LineItems_Json.length(); i++) {

                            lineitem_bean = new LineItem_Bean();
                            JSONObject jsonObject_line = LineItems_Json.getJSONObject(i);


                            lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject_line.getString("RPR_ESTMT_DTL_ID"));
                            lineitem_bean.setRPR_ESTMT_ID(jsonObject_line.getString("RPR_ESTMT_ID"));
                            lineitem_bean.setItem(jsonObject_line.getString("ItemCd"));
                            lineitem_bean.setItemCode(jsonObject_line.getString("Item"));
                            lineitem_bean.setRepair_Code(jsonObject_line.getString("RepairCd"));
                            lineitem_bean.setRepair_Code_Id(jsonObject_line.getString("Repair"));
                            lineitem_bean.setSubItem(jsonObject_line.getString("SubItemCd"));
                            lineitem_bean.setSuIitemCode(jsonObject_line.getString("SubItem"));
                            lineitem_bean.setDamage_Code(jsonObject_line.getString("DamageCd"));
                            lineitem_bean.setDamage_Code_Id(jsonObject_line.getString("Damage"));
                            lineitem_bean.setManHour(jsonObject_line.getString("ManHour"));
                            lineitem_bean.setManHour_Cost(jsonObject_line.getString("MHC"));
                            lineitem_bean.setMHR(jsonObject_line.getString("MHR"));
                            lineitem_bean.setMetial_Cost(jsonObject_line.getString("MC"));
                            lineitem_bean.setResponsibility_Cd(jsonObject_line.getString("ResponsibilityCd"));
                            lineitem_bean.setResponsibility(jsonObject_line.getString("Responsibility"));
                            lineitem_bean.setTotel(jsonObject_line.getString("TC"));
                            lineitem_bean.setRemark(jsonObject_line.getString("DmgRprRemarks"));
                            lineitem_bean.setDamageDescription(jsonObject_line.getString("DamageDescription"));
                            lineitem_bean.setRepairDescription(jsonObject_line.getString("RepairDescription"));
                            lineitem_bean.setCheckButton(jsonObject_line.getString("CheckButton"));
                            lineitem_bean.setChangingBit(jsonObject_line.getString("ChangingBit"));

                            lineitem_arraylist.add(lineitem_bean);


                        }


                    }


                }
            }catch (Exception e)
            {

            }



        tv_lineitem_count.setText(String.valueOf(lineitem_arraylist.size()));
        for(int i=0;i<lineitem_arraylist.size();i++)
        {
            TC=lineitem_arraylist.get(i).getTotel();
            total=total+(int) Math.round(Float.parseFloat(TC));
            if(lineitem_arraylist.get(i).getManHour().equals(""))
            {
                man_hour=0.0f;
            }else
            {
                man_hour=man_hour+Float.valueOf(lineitem_arraylist.get(i).getManHour());
            }

            man_hour_cost=man_hour_cost+Float.valueOf(lineitem_arraylist.get(i).getManHour_Cost());
            metrial_cost=metrial_cost+Float.valueOf(lineitem_arraylist.get(i).getMetial_Cost());
            man_hr_rate=man_hr_rate+Float.valueOf(lineitem_arraylist.get(i).getMHR());

        }
        Log.i("Totel Cost===", String.valueOf(total));
        tv_amount.setText(String.valueOf(total)+".00"+" "+GlobalConstants.currency);
        tv_totalCost.setText(String.valueOf(total)+".00");
        tv_cust_crr.setText(String.valueOf(total)+".00"+" "+GlobalConstants.currency);
        tv_depo_crr.setText(String.valueOf(total)+".00"+" "+GlobalConstants.currency);
        tv_man_hour.setText(String.valueOf(man_hour)+".00");
        tv_manhour_cost.setText(String.valueOf(man_hour_cost)+".00");
        tv_metrial_cost.setText(String.valueOf(metrial_cost)+".00");
        JSONObject summary_object=new JSONObject();
        summary_object_head=new JSONObject();
        JSONArray summary_array=new JSONArray();
        try {
            summary_object.put("MH",String.valueOf(man_hour));
            summary_object.put("MHC",String.valueOf(man_hour_cost));
            summary_object.put("MC",String.valueOf(metrial_cost));
            summary_object.put("TC",String.valueOf(total));
            summary_object.put("MHR",String.valueOf(man_hr_rate) );
            summary_object.put("ResponsibiltyID", "1");
            summary_object.put("MHCSTSummary", "1");
            summary_object.put("ResponsibiltyCD", "C");
            summary_array.put(summary_object);
            summary_object_head.put("LineSummaryDetail",summary_array);
        }catch (Exception e)
        {

        }
      /*  {
            "LineSummaryDetail":
  [
            {
                "MH" :"2520.00",
                    "MHC" :"0.0000",
                    "MC" :"85.00",
                    "TC" :"85.0000",
                    "MHR" :"0.00",
                    "ResponsibiltyID" :"1"
            }
  ]
        }*/

        Image_arraylist=GlobalConstants.multiple_encodeArray;




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
                GlobalConstants.from=tv_amount.getText().toString();
                if(GlobalConstants.from.equalsIgnoreCase("MysubmitAttach") || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails") )
                {
                    GlobalConstants.from="MySubmitRepair";
                }else
                {
                    GlobalConstants.from="SurveyPending";
                }
                finish();
                GlobalConstants.summaryfrom="summaryfrom";
                 survey_name=GlobalConstants.Surveyor_name;
                String remarks=GlobalConstants.remark;
                 date=GlobalConstants.Survey_CompletionDate;
                GlobalConstants.Surveyor_name=survey_name;
                GlobalConstants.remark=remarks;
                String invoice_prty_cd=GlobalConstants.invoice_PartyCD;
                String invoice_prty_id=GlobalConstants.invoice_PartyID;
                GlobalConstants.invoice_PartyCD=invoice_prty_cd;
                GlobalConstants.invoice_PartyID=invoice_prty_id;
                GlobalConstants.Survey_CompletionDate=date;
                startActivity(new Intent(getApplicationContext(),Survey_Completion_wizard.class));

                break;
            case R.id.iv_back:
                onBackPressed();
                break;
            case R.id.more_info:

                popUp_equipment_info(GlobalConstants.equipment_no,GlobalConstants.status,GlobalConstants.status_id,GlobalConstants.previous_cargo,"","","","");


                break;
            case R.id.repair_estimate:
                finish();
                startActivity(new Intent(getApplicationContext(),Repair_MainActivity.class));
                break;
            case R.id.add_details:
                GlobalConstants.from=tv_amount.getText().toString();
                if(GlobalConstants.from.equalsIgnoreCase("MysubmitAttach") || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails") )
                {
                    GlobalConstants.from="MySubmitRepair";
                }else
                {
                    GlobalConstants.from="SurveyPending";
                }
                GlobalConstants.summaryfrom="summaryfrom";
                finish();
                 survey_name=GlobalConstants.Surveyor_name;
                 remarks=GlobalConstants.remark;
                 date=GlobalConstants.Survey_CompletionDate;
                GlobalConstants.Surveyor_name=survey_name;
                GlobalConstants.remark=remarks;
                 invoice_prty_cd=GlobalConstants.invoice_PartyCD;
                 invoice_prty_id=GlobalConstants.invoice_PartyID;
                GlobalConstants.invoice_PartyCD=invoice_prty_cd;
                GlobalConstants.invoice_PartyID=invoice_prty_id;
                GlobalConstants.Survey_CompletionDate=date;
                startActivity(new Intent(getApplicationContext(),Add_Survey_Completion_Details.class));

                break;
            case R.id.iv_send_mail:

                if (cd.isConnectingToInternet()) {

                    // Survey Completion
                    // Repair Approval
                    new Print_pdf().execute();

                }else{
                    shortToast(getApplicationContext(), "Please check your Internet Connection.");
                }

                break;
            case R.id.heat_submit:

                json_array=new JSONArray();
                reqObj_lineitem=new JSONObject();

                try {
                    for (int i = 0; i < lineitem_arraylist.size(); i++) {


                        /*"RPR_ESTMT_DTL_ID" :"13747",
                              "RPR_ESTMT_ID" :"13747",
                              "Item":"1",
                              "ItemCd" :"1",
                              "SubItem" :"2",
                              "SubItemCd" :"2",
                              "Damage" :"13",
                              "DamageCd" :"13",
                              "Repair" :"31",
                              "RepairCd" :"BN",
                              "ManHour" :"120.00",
                              "DmgRprRemarks" :"NEW",
                              "MHR" :"0.00",
                              "MHC" :"0.0000",
                              "MC" :"25.00",
                                 "TC" :"25.0000",
                              "Responsibility" :"66",
                              "ResponsibilityCd" :"C",
                               "DamageDescription" :"Broken/ Cracked",
                               "RepairDescription":"Burnt",
                               "CheckButton":"0",
                               "ChangingBit": "False"*/

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
                        object2.put("CheckButton", "0");
                        try {
                            if (!lineitem_arraylist.get(i).getChangingBit().equals(null)||lineitem_arraylist.get(i).getChangingBit().equals("") ) {
                                object2.put("ChangingBit", lineitem_arraylist.get(i).getChangingBit());

                            }else{
                                object2.put("ChangingBit", "False");
                            }
                        }catch (Exception e)
                        {
                            object2.put("ChangingBit", "False");

                        }
                        json_array.put(object2);


                    }
                    reqObj_lineitem.put("LineItemList", json_array);
                    Log.d("reqObj_lineitem", String.valueOf(reqObj_lineitem));
                    if((GlobalConstants.Surveyor_name.equals(""))||GlobalConstants.Survey_CompletionDate.equals("") ) {
                        shortToast(getApplicationContext(),"Survey Name and Survey Completion Date is Required");

                    }else
                    {
                        new PostInfo().execute();

                    }
                }catch (Exception e)
                {
                    Log.i("Exception", String.valueOf(e));
                }

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
    public class PostInfo extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        private JSONArray image_jsonlist;
        private JSONObject imagejsonObject;
        private JSONObject reqObj;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(SurveyCompletion_Summary.this);
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
            try{
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
                    for(int i=0;i<Image_arraylist.size();i++) {

                        imagejsonObject = new JSONObject();
                        imagejsonObject.put("FileName", Image_arraylist.get(i).getImage_Name());
                        imagejsonObject.put("ContentLength", Image_arraylist.get(i).getImage_length());
                        imagejsonObject.put("base64imageString", Image_arraylist.get(i).getImage());
                        image_jsonlist.put(imagejsonObject);
                    }




                }catch (Exception e)
                {

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
                SimpleDateFormat myFormat = new SimpleDateFormat("dd-MMM-yyyy",Locale.ENGLISH);

                String datePattern = "\\d{2}-\\d{2}-\\d{4}";

                get_survey_date=GlobalConstants.Survey_CompletionDate;
                try {

                    if(get_survey_date.equals(null)||get_survey_date.length()<0)
                    {

                        get_survey_date="";
                    }else
                    {
                        Boolean is_edt_Date1 = get_survey_date.matches(datePattern);
                        if(is_edt_Date1==true)
                        {
                            get_survey_date = myFormat.format(fromUser.parse(get_survey_date));


                        }else
                        {
                            get_survey_date = "";

                        }
                    }}catch (Exception e)
                {
                    get_survey_date="";
                }

                try {

                    if(get_est_date.equals(null)||get_est_date.length()<0)
                    {

                        get_est_date="";
                    }else
                    {
                        Boolean is_edt_Date1 = get_est_date.matches(datePattern);
                        if(is_edt_Date1==true)
                        {
                            get_est_date = myFormat.format(fromUser.parse(get_est_date));


                        }else
                        {
                            get_est_date = get_est_date;

                        }
                    }}catch (Exception e)
                {
                    get_est_date="";
                }

                try
                {
                    if(get_original_est_date.equals(null)||get_original_est_date.length()<0)
                    {
                        get_original_est_date="";
                    }else
                    {
                        Boolean is_ori_Date1 = get_original_est_date.matches(datePattern);
                        if(is_ori_Date1==true)
                        {
                            get_original_est_date = myFormat.format(fromUser.parse(get_original_est_date));

                        }else
                        {
                            get_original_est_date = get_original_est_date;

                        }


                    }
                }catch (Exception e)
                {
                    get_original_est_date="";
                }

                try
                {
                    if(get_ed_last_testDate.equals(null)||get_ed_last_testDate.length()<0)
                    {
                        get_ed_last_testDate="";
                    }else
                    {
                        Boolean is_last_Date1 = get_ed_last_testDate.matches(datePattern);
                        if(is_last_Date1==true)
                        {
                            get_ed_last_testDate = myFormat.format(fromUser.parse(get_ed_last_testDate));



                        }else
                        {
                            get_ed_last_testDate = get_ed_last_testDate;

                        }

                    }
                }catch (Exception e)
                {
                    get_ed_last_testDate="";

                }


                try
                {
                    if(get_next_test_date.equals(null)||get_next_test_date.length()<0)
                    {
                        get_next_test_date="";
                    }else
                    {
                        Boolean is_next_Date1 = get_next_test_date.matches(datePattern);
                        if(is_next_Date1==true)
                        {
                            get_next_test_date = myFormat.format(fromUser.parse(get_next_test_date));




                        }else
                        {
                            get_next_test_date = get_next_test_date;

                        }

                    }

                }catch (Exception e)
                {
                    get_next_test_date="";

                }


                try {
                    if (get_approval_date.equals(null) || get_approval_date.length() < 0) {
                        get_approval_date = "";
                    } else {
                        Boolean is_app_Date1 = get_approval_date.matches(datePattern);
                        if (is_app_Date1 == true) {
                            get_approval_date = myFormat.format(fromUser.parse(get_approval_date));

                        } else {
                            get_approval_date = get_approval_date;

                        }
                    }
                }catch (Exception e)
                {
                    e.printStackTrace();
                    get_approval_date = "";
                }

                if(GlobalConstants.from.equalsIgnoreCase("MysubmitAttach") || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails") ) {
                    jsonObject.put("Attchment", IfAttchment);
                    jsonObject.put("PageName", "Survey Completion");
                    jsonObject.put("bv_strRepairEstimateId",GlobalConstants.repairEstimateId);
                    jsonObject.put("bv_strCustomerID", GlobalConstants.customer_Id);
                    jsonObject.put("bv_strCustomerCode", GlobalConstants.customer_name);
                    jsonObject.put("bv_strEstimateDate", get_est_date);
                    jsonObject.put("bv_strOrginalEstimateDate", get_original_est_date);
                    jsonObject.put("bv_strStatusID", get_rep_status_id);
                    jsonObject.put("bv_strStatusCode","AUR");
                    jsonObject.put("bv_strEquipmentNo", GlobalConstants.equipment_no);
                    jsonObject.put("bv_strEIRNo",  GlobalConstants.gi_trans_no);
                    jsonObject.put("bv_strGateInTransaction", GlobalConstants.gi_trans_no);
                    jsonObject.put("bv_strLastTestDate", get_last_test_date);
                    jsonObject.put("bv_strLastTestTypeID", "0");
                    jsonObject.put("bv_strLastTestTypeCode", "");
                    jsonObject.put("bv_strValidityYear", get_validate_period_test);
                    jsonObject.put("bv_strNextTestDate", get_nexttest_date);
                    jsonObject.put("bv_strLastSurveyor", get_lst_survey);
                    jsonObject.put("bv_strNextTestTypeID", "0");
                    jsonObject.put("bv_strNextTestTypeCode", "");
                    jsonObject.put("bv_strRepairTypeID", get_repair_type_id);
                    jsonObject.put("bv_strRepairTypeCode", get_repair_type_cd);
                    jsonObject.put("bv_strCertOfCleanlinessBit", "False");
                    jsonObject.put("bv_strInvoicingPartyID", GlobalConstants.invoice_PartyID);
                    jsonObject.put("bv_strInvoicingPartyCode", GlobalConstants.invoice_PartyCD);
                    jsonObject.put("bv_strLaborRate", get_lbr_rate);
                    jsonObject.put("bv_strApprovalAmount", "0.00");
                    jsonObject.put("bv_strApprovalDate",get_approval_date);
                    jsonObject.put("bv_strApprovalRef", get_cust_app_ref_no);
                    jsonObject.put("bv_strSurveyDate",get_survey_date);
                    jsonObject.put("bv_strSurveyName", GlobalConstants.Surveyor_name);
                    jsonObject.put("bv_strWFData", "USERID=1&SYSTM_DT=15-JUN-2017 11:52:47 AM&USERNAME=ADMIN&RL_CD=ADMIN&RL_ID=1&DPT_ID=1&DPT_CD=DEPOT!&MSTR_ID_CSV=&QCK_LNK_ID_CSV=&CRT_BT=True&EDT_BT=True&VW_BT=True&ACTIVITYID=170&ACTN_DT=JUN 15, 2017");
                    jsonObject.put("bv_strEstimateId", GlobalConstants.repairEstimateId);
                    jsonObject.put("bv_strRevisionNo", "0");
                    jsonObject.put("bv_strRemarks",GlobalConstants.remark);
                    jsonObject.put("bv_strEstimationNo", GlobalConstants.repairEstimateId);
                    jsonObject.put("bv_strCustomerEstimatedCost", "0.00");
                    jsonObject.put("bv_strCustomerApprovedCost", "0.00");
                    jsonObject.put("bv_strPartyApprovalRef", get_party_app_ref_no);
                    jsonObject.put("bv_intActivityId", "172");
                    jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                    jsonObject.put("EquipmentNo", GlobalConstants.equipment_no);
                    jsonObject.put("GITransactionNo", GlobalConstants.gi_trans_no);
                    jsonObject.put("bv_strMode", "edit");


                }else
                {
                    jsonObject.put("Attchment", IfAttchment);
                    jsonObject.put("PageName", "Survey Completion");
                    jsonObject.put("bv_strRepairEstimateId",GlobalConstants.repair_EstimateID);
                    jsonObject.put("bv_strCustomerID", GlobalConstants.customer_Id);
                    jsonObject.put("bv_strCustomerCode", GlobalConstants.customer_name);
                    jsonObject.put("bv_strEstimateDate", get_est_date);
                    jsonObject.put("bv_strOrginalEstimateDate", get_original_est_date);
                    jsonObject.put("bv_strStatusID",get_rep_status_id);
                    jsonObject.put("bv_strStatusCode",get_rep_status);
                    jsonObject.put("bv_strEquipmentNo", GlobalConstants.equipment_no);
                    jsonObject.put("bv_strEIRNo", GlobalConstants.gi_trans_no);
                    jsonObject.put("bv_strGateInTransaction", GlobalConstants.gi_trans_no);
                    jsonObject.put("bv_strLastTestDate", get_last_test_date);
                    jsonObject.put("bv_strLastTestTypeID", "0");
                    jsonObject.put("bv_strLastTestTypeCode", "");
                    jsonObject.put("bv_strValidityYear", get_validate_period_test);
                    jsonObject.put("bv_strNextTestDate", get_nexttest_date);
                    jsonObject.put("bv_strLastSurveyor", get_lst_survey);
                    jsonObject.put("bv_strNextTestTypeID", "0");
                    jsonObject.put("bv_strNextTestTypeCode", "");
                    jsonObject.put("bv_strRepairTypeID", get_repair_type_id);
                    jsonObject.put("bv_strRepairTypeCode", get_repair_type_cd);
                    jsonObject.put("bv_strCertOfCleanlinessBit", "False");
                    jsonObject.put("bv_strInvoicingPartyID", GlobalConstants.invoice_PartyID);
                    jsonObject.put("bv_strInvoicingPartyCode", GlobalConstants.invoice_PartyCD);
                    jsonObject.put("bv_strLaborRate", get_lbr_rate);
                    jsonObject.put("bv_strApprovalAmount", "0.00");
                    jsonObject.put("bv_strApprovalDate", "");
                    jsonObject.put("bv_strApprovalRef", get_cust_app_ref_no);
                    jsonObject.put("bv_strSurveyDate", get_survey_date);
                    jsonObject.put("bv_strSurveyName", GlobalConstants.Surveyor_name);
                    jsonObject.put("bv_strWFData", "USERID=1&SYSTM_DT=15-JUN-2017 11:52:47 AM&USERNAME=ADMIN&RL_CD=ADMIN&RL_ID=1&DPT_ID=1&DPT_CD=DEPOT!&MSTR_ID_CSV=&QCK_LNK_ID_CSV=&CRT_BT=True&EDT_BT=True&VW_BT=True&ACTIVITYID=170&ACTN_DT=JUN 15, 2017");
                    jsonObject.put("bv_strEstimateId", GlobalConstants.repair_EstimateID);
                    jsonObject.put("bv_strRevisionNo", "0");
                    jsonObject.put("bv_strRemarks", GlobalConstants.remark);
                    jsonObject.put("bv_strEstimationNo", GlobalConstants.repairEstimateNo);
                    jsonObject.put("bv_strCustomerEstimatedCost", "0.00");
                    jsonObject.put("bv_strCustomerApprovedCost", "0.00");
                    jsonObject.put("bv_strPartyApprovalRef", get_party_app_ref_no);
                    jsonObject.put("bv_intActivityId", "0");
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
                responseString=message;
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
                if (responseString.equalsIgnoreCase("Success") || responseString.equalsIgnoreCase("This operation requires IIS integrated pipeline mode.")) {


                        Toast.makeText(getApplicationContext(), "SurveyCompletion Updated Successfully.", Toast.LENGTH_SHORT).show();
                        finish();
                        Intent i = new Intent(getApplication(), SurveyCompletionPending.class);
                        startActivity(i);



                } else {
                    Toast.makeText(getApplicationContext(), "SurveyCompletion Failed", Toast.LENGTH_SHORT).show();

                }
            }else
            {
                Toast.makeText(getApplicationContext(), "SurveyCompletion Created Failed..!", Toast.LENGTH_SHORT).show();

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
            progressDialog = new ProgressDialog(SurveyCompletion_Summary.this);
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
                Log.d("rep_object1", resp);
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
                        try {

                            lineitem_arraylist = new ArrayList<LineItem_Bean>();
                            for (int j = 0; j < jsonarray.length(); j++) {

                                jsonObject = jsonarray.getJSONObject(j);

                                if (GlobalConstants.equipment_no.equalsIgnoreCase(jsonObject.getString("EquipmentNo"))) {

                                    LineItems_Json = jsonObject.getJSONArray("LineItems");

                                    get_cust= jsonObject.getString("Customer");
                                    get_cust_id= jsonObject.getString("CSTMR_ID");
                                    get_Status_id= jsonObject.getString("EquipmentStatusId");
                                    get_Status= jsonObject.getString("EquipmentStatusCd");
                                    get_approval_date= jsonObject.getString("ApprovalDate");
                                    get_eqp_no=jsonObject.getString("EquipmentNo");
                                    get_indate= jsonObject.getString("InDate");
                                    get_previous_cargo=jsonObject.getString("PreviousCargo");
                                    get_laststatus_date=jsonObject.getString("LastStatusDate");
                                    get_lbr_rate=jsonObject.getString("LaborRate");
                                    get_last_test_type=jsonObject.getString("LastTestType");
                                    get_nexttest_type =jsonObject.getString("NextTestType");
                                    get_last_test_date=jsonObject.getString("LastTestDate");
                                    get_nexttest_date=jsonObject.getString("NextTestDate");
                                    get_lst_survey=jsonObject.getString("LastSurveyor");
                                    get_validate_period_test=jsonObject.getString("ValidityPeriodforTest");
                                    get_repair_type_id= jsonObject.getString("RepairTypeID");
                                    get_repair_type_cd= jsonObject.getString("RepairTypeCD");
                                    get_invoice_prty_cd= jsonObject.getString("InvoicingPartyCD");
                                    get_invoice_prty_id= jsonObject.getString("InvoicingPartyID");
                                    get_invoice_prty_name= jsonObject.getString("InvoicingPartyName");
                                    get_gi_trans_no=jsonObject.getString("GiTransactionNo");
                                    get_revision_no= jsonObject.getString("RevisionNo");
                                    get_cust_app_ref_no= jsonObject.getString("CustomerAppRef");
                                    get_approval_date= jsonObject.getString("ApprovalDate");
                                    get_party_app_ref_no= jsonObject.getString("PartyAppRef");
                                    get_survey_name= jsonObject.getString("SurveyorName");
                                    get_rematk= jsonObject.getString("Remarks");
                                    get_invoice_prty_cd= jsonObject.getString("InvoicingPartyCD");
                                    get_invoice_prty_id= jsonObject.getString("InvoicingPartyID");
                                    get_survey_completion_date=jsonObject.getString("SurveyCompletionDate");
                                    get_repair_est_no=jsonObject.getString("RepairEstimateNo");
                                    get_rep_status_id=jsonObject.getString("EquipmentStatusId");
                                    get_rep_status=jsonObject.getString("EquipmentStatusCd");

                                    for (int i = 0; i < LineItems_Json.length(); i++) {

                                        lineitem_bean = new LineItem_Bean();
                                        JSONObject jsonObject_line = LineItems_Json.getJSONObject(i);

                                        lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject_line.getString("RPR_ESTMT_DTL_ID"));
                                        lineitem_bean.setRPR_ESTMT_ID(jsonObject_line.getString("RPR_ESTMT_ID"));
                                        lineitem_bean.setItem(jsonObject_line.getString("ItemCd"));
                                        lineitem_bean.setItemCode(jsonObject_line.getString("Item"));
                                        lineitem_bean.setRepair_Code(jsonObject_line.getString("RepairCd"));
                                        lineitem_bean.setRepair_Code_Id(jsonObject_line.getString("Repair"));
                                        lineitem_bean.setSubItem(jsonObject_line.getString("SubItemCd"));
                                        lineitem_bean.setSuIitemCode(jsonObject_line.getString("SubItem"));
                                        lineitem_bean.setDamage_Code(jsonObject_line.getString("DamageCd"));
                                        lineitem_bean.setDamage_Code_Id(jsonObject_line.getString("Damage"));
                                        lineitem_bean.setManHour(jsonObject_line.getString("ManHour"));
                                        lineitem_bean.setManHour_Cost(jsonObject_line.getString("MHC"));
                                        lineitem_bean.setMHR(jsonObject_line.getString("MHR"));
                                        lineitem_bean.setMetial_Cost(jsonObject_line.getString("MC"));
                                        lineitem_bean.setResponsibility_Cd(jsonObject_line.getString("ResponsibilityCd"));
                                        lineitem_bean.setResponsibility(jsonObject_line.getString("Responsibility"));
                                        lineitem_bean.setTotel(jsonObject_line.getString("TC"));
                                        lineitem_bean.setRemark(jsonObject_line.getString("DmgRprRemarks"));

                                        lineitem_arraylist.add(lineitem_bean);
                                        totel_amount=totel_amount+(int) Math.round(Float.parseFloat(jsonObject_line.getString("TC")));


                                    }


                                }


                            }
                        }catch (Exception e)
                        {

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


            if ((progressDialog != null) && progressDialog.isShowing()) {
                progressDialog.dismiss();
            }
            if(lineitem_arraylist!=null)
            {
                tv_lineitem_count.setText(String.valueOf(lineitem_arraylist.size()));
                for(int i=0;i<lineitem_arraylist.size();i++)
                {
                    TC=lineitem_arraylist.get(i).getTotel();
                    total=total+(int) Math.round(Float.parseFloat(TC));
                    //    man_hour=man_hour+Integer.parseInt(lineitem_arraylist.get(i).getManHour());
                    if(lineitem_arraylist.get(i).getManHour().equals(""))
                    {
                        man_hour=0.0f;
                    }else
                    {
                        man_hour=man_hour+Float.valueOf(lineitem_arraylist.get(i).getManHour());
                    }

                    man_hour_cost=man_hour_cost+Float.valueOf(lineitem_arraylist.get(i).getManHour_Cost());
                    metrial_cost=metrial_cost+Float.valueOf(lineitem_arraylist.get(i).getMetial_Cost());
                }
                Log.i("Totel Cost===", String.valueOf(total));
                tv_totalCost.setText(String.valueOf(total));
                tv_man_hour.setText(String.valueOf(man_hour));
                tv_manhour_cost.setText(String.valueOf(man_hour_cost));
                tv_metrial_cost.setText(String.valueOf(metrial_cost));

            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


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
            progressDialog = new ProgressDialog(SurveyCompletion_Summary.this);
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
