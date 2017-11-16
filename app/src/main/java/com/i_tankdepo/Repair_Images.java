package com.i_tankdepo;

import android.app.Activity;
import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
import android.support.v4.view.PagerAdapter;
import android.support.v4.view.ViewPager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Metaplore on 5/5/2017.
 */

public class Repair_Images extends CommonActivity {
    private ViewPager viewpager;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.repair_viewpager);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
         viewpager = (ViewPager) findViewById(R.id.viewpager);
        Button next = (Button)findViewById(R.id.button1);
        Button previous = (Button)findViewById(R.id.button2);
        PagerAdapter adapter = new CustomAdapter(Repair_Images.this);
        viewpager.setAdapter(adapter);


        next.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                viewpager.setCurrentItem(viewpager.getCurrentItem()+1, true);
            }
        });
        previous.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                viewpager.setCurrentItem(viewpager.getCurrentItem()-1, true);
            }
        });
    }

    @Override
    public void onClick(View view) {

    }

    @Override
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

    }


}
