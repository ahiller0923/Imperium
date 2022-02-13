using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 targetPosition;
    private Animator animator;

    public float speed = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
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
    }

    public void SetAlignment(Vector3 target)
    {
        if (transform.position.x - target.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void SetAnimations()
    {
        if(transform.position == targetPosition)
        {
            TurnOffMovementAnimations();
        }

        else if(Mathf.Abs(transform.position.x - targetPosition.x) > Mathf.Abs(transform.position.y - targetPosition.y)) {

            SetAlignment(targetPosition);

            animator.SetBool("walkingUp", false);
            animator.SetBool("walkingDown", false);
            animator.SetBool("walkingSide", true);
        }

        else
        {
            animator.SetBool("walkingSide", false);

            if (transform.position.y - targetPosition.y < 0)
            {
                animator.SetBool("walkingUp", true);
                animator.SetBool("walkingDown", false);
            }
            else
            {
                animator.SetBool("walkingDown", true);
                animator.SetBool("walkingUp", false);
            }
        }
    }

    private void TurnOffMovementAnimations()
    {
        animator.SetBool("walkingUp", false);
        animator.SetBool("walkingDown", false);
        animator.SetBool("walkingSide", false);
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        targetPosition.z = 0;
    }

    public Vector3 GetTarget()
    {
        return targetPosition;
    }
}
