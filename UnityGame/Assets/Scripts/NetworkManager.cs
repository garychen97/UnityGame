using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public bool isConnected = false;
    public Socket ClientSocket;
    public Socket ServerSocket;
    #region 单例
/*
    private static NetworkManager instance;
    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("NetworkManager");
                if (go == null)
                {
                    Debug.LogError("未找到名为NetworkManager的物体");
                }
                instance = go.GetComponent<NetworkManager>();
                if (instance == null)
                {
                    instance = go.AddComponent<NetworkManager>();
                }
            }

            return instance;
        }
    }
*/
    #endregion

    public void ConnetToServer()
    {
        IPAddress iP = IPAddress.Parse("127.0.0.1");
        IPEndPoint iPEndPoint = new IPEndPoint(iP, 6688);
        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ClientSocket.Connect(iPEndPoint);
        byte [] tmp = new byte[1];

        ClientSocket.Blocking = false;
        ClientSocket.Send(tmp, 0, 0);
        Debug.Log("Connected!");
    }
    
    public void DisConnetToServer()
    {
        if (ClientSocket == null)
        {
            return;
        }
        if (ClientSocket.Connected)
        {
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Disconnect(false);
        }
    }

    public void SendTextToServer(string strMsg)
    {
        if (Instance.ClientSocket == null || !Instance.ClientSocket.Connected)
        {
            Debug.Log("尚未连接到服务器");
            return;
        }
        byte[] buffer = new byte[2048];
        
        buffer = Encoding.Default.GetBytes(strMsg);
        ClientSocket.Send(buffer);
        Debug.Log("发送了字符串:"+ strMsg);
    }
    

    public void OnDestroy()
    {
        DisConnetToServer();
    }
}
