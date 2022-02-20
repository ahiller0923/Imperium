using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public float attackDelay;
    public float attackRange;
    public float agroRange;
    public float basePhysicalDamage;
    public float baseMagicDamage;

    public GameObject projectileModel;
    private GameObject arrow;

    private float attackTime;
    private Vector3 target;

    private Animator animator;
    private Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<Stats>();
        target = transform.position;
    }

    /* If enough time has passed since last attack, attack point of click
     * @param hit - point where mouse was clicked
    */
    public void RangedAttack(Vector3 hit)
    {
        if (Time.time - attackTime > attackDelay)
        {
            target = hit;
            PlayAnimation(target);
            attackTime = Time.time;
        }
    }

    public void MeleeAttack(GameObject player)
    {
        if (Time.time - attackTime > attackDelay)
        {
            PlayAnimation(player.transform.position);
            attackTime = Time.time;
            player.GetComponent<HealthComponent>().TakeDamage(stats.CalculatePhysicalDamageDealt(basePhysicalDamage), 0);
        }
        
    }

    /* Determine which animation should play based on position of target relative to player position
     * @param target - position of the target enemy
    */
    private void PlayAnimation(Vector3 target)
    {
        if (Mathf.Abs(transform.position.x - target.x) > Mathf.Abs(transform.position.y - target.y))
        {
            animator.Play("Attack_EW");
        }

        else
        {
            if (transform.position.y - target.y > 0)
            {
                animator.Play("Attack_S");
            }

            else
            {
                animator.Play("Attack_N");
            }
        }
    }

    public void CreateProjectile()
    {
        // Create projectile object
        arrow = Instantiate(projectileModel, transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), arrow.GetComponent<Collider2D>());

        // Set target and damage
        arrow.GetComponent<ProjectileTargetting>().SetTarget(target);
        arrow.GetComponent<ProjectileTargetting>().SetSpeed(stats.projectileSpeed);
        arrow.GetComponent<ProjectileTargetting>().SetPhysicalDamage(stats.CalculatePhysicalDamageDealt(basePhysicalDamage));
    }
}

