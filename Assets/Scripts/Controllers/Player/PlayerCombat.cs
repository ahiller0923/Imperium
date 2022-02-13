using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackDelay = 1;
    public GameObject projectileModel;
    private GameObject arrow;
    private float shotTime = 0;
    private Animator animator;
    private Vector3 target;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(RaycastHit2D hit)
    {
        if (Time.time - shotTime > attackDelay)
        {
            target = hit.transform.position;
            PlayAnimation(target);
            shotTime = Time.time;
        }
    }

    private void PlayAnimation(Vector3 target)
    {
        if(Mathf.Abs(transform.position.x - target.x) > Mathf.Abs(transform.position.y - target.y)) {
            animator.Play("Player_Attack_EW");
        }

        else
        {
            if(transform.position.y - target.y > 0)
            {
                animator.Play("Player_Attack_S");
            }

            else
            {
                animator.Play("Player_Attack_N");
            }
        }
    }

    public void CreateProjectile()
    {
        arrow = Instantiate(projectileModel, transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), arrow.GetComponent<Collider2D>());
        arrow.GetComponent<ProjectileTargetting>().SetTarget(target);
        arrow.GetComponent<ProjectileTargetting>().SetDamage(50);
    }
}
