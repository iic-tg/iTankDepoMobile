package com.i_tankdepo;

import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.NavigationView;
import android.support.v4.content.ContextCompat;
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
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.Equipment_HistoryBean;
import com.i_tankdepo.Beanclass.PendingBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.helper.ServiceHandler;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.entity.mime.content.StringBody;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpParams;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Locale;

import static com.i_tankdepo.Constants.GlobalConstants.code;
import static com.i_tankdepo.Constants.GlobalConstants.equipment_no;
import static com.i_tankdepo.Constants.GlobalConstants.gi_trans_no;
import static com.i_tankdepo.Constants.GlobalConstants.status;


/**
 * Created by Admin on 12/21/2016.
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
public class EquipmentHistory extends CommonActivity implements NavigationView.OnNavigationItemSelectedListener{

    private Toolbar toolbar;

    private DrawerLayout drawer;
    private ImageView iv_back,menu,im_searchview;
    private Button equip_home,equip_refresh;
    private TextView tv_toolbarTitle,tv_Cust_Name,tv_equip_no,tv_previous_crg,tv_eir_no;
    private EditText ed_searchview;
    private LinearLayout LL_Equipment_Info;
    private Intent mServiceIntent;
    private ProgressDialog progressDialog;
    private ArrayList<Equipment_HistoryBean> equip_history_arraylist = new ArrayList<>();
    private Equipment_HistoryBean equipment_Historybean;
    private UserListAdapter adapter;
    private ListView equip_listview;
    private ViewHolder holder;
    private String getEquipNo,getGITransNO,getTrackingID,getActivityName,getRemarks,getStatusCD;
    private String CustomerName,EquipmentNo,code,Type,previousCargo,eir_no;
    private String validation,deleteActivity;
    private ImageView delete;
    private String get_remark;
    private ImageView iv_changeOfStatus;
    private String CRTD_DT,ACTVTY_DT;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.equipment_history);

        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setVisibility(View.GONE);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Equipment History");
        ed_searchview = (EditText)findViewById(R.id.ed_searchview);
        im_searchview = (ImageView)findViewById(R.id.im_searchview);
        LL_Equipment_Info = (LinearLayout)findViewById(R.id.LL_Equipment);
        LL_Equipment_Info.setVisibility(View.GONE);
        equip_listview = (ListView)findViewById(R.id.equip_listview);


        tv_Cust_Name = (TextView) findViewById(R.id.text1);
        tv_equip_no = (TextView) findViewById(R.id.text2);
        tv_previous_crg = (TextView) findViewById(R.id.text3);
        tv_eir_no = (TextView) findViewById(R.id.text4);
        equip_home = (Button)findViewById(R.id.equip_home);
        equip_refresh = (Button)findViewById(R.id.equip_refresh);
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);

        equip_home.setOnClickListener(this);
        equip_refresh.setOnClickListener(this);
        im_searchview.setOnClickListener(this);



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
                    drawer.openDrawer(
                            Gravity.START);
            }
        });

        navigationView.setNavigationItemSelectedListener(this);

        ed_searchview.addTextChangedListener(new TextWatcher() {

            @Override
            public void afterTextChanged(Editable arg0) {
                // TODO Auto-generated method stub
                String text = ed_searchview.getText().toString().toLowerCase(Locale.getDefault());
                if(text.length()!=0)
                {
                    im_searchview.setColorFilter(ContextCompat.getColor(getApplicationContext(), R.color.black));

                }else{
                    im_searchview.setColorFilter(ContextCompat.getColor(getApplicationContext(), R.color.grey));
                }

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



    @Override
    public void onClick(View view) {
        switch (view.getId()){
            case R.id.iv_changeOfStatus:
                GlobalConstants.equipment_no="";
                GlobalConstants.status="";
                GlobalConstants.status_id="";
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));
                break;
            case R.id.equip_home:
                startActivity(new Intent(getApplicationContext(),MainActivity.class));
                break;
            case R.id.equip_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.im_searchview:
                getEquipNo = ed_searchview.getText().toString();

                if(getEquipNo == "" || getEquipNo.length()<11){

                    shortToast(getApplicationContext(),"Please enter the Valid Equipment Number..!");

                }else {
                    String Letter = getEquipNo.substring(0,4);
                    String Number = getEquipNo.substring(4,11);
                    if(Character.isLetter(Letter.charAt(0)) && Character.isDigit(Number.charAt(4))){
                        Log.d("Character",Letter);
                        Log.d("Numbers",Number);
                        if (cd.isConnectingToInternet()) {
                            new Get_Equipment_History_details().execute();
                        } else {
                            shortToast(getApplicationContext(), "Please check your Internet Connection..!");
                        }

                    }else{
                        shortToast(getApplicationContext(),"This Equipment Number is Not Valid..!");
                    }

                }
                break;


        }
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

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

    public class Get_Equipment_History_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;


        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(EquipmentHistory.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLEquipmentHistory);
            httpPost.setHeader("Content-Type", "application/json");

            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("equipmentNo",getEquipNo);

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("List");

                if(jsonarray.length()<=0)
                {
                    runOnUiThread(new Runnable(){

                        @Override
                        public void run(){
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(), "Please enter the Valid Equipment Number");
                            LL_Equipment_Info.setVisibility(View.GONE);


                        }
                    });

                } else {

                        equip_history_arraylist = new ArrayList<>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            equipment_Historybean = new Equipment_HistoryBean();
                            jsonObject = jsonarray.getJSONObject(i);



                             CustomerName = jsonObject.getString("CSTMR_CD");
                             EquipmentNo = jsonObject.getString("EQPMNT_NO");
                             Type = jsonObject.getString("EQPMNT_TYP_CD");
                             code = jsonObject.getString("CSTMR_ID");
                             previousCargo = jsonObject.getString("PRDCT_DSCRPTN_VC");
                             eir_no = jsonObject.getString("EIR_NO");

                            equipment_Historybean.setTracking_id(jsonObject.getString("TRCKNG_ID"));
                            equipment_Historybean.setCSTMR_ID(jsonObject.getString("CSTMR_ID"));
                            equipment_Historybean.setCSTMR_CD(jsonObject.getString("CSTMR_CD"));
                            equipment_Historybean.setEQPMNT_NO(jsonObject.getString("EQPMNT_NO"));
                            equipment_Historybean.setACTVTY_NAM(jsonObject.getString("ACTVTY_NAM"));
                            equipment_Historybean.setEQPMNT_STTS_ID(jsonObject.getString("EQPMNT_STTS_ID"));
                            equipment_Historybean.setEQPMNT_STTS_CD(jsonObject.getString("EQPMNT_STTS_CD"));
                            equipment_Historybean.setACTVTY_NO(jsonObject.getString("ACTVTY_NO"));

                            SimpleDateFormat fromUser = new SimpleDateFormat("MM-dd-yyyy", Locale.ENGLISH);
                            SimpleDateFormat myFormat = new SimpleDateFormat("dd-MM-yyyy",Locale.ENGLISH);

                            ACTVTY_DT=jsonObject.getString("ACTVTY_DT");
                            CRTD_DT=jsonObject.getString("CRTD_DT");
                            String[] In_date=ACTVTY_DT.split(" ");
                            String[] split_LastStatusDate=CRTD_DT.split(" ");
                            ACTVTY_DT=In_date[0];
                            CRTD_DT=split_LastStatusDate[0];
                            try {
                                if (ACTVTY_DT.equals(null) || ACTVTY_DT.length() < 0) {

                                    ACTVTY_DT = "";
                                } else {

                                    ACTVTY_DT = myFormat.format(fromUser.parse(ACTVTY_DT));




                                } if (CRTD_DT.equals(null) || CRTD_DT.length() < 0) {

                                    CRTD_DT = "";
                                } else {

                                    CRTD_DT = myFormat.format(fromUser.parse(CRTD_DT));




                                }

                            }catch (Exception e)
                            {

                            }

                            equipment_Historybean.setCRTD_DT(CRTD_DT);

                            equipment_Historybean.setACTVTY_DT(ACTVTY_DT);
                            equipment_Historybean.setACTVTY_RMRKS(jsonObject.getString("ACTVTY_RMRKS"));
                            equipment_Historybean.setGI_TRNSCTN_NO(jsonObject.getString("GI_TRNSCTN_NO"));
                            equipment_Historybean.setINVCNG_PRTY_ID(jsonObject.getString("INVCNG_PRTY_ID"));
                            equipment_Historybean.setINVCNG_PRTY_CD(jsonObject.getString("INVCNG_PRTY_CD"));
                            equipment_Historybean.setPRDCT_ID(jsonObject.getString("PRDCT_ID"));
                            equipment_Historybean.setEIR_NO(jsonObject.getString("EIR_NO"));
                            equipment_Historybean.setRF_NO(jsonObject.getString("RF_NO"));
                            equipment_Historybean.setPRDCT_DSCRPTN_VC(jsonObject.getString("PRDCT_DSCRPTN_VC"));
                            equipment_Historybean.setINVC_GNRTN(jsonObject.getString("INVC_GNRTN"));
                            equipment_Historybean.setCRTD_BY(jsonObject.getString("CRTD_BY"));
                            equipment_Historybean.setCNCLD_BY(jsonObject.getString("CNCLD_BY"));
                            equipment_Historybean.setCNCLD_DT(jsonObject.getString("CNCLD_DT"));
                            equipment_Historybean.setADT_RMRKS(jsonObject.getString("ADT_RMRKS"));
                            equipment_Historybean.setDPT_ID(jsonObject.getString("DPT_ID"));
                            equipment_Historybean.setYRD_LCTN(jsonObject.getString("YRD_LCTN"));
                            equipment_Historybean.setEQPMNT_TYP_ID(jsonObject.getString("EQPMNT_TYP_ID"));
                            equipment_Historybean.setEQPMNT_TYP_CD(jsonObject.getString("EQPMNT_TYP_CD"));
                            equipment_Historybean.setEQPMNT_CD_ID(jsonObject.getString("EQPMNT_CD_ID"));
                            equipment_Historybean.setEQPMNT_CD_CD(jsonObject.getString("EQPMNT_CD_CD"));
                            equipment_Historybean.setRMRKS_VC(jsonObject.getString("RMRKS_VC"));
                            equipment_Historybean.setPRDCT_CD(jsonObject.getString("PRDCT_CD"));
                            equipment_Historybean.setRNTL_CSTMR_ID(jsonObject.getString("RNTL_CSTMR_ID"));
                            equipment_Historybean.setRNTL_RFRNC_NO(jsonObject.getString("RNTL_RFRNC_NO"));
                            equipment_Historybean.setAGNT_CD(jsonObject.getString("AGNT_CD"));
                            equipment_Historybean.setSTTS_ID(jsonObject.getString("STTS_ID"));
                            equipment_Historybean.setCSTMR_NAM(jsonObject.getString("CSTMR_NAM"));
                            equipment_Historybean.setADDTNL_CLNNG_BT(jsonObject.getString("ADDTNL_CLNNG_BT"));
                            equipment_Historybean.setDPT_CD(jsonObject.getString("DPT_CD"));

                            equip_history_arraylist.add(equipment_Historybean);





                    }
                }
                /*else {
                    runOnUiThread(new Runnable(){

                        @Override
                        public void run(){
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(),"No Records Found");
                            LL_Equipment_Info.setVisibility(View.VISIBLE);


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


//            if ((progressDialog != null) && progressDialog.isShowing()) {
            progressDialog.dismiss();
//            }

            if(jsonarray!=null) {
                if (equip_history_arraylist.size()>0)
                {
                    LL_Equipment_Info.setVisibility(View.VISIBLE);
                    tv_Cust_Name.setText(CustomerName);
                    tv_eir_no.setText(eir_no);
                    tv_equip_no.setText(EquipmentNo + "," + Type + "," + code);
                    tv_previous_crg.setText(previousCargo);
                    adapter = new UserListAdapter(EquipmentHistory.this, R.layout.history_list_item_row, equip_history_arraylist);
                    equip_listview.setAdapter(adapter);
                }else {
                    shortToast(getApplicationContext(), "Please enter the Valid Equipment Number");
                }
            }else
            {
                shortToast(getApplicationContext(), "Please enter the Valid Equipment Number");

            }

        }

    }

    public class UserListAdapter extends BaseAdapter {

        private ArrayList<Equipment_HistoryBean> list;
        Context context;

        int resource;
        private Equipment_HistoryBean userListBean;
        int lastPosition = -1;
        public UserListAdapter(Context context, int resource, ArrayList<Equipment_HistoryBean> list) {
            this.context = context;
            this.list = list;
            this.resource = resource;

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
                holder = new EquipmentHistory.ViewHolder();


                holder.whole = (LinearLayout) convertView.findViewById(R.id.LL_whole);

                holder.tv_activityName = (TextView) convertView.findViewById(R.id.tv_activityName);
                holder.tv_activity = (TextView) convertView.findViewById(R.id.tv_activity);
                holder.tv_RefNo = (TextView) convertView.findViewById(R.id.tv_RefNo);
                holder.tv_modifiedBy = (TextView) convertView.findViewById(R.id.tv_modifiedBy);
                holder.tv_Invoice_Party = (TextView) convertView.findViewById(R.id.tv_Invoice_Party);
                holder.tv_modifiedDate = (TextView) convertView.findViewById(R.id.tv_modifiedDate);
                holder.tv_remark = (TextView) convertView.findViewById(R.id.tv_remark);
                holder.Gi_trans_no = (TextView) convertView.findViewById(R.id.tv_code);
                holder.Tracking_ID = (TextView) convertView.findViewById(R.id.tv_location);
                holder.Billing_Status = (TextView) convertView.findViewById(R.id.tv_billing_status);

                holder.delete_grey = (ImageView)convertView.findViewById(R.id.im_delete_grey);
                holder.delete_red= (ImageView)convertView.findViewById(R.id.im_delete_red);



                convertView.setTag(holder);
            } else {
                holder = (EquipmentHistory.ViewHolder) convertView.getTag();
            }
            if (list.size() < 1){
                Toast.makeText(getApplicationContext(), "NO DATA FOUND", Toast.LENGTH_LONG).show();
            }else {

                userListBean = list.get(position);

                String[] parts = userListBean.getACTVTY_DT().split(" ");

                holder.tv_activityName.setText(userListBean.getACTVTY_NAM());

                if(userListBean.getEQPMNT_STTS_CD().equals(""))
                {
                    holder.tv_activity.setText(userListBean.getACTVTY_DT());

                }else
                {
                    holder.tv_activity.setText(userListBean.getEQPMNT_STTS_CD() + "," + userListBean.getACTVTY_DT());

                }

                holder.tv_RefNo.setText(userListBean.getRF_NO());

                holder.tv_modifiedBy.setText(userListBean.getCRTD_BY());
                holder.tv_Invoice_Party.setText(userListBean.getINVCNG_PRTY_CD());
                holder.tv_modifiedDate.setText(userListBean.getCRTD_DT());
                holder.tv_remark.setText(userListBean.getRMRKS_VC());

                holder.Gi_trans_no.setText(userListBean.getGI_TRNSCTN_NO());
                holder.Tracking_ID.setText(userListBean.getTracking_id());
                holder.Billing_Status.setText(userListBean.getINVC_GNRTN());

                for(int i=0;i<list.size();i++){
                    if(position==0) {
                        holder.delete_red.setVisibility(View.VISIBLE);
                        holder.delete_grey.setVisibility(View.GONE);
                    }else{
                        holder.delete_red.setVisibility(View.GONE);
                        holder.delete_grey.setVisibility(View.VISIBLE);
                    }
                }

                holder.delete_red.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        getEquipNo = ed_searchview.getText().toString();
                        getGITransNO = list.get(position).getGI_TRNSCTN_NO();
                        getTrackingID = list.get(position).getTracking_id();
                        getActivityName = list.get(position).getACTVTY_NAM();
                        getStatusCD = list.get(position).getEQPMNT_STTS_CD();
                        if(cd.isConnectingToInternet()){
                            new Post_Validate_Delete().execute();
                        }else{
                            shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                        }
                    }
                });

            }
            return convertView;
        }




    }
    static class ViewHolder {
        TextView equip_no,time, Cust_Name,previous_crg,eir_no,tv_activityName,tv_activity,tv_RefNo,tv_modifiedBy,tv_Invoice_Party,tv_modifiedDate
                ,tv_remark,Gi_trans_no,Tracking_ID,Billing_Status;
        CheckBox checkBox;
        ImageView delete_red,delete_grey;

        LinearLayout whole,LL_username;
    }


    public class Post_Validate_Delete extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        JSONObject jsonobject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(EquipmentHistory.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
            //  progressDialog.show();
        }

        @Override
        protected Void doInBackground(Void... params) {

            DefaultHttpClient httpClient = new DefaultHttpClient();
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpClient httpclient = new DefaultHttpClient();
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLEquipmentValidation);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));
                jsonObject.put("bv_strEquipmentNO", getEquipNo  );
                jsonObject.put("bv_strGI_Trnsctn_NO", getGITransNO  );


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("responce", resp);
                Log.d("req_", jsonObject.toString());
                JSONObject jsonResp = new JSONObject(resp);

                jsonobject = jsonResp.getJSONObject("d");

                if (jsonobject != null) {

                    validation=jsonobject.getString("status");

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


            if (jsonobject != null) {
                if (validation.equals("Success")) {

                    if (cd.isConnectingToInternet()) {
                        dialog = new Dialog(EquipmentHistory.this);
                        dialog.setContentView(R.layout.delete_popup);
                        dialog.getWindow().setBackgroundDrawableResource(android.R.color.transparent);
                        Button btn_close = (Button) dialog.findViewById(R.id.cancel);
                        final EditText tv_forgot_edit = (EditText) dialog.findViewById(R.id.tv_forgot_edit);
                        final Button submit = (Button) dialog.findViewById(R.id.yes);
                        final Button Ok = (Button) dialog.findViewById(R.id.Ok);
                        Ok.setVisibility(View.GONE);
                        btn_close.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                finishActivity(1);
                                //  finish();
                                dialog.dismiss();
                            }
                        });
                        submit.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                Ok.setVisibility(View.VISIBLE);
                                submit.setVisibility(View.GONE);
                                tv_forgot_edit.setVisibility(View.VISIBLE);
                            }
                        });
                        Ok.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                get_remark = tv_forgot_edit.getText().toString();
                                if (get_remark.length() > 0) {
                                    new Post_Delete_Activity().execute();
                                    dialog.setCancelable(true);
                                    //  finishActivity(1);
                                    dialog.dismiss();
                                } else {
                                    shortToast(getApplicationContext(), "Please enter the Remarks..!");
                                }
                            }
                        });
                        dialog.show();

                   }else{
                       shortToast(getApplicationContext(),"Please Check your Internet Connection..!");
                   }
                }
            } else {
                shortToast(getApplicationContext(), "Data Not Found");
            }

            progressDialog.dismiss();

        }
    }

    public class Post_Delete_Activity extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        JSONObject jsonobject;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(EquipmentHistory.this);
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

            HttpPost httpPost = new HttpPost(ConstantValues.baseURLEquipmentDeleteActivity);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("bv_strTrackingID", getTrackingID );
                jsonObject.put("bv_strActivityName", getActivityName );
                jsonObject.put("bv_strRemarks", get_remark );
                jsonObject.put("equipmentNo", getEquipNo );
                jsonObject.put("UserName", sp.getString(SP_USER_ID,"user_Id"));



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

                    deleteActivity=jsonobject.getString("status");

                    Log.d("deleteActivity status", deleteActivity);


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
                if(deleteActivity.contains("deleted from Equipment History"))
                {
                    shortToast(getApplicationContext(),"Activity : "+ getStatusCD +" has been deleted from Equipment History");
                   if(cd.isConnectingToInternet()){
                       new Get_Equipment_History_details().execute();
                   }else{
                       shortToast(getApplicationContext(),"Please check your Internet Connection..!");
                   }
                }else if(deleteActivity.contains("Incorrect syntax"))
                {
                    shortToast(getApplicationContext(),"Activity could not be deleted..!");
                }
            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");
            }

            progressDialog.dismiss();

        }
    }


}
