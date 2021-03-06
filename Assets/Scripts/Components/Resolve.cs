using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolve : MonoBehaviour
{
    private Stats stats;

    private Combat combat;
    private NpcTargeting targetting;
    private float resolve;
    public bool isEnemy = false;
    public float enemyThreshold = 55;
    public float scanRange = 5f;
    public float agroRange = 10f;

    private void Start()
    {
        stats = GetComponent<Stats>();
        combat = GetComponent<Combat>();
        targetting = GetComponent<NpcTargeting>();
        resolve = stats.resolve;
    }

    private void Update()
    {
        if (targetting.target == null)
        {
            if (CheckAlignment().Count == 0 && !isEnemy)
            {
                //targetting.enabled = false;
                combat.enabled = false;
                tag = "NPC";
            }

            else
            {
                targetting.enabled = true;
                combat.enabled = true;
                targetting.LoadTargets(CheckAlignment());

                if (isEnemy)
                {
                    tag = "Enemy";
                }

                else
                {
                    tag = "NPC";
                }
            }
        }
    }

    public ArrayList CheckAlignment()
    {
        ArrayList enemies = new ArrayList();

        foreach (Collider2D obj in Physics2D.OverlapCircleAll(transform.position, scanRange)) {
            if(obj.gameObject.TryGetComponent(out Stats stats))
            {
                if (Mathf.Abs(resolve - stats.resolve) > enemyThreshold && (Vector3.Distance(transform.position, obj.transform.position) < combat.agroRange))
                {
                    if(obj.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMove))
                    {
                        isEnemy = true;
                    }
                    enemies.Add(obj.gameObject);
                }

                else if(obj.gameObject.TryGetComponent<NpcTargeting>(out NpcTargeting otherTargetting) && Mathf.Abs(resolve - stats.resolve) < 20)
                {
                    if(otherTargetting.target != null && Vector3.Distance(otherTargetting.target.transform.position, transform.position) < agroRange)
                    {
                        enemies.Add(otherTargetting.target);

                        if(otherTargetting.target.CompareTag("Player"))
                        {
                            isEnemy = true;
                        }
                    } 
                }
            }  
        }

        return enemies;
    }
}
