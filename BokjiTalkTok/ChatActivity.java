package com.example.serverapp;

import android.os.Build;
import android.os.Bundle;
import android.os.Handler;

import android.os.Message;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.DataInput;
import java.io.DataInputStream;
import java.io.DataOutput;
import java.io.DataOutputStream;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.InetAddress;
import java.net.Socket;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import java.net.UnknownHostException;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;

import static java.sql.DriverManager.println;


public class ChatActivity extends AppCompatActivity  {
    String ip = "219.241.82.200";            // IP 번호. 서버의 ip를 입력해둔다.
    String port = "9999";
    Socket client; //소켓
    EditText input;  //서버로 전송할 메세지를 작성하는 EditText
    Button conbtn; //서버와 연결하는 버튼.
    Button btnSend; //메세지 전송버튼
    ListView listView; //리스트뷰 선언
    ArrayList<ChatMessage> messages = new ArrayList<ChatMessage>();
    // ArrayList() 에 채팅이 추가될 때 마다 하나씩 쌓는다.
    ChatListAdapter adapter; // 채팅이 들어갈 리스트뷰에 변화를 적용시키는 함수
    String prvmsg = "";
    private DataOutputStream dos; //데이터를 보내는 DataOutputStream
    private DataInputStream dis; //데이터 송신하는 DataInputStream

    @Override // 각종 버튼, 텍스트 입력창들을 선언하고 적용시키는 onCreate()
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_chat);

        conbtn = (Button) findViewById(R.id.goConnect); //btn1
        listView = (ListView) findViewById(R.id.list_view); //scroll
        input = (EditText) findViewById(R.id.msg_type);
        btnSend = findViewById(R.id.btn_chat_send);
        adapter = new ChatListAdapter(this, R.layout.item_out, messages); // 리스트뷰 적용
        listView.setAdapter(adapter);

        //      TextView text_msg;  //서버로 부터 받은 메세지를 보여주는 TextView
        //      EditText edit_msg;  //서버로 전송할 메세지를 작성하는 EditText
        //   EditText edit_ip;   //서버의 IP를 작성할 수 있는 EditText
        String msg0 = "안녕하세요! 어떤 정책을 찾으시나요? 먼저 연결을 해주세요!";
        ChatMessage chatMessage0 = new ChatMessage(msg0, isMine(2));
        messages.add(chatMessage0);

        adapter.notifyDataSetChanged();
        listView.setSelection(adapter.getCount() - 1); //always scroll to bottom of list view

    }

    //Button 클릭시 자동으로 호출되는 callback 메소드
    public void mOnClick(View v) {
        switch (v.getId()) {
            case R.id.goConnect://서버에 접속
                //Android API14버전이상 부터 네트워크 작업은 무조건 별도의 Thread에서 실행 해야함.
                //파이썬과 안드로이드(자바) 간의 문자 송수신은 서로 사용하는 양식(형)이 달라서,
                //byte[] receiver = new byte[1024]; 로 바이트 단위 숫자값을 읽어들인뒤 한번 문자로 전환하여 채팅을 이어간다.
                //채팅 진행 시 UI 변경사항이 생기면 즉각 적용해야 하나 메인 쓰레드에서만 변경작업이 가능하여(Android특성)
                //아래 쓰래드 내부에 별도로 메인쓰레드에게 ui변경을 요청하는 메소드를 생성한다.

                String msgcon = "연결시도...";

                ChatMessage chatMessageC = new ChatMessage(msgcon, isMine(1)); //
                messages.add(chatMessageC);
                adapter.notifyDataSetChanged();
                listView.setSelection(adapter.getCount() - 1); //리스트뷰를 하단으로 내려주는 기능

                new Thread(new Runnable() {
                    @RequiresApi(api = Build.VERSION_CODES.KITKAT)
                    @Override
                    public void run() {
                        // TODO Auto-generated method stub
                        try {
                            //서버연결 소켓 생성
                            client = new Socket(InetAddress.getByName(ip), Integer.parseInt(port));
                            dos = new DataOutputStream(client.getOutputStream());   // output에 보낼꺼 넣음
                            dis = new DataInputStream(client.getInputStream());     // input에 받을꺼 넣어짐
                          //  dos.writeUTF("교육비가 필요해요");
                               } catch (IOException e) {

                            // TODO Auto-generated catch block
                            e.printStackTrace();
                        }
                        //서버와 접속이 끊길 때까지 무한반복하면서 서버의 메세지 수신
                        while (true) {
                            try {
                                String line = "";
                                byte[] receiver = new byte[1024]; // readutf 는 writeutf() 로 쓰여진 것만 읽을 수 있어서, 파이썬에서 전송된 string은 읽을 수 없다.
                                dis.read(receiver);     //대신 숫자는 문제없이 read 할 수 있는데, 이를 이용해
                                // 바이트로 읽어와서 string으로 한번 변화시켜주는 식.
                                line = new String(receiver);


                                //안드로이드는 메인쓰레드만 ui를 변경할 수 있어서, 별도로 메인쓰레드에게 ui변경을 요청하는 메소드를 만든다.
                                {
                                    String finalLine = line;
                                    runOnUiThread(new Runnable() {
                                        @Override
                                        public void run() {
                                            //      println(msg + "run 진행확인");
                                            //Toast.makeText(ChatActivity.this, "run", Toast.LENGTH_SHORT).show();
                                            // TODO Auto-generated method stub
                                            ChatMessage chatMessage2 = new ChatMessage(finalLine, isMine(2));
                                            messages.add(chatMessage2);
                                            adapter.notifyDataSetChanged();
                                            listView.setSelection(adapter.getCount() - 1); //리스트뷰의 변경 감지 후 항상 맨 아래로 스크롤 이동시킴
                                        }

                                    });
                                }
                            } catch (IOException e) {
                                // TODO Auto-generated catch block
                                e.printStackTrace();

                            }

                        }//while
                    }//run method...
                }).start();//Thread 실행..
                break;
            case R.id.btn_chat_send: //메세지 전송버튼
                if (dos == null) return;   //서버와 연결상태여야 전송가능
                //네트워크 작업이므로 Thread 생성
                String msg3 = input.getText().toString();
                prvmsg = msg3;
                if(msg3=="0") return;
                ChatMessage chatMessage1 = new ChatMessage(msg3, isMine(1));
                messages.add(chatMessage1);
                adapter.notifyDataSetChanged();
                listView.setSelection(adapter.getCount() - 1); //리스트뷰를 하단으로 내려주는 기능

                //아래 쓰레드에서는 dos.writeUTF() 로 서버에 메세지를 전송하는 기능을 한다.
                //파이썬에서는 자바측에서 보낸 writeUTF()를 읽을 때
                //문자열의 앞에 특정 특수문자가 생성되는 것 말고는 별 지장없이 String 값이 읽히기에 별다른 처리를 하지 않는다.
                new Thread(new Runnable() {
                    @Override
                    public void run() {
                        // TODO Auto-generated method stub
                        //서버로 보낼 메세지 EditText로 부터 얻어오기
                        try {
                            dos.writeUTF(msg3);  //서버로 메세지 보내기. 파이썬은 writeUTF 로 써도 읽을 순 있다.(다만 앞에 특수문자가 생성되어 제거해야함)
                            dos.flush();        //다음 메세지 전송을 위해 버퍼 제거

                        } catch (IOException e) {
                            // TODO Auto-generated catch block
                            e.printStackTrace();
                        }

                    }//run
                }).start(); //Thread
                input.setText("");//쓰레드에서 송신작업 끝나면 입력창을 비워둔다.
                break;



            case R.id.btn_chat_again: //다시 질문 버튼
                if (dos == null) return;   //서버와 연결상태여야 전송가능
                //네트워크 작업이므로 Thread 생성
                String msg4 = prvmsg; // 재질문 발동 키워드 전송, 특정 커맨드를 명령하는 식이라 별다른 조치 취하지 x
                String msg5 = "다른 답변을 부탁합니다.";
                if(msg4=="0") return;
                ChatMessage chatMessage4 = new ChatMessage(msg5, isMine(1)); // 다른 답변을 탐색합니다 를 유저측 채팅창에 표시함
                messages.add(chatMessage4);
                adapter.notifyDataSetChanged();
                listView.setSelection(adapter.getCount() - 1); //리스트뷰를 하단으로 내려주는 기능
                new Thread(new Runnable() {
                    @Override
                    public void run() {
                        // TODO Auto-generated method stub
                        //서버로 보낼 메세지 EditText로 부터 얻어오기
                        try {
                            dos.writeUTF(msg4);  //서버로 메세지 보내기. 파이썬은 writeUTF 로 써도 읽을 순 있다.(다만 앞에 특수문자가 생성되어 제거해야함)
                            dos.flush();        //다음 메세지 전송을 위해 버퍼 제거

                        } catch (IOException e) {
                            // TODO Auto-generated catch block
                            e.printStackTrace();
                        }

                    }//run
                }).start(); //Thread
                input.setText("");//쓰레드에서 송신작업 끝나면 입력창을 비워둔다.
                break;


        }}

        //챗봇측과 유저측 말풍선 다르게 해주는 함수. 리턴값이 1이면 사용자 말풍선, 2면 챗봇의 응답 말풍선
        static int isMine(int i){
            int a;
            a = 0;
            if (i == 1) {
                a = 1;
            } else if (i == 2) {
                a = 2;
            }
            return a;
        }

    }

