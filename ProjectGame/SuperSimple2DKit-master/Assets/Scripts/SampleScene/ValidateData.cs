using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using SimpleJSON;
using UnityEngine.Networking;

public class ValidateData : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField emailInput;
    public TextMeshProUGUI Message;

    //public TMP_Text name;

    private string APILogin = "https://localhost:5001/api/login?";


    private void Start() {
        Message.text = "";
        //name.text = PlayerPrefs.GetString("nameInput");
    }
    
    public void SubmitLogin()
    {   
        // string emailIndex = emailInput.text;
        // Aplicante _aplicante = APIHelper.GetNewName(emailIndex);
        // int idUser = _aplicante.idAplicante;

        // Debug.Log(idUser);

        if (nameInput.text == "" || emailInput.text == "" )
        {
            Message.text = "Please fill all fields ";
            return;
        }

        Debug.Log(nameInput.text);
        Debug.Log(emailInput.text);
        
        
        APILogin = APILogin + "nombreUnity=" + nameInput.text + "&correoUnity=" + emailInput.text;
        UnityWebRequest API = UnityWebRequest.Get(APILogin);

       //yield return API.SendWebRequest();
        
        if( API.isHttpError){
            Message.text = ":(";
            return;
        } else {
            Message.text = "se logró";
            
            return;
        }
    }

    
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

