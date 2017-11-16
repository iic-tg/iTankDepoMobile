package com.i_tankdepo;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.i_tankdepo.Beanclass.LeakTestBean;
import com.i_tankdepo.Constants.ConstantValues;
import com.i_tankdepo.Constants.GlobalConstants;
import com.i_tankdepo.helper.ServiceHandler;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpParams;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.util.ArrayList;

/**
 * Created by Metaplore on 12/27/2016.
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
public class RevisionNo extends CommonActivity {
    private ImageView menu,iv_back,add_new_heating,im_testDate;
    private TextView tv_toolbarTitle,leakTest_text,tv_heat_refresh,text1,text2;
    private Button heat_home,heat_refresh,bt_heating,cleaning,inspection,leakTest,heat_submit,bt_revisionNo;
    private LinearLayout LL_heat_submit,LL_heat;
    private RelativeLayout RL_heating,RL_Repair;
    private ViewHolder holder;
    private ProgressDialog progressDialog;
    private String getTestDate,getreliefvalue1,getreliefvalue2,getpress_guage1,getpress_guage2,getReamrk,get_switch_shellTest,get_switch_steam;
    private ArrayList<LeakTestBean> leakTest_arraylist = new ArrayList<>();
    private ListView revision_list_view;
    private LeakTestBean leakTest_bean;
    String Equip_NO,Cust_Name,remark,pressureGauge1,pressureGauge2,relief_value1,Type,
            NoofTimesGenerated,shellTest,TestDate,relief_value2,steamTubeTest,current_status,Gi_trans_no;
    private UserListAdapter adapter;
    private ImageView iv_changeOfStatus;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.revision_history);
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        menu = (ImageView) findViewById(R.id.iv_menu);
        iv_back = (ImageView) findViewById(R.id.iv_back);
        add_new_heating = (ImageView) findViewById(R.id.im_add);
        menu.setVisibility(View.GONE);
        tv_toolbarTitle = (TextView) findViewById(R.id.tv_Title);
        tv_toolbarTitle.setText("Revision History");

        heat_home = (Button)findViewById(R.id.heat_home);
        heat_refresh = (Button)findViewById(R.id.heat_refresh);

        revision_list_view = (ListView)findViewById(R.id.revision_list_view);

        bt_heating = (Button)findViewById(R.id.heating);
        bt_heating.setVisibility(View.GONE);
        cleaning = (Button)findViewById(R.id.cleaning);
        inspection = (Button)findViewById(R.id.inspection);
        LL_heat = (LinearLayout)findViewById(R.id.LL_heat);
        inspection.setVisibility(View.GONE);
        cleaning.setVisibility(View.GONE);
        LL_heat.setAlpha(0.5f);
        LL_heat.setClickable(false);
        leakTest_text = (TextView)findViewById(R.id.tv_heating);
        leakTest_text.setText("Add New");
        LL_heat_submit = (LinearLayout)findViewById(R.id.LL_heat_submit);
        LL_heat_submit.setAlpha(0.5f);
        LL_heat_submit.setClickable(false);
        heat_submit = (Button) findViewById(R.id.heat_submit);

        tv_heat_refresh = (TextView)findViewById(R.id.tv_heat_refresh);
        tv_heat_refresh.setText("Reset");

        RL_heating =(RelativeLayout)findViewById(R.id.RL_heating);
        RL_Repair =(RelativeLayout)findViewById(R.id.RL_Repair);
        RL_Repair.setVisibility(View.GONE);
        iv_changeOfStatus = (ImageView)findViewById(R.id.iv_changeOfStatus);
        iv_changeOfStatus.setOnClickListener(this);

        text1 = (TextView)findViewById(R.id.text1);
        text2 = (TextView)findViewById(R.id.text2);

        heat_home.setOnClickListener(this);
        heat_refresh.setOnClickListener(this);
        iv_back.setOnClickListener(this);


        Cust_Name = GlobalConstants.customer_name;
        Equip_NO = GlobalConstants.equipment_no;
        Type = GlobalConstants.type;
        NoofTimesGenerated = GlobalConstants.no_of_tms_granted;
        TestDate = GlobalConstants.TestDate;

        relief_value1 = GlobalConstants.RLFVLV1;
        relief_value2 = GlobalConstants.RLFVLV2;
        pressureGauge1 = GlobalConstants.PG1;
        pressureGauge2 = GlobalConstants.PG2;
        remark = GlobalConstants.remark;
        shellTest = GlobalConstants.SHLL_TST_BT;
        steamTubeTest = GlobalConstants.STM_TB_TST_BT;
        current_status = GlobalConstants.EQPMNT_STTS_CD;
        Gi_trans_no = GlobalConstants.gt_transaction_no;

        text1.setText(Cust_Name+" , "+Equip_NO+" , "+Type);
        text2.setText("No of Times Generated = " + NoofTimesGenerated);

        if(cd.isConnectingToInternet()){
            new Get_Revision_Number_details().execute();
        }else{
            shortToast(getApplicationContext(),"Please check your Internet Connection..!");
        }

    }

    @Override
    public void onClick(View view) {
    switch (view.getId()){
        case R.id.iv_back:
            finish();
            onBackPressed();
            break;
        case R.id.heat_home:
            startActivity(new Intent(getApplicationContext(),MainActivity.class));
            break;
        case R.id.heat_refresh:
            finish();
            startActivity(getIntent());
            break;
        case R.id.iv_changeOfStatus:
            startActivity(new Intent(getApplicationContext(),ChangeOfStatus.class));
            break;
    }
    }

    @Override
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

    }

    public class Get_Revision_Number_details extends AsyncTask<Void, Void, Void> {
        private JSONArray jsonarray;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(RevisionNo.this);
            progressDialog.setMessage("Please Wait...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(false);
//            if ((progressDialog != null) && progressDialog.isShowing()) {
            progressDialog.show();
//            }
        }

        @Override
        protected Void doInBackground(Void... params) {

            ServiceHandler sh = new ServiceHandler();
            HttpParams httpParameters = new BasicHttpParams();
            DefaultHttpClient httpClient = new DefaultHttpClient(httpParameters);
            HttpEntity httpEntity = null;
            HttpResponse response = null;
            HttpPost httpPost = new HttpPost(ConstantValues.baseURLLeakTestRevisionNo);

            httpPost.setHeader("Content-Type", "application/json");


            try {
                JSONObject jsonObject = new JSONObject();

                jsonObject.put("UserName", sp.getString(SP_USER_ID, "user_Id"));
                jsonObject.put("equipmentNo",Equip_NO);
                jsonObject.put("GITransactionNo",Gi_trans_no);

                StringEntity stringEntity = new StringEntity(jsonObject.toString());
                httpPost.setEntity(stringEntity);
                response = httpClient.execute(httpPost);
                httpEntity = response.getEntity();
                String resp = EntityUtils.toString(httpEntity);

                Log.d("rep", resp);
                JSONObject jsonrootObject = new JSONObject(resp);
                JSONObject getJsonObject = jsonrootObject.getJSONObject("d");


                jsonarray = getJsonObject.getJSONArray("arrayOfLTM");
                if (jsonarray != null) {

                    System.out.println("Am HashMap list" + jsonarray);
                    if (jsonarray.length() < 1) {
                        runOnUiThread(new Runnable() {
                            public void run() {
//                        longToast("This takes longer than usual time. Connection Timeout !");
                                shortToast(getApplicationContext(), "No Records Found");
                            }
                        });
                    } else {

                        leakTest_arraylist = new ArrayList<LeakTestBean>();


                        for (int i = 0; i < jsonarray.length(); i++) {

                            leakTest_bean = new LeakTestBean();
                            jsonObject = jsonarray.getJSONObject(i);

                            leakTest_bean.setLK_TST_ID(jsonObject.getString("LK_TST_ID"));
                            leakTest_bean.setEQPMNT_NO(jsonObject.getString("EQPMNT_NO"));
                            leakTest_bean.setGI_TRNSCTN_NO(jsonObject.getString("GI_TRNSCTN_NO"));
                            leakTest_bean.setTST_DT(jsonObject.getString("TST_DT"));
                            leakTest_bean.setRLF_VLV_SRL_1(jsonObject.getString("RLF_VLV_SRL_1"));
                            leakTest_bean.setRLF_VLV_SRL_2(jsonObject.getString("RLF_VLV_SRL_2"));
                            leakTest_bean.setPG_1(jsonObject.getString("PG_1"));
                            leakTest_bean.setPG_2(jsonObject.getString("PG_2"));
                            leakTest_bean.setRVSN_NO(jsonObject.getString("RVSN_NO"));
                            leakTest_bean.setLST_GNRTD_BY(jsonObject.getString("LST_GNRTD_BY"));
                            leakTest_bean.setLST_GNRTD_DT(jsonObject.getString("LST_GNRTD_DT"));
                            leakTest_bean.setLTST_RPRT_NO(jsonObject.getString("LTST_RPRT_NO"));
                            leakTest_bean.setSHLL_TST_BT(jsonObject.getString("SHLL_TST_BT"));
                            leakTest_bean.setSTM_TB_TST_BT(jsonObject.getString("STM_TB_TST_BT"));
                            leakTest_bean.setRMRKS_VC(jsonObject.getString("RMRKS_VC"));


                            leakTest_arraylist.add(leakTest_bean);


                        }
                    }
                } else if (jsonarray.length() < 1) {
                    runOnUiThread(new Runnable() {

                        @Override
                        public void run() {
                            //update ui here
                            // display toast here
                            shortToast(getApplicationContext(), "No Records Found");


                        }
                    });

                }

            } catch (JSONException e) {
                e.printStackTrace();
            } catch (ClientProtocolException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }


            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {


//            if ((progressDialog != null) && progressDialog.isShowing()) {
            progressDialog.dismiss();
//            }
            if (leakTest_arraylist != null) {
                adapter = new UserListAdapter(RevisionNo.this, R.layout.revision_list_item_row, leakTest_arraylist);
                revision_list_view.setAdapter(adapter);




            } else if (leakTest_arraylist.size() < 1) {
                shortToast(getApplicationContext(), "Data Not Found");
            }

        }

    }

    public class UserListAdapter extends BaseAdapter {
        private final ArrayList<LeakTestBean> arraylist;
        Context context;
        ArrayList<LeakTestBean> list = new ArrayList<>();
        int resource;
        private LeakTestBean userListBean;
        int lastPosition = -1;

        public UserListAdapter(Context context, int resource, ArrayList<LeakTestBean> list) {
            this.context = context;
            this.list = list;
            this.resource = resource;
            this.arraylist = new ArrayList<LeakTestBean>();
            this.arraylist.addAll(list);
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
//                holder.Cust_Name = (TextView) convertView.findViewById(R.id.text1);
//                holder.equip_no = (TextView) convertView.findViewById(R.id.text2);
                holder.testBit = (TextView) convertView.findViewById(R.id.tv_shellTest);
                holder.testDate = (TextView) convertView.findViewById(R.id.tv_testDate);
                holder.revisionNO = (TextView) convertView.findViewById(R.id.tv_RevNo);
                holder.STM_TB_TST_BT = (TextView) convertView.findViewById(R.id.tv_steamTubeTest);
                holder.RLFVLV1 = (TextView) convertView.findViewById(R.id.tv_reliefValue1);
                holder.RLFVLV2 = (TextView) convertView.findViewById(R.id.tv_reliefValue2);
                holder.PG1 = (TextView) convertView.findViewById(R.id.tv_pressureGauge1);
                holder.PG2 = (TextView) convertView.findViewById(R.id.tv_pressureGauge2);
                holder.listGrantedBy = (TextView) convertView.findViewById(R.id.tv_last_gene_by);
                holder.listGrantedDate = (TextView) convertView.findViewById(R.id.tv_last_gene_date);
                holder.listReportNo = (TextView) convertView.findViewById(R.id.tv_latest_report_No);
                holder.RMRKS_VC = (TextView) convertView.findViewById(R.id.tv_remark);


                convertView.setTag(holder);
            } else {
                holder = (ViewHolder) convertView.getTag();
            }
            if (list.size() < 1) {
                Toast.makeText(getApplicationContext(), "NO DATA FOUND", Toast.LENGTH_LONG).show();
            } else {
                userListBean = list.get(position);
                String[] parts = userListBean.getTST_DT().split(" ");
               String part1_date = parts[0];
//                String part1_time = parts[1];
//                System.out.println("from date after split" + part1_date);
//                holder.equip_no.setText(userListBean.getEQPMNT_NO());
//                holder.leaktestID.setText(userListBean.getLK_TST_ID());
//                holder.transactionNO.setText(userListBean.getGI_TRNSCTN_NO());
                holder.testDate.setText(part1_date);
                holder.RLFVLV1.setText(userListBean.getRLF_VLV_SRL_1());
                holder.RLFVLV2.setText(userListBean.getRLF_VLV_SRL_2());
                holder.PG1.setText(userListBean.getPG_1());
                holder.PG2.setText(userListBean.getPG_2());
                holder.revisionNO.setText(userListBean.getRVSN_NO());
                holder.listGrantedBy.setText(userListBean.getLST_GNRTD_BY());
                holder.listGrantedDate.setText(userListBean.getLST_GNRTD_DT());
                holder.listReportNo.setText(userListBean.getLTST_RPRT_NO());
                holder.testBit.setText(userListBean.getSHLL_TST_BT());
                holder.STM_TB_TST_BT.setText(userListBean.getSTM_TB_TST_BT());
                holder.RMRKS_VC.setText(userListBean.getRMRKS_VC());

                if(userListBean.getSHLL_TST_BT().equalsIgnoreCase("True")){
                    holder.testBit.setText("Yes");
                }else{
                    holder.testBit.setText("No");
                }

                if(userListBean.getSTM_TB_TST_BT().equalsIgnoreCase("True")){
                    holder.STM_TB_TST_BT.setText("Yes");
                }else{
                    holder.STM_TB_TST_BT.setText("No");
                }

                holder.revisionNO.setText("Rev No : " + userListBean.getRVSN_NO());

/*
                if(userListBean.getVechicle().equals("")||userListBean.getVechicle()==null)
                {
                    holder.PG2.setText("");
                }
               else{
                    holder.PG2.setText(userListBean.getVechicle());
                }*/

            }
            return convertView;
        }


    }

    static class ViewHolder {
        TextView equip_no, previous_cargo,date, Cust_Name, leaktestID, transactionNO, testDate, RLFVLV1, RLFVLV2, pre_id, testBit, cust_code, type_id, code_id,
                PG2, PG1, estimateNo, noofTimesGranted, listGrantedDate, revisionNO, listReportNo, listGrantedBy, type,CRTD_DT,
                pressureGauge1,pressureGauge2,CRTD_BY,ACTV_BT,RMRKS_VC,STM_TB_TST_BT,remark,latest_report_No,last_gene_date,last_gene_by,reliefValue1,reliefValue2,steamTubeTest,shellTest;
        CheckBox checkBox;

        LinearLayout whole;
    }



}
