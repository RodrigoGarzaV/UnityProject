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
using UnityEngine.SceneManagement;

public class ValidateData : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField emailInput;
    public TextMeshProUGUI Message;

    //public TMP_Text name;

    private string APILogin = "https://localhost:5001/api/APIMySQLController/";


    private void Start() {
        Message.text = "";
       
    }

     public void SubmitLogin()
     {
        string emailIndex = emailInput.text;
        StartCoroutine(GetAPI(emailIndex));
     }


     IEnumerator GetAPI(string _email)
    {
        string  apiURL = APILogin + _email;
        Debug.Log(apiURL);
        UnityWebRequest apiRequest = UnityWebRequest.Get(apiURL);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.LogError(apiRequest.error);
            yield break;
        }

        JSONNode apiInfo = JSON.Parse(apiRequest.downloadHandler.text);
        
        int idUser = apiInfo["idAplicante"];
        Debug.LogError(idUser);

        SceneManager.LoadScene("Menu");

        //ScoreAplicante.idAplicante = idUser;

    }











    
    // public void SubmitLogin()
    // {   
    //     // string emailIndex = emailInput.text;
    //     // Aplicante _aplicante = APIHelper.GetNewName(emailIndex);
    //     // int idUser = _aplicante.idAplicante;

    //     // Debug.Log(idUser);

    //     if (nameInput.text == "" || emailInput.text == "" )
    //     {
    //         Message.text = "Please fill all fields ";
    //         return;
    //     }

    //     Debug.Log(nameInput.text);
    //     Debug.Log(emailInput.text);
        
        
    //     APILogin = APILogin + "nombreUnity=" + nameInput.text + "&correoUnity=" + emailInput.text;
    //     UnityWebRequest API = UnityWebRequest.Get(APILogin);

    //    //yield return API.SendWebRequest();
        
    //     if( API.isHttpError){
    //         Message.text = ":(";
    //         return;
    //     } else {
    //         Message.text = "se logró";
            
    //         return;
    //     }
    // }

    
      // https://www.codegrepper.com/code-examples/csharp/unitywebrequest+example
    /*IEnumerator SubmitLogin()
    {   
        if (nameInput.text == "" || emailInput.text == "" )
        {
            Message.text = "Please fill all fields ";
        }
        
        APILogin = APILogin + "nombreUnity=" + nameInput.text + "&correoUnity=" + emailInput.text;
        UnityWebRequest API = UnityWebRequest.Get(APILogin);

        yield return API.SendWebRequest();
        
        JSONNode apiInfo = JSON.Parse(API.downloadHandler.text);
        if( API.isHttpError){
            Message.text = ":(";
        } else{
            Message.text = "se logró";
        }

    }
    
    IEnumerator SubmitLogin()
    {   
        if (nameInput.text == "" || emailInput.text == "" )
        {
            Message.text = "Please fill all fields ";
        }
        
        APILogin = APILogin + "nombreUnity=" + nameInput.text + "&correoUnity=" + emailInput.text;
        UnityWebRequest API = UnityWebRequest.Get(APILogin);

        yield return API.SendWebRequest();
        
        JSONNode apiInfo = JSON.Parse(API.downloadHandler.text);

        if( apiInfo["statusCode"] = 200){
            string pokeName = apiInfo["reasonPhrase"];
            Message.text = pokeName;
        } else{
            string pokeName = apiInfo["reasonPhrase"];
            Message.text = pokeName;
        }
        
    
    }*/
    
    
    
}

