using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueComponent : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    public string name;

    private void Start()
    {
        gameObject.GetComponent<NpcTargeting>().enabled = false;
        gameObject.GetComponent<Combat>().enabled = false;
    }

    public void TriggerDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, name);
    }
}
