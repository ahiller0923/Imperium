using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTargetting : MonoBehaviour
{
    public float speed = 10;
    private Vector3 targetPosition;
    private float damage = 0;

    void Start()
    {
        //targetPosition = transform.position;
        //transform.LookAt(targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        Vector3 direction = target - transform.position;
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
        transform.Rotate(new Vector3(0, 0, -90));
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            collision.collider.gameObject.GetComponent<HealthComponent>().health -= damage;
        }
        Destroy(gameObject);
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
}
