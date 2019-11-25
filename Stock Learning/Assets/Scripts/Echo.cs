using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using UnityEngine.UI;
using System;
public class Echo : MonoBehaviour
{
    //定义套接字
    Socket socket;

    public InputField inputField;
    public Text text;

    byte[] readBuff = new byte[1024];
    string recvStr = "";

    public void Connection()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //socket.Connect("192.168.1.9", 8888);
        socket.BeginConnect("127.0.0.1", 8888, ConnectCallback, socket);
    }

    public void Send()
    {
        string sendStr = inputField.text;
        byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
        socket.Send(sendBytes);

        //byte[] readBuff = new byte[1024];
        //int count = socket.Receive(readBuff);
        //string recvStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);
        //text.text = recvStr;
        //socket.Close();
    }

    private void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndConnect(ar);
            Debug.Log("Socket Connect Succ");
            socket.BeginReceive(readBuff, 0, 1024, 0, ReceiveCallback, socket);
        }
        catch (SocketException ex)
        {
            Debug.Log("Socket Connect fail" + ex.ToString());
        }
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            int count = socket.EndReceive(ar);
            recvStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);
            socket.BeginReceive(readBuff, 0, 1024, 0, ReceiveCallback, socket);
        }
        catch(SocketException ex)
        {
            Debug.Log("Socket Receive fail" + ex.ToString());
        }
    }
    public void Update()
    {

        text.text = recvStr;
    }
}
