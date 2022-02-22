using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;
using System;

public class DialogueManager : MonoBehaviour
{
    public GameObject player;
    private Stats stats;

    private static DialogueManager instance;

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]

    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    public bool dialogueIsPlaying;

    private Story currentStory;

    private float resolveEffect = 0;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        dialogueText = dialoguePanel.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        dialoguePanel.transform.GetChild(1).GetChild(3).GetComponent<Button>().onClick.AddListener(() => ContinueStory());
        stats = player.GetComponent<Stats>();
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // Create Choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, string name)
    { 
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;

        dialoguePanel.SetActive(true);
        dialoguePanel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = name;

        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialoguePanel.transform.GetChild(1).GetChild(3).gameObject.SetActive(true);
            dialogueText.text = currentStory.Continue();

            DisplayChoices();
        }

        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("UI is not setup to handle number of provided choices: " + currentChoices.Count);
        }

        int index = 0;

        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        if(index != 0)
        {
            dialoguePanel.transform.GetChild(1).GetChild(3).gameObject.SetActive(false);
        }

        for(int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        CheckResolve();
    }

    public void CheckResolve()
    {
        
        if(currentStory != null)
        {
            resolveEffect = float.Parse(currentStory.variablesState.GetVariableWithName("resolve").ToString());
            stats.resolve += resolveEffect;
        }
    }
}
