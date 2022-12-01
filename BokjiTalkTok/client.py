from socket import *

from bayes import bayes


port = 9999

clientSock = socket(AF_INET, SOCK_STREAM)
clientSock.connect(('192.168.219.101', port))

print('접속 완료')

while True:
    sendData = input('>>>')
    clientSock.send(sendData.encode('utf-8'))
    recvData = clientSock.recv(1024)
    print('상대방 :', recvData.decode('utf-8'))
