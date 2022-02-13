using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private MovementComponent movement;

    public float agroRange = 0;
    public GameObject player;

    private Vector2 playerPos;
    private Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<MovementComponent>();
        movement.SetTarget(transform.position);
        pos = transform.position;
        playerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        pos = transform.position;

        if(Mathf.Sqrt(Mathf.Pow(playerPos.x - pos.x, 2) + Mathf.Pow(playerPos.y - pos.y, 2)) < agroRange)
        {
            movement.SetTarget(playerPos);
        }
    }
}
