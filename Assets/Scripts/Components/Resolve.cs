using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolve : MonoBehaviour
{
    private Stats stats;

    private Combat combat;
    private NpcTargeting targetting;
    private float resolve;
    private bool isEnemy = false;

    private void Start()
    {
        stats = GetComponent<Stats>();
        combat = GetComponent<Combat>();
        targetting = GetComponent<NpcTargeting>();
        resolve = stats.resolve;
    }

    private void Update()
    {
        isEnemy = false; 

        if(CheckAlignment().Count == 0)
        {
            targetting.enabled = false;
            combat.enabled = false;
            tag = "NPC";
        }

        else
        {
            targetting.enabled = true;
            combat.enabled = true;
            targetting.LoadTargets(CheckAlignment());

            if(isEnemy)
            {
                tag = "Enemy";
            }
            
            else
            {
                tag = "NPC";
            }
        }
    }

    public ArrayList CheckAlignment()
    {
        ArrayList enemies = new ArrayList();

        foreach (Collider2D obj in Physics2D.OverlapCircleAll(transform.position, 20f)) {
            if(obj.gameObject.TryGetComponent(out Stats stats))
            {
                if (Mathf.Abs(resolve - stats.resolve) > 50 && (Vector3.Distance(transform.position, obj.transform.position) < combat.agroRange))
                {
                    if(obj.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMove))
                    {
                        isEnemy = true;
                        Debug.Log("EnemyTime");
                    }
                    enemies.Add(obj.gameObject);
                }
            }  
        }

        return enemies;
    }
}
