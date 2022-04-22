/*
    Manager which handles displaying dialogue boxes, displaying text, and some interaction
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class DialogueManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}

    //Label appears above the dialogue box
    public TMP_Text labelText;
    //Text for the dialogue box itself
    public TMP_Text dialogueText;

    //Animator for dialogue UI
    public Animator animator;

    //Queue used to organize strings to display in the dialogue box
    private Queue<string> sentences;
    //Object which holds dialogue objects retrieved from an xml file
    private DialogueCollection dialogueContainer;

    //Initializes manager
    public void Startup()
    {
        //Initializing manager
        Debug.Log("Dialogue manager starting...");
        sentences = new Queue<string>();

        //Generates path to text.xml
        string[] targets = {Application.dataPath, "XML", "Dialogue.xml"};
        string fullPath = Path.Combine(targets);

        //Gets dialogue from text.xml and stores it in dialogue container
        XmlSerializer deserializer = new XmlSerializer(typeof(DialogueCollection));
        TextReader reader = new StreamReader(fullPath);
        object obj = deserializer.Deserialize(reader);
        dialogueContainer = (DialogueCollection)obj;
        reader.Close();

        //Finished
        status = ManagerStatus.Started;
    }

    //Takes a dialogue object and displays it
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        Managers.Player.Hold();

        //Sets label/speaker for dialogue box
        labelText.text = dialogue.label;

        //clears any left over sentences
        sentences.Clear();

        //queues sentences from dialogue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Debug.Log("Sentences from dialogue: " + sentences.Count);

        DisplayNextSentence();
    }

    //Ends dialogue if no more remains or begins displaying next sentence from dialogue
    public void DisplayNextSentence()
    {
        Debug.Log("Sentences to display: " + sentences.Count);
        //Is there still dialogue to display?
        if (sentences.Count == 0)
        {
            //No more dialogue
            EndDialogue();
            return;
        }

        //Get next line for dialogue
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Used to make text be displayed one character at a time 
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            //Wait time between characters being displayed
            yield return new WaitForSeconds(.08f);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Managers.Player.Release();
    }

    //Finds a dialogue from the DialogueContainer using the "id" xml attribute
    public Dialogue FindByID(string get)
    {
        Dialogue dialogue = dialogueContainer.dialogueList.Find(x => x.id == get);
        return dialogue;
    }
    
}
