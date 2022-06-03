using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script stores every dialogue conversation in a public Dictionary.*/

public class Dialogue : MonoBehaviour
{
    public Dictionary<string, string[]> dialogue = new Dictionary<string, string[]>();

    void Start()
    {
        //Door
        dialogue.Add("LockedDoorA", new string[] {
            "A large door...",
            "Looks like it has a key hole!"
        });


        dialogue.Add("LockedDoorB", new string[] {
            "Key used!"
        });

        //NPC
        dialogue.Add("CharacterA", new string[] {
            "Hi there!",
            "I'm an NPC! This conversation is called 'npcA'...",
            "If you go and find me 80 coins, my dialogue will move on to 'npcB'!",
            "Feel free to edit my dialogue in the 'Dialogue.cs' file!",
            "To keep it simple, you can also ask me one, and only one, question...",
            "...Like you just did! And I'll just move on to the next sentence.",
            "I'll answer that question, but it won't change much about the game!",
            "You can always tweak the 'DialogueBox.cs' script to add more functionality!"
        });

        dialogue.Add("CharacterAChoice1", new string[] {
            "",
            "",
            "Let me go find some coins!",
        });

        dialogue.Add("CharacterAChoice2", new string[] {
            "",
            "",
            "What else can you do?"
        });

        dialogue.Add("CharacterB", new string[] {
            "Hey! You found 80 coins! That means 'npcB' is now being used inside 'Dialogue.cs'!",
            "After my dialogue completes, I'll take 80 coins, or however many you specify in the inspector...",
            "And I'll also give you a new ability!",
            "In this case, how about a generic DOWNWARD SMASH? Simply attack while pressing down in mid-air!"
        });

        dialogue.Add("TestDialogue", new string[] {
            "This is a test of the dialogue function",
            "If this works I should be saying the next line",
            "And if this works, you should have 2 options on screen",
            "What."
        });

        dialogue.Add("TestDialogueChoice1", new string[] {
            "",
            "",
            "Tu mama"
        });

        dialogue.Add("TestDialogueChoice2", new string[] {
            "",
            "",
            "Pero si le ponen la cancion"
        });

        dialogue.Add("BugText", new string[] {
            "There's a bug in the system.",
            "Attempt to fix?",
            "Loading..."
        });

        dialogue.Add("BugTextChoice1", new string[] {
            "",
            "Later"
        });

        dialogue.Add("BugTextChoice2", new string[] {
            "",
            "Yes"
        });

        dialogue.Add("CompText", new string[] {
            "Los jugadores necesitan tu ayuda.",
            "Tendras que entrar al sistema y eliminar todos los bugs.",
            "Entrar al mundo virtual?",
            "Cargando..."
        });

        dialogue.Add("CompTextChoice1", new string[] {
            "",
            "",
            "Luego."
        });

        dialogue.Add("CompTextChoice2", new string[] {
            "",
            "",
            "Si."
        });

        dialogue.Add("DemoText", new string[] {
            "..."
        });









    }
}
