using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


public class client : MonoBehaviour
{
    [SerializeField] string serverIP = "127.0.0.1";
    [SerializeField] int port = 8080;
    [SerializeField]

    Socket udp;
    IPEndPoint server;
    
    void Start()
    {
        server = new IPEndPoint(IPAddress.Parse(serverIP), port);
        udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udp.Blocking = false;
        SendInitialReqToServer();
    }

    void SendInitialReqToServer()
    {
        string path = "Assets/Resources/JSONText.json";
        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        reader.Close();

        Debug.Log("Client sent info:" + json);
        byte[] packet = Encoding.ASCII.GetBytes(json);
        udp.SendTo(packet, server); 
    }
    void Update()
    {
        
    }
}
