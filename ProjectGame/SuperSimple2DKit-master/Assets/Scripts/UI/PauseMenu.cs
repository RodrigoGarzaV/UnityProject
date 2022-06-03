using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Disables the cursor, freezes timeScale and contains functions that the pause menu button can use*/ 

public class PauseMenu : MonoBehaviour
{
    [SerializeField] AudioClip pressSound;
    [SerializeField] AudioClip openSound;
    [SerializeField] GameObject pauseMenu;

    // Use this for initialization
    void OnEnable()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Cursor.visible = false;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
