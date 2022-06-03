using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/*Controls the dialogue box and it's communication with Dialogue.cs, which contains the character dialogue*/

public class DialogueBoxController : MonoBehaviour
{

    [Header("References")]
    public Animator animator;
    [SerializeField] Dialogue dialogue;
    private DialogueTrigger currentDialogueTrigger;

    [Header("Text Mesh Pro")]
    [SerializeField] TextMeshProUGUI choice1Mesh;
    [SerializeField] TextMeshProUGUI choice2Mesh;
    [SerializeField] TextMeshProUGUI nameMesh;
    [SerializeField] TextMeshProUGUI textMesh;

    [Header("Other")]
    private bool ableToAdvance;
    private bool activated;
    private int choiceLocation;
    private int cPos = 0;
    private string[] characterDiologue;
    private string[] choiceDiologue;
    private DialogueTrigger dialogueTrigger;
    [System.NonSerialized] public bool extendConvo;
    private string fileName;
    private int index = -1;
    private bool repeat;
    private bool horizontalKeyIsDown = true;
    private bool submitKeyIsDown = true;
    private bool typing = true;
    private bool sceneChange;
    private bool dialogueNextScene;
    private string nextScene;


    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            //Submit
            //Check for key press
            if (((Input.GetAxis("Submit") > 0) || (Input.GetAxis("Jump") > 0)) && !submitKeyIsDown)
            {
                submitKeyIsDown = true;
                if (!typing)
                {
                    if (index < choiceLocation || (extendConvo && index < characterDiologue.Length - 1))
                    {
                        if (ableToAdvance)
                        {
                            StartCoroutine(Advance());
                        }
                    }
                    else
                    {
                        StartCoroutine(Close());
                    }
                    if (index == 0)
                    {
                        ableToAdvance = true;
                    }
                }
            }

            //Check for first release to ensure we can't spam
            if (submitKeyIsDown && Input.GetAxis("Submit") < .001 && Input.GetAxis("Jump") < .001)
            {
                if (!typing)
                {
                    submitKeyIsDown = false;
                    if (index == 0)
                    {
                        ableToAdvance = true;
                    }
                }
            }

            //Choices
            //Check for key press
            if ((Input.GetAxis("Horizontal") != 0) && !horizontalKeyIsDown && animator.GetBool("hasChoices") == true)
            {
                if (animator.GetInteger("choiceSelection") == 1)
                {
                    animator.SetInteger("choiceSelection", 2);
                    extendConvo = true;
                }
                else
                {
                    extendConvo = false;
                    animator.SetInteger("choiceSelection", 1);
                }
                horizontalKeyIsDown = true;
            }

            //Check for first release to ensure we can't spam
            if (horizontalKeyIsDown && Input.GetAxis("Horizontal") == 0)
            {
                horizontalKeyIsDown = false;
            }
        }
    }

    // Llama el dialogo inicial
    public void Appear(string fName, string characterName, DialogueTrigger dTrigger, bool sChange, string nScene, bool r)
    {
        repeat = r;
        sceneChange = sChange;
        nextScene = nScene;
        dialogueTrigger = dTrigger;
        choice1Mesh.text = "";
        choice2Mesh.text = "";
        fileName = fName;


        nameMesh.text = characterName;
        characterDiologue = dialogue.dialogue[fileName];


        if (dialogue.dialogue.ContainsKey(fileName + "Choice1"))
        {
            choiceDiologue = dialogue.dialogue[fileName + "Choice1"];
            choiceLocation = GetChoiceLocation();
        }
        else
        {
            choiceLocation = characterDiologue.Length - 1;
        }

        animator.SetBool("active", true);
        activated = true;
        PlatformerPlayer.Instance.Freeze(true);
        StartCoroutine(Advance());
    }

    IEnumerator Close()
    {

        if (extendConvo)
        {
            dialogueNextScene = true;
        }

        activated = false;
        animator.SetBool("active", false);
        StopCoroutine("TypeText");
        index = -1;
        submitKeyIsDown = false;
        ableToAdvance = false;
        extendConvo = false;
        choiceLocation = 0;
        ShowChoices(false);

        if (!repeat)
        {
            dialogueTrigger.completed = true;
        }


        dialogueTrigger = null;
        yield return new WaitForSeconds(1f);
        PlatformerPlayer.Instance.Freeze(false);
        animator.SetInteger("choiceSelection", 1);

        if (sceneChange && dialogueNextScene)
        {
            SceneManager.LoadScene("ClickandDrag");
        }


    }

    IEnumerator Advance()
    {
        index++;
        typing = true;

        if (ableToAdvance)
        {
            animator.SetTrigger("press");
        }

        if (index != choiceLocation)
        {
            ShowChoices(false);
        }

        textMesh.text = "";
        StartCoroutine("TypeText");

        //Wait before typing
        yield return new WaitForSeconds(.4f);

        //Show choices
        if (index == choiceLocation && dialogue.dialogue.ContainsKey(fileName + "Choice1"))
        {
            ShowChoices(true);
        }

    }

    IEnumerator TypeText()
    {
        WaitForSeconds wait = new WaitForSeconds(.01f);
        foreach (char c in characterDiologue[index])
        {
            cPos++;
            if (cPos != 0 && cPos == characterDiologue[index].Length)
            {
                typing = false;
                cPos = 0;
            }

            textMesh.text += c;
            yield return wait;
        }
    }

    public int GetChoiceLocation()
    {
        for (int i = 0; i < choiceDiologue.Length; i++)
        {
            if (choiceDiologue[i] != "")
            {
                return i;
            }
        }
        return 0;
    }

    void ShowChoices(bool show)
    {
        animator.SetBool("hasChoices", show);
        if (show)
        {
            choice1Mesh.text = dialogue.dialogue[fileName + "Choice1"][choiceLocation];
            choice2Mesh.text = dialogue.dialogue[fileName + "Choice2"][choiceLocation];
        }
    }
}
