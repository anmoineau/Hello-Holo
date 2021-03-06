﻿using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class XML_Serializer : MonoBehaviour {
    InterfaceCreator IC;

	void Start () {
        var serializer = new XmlSerializer(typeof(Scenario));
        var stream = File.Open("Assets/Scenarios/Isimatic.xml", FileMode.Open);
        var scenario = (Scenario)serializer.Deserialize(stream);
        IC = GameObject.Find("Interface").GetComponent<InterfaceCreator>();
        IC.Launch(scenario);
        stream.Dispose();
    }
}
