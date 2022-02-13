using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerCombat combat;
    public RaycastHit2D target;

    void Start()
    {
        combat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessInteraction(RaycastHit2D hit)
    {
        if(hit.collider.CompareTag("Enemy"))
        {
            target = hit;
            combat.Attack(hit);
        }
    }
}
