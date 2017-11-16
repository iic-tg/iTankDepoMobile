package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
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

import android.widget.ScrollView;

import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.CustomerDropdownBean;
import com.i_tankdepo.Beanclass.PendingAccordionBean;
import com.i_tankdepo.Beanclass.InspectionBean;
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

import static com.i_tankdepo.R.layout.heating;


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

public class InspectionPending extends CommonActivity implements NavigationView.OnNavigationItemSelectedListener {

    private DrawerLayout drawer;
    private Toolbar toolbar;

    private ListView listview, searchlist;
    private RelativeLayout RL_musubmit, RL_pending,RL_heating,RL_Repair;
    private ImageView menu, im_up, im_down, im_ok, im_close;
    String equip_no, Cust_Name, previous_crg, attachmentstatus, gateIn_Id, code, location, Gate_In, cust_code, type_id, code_id, pre_code, pre_id,
            vechicle, transport, Eir_no, heating_bt, rental_bt, remark, type, status, date, time, pre_adv_id;
    LinearLayout LL_hole, LL_Submit, LL_footer_delete,LL_search_Value,LL_heat_submit,LL_heat,LL_username;
    Button bt_pending, bt_add, bt_mysubmit, bt_home, bt_refresh, im_add, im_print,cleaning,heating,inspection,leakTest,bt_gateout;
    private String[] Fields = {"Customer", "Equipment No", "Type", "Previous Cargo"};
    private String[] Operators = {"Contains", "Does Not Contain", "Equals", "Not Similar", "Similar"};
    ArrayList<String> selectedlist = new ArrayList<>();
    private TextView tv_type,tv_equip_no,tv_cargo,tv_cust_name,tv_toolbarTitle, tv_add, tv_search_options,no_data,cleaning_text,list_noData;
    private Intent mServiceIntent;
    private ArrayList<InspectionBean> inspection_arraylist = new ArrayList<>();
    private InspectionBean inspection_bean;
    private ArrayList<PendingAccordionBean> pending_accordion_arraylist = new ArrayList<>();
    private PendingAccordionBean pending_accordion_bean;
    private ViewHolder holder;

    private Spinner fieldSpinner, operatorSpinner;
    private String fieldItems, opratorItems;
    private EditText searchView2, searchView1, ed_text;

    private UserListAdapter adapter;
    ArrayList<Product> products;
    private ListAdapter boxAdapter;
    private ArrayList<Product> box;
    List<String> selected_name = new ArrayList<String>();
    private JSONObject filenamejson;
    private String filename;
    private ProgressDialog progressDialog;
    private String equipment_no;
    private String Lock_return_Message;
    private ImageView iv_back;
    private String getEditText;
    private ScrollView scrollbar;
    private ImageView iv_changeOfStatus;
    List<String> Cust_name = new ArrayList<>();
    List<String> Cust_code = new ArrayList<>();
    private ArrayList<String[]> dropdown_customer_list = new ArrayList<>();
    private ArrayList<String> worldlist;
    private ArrayList<CustomerDropdownBean> CustomerDropdownArrayList;
    private CustomerDropdownBean customer_DropdownBean;
    private String CustomerName,CustomerCode;
    private String LatestInspectionDate,OriginalInspectionDate,LatestCleaningDate,OriginalCleaningDate,Date_in;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.inspection);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setVisibility(View.GONE);
        im_down = (ImageView) findViewById(R.id.im_down);
        im_up = (ImageView) findViewById(R.id.im_up);
        listview = (ListView) findViewById(R.id.list_view);
        searchlist = (ListView) findViewById(R.id.search_list);
        searchView2 = (EditText) findViewById(R.id.searchView2);
        searchView1 = (EditText) findViewById(R.id.searchView1);
        ed_text = (EditText) findViewById(R.id.editText2);
        no_data = (TextView)findViewById(R.id.no_data);
        cleaning_text = (TextView)findViewById(R.id.tv_heating);
        heating = (Button)findViewById(R.id.heating);
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        leakTest = (Button)findViewById(R.id.leakTest);
        cleaning.setVisibility(View.GONE);
        heating.setVisibility(View.GONE);
        leakTest.setVisibility(View.GONE);
        cleaning_text.setText("Inspection");
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        bt_gateout = (Button)findViewById(R.id.bt_gateout);
        bt_gateout.setVisibility(View.GONE);


        no_data.setVisibility(View.GONE);
        bt_pending = (Button) findViewById(R.id.bt_pending);
        RL_musubmit = (RelativeLayout) findViewById(R.id.RL_mysubmit);

        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_Repair.setVisibility(View.GONE);

        LL_hole = (LinearLayout) findViewById(R.id.LL_hole);
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);
        LL_heat = (LinearLayout) findViewById(R.id.LL_heat);


        im_ok = (ImageView) findViewById(R.id.im_ok);
        im_close = (ImageView) findViewById(R.id.im_close);

        im_ok.setOnClickListener(this);
        im_close.setOnClickListener(this);

        LL_heat_submit.setAlpha(0.5f);
        LL_heat_submit.setClickable(false);

        LL_heat.setAlpha(0.5f);
        LL_heat.setClickable(false);
        list_noData = (TextView)findViewById(R.id.list_noData);
        list_noData.setVisibility(View.GONE);


        RL_pending = (RelativeLayout) findViewById(R.id.RL_pending);

        bt_mysubmit = (Button) findViewById(R.id.bt_mysubmit);


        bt_mysubmit.setOnClickListener(this);
        bt_home = (Button) findViewById(R.id.heat_home);
        bt_home.setOnClickListener(this);
        bt_refresh = (Button) findViewById(R.id.heat_refresh);
        bt_refresh.setOnClickListener(this);
        fieldSpinner = (Spinner) findViewById(R.id.sp_fields);
        operatorSpinner = (Spinner) findViewById(R.id.sp_operators);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_search_options = (TextView) findViewById(R.id.tv_search_options);
        LL_search_Value = (LinearLayout)findViewById(R.id.LL_search_Value);
        scrollbar = (ScrollView)findViewById(R.id.scrollbar);
        LL_search_Value.setVisibility(View.GONE);


        tv_toolbarTitle.setText("Inspection");
        tv_cust_name = (TextView)findViewById(R.id.tv_cust_name);
        tv_cargo = (TextView)findViewById(R.id.tv_cargo);
        tv_equip_no = (TextView)findViewById(R.id.tv_equip_no);
        tv_type = (TextView)findViewById(R.id.tv_type);
        tv_cargo.setVisibility(View.GONE);
        tv_type.setVisibility(View.GONE);
        tv_equip_no.setVisibility(View.GONE);


        RL_musubmit.setBackgroundColor(Color.parseColor("#ffffff"));

        searchView2.requestFocus();

        listview.setTextFilterEnabled(true);

        searchlist.setOnTouchListener(new View.OnTouchListener() {
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
//        im_down.setVisibility(View.GONE);

        ed_text.addTextChangedListener(editTextWatcher);
//        searchView2.addTextChangedListener(mTextEditorWatcher);

        if (cd.isConnectingToInternet()) {
            new Get_Cleaning_details().execute();
        } else {
            shortToast(getApplicationContext(), "Please check your Internet Connection.");
        }
        im_down.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_hole.setVisibility(View.VISIBLE);
                im_down.setVisibility(View.GONE);
                im_up.setVisibility(View.VISIBLE);

                if (cd.isConnectingToInternet()) {
                    getEditText = "";
                    if (fieldItems.equalsIgnoreCase("Customer") ||fieldItems.equalsIgnoreCase("CSTMR_CD")  ) {
                        new Create_GateIn_Customer_details().execute();
                    }else {
                        new Get_Inspection_Dropdown_details().execute();
                        new Get_Cleaning_details().execute();
                    }
                } else {
                    shortToast(getApplicationContext(), "Please check Your Internet Connection");
                }
                new Get_Cleaning_details().execute();
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




        drawer = (DrawerLayout) findViewById(R.id.drawer_layout);

        ArrayAdapter<String> FieldsAdapter = new ArrayAdapter<>(getApplicationContext(), R.layout.spinner_text, Fields);


        FieldsAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        fieldSpinner.setAdapter(FieldsAdapter);



        ArrayAdapter<String> OperatorAdapter = new ArrayAdapter<>(getApplicationContext(), R.layout.spinner_text, Operators);
        OperatorAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        operatorSpinner.setAdapter(OperatorAdapter);


        fieldSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                fieldItems = fieldSpinner.getSelectedItem().toString();
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
                       shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                   }

                } else if (fieldItems.equalsIgnoreCase("Equipment No")) {
                    fieldItems = "EQPMNT_NO";
                    tv_cust_name.setVisibility(View.GONE);
                    tv_type.setVisibility(View.GONE);
                    tv_equip_no.setVisibility(View.VISIBLE);
                    tv_cargo.setVisibility(View.GONE);
                    if(cd.isConnectingToInternet()){
                        new Get_Inspection_Dropdown_details().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                } else if (fieldItems.equalsIgnoreCase("Type")) {
                    fieldItems = "EQPMNT_TYP_CD";
                    tv_cust_name.setVisibility(View.GONE);
                    tv_type.setVisibility(View.VISIBLE);
                    tv_equip_no.setVisibility(View.GONE);
                    tv_cargo.setVisibility(View.GONE);
                    if(cd.isConnectingToInternet()){
                        new Get_Inspection_Dropdown_details().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                } else if (fieldItems.equalsIgnoreCase("Previous Cargo")) {
                    fieldItems = "PRDCT_DSCRPTN_VC";
                    tv_cust_name.setVisibility(View.GONE);
                    tv_type.setVisibility(View.GONE);
                    tv_equip_no.setVisibility(View.GONE);
                    tv_cargo.setVisibility(View.VISIBLE);
                    if(cd.isConnectingToInternet()){
                        new Get_Inspection_Dropdown_details().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
        operatorSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

                opratorItems = operatorSpinner.getSelectedItem().toString();

                if (opratorItems.equalsIgnoreCase("Does Not Contain")) {
                    opratorItems = "";
                    if(cd.isConnectingToInternet()){
                        new Get_Inspection_Dropdown_details().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                } else if (opratorItems.equalsIgnoreCase("Not Similar")) {
                    opratorItems = "";
                    if(cd.isConnectingToInternet()){
                        new Get_Inspection_Dropdown_details().execute();
                    }else{
                        shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                    }
                }else{
                    if(cd.isConnectingToInternet()){
                        new Get_Inspection_Dropdown_details().execute();
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

                if (drawer.isDrawerOpen(Gravity.START))

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

            getEditText = ed_text.getText().toString();
            no_data.setVisibility(View.GONE);
            LL_hole.setVisibility(View.VISIBLE);
            if (cd.isConnectingToInternet()) {
                new Get_Inspection_Dropdown_details().execute();
            }else if(getEditText.length()==0){

            }
            else {
                shortToast(getApplicationContext(), "Please check Your Internet Connection");
            }

        }
    };

    @Override
    public void onClick(View view) {
        switch (view.getId())
        {
            case R.id.iv_changeOfStatus:
                GlobalConstants.equipment_no="";
                GlobalConstants.status="INS";
                GlobalConstants.status_id="6";
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));
                break;
            case R.id.bt_mysubmit:
                finish();
                startActivity(new Intent(getApplicationContext(),InspectionMySubmit.class));
                break;
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(),MainActivity.class));
                break;
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.im_close:
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
            case R.id.im_ok:
                if(boxAdapter.getBox().size()==0) {
                    shortToast(getApplicationContext(), "Please Select atleast One Value..!");
                    GlobalConstants.selected_Stock_Cust_Id.removeAll( GlobalConstants.selected_Stock_Cust_Id);

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

                           /* for(int i=0;i<selected_name.size();i++) {
                                tv_search_options.append(selected_name.get(i)+", ");
                            }
                                LL_search_Value.setVisibility(View.VISIBLE);*/


                            //shortToast(getApplicationContext(),p.name);

                            if (cd.isConnectingToInternet()) {
                                new Get_Inspection_SearchList_details().execute();
                            } else {
                                shortToast(getApplicationContext(), "Please check Your Internet Connection");
                            }
                        } else {
                            shortToast(getApplicationContext(), "Please Select CustomerName");
                        }
                    }
                }
                }
                break;
        }

    }

    @Override
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

    }

    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem item) {
        // Handle navigation view item clicks here.
        //
        int id = item.getItemId();

        if (id == R.id.nav_home) {
            finish();
            startActivity(new Intent(getApplicationContext(),MainActivity.class));
            // Handle the camera action
        }else if (id == R.id.nav_changePwd) {
            startActivity(new Intent(getApplicationContext(),Change_Password.class));
        } else if (id == R.id.nav_Logout) {
            if(mServiceIntent!=null)
                getApplicationContext().stopService(mServiceIntent);
            SharedPreferences.Editor editor = sp.edit();
            editor.putBoolean(SP_LOGGED_IN, false);
            editor.commit();
            finish();
            Intent in = new Intent(getApplicationContext(), Login_Activity.class);
            startActivity(in);

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    public class Get_Cleaning_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(InspectionPending.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLInspectionPreadvice);
            httpPost.setHeader("Content-Type", "application/json");

            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("ArrayOfInspection");
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

                        inspection_arraylist = new ArrayList<InspectionBean>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            inspection_bean = new InspectionBean();
                            jsonObject = jsonarray.getJSONObject(i);


                             /*"EquipmentNo": "AAAA1111111", "EquipmentTypeId": "1", "EquipmentTypeCd": "TNK", "CLNNG_CERT_NO": "6297",
                    "CustomerId": "181", "CustomerCd": "TSTCUST", "PrevoiusCargo": "Acetic Acid",
                    "CHMCL_NM": "Acetic Acid","InDate": "10-13-2016 00:00:00", "OriginalCleaningDate": "8-1-2017 00:00:00",
                    "LatestCleaningDate": "8-1-2017 00:00:00",
                     "OriginalInspectionDate": "8-2-2017 00:00:00", "LatestInspectionDate":
                      "8-2-2017 00:00:00", "StatusID": "6", "StatusCD": "INS",
                     "Cleanedfor": "Chemicals 1", "LocationofCleaning": "Chennai Chemical Lab",
                     "CleaningStatusIid": "60", "CleaningStatusIcd": "Dry",
                     "CleaningStatusIIid": "64", "CleaningStatusIIcd": "Odourless", "ConditionID": "62", "ConditionCD": "Clean",
                     "ValveandFittingConditionID": "63", "ValveandFittingConditionCD": "UnClean",
                     "ManLidSealNo": "Man", "TopSealNo": "top",
                     "BottomSealNo": "bottom", "EIR_NO": "", "CleaningReferenceNo": "Ref1234567",
                      "CleaningRate": "800.00", "Remarks": "Cleaning Remarks",
                      "InvoiceToID": "183", "InvoiceToCD": "TEST1",
                     "AdditionalCleaningBit": "False", "GiTransactionNo": "8496", "CleaningId": "4385"*/

                            inspection_bean.setEquip_no(jsonObject.getString("EquipmentNo"));
                            inspection_bean.setEquip_no_type(jsonObject.getString("EquipmentTypeCd"));
                            inspection_bean.setEquip_no_type_id(jsonObject.getString("EquipmentTypeId"));
                            inspection_bean.setCertification_no(jsonObject.getString("CLNNG_CERT_NO"));
                            inspection_bean.setCustomer(jsonObject.getString("CustomerCd"));
                            inspection_bean.setCustomerId(jsonObject.getString("CustomerId"));
                            inspection_bean.setPrevious_cargo(jsonObject.getString("PrevoiusCargo"));
                            inspection_bean.setChemical_Name(jsonObject.getString("CHMCL_NM"));


                            SimpleDateFormat fromUser = new SimpleDateFormat("MM-dd-yyyy", Locale.ENGLISH);
                            SimpleDateFormat myFormat = new SimpleDateFormat("dd-MMM-yyyy",Locale.ENGLISH);

                            Date_in=jsonObject.getString("InDate");
                            OriginalCleaningDate=jsonObject.getString("OriginalCleaningDate");
                            LatestCleaningDate=jsonObject.getString("LatestCleaningDate");
                            OriginalInspectionDate=jsonObject.getString("OriginalInspectionDate");
                            LatestInspectionDate=jsonObject.getString("LatestInspectionDate");
                            String[] In_date=Date_in.split(" ");
                            String[] Original_CleaningDate=OriginalCleaningDate.split(" ");
                            String[] LastStatus_Date=LatestCleaningDate.split(" ");
                            String[] OriginalInspection_Date=OriginalInspectionDate.split(" ");
                            String[] LatestInspection_Date=LatestInspectionDate.split(" ");
                            Date_in=In_date[0];
                            OriginalCleaningDate=Original_CleaningDate[0];
                            OriginalInspectionDate=OriginalInspection_Date[0];
                            LatestInspectionDate=LatestInspection_Date[0];
                            LatestCleaningDate=LastStatus_Date[0];

                            try {
                                if (Date_in.equals(null) || Date_in.length() < 0
                                        ||Date_in.equals("") ) {

                                    Date_in = "";
                                } else {

                                    Date_in = myFormat.format(fromUser.parse(Date_in));




                                } if (OriginalCleaningDate.equals(null) || OriginalCleaningDate.length() < 0
                                        ||OriginalCleaningDate.equals("")||OriginalCleaningDate.equals("null") ) {

                                    OriginalCleaningDate = "";
                                } else {

                                    OriginalCleaningDate = myFormat.format(fromUser.parse(OriginalCleaningDate));




                                }if (LatestCleaningDate.equals(null) || LatestCleaningDate.length() < 0
                                        ||LatestCleaningDate.equals("") ) {

                                    LatestCleaningDate = "";
                                } else {

                                    LatestCleaningDate = myFormat.format(fromUser.parse(LatestCleaningDate));




                                }if (OriginalInspectionDate.equals(null) || OriginalInspectionDate.length() < 0
                                        ||OriginalInspectionDate.equals("") ) {

                                    OriginalInspectionDate = "";
                                } else {

                                    OriginalInspectionDate = myFormat.format(fromUser.parse(OriginalInspectionDate));




                                }if (LatestInspectionDate.equals(null) || LatestInspectionDate.length() < 0
                                        ||LatestInspectionDate.equals("") ) {

                                    LatestInspectionDate = "";
                                } else {

                                    LatestInspectionDate = myFormat.format(fromUser.parse(LatestInspectionDate));




                                }

                            }catch (Exception e)
                            {

                            }


                            inspection_bean.setInDate(Date_in);
                            inspection_bean.setOrgCleaningDate(OriginalCleaningDate);
                            inspection_bean.setLastCleaningDate(LatestCleaningDate);
                            inspection_bean.setOrgInspectionDate(OriginalInspectionDate);
                            inspection_bean.setLastInspectionDate(LatestInspectionDate);

                            inspection_bean.setEquip_status(jsonObject.getString("StatusCD"));
                            inspection_bean.setEquip_statusType(jsonObject.getString("StatusID"));
                            inspection_bean.setCleanedfor(jsonObject.getString("Cleanedfor"));
                            inspection_bean.setLocationofCleaning(jsonObject.getString("LocationofCleaning"));
                            inspection_bean.setCleaningStatusIid(jsonObject.getString("CleaningStatusIid"));
                            inspection_bean.setCleaningStatusIcd(jsonObject.getString("CleaningStatusIcd"));
                            inspection_bean.setCleaningStatusIIid(jsonObject.getString("CleaningStatusIIid"));
                            inspection_bean.setCleaningStatusIIcd(jsonObject.getString("CleaningStatusIIcd"));
                            inspection_bean.setConditionID(jsonObject.getString("ConditionID"));
                            inspection_bean.setConditionCD(jsonObject.getString("ConditionCD"));
                            inspection_bean.setValveandFittingConditionID(jsonObject.getString("ValveandFittingConditionID"));
                            inspection_bean.setValveandFittingConditionCD(jsonObject.getString("ValveandFittingConditionCD"));
                            inspection_bean.setManLidSealNo(jsonObject.getString("ManLidSealNo"));
                            inspection_bean.setTopSealNo(jsonObject.getString("TopSealNo"));
                            inspection_bean.setBottomSealNo(jsonObject.getString("BottomSealNo"));
                            inspection_bean.setEir_no(jsonObject.getString("EIR_NO"));
                            inspection_bean.setCleaningRefNo(jsonObject.getString("CleaningReferenceNo"));
                            inspection_bean.setCleaningRate(jsonObject.getString("CleaningRate"));
                            inspection_bean.setRemark(jsonObject.getString("Remarks"));
                            inspection_bean.setInvoiceToID(jsonObject.getString("InvoiceToID"));
                            inspection_bean.setInvoiceToCD(jsonObject.getString("InvoiceToCD"));
                            inspection_bean.setAdd_cleaningBit(jsonObject.getString("AdditionalCleaningBit"));
                            inspection_bean.setGi_trans_no(jsonObject.getString("GiTransactionNo"));
                            inspection_bean.setCleaningId(jsonObject.getString("CleaningId"));




/*
                            inspection_bean.setClean_unclean(jsonObject.getString("Clean_Unclean"));
                            inspection_bean.setSeal_no(jsonObject.getString("Seal_No"));

                            inspection_bean.setSlabrate(jsonObject.getString("SlabRate"));*/

                            inspection_arraylist.add(inspection_bean);



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
        protected void onPostExecute (Void aVoid) {


            if ((progressDialog != null) && progressDialog.isShowing()) {
                progressDialog.dismiss();
            }
            if (jsonarray != null) {
            if (inspection_arraylist != null) {
                adapter = new UserListAdapter(InspectionPending.this, R.layout.list_item_row_inspection, inspection_arraylist);
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
            } else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }
        }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }
        }

    }

    public class UserListAdapter extends BaseAdapter {
        private final ArrayList<InspectionBean> arraylist;
        private ArrayList<InspectionBean> list;
        Context context;

        int resource;
        private InspectionBean userListBean;
        int lastPosition = -1;
        public UserListAdapter(Context context, int resource, ArrayList<InspectionBean> list) {
            this.context = context;
            this.list = list;
            this.resource = resource;
            this.arraylist = new ArrayList<InspectionBean>();
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

                lastPosition = position;

                holder.whole = (LinearLayout) convertView.findViewById(R.id.LL_whole);
                holder.Cust_Name = (TextView) convertView.findViewById(R.id.text1);
                holder.equip_no = (TextView) convertView.findViewById(R.id.text2);
                holder.time = (TextView) convertView.findViewById(R.id.text3);
                holder.tv_Bill_to = (TextView) convertView.findViewById(R.id.tv_Bill_to);
                holder.tv_Billing_type = (TextView) convertView.findViewById(R.id.tv_Billing_type);
                holder.previous_crg = (TextView) convertView.findViewById(R.id.text4);
                holder.equipStatusType = (TextView) convertView.findViewById(R.id.text5);
                holder.orgCleaningDate = (TextView) convertView.findViewById(R.id.tv_type);
                holder.customer_Id = (TextView) convertView.findViewById(R.id.text6);
                holder.cleaningRate = (TextView) convertView.findViewById(R.id.tv_code);
                holder.lastStatusDate = (TextView) convertView.findViewById(R.id.tv_location);
                holder.add_cleaning_bit = (TextView) convertView.findViewById(R.id.tv_transport);
                holder.cleaningId = (TextView) convertView.findViewById(R.id.tv_vechicle);
                holder.cleaningRefNo = (TextView) convertView.findViewById(R.id.text7);
                holder.remark = (TextView) convertView.findViewById(R.id.text8);
                holder.eir_no = (TextView) convertView.findViewById(R.id.tv_pre_adv_id);
                holder.equipStatus = (TextView) convertView.findViewById(R.id.text10);
                holder.slabRate = (TextView) convertView.findViewById(R.id.tv_status);
                holder.orgInspectionDate = (TextView) convertView.findViewById(R.id.text9);
                holder.clean_unclean = (TextView) convertView.findViewById(R.id.tv_pre_code);
                holder.gi_transNo = (TextView) convertView.findViewById(R.id.tv_pre_id);
                holder.Seal_no = (TextView) convertView.findViewById(R.id.tv_cust_code);
                holder.type_id = (TextView) convertView.findViewById(R.id.tv_type_code);
                holder.code_id = (TextView) convertView.findViewById(R.id.tv_code_id);
                holder.cleaningMethod = (TextView) convertView.findViewById(R.id.tv_cleaning_method);
                holder.add_Cleaning = (TextView) convertView.findViewById(R.id.tv_additional_cleraning);
                holder.LL_username = (LinearLayout)convertView.findViewById(R.id.LL_username);
                holder.LL_username.setVisibility(View.GONE);

                holder.tv_Billing_type.setVisibility(View.INVISIBLE);

                // R.id.tv_customerName,R.id.tv_Inv_no,R.id.tv_date,R.id.tv_val,R.id.tv_due
                convertView.setTag(holder);
            } else {
                holder = (ViewHolder) convertView.getTag();
            }
            if (list.size() < 1){
                Toast.makeText(getApplicationContext(), "NO DATA FOUND", Toast.LENGTH_LONG).show();
            }else {

                userListBean = list.get(position);



                holder.equip_no.setText(userListBean.getEquip_no() );
                holder.Cust_Name.setText(userListBean.getCustomer()+ "," + userListBean.getEquip_no_type());
                holder.time.setText(userListBean.getOrgCleaningDate());
                holder.previous_crg.setText(userListBean.getLastCleaningDate());
                holder.tv_Bill_to.setText(userListBean.getPrevious_cargo());
                holder.tv_Billing_type.setText(userListBean.getPrevious_cargo());




               /* if(userListBean.getVechicle().equals("")||userListBean.getVechicle()==null)
                {
                    holder.vechicle.setText("");
                }
               else{
                    holder.vechicle.setText(userListBean.getVechicle());
                }*/

                holder.equipStatus.setText(userListBean.getEquip_status());
                holder.customer_Id.setText(userListBean.getCustomerId());
                holder.lastStatusDate.setText(userListBean.getLastStatusDate());
                holder.add_cleaning_bit.setText(userListBean.getAdd_cleaningBit());
                holder.cleaningId.setText(userListBean.getCleaningId());
                holder.cleaningRefNo.setText(userListBean.getCleaningRefNo());
                holder.remark.setText(userListBean.getRemark());
                holder.orgCleaningDate.setText(userListBean.getOrgCleaningDate());
                holder.orgInspectionDate.setText(userListBean.getOrgInspectionDate());
                holder.Seal_no.setText(userListBean.getSeal_no());
                holder.clean_unclean.setText(userListBean.getClean_unclean());
                holder.eir_no.setText(userListBean.getEir_no());
                holder.cleaningRate.setText(userListBean.getCleaningRate());
                holder.slabRate.setText(userListBean.getSlabrate());
                holder.gi_transNo.setText(userListBean.getGi_trans_no());




                holder.whole.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {

                        Intent i = new Intent(getApplicationContext(), InspectionUpdate.class);

                        /*"EquipmentNo": "GSHS", "EquipmentTypeId": "1", "EquipmentTypeCd": "TNK",
                         "CLNNG_CERT_NO": "", "CustomerId": "181", "CustomerCd": "TSTCUST",
                         "PrevoiusCargo": "Acetic Acid", "CHMCL_NM": "Acetic Acid", "InDate": "1-5-2017 00:00:00",
                          "OriginalCleaningDate": "1-24-2017 11:26:00", "LatestCleaningDate": "1-24-2017 11:26:00",
                           "OriginalInspectionDate": null, "LatestInspectionDate": null, "StatusID": "6",
                           "StatusCD": "INS", "Cleanedfor": "Chemicals", "LocationofCleaning": "JUB ",
                           "CleaningStatusIid": "60", "CleaningStatusIcd": "Dry", "CleaningStatusIIid": "64",
                           "CleaningStatusIIcd": "Odourless", "ConditionID": "62", "ConditionCD": "Clean",
                            "ValveandFittingConditionID": "62", "ValveandFittingConditionCD": "Clean", "ManLidSealNo": "",
                             "TopSealNo": "", "BottomSealNo": "", "EIR_NO": "", "CleaningReferenceNo": "",
                             "CleaningRate": "800.00", "Remarks": "", "InvoiceToID": "", "InvoiceToCD": "",
                             "AdditionalCleaningBit": "False", "GiTransactionNo": "8563", "CleaningId": "4379"*/

                        GlobalConstants.equipment_no = list.get(position).getEquip_no();
                        GlobalConstants.customer_name = list.get(position).getCustomer();
                        GlobalConstants.customer_Id = list.get(position).getCustomerId();
                        GlobalConstants.status = list.get(position).getEquip_status();
                        GlobalConstants.status_id = list.get(position).getEquip_statusType();
                        GlobalConstants.equip_status_type = list.get(position).getEquip_no_type();
                        GlobalConstants.equip_status_id = list.get(position).getEquip_no_type_id();
                        GlobalConstants.LatestInspectionDate = list.get(position).getLastInspectionDate();
                        GlobalConstants.OriginalInspectionDate = list.get(position).getOrgInspectionDate();
                        GlobalConstants.indate = list.get(position).getInDate();
                        GlobalConstants.previous_cargo = list.get(position).getPrevious_cargo();
                        GlobalConstants.add_cleaning_bit = list.get(position).getAdd_cleaningBit();
                        GlobalConstants.cleaning_id = list.get(position).getCleaningId();
                        GlobalConstants.cleaning_RefNo = list.get(position).getCleaningRefNo();
                        GlobalConstants.remark = list.get(position).getRemark();
                        GlobalConstants.org_cleaningDate = list.get(position).getOrgCleaningDate();
                        GlobalConstants.org_inspectionDate = list.get(position).getOrgInspectionDate();
                        GlobalConstants.clean_unclean = list.get(position).getClean_unclean();
                        GlobalConstants.Certification_no = list.get(position).getCertification_no();
                        GlobalConstants.seal_no = list.get(position).getSeal_no();
                        GlobalConstants.eir_no = list.get(position).getEir_no();
                        GlobalConstants.clean_rate = list.get(position).getCleaningRate();
                        GlobalConstants.slab_rate = list.get(position).getSlabrate();
                        GlobalConstants.LocationofCleaning = list.get(position).getLocationofCleaning();
                        GlobalConstants.Cleanedfor = list.get(position).getCleanedfor();
                        GlobalConstants.CleaningRate = list.get(position).getCleaningRate();
                        GlobalConstants.gi_trans_no = list.get(position).getGi_trans_no();
                        GlobalConstants.BottomSealNo = list.get(position).getBottomSealNo();
                        GlobalConstants.TopSealNo = list.get(position).getTopSealNo();
                        GlobalConstants.ManLidSealNo = list.get(position).getManLidSealNo();
                        GlobalConstants.ConditionCD = list.get(position).getConditionCD();
                        GlobalConstants.ConditionID = list.get(position).getConditionID();
                        GlobalConstants.CleaningStatusIIcd = list.get(position).getCleaningStatusIIcd();
                        GlobalConstants.CleaningStatusIIid = list.get(position).getCleaningStatusIIid();
                        GlobalConstants.CleaningStatusIcd = list.get(position).getCleaningStatusIcd();
                        GlobalConstants.CleaningStatusIid = list.get(position).getCleaningStatusIid();
                        GlobalConstants.ValveandFittingConditionCD = list.get(position).getValveandFittingConditionCD();
                        GlobalConstants.ValveandFittingConditionID = list.get(position).getValveandFittingConditionID();

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
                for (InspectionBean wp : arraylist) {
                    if (wp.getCustomer().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getEquip_no().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getPrevious_cargo().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getInDate().toLowerCase(Locale.getDefault()).contains(charText)||
                            wp.getEquip_statusType().toLowerCase(Locale.getDefault()).contains(charText)
                            ) {
                        list.add(wp);
/*
                        listview.setVisibility(View.VISIBLE);
*/
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
        TextView equip_no,tv_Billing_type,tv_Bill_to,time, date,Cust_Name,equipStatusType,previous_crg, equipStatus,cleaningMethod,add_Cleaning,customer_Id, cleaningRate, lastStatusDate, gi_transNo,pre_code,cust_code,type_id,code_id,
                vechicle, add_cleaning_bit, cleaningId, cleaningRefNo,rental_bt,remark, eir_no,orgInspectionDate,clean_unclean,Seal_no,slabRate,pre_adv_id, orgCleaningDate;
        CheckBox checkBox;

        LinearLayout whole,LL_username;
    }


    public class Create_GateIn_Customer_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(InspectionPending.this);
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


            if (jsonarray != null)
            {
                if (dropdown_customer_list != null) {
                    boxAdapter = new ListAdapter(InspectionPending.this, products);
                    searchlist.setAdapter(boxAdapter);

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


                }else
                {
                    shortToast(getApplicationContext(),"Data Not Found");

                }

            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");

            }

            progressDialog.dismiss();

        }

    }

    public class Get_Inspection_Dropdown_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(InspectionPending.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLInspectionFilter);
            httpPost.setHeader("Content-Type", "application/json");

            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("filterType", fieldItems);
                jsonObject.put("filterCondition", opratorItems);
                jsonObject.put("filterValue", getEditText);
                jsonObject.put("Mode", "new");



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

                    System.out.println("Am HashMap list"+jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
//                                shortToast(getApplicationContext(), "No Records Found");
                                products.clear();
                                no_data.setVisibility(View.VISIBLE);

                            }
                        });
                    }else {

                        pending_accordion_arraylist = new ArrayList<>();

                        products = new ArrayList<Product>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            pending_accordion_bean = new PendingAccordionBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            pending_accordion_bean.setValues(jsonObject.getString("Values"));
                            products.add(new Product(jsonObject.getString("Values"),false));

                            pending_accordion_arraylist.add(pending_accordion_bean);

                        }
                    }
                }else if(jsonarray.length()<1){
                    runOnUiThread(new Runnable(){

                        @Override
                        public void run(){
                            //update ui here
                            // display toast here
//                            shortToast(getApplicationContext(),"No Records Found.");
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
        protected void onPostExecute (Void aVoid) {


            if (jsonarray != null)
            {
                if (pending_accordion_arraylist != null) {
                    boxAdapter = new ListAdapter(InspectionPending.this, products);
                    searchlist.setAdapter(boxAdapter);


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


                }  else
                {
//                    shortToast(getApplicationContext(),"Data Not Found");
                    LL_hole.setVisibility(View.GONE);
                    no_data.setVisibility(View.VISIBLE);
                    searchlist.setVisibility(View.GONE);


                }
        }
            else
            {
//                shortToast(getApplicationContext(),"Data Not Found");
                LL_hole.setVisibility(View.GONE);
                no_data.setVisibility(View.VISIBLE);
                searchlist.setVisibility(View.GONE);


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



    public class Get_Inspection_SearchList_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray preadvicejsonlist;
        private JSONObject preadvicejsonObject;
        private JSONObject SearchValuesObject;
        private String preadviceObject;


        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(InspectionPending.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLInspectionSearchList);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try{
                JSONObject jsonObject = new JSONObject();

                preadvicejsonlist = new JSONArray();
                SearchValuesObject=new JSONObject();

                for (int i = 0; i <selected_name.size(); i++) {
                    preadvicejsonObject=new JSONObject();
                    preadvicejsonObject.put("values", selected_name.get(i));
                    preadvicejsonlist.put(preadvicejsonObject);
                }

                SearchValuesObject.put("SearchValues",preadvicejsonlist);




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
                Log.d("Search_request", jsonObject.toString());

                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("ArrayOfInspection");
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

                        inspection_arraylist = new ArrayList<InspectionBean>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            inspection_bean = new InspectionBean();
                            jsonObject = jsonarray.getJSONObject(i);


                             /*"EquipmentNo": "AAAA1111111", "EquipmentTypeId": "1", "EquipmentTypeCd": "TNK", "CLNNG_CERT_NO": "6297",
                    "CustomerId": "181", "CustomerCd": "TSTCUST", "PrevoiusCargo": "Acetic Acid",
                    "CHMCL_NM": "Acetic Acid","InDate": "10-13-2016 00:00:00", "OriginalCleaningDate": "8-1-2017 00:00:00",
                    "LatestCleaningDate": "8-1-2017 00:00:00",
                     "OriginalInspectionDate": "8-2-2017 00:00:00", "LatestInspectionDate":
                      "8-2-2017 00:00:00", "StatusID": "6", "StatusCD": "INS",
                     "Cleanedfor": "Chemicals 1", "LocationofCleaning": "Chennai Chemical Lab",
                     "CleaningStatusIid": "60", "CleaningStatusIcd": "Dry",
                     "CleaningStatusIIid": "64", "CleaningStatusIIcd": "Odourless", "ConditionID": "62", "ConditionCD": "Clean",
                     "ValveandFittingConditionID": "63", "ValveandFittingConditionCD": "UnClean",
                     "ManLidSealNo": "Man", "TopSealNo": "top",
                     "BottomSealNo": "bottom", "EIR_NO": "", "CleaningReferenceNo": "Ref1234567",
                      "CleaningRate": "800.00", "Remarks": "Cleaning Remarks",
                      "InvoiceToID": "183", "InvoiceToCD": "TEST1",
                     "AdditionalCleaningBit": "False", "GiTransactionNo": "8496", "CleaningId": "4385"*/

                            inspection_bean.setEquip_no(jsonObject.getString("EquipmentNo"));
                            inspection_bean.setEquip_no_type(jsonObject.getString("EquipmentTypeCd"));
                            inspection_bean.setEquip_no_type_id(jsonObject.getString("EquipmentTypeId"));
                            inspection_bean.setCertification_no(jsonObject.getString("CLNNG_CERT_NO"));
                            inspection_bean.setCustomer(jsonObject.getString("CustomerCd"));
                            inspection_bean.setCustomerId(jsonObject.getString("CustomerId"));
                            inspection_bean.setPrevious_cargo(jsonObject.getString("PrevoiusCargo"));
                            inspection_bean.setChemical_Name(jsonObject.getString("CHMCL_NM"));


                            SimpleDateFormat fromUser = new SimpleDateFormat("MM-dd-yyyy", Locale.ENGLISH);
                            SimpleDateFormat myFormat = new SimpleDateFormat("dd-MMM-yyyy",Locale.ENGLISH);

                            Date_in=jsonObject.getString("InDate");
                            OriginalCleaningDate=jsonObject.getString("OriginalCleaningDate");
                            LatestCleaningDate=jsonObject.getString("LatestCleaningDate");
                            OriginalInspectionDate=jsonObject.getString("OriginalInspectionDate");
                            LatestInspectionDate=jsonObject.getString("LatestInspectionDate");
                            String[] In_date=Date_in.split(" ");
                            String[] Original_CleaningDate=OriginalCleaningDate.split(" ");
                            String[] LastStatus_Date=LatestCleaningDate.split(" ");
                            String[] OriginalInspection_Date=OriginalInspectionDate.split(" ");
                            String[] LatestInspection_Date=LatestInspectionDate.split(" ");
                            Date_in=In_date[0];
                            OriginalCleaningDate=Original_CleaningDate[0];
                            OriginalInspectionDate=OriginalInspection_Date[0];
                            LatestInspectionDate=LatestInspection_Date[0];
                            LatestCleaningDate=LastStatus_Date[0];

                            try {
                                if (Date_in.equals(null) || Date_in.length() < 0
                                        ||Date_in.equals("") ) {

                                    Date_in = "";
                                } else {

                                    Date_in = myFormat.format(fromUser.parse(Date_in));




                                } if (OriginalCleaningDate.equals(null) || OriginalCleaningDate.length() < 0
                                        ||OriginalCleaningDate.equals("")||OriginalCleaningDate.equals("null") ) {

                                    OriginalCleaningDate = "";
                                } else {

                                    OriginalCleaningDate = myFormat.format(fromUser.parse(OriginalCleaningDate));




                                }if (LatestCleaningDate.equals(null) || LatestCleaningDate.length() < 0
                                        ||LatestCleaningDate.equals("") ) {

                                    LatestCleaningDate = "";
                                } else {

                                    LatestCleaningDate = myFormat.format(fromUser.parse(LatestCleaningDate));




                                }if (OriginalInspectionDate.equals(null) || OriginalInspectionDate.length() < 0
                                        ||OriginalInspectionDate.equals("") ) {

                                    OriginalInspectionDate = "";
                                } else {

                                    OriginalInspectionDate = myFormat.format(fromUser.parse(OriginalInspectionDate));




                                }if (LatestInspectionDate.equals(null) || LatestInspectionDate.length() < 0
                                        ||LatestInspectionDate.equals("") ) {

                                    LatestInspectionDate = "";
                                } else {

                                    LatestInspectionDate = myFormat.format(fromUser.parse(LatestInspectionDate));




                                }

                            }catch (Exception e)
                            {

                            }


                            inspection_bean.setInDate(Date_in);
                            inspection_bean.setOrgCleaningDate(OriginalCleaningDate);
                            inspection_bean.setLastCleaningDate(LatestCleaningDate);
                            inspection_bean.setOrgInspectionDate(OriginalInspectionDate);
                            inspection_bean.setLastInspectionDate(LatestInspectionDate);

                            inspection_bean.setEquip_status(jsonObject.getString("StatusCD"));
                            inspection_bean.setEquip_statusType(jsonObject.getString("StatusID"));
                            inspection_bean.setCleanedfor(jsonObject.getString("Cleanedfor"));
                            inspection_bean.setLocationofCleaning(jsonObject.getString("LocationofCleaning"));
                            inspection_bean.setCleaningStatusIid(jsonObject.getString("CleaningStatusIid"));
                            inspection_bean.setCleaningStatusIcd(jsonObject.getString("CleaningStatusIcd"));
                            inspection_bean.setCleaningStatusIIid(jsonObject.getString("CleaningStatusIIid"));
                            inspection_bean.setCleaningStatusIIcd(jsonObject.getString("CleaningStatusIIcd"));
                            inspection_bean.setConditionID(jsonObject.getString("ConditionID"));
                            inspection_bean.setConditionCD(jsonObject.getString("ConditionCD"));
                            inspection_bean.setValveandFittingConditionID(jsonObject.getString("ValveandFittingConditionID"));
                            inspection_bean.setValveandFittingConditionCD(jsonObject.getString("ValveandFittingConditionCD"));
                            inspection_bean.setManLidSealNo(jsonObject.getString("ManLidSealNo"));
                            inspection_bean.setTopSealNo(jsonObject.getString("TopSealNo"));
                            inspection_bean.setBottomSealNo(jsonObject.getString("BottomSealNo"));
                            inspection_bean.setEir_no(jsonObject.getString("EIR_NO"));
                            inspection_bean.setCleaningRefNo(jsonObject.getString("CleaningReferenceNo"));
                            inspection_bean.setCleaningRate(jsonObject.getString("CleaningRate"));
                            inspection_bean.setRemark(jsonObject.getString("Remarks"));
                            inspection_bean.setInvoiceToID(jsonObject.getString("InvoiceToID"));
                            inspection_bean.setInvoiceToCD(jsonObject.getString("InvoiceToCD"));
                            inspection_bean.setAdd_cleaningBit(jsonObject.getString("AdditionalCleaningBit"));
                            inspection_bean.setGi_trans_no(jsonObject.getString("GiTransactionNo"));
                            inspection_bean.setCleaningId(jsonObject.getString("CleaningId"));




/*
                            inspection_bean.setClean_unclean(jsonObject.getString("Clean_Unclean"));
                            inspection_bean.setSeal_no(jsonObject.getString("Seal_No"));

                            inspection_bean.setSlabrate(jsonObject.getString("SlabRate"));*/

                            inspection_arraylist.add(inspection_bean);



                        }
                    }
                }else{
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
                if (inspection_arraylist != null) {
                    adapter = new UserListAdapter(InspectionPending.this, R.layout.list_item_row_inspection, inspection_arraylist);
                    listview.setAdapter(adapter);

                } else {
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
