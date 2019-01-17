using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public class XML_Serializer : MonoBehaviour {

    public GameObject ui;
    public GameObject buttonPrefab;
	// Use this for initialization
	void Start () {
        var serializer = new XmlSerializer(typeof(Xml));
        var stream = File.Open("Assets/Scenarios/Isimatic.xml", FileMode.Open);
        var scenario = (Xml)serializer.Deserialize(stream);
        stream.Close();

        //Instanciation de l'interface
        Text intitule = ui.GetComponentInChildren<Text>();
        intitule.text = scenario.Accueil.Texte;
        int i = 0;
        List<GameObject> newButtons = new List<GameObject>(); 
        foreach(int choix in scenario.Accueil.Suivants)
        {
            newButtons.Add(CreateButton(scenario.Questions[choix].Intitule , new Vector3(0, 30 - (i++ * 40), 0)));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    GameObject CreateButton(String text, Vector3 position)
    {
        GameObject button = GameObject.Instantiate(buttonPrefab, ui.transform.Find("Canvas"));
        button.GetComponentInChildren<Text>().text = text;
        RectTransform bTransform = button.GetComponent<RectTransform>();
        bTransform.localPosition = position;
        bTransform.sizeDelta = new Vector2(300, 30);
        return button;
    }
}
