package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.media.Image;
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

import android.widget.Filter;

import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.CustomerDropdownBean;
import com.i_tankdepo.Beanclass.HeatingAccordionBean;
import com.i_tankdepo.Beanclass.HeatingBean;
import com.i_tankdepo.Beanclass.PendingAccordionBean;
import com.i_tankdepo.Beanclass.PendingBean;
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
import java.util.List;
import java.util.Locale;

import static com.i_tankdepo.Constants.GlobalConstants.equipment_no;
import static com.i_tankdepo.R.layout.heating;

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
public class Heating extends CommonActivity implements NavigationView.OnNavigationItemSelectedListener {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu, im_down, im_up, im_heat_close, im_heat_ok, iv_back;
    private ListView heating_list_view;

    private String[] Fields = {"Customer", "Equipment No", "Type", "Previous Cargo"};
    private String[] Operators = {"Contains", "Does Not Contain", "Equals", "Not Similar", "Similar"};


    private TextView tv_toolbarTitle, tv_search_options,no_data,list_noData,tv_type,tv_equip_no,tv_cargo,tv_cust_name;
    LinearLayout LL_hole, LL_heat_submit,LL_search_Value,LL_heat;

    private Spinner sp_fields, sp_operator;
    private String fieldItems, opratorItems;
    private ProgressDialog progressDialog;
    private ArrayList<HeatingBean> heating_arraylist = new ArrayList<>();
    private HeatingBean heating_bean;
    private ViewHolder holder;
    private ArrayList<HeatingAccordionBean> heating_accordion_arraylist = new ArrayList<>();
    private HeatingAccordionBean heating_accordion_bean;
    ArrayList<Product> products ;
    private ArrayList<Product> box;
    private Heating.ListAdapter boxAdapter;
    private UserListAdapter adapter;
    private ListView listview, search_heat_list;
    List<String> selected_name = new ArrayList<String>();

    private Intent mServiceIntent;

    private EditText ed_text1, searchView1, searchView2;
    private Button heat_refresh, heat_home, heat_submit,heating,cleaning,inspection,leakTest,bt_gateout;
    private String getEditText;
    private RelativeLayout RL_heating,RL_Repair;
    private ImageView iv_changeOfStatus;
    List<String> Cust_name = new ArrayList<>();
    List<String> Cust_code = new ArrayList<>();
    private ArrayList<String[]> dropdown_customer_list = new ArrayList<>();
    private ArrayList<String> worldlist;
    private ArrayList<CustomerDropdownBean> CustomerDropdownArrayList;
    private CustomerDropdownBean customer_DropdownBean;
    private String CustomerName,CustomerCode;
    private TextView tv_heating;
    private String HTNG_STRT_DT,HTNG_END_DT;
    private String Date_in;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.heating);
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
        LL_heat.setAlpha(0.5f);
        LL_heat.setClickable(false);
        tv_heating=(TextView)findViewById(R.id.tv_heating);
        tv_heating.setText("Heating");
        bt_gateout = (Button)findViewById(R.id.bt_gateout);
        bt_gateout.setVisibility(View.GONE);
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
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);

        heating = (Button)findViewById(R.id.heating);
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        leakTest = (Button)findViewById(R.id.leakTest);
        inspection.setVisibility(View.GONE);
        leakTest.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);
        list_noData = (TextView)findViewById(R.id.list_noData);
        list_noData.setVisibility(View.GONE);


        im_heat_close.setOnClickListener(this);
        im_heat_ok.setOnClickListener(this);
        heat_refresh.setOnClickListener(this);
        heat_home.setOnClickListener(this);
        tv_search_options = (TextView) findViewById(R.id.tv_search_options);

        LL_search_Value = (LinearLayout) findViewById(R.id.LL_search_Value);
//        tv_search_options.setVisibility(View.GONE);
        LL_search_Value.setVisibility(View.GONE);

        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Heating");
        sp_fields = (Spinner) findViewById(R.id.sp_heat_customer);
        sp_operator = (Spinner) findViewById(R.id.sp_heat_operator);

        tv_cust_name = (TextView)findViewById(R.id.tv_cust_name);
        tv_cargo = (TextView)findViewById(R.id.tv_cargo);
        tv_equip_no = (TextView)findViewById(R.id.tv_equip_no);
        tv_type = (TextView)findViewById(R.id.tv_type);
        tv_cargo.setVisibility(View.GONE);
        tv_type.setVisibility(View.GONE);
        tv_equip_no.setVisibility(View.GONE);


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
                        new Get_Heating_filter().execute();
                    }
                }else
                {
                    shortToast(getApplicationContext(),"Please check Your Internet Connection");
                }
                new Get_Heating_details().execute();


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
            new Get_Heating_details().execute();
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
                        new Get_Heating_filter().execute();
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
                        new Get_Heating_filter().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                } else if (fieldItems.equalsIgnoreCase("Previous Cargo")) {
                    fieldItems = "PRDCT_DSCRPTN_VC";
                    tv_cust_name.setVisibility(View.GONE);
                    tv_type.setVisibility(View.GONE);
                    tv_equip_no.setVisibility(View.GONE);
                    tv_cargo.setVisibility(View.VISIBLE);
                    if(cd.isConnectingToInternet()) {
                        new Get_Heating_filter().execute();
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
                        new Get_Heating_filter().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                } else if (opratorItems.equalsIgnoreCase("Not Similar")) {
                    opratorItems = "";
                    if(cd.isConnectingToInternet()) {
                        new Get_Heating_filter().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                }else{
                    if(cd.isConnectingToInternet()) {
                        new Get_Heating_filter().execute();
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
                new Get_Heating_filter().execute();
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

                }                finish();
                startActivity(getIntent());
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
                                    new Get_Heating_SearchList_details().execute();
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

    public class Get_Heating_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Heating.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLHeatingList);

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


                jsonarray = getJsonObject.getJSONArray("HeatingArray");
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

                        heating_arraylist = new ArrayList<HeatingBean>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            heating_bean = new HeatingBean();
                            jsonObject = jsonarray.getJSONObject(i);
                            SimpleDateFormat fromUser = new SimpleDateFormat("MM-dd-yyyy", Locale.ENGLISH);
                            SimpleDateFormat myFormat = new SimpleDateFormat("dd-MM-yyyy",Locale.ENGLISH);

                            HTNG_STRT_DT=jsonObject.getString("HTNG_STRT_DT");
                            HTNG_END_DT=jsonObject.getString("HTNG_END_DT");
                            String[] In_date=HTNG_STRT_DT.split(" ");
                            String[] split_LastStatusDate=HTNG_END_DT.split(" ");
                            HTNG_STRT_DT=In_date[0];
                            HTNG_END_DT=split_LastStatusDate[0];
                            try {
                                if (HTNG_STRT_DT.equals(null) || HTNG_STRT_DT.length() < 0) {

                                    HTNG_STRT_DT = "";
                                } else {

                                    HTNG_STRT_DT = myFormat.format(fromUser.parse(HTNG_STRT_DT));




                                } if (HTNG_END_DT.equals(null) || HTNG_END_DT.length() < 0) {

                                    HTNG_END_DT = "";
                                } else {

                                    HTNG_END_DT = myFormat.format(fromUser.parse(HTNG_END_DT));




                                }

                            }catch (Exception e)
                            {

                            }

                            heating_bean.setHTNG_STRT_DT(HTNG_STRT_DT);
                            heating_bean.setHTNG_END_DT(HTNG_END_DT);
                            heating_bean.setEQPMNT_NO(jsonObject.getString("EQPMNT_NO"));
                            heating_bean.setCSTMR_CD(jsonObject.getString("CSTMR_CD"));
                            heating_bean.setEQPMNT_TYP_CD(jsonObject.getString("EQPMNT_TYP_CD"));
                            heating_bean.setPRDCT_DSCRPTN_VC(jsonObject.getString("PRDCT_DSCRPTN_VC"));



                            Date_in=jsonObject.getString("GTN_DT");

                            String[] GTN_DT=Date_in.split(" ");
                            Date_in=GTN_DT[0];
                            try {
                                if (Date_in.equals(null) || Date_in.length() < 0) {

                                    Date_in = "";
                                } else {

                                    Date_in = myFormat.format(fromUser.parse(Date_in));

                                }
                            }catch (Exception e)
                            {

                            }
                            heating_bean.setGTN_DT(Date_in);
//                            repair_bean.setInDate(Date_in);

                            heating_bean.setYRD_LCTN(jsonObject.getString("YRD_LCTN"));
                            heating_bean.setGI_TRNSCTN_NO(jsonObject.getString("GI_TRNSCTN_NO"));
                            heating_bean.setHTNG_STRT_TM(jsonObject.getString("HTNG_STRT_TM"));
                            heating_bean.setHTNG_END_TM(jsonObject.getString("HTNG_END_TM"));
                            heating_bean.setHTNG_TMPRTR(jsonObject.getString("HTNG_TMPRTR"));
                            heating_bean.setTTL_HTN_PRD(jsonObject.getString("TTL_HTN_PRD"));
                            heating_bean.setMIN_HTNG_RT_NC(jsonObject.getString("MIN_HTNG_RT_NC"));
                            heating_bean.setHRLY_CHRG_NC(jsonObject.getString("HRLY_CHRG_NC"));
                            heating_bean.setCSTMR_CRRNCY_CD(jsonObject.getString("CSTMR_CRRNCY_CD"));
                            heating_bean.setMIN_HTNG_PRD_NC(jsonObject.getString("MIN_HTNG_PRD_NC"));
                            heating_bean.setTTL_RT_NC(jsonObject.getString("TTL_RT_NC"));

                            heating_arraylist.add(heating_bean);


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
            if (heating_arraylist != null) {
                adapter = new UserListAdapter(Heating.this, R.layout.list_item_row, heating_arraylist);
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


            } else if (heating_arraylist.size() < 1) {
                shortToast(getApplicationContext(), "Data Not Found");
            }

        }

    }

    public class UserListAdapter extends BaseAdapter {
        private final ArrayList<HeatingBean> arraylist;
        Context context;
        ArrayList<HeatingBean> list = new ArrayList<>();
        int resource;
        private HeatingBean userListBean;
        int lastPosition = -1;

        public UserListAdapter(Context context, int resource, ArrayList<HeatingBean> list) {
            this.context = context;
            this.list = list;
            this.resource = resource;
            this.arraylist = new ArrayList<HeatingBean>();
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
                holder = new Heating.ViewHolder();



                holder.whole = (LinearLayout) convertView.findViewById(R.id.LL_whole);
                holder.Cust_Name = (TextView) convertView.findViewById(R.id.text1);
                holder.equip_no = (TextView) convertView.findViewById(R.id.text2);
                holder.date = (TextView) convertView.findViewById(R.id.text3);
                holder.previous_crg = (TextView) convertView.findViewById(R.id.text4);
                holder.transactionNO = (TextView) convertView.findViewById(R.id.text5);
                holder.type = (TextView) convertView.findViewById(R.id.tv_type);
                holder.location = (TextView) convertView.findViewById(R.id.text6);
                holder.startDate = (TextView) convertView.findViewById(R.id.tv_code);
                holder.startTime = (TextView) convertView.findViewById(R.id.tv_location);
                holder.endDate = (TextView) convertView.findViewById(R.id.tv_transport);
                holder.endTime = (TextView) convertView.findViewById(R.id.tv_vechicle);
                holder.temp = (TextView) convertView.findViewById(R.id.text7);
                holder.ttl_htngPrd = (TextView) convertView.findViewById(R.id.text8);
                holder.min_htngRate_NC = (TextView) convertView.findViewById(R.id.tv_pre_adv_id);
                holder.hourly_charge = (TextView) convertView.findViewById(R.id.text10);
                holder.cust_currency = (TextView) convertView.findViewById(R.id.tv_status);
                holder.min_htngPRD_NC = (TextView) convertView.findViewById(R.id.text9);
                holder.TTL_RT_NC = (TextView) convertView.findViewById(R.id.tv_pre_code);
                holder.LL_username = (LinearLayout)convertView.findViewById(R.id.LL_username);
                holder.LL_username.setVisibility(View.GONE);

                convertView.setTag(holder);
            } else {
                holder = (Heating.ViewHolder) convertView.getTag();
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
                holder.previous_crg.setText(userListBean.getPRDCT_DSCRPTN_VC());
                holder.transactionNO.setText(userListBean.getGI_TRNSCTN_NO());
                holder.temp.setText(userListBean.getHTNG_TMPRTR());
                holder.location.setText(userListBean.getYRD_LCTN());
                holder.startDate.setText(userListBean.getHTNG_STRT_DT());
                holder.startTime.setText(userListBean.getHTNG_STRT_TM());
                holder.endDate.setText(userListBean.getHTNG_END_DT());
                holder.endTime.setText(userListBean.getHTNG_END_TM());
                holder.ttl_htngPrd.setText(userListBean.getTTL_HTN_PRD());
                holder.min_htngRate_NC.setText(userListBean.getMIN_HTNG_RT_NC());
                holder.hourly_charge.setText(userListBean.getHRLY_CHRG_NC());
                holder.cust_currency.setText(userListBean.getCSTMR_CRRNCY_CD());
                holder.min_htngPRD_NC.setText(userListBean.getMIN_HTNG_PRD_NC());
                holder.TTL_RT_NC.setText(userListBean.getTTL_RT_NC());
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

                        Log.i("heat_startDate",list.get(position).getHTNG_STRT_DT());
                        Log.i("heat_endDate",list.get(position).getHTNG_END_DT());
                        Log.i("heat_inDate",list.get(position).getGTN_DT());
                        Intent i = new Intent(getApplicationContext(), HeatingPeriod.class);
                        GlobalConstants.equipment_no = list.get(position).getEQPMNT_NO();
                        GlobalConstants.customer_name = list.get(position).getCSTMR_CD();
                        GlobalConstants.date = list.get(position).getGTN_DT();
                        GlobalConstants.type = list.get(position).getEQPMNT_TYP_CD();
                        GlobalConstants.previous_cargo = list.get(position).getPRDCT_DSCRPTN_VC();
                        GlobalConstants.location = list.get(position).getYRD_LCTN();
                        GlobalConstants.gt_transaction_no = list.get(position).getGI_TRNSCTN_NO();
                        GlobalConstants.htng_startDate = list.get(position).getHTNG_STRT_DT();
                        GlobalConstants.htng_startTime = list.get(position).getHTNG_STRT_TM();
                        GlobalConstants.htng_EndDate = list.get(position).getHTNG_END_DT();
                        GlobalConstants.htng_EndTime = list.get(position).getHTNG_END_TM();
                        GlobalConstants.htng_temp = list.get(position).getHTNG_TMPRTR();
                        GlobalConstants.ttl_htngPrd = list.get(position).getTTL_HTN_PRD();
                        GlobalConstants.min_htng_rate = list.get(position).getMIN_HTNG_RT_NC();
                        GlobalConstants.hourly_charge = list.get(position).getHRLY_CHRG_NC();
                        GlobalConstants.cust_currency = list.get(position).getCSTMR_CRRNCY_CD();
                        GlobalConstants.min_htngPrd = list.get(position).getMIN_HTNG_PRD_NC();
                        GlobalConstants.ttl_RT_NC = list.get(position).getMIN_HTNG_RT_NC();
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
                for (HeatingBean wp : arraylist) {
                    if (wp.getCSTMR_CD().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getEQPMNT_NO().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getPRDCT_DSCRPTN_VC().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getGTN_DT().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getEQPMNT_TYP_CD().toLowerCase(Locale.getDefault()).contains(charText)
                            ) {
                        list.add(wp);
                       /* listview.setVisibility(View.VISIBLE);*/
                    }/*else{
                        list_noData.setVisibility(View.VISIBLE);
                        listview.setVisibility(View.GONE);
                    }*/
                }
            }
            notifyDataSetChanged();
        }




    }

    static class ViewHolder {
        TextView equip_no, date, Cust_Name, previous_crg, transactionNO, location, startDate, startTime, pre_id, TTL_RT_NC, cust_code, type_id, code_id,
                endTime, endDate, temp, min_htngPRD_NC, hourly_charge, ttl_htngPrd, cust_currency, min_htngRate_NC, type;
        CheckBox checkBox;

        LinearLayout whole,LL_username;
    }

    public class Create_GateIn_Customer_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Heating.this);
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
        protected void onPostExecute (Void aVoid) {


            if (jsonarray != null) {
            if (dropdown_customer_list != null) {
                boxAdapter = new ListAdapter(Heating.this, products);
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

    public class Get_Heating_filter extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Heating.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLHeatingFilter);
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

                boxAdapter = new ListAdapter(Heating.this, products);
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

    public class Get_Heating_SearchList_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray heatingjsonlist;
        private JSONObject heatingjsonObject;
        private JSONObject SearchValuesObject;
        private String preadviceObject;


        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Heating.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLHeatingSearchList);
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


                jsonarray = getJsonObject.getJSONArray("HeatingArray");
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

                        heating_arraylist = new ArrayList<>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            heating_bean = new HeatingBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            heating_bean.setEQPMNT_NO(jsonObject.getString("EQPMNT_NO"));
                            heating_bean.setCSTMR_CD(jsonObject.getString("CSTMR_CD"));
                            heating_bean.setEQPMNT_TYP_CD(jsonObject.getString("EQPMNT_TYP_CD"));
                            heating_bean.setPRDCT_DSCRPTN_VC(jsonObject.getString("PRDCT_DSCRPTN_VC"));
                            SimpleDateFormat fromUser = new SimpleDateFormat("MM-dd-yyyy", Locale.ENGLISH);
                            SimpleDateFormat myFormat = new SimpleDateFormat("dd-MM-yyyy",Locale.ENGLISH);
                            Date_in=jsonObject.getString("GTN_DT");

                            String[] GTN_DT=Date_in.split(" ");
                            Date_in=GTN_DT[0];
                            try {
                                if (Date_in.equals(null) || Date_in.length() < 0) {

                                    Date_in = "";
                                } else {

                                    Date_in = myFormat.format(fromUser.parse(Date_in));

                                }
                            }catch (Exception e)
                            {

                            }
                            heating_bean.setGTN_DT(Date_in);
                            heating_bean.setYRD_LCTN(jsonObject.getString("YRD_LCTN"));
                            heating_bean.setGI_TRNSCTN_NO(jsonObject.getString("GI_TRNSCTN_NO"));
                            heating_bean.setHTNG_STRT_DT(jsonObject.getString("HTNG_STRT_DT"));
                            heating_bean.setHTNG_STRT_TM(jsonObject.getString("HTNG_STRT_TM"));
                            heating_bean.setHTNG_END_DT(jsonObject.getString("HTNG_END_DT"));
                            heating_bean.setHTNG_END_TM(jsonObject.getString("HTNG_END_TM"));
                            heating_bean.setHTNG_TMPRTR(jsonObject.getString("HTNG_TMPRTR"));
                            heating_bean.setTTL_HTN_PRD(jsonObject.getString("TTL_HTN_PRD"));
                            heating_bean.setMIN_HTNG_RT_NC(jsonObject.getString("MIN_HTNG_RT_NC"));
                            heating_bean.setHRLY_CHRG_NC(jsonObject.getString("HRLY_CHRG_NC"));
                            heating_bean.setCSTMR_CRRNCY_CD(jsonObject.getString("CSTMR_CRRNCY_CD"));
                            heating_bean.setMIN_HTNG_PRD_NC(jsonObject.getString("MIN_HTNG_PRD_NC"));
                            heating_bean.setTTL_RT_NC(jsonObject.getString("TTL_RT_NC"));
                            heating_arraylist.add(heating_bean);

                        }
                    }
                }else {
                    runOnUiThread(new Runnable(){

                        @Override
                        public void run(){
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(),"No Records Found");
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
                if (heating_arraylist != null) {
                    adapter = new UserListAdapter(Heating.this, R.layout.list_item_row, heating_arraylist);
                    listview.setAdapter(adapter);

                } else
                {
                    shortToast(getApplicationContext(),"No Records Found");
                    listview.setVisibility(View.GONE);


                }
            }else
            {
                shortToast(getApplicationContext(),"No Records Found");
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
