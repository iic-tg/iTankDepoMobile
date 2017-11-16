package com.i_tankdepo;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.provider.MediaStore;
import android.support.design.widget.NavigationView;
import android.support.v4.app.FragmentActivity;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.util.Base64;
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
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.Image_Bean;
import com.i_tankdepo.Beanclass.Item;
import com.i_tankdepo.Beanclass.LineItem_Bean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.Util.Utility;
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
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
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
public class Attach_Repair_Completion_MySubmit extends CommonActivity implements View.OnClickListener {

    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu, im_down, im_up, im_heat_close, im_heat_ok, iv_back;


    private TextView tv_toolbarTitle, tv_search_options,no_data;
    LinearLayout LL_hole, LL_heat_submit,LL_search_Value,LL_heat;

    private ProgressDialog progressDialog;

    private Intent mServiceIntent;

    private Button heat_refresh, heat_home, heat_submit,heating,cleaning,inspection;
    private FragmentActivity mycontaxt;
    private TextView customer_name,equipment_no,eqip_type;
    private Button LL_Line_iteam,bt_refresh,LL_add_details,LL_attachments,LL_summary;
    private Spinner sp_tarif_group,sp_tarif_code;
    private ListView attachment_list;
    private ImageView im_capture;
    boolean isCamPermission;
    String userChoosenTask;
    private static final int CustomGallerySelectId = 1;//Set Intent Id
    public static final String CustomGalleryIntentKey = "ImageArray";//Set Intent Key Value
    private Bitmap selectedImageBitmap;
    private String encodedImage;
    private ArrayList<Image_Bean> encodeArray;
    private Image_Bean image_bean;
    private ArrayList<Item> box;
    private ArrayList<Item> items;
    private ImageView iv_changeOfStatus;
    private LinearLayout attachments;
    private JSONArray invite_jsonlist;
    private JSONObject d_reqObj,reqObj;
    private JSONObject invitejsonObject;
    private ViewHolder holder;
    private UserListAdapter adapter;
    private JSONArray json_array;
    private ArrayList<LineItem_Bean> lineitem_arraylist;
    private JSONObject object2;
    private String IfAttchment;
    private Button notification_text;
    private TextView repair_estimate_text;
    private Button repair_estimate,repair_approval,repair_completion,survey_completion;
    private RelativeLayout RL_heating,RL_Repair;
    private ImageView more_info;
    private String add_detail_jsonobject;
    private ImageView add_pen,iv_send_mail;
    private int REQUEST_CAMERA = 0, SELECT_FILE = 1;
    private File selectedImage;
    private String filePath,extansion,Filename;
    private Bitmap bitmap=null;
    int service_count=0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.repair_attachment_completion);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);


        add_detail_jsonobject= GlobalConstants.add_detail_jsonobject;
        encodeArray=GlobalConstants.multiple_encodeArray;

        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setOnClickListener(this);
        iv_changeOfStatus = (ImageView) findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);
        menu.setVisibility(View.GONE);
        LL_Line_iteam=(Button) findViewById(R.id.Line_iteam);
        LL_Line_iteam.setOnClickListener(this);
        LL_add_details=(Button)findViewById(R.id.add_details);
        bt_refresh = (Button) findViewById(R.id.heat_refresh);
        bt_refresh.setOnClickListener(this);
        LL_add_details.setOnClickListener(this);
        LL_attachments=(Button)findViewById(R.id.attachments);
        attachments=(LinearLayout)findViewById(R.id.LL_attachments);
        attachments.setBackgroundColor(getResources().getColor(R.color.submit));
        notification_text = (Button)findViewById(R.id.notification_text);
        repair_estimate_text = (TextView)findViewById(R.id.tv_heating);
        repair_estimate_text.setText("Repair Completion");
        LL_attachments.setOnClickListener(this);
        im_capture=(ImageView)findViewById(R.id.im_capture);
        im_capture.setOnClickListener(this);
        more_info = (ImageView) findViewById(R.id.more_info);
        more_info.setOnClickListener(this);

        attachment_list=(ListView)findViewById(R.id.attachment_list);
      //  LL_summary=(Button)findViewById(R.id.summary);
       // LL_summary.setOnClickListener(this);
        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_heating.setVisibility(View.GONE);

        heat_home = (Button) findViewById(R.id.heat_home);
        heat_refresh = (Button) findViewById(R.id.heat_refresh);
        heat_submit = (Button) findViewById(R.id.heat_submit);
        LL_heat_submit = (LinearLayout) findViewById(R.id.LL_heat_submit);
        heat_submit.setOnClickListener(this);
        LL_heat = (LinearLayout)findViewById(R.id.LL_heat);
        LL_heat.setOnClickListener(this);

        repair_estimate = (Button)findViewById(R.id.repair_estimate);
        repair_estimate.setOnClickListener(this);
        repair_approval = (Button)findViewById(R.id.repair_approval);
        repair_approval.setVisibility(View.GONE);
        repair_completion = (Button)findViewById(R.id.repair_completion);
        repair_completion.setVisibility(View.GONE);
        survey_completion = (Button)findViewById(R.id.survey_completion);
        survey_completion.setVisibility(View.GONE);
//        tabLayout.setupWithViewPager(viewPager);

        iv_send_mail = (ImageView) findViewById(R.id.iv_send_mail);
        add_pen = (ImageView) findViewById(R.id.add_pen);
        iv_send_mail.setOnClickListener(this);
       // tabLayout.clearOnTabSelectedListeners();

        add_pen.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LL_add_details.performClick();
            }
        });
        heating = (Button)findViewById(R.id.heating);
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        inspection.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);

      /*  heat_refresh.setOnClickListener(this);
        heat_home.setOnClickListener(this);*/
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);

        Log.i("customer_name",GlobalConstants.customer_name);
        customer_name = (TextView) findViewById(R.id.text1);
        customer_name.setText(GlobalConstants.customer_name + " , " + GlobalConstants.equipment_no + " , " + GlobalConstants.type);
       // equipment_no = (TextView) findViewById(R.id.text2);
//        equipment_no.setText(GlobalConstants.equipment_no);
        eqip_type = (TextView) findViewById(R.id.text3);
//        eqip_type.setText(GlobalConstants.previous_cargo);
        tv_toolbarTitle.setText("Attach files");

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


        if(GlobalConstants.from.equalsIgnoreCase("Repairpending_delete"))
        {
            try {
                encodeArray= GlobalConstants.multiple_encodeArray;
                adapter = new UserListAdapter(Attach_Repair_Completion_MySubmit.this, R.layout.image_list_row, encodeArray);
                attachment_list.setAdapter(adapter);

            }catch (Exception e)
            {
                Log.i("Exception", String.valueOf(e));
            }

        }else
        {
            encodeArray= GlobalConstants.multiple_encodeArray;
            adapter = new UserListAdapter(Attach_Repair_Completion_MySubmit.this, R.layout.image_list_row, encodeArray);
            attachment_list.setAdapter(adapter);
        }
        try {
            if (encodeArray.size() > 0) {

                adapter = new UserListAdapter(Attach_Repair_Completion_MySubmit.this, R.layout.image_list_row, encodeArray);
                attachment_list.setAdapter(adapter);
                notification_text.setText(String.valueOf(encodeArray.size()));
            }else {
                if(service_count < 1)
                {
                    new Get_Repair_MySubmit_details().execute();

                }else
                {
                    adapter = new UserListAdapter(Attach_Repair_Completion_MySubmit.this, R.layout.image_list_row, encodeArray);
                    attachment_list.setAdapter(adapter);
                    notification_text.setText(String.valueOf(encodeArray.size()));
                }

            }
        }catch ( Exception e)
        {
            new Get_Repair_MySubmit_details().execute();
            e.printStackTrace();
        }

//        new Get_Repair_MySubmit_details().execute();

    }





    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.heat_home:
                startActivity(new Intent(getApplicationContext(), MainActivity.class));
                break;

            case R.id.iv_back:
                onBackPressed();
                break;
            case R.id.repair_estimate:
                finish();
                startActivity(new Intent(getApplicationContext(),Repair_MainActivity.class));
                break;
            case R.id.more_info:

                popUp_equipment_info(GlobalConstants.equipment_no,GlobalConstants.status,GlobalConstants.status_id,GlobalConstants.previous_cargo,"","","","");


                break;
            case R.id.im_capture:
                final CharSequence[] items = { "Take Photo", "Choose from Library"};

                isCamPermission = sp2.getBoolean(SP2_CAMERA_PERM_DENIED, false);
                AlertDialog.Builder builder = new AlertDialog.Builder(Attach_Repair_Completion_MySubmit.this);
                builder.setTitle("Add Photo!");
                builder.setItems(items, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int item) {
                        boolean result= Utility.checkPermission(Attach_Repair_Completion_MySubmit.this, isCamPermission);

                        if (items[item].equals("Take Photo")) {
                            userChoosenTask ="Take Photo";
                            if(result) {
                                try {
                                    Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
                                    startActivityForResult(intent, REQUEST_CAMERA);
                                }catch (Exception e)
                                {
                                    shortToast(getApplicationContext(),"Camera Permission Required");
                                }

                            }

                        } else if (items[item].equals("Choose from Library")) {
                            userChoosenTask ="Choose from Library";
                            if(result)
                                startActivityForResult(new Intent(Attach_Repair_Completion_MySubmit.this, CustomGallery_Activity.class), CustomGallerySelectId);

                        }
                    }
                });
                builder.show();
                break;

            case R.id.heat_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.heat_submit:

                    if(GlobalConstants.completion_date.equals("")|| GlobalConstants.time.equals(""))
                    {
                        shortToast(getApplicationContext(),"Please key-in Mandate Fields");
                    }else
                    {
                        new PostInfo().execute();
                    }




                break;
            case R.id.iv_send_mail:

             /*   GlobalConstants.print_string= String.valueOf(print_object);
                startActivity(new Intent(getApplicationContext(),SendMailActivity.class));*/
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

                break;
            case R.id.Line_iteam:

                finish();
                if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {
                    GlobalConstants.from="MysubmitAttach";
                }else
                {
                    GlobalConstants.from="Attach";
                }
                GlobalConstants.summaryfrom="summaryfrom";
                GlobalConstants.multiple_encodeArray=encodeArray;
                String ed_actual_MH_time=GlobalConstants.actual_mh;
                GlobalConstants.actual_mh=ed_actual_MH_time;
                String ed_MH_date=GlobalConstants.estimated_manHr;
                GlobalConstants.estimated_manHr=ed_MH_date;
                String ed_completion_date=GlobalConstants.completion_date;
                GlobalConstants.completion_date=ed_completion_date;
                GlobalConstants.attach_count= String.valueOf(encodeArray.size());
                String ed_completion_time=GlobalConstants.time;
                GlobalConstants.time=ed_completion_time;
                String remark=GlobalConstants.remark;
                GlobalConstants.remark=remark;
                String ed_temp=GlobalConstants.location;
                GlobalConstants.location=ed_temp;
                startActivity(new Intent(getApplicationContext(),Repair_Completions_wizard_MySubmit.class));

                break;
            case R.id.add_details:
                finish();
                if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {
                    GlobalConstants.from="MysubmitAttach";
                }else
                {
                    GlobalConstants.from="Attach";
                }
                GlobalConstants.summaryfrom="summaryfrom";
                 ed_actual_MH_time=GlobalConstants.actual_mh;
                GlobalConstants.actual_mh=ed_actual_MH_time;
                 ed_MH_date=GlobalConstants.estimated_manHr;
                GlobalConstants.estimated_manHr=ed_MH_date;
                 ed_completion_date=GlobalConstants.completion_date;
                GlobalConstants.completion_date=ed_completion_date;
                 ed_completion_time=GlobalConstants.time;
                GlobalConstants.time=ed_completion_time;
                 remark=GlobalConstants.remark;
                GlobalConstants.remark=remark;
                 ed_temp=GlobalConstants.location;
                GlobalConstants.attach_count= String.valueOf(encodeArray.size());
                GlobalConstants.multiple_encodeArray=encodeArray;

                GlobalConstants.location=ed_temp;
                startActivity(new Intent(getApplicationContext(),AddRepair_Completion_Details_MySubmit.class));

                break;


            case R.id.iv_changeOfStatus:
                GlobalConstants.multiple_encodeArray=encodeArray;
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));

                break;

        }

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
    private void onCaptureImageResult(Intent data) {

        Bitmap thumbnail = (Bitmap) data.getExtras().get("data");

        String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss", Locale.getDefault())
                .format(new Date());

        File cacheDir = new File(android.os.Environment.getExternalStorageDirectory(), "happihive.com");
        boolean dirCreate = cacheDir.exists();
        if (!dirCreate) {
            dirCreate = cacheDir.mkdirs();
        }

        selectedImage = new File(cacheDir,timeStamp + ".jpg");
        filePath = cacheDir+"/"+timeStamp+".jpg";

        FileOutputStream fos = null;
        if(thumbnail != null) {
            try {
                fos = new FileOutputStream(selectedImage);
                thumbnail.compress(Bitmap.CompressFormat.JPEG, 100, fos);
                fos.flush();
                fos.close();
                MediaStore.Images.Media.insertImage(getApplicationContext().getContentResolver(),
                        thumbnail, selectedImage.getPath(), timeStamp + ".jpg");
                ByteArrayOutputStream bytes = new ByteArrayOutputStream();
                thumbnail.compress(Bitmap.CompressFormat.JPEG, 90, bytes);

                byte[] byteArrayImage = bytes.toByteArray();
                encodedImage = Base64.encodeToString(byteArrayImage, Base64.DEFAULT);
                String imageName=filePath.substring(filePath.lastIndexOf("/")+1);
                extansion=filePath.substring(filePath.lastIndexOf(".") + 1);
                Filename=imageName;
                Log.i("file Path --->",filePath);
                Log.i("filemanagerstring--->",Filename);
                Log.i("file extansion --->",extansion);
                image_bean=new Image_Bean();
                image_bean.setBase64(encodedImage);
                image_bean.setImage_length(String.valueOf(encodedImage.length()));
                image_bean.setImage_Name(Filename);
                image_bean.setUriPath("");
                image_bean.setAttachment_Id("");
                image_bean.setImage(Filename);
                encodeArray.add(image_bean);
                GlobalConstants.multiple_encodeArray=encodeArray;
                GlobalConstants.Attachment_status="True";

                notification_text.setText(String.valueOf(encodeArray.size()));
                GlobalConstants.attach_count= String.valueOf(encodeArray.size());
                adapter = new UserListAdapter(Attach_Repair_Completion_MySubmit.this, R.layout.image_list_row, encodeArray);
                attachment_list.setAdapter(adapter);
            }catch (Exception e) {
                e.printStackTrace();
            }
        }


    }


    private void loadGridView(ArrayList<String> imagesArray,Intent imagereturnintent) {

        items = new ArrayList<Item>();

        for (int i=0;i<imagesArray.size();i++){
//            imagesPathList.add(imagesArray(i));
            selectedImageBitmap = BitmapFactory.decodeFile(imagesArray.get(i));
            image_bean=new Image_Bean();
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            selectedImageBitmap.compress(Bitmap.CompressFormat.JPEG, 100, byteArrayOutputStream);
            byte[] byteArrayImage = byteArrayOutputStream.toByteArray();
            try {
                String path = imagesArray.get(i);
                encodedImage = Base64.encodeToString(byteArrayImage, Base64.DEFAULT);
                image_bean.setBase64(encodedImage);
                image_bean.setImage_length(String.valueOf(encodedImage.length()));
                image_bean.setImage_Name(path.substring(path.lastIndexOf("/") + 1));
                image_bean.setUriPath("");
                image_bean.setAttachment_Id("");
                image_bean.setImage("image_comp"+imagesArray.get(i));
                encodeArray.add(image_bean);

            }catch (Exception e)
            {
                shortToast(getApplicationContext(),"Image size Should be < 20 MB");
            }


            final Uri data = imagereturnintent.getData();
            items.add(new Item("image_"+imagesArray.get(i),encodedImage));
            GlobalConstants.Attachment_status="True";
            GlobalConstants.attach_count= String.valueOf(encodeArray.size());
        }

        invite_jsonlist = new JSONArray();
        reqObj = new JSONObject();
        d_reqObj = new JSONObject();
        try {
            for (int i = 0; i < encodeArray.size(); i++) {

                invitejsonObject = new JSONObject();
                invitejsonObject.put("FileName", encodeArray.get(i).getImage_Name());
                invitejsonObject.put("ContentLength", encodeArray.get(i).getImage().length());
                invitejsonObject.put("base64imageString", encodeArray.get(i).getBase64());
                invite_jsonlist.put(invitejsonObject);
            }
            reqObj.put("ArrayOfFileParams", invite_jsonlist);
            d_reqObj.put("d",reqObj);
        }catch (Exception e)
        {

        }
        SharedPreferences.Editor editor = sp.edit();
        editor.putString(SP_Multiple_Image_Json, String.valueOf(reqObj));
        editor.commit();
        GlobalConstants.multiple_encodeArray=encodeArray;

        notification_text.setText(String.valueOf(encodeArray.size()));

        adapter = new UserListAdapter(Attach_Repair_Completion_MySubmit.this, R.layout.image_list_row, encodeArray);
        attachment_list.setAdapter(adapter);

    }


    @Override
    public void onResume(){
        super.onResume();
        // put your code here...
        if(encodeArray==null)
        {
            encodeArray=new ArrayList<Image_Bean>();

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
public class UserListAdapter extends BaseAdapter {

    private final ArrayList<Image_Bean> arraylist;
    private ArrayList<Image_Bean> list;
    Context context;

    int resource;
    private Image_Bean userListBean;
    int lastPosition = -1;
    public UserListAdapter(Context context, int resource, ArrayList<Image_Bean> list) {
        this.context = context;
        this.list = list;
        this.resource = resource;
        this.arraylist = new ArrayList<Image_Bean>();
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
        if (list.size() < 1){
            Toast.makeText(getApplicationContext(), "NO DATA FOUND", Toast.LENGTH_LONG).show();
        }else {

            userListBean = list.get(position);


            final String fileName_from = userListBean.getImage_Name().substring(userListBean.getImage_Name().lastIndexOf('/') + 1);
            holder.Cust_Name.setText(fileName_from);
            holder.attachment_id.setText(userListBean.getAttachment_Id());
            holder.image_position.setText(String.valueOf(position));
            holder.Cust_Name.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    if(list.get(position).getUriPath().equals("")) {
                        String base64Content = list.get(position).getBase64();
                        byte[] bytes = Base64.decode(base64Content, Base64.DEFAULT);
                         bitmap = BitmapFactory.decodeByteArray(bytes, 0, bytes.length);
                         popImageUp(bitmap,list.get(position).getUriPath());
//                        Log.i("image_url",list.get(position).getUriPath());

                    }else{
//                        Log.i("image_url",list.get(position).getImage());
//                        Log.i("image_url-1",list.get(position).getUriPath());
                        popImageUp(bitmap,list.get(position).getImage());

                    }
                }
            });

            holder.im_delete.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    GlobalConstants.attach_ID=list.get(position).getAttachment_Id();
                    GlobalConstants.position= String.valueOf(list.get(position));
                    Log.i("getAttachment_Id()",list.get(position).getAttachment_Id());
                        if(list.get(position).getAttachment_Id().equals(""))
                        {
                            GlobalConstants.from="Repairpending_delete";
                            GlobalConstants.image_name= fileName_from;

//                                image_arraylist=new ArrayList<Image_Bean>();


                            list.remove(list.get(position));

                            notification_text.setText(String.valueOf(list.size()));

                            GlobalConstants.multiple_encodeArray=list;

                            finish();
                            startActivity(getIntent());

                        }else
                        {
                            new Delete_Attachment().execute();

                        }

                }
            });


        }
        return convertView;
    }



}
    static class ViewHolder {
        TextView equip_no,time, Cust_Name,previous_crg,attachmentstatus,gateIn_Id,code,location,pre_id,pre_code,cust_code,type_id,code_id,
                vechicle,transport,attachID,filename,Eir_no,heating_bt,rental_bt,remark,status,pre_adv_id,type;
        CheckBox checkBox;

        LinearLayout whole,LL_username;
        public TextView attachment_id;
        public TextView image_position;
        public ImageView im_delete;
    }
    public class Get_Repair_MySubmit_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray LineItems_Json;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Attach_Repair_Completion_MySubmit.this);
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

                       /* if(GlobalConstants.from.equalsIgnoreCase("delete")||GlobalConstants.from.equalsIgnoreCase("new_delete"))
                        {
                            encodeArray=new ArrayList<Image_Bean>() ;
                        }*/
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


/*
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
*/

//                                        image_arraylist.add(image_bean);
                                    encodeArray.add(image_bean);
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


            if ((progressDialog != null) && progressDialog.isShowing()) {
                progressDialog.dismiss();
            }
            if(encodeArray!=null)
            {
                service_count++;
                GlobalConstants.attach_count= String.valueOf(encodeArray.size());
                GlobalConstants.multiple_encodeArray=encodeArray;
                notification_text.setText(String.valueOf(encodeArray.size()));
                invite_jsonlist = new JSONArray();
                reqObj = new JSONObject();
                try {
                    for (int i = 0; i < encodeArray.size(); i++) {

                        invitejsonObject = new JSONObject();
                        invitejsonObject.put("FileName", encodeArray.get(i).getImage_Name());
                        invitejsonObject.put("ContentLength", encodeArray.get(i).getImage().length());
                        invitejsonObject.put("base64imageString", encodeArray.get(i).getBase64());
                        invite_jsonlist.put(invitejsonObject);
                    }
                    reqObj.put("ArrayOfFileParams", invite_jsonlist);
                    d_reqObj.put("d",reqObj);
                }catch (Exception e)
                {

                }

                SharedPreferences.Editor editor = sp.edit();
                editor.putString(SP_Multiple_Image_Json, String.valueOf(d_reqObj));
                editor.commit();
                adapter = new UserListAdapter(Attach_Repair_Completion_MySubmit.this, R.layout.image_list_row, encodeArray);
                attachment_list.setAdapter(adapter);

            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");


            }
        }

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
            progressDialog = new ProgressDialog(Attach_Repair_Completion_MySubmit.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLRepairCompletion_Update);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();

                image_jsonlist = new JSONArray();
                reqObj = new JSONObject();

                try {




                        for(int i=0;i<encodeArray.size();i++) {

                            imagejsonObject = new JSONObject();

                            if(encodeArray.get(i).getAttachment_Id().equals("")) {

                                imagejsonObject.put("FileName", encodeArray.get(i).getImage_Name());
                                imagejsonObject.put("ContentLength", encodeArray.get(i).getImage().length());
                                imagejsonObject.put("base64imageString", encodeArray.get(i).getBase64());

                            }else
                            {
                                if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {
                                    imagejsonObject.put("FileName", encodeArray.get(i).getImage_Name());
                                    imagejsonObject.put("base64imageString", encodeArray.get(i).getImage());
                                }else
                                {
                                    imagejsonObject.put("FileName", encodeArray.get(i).getImage_Name());
                                    imagejsonObject.put("base64imageString", encodeArray.get(i).getUriPath());
                                }
                            }
                            image_jsonlist.put(imagejsonObject);

                        }












                    reqObj.put("ArrayOfFileParams", image_jsonlist);



                }catch (Exception e)
                {

                    Log.i("Exception ", String.valueOf(e));
                }




                try {
                    if (encodeArray.size() < 0) {

                    } else {
                        IfAttchment = "True";
                    }
                } catch (Exception e) {

                    IfAttchment = "False";
                }



                try {

                    if(GlobalConstants.completion_date.equals(null)||GlobalConstants.completion_date.length()<0)
                    {

                        GlobalConstants.completion_date="";
                    }else
                    {


                    }}catch (Exception e)
                {
                    GlobalConstants.completion_date="";
                }

                if(GlobalConstants.actual_mh.equals(""))
                {
                    GlobalConstants.actual_mh="0";
                }

                if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        ||GlobalConstants.from.equalsIgnoreCase("delete")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {
                   /*{"Username":"","Mode":"","EquipmentNo":"","Attchment":"",
                   "YardLocation":"","ActualManHours":"","CompletedDate":"",
                   "CompletedTime":"","Remarks":""}*/
                    jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                    jsonObject.put("Mode", "edit");
                    jsonObject.put("YardLocation", GlobalConstants.location);
                    jsonObject.put("ActualManHours",GlobalConstants.actual_mh );
                    jsonObject.put("CompletedDate", GlobalConstants.completion_date);
                    jsonObject.put("CompletedTime", GlobalConstants.time);
                    jsonObject.put("Remarks", GlobalConstants.remark);
                    jsonObject.put("EquipmentNo", GlobalConstants.equipment_no);
                    jsonObject.put("Attchment",IfAttchment);


                }else
                {
                    jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                    jsonObject.put("Mode", "new");
                    jsonObject.put("YardLocation", GlobalConstants.location);
                    jsonObject.put("ActualManHours", GlobalConstants.actual_mh);
                    jsonObject.put("CompletedDate", GlobalConstants.completion_date);
                    jsonObject.put("CompletedTime", GlobalConstants.time);
                    jsonObject.put("Remarks", GlobalConstants.remark);
                    jsonObject.put("EquipmentNo", GlobalConstants.equipment_no);
                    jsonObject.put("Attchment",IfAttchment);

                }

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

                String message = returnMessage.getString("Status");
                responseString=message;
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
                if (responseString.equalsIgnoreCase("Success") || responseString.equalsIgnoreCase("Equipment Updated Successfully")) {

                    if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                            ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                            ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                            ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {

                        Toast.makeText(getApplicationContext(), "RepairCompletion Updated Successfully.", Toast.LENGTH_SHORT).show();
                        finish();
                        Intent i = new Intent(getApplication(), RepairCompletionMySubmit.class);
                        startActivity(i);
                    }else
                    {
                        Toast.makeText(getApplicationContext(), "RepairCompletion Created Successfully.", Toast.LENGTH_SHORT).show();
                        finish();
                        Intent i = new Intent(getApplication(), RepairCompletionPending.class);
                        startActivity(i);
                    }


                } else {
                    Toast.makeText(getApplicationContext(), "Repair Failed", Toast.LENGTH_SHORT).show();

                }
            }else
            {
                Toast.makeText(getApplicationContext(), "Repair Created Failed..!", Toast.LENGTH_SHORT).show();

            }
            progressDialog.dismiss();

        }
    }
    public class Delete_Attachment extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;
        private JSONArray LineItems_Json;
        private String response_status;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Attach_Repair_Completion_MySubmit.this);
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

            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("bv_strAttachmentId",GlobalConstants.attach_ID);


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                Log.d("req", jsonObject.toString());
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


//                jsonarray = getJsonObject.getJSONArray("Response");
                response_status = getJsonObject.getString("Status");


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

            if(response_status.equalsIgnoreCase("Success"))
            {
                shortToast(getApplicationContext(),"Attachment Deleted Succesfully");

                for (int i=0;i<encodeArray.size();i++)
                {
                    if(encodeArray.get(i).getAttachment_Id().equalsIgnoreCase(GlobalConstants.attach_ID))
                    {
                        encodeArray.remove(i);
                    }

                }
                notification_text.setText(String.valueOf(encodeArray.size()));
                adapter = new UserListAdapter(Attach_Repair_Completion_MySubmit.this, R.layout.image_list_row, encodeArray);
                attachment_list.setAdapter(adapter);
              /*  if(GlobalConstants.from.equalsIgnoreCase("MySubmitRepair")|| GlobalConstants.from.equalsIgnoreCase("MySubmitRepair_Summary")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAttach")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitAddDetails")
                        ||GlobalConstants.from.equalsIgnoreCase("delete")
                        ||GlobalConstants.from.equalsIgnoreCase("MysubmitlineItem")) {
                    GlobalConstants.from = "delete";
                }else
                {
                    GlobalConstants.from = "new_delete";
                }
                new Get_Repair_MySubmit_details().execute();
*/

            }

        }

    }

}
