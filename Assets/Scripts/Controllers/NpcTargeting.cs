using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTargeting : MonoBehaviour
{

    private Queue<GameObject> targets;
    public GameObject target;
    private Combat combat;
    private MovementComponent movement;

    public bool isRanged;

    private float distanceFromPlayer;

    public float immersiveMoveTimeLimit = 20;
    public int randomMove = 2000;
    private int moveCheck;
    private float immersiveMoveTime;

    public float immersiveMoveDistance;

    public Vector3 villageCenter = new Vector3(15, 5, 0);

    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<Combat>();
        movement = GetComponent<MovementComponent>();
        targets = new Queue<GameObject>();
        moveCheck = randomMove;
        immersiveMoveTime = Time.time;
        immersiveMoveDistance = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            NewTarget();
        }

        else
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
        if(targets.Count > 0)
        {
            target = targets.Dequeue();
        }
        
        else
        {
            DocileMovement();
        }
    }

    public void SetTarget(GameObject enemy)
    {
        targets.Enqueue(enemy);
        NewTarget();
    }

    private void CheckAttack()
    {
        distanceFromPlayer = Vector2.Distance(target.transform.position, transform.position);

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

    private void DocileMovement()
    {
        if (targets.Count == 0 && target == null)
        {
            if (Random.Range(0, moveCheck) == 7)
            {
                Vector3 moveLocation = new Vector3(Random.Range(-immersiveMoveDistance, immersiveMoveDistance) + villageCenter.x,
                    Random.Range(-immersiveMoveDistance, immersiveMoveDistance) + villageCenter.y, 0);
                movement.SetTarget(moveLocation);
                moveCheck = randomMove;
                immersiveMoveTime = Time.time;
            }

            else if (Time.time - immersiveMoveTime >= immersiveMoveTimeLimit)
            {
                moveCheck = moveCheck / 2;
                immersiveMoveTime = Time.time;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.collider.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.collider);
        }
    }
}
