using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public bool canBlink = true;
    public float blinkCoolDown = 3;
    public float blinkDist = 1f;
    public int blinkChargesMax = 3;

    public int currentBlinkCharges;
    private float blinkTime = 0;
    private float baseBlinkTime = 1; // to make sure all charges aren't used at once
    private float lastBlink;

    private Animator animator;
    private MovementComponent movement;

    public void Start()
    {
        currentBlinkCharges = blinkChargesMax;
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<MovementComponent>();
    }

    public void Update()
    {
        if(currentBlinkCharges < blinkChargesMax)
        {
            if(Time.time - blinkTime > blinkCoolDown)
            {
                currentBlinkCharges++;
                blinkTime = Time.time;
            }
        }
    }

    public void Blink(Vector3 target)
    {
        if(currentBlinkCharges != 0 && canBlink && Time.time - lastBlink > baseBlinkTime)
        {
            animator.Play("Player_Blink");
            animator.SetInteger("AnimState", 0);
            transform.position = Vector3.MoveTowards(transform.position, target, blinkDist);
            movement.SetTarget(transform.position);
            lastBlink = Time.time;
            currentBlinkCharges--;
        }
    }
}
