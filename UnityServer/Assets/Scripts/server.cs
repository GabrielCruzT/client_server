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

    [System.Serializable]
    public class stamp
    {
        public int sec;
        public int nsec;
    }

    [System.Serializable]
    public class header
    {
        public int seq;
        public string frame_id;
        public stamp stamp;
    }

    [System.Serializable]
    public class poses
    {
        public string pose_name;
        public int pose_id;
        public float prediction_score;
        public string prediction;
        public int class_id;
        public float confidence;
    }

    [System.Serializable]
    public class JOINT
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class joints
    {
        public string model;
        public JOINT HEAD;
        public JOINT NECK;
        public JOINT RIGHT_SHOULDER;
        public JOINT RIGHT_ELBOW;
        public JOINT RIGHT_WRIST;
        public JOINT LEFT_SHOULDER;
        public JOINT LEFT_ELBOW;
        public JOINT LEFT_WRIST;
        public JOINT RIGHT_HIP;
        public JOINT RIGHT_KNEE;
        public JOINT RIGHT_ANKLE;
        public JOINT LEFT_HIP;
        public JOINT LEFT_KNEE;
        public JOINT LEFT_ANKLE;
        public JOINT CHEST;
    }

    [System.Serializable]
    public class pose_tracks
    {
        public int id;
        public float height;
        public float orientation;
        public float age;
        public string predicted_pose_name;
        public int predicted_pose_id;
        public float prediction_score;
        public poses[] poses;
        public joints joints;
    }

    [System.Serializable]
    public class PoseAnnotation
    {
        public header header;
        public pose_tracks[] pose_tracks;
    }
    public PoseAnnotation myPoseAnnotation = new PoseAnnotation();

    public GameObject[] myObjects = new GameObject[15];
    int i = 0;
    bool juntas;
    int a = 0;

    [SerializeField]
    GameObject[] junta = new GameObject[15];

    [SerializeField]
    Color[] mycolors = new Color[5];

    // Start is called before the first frame update
    void Start()
    {   
        IPHostEntry host = Dns.Resolve(Dns.GetHostName());
        IPAddress ip = host.AddressList[1];  
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
            byte[] packet = new byte[4096];
            EndPoint sender = new IPEndPoint(IPAddress.Any, port);
            int rec = udp.ReceiveFrom(packet, ref sender);
            Debug.Log("Bytes recebidos: " + rec);
            string receivedInfo = Encoding.Default.GetString(packet);
            Debug.Log("Server received: " + receivedInfo);
            if (receivedInfo.Contains("pose_tracks"))
            {
                juntas = true;
                myPoseAnnotation = JsonUtility.FromJson<PoseAnnotation>(receivedInfo);
            }
            
            if (juntas == true)
            {
                foreach (GameObject joint in myObjects)
                    joint.SetActive(true);
                
                if (myPoseAnnotation.pose_tracks.Length == 1)
                {
                    Debug.Log(myPoseAnnotation.pose_tracks[0].joints.HEAD.x);
                    JointPosition(0, myPoseAnnotation.pose_tracks[0].joints.HEAD.x, myPoseAnnotation.pose_tracks[0].joints.HEAD.y, myPoseAnnotation.pose_tracks[0].joints.HEAD.z);
                    JointPosition(1, myPoseAnnotation.pose_tracks[0].joints.NECK.x, myPoseAnnotation.pose_tracks[0].joints.NECK.y, myPoseAnnotation.pose_tracks[0].joints.NECK.z);
                    JointPosition(2,myPoseAnnotation.pose_tracks[0].joints.RIGHT_SHOULDER.x, myPoseAnnotation.pose_tracks[0].joints.RIGHT_SHOULDER.y, myPoseAnnotation.pose_tracks[i].joints.RIGHT_SHOULDER.z);
                    JointPosition(3,myPoseAnnotation.pose_tracks[0].joints.RIGHT_ELBOW.x, myPoseAnnotation.pose_tracks[0].joints.RIGHT_ELBOW.y, myPoseAnnotation.pose_tracks[0].joints.RIGHT_ELBOW.z);
                    JointPosition(4,myPoseAnnotation.pose_tracks[0].joints.RIGHT_WRIST.x, myPoseAnnotation.pose_tracks[0].joints.RIGHT_WRIST.y, myPoseAnnotation.pose_tracks[0].joints.RIGHT_WRIST.z);
                    JointPosition(5,myPoseAnnotation.pose_tracks[0].joints.LEFT_SHOULDER.x, myPoseAnnotation.pose_tracks[0].joints.LEFT_SHOULDER.y, myPoseAnnotation.pose_tracks[0].joints.LEFT_SHOULDER.z);
                    JointPosition(6,myPoseAnnotation.pose_tracks[0].joints.LEFT_ELBOW.x, myPoseAnnotation.pose_tracks[0].joints.LEFT_ELBOW.y, myPoseAnnotation.pose_tracks[0].joints.LEFT_ELBOW.z);
                    JointPosition(7,myPoseAnnotation.pose_tracks[0].joints.LEFT_WRIST.x, myPoseAnnotation.pose_tracks[0].joints.LEFT_WRIST.y, myPoseAnnotation.pose_tracks[0].joints.LEFT_WRIST.z);
                    JointPosition(8,myPoseAnnotation.pose_tracks[0].joints.RIGHT_HIP.x, myPoseAnnotation.pose_tracks[0].joints.RIGHT_HIP.y, myPoseAnnotation.pose_tracks[0].joints.RIGHT_HIP.z);
                    JointPosition(9,myPoseAnnotation.pose_tracks[0].joints.RIGHT_KNEE.x, myPoseAnnotation.pose_tracks[0].joints.RIGHT_KNEE.y, myPoseAnnotation.pose_tracks[0].joints.RIGHT_KNEE.z);
                    JointPosition(10,myPoseAnnotation.pose_tracks[0].joints.RIGHT_ANKLE.x, myPoseAnnotation.pose_tracks[0].joints.RIGHT_ANKLE.y, myPoseAnnotation.pose_tracks[0].joints.RIGHT_ANKLE.z);
                    JointPosition(11,myPoseAnnotation.pose_tracks[0].joints.LEFT_HIP.x, myPoseAnnotation.pose_tracks[0].joints.LEFT_HIP.y, myPoseAnnotation.pose_tracks[0].joints.LEFT_HIP.z);
                    JointPosition(12,myPoseAnnotation.pose_tracks[0].joints.LEFT_KNEE.x, myPoseAnnotation.pose_tracks[0].joints.LEFT_KNEE.y, myPoseAnnotation.pose_tracks[0].joints.LEFT_KNEE.z);
                    JointPosition(13,myPoseAnnotation.pose_tracks[0].joints.LEFT_ANKLE.x, myPoseAnnotation.pose_tracks[0].joints.LEFT_ANKLE.y, myPoseAnnotation.pose_tracks[0].joints.LEFT_ANKLE.z);
                    JointPosition(14,myPoseAnnotation.pose_tracks[0].joints.CHEST.x, myPoseAnnotation.pose_tracks[0].joints.CHEST.y, myPoseAnnotation.pose_tracks[0].joints.CHEST.z);

                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.HEAD.confidence, 0);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.NECK.confidence, 1);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.RIGHT_SHOULDER.confidence, 2);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.RIGHT_ELBOW.confidence, 3);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.RIGHT_WRIST.confidence, 4);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.LEFT_SHOULDER.confidence, 5);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.LEFT_ELBOW.confidence, 6);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.LEFT_WRIST.confidence, 7);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.RIGHT_HIP.confidence, 8);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.RIGHT_KNEE.confidence, 9);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.RIGHT_ANKLE.confidence, 10);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.LEFT_HIP.confidence, 11);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.LEFT_KNEE.confidence, 12);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.LEFT_ANKLE.confidence, 13);
                    ColorChangerr(myPoseAnnotation.pose_tracks[0].joints.CHEST.confidence, 14);
                }
                juntas = false;
            }
        }
    }
    void JointPosition(int indice, float jointPositionX, float jointPositionY, float jointPositionZ) 
    {
        myObjects[indice].transform.position = new Vector3(jointPositionX, jointPositionZ, -jointPositionY );
    }

    void ColorChangerr(float confidense, int numeroCubo)
    {
        if (confidense <= 0.2f)
            junta[numeroCubo].GetComponent<Renderer>().material.color = mycolors[0];

        if (confidense > 0.2f && confidense <= 0.4f)
            junta[numeroCubo].GetComponent<Renderer>().material.color = mycolors[1];

        if (confidense > 0.4f && confidense <= 0.6f)
            junta[numeroCubo].GetComponent<Renderer>().material.color = mycolors[2];

        if (confidense > 0.6f && confidense <= 0.8f)
            junta[numeroCubo].GetComponent<Renderer>().material.color = mycolors[3];

        if (confidense > 0.8f)
            junta[numeroCubo].GetComponent<Renderer>().material.color = mycolors[4];
    }
}
