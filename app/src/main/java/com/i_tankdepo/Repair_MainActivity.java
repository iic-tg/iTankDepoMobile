package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.appindexing.Action;
import com.google.android.gms.appindexing.AppIndex;
import com.google.android.gms.appindexing.Thing;
import com.google.android.gms.common.api.GoogleApiClient;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.SQLite.DBAdapter;

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
public class Repair_MainActivity extends CommonActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    Toolbar toolbar;
    private ImageView menu, up, down, iv_back, iv_changeOfStatus;
    private DrawerLayout drawer;
    LinearLayout LL_hole;
    private TextView tv_toolbarTitle;
    private Intent mServiceIntent;
    private JSONArray jsonArray;
    private ArrayList<String> list;
    private String activityName, RoleID;
    private String Repair_Est,Repair_Appr,Repair_Com,Survey_Com;
    /**
     * ATTENTION: This was auto-generated to implement the App Indexing API.
     * See https://g.co/AppIndexing/AndroidStudio for more information.
     */
    private GoogleApiClient client;
    private LinearLayout toolbar_background,LL_repair_Estimation,LL_Repair_Approval,LL_Repair_completion,LL_Survey_completion;
    private ImageView im_repair_estimation,im_repair_approval,im_repair_completion,im_survey_completion,home_changeStatus;
    private Button repair_est_Count,repair_app_Count,repair_com_Count,survey_com_Count;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.repair_menu);


        Repair_Est=GlobalConstants.repair_est_count;
        Repair_Appr=GlobalConstants.repair_app_count;
        Repair_Com=GlobalConstants.repair_com_count;
        Survey_Com=GlobalConstants.survey_com_count;

        im_repair_estimation = (ImageView) findViewById(R.id.im_repair_estimation);
        im_repair_approval = (ImageView) findViewById(R.id.im_repair_approval);
        im_repair_completion = (ImageView) findViewById(R.id.im_repair_completion);
        im_survey_completion = (ImageView) findViewById(R.id.im_survey_completion);
        repair_est_Count = (Button) findViewById(R.id.bt_repair_est_Count);
        repair_app_Count = (Button) findViewById(R.id.bt_repair_app_count);
        repair_com_Count = (Button) findViewById(R.id.bt_repair_com_count);
        survey_com_Count = (Button) findViewById(R.id.bt_survey_com_count);

        repair_est_Count.setVisibility(View.VISIBLE);
        repair_app_Count.setVisibility(View.VISIBLE);
        repair_com_Count.setVisibility(View.VISIBLE);
        survey_com_Count.setVisibility(View.VISIBLE);
        repair_est_Count.setText(Repair_Est);
        repair_app_Count.setText(Repair_Appr);
        repair_com_Count.setText(Repair_Com);
        survey_com_Count.setText(Survey_Com);

        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        iv_back.setVisibility(View.GONE);
        RoleID = GlobalConstants.roleID;

        home_changeStatus = (ImageView) findViewById(R.id.iv_changeOfStatus);
        home_changeStatus.setOnClickListener(this);




        if (cd.isConnectingToInternet()) {
          //  new Post_Role_Based().execute();
        } else {
            shortToast(getApplicationContext(), "Please Check Your Internet Connection");
        }


        LL_repair_Estimation = (LinearLayout) findViewById(R.id.LL_repair_Estimation);
        LL_Repair_Approval = (LinearLayout) findViewById(R.id.LL_Repair_Approval);
        LL_Repair_completion = (LinearLayout) findViewById(R.id.LL_Repair_completion);
        LL_Survey_completion = (LinearLayout) findViewById(R.id.LL_Survey_completion);


        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Repair");


        LL_repair_Estimation.setOnClickListener(this);
        LL_Repair_Approval.setOnClickListener(this);
        LL_Repair_completion.setOnClickListener(this);
        LL_Survey_completion.setOnClickListener(this);


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
            finish();
            startActivity(new Intent(getApplicationContext(), MainActivity.class));

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
            case R.id.iv_changeOfStatus:
                GlobalConstants.equipment_no="";
                GlobalConstants.status="AWE";
                GlobalConstants.status_id="7";
                startActivity(new Intent(getApplicationContext(), ChangeOfStatus.class));
                break;
            case R.id.LL_repair_Estimation:
                startActivity(new Intent(getApplicationContext(), RepairEstimatePending.class));
                break;
            case R.id.LL_Repair_Approval:
                startActivity(new Intent(getApplicationContext(), RepairApprovalPending.class));
                break;
            case R.id.LL_Repair_completion:
                startActivity(new Intent(getApplicationContext(), RepairCompletionPending.class));
                break;
            case R.id.LL_Survey_completion:
                startActivity(new Intent(getApplicationContext(), SurveyCompletionPending.class));
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





}
