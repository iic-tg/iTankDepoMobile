package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
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
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.CustomerDropdownBean;
import com.i_tankdepo.Beanclass.Image_Bean;
import com.i_tankdepo.Beanclass.LineItem_Bean;
import com.i_tankdepo.Beanclass.RepairBean;
import com.i_tankdepo.Beanclass.RepairCompletionBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.adapter.SpinnerCustomAdapter;
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
import java.util.ArrayList;
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
public class Repair_Completions_wizard extends CommonActivity implements View.OnClickListener {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView iv_changeOfStatus, menu, im_down, im_up, im_heat_close, im_heat_ok, im_skip, iv_back;


    private TextView tv_toolbarTitle, tv_search_options, no_data;
    LinearLayout LL_hole, LL_heat_submit, LL_search_Value, LL_heat;

    private ProgressDialog progressDialog;

    private Intent mServiceIntent;
    private Button heat_refresh, heat_home, heat_submit, heating, cleaning, inspection;
    private FragmentActivity mycontaxt;
    private TextView customer_name, equipment_no, eqip_type;
    private Button bt_refresh, LL_Line_iteam;
    private Button button_fetch, LL_add_details;
    private Button LL_attachments;
    private Button LL_summary;
    private Spinner sp_tarif_group, sp_tarif_code;
    private RelativeLayout RL_next;
    private ArrayList<String[]> dropdown_customer_list;
    private ArrayList<CustomerDropdownBean> CustomerDropdownArrayList;
    private CustomerDropdownBean customer_DropdownBean;
    private String tariffName;
    private String tariffCode;
    List<String> Cust_name = new ArrayList<>();
    List<String> Cust_code = new ArrayList<>();
    private ArrayList<String> worldlist;
    private ArrayList<String> Subworldlist;
    private ArrayList<String> damage_worldlist;
    private ArrayList<String> repair_worldlist;
    private LinearLayout LL_Line;
    private EditText ed_tariff_code, ed_endDate;
    private String tariff_code_name, tariff_group_name;
    private JSONArray jsonarray;
    private ArrayList<CustomerDropdownBean> Tariff_code_ArrayList;
    private ArrayList<CustomerDropdownBean> Tariff_Itemcode_ArrayList;
    private ArrayList<CustomerDropdownBean> Tariff_SubItemcode_ArrayList;
    private ArrayList<CustomerDropdownBean> Tariff_DamageItemcode_ArrayList;
    private ArrayList<CustomerDropdownBean> Tariff_RepairItemcode_ArrayList;
    private String get_sp_tariffGroup_code, get_sp_tariff_code, get_sp_item_code;
    private LineItem_Bean moreInfo_lineItem;
    private ArrayList<LineItem_Bean> LineItem_MoreInfo_arraylist;
    private ArrayList<LineItem_Bean> lineitem_arraylist;
    private LineItem_Bean lineitem_bean;
    private ViewHolder holder;
    private UserListAdapter adapter;
    private ListView list_line_items;
    private JSONObject object2;
    private JSONObject object1;
    private TextView tv_amount;
    private int totel_amount = 0;
    private JSONObject jsonObject;
    private RepairBean repair_bean;
    private String line_from;
    private TextView repair_estimate_text;
    private Button repair_completion, repair_estimate, repair_approval, survey_completion;
    private RelativeLayout RL_heating, RL_Repair;
    private ImageView more_info;
    private ImageView iv_send_mail;
    private ArrayList<RepairCompletionBean> repair_completion_arraylist;
    private String Line_item_Json;
    private Button notification_text;
    private String Activity_Date;
    private String location;
    private String estimate_mh;
    private Image_Bean image_bean;
    private Bitmap selectedImageBitmap;
    private String encodedImage;
    private ArrayList<Image_Bean> encodeArray;
    private SpinnerCustomAdapter customAdapter;
    private ArrayList<Image_Bean> multiple_encodeArray;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.repair_estimate_completion);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

        encodeArray=GlobalConstants.multiple_encodeArray;
        Activity_Date=GlobalConstants.ActivityDate;
        notification_text=(Button)findViewById(R.id.notification_text);

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
        repair_completion_arraylist=GlobalConstants.repair_completion_arraylist;
        Line_item_Json= GlobalConstants.Line_item_Json;
        sp_tarif_group = (Spinner) findViewById(R.id.sp_tarif_group);
        sp_tarif_code = (Spinner) findViewById(R.id.sp_tarif_code);
        list_line_items = (ListView) findViewById(R.id.list_line_items);
        iv_send_mail = (ImageView) findViewById(R.id.iv_send_mail);
        iv_send_mail.setOnClickListener(this);
        menu = (ImageView) findViewById(R.id.iv_menu);
        //    tv_amount = (TextView) findViewById(R.id.tv_amount);
        iv_changeOfStatus = (ImageView) findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        im_skip = (ImageView) findViewById(R.id.im_skip);
        bt_refresh = (Button) findViewById(R.id.heat_refresh);
        bt_refresh.setOnClickListener(this);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setOnClickListener(this);
        im_skip.setOnClickListener(this);
        //    tv_amount.setOnClickListener(this);
        menu.setVisibility(View.GONE);
        LL_Line_iteam = (Button) findViewById(R.id.Line_iteam);
        LL_Line = (LinearLayout) findViewById(R.id.LL_Line_iteam);
        LL_Line.setBackgroundColor(getResources().getColor(R.color.submit));

        repair_estimate = (Button) findViewById(R.id.repair_estimate);
        repair_estimate.setOnClickListener(this);
        repair_approval = (Button) findViewById(R.id.repair_approval);
        repair_approval.setVisibility(View.GONE);
        repair_completion = (Button) findViewById(R.id.repair_completion);
        repair_completion.setVisibility(View.GONE);
        survey_completion = (Button) findViewById(R.id.survey_completion);
        survey_completion.setVisibility(View.GONE);
        LL_add_details = (Button) findViewById(R.id.add_details);
        button_fetch = (Button) findViewById(R.id.button_fetch);
        ed_endDate = (EditText) findViewById(R.id.ed_endDate);
        ed_tariff_code = (EditText) findViewById(R.id.ed_tariff_code);
        ed_tariff_code.setOnClickListener(this);
        ed_endDate.setOnClickListener(this);
        LL_add_details.setOnClickListener(this);
        LL_attachments = (Button) findViewById(R.id.attachments);
        LL_attachments.setOnClickListener(this);

        RL_heating = (RelativeLayout) findViewById(R.id.RL_heating);
        RL_Repair = (RelativeLayout) findViewById(R.id.RL_Repair);
        RL_heating.setVisibility(View.GONE);

        heat_home = (Button) findViewById(R.id.heat_home);
        heat_refresh = (Button) findViewById(R.id.heat_refresh);
        heat_submit = (Button) findViewById(R.id.heat_submit);
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);
        repair_estimate_text = (TextView) findViewById(R.id.tv_heating);
        repair_estimate_text.setText("Repair Completion");
        LL_heat_submit.setClickable(false);
        LL_heat_submit.setAlpha(0.5f);
        button_fetch.setAlpha(0.5f);
        LL_heat = (LinearLayout) findViewById(R.id.LL_heat);
        LL_heat.setOnClickListener(this);
        //  button_fetch.setOnClickListener(this);
        more_info = (ImageView) findViewById(R.id.more_info);
        more_info.setOnClickListener(this);

        Log.i("from_values", GlobalConstants.repair_EstimateID + "," + GlobalConstants.lobor_rate + "," + GlobalConstants.repairEstimateNo);

        ArrayList<Image_Bean> encodeArray=new ArrayList();
        GlobalConstants.multiple_encodeArray=encodeArray;
//        tabLayout.setupWithViewPager(viewPager);


        // tabLayout.clearOnTabSelectedListeners();

        if (cd.isConnectingToInternet()) {
//            new Tarif_Group_details().execute();
            new Get_Repair_MySubmit_details().execute();


        } else {

            shortToast(getApplicationContext(), "Please check your Internet Connection.");

        }

        sp_tarif_group.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {

                tariff_group_name = CustomerDropdownArrayList.get(i).getName();
                get_sp_tariffGroup_code = CustomerDropdownArrayList.get(i).getCode();
                ed_endDate.setText(tariff_group_name);
                new Tarif_Code_details().execute();


            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        sp_tarif_code.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {

                tariff_code_name = Tariff_code_ArrayList.get(i).getName();
                get_sp_tariff_code = Tariff_code_ArrayList.get(i).getCode();
                ed_tariff_code.setText(tariff_code_name);

            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
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
        customer_name.setText(GlobalConstants.customer_name + " , " + GlobalConstants.equipment_no + " , " + GlobalConstants.type);
        equipment_no = (TextView) findViewById(R.id.text2);
//        equipment_no.setText(GlobalConstants.equipment_no);
        eqip_type = (TextView) findViewById(R.id.text3);
//        eqip_type.setText(GlobalConstants.previous_cargo);
        tv_toolbarTitle.setText("Line Details");

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
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(), MainActivity.class));
                break;
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
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
            case R.id.more_info:

                popUp_equipment_info(GlobalConstants.equipment_no, GlobalConstants.status, GlobalConstants.status_id, GlobalConstants.previous_cargo, "", "", "", "");


                break;
            case R.id.repair_estimate:
                finish();
                startActivity(new Intent(getApplicationContext(), Repair_MainActivity.class));
                break;
            case R.id.add_details:


                if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                    //   repair_arraylist=GlobalConstants.repair_arraylist;
                    //  new Get_Repair_MySubmit_details().execute();

                    GlobalConstants.from = "MysubmitlineItem";

                } else {
                    GlobalConstants.from = "PendinglineItem";
                }

                SharedPreferences.Editor editor = sp.edit();
                editor.putString(SP_LINE_ITEM_JSON, String.valueOf(object1));
                editor.commit();
                String equipment_no = GlobalConstants.equipment_no;
                String actual_manHr = GlobalConstants.actual_manHr;
                String completion_date = GlobalConstants.completion_date;
                GlobalConstants.equipment_no = equipment_no;
                String act_mh=GlobalConstants.actual_mh;
                String time=GlobalConstants.time;
                estimate_mh=GlobalConstants.estimated_manHr;
                GlobalConstants.estimated_manHr=estimate_mh;
                String remarks=GlobalConstants.remark;
                GlobalConstants.actual_mh=act_mh;
                GlobalConstants.attach_count=notification_text.getText().toString();
                GlobalConstants.time=time;
                location = GlobalConstants.location ;
                GlobalConstants.location=location;
                GlobalConstants.completion_date=completion_date;
                GlobalConstants.actual_manHr=actual_manHr;
                GlobalConstants.remark=remarks;
                GlobalConstants.ActivityDate=Activity_Date;
                GlobalConstants.multiple_encodeArray=encodeArray;
                GlobalConstants.Line_item_Json = Line_item_Json;
                GlobalConstants.lineItem_beanArrayList = lineitem_arraylist;
                GlobalConstants.repair_completion_arraylist = repair_completion_arraylist;
                startActivity(new Intent(getApplicationContext(), AddRepair_Completion_Details.class));


                break;
            case R.id.im_skip:
                LL_add_details.performClick();
                break;
            case R.id.attachments:


                if (GlobalConstants.from.equalsIgnoreCase("MySubmitRepair") || GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        || GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                    //   repair_arraylist=GlobalConstants.repair_arraylist;
                    //  new Get_Repair_MySubmit_details().execute();
                    GlobalConstants.from = "MysubmitlineItem";

                } else {
                    GlobalConstants.from = "PendinglineItem";
                }
                editor = sp.edit();
                editor.putString(SP_LINE_ITEM_JSON, String.valueOf(object1));
                editor.commit();
                finish();
                 equipment_no = GlobalConstants.equipment_no;
                 actual_manHr = GlobalConstants.actual_manHr;
                 location = GlobalConstants.location ;
                GlobalConstants.location=location;
                    estimate_mh=GlobalConstants.estimated_manHr;
                    GlobalConstants.estimated_manHr=estimate_mh;

                 completion_date = GlobalConstants.completion_date;
                GlobalConstants.equipment_no = equipment_no;
                 act_mh=GlobalConstants.actual_mh;
                 time=GlobalConstants.time;
                 remarks=GlobalConstants.remark;
                GlobalConstants.actual_mh=act_mh;
                GlobalConstants.time=time;
                GlobalConstants.completion_date=completion_date;
                GlobalConstants.actual_manHr=actual_manHr;
                GlobalConstants.remark=remarks;
                GlobalConstants.ActivityDate=Activity_Date;
                GlobalConstants.multiple_encodeArray=encodeArray;
                GlobalConstants.lineItem_beanArrayList = lineitem_arraylist;
                GlobalConstants.repair_completion_arraylist = repair_completion_arraylist;
//                        GlobalConstants.customer_Id=customer_Id;
                GlobalConstants.Line_item_Json = Line_item_Json;
                startActivity(new Intent(getApplicationContext(), Attach_Repair_Completion.class));


                break;


            case R.id.tv_amount:
                LL_summary.performClick();
                break;
            case R.id.ed_endDate:

                sp_tarif_group.performClick();

                break;
            case R.id.ed_tariff_code:

                sp_tarif_code.performClick();

                break;
            case R.id.iv_back:
                onBackPressed();
                break;
            case R.id.iv_changeOfStatus:
                GlobalConstants.Line_item_Json = String.valueOf(object1);
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

    public class Tarif_Group_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Completions_wizard.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLTariffGroup);
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
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("arrayOfDropdowns");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list" + jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
//                                shortToast(getApplicationContext(), "No Records Found.");
                            }
                        });
                    } else {

                        dropdown_customer_list = new ArrayList<>();


                        worldlist = new ArrayList<String>();
                        CustomerDropdownArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            customer_DropdownBean.setName(jsonObject.getString("TRFF_GRP_CD"));
                            customer_DropdownBean.setCode(jsonObject.getString("TRFF_GRP_ID"));
                            tariffName = jsonObject.getString("TRFF_GRP_CD");
                            tariffCode = jsonObject.getString("TRFF_GRP_ID");

                            String[] set1 = new String[2];
                            set1[0] = tariffName;
                            set1[1] = tariffCode;
                            dropdown_customer_list.add(set1);
                            Cust_name.add(set1[0]);
                            Cust_code.add(set1[1]);
                            CustomerDropdownArrayList.add(customer_DropdownBean);
                            worldlist.add(tariffName);


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
                sp_tarif_group.setAdapter(CustomerAdapter);

            } else {
//                shortToast(getApplicationContext(), "Data Not Found");

            }

            progressDialog.dismiss();

        }

    }

    public class Tarif_Code_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Completions_wizard.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
            if ((progressDialog != null) && progressDialog.isShowing())
                progressDialog.show();
        }

        @Override
        protected Void doInBackground(Void... params) {

            ServiceHandler sh = new ServiceHandler();
            HttpParams httpParameters = new BasicHttpParams();
            DefaultHttpClient httpClient = new DefaultHttpClient(httpParameters);
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLTariff_Code);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("TariffGroupCode", tariff_group_name);


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
//                                shortToast(getApplicationContext(), "No Records Found.");
                            }
                        });
                    } else {




                        Tariff_code_ArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {
                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);
                            customer_DropdownBean.setName(jsonObject.getString("TRFF_CD_CD"));
                            customer_DropdownBean.setCode(jsonObject.getString("TRFF_CD_ID"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("TRFF_CD_DESCRPTN_VC"));
                            Tariff_code_ArrayList.add(customer_DropdownBean);
                        }

                    }
                } else if (jsonarray.length() < 1) {
                    runOnUiThread(new Runnable() {

                        @Override
                        public void run() {
                            //update ui here
                            // display toast here
//                            shortToast(getApplicationContext(), "No Records Found.");


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
            if(jsonarray!=null) {
                if (Tariff_code_ArrayList != null) {
                /*ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, worldlist);
                CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_tarif_code.setAdapter(CustomerAdapter);*/
                    customAdapter = new SpinnerCustomAdapter(Repair_Completions_wizard.this, R.layout.spinner_text, Tariff_code_ArrayList);
                    customAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    // Set adapter to spinner
                    sp_tarif_code.setAdapter(customAdapter);
                } else {
//                    shortToast(getApplicationContext(), "Data Not Found");

                }
            }else{
                Tariff_code_ArrayList.removeAll(Tariff_code_ArrayList);
                ed_tariff_code.setText("");
            }


        }

    }


    public class UserListAdapter extends BaseAdapter {

        private final ArrayList<LineItem_Bean> arraylist;
        private ArrayList<LineItem_Bean> list;
        ArrayList<String> worldlist;
        Context context;
        ArrayList<String> Subworldlist;
        ArrayList<String> damageworldlist;
        ArrayList<String> repairworldlist;
        int resource;
        private LineItem_Bean userListBean;
        int lastPosition = -1;

        public UserListAdapter(Context context, int resource, ArrayList<LineItem_Bean> list) {
            this.context = context;
            this.list = list;
            this.resource = resource;
            this.arraylist = new ArrayList<LineItem_Bean>();
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

               /* Animation animation = AnimationUtils.loadAnimation(getApplicationContext(), (position > lastPosition) ? R.anim.up_from_bottom : R.anim.down_from_top);
                convertView.startAnimation(animation);
                lastPosition = position;*/
                holder.whole = (LinearLayout) convertView.findViewById(R.id.LL_whole);
                holder.tariff = (TextView) convertView.findViewById(R.id.tariff_code);
                holder.tariffGroup = (TextView) convertView.findViewById(R.id.tariff_group);
                holder.item = (TextView) convertView.findViewById(R.id.tv_item);
                holder.subitem = (TextView) convertView.findViewById(R.id.tv_sub_item);
                holder.damagecode = (TextView) convertView.findViewById(R.id.tv_damage_code);
                holder.repaircode = (TextView) convertView.findViewById(R.id.tv_repair_cost);
                holder.manhour = (TextView) convertView.findViewById(R.id.tv_man_hour);
                holder.manhourcost = (TextView) convertView.findViewById(R.id.tv_manhour_cost);
                holder.metrialcost = (TextView) convertView.findViewById(R.id.tv_metrial_cost);
                holder.sp_damage_code = (Spinner) convertView.findViewById(R.id.sp_damage_code);
                holder.sp_item = (Spinner) convertView.findViewById(R.id.sp_item);
                holder.sp_repair_code = (Spinner) convertView.findViewById(R.id.sp_repair_code);
                holder.sp_subItem = (Spinner) convertView.findViewById(R.id.sp_sub_item);
                holder.responsibility = (TextView) convertView.findViewById(R.id.tv_responsibility);
                holder.totelcost = (TextView) convertView.findViewById(R.id.tv_totalCost);
                holder.id_attachment = (ImageView) convertView.findViewById(R.id.id_attachment);
                holder.id_attachment.setVisibility(View.GONE);

                // R.id.tv_customerName,R.id.tv_Inv_no,R.id.tv_date,R.id.tv_val,R.id.tv_due
                convertView.setTag(holder);
            } else {
                holder = (ViewHolder) convertView.getTag();
            }
            if (list.size() < 1) {
                Toast.makeText(getApplicationContext(), "NO DATA FOUND", Toast.LENGTH_LONG).show();
            } else {

                userListBean = list.get(position);
                String group = getColoredSpanned("Tariff Group -", "#bbbbbb");
                String tariff = getColoredSpanned("Tariff -", "#bbbbbb");

                String surName_group = getColoredSpanned(userListBean.getTariff_Group(), "#cb0da5");
                String surName_tariff = getColoredSpanned(userListBean.getTariff_Code(), "#cb0da5");

                holder.tariffGroup.setText(Html.fromHtml(group + surName_group));
                holder.tariff.setText(Html.fromHtml(tariff + surName_tariff));
//                holder.tariffGroup.setText(userListBean.getTariff_Group());
                holder.item.setText(userListBean.getItemCode());
                holder.subitem.setText(userListBean.getSuIitemCode());
                holder.manhour.setText(userListBean.getManHour());
                holder.manhourcost.setText(userListBean.getManHour_Cost());
                holder.metrialcost.setText(userListBean.getMetial_Cost());
                holder.totelcost.setText(userListBean.getTotel());
                holder.damagecode.setText(userListBean.getDamage_Code_Id());
                holder.repaircode.setText(userListBean.getRepair_Code());
                holder.responsibility.setText(userListBean.getResponsibility_Cd());


            }
            return convertView;
        }


    }

    static class ViewHolder {
        TextView tariff, tariffGroup, item, subitem, damagecode, repaircode, repaircost, damagecost, manhour, manhourcost, responsibility,
                metrialcost, totelcost, remark;
        CheckBox checkBox;
        Spinner sp_item, sp_subItem, sp_repair_code, sp_damage_code;
        LinearLayout whole, LL_username;
        public ImageView id_attachment;
    }


    public class Get_Repair_MySubmit_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray LineItems_Json;
        private JSONArray attchement_Json;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Completions_wizard.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLRepairEstimateList);
            httpPost.setHeader("Content-Type", "application/json");

            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));

                jsonObject.put("Mode", "edit");
                jsonObject.put("PageName", "Repair Approval");

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                object1 = jsonrootObject;
                Log.d("rep_object1", String.valueOf(object1));
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("Response");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list" + jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                // shortToast(getApplicationContext(), "No Records Found");
                            }
                        });
                    } else {
                        try {

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
                                        lineitem_bean.setTariff_Group(jsonObject_line.getString("RPR_ESTMT_DTL_ID"));
                                        lineitem_bean.setRPR_ESTMT_ID(jsonObject_line.getString("RPR_ESTMT_ID"));
                                        lineitem_bean.setTariff_Code(jsonObject_line.getString("RPR_ESTMT_ID"));
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
                                        lineitem_bean.setMetial_Cost(jsonObject_line.getString("MC"));
                                        lineitem_bean.setMHR(jsonObject_line.getString("MHR"));
                                        lineitem_bean.setResponsibility_Cd(jsonObject_line.getString("ResponsibilityCd"));
                                        lineitem_bean.setResponsibility(jsonObject_line.getString("Responsibility"));
                                        lineitem_bean.setTotel(jsonObject_line.getString("TC"));
                                        lineitem_bean.setRemark(jsonObject_line.getString("DmgRprRemarks"));

                                        lineitem_arraylist.add(lineitem_bean);
                                        //  totel_amount=totel_amount+Integer.parseInt(jsonObject_line.getString("TotelCost"));
                                        totel_amount = totel_amount + (int) Math.round(Float.parseFloat(jsonObject_line.getString("TC")));


                                    }
                                }


                            }
                        } catch (Exception e) {

                        }
                    }
                } else if (jsonarray.length() < 1) {
                    runOnUiThread(new Runnable() {

                        @Override
                        public void run() {
                            //update ui here
                            // display toast here
                            //      shortToast(getApplicationContext(),"No Records Found");


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


            if ((progressDialog != null) && progressDialog.isShowing()) {
                progressDialog.dismiss();
            }


            if (lineitem_arraylist != null) {
                try {
                    notification_text.setText(String.valueOf(encodeArray.size()));

                } catch (Exception e) {

                }
                GlobalConstants.Line_item_Json = String.valueOf(object1);
                GlobalConstants.lineItem_beanArrayList = lineitem_arraylist;
                SharedPreferences.Editor editor = sp.edit();
                editor.putString(SP_LINE_ITEM_JSON, String.valueOf(object1));
                editor.commit();
                new Get_Repair_Completion_MySubmit_details().execute();
                new Get_Linedetails_Fetch().execute();
            } else {
                //   shortToast(getApplicationContext(),"Data Not Found");


            }
        }

    }

    public class Get_Linedetails_Fetch extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONObject jsonrootObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Completions_wizard.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLTariff_Fetch);
            httpPost.setHeader("Content-Type", "application/json");

            try {
                JSONObject jsonObject = new JSONObject();
                /*{"UserName":"Admin",
                    "Mode":"new",
                    "TariffId":"378",
                    "TariffGroupId":"65",
                    "bv_strEstimateId":"5",
                    "NextIndex":"",
                    "LaborRate":"0.00"}*/
                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("TariffId", "378");
                jsonObject.put("TariffGroupId", "65");
                jsonObject.put("bv_strEstimateId", "5");
                jsonObject.put("NextIndex", "");
                jsonObject.put("Mode", "new");
                jsonObject.put("LaborRate", "0.0");

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                Log.d("Request", jsonObject.toString());


                jsonrootObject = new JSONObject(resp);


                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");
                jsonarray = getJsonObject.getJSONArray("Response");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list" + jsonarray);
                    System.out.println("object1" + object1);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
//                                shortToast(getApplicationContext(), "No Records Found");
                            }
                        });
                    } else {

                        lineitem_arraylist = new ArrayList<LineItem_Bean>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            lineitem_bean = new LineItem_Bean();
                            jsonObject = jsonarray.getJSONObject(i);

                             /*
                                "RPR_ESTMT_DTL_ID": "1",
                                "RPR_ESTMT_ID": "",
                                "Item": "1",
                                "ItemCd": "1",
                                "SubItem": "1",
                                "SubItemCd": "1",
                                "Damage": "10",
                                "DamageCd": "10",
                                "Repair": "31",
                                "RepairCd": "BN",
                                "ManHour": "11.00",
                                "DmgRprRemarks": "nbbn",
                                "MHR": "0.00",
                                "MHC": "0.0000",
                                "MC": "12.00",
                                "TC": "12.0000",
                                "Responsibility": "66",
                                "ResponsibilityCd": "C"
                              */
                            lineitem_bean.setTariff_Code(get_sp_tariff_code);
                            lineitem_bean.setTariff_Group(get_sp_tariffGroup_code);
                            lineitem_bean.setItem(jsonObject.getString("ItemCd"));
                            lineitem_bean.setItemCode(jsonObject.getString("Item"));
                            lineitem_bean.setRepair_Code(jsonObject.getString("RepairCd"));
                            lineitem_bean.setRepair_Code_Id(jsonObject.getString("Repair"));
                            lineitem_bean.setSubItem(jsonObject.getString("SubItemCd"));
                            lineitem_bean.setSuIitemCode(jsonObject.getString("SubItem"));
                            lineitem_bean.setDamage_Code(jsonObject.getString("DamageCd"));
                            lineitem_bean.setDamage_Code_Id(jsonObject.getString("Damage"));
                            lineitem_bean.setManHour(jsonObject.getString("ManHour"));
                            lineitem_bean.setManHour_Cost(jsonObject.getString("MHC"));
                            lineitem_bean.setMetial_Cost(jsonObject.getString("MC"));
                            lineitem_bean.setResponsibility_Cd(jsonObject.getString("ResponsibilityCd"));
                            lineitem_bean.setResponsibility(jsonObject.getString("Responsibility"));
                            lineitem_bean.setTotel(jsonObject.getString("TC"));
                            lineitem_bean.setRemark(jsonObject.getString("DmgRprRemarks"));

                            lineitem_arraylist.add(lineitem_bean);
                            totel_amount = totel_amount + (int) Math.round(Float.parseFloat(jsonObject.getString("TC")));


                        }
                    }
                } else if (jsonarray.length() < 1) {
                    runOnUiThread(new Runnable() {

                        @Override
                        public void run() {
                            //update ui here
                            // display toast here
//                            shortToast(getApplicationContext(), "No Records Found");


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


//            progressDialog.dismiss();

            if (jsonarray != null) {
                adapter = new UserListAdapter(Repair_Completions_wizard.this, R.layout.line_item_row_survey, lineitem_arraylist);
                list_line_items.setAdapter(adapter);

            } else {

            }

        }

    }
    public class Get_Repair_Completion_MySubmit_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray LineItems_Json;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Completions_wizard.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
            if ((progressDialog != null) && progressDialog.isShowing())
                progressDialog.show();

        }

        @Override
        protected Void doInBackground(Void... params) {

            ServiceHandler sh = new ServiceHandler();
            HttpParams httpParameters = new BasicHttpParams();
            DefaultHttpClient httpClient = new DefaultHttpClient(httpParameters);
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLRepairCompletionList);
            httpPost.setHeader("Content-Type", "application/json");

            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));

                if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {
                    jsonObject.put("Mode", "edit");

                }else
                {
                    jsonObject.put("Mode", "new");

                }

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep_my", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("RC");
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

                        multiple_encodeArray =new ArrayList<Image_Bean>();
                        for (int j = 0; j < jsonarray.length(); j++) {

                            jsonObject = jsonarray.getJSONObject(j);
                            Log.i("equip",GlobalConstants.equipment_no );
                            if(GlobalConstants.equipment_no.equalsIgnoreCase(jsonObject.getString("EquipmentNo")))
                            {
                                LineItems_Json= jsonObject.getJSONArray("attchement");
                                for (int i = 0; i < LineItems_Json.length(); i++) {

                                    image_bean = new Image_Bean();

                                    JSONObject jsonObject_line = LineItems_Json.getJSONObject(i);


                                    image_bean.setImage_Name(jsonObject_line.getString("fileName"));
                                    image_bean.setImage(jsonObject_line.getString("imageUrl"));
                                    image_bean.setUriPath(jsonObject_line.getString("attchPath"));
                                    image_bean.setAttachment_Id(jsonObject_line.getString("attchId"));
                                    multiple_encodeArray.add(image_bean);
                                    // totel_amount=totel_amount+Integer.parseInt(jsonObject_line.getString("TotelCost"));



                                }


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

            if(encodeArray!=null)
            {
                try {
                    if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")||
                            GlobalConstants.from.equalsIgnoreCase("Repairpending")) {
                        notification_text.setText(String.valueOf(multiple_encodeArray.size()));

                    }else {
                        notification_text.setText((GlobalConstants.attach_count));
                    }

                } catch (Exception e) {

                }
            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }
        }

    }

}
