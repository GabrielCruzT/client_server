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

    /*[System.Serializable]
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
    public PlayerList myPlayerList = new PlayerList();*/

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
    public class HEAD
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class NECK
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class RSHOULDER
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class RELBOW
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class RWRIST
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class LSHOULDER
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class LELBOW
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class LWRIST
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class RHIP
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class RKNEE
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class RANKLE
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class LHIP
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class LKNEE
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class LANKLE
    {
        public float x;
        public float y;
        public float z;
        public float confidence;
    }

    [System.Serializable]
    public class CHEST
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
        public HEAD HEAD;
        public NECK NECK;
        public RSHOULDER RSHOULDER;
        public RELBOW RELBOW;
        public RWRIST RWRIST;
        public LSHOULDER LSHOULDER;
        public LELBOW LELBOW;
        public LWRIST LWRIST;
        public RHIP RHIP;
        public RKNEE RKNEE;
        public RANKLE RANKLE;
        public LHIP LHIP;
        public LKNEE LKNEE;
        public LANKLE LANKLE;
        public CHEST CHEST;
    }

    [System.Serializable]
    public class persons
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
        public persons[] persons;
    }
    public PoseAnnotation myPoseAnnotation = new PoseAnnotation();

    /*JOINTS
    float jointHeadX;
    float jointHeadY;
    float jointHeadZ;

    float jointNeckX;
    float jointNeckY;
    float jointNeckZ;

    float jointRShoulderX;
    float jointRShoulderY;
    float jointRShoulderZ;

    float jointRElbowX;
    float jointRElbowY;
    float jointRElbowZ;

    float jointRWristX;
    float jointRWristY;
    float jointRWristZ;

    float jointLShoulderX;
    float jointLShoulderY;
    float jointLShoulderZ;

    float jointLElbowX;
    float jointLElbowY;
    float jointLElbowZ;

    float jointLWristX;
    float jointLWristY;
    float jointLWristZ;

    float jointRHipX;
    float jointRHipY;
    float jointRHipZ;

    float jointRKneeX;
    float jointRKneeY;
    float jointRKneeZ;

    float jointRAnkleX;
    float jointRAnkleY;
    float jointRAnkleZ;

    float jointLHipX;
    float jointLHipY;
    float jointLHipZ;

    float jointLKneeX;
    float jointLKneeY;
    float jointLKneeZ;

    float jointLAnkleX;
    float jointLAnkleY;
    float jointLAnkleZ;

    float jointChestX;
    float jointChestY;
    float jointChestZ;

    public GameObject bolaHead;
    public GameObject bolaNeck;
    public GameObject bolaRShoulder;
    public GameObject bolaRElbow;
    public GameObject bolaRWrist;
    public GameObject bolaLShoulder;
    public GameObject bolaLElbow;
    public GameObject bolaLWrist;
    public GameObject bolaRHip;
    public GameObject bolaRKnee;
    public GameObject bolaRAnkle;
    public GameObject bolaLHip;
    public GameObject bolaLKnee;
    public GameObject bolaLAnkle;
    public GameObject bolaChest;*/

    public GameObject[] myObjects = new GameObject[15];

    int i = 0;

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
            byte[] packet = new byte[4096];
            EndPoint sender = new IPEndPoint(IPAddress.Any, port);
            int rec = udp.ReceiveFrom(packet, ref sender);
            Debug.Log("Bytes recebidos: " + rec);
            string receivedInfo = Encoding.Default.GetString(packet);
            Debug.Log("Server received: " + receivedInfo);
            myPoseAnnotation = JsonUtility.FromJson<PoseAnnotation>(receivedInfo);
            //Debug.Log(myPlayerList.dificulty.number);
            //Debug.Log(myPlayerList.player.Length);
            //Debug.Log(myPlayerList.player[0].name + "  " + myPlayerList.player[0].age);
            //Debug.Log(myPlayerList.player[1].name + "  " + myPlayerList.player[1].age);

            Debug.Log("Sec:" + myPoseAnnotation.header.stamp.sec);
            Debug.Log("Frame:" + myPoseAnnotation.header.frame_id);
            Debug.Log("Persons Id:" + myPoseAnnotation.persons[0].id);
            Debug.Log("Pose name:" + myPoseAnnotation.persons[0].poses[0].pose_name);
            Debug.Log("Joints model:" + myPoseAnnotation.persons[0].joints.model);

            Debug.Log("Joints head x:" + myPoseAnnotation.persons[0].joints.HEAD.x);
            Debug.Log("Joints neck y:" + myPoseAnnotation.persons[0].joints.NECK.y);
            Debug.Log("Joints rwrist z:" + myPoseAnnotation.persons[0].joints.RWRIST.z);
            Debug.Log("Joints rhip confidense:" + myPoseAnnotation.persons[0].joints.RHIP.confidence);
            
            foreach (GameObject joint in myObjects)
            {
                joint.SetActive(true);
            }

            JointPosition(myPoseAnnotation.persons[0].joints.HEAD.x, myPoseAnnotation.persons[0].joints.HEAD.y, myPoseAnnotation.persons[0].joints.HEAD.z);
            JointPosition(myPoseAnnotation.persons[0].joints.NECK.x, myPoseAnnotation.persons[0].joints.NECK.y, myPoseAnnotation.persons[0].joints.NECK.z);
            JointPosition(myPoseAnnotation.persons[0].joints.RSHOULDER.x, myPoseAnnotation.persons[0].joints.RSHOULDER.y, myPoseAnnotation.persons[0].joints.RSHOULDER.z);
            JointPosition(myPoseAnnotation.persons[0].joints.RELBOW.x, myPoseAnnotation.persons[0].joints.RELBOW.y, myPoseAnnotation.persons[0].joints.RELBOW.z);
            JointPosition(myPoseAnnotation.persons[0].joints.RWRIST.x, myPoseAnnotation.persons[0].joints.RWRIST.y, myPoseAnnotation.persons[0].joints.RWRIST.z);
            JointPosition(myPoseAnnotation.persons[0].joints.LSHOULDER.x, myPoseAnnotation.persons[0].joints.LSHOULDER.y, myPoseAnnotation.persons[0].joints.LSHOULDER.z);
            JointPosition(myPoseAnnotation.persons[0].joints.LELBOW.x, myPoseAnnotation.persons[0].joints.LELBOW.y, myPoseAnnotation.persons[0].joints.LELBOW.z);
            JointPosition(myPoseAnnotation.persons[0].joints.LWRIST.x, myPoseAnnotation.persons[0].joints.LWRIST.y, myPoseAnnotation.persons[0].joints.LWRIST.z);
            JointPosition(myPoseAnnotation.persons[0].joints.RHIP.x, myPoseAnnotation.persons[0].joints.RHIP.y, myPoseAnnotation.persons[0].joints.RHIP.z);
            JointPosition(myPoseAnnotation.persons[0].joints.RKNEE.x, myPoseAnnotation.persons[0].joints.RKNEE.y, myPoseAnnotation.persons[0].joints.RKNEE.z);
            JointPosition(myPoseAnnotation.persons[0].joints.RANKLE.x, myPoseAnnotation.persons[0].joints.RANKLE.y, myPoseAnnotation.persons[0].joints.RANKLE.z);
            JointPosition(myPoseAnnotation.persons[0].joints.LHIP.x, myPoseAnnotation.persons[0].joints.LHIP.y, myPoseAnnotation.persons[0].joints.LHIP.z);
            JointPosition(myPoseAnnotation.persons[0].joints.LKNEE.x, myPoseAnnotation.persons[0].joints.LKNEE.y, myPoseAnnotation.persons[0].joints.LKNEE.z);
            JointPosition(myPoseAnnotation.persons[0].joints.LANKLE.x, myPoseAnnotation.persons[0].joints.LANKLE.y, myPoseAnnotation.persons[0].joints.LANKLE.z);
            JointPosition(myPoseAnnotation.persons[0].joints.CHEST.x, myPoseAnnotation.persons[0].joints.CHEST.y, myPoseAnnotation.persons[0].joints.CHEST.z);
            
            /*JOINTS
            bolaHead.SetActive(true);
            bolaNeck.SetActive(true);
            bolaRShoulder.SetActive(true);
            bolaRElbow.SetActive(true);
            bolaRWrist.SetActive(true);
            bolaLShoulder.SetActive(true);
            bolaLElbow.SetActive(true);
            bolaLWrist.SetActive(true);
            bolaRHip.SetActive(true);
            bolaRKnee.SetActive(true);
            bolaRAnkle.SetActive(true);
            bolaLHip.SetActive(true);
            bolaLKnee.SetActive(true);
            bolaLAnkle.SetActive(true);
            bolaChest.SetActive(true);

            jointHeadX = myPoseAnnotation.persons[0].joints.HEAD.x;
            jointHeadY = myPoseAnnotation.persons[0].joints.HEAD.y;
            jointHeadZ = myPoseAnnotation.persons[0].joints.HEAD.z;

            jointNeckX = myPoseAnnotation.persons[0].joints.NECK.x;
            jointNeckY = myPoseAnnotation.persons[0].joints.NECK.y;
            jointNeckZ = myPoseAnnotation.persons[0].joints.NECK.z;

            jointRShoulderX = myPoseAnnotation.persons[0].joints.RSHOULDER.x;
            jointRShoulderY = myPoseAnnotation.persons[0].joints.RSHOULDER.y;
            jointRShoulderZ = myPoseAnnotation.persons[0].joints.RSHOULDER.z;

            jointRElbowX = myPoseAnnotation.persons[0].joints.RELBOW.x;
            jointRElbowY = myPoseAnnotation.persons[0].joints.RELBOW.y;
            jointRElbowZ = myPoseAnnotation.persons[0].joints.RELBOW.z;

            jointRWristX = myPoseAnnotation.persons[0].joints.RWRIST.x;
            jointRWristY = myPoseAnnotation.persons[0].joints.RWRIST.y;
            jointRWristZ = myPoseAnnotation.persons[0].joints.RWRIST.z;

            jointLShoulderX = myPoseAnnotation.persons[0].joints.LSHOULDER.x;
            jointLShoulderY = myPoseAnnotation.persons[0].joints.LSHOULDER.y;
            jointLShoulderZ = myPoseAnnotation.persons[0].joints.LSHOULDER.z;

            jointLElbowX = myPoseAnnotation.persons[0].joints.LELBOW.x;
            jointLElbowY = myPoseAnnotation.persons[0].joints.LELBOW.y;
            jointLElbowZ = myPoseAnnotation.persons[0].joints.LELBOW.z;

            jointLWristX = myPoseAnnotation.persons[0].joints.LWRIST.x;
            jointLWristY = myPoseAnnotation.persons[0].joints.LWRIST.y;
            jointLWristZ = myPoseAnnotation.persons[0].joints.LWRIST.z;

            jointRHipX = myPoseAnnotation.persons[0].joints.RHIP.x;
            jointRHipY = myPoseAnnotation.persons[0].joints.RHIP.y;
            jointRHipZ = myPoseAnnotation.persons[0].joints.RHIP.z;

            jointRKneeX = myPoseAnnotation.persons[0].joints.RKNEE.x;
            jointRKneeY = myPoseAnnotation.persons[0].joints.RKNEE.y;
            jointRKneeZ = myPoseAnnotation.persons[0].joints.RKNEE.z;

            jointRAnkleX = myPoseAnnotation.persons[0].joints.RANKLE.x;
            jointRAnkleY = myPoseAnnotation.persons[0].joints.RANKLE.y;
            jointRAnkleZ = myPoseAnnotation.persons[0].joints.RANKLE.z;

            jointLHipX = myPoseAnnotation.persons[0].joints.LHIP.x;
            jointLHipY = myPoseAnnotation.persons[0].joints.LHIP.y;
            jointLHipZ = myPoseAnnotation.persons[0].joints.LHIP.z;

            jointLKneeX = myPoseAnnotation.persons[0].joints.LKNEE.x;
            jointLKneeY = myPoseAnnotation.persons[0].joints.LKNEE.y;
            jointLKneeZ = myPoseAnnotation.persons[0].joints.LKNEE.z;

            jointLAnkleX = myPoseAnnotation.persons[0].joints.LANKLE.x;
            jointLAnkleY = myPoseAnnotation.persons[0].joints.LANKLE.y;
            jointLAnkleZ = myPoseAnnotation.persons[0].joints.LANKLE.z;

            jointChestX = myPoseAnnotation.persons[0].joints.CHEST.x;
            jointChestY = myPoseAnnotation.persons[0].joints.CHEST.y;
            jointChestZ = myPoseAnnotation.persons[0].joints.CHEST.z;

            bolaHead.transform.position = new Vector3(jointHeadX, jointHeadY, jointHeadZ);
            bolaNeck.transform.position = new Vector3(jointNeckX, jointNeckY, jointNeckZ);
            bolaRShoulder.transform.position = new Vector3(jointRShoulderX, jointRShoulderY, jointRShoulderZ);
            bolaRElbow.transform.position = new Vector3(jointRElbowX, jointRElbowY, jointRElbowZ);
            bolaRWrist.transform.position = new Vector3(jointRWristX, jointRWristY, jointRWristZ);
            bolaLShoulder.transform.position = new Vector3(jointLShoulderX, jointLShoulderY, jointLShoulderZ);
            bolaLElbow.transform.position = new Vector3(jointLElbowX, jointLElbowY, jointLElbowZ);
            bolaLWrist.transform.position = new Vector3(jointLWristX, jointLWristY, jointLWristZ);
            bolaRHip.transform.position = new Vector3(jointRHipX, jointRHipY, jointRHipZ);
            bolaRKnee.transform.position = new Vector3(jointRKneeX, jointRKneeY, jointRKneeZ);
            bolaRAnkle.transform.position = new Vector3(jointRAnkleX, jointRAnkleY, jointRAnkleZ);
            bolaLHip.transform.position = new Vector3(jointLHipX, jointLHipY, jointLHipZ);
            bolaLKnee.transform.position = new Vector3(jointLKneeX, jointLKneeY, jointLKneeZ);
            bolaLAnkle.transform.position = new Vector3(jointLAnkleX, jointLAnkleY, jointLAnkleZ);
            bolaChest.transform.position = new Vector3(jointChestX, jointChestY, jointChestZ);*/
        }
    }
    void JointPosition(float jointPositionX, float jointPositionY, float jointPositionZ) 
    {
        myObjects[i].transform.position = new Vector3(jointPositionX, jointPositionY, jointPositionZ);
        i++;

        if (i > 15)
            i = 0;
    }
}
