using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class InterfaceCreator : MonoBehaviour
{

    public GameObject buttonPrefab;
    private List<GameObject> Buttons = new List<GameObject>();
    private Text intitule;
    private Xml scenario;

    public void Launch(Xml scenario)
    {
        this.scenario = scenario;
        intitule = this.GetComponentInChildren<Text>();
        intitule.text = scenario.Accueil.Texte;
        int i = 0;
        
        foreach (int choix in scenario.Accueil.Suivants)
        {
            Buttons.Add(CreateButton(scenario.Questions[choix], new Vector3(0, 40 - (i++ * 40), 0), false));
        }
    }

    GameObject CreateButton(Question question, Vector3 position, Boolean precedent)
    {
        GameObject button = Instantiate(buttonPrefab, this.transform.Find("Canvas"));
        button.GetComponentInChildren<Text>().text = precedent? "Precedent" : question.Intitule;
        RectTransform bTransform = button.GetComponent<RectTransform>();
        bTransform.localPosition = position;
        bTransform.sizeDelta = new Vector2(300, 30);
        Button handler = button.GetComponent<Button>();
        handler.onClick.AddListener(() => ButtonClicked(question));
        return button;
    }

    void ButtonClicked(Question question)
    {
        foreach (GameObject b in Buttons)
            Destroy(b);
        intitule.text = question.Reponse;
        int i = 0;
        foreach (int suivant in question.Suivants)
        {
            Buttons.Add(CreateButton(scenario.Questions[suivant], new Vector3(0, 30 - (i++ * 40), 0), false));
        }
        if(question.Id != 0)
            Buttons.Add(CreateButton(scenario.Questions[question.Precedent], new Vector3(0, 30 - (i++ * 40), 0), true));
    }
}
