package com.i_tankdepo;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.app.TimePickerDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.support.annotation.RequiresApi;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.text.Editable;
import android.text.Html;
import android.text.InputFilter;
import android.text.InputType;
import android.text.Spanned;
import android.text.TextWatcher;
import android.text.method.DigitsKeyListener;
import android.util.Base64;
import android.util.Log;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.AutoCompleteTextView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.TimePicker;
import android.widget.Toast;
import com.i_tankdepo.Beanclass.CustomerDropdownBean;
import com.i_tankdepo.Beanclass.EquipMent_Info_Bean;
import com.i_tankdepo.Beanclass.EquipmentDropdownBean;
import com.i_tankdepo.Beanclass.Equipment_Info_TypeDropdownBean;
import com.i_tankdepo.Beanclass.Multi_Photo_Bean;
import com.i_tankdepo.Beanclass.Previous_CargoDropdownBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.SQLite.DBAdapter;
import com.i_tankdepo.Util.Utility;
import com.i_tankdepo.adapter.SpinnerCustomAdapter;
import com.i_tankdepo.adapter.SpinnerCustomAdapter_Cargo;
import com.i_tankdepo.adapter.SpinnerCustomAdapter_last_type;
import com.i_tankdepo.customcomponents.CustomSpinner;
import com.i_tankdepo.helper.ServiceHandler;

import org.apache.commons.io.FilenameUtils;
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
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.DecimalFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.regex.Pattern;

import static android.inputmethodservice.Keyboard.KEYCODE_DELETE;

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

public class Create_GateIn extends CommonActivity   {

    ImageView up,more_up,equip_up,down,more_down,equip_down;
    LinearLayout LL_general_info;
    private TextView tv_toolbarTitle,tv_add,tv_name,tv_equip_no,tv_type,tv_code,tv_status,tv_date,tv_time,tv_cargo;
    private ImageView menu,im_date,im_time,im_Attachment,im_manuf_date,iv_back,im_last_Testdate;
    private DrawerLayout drawer;
    private Toolbar toolbar;
    private Intent mServiceIntent;
    private String ed_last_type_id="",ed_next_type_id="",userChoosenTask;
    private boolean imV_manuf_date=false,manuf_date=false,last_test_date=false,last_test_date_im=false,in_date=false,im_in_date=false;
    private Button fotter_add,im_add,im_print,bt_home,bt_refresh,fotter_submit;
    private Spinner sp_equip_type;
    private CustomSpinner sp_last_test_type;
    private EditText ed_customer,ed_last_testtype,ed_time,ed_attach,ed_date,ed_equipement,ed_code,ed_status,ed_preCargo,ed_location,
            ed_eir_no,ed_vechicle,ed_transport,ed_remark;
    private EditText ed_manuf_date,ed_tare_weight,ed_Gross_weight,ed_capacity,ed_last_survey,ed_last_test_date,
            ed_next_date,ed_info_remark,switch_active,switch_remtal,ed_next_type;
    static final int DATE_DIALOG_ID = 1999;
    private Calendar c;
    private int year,month,day,second;

    private String systemDate,get_equipment,get_date,get_time,get_code,get_status,get_location,get_vechicle,get_transport,get_eir_no,get_remark;

    //New Fields - added by Anantha krishnan
    private String get_consignee_name,get_consignee_location;

    private EditText ed_consignee_name,ed_consignee_location;

    private String curTime,get_swt_heating,get_swt_rental,get_swt_info_active,get_swt_info_rental;
    int mHour,mMinute;
    TimePickerDialog timePickerDialog;
    String get_manu_date,get_tare_weight,get_gross, get_capacity,get_last_survy,get_last_test_date,get_last_test_type="",get_next_date,get_next_type="",
            get_info_remark;
    String EIEQPMNT_TYP_CD,EIMNFCTR_DT,EITR_WGHT_NC,EIGRSS_WGHT_NC,EICPCTY_NC,
            EILST_SRVYR_NM,EILST_TST_DT,EILST_TST_TYP_ID,EINXT_TST_DT,EINXT_TST_TYP_ID,EIRMRKS_VC,EIACTV_BT,EIRNTL_BT;
    ArrayList<String[]> dropdown_customer_list = new ArrayList<>();
    private String CustomerName,CustomerCode,EquipmentType,EquipmentCode,PreviousCargoID,PreviousCargoCode,PreviousCargoDescription,ENM_ID,ENM_Code;
    List<String> Cust_name = new ArrayList<>();
    List<String> Cust_code = new ArrayList<>();

    ArrayList<String> dropdown_equipment_list = new ArrayList<>();
    List<String> Equip_Type = new ArrayList<>();
    List<String> Equip_Code = new ArrayList<>();

    ArrayList<String> dropdown_PreviousCargo_list = new ArrayList<>();
    ArrayList<String> dropdown_MoreInfo_list = new ArrayList<>();
    ArrayList<Previous_CargoDropdownBean> dropdown_PreviousCargo_arraylist = new ArrayList<>();
    List<String> Cargo_ID = new ArrayList<>();
    List<String> Cargo_Code = new ArrayList<>();
    List<String> Cargo_Description = new ArrayList<>();

    ArrayList<String> dropdown_equipment_info_list = new ArrayList<>();
    ArrayList<Equipment_Info_TypeDropdownBean> dropdown_equipment_info_arraylist = new ArrayList<>();
    List<String> ENM_ID_LIST = new ArrayList<>();
    List<String> ENM_Code_LIST = new ArrayList<>();

    LinearLayout LL_Equipment_Info,LL_Submit,LL_footer_delete,footer_add_btn;
    private int pendingsize;
    Switch heating,rental,info_rental,info_active;
    private String get_sp_customer,get_sp_equipe,get_sp_previous,get_sp_previous_dec,get_sp_previous_id;
    private CustomerDropdownBean customer_DropdownBean;
    ArrayList<CustomerDropdownBean> CustomerDropdownArrayList = new ArrayList<>();
    private ArrayList<String> worldlist;
    private String get_sp_customer_code,get_sp_equipe_code,get_sp_previous_code;
    private EquipmentDropdownBean equipment_DropdownBean;
    private ArrayList<EquipmentDropdownBean> dropdown_equipment_arraylist;
    private Previous_CargoDropdownBean cargo_DropdownBean;
    private String equipement_code,equipement_id;
    public static boolean isCamPermission;
    private int REQUEST_CAMERA = 0, SELECT_FILE = 1;
    private String TAG="Create_GateIn";
    private String encodedImage,selectedImagePath,filename,IfAttchment="False";
    private File file;
    private String validationStatus;
    private JSONObject reqObj;
    private ArrayList<EquipMent_Info_Bean> equipmentBeanArrayList;
    private EquipMent_Info_Bean equipment_info;
    private String changes;
    private JSONObject getJsonObject;
    private Equipment_Info_TypeDropdownBean typeDropdownBean;
    private ArrayList<Equipment_Info_TypeDropdownBean> dropdown_MoreInfo_arraylist;
    private Equipment_Info_TypeDropdownBean moreInfo_DropdownBean;
    private String infoId,infoCode;
    private String getStatus,getYardLocation,getEquipment_Type_code,getEquipment_Type;
    private ImageView iv_changeOfStatus;

    private String imageName;
    private String filePath;
    private String CaptureValue;
    private String Letter,Number;
    private DBAdapter db;
    private String clickMessage="";
    private static final int CustomGallerySelectId = 1;//Set Intent Id
    public static final String CustomGalleryIntentKey = "ImageArray";
    private ArrayList<Multi_Photo_Bean> encodeArray;
    private Bitmap selectedImageBitmap;
    private String Filename;
    private Multi_Photo_Bean photo_attach_bean;
    private ArrayList<String> list;
    private String Str="";
    private int count=0;
    private int pre_count=0;
    private int last_test_type_count=0;
    private CustomSpinner sp_previous_cargo,sp_customer;
    private SpinnerCustomAdapter customAdapter;
    private SpinnerCustomAdapter_Cargo customAdapter_cargo;
    private SpinnerCustomAdapter_last_type customAdapter_last_test_type;
    private RecyclerView list_item;
    private ViewHolder holder;
    private VerticalAdapter verticalAdapter;
    private String SettingDirect,equipment_code_id, equipment_Type_cd, equipment_code, equipment_Type_id;
    private String check_digit;
    private String SettingsDirect;


    @RequiresApi(api = Build.VERSION_CODES.JELLY_BEAN)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.create_gate_in);
        menu=(ImageView)findViewById(R.id.iv_menu) ;
        iv_back = (ImageView)findViewById(R.id.iv_back);
        menu.setVisibility(View.GONE);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

        systemDate = new SimpleDateFormat("dd-MM-yyyy").format(new Date());


        db = new DBAdapter(Create_GateIn.this);

        //  id access from XMl

        CaptureValue = GlobalConstants.fullname;
        pendingsize= GlobalConstants.pendingcount;
        tv_name = (TextView)findViewById(R.id.tv_name);
        tv_equip_no = (TextView)findViewById(R.id.tv_equip_no);
        tv_type = (TextView)findViewById(R.id.tv_type);
        tv_code = (TextView)findViewById(R.id.tv_code);
        tv_status = (TextView)findViewById(R.id.tv_status);
        tv_date = (TextView)findViewById(R.id.tv_date);
        list_item = (RecyclerView)findViewById(R.id.list_item);
        list_item.setVerticalScrollBarEnabled(true);
        list_item.setOnTouchListener(new ListView.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                int action = event.getAction();
                switch (action) {
                    case MotionEvent.ACTION_DOWN:
                        // Disallow ScrollView to intercept touch events.
                        v.getParent().requestDisallowInterceptTouchEvent(true);
                        break;

                    case MotionEvent.ACTION_UP:
                        // Allow ScrollView to intercept touch events.
                        v.getParent().requestDisallowInterceptTouchEvent(false);
                        break;
                }

                // Handle ListView touch events.
                v.onTouchEvent(event);
                return true;
            }
        });

        LL_footer_delete = (LinearLayout) findViewById(R.id.LL_footer_delete);
        footer_add_btn = (LinearLayout) findViewById(R.id.footer_add_btn);
        LL_footer_delete.setAlpha(0.5f);
        LL_footer_delete.setClickable(false);
        footer_add_btn.setAlpha(0.5f);
        footer_add_btn.setClickable(false);
        tv_time = (TextView)findViewById(R.id.tv_time);
        tv_cargo = (TextView)findViewById(R.id.tv_cargo);
        ed_date = (EditText)findViewById(R.id.ed_date);
        ed_time = (EditText)findViewById(R.id.ed_time);
        ed_last_testtype = (EditText)findViewById(R.id.ed_last_testtype);
        ed_last_testtype.setOnClickListener(this);
        ed_customer = (EditText)findViewById(R.id.ed_customer);
        ed_customer.setOnClickListener(this);
        ed_attach = (EditText)findViewById(R.id.ed_attach);
        heating=(Switch)findViewById(R.id.switch_heating);
        rental=(Switch)findViewById(R.id.switch_rental);
        rental.setEnabled(false);
        info_active=(Switch)findViewById(R.id.swt_moreinfo_Active);
        info_rental=(Switch)findViewById(R.id.swt_moreinfo_remtal);
        ed_equipement = (EditText)findViewById(R.id.ed_equip_no);
        ed_equipement.setOnKeyListener(new View.OnKeyListener() {
            @Override
            public boolean onKey(View v, int keyCode, KeyEvent event) {
                //You can identify which key pressed buy checking keyCode value with KeyEvent.KEYCODE_
                if(keyCode == KeyEvent.KEYCODE_DEL) {
                    //this is for backspace
                }
                return false;
            }
        });
        if(GlobalConstants.from.equalsIgnoreCase("ocr")) {
            ed_equipement.setText(GlobalConstants.blocks_numbers.replaceAll(" ",""));

            Log.i("blocks_numbers",GlobalConstants.blocks_numbers.replaceAll(" ",""));
        }else if(GlobalConstants.from.equalsIgnoreCase("skip"))
        {
            ed_equipement.setText("");
        }
        ed_equipement.addTextChangedListener(mTextEditorWatcher);

        ed_code = (EditText)findViewById(R.id.ed_code);

        ed_manuf_date=(EditText)findViewById(R.id.ed_manfu);
        ed_tare_weight=(EditText)findViewById(R.id.ed_tare_weight);
        ed_tare_weight.setKeyListener(DigitsKeyListener.getInstance(true,true));
        ed_Gross_weight=(EditText)findViewById(R.id.ed_gross_weight);
        ed_Gross_weight.setKeyListener(DigitsKeyListener.getInstance(true,true));
        ed_capacity=(EditText)findViewById(R.id.ed_capacity);
        ed_capacity.setKeyListener(DigitsKeyListener.getInstance(true,true));
        ed_last_survey=(EditText)findViewById(R.id.ed_Last_survay);
        ed_last_test_date=(EditText)findViewById(R.id.ed_test_date);
        sp_last_test_type=(CustomSpinner) findViewById(R.id.sp_last_testtype);
        ed_next_date=(EditText)findViewById(R.id.ed_next_date);
        ed_next_type=(EditText)findViewById(R.id.ed_next_testtype);
        ed_info_remark=(EditText)findViewById(R.id.ed_info_remark);
        im_manuf_date = (ImageView)findViewById(R.id.im_manuf_date);



        ed_eir_no = (EditText)findViewById(R.id.ed_eir_no);
        ed_eir_no.setFilters(new InputFilter[] {
                new InputFilter.AllCaps() {
                    @Override
                    public CharSequence filter(CharSequence source, int start, int end, Spanned dest, int dstart, int dend) {
                        return String.valueOf(source).toUpperCase();
                    }
                },
                new InputFilter.LengthFilter(17)
        });

        //New Fields - Added by Anantha krishnan
        ed_consignee_name = (EditText)findViewById(R.id.ed_consignee_name);
        ed_consignee_location = (EditText)findViewById(R.id.ed_consignee_location);

        ed_location = (EditText)findViewById(R.id.ed_location);
        ed_preCargo = (EditText)findViewById(R.id.ed_preCargo);
        ed_preCargo.setOnClickListener(this);
        ed_status = (EditText)findViewById(R.id.ed_status);
        ed_vechicle = (EditText)findViewById(R.id.ed_vechicle);
        ed_transport = (EditText)findViewById(R.id.ed_transport);
        ed_remark = (EditText)findViewById(R.id.ed_remarks);
        im_date =(ImageView)findViewById(R.id.im_date);
        im_Attachment =(ImageView)findViewById(R.id.im_Attachment);
        im_time =(ImageView)findViewById(R.id.im_time);

        im_last_Testdate = (ImageView)findViewById(R.id.im_last_Testdate);
        im_last_Testdate.setOnClickListener(this);
        im_manuf_date.setOnClickListener(this);

        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        LL_Equipment_Info = (LinearLayout) findViewById(R.id.LL_Equipment_Info);
        LL_Submit = (LinearLayout) findViewById(R.id.LL_Submit);
        tv_toolbarTitle.setText("Add New GateIn");

        equip_up=(ImageView)findViewById(R.id.equip_up) ;
        equip_down=(ImageView)findViewById(R.id.equip_down) ;
        fotter_add=(Button)findViewById(R.id.add);
        fotter_submit=(Button)findViewById(R.id.submit);
        fotter_submit.setOnClickListener(this);
        tv_add=(TextView)findViewById(R.id.tv_add);
        tv_add.setText("Print");
        im_add = (Button)findViewById(R.id.add);
        im_print = (Button)findViewById(R.id.print);
        im_add.setVisibility(View.GONE);
        equip_up.setVisibility(View.GONE);
        LL_Submit.setOnClickListener(this);
        im_Attachment.setOnClickListener(this);
        LL_Equipment_Info.setVisibility(View.GONE);
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);

//        ed_equipement.setText(CaptureValue);


//         click event

        ed_time.setOnClickListener(this);
        ed_date.setOnClickListener(this);
        im_date.setOnClickListener(this);
        im_time.setOnClickListener(this);
        ed_manuf_date.setOnClickListener(this);

        equip_down.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                clickMessage ="Accordion";

                LL_Equipment_Info.requestFocus();

                get_equipment = ed_equipement.getText().toString();


                if(GlobalConstants.check_digit.equalsIgnoreCase("False"))
                {
                    if(get_equipment.length()< 11 || get_equipment==null)
                    {

                        shortToast(getApplicationContext(),"Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit..!");
                        ed_equipement.requestFocus();


                    }else
                    {

//                        Digit validation from commanActivity
                        Letter = get_equipment.substring(0,4);
                        Number = get_equipment.substring(4,11);
                        int let= check_one(Letter);
                        int num=check_two(Number);
                        Log.d("let", String.valueOf(let));
                        Log.d("num-3", String.valueOf(num));
                        if ((let==4) && num==7) {
                            if (cd.isConnectingToInternet()) {
                                new Post_Verify_Equipment_No().execute();
                            } else {
                                shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                            }


                        }else{
                            shortToast(getApplicationContext(),"Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit..!");
                        }
                    }

                }else
                {


                        if (cd.isConnectingToInternet()) {
                            new Post_Verify_Equipment_No().execute();
                        } else {
                            shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                        }



                }



            }
        });
        get_swt_heating="False";
        heating.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if(b) {
                    get_swt_heating="True";
                }else
                {
                    get_swt_heating="False";

                }
            }
        });
        get_swt_rental="False";
        rental.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if(b) {

                    new Post_Verify_Rental_Entry().execute();

                }else
                {
                    get_swt_rental="False";
                }
            }
        });
        get_swt_info_rental="False";
        info_rental.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if(b) {
                    get_swt_info_rental="True";
                }else
                {
                    get_swt_info_rental="False";

                }
            }
        });
        get_swt_info_active="False";
        info_active.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if(b) {
                    get_swt_info_active="True";
                }else
                {
                    get_swt_info_active="False";
                }
            }
        });
        equip_up.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_Equipment_Info.setVisibility(View.GONE);
                equip_up.setVisibility(View.GONE);
                equip_down.setVisibility(View.VISIBLE);
            }
        });
        sp_customer = (CustomSpinner)findViewById(R.id.sp_customer);
        sp_equip_type = (Spinner)findViewById(R.id.sp_type);
        sp_previous_cargo = (CustomSpinner)findViewById(R.id.sp_previ_cargo);



        drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();




        ed_last_test_date.setOnClickListener(this);

        bt_home = (Button)findViewById(R.id.home);
        iv_changeOfStatus.setOnClickListener(this);
        bt_home.setOnClickListener(this);
        bt_refresh = (Button)findViewById(R.id.refresh);
        bt_refresh.setOnClickListener(this);
        bt_refresh.setText("Reset");
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
        SimpleDateFormat time = new SimpleDateFormat("HH:MM");
        curTime = time.format(new Date());
        systemDate = new SimpleDateFormat("dd-MMM-yyyy").format(new Date());

        ed_date.setText(systemDate);
//        ed_manuf_date.setText(systemDate);
//        ed_time.setText(curTime);

//        ed_last_test_date.setText(systemDate);



        sp_customer.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> arg0,
                                       View arg1, int position, long arg3) {

                if (count != 0) {
                    get_sp_customer_code = CustomerDropdownArrayList.get(position).getCode();
                    get_sp_customer = CustomerDropdownArrayList.get(position).getName();
                    ed_customer.setText(get_sp_customer);

                    new CheckDigitBasedonCustomer().execute();
                }
                count++;

            }

            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });


        sp_last_test_type.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {

                if (last_test_type_count>0)
                {
                    if(i == 0){
                        ed_next_type.setText(dropdown_MoreInfo_arraylist.get(1).getCode());
                        ed_next_type_id=dropdown_MoreInfo_arraylist.get(1).getId();
                        ed_last_type_id=dropdown_MoreInfo_arraylist.get(0).getId();
                        ed_last_testtype.setText(dropdown_MoreInfo_arraylist.get(0).getCode());
                    }else if(i==1)
                    {
                        ed_next_type.setText(dropdown_MoreInfo_arraylist.get(0).getCode());
                        ed_last_type_id=dropdown_MoreInfo_arraylist.get(1).getId();
                        ed_next_type_id=dropdown_MoreInfo_arraylist.get(0).getId();
                        ed_last_testtype.setText(dropdown_MoreInfo_arraylist.get(1).getCode());
//                        ed_last_testtype.setText(dropdown_MoreInfo_arraylist.get(0).getCode());
                    }
                }
                last_test_type_count++;
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });



        get_equipment="AAAL8445312";
        get_sp_customer="TSTCUST";


        if (cd.isConnectingToInternet()){

            /*
            * Settings and Dropdown services
            * */
            new Equipment_Type_Settings().execute();
            new Time_settings().execute();
            new Create_GateIn_moreInfo_list_details().execute();
            new GateIn_Default_Values().execute();


        }else
        {
            shortToast(getApplicationContext(),"Please check your Internet Connection.");
        }

        sp_equip_type.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                get_sp_equipe_code=dropdown_equipment_arraylist.get(i).getCode();
                get_sp_equipe = sp_equip_type.getSelectedItem().toString();

                // shortToast(getApplicationContext(),"get_sp_equipe_code==>"+get_sp_equipe_code);

                if(cd.isConnectingToInternet()) {
                    new PostCustomer_Code().execute();
                }else{

                    shortToast(getApplicationContext(),"Please check your Internet Connection..!");

                }

            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
      /*  if ((Str.length() < 0)|| Str.equals("")) {
            //im_Attachment.setColorFilter(Color.CYAN,PorterDuff.Mode.LIGHTEN);
            //   Toast.makeText(Update_Gateout.this,"greater",Toast.LENGTH_LONG).show();
            Animation fade = AnimationUtils.loadAnimation(this, R.anim.img_fade);
            im_Attachment.startAnimation(fade);

        } else {
            //im_Attachment.setColorFilter(Color.CYAN, PorterDuff.Mode.DARKEN);
            //  Toast.makeText(Update_Gateout.this,"0",Toast.LENGTH_LONG).show();

        }*/
        im_Attachment.setOnClickListener(this);


        sp_previous_cargo.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {

                if(pre_count>0) {
                    get_sp_previous_code = dropdown_PreviousCargo_arraylist.get(i).getCode();
                    get_sp_previous_id = dropdown_PreviousCargo_arraylist.get(i).getId();
                    get_sp_previous_dec = dropdown_PreviousCargo_arraylist.get(i).getDesc();
                    ed_preCargo.setText(get_sp_previous_dec.toUpperCase());
                    //  shortToast(getApplicationContext(),"get_sp_previous==>"+get_sp_previous);
                }
                pre_count++;
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });





        String customer = getColoredSpanned("Customer","#bbbbbb");
        String equipNo = getColoredSpanned("Equipment Number","#bbbbbb");
        String type = getColoredSpanned("Type","#bbbbbb");
        String code = getColoredSpanned("Code","#bbbbbb");
        String status = getColoredSpanned("Status","#bbbbbb");
        String date = getColoredSpanned("In Date","#bbbbbb");
        String t_time = getColoredSpanned("Time","#bbbbbb");
        String cargo = getColoredSpanned("Previous Cargo","#bbbbbb");

        String surName = getColoredSpanned("*","#cb0da5");

        tv_name.setText(Html.fromHtml(customer+" "+surName));
        tv_equip_no.setText(Html.fromHtml(equipNo+" "+surName));
        tv_type.setText(Html.fromHtml(type+" "+surName));
        tv_code.setText(Html.fromHtml(code+" "+surName));
        tv_status.setText(Html.fromHtml(status+" "+surName));
        tv_date.setText(Html.fromHtml(date+" "+surName));
        tv_time.setText(Html.fromHtml(t_time+" "+surName));
        tv_cargo.setText(Html.fromHtml(cargo+" "+surName));

        iv_back.setOnClickListener(this);


/*
        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        menu.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(drawer.isDrawerOpen(Gravity.START))
                    drawer.closeDrawer(Gravity.END);
                else
                    drawer.openDrawer(Gravity.START);
            }
        });

        navigationView.setNavigationItemSelectedListener(this);*/
    }
    // EditTextWacther  Implementation

    private final TextWatcher mTextEditorWatcher = new TextWatcher() {

        public void beforeTextChanged(CharSequence s, int start, int count, int after)
        {
            // When No Password Entered
        }

        public void onTextChanged(CharSequence s, int start, int before, int count)
        {

                if(s.length()>=4) {
                    ed_equipement.setInputType(InputType.TYPE_CLASS_NUMBER);
                }else if(s.length()<=3) {
                    ed_equipement.setInputType(InputType.TYPE_TEXT_FLAG_CAP_CHARACTERS);
                }
        }

        public void afterTextChanged(Editable s)
        {
            if(s.length()<=11) {
                // textViewPasswordStrengthIndiactor.setText("Password Max Length Reached");
                // shortToast(getApplicationContext(),"Please Enter 11 Digit");
            }else if(s.length()==11 )
            {
                get_sp_customer = sp_customer.getSelectedItem().toString();
                get_equipment=ed_equipement.getText().toString();
                //   new Post_Verify_Equipment_No().execute();

            }
        }
    };

    @Override
    public void onResume(){
        super.onResume();
        // put your code here...
        if(encodeArray==null)
        {
            encodeArray=new ArrayList<Multi_Photo_Bean>();
            list = new ArrayList<>();

        }

    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @Override
    public void onClick(View view) {

        switch (view.getId())
        {
            case R.id.iv_changeOfStatus:
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));
                break;
            case R.id.LL_GateIn:
                startActivity(new Intent(getApplicationContext(),GateIn.class));
                break;
            case R.id.ed_customer:

                sp_customer.performClick();
                break;
            case R.id.ed_preCargo:
                sp_previous_cargo.performClick();
                break;
            case R.id.ed_last_testtype:

                sp_last_test_type.performClick();
                break;
            case R.id.ed_date:
                manuf_date=false;
                last_test_date=false;
                in_date=true;
                im_in_date=false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_date:
                manuf_date=false;
                im_in_date=true;
                in_date=false;
                last_test_date=false;
                showDialog(DATE_DIALOG_ID);

                break;
            case R.id.ed_manfu:
                manuf_date=true;
                imV_manuf_date=false;
                last_test_date = false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_manuf_date:
                last_test_date = false;
                imV_manuf_date=true;

                showDialog(DATE_DIALOG_ID);
                break;

            case R.id.ed_test_date:
                imV_manuf_date=false;
                manuf_date=false;
                last_test_date = true;
                last_test_date_im = false;
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_last_Testdate:
                manuf_date=false;
                last_test_date = false;
                last_test_date_im=true;
                imV_manuf_date=false;

                showDialog(DATE_DIALOG_ID);
                break;

            case R.id.iv_back:
                finish();
                onBackPressed();
                break;
            case R.id.submit:

                /*
                *  Creat gatein service
                * */


                    clickMessage="Submit";
                    get_sp_customer = ed_customer.getText().toString();
                    get_equipment = ed_equipement.getText().toString();
                //    get_sp_previous = sp_previous_cargo.getSelectedItem().toString();
                    get_sp_previous = ed_preCargo.getText().toString();
                    get_status = ed_status.getText().toString();
                    get_code = ed_code.getText().toString();
                    get_date = ed_date.getText().toString();
                    get_time = ed_time.getText().toString();
                    get_location = ed_location.getText().toString();
                    get_eir_no = ed_eir_no.getText().toString().toUpperCase();
                    get_vechicle = ed_vechicle.getText().toString();
                    get_transport = ed_transport.getText().toString();
                    get_remark = ed_remark.getText().toString();

                    get_manu_date = ed_manuf_date.getText().toString();
                    get_tare_weight = ed_tare_weight.getText().toString();
                    get_gross = ed_Gross_weight.getText().toString();
                    get_last_survy = ed_last_survey.getText().toString();
                    get_capacity = ed_capacity.getText().toString();
                    get_last_test_date = ed_last_test_date.getText().toString();
                    get_next_date = ed_next_date.getText().toString();
                    get_next_type = ed_next_type.getText().toString();
                    get_info_remark = ed_info_remark.getText().toString();
                    filename = ed_attach.getText().toString();

                    //new fields - added by Anantha krishnan

                    get_consignee_name = ed_consignee_name.getText().toString();
                    get_consignee_location = ed_consignee_location.getText().toString();




                get_sp_equipe = sp_equip_type.getSelectedItem().toString();

                    try {
                        if (sp_last_test_type.getSelectedItem().toString().length() == 0 || ed_last_test_date.getText().toString().length() == 0) {

                        } else {
                            get_last_test_type = sp_last_test_type.getSelectedItem().toString();
                            get_last_test_date = ed_last_test_date.getText().toString();
                        }

                    } catch (Exception e) {
                        shortToast(getApplicationContext(), "Please Select Equipment Info");
                        get_last_test_type = "";
                        get_last_test_date = "";
                    }

                if(cd.isConnectingToInternet()) {


                    if ((get_equipment.trim().equals("") && get_equipment == null && get_equipment.length() < 11) ||
                            (get_sp_customer.trim().equals("") || get_sp_customer == null) ||
                            (get_sp_equipe.trim().equals("") || get_sp_equipe == null) ||
                            (get_code.trim().equals("") || get_code == null) ||
                            (get_sp_previous.trim().equals("") || get_sp_previous == null) ||
                            (get_time.trim().equals("") || get_time == null) ||
                            (get_date.trim().equals("") || get_date == null)) {
                        shortToast(getApplicationContext(), "Please Key-in Mandate Fields");
                    } else {



                        if (get_manu_date.equals(EIMNFCTR_DT) && get_tare_weight.equals(EITR_WGHT_NC) && get_gross.equals(EIGRSS_WGHT_NC)
                                && get_capacity.equals(EICPCTY_NC)
                                && get_last_survy.equals(EILST_SRVYR_NM) && get_last_test_date.equals(EILST_TST_DT)
                                && get_last_test_type.equals(EILST_TST_TYP_ID) && get_next_date.equals(EINXT_TST_DT)
                                && get_next_type.equals(EINXT_TST_TYP_ID) && get_info_remark.equals(EIRMRKS_VC) && get_swt_info_rental.equals(EIRNTL_BT)
                                && get_swt_info_active.equals(EIACTV_BT)) {

                            changes = "False";

                            if(GlobalConstants.check_digit.equalsIgnoreCase("False"))
                            {
                                if (get_equipment.length() < 11) {

                                    shortToast(getApplicationContext(),"Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit..!");


                                } else {
                                    Letter = get_equipment.substring(0,4);
                                    Number = get_equipment.substring(4,11);
                                    int let= check_one(Letter);
                                    int num=check_two(Number);
                                    Log.d("let", String.valueOf(let));
                                    Log.d("num-3", String.valueOf(num));
                                    if ((let==4) && num==7) {

                                        if (cd.isConnectingToInternet()) {
                                            new Post_Verify_Equipment_No().execute();

                                        } else {
                                            shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                                        }


                                    } else {
                                        shortToast(getApplicationContext(), "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit..!");
                                    }
                                }
                            }else
                            {
                                if (get_equipment.length() > 10) {

                                    shortToast(getApplicationContext(), "Check Digit is incorrect for the entered Equipment.");


                                }else {

                                    Log.d("get_equipment", get_equipment);
                                    GlobalConstants.equipment_no = get_equipment;
                                    new baseURL_CheckDigitByContainerNo().execute();

                                    get_equipment = GlobalConstants.get_equipment_sevice;
                                    Log.d("get_equipment", get_equipment);
                                }

                            }



                        } else {


                            changes = "True";

                            if(GlobalConstants.check_digit.equalsIgnoreCase("False"))
                            {
                            if (get_equipment.length() < 11) {

                                shortToast(getApplicationContext(),"Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit..!");


                            } else {

                                if (!get_tare_weight.equals("")) {



                                  /*  if ((get_tare_weight.matches(".\\d"))) {
                                        get_tare_weight = "0" + get_tare_weight + "00";
                                    } else if ((get_tare_weight.matches("\\d."))) {
                                        get_tare_weight = get_tare_weight + "000";
                                    }*/

                                    if (get_tare_weight.contains(".")) {

                                        String[] new_MH = get_tare_weight.split(Pattern.quote("."));

                                        String str1 = new_MH[0].toString();
                                        String str2 = new_MH[1].toString();
                                        if (str1.length() <= 7 && str2.length() == 2) {
                                            get_tare_weight = str1 + "." + str2 + "0";

                                        } else if (str1.length() <= 7 && (str2.length() == 1)) {
                                            get_tare_weight = str1 + "." + str2 + "00";
                                        } else if (str1.length() <= 7 && (str2.length() == 0)) {
                                            get_tare_weight = str1 + ".000";
                                        } else {
//                                            shortToast(getApplicationContext(), "Invalid Tare Weight. Range must be from 0.01 to 9999999.99");
                                            get_tare_weight = get_tare_weight + ".000";
                                        }

                                    } else {

                                        if (get_tare_weight.length() <= 7) {
                                            get_tare_weight = get_tare_weight + ".000";
                                        } else {
//                                            shortToast(getApplicationContext(), "Invalid Tare Weight. Range must be from 0.01 to 9999999.99");
                                            get_tare_weight = get_tare_weight + ".000";
                                        }
                                    }


                                } else {
                                    get_tare_weight = "";
                                }
                                if (!get_gross.equals("")) {


/*

                                    if ((get_gross.matches(".\\d"))) {
                                        get_gross = "0" + get_gross + "00";
                                    } else if ((get_gross.matches("\\d."))) {
                                        get_gross = get_gross + "000";
                                    }
*/

                                    if (get_gross.contains(".")) {

                                        String[] new_MH = get_gross.split(Pattern.quote("."));

                                        String str1 = new_MH[0].toString();
                                        String str2 = new_MH[1].toString();
                                        if (str1.length() <= 7 && str2.length() == 2) {
                                            get_gross = str1 + "." + str2 + "0";

                                        } else if (str1.length() <= 7 && (str2.length() == 1)) {
                                            get_gross = str1 + "." + str2 + "00";
                                        } else if (str1.length() <= 7 && (str2.length() == 0)) {
                                            get_gross = str1 + ".000";
                                        } else {
//                                            shortToast(getApplicationContext(), "Invalid Gross Weight. Range must be from 0.01 to 9999999.99");
                                            get_gross = get_gross + ".000";
                                        }

                                    } else {

                                        if (get_gross.length() <= 7) {
                                            get_gross = get_gross + ".000";
                                        } else {
//                                            shortToast(getApplicationContext(), "Invalid Gross Weight. Range must be from 0.01 to 9999999.99");
                                            get_gross = get_gross + ".000";
                                        }
                                    }


                                } else {
                                    get_gross = "";
                                }
                                if (!get_capacity.equals("")) {



                                  /*  if ((get_capacity.matches(".\\d"))) {
                                        get_capacity = "0" + get_capacity + "00";
                                    } else if ((get_capacity.matches("\\d."))) {
                                        get_capacity = get_capacity + "000";
                                    }*/

                                    if (get_capacity.contains(".")) {

                                        String[] new_MH = get_capacity.split(Pattern.quote("."));

                                        String str1 = new_MH[0].toString();
                                        String str2 = new_MH[1].toString();
                                        if (str1.length() <= 7 && str2.length() == 2) {
                                            get_capacity = str1 + "." + str2 + "0";

                                        } else if (str1.length() <= 7 && (str2.length() == 1)) {
                                            get_capacity = str1 + "." + str2 + "00";
                                        } else if (str1.length() <= 7 && (str2.length() == 0)) {
                                            get_capacity = str1 + ".000";
                                        } else {
//                                            shortToast(getApplicationContext(), "Invalid Capacity. Range must be from 0.01 to 9999999.99");
                                            get_capacity = get_capacity + ".000";
                                        }

                                    } else {

                                        if (get_capacity.length() <= 7) {
                                            get_capacity = get_capacity + ".000";
                                        } else {
//                                            shortToast(getApplicationContext(), "Invalid Capacity. Range must be from 0.01 to 9999999.99");
                                            get_capacity = get_capacity + ".000";
                                        }
                                    }


                                } else {
                                    get_capacity = "";
                                }


                                if (!get_capacity.equals("") || !get_tare_weight.equals("") || !get_gross.equals("")) {
                                    String[] new_get_mh = get_tare_weight.split(Pattern.quote("."));
                                    String[] new_get_mc = get_capacity.split(Pattern.quote("."));
                                    String[] new_tare_weight = get_gross.split(Pattern.quote("."));

                                    String str1, str1_2, str2, str2_2, str3, str3_2;
                                    if (get_tare_weight.equals("")) {
                                        str1 = "";
                                        str1_2 = "";
                                    } else {
                                        str1 = new_get_mh[0].toString();
                                        str1_2 = new_get_mh[1].toString();
                                    }
                                    if (get_capacity.equals("")) {
                                        str2 = "";
                                        str2_2 = "";
                                    } else {
                                        str2 = new_get_mc[0].toString();
                                        str2_2 = new_get_mc[1].toString();
                                    }
                                    if (get_gross.equals("")) {
                                        str3 = "";
                                        str3_2 = "";
                                    } else {
                                        str3 = new_tare_weight[0].toString();
                                        str3_2 = new_tare_weight[1].toString();
                                    }


                                    if (str1.length() > 7 || str1_2.length() > 3) {

                                        shortToast(getApplicationContext(), "Invalid Tare Weight. Range must be from 0.01 to 9999999.99");

                                    } else if (str2.length() > 7 || str2_2.length() > 3) {
                                        shortToast(getApplicationContext(), "Invalid Capacity. Range must be from 0.01 to 9999999.99");

                                    } else if (str3.length() > 7 || str3_2.length() > 3) {
                                        shortToast(getApplicationContext(), "Invalid Gross Weight. Range must be from 0.01 to 9999999.99");

                                    } else {
                                        Letter = get_equipment.substring(0, 4);
                                        Number = get_equipment.substring(4, 11);
                                        int let= check_one(Letter);
                                        int num=check_two(Number);
                                        Log.d("let", String.valueOf(let));
                                        Log.d("num-3", String.valueOf(num));
                                        if ((let==4) && num==7) {
                                            Log.d("Character", Letter);
                                            Log.d("Numbers-2", Number);
                                            if (cd.isConnectingToInternet()) {
                                                new Post_Verify_Equipment_No().execute();

                                            } else {
                                                shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                                            }


                                        } else {
                                            shortToast(getApplicationContext(), "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit..!");
                                        }
                                    }
                                } else {
                                    Letter = get_equipment.substring(0, 4);
                                    Number = get_equipment.substring(4, 11);

                                   int let= check_one(Letter);
                                    int num=check_two(Number);
                                    Log.d("let", String.valueOf(let));
                                    Log.d("num-3", String.valueOf(num));
                                    if ((let==4) && num==7) {

                                        Log.d("Character", Letter);
                                        Log.d("Numbers-3", Number);
                                        if (cd.isConnectingToInternet()) {
                                            new Post_Verify_Equipment_No().execute();

                                        } else {
                                            shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                                        }


                                    } else {
                                        shortToast(getApplicationContext(), "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit..!");
                                    }
                                }
                            }
                        }else {

                                if (get_equipment.length() > 10 ) {

                                    shortToast(getApplicationContext(), "Check Digit is incorrect for the entered Equipment.");


                                }else
                                {
                                    Log.d("get_equipment==", get_equipment);
                                    GlobalConstants.equipment_no=get_equipment;
                                    new baseURL_CheckDigitByContainerNo().execute();

                                    get_equipment=GlobalConstants.get_equipment_sevice;

                                    Log.d("get_equipment==", get_equipment);
                                    Log.d("get_equipment_sevice==", GlobalConstants.get_equipment_sevice);
                                }



                            }

                        }

                    }

                }else {
                    if ((get_equipment.trim().equals("") && get_equipment == null && get_equipment.length() < 11) ||
                            (get_sp_customer.trim().equals("") || get_sp_customer == null) ||
                            (get_sp_equipe.trim().equals("") || get_sp_equipe == null) ||
                            (get_code.trim().equals("") || get_code == null) ||
                            (get_sp_previous.trim().equals("") || get_sp_previous == null) ||
                            (get_time.trim().equals("") || get_time == null) ||
                            (get_date.trim().equals("") || get_date == null)) {
                        shortToast(getApplicationContext(), "Please Key-in Mandate Fields");
                    } else {

                        if (get_manu_date.equals(EIMNFCTR_DT) && get_tare_weight.equals(EITR_WGHT_NC) && get_gross.equals(EIGRSS_WGHT_NC)
                                && get_capacity.equals(EICPCTY_NC)
                                && get_last_survy.equals(EILST_SRVYR_NM) && get_last_test_date.equals(EILST_TST_DT)
                                && get_last_test_type.equals(EILST_TST_TYP_ID) && get_next_date.equals(EINXT_TST_DT)
                                && get_next_type.equals(EINXT_TST_TYP_ID) && get_info_remark.equals(EIRMRKS_VC) && get_swt_info_rental.equals(EIRNTL_BT)
                                && get_swt_info_active.equals(EIACTV_BT)) {
                            changes = "False";
                        } else {
                            changes = "True";
                        }
                        String numberAsString = Integer.toString(pendingsize + 1);
                        db.open();

                        db.insertGateIn(get_sp_customer, get_sp_customer_code, numberAsString, "new", "GateIn", numberAsString, sp.getString(SP_USER_ID, "user_Id"), "False",
                                changes, "", IfAttchment, "True", get_equipment, get_sp_equipe, get_sp_equipe_code, get_code, equipement_id, get_location, get_date, get_time,
                                get_sp_previous, get_sp_previous_id, get_sp_previous_code, get_vechicle, get_eir_no, get_transport, get_remark, get_swt_heating, get_swt_rental, get_manu_date, get_tare_weight, get_gross
                                , get_capacity, get_last_survy, get_last_test_date, get_last_test_type, get_next_date, get_next_type, get_info_remark,
                                get_swt_info_active, get_swt_info_rental,get_consignee_name,get_consignee_location);
                        db.close();

                    }
                }

                break;

            case R.id.ed_time:
                mHour = c.get(Calendar.HOUR_OF_DAY);
                mMinute = c.get(Calendar.MINUTE);

                // Launch Time Picker Dialog
                timePickerDialog = new TimePickerDialog(this,
                        new TimePickerDialog.OnTimeSetListener() {

                            @Override
                            public void onTimeSet(TimePicker view, int hourOfDay,
                                                  int minute) {
                                boolean isPM = (hourOfDay >= 12);
                                ed_time.setText(String.format("%02d:%02d", (hourOfDay == 12 || hourOfDay == 0) ? 12 : hourOfDay % 12,minute));
                                //   from_time.setText(hourOfDay + ":" + minute);
                            }
                        }, mHour, mMinute, false);
                timePickerDialog.show();
                break;
            case R.id.im_time:
                mHour = c.get(Calendar.HOUR_OF_DAY);
                mMinute = c.get(Calendar.MINUTE);

                // Launch Time Picker Dialog
                timePickerDialog = new TimePickerDialog(this,
                        new TimePickerDialog.OnTimeSetListener() {

                            @Override
                            public void onTimeSet(TimePicker view, int hourOfDay,
                                                  int minute) {
                                boolean isPM = (hourOfDay >= 12);
                                ed_time.setText(String.format("%02d:%02d", (hourOfDay == 12 || hourOfDay == 0) ? 12 : hourOfDay % 12,minute));
                                //   from_time.setText(hourOfDay + ":" + minute);
                            }
                        }, mHour, mMinute, false);
                timePickerDialog.show();
                break;
            case R.id.home:
                startActivity(new Intent(getApplicationContext(),MainActivity.class));
                break;
            case R.id.refresh:

               count=0;
               pre_count=0;
               last_test_type_count=0;
                ed_equipement.setText("");
                finish();
                startActivity(getIntent());
                break;
            case R.id.im_Attachment:

                final CharSequence[] items = { "Take Photo", "Choose from Library"};

                isCamPermission = sp2.getBoolean(SP2_CAMERA_PERM_DENIED, false);
                AlertDialog.Builder builder = new AlertDialog.Builder(Create_GateIn.this);
                builder.setTitle("Add Photo!");
                builder.setItems(items, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int item) {
                        boolean result= Utility.checkPermission(Create_GateIn.this, isCamPermission);

                        if (items[item].equals("Take Photo")) {
                            userChoosenTask ="Take Photo";
                            if(result) {
                                Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
                                try {
                                    startActivityForResult(intent, REQUEST_CAMERA);
                                }catch (Exception e)
                                {
                                  /*  isCamPermission = sp2.getBoolean(SP2_CAMERA_PERM_DENIED, false);

                                    result = Utility.checkPermission(Create_GateIn.this, isCamPermission);*/
                                    shortToast(getApplicationContext(),"Camera Permission is Required");

                                }
                            }

                        } else if (items[item].equals("Choose from Library")) {
                            userChoosenTask ="Choose from Library";
                            if(result)
                                startActivityForResult(new Intent(Create_GateIn.this, CustomGallery_Activity.class), CustomGallerySelectId);

                        }
                    }
                });
                builder.show();



                break;
        }
    }

    private void selectImage() {
        final CharSequence[] items = {  "Choose from Library"};

        isCamPermission = sp2.getBoolean(SP2_CAMERA_PERM_DENIED, false);
        AlertDialog.Builder builder = new AlertDialog.Builder(Create_GateIn.this);
        builder.setTitle("Add Photo!");
        builder.setItems(items, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int item) {
                boolean result= Utility.checkPermission(Create_GateIn.this, isCamPermission);

                if (items[item].equals("Choose from Library")) {
                    userChoosenTask ="Choose from Library";
                    if(result)
                        startActivityForResult(new Intent(Create_GateIn.this, CustomGallery_Activity.class), CustomGallerySelectId);

                }

                if (items[item].equals("Take Photo")) {
                    userChoosenTask ="Take Photo";
                    if(result)
                        cameraIntent();

                } else if (items[item].equals("Choose from Library")) {
                    userChoosenTask ="Choose from Library";
                    if(result)
                        galleryIntent();

                }
            }
        });
        builder.show();
    }


    private void galleryIntent()
    {
        Intent intent = new Intent();
        intent.setType("image/*");
        intent.setAction(Intent.ACTION_GET_CONTENT);//
        startActivityForResult(Intent.createChooser(intent, "Select File"),SELECT_FILE);
    }

    private void cameraIntent()
    {
        Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        startActivityForResult(intent, REQUEST_CAMERA);
    }

    protected void onActivityResult(int requestcode, int resultcode,
                                    Intent imagereturnintent) {
        super.onActivityResult(requestcode, resultcode, imagereturnintent);


        if (resultcode == RESULT_OK) {

            if (requestcode == SELECT_FILE) {
                String imagesArray = imagereturnintent.getStringExtra(CustomGalleryIntentKey);//get Intent data
                //Convert string array into List by splitting by ',' and substring after '[' and before ']'
                List<String> selectedImages = Arrays.asList(imagesArray.substring(1, imagesArray.length() - 1).split(", "));
                loadGridView(new ArrayList<String>(selectedImages), imagereturnintent);//call load gridview method by passing converted list into arrayList
            } else if (requestcode == REQUEST_CAMERA)
                onCaptureImageResult(imagereturnintent);


        }



    }

    private void loadGridView(ArrayList<String> imagesArray,Intent imagereturnintent) {
        GridView_Adapter adapter = new GridView_Adapter(Create_GateIn.this, imagesArray, false);
        for (int i=0;i<imagesArray.size();i++){
//            imagesPathList.add(imagesArray(i));
            try {
            selectedImageBitmap = BitmapFactory.decodeFile(imagesArray.get(i));

            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            selectedImageBitmap.compress(Bitmap.CompressFormat.JPEG, 100, byteArrayOutputStream);
            byte[] byteArrayImage = byteArrayOutputStream.toByteArray();


                encodedImage = Base64.encodeToString(byteArrayImage, Base64.DEFAULT);

            }catch (Exception e)
            {
                shortToast(getApplicationContext(),"Image Size Should be lessthan 4MB ");
            }
            Log.i("encodedImage ", encodedImage);

            photo_attach_bean=new Multi_Photo_Bean();

            Filename="imageName";
            String path = imagesArray.get(i);
            String filename = path.substring(path.lastIndexOf("/") + 1);
            final String fileName_from = path.substring(path.lastIndexOf('/') + 1);

            Log.d("Last ", filename);
            Log.d("Last fileName_from ", fileName_from);
            photo_attach_bean.setName(fileName_from);
            photo_attach_bean.setBase64(encodedImage);
            photo_attach_bean.setLength(String.valueOf(encodedImage.length()));
            photo_attach_bean.setAttachment_Id("");
            encodeArray.add(photo_attach_bean);
            IfAttchment = "True";

            verticalAdapter=new VerticalAdapter(encodeArray);
            LinearLayoutManager verticalLayoutmanager
                    = new LinearLayoutManager(Create_GateIn.this, LinearLayoutManager.VERTICAL, false);
            list_item.setLayoutManager(verticalLayoutmanager);
            list_item.setAdapter(verticalAdapter);


        }
        String Str = "";
        for (int ii = 0; ii < encodeArray.size(); ii++) {
            Str += " " + encodeArray.get(ii).getName() + ",";
        }
        if (Str.endsWith(","))
        {
            Str=Str.substring(0,Str.length()-1);
            ed_attach.setText(Str);
        }
//        ed_attach.setText(photo_attach_bean.getName());
     //   ed_attach.setText("imageName");
        Filename=ed_attach.getText().toString();

    }




    private void onCaptureImageResult(Intent data) {
        Bitmap thumbnail = (Bitmap) data.getExtras().get("data");
        ByteArrayOutputStream bytes = new ByteArrayOutputStream();
        thumbnail.compress(Bitmap.CompressFormat.JPEG, 90, bytes);

        File destination = new File(Environment.getExternalStorageDirectory(),
                System.currentTimeMillis() + ".jpg");
        filename= destination.getAbsolutePath();
        FileOutputStream fo;
        try {
            destination.createNewFile();
            fo = new FileOutputStream(destination);
            fo.write(bytes.toByteArray());
            fo.close();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }

        //  profile.setImageBitmap(thumbnail);
        byte[] byteArrayImage = bytes.toByteArray();
        encodedImage = Base64.encodeToString(byteArrayImage, Base64.DEFAULT);
        filename=filename.substring(filename.lastIndexOf("/")+1);
        photo_attach_bean=new Multi_Photo_Bean();
//            Filename="imageName";
        photo_attach_bean.setName(filename);
        photo_attach_bean.setBase64(encodedImage);
        photo_attach_bean.setLength(String.valueOf(encodedImage.length()));
        photo_attach_bean.setAttachment_Id("");
        encodeArray.add(photo_attach_bean);
        IfAttchment = "True";

        ed_attach.setText(filename);
        list.add(filename);
        for (int ii = 0; ii < list.size(); ii++) {
            Str += " " + list.get(ii) + ",";
        }
        if (Str.endsWith(",")) {
            Str = Str.substring(0, Str.length() - 1);
            ed_attach.setText(Str);
        }
        verticalAdapter=new VerticalAdapter(encodeArray);
        LinearLayoutManager verticalLayoutmanager
                = new LinearLayoutManager(Create_GateIn.this, LinearLayoutManager.VERTICAL, false);
        list_item.setLayoutManager(verticalLayoutmanager);
        list_item.setAdapter(verticalAdapter);

    }
    public String getPath(Context context, Uri uri) {
        String[] projection = { MediaStore.Images.Media.DATA };
        Cursor cursor = managedQuery(uri, projection, null, null, null);
        if (cursor != null) {
            // HERE YOU WILL GET A NULLPOINTER IF CURSOR IS NULL
            // THIS CAN BE, IF YOU USED OI FILE MANAGER FOR PICKING THE MEDIA
            int column_index = cursor
                    .getColumnIndexOrThrow(MediaStore.Images.Media.DATA);
            cursor.moveToFirst();
            return cursor.getString(column_index);
        } else
            return null;
    }

    @SuppressWarnings("deprecation")
    private void onSelectFromGalleryResult(Intent data) {


        Log.i("intent ", String.valueOf(data));
        ByteArrayOutputStream bytes = new ByteArrayOutputStream();
        if (data != null) {

            Uri selectedImageUri = data.getData();
            String filePath = null;
            // OI FILE Manager
            String filemanagerstring = selectedImageUri.getPath();
            // MEDIA GALLERY
            selectedImagePath = getPath(this,selectedImageUri);
            filePath = data.getData().getPath();
//            filePath = getPath(this, selectedImageUri);
//            filename=selectedImagePath.substring(selectedImagePath.lastIndexOf("/")+1);


            file = new File(filePath);
            Filename= file.getAbsolutePath();
            imageName=filePath.substring(Filename.lastIndexOf("/")+1);
            String basename = FilenameUtils.getBaseName(Filename);
            String fileType = FilenameUtils.getExtension(Filename);
            ed_attach.setText(basename+fileType);
            // typedFile = new TypedFile("image*//*", file);
            Bitmap selectedImageBitmap = null;
            try {
                selectedImageBitmap = MediaStore.Images.Media.getBitmap(getContentResolver(), selectedImageUri);
            } catch (IOException e) {
                e.printStackTrace();
            }
            //  profile.setImageBitmap(selectedImageBitmap);
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            selectedImageBitmap.compress(Bitmap.CompressFormat.JPEG, 100, byteArrayOutputStream);
            byte[] byteArrayImage = byteArrayOutputStream.toByteArray();
            encodedImage = Base64.encodeToString(byteArrayImage, Base64.DEFAULT);
            Log.d(TAG, "File Path: " + imageName);
            Log.d(TAG, "File imageName: " + imageName);

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

            year  = selectedYear;
            month = selectedMonth;
            day   = selectedDay;


            if(manuf_date || imV_manuf_date)
            {
                ed_manuf_date.setText(formatDate(year, month, day));


            }else if(last_test_date|| last_test_date_im) {

                ed_last_test_date.setText(formatDate(year, month, day));
                ed_next_date.setText(formatDate(year+2, month+6, day));


            }else if(im_in_date || in_date)
            {
                ed_date.setText(formatDate(year, month, day));
            }

            //    System.out.println("am a new from date====>>" + str_From);

            //  date.setText(formatDate(year, month, day));

        }
    };


    @Override
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

    }


    public class Create_GateIn_Customer_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
                        CustomerDropdownArrayList=new ArrayList<CustomerDropdownBean>();
                        for (int i = 0; i < jsonarray.length(); i++) {

                            customer_DropdownBean = new CustomerDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);


                            customer_DropdownBean.setName(jsonObject.getString("Name"));
                            customer_DropdownBean.setCode(jsonObject.getString("Code"));
                            customer_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("Name"));
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



            if(dropdown_customer_list!=null)
            {

                customAdapter = new SpinnerCustomAdapter(Create_GateIn.this, R.layout.spinner_text, CustomerDropdownArrayList);
                customAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                // Set adapter to spinner
                sp_customer.setAdapter(customAdapter);
              /*  ArrayAdapter<String> CustomerAdapter = new ArrayAdapter<>(getApplicationContext(),R.layout.spinner_text,worldlist);
                CustomerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_customer.setAdapter(CustomerAdapter);
*/
            }
            else if(dropdown_customer_list.size()<1)
            {
                shortToast(getApplicationContext(),"Data Not Found");

            }

            progressDialog.dismiss();

        }

    }

    public class Create_GateIn_EquipmentType_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
            //  progressDialog.show();
        }

        @Override
        protected Void doInBackground(Void... params) {

            ServiceHandler sh = new ServiceHandler();
            HttpParams httpParameters = new BasicHttpParams();
            DefaultHttpClient httpClient = new DefaultHttpClient(httpParameters);
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCreateGateInEquipMentType);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
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

                        dropdown_equipment_list = new ArrayList<>();
                        dropdown_equipment_arraylist = new ArrayList<EquipmentDropdownBean>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            equipment_DropdownBean = new EquipmentDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            equipment_DropdownBean.setName(jsonObject.getString("Type"));
                            equipment_DropdownBean.setCode(jsonObject.getString("Code"));
                            EquipmentType = jsonObject.getString("Type");
                            EquipmentCode = jsonObject.getString("Code");
                            String[] set1 = new String[2];
                            set1[0] = EquipmentType;
                            set1[1] = EquipmentCode;
                            dropdown_equipment_list.add(EquipmentType);
                            Equip_Type.add(set1[0]);
                            Equip_Code.add(set1[1]);
                            dropdown_equipment_arraylist.add(equipment_DropdownBean);
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
            if (dropdown_equipment_list != null) {
//                UserListAdapter adapter = new UserListAdapter(Create_GateIn.this, R.layout.list_item_row, pending_arraylist);
//                listview.setAdapter(adapter);
                ArrayAdapter<String> EquipmentAdapter = new ArrayAdapter<>(getApplicationContext(), R.layout.spinner_text, dropdown_equipment_list);
                EquipmentAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_equip_type.setAdapter(EquipmentAdapter);

                try {
                    if (!equipment_Type_cd.equals("")) {
                        for (int i = 0; i < dropdown_equipment_arraylist.size(); i++) {
                            if (dropdown_equipment_arraylist.get(i).getName().equalsIgnoreCase(equipment_Type_cd)) {
                                sp_equip_type.setSelection(i);
                            }

                        }
                    }

                }catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
        }
            else
            {
//                shortToast(getApplicationContext(),"Data Not Found");


            }

            progressDialog.dismiss();

        }

    }



    public class PostInfo extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        private JSONArray invite_jsonlist;
        private JSONObject invitejsonObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCreate_GateIn);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();

                invite_jsonlist = new JSONArray();
                reqObj = new JSONObject();
                try {
                /*
                    invitejsonObject = new JSONObject();
                    invitejsonObject.put("FileName", filename);
                    invitejsonObject.put("ContentLength",encodedImage.length());
                    invitejsonObject.put("base64imageString",encodedImage);
                    invite_jsonlist.put(invitejsonObject);
                    reqObj.put("ArrayOfFileParams",invite_jsonlist);*/
                    for(int i=0;i<encodeArray.size();i++) {

                        invitejsonObject = new JSONObject();
                        invitejsonObject.put("FileName", encodeArray.get(i).getName());
                        invitejsonObject.put("ContentLength", encodeArray.get(i).getLength());
                        invitejsonObject.put("base64imageString", encodeArray.get(i).getBase64());
                        invite_jsonlist.put(invitejsonObject);

                       Log.d("base64imageString",encodeArray.get(i).getBase64());

                    }
                    reqObj.put("ArrayOfFileParams",invite_jsonlist);
//                    Log.d("ArrayOfFileParams", String.valueOf(invite_jsonlist));



                }catch (Exception e)
                {

                }
                SimpleDateFormat fromUser = new SimpleDateFormat("dd-MM-yyyy", Locale.ENGLISH);
                SimpleDateFormat myFormat = new SimpleDateFormat("dd-MMM-yyyy",Locale.ENGLISH);
                String datePattern = "\\d{2}-\\d{2}-\\d{4}";


                try {
                    if (get_date.equals(null) || get_date.length() < 0) {

                        get_date = "";
                    } else {
                        Boolean is_edt_Date1 = get_date.matches(datePattern);
                        if (is_edt_Date1 == true) {
                            get_date = myFormat.format(fromUser.parse(get_date));


                        } else {

                        }


                    }

                }catch (Exception e)
                {
                    get_date = "";
                }
                String numberAsString = Integer.toString(pendingsize+1);
                jsonObject.put("GTN_ID", numberAsString);
                jsonObject.put("CSTMR_ID", get_sp_customer_code);
                jsonObject.put("CSTMR_CD", get_sp_customer);
                jsonObject.put("EQPMNT_NO", get_equipment);
                jsonObject.put("EQPMNT_TYP_ID", get_sp_equipe_code);
                jsonObject.put("EQPMNT_TYP_CD", get_sp_equipe);
                jsonObject.put("EQPMNT_CD_ID",equipement_id );
                jsonObject.put("EQPMNT_CD_CD", get_code);
                jsonObject.put("YRD_LCTN", get_location);
                jsonObject.put("GTN_DT", get_date );
                jsonObject.put("GTN_TM", get_time);
                jsonObject.put("PRDCT_ID", get_sp_previous_id);
                jsonObject.put("PRDCT_CD", get_sp_previous_code);
                jsonObject.put("EIR_NO", get_eir_no);
                jsonObject.put("VHCL_NO", get_vechicle);
                jsonObject.put("TRNSPRTR_CD",get_transport);
                jsonObject.put("HTNG_BT", get_swt_heating);
                jsonObject.put("RMRKS_VC", get_remark);
                jsonObject.put("CHECKED", "True");
                jsonObject.put("PRDCT_DSCRPTN_VC", get_sp_previous_dec);
                jsonObject.put("RNTL_BT", get_swt_rental);
                jsonObject.put("RepairEstimateId", numberAsString);
                jsonObject.put("UserName",sp.getString(SP_USER_ID,"user_Id"));

                //New fields - Added by Anantha krishnan
                jsonObject.put("CNSGN_NAM", get_consignee_name);
                jsonObject.put("CNSGN_LCTN", get_consignee_location);

                jsonObject.put("Mode", "new");





                try {

                    if(get_manu_date.equals(null)||get_manu_date.length()<0)
                    {

                        get_manu_date="";
                    }else
                    {
                        Boolean is_edt_Date1 = get_manu_date.matches(datePattern);
                        if(is_edt_Date1==true)
                        {
                            get_manu_date = myFormat.format(fromUser.parse(get_manu_date));


                        }else
                        {

                        }
                    }}catch (Exception e)
                {
                    get_manu_date="";
                }
                try {

                    if(get_last_test_date.equals(null)||get_last_test_date.length()<0)
                    {

                        get_last_test_date="";
                    }else
                    {
                        Boolean is_edt_Date1 = get_last_test_date.matches(datePattern);
                        if(is_edt_Date1==true)
                        {
                            get_last_test_date = myFormat.format(fromUser.parse(get_last_test_date));


                        }else
                        {

                        }
                    }
                }catch (Exception e)
                {
                    get_manu_date="";
                }  try {

                    if(get_next_date.equals(null)||get_next_date.length()<0)
                    {

                        get_next_date="";
                    }else
                    {
                        Boolean is_edt_Date1 = get_next_date.matches(datePattern);
                        if(is_edt_Date1==true)
                        {
                            get_next_date = myFormat.format(fromUser.parse(get_next_date));


                        }else
                        {

                        }
                    }}catch (Exception e)
                {
                    get_next_date="";
                }


                jsonObject.put("EIMNFCTR_DT", get_manu_date);
                jsonObject.put("EITR_WGHT_NC", get_tare_weight);
                jsonObject.put("EIGRSS_WGHT_NC", get_gross);
                jsonObject.put("EICPCTY_NC",get_capacity);
                jsonObject.put("EILST_SRVYR_NM", get_last_survy);
                jsonObject.put("EILST_TST_DT", get_last_test_date);
                jsonObject.put("EILST_TST_TYP_ID",ed_last_type_id);
                jsonObject.put("EINXT_TST_DT", get_next_date);
                jsonObject.put("EINXT_TST_TYP_ID",ed_next_type_id);
                jsonObject.put("EIRMRKS_VC", get_info_remark);
                jsonObject.put("EIACTV_BT", get_swt_info_active);
                jsonObject.put("EIRNTL_BT", get_swt_info_rental);
                jsonObject.put("EIAttachment", "False");
                jsonObject.put("EIHasChanges", changes);
                jsonObject.put("PageName", "GateIn");
                jsonObject.put("GateinTransactionNo", "");
                jsonObject.put("IfAttchment",IfAttchment );
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

                responseString = returnMessage.getString("equipmentUpdate");
//                responseString=message;
                Log.d("responseString", returnMessage.toString());


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
                    Toast.makeText(getApplicationContext(), "GateIn Created Successfully.", Toast.LENGTH_SHORT).show();

                    ed_equipement.setText("");
                    GlobalConstants.get_equipment_sevice="";
                    finish();
                    Intent i = new Intent(getApplication(), GateIn.class);
                    startActivity(i);

                } else {
                    Toast.makeText(getApplicationContext(), "GateIn Creation Failed", Toast.LENGTH_SHORT).show();

                }
            }else
            {
                Toast.makeText(getApplicationContext(), "Unable to connect the Server..!", Toast.LENGTH_SHORT).show();

            }
            progressDialog.dismiss();

        }
    }

    public class Create_GateIn_moreInfo_list_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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

                        dropdown_MoreInfo_arraylist = new ArrayList<Equipment_Info_TypeDropdownBean>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            moreInfo_DropdownBean = new Equipment_Info_TypeDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            moreInfo_DropdownBean.setId(jsonObject.getString("ENM_ID"));
                            moreInfo_DropdownBean.setCode(jsonObject.getString("ENM_CD"));
                            moreInfo_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("ENM_CD"));
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



            if(jsonarray!=null)
            {
//                UserListAdapter adapter = new UserListAdapter(Create_GateIn.this, R.layout.list_item_row, pending_arraylist);
//                listview.setAdapter(adapter);
             /*   ArrayAdapter<String> CargoAdapter = new ArrayAdapter<>(getApplicationContext(),R.layout.spinner_text,dropdown_MoreInfo_list);
                CargoAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sp_last_test_type.setAdapter(CargoAdapter);
*/
                customAdapter_last_test_type = new SpinnerCustomAdapter_last_type(Create_GateIn.this, R.layout.spinner_text, dropdown_MoreInfo_arraylist);
                customAdapter_last_test_type.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                // Set adapter to spinner
                sp_last_test_type.setAdapter(customAdapter_last_test_type);

            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }

            progressDialog.dismiss();

        }

    }


    public class Create_GateIn_PreviousCargo_details extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCreateGateInPreviousCargo);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
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

                        dropdown_PreviousCargo_arraylist = new ArrayList<Previous_CargoDropdownBean>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            cargo_DropdownBean = new Previous_CargoDropdownBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            cargo_DropdownBean.setId(jsonObject.getString("ID"));
                            cargo_DropdownBean.setCode(jsonObject.getString("Code"));
                            cargo_DropdownBean.setDesc(jsonObject.getString("Description"));
                            cargo_DropdownBean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("Code_Description"));
                            PreviousCargoID = jsonObject.getString("ID");
                            PreviousCargoCode = jsonObject.getString("Code");
                            PreviousCargoDescription = jsonObject.getString("Description");
                            String[] set1 = new String[3];
                            set1[0] = PreviousCargoID;
                            set1[1] = PreviousCargoCode;
                            set1[2] = PreviousCargoDescription;
                            dropdown_PreviousCargo_list.add(PreviousCargoDescription);
                            Cargo_ID.add(set1[0]);
                            Cargo_Code.add(set1[1]);
                            Cargo_Description.add(set1[2]);
                            dropdown_PreviousCargo_arraylist.add(cargo_DropdownBean);
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



            if(dropdown_PreviousCargo_list!=null)
            {
//                UserListAdapter adapter = new UserListAdapter(Create_GateIn.this, R.layout.list_item_row, pending_arraylist);
//                listview.setAdapter(adapter);


                customAdapter_cargo = new SpinnerCustomAdapter_Cargo(Create_GateIn.this, R.layout.spinner_text, dropdown_PreviousCargo_arraylist);
                customAdapter_cargo.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
              /*  ArrayAdapter<String> CargoAdapter = new ArrayAdapter<>(getApplicationContext(),R.layout.spinner_text,dropdown_PreviousCargo_list);
                CargoAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);*/
                sp_previous_cargo.setAdapter(customAdapter_cargo);

            }
            else if(dropdown_PreviousCargo_list.size()<1)
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }

            progressDialog.dismiss();

        }

    }
    /*
    * Post_Verify_Equipment_No for Equipment validation for already exit or not
    * */
    public class Post_Verify_Equipment_No extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        JSONObject jsonobject;
        JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLVerify_Equipment_No);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("CustomerName", get_sp_customer);
                jsonObject.put("bv_userName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("bv_strEquipmentNo", get_equipment);
                jsonObject.put("bv_intGridIndex", pendingsize);

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("responce", resp);
                Log.d("req", jsonObject.toString());
                JSONObject jsonResp = new JSONObject(resp);

                jsonobject = jsonResp.getJSONObject("d");
                // jsonarray = jsonobject.getJSONArray("subcategorylist");
                if (jsonobject != null) {

                    validationStatus=jsonobject.getString("validationStatus");

                    GlobalConstants.validationStatus = validationStatus;
/*
                    System.out.println("Am HashMap list"+jsonarray);


                    for (int i = 0; i < jsonarray.length(); i++) {

                        jsonobject = jsonarray.getJSONObject(i);

                        String Subcat_id = jsonobject.getString("SubCategoryId");
                        String Subcat_name = jsonobject.getString("SubCategoryName");


                    }
*/
                }
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


            if(jsonobject!=null)
            {
                if(validationStatus.equalsIgnoreCase("Success"))
                {
                    new Equipment_More_Info().execute();
                    new Create_GateIn_EquipmentType_details().execute();
                    if(clickMessage.equalsIgnoreCase("Submit"))
                    {
                        if(systemDate.compareTo(get_date)<0 )
                        {
                            shortToast(getApplicationContext(),"In Date cannot be greater than Current Date..!");
                        }else {
                            new PostInfo().execute();
                        }

                    }else if(clickMessage.equals("Accordion"))
                    {
                        /*if(systemDate.compareTo(get_date)<0 )
                        {
                            shortToast(getApplicationContext(),"In Date cannot be greater than Current Date..!");
                        }else if( systemDate.compareTo(get_manu_date)<0)
                        {
                            shortToast(getApplicationContext(),"Maunf. Date cannot be greater than Current Date..!");

                        }*/
                    }
                }else
                {
                    shortToast(getApplicationContext(),validationStatus);
                }


            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");
            }

            progressDialog.dismiss();

        }
    }

    public class PostCustomer_Code extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        JSONObject jsonobject;
        JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLGet_Type_Code);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("bv_strType", get_sp_equipe);
                jsonObject.put("bv_userName", sp.getString(SP_USER_ID,"user_Id"));

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("responce", resp);
                Log.d("req", jsonObject.toString());
                JSONObject jsonResp = new JSONObject(resp);

                jsonobject = jsonResp.getJSONObject("d");
                // jsonarray = jsonobject.getJSONArray("subcategorylist");
                if (jsonobject != null) {

                    equipement_code=jsonobject.getString("code");
                    equipement_id=jsonobject.getString("id");

/*
                    System.out.println("Am HashMap list"+jsonarray);


                    for (int i = 0; i < jsonarray.length(); i++) {

                        jsonobject = jsonarray.getJSONObject(i);

                        String Subcat_id = jsonobject.getString("SubCategoryId");
                        String Subcat_name = jsonobject.getString("SubCategoryName");


                    }
*/
                }
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


            if(jsonobject!=null)
            {
                ed_code.setText(equipement_code);


            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");
            }

            progressDialog.dismiss();

        }
    }

    public class Equipment_More_Info extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLVerify_Equipment_InFo);
            httpPost.setHeader("Content-Type", "application/json");

            try{
                JSONObject jsonObject = new JSONObject();

                            /*"EquipmentNo":"TEST3333333",
             "PageName":"GateIn",
             "GateinTransactionNo":"",
             "Attachment":"False",*/

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("EquipmentNo", get_equipment);
                jsonObject.put("PageName","GateIn");
                jsonObject.put("Attachment","False");
                jsonObject.put("GateinTransactionNo","");

               /* JSONObject jsonObject1 = new JSONObject();
                jsonObject1.put("Credentials",jsonObject);*/

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                Log.d("request", jsonObject.toString());
                JSONObject jsonrootObject = new JSONObject(resp);
                getJsonObject = jsonrootObject.getJSONObject("d");


                if (getJsonObject != null) {


                    if (getJsonObject.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found.");
                            }
                        });
                    }else {
                        equipmentBeanArrayList = new ArrayList<>();

                        EIEQPMNT_TYP_CD=(getJsonObject.getString("EIEQPMNT_TYP_CD"));
                        EICPCTY_NC=(getJsonObject.getString("EICPCTY_NC"));
                        EIGRSS_WGHT_NC=(getJsonObject.getString("EIGRSS_WGHT_NC"));
                        EILST_SRVYR_NM=(getJsonObject.getString("EILST_SRVYR_NM"));
                        EILST_TST_DT=(getJsonObject.getString("EILST_TST_DT"));
                        EILST_TST_TYP_ID=(getJsonObject.getString("EILST_TST_TYP_ID"));
                        EITR_WGHT_NC=(getJsonObject.getString("EITR_WGHT_NC"));
                        EIRMRKS_VC=(getJsonObject.getString("EIRMRKS_VC"));
                        EIACTV_BT=(getJsonObject.getString("EIACTV_BT"));
                        EIMNFCTR_DT=(getJsonObject.getString("EIMNFCTR_DT"));
                        EINXT_TST_TYP_ID=(getJsonObject.getString("EINXT_TST_TYP_ID"));
                        EIRNTL_BT=(getJsonObject.getString("EIRNTL_BT"));


                    }
                }else if(getJsonObject.length()<1){
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



            if(getJsonObject!=null)
            {

                LL_Equipment_Info.setVisibility(View.VISIBLE);
                equip_up.setVisibility(View.VISIBLE);
                equip_down.setVisibility(View.GONE);
                ed_manuf_date.setText(EIMNFCTR_DT);
                ed_tare_weight.setText(EITR_WGHT_NC);
                ed_Gross_weight.setText(EIGRSS_WGHT_NC);
                ed_capacity.setText(EICPCTY_NC);
                ed_last_survey.setText(EILST_SRVYR_NM);
//                ed_last_test_date.setText(EILST_TST_DT);
             //   ed_last_test_type.setText(EILST_TST_TYP_ID);
                ed_next_type.setText(EINXT_TST_DT);
                ed_next_date.setText(EINXT_TST_TYP_ID);

                if(EIACTV_BT.equalsIgnoreCase("true"))
                {
                    info_active.setChecked(true);
                }else
                {
                    info_active.setChecked(false);
                }

                if(EIRNTL_BT.equalsIgnoreCase("true"))
                {
                    info_rental.setChecked(true);
                }else{
                    info_rental.setChecked(false);
                }


            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }

            progressDialog.dismiss();

        }

    }
        /*
        * Post_Verify_Rental_Entry for check rental already there
        * */
    public class Post_Verify_Rental_Entry extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        JSONObject jsonobject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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

            HttpPost httpPost = new HttpPost(ConstantValues.baseURLVerify_Rental_Entry);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("EquipmentNo", get_equipment);

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("responce", resp);
                Log.d("req", jsonObject.toString());
                JSONObject jsonResp = new JSONObject(resp);

                jsonobject = jsonResp.getJSONObject("d");

                if (jsonobject != null) {

                    validationStatus=jsonobject.getString("statusText");

                }
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


            if(jsonobject!=null)
            {
                if(validationStatus.equalsIgnoreCase("Success"))
                {
                    get_swt_rental="True";

                }else if(validationStatus.contains("cannot be marked for Rental Gate In"))
                {
                    longToast(getApplicationContext(),"This Equipment Number cannot be marked for Rental Gate In, as there is no Rental Entry / Rental Gate Out created");
                    get_swt_rental="False";
                }
            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");
            }

            progressDialog.dismiss();

        }
    }

    public class GateIn_Default_Values extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONObject jsongetObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLCreate_GateIn_DefaultValues);
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
                Log.d("request", jsonObject.toString());
                JSONObject jsonrootObject = new JSONObject(resp);
                getJsonObject = jsonrootObject.getJSONObject("d");





                System.out.println("Am HashMap list"+getJsonObject);
                if (getJsonObject.length() < 1) {
                    runOnUiThread(new Runnable() {
                        public void run() {
                            shortToast(getApplicationContext(), "No Records Found.");
                        }
                    });
                }else {
                    getStatus=getJsonObject.getString("EquipmentStatus");
                    getYardLocation=getJsonObject.getString("YardLocation");
                    getEquipment_Type=getJsonObject.getString("EquipmentType");
                    equipement_code=getJsonObject.getString("EquipmentCode");

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



            if(getJsonObject!=null)
            {
                for(int i=0;i<dropdown_equipment_list.size();i++)
                {
                    if(getEquipment_Type.equals(dropdown_equipment_arraylist.get(i).getName()))
                    {

                        sp_equip_type.setSelection(i);
                    }
                }

                ed_location.setText(getYardLocation);
                ed_status.setText(getStatus);
                ed_code.setText(equipement_code);



            }
            else

            {
                shortToast(getApplicationContext(),"Data Not Found");


            }

            progressDialog.dismiss();

        }

    }


    public class VerticalAdapter extends RecyclerView.Adapter<VerticalAdapter.MyViewHolder> {

        private ArrayList<Multi_Photo_Bean> verticalList;
        private Multi_Photo_Bean userListBean;

        public class MyViewHolder extends RecyclerView.ViewHolder {
            public TextView Cust_Name,time,attachment_id,image_position;
            ImageView im_delete;
            public MyViewHolder(View view) {
                super(view);
                LinearLayout whole = (LinearLayout) view.findViewById(R.id.LL_whole);
                Cust_Name = (TextView) view.findViewById(R.id.image_name);
                time = (TextView) view.findViewById(R.id.text3);
                attachment_id = (TextView) view.findViewById(R.id.attachment_id);
                image_position = (TextView) view.findViewById(R.id.image_position);
                im_delete = (ImageView) view.findViewById(R.id.im_delete);
            }
        }


        public VerticalAdapter(ArrayList<Multi_Photo_Bean> verticalList) {
            this.verticalList = verticalList;
        }

        @Override
        public MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
            View itemView = LayoutInflater.from(parent.getContext())
                    .inflate(R.layout.image_list_row, parent, false);

            return new MyViewHolder(itemView);
        }

        @Override
        public void onBindViewHolder(final MyViewHolder holder, final int position) {

       /* holder.txtView.setText(verticalList.get(position));
        holder.txtView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

            }
        });*/
            userListBean = verticalList.get(position);
            final String fileName_from = userListBean.getName().substring(userListBean.getName().lastIndexOf('/') + 1);
            holder.Cust_Name.setText(fileName_from);
            holder.attachment_id.setText(userListBean.getAttachment_Id());
            holder.image_position.setText(String.valueOf(position));

            holder.Cust_Name.setOnClickListener(new View.OnClickListener() {
                public Bitmap bitmap;

                @Override
                public void onClick(View view) {

                    Log.i("getAttachment_Id()", verticalList.get(position).getAttachment_Id());

                    String base64Content = verticalList.get(position).getBase64();
                    byte[] bytes = Base64.decode(base64Content, Base64.DEFAULT);
                    try {
                        bitmap = BitmapFactory.decodeByteArray(bytes, 0, bytes.length);
                    }catch (Exception e)
                    {
                        shortToast(getApplicationContext(),"Image Size Should be lessthan 4MB ");
                    }
                    popImageUp(bitmap, "");
//                            Log.i("image_url", list.get(position).getPathUrl());


                }
            });

            holder.im_delete.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    GlobalConstants.attach_ID = verticalList.get(position).getAttachment_Id();
                    GlobalConstants.position = String.valueOf(verticalList.get(position));
                    Log.i("getAttachment_Id()", verticalList.get(position).getAttachment_Id());
                    if (verticalList.get(position).getAttachment_Id().equals("")) {
                        GlobalConstants.from = "Repairpending_delete";
                        GlobalConstants.image_name = fileName_from;

//                                image_arraylist=new ArrayList<Image_Bean>();


                        verticalList.remove(verticalList.get(position));

                        verticalAdapter=new VerticalAdapter(encodeArray);
                        LinearLayoutManager verticalLayoutmanager
                                = new LinearLayoutManager(Create_GateIn.this, LinearLayoutManager.VERTICAL, false);
                        list_item.setLayoutManager(verticalLayoutmanager);
                        list_item.setAdapter(verticalAdapter);

                    } else {
                        new Delete_Attachment().execute();

                    }

                }
            });

        }

        @Override
        public int getItemCount() {
            return verticalList.size();
        }
    }

    public class UserListAdapter extends BaseAdapter {

        private final ArrayList<Multi_Photo_Bean> arraylist;
        private ArrayList<Multi_Photo_Bean> list;
        Context context;

        int resource;
        private Multi_Photo_Bean userListBean;
        int lastPosition = -1;

        public UserListAdapter(Context context, int resource, ArrayList<Multi_Photo_Bean> list) {
            this.context = context;
            this.list = list;
            this.resource = resource;
            this.arraylist = new ArrayList<Multi_Photo_Bean>();
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
                holder.Cust_Name = (TextView) convertView.findViewById(R.id.image_name);
                holder.time = (TextView) convertView.findViewById(R.id.text3);
                holder.attachment_id = (TextView) convertView.findViewById(R.id.attachment_id);
                holder.image_position = (TextView) convertView.findViewById(R.id.image_position);
                holder.im_delete = (ImageView) convertView.findViewById(R.id.im_delete);


                // R.id.tv_customerName,R.id.tv_Inv_no,R.id.tv_date,R.id.tv_val,R.id.tv_due
                convertView.setTag(holder);
            } else {
                holder = (ViewHolder) convertView.getTag();
            }
            if (list.size() < 1) {
                Toast.makeText(getApplicationContext(), "NO DATA FOUND", Toast.LENGTH_LONG).show();
            } else {

                userListBean = list.get(position);


                final String fileName_from = userListBean.getName().substring(userListBean.getName().lastIndexOf('/') + 1);
                holder.Cust_Name.setText(fileName_from);

                holder.attachment_id.setText("");

                holder.image_position.setText(String.valueOf(position));

                holder.Cust_Name.setOnClickListener(new View.OnClickListener() {
                    public Bitmap bitmap;

                    @Override
                    public void onClick(View view) {

                        Log.i("getAttachment_Id()", list.get(position).getAttachment_Id());

                            String base64Content = list.get(position).getBase64();
                            byte[] bytes = Base64.decode(base64Content, Base64.DEFAULT);
                        try {
                            bitmap = BitmapFactory.decodeByteArray(bytes, 0, bytes.length);
                        }catch (Exception e)
                        {
                            shortToast(getApplicationContext(),"Image Size Should be lessthan 4MB ");
                        }
                            popImageUp(bitmap, "");
//                            Log.i("image_url", list.get(position).getPathUrl());


                    }
                });

                holder.im_delete.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        GlobalConstants.attach_ID = list.get(position).getAttachment_Id();
                        GlobalConstants.position = String.valueOf(list.get(position));
                        Log.i("getAttachment_Id()", list.get(position).getAttachment_Id());
                        if (list.get(position).getAttachment_Id().equals("")) {
                            GlobalConstants.from = "Repairpending_delete";
                            GlobalConstants.image_name = fileName_from;

//                                image_arraylist=new ArrayList<Image_Bean>();


                            list.remove(list.get(position));

                            verticalAdapter=new VerticalAdapter(encodeArray);
                            LinearLayoutManager verticalLayoutmanager
                                    = new LinearLayoutManager(Create_GateIn.this, LinearLayoutManager.VERTICAL, false);
                            list_item.setLayoutManager(verticalLayoutmanager);
                            list_item.setAdapter(verticalAdapter);

                        } else {
                            new Delete_Attachment().execute();

                        }

                    }
                });


            }
            return convertView;
        }


    }

    public class Delete_Attachment extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray LineItems_Json;
        private ProgressDialog progressDialog;
        private String getJsonObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLRepairEstimate_Attachment_Delete);
            httpPost.setHeader("Content-Type", "application/json");

            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("bv_strAttachmentId", GlobalConstants.attach_ID);


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", String.valueOf(jsonObject));
                Log.d("req", resp);
                JSONObject jsonrootObject = new JSONObject(resp);

                JSONObject d_JsonObject = jsonrootObject.getJSONObject("d");
                getJsonObject = d_JsonObject.getString("Status");


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
            try {
                if (getJsonObject.equalsIgnoreCase("Success")) {
                    shortToast(getApplicationContext(), "Attachment Deleted Succesfully");

                    for (int i = 0; i < encodeArray.size(); i++) {
                        if (encodeArray.get(i).getAttachment_Id().equalsIgnoreCase(GlobalConstants.attach_ID)) {
                            encodeArray.remove(i);
                        }

                    }
                    verticalAdapter=new VerticalAdapter(encodeArray);
                    LinearLayoutManager verticalLayoutmanager
                            = new LinearLayoutManager(Create_GateIn.this, LinearLayoutManager.VERTICAL, false);
                    list_item.setLayoutManager(verticalLayoutmanager);
                    list_item.setAdapter(verticalAdapter);


                } else {
                    shortToast(getApplicationContext(), "Attachment Deleted Failed");

                }
            } catch (Exception e) {

            }
        }

    }

    static class ViewHolder {
        TextView equip_no, time, image_position, attachment_id, Cust_Name, previous_crg, attachmentstatus, gateIn_Id, code, location, pre_id, pre_code, cust_code, type_id, code_id,
                vechicle, transport, attachID, filename, Eir_no, heating_bt, rental_bt, remark, status, pre_adv_id, type;
        CheckBox checkBox;
        ImageView im_delete;

        LinearLayout whole, LL_username;
    }

    public class Equipment_Type_Settings extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLEquip_Type_Settings);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("KY_NAM", "009");


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("res_equ_settings", resp);
                Log.d("req_equ_settings", String.valueOf(jsonObject));
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("SettingsList");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list" + jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "Invalid Equipment Type. Click on the list for valid values");
                                ed_code.setText("");
                            }
                        });
                    } else {


                        for (int i = 0; i < jsonarray.length(); i++) {

                            jsonObject = jsonarray.getJSONObject(i);

                                        /*  "EQPMNT_TYP_ID": "1",
                                            "EQPMNT_TYP_CD": "20G0",
                                            "EQPMNT_CD_ID": "1",
                                            "EQPMNT_CD_CD": "GP"*/

//                            bill_to_Bean.setItem_Id(jsonObject.getString("EQPMNT_TYP_ID"));
//                            bill_to_Bean.setCode(jsonObject.getString("EQPMNT_CD_CD"));
                            equipment_Type_id = (jsonObject.getString("EQPMNT_TYP_ID"));
                            equipment_code = (jsonObject.getString("EQPMNT_CD_CD"));

                            equipment_Type_cd = (jsonObject.getString("EQPMNT_TYP_CD"));
                            equipement_id = (jsonObject.getString("EQPMNT_CD_ID"));


                            GlobalConstants.default_equi_id = equipment_Type_id;

//                            bill_to_Bean.setName(jsonObject.getString("ENM_DSCRPTN_VC"));
//                            bill_to_Bean.setTRFF_CD_DESCRPTN_VC(jsonObject.getString("ENM_DSCRPTN_VC"));

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

            try {
                if (jsonarray != null) {
                    if (jsonarray != null || jsonarray.length() > 0) {


                        ed_code.setText(equipment_code);

                         /*
                        * dropdown services
                        * */
                        new Create_GateIn_EquipmentType_details().execute();

                        new Create_GateIn_Customer_details().execute();
                        new Create_GateIn_PreviousCargo_details().execute();


                    } else {

                        shortToast(getApplicationContext(), "Invalid Equipment Type. Click on the list for valid values");
                        ed_code.setText("");
                    }
                }
            }catch (Exception e)
            {

            }

            progressDialog.dismiss();

        }


    }
    public class baseURL_CheckDigitByContainerNo extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;
        private JSONObject jsonrootObject;
        private String get_equipment_sevice;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURL_CheckDigitByContainerNo);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try {
                JSONObject jsonObject = new JSONObject();
//GlobalConstants.equipment_no=get_equipment;
                jsonObject.put("br_strContainerNo", GlobalConstants.equipment_no);
                jsonObject.put("br_strErrorBuilder", "");



                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("res_settings", resp);
                Log.d("req_settings", String.valueOf(jsonObject));
                 jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                /*  "__type": "SettingsListModel",
                        "SettingsDirect": "",
                        "ContainerNo": "ABCD1234560",
                        "SettingsList": null*/

                SettingsDirect = getJsonObject.getString("SettingsDirect");
                get_equipment_sevice = getJsonObject.getString("ContainerNo");
                GlobalConstants.SettingsDirect=SettingsDirect;
                GlobalConstants.get_equipment_sevice=get_equipment_sevice;
                Log.d("get_equipment_sevice",  GlobalConstants.get_equipment_sevice);

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
            if(jsonrootObject!=null) {

                if(changes.equalsIgnoreCase("False"))
                {

                    if (!GlobalConstants.SettingsDirect.equals("")) {

                        shortToast(getApplicationContext(),"Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit..!");


                    } else {



                        dialog = new Dialog(Create_GateIn.this);
                        dialog.setContentView(R.layout.check_degit_equipment_no);
                        TextView tv_equip_no=(TextView)dialog.findViewById(R.id.tv_equip_no);
                        TextView equip_no=(TextView)dialog.findViewById(R.id.equip_no);
                        final TextView bt_close=(TextView) dialog.findViewById(R.id.bt_close);
                        equip_no.setText("Equipment No");
                        tv_equip_no.setText(get_equipment);

                        bt_close.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View view) {

                                Letter = get_equipment.substring(0,4);
                                Number = get_equipment.substring(4,11);
                                if (Character.isLetter(Letter.charAt(0)) && Character.isDigit(Number.charAt(4))) {
                                    Log.d("Character", Letter);
                                    Log.d("Numbers", Number);
                                    if (cd.isConnectingToInternet()) {
                                        new Post_Verify_Equipment_No().execute();

                                    } else {
                                        shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                                    }


                                } else {
                                    shortToast(getApplicationContext(), "Invalid Equipment No Format. Must 4-Alphabets, 6-Numeric, 1 Checkdigit..!");
                                }
                                dialog.dismiss();
                            }
                        });

                        dialog.show();


                    }

                }else
                {
                    if (!GlobalConstants.SettingsDirect.equals("")) {

                        shortToast(getApplicationContext(), SettingsDirect);


                    }else
                    {

                        if (!get_tare_weight.equals("")) {



                                  /*  if ((get_tare_weight.matches(".\\d"))) {
                                        get_tare_weight = "0" + get_tare_weight + "00";
                                    } else if ((get_tare_weight.matches("\\d."))) {
                                        get_tare_weight = get_tare_weight + "000";
                                    }*/

                            if (get_tare_weight.contains(".")) {

                                String[] new_MH = get_tare_weight.split(Pattern.quote("."));

                                String str1 = new_MH[0].toString();
                                String str2 = new_MH[1].toString();
                                if (str1.length() <= 7 && str2.length() == 2) {
                                    get_tare_weight = str1 + "." + str2 + "0";

                                } else if (str1.length() <= 7 && (str2.length() == 1)) {
                                    get_tare_weight = str1 + "." + str2 + "00";
                                } else if (str1.length() <= 7 && (str2.length() == 0)) {
                                    get_tare_weight = str1 + ".000";
                                } else {
//                                            shortToast(getApplicationContext(), "Invalid Tare Weight. Range must be from 0.01 to 9999999.99");
                                    get_tare_weight = get_tare_weight + ".000";
                                }

                            } else {

                                if (get_tare_weight.length() <= 7) {
                                    get_tare_weight = get_tare_weight + ".000";
                                } else {
//                                            shortToast(getApplicationContext(), "Invalid Tare Weight. Range must be from 0.01 to 9999999.99");
                                    get_tare_weight = get_tare_weight + ".000";
                                }
                            }


                        } else {
                            get_tare_weight = "";
                        }
                        if (!get_gross.equals("")) {


/*

                                    if ((get_gross.matches(".\\d"))) {
                                        get_gross = "0" + get_gross + "00";
                                    } else if ((get_gross.matches("\\d."))) {
                                        get_gross = get_gross + "000";
                                    }
*/

                            if (get_gross.contains(".")) {

                                String[] new_MH = get_gross.split(Pattern.quote("."));

                                String str1 = new_MH[0].toString();
                                String str2 = new_MH[1].toString();
                                if (str1.length() <= 7 && str2.length() == 2) {
                                    get_gross = str1 + "." + str2 + "0";

                                } else if (str1.length() <= 7 && (str2.length() == 1)) {
                                    get_gross = str1 + "." + str2 + "00";
                                } else if (str1.length() <= 7 && (str2.length() == 0)) {
                                    get_gross = str1 + ".000";
                                } else {
//                                            shortToast(getApplicationContext(), "Invalid Gross Weight. Range must be from 0.01 to 9999999.99");
                                    get_gross = get_gross + ".000";
                                }

                            } else {

                                if (get_gross.length() <= 7) {
                                    get_gross = get_gross + ".000";
                                } else {
//                                            shortToast(getApplicationContext(), "Invalid Gross Weight. Range must be from 0.01 to 9999999.99");
                                    get_gross = get_gross + ".000";
                                }
                            }


                        } else {
                            get_gross = "";
                        }
                        if (!get_capacity.equals("")) {



                                  /*  if ((get_capacity.matches(".\\d"))) {
                                        get_capacity = "0" + get_capacity + "00";
                                    } else if ((get_capacity.matches("\\d."))) {
                                        get_capacity = get_capacity + "000";
                                    }*/

                            if (get_capacity.contains(".")) {

                                String[] new_MH = get_capacity.split(Pattern.quote("."));

                                String str1 = new_MH[0].toString();
                                String str2 = new_MH[1].toString();
                                if (str1.length() <= 7 && str2.length() == 2) {
                                    get_capacity = str1 + "." + str2 + "0";

                                } else if (str1.length() <= 7 && (str2.length() == 1)) {
                                    get_capacity = str1 + "." + str2 + "00";
                                } else if (str1.length() <= 7 && (str2.length() == 0)) {
                                    get_capacity = str1 + ".000";
                                } else {
//                                            shortToast(getApplicationContext(), "Invalid Capacity. Range must be from 0.01 to 9999999.99");
                                    get_capacity = get_capacity + ".000";
                                }

                            } else {

                                if (get_capacity.length() <= 7) {
                                    get_capacity = get_capacity + ".000";
                                } else {
//                                            shortToast(getApplicationContext(), "Invalid Capacity. Range must be from 0.01 to 9999999.99");
                                    get_capacity = get_capacity + ".000";
                                }
                            }


                        } else {
                            get_capacity = "";
                        }


                        if (!get_capacity.equals("") || !get_tare_weight.equals("") || !get_gross.equals("")) {
                            String[] new_get_mh = get_tare_weight.split(Pattern.quote("."));
                            String[] new_get_mc = get_capacity.split(Pattern.quote("."));
                            String[] new_tare_weight = get_gross.split(Pattern.quote("."));

                            String str1, str1_2, str2, str2_2, str3, str3_2;
                            if (get_tare_weight.equals("")) {
                                str1 = "";
                                str1_2 = "";
                            } else {
                                str1 = new_get_mh[0].toString();
                                str1_2 = new_get_mh[1].toString();
                            }
                            if (get_capacity.equals("")) {
                                str2 = "";
                                str2_2 = "";
                            } else {
                                str2 = new_get_mc[0].toString();
                                str2_2 = new_get_mc[1].toString();
                            }
                            if (get_gross.equals("")) {
                                str3 = "";
                                str3_2 = "";
                            } else {
                                str3 = new_tare_weight[0].toString();
                                str3_2 = new_tare_weight[1].toString();
                            }


                            if (str1.length() > 7 || str1_2.length() > 3) {

                                shortToast(getApplicationContext(), "Invalid Tare Weight. Range must be from 0.01 to 9999999.99");

                            } else if (str2.length() > 7 || str2_2.length() > 3) {
                                shortToast(getApplicationContext(), "Invalid Capacity. Range must be from 0.01 to 9999999.99");

                            } else if (str3.length() > 7 || str3_2.length() > 3) {
                                shortToast(getApplicationContext(), "Invalid Gross Weight. Range must be from 0.01 to 9999999.99");

                            } else {

                                if (cd.isConnectingToInternet()) {



                                    dialog = new Dialog(Create_GateIn.this);
                                    dialog.setContentView(R.layout.check_degit_equipment_no);
                                    TextView tv_equip_no=(TextView)dialog.findViewById(R.id.tv_equip_no);
                                    TextView equip_no=(TextView)dialog.findViewById(R.id.equip_no);
                                    final TextView bt_close=(TextView) dialog.findViewById(R.id.bt_close);
                                    equip_no.setText("Equipment No");
                                    tv_equip_no.setText(GlobalConstants.get_equipment_sevice);

                                    bt_close.setOnClickListener(new View.OnClickListener() {
                                        @Override
                                        public void onClick(View view) {


                                            if (cd.isConnectingToInternet()) {
                                                get_equipment=GlobalConstants.get_equipment_sevice;
                                                new Post_Verify_Equipment_No().execute();

                                            } else {
                                                shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                                            }


                                            dialog.dismiss();
                                        }
                                    });

                                    dialog.show();

//                                                    new Post_Verify_Equipment_No().execute();



                                } else {
                                    shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                                }


                            }
                        } else {
                                       /* Letter = get_equipment.substring(0, 4);
                                        Number = get_equipment.substring(4, 11);

                                            Log.d("Character", Letter);
                                            Log.d("Numbers", Number);*/
                            if (cd.isConnectingToInternet()) {

                                dialog = new Dialog(Create_GateIn.this);
                                dialog.setContentView(R.layout.check_degit_equipment_no);
                                TextView tv_equip_no = (TextView) dialog.findViewById(R.id.tv_equip_no);
                                TextView equip_no = (TextView) dialog.findViewById(R.id.equip_no);
                                final TextView bt_close = (TextView) dialog.findViewById(R.id.bt_close);
                                equip_no.setText("Equipment No");
                                tv_equip_no.setText(GlobalConstants.get_equipment_sevice);

                                bt_close.setOnClickListener(new View.OnClickListener() {
                                    @Override
                                    public void onClick(View view) {


                                        if (cd.isConnectingToInternet()) {
                                            get_equipment=GlobalConstants.get_equipment_sevice;
                                            new Post_Verify_Equipment_No().execute();

                                        } else {
                                            shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                                        }


                                        dialog.dismiss();
                                    }
                                });

                                dialog.show();


                            } else {
                                shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                            }


                        }

                    }
                }

//                GlobalConstants.get_equipment_sevice=get_equipment_sevice;

            }
            else
            {
//                ed_time.setText(curTime);
            }



        }


    }
    public class CheckDigitBasedonCustomer extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;
        private JSONObject jsonrootObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURL_CheckDigitBasedonCustomer);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("CustomerId", get_sp_customer_code);



                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("res_settings", resp);
                Log.d("req_settings", String.valueOf(jsonObject));
                 jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");

                jsonarray = getJsonObject.getJSONArray("SettingsList");


                for (int i = 0; i < jsonarray.length(); i++) {

                    jsonObject = jsonarray.getJSONObject(i);

                                        /*  "EQPMNT_TYP_ID": "1",
                                            "EQPMNT_TYP_CD": "20G0",
                                            "EQPMNT_CD_ID": "1",
                                            "EQPMNT_CD_CD": "GP"*/


                    check_digit = (jsonObject.getString("CHK_DGT_VLDTN_BT"));


                    GlobalConstants.check_digit = check_digit;


                }


//                                 = getJsonObject.getString("SettingsDirect");



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
            if(jsonrootObject!=null) {

//                ed_time.setText(SettingDirect);
            }
            else
            {
//                ed_time.setText(curTime);
            }



        }


    }
    public class Time_settings extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONArray jsonarray;
        private JSONObject jsonrootObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Create_GateIn.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLEquip_Time_Settings);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("KY_NAM", "008");


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("res_settings", resp);
                 jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");
                SettingDirect = getJsonObject.getString("SettingsDirect");

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
            if(jsonrootObject!=null) {

                ed_time.setText(SettingDirect);
            }
            else
            {
                ed_time.setText(curTime);
            }



        }


    }




}
