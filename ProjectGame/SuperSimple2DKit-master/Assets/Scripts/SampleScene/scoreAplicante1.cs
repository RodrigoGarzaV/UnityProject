using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreAplicante1 : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int idAplicante;
    public TextMeshProUGUI score;
    void Start()
    {
        score.text = "" + (contador.preCorrectas*5) + "";
    }
}