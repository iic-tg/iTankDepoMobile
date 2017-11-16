package com.i_tankdepo;

import android.Manifest;
import android.annotation.TargetApi;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.pdf.PdfDocument;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.support.annotation.RequiresApi;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.util.DisplayMetrics;
import android.view.Display;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Constants.GlobalConstants;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
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
public class SendMailActivity extends AppCompatActivity implements View.OnClickListener{

    Button btn_generate;
    TextView tv_link,tv_Customer,tv_Equipment,tv_Code,tv_Type,tv_Status,tv_Yard_Loc,tv_In_Date,tv_Time,tv_Previous_Cargo,tv_EIR_No
            ,tv_Vechicle_No,tv_Transporter,tv_Heating,tv_Remarks,tv_Rental,tv_Printed_By,tv_Printed_Date,tv_Page_no;
    ImageView iv_image;
    LinearLayout ll_pdflayout;
    public static int REQUEST_PERMISSIONS = 1;
    boolean boolean_permission;
    boolean boolean_save;
    Bitmap bitmap;
    ProgressDialog progressDialog;
    private String systemDate;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.print_email_format);
        init();
        fn_permission();
        listener();
        systemDate = new SimpleDateFormat("dd-MMM-yyyy").format(new Date());
            tv_Printed_Date.setText(systemDate);
        String print_string=GlobalConstants.print_string;

        try {
            JSONObject jsonrootObject = new JSONObject(print_string);
            tv_Previous_Cargo.setText(jsonrootObject.getString("get_sp_previous"));
            tv_Equipment.setText(jsonrootObject.getString("get_equipment"));
            tv_Type.setText(jsonrootObject.getString("get_repair_type_cd"));
            tv_Status.setText(jsonrootObject.getString("get_status"));
            tv_Code.setText(jsonrootObject.getString("get_code"));
            tv_In_Date.setText(jsonrootObject.getString("get_InDate"));
            tv_Time.setText(jsonrootObject.getString("get_InDate"));
            tv_Vechicle_No.setText(jsonrootObject.getString("get_repair_type_cd"));
            tv_Transporter.setText(jsonrootObject.getString("get_nexttest_date"));
            tv_Heating.setText(jsonrootObject.getString("get_last_test_date"));
            tv_Remarks.setText(jsonrootObject.getString("get_remark"));


        } catch (JSONException e) {
            e.printStackTrace();
        }

    }


    private void init(){
        btn_generate = (Button)findViewById(R.id.btn_generate);
        tv_Customer = (TextView)findViewById(R.id.tv_Customer);
        tv_Equipment = (TextView)findViewById(R.id.tv_Equipment);
        tv_Type = (TextView)findViewById(R.id.tv_Type);
        tv_Code = (TextView)findViewById(R.id.tv_Code);
        tv_Status = (TextView)findViewById(R.id.tv_Status);
        tv_Yard_Loc = (TextView)findViewById(R.id.tv_Yard_Loc);
        tv_In_Date = (TextView)findViewById(R.id.tv_In_Date);
        tv_Time = (TextView)findViewById(R.id.tv_Time);
        tv_Previous_Cargo = (TextView)findViewById(R.id.tv_Previous_Cargo);
        tv_EIR_No = (TextView)findViewById(R.id.tv_EIR_No);
        tv_Vechicle_No = (TextView)findViewById(R.id.tv_Vechicle_No);
        tv_Transporter = (TextView)findViewById(R.id.tv_Transporter);
        tv_Heating = (TextView)findViewById(R.id.tv_Heating);
        tv_Remarks = (TextView)findViewById(R.id.tv_Remarks);
        tv_Rental = (TextView)findViewById(R.id.tv_Rental);
        tv_Page_no = (TextView)findViewById(R.id.tv_Page_no);
        tv_Printed_By = (TextView)findViewById(R.id.tv_Printed_By);
        tv_Printed_Date = (TextView)findViewById(R.id.tv_Printed_Date);
        ll_pdflayout = (LinearLayout) findViewById(R.id.ll_pdflayout);

    }

    private void listener(){
        btn_generate.setOnClickListener(this);
    }

    @TargetApi(Build.VERSION_CODES.KITKAT)
    @RequiresApi(api = Build.VERSION_CODES.KITKAT)
    @Override
    public void onClick(View view) {

        switch (view.getId()) {
            case R.id.btn_generate:

                if (boolean_save) {
                    Intent intent = new Intent(getApplicationContext(), PDFViewActivity.class);
                    startActivity(intent);

                } else {
                    if (boolean_permission) {
                        progressDialog = new ProgressDialog(SendMailActivity.this);
                        progressDialog.setMessage("Please wait");
                        bitmap = loadBitmapFromView(ll_pdflayout, ll_pdflayout.getWidth(), ll_pdflayout.getHeight());
                        createPdf();
//                        saveBitmap(bitmap);
                    } else {

                    }

                    createPdf();
                }

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

        }
    }

    @RequiresApi(api = Build.VERSION_CODES.KITKAT)
    private void createPdf(){
        WindowManager wm = (WindowManager) getSystemService(Context.WINDOW_SERVICE);
        Display display = wm.getDefaultDisplay();
        DisplayMetrics displaymetrics = new DisplayMetrics();
        this.getWindowManager().getDefaultDisplay().getMetrics(displaymetrics);
        float hight = displaymetrics.heightPixels ;
        float width = displaymetrics.widthPixels ;

        int convertHighet = (int) hight, convertWidth = (int) width;

//        Resources mResources = getResources();
//        Bitmap bitmap = BitmapFactory.decodeResource(mResources, R.drawable.screenshot);

        PdfDocument document = new PdfDocument();
        PdfDocument.PageInfo pageInfo = new PdfDocument.PageInfo.Builder(convertWidth, convertHighet, 1).create();
        PdfDocument.Page page = document.startPage(pageInfo);

        Canvas canvas = page.getCanvas();


        Paint paint = new Paint();
        canvas.drawPaint(paint);


        bitmap = Bitmap.createScaledBitmap(bitmap, convertWidth, convertHighet, true);

        paint.setColor(Color.BLUE);
        canvas.drawBitmap(bitmap, 0, 0 , null);
        document.finishPage(page);


        // write the document content
        String targetPdf = "/sdcard/repair_estimate.pdf";
        File filePath = new File(targetPdf);
        try {
            document.writeTo(new FileOutputStream(filePath));
            btn_generate.setText("Check PDF");
            Intent intent = new Intent();
            intent.setAction(Intent.ACTION_VIEW);
            intent.setDataAndType(Uri.fromFile(filePath), "application/pdf");
            startActivity(intent);
            boolean_save=true;
        } catch (IOException e) {
            e.printStackTrace();
            Toast.makeText(this, "Something wrong: " + e.toString(), Toast.LENGTH_LONG).show();
        }

        // close the document
        document.close();
    }



    public static Bitmap loadBitmapFromView(View v, int width, int height) {
        Bitmap b = Bitmap.createBitmap(width, height, Bitmap.Config.ARGB_8888);
        Canvas c = new Canvas(b);
        v.draw(c);

        return b;
    }

    private void fn_permission() {
        if ((ContextCompat.checkSelfPermission(getApplicationContext(), Manifest.permission.READ_EXTERNAL_STORAGE) != PackageManager.PERMISSION_GRANTED)||
                (ContextCompat.checkSelfPermission(getApplicationContext(), Manifest.permission.WRITE_EXTERNAL_STORAGE) != PackageManager.PERMISSION_GRANTED)) {

            if ((ActivityCompat.shouldShowRequestPermissionRationale(SendMailActivity.this, Manifest.permission.READ_EXTERNAL_STORAGE))) {
            } else {
                ActivityCompat.requestPermissions(SendMailActivity.this, new String[]{Manifest.permission.READ_EXTERNAL_STORAGE},
                        REQUEST_PERMISSIONS);

            }

            if ((ActivityCompat.shouldShowRequestPermissionRationale(SendMailActivity.this, Manifest.permission.WRITE_EXTERNAL_STORAGE))) {
            } else {
                ActivityCompat.requestPermissions(SendMailActivity.this, new String[]{Manifest.permission.WRITE_EXTERNAL_STORAGE},
                        REQUEST_PERMISSIONS);

            }
        } else {
            boolean_permission = true;


        }
    }
    @Override
    public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        if (requestCode == REQUEST_PERMISSIONS) {

            if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {

                boolean_permission = true;


            } else {
                Toast.makeText(getApplicationContext(), "Please allow the permission", Toast.LENGTH_LONG).show();

            }
        }
    }


}