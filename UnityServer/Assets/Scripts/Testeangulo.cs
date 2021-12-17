using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testeangulo : MonoBehaviour
{
    [SerializeField]
    private GameObject cubo;
    [SerializeField]
    private GameObject ombro;
    [SerializeField]
    private GameObject cotovelo;

    private Quaternion q;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ombro.transform.rotation = Teste.q;
    }
}
