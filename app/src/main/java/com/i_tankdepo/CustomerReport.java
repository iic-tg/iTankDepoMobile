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

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

/**
 * Created by Admin on 1/4/2017.
 */

public class CustomerReport extends CommonActivity {


    private DrawerLayout drawer;
    private Toolbar toolbar;
    private ImageView menu, im_down, im_up, im_heat_close, im_heat_ok, iv_back,iv_changeOfStatus;
    private TextView tv_date,tv_createdBy,tv_toolbarTitle,tv_heat_refresh;
    private Button bt_general,bt_customer,bt_type,bt_all,status_home,status_refresh,run_report;
    private LinearLayout LL_status_submit,LL_run;
    private ArrayList<CustomerReportBean> customerReportbeanArrayList;
    private ArrayList<TypeReportBean> typeReportBeanArrayList;
    private ArrayList<GeneralReportBean> generalReportBeanArrayList;
    private ListView report_listView;
    private UserListAdapter adapter;
    private ViewHolder holder;
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

        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Reports");
        customerReportbeanArrayList= GlobalConstants.customerReportbeanArrayList;
        typeReportBeanArrayList= GlobalConstants.typeReportBeanArrayList;
        generalReportBeanArrayList= GlobalConstants.generalReportBeanArrayList;
        report_listView=(ListView)findViewById(R.id.report_listView);
        adapter = new UserListAdapter(CustomerReport.this, R.layout.customer_report_listview, customerReportbeanArrayList);
        report_listView.setAdapter(adapter);
        tv_date  = (TextView)findViewById(R.id.tv_date);
        tv_createdBy  = (TextView)findViewById(R.id.tv_createdBy);
        tv_heat_refresh  = (TextView)findViewById(R.id.tv_heat_refresh);
        tv_heat_refresh.setText("Reset");

        bt_general = (Button)findViewById(R.id.bt_general);
        bt_customer = (Button)findViewById(R.id.bt_customer);
        bt_customer.setBackgroundColor(getResources().getColor(R.color.toolbar));
        bt_customer.setTextColor(getResources().getColor(R.color.Title));
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
        SimpleDateFormat time = new SimpleDateFormat("hh:mm");
        curTime = time.format(new Date());
        systemDate = new SimpleDateFormat("yyyy-MM-dd").format(new Date());
        tv_date.setText(systemDate);
        tv_createdBy.setText(sp.getString(SP_USER_ID,"user_Id"));
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
            case R.id.bt_general:
                GlobalConstants.typeReportBeanArrayList=typeReportBeanArrayList;
                GlobalConstants.generalReportBeanArrayList=generalReportBeanArrayList;
                finish();
                startActivity(new Intent(getApplicationContext(),GeneralReport.class));
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


        private ArrayList<CustomerReportBean> list;
        Context context;

        int resource;
        private CustomerReportBean userListBean;
        int lastPosition = -1;
        public UserListAdapter(Context context, int resource, ArrayList<CustomerReportBean> list) {
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
                holder.ind.setText(userListBean.getIND());
                holder.cust_name.setText(userListBean.getCustomer());
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
        TextView total,cust_name,ind,phl,acn,clnandawe,awe,aar,
               aur, asr,srv,cln,ins,insandrpc,rpc,sto,avl,out ;

        LinearLayout whole,LL_username;
    }
    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

    }
}
