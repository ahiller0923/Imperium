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
        gameObject.GetComponent<EnemyMovement>().enabled = false;
        gameObject.GetComponent<Combat>().enabled = false;
    }

    private void Update()
    {
        if(DialogueManager.GetInstance().CheckHostility())
        {
            gameObject.GetComponent<EnemyMovement>().enabled = true;
            gameObject.GetComponent<Combat>().enabled = true;
            gameObject.tag = "Enemy";
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, name);
    }
}
