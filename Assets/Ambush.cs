using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambush : MonoBehaviour
{
    public GameObject player;
    public GameObject ambushPoint;

    private MovementComponent movement;
    private Combat combat;
    private Resolve resolve;
    private BoxCollider2D collider;

    public string ambushPointDesignation;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<MovementComponent>();
        combat = GetComponent<Combat>();
        resolve = GetComponent<Resolve>();
        collider = GetComponent<BoxCollider2D>();
        movement.enabled = false;
        combat.enabled = false;
        resolve.enabled = false;
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(ambushPointDesignation)
        {
            case "right":
                if(player.transform.position.x > ambushPoint.transform.position.x)
                {
                    InitiateAmbush();
                }
                break;

            case "left":
                if (player.transform.position.x < ambushPoint.transform.position.x)
                {
                    InitiateAmbush();
                }
                break;

            case "above":
                if (player.transform.position.y > ambushPoint.transform.position.y)
                {
                    InitiateAmbush();
                }
                break;

            case "below":
                if (player.transform.position.y < ambushPoint.transform.position.y)
                {
                    InitiateAmbush();
                }
                break;
        }
    }

    private void InitiateAmbush()
    {
        movement.enabled = true;
        combat.enabled = true;
        resolve.enabled = true;
        collider.enabled = true;
        GetComponent<Ambush>().enabled = false;
    }
}
