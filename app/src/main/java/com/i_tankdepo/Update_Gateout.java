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
import android.graphics.PorterDuff;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;

import android.provider.MediaStore;
import android.support.annotation.RequiresApi;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.text.Editable;
import android.text.Html;
import android.text.InputFilter;
import android.text.Spanned;
import android.text.TextWatcher;
import android.util.Base64;
import android.util.Log;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.WindowManager;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.TimePicker;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.CustomerDropdownBean;
import com.i_tankdepo.Beanclass.EquipmentDropdownBean;
import com.i_tankdepo.Beanclass.Equipment_Info_TypeDropdownBean;
import com.i_tankdepo.Beanclass.Multi_Photo_Bean;
import com.i_tankdepo.Beanclass.PendingBean;
import com.i_tankdepo.Beanclass.Previous_CargoDropdownBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.SQLite.DBAdapter;
import com.i_tankdepo.Util.Utility;

import org.apache.http.HttpEntity;
import org.apache.commons.io.FilenameUtils;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import static com.i_tankdepo.R.id.ed_type;

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
public class Update_Gateout extends CommonActivity {

    //Added by Anantha krishnan
    private EditText ed_booking_auth_no,ed_lessee_name,ed_lessee_location;
    private String booking_auth_no,lessee_name,lessee_location;
    private String get_booking_auth_no,get_lessee_name,get_lessee_location;

    ImageView up,more_up,equip_up,down,more_down,equip_down;
    LinearLayout LL_general_info;
    private TextView tv_toolbarTitle, tv_add, tv_status, tv_date, tv_time, tv_heat_refresh, leakTest_text;
    private int myear, mmonth, mday;
    private ImageView menu, im_date, im_time, im_Attachment, iv_back;
    private DrawerLayout drawer;
    private Toolbar toolbar;
    private Intent mServiceIntent;
    private String userChoosenTask;
    private static final int CustomGallerySelectId = 1;//Set Intent Id
    private Button fotter_add, im_add, im_print, bt_home, bt_refresh, fotter_submit;
    private EditText ed_time, ed_attach, ed_date, ed_status, ed_location, ed_eir_no, ed_vechicle, ed_transport, ed_remark, ed_code;
    static final int DATE_DIALOG_ID = 1999;
    private Calendar c;
    private int year,month,day,second;
    private String systemDate,getType,get_date,get_time,get_status,get_location,get_vechicle,get_transport,get_eir_no,get_remark;
    private String curTime,get_swt_rental;
    int mHour,mMinute;

    private boolean manuf_date=false;

    TimePickerDialog timePickerDialog;

    ArrayList<String[]> dropdown_customer_list = new ArrayList<>();
    private String CustomerName,CustomerCode,EquipmentType,EquipmentCode,PreviousCargoID,PreviousCargoCode,PreviousCargoDescription;
    List<String> Cust_name = new ArrayList<>();
    List<String> Cust_code = new ArrayList<>();
    ArrayList<String> dropdown_equipment_list = new ArrayList<>();
    List<String> Equip_Type = new ArrayList<>();
    List<String> Equip_Code = new ArrayList<>();
    String get_manu_date,get_tare_weight,get_gross, get_capacity,get_last_survy,get_last_test_date,get_last_test_type,get_next_date,get_next_type,
            get_info_remark;
    String EIEQPMNT_TYP_CD,EIMNFCTR_DT,EITR_WGHT_NC,EIGRSS_WGHT_NC,EICPCTY_NC,
            EILST_SRVYR_NM,EILST_TST_DT,EILST_TST_TYP_ID,EINXT_TST_DT,EINXT_TST_TYP_ID,EIRMRKS_VC,EIACTV_BT,EIRNTL_BT;
    ArrayList<String> dropdown_PreviousCargo_list = new ArrayList<>();
    ArrayList<Previous_CargoDropdownBean> dropdown_PreviousCargo_arraylist = new ArrayList<>();
    List<String> Cargo_ID = new ArrayList<>();
    List<String> Cargo_Code = new ArrayList<>();
    List<String> Cargo_Description = new ArrayList<>();
    LinearLayout LL_Submit,LL_footer_delete;
    private int pendingsize;
    String equip_no,From ,Cust_Name,previous_crg,gateIn_Id,code,location,cust_code,type_id,code_id,pre_code,pre_id,
            trans_no, vechicle,transport,Eir_no,heating_bt,rental_bt,remark,type,status,date,time,get_swt_info_rental,get_swt_info_active;

    Switch rental;
    private String get_sp_customer,get_sp_equipe,get_sp_previous,get_sp_previous_id;
    private CustomerDropdownBean customer_DropdownBean;
    ArrayList<CustomerDropdownBean> CustomerDropdownArrayList = new ArrayList<>();
    private ArrayList<String> worldlist;
    private EquipmentDropdownBean equipment_DropdownBean;
    private ArrayList<EquipmentDropdownBean> dropdown_equipment_arraylist;
    private Previous_CargoDropdownBean cargo_DropdownBean;
    private String equipement_code,equipement_id;
    public static boolean isCamPermission;

    private int REQUEST_CAMERA = 0, SELECT_FILE = 1;
    private String TAG="Create_GateIn";
    private String encodedImage,selectedImagePath,filename,IfAttchment;
    private File file;
    private String validationStatus;
    private JSONObject reqObj;
    private String changes;
    private ArrayList<Equipment_Info_TypeDropdownBean> dropdown_MoreInfo_arraylist;
    private Equipment_Info_TypeDropdownBean moreInfo_DropdownBean;
    ArrayList<String> dropdown_MoreInfo_list = new ArrayList<>();
    private TextView text1;
    private String Gi_transaction_id;
    private ImageView iv_changeOfStatus;
    private Button heat_home,heat_refresh,bt_heating,cleaning,inspection,heat_submit,Leaktest,bt_gateout;
    private LinearLayout LL_heat,LL_heat_submit;
    private RelativeLayout RL_heating,RL_Repair;
    private String Filename;
    private String imageName;
    private String filePath;
    private Switch rentalbit;
    public static final String CustomGalleryIntentKey = "ImageArray";//Set Intent Key Value
    private ArrayList<Multi_Photo_Bean> encodeArray;
    private Bitmap selectedImageBitmap;
    private Multi_Photo_Bean multi_photo_bean;
    private DBAdapter db;
    private Calendar mcurrentTime;
    private int hour, minute;
    private TimePickerDialog mTimePicker;
    private ImageView more_info,tv_view_remarks;
    private JSONObject print_object;
    private String get_equipment;
    private String get_code;
    private String current_date;
    private Date date2,date1;
    private String get_status_id;
    private String get_previous_cargo;
    private String Str = "";
    private ArrayList<String> list;
    private Date Activity_date1;
    boolean new_attachment=false;
    private String ReportURL;

    @RequiresApi(api = Build.VERSION_CODES.JELLY_BEAN)
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.update_gate_out);
        pendingsize= GlobalConstants.pendingcount;
        Cust_Name= GlobalConstants.customer_name;
        From= GlobalConstants.from;
        Gi_transaction_id = GlobalConstants.pre_adv_id;
        encodeArray= GlobalConstants.pending_attach_arraylist;
        gateIn_Id=  GlobalConstants.GateInId;

        db = new DBAdapter(Update_Gateout.this);

        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        equip_no = GlobalConstants.equipment_no;
        location = GlobalConstants.location;
        type = GlobalConstants.type;
        date = GlobalConstants.date;

        //Added by Anantha krishnan
        booking_auth_no = GlobalConstants.AUTH_NO;
        lessee_name = GlobalConstants.LSS_NM;
        lessee_location = GlobalConstants.LSS_LCTN;

        try {
            Activity_date1 = new SimpleDateFormat("dd-MM-yyyy").parse(date);
        }catch (Exception e)
        {

        }
        time = GlobalConstants.time;
        code = GlobalConstants.code;
        status = GlobalConstants.status;
        Eir_no = GlobalConstants.eir_no;
        vechicle = GlobalConstants.vechicle_no;
        transport = GlobalConstants.Transport_No;
        rental_bt = GlobalConstants.rental_bt;
        remark = GlobalConstants.remark;
        filename =GlobalConstants.attach_filename;

        im_print = (Button)findViewById(R.id.bt_gateout);

        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        tv_status = (TextView) findViewById(R.id.tv_status);
        tv_date = (TextView) findViewById(R.id.tv_date);
        tv_time = (TextView) findViewById(R.id.tv_time);
        ed_date = (EditText) findViewById(R.id.ed_date);
        ed_time = (EditText) findViewById(R.id.ed_time);
        ed_status = (EditText) findViewById(R.id.ed_status);
        ed_code = (EditText) findViewById(R.id.ed_code);
        heat_home = (Button) findViewById(R.id.heat_home);
        heat_refresh = (Button) findViewById(R.id.heat_refresh);
        bt_heating = (Button) findViewById(R.id.heating);
        RL_Repair = (RelativeLayout) findViewById(R.id.RL_Repair);
        bt_heating.setVisibility(View.GONE);

        tv_view_remarks = (ImageView)findViewById(R.id.tv_view_remarks);
        more_info = (ImageView)findViewById(R.id.more_info);
        more_info.setOnClickListener(this);
        tv_view_remarks.setOnClickListener(this);
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        LL_heat = (LinearLayout)findViewById(R.id.LL_heat);
        inspection.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);

        LL_heat.setClickable(false);
        if(From.equalsIgnoreCase("new"))
        {
            LL_heat.setAlpha(0.5f);
        }else {
            im_print.setOnClickListener(this);
        }
        Leaktest = (Button)findViewById(R.id.leakTest);
        bt_gateout = (Button)findViewById(R.id.bt_gateout);
        Leaktest.setVisibility(View.GONE);
        leakTest_text = (TextView)findViewById(R.id.tv_heating);
        leakTest_text.setGravity(Gravity.CENTER_HORIZONTAL);
        leakTest_text.setText("Print");
        LL_heat_submit = (LinearLayout)findViewById(R.id.LL_heat_submit);
        heat_submit = (Button) findViewById(R.id.heat_submit);

        tv_heat_refresh = (TextView)findViewById(R.id.tv_heat_refresh);
        tv_heat_refresh.setText("Reset");

        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_Repair.setVisibility(View.GONE);


      //  ed_previous = (EditText)findViewById(R.id.ed_previous);
        ed_attach = (EditText)findViewById(R.id.ed_attach);
        rental=(Switch)findViewById(R.id.switch_rental);
        rental.setClickable(false);


        text1 = (TextView)findViewById(R.id.text1);
      //  text2 = (TextView)findViewById(R.id.text2);
        ed_eir_no = (EditText)findViewById(R.id.ed_eir_no);

        ed_eir_no.setFilters(new InputFilter[] {
                new InputFilter.AllCaps() {
                    @Override
                    public CharSequence filter(CharSequence source, int start, int end, Spanned dest, int dstart, int dend) {
                        return String.valueOf(source).toUpperCase();
                    }
                },
                new InputFilter.LengthFilter(50)
        });
        ed_location = (EditText)findViewById(R.id.ed_location);
        ed_vechicle = (EditText)findViewById(R.id.ed_vechicle);
        ed_transport = (EditText)findViewById(R.id.ed_transport);
        ed_remark = (EditText)findViewById(R.id.ed_remarks);
        im_date =(ImageView)findViewById(R.id.im_date);
        im_Attachment =(ImageView)findViewById(R.id.im_Attachment);
        im_time =(ImageView)findViewById(R.id.im_time);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        try {
            boolean attach_disable=false;
            if (encodeArray.size()>0) {
                for(int i=0;i<encodeArray.size();i++)
                {
                    if(equip_no.equals(encodeArray.get(i).getEquipmentNo())) {
                        Str += " " + encodeArray.get(i).getName() + ",";
                        if(encodeArray.get(i).getName().equals(""))
                        {
                            attach_disable=true;
                        }

                    }else {
                    }
                }
                if (Str.endsWith(",")) {
                    Str = Str.substring(0, Str.length() - 1);
                    ed_attach.setText(Str);
                }
                if(Str.equals(""))
                {

                }
               /* if(attach_disable)
                {
                }else {


                }*/
                ed_attach.setText(Str);

            }else {



            }
        }catch (Exception e)
        {
        }
//        SimpleDateFormat sdf = new SimpleDateFormat("dd-MMM-yyyy");
        ed_date.setText(date);
        Log.i("time",time);
        ed_time.setText(time);
        ed_location.setText(location.replaceAll("\\s+$", ""));
        ed_eir_no.setText(Eir_no);
        ed_remark.setText(remark);
        ed_code.setText(code);
        ed_status.setText(status);

        //Added by Anantha krishnan
        ed_booking_auth_no = (EditText) findViewById(R.id.ed_booking_auth_no);
        ed_booking_auth_no.setText(booking_auth_no);
        ed_lessee_name = (EditText) findViewById(R.id.ed_lessee_name);
        ed_lessee_name.setText(lessee_name);
        ed_lessee_location = (EditText) findViewById(R.id.ed_lessee_location);
        ed_lessee_location.setText(lessee_location);




        if(vechicle.equals("")||vechicle.equalsIgnoreCase("null")||vechicle=="null")
        {
            ed_vechicle.setText("");
        }else
        {
            ed_vechicle.setText(vechicle);
        }

        text1.setText(Cust_Name+" , "+equip_no+" , "+type);
        ed_transport.setText(transport);


        tv_toolbarTitle.setText("Update GateOut");
        menu=(ImageView)findViewById(R.id.iv_menu) ;
        iv_back = (ImageView)findViewById(R.id.iv_back);
        menu.setVisibility(View.GONE);
        equip_up=(ImageView)findViewById(R.id.equip_up) ;
        equip_down=(ImageView)findViewById(R.id.equip_down) ;


        if(rental_bt.equalsIgnoreCase("True"))
        {
            rental.setChecked(true);
            get_swt_rental="True";
        }else
        {
            rental.setChecked(false);
            get_swt_rental="False";
        }



        rental.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if(b) {
                    get_swt_rental="True";
                }else
                {
                    get_swt_rental="False";
                }
            }
        });
        if ((ed_attach.getText().length() < 0)|| Str.equals("")) {
            //im_Attachment.setColorFilter(Color.CYAN,PorterDuff.Mode.LIGHTEN);
            //   Toast.makeText(Update_Gateout.this,"greater",Toast.LENGTH_LONG).show();
            Animation fade = AnimationUtils.loadAnimation(this, R.anim.img_fade);
            im_Attachment.startAnimation(fade);

        } else {
            //im_Attachment.setColorFilter(Color.CYAN, PorterDuff.Mode.DARKEN);
            //  Toast.makeText(Update_Gateout.this,"0",Toast.LENGTH_LONG).show();

        }
        im_Attachment.setOnClickListener(this);

       
        drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();

        iv_back.setOnClickListener(this);
        ed_time.setOnClickListener(this);
        ed_date.setOnClickListener(this);
        im_date.setOnClickListener(this);
        im_time.setOnClickListener(this);
        heat_home.setOnClickListener(this);
        heat_refresh.setOnClickListener(this);
        heat_submit.setOnClickListener(this);


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

        /*ed_date.setText(systemDate);
        ed_time.setText(curTime);*/





        String outDate = getColoredSpanned("Out Date","#bbbbbb");
        String tvtime = getColoredSpanned("Time","#bbbbbb");


        String surName = getColoredSpanned("*","#cb0da5");

        tv_date.setText(Html.fromHtml(outDate+" "+surName));
        tv_time.setText(Html.fromHtml(tvtime+" "+surName));




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

    }
    // EditTextWacther  Implementation

    private final TextWatcher mTextEditorWatcher = new TextWatcher() {

        public void beforeTextChanged(CharSequence s, int start, int count, int after)
        {
            // When No Password Entered
        }

        public void onTextChanged(CharSequence s, int start, int before, int count)
        {

        }

        public void afterTextChanged(Editable s)
        {
            if(s.length()<=11) {
               // textViewPasswordStrengthIndiactor.setText("Password Max Length Reached");
               // shortToast(getApplicationContext(),"Please Enter 11 Digit");
            }else if(s.length()==11 )
            {

            }
        }
    };



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
            case R.id.ed_date:

                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.tv_view_remarks:
                get_remark=ed_remark.getText().toString();
              popUp_remarks(get_remark);
                break;
            case R.id.im_date:

                showDialog(DATE_DIALOG_ID);

                break;
            case R.id.more_info:

                get_status = GlobalConstants.status;
                get_status_id = GlobalConstants.status_id;
                get_previous_cargo = GlobalConstants.previous_cargo;

                popUp_equipment_info(GlobalConstants.equipment_no, get_status, get_status_id, get_previous_cargo, "", "", "", "");


                break;
            case R.id.bt_gateout:


                if (cd.isConnectingToInternet()) {

                    new Print_pdf().execute();

                }else{
                    shortToast(getApplicationContext(), "Please check your Internet Connection.");
                }

                break;

            case R.id.ed_manfu:

                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_manuf_date:

                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.ed_test_date:
                showDialog(DATE_DIALOG_ID);
                break;
            case R.id.im_last_Testdate:
                showDialog(DATE_DIALOG_ID);
                break;

            case R.id.iv_back:
                finish();
                onBackPressed();
                break;
            case R.id.heat_submit:

                String attachment=ed_attach.getText().toString();

                try {

                    if(!attachment.equals(""))
                    {
                        IfAttchment = "True";

                    }else {
                        IfAttchment = "False";

                    }
                  /*  if( (encodedImage.length() > 0) ) {
                        IfAttchment = "True";
                    } else if(pending_attach_arraylist.size()>0) {
                        IfAttchment = "True";
                    }else {
                        IfAttchment = "False";
                    }*/
                }catch (Exception e)
                {
//                    IfAttchment = "False";
                }




                get_date=ed_date.getText().toString();
                get_time=ed_time.getText().toString();
                get_location=ed_location.getText().toString();
                get_eir_no=ed_eir_no.getText().toString().toUpperCase();
                get_vechicle=ed_vechicle.getText().toString();
                get_transport=ed_transport.getText().toString();
                get_remark=ed_remark.getText().toString();

                //Added by Anantha krishnan

                get_booking_auth_no = ed_booking_auth_no.getText().toString();
                get_lessee_name = ed_lessee_name.getText().toString();
                get_lessee_location = ed_lessee_location.getText().toString();


              //  get_last_test_type=sp_last_test_type.getSelectedItem().toString();
            if(cd.isConnectingToInternet()) {

                if ((get_time.trim().equals("") || get_time == null) ||
                        (get_date.trim().equals("") || get_date == null)) {

                    shortToast(getApplicationContext(), "Please Key-in Mandate Fields");

                }
                else {
                    Pattern pattern = Pattern.compile("[a-zA-Z0-9]*");
                    try {
                        current_date = new SimpleDateFormat("dd-MMM-yyyy").format(new Date());
                        SimpleDateFormat sdf = new SimpleDateFormat("dd-MMM-yyyy");
                        date1 = sdf.parse(get_date);
                        date2 = sdf.parse(current_date);

    /* historyDate <= todayDate <= futureDate */

                        if ((date1.compareTo(date2) > 0)) {
                            shortToast(getApplicationContext(), "cannot select future date");
                        } else {
                            Matcher matcher = pattern.matcher(get_eir_no);
                            if (!matcher.matches()) {
                                shortToast(getApplicationContext(), "Special Character Not Allowed in EIR No");
                            } else {
                                if (cd.isConnectingToInternet()) {

                                    new PostInfo_GateOut().execute();

                                } else {
                                    shortToast(getApplicationContext(), "Please Check Your Internet Connection");
                                }
                            }

                        }

                    } catch (Exception e) {
                       Log.i("e", String.valueOf(e));
                        e.printStackTrace();
                    }
                }
            }else{
                if ((get_time.trim().equals("") || get_time == null) ||
                        (get_date.trim().equals("") || get_date == null)) {
                    shortToast(getApplicationContext(), "Please Key-in Mandate Fields");
                } else {


                    Pattern pattern = Pattern.compile("[a-zA-Z0-9]*");

                    Matcher matcher = pattern.matcher(get_eir_no);
                    if (!matcher.matches()) {
                        shortToast(getApplicationContext(), "Special Character Not Allowed in EIR No");
                    } else {
                        if (cd.isConnectingToInternet()) {

                            db.open();
                            db.updateGateOut(sp.getString(SP_USER_ID,"user_Id"),equip_no,get_location,get_date,get_time,get_eir_no,get_vechicle,get_transport,
                                    get_remark,get_swt_rental,get_booking_auth_no,get_lessee_name,get_lessee_location,Gi_transaction_id,IfAttchment,From);
                            db.close();

                        } else {
                            shortToast(getApplicationContext(), "Please Check Your Internet Connection");
                        }
                    }

                }
            }
                break;

            case R.id.ed_time:
                mcurrentTime = Calendar.getInstance();
                hour = mcurrentTime.get(Calendar.HOUR_OF_DAY);
                minute = mcurrentTime.get(Calendar.MINUTE);

                mTimePicker = new TimePickerDialog(Update_Gateout.this, new TimePickerDialog.OnTimeSetListener() {
                    @Override
                    public void onTimeSet(TimePicker timePicker, int selectedHour, int selectedMinute) {
                        ed_time.setText(String.format("%02d:%02d", selectedHour, selectedMinute));

                    }
                }, hour, minute, true
                );
                mTimePicker.show();
                break;
            case R.id.im_time:
                mcurrentTime = Calendar.getInstance();
                hour = mcurrentTime.get(Calendar.HOUR_OF_DAY);
                minute = mcurrentTime.get(Calendar.MINUTE);

                mTimePicker = new TimePickerDialog(Update_Gateout.this, new TimePickerDialog.OnTimeSetListener() {
                    @Override
                    public void onTimeSet(TimePicker timePicker, int selectedHour, int selectedMinute) {
                        ed_time.setText(String.format("%02d:%02d", selectedHour, selectedMinute));

                    }
                }, hour, minute, true
                );
                mTimePicker.show();
                break;
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(),MainActivity.class));
                break;
            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.im_Attachment:


                final CharSequence[] items = { "Take Photo", "Choose from Library"};

                isCamPermission = sp2.getBoolean(SP2_CAMERA_PERM_DENIED, false);
                AlertDialog.Builder builder = new AlertDialog.Builder(Update_Gateout.this);
                builder.setTitle("Add Photo!");
                builder.setItems(items, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int item) {
                        boolean result= Utility.checkPermission(Update_Gateout.this, isCamPermission);

                        if (items[item].equals("Take Photo")) {
                            userChoosenTask ="Take Photo";
                            if(result) {
                                Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
                                try {
                                    startActivityForResult(intent, REQUEST_CAMERA);
                                }catch (Exception e)
                                {
                                   /* isCamPermission = sp2.getBoolean(SP2_CAMERA_PERM_DENIED, false);

                                    result = Utility.checkPermission(Update_Gateout.this, isCamPermission);*/

                                    shortToast(getApplicationContext(),"Camera Permission is Required");


                                }
                            }

                        } else if (items[item].equals("Choose from Library")) {
                            userChoosenTask ="Choose from Library";
                            if(result)
                                startActivityForResult(new Intent(Update_Gateout.this, CustomGallery_Activity.class), CustomGallerySelectId);

                        }
                    }
                });
                builder.show();


                break;

        }
    }

    private void selectImage() {
        final CharSequence[] items = { "Choose from Library" };

        isCamPermission = sp2.getBoolean(SP2_CAMERA_PERM_DENIED, false);
        AlertDialog.Builder builder = new AlertDialog.Builder(Update_Gateout.this);
        builder.setTitle("Add Photo!");
        builder.setItems(items, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int item) {
                boolean result= Utility.checkPermission(Update_Gateout.this, isCamPermission);

              /*  if (items[item].equals("Take Photo")) {
                    userChoosenTask ="Take Photo";
                    if(result)
                        cameraIntent();

                } else*/ if (items[item].equals("Choose from Library")) {
                    userChoosenTask ="Choose from Library";
                    if(result)
                    startActivityForResult(new Intent(Update_Gateout.this, CustomGallery_Activity.class), CustomGallerySelectId);


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
        GridView_Adapter adapter = new GridView_Adapter(Update_Gateout.this, imagesArray, false);


        for (int i = 0; i < imagesArray.size(); i++) {
//            imagesPathList.add(imagesArray(i));
            selectedImageBitmap = BitmapFactory.decodeFile(imagesArray.get(i));

            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            selectedImageBitmap.compress(Bitmap.CompressFormat.JPEG, 100, byteArrayOutputStream);
            byte[] byteArrayImage = byteArrayOutputStream.toByteArray();
            encodedImage = Base64.encodeToString(byteArrayImage, Base64.DEFAULT);
            Log.i("encodedImage ", encodedImage);
           /* filePath = imagereturnintent.getData().getPath();
            multi_photo_bean= new Multi_Photo_Bean();
            file = new File(filePath);
            Filename= file.getAbsolutePath();
            filename=Filename.substring(Filename.lastIndexOf("/")+1);*/
            multi_photo_bean=new Multi_Photo_Bean();
//            Filename="imageName";
            multi_photo_bean.setName(Filename+imagesArray.get(i));
            multi_photo_bean.setBase64(encodedImage);
            multi_photo_bean.setLength(String.valueOf(encodedImage.length()));
            encodeArray.add(multi_photo_bean);
            String path = imagesArray.get(i);
            String filename = path.substring(path.lastIndexOf("/") + 1);
            Log.d("Last ", filename);
            list.add(filename);
        }
        Log.d("Array", String.valueOf(list));
//        ed_attach.setText((CharSequence) list);
        String Str = "";
        for (int ii = 0; ii < list.size(); ii++) {
            Str += " " + list.get(ii) + ",";
        }
        if (Str.endsWith(",")) {
            Str = Str.substring(0, Str.length() - 1);
            ed_attach.setText(Str);
        }


        //   ed_attach.setText(multi_photo_bean.getName());
//        ed_attach.setText("imageName");
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
        multi_photo_bean=new Multi_Photo_Bean();
//            Filename="imageName";
        multi_photo_bean.setName(filename);
        multi_photo_bean.setBase64(encodedImage);
        multi_photo_bean.setLength(String.valueOf(encodedImage.length()));
        encodeArray.add(multi_photo_bean);
        ed_attach.setText(filename);
        list.add(filename);
        for (int ii = 0; ii < list.size(); ii++) {
            Str += " " + list.get(ii) + ",";
        }
        if (Str.endsWith(",")) {
            Str = Str.substring(0, Str.length() - 1);
            ed_attach.setText(Str);
        }
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
           /* filePath = getPath(this, selectedImageUri);
             filename=selectedImagePath.substring(selectedImagePath.lastIndexOf("/")+1);*/

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
            filename=Filename.substring(Filename.lastIndexOf("/")+1);

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

    /*  @Override
    protected Dialog onCreateDialog(int id) {
        Calendar c = Calendar.getInstance();
        int cyear = c.get(Calendar.YEAR);
        int cmonth = c.get(Calendar.MONTH);
        int cday = c.get(Calendar.DAY_OF_MONTH);
        switch (id) {
            case DATE_DIALOG_ID:
                //start changes...
                DatePickerDialog dialog = new DatePickerDialog(this, pickerListener, cyear, cmonth, cday);
                dialog.getDatePicker().setMinDate(new Date().getTime());
                dialog.getDatePicker().setMaxDate(Activity_date1.getTime());
                return dialog;
            //end changes...
        }
        return null;
    }*/
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

            year = selectedYear;
            month = selectedMonth;
            day = selectedDay;


                ed_date.setText(formatDate(year, month, day));

        }
    };



    @Override
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

    }



    public class PostInfo_GateOut extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        private JSONArray invite_jsonlist;
        private JSONObject invitejsonObject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Update_Gateout.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.    baseURLUpdate_GateOut);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();

                invite_jsonlist = new JSONArray();
                reqObj = new JSONObject();
                try {
                    for(int i=0;i<encodeArray.size();i++) {
                        invitejsonObject = new JSONObject();
                        if(new_attachment) {
                            invitejsonObject.put("FileName", encodeArray.get(i).getName());
                            invitejsonObject.put("ContentLength", encodeArray.get(i).getLength());
                            invitejsonObject.put("base64imageString", encodeArray.get(i).getBase64());
                        }else {
                            invitejsonObject.put("FileName", encodeArray.get(i).getName());
                            invitejsonObject.put("base64imageString", encodeArray.get(i).getPathUrl());
                        }
                        invite_jsonlist.put(invitejsonObject);
                    }
                    reqObj.put("ArrayOfFileParams",invite_jsonlist);




                }catch (Exception e)
                {
                    runOnUiThread(new Runnable() {

                        @Override
                        public void run() {
                            //update ui here
                            // display toast here
                            //    pluse.setVisibility(View.INVISIBLE);
                            // no_eventtoday.setVisibility(View.INVISIBLE);
                            //   shortToast(getApplicationContext(),"please select Members");


                        }
                    });

                }
                SimpleDateFormat fromUser = new SimpleDateFormat("dd-MM-yyyy", Locale.ENGLISH);
                SimpleDateFormat myFormat = new SimpleDateFormat("dd-MMM-yyyy",Locale.ENGLISH);
                String datePattern = "\\d{2}-\\d{2}-\\d{4}";
                try {
                    if (get_date.equals(null) || get_date.length() < 0) {
                        get_date = "";
                    } else {
                        Boolean is_next_Date1 = get_date.matches(datePattern);
                        if (is_next_Date1 == true) {
                            get_date = myFormat.format(fromUser.parse(get_date));


                        } else {
                            get_date = get_date;

                        }

                    }

                }catch (Exception e)
                {
                    get_date = "";
                }

                String numberAsString = Integer.toString( pendingsize+1);
                jsonObject.put("EquipmentNo",equip_no);
                jsonObject.put("YardLocation", get_location);
                jsonObject.put("OutDate", get_date );
                jsonObject.put("Time", get_time);
                jsonObject.put("EIRNo", get_eir_no);
                jsonObject.put("VehicalNo", get_vechicle);
                jsonObject.put("TransPorter",get_transport);
                jsonObject.put("Remarks",get_remark);
                jsonObject.put("Rental", get_swt_rental);
                jsonObject.put("RepairEstimateId", Gi_transaction_id);

                //Added by Anantha krishnan
                jsonObject.put("booking_auth_no",get_booking_auth_no);
                jsonObject.put("lessee_name", get_lessee_name);
                jsonObject.put("lessee_location", get_lessee_location);

               /* if(attachmentstatus.equalsIgnoreCase("True"))
                {

                }else{
                    jsonObject.put("RepairEstimateId", gateIn_Id);

                }*/

                jsonObject.put("IfAttchment",IfAttchment );
                jsonObject.put("UserName",sp.getString(SP_USER_ID,"user_Id"));

                if (From.equalsIgnoreCase("new"))
                {
                    jsonObject.put("Mode", "new");

                }else {
                    jsonObject.put("Mode", "edit");
                }

                jsonObject.put("hfc", reqObj);



                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("Update request", jsonObject.toString());
                Log.d("Update responce", resp);
                JSONObject jsonResp = new JSONObject(resp);


                JSONObject returnMessage = jsonResp.getJSONObject("d");
                String message = returnMessage.getString("Status");
                responseString=message;
                Log.d("responseString", returnMessage.toString());
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
            if(responseString!=null) {
                if (responseString.equalsIgnoreCase("Updated")) {
                    Toast.makeText(getApplicationContext(), "Gate Out : Equipment(s) Updated Successfully.", Toast.LENGTH_SHORT).show();
                    finish();
                    if(GlobalConstants.from.equalsIgnoreCase("new")) {
                        Intent i = new Intent(getApplicationContext(), GateOut.class);
                        startActivity(i);
                    }else{
                        Intent i = new Intent(getApplicationContext(), GateOut_Mysubmit.class);
                        startActivity(i);
                    }
                }else{
                    shortToast(getApplicationContext(),"GateOut Not Updated..!");
                }
            }else
            {
                Toast.makeText(getApplicationContext(), "Unable to connect the Server..!", Toast.LENGTH_SHORT).show();

            }
            progressDialog.dismiss();

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
            progressDialog = new ProgressDialog(Update_Gateout.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURL_gateOutPrintDocument);
            httpPost.setHeader("Content-Type", "application/json");
            try {
                jsonObject = new JSONObject();

                /*
                {
                    "gateinID":"78",
                        "UserName":"ADMIN",
                        "depotid":"1"

                }*/

                jsonObject.put("gateOutID",  gateIn_Id);
                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("depotid","1");



                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("Update request", jsonObject.toString());
                Log.d("Update responce", resp);
                JSONObject jsonResp = new JSONObject(resp);


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
                        try
                        {
                            Intent i = new Intent(Intent.ACTION_VIEW);
                            i.setData(Uri.parse(ReportURL));
                            startActivity(i);
                        }catch (Exception e)
                        {
                            shortToast(getApplicationContext(),"Incorrect URL");
                        }

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
