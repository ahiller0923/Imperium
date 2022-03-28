using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private Animator animator;
    private bool alive = true;
    private Stats stats;

    public Image healthBar;
    public TextMeshProUGUI healthText;

    void Start()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<Stats>();
        maxHealth = stats.health;
        health = maxHealth;

        if(CompareTag("Player"))
        {
            UpdateUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
            GetComponent<MovementComponent>().TurnOffAnimations();
            GetComponent<MovementComponent>().enabled = false;
            GetComponent<Combat>().enabled = false;
            GetComponent<NpcTargeting>().enabled = false;
            
        }
    }

    public void TakeDamage(float physicalDamage, float magicDamage, GameObject attacker)
    {
        float dmg = stats.CalculateDamageTaken(physicalDamage, magicDamage);
        health -= dmg;
        if (!CompareTag("Player"))
        {
            GetComponent<NpcTargeting>().SetTarget(attacker);
            if(attacker.CompareTag("Player"))
            {
                tag = "Enemy";
                GetComponent<Resolve>().isEnemy = true;
            }
        }
        
        else
        {
            UpdateUI();
        }
        
        if (health > 0)
        {
            //animator.SetTrigger("Hurt");
        }

        else if (health <= 0 && alive)
        {
            HandleDeath();

            if(attacker.CompareTag("Player"))
            {
                if(stats.resolve == 100)
                {
                    attacker.GetComponent<Stats>().resolve -= 2;
                }
                
                else if (stats.resolve == 0)
                {
                    attacker.GetComponent<Stats>().resolve += 2;
                }
            }
        }
    }

    private void UpdateUI()
    {
        float ratio = health / maxHealth;
        healthBar.rectTransform.localPosition = new Vector3(0, healthBar.rectTransform.rect.height * ratio - healthBar.rectTransform.rect.height, 0);
        healthText.text = health.ToString("0") + "/" + maxHealth.ToString("0");
    }
}
