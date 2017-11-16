package com.i_tankdepo;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.widget.Toolbar;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;

import com.i_tankdepo.Beanclass.CustomerReportBean;
import com.i_tankdepo.Beanclass.GeneralReportBean;
import com.i_tankdepo.Beanclass.PendingAccordionBean;
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
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Locale;

/**
 * Created by Admin on 1/4/2017.
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
public class SelectOptions extends CommonActivity {
    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu, im_down, im_up, im_heat_close, im_heat_ok, iv_back,iv_changeOfStatus,im_date;
    private Button status_home,status_refresh,run_report;
    private EditText searchView;
    private ListView select_list_view;
    private TextView tv_toolbarTitle,tv_heat_refresh,tv_selected;
    private LinearLayout LL_status_submit,LL_run,LL_date,LL_mandate_fields;
    String from,selected_name,selected_name_optional;
    private PendingAccordionBean pending_accordion_bean;
    private ArrayList<PendingAccordionBean> pending_accordion_arraylist;
    private ArrayList<Product_Stock> products;
    private ArrayList<Product_Stock> box;
    private ListAdapter boxAdapter;
    List<String> selected_id_list = new ArrayList<String>();
    List<String> selected_equip_id_list = new ArrayList<String>();
    List<String> selected_prev_id_list = new ArrayList<String>();
    List<String> selected_curnt_id_list = new ArrayList<String>();
    List<String> selected_next_id_list = new ArrayList<String>();
    List<String> selected_depo_id_list = new ArrayList<String>();
    private ProgressDialog progressDialog;
    private GeneralReportBean generalReportBean;
    private ArrayList<GeneralReportBean> generalReportBeanArrayList;
    private TypeReportBean typeReportBean;
    private CustomerReportBean customerReportBean;
    private ArrayList<CustomerReportBean> customerReportbeanArrayList;
    private ArrayList<TypeReportBean> typeReportBeanArrayList;
    private EditText ed_date;
    static final int DATE_DIALOG_ID = 1999;
    private Calendar c;
    private int year,month,day,second;
    private String systemDate;
    String cleaning_frm_date,cleaning_to_date,Insp_frm_date,Insp_to_date,In_date_from,In_date_to,Out_date_from,
            Out_date_to,Equip_date,EIR_date,current_frm_date,current_to_date,Nxt_frm_date,Nxt_to_date;
    private List<String> Cust_id=new ArrayList<>();
    private List<String> Equip_id=new ArrayList<>();
    private List<String> prevc_id=new ArrayList<>();
    private List<String> curnt_staus_id=new ArrayList<>();
    private List<String> next_test_id=new ArrayList<>();
    private List<String> dept_id=new ArrayList<>();
    private String curTime;
    private JSONObject CustomerSummaryjsonarray;
    private JSONObject TypeSummaryjsonarray;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.select_options);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        menu.setVisibility(View.GONE);


        from= GlobalConstants.from;
        selected_name= GlobalConstants.selectedName;
        selected_name_optional= GlobalConstants.selectedName_optional;
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_selected = (TextView) findViewById(R.id.tv_selected);
        tv_toolbarTitle.setText("Select Options");
        tv_heat_refresh  = (TextView)findViewById(R.id.tv_heat_refresh);
        select_list_view  = (ListView)findViewById(R.id.select_list_view);
        tv_heat_refresh.setText("Reset");

        ed_date = (EditText)findViewById(R.id.ed_date);
        ed_date.setOnClickListener(this);

        im_date = (ImageView)findViewById(R.id.im_date);
        im_date.setOnClickListener(this);
        if(cd.isConnectingToInternet()){
             new Get_ScockReport_list_details().execute();
        }else{
            shortToast(getApplicationContext(),"Please check your Internet Connection..!");
        }
        status_home = (Button)findViewById(R.id.status_home);
        status_refresh = (Button)findViewById(R.id.status_refresh);
        run_report = (Button)findViewById(R.id.run_report);
        searchView = (EditText)findViewById(R.id.searchView);
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        LL_status_submit = (LinearLayout)findViewById(R.id.LL_status_submit);
        LL_run = (LinearLayout)findViewById(R.id.LL_run);
        LL_mandate_fields = (LinearLayout)findViewById(R.id.LL_mandate_fields);
        LL_date = (LinearLayout)findViewById(R.id.LL_date);
        LL_status_submit.setVisibility(View.GONE);
        tv_selected.setOnClickListener(this);
        if(from.equalsIgnoreCase("optional"))
        {
            LL_mandate_fields.setVisibility(View.GONE);
            LL_date.setVisibility(View.VISIBLE);
        }else
        {
            LL_mandate_fields.setVisibility(View.VISIBLE);
            LL_date.setVisibility(View.GONE);
        }

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
        systemDate = new SimpleDateFormat("dd-MMM-yyyy").format(new Date());

        Log.i("systemDate",systemDate);
        status_home.setOnClickListener(this);
        status_refresh.setOnClickListener(this);
        run_report.setOnClickListener(this);
        iv_back.setOnClickListener(this);
    }

    @Override
    public void onClick(View v) {
        switch (v.getId() ){
            case R.id.iv_changeOfStatus:
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));
                break;
            case R.id.status_home:
                startActivity(new Intent(getApplicationContext(),MainActivity.class));
                break;
            case R.id.status_refresh:

                /*    public static List<String> selected_Stock_Cust_Id;
            public static List<String> selected_Stock_Equp_Id;
            public static List<String> selected_Stock_Prev_Crg_Id;
            public static List<String> selected_Stock_Curnt_Status_Id;
            public static List<String> selected_Stock_Nxt_Tst_Type_Id;
            public static List<String> selected_Stock_Depot_Id;*/
                if(selected_name.equalsIgnoreCase("Customer"))
                {
                    GlobalConstants.selected_Stock_Cust_Id.removeAll( GlobalConstants.selected_Stock_Cust_Id);

                }else if(selected_name.equalsIgnoreCase("Equipment Type"))
                {
                    GlobalConstants.selected_Stock_Equp_Id.removeAll( GlobalConstants.selected_Stock_Equp_Id);
                }else if(selected_name.equalsIgnoreCase("Previous Cargo"))
                {
                    GlobalConstants.selected_Stock_Prev_Crg_Id.removeAll( GlobalConstants.selected_Stock_Prev_Crg_Id);

                }else if(selected_name.equalsIgnoreCase("Current Status")) {
                    GlobalConstants.selected_Stock_Curnt_Status_Id.removeAll( GlobalConstants.selected_Stock_Curnt_Status_Id);

                }else if(selected_name.equalsIgnoreCase("Next Test Type"))
                {
                    GlobalConstants.selected_Stock_Nxt_Tst_Type_Id.removeAll( GlobalConstants.selected_Stock_Nxt_Tst_Type_Id);

                }else {
                    GlobalConstants.selected_Stock_Depot_Id.removeAll( GlobalConstants.selected_Stock_Depot_Id);

                }
                finish();
                startActivity(getIntent());
                break;
            case R.id.ed_date:
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_date:
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.iv_back:
                finish();
                onBackPressed();
                break;
            case R.id.tv_selected:

                String date=ed_date.getText().toString();

                if (selected_name_optional.equalsIgnoreCase("Cleaning Date From")) {
                    GlobalConstants.Cleaning_date_from = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                } else if (selected_name_optional.equalsIgnoreCase("Cleaning Date To")) {
                    GlobalConstants.Cleaning_date_to = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                } else if (selected_name_optional.equalsIgnoreCase("In Date From")) {
                    GlobalConstants.In_date_from = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                }
                else if (selected_name_optional.equalsIgnoreCase("In Date To")) {
                    GlobalConstants.In_date_to = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                }
                else if (selected_name_optional.equalsIgnoreCase("Inspection Date From")) {
                    GlobalConstants.Inspection_date_from = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                } else if (selected_name_optional.equalsIgnoreCase("Inspection Date To")) {
                    GlobalConstants.Inspection_date_to = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                } else if (selected_name_optional.equalsIgnoreCase("Current Status Date From")) {
                    GlobalConstants.Current_status_date_from = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                } else if (selected_name_optional.equalsIgnoreCase("Current Status Date To")) {
                    GlobalConstants.Current_status_date_to = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                } else if (selected_name_optional.equalsIgnoreCase("Next Test Date From")) {
                    GlobalConstants.Nxt_Tst_date_from = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                } else if (selected_name_optional.equalsIgnoreCase("Next Test Date To")) {
                    GlobalConstants.Nxt_Tst_date_to = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                } else if (selected_name_optional.equalsIgnoreCase("Equipment Number")) {
                    GlobalConstants.Equip_No_date = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                } else if (selected_name_optional.equalsIgnoreCase("EIR Number")) {
                    GlobalConstants.EIR_No_date = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                } else if (selected_name_optional.equalsIgnoreCase("Out Date From")) {
                    GlobalConstants.Out_date_from = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                } else if (selected_name_optional.equalsIgnoreCase("Out Date To")) {
                    GlobalConstants.Out_date_to = date;
                    if(date.length()!=0) {
                        shortToast(getApplicationContext(), "Date Selected");
                    }else{
                        shortToast(getApplicationContext(), "Please Select the date..!");
                    }
                }

                if(boxAdapter.getBox().size()==0) {
                   // shortToast(getApplicationContext(), "Please Select atleast One Value..!");
                }else {

                    selected_id_list.clear();
                    selected_equip_id_list.clear();
                    selected_prev_id_list.clear();
                    selected_curnt_id_list.clear();
                    selected_next_id_list.clear();
                    selected_depo_id_list.clear();

                    for (Product_Stock p : boxAdapter.getBox()) {
                        if (p.box) {
                            if (p.box == true) {
                                String[] set = new String[2];
                                set[0] = p.Id;



                                if(selected_name.equalsIgnoreCase("Customer"))
                                {
                                    selected_id_list.add(set[0]);
                                    GlobalConstants.selected_Stock_Cust_Id=selected_id_list;
                                    shortToast(getApplicationContext(),"Customer Selected");
                                    Log.d("Customer", String.valueOf(selected_id_list));

                                }else if(selected_name.equalsIgnoreCase("Equipment Type"))
                                {
                                    selected_equip_id_list.add(set[0]);
                                    GlobalConstants.selected_Stock_Equp_Id=selected_equip_id_list;
                                    shortToast(getApplicationContext(),"Equipment Type Selected");
                                    Log.d("Equipment", String.valueOf(selected_equip_id_list));

                                }else if(selected_name.equalsIgnoreCase("Previous Cargo"))
                                {
                                    selected_prev_id_list.add(set[0]);
                                    GlobalConstants.selected_Stock_Prev_Crg_Id=selected_prev_id_list;
                                    shortToast(getApplicationContext(),"Previous Cargo Selected");
                                    Log.d("Previous", String.valueOf(selected_prev_id_list));

                                }else if(selected_name.equalsIgnoreCase("Current Status")) {
                                    selected_curnt_id_list.add(set[0]);
                                    GlobalConstants.selected_Stock_Curnt_Status_Id=selected_curnt_id_list;
                                    shortToast(getApplicationContext(),"Current Status Selected");
                                    Log.d("Current", String.valueOf(selected_curnt_id_list));

                                }else if(selected_name.equalsIgnoreCase("Next Test Type"))
                                {
                                    selected_next_id_list.add(set[0]);
                                    GlobalConstants.selected_Stock_Nxt_Tst_Type_Id=selected_next_id_list;
                                    shortToast(getApplicationContext(),"Next Test Type Selected");
                                    Log.d("Next", String.valueOf(selected_next_id_list));

                                }else if(selected_name.equalsIgnoreCase("Depot")) {
                                    selected_depo_id_list.add(set[0]);
                                    GlobalConstants.selected_Stock_Depot_Id=selected_depo_id_list;
                                    shortToast(getApplicationContext(),"Depot selected");
                                    Log.d("Depot", String.valueOf(selected_depo_id_list));

                                }
                            } else {
                                shortToast(getApplicationContext(), "Please Select Customer Name..!");
                            }
                        }
                    }
                }
                break;

            case R.id.run_report:

                    Cust_id=GlobalConstants.selected_Stock_Cust_Id;

                    Equip_id=GlobalConstants.selected_Stock_Equp_Id;
                    prevc_id=GlobalConstants.selected_Stock_Prev_Crg_Id;
                    curnt_staus_id=GlobalConstants.selected_Stock_Curnt_Status_Id;

                    next_test_id=GlobalConstants.selected_Stock_Nxt_Tst_Type_Id;
                    dept_id=GlobalConstants.selected_Stock_Depot_Id;

                    cleaning_frm_date=GlobalConstants.Cleaning_date_from;
                    cleaning_to_date=GlobalConstants.Cleaning_date_to;
                    In_date_from=GlobalConstants.In_date_from;
                    In_date_to=GlobalConstants.In_date_to;
                    Insp_frm_date=GlobalConstants.Inspection_date_from;
                    Insp_to_date=GlobalConstants.Inspection_date_to;
                    current_frm_date=GlobalConstants.Current_status_date_from;
                    current_to_date=GlobalConstants.Current_status_date_to;
                    Nxt_frm_date=GlobalConstants.Nxt_Tst_date_from;
                    Nxt_to_date=GlobalConstants.Nxt_Tst_date_to;
                    Equip_date=GlobalConstants.Equip_No_date;
                    EIR_date=GlobalConstants.EIR_No_date;
                    Out_date_from=GlobalConstants.Out_date_from;
                    Out_date_to=GlobalConstants.Out_date_to;


                    if(cd.isConnectingToInternet())
                    {
                        new Post_Stock_report().execute();
                    }else
                    {
                        shortToast(getApplicationContext(),"Please Check Your Internet Connection..!");
                    }


                break;
        }
    }

    @Override
    protected Dialog onCreateDialog(int id) {
        switch (id) {
            case DATE_DIALOG_ID:

                // open datepicker dialog.
                // set date picker for current date
                // add pickerListener listner to date picker
                return new DatePickerDialog(this, pickerListener, year, month,day);



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



            //    System.out.println("am a new from date====>>" + str_From);

            ed_date.setText(formatDate(year, month, day));

        }
    };

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
            progressDialog = new ProgressDialog(SelectOptions.this);
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
                if (Cust_id.size() != 0) {
                    for (int i = 0; i < Cust_id.size(); i++) {
                        Log.d("Current Status", String.valueOf(Cust_id));
                        preadvicejsonObject = new JSONObject();
                        preadvicejsonObject.put("Type", Cust_id.get(i));
                        Custjsonlist.put(preadvicejsonObject);
                    }
                }
            }catch (Exception e)
            {

            }
          try
          {
           if(Equip_id.size()!=0) {

               for (int i = 0; i < Equip_id.size(); i++) {
                   Log.d("Current Status", String.valueOf(Equip_id));
                   preadvicejsonObject = new JSONObject();
                   preadvicejsonObject.put("Type", Equip_id.get(i));
                   Equipjsonlist.put(preadvicejsonObject);
               }
           }
    } catch (Exception e)
          {

          }

                try {
                    if (prevc_id.size() != 0) {
                        for (int i = 0; i < prevc_id.size(); i++) {
                            preadvicejsonObject = new JSONObject();
                            preadvicejsonObject.put("Type", prevc_id.get(i));
                            previousjsonlist.put(preadvicejsonObject);
                        }
                    }
                }
                catch (Exception e)

                {

                }
                try
                {
                if(curnt_staus_id.size()!=0) {

                for (int i = 0; i < curnt_staus_id.size(); i++) {

                    preadvicejsonObject = new JSONObject();
                    preadvicejsonObject.put("Type", curnt_staus_id.get(i));
                    currentStausjsonlist.put(preadvicejsonObject);
                }
                        }
                }
                catch (Exception e)
                {

                }
                try
                {
                    if(next_test_id.size()!=0) {
                        for (int i = 0; i < next_test_id.size(); i++) {
                            preadvicejsonObject = new JSONObject();
                            preadvicejsonObject.put("Type", next_test_id.get(i));
                            NextTestjsonlist.put(preadvicejsonObject);
                        }
                    }
                }catch (Exception e)
                {

                }
                    try
                {
                if(dept_id.size()!=0) {

                for (int i = 0; i < dept_id.size(); i++) {
                    preadvicejsonObject = new JSONObject();
                    preadvicejsonObject.put("Type", dept_id.get(i));
                    deoptjsonlist.put(preadvicejsonObject);
                }
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
                jsonObjectStockReportModel.put("Next_Test_Date_From",Nxt_frm_date);
                jsonObjectStockReportModel.put("Next_Test_Date_To", Nxt_to_date);
                jsonObjectStockReportModel.put("Equipment_No", Equip_date);
                jsonObjectStockReportModel.put("EIR_No", EIR_date);
                jsonObjectStockReportModel.put("Cleaning_Date_From",cleaning_frm_date);
                jsonObjectStockReportModel.put("Cleaning_Date_To", cleaning_to_date);
                jsonObjectStockReportModel.put("Inspection_Date_From", Insp_frm_date);
                jsonObjectStockReportModel.put("Inspection_Date_To", Insp_to_date);
                jsonObjectStockReportModel.put("Current_Status_Date_From", current_frm_date);
                jsonObjectStockReportModel.put("In_Date_From", In_date_from);
                jsonObjectStockReportModel.put("In_Date_To", In_date_to);
                jsonObjectStockReportModel.put("Current_Status_Date_To", current_to_date);

                if(Out_date_from=="null" || Out_date_from=="" )
                {
                    jsonObjectStockReportModel.put("Out_Date_From", systemDate);

                }else
                {
                    jsonObjectStockReportModel.put("Out_Date_From", Out_date_from);

                }
                if(Out_date_to=="null" || Out_date_to=="") {
                    jsonObjectStockReportModel.put("Out_Date_To", systemDate);
                }else
                {
                    jsonObjectStockReportModel.put("Out_Date_To", Out_date_to);
                }

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
                shortToast(getApplicationContext(),"No Records Found..!");


            }
            progressDialog.dismiss();

        }

    }

    public class Get_ScockReport_list_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(SelectOptions.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLStock_DropDown);
            httpPost.setHeader("Content-Type", "application/json");

            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));



                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d(" rep", resp);
                Log.d(" req", jsonObject.toString());
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");

                if(selected_name.equalsIgnoreCase("Customer"))
                {
                    jsonarray = getJsonObject.getJSONArray("Customer");
                }else if(selected_name.equalsIgnoreCase("Equipment Type"))
                {
                    jsonarray = getJsonObject.getJSONArray("EquipmentType");
                }else if(selected_name.equalsIgnoreCase("Previous Cargo"))
                {
                    jsonarray = getJsonObject.getJSONArray("PrevoiusCargo");

                }else if(selected_name.equalsIgnoreCase("Current Status")) {
                    jsonarray = getJsonObject.getJSONArray("CurrentStatus");

                }else if(selected_name.equalsIgnoreCase("Next Test Type"))
                {
                    jsonarray = getJsonObject.getJSONArray("NextTestType");

                }else {
                    jsonarray = getJsonObject.getJSONArray("Depot");

                }

                if (jsonarray != null) {

                    System.out.println("Am HashMap list"+jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {

                                products.clear();

                            }
                        });
                    }else {

                        pending_accordion_arraylist = new ArrayList<PendingAccordionBean>();
                        pending_accordion_arraylist.clear();
                        products = new ArrayList<Product_Stock>();
                        products.clear();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            pending_accordion_bean = new PendingAccordionBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            pending_accordion_bean.setValues(jsonObject.getString("Description"));
                            products.add(new Product_Stock(jsonObject.getString("Description"),jsonObject.getString("Id"),false));

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
                          //  no_data.setVisibility(View.VISIBLE);

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



            if(pending_accordion_arraylist!=null)
            {
                boxAdapter = new ListAdapter(SelectOptions.this, products);
                select_list_view.setAdapter(boxAdapter);

             /*   UserListAdapterDropdown adapter = new UserListAdapterDropdown(GateIn.this, R.layout.list_item_row_accordion, pending_accordion_arraylist);
                searchlist.setAdapter(adapter);*/

                searchView.addTextChangedListener(new TextWatcher() {

                    @Override
                    public void afterTextChanged(Editable arg0) {
                        // TODO Auto-generated method stub
                        String text = searchView.getText().toString().toLowerCase(Locale.getDefault());
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
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");

            }

            progressDialog.dismiss();

        }

    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

    }

    public class ListAdapter extends BaseAdapter {
        private final ArrayList<Product_Stock> arraylist;
        Context ctx;
        LayoutInflater lInflater;
        ArrayList<Product_Stock> objects;
        Product_Stock p;

        ListAdapter(Context context, ArrayList<Product_Stock> products) {
            ctx = context;
            objects = products;
            this.arraylist = new ArrayList<Product_Stock>();
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

            ((TextView) view.findViewById(R.id.tv_cust_name)).setText(p.Id+" , "+p.name);


            CheckBox cbBuy = (CheckBox) view.findViewById(R.id.checkbox);
            cbBuy.setOnCheckedChangeListener(myCheckChangList);
            cbBuy.setTag(position);
            cbBuy.setChecked(p.box);

            if(selected_name.equalsIgnoreCase("Customer"))
            {
                if(GlobalConstants.selected_Stock_Cust_Id!=null) {
                    for (int i = 0; i < GlobalConstants.selected_Stock_Cust_Id.size(); i++) {
                        if (p.Id.equalsIgnoreCase(String.valueOf(GlobalConstants.selected_Stock_Cust_Id.get(i)))) {
                            cbBuy.setChecked(true);
                        }
                    }
                }

            }else if(selected_name.equalsIgnoreCase("Equipment Type"))
            {
                if(GlobalConstants.selected_Stock_Equp_Id!=null)
                {
                    for (int i = 0; i < GlobalConstants.selected_Stock_Equp_Id.size(); i++) {
                        if (p.Id.equalsIgnoreCase(String.valueOf(GlobalConstants.selected_Stock_Equp_Id.get(i)))) {
                            cbBuy.setChecked(true);
                        }
                    }
                }
            }else if(selected_name.equalsIgnoreCase("Previous Cargo"))
            {
                if(GlobalConstants.selected_Stock_Prev_Crg_Id!=null)
                {
                    for (int i = 0; i < GlobalConstants.selected_Stock_Prev_Crg_Id.size(); i++) {
                        if (p.Id.equalsIgnoreCase(String.valueOf(GlobalConstants.selected_Stock_Prev_Crg_Id.get(i)))) {
                            cbBuy.setChecked(true);
                        }
                    }
                }

            }else if(selected_name.equalsIgnoreCase("Current Status")) {
                if(GlobalConstants.selected_Stock_Curnt_Status_Id!=null)
                {
                    for (int i = 0; i < GlobalConstants.selected_Stock_Curnt_Status_Id.size(); i++) {
                        if (p.Id.equalsIgnoreCase(String.valueOf(GlobalConstants.selected_Stock_Curnt_Status_Id.get(i)))) {
                            cbBuy.setChecked(true);
                        }
                    }
                }

            }else if(selected_name.equalsIgnoreCase("Next Test Type"))
            {
                if(GlobalConstants.selected_Stock_Nxt_Tst_Type_Id!=null)
                {
                    for (int i = 0; i < GlobalConstants.selected_Stock_Nxt_Tst_Type_Id.size(); i++) {
                        if (p.Id.equalsIgnoreCase(String.valueOf(GlobalConstants.selected_Stock_Nxt_Tst_Type_Id.get(i)))) {
                            cbBuy.setChecked(true);
                        }
                    }

                }
            }else {
                if(GlobalConstants.selected_Stock_Depot_Id!=null)
                {
                    for (int i = 0; i < GlobalConstants.selected_Stock_Depot_Id.size(); i++) {
                        if (p.Id.equalsIgnoreCase(String.valueOf(GlobalConstants.selected_Stock_Depot_Id.get(i)))) {
                            cbBuy.setChecked(true);
                        }
                    }

                }

            }

            return view;
        }

        Product_Stock getProduct(int position) {
            return ((Product_Stock) getItem(position));
        }

        ArrayList<Product_Stock> getBox() {


            box = new ArrayList<Product_Stock>();
            box.clear();
            for (Product_Stock p : objects) {
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
                for (Product_Stock wp : arraylist) {
                    if (wp.name.toLowerCase(Locale.getDefault())
                            .contains(charText)) {
                        objects.add(wp);
                    }
                }
            }
            notifyDataSetChanged();
        }

    }

    protected void onResume(){
        super.onResume();
        selected_id_list.clear();
        selected_equip_id_list.clear();
        selected_prev_id_list.clear();
        selected_curnt_id_list.clear();
        selected_next_id_list.clear();
        selected_depo_id_list.clear();

    }
}
