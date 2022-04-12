using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderingOrder : MonoBehaviour
{
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        if(GetComponentInParent<MovementComponent>() == null)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
