using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    MovementComponent movement;
    PlayerInteraction interact;

    public Texture2D cursorTexture;

    RaycastHit2D hit;
    Vector3 clickPoint;

    void Start()
    {
        movement = GetComponent<MovementComponent>();
        interact = GetComponent<PlayerInteraction>();
        GetComponent<Animator>().SetBool("attackOnce", true);
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            clickPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            hit = Physics2D.Raycast(clickPoint, Vector2.zero);

            if(!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.collider == null)
                {
                    DialogueManager.GetInstance().ExitDialogueMode();
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
}
