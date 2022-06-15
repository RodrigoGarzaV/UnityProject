using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class DataBase : MonoBehaviour
{
    public TextMeshProUGUI score;
    private int Id = ValidateData.idUser;
    private int score2;
    private string APIScore = "https://localhost:5001/api/entrevista?";
    //private string APIScore = "https://localhost:44380/api/entrevista?";

    int helper = contador.preCorrectas;

    //Puntaje.SetText (helper.ToString);
    void Start()
    {
        score.text = "" + (contador.preCorrectas*20) + "";
        StartCoroutine(PostAPI());
    }

    IEnumerator PostAPI()
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");

        score2 = (contador.preCorrectas*20);

        string  apiURL = APIScore + "scoreEntrevista=" + score2 + "&idUsuario=" + Id.ToString();

        UnityWebRequest InfoRequest = UnityWebRequest.Post(apiURL, form);

        yield return InfoRequest.SendWebRequest();

        if (InfoRequest.isNetworkError || InfoRequest.isHttpError)
        {
            Debug.LogError(InfoRequest.error);
            yield break;
        }
    }
}
