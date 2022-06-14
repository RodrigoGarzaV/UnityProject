using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class DataBase : MonoBehaviour
{
    public TextMeshProUGUI Puntaje;
    private string Id = "8";
    private string APIScore = "https://localhost:5001/api/entrevista?";

    int helper = contador.preCorrectas;

    //Puntaje.SetText (helper.ToString);
    void Start()
    {
        StartCoroutine(PostAPI());
    }

    IEnumerator PostAPI()
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");

        string  apiURL = APIScore + "scoreEntrevista=" + Puntaje.text + "&idUsuario=" + Id;

        UnityWebRequest InfoRequest = UnityWebRequest.Post(apiURL, form);

        yield return InfoRequest.SendWebRequest();

        if (InfoRequest.isNetworkError || InfoRequest.isHttpError)
        {
            Debug.LogError(InfoRequest.error);
            yield break;
        }
    }
}
