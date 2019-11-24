using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using UnityEngine.UI;
public class Echo : MonoBehaviour
{
    //定义套接字
    Socket socket;

    public InputField inputField;
    public Text text;
    
    public void Connection()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect("192.168.1.9", 8888);
    }

    public void Send()
    {
        string sendStr = inputField.text;
        byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
        socket.Send(sendBytes);

        byte[] readBuff = new byte[1024];
        int count = socket.Receive(readBuff);
        string recvStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);
        text.text = recvStr;
        socket.Close();
    }
}
