using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private MovementComponent movement;
    private Combat combat;

    public float agroRange = 0;
    public GameObject player;

    private Vector2 playerPos;
    private Vector2 pos;
    private float distanceFromPlayer;

    public bool isRanged = false;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<MovementComponent>();
        combat = GetComponent<Combat>();

        movement.SetTarget(transform.position);
        pos = transform.position;
        playerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        pos = transform.position;
        distanceFromPlayer = Mathf.Sqrt(Mathf.Pow(playerPos.x - pos.x, 2) + Mathf.Pow(playerPos.y - pos.y, 2));

        if(distanceFromPlayer < combat.attackRange)
        {
            if(isRanged)
            {
                combat.Attack(playerPos);
            }
            else
            {
                combat.MeleeAttack(player);
            }
        }

        else if (distanceFromPlayer < agroRange)
        {
            movement.SetTarget(playerPos);
        }
    }
}
