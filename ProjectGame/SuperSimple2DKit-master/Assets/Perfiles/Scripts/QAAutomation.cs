using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.Text;
using System;

public class QAAutomation : MonoBehaviour
{
    [SerializeField] AudioClip pressSound;
    [SerializeField] AudioClip openSound;
    [SerializeField] GameObject Questions;  
   
    public TextMeshProUGUI pregunta1;
    public TextMeshProUGUI respuesta1;
    public TextMeshProUGUI respuesta2;
    public TextMeshProUGUI respuesta3;
    public TextMeshProUGUI respuesta4;
    
    private string  baseURL = "https://localhost:5001/api/preguntas/QAAutomation?";
    //private string  baseURL = "https://localhost:44380/api/preguntas/QAAutomation?";

    void OnEnable()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;

        int randomID = UnityEngine.Random.Range(0, 19);

        StartCoroutine(GetAPI(randomID));
    }
    
    IEnumerator GetAPI(int APIIndex)
    {
        string  apiURL = baseURL + "idUnity=" + APIIndex.ToString();
        Debug.Log(apiURL);
        UnityWebRequest apiRequest = UnityWebRequest.Get(apiURL);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.LogError(apiRequest.error);
            yield break;
        }

        JSONNode apiInfo = JSON.Parse(apiRequest.downloadHandler.text);
        
        pregunta1.text = apiInfo["question"];
        respuesta1.text = apiInfo["answer"];
        respuesta2.text = apiInfo["option1"];
        respuesta3.text = apiInfo["option2"];
        respuesta4.text = apiInfo["option3"];
    }

    public void Regresar()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}