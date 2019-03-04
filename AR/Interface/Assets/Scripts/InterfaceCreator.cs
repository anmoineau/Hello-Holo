using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;


public class InterfaceCreator : MonoBehaviour
{
    public GameObject buttonPrefab;
    private List<GameObject> Buttons = new List<GameObject>();
    private Text intituTexte;
    private Text infoTexte;
    private Xml scenario;
    private int buttonWidth = 350;
    private int buttonHeight = 40;
    private int maxButton = 8;
    private bool initialized = false;

    public void Launch(Xml scenario)
    {
        this.scenario = scenario;
        intituTexte = this.GetComponentsInChildren<Text>()[0];
        infoTexte = this.GetComponentsInChildren<Text>()[1];
        intituTexte.text = scenario.Accueil.Texte;
        SetInterface(scenario.Accueil.Suivants);
        StartPeriodic();
        initialized = true;
    }

    private void SetInterface(List<int> IDs, bool isPrecedent = false, int posStart = 0)
    {
        if(IDs.Count < maxButton)
        {
            int i = posStart;
            foreach (int ID in IDs)
            {
                Question newQuestion = scenario.Questions.First(q => q.Id == ID);
                float newHeight = intituTexte.transform.parent.localPosition[1]- 65 - (i++ * buttonHeight * 1.1f);
                Vector3 newPosition = new Vector3(0, newHeight, 0);
                Buttons.Add(CreateButton(newQuestion, newPosition, isPrecedent));
            }
        }
        else
        {
            intituTexte.text = "Erreur format scénario";
        }
    }

    private GameObject CreateButton(Question question, Vector3 position, bool isPrecedent)
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
        intituTexte.text = question.Reponse;
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
            int start;
            DateTime startTime;
            int stop;
            DateTime stopTime;
            foreach (Info info in scenario.Informations.Where(i => i.Ocurrences.Count != 0))
            {
                foreach (Occurence occurenceH in info.Ocurrences.Where(i => i.Horaire != null))
                {
                        startTime = DateTime.Parse(occurenceH.Horaire);
                        stopTime = startTime.AddSeconds(occurenceH.Duree);
                        start = DateTime.Compare(DateTime.Now, startTime);
                        stop = DateTime.Compare(DateTime.Now, stopTime);
                        if (stop >= 0)
                        {
                            infoTexte.text = infoTexte.text.Replace(" - " + info.Texte + "\n", "");
                        } else if (start >= 0 && occurenceH.Active)
                        {
                            occurenceH.Active = false;
                            infoTexte.text = infoTexte.text + " - " + info.Texte + "\n";
                        }
                }
            }
        }
    }

    private void StartPeriodic()
    {
        foreach (Info info in scenario.Informations.Where(i => i.Ocurrences.Count != 0))
        {
            foreach (Occurence occurenceF in info.Ocurrences.Where(i => i.Periode != 0))
            {
                StartCoroutine(WaitAndPrint(info.Texte, occurenceF.Duree, occurenceF.Periode));
            }
        }
    }

    IEnumerator WaitAndErase(string _intitule, float _duree, float _periode)
    {
        yield return new WaitForSeconds(_duree);
        infoTexte.text = infoTexte.text.Replace(" - " + _intitule + "\n", "");
        StartCoroutine(WaitAndPrint(_intitule, _duree, _periode));
    }

    IEnumerator WaitAndPrint(string _intitule, float _duree, float _periode)
    {
        yield return new WaitForSeconds(_periode - _duree);
        infoTexte.text = infoTexte.text + " - " + _intitule + "\n";
        StartCoroutine(WaitAndErase(_intitule, _duree, _periode));
    }
}
