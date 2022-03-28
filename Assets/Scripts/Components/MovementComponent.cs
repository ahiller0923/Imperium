using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 targetPosition;
    private Animator animator;
    public bool isFlipped;
    private bool tryAttack = false;
    private Combat combat;

    public float speed = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
        combat = GetComponent<Combat>();

        if(!CompareTag("Player"))
        {
            speed = Random.Range(speed - .2f, speed + .5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
            GetComponent<SpriteRenderer>().flipX = !isFlipped;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = isFlipped;
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
        if(transform.position == targetPosition)
        {
            TurnOffMovementAnimations();
        }

        else if(Mathf.Abs(transform.position.x - targetPosition.x) > Mathf.Abs(transform.position.y - targetPosition.y)) {

            SetAlignment(targetPosition);

            animator.SetInteger("AnimState", 3);
        }

        else
        {

            if (transform.position.y - targetPosition.y < 0)
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

    public void SetTarget(Vector3 target, bool attack = false)
    {
        targetPosition = target;
        targetPosition.z = 0;

        tryAttack = attack;
    }

    public Vector3 GetTarget()
    {
        return targetPosition;
    }
}
