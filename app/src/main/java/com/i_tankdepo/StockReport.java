package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Gravity;
import android.view.MenuItem;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.i_tankdepo.Beanclass.CustomerReportBean;
import com.i_tankdepo.Beanclass.GeneralReportBean;
import com.i_tankdepo.Beanclass.TypeReportBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
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
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * Created by Admin on 1/4/2017.
 */

public class StockReport extends CommonActivity implements NavigationView.OnNavigationItemSelectedListener  {
    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu, im_down, im_up, im_heat_close, im_heat_ok, iv_back,iv_changeOfStatus;
    private Intent mServiceIntent;
    private ListView mandate_fields,optional_fields;
    private TextView tv_toolbarTitle,tv_heat_refresh,tv_mandate_fields,tv_optional_fields;
    private LinearLayout LL_status_submit,LL_run;
    private Button status_home,status_refresh,run_report;
    private String[] Mandate_Fields = {"Customer","Equipment Type","Previous Cargo","Current Status","Next Test Type","Depot"};
    private String[] Optional_Fields = {"Cleaning Date Form","Cleaning Date To","Inspection Date From","Inspection Date To","Current Status Date From"
                                        ,"Current Status Date To","Next Test Date From","Next Test Date To","Equipment Number","EIR Number","Out Date From"};
    private RadioButton radio_button;
    private RadioGroup radio_group,radio_group1;
    private RadioButton radioButton;
    private String selected;
    RelativeLayout RL_run_submit;
    private ProgressDialog progressDialog;
    private GeneralReportBean generalReportBean;
    private ArrayList<GeneralReportBean> generalReportBeanArrayList;
    private TypeReportBean typeReportBean;
    private CustomerReportBean customerReportBean;
    private ArrayList<CustomerReportBean> customerReportbeanArrayList;
    private ArrayList<TypeReportBean> typeReportBeanArrayList;
    private List<String> Cust_id=new ArrayList<>();
    private List<String> Equip_id=new ArrayList<>();
    private List<String> prevc_id=new ArrayList<>();
    private List<String> curnt_staus_id=new ArrayList<>();
    private List<String> next_test_id=new ArrayList<>();
    private List<String> dept_id=new ArrayList<>();
    private String curTime;
    private String systemDate;
    private JSONObject CustomerSummaryjsonarray;
    private JSONObject TypeSummaryjsonarray;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.stock_report);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setVisibility(View.GONE);

        RL_run_submit=(RelativeLayout)findViewById(R.id.RL_run_submit);
      /*  RL_run_submit.setAlpha(0.5f);
        RL_run_submit.setClickable(false);*/
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_mandate_fields = (TextView) findViewById(R.id.tv_mandate_fields);
        tv_optional_fields = (TextView) findViewById(R.id.tv_optional_fields);
        tv_mandate_fields.setOnClickListener(this);
        tv_optional_fields.setOnClickListener(this);
        tv_toolbarTitle.setText("Stock Report");
        tv_heat_refresh  = (TextView)findViewById(R.id.tv_heat_refresh);
        tv_heat_refresh.setText("Reset");

        LL_status_submit = (LinearLayout)findViewById(R.id.LL_status_submit);
        LL_run = (LinearLayout)findViewById(R.id.LL_run);
        LL_status_submit.setVisibility(View.GONE);

        LL_run.setOnClickListener(this);
        radio_button = (RadioButton)findViewById(R.id.radio_button);
        radio_group = (RadioGroup)findViewById(R.id.Radio_group);
        radio_group1 = (RadioGroup)findViewById(R.id.Radio_group1);


        status_home = (Button)findViewById(R.id.status_home);
        status_refresh = (Button)findViewById(R.id.status_refresh);
        run_report = (Button)findViewById(R.id.run_report);
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);

        status_home.setOnClickListener(this);
        status_refresh.setOnClickListener(this);
        run_report.setOnClickListener(this);

        SimpleDateFormat time = new SimpleDateFormat("hh:mm");
        curTime = time.format(new Date());
        systemDate = new SimpleDateFormat("dd-MMM-yyyy").format(new Date());

        drawer = (DrawerLayout) findViewById(R.id.drawer_layout);

        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        menu.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (drawer.isDrawerOpen(Gravity.START))
                    drawer.closeDrawer(Gravity.END);
                else
                    drawer.openDrawer(Gravity.START);
            }
        });

        navigationView.setNavigationItemSelectedListener(this);

    }



    @Override
    public void onClick(View view) {
        switch (view.getId() ){
            case R.id.iv_changeOfStatus:
                GlobalConstants.equipment_no="";
                GlobalConstants.status="";
                GlobalConstants.status_id="";
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));
                break;
            case R.id.status_home:
                startActivity(new Intent(getApplicationContext(),MainActivity.class));
                break;
            case R.id.status_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.run_report:
                if(cd.isConnectingToInternet())
                {
                    new Post_Stock_report().execute();
                }else
                {
                    shortToast(getApplicationContext(),"Please Check Your Internet Connection");
                }

                break;
            case R.id.LL_run:
                if(cd.isConnectingToInternet())
                {
                    new Post_Stock_report().execute();
                }else
                {
                    shortToast(getApplicationContext(),"Please Check Your Internet Connection");
                }


                break;
            case R.id.tv_mandate_fields:
                int selectedId = radio_group.getCheckedRadioButtonId();

                // find the radiobutton by returned id
                radioButton = (RadioButton) findViewById(selectedId);
                 selected=radioButton.getText().toString();
              //  shortToast(getApplicationContext(), selected);

                Intent i=new Intent(getApplicationContext(),SelectOptions.class);
                GlobalConstants.from="mandate";
                GlobalConstants.selectedName=selected;

                startActivity(i);

                break;
            case R.id.tv_optional_fields:
                 selectedId = radio_group1.getCheckedRadioButtonId();

                radioButton = (RadioButton) findViewById(selectedId);

                try {
                    selected = radioButton.getText().toString();
                    if(selected.length()<=0)
                    {

                    }else
                    {
                        Intent i1 = new Intent(getApplicationContext(), SelectOptions.class);
                        GlobalConstants.from = "Optional";
                        GlobalConstants.selectedName_optional = selected;
                        startActivity(i1);
                    }

                }catch (Exception e)
                {
                    shortToast(getApplicationContext(),"Please Select One Optional Field..!");

                }


                break;
        }
    }
    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem item) {
        // Handle navigation view item clicks here.
        //
        int id = item.getItemId();

        if (id == R.id.nav_home) {
            finish();
            startActivity(new Intent(getApplicationContext(), MainActivity.class));
            // Handle the camera action
        } else if (id == R.id.nav_changePwd) {
            startActivity(new Intent(getApplicationContext(), Change_Password.class));
        } else if (id == R.id.nav_Logout) {
            {

                if (mServiceIntent != null)
                    getApplicationContext().stopService(mServiceIntent);
                SharedPreferences.Editor editor = sp.edit();
                editor.putBoolean(SP_LOGGED_IN, false);
                editor.commit();
                finish();
                Intent in = new Intent(getApplicationContext(), Login_Activity.class);
                startActivity(in);

            }
        }
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

    }

    public class Post_Stock_report extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONObject preadvicejsonObject;
        private JSONArray Custjsonlist;
        private JSONArray Equipjsonlist;
        private JSONArray previousjsonlist;
        private JSONArray currentStausjsonlist;
        private JSONArray NextTestjsonlist;
        private JSONArray deoptjsonlist;


        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(StockReport.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLStock_Run_Report);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try{
                JSONObject jsonObject = new JSONObject();
                JSONObject jsonObjectStockReportModel = new JSONObject();

                //    preadvicejsonlist = new JSONArray();
                Custjsonlist = new JSONArray();
                Equipjsonlist = new JSONArray();
                previousjsonlist = new JSONArray();
                currentStausjsonlist = new JSONArray();
                NextTestjsonlist = new JSONArray();
                deoptjsonlist = new JSONArray();

                try {
                    for (int i = 0; i < Cust_id.size(); i++) {
                        preadvicejsonObject = new JSONObject();
                        preadvicejsonObject.put("Type", Cust_id.get(i));
                        Custjsonlist.put(preadvicejsonObject);
                    }


                    for (int i = 0; i < Equip_id.size(); i++) {
                        preadvicejsonObject = new JSONObject();
                        preadvicejsonObject.put("Type", Equip_id.get(i));
                        Equipjsonlist.put(preadvicejsonObject);
                    }

                    for (int i = 0; i < prevc_id.size(); i++) {
                        preadvicejsonObject = new JSONObject();
                        preadvicejsonObject.put("Type", prevc_id.get(i));
                        previousjsonlist.put(preadvicejsonObject);
                    }


                    for (int i = 0; i < curnt_staus_id.size(); i++) {
                        preadvicejsonObject = new JSONObject();
                        preadvicejsonObject.put("Type", curnt_staus_id.get(i));
                        currentStausjsonlist.put(preadvicejsonObject);
                    }

                    for (int i = 0; i < next_test_id.size(); i++) {
                        preadvicejsonObject = new JSONObject();
                        preadvicejsonObject.put("Type", next_test_id.get(i));
                        NextTestjsonlist.put(preadvicejsonObject);
                    }
                    for (int i = 0; i < dept_id.size(); i++) {
                        preadvicejsonObject = new JSONObject();
                        preadvicejsonObject.put("Type", dept_id.get(i));
                        deoptjsonlist.put(preadvicejsonObject);
                    }
                }catch (Exception e)
                {

                }
                jsonObjectStockReportModel.put("Customer",Custjsonlist);
                jsonObjectStockReportModel.put("Depot",deoptjsonlist);
                jsonObjectStockReportModel.put("Next_Test_Type",NextTestjsonlist);
                jsonObjectStockReportModel.put("Current_Status",currentStausjsonlist);
                jsonObjectStockReportModel.put("Previous_Cargo",previousjsonlist);
                jsonObjectStockReportModel.put("Equipment_Type",Equipjsonlist);

                jsonObjectStockReportModel.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObjectStockReportModel.put("Next_Test_Date_From","");
                jsonObjectStockReportModel.put("Next_Test_Date_To", "");
                jsonObjectStockReportModel.put("Equipment_No", "");
                jsonObjectStockReportModel.put("EIR_No", "");
                jsonObjectStockReportModel.put("Cleaning_Date_From","");
                jsonObjectStockReportModel.put("Cleaning_Date_To", "");
                jsonObjectStockReportModel.put("Inspection_Date_From", "");
                jsonObjectStockReportModel.put("Inspection_Date_To", "");
                jsonObjectStockReportModel.put("Current_Status_Date_From", "");
                jsonObjectStockReportModel.put("In_Date_From", "");
                jsonObjectStockReportModel.put("In_Date_To", "");
                jsonObjectStockReportModel.put("Current_Status_Date_To", "");


                    jsonObjectStockReportModel.put("Out_Date_From", systemDate);


                    jsonObjectStockReportModel.put("Out_Date_To", systemDate);

                jsonObject.put("StockReportModel",jsonObjectStockReportModel);
               /* JSONObject jsonObject1 = new JSONObject();
                jsonObject1.put("Credentials",jsonObject);*/

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                Log.d("Search_request", jsonObject.toString());

                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("ActivityStatus");
                CustomerSummaryjsonarray = getJsonObject.getJSONObject("CustomerSummary");
                TypeSummaryjsonarray = getJsonObject.getJSONObject("TypeSummary");
                if (jsonarray == null || CustomerSummaryjsonarray==null ||TypeSummaryjsonarray==null ) {
                    runOnUiThread(new Runnable() {
                        public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                            shortToast(getApplicationContext(), "No Records Found");
                        }
                    });

                }else {
                    try {

                        generalReportBeanArrayList = new ArrayList<GeneralReportBean>();
                        typeReportBeanArrayList = new ArrayList<TypeReportBean>();
                        customerReportbeanArrayList = new ArrayList<CustomerReportBean>();

                        for (int i = 0; i < jsonarray.length(); i++) {
                            jsonObject = jsonarray.getJSONObject(i);
                            generalReportBean = new GeneralReportBean();

                            generalReportBean.setDepot(jsonObject.getString("Depot"));
                            generalReportBean.setCustomer(jsonObject.getString("Customer"));
                            generalReportBean.setEquipmentNo(jsonObject.getString("EquipmentNo"));
                            generalReportBean.setType(jsonObject.getString("Type"));
                            generalReportBean.setIndate(jsonObject.getString("Indate"));
                            generalReportBean.setPreviousCargo(jsonObject.getString("PreviousCargo"));
                            generalReportBean.setEirNo(jsonObject.getString("EirNo"));
                            generalReportBean.setCleaningCertNo(jsonObject.getString("CleaningCertNo"));
                            generalReportBean.setCurrentStatusDate(jsonObject.getString("CurrentStatusDate"));
                            generalReportBean.setCurrentStatus(jsonObject.getString("CurrentStatus"));
                            generalReportBean.setCleaningDate(jsonObject.getString("CleaningDate"));
                            generalReportBean.setInspectionDate(jsonObject.getString("InspectionDate"));
                            generalReportBean.setRemarks(jsonObject.getString("Remarks"));
                            generalReportBean.setNextTestDate(jsonObject.getString("NextTestDate"));
                            generalReportBean.setNextTestType(jsonObject.getString("NextTestType"));

                            generalReportBeanArrayList.add(generalReportBean);

                        }


                        customerReportBean = new CustomerReportBean();

                        customerReportBean.setCustomer(CustomerSummaryjsonarray.getString("Customer"));
                        // customerReportBean.setType(jsonObject.getString("Type"));
                        customerReportBean.setIND(CustomerSummaryjsonarray.getString("IND"));
                        customerReportBean.setPHL(CustomerSummaryjsonarray.getString("PHL"));
                        customerReportBean.setACN(CustomerSummaryjsonarray.getString("ACN"));
                        customerReportBean.setAWECLN(CustomerSummaryjsonarray.getString("AWECLN"));
                        customerReportBean.setAWE(CustomerSummaryjsonarray.getString("AWE"));
                        customerReportBean.setAAR(CustomerSummaryjsonarray.getString("AAR"));
                        customerReportBean.setAUR(CustomerSummaryjsonarray.getString("AUR"));
                        customerReportBean.setASR(CustomerSummaryjsonarray.getString("ASR"));
                        customerReportBean.setSRV(CustomerSummaryjsonarray.getString("SRV"));
                        customerReportBean.setAVLCLN(CustomerSummaryjsonarray.getString("AVLCLN"));
                        customerReportBean.setAVLINS(CustomerSummaryjsonarray.getString("AVLINS"));
                        customerReportBean.setINSRPC(CustomerSummaryjsonarray.getString("INSRPC"));
                        customerReportBean.setRPC(CustomerSummaryjsonarray.getString("RPC"));
                        customerReportBean.setSTO(CustomerSummaryjsonarray.getString("STO"));
                        customerReportBean.setAVL(CustomerSummaryjsonarray.getString("AVL"));
                        customerReportBean.setOUT(CustomerSummaryjsonarray.getString("OUT"));
                        customerReportBean.setTOTAL(CustomerSummaryjsonarray.getString("TOTAL"));

                        customerReportbeanArrayList.add(customerReportBean);


                        typeReportBean = new TypeReportBean();

//                            typeReportBean.setCustomer(jsonObject.getString("Customer"));
                        typeReportBean.setType(TypeSummaryjsonarray.getString("Type"));
                        typeReportBean.setIND(TypeSummaryjsonarray.getString("IND"));
                        typeReportBean.setPHL(TypeSummaryjsonarray.getString("PHL"));
                        typeReportBean.setACN(TypeSummaryjsonarray.getString("ACN"));
                        typeReportBean.setAWECLN(TypeSummaryjsonarray.getString("AWECLN"));
                        typeReportBean.setAWE(TypeSummaryjsonarray.getString("AWE"));
                        typeReportBean.setAAR(TypeSummaryjsonarray.getString("AAR"));
                        typeReportBean.setAUR(TypeSummaryjsonarray.getString("AUR"));
                        typeReportBean.setASR(TypeSummaryjsonarray.getString("ASR"));
                        typeReportBean.setSRV(TypeSummaryjsonarray.getString("SRV"));
                        typeReportBean.setAVLCLN(TypeSummaryjsonarray.getString("AVLCLN"));
                        typeReportBean.setAVLINS(TypeSummaryjsonarray.getString("AVLINS"));
                        typeReportBean.setINSRPC(TypeSummaryjsonarray.getString("INSRPC"));
                        typeReportBean.setRPC(TypeSummaryjsonarray.getString("RPC"));
                        typeReportBean.setSTO(TypeSummaryjsonarray.getString("STO"));
                        typeReportBean.setAVL(TypeSummaryjsonarray.getString("AVL"));
                        typeReportBean.setOUT(TypeSummaryjsonarray.getString("OUT"));
                        typeReportBean.setTOTAL(TypeSummaryjsonarray.getString("TOTAL"));

                        typeReportBeanArrayList.add(typeReportBean);


                    }catch (Exception e)
                    {

                    }
                }
                /*else if(jsonarray.length()<1){
                    runOnUiThread(new Runnable(){

                        @Override
                        public void run(){
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(),"No Records Found");


                        }
                    });

                }*/

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



            if (jsonarray != null || CustomerSummaryjsonarray!=null ||TypeSummaryjsonarray!=null )
            {

                Intent i=new Intent(getApplicationContext(),GeneralReport.class);
                GlobalConstants.customerReportbeanArrayList=customerReportbeanArrayList;
                GlobalConstants.typeReportBeanArrayList=typeReportBeanArrayList;
                GlobalConstants.generalReportBeanArrayList=generalReportBeanArrayList;

                startActivity(i);

            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }
            progressDialog.dismiss();

        }

    }

}
