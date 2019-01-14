using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;  

public class XML_Serializer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var serializer = new XmlSerializer(typeof(Xml));
        var stream = File.Open("Assets/Scenarios/test.xml", FileMode.Open);
        var container = (Xml)serializer.Deserialize(stream);
        Debug.Log(container.Questions[0].Intitule + "\n");
        stream.Close();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
