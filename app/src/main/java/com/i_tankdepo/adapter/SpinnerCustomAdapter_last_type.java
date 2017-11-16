package com.i_tankdepo.adapter;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.i_tankdepo.Beanclass.Equipment_Info_TypeDropdownBean;
import com.i_tankdepo.Beanclass.Previous_CargoDropdownBean;

import java.util.ArrayList;

/**
 * Created by Metaplore on 7/16/2017.
 */

public class SpinnerCustomAdapter_last_type extends ArrayAdapter<String> {

    private Activity activity;
    private ArrayList data;
    Equipment_Info_TypeDropdownBean tempValues=null;
    LayoutInflater inflater;

    /*************  CustomAdapter Constructor *****************/
    public SpinnerCustomAdapter_last_type(
            Activity activitySpinner,
            int textViewResourceId,
            ArrayList objects
    )
    {
        super(activitySpinner, textViewResourceId, objects);

        /********** Take passed values **********/
        activity = activitySpinner;
        data     = objects;

        /***********  Layout inflator to call external xml layout () **********************/
        inflater = (LayoutInflater)activity.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

    }

    @Override
    public View getDropDownView(int position, View convertView, ViewGroup parent) {
        return getCustomView(position, convertView, parent);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        return getCustomView(position, convertView, parent);
    }

    // This funtion called for each row ( Called data.size() times )
    public View getCustomView(int position, View convertView, ViewGroup parent) {

        /********** Inflate spinner_rows.xml file for each row ( Defined below ) ************/
        //View row = inflater.inflate(R.layout.spinner_text, parent, false);

        /***** Get each Model object from Arraylist ********/
        tempValues = null;
        tempValues = (Equipment_Info_TypeDropdownBean) data.get(position);

        View mView = super.getDropDownView(position, convertView, parent);
        TextView mTextView = (TextView) mView;
        mTextView.setText(tempValues.getTRFF_CD_DESCRPTN_VC());


        return mView;
    }

}
