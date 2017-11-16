package com.i_tankdepo;

import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.FragmentActivity;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.text.Editable;
import android.text.Html;
import android.text.InputFilter;
import android.text.InputType;
import android.text.TextWatcher;
import android.text.method.DigitsKeyListener;
import android.text.method.ScrollingMovementMethod;
import android.util.Base64;
import android.util.Log;
import android.util.SparseBooleanArray;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
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

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.regex.Pattern;

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
public class Repair_Estimation_Approvel_wizard_MySubmit extends CommonActivity implements View.OnClickListener {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView iv_changeOfStatus, add_line_items,menu, im_down, im_up, im_heat_close, im_heat_ok, im_skip,iv_back;


    private TextView tv_toolbarTitle, tv_search_options, no_data;
    LinearLayout LL_hole, LL_heat_submit, LL_search_Value, LL_heat;

    private ProgressDialog progressDialog;
    private ArrayList<String> Invoice_worldlist;
    private ArrayList<CustomerDropdownBean> CustomerDropdown_Invoice_ArrayList;
    private String RepairType_Name,RepairType_Code;
    private Intent mServiceIntent;
    private Button heat_refresh, heat_home, heat_submit, heating, cleaning, inspection;
    private FragmentActivity mycontaxt;
    private TextView customer_name, equipment_no, eqip_type;
    private Button LL_Line_iteam;
    private Button button_fetch, bt_add_details;
    private Button bt_attachments;
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
    private String get_sp_tariffGroup_code="", get_sp_tariff_code="",get_sp_item_code="";
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
    private int totel_amount=0;
    private int original_totel_amount=0;
    private JSONObject jsonObject;
    private ArrayList<RepairBean> repair_arraylist;
    private String line_from;
    private String item,subitem,damage_name,repir_name;
    private String get_sp_damage_code,get_sp_repair_code,get_sp_subitem_code;
    private String add_detail_jsonobject;
    private ArrayList<Image_Bean> multiple_encodeArray;
    private ArrayList<Image_Bean> encodeArray;
    private String customer_Id;
    private RepairBean repair_bean;
    private static final int CustomGallerySelectId = 1;//Set Intent Id
    private TextView repair_estimate_text;
    private Button repair_completion,repair_estimate,repair_approval,survey_completion;
    private RelativeLayout RL_heating,RL_Repair;
    private ImageView more_info;
    private ImageView iv_send_mail;
    private String responsibilitys_cd;
    private String Line_item_Json;
    private Spinner sp_item;
    private String repairType_name;
    private JSONObject jsonrootObject;
    private int Invoice_party;
    private Button notification_text;
    private int count=0;
    int count_i = 0;
    int count_si = 0;
    int count_d = 0;
    int count_r= 0;
    private TextView MHR;
    private ArrayList<String> selected_count;
    private ImageView im_delete;
    private int count_t=0;
    private SpinnerCustomAdapter customAdapter;
    private int invoice_count=0;
    private String ActivityDate;
    private JSONObject print_object;
    String equip_no,From ,Cust_Name,previous_crg,attachmentstatus,gateIn_Id,code,location,cust_code,type_id,code_id,pre_code,pre_id,
            trans_no, vechicle,transport,Eir_no,heating_bt,rental_bt,previous_cargo,filename,remark,type,status,date,time,get_swt_info_rental,get_swt_info_active;
    private String get_mc,get_mh,get_ed_values;
    private int fetch_count=0;
    private Bitmap selectedImageBitmap;
    private String encodedImage;
    private Image_Bean image_bean;
    private ArrayList<Image_Bean> ttach_encodeArray;
    private int aad_totel_amount=0;
    private ArrayList<CustomerDropdownBean> new_Subworldlist;
    private String lable_item;
    private int original_tc_amount=0;
    private String ReportURL;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.repair_estimate_wizard);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

        encodeArray=GlobalConstants.multiple_encodeArray;
        customer_Id=GlobalConstants.cust_Id;
        add_detail_jsonobject=GlobalConstants.add_detail_jsonobject;
        Invoice_party=GlobalConstants.Invoice_party;
        SharedPreferences.Editor editor_ = sp.edit();
        editor_.putString(SP_ADD_LINE_ITEM_JSON, "add_line_item_json");
        editor_.commit();

        add_line_items = (ImageView) findViewById(R.id.add_line_items);
        add_line_items.setOnClickListener(this);
        sp_tarif_group = (CustomSpinner) findViewById(R.id.sp_tarif_group);
        sp_tarif_code = (CustomSpinner) findViewById(R.id.sp_tarif_code);
        list_line_items = (ListView) findViewById(R.id.list_line_items);
        MHR = (TextView) findViewById(R.id.MHR);

        String surName = getColoredSpanned("ManHour Rate-", "#cb0da5");
        try {
            if (GlobalConstants.lobor_rate.equals(null) ||GlobalConstants.lobor_rate.equals("")) {
                MHR.setText(Html.fromHtml(surName + "0.0" + "("+GlobalConstants.currency+")"));

            } else {
                MHR.setText(Html.fromHtml(surName + GlobalConstants.lobor_rate +  "("+GlobalConstants.currency+")"));

            }
        } catch (Exception e) {
            MHR.setText(Html.fromHtml(surName + "0.0"));
        }
        menu = (ImageView) findViewById(R.id.iv_menu);
        tv_amount = (TextView) findViewById(R.id.tv_amount);
        iv_changeOfStatus = (ImageView) findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        iv_changeOfStatus.setVisibility(View.GONE);
        more_info = (ImageView) findViewById(R.id.more_info);
        more_info.setOnClickListener(this);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        im_skip = (ImageView) findViewById(R.id.im_skip);
        iv_back.setOnClickListener(this);
        im_skip.setOnClickListener(this);
        im_delete = (ImageView) findViewById(R.id.im_delete);
        im_delete.setOnClickListener(this);
        tv_amount.setOnClickListener(this);
        menu.setVisibility(View.GONE);
        LL_Line_iteam = (Button) findViewById(R.id.Line_iteam);
        LL_Line = (LinearLayout) findViewById(R.id.LL_Line_iteam);
        LL_Line.setBackgroundColor(getResources().getColor(R.color.submit));
        repair_estimate_text = (TextView)findViewById(R.id.tv_heating);
        repair_estimate_text.setText("Repair Approval");
        bt_add_details = (Button) findViewById(R.id.add_details);
        button_fetch = (Button) findViewById(R.id.button_fetch);
        ed_endDate = (EditText) findViewById(R.id.ed_endDate);

        ed_tariff_code = (EditText) findViewById(R.id.ed_tariff_code);
        ed_tariff_code.setOnClickListener(this);
        ed_endDate.setOnClickListener(this);
        bt_add_details.setOnClickListener(this);
        bt_attachments = (Button) findViewById(R.id.attachments);
        bt_attachments.setOnClickListener(this);
        iv_send_mail = (ImageView) findViewById(R.id.iv_send_mail);
        iv_send_mail.setOnClickListener(this);
        LL_summary = (Button) findViewById(R.id.summary);
        LL_summary.setOnClickListener(this);
        heat_home = (Button) findViewById(R.id.heat_home);
        heat_refresh = (Button) findViewById(R.id.heat_refresh);
        heat_submit = (Button) findViewById(R.id.heat_submit);
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);

        LL_heat_submit.setClickable(false);
        LL_heat_submit.setAlpha(0.5f);

        LL_heat = (LinearLayout) findViewById(R.id.LL_heat);
        button_fetch.setOnClickListener(this);
        LL_heat.setOnClickListener(this);
        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_heating.setVisibility(View.GONE);

        repair_estimate = (Button)findViewById(R.id.repair_estimate);
        repair_estimate.setOnClickListener(this);
        repair_approval = (Button)findViewById(R.id.repair_approval);
        repair_approval.setVisibility(View.GONE);
        repair_completion = (Button)findViewById(R.id.repair_completion);
        repair_completion.setVisibility(View.GONE);
        survey_completion = (Button)findViewById(R.id.survey_completion);
        survey_completion.setVisibility(View.GONE);
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
        if (cd.isConnectingToInternet()) {
            new Tarif_Group_details().execute();
            new ItemAliasSettingsList().execute();
            /*|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                    ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                    ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                    ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")*/
            if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")) {
                iv_send_mail.setVisibility(View.VISIBLE);
                ActivityDate =GlobalConstants.ActivityDate;
                new Get_Repair_MySubmit_details().execute();

            }else if(GlobalConstants.from.equalsIgnoreCase("RepairApprovelPending"))
            {
                notification_text.setText("0");
                new Get_Repair_Pending_details().execute();
                ArrayList<Image_Bean> encodeArray=new ArrayList();
                GlobalConstants.multiple_encodeArray=encodeArray;


            }else {
                ed_endDate.setText(GlobalConstants.tariff_Group);
                ed_tariff_code.setText(GlobalConstants.tariff_Id);
                Line_item_Json=GlobalConstants.Line_item_Json;
                if (!GlobalConstants.Line_item_Json.equals("")||!GlobalConstants.Line_item_Json.equals(null)) {
                    Line_item_Json=GlobalConstants.Line_item_Json;
                    new ItemAliasSettingsList().execute();
                    try{
                        JSONObject jsonObject = new JSONObject();




                        Log.d("rep", Line_item_Json);
                        JSONObject jsonrootObject = new JSONObject(Line_item_Json);
                        object1=jsonrootObject;
                        Log.d("rep_object1", String.valueOf(object1));
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

                                repair_arraylist = new ArrayList<RepairBean>();


                                repair_bean = new RepairBean();



                                if(lineitem_arraylist==null)
                                {
                                    lineitem_arraylist = new ArrayList<LineItem_Bean>();


                                }

                                for (int i = 0; i < jsonarray.length(); i++) {

                                    lineitem_bean = new LineItem_Bean();
                                    jsonObject = jsonarray.getJSONObject(i);


                                    lineitem_bean.setTariff_Code(jsonObject.getString("Tariff_Code"));
                                    lineitem_bean.setTariff_Group(jsonObject.getString("Tariff_Group"));
                                    lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject.getString("RPR_ESTMT_DTL_ID"));
                                    lineitem_bean.setRPR_ESTMT_ID(jsonObject.getString("RPR_ESTMT_ID"));
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
                                    lineitem_bean.setMHR(jsonObject.getString("MHR"));
                                    lineitem_bean.setResponsibility_Cd(jsonObject.getString("ResponsibilityCd"));
                                    lineitem_bean.setResponsibility(jsonObject.getString("Responsibility"));
                                    lineitem_bean.setTotel(jsonObject.getString("TC"));
                                    lineitem_bean.setRemark(jsonObject.getString("DmgRprRemarks"));
                                    lineitem_bean.setRowState(jsonObject.getString("RowState"));


                                    lineitem_arraylist.add(lineitem_bean);
                                    original_totel_amount=original_totel_amount+(int) (Float.parseFloat(jsonObject.getString("TC")));

                                }
                                //Tariff_RepairItemcode_ArrayList,Tariff_DamageItemcode_ArrayList,Tariff_SubItemcode_ArrayList,Tariff_Itemcode_ArrayList
                                GlobalConstants.original_totel_amount=original_totel_amount;
                                new Item_details().execute();
                                adapter = new UserListAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.line_item_row, lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);
                                list_line_items.setAdapter(adapter);
                                tv_amount.setText(String.valueOf(original_totel_amount)+".00"+" "+GlobalConstants.currency);


                            }
                        }else {

                            shortToast(getApplicationContext(),"No Records Found");

                        }

                    } catch (JSONException e) {
                        e.printStackTrace();
                    }



                } else {
                    new Get_Linedetails_details().execute();

                }
            }
        } else {

            shortToast(getApplicationContext(), "Please check your Internet Connection.");

        }

//        tabLayout.setupWithViewPager(viewPager);


        // tabLayout.clearOnTabSelectedListeners();
        sp_tarif_group.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> arg0, View arg1, int i, long arg3) {
                if (count_t != 0) {
                    tariff_group_name = CustomerDropdownArrayList.get(i).getName();
                    get_sp_tariffGroup_code = CustomerDropdownArrayList.get(i).getCode();
                    ed_endDate.setText(tariff_group_name );
                    GlobalConstants.tariff_Group=tariff_group_name;
                    new Tarif_Code_details().execute();

                }
                count_t++;
            }


            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });

        sp_tarif_code.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                if (count != 0) {
                    tariff_code_name = Tariff_code_ArrayList.get(i).getName();
                    get_sp_tariff_code = Tariff_code_ArrayList.get(i).getCode();
                    ed_tariff_code.setText(tariff_code_name);
                    GlobalConstants.tariff_Id=tariff_code_name;

                }
                count++;
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
        customer_name.setText(GlobalConstants.customer_name + " , " +GlobalConstants.type+", "+ GlobalConstants.equipment_no+","+ GlobalConstants.repairEstimateNo);
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
        selected_count=new ArrayList<String>();
        list_line_items.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int pos, long l) {

                TextView textView = (TextView) view.findViewById(R.id.rpe_edt_id_dl);
                selected_count.add(lineitem_arraylist.get(pos).getRPR_ESTMT_DTL_ID());

                Log.i("selected_count", String.valueOf(lineitem_arraylist.get(pos).getMetial_Cost()));
                Log.i("selected_count", String.valueOf(lineitem_arraylist.get(pos).getRPR_ESTMT_DTL_ID()));
                Log.i("selected_count", String.valueOf(lineitem_arraylist.get(pos).getRPR_ESTMT_ID()));
                Log.i("selected_count",textView.getText().toString());
            }
        });

        /*list_line_items.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE_MODAL);
        // Capture ListView item click
        list_line_items.setMultiChoiceModeListener(new AbsListView.MultiChoiceModeListener() {
            @Override
            public void onItemCheckedStateChanged(ActionMode mode,
                                                  int position, long id, boolean checked) {
                // Capture total checked items
                final int checkedCount = list_line_items.getCheckedItemCount();
                // Set the CAB title according to total checked items
                mode.setTitle(checkedCount + " Selected");
                // Calls toggleSelection method from ListViewAdapter Class
                adapter.toggleSelection(position);
            }

            @Override
            public boolean onActionItemClicked(ActionMode mode, MenuItem item) {
                switch (item.getItemId()) {
                    case R.id.delete:
                        // Calls getSelectedIds method from ListViewAdapter Class
                        SparseBooleanArray selected = adapter
                                .getSelectedIds();
                        Log.i("selected", String.valueOf(selected));
                        // Captures all selected ids with a loop
                        for (int i = (selected.size() - 1); i >= 0; i--) {
                            if (selected.valueAt(i)) {
                                LineItem_Bean selecteditem = adapter
                                        .getItem(selected.keyAt(i));
                                // Remove selected items following the ids
//                                String id=selecteditem.getRPR_ESTMT_ID();
                                String id=selecteditem.getRPR_ESTMT_DTL_ID();
                                GlobalConstants.Line_item_id=id;
                                try
                                {
                                    if(GlobalConstants.Line_item_id.equals(null)||GlobalConstants.Line_item_id.equals(""))
                                    {
                                        GlobalConstants.Line_item_Json="";
                                        finish();
                                        startActivity(getIntent());
                                    }else
                                    {
                                        new Delete_Line_itens().execute();

                                    }
                                }catch (Exception e)
                                {
                                    GlobalConstants.Line_item_Json="";
                                    finish();
                                    startActivity(getIntent());
                                }
                            }
                        }
                        // Close CAB
                        mode.finish();
                        return true;

                    default:
                        return false;
                }
            }

            @Override
            public boolean onCreateActionMode(ActionMode mode, Menu menu) {
                mode.getMenuInflater().inflate(R.menu.multiple_delete, menu);
                return true;
            }

            @Override
            public void onDestroyActionMode(ActionMode mode) {
                // TODO Auto-generated method stub
                adapter.removeSelection();
            }

            @Override
            public boolean onPrepareActionMode(ActionMode mode, Menu menu) {
                // TODO Auto-generated method stub
                return false;
            }
        });


*/
    }


    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(), MainActivity.class));
                break;
            case R.id.LL_heat:
                startActivity(new Intent(getApplicationContext(), Repair_MainActivity.class));
                break;
            case R.id.im_delete:


                if(selected_count.size()!=0)
                {
                    HashSet hs = new HashSet();
                    hs.addAll(selected_count);
                    selected_count.clear();
                    selected_count.addAll(hs);
                    for(int i=0;i<selected_count.size();i++)
                    {
                        GlobalConstants.Line_item_id = selected_count.get(i);
                        Log.i("Line_item_id",selected_count.get(i));
                        new Delete_Line_itens().execute();
                    }

                }else {
                    shortToast(getApplicationContext(),"Please select atleat one line item");
                }

                break;
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.more_info:

                popUp_equipment_info(GlobalConstants.equipment_no,GlobalConstants.status,GlobalConstants.status_id,GlobalConstants.previous_cargo,"","","","");


                break;
            case R.id.iv_send_mail:

                if (cd.isConnectingToInternet()) {

                    // Survey Completion
                    // Repair Approval
                    new Print_pdf().execute();

                }else{
                    shortToast(getApplicationContext(), "Please check your Internet Connection.");
                }

/*
                try {
                    String targetPdf = "/sdcard/test.pdf";

                    String sendemail = "";
                    Intent email = new Intent(Intent.ACTION_SEND);
                    email.putExtra(Intent.EXTRA_EMAIL, new String[]{ sendemail});
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
*/

                break;
            case R.id.repair_estimate:
                finish();
                 encodeArray=new ArrayList();
                GlobalConstants.multiple_encodeArray=encodeArray;
                startActivity(new Intent(getApplicationContext(),Repair_MainActivity.class));
                break;
            case R.id.add_line_items:
                count = 0;
                invoice_count = 0;
                count_t = 0;
                count_i = 0;
                count_si = 0;
                count_d = 0;
                count_r= 0;
                fetch_count= 0;
                line_from = "add_line";
                try {
                    if(sp.getString(SP_ADD_LINE_ITEM_JSON,"add_line_item_json").equalsIgnoreCase("add_line_item_json") ||
                            sp.getString(SP_ADD_LINE_ITEM_JSON,"add_line_item_json").equals(""))
                    {
                        new Get_Repair_MySubmit_details().execute();

                    }else {


                        jsonrootObject = new JSONObject(sp.getString(SP_ADD_LINE_ITEM_JSON, "add_line_item_json"));
                        System.out.println("jsonrootObject" + jsonrootObject);

                        JSONObject getJsonObject = jsonrootObject.getJSONObject("d");
                        jsonarray = getJsonObject.getJSONArray("Response");
                        if (jsonarray != null) {

                            System.out.println("Am HashMap list" + jsonarray);
                            if (jsonarray.length() < 1) {
                                popUp_add("Add LineItem", lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);

                            } else {
                                lineitem_arraylist =new ArrayList<LineItem_Bean>();
//                                lineitem_arraylist.removeAll(lineitem_arraylist);
                                for (int j = 0; j < jsonarray.length(); j++) {

                                    repair_bean = new RepairBean();
                                    jsonObject = jsonarray.getJSONObject(j);


                                    lineitem_bean = new LineItem_Bean();
                                    JSONObject jsonObject_line = jsonarray.getJSONObject(j);

                                    if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                            ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                            ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                            ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                                        if(jsonObject_line.getString("RPR_ESTMT_DTL_ID").equals(""))
                                        {
                                            lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject_line.getString("1"));

                                        }else
                                        {
                                            lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject_line.getString("RPR_ESTMT_DTL_ID"));

                                        }
                                        if(jsonObject_line.getString("RPR_ESTMT_ID").equals(""))
                                        {
                                            lineitem_bean.setRPR_ESTMT_ID(jsonObject_line.getString("1"));

                                        }else
                                        {
                                            lineitem_bean.setRPR_ESTMT_ID(jsonObject_line.getString("RPR_ESTMT_ID"));

                                        }

                                    } else {
                                        lineitem_bean.setRPR_ESTMT_DTL_ID("1");
                                        lineitem_bean.setRPR_ESTMT_ID("1");
                                    }


                                    lineitem_bean.setTariff_Code(jsonObject_line.getString("Tariff_Code"));
                                    lineitem_bean.setTariff_Group(jsonObject_line.getString("Tariff_Group"));
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
                                    totel_amount = totel_amount + (int) (Float.parseFloat(jsonObject.getString("TC")));
                                    lineitem_arraylist.add(lineitem_bean);
                                }
                                System.out.println("lineitem_arraylist" + lineitem_arraylist.size());
                                popUp_add("Add LineItem", lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);

                            }
                        }
                    }

                } catch (JSONException e) {
                    e.printStackTrace();
                }

//                new Get_Repair_MySubmit_details().execute();

                break;
            case R.id.add_details:
                try {
                   /* if (object1.equals(null) || object1.equals("") || object1.equals("null")) {
                        shortToast(getApplicationContext(), "Please select Atleast One LineItem");
                    } else {*/
                        if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                            //   repair_arraylist=GlobalConstants.repair_arraylist;
                            //  new Get_Repair_MySubmit_details().execute();
                            GlobalConstants.ActivityDate=ActivityDate;
                            GlobalConstants.from = "MysubmitlineItem";
                            String count=GlobalConstants.attach_count;
                            GlobalConstants.attach_count=count;

                        } else if(GlobalConstants.from.equalsIgnoreCase("RepairApprovelPending")) {
                            GlobalConstants.from = "PendinglineItem";
                        }else {
                            GlobalConstants.from = "";
                        }
                        SharedPreferences.Editor editor = sp.edit();
                        editor.putString(SP_LINE_ITEM_JSON, String.valueOf(object1));
                        editor.commit();
                        String equipment_no = GlobalConstants.equipment_no;
                        GlobalConstants.equipment_no = equipment_no;
                        String amount=tv_amount.getText().toString();
                        GlobalConstants.totale_amount = amount;
                        GlobalConstants.multiple_encodeArray=encodeArray;
                        GlobalConstants.Line_item_Json=String.valueOf(object1);
                        GlobalConstants.customer_Id=customer_Id;
                        String date=GlobalConstants.indate;
                        String est_id=GlobalConstants.repair_EstimateID;
                        String est_no=GlobalConstants.repairEstimateNo;
                        String reff_no=GlobalConstants.Cust_AppRef;
                        GlobalConstants.Cust_AppRef=reff_no;
                        GlobalConstants.Invoice_party=Invoice_party;
                        String gi_transaction_no= GlobalConstants.gi_trans_no;
                        GlobalConstants.repair_EstimateID=est_id;

                        GlobalConstants.original_totel_amount=original_totel_amount;
                        GlobalConstants.repair_EstimateNo=est_no;
                        GlobalConstants.gi_trans_no=gi_transaction_no;
                        GlobalConstants.indate=date;
                        String last_testDate=GlobalConstants.last_testDate;
                        GlobalConstants.last_testDate=last_testDate;
                        String next_testDate = GlobalConstants.next_testDate;
                        GlobalConstants.next_testDate = next_testDate;
                        String lastSurveyor = GlobalConstants.lastSurveyor;
                        GlobalConstants.lastSurveyor = lastSurveyor;
                    GlobalConstants.add_detail_jsonobject=add_detail_jsonobject;
                        startActivity(new Intent(getApplicationContext(), AddRepair_Approvel_Details_MySubmit.class));
//                    }
                }catch (Exception e)
                {
                    e.printStackTrace();
                    Log.i("e", String.valueOf(e));
                }

                break;
            case R.id.im_skip:
                bt_add_details.performClick();
                break;
            case R.id.attachments:
                try {
                   /* if (object1.equals(null) || object1.equals("") || object1.equals("null")) {
                        shortToast(getApplicationContext(), "Please select Atleast One LineItem");
                    } else {*/
                        if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                            String count=GlobalConstants.attach_count;
                            GlobalConstants.attach_count=count;
                            GlobalConstants.ActivityDate=ActivityDate;
                            GlobalConstants.from = "MysubmitlineItem";

                        } else if(GlobalConstants.from.equalsIgnoreCase("RepairApprovelPending")) {
                            GlobalConstants.from = "PendinglineItem";
                        }else {
                            GlobalConstants.from = "";
                        }
                        SharedPreferences.Editor editor = sp.edit();
                        editor.putString(SP_LINE_ITEM_JSON, String.valueOf(object1));
                        editor.commit();
                        finish();
                        String equipment_no = GlobalConstants.equipment_no;
                        GlobalConstants.equipment_no = equipment_no;
                        String amount=tv_amount.getText().toString();
                        GlobalConstants.totale_amount = amount;
                        GlobalConstants.multiple_encodeArray=encodeArray;
                        GlobalConstants.Line_item_Json=String.valueOf(object1);
                        GlobalConstants.customer_Id=customer_Id;
                        String date=GlobalConstants.indate;
                        String est_id=GlobalConstants.repair_EstimateID;
                        String est_no=GlobalConstants.repairEstimateNo;
                        String gi_transaction_no= GlobalConstants.gi_trans_no;
                        GlobalConstants.repair_EstimateID=est_id;
                        String reff_no=GlobalConstants.Cust_AppRef;
                        GlobalConstants.Cust_AppRef=reff_no;
                        String last_testDate=GlobalConstants.last_testDate;
                        GlobalConstants.last_testDate=last_testDate;
                        GlobalConstants.repair_EstimateNo=est_no;
                        GlobalConstants.gi_trans_no=gi_transaction_no;
                        GlobalConstants.indate=date;
                    String next_testDate = GlobalConstants.next_testDate;
                    GlobalConstants.next_testDate = next_testDate;
                    String lastSurveyor = GlobalConstants.lastSurveyor;
                    GlobalConstants.lastSurveyor = lastSurveyor;
                        GlobalConstants.add_detail_jsonobject=add_detail_jsonobject;
                        startActivity(new Intent(getApplicationContext(), Attach_Repair_Approvel_MySubmit.class));
//                    }
                }catch (Exception e)
                {
                    e.printStackTrace();
                    Log.i("e", String.valueOf(e));
                }
                break;
            case R.id.button_fetch:

                if (cd.isConnectingToInternet()) {


                    String tariff_grp = ed_endDate.getText().toString();
                    String tariff_code = ed_tariff_code.getText().toString();
                    if (tariff_grp.equals("") || tariff_code.equals("")) {
                        shortToast(getApplicationContext(), "Please Select The Tariff Code & Tariff Group");
                    } else {
                        line_from = "fetch_line";
                        new Get_Linedetails_Fetch().execute();

                    }

                } else {
                    shortToast(getApplicationContext(), "Please Check Your InterNet Connection");
                }
                break;
            case R.id.summary:

                try {
                   /* if (object1.equals(null) || object1.equals("") || object1.equals("null")) {

                        shortToast(getApplicationContext(), "Please select Atleast One LineItem");
                    } else {*/
                        if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                            //   repair_arraylist=GlobalConstants.repair_arraylist;
                            //  new Get_Repair_MySubmit_details().execute();
                            String count=GlobalConstants.attach_count;
                            GlobalConstants.attach_count=count;
                            GlobalConstants.from = "MysubmitlineItem";
                            GlobalConstants.ActivityDate=ActivityDate;

                        } else {
                            GlobalConstants.from = "PendinglineItem";
                        }

                        SharedPreferences.Editor editor = sp.edit();
                        editor.putString(SP_LINE_ITEM_JSON, String.valueOf(object1));
                        editor.commit();
                        String est_id=GlobalConstants.repair_EstimateID;
                        String est_no=GlobalConstants.repairEstimateNo;
                        String gi_transaction_no= GlobalConstants.gi_trans_no;
                        GlobalConstants.repair_EstimateID=est_id;
                        GlobalConstants.repair_EstimateNo=est_no;
                        GlobalConstants.gi_trans_no=gi_transaction_no;
                        String equipment_no = GlobalConstants.equipment_no;
                        GlobalConstants.equipment_no = equipment_no;
                        String amount=tv_amount.getText().toString();
                        GlobalConstants.totale_amount = amount;
                         GlobalConstants.original_totel_amount=original_totel_amount;
                        String reff_no=GlobalConstants.Cust_AppRef;
                        GlobalConstants.Cust_AppRef=reff_no;
                        String last_testDate=GlobalConstants.last_testDate;
                        GlobalConstants.last_testDate=last_testDate;
                        GlobalConstants.multiple_encodeArray=encodeArray;
                        String date=GlobalConstants.indate;
                        GlobalConstants.indate=date;
                        Log.i("submit_line",String.valueOf(object1));
                        GlobalConstants.Line_item_Json=String.valueOf(object1);
                        GlobalConstants.customer_Id=customer_Id;
                    String next_testDate = GlobalConstants.next_testDate;
                    GlobalConstants.next_testDate = next_testDate;
                    String lastSurveyor = GlobalConstants.lastSurveyor;
                    GlobalConstants.lastSurveyor = lastSurveyor;
                        GlobalConstants.add_detail_jsonobject=add_detail_jsonobject;
                        startActivity(new Intent(getApplicationContext(), Repair_Approvel_Summary_MySubmit.class));
//                    }
                }catch (Exception e)
                {
                    e.printStackTrace();
                    Log.i("e", String.valueOf(e));
                }
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
                ArrayList<Image_Bean> encodeArray=new ArrayList();
                GlobalConstants.multiple_encodeArray=encodeArray;
                onBackPressed();
                break;
            case R.id.iv_changeOfStatus:
                GlobalConstants.Line_item_Json= String.valueOf(object1);
                startActivity(new Intent(getApplicationContext(), ChangeOfStatus.class));

                break;


        }

    }
    @Override
    public void onResume(){
        super.onResume();
        // put your code here...
        if(lineitem_arraylist==null)
        {
            lineitem_arraylist = new ArrayList<LineItem_Bean>();


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
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
                                shortToast(getApplicationContext(), "No Records Found.");
                            }
                        });
                    } else {

                        CustomerDropdownArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);
                            customer_DropdownBean.setName(jsonObject.getString("TRFF_GRP_CD"));
                            customer_DropdownBean.setCode(jsonObject.getString("TRFF_GRP_ID"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("TRFF_GRP_DESCRPTN_VC"));
                            CustomerDropdownArrayList.add(customer_DropdownBean);


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


            if (CustomerDropdownArrayList != null) {
                customAdapter = new SpinnerCustomAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.spinner_text, CustomerDropdownArrayList);
                customAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_tarif_group.setAdapter(customAdapter);
            } else {


            }


        }

    }

    public class Tarif_Code_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
                                ed_tariff_code.setText("");
                                GlobalConstants.tariff_Id="";
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
                    customAdapter = new SpinnerCustomAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.spinner_text, Tariff_code_ArrayList);
                    customAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    // Set adapter to spinner
                    sp_tarif_code.setAdapter(customAdapter);
                } else {
                    shortToast(getApplicationContext(), "Data Not Found");

                }
            }else
            {
                Tariff_code_ArrayList.clear();
            }

        }

    }

    public class Get_Linedetails_details  extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONObject jsonrootObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
            if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.show();

        }

        @Override
        protected Void doInBackground(Void... params) {


            try {


                Log.d("json_string_SP", GlobalConstants.Line_item_Json);



                JSONObject jsonrootObject = new JSONObject(GlobalConstants.Line_item_Json);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");
                jsonarray = getJsonObject.getJSONArray("MenuItems");
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
                            lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject.getString("RPR_ESTMT_DTL_ID"));
                            lineitem_bean.setRPR_ESTMT_ID(jsonObject.getString("RPR_ESTMT_ID"));
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
                            lineitem_bean.setMHR(jsonObject.getString("MHR"));
                            lineitem_bean.setMetial_Cost(jsonObject.getString("MC"));
                            lineitem_bean.setResponsibility_Cd(jsonObject.getString("ResponsibilityCd"));
                            lineitem_bean.setResponsibility(jsonObject.getString("Responsibility"));
                            lineitem_bean.setTotel(jsonObject.getString("TC"));
                            lineitem_bean.setRemark(jsonObject.getString("DmgRprRemarks"));
                            lineitem_bean.setRowState(jsonObject.getString("RowState"));

                            lineitem_arraylist.add(lineitem_bean);
                            totel_amount=totel_amount+(int) (Float.parseFloat(jsonObject.getString("TC")));


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


            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {

            if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();
            tv_amount.setText(String.valueOf(totel_amount)+".00"+" "+GlobalConstants.currency);

            if (lineitem_arraylist != null) {

                new Item_details().execute();


            }

        }

    }
    public class Get_Linedetails_Fetch extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONObject jsonrootObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
                jsonObject.put("UserName",sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("TariffId", get_sp_tariff_code);
                jsonObject.put("TariffGroupId", get_sp_tariffGroup_code);
                jsonObject.put("bv_strEstimateId", GlobalConstants.repairEstimateId);
                jsonObject.put("NextIndex", "");
                jsonObject.put("Mode", "new");
                jsonObject.put("LaborRate", GlobalConstants.lobor_rate);

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                Log.d("Request", jsonObject.toString());



                jsonrootObject = new JSONObject(resp);


                object1=jsonrootObject;

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
                            lineitem_bean.setTariff_Code( tariff_code_name+"("+get_sp_tariff_code+")");
                            lineitem_bean.setTariff_Group(tariff_group_name+"("+get_sp_tariffGroup_code+")");
                            lineitem_bean.setItem(jsonObject.getString("Item"));
                            if(jsonObject.getString("RPR_ESTMT_DTL_ID").equals(""))
                            {
                                lineitem_bean.setRPR_ESTMT_DTL_ID("1");

                            }else
                            {
                                lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject.getString("RPR_ESTMT_DTL_ID"));

                            }
                            if(jsonObject.getString("RPR_ESTMT_ID").equals(""))
                            {
                                lineitem_bean.setRPR_ESTMT_ID("1");

                            }else
                            {
                                lineitem_bean.setRPR_ESTMT_ID(jsonObject.getString("RPR_ESTMT_ID"));

                            }

                            lineitem_bean.setItemCode(jsonObject.getString("Item"));
                            lineitem_bean.setRepair_Code(jsonObject.getString("RepairCd"));
                            lineitem_bean.setRepair_Code_Id(jsonObject.getString("Repair"));
                            lineitem_bean.setSubItem(jsonObject.getString("SubItemCd"));
                            lineitem_bean.setSuIitemCode(jsonObject.getString("SubItem"));
                            lineitem_bean.setDamage_Code(jsonObject.getString("DamageCd"));
                            lineitem_bean.setDamage_Code_Id(jsonObject.getString("Damage"));
                            lineitem_bean.setManHour(jsonObject.getString("ManHour"));

                            Float litersOfPetrol=Float.parseFloat(jsonObject.getString("MHC"));
                            DecimalFormat df = new DecimalFormat("0.00");
                            df.setMaximumFractionDigits(2);
                            lineitem_bean.setManHour_Cost(df.format(litersOfPetrol));
                            lineitem_bean.setMetial_Cost(jsonObject.getString("MC"));
                            lineitem_bean.setMHR(jsonObject.getString("MHR"));
                            lineitem_bean.setResponsibility_Cd(jsonObject.getString("ResponsibilityCd"));
                            lineitem_bean.setResponsibility(jsonObject.getString("Responsibility"));
                            Float litersOfPetrol_TC=Float.parseFloat(jsonObject.getString("TC"));
                            DecimalFormat df_TC = new DecimalFormat("0.00");
                            df_TC.setMaximumFractionDigits(2);
                            lineitem_bean.setTotel(df.format(litersOfPetrol_TC));
                            lineitem_bean.setRemark(jsonObject.getString("DmgRprRemarks"));
                            lineitem_bean.setRowState("Added");

                            lineitem_arraylist.add(lineitem_bean);
                            totel_amount = totel_amount + (int)(Float.parseFloat(jsonObject.getString("TC")));


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

            if ((progressDialog != null) && progressDialog.isShowing())
                progressDialog.dismiss();
            tv_amount.setText(String.valueOf(totel_amount)+".00"+" "+GlobalConstants.currency);
            LL_summary.setVisibility(View.GONE);

            if (lineitem_arraylist != null) {

                try {


                    if(fetch_count>0)
                    {



                        object1 = new JSONObject();
                        JSONObject object_d = new JSONObject();
                        JSONArray json_array = new JSONArray();
                        for (int i = 0; i < lineitem_arraylist.size(); i++) {

                            if (lineitem_arraylist.get(i).getResponsibility_Cd().equalsIgnoreCase("I")) {
                                Invoice_party = Invoice_party + 1;
                            }
                            object2 = new JSONObject();
                            object2.put("RPR_ESTMT_DTL_ID", lineitem_arraylist.get(i).getRPR_ESTMT_DTL_ID());
                            object2.put("RPR_ESTMT_ID", lineitem_arraylist.get(i).getRPR_ESTMT_ID());
                            object2.put("Tariff_Code", lineitem_arraylist.get(i).getTariff_Code());
                            object2.put("Tariff_Group", lineitem_arraylist.get(i).getTariff_Group());
                            object2.put("ItemCd", lineitem_arraylist.get(i).getItem());
                            object2.put("Item", lineitem_arraylist.get(i).getItem());
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
                            if(lineitem_arraylist.get(i).getRPR_ESTMT_DTL_ID().length()>1)
                            {
                                object2.put("RowState", "Modified");
                            }else {
                                object2.put("RowState", "Added");
                            }
                            json_array.put(object2);
                        }
                        object_d.put("Response", json_array);
                        object1.put("d", object_d);
                        GlobalConstants.Line_item_Json= String.valueOf(object1);
                        GlobalConstants.lineItem_beanArrayList= lineitem_arraylist;
                        Log.i("object1", String.valueOf(object1));

                        SharedPreferences.Editor editor = sp.edit();
                        editor.putString(SP_ADD_LINE_ITEM_JSON, String.valueOf(object1));
                        editor.commit();
                    }else
                    {

                        object1 = new JSONObject();
                        JSONObject object_d = new JSONObject();
                        JSONArray json_array = new JSONArray();
                        for (int i = 0; i < lineitem_arraylist.size(); i++) {

                            if (lineitem_arraylist.get(i).getResponsibility_Cd().equalsIgnoreCase("I")) {
                                Invoice_party = Invoice_party + 1;
                            }
                            object2 = new JSONObject();
                            object2.put("RPR_ESTMT_DTL_ID", lineitem_arraylist.get(i).getRPR_ESTMT_DTL_ID());
                            object2.put("RPR_ESTMT_ID", lineitem_arraylist.get(i).getRPR_ESTMT_ID());
                            object2.put("Tariff_Code", lineitem_arraylist.get(i).getTariff_Code());
                            object2.put("Tariff_Group", lineitem_arraylist.get(i).getTariff_Group());
                            object2.put("ItemCd", lineitem_arraylist.get(i).getItem());
                            object2.put("Item", lineitem_arraylist.get(i).getItem());
                            object2.put("RepairCd", lineitem_arraylist.get(i).getRepair_Code());
                            object2.put("Repair", lineitem_arraylist.get(i).getRepair_Code_Id());
                            object2.put("SubItemCd", lineitem_arraylist.get(i).getSuIitemCode());
                            object2.put("SubItem", lineitem_arraylist.get(i).getSubItem());
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
                            if(lineitem_arraylist.get(i).getRPR_ESTMT_DTL_ID().length()>1)
                            {
                                object2.put("RowState", "Modified");
                            }else {
                                object2.put("RowState", "Added");
                            }
                            json_array.put(object2);
                        }
                        object_d.put("Response", json_array);
                        object1.put("d", object_d);
                        GlobalConstants.Line_item_Json= String.valueOf(object1);
                        GlobalConstants.lineItem_beanArrayList= lineitem_arraylist;
                        Log.i("object1", String.valueOf(object1));

                        SharedPreferences.Editor editor = sp.edit();
                        editor.putString(SP_ADD_LINE_ITEM_JSON, String.valueOf(object1));
                        editor.commit();
                    }

                }catch (Exception e)
                {
                    Log.i("Exception", String.valueOf(e));
                }


                new Item_details().execute();


            }else
            {

            }

        }

    }

    public class UserListAdapter extends ArrayAdapter<LineItem_Bean> {


        LayoutInflater inflater;
        ArrayList<LineItem_Bean> arraylist;
        SparseBooleanArray mSelectedItemsIds;
        private ArrayList<LineItem_Bean> list;
        ArrayList<CustomerDropdownBean> worldlist;
        Context context;
        ArrayList<CustomerDropdownBean> Subworldlist;
        ArrayList<CustomerDropdownBean> damageworldlist;
        ArrayList<CustomerDropdownBean> repairworldlist;
        int resource;
        private LineItem_Bean userListBean;
        int lastPosition = -1;
        private boolean isCamPermission;
        private String userChoosenTask;

        public UserListAdapter(Context context, int resourceId,
                               ArrayList<LineItem_Bean> list,
                               ArrayList<CustomerDropdownBean> worldlist, ArrayList<CustomerDropdownBean> Subworldlist,
                               ArrayList<CustomerDropdownBean> damageworldlist, ArrayList<CustomerDropdownBean> repairworldlist) {
            super(context, resourceId, list);
            mSelectedItemsIds = new SparseBooleanArray();
            this.list = list;
            this.resource = resource;
            this.worldlist = worldlist;
            this.Subworldlist = Subworldlist;
            this.damageworldlist = damageworldlist;
            this.repairworldlist = repairworldlist;
            inflater = LayoutInflater.from(context);
        }
       /* public UserListAdapter(Context context, int resource, ) {
            this.context = context;
            this.list = list;
            this.resource = resource;
            this.worldlist = worldlist;
            this.Subworldlist = Subworldlist;
            this.damageworldlist = damageworldlist;
            this.repairworldlist = repairworldlist;
            this.arraylist = new ArrayList<LineItem_Bean>();
            this.arraylist.addAll(list);
        }*/


        @Override
        public int getCount() {
            return list.size();
        }


        @Override
        public long getItemId(int position) {
            return position;
        }

        @Override
        public View getView(final int position, View convertView, ViewGroup parent) {
            if (convertView == null) {
                // LayoutInflater inflater = LayoutInflater.from(context);
                convertView = inflater.inflate(R.layout.line_item_row, null);
                //  convertView = inflater.inflate(resource, null);
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
                holder.rpe_edt_id = (TextView) convertView.findViewById(R.id.rpe_edt_id);
                holder.rpe_edt_id_dl = (TextView) convertView.findViewById(R.id.rpe_edt_id_dl);
                holder.MHR = (TextView) convertView.findViewById(R.id.MHR);
                holder.tv_responsibility_id = (TextView) convertView.findViewById(R.id.tv_responsibility_id);
                holder.totelcost = (TextView) convertView.findViewById(R.id.tv_totalCost);
                holder.remark = (TextView) convertView.findViewById(R.id.tv_remark);
                holder.line_item_id = (TextView) convertView.findViewById(R.id.line_item_id);
                holder.id_attachment = (ImageView) convertView.findViewById(R.id.id_attachment);
                holder.tv_lable_item = (TextView) convertView.findViewById(R.id.tv_lable_item);


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
                try {
                    if (userListBean.getTariff_Group().equals(null) || userListBean.getTariff_Code().equals(null)) {
                        holder.tariffGroup.setText(Html.fromHtml(group + ""));
                        holder.tariff.setText(Html.fromHtml(tariff + ""));
                    } else {
                        String surName_group = getColoredSpanned(userListBean.getTariff_Group(), "#cb0da5");
                        String surName_tariff = getColoredSpanned(userListBean.getTariff_Code(), "#cb0da5");
                        holder.tariffGroup.setText(Html.fromHtml(group + surName_group));
                        holder.tariff.setText(Html.fromHtml(tariff + surName_tariff));
                    }
                }catch (Exception e)
                {
                    holder.tariffGroup.setText(Html.fromHtml(group + ""));
                    holder.tariff.setText(Html.fromHtml(tariff + ""));
                }


//                holder.tariffGroup.setText(userListBean.getTariff_Group());
                holder.item.setText(userListBean.getItemCode());
                holder.subitem.setText(userListBean.getSuIitemCode());
                holder.manhour.setText(userListBean.getManHour());
                holder.manhourcost.setText(userListBean.getManHour_Cost());
                holder.metrialcost.setText(userListBean.getMetial_Cost());
                holder.remark.setText(userListBean.getRemark());
                holder.totelcost.setText(userListBean.getTotel());




                String ManHour = getColoredSpanned("ManHour Rate-", "#cb0da5");
//                holder.MHR.setText("ManHour Rate-"+userListBean.getMHR());
                String surName = getColoredSpanned("ManHour Rate-", "#d91e18");
                try {
                    if (userListBean.getMHR().equals(null) || userListBean.getMHR().equals("")) {
                        holder.MHR.setText(Html.fromHtml(surName + "0.0" + "("+GlobalConstants.currency+")"));

                    } else {
                        holder.MHR.setText(Html.fromHtml(surName + userListBean.getMHR() +  "("+GlobalConstants.currency+")"));

                    }
                    if(lable_item.equals("")||lable_item.equals("null")|| lable_item.equals(null))
                    {
                        holder.tv_lable_item.setText("Item");
                    }else
                    {
                        holder.tv_lable_item.setText(lable_item);
                    }


                } catch (Exception e) {
                    holder.MHR.setText(Html.fromHtml(surName + "0.0"));
                    holder.tv_lable_item.setText(sp.getString(SP_ITEM_LABLE,"lable_item"));
                }

                holder.damagecode.setText(userListBean.getDamage_Code_Id());
                holder.repaircode.setText(userListBean.getRepair_Code());
                holder.responsibility.setText(userListBean.getResponsibility_Cd());
                holder.rpe_edt_id_dl.setText(userListBean.getRPR_ESTMT_DTL_ID());
                holder.rpe_edt_id.setText(userListBean.getRPR_ESTMT_ID());
                holder.tv_responsibility_id.setText(userListBean.getResponsibility());


                Log.i("getRowState",userListBean.getRowState());
                if(userListBean.getRowState().equalsIgnoreCase("Deleted"))
                {
                    holder.id_attachment.setAlpha(0.5f);
                }



                holder.item.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        //  holder.sp_item.performClick();
                        //Tariff_RepairItemcode_ArrayList,Tariff_DamageItemcode_ArrayList,Tariff_SubItemcode_ArrayList,Tariff_Itemcode_ArrayList
                        popUp("Item",list.get(position).getItem(),list.get(position),list ,Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);


                    }
                });
                holder.subitem.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        //  holder.sp_item.performClick();
                        new_Subworldlist=new ArrayList<CustomerDropdownBean>();
                        for(int j=0;j<Subworldlist.size();j++)
                        {
                            if(list.get(position).getItem().equalsIgnoreCase(Subworldlist.get(j).getItem_Id()))
                            {
                                customer_DropdownBean = new CustomerDropdownBean();


                                customer_DropdownBean.setName(Subworldlist.get(j).getName());
                                customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(Subworldlist.get(j).getTRFF_CD_DESCRPTN_VC());
                                customer_DropdownBean.setCode(Subworldlist.get(j).getCode());
                                customer_DropdownBean.setItem_Id(Subworldlist.get(j).getItem_Id());
                                customer_DropdownBean.setSub_item_Id(Subworldlist.get(j).getSub_item_Id());

                                new_Subworldlist.add(customer_DropdownBean);
                            }

                        }

                        popUp("SubItem",  list.get(position).getSuIitemCode(),list.get(position), list,worldlist, new_Subworldlist, damageworldlist, repairworldlist);


                    }
                });
                holder.damagecode.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        //  holder.sp_item.performClick();
                        popUp("Damage", list.get(position).getDamage_Code_Id(),list.get(position),list ,worldlist, Subworldlist, damageworldlist, repairworldlist);


                    }
                });
                holder.repaircode.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        //  holder.sp_item.performClick();
                        popUp("Repair",list.get(position).getRepair_Code_Id(),list.get(position), list,worldlist, Subworldlist, damageworldlist, repairworldlist);


                    }
                });


                holder.manhour.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        popUp("ManHour",list.get(position).getManHour(),list.get(position), list,worldlist, Subworldlist, damageworldlist, repairworldlist);
                    }
                });
                holder.metrialcost.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        popUp("Material Cost",list.get(position).getMetial_Cost(),list.get(position), list,worldlist, Subworldlist, damageworldlist, repairworldlist);
                    }
                });
                holder.responsibility.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        popUp("Responsibility",list.get(position).getResponsibility_Cd().toString(),list.get(position), list,worldlist, Subworldlist, damageworldlist, repairworldlist);
                    }
                });
                holder.remark.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        popUp("Remark",list.get(position).getRemark(),list.get(position),list ,worldlist, Subworldlist, damageworldlist, repairworldlist);
                    }
                });

                holder.whole.setOnLongClickListener(new View.OnLongClickListener() {
                    @Override
                    public boolean onLongClick(View view) {
                        return false;
                    }

                });

                holder.id_attachment.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {

                        try {
                            if (list.get(position).getRowState().equalsIgnoreCase("Added")) {

                                lineitem_arraylist.remove(list.get(position));
                            } else if (list.get(position).getRowState().equalsIgnoreCase("Deleted")) {
                                list.get(position).setRowState("Modified");
                            } else if (list.get(position).getRowState().equalsIgnoreCase("Modified")) {
                                list.get(position).setRowState("Deleted");
                            }
                        }catch ( Exception e)
                        {

                        }
                        lineitem_arraylist=list;
//
//                        selected_count_pos.add(String.valueOf(list.get(position)));
                      /*  lineitem_arraylist=new ArrayList<LineItem_Bean>();
                        for(int i=0;i<list.size();i++)
                        {
                            if(!list.get(i).getRowState().equalsIgnoreCase("Deleted"))
                            {

                                lineitem_bean.setItem(list.get(i).getItem());
                                lineitem_bean.setItemCode(list.get(i).getItemCode());
                                lineitem_bean.setRepair_Code(list.get(i).getRepair_Code());
                                lineitem_bean.setRepair_Code_Id(list.get(i).getRepair_Code_Id());
                                lineitem_bean.setSubItem(list.get(i).getSubItem());
                                lineitem_bean.setSuIitemCode(list.get(i).getSuIitemCode());
                                lineitem_bean.setDamage_Code(list.get(i).getDamage_Code());
                                lineitem_bean.setDamage_Code_Id(list.get(i).getDamage_Code_Id());
                                lineitem_bean.setManHour(list.get(i).getManHour());
                                lineitem_bean.setManHour_Cost(list.get(i).getManHour_Cost());
                                lineitem_bean.setMHR(list.get(i).getMHR());
                                lineitem_bean.setMetial_Cost(list.get(i).getMetial_Cost());
                                lineitem_bean.setResponsibility_Cd(list.get(i).getResponsibility_Cd());
                                lineitem_bean.setResponsibility(list.get(i).getResponsibility());
                                lineitem_bean.setTotel(list.get(i).getTotel());
                                lineitem_bean.setRemark(list.get(i).getRemark());
                                lineitem_bean.setRowState(list.get(i).getRowState());

                                lineitem_arraylist.add(lineitem_bean);

                            }
                        }*/
                        try {
                            totel_amount=0;
                            object1 = new JSONObject();
                            JSONObject object_d = new JSONObject();
                            JSONArray json_array = new JSONArray();
                            for (int i = 0; i < lineitem_arraylist.size(); i++) {

                                if (lineitem_arraylist.get(i).getResponsibility_Cd().equalsIgnoreCase("I")) {
                                    Invoice_party = Invoice_party + 1;
                                }
                                object2 = new JSONObject();
                                object2.put("RPR_ESTMT_DTL_ID", lineitem_arraylist.get(i).getRPR_ESTMT_DTL_ID());
                                object2.put("RPR_ESTMT_ID", lineitem_arraylist.get(i).getRPR_ESTMT_ID());
                                object2.put("Tariff_Code", lineitem_arraylist.get(i).getTariff_Code());
                                object2.put("Tariff_Group", lineitem_arraylist.get(i).getTariff_Group());
                                object2.put("ItemCd", lineitem_arraylist.get(i).getItem());
                                object2.put("Item", lineitem_arraylist.get(i).getItem());
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
                                object2.put("RowState", lineitem_arraylist.get(i).getRowState());
                                totel_amount = totel_amount+(int) ((Float.parseFloat(lineitem_arraylist.get(i).getTotel())));

                                json_array.put(object2);
                            }
                            object_d.put("Response", json_array);
                            object1.put("d", object_d);
                        }catch (Exception e)
                        {

                        }
                        tv_amount.setText(String.valueOf(totel_amount)+".00 "+" "+GlobalConstants.currency);

                        GlobalConstants.Line_item_Json= String.valueOf(object1);
                        GlobalConstants.lineItem_beanArrayList= lineitem_arraylist;
                        Log.i("object1", String.valueOf(object1));

                        adapter = new UserListAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.line_item_row, lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);
                        list_line_items.setAdapter(adapter);

                    }
                });


            }
            return convertView;
        }

        @Override
        public void remove(LineItem_Bean object) {
            list.remove(object);
            notifyDataSetChanged();
        }

        public List<LineItem_Bean> getWorldPopulation() {
            return list;
        }

        public void toggleSelection(int position) {
            selectView(position, !mSelectedItemsIds.get(position));
        }

        public void removeSelection() {
            mSelectedItemsIds = new SparseBooleanArray();
            notifyDataSetChanged();
        }

        public void selectView(int position, boolean value) {
            if (value)
                mSelectedItemsIds.put(position, value);
            else
                mSelectedItemsIds.delete(position);
            notifyDataSetChanged();
        }

        public int getSelectedCount() {
            return mSelectedItemsIds.size();
        }

        public SparseBooleanArray getSelectedIds() {
            return mSelectedItemsIds;
        }

    }
    static class ViewHolder {
        TextView tariff,rpe_edt_id_dl,rpe_edt_id,tariffGroup,item,subitem,damagecode,repaircode,repaircost,damagecost,manhour,manhourcost,tv_responsibility_id,responsibility,
        metrialcost,totelcost,line_item_id,tv_lable_item,remark;
        CheckBox checkBox;
        Spinner sp_item,sp_responsibility,sp_subItem,sp_repair_code,sp_damage_code;
        LinearLayout whole,LL_username;

        ImageView id_attachment;
        public TextView MHR;
    }

    private void popUp(final String fieldname,final String getvalues, final LineItem_Bean position,final ArrayList<LineItem_Bean> lineitem_arraylist,
                       final ArrayList<CustomerDropdownBean> worldlist,
                       final ArrayList<CustomerDropdownBean> Subworldlist,
                       final ArrayList<CustomerDropdownBean> Damageworldlist,
                       final ArrayList<CustomerDropdownBean> Repairworldlist
    ) {

        String[] responsibilitys = {"C", "I"};
        String[] responsibilitys_C = {"C"};
        String[] responsibilitys_I = {"I"};
        dialog = new Dialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
        dialog.setContentView(R.layout.profile_update_popup);
        Button btn_edit_ok = (Button) dialog.findViewById(R.id.bt_ok);
        TextView tv_name = (TextView) dialog.findViewById(R.id.tv_name);
        final EditText ed_get = (EditText) dialog.findViewById(R.id.edit);
        final EditText remark_edit = (EditText) dialog.findViewById(R.id.remark_edit);
        Button btn_close = (Button) dialog.findViewById(R.id.tv_resend);
        final Spinner sp_item = (Spinner) dialog.findViewById(R.id.item_spinner);
        final RelativeLayout sp_LL = (RelativeLayout) dialog.findViewById(R.id.sp_LL);
        tv_name.setText(fieldname);
        ed_get.setText(getvalues);
        if(fieldname.equalsIgnoreCase("Remark"))
        {
            remark_edit.setText(getvalues);
            remark_edit.setVisibility(View.VISIBLE);
            sp_LL.setVisibility(View.GONE);
            sp_item.setVisibility(View.GONE);
            remark_edit.setInputType(InputType.TYPE_CLASS_TEXT);
            remark_edit.setSingleLine(false);

        }else {
            ed_get.setFilters(new InputFilter[] { new InputFilter.LengthFilter(12) });
            ed_get.setKeyListener(DigitsKeyListener.getInstance(true,true));
            ed_get.setLongClickable(false);
            ed_get.setMovementMethod(new ScrollingMovementMethod());
        }
        if (fieldname.equalsIgnoreCase("Item") || fieldname.equalsIgnoreCase("SubItem") ||
                fieldname.equalsIgnoreCase("Damage")
                || fieldname.equalsIgnoreCase("Repair")
                || fieldname.equalsIgnoreCase("Responsibility")) {
            ed_get.setVisibility(View.GONE);

        }
        if (fieldname.equalsIgnoreCase("Item")) {
//            ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, worldlist);




            SpinnerCustomAdapter  CustomerAdapter = new SpinnerCustomAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.spinner_text, worldlist);
            CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            sp_item.setAdapter(CustomerAdapter);

        } else if (fieldname.equalsIgnoreCase("SubItem")) {


            SpinnerCustomAdapter  CustomerAdapter = new SpinnerCustomAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.spinner_text, Subworldlist);
//            ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, Subworldlist);
            CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            sp_item.setAdapter(CustomerAdapter);
        } else if (fieldname.equalsIgnoreCase("Damage")) {
            SpinnerCustomAdapter  CustomerAdapter = new SpinnerCustomAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.spinner_text, Damageworldlist);
//            ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, Damageworldlist);
            CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            sp_item.setAdapter(CustomerAdapter);
        } else if (fieldname.equalsIgnoreCase("Repair")) {
            SpinnerCustomAdapter  CustomerAdapter = new SpinnerCustomAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.spinner_text, Repairworldlist);
//            ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, Repairworldlist);
            CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            sp_item.setAdapter(CustomerAdapter);
        } else if (fieldname.equalsIgnoreCase("Responsibility")) {

            ArrayAdapter<String> Repair_sp_responsibilitys = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, responsibilitys);
            Repair_sp_responsibilitys.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            sp_item.setAdapter(Repair_sp_responsibilitys);


        }


        btn_close.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dialog.dismiss();
            }
        });
        sp_item.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                String item;
                if (fieldname.equalsIgnoreCase("Item")) {
                    get_sp_item_code = Tariff_Itemcode_ArrayList.get(i).getCode();
                    item = Tariff_Itemcode_ArrayList.get(i).getItem_Id();
                    // holder.item.setText(item);
                    Log.i("get_sp_item_code",get_sp_item_code);
                    Log.i("item",item);
//                    new Sub_Item_details_filter(get_sp_item_code).execute();


                    position.setItem(item);
                    position.setItemCode(get_sp_item_code);
                } else if (fieldname.equalsIgnoreCase("SubItem")) {
                    get_sp_item_code = Subworldlist.get(i).getCode();
                    item = Subworldlist.get(i).getSub_item_Id();
                    // holder.item.setText(item);
                    position.setSubItem(item);
                    position.setSuIitemCode(get_sp_item_code);
                } else if (fieldname.equalsIgnoreCase("Damage")) {
                    get_sp_item_code = Tariff_DamageItemcode_ArrayList.get(i).getCode();
                    item = Tariff_DamageItemcode_ArrayList.get(i).getName();
                    // holder.item.setText(item);
                    position.setDamage_Code(item);
                    position.setDamage_Code_Id(get_sp_item_code);
                } else if (fieldname.equalsIgnoreCase("Repair")) {
                    get_sp_item_code = Tariff_RepairItemcode_ArrayList.get(i).getCode();
                    item = Tariff_RepairItemcode_ArrayList.get(i).getName();
                    // holder.item.setText(item);
                    position.setRepair_Code(item);
                    position.setRepair_Code_Id(get_sp_item_code);
                } else if (fieldname.equalsIgnoreCase("Responsibility")) {

                    responsibilitys_cd = sp_item.getSelectedItem().toString();
                    if (responsibilitys_cd.equalsIgnoreCase("C")) {
                        position.setResponsibility_Cd(responsibilitys_cd);
                        position.setResponsibility("66");
                        GlobalConstants.Invoice_party=0;
                        try {
                            totel_amount=0;
                            for (int j = 0; j < lineitem_arraylist.size(); j++) {


                                totel_amount = totel_amount+(int) ((Float.parseFloat(lineitem_arraylist.get(j).getTotel())));
                            }
                            tv_amount.setText(String.valueOf(totel_amount)+".00 "+" "+GlobalConstants.currency);

                        } catch (Exception e) {
                            Log.i("e==>", String.valueOf(e));
                        }
                    } else {
                        totel_amount=0;
                        position.setResponsibility_Cd(responsibilitys_cd);
                        position.setResponsibility("67");
                        object1 = new JSONObject();
                        JSONObject object_d = new JSONObject();
                        JSONArray json_array = new JSONArray();
                        try {
                            for (int j = 0; j < lineitem_arraylist.size(); j++) {

                                object2 = new JSONObject();
                                object2.put("RPR_ESTMT_DTL_ID", lineitem_arraylist.get(j).getRPR_ESTMT_DTL_ID());
                                object2.put("RPR_ESTMT_ID",  lineitem_arraylist.get(j).getRPR_ESTMT_ID());
                                object2.put("Tariff_Code", lineitem_arraylist.get(j).getTariff_Code());
                                object2.put("Tariff_Group", lineitem_arraylist.get(j).getTariff_Group());
                                object2.put("ItemCd", lineitem_arraylist.get(j).getItem());
                                object2.put("Item", lineitem_arraylist.get(j).getItem());
                                object2.put("RepairCd", lineitem_arraylist.get(j).getRepair_Code());
                                object2.put("Repair", lineitem_arraylist.get(j).getRepair_Code_Id());
                                object2.put("SubItemCd", lineitem_arraylist.get(j).getSubItem());
                                object2.put("SubItem", lineitem_arraylist.get(j).getSuIitemCode());
                                object2.put("DamageCd", lineitem_arraylist.get(j).getDamage_Code());
                                object2.put("Damage", lineitem_arraylist.get(j).getDamage_Code_Id());
                                object2.put("ManHour", lineitem_arraylist.get(j).getManHour());
                                object2.put("MHC", lineitem_arraylist.get(j).getManHour_Cost());
                                object2.put("MC", lineitem_arraylist.get(j).getMetial_Cost());
                                object2.put("MHR", lineitem_arraylist.get(j).getMHR());
                                if (lineitem_arraylist.get(j).getResponsibility_Cd().equalsIgnoreCase("I")) {
                                    Invoice_party = Invoice_party + 1;
                                }
                                object2.put("ResponsibilityCd", lineitem_arraylist.get(j).getResponsibility_Cd());
                                object2.put("Responsibility", lineitem_arraylist.get(j).getResponsibility());
                                object2.put("TC", lineitem_arraylist.get(j).getTotel());
                                object2.put("DmgRprRemarks", lineitem_arraylist.get(j).getRemark());
                                object2.put("RowState", lineitem_arraylist.get(j).getRowState());
                                json_array.put(object2);
                                totel_amount = totel_amount+(int) ((Float.parseFloat(lineitem_arraylist.get(j).getTotel())));
                            }
                            object_d.put("Response", json_array);
                            object1.put("d", object_d);
                            tv_amount.setText(String.valueOf(totel_amount)+".00 "+" "+GlobalConstants.currency);
                            GlobalConstants.Invoice_party = Invoice_party;
                            Log.i("lineitem_arraylist_json", String.valueOf(object1));
                            GlobalConstants.Line_item_Json= String.valueOf(object1);
                            SharedPreferences.Editor editor_ = sp.edit();
                            editor_.putString(SP_ADD_LINE_ITEM_JSON, String.valueOf(object1));
                            editor_.commit();
                            if (lineitem_arraylist.size() != 0) {
                                dialog.dismiss();
                                //Tariff_RepairItemcode_ArrayList,Tariff_DamageItemcode_ArrayList,Tariff_SubItemcode_ArrayList,Tariff_Itemcode_ArrayList
                                adapter = new UserListAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.line_item_row, lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);
                                list_line_items.setAdapter(adapter);


                            }
                        } catch (Exception e) {
                            Log.i("e==>", String.valueOf(e));
                        }

                        dialog.dismiss();
//                        new InvoiceParty_details().execute();


                    }

                }

            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }

        });

        btn_edit_ok.setOnClickListener(new View.OnClickListener() {
            public String str1,str2;
            public JSONObject object2;

            @Override
            public void onClick(View view) {

                if (fieldname.equalsIgnoreCase("Material Cost")) {


                    get_ed_values=ed_get.getText().toString();


                    if (!get_ed_values.equals("")) {

                       /* if ((get_ed_values.matches(".\\d"))) {
                            get_ed_values = "0" + get_ed_values + "0";
                        } else if ((get_ed_values.matches("\\d."))) {
                            get_ed_values = get_ed_values + "00";
                        }else if ((get_ed_values.matches("\\d\\d"))) {
                            get_ed_values = get_ed_values + ".00";
                        }*/

                        if (get_ed_values.contains(".")) {

                            String[] new_MH = get_ed_values.split(Pattern.quote("."));

                            try {
                                str1 = new_MH[0].toString();
                            }catch (Exception e)
                            {
                                str1="";
                            } try {
                                str2 = new_MH[1].toString();
                            }catch (Exception e)
                            {
                                str2="";
                            }

                            if(str1.length() ==1 && str2.length() == 0)
                            {
                                get_ed_values=str1+".00";
                            }  if(str1.length() ==0 && str2.length() == 1)
                            {
                                get_ed_values="0."+str2+"0";
                            }if(str1.length() ==0 && str2.length() == 2)
                            {
                                get_ed_values="0."+str2;
                            }else if (str1.length() <= 7 && str2.length() == 2) {
                                get_ed_values = ed_get.getText().toString();

                            } else if (str1.length() <= 7 && (str2.length() == 0)) {
                                get_ed_values = str1 + ".00";
                            }else if (str1.length() <= 7 && (str2.length() == 1)) {
                                get_ed_values = get_ed_values + "0";
                            }else if (str1.length() <= 7 && (str2.length()>2)) {
                                get_ed_values = get_ed_values.replace(".","") + ".0";
                            }else if (str1.length()==0 && (str2.length()>2)) {
                                get_ed_values =  "0"+get_ed_values;
                            }else if (str1.length()<=7 && (str2.length()==0)) {
                                get_ed_values =  get_ed_values+"00";
                            } else {
                                shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");
                                get_ed_values=get_ed_values+".00";
                            }

                        } else {

                            if (get_ed_values.length() <= 7) {
                                get_ed_values = get_ed_values + ".00";
                                Log.i("get_ed_values",get_ed_values);
                            } else {
                                shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");
                                get_ed_values=get_ed_values+".00";
                            }
                        }

                    } else {

                        shortToast(getApplicationContext(), "Please key-in Mandate Fields");
                    }

                    String[] new_get_mh = get_ed_values.split(Pattern.quote("."));

                    String str1 = new_get_mh[0].toString();

                    if (str1.length() > 7 ) {

                        shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");

                    }else
                    {
                        Log.i("get_ed_values", get_ed_values);
                        holder.metrialcost.setText(get_ed_values);
                        position.setMetial_Cost(get_ed_values);

                        float MH = Float.parseFloat(position.getManHour());
                        float MH_R = Float.parseFloat(position.getMHR());
                        float MC = Float.parseFloat(get_ed_values);
                        float totel = MH * MH_R;
                        Log.i("totel_cost", String.valueOf(totel + MC));
                        String tc = String.valueOf((int) totel + MC);

                        if (getvalues.equals(get_ed_values)) {

                        } else {
                            position.setTotel(tc.replace("E", ""));

                        }

                    }


                } else if (fieldname.equalsIgnoreCase("ManHour")) {



                    get_ed_values=ed_get.getText().toString();


                    if (!get_ed_values.equals("")) {

                       /* if ((get_ed_values.matches(".\\d"))) {
                            get_ed_values = "0" + get_ed_values + "0";
                        } else if ((get_ed_values.matches("\\d."))) {
                            get_ed_values = get_ed_values + "00";
                        }else if ((get_ed_values.matches("\\d\\d"))) {
                            get_ed_values = get_ed_values + ".00";
                        }*/

                        if (get_ed_values.contains(".")) {

                            String[] new_MH = get_ed_values.split(Pattern.quote("."));

                            try {
                                str1 = new_MH[0].toString();
                            }catch (Exception e)
                            {
                                str1="";
                            } try {
                                str2 = new_MH[1].toString();
                            }catch (Exception e)
                            {
                                str2="";
                            }

                            if(str1.length() ==1 && str2.length() == 0)
                            {
                                get_ed_values=str1+".00";
                            }  if(str1.length() ==0 && str2.length() == 1)
                            {
                                get_ed_values="0."+str2+"0";
                            }if(str1.length() ==0 && str2.length() == 2)
                            {
                                get_ed_values="0."+str2;
                            }else if (str1.length() <= 7 && str2.length() == 2) {
                                get_ed_values = ed_get.getText().toString();

                            } else if (str1.length() <= 7 && (str2.length() == 0)) {
                                get_ed_values = str1 + ".00";
                            }else if (str1.length() <= 7 && (str2.length() == 1)) {
                                get_ed_values = get_ed_values + "0";
                            }else if (str1.length() <= 7 && (str2.length()>2)) {
                                get_ed_values = get_ed_values.replace(".","") + ".0";
                            }else if (str1.length()==0 && (str2.length()>2)) {
                                get_ed_values =  "0"+get_ed_values;
                            }else if (str1.length()<=7 && (str2.length()==0)) {
                                get_ed_values =  get_ed_values+"00";
                            } else {
                                shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");
                                get_ed_values=get_ed_values+".00";
                            }

                        } else {

                            if (get_ed_values.length() <= 7) {
                                get_ed_values = get_ed_values + ".00";
                                Log.i("get_ed_values",get_ed_values);
                            } else {
                                shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");
                                get_ed_values=get_ed_values+".00";
                            }
                        }

                    } else {

                        shortToast(getApplicationContext(), "Please key-in Mandate Fields");
                    }

                    String[] new_get_mh = get_ed_values.split(Pattern.quote("."));

                    String str1 = new_get_mh[0].toString();

                    if (str1.length() > 7 ) {

                        shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");

                    }else {

                        holder.manhour.setText(get_ed_values);
                        position.setManHour(get_ed_values);

                        float MH = Float.parseFloat(holder.manhour.getText().toString());
                        float MH_R = Float.parseFloat(position.getMHR());
                        float MC = Float.parseFloat(position.getMetial_Cost());
                        float totel = MH * MH_R;
                        Log.i("totel_cost", String.valueOf(totel + MC));
                        String tc = String.valueOf((int) totel + MC);
                        String MH_tc = String.valueOf((int) totel);

                        if (getvalues.equals(get_ed_values)) {

                        } else {
                            position.setTotel(tc.replace("E", ""));

                        }
                        position.setManHour_Cost(MH_tc.replace("E", ""));

                    }
                }else if(fieldname.equalsIgnoreCase("Remark"))
                {
                    holder.manhour.setText(remark_edit.getText().toString());
                    position.setRemark(remark_edit.getText().toString());
                }


                object1 = new JSONObject();
                JSONObject object_d = new JSONObject();
                JSONArray json_array = new JSONArray();
                try {
                    totel_amount=0;
                    for (int i = 0; i < lineitem_arraylist.size(); i++) {

                        object2 = new JSONObject();

                        if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                            object2.put("RPR_ESTMT_DTL_ID", lineitem_arraylist.get(i).getRPR_ESTMT_DTL_ID());
                            object2.put("RPR_ESTMT_ID", lineitem_arraylist.get(i).getRPR_ESTMT_ID());

                        }else
                        {
                            object2.put("RPR_ESTMT_DTL_ID", "1");
                            object2.put("RPR_ESTMT_ID", "1");
                        }
                        object2.put("Tariff_Code", lineitem_arraylist.get(i).getTariff_Code());
                        object2.put("Tariff_Group", lineitem_arraylist.get(i).getTariff_Group());
                        object2.put("ItemCd", lineitem_arraylist.get(i).getItem());
                        object2.put("Item", lineitem_arraylist.get(i).getItem());
                        object2.put("RepairCd", lineitem_arraylist.get(i).getRepair_Code());
                        object2.put("Repair", lineitem_arraylist.get(i).getRepair_Code_Id());
                        object2.put("SubItemCd", lineitem_arraylist.get(i).getSubItem());
                        object2.put("SubItem", lineitem_arraylist.get(i).getSuIitemCode());
                        object2.put("DamageCd", lineitem_arraylist.get(i).getDamage_Code());
                        object2.put("Damage", lineitem_arraylist.get(i).getDamage_Code_Id());
                        object2.put("ManHour", lineitem_arraylist.get(i).getManHour());
                        object2.put("MHC", lineitem_arraylist.get(i).getManHour_Cost());
                        object2.put("MC", lineitem_arraylist.get(i).getMetial_Cost());
                        object2.put("MHR", lineitem_arraylist.get(i).getMHR());
                        if (lineitem_arraylist.get(i).getResponsibility_Cd().equalsIgnoreCase("I")) {
                            Invoice_party = Invoice_party + 1;
                        }
                        object2.put("ResponsibilityCd", lineitem_arraylist.get(i).getResponsibility_Cd());
                        object2.put("Responsibility", lineitem_arraylist.get(i).getResponsibility());
                        object2.put("TC", lineitem_arraylist.get(i).getTotel());
                        object2.put("DmgRprRemarks", lineitem_arraylist.get(i).getRemark());
                        object2.put("RowState", lineitem_arraylist.get(i).getRowState());
                        json_array.put(object2);
                        totel_amount =totel_amount+ (int) ((Float.parseFloat(lineitem_arraylist.get(i).getTotel())));
                    }
                    object_d.put("Response", json_array);
                    object1.put("d", object_d);
                    tv_amount.setText(String.valueOf(totel_amount)+".00 "+" "+GlobalConstants.currency);
                    GlobalConstants.Invoice_party = Invoice_party;
                    Log.i("edit_arraylist_json", String.valueOf(object1));
                    SharedPreferences.Editor editor_ = sp.edit();
                    editor_.putString(SP_ADD_LINE_ITEM_JSON, String.valueOf(object1));
                    editor_.commit();
                    if (lineitem_arraylist.size() != 0) {
                        dialog.dismiss();
                        //Tariff_RepairItemcode_ArrayList,Tariff_DamageItemcode_ArrayList,Tariff_SubItemcode_ArrayList,Tariff_Itemcode_ArrayList
                        adapter = new UserListAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.line_item_row, lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);
                        list_line_items.setAdapter(adapter);
                    }
                } catch (Exception e) {
                    Log.i("e==>", String.valueOf(e));
                }
            }
        });

        dialog.show();
    }

    public class Item_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLItem);
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
                        Tariff_Itemcode_ArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("ITM_DSCRPTN_VC"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("ITM_DSCRPTN_VC"));
                            customer_DropdownBean.setCode(jsonObject.getString("ITM_CD"));
                            customer_DropdownBean.setItem_Id(jsonObject.getString("ITM_ID"));
                            tariffName = jsonObject.getString("ITM_DSCRPTN_VC");
                            tariffCode = jsonObject.getString("ITM_CD");
                            String[] set1 = new String[2];
                            set1[0] = tariffName;
                            set1[1] = tariffCode;
                            dropdown_customer_list.add(set1);
                            Cust_name.add(set1[0]);
                            Cust_code.add(set1[1]);
                            Tariff_Itemcode_ArrayList.add(customer_DropdownBean);
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

               /* adapter = new UserListAdapter(Repair_Estimation_wizard.this, R.layout.line_item_row, lineitem_arraylist,worldlist,Sub_worldlist);
                list_line_items.setAdapter(adapter);*/
                new Sub_Item_details().execute();


            } else {
//                shortToast(getApplicationContext(), "Data Not Found");

            }
            if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();

        }

    }

    public class Sub_Item_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLSubItem);
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


                        Subworldlist = new ArrayList<String>();
                        Tariff_SubItemcode_ArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("SB_ITM_DSCRPTN_VC"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("SB_ITM_DSCRPTN_VC"));
                            customer_DropdownBean.setCode(jsonObject.getString("SB_ITM_CD"));
                            customer_DropdownBean.setSub_item_Id(jsonObject.getString("SB_ITM_ID"));
                            customer_DropdownBean.setItem_Id(jsonObject.getString("ITM_ID"));
                            tariffName = jsonObject.getString("SB_ITM_DSCRPTN_VC");
                            tariffCode = jsonObject.getString("SB_ITM_CD");
                            String[] set1 = new String[2];
                            set1[0] = tariffName;
                            set1[1] = tariffCode;
                            dropdown_customer_list.add(set1);
                            Cust_name.add(set1[0]);
                            Cust_code.add(set1[1]);
                            Tariff_SubItemcode_ArrayList.add(customer_DropdownBean);
                            Subworldlist.add(tariffName);


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

               /* adapter = new UserListAdapter(Repair_Estimation_wizard.this, R.layout.line_item_row, lineitem_arraylist,worldlist,Sub_worldlist);
                list_line_items.setAdapter(adapter);*/
                new Damage_Code_details().execute();


            } else {
//                shortToast(getApplicationContext(), "Data Not Found");

            }
            if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();

        }

    }
    public class Damage_Code_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLDamage_Code);
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


                        damage_worldlist = new ArrayList<String>();
                        Tariff_DamageItemcode_ArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("DMG_DSCRPTN_VC"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("DMG_DSCRPTN_VC"));
                            customer_DropdownBean.setCode(jsonObject.getString("DMG_CD"));
                            tariffName = jsonObject.getString("DMG_DSCRPTN_VC");
                            tariffCode = jsonObject.getString("DMG_CD");
                            String[] set1 = new String[2];
                            set1[0] = tariffName;
                            set1[1] = tariffCode;
                            dropdown_customer_list.add(set1);
                            Cust_name.add(set1[0]);
                            Cust_code.add(set1[1]);
                            Tariff_DamageItemcode_ArrayList.add(customer_DropdownBean);
                            damage_worldlist.add(tariffName);


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

                new Repair_Code_details().execute();

               /* adapter = new UserListAdapter(Repair_Estimation_wizard.this, R.layout.line_item_row, lineitem_arraylist,worldlist,Sub_worldlist);
                list_line_items.setAdapter(adapter);*/



            } else {
//                shortToast(getApplicationContext(), "Data Not Found");

            }
            if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();

        }

    }

    public class Repair_Code_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLRepair_Code);
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


                        repair_worldlist = new ArrayList<String>();
                        Tariff_RepairItemcode_ArrayList = new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("RPR_CD"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("RPR_DSCRPTN_VC"));
                            customer_DropdownBean.setCode(jsonObject.getString("RPR_ID"));
                            tariffName = jsonObject.getString("RPR_DSCRPTN_VC");
                            tariffCode = jsonObject.getString("RPR_ID");
                            String[] set1 = new String[2];
                            set1[0] = tariffName;
                            set1[1] = tariffCode;
                            dropdown_customer_list.add(set1);
                            Cust_name.add(set1[0]);
                            Cust_code.add(set1[1]);
                            Tariff_RepairItemcode_ArrayList.add(customer_DropdownBean);
                            repair_worldlist.add(tariffName);


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


            if (dropdown_customer_list != null) {
                try {
                    if (line_from.equalsIgnoreCase("add_line")) {
                        Log.i("LineItemlist==", String.valueOf(lineitem_arraylist.size()));

                        Log.i("lineItem_beanArray", sp.getString(SP_ADD_LINE_ITEM_JSON, "add_line_item_json"));
                        String sp_json = sp.getString(SP_ADD_LINE_ITEM_JSON, "add_line_item_json");
                        if (!sp_json.equalsIgnoreCase("add_line_item_json") ) {
                            try {
                                jsonrootObject = new JSONObject(sp_json);
                                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");
                                jsonarray = getJsonObject.getJSONArray("Response");
                                if (jsonarray != null) {

                                    System.out.println("Am HashMap list" + jsonarray);
                                    if (jsonarray.length() < 1) {
                                        popUp_add("Add LineItem", lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);

                                    } else {


                                        for (int j = 0; j < jsonarray.length(); j++) {

                                            repair_bean = new RepairBean();
                                            jsonObject = jsonarray.getJSONObject(j);


                                            lineitem_bean = new LineItem_Bean();
                                            JSONObject jsonObject_line = jsonarray.getJSONObject(j);

                                            lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject_line.getString("RPR_ESTMT_DTL_ID"));
                                            lineitem_bean.setRPR_ESTMT_ID(jsonObject_line.getString("RPR_ESTMT_ID"));
                                            lineitem_bean.setTariff_Code("0");
                                            lineitem_bean.setTariff_Group("0");
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

                                            lineitem_arraylist.add(lineitem_bean);
                                        }
                                        popUp_add("Add LineItem", lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);

                                    }
                                }

                            } catch (JSONException e) {
                                e.printStackTrace();
                            }
                        } else {
                            popUp_add("Add LineItem", lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);

                        }


                    } else {
                        //Tariff_RepairItemcode_ArrayList,Tariff_DamageItemcode_ArrayList,Tariff_SubItemcode_ArrayList,Tariff_Itemcode_ArrayList

                        adapter = new UserListAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.line_item_row, lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);
                        list_line_items.setAdapter(adapter);
                        fetch_count++;
                        LL_summary.setVisibility(View.GONE);
                    }
                } catch (Exception e) {
                    //Tariff_RepairItemcode_ArrayList,Tariff_DamageItemcode_ArrayList,Tariff_SubItemcode_ArrayList,Tariff_Itemcode_ArrayList
                    adapter = new UserListAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.line_item_row, lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);
                    list_line_items.setAdapter(adapter);
                    fetch_count++;
                    LL_summary.setVisibility(View.GONE);

                }


            } else {
               // shortToast(getApplicationContext(), "Data Not Found");

            }
            if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();

        }

    }

    private void popUp_add(final String fieldname,final ArrayList<LineItem_Bean> LineItemlist,
                           final ArrayList<CustomerDropdownBean> worldlist,
                           final ArrayList<CustomerDropdownBean> Subworldlist,
                           final ArrayList<CustomerDropdownBean> Damageworldlist,
                           final ArrayList<CustomerDropdownBean> Repairworldlist
    ) {
        final ArrayList<LineItem_Bean> add_line_item = new ArrayList<LineItem_Bean>();
        add_dialog = new Dialog(this);
        add_dialog.setContentView(R.layout.add_line_item);

        final String[] responsibilitys = {"C", "I"};
        final Button btn_ok = (Button) add_dialog.findViewById(R.id.bt_ok);
        Button btn_close = (Button) add_dialog.findViewById(R.id.tv_resend);
        final EditText tv_item = (EditText) add_dialog.findViewById(R.id.tv_item);
        final EditText tv_sub_item = (EditText) add_dialog.findViewById(R.id.tv_sub_item);
        final EditText tv_damage_code = (EditText) add_dialog.findViewById(R.id.tv_damage_code);
        final EditText tv_repair_cost = (EditText) add_dialog.findViewById(R.id.tv_repair_cost);
        final EditText tv_manhour_cost = (EditText) add_dialog.findViewById(R.id.tv_manhour_cost);
        tv_manhour_cost.setKeyListener(DigitsKeyListener.getInstance(true,true));
        tv_manhour_cost.setLongClickable(false);

        final EditText tv_manhour_rate = (EditText) add_dialog.findViewById(R.id.tv_manhour_rate);
        tv_manhour_rate.setText(GlobalConstants.lobor_rate);

        tv_manhour_rate.setKeyListener(DigitsKeyListener.getInstance(true,true));
        tv_manhour_rate.setLongClickable(false);
        final EditText tv_metrial_cost = (EditText) add_dialog.findViewById(R.id.tv_metrial_cost);
        tv_metrial_cost.setKeyListener(DigitsKeyListener.getInstance(true,true));
        tv_metrial_cost.setLongClickable(false);
        final EditText manhour = (EditText) add_dialog.findViewById(R.id.tv_man_hour);
        manhour.setKeyListener(DigitsKeyListener.getInstance(true,true));
        manhour.setLongClickable(false);
        final EditText tv_remark = (EditText) add_dialog.findViewById(R.id.tv_remark);
        final EditText tv_responsibility = (EditText) add_dialog.findViewById(R.id.tv_responsibility);
        tv_responsibility.setLongClickable(false);

        final TextView tv_responsibility_id = (TextView) add_dialog.findViewById(R.id.responsibility_id);
        final TextView tv_item_ = (TextView) add_dialog.findViewById(R.id.tv_item_);
        final TextView tv_subitem = (TextView) add_dialog.findViewById(R.id.tv_subitem);
        final TextView tv_damagecode = (TextView) add_dialog.findViewById(R.id.tv_damagecode);
        final TextView tv_repaircode = (TextView) add_dialog.findViewById(R.id.tv_repaircode);
        final TextView tv_manhour = (TextView) add_dialog.findViewById(R.id.tv_manhour);
        final TextView tv_lable_responsibility = (TextView) add_dialog.findViewById(R.id.tv_lable_responsibility);
        String lable_item_default = getColoredSpanned("Item", "#bbbbbb");
        String lable_subitem = getColoredSpanned("SubItem", "#bbbbbb");
        String lable_damage = getColoredSpanned("Damage Code", "#bbbbbb");
        String lable_repair = getColoredSpanned("Repair Code", "#bbbbbb");
        String lable_man_hour = getColoredSpanned("Man Hour", "#bbbbbb");
        String lable_Responsibility = getColoredSpanned("Responsibility", "#bbbbbb");

        String surName = getColoredSpanned("*", "#cb0da5");

        if(lable_item.equals("")||lable_item.equals("null")|| lable_item.equals(null))
        {
            tv_item_.setText(Html.fromHtml(lable_item_default + " " + surName));
        }else
        {
            tv_item_.setText(Html.fromHtml(lable_item + " " + surName));
        }
        tv_subitem.setText(Html.fromHtml(lable_subitem ));
        tv_damagecode.setText(Html.fromHtml(lable_damage + " " + surName));
        tv_repaircode.setText(Html.fromHtml(lable_repair + " " + surName));
        tv_manhour.setText(Html.fromHtml(lable_man_hour + " " + surName));
        tv_lable_responsibility.setText(Html.fromHtml(lable_Responsibility + " " + surName));

        final EditText tv_totalCost = (EditText) add_dialog.findViewById(R.id.tv_totalCost);
        final CustomSpinner sp_item = (CustomSpinner) add_dialog.findViewById(R.id.sp_item);
        final CustomSpinner sp_sub_item = (CustomSpinner) add_dialog.findViewById(R.id.sp_sub_item);
        final CustomSpinner sp_damage_code = (CustomSpinner) add_dialog.findViewById(R.id.sp_damage_code);
        final CustomSpinner sp_repair_code = (CustomSpinner) add_dialog.findViewById(R.id.sp_repair_code);
        final Spinner sp_responsibilitys = (Spinner) add_dialog.findViewById(R.id.sp_responsibilitys);
        // tv_name.setText(fieldname);
//        tv_manhour_rate.setText(GlobalConstants.lobor_rate);

        tv_responsibility.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sp_responsibilitys.performClick();
            }
        });
        //  ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, worldlist);
        SpinnerCustomAdapter CustomerAdapter = new SpinnerCustomAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.spinner_text, worldlist);
        CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        sp_item.setAdapter(CustomerAdapter);

//        ArrayAdapter<String> Sub_Item = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, Subworldlist);
      /*  SpinnerCustomAdapter Sub_Item = new SpinnerCustomAdapter(Repair_Estimation_wizard.this, R.layout.spinner_text, Subworldlist);
        Sub_Item.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        sp_sub_item.setAdapter(Sub_Item);*/

//        ArrayAdapter<String> Damage_code = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, Damageworldlist);
        SpinnerCustomAdapter Damage_code = new SpinnerCustomAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.spinner_text, Damageworldlist);
        Damage_code.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        sp_damage_code.setAdapter(Damage_code);

//        ArrayAdapter<String> Repair_code = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, Repairworldlist);
        SpinnerCustomAdapter Repair_code = new SpinnerCustomAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.spinner_text, Repairworldlist);
        Repair_code.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        sp_repair_code.setAdapter(Repair_code);

        ArrayAdapter<String> Repair_sp_responsibilitys = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, responsibilitys);
        Repair_sp_responsibilitys.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        sp_responsibilitys.setAdapter(Repair_sp_responsibilitys);

        tv_item.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sp_item.performClick();
            }
        });
        tv_sub_item.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sp_sub_item.performClick();
            }
        });
        tv_repair_cost.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sp_repair_code.performClick();
            }
        });
        tv_damage_code.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sp_damage_code.performClick();
            }
        });

        btn_close.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
//                GlobalConstants.add_lineItem_beanArrayList = final_array_list;
                manhour.setText("");
                tv_remark.setText("");
                tv_item.setText("");
                tv_sub_item.setText("");
                tv_repair_cost.setText("");
                tv_damage_code.setText("");
                tv_remark.setText("");
                tv_metrial_cost.setText("");
                count = 0;
                count_t = 0;
                count_i = 0;
                count_si = 0;
                count_d = 0;
                count_r= 0;
                ArrayAdapter<String> Repair_sp_responsibilitys = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, responsibilitys);
                Repair_sp_responsibilitys.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_responsibilitys.setAdapter(Repair_sp_responsibilitys);
                add_dialog.dismiss();
                add_dialog.cancel();
            }
        });
        sp_item.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                if(count_i!=0) {
                    get_sp_item_code = Tariff_Itemcode_ArrayList.get(i).getItem_Id();
                    item = Tariff_Itemcode_ArrayList.get(i).getItem_Id();
                    tv_item.setText(Tariff_Itemcode_ArrayList.get(i).getCode());

                    new_Subworldlist=new ArrayList<CustomerDropdownBean>();
                    for(int j=0;j<Subworldlist.size();j++)
                    {
                        if(get_sp_item_code.equals(Subworldlist.get(j).getItem_Id()))
                        {
                            customer_DropdownBean = new CustomerDropdownBean();


                            customer_DropdownBean.setName(Subworldlist.get(j).getName());
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(Subworldlist.get(j).getTRFF_CD_DESCRPTN_VC());
                            customer_DropdownBean.setCode(Subworldlist.get(j).getCode());
                            customer_DropdownBean.setItem_Id(Subworldlist.get(j).getItem_Id());

                            new_Subworldlist.add(customer_DropdownBean);
                        }

                    }

                    SpinnerCustomAdapter Sub_Item = new SpinnerCustomAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.spinner_text, new_Subworldlist);
                    Sub_Item.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    sp_sub_item.setAdapter(Sub_Item);
                    tv_sub_item.setText("");
                    count_si=0;
                }
                count_i++;
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        sp_responsibilitys.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {

                responsibilitys_cd = sp_responsibilitys.getSelectedItem().toString();
                tv_responsibility.setText(responsibilitys_cd);
                if (responsibilitys_cd.equalsIgnoreCase("C")) {
                    tv_responsibility_id.setText("66");
                    GlobalConstants.Invoice_party=0;
                } else {
                    tv_responsibility_id.setText("67");
                    GlobalConstants.Invoice_party=1;
//                    new InvoiceParty_details().execute();
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        sp_sub_item.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                String item;
                if(count_si!=0) {
                    get_sp_subitem_code = new_Subworldlist.get(i).getCode();
                    subitem = new_Subworldlist.get(i).getName();
                    tv_sub_item.setText(get_sp_subitem_code);
                }
                count_si++;
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        sp_damage_code.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                String item;
                if(count_d!=0) {
                    get_sp_damage_code = Tariff_DamageItemcode_ArrayList.get(i).getCode();
                    damage_name = Tariff_DamageItemcode_ArrayList.get(i).getName();
                    tv_damage_code.setText(get_sp_damage_code);
                }
                count_d++;
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        sp_repair_code.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                String item;

                if (count_r != 0) {
                    get_sp_repair_code = Tariff_RepairItemcode_ArrayList.get(i).getCode();
                    repir_name = Tariff_RepairItemcode_ArrayList.get(i).getName();
                    tv_repair_cost.setText(repir_name);
                }

                count_r++;

            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        manhour.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                try {
                    float MH = Float.parseFloat(manhour.getText().toString());
                    float MH_R = Float.parseFloat(tv_manhour_rate.getText().toString());
                    float totel = MH * MH_R;
                    Log.i("totel_cost", String.valueOf(totel));
                    tv_totalCost.setText(String.valueOf((int) totel));
                    float MC = Float.parseFloat(tv_metrial_cost.getText().toString());
                    String tc = String.valueOf((int) totel +(int) MC).replace(".", "");
                    tv_manhour_cost.setText((String.valueOf(totel).replace("E","")).replace("-",""));
                    int tc_new= (int) ((int) Math.round(MC)+ (int)totel);
                    Log.i("MC", String.valueOf((int) ((int) Math.round(MC)+ totel)));
                    Log.i("tc_new", String.valueOf(tc_new));
                    tv_totalCost.setText(String.valueOf(tc_new).replace("-",""));
                } catch (Exception e) {
                    Log.e("Exception", String.valueOf(e));
                }

            }

            @Override
            public void afterTextChanged(Editable editable) {

            }
        });
        tv_metrial_cost.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                try {
                    float MH = Float.parseFloat(manhour.getText().toString());
                    float MH_R = Float.parseFloat(tv_manhour_rate.getText().toString());
                    float MC = Float.parseFloat(tv_metrial_cost.getText().toString());
                    float totel = MH * MH_R;
                    Log.i("totel_cost", String.valueOf(totel + MC));

                    String tc = String.valueOf((int) totel +String.valueOf((int) MC));
                    String tc_mm = String.valueOf(totel).replace(".", "");

                    tv_manhour_cost.setText((String.valueOf(totel).replace("E","")).replace("-",""));
                    int tc_new= (int) ((int) Math.round(MC)+ (int)totel);
                    Log.i("tc_new_met", String.valueOf((int) ((int) Math.round(MC)+ (int)totel)));

                    tv_totalCost.setText(String.valueOf(tc_new).replace("-",""));

                } catch (Exception e) {
                    Log.e("Exception", String.valueOf(e));
                }

            }

            @Override
            public void afterTextChanged(Editable editable) {

            }
        });

      /*  manhour.addTextChangedListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {


                return false;
            }
        });*/
        btn_ok.setOnClickListener(new View.OnClickListener() {
            public String sub_item;
            public JSONObject object2;

            @Override
            public void onClick(View view) {

                String MH_C = tv_manhour_cost.getText().toString();
                String repair = tv_repair_cost.getText().toString();
                String damage = tv_damage_code.getText().toString();
                sub_item = tv_sub_item.getText().toString();
                String item = tv_item.getText().toString();
                String MH = manhour.getText().toString();
                String MH_R = tv_manhour_rate.getText().toString();
                String MC = tv_metrial_cost.getText().toString();
                String TC = tv_totalCost.getText().toString();
                String remarks = tv_remark.getText().toString();
                String responsibility = tv_responsibility.getText().toString();
                String responsibility_id = tv_responsibility_id.getText().toString();

                try {

                    if (!MH.equals("")) {

                        if ((MH.matches(".\\d"))) {
                            MH = "0" + MH + "0";
                        } else if ((MH.matches("\\d."))) {
                            MH = MH + "00";
                        }

                        if (MH.contains(".")) {

                            String[] new_MH = MH.split(Pattern.quote("."));

                            String str1 = new_MH[0].toString();
                            String str2 = new_MH[1].toString();
                            if (str1.length() <= 7 && str2.length() == 2) {
                                get_mh = MH;

                            } else if (str1.length() <= 7 && (str2.length() == 1 || str2.length() == 0)) {
                                get_mh = str1 + ".00";
                            } else {
                                shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");
                                get_mh=MH+".00";
                            }

                        } else {

                            if (MH.length() <= 7) {
                                get_mh = MH + ".00";
                            } else {
                                shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");
                                get_mh=MH+".00";
                            }
                        }

                    } else {

                        shortToast(getApplicationContext(), "Please key-in Mandate Fields");
                    }

                    if (!MC.equals("")) {
                        if ((MC.matches(".\\d"))) {
                            MC = "0" + MC + "0";
                        } else if ((MC.matches("\\d."))) {
                            MC = MC + "00";
                        }

                        if (MC.contains(".")) {

                            String[] new_MC = MC.split(Pattern.quote("."));

                            String str1 = new_MC[0];
                            String str2 = new_MC[1];

                            if (str1.length() <= 7 && str2.length() == 2) {
                                get_mc = MC;

                            } else if (str1.length() <= 7 && (str2.length() == 1 || str2.length() == 0)) {
                                get_mc = str1 + ".00";
                            } else {
                                shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");
                                get_mc=MC+".00";
                            }

                        } else {

                            if (MC.length() <= 7) {
                                get_mc = MC + ".00";
                            } else {
                                get_mc=MC+".00";
                                shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");

                            }
                        }

                    } else {

                        shortToast(getApplicationContext(), "Please key-in Mandate Fields");
                    }


                    if (repair.equals("") || item.equals("") || damage.equals("")) {


                        shortToast(getApplicationContext(), "Please key-in Mandate Fields");

                    } else {

                        String[] new_get_mh = get_mh.split(Pattern.quote("."));
                        String[] new_get_mc = get_mc.split(Pattern.quote("."));

                        String str1 = new_get_mh[0].toString();
                        String str2 = new_get_mc[0].toString();

                        if (str1.length() > 7 || str2.length() > 7) {

                            shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");

                        }else
                        {

                            LineItem_Bean lb = new LineItem_Bean();
                            lb.setTariff_Code("0");
                            lb.setTariff_Group("0");
                            lb.setRPR_ESTMT_DTL_ID("1");
                            lb.setRPR_ESTMT_ID("1");
                            lb.setItem(item);
                            lb.setItemCode(get_sp_item_code);
                            lb.setRepair_Code(repir_name);
                            lb.setRepair_Code_Id(get_sp_repair_code);
                            if (sub_item.equals("")) {
                                lb.setSubItem("");
                                lb.setSuIitemCode("");

                            } else {
                                lb.setSubItem(subitem);
                                lb.setSuIitemCode(get_sp_subitem_code);
                            }

//                        lb.setSuIitemCode(get_sp_subitem_code);
                            lb.setDamage_Code(damage_name);
                            lb.setDamage_Code_Id(get_sp_damage_code);
                            lb.setManHour(get_mh);
                            lb.setManHour_Cost(MH_C);
                            lb.setMetial_Cost(get_mc);
                            lb.setMHR(MH_R);
                            lb.setResponsibility_Cd(responsibility);
                            lb.setResponsibility(responsibility_id);
                            lb.setTotel(TC);
                            lb.setRemark(remarks);
                            lb.setRowState("Added");
                            int index = 0;
               /* for(int i=0;i<LineItemlist.size();i++)
                {
                    index=index + LineItemlist.;
                }*/
                            Log.i("LineItemlist before", String.valueOf(lineitem_arraylist.size()));
                            lineitem_arraylist.add(lb);

                            object1 = new JSONObject();
                            JSONObject object_d = new JSONObject();
                            JSONArray json_array = new JSONArray();
                            try {
                                totel_amount=0;
                                String array_size = String.valueOf(GlobalConstants.lineItem_beanArrayList.size());
                                Log.i("GlobalCons.lineItem", array_size);
                                //    final_array_list.addAll(GlobalConstants.lineItem_beanArrayList);
                                for (int i = 0; i < lineitem_arraylist.size(); i++) {

                                    if (lineitem_arraylist.get(i).getResponsibility_Cd().equalsIgnoreCase("I")) {
                                        Invoice_party = Invoice_party + 1;
                                    }
                                    object2 = new JSONObject();

                                    object2.put("RPR_ESTMT_DTL_ID", lineitem_arraylist.get(i).getRPR_ESTMT_DTL_ID());
                                    object2.put("RPR_ESTMT_ID", lineitem_arraylist.get(i).getRPR_ESTMT_ID());
                                    object2.put("Tariff_Code", lineitem_arraylist.get(i).getTariff_Code());
                                    object2.put("Tariff_Group", lineitem_arraylist.get(i).getTariff_Group());
                                    object2.put("ItemCd", lineitem_arraylist.get(i).getItem());
                                    object2.put("Item", lineitem_arraylist.get(i).getItem());
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
                                    object2.put("RowState", lineitem_arraylist.get(i).getRowState());
                                    totel_amount = totel_amount+(int) ((Float.parseFloat(lineitem_arraylist.get(i).getTotel())));

                                    json_array.put(object2);
                                }
                                object_d.put("Response", json_array);
                                object1.put("d", object_d);
                                tv_amount.setText(String.valueOf(totel_amount) + ".00 " + " " + GlobalConstants.currency);
                                LL_summary.setVisibility(View.GONE);
                                GlobalConstants.Invoice_party = Invoice_party;
                                Log.i("lineitem_arraylist_json", String.valueOf(object1));
                                GlobalConstants.Line_item_Json = String.valueOf(object1);
                                GlobalConstants.totale_amount = String.valueOf(totel_amount);

                                GlobalConstants.lineItem_beanArrayList = lineitem_arraylist;

                                shortToast(getApplicationContext(), "LineItem Saved");
                                SharedPreferences.Editor editor = sp.edit();
                                editor.putString(SP_ADD_LINE_ITEM_JSON, String.valueOf(object1));
                                editor.commit();
                                tv_remark.setText("");
                                tv_item.setText("");
                                tv_sub_item.setText("");
                                tv_repair_cost.setText("");
                                tv_damage_code.setText("");
                                tv_remark.setText("");
                                manhour.setText("0.00");
                                ArrayAdapter<String> Repair_sp_responsibilitys = new ArrayAdapter<String>(getApplicationContext(), R.layout.spinner_text, responsibilitys);
                                Repair_sp_responsibilitys.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                                sp_responsibilitys.setAdapter(Repair_sp_responsibilitys);
                                tv_manhour_cost.setText("0.00");
                                tv_metrial_cost.setText("0.00");

                                //Tariff_RepairItemcode_ArrayList,Tariff_DamageItemcode_ArrayList,Tariff_SubItemcode_ArrayList,Tariff_Itemcode_ArrayList
                                adapter = new UserListAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.line_item_row, lineitem_arraylist, Tariff_Itemcode_ArrayList, Tariff_SubItemcode_ArrayList, Tariff_DamageItemcode_ArrayList, Tariff_RepairItemcode_ArrayList);
                                list_line_items.setAdapter(adapter);
                                GlobalConstants.add_lineItem_beanArrayList = lineitem_arraylist;
                                String s = String.valueOf(GlobalConstants.add_lineItem_beanArrayList.size());
                                Log.i("add_lineItem_ArrayList", s);

                            } catch (Exception e) {

                                Log.i("Exception---", String.valueOf(e));
                            }

//                      add_dialog.dismiss();
                        }
                    }
                }catch (Exception e)
                {
                    e.printStackTrace();
//                    shortToast(getApplicationContext(), "Invalid Material Cost or Man Hour. Range must be from 0.01 to 9999999.99");

                }
            }
        });

        add_dialog.show();
    }

    public class Get_Repair_Pending_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray LineItems_Json;
        private JSONArray attchement_Json;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
                jsonObject.put("PageName","Repair Approval");


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                object1=jsonrootObject;
                GlobalConstants.Line_item_Json= String.valueOf(object1);
                GlobalConstants.lineItem_beanArrayList= lineitem_arraylist;

                Log.d("rep_my submit_object1", String.valueOf(object1));
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

                        repair_arraylist = new ArrayList<RepairBean>();


                        for (int j = 0; j < jsonarray.length(); j++) {

                            repair_bean = new RepairBean();
                            jsonObject = jsonarray.getJSONObject(j);

                            if(GlobalConstants.equipment_no.equalsIgnoreCase(jsonObject.getString("EquipmentNo")))
                            {

                                LineItems_Json= jsonObject.getJSONArray("LineItems");

                                for (int i = 0; i < LineItems_Json.length(); i++) {

                                    lineitem_bean = new LineItem_Bean();
                                    JSONObject  jsonObject_line = LineItems_Json.getJSONObject(i);

                                    lineitem_bean.setTariff_Code(get_sp_tariff_code);
                                    lineitem_bean.setTariff_Group(get_sp_tariffGroup_code);
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
                                    lineitem_bean.setMetial_Cost(jsonObject_line.getString("MC"));
                                    lineitem_bean.setMHR(jsonObject_line.getString("MHR"));
                                    lineitem_bean.setResponsibility_Cd(jsonObject_line.getString("ResponsibilityCd"));
                                    lineitem_bean.setResponsibility(jsonObject_line.getString("Responsibility"));
                                    lineitem_bean.setTotel(jsonObject_line.getString("TC"));
                                    lineitem_bean.setRemark(jsonObject_line.getString("DmgRprRemarks"));
                                    lineitem_bean.setDamageDescription(jsonObject_line.getString("DamageDescription"));
                                    lineitem_bean.setRepairDescription(jsonObject_line.getString("RepairDescription"));
                                    lineitem_bean.setCheckButton(jsonObject_line.getString("CheckButton"));
                                    lineitem_bean.setChangingBit(jsonObject_line.getString("ChangingBit"));
                                    lineitem_bean.setRowState("Modified");

                                    lineitem_arraylist.add(lineitem_bean);
                                    totel_amount=totel_amount+(int) (Float.parseFloat(jsonObject_line.getString("TC")));


                                }

                               ttach_encodeArray=new ArrayList<Image_Bean>();
                                attchement_Json = jsonObject.getJSONArray("attchement");

                                for (int i = 0; i < attchement_Json.length(); i++) {

                                    image_bean = new Image_Bean();

                                    JSONObject jsonObject_line = attchement_Json.getJSONObject(i);


                                    image_bean.setImage_Name(jsonObject_line.getString("fileName"));
                                    image_bean.setImage(jsonObject_line.getString("imageUrl"));
                                    image_bean.setUriPath(jsonObject_line.getString("attchPath"));
                                    image_bean.setAttachment_Id(jsonObject_line.getString("attchId"));


                                    try {
//                                                 https://nupel.ufba.br/sites/nupel.ufba.br/files/logo_1_0.png
                                        java.net.URL url = new java.net.URL(jsonObject_line.getString("imageUrl"));
                                        HttpURLConnection connection = (HttpURLConnection) url
                                                .openConnection();
                                        connection.setDoInput(true);
                                        connection.connect();
                                        InputStream input = connection.getInputStream();
                                        selectedImageBitmap = BitmapFactory.decodeStream(input);
                                        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
                                        selectedImageBitmap.compress(Bitmap.CompressFormat.JPEG, 100, byteArrayOutputStream);
                                        byte[] byteArrayImage = byteArrayOutputStream.toByteArray();
                                        encodedImage = Base64.encodeToString(byteArrayImage, Base64.DEFAULT);
                                        image_bean.setBase64(encodedImage);

                                        Log.i("encodedImage==",encodedImage);
                                    } catch (IOException e) {
                                        e.printStackTrace();
                                    }

//                                        image_arraylist.add(image_bean);
                                    ttach_encodeArray.add(image_bean);
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

            if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();
            tv_amount.setText(String.valueOf(totel_amount)+".00"+" "+GlobalConstants.currency);
            LL_summary.setVisibility(View.GONE);

            if(lineitem_arraylist!=null)
            {

                try {
                    notification_text.setText(String.valueOf(ttach_encodeArray.size()));
                    GlobalConstants.attach_count=String.valueOf(ttach_encodeArray.size());

                } catch (Exception e) {

                }
                try {
                      object1 = new JSONObject();
                    JSONObject object_d = new JSONObject();
                    JSONArray json_array = new JSONArray();
                    for (int i = 0; i < lineitem_arraylist.size(); i++) {

                        if (lineitem_arraylist.get(i).getResponsibility_Cd().equalsIgnoreCase("I")) {
                            Invoice_party = Invoice_party + 1;
                        }
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
                        object2.put("RowState", lineitem_arraylist.get(i).getRowState());
                        object2.put("DmgRprRemarks", lineitem_arraylist.get(i).getRemark());
                        json_array.put(object2);
                    }
                    object_d.put("Response", json_array);
                    object1.put("d", object_d);
                    GlobalConstants.Line_item_Json= String.valueOf(object1);
                    GlobalConstants.lineItem_beanArrayList= lineitem_arraylist;
                    SharedPreferences.Editor editor = sp.edit();
                    editor.putString(SP_ADD_LINE_ITEM_JSON, String.valueOf(object1));
                    editor.commit();
                }catch (Exception e)
                {
                    Log.i("Exception", String.valueOf(e));
                }


                new Item_details().execute();


            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }
        }

    }

    public class Get_Repair_MySubmit_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray LineItems_Json;
        private JSONArray attchement_Json;
        private Image_Bean image_bean;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
                jsonObject.put("PageName","Repair Approval");


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                object1=jsonrootObject;
                GlobalConstants.Line_item_Json= String.valueOf(object1);
                GlobalConstants.lineItem_beanArrayList= lineitem_arraylist;

                Log.d("rep_my submit_object1", String.valueOf(object1));
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

                        repair_arraylist = new ArrayList<RepairBean>();
                        lineitem_arraylist = new ArrayList<LineItem_Bean>();

                        original_tc_amount=0;
                        for (int j = 0; j < jsonarray.length(); j++) {

                            repair_bean = new RepairBean();
                            jsonObject = jsonarray.getJSONObject(j);

                            if(GlobalConstants.equipment_no.equalsIgnoreCase(jsonObject.getString("EquipmentNo")))
                            {

                                LineItems_Json= jsonObject.getJSONArray("LineItems");

                                for (int i = 0; i < LineItems_Json.length(); i++) {

                                    lineitem_bean = new LineItem_Bean();
                                    JSONObject  jsonObject_line = LineItems_Json.getJSONObject(i);

                                    lineitem_bean.setTariff_Code("0");
                                    lineitem_bean.setTariff_Group("0");
                                    lineitem_bean.setRPR_ESTMT_DTL_ID(jsonObject_line.getString("RPR_ESTMT_DTL_ID"));
                                    lineitem_bean.setRPR_ESTMT_ID(jsonObject_line.getString("RPR_ESTMT_ID"));
                                    lineitem_bean.setItem(jsonObject_line.getString("Item"));
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
                                    lineitem_bean.setRowState("Modified");

                                    lineitem_arraylist.add(lineitem_bean);
                                    totel_amount=totel_amount+(int) (Float.parseFloat(jsonObject_line.getString("TC")));
                                    original_tc_amount = original_tc_amount + (int) (Float.parseFloat(jsonObject_line.getString("TC")));


                                }
                                ttach_encodeArray=new ArrayList<Image_Bean>();
                                attchement_Json = jsonObject.getJSONArray("attchement");

                                for (int i = 0; i < attchement_Json.length(); i++) {

                                    image_bean = new Image_Bean();

                                    JSONObject jsonObject_line = attchement_Json.getJSONObject(i);


                                    image_bean.setImage_Name(jsonObject_line.getString("fileName"));
                                    image_bean.setImage(jsonObject_line.getString("imageUrl"));
                                    image_bean.setUriPath(jsonObject_line.getString("attchPath"));
                                    image_bean.setAttachment_Id(jsonObject_line.getString("attchId"));


                                    try {
//                                                 https://nupel.ufba.br/sites/nupel.ufba.br/files/logo_1_0.png
                                        java.net.URL url = new java.net.URL(jsonObject_line.getString("imageUrl"));
                                        HttpURLConnection connection = (HttpURLConnection) url
                                                .openConnection();
                                        connection.setDoInput(true);
                                        connection.connect();
                                        InputStream input = connection.getInputStream();
                                        selectedImageBitmap = BitmapFactory.decodeStream(input);
                                        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
                                        selectedImageBitmap.compress(Bitmap.CompressFormat.JPEG, 100, byteArrayOutputStream);
                                        byte[] byteArrayImage = byteArrayOutputStream.toByteArray();
                                        encodedImage = Base64.encodeToString(byteArrayImage, Base64.DEFAULT);
                                        image_bean.setBase64(encodedImage);

                                        Log.i("encodedImage==",encodedImage);
                                    } catch (IOException e) {
                                        e.printStackTrace();
                                    }

//                                        image_arraylist.add(image_bean);
                                    ttach_encodeArray.add(image_bean);
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

            if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();

            if(lineitem_arraylist!=null)
            {
                try {
                    object1 = new JSONObject();
                    JSONObject object_d = new JSONObject();
                    JSONArray json_array = new JSONArray();
                    for (int i = 0; i < lineitem_arraylist.size(); i++) {

                        if (lineitem_arraylist.get(i).getResponsibility_Cd().equalsIgnoreCase("I")) {
                            Invoice_party = Invoice_party + 1;
                        }
                        object2 = new JSONObject();
                        object2.put("RPR_ESTMT_DTL_ID", lineitem_arraylist.get(i).getRPR_ESTMT_DTL_ID());
                        object2.put("RPR_ESTMT_ID", lineitem_arraylist.get(i).getRPR_ESTMT_ID());
                        object2.put("Tariff_Code", lineitem_arraylist.get(i).getTariff_Code());
                        object2.put("Tariff_Group", lineitem_arraylist.get(i).getTariff_Group());
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
                        object2.put("RowState", lineitem_arraylist.get(i).getRowState());
                        json_array.put(object2);
                    }
                    object_d.put("Response", json_array);
                    object1.put("d", object_d);
                    GlobalConstants.Line_item_Json= String.valueOf(object1);
                    GlobalConstants.lineItem_beanArrayList= lineitem_arraylist;
                    GlobalConstants.Invoice_party= Invoice_party;
                    SharedPreferences.Editor editor = sp.edit();
                    editor.putString(SP_ADD_LINE_ITEM_JSON, String.valueOf(object1));
                    editor.putString(SP_ORG_APP_TC_AMT, String.valueOf(original_tc_amount));

                    editor.commit();
                }catch (Exception e)
                {
                    Log.i("Exception", String.valueOf(e));
                }

                Log.d("rep_object1", String.valueOf(object1));
                tv_amount.setText(String.valueOf(totel_amount)+".00 "+GlobalConstants.currency);
                Log.d("after_submit_object1",String.valueOf(object1));
                LL_summary.setVisibility(View.GONE);
                try {
                    GlobalConstants.attach_count=String.valueOf(ttach_encodeArray.size());
                    notification_text.setText(String.valueOf(ttach_encodeArray.size()));

                } catch (Exception e) {

                }
                new Item_details().execute();

            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }
        }

    }
    public class Delete_Line_itens extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray LineItems_Json;
        private String getJsonObject;
        private String getStatus;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLRepairEstimate_Line_Item_Delete);
            httpPost.setHeader("Content-Type", "application/json");

            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("bv_strEstimateId",GlobalConstants.Line_item_id);


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("req", String.valueOf(jsonObject));
                Log.d("rep", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");
                getStatus = getJsonObject.getString("Status");




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


            try {
                if (getJsonObject.equalsIgnoreCase("Success")) {
                    shortToast(getApplicationContext(), "LineItemDeleted Succesfully");
                    GlobalConstants.from = "MysubmitlineItem";
                    finish();
                    startActivity(getIntent());
                } else {
                    shortToast(getApplicationContext(), "LineItemDeleted Deleted Failed");

                }
            }catch (Exception e)
            {
                finish();
                startActivity(getIntent());
            }
            if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();

        }

    }
    public class InvoiceParty_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
                jsonObject.put("StatusName","");


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
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("INVCNG_PRTY_CD"));
                            customer_DropdownBean.setCode(jsonObject.getString("INVCNG_PRTY_ID"));
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


            if (dropdown_customer_list != null) {


                PopUp_invoice_party(Invoice_worldlist,CustomerDropdown_Invoice_ArrayList);

            } else {
                shortToast(getApplicationContext(), "Data Not Found");

            }
            if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();

        }

    }
    private void PopUp_invoice_party(ArrayList<String> invoice_worldlist, final ArrayList<CustomerDropdownBean> customerDropdown_Invoice_ArrayList) {

        dialog = new Dialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
        dialog.setContentView(R.layout.invoice_party_popup);
        Button btn_invoice_ok = (Button) dialog.findViewById(R.id.bt_ok);
        TextView tv_name = (TextView) dialog.findViewById(R.id.tv_name);
        final EditText ed_get = (EditText) dialog.findViewById(R.id.edit);
        Button btn_close = (Button) dialog.findViewById(R.id.tv_resend);
        final CustomSpinner sp_invoice_item = (CustomSpinner) dialog.findViewById(R.id.item_spinner);
        tv_name.setText("Invoice Party");
        btn_invoice_ok.setText("Save");
        dialog.show();
        ed_get.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sp_invoice_item.performClick();
            }
        });
        ArrayAdapter<String> CustomerAdapter = new SpinnerCustomAdapter(Repair_Estimation_Approvel_wizard_MySubmit.this, R.layout.spinner_text, CustomerDropdown_Invoice_ArrayList);
        CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        sp_invoice_item.setAdapter(CustomerAdapter);

        sp_invoice_item.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {

                if (invoice_count != 0) {
                    String id=customerDropdown_Invoice_ArrayList.get(i).getCode();
                    String repairType_name=customerDropdown_Invoice_ArrayList.get(i).getName();
                    ed_get.setText(repairType_name);
                    GlobalConstants.InvoiceParty_name = repairType_name;
                    GlobalConstants.InvoiceParty_Id = id;
                    GlobalConstants.position = String.valueOf(i);
                    Log.i("InvoiceParty_Id",id);
                    Invoice_party = Invoice_party + 1;
                }
                invoice_count++;
                Invoice_party = Invoice_party + 1;
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        btn_invoice_ok.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(!ed_get.getText().toString().equals("")) {
                    invoice_count=0;
                    GlobalConstants.InvoiceParty_name = ed_get.getText().toString();
                    dialog.dismiss();
                }else {
                    shortToast(getApplicationContext(),"Please select Invoicing Party, as one of the Repair Item “Res” is indicated as I");
                }
            }
        });
        btn_close.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                invoice_count=0;
                dialog.dismiss();
            }
        });

    }
    public class ItemAliasSettingsList extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURL_ItemAliasSettingsList);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("KY_NAM", "044");


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("res_settings", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");
                lable_item = getJsonObject.getString("SettingsDirect");

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

            if(jsonarray!=null) {
                SharedPreferences.Editor editor = sp.edit();
                editor.putString(SP_ITEM_LABLE, lable_item);

                editor.commit();
            }
            else
            {
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
            progressDialog = new ProgressDialog(Repair_Estimation_Approvel_wizard_MySubmit.this);
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
                jsonObject.put("pageName","Repair Approval");
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
