package com.i_tankdepo.adapter;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.i_tankdepo.Beanclass.Multi_Photo_Bean;
import com.i_tankdepo.R;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Metaplore on 8/2/2017.
 */
public class VerticalAdapter extends RecyclerView.Adapter<VerticalAdapter.MyViewHolder> {

    private ArrayList<Multi_Photo_Bean> verticalList;

    public class MyViewHolder extends RecyclerView.ViewHolder {
        public TextView Cust_Name,time,attachment_id,image_position;
        ImageView im_delete;
        public MyViewHolder(View view) {
            super(view);
            LinearLayout whole = (LinearLayout) view.findViewById(R.id.LL_whole);
            Cust_Name = (TextView) view.findViewById(R.id.image_name);
            time = (TextView) view.findViewById(R.id.text3);
            attachment_id = (TextView) view.findViewById(R.id.attachment_id);
            image_position = (TextView) view.findViewById(R.id.image_position);
            im_delete = (ImageView) view.findViewById(R.id.im_delete);
        }
    }


    public VerticalAdapter(ArrayList<Multi_Photo_Bean> verticalList) {
        this.verticalList = verticalList;
    }

    @Override
    public MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View itemView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.image_list_row, parent, false);

        return new MyViewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(final MyViewHolder holder, final int position) {

       /* holder.txtView.setText(verticalList.get(position));
        holder.txtView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

            }
        });*/
    }

    @Override
    public int getItemCount() {
        return verticalList.size();
    }
}
