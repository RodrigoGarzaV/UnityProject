using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreAplicante1 : MonoBehaviour
{
    // Start is called before the first frame update
    
    //public int idUsuario = ValidateData.idUser;
    public TextMeshProUGUI score;
    void Start()
    {
        //Debug.Log(ValidateData.idUser);
        score.text = "" + (contador.preCorrectas*5) + "";
    }
}