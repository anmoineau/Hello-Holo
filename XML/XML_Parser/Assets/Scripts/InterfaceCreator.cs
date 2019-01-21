using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;


public class InterfaceCreator : MonoBehaviour
{

    public GameObject buttonPrefab;
    private List<GameObject> Buttons = new List<GameObject>();
    private Text intitule;
    private Xml scenario;
    private int buttonWidth = 350;
    private int buttonHeight = 40;

    public void Launch(Xml scenario)
    {
        this.scenario = scenario;
        intitule = this.GetComponentInChildren<Text>();
        intitule.text = scenario.Accueil.Texte;
        SetInterface(scenario.Accueil.Suivants);
    }

    void SetInterface(List<int> IDs, Boolean isPrecedent = false, int posStart = 0)
    {
        if(IDs.Count < 8)
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

    GameObject CreateButton(Question question, Vector3 position, Boolean isPrecedent)
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

    void ButtonClicked(Question question)
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
}
