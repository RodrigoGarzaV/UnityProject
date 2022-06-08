using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Triggers a dialogue conversation, passing unique commands and information to the dialogue box and inventory system for fetch quests, etc.*/

public class DialogueTrigger : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private GameObject finishTalkingActivateObject; //After completing a conversation, an object can activate. 
    [SerializeField] private Animator iconAnimator; //The E icon animator

    [Header("Trigger")]
    [SerializeField] private bool autoHit; //Does the player need to press the interact button, or will it simply fire automatically?
    public bool completed;
    [SerializeField] private bool repeat; //Set to true if the player should be able to talk again and again to the NPC. 
    [SerializeField] private bool sleeping;
    [SerializeField] private bool sceneChange;
    


    [Header ("Dialogue")]
    [SerializeField] private string characterName; //The character's name shown in the dialogue UI
    [SerializeField] private string dialogueStringA; //The dialogue string that occurs before the fetch quest
    [SerializeField] private string dialogueStringB; //The dialogue string that occurs after fetch quest
    [SerializeField] private string nextScene;
    [SerializeField] private AudioClip[] audioLinesA; //The audio lines that occurs before the fetch quest    
    [SerializeField] private AudioClip[] audioLinesB; //The audio lines that occur after the fetch quest
    [SerializeField] private AudioClip[] audioChoices; //The audio lines that occur when selecting an audio choice


    [Header ("Presentacion")]
    private Scene Scene1;
    private Scene Scene2;
    private Scene currentScene;
    


    //  GETAXIS(SUBMIT) = TECLA E. Se cambia en Edit > Project Settings > Input Manager > Axes


    // col.gameObject == player.gameObject


    void Start(){

        Scene1 = SceneManager.GetSceneByName("Juego");
        Scene2 = SceneManager.GetSceneByName("VirtualWorld");
        currentScene = SceneManager.GetActiveScene();

    }
    
    void OnTriggerStay2D(Collider2D col)
    {

        // Si player esta cerca
        if ( (col.gameObject == PlatformerPlayer.Instance.gameObject && !sleeping && !completed && PlatformerPlayer.Instance.grounded ) || (col.tag == "Player") )
        {
            // Icon de interaccion se despliega
            iconAnimator.SetBool("active", true);
            // ... y pica "E"
            if ((Input.GetAxis("Submit") > 0))
            {
                // Se desactiva icon
                iconAnimator.SetBool("active", false);

                // // funcion de presentacion lol
                // if (currentScene == Scene1){
                // SceneManager.LoadScene("VirtualWorld");
                // }

                // GameManager.Instance.dialogueBoxController.Appear(dialogueStringA, characterName, this, sceneChange, repeat);
                // // Se desactiva la interaccion (?)
                // sleeping = true;
            }
        }
        // else
        // {
        //     iconAnimator.SetBool("active", false);
        // }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == PlatformerPlayer.Instance.gameObject)
        {
            iconAnimator.SetBool("active", false);
            sleeping = completed;
        }
    }
}