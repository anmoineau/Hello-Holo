using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot(ElementName = "question")]
public class Question
{
    [XmlElement(ElementName="id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "intitule")]
    public string Intitule { get; set; }

    [XmlElement(ElementName = "reponse")]
    public List<string> Reponses { get; set; }

    [XmlElement(ElementName = "suivant")]
    public List<int> Suivant { get; set; }

    [XmlElement(ElementName = "precedent")]
    public int Precedent { get; set; }
}

[XmlRoot(ElementName = "intro")]
public class Intro
{
    [XmlElement(ElementName = "texte")]
    public string Texte { get; set; }

    [XmlElement(ElementName = "suivant")]
    public List<int> Suivants { get; set; }
}

[XmlRoot(ElementName = "xml")]
public class Xml
{
    [XmlElement(ElementName = "question")]
    public List<Question> Questions { get; set; }
    [XmlElement(ElementName = "intro")]
    public Intro Accueil { get; set; }
}

