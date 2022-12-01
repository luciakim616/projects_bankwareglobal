package com.example.serverapp;
public class ChatMessage {
    private int isMine;
    private String message;

    public ChatMessage(String message, int isMine) {
        this.isMine = isMine;
        this.message = message;
    }
    // ListView에 적용하기 쉽게 + 나인패치 말풍선 적용하기 위한 챗메시지 추가 함수

    public int isMine() {
        return isMine;
    }
    // 위에서 사용된 말풍선 적용 함수 (송신/ 수신 말풍선 구분)

    public String getMessage() {
        return message;
    }
    //입력받는 String 값을 message 꼴로 반환하는 함수




    public void setMine(int mine) {
        isMine = mine;
    }
    public void setMessage(String message) {
        this.message = message;
    }
}