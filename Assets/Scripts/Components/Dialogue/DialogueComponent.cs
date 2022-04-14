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
        if(TryGetComponent<Combat>(out Combat combat))
        {
            combat.enabled = false;
        }

        if(inkJSON == null)
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void TriggerDialogue()
    {
        if(inkJSON != null)
        {
            DialogueManager.GetInstance().EnterDialogueMode(gameObject);
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
