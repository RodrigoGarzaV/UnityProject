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
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void Regresar()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
