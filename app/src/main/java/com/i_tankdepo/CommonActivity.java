package com.i_tankdepo;

import android.app.Activity;
import android.app.Dialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.Typeface;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.support.annotation.Nullable;
import android.text.InputFilter;
import android.text.Spanned;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;


import com.i_tankdepo.connection.ConnectionDetector;
import com.squareup.picasso.Picasso;

import java.util.List;
import java.util.regex.Pattern;


/**
 * Created by Edwin on 24-11-2015.
 */
public abstract class CommonActivity extends Activity implements View.OnClickListener,AdapterView.OnItemClickListener {
//public abstract class CommonActivity extends AppCompatActivity {
    public Typeface Regular_font, Bold_font;
//    public abstract void init();

    // Shared Preferences
    public static SharedPreferences sp;
    public SharedPreferences sp2;
    public static final String PREF_NAME2 = "Redington perm";
    public static final String SP_USER_REMEMBER = "remember";
    public static final String SP_USER_ROLL = "ASM";
    public static final String SP_USER_IMAGE = "profile";
    public static final String SP2_CAMERA_PERM_DENIED = "camPermDenied";
    public static final String SP2_STORAGE_PERM_DENIED = "storagePermDenied";

    public ConnectionDetector cd;
    public static final String PREF_NAME = "HappiHive";
    public static final String SP_AUTHR_USER = "redington";
    public static final String SP_AUTHR_PASSWORD = "password";
    public static final String SP_TOKEN = "token";
    public static final String SP_ITEM_LABLE = "lable_item";
    public static final String SP_LOGGED_IN = "loggedIn";
    public static final String SP_USER_ID = "user_Id";
    public static final String SP_ROLE_ID = "role_Id";
    public static final String SP_LINE_ITEM_JSON = "line_item_json";
    public static final String SP_ADD_LINE_ITEM_JSON = "add_line_item_json";
    public static final String SP_ORG_TC_AMT = "original_tc_amount";
    public static final String SP_ORG_APP_TC_AMT = "original_app_tc_amount";
    public static final String SP_ID= "Id";
    public static final String SP_HS_ID = "hsId";
    public static final String SP_Multiple_Image_Json = "image_json";
    public static final String SP_Add_datails_Json = "detail_json";

    public static final String SP_TYPE = "user_type";
    public static final String SP_MODEL_LIST = "model_list";
    public Dialog add_dialog,dialog;
    public ListView mListView;
    ArrayAdapter<String> aa;
    private String TAG="CommonActivity";
     Dialog mDialog;
    private Toast mToastToShow;
    private int count;


    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Regular_font = Typeface.createFromAsset(this.getAssets(), "fonts/Montserrat-Regular.ttf");
        Bold_font = Typeface.createFromAsset(this.getAssets(), "fonts/Montserrat-Bold.ttf");

        sp = getSharedPreferences(PREF_NAME, Context.MODE_PRIVATE);
        sp2 = getSharedPreferences(PREF_NAME2, Context.MODE_PRIVATE);
        cd=new ConnectionDetector(this);
//        init();
    }


    //for Applying Regular_font
    public void setRegularFont(int[] ids) {
        for (int i=0; i<ids.length; i++) {
            if (findViewById(ids[i]) instanceof Button) {
                Button button = (Button) findViewById(ids[i]);
                button.setTypeface(Regular_font);
            }
            else if (findViewById(ids[i]) instanceof TextView){
                TextView textView = (TextView) findViewById(ids[i]);
                textView.setTypeface(Regular_font);
            }
            else if (findViewById(ids[i]) instanceof EditText){
                EditText editText = (EditText) findViewById(ids[i]);
                editText.setTypeface(Regular_font);
            }
        }
    }

    //for Applying Bold_font
    public void setBoldFont(int[] ids) {
        for (int i=0; i<ids.length; i++) {
            if (findViewById(ids[i]) instanceof Button) {
                Button button = (Button) findViewById(ids[i]);
                button.setTypeface(Bold_font);
            }
            else if (findViewById(ids[i]) instanceof TextView){
                TextView textView = (TextView) findViewById(ids[i]);
                textView.setTypeface(Bold_font);
            }
            else if (findViewById(ids[i]) instanceof EditText){
                EditText editText = (EditText) findViewById(ids[i]);
                editText.setTypeface(Bold_font);
            }
        }
    }

    //for Dialog
    public void setRegularFont(Dialog dialog, int[] ids) {
        for (int i=0; i<ids.length; i++) {
            if (dialog.findViewById(ids[i]) instanceof Button) {
                Button button = (Button) dialog.findViewById(ids[i]);
                button.setTypeface(Regular_font);
            } else if (dialog.findViewById(ids[i]) instanceof EditText) {
                EditText editText = (EditText) dialog.findViewById(ids[i]);
                editText.setTypeface(Regular_font);
            } else if (dialog.findViewById(ids[i]) instanceof TextView) {
                TextView textView = (TextView) dialog.findViewById(ids[i]);
                textView.setTypeface(Regular_font);
            }
        }
    }

    //for Dialog
    public void setBoldFont(Dialog dialog, int[] ids) {
        for (int i=0; i<ids.length; i++) {
            if (dialog.findViewById(ids[i]) instanceof Button) {
                Button button = (Button) dialog.findViewById(ids[i]);
                button.setTypeface(Bold_font);
            } else if (dialog.findViewById(ids[i]) instanceof EditText) {
                EditText editText = (EditText) dialog.findViewById(ids[i]);
                editText.setTypeface(Bold_font);
            } else if (dialog.findViewById(ids[i]) instanceof TextView) {
                TextView textView = (TextView) dialog.findViewById(ids[i]);
                textView.setTypeface(Bold_font);
            }
        }
    }

    public static boolean isEmpty(String input) {
        if (input == null || input.trim().length() == 0
                || "".equals(input.toString().trim())
                || " ".equals(input.toString().trim())) {
            return true;
        }
        return false;
    }


    public void shortToast(Context con, String msg) {
        Toast.makeText(con, msg, Toast.LENGTH_SHORT).show();
    }


/*
    public void shortToast(Context con,String msg) {
        // Set the toast and duration
        int toastDurationInMilliSeconds = 8000;
        mToastToShow = Toast.makeText(con,msg, Toast.LENGTH_LONG);

        // Set the countdown to display the toast
        CountDownTimer toastCountDown;
        toastCountDown = new CountDownTimer(toastDurationInMilliSeconds, 1000 */
/*Tick duration*//*
) {
            public void onTick(long millisUntilFinished) {
                mToastToShow.show();
            }
            public void onFinish() {
                mToastToShow.cancel();
            }
        };

        // Show the toast and starts the countdown
        mToastToShow.show();
        toastCountDown.start();
    }
*/

    public void longToast(Context con, String msg) {
        Toast.makeText(con, msg, Toast.LENGTH_LONG).show();
    }

    public int check_one(String values)
    {
        count=0;
        for(int i=0;i<values.length();i++)
        {
            if(Character.isLetter(values.charAt(i)))
            {
                count++;
            }
        }

        return count;
    }
    public int check_two(String values)
    {
        count=0;
        for(int i=0;i<values.length();i++)
        {
            if(values.matches(".*[0-9].*"))
            {
                count++;
            }
        }
        return count;
    }

    public boolean isReqFieldsFilled(Context con, String[] allFields) {
        for (int i=0; i<allFields.length; i++) {
            if(isEmpty(allFields[i])) {
                longToast(getApplicationContext(), "Please fill the required fields");
                return false;
            }
        }
        return true;
    }
    public void popImageUp(final Bitmap bitmap,String url) {

        dialog = new Dialog(this, R.style.FullHeightDialog);
        dialog.requestWindowFeature(dialog.getWindow().FEATURE_NO_TITLE);
        dialog.setContentView(R.layout.coupon_view);
        ImageView image=(ImageView) dialog.findViewById(R.id.coupon_image);
        ImageView btn_ok=(ImageView)dialog.findViewById(R.id.close_btn);

            if(url.equals(""))
            {
                image.setImageBitmap(bitmap);

            }else{

                Picasso.with(getApplicationContext())
                        .load(url)
                        .into(image);

            }




        // R.id.b_ok,R.id.b_lookUp,R.id.ed_costumer_code,R.id.dialog_text
        btn_ok.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {


                dialog.dismiss();


            }
        });


        dialog.show();
    }

    private String blockCharacterSet = "~#^|$%&*!";

    public InputFilter filter = new InputFilter() {

        @Override
        public CharSequence filter(CharSequence source, int start, int end, Spanned dest, int dstart, int dend) {

            if (source != null && blockCharacterSet.contains(("" + source))) {
                return "";
            }
            return null;
        }
    };
    public void showDialogWithLV(Context con, final List<String> list) {
        mDialog = new Dialog(con);
        mDialog.requestWindowFeature(Window.FEATURE_NO_TITLE);
        mDialog.setContentView(R.layout.list_view);
        mDialog.setCanceledOnTouchOutside(true);


        mListView = (ListView) mDialog.findViewById(R.id.ad_listView);
        mListView.setOnItemClickListener(this);

        aa = new ArrayAdapter<String>(getApplicationContext(), R.layout.mytextview, list) {
            @Override
            public View getView(int position, View convertView, ViewGroup parent) {
                View view = super.getView(position, convertView, parent);


                aa.notifyDataSetChanged();
                TextView text = (TextView) view.findViewById(android.R.id.text1);


                //text.setTypeface(containerFont);


                return view;
            }

        };
        mListView.setAdapter(aa);

        // aa.notifyDataSetChanged();

        mDialog.show();
    }

    public void popUp_remarks(String remark) {

        dialog = new Dialog(CommonActivity.this);
        dialog.setContentView(R.layout.remark_info);
        TextView equip_no=(TextView)dialog.findViewById(R.id.equip_no);
        final TextView ed_remark=(TextView)dialog.findViewById(R.id.ed_remark);
        ed_remark.setText(remark);
        final TextView bt_close=(TextView) dialog.findViewById(R.id.bt_close);



        bt_close.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                dialog.dismiss();
            }
        });

        dialog.show();
    }

    public void popUp_equipment_info(String get_equip_no,String get_status, String get_status_id,
                                      String  get_previous_cargo,String tare,String cross,String capacity,String manf_date) {

        dialog = new Dialog(CommonActivity.this);
        dialog.setContentView(R.layout.repair_equipment_info);
        TextView equip_no=(TextView)dialog.findViewById(R.id.equip_no);
        final EditText tv_previous_cagro=(EditText)dialog.findViewById(R.id.tv_previous_cagro);
        final EditText tv_Status_id=(EditText)dialog.findViewById(R.id.tv_Status_id);
        final EditText tv_status=(EditText)dialog.findViewById(R.id.tv_status);
        final EditText tv_tare_weight=(EditText)dialog.findViewById(R.id.tv_tare_weight);
        final EditText tv_cross_weight=(EditText)dialog.findViewById(R.id.tv_cross_weight);
        final EditText tv_capacity=(EditText)dialog.findViewById(R.id.tv_capacity);
        final EditText tv_manf_date=(EditText)dialog.findViewById(R.id.tv_manf_date);
        final TextView bt_close=(TextView) dialog.findViewById(R.id.bt_close);

        tv_previous_cagro.setText(get_previous_cargo);
        equip_no.setText("Equipment Detail - "+get_equip_no);
        tv_Status_id.setText(get_status_id);
        tv_status.setText(get_status);
        tv_tare_weight.setText(tare);
        tv_cross_weight.setText(cross);
        tv_capacity.setText(capacity);
        tv_manf_date.setText(manf_date);

        bt_close.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                dialog.dismiss();
            }
        });

        dialog.show();
    }
    public String getColoredSpanned(String text, String color) {
        String input = "<font color=" + color + ">" + text + "</font>";
        return input;
    }


}
