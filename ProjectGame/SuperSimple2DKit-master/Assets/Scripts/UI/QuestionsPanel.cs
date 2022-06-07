using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsPanel : MonoBehaviour
{
    [SerializeField] AudioClip pressSound;
    [SerializeField] AudioClip openSound;
    [SerializeField] GameObject Questions;


    void OnEnable()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
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
}
