package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Context;
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

import android.text.Editable;
import android.text.TextWatcher;

import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.MenuItem;

import android.view.MotionEvent;

import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;

import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.CustomerDropdownBean;
import com.i_tankdepo.Beanclass.HeatingAccordionBean;
import com.i_tankdepo.Beanclass.LeakTestBean;
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
import java.util.ArrayList;
import java.util.List;
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
public class LeakTest extends CommonActivity implements NavigationView.OnNavigationItemSelectedListener {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu, im_down, im_up, im_heat_close, im_heat_ok, iv_back;
    private ListView heating_list_view;

    private String[] Fields = {"Customer", "Equipment No", "Type"};
    private String[] Operators = {"Contains", "Does Not Contain", "Equals", "Not Similar", "Similar"};


    private TextView tv_type,tv_equip_no,tv_cargo,tv_cust_name,tv_toolbarTitle, tv_search_options,no_data,list_noData;
    LinearLayout LL_hole, LL_heat_submit,LL_search_Value,LL_heat;
    List<String> Cust_name = new ArrayList<>();
    List<String> Cust_code = new ArrayList<>();
    private ArrayList<String[]> dropdown_customer_list = new ArrayList<>();
    private ArrayList<String> worldlist;
    private ArrayList<CustomerDropdownBean> CustomerDropdownArrayList;
    private CustomerDropdownBean customer_DropdownBean;
    private String CustomerName,CustomerCode;


    private Spinner sp_fields, sp_operator;
    private String fieldItems, opratorItems;
    private ProgressDialog progressDialog;
    private ArrayList<LeakTestBean> leakTest_arraylist = new ArrayList<>();
    private LeakTestBean leakTest_bean;
    private ViewHolder holder;
    private ArrayList<HeatingAccordionBean> heating_accordion_arraylist = new ArrayList<>();
    private HeatingAccordionBean heating_accordion_bean;
    ArrayList<Product> products;
    private ArrayList<Product> box;
    private ListAdapter boxAdapter;
    private UserListAdapter adapter;
    private ListView listview, search_heat_list;
    List<String> selected_name = new ArrayList<String>();

    private Intent mServiceIntent;

    private EditText ed_text1, searchView1, searchView2;
    private Button heat_refresh, heat_home, heat_submit,heating,cleaning,inspection,Leaktest,leakTest,bt_gateout;
    private String getEditText;
    private RelativeLayout RL_heating,RL_Repair;
    private TextView leakTest_text;
    private ImageView iv_changeOfStatus;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.leak_test);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setVisibility(View.GONE);
        im_down = (ImageView) findViewById(R.id.im_down);
        im_up = (ImageView) findViewById(R.id.im_up);
        listview = (ListView) findViewById(R.id.heating_list_view);
        LL_hole = (LinearLayout) findViewById(R.id.LL_hole);
        heat_home = (Button) findViewById(R.id.heat_home);
        heat_refresh = (Button) findViewById(R.id.heat_refresh);
        heat_submit = (Button) findViewById(R.id.heat_submit);
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);
        LL_heat_submit.setAlpha(0.5f);
        LL_heat_submit.setClickable(false);
        LL_heat = (LinearLayout)findViewById(R.id.LL_heat);


        search_heat_list = (ListView) findViewById(R.id.search_heat_list);
        ed_text1 = (EditText) findViewById(R.id.ed_text1);

        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_Repair.setVisibility(View.GONE);



        searchView1 = (EditText) findViewById(R.id.searchView1);
        searchView2 = (EditText) findViewById(R.id.searchView2);
        im_heat_close = (ImageView) findViewById(R.id.im_heat_close);
        im_heat_ok = (ImageView) findViewById(R.id.im_heat_ok);
        no_data = (TextView)findViewById(R.id.no_data);
        no_data.setVisibility(View.GONE);

        heating = (Button)findViewById(R.id.heating);
        heating.setVisibility(View.GONE);
        Leaktest = (Button)findViewById(R.id.leakTest);
        leakTest_text = (TextView)findViewById(R.id.tv_heating);
        leakTest_text.setGravity(Gravity.CENTER_HORIZONTAL);
        leakTest_text.setText("Add New");
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        inspection.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);
        bt_gateout = (Button)findViewById(R.id.bt_gateout);
        bt_gateout.setVisibility(View.GONE);
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);


        list_noData = (TextView)findViewById(R.id.list_noData);
        list_noData.setVisibility(View.GONE);
        tv_cust_name = (TextView)findViewById(R.id.tv_cust_name);
        tv_cargo = (TextView)findViewById(R.id.tv_cargo);
        tv_equip_no = (TextView)findViewById(R.id.tv_equip_no);
        tv_type = (TextView)findViewById(R.id.tv_type);
        tv_cargo.setVisibility(View.GONE);
        tv_type.setVisibility(View.GONE);
        tv_equip_no.setVisibility(View.GONE);

        im_heat_close.setOnClickListener(this);
        im_heat_ok.setOnClickListener(this);
        heat_refresh.setOnClickListener(this);
        heat_home.setOnClickListener(this);
        leakTest_text.setOnClickListener(this);
        Leaktest.setOnClickListener(this);
        tv_search_options = (TextView) findViewById(R.id.tv_search_options);

        LL_search_Value = (LinearLayout) findViewById(R.id.LL_search_Value);
//        tv_search_options.setVisibility(View.GONE);
        LL_search_Value.setVisibility(View.GONE);

        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Leak Test");
        sp_fields = (Spinner) findViewById(R.id.sp_heat_customer);
        sp_operator = (Spinner) findViewById(R.id.sp_heat_operator);

        searchView2.requestFocus();
        search_heat_list.setOnTouchListener(new View.OnTouchListener() {
            // Setting on Touch Listener for handling the touch inside ScrollView
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                // Disallow the touch request for parent scroll on touch of child view
                v.getParent().requestDisallowInterceptTouchEvent(true);
                return false;
            }
        });

        im_up.setVisibility(View.GONE);
        LL_hole.setVisibility(View.GONE);
        ed_text1.addTextChangedListener(editTextWatcher);



//        heating_list_view.setOnItemClickListener(this);

        im_down.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_hole.setVisibility(View.VISIBLE);
                im_down.setVisibility(View.GONE);
                im_up.setVisibility(View.VISIBLE);

                if(cd.isConnectingToInternet()) {
                    getEditText = "";
                    if (fieldItems.equalsIgnoreCase("Customer") ||fieldItems.equalsIgnoreCase("CSTMR_CD")  ) {
                        new Create_GateIn_Customer_details().execute();
                    }else {
                        new Get_LeakTest_filter().execute();
                        new Get_Leaktest_details().execute();
                    }
                }else
                {
                    shortToast(getApplicationContext(),"Please check Your Internet Connection");
                }
                new Get_Leaktest_details().execute();
            }
        });
        im_up.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_hole.setVisibility(View.GONE);
                im_down.setVisibility(View.VISIBLE);
                im_up.setVisibility(View.GONE);
            }
        });

        if (cd.isConnectingToInternet()) {
            new Get_Leaktest_details().execute();
        } else {
            shortToast(getApplicationContext(), "Please Check your Internet Connection.");
        }

        drawer = (DrawerLayout) findViewById(R.id.drawer_layout);

        ArrayAdapter<String> FieldsAdapter = new ArrayAdapter<>(getApplicationContext(), R.layout.spinner_text,Fields);
        FieldsAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        sp_fields.setAdapter(FieldsAdapter);


        ArrayAdapter<String> OperatorAdapter = new ArrayAdapter<>(getApplicationContext(), R.layout.spinner_text,Operators);
        OperatorAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        sp_operator.setAdapter(OperatorAdapter);

        sp_fields.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                fieldItems = sp_fields.getSelectedItem().toString();
                Log.i("Selected item : ", fieldItems);
                if (fieldItems.equalsIgnoreCase("Customer")) {
                    fieldItems = "CSTMR_CD";
                    tv_cust_name.setVisibility(View.VISIBLE);
                    tv_cargo.setVisibility(View.GONE);
                    tv_type.setVisibility(View.GONE);
                    tv_equip_no.setVisibility(View.GONE);
                    if(cd.isConnectingToInternet()) {
                        new Create_GateIn_Customer_details().execute();
                        LL_hole.setVisibility(View.GONE);
                    }else{
                        shortToast(getApplicationContext(),"Please Check your Internet Connection..!");
                    }
                } else if (fieldItems.equalsIgnoreCase("Equipment No")) {
                    fieldItems = "EQPMNT_NO";
                    tv_cust_name.setVisibility(View.GONE);
                    tv_type.setVisibility(View.GONE);
                    tv_equip_no.setVisibility(View.VISIBLE);
                    tv_cargo.setVisibility(View.GONE);
                    if(cd.isConnectingToInternet()) {
                        new Get_LeakTest_filter().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                } else if (fieldItems.equalsIgnoreCase("Type")) {
                    fieldItems = "EQPMNT_TYP_CD";
                    tv_cust_name.setVisibility(View.GONE);
                    tv_type.setVisibility(View.VISIBLE);
                    tv_equip_no.setVisibility(View.GONE);
                    tv_cargo.setVisibility(View.GONE);
                    if(cd.isConnectingToInternet()) {
                        new Get_LeakTest_filter().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                } else if (fieldItems.equalsIgnoreCase("Previous Cargo")) {
                    fieldItems = "PRDCT_DSCRPTN_VC";
                    tv_cust_name.setVisibility(View.GONE);
                    tv_type.setVisibility(View.GONE);
                    tv_equip_no.setVisibility(View.GONE);
                    tv_cargo.setVisibility(View.GONE);
                    if(cd.isConnectingToInternet()) {
                        new Get_LeakTest_filter().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }

                }

            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        sp_operator.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                opratorItems = sp_operator.getSelectedItem().toString();


                if (opratorItems.equalsIgnoreCase("Does Not Contain")) {
                    opratorItems = "";
                    if(cd.isConnectingToInternet()) {
                        new Get_LeakTest_filter().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                } else if (opratorItems.equalsIgnoreCase("Not Similar")) {
                    opratorItems = "";
                    if(cd.isConnectingToInternet()) {
                        new Get_LeakTest_filter().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                }else{
                    if(cd.isConnectingToInternet()) {
                        new Get_LeakTest_filter().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                }


            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

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

        navigationView.setNavigationItemSelectedListener(this);
    }


    private final TextWatcher editTextWatcher = new TextWatcher() {
        public void beforeTextChanged(CharSequence s, int start, int count, int after) {

        }

        public void onTextChanged(CharSequence s, int start, int before, int count) {

        }
        public void afterTextChanged(Editable s) {

            getEditText = ed_text1.getText().toString();
            no_data.setVisibility(View.GONE);
            LL_hole.setVisibility(View.VISIBLE);
            if (cd.isConnectingToInternet()) {
                new Get_LeakTest_filter().execute();
            } else {
                shortToast(getApplicationContext(), "Please check Your Internet Connection");
            }
        }
    };




    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.iv_changeOfStatus:
                GlobalConstants.equipment_no="";
                GlobalConstants.status="";
                GlobalConstants.status_id="";
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));
                break;
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(), MainActivity.class));
                break;
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.im_heat_close:
                LL_hole.setVisibility(View.GONE);
                im_down.setVisibility(View.VISIBLE);
                im_up.setVisibility(View.GONE);
                try {
                    GlobalConstants.selected_Stock_Cust_Id.removeAll( GlobalConstants.selected_Stock_Cust_Id);
                }catch (Exception e)
                {

                }
                finish();
                startActivity(getIntent());
                break;
            case R.id.tv_heating:
                startActivity(new Intent(getApplicationContext(),LeakTestCreate.class));
                break;
            case R.id.leakTest:
                startActivity(new Intent(getApplicationContext(),LeakTestCreate.class));
                break;
            case R.id.im_heat_ok:
                if(boxAdapter.getBox().size()==0) {
                    shortToast(getApplicationContext(), "Please Select atleast One Value..!");
                }else {
                    selected_name.clear();
                    for (Product p : boxAdapter.getBox()) {
                        if (p.box) {
                            if (p.box == true) {
                                String[] set = new String[2];
                                set[0] = p.name;

                                selected_name.add(set[0]);
                                GlobalConstants.selected_Stock_Cust_Id=selected_name;
                                LL_hole.setVisibility(View.GONE);
                                im_down.setVisibility(View.VISIBLE);
                                im_up.setVisibility(View.GONE);

                            /*for(int i=0;i<selected_name.size();i++) {
                                tv_search_options.append(selected_name.get(i)+", ");
                            }
                                LL_search_Value.setVisibility(View.VISIBLE);*/


                                //shortToast(getApplicationContext(),p.name);

                                if (cd.isConnectingToInternet()) {
                                    new Get_Leaktest_SearchList_details().execute();
                                } else {
                                    shortToast(getApplicationContext(), "Please check Your Internet Connection");
                                }
                            } else {
                                shortToast(getApplicationContext(), "Please Select Customer Name");
                            }
                        }
                    }
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
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

    }

    public class Get_Leaktest_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(LeakTest.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
//            if ((progressDialog != null) && progressDialog.isShowing()) {
            progressDialog.show();
//            }
        }

        @Override
        protected Void doInBackground(Void... params) {

            ServiceHandler sh = new ServiceHandler();
            HttpParams httpParameters = new BasicHttpParams();
            DefaultHttpClient httpClient = new DefaultHttpClient(httpParameters);
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLLeakTestList);

            httpPost.setHeader("Content-Type", "application/json");


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


                jsonarray = getJsonObject.getJSONArray("arrayOfLTM");
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

                        leakTest_arraylist = new ArrayList<LeakTestBean>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            leakTest_bean = new LeakTestBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            leakTest_bean.setLK_TST_ID(jsonObject.getString("LK_TST_ID"));
                            leakTest_bean.setEQPMNT_NO(jsonObject.getString("EQPMNT_NO"));
                            leakTest_bean.setGI_TRNSCTN_NO(jsonObject.getString("GI_TRNSCTN_NO"));
                            leakTest_bean.setESTMT_NO(jsonObject.getString("ESTMT_NO"));
                            leakTest_bean.setTST_DT(jsonObject.getString("TST_DT"));
                            leakTest_bean.setRLF_VLV_SRL_1(jsonObject.getString("RLF_VLV_SRL_1"));
                            leakTest_bean.setRLF_VLV_SRL_2(jsonObject.getString("RLF_VLV_SRL_2"));
                            leakTest_bean.setPG_1(jsonObject.getString("PG_1"));
                            leakTest_bean.setPG_2(jsonObject.getString("PG_2"));
                            leakTest_bean.setRVSN_NO(jsonObject.getString("RVSN_NO"));
                            leakTest_bean.setLST_GNRTD_BY(jsonObject.getString("LST_GNRTD_BY"));
                            leakTest_bean.setLST_GNRTD_DT(jsonObject.getString("LST_GNRTD_DT"));
                            leakTest_bean.setLTST_RPRT_NO(jsonObject.getString("LTST_RPRT_NO"));
                            leakTest_bean.setNO_OF_TMS_GNRTD(jsonObject.getString("NO_OF_TMS_GNRTD"));
                            leakTest_bean.setSHLL_TST_BT(jsonObject.getString("SHLL_TST_BT"));
                            leakTest_bean.setSTM_TB_TST_BT(jsonObject.getString("STM_TB_TST_BT"));
                            leakTest_bean.setRMRKS_VC(jsonObject.getString("RMRKS_VC"));
                            leakTest_bean.setACTV_BT(jsonObject.getString("ACTV_BT"));
                            leakTest_bean.setCRTD_BY(jsonObject.getString("CRTD_BY"));
                            leakTest_bean.setCRTD_DT(jsonObject.getString("CRTD_DT"));
                            leakTest_bean.setMDFD_BY(jsonObject.getString("MDFD_BY"));
                            leakTest_bean.setMDFD_DT(jsonObject.getString("MDFD_DT"));
                            leakTest_bean.setEQPMNT_TYP_ID(jsonObject.getString("EQPMNT_TYP_ID"));
                            leakTest_bean.setEQPMNT_TYP_CD(jsonObject.getString("EQPMNT_TYP_CD"));
                            leakTest_bean.setEQPMNT_CD_ID(jsonObject.getString("EQPMNT_CD_ID"));
                            leakTest_bean.setEQPMNT_CD_CD(jsonObject.getString("EQPMNT_CD_CD"));
                            leakTest_bean.setEQPMNT_STTS_ID(jsonObject.getString("EQPMNT_STTS_ID"));
                            leakTest_bean.setEQPMNT_STTS_CD(jsonObject.getString("EQPMNT_STTS_CD"));
                            leakTest_bean.setCHECKED(jsonObject.getString("CHECKED"));
                            leakTest_bean.setGTN_DT(jsonObject.getString("GTN_DT"));
                            leakTest_bean.setCSTMR_ID(jsonObject.getString("CSTMR_ID"));
                            leakTest_bean.setCSTMR_CD(jsonObject.getString("CSTMR_CD"));
                            leakTest_bean.setDPT_ID(jsonObject.getString("DPT_ID"));
                            leakTest_bean.setSHL_TST(jsonObject.getString("SHL_TST"));
                            leakTest_bean.setSTM_TB_TST(jsonObject.getString("STM_TB_TST"));
                            leakTest_bean.setYRD_LCTN(jsonObject.getString("YRD_LCTN"));


                            leakTest_arraylist.add(leakTest_bean);


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
            } catch (ClientProtocolException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }


            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {


//            if ((progressDialog != null) && progressDialog.isShowing()) {
            progressDialog.dismiss();
//            }
            if (leakTest_arraylist != null) {
                adapter = new UserListAdapter(LeakTest.this, R.layout.list_item_row, leakTest_arraylist);
                listview.setAdapter(adapter);


                searchView2.addTextChangedListener(new TextWatcher() {

                    @Override
                    public void afterTextChanged(Editable arg0) {
                        // TODO Auto-generated method stub
                        String text = searchView2.getText().toString().toLowerCase(Locale.getDefault());
                        adapter.filter(text);
                    }

                    @Override
                    public void beforeTextChanged(CharSequence arg0, int arg1,
                                                  int arg2, int arg3) {
                        // TODO Auto-generated method stub
                    }

                    @Override
                    public void onTextChanged(CharSequence arg0, int arg1, int arg2,
                                              int arg3) {
                        // TODO Auto-generated method stub
                    }
                });


            } else if (leakTest_arraylist.size() < 1) {
                shortToast(getApplicationContext(), "Data Not Found");
            }

        }

    }

    public class UserListAdapter extends BaseAdapter {
        private final ArrayList<LeakTestBean> arraylist;
        Context context;
        ArrayList<LeakTestBean> list = new ArrayList<>();
        int resource;
        private LeakTestBean userListBean;
        int lastPosition = -1;

        public UserListAdapter(Context context, int resource, ArrayList<LeakTestBean> list) {
            this.context = context;
            this.list = list;
            this.resource = resource;
            this.arraylist = new ArrayList<LeakTestBean>();
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
                holder = new LeakTest.ViewHolder();



                holder.whole = (LinearLayout) convertView.findViewById(R.id.LL_whole);
                holder.Cust_Name = (TextView) convertView.findViewById(R.id.text1);
                holder.equip_no = (TextView) convertView.findViewById(R.id.text2);
                holder.date = (TextView) convertView.findViewById(R.id.text3);
                holder.previous_cargo = (TextView) convertView.findViewById(R.id.text4);
                holder.previous_cargo.setVisibility(View.GONE);
                holder.leaktestID = (TextView) convertView.findViewById(R.id.tv_text30);
                holder.transactionNO = (TextView) convertView.findViewById(R.id.text5);
                holder.type = (TextView) convertView.findViewById(R.id.tv_type);
                holder.testDate = (TextView) convertView.findViewById(R.id.text6);
                holder.RLFVLV1 = (TextView) convertView.findViewById(R.id.tv_code);
                holder.RLFVLV2 = (TextView) convertView.findViewById(R.id.tv_location);
                holder.PG1 = (TextView) convertView.findViewById(R.id.tv_transport);
                holder.PG2 = (TextView) convertView.findViewById(R.id.tv_vechicle);
                holder.estimateNo = (TextView) convertView.findViewById(R.id.text7);
                holder.revisionNO = (TextView) convertView.findViewById(R.id.text8);
                holder.listGrantedBy = (TextView) convertView.findViewById(R.id.tv_pre_adv_id);
                holder.listGrantedDate = (TextView) convertView.findViewById(R.id.text10);
                holder.listReportNo = (TextView) convertView.findViewById(R.id.tv_status);
                holder.noofTimesGranted = (TextView) convertView.findViewById(R.id.text9);
                holder.testBit = (TextView) convertView.findViewById(R.id.tv_text11);
                holder.STM_TB_TST_BT = (TextView) convertView.findViewById(R.id.text12);
                holder.RMRKS_VC = (TextView) convertView.findViewById(R.id.tv_text13);
                holder.ACTV_BT = (TextView) convertView.findViewById(R.id.tv_text14);
                holder.CRTD_BY = (TextView) convertView.findViewById(R.id.tv_text15);
                holder.CRTD_DT = (TextView) convertView.findViewById(R.id.tv_text16);
                holder.MDFD_BY = (TextView) convertView.findViewById(R.id.tv_text17);
                holder.MDFD_DT = (TextView) convertView.findViewById(R.id.tv_text18);
                holder.EQPMNT_TYP_ID = (TextView) convertView.findViewById(R.id.tv_text19);
                holder.EQPMNT_CD_ID = (TextView) convertView.findViewById(R.id.tv_text20);
                holder.EQPMNT_CD_CD = (TextView) convertView.findViewById(R.id.tv_text29);
                holder.EQPMNT_STTS_ID = (TextView) convertView.findViewById(R.id.tv_text21);
                holder.EQPMNT_STTS_CD = (TextView) convertView.findViewById(R.id.tv_text22);
                holder.CHECKED = (TextView) convertView.findViewById(R.id.tv_text23);
                holder.CSTMR_ID = (TextView) convertView.findViewById(R.id.tv_text24);
                holder.DPT_ID = (TextView) convertView.findViewById(R.id.tv_text25);
                holder.SHL_TST = (TextView) convertView.findViewById(R.id.tv_text26);
                holder.STM_TB_TST = (TextView) convertView.findViewById(R.id.tv_text27);
                holder.YRD_LCTN = (TextView) convertView.findViewById(R.id.tv_text28);

                holder.LL_username = (LinearLayout)convertView.findViewById(R.id.LL_username);
                holder.LL_username.setVisibility(View.GONE);

                convertView.setTag(holder);
            } else {
                holder = (LeakTest.ViewHolder) convertView.getTag();
            }
            if (list.size() < 1) {
                Toast.makeText(getApplicationContext(), "NO DATA FOUND", Toast.LENGTH_LONG).show();
            } else {
                userListBean = list.get(position);
//                String[] parts = userListBean.getGTN_DT().split(" ");
//                String part1_date = parts[0];
//                String part1_time = parts[1];
//                System.out.println("from date after split" + part1_date);
                holder.equip_no.setText(userListBean.getEQPMNT_NO() + "," + userListBean.getEQPMNT_TYP_CD());
                holder.Cust_Name.setText(userListBean.getCSTMR_CD());
                holder.date.setText(userListBean.getGTN_DT());


                holder.leaktestID.setText(userListBean.getLK_TST_ID());
                holder.transactionNO.setText(userListBean.getGI_TRNSCTN_NO());
                holder.estimateNo.setText(userListBean.getESTMT_NO());
                holder.testDate.setText(userListBean.getTST_DT());
                holder.RLFVLV1.setText(userListBean.getRLF_VLV_SRL_1());
                holder.RLFVLV2.setText(userListBean.getRLF_VLV_SRL_2());
                holder.PG1.setText(userListBean.getPG_1());
                holder.PG2.setText(userListBean.getPG_2());
                holder.revisionNO.setText(userListBean.getRVSN_NO());
                holder.listGrantedBy.setText(userListBean.getLST_GNRTD_BY());
                holder.listGrantedDate.setText(userListBean.getLST_GNRTD_DT());
                holder.listReportNo.setText(userListBean.getLTST_RPRT_NO());
                holder.noofTimesGranted.setText(userListBean.getNO_OF_TMS_GNRTD());
                holder.testBit.setText(userListBean.getSHLL_TST_BT());
                holder.STM_TB_TST_BT.setText(userListBean.getSTM_TB_TST_BT());
                holder.RMRKS_VC.setText(userListBean.getRMRKS_VC());
                holder.ACTV_BT.setText(userListBean.getACTV_BT());
                holder.CRTD_BY.setText(userListBean.getCRTD_BY());
                holder.CRTD_DT.setText(userListBean.getCRTD_DT());
                holder.MDFD_BY.setText(userListBean.getMDFD_BY());
                holder.MDFD_DT.setText(userListBean.getMDFD_DT());
                holder.EQPMNT_TYP_ID.setText(userListBean.getEQPMNT_TYP_ID());
                holder.EQPMNT_CD_ID.setText(userListBean.getEQPMNT_CD_ID());
                holder.EQPMNT_CD_CD.setText(userListBean.getEQPMNT_CD_CD());
                holder.EQPMNT_STTS_ID.setText(userListBean.getEQPMNT_STTS_ID());
                holder.EQPMNT_STTS_CD.setText(userListBean.getEQPMNT_STTS_CD());
                holder.CHECKED.setText(userListBean.getCHECKED());
                holder.CSTMR_ID.setText(userListBean.getCSTMR_ID());
                holder.DPT_ID.setText(userListBean.getDPT_ID());
                holder.SHL_TST.setText(userListBean.getSHL_TST());
                holder.STM_TB_TST.setText(userListBean.getSTM_TB_TST());
                holder.YRD_LCTN.setText(userListBean.getYRD_LCTN());
/*
                if(userListBean.getVechicle().equals("")||userListBean.getVechicle()==null)
                {
                    holder.PG2.setText("");
                }
               else{
                    holder.PG2.setText(userListBean.getVechicle());
                }*/


                holder.whole.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                       Intent i = new Intent(getApplicationContext(), LeakTestUpdate.class);

                        GlobalConstants.leskTestID = list.get(position).getLK_TST_ID();
                        GlobalConstants.equipment_no = list.get(position).getEQPMNT_NO();
                        GlobalConstants.gt_transaction_no = list.get(position).getGI_TRNSCTN_NO();
                        GlobalConstants.estimate_no = list.get(position).getESTMT_NO();
                        GlobalConstants.TestDate = list.get(position).getTST_DT();
                        GlobalConstants.RLFVLV1 = list.get(position).getRLF_VLV_SRL_1();
                        GlobalConstants.RLFVLV2 = list.get(position).getRLF_VLV_SRL_2();
                        GlobalConstants.PG1 = list.get(position).getPG_1();
                        GlobalConstants.PG2 = list.get(position).getPG_2();
                        GlobalConstants.revisionNo = list.get(position).getRVSN_NO();
                        GlobalConstants.LastGrantedBy = list.get(position).getLST_GNRTD_BY();
                        GlobalConstants.LastGrantedDate = list.get(position).getLST_GNRTD_DT();
                        GlobalConstants.ListRptNo = list.get(position).getLTST_RPRT_NO();
                        GlobalConstants.no_of_tms_granted = list.get(position).getNO_OF_TMS_GNRTD();
                        GlobalConstants.SHLL_TST_BT = list.get(position).getSHLL_TST_BT();
                        GlobalConstants.STM_TB_TST_BT = list.get(position).getSTM_TB_TST_BT();
                        GlobalConstants.remark = list.get(position).getRMRKS_VC();
                        GlobalConstants.ACTV_BT = list.get(position).getACTV_BT();
                        GlobalConstants.CRTD_BY = list.get(position).getCRTD_BY();
                        GlobalConstants.CRTD_DT = list.get(position).getCRTD_DT();
                        GlobalConstants.MDFD_BY = list.get(position).getMDFD_BY();
                        GlobalConstants.MDFD_DT = list.get(position).getMDFD_DT();
                        GlobalConstants.EQPMNT_TYP_ID = list.get(position).getEQPMNT_TYP_ID();
                        GlobalConstants.type = list.get(position).getEQPMNT_TYP_CD();
                        GlobalConstants.EQPMNT_CD_ID = list.get(position).getEQPMNT_CD_ID();
                        GlobalConstants.EQPMNT_CD_CD = list.get(position).getEQPMNT_CD_CD();
                        GlobalConstants.EQPMNT_STTS_ID = list.get(position).getEQPMNT_STTS_ID();
                        GlobalConstants.EQPMNT_STTS_CD = list.get(position).getEQPMNT_STTS_CD();
                        GlobalConstants.CHECKED = list.get(position).getCHECKED();
                        GlobalConstants.date = list.get(position).getGTN_DT();
                        GlobalConstants.customer_Id = list.get(position).getCSTMR_ID();
                        GlobalConstants.customer_name = list.get(position).getCSTMR_CD();
                        GlobalConstants.DPT_ID = list.get(position).getDPT_ID();
                        GlobalConstants.SHL_TST = list.get(position).getSHL_TST();
                        GlobalConstants.STM_TB_TST = list.get(position).getSTM_TB_TST();
                        GlobalConstants.location = list.get(position).getYRD_LCTN();


                       startActivity(i);
                    }
                });


            }
            return convertView;
        }

        public void filter(String charText) {
            charText = charText.toLowerCase(Locale.getDefault());
            list.clear();
            if (charText.length() == 0) {
                list.addAll(arraylist);
                /*listview.setVisibility(View.VISIBLE);
                list_noData.setVisibility(View.GONE);*/
            } else {
                for (LeakTestBean wp : arraylist) {
                    if (wp.getCSTMR_CD().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getEQPMNT_NO().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getGTN_DT().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getEQPMNT_TYP_CD().toLowerCase(Locale.getDefault()).contains(charText)
                            ) {
                        list.add(wp);
                        /*listview.setVisibility(View.VISIBLE);*/
                    }else{
                       /* list_noData.setVisibility(View.VISIBLE);
                        listview.setVisibility(View.GONE);*/
                    }
                }
            }
            notifyDataSetChanged();
        }




    }

    static class ViewHolder {
        TextView equip_no, previous_cargo,date, Cust_Name, leaktestID, transactionNO, testDate, RLFVLV1, RLFVLV2, pre_id, testBit, cust_code, type_id, code_id,
                PG2, PG1, estimateNo, noofTimesGranted, listGrantedDate, revisionNO, listReportNo, listGrantedBy, type,CRTD_DT,
                CRTD_BY,ACTV_BT,RMRKS_VC,STM_TB_TST_BT,MDFD_BY,MDFD_DT,EQPMNT_TYP_ID,EQPMNT_CD_CD,EQPMNT_CD_ID,EQPMNT_STTS_ID,EQPMNT_STTS_CD,CHECKED
                ,CSTMR_ID,DPT_ID,SHL_TST,STM_TB_TST,YRD_LCTN;
        CheckBox checkBox;

        LinearLayout whole,LL_username;
    }

    public class Create_GateIn_Customer_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(LeakTest.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCreateGateInCustomer);
//            httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
//            httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));

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

                    System.out.println("Am HashMap list"+jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found.");
                            }
                        });
                    }else {

                        dropdown_customer_list = new ArrayList<>();


                       /* businessAccessDetailsBeanArrayList = new ArrayList<>();
                        for (int i = 0; i < jsonArray.length(); i++) {
                            businessAccessDetailsBean = new BusinessAccessDetailsBean();
                            JSONObject jsonObject = jsonArray.getJSONObject(i);
                            businessAccessDetailsBean.setBusinessCode(jsonObject.getString("BUSINESS CODE"));
                            businessAccessDetailsBean.setBusinessDescription(jsonObject.getString("BUSINESS DESC"));
                            businessAccessDetailsBeanArrayList.add(businessAccessDetailsBean);
                        }*/
                        worldlist = new ArrayList<String>();
                        products = new ArrayList<Product>();
                        CustomerDropdownArrayList=new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("Name"));
                            customer_DropdownBean.setCode(jsonObject.getString("Code"));
                            CustomerName = jsonObject.getString("Name");
                            CustomerCode = jsonObject.getString("Code");
                            String[] set1 = new String[2];
                            set1[0] = CustomerName;
                            set1[1] = CustomerCode;
                            dropdown_customer_list.add(set1);
                            Cust_name.add(set1[0]);
                            Cust_code.add(set1[1]);
                            CustomerDropdownArrayList.add(customer_DropdownBean);
                            worldlist.add(CustomerName);
                            products.add(new Product(jsonObject.getString("Name"),false));

                        }
                    }
                }else if(jsonarray.length()<1){
                    runOnUiThread(new Runnable(){

                        @Override
                        public void run(){
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(),"No Records Found.");


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


            if (jsonarray != null) {
            if(dropdown_customer_list!=null) {
                boxAdapter = new ListAdapter(LeakTest.this, products);
                search_heat_list.setAdapter(boxAdapter);

             /*   UserListAdapterDropdown adapter = new UserListAdapterDropdown(GateIn.this, R.layout.list_item_row_accordion, pending_accordion_arraylist);
                searchlist.setAdapter(adapter);*/

                searchView1.addTextChangedListener(new TextWatcher() {

                    @Override
                    public void afterTextChanged(Editable arg0) {
                        // TODO Auto-generated method stub
                        String text = searchView1.getText().toString().toLowerCase(Locale.getDefault());
                        boxAdapter.filter(text);
                    }

                    @Override
                    public void beforeTextChanged(CharSequence arg0, int arg1,
                                                  int arg2, int arg3) {
                        // TODO Auto-generated method stub
                    }

                    @Override
                    public void onTextChanged(CharSequence arg0, int arg1, int arg2,
                                              int arg3) {
                        // TODO Auto-generated method stub
                    }
                });

            }

            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");

            }

            progressDialog.dismiss();

        }

    }

    public class Get_LeakTest_filter extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(LeakTest.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLLeakTestFilter);
            httpPost.setHeader("Content-Type", "application/json");

            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("filterType", fieldItems);
                jsonObject.put("filterCondition", opratorItems);

                jsonObject.put("filterValue", getEditText);

                jsonObject.put("Mode", "");

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d(" rep", resp);
                Log.d(" req", jsonObject.toString());
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("ListGateInss");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list" + jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");

//                                shortToast(getApplicationContext(), "No Records Found");
                                products.clear();
                                no_data.setVisibility(View.VISIBLE);

                            }
                        });
                    } else {

                        heating_accordion_arraylist = new ArrayList<>();

                        products = new ArrayList<Product>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            heating_accordion_bean = new HeatingAccordionBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            heating_accordion_bean.setValues(jsonObject.getString("Values"));
                            products.add(new Product(jsonObject.getString("Values"), false));

                            // pending_accordion_arraylist.add(pending_accordion_bean);


                        }
                    }
                } else if (jsonarray.length() < 1) {
                    runOnUiThread(new Runnable() {

                        @Override
                        public void run() {
                            //update ui here
                            // display toast here

//                            shortToast(getApplicationContext(), "No Records Found.");
                            products.clear();
                            no_data.setVisibility(View.VISIBLE);


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


            if (heating_accordion_arraylist != null) {

                boxAdapter = new LeakTest.ListAdapter(LeakTest.this, products);
                search_heat_list.setAdapter(boxAdapter);
             /*   UserListAdapterDropdown adapter = new UserListAdapterDropdown(GateIn.this, R.layout.list_item_row_accordion, pending_accordion_arraylist);
                searchlist.setAdapter(adapter);*/


                searchView1.addTextChangedListener(new TextWatcher() {

                    @Override
                    public void afterTextChanged(Editable arg0) {
                        // TODO Auto-generated method stub
                        String text = searchView1.getText().toString().toLowerCase(Locale.getDefault());
                        boxAdapter.filter(text);
                    }

                    @Override
                    public void beforeTextChanged(CharSequence arg0, int arg1,
                                                  int arg2, int arg3) {
                        // TODO Auto-generated method stub
                    }

                    @Override
                    public void onTextChanged(CharSequence arg0, int arg1, int arg2,
                                              int arg3) {
                        // TODO Auto-generated method stub
                    }
                });


            } else if (heating_accordion_arraylist.size() < 1) {
                shortToast(getApplicationContext(), "Data Not Found");
                LL_hole.setVisibility(View.GONE);
                no_data.setVisibility(View.VISIBLE);
                search_heat_list.setVisibility(View.GONE);


            }

            progressDialog.dismiss();

        }

    }

    public class ListAdapter extends BaseAdapter {
        private final ArrayList<Product> arraylist;
        Context ctx;
        LayoutInflater lInflater;
        ArrayList<Product> objects;
        Product p;

        ListAdapter(Context context, ArrayList<Product> products) {
            ctx = context;
            objects = products;
            this.arraylist = new ArrayList<Product>();
            this.arraylist.addAll(objects);
            lInflater = (LayoutInflater) ctx
                    .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        }

        @Override
        public int getCount() {
            return objects.size();
        }

        @Override
        public Object getItem(int position) {
            return objects.get(position);
        }

        @Override
        public long getItemId(int position) {
            return position;
        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {
            View view = convertView;
            if (view == null) {
                view = lInflater.inflate(R.layout.list_item_row_accordion, parent, false);
            }

            p = getProduct(position);

            ((TextView) view.findViewById(R.id.tv_cust_name)).setText(p.name);


            CheckBox cbBuy = (CheckBox) view.findViewById(R.id.checkbox);
            cbBuy.setOnCheckedChangeListener(myCheckChangList);
            cbBuy.setTag(position);
            cbBuy.setChecked(p.box);




            if(GlobalConstants.selected_Stock_Cust_Id!=null) {
                for (int i = 0; i < GlobalConstants.selected_Stock_Cust_Id.size(); i++) {
                    if (p.name.equalsIgnoreCase(String.valueOf(GlobalConstants.selected_Stock_Cust_Id.get(i)))) {
                        cbBuy.setChecked(true);
                    }
                }
            }


            return view;
        }

        Product getProduct(int position) {
            return ((Product) getItem(position));
        }

        ArrayList<Product> getBox() {


            box = new ArrayList<Product>();
            box.clear();
            for (Product p : objects) {
                if (p.box)
                    box.add(p);
            }
            return box;
        }

        CompoundButton.OnCheckedChangeListener myCheckChangList = new CompoundButton.OnCheckedChangeListener() {
            public void onCheckedChanged(CompoundButton buttonView,
                                         boolean isChecked) {
                getProduct((Integer) buttonView.getTag()).box = isChecked;
            }
        };


        public void filter(String charText) {
            charText = charText.toLowerCase(Locale.getDefault());
            objects.clear();
            if (charText.length() == 0) {
                objects.addAll(arraylist);
            } else {
                for (Product wp : arraylist) {
                    if (wp.name.toLowerCase(Locale.getDefault())
                            .contains(charText)) {
                        objects.add(wp);
                    }
                }
            }
            notifyDataSetChanged();
        }



    }

    public class Get_Leaktest_SearchList_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray heatingjsonlist;
        private JSONObject heatingjsonObject;
        private JSONObject SearchValuesObject;
        private String preadviceObject;


        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(LeakTest.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLLeakTestSearchList);
            httpPost.setHeader("Content-Type", "application/json");

            try{
                JSONObject jsonObject = new JSONObject();

                heatingjsonlist = new JSONArray();
                SearchValuesObject=new JSONObject();



                for (int i = 0; i < selected_name.size(); i++) {
                    heatingjsonObject=new JSONObject();
                    heatingjsonObject.put("values", selected_name.get(i));
                    heatingjsonlist.put(heatingjsonObject);
                }

                SearchValuesObject.put("SearchValues",heatingjsonlist);




                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("filterType", fieldItems);
                jsonObject.put("Mode", "new");
                jsonObject.put("SearchValues", SearchValuesObject);

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                Log.d("request", jsonObject.toString());
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("arrayOfLTM");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list"+jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");

                                shortToast(getApplicationContext(), "No Records Found");
                                listview.setVisibility(View.GONE);

                            }
                        });
                    }else {

                        leakTest_arraylist = new ArrayList<>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            leakTest_bean = new LeakTestBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            leakTest_bean.setLK_TST_ID(jsonObject.getString("LK_TST_ID"));
                            leakTest_bean.setEQPMNT_NO(jsonObject.getString("EQPMNT_NO"));
                            leakTest_bean.setGI_TRNSCTN_NO(jsonObject.getString("GI_TRNSCTN_NO"));
                            leakTest_bean.setESTMT_NO(jsonObject.getString("ESTMT_NO"));
                            leakTest_bean.setTST_DT(jsonObject.getString("TST_DT"));
                            leakTest_bean.setRLF_VLV_SRL_1(jsonObject.getString("RLF_VLV_SRL_1"));
                            leakTest_bean.setRLF_VLV_SRL_2(jsonObject.getString("RLF_VLV_SRL_2"));
                            leakTest_bean.setPG_1(jsonObject.getString("PG_1"));
                            leakTest_bean.setPG_2(jsonObject.getString("PG_2"));
                            leakTest_bean.setRVSN_NO(jsonObject.getString("RVSN_NO"));
                            leakTest_bean.setLST_GNRTD_BY(jsonObject.getString("LST_GNRTD_BY"));
                            leakTest_bean.setLST_GNRTD_DT(jsonObject.getString("LST_GNRTD_DT"));
                            leakTest_bean.setLTST_RPRT_NO(jsonObject.getString("LTST_RPRT_NO"));
                            leakTest_bean.setNO_OF_TMS_GNRTD(jsonObject.getString("NO_OF_TMS_GNRTD"));
                            leakTest_bean.setSHLL_TST_BT(jsonObject.getString("SHLL_TST_BT"));
                            leakTest_bean.setSTM_TB_TST_BT(jsonObject.getString("STM_TB_TST_BT"));
                            leakTest_bean.setRMRKS_VC(jsonObject.getString("RMRKS_VC"));
                            leakTest_bean.setACTV_BT(jsonObject.getString("ACTV_BT"));
                            leakTest_bean.setCRTD_BY(jsonObject.getString("CRTD_BY"));
                            leakTest_bean.setCRTD_DT(jsonObject.getString("CRTD_DT"));
                            leakTest_bean.setMDFD_BY(jsonObject.getString("MDFD_BY"));
                            leakTest_bean.setMDFD_DT(jsonObject.getString("MDFD_DT"));
                            leakTest_bean.setEQPMNT_TYP_ID(jsonObject.getString("EQPMNT_TYP_ID"));
                            leakTest_bean.setEQPMNT_TYP_CD(jsonObject.getString("EQPMNT_TYP_CD"));
                            leakTest_bean.setEQPMNT_CD_ID(jsonObject.getString("EQPMNT_CD_ID"));
                            leakTest_bean.setEQPMNT_CD_CD(jsonObject.getString("EQPMNT_CD_CD"));
                            leakTest_bean.setEQPMNT_STTS_ID(jsonObject.getString("EQPMNT_STTS_ID"));
                            leakTest_bean.setEQPMNT_STTS_CD(jsonObject.getString("EQPMNT_STTS_CD"));
                            leakTest_bean.setCHECKED(jsonObject.getString("CHECKED"));
                            leakTest_bean.setGTN_DT(jsonObject.getString("GTN_DT"));
                            leakTest_bean.setCSTMR_ID(jsonObject.getString("CSTMR_ID"));
                            leakTest_bean.setCSTMR_CD(jsonObject.getString("CSTMR_CD"));
                            leakTest_bean.setDPT_ID(jsonObject.getString("DPT_ID"));
                            leakTest_bean.setSHL_TST(jsonObject.getString("SHL_TST"));
                            leakTest_bean.setSTM_TB_TST(jsonObject.getString("STM_TB_TST"));
                            leakTest_bean.setYRD_LCTN(jsonObject.getString("YRD_LCTN"));
                            leakTest_arraylist.add(leakTest_bean);
                            runOnUiThread(new Runnable(){

                                @Override
                                public void run(){
                                    //update ui here
                                    // display toast here
                                    listview.setVisibility(View.VISIBLE);

                                }
                            });

                        }
                    }
                }else {
                    runOnUiThread(new Runnable(){

                        @Override
                        public void run(){
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(), "Data Not Found");
                            listview.setVisibility(View.GONE);



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


            if (jsonarray != null) {
                if (leakTest_arraylist != null) {
                    adapter = new LeakTest.UserListAdapter(LeakTest.this, R.layout.list_item_row, leakTest_arraylist);
                    listview.setAdapter(adapter);

                } else  {
                    shortToast(getApplicationContext(), "Data Not Found");
                    listview.setVisibility(View.GONE);

                }
            }else
            {
                shortToast(getApplicationContext(), "Data Not Found");
                listview.setVisibility(View.GONE);

            }
            progressDialog.dismiss();

        }

    }
    @Override
    public void onPause() {
        super.onPause();

        if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();
        progressDialog = null;
    }


}
