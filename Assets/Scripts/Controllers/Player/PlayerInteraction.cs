using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Combat combat;
    private PlayerDialogue dialogue;

    public GameObject grass;
    public GameObject trees;
    public GameObject music;

    void Start()
    {
        combat = GetComponent<Combat>();
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
            combat.RangedAttack(hit.transform.position);
        }

        else if(hit.collider.CompareTag("NPC"))
        {
            dialogue.InitiateDialogue(hit);
        }
        else if(hit.collider.CompareTag("Gargoyle")) {
            InitiateEvilMode();
        }
    }

    public void InitiateEvilMode()
    {
        foreach (SpriteRenderer tile in grass.GetComponentsInChildren<SpriteRenderer>()) {
            tile.color = new Color32(111, 7, 0, 255);
        }

        foreach (SpriteRenderer tree in trees.GetComponentsInChildren<SpriteRenderer>())
        {
            tree.color = new Color32(111, 7, 0, 255);
        }

        GetComponentInChildren<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
        GetComponent<Stats>().resolve = -1000;
        music.GetComponent<Music>().PlayNewMusic();
    }
}
