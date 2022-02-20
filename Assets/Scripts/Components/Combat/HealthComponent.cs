using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    public float health;
    private Animator animator;
    private bool alive = true;
    public Stats stats;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("alive", true);
        stats = GetComponent<Stats>();
        health = stats.health;
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
        if(gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            alive = false;
            animator.Play("Death");
            animator.SetBool("alive", false);
            GetComponent<MovementComponent>().enabled = false;
            GetComponent<Combat>().enabled = false;
        }
    }

    public void TakeDamage(float physicalDamage, float magicDamage)
    {
        health -= stats.CalculateDamageTaken(physicalDamage, magicDamage);
        if (health > 0)
        {
            animator.SetTrigger("Hurt");
        }
    }
}
