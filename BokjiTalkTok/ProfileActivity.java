package com.example.serverapp;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;


public class ProfileActivity extends AppCompatActivity implements View.OnClickListener{
    private static final String TAG = "ProfileActivity";
//시작화면 클래스 

    private TextView textView; //
    private Button buttonChat; //누르면 챗봇화면으로 넘어가는 onClick()함수가 걸려있다.


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);

        //initializing views
        textView = (TextView) findViewById(R.id.textviewUserEmail);
        buttonChat = (Button) findViewById(R.id.buttonChat);
        //initializing firebase authentication object


        //textViewUserEmail의 내용을 변경해 준다.
        textView.setText("어서오세요!");

        //logout button event
        buttonChat.setOnClickListener(this);


    }

    //로그아웃 시 finish
    @Override
    public void onClick(View view) {
       //

        if (view == buttonChat){
            finish();
            startActivity(new Intent(this, ChatActivity.class));
        }
    }


}