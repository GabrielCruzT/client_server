using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    [SerializeField]
    GameObject[] cubo = new GameObject[15];

    [SerializeField] 
    Color[] mycolors = new Color[5];

    float valor = 0.8f;
    float valor1 = 0.6f;
    float valor2 = 0.4f;
    float valor3 = 0.2f;
    float valor4 = 1.0f;
    int numero = 0;
    int numero1 = 1;
    int numero2 = 2;
    int numero3 = 3;
    int numero4 = 4;

    void Update()
    {
        
        ColorChangerr(valor, numero);
        ColorChangerr(valor1, numero1);
        ColorChangerr(valor2, numero2);
        ColorChangerr(valor3, numero3);
        ColorChangerr(valor4, numero4);
    }

    void ColorChangerr(float confidense, int numeroCubo)
    {
        if (confidense <= 0.2f)
            cubo[numeroCubo].GetComponent<Renderer>().material.color = mycolors[0];

        if (confidense > 0.2f && confidense <= 0.4f)
            cubo[numeroCubo].GetComponent<Renderer>().material.color = mycolors[1];

        if (confidense > 0.4f && confidense <= 0.6f)
            cubo[numeroCubo].GetComponent<Renderer>().material.color = mycolors[2];

        if (confidense > 0.6f && confidense <= 0.8f)
            cubo[numeroCubo].GetComponent<Renderer>().material.color = mycolors[3];

        if (confidense > 0.8f)
            cubo[numeroCubo].GetComponent<Renderer>().material.color = mycolors[4];
    }
}

