using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/*Manages and updates the HUD, which contains your health bar, bugs, etc*/

public class HUD : MonoBehaviour
{
    [Header ("Reference")]
    public Animator animator;
    public TextMeshProUGUI bugsMesh;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Image inventoryItemGraphic;
    [SerializeField] private GameObject startUp;

    [System.NonSerialized] public Sprite blankUI; //The sprite that is shown in the UI when you don't have any items
    private float bugs;
    private float bugsEased;
    private float healthBarWidth;
    private float healthBarWidthEased;
    [System.NonSerialized] public string loadSceneName;
    [System.NonSerialized] public bool resetPlayer;




    void Start()
    {
        //Set all bar widths to 1, and also the smooth variables.

        healthBarWidth = 1;
        healthBarWidthEased = healthBarWidth;
        bugs = (float)PlatformerPlayer.Instance.bugs;
        bugsEased = bugs;
        blankUI = inventoryItemGraphic.GetComponent<Image>().sprite;
    }

    void Update()
    {
        //Update bugs text mesh to reflect how many bugs the player has! However, we want them to count up.
        bugsMesh.text = Mathf.Round(bugsEased).ToString();
        bugsEased += ((float)PlatformerPlayer.Instance.bugs - bugsEased) * Time.deltaTime * 5f;

        if (bugsEased >= bugs)
        {
            animator.SetTrigger("getGem");
            bugs = bugsEased + 1;
        }

        //Controls the width of the health bar based on the player's total health
        healthBarWidth = (float)PlatformerPlayer.Instance.health / (float)PlatformerPlayer.Instance.maxHealth;
        healthBarWidthEased += (healthBarWidth - healthBarWidthEased) * Time.deltaTime * healthBarWidthEased;
        healthBar.transform.localScale = new Vector2(healthBarWidthEased, 1);

 
    }

    public void HealthBarHurt()
    {
        animator.SetTrigger("hurt");
    }

    public void SetInventoryImage(Sprite image)
    {
        inventoryItemGraphic.sprite = image;
    }

    void ResetScene()
    {
        if (GameManager.Instance.inventory.ContainsKey("reachedCheckpoint"))
        {
            //Send player back to the checkpoint if they reached one!
            PlatformerPlayer.Instance.ResetLevel();
        }
        else
        {
            //Reload entire scene
            SceneManager.LoadScene(loadSceneName);
        }
    }

}
