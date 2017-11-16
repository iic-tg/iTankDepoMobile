package com.i_tankdepo;

import android.app.Dialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Typeface;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

/**
 * Created by Edwin on 26-11-2015.
 */
public abstract class CommonFragment extends Fragment implements View.OnClickListener{
    public Typeface Regular_font, Bold_Font;
    public abstract void init(View view);

    // Shared Preferences
    public SharedPreferences sp;
    public static final String PREF_NAME = "Redington";
    public static final String SP_AUTHR_USER = "redington";
    public static final String SP_AUTHR_PASSWORD = "red1ngt0n";
    public static final String SP_USER_ID = "user_Id";
    public static final String SP_TYPE = "user_type";

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Regular_font = Typeface.createFromAsset(getActivity().getAssets(), "fonts/Montserrat-Regular.ttf");
        Bold_Font = Typeface.createFromAsset(getActivity().getAssets(), "fonts/Montserrat-Bold.ttf");

        sp = getActivity().getSharedPreferences(PREF_NAME, Context.MODE_PRIVATE);
    }

    @Override
    public void onViewCreated(View view, Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        init(view);
    }

    //for Applying Regular_font
    public void setRegularFont(View view, int[] ids) {
        for (int i=0; i<ids.length; i++) {
            if (getView().findViewById(ids[i]) instanceof Button) {
                Button button = (Button) view.findViewById(ids[i]);
                button.setTypeface(Regular_font);
            } else if (getView().findViewById(ids[i]) instanceof EditText) {
                EditText editText = (EditText) view.findViewById(ids[i]);
                editText.setTypeface(Regular_font);
            } else if (getView().findViewById(ids[i]) instanceof TextView) {
                TextView textView = (TextView) view.findViewById(ids[i]);
                textView.setTypeface(Regular_font);
            }
        }
    }

    //for Dialog
    public void setRegularFont(Dialog dialog, int[] ids) {
        for (int i=0; i<ids.length; i++) {
            if (dialog.findViewById(ids[i]) instanceof Button) {
                Button button = (Button) dialog.findViewById(ids[i]);
                button.setTypeface(Regular_font);
            } else if (dialog.findViewById(ids[i]) instanceof EditText) {
                EditText editText = (EditText) dialog.findViewById(ids[i]);
                editText.setTypeface(Regular_font);
            } else if (dialog.findViewById(ids[i]) instanceof TextView) {
                TextView textView = (TextView) dialog.findViewById(ids[i]);
                textView.setTypeface(Regular_font);
            }
        }
    }

    //for Applying Bold_font
    public void setBoldFont(View view, int[] ids) {
        for (int i=0; i<ids.length; i++) {
            if (getView().findViewById(ids[i]) instanceof Button) {
                Button button = (Button) view.findViewById(ids[i]);
                button.setTypeface(Bold_Font);
            } else if (getView().findViewById(ids[i]) instanceof EditText) {
                EditText editText = (EditText) view.findViewById(ids[i]);
                editText.setTypeface(Bold_Font);
            } else if (getView().findViewById(ids[i]) instanceof TextView) {
                TextView textView = (TextView) view.findViewById(ids[i]);
                textView.setTypeface(Bold_Font);
            }
        }
    }

    //for Dialog
    public void setBoldFont(Dialog dialog, int[] ids) {
        for (int i=0; i<ids.length; i++) {
            if (dialog.findViewById(ids[i]) instanceof Button) {
                Button button = (Button) dialog.findViewById(ids[i]);
                button.setTypeface(Bold_Font);
            } else if (dialog.findViewById(ids[i]) instanceof EditText) {
                EditText editText = (EditText) dialog.findViewById(ids[i]);
                editText.setTypeface(Bold_Font);
            } else if (dialog.findViewById(ids[i]) instanceof TextView) {
                TextView textView = (TextView) dialog.findViewById(ids[i]);
                textView.setTypeface(Bold_Font);
            }
        }
    }

    public void shortToast(String msg) {
        Toast.makeText(getActivity(), msg, Toast.LENGTH_SHORT).show();
    }

    public void longToast(String msg) {
        Toast.makeText(getActivity(), msg, Toast.LENGTH_LONG).show();
    }

}
