package com.i_tankdepo;

/**
 * Created by Metaplore on 3/16/2016.
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
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;


import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.connection.ConnectionDetector;
import com.i_tankdepo.interfaces.AsyncResponseListener;

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
import java.io.InputStream;
import java.util.TreeSet;

public class Login_Activity extends CommonActivity implements AsyncResponseListener, View.OnClickListener {

    TextView tv_forget,tv_changePsw;

    String cityName="";
    Boolean isInternetPresent = false;
    Button signup,loginbutton;
    String lat,SecurityToken,responseString,getusername,getpassword,hsId,userId,responseStringId,GateinCount,GateOutCount,CleaningCount,InspectionCount
            ,HeatingCount,LeaktestCount;
     EditText username,password;
     ConnectionDetector cd;
    private ProgressDialog progressDialog;
    private String token;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        // TODO Auto-generated method stub
        super.onCreate(savedInstanceState);
        setContentView(R.layout.login);


        username=(EditText)findViewById(R.id.username);
        password=(EditText)findViewById(R.id.password);
        loginbutton=(Button)findViewById(R.id.login);
        tv_forget=(TextView)findViewById(R.id.tv_forget);

        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        loginbutton.setOnClickListener(this);
        tv_forget.setOnClickListener(this);

     /*   int idBold[] = {R.id.tv_Title};
        setBoldFont(idBold);*/

        int idNormal[] = {R.id.login,R.id.username,R.id.password,R.id.tv_forget,
                R.id.login};
        setRegularFont(idNormal);




    }

    @Override
    public void onPause() {
        super.onPause();

        if ((progressDialog != null) && progressDialog.isShowing())
            progressDialog.dismiss();
        progressDialog = null;
    }




    @Override
    public void onClick(View view) {
        switch (view.getId()) {



            // check for Internet slabRate

            case R.id.login:
              //  Toast.makeText(getApplicationContext(), "Login Successfully", Toast.LENGTH_SHORT).show();

                getusername=username.getText().toString();
                getpassword=password.getText().toString();



                if(getusername.equals("") && getpassword.equals(""))
                {
                    username.setError("Required");
                    password.setError("Required");


                }else {
                    if (isConnectingToInternet()) {
//                        SharedPreferences.Editor editor = sp.edit();
//                        editor.putString(SP_USER_ID, getusername);
//                        editor.putString(SP_AUTHR_PASSWORD, getpassword);
//                        editor.putBoolean(SP_LOGGED_IN, true);
//                        editor.commit();
                       // startActivity(new Intent(getApplicationContext(),MainActivity.class));
                     new PostInfo().execute();
                    }
                    else{
                        shortToast(getApplicationContext(), "Please Check Your Internet Connection");
                    }


                }

                break;

            case R.id.tv_forget:

                    startActivity(new Intent(getApplicationContext(),Forget_Password.class));
                break;


        }
    }
    public boolean isConnectingToInternet(){
        ConnectivityManager connectivity = (ConnectivityManager)getSystemService(Context.CONNECTIVITY_SERVICE);
        if (connectivity != null)
        {
            NetworkInfo[] info = connectivity.getAllNetworkInfo();
            if (info != null)
                for (int i = 0; i < info.length; i++)
                    if (info[i].getState() == NetworkInfo.State.CONNECTED)
                    {
                        return true;
                    }

        }
        return false;
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

    }

    @Override
    public void processFinish(String requestURL, String response) {

    }


    public class PostInfo extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONObject jsonResp;
        private JSONArray jsonArrayforRole;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Login_Activity.this);
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

            HttpPost httpPost = new HttpPost(ConstantValues.baseURLLogin);
           // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
       //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try{
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", getusername);
                jsonObject.put("Password", getpassword);
                JSONObject jsonObject1 = new JSONObject();
                jsonObject1.put("Credentials",jsonObject);

                StringEntity stringEntity = new StringEntity(jsonObject1.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("responce", resp);
                Log.d("req", String.valueOf(jsonObject1));
                 jsonResp = new JSONObject(resp);


               JSONObject returnMessage=jsonResp.getJSONObject("d");

                responseString = returnMessage.getString("ResponseText");
                token = returnMessage.getString("Token");
                responseStringId = returnMessage.getString("RL_ID");
               /* GateinCount = returnMessage.getString("GateinCount");
                GateOutCount = returnMessage.getString("GateoutCount");
                CleaningCount = returnMessage.getString("CleaningCount");
                InspectionCount = returnMessage.getString("InspectionCount");
                HeatingCount = returnMessage.getString("HeatingCount");
                LeaktestCount = returnMessage.getString("LeakTestCount");*/
                Log.d("responseString", responseString);
//                SecurityToken= token.get("Token").toString();
//                hsId= token.get("UserId").toString();
//                userId= token.get("HSId").toString();
               SharedPreferences.Editor editor = sp.edit();
//                editor.putString(SP_TOKEN, SecurityToken);
                 editor.putString(SP_USER_ID, getusername);
                 editor.putString(SP_AUTHR_PASSWORD, getpassword);
                 editor.putString(SP_ID, userId);
                 editor.putString(SP_ROLE_ID, responseStringId);
                 editor.putString(SP_TOKEN, token);
//                editor.putString(SP_HS_ID, hsId);
//                editor.putString(SP_USER_REMEMBER,remember );
               editor.commit();
               Log.d("user_Id--->",sp.getString(SP_USER_ID,"user_Id"));

             /*   for (int i = 0; i < returnMessage.length(); i++){
                     responseString = returnMessage.get(i).toString();
                    Log.d("responseString", responseString);
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
            if(responseString!=null)
            {
                if(responseString.equalsIgnoreCase("Success"))
                {
                    Toast.makeText(getApplicationContext(),"Login successful.", Toast.LENGTH_SHORT).show();
                    SharedPreferences.Editor editor = sp.edit();
                    editor.putBoolean(SP_LOGGED_IN, true);
                    editor.commit();

                    Intent i = new Intent(getApplication(), MainActivity.class);
                    GlobalConstants.roleID = responseStringId;

                    startActivity(i);

                }else
                {
                    Toast.makeText(getApplicationContext(),"Invalid Login", Toast.LENGTH_SHORT).show();

                }
            }else
            {
                Toast.makeText(getApplicationContext(),"Server problem !, Please try again later.", Toast.LENGTH_SHORT).show();
            }

            progressDialog.dismiss();

        }
    }


}
