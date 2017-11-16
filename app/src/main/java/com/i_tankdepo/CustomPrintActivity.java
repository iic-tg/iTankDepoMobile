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

import static com.i_tankdepo.R.id.ed_MH_date;

public class CustomPrintActivity extends AppCompatActivity implements View.OnClickListener{

    Button btn_generate;
    TextView tv_link,tv_Customer,tv_Equipment,tv_Code,tv_Type,textView2,tv_Status,tv_Yard_Loc,tv_In_Date,tv_Time,tv_Previous_Cargo,tv_EIR_No
            ,tv_Vechicle_No,tv_Transporter,tv_Heating,tv_Remarks,tv_Rental,tv_Printed_By,tv_Printed_Date,tv_Page_no;
    ImageView iv_image;
    LinearLayout ll_pdflayout;
    public static int REQUEST_PERMISSIONS = 1;
    boolean boolean_permission;
    boolean boolean_save;
    Bitmap bitmap;
    ProgressDialog progressDialog;
    private String systemDate;
    private LinearLayout ll_gulf,ll_gulf_one,LL_previous_cargo;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.print);
        init();
        fn_permission();
        listener();
        systemDate = new SimpleDateFormat("dd-MM-yyyy").format(new Date());
            tv_Printed_Date.setText(systemDate);
        String print_string=GlobalConstants.print_string;
        if(GlobalConstants.from.equalsIgnoreCase("GateOut")) {
            textView2.setText("GateOut");
        } else if(GlobalConstants.from.equalsIgnoreCase("GateIn")){
            textView2.setText("GateIn");
        }else {
            textView2.setText("Repair Estimation");
            ll_gulf.setVisibility(View.GONE);
            ll_gulf_one.setVisibility(View.GONE);
        }
            /*  print_object.put("get_sp_previous", get_sp_previous);
                    print_object.put("get_sp_customer", get_sp_customer);
                    print_object.put("get_sp_equipe", get_sp_equipe);
                    print_object.put("get_equipment", get_equipment);
                    print_object.put("getType", getType);
                    print_object.put("get_status", get_status);
                    print_object.put("get_code", get_code);
                    print_object.put("get_date", get_date);
                    print_object.put("get_time", get_time);
                    print_object.put("get_location", get_location);
                    print_object.put("get_eir_no", get_eir_no);
                    print_object.put("get_vechicle", get_vechicle);
                    print_object.put("get_transport", get_transport);
                    print_object.put("get_remark", get_remark);*/
        try {
            JSONObject jsonrootObject = new JSONObject(print_string);
            tv_Previous_Cargo.setText(jsonrootObject.getString("get_sp_previous"));
            tv_Equipment.setText(jsonrootObject.getString("get_equipment"));
            tv_Type.setText(jsonrootObject.getString("getType"));
            tv_Status.setText(jsonrootObject.getString("get_status"));
            tv_Code.setText(jsonrootObject.getString("get_code"));
            tv_In_Date.setText(jsonrootObject.getString("get_date"));
            tv_Time.setText(jsonrootObject.getString("get_time"));
            tv_Yard_Loc.setText(jsonrootObject.getString("get_location"));
            tv_EIR_No.setText(jsonrootObject.getString("get_eir_no"));
            tv_Vechicle_No.setText(jsonrootObject.getString("get_vechicle"));
            tv_Transporter.setText(jsonrootObject.getString("get_transport"));
            tv_Remarks.setText(jsonrootObject.getString("get_remark"));
            tv_Customer.setText(jsonrootObject.getString("get_sp_customer"));
//              print_object.put("get_createdBy", sp.getString(SP_USER_ID, "user_Id"));
//            print_object.put("get_current_date", systemDate);
            tv_Printed_By.setText(jsonrootObject.getString("get_createdBy"));
            tv_Printed_Date.setText(jsonrootObject.getString("get_current_date"));

        } catch (JSONException e) {
            e.printStackTrace();
        }

    }


    private void init(){


        btn_generate = (Button)findViewById(R.id.btn_generate);
        tv_Customer = (TextView)findViewById(R.id.tv_Customer);
        LL_previous_cargo = (LinearLayout)findViewById(R.id.LL_previous_cargo);
        ll_gulf_one = (LinearLayout)findViewById(R.id.ll_gulf_one);
        ll_gulf = (LinearLayout)findViewById(R.id.ll_gulf);
        tv_Equipment = (TextView)findViewById(R.id.tv_Equipment);
        tv_Type = (TextView)findViewById(R.id.tv_Type);
        tv_Code = (TextView)findViewById(R.id.tv_Code);
        tv_Status = (TextView)findViewById(R.id.tv_Status);
        textView2 = (TextView)findViewById(R.id.textView2);
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
        if(GlobalConstants.from.equalsIgnoreCase("GateOut"))
        {
            LL_previous_cargo.setVisibility(View.GONE);
        }
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
                        progressDialog = new ProgressDialog(CustomPrintActivity.this);
                        progressDialog.setMessage("Please wait");
                        bitmap = loadBitmapFromView(ll_pdflayout, ll_pdflayout.getWidth(), ll_pdflayout.getHeight());
                        createPdf();
//                        saveBitmap(bitmap);
                    } else {

                    }

                    createPdf();
                    break;
                }
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
        String targetPdf = "/sdcard/test.pdf";
        File filePath = new File(targetPdf);
        try {
            document.writeTo(new FileOutputStream(filePath));
            btn_generate.setText("Check PDF");
            Intent intent = new Intent();
            intent.setAction(android.content.Intent.ACTION_VIEW);
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

            if ((ActivityCompat.shouldShowRequestPermissionRationale(CustomPrintActivity.this, android.Manifest.permission.READ_EXTERNAL_STORAGE))) {
            } else {
                ActivityCompat.requestPermissions(CustomPrintActivity.this, new String[]{android.Manifest.permission.READ_EXTERNAL_STORAGE},
                        REQUEST_PERMISSIONS);

            }

            if ((ActivityCompat.shouldShowRequestPermissionRationale(CustomPrintActivity.this, Manifest.permission.WRITE_EXTERNAL_STORAGE))) {
            } else {
                ActivityCompat.requestPermissions(CustomPrintActivity.this, new String[]{Manifest.permission.WRITE_EXTERNAL_STORAGE},
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