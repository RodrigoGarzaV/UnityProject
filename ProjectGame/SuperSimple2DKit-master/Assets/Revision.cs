using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Revision : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void correctAnswer(){
        contador.preCorrectas++;
        contador.numPreguntas++;
        if(contador.numPreguntas == 5){
            SceneManager.LoadScene("End");
        }
    }
    public void wrongAnswer(){
        contador.numPreguntas++;
        if(contador.numPreguntas == 5){
            SceneManager.LoadScene("End");
        }
    }
}
