using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MovementComponent : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 targetPosition;
    private GameObject targetObject;
    private Animator animator;
    public bool isFlipped;
    private bool tryAttack = false;
    private Combat combat;
    private SpriteRenderer sprite;
    private AIDestinationSetter destinationSetter;

    private Vector3 lastPos;

    public float speed = 1;

    public bool isMoving;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        targetPosition = transform.position;
        combat = GetComponent<Combat>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        targetObject = new GameObject();

        if (!CompareTag("Player"))
        {
            speed = Random.Range(speed - .2f, speed + .5f);
        }
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        SetAnimations();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        if(tryAttack)
        {
            combat.RangedAttack(targetPosition);
        }
    }

    public void SetAlignment(Vector3 target)
    {
        if (transform.position.x - target.x < 0)
        {
            sprite.flipX = !isFlipped;
        }
        else
        {
            sprite.flipX = isFlipped;
        }
    }

    /* Animation States
     * 0 - Idle
     * 1 - Walk Up
     * 2 - Walk Down
     * 3 - Walk Side
     * */
    private void SetAnimations()
    {
        if(transform.position == lastPos)
        {
            TurnOffMovementAnimations();
            isMoving = false;
        }

        else if(Mathf.Abs(transform.position.x - targetPosition.x) > Mathf.Abs(transform.position.y - targetPosition.y)) {

            SetAlignment(targetPosition);

            animator.SetInteger("AnimState", 3);
            isMoving = true;
        }

        else
        {
            isMoving = true;
            if (transform.position.y - targetPosition.y < 0)
            {
                animator.SetInteger("AnimState", 1);
            }
            else
            {
                animator.SetInteger("AnimState", 2);
            }
        }
        lastPos = transform.position;
    }

    public void TurnOffAnimations()
    {
        animator.SetInteger("AnimState", -1);
        enabled = false;
    }

    private void TurnOffMovementAnimations()
    {
        animator.SetInteger("AnimState", 0);
    }

    public void SetTarget(Vector3 target, bool attack = false)
    {
        targetPosition = target;
        targetPosition.z = 0;
        targetObject.transform.position = targetPosition;

        destinationSetter.target = targetObject.transform;

        tryAttack = attack;
    }

    public Vector3 GetTarget()
    {
        return targetPosition;
    }
}
