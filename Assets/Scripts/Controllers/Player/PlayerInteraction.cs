using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerCombat combat;
    private PlayerDialogue dialogue;

    void Start()
    {
        combat = GetComponent<PlayerCombat>();
        dialogue = GetComponent<PlayerDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessInteraction(RaycastHit2D hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            combat.Attack(hit);
        }

        else if(hit.collider.CompareTag("Docile"))
        {
            dialogue.InitiateDialogue(hit);
        }
    }
}
