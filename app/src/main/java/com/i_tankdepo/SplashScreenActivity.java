package com.i_tankdepo;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.widget.AdapterView;
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
public class SplashScreenActivity extends CommonActivity {

    private static int SPLASH_TIME_OUT = 3000;
    Handler mHandler = new Handler();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash_screen);

        mHandler.postDelayed(run, SPLASH_TIME_OUT);
    }
    Runnable run = new Runnable() {
        @Override
        public void run() {
            boolean loggedIn = sp.getBoolean(SP_LOGGED_IN, false);

            if (loggedIn) {
               /* CommonFragment cf=new CommonFragment() {
                    @Override
                    public void init(View view) {

                    }

                    @Override
                    public void onClick(View v) {

                    }

                    @Override
                    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                    }
                };
                if(cf.location_status.equals("success")){
                    if(cf.routes_status.equals("success")){*/
                startActivity(new Intent(SplashScreenActivity.this, MainActivity.class));
                   /* }
                    else {
                        startActivity(new Intent(SplashScreen.this, SignUpActivity.class));
                        cf.replaceFragment(new SixthPage());
                    }

                }
                else {
                    startActivity(new Intent(SplashScreen.this, SignUpActivity.class));
                    cf.replaceFragment(new FifthPageRider());

                }*/

            }
            else {

                startActivity(new Intent(SplashScreenActivity.this, Login_Activity.class));

            }
            finish();
        }
    };

    @Override
    public void onClick(View view) {

    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

    }
}
