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
    public string Reponse { get; set; }

    [XmlElement(ElementName = "suivant")]
    public List<int> Suivants { get; set; }

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

[XmlRoot(ElementName = "information")]
public class Info
{
    [XmlElement(ElementName = "intitule")]
    public string Texte { get; set; }

    [XmlElement(ElementName = "occurence")]
    public List<Occurence> Ocurrences { get; set; }

    [XmlElement(ElementName = "frequence")]
    public float Frequence { get; set; }

    [XmlElement(ElementName = "duree")]
    public float Duree { get; set; }
}

[XmlRoot(ElementName = "occurence")]
public class Occurence
{
    [XmlElement(ElementName = "horaire")]
    public string Horaire { get; set; }

    [XmlElement(ElementName = "frequence")]
    public float Periode { get; set; }

    [XmlElement(ElementName = "duree")]
    public float Duree { get; set; }

    [XmlElement(ElementName = "active")]
    public bool Active { get; set; }
}

[XmlRoot(ElementName = "xml")]
public class Xml
{
    [XmlElement(ElementName = "question")]
    public List<Question> Questions { get; set; }
    [XmlElement(ElementName = "intro")]
    public Intro Accueil { get; set; }
    [XmlElement(ElementName = "information")]
    public List<Info> Informations { get; set; }
}

