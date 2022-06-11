using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respuestas : MonoBehaviour
{
    public bool correcta = false;
    // Start is called before the first frame update
    public void onClick()
    {
        if(correcta){
            Debug.Log("Siuu");
        } 
        else{
            Debug.Log("Nel");
        }
    }
}
