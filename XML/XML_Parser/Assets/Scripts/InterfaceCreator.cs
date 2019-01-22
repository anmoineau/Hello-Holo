﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;


public class InterfaceCreator : MonoBehaviour
{

    public GameObject buttonPrefab;
    private List<GameObject> Buttons = new List<GameObject>();
    private Text intitule;
    private Xml scenario;
    private int buttonWidth = 350;
    private int buttonHeight = 40;
    private int maxButton = 8;
    private Boolean initialized = false;

    public void Launch(Xml scenario)
    {
        this.scenario = scenario;
        intitule = this.GetComponentInChildren<Text>();
        intitule.text = scenario.Accueil.Texte;
        SetInterface(scenario.Accueil.Suivants);
        initialized = true;
    }

    private void SetInterface(List<int> IDs, Boolean isPrecedent = false, int posStart = 0)
    {
        if(IDs.Count < maxButton)
        {
            int i = posStart;
            foreach (int ID in IDs)
            {
                Question newQuestion = scenario.Questions.First(q => q.Id == ID);
                float newHeight = intitule.rectTransform.localPosition[1] - buttonHeight * 1.5f - (i++ * buttonHeight * 1.1f);
                Vector3 newPosition = new Vector3(0, newHeight, 0);
                Buttons.Add(CreateButton(newQuestion, newPosition, isPrecedent));
            }
        }
        else
        {
            intitule.text = "Erreur format scénario";
        }
    }

    private GameObject CreateButton(Question question, Vector3 position, Boolean isPrecedent)
    {
        GameObject button = Instantiate(buttonPrefab, this.transform.Find("Canvas"));
        button.GetComponentInChildren<Text>().text = isPrecedent? "Precedent" : question.Intitule;
        RectTransform bTransform = button.GetComponent<RectTransform>();
        bTransform.localPosition = position;
        bTransform.sizeDelta = new Vector2(buttonWidth, buttonHeight);
        Button handler = button.GetComponent<Button>();
        handler.onClick.AddListener(() => ButtonClicked(question));
        return button;
    }

    private void ButtonClicked(Question question)
    {
        foreach (GameObject b in Buttons)
            Destroy(b);
        intitule.text = question.Reponse;
        SetInterface(question.Suivants);
        if (question.Id != 0)
        {
            List<int> prec = new List<int>();
            prec.Add(question.Precedent);
            SetInterface(prec, true, question.Suivants.Count);
        }
    }

    private void Update()
    {
        if (initialized)
        {
            foreach (Info info in scenario.Informations.Where(i => i.Horaires.Count != 0))
            {
                foreach (string horaire in info.Horaires)
                {
                    int retard = DateTime.Compare(DateTime.Now, DateTime.Parse(info.Horaires[0], CultureInfo.CreateSpecificCulture("fr-FR")));
                    if (retard > 0)
                    {
                        Debug.Log(info.Texte);
                    }
                }
            }
        }
    }
}
