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
    private AIPath path;
    private Stats stats;

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
        stats = GetComponent<Stats>();
        targetObject = new GameObject();
        path = GetComponent<AIPath>();

        if (!CompareTag("Player"))
        {
            speed = Random.Range(stats.moveSpeed - .2f, stats.moveSpeed + .5f);
        }
        lastPos = transform.position;

        GetComponent<AIPath>().maxSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
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

    public void SetAlignment()
    {
        if (targetPosition.x - transform.position.x > 0)
        {
            sprite.flipX = !isFlipped;
        }
        else if (targetPosition.x - transform.position.x < 0)
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
        Vector2 deltaLocation = new Vector2(path.steeringTarget.x - transform.position.x, path.steeringTarget.y - transform.position.y);
        if(path.reachedEndOfPath)
        {
            TurnOffMovementAnimations();
            isMoving = false;
        }

        else if(Mathf.Abs(deltaLocation.x) > Mathf.Abs(deltaLocation.y)) {

            SetAlignment();

            animator.SetInteger("AnimState", 3);
            isMoving = true;
        }

        else
        {
            isMoving = true;
            if (deltaLocation.y > 0)
            {
                animator.SetInteger("AnimState", 1);
            }
            else
            {
                animator.SetInteger("AnimState", 2);
            }
        }
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

    public void SetTarget(Vector3 target, bool moveToAttack = false, bool currentlyAttacking = false)
    {
        targetPosition = target;
        targetPosition.z = 0;

        if (!currentlyAttacking)
        {
            targetObject.transform.position = targetPosition;
        }

        else
        {
            targetObject.transform.position = transform.position;
        }

        destinationSetter.target = targetObject.transform;
        tryAttack = moveToAttack;
    }

    public Vector3 GetTarget()
    {
        return targetPosition;
    }
}
