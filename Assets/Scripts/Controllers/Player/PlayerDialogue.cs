using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateDialogue(RaycastHit2D hit) 
    {
        if(hit.collider.gameObject.TryGetComponent<DialogueComponent>(out DialogueComponent dialogue))
        {
            dialogue.TriggerDialogue();
        }
    }
}
