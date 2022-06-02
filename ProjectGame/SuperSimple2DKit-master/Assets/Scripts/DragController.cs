using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DragController : MonoBehaviour
{
    // Game objects referenced
    [SerializeField] GameObject GameCanvas;
    public TextMeshProUGUI Result;
    [SerializeField] private GameObject answers;
    [SerializeField] private GameObject answerSlots;


    // Inicializa el canvas de Click and Drag
    void Start()
    {
        GameObject answers = GameObject.FindGameObjectWithTag("Correct");
        Result.enabled = false;
        
    }


    public void VerifyAnswers()
    {
        if (Vector3.Distance(answers.transform.position, answerSlots.transform.position) < 10)
        {
            Result.enabled = true;
        }
        
        // verificar que los tags de las respuestas en el click and drag sean correctas
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
