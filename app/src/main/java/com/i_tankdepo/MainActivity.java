package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteStatement;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.appindexing.Action;
import com.google.android.gms.appindexing.AppIndex;
import com.google.android.gms.appindexing.Thing;
import com.google.android.gms.common.api.GoogleApiClient;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.SQLite.DBAdapter;
import com.i_tankdepo.Util.Utility;

import org.apache.http.HttpEntity;
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

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.List;


import com.microsoft.appcenter.AppCenter;
import com.microsoft.appcenter.analytics.Analytics;
import com.microsoft.appcenter.crashes.Crashes;


import static com.i_tankdepo.SQLite.DBAdapter.KEY_ROWID;
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
public class MainActivity extends CommonActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    public static boolean isCamPermission;
    Toolbar toolbar;
    private ImageView menu, up, down, iv_back, iv_changeOfStatus;
    private DrawerLayout drawer;
    private LinearLayout LL_Gatein, LL_Heating, Ll_Cleaning, LL_Leaktest, LL_Repair, LL_History, LL_change_status, LL_Stock_Report, LL_GateOut, LL_Inspection;
    LinearLayout LL_hole;
    private TextView tv_toolbarTitle;
    private Intent mServiceIntent;
    private JSONArray jsonArray;
    private ArrayList<String> list;
    private String activityName, RoleID;
    private ImageView im_gatein, im_gateout, im_heating, im_cleaning, im_Inspection, im_repair, im_leaktest, im_equipement, im_stock, home_changeStatus;
    private Button gateoutCount, gateInCount, heatingCount, cleaningCount, leaktestCount, repairCount, inspectionCount;
    private String gateIn, gateout, cleaning, heating, inspection, leaktest;
    private String GateinCount, GateOutCount, CleaningCount, InspectionCount, HeatingCount, Repair_Est,Repair_Appr,Repair_Com,Survey_Com,
            RepairCount,LeaktestCount, authorization;
    private DBAdapter db;
    private long Total_Count;
    private Cursor c;
    private int table_row_count, heatCount,leakCount,outCount;
    /**
     * ATTENTION: This was auto-generated to implement the App Indexing API.
     * See https://g.co/AppIndexing/AndroidStudio for more information.
     */
    private GoogleApiClient client;
    private int row_Count;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);


        isCamPermission = sp2.getBoolean(SP2_CAMERA_PERM_DENIED, false);
        if(checkWriteExternalPermission()!=true) {
            boolean result = Utility.checkPermission(MainActivity.this, isCamPermission);
        }

        AppCenter.start(getApplication(), "b17bd220-e2e9-47c0-97ec-1cf408d5263b", Analytics.class, Crashes.class);




        db = new DBAdapter(MainActivity.this);

        table_row_count = db.getGateInCount();
        Log.d("Gate In Count", String.valueOf(table_row_count));

        heatCount = db.getHeatingCount();
        Log.d("Heating Count", String.valueOf(heatCount));

        leakCount = db.getLeakTestCount();
        Log.d("LeakTest Count", String.valueOf(leakCount));

        outCount = db.getGateOutCount();
        Log.d("Gate Out Count", String.valueOf(outCount));

        if (table_row_count > 0) {
            db.open();

            c = db.getCreateGateIN();


            if (c.moveToFirst()) {
                do {

                    new PostInfo(c).execute();

                } while (c.moveToNext());
            }
            db.close();
        }

        if (heatCount > 0){
            db.open();
            c = db.getHeating();

            if(c.moveToFirst()){
                do{
                    new Post_Heating_Update(c).execute();
                }while (c.moveToNext());
            }
            db.close();
        }

        if (leakCount > 0) {
            db.open();
            c= db.getLeakTest();
            if (c.moveToFirst()){
                do{
                    new CreateLeakTest(c).execute();
                }while (c.moveToNext());
            }
            db.close();
        }

        if (outCount > 0){
            db.open();
            c = db.updateGateOut();
            if (c.moveToFirst()){
                do{
                    new PostInfo_GateOut(c).execute();
                }while (c.moveToNext());
            }
        db.close();
        }
        im_gatein = (ImageView) findViewById(R.id.im_gatein);
        im_gateout = (ImageView) findViewById(R.id.im_gateout);
        im_heating = (ImageView) findViewById(R.id.im_heating);
        im_cleaning = (ImageView) findViewById(R.id.im_cleaning);
        im_Inspection = (ImageView) findViewById(R.id.im_Inspection);
        im_repair = (ImageView) findViewById(R.id.im_repair);
        im_leaktest = (ImageView) findViewById(R.id.im_leaktest);
        im_equipement = (ImageView) findViewById(R.id.im_history);
        im_stock = (ImageView) findViewById(R.id.im_report);
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setVisibility(View.GONE);
        RoleID = GlobalConstants.roleID;
        home_changeStatus = (ImageView) findViewById(R.id.home_changeStatus);
        home_changeStatus.setOnClickListener(this);

        gateInCount = (Button) findViewById(R.id.bt_gateinCount);

        gateoutCount = (Button) findViewById(R.id.bt_gateoutCount);

        heatingCount = (Button) findViewById(R.id.bt_heatingCount);

        cleaningCount = (Button) findViewById(R.id.bt_cleaningCount);

        inspectionCount = (Button) findViewById(R.id.bt_inspectionCount);

        leaktestCount = (Button) findViewById(R.id.bt_leaktestCount);

        repairCount = (Button) findViewById(R.id.bt_repairCount);


        if (cd.isConnectingToInternet()) {
            new Post_Role_Based().execute();
        } else {
            shortToast(getApplicationContext(), "Please Check Your Internet Connection");
        }


        LL_Gatein = (LinearLayout) findViewById(R.id.LL_GateIn);
        LL_GateOut = (LinearLayout) findViewById(R.id.LL_GateOut);
        LL_Heating = (LinearLayout) findViewById(R.id.LL_Heating);
        Ll_Cleaning = (LinearLayout) findViewById(R.id.LL_Cleaning);
        LL_Inspection = (LinearLayout) findViewById(R.id.LL_Inspection);
        LL_Leaktest = (LinearLayout) findViewById(R.id.LL_Leaktest);
        LL_Repair = (LinearLayout) findViewById(R.id.LL_Repair);
        LL_History = (LinearLayout) findViewById(R.id.LL_History);
        LL_Stock_Report = (LinearLayout) findViewById(R.id.LL_Stock_Report);
        LL_change_status = (LinearLayout) findViewById(R.id.LL_change_status);

        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Home");


        LL_Gatein.setOnClickListener(this);
        LL_GateOut.setOnClickListener(this);
        LL_Heating.setOnClickListener(this);
        Ll_Cleaning.setOnClickListener(this);
        LL_Inspection.setOnClickListener(this);
        LL_Leaktest.setOnClickListener(this);
        LL_Repair.setOnClickListener(this);
        LL_History.setOnClickListener(this);
        LL_Stock_Report.setOnClickListener(this);
        LL_change_status.setOnClickListener(this);

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
        // ATTENTION: This was auto-generated to implement the App Indexing API.
        // See https://g.co/AppIndexing/AndroidStudio for more information.
//        client = new GoogleApiClient.Builder(this).addApi(AppIndex.API).build();
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
    public void onRestart()
    {
        super.onRestart();
        finish();
        startActivity(getIntent());
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

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        //  startActivity(new Intent(getApplicationContext(),MainActivity.class));
        int id = item.getItemId();

        if (id == R.id.nav_home) {


        } else if (id == R.id.nav_changePwd) {

            startActivity(new Intent(getApplicationContext(), Change_Password.class));

        } else if (id == R.id.nav_Logout) {

            if (mServiceIntent != null)
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

    @Override
    public void onClick(View view) {

        switch (view.getId()) {
            case R.id.home_changeStatus:
                GlobalConstants.equipment_no="";
                GlobalConstants.status="";
                startActivity(new Intent(getApplicationContext(), ChangeOfStatus.class));
                break;
            case R.id.LL_GateIn:
                startActivity(new Intent(getApplicationContext(), GateIn.class));
                break;
            case R.id.LL_GateOut:
                startActivity(new Intent(getApplicationContext(), GateOut.class));
                break;
            case R.id.LL_Heating:
                startActivity(new Intent(getApplicationContext(), Heating.class));
                break;
            case R.id.LL_Cleaning:
                startActivity(new Intent(getApplicationContext(), Cleaning.class));
                break;
            case R.id.LL_Inspection:
                startActivity(new Intent(getApplicationContext(), InspectionPending.class));
                break;
            case R.id.LL_Leaktest:
                startActivity(new Intent(getApplicationContext(), LeakTest.class));
                break;
            case R.id.LL_Repair:

                GlobalConstants.repair_est_count=Repair_Est;
                GlobalConstants.repair_app_count=Repair_Appr;
                GlobalConstants.repair_com_count=Repair_Com;
                GlobalConstants.survey_com_count=Survey_Com;
                startActivity(new Intent(getApplicationContext(),Repair_MainActivity.class));
                break;
            case R.id.LL_History:
                startActivity(new Intent(getApplicationContext(), EquipmentHistory.class));
                break;
            case R.id.LL_Stock_Report:
                startActivity(new Intent(getApplicationContext(), StockReport.class));
                break;
            case R.id.LL_change_status:
                GlobalConstants.equipment_no="";
                GlobalConstants.status="";
                startActivity(new Intent(getApplicationContext(), ChangeOfStatus.class));
                break;
        }
    }


    @Override
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

    }

    /**
     * ATTENTION: This was auto-generated to implement the App Indexing API.
     * See https://g.co/AppIndexing/AndroidStudio for more information.
     */
    public Action getIndexApiAction() {
        Thing object = new Thing.Builder()
                .setName("Main Page") // TODO: Define a title for the content shown.
                // TODO: Make sure this auto-generated URL is correct.
                .setUrl(Uri.parse("http://[ENTER-YOUR-URL-HERE]"))
                .build();
        return new Action.Builder(Action.TYPE_VIEW)
                .setObject(object)
                .setActionStatus(Action.STATUS_TYPE_COMPLETED)
                .build();
    }

  /*  @Override
    public void onStart() {
        super.onStart();

        // ATTENTION: This was auto-generated to implement the App Indexing API.
        // See https://g.co/AppIndexing/AndroidStudio for more information.
        client.connect();
        AppIndex.AppIndexApi.start(client, getIndexApiAction());
    }

    @Override
    public void onStop() {
        super.onStop();

        // ATTENTION: This was auto-generated to implement the App Indexing API.
        // See https://g.co/AppIndexing/AndroidStudio for more information.
        AppIndex.AppIndexApi.end(client, getIndexApiAction());
        client.disconnect();
    }
*/
    public class Post_Role_Based extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONObject jsonResp;
        private JSONArray jsonArrayforRole;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(MainActivity.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);

            if (! isFinishing()) {

                progressDialog.show();

            }
//            if ((progressDialog != null) && progressDialog.isShowing()) {
            progressDialog.show();
//            }
        }

        @Override
        protected Void doInBackground(Void... params) {

            DefaultHttpClient httpClient = new DefaultHttpClient();
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpClient httpclient = new DefaultHttpClient();

            HttpPost httpPost = new HttpPost(ConstantValues.baseURLRoleBasedLogin);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            httpPost.setHeader("Authorization", sp.getString(SP_TOKEN, "token"));
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("RL_ID", sp.getString(SP_ROLE_ID,"role_Id"));
                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));


                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("responce", resp);
                Log.d("req_roll", String.valueOf(jsonObject));
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                /* "GateinCount": 9,
                    "GateoutCount": 1,
                    "CleaningCount": 116,
                    "RepairEstimateCount": 7,
                    "RepairApprovalCount": 21,
                    "RepairCompletionCount": 10,
                    "SurveyCompletionCount": 2,
                    "RepairCount": 109,
                    "InspectionCount": 17,
                    "HeatingCount": 41,
                    "LeakTestCount": 24,
                    "Status": "Success"*/

                jsonArray = getJsonObject.getJSONArray("RoleDetails");

                GateinCount = getJsonObject.getString("GateinCount");
                GateOutCount = getJsonObject.getString("GateoutCount");
                CleaningCount = getJsonObject.getString("CleaningCount");
                InspectionCount = getJsonObject.getString("InspectionCount");
                HeatingCount = getJsonObject.getString("HeatingCount");
                LeaktestCount = getJsonObject.getString("LeakTestCount");
                RepairCount = getJsonObject.getString("RepairCount");
                Repair_Est = getJsonObject.getString("RepairEstimateCount");
                Repair_Appr = getJsonObject.getString("RepairApprovalCount");
                Repair_Com = getJsonObject.getString("RepairCompletionCount");
                Survey_Com = getJsonObject.getString("SurveyCompletionCount");
               /* authorization = getJsonObject.getString("Status");
                Log.d("error...",authorization);*/

                try {

                    if (jsonArray != null) {
                        list = new ArrayList<String>();
                        for (int i = 0; i < jsonArray.length(); i++) {

                            jsonObject = jsonArray.getJSONObject(i);
                            String view = jsonObject.getString("View");

                            if (view.equalsIgnoreCase("False")) {
                                // AtivityName
                                activityName = jsonObject.getString("AtivityName");

                                list.add(activityName);
                            }

                        }

                        for (int i = 0; i < list.size(); i++) {
                            String role = list.get(i);
                            if (role.equalsIgnoreCase("Heating")) {

                                runOnUiThread(new Runnable() {

                                    @Override
                                    public void run() {
                                        //update ui here
                                        // display toast here
                                        im_heating.setVisibility(View.GONE);


                                    }
                                });
                            } else if (role.equalsIgnoreCase("Gate Out")) {

                                runOnUiThread(new Runnable() {

                                    @Override
                                    public void run() {
                                        //update ui here
                                        // display toast here
                                        im_gateout.setVisibility(View.GONE);


                                    }
                                });
                            } else if (role.equalsIgnoreCase("Equipment History")) {

                                runOnUiThread(new Runnable() {

                                    @Override
                                    public void run() {
                                        //update ui here
                                        // display toast here
                                        im_equipement.setVisibility(View.GONE);


                                    }
                                });
                            } else if (role.equalsIgnoreCase("Gate In Creation")) {

                                runOnUiThread(new Runnable() {

                                    @Override
                                    public void run() {
                                        //update ui here
                                        // display toast here
                                        im_gatein.setVisibility(View.GONE);


                                    }
                                });

                            } else if (role.equalsIgnoreCase("Cleaning")) {


                                runOnUiThread(new Runnable() {

                                    @Override
                                    public void run() {
                                        //update ui here
                                        // display toast here
                                        im_cleaning.setVisibility(View.GONE);


                                    }
                                });

                            } else if (role.equalsIgnoreCase("Inspection")) {


                                runOnUiThread(new Runnable() {

                                    @Override
                                    public void run() {
                                        //update ui here
                                        // display toast here
                                        im_Inspection.setVisibility(View.GONE);


                                    }
                                });

                            } else if (role.equalsIgnoreCase("Leak Test")) {

                                runOnUiThread(new Runnable() {

                                    @Override
                                    public void run() {
                                        //update ui here
                                        // display toast here
                                        im_leaktest.setVisibility(View.GONE);


                                    }
                                });

                            } else if (role.equalsIgnoreCase("Repair Estimate")) {

                                runOnUiThread(new Runnable() {

                                    @Override
                                    public void run() {
                                        //update ui here
                                        // display toast here
                                        im_repair.setVisibility(View.GONE);


                                    }
                                });
                            }

                            Log.i("role", role);
                        }
                    }

                } catch (JSONException e) {
                    e.printStackTrace();
                    shortToast(getApplicationContext(), "Data Not Found");
                }
            } catch (UnsupportedEncodingException e) {
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
       /* if(authorization.equalsIgnoreCase("Authentication Required")){
            if(mServiceIntent!=null)
                getApplicationContext().stopService(mServiceIntent);
                SharedPreferences.Editor editor = sp.edit();
                editor.putBoolean(SP_LOGGED_IN, false);
                editor.commit();
                finish();
                Intent in = new Intent(getApplicationContext(), Login_Activity.class);
                startActivity(in);
        }else {*/
            gateInCount.setText(GateinCount);
            gateoutCount.setText(GateOutCount);
            heatingCount.setText(HeatingCount);
            inspectionCount.setText(InspectionCount);
            cleaningCount.setText(CleaningCount);
            leaktestCount.setText(LeaktestCount);
            repairCount.setText(RepairCount);

            repairCount.setVisibility(View.VISIBLE);
            gateInCount.setVisibility(View.VISIBLE);
            gateoutCount.setVisibility(View.VISIBLE);
            heatingCount.setVisibility(View.VISIBLE);
            cleaningCount.setVisibility(View.VISIBLE);
            inspectionCount.setVisibility(View.VISIBLE);
            leaktestCount.setVisibility(View.VISIBLE);

//        }
            if ((progressDialog != null) && progressDialog.isShowing()) {
                progressDialog.dismiss();
            }


        }
    }

    private void DisplayContact(Cursor c) {
        // TODO Auto-generated method stub
        Toast.makeText(getBaseContext(), "id: " + c.getString(0) + "\n" + "Name: " + c.getString(1) + "\n" + "Email: " + c.getString(3),
                Toast.LENGTH_LONG).show();
    }

    public class PostInfo extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        String responseString;
        private JSONArray invite_jsonlist;
        private JSONObject invitejsonObject;
        Cursor c;
        private JSONObject reqObj;

        public PostInfo(Cursor c) {
            this.c = c;
        }

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(MainActivity.this);
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
            try {
                JSONObject jsonObject = new JSONObject();

                invite_jsonlist = new JSONArray();
                reqObj = new JSONObject();
                try {

                    invitejsonObject = new JSONObject();

                    reqObj.put("ArrayOfFileParams", invite_jsonlist);


                } catch (Exception e) {
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
//                 if (cursor.moveToFirst()) {
                if (c != null && c.moveToFirst()) {
                    jsonObject.put("CSTMR_CD", c.getString(0));
                    jsonObject.put("CSTMR_ID", c.getString(1));
                    jsonObject.put("GTN_ID", c.getString(2));
                    jsonObject.put("Mode", c.getString(3));
                    jsonObject.put("PageName", c.getString(4));
                    jsonObject.put("RepairEstimateId", c.getString(5));
                    jsonObject.put("UserName", c.getString(6));
                    jsonObject.put("EIAttachment", c.getString(7));
                    jsonObject.put("EIHasChanges", c.getString(8));
                    jsonObject.put("GateinTransactionNo", c.getString(9));
                    jsonObject.put("IfAttchment", c.getString(10));
                    jsonObject.put("CHECKED", c.getString(11));
                    jsonObject.put("EQPMNT_NO", c.getString(12));
                    jsonObject.put("EQPMNT_TYP_CD", c.getString(13));
                    jsonObject.put("EQPMNT_TYP_ID", c.getString(14));
                    jsonObject.put("EQPMNT_CD_CD", c.getString(15));
                    jsonObject.put("EQPMNT_CD_ID", c.getString(16));
                    jsonObject.put("YRD_LCTN", c.getString(17));
                    jsonObject.put("GTN_DT", c.getString(18));
                    jsonObject.put("GTN_TM", c.getString(19));
                    jsonObject.put("PRDCT_DSCRPTN_VC", c.getString(20));
                    jsonObject.put("PRDCT_ID", c.getString(21));
                    jsonObject.put("PRDCT_CD", c.getString(22));
                    jsonObject.put("EIR_NO", c.getString(23));
                    jsonObject.put("VHCL_NO", c.getString(24));
                    jsonObject.put("TRNSPRTR_CD", c.getString(25));
                    jsonObject.put("RMRKS_VC", c.getString(26));
                    jsonObject.put("HTNG_BT", c.getString(27));
                    jsonObject.put("RNTL_BT", c.getString(28));
                    jsonObject.put("hfc", reqObj);
                    jsonObject.put("EIMNFCTR_DT", c.getString(29));
                    jsonObject.put("EITR_WGHT_NC", c.getString(30));
                    jsonObject.put("EIGRSS_WGHT_NC", c.getString(31));
                    jsonObject.put("EICPCTY_NC", c.getString(32));
                    jsonObject.put("EILST_SRVYR_NM", c.getString(33));
                    jsonObject.put("EILST_TST_DT", c.getString(34));
                    jsonObject.put("EILST_TST_TYP_ID", c.getString(35));
                    jsonObject.put("EINXT_TST_DT", c.getString(36));
                    jsonObject.put("EINXT_TST_TYP_ID", c.getString(37));
                    jsonObject.put("EIRMRKS_VC", c.getString(38));
                    jsonObject.put("EIACTV_BT", c.getString(39));
                    jsonObject.put("EIRNTL_BT", c.getString(40));

                }

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("request", jsonObject.toString());
                Log.d("responce", resp);
                JSONObject jsonResp = new JSONObject(resp);


                JSONObject returnMessage = jsonResp.getJSONObject("d");

                String message = returnMessage.getString("equipmentUpdate");
                responseString = message;
                Log.d("responseString", returnMessage.toString());

             /*   for(int j=0;j<returnMessage.length();j++)
                {
                     message = returnMessage.getString(j);
                    responseString=message;
                    Log.i("....responseString...",message);
                    // loop and add it to array or arraylist
                }
*/


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
            if (responseString != null) {
                if (responseString.equalsIgnoreCase("Success") || responseString.equalsIgnoreCase("This operation requires IIS integrated pipeline mode.")) {
                    Toast.makeText(getApplicationContext(), "GateIn Created Successfully.", Toast.LENGTH_SHORT).show();

                   /* db.open();
                    db.deleteContact(row_Count);
                    db.close();*/

                } else {
                    Toast.makeText(getApplicationContext(), "GateIn Creation Failed", Toast.LENGTH_SHORT).show();

                }
            } else {
                Toast.makeText(getApplicationContext(), "Unable to connect the Server..!", Toast.LENGTH_SHORT).show();

            }
            progressDialog.dismiss();

        }
    }



    public class Post_Heating_Update extends AsyncTask<Void, Void, Void> {
         Cursor c;
        ProgressDialog progressDialog;
        JSONObject jsonobject;
        private String responseStatus;

        public Post_Heating_Update(Cursor c) {
            this.c = c;
        }

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(MainActivity.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
//            if ((progressDialog != null) && progressDialog.isShowing()) {
                progressDialog.show();
//            }
        }

        @Override
        protected Void doInBackground(Void... params) {

            DefaultHttpClient httpClient = new DefaultHttpClient();
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpClient httpclient = new DefaultHttpClient();

            HttpPost httpPost = new HttpPost(ConstantValues.baseURLHeatingUpdate);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();
                if (c != null && c.moveToFirst()) {
                    jsonObject.put("UserName", c.getString(0));
                    jsonObject.put("EquipmentNo", c.getString(1));
                    jsonObject.put("StartDate", c.getString(2) + " " + c.getString(3));
                    jsonObject.put("StartTime", c.getString(3));
                    jsonObject.put("EndDate", c.getString(4) + " " + c.getString(5));
                    jsonObject.put("EndTime", c.getString(5));
                    jsonObject.put("Temp", c.getString(6));
                    jsonObject.put("TotalPeriod", c.getString(7));
                    jsonObject.put("MinRate", c.getString(8));
                    jsonObject.put("HourlyRate", c.getString(9));
                    jsonObject.put("TotalRate", c.getString(10));
                }
                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("total amount responce", resp);
                Log.d("req", jsonObject.toString());
                JSONObject jsonResp = new JSONObject(resp);

                jsonobject = jsonResp.getJSONObject("d");

                if (jsonobject != null) {


                    responseStatus  =jsonobject.getString("Status");
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
                if(responseStatus.equalsIgnoreCase("Heating Updated")){
                    shortToast(getApplicationContext(),responseStatus);
                    /*db.open();
                    db.delete();
                    db.close();*/
                }else{
                    shortToast(getApplicationContext(),"Heating Not Updated..!");
                }
            }
            else
            {
                shortToast(getApplicationContext(),"Data Not Found");
            }

            progressDialog.dismiss();

        }
    }


    public class CreateLeakTest extends AsyncTask<Void, Void, Void> {
        private final Cursor c;
        ProgressDialog progressDialog;
        String responseString;
        private JSONArray invite_jsonlist;
        private JSONObject invitejsonObject;

        public CreateLeakTest(Cursor c) {
            this.c = c;
        }


        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(MainActivity.this);
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
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLUpdateLeakTest);
            httpPost.setHeader("Content-Type", "application/json");
            try{
                JSONObject jsonObject = new JSONObject();


                if (c != null && c.moveToFirst()) {
                    jsonObject.put("UserName",c.getString(0));
                    jsonObject.put("LK_TST_ID", c.getString(1));
                    jsonObject.put("EQPMNT_NO", c.getString(2));
                    jsonObject.put("TST_DT", c.getString(3));
                    jsonObject.put("SHLL_TST_BT", c.getString(4));
                    jsonObject.put("STM_TB_TST_BT", c.getString(5));
                    jsonObject.put("EQPMNT_TYP_CD", c.getString(6));
                    jsonObject.put("EQPMNT_STTS_CD", c.getString(7));
                    jsonObject.put("CHECKED", c.getString(8));
                    jsonObject.put("GTN_DT", c.getString(9));
                    jsonObject.put("CSTMR_CD", c.getString(10));
                    jsonObject.put("RLF_VLV_SRL_1", c.getString(11));
                    jsonObject.put("RLF_VLV_SRL_2", c.getString(12));
                    jsonObject.put("PG_1", c.getString(13));
                    jsonObject.put("PG_2", c.getString(14));
                    jsonObject.put("RMRKS_VC", c.getString(15));

                }





                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("request", jsonObject.toString());
                Log.d("responce", resp);
                JSONObject jsonResp = new JSONObject(resp);


                JSONObject returnMessage = jsonResp.getJSONObject("d");

                responseString = returnMessage.getString("status");

                Log.d("responseString", returnMessage.toString());
/*
                for(int i=0;i<returnMessage.length();i++)
                {
                    String message = returnMessage.getString(i);
                    responseString=message;
                    Log.i("....responseString...",message);
                    // loop and add it to array or arraylist
                }
*/


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
                if (responseString.equalsIgnoreCase("Updated Successfully") ) {
                    Toast.makeText(getApplicationContext(), "Leak Test Created Successfully.", Toast.LENGTH_SHORT).show();



                } else {
                    Toast.makeText(getApplicationContext(), "Leak Test Creation Failed..!", Toast.LENGTH_SHORT).show();

                }
            }else
            {
                Toast.makeText(getApplicationContext(), "Connection TimeOut", Toast.LENGTH_SHORT).show();

            }
            progressDialog.dismiss();

        }
    }

    public class PostInfo_GateOut extends AsyncTask<Void, Void, Void> {
        private final Cursor c;
        ProgressDialog progressDialog;
        String responseString;
        private JSONArray invite_jsonlist;
        private JSONObject invitejsonObject;
        private JSONObject reqObj;

        public PostInfo_GateOut(Cursor c) {
            this.c = c;
        }


        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(MainActivity.this);
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

                        invitejsonObject = new JSONObject();
                       /* invitejsonObject.put("FileName", encodeArray.get(i).getFilename());
                        invitejsonObject.put("ContentLength",encodeArray.get(i).getLength());
                        invitejsonObject.put("base64imageString", encodeArray.get(i).getBase64());
                        invite_jsonlist.put(invitejsonObject);*/

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
//                String numberAsString = Integer.toString( pendingsize+1);
                if (c != null && c.moveToFirst()) {

                    jsonObject.put("UserName", c.getString(0));
                    jsonObject.put("EquipmentNo", c.getString(1));
                    jsonObject.put("YardLocation", c.getString(2));
                    jsonObject.put("OutDate", c.getString(3));
                    jsonObject.put("Time", c.getString(4));
                    jsonObject.put("EIRNo", c.getString(5));
                    jsonObject.put("hfc", reqObj);
                    jsonObject.put("VehicalNo", c.getString(6));
                    jsonObject.put("TransPorter", c.getString(7));
                    jsonObject.put("Remarks", c.getString(8));
                    jsonObject.put("Rental", c.getString(9));
                    jsonObject.put("RepairEstimateId", c.getString(10));
                    jsonObject.put("IfAttchment", c.getString(11));
                    jsonObject.put("Mode", c.getString(12));

                   /* if (From.equalsIgnoreCase("new")) {
                        jsonObject.put("Mode", "new");

                    } else {
                        jsonObject.put("Mode", "edit");
                    }*/
                }




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
                    Toast.makeText(getApplicationContext(), "GateOut Updated Successfully.", Toast.LENGTH_SHORT).show();

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

    private boolean checkWriteExternalPermission()
    {

        String permission = "android.permission.WRITE_EXTERNAL_STORAGE";
        int res = getApplicationContext().checkCallingOrSelfPermission(permission);
        return (res == PackageManager.PERMISSION_GRANTED);
    }

}
