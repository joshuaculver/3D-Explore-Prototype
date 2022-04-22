using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot ("scene")]
public class Scene
{
    [XmlAttribute ("id")]
    public string id {get; set;}

    [XmlElement("dialogue")]
    public Dialogue[] dialogues;
}

public class SceneCollection
{
    [XmlElement("scene")]
    public List<Scene> SceneList = new List<Scene>();
}