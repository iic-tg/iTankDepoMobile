package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Intent;
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

import static com.i_tankdepo.R.id.username;

/**
 * Created by Metaplore on 10/19/2016.
 */

public class Forget_Password extends CommonActivity {

    private EditText email,username;
    private Button submit,forgotLogin;
    String getEmail,getusername;
    private String responseString,responseError;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        // TODO Auto-generated method stub
        super.onCreate(savedInstanceState);
        setContentView(R.layout.forget_password);
        username = (EditText)findViewById(R.id.tv_username);
        email = (EditText)findViewById(R.id.tv_email);
        submit = (Button)findViewById(R.id.bt_submit);
        submit.setOnClickListener(this);
        forgotLogin = (Button)findViewById(R.id.forgot_login);
        forgotLogin.setOnClickListener(this);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        int idNormal[] = {R.id.login,R.id.tv_username,R.id.password,R.id.tv_forget,
                R.id.tv_email};
        setRegularFont(idNormal);

    }
    @Override
    public void onClick(View view) {
        switch(view.getId()){
            case R.id.bt_submit:
                getEmail = email.getText().toString();
                getusername = username.getText().toString();

                if(getusername.equals("") && getEmail.equals("")){

                    email.setError("Enter your Email..!");
                    username.setError("Username Field is Required..!");

                }else {
                    if (cd.isConnectingToInternet()) {


                        new ForgotPassword().execute();

                    }else{
                        shortToast(getApplicationContext(), "Please Check Your Internet Connection..!");
                    }
                }
                break;
            case R.id.forgot_login:
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
            progressDialog = new ProgressDialog(Forget_Password.this);
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

            HttpPost httpPost = new HttpPost(ConstantValues.baseURLForgotPwd);
            // httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-Type", "application/json");
            //     httpPost.addHeader("content-orgCleaningDate", "application/x-www-form-urlencoded");
//            httpPost.setHeader("SecurityToken", sp.getString(SP_TOKEN,"token"));
            try {
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("bv_strUserName", getusername);
                jsonObject.put("bv_strEmailId", getEmail);

                //  jsonObject.put("confirmPassword", getconfirmPwd);
                JSONObject jsonObject1 = new JSONObject();
//                jsonObject1.put("ChangePasswordCredentials",jsonObject);

                StringEntity stringEntity = new StringEntity(jsonObject1.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("responce", resp);
                Log.d("request", String.valueOf(jsonObject1));
                jsonResp = new JSONObject(resp);


                //  JSONArray returnMessage = jsonResp.getJSONArray("ReturnMessage");
                JSONObject returnMessage = jsonResp.getJSONObject("d");

                responseString = returnMessage.getString("statusText");
                responseError = returnMessage.getString("validateEmailStatus");
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

            progressDialog.dismiss();

            super.onPostExecute(aVoid);
            if (responseString != null) {
                if (responseString.equalsIgnoreCase("OK")) {

                    Toast.makeText(getApplicationContext(), "Your Password has been sent your Mail Id.", Toast.LENGTH_SHORT).show();
                    username.setText("");
                    email.setText("");

                    Intent i = new Intent(getApplication(), Login_Activity.class);
                    startActivity(i);

                }else if(responseError.equalsIgnoreCase("Invalid Username or Email Id") ||responseString.equalsIgnoreCase("")){

                    Toast.makeText(getApplicationContext(), responseError, Toast.LENGTH_SHORT).show();
                }



            }else {
                Toast.makeText(getApplicationContext(), "Server Problem !, Please try again later.", Toast.LENGTH_SHORT).show();

            }
        }

    }



}
