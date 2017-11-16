package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.i_tankdepo.Constants.ConstantValues;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;

/**
 * Created by Metaplore on 10/19/2016.
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
public class Change_Password extends CommonActivity {

    private EditText oldPassword,newPassword,confirmPassword,username;
    private Button save,forgotLogin;
    String getoldPwd,getnewPwd,responseString,getconfirmPwd,getusername;
    String userName;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        // TODO Auto-generated method stub
        super.onCreate(savedInstanceState);
        setContentView(R.layout.change_password);
        username = (EditText)findViewById(R.id.tv_username);
        oldPassword = (EditText)findViewById(R.id.oldPassword);
        newPassword = (EditText)findViewById(R.id.newPassword);
        confirmPassword = (EditText)findViewById(R.id.confirm_Psw);
        save = (Button)findViewById(R.id.save);
        forgotLogin =(Button)findViewById(R.id.change_login);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        int idNormal[] = {R.id.login,R.id.tv_username,R.id.oldPassword,R.id.newPassword,R.id.confirm_Psw,R.id.save,
                R.id.change_login};
        setRegularFont(idNormal);
        save.setOnClickListener(this);
        forgotLogin.setOnClickListener(this);


    }
    @Override
    public void onClick(View view) {
        switch (view.getId()){
            case R.id.save:
                     getusername = username.getText().toString();
                     getoldPwd = oldPassword.getText().toString();
                     getnewPwd = newPassword.getText().toString();
                     getconfirmPwd = confirmPassword.getText().toString();

                if(getoldPwd.equals("") && getnewPwd.equals("") && getconfirmPwd.equals(""))
                {
                    username.setError("Username is Required");
                    oldPassword.setError("Old Password Required");
                    newPassword.setError("New Password Required");
                    confirmPassword.setError("Confirm Password Required");


                }else if(getoldPwd.equals(getnewPwd)) {
                    shortToast(getApplicationContext(), "The New password cannot be same as Old password.");
                }else if(newPassword.equals(confirmPassword)){
                    shortToast(getApplicationContext(),"Passwords don't match.");
                }
                else if (cd.isConnectingToInternet()) {
//                        SharedPreferences.Editor editor = sp.edit();
//                        editor.putString(SP_USER_ID, getusername);
//                        editor.putString(SP_AUTHR_PASSWORD, getpassword);
//                        editor.putBoolean(SP_LOGGED_IN, true);
//                        editor.commit();
                        // startActivity(new Intent(getApplicationContext(),MainActivity.class));
                        new ForgotPassword().execute();
                    }
                    else {
                        shortToast(getApplicationContext(), "Please Check Your Internet Connection");
                    }



        break;

        case R.id.change_login:
                finish();
                startActivity(new Intent(getApplicationContext(),Login_Activity.class));
                break;

        }
    }

    @Override
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

    }
    public class ForgotPassword extends AsyncTask<Void, Void, Void> {
        ProgressDialog progressDialog;
        private JSONObject jsonResp;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Change_Password.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
//            if ((progressDialog != null) && progressDialog.isShowing()) {
                progressDialog.show();

        }

        @Override
        protected Void doInBackground(Void... params) {

            DefaultHttpClient httpClient = new DefaultHttpClient();
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpClient httpclient = new DefaultHttpClient();

            HttpPost httpPost = new HttpPost(ConstantValues.baseURLChangePwd);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try{
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("Username",getusername);
                jsonObject.put("bv_strOldPassword", getoldPwd);
                jsonObject.put("bv_strNewPwd", getnewPwd);
              //  jsonObject.put("confirmPassword", getconfirmPwd);
                JSONObject jsonObject1 = new JSONObject();
                jsonObject1.put("ChangePasswordCredentials",jsonObject);

                StringEntity stringEntity = new StringEntity(jsonObject1.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("responce", resp);
                Log.d("request", String.valueOf(jsonObject1));
                jsonResp = new JSONObject(resp);


                //  JSONArray returnMessage = jsonResp.getJSONArray("ReturnMessage");
                JSONObject returnMessage=jsonResp.getJSONObject("d");

                responseString = returnMessage.getString("changePassordStatus");
                Log.d("responseString", responseString);

               // sp.getString(SP_USER_ID,"user_Id");
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
                if(responseString.equalsIgnoreCase("Your password has been changed successfully"))
                {

                    Toast.makeText(getApplicationContext(),"Your password has been changed successfully.", Toast.LENGTH_SHORT).show();
                    username.setText("");
                    oldPassword.setText("");
                    newPassword.setText("");
                    confirmPassword.setText("");
                    finish();
                    Intent i = new Intent(getApplication(), Login_Activity.class);
                    startActivity(i);

                }else if(responseString.equalsIgnoreCase("The Old Password is Invalid"))
                {
                    Toast.makeText(getApplicationContext(),"The Old Password is Invalid.", Toast.LENGTH_SHORT).show();

                }
            }else
            {
                Toast.makeText(getApplicationContext(),"Server problem !, Please try again later.", Toast.LENGTH_SHORT).show();
            }

            progressDialog.dismiss();

        }
    }



}
