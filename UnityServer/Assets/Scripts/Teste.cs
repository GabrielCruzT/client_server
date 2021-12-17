using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    [SerializeField]
    private GameObject bola1;
    [SerializeField]
    private GameObject bola2;
    [SerializeField]
    private GameObject prefab;

    Vector3 direction;
    float angleXY;
    float angleXZ;
    float angleYZ;
    public static Quaternion q;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = bola2.transform.position - bola1.transform.position;
        angleXY = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Debug.Log("XY: "+ angleXY);
        angleXZ = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Debug.Log("XZ: " + angleXZ);
        angleYZ = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
        Debug.Log("YZ: " + angleYZ);

        Vector3 v = new Vector3 (angleYZ, angleXZ, angleXY);
        q = Quaternion.Euler(v);
        
    }
}