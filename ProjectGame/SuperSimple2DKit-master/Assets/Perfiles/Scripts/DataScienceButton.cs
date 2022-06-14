using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataScienceButton : MonoBehaviour
{
    // Start is called before the first frame update

    public void DataScience()
    {
        SceneManager.LoadScene("VirtualWorld");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
