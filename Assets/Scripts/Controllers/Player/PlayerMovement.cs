using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    MovementComponent movement;
    PlayerInteraction interact;

    RaycastHit2D hit;
    Vector3 clickPoint;

    void Start()
    {
        movement = GetComponent<MovementComponent>();
        interact = GetComponent<PlayerInteraction>();
        GetComponent<Animator>().SetBool("attackOnce", true);
    }

    void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            clickPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            hit = Physics2D.Raycast(clickPoint, Vector2.zero);

            if(hit.collider == null)
            {
                movement.SetTarget(clickPoint);
            }

            else
            {
                movement.SetTarget(transform.position);
                movement.SetAlignment(hit.transform.position);
                interact.ProcessInteraction(hit);
            }
        }
    }
}
