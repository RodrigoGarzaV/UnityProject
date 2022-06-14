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

/*public class QuestionsPanel : MonoBehaviour
{
    [SerializeField] AudioClip pressSound;
    [SerializeField] AudioClip openSound;
    [SerializeField] GameObject Questions;


    public TextMeshProUGUI pregunta1;
    public TextMeshProUGUI respuesta1;
    public TextMeshProUGUI respuesta2;
    public TextMeshProUGUI respuesta3;
    public TextMeshProUGUI respuesta4;

   
    public TextMeshProUGUI pregunta1;
    public Button respuesta1;
    public Button respuesta2;
    public Button respuesta3;
    public Button respuesta4;
    //public List<string> opciones;
    //public List<Button> textOptions;
   private string  baseURL = "https://localhost:5001/api/preguntas/DataScience?";

    void OnEnable()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;

        int randomID = UnityEngine.Random.Range(0, 19);
        
        StartCoroutine(GetAPI(randomID, ResponseCallback));
    }
    
    IEnumerator GetAPI(int APIIndex, Action<string> callback = null)
    {
        string  apiURL = baseURL + "idUnity=" + APIIndex.ToString();
        Debug.Log(apiURL);
        //UnityWebRequest apiRequest = UnityWebRequest.Get(apiURL);
        var apiRequest = CreateRequest(apiURL);


        yield return apiRequest.SendWebRequest();

        //JSONNode apiInfo = JSON.Parse(apiRequest.downloadHandler.text);
        var data = apiRequest.downloadHandler.text;

        if (callback != null) 
        {
            callback(data);
        }

        //string Answer = data["answer"];
        //pregunta1.text = Answer;
    }

    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
    
        return request;
    }

    private async void ResponseCallback(string data)
    {
        Debug.Log(data);
        var dataAPI = JsonConvert.DeserializeObject<DataEngineering>(data);
        //Debug.Log(dataAPI);
        Debug.Log(dataAPI.id);

        pregunta1.text = dataAPI.question;

        /*opciones.Add(dataAPI.answer);
        opciones.Add(dataAPI.option1);
        opciones.Add(dataAPI.option2);
        opciones.Add(dataAPI.option3);

        textOptions.Add(respuesta1);
        textOptions.Add(respuesta2);
        textOptions.Add(respuesta3);
        textOptions.Add(respuesta4);
        
        for(int i = 0; i < 4; i++)
        {
            int index = UnityEngine.Random.Range(0, opciones.Count);
            textOptions[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = opciones[index];
            if(opciones[index] == dataAPI.answer)
            {
                Debug.Log(opciones[index]);
                Debug.Log(dataAPI.answer);
                textOptions[i].GetComponent<Respuestas>().correcta = true;
            }
            opciones.RemoveAt(index);
        }

        respuesta1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dataAPI.answer;
        respuesta2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dataAPI.option1;
        respuesta3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dataAPI.option2;
        respuesta4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dataAPI.option3;

        string ayuda1 = respuesta1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

        if(ayuda1.CompareTo(dataAPI.answer) == 0)
        {
            Debug.Log("casi");
        } 
        else 
        {
            Debug.Log("No");
        }
    }
    
    

    public void Enviar()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void Regresar()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void Unpause()
    {
        Cursor.visible = false;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}

[System.Serializable]
    public class DataEngineering
    {
        public int id;
        public string question;
        public string answer;
        public string option1;
        public string option2;
        public string option3;
    }

    public enum RequestType
    {
        GET = 0,
        POST = 1,
        PUT = 2
    } 
*/

public class QuestionsPanel : MonoBehaviour
{
    [SerializeField] AudioClip pressSound;
    [SerializeField] AudioClip openSound;
    [SerializeField] GameObject Questions;  
   
    public TextMeshProUGUI pregunta1;
    public TextMeshProUGUI respuesta1;
    public TextMeshProUGUI respuesta2;
    public TextMeshProUGUI respuesta3;
    public TextMeshProUGUI respuesta4;
    
   private string  baseURL = "https://localhost:5001/api/preguntas/DataScience?";

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
        
        pregunta1.text = apiInfo["answer"];
        respuesta1.text = apiInfo["answer"];
        respuesta2.text = apiInfo["option1"];
        respuesta3.text = apiInfo["option2"];
        respuesta4.text = apiInfo["option3"];
    }

    public void Enviar()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void Regresar()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void Unpause()
    {
        Cursor.visible = false;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}