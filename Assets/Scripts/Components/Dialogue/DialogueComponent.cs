using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueComponent : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] public TextAsset inkJSON;
    public string name;

    private void Start()
    {
        gameObject.GetComponent<Combat>().enabled = false;
    }

    public void TriggerDialogue()
    {
        if(inkJSON != null)
        {
            DialogueManager.GetInstance().EnterDialogueMode(gameObject);
        }
    }
}
