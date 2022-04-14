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
    private PlayerAbilities abilities;

    public Texture2D cursorTexture;
    public Texture2D attackCursor;

    RaycastHit2D hit;
    Vector3 clickPoint;

    private Camera camera;

    void Start()
    {
        movement = GetComponent<MovementComponent>();
        interact = GetComponent<PlayerInteraction>();
        GetComponentInChildren<Animator>().SetBool("attackOnce", true);
        abilities = GetComponent<PlayerAbilities>();
        camera = GetComponentInChildren<Animator>().gameObject.GetComponentInChildren<Camera>();
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
                if (hit.collider == null || (!hit.collider.CompareTag("Enemy") && !hit.collider.CompareTag("NPC") && !hit.collider.CompareTag("Gargoyle")))
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

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Cursor.SetCursor(attackCursor, Vector2.zero, CursorMode.Auto);
        }

        

        if (Keyboard.current.eKey.isPressed)
        {
            abilities.Blink(clickPoint);
            
        }

        if(Mouse.current.scroll.ReadValue() != Vector2.zero)
        {
            if(Mouse.current.scroll.ReadValue().y < 0 && camera.orthographicSize < 4)
            {
                camera.orthographicSize += .2f;
            }

            else if(Mouse.current.scroll.ReadValue().y > 0 && camera.orthographicSize > 1)
            {
                camera.orthographicSize -= .2f;
            }
        }
    }

    private void interactWithNpc()
    {
        movement.SetTarget(hit.transform.position, true, true);
        //movement.SetAlignment(new Vector2(hit.transform.position.x - transform.position.x, hit.transform.position.y - transform.position.y));
        interact.ProcessInteraction(hit);
    }
}
