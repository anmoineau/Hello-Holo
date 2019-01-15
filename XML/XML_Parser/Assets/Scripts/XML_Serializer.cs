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
        var stream = File.Open("Assets/Scenarios/test.xml", FileMode.Open);
        var scenario = (Xml)serializer.Deserialize(stream);
        stream.Close();

        //Instanciation de l'interface
        Question Q1 = scenario.Questions[0];
        Text intitule = ui.GetComponentInChildren<Text>();
        intitule.text = Q1.Intitule;
        int i = 0;
        List<GameObject> newButtons = new List<GameObject>(); 
        foreach(String proposition in Q1.Propositions)
        {
            newButtons.Add(CreateButton(proposition , new Vector3(0, 60 - (i++ * 30), 0)));
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
        return button;
    }
}
