package com.i_tankdepo;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.CustomerReportBean;
import com.i_tankdepo.Beanclass.GeneralReportBean;
import com.i_tankdepo.Beanclass.TypeReportBean;
import com.i_tankdepo.Constants.GlobalConstants;

import org.w3c.dom.Text;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

/**
 * Created by Admin on 1/4/2017.
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
public class GeneralReport extends CommonActivity {


    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu, im_down, im_up, im_heat_close, im_heat_ok, iv_back,iv_changeOfStatus;
    private TextView tv_date,tv_createdBy,tv_toolbarTitle,tv_heat_refresh;
    private Button bt_general,bt_customer,bt_type,bt_all,status_home,status_refresh,run_report;
    private LinearLayout LL_status_submit,LL_run;
    private GeneralReportBean generalReportBean;
    private ArrayList<GeneralReportBean> generalReportBeanArrayList;
    private TypeReportBean typeReportBean;
    private ArrayList<CustomerReportBean> customerReportbeanArrayList;
    private ArrayList<TypeReportBean> typeReportBeanArrayList;
    private ListView report_listView;
    private ViewHolder holder;
    private UserListAdapter adapter;
    private String curTime;
    private String systemDate;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.reports);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        menu.setVisibility(View.GONE);

        generalReportBeanArrayList= GlobalConstants.generalReportBeanArrayList;
        customerReportbeanArrayList= GlobalConstants.customerReportbeanArrayList;
        typeReportBeanArrayList= GlobalConstants.typeReportBeanArrayList;
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);

        report_listView=(ListView)findViewById(R.id.report_listView);

        adapter = new UserListAdapter(GeneralReport.this, R.layout.general_report_listview, generalReportBeanArrayList);
        report_listView.setAdapter(adapter);
        tv_toolbarTitle.setText("Reports");
        tv_heat_refresh  = (TextView)findViewById(R.id.tv_heat_refresh);
        tv_heat_refresh.setText("Reset");


        tv_date  = (TextView)findViewById(R.id.tv_date);
        tv_createdBy  = (TextView)findViewById(R.id.tv_createdBy);
        SimpleDateFormat time = new SimpleDateFormat("hh:mm");
        curTime = time.format(new Date());
        systemDate = new SimpleDateFormat("yyyy-MM-dd").format(new Date());
        tv_date.setText(systemDate);
        tv_createdBy.setText(sp.getString(SP_USER_ID,"user_Id"));
        bt_general = (Button)findViewById(R.id.bt_general);
        bt_general.setBackgroundColor(getResources().getColor(R.color.toolbar));
        bt_general.setTextColor(getResources().getColor(R.color.Title));
        bt_customer = (Button)findViewById(R.id.bt_customer);
        bt_type = (Button)findViewById(R.id.bt_type);
        bt_all = (Button)findViewById(R.id.bt_all);
        status_home = (Button)findViewById(R.id.status_home);
        status_refresh = (Button)findViewById(R.id.status_refresh);
        run_report = (Button)findViewById(R.id.run_report);
        LL_status_submit = (LinearLayout)findViewById(R.id.LL_status_submit);
        LL_run = (LinearLayout)findViewById(R.id.LL_run);
        LL_run.setAlpha(0.5f);
        LL_run.setClickable(false);
        LL_status_submit.setVisibility(View.GONE);

        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);



        iv_changeOfStatus.setOnClickListener(this);
        bt_general.setOnClickListener(this);
        bt_customer.setOnClickListener(this);
        bt_type.setOnClickListener(this);
        bt_all.setOnClickListener(this);
        status_home.setOnClickListener(this);
        status_refresh.setOnClickListener(this);
        iv_back.setOnClickListener(this);

    }

    @Override
    public void onClick(View v) {
        switch (v.getId()){
            case R.id.iv_back:

                finish();
                onBackPressed();
                break;
            case R.id.iv_changeOfStatus:
                startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));
                break;
            case R.id.status_home:
                startActivity(new Intent(getApplicationContext(),MainActivity.class));
                break;
            case R.id.status_refresh:
                finish();
                startActivity(getIntent());
                break;
            case R.id.bt_type:
                GlobalConstants.typeReportBeanArrayList=typeReportBeanArrayList;
                GlobalConstants.generalReportBeanArrayList=generalReportBeanArrayList;
                finish();
                startActivity(new Intent(getApplicationContext(),TypeReport.class));
                break;
            case R.id.bt_customer:

                GlobalConstants.customerReportbeanArrayList=customerReportbeanArrayList;
                GlobalConstants.generalReportBeanArrayList=generalReportBeanArrayList;
                finish();
                startActivity(new Intent(getApplicationContext(),CustomerReport.class));
                break;
            case R.id.bt_all:

                GlobalConstants.generalReportBeanArrayList=generalReportBeanArrayList;
                GlobalConstants.customerReportbeanArrayList=customerReportbeanArrayList;
                GlobalConstants.generalReportBeanArrayList=generalReportBeanArrayList;
                finish();
                startActivity(new Intent(getApplicationContext(),AllReport.class));
                break;

        }

    }
    public class UserListAdapter extends BaseAdapter {


        private ArrayList<GeneralReportBean> list;
        Context context;

        int resource;
        private GeneralReportBean userListBean;
        int lastPosition = -1;
        public UserListAdapter(Context context, int resource, ArrayList<GeneralReportBean> list) {
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
                holder = new ViewHolder();


                holder.whole = (LinearLayout) convertView.findViewById(R.id.LL_whole);
                holder.dept = (TextView) convertView.findViewById(R.id.tv_depot);
                holder.Cust_Name = (TextView) convertView.findViewById(R.id.tv_cust_name);
                holder.equip_no = (TextView) convertView.findViewById(R.id.tv_equip_no);
                holder.type = (TextView) convertView.findViewById(R.id.tv_type);
                holder.indate= (TextView) convertView.findViewById(R.id.tv_inDate);
                holder.previous_crg = (TextView) convertView.findViewById(R.id.tv_preCargo);
                holder.Eir_no = (TextView) convertView.findViewById(R.id.tv_eirNo);
                holder.cleaning_cer_no = (TextView) convertView.findViewById(R.id.tv_cleaningCertNum);
                holder.cleaning_date = (TextView) convertView.findViewById(R.id.tv_cleaningDate);
                holder.inspection_date = (TextView) convertView.findViewById(R.id.tv_inspectionDate);
                holder.current_status = (TextView) convertView.findViewById(R.id.tv_currentStatus);
                holder.current_status_date = (TextView) convertView.findViewById(R.id.tv_currentStatusDate);
                holder.Next_test_date = (TextView) convertView.findViewById(R.id.tv_nextTestDate);
                holder.Next_test_type = (TextView) convertView.findViewById(R.id.tv_nextTestType);
                holder.remark = (TextView) convertView.findViewById(R.id.tv_remark);



                // R.id.tv_customerName,R.id.tv_Inv_no,R.id.tv_date,R.id.tv_val,R.id.tv_due
                convertView.setTag(holder);
            } else {
                holder = (ViewHolder) convertView.getTag();
            }
            if (list.size() < 1){
                Toast.makeText(getApplicationContext(), "NO DATA FOUND", Toast.LENGTH_LONG).show();
            }else {

                userListBean = list.get(position);
                String[] parts = userListBean.getCurrentStatusDate().split(" ");
                String[] parts1 = userListBean.getCleaningDate().split(" ");
                String[] parts2 = userListBean.getInspectionDate().split(" ");
                String[] parts3 = userListBean.getNextTestDate().split(" ");
                String[] parts4 = userListBean.getIndate().split(" ");
                String current_date = parts[0];
                String cleaning_date = parts1[0];
                String inspection_date = parts2[0];
                String next_test_date = parts3[0];
                String In_date = parts4[0];

                holder.equip_no.setText(userListBean.getEquipmentNo() );
                holder.Cust_Name.setText(userListBean.getCustomer());

                holder.previous_crg.setText(userListBean.getPreviousCargo());
               // holder.Eir_no.setText(userListBean.getEirNo());
                holder.remark.setText(userListBean.getRemarks());
                holder.type.setText(userListBean.getType());
                holder.cleaning_cer_no.setText(userListBean.getCleaningCertNo());
                holder.cleaning_date.setText(cleaning_date);
                holder.current_status_date.setText(current_date);
                holder.Next_test_date.setText(next_test_date);
                holder.Next_test_type.setText(userListBean.getNextTestType());
                holder.current_status.setText(userListBean.getCurrentStatus());
                holder.inspection_date.setText(inspection_date);
                holder.indate.setText(In_date);
                holder.dept.setText(userListBean.getDepot());



            }
            return convertView;
        }



    }
    static class ViewHolder {
        TextView equip_no,current_status_date,dept ,Cust_Name,previous_crg,attachmentstatus,current_status,Next_test_date,Next_test_type,
                Eir_no,indate,cleaning_cer_no,remark,cleaning_date,inspection_date,type;

        LinearLayout whole,LL_username;
    }
    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

    }
}
