using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
    [SerializeField] GameObject questionsMenu;

    void Start(){

    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Q)){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            questionsMenu.gameObject.SetActive(!questionsMenu.gameObject.activeSelf);
            // questionsMenu.gameObject.SetActive(true);
        }
    }

    public void Send(){
        questionsMenu.gameObject.SetActive(!questionsMenu.gameObject.activeSelf); 
    }
}
