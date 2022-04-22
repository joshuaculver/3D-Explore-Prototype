/*
    Dialogue and dialogue collection objects. Used to store data from xml file that is used
    for DialogueManager to display dialogue.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]

[XmlRoot ("dialogue")]
public class Dialogue
{
    [XmlAttribute ("id")]
    public string id {get; set;}

    [XmlElement ("label")]
    public string label;

    [TextArea]
    [XmlElement("sentence")]
    public string[] sentences;
}

public class DialogueCollection
{
    [XmlElement("dialogue")]
    public List<Dialogue> dialogueList = new List<Dialogue>();
}