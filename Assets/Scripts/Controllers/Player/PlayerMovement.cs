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
    public Texture2D attackCursor;

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
        clickPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        hit = Physics2D.Raycast(clickPoint, Vector2.zero);

        if (Keyboard.current.ctrlKey.isPressed)
        {
            if (hit.collider != null)
            {
                Cursor.SetCursor(attackCursor, Vector2.zero, CursorMode.Auto);

                if(Mouse.current.leftButton.isPressed)
                {
                    hit.collider.gameObject.tag = "Enemy";
                    interactWithNpc();
                }
            }

            else
            {
                Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            }
        }

        else if (Mouse.current.leftButton.isPressed)
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.collider == null)
                {
                    DialogueManager.GetInstance().ExitDialogueMode();
                    movement.SetTarget(clickPoint);
                }

                else
                {
                    interactWithNpc();
                }
            }
        }

        else
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void interactWithNpc()
    {
        movement.SetTarget(transform.position);
        movement.SetAlignment(hit.transform.position);
        interact.ProcessInteraction(hit);
    }
}
