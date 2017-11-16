package com.i_tankdepo;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.ScrollView;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.CustomerReportBean;
import com.i_tankdepo.Beanclass.GeneralReportBean;
import com.i_tankdepo.Beanclass.TypeReportBean;
import com.i_tankdepo.Constants.GlobalConstants;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

/**
 * Created by Admin on 1/4/2017.
 */

public class AllReport extends CommonActivity {


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
    private ListView report_listView,report_listView1,report_listView2;
    private ViewHolder holder;
    private UserListAdapter adapter;
    private UserListAdapter_Customer customeradapter;
    private UserListAdapter_Type type_adapter;
    private ScrollView scrollview;
    private String curTime;
    private String systemDate;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.reports_all);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        scrollview=(ScrollView)findViewById(R.id.report_Scroll);
      //  scrollview.fullScroll(ScrollView.FOCUS_UP);
        menu.setVisibility(View.GONE);
        generalReportBeanArrayList= GlobalConstants.generalReportBeanArrayList;
        customerReportbeanArrayList= GlobalConstants.customerReportbeanArrayList;
        typeReportBeanArrayList= GlobalConstants.typeReportBeanArrayList;

        report_listView=(ListView)findViewById(R.id.report_listView);
        report_listView1=(ListView)findViewById(R.id.report_listView1);
        report_listView2=(ListView)findViewById(R.id.report_listView2);

        report_listView.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                v.getParent().requestDisallowInterceptTouchEvent(true);
                return false;
            }
        });

        report_listView.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {

// Disallow the touch request for parent scroll on touch of child view
                scrollview.requestDisallowInterceptTouchEvent(true);

                int action = event.getActionMasked();
                switch (action) {
                    case MotionEvent.ACTION_UP:
                        scrollview.requestDisallowInterceptTouchEvent(false);
                        break;
                }
                return false;
            }
        });
        adapter = new UserListAdapter(AllReport.this, R.layout.general_report_listview, generalReportBeanArrayList);
        report_listView.setAdapter(adapter);

        customeradapter = new UserListAdapter_Customer(AllReport.this, R.layout.customer_report_listview, customerReportbeanArrayList);
        report_listView1.setAdapter(customeradapter);

        type_adapter = new UserListAdapter_Type(AllReport.this, R.layout.customer_report_listview, typeReportBeanArrayList);
        report_listView2.setAdapter(type_adapter);

        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Reports");

        tv_date  = (TextView)findViewById(R.id.tv_date);
        tv_createdBy  = (TextView)findViewById(R.id.tv_createdBy);
        tv_heat_refresh  = (TextView)findViewById(R.id.tv_heat_refresh);
        tv_heat_refresh.setText("Reset");
        SimpleDateFormat time = new SimpleDateFormat("hh:mm");
        curTime = time.format(new Date());
        systemDate = new SimpleDateFormat("yyyy-MM-dd").format(new Date());
        tv_date.setText(systemDate);
        tv_createdBy.setText(sp.getString(SP_USER_ID,"user_Id"));
        bt_general = (Button)findViewById(R.id.bt_general);
        bt_customer = (Button)findViewById(R.id.bt_customer);
        bt_type = (Button)findViewById(R.id.bt_type);
        bt_all = (Button)findViewById(R.id.bt_all);

        bt_all.setBackgroundColor(getResources().getColor(R.color.toolbar));
        bt_all.setTextColor(getResources().getColor(R.color.Title));

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
                finish();
                startActivity(new Intent(getApplicationContext(),TypeReport.class));
                break;
            case R.id.bt_general:
                finish();
                startActivity(new Intent(getApplicationContext(),GeneralReport.class));
                break;
            case R.id.bt_customer:
                finish();
                startActivity(new Intent(getApplicationContext(),CustomerReport.class));
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

                holder.equip_no.setText(userListBean.getEquipmentNo());
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
    public class UserListAdapter_Type extends BaseAdapter {


        private ArrayList<TypeReportBean> list;
        Context context;

        int resource;
        private TypeReportBean userListBean;
        int lastPosition = -1;
        public UserListAdapter_Type(Context context, int resource, ArrayList<TypeReportBean> list) {
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
               /*tv_ind,tv_phl,tv_acn,tv_clnandawe,tv_awe,tv_aar,
                        tv_aur, tv_asr,tv_srv,tv_cln,tv_ins,tv_insandrpc
                tv_rpc,tv_sto,tv_avl,tv_out */

                holder.whole = (LinearLayout) convertView.findViewById(R.id.LL_whole);
                holder.tv_ind = (TextView) convertView.findViewById(R.id.tv_ind);
                holder.cust_name = (TextView) convertView.findViewById(R.id.tv_cust_name);
                holder.total = (TextView) convertView.findViewById(R.id.tv_value);
                holder.tv_phl = (TextView) convertView.findViewById(R.id.tv_phl);
                holder.tv_acn = (TextView) convertView.findViewById(R.id.tv_acn);
                holder.tv_clnandawe = (TextView) convertView.findViewById(R.id.tv_clnandawe);
                holder.tv_awe= (TextView) convertView.findViewById(R.id.tv_awe);
                holder.tv_aar = (TextView) convertView.findViewById(R.id.tv_aar);
                holder.tv_aur = (TextView) convertView.findViewById(R.id.tv_aur);
                holder.tv_asr = (TextView) convertView.findViewById(R.id.tv_asr);
                holder.tv_srv = (TextView) convertView.findViewById(R.id.tv_srv);
                holder.tv_cln = (TextView) convertView.findViewById(R.id.tv_cln);
                holder.tv_ins = (TextView) convertView.findViewById(R.id.tv_ins);
                holder.tv_insandrpc = (TextView) convertView.findViewById(R.id.tv_insandrpc);
                holder.tv_rpc = (TextView) convertView.findViewById(R.id.tv_rpc);
                holder.tv_sto = (TextView) convertView.findViewById(R.id.tv_sto);
                holder.tv_avl = (TextView) convertView.findViewById(R.id.tv_avl);
                holder.tv_out = (TextView) convertView.findViewById(R.id.tv_out);



                // R.id.tv_customerName,R.id.tv_Inv_no,R.id.tv_date,R.id.tv_val,R.id.tv_due
                convertView.setTag(holder);
            } else {
                holder = (ViewHolder) convertView.getTag();
            }
            if (list.size() < 1){
                Toast.makeText(getApplicationContext(), "NO DATA FOUND", Toast.LENGTH_LONG).show();
            }else {


                userListBean = list.get(position);
                holder.tv_ind.setText(userListBean.getIND() );
                holder.tv_aar.setText(userListBean.getAAR());
                holder.cust_name.setText(userListBean.getType() );
                holder.total.setText(userListBean.getTOTAL());
                holder.tv_acn.setText(userListBean.getACN());
                holder.tv_insandrpc.setText(userListBean.getINSRPC());
                holder.tv_asr.setText(userListBean.getASR());
                holder.tv_avl.setText(userListBean.getAVL());
                holder.tv_awe.setText(userListBean.getAWE());
                holder.tv_cln.setText(userListBean.getAWECLN());
                holder.tv_clnandawe.setText(userListBean.getAVLCLN());
                holder.tv_ins.setText(userListBean.getAVLINS());
                holder.tv_out.setText(userListBean.getOUT());
                holder.tv_phl.setText(userListBean.getPHL());
                holder.tv_rpc.setText(userListBean.getRPC());
                holder.tv_srv.setText(userListBean.getSRV());
                holder.tv_sto.setText(userListBean.getSTO());


            }
            return convertView;
        }



    }

    public class UserListAdapter_Customer extends BaseAdapter {


        private ArrayList<CustomerReportBean> list;
        Context context;

        int resource;
        private CustomerReportBean userListBean;
        int lastPosition = -1;
        public UserListAdapter_Customer(Context context, int resource, ArrayList<CustomerReportBean> list) {
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
               /*tv_ind,tv_phl,tv_acn,tv_clnandawe,tv_awe,tv_aar,
                        tv_aur, tv_asr,tv_srv,tv_cln,tv_ins,tv_insandrpc
                tv_rpc,tv_sto,tv_avl,tv_out */

                holder.whole = (LinearLayout) convertView.findViewById(R.id.LL_whole);
                holder.ind = (TextView) convertView.findViewById(R.id.tv_ind);
                holder.cust_name = (TextView) convertView.findViewById(R.id.tv_cust_name);
                holder.total = (TextView) convertView.findViewById(R.id.tv_value);
                holder.phl = (TextView) convertView.findViewById(R.id.tv_phl);
                holder.acn = (TextView) convertView.findViewById(R.id.tv_acn);
                holder.clnandawe = (TextView) convertView.findViewById(R.id.tv_clnandawe);
                holder.awe= (TextView) convertView.findViewById(R.id.tv_awe);
                holder.aar = (TextView) convertView.findViewById(R.id.tv_aar);
                holder.aur = (TextView) convertView.findViewById(R.id.tv_aur);
                holder.asr = (TextView) convertView.findViewById(R.id.tv_asr);
                holder.srv = (TextView) convertView.findViewById(R.id.tv_srv);
                holder.cln = (TextView) convertView.findViewById(R.id.tv_cln);
                holder.ins = (TextView) convertView.findViewById(R.id.tv_ins);
                holder.insandrpc = (TextView) convertView.findViewById(R.id.tv_insandrpc);
                holder.rpc = (TextView) convertView.findViewById(R.id.tv_rpc);
                holder.sto = (TextView) convertView.findViewById(R.id.tv_sto);
                holder.avl = (TextView) convertView.findViewById(R.id.tv_avl);
                holder.out = (TextView) convertView.findViewById(R.id.tv_out);



                // R.id.tv_customerName,R.id.tv_Inv_no,R.id.tv_date,R.id.tv_val,R.id.tv_due
                convertView.setTag(holder);
            } else {
                holder = (ViewHolder) convertView.getTag();
            }
            if (list.size() < 1){
                Toast.makeText(getApplicationContext(), "NO DATA FOUND", Toast.LENGTH_LONG).show();
            }else {


                userListBean = list.get(position);
                holder.ind.setText(userListBean.getIND() );
                holder.cust_name.setText(userListBean.getCustomer() );
                holder.total.setText(userListBean.getTOTAL());
                holder.aar.setText(userListBean.getAAR());
                holder.acn.setText(userListBean.getACN());
                holder.insandrpc.setText(userListBean.getINSRPC());
                holder.asr.setText(userListBean.getASR());
                holder.avl.setText(userListBean.getAVL());
                holder.awe.setText(userListBean.getAWE());
                holder.cln.setText(userListBean.getAWECLN());
                holder.clnandawe.setText(userListBean.getAVLCLN());
                holder.ins.setText(userListBean.getAVLINS());
                holder.out.setText(userListBean.getOUT());
                holder.phl.setText(userListBean.getPHL());
                holder.rpc.setText(userListBean.getRPC());
                holder.srv.setText(userListBean.getSRV());
                holder.sto.setText(userListBean.getSTO());


            }
            return convertView;
        }



    }
    static class ViewHolder {
        TextView equip_no,current_status_date,dept ,Cust_Name,previous_crg,attachmentstatus,current_status,Next_test_date,Next_test_type,
                Eir_no,indate,cleaning_cer_no,remark,cleaning_date,inspection_date,type;
        TextView ind,phl,acn,clnandawe,awe,aar,
                aur, asr,srv,cln,ins,insandrpc,rpc,sto,avl,out ;
        TextView cust_name,total,tv_ind,tv_phl,tv_acn,tv_clnandawe,tv_awe,tv_aar,
                tv_aur, tv_asr,tv_srv,tv_cln,tv_ins,tv_insandrpc,tv_rpc,tv_sto,tv_avl,tv_out ;
        LinearLayout whole,LL_username;
    }
    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

    }
}
