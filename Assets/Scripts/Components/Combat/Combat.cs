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

    private GameObject targetObject;
    private MovementComponent movement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<Stats>();
        target = transform.position;
        movement = GetComponent<MovementComponent>();
    }

    /* If enough time has passed since last attack, attack point of click
     * @param hit - point where mouse was clicked
    */
    public void RangedAttack(Vector3 hit)
    {
        if(Vector2.Distance(transform.position, hit) < attackRange)
        {
            if(Time.time - attackTime > attackDelay)
        {
                target = hit;
                PlayAnimation(target);
                attackTime = Time.time;
            }
        }
        
        else
        {
            movement.SetTarget(hit, true);
        }
    }

    public void MeleeAttack(GameObject player)
    {
        if (Time.time - attackTime > attackDelay && animator != null)
        {
            animator.SetBool("attacking", true);
            PlayAnimation(player.transform.position);
            attackTime = Time.time;
            targetObject = player;
        }
    }

    public void DealDamage()
    {
        if(Vector2.Distance(targetObject.transform.position, transform.position) < attackRange)
        {
            targetObject.GetComponent<HealthComponent>().TakeDamage(stats.CalculatePhysicalDamageDealt(basePhysicalDamage), 0, gameObject);
        }
        
        animator.SetBool("attacking", false);
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
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), arrow.GetComponent<Collider2D>());

        // Set target and damage
        ProjectileTargetting arrowSettings = arrow.GetComponent<ProjectileTargetting>();
        arrowSettings.SetTarget(target);
        arrowSettings.SetSpeed(stats.projectileSpeed);
        arrowSettings.SetPhysicalDamage(stats.CalculatePhysicalDamageDealt(basePhysicalDamage));
        arrowSettings.SetAttacker(gameObject);
    }
}

