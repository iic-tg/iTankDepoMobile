package com.i_tankdepo.adapter;

import android.app.Activity;
import android.content.Context;
import android.content.res.Resources;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.i_tankdepo.Beanclass.CustomerDropdownBean;
import com.i_tankdepo.R;

import java.util.ArrayList;

/**
 * Created by Metaplore on 7/16/2017.
 */

public class SpinnerCustomAdapter extends ArrayAdapter<String> {

    private Activity activity;
    private ArrayList data;
    CustomerDropdownBean tempValues=null;
    LayoutInflater inflater;

    /*************  CustomAdapter Constructor *****************/
    public SpinnerCustomAdapter(
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
        tempValues = (CustomerDropdownBean) data.get(position);

        View mView = super.getDropDownView(position, convertView, parent);
        TextView mTextView = (TextView) mView;
        mTextView.setText(tempValues.getTRFF_CD_DESCRPTN_VC());
       /* if(position==0){

            // Default selected Spinner item
            mTextView.setText("Please select company");
        }
        else
        {
            // Set values for spinner each row
            mTextView.setText(tempValues.getName());


        }*/

        return mView;
    }

}
