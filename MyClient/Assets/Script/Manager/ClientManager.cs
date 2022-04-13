using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using Common;
using System;

public class ClientManager : BaseManager
{
    private Socket clientSocket;
    private string ip = "192.168.0.101";
    private int port = 5000;
    private Message mess = new Message();

    public ClientManager(GameFace _gameface) : base(_gameface)
    {
    }

    public override void OnInit()
    {
        base.OnInit();
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(ip, port);
            Start();
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("客服端连接服务器失败，请检查服务器" + e);
        }
    }
    private void Start()
    {
        clientSocket.BeginReceive(mess.GetData, mess.StartIndex, mess.RemoveIndex, SocketFlags.None, ReceiveCallback, null);        
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            int count = clientSocket.EndReceive(ar);
            mess.ReadByte(count, processDataCallback);
            Start();
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("客户端接收服务器消息失败" + e);
        }
    }
    private void processDataCallback(ActionCode actionCode,string msg)
    {
        gameface.HandleRequest(actionCode, msg);
    }
    public void SendRequest(RequestCode requestCode,ActionCode actionCode,string msg)
    {
        byte[] datas =  Message.PickedMessage(requestCode, actionCode, msg);
        
        clientSocket.Send(datas);
    }
    public override void OnDestory()
    {
        base.OnDestory();
        try
        {
            clientSocket.Close();
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("关闭服务器失败" + e);
        }
    }
}
