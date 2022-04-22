/*
    Object which has it's Interact function called by other objects
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //ID of dialogue to play
    public string id;

    public void Interact()
    {
        Dialogue dialogue = Managers.Dialogue.FindByID(id);
        Managers.Dialogue.StartDialogue(dialogue);
    } 
}
