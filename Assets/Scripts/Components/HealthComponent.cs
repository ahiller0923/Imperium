using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float health = 100;
    private Animator animator;
    private bool alive = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("alive", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && alive)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        alive = false;
        animator.Play("Enemy_Death");
        animator.SetBool("alive", false);
        GetComponent<MovementComponent>().enabled = false;
    }
}
