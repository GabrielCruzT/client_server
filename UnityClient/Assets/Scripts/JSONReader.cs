using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset textJSON;
    [System.Serializable]
    public class Player 
    {
        public string name;
    }
    [System.Serializable]
    public class PlayerList 
    {
        public Player[] player;
    }

    public PlayerList myPlayerList = new PlayerList();

    public static string valor;
    // Start is called before the first frame update
    void Awake()
    {
        myPlayerList = JsonUtility.FromJson<PlayerList>(textJSON.text);
        string json = JsonUtility.ToJson(myPlayerList.player[0]);
        valor = json;
        //Debug.Log(valor);
    }
    void Update()
    {
        
    }
}
