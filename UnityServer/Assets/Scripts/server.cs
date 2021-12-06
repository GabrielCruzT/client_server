using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Text;

public class server : MonoBehaviour
{  
    [SerializeField] int port = 8080;

    Socket udp;
    int idAssignIndex = 0;

    public string guardar;

    public TextAsset textJSON;

    [System.Serializable]
    public class Player
    {
        public string name;
        public int age;
    }

    [System.Serializable]
    public class Dificult
    {
        public string name;
        public int number;
    }

    [System.Serializable]
    public class PlayerList
    {
        public Dificult dificulty;
        public Player[] player;
    }


    public PlayerList myPlayerList = new PlayerList();

    // Start is called before the first frame update
    void Start()
    {   
        IPHostEntry host = Dns.Resolve(Dns.GetHostName());
        IPAddress ip = host.AddressList[4];  
        IPEndPoint endPoint = new IPEndPoint(ip, port);
        Debug.Log("Server IP Address: " + ip);
        Debug.Log("Port: " + port);
        udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udp.Bind(endPoint);
        udp.Blocking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (udp.Available > 0)
        {
            byte[] packet = new byte[1024];
            EndPoint sender = new IPEndPoint(IPAddress.Any, port);
            int rec = udp.ReceiveFrom(packet, ref sender);
            Debug.Log("Bytes recebidos: " + rec);
            string receivedInfo = Encoding.Default.GetString(packet);
            Debug.Log("Server received: " + receivedInfo);
            //guardar = info;
            myPlayerList = JsonUtility.FromJson<PlayerList>(receivedInfo);
            Debug.Log(myPlayerList.dificulty.number);
            Debug.Log(myPlayerList.player.Length);
            Debug.Log(myPlayerList.player[0].name + "  " + myPlayerList.player[0].age);
            Debug.Log(myPlayerList.player[1].name + "  " + myPlayerList.player[1].age);
        }
    }
    


}
