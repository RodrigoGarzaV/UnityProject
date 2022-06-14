using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class contador : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI strPreguntas;
    public static int numPreguntas;
    public static int preCorrectas;
    
    void Start()
    {
        strPreguntas.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        strPreguntas.text = "" + numPreguntas + "";
    }
}