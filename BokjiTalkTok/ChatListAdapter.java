package com.example.serverapp;
import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import java.util.ArrayList;

public class ChatListAdapter extends ArrayAdapter<ChatMessage> {
//채팅 리스트뷰 클래스
    private Activity activity;
    private ArrayList<ChatMessage> chatMessages;

    // 아래의 네개 함수를 종합하여 나인패치가 적용된 말풍선을
    // ArrayList에 추가하여 리스트뷰 화면에 출력하는 역할을 한다.
    public ChatListAdapter(Activity context, int resource, ArrayList<ChatMessage> objects) {
        super(context, resource, objects);
        this.activity =context;
        this.chatMessages = objects;
    }
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        ViewHolder holder;
        LayoutInflater inflater = (LayoutInflater) activity.getSystemService(Activity.LAYOUT_INFLATER_SERVICE);

        int layoutResource = 0; // determined by view type
        ChatMessage chatMessage = getItem(position);

        if (chatMessage.isMine()==1) {
            //ismine() 값이 참이면 in 말풍선(유저말풍선) 출력 아니면 out(챗봇의 말풍선) 출력
            layoutResource = R.layout.item_in;
        } if(chatMessage.isMine()==2) {
            layoutResource = R.layout.item_out;
        }


        if (convertView != null) {
            holder = (ViewHolder) convertView.getTag();
        } else {
            convertView = inflater.inflate(layoutResource, parent, false);
            holder = new ViewHolder(convertView);
            convertView.setTag(holder);
        }

        //set message content
        holder.message.setText(chatMessage.getMessage());

        return convertView;
    }
    @Override
    public int getViewTypeCount() {
        // return the total number of view types. this value should never change
        // at runtime
        return 2;
    }
    @Override
    public int getItemViewType(int position) {
        // return a value between 0 and (getViewTypeCount - 1)
        return position % 2;
    }
    static class ViewHolder {
        private TextView message;

        public ViewHolder(View v) {
            message = (TextView)v.findViewById(R.id.text);
        }
    }
}