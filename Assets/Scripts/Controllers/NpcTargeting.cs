using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTargeting : MonoBehaviour
{

    private Queue<GameObject> targets;
    private GameObject target;
    private Combat combat;
    private MovementComponent movement;

    public bool isRanged;

    private float distanceFromPlayer;
    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<Combat>();
        movement = GetComponent<MovementComponent>();
        targets = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null && targets.Count > 0)
        {
            NewTarget();
        }

        if(target != null)
        {
            CheckAttack();
        }
    }

    public void LoadTargets(ArrayList enemies)
    {
        targets = new Queue<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            targets.Enqueue(enemy);
        }
    }

    private void NewTarget()
    {
        target = targets.Dequeue();
    }

    private void CheckAttack()
    {
        distanceFromPlayer = Vector3.Distance(target.transform.position, transform.position);

        if (distanceFromPlayer < combat.attackRange)
        {
            if (isRanged)
            {
                combat.RangedAttack(target.transform.position);
            }
            else
            {
                combat.MeleeAttack(target);
            }
        }

        else if (distanceFromPlayer < combat.agroRange)
        {
            movement.SetTarget(target.transform.position);
        }

        else
        {
            NewTarget();
        }
    }
}
