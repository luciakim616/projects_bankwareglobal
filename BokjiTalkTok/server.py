# server.py 
import socket
import logging
import struct

from bayes import bayes

message = u"GET~CARD\n"
size = len(message)
host = '58.230.143.163' # 호스트 ip
port = 9999            # 포트번호


logger = logging.getLogger()
logger.setLevel(logging.INFO)
formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
stream_handler = logging.StreamHandler()
stream_handler.setFormatter(formatter)
logger.addHandler(stream_handler)

server_sock = socket.socket(socket.AF_INET)
server_sock.bind((host, port))
server_sock.listen(1)


print("기다리는 중")
client_sock, addr = server_sock.accept()

print('Connected by', addr)

while True:
    

    recvData = client_sock.recv(1024)[2:] # [2:] 있어야 안드에서 제대로 수신됨.
    #(안드에선 송신 시 앞에 특정 바이트수가 붙어와서 이상한 값이 하나 붙어서 잘라주는 역할.)
    
    print('상대방 :', recvData.decode('utf-8'))
    logger.info("recieve")

    sendData = bayes(recvData.decode('utf-8'))
    client_sock.send(sendData.encode('utf-8'))
    logger.info("send")
